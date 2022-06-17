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
using NipponPaint.NpCommon.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace NipponPaint.NpCommon.Database.Sql.NpMain
{
    public static class Cans
    {
        #region 参照系

        #region 注文番号による一覧データ取得
        /// <summary>
        /// 注文番号による一覧データ取得
        /// </summary>
        /// <param name="viewSettings"></param>
        /// <returns></returns>
        public static string GetPreviewByOrderId(List<GridViewSetting> viewSettings, string plant)
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
                sql.Append($"{item.SqlSentence}");
                cnt++;
            }
            sql.Append($"FROM {SelectCans(plant)} ");
            sql.Append($"WHERE C.Order_id = @orderId ");
            sql.Append($"ORDER BY ");
            sql.Append($"  C.Can_Number ");
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
            sql.Append($"SELECT C.Cans_id, C.Barcode, C.Order_id, C.White_Code AS Code, 0 As Row_Index, C.White_Weight AS Weight, C.White_Dispensed AS Dispensed FROM {SelectCans(plant)}  ");
            for (var cnt = 1; cnt < 20; cnt++)
            {
                sql.Append($"UNION ALL SELECT C.Cans_id, C.Barcode, C.Order_id, C.Colorant_{cnt} AS Code, {cnt} As Row_Index, C.Weight_{cnt} AS Weight, C.Dispensed_{cnt} AS Dispensed FROM {SelectCans(plant)} ");
            }
            return sql.ToString();
        }
        #endregion

        #region バーコードで調合状況を取得
        /// <summary>
        /// バーコードで調合状況を取得
        /// </summary>
        /// <returns></returns>
        public static string GetPreviewDispensedData(string plant)
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT ");
            sql.Append($"  TB0.Barcode ");
            sql.Append($" ,TB0.Code ");
            sql.Append($" ,TB0.Weight ");
            sql.Append($" ,TB0.Dispensed ");
            sql.Append($" ,TB1.Errors_1 ");
            sql.Append($" ,TB1.Errors_2 ");
            sql.Append($" ,TB1.Errors_3 ");
            sql.Append($" ,TB2.Mixing_Time ");
            sql.Append($" ,TB2.Mixing_Speed ");
            sql.Append($" ,TB2.Cap_Type ");
            sql.Append($" ,TB1.Can_Number ");
            sql.Append($"FROM ({GetPreviewDispensed(plant)}) AS TB0 ");
            sql.Append($"INNER JOIN Cans   AS TB1 ON TB1.Cans_id  = TB0.Cans_id ");
            sql.Append($"INNER JOIN Orders AS TB2 ON TB2.Order_id = TB0.Order_id ");
            sql.Append($"WHERE TB0.Barcode = @barcode");
            sql.Append($"ORDER BY ");
            sql.Append($"  TB0.Order_id ");
            sql.Append($" ,TB0.Barcode ");
            return sql.ToString();
        }
        #endregion
        #region バーコードで調合状況を取得
        /// <summary>
        /// バーコードで調合状況を取得
        /// </summary>
        /// <returns></returns>
        public static string GetPreviewDispensedData2(List<GridViewSetting> viewSettings, string plant)
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
                sql.Append($"{item.SqlSentence}");
                cnt++;
            }
            sql.Append($"FROM ({GetPreviewDispensed(plant)}) AS TB0 ");
            //sql.Append($"INNER JOIN Cans   AS TB1 ON TB1.Cans_id  = TB0.Cans_id ");
            sql.Append($"INNER JOIN ({Orders.GetPreviewDispensed(plant)}) AS TB2 ON TB2.Order_id = TB0.Order_id AND TB2.Code = TB0.Code AND TB0.Row_Index = TB2.Row_Index ");
            sql.Append($"WHERE TB0.Barcode = @barcode ");
            sql.Append($"ORDER BY ");
            sql.Append($"  TB0.Order_id ");
            sql.Append($" ,TB0.Barcode ");
            sql.Append($" ,TB0.Row_Index ");
            return sql.ToString();
        }
        #endregion

        #region Cansテーブルをplantで抽出
        /// <summary>
        /// Cansテーブルをplantで抽出
        /// 単体では使用せず、FROM句のあとに呼び出す
        /// </summary>
        /// <param name="plant"></param>
        /// <returns></returns>
        private static string SelectCans(string plant)
        {
            var sql = new StringBuilder();
            sql.Append($"Cans AS C ");
            sql.Append($"INNER JOIN Orders AS O ON O.Order_id = C.Order_id ");
            sql.Append($"INNER JOIN (SELECT SS_Code FROM Plants WHERE REPLACE(Plant_Description, ' ', '') = '{plant}')  AS P ON P.SS_Code = O.HG_SS_Code ");
            return sql.ToString();
        }
        #endregion

        #region バーコードによるレコード取得
        public static string GetDetailByBarcode()
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT ");
            sql.Append($" * ");
            sql.Append($"FROM Cans AS C ");
            sql.Append($"LEFT JOIN Orders AS O ON O.Order_id = C.Order_id ");
            sql.Append($"WHERE Barcode = @barcode");
            return sql.ToString();
        }
        #endregion

        #region 色コード、吐出量、重量を初期化する
        /// <summary>
        /// 色コード、吐出量、重量を初期化する
        /// </summary>
        /// <returns></returns>
        public static string RemanufacturedCanByBarcode(string value)
        {
            var sql = new StringBuilder();
            sql.Append($"UPDATE ");
            sql.Append($"Cans ");
            sql.Append($"SET ");
            sql.Append($"DB = 0 ");
            sql.Append($",Formula_Release = 0 ");
            sql.Append($",White_Code = '' ");
            sql.Append($",White_Weight = 0 ");
            sql.Append($",White_Dispensed = 0 ");
            sql.Append($",Colorant_1 = '' ");
            sql.Append($",Weight_1 = 0 ");
            sql.Append($",Dispensed_1 = 0 ");
            sql.Append($",Colorant_2 = '' ");
            sql.Append($",Weight_2 = 0 ");
            sql.Append($",Dispensed_2 = 0 ");
            sql.Append($",Colorant_3 = '' ");
            sql.Append($",Weight_3 = 0 ");
            sql.Append($",Dispensed_3 = 0 ");
            sql.Append($",Colorant_4 = '' ");
            sql.Append($",Weight_4 = 0 ");
            sql.Append($",Dispensed_4 = 0 ");
            sql.Append($",Colorant_5 = '' ");
            sql.Append($",Weight_5 = 0 ");
            sql.Append($",Dispensed_5 = 0 ");
            sql.Append($",Colorant_6 = '' ");
            sql.Append($",Weight_6 = 0 ");
            sql.Append($",Dispensed_6 = 0 ");
            sql.Append($",Colorant_7 = '' ");
            sql.Append($",Weight_7 = 0 ");
            sql.Append($",Dispensed_7 = 0 ");
            sql.Append($",Colorant_8 = '' ");
            sql.Append($",Weight_8 = 0 ");
            sql.Append($",Dispensed_8 = 0 ");
            sql.Append($",Colorant_9 = '' ");
            sql.Append($",Weight_9 = 0 ");
            sql.Append($",Dispensed_9 = 0 ");
            sql.Append($",Colorant_10 = '' ");
            sql.Append($",Weight_10 = 0 ");
            sql.Append($",Dispensed_10 = 0 ");
            sql.Append($",Colorant_11 = '' ");
            sql.Append($",Weight_11= 0 ");
            sql.Append($",Dispensed_11 = 0 ");
            sql.Append($",Colorant_12 = '' ");
            sql.Append($",Weight_12 = 0 ");
            sql.Append($",Dispensed_12 = 0 ");
            sql.Append($",Colorant_13 = '' ");
            sql.Append($",Weight_13 = 0 ");
            sql.Append($",Dispensed_13 = 0 ");
            sql.Append($",Colorant_14 = '' ");
            sql.Append($",Weight_14 = 0 ");
            sql.Append($",Dispensed_14 = 0 ");
            sql.Append($",Colorant_15 = '' ");
            sql.Append($",Weight_15 = 0 ");
            sql.Append($",Dispensed_15 = 0 ");
            sql.Append($",Colorant_16 = '' ");
            sql.Append($",Weight_16 = 0 ");
            sql.Append($",Dispensed_16 = 0 ");
            sql.Append($",Colorant_17 = '' ");
            sql.Append($",Weight_17 = 0 ");
            sql.Append($",Dispensed_17 = 0 ");
            sql.Append($",Colorant_18 = '' ");
            sql.Append($",Weight_18 = 0 ");
            sql.Append($",Dispensed_18 = 0 ");
            sql.Append($",Colorant_19 = '' ");
            sql.Append($",Weight_19 = 0 ");
            sql.Append($",Dispensed_19 = 0 ");
            sql.Append($",Total_Weight = 0 ");
            sql.Append($",Overfilling = 0 ");
            sql.Append($",IN_Weight = 0 ");
            sql.Append($",OUT_Weight = 0 ");
            sql.Append($",Target_Weight = 0 ");
            sql.Append($",Errors_1 = 0 ");
            sql.Append($",Errors_2 = 0 ");
            sql.Append($",Errors_3 = 0 ");
            sql.Append($",Error_Mask_1 = 0 ");
            sql.Append($",Error_Mask_2 = 0 ");
            sql.Append($",Error_Mask_3 = 0 ");
            sql.Append($"WHERE ");
            sql.Append($"Barcode IN ({value})");

            return sql.ToString();
        }
        #endregion

        #endregion
    }
}