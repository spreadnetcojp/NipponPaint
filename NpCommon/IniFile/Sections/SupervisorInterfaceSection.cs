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

namespace NipponPaint.NpCommon.IniFile.Sections
{
    public class SupervisorInterfaceSection
    {
        #region 定数
        private const string MySectionName = "SUPERVISOR_IF";
        #endregion

        #region プロパティ
        public int TickTime { get { return _tickTime; } }
        public int StandardMixingRpm { get { return _standardMixingRpm; } }
        #endregion

        #region メンバ変数
        private int _tickTime = 10;
        private int _standardMixingRpm = 150;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="filePath"></param>
        public SupervisorInterfaceSection(string filePath)
        {
            var reader = new FileInterface(filePath);
            _tickTime = int.Parse(reader.GetItem(MySectionName, "TickTime", _tickTime.ToString()));
            _standardMixingRpm = int.Parse(reader.GetItem(MySectionName, "StandardMixingRpm", _standardMixingRpm.ToString()));
        }
        #endregion
    }
}
