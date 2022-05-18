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
#endregion

namespace MlController.Inifile.Sections
{
    public class PropertiesSection
    {
        #region 定数
        private const string MySectionName = "PROPERTIES";
        #endregion

        #region プロパティ
        /// <summary>
        /// 
        /// </summary>
        public string Setting { get { return _setting; } }
        public int TimeOutSec { get { return _timeOutSec; } }
        public int ProtocolType { get { return _protocolType; } }
        #endregion

        #region メンバ変数
        private string _setting;
        private int _timeOutSec;
        private int _protocolType;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="filePath"></param>
        public PropertiesSection(string filePath)
        {
            var reader = new FileInterface(filePath);
            _setting = reader.GetItem(MySectionName, "Setting", "USB:");
            _timeOutSec = int.Parse(reader.GetItem(MySectionName, "TimeOutSec", "3"));
            _protocolType = int.Parse(reader.GetItem(MySectionName, "ProtocolType", "0"));
        }
        #endregion
    }
}
