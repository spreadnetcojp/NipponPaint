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

#region using defines
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Windows.Forms;
using NipponPaint.NpCommon.FormControls;
using System.Drawing;
#endregion

namespace NipponPaint.NpCommon
{
    public class Funcs
    {
        private static readonly Color BACK_COLOR = Color.Black;
        private static readonly Color FORE_COLOR = Color.White;

        // フォントサイズ
        private static int FONTSIZE_PRODUCT_CODE = 24;
        private static int FONTSIZE_DEFAULT = 12;
        private static int FONTSIZE_PRODUCT_CODE_GVORDER =16;
        private static int FONTSIZE_DEFAULT_GVORDER = 8;

        #region 取得値設定先のコントロールを抽出する
        /// <summary>
        /// 取得値設定先のコントロールを抽出する
        /// </summary>
        /// <param name="controls"></param>
        /// <param name="result"></param>
        public static void FindControls(Control.ControlCollection controls, List<Control> result)
        {
            foreach (Control control in controls)
            {
                switch (control)
                {
                    case LabelTextBox labelTextBox:
                        result.Add(labelTextBox);
                        break;
                    case LabelTextBoxDb labelTextBoxDb:
                        result.Add(labelTextBoxDb);
                        break;
                    case LabelTextSeparate labelTextSeparate:
                        result.Add(labelTextSeparate);
                        break;
                    case LabelNumericUpDown labelNumericUpDown:
                        result.Add(labelNumericUpDown);
                        break;
                    case LabelCheckBoxSingle labelCheckBoxSingle:
                        result.Add(labelCheckBoxSingle);
                        break;
                    case LabelNumericUpDownMulti labelNumericUpDownMulti:
                        result.Add(labelNumericUpDownMulti);
                        break;
                    case TextBox textBox:
                        result.Add(textBox);
                        break;
                    case TabPage tabPage:
                        result.Add(tabPage);
                        if (control.Controls.Count > 0)
                        {
                            FindControls(control.Controls, result);
                        }
                        break;
                    case LabelColorDiaLog labelColorDiaLog:
                        result.Add(labelColorDiaLog);
                        break;
                    case LabelCodeText labelCodeText:
                        result.Add(labelCodeText);
                        break;
                    case DeliveryClassification deliveryClassfication:
                        result.Add(deliveryClassfication);
                        break;
                    case TabCtrlPagePlants tabCtrlPagePlants:
                        result.Add(tabCtrlPagePlants);
                        break;
                    case TabCtrlPagePlantsSetting tabCtrlPagePlantsSetting:
                        result.Add(tabCtrlPagePlantsSetting);
                        break;
                    case LabelDateTimePicker labelDateTimePicker:
                        result.Add(labelDateTimePicker);
                        break;
                    case NumericUpDown numericUpDown:
                        result.Add(numericUpDown);
                        break;
                    case DateTimePicker dateTimePicker:
                        result.Add(dateTimePicker);
                        break;
                    case LabelDropDown labelDropDown:
                        result.Add(labelDropDown);
                        break;
                    case LabelTextButton labelTextButton:
                        result.Add(labelTextButton);
                        break;
                    default:
                        if (control.Controls.Count > 0)
                        {
                            FindControls(control.Controls, result);
                        }
                        break;
                }
            }
        }
        #endregion

