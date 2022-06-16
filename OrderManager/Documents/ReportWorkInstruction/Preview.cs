using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace NipponPaint.OrderManager.Documents.ReportWorkInstruction
{
    public partial class Preview : Form
    {
        public Preview()
        {
            InitializeComponent();
            var parameters = new List<ReportParameter> {
                new ReportParameter("TitleValue", "調色作業指示書(控え)"),
                new ReportParameter("ReadValue", "(既読済)"),
                new ReportParameter("CodeValue", "JZ"),
            };
            parameters.AddRange(GetReportParameter("HgSsShippingDate", "製造日", DateTime.Now.Month.ToString() + "月" + DateTime.Now.Day.ToString() + "日"));
            parameters.AddRange(GetReportParameter("HgOrderInvoiceId", "伝区", "1B"));
            parameters.AddRange(GetReportParameter("HgDataNumber", "処理NO", "147734449"));
            parameters.AddRange(GetReportParameter("SlipDate", "伝票発行日", "06/13 15:56"));
            parameters.AddRange(GetReportParameter("HgTintingPriceRank", "発注時ランク", "A"));
            parameters.AddRange(GetReportParameter("OperatorName", "製造者:", "山本虎"));
            parameters.AddRange(GetReportParameter("InputTime", "投入日時:", "2020　年　1　月　10　日　16:16"));
            parameters.AddRange(GetReportParameter("HgSalesInCharge", "投入者:", "田口めぐみ"));
            parameters.AddRange(GetReportParameter("HgProductNo", "商品コード", "1061113", "VCS13"));
            parameters.AddRange(GetReportParameter("HgProductNameonly", "商品名（品種/色名）", "N　DANシリコンセラR", "全艶消　NDー110"));
            parameters.AddRange(GetReportParameter("HgVolumeCode", "容量", "15K"));
            parameters.AddRange(GetReportParameter("HgOrderCans", "数量", "3"));
            parameters.AddRange(GetReportParameter("HgGlossDictation", "艶区分", "艶消"));
            parameters.AddRange(GetReportParameter("HgSupplementDictation", "調色機能", "2"));
            parameters.AddRange(GetReportParameter("HgColorSample", "標準見本", "ND:一任"));
            parameters.AddRange(GetReportParameter("HgSamplePlates", "塗板添付", "3"));
            parameters.AddRange(GetReportParameter("HgNote", "調色摘要欄", "1/14出荷"));
            parameters.AddRange(GetReportParameter("HgComments", "摘要欄", "1/15必着　EDI"));
            parameters.AddRange(GetReportParameter("HgSampleBack", "見本返却欄", ""));
            parameters.AddRange(GetReportParameter("HgTintingDirection", "指定ロット", ""));
            parameters.AddRange(GetReportParameter("HgSalesBranchName", "営業所名", "神戸　営業所"));
            parameters.AddRange(GetReportParameter("HgCustomerCode", "得意先コード", "550021"));
            parameters.AddRange(GetReportParameter("HgCustomerNameKanji", "得意先名", "井原塗料（株）本社"));
            parameters.AddRange(GetReportParameter("HgSsCode", "SSコード", "51F"));
            parameters.AddRange(GetReportParameter("HgTruckCompanyName", "配送業者名", "セントラル"));
            parameters.AddRange(GetReportParameter("HgHgShippingId", "HG運区", "区域2"));
            parameters.AddRange(GetReportParameter("HgDeliveryNameKanji", "納入先名/納入先住所", "井原塗料（神戸）", "兵庫県神戸市兵庫区鍛冶屋町　1-3-15"));
            parameters.AddRange(GetReportParameter("HgDeliveryTelno", "TEL/納期", "078-651-8901", "1月　15日"));
            this.Viewer.LocalReport.SetParameters(parameters);
        }

        private void Preview_Load(object sender, EventArgs e)
        {

            this.Viewer.RefreshReport();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propartyName"></param>
        /// <param name="titleText"></param>
        /// <param name="valueText"></param>
        /// <returns></returns>
        private List<ReportParameter> GetReportParameter(string propartyName, 
                                                         string titleText, 
                                                         string valueText)
        {
            var parameters = new List<ReportParameter> {
                new ReportParameter($"{propartyName}Title", titleText),
                new ReportParameter($"{propartyName}Value", valueText),
            };
            return parameters;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="propartyName"></param>
        /// <param name="titleText"></param>
        /// <param name="valueTextUpper"></param>
        /// <param name="valueTextLower"></param>
        /// <returns></returns>
        private List<ReportParameter> GetReportParameter(string propartyName,
                                                         string titleText,
                                                         string valueTextUpper,
                                                         string valueTextLower)
        {
            var parameters = new List<ReportParameter> {
                new ReportParameter($"{propartyName}Title", titleText),
                new ReportParameter($"{propartyName}ValueUpper", valueTextUpper),
                new ReportParameter($"{propartyName}ValueLower", valueTextLower),
            };
            return parameters;
        }
    }
}
