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
using System.Data;
using NipponPaint.NpCommon;
using NipponPaint.NpCommon.Database;

#endregion

namespace NipponPaint.OrderManager.Dialogs
{
    /// <summary>
    /// 注文開始ダイアログ
    /// </summary>
    public partial class FrmOrderStart : BaseForm
    {
        #region コンストラクタ
        public FrmOrderStart(ViewModels.OrderStartData vm)
        {
            InitializeComponent();
            InitializeForm();
            TxtOrderId.Value = vm.OrderID.ToString();
            TxtOrderNumber.Value = vm.OrderNumber;
            TxtColorName.Value = vm.ColorName;
            TxtProductCode.Value = vm.ProductCode;
            TxtNumberOfCan.Value = vm.NumberOfCan.ToString();
            TxtPaintName.Value = vm.ItemName;
            TxtFormalPaintName.Value = vm.FormalItemName;
            TxtWhiteCode.Value = vm.WhiteCode;
            TxtRevision.Value = vm.Revision.ToString();
            TxtTotalWeight.Value = vm.TotalWeight.ToString();
            NumUpDownOverfilling.Value = (decimal)vm.Overfilling;
            NumUpDownFilledWeight.Value = vm.FilledWeight;
            NumUpDownWeightTolerance.ValueLeft = (decimal)vm.WeightToleranceMin;
            NumUpDownWeightTolerance.ValueRight = (decimal)vm.WeightToleranceMax;
            NumUpDownQualitySample.Value = vm.QualitySample;
            NumUpDownMixingTime.Value = vm.MixingTime;
            NumUpDownMixingSpeed.Value = vm.MixingSpeed;
            DropDownLabelType.InitializeDropdownItems(SqlBase.DatabaseKind.ORDER, Log.ApplicationType.OrderManager);
            DropDownCanType.InitializeDropdownItems(SqlBase.DatabaseKind.ORDER, Log.ApplicationType.OrderManager);
            DropDownCanType.SelectedValue = vm.CanType;
            DropDownCapType.InitializeDropdownItems(SqlBase.DatabaseKind.ORDER, Log.ApplicationType.OrderManager);
            DropDownCapType.SelectedValue = vm.CapType;
            labelStatusRadioButtons1.Rbt1CheckState.Checked = true;
            NumUpDownWeightTolerance.LeftSide.Increment = UPDOWN_NUM_DECIMAL;
            NumUpDownWeightTolerance.RightSide.Increment = UPDOWN_NUM_DECIMAL;
            NumUpDownOverfilling.ValueNumericUpDown.Increment = UPDOWN_NUM_DECIMAL;
        }
        #endregion

        #region 定数
        /// <summary>
        /// テスト缶
        /// </summary>
        private const int STATUS_TEST = 3;
        /// <summary>
        /// 信頼できる配合
        /// </summary>
        private const int STATUS_PRODUCTION = 4;
        /// <summary>
        /// 手動キャッピング
        /// </summary>
        private const int CAPPING_MACHINE_MANUAL = 128;
        /// <summary>
        /// 手動キャッピング以外
        /// </summary>
        private const int CAPPING_MACHINE = 1;
        /// <summary>
        /// ラベルデータ
        /// </summary>
        private DataTable labelData = new DataTable();
        /// <summary>
        /// 缶データ
        /// </summary>
        private DataTable canData = new DataTable();
        /// <summary>
        /// キャップデータ
        /// </summary>
        private DataTable capData = new DataTable();
        /// <summary>
        /// 重量リスト（１～１９)
        /// </summary>
        private Dictionary<string, double> weightList = new Dictionary<string, double>();

        private const decimal UPDOWN_NUM_DECIMAL = 0.1M;
        #endregion

