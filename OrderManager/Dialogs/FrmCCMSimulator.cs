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
using System.Collections.Generic;
using System.Windows.Forms;
using NipponPaint.NpCommon;
using NipponPaint.NpCommon.Database;
using NipponPaint.NpCommon.FormControls;
#endregion

namespace NipponPaint.OrderManager.Dialogs
{
    /// <summary>
    /// 注文開始ダイアログ
    /// </summary>
    public partial class FrmCCMSimulator : BaseForm
    {
        static string productCodeLeft;
        static string productCodeRight;
        #region コンストラクタ
        public FrmCCMSimulator(ViewModels.CCMSimulatorData vm)
        {
            InitializeComponent();
            InitializeForm();
            TxtKanjiColorName.Value = vm.KanjiColorName;
            productCodeLeft = vm.ProductCodeLeft;
            productCodeRight = vm.ProductCodeRight;
            DrpProductCodeLeft.DropDown.Text = productCodeLeft;
            DrpProductCodeRight.Text = productCodeRight;
            TxtPaintName.Value = vm.PaintName;
            TxtColorFileName.Value = vm.ColorFileName;
            TxtLine.Value = vm.Line;
            NumUpDownCorrection.Value = vm.Correction;
            TxtBaseValue.Text = String.Format("{0:F3}", vm.BaseValue);
            NumUpDownLabel.Value = vm.Label;
            TxtCSAreaCode.Text = vm.CSAreaCode;
            TxtColoarantValue1.Text = String.Format("{0:F3}", vm.ColoarantValue1);
            TxtColoarantValue2.Text = String.Format("{0:F3}", vm.ColoarantValue2);
            TxtColoarantValue3.Text = String.Format("{0:F3}", vm.ColoarantValue3);
            TxtColoarantValue4.Text = String.Format("{0:F3}", vm.ColoarantValue4);
            TxtColoarantValue5.Text = String.Format("{0:F3}", vm.ColoarantValue5);
            TxtColoarantValue6.Text = String.Format("{0:F3}", vm.ColoarantValue6);
            TxtColoarantValue7.Text = String.Format("{0:F3}", vm.ColoarantValue7);
            TxtColoarantValue8.Text = String.Format("{0:F3}", vm.ColoarantValue8);
            TxtColoarantValue9.Text = String.Format("{0:F3}", vm.ColoarantValue9);
            TxtColoarantValue10.Text = String.Format("{0:F3}", vm.ColoarantValue10);
        }
        #endregion

        /// <summary>
        /// ボタン押下時のショートカット動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.F9:
                        // 色名ラベルのプリント
                        BtnColorNamePrint.PerformClick();
                        break;
                    case Keys.F6:
                        // キャンセル
                        BtnCancel.PerformClick();
                        break;
                    case Keys.F7:
                        // 閉じる
                        BtnClose.PerformClick();
                        break;
                }
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 色名ラベルのプリントボタン(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        #region イベント
        private void BtnColorNamePrintClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// CCMデータ送信ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCCMDataSendClick(object sender, EventArgs e)
        {
            try
            {
                var parameters = new List<ParameterItem>()
                {
                    new ParameterItem("@ColorName", TxtKanjiColorName.Value),
                    new ParameterItem("@ProductCode", $"{DrpProductCodeLeft.DropDown.Text}{DrpProductCodeRight.Text}"),
                    new ParameterItem("@PaintName", TxtPaintName.Value),
                    new ParameterItem("@TintedColor", TxtColorFileName.Value),
                    new ParameterItem("@LineName", TxtLine.Value),
                    new ParameterItem("@Revision", NumUpDownCorrection.Value),
                    new ParameterItem("@WhiteCode", DrpBaseSelect.Text),
                    new ParameterItem("@WhiteWeight", TxtBaseValue.Text),
                    new ParameterItem("@Colorant1", DrpColoarantSelect1.Text),
                    new ParameterItem("@Weight1", TxtColoarantValue1.Text),
                    new ParameterItem("@Colorant2", DrpColoarantSelect2.Text),
                    new ParameterItem("@Weight2", TxtColoarantValue2.Text),
                    new ParameterItem("@Colorant3", DrpColoarantSelect3.Text),
                    new ParameterItem("@Weight3", TxtColoarantValue3.Text),
                    new ParameterItem("@Colorant4", DrpColoarantSelect4.Text),
                    new ParameterItem("@Weight4", TxtColoarantValue4.Text),
                    new ParameterItem("@Colorant5", DrpColoarantSelect5.Text),
                    new ParameterItem("@Weight5", TxtColoarantValue5.Text),
                    new ParameterItem("@Colorant6", DrpColoarantSelect6.Text),
                    new ParameterItem("@Weight6", TxtColoarantValue6.Text),
                    new ParameterItem("@Colorant7", DrpColoarantSelect7.Text),
                    new ParameterItem("@Weight7", TxtColoarantValue7.Text),
                    new ParameterItem("@Colorant8", DrpColoarantSelect8.Text),
                    new ParameterItem("@Weight8", TxtColoarantValue8.Text),
                    new ParameterItem("@Colorant9", DrpColoarantSelect9.Text),
                    new ParameterItem("@Weight9", TxtColoarantValue9.Text),
                    new ParameterItem("@Colorant10", DrpColoarantSelect10.Text),
                    new ParameterItem("@Weight10", TxtColoarantValue10.Text),
                };
                var a = parameters;
                //Messages.ShowDialog(Sentence.Messages.SelectMaterialError);
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// キャンセルボタン(F6)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnCancelClick(object sender, EventArgs e)
        {
            try
            {
                NumUpDownCorrection.Value = 0;
                TxtKanjiColorName.Value = "";
                TxtPaintName.Value = "";
                DrpProductCodeLeft.DropDown.Text = productCodeLeft;
                DrpProductCodeRight.Text = productCodeRight;
                DrpBaseSelect.Text = "";
                DrpColoarantSelect1.Text = "";
                DrpColoarantSelect2.Text = "";
                DrpColoarantSelect3.Text = "";
                DrpColoarantSelect4.Text = "";
                DrpColoarantSelect5.Text = "";
                DrpColoarantSelect6.Text = "";
                DrpColoarantSelect7.Text = "";
                DrpColoarantSelect8.Text = "";
                DrpColoarantSelect9.Text = "";
                DrpColoarantSelect10.Text = "";
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 閉じるボタン(F7)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCloseClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
                this.Close();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrpBaseSelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex > 0)
            {
                TxtBaseValue.Enabled = true;
                FrmKeyPad frmKeyPad = new FrmKeyPad();
                frmKeyPad.ShowDialog();
                TxtBaseValue.Text = Funcs.StrToDecimal(frmKeyPad.NumLabel.Text);
            }
            else
            {
                TxtBaseValue.Enabled = false;
            }
        }
        private void DrpColoarantSelect1SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex > 0)
            {
                TxtColoarantValue1.Enabled = true;
                FrmKeyPad frmKeyPad = new FrmKeyPad();
                frmKeyPad.ShowDialog();
                TxtColoarantValue1.Text = Funcs.StrToDecimal(frmKeyPad.NumLabel.Text);
            }
            else
            {
                TxtColoarantValue1.Enabled = false;
            }
        }
        private void DrpColoarantSelect2SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex > 0)
            {
                TxtColoarantValue2.Enabled = true;
                FrmKeyPad frmKeyPad = new FrmKeyPad();
                frmKeyPad.ShowDialog();
                TxtColoarantValue2.Text = Funcs.StrToDecimal(frmKeyPad.NumLabel.Text);
            }
            else
            {
                TxtColoarantValue2.Enabled = false;
            }
        }
        private void DrpColoarantSelect3SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex > 0)
            {
                TxtColoarantValue3.Enabled = true;
                FrmKeyPad frmKeyPad = new FrmKeyPad();
                frmKeyPad.ShowDialog();
                TxtColoarantValue3.Text = Funcs.StrToDecimal(frmKeyPad.NumLabel.Text);
            }
            else
            {
                TxtColoarantValue3.Enabled = false;
            }
        }
        private void DrpColoarantSelect4SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex > 0)
            {
                TxtColoarantValue4.Enabled = true;
                FrmKeyPad frmKeyPad = new FrmKeyPad();
                frmKeyPad.ShowDialog();
                TxtColoarantValue4.Text = Funcs.StrToDecimal(frmKeyPad.NumLabel.Text);
            }
            else
            {
                TxtColoarantValue4.Enabled = false;
            }
        }
        private void DrpColoarantSelect5SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex > 0)
            {
                TxtColoarantValue5.Enabled = true;
                FrmKeyPad frmKeyPad = new FrmKeyPad();
                frmKeyPad.ShowDialog();
                TxtColoarantValue5.Text = Funcs.StrToDecimal(frmKeyPad.NumLabel.Text);
            }
            else
            {
                TxtColoarantValue5.Enabled = false;
            }
        }
        private void DrpColoarantSelect6SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex > 0)
            {
                TxtColoarantValue6.Enabled = true;
                FrmKeyPad frmKeyPad = new FrmKeyPad();
                frmKeyPad.ShowDialog();
                TxtColoarantValue6.Text = Funcs.StrToDecimal(frmKeyPad.NumLabel.Text);
            }
            else
            {
                TxtColoarantValue6.Enabled = false;
            }
        }
        private void DrpColoarantSelect7SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex > 0)
            {
                TxtColoarantValue7.Enabled = true;
                FrmKeyPad frmKeyPad = new FrmKeyPad();
                frmKeyPad.ShowDialog();
                TxtColoarantValue7.Text = Funcs.StrToDecimal(frmKeyPad.NumLabel.Text);
            }
            else
            {
                TxtColoarantValue7.Enabled = false;
            }
        }
        private void DrpColoarantSelect8SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex > 0)
            {
                TxtColoarantValue8.Enabled = true;
                FrmKeyPad frmKeyPad = new FrmKeyPad();
                frmKeyPad.ShowDialog();
                TxtColoarantValue8.Text = Funcs.StrToDecimal(frmKeyPad.NumLabel.Text);
            }
            else
            {
                TxtColoarantValue8.Enabled = false;
            }
        }
        private void DrpColoarantSelect9SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex > 0)
            {
                TxtColoarantValue9.Enabled = true;
                FrmKeyPad frmKeyPad = new FrmKeyPad();
                frmKeyPad.ShowDialog();
                TxtColoarantValue9.Text = Funcs.StrToDecimal(frmKeyPad.NumLabel.Text);
            }
            else
            {
                TxtColoarantValue9.Enabled = false;
            }
        }
        private void DrpColoarantSelect10SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex > 0)
            {
                TxtColoarantValue10.Enabled = true;
                FrmKeyPad frmKeyPad = new FrmKeyPad();
                frmKeyPad.ShowDialog();
                TxtColoarantValue10.Text = Funcs.StrToDecimal(frmKeyPad.NumLabel.Text);
            }
            else
            {
                TxtColoarantValue10.Enabled = false;
            }
        }

        private void TxtBaseValueMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                FrmKeyPad frmKeyPad = new FrmKeyPad();
                frmKeyPad.ShowDialog();
                ((TextBoxBase)sender).Text = Funcs.StrToDecimal(frmKeyPad.NumLabel.Text);
            }
        }
        #endregion

        #region private functions
        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitializeForm()
        {
            //コントロールの配置

            // イベントの追加
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(this.FormKeyDown);
            this.BtnColorNamePrint.Click += new EventHandler(this.BtnColorNamePrintClick);
            this.BtnCCMDataSend.Click += new EventHandler(this.BtnCCMDataSendClick);
            this.BtnCancel.Click += new EventHandler(this.BtnCancelClick);
            this.BtnClose.Click += new EventHandler(this.BtnCloseClick);
            this.DrpBaseSelect.SelectedIndexChanged += new System.EventHandler(this.DrpBaseSelectedIndexChanged);
            this.DrpColoarantSelect1.SelectedIndexChanged += new System.EventHandler(this.DrpColoarantSelect1SelectedIndexChanged);
            this.DrpColoarantSelect2.SelectedIndexChanged += new System.EventHandler(this.DrpColoarantSelect2SelectedIndexChanged);
            this.DrpColoarantSelect3.SelectedIndexChanged += new System.EventHandler(this.DrpColoarantSelect3SelectedIndexChanged);
            this.DrpColoarantSelect4.SelectedIndexChanged += new System.EventHandler(this.DrpColoarantSelect4SelectedIndexChanged);
            this.DrpColoarantSelect5.SelectedIndexChanged += new System.EventHandler(this.DrpColoarantSelect5SelectedIndexChanged);
            this.DrpColoarantSelect6.SelectedIndexChanged += new System.EventHandler(this.DrpColoarantSelect6SelectedIndexChanged);
            this.DrpColoarantSelect7.SelectedIndexChanged += new System.EventHandler(this.DrpColoarantSelect7SelectedIndexChanged);
            this.DrpColoarantSelect8.SelectedIndexChanged += new System.EventHandler(this.DrpColoarantSelect8SelectedIndexChanged);
            this.DrpColoarantSelect9.SelectedIndexChanged += new System.EventHandler(this.DrpColoarantSelect9SelectedIndexChanged);
            this.DrpColoarantSelect10.SelectedIndexChanged += new System.EventHandler(this.DrpColoarantSelect10SelectedIndexChanged);
            this.TxtBaseValue.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TxtBaseValueMouseUp);
            this.TxtColoarantValue1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TxtBaseValueMouseUp);
            this.TxtColoarantValue2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TxtBaseValueMouseUp);
            this.TxtColoarantValue3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TxtBaseValueMouseUp);
            this.TxtColoarantValue4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TxtBaseValueMouseUp);
            this.TxtColoarantValue5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TxtBaseValueMouseUp);
            this.TxtColoarantValue6.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TxtBaseValueMouseUp);
            this.TxtColoarantValue7.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TxtBaseValueMouseUp);
            this.TxtColoarantValue8.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TxtBaseValueMouseUp);
            this.TxtColoarantValue9.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TxtBaseValueMouseUp);
            this.TxtColoarantValue10.MouseUp += new System.Windows.Forms.MouseEventHandler(this.TxtBaseValueMouseUp);

            Funcs.SetControlEnabled(this.Controls, true);

            //データ表示部の設定
            //カレンダー初期値 年月日
            DateTimePicker.Value = DateTime.Now;
            TxtBaseValue.Enabled = false;
            TxtColoarantValue1.Enabled = false;
            TxtColoarantValue2.Enabled = false;
            TxtColoarantValue3.Enabled = false;
            TxtColoarantValue4.Enabled = false;
            TxtColoarantValue5.Enabled = false;
            TxtColoarantValue6.Enabled = false;
            TxtColoarantValue7.Enabled = false;
            TxtColoarantValue8.Enabled = false;
            TxtColoarantValue9.Enabled = false;
            TxtColoarantValue10.Enabled = false;
            /// <summary>
            /// 製品コード(左)のドロップダウン
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            DrpProductCodeLeft.DropDown.Items.AddRange(new object[] {
                "A",
                "B",
                "C",
                "D",
                "E",
                "F",
                "G",
                "H",
                "J",
                "K",
                "L",
                "M",
                "N",
                "P",
                "Q",
                "R",
                "S",
                "T",
                "U",
                "V",
                "W"
            });
            /// <summary>
            /// 製品コード(右)のドロップダウン(コンボボックス)
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            DrpProductCodeRight.Items.AddRange(new object[] {
                "2",
                "3",
                "4",
                "5",
                "6",
                "7",
                "8",
                "9",
                "A",
                "B",
                "C",
                "D",
                "E",
                "F",
                "G",
                "H",
                "J",
                "K",
                "L",
                "M",
                "N",
                "P",
                "Q",
                "R",
                "S",
                "T",
                "U",
                "V",
                "W",
                "X",
                "Y",
                "Z"
            });
            /// <summary>
            /// ベースのドロップダウン
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            DrpBaseSelect.Items.AddRange(new object[] {
                "",
                "EMPTY",
                "3OFSK",
                "993OFS",
                "WELMｵｰｶｰ",
                "WEｵｰｶｰ",
                "WEﾌﾞﾗｯｸ",
                "WEｲﾝﾃﾞｱﾝ",
                "WEﾊﾟ-ﾏﾈY",
                "ＷＥシャニンＢシ",
                "WEｼﾝｶRN",
                "WEｸﾞﾘｰﾝ",
                "WEｼｬﾆﾝBN",
                "OFS",
                "OFU",
                "TW1",
                "SSSU",
                "HVE",
                "EF6",
                "EF7",
                "WKEN",
                "DFR",
                "OFUC",
                "OFSC",
                "HV3",
                "OS3",
                "OS5",
                "OS0",
                "OG",
                "IDALE",
                "IDANE",
                "HV70E",
                "OFF",
                "OFS5S",
                "EFC",
                "OU3",
                "SOFS",
                "NLB",
                "OFS5C",
                "WFU0",
                "EF10",
                "OU0",
                "OFS50",
                "OU5",
                "HV7",
                "SCHD",
                "OG3",
                "SCHL",
                "OG0",
                "FBRE",
                "DFR0",
                "DFRC",
                "WKENC",
                "SSSUC",
                "DSR",
                "DSR0",
                "APSN",
                "DSRC",
                "OCGE0",
                "DTW",
                "SSSU5",
                "SSSU3",
                "SSSU0",
                "OCGEC",
                "OCGE3",
                "OCGE",
                "WH3E",
                "ONT3",
                "OFA",
                "OCGE5",
                "OFS5N",
                "OFMC",
                "SOFC",
                "DAEU",
                "WFU5",
                "WFU3",
                "HV3N",
                "OFSN",
                "OFUN",
                "OFSN0",
                "OFSN3",
                "OFUN0",
                "OFSN5",
                "30FS",
                "ALCT",
                "WFU",
                "3OFSC",
                "3OFS0",
                "3OFS3",
                "3ＯＦＳ5",
                "OFUN3",
                "OFUN5",
                "HVE6",
                "ONT5",
                "DAESI",
                "PT",
                "PTC",
                "PT3",
                "PT5",
                "PT0",
                "TEW",
                "EFN7",
                "EFN6",
                "EFN10",
                "PT3N",
                "WTWS",
                "WTWSC",
                "WTWS3",
                "WTWF",
                "SOFSN",
                "SOFSN3",
                "OCGEN",
                "OCGENC",
                "OCGEN0",
                "OCGEN3",
                "OCGEN5",
                "TW1N",
                "WWS",
                "TW1N3",
                "TW1N5",
                "TW1NC",
                "HVE7",
                "DAEF",
                "DAEFM",
                "ALCT5",
                "IDFC",
                "IDFCM",
                "IDFCD",
                "JSR",
                "SOFF",
                "WFSI",
                "WFSI3",
                "HVF",
                "DFRN",
                "DFRNC",
                "DFRN0",
                "DFRN5",
                "NOKR",
                "DSRN0",
                "ALCF",
                "POS",
                "POS5",
                "POS3",
                "POS0",
                "POF",
                "POF5",
                "POF3",
                "POF0",
                "WKENG",
                "WKEN3",
                "OGN3",
                "OGN5",
                "OGN0",
                "WFSIC",
                "EP",
                "PCTGM",
                "NDTW",
                "GPW",
                "GPW3",
                "GPW5",
                "GPW0",
                "FBRE0",
                "OFF3",
                "EFCN",
                "PTCN",
                "DTSI",
                "DTSC",
                "EPN",
                "APS2",
                "3OFS7",
                "DSRNN",
                "ALCTN",
                "DSRN5",
                "ＮＫ",
                "NOK",
                "PCTG",
                "ＤＳＲＮＣ"
            });
            DrpBaseSelect.SelectedIndex = 0;
            /// <summary>
            /// 着色剤1のドロップダウン
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            DrpColoarantSelect1.Items.AddRange(new object[] {
                "",
                "EMPTY",
                "3OFSK",
                "993OFS",
                "WELMｵｰｶｰ",
                "WEｵｰｶｰ",
                "WEﾌﾞﾗｯｸ",
                "WEｲﾝﾃﾞｱﾝ",
                "WEﾊﾟ-ﾏﾈY",
                "ＷＥシャニンＢシ",
                "WEｼﾝｶRN",
                "WEｸﾞﾘｰﾝ",
                "WEｼｬﾆﾝBN",
                "OFS",
                "OFU",
                "TW1",
                "SSSU",
                "HVE",
                "EF6",
                "EF7",
                "WKEN",
                "DFR",
                "OFUC",
                "OFSC",
                "HV3",
                "OS3",
                "OS5",
                "OS0",
                "OG",
                "IDALE",
                "IDANE",
                "HV70E",
                "OFF",
                "OFS5S",
                "EFC",
                "OU3",
                "SOFS",
                "NLB",
                "OFS5C",
                "WFU0",
                "EF10",
                "OU0",
                "OFS50",
                "OU5",
                "HV7",
                "SCHD",
                "OG3",
                "SCHL",
                "OG0",
                "FBRE",
                "DFR0",
                "DFRC",
                "WKENC",
                "SSSUC",
                "DSR",
                "DSR0",
                "APSN",
                "DSRC",
                "OCGE0",
                "DTW",
                "SSSU5",
                "SSSU3",
                "SSSU0",
                "OCGEC",
                "OCGE3",
                "OCGE",
                "WH3E",
                "ONT3",
                "OFA",
                "OCGE5",
                "OFS5N",
                "OFMC",
                "SOFC",
                "DAEU",
                "WFU5",
                "WFU3",
                "HV3N",
                "OFSN",
                "OFUN",
                "OFSN0",
                "OFSN3",
                "OFUN0",
                "OFSN5",
                "30FS",
                "ALCT",
                "WFU",
                "3OFSC",
                "3OFS0",
                "3OFS3",
                "3ＯＦＳ5",
                "OFUN3",
                "OFUN5",
                "HVE6",
                "ONT5",
                "DAESI",
                "PT",
                "PTC",
                "PT3",
                "PT5",
                "PT0",
                "TEW",
                "EFN7",
                "EFN6",
                "EFN10",
                "PT3N",
                "WTWS",
                "WTWSC",
                "WTWS3",
                "WTWF",
                "SOFSN",
                "SOFSN3",
                "OCGEN",
                "OCGENC",
                "OCGEN0",
                "OCGEN3",
                "OCGEN5",
                "TW1N",
                "WWS",
                "TW1N3",
                "TW1N5",
                "TW1NC",
                "HVE7",
                "DAEF",
                "DAEFM",
                "ALCT5",
                "IDFC",
                "IDFCM",
                "IDFCD",
                "JSR",
                "SOFF",
                "WFSI",
                "WFSI3",
                "HVF",
                "DFRN",
                "DFRNC",
                "DFRN0",
                "DFRN5",
                "NOKR",
                "DSRN0",
                "ALCF",
                "POS",
                "POS5",
                "POS3",
                "POS0",
                "POF",
                "POF5",
                "POF3",
                "POF0",
                "WKENG",
                "WKEN3",
                "OGN3",
                "OGN5",
                "OGN0",
                "WFSIC",
                "EP",
                "PCTGM",
                "NDTW",
                "GPW",
                "GPW3",
                "GPW5",
                "GPW0",
                "FBRE0",
                "OFF3",
                "EFCN",
                "PTCN",
                "DTSI",
                "DTSC",
                "EPN",
                "APS2",
                "3OFS7",
                "DSRNN",
                "ALCTN",
                "DSRN5",
                "ＮＫ",
                "NOK",
                "PCTG",
                "ＤＳＲＮＣ"
            });
            DrpColoarantSelect1.SelectedIndex = 0;
            /// <summary>
            /// 着色剤2のドロップダウン
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            DrpColoarantSelect2.Items.AddRange(new object[] {
                "",
                "EMPTY",
                "3OFSK",
                "993OFS",
                "WELMｵｰｶｰ",
                "WEｵｰｶｰ",
                "WEﾌﾞﾗｯｸ",
                "WEｲﾝﾃﾞｱﾝ",
                "WEﾊﾟ-ﾏﾈY",
                "ＷＥシャニンＢシ",
                "WEｼﾝｶRN",
                "WEｸﾞﾘｰﾝ",
                "WEｼｬﾆﾝBN",
                "OFS",
                "OFU",
                "TW1",
                "SSSU",
                "HVE",
                "EF6",
                "EF7",
                "WKEN",
                "DFR",
                "OFUC",
                "OFSC",
                "HV3",
                "OS3",
                "OS5",
                "OS0",
                "OG",
                "IDALE",
                "IDANE",
                "HV70E",
                "OFF",
                "OFS5S",
                "EFC",
                "OU3",
                "SOFS",
                "NLB",
                "OFS5C",
                "WFU0",
                "EF10",
                "OU0",
                "OFS50",
                "OU5",
                "HV7",
                "SCHD",
                "OG3",
                "SCHL",
                "OG0",
                "FBRE",
                "DFR0",
                "DFRC",
                "WKENC",
                "SSSUC",
                "DSR",
                "DSR0",
                "APSN",
                "DSRC",
                "OCGE0",
                "DTW",
                "SSSU5",
                "SSSU3",
                "SSSU0",
                "OCGEC",
                "OCGE3",
                "OCGE",
                "WH3E",
                "ONT3",
                "OFA",
                "OCGE5",
                "OFS5N",
                "OFMC",
                "SOFC",
                "DAEU",
                "WFU5",
                "WFU3",
                "HV3N",
                "OFSN",
                "OFUN",
                "OFSN0",
                "OFSN3",
                "OFUN0",
                "OFSN5",
                "30FS",
                "ALCT",
                "WFU",
                "3OFSC",
                "3OFS0",
                "3OFS3",
                "3ＯＦＳ5",
                "OFUN3",
                "OFUN5",
                "HVE6",
                "ONT5",
                "DAESI",
                "PT",
                "PTC",
                "PT3",
                "PT5",
                "PT0",
                "TEW",
                "EFN7",
                "EFN6",
                "EFN10",
                "PT3N",
                "WTWS",
                "WTWSC",
                "WTWS3",
                "WTWF",
                "SOFSN",
                "SOFSN3",
                "OCGEN",
                "OCGENC",
                "OCGEN0",
                "OCGEN3",
                "OCGEN5",
                "TW1N",
                "WWS",
                "TW1N3",
                "TW1N5",
                "TW1NC",
                "HVE7",
                "DAEF",
                "DAEFM",
                "ALCT5",
                "IDFC",
                "IDFCM",
                "IDFCD",
                "JSR",
                "SOFF",
                "WFSI",
                "WFSI3",
                "HVF",
                "DFRN",
                "DFRNC",
                "DFRN0",
                "DFRN5",
                "NOKR",
                "DSRN0",
                "ALCF",
                "POS",
                "POS5",
                "POS3",
                "POS0",
                "POF",
                "POF5",
                "POF3",
                "POF0",
                "WKENG",
                "WKEN3",
                "OGN3",
                "OGN5",
                "OGN0",
                "WFSIC",
                "EP",
                "PCTGM",
                "NDTW",
                "GPW",
                "GPW3",
                "GPW5",
                "GPW0",
                "FBRE0",
                "OFF3",
                "EFCN",
                "PTCN",
                "DTSI",
                "DTSC",
                "EPN",
                "APS2",
                "3OFS7",
                "DSRNN",
                "ALCTN",
                "DSRN5",
                "ＮＫ",
                "NOK",
                "PCTG",
                "ＤＳＲＮＣ"
            });
            DrpColoarantSelect2.SelectedIndex = 0;
            /// <summary>
            /// 着色剤3のドロップダウン
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            DrpColoarantSelect3.Items.AddRange(new object[] {
                "",
                "EMPTY",
                "3OFSK",
                "993OFS",
                "WELMｵｰｶｰ",
                "WEｵｰｶｰ",
                "WEﾌﾞﾗｯｸ",
                "WEｲﾝﾃﾞｱﾝ",
                "WEﾊﾟ-ﾏﾈY",
                "ＷＥシャニンＢシ",
                "WEｼﾝｶRN",
                "WEｸﾞﾘｰﾝ",
                "WEｼｬﾆﾝBN",
                "OFS",
                "OFU",
                "TW1",
                "SSSU",
                "HVE",
                "EF6",
                "EF7",
                "WKEN",
                "DFR",
                "OFUC",
                "OFSC",
                "HV3",
                "OS3",
                "OS5",
                "OS0",
                "OG",
                "IDALE",
                "IDANE",
                "HV70E",
                "OFF",
                "OFS5S",
                "EFC",
                "OU3",
                "SOFS",
                "NLB",
                "OFS5C",
                "WFU0",
                "EF10",
                "OU0",
                "OFS50",
                "OU5",
                "HV7",
                "SCHD",
                "OG3",
                "SCHL",
                "OG0",
                "FBRE",
                "DFR0",
                "DFRC",
                "WKENC",
                "SSSUC",
                "DSR",
                "DSR0",
                "APSN",
                "DSRC",
                "OCGE0",
                "DTW",
                "SSSU5",
                "SSSU3",
                "SSSU0",
                "OCGEC",
                "OCGE3",
                "OCGE",
                "WH3E",
                "ONT3",
                "OFA",
                "OCGE5",
                "OFS5N",
                "OFMC",
                "SOFC",
                "DAEU",
                "WFU5",
                "WFU3",
                "HV3N",
                "OFSN",
                "OFUN",
                "OFSN0",
                "OFSN3",
                "OFUN0",
                "OFSN5",
                "30FS",
                "ALCT",
                "WFU",
                "3OFSC",
                "3OFS0",
                "3OFS3",
                "3ＯＦＳ5",
                "OFUN3",
                "OFUN5",
                "HVE6",
                "ONT5",
                "DAESI",
                "PT",
                "PTC",
                "PT3",
                "PT5",
                "PT0",
                "TEW",
                "EFN7",
                "EFN6",
                "EFN10",
                "PT3N",
                "WTWS",
                "WTWSC",
                "WTWS3",
                "WTWF",
                "SOFSN",
                "SOFSN3",
                "OCGEN",
                "OCGENC",
                "OCGEN0",
                "OCGEN3",
                "OCGEN5",
                "TW1N",
                "WWS",
                "TW1N3",
                "TW1N5",
                "TW1NC",
                "HVE7",
                "DAEF",
                "DAEFM",
                "ALCT5",
                "IDFC",
                "IDFCM",
                "IDFCD",
                "JSR",
                "SOFF",
                "WFSI",
                "WFSI3",
                "HVF",
                "DFRN",
                "DFRNC",
                "DFRN0",
                "DFRN5",
                "NOKR",
                "DSRN0",
                "ALCF",
                "POS",
                "POS5",
                "POS3",
                "POS0",
                "POF",
                "POF5",
                "POF3",
                "POF0",
                "WKENG",
                "WKEN3",
                "OGN3",
                "OGN5",
                "OGN0",
                "WFSIC",
                "EP",
                "PCTGM",
                "NDTW",
                "GPW",
                "GPW3",
                "GPW5",
                "GPW0",
                "FBRE0",
                "OFF3",
                "EFCN",
                "PTCN",
                "DTSI",
                "DTSC",
                "EPN",
                "APS2",
                "3OFS7",
                "DSRNN",
                "ALCTN",
                "DSRN5",
                "ＮＫ",
                "NOK",
                "PCTG",
                "ＤＳＲＮＣ"
            });
            DrpColoarantSelect3.SelectedIndex = 0;
            /// <summary>
            /// 着色剤4のドロップダウン
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            DrpColoarantSelect4.Items.AddRange(new object[] {
                "",
                "EMPTY",
                "3OFSK",
                "993OFS",
                "WELMｵｰｶｰ",
                "WEｵｰｶｰ",
                "WEﾌﾞﾗｯｸ",
                "WEｲﾝﾃﾞｱﾝ",
                "WEﾊﾟ-ﾏﾈY",
                "ＷＥシャニンＢシ",
                "WEｼﾝｶRN",
                "WEｸﾞﾘｰﾝ",
                "WEｼｬﾆﾝBN",
                "OFS",
                "OFU",
                "TW1",
                "SSSU",
                "HVE",
                "EF6",
                "EF7",
                "WKEN",
                "DFR",
                "OFUC",
                "OFSC",
                "HV3",
                "OS3",
                "OS5",
                "OS0",
                "OG",
                "IDALE",
                "IDANE",
                "HV70E",
                "OFF",
                "OFS5S",
                "EFC",
                "OU3",
                "SOFS",
                "NLB",
                "OFS5C",
                "WFU0",
                "EF10",
                "OU0",
                "OFS50",
                "OU5",
                "HV7",
                "SCHD",
                "OG3",
                "SCHL",
                "OG0",
                "FBRE",
                "DFR0",
                "DFRC",
                "WKENC",
                "SSSUC",
                "DSR",
                "DSR0",
                "APSN",
                "DSRC",
                "OCGE0",
                "DTW",
                "SSSU5",
                "SSSU3",
                "SSSU0",
                "OCGEC",
                "OCGE3",
                "OCGE",
                "WH3E",
                "ONT3",
                "OFA",
                "OCGE5",
                "OFS5N",
                "OFMC",
                "SOFC",
                "DAEU",
                "WFU5",
                "WFU3",
                "HV3N",
                "OFSN",
                "OFUN",
                "OFSN0",
                "OFSN3",
                "OFUN0",
                "OFSN5",
                "30FS",
                "ALCT",
                "WFU",
                "3OFSC",
                "3OFS0",
                "3OFS3",
                "3ＯＦＳ5",
                "OFUN3",
                "OFUN5",
                "HVE6",
                "ONT5",
                "DAESI",
                "PT",
                "PTC",
                "PT3",
                "PT5",
                "PT0",
                "TEW",
                "EFN7",
                "EFN6",
                "EFN10",
                "PT3N",
                "WTWS",
                "WTWSC",
                "WTWS3",
                "WTWF",
                "SOFSN",
                "SOFSN3",
                "OCGEN",
                "OCGENC",
                "OCGEN0",
                "OCGEN3",
                "OCGEN5",
                "TW1N",
                "WWS",
                "TW1N3",
                "TW1N5",
                "TW1NC",
                "HVE7",
                "DAEF",
                "DAEFM",
                "ALCT5",
                "IDFC",
                "IDFCM",
                "IDFCD",
                "JSR",
                "SOFF",
                "WFSI",
                "WFSI3",
                "HVF",
                "DFRN",
                "DFRNC",
                "DFRN0",
                "DFRN5",
                "NOKR",
                "DSRN0",
                "ALCF",
                "POS",
                "POS5",
                "POS3",
                "POS0",
                "POF",
                "POF5",
                "POF3",
                "POF0",
                "WKENG",
                "WKEN3",
                "OGN3",
                "OGN5",
                "OGN0",
                "WFSIC",
                "EP",
                "PCTGM",
                "NDTW",
                "GPW",
                "GPW3",
                "GPW5",
                "GPW0",
                "FBRE0",
                "OFF3",
                "EFCN",
                "PTCN",
                "DTSI",
                "DTSC",
                "EPN",
                "APS2",
                "3OFS7",
                "DSRNN",
                "ALCTN",
                "DSRN5",
                "ＮＫ",
                "NOK",
                "PCTG",
                "ＤＳＲＮＣ"
            });
            DrpColoarantSelect4.SelectedIndex = 0;
            /// <summary>
            /// 着色剤5のドロップダウン
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            DrpColoarantSelect5.Items.AddRange(new object[] {
                "",
                "EMPTY",
                "3OFSK",
                "993OFS",
                "WELMｵｰｶｰ",
                "WEｵｰｶｰ",
                "WEﾌﾞﾗｯｸ",
                "WEｲﾝﾃﾞｱﾝ",
                "WEﾊﾟ-ﾏﾈY",
                "ＷＥシャニンＢシ",
                "WEｼﾝｶRN",
                "WEｸﾞﾘｰﾝ",
                "WEｼｬﾆﾝBN",
                "OFS",
                "OFU",
                "TW1",
                "SSSU",
                "HVE",
                "EF6",
                "EF7",
                "WKEN",
                "DFR",
                "OFUC",
                "OFSC",
                "HV3",
                "OS3",
                "OS5",
                "OS0",
                "OG",
                "IDALE",
                "IDANE",
                "HV70E",
                "OFF",
                "OFS5S",
                "EFC",
                "OU3",
                "SOFS",
                "NLB",
                "OFS5C",
                "WFU0",
                "EF10",
                "OU0",
                "OFS50",
                "OU5",
                "HV7",
                "SCHD",
                "OG3",
                "SCHL",
                "OG0",
                "FBRE",
                "DFR0",
                "DFRC",
                "WKENC",
                "SSSUC",
                "DSR",
                "DSR0",
                "APSN",
                "DSRC",
                "OCGE0",
                "DTW",
                "SSSU5",
                "SSSU3",
                "SSSU0",
                "OCGEC",
                "OCGE3",
                "OCGE",
                "WH3E",
                "ONT3",
                "OFA",
                "OCGE5",
                "OFS5N",
                "OFMC",
                "SOFC",
                "DAEU",
                "WFU5",
                "WFU3",
                "HV3N",
                "OFSN",
                "OFUN",
                "OFSN0",
                "OFSN3",
                "OFUN0",
                "OFSN5",
                "30FS",
                "ALCT",
                "WFU",
                "3OFSC",
                "3OFS0",
                "3OFS3",
                "3ＯＦＳ5",
                "OFUN3",
                "OFUN5",
                "HVE6",
                "ONT5",
                "DAESI",
                "PT",
                "PTC",
                "PT3",
                "PT5",
                "PT0",
                "TEW",
                "EFN7",
                "EFN6",
                "EFN10",
                "PT3N",
                "WTWS",
                "WTWSC",
                "WTWS3",
                "WTWF",
                "SOFSN",
                "SOFSN3",
                "OCGEN",
                "OCGENC",
                "OCGEN0",
                "OCGEN3",
                "OCGEN5",
                "TW1N",
                "WWS",
                "TW1N3",
                "TW1N5",
                "TW1NC",
                "HVE7",
                "DAEF",
                "DAEFM",
                "ALCT5",
                "IDFC",
                "IDFCM",
                "IDFCD",
                "JSR",
                "SOFF",
                "WFSI",
                "WFSI3",
                "HVF",
                "DFRN",
                "DFRNC",
                "DFRN0",
                "DFRN5",
                "NOKR",
                "DSRN0",
                "ALCF",
                "POS",
                "POS5",
                "POS3",
                "POS0",
                "POF",
                "POF5",
                "POF3",
                "POF0",
                "WKENG",
                "WKEN3",
                "OGN3",
                "OGN5",
                "OGN0",
                "WFSIC",
                "EP",
                "PCTGM",
                "NDTW",
                "GPW",
                "GPW3",
                "GPW5",
                "GPW0",
                "FBRE0",
                "OFF3",
                "EFCN",
                "PTCN",
                "DTSI",
                "DTSC",
                "EPN",
                "APS2",
                "3OFS7",
                "DSRNN",
                "ALCTN",
                "DSRN5",
                "ＮＫ",
                "NOK",
                "PCTG",
                "ＤＳＲＮＣ"
            });
            DrpColoarantSelect5.SelectedIndex = 0;
            /// <summary>
            /// 着色剤6のドロップダウン
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            DrpColoarantSelect6.Items.AddRange(new object[] {
                "",
                "EMPTY",
                "3OFSK",
                "993OFS",
                "WELMｵｰｶｰ",
                "WEｵｰｶｰ",
                "WEﾌﾞﾗｯｸ",
                "WEｲﾝﾃﾞｱﾝ",
                "WEﾊﾟ-ﾏﾈY",
                "ＷＥシャニンＢシ",
                "WEｼﾝｶRN",
                "WEｸﾞﾘｰﾝ",
                "WEｼｬﾆﾝBN",
                "OFS",
                "OFU",
                "TW1",
                "SSSU",
                "HVE",
                "EF6",
                "EF7",
                "WKEN",
                "DFR",
                "OFUC",
                "OFSC",
                "HV3",
                "OS3",
                "OS5",
                "OS0",
                "OG",
                "IDALE",
                "IDANE",
                "HV70E",
                "OFF",
                "OFS5S",
                "EFC",
                "OU3",
                "SOFS",
                "NLB",
                "OFS5C",
                "WFU0",
                "EF10",
                "OU0",
                "OFS50",
                "OU5",
                "HV7",
                "SCHD",
                "OG3",
                "SCHL",
                "OG0",
                "FBRE",
                "DFR0",
                "DFRC",
                "WKENC",
                "SSSUC",
                "DSR",
                "DSR0",
                "APSN",
                "DSRC",
                "OCGE0",
                "DTW",
                "SSSU5",
                "SSSU3",
                "SSSU0",
                "OCGEC",
                "OCGE3",
                "OCGE",
                "WH3E",
                "ONT3",
                "OFA",
                "OCGE5",
                "OFS5N",
                "OFMC",
                "SOFC",
                "DAEU",
                "WFU5",
                "WFU3",
                "HV3N",
                "OFSN",
                "OFUN",
                "OFSN0",
                "OFSN3",
                "OFUN0",
                "OFSN5",
                "30FS",
                "ALCT",
                "WFU",
                "3OFSC",
                "3OFS0",
                "3OFS3",
                "3ＯＦＳ5",
                "OFUN3",
                "OFUN5",
                "HVE6",
                "ONT5",
                "DAESI",
                "PT",
                "PTC",
                "PT3",
                "PT5",
                "PT0",
                "TEW",
                "EFN7",
                "EFN6",
                "EFN10",
                "PT3N",
                "WTWS",
                "WTWSC",
                "WTWS3",
                "WTWF",
                "SOFSN",
                "SOFSN3",
                "OCGEN",
                "OCGENC",
                "OCGEN0",
                "OCGEN3",
                "OCGEN5",
                "TW1N",
                "WWS",
                "TW1N3",
                "TW1N5",
                "TW1NC",
                "HVE7",
                "DAEF",
                "DAEFM",
                "ALCT5",
                "IDFC",
                "IDFCM",
                "IDFCD",
                "JSR",
                "SOFF",
                "WFSI",
                "WFSI3",
                "HVF",
                "DFRN",
                "DFRNC",
                "DFRN0",
                "DFRN5",
                "NOKR",
                "DSRN0",
                "ALCF",
                "POS",
                "POS5",
                "POS3",
                "POS0",
                "POF",
                "POF5",
                "POF3",
                "POF0",
                "WKENG",
                "WKEN3",
                "OGN3",
                "OGN5",
                "OGN0",
                "WFSIC",
                "EP",
                "PCTGM",
                "NDTW",
                "GPW",
                "GPW3",
                "GPW5",
                "GPW0",
                "FBRE0",
                "OFF3",
                "EFCN",
                "PTCN",
                "DTSI",
                "DTSC",
                "EPN",
                "APS2",
                "3OFS7",
                "DSRNN",
                "ALCTN",
                "DSRN5",
                "ＮＫ",
                "NOK",
                "PCTG",
                "ＤＳＲＮＣ"
            });
            DrpColoarantSelect6.SelectedIndex = 0;
            /// <summary>
            /// 着色剤7のドロップダウン
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            DrpColoarantSelect7.Items.AddRange(new object[] {
                "",
                "EMPTY",
                "3OFSK",
                "993OFS",
                "WELMｵｰｶｰ",
                "WEｵｰｶｰ",
                "WEﾌﾞﾗｯｸ",
                "WEｲﾝﾃﾞｱﾝ",
                "WEﾊﾟ-ﾏﾈY",
                "ＷＥシャニンＢシ",
                "WEｼﾝｶRN",
                "WEｸﾞﾘｰﾝ",
                "WEｼｬﾆﾝBN",
                "OFS",
                "OFU",
                "TW1",
                "SSSU",
                "HVE",
                "EF6",
                "EF7",
                "WKEN",
                "DFR",
                "OFUC",
                "OFSC",
                "HV3",
                "OS3",
                "OS5",
                "OS0",
                "OG",
                "IDALE",
                "IDANE",
                "HV70E",
                "OFF",
                "OFS5S",
                "EFC",
                "OU3",
                "SOFS",
                "NLB",
                "OFS5C",
                "WFU0",
                "EF10",
                "OU0",
                "OFS50",
                "OU5",
                "HV7",
                "SCHD",
                "OG3",
                "SCHL",
                "OG0",
                "FBRE",
                "DFR0",
                "DFRC",
                "WKENC",
                "SSSUC",
                "DSR",
                "DSR0",
                "APSN",
                "DSRC",
                "OCGE0",
                "DTW",
                "SSSU5",
                "SSSU3",
                "SSSU0",
                "OCGEC",
                "OCGE3",
                "OCGE",
                "WH3E",
                "ONT3",
                "OFA",
                "OCGE5",
                "OFS5N",
                "OFMC",
                "SOFC",
                "DAEU",
                "WFU5",
                "WFU3",
                "HV3N",
                "OFSN",
                "OFUN",
                "OFSN0",
                "OFSN3",
                "OFUN0",
                "OFSN5",
                "30FS",
                "ALCT",
                "WFU",
                "3OFSC",
                "3OFS0",
                "3OFS3",
                "3ＯＦＳ5",
                "OFUN3",
                "OFUN5",
                "HVE6",
                "ONT5",
                "DAESI",
                "PT",
                "PTC",
                "PT3",
                "PT5",
                "PT0",
                "TEW",
                "EFN7",
                "EFN6",
                "EFN10",
                "PT3N",
                "WTWS",
                "WTWSC",
                "WTWS3",
                "WTWF",
                "SOFSN",
                "SOFSN3",
                "OCGEN",
                "OCGENC",
                "OCGEN0",
                "OCGEN3",
                "OCGEN5",
                "TW1N",
                "WWS",
                "TW1N3",
                "TW1N5",
                "TW1NC",
                "HVE7",
                "DAEF",
                "DAEFM",
                "ALCT5",
                "IDFC",
                "IDFCM",
                "IDFCD",
                "JSR",
                "SOFF",
                "WFSI",
                "WFSI3",
                "HVF",
                "DFRN",
                "DFRNC",
                "DFRN0",
                "DFRN5",
                "NOKR",
                "DSRN0",
                "ALCF",
                "POS",
                "POS5",
                "POS3",
                "POS0",
                "POF",
                "POF5",
                "POF3",
                "POF0",
                "WKENG",
                "WKEN3",
                "OGN3",
                "OGN5",
                "OGN0",
                "WFSIC",
                "EP",
                "PCTGM",
                "NDTW",
                "GPW",
                "GPW3",
                "GPW5",
                "GPW0",
                "FBRE0",
                "OFF3",
                "EFCN",
                "PTCN",
                "DTSI",
                "DTSC",
                "EPN",
                "APS2",
                "3OFS7",
                "DSRNN",
                "ALCTN",
                "DSRN5",
                "ＮＫ",
                "NOK",
                "PCTG",
                "ＤＳＲＮＣ"
            });
            DrpColoarantSelect7.SelectedIndex = 0;
            /// <summary>
            /// 着色剤8のドロップダウン
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            DrpColoarantSelect8.Items.AddRange(new object[] {
                "",
                "EMPTY",
                "3OFSK",
                "993OFS",
                "WELMｵｰｶｰ",
                "WEｵｰｶｰ",
                "WEﾌﾞﾗｯｸ",
                "WEｲﾝﾃﾞｱﾝ",
                "WEﾊﾟ-ﾏﾈY",
                "ＷＥシャニンＢシ",
                "WEｼﾝｶRN",
                "WEｸﾞﾘｰﾝ",
                "WEｼｬﾆﾝBN",
                "OFS",
                "OFU",
                "TW1",
                "SSSU",
                "HVE",
                "EF6",
                "EF7",
                "WKEN",
                "DFR",
                "OFUC",
                "OFSC",
                "HV3",
                "OS3",
                "OS5",
                "OS0",
                "OG",
                "IDALE",
                "IDANE",
                "HV70E",
                "OFF",
                "OFS5S",
                "EFC",
                "OU3",
                "SOFS",
                "NLB",
                "OFS5C",
                "WFU0",
                "EF10",
                "OU0",
                "OFS50",
                "OU5",
                "HV7",
                "SCHD",
                "OG3",
                "SCHL",
                "OG0",
                "FBRE",
                "DFR0",
                "DFRC",
                "WKENC",
                "SSSUC",
                "DSR",
                "DSR0",
                "APSN",
                "DSRC",
                "OCGE0",
                "DTW",
                "SSSU5",
                "SSSU3",
                "SSSU0",
                "OCGEC",
                "OCGE3",
                "OCGE",
                "WH3E",
                "ONT3",
                "OFA",
                "OCGE5",
                "OFS5N",
                "OFMC",
                "SOFC",
                "DAEU",
                "WFU5",
                "WFU3",
                "HV3N",
                "OFSN",
                "OFUN",
                "OFSN0",
                "OFSN3",
                "OFUN0",
                "OFSN5",
                "30FS",
                "ALCT",
                "WFU",
                "3OFSC",
                "3OFS0",
                "3OFS3",
                "3ＯＦＳ5",
                "OFUN3",
                "OFUN5",
                "HVE6",
                "ONT5",
                "DAESI",
                "PT",
                "PTC",
                "PT3",
                "PT5",
                "PT0",
                "TEW",
                "EFN7",
                "EFN6",
                "EFN10",
                "PT3N",
                "WTWS",
                "WTWSC",
                "WTWS3",
                "WTWF",
                "SOFSN",
                "SOFSN3",
                "OCGEN",
                "OCGENC",
                "OCGEN0",
                "OCGEN3",
                "OCGEN5",
                "TW1N",
                "WWS",
                "TW1N3",
                "TW1N5",
                "TW1NC",
                "HVE7",
                "DAEF",
                "DAEFM",
                "ALCT5",
                "IDFC",
                "IDFCM",
                "IDFCD",
                "JSR",
                "SOFF",
                "WFSI",
                "WFSI3",
                "HVF",
                "DFRN",
                "DFRNC",
                "DFRN0",
                "DFRN5",
                "NOKR",
                "DSRN0",
                "ALCF",
                "POS",
                "POS5",
                "POS3",
                "POS0",
                "POF",
                "POF5",
                "POF3",
                "POF0",
                "WKENG",
                "WKEN3",
                "OGN3",
                "OGN5",
                "OGN0",
                "WFSIC",
                "EP",
                "PCTGM",
                "NDTW",
                "GPW",
                "GPW3",
                "GPW5",
                "GPW0",
                "FBRE0",
                "OFF3",
                "EFCN",
                "PTCN",
                "DTSI",
                "DTSC",
                "EPN",
                "APS2",
                "3OFS7",
                "DSRNN",
                "ALCTN",
                "DSRN5",
                "ＮＫ",
                "NOK",
                "PCTG",
                "ＤＳＲＮＣ"
            });
            DrpColoarantSelect8.SelectedIndex = 0;
            /// <summary>
            /// 着色剤9のドロップダウン
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            DrpColoarantSelect9.Items.AddRange(new object[] {
                "",
                "EMPTY",
                "3OFSK",
                "993OFS",
                "WELMｵｰｶｰ",
                "WEｵｰｶｰ",
                "WEﾌﾞﾗｯｸ",
                "WEｲﾝﾃﾞｱﾝ",
                "WEﾊﾟ-ﾏﾈY",
                "ＷＥシャニンＢシ",
                "WEｼﾝｶRN",
                "WEｸﾞﾘｰﾝ",
                "WEｼｬﾆﾝBN",
                "OFS",
                "OFU",
                "TW1",
                "SSSU",
                "HVE",
                "EF6",
                "EF7",
                "WKEN",
                "DFR",
                "OFUC",
                "OFSC",
                "HV3",
                "OS3",
                "OS5",
                "OS0",
                "OG",
                "IDALE",
                "IDANE",
                "HV70E",
                "OFF",
                "OFS5S",
                "EFC",
                "OU3",
                "SOFS",
                "NLB",
                "OFS5C",
                "WFU0",
                "EF10",
                "OU0",
                "OFS50",
                "OU5",
                "HV7",
                "SCHD",
                "OG3",
                "SCHL",
                "OG0",
                "FBRE",
                "DFR0",
                "DFRC",
                "WKENC",
                "SSSUC",
                "DSR",
                "DSR0",
                "APSN",
                "DSRC",
                "OCGE0",
                "DTW",
                "SSSU5",
                "SSSU3",
                "SSSU0",
                "OCGEC",
                "OCGE3",
                "OCGE",
                "WH3E",
                "ONT3",
                "OFA",
                "OCGE5",
                "OFS5N",
                "OFMC",
                "SOFC",
                "DAEU",
                "WFU5",
                "WFU3",
                "HV3N",
                "OFSN",
                "OFUN",
                "OFSN0",
                "OFSN3",
                "OFUN0",
                "OFSN5",
                "30FS",
                "ALCT",
                "WFU",
                "3OFSC",
                "3OFS0",
                "3OFS3",
                "3ＯＦＳ5",
                "OFUN3",
                "OFUN5",
                "HVE6",
                "ONT5",
                "DAESI",
                "PT",
                "PTC",
                "PT3",
                "PT5",
                "PT0",
                "TEW",
                "EFN7",
                "EFN6",
                "EFN10",
                "PT3N",
                "WTWS",
                "WTWSC",
                "WTWS3",
                "WTWF",
                "SOFSN",
                "SOFSN3",
                "OCGEN",
                "OCGENC",
                "OCGEN0",
                "OCGEN3",
                "OCGEN5",
                "TW1N",
                "WWS",
                "TW1N3",
                "TW1N5",
                "TW1NC",
                "HVE7",
                "DAEF",
                "DAEFM",
                "ALCT5",
                "IDFC",
                "IDFCM",
                "IDFCD",
                "JSR",
                "SOFF",
                "WFSI",
                "WFSI3",
                "HVF",
                "DFRN",
                "DFRNC",
                "DFRN0",
                "DFRN5",
                "NOKR",
                "DSRN0",
                "ALCF",
                "POS",
                "POS5",
                "POS3",
                "POS0",
                "POF",
                "POF5",
                "POF3",
                "POF0",
                "WKENG",
                "WKEN3",
                "OGN3",
                "OGN5",
                "OGN0",
                "WFSIC",
                "EP",
                "PCTGM",
                "NDTW",
                "GPW",
                "GPW3",
                "GPW5",
                "GPW0",
                "FBRE0",
                "OFF3",
                "EFCN",
                "PTCN",
                "DTSI",
                "DTSC",
                "EPN",
                "APS2",
                "3OFS7",
                "DSRNN",
                "ALCTN",
                "DSRN5",
                "ＮＫ",
                "NOK",
                "PCTG",
                "ＤＳＲＮＣ"
            });
            DrpColoarantSelect9.SelectedIndex = 0;
            /// <summary>
            /// 着色剤10のドロップダウン
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            DrpColoarantSelect10.Items.AddRange(new object[] {
                "",
                "EMPTY",
                "3OFSK",
                "993OFS",
                "WELMｵｰｶｰ",
                "WEｵｰｶｰ",
                "WEﾌﾞﾗｯｸ",
                "WEｲﾝﾃﾞｱﾝ",
                "WEﾊﾟ-ﾏﾈY",
                "ＷＥシャニンＢシ",
                "WEｼﾝｶRN",
                "WEｸﾞﾘｰﾝ",
                "WEｼｬﾆﾝBN",
                "OFS",
                "OFU",
                "TW1",
                "SSSU",
                "HVE",
                "EF6",
                "EF7",
                "WKEN",
                "DFR",
                "OFUC",
                "OFSC",
                "HV3",
                "OS3",
                "OS5",
                "OS0",
                "OG",
                "IDALE",
                "IDANE",
                "HV70E",
                "OFF",
                "OFS5S",
                "EFC",
                "OU3",
                "SOFS",
                "NLB",
                "OFS5C",
                "WFU0",
                "EF10",
                "OU0",
                "OFS50",
                "OU5",
                "HV7",
                "SCHD",
                "OG3",
                "SCHL",
                "OG0",
                "FBRE",
                "DFR0",
                "DFRC",
                "WKENC",
                "SSSUC",
                "DSR",
                "DSR0",
                "APSN",
                "DSRC",
                "OCGE0",
                "DTW",
                "SSSU5",
                "SSSU3",
                "SSSU0",
                "OCGEC",
                "OCGE3",
                "OCGE",
                "WH3E",
                "ONT3",
                "OFA",
                "OCGE5",
                "OFS5N",
                "OFMC",
                "SOFC",
                "DAEU",
                "WFU5",
                "WFU3",
                "HV3N",
                "OFSN",
                "OFUN",
                "OFSN0",
                "OFSN3",
                "OFUN0",
                "OFSN5",
                "30FS",
                "ALCT",
                "WFU",
                "3OFSC",
                "3OFS0",
                "3OFS3",
                "3ＯＦＳ5",
                "OFUN3",
                "OFUN5",
                "HVE6",
                "ONT5",
                "DAESI",
                "PT",
                "PTC",
                "PT3",
                "PT5",
                "PT0",
                "TEW",
                "EFN7",
                "EFN6",
                "EFN10",
                "PT3N",
                "WTWS",
                "WTWSC",
                "WTWS3",
                "WTWF",
                "SOFSN",
                "SOFSN3",
                "OCGEN",
                "OCGENC",
                "OCGEN0",
                "OCGEN3",
                "OCGEN5",
                "TW1N",
                "WWS",
                "TW1N3",
                "TW1N5",
                "TW1NC",
                "HVE7",
                "DAEF",
                "DAEFM",
                "ALCT5",
                "IDFC",
                "IDFCM",
                "IDFCD",
                "JSR",
                "SOFF",
                "WFSI",
                "WFSI3",
                "HVF",
                "DFRN",
                "DFRNC",
                "DFRN0",
                "DFRN5",
                "NOKR",
                "DSRN0",
                "ALCF",
                "POS",
                "POS5",
                "POS3",
                "POS0",
                "POF",
                "POF5",
                "POF3",
                "POF0",
                "WKENG",
                "WKEN3",
                "OGN3",
                "OGN5",
                "OGN0",
                "WFSIC",
                "EP",
                "PCTGM",
                "NDTW",
                "GPW",
                "GPW3",
                "GPW5",
                "GPW0",
                "FBRE0",
                "OFF3",
                "EFCN",
                "PTCN",
                "DTSI",
                "DTSC",
                "EPN",
                "APS2",
                "3OFS7",
                "DSRNN",
                "ALCTN",
                "DSRN5",
                "ＮＫ",
                "NOK",
                "PCTG",
                "ＤＳＲＮＣ"
            });
            DrpColoarantSelect10.SelectedIndex = 0;
        }
        #endregion

        private void GrpInfo_Enter(object sender, EventArgs e)
        {

        }
    }
}

