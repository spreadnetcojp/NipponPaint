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
    public static class PlantsSettings
    {
        #region 定数
        //テーブル
        public const string MAIN_TABLE = "Plants_Settings";
        //カラム
        public const string COLUMN_ID = "id";
        public const string COLUMN_FTP_SERVER = "FTP_Server";
        public const string COLUMN_FTP_PORT = "FTP_Port";
        public const string COLUMN_FTP_USER = "FTP_User";
        public const string COLUMN_FTP_PASSWORD = "FTP_Password";
        public const string COLUMN_FTP_DIR_RECEIVE = "FTP_Dir_Receive";
        public const string COLUMN_FTP_DIR_SEND = "FTP_Dir_Send";
        public const string COLUMN_FTP_DIR_NOTIFY = "FTP_Dir_Notify";
        public const string COLUMN_ENABLED = "Enabled";
        public const string COLUMN_TRK_SHIPPER_CODE = "TRK_Shipper_Code";
        public const string COLUMN_TRK_SHIPPER_ADDRESS_1 = "TRK_Shipper_Address_1";
        public const string COLUMN_TRK_SHIPPER_ADDRESS_2 = "TRK_Shipper_Address_2";
        public const string COLUMN_TRK_SHIPPER_COMPANY = "TRK_Shipper_Company";
        public const string COLUMN_TRK_SHIPPER_SUBSIDIARY = "TRK_Shipper_Subsidiary";
        public const string COLUMN_TRK_TELEPHONE_NUMBER = "TRK_Telephone_Number";
        #endregion

        #region 参照系
        #region 一覧データ取得
        /// <summary>
        /// 一覧データ取得
        /// </summary>
        /// <returns></returns>
        public static string GetPreview()
        {
            var sql = new StringBuilder();
            sql.Append($"select　");
            sql.Append($"*　");;
            sql.Append($"from {MAIN_TABLE} ");
            return sql.ToString();
        }
        #endregion
        #endregion

        #region 更新系
        #endregion
    }
}
