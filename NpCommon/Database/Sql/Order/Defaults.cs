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
    public static class Defaults
    {
        #region 定数
        // テーブル
        public const string MAIN_TABLE = "Defaults";
        // カラム
        public const string COLUMN_WHITE_CODE = "White_Code";
        public const string COLUMN_CAN_TYPE = "Can_Type";
        public const string COLUMN_CAP_TYPE = "Cap_Type";
        public const string COLUMN_OVERFILLING = "Overfilling";
        public const string COLUMN_PREFILL_AMOUNT = "Prefill_Amount";
        public const string COLUMN_MIXING_TIME = "Mixing_Time";
        public const string COLUMN_MIXING_SPEED = "Mixing_Speed";
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
            sql.Append($"ORDER BY {COLUMN_WHITE_CODE} ");
            return sql.ToString();
        }
        #endregion

        #region 詳細データの取得
        /// <summary>
        /// 詳細データの取得
        /// </summary>
        /// <param name="whiteCode"></param>
        /// <returns></returns>
        public static string GetDetail()
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT ");
            sql.Append(" DF.* ");
            sql.Append($" ,CN.Can_Description ");
            sql.Append($" ,CP.Cap_Description ");
            sql.Append($"FROM {MAIN_TABLE} AS DF ");
            sql.Append($"LEFT JOIN Can_types AS CN ON DF.Can_Type = CN.Can_Type  ");
            sql.Append($"LEFT JOIN Cap_types AS CP ON DF.Cap_Type = CP.Cap_Type  ");
            sql.Append($"WHERE {COLUMN_WHITE_CODE} = @whiteCode ");
            return sql.ToString();





        }
        #endregion


        #endregion

        #region 更新系
        #endregion
    }
}
