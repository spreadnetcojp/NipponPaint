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
    public static class MainSet
    {
        #region 定数
        //テーブル
        public const string MAIN_TABLE = "Main_Set";
        //カラム
        public const string COLUMN_MAIN_SET_ID = "Main_Set_Id";
        public const string COLUMN_COLOR_STATION_AREA_CODE = "Color_Station_Area_Code";
        public const string COLUMN_TRK_CURRENT_FIRST = "TRK_Current_First";
        public const string COLUMN_TRK_CURRENT_LAST = "TRK_Current_Last";
        public const string COLUMN_TRK_LATEST_USED = "TRK_Latest_Used";
        public const string COLUMN_TRK_NEXT_FIRST = "TRK_Next_First";
        public const string COLUMN_TRK_NEXT_LAST = "TRK_Next_Last";
        public const string COLUMN_TRK_DEPARTURE_CODE = "TRK_Departure_Code";
        public const string COLUMN_TRK_DEPARTURE_NAME = "TRK_Departure_Name";
        public const string COLUMN_FTP_DELAY_CHECK = "FTP_Delay_Check";
        public const string COLUMN_AUTO_CLOSE_ORDERS = "Auto_Close_Orders";
        public const string COLUMN_ARCHIVE_PERMANENCE = "Archive_Permanence";
        public const string COLUMN_CSV_PATH = "CSV_Path";
        public const string COLUMN_CSV_DAY = "CSV_Day";
        public const string COLUMN_CSV_TIME = "CSV_Time";
        public const string COLUMN_BCK_DAILY_TIME = "BCK_Daily_Time";
        public const string COLUMN_BCK_WEEKLY_TIME = "BCK_Weekly_Time";
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
