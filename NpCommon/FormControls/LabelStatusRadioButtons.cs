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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NipponPaint.NpCommon.FormControls
{
    public partial class LabelStatusRadioButtons : UserControl
    {
        /// <summary>
        /// テスト缶ステータス
        /// </summary>
        public RadioButton Rbt1CheckState
        {
            get { return Rbt1; }
        }
        /// <summary>
        /// 信頼できる配合ステータス
        /// </summary>
        public RadioButton Rbt2CheckState
        {
            get { return Rbt2; }
        }
        public LabelStatusRadioButtons()
        {
            InitializeComponent();
        }
    }
}
