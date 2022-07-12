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

#region using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace NipponPaint.NpCommon.Database.Sql.IOSSUP
{
    public static class Raw
    {
        #region 定数
        // データベース
        private const string DATABASE_IOSSUP = "IOSSUP_";
        // テーブル
        private const string MAIN_TABLE = "Raw";
        // カラム
        private const string COLUMN_PRODUCT_ID = "PRODUCT_ID";
        public const string COLUMN_PRODUCT_NAME = "PRODUCT_NAME";
        private const string COLUMN_PRODUCT_DESCRIPTION = "PRODUCT_DESCRIPTION";
        private const string COLUMN_SPECIFIC_GRAVITY = "SPECIFIC_GRAVITY";
        #endregion

        #region 参照系

        #region
        public static string GetWhiteCode(string plant)
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT {COLUMN_PRODUCT_NAME} ");
            sql.Append($"FROM {DATABASE_IOSSUP}{plant}.dbo.{MAIN_TABLE}");
            return sql.ToString();
        }
        #endregion

        #endregion

        #region 更新系

        #endregion
    }
}
