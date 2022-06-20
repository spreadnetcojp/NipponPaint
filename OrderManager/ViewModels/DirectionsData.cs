#region using defines
using System.Data;
#endregion

namespace NipponPaint.OrderManager.ViewModels
{
    /// <summary>
    /// ビューモデル
    /// 作業指示書プレビューダイアログ
    /// </summary>
    public class DirectionsData
    {
        /// <summary>
        /// 製造日
        /// </summary>
        public string HG_SS_Shipping_Date { get; set; } /// <summary>
                                                        /// 既読済
                                                        /// </summary>
        public bool Urgent { get; set; } /// <summary>
                                         /// JZ
                                         /// </summary>
        public string Product_Code { get; set; }
        /// <summary>
        /// 伝区
        /// </summary>
        public string HG_Order_Invoice_ID { get; set; }
        /// <summary>
        /// 処理NO
        /// </summary>
        public string HG_Data_Number { get; set; }
        /// <summary>
        /// 発注時ランク
        /// </summary>
        public string HG_Tinting_Price_Rank { get; set; }
        /// <summary>
        /// 製造者
        /// </summary>
        public string Operator_Name { get; set; }
        /// <summary>
        /// 投入日時（月日）
        /// </summary>
        public string HG_Order_Input_Date { get; set; }
        /// <summary>
        /// 投入日時（時間）
        /// </summary>
        public string HG_Order_Input_Time { get; set; }
        /// <summary>
        /// 投入者
        /// </summary>
        public string HG_Sales_in_Charge { get; set; }
        /// <summary>
        /// 商品コード（上）
        /// </summary>
        public string HG_Product_No { get; set; }
        /// <summary>
        /// 商品コード（下）
        /// </summary>
        public string HG_NPC_Article_Number { get; set; }
        /// <summary>
        /// 商品名（品種）
        /// </summary>
        public string HG_Product_NameOnly { get; set; }
        /// <summary>
        /// 商品名（色名）
        /// </summary>
        public string HG_Color_Name { get; set; }
        /// <summary>
        /// 容量
        /// </summary>
        public string HG_Volume_Code { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int HG_Order_Cans { get; set; }
        /// <summary>
        /// 艶区分
        /// </summary>
        public string HG_Gloss_Dictation { get; set; }
        /// <summary>
        /// 調色機能
        /// </summary>
        public string HG_Supplement_Dictation { get; set; }
        /// <summary>
        /// 標準見本
        /// </summary>
        public string HG_Color_Sample { get; set; }
        /// <summary>
        /// 塗板添付
        /// </summary>
        public string HG_Sample_Plates { get; set; }
        /// <summary>
        /// 調色摘要欄
        /// </summary>
        public string HG_Note { get; set; }
        /// <summary>
        /// 摘要欄
        /// </summary>
        public string HG_Comments { get; set; }
        /// <summary>
        /// 見本返却欄
        /// </summary>
        public string HG_Sample_Back { get; set; }
        /// <summary>
        /// 指定ロット
        /// </summary>
        public string HG_Tinting_Direction { get; set; }
        /// <summary>
        /// 営業所名
        /// </summary>
        public string HG_Sales_Branch_Name { get; set; }
        /// <summary>
        /// 得意先コード
        /// </summary>
        public string HG_Customer_Code { get; set; }
        /// <summary>
        /// 得意先名
        /// </summary>
        public string HG_Customer_Name_Kanji { get; set; }
        /// <summary>
        /// SSコード
        /// </summary>
        public string HG_SS_Code { get; set; }
        /// <summary>
        /// 配送業者名
        /// </summary>
        public string HG_Truck_Company_Name { get; set; }
        /// <summary>
        /// HG運区
        /// </summary>
        public string HG_HG_Shipping_ID { get; set; }
        /// <summary>
        /// 納入先名
        /// </summary>
        public string HG_Delivery_Name_Kanji { get; set; }
        /// <summary>
        /// 納入先住所
        /// </summary>
        public string HG_Delivery_Address_Kanji { get; set; }
        /// <summary>
        /// TEL
        /// </summary>
        public string HG_Delivery_TelNo { get; set; }
        /// <summary>
        /// 納期
        /// </summary>
        public string HG_Delivery_Date { get; set; }

        public DirectionsData(DataTable directionsData)
        {
            HG_SS_Shipping_Date = directionsData.Rows[0]["HG_SS_Shipping_Date"].ToString();
            Product_Code = directionsData.Rows[0]["Product_Code"].ToString();
            HG_Order_Invoice_ID = directionsData.Rows[0]["HG_Order_Invoice_ID"].ToString();
            HG_Data_Number = directionsData.Rows[0]["HG_Data_Number"].ToString();
            HG_Tinting_Price_Rank = directionsData.Rows[0]["HG_Tinting_Price_Rank"].ToString();
            Operator_Name = directionsData.Rows[0]["Operator_Name"].ToString();
            HG_Order_Input_Date = directionsData.Rows[0]["HG_Order_Input_Date"].ToString();
            HG_Order_Input_Time = directionsData.Rows[0]["HG_Order_Input_Time"].ToString();
            HG_Sales_in_Charge = directionsData.Rows[0]["HG_Sales_in_Charge"].ToString();
            HG_Product_No = directionsData.Rows[0]["HG_Product_No"].ToString();
            HG_NPC_Article_Number = directionsData.Rows[0]["HG_NPC_Article_Number"].ToString();
            HG_Product_NameOnly = directionsData.Rows[0]["HG_Product_NameOnly"].ToString();
            HG_Color_Name = directionsData.Rows[0]["HG_Color_Name"].ToString();
            HG_Volume_Code = directionsData.Rows[0]["HG_Volume_Code"].ToString();
            HG_Order_Cans = NpCommon.Funcs.StrToInt(directionsData.Rows[0]["HG_Order_Cans"].ToString());
            HG_Gloss_Dictation = directionsData.Rows[0]["HG_Gloss_Dictation"].ToString();
            HG_Supplement_Dictation = directionsData.Rows[0]["HG_Supplement_Dictation"].ToString();
            HG_Color_Sample = directionsData.Rows[0]["HG_Color_Sample"].ToString();
            HG_Sample_Plates = directionsData.Rows[0]["HG_Sample_Plates"].ToString();
            HG_Note = directionsData.Rows[0]["HG_Note"].ToString();
            HG_Comments = directionsData.Rows[0]["HG_Comments"].ToString();
            HG_Sample_Back = directionsData.Rows[0]["HG_Sample_Back"].ToString();
            HG_Tinting_Direction = directionsData.Rows[0]["HG_Tinting_Direction"].ToString();
            HG_Sales_Branch_Name = directionsData.Rows[0]["HG_Sales_Branch_Name"].ToString();
            HG_Customer_Code = directionsData.Rows[0]["HG_Customer_Code"].ToString();
            HG_Customer_Name_Kanji = directionsData.Rows[0]["HG_Customer_Name_Kanji"].ToString();
            HG_SS_Code = directionsData.Rows[0]["HG_SS_Code"].ToString();
            HG_Truck_Company_Name = directionsData.Rows[0]["HG_Truck_Company_Name"].ToString();
            HG_HG_Shipping_ID = directionsData.Rows[0]["HG_HG_Shipping_ID"].ToString();
            HG_Delivery_Name_Kanji = directionsData.Rows[0]["HG_Delivery_Name_Kanji"].ToString();
            HG_Delivery_Address_Kanji = directionsData.Rows[0]["HG_Delivery_Address_Kanji"].ToString();
            HG_Delivery_TelNo = directionsData.Rows[0]["HG_Delivery_TelNo"].ToString();
            HG_Delivery_Date = directionsData.Rows[0]["HG_Delivery_Date"].ToString();
        }
    }
}
