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
        private const string COLUMN_ORDERS_HG_PAINT_KIND_CODE = NpCommon.Database.Sql.NpMain.Orders.COLUMN_HG_PAINT_KIND_CODE;
        private const string COLUMN_ORDERS_HG_THEME_CODE = NpCommon.Database.Sql.NpMain.Orders.COLUMN_HG_THEME_CODE;
        private const string PAINT_KIND_CODE = NpCommon.Database.Sql.NpMain.Orders.PAINT_KIND_CODE;
        private const string THEME_CODE = NpCommon.Database.Sql.NpMain.Orders.THEME_CODE;

        /// <summary>
        /// 調色ランク
        /// </summary>
        private enum TintingPriceRank
        {
            A,
            B,
            C,
            D,
            DY,
            DG,
            DB,
            DR
        }

        private static DataTable DgvListDataSource;

        private List<GridViewSetting> ViewSettingsWareHouse = new List<GridViewSetting>()
        {
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_ORDERS_HG_PAINT_KIND_CODE, DisplayName = PAINT_KIND_CODE, Visible = true, Width = 135, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_ORDERS_HG_THEME_CODE, DisplayName = THEME_CODE, Visible = true, Width = 135, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = TintingPriceRank.A.ToString(), DisplayName = TintingPriceRank.A.ToString(), Visible = true, Width = 88, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = TintingPriceRank.B.ToString(), DisplayName = TintingPriceRank.B.ToString(), Visible = true, Width = 88, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = TintingPriceRank.C.ToString(), DisplayName = TintingPriceRank.C.ToString(), Visible = true, Width = 88, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = TintingPriceRank.D.ToString(), DisplayName = TintingPriceRank.D.ToString(), Visible = true, Width = 88, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = TintingPriceRank.DY.ToString(), DisplayName = TintingPriceRank.DY.ToString(), Visible = true, Width = 88, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = TintingPriceRank.DG.ToString(), DisplayName = TintingPriceRank.DG.ToString(), Visible = true, Width = 88, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = TintingPriceRank.DB.ToString(), DisplayName = TintingPriceRank.DB.ToString(), Visible = true, Width = 88, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = TintingPriceRank.DR.ToString(), DisplayName = TintingPriceRank.DR.ToString(), Visible = true, Width = 88, alignment = DataGridViewContentAlignment.MiddleRight } },
        };

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