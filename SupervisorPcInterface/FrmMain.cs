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
        private Settings _settings;

        public FrmMain()
        {
            InitializeComponent();
            _settings = new Settings();
        }

        private void FrmMain_Shown(object sender, EventArgs e)
        {
            // タイマー開始前に実行する
            Execute();
            // 周期実行ON
            TickTimer.Interval = _settings.SupervisorInterface.TickTime * 1000;
            TickTimer.Enabled = true;
        }

        private void TickTimer_Tick(object sender, EventArgs e)
        {
            // 周期実行
            Execute();
        }

        private void Execute()
        {
            using (var dbs = new SqlBase(SqlBase.DatabaseKind.SUPERVISOR, SqlBase.TransactionUse.Yes, Log.ApplicationType.SupervisorInterface))
            {
                try
                {
                    PutLog(Sentence.Messages.StartSupervisorInterface);
                    var barcodeRows = dbs.Select(TbBarcode.GetPreview());
                    using (var dbn = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.No, Log.ApplicationType.SupervisorInterface))
                    {
                        foreach (DataRow barcodeRow in barcodeRows.Rows)
                        {
                            var barCode = barcodeRow[TbBarcode.BARCODE].ToString();
                            PutLog(Sentence.Messages.ExecuteSupervisorInterface, barCode);
                            var dispenseRows = dbn.Select(Cans.GetPreviewDispensedByBarcode(barCode, _settings.Facility.Plant));
                            var cnt = 0;
                            var updateDateTime = DateTime.Now;
                            var parameters = new List<ParameterItem>();
                            var sql = string.Empty;
                            foreach (DataRow dispenseRow in dispenseRows.Rows)
                            {
                                if (cnt == 0)
                                {
                                    sql = SetSqlBarcode(barcodeRow, dispenseRow, updateDateTime, out parameters);
                                    dbs.Execute(sql, parameters);
                                    sql = SetSqlJob(barcodeRow, dispenseRow, updateDateTime, out parameters);
                                    dbs.Execute(sql, parameters);
                                }
                                sql = SetSqlFormura(barcodeRow, dispenseRow, updateDateTime, out parameters);
                                dbs.Execute(sql, parameters);
                                cnt++;
                            }
                        }
                    }
                    dbs.Commit();
                    PutLog(Sentence.Messages.EndSupervisorInterface);
                }
                catch (Exception ex)
                {
                    dbs.Rollback();
                    PutLog(ex, false);
                }
            }
        }

        /// <summary>
        /// TB_FORMURA更新
        /// </summary>
        /// <param name="barcodeRow"></param>
        /// <param name="dispenseRow"></param>
        /// <param name="updateDateTime"></param>
        /// <param name="resultParameters"></param>
        /// <returns></returns>
        private string SetSqlFormura(DataRow barcodeRow,
                                     DataRow dispenseRow,
                                     DateTime updateDateTime,
                                     out List<ParameterItem> parameters)
        {
            // SQL作成
            var sql = TbFormula.Merge(out List<ParameterItem> items);
            /// SQLパラメータの値設定
            items.Find(x => x.Key == TbFormula.PRD_BARCODE).Value = barcodeRow[TbBarcode.BARCODE].ToString();
            items.Find(x => x.Key == TbFormula.PRD_PROCESS_CODE).Value = barcodeRow[TbBarcode.PROCESS_CODE].ToString();
            items.Find(x => x.Key == TbFormula.PRD_TIME_INSERTED).Value = updateDateTime;
            items.Find(x => x.Key == TbFormula.PRD_STATUS).Value = 0;
            items.Find(x => x.Key == TbFormula.PRD_CODE).Value = dispenseRow["Code"];
            items.Find(x => x.Key == TbFormula.PRD_DESC).Value = dispenseRow["Code"];
            items.Find(x => x.Key == TbFormula.PRD_UM).Value = 1;
            items.Find(x => x.Key == TbFormula.PRD_SPECIFIC_GRAVITY).Value = DBNull.Value;
            items.Find(x => x.Key == TbFormula.PRD_QTY_REQ).Value = dispenseRow["Weight"];
            items.Find(x => x.Key == TbFormula.PRD_QTY_DISP).Value = (float)0;
            items.Find(x => x.Key == TbFormula.PRD_START_DISP).Value = DBNull.Value;
            items.Find(x => x.Key == TbFormula.PRD_END_DISP).Value = DBNull.Value;
            items.Find(x => x.Key == TbFormula.PRD_PRIORITY).Value = DBNull.Value;
            items.Find(x => x.Key == TbFormula.PRD_NUM).Value = DBNull.Value;
            items.Find(x => x.Key == TbFormula.PRD_ISPREFILLED).Value = 1;
            items.Find(x => x.Key == TbFormula.PRD_PREFILLED_QTY).Value = dispenseRow["Dispensed"];
            parameters = new List<ParameterItem>();
            parameters.AddRange(items);
            // SQL返却
            return sql;
        }

        /// <summary>
        /// TB_BARCODE更新
        /// </summary>
        /// <param name="barcodeRow"></param>
        /// <param name="dispenseRow"></param>
        /// <param name="updateDateTime"></param>
        /// <param name="resultParameters"></param>
        /// <returns></returns>
        private string SetSqlBarcode(DataRow barcodeRow,
                                     DataRow dispenseRow,
                                     DateTime updateDateTime,
                                     out List<ParameterItem> parameters)
        {
            // SQL作成
            var sql = TbBarcode.Update();
            /// パラメータ設定
            parameters = new List<ParameterItem>();
            parameters.Add(new ParameterItem($"{TbBarcode.BARCODE}", barcodeRow[TbBarcode.BARCODE].ToString()));
            parameters.Add(new ParameterItem($"{TbBarcode.PROCESS_CODE}", barcodeRow[TbBarcode.PROCESS_CODE].ToString()));
            parameters.Add(new ParameterItem($"{TbBarcode.BRC_TIME_PROCESSED}", updateDateTime));
            parameters.Add(new ParameterItem($"{TbBarcode.BRC_STATUS}", 1));
            parameters.Add(new ParameterItem($"{TbBarcode.BRC_ERR_1}", dispenseRow["Errors_1"]));
            parameters.Add(new ParameterItem($"{TbBarcode.BRC_ERR_2}", dispenseRow["Errors_2"]));
            parameters.Add(new ParameterItem($"{TbBarcode.BRC_ERR_3}", dispenseRow["Errors_3"]));
            // SQL返却
            return sql;
        }

        /// <summary>
        /// TB_JOB更新
        /// </summary>
        /// <param name="barcodeRow"></param>
        /// <param name="dispenseRow"></param>
        /// <param name="updateDateTime"></param>
        /// <param name="resultParameters"></param>
        /// <returns></returns>
        private string SetSqlJob(DataRow barcodeRow,
                                 DataRow dispenseRow,
                                 DateTime updateDateTime,
                                 out List<ParameterItem> parameters)
        {
            // SQL作成
            var sql = TbJob.Merge(out List<ParameterItem> items);
            /// パラメータ設定
            items.Find(x => x.Key == TbJob.JOB_BARCODE).Value = barcodeRow[TbBarcode.BARCODE].ToString();
            items.Find(x => x.Key == TbJob.JOB_PROCESS_CODE).Value = barcodeRow[TbBarcode.PROCESS_CODE].ToString();
            items.Find(x => x.Key == TbJob.JOB_TIME_INSERTED).Value = updateDateTime;
            items.Find(x => x.Key == TbJob.JOB_STATUS).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_TARE_WEIGHT_EXPECTED).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_TARE_WEIGHT_DETECTED).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_TARE_WEIGHT_PERC_ERR_ADMITTED).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_GROSS_WEIGHT_EXPECTED).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_GROSS_WEIGHT_DETECTED).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_GROSS_WEIGHT_PERC_ERR_ADMITTED).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_NET_WEIGHT_EXPECTED).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_NET_WEIGHT_DETECTED).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_NET_WEIGHT_PERC_ERR_ADMITTED).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_TOT_COLORANT_WEIGHT_EXPECTED).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_TOT_COLORANT_WEIGHT_DETECTED).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_TOT_COLORANT_WEIGHT_PERC_ERR_ADMITTED).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_TOT_GRAVIMETRIC_WEIGHT_EXPECTED).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_TOT_GRAVIMETRIC_WEIGHT_DETECTED).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_TOT_GRAVIMETRIC_WEIGHT_PERC_ERR_ADMITTED).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_MIXING).Value = 1;
            items.Find(x => x.Key == TbJob.JOB_MIXING_TIME).Value = dispenseRow["Mixing_Time"];
            items.Find(x => x.Key == TbJob.JOB_MIXING_SPEED).Value = dispenseRow["Mixing_Speed"]; // TODO：計算
            items.Find(x => x.Key == TbJob.JOB_CAPPING).Value = dispenseRow["Cap_Type"].ToString() == "0" ? 0 : 1;
            items.Find(x => x.Key == TbJob.JOB_LID_PLACING).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_LID_CHECK).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_PRINTING_1).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_PRINTING_2).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_PRINTING_3).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_EXIT_POSITION).Value = dispenseRow["Can_Number"].ToString() == "1" ? 2 : 1;
            items.Find(x => x.Key == TbJob.JOB_TAG_1).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_TAG_2).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_TAG_3).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_TAG_4).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_TAG_5).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_ERR_1).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_ERR_2).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_ERR_3).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_ERR_4).Value = DBNull.Value;
            items.Find(x => x.Key == TbJob.JOB_ERR_5).Value = DBNull.Value;
            parameters = new List<ParameterItem>();
            parameters.AddRange(items);
            // SQL返却
            return sql;
        }

        #region ログ出力
        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="MessageId"></param>
        private void PutLog(Sentence.Messages MessageId, object[] addtionalInfo = null, bool displayDialog = false)
        {
            Log.Write(MessageId, Log.ApplicationType.SupervisorInterface, addtionalInfo);
            if (displayDialog)
            {
                Messages.ShowDialog(MessageId, addtionalInfo);
            }
        }
        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="MessageId"></param>
        private void PutLog(Sentence.Messages MessageId, string addtionalInfo, bool displayDialog = false)
        {
            Log.Write(MessageId, Log.ApplicationType.SupervisorInterface, addtionalInfo);
            if (displayDialog)
            {
                Messages.ShowDialog(MessageId, addtionalInfo);
            }
        }
        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="MessageId"></param>
        private void PutLog(Exception ex, bool displayDialog = true)
        {
            Log.Write(Sentence.Messages.Exception, Log.ApplicationType.SupervisorInterface, ex.Message);
            if (displayDialog)
            {
                Messages.ShowDialog(Sentence.Messages.Exception, ex.Message);
            }
        }
        #endregion

    }
}
