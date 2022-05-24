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
using NipponPaint.NpCommon.IniFile;
using NipponPaint.NpCommon.Database;
using NipponPaint.NpCommon.Database.Sql.SupervisorPc;
using NipponPaint.NpCommon.Database.Sql.NpMain;
using Sql = NipponPaint.NpCommon.Database.Sql;
#endregion


namespace SupervisorPcInterface
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            // タイマー開始前に実行する
            Execute();
            // 周期実行ON
            var settings = new NipponPaint.NpCommon.IniFile.Settings();
            TickTimer.Interval = settings.SupervisorInterface.TickTime * 1000;
            TickTimer.Enabled = true;
        }

        private void TickTimer_Tick(object sender, EventArgs e)
        {
            // 周期実行
            Execute();
        }

        private void Execute()
        {
            var settings = new Settings();
            var dispenses = new DataTable();
            using (var dbs = new SqlBase(SqlBase.DatabaseKind.SUPERVISOR, SqlBase.TransactionUse.Yes, Log.ApplicationType.SupervisorInterface))
            {
                using (var dbn = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.No, Log.ApplicationType.SupervisorInterface))
                {
                    var barcodeRows = dbs.Select(TbBarcode.GetPreviewAll());
                    foreach (DataRow barcodeRow in barcodeRows.Rows)
                    {
                        var barCode = barcodeRow[TbBarcode.BARCODE].ToString();
                        var dispenseRows = dbn.Select(Cans.GetPreviewDispensedByBarcode(barCode, settings.Facility.Plant));
                        foreach (DataRow dispenseRow in dispenseRows.Rows)
                        {

                        }
                    }
                }
            }
        }

    }
}
