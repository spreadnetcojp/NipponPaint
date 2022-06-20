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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NipponPaint.NpCommon;
using NipponPaint.NpCommon.Settings;
using NipponPaint.NpCommon.Database;
using NipponPaint.NpCommon.Database.Sql.SupervisorPc;
using Sql = NipponPaint.NpCommon.Database.Sql;
#endregion

namespace SupervisorIfSim
{
    public partial class FrmMain : BaseForm
    {
        #region メンバ変数
        private List<GridViewSetting> _viewSettingsBarcode;
        private List<GridViewSetting> _viewSettingsFormula;
        #endregion

        #region 定数
        private const int INDEX_BARCODE = 0;
        private const int INDEX_PROCESS_CODE = 1;
        // BRC_STATUSラジオボタン
        private const int BRD_STATUS_COROB = 0;
        private const int BRD_STATUS_ERP = 1;
        // YES/NOのラジオボタン
        private const int JOB_NO = 0;
        private const int JOB_YES = 1;
        // JOB_EXIT_POSITIONラジオボタン
        private const int JOB_EXIT_POSITION_MAIN = 1;
        private const int JOB_EXIT_POSITION_TEST = 2;
        // PRD_STATUSラジオボタン
        private const int PRD_STATUS_UNDISCHARGED = 0;
        private const int PRD_STATUS_COMPLATED = 1;
        private const int PRD_STATUS_ERROR = -1;
        // PRD_UMラジオボタン
        private const int PRD_UM_GRAM = 1;
        private const int PRD_UM_CC = 2;
        // JOB_STATUSドロップダウン
        private static List<Data.DropDownRow> DrpJobStatusDefines = new List<Data.DropDownRow>()
        {
            { new Data.DropDownRow() { Value = 0, Text = "ライン挿入" } },
            { new Data.DropDownRow() { Value = 1, Text = "缶は良好にライン終了" } },
            { new Data.DropDownRow() { Value = 99, Text = "缶がライン上に存在" } },
            { new Data.DropDownRow() { Value = -1, Text = "缶はラインでエラー発生" } },
        };
        #endregion

        #region プロパティ
        #region BRC_STATUSラジオボタン
        /// <summary>
        /// BRC_STATUSラジオボタン
        /// </summary>
        private int RdbBrcStatus
        {
            get
            {
                if (RdbBrcStatusCorob.Checked)
                {
                    return BRD_STATUS_COROB;
                }
                if (RdbBrcStatusErp.Checked)
                {
                    return BRD_STATUS_ERP;
                }
                return 0;
            }
            set
            {

                RdbBrcStatusCorob.Checked = false;
                RdbBrcStatusErp.Checked = false;
                RdbBrcStatusCorob.ForeColor = Color.Black;
                RdbBrcStatusErp.ForeColor = Color.Black;
                Color color = Color.Black;
                if (PnlBrcStatus.Tag != null && PnlBrcStatus.Tag.ToString() != value.ToString())
                {
                    color = Color.Red;
                }
                switch (value)
                {
                    case BRD_STATUS_COROB:
                        RdbBrcStatusCorob.Checked = true;
                        RdbBrcStatusCorob.ForeColor = color;
                        break;
                    case BRD_STATUS_ERP:
                        RdbBrcStatusErp.Checked = true;
                        RdbBrcStatusErp.ForeColor = color;
                        break;
                    default:
                        break;
                }
                PnlBrcStatus.Tag = value.ToString();
            }
        }
        #endregion
        #region JOB_MIXINGラジオボタン
        /// <summary>
        /// JOB_MIXINGラジオボタン
        /// </summary>
        private int RdbJobMixing
        {
            get
            {
                if (RdbJobMixingYes.Checked)
                {
                    return JOB_YES;
                }
                if (RdbJobMixingNo.Checked)
                {
                    return JOB_NO;
                }
                return JOB_NO;
            }
            set
            {
                RdbJobMixingYes.Checked = false;
                RdbJobMixingNo.Checked = false;
                RdbJobMixingYes.ForeColor = Color.Black;
                RdbJobMixingNo.ForeColor = Color.Black;
                Color color = Color.Black;
                if (PnlJobMixing.Tag != null && PnlJobMixing.Tag.ToString() != value.ToString())
                {
                    color = Color.Red;
                }
                switch (value)
                {
                    case JOB_YES:
                        RdbJobMixingYes.Checked = true;
                        RdbJobMixingYes.ForeColor = color;
                        break;
                    case JOB_NO:
                        RdbJobMixingNo.Checked = true;
                        RdbJobMixingNo.ForeColor = color;
                        break;
                    default:
                        break;
                }
                PnlJobMixing.Tag = value.ToString();
            }
        }
        #endregion
        #region JOB_CAPPINGラジオボタン
        /// <summary>
        /// JOB_CAPPINGラジオボタン
        /// </summary>
        private int RdbJobCapping
        {
            get
            {
                if (RdbJobCappingYes.Checked)
                {
                    return JOB_YES;
                }
                if (RdbJobCappingNo.Checked)
                {
                    return JOB_NO;
                }
                return JOB_NO;
            }
            set
            {
                RdbJobCappingYes.Checked = false;
                RdbJobCappingNo.Checked = false;
                RdbJobCappingYes.ForeColor = Color.Black;
                RdbJobCappingNo.ForeColor = Color.Black;
                Color color = Color.Black;
                if (PnlJobCapping.Tag != null && PnlJobCapping.Tag.ToString() != value.ToString())
                {
                    color = Color.Red;
                }
                switch (value)
                {
                    case JOB_YES:
                        RdbJobCappingYes.Checked = true;
                        RdbJobCappingYes.ForeColor = color;
                        break;
                    case JOB_NO:
                        RdbJobCappingNo.Checked = true;
                        RdbJobCappingNo.ForeColor = color;
                        break;
                    default:
                        break;
                }
                PnlJobCapping.Tag = value.ToString();
            }
        }
        #endregion
        #region JOB_LID_PLACINGラジオボタン
        /// <summary>
        /// JOB_LID_PLACINGラジオボタン
        /// </summary>
        private int RdbJobLidPlacing
        {
            get
            {
                if (RdbJobLidPlacingYes.Checked)
                {
                    return JOB_YES;
                }
                if (RdbJobLidPlacingNo.Checked)
                {
                    return JOB_NO;
                }
                return JOB_NO;
            }
            set
            {
                RdbJobLidPlacingYes.Checked = false;
                RdbJobLidPlacingNo.Checked = false;
                RdbJobLidPlacingYes.ForeColor = Color.Black;
                RdbJobLidPlacingNo.ForeColor = Color.Black;
                Color color = Color.Black;
                if (PnlJobLidPlacing.Tag != null && PnlJobLidPlacing.Tag.ToString() != value.ToString())
                {
                    color = Color.Red;
                }
                switch (value)
                {
                    case JOB_YES:
                        RdbJobLidPlacingYes.Checked = true;
                        RdbJobLidPlacingYes.ForeColor = color;
                        break;
                    case JOB_NO:
                        RdbJobLidPlacingNo.Checked = true;
                        RdbJobLidPlacingNo.ForeColor = color;
                        break;
                    default:
                        break;
                }
                PnlJobLidPlacing.Tag = value.ToString();
            }
        }
        #endregion
        #region JOB_LID_CHECKラジオボタン
        /// <summary>
        /// JOB_LID_CHECKラジオボタン
        /// </summary>
        private int RdbJobLidCheck
        {
            get
            {
                if (RdbJobLidCheckYes.Checked)
                {
                    return JOB_YES;
                }
                if (RdbJobLidCheckNo.Checked)
                {
                    return JOB_NO;
                }
                return JOB_NO;
            }
            set
            {
                RdbJobLidCheckYes.Checked = false;
                RdbJobLidCheckNo.Checked = false;
                RdbJobLidCheckYes.ForeColor = Color.Black;
                RdbJobLidCheckNo.ForeColor = Color.Black;
                Color color = Color.Black;
                if (PnlJobLidCheck.Tag != null && PnlJobLidCheck.Tag.ToString() != value.ToString())
                {
                    color = Color.Red;
                }
                switch (value)
                {
                    case JOB_YES:
                        RdbJobLidCheckYes.Checked = true;
                        RdbJobLidCheckYes.ForeColor = color;
                        break;
                    case JOB_NO:
                        RdbJobLidCheckNo.Checked = true;
                        RdbJobLidCheckNo.ForeColor = color;
                        break;
                    default:
                        break;
                }
                PnlJobLidCheck.Tag = value.ToString();
            }
        }
        #endregion
        #region JOB_PRINTING_1ラジオボタン
        /// <summary>
        /// JOB_PRINTING_1ラジオボタン
        /// </summary>
        private int RdbJobPrinting1
        {
            get
            {
                if (RdbJobPrinting1Yes.Checked)
                {
                    return JOB_YES;
                }
                if (RdbJobPrinting1No.Checked)
                {
                    return JOB_NO;
                }
                return JOB_NO;
            }
            set
            {
                RdbJobPrinting1Yes.Checked = false;
                RdbJobPrinting1No.Checked = false;
                RdbJobPrinting1Yes.ForeColor = Color.Black;
                RdbJobPrinting1No.ForeColor = Color.Black;
                Color color = Color.Black;
                if (PnlJobPrinting1.Tag != null && PnlJobPrinting1.Tag.ToString() != value.ToString())
                {
                    color = Color.Red;
                }
                switch (value)
                {
                    case JOB_YES:
                        RdbJobPrinting1Yes.Checked = true;
                        RdbJobPrinting1Yes.ForeColor = color;
                        break;
                    case JOB_NO:
                        RdbJobPrinting1No.Checked = true;
                        RdbJobPrinting1No.ForeColor = color;
                        break;
                    default:
                        break;
                }
                PnlJobPrinting1.Tag = value.ToString();
            }
        }
        #endregion
        #region JOB_PRINTING_2ラジオボタン
        /// <summary>
        /// JOB_PRINTING_2ラジオボタン
        /// </summary>
        private int RdbJobPrinting2
        {
            get
            {
                if (RdbJobPrinting2Yes.Checked)
                {
                    return JOB_YES;
                }
                if (RdbJobPrinting2No.Checked)
                {
                    return JOB_NO;
                }
                return JOB_NO;
            }
            set
            {
                RdbJobPrinting2Yes.Checked = false;
                RdbJobPrinting2No.Checked = false;
                RdbJobPrinting2Yes.ForeColor = Color.Black;
                RdbJobPrinting2No.ForeColor = Color.Black;
                Color color = Color.Black;
                if (PnlJobPrinting2.Tag != null && PnlJobPrinting2.Tag.ToString() != value.ToString())
                {
                    color = Color.Red;
                }
                switch (value)
                {
                    case JOB_YES:
                        RdbJobPrinting2Yes.Checked = true;
                        RdbJobPrinting2Yes.ForeColor = color;
                        break;
                    case JOB_NO:
                        RdbJobPrinting2No.Checked = true;
                        RdbJobPrinting2No.ForeColor = color;
                        break;
                    default:
                        break;
                }
                PnlJobPrinting2.Tag = value.ToString();
            }
        }
        #endregion
        #region JOB_PRINTING_3ラジオボタン
        /// <summary>
        /// JOB_PRINTING_3ラジオボタン
        /// </summary>
        private int RdbJobPrinting3
        {
            get
            {
                if (RdbJobPrinting3Yes.Checked)
                {
                    return JOB_YES;
                }
                if (RdbJobPrinting3No.Checked)
                {
                    return JOB_NO;
                }
                return JOB_NO;
            }
            set
            {
                RdbJobPrinting3Yes.Checked = false;
                RdbJobPrinting3No.Checked = false;
                RdbJobPrinting3Yes.ForeColor = Color.Black;
                RdbJobPrinting3No.ForeColor = Color.Black;
                Color color = Color.Black;
                if (PnlJobPrinting3.Tag != null && PnlJobPrinting3.Tag.ToString() != value.ToString())
                {
                    color = Color.Red;
                }
                switch (value)
                {
                    case JOB_YES:
                        RdbJobPrinting3Yes.Checked = true;
                        RdbJobPrinting3Yes.ForeColor = color;
                        break;
                    case JOB_NO:
                        RdbJobPrinting3No.Checked = true;
                        RdbJobPrinting3No.ForeColor = color;
                        break;
                    default:
                        break;
                }
                PnlJobPrinting3.Tag = value.ToString();
            }
        }
        #endregion
        #region JOB_EXIT_POSITIONラジオボタン
        /// <summary>
        /// JOB_EXIT_POSITIONラジオボタン
        /// </summary>
        private int RdbJobExitPosition
        {
            get
            {
                if (RdbJobExitPositionMain.Checked)
                {
                    return JOB_EXIT_POSITION_MAIN;
                }
                if (RdbJobExitPositionTest.Checked)
                {
                    return JOB_EXIT_POSITION_TEST;
                }
                return JOB_EXIT_POSITION_MAIN;
            }
            set
            {
                RdbJobExitPositionMain.Checked = false;
                RdbJobExitPositionTest.Checked = false;
                RdbJobExitPositionMain.ForeColor = Color.Black;
                RdbJobExitPositionTest.ForeColor = Color.Black;
                Color color = Color.Black;
                if (PnlJobExitPosition.Tag != null && PnlJobExitPosition.Tag.ToString() != value.ToString())
                {
                    color = Color.Red;
                }
                switch (value)
                {
                    case JOB_EXIT_POSITION_MAIN:
                        RdbJobExitPositionMain.Checked = true;
                        RdbJobExitPositionMain.ForeColor = color;
                        break;
                    case JOB_EXIT_POSITION_TEST:
                        RdbJobExitPositionTest.Checked = true;
                        RdbJobExitPositionTest.ForeColor = color;
                        break;
                    default:
                        break;
                }
                PnlJobExitPosition.Tag = value.ToString();
            }
        }
        #endregion
        #region PRD_STATUSラジオボタン
        /// <summary>
        /// PRD_STATUSラジオボタン
        /// </summary>
        private int RdbPrdStatus
        {
            get
            {
                if (RdbPrdStatusUndischarged.Checked)
                {
                    return PRD_STATUS_UNDISCHARGED;
                }
                if (RdbPrdStatusCompleted.Checked)
                {
                    return PRD_STATUS_COMPLATED;
                }
                if (RdbPrdStatusError.Checked)
                {
                    return PRD_STATUS_ERROR;
                }
                return 0;
            }
            set
            {
                RdbPrdStatusUndischarged.Checked = false;
                RdbPrdStatusCompleted.Checked = false;
                RdbPrdStatusError.Checked = false;
                RdbPrdStatusUndischarged.ForeColor = Color.Black;
                RdbPrdStatusCompleted.ForeColor = Color.Black;
                RdbPrdStatusError.ForeColor = Color.Black;
                Color color = Color.Black;
                if (PnlPrdStatus.Tag != null && PnlPrdStatus.Tag.ToString() != value.ToString())
                {
                    color = Color.Red;
                }
                switch (value)
                {
                    case PRD_STATUS_UNDISCHARGED:
                        RdbPrdStatusUndischarged.Checked = true;
                        RdbPrdStatusUndischarged.ForeColor = color;
                        break;
                    case PRD_STATUS_COMPLATED:
                        RdbPrdStatusCompleted.Checked = true;
                        RdbPrdStatusCompleted.ForeColor = color;
                        break;
                    case PRD_STATUS_ERROR:
                        RdbPrdStatusError.Checked = true;
                        RdbPrdStatusError.ForeColor = color;
                        break;
                    default:
                        break;
                }
                PnlPrdStatus.Tag = value.ToString();
            }
        }
        #endregion
        #region PRD_UMラジオボタン
        /// <summary>
        /// PRD_UMラジオボタン
        /// </summary>
        private int RdbPrdUm
        {
            get
            {
                if (RdbPrdUmGram.Checked)
                {
                    return PRD_UM_GRAM;
                }
                if (RdbPrdUmCc.Checked)
                {
                    return PRD_UM_CC;
                }
                return 0;
            }
            set
            {
                RdbPrdUmGram.Checked = false;
                RdbPrdUmCc.Checked = false;
                RdbPrdUmGram.ForeColor = Color.Black;
                RdbPrdUmCc.ForeColor = Color.Black;
                Color color = Color.Black;
                if (PnlPrdUm.Tag != null && PnlPrdUm.Tag.ToString() != value.ToString())
                {
                    color = Color.Red;
                }
                switch (value)
                {
                    case PRD_UM_GRAM:
                        RdbPrdUmGram.Checked = true;
                        RdbPrdUmGram.ForeColor = color;
                        break;
                    case PRD_UM_CC:
                        RdbPrdUmCc.Checked = true;
                        RdbPrdUmCc.ForeColor = color;
                        break;
                    default:
                        break;
                }
                PnlPrdUm.Tag = value.ToString();
            }
        }
        #endregion
        #region PRD_ISPREFILLEDラジオボタン
        /// <summary>
        /// PRD_ISPREFILLEDラジオボタン
        /// </summary>
        private bool RdbPrdIsprefilled
        {
            get
            {
                if (RdbPrdIsprefilledTrue.Checked)
                {
                    return true;
                }
                if (RdbPrdIsprefilledFalse.Checked)
                {
                    return false;
                }
                return true;
            }
            set
            {
                RdbPrdIsprefilledTrue.Checked = false;
                RdbPrdIsprefilledFalse.Checked = false;
                RdbPrdIsprefilledTrue.ForeColor = Color.Black;
                RdbPrdIsprefilledFalse.ForeColor = Color.Black;
                Color color = Color.Black;
                if (PnlPrdIsprefilled.Tag != null && PnlPrdIsprefilled.Tag.ToString() != value.ToString())
                {
                    color = Color.Red;
                }
                switch (value)
                {
                    case true:
                        RdbPrdIsprefilledTrue.Checked = true;
                        RdbPrdIsprefilledTrue.ForeColor = color;
                        break;
                    case false:
                        RdbPrdIsprefilledFalse.Checked = true;
                        RdbPrdIsprefilledFalse.ForeColor = color;
                        break;
                    default:
                        break;
                }
                PnlPrdIsprefilled.Tag = value.ToString();
            }
        }
        #endregion
        #endregion

