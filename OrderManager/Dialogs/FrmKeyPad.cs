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
using System;
using System.Windows.Forms;
using NipponPaint.NpCommon;
#endregion

namespace NipponPaint.OrderManager.Dialogs
{
    /// <summary>
    /// KEYPADダイアログ
    /// </summary>
    public partial class FrmKeyPad : BaseForm
    {
        #region コンストラクタ
        public FrmKeyPad()
        {
            InitializeComponent();
            InitializeForm();
        }
        #endregion
        #region イベント

        /// <summary>
        /// 演算処理
        /// </summary>
        public bool LabelOverWrite = true;　　　　//NumLabelの上書きをする場合はtrue
        private bool NumPeriod = false;          //「.」の有無判定

        /// <summary>
        /// キー操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.NumPad0:
                        // テンキーにて「0」
                        BtnInput0.PerformClick();
                        break;
                    case Keys.NumPad1:
                        // テンキーにて「1」
                        BtnInput1.PerformClick();
                        break;
                    case Keys.NumPad2:
                        // テンキーにて「2」
                        BtnInput2.PerformClick();
                        break;
                    case Keys.NumPad3:
                        // テンキーにて「3」
                        BtnInput3.PerformClick();
                        break;
                    case Keys.NumPad4:
                        // テンキーにて「4」
                        BtnInput4.PerformClick();
                        break;
                    case Keys.NumPad5:
                        // テンキーにて「5」
                        BtnInput5.PerformClick();
                        break;
                    case Keys.NumPad6:
                        // テンキーにて「6」
                        BtnInput6.PerformClick();
                        break;
                    case Keys.NumPad7:
                        // テンキーにて「7」
                        BtnInput7.PerformClick();
                        break;
                    case Keys.NumPad8:
                        // テンキーにて「8」
                        BtnInput8.PerformClick();
                        break;
                    case Keys.NumPad9:
                        // テンキーにて「9」
                        BtnInput9.PerformClick();
                        break;
                    case Keys.Decimal:
                        // テンキーにて「小数点(.)」
                        BtnInputPeriod.PerformClick();
                        break;
                    case Keys.Add:
                        // テンキーにて1文字削除「C」
                        BtnC.PerformClick();
                        break;
                    case Keys.D0:
                        // フルキーにて「0」
                        BtnInput0.PerformClick();
                        break;
                    case Keys.D1:
                        // フルキーにて「1」
                        BtnInput1.PerformClick();
                        break;
                    case Keys.D2:
                        // フルキーにて「2」
                        BtnInput2.PerformClick();
                        break;
                    case Keys.D3:
                        // フルキーにて「3」
                        BtnInput3.PerformClick();
                        break;
                    case Keys.D4:
                        // フルキーにて「4」
                        BtnInput4.PerformClick();
                        break;
                    case Keys.D5:
                        // フルキーにて「5」
                        BtnInput5.PerformClick();
                        break;
                    case Keys.D6:
                        // フルキーにて「6」
                        BtnInput6.PerformClick();
                        break;
                    case Keys.D7:
                        // フルキーにて「7」
                        BtnInput7.PerformClick();
                        break;
                    case Keys.D8:
                        // フルキーにて「8」
                        BtnInput8.PerformClick();
                        break;
                    case Keys.D9:
                        // フルキーにて「9」
                        BtnInput9.PerformClick();
                        break;
                    case Keys.OemPeriod:
                        // フルキーにて「小数点（.）」
                        BtnInputPeriod.PerformClick();
                        break;
                    case Keys.Back:
                        // フルキーにて1文字削除「C」
                        BtnC.PerformClick();
                        break;
                    case Keys.Enter:
                        // Enter「OK」
                        BtnOK.PerformClick();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 「0」入力ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInput0Click(object sender, EventArgs e)
        {
            try
            {
                if(LabelOverWrite == true)
                {
                    NumLabel.Text = BtnInput0.Text;
                }
                else
                {
                    NumLabel.Text += BtnInput0.Text;
                }
                
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch(Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 「１」入力ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInput1Click(object sender, EventArgs e)
        {
            try
            {
                
                textInput(sender);
                //if(LabelOverWrite == true)
                //{
                //    NumLabel.Text = BtnInput1.Text;
                //    LabelOverWrite = false;
                //}
                //else
                //{
                //    NumLabel.Text += BtnInput1.Text;
                //}
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch(Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 「2」入力ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInput2Click(object sender, EventArgs e)
        {
            try
            {
                textInput(sender);
                //if (LabelOverWrite == true)
                //{
                //    NumLabel.Text = BtnInput2.Text;
                //    LabelOverWrite = false;
                //}
                //else
                //{
                //    NumLabel.Text += BtnInput2.Text;
                //}
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch(Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 「3」入力ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInput3Click(object sender, EventArgs e)
        {
            try
            {
                textInput(sender);
                //if (LabelOverWrite == true)
                //{
                //    NumLabel.Text = BtnInput3.Text;
                //    LabelOverWrite = false;
                //}
                //else
                //{
                //    NumLabel.Text += BtnInput3.Text;
                //}
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch(Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 「4」入力ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInput4Click(object sender, EventArgs e)
        {
            try
            {
                textInput(sender);
                //if (LabelOverWrite == true)
                //{
                //    NumLabel.Text = BtnInput4.Text;
                //    LabelOverWrite = false;
                //}
                //else
                //{
                //    NumLabel.Text += BtnInput4.Text;
                //}
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch(Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 「5」入力ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInput5Click(object sender, EventArgs e)
        {
            try
            {
                textInput(sender);
                //if (LabelOverWrite == true)
                //{
                //    NumLabel.Text = BtnInput5.Text;
                //    LabelOverWrite = false;
                //}
                //else
                //{
                //    NumLabel.Text += BtnInput5.Text;
                //}
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch(Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 「6」入力ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInput6Click(object sender, EventArgs e)
        {
            try
            {
                textInput(sender);
                //if (LabelOverWrite == true)
                //{
                //    NumLabel.Text = BtnInput6.Text;
                //    LabelOverWrite = false;
                //}
                //else
                //{
                //    NumLabel.Text += BtnInput6.Text;
                //}
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch(Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 「7」入力ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInput7Click(object sender, EventArgs e)
        {
            try
            {
                textInput(sender);
                //if (LabelOverWrite == true)
                //{
                //    NumLabel.Text = BtnInput7.Text;
                //    LabelOverWrite = false;
                //}
                //else
                //{
                //    NumLabel.Text += BtnInput7.Text;
                //}
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch(Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 「8」入力ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInput8Click(object sender, EventArgs e)
        {
            try
            {
                textInput(sender);
                //if (LabelOverWrite == true)
                //{
                //    NumLabel.Text = BtnInput8.Text;
                //    LabelOverWrite = false;
                //}
                //else
                //{
                //    NumLabel.Text += BtnInput8.Text;
                //}
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 「9」入力ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInput9Click(object sender, EventArgs e)
        {
            try
            {
                textInput(sender);
                //if (LabelOverWrite == true)
                //{
                //    NumLabel.Text = BtnInput9.Text;
                //    LabelOverWrite = false;
                //}
                //else
                //{
                //    NumLabel.Text += BtnInput9.Text;
                //}
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch(Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 「.」入力ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPeriodClick(object sender, EventArgs e)
        {
            try
            {
                if(NumPeriod == false)
                {
                    NumLabel.Text = NumLabel.Text + ".";       //「.」が押せるようになる
                    NumPeriod = true;　　　　　　　　　　　　　　//「.」の連打防止
                    LabelOverWrite = false;
                }
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch(Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 「+/-」入力ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInputPlusMinusClick(object sender, EventArgs e)　　　//現行アプリでは操作不可となっているため、 Enabled は false にしてあります
        {
            try
            {
                if (NumLabel.Text.Contains("-"))
                {
                    NumLabel.Text = NumLabel.Text.Replace("-", "");
                }
                else
                {
                    NumLabel.Text = "-" + NumLabel.Text;
                }
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch(Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 「CA」入力ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCAClick(object sender, EventArgs e)
        {
            try
            {
                NumLabel.Text = "0";　　　　　//全文字削除し、「0」を表示
                LabelOverWrite = true;　　　　//全文字削除したあとの「0」の連打防止
                NumPeriod = false;
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch(Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 「C」入力ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCClick(object sender, EventArgs e)
        {
            try
            {
                NumLabel.Text = NumLabel.Text.Remove(NumLabel.Text.Length - 1);　　　　　//打ち込んだ数列の最後の1文字だけ削除
                decimal.TryParse(NumLabel.Text.ToString(), out decimal intNumLabel);
                if (string.IsNullOrEmpty(NumLabel.Text) || intNumLabel == 0)
                {
                    NumLabel.Text = "0";　　　　　//最後の1文字を削除後、「0」を表示
                    LabelOverWrite = true;　　　 //最後の1文字を削除して「0」表示になったあとに「0」の連打防止
                    NumPeriod = false;
                }
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch(Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 「OK」入力ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOKClick(object sender, EventArgs e)
        {
            try
            {
                MessageBox.Show("入力を決定しました。");
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
                this.Close();
            }
            catch(Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 「CANCEL」入力ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnCancelClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
                this.Close();
            }
            catch(Exception ex)
            {
                PutLog(ex);
            }
        }
        #endregion

        #region private functions
        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitializeForm()
        {
            // イベントの追加
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(this.FormKeyDown);
            this.BtnInput0.Click += new EventHandler(this.BtnInput0Click);
            this.BtnInput1.Click += new EventHandler(this.BtnInput1Click);
            this.BtnInput2.Click += new EventHandler(this.BtnInput2Click);
            this.BtnInput3.Click += new EventHandler(this.BtnInput3Click);
            this.BtnInput4.Click += new EventHandler(this.BtnInput4Click);
            this.BtnInput5.Click += new EventHandler(this.BtnInput5Click);
            this.BtnInput6.Click += new EventHandler(this.BtnInput6Click);
            this.BtnInput7.Click += new EventHandler(this.BtnInput7Click);
            this.BtnInput8.Click += new EventHandler(this.BtnInput8Click);
            this.BtnInput9.Click += new EventHandler(this.BtnInput9Click);
            this.BtnInputPeriod.Click += new EventHandler(this.BtnPeriodClick);
            this.BtnInputPlusMinus.Click += new EventHandler(this.BtnInputPlusMinusClick);
            this.BtnCA.Click += new EventHandler(this.BtnCAClick);
            this.BtnC.Click += new EventHandler(this.BtnCClick);
            this.BtnOK.Click += new EventHandler(this.BtnOKClick);
            this.BtnCancel.Click += new EventHandler(this.BtnCancelClick);
            this.ActiveControl = this.BtnOK;  //最初からフォーカスをOKボタンに照準
        }
        /// <summary>
        /// 「0」～「9」のボタン動作をまとめた挙動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void textInput(object sender)
        {
            var Button = sender as Button;
            //string num = sender.
            switch (Button.Text)
            {
                case "0":
                    if (LabelOverWrite == true)
                    {
                        NumLabel.Text = Button.Text;
                    }
                    else
                    {
                        NumLabel.Text += Button.Text;
                    }
                    break;
                default:
                    if (LabelOverWrite == true)
                    {
                        NumLabel.Text = Button.Text;
                        LabelOverWrite = false;
                    }
                    else
                    {
                        NumLabel.Text += Button.Text;
                    }
                    break;
            }
        }
        #endregion
    }
}

