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

using NipponPaint.NpCommon.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NipponPaint.NpCommon.Database.Sql.NpMain
{
    public static class HgLog
    {
        #region 定数
        //テーブル
        public const string MAIN_TABLE = "HG_Log";
        //カラム
        public const string COLUMN_HG_LOG_ID = "HG_Log_Id";
        public const string COLUMN_NOTIFY_MSG = "Notify_Msg";
        public const string COLUMN_RECEIVE_TIME = "Receive_Time";
        public const string COLUMN_REJECTED = "Rejected";
        public const string COLUMN_ACKNOWLEDGED = "Acknowledged";
        #endregion

        #region 参照系

        #region 一覧データの取得
        /// <summary>
        /// 一覧データの取得
        /// </summary>
        /// <returns></returns>
        public static string GetPreview(List<GridViewSetting> viewSettings)
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
            sql.Append($"FROM {MAIN_TABLE} ");
            sql.Append($"ORDER BY {COLUMN_HG_LOG_ID} DESC ");
            return sql.ToString();
        }
        #endregion

        #endregion

        #region 更新系

        #region 「認識」ボタン押下でのAcknowledged更新
        /// <summary>
        /// 「認識」ボタン押下でのAcknowledged更新
        /// </summary>
        /// <returns></returns>
        public static string Acknowledged()
        {
            var sql = new StringBuilder();
            sql.Append($"UPDATE ");
            sql.Append($"{MAIN_TABLE} ");
            sql.Append($"SET ");
            sql.Append($"{COLUMN_ACKNOWLEDGED} = @acknowledged1 ");
            sql.Append($"WHERE {COLUMN_ACKNOWLEDGED} = @acknowledged2 ");
            return sql.ToString();
        }
        #endregion

        #region 「Clear」ボタンで全てのログを削除
        public static string LogDelete()
        {
            var sql = new StringBuilder();
            sql.Append($"DELETE ");
            sql.Append($"FROM {MAIN_TABLE} ");
            return sql.ToString();
        }
        #endregion

        #endregion


    }
}
