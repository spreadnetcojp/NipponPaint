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
#endregion

namespace SupervisorIfSim.Dialogs
{
    public partial class FrmEdit : BaseForm
    {
        #region DataGridViewの列定義
        private List<GridViewSetting> ViewSettingsMain = new List<GridViewSetting>()
        {
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbBarcode.BARCODE, DisplayName = TbBarcode.BARCODE, Visible = true, Width = 200, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbBarcode.PROCESS_CODE, DisplayName = TbBarcode.PROCESS_CODE, Visible = true, Width = 200, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbBarcode.BRC_TIME_INSERTED, DisplayName = TbBarcode.BRC_TIME_INSERTED, Visible = true, Width = 120, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbBarcode.BRC_TIME_PROCESSED, DisplayName = TbBarcode.BRC_TIME_PROCESSED, Visible = true, Width = 120, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbBarcode.BRC_STATUS, DisplayName = TbBarcode.BRC_STATUS, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbBarcode.BRC_ERR_1, DisplayName = TbBarcode.BRC_ERR_1, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbBarcode.BRC_ERR_2, DisplayName = TbBarcode.BRC_ERR_2, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbBarcode.BRC_ERR_3, DisplayName = TbBarcode.BRC_ERR_3, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_TIME_INSERTED, DisplayName = TbFormula.PRD_TIME_INSERTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_STATUS, DisplayName = TbFormula.PRD_STATUS, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_CODE, DisplayName = TbFormula.PRD_CODE, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_DESC, DisplayName = TbFormula.PRD_DESC, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_UM, DisplayName = TbFormula.PRD_UM, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_SPECIFIC_GRAVITY, DisplayName = TbFormula.PRD_SPECIFIC_GRAVITY, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_QTY_REQ, DisplayName = TbFormula.PRD_QTY_REQ, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_QTY_DISP, DisplayName = TbFormula.PRD_QTY_DISP, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_START_DISP, DisplayName = TbFormula.PRD_START_DISP, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_END_DISP, DisplayName = TbFormula.PRD_END_DISP, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_PRIORITY, DisplayName = TbFormula.PRD_PRIORITY, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_NUM, DisplayName = TbFormula.PRD_NUM, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_ISPREFILLED, DisplayName = TbFormula.PRD_ISPREFILLED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbFormula.PRD_PREFILLED_QTY, DisplayName = TbFormula.PRD_PREFILLED_QTY, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TIME_INSERTED, DisplayName = TbJob.JOB_TIME_INSERTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_STATUS, DisplayName = TbJob.JOB_STATUS, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TARE_WEIGHT_EXPECTED, DisplayName = TbJob.JOB_TARE_WEIGHT_EXPECTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TARE_WEIGHT_DETECTED, DisplayName = TbJob.JOB_TARE_WEIGHT_DETECTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TARE_WEIGHT_PERC_ERR_ADMITTED, DisplayName = TbJob.JOB_TARE_WEIGHT_PERC_ERR_ADMITTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_GROSS_WEIGHT_EXPECTED, DisplayName = TbJob.JOB_GROSS_WEIGHT_EXPECTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_GROSS_WEIGHT_DETECTED, DisplayName = TbJob.JOB_GROSS_WEIGHT_DETECTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_GROSS_WEIGHT_PERC_ERR_ADMITTED, DisplayName = TbJob.JOB_GROSS_WEIGHT_PERC_ERR_ADMITTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_NET_WEIGHT_EXPECTED, DisplayName = TbJob.JOB_NET_WEIGHT_EXPECTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_NET_WEIGHT_DETECTED, DisplayName = TbJob.JOB_NET_WEIGHT_DETECTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_NET_WEIGHT_PERC_ERR_ADMITTED, DisplayName = TbJob.JOB_NET_WEIGHT_PERC_ERR_ADMITTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TOT_COLORANT_WEIGHT_EXPECTED, DisplayName = TbJob.JOB_TOT_COLORANT_WEIGHT_EXPECTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TOT_COLORANT_WEIGHT_DETECTED, DisplayName = TbJob.JOB_TOT_COLORANT_WEIGHT_DETECTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TOT_COLORANT_WEIGHT_PERC_ERR_ADMITTED, DisplayName = TbJob.JOB_TOT_COLORANT_WEIGHT_PERC_ERR_ADMITTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TOT_GRAVIMETRIC_WEIGHT_EXPECTED, DisplayName = TbJob.JOB_TOT_GRAVIMETRIC_WEIGHT_EXPECTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TOT_GRAVIMETRIC_WEIGHT_DETECTED, DisplayName = TbJob.JOB_TOT_GRAVIMETRIC_WEIGHT_DETECTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TOT_GRAVIMETRIC_WEIGHT_PERC_ERR_ADMITTED, DisplayName = TbJob.JOB_TOT_GRAVIMETRIC_WEIGHT_PERC_ERR_ADMITTED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_MIXING, DisplayName = TbJob.JOB_MIXING, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_MIXING_TIME, DisplayName = TbJob.JOB_MIXING_TIME, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_MIXING_SPEED, DisplayName = TbJob.JOB_MIXING_SPEED, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_CAPPING, DisplayName = TbJob.JOB_CAPPING, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_LID_PLACING, DisplayName = TbJob.JOB_LID_PLACING, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_LID_CHECK, DisplayName = TbJob.JOB_LID_CHECK, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_PRINTING_1, DisplayName = TbJob.JOB_PRINTING_1, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_PRINTING_2, DisplayName = TbJob.JOB_PRINTING_2, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_PRINTING_3, DisplayName = TbJob.JOB_PRINTING_3, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_EXIT_POSITION, DisplayName = TbJob.JOB_EXIT_POSITION, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TAG_1, DisplayName = TbJob.JOB_TAG_1, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TAG_2, DisplayName = TbJob.JOB_TAG_2, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TAG_3, DisplayName = TbJob.JOB_TAG_3, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TAG_4, DisplayName = TbJob.JOB_TAG_4, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_TAG_5, DisplayName = TbJob.JOB_TAG_5, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_ERR_1, DisplayName = TbJob.JOB_ERR_1, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_ERR_2, DisplayName = TbJob.JOB_ERR_2, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_ERR_3, DisplayName = TbJob.JOB_ERR_3, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_ERR_4, DisplayName = TbJob.JOB_ERR_4, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = TbJob.JOB_ERR_5, DisplayName = TbJob.JOB_ERR_5, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleLeft } },
        };
        #endregion

        #region メンバ変数
        private List<Data.TextBoxItem> _FormulaTextBoxList;
        private string _barCode;
        private string _processCode;
        #endregion

        #region 定数
        // PRD_STATUSラジオボタン
        private const int PRD_STATUS_UNDISCHARGED = 0;
        private const int PRD_STATUS_COMPLATED = 1;
        private const int PRD_STATUS_ERROR = -1;
        // PRD_UMラジオボタン
        private const int PRD_UM_GRAM = 1;
        private const int PRD_UM_CC = 2;
        public enum DialogMode
        {
            Merge,
            Update,
        }
        public enum SaveMode
        {
            All,
            Barcode,
            Formula,
            Job
        }
        #endregion

        #region プロパティ
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
        public FrmEdit(string barCode, string processCode, DialogMode mode)
        {
            _barCode = barCode;
            _processCode = processCode;
            InitializeComponent();
            _FormulaTextBoxList = new List<Data.TextBoxItem>()
            {
                { new Data.TextBoxItem() { TextBox = TxtPrdTimeInserted, FieldName = "PRD_TIME_INSERTED", FieldType = typeof(DateTime) } },
                { new Data.TextBoxItem() { TextBox = TxtPrdCode, FieldName = "PRD_CODE", FieldType = typeof(string) } },
                { new Data.TextBoxItem() { TextBox = TxtPrdDesc, FieldName = "PRD_DESC", FieldType = typeof(string) } },
                { new Data.TextBoxItem() { TextBox = TxtPrdSpecificGravity, FieldName = "PRD_SPECIFIC_GRAVITY", FieldType = typeof(string) } },
                { new Data.TextBoxItem() { TextBox = TxtPrdQtyReq, FieldName = "PRD_QTY_REQ", FieldType = typeof(float) } },
                { new Data.TextBoxItem() { TextBox = TxtPrdQtyDisp, FieldName = "PRD_QTY_DISP", FieldType = typeof(float) } },
                { new Data.TextBoxItem() { TextBox = TxtPrdStartDisp, FieldName = "PRD_START_DISP", FieldType = typeof(DateTime) } },
                { new Data.TextBoxItem() { TextBox = TxtPrdEndDisp, FieldName = "PRD_END_DISP", FieldType = typeof(DateTime) } },
                { new Data.TextBoxItem() { TextBox = TxtPrdPriority, FieldName = "PRD_PRIORITY", FieldType = typeof(int) } },
                { new Data.TextBoxItem() { TextBox = TxtPrdNum, FieldName = "PRD_NUM", FieldType = typeof(int) } },
                { new Data.TextBoxItem() { TextBox = TxtPrdPrefilledQty, FieldName = "PRD_PREFILLED_QTY", FieldType = typeof(float) } },
            };
            InitializeForm(barCode, processCode);
        }
        #endregion

        #region イベント

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
                        // TB_BARCODE
                        //BtnSaveBarcode.PerformClick();
                        break;
                    case Keys.F3:
                        // TB_FORMULA
                        //BtnSaveFormula.PerformClick();
                        break;
                    case Keys.F4:
                        // TB_JOB
                        //BtnSaveJob.PerformClick();
                        break;
                    case Keys.F8:
                        // すべて
                        //BtnSaveAll.PerformClick();
                        break;
                }
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        #region すべて更新
        /// <summary>
        /// すべて更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveAll_Click(object sender, EventArgs e)
        {
            using (var db = new SqlBase(SqlBase.DatabaseKind.SUPERVISOR, SqlBase.TransactionUse.Yes, Log.ApplicationType.SupervisorInterface))
            {
                try
                {
                    if (Save(SaveMode.All, db))
                    {
                        db.Commit();
                        this.Close();
                    }
                    else
                    {
                        db.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    PutLog(ex, true);
                }
            }
        }
        #endregion

        #region TB_BARCODE更新
        /// <summary>
        /// TB_BARCODE更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveBarcode_Click(object sender, EventArgs e)
        {
            using (var db = new SqlBase(SqlBase.DatabaseKind.SUPERVISOR, SqlBase.TransactionUse.Yes, Log.ApplicationType.SupervisorInterface))
            {
                try
                {
                    if (Save(SaveMode.Barcode, db))
                    {
                        db.Commit();
                        this.Close();
                    }
                    else
                    {
                        db.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    PutLog(ex, true);
                }
            }
        }
        #endregion

        #region TB_FORMULA更新
        /// <summary>
        /// TB_FORMULA更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveFormula_Click(object sender, EventArgs e)
        {
            using (var db = new SqlBase(SqlBase.DatabaseKind.SUPERVISOR, SqlBase.TransactionUse.Yes, Log.ApplicationType.SupervisorInterface))
            {
                try
                {
                    if (Save(SaveMode.Formula, db))
                    {
                        db.Commit();
                        this.Close();
                    }
                    else
                    {
                        db.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    PutLog(ex, true);
                }
            }
        }
        #endregion

        #region TB_JOB更新
        /// <summary>
        /// TB_JOB更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveJob_Click(object sender, EventArgs e)
        {
            using (var db = new SqlBase(SqlBase.DatabaseKind.SUPERVISOR, SqlBase.TransactionUse.Yes, Log.ApplicationType.SupervisorInterface))
            {
                try
                {
                    if (Save(SaveMode.Job, db))
                    {
                        db.Commit();
                        this.Close();
                    }
                    else
                    {
                        db.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    PutLog(ex, true);
                }
            }
        }
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPreview_Click(object sender, EventArgs e)
        {
            try
            {
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
                PutLog(Sentence.Messages.PreviewData, ((Button)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        #endregion

        #region private functions

        #region ダイアログ初期化
        /// <summary>
        /// ダイアログ初期化
        /// </summary>
        /// <param name="barCode"></param>
        /// <param name="processCode"></param>
        private void InitializeForm(string barCode, string processCode)
        {
            //this.Shown += new EventHandler(this.FrmMainShown);
            //this.BtnEdit.Click += new EventHandler(this.BtnEdit_Click);
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(this.FormKeyDown);
            // データ表示
            PreviewData(barCode, processCode);
        }
        #endregion
        #region TB_BARCODEの更新
        /// <summary>
        /// TB_BARCODEの更新
        /// </summary>
        /// <param name="db"></param>
        private void SaveBarcode(SqlBase db, DateTime entryTime)
        {
            //
            // SQL文生成＋SQLパラメータ作成
            //
            string sql = TbBarcode.Merge(out List<ParameterItem> parameters);
            // キー
            SetParameter(parameters, TbBarcode.BARCODE, _barCode);
            SetParameter(parameters, TbBarcode.PROCESS_CODE, _processCode);
            //// テキストボックス
            //foreach (var item in _barcodeTextBoxList)
            //{
            //    SetParameter(parameters, item.FieldName, item.TextBox.Text, item.FieldType, entryTime);
            //}
            //
            // 更新実行
            //
            db.Execute(sql, parameters);
        }
        #endregion

        #region TB_BARCODEの更新
        // TB_BARCODEの更新
        private void SaveFormula(SqlBase db, DateTime entryTime)
        {
            //
            // SQL文生成＋SQLパラメータ作成
            //
            string sql = TbFormula.Merge(out List<ParameterItem> parameters);
            // キー
            SetParameter(parameters, TbFormula.PRD_BARCODE, _barCode);
            SetParameter(parameters, TbFormula.PRD_PROCESS_CODE, _processCode);
            // テキストボックス
            foreach (var item in _FormulaTextBoxList)
            {
                SetParameter(parameters, item.FieldName, item.TextBox.Text, item.FieldType, entryTime);
            }
            // ラジオボタン
            SetParameter(parameters, TbFormula.PRD_STATUS, RdbPrdStatus);
            SetParameter(parameters, TbFormula.PRD_UM, RdbPrdUm);
            SetParameter(parameters, TbFormula.PRD_ISPREFILLED, RdbPrdIsprefilled);
            //
            // 更新実行
            //
            db.Execute(sql, parameters);
        }
        #endregion

        #region TB_JOBの更新
        /// <summary>
        /// TB_JOBの更新
        /// </summary>
        /// <param name="db"></param>
        private void SaveJob(SqlBase db, DateTime entryTime)
        {
            //
            // SQL文生成＋SQLパラメータ作成
            //
            string sql = TbJob.Merge(out List<ParameterItem> parameters);
            // キー
            SetParameter(parameters, TbJob.JOB_BARCODE, _barCode);
            SetParameter(parameters, TbJob.JOB_PROCESS_CODE, _processCode);
            //// テキストボックス
            //foreach (var item in _jobTextBoxList)
            //{
            //    SetParameter(parameters, item.FieldName, item.TextBox.Text, item.FieldType, entryTime);
            //}
            //// ドロップダウン
            //SetParameter(parameters, TbJob.JOB_STATUS, DrpJobStatus.SelectedValue);
            //// ラジオボタン
            //SetParameter(parameters, TbJob.JOB_MIXING, RdbJobMixing);
            //SetParameter(parameters, TbJob.JOB_CAPPING, RdbJobCapping);
            //SetParameter(parameters, TbJob.JOB_LID_PLACING, RdbJobLidPlacing);
            //SetParameter(parameters, TbJob.JOB_LID_CHECK, RdbJobLidCheck);
            //SetParameter(parameters, TbJob.JOB_PRINTING_1, RdbJobPrinting1);
            //SetParameter(parameters, TbJob.JOB_PRINTING_2, RdbJobPrinting2);
            //SetParameter(parameters, TbJob.JOB_PRINTING_3, RdbJobPrinting3);
            //SetParameter(parameters, TbJob.JOB_EXIT_POSITION, RdbJobExitPosition);
            //
            // 更新実行
            //
            db.Execute(sql, parameters);
        }
        #endregion

        #region SQLパラメータ値の設定
        /// <summary>
        /// SQLパラメータ値の設定
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void SetParameter(List<ParameterItem> parameters, string key, object value)
        {
            foreach (var item in parameters)
            {
                if (item.Key == key)
                {
                    item.Value = value;
                    return;
                }
            }
        }
        /// <summary>
        /// SQLパラメータ値の設定
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        private void SetParameter(List<ParameterItem> parameters, string key, object value, Type fieldType, DateTime entryTime)
        {
            foreach (var item in parameters)
            {
                if (item.Key == key)
                {
                    if (fieldType == typeof(DateTime))
                    {
                        item.Value = entryTime;
                    }
                    else
                    {
                        item.Value = value;
                    }
                    return;
                }
            }
        }
        #endregion

        #region データベースのカラム名からインデクスを取得
        /// <summary>
        /// データベースのカラム名からインデクスを取得
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        private int GetColumnIndex(string columnName, DataColumnCollection columns)
        {
            int index = 0;
            foreach (DataColumn column in columns)
            {
                if (column.ColumnName == columnName)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }
        #endregion

        #region データベースの値をテキストボックスに反映
        /// <summary>
        /// データベースの値をテキストボックスに反映
        /// </summary>
        /// <param name="items"></param>
        /// <param name="dt"></param>
        private void DbToTextBox(List<Data.TextBoxItem> items, DataTable dt)
        {
            foreach (var item in items)
            {
                var index = GetColumnIndex(item.FieldName, dt.Columns);
                if (!(index < 0))
                {
                    item.TextBox.Text = dt.Rows[0][index].ToString();
                    if (item.FieldType == typeof(int) || item.FieldType == typeof(float))
                    {
                        item.TextBox.TextAlign = HorizontalAlignment.Right;
                    }
                    if (item.TextBox.Tag != null && item.TextBox.Tag.ToString() != item.TextBox.Text.ToString())
                    {
                        item.TextBox.ForeColor = Color.Red;
                    }
                    else
                    {
                        item.TextBox.ForeColor = Color.Black;
                    }
                    item.TextBox.Tag = item.TextBox.Text.ToString();
                }
            }
        }
        #endregion

        #region ＤＢ更新
        /// <summary>
        /// ＤＢ更新
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        private bool Save(SaveMode mode, SqlBase db)
        {
            if (string.IsNullOrEmpty(_barCode))
            {
                Messages.ShowDialog(Sentence.Messages.NotEntryData, "BARCODE");
                return false;
            }
            if (string.IsNullOrEmpty(_processCode))
            {
                Messages.ShowDialog(Sentence.Messages.NotEntryData, "PROCESS_CODE");
                return false;
            }
            var entryTime = DateTime.Now;
            switch (mode)
            {
                case SaveMode.All:
                    SaveBarcode(db, entryTime);
                    SaveFormula(db, entryTime);
                    SaveJob(db, entryTime);
                    break;
                case SaveMode.Barcode:
                    SaveBarcode(db, entryTime);
                    break;
                case SaveMode.Formula:
                    SaveFormula(db, entryTime);
                    break;
                case SaveMode.Job:
                    SaveJob(db, entryTime);
                    break;
            }
            return true;
        }
        #endregion

        #region 画面表示
        /// <summary>
        /// 画面表示
        /// </summary>
        private void PreviewData(string barCode, string processCode)
        {
            using (var db = new SqlBase(SqlBase.DatabaseKind.SUPERVISOR, SqlBase.TransactionUse.No, Log.ApplicationType.SupervisorInterface))
            {
                var result = db.Select(TbBarcode.GetPreviewAll(barCode, processCode));
                if (result.Rows.Count > 0)
                {
                    //DbToTextBox(_headerTextBoxList, result);
                    //DbToTextBox(_barcodeTextBoxList, result);
                    DbToTextBox(_FormulaTextBoxList, result);
                    //DbToTextBox(_jobTextBoxList, result);
                    ////RdbBrcStatus = int.Parse(result.Rows[0][GetColumnIndex(TbBarcode.BRC_STATUS, result.Columns)].ToString());
                    RdbPrdStatus = int.Parse(result.Rows[0][GetColumnIndex(TbFormula.PRD_STATUS, result.Columns)].ToString());
                    RdbPrdUm = int.Parse(result.Rows[0][GetColumnIndex(TbFormula.PRD_UM, result.Columns)].ToString());
                    RdbPrdIsprefilled = result.Rows[0][GetColumnIndex(TbFormula.PRD_ISPREFILLED, result.Columns)].ToString() == "1";
                    //RdbJobMixing = int.Parse(result.Rows[0][GetColumnIndex(TbJob.JOB_MIXING, result.Columns)].ToString());
                    //RdbJobCapping = int.Parse(result.Rows[0][GetColumnIndex(TbJob.JOB_CAPPING, result.Columns)].ToString());
                    //RdbJobLidPlacing = int.Parse(result.Rows[0][GetColumnIndex(TbJob.JOB_LID_PLACING, result.Columns)].ToString());
                    //RdbJobLidCheck = int.Parse(result.Rows[0][GetColumnIndex(TbJob.JOB_LID_CHECK, result.Columns)].ToString());
                    //RdbJobPrinting1 = int.Parse(result.Rows[0][GetColumnIndex(TbJob.JOB_PRINTING_1, result.Columns)].ToString());
                    //RdbJobPrinting2 = int.Parse(result.Rows[0][GetColumnIndex(TbJob.JOB_PRINTING_2, result.Columns)].ToString());
                    //RdbJobPrinting3 = int.Parse(result.Rows[0][GetColumnIndex(TbJob.JOB_PRINTING_3, result.Columns)].ToString());
                    //RdbJobExitPosition = int.Parse(result.Rows[0][GetColumnIndex(TbJob.JOB_EXIT_POSITION, result.Columns)].ToString());
                    //DrpJobStatus.SelectedValue = result.Rows[0][GetColumnIndex(TbJob.JOB_STATUS, result.Columns)].ToString();
                }
                else
                {
                    //foreach (var item in _headerTextBoxList)
                    //{
                    //    item.TextBox.Text = string.Empty;
                    //}
                    //foreach (var item in _barcodeTextBoxList)
                    //{
                    //    item.TextBox.Text = string.Empty;
                    //}
                    foreach (var item in _FormulaTextBoxList)
                    {
                        item.TextBox.Text = string.Empty;
                    }
                    //foreach (var item in _jobTextBoxList)
                    //{
                    //    item.TextBox.Text = string.Empty;
                    //}
                    //RdbBrcStatus = BRD_STATUS_COROB;
                    RdbPrdStatus = PRD_STATUS_UNDISCHARGED;
                    RdbPrdUm = PRD_UM_CC;
                    RdbPrdIsprefilled = false;
                    //RdbJobMixing = JOB_YES;
                    //RdbJobCapping = JOB_YES;
                    //RdbJobLidPlacing = JOB_YES;
                    //RdbJobLidCheck = JOB_YES;
                    //RdbJobPrinting1 = JOB_NO;
                    //RdbJobPrinting2 = JOB_NO;
                    //RdbJobPrinting3 = JOB_NO;
                    //RdbJobExitPosition = JOB_EXIT_POSITION_MAIN;
                    //DrpJobStatus.SelectedValue = DrpJobStatusDefines[0].Value;
                }
            }
        }
        #endregion

        #endregion
    }
}
