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
        public const string MySectionName = "FACILITY";
        public const string TitleLastProductCode = "LastProductCode";
        private readonly List<string> productCodeUpper = new List<string>()
        {
            "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "W",
        };
        private readonly List<string> productCodeLower = new List<string>()
        {
            "2", "3", "4", "5", "6", "7", "8", "9",
            "A", "B", "C", "D", "E", "F", "G", "H", "J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z",
        };
        #endregion

        #region プロパティ
        /// <summary>
        /// プラント
        /// </summary>
        public string Plant { get { return _plant; } }
        /// <summary>
        /// 最終製品コード
        /// </summary>
        public string LastProductCode { get { return _lastProductCode; } }
        /// <summary>
        /// 次の製品コード
        /// </summary>
        public string NextProductCode
        {
            get
            {
                // 1桁ずつに分割
                var upper = _lastProductCode.Substring(0, 1);
                var lower = _lastProductCode.Substring(1, 1);
                var upperIndex = productCodeUpper.IndexOf(upper);
                var lowerIndex = productCodeLower.IndexOf(lower);
                // 下のインデクスをインクリメント
                lowerIndex++;
                if (lowerIndex > productCodeLower.Count - 1)
                {
                    lowerIndex = 0;
                    upperIndex++;
                    if (upperIndex > productCodeUpper.Count - 1)
                    {
                        upperIndex = 0;
                    }
                }
                return $"{productCodeUpper[upperIndex]}{productCodeLower[lowerIndex]}";
            }
        }
        #endregion

        #region メンバ変数
        private string _plant;
        private string _lastProductCode;
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
            _lastProductCode = reader.GetItem(MySectionName, TitleLastProductCode, "A2");
        }
        #endregion

    }
}
