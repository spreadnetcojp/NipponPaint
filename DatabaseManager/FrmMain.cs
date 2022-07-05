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
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DatabaseManager.Dialogs;
using NipponPaint.NpCommon;
using NipponPaint.NpCommon.Database;
using NipponPaint.NpCommon.Settings;
using Sql = NipponPaint.NpCommon.Database.Sql;


#endregion

namespace DatabaseManager
{
    public partial class FrmMain : BaseForm
    {
        #region 定数
        private static readonly Color ALERT_BACK_COLOR_RED = Color.Red;
        private static readonly Color ALERT_BACK_COLOR_WHITE = Color.White;
        private static readonly Color FORE_COLOR = Color.LimeGreen;
        private static readonly Color REJECTED_FORE_COLOR = Color.FromArgb(250, 250, 0);
        private static readonly Color REJECTED_AND_ACKONWLEDGED_FORE_COLOR = Color.FromArgb(130, 130, 0);

        private const string DISPLAY_NAME_NOTIFY_MSG = "メッセージ";
        private const string DISPLAY_NAME_RECEIVE_TIME = "時間(Time)";
        private const string DISPLAY_NAME_REJECTED = "拒否";
        private const string DISPLAY_NAME_ACKNOWLEDGED = "認識";

        //テーブル

        //カラム
        private const string COLUMN_NAME_NOTIFY_MSG = Sql.NpMain.HgLog.COLUMN_NOTIFY_MSG;
        private const string COLUMN_NAME_RECEIVE_TIME = Sql.NpMain.HgLog.COLUMN_RECEIVE_TIME;
        private const string COLUMN_NAME_REJECTED = Sql.NpMain.HgLog.COLUMN_REJECTED;
        private const string COLUMN_NAME_ACKNOWLEDGED = Sql.NpMain.HgLog.COLUMN_ACKNOWLEDGED;

        #endregion

        #region DataGridViewの列定義
        private List<GridViewSetting> ViewSettings = new List<GridViewSetting>()
        {
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_NOTIFY_MSG, DisplayName = DISPLAY_NAME_NOTIFY_MSG, Visible = true, Width = 730, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.DateTime, ColumnName = COLUMN_NAME_RECEIVE_TIME, DisplayName = $"'{DISPLAY_NAME_RECEIVE_TIME}'", Visible = true, Width = 200, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.DateTime, ColumnName = COLUMN_NAME_REJECTED, DisplayName = DISPLAY_NAME_REJECTED, Visible = false, Width = 50, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.DateTime, ColumnName = COLUMN_NAME_ACKNOWLEDGED, DisplayName = DISPLAY_NAME_ACKNOWLEDGED, Visible = false, Width = 50, alignment = DataGridViewContentAlignment.MiddleLeft } },
        };
        #endregion

        #region コンストラクタ
        public FrmMain()
        {
            InitializeComponent();
            InitializeForm();
        }
        #endregion

        #region イベント

        private void FrmKeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyData)
            {
                case Keys.S | Keys.Alt:
                    BtnHGServerStart.PerformClick();
                    break;
                case Keys.A | Keys.Alt:
                    BtnHGServerStop.PerformClick();
                    break;
                case Keys.R | Keys.Alt:
                    BtnHGLogRecognition.PerformClick();
                    break;
                case Keys.C | Keys.Alt:
                    BtnHGLogClear.PerformClick();
                    break;
                case Keys.E | Keys.Alt:
                    BtnChangeENG.PerformClick();
                    break;
                case Keys.J | Keys.Alt:
                    BtnChangeJPN.PerformClick();
                    break;
                case Keys.I | Keys.Alt:
                    BtnSetting.PerformClick();
                    break;
                case Keys.D | Keys.Alt:
                    BtnDBMaintenance.PerformClick();
                    break;
                case Keys.Escape:
                    BtnClose.PerformClick();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// HGサーバー作動
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHGServerStartClick(object sender, EventArgs e)
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
        /// HGサーバー停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHGServerStopClick(object sender, EventArgs e)
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
        /// 認識
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHGLogRecognitionClick(object sender, EventArgs e)
        {
            try
            {
                using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.Yes, Log.ApplicationType.Databasemanager))
                {
                    var parameters = new List<ParameterItem>()
                    {
                        new ParameterItem("acknowledged1", true),
                        new ParameterItem("acknowledged2", false),
                    };
                    db.Execute(Sql.NpMain.HgLog.Acknowledged(), parameters);
                    db.Commit();
                }
                // DgvListの初期設定
                InitializeDgvList();
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        /// <summary>
        /// Clear
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnHGLogClearClick(object sender, EventArgs e)
        {
            try
            {
                using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.Yes, Log.ApplicationType.Databasemanager))
                {
                    var result = Messages.ShowDialog(Sentence.Messages.ClearLogFileInformation);
                    switch (result)
                    {
                        case DialogResult.Yes:
                            db.Execute(Sql.NpMain.HgLog.LogDelete());
                            db.Commit();
                            break;
                        case DialogResult.No:
                            break;
                        default:
                            break;
                    }
                }
                // DgvListの初期設定
                InitializeDgvList();
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        /// <summary>
        /// 英語
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnChangeENGClick(object sender, EventArgs e)
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
        /// 日本語
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnChangeJPNClick(object sender, EventArgs e)
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
        /// 設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSettingClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
                FrmSetting frmSetting = new FrmSetting();
                frmSetting.ShowDialog();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        /// <summary>
        /// データベースメンテナンス
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDBMaintenanceClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
                FrmDBMaintenance frmDBMaintenance = new FrmDBMaintenance();
                frmDBMaintenance.ShowDialog();

            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        /// <summary>
        /// 閉じる
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
        /// Datagridviewをバインドする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvList_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow row in DgvList.Rows)
            {
                int rejectedCell = Convert.ToInt32(row.Cells[DISPLAY_NAME_REJECTED].Value);
                int acknowledgedCell = Convert.ToInt32(row.Cells[DISPLAY_NAME_ACKNOWLEDGED].Value);
                switch (rejectedCell)
                {
                    case 0:
                        row.DefaultCellStyle.ForeColor = FORE_COLOR;
                        row.DefaultCellStyle.SelectionForeColor = FORE_COLOR;
                        break;
                    case 1:
                        if (acknowledgedCell == 0)
                        {
                            row.DefaultCellStyle.ForeColor = REJECTED_FORE_COLOR;
                            row.DefaultCellStyle.SelectionForeColor = REJECTED_FORE_COLOR;
                        }
                        else
                        {
                            row.DefaultCellStyle.ForeColor = REJECTED_AND_ACKONWLEDGED_FORE_COLOR;
                            row.DefaultCellStyle.SelectionForeColor = REJECTED_AND_ACKONWLEDGED_FORE_COLOR;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// データ更新サイクル
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerTick(object sender, EventArgs e)
        {
            // DgvListの初期設定
            InitializeDgvList();
        }

        /// <summary>
        /// サーバーステータスの赤点滅
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmrPnlServerStatusBlinkingTick(object sender, EventArgs e)
        {
            PnlServerStatus.BackColor = !PnlServerStatus.BackColor.Equals(ALERT_BACK_COLOR_RED) ? ALERT_BACK_COLOR_RED : ALERT_BACK_COLOR_WHITE;
        }
        #endregion

        #region private functions

        #region 画面の初期化
        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitializeForm()
        {
            //イベントを追加
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmKeyDown);
            this.BtnClose.Click += new System.EventHandler(this.BtnCloseClick);
            this.BtnSetting.Click += new System.EventHandler(this.BtnSettingClick);
            this.BtnDBMaintenance.Click += new System.EventHandler(this.BtnDBMaintenanceClick);
            this.BtnChangeENG.Click += new System.EventHandler(this.BtnChangeENGClick);
            this.BtnChangeJPN.Click += new System.EventHandler(this.BtnChangeJPNClick);
            this.BtnHGLogRecognition.Click += new System.EventHandler(this.BtnHGLogRecognitionClick);
            this.BtnHGLogClear.Click += new System.EventHandler(this.BtnHGLogClearClick);
            this.BtnHGServerStart.Click += new System.EventHandler(this.BtnHGServerStartClick);
            this.BtnHGServerStop.Click += new System.EventHandler(this.BtnHGServerStopClick);
            this.DgvList.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.DgvList_DataBindingComplete);
            this.Timer.Tick += new System.EventHandler(this.TimerTick);
            this.TmrPnlServerStatusBlinking.Tick += new System.EventHandler(this.TmrPnlServerStatusBlinkingTick);
            // DgvListの初期設定
            InitializeDgvList();
        }
        #endregion

        #region 一覧表示
        /// <summary>
        /// 一覧表示
        /// </summary>
        private void PreviewData()
        {
            // DataGridViewの表示
            using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.No, Log.ApplicationType.Databasemanager))
            {
                var result = db.Select(Sql.NpMain.HgLog.GetPreview(ViewSettings));
                DgvList.DataSource = result;
            }
        }
        #endregion

        #region DgvListの初期設定
        /// <summary>
        /// DgvListの初期設定
        /// </summary>
        private void InitializeDgvList()
        {
            // DataGridViewの初期設定
            InitializeGridView(DgvList);
            // 一覧表示
            PreviewData();
            // 一覧レイアウトの設定
            var cnt = 0;
            foreach (var item in ViewSettings)
            {
                DgvList.Columns[cnt].Width = item.Width;
                DgvList.Columns[cnt].SortMode = DataGridViewColumnSortMode.NotSortable;
                DgvList.Columns[cnt].Visible = item.Visible;
                DgvList.Columns[cnt].DefaultCellStyle.Alignment = item.alignment;
                cnt++;
            }
        }
        #endregion

        #endregion
    }
}
