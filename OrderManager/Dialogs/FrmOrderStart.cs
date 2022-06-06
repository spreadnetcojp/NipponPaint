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
            NumUpDownWeightTolerance.LeftSide.Value = (decimal)vm.WeightToleranceMin;
            NumUpDownWeightTolerance.RightSide.Value = (decimal)vm.WeightToleranceMax;
            NumUpDownQualitySample.Value = vm.QualitySample;
            NumUpDownMixingTime.Value = vm.MixingTime;
            NumUpDownMixingSpeed.Value = vm.MixingSpeed;
            DropDownLabelType.InitializeDropdownItems(SqlBase.DatabaseKind.ORDER, Log.ApplicationType.OrderManager);
            DropDownCanType.InitializeDropdownItems(SqlBase.DatabaseKind.ORDER, Log.ApplicationType.OrderManager);
            DropDownCapType.InitializeDropdownItems(SqlBase.DatabaseKind.ORDER, Log.ApplicationType.OrderManager);
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
        //　▼ DBカラムnumber
        /// <summary>
        /// ラベルタイプ
        /// </summary>
        private const int COLUMN_LABEL_TYPE = 0;
        /// <summary>
        /// ラベル詳細
        /// </summary>
        private const int COLUMN_LABLE_DESCRIPTION = 1;
        /// <summary>
        /// 缶タイプ
        /// </summary>
        private const int COLUMN_CAN_TYPE = 0;
        /// <summary>
        /// 缶詳細
        /// </summary>
        private const int COLUMN_CAN_DESCRIPTION = 1;
        /// <summary>
        /// 缶サイズ[mm]
        /// </summary>
        private const int COLUMN_CAN_HOLE_SIZE = 2;
        /// <summary>
        /// 空缶重量[g]
        /// </summary>
        private const int COLUMN_CAN_AVAILABLE_VOLUME = 3;
        /// <summary>
        /// 通常容量[ml]
        /// </summary>
        private const int COLUMN_CAN_WEIGHT = 4;
        /// <summary>
        /// 最大容量[ml]
        /// </summary>
        private const int COLUMN_NOMINAL_VOLUME = 5;
        /// <summary>
        /// キャップタイプ
        /// </summary>
        private const int COLUMN_CAP_TYPE = 0;
        /// <summary>
        /// キャップ詳細
        /// </summary>
        private const int COLUMN_CAP_DESCRIPTION = 1;
        /// <summary>
        /// キャップサイズ[mm]
        /// </summary>
        private const int COLUMN_CAP_HOLE_SIZE = 2;
        /// <summary>
        /// キャップ重量[g]
        /// </summary>
        private const int COLUMN_CAP_WEIGHT = 3;
        /// <summary>
        /// キャッパー
        /// </summary>
        private const int COLUMN_CAP_CAPPING_MACHINE = 4;
        //　▲ DBカラムnumber
        private DataTable labelData = new DataTable();
        private DataTable canData = new DataTable();
        private DataTable capData = new DataTable();
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
                // ラジオボタン選択・テスト缶or信頼できる配合でStatus変化
                int status = 0;
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
                using (var db = new SqlBase(SqlBase.DatabaseKind.ORDER, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
                {
                    // ラベルタイプデータ取得
                    labelData = DropDownLabelType.DropdownItemValue(db, labelSelectValue);
                    // 缶タイプデータ取得
                    canData = DropDownCanType.DropdownItemValue(db, canSelectValue);
                    // キャップタイプデータ取得
                    capData = DropDownCapType.DropdownItemValue(db, capSelectValue);
                }
                if (status == STATUS_TEST || status == STATUS_PRODUCTION)
                {
                    // 注文開始更新用パラメータ作成
                    var parameters = new List<ParameterItem>()
                    {
                        new ParameterItem("@Status", status),
                        new ParameterItem("@LabelType", labelSelectValue),
                        new ParameterItem("@LabelDescription", labelData.Rows[0].ItemArray[COLUMN_LABLE_DESCRIPTION]),
                        new ParameterItem("@CanType", canSelectValue),
                        new ParameterItem("@CanDescription", canData.Rows[0].ItemArray[COLUMN_CAN_DESCRIPTION]),
                        new ParameterItem("@CanWeight", canData.Rows[0].ItemArray[COLUMN_CAN_WEIGHT]),
                        new ParameterItem("@CanNominal", canData.Rows[0].ItemArray[COLUMN_NOMINAL_VOLUME]),
                        new ParameterItem("@CanAvailable", canData.Rows[0].ItemArray[COLUMN_CAN_AVAILABLE_VOLUME]),
                        new ParameterItem("@CapType", capSelectValue),
                        new ParameterItem("@CapDescription", capData.Rows[0].ItemArray[COLUMN_CAP_DESCRIPTION]),
                        new ParameterItem("@CapWeight", capData.Rows[0].ItemArray[COLUMN_CAP_WEIGHT]),
                        new ParameterItem("@CapHoleSize", capData.Rows[0].ItemArray[COLUMN_CAP_HOLE_SIZE]),
                        new ParameterItem("@CappingMachine", capData.Rows[0].ItemArray[COLUMN_CAP_CAPPING_MACHINE]),
                        new ParameterItem("@Overfilling", NumUpDownOverfilling.Value),
                        new ParameterItem("@PrefillAmount", NumUpDownFilledWeight.Value),
                        new ParameterItem("@PWeightTolerance", NumUpDownWeightTolerance.ValueLeft),
                        new ParameterItem("@NWeightTolerance", NumUpDownWeightTolerance.ValueRight),
                        new ParameterItem("@QualitySample", NumUpDownQualitySample.Value),
                        new ParameterItem("@MixingTime", NumUpDownMixingTime.Value),
                        new ParameterItem("@MixingSpeed", NumUpDownMixingSpeed.Value),
                        new ParameterItem("@OrderNumber", TxtOrderNumber.Value),
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

        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitializeForm()
        {
            // イベントの追加
            this.BtnOrderStart.Click += new EventHandler(this.BtnOrderStartClick);
            this.BtnOrderBack.Click += new EventHandler(this.BtnOrderBackClick);
            this.BtnClose.Click += new EventHandler(this.BtnCloseClick);
        }
        #endregion

        private void FrmOrderStart_Load(object sender, EventArgs e)
        {

        }
    }
}