        #region コンストラクタ
        public FrmMain()
        {
            InitializeComponent();
            InitializeForm();
        }
        #endregion

        #region イベント

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMainShown(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.OpenMainForm);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

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
                    case Keys.F2:
                        // 編集
                        BtnEdit.PerformClick();
                        break;
                    case Keys.F5:
                        // 一覧更新
                        BtnPreview.PerformClick();
                        break;
                    case Keys.F9:
                        // 開始／停止
                        BtnTimer.PerformClick();
                        break;
                }
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
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
                var barcode = GvBarcode.SelectedRows[0].Cells[INDEX_BARCODE].Value.ToString();
                var processCode = GvBarcode.SelectedRows[0].Cells[INDEX_PROCESS_CODE].Value.ToString();
                EditData(barcode, processCode, Dialogs.FrmEdit.DialogMode.Merge);
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
        private void BtnTimer_Click(object sender, EventArgs e)
        {
            try
            {
                TimerReset(!TimerPreview.Enabled);
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
        private void BtnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                PreviewBarcodeData();
                PutLog(Sentence.Messages.PreviewData, ((Button)sender).Text);
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
        private void TimerPreview_Tick(object sender, EventArgs e)
        {
            try
            {
                PreviewBarcodeData();
                PutLog(Sentence.Messages.PreviewData, ((Button)sender).Text);
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
        private void GvMain_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var barcode = GvBarcode.Rows[e.RowIndex].Cells[INDEX_BARCODE].Value.ToString();
                var processCode = GvBarcode.Rows[e.RowIndex].Cells[INDEX_PROCESS_CODE].Value.ToString();
                EditData(barcode, processCode, Dialogs.FrmEdit.DialogMode.Update);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        private void GvMain_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                DataGridViewFormatting((DataGridView)sender);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        #endregion

        #region private functions

        private void InitializeForm()
        {
            GvBarcode.Dock = DockStyle.Fill;
            GvFormula.Dock = DockStyle.Fill;
            // 画面の定義
            _viewSettingsBarcode = new List<GridViewSetting>()
            {
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbBarcode.BARCODE, DisplayName = TbBarcode.BARCODE, Visible = true, Width = 200, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtBarcode } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbBarcode.PROCESS_CODE, DisplayName = TbBarcode.PROCESS_CODE, Visible = true, Width = 200, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtProcessCode  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbBarcode.BRC_TIME_INSERTED, DisplayName = TbBarcode.BRC_TIME_INSERTED, Visible = true, Width = 120, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtBrcTimeInserted  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbBarcode.BRC_TIME_PROCESSED, DisplayName = TbBarcode.BRC_TIME_PROCESSED, Visible = true, Width = 120, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtBrcTimeProcessed  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbBarcode.BRC_STATUS, DisplayName = TbBarcode.BRC_STATUS, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = null  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbBarcode.BRC_ERR_1, DisplayName = TbBarcode.BRC_ERR_1, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtBrcErr1  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbBarcode.BRC_ERR_2, DisplayName = TbBarcode.BRC_ERR_2, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtBrcErr2  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbBarcode.BRC_ERR_3, DisplayName = TbBarcode.BRC_ERR_3, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtBrcErr3  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TIME_INSERTED, DisplayName = TbJob.JOB_TIME_INSERTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobTimeInserted  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_STATUS, DisplayName = TbJob.JOB_STATUS, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = null  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TARE_WEIGHT_EXPECTED, DisplayName = TbJob.JOB_TARE_WEIGHT_EXPECTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobTareWeightExpected  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TARE_WEIGHT_DETECTED, DisplayName = TbJob.JOB_TARE_WEIGHT_DETECTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobTareWeightDetected  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TARE_WEIGHT_PERC_ERR_ADMITTED, DisplayName = TbJob.JOB_TARE_WEIGHT_PERC_ERR_ADMITTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobTareWeightPercErrAdmitted  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_GROSS_WEIGHT_EXPECTED, DisplayName = TbJob.JOB_GROSS_WEIGHT_EXPECTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobGrossWeightExpected  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_GROSS_WEIGHT_DETECTED, DisplayName = TbJob.JOB_GROSS_WEIGHT_DETECTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobGrossWeightDetected  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_GROSS_WEIGHT_PERC_ERR_ADMITTED, DisplayName = TbJob.JOB_GROSS_WEIGHT_PERC_ERR_ADMITTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobGrossWeightPercErrAdmitted  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_NET_WEIGHT_EXPECTED, DisplayName = TbJob.JOB_NET_WEIGHT_EXPECTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobNetWeightExpected  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_NET_WEIGHT_DETECTED, DisplayName = TbJob.JOB_NET_WEIGHT_DETECTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobNetWeightDetected  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_NET_WEIGHT_PERC_ERR_ADMITTED, DisplayName = TbJob.JOB_NET_WEIGHT_PERC_ERR_ADMITTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobNetWeightPercErrAdmitted  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TOT_COLORANT_WEIGHT_EXPECTED, DisplayName = TbJob.JOB_TOT_COLORANT_WEIGHT_EXPECTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobTotColorantWeightExpected  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TOT_COLORANT_WEIGHT_DETECTED, DisplayName = TbJob.JOB_TOT_COLORANT_WEIGHT_DETECTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobTotColorantWeightDetected  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TOT_COLORANT_WEIGHT_PERC_ERR_ADMITTED, DisplayName = TbJob.JOB_TOT_COLORANT_WEIGHT_PERC_ERR_ADMITTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobTotColorantWeightPercErrAdmitted  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TOT_GRAVIMETRIC_WEIGHT_EXPECTED, DisplayName = TbJob.JOB_TOT_GRAVIMETRIC_WEIGHT_EXPECTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobTotGravimetricWeightExpected  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TOT_GRAVIMETRIC_WEIGHT_DETECTED, DisplayName = TbJob.JOB_TOT_GRAVIMETRIC_WEIGHT_DETECTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobTotGravimetricWeightDetected  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TOT_GRAVIMETRIC_WEIGHT_PERC_ERR_ADMITTED, DisplayName = TbJob.JOB_TOT_GRAVIMETRIC_WEIGHT_PERC_ERR_ADMITTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobTotGravimetricWeightPercErrAdmitted  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_MIXING, DisplayName = TbJob.JOB_MIXING, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = null  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_MIXING_TIME, DisplayName = TbJob.JOB_MIXING_TIME, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobMixingTime  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_MIXING_SPEED, DisplayName = TbJob.JOB_MIXING_SPEED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobMixingSpeed  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_CAPPING, DisplayName = TbJob.JOB_CAPPING, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = null  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_LID_PLACING, DisplayName = TbJob.JOB_LID_PLACING, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = null  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_LID_CHECK, DisplayName = TbJob.JOB_LID_CHECK, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = null  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_PRINTING_1, DisplayName = TbJob.JOB_PRINTING_1, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = null  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_PRINTING_2, DisplayName = TbJob.JOB_PRINTING_2, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = null  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_PRINTING_3, DisplayName = TbJob.JOB_PRINTING_3, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = null  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_EXIT_POSITION, DisplayName = TbJob.JOB_EXIT_POSITION, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = null  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TAG_1, DisplayName = TbJob.JOB_TAG_1, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobTag1  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TAG_2, DisplayName = TbJob.JOB_TAG_2, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobTag2  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TAG_3, DisplayName = TbJob.JOB_TAG_3, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobTag3  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TAG_4, DisplayName = TbJob.JOB_TAG_4, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobTag4  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TAG_5, DisplayName = TbJob.JOB_TAG_5, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobTag5  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_ERR_1, DisplayName = TbJob.JOB_ERR_1, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobErr1  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_ERR_2, DisplayName = TbJob.JOB_ERR_2, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobErr2  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_ERR_3, DisplayName = TbJob.JOB_ERR_3, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobErr3  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_ERR_4, DisplayName = TbJob.JOB_ERR_4, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobErr4  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_ERR_5, DisplayName = TbJob.JOB_ERR_5, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtJobErr5  } },
            };
            _viewSettingsFormula = new List<GridViewSetting>()
            {
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_TIME_INSERTED, DisplayName = TbFormula.PRD_TIME_INSERTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtPrdTimeInserted  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_STATUS, DisplayName = TbFormula.PRD_STATUS, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = null  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_CODE, DisplayName = TbFormula.PRD_CODE, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtPrdCode  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_DESC, DisplayName = TbFormula.PRD_DESC, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtPrdDesc  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_UM, DisplayName = TbFormula.PRD_UM, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = null  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_SPECIFIC_GRAVITY, DisplayName = TbFormula.PRD_SPECIFIC_GRAVITY, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtPrdSpecificGravity  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_QTY_REQ, DisplayName = TbFormula.PRD_QTY_REQ, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtPrdQtyReq  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_QTY_DISP, DisplayName = TbFormula.PRD_QTY_DISP, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtPrdQtyDisp  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_START_DISP, DisplayName = TbFormula.PRD_START_DISP, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtPrdStartDisp  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_END_DISP, DisplayName = TbFormula.PRD_END_DISP, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtPrdEndDisp  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_PRIORITY, DisplayName = TbFormula.PRD_PRIORITY, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtPrdPriority  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_NUM, DisplayName = TbFormula.PRD_NUM, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtPrdNum  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_ISPREFILLED, DisplayName = TbFormula.PRD_ISPREFILLED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = null  } },
                { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_PREFILLED_QTY, DisplayName = TbFormula.PRD_PREFILLED_QTY, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft, DisplayControl = TxtPrdPrefilledQty  } },
            };
            // ドロップダウン要素の生成
            CreateDropDownItems();
            this.Shown += new EventHandler(this.FrmMainShown);
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(this.FormKeyDown);
            this.BtnEdit.Click += new EventHandler(this.BtnEdit_Click);
            this.BtnTimer.Click += new EventHandler(this.BtnTimer_Click);
            this.BtnPreview.Click += new EventHandler(this.BtnPreview_Click);
            this.GvBarcode.SelectionChanged += new System.EventHandler(this.GvMain_SelectionChanged);
            this.GvFormula.SelectionChanged += new System.EventHandler(this.GvFormula_SelectionChanged);
            this.GvBarcode.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(this.GvMain_DataBindingComplete);
            this.TimerPreview.Tick += new EventHandler(this.TimerPreview_Tick);
            // タイマーの周期を設定
            TimerPreview.Interval = int.Parse(LblCycle.Text) * 1000;
            // DataGridViewの表示
            PreviewBarcodeData();
            var cnt = 0;
            foreach (var item in _viewSettingsBarcode)
            {
                GvBarcode.Columns[cnt].Width = item.Width;
                GvBarcode.Columns[cnt].Visible = item.Visible;
                GvBarcode.Columns[cnt].DefaultCellStyle.Alignment = item.alignment;
                cnt++;
            }
            GvBarcode.Columns[INDEX_PROCESS_CODE].Frozen = true;
        }

        #region DataGridViewの書式設定
        /// <summary>
        /// DataGridViewの書式設定
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="e"></param>
        private void DataGridViewFormatting(DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.Cells[INDEX_BARCODE].Style.BackColor = Color.LightYellow;
                row.Cells[INDEX_PROCESS_CODE].Style.BackColor = Color.LightYellow;
            }
        }
        #endregion

        #region Barcode一覧表示
        /// <summary>
        /// Barcode一覧表示
        /// </summary>
        private void PreviewBarcodeData()
        {
            using (var db = new SqlBase(SqlBase.DatabaseKind.SUPERVISOR, SqlBase.TransactionUse.No, Log.ApplicationType.SupervisorInterface))
            {
                var selectedIndex = -1;
                if (GvBarcode.SelectedRows.Count > 0)
                {
                    selectedIndex = GvBarcode.SelectedRows[0].Index;
                }
                var result = db.Select(TbBarcode.GetPreviewAll());
                GvBarcode.DataSource = result;
                LblPreviewTime.Text = $"秒周期で更新（最終更新時刻：{DateTime.Now.ToString("HH:mm:ss")}）";
                if (GvBarcode.Rows.Count > 0 && selectedIndex > -1)
                {
                    GvBarcode.Rows[selectedIndex].Selected = true;
                }
            }
        }
        #endregion

        #region Formura一覧表示
        /// <summary>
        /// Formura一覧表示
        /// </summary>
        private void PreviewFormuraData(string barCode, string processCode)
        {
            using (var db = new SqlBase(SqlBase.DatabaseKind.SUPERVISOR, SqlBase.TransactionUse.No, Log.ApplicationType.SupervisorInterface))
            {
                var selectedIndex = -1;
                if (GvFormula.SelectedRows.Count > 0)
                {
                    selectedIndex = GvFormula.SelectedRows[0].Index;
                }
                var result = db.Select(TbFormula.GetPreview(barCode, processCode));
                GvFormula.DataSource = result;
                if (GvFormula.Rows.Count > 0 && selectedIndex > -1)
                {
                    GvFormula.Rows[selectedIndex].Selected = true;
                }
            }
        }
        #endregion

        #region タイマーリセット
        /// <summary>
        /// タイマーリセット
        /// </summary>
        /// <param name="timerEnabled"></param>
        private void TimerReset(bool timerEnabled)
        {
            if (timerEnabled)
            {
                BtnTimer.Text = "停止";
            }
            else
            {
                BtnTimer.Text = "開始";
            }
            BtnPreview.Enabled = !timerEnabled;
            TimerPreview.Enabled = timerEnabled;
        }
        #endregion

        #region 編集画面を開く
        /// <summary>
        /// 編集画面を開く
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="processCode"></param>
        /// <param name="mode"></param>
        private void EditData(string barcode, string processCode, Dialogs.FrmEdit.DialogMode mode)
        {
            var timerEnabled = TimerPreview.Enabled;
            TimerReset(false);
            var form = new Dialogs.FrmEdit(barcode, processCode, mode);
            form.ShowDialog();
            PreviewBarcodeData();
            TimerReset(timerEnabled);
        }
        #endregion

        #region ドロップダウンの要素を生成する
        /// <summary>
        /// ドロップダウンの要素を生成する
        /// </summary>
        private void CreateDropDownItems()
        {
            var list = new DataTable();
            list.Columns.Add(nameof(Data.DropDownRow.Value), typeof(int));
            list.Columns.Add(nameof(Data.DropDownRow.Text), typeof(string));
            foreach (var item in DrpJobStatusDefines)
            {
                var row = list.NewRow();
                row[nameof(Data.DropDownRow.Value)] = item.Value;
                row[nameof(Data.DropDownRow.Text)] = item.Text;
                list.Rows.Add(row);
            }
            DrpJobStatus.DataSource = list;
            DrpJobStatus.ValueMember = nameof(Data.DropDownRow.Value);
            DrpJobStatus.DisplayMember = nameof(Data.DropDownRow.Text);
        }
        #endregion

        #endregion

        private void GvMain_SelectionChanged(object sender, EventArgs e)
        {
            var rows = ((DataGridView)sender).SelectedRows;
            if (rows.Count > 0)
            {
                PreviewDetail(rows[0], _viewSettingsBarcode);
                PreviewFormuraData(rows[0].Cells[TbBarcode.BARCODE].Value.ToString(), rows[0].Cells[TbBarcode.PROCESS_CODE].Value.ToString());
            }
            else
            {
                ClearDetail(_viewSettingsBarcode);
            }
        }

        private void PreviewDetail(DataGridViewRow row, List<GridViewSetting> settings)
        {
            foreach (var setting in settings)
            {
                if (setting.DisplayControl != null)
                {
                    //switch (setting.DisplayControl)
                    //{
                    //    case 
                    //}
                    setting.DisplayControl.Text = row.Cells[setting.ColumnName].Value.ToString();
                }
            }
        }
        private void ClearDetail(List<GridViewSetting> settings)
        {
            foreach (var setting in settings)
            {
                if (setting.DisplayControl != null)
                {
                    setting.DisplayControl.Text = string.Empty;
                }
            }
        }


        private void GvFormula_SelectionChanged(object sender, EventArgs e)
        {
            var rows = ((DataGridView)sender).SelectedRows;
            if (rows.Count > 0)
            {
                PreviewDetail(rows[0], _viewSettingsFormula);
            }
            else
            {
                ClearDetail(_viewSettingsFormula);
            }
        }
    }
}
