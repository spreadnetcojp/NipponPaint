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
using NipponPaint.NpCommon;
using NipponPaint.NpCommon.Database;
using NipponPaint.NpCommon.Settings;
#endregion

namespace NipponPaint.OrderManager.Dialogs
{
    /// <summary>
    /// 出庫ダイアログ
    /// </summary>
    public partial class FrmShipping : BaseForm
    {
        #region 定数
        private const string COLUMN_ORDERS_HG_PAINT_KIND_CODE = NpCommon.Database.Sql.NpMain.Orders.COLUMN_HG_PAINT_KIND_CODE;
        private const string COLUMN_ORDERS_HG_THEME_CODE = NpCommon.Database.Sql.NpMain.Orders.COLUMN_HG_THEME_CODE;
        private const string PAINT_KIND_CODE = NpCommon.Database.Sql.NpMain.Orders.PAINT_KIND_CODE;
        private const string THEME_CODE = NpCommon.Database.Sql.NpMain.Orders.THEME_CODE;
        #endregion

        #region メンバ変数
        private static DataTable DgvListDataSource;
        #endregion

        #region 列定義
        private List<GridViewSetting> ViewSettingsWareHouse = new List<GridViewSetting>()
        {
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_ORDERS_HG_PAINT_KIND_CODE, DisplayName = PAINT_KIND_CODE, Visible = true, Width = 135, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_ORDERS_HG_THEME_CODE, DisplayName = THEME_CODE, Visible = true, Width = 135, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = NpCommon.Database.Sql.NpMain.Orders.TintingPriceRank.A.ToString(), DisplayName = NpCommon.Database.Sql.NpMain.Orders.TintingPriceRank.A.ToString(), Visible = true, Width = 88, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = NpCommon.Database.Sql.NpMain.Orders.TintingPriceRank.B.ToString(), DisplayName = NpCommon.Database.Sql.NpMain.Orders.TintingPriceRank.B.ToString(), Visible = true, Width = 88, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = NpCommon.Database.Sql.NpMain.Orders.TintingPriceRank.C.ToString(), DisplayName = NpCommon.Database.Sql.NpMain.Orders.TintingPriceRank.C.ToString(), Visible = true, Width = 88, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = NpCommon.Database.Sql.NpMain.Orders.TintingPriceRank.D.ToString(), DisplayName = NpCommon.Database.Sql.NpMain.Orders.TintingPriceRank.D.ToString(), Visible = true, Width = 88, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = NpCommon.Database.Sql.NpMain.Orders.TintingPriceRank.DY.ToString(), DisplayName = NpCommon.Database.Sql.NpMain.Orders.TintingPriceRank.DY.ToString(), Visible = true, Width = 88, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = NpCommon.Database.Sql.NpMain.Orders.TintingPriceRank.DG.ToString(), DisplayName = NpCommon.Database.Sql.NpMain.Orders.TintingPriceRank.DG.ToString(), Visible = true, Width = 88, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = NpCommon.Database.Sql.NpMain.Orders.TintingPriceRank.DB.ToString(), DisplayName = NpCommon.Database.Sql.NpMain.Orders.TintingPriceRank.DB.ToString(), Visible = true, Width = 88, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = NpCommon.Database.Sql.NpMain.Orders.TintingPriceRank.DR.ToString(), DisplayName = NpCommon.Database.Sql.NpMain.Orders.TintingPriceRank.DR.ToString(), Visible = true, Width = 88, alignment = DataGridViewContentAlignment.MiddleRight } },
        };
        #endregion

        #region コンストラクタ
        public FrmShipping()
        {
            InitializeComponent();
            InitializeForm();
        }
        #endregion

        #region イベント

        /// <summary>
        /// 終了ボタン
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
            catch(Exception ex)
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
            this.BtnClose.Click += new EventHandler(this.BtnCloseClick);

            InitializeGridView(DgvList, ViewSettingsWareHouse);
            using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
            {
                DgvListDataSource = db.Select(NpCommon.Database.Sql.NpMain.Orders.GetWareHouse(BaseSettings.Facility.Plant));
                DgvList.DataSource = DgvListDataSource;
            }
            // 一覧レイアウトの設定
            var cnt = 0;
            foreach (var item in ViewSettingsWareHouse)
            {
                DgvList.Columns[cnt].Width = item.Width;
                DgvList.Columns[cnt].Visible = item.Visible;
                DgvList.Columns[cnt].DefaultCellStyle.Alignment = item.alignment;
                cnt++;
            }
        }
        #endregion
    }
}