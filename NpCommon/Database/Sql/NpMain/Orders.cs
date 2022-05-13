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
    public static class Orders
    {

        #region 参照系

        #region 一覧データの取得
        /// <summary>
        /// 一覧データの取得
        /// </summary>
        /// <param name="viewSettings"></param>
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
            sql.Append($"FROM Orders ");
            sql.Append($"WHERE HG_SS_Code = '51F' AND Status IN (0, 1, 2, 3, 4) ");
            sql.Append($"ORDER BY ");
            sql.Append($"  Status ");
            sql.Append($" ,HG_HG_Delivery_Code ");
            return sql.ToString();
        }
        #endregion

        #region 一覧データの取得
        /// <summary>
        /// 一覧データの取得
        /// </summary>
        /// <param name="viewSettings"></param>
        /// <returns></returns>
        public static string GetPreviewCloseOrders(List<GridViewSetting> viewSettings, int selectedStatus)
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT ");
            sql.Append($"  Order_id ");
            sql.Append($" ,Product_Code ");
            sql.Append($" ,S.StatusText ");
            //sql.Append($"  CASE O.Status ");
            //sql.Append($"   WHEN 0 THEN '調色担当待ち' ");
            //sql.Append($"   WHEN 1 THEN 'CCM配合待ち' ");
            //sql.Append($"   WHEN 2 THEN '準備完' ");
            //sql.Append($"   WHEN 3 THEN 'テスト缶実施中' ");
            //sql.Append($"   WHEN 4 THEN '製造缶実施中' ");
            //sql.Append($"   ELSE '' ");
            //sql.Append($"  END AS ステータス ");
            sql.Append($" ,Operator_Name ");
            sql.Append($" ,HG_Product_Name ");
            sql.Append($" ,TRIM(TRIM('　' FROM HG_Volume_Code)) ");
            sql.Append($" ,Number_of_cans ");
            sql.Append($" ,Order_Number ");
            sql.Append($"FROM Orders AS O ");
            // 仮実装
            sql.Append($"INNER JOIN EXOPMG..OrderStatus AS S ON O.Status = S.Status ");
            sql.Append($"WHERE HG_SS_Code = '51F' ");
            if (selectedStatus > -1)
            {
                sql.Append($" AND O.Status = {selectedStatus} ");
            }
            else
            {
                sql.Append($" AND O.Status IN(0, 1, 2, 3, 4) ");
            }
            sql.Append($"ORDER BY ");
            sql.Append($"  O.Status ");
            sql.Append($" ,HG_HG_Delivery_Code ");
            return sql.ToString();
        }
        #endregion

        #region オーダーIDによるレコード取得

        /// <summary>
        /// オーダーIDによるレコード取得
        /// </summary>
        /// <returns></returns>
        public static string GetDetailByOrderId()
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT * FROM Orders WHERE order_id = @orderId");
            return sql.ToString();
        }
        #endregion

        #region 処理Noによるレコード取得

        /// <summary>
        /// 処理Noによるレコード取得
        /// </summary>
        /// <returns></returns>
        public static string GetDetailByDataNumber()
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT * FROM Orders WHERE HG_Data_Number = @dataNumber");
            return sql.ToString();
        }
        #endregion

        #endregion

        #region 更新系
        /// <summary>
        /// ステータスを戻す
        /// </summary>
        /// <returns></returns>
        public static string StatusResume()
        {
            var sql = new StringBuilder();
            sql.Append($"UPDATE Orders SET Status = 1 FROM Orders WHERE order_id = @orderId");
            return sql.ToString();
        }

        #endregion
    }
}
