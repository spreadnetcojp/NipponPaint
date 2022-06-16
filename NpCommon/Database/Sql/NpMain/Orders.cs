//*****************************************************************************
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
        // DB
        private const string DATABASE_NP_MAIN = "NP_MAIN";
        private const string DATABASE_ORDER = "ORDER_"; //ORDER_xx  ←　xxはPlantの値をSQL内で指定する
        // テーブル
        private const string MAIN_TABLE = "Orders";
        // カラム
        private const string COLUMN_ORDER_ID = "Order_id";
        private const string COLUMN_ORDER_NUMBER = "Order_Number";
        private const string COLUMN_COLOR_NAME = "Color_Name";
        private const string COLUMN_PRODUCT_CODE = "Product_Code";
        private const string COLUMN_NUMBER_OF_CAN = "Number_of_cans";
        private const string COLUMN_PAINT_NAME = "Paint_Name";
        private const string COLUMN_WHITE_CODE = "White_Code";
        private const string COLUMN_REVISION = "Revision";
        public const string COLUMN_TOTAL_WEIGHT = "Total_Weight";
        private const string COLUMN_OVERFILLING = "Overfilling";
        private const string COLUMN_LABEL_TYPE = "Label_Type";
        private const string COLUMN_LABEL_DESCRIPTION = "Label_Description";
        private const string COLUMN_CAN_TYPE = "Can_Type";
        private const string COLUMN_CAN_DESCRIPTION = "Can_Description";
        private const string COLUMN_CAN_WEIGHT = "Can_Weight";
        private const string COLUMN_CAN_NOMINAL = "Can_Nominal";
        private const string COLUMN_CAN_AVAILABLE = "Can_Available";
        private const string COLUMN_CAP_TYPE = "Cap_Type";
        private const string COLUMN_CAP_DESCRIPTION = "Cap_Description";
        private const string COLUMN_CAP_WEIGHT = "Cap_Weight";
        private const string COLUMN_HOLE_SIZE = "Hole_Size";
        private const string COLUMN_CAPPING_MACHINE = "Capping_Machine";
        private const string COLUMN_STATUS = "Status";
        private const string COLUMN_PREFILL_AMOUNT = "Prefill_Amount";
        private const string COLUMN_P_WEIGHT_TOLERANCE = "P_Weight_Tolerance";
        private const string COLUMN_N_WEIGHT_TOLERANCE = "N_Weight_Tolerance";
        private const string COLUMN_QUALITY_SAMPLE = "Quality_Sample";
        private const string COLUMN_MIXING_TIME = "Mixing_Time";
        private const string COLUMN_MIXING_SPEED = "Mixing_Speed";
        private const string COLUMN_COLORANT_1 = "Colorant_1";
        public const string COLUMN_WEIGHT_1 = "Weight_1";
        private const string COLUMN_COLORANT_2 = "Colorant_2";
        public const string COLUMN_WEIGHT_2 = "Weight_2";
        private const string COLUMN_COLORANT_3 = "Colorant_3";
        public const string COLUMN_WEIGHT_3 = "Weight_3";
        private const string COLUMN_COLORANT_4 = "Colorant_4";
        public const string COLUMN_WEIGHT_4 = "Weight_4";
        private const string COLUMN_COLORANT_5 = "Colorant_5";
        public const string COLUMN_WEIGHT_5 = "Weight_5";
        private const string COLUMN_COLORANT_6 = "Colorant_6";
        public const string COLUMN_WEIGHT_6 = "Weight_6";
        private const string COLUMN_COLORANT_7 = "Colorant_7";
        public const string COLUMN_WEIGHT_7 = "Weight_7";
        private const string COLUMN_COLORANT_8 = "Colorant_8";
        public const string COLUMN_WEIGHT_8 = "Weight_8";
        private const string COLUMN_COLORANT_9 = "Colorant_9";
        public const string COLUMN_WEIGHT_9 = "Weight_9";
        private const string COLUMN_COLORANT_10 = "Colorant_10";
        public const string COLUMN_WEIGHT_10 = "Weight_10";
        private const string COLUMN_COLORANT_11 = "Colorant_11";
        public const string COLUMN_WEIGHT_11 = "Weight_11";
        private const string COLUMN_COLORANT_12 = "Colorant_12";
        public const string COLUMN_WEIGHT_12 = "Weight_12";
        private const string COLUMN_COLORANT_13 = "Colorant_13";
        public const string COLUMN_WEIGHT_13 = "Weight_13";
        private const string COLUMN_COLORANT_14 = "Colorant_14";
        public const string COLUMN_WEIGHT_14 = "Weight_14";
        private const string COLUMN_COLORANT_15 = "Colorant_15";
        public const string COLUMN_WEIGHT_15 = "Weight_15";
        private const string COLUMN_COLORANT_16 = "Colorant_16";
        public const string COLUMN_WEIGHT_16 = "Weight_16";
        private const string COLUMN_COLORANT_17 = "Colorant_17";
        public const string COLUMN_WEIGHT_17 = "Weight_17";
        private const string COLUMN_COLORANT_18 = "Colorant_18";
        public const string COLUMN_WEIGHT_18 = "Weight_18";
        private const string COLUMN_COLORANT_19 = "Colorant_19";
        public const string COLUMN_WEIGHT_19 = "Weight_19";

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
        public static string GetDetailOrderStartByOrderId(string plant)
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT ");
            sql.Append($"  o.{COLUMN_ORDER_NUMBER}                  AS OrderNumber ");
            sql.Append($" ,o.{COLUMN_COLOR_NAME}                    AS ColorName ");
            sql.Append($" ,o.{COLUMN_PRODUCT_CODE}                  AS ProductCode ");
            sql.Append($" ,o.{COLUMN_NUMBER_OF_CAN}                 AS NumberOfCan ");
            sql.Append($" ,o.{COLUMN_PAINT_NAME}                    AS ItemName ");
            sql.Append($" ,o.{COLUMN_PAINT_NAME}                    AS FormalItemName ");
            sql.Append($" ,o.{COLUMN_WHITE_CODE}                    AS WhiteCode ");
            sql.Append($" ,o.{COLUMN_REVISION}                      AS Revision ");
            sql.Append($" ,o.{COLUMN_TOTAL_WEIGHT}                  AS TotalWeight ");
            sql.Append($" ,d.{Order.Defaults.COLUMN_CAN_TYPE}       AS CanType ");
            sql.Append($" ,d.{Order.Defaults.COLUMN_CAP_TYPE}       AS CapType ");
            sql.Append($" ,d.{Order.Defaults.COLUMN_OVERFILLING}    AS Overfilling ");
            sql.Append($" ,d.{Order.Defaults.COLUMN_PREFILL_AMOUNT} AS PrefillAmount ");
            sql.Append($" ,d.{Order.Defaults.COLUMN_MIXING_TIME}    AS MixingTime ");
            sql.Append($" ,d.{Order.Defaults.COLUMN_MIXING_SPEED}   AS MixingSpeed ");
            sql.Append($" ,o.{COLUMN_ORDER_ID}                      AS OrderId ");
            sql.Append($"FROM NP_MAIN.dbo.{MAIN_TABLE} o ");
            sql.Append($"LEFT JOIN {DATABASE_ORDER}{plant}.dbo.{Order.Defaults.MAIN_TABLE} d ON o.{COLUMN_WHITE_CODE} = d.White_Code ");
            sql.Append($"WHERE {COLUMN_ORDER_ID} = @orderId ");
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

        #region
        /// <summary>
        /// 重量１～１９の取得
        /// </summary>
        /// <returns></returns>
        public static string GetItemsWeightValue()
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT ");
            sql.Append($"  {COLUMN_WEIGHT_1} ");
            sql.Append($" ,{COLUMN_WEIGHT_2} ");
            sql.Append($" ,{COLUMN_WEIGHT_3} ");
            sql.Append($" ,{COLUMN_WEIGHT_4} ");
            sql.Append($" ,{COLUMN_WEIGHT_5} ");
            sql.Append($" ,{COLUMN_WEIGHT_6} ");
            sql.Append($" ,{COLUMN_WEIGHT_7} ");
            sql.Append($" ,{COLUMN_WEIGHT_8} ");
            sql.Append($" ,{COLUMN_WEIGHT_9} ");
            sql.Append($" ,{COLUMN_WEIGHT_10} ");
            sql.Append($" ,{COLUMN_WEIGHT_11} ");
            sql.Append($" ,{COLUMN_WEIGHT_12} ");
            sql.Append($" ,{COLUMN_WEIGHT_13} ");
            sql.Append($" ,{COLUMN_WEIGHT_14} ");
            sql.Append($" ,{COLUMN_WEIGHT_15} ");
            sql.Append($" ,{COLUMN_WEIGHT_16} ");
            sql.Append($" ,{COLUMN_WEIGHT_17} ");
            sql.Append($" ,{COLUMN_WEIGHT_18} ");
            sql.Append($" ,{COLUMN_WEIGHT_19} ");
            sql.Append($"FROM {DATABASE_NP_MAIN}.dbo.{MAIN_TABLE} ");
            sql.Append($"WHERE {COLUMN_ORDER_ID} = @orderId ");
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
            sql.Append($"{MAIN_TABLE} ");
            sql.Append($"SET ");
            sql.Append($" {COLUMN_LABEL_TYPE}         = @LabelType ");
            sql.Append($",{COLUMN_LABEL_DESCRIPTION}  = @LabelDescription ");
            sql.Append($",{COLUMN_CAN_TYPE}           = @CanType ");
            sql.Append($",{COLUMN_CAN_DESCRIPTION}    = @CanDescription ");
            sql.Append($",{COLUMN_CAN_WEIGHT}         = @CanWeight ");
            sql.Append($",{COLUMN_CAN_NOMINAL}        = @CanNominal ");
            sql.Append($",{COLUMN_CAN_AVAILABLE}      = @CanAvailable ");
            sql.Append($",{COLUMN_CAP_TYPE}           = @CapType ");
            sql.Append($",{COLUMN_CAP_DESCRIPTION}    = @CapDescription ");
            sql.Append($",{COLUMN_CAP_WEIGHT}         = @CapWeight ");
            sql.Append($",{COLUMN_HOLE_SIZE}          = @CapHoleSize ");
            sql.Append($",{COLUMN_CAPPING_MACHINE}    = @CappingMachine ");
            sql.Append($",{COLUMN_STATUS}             = @Status ");
            sql.Append($",{COLUMN_OVERFILLING}        = @Overfilling ");
            sql.Append($",{COLUMN_PREFILL_AMOUNT}     = @PrefillAmount ");
            sql.Append($",{COLUMN_P_WEIGHT_TOLERANCE} = @PWeightTolerance ");
            sql.Append($",{COLUMN_N_WEIGHT_TOLERANCE} = @NWeightTolerance ");
            sql.Append($",{COLUMN_QUALITY_SAMPLE}     = @QualitySample ");
            sql.Append($",{COLUMN_MIXING_TIME}        = @MixingTime ");
            sql.Append($",{COLUMN_MIXING_SPEED}       = @MixingSpeed ");
            sql.Append($",{COLUMN_WEIGHT_1}           = @Weight_1 ");
            sql.Append($",{COLUMN_WEIGHT_2}           = @Weight_2 ");
            sql.Append($",{COLUMN_WEIGHT_3}           = @Weight_3 ");
            sql.Append($",{COLUMN_WEIGHT_4}           = @Weight_4 ");
            sql.Append($",{COLUMN_WEIGHT_5}           = @Weight_5 ");
            sql.Append($",{COLUMN_WEIGHT_6}           = @Weight_6 ");
            sql.Append($",{COLUMN_WEIGHT_7}           = @Weight_7 ");
            sql.Append($",{COLUMN_WEIGHT_8}           = @Weight_8 ");
            sql.Append($",{COLUMN_WEIGHT_9}           = @Weight_9 ");
            sql.Append($",{COLUMN_WEIGHT_10}          = @Weight_10 ");
            sql.Append($",{COLUMN_WEIGHT_11}          = @Weight_11 ");
            sql.Append($",{COLUMN_WEIGHT_12}          = @Weight_12 ");
            sql.Append($",{COLUMN_WEIGHT_13}          = @Weight_13 ");
            sql.Append($",{COLUMN_WEIGHT_14}          = @Weight_14 ");
            sql.Append($",{COLUMN_WEIGHT_15}          = @Weight_15 ");
            sql.Append($",{COLUMN_WEIGHT_16}          = @Weight_16 ");
            sql.Append($",{COLUMN_WEIGHT_17}          = @Weight_17 ");
            sql.Append($",{COLUMN_WEIGHT_18}          = @Weight_18 ");
            sql.Append($",{COLUMN_WEIGHT_19}          = @Weight_19 ");
            sql.Append($",{COLUMN_TOTAL_WEIGHT}       = @TotalWeight ");
            sql.Append($"WHERE {COLUMN_ORDER_NUMBER} = @OrderNumber ");
            return sql.ToString();
        }
        #endregion

        #region
        /// <summary>
        /// テスト缶仕上がりボタンを押下した際のstatus更新
        /// </summary>
        /// <returns></returns>
        public static string StatusProductionChange()
        {
            var sql = new StringBuilder();
            sql.Append($"UPDATE ");
            sql.Append($" {MAIN_TABLE} ");
            sql.Append($"SET ");
            sql.Append($" {COLUMN_STATUS}        = @Status ");
            sql.Append($"WHERE {COLUMN_ORDER_ID} = @OrderId ");
            return sql.ToString();
        }
        #endregion

        #endregion
    }
}
