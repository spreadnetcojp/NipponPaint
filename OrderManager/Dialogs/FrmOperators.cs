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
using NipponPaint.NpCommon.Database;
using NipponPaint.NpCommon.Settings;
using Sql = NipponPaint.NpCommon.Database.Sql;
#endregion

namespace NipponPaint.OrderManager.Dialogs
{
    /// <summary>
    /// 担当者を決定ダイアログ
    /// </summary>
    public partial class FrmOperators : BaseForm
    {
        #region 定数
        private const string COLUMN_OPERATORS_OPERATOR_CODE = Sql.NpMain.Operators.COLUMN_OPERATOR_CODE;
        private const string COLUMN_OPERATORS_OPERATOR_NAME = Sql.NpMain.Operators.COLUMN_OPERATOR_NAME;
        private const string DISPLAY_OPERATOR_CODE = "' '";
        private const string DISPLAY_OPERATOR_NAME = "担当者";
        /// <summary>
        /// 選択している行の取得用、1件選択なので「0」
        /// </summary>
        private const int SELECTED_ROW = 0;
        private const int COLUMN_OPERATOR_CODE = 0;
        private const int COLUMN_OPERATOR_NAME = 1;
        #endregion

        #region メンバ変数
        private int orderId = 0;
        #endregion

        #region DataGridViewの列定義
        private List<GridViewSetting> ViewSettings = new List<GridViewSetting>()
        {
            { new GridViewSetting() {ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_OPERATORS_OPERATOR_CODE, DisplayName = DISPLAY_OPERATOR_CODE, Visible = true, Width = 35, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() {ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_OPERATORS_OPERATOR_NAME, DisplayName = DISPLAY_OPERATOR_NAME, Visible = true, Width = 430, alignment = DataGridViewContentAlignment.MiddleLeft } },
        };
        #endregion

        #region コンストラクタ
        public FrmOperators(int id)
        {
            InitializeComponent();
            InitializeForm();
            orderId = id;
        }
        #endregion

        #region イベント
        /// <summary>
        /// ダイアログ表示後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmOperatorsShown(object sender, EventArgs e)
        {
            PutLog(Sentence.Messages.OpenMainForm);
        }
        /// <summary>
        /// 担当者を決定ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAssignClick(object sender, EventArgs e)
        {
            try
            {
                // 選択した担当者のCODE取得
                var operatorCode = DgvListLeft.SelectedRows[SELECTED_ROW].Cells[COLUMN_OPERATOR_CODE].Value.ToString();
                // 選択した担当者のName取得
                var operatorName = DgvListLeft.SelectedRows[SELECTED_ROW].Cells[COLUMN_OPERATOR_NAME].Value.ToString();
                var paramaters = new List<ParameterItem>()
                {
                    new ParameterItem("@OperatorCode", operatorCode),
                    new ParameterItem("@OperatorName", operatorName),
                    new ParameterItem("@OrderId", orderId),
                };
                using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.Yes, Log.ApplicationType.OrderManager))
                {
                    db.Execute(Sql.NpMain.Orders.DecideOperator(), paramaters);
                    db.Commit();
                }
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
                this.Close();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 閉じるボタン
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
        #endregion

        #region 画面の初期化
        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitializeForm()
        {
            //コントロールの配置

            //イベントの追加
            this.Shown += new System.EventHandler(this.FrmOperatorsShown);
            this.KeyPreview = true;
            this.BtnAssign.Click += new EventHandler(this.BtnAssignClick);
            this.BtnClose.Click += new EventHandler(this.BtnCloseClick);
            //this.DgvListLeft.SelectionChanged += new EventHandler(this.DgvListLeftSelectionChanged);
            // DataGridViewの初期設定
            InitializeGridView(DgvListLeft);
            //InitializeGridView(DgvListRight);
            // 一覧表示
            PreviewData();
            // 一覧レイアウトの設定
            var cnt = 0;
            foreach (var item in ViewSettings)
            {
                DgvListLeft.Columns[cnt].SortMode = DataGridViewColumnSortMode.NotSortable;　　　　　//DataGridView(左)内ソート禁止
                DgvListLeft.Columns[cnt].Width = item.Width;
                DgvListLeft.Columns[cnt].Visible = item.Visible;
                DgvListLeft.Columns[cnt].DefaultCellStyle.Alignment = item.alignment;
                //DgvListRight.Columns[cnt].SortMode = DataGridViewColumnSortMode.NotSortable;　　　　　//DataGridView(右)内ソート禁止
                //DgvListRight.Columns[cnt].Width = item.Width;
                //DgvListRight.Columns[cnt].Visible = item.Visible;
                //DgvListRight.Columns[cnt].DefaultCellStyle.Alignment = item.alignment;
                cnt++;
            }
            // データ表示部の設定
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
            InitializeGridView(DgvListLeft);
            // 一覧表示
            PreviewData();
            // 一覧レイアウトの設定
            var cnt = 0;
            foreach (var item in ViewSettings)
            {
                DgvListLeft.Columns[cnt].Width = item.Width;
                DgvListLeft.Columns[cnt].Visible = item.Visible;
                DgvListLeft.Columns[cnt].DefaultCellStyle.Alignment = item.alignment;
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
            using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
            {
                var result = db.Select(Sql.NpMain.Operators.GetPreviewOperators(ViewSettings));
                DgvListLeft.DataSource = result;
                //DgvListRight.DataSource = result;
            }
        }
        #endregion


        private void FrmOperators_Load(object sender, EventArgs e)
        {

        }
    }
}
