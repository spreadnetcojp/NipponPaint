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
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NipponPaint.NpCommon;
using NipponPaint.NpCommon.Database;
using NipponPaint.NpCommon.FormControls;
using NipponPaint.OrderManager.Dialogs;
using NipponPaint.NpCommon.Settings;
using Sql = NipponPaint.NpCommon.Database.Sql;
using System.Data;
using System.Linq;
#endregion

namespace NipponPaint.OrderManager.Dialogs
{
    /// <summary>
    /// 注文開始ダイアログ
    /// </summary>
    public partial class FrmOrderCloseSelectItems : BaseForm
    {
        #region 列定義定数(ColumnName)
        private const string COLUMN_NAME_ORDERS_ORDER_ID = Sql.NpMain.Orders.COLUMN_ORDER_ID;
        private const string COLUMN_NAME_ORDERS_PRODUCT_CODE = Sql.NpMain.Orders.COLUMN_PRODUCT_CODE;
        private const string COLUMN_NAME_ORDERS_STATUS = Sql.NpMain.Orders.COLUMN_STATUS;
        private const string COLUMN_NAME_ORDERS_OPERATOR_NAME = Sql.NpMain.Orders.COLUMN_OPERATOR_NAME;
        private const string COLUMN_NAME_ORDERS_HG_PRODUCT_NAME = Sql.NpMain.Orders.COLUMN_HG_PRODUCT_NAME;
        private const string COLUMN_NAME_ORDERS_HG_VOLUME_CODE = Sql.NpMain.Orders.COLUMN_HG_VOLUME_CODE;
        private const string COLUMN_NAME_ORDERS_NUMBER_OF_CAN = Sql.NpMain.Orders.COLUMN_NUMBER_OF_CAN;
        private const string COLUMN_NAME_ORDERS_ORDER_NUMBER = Sql.NpMain.Orders.COLUMN_ORDER_NUMBER;
        private const string COLUMN_NAME_ORDERS_STATSU_TEXT = "StatusText";
        #endregion

        #region 列定義定数(DisplayName)
        private const string DISPLAY_NAME_ORDER_ID = "Order_id";
        private const string DISPLAY_NAME_STATUS = "Status";
        private const string DISPLAY_NAME_PRODUCT_CODE = "製品ｺｰﾄﾞ";
        private const string DISPLAY_NAME_HG_PRODUCT_NAME = "品名";
        private const string DISPLAY_NAME_HG_VOLUME_CODE = "容量ｺｰﾄﾞ";
        private const string DISPLAY_NAME_NUMBER_OF_CAN = "缶数";
        private const string DISPLAY_NAME_OPERATOR_NAME = "担当者";
        private const string DISPLAY_NAME_ORDER_NUMBER = "注文番号";
        private const string DISPLAY_NAME_STATUS_TEXT = "ステータス";
        #endregion

        /// <summary>
        /// 列の先頭に追加しているので、インデックスは「０」
        /// </summary>
        private const int CHECKEDBOX_COLUMN = 0;

        #region DataGridViewの列定義
        private List<GridViewSetting> ViewSettings = new List<GridViewSetting>()
        {
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_ORDER_ID, DisplayName = DISPLAY_NAME_ORDER_ID, Visible = false, Width = 0, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_ORDERS_PRODUCT_CODE, DisplayName = DISPLAY_NAME_PRODUCT_CODE, Visible = true, Width = 60, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_ORDERS_STATUS, DisplayName = DISPLAY_NAME_STATUS, Visible = false, Width = 0, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_ORDERS_STATSU_TEXT, DisplayName = DISPLAY_NAME_STATUS_TEXT, Visible = true, Width = 130, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_ORDERS_OPERATOR_NAME, DisplayName = DISPLAY_NAME_OPERATOR_NAME, Visible = true, Width = 100 } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_ORDERS_HG_PRODUCT_NAME, DisplayName = DISPLAY_NAME_HG_PRODUCT_NAME, Visible = true, Width = 560 } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_ORDERS_HG_VOLUME_CODE, DisplayName = DISPLAY_NAME_HG_VOLUME_CODE, Visible = true, Width = 80, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_NUMBER_OF_CAN, DisplayName = DISPLAY_NAME_NUMBER_OF_CAN, Visible = true, Width = 50, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_ORDER_NUMBER, DisplayName = DISPLAY_NAME_ORDER_NUMBER, Visible = true, Width = 150, alignment = DataGridViewContentAlignment.MiddleCenter } },
        };
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="selectedStatus"></param>
        /// <param name="plant"></param>
        public FrmOrderCloseSelectItems(Sql.NpMain.Orders.OrderStatus selectedStatus)
        {
            InitializeComponent();
            InitializeForm(selectedStatus);
        }
        #endregion

        #region イベント
        /// <summary>
        /// 注文を閉じるボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOrderCloseClick(object sender, EventArgs e)
        {
            try
            {
                var frmMain = new FrmMain();
                // 注文番号をリスト化
                var orderNumbers = new List<string>();
                // 注文番号のIndex取得
                var orderNumberIndex = GvCloseOrders.Columns[COLUMN_NAME_ORDERS_ORDER_NUMBER].Index;
                // 選択している注文番号をリストに格納
                foreach (DataGridViewRow row in GvCloseOrders.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[CHECKEDBOX_COLUMN].Value))
                    {
                        orderNumbers.Add(row.Cells[orderNumberIndex].Value.ToString());
                    }
                }
                if (orderNumbers.Any())
                {
                    if (Messages.ShowDialog(Sentence.Messages.BtnOrderCloseMultipleClicked) == DialogResult.Yes)
                    {
                        frmMain.DeleteOrdersConfirmation(orderNumbers);
                    }
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
        public void BtnCloseClick(object sender, EventArgs e)
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
        /// <summary>
        /// 全て選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChkSelectAllChecked(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in GvCloseOrders.Rows)
                {
                    row.Cells[CHECKEDBOX_COLUMN].Value = ((CheckBox)sender).CheckState == CheckState.Checked ? true : false;
                }
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        #region private functions

        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitializeForm(Sql.NpMain.Orders.OrderStatus selectedStatus)
        {
            // イベントの追加
            this.BtnOrderClose.Click += new EventHandler(this.BtnOrderCloseClick);
            this.BtnClose.Click += new EventHandler(this.BtnCloseClick);
            // DataGridViewの初期設定
            InitializeGridView(GvCloseOrders, ViewSettings);
            // DataGridViewの表示
            using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
            {
                GvCloseOrders.DataSource = db.Select(Sql.NpMain.Orders.GetPreviewCloseOrders(selectedStatus, BaseSettings.Facility.Plant));
            }
            var cnt = 0;
            // DataGridViewのスタイル設定
            GvCloseOrders.ColumnHeadersVisible = false;
            //GvCloseOrders.EditMode = DataGridViewEditMode.EditProgrammatically;
            // チェックボックスカラム追加
            var checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.Name = "Selected";
            checkboxColumn.HeaderText = "選択";
            checkboxColumn.Width = 30;
            GvCloseOrders.Columns.Insert(0, checkboxColumn);
            // カラム定義
            cnt++;
            foreach (var item in ViewSettings)
            {
                GvCloseOrders.Columns[cnt].Width = item.Width;
                GvCloseOrders.Columns[cnt].Visible = item.Visible;
                GvCloseOrders.Columns[cnt].DefaultCellStyle.Alignment = item.alignment;
                cnt++;
            }
            GvCloseOrders.ReadOnly = false;
            // 全て選択イベント追加
            this.ChkSelectAll.CheckedChanged += new EventHandler(this.ChkSelectAllChecked);
        }
        #endregion
    }
}

