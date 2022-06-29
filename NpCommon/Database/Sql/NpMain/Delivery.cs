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
using System.Text;
using NipponPaint.NpCommon.Settings;
#endregion

namespace NipponPaint.NpCommon.Database.Sql.NpMain
{
    public static class Delivery
    {
        #region 定数
        //テーブル
        public const string MAIN_TABLE = "Delivery";
        //カラム
        public const string COLUMN_HG_DELIVERY_KANJI = "HG_Delivery_Kanji";
        public const string COLUMN_DEFAULT_DELIVERY_TIME = "Default_Delivery_Time";
        public const string COLUMN_SORT_ORDER = "Sort_Order";
        public const string COLUMN_MUST_NOTIFY = "Must_Notify";
        public const string COLUMN_HG_DELIVERY_CODE = "HG_Delivery_Code";
        #endregion

        #region 参照系
        #region 一覧データの取得
        /// <summary>
        /// 一覧データの取得
        /// </summary>
        /// <returns></returns>
        public static string GetPreview()
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT ");
            sql.Append($"* ");
            sql.Append($"FROM {MAIN_TABLE} ");
            return sql.ToString();
        }
        #endregion
        #endregion

        #region 更新系
        #endregion
    }
}
