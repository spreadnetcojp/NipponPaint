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
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NipponPaint.OrderManager.ViewModels
{
    /// <summary>
    /// ビューモデル
    /// 商品コードマスタダイアログ
    /// </summary>
    public class RemanufacturedCanData
    {
        /// <summary>
        /// GvBarcodeのデータソース
        /// </summary>
        public DataTable DataSource { get; set; }
        /// <summary>
        /// GvBarcodeの選択行
        /// </summary>
        public int SelectedIndex { get; set; }

        public RemanufacturedCanData()
        {

        }
    }
}
