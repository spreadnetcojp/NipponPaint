//*****************************************************************************
//
//  システム名：調色工場用自動計量システム NpCommon
//
//  Copyright 三菱電機エンジニアリング株式会社 2022 All rights reserved.
//
//-----------------------------------------------------------------------------
//  変更履歴:
//  Ver      日付        担当       コメント
//  0.0      2022/04/30  M.Nakai    新規作成
#region 更新履歴
#endregion
//*****************************************************************************

#region using defines
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace NipponPaint.NpCommon.Database.Sql.NpMain
{
    public static class CansArchive
    {
        #region 定数
        // テーブル
        public const string MAIN_TABLE = "Cans_Archive";
        // カラム
        public const string COLUMN_ORDER_ID = "Order_id";
        #endregion

        #region 参照系
        #endregion

        #region 更新系
        public static string DeleteArchive()
        {
            var sql = new StringBuilder();
            sql.Append($"DELETE ");
            sql.Append($"C ");
            sql.Append($"FROM {MAIN_TABLE} AS C ");
            sql.Append($"INNER JOIN ");
            sql.Append($" (SELECT  ");
            sql.Append($"   {OrderArchive.COLUMN_ORDER_ID} ");
            sql.Append($"  FROM {OrderArchive.MAIN_TABLE} ");
            sql.Append($"  WHERE {OrderArchive.COLUMN_PRODUCTION_DATE} < @productionDate) ");
            sql.Append($"AS O ON O.{OrderArchive.COLUMN_ORDER_ID} = C.{COLUMN_ORDER_ID} ");
            return sql.ToString();
        }
            #endregion
    }
}
