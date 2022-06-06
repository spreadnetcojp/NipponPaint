﻿//*****************************************************************************
//
//  システム名：調色工場用自動計量システム NpCommon
//
//  Copyright 三菱電機エンジニアリング株式会社 2022 All rights reserved.
//
//-----------------------------------------------------------------------------
//  変更履歴:
//  Ver      日付        担当       コメント
//  0.0      2022/04/30  A.Satou    新規作成
#region 更新履歴
#endregion
//*****************************************************************************

#region using defines
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NipponPaint.NpCommon.Settings;
using System.ComponentModel.DataAnnotations;
#endregion

namespace NipponPaint.NpCommon.Database.Sql.NpMain
{
    public static class Orders
    {
        #region 定数
        /// <summary>
        /// オーダーステータス
        /// </summary>
        public enum OrderStatus
        {
            /// <summary>
            /// 調色担当待ち
            /// </summary>
            [Display(Description = "調色担当待ち")]
            WaitingForToning = 0,
            /// <summary>
            /// CCM配合待ち
            /// </summary>
            [Display(Description = "CCM配合待ち")]
            WaitingForCCMformulation = 1,
            /// <summary>
            /// 準備完
            /// </summary>
            [Display(Description = "準備完")]
            Ready = 2,
            /// <summary>
            /// テスト缶実施中
            /// </summary>
            [Display(Description = "テスト缶実施中")]
            TestCanInProgress = 3,
            /// <summary>
            /// 製造缶実施中
            /// </summary>
            [Display(Description = "製造缶実施中")]
            ManufacturingCansInProgress = 4,
        }
        /// <summary>
        /// 配送区分
        /// </summary>
        public enum DeliveryCode
        {
            /// <summary>
            /// 区域1
            /// </summary>
            [Display(Description = "区域1")]
            Area1 = 1,
            /// <summary>
            /// 区域2
            /// </summary>
            [Display(Description = "区域2")]
            Area2 = 2,
            /// <summary>
            /// 使い
            /// </summary>
            [Display(Description = "使い")]
            Reuse = 3,
            /// <summary>
            /// 未使用
            /// </summary>
            [Display(Description = "未使用(4)")]
            DeliveryCode4 = 4,
            /// <summary>
            /// 未使用
            /// </summary>
            [Display(Description = "未使用(5)")]
            DeliveryCode5 = 5,
            /// <summary>
            /// 路線
            /// </summary>
            [Display(Description = "路線")]
            Route = 6,
            /// <summary>
            /// 区域A
            /// </summary>
            [Display(Description = "区域A")]
            AreaA = 7,
            /// <summary>
            /// 区域B
            /// </summary>
            [Display(Description = "区域B")]
            AreaB = 8,
            /// <summary>
            /// 特別便
            /// </summary>
            [Display(Description = "特別便")]
            Special = 9,
        }
        #endregion

        #region 参照系

        #region 一覧データの取得
        /// <summary>
        /// 一覧データの取得
        /// </summary>
        /// <param name="viewSettings"></param>
        /// <param name="plant"></param>
        /// <returns></returns>
        public static string GetPreview(List<GridViewSetting> viewSettings, string plant)
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT ");
            int cnt = 0;
            foreach (var item in viewSettings)
            {
                if (cnt > 0)
                {
                    sql.Append(",");
                }
                sql.Append(item.SqlSentence);
                cnt++;
            }
            sql.Append($"FROM {SelectOrders(plant)} ");
            sql.Append($"WHERE Status IN (0, 1, 2, 3, 4) ");
            sql.Append($"ORDER BY ");
            sql.Append($"  Status ");
            sql.Append($" ,HG_HG_Delivery_Code ");
            return sql.ToString();
        }
        #endregion

