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
        #region 定数
        // テーブル
        public const string MAIN_TABLE = "Cans";
        // カラム
        public const string COLUMN_BARCODE = "Barcode";
        public const string COLUMN_ORDER_ID = "Order_id";
        public const string COLUMN_CAN_NUMBER = "Can_Number";
        public const string COLUMN_STATUS = "Status";
        public const string COLUMN_TEST_CAN = "Test_Can";
        public const string COLUMN_SAMPLE_PRESENT = "Sample_Present";
        public const string COLUMN_FORMULA_RELEASE = "Formula_Release";
        public const string COLUMN_WHITE_CODE = "White_Code";
        public const string COLUMN_WHITE_DISPENSED = "White_Dispensed";
        public const string COLUMN_WHITE_WEIGHT = "White_Weight";
        public const string COLUMN_COLORANT_1 = "Colorant_1";
        public const string COLUMN_DISPENSED_1 = "Dispensed_1";
        public const string COLUMN_COLORANT_2 = "Colorant_2";
        public const string COLUMN_DISPENSED_2 = "Dispensed_2";
        public const string COLUMN_COLORANT_3 = "Colorant_3";
        public const string COLUMN_DISPENSED_3 = "Dispensed_3";
        public const string COLUMN_COLORANT_4 = "Colorant_4";
        public const string COLUMN_DISPENSED_4 = "Dispensed_4";
        public const string COLUMN_COLORANT_5 = "Colorant_5";
        public const string COLUMN_DISPENSED_5 = "Dispensed_5";
        public const string COLUMN_COLORANT_6 = "Colorant_6";
        public const string COLUMN_DISPENSED_6 = "Dispensed_6";
        public const string COLUMN_COLORANT_7 = "Colorant_7";
        public const string COLUMN_DISPENSED_7 = "Dispensed_7";
        public const string COLUMN_COLORANT_8 = "Colorant_8";
        public const string COLUMN_DISPENSED_8 = "Dispensed_8";
        public const string COLUMN_COLORANT_9 = "Colorant_9";
        public const string COLUMN_DISPENSED_9 = "Dispensed_9";
        public const string COLUMN_COLORANT_10 = "Colorant_10";
        public const string COLUMN_DISPENSED_10 = "Dispensed_10";
        public const string COLUMN_COLORANT_11 = "Colorant_11";
        public const string COLUMN_DISPENSED_11 = "Dispensed_11";
        public const string COLUMN_COLORANT_12 = "Colorant_12";
        public const string COLUMN_DISPENSED_12 = "Dispensed_12";
        public const string COLUMN_COLORANT_13 = "Colorant_13";
        public const string COLUMN_DISPENSED_13 = "Dispensed_13";
        public const string COLUMN_COLORANT_14 = "Colorant_14";
        public const string COLUMN_DISPENSED_14 = "Dispensed_14";
        public const string COLUMN_COLORANT_15 = "Colorant_15";
        public const string COLUMN_DISPENSED_15 = "Dispensed_15";
        public const string COLUMN_COLORANT_16 = "Colorant_16";
        public const string COLUMN_DISPENSED_16 = "Dispensed_16";
        public const string COLUMN_COLORANT_17 = "Colorant_17";
        public const string COLUMN_DISPENSED_17 = "Dispensed_17";
        public const string COLUMN_COLORANT_18 = "Colorant_18";
        public const string COLUMN_DISPENSED_18 = "Dispensed_18";
        public const string COLUMN_COLORANT_19 = "Colorant_19";
        public const string COLUMN_DISPENSED_19 = "Dispensed_19";
        public const string COLUMN_CANS_ID = "Cans_Id";
        public const string COLUMN_ORDER_NUMBER = "Order_Number";
        public const string COLUMN_ERRORS_1 = "Errors_1";
        public const string COLUMN_ERRORS_2 = "Errors_2";
        public const string COLUMN_ERRORS_3 = "Errors_3";
        // 別名
        public const string COLUMN_CODE = "Code";
        public const string COLUMN_INDEX = "Row_Index";
        public const string COLUMN_COLORANT = "Colorant";
        public const string COLUMN_WEIGHT = "Weight";
        public const string COLUMN_DISPENSED = "Dispensed";
        public const string COLUMN_ERRORS = "Errors";
        // 色コードが設定されているカラムのリスト
        public static readonly List<string[]> ColorColumns = new List<string[]>()
        {
            new string[]{ COLUMN_WHITE_CODE, COLUMN_WHITE_DISPENSED },
            new string[]{ COLUMN_COLORANT_1, COLUMN_DISPENSED_1 },
            new string[]{ COLUMN_COLORANT_2, COLUMN_DISPENSED_2 },
            new string[]{ COLUMN_COLORANT_3, COLUMN_DISPENSED_3 },
            new string[]{ COLUMN_COLORANT_4, COLUMN_DISPENSED_4 },
            new string[]{ COLUMN_COLORANT_5, COLUMN_DISPENSED_5 },
            new string[]{ COLUMN_COLORANT_6, COLUMN_DISPENSED_6 },
            new string[]{ COLUMN_COLORANT_7, COLUMN_DISPENSED_7 },
            new string[]{ COLUMN_COLORANT_8, COLUMN_DISPENSED_8 },
            new string[]{ COLUMN_COLORANT_9, COLUMN_DISPENSED_9 },
            new string[]{ COLUMN_COLORANT_10, COLUMN_DISPENSED_10 },
            new string[]{ COLUMN_COLORANT_11, COLUMN_DISPENSED_11 },
            new string[]{ COLUMN_COLORANT_12, COLUMN_DISPENSED_12 },
            new string[]{ COLUMN_COLORANT_13, COLUMN_DISPENSED_13 },
            new string[]{ COLUMN_COLORANT_14, COLUMN_DISPENSED_14 },
            new string[]{ COLUMN_COLORANT_15, COLUMN_DISPENSED_15 },
            new string[]{ COLUMN_COLORANT_16, COLUMN_DISPENSED_16 },
            new string[]{ COLUMN_COLORANT_17, COLUMN_DISPENSED_17 },
            new string[]{ COLUMN_COLORANT_18, COLUMN_DISPENSED_18 },
            new string[]{ COLUMN_COLORANT_19, COLUMN_DISPENSED_19 },
        };
        #endregion

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
            sql.Append($"SELECT C.{COLUMN_CANS_ID}, C.{COLUMN_BARCODE}, C.{COLUMN_ORDER_ID}, C.{COLUMN_WHITE_CODE} AS {COLUMN_CODE}, 0 As {COLUMN_INDEX}, C.{COLUMN_WHITE_WEIGHT} AS {COLUMN_WEIGHT}, C.{COLUMN_WHITE_DISPENSED} AS {COLUMN_DISPENSED} FROM {SelectCans(plant)}  ");
            for (var cnt = 1; cnt < 20; cnt++)
            {
                sql.Append($"UNION ALL SELECT C.{COLUMN_CANS_ID}, C.{COLUMN_BARCODE}, C.{COLUMN_ORDER_ID}, C.{COLUMN_COLORANT}_{cnt} AS {COLUMN_CODE}, {cnt} As {COLUMN_INDEX}, C.{COLUMN_WEIGHT}_{cnt} AS {COLUMN_WEIGHT}, C.{COLUMN_DISPENSED}_{cnt} AS {COLUMN_DISPENSED} FROM {SelectCans(plant)} ");
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
            sql.Append($"  TB0.{COLUMN_BARCODE} ");
            sql.Append($" ,TB0.{COLUMN_CODE} ");
            sql.Append($" ,TB0.{COLUMN_WEIGHT} ");
            sql.Append($" ,TB0.{COLUMN_DISPENSED} ");
            sql.Append($" ,TB1.{COLUMN_ERRORS_1} ");
            sql.Append($" ,TB1.{COLUMN_ERRORS_2} ");
            sql.Append($" ,TB1.{COLUMN_ERRORS_3} ");
            sql.Append($" ,TB2.{Orders.COLUMN_MIXING_TIME} ");
            sql.Append($" ,TB2.{Orders.COLUMN_MIXING_SPEED} ");
            sql.Append($" ,TB2.{Orders.COLUMN_INPUT_CAN} ");
            sql.Append($" ,TB2.{Orders.COLUMN_P_WEIGHT_TOLERANCE} ");
            sql.Append($" ,TB2.{Orders.COLUMN_CAN_WEIGHT} ");
            sql.Append($" ,TB2.{Orders.COLUMN_HG_WEIGHT} ");
            sql.Append($" ,TB1.{COLUMN_CAN_NUMBER} ");
            sql.Append($" ,TB1.{COLUMN_TEST_CAN} ");
            sql.Append($" ,ISNULL(TB1.{COLUMN_ERRORS_1}, 0) + ISNULL(TB1.{COLUMN_ERRORS_2}, 0) + ISNULL(TB1.{COLUMN_ERRORS_3}, 0) AS {COLUMN_ERRORS} ");
            sql.Append($"FROM ({GetPreviewDispensed(plant)}) AS TB0 ");
            sql.Append($"INNER JOIN {MAIN_TABLE}             AS TB1 ON TB1.{COLUMN_CANS_ID}  = TB0.{COLUMN_CANS_ID} ");
            sql.Append($"INNER JOIN {Orders.MAIN_TABLE}      AS TB2 ON TB2.{Orders.COLUMN_ORDER_ID} = TB0.{COLUMN_ORDER_ID} ");
            sql.Append($"WHERE TB0.{COLUMN_BARCODE} = @barcode ");
            sql.Append($"ORDER BY ");
            sql.Append($"  TB0.{COLUMN_ORDER_ID} ");
            sql.Append($" ,TB0.{COLUMN_BARCODE} ");
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
            sql.Append($"{MAIN_TABLE} AS C ");
            sql.Append($"INNER JOIN {Orders.MAIN_TABLE} AS O ON O.{Orders.COLUMN_ORDER_ID} = C.{COLUMN_ORDER_ID} ");
            sql.Append($"INNER JOIN (SELECT {Plants.COLUMN_SS_CODE} FROM {Plants.MAIN_TABLE} WHERE REPLACE({Plants.COLUMN_PLANT_DESCRIPTION}, ' ', '') = '{plant}')  AS P ON P.{Plants.COLUMN_SS_CODE} = O.{Orders.COLUMN_HG_SS_CODE} ");
            return sql.ToString();
        }
        #endregion

        #region バーコードによるレコード取得
        public static string GetDetailByBarcode()
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT ");
            sql.Append($" * ");
            sql.Append($"FROM {MAIN_TABLE} AS C ");
            sql.Append($"LEFT JOIN {Orders.MAIN_TABLE} AS O ON O.{Orders.COLUMN_ORDER_ID} = C.{COLUMN_ORDER_ID} ");
            sql.Append($"WHERE {COLUMN_BARCODE} = @barcode");
            return sql.ToString();
        }
        #endregion

        /// <summary>
        /// オーダー番号による缶データの習得
        /// </summary>
        /// <param name="plant"></param>
        /// <returns></returns>
        public static string GetCansFormulaRelease(string plant)
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT ");
            sql.Append($"  C.{COLUMN_FORMULA_RELEASE} ");
            sql.Append($" ,C.{COLUMN_ORDER_ID} ");
            sql.Append($" ,O.{Orders.COLUMN_STATUS} ");
            sql.Append($"FROM {SelectCans(plant)} ");
            sql.Append($"WHERE C.{COLUMN_ORDER_NUMBER} = @OrderNumber ");
            return sql.ToString();
        }

        /// <summary>
        /// オーダーIDによるバーコードの習得
        /// </summary>
        /// <param name="plant"></param>
        /// <returns></returns>
        public static string GetBarcodes(string plant)
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT ");
            sql.Append($" C.{COLUMN_BARCODE} ");
            sql.Append($"FROM {SelectCans(plant)} ");
            sql.Append($"WHERE C.{COLUMN_ORDER_ID} = @OrderId ");
            return sql.ToString();
        }

        #endregion

        #region 更新系

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

        public static string SetDispensedFromSupervisor(List<string> updateItems)
        {
            var sql = new StringBuilder();
            sql.Append($"UPDATE Cans SET {string.Join(" ,", updateItems)} ");
            sql.Append($"WHERE Barcode = @barcode");
            return sql.ToString();
        }

        #endregion

    }
}