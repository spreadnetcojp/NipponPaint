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
using System.Data;
using System.Windows.Forms;
using System.Linq;
using NipponPaint.NpCommon;
using NipponPaint.NpCommon.Database;
using NipponPaint.NpCommon.FormControls;
using System.Text;
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
        private const string CancelNumber = "CANCEL";
        /// <summary>
        /// 製品コード　左
        /// </summary>
        private readonly string[] PRODUCT_CODE_LEFT = { "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "W" };
        /// <summary>
        /// 製品コード　右
        /// </summary>
        private readonly string[] PRODUCT_CODE_RIGHT = { "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };
        /// <summary>
        /// 取得データ１件の為、Indexは「0」
        /// </summary>
        private const int SELECT_DATE = 0;
        /// <summary>
        /// ドロップダウンの初期選択
        /// </summary>
        private const int SELECT_INDEX = 0;

        /// <summary>
        /// 投入缶の種類
        /// </summary>
        private enum RdoStatusChange
        {
            /// <summary>
            /// 空缶
            /// </summary>
            ChkEmptyCan,
            /// <summary>
            /// 充填缶
            /// </summary>
            ChkFilledCan,
            /// <summary>
            /// 修正缶
            /// </summary>
            ChkFixedCan,
        }
        /// <summary>
        /// 配合の状態
        /// </summary>
        private enum LastFormulaRelease
        {
            /// <summary>
            /// リリース前
            /// </summary>
            BeforeRelease,
            /// <summary>
            /// リリース
            /// </summary>
            Release,
            /// <summary>
            /// 修正
            /// </summary>
            FixedRelease,
        }

        /// <summary>
        /// 空缶
        /// </summary>
        private const string Chk_Empty_Can = "ChkEmptyCan";
        /// <summary>
        /// 充填缶
        /// </summary>
        private const string Chk_Filled_Can = "ChkFilledCan";
        /// <summary>
        /// 修正缶
        /// </summary>
        private const string Chk_Fixed_Can = "ChkFixedCan";
        /// <summary>
        /// 修正(入力不可)
        /// </summary>
        private const string CORRECTION = "correction";

        private const string InitialValue = "0.000";

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
                var inputCan = false;
                var status = 0;
                // 選択した項目によって更新しないカラムが発生する
                var UpdateFlg = true;
                if (string.IsNullOrEmpty(TxtPaintName.Value) || string.IsNullOrEmpty(TxtKanjiColorName.Value))
                {
                    // 色名及び品名が空の場合は処理をしない
                    Messages.ShowDialog(Sentence.Messages.PleaseInsertProductNameAndPaintName);
                    return;
                }
                if (string.IsNullOrEmpty(DrpBaseSelect.Text))
                {
                    // ベースが空の場合は処理をしない
                    Messages.ShowDialog(Sentence.Messages.SelectMaterialError);
                    return;
                }
                // データ取得
                var dt = GetData();
                // レコードがなければ実施しない
                if (dt[SELECT_DATE].Rows.Count < 1)
                {
                    Messages.ShowDialog(Sentence.Messages.NoOrderWithMatchingProductCode);
                    return;
                }
                // order_id取得
                var orderId = int.Parse(dt[SELECT_DATE].Rows[SELECT_DATE][NpCommon.Database.Sql.NpMain.Orders.COLUMN_ORDER_ID].ToString());
                // dtからformulaRelease取得かつ更新用に　+1
                var formulaRelease = int.Parse(dt[SELECT_DATE].Rows[SELECT_DATE][NpCommon.Database.Sql.NpMain.Orders.COLUMN_FORMULA_RELEASE].ToString()) + 1;
                // 選択している投入缶の種類取得
                var selectPutCan = SelectPutCan();
                // 選択した投入缶によって更新データ変更
                switch (selectPutCan)
                {
                    case RdoStatusChange.ChkEmptyCan:
                        break;
                    case RdoStatusChange.ChkFilledCan:
                        inputCan = true;
                        break;
                    case RdoStatusChange.ChkFixedCan:
                        switch (int.Parse(dt[SELECT_DATE].Rows[SELECT_DATE][NpCommon.Database.Sql.NpMain.Orders.COLUMN_STATUS].ToString()))
                        {
                            case (int)NpCommon.Database.Sql.NpMain.Orders.OrderStatus.WaitingForCCMformulation:
                                Messages.ShowDialog(Sentence.Messages.RejectedCorrectionForAnOrderWithNoFormula);
                                return;
                            default:
                                UpdateFlg = false;
                                break;
                        }
                        break;
                }
                var colorantCount = GetColorantsCount(orderId);
                // 総重量の取得
                var totalWeight = GetTotalWeight(dt[SELECT_DATE].Rows[SELECT_DATE][NpCommon.Database.Sql.NpMain.Orders.COLUMN_TOTAL_WEIGHT]);
                if (ChkEmptyCan.Checked && TxtBaseValue.Text == InitialValue)
                {
                    if (Messages.ShowDialog(Sentence.Messages.WhiteWeightZeroEmptyCanProceedAnyway) == DialogResult.No)
                    {
                        return;
                    }
                }
                if (int.Parse(dt[SELECT_DATE].Rows[SELECT_DATE][NpCommon.Database.Sql.NpMain.Orders.COLUMN_STATUS].ToString()) == NumUpDownCorrection.Value)
                {

                }
                // 取得したステータスによって更新するカラムを変更
                switch (int.Parse(dt[SELECT_DATE].Rows[SELECT_DATE][NpCommon.Database.Sql.NpMain.Orders.COLUMN_STATUS].ToString()))
                {
                    case (int)NpCommon.Database.Sql.NpMain.Orders.OrderStatus.WaitingForCCMformulation:
                        status = (int)NpCommon.Database.Sql.NpMain.Orders.OrderStatus.Ready;
                        break;
                    case (int)NpCommon.Database.Sql.NpMain.Orders.OrderStatus.Ready:
                    case (int)NpCommon.Database.Sql.NpMain.Orders.OrderStatus.TestCanInProgress:
                    case (int)NpCommon.Database.Sql.NpMain.Orders.OrderStatus.ManufacturingCansInProgress:
                        UpdateFlg = false;
                        break;
                    default:
                        UpdateFlg = false;
                        break;
                }
                var parameters = new List<ParameterItem>()
                {
                    new ParameterItem("@OrderId", orderId),
                    new ParameterItem("@Status", status),
                    new ParameterItem("@PaintName", TxtPaintName.Value),
                    new ParameterItem("@TintedColor", TxtColorFileName.Value),
                    new ParameterItem("@IndexNumber", TxtIndexNumber.Value),
                    new ParameterItem("@LineName", TxtLine.Value),
                    new ParameterItem("@FormulaRelease", formulaRelease),
                    new ParameterItem("@InputCan", inputCan),
                    new ParameterItem("@Revision", NumUpDownCorrection.Value),
                    new ParameterItem("@White_Code", DrpBaseSelect.Text),
                    new ParameterItem("@White_Weight", TxtBaseValue.Text),
                    new ParameterItem("@Colorant_1", DrpColoarantSelect1.Text),
                    new ParameterItem("@Weight_1", TxtColoarantValue1.Text),
                    new ParameterItem("@Colorant_2", DrpColoarantSelect2.Text),
                    new ParameterItem("@Weight_2", TxtColoarantValue2.Text),
                    new ParameterItem("@Colorant_3", DrpColoarantSelect3.Text),
                    new ParameterItem("@Weight_3", TxtColoarantValue3.Text),
                    new ParameterItem("@Colorant_4", DrpColoarantSelect4.Text),
                    new ParameterItem("@Weight_4", TxtColoarantValue4.Text),
                    new ParameterItem("@Colorant_5", DrpColoarantSelect5.Text),
                    new ParameterItem("@Weight_5", TxtColoarantValue5.Text),
                    new ParameterItem("@Colorant_6", DrpColoarantSelect6.Text),
                    new ParameterItem("@Weight_6", TxtColoarantValue6.Text),
                    new ParameterItem("@Colorant_7", DrpColoarantSelect7.Text),
                    new ParameterItem("@Weight_7", TxtColoarantValue7.Text),
                    new ParameterItem("@Colorant_8", DrpColoarantSelect8.Text),
                    new ParameterItem("@Weight_8", TxtColoarantValue8.Text),
                    new ParameterItem("@Colorant_9", DrpColoarantSelect9.Text),
                    new ParameterItem("@Weight_9", TxtColoarantValue9.Text),
                    new ParameterItem("@Colorant_10", DrpColoarantSelect10.Text),
                    new ParameterItem("@Weight_10", TxtColoarantValue10.Text),
                    new ParameterItem("@TotalWeight", totalWeight),
                    new ParameterItem("@CCMColorName", TxtKanjiColorName.Value),
                };
                using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.Yes, Log.ApplicationType.OrderManager))
                {
                    db.Execute(NpCommon.Database.Sql.NpMain.Orders.CCMSimulatorDataUpdate(UpdateFlg, colorantCount), parameters);
                    db.Commit();
                }
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
                // 投入缶の選択で充填缶を選択している場合キーパッドは開かない
                if (!TxtBaseValue.Enabled)
                {
                    TxtBaseValue.Text = InitialValue;
                    TxtBaseValue.ForeColor = System.Drawing.SystemColors.Window;
                    return;
                }
                KeyPadPush(TxtBaseValue);
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
                KeyPadPush(TxtColoarantValue1);
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
                KeyPadPush(TxtColoarantValue2);
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
                KeyPadPush(TxtColoarantValue3);
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
                KeyPadPush(TxtColoarantValue4);
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
                KeyPadPush(TxtColoarantValue5);
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
                KeyPadPush(TxtColoarantValue6);
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
                KeyPadPush(TxtColoarantValue7);
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
                KeyPadPush(TxtColoarantValue8);
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
                KeyPadPush(TxtColoarantValue9);
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
                KeyPadPush(TxtColoarantValue10);
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
                FrmKeyPad frmKeyPad = new FrmKeyPad(((TextBoxBase)sender).Text);
                frmKeyPad.ShowDialog();
                // Cancelが押された場合はTextを書き換えない
                if (frmKeyPad.NumLabel.Text != CancelNumber)
                {
                    ((TextBoxBase)sender).Text = Funcs.KeyPadPush(frmKeyPad.NumLabel.Text);
                }
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
            this.ChkFixedCan.CheckedChanged += new System.EventHandler(this.ChkFixedCan_CheckedChanged);
            this.ChkFilledCan.CheckedChanged += new System.EventHandler(this.ChkFilledCan_CheckedChanged);
            this.ChkEmptyCan.CheckedChanged += new System.EventHandler(this.ChkEmptyCan_CheckedChanged);

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
            DrpProductCodeLeft.DropDown.Items.AddRange(PRODUCT_CODE_LEFT);
            /// <summary>
            /// 製品コード(右)のドロップダウン(コンボボックス)
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            DrpProductCodeRight.Items.AddRange(PRODUCT_CODE_RIGHT);
            using (var db = new SqlBase(SqlBase.DatabaseKind.IOSSUP, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
            {
                /// <summary>
                /// ベースのドロップダウン
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                DrpBaseSelect.Items.AddRange(GetWhiteCode(db));
                DrpBaseSelect.SelectedIndex = SELECT_INDEX;
                /// <summary>
                /// 着色剤1のドロップダウン
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                DrpColoarantSelect1.Items.AddRange(GetWhiteCode(db));
                DrpColoarantSelect1.SelectedIndex = SELECT_INDEX;
                /// <summary>
                /// 着色剤2のドロップダウン
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                DrpColoarantSelect2.Items.AddRange(GetWhiteCode(db));
                DrpColoarantSelect2.SelectedIndex = SELECT_INDEX;
                /// <summary>
                /// 着色剤3のドロップダウン
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                DrpColoarantSelect3.Items.AddRange(GetWhiteCode(db));
                DrpColoarantSelect3.SelectedIndex = SELECT_INDEX;
                /// <summary>
                /// 着色剤4のドロップダウン
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                DrpColoarantSelect4.Items.AddRange(GetWhiteCode(db));
                DrpColoarantSelect4.SelectedIndex = SELECT_INDEX;
                /// <summary>
                /// 着色剤5のドロップダウン
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                DrpColoarantSelect5.Items.AddRange(GetWhiteCode(db));
                DrpColoarantSelect5.SelectedIndex = SELECT_INDEX;
                /// <summary>
                /// 着色剤6のドロップダウン
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                DrpColoarantSelect6.Items.AddRange(GetWhiteCode(db));
                DrpColoarantSelect6.SelectedIndex = SELECT_INDEX;
                /// <summary>
                /// 着色剤7のドロップダウン
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                DrpColoarantSelect7.Items.AddRange(GetWhiteCode(db));
                DrpColoarantSelect7.SelectedIndex = SELECT_INDEX;
                /// <summary>
                /// 着色剤8のドロップダウン
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                DrpColoarantSelect8.Items.AddRange(GetWhiteCode(db));
                DrpColoarantSelect8.SelectedIndex = SELECT_INDEX;
                /// <summary>
                /// 着色剤9のドロップダウン
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                DrpColoarantSelect9.Items.AddRange(GetWhiteCode(db));
                DrpColoarantSelect9.SelectedIndex = SELECT_INDEX;
                /// <summary>
                /// 着色剤10のドロップダウン
                /// </summary>
                /// <param name="sender"></param>
                /// <param name="e"></param>
                DrpColoarantSelect10.Items.AddRange(GetWhiteCode(db));
                DrpColoarantSelect10.SelectedIndex = SELECT_INDEX;
            }
        }
        #endregion

        #region KeyPad入力処理
        /// <summary>
        /// KeyPad入力処理
        /// </summary>
        /// <param name="sender"></param>
        private void KeyPadPush(TextBox txt)
        {
            txt.Enabled = true;
            FrmKeyPad frmKeyPad = new FrmKeyPad(txt.Text);
            frmKeyPad.ShowDialog();
            // Cancelが押された場合はTextを書き換えない
            if (frmKeyPad.NumLabel.Text != CancelNumber)
            {
                txt.Text = Funcs.KeyPadPush(frmKeyPad.NumLabel.Text);
            }
        }
        #endregion

        #region ホワイトコード取得
        /// <summary>
        /// ホワイトコード取得
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        private string[] GetWhiteCode(SqlBase db)
        {
            var whiteCodeList = new List<string>() { "" };
            try
            {
                var dt = db.Select(NpCommon.Database.Sql.IOSSUP.Raw.GetWhiteCode(BaseSettings.Facility.Plant));
                whiteCodeList.AddRange(dt.AsEnumerable().Select(x => x.Field<string>(NpCommon.Database.Sql.IOSSUP.Raw.COLUMN_PRODUCT_NAME)).ToList());
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
            return whiteCodeList.ToArray();
        }
        #endregion

        #region 選択している投入缶の種類取得
        /// <summary>
        /// 選択している投入缶の種類取得
        /// </summary>
        /// <returns></returns>
        private RdoStatusChange SelectPutCan()
        {
            var rbtCheckInGroup = GrpBoxPutCan.Controls.OfType<RadioButton>().SingleOrDefault(rb => rb.Checked == true);
            switch (rbtCheckInGroup.Name)
            {
                case Chk_Empty_Can:
                    return RdoStatusChange.ChkEmptyCan;
                case Chk_Filled_Can:
                    return RdoStatusChange.ChkFilledCan;
                case Chk_Fixed_Can:
                    return RdoStatusChange.ChkFixedCan;
            }
            return RdoStatusChange.ChkEmptyCan;
        }
        #endregion

        #region 総重量取得
        /// <summary>
        /// 総重量取得
        /// </summary>
        /// <param name="totalWeight"></param>
        /// <returns>入力値と元データの総重量の合計</returns>
        private string GetTotalWeight(object totalWeight)
        {
            var totalWeght = (Funcs.StrToDecimal(TxtBaseValue.Text)
                              + Funcs.StrToDecimal(TxtColoarantValue1.Text)
                              + Funcs.StrToDecimal(TxtColoarantValue2.Text)
                              + Funcs.StrToDecimal(TxtColoarantValue3.Text)
                              + Funcs.StrToDecimal(TxtColoarantValue4.Text)
                              + Funcs.StrToDecimal(TxtColoarantValue5.Text)
                              + Funcs.StrToDecimal(TxtColoarantValue6.Text)
                              + Funcs.StrToDecimal(TxtColoarantValue7.Text)
                              + Funcs.StrToDecimal(TxtColoarantValue8.Text)
                              + Funcs.StrToDecimal(TxtColoarantValue9.Text)
                              + Funcs.StrToDecimal(TxtColoarantValue10.Text)
                              + Funcs.StrToDecimal(totalWeight.ToString())
                              );
            return totalWeght.ToString();
        }
        #endregion

        #region 製品コードから注文情報取得
        /// <summary>
        /// 製品コードから注文情報取得
        /// </summary>
        /// <returns></returns>
        private List<DataTable> GetData()
        {
            var result = new List<DataTable>();
            var parameter = new List<ParameterItem>()
            {
                new ParameterItem("@ProductCode", $"{DrpProductCodeLeft.DropDown.Text}{DrpProductCodeRight.Text}"),
            };
            using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
            {
                result.Add(db.Select(NpCommon.Database.Sql.NpMain.Orders.GetDataByProductCodeToOrderId(BaseSettings.Facility.Plant), parameter));
            }
            return result;
        }
        #endregion

        #region 白コード・着色剤コード登録済み数取得
        /// <summary>
        /// 白コード・着色剤コード登録済み数取得
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        private int GetColorantsCount(int orderId)
        {
            var weightlist = new DataTable();
            using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
            {
                var parameter = new List<ParameterItem>()
                {
                    new ParameterItem("@OrderId", orderId),
                };
                weightlist = db.Select(NpCommon.Database.Sql.NpMain.Orders.GetColorantbyOrderId(), parameter);
            }
            // 値のある物のみ取得
            var result = weightlist.Rows[SELECT_DATE].ItemArray.AsEnumerable().Where(x => !string.IsNullOrEmpty(x.ToString()));
            return result.Count();
        }
        #endregion

        #region 投入缶の種類（修正缶を選択した場合）
        /// <summary>
        /// 投入缶の種類（修正缶を選択した場合）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkFixedCan_CheckedChanged(object sender, EventArgs e)
        {
            bool rdbChecked = ((RadioButton)sender).Checked;
            if (rdbChecked)
            {
                TxtPaintName.Value = CORRECTION;
                TxtKanjiColorName.Value = CORRECTION;
                TxtPaintName.Enabled = false;
                TxtKanjiColorName.Enabled = false;
                TxtPaintName.TextForeColor = System.Drawing.SystemColors.ControlLight;
                TxtKanjiColorName.TextForeColor = System.Drawing.SystemColors.ControlLight;
            }
            else
            {
                TxtPaintName.Enabled = true;
                TxtKanjiColorName.Enabled = true;
                TxtPaintName.TextForeColor = System.Drawing.SystemColors.Window;
                TxtKanjiColorName.TextForeColor = System.Drawing.SystemColors.Window;
            }
        }
        #endregion

        #region 投入缶の種類（空缶を選択した場合）
        /// <summary>
        /// 投入缶の種類（空缶を選択した場合）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkEmptyCan_CheckedChanged(object sender, EventArgs e)
        {
            RboCheckedChanged();
        }
        #endregion

        #region 投入缶の種類（充填缶を選択した場合）
        /// <summary>
        /// 投入缶の種類（充填缶を選択した場合）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkFilledCan_CheckedChanged(object sender, EventArgs e)
        {
            RboCheckedChanged();
            bool rdbChecked = ((RadioButton)sender).Checked;
            if (rdbChecked)
            {
                TxtBaseValue.Enabled = false;
                TxtBaseValue.Text = InitialValue;
            }
            else
            {
                TxtBaseValue.Enabled = true;
            }
        }
        #endregion

        #region 投入缶の選択変更時（共通）
        /// <summary>
        /// 投入缶の選択変更時（共通）
        /// </summary>
        private void RboCheckedChanged()
        {
            TxtPaintName.Value = "";
            TxtKanjiColorName.Value = "";
        }
        #endregion
    }
}

