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
    public static class Operators
    {
        #region 定数
        //テーブル
        public const string MAIN_TABLE = "Operators";
        //カラム
        public const string COLUMN_OPERATORS_ID = "Operators_Id";
        public const string COLUMN_OPERATOR_NAME = "Operator_Name";
        public const string COLUMN_OPERATOR_CODE = "Operator_Code";
        #endregion

        #region 参照系

        #region 一覧データの取得(FrmMixingColorsOperators)
        /// <summary>
        /// 一覧データの取得(FrmMixingColorsOperators)
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

        #region 一覧データの取得(FrmOperators)
        /// <summary>
        /// 一覧データの取得(FrmOperators)
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static string GetPreviewOperators(List<GridViewSetting> viewSettings)
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
            sql.Append($"WHERE {COLUMN_OPERATOR_NAME} <> '' ");　　　　　//空白の担当者の欄は表示させない
            sql.Append($"ORDER BY {COLUMN_OPERATORS_ID} ");
            return sql.ToString();
        }
        #endregion

        #region 詳細データの取得(FrmOperators)
        /// <summary>
        /// 詳細データの取得(FrmOperators)
        /// </summary>
        /// <returns></returns>
        public static string GetDetail()
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT * FROM {MAIN_TABLE} WHERE {COLUMN_OPERATORS_ID} = @operatorsId");
            return sql.ToString();
        }
        #endregion

        #endregion

        #region 更新系
        #endregion


    }
}
