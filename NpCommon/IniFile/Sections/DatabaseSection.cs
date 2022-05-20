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
    public class DatabaseSection
    {
        #region 定数
        private const string DEFAULT_STRING = @"Provider=SQLOLEDB;Password=cps_pwd2000;Persist Security Info=True;User ID=sa;Initial Catalog={0};Data Source=CPSCOLOR\SQLEXPRESS;Trusted_Connection=true";
        #endregion

        #region プロパティ
        /// <summary>
        /// 
        /// </summary>
        public string NpOrder { get { return _npOrder; } }
        /// <summary>
        /// 
        /// </summary>
        public string NpMain { get { return _npMain; } }
        /// <summary>
        /// 
        /// </summary>
        public string IosSup { get { return _iosSup; } }
        /// <summary>
        /// 
        /// </summary>
        public string SuperVision { get { return _superVision; } }
        #endregion

        #region メンバ変数
        private string _npOrder;
        private string _npMain;
        private string _iosSup;
        private string _superVision;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="filePath"></param>
        public DatabaseSection(string filePath)
        {
            var reader = new FileInterface(filePath);
            _npOrder = reader.GetItem("DATABASE", "NP_ORDERConnStr", string.Format(DEFAULT_STRING, "ORDER_RF1"));
            _npMain = reader.GetItem("DATABASE", "NP_MAINConnStr", string.Format(DEFAULT_STRING, "NP_MAIN"));
            _iosSup = reader.GetItem("DATABASE", "IOSSUPConnStr", string.Format(DEFAULT_STRING, "IOSSUP_RF1"));
            _superVision = reader.GetItem("DATABASE", "SPCConnStr", string.Format(DEFAULT_STRING, "SUPERVISION_PC"));
        }
        #endregion

    }
}
