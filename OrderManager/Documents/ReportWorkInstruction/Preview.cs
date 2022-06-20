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
        public Preview(ViewModels.DirectionsData vm)

        {
            InitializeComponent();
            var parameters = new List<ReportParameter> {
                new ReportParameter("CopyTitleValue", "調色作業指示書(控え)"),
                new ReportParameter("TitleValue", "調色作業指示書"),
                new ReportParameter("UrgentValue", vm.Urgent ? "(既読済)" : ""),
                new ReportParameter("ProductCodeValue", vm.Product_Code.Trim()),
            };
            parameters.AddRange(GetReportParameter("HgSsShippingDate", "製造日", DateTime.ParseExact(vm.HG_SS_Shipping_Date, "yyyyMMdd", null).Month.ToString().Trim() + "月" + DateTime.ParseExact(vm.HG_SS_Shipping_Date, "yyyyMMdd", null).Day.ToString().Trim() + "日"));
            parameters.AddRange(GetReportParameter("HgOrderInvoiceId", "伝区", vm.HG_Order_Invoice_ID.Trim()));
            parameters.AddRange(GetReportParameter("HgDataNumber", "処理NO", vm.HG_Data_Number.Trim()));
            parameters.AddRange(GetReportParameter("SlipDate", "伝票発行日", DateTime.Now.ToString("MM/dd HH:mm")));
            parameters.AddRange(GetReportParameter("HgTintingPriceRank", "発注時ランク", vm.HG_Tinting_Price_Rank.Trim()));
            parameters.AddRange(GetReportParameter("OperatorName", "製造者:", vm.Operator_Name.Trim()));
            parameters.AddRange(GetReportParameter("HgOrderInputDate", "投入日時:", DateTime.ParseExact(vm.HG_Order_Input_Date, "yyyyMMdd", null).Year.ToString().Trim() + "年" + DateTime.ParseExact(vm.HG_Order_Input_Date, "yyyyMMdd", null).Month.ToString().Trim() + "月" + DateTime.ParseExact(vm.HG_Order_Input_Date, "yyyyMMdd", null).Day.ToString().Trim() + "日", vm.HG_Order_Input_Time.ToString().Trim().Substring(0, 2) + ":" + vm.HG_Order_Input_Time.ToString().Trim().Substring(2, 2)));
            parameters.AddRange(GetReportParameter("HgSalesInCharge", "投入者:", vm.HG_Sales_in_Charge.Trim()));
            parameters.AddRange(GetReportParameter("HgProductNo", "商品コード", vm.HG_Product_No.Trim(), vm.HG_NPC_Article_Number.Trim()));
            parameters.AddRange(GetReportParameter("HgProductNameonly", "商品名（品種/色名）", vm.HG_Product_NameOnly.Trim(), vm.HG_Color_Name.Trim()));
            parameters.AddRange(GetReportParameter("HgVolumeCode", "容量", vm.HG_Volume_Code.Trim()));
            parameters.AddRange(GetReportParameter("HgOrderCans", "数量", vm.HG_Order_Cans.ToString().Trim()));
            parameters.AddRange(GetReportParameter("HgGlossDictation", "艶区分", vm.HG_Gloss_Dictation.Trim()));
            parameters.AddRange(GetReportParameter("HgSupplementDictation", "調色機能", vm.HG_Supplement_Dictation.Trim()));
            parameters.AddRange(GetReportParameter("HgColorSample", "標準見本", vm.HG_Color_Sample.Trim()));
            parameters.AddRange(GetReportParameter("HgSamplePlates", "塗板添付", vm.HG_Sample_Plates.Trim()));
            parameters.AddRange(GetReportParameter("HgNote", "調色摘要欄", vm.HG_Note.Trim()));
            parameters.AddRange(GetReportParameter("HgComments", "摘要欄", vm.HG_Comments.Trim()));
            parameters.AddRange(GetReportParameter("HgSampleBack", "見本返却欄", vm.HG_Sample_Back.Trim()));
            parameters.AddRange(GetReportParameter("HgTintingDirection", "指定ロット", vm.HG_Tinting_Direction.Trim()));
            parameters.AddRange(GetReportParameter("HgSalesBranchName", "営業所名", vm.HG_Sales_Branch_Name.Trim()));
            parameters.AddRange(GetReportParameter("HgCustomerCode", "得意先コード", vm.HG_Customer_Code.Trim()));
            parameters.AddRange(GetReportParameter("HgCustomerNameKanji", "得意先名", vm.HG_Customer_Name_Kanji.Trim()));
            parameters.AddRange(GetReportParameter("HgSsCode", "SSコード", vm.HG_SS_Code.Trim()));
            parameters.AddRange(GetReportParameter("HgTruckCompanyName", "配送業者名", vm.HG_Truck_Company_Name.Trim()));
            parameters.AddRange(GetReportParameter("HgHgShippingId", "HG運区", vm.HG_HG_Shipping_ID.Trim()));
            parameters.AddRange(GetReportParameter("HgDeliveryNameKanji", "納入先名/納入先住所", vm.HG_Delivery_Name_Kanji.Trim(), vm.HG_Delivery_Address_Kanji.Trim()));
            parameters.AddRange(GetReportParameter("HgDeliveryTelno", "TEL/納期", vm.HG_Delivery_TelNo.Trim(), DateTime.ParseExact(vm.HG_Delivery_Date, "yyyyMMdd", null).Month.ToString().Trim() + "月" + DateTime.ParseExact(vm.HG_Delivery_Date, "yyyyMMdd", null).Day.ToString().Trim() + "日"));
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