        #region 一覧データの取得
        /// <summary>
        /// 一覧データの取得
        /// </summary>
        /// <param name="viewSettings"></param>
        /// <returns></returns>
        public static string GetPreviewCloseOrders(OrderStatus selectedStatus, string plant)
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT ");
            sql.Append($"  Order_id ");
            sql.Append($" ,Product_Code ");
            sql.Append($" ,Status ");
            sql.Append($" ,CASE Status ");
            sql.Append($"   WHEN {(int)OrderStatus.WaitingForToning}            THEN N'{Messages.GetOrderStatusText(OrderStatus.WaitingForToning)}' ");
            sql.Append($"   WHEN {(int)OrderStatus.WaitingForCCMformulation}    THEN N'{Messages.GetOrderStatusText(OrderStatus.WaitingForCCMformulation)}' ");
            sql.Append($"   WHEN {(int)OrderStatus.Ready}                       THEN N'{Messages.GetOrderStatusText(OrderStatus.Ready)}' ");
            sql.Append($"   WHEN {(int)OrderStatus.TestCanInProgress}           THEN N'{Messages.GetOrderStatusText(OrderStatus.TestCanInProgress)}' ");
            sql.Append($"   WHEN {(int)OrderStatus.ManufacturingCansInProgress} THEN N'{Messages.GetOrderStatusText(OrderStatus.ManufacturingCansInProgress)}' ");
            sql.Append($"  END AS StatusText ");
            sql.Append($" ,Operator_Name ");
            sql.Append($" ,HG_Product_Name ");
            sql.Append($" ,TRIM(TRIM('　' FROM HG_Volume_Code)) ");
            sql.Append($" ,Number_of_cans ");
            sql.Append($" ,Order_Number ");
            sql.Append($"FROM Orders AS O ");
            sql.Append($"INNER JOIN (SELECT SS_Code FROM Plants WHERE REPLACE(Plant_Description, ' ', '') = '{plant}')  AS P ON P.SS_Code = O.HG_SS_Code ");
            sql.Append($"WHERE Status = {(int)selectedStatus} ");
            sql.Append($"ORDER BY ");
            sql.Append($"  Status ");
            sql.Append($" ,HG_HG_Delivery_Code ");
            return sql.ToString();
        }
        #endregion

        #region オーダーIDによるレコード取得
        /// <summary>
        /// オーダーIDによるレコード取得
        /// </summary>
        /// <returns></returns>
        public static string GetDetailByOrderId(string plant)
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT ");
            sql.Append($" O.* ");
            sql.Append($",Can.Can_Description ");
            sql.Append($",Cap.Cap_Description ");
            sql.Append($"FROM Orders AS O ");
            sql.Append($"INNER JOIN (SELECT SS_Code FROM Plants WHERE REPLACE(Plant_Description, ' ', '') = '{plant}')  AS P ON P.SS_Code = O.HG_SS_Code ");
            sql.Append($"Left JOIN ORDER_{plant}..Can_types AS Can ON O.Can_Type = Can.Can_Type ");
            sql.Append($"Left JOIN ORDER_{plant}..Cap_types AS Cap ON O.Cap_Type = Cap.Cap_Type ");
            sql.Append($"WHERE O.order_id = @orderId");
            return sql.ToString();
        }
        #endregion

        #region オーダーIDによるレコード取得（注文開始画面表示用）
        /// <summary>
        /// オーダーIDによるレコード取得
        /// </summary>
        /// <returns></returns>
        public static string GetDetailOrderStartByOrderId()
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT ");
            sql.Append($"  Order_Number   AS OrderNumber ");
            sql.Append($" ,Color_Name     AS ColorName ");
            sql.Append($" ,Product_Code   AS ProductCode ");
            sql.Append($" ,Number_of_cans AS NumberOfCan ");
            sql.Append($" ,Paint_Name     AS ItemName ");
            sql.Append($" ,Paint_Name     AS FormalItemName ");
            sql.Append($" ,White_Code     AS WhiteCode ");
            sql.Append($" ,Revision       AS Revision ");
            sql.Append($" ,Total_Weight   AS TotalWeight ");
            sql.Append($" ,Overfilling    AS Overfilling ");
            sql.Append($"FROM Orders ");
            sql.Append($"WHERE order_id = @orderId ");
            return sql.ToString();
        }
        #endregion

        #region 処理Noによるレコード取得

        /// <summary>
        /// 処理Noによるレコード取得
        /// </summary>
        /// <returns></returns>
        public static string GetDetailByDataNumber(string plant)
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT ");
            sql.Append($" O.* ");
            sql.Append($"FROM Orders AS O ");
            sql.Append($"INNER JOIN (SELECT SS_Code FROM Plants WHERE REPLACE(Plant_Description, ' ', '') = '{plant}')  AS P ON P.SS_Code = O.HG_SS_Code ");
            sql.Append($"WHERE HG_Data_Number = @dataNumber");
            return sql.ToString();
        }
        #endregion

        #region 調合状況の取得
        /// <summary>
        /// 調合状況の取得
        /// </summary>
        /// <returns></returns>
        public static string GetPreviewDispensed(string plant)
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT O.Order_id, O.White_Code AS Code, 0 As Row_Index, O.White_Weight AS Weight FROM {SelectOrders(plant)} ");
            for (var cnt = 1; cnt < 20; cnt++)
            {
                sql.Append($"UNION ALL SELECT O.Order_id, O.Colorant_{cnt} AS Code, {cnt} As Row_Index, O.Weight_{cnt} AS Weight FROM {SelectOrders(plant)} ");
            }
            return sql.ToString();
        }
        #endregion

        #region Ordersテーブルをplantで抽出
        /// <summary>
        /// Ordersテーブルをplantで抽出
        /// 単体では使用せず、FROM句のあとに呼び出す
        /// </summary>
        /// <param name="plant"></param>
        /// <returns></returns>
        private static string SelectOrders(string plant)
        {
            var sql = new StringBuilder();
            sql.Append($"Orders AS O INNER JOIN (SELECT SS_Code FROM Plants WHERE REPLACE(Plant_Description, ' ', '') = '{plant}')  AS P ON P.SS_Code = O.HG_SS_Code ");
            return sql.ToString();
        }
        #endregion

        #endregion

        #region 更新系

        #region ステータスを戻す
        /// <summary>
        /// ステータスを戻す
        /// </summary>
        /// <returns></returns>
        public static string StatusResume()
        {
            var sql = new StringBuilder();
            sql.Append($"UPDATE ");
            sql.Append($"Orders ");
            sql.Append($"SET ");
            sql.Append($"Status = {(int)OrderStatus.WaitingForCCMformulation} ");
            sql.Append($",Formula_Release = 0 ");
            sql.Append($",White_Code = '' ");
            sql.Append($",White_Weight = 0 ");
            sql.Append($",Colorant_1 = '' ");
            sql.Append($",Weight_1 = 0 ");
            sql.Append($",Colorant_2 = '' ");
            sql.Append($",Weight_2 = 0 ");
            sql.Append($",Colorant_3 = '' ");
            sql.Append($",Weight_3 = 0 ");
            sql.Append($",Colorant_4 = '' ");
            sql.Append($",Weight_4 = 0 ");
            sql.Append($",Colorant_5 = '' ");
            sql.Append($",Weight_5 = 0 ");
            sql.Append($",Colorant_6 = '' ");
            sql.Append($",Weight_6 = 0 ");
            sql.Append($",Colorant_7 = '' ");
            sql.Append($",Weight_7 = 0 ");
            sql.Append($",Colorant_8 = '' ");
            sql.Append($",Weight_8 = 0 ");
            sql.Append($",Colorant_9 = '' ");
            sql.Append($",Weight_9 = 0 ");
            sql.Append($",Colorant_10 = '' ");
            sql.Append($",Weight_10 = 0 ");
            sql.Append($",Colorant_11 = '' ");
            sql.Append($",Weight_11= 0 ");
            sql.Append($",Colorant_12 = '' ");
            sql.Append($",Weight_12 = 0 ");
            sql.Append($",Colorant_13 = '' ");
            sql.Append($",Weight_13 = 0 ");
            sql.Append($",Colorant_14 = '' ");
            sql.Append($",Weight_14 = 0 ");
            sql.Append($",Colorant_15 = '' ");
            sql.Append($",Weight_15 = 0 ");
            sql.Append($",Colorant_16 = '' ");
            sql.Append($",Weight_16 = 0 ");
            sql.Append($",Colorant_17 = '' ");
            sql.Append($",Weight_17 = 0 ");
            sql.Append($",Colorant_18 = '' ");
            sql.Append($",Weight_18 = 0 ");
            sql.Append($",Colorant_19 = '' ");
            sql.Append($",Weight_19 = 0 ");
            sql.Append($",Total_Weight = 0 ");
            sql.Append($"FROM Orders ");
            sql.Append($"WHERE ");
            sql.Append($"order_id = @orderId");

            return sql.ToString();
        }
        #endregion

        #region オペレータ削除
        /// <summary>
        /// オペレータ削除
        /// </summary>
        /// <returns></returns>
        public static string DeleteOperator()
        {
            var sql = new StringBuilder();
            sql.Append($"UPDATE ");
            sql.Append($"Orders ");
            sql.Append($"SET ");
            sql.Append($"Status = {(int)OrderStatus.WaitingForToning} ");
            sql.Append($",Operator_Code = '' ");
            sql.Append($",Operator_Name = '' ");
            sql.Append($"WHERE ");
            sql.Append($"order_id = @orderId");
            return sql.ToString();
        }
        #endregion

        #region
        /// <summary>
        /// 注文開始
        /// </summary>
        /// <returns></returns>
        public static string StartOrder()
        {
            var sql = new StringBuilder();
            sql.Append($"UPDATE ");
            sql.Append($"Orders ");
            sql.Append($"SET ");
            sql.Append($" Label_Type         = @LabelType ");
            sql.Append($",Label_Description  = @LabelDescription ");
            sql.Append($",Can_Type           = @CanType ");
            sql.Append($",Can_Description    = @CanDescription ");
            sql.Append($",Can_Weight         = @CanWeight ");
            sql.Append($",Can_Nominal        = @CanNominal ");
            sql.Append($",Can_Available      = @CanAvailable ");
            sql.Append($",Cap_Type           = @CapType ");
            sql.Append($",Cap_Description    = @CapDescription ");
            sql.Append($",Cap_Weight         = @CapWeight ");
            sql.Append($",Hole_Size          = @CapHoleSize ");
            sql.Append($",Capping_Machine    = @CappingMachine ");
            sql.Append($",Status             = @Status ");
            sql.Append($",Overfilling        = @Overfilling ");
            sql.Append($",Prefill_Amount     = @PrefillAmount ");
            sql.Append($",P_Weight_Tolerance = @PWeightTolerance ");
            sql.Append($",N_Weight_Tolerance = @NWeightTolerance ");
            sql.Append($",Quality_Sample     = @QualitySample ");
            sql.Append($",Mixing_Time        = @MixingTime ");
            sql.Append($",Mixing_Speed       = @MixingSpeed ");
            sql.Append($"WHERE Order_Number  = @OrderNumber ");
            return sql.ToString();
        }
        #endregion

        #endregion
    }
}
