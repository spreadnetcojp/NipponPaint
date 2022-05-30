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
