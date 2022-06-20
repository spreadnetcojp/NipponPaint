using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NipponPaint.SupervisorIfSim.FormControls
{
    public class RadioButtonSetting
    {
        public string Text { get; set; }
        public int Left { get; set; }
        public int Value { get; set; }
        public bool Visible { get; set; }
    }
    public partial class ThreeRadioButtons : UserControl
    {
        public string FirstButtonText { get { return FirstButton.Text; } set { FirstButton.Text = value; } }
        public string SecondButtonText { get { return SecondButton.Text; } set { SecondButton.Text = value; } }
        public string ThirdButtonText { get { return ThirdButton.Text; } set { ThirdButton.Text = value; } }
        public int FirstButtonLeftPosition { get { return FirstButton.Left; } set { FirstButton.Left = value; } }
        public int SecondButtonLeftPosition { get { return SecondButton.Left; } set { SecondButton.Left = value; } }
        public int ThirdButtonLeftPosition { get { return ThirdButton.Left; } set { ThirdButton.Left = value; } }
        public int FirstButtonDefault { get { return int.Parse(FirstButton.Tag.ToString()); } set { FirstButton.Tag = value; } }
        public int SecondButtonDefault { get { return int.Parse(SecondButton.Tag.ToString()); } set { SecondButton.Tag = value; } }
        public int ThirdButtonDefault { get { return int.Parse(ThirdButton.Tag.ToString()); } set { ThirdButton.Tag = value; } }
        public bool FirstButtonVisible { get { return FirstButton.Visible; } set { FirstButton.Visible = value; } }
        public bool SecondButtonVisible { get { return SecondButton.Visible; } set { SecondButton.Visible = value; } }
        public bool ThirdButtonVisible { get { return ThirdButton.Visible; } set { ThirdButton.Visible = value; } }
        public int Value
        {
            get
            {
                if (FirstButton.Checked)
                {
                    return int.Parse(FirstButton.Tag.ToString());
                }
                if (SecondButton.Checked)
                {
                    return int.Parse(SecondButton.Tag.ToString());
                }
                if (ThirdButton.Checked)
                {
                    return int.Parse(ThirdButton.Tag.ToString());
                }
                return -99;
            }
            set
            {
                FirstButton.Checked = false;
                SecondButton.Checked = false;
                ThirdButton.Checked = false;
                if (SetValue(value, FirstButton))
                {
                    return;
                }
                else if (SetValue(value, SecondButton))
                {
                    return;
                }
                else if (SetValue(value, ThirdButton))
                {
                    return;
                }
            }
        }
        public ThreeRadioButtons()
        {
            InitializeComponent();
        }

        private bool SetValue(int value, RadioButton rdo)
        {
            if (int.TryParse(rdo.Tag.ToString(), out int val))
            {
                if (value == val)
                {
                    rdo.Checked = true;
                    return true;
                }
            }
            return false;
        }
    }
}
