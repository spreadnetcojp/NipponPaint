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
    public static class Plants
    {
        #region 定数
        //テーブル
        public const string MAIN_TABLE = "Plants";
        //カラム
        public const string COLUMN_PLANT_ID = "Plant_ID";
        public const string COLUMN_PLANT_DESCRIPTION = "Plant_Description";
        public const string COLUMN_DAILY_CAPABILITY = "Daily_Capability";
        public const string COLUMN_DAILY_WORKING_TIME = "Daily_Working_Time";
        public const string COLUMN_START_WORKING_TIME = "Start_Working_Time";
        public const string COLUMN_SS_CODE = "SS_Code";
        #endregion

        #region 参照系
        #region 一覧データの取得
        /// <summary>
        /// 一覧データの取得
        /// </summary>
        /// <param name=""></param>
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
