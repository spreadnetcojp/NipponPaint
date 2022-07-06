using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NipponPaint.NpCommon;
using NipponPaint.NpCommon.Database;

namespace NipponPaint.OrderManager.Dialogs
{
    public partial class FrmRemanufacturedCan : BaseForm
    {
        static DataTable dt;
        #region コンストラクタ
        public FrmRemanufacturedCan(ViewModels.RemanufacturedCanData vm)
        {
            InitializeComponent();
            InitializeForm();
            int cnt = 1;
            dt = vm.DataSource;
            foreach(var row in dt.Rows)
            {
                checkedListBox1.Items.Add(cnt);
                cnt++;
            }
            checkedListBox1.SetItemCheckState(vm.SelectedIndex, CheckState.Checked);
        }
        #endregion

        #region イベント
        /// <summary>
        /// ダイアログ表示後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmCanTypeShown(object sender, EventArgs e)
        {
            PutLog(Sentence.Messages.OpenMainForm);
        }

        /// <summary>
        /// 選択中のCheckBoxより前のCheckBoxを全て選択状態にする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelectBefore_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            foreach (var row in dt.Rows)
            {
                if(cnt <= checkedListBox1.SelectedIndex)
                {
                    checkedListBox1.SetItemCheckState(cnt, CheckState.Checked);
                }
                else
                {
                    checkedListBox1.SetItemCheckState(cnt, CheckState.Unchecked);
                }
                cnt++;
            }
        }

        /// <summary>
        /// 選択中のCheckBoxより後のCheckBoxを全て選択状態にする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelectBack_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            foreach (var row in dt.Rows)
            {
                if (cnt >= checkedListBox1.SelectedIndex)
                {
                    checkedListBox1.SetItemCheckState(cnt, CheckState.Checked);
                }
                else
                {
                    checkedListBox1.SetItemCheckState(cnt, CheckState.Unchecked);
                }
                cnt++;
            }
        }

        /// <summary>
        /// 全てのCheckBoxを選択状態にする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelectAll_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            foreach (var row in dt.Rows)
            {
                checkedListBox1.SetItemCheckState(cnt, CheckState.Checked);
                cnt++;
            }
        }

        /// <summary>
        /// 全てのCheckBoxを非選択状態にする
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSelectClear_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            foreach (var row in dt.Rows)
            {
                checkedListBox1.SetItemCheckState(cnt, CheckState.Unchecked);
                cnt++;
            }
        }
        /// <summary>
        /// 缶の再製造ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRemanufacturedCan_Click(object sender, EventArgs e)
        {

            var param = new List<SqlParameter>();
            var values = new List<string>();
            string column = "barcode";
            try
            {
                using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.Yes, Log.ApplicationType.OrderManager))
                {
                    var cnt = 1;
                    //チェックされたアイテムを抽出する
                    if (checkedListBox1.CheckedItems.Count > 0)
                    {
                        foreach (int item in checkedListBox1.CheckedItems)
                        {
                            var value = dt.Rows[item - 1]["バーコード"].ToString();
                            values.Add("@" + column + cnt.ToString());
                            param.Add(new SqlParameter(column + cnt.ToString(), value));
                            cnt++;
                        }
                        var sqlParameter = string.Join(",", values);
                        db.Execute(NpCommon.Database.Sql.NpMain.Cans.RemanufacturedCanByBarcode(sqlParameter), param);
                        db.Commit();
                    }
                    //更新が完了したあと、ダイアログを閉じる
                    PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
                    this.Close();
                }
            }
            catch(Exception ex)
            {
                PutLog(ex);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
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

        #region 画面の初期化
        private void InitializeForm()
        {
            // イベントの追加
            this.Shown += new System.EventHandler(this.FrmCanTypeShown);
            this.BtnSelectBefore.Click += new System.EventHandler(this.BtnSelectBefore_Click);
            this.BtnSelectBack.Click += new System.EventHandler(this.BtnSelectBack_Click);
            this.BtnSelectAll.Click += new System.EventHandler(this.BtnSelectAll_Click);
            this.BtnSelectClear.Click += new System.EventHandler(this.BtnSelectClear_Click);
            this.BtnRemanufacturedCan.Click += new System.EventHandler(this.BtnRemanufacturedCan_Click);
            this.BtnClose.Click += new System.EventHandler(this.BtnClose_Click);
        }
        #endregion[
    }
}
