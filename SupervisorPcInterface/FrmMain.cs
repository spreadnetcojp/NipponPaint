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

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public FrmMain()
        {
            InitializeComponent();
            _settings = new Settings();
            // ログファイルのクリーンアップ
            Log.CleanUp(_settings, Log.ApplicationType.SupervisorInterface);
        }

        /// <summary>
        /// 画面を開いた時の処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMain_Shown(object sender, EventArgs e)
        {
            LblStatus.Refresh();
            // タイマー開始前に実行する
            Execute();
            // 周期実行ON
            TickTimer.Interval = _settings.SupervisorInterface.TickTime * 1000;
            TickTimer.Enabled = true;
        }

        /// <summary>
        /// 周期処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TickTimer_Tick(object sender, EventArgs e)
        {
            // 周期実行
            Execute();
        }

        /// <summary>
        /// 周期処理の実行
        /// </summary>
        private void Execute()
        {
            PutLog(Sentence.Messages.StartSupervisorInterface);
            // ERP -> COROB
            PutLog(Sentence.Messages.ExecuteSupervisorInterface, "ERP to COROB");
            ToSupervisor();
            // COROB -> ERP
            PutLog(Sentence.Messages.ExecuteSupervisorInterface, "COROB to ERP");
            FromSupervisor();
            PutLog(Sentence.Messages.EndSupervisorInterface);
            LblStatus.Text = $"前回処理時刻：{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}";
        }

        #region 更新SQL作成
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
            items.Find(x => x.Key == TbFormula.PRD_CODE).Value = dispenseRow[Cans.COLUMN_CODE];
            items.Find(x => x.Key == TbFormula.PRD_DESC).Value = dispenseRow[Cans.COLUMN_CODE];
            items.Find(x => x.Key == TbFormula.PRD_UM).Value = 1;
            items.Find(x => x.Key == TbFormula.PRD_SPECIFIC_GRAVITY).Value = DBNull.Value;
            items.Find(x => x.Key == TbFormula.PRD_QTY_REQ).Value = dispenseRow[Cans.COLUMN_WEIGHT];
            items.Find(x => x.Key == TbFormula.PRD_QTY_DISP).Value = (float)0;
            items.Find(x => x.Key == TbFormula.PRD_START_DISP).Value = DBNull.Value;
            items.Find(x => x.Key == TbFormula.PRD_END_DISP).Value = DBNull.Value;
            items.Find(x => x.Key == TbFormula.PRD_PRIORITY).Value = DBNull.Value;
            items.Find(x => x.Key == TbFormula.PRD_NUM).Value = DBNull.Value;
            items.Find(x => x.Key == TbFormula.PRD_ISPREFILLED).Value = 1;
            items.Find(x => x.Key == TbFormula.PRD_PREFILLED_QTY).Value = dispenseRow[Cans.COLUMN_DISPENSED];
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
            parameters.Add(new ParameterItem($"{TbBarcode.BRC_STATUS}", TbBarcode.STATUS_ERP_PROCESSED));
            parameters.Add(new ParameterItem($"{TbBarcode.BRC_ERR_1}", dispenseRow[Cans.COLUMN_ERRORS_1]));
            parameters.Add(new ParameterItem($"{TbBarcode.BRC_ERR_2}", dispenseRow[Cans.COLUMN_ERRORS_2]));
            parameters.Add(new ParameterItem($"{TbBarcode.BRC_ERR_3}", dispenseRow[Cans.COLUMN_ERRORS_3]));
            // SQL返却
            return sql;
        }

        /// <summary>
        /// TB_BARCODE更新（エラー時）
        /// </summary>
        /// <param name="barcodeRow"></param>
        /// <param name="errorCode"></param>
        /// <param name="updateDateTime"></param>
        /// <param name="resultParameters"></param>
        /// <returns></returns>
        private string SetSqlBarcode(DataRow barcodeRow,
                                     string[] errorCode,
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
            parameters.Add(new ParameterItem($"{TbBarcode.BRC_STATUS}", TbBarcode.STATUS_ERP_PROCESSED));
            parameters.Add(new ParameterItem($"{TbBarcode.BRC_ERR_1}", errorCode[0]));
            parameters.Add(new ParameterItem($"{TbBarcode.BRC_ERR_2}", errorCode[1]));
            parameters.Add(new ParameterItem($"{TbBarcode.BRC_ERR_3}", errorCode[2]));
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
            items.Find(x => x.Key == TbJob.JOB_TARE_WEIGHT_EXPECTED).Value = GetTareWeightExpected(dispenseRow);
            items.Find(x => x.Key == TbJob.JOB_TARE_WEIGHT_DETECTED).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_TARE_WEIGHT_PERC_ERR_ADMITTED).Value = dispenseRow[Orders.COLUMN_P_WEIGHT_TOLERANCE];
            items.Find(x => x.Key == TbJob.JOB_GROSS_WEIGHT_EXPECTED).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_GROSS_WEIGHT_DETECTED).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_GROSS_WEIGHT_PERC_ERR_ADMITTED).Value = dispenseRow[Orders.COLUMN_P_WEIGHT_TOLERANCE];
            items.Find(x => x.Key == TbJob.JOB_NET_WEIGHT_EXPECTED).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_NET_WEIGHT_DETECTED).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_NET_WEIGHT_PERC_ERR_ADMITTED).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_TOT_COLORANT_WEIGHT_EXPECTED).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_TOT_COLORANT_WEIGHT_DETECTED).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_TOT_COLORANT_WEIGHT_PERC_ERR_ADMITTED).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_TOT_GRAVIMETRIC_WEIGHT_EXPECTED).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_TOT_GRAVIMETRIC_WEIGHT_DETECTED).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_TOT_GRAVIMETRIC_WEIGHT_PERC_ERR_ADMITTED).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_MIXING).Value = 1;
            items.Find(x => x.Key == TbJob.JOB_MIXING_TIME).Value = dispenseRow[Orders.COLUMN_MIXING_TIME];
            items.Find(x => x.Key == TbJob.JOB_MIXING_SPEED).Value = GetMixingSpeed(dispenseRow);
            items.Find(x => x.Key == TbJob.JOB_CAPPING).Value = dispenseRow[Orders.COLUMN_INPUT_CAN].ToString() == Orders.INPUT_CAN_YES.ToString() ? 1 : 0;
            items.Find(x => x.Key == TbJob.JOB_LID_PLACING).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_LID_CHECK).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_PRINTING_1).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_PRINTING_2).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_PRINTING_3).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_EXIT_POSITION).Value = dispenseRow[Cans.COLUMN_TEST_CAN].ToString() == Orders.TEST_CAN_YES.ToString() ? 2 : 1;
            items.Find(x => x.Key == TbJob.JOB_TAG_1).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_TAG_2).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_TAG_3).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_TAG_4).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_TAG_5).Value = 0;
            items.Find(x => x.Key == TbJob.JOB_ERR_1).Value = string.Empty;
            items.Find(x => x.Key == TbJob.JOB_ERR_2).Value = string.Empty;
            items.Find(x => x.Key == TbJob.JOB_ERR_3).Value = string.Empty;
            items.Find(x => x.Key == TbJob.JOB_ERR_4).Value = string.Empty;
            items.Find(x => x.Key == TbJob.JOB_ERR_5).Value = string.Empty;
            parameters = new List<ParameterItem>();
            parameters.AddRange(items);
            // SQL返却
            return sql;
        }

        #endregion

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

        #region ERPからSupervisorPC(COROB)への書き込み
        /// <summary>
        /// ERPからSupervisorPC(COROB)への書き込み
        /// </summary>
        private void ToSupervisor()
        {
            using (var dbs = new SqlBase(SqlBase.DatabaseKind.SUPERVISOR, SqlBase.TransactionUse.Yes, Log.ApplicationType.SupervisorInterface))
            {
                try
                {
                    // TB_BARCODEからバーコードデータを取得
                    var barcodeRows = dbs.Select(TbBarcode.GetPreview());
                    using (var dbn = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.No, Log.ApplicationType.SupervisorInterface))
                    {
                        foreach (DataRow barcodeRow in barcodeRows.Rows)
                        {
                            var barCode = barcodeRow[TbBarcode.BARCODE].ToString();
                            // ERPのテーブルから情報収集
                            // 行取得のSQLを作成
                            var parameters = new List<ParameterItem>()
                            {
                                new ParameterItem("barcode", barCode),
                            };
                            // バーコードをキーにERPのテーブルから情報収集
                            var dispenseDatas = dbn.Select(Cans.GetPreviewDispensedData(_settings.Facility.Plant), parameters);
                            var dispenseRows = dispenseDatas.Select($"{Cans.COLUMN_BARCODE} = '{barCode}' AND {Cans.COLUMN_CODE} <> '' AND {Cans.COLUMN_WEIGHT} <> {Cans.COLUMN_DISPENSED}");
                            //PutLog(Sentence.Messages.ExecuteSupervisorInterface, new string[] { barCode, dispenseRows.Count().ToString() });
                            var cnt = 0;
                            // SQLを作成してTB_BARCODE、TB_JOB、TB_FORMULAを更新
                            if (dispenseRows != null && dispenseRows.Any())
                            {
                                foreach (DataRow dispenseRow in dispenseRows)
                                {
                                    var updateDateTime = DateTime.Now;
                                    if (cnt == 0)
                                    {
                                        // TB_BARCODE
                                        dbs.Execute(SetSqlBarcode(barcodeRow, dispenseRow, updateDateTime, out List<ParameterItem> p0), p0);
                                        // TB_JOB
                                        dbs.Execute(SetSqlJob(barcodeRow, dispenseRow, updateDateTime, out List<ParameterItem> p1), p1);
                                    }
                                    // TB_FORMULA
                                    dbs.Execute(SetSqlFormura(barcodeRow, dispenseRow, updateDateTime, out List<ParameterItem> p2), p2);
                                    cnt++;
                                }
                            }
                            else
                            {
                                // TB_BARCODE（対象データがERPに存在しない）
                                var updateDateTime = DateTime.Now;
                                dbs.Execute(SetSqlBarcode(barcodeRow, new string[] { "1", "2", "3" }, updateDateTime, out List<ParameterItem> p0), p0);
                            }
                        }
                    }
                    dbs.Commit();
                }
                catch (Exception ex)
                {
                    dbs.Rollback();
                    PutLog(ex, false);
                }
            }
        }
        #endregion

        #region SupervisorPC(COROB)からERPへの書き込み
        /// <summary>
        /// SupervisorPC(COROB)からERPへの書き込み
        /// </summary>
        private void FromSupervisor()
        {
            using (var dbn = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.Yes, Log.ApplicationType.SupervisorInterface))
            {
                try
                {
                    using (var dbs = new SqlBase(SqlBase.DatabaseKind.SUPERVISOR, SqlBase.TransactionUse.No, Log.ApplicationType.SupervisorInterface))
                    {
                        // TB_BARCODEからバーコードデータを取得
                        var barcodeRows = dbs.Select(TbBarcode.GetPreview());
                        foreach (DataRow barcodeRow in barcodeRows.Rows)
                        {
                            var barCode = barcodeRow[TbBarcode.BARCODE].ToString();
                            if (!IsError(barcodeRow, out List<string> errors))
                            {
                                // ERPのテーブルから情報収集
                                // 行取得のSQLを作成
                                var parameters = new List<ParameterItem>()
                                {
                                    new ParameterItem("barcode", barCode),
                                };
                                // バーコードをキーにCOROBのテーブルから情報収集
                                var dispenseDatas = dbs.Select(TbFormula.GetPreviewAll(), parameters);
                                var cans = dbn.Select(Cans.GetDetailByBarcode(), parameters);
                                if (cans.Rows.Count > 0)
                                {
                                    var updateItems = new List<string>();
                                    foreach (var colorColumns in Cans.ColorColumns)
                                    {
                                        var item = dispenseDatas.AsEnumerable().FirstOrDefault(x => x[TbFormula.PRD_CODE].ToString() == cans.Rows[0][colorColumns[0]].ToString());
                                        if (item != null)
                                        {
                                            // TB_FORMULAテーブルの値を設定
                                            parameters.Add(new ParameterItem(colorColumns[1], item[TbFormula.PRD_PREFILLED_QTY]));
                                            updateItems.Add($"{colorColumns[1]} = @{colorColumns[1]}");
                                        }
                                    }
                                    // 更新対象カラムがある場合のみCansをupdate
                                    if (updateItems.Any())
                                    {
                                        dbn.Execute(Cans.SetDispensedFromSupervisor(updateItems), parameters);
                                    }
                                }
                            }
                            else
                            {
                                PutLog(Sentence.Messages.ErrorOnTbJob, new object[] { barCode, string.Join(",", errors) });
                            }
                        }
                    }
                    dbn.Commit();
                }
                catch (Exception ex)
                {
                    dbn.Rollback();
                    PutLog(ex, false);
                }
            }
        }
        #endregion

        #region COROBが更新したエラー内容のチェック
        /// <summary>
        /// エラーチェック対象カラム
        /// </summary>
        private readonly string[] ErrorCheckColumns = new string[]
        {
            TbJob.JOB_ERR_1,
            TbJob.JOB_ERR_2,
            TbJob.JOB_ERR_3,
            TbJob.JOB_ERR_4,
            TbJob.JOB_ERR_5,
        };
        /// <summary>
        /// COROBが更新したエラー内容のチェック
        /// </summary>
        /// <param name="barcodeRow"></param>
        /// <param name="messages"></param>
        /// <returns></returns>
        private bool IsError(DataRow barcodeRow, out List<string> messages)
        {
            messages = new List<string>();
            foreach (var errorCheckColumn in ErrorCheckColumns)
            {
                if (!string.IsNullOrEmpty(barcodeRow[errorCheckColumn].ToString()))
                {
                    messages.Add($"{errorCheckColumn}[{barcodeRow[errorCheckColumn]}]");
                }
            }
            if (messages.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 理論上の風袋重量(グラム)
        /// <summary>
        /// 理論上の風袋重量(グラム)
        /// </summary>
        /// <param name="dispenseRow"></param>
        /// <returns></returns>
        private float GetTareWeightExpected(DataRow dispenseRow)
        {
            return float.Parse(dispenseRow[Orders.COLUMN_CAN_WEIGHT].ToString()) + float.Parse(dispenseRow[Orders.COLUMN_HG_WEIGHT].ToString());
        }
        #endregion

        #region 撹拌スピード（％）
        /// <summary>
        /// 撹拌スピード（％）
        /// </summary>
        /// <param name="dispenseRow"></param>
        /// <returns></returns>
        private int GetMixingSpeed(DataRow dispenseRow)
        {
            var value = float.Parse(dispenseRow[Orders.COLUMN_MIXING_SPEED].ToString()) / float.Parse(_settings.SupervisorInterface.StandardMixingRpm.ToString());
            return int.Parse(value.ToString());
        }
        #endregion

    }
}
