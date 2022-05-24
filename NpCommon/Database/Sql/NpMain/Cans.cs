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
        public static string GetPreviewByOrderId(List<GridViewSetting> viewSettings)
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
            sql.Append($"FROM Cans ");
            sql.Append($"WHERE Order_id = @orderId ");
            sql.Append($"ORDER BY ");
            sql.Append($"  Can_Number ");
            return sql.ToString();
        }
        #endregion

        public static string GetPreviewDispensed()
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT Cans_id, Order_id, White_Code  AS Code, 0  As Row_Index, White_Weight AS Weight, White_Dispensed AS Dispensed FROM Cans WHERE White_Code  <> '' ");
            for (var cnt = 1; cnt < 20; cnt++)
            {
                sql.Append($"UNION ALL SELECT Cans_id, Order_id, Colorant_{cnt} AS Code, {cnt} As Row_Index, Weight_{cnt} AS Weight, Dispensed_{cnt} AS Dispensed FROM Cans WHERE Colorant_{cnt} <> '' ");
            }
            return sql.ToString();
        }
    }
}