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
    public class FacilitySection
    {
        #region 定数
        private const string MySectionName = "FACILITY";
        #endregion

        #region プロパティ
        /// <summary>
        /// 
        /// </summary>
        public string Plant { get { return _plant; } }
        #endregion

        #region メンバ変数
        private string _plant;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="filePath"></param>
        public FacilitySection(string filePath)
        {
            var reader = new FileInterface(filePath);
            _plant = reader.GetItem(MySectionName, "Plant", string.Format("RF1"));
        }
        #endregion
    }
}
