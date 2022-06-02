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
        #region 定数
        private const int COLOR_NAME_LOWER_LIMIT = 0;
        private const int COLOR_NAME_HIGH_LIMIT = 14;
        #endregion

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
        public string Value { get; set; } = string.Empty;

        public string DatabaseColumnName { get; set; } = string.Empty;
        public LabelTextSeparate()
        {
            InitializeComponent();
        }

        //データベースから取得した値を分割する
        public void ValueChanged()
        {
            int strLength = Value.Trim().Length;
            int seprateIndex = Value.Trim().IndexOf('　');
            int length = 0;
            if (seprateIndex > -1)
            {

                length = Value.Trim().Substring(seprateIndex).Length;
            }
            if (strLength <= COLOR_NAME_LOWER_LIMIT)
            {
                Lbl1Value = string.Empty;
                Lbl2Value = string.Empty;
                return;
            }
            if (strLength > COLOR_NAME_HIGH_LIMIT)
            {
                if (seprateIndex < COLOR_NAME_LOWER_LIMIT || COLOR_NAME_HIGH_LIMIT < seprateIndex + 1 || length > COLOR_NAME_HIGH_LIMIT)
                {
                    Lbl1Value = Value.Trim().Substring(0, COLOR_NAME_HIGH_LIMIT);
                    Lbl2Value = Value.Trim().Substring(COLOR_NAME_HIGH_LIMIT);
                }
                else
                {
                    Lbl1Value = Value.Trim().Substring(0, seprateIndex + 1);
                    Lbl2Value = Value.Trim().Substring(seprateIndex + 1);
                }
            }
            else
            {
                Lbl1Value = Value.Trim();
                Lbl2Value = string.Empty;
            }
        }
        //文字列を任意の場所で分割する
        public int SetString(int param)
        {
            switch (param)
            {
                case 0:

                    if (COLOR_NAME_LOWER_LIMIT < Lbl1Value.Length && Lbl2Value.Length < COLOR_NAME_HIGH_LIMIT)
                    {
                        Lbl2Value = $"{Lbl1Value.Substring(Lbl1Value.Length - 1, 1)}{Lbl2Value}";
                        Lbl1Value = $"{Lbl1Value.Substring(0, Lbl1Value.Length - 1)}";
                    }
                    break;
                case 1:
                    if (COLOR_NAME_LOWER_LIMIT < Lbl2Value.Length && Lbl1Value.Length < COLOR_NAME_HIGH_LIMIT)
                    {
                        Lbl1Value = $"{Lbl1Value}{Lbl2Value.Substring(0, 1)}";
                        Lbl2Value = Lbl2Value.Substring(1, Lbl2Value.Length - 1);
                    }
                    break;
            }
            //LabelTextSeperateコントロールのLabelへ文字数を設定する
            WordCount = $"({Lbl1Value.Length}/{Lbl2Value.Length})";
            return Lbl1Value.Length;


        }
    }
}
