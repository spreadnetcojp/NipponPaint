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
    public static class ProductNoMaster
    {
        #region 定数
        //テーブル
        public const string MAIN_TABLE = "ProductNo_Master";
        //カラム
        public const string COLUMN_PRD_ID = "PRD_ID";
        public const string COLUMN_PRODUCT_NO = "Product_No";
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
            sql.Append($"ORDER BY {COLUMN_PRD_ID} ");
            return sql.ToString();
        }
        #endregion

        #region 詳細データの取得
        /// <summary>
        /// 詳細データの取得
        /// </summary>
        /// <param name="ProductCode"></param>
        /// <returns></returns>
        public static string GetDetail()
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT * FROM {MAIN_TABLE} WHERE {COLUMN_PRD_ID} = @ProductCode");
            return sql.ToString();
        }
        #endregion

        #region 品名コードによるレコード件数取得
        public static string GetCountByProductNo()
        {
            var sql = new StringBuilder();
            sql.Append($"select COUNT({COLUMN_PRODUCT_NO}) as Product_No_Count FROM {MAIN_TABLE} ");
            sql.Append($"where {COLUMN_PRODUCT_NO} = @ProductNo");
            return sql.ToString();
        }
        #endregion

        #region 更新系
    }
}
#endregion

#endregion