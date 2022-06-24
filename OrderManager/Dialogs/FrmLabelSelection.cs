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
using System.Collections.Generic;
using NipponPaint.NpCommon;
using NipponPaint.NpCommon.Database;
using NipponPaint.NpCommon.Settings;
using Sql = NipponPaint.NpCommon.Database.Sql;
#endregion

namespace NipponPaint.OrderManager.Dialogs
{
    /// <summary>
    /// ラベル(仮)ダイアログ
    /// </summary>
    public partial class FrmLabelSelection : BaseForm
    {
        #region 定数
        private enum EditParameter
        {
            /// <summary>
            /// 新規作成
            /// </summary>
            Create,
            /// <summary>
            /// 複写して新規作成
            /// </summary>
            CopyAndCreate,
            /// <summary>
            /// 修正
            /// </summary>
            Modify,
        }

        private const string DISPLAY_NAME_LABEL_TYPE = "ラベルタイプ";
        private const string DISPLAY_NAME_LABEL_DESCRIPTION = "ラベル詳細";
        //テーブル
        private const string LABELS_TABLE = Sql.Order.Labels.MAIN_TABLE;
        //カラム
        private const string COLUMN_NAME_LABEL_TYPE = Sql.Order.Labels.COLUMN_LABEL_TYPE;
        private const string COLUMN_NAME_LABEL_DESCRIPTION = Sql.Order.Labels.COLUMN_LABLE_DESCRIPTION;
        #endregion

        #region DataGridViewの列定義
        private List<GridViewSetting> ViewSettings = new List<GridViewSetting>()
        {
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_LABEL_TYPE, DisplayName = DISPLAY_NAME_LABEL_TYPE, Visible = true, Width = 120, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_LABEL_DESCRIPTION, DisplayName = DISPLAY_NAME_LABEL_DESCRIPTION, Visible = true, Width = 820, alignment = DataGridViewContentAlignment.MiddleLeft } },
        };
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="vm"></param>
        public FrmLabelSelection(ViewModels.LabelTypeData vm)
        {
            InitializeComponent();
            InitializeForm();
            TxtLabelType.Value = vm.LabelType.ToString();
            TxtLabelDescription.Value = vm.LabelDescription;
        }
        #endregion

        #region イベント
        /// <summary>
        /// ダイアログ表示後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmLabelSelectionShown(object sender, EventArgs e)
        {
            PutLog(Sentence.Messages.OpenMainForm);
        }
        /// <summary>
        /// 一覧の行選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgvListSelectionChanged(object sender, EventArgs e)
        {
            var columnIndex = ViewSettings.FindIndex(x => x.ColumnName == COLUMN_NAME_LABEL_TYPE);
            var dgv = (DataGridView)sender;
            if (dgv.SelectedRows.Count > 0)
            {
                // 選択している行を取得
                var selectedRow = dgv.SelectedRows[0];
                int.TryParse(selectedRow.Cells[columnIndex].Value.ToString(), out int labelType);
                using (var db = new SqlBase(SqlBase.DatabaseKind.ORDER, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
                {
                    // 行取得のSQLを作成
                    var parameter = new List<ParameterItem>()
                    {
                        new ParameterItem("labelType", labelType),
                    };
                    var rec = db.Select(Sql.Order.Labels.GetDetail(), parameter);
                    // フォームで定義された、取得値設定先のコントロールを抽出する
                    db.ToLabelTextBox(this.Controls, rec.Rows);
                }
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
                    case Keys.F5:
                        // 新規
                        BtnCreate.PerformClick();
                        break;
                    case Keys.F6:
                        // 複製して新規作成
                        BtnCopyCreate.PerformClick();
                        break;
                    case Keys.F2:
                        // 修正
                        BtnModify.PerformClick();
                        break;
                    case Keys.F4:
                        // 削除
                        BtnDelete.PerformClick();
                        break;
                }
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 新規作成ボタン(F5)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCreateClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
                var vm = new ViewModels.LabelEdit();
                //新規作成
                vm.Parameter = (int)EditParameter.Create;
                //ラベルタイプ0"Template Label"を選択
                vm.LabelType = 0;
                FrmLabelEdit frmLabelEdit = new FrmLabelEdit(vm);
                frmLabelEdit.ShowDialog();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 複製して新規作成ボタン(F6)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnCopyCreateClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
                var vm = new ViewModels.LabelEdit();
                //複製して新規作成
                vm.Parameter = (int)EditParameter.CopyAndCreate;
                int.TryParse(TxtLabelType.Value, out int labelType);
                vm.LabelType = labelType;
                FrmLabelEdit frmLabelEdit = new FrmLabelEdit(vm);
                frmLabelEdit.ShowDialog();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 修正ボタン(F2)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnModifyClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
                var vm = new ViewModels.LabelEdit();
                vm.Parameter = (int)EditParameter.Modify;
                int.TryParse(TxtLabelType.Value, out int labelType);
                vm.LabelType = labelType;
                FrmLabelEdit frmLabelEdit = new FrmLabelEdit(vm);
                frmLabelEdit.ShowDialog();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 削除ボタン(F4)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDeleteClick(object sender, EventArgs e)
        {
            try
            {
                var result = Messages.ShowDialog(Sentence.Messages.SelectedLabelTypeWillBeDeletedContinue);
                switch (result)
                {
                    case DialogResult.Yes:
                        break;
                    case DialogResult.No:
                        break;
                    default:
                        break;
                }
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 終了ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnExitClick(object sender, EventArgs e)
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
            //イベントの追加
            this.Shown += new System.EventHandler(this.FrmLabelSelectionShown);
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(this.FormKeyDown);
            this.BtnCreate.Click += new System.EventHandler(this.BtnCreateClick);
            this.BtnCopyCreate.Click += new System.EventHandler(this.BtnCopyCreateClick);
            this.BtnModify.Click += new System.EventHandler(this.BtnModifyClick);
            this.BtnDelete.Click += new System.EventHandler(this.BtnDeleteClick);
            this.BtnExit.Click += new System.EventHandler(this.BtnExitClick);
            this.DgvList.SelectionChanged += new System.EventHandler(this.DgvListSelectionChanged);
            // DataGridViewの初期設定
            InitializeGridView(DgvList);
            // 一覧表示
            PreviewData();
            // 一覧レイアウトの設定
            var cnt = 0;
            foreach (var item in ViewSettings)
            {
                DgvList.Columns[cnt].SortMode = DataGridViewColumnSortMode.NotSortable;
                DgvList.Columns[cnt].Width = item.Width;
                DgvList.Columns[cnt].Visible = item.Visible;
                DgvList.Columns[cnt].DefaultCellStyle.Alignment = item.alignment;
                cnt++;
            }
            //データ表示部の設定
            Funcs.SetControlEnabled(this.Controls, true);
        }
        #endregion

        #region DataGridView初期化 
        /// <summary>
        /// DataGridView初期化 
        /// </summary>
        private void InitializeGridViewLayout()
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
                DgvList.Columns[cnt].Visible = item.Visible;
                DgvList.Columns[cnt].DefaultCellStyle.Alignment = item.alignment;
                cnt++;
            }
        }
        #endregion

        #region 一覧表示
        /// <summary>
        /// 一覧表示
        /// </summary>
        private void PreviewData()
        {
            //DataGridViewの表示
            using (var db = new SqlBase(SqlBase.DatabaseKind.ORDER, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
            {
                var result = db.Select(Sql.Order.Labels.GetPreview(ViewSettings));
                DgvList.DataSource = result;
            }
        }
        #endregion
    }
}
#endregion