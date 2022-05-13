﻿//*****************************************************************************
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
#endregion

namespace NipponPaint.OrderManager.Dialogs
{
    public partial class FrmOrderChangeStatusSelectItem : BaseForm
    {
        #region DataGridViewの列定義
        private List<GridViewSetting> ViewSettings = new List<GridViewSetting>()
        {
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = "Order_id", DisplayName = "Order_id", Visible = false, Width = 0, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = "Product_Code", DisplayName = "製品ｺｰﾄﾞ", Visible = true, Width = 60, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = "Status", DisplayName = "Status", Visible = false, Width = 0, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = "StatusText", DisplayName = "ステータス", Visible = true, Width = 130, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = "Operator_Name", DisplayName = "担当者", Visible = true, Width = 100 } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = "HG_Product_Name", DisplayName = "品名", Visible = true, Width = 560 } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = "HG_Volume_Code", DisplayName = "容量ｺｰﾄﾞ", Visible = true, Width = 80, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = "Number_of_cans", DisplayName = "缶数", Visible = true, Width = 50, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = "Order_Number", DisplayName = "注文番号", Visible = true, Width = 150, alignment = DataGridViewContentAlignment.MiddleCenter } },
        };
        #endregion

        #region コンストラクタ
        public FrmOrderChangeStatusSelectItem()
        {
            InitializeComponent();
            InitializeForm();
        }
        #endregion


        #region イベント
        private void BtnChangeStatusClick(object sender, EventArgs e)
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

        #region private functions

        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitializeForm()
        {
            // イベントの追加
            this.BtnChangeStatus.Click += new EventHandler(this.BtnChangeStatusClick);
            this.BtnClose.Click += new EventHandler(this.BtnCloseClick);
            // DataGridViewの初期設定
            InitializeGridView(GvChangeOrders, ViewSettings);
            // DataGridViewの表示
            using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
            {
                var result = db.Select(Sql.NpMain.Orders.GetPreviewCloseOrders(Sql.NpMain.Orders.OrderStatus.TestCanInProgress, BaseSettings.Facility.Plant));
                GvChangeOrders.DataSource = Funcs.ConvertDataTable(result);
            }
            var cnt = 0;
            // DataGridViewのスタイル設定
            GvChangeOrders.ColumnHeadersVisible = false;
            GvChangeOrders.EditMode = DataGridViewEditMode.EditProgrammatically;
            // チェックボックスカラム追加
            var checkboxColumn = new DataGridViewCheckBoxColumn();
            checkboxColumn.Name = "Selected";
            checkboxColumn.HeaderText = "選択";
            checkboxColumn.Width = 30;
            GvChangeOrders.Columns.Insert(0, checkboxColumn);
            // カラム定義
            cnt++;
            foreach (var item in ViewSettings)
            {
                GvChangeOrders.Columns[cnt].Width = item.Width;
                GvChangeOrders.Columns[cnt].Visible = item.Visible;
                GvChangeOrders.Columns[cnt].DefaultCellStyle.Alignment = item.alignment;
                cnt++;
            }
        }
        #endregion

    }
}
