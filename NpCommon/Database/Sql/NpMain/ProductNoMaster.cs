﻿//*****************************************************************************
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
            sql.Append($"FROM ProductNo_Master ");
            sql.Append($"ORDER BY PRD_ID ");
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
            sql.Append($"SELECT * FROM ProductNo_Master WHERE PRD_ID = @ProductCode");
            return sql.ToString();
        }
        #endregion

        #region 品名コードによるレコード件数取得
        public static string GetCountByProductNo()
        {
            var sql = new StringBuilder();
            sql.Append($"select COUNT(Product_No) as Product_No_Count FROM ProductNo_Master ");
            sql.Append($"where Product_No = @ProductNo");
            return sql.ToString();
        }
        #endregion

        #region 更新系
    }
}
#endregion

#endregion