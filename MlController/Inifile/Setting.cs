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

#region using define
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
#endregion

namespace MlController.Inifile
{
    public class Setting
    {
        #region プロパティ
        public Sections.PropertiesSection Properties { get { return _properties; } }
        #endregion

        #region メンバ変数
        private Sections.PropertiesSection _properties;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Setting(string filePath)
        {
            _properties = new Sections.PropertiesSection(filePath);
        }
        #endregion

    }
}
