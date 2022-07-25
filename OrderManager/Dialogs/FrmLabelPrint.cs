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
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using NipponPaint.NpCommon;
#endregion

namespace NipponPaint.OrderManager.Dialogs
{
    /// <summary>
    /// ラベル印刷ダイアログ
    /// </summary>
    public partial class FrmLabelPrint : BaseForm
    {
        #region 定数
        /// <summary>
        /// ラベルの種類
        /// </summary>
        private enum LabelType
        {
            /// <summary>
            /// 製品ラベル
            /// </summary>
            Product,
            /// <summary>
            /// 色名ラベル
            /// </summary>
            CororName,
            /// <summary>
            /// 控え板ラベル
            /// </summary>
            BackPlate,
            /// <summary>
            /// 荷札ラベル
            /// </summary>
            Tag,
            /// <summary>
            /// 未選択
            /// </summary>
            None,
        }
        #endregion

        #region プロパティ
        /// <summary>
        /// 選択しているラベルの種類
        /// </summary>
        private LabelType SelectedLabelType
        {
            get
            {
                // 製品ラベル
                if (RBtnProductLabelPrint.Checked)
                {
                    return LabelType.Product;
                }
                // 色名ラベル
                if (RBtnColorNameLabelPrint.Checked)
                {
                    return LabelType.CororName;
                }
                // 控え板ラベル
                if (RBtnReserveLabelPrint.Checked)
                {
                    return LabelType.BackPlate;
                }
                //荷札ラベル
                if (RBtnNifudaLabelPrint.Checked)
                {
                    return LabelType.Tag;
                }
                // 未選択
                return LabelType.None;
            }
        }
        #endregion

        #region コンストラクタ
        public FrmLabelPrint(ViewModels.LabelPrintData vm)
        {
            InitializeComponent();
            InitializeForm();
            NumUpDownNumberOfPrint.Value = vm.PrintNumber;
            TxtKanjiHeight.Value = vm.KanjiHeight.ToString();
            TxtKanjiWidth.Text = vm.KanjiWidth.ToString();
            TxtNormalHeight.Value = vm.NormalHeight.ToString();
            TxtNormalWidth.Text = vm.NormalWidth.ToString();
        }
        #endregion

        #region イベント

        private void FrmLabelPrintLoad(object sender, EventArgs e)
        {
            try
            {
                this.GrpBoxNumberOfCan.Visible = false;
                this.Height = 420;
                PutLog(Sentence.Messages.ButtonClicked, ((FrmLabelPrint)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        private void RBtnProductLabelPrintCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (RBtnProductLabelPrint.Checked == true)
                {
                    this.GrpBoxNumberOfPrint.Visible = true;
                    this.GrpBoxNumberOfCan.Visible = false;
                }
                PutLog(Sentence.Messages.ButtonClicked, ((RadioButton)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        private void RBtnColorNameLabelPrintCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (RBtnColorNameLabelPrint.Checked == true)
                {
                    this.GrpBoxNumberOfPrint.Visible = true;
                    this.GrpBoxNumberOfCan.Visible = false;
                }
                PutLog(Sentence.Messages.ButtonClicked, ((RadioButton)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        private void RBtnReserveLabelPrintCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (RBtnReserveLabelPrint.Checked == true)
                {
                    this.GrpBoxNumberOfPrint.Visible = true;
                    this.GrpBoxNumberOfCan.Visible = false;
                }
                PutLog(Sentence.Messages.ButtonClicked, ((RadioButton)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        private void RBtnNifudaLabelPrintCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (RBtnNifudaLabelPrint.Checked == true)
                {
                    this.GrpBoxNumberOfPrint.Visible = false;
                    this.GrpBoxNumberOfCan.Visible = true;
                    this.GrpBoxNumberOfCan.Location = new Point(470, 12);
                }
                PutLog(Sentence.Messages.ButtonClicked, ((RadioButton)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        private void BtnPrintClick(object sender, EventArgs e)
        {
            try
            {
                PrintLabel();
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
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
        #endregion

        #region private functions

        #region 画面の初期化
        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitializeForm()
        {
            // イベントの追加
            this.Load += new System.EventHandler(this.FrmLabelPrintLoad);
            this.RBtnProductLabelPrint.CheckedChanged += new EventHandler(this.RBtnProductLabelPrintCheckedChanged);
            this.RBtnColorNameLabelPrint.CheckedChanged += new EventHandler(this.RBtnColorNameLabelPrintCheckedChanged);
            this.RBtnReserveLabelPrint.CheckedChanged += new EventHandler(this.RBtnReserveLabelPrintCheckedChanged);
            this.RBtnNifudaLabelPrint.CheckedChanged += new EventHandler(this.RBtnNifudaLabelPrintCheckedChanged);
            this.BtnPrint.Click += new EventHandler(this.BtnPrintClick);
            this.BtnClose.Click += new EventHandler(this.BtnCloseClick);
        }
        #endregion

        #region ラベル印刷
        /// <summary>
        /// ラベル印刷
        /// ラベルプリンターでラベル出力を行います。
        /// 失敗すると例外エラーとなるので、プリント処理では try～catch にて例外の補足を行ってください。
        /// </summary>
        private void PrintLabel()
        {
            var fileName = string.Empty;
            switch (SelectedLabelType)
            {
                case LabelType.Product:
                    fileName = "";
                    break;
                case LabelType.CororName:
                    fileName = "ColorName.mllayx";
                    break;
                case LabelType.BackPlate:
                    fileName = "BackPlate.mllayx";
                    break;
                case LabelType.Tag:
                    fileName = "Tag.mllayx";
                    break;
                default:
                    // TODO:エラー処理
                    return;
            }
            if (!string.IsNullOrEmpty(fileName))
            {
                // ラベルプリンタからの印刷
                var layoutFileName = Path.Combine(Application.ExecutablePath, "LabelLayouts", fileName);
                //// 文字列型データ
                //var printPutData = string.Empty;
                // 配列型データ
                var printOutDataArray = new string[] { "A", "B" };
                //// リスト型データ
                //var printOutDataList = new List<string>() { "AA", "BB" };

                using (var printer = new MlController.Ct4Lx(layoutFileName))
                {
                    //printer.Print(printPutData);
                    printer.Print(printOutDataArray);
                    //printer.Print(printOutDataList);
                }
            }
            else
            {
                // TODO:製品ラベル発行システムからの印刷
            }
        }
        #endregion

        #endregion

    }
}
