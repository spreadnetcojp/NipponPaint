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
        #region 定数
        private const int NumLabelMaxValue = 25000;
        private const int NumLabelMinValue = 0;
        #endregion

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
        /// 「0」から「9」のボタンそれぞれの挙動まとめ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnInputClick(object sender, EventArgs e)
        {
            try
            {
                var Button = sender as Button;
                //「0」とそれ以外
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
                Validation();
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
                Validation();
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
                Validation();
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
                NumPeriod = NumLabel.Text.ToString().Contains(".");              //「.」を削除したあとに再度「.」を打てるようにする
                if (string.IsNullOrEmpty(NumLabel.Text) || intNumLabel == 0)
                {
                    NumLabel.Text = "0";　　　　　//最後の1文字を削除後、「0」を表示
                    LabelOverWrite = true;　　　 //最後の1文字を削除して「0」表示になったあとに「0」の連打防止
                    NumPeriod = false;
                }
                Validation();
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
            this.BtnInput0.Click += new EventHandler(this.BtnInputClick);
            this.BtnInput1.Click += new EventHandler(this.BtnInputClick);
            this.BtnInput2.Click += new EventHandler(this.BtnInputClick);
            this.BtnInput3.Click += new EventHandler(this.BtnInputClick);
            this.BtnInput4.Click += new EventHandler(this.BtnInputClick);
            this.BtnInput5.Click += new EventHandler(this.BtnInputClick);
            this.BtnInput6.Click += new EventHandler(this.BtnInputClick);
            this.BtnInput7.Click += new EventHandler(this.BtnInputClick);
            this.BtnInput8.Click += new EventHandler(this.BtnInputClick);
            this.BtnInput9.Click += new EventHandler(this.BtnInputClick);
            this.BtnInputPeriod.Click += new EventHandler(this.BtnPeriodClick);
            this.BtnInputPlusMinus.Click += new EventHandler(this.BtnInputPlusMinusClick);
            this.BtnCA.Click += new EventHandler(this.BtnCAClick);
            this.BtnC.Click += new EventHandler(this.BtnCClick);
            this.BtnOK.Click += new EventHandler(this.BtnOKClick);
            this.BtnCancel.Click += new EventHandler(this.BtnCancelClick);
            this.ActiveControl = BtnOK;  //最初からフォーカスをOKボタンに標準
        }
        /// <summary>
        /// 入力値の最大値と最小値の決定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Validation()
        {
            bool boolResult = decimal.TryParse(NumLabel.Text, out decimal InputValue);　　　//stringから数値に変換　　　
            if (boolResult)
            {
                if (NumLabelMinValue <= InputValue && InputValue <= NumLabelMaxValue)　　　　//入力値が「0」以上及び「25000」未満のときはOKボタン使用可能
                {
                    BtnOK.Enabled = true;
                    NumLabel.ForeColor = System.Drawing.Color.Aqua;
                }
                else
                {
                    BtnOK.Enabled = false;
                    NumLabel.ForeColor = System.Drawing.Color.Yellow;       //入力値が「0」以下及び「25000」超過の時は数値の文字色が黄色に変化する
                }
            }
            else
            {
                ; //何もしない
            }
        }
        #endregion
    }
}

