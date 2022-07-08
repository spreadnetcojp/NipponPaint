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
        private const string MySectionName = "DISPLAY";
        public const int AutoBindCancel = 1;
        #endregion
        
        #region プロパティ
        /// <summary>
        /// 表示更新周期（秒）
        /// </summary>
        public int PreviewCycleSeconds { get { return _previewCycleSeconds; } }
        /// <summary>
        /// 表示更新周期（秒）
        /// </summary>
        public int PreviewCycleMillisecond { get { return _previewCycleMillisecond; } }
        /// <summary>
        /// 
        /// </summary>
        public List<string> HgNoteStrList { get { return _hgNoteStrList; } }
        /// <summary>
        /// 
        /// </summary>
        public List<string> ColorNameStrList { get { return _colorNameStrList; } }
        #endregion

        #region メンバ変数
        private int _previewCycleSeconds = 10;
        private int _previewCycleMillisecond = 10000;
        private List<string> _hgNoteStrList = new List<string>();
        private List<string> _colorNameStrList = new List<string>();
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="filePath"></param>
        public DisplaySection(string filePath)
        {
            var reader = new FileInterface(filePath);
            var value = reader.GetItem(MySectionName, "PreviewCycle", "10");
            if (!string.IsNullOrEmpty(value))
            {
                int.TryParse(value, out _previewCycleSeconds);
                _previewCycleMillisecond = _previewCycleSeconds * 1000;
            }
            var toningItems = reader.GetItem(MySectionName, "ToningAppKeyword", "");
            if (!string.IsNullOrEmpty(toningItems))
            {
                foreach (var item in toningItems.Split(','))
                {
                    _hgNoteStrList.Add(item);
                }
            }
            var ColorNameItems = reader.GetItem(MySectionName, "ColorNameKeyword", "");
            if (!string.IsNullOrEmpty(ColorNameItems))
            {
                foreach (var item in ColorNameItems.Split(','))
                {
                    _colorNameStrList.Add(item);
                }
            }
        }
        #endregion
    }
}
