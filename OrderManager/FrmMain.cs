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
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using NipponPaint.NpCommon;
using NipponPaint.NpCommon.Database;
using NipponPaint.NpCommon.FormControls;
using NipponPaint.OrderManager.Dialogs;
using NipponPaint.NpCommon.Settings;
using Sql = NipponPaint.NpCommon.Database.Sql;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.Data.SqlClient;
#endregion

namespace NipponPaint.OrderManager
{
    /// <summary>
    /// メイン画面
    /// </summary>
    public partial class FrmMain : BaseForm
    {
        #region 定数
        private static List<Color> StatusBackColorList = new List<Color>() {
            Color.LightPink,
            Color.Red,
            Color.Lime,
            Color.Cyan,
            Color.Blue,
            Color.White,
        };
        private static readonly Color ALERT_BACK_COLOR_RED = Color.Red;

        private static DataTable GvOrderDataSource;
        //private static DataTable GvDetailDataSource;
        //private static DataTable GvFormulationDataSource;
        private static DataTable GvOrderNumberDataSource;
        private static DataTable GvBarcodeDataCource;

        private const string Decimal_Place2 = "0.00";
        private const string Decimal_Place3 = "0.000";

        private const int COLUMN_STATUS = 1;
        private const int COLUMN_STATUS_COLOR = 0;
        //private int COLUMN_PRODUCT_NAME = 0;
        //private int COLUMN_COLOR_NAME = 0;
        private const int COLUMN_ORDER_ID = 2;
        private const int COLUMN_DELIVERY_CODE = 0;
        private const int COLUMN_SHIPPING_ID = 3;
        private const int COLUMN_PRODUCT_CODE = 4;
        private const int COLUMN_PRODUCT_NAME = 5;
        private const int COLUMN_VOLUME_CODE = 6;
        private const int COLUMN_NUMBER_OF_CANS = 7;
        private const int COLUMN_SHIPPING_DATE = 8;
        private const int COLUMN_VISIBLE_SHIPPING_DATE = 9;
        private const int COLUMN_VISIBLE_OPERATOR = 10;
        private const int COLUMN_DELIVERY_DATE = 11;
        private const int COLUMN_VISIBLE_DELIVERY_DATE = 12;
        private const int COLUMN_COLOR_SAMPLE = 13;
        private const int COLUMN_URGENT = 18;
        private const int COLUMN_OPERATOR = 10;

        private const int TAB_INDEX_ORDER = 0;
        private const int TAB_INDEX_DETAIL = 1;
        private const int TAB_INDEX_FORMULATION = 2;
        private const int TAB_INDEX_CAN = 3;

        //GvOrderの各種文字サイズと行高さ
        private const int GVORDER_ROW_HEIGHT = 32;
        private const int GVORDER_FONTSIZE_PRODUCTCODE = 16;
        private const int GVORDER_FONTSIZE_DEFAULT = 8;

        ////色名セパレータの1行あたりの文字数
        private const int COLOR_NAME_LOW_LIMIT = 0;
        private const int COLOR_NAME_HIGH_LIMIT = 14;

        private List<string> ViewGrid = new List<string>();
        //private const Log.ApplicationType MyApp = Log.ApplicationType.OrderManager;

        /// <summary>
        /// 強調するセルの背景色
        /// </summary>
        private readonly Color EMPHASIS_CELL_COLOR = Color.Orange;

        #region ソート
        // 運送区分
        private const string SORT_KUBUN = "[Status], [SS出荷予定日日付型], [並び順], [順位コード], [品名], [運送区分] ASC";
        // 順位コード
        private const string SORT_RANKING = "[Status] , [SS出荷予定日日付型] , [順位コード] , [並び順] , [品名] ASC";
        // 担当者名
        private const string SORT_ORDER_PERSON = "[担当者コード] , [Status] , [順位コード] , [並び順] , [品名] ASC";
        #endregion

        /// <summary>
        /// 選択行：1行選択なので値は「0」
        /// </summary>
        private const int SELECTED_ROW = 0;

        /// <summary>
        /// 注文のキャンセルフラグ
        /// </summary>
        private const int ORDER_CANCEL_FLG = 1;

        #region 列定義定数(ColumnName)
        private const string COLUMN_NAME_ORDERS_HG_HG_DELIVERY_CODE = Sql.NpMain.Orders.COLUMN_HG_HG_DELIVERY_CODE;
        private const string COLUMN_NAME_ORDERS_STATUS = Sql.NpMain.Orders.COLUMN_STATUS;
        private const string COLUMN_NAME_ORDERS_ORDER_ID = Sql.NpMain.Orders.COLUMN_ORDER_ID;
        private const string COLUMN_NAME_ORDERS_HG_HG_SHIPPING_ID = Sql.NpMain.Orders.COLUMN_HG_HG_SHIPPING_ID;
        private const string COLUMN_NAME_ORDERS_PRODUCT_CODE = Sql.NpMain.Orders.COLUMN_PRODUCT_CODE;
        private const string COLUMN_NAME_ORDERS_HG_PRODUCT_NAME = Sql.NpMain.Orders.COLUMN_HG_PRODUCT_NAME;
        private const string COLUMN_NAME_ORDERS_HG_VOLUME_CODE = Sql.NpMain.Orders.COLUMN_HG_VOLUME_CODE;
        private const string COLUMN_NAME_ORDERS_NUMBER_OF_CAN = Sql.NpMain.Orders.COLUMN_NUMBER_OF_CAN;
        private const string COLUMN_NAME_ORDERS_HG_SS_SHIPPING_DATE = Sql.NpMain.Orders.COLUMN_HG_SS_SHIPPING_DATE;
        private const string COLUMN_NAME_ORDERS_OPERATOR_NAME = Sql.NpMain.Orders.COLUMN_OPERATOR_NAME;
        private const string COLUMN_NAME_ORDERS_HG_DELIVERY_DATE = Sql.NpMain.Orders.COLUMN_HG_DELIVERY_DATE;
        private const string COLUMN_NAME_ORDERS_HG_COLOR_SAMPLE = Sql.NpMain.Orders.COLUMN_HG_COLOR_SAMPLE;
        private const string COLUMN_NAME_ORDERS_COLOR_NAME = Sql.NpMain.Orders.COLUMN_COLOR_NAME;
        private const string COLUMN_NAME_ORDERS_HG_SUM_UP_KEY = Sql.NpMain.Orders.COLUMN_HG_SUM_UP_KEY;
        private const string COLUMN_NAME_ORDERS_OPERATOR_CODE = Sql.NpMain.Orders.COLUMN_OPERATOR_CODE;
        private const string COLUMN_NAME_ORDERS_SORT_ORDER = Sql.NpMain.Orders.COLUMN_SORT_ORDER;
        private const string COLUMN_NAME_ORDERS_URGENT = Sql.NpMain.Orders.COLUMN_URGENT;
        private const string COLUMN_NAME_ORDERS_STATUSCOLOR = Sql.NpMain.Orders.COLUMN_STATUSCOLOR;
        private const string COLUMN_NAME_ORDERS_ORDER_NUMBER = Sql.NpMain.Orders.COLUMN_ORDER_NUMBER;
        private const string COLUMN_NAME_ORDERS_REVISION = Sql.NpMain.Orders.COLUMN_REVISION;
        private const string COLUMN_NAME_ORDERS_FORMULA_RELEASE = Sql.NpMain.Orders.COLUMN_FORMULA_RELEASE;
        private const string COLUMN_NAME_ORDERS_WHITE_CODE = Sql.NpMain.Orders.COLUMN_WHITE_CODE;
        private const string COLUMN_NAME_ORDERS_WHITE_WEIGHT = Sql.NpMain.Orders.COLUMN_WHITE_WEIGHT;
        private const string COLUMN_NAME_ORDERS_COLORANT_1 = Sql.NpMain.Orders.COLUMN_COLORANT_1;
        private const string COLUMN_NAME_ORDERS_WEIGHT_1 = Sql.NpMain.Orders.COLUMN_WEIGHT_1;
        private const string COLUMN_NAME_ORDERS_COLORANT_2 = Sql.NpMain.Orders.COLUMN_COLORANT_2;
        private const string COLUMN_NAME_ORDERS_WEIGHT_2 = Sql.NpMain.Orders.COLUMN_WEIGHT_2;
        private const string COLUMN_NAME_ORDERS_COLORANT_3 = Sql.NpMain.Orders.COLUMN_COLORANT_3;
        private const string COLUMN_NAME_ORDERS_WEIGHT_3 = Sql.NpMain.Orders.COLUMN_WEIGHT_3;
        private const string COLUMN_NAME_ORDERS_COLORANT_4 = Sql.NpMain.Orders.COLUMN_COLORANT_4;
        private const string COLUMN_NAME_ORDERS_WEIGHT_4 = Sql.NpMain.Orders.COLUMN_WEIGHT_4;
        private const string COLUMN_NAME_ORDERS_COLORANT_5 = Sql.NpMain.Orders.COLUMN_COLORANT_5;
        private const string COLUMN_NAME_ORDERS_WEIGHT_5 = Sql.NpMain.Orders.COLUMN_WEIGHT_5;
        private const string COLUMN_NAME_ORDERS_COLORANT_6 = Sql.NpMain.Orders.COLUMN_COLORANT_6;
        private const string COLUMN_NAME_ORDERS_WEIGHT_6 = Sql.NpMain.Orders.COLUMN_WEIGHT_6;
        private const string COLUMN_NAME_ORDERS_COLORANT_7 = Sql.NpMain.Orders.COLUMN_COLORANT_7;
        private const string COLUMN_NAME_ORDERS_WEIGHT_7 = Sql.NpMain.Orders.COLUMN_WEIGHT_7;
        private const string COLUMN_NAME_ORDERS_COLORANT_8 = Sql.NpMain.Orders.COLUMN_COLORANT_8;
        private const string COLUMN_NAME_ORDERS_WEIGHT_8 = Sql.NpMain.Orders.COLUMN_WEIGHT_8;
        private const string COLUMN_NAME_ORDERS_COLORANT_9 = Sql.NpMain.Orders.COLUMN_COLORANT_9;
        private const string COLUMN_NAME_ORDERS_WEIGHT_9 = Sql.NpMain.Orders.COLUMN_WEIGHT_9;
        private const string COLUMN_NAME_ORDERS_COLORANT_10 = Sql.NpMain.Orders.COLUMN_COLORANT_10;
        private const string COLUMN_NAME_ORDERS_WEIGHT_10 = Sql.NpMain.Orders.COLUMN_WEIGHT_10;
        private const string COLUMN_NAME_ORDERS_COLORANT_11 = Sql.NpMain.Orders.COLUMN_COLORANT_11;
        private const string COLUMN_NAME_ORDERS_WEIGHT_11 = Sql.NpMain.Orders.COLUMN_WEIGHT_11;
        private const string COLUMN_NAME_ORDERS_COLORANT_12 = Sql.NpMain.Orders.COLUMN_COLORANT_12;
        private const string COLUMN_NAME_ORDERS_WEIGHT_12 = Sql.NpMain.Orders.COLUMN_WEIGHT_12;
        private const string COLUMN_NAME_ORDERS_COLORANT_13 = Sql.NpMain.Orders.COLUMN_COLORANT_13;
        private const string COLUMN_NAME_ORDERS_WEIGHT_13 = Sql.NpMain.Orders.COLUMN_WEIGHT_13;
        private const string COLUMN_NAME_ORDERS_COLORANT_14 = Sql.NpMain.Orders.COLUMN_COLORANT_14;
        private const string COLUMN_NAME_ORDERS_WEIGHT_14 = Sql.NpMain.Orders.COLUMN_WEIGHT_14;
        private const string COLUMN_NAME_ORDERS_COLORANT_15 = Sql.NpMain.Orders.COLUMN_COLORANT_15;
        private const string COLUMN_NAME_ORDERS_WEIGHT_15 = Sql.NpMain.Orders.COLUMN_WEIGHT_15;
        private const string COLUMN_NAME_ORDERS_COLORANT_16 = Sql.NpMain.Orders.COLUMN_COLORANT_16;
        private const string COLUMN_NAME_ORDERS_WEIGHT_16 = Sql.NpMain.Orders.COLUMN_WEIGHT_16;
        private const string COLUMN_NAME_ORDERS_COLORANT_17 = Sql.NpMain.Orders.COLUMN_COLORANT_17;
        private const string COLUMN_NAME_ORDERS_WEIGHT_17 = Sql.NpMain.Orders.COLUMN_WEIGHT_17;
        private const string COLUMN_NAME_ORDERS_COLORANT_18 = Sql.NpMain.Orders.COLUMN_COLORANT_18;
        private const string COLUMN_NAME_ORDERS_WEIGHT_18 = Sql.NpMain.Orders.COLUMN_WEIGHT_18;
        private const string COLUMN_NAME_ORDERS_COLORANT_19 = Sql.NpMain.Orders.COLUMN_COLORANT_19;
        private const string COLUMN_NAME_ORDERS_WEIGHT_19 = Sql.NpMain.Orders.COLUMN_WEIGHT_19;
        private const string COLUMN_NAME_CANS_BARCODE = Sql.NpMain.Cans.COLUMN_BARCODE;
        private const string COLUMN_NAME_CANS_ORDER_ID = Sql.NpMain.Cans.COLUMN_ORDER_ID;
        private const string COLUMN_NAME_CANS_CAN_NUMBER = Sql.NpMain.Cans.COLUMN_CAN_NUMBER;
        private const string COLUMN_NAME_CANS_STATUS = Sql.NpMain.Cans.COLUMN_STATUS;
        private const string COLUMN_NAME_CANS_TEST_CAN = Sql.NpMain.Cans.COLUMN_TEST_CAN;
        private const string COLUMN_NAME_CANS_SAMPLE_PRESENT = Sql.NpMain.Cans.COLUMN_SAMPLE_PRESENT;
        private const string COLUMN_NAME_CANS_FORMULA_RELEASE = Sql.NpMain.Cans.COLUMN_FORMULA_RELEASE;
        private const string COLUMN_NAME_CANS_WHITE_CODE = Sql.NpMain.Cans.COLUMN_WHITE_CODE;
        private const string COLUMN_NAME_CANS_WHITE_DISPENSED = Sql.NpMain.Cans.COLUMN_WHITE_DISPENSED;
        private const string COLUMN_NAME_CANS_COLORANT_1 = Sql.NpMain.Cans.COLUMN_COLORANT_1;
        private const string COLUMN_NAME_CANS_DISPENSED_1 = Sql.NpMain.Cans.COLUMN_DISPENSED_1;
        private const string COLUMN_NAME_CANS_COLORANT_2 = Sql.NpMain.Cans.COLUMN_COLORANT_2;
        private const string COLUMN_NAME_CANS_DISPENSED_2 = Sql.NpMain.Cans.COLUMN_DISPENSED_2;
        private const string COLUMN_NAME_CANS_COLORANT_3 = Sql.NpMain.Cans.COLUMN_COLORANT_3;
        private const string COLUMN_NAME_CANS_DISPENSED_3 = Sql.NpMain.Cans.COLUMN_DISPENSED_3;
        private const string COLUMN_NAME_CANS_COLORANT_4 = Sql.NpMain.Cans.COLUMN_COLORANT_4;
        private const string COLUMN_NAME_CANS_DISPENSED_4 = Sql.NpMain.Cans.COLUMN_DISPENSED_4;
        private const string COLUMN_NAME_CANS_COLORANT_5 = Sql.NpMain.Cans.COLUMN_COLORANT_5;
        private const string COLUMN_NAME_CANS_DISPENSED_5 = Sql.NpMain.Cans.COLUMN_DISPENSED_5;
        private const string COLUMN_NAME_CANS_COLORANT_6 = Sql.NpMain.Cans.COLUMN_COLORANT_6;
        private const string COLUMN_NAME_CANS_DISPENSED_6 = Sql.NpMain.Cans.COLUMN_DISPENSED_6;
        private const string COLUMN_NAME_CANS_COLORANT_7 = Sql.NpMain.Cans.COLUMN_COLORANT_7;
        private const string COLUMN_NAME_CANS_DISPENSED_7 = Sql.NpMain.Cans.COLUMN_DISPENSED_7;
        private const string COLUMN_NAME_CANS_COLORANT_8 = Sql.NpMain.Cans.COLUMN_COLORANT_8;
        private const string COLUMN_NAME_CANS_DISPENSED_8 = Sql.NpMain.Cans.COLUMN_DISPENSED_8;
        private const string COLUMN_NAME_CANS_COLORANT_9 = Sql.NpMain.Cans.COLUMN_COLORANT_9;
        private const string COLUMN_NAME_CANS_DISPENSED_9 = Sql.NpMain.Cans.COLUMN_DISPENSED_9;
        private const string COLUMN_NAME_CANS_COLORANT_10 = Sql.NpMain.Cans.COLUMN_COLORANT_10;
        private const string COLUMN_NAME_CANS_DISPENSED_10 = Sql.NpMain.Cans.COLUMN_DISPENSED_10;
        private const string COLUMN_NAME_CANS_COLORANT_11 = Sql.NpMain.Cans.COLUMN_COLORANT_11;
        private const string COLUMN_NAME_CANS_DISPENSED_11 = Sql.NpMain.Cans.COLUMN_DISPENSED_11;
        private const string COLUMN_NAME_CANS_COLORANT_12 = Sql.NpMain.Cans.COLUMN_COLORANT_12;
        private const string COLUMN_NAME_CANS_DISPENSED_12 = Sql.NpMain.Cans.COLUMN_DISPENSED_12;
        private const string COLUMN_NAME_CANS_COLORANT_13 = Sql.NpMain.Cans.COLUMN_COLORANT_13;
        private const string COLUMN_NAME_CANS_DISPENSED_13 = Sql.NpMain.Cans.COLUMN_DISPENSED_13;
        private const string COLUMN_NAME_CANS_COLORANT_14 = Sql.NpMain.Cans.COLUMN_COLORANT_14;
        private const string COLUMN_NAME_CANS_DISPENSED_14 = Sql.NpMain.Cans.COLUMN_DISPENSED_14;
        private const string COLUMN_NAME_CANS_COLORANT_15 = Sql.NpMain.Cans.COLUMN_COLORANT_15;
        private const string COLUMN_NAME_CANS_DISPENSED_15 = Sql.NpMain.Cans.COLUMN_DISPENSED_15;
        private const string COLUMN_NAME_CANS_COLORANT_16 = Sql.NpMain.Cans.COLUMN_COLORANT_16;
        private const string COLUMN_NAME_CANS_DISPENSED_16 = Sql.NpMain.Cans.COLUMN_DISPENSED_16;
        private const string COLUMN_NAME_CANS_COLORANT_17 = Sql.NpMain.Cans.COLUMN_COLORANT_17;
        private const string COLUMN_NAME_CANS_DISPENSED_17 = Sql.NpMain.Cans.COLUMN_DISPENSED_17;
        private const string COLUMN_NAME_CANS_COLORANT_18 = Sql.NpMain.Cans.COLUMN_COLORANT_18;
        private const string COLUMN_NAME_CANS_DISPENSED_18 = Sql.NpMain.Cans.COLUMN_DISPENSED_18;
        private const string COLUMN_NAME_CANS_COLORANT_19 = Sql.NpMain.Cans.COLUMN_COLORANT_19;
        private const string COLUMN_NAME_CANS_DISPENSED_19 = Sql.NpMain.Cans.COLUMN_DISPENSED_19;
        private const string COLUMN_NAME_CANS_WHITE_WEIGHT = Sql.NpMain.Cans.COLUMN_WHITE_WEIGHT;
        #endregion
        #region 列定義定数(DisplayName)
        private const string DISPLAY_NAME_HG_HG_DELIVERY_CODE = "運送区分";
        private const string DISPLAY_NAME_STATUS = "Status";
        private const string DISPLAY_NAME_ORDER_ID = "Order_id";
        private const string DISPLAY_NAME_HG_HG_SHIPPING_ID = "配送ﾓｰﾄﾞ";
        private const string DISPLAY_NAME_PRODUCT_CODE = "製品ｺｰﾄﾞ";
        private const string DISPLAY_NAME_HG_PRODUCT_NAME = "品名";
        private const string DISPLAY_NAME_HG_VOLUME_CODE = "容量ｺｰﾄﾞ";
        private const string DISPLAY_NAME_NUMBER_OF_CAN = "缶数";
        private const string DISPLAY_NAME_HG_SS_SHIPPING_DATE = "SS出荷予定日";
        private const string DISPLAY_NAME_HG_SS_SHIPPING_PATTERN = "SS出荷予定日日付型";
        private const string DISPLAY_NAME_OPERATOR_NAME = "担当者";
        private const string DISPLAY_NAME_HG_DELIVERY_DATE = "納期";
        private const string DISPLAY_NAME_HG_DELIVERY_DATE_PATTERN = "納期日付型";
        private const string DISPLAY_NAME_HG_COLOR_SAMPLE = "標準色見本";
        private const string DISPLAY_NAME_COLOR_NAME = "色名";
        private const string DISPLAY_NAME_HG_SUM_UP_KEY = "順位コード";
        private const string DISPLAY_NAME_OPERATOR_CODE = "担当者コード";
        private const string DISPLAY_NAME_SORT_ORDER = "並び順";
        private const string DISPLAY_NAME_URGENT = "Urgent";
        private const string DISPLAY_NAME_CODE = "コード";
        private const string DISPLAY_NAME_WEIGHT = "重量[g]";
        private const string DISPLAY_NAME_STATUSCOLOR = "StatusColor";
        private const string DISPLAY_NAME_ORDER_NUMBER = "注文番号";
        private const string DISPLAY_NAME_REVISION = "補正";
        private const string DISPLAY_NAME_FORMULA_RELEASE = "配合受取";
        private const string DISPLAY_NAME_WHITE_CODE = "白コード";
        private const string DISPLAY_NAME_WHITE_WEIGHT = "白重量";
        private const string DISPLAY_NAME_COLORANT_1 = "着色剤1";
        private const string DISPLAY_NAME_WEIGHT_1 = "重量1";
        private const string DISPLAY_NAME_COLORANT_2 = "着色剤2";
        private const string DISPLAY_NAME_WEIGHT_2 = "重量2";
        private const string DISPLAY_NAME_COLORANT_3 = "着色剤3";
        private const string DISPLAY_NAME_WEIGHT_3 = "重量3";
        private const string DISPLAY_NAME_COLORANT_4 = "着色剤4";
        private const string DISPLAY_NAME_WEIGHT_4 = "重量4";
        private const string DISPLAY_NAME_COLORANT_5 = "着色剤5";
        private const string DISPLAY_NAME_WEIGHT_5 = "重量5";
        private const string DISPLAY_NAME_COLORANT_6 = "着色剤6";
        private const string DISPLAY_NAME_WEIGHT_6 = "重量6";
        private const string DISPLAY_NAME_COLORANT_7 = "着色剤7";
        private const string DISPLAY_NAME_WEIGHT_7 = "重量7";
        private const string DISPLAY_NAME_COLORANT_8 = "着色剤8";
        private const string DISPLAY_NAME_WEIGHT_8 = "重量8";
        private const string DISPLAY_NAME_COLORANT_9 = "着色剤9";
        private const string DISPLAY_NAME_WEIGHT_9 = "重量9";
        private const string DISPLAY_NAME_COLORANT_10 = "着色剤10";
        private const string DISPLAY_NAME_WEIGHT_10 = "重量10";
        private const string DISPLAY_NAME_COLORANT_11 = "着色剤11";
        private const string DISPLAY_NAME_WEIGHT_11 = "重量11";
        private const string DISPLAY_NAME_COLORANT_12 = "着色剤12";
        private const string DISPLAY_NAME_WEIGHT_12 = "重量12";
        private const string DISPLAY_NAME_COLORANT_13 = "着色剤13";
        private const string DISPLAY_NAME_WEIGHT_13 = "重量13";
        private const string DISPLAY_NAME_COLORANT_14 = "着色剤14";
        private const string DISPLAY_NAME_WEIGHT_14 = "重量14";
        private const string DISPLAY_NAME_COLORANT_15 = "着色剤15";
        private const string DISPLAY_NAME_WEIGHT_15 = "重量15";
        private const string DISPLAY_NAME_COLORANT_16 = "着色剤16";
        private const string DISPLAY_NAME_WEIGHT_16 = "重量16";
        private const string DISPLAY_NAME_COLORANT_17 = "着色剤17";
        private const string DISPLAY_NAME_WEIGHT_17 = "重量17";
        private const string DISPLAY_NAME_COLORANT_18 = "着色剤18";
        private const string DISPLAY_NAME_WEIGHT_18 = "重量18";
        private const string DISPLAY_NAME_COLORANT_19 = "着色剤19";
        private const string DISPLAY_NAME_WEIGHT_19 = "重量19";
        private const string DISPLAY_NAME_BARCODE = "バーコード";
        private const string DISPLAY_NAME_CAN_NUMBER = "缶";
        private const string DISPLAY_NAME_CANS_STATUS = "ステータス";
        private const string DISPLAY_NAME_TEST_CAN = "テスト";
        private const string DISPLAY_NAME_SAMPLE_PRESENT = "サンプル";
        private const string DISPLAY_NAME_CANS_FORMULA_RELEASE = "最新配合受取";
        private const string DISPLAY_NAME_WHITE_DISPENSED = "白吐出";
        private const string DISPLAY_NAME_DISPENSED_1 = "吐出1";
        private const string DISPLAY_NAME_DISPENSED_2 = "吐出2";
        private const string DISPLAY_NAME_DISPENSED_3 = "吐出3";
        private const string DISPLAY_NAME_DISPENSED_4 = "吐出4";
        private const string DISPLAY_NAME_DISPENSED_5 = "吐出5";
        private const string DISPLAY_NAME_DISPENSED_6 = "吐出6";
        private const string DISPLAY_NAME_DISPENSED_7 = "吐出7";
        private const string DISPLAY_NAME_DISPENSED_8 = "吐出8";
        private const string DISPLAY_NAME_DISPENSED_9 = "吐出9";
        private const string DISPLAY_NAME_DISPENSED_10 = "吐出10";
        private const string DISPLAY_NAME_DISPENSED_11 = "吐出11";
        private const string DISPLAY_NAME_DISPENSED_12 = "吐出12";
        private const string DISPLAY_NAME_DISPENSED_13 = "吐出13";
        private const string DISPLAY_NAME_DISPENSED_14 = "吐出14";
        private const string DISPLAY_NAME_DISPENSED_15 = "吐出15";
        private const string DISPLAY_NAME_DISPENSED_16 = "吐出16";
        private const string DISPLAY_NAME_DISPENSED_17 = "吐出17";
        private const string DISPLAY_NAME_DISPENSED_18 = "吐出18";
        private const string DISPLAY_NAME_DISPENSED_19 = "吐出19";
        #endregion
        #endregion


