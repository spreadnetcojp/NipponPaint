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
using NipponPaint.NpCommon;
using NipponPaint.NpCommon.Database;
#endregion

namespace NipponPaint.OrderManager.Dialogs
{
    /// <summary>
    /// 注文開始ダイアログ
    /// </summary>
    public partial class FrmLotRegister : BaseForm
    {
        #region コンストラクタ
        public FrmLotRegister(ViewModels.LotRegister vm)
        {
            InitializeComponent();
            InitializeForm();
            TxtLotRegister.Value = vm.Lot;
            TxtLotRegister.Id = vm.DataNumber.ToString();
        }
        #endregion

        #region イベント
        /// <summary>
        /// OKボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOKClick(object sender, EventArgs e)
        {
            try
            {
                RegistData();
                MessageBox.Show("設定内容を保存しました。");
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
                this.Close();
            }
            catch(Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// キャンセルボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void BtnCancelClick(object sender, EventArgs e)
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
            this.BtnOK.Click += new EventHandler(this.BtnOKClick);
            this.BtnCancel.Click += new EventHandler(this.BtnCancelClick);
            Funcs.SetControlEnabled(this.Controls, false);
        }

        #region 画面上で入力された指定LOTをDBへ反映する
        /// <summary>
        /// 画面上で変更された指定LOTをDBへ反映する
        /// </summary>
        private void RegistData()
        {
            using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.Yes, Log.ApplicationType.OrderManager))
            {
                try
                {
                    // フォームで定義された、指定LOT設定先のコントロールを抽出する
                    db.FromLabelTextBox(this.Controls, "Orders", "HG_Data_Number");

                    db.Commit();
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    // ログ出力とエラーメッセージ
                    BaseForm baseForm = new BaseForm();
                    baseForm.PutLog(ex);
                }
            }
        }

        #endregion

        #endregion
    }
}

