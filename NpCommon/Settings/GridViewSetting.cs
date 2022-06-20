﻿//*****************************************************************************
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

#region using defines
using System.Windows.Forms;
using System.Collections.Generic;
#endregion

namespace NipponPaint.NpCommon.Settings
{
    public class GridViewSetting
    {
        private const string FALSE = "○";
        private const string TRUE = "●";

        public enum ColumnModeType
        {
            Numeric,
            String,
            DateTime,
            Checkbox,
            Blank,
            Bit,
        }
        /// <summary>
        /// データの種類
        /// </summary>
        public ColumnModeType ColumnType { get; set; } = ColumnModeType.String;
        /// <summary>
        /// データベース上の列名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 画面上の列名
        /// （ヘッダテキスト）
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 列の可視性
        /// </summary>
        public bool Visible { get; set; }
        /// <summary>
        /// 列幅
        /// </summary>
        public int Width { get; set; }
        /// <summary>
        /// 配置
        /// </summary>
        public DataGridViewContentAlignment alignment { get; set; } = DataGridViewContentAlignment.MiddleLeft;
        /// <summary>
        /// セルの値を反映するコントロール
        /// </summary>
        public Control DisplayControl { get; set; } = null;
        /// <summary>
        /// データベース上のテーブル名
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// テーブルへの更新対象項目か
        /// </summary>
        public bool IsUpdateColumn { get; set; } = false;

        #region コンストラクタ
        public GridViewSetting()
        {

        }

        public GridViewSetting(GridViewSetting item)
        {
            ColumnType = item.ColumnType;
            ColumnName = item.ColumnName;
            DisplayName = item.DisplayName;
            Visible = item.Visible;
            Width = item.Width;
            alignment = item.alignment;
        }
        #endregion

        /// <summary>
        /// SQL文の文言
        /// </summary>
        public string SqlSentence
        {
            get
            {
                switch (ColumnType)
                {
                    case ColumnModeType.String:
                        return $"TRIM(TRIM('　' FROM {ColumnName})) AS {DisplayName} ";
                    case ColumnModeType.Blank:
                        return $"'' AS {DisplayName} ";
                    case ColumnModeType.Bit:
                        return $"case {ColumnName} when 0 then N'{FALSE}' else N'{TRUE}' End AS {DisplayName}";
                    default:
                        return $"{ColumnName} AS {DisplayName} ";
                }
            }
        }
    }
}
