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
    public static  class OrderArchive
    {
        #region 定数
        // テーブル
        public const string MAIN_TABLE = "Orders_Archive";
        // カラム
        public const string COLUMN_ORDER_ID = "Order_id";
        public const string COLUMN_PRODUCTION_DATE = "Production_Date";
        #endregion

        #region 参照系
        #endregion

        #region 更新系
        public static string DeleteArchive()
        {
            var sql = new StringBuilder();
            sql.Append($"DELETE ");
            sql.Append($"FROM {MAIN_TABLE} ");
            sql.Append($"WHERE {COLUMN_PRODUCTION_DATE} < @productionDate ");
            return sql.ToString();
        }
        #endregion
    }
}
