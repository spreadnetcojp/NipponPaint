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
    public class DisplaySection
    {
        #region 定数
        #endregion

        #region プロパティ
        public List<string> HgNoteStrList { get { return _hgNoteStrList; } }
        #endregion

        #region メンバ変数
        private List<string> _hgNoteStrList = new List<string>();

        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="filePath"></param>
        public DisplaySection(string filePath)
        {
            var reader = new FileInterface(filePath);
            var items = reader.GetItem("DISPLAY", "ToningAppKeyword", "");
            if (!string.IsNullOrEmpty(items))
            {
                foreach (var item in items.Split(','))
                {
                    _hgNoteStrList.Add(item);
                }
            }
        }
        #endregion
    }
}