        #region データ表示部の設定
        /// <summary>
        /// データ表示部の設定
        /// </summary>
        /// <param name="formControls"></param>
        /// <param name="readOnly"></param>
        public static void SetControlEnabled(Control.ControlCollection formControls, bool readOnly)
        {
            // iniファイルを取得する
            var settings = new IniFile.Settings();
            // フォームで定義された、取得値設定先のコントロールを抽出する
            var controls = new List<Control>();
            FindControls(formControls, controls);
            foreach (var control in controls)
            {
                switch (control)
                {
                    case LabelTextBox labelTextBox:
                        // LabelTextBoxコントロールへの設定
                        if (!string.IsNullOrEmpty(labelTextBox.DatabaseColumnName))
                        {
                            labelTextBox.DataReadOnly = readOnly;
                        }

                        switch (labelTextBox.TextBackColor.Name)
                        {
                            case "Window":
                                labelTextBox.TextBackColor = BACK_COLOR;
                                labelTextBox.TextForeColor = FORE_COLOR;
                                break;
                        }
                        break;
                    case LabelTextSeparate labelTextSeparate:
                        // LabelTextSeparateコントロールへの設定
                        switch (labelTextSeparate.PanelBackColor.Name)
                        {
                            case "Window":
                                labelTextSeparate.PanelBackColor = BACK_COLOR;
                                labelTextSeparate.Label1BackColor = BACK_COLOR;
                                labelTextSeparate.Label2BackColor = BACK_COLOR;
                                labelTextSeparate.Label1ForeColor = FORE_COLOR;
                                labelTextSeparate.Label2ForeColor = FORE_COLOR;
                                break;
                        }
                        break;
                    case LabelCodeText labelCodeText:
                        // LabelCodeTextコントロールへの設定
                        if (!string.IsNullOrEmpty(labelCodeText.DatabaseColumnName))
                        {
                            labelCodeText.DataReadOnly = readOnly;
                        }
                        if (!string.IsNullOrEmpty(labelCodeText.DatabaseColumnCode))
                        {
                            labelCodeText.CodeReadOnly = readOnly;
                        }

                        switch (labelCodeText.TextBackColor.Name)
                        {
                            case "Window":
                                labelCodeText.TextBackColor = BACK_COLOR;
                                labelCodeText.TextForeColor = FORE_COLOR;
                                break;
                        }
                        switch (labelCodeText.CodeBackColor.Name)
                        {
                            case "ffffffc0":
                                labelCodeText.CodeBackColor = BACK_COLOR;
                                labelCodeText.CodeForeColor = FORE_COLOR;
                                break;
                        }
                        break;
                    case LabelNumericUpDown labelNumericUpDown:
                        // LabelNumericUpDownコントロールへの設定
                        switch (labelNumericUpDown.TextBackColor.Name)
                        {
                            case "Window":
                                labelNumericUpDown.TextBackColor = BACK_COLOR;
                                labelNumericUpDown.TextForeColor = FORE_COLOR;
                                break;
                        }
                        break;
                    case LabelNumericUpDownMulti labelNumericUpDownMulti:
                        // LabelNumericUpDownコントロールへの設定
                        switch (labelNumericUpDownMulti.TextLeftBackColor.Name)
                        {
                            case "Window":
                                labelNumericUpDownMulti.TextLeftBackColor = BACK_COLOR;
                                labelNumericUpDownMulti.TextRightBackColor = BACK_COLOR;
                                labelNumericUpDownMulti.TextLeftForeColor = FORE_COLOR;
                                labelNumericUpDownMulti.TextRightForeColor = FORE_COLOR;
                                break;
                        }
                        break;
                    case LabelDropDown labelDropDown:
                        // LabelDropDownコントロールへの設定
                        switch (labelDropDown.TextBackColor.Name)
                        {
                            case "Window":
                                labelDropDown.TextBackColor = BACK_COLOR;
                                labelDropDown.TextForeColor = FORE_COLOR;
                                break;
                        }
                        break;
                    case LabelTextButton labelTextButton:
                        // LabelDropDownコントロールへの設定
                        switch (labelTextButton.TextBackColor.Name)
                        {
                            case "Window":
                                labelTextButton.TextBackColor = BACK_COLOR;
                                labelTextButton.TextForeColor = FORE_COLOR;
                                break;
                        }
                        break;
                    case TabPage tabPage:
                        switch (tabPage.BackColor.Name)
                        {
                            case "Control":
                                tabPage.BackColor = BACK_COLOR;
                                break;
                        }
                        break;
                    case DateTimePicker dateTimePicker:
                        //LabelDateTimePickerコントロールへの設定
                        switch (dateTimePicker.CalendarMonthBackground.Name)
                        {
                            case "Window":
                                dateTimePicker.CalendarMonthBackground = BACK_COLOR;
                                dateTimePicker.CalendarForeColor = FORE_COLOR;
                                break;
                        }
                        break;
                    case ComboBoxEx comboBoxEx:
                        switch (comboBoxEx.BackColor.Name)
                        {
                            case "Window":
                                comboBoxEx.BackColor = BACK_COLOR;
                                comboBoxEx.ForeColor = FORE_COLOR;
                                break;
                        }
                        break;
                    case DataGridView dg:
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region 10数からRGB / RGBから10数
        /// <summary>
        /// RGBから10進数
        /// </summary>
        /// <param name="colorDialog"></param>
        /// <returns></returns>
        public static int RgbToInt(ColorDialog colorDialog)
        {
            //取得した色のRGBそれぞれをbyte配列から16進数へ変換　         
            var data = new byte[] { colorDialog.Color.R, colorDialog.Color.G, colorDialog.Color.B };
            var val = BitConverter.ToString(data).Replace("-", string.Empty);
            //16進数から10進数へ変換
            int rgbToInt = Convert.ToInt32(val, 16);

            return rgbToInt;
        }

        /// <summary>
        /// 10数からRGBA
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Color RgbFromInt(object obj)
        {
            //DBのRGBの値(10進数)を16進数
            int intObj = int.Parse(obj.ToString());
            string rgbFromInt = "#" + intObj.ToString("X6");
            //16進数からRGBAへ変換
            Color rgb = ColorTranslator.FromHtml(rgbFromInt);

            return rgb;
        }
        #endregion

        #region データベースへの登録値に変換する
        /// <summary>
        /// データベースへの登録値に変換する
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetDatabaseValue(object value)
        {
            switch (value)
            {
                case DateTime dt:
                    return $"'{string.Format("yyyy/MM/dd HH:mm:ss.fff")}'";
                case int i:
                    return $"{i}";
                case float f:
                    return $"{f}";
                case string s:
                    return $"'{value}'";
                default:
                    return $"'{value}'";
            }
        }
        #endregion

        #region DataTableの変換
        /// <summary>
        /// DataTableの変換
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DataTable ConvertDataTable(DataTable source)
        {
            DataTable dt = new DataTable();
            foreach (DataColumn column in source.Columns)
            {
                dt.Columns.Add(column.ColumnName);
            }
            foreach (DataRow row in source.Rows)
            {
                var dr = dt.NewRow();
                foreach (DataColumn column in source.Columns)
                {
                    switch (column.ColumnName)
                    {
                        case "StatusText":
                            dr[column.ColumnName] = Messages.GetOrderStatusText((Database.Sql.NpMain.Orders.OrderStatus)row["Status"]);
                            break;
                        default:
                            dr[column.ColumnName] = row[column.ColumnName];
                            break;
                    }
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        #endregion

        #region stringからintへ変換（TryParse)
        /// <summary>
        /// stringからintへ変換（TryParse)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int StrToInt(string value)
        {
            int.TryParse(value, out int intVal);
            return intVal;
        }
        #endregion

        #region stringからdoubleへ変換（TryParse)
        /// <summary>
        /// stringからdoubleへ変換（TryParse)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static double StrToDouble(string value)
        {
            double.TryParse(value, out double doubleVal);
            return doubleVal;
        }
        #endregion

        #region
        /// <summary>
        /// 一覧表示用GridViewのカラム設定及びフォントサイズ設定
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="settings"></param>
        /// <param name="gv"></param>
        public static void BindDataGridView(DataTable dt, List<Settings.GridViewSetting> settings, DataGridView gv)
        {
            var fontSizeProductCode = FONTSIZE_PRODUCT_CODE;
            var fontSizeDefault = FONTSIZE_DEFAULT;
            switch (gv.Name)
            {
                case "GvOrder":
                    fontSizeProductCode = FONTSIZE_PRODUCT_CODE_GVORDER;
                    fontSizeDefault = FONTSIZE_DEFAULT_GVORDER;
                    break;
            }
            // テーブル設定を元にカラムの作成
            foreach (var setting in settings)
            {
                var col = new DataGridViewColumn();
                col.Width = setting.Width;
                col.Visible = setting.Visible;
                col.DefaultCellStyle.Alignment = setting.alignment;
                col.DataPropertyName = setting.DisplayName;
                col.Name = setting.DisplayName;
                col.HeaderText = setting.DisplayName;
                // フォントサイズ変更
                switch (setting.ColumnName)
                {
                    case "Product_Code":
                        col.DefaultCellStyle.Font = new Font(gv.Font.Name, fontSizeProductCode);
                        break;
                    default:
                        col.DefaultCellStyle.Font = new Font(gv.Font.Name, fontSizeDefault);
                        break;
                }
                col.CellTemplate = new DataGridViewTextBoxCell();
                gv.Columns.Add(col);
            }
            // BindingSourceオブジェクト作成
            var bindingSource = new BindingSource();
            // DataTableをBindingSourceオブジェクトにセット
            bindingSource.DataSource = dt;
            // DataGridViewのDataSourceはBindingSourceオブジェクトにする
            gv.DataSource = bindingSource;
        }
        #endregion

        #region 強調するセルの確認
        /// <summary>
        /// 強調するセルの確認
        /// </summary>
        /// <param name="row"></param>
        /// <param name="columnDelivaryCode"></param>
        /// <param name="columnShippingDay"></param>
        /// <param name="columnDelivarDayy"></param>
        /// <returns></returns>
        public static bool EmphasisCellConfimation(DataGridViewRow row, int columnDelivaryCode, int columnShippingDay, int columnDelivarDayy)
        {
            return (StrToInt(row.Cells[columnDelivaryCode].Value.ToString()) == (int)Database.Sql.NpMain.Orders.DeliveryCode.Reuse && DateTime.Parse(row.Cells[columnShippingDay].Value.ToString()) <= DateTime.Today && DateTime.Parse(row.Cells[columnDelivarDayy].Value.ToString()) > DateTime.Today);
        }
        #endregion
    }
}
