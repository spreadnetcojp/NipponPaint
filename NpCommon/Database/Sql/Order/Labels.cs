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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NipponPaint.NpCommon.Database.Sql.Order
{
    public static class Labels
    {
        #region 定数
        // テーブル
        public const string MAIN_TABLE = "Labels";
        // カラム
        public const string COLUMN_LABEL_TYPE = "Label_Type";
        public const string COLUMN_LABLE_DESCRIPTION = "Label_Description";
        public const string COLUMN_JIS_LOGO = "JIS_Logo";
        public const string COLUMN_FLA_LOGO = "FLA_Logo";
        public const string COLUMN_TOX_LOGO = "TOX_Logo";
        public const string COLUMN_NPC_LOGO = "NPC_Logo";
        #endregion

        #region 参照系
        #endregion

        #region 更新系
        #endregion
    }
}