        #region イベント
        /// <summary>
        /// 注文開始ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOrderStartClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
                int status = 0;
                int cappingStatus = 0;
                // ラジオボタン選択・テスト缶or信頼できる配合でStatus変化
                if (labelStatusRadioButtons1.Rbt1CheckState.Checked == true)
                {
                    status = STATUS_TEST;
                }
                else if (labelStatusRadioButtons1.Rbt2CheckState.Checked == true)
                {
                    status = STATUS_PRODUCTION;
                }
                // ラベルタイプ取得
                int.TryParse(DropDownLabelType.SelectedValue.ToString(), out int labelSelectValue);
                // 缶タイプ取得
                int.TryParse(DropDownCanType.SelectedValue.ToString(), out int canSelectValue);
                // キャップタイプ取得
                int.TryParse(DropDownCapType.SelectedValue.ToString(), out int capSelectValue);
                // 超過の値取得
                decimal.TryParse(NumUpDownOverfilling.Value.ToString(), out decimal overFilling);
                using (var db = new SqlBase(SqlBase.DatabaseKind.ORDER, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
                {
                    // ラベルタイプデータ取得
                    labelData = DropDownLabelType.DropdownItemValue(db, labelSelectValue);
                    // 缶タイプデータ取得
                    canData = DropDownCanType.DropdownItemValue(db, canSelectValue);
                    // キャップタイプデータ取得
                    capData = DropDownCapType.DropdownItemValue(db, capSelectValue);
                    // 重量超過分のパラメータ作成
                    int.TryParse(TxtOrderId.Value.ToString(), out int orderId);
                    weightList = GetItemWeight(db, orderId, overFilling);
                }
                if(overFilling > 0)
                {
                    TxtTotalWeight.Value = GetTotalWeight(weightList).ToString();
                }
                // 手動キャッピングのステータス判断
                if (ChkHandCapping.CheckState.Checked)
                {
                    cappingStatus = CAPPING_MACHINE_MANUAL;
                }
                else
                {
                    cappingStatus = CAPPING_MACHINE;
                }
                if (status == STATUS_TEST || status == STATUS_PRODUCTION)
                {
                    // 注文開始更新用パラメータ作成
                    var parameters = new List<ParameterItem>()
                    {
                        new ParameterItem("@Status", status),
                        new ParameterItem("@LabelType", labelSelectValue),
                        new ParameterItem("@LabelDescription", labelData.Rows[0][NpCommon.Database.Sql.Order.Labels.COLUMN_LABLE_DESCRIPTION]),
                        new ParameterItem("@CanType", canSelectValue),
                        new ParameterItem("@CanDescription", canData.Rows[0][NpCommon.Database.Sql.Order.CanTypes.COLUMN_CAN_DESCRIPTION]),
                        new ParameterItem("@CanWeight", canData.Rows[0][NpCommon.Database.Sql.Order.CanTypes.COLUMN_CAN_WEIGHT]),
                        new ParameterItem("@CanNominal", canData.Rows[0][NpCommon.Database.Sql.Order.CanTypes.COLUMN_NOMINAL_VOLUME]),
                        new ParameterItem("@CanAvailable", canData.Rows[0][NpCommon.Database.Sql.Order.CanTypes.COLUMN_AVAILABLE_VOLUME]),
                        new ParameterItem("@CapType", capSelectValue),
                        new ParameterItem("@CapDescription", capData.Rows[0][NpCommon.Database.Sql.Order.CapTypes.COLUMN_CAP_DESCRIPTION]),
                        new ParameterItem("@CapWeight", capData.Rows[0][NpCommon.Database.Sql.Order.CapTypes.COLUMN_CAP_WEIGHT]),
                        new ParameterItem("@CapHoleSize", capData.Rows[0][NpCommon.Database.Sql.Order.CapTypes.COLUMN_HOLE_SIZE]),
                        new ParameterItem("@CappingMachine", cappingStatus),
                        new ParameterItem("@Overfilling", NumUpDownOverfilling.Value),
                        new ParameterItem("@PrefillAmount", NumUpDownFilledWeight.Value),
                        new ParameterItem("@PWeightTolerance", NumUpDownWeightTolerance.ValueLeft),
                        new ParameterItem("@NWeightTolerance", NumUpDownWeightTolerance.ValueRight),
                        new ParameterItem("@QualitySample", NumUpDownQualitySample.Value),
                        new ParameterItem("@MixingTime", NumUpDownMixingTime.Value),
                        new ParameterItem("@MixingSpeed", NumUpDownMixingSpeed.Value),
                        new ParameterItem("@OrderNumber", TxtOrderNumber.Value),
                        new ParameterItem("@Weight_1", weightList[NpCommon.Database.Sql.NpMain.Orders.COLUMN_WEIGHT_1]),
                        new ParameterItem("@Weight_2", weightList[NpCommon.Database.Sql.NpMain.Orders.COLUMN_WEIGHT_2]),
                        new ParameterItem("@Weight_3", weightList[NpCommon.Database.Sql.NpMain.Orders.COLUMN_WEIGHT_3]),
                        new ParameterItem("@Weight_4", weightList[NpCommon.Database.Sql.NpMain.Orders.COLUMN_WEIGHT_4]),
                        new ParameterItem("@Weight_5", weightList[NpCommon.Database.Sql.NpMain.Orders.COLUMN_WEIGHT_5]),
                        new ParameterItem("@Weight_6", weightList[NpCommon.Database.Sql.NpMain.Orders.COLUMN_WEIGHT_6]),
                        new ParameterItem("@Weight_7", weightList[NpCommon.Database.Sql.NpMain.Orders.COLUMN_WEIGHT_7]),
                        new ParameterItem("@Weight_8", weightList[NpCommon.Database.Sql.NpMain.Orders.COLUMN_WEIGHT_8]),
                        new ParameterItem("@Weight_9", weightList[NpCommon.Database.Sql.NpMain.Orders.COLUMN_WEIGHT_9]),
                        new ParameterItem("@Weight_10", weightList[NpCommon.Database.Sql.NpMain.Orders.COLUMN_WEIGHT_10]),
                        new ParameterItem("@Weight_11", weightList[NpCommon.Database.Sql.NpMain.Orders.COLUMN_WEIGHT_11]),
                        new ParameterItem("@Weight_12", weightList[NpCommon.Database.Sql.NpMain.Orders.COLUMN_WEIGHT_12]),
                        new ParameterItem("@Weight_13", weightList[NpCommon.Database.Sql.NpMain.Orders.COLUMN_WEIGHT_13]),
                        new ParameterItem("@Weight_14", weightList[NpCommon.Database.Sql.NpMain.Orders.COLUMN_WEIGHT_14]),
                        new ParameterItem("@Weight_15", weightList[NpCommon.Database.Sql.NpMain.Orders.COLUMN_WEIGHT_15]),
                        new ParameterItem("@Weight_16", weightList[NpCommon.Database.Sql.NpMain.Orders.COLUMN_WEIGHT_16]),
                        new ParameterItem("@Weight_17", weightList[NpCommon.Database.Sql.NpMain.Orders.COLUMN_WEIGHT_17]),
                        new ParameterItem("@Weight_18", weightList[NpCommon.Database.Sql.NpMain.Orders.COLUMN_WEIGHT_18]),
                        new ParameterItem("@Weight_19", weightList[NpCommon.Database.Sql.NpMain.Orders.COLUMN_WEIGHT_19]),
                        new ParameterItem("@TotalWeight", TxtTotalWeight.Value),
                    };
                    using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.Yes, Log.ApplicationType.OrderManager))
                    {
                        db.Execute(NpCommon.Database.Sql.NpMain.Orders.StartOrder(), parameters);
                        db.Commit();
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 注文を戻すボタン(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOrderBackClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
                using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.Yes, Log.ApplicationType.OrderManager))
                {
                    // 確認ダイアログ表示
                    DialogResult result = Messages.ShowDialog(Sentence.Messages.BtnOrderBackClick);
                    switch (result)
                    {
                        case DialogResult.Yes:
                            // Order_id取得・Order_idをキーに注文を戻す実施
                            int.TryParse(TxtOrderId.Value.ToString(), out int orderId);
                            var parameters = new List<ParameterItem>()
                            {
                                new ParameterItem("orderId", orderId),
                            };
                            db.StatusResume(NpCommon.Database.Sql.NpMain.Orders.StatusResume(), parameters);
                            db.Commit();
                            this.Close();
                            break;
                        case DialogResult.No:
                            break;
                        default:
                            break;
                    }
                }
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

        #region private functions

        #region 画面の初期化
        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitializeForm()
        {
            // イベントの追加
            this.BtnOrderStart.Click += new EventHandler(this.BtnOrderStartClick);
            this.BtnOrderBack.Click += new EventHandler(this.BtnOrderBackClick);
            this.BtnClose.Click += new EventHandler(this.BtnCloseClick);
            this.DropDownCapType.SelectedIndexChanged += new EventHandler(this.DropDownCapTypeSelectedIndexChanged);
            // データ表示部の設定
            Funcs.SetControlEnabled(this.Controls, true);
        }
        #endregion

        #region　キャップタイプに連動した、手動キャッピングのチェック処理
        /// <summary>
        /// キャップタイプに連動した、手動キャッピングのチェック処理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DropDownCapTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownCapType.SelectedValue == null)
            {
                return;
            }
            int.TryParse(DropDownCapType.SelectedValue.ToString(), out int capType);
            using (var db = new SqlBase(SqlBase.DatabaseKind.ORDER, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
            {
                capData = DropDownCapType.DropdownItemValue(db, capType);
            }
            if (capData.Rows.Count > 0)
            {
                var cappingMachine = Funcs.StrToInt(capData.Rows[0][NpCommon.Database.Sql.Order.CapTypes.COLUMN_CAPPING_MACHINE].ToString());
                switch (cappingMachine)
                {
                    case CAPPING_MACHINE_MANUAL:
                        ChkHandCapping.CheckState.Checked = true;
                        break;
                    default:
                        ChkHandCapping.CheckState.Checked = false;
                        break;
                }
                // キャップタイプの初期設定で手動キャッピングがONになっているものは編集不可
                ChkHandCapping.Enabled = !ChkHandCapping.CheckState.Checked;
            }
        }
        #endregion

        #region　超過の値に伴う重量１～１９の再計算
        /// <summary>
        /// 超過の値に伴う重量１～１９の再計算
        /// </summary>
        /// <param name="db"></param>
        /// <param name="orderId"></param>
        /// <param name="overFilling"></param>
        /// <returns></returns>
        private Dictionary<string, double> GetItemWeight(SqlBase db, int orderId, decimal overFilling)
        {
            var parameters = new List<ParameterItem>()
            {
                new ParameterItem("@OrderId", orderId),
            };
            // 重量１～１９を取得
            var weightValues = db.Select(NpCommon.Database.Sql.NpMain.Orders.GetItemsWeightValue(), parameters);
            // weightValuesのValueをリスト化
            var weightValueList = ConvertDataTableTolist(weightValues);
            var weightKeyValue = new Dictionary<string, double>();
            var weightNumber = 1;
            // overFillingが0なら計算しない
            if (overFilling > 0)
            {
                // 超過の値を計算できる値に変更
                double overFillingValue = 1 + (Convert.ToDouble(overFilling) / 100);
                foreach(var weightValue in weightValueList)
                {
                    // keyをカラム名に設定、Valueをoverfillingの値によって計算、小数点第４位四捨五入
                    weightKeyValue.Add($"Weight_{weightNumber}", Math.Round((weightValue * overFillingValue), 3, MidpointRounding.AwayFromZero));
                    weightNumber++;
                }
                return weightKeyValue;
            }
            else
            {
                foreach (var weightValue in weightValueList)
                {
                    weightKeyValue.Add($"Weight_{weightNumber}", weightValue);
                    weightNumber++;
                }
                return weightKeyValue;
            }
        }
        #endregion

        #region　DataTableのValueのみをリスト化
        /// <summary>
        /// DataTableのValueのみをリスト化
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static List<double> ConvertDataTableTolist(DataTable dt)
        {
            List<double> result = new List<double>();
            foreach(double item in dt.Rows[0].ItemArray)
            {
                result.Add(item);
            }
            return result;
        }
        #endregion

        #region 超過があった場合に重量１～１９を計算して総重量を算出
        /// <summary>
        /// 超過があった場合に重量１～１９を計算して総重量を算出
        /// </summary>
        /// <param name="weightList"></param>
        /// <returns></returns>
        private double GetTotalWeight(Dictionary<string, double> weightList)
        {
            double totalWeight = 0;
            foreach (var weight in weightList)
            {
                totalWeight += weight.Value;
            }
            return totalWeight;
        }
        #endregion

        #endregion
    }
}
