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
                new ReportParameter("ProductionDateTitle", "製造日"),
                new ReportParameter("ProductionDateValue", DateTime.Now.ToString()),
            };
            this.Viewer.LocalReport.SetParameters(parameters);
        }

        private void Preview_Load(object sender, EventArgs e)
        {

            this.Viewer.RefreshReport();
        }
    }
}
