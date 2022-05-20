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

namespace NipponPaint.NpCommon.IniFile
{
    public class Settings
    {
        #region 定数
        #endregion

        #region プロパティ
        public Sections.DatabaseSection Database { get { return _database; } }
        public Sections.DisplaySection Display { get { return _display; } }
        public Sections.FacilitySection Facility { get { return _facility; } }
        public string FilePath { get { return _filePath; } }
        #endregion

        #region メンバ変数
        private string _filePath;
        private Sections.DatabaseSection _database;
        private Sections.DisplaySection _display;
        private Sections.FacilitySection _facility;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public Settings()
        {
            var folder = Path.GetDirectoryName(Application.ExecutablePath);
            _filePath = Path.Combine(folder, Constants.IniFiles.NP_COMMON);
            _database = new Sections.DatabaseSection(_filePath);
            _display = new Sections.DisplaySection(_filePath);
            _facility = new Sections.FacilitySection(_filePath);
        }
        #endregion
    }
}
