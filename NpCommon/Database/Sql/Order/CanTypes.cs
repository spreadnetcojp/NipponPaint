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

namespace NipponPaint.NpCommon.Database.Sql.Order
{
    public static class CanTypes
    {
        #region　定数
        // テーブル
        private const string MAIN_TABLE = "Can_types";
        // カラム
        public const string COLUMN_CAN_TYPE = "Cap_Type";
        public const string COLUMN_CAN_DESCRIPTION = "Can_Description";
        public const string COLUMN_HOLE_SIZE = "Hole_Size";
        public const string COLUMN_AVAILABLE_VOLUME = "Available_Volume";
        public const string COLUMN_CAN_WEIGHT = "Can_Weight";
        public const string COLUMN_NOMINAL_VOLUME = "Nominal_Volume";
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
            sql.Append($"ORDER BY {COLUMN_CAN_TYPE} ");
            return sql.ToString();
        }
        #endregion

        #region 詳細データの取得
        /// <summary>
        /// 詳細データの取得
        /// </summary>
        /// <returns></returns>
        public static string GetDetail()
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT * FROM {MAIN_TABLE} WHERE {COLUMN_CAN_TYPE} = @canType");
            return sql.ToString();
        }
        #endregion

        #endregion

        #region 更新系
        #endregion
    }
}