        #region DataGridViewの列定義
        private List<GridViewSetting> ViewSettingsOrders = new List<GridViewSetting>()
        {
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_HG_HG_DELIVERY_CODE, DisplayName = DISPLAY_NAME_HG_HG_DELIVERY_CODE, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_STATUS, DisplayName = DISPLAY_NAME_STATUS, Visible = false, Width = 0, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_ORDER_ID, DisplayName = DISPLAY_NAME_ORDER_ID, Visible = false, Width = 0, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_ORDERS_HG_HG_SHIPPING_ID, DisplayName = DISPLAY_NAME_HG_HG_SHIPPING_ID, Visible = true, Width = 100 } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_ORDERS_PRODUCT_CODE, DisplayName = DISPLAY_NAME_PRODUCT_CODE, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_ORDERS_HG_PRODUCT_NAME, DisplayName = DISPLAY_NAME_HG_PRODUCT_NAME, Visible = true, Width = 500 } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_ORDERS_HG_VOLUME_CODE, DisplayName = DISPLAY_NAME_HG_VOLUME_CODE, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_NUMBER_OF_CAN, DisplayName = DISPLAY_NAME_NUMBER_OF_CAN, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.DateTime, ColumnName = $"FORMAT(CONVERT(DATE,{COLUMN_NAME_ORDERS_HG_SS_SHIPPING_DATE}), 'MM/dd')", DisplayName = DISPLAY_NAME_HG_SS_SHIPPING_DATE, Visible = true, Width = 130, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.DateTime, ColumnName = $"CONVERT(DATE,{COLUMN_NAME_ORDERS_HG_SS_SHIPPING_DATE})", DisplayName = DISPLAY_NAME_HG_SS_SHIPPING_PATTERN, Visible = false, Width = 0, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_ORDERS_OPERATOR_NAME, DisplayName = DISPLAY_NAME_OPERATOR_NAME, Visible = true, Width = 100 } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.DateTime, ColumnName = $"FORMAT(CONVERT(DATE,{COLUMN_NAME_ORDERS_HG_DELIVERY_DATE}), 'MM/dd')", DisplayName = DISPLAY_NAME_HG_DELIVERY_DATE, Visible = true, Width = 100, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.DateTime, ColumnName = $"CONVERT(DATE,{COLUMN_NAME_ORDERS_HG_DELIVERY_DATE})", DisplayName = DISPLAY_NAME_HG_DELIVERY_DATE_PATTERN, Visible = false, Width = 0, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_ORDERS_HG_COLOR_SAMPLE, DisplayName = DISPLAY_NAME_HG_COLOR_SAMPLE, Visible = true, Width = 300 } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_ORDERS_COLOR_NAME, DisplayName = DISPLAY_NAME_COLOR_NAME, Visible = false, Width = 0 } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_ORDERS_HG_SUM_UP_KEY, DisplayName = DISPLAY_NAME_HG_SUM_UP_KEY, Visible = false, Width = 0 } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_ORDERS_OPERATOR_CODE, DisplayName = DISPLAY_NAME_OPERATOR_CODE, Visible = false, Width = 0 } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_SORT_ORDER, DisplayName = DISPLAY_NAME_SORT_ORDER, Visible = false, Width = 0 } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_URGENT, DisplayName = DISPLAY_NAME_URGENT, Visible = false, Width = 0, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = "Order_Number", DisplayName = "注文番号", Visible = false, Width = 0, alignment = DataGridViewContentAlignment.MiddleCenter } },
        };
        private List<GridViewSetting> ViewSettingsWeights = new List<GridViewSetting>()
        {
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_ORDERS_WHITE_CODE, DisplayName = DISPLAY_NAME_CODE, Visible = true, Width = 300, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_WHITE_WEIGHT, DisplayName = DISPLAY_NAME_WEIGHT, Visible = true, Width = 200, alignment = DataGridViewContentAlignment.MiddleRight } },
        };
        private List<GridViewSetting> ViewSettingsOrderNumbers = new List<GridViewSetting>()
        {
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Blank, ColumnName = COLUMN_NAME_ORDERS_STATUSCOLOR, DisplayName = DISPLAY_NAME_STATUSCOLOR, Visible = true, Width = 110, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_STATUS, DisplayName = DISPLAY_NAME_STATUS, Visible = false, Width = 35, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_ORDER_ID, DisplayName = DISPLAY_NAME_ORDER_ID, Visible = false, Width = 0, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_ORDERS_PRODUCT_CODE, DisplayName = DISPLAY_NAME_PRODUCT_CODE, Visible = true, Width = 110, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_NUMBER_OF_CAN, DisplayName = DISPLAY_NAME_NUMBER_OF_CAN, Visible = true, Width = 95, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_ORDERS_ORDER_NUMBER, DisplayName = DISPLAY_NAME_ORDER_NUMBER, Visible = true, Width = 240, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_REVISION, DisplayName = DISPLAY_NAME_REVISION, Visible = true, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = $"O.{COLUMN_NAME_ORDERS_FORMULA_RELEASE}", DisplayName = DISPLAY_NAME_FORMULA_RELEASE, Visible = true, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_ORDERS_COLOR_NAME, DisplayName = DISPLAY_NAME_COLOR_NAME, Visible = false, Width = 0 } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_ORDERS_HG_SUM_UP_KEY, DisplayName = DISPLAY_NAME_HG_SUM_UP_KEY, Visible = false, Width = 0 } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_ORDERS_OPERATOR_CODE, DisplayName = DISPLAY_NAME_OPERATOR_CODE, Visible = false, Width = 0 } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_SORT_ORDER, DisplayName = DISPLAY_NAME_SORT_ORDER, Visible = false, Width = 0 } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_ORDERS_WHITE_CODE, DisplayName = DISPLAY_NAME_WHITE_CODE, Visible = false, Width = 95 } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_WHITE_WEIGHT, DisplayName = DISPLAY_NAME_WHITE_WEIGHT, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"O.{COLUMN_NAME_ORDERS_COLORANT_1}", DisplayName = DISPLAY_NAME_COLORANT_1, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_WEIGHT_1, DisplayName = DISPLAY_NAME_WEIGHT_1, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"O.{COLUMN_NAME_ORDERS_COLORANT_2}", DisplayName = DISPLAY_NAME_COLORANT_2, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_WEIGHT_2, DisplayName = DISPLAY_NAME_WEIGHT_2, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"O.{COLUMN_NAME_ORDERS_COLORANT_3}", DisplayName = DISPLAY_NAME_COLORANT_3, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_WEIGHT_3, DisplayName = DISPLAY_NAME_WEIGHT_3, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"O.{COLUMN_NAME_ORDERS_COLORANT_4}", DisplayName = DISPLAY_NAME_COLORANT_4, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_WEIGHT_4, DisplayName = DISPLAY_NAME_WEIGHT_4, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"O.{COLUMN_NAME_ORDERS_COLORANT_5}", DisplayName = DISPLAY_NAME_COLORANT_5, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_WEIGHT_5, DisplayName = DISPLAY_NAME_WEIGHT_5, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"O.{COLUMN_NAME_ORDERS_COLORANT_6}", DisplayName = DISPLAY_NAME_COLORANT_6, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_WEIGHT_6, DisplayName = DISPLAY_NAME_WEIGHT_6, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"O.{COLUMN_NAME_ORDERS_COLORANT_7}", DisplayName = DISPLAY_NAME_COLORANT_7, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_WEIGHT_7, DisplayName = DISPLAY_NAME_WEIGHT_7, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"O.{COLUMN_NAME_ORDERS_COLORANT_8}", DisplayName = DISPLAY_NAME_COLORANT_8, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_WEIGHT_8, DisplayName = DISPLAY_NAME_WEIGHT_8, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"O.{COLUMN_NAME_ORDERS_COLORANT_9}", DisplayName = DISPLAY_NAME_COLORANT_9, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_WEIGHT_9, DisplayName = DISPLAY_NAME_WEIGHT_9, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"O.{COLUMN_NAME_ORDERS_COLORANT_10}", DisplayName = DISPLAY_NAME_COLORANT_10, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_WEIGHT_10, DisplayName = DISPLAY_NAME_WEIGHT_10, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"O.{COLUMN_NAME_ORDERS_COLORANT_11}", DisplayName = DISPLAY_NAME_COLORANT_11, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_WEIGHT_11, DisplayName = DISPLAY_NAME_WEIGHT_11, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"O.{COLUMN_NAME_ORDERS_COLORANT_12}", DisplayName = DISPLAY_NAME_COLORANT_12, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_WEIGHT_12, DisplayName = DISPLAY_NAME_WEIGHT_12, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"O.{COLUMN_NAME_ORDERS_COLORANT_13}", DisplayName = DISPLAY_NAME_COLORANT_13, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_WEIGHT_13, DisplayName = DISPLAY_NAME_WEIGHT_13, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"O.{COLUMN_NAME_ORDERS_COLORANT_14}", DisplayName = DISPLAY_NAME_COLORANT_14, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_WEIGHT_14, DisplayName = DISPLAY_NAME_WEIGHT_14, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"O.{COLUMN_NAME_ORDERS_COLORANT_15}", DisplayName = DISPLAY_NAME_COLORANT_15, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_WEIGHT_15, DisplayName = DISPLAY_NAME_WEIGHT_15, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"O.{COLUMN_NAME_ORDERS_COLORANT_16}", DisplayName = DISPLAY_NAME_COLORANT_16, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_WEIGHT_16, DisplayName = DISPLAY_NAME_WEIGHT_16, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"O.{COLUMN_NAME_ORDERS_COLORANT_17}", DisplayName = DISPLAY_NAME_COLORANT_17, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_WEIGHT_17, DisplayName = DISPLAY_NAME_WEIGHT_17, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"O.{COLUMN_NAME_ORDERS_COLORANT_18}", DisplayName = DISPLAY_NAME_COLORANT_18, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_WEIGHT_18, DisplayName = DISPLAY_NAME_WEIGHT_18, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"O.{COLUMN_NAME_ORDERS_COLORANT_19}", DisplayName = DISPLAY_NAME_COLORANT_19, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_WEIGHT_19, DisplayName = DISPLAY_NAME_WEIGHT_19, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_URGENT, DisplayName = DISPLAY_NAME_URGENT, Visible = false, Width = 0, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.DateTime, ColumnName = $"CONVERT(DATE,{COLUMN_NAME_ORDERS_HG_SS_SHIPPING_DATE})", DisplayName = DISPLAY_NAME_HG_SS_SHIPPING_PATTERN, Visible = false, Width = 0, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_ORDERS_HG_PRODUCT_NAME, DisplayName = DISPLAY_NAME_HG_PRODUCT_NAME, Visible = false, Width = 500 } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_ORDERS_HG_HG_DELIVERY_CODE, DisplayName = DISPLAY_NAME_HG_HG_DELIVERY_CODE, Visible = false, Width = 100, alignment = DataGridViewContentAlignment.MiddleCenter } },
        };
        private List<GridViewSetting> ViewSettingsBarcodes = new List<GridViewSetting>()
        {
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_CANS_BARCODE, DisplayName = DISPLAY_NAME_BARCODE, Visible = true, Width = 240, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = $"C.{COLUMN_NAME_CANS_ORDER_ID}", DisplayName = DISPLAY_NAME_ORDER_ID, Visible = false, Width = 0, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_CANS_CAN_NUMBER, DisplayName = DISPLAY_NAME_CAN_NUMBER, Visible = true, Width = 120, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Bit, ColumnName = $"C.{COLUMN_NAME_CANS_STATUS}", DisplayName = DISPLAY_NAME_CANS_STATUS, Visible = true, Width = 120, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Bit, ColumnName = COLUMN_NAME_CANS_TEST_CAN, DisplayName = DISPLAY_NAME_TEST_CAN, Visible = true, Width = 120, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Bit, ColumnName = COLUMN_NAME_CANS_SAMPLE_PRESENT, DisplayName = DISPLAY_NAME_SAMPLE_PRESENT, Visible = true, Width = 120, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Bit, ColumnName = $"C.{COLUMN_NAME_CANS_FORMULA_RELEASE}", DisplayName = DISPLAY_NAME_CANS_FORMULA_RELEASE, Visible = true, Width = 130, alignment = DataGridViewContentAlignment.MiddleCenter } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName =$"C.{COLUMN_NAME_CANS_WHITE_CODE}", DisplayName = DISPLAY_NAME_WHITE_CODE, Visible = false, Width = 95 } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_CANS_WHITE_DISPENSED, DisplayName = DISPLAY_NAME_WHITE_DISPENSED, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"C.{COLUMN_NAME_CANS_COLORANT_1}", DisplayName = DISPLAY_NAME_COLORANT_1, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_CANS_DISPENSED_1, DisplayName = DISPLAY_NAME_DISPENSED_1, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"C.{COLUMN_NAME_CANS_COLORANT_2}", DisplayName = DISPLAY_NAME_COLORANT_2, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_CANS_DISPENSED_2, DisplayName = DISPLAY_NAME_DISPENSED_2, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"C.{COLUMN_NAME_CANS_COLORANT_3}", DisplayName = DISPLAY_NAME_COLORANT_3, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_CANS_DISPENSED_3, DisplayName = DISPLAY_NAME_DISPENSED_3, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"C.{COLUMN_NAME_CANS_COLORANT_4}", DisplayName = DISPLAY_NAME_COLORANT_4, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_CANS_DISPENSED_4, DisplayName = DISPLAY_NAME_DISPENSED_4, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"C.{COLUMN_NAME_CANS_COLORANT_5}", DisplayName = DISPLAY_NAME_COLORANT_5, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_CANS_DISPENSED_5, DisplayName = DISPLAY_NAME_DISPENSED_5, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"C.{COLUMN_NAME_CANS_COLORANT_6}", DisplayName = DISPLAY_NAME_COLORANT_6, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_CANS_DISPENSED_6, DisplayName = DISPLAY_NAME_DISPENSED_6, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"C.{COLUMN_NAME_CANS_COLORANT_7}", DisplayName = DISPLAY_NAME_COLORANT_7, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_CANS_DISPENSED_7, DisplayName = DISPLAY_NAME_DISPENSED_7, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"C.{COLUMN_NAME_CANS_COLORANT_8}", DisplayName = DISPLAY_NAME_COLORANT_8, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_CANS_DISPENSED_8, DisplayName = DISPLAY_NAME_DISPENSED_8, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"C.{COLUMN_NAME_CANS_COLORANT_9}", DisplayName = DISPLAY_NAME_COLORANT_9, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_CANS_DISPENSED_9, DisplayName = DISPLAY_NAME_DISPENSED_9, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"C.{COLUMN_NAME_CANS_COLORANT_10}", DisplayName = DISPLAY_NAME_COLORANT_10, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_CANS_DISPENSED_10, DisplayName = DISPLAY_NAME_DISPENSED_10, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"C.{COLUMN_NAME_CANS_COLORANT_11}", DisplayName = DISPLAY_NAME_COLORANT_11, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_CANS_DISPENSED_11, DisplayName = DISPLAY_NAME_DISPENSED_11, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"C.{COLUMN_NAME_CANS_COLORANT_12}", DisplayName = DISPLAY_NAME_COLORANT_12, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_CANS_DISPENSED_12, DisplayName = DISPLAY_NAME_DISPENSED_12, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"C.{COLUMN_NAME_CANS_COLORANT_13}", DisplayName = DISPLAY_NAME_COLORANT_13, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_CANS_DISPENSED_13, DisplayName = DISPLAY_NAME_DISPENSED_13, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"C.{COLUMN_NAME_CANS_COLORANT_14}", DisplayName = DISPLAY_NAME_COLORANT_14, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_CANS_DISPENSED_14, DisplayName = DISPLAY_NAME_DISPENSED_14, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"C.{COLUMN_NAME_CANS_COLORANT_15}", DisplayName = DISPLAY_NAME_COLORANT_15, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_CANS_DISPENSED_15, DisplayName = DISPLAY_NAME_DISPENSED_15, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"C.{COLUMN_NAME_CANS_COLORANT_16}", DisplayName = DISPLAY_NAME_COLORANT_16, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_CANS_DISPENSED_16, DisplayName = DISPLAY_NAME_DISPENSED_16, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"C.{COLUMN_NAME_CANS_COLORANT_17}", DisplayName = DISPLAY_NAME_COLORANT_17, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_CANS_DISPENSED_17, DisplayName = DISPLAY_NAME_DISPENSED_17, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"C.{COLUMN_NAME_CANS_COLORANT_18}", DisplayName = DISPLAY_NAME_COLORANT_18, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_CANS_DISPENSED_18, DisplayName = DISPLAY_NAME_DISPENSED_18, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = $"C.{COLUMN_NAME_CANS_COLORANT_19}", DisplayName = DISPLAY_NAME_COLORANT_19, Visible = false, Width = 95 , alignment = DataGridViewContentAlignment.MiddleRight} },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_CANS_DISPENSED_19, DisplayName = DISPLAY_NAME_DISPENSED_19, Visible = false, Width = 95, alignment = DataGridViewContentAlignment.MiddleRight } },
        };
        private List<GridViewSetting> ViewSettingsWeightDetails = new List<GridViewSetting>()
        {
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = COLUMN_NAME_CANS_WHITE_CODE, DisplayName = DISPLAY_NAME_CODE, Visible = true, Width = 550, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = COLUMN_NAME_CANS_WHITE_WEIGHT, DisplayName = DISPLAY_NAME_WEIGHT, Visible = true, Width = 200, alignment = DataGridViewContentAlignment.MiddleRight } },
        };
        private List<GridViewSetting> ViewSettingsOutWeights = new List<GridViewSetting>()
        {
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.String, ColumnName = "TB0.Code", DisplayName = "コード", Visible = true, Width = 450, alignment = DataGridViewContentAlignment.MiddleLeft } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = "TB0.Dispensed", DisplayName = "吐出[g]", Visible = true, Width = 200, alignment = DataGridViewContentAlignment.MiddleRight } },
            { new GridViewSetting() { ColumnType = GridViewSetting.ColumnModeType.Numeric, ColumnName = "(TB2.Weight - TB0.Dispensed)", DisplayName = "重量[g]", Visible = true, Width = 200, alignment = DataGridViewContentAlignment.MiddleRight } },
        };
        #endregion

        #region メンバ変数
        private string selectProductCode = string.Empty;
        private int selectingTabIndex = 0;
        private Dictionary<string, int> tbColorNameSetting = new Dictionary<string, int>();
        private Color alertBackColorControl;
        #endregion

        #region コンストラクタ
        public FrmMain()
        {
            InitializeComponent();
            InitializeForm();
        }
        #endregion

        #region イベント
        /// <summary>
        /// 画面を開いた時
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMainShown(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.OpenMainForm);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 画面を閉じた後
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMainClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.CloseMainForm);
                CloseedForm();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// ボタン押下時のショートカット動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.F1:
                        // 表示選択－全て
                        RdoPreviewAll.Select();
                        break;
                    case Keys.F2:
                        // 表示選択－今日以前
                        RdoTodayBefore.Select();
                        break;
                    case Keys.F3:
                        // 表示選択－明日以降
                        RdoTomorrowAfter.Select();
                        break;
                    case Keys.F4:
                        // ラベル印刷
                        BtnPrint.PerformClick();
                        break;
                    case Keys.F5:
                        // 作業指示書の印刷
                        BtnPrintInstructions.PerformClick();
                        break;
                    case Keys.F6:
                        // 緊急印刷
                        BtnPrintEmergency.PerformClick();
                        break;
                    case Keys.F7:
                        // 注文開始
                        BtnOrderStart.PerformClick();
                        break;
                    case Keys.F8:
                        // ステータスを戻す
                        BtnStatusResume.PerformClick();
                        break;
                    case Keys.F9:
                        // 担当者を決定
                        BtnDecidePerson.PerformClick();
                        break;
                    case Keys.F10:
                        // 注文を閉じる
                        BtnOrderClose.PerformClick();
                        break;
                    case Keys.F11:
                        // ステータス一括変更
                        BtnBulkChangeStatus.PerformClick();
                        break;
                    case Keys.F12:
                        // 処理No.詳細
                        BtnProcessDetail.PerformClick();
                        break;
                    default:
                        // Altキーを押された状態はアクセラレータキーなので何もしない
                        if (e.Alt)
                        {
                            break;
                        }
                        // KeyCodeを文字列に変換する。
                        // テンキーからの数値入力を想定して"NumPad"は削除する。
                        var keyValue = new KeysConverter().ConvertToString(e.KeyCode).Replace("NumPad", "");
                        // 文字種チェック。
                        // アルファベットは小文字で入力しても、ここでは大文字になる。
                        if (Regex.IsMatch(keyValue, "^[0-9A-Z]+$"))
                        {
                            selectProductCode += keyValue;
                            // 製品コードの桁数に達したら
                            if (selectProductCode.Length == 2)
                            {
                                if (tabMain.SelectedIndex == TAB_INDEX_DETAIL)
                                {
                                    SelectDataGridViewRowByProductCode();
                                }
                                else
                                {
                                    // 「詳細」タブを開く
                                    // タブを開く動作でタイムラグが発生する為、この場合の対象行選択は「詳細」タブのSelectedIndexChangedイベントで行う。
                                    tabMain.SelectedIndex = TAB_INDEX_DETAIL;
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        private void GvBarcodeDataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                GvBarcodeFormatting((DataGridView)sender);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        private void GvOrderDataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                DataGridViewFormatting((DataGridView)sender);
                PutLog(Sentence.Messages.PreviewData);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        private void GvFormulationDataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                DataGridViewFormatting((DataGridView)sender);
                PutLog(Sentence.Messages.PreviewData);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        private void GvOrderNumberDataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                GvOrderNumberFormatting((DataGridView)sender);
                PutLog(Sentence.Messages.PreviewData);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        private void GvDetailDataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            try
            {
                DataGridViewFormatting((DataGridView)sender);
                PutLog(Sentence.Messages.PreviewData);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        private void GvOrderCellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                var dgv = (DataGridView)sender;
                PutLog(Sentence.Messages.SelectRow);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        private void Gv_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridView gv = (DataGridView)sender;
            if (e.RowIndex >= 0)
            {
                gv.Rows[e.RowIndex].Selected = true;
                DataGridViewRow row = gv.SelectedRows[0];
                switch ((Sql.NpMain.Orders.OrderStatus)int.Parse(row.Cells[COLUMN_STATUS].Value.ToString()))
                {
                    case Sql.NpMain.Orders.OrderStatus.WaitingForToning:
                        TsmiDecide.Enabled = true;
                        TsmiOperatorDelete.Enabled = false;
                        TsmiOrderStart.Enabled = false;
                        TsmiInstructionPrint.Enabled = false;
                        TsmiProductLabelPrint.Enabled = false;
                        TsmiColorLabelPrint.Enabled = false;
                        TsmiCopyLabelPrint.Enabled = false;
                        TsmiOrderClose.Enabled = true;
                        break;
                    case Sql.NpMain.Orders.OrderStatus.WaitingForCCMformulation:
                        TsmiDecide.Enabled = false;
                        TsmiOperatorDelete.Enabled = true;
                        TsmiOrderStart.Enabled = false;
                        TsmiInstructionPrint.Enabled = true;
                        TsmiProductLabelPrint.Enabled = false;
                        TsmiColorLabelPrint.Enabled = false;
                        TsmiCopyLabelPrint.Enabled = false;
                        TsmiOrderClose.Enabled = true;
                        break;
                    case Sql.NpMain.Orders.OrderStatus.Ready:
                        TsmiDecide.Enabled = false;
                        TsmiOperatorDelete.Enabled = false;
                        if (Convert.ToBoolean(row.Cells[COLUMN_URGENT].Value))
                        {
                            TsmiOrderStart.Enabled = false;
                            TsmiInstructionPrint.Enabled = true;
                            TsmiProductLabelPrint.Enabled = true;
                        }
                        else
                        {
                            TsmiOrderStart.Enabled = true;
                            TsmiInstructionPrint.Enabled = false;
                            TsmiProductLabelPrint.Enabled = false;
                        }
                        TsmiColorLabelPrint.Enabled = false;
                        TsmiCopyLabelPrint.Enabled = false;
                        TsmiOrderClose.Enabled = true;
                        break;
                    case Sql.NpMain.Orders.OrderStatus.TestCanInProgress:
                        TsmiDecide.Enabled = false;
                        TsmiOperatorDelete.Enabled = false;
                        TsmiOrderStart.Enabled = false;
                        TsmiInstructionPrint.Enabled = false;
                        TsmiProductLabelPrint.Enabled = false;
                        TsmiColorLabelPrint.Enabled = false;
                        TsmiCopyLabelPrint.Enabled = false;
                        TsmiOrderClose.Enabled = false;
                        break;
                    case Sql.NpMain.Orders.OrderStatus.ManufacturingCansInProgress:
                        TsmiDecide.Enabled = false;
                        TsmiOperatorDelete.Enabled = false;
                        TsmiOrderStart.Enabled = false;
                        TsmiInstructionPrint.Enabled = false;
                        TsmiProductLabelPrint.Enabled = true;
                        TsmiColorLabelPrint.Enabled = true;
                        TsmiCopyLabelPrint.Enabled = true;
                        TsmiOrderClose.Enabled = true;
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// ラベル印刷(F4)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrintClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
                tabMain.SelectedIndex = TAB_INDEX_DETAIL;
                var vm = new ViewModels.LabelPrintData();
                FrmLabelPrint frmLabelPrint = new FrmLabelPrint(vm);
                frmLabelPrint.ShowDialog();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 作業指示書の印刷(F5)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrintInstructionsClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
                // Order_idで検索する
                var columnIndex = GetActiveGridViewSetting().FindIndex(x => x.ColumnName == COLUMN_NAME_ORDERS_ORDER_ID);
                var directionsData = new DataTable();
                if (GvOrder.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = GvOrder.SelectedRows[0];
                    int.TryParse(row.Cells[columnIndex].Value.ToString(), out int orderId);
                    using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
                    {
                        var parameters = new List<ParameterItem>()
                        {
                            new ParameterItem("orderId", orderId),
                        };
                        // 選択しているデータ取得
                        directionsData = db.Select(Sql.NpMain.Orders.GetOrderDirectionsDataByOrderId(), parameters);
                    }
                }
                // 注文データを元にビューモデル作成
                var vm = new ViewModels.DirectionsData(directionsData);
                //MessageBox.Show("作業指示書印刷がクリックされました");
                var frm = new Documents.ReportWorkInstruction.Preview(vm);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 緊急印刷(F6)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrintEmergencyClick(object sender, EventArgs e)
        {
            try
            {
                var result = Messages.ShowDialog(Sentence.Messages.BtnPrintEmergencyClick);
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 注文開始(F7)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOrderStartClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
                var orderId = 0;
                // Order_idで検索する
                var columnIndex = GetActiveGridViewSetting().FindIndex(x => x.ColumnName == COLUMN_NAME_ORDERS_ORDER_ID);
                var orderData = new DataTable();
                if (GvOrder.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = GvOrder.SelectedRows[0];
                    int.TryParse(row.Cells[columnIndex].Value.ToString(), out orderId);
                    using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
                    {
                        var parameters = new List<ParameterItem>()
                        {
                            new ParameterItem("orderId", orderId),
                        };
                        // 選択している注文データ取得
                        orderData = db.Select(Sql.NpMain.Orders.GetDetailOrderStartByOrderId(BaseSettings.Facility.Plant), parameters);
                    }
                }
                // 注文データを元にビューモデル作成
                var vm = new ViewModels.OrderStartData(orderData);
                FrmOrderStart frmOrderStart = new FrmOrderStart(vm);
                frmOrderStart.ShowDialog();
                // ダイアログ閉後の再バインド
                DialogCloseBinding();
                // 事前に取得していたOrder_idを元にフォーカス移動
                FocusSelectedRow(orderId);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// ステータスを戻すボタン押下(F8)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStatusResumeClick(object sender, EventArgs e)
        {
            try
            {
                // Order_id取得
                var gdvSelectedOrderId = GetOrderId();
                DialogResult result = Messages.ShowDialog(Sentence.Messages.BtnStatusResumeClicked);
                switch (result)
                {
                    case DialogResult.Yes:
                        var statusColumnIndex = GetActiveGridViewSetting().FindIndex(x => x.ColumnName == COLUMN_NAME_ORDERS_STATUS);
                        var dgv = GvDetail;
                        if (dgv.SelectedRows.Count > 0)
                        {
                            // 選択している行を取得
                            var selectedRow = dgv.SelectedRows[0];
                            int.TryParse(selectedRow.Cells[statusColumnIndex].Value.ToString(), out int status);
                            StatusResumeOrders(gdvSelectedOrderId, status);
                        }
                        DialogCloseBinding();
                        break;
                    case DialogResult.No:
                        break;
                    default:
                        break;
                }
                FocusSelectedRow(gdvSelectedOrderId);
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }

        }
        /// <summary>
        /// 担当者を決定(F9)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDecidePerson_Click(object sender, EventArgs e)
        {
            PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            FrmOperators frmOperators = new FrmOperators();
            frmOperators.ShowDialog();
        }
        /// <summary>
        /// 注文を閉じる(F10)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnOrderCloseClick(object sender, EventArgs e)
        {
            try
            {
                // 注文番号カラムインデックスの取得
                var orderNumberColumnIndex = GetActiveGridViewSetting().FindIndex(x => x.ColumnName == COLUMN_NAME_ORDERS_ORDER_NUMBER);
                // 注文番号取得
                var orderNumber = GetActiveGridViewName().SelectedRows[SELECTED_ROW].Cells[orderNumberColumnIndex].Value.ToString();
                // 注文番号をリスト化
                List<string> orderNumbers = new List<string>();
                orderNumbers.Add(orderNumber);
                //クリック時にCtrlキーが押されているか判別する
                switch (Control.ModifierKeys)
                {
                    //ctrlキーが押されている場合
                    case Keys.Control:
                        FrmOrderClose frmOrderClose = new FrmOrderClose();
                        frmOrderClose.ShowDialog();
                        break;
                    //ctrlキーが押されていない場合
                    default:
                        DialogResult result = Messages.ShowDialog(Sentence.Messages.BtnOrderCloseClicked, orderNumber);
                        switch (result)
                        {
                            case DialogResult.Yes:
                                DeleteOrdersConfirmation(orderNumbers);
                                break;
                            case DialogResult.No:
                                break;
                            default:
                                break;
                        }
                        break;
                }
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
                using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
                {
                    BindDataGridViewAgain(db);
                }
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// ステータス一括変更(F11)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnBulkChangeStatusClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
                var form = new FrmOrderChangeStatusSelectItem();
                form.ShowDialog();
                DialogCloseBinding();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 処理No.詳細(F12)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemHelpFormClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((ToolStripMenuItem)sender).Text);
                var form = new FrmHelp();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// メニューバーのファイルで「閉じる」を選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemCloseFormClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((ToolStripMenuItem)sender).Text);
                Application.Exit();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// メニューバーの管理ツールで「缶タイプ」を選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemCanTypeClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((ToolStripMenuItem)sender).Text);
                var vm = new ViewModels.CanTypeData();
                FrmCanType frmCanType = new FrmCanType(vm);
                frmCanType.ShowDialog();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// メニューバーの管理ツールで「キャップタイプ」を選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemCapTypeClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((ToolStripMenuItem)sender).Text);
                var vm = new ViewModels.CapTypeData();
                FrmCapType frmCapType = new FrmCapType(vm);
                frmCapType.ShowDialog();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// メニューバーの管理ツールで「商品コードマスタ」を選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemProductCodeMasterClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((ToolStripMenuItem)sender).Text);
                var vm = new ViewModels.ProductCodeMasterData();
                FrmProductCodeMaster frmProductCodeMaster = new FrmProductCodeMaster(vm);
                frmProductCodeMaster.ShowDialog();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// メニューバーの管理ツールで「NP商品コードマスタ」を選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemNPProductCodeMasterClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((ToolStripMenuItem)sender).Text);
                var vm = new ViewModels.NPProductCodeMasterData();
                FrmNPProductCodeMaster frmNPProductCodeMaster = new FrmNPProductCodeMaster(vm);
                frmNPProductCodeMaster.ShowDialog();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// メニューバーの管理ツールで「初期設定」を選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemInitialSettingClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((ToolStripMenuItem)sender).Text);
                var vm = new ViewModels.InitialSettingData();
                FrmInitialSetting frmInitialSetting = new FrmInitialSetting(vm);
                frmInitialSetting.ShowDialog();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// メニューバーの管理ツールで「設定」を選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemSettingClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((ToolStripMenuItem)sender).Text);
                var vm = new ViewModels.SettingData();
                FrmSetting frmSetting = new FrmSetting(vm);
                frmSetting.ShowDialog();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// メニューバーの管理ツールで「ラベル」を選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemSelectLabelClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((ToolStripMenuItem)sender).Text);
                FrmSelectLabel frmSelectLabel = new FrmSelectLabel();
                frmSelectLabel.ShowDialog();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// メニューバーの管理ツールで「出庫」を選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemShippingClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((ToolStripMenuItem)sender).Text);
                FrmShipping frmShipping = new FrmShipping();
                frmShipping.ShowDialog();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// メニューバーの管理ツールで「COMポート」を選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemCOMPortClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((ToolStripMenuItem)sender).Text);
                FrmCOMPort frmCOMPort = new FrmCOMPort();
                frmCOMPort.ShowDialog();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// メニューバーの管理ツールで「CCMシミュレーター」を選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemCCMSimulatorClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((ToolStripMenuItem)sender).Text);
                var vm = new ViewModels.CCMSimulatorData();
                int selectedindex = tabMain.SelectedIndex;
                var productCodeColumnIndex = GetActiveGridViewSetting().FindIndex(x => x.ColumnName == COLUMN_NAME_ORDERS_PRODUCT_CODE);
                var productCode2ColumnIndex = ViewSettingsOrderNumbers.FindIndex(x => x.ColumnName == COLUMN_NAME_ORDERS_PRODUCT_CODE);
                string CodeP = string.Empty;
                switch (selectedindex)
                {
                    case TAB_INDEX_ORDER:
                        CodeP = GvOrder.SelectedRows[0].Cells[productCodeColumnIndex].Value.ToString();
                        break;
                    case TAB_INDEX_DETAIL:
                        CodeP = GvDetail.SelectedRows[0].Cells[productCodeColumnIndex].Value.ToString();
                        break;
                    case TAB_INDEX_FORMULATION:
                        CodeP = GvFormulation.SelectedRows[0].Cells[productCodeColumnIndex].Value.ToString();
                        break;
                    case TAB_INDEX_CAN:
                        CodeP = GvOrderNumber.SelectedRows[0].Cells[productCode2ColumnIndex].Value.ToString();
                        break;
                }
                string productCode = CodeP;
                if (productCode.Length == 2)
                {
                    vm.ProductCodeLeft = productCode[0].ToString();     　//CCMシミュレーター画面にて選択している製品コードの1文字目を表示
                    vm.ProductCodeRight = productCode[1].ToString();    　//CCMシミュレーター画面にて選択している製品コードの2文字目を表示
                }
                FrmCCMSimulator frmCCMSimulator = new FrmCCMSimulator(vm);
                frmCCMSimulator.ShowDialog();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// メニューバーの管理ツールで「ラベル(仮)」を選択　　ラベル(仮) 後に削除致します。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItemLabelSelectionClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((ToolStripMenuItem)sender).Text);
                var vm = new ViewModels.LabelTypeData();
                FrmLabelSelection frmLabelSelection = new FrmLabelSelection(vm);
                frmLabelSelection.ShowDialog();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        /// <summary>
        /// ContextMenuの「担当者を決定」を選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiDecide_Click(object sender, EventArgs e)
        {
            try
            {
                //PutLog(Sentence.Messages.ButtonClicked, ((ToolStripMenuItem)sender).Text);
                //FrmOperators frmOperators = new FrmOperators();
                //frmOperators.ShowDialog();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        /// <summary>
        /// ContextMenuの「オペレータ削除」を選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiOperatorDelete_Click(object sender, EventArgs e)
        {
            try
            {
                // 選択している行のOrder_id取得
                var gdvSelectedOrderId = GetOrderId();
                using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.Yes, Log.ApplicationType.OrderManager))
                {
                    DataGridView dgv = new DataGridView();
                    switch (tabMain.SelectedIndex)
                    {
                        case TAB_INDEX_ORDER:
                            dgv = GvOrder;
                            break;
                        case TAB_INDEX_DETAIL:
                            dgv = GvDetail;
                            break;
                        case TAB_INDEX_FORMULATION:
                            dgv = GvFormulation;
                            break;
                        default:
                            break;
                    }
                    var orderIdColumnIndex = GetActiveGridViewSetting().FindIndex(x => x.ColumnName == COLUMN_NAME_ORDERS_ORDER_ID);
                    if (dgv.SelectedRows.Count > 0)
                    {
                        // 行取得のSQLを作成
                        var parameters = new List<ParameterItem>()
                        {
                            new ParameterItem("orderId", gdvSelectedOrderId),
                        };
                        db.DeleteOperator(Sql.NpMain.Orders.DeleteOperator(), parameters);
                        db.Commit();
                    }
                    // 更新データを再バインド
                    BindDataGridViewAgain(db);
                }
                // 事前に取得していたOrder_idを元に行を移動
                FocusSelectedRow(gdvSelectedOrderId);
                PutLog(Sentence.Messages.ButtonClicked, ((ToolStripMenuItem)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        /// <summary>
        /// ContextMenuの「注文開始」を選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiOrderStart_Click(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((ToolStripMenuItem)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        /// <summary>
        /// ContextMenuの「作業指示書の印刷」を選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiInstructionPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((ToolStripMenuItem)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        /// <summary>
        /// ContextMenuの「製品ラベルのプリント」を選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiProductLabelPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((ToolStripMenuItem)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        /// <summary>
        /// ContextMenuの「色名ラベルのプリント」を選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiColorLabelPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((ToolStripMenuItem)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        /// <summary>
        /// ContextMenuの「控え板ラベル印刷」を選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiCopyLabelPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((ToolStripMenuItem)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        /// <summary>
        /// ContextMenuの「注文を閉じる」を選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TsmiOrderClose_Click(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((ToolStripMenuItem)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        /// <summary>
        /// 缶タブの「テスト仕上がり」を選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnTestCan_Click(object sender, EventArgs e)
        {
            try
            {
                // Statusで検索する
                var columnStatusIndex = GetActiveGridViewSetting().FindIndex(x => x.ColumnName == COLUMN_NAME_ORDERS_STATUS);
                // Order_idで検索する
                var columnOrderIdIndex = GetActiveGridViewSetting().FindIndex(x => x.ColumnName == COLUMN_NAME_ORDERS_ORDER_ID);
                if (GvOrder.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = GvOrderNumber.SelectedRows[0];
                    // 選択行のStatus取得
                    int.TryParse(row.Cells[columnStatusIndex].Value.ToString(), out int status);
                    // テスト缶実施中のみ
                    if (status == (int)Sql.NpMain.Orders.OrderStatus.TestCanInProgress)
                    {
                        // 選択行のOrder_id取得
                        int.TryParse(row.Cells[columnOrderIdIndex].Value.ToString(), out int orderId);
                        var orderIdBox = new List<int>();
                        orderIdBox.Add(orderId);
                        OrderTestCanToProduct(orderIdBox);
                    }
                }
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        /// <summary>
        /// 缶タブの「荷札印刷」を選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnPrintTag_Click(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((ToolStripMenuItem)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        /// <summary>
        /// 缶タブの「缶の再製造」を選択
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnRemanufacturedCan_Click(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
                var vm = new ViewModels.RemanufacturedCanData();
                vm.DataSource = GvBarcodeDataCource;
                foreach (DataGridViewRow row in GvBarcode.SelectedRows)
                {
                    vm.SelectedIndex = row.Index;
                }
                FrmRemanufacturedCan frmRemanufacturedCan = new FrmRemanufacturedCan(vm);
                frmRemanufacturedCan.ShowDialog();
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        /// <summary>
        /// 表示選択切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RdoCheckedChanged(object sender, EventArgs e)
        {
            // ラジオボタンのコントロールを2回通るので、checked=tureを判断して表示を切り替え
            bool rdbChecked = ((RadioButton)sender).Checked;
            if (rdbChecked)
            {
                //選択表示(Panel3)のグループ内のチェックされているラジオボタンを取得する
                var rbtCheckInGroup = panel3.Controls.OfType<RadioButton>()
                    .SingleOrDefault(rb => rb.Checked == true);
                var filter = string.Empty;
                switch (rbtCheckInGroup.Name)
                {
                    case "RdoTodayBefore":
                        filter = $"[SS出荷予定日日付型] <= #{DateTime.Today}#";
                        break;
                    case "RdoTomorrowAfter":
                        filter = $"#{DateTime.Today}# < [SS出荷予定日日付型]";
                        break;
                    default:
                        break;
                }
                GvOrderDataSource.DefaultView.RowFilter = filter;
                GvOrderNumberDataSource.DefaultView.RowFilter = filter;
                // GvOrderDataSourceのみで全て機能するので、下記の記述は不要
                //GvDetailDataSource.DefaultView.RowFilter = filter;
                //GvFormulationDataSource.DefaultView.RowFilter = filter;
            }
        }

        /// <summary>
        /// ソート順切り替え
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RdoSort_CheckedChanged(object sender, EventArgs e)
        {
            // ラジオボタンのコントロールを2回通るので、checked=tureを判断して表示を切り替え
            bool rdbChecked = ((RadioButton)sender).Checked;
            if (rdbChecked)
            {
                this.GvOrder.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(this.GvOrderDataBindingComplete);
                //ソート順(Panel2)のグループ内のチェックされているラジオボタンを取得する
                var rbtCheckInGroup = panel2.Controls.OfType<RadioButton>()
                    .SingleOrDefault(rb => rb.Checked == true);
                // フォーカスしている行のOrder_id取得
                var gdvSelectedOrderId = GetOrderId();
                // ソート順切り替え
                var sortCondition = string.Empty;
                switch (rbtCheckInGroup.Name)
                {
                    case "RdoSortKubun":
                        sortCondition = $"{SORT_KUBUN}";
                        break;
                    case "RdoSortRanking":
                        sortCondition = $"{SORT_RANKING}";
                        break;
                    case "RdoOrderPerson":
                        sortCondition = $"{SORT_ORDER_PERSON}";
                        break;
                    default:
                        break;
                }
                GvOrderDataSource.DefaultView.Sort = sortCondition;
                GvOrderNumberDataSource.DefaultView.Sort = sortCondition;
                // GvOrderDataSourceのみで全て機能するので、下記の記述は不要
                //GvDetailDataSource.DefaultView.Sort = sortCondition;
                //GvFormulationDataSource.DefaultView.Sort = sortCondition;
                // 事前に選択していたデータ行へ移動
                FocusSelectedRow(gdvSelectedOrderId);
            }
        }

        /// <summary>
        /// ロットNo.編集画面を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnLotRegisterClick(object sender, EventArgs e)
        {
            try
            {
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
                var vm = new ViewModels.LotRegister();
                vm.Lot = HgTintingDirection.Value;
                var gdvSelectedOrderId = 0;
                if (GvDetail.SelectedRows.Count > 0)
                {
                    // Order_idを取得する
                    gdvSelectedOrderId = GetOrderId();
                    using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
                    {
                        // 行取得のSQLを作成
                        var parameters = new List<ParameterItem>()
                        {
                            new ParameterItem("orderId", gdvSelectedOrderId),
                        };
                        var rec = db.Select(Sql.NpMain.Orders.GetDetailByOrderId(BaseSettings.Facility.Plant), parameters);
                        int.TryParse(rec.Rows[0]["HG_Data_Number"].ToString(), out int dataNumber);
                        vm.DataNumber = dataNumber;
                    }
                }
                FrmLotRegister frmLotRegister = new FrmLotRegister(vm);
                frmLotRegister.ShowDialog();
                // ダイアログ閉後の再バインド
                DialogCloseBinding();
                // 事前に取得していたOrder_idを元にフォーカス移動
                FocusSelectedRow(gdvSelectedOrderId);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 処理No.指定画面を開く
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnProcessDetailClick(object sender, EventArgs e)
        {
            try
            {
                var vm = new ViewModels.SelectDataNumber();
                // Order_idで検索する
                var columnIndex = GetActiveGridViewSetting().FindIndex(x => x.ColumnName == COLUMN_NAME_ORDERS_ORDER_ID);
                if (GvOrder.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = GvOrder.SelectedRows[0];
                    int.TryParse(row.Cells[columnIndex].Value.ToString(), out int orderId);
                    using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
                    {
                        var parameters = new List<ParameterItem>()
                        {
                            new ParameterItem("orderId", orderId),
                        };
                        var rec = db.Select(Sql.NpMain.Orders.GetDetailByOrderId(BaseSettings.Facility.Plant), parameters);
                        int.TryParse(rec.Rows[0]["HG_Data_Number"].ToString(), out int dataNumber);
                        vm.DataNumber = dataNumber;
                    }
                }
                var frmDataNumber = new FrmSelectDataNumber(vm);
                if (frmDataNumber.ShowDialog() == DialogResult.OK)
                {
                    selectProductCode = vm.SelectedProductCode;
                    if (tabMain.SelectedIndex == TAB_INDEX_DETAIL)
                    {
                        SelectDataGridViewRowByProductCode();
                    }
                    else
                    {
                        tabMain.SelectedIndex = TAB_INDEX_DETAIL;
                    }
                }
                PutLog(Sentence.Messages.ButtonClicked, ((Button)sender).Text);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        /// <summary>
        /// <(&J)　色名セパレータを一つ後ろに移動する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSeparaterBack_Click(object sender, EventArgs e)
        {
            int ColorNamelength = ColorName.SetString(0);
            //tbColorNameSettingに既に変更を保存したプロダクトコードが存在する場合は値を更新する
            //存在しない場合は新規で変更を保存する
            if (tbColorNameSetting.ContainsKey(ProductCode.Value))
            {
                tbColorNameSetting[ProductCode.Value] = ColorNamelength;
            }
            else
            {
                tbColorNameSetting.Add(ProductCode.Value, ColorNamelength);
            }
        }

        /// <summary>
        /// >(&K) 色名セパレータを一つ前に移動する
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSeperateForward_Click(object sender, EventArgs e)
        {
            int ColorNamelength = ColorName.SetString(1);
            //tbColorNameSettingに既に変更を保存したプロダクトコードが存在する場合は値を更新する
            //存在しない場合は新規で変更を保存する
            if (tbColorNameSetting.ContainsKey(ProductCode.Value))
            {
                tbColorNameSetting[ProductCode.Value] = ColorNamelength;
            }
            else
            {
                tbColorNameSetting.Add(ProductCode.Value, ColorNamelength);
            }
        }

        /// <summary>
        /// タブ選択変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 一覧にデータがない場合は処理をしない
            if (GvOrder.CurrentRow != null)
            {
                SelectDataGridViewRowByProductCode();
                int gdvSelectedIndex = 0;
                //変更前に選択していたタブを取得
                switch (selectingTabIndex)
                {
                    case TAB_INDEX_ORDER:
                        gdvSelectedIndex = GvOrder.SelectedRows[SELECTED_ROW].Index;
                        break;
                    case TAB_INDEX_DETAIL:
                        gdvSelectedIndex = GvDetail.SelectedRows[SELECTED_ROW].Index;
                        break;
                    case TAB_INDEX_FORMULATION:
                        gdvSelectedIndex = GvFormulation.SelectedRows[SELECTED_ROW].Index;
                        break;
                    case TAB_INDEX_CAN:
                        gdvSelectedIndex = GvOrderNumber.SelectedRows[SELECTED_ROW].Index;
                        break;
                    default:
                        gdvSelectedIndex = GvOrder.SelectedRows[SELECTED_ROW].Index;
                        break;
                }
                //現在選択中のタブを取得する
                switch (tabMain.SelectedIndex)
                {
                    case TAB_INDEX_ORDER:
                        GvOrder.CurrentCell = GvOrder.Rows[gdvSelectedIndex].Cells[COLUMN_DELIVERY_CODE];
                        DataGridViewFormatting(GvOrder);
                        break;
                    case TAB_INDEX_DETAIL:
                        GvDetail.CurrentCell = GvDetail.Rows[gdvSelectedIndex].Cells[COLUMN_DELIVERY_CODE];
                        DataGridViewFormatting(GvDetail);
                        // ステータスを戻すボタンの活性/非活性
                        switch (Funcs.StrToInt(GvDetail.SelectedRows[SELECTED_ROW].Cells[COLUMN_STATUS].Value.ToString()))
                        {
                            case (int)Sql.NpMain.Orders.OrderStatus.Ready:
                            case (int)Sql.NpMain.Orders.OrderStatus.TestCanInProgress:
                            case (int)Sql.NpMain.Orders.OrderStatus.ManufacturingCansInProgress:
                                BtnStatusResume.Enabled = true;
                                break;
                            case (int)Sql.NpMain.Orders.OrderStatus.WaitingForToning:
                            case (int)Sql.NpMain.Orders.OrderStatus.WaitingForCCMformulation:
                                BtnStatusResume.Enabled = false;
                                break;
                            default:
                                BtnStatusResume.Enabled = false;
                                break;
                        }
                        break;
                    case TAB_INDEX_FORMULATION:
                        GvFormulation.CurrentCell = GvFormulation.Rows[gdvSelectedIndex].Cells[COLUMN_DELIVERY_CODE];
                        DataGridViewFormatting(GvFormulation);
                        BtnStatusResume.Enabled = false;
                        break;
                    case TAB_INDEX_CAN:
                        GvOrderNumber.CurrentCell = GvOrderNumber.Rows[gdvSelectedIndex].Cells[COLUMN_DELIVERY_CODE];
                        GvOrderNumberFormatting(GvOrderNumber);
                        BtnStatusResume.Enabled = false;
                        break;
                    default:
                        GvOrder.CurrentCell = GvOrder.Rows[gdvSelectedIndex].Cells[COLUMN_DELIVERY_CODE];
                        DataGridViewFormatting(GvOrder);
                        BtnStatusResume.Enabled = false;
                        break;
                }
            }
            //現在選択中のタブ(変更後)のタブを保存する
            selectingTabIndex = tabMain.SelectedIndex;
        }
        /// <summary>
        /// 注文タブ内一覧の選択行変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GvOrder_SelectionChanged(object sender, EventArgs e)
        {
            // 注文タブを開いていない時はスルー
            if (tabMain.SelectedIndex != TAB_INDEX_ORDER)
            {
                return;
            }
            try
            {
                // Statusを取得する
                var statusColumnIndex = GetActiveGridViewSetting().FindIndex(x => x.ColumnName == COLUMN_NAME_ORDERS_STATUS);
                // Urgentを取得する
                var urgentColumnIndex = GetActiveGridViewSetting().FindIndex(x => x.ColumnName == COLUMN_NAME_ORDERS_URGENT);
                var dgv = (DataGridView)sender;
                if (dgv.SelectedRows.Count > 0)
                {
                    // 選択している行を取得
                    var selectedRow = dgv.SelectedRows[0];
                    int.TryParse(selectedRow.Cells[statusColumnIndex].Value.ToString(), out int status);
                    var urgentBool = Convert.ToBoolean(selectedRow.Cells[urgentColumnIndex].Value);
                    var statusEnable = false;
                    //各種ボタンの表示制御
                    ButtonsEnableSetting(status, urgentBool, statusEnable);
                }
                PutLog(Sentence.Messages.SelectRow);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        /// <summary>
        /// 詳細タブ内一覧の選択行変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GvDetail_SelectionChanged(object sender, EventArgs e)
        {
            // 詳細タブを開いていない時はスルー
            if (tabMain.SelectedIndex != TAB_INDEX_DETAIL)
            {
                return;
            }
            try
            {
                // Statusを取得する
                var statusColumnIndex = GetActiveGridViewSetting().FindIndex(x => x.ColumnName == COLUMN_NAME_ORDERS_STATUS);
                // Order_idで検索する
                var orderIdColumnIndex = GetActiveGridViewSetting().FindIndex(x => x.ColumnName == COLUMN_NAME_ORDERS_ORDER_ID);
                // Urgentを取得する
                var urgentColumnIndex = GetActiveGridViewSetting().FindIndex(x => x.ColumnName == COLUMN_NAME_ORDERS_URGENT);
                var dgv = (DataGridView)sender;
                if (dgv.SelectedRows.Count > 0)
                {
                    // 選択している行を取得
                    var selectedRow = dgv.SelectedRows[0];
                    int.TryParse(selectedRow.Cells[statusColumnIndex].Value.ToString(), out int status);
                    int.TryParse(selectedRow.Cells[orderIdColumnIndex].Value.ToString(), out int orderId);
                    var urgentBool = Convert.ToBoolean(selectedRow.Cells[urgentColumnIndex].Value);

                    using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
                    {
                        // 行取得のSQLを作成
                        var parameters = new List<ParameterItem>()
                        {
                            new ParameterItem("orderId", orderId),
                        };
                        var rec = db.Select(Sql.NpMain.Orders.GetDetailByOrderId(BaseSettings.Facility.Plant), parameters);
                        // フォームで定義された、取得値設定先のコントロールを抽出する
                        db.ToLabelTextBox(this.Controls, rec.Rows);
                        //コントロールに設定した値にセパレータを設定する
                        ColorName.ValueChanged();
                        //改行場所を変更済みの色名があれば適用する
                        if (tbColorNameSetting.ContainsKey(ProductCode.Value))
                        {
                            ColorName.Lbl1Value = ColorName.Value.Trim().Substring(0, tbColorNameSetting[ProductCode.Value]);
                            ColorName.Lbl2Value = ColorName.Value.Trim().Substring(tbColorNameSetting[ProductCode.Value]);
                        }
                        else
                        {
                            ; //何もしない
                        }
                        //LabelTextSeperateコントロールのLabelへ文字数を設定する
                        ColorName.WordCount = $"({ColorName.Lbl1Value.Length}/{ColorName.Lbl2Value.Length})";

                        //指定LotのTextBoxコントロールの入力値を有無を調べる
                        if (string.IsNullOrEmpty(HgTintingDirection.Value))
                        {
                            BorderHgTintingDirection.Visible = false;
                        }
                        else
                        {
                            BorderHgTintingDirection.Visible = true;
                        }
                        //塗板添付枚数のLabelTextBoxコントロールに設定された値を取得する
                        bool result = int.TryParse(HgSamplePlates.Value, out int intResult);
                        if (result)
                        {
                            BorderBtnPrint.Visible = intResult != 0;
                            BorderHgSamplePlates.Visible = intResult != 0;
                        }
                        else
                        {
                            BorderHgSamplePlates.Visible = false;
                            BorderBtnPrint.Visible = false;
                        }
                        //調色適用のLabelTextBoxコントロールに設定された値を取得する
                        var items = BaseSettings.Display.HgNoteStrList;
                        foreach (string item in items)
                        {
                            if (HgNote.Value.Contains(item))
                            {
                                BorderHgNote.Visible = true;
                                break;
                            }
                            else
                            {
                                BorderHgNote.Visible = false;
                            }
                        }
                        //容量コードのLabelTextBoxコントロールに設定された値を取得してProductNo_Masterに存在するか確認する
                        string productNo = HgProductNo.Value.Trim();
                        // 行取得のSQLを作成
                        parameters = new List<ParameterItem>()
                        {
                            new ParameterItem("ProductNo", productNo),
                        };
                        rec = db.Select(Sql.NpMain.ProductNoMaster.GetCountByProductNo(), parameters);
                        result = int.TryParse(rec.Rows[0]["Product_No_Count"].ToString(), out intResult);
                        if (result)
                        {
                            BorderHgVolumeCode.Visible = 0 < intResult;
                        }
                        else
                        {
                            BorderHgVolumeCode.Visible = false;
                        }
                        //各種ボタンの表示制御
                        var statusEnable = true;
                        ButtonsEnableSetting(status, urgentBool, statusEnable);
                    }
                }
                PutLog(Sentence.Messages.SelectRow);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        /// <summary>
        /// 配合タブ内一覧の選択行変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GvFormulation_SelectionChanged(object sender, EventArgs e)
        {
            // 配合タブを開いていない時はスルー
            if (tabMain.SelectedIndex != TAB_INDEX_FORMULATION)
            {
                return;
            }
            try
            {
                // Statusと取得する
                var statusColumnIndex = GetActiveGridViewSetting().FindIndex(x => x.ColumnName == COLUMN_NAME_ORDERS_STATUS);
                // Order_idで検索する
                var orderIdColumnIndex = GetActiveGridViewSetting().FindIndex(x => x.ColumnName == COLUMN_NAME_ORDERS_ORDER_ID);
                // Urgentを取得する
                var urgentColumnIndex = GetActiveGridViewSetting().FindIndex(x => x.ColumnName == COLUMN_NAME_ORDERS_URGENT);
                var dgv = (DataGridView)sender;
                if (dgv.SelectedRows.Count > 0)
                {
                    // 選択している行を取得
                    var selectedRow = dgv.SelectedRows[0];
                    int.TryParse(selectedRow.Cells[statusColumnIndex].Value.ToString(), out int status);
                    int.TryParse(selectedRow.Cells[orderIdColumnIndex].Value.ToString(), out int orderId);
                    var urgentBool = Convert.ToBoolean(selectedRow.Cells[urgentColumnIndex].Value);

                    using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
                    {
                        // 行取得のSQLを作成
                        var parameters = new List<ParameterItem>()
                        {
                            new ParameterItem("orderId", orderId),
                        };
                        var rec = db.Select(Sql.NpMain.Orders.GetDetailByOrderId(BaseSettings.Facility.Plant), parameters);
                        // フォームで定義された、取得値設定先のコントロールを抽出する
                        db.ToLabelTextBox(this.Controls, rec.Rows);
                        //重量グリッドビューの設定
                        GvWeight.Rows.Clear();
                        var cnt = 1;
                        foreach (DataColumn column in rec.Columns)
                        {
                            if (column.ColumnName.Equals("White_Code"))
                            {
                                double.TryParse(rec.Rows[0]["White_Weight"].ToString(), out double whiteWeight);
                                GvWeight.Rows.Add(rec.Rows[0]["White_Code"], whiteWeight.ToString(Decimal_Place3));
                            }
                            if (column.ColumnName.Equals($"Colorant_{cnt}"))
                            {
                                double.TryParse(rec.Rows[0][$"Weight_{cnt}"].ToString(), out double weight);
                                GvWeight.Rows.Add(rec.Rows[0][$"Colorant_{cnt}"], weight.ToString(Decimal_Place3));
                                cnt++;
                            }
                        }
                        ////各種ボタンの表示制御
                        var statusEnable = false;
                        ButtonsEnableSetting(status, urgentBool, statusEnable);
                    }
                }
                PutLog(Sentence.Messages.SelectRow);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        /// <summary>
        /// 缶タブ内一覧の選択行変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GvOrderNumber_SelectionChanged(object sender, EventArgs e)
        {
            // 缶タブを開いていない時はスルー
            if (tabMain.SelectedIndex != TAB_INDEX_CAN)
            {
                return;
            }
            try
            {
                // Statusと取得する
                var statusColumnIndex = ViewSettingsOrderNumbers.FindIndex(x => x.ColumnName == COLUMN_NAME_ORDERS_STATUS);
                // Order_idで検索する
                var orderIdColumnIndex = ViewSettingsOrderNumbers.FindIndex(x => x.ColumnName == COLUMN_NAME_ORDERS_ORDER_ID);
                // Urgentを取得する
                var urgentColumnIndex = ViewSettingsOrderNumbers.FindIndex(x => x.ColumnName == COLUMN_NAME_ORDERS_URGENT);
                var dgv = (DataGridView)sender;
                if (dgv.SelectedRows.Count > 0)
                {
                    // 選択している行を取得
                    var selectedRow = dgv.SelectedRows[0];
                    int.TryParse(selectedRow.Cells[statusColumnIndex].Value.ToString(), out int status);
                    int.TryParse(selectedRow.Cells[orderIdColumnIndex].Value.ToString(), out int orderId);
                    var urgentBool = Convert.ToBoolean(selectedRow.Cells[urgentColumnIndex].Value);

                    using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
                    {
                        // 行取得のSQLを作成
                        var parameters = new List<ParameterItem>()
                        {
                            new ParameterItem("orderId", orderId),
                        };
                        var rec = db.Select(Sql.NpMain.Orders.GetDetailByOrderId(BaseSettings.Facility.Plant), parameters);
                        // フォームで定義された、取得値設定先のコントロールを抽出する
                        db.ToLabelTextBox(this.Controls, rec.Rows);
                        //Order_NumberでCansテーブルの一覧を表示する
                        // Can一覧取得のSQLを作成
                        GvBarcodeDataCource = db.Select(Sql.NpMain.Cans.GetPreviewByOrderId(ViewSettingsBarcodes, BaseSettings.Facility.Plant), parameters);
                        GvBarcode.DataSource = GvBarcodeDataCource;
                        var cnt = 0;
                        foreach (var item in ViewSettingsBarcodes)
                        {
                            var column = new DataGridViewColumn();

                            GvBarcode.Columns[cnt].Width = item.Width;
                            GvBarcode.Columns[cnt].Visible = item.Visible;
                            GvBarcode.Columns[cnt].DefaultCellStyle.Alignment = item.alignment;
                            cnt++;
                        }
                        ////各種ボタンの表示制御
                        var statusEnable = false;
                        ButtonsEnableSetting(status, urgentBool, statusEnable);
                    }
                }
                PutLog(Sentence.Messages.SelectRow);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }

        /// <summary>
        /// 缶タブ内バーコード一覧の選択行変更イベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GvBarcode_SelectionChanged(object sender, EventArgs e)
        {
            // 缶タブを開いていない時はスルー
            if (tabMain.SelectedIndex != TAB_INDEX_CAN)
            {
                return;
            }
            try
            {
                // Order_idで検索する
                var barcodeColumnIndex = ViewSettingsBarcodes.FindIndex(x => x.ColumnName == "Barcode");
                var orderIdColumnIndex = ViewSettingsBarcodes.FindIndex(x => x.ColumnName == "C.Order_id");
                var dgv = (DataGridView)sender;
                if (dgv.SelectedRows.Count > 0)
                {
                    // 選択している行を取得
                    var selectedRow = dgv.SelectedRows[0];
                    string barcode = selectedRow.Cells[barcodeColumnIndex].Value.ToString().Trim();
                    int.TryParse(selectedRow.Cells[orderIdColumnIndex].Value.ToString(), out int orderId);
                    using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
                    {
                        // 行取得のSQLを作成
                        var parameters = new List<ParameterItem>()
                        {
                            new ParameterItem("barcode", barcode),
                        };
                        var rec = db.Select(Sql.NpMain.Cans.GetDetailByBarcode(), parameters);
                        var list = new List<Control>();
                        list.Add(Barcode);
                        list.Add(TargetWeight);
                        list.Add(OutWeight);
                        list.Add(CansFormulaRelease);
                        // フォームで定義された、取得値設定先のコントロールを抽出する
                        db.ToLabelTextBoxBarcode(list, rec.Rows);
                        //GvWeightDetailグリッドビューを表示する
                        GvWeightDetail.Rows.Clear();
                        int GvWeightDetailCurrentIndex = GvOrderNumber.CurrentRow.Index;
                        var cnt = 1;
                        foreach (DataColumn column in GvOrderNumberDataSource.Columns)
                        {
                            if (column.ColumnName.Equals("白コード"))
                            {
                                double.TryParse(GvOrderNumberDataSource.Rows[GvWeightDetailCurrentIndex]["白重量"].ToString(), out double whiteWeight);
                                GvWeightDetail.Rows.Add(GvOrderNumberDataSource.Rows[GvWeightDetailCurrentIndex]["白コード"], whiteWeight.ToString(Decimal_Place3));
                            }
                            if (column.ColumnName.Equals($"着色剤{cnt}"))
                            {
                                double.TryParse(GvOrderNumberDataSource.Rows[GvWeightDetailCurrentIndex][$"重量{cnt}"].ToString(), out double weight);
                                GvWeightDetail.Rows.Add(GvOrderNumberDataSource.Rows[GvWeightDetailCurrentIndex][$"着色剤{cnt}"], weight.ToString(Decimal_Place3));
                                cnt++;
                            }
                        }
                        //GvOutWeightグリッドビューを表示する
                        GvOutWeight.Rows.Clear();
                        int GvOutWeightCurrentIndex = GvBarcode.CurrentRow.Index;
                        cnt = 1;
                        foreach (DataColumn column in GvBarcodeDataCource.Columns)
                        {
                            if (column.ColumnName.Equals("白コード"))
                            {
                                double.TryParse(GvBarcodeDataCource.Rows[GvOutWeightCurrentIndex]["白吐出"].ToString(), out double canWhiteWeight);
                                double.TryParse(GvOrderNumberDataSource.Rows[GvWeightDetailCurrentIndex]["白重量"].ToString(), out double orderWhiteWeight);
                                GvOutWeight.Rows.Add(GvBarcodeDataCource.Rows[0]["白コード"], canWhiteWeight.ToString(Decimal_Place3), (orderWhiteWeight - canWhiteWeight).ToString(Decimal_Place3));
                            }
                            if (column.ColumnName.Equals($"吐出{cnt}"))
                            {
                                double.TryParse(GvBarcodeDataCource.Rows[GvOutWeightCurrentIndex][$"吐出{cnt}"].ToString(), out double canWeight);
                                double.TryParse(GvOrderNumberDataSource.Rows[GvWeightDetailCurrentIndex][$"重量{cnt}"].ToString(), out double orderWeight);
                                GvOutWeight.Rows.Add(GvBarcodeDataCource.Rows[0][$"着色剤{cnt}"], canWeight.ToString(Decimal_Place3), (orderWeight - canWeight).ToString(Decimal_Place3));
                                cnt++;
                            }
                        }
                    }
                }
                PutLog(Sentence.Messages.SelectRow);
            }
            catch (Exception ex)
            {
                PutLog(ex);
            }
        }
        #endregion

        #region private functions

        #region 画面の初期化
        /// <summary>
        /// 画面の初期化
        /// </summary>
        private void InitializeForm()
        {
            // イベントの追加
            this.Shown += new System.EventHandler(this.FrmMainShown);
            this.FormClosed += new FormClosedEventHandler(this.FrmMainClosed);
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(this.FormKeyDown);
            AddEvent();
            this.GvOrderNumber.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(this.GvOrderNumberDataBindingComplete);
            this.GvBarcode.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(this.GvBarcodeDataBindingComplete);
            this.GvOrder.CellClick += new DataGridViewCellEventHandler(this.GvOrderCellClick);
            this.BtnPrint.Click += new EventHandler(this.BtnPrintClick);
            this.BtnPrintInstructions.Click += new EventHandler(this.BtnPrintInstructionsClick);
            this.BtnPrintEmergency.Click += new EventHandler(this.BtnPrintEmergencyClick);
            this.BtnOrderStart.Click += new EventHandler(this.BtnOrderStartClick);
            this.BtnStatusResume.Click += new System.EventHandler(this.BtnStatusResumeClick);
            this.BtnDecidePerson.Click += new System.EventHandler(this.BtnDecidePerson_Click);
            this.BtnOrderClose.Click += new EventHandler(this.BtnOrderCloseClick);
            this.BtnBulkChangeStatus.Click += new EventHandler(this.BtnBulkChangeStatusClick);
            this.BtnProcessDetail.Click += new EventHandler(this.BtnProcessDetailClick);
            this.BtnRemanufacturedCan.Click += new System.EventHandler(this.BtnRemanufacturedCan_Click);
            this.BtnPrintTag.Click += new System.EventHandler(this.BtnPrintTag_Click);
            this.BtnTestCan.Click += new System.EventHandler(this.BtnTestCan_Click);
            this.RdoPreviewAll.CheckedChanged += new System.EventHandler(this.RdoCheckedChanged);
            this.RdoTodayBefore.CheckedChanged += new System.EventHandler(this.RdoCheckedChanged);
            this.RdoTomorrowAfter.CheckedChanged += new System.EventHandler(this.RdoCheckedChanged);
            this.RdoSortKubun.CheckedChanged += new System.EventHandler(this.RdoSort_CheckedChanged);
            this.RdoSortRanking.CheckedChanged += new System.EventHandler(this.RdoSort_CheckedChanged);
            this.RdoOrderPerson.CheckedChanged += new System.EventHandler(this.RdoSort_CheckedChanged);
            this.ToolStripMenuItemCloseForm.Click += new EventHandler(this.ToolStripMenuItemCloseFormClick);
            this.ToolStripMenuItemCanType.Click += new EventHandler(this.ToolStripMenuItemCanTypeClick);
            this.ToolStripMenuItemCapType.Click += new EventHandler(this.ToolStripMenuItemCapTypeClick);
            this.ToolStripMenuItemProductCodeMaster.Click += new EventHandler(this.ToolStripMenuItemProductCodeMasterClick);
            this.ToolStripMenuItemNPProductCodeMaster.Click += new EventHandler(this.ToolStripMenuItemNPProductCodeMasterClick);
            this.ToolStripMenuItemInitialSetting.Click += new EventHandler(this.ToolStripMenuItemInitialSettingClick);
            this.ToolStripMenuItemSetting.Click += new EventHandler(this.ToolStripMenuItemSettingClick);
            this.ToolStripMenuItemSelectLabel.Click += new EventHandler(this.ToolStripMenuItemSelectLabelClick);
            this.ToolStripMenuItemShipping.Click += new EventHandler(this.ToolStripMenuItemShippingClick);
            this.ToolStripMenuItemCOMPort.Click += new EventHandler(this.ToolStripMenuItemCOMPortClick);
            this.ToolStripMenuItemCCMSimulator.Click += new EventHandler(this.ToolStripMenuItemCCMSimulatorClick);
            this.TsmiDecide.Click += new System.EventHandler(this.TsmiDecide_Click);
            this.TsmiOperatorDelete.Click += new System.EventHandler(this.TsmiOperatorDelete_Click);
            this.TsmiOrderStart.Click += new System.EventHandler(this.TsmiOrderStart_Click);
            this.TsmiInstructionPrint.Click += new System.EventHandler(this.TsmiInstructionPrint_Click);
            this.TsmiProductLabelPrint.Click += new System.EventHandler(this.TsmiProductLabelPrint_Click);
            this.TsmiColorLabelPrint.Click += new System.EventHandler(this.TsmiColorLabelPrint_Click);
            this.TsmiCopyLabelPrint.Click += new System.EventHandler(this.TsmiCopyLabelPrint_Click);
            this.TsmiOrderClose.Click += new System.EventHandler(this.TsmiOrderClose_Click);
            this.BtnSeperateForward.Click += new System.EventHandler(this.BtnSeperateForward_Click);
            this.BtnSeparaterBack.Click += new System.EventHandler(this.BtnSeparaterBack_Click);
            this.GvOrder.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Gv_CellMouseUp);
            this.GvDetail.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Gv_CellMouseUp);
            this.GvFormulation.CellMouseUp += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.Gv_CellMouseUp);
            this.ヘルプHToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemHelpFormClick);
            this.TmrPnlColorExplanationBlinking.Tick += new System.EventHandler(this.TmrPnlColorExplanationBlinkingTick);
            //ラベル(仮)のイベントハンドラー
            this.ToolStripMenuItemLabelSelection.Click += new EventHandler(this.ToolStripMenuItemLabelSelectionClick);
            this.BtnLotRegister.Click += new EventHandler(this.BtnLotRegisterClick);
            this.tabMain.SelectedIndexChanged += new EventHandler(this.tabMain_SelectedIndexChanged);
            this.GvOrder.SelectionChanged += new EventHandler(this.GvOrder_SelectionChanged);
            this.GvDetail.SelectionChanged += new EventHandler(this.GvDetail_SelectionChanged);
            this.GvFormulation.SelectionChanged += new EventHandler(this.GvFormulation_SelectionChanged);
            this.GvOrderNumber.SelectionChanged += new EventHandler(this.GvOrderNumber_SelectionChanged);
            this.GvBarcode.SelectionChanged += new EventHandler(this.GvBarcode_SelectionChanged);
            // 背景色の定義
            lblStatus1.BackColor = StatusBackColorList[(int)Sql.NpMain.Orders.OrderStatus.WaitingForToning];
            lblStatus2.BackColor = StatusBackColorList[(int)Sql.NpMain.Orders.OrderStatus.WaitingForCCMformulation];
            lblStatus3.BackColor = StatusBackColorList[(int)Sql.NpMain.Orders.OrderStatus.Ready];
            lblStatus4.BackColor = StatusBackColorList[(int)Sql.NpMain.Orders.OrderStatus.TestCanInProgress];
            lblStatus5.BackColor = StatusBackColorList[(int)Sql.NpMain.Orders.OrderStatus.ManufacturingCansInProgress];
            lblStatus5.ForeColor = StatusBackColorList[(int)Sql.NpMain.Orders.OrderStatus.ProductionCompleted];
            alertBackColorControl = pnlColorExplanation.BackColor;
            // ラベルの固定文言を設定
            lblStatus1.Text = Messages.GetOrderStatusText(Sql.NpMain.Orders.OrderStatus.WaitingForToning);
            lblStatus2.Text = Messages.GetOrderStatusText(Sql.NpMain.Orders.OrderStatus.WaitingForCCMformulation);
            lblStatus3.Text = Messages.GetOrderStatusText(Sql.NpMain.Orders.OrderStatus.Ready);
            lblStatus4.Text = Messages.GetOrderStatusText(Sql.NpMain.Orders.OrderStatus.TestCanInProgress);
            lblStatus5.Text = Messages.GetOrderStatusText(Sql.NpMain.Orders.OrderStatus.ManufacturingCansInProgress);

            //セパレータの文字数設定
            ColorName.SeparaterLowLimit = COLOR_NAME_LOW_LIMIT;
            ColorName.SeparaterHighLimit = COLOR_NAME_HIGH_LIMIT;

            // DataGridViewの初期設定
            //var ViewSettingsOrderDetails = GridViewSettingCopy(ViewSettingsOrders);
            var ViewSettingsFormulations = GridViewSettingCopy(ViewSettingsOrders);
            var ViewSettingsDetails = GridViewSettingCopy(ViewSettingsOrders);
            InitializeGridView(GvOrder, ViewSettingsOrders);
            GvOrder.AutoGenerateColumns = false;
            InitializeGridView(GvDetail, ViewSettingsDetails);
            GvDetail.AutoGenerateColumns = false;
            InitializeGridView(GvFormulation, ViewSettingsFormulations);
            GvFormulation.AutoGenerateColumns = false;
            InitializeGridView(GvWeight, ViewSettingsWeights);
            InitializeGridView(GvBarcode, ViewSettingsBarcodes);
            InitializeGridView(GvOrderNumber, ViewSettingsOrderNumbers);
            InitializeGridView(GvWeightDetail, ViewSettingsWeightDetails);
            InitializeGridView(GvOutWeight, ViewSettingsOutWeights);
            // DataGridViewの表示
            using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
            {
                GvOrderDataSource = db.Select(Sql.NpMain.Orders.GetPreview(ViewSettingsOrders, BaseSettings.Facility.Plant));
                Funcs.BindDataGridView(GvOrderDataSource, ViewSettingsOrders, GvOrder);
                ColorExplanation(GvOrderDataSource);
                //GvOrder.DataSource = GvOrderDataSource;

                //GvDetailDataSource = db.Select(Sql.NpMain.Orders.GetPreview(ViewSettingsDetails, BaseSettings.Facility.Plant));
                Funcs.BindDataGridView(GvOrderDataSource, ViewSettingsDetails, GvDetail);
                //GvDetail.DataSource = GvDetailDataSource;

                //GvFormulationDataSource = db.Select(Sql.NpMain.Orders.GetPreview(ViewSettingsFormulations, BaseSettings.Facility.Plant));
                Funcs.BindDataGridView(GvOrderDataSource, ViewSettingsFormulations, GvFormulation);
                //GvFormulation.DataSource = GvFormulationDataSource;

                GvOrderNumberDataSource = db.Select(Sql.NpMain.Orders.GetPreview(ViewSettingsOrderNumbers, BaseSettings.Facility.Plant));
                //Funcs.BindDataGridView(GvOrderNumberDataSource, ViewSettingsOrderNumbers, GvOrderNumber);
                GvOrderNumber.DataSource = GvOrderNumberDataSource;
            }
            var cnt = 0;
            foreach (var item in ViewSettingsOrders)
            {
                GvOrder.Columns[cnt].Width = item.Width;
                GvOrder.Columns[cnt].Visible = item.Visible;
                GvOrder.Columns[cnt].DefaultCellStyle.Alignment = item.alignment;
                cnt++;
            }
            cnt = 0;
            foreach (var item in ViewSettingsWeights)
            {
                //GvWeight.Columns[cnt].Width = item.Width;
                //GvWeight.Columns[cnt].Visible = item.Visible;
                //GvWeight.Columns[cnt].DefaultCellStyle.Alignment = item.alignment;
                //GvWeight.Columns[cnt].HeaderText = item.DisplayName;
                //cnt++;
                var column = new DataGridViewColumn();
                column.Name = item.ColumnName;
                column.HeaderText = item.DisplayName;
                column.Width = item.Width;
                column.Visible = item.Visible;
                column.DefaultCellStyle.Alignment = item.alignment;
                column.CellTemplate = new DataGridViewTextBoxCell();
                GvWeight.Columns.Add(column);
                cnt++;

            }
            cnt = 0;
            foreach (var item in ViewSettingsOrderNumbers)
            {
                GvOrderNumber.Columns[cnt].Width = item.Width;
                GvOrderNumber.Columns[cnt].Visible = item.Visible;
                GvOrderNumber.Columns[cnt].DefaultCellStyle.Alignment = item.alignment;
                if (cnt <= COLUMN_STATUS_COLOR)
                {
                    GvOrderNumber.Columns[cnt].HeaderText = string.Empty;
                }
                else
                {
                    GvOrderNumber.Columns[cnt].HeaderText = item.DisplayName;
                }
                cnt++;
                //var column = new DataGridViewColumn();
                //column.Name = item.ColumnName;
                //column.HeaderText = item.DisplayName;
                //column.Width = item.Width;
                //column.Visible = item.Visible;
                //column.DefaultCellStyle.Alignment = item.alignment;
                //GvOrderNumber.Columns.Add(column);
            }
            foreach (var item in ViewSettingsWeightDetails)
            {
                var column = new DataGridViewColumn();
                column.Name = item.ColumnName;
                column.HeaderText = item.DisplayName;
                column.Width = item.Width;
                column.Visible = item.Visible;
                column.DefaultCellStyle.Alignment = item.alignment;
                column.CellTemplate = new DataGridViewTextBoxCell();
                GvWeightDetail.Columns.Add(column);
            }
            foreach (var item in ViewSettingsOutWeights)
            {
                var column = new DataGridViewColumn();
                column.Name = item.ColumnName;
                column.HeaderText = item.DisplayName;
                column.Width = item.Width;
                column.Visible = item.Visible;
                column.DefaultCellStyle.Alignment = item.alignment;
                column.CellTemplate = new DataGridViewTextBoxCell();
                GvOutWeight.Columns.Add(column);
            }
            // データ表示部のコントロール制御
            Funcs.SetControlEnabled(this.Controls, true);
            // ログ出力
            PutLog(Sentence.Messages.InitializedMainForm);
        }
        #endregion

        #region 画面の終了
        /// <summary>
        /// 画面の終了
        /// </summary>
        private void CloseedForm()
        {
            SaveDataGridViewSetting(GvOrder);
            SaveDataGridViewSetting(GvDetail);
            SaveDataGridViewSetting(GvFormulation);
            SaveDataGridViewSetting(GvWeight);
            SaveDataGridViewSetting(GvBarcode);
            SaveDataGridViewSetting(GvOrderNumber);
            SaveDataGridViewSetting(GvWeightDetail);
            SaveDataGridViewSetting(GvOutWeight);
        }
        #endregion

        #region DataGridViewの書式設定
        /// <summary>
        /// DataGridViewの書式設定
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="e"></param>
        private void DataGridViewFormatting(DataGridView dgv)
        {
            // 表示されてる画面でなければスルー
            if (dgv.Name != GetActiveGridViewName().Name)
            {
                return;
            }
            Console.WriteLine(dgv.Name);
            ViewGrid = new List<string>();
            var rowHeight = 48;
            switch (dgv.Name)
            {
                case "GvOrder":
                    rowHeight = GVORDER_ROW_HEIGHT;
                    break;
            }
            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.Height = rowHeight;
                row.Cells[COLUMN_DELIVERY_CODE].Style.BackColor = StatusBackColorList[int.Parse(row.Cells[COLUMN_STATUS].Value.ToString())];
                row.Cells[COLUMN_DELIVERY_CODE].Style.ForeColor = Color.Black;
                row.Cells[COLUMN_PRODUCT_CODE].Style.BackColor = Color.LightYellow;
                row.Cells[COLUMN_PRODUCT_NAME].Style.WrapMode = DataGridViewTriState.True;
                row.Cells[COLUMN_COLOR_SAMPLE].Style.WrapMode = DataGridViewTriState.True;
                row.Cells[COLUMN_PRODUCT_CODE].Style.ForeColor = Color.Black;
                if (Funcs.EmphasisCellConfimation(row, COLUMN_DELIVERY_CODE, COLUMN_VISIBLE_SHIPPING_DATE, COLUMN_VISIBLE_DELIVERY_DATE))
                {
                    row.Cells[COLUMN_SHIPPING_DATE].Style.BackColor = EMPHASIS_CELL_COLOR;
                    row.Cells[COLUMN_DELIVERY_DATE].Style.BackColor = EMPHASIS_CELL_COLOR;
                    row.Cells[COLUMN_OPERATOR].Style.BackColor = EMPHASIS_CELL_COLOR;
                }
            }
        }

        #endregion

        private void GvOrderNumberFormatting(DataGridView dgv)
        {
            if (!dgv.Visible)
            {
                return;
            }
            //if (!ViewGrid.Contains(dgv.Name))
            //{
            //    ViewGrid.Add(dgv.Name);
            //    return;
            //}
            foreach (DataGridViewRow row in dgv.Rows)
            {
                row.Cells[COLUMN_STATUS].Style.BackColor = StatusBackColorList[int.Parse(row.Cells[COLUMN_STATUS].Value.ToString())];
                row.Cells[COLUMN_STATUS_COLOR].Style.BackColor = row.Cells[COLUMN_STATUS].Style.BackColor;
            }
        }

        private void GvBarcodeFormatting(DataGridView dgv)
        {
            if (!dgv.Visible)
            {
                return;
            }
            foreach (DataGridViewRow row in dgv.Rows)
            {

            }
        }
        #region 製品コードでDataGridViewの該当行を探す
        /// <summary>
        /// 製品コードでDataGridViewの該当行を探す
        /// </summary>
        /// <param name="productCode"></param>
        /// <returns></returns>
        private int GetGridViewRowIndexByProductCode(string productCode)
        {
            return GetGridViewRowIndex(productCode, GetActiveGridViewSetting().FindIndex(x => x.ColumnName == COLUMN_NAME_ORDERS_PRODUCT_CODE));
        }
        #endregion

        #region DataGridViewの該当行を探す
        /// <summary>
        /// DataGridViewの該当行を探す
        /// </summary>
        /// <param name="code"></param>
        /// <param name="cellIndex"></param>
        /// <returns></returns>
        private int GetGridViewRowIndex(string code, int cellIndex)
        {
            var rowIndex = -1;
            if (string.IsNullOrEmpty(code))
            {
                return rowIndex;
            }
            if (GvOrder.SelectedRows.Count > 0)
            {
                rowIndex = GvOrder.SelectedRows[0].Index;
            }
            foreach (DataGridViewRow row in GvOrder.Rows)
            {
                if (row.Cells[cellIndex].Value.ToString() == code)
                {
                    rowIndex = row.Index;
                    break;
                }
            }
            return rowIndex;
        }
        #endregion

        #region DataGridViewの選択行を移動する
        /// <summary>
        /// DataGridViewの選択行を移動する
        /// </summary>
        /// <param name="gv"></param>
        /// <param name="rowIndex"></param>
        private void SetGridViewRowIndex(DataGridView gv, int rowIndex)
        {
            gv.ClearSelection();
            gv.Rows[rowIndex].Selected = true;
            gv.FirstDisplayedScrollingRowIndex = rowIndex;
        }
        #endregion

        #region 入力した製品コードを選択状態にする
        /// <summary>
        /// 入力した製品コードを選択状態にする
        /// </summary>
        private void SelectDataGridViewRowByProductCode()
        {
            // 選択行の移動
            var nextRowIndex = GetGridViewRowIndexByProductCode(selectProductCode);
            if (nextRowIndex > -1)
            {
                SetGridViewRowIndex(GvOrder, nextRowIndex);
                SetGridViewRowIndex(GvDetail, nextRowIndex);
                SetGridViewRowIndex(GvFormulation, nextRowIndex);
                SetGridViewRowIndex(GvOrderNumber, nextRowIndex);
            }
            selectProductCode = string.Empty;
        }
        #endregion

        #region "カラーの説明"部の設定
        /// <summary>
        /// "カラーの説明"部の設定
        /// </summary>
        /// <param name="dt"></param>
        private void ColorExplanation(DataTable dt)
        {
            //調色担当待ち
            DataRow[] beforeSS = dt.Select($"[SS出荷予定日日付型] <= #{DateTime.Today}# AND {COLUMN_NAME_ORDERS_STATUS} = {(int)Sql.NpMain.Orders.OrderStatus.WaitingForToning}");
            DataRow[] ss = dt.Select($"{COLUMN_NAME_ORDERS_STATUS} = {(int)Sql.NpMain.Orders.OrderStatus.WaitingForToning}");
            double total = 0;
            string strTotal = string.Empty;
            foreach (DataRow row in ss)
            {
                int.TryParse(row[COLUMN_VOLUME_CODE].ToString().Replace("K", ""), out int weight);
                int.TryParse(row[COLUMN_NUMBER_OF_CANS].ToString(), out int number);
                total += weight * number;
            }
            total = total / 1000;
            total = Math.Round(total, 2, MidpointRounding.AwayFromZero);
            strTotal = total.ToString(Decimal_Place2);
            label6.Text = $"[{beforeSS.Length}/{ss.Length}]";
            label4.Text = $"{strTotal}t";

            //CCM配合待ち
            DataRow[] ccm = dt.Select($"{COLUMN_NAME_ORDERS_STATUS} = {(int)Sql.NpMain.Orders.OrderStatus.WaitingForCCMformulation}");
            foreach (DataRow row in ccm)
            {
                int.TryParse(row[COLUMN_VOLUME_CODE].ToString().Replace("K", ""), out int weight);
                int.TryParse(row[COLUMN_NUMBER_OF_CANS].ToString(), out int number);
                total += weight * number;
            }
            total = total / 1000;
            total = Math.Round(total, 2, MidpointRounding.AwayFromZero);
            strTotal = total.ToString(Decimal_Place2);
            label7.Text = $"[{ccm.Length}]";
            label5.Text = $"{strTotal}t";

            //準備完
            DataRow[] ready = dt.Select($"{COLUMN_NAME_ORDERS_STATUS} = {(int)Sql.NpMain.Orders.OrderStatus.Ready}");
            foreach (DataRow row in ready)
            {
                int.TryParse(row[COLUMN_VOLUME_CODE].ToString().Replace("K", ""), out int weight);
                int.TryParse(row[COLUMN_NUMBER_OF_CANS].ToString(), out int number);
                total += weight * number;
            }
            total = total / 1000;
            total = Math.Round(total, 2, MidpointRounding.AwayFromZero);
            strTotal = total.ToString(Decimal_Place2);
            label8.Text = $"[{ready.Length}]";
            label11.Text = $"{strTotal}t";

            //テスト缶実施中
            DataRow[] testCan = dt.Select($"{COLUMN_NAME_ORDERS_STATUS} = {(int)Sql.NpMain.Orders.OrderStatus.TestCanInProgress}");
            foreach (DataRow row in testCan)
            {
                int.TryParse(row[COLUMN_VOLUME_CODE].ToString().Replace("K", ""), out int weight);
                int.TryParse(row[COLUMN_NUMBER_OF_CANS].ToString(), out int number);
                total += weight * number;
            }
            total = total / 1000;
            total = Math.Round(total, 2, MidpointRounding.AwayFromZero);
            strTotal = total.ToString(Decimal_Place2);
            label9.Text = $"[{testCan.Length}]";
            label12.Text = $"{strTotal}t";

            //製造缶実施中
            DataRow[] productCan = dt.Select($"{COLUMN_NAME_ORDERS_STATUS} = {(int)Sql.NpMain.Orders.OrderStatus.ManufacturingCansInProgress}");
            foreach (DataRow row in productCan)
            {
                int.TryParse(row[COLUMN_VOLUME_CODE].ToString().Replace("K", ""), out int weight);
                int.TryParse(row[COLUMN_NUMBER_OF_CANS].ToString(), out int number);
                total += weight * number;
            }
            total = total / 1000;
            total = Math.Round(total, 2, MidpointRounding.AwayFromZero);
            strTotal = total.ToString(Decimal_Place2);
            label10.Text = $"[{productCan.Length}]";
            label13.Text = $"{strTotal}t";
        }

        #endregion

        #region ボタン表示制御
        private void ButtonsEnableSetting(int status, bool urgent = false, bool statusEnable = false)
        {
            switch ((Sql.NpMain.Orders.OrderStatus)status)
            {
                case Sql.NpMain.Orders.OrderStatus.WaitingForToning:
                    BtnPrint.Enabled = false;
                    BtnPrintInstructions.Enabled = false;
                    BtnPrintEmergency.Enabled = false;
                    BtnOrderStart.Enabled = false;
                    BtnStatusResume.Enabled = false;
                    BtnDecidePerson.Enabled = true;
                    BtnOrderClose.Enabled = true;
                    //BtnProcessDetail.Enabled = false;
                    break;
                case Sql.NpMain.Orders.OrderStatus.WaitingForCCMformulation:
                    BtnPrint.Enabled = false;
                    BtnPrintInstructions.Enabled = true;
                    BtnPrintEmergency.Enabled = true;
                    BtnOrderStart.Enabled = false;
                    BtnStatusResume.Enabled = false;
                    BtnDecidePerson.Enabled = false;
                    BtnOrderClose.Enabled = true;
                    //BtnProcessDetail.Enabled = false;
                    break;
                case Sql.NpMain.Orders.OrderStatus.Ready:
                    BtnPrintEmergency.Enabled = false;
                    if (urgent)
                    {
                        BtnOrderStart.Enabled = false;
                        BtnPrintInstructions.Enabled = true;
                        BtnPrint.Enabled = true;
                    }
                    else
                    {
                        BtnOrderStart.Enabled = true;
                        BtnPrintInstructions.Enabled = false;
                        BtnPrint.Enabled = false;
                    }
                    BtnStatusResume.Enabled = true;
                    BtnDecidePerson.Enabled = false;
                    BtnOrderClose.Enabled = true;
                    //BtnProcessDetail.Enabled = false;
                    break;
                case Sql.NpMain.Orders.OrderStatus.TestCanInProgress:
                    BtnPrint.Enabled = true;
                    BtnPrintInstructions.Enabled = false;
                    BtnPrintEmergency.Enabled = false;
                    BtnOrderStart.Enabled = false;
                    BtnStatusResume.Enabled = true;
                    BtnDecidePerson.Enabled = false;
                    BtnOrderClose.Enabled = true;
                    //BtnProcessDetail.Enabled = false;
                    break;
                case Sql.NpMain.Orders.OrderStatus.ManufacturingCansInProgress:
                    BtnPrint.Enabled = true;
                    BtnPrintInstructions.Enabled = false;
                    BtnPrintEmergency.Enabled = false;
                    BtnOrderStart.Enabled = false;
                    BtnStatusResume.Enabled = true;
                    BtnDecidePerson.Enabled = false;
                    BtnOrderClose.Enabled = true;
                    //BtnProcessDetail.Enabled = false;
                    break;
                default:
                    BtnPrint.Enabled = false;
                    BtnPrintEmergency.Enabled = true;

                    break;
            }
            if (tabMain.SelectedIndex == TAB_INDEX_CAN)
            {
                switch ((Sql.NpMain.Orders.OrderStatus)status)
                {
                    case Sql.NpMain.Orders.OrderStatus.TestCanInProgress:
                        BtnTestCan.Enabled = true;
                        BtnPrintTag.Enabled = true;
                        BtnRemanufacturedCan.Enabled = false;
                        break;
                    case Sql.NpMain.Orders.OrderStatus.ManufacturingCansInProgress:
                        BtnTestCan.Enabled = false;
                        BtnPrintTag.Enabled = false;
                        BtnRemanufacturedCan.Enabled = true;
                        break;
                    default:
                        BtnTestCan.Enabled = false;
                        BtnPrintTag.Enabled = false;
                        BtnRemanufacturedCan.Enabled = false;
                        break;
                }
            }
        }
        #endregion

        #region DataGirdViewの設定をコピーする
        private List<GridViewSetting> GridViewSettingCopy(List<GridViewSetting> source)
        {
            var result = new List<GridViewSetting>();
            foreach (var item in source)
            {
                result.Add(new GridViewSetting(item));
            }
            return result;
        }

        #endregion

        #region ラベル印刷
        /// <summary>
        /// ラベル印刷
        /// ラベルプリンターでラベル出力を行います。
        /// 失敗すると例外エラーとなるので、プリント処理では try～catch にて例外の補足を行ってください。
        /// </summary>
        private void PrintLabel()
        {
            var layoutFileName = string.Empty;
            // 文字列型データ
            var printPutData = string.Empty;
            // 配列型データ
            var printOutDataArray = new string[] { "A", "B" };
            // リスト型データ
            var printOutDataList = new List<string>() { "AA", "BB" };

            using (var printer = new MlController.Ct4Lx(layoutFileName))
            {
                printer.Print(printPutData);
                printer.Print(printOutDataArray);
                printer.Print(printOutDataList);
            }
        }


        #endregion

        #endregion

        private void BtnPrintInstructions_Click(object sender, EventArgs e)
        {

        }

        #region イベントの追加/イベントの削除
        /// <summary>
        /// イベントの追加
        /// </summary>
        private void AddEvent()
        {
            this.GvOrder.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(this.GvOrderDataBindingComplete);
            this.GvDetail.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(this.GvDetailDataBindingComplete);
            this.GvFormulation.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(this.GvFormulationDataBindingComplete);
        }

        /// <summary>
        /// イベントの削除
        /// </summary>
        private void RemoveEvent()
        {
            this.GvOrder.DataBindingComplete -= new DataGridViewBindingCompleteEventHandler(this.GvOrderDataBindingComplete);
            this.GvDetail.DataBindingComplete -= new DataGridViewBindingCompleteEventHandler(this.GvDetailDataBindingComplete);
            this.GvFormulation.DataBindingComplete -= new DataGridViewBindingCompleteEventHandler(this.GvFormulationDataBindingComplete);
        }

        /// <summary>
        /// サーバーステータスの赤点滅
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TmrPnlColorExplanationBlinkingTick(object sender, EventArgs e)
        {
            pnlColorExplanation.BackColor = !pnlColorExplanation.BackColor.Equals(ALERT_BACK_COLOR_RED) ? ALERT_BACK_COLOR_RED : alertBackColorControl;
        }
        #endregion

        #region 表示されている画面のName取得
        /// <summary>
        /// 表示されている画面のName取得
        /// </summary>
        /// <returns></returns>
        private DataGridView GetActiveGridViewName()
        {
            switch (tabMain.SelectedIndex)
            {
                case TAB_INDEX_ORDER:
                    return GvOrder;
                case TAB_INDEX_DETAIL:
                    return GvDetail;
                case TAB_INDEX_FORMULATION:
                    return GvFormulation;
                case TAB_INDEX_CAN:
                    return GvOrderNumber;
                default:
                    return null;
            }
        }
        #endregion

        #region 表示されている画面の列定義取得
        /// <summary>
        /// 表示されている画面の列定義取得
        /// </summary>
        /// <returns></returns>
        private List<GridViewSetting> GetActiveGridViewSetting()
        {
            switch (tabMain.SelectedIndex)
            {
                case TAB_INDEX_ORDER:
                case TAB_INDEX_DETAIL:
                case TAB_INDEX_FORMULATION:
                    return ViewSettingsOrders;
                case TAB_INDEX_CAN:
                    return ViewSettingsOrderNumbers;
                default:
                    return ViewSettingsOrders;
            }
        }
        #endregion

        #region フォーカスしている行のOrder_idを取得
        /// <summary>
        /// フォーカスしている行のOrder_idを取得
        /// </summary>
        /// <returns></returns>
        private int GetOrderId()
        {
            var gdvSelectedOrderId = 0;
            return gdvSelectedOrderId = Funcs.StrToInt(GetActiveGridViewName().SelectedRows[SELECTED_ROW].Cells[COLUMN_ORDER_ID].Value.ToString());
        }
        #endregion

        #region 取得していたOrder_idを元にフォーカスを移動
        /// <summary>
        /// 取得していたOrder_idを元にフォーカスを移動
        /// </summary>
        /// <param name="gdvSelectedOrderId"></param>
        private void FocusSelectedRow(int gdvSelectedOrderId)
        {
            var getGridViewRowIndex = GetGridViewRowIndex(gdvSelectedOrderId.ToString(), COLUMN_ORDER_ID);
            SetGridViewRowIndex(GetActiveGridViewName(), getGridViewRowIndex);
        }
        #endregion

        #region 更新データ再バインド
        /// <summary>
        /// 更新データ再バインド
        /// </summary>
        /// <param name="db"></param>
        private void BindDataGridViewAgain(SqlBase db)
        {
            GvOrderDataSource = db.Select(Sql.NpMain.Orders.GetPreview(ViewSettingsOrders, BaseSettings.Facility.Plant));
            GvOrder.DataSource = GvOrderDataSource;
            GvDetail.DataSource = GvOrderDataSource;
            GvFormulation.DataSource = GvOrderDataSource;
            GvOrderNumberDataSource = db.Select(Sql.NpMain.Orders.GetPreview(ViewSettingsOrderNumbers, BaseSettings.Facility.Plant));
            GvOrderNumber.DataSource = GvOrderNumberDataSource;
        }
        #endregion

        #region データ更新用ダイアログでデータ更新された場合の処理
        /// <summary>
        /// データ更新用ダイアログでデータ更新された場合の処理
        /// </summary>
        private void DialogCloseBinding()
        {
            using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
            {
                BindDataGridViewAgain(db);
            }
        }
        #endregion

        #region 注文を閉じる実施
        /// <summary>
        /// 注文を閉じる実施
        /// </summary>
        /// <param name="orderNumbers"></param>
        public void DeleteOrdersConfirmation(List<string> orderNumbers)
        {
            // メソッド内定数
            const int formulaReleaseColumn = 0;
            // 注文番号用のリスト作成
            List<DataTable> orders = GetCansFormulaReleaseFlg(orderNumbers);
            foreach (var order in orders)
            {
                switch (order.Rows[SELECTED_ROW][COLUMN_NAME_ORDERS_STATUS])
                {
                    case (int)Sql.NpMain.Orders.OrderStatus.WaitingForToning:
                    case (int)Sql.NpMain.Orders.OrderStatus.WaitingForCCMformulation:
                        // ステータスが「調色担当者待ち」・「CCM配合待ち」の場合は、缶の確認なく削除
                        DeleteOrders(Funcs.StrToInt(order.Rows[SELECTED_ROW][COLUMN_NAME_ORDERS_ORDER_ID].ToString()));
                        break;
                    default:
                        // 最終配合チェック用
                        var formulaReleaseCheckNum = order.AsEnumerable().Where(x => int.Parse(x[formulaReleaseColumn].ToString()) == 0).ToList();
                        // 全ての缶に最終配合が吐出されておればそのままDelete
                        if (!formulaReleaseCheckNum.Any())
                        {
                            DeleteOrders(Funcs.StrToInt(order.Rows[SELECTED_ROW][COLUMN_NAME_ORDERS_ORDER_ID].ToString()));
                        }
                        else
                        {
                            // 最終配合が吐出されていない缶があれば再度確認
                            DialogResult result = Messages.ShowDialog(Sentence.Messages.SomeCansHaventBeenDispensedWithLastReleaseCloseOrder);
                            switch (result)
                            {
                                case DialogResult.Yes:
                                    // 完了通知を出すか確認　←　Yes:完了（正常）、　No:製造中止
                                    result = Messages.ShowDialog(Sentence.Messages.NotifyOrderAsCompletelyProduced);
                                    switch (result)
                                    {
                                        case DialogResult.Yes:
                                            DeleteOrders(Funcs.StrToInt(order.Rows[SELECTED_ROW][COLUMN_NAME_ORDERS_ORDER_ID].ToString()));
                                            break;
                                        case DialogResult.No:
                                            var canselFlg = ORDER_CANCEL_FLG;
                                            var status = (int)Sql.NpMain.Orders.OrderStatus.Discontinued;
                                            DeleteOrders(Funcs.StrToInt(order.Rows[SELECTED_ROW][COLUMN_NAME_ORDERS_ORDER_ID].ToString()), status, canselFlg);
                                            break;
                                        default:
                                            break;
                                    }
                                    break;
                                case DialogResult.No:
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                }
            }
            ProductionEndPrint();
        }
        #endregion

        #region Cansの最終配合取得
        /// <summary>
        /// Cansの最終配合取得
        /// </summary>
        /// <param name="orderNumbers"></param>
        /// <returns></returns>
        private List<DataTable> GetCansFormulaReleaseFlg(List<string> orderNumbers)
        {
            var orderCans = new List<DataTable>();
            using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
            {
                foreach (string orderNumber in orderNumbers)
                {
                    var parameters = new List<ParameterItem>()
                    {
                        new ParameterItem("@OrderNumber", orderNumber),
                    };
                    orderCans.Add(db.Select(Sql.NpMain.Cans.GetCansFormulaRelease(BaseSettings.Facility.Plant), parameters));
                }
            }
            return orderCans;
        }
        #endregion

        #region 注文を閉じる実施
        /// <summary>
        /// 注文を閉じる実施
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="status"></param>
        /// <param name="cancelFlg"></param>
        private void DeleteOrders(int orderId, int status = (int)Sql.NpMain.Orders.OrderStatus.ProductionCompleted, int cancelFlg = 0)
        {
            using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.Yes, Log.ApplicationType.OrderManager))
            {
                var parameters = new List<ParameterItem>()
                {
                    new ParameterItem("@OrderId", orderId),
                    new ParameterItem("@Status", status),
                };
                db.Execute(Sql.NpMain.Orders.DeleteOrders(cancelFlg), parameters);
                db.Commit();
            }
        }
        #endregion

        #region プリントの出力確認
        /// <summary>
        /// プリントの出力確認
        /// </summary>
        private void ProductionEndPrint()
        {
            DialogResult result = Messages.ShowDialog(Sentence.Messages.PrintOrderProductionReport);
            switch (result)
            {
                case DialogResult.Yes:
                    break;
                case DialogResult.No:
                    break;
                default:
                    break;
            }
        }
        #endregion

        /// <summary>
        /// ステータス一括変更　実施
        /// </summary>
        /// <param name="gdvSelectedOrderIds"></param>
        /// <param name="status"></param>
        public void StatusResumeOrders(int gdvSelectedOrderIds, int status)
        {
            var dt = new DataTable();
            using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.No, Log.ApplicationType.OrderManager))
            {
                var parameters = new List<ParameterItem>()
                {
                    new ParameterItem("@OrderId", gdvSelectedOrderIds),
                };
                dt = db.Select(Sql.NpMain.Cans.GetBarcodes(BaseSettings.Facility.Plant), parameters);
            }
            var param = new List<SqlParameter>();
            var values = new List<string>();
            string column = "barcode";
            var cnt = 1;
            foreach (DataRow barcode in dt.Rows)
            {
                values.Add("@" + column + cnt.ToString());
                param.Add(new SqlParameter(column + cnt.ToString(), barcode.ItemArray[0].ToString()));
                cnt++;
            }
            string sqlParameter = string.Join(",", values);　
            switch ((Sql.NpMain.Orders.OrderStatus)status)
            {
                case Sql.NpMain.Orders.OrderStatus.WaitingForToning:
                    break;
                case Sql.NpMain.Orders.OrderStatus.WaitingForCCMformulation:
                    break;
                case Sql.NpMain.Orders.OrderStatus.Ready:
                case Sql.NpMain.Orders.OrderStatus.TestCanInProgress:
                case Sql.NpMain.Orders.OrderStatus.ManufacturingCansInProgress:
                    using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.Yes, Log.ApplicationType.OrderManager))
                    {
                        db.StatusResume(Sql.NpMain.Orders.StatusResume(gdvSelectedOrderIds.ToString()));
                        db.Execute(Sql.NpMain.Cans.RemanufacturedCanByBarcode(sqlParameter), param);　　　　　// 缶のステータスを更新
                        db.Commit();
                    }
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// テスト缶実施中を製造缶実施中へ移行
        /// </summary>
        /// <param name="orderId"></param>
        public void OrderTestCanToProduct(List<int> orderIds)
        {
            if (orderIds.Count > 0)　　　　　// チェックが入っていればボタンを押せば移行実施、チェックが入っていないorテスト缶実施中がそもそもない状態でボタンを押せばスルー
            {
                string sqlParameter = string.Join(",", orderIds);
                using (var db = new SqlBase(SqlBase.DatabaseKind.NPMAIN, SqlBase.TransactionUse.Yes, Log.ApplicationType.OrderManager))
                {
                    var parameters = new List<ParameterItem>()
                {
                    new ParameterItem("@status", (int)Sql.NpMain.Orders.OrderStatus.ManufacturingCansInProgress),
                };
                    // 選択している注文データのステータスを変更
                    db.Execute(Sql.NpMain.Orders.StatusProductionChange(sqlParameter), parameters);
                    db.Commit();
                }
            }
            else
            {

            }
        }
    }
}
