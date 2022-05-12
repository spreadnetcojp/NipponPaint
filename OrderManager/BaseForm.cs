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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NipponPaint.NpCommon;
using NipponPaint.NpCommon.IniFile;
using NipponPaint.NpCommon.Settings;
#endregion

namespace NipponPaint.OrderManager
{
    /// <summary>
    /// フォーム基底クラス
    /// </summary>
    public class BaseForm : Form
    {

        public Settings BaseSettings { get; set; }

        public BaseForm()
        {
            // アプリ定義ファイル読み込み
            BaseSettings = new Settings();
        }

        #region DataGridViewの初期設定
        /// <summary>
        /// DataGridViewの初期設定
        /// </summary>
        /// <param name="target"></param>
        public void InitializeGridView(DataGridView target, List<GridViewSetting> viewSettings = null)
        {
            if (viewSettings != null)
            {
                var reader = new FileInterface(BaseSettings.FilePath);
                var columnsWidth = reader.GetItem("GRID", target.Name);
                var i = 0;
                foreach (var columnWidth in columnsWidth)
                {
                    viewSettings[i].Width = columnWidth;
                    i++;
                }
            }
            target.DefaultCellStyle.BackColor = System.Drawing.Color.Black;
            target.DefaultCellStyle.ForeColor = System.Drawing.Color.White;
            target.Columns.Clear();
            target.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            target.RowHeadersVisible = false;
            target.MultiSelect = false;
            target.AllowUserToAddRows = false;
            target.AllowUserToDeleteRows = false;
            target.AllowUserToResizeRows = false;
            target.ReadOnly = true;
        }
        #endregion

        #region DataGridViewの設定を保存する
        public void SaveDataGridViewSetting(DataGridView target)
        {
            var columnsWidth = new List<int>();
            foreach (DataGridViewColumn column in target.Columns)
            {
                columnsWidth.Add(column.Width);
            }
            var reader = new FileInterface(BaseSettings.FilePath);
            reader.SetItem("GRID", target.Name, columnsWidth.ToArray());
        }
        #endregion

        #region ログ出力
        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="MessageId"></param>
        public void PutLog(Sentence.Messages MessageId, object[] addtionalInfo = null, bool displayDialog = false)
        {
            Log.Write(MessageId, Log.ApplicationType.OrderManager, addtionalInfo);
            if (displayDialog)
            {
                Messages.ShowDialog(MessageId, addtionalInfo);
            }
        }
        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="MessageId"></param>
        public void PutLog(Sentence.Messages MessageId, string addtionalInfo, bool displayDialog = false)
        {
            Log.Write(MessageId, Log.ApplicationType.OrderManager, addtionalInfo);
            if (displayDialog)
            {
                Messages.ShowDialog(MessageId, addtionalInfo);
            }
        }
        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="MessageId"></param>
        public void PutLog(Exception ex, bool displayDialog = false)
        {
            Log.Write(Sentence.Messages.Exception, Log.ApplicationType.OrderManager, ex.Message);
            if (displayDialog)
            {
                Messages.ShowDialog(Sentence.Messages.Exception, ex.Message);
            }
        }
        #endregion
    }
}
