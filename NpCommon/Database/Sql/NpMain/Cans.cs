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
                sql.Append(item.SqlSentence);
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
            sql.Append($"SELECT C.Cans_id, C.Barcode, C.Order_id, C.White_Code AS Code, 0 As Row_Index, C.White_Weight AS Weight, C.White_Dispensed AS Dispensed FROM {SelectCans(plant)} WHERE C.White_Code  <> '' ");
            for (var cnt = 1; cnt < 20; cnt++)
            {
                sql.Append($"UNION ALL SELECT C.Cans_id, C.Barcode, C.Order_id, C.Colorant_{cnt} AS Code, {cnt} As Row_Index, C.Weight_{cnt} AS Weight, C.Dispensed_{cnt} AS Dispensed FROM {SelectCans(plant)} WHERE C.Colorant_{cnt} <> '' ");
            }
            return sql.ToString();
        }
        #endregion

        #region バーコードで調合状況を取得
        /// <summary>
        /// バーコードで調合状況を取得
        /// </summary>
        /// <returns></returns>
        public static string GetPreviewDispensedByBarcode(string barCode, string plant)
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT ");
            sql.Append($"  TB0.Code ");
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
            sql.Append($"WHERE TB0.Barcode = '{barCode}' ");
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

    }
}