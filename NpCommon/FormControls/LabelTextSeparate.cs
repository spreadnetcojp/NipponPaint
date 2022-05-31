using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NipponPaint.NpCommon.FormControls
{
    public partial class LabelTextSeparate : UserControl
    {

        public string TitleControlName
        {
            get { return LblTitle.Name; }
            set { LblTitle.Name = value; }
        }

        public string Title
        {
            get { return LblTitle.Text; }
            set { LblTitle.Text = value; }
        }

        public string WordCount
        {
            get { return LblWordCount.Text; }
            set { LblWordCount.Text = value; }
        }

        public Size TitleSize
        {
            get { return PnlTitle.Size; }
            set { PnlTitle.Size = value; }
        }

        public Size DatePanelSize
        {
            get { return PnlData.Size; }
            set { PnlData.Size = new Size(value.Width, value.Height); }
        }
        public BorderStyle PanelBorderStyle
        {
            get { return PnlData.BorderStyle; }
            set { PnlData.BorderStyle = new BorderStyle(); }
        }

        public string DataControlName
        {
            get { return PnlData.Name; }
            set { PnlData.Name = value; }
        }
        public string Lbl1Value
        {
            get { return LblData1.Text; }
            set { LblData1.Text = value; }
        }
        public string Lbl2Value
        {
            get { return LblData2.Text; }
            set { LblData2.Text = value; }
        }
        public Color Label1BackColor
        {
            get { return LblData1.BackColor; }
            set { LblData1.BackColor = value; }
        }

        public Color Label2BackColor

        {
            get { return LblData2.BackColor; }
            set { LblData2.BackColor = value; }
        }

        public Color PanelBackColor
        {
            get { return PnlData.BackColor; }
            set { PnlData.BackColor = value; }
        }

        public BorderStyle TextBorderStyle
        {
            get { return PnlData.BorderStyle; }
            set { PnlData.BorderStyle = value; }
        }


        public Color Label1ForeColor
        {
            get { return LblData1.ForeColor; }
            set { LblData1.ForeColor = value; }
        }

        public Color Label2ForeColor
        {
            get { return LblData2.ForeColor; }
            set { LblData2.ForeColor = value; }
        }

        public string DatabaseColumnName { get; set; } = string.Empty;
        public LabelTextSeparate()
        {
            InitializeComponent();
        }
    }
}
