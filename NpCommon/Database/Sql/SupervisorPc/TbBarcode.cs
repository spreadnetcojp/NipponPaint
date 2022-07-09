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
using System.Text;
using System.Threading.Tasks;
using Attr = NipponPaint.NpCommon.Attributes.AttributeUtility;
#endregion

namespace NipponPaint.NpCommon.Database.Sql.SupervisorPc
{
    public class TbBarcode
    {
        #region 定数
        public const string MAIN_TABLE = "TB_BARCODE";

        public const string BARCODE = "BARCODE";
        public const string PROCESS_CODE = "PROCESS_CODE";
        public const string BRC_TIME_INSERTED = "BRC_TIME_INSERTED";
        public const string BRC_TIME_PROCESSED = "BRC_TIME_PROCESSED";
        public const string BRC_STATUS = "BRC_STATUS";
        public const string BRC_ERR_1 = "BRC_ERR_1";
        public const string BRC_ERR_2 = "BRC_ERR_2";
        public const string BRC_ERR_3 = "BRC_ERR_3";

        public static List<MergeItemDefine> Fields = new List<MergeItemDefine>()
        {
            new MergeItemDefine(){ IsKey = true, IsInsert = true, IsUpdate = false, Field = BARCODE },
            new MergeItemDefine(){ IsKey = true, IsInsert = true, IsUpdate = false, Field = PROCESS_CODE },
            new MergeItemDefine(){ IsKey = false, IsInsert = false, IsUpdate = false, Field = BRC_TIME_INSERTED },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = BRC_TIME_PROCESSED },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = BRC_STATUS },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = BRC_ERR_1 },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = BRC_ERR_2 },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = BRC_ERR_3 },
        };

        public const int STATUS_ERP_PROCESSED = 1;
        #endregion

        #region メンバ変数
        private Data.SupervisorPc.TbBarcode _tbBarcode;
        #endregion

        #region プロパティ
        private Type ObjectType { get { return _tbBarcode.GetType(); } }
        #endregion

        #region コンストラクタ
        public TbBarcode()
        {
            _tbBarcode = new Data.SupervisorPc.TbBarcode();
        }

        public TbBarcode(Data.SupervisorPc.TbBarcode tbBarcode)
        {
            _tbBarcode = tbBarcode;
        }
        #endregion

        #region 参照系

        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <returns></returns>
        public static string GetPreview()
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT * FROM( ");
            sql.Append($"SELECT ");
            sql.Append($"  B.{BARCODE} ");
            sql.Append($" ,B.{PROCESS_CODE} ");
            sql.Append($" ,B.{BRC_TIME_INSERTED} ");
            sql.Append($" ,B.{BRC_TIME_PROCESSED} ");
            sql.Append($" ,B.{BRC_STATUS} ");
            sql.Append($" ,B.{BRC_ERR_1} ");
            sql.Append($" ,B.{BRC_ERR_2} ");
            sql.Append($" ,B.{BRC_ERR_3} ");
            sql.Append($" ,ISNULL(J.{TbJob.JOB_STATUS}, 0) AS {TbJob.JOB_STATUS} ");
            sql.Append($"FROM {MAIN_TABLE} AS B ");
            sql.Append($"LEFT JOIN {TbJob.MAIN_TABLE} AS J ON J.{TbJob.JOB_BARCODE} = B.{BARCODE} AND J.{TbJob.JOB_PROCESS_CODE} = B.{PROCESS_CODE} ");
            sql.Append($") AS TB0 ");
            // TB_JOBのJOB_STATUSが1(缶は良好にラインを終了)以外のレコードを取得
            sql.Append($"WHERE JOB_STATUS <> 1 ");
            return sql.ToString();
        }

        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <param name="barCode"></param>
        /// <param name="processCode"></param>
        /// <returns></returns>
        public static string GetPreviewAll()
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT ");
            sql.Append($"  BC.{BARCODE} ");
            sql.Append($" ,BC.{PROCESS_CODE} ");
            sql.Append($" ,BC.{BRC_TIME_INSERTED} ");
            sql.Append($" ,BC.{BRC_TIME_PROCESSED} ");
            sql.Append($" ,BC.{BRC_STATUS} ");
            sql.Append($" ,BC.{BRC_ERR_1} ");
            sql.Append($" ,BC.{BRC_ERR_2} ");
            sql.Append($" ,BC.{BRC_ERR_3} ");
            sql.Append($" ,JB.{TbJob.JOB_TIME_INSERTED} ");
            sql.Append($" ,JB.{TbJob.JOB_STATUS} ");
            sql.Append($" ,JB.{TbJob.JOB_TARE_WEIGHT_EXPECTED} ");
            sql.Append($" ,JB.{TbJob.JOB_TARE_WEIGHT_DETECTED} ");
            sql.Append($" ,JB.{TbJob.JOB_TARE_WEIGHT_PERC_ERR_ADMITTED} ");
            sql.Append($" ,JB.{TbJob.JOB_GROSS_WEIGHT_EXPECTED} ");
            sql.Append($" ,JB.{TbJob.JOB_GROSS_WEIGHT_DETECTED} ");
            sql.Append($" ,JB.{TbJob.JOB_GROSS_WEIGHT_PERC_ERR_ADMITTED} ");
            sql.Append($" ,JB.{TbJob.JOB_NET_WEIGHT_EXPECTED} ");
            sql.Append($" ,JB.{TbJob.JOB_NET_WEIGHT_DETECTED} ");
            sql.Append($" ,JB.{TbJob.JOB_NET_WEIGHT_PERC_ERR_ADMITTED} ");
            sql.Append($" ,JB.{TbJob.JOB_TOT_COLORANT_WEIGHT_EXPECTED} ");
            sql.Append($" ,JB.{TbJob.JOB_TOT_COLORANT_WEIGHT_DETECTED} ");
            sql.Append($" ,JB.{TbJob.JOB_TOT_COLORANT_WEIGHT_PERC_ERR_ADMITTED} ");
            sql.Append($" ,JB.{TbJob.JOB_TOT_GRAVIMETRIC_WEIGHT_EXPECTED} ");
            sql.Append($" ,JB.{TbJob.JOB_TOT_GRAVIMETRIC_WEIGHT_DETECTED} ");
            sql.Append($" ,JB.{TbJob.JOB_TOT_GRAVIMETRIC_WEIGHT_PERC_ERR_ADMITTED} ");
            sql.Append($" ,JB.{TbJob.JOB_MIXING} ");
            sql.Append($" ,JB.{TbJob.JOB_MIXING_TIME} ");
            sql.Append($" ,JB.{TbJob.JOB_MIXING_SPEED} ");
            sql.Append($" ,JB.{TbJob.JOB_CAPPING} ");
            sql.Append($" ,JB.{TbJob.JOB_LID_PLACING} ");
            sql.Append($" ,JB.{TbJob.JOB_LID_CHECK} ");
            sql.Append($" ,JB.{TbJob.JOB_PRINTING_1} ");
            sql.Append($" ,JB.{TbJob.JOB_PRINTING_2} ");
            sql.Append($" ,JB.{TbJob.JOB_PRINTING_3} ");
            sql.Append($" ,JB.{TbJob.JOB_EXIT_POSITION} ");
            sql.Append($" ,JB.{TbJob.JOB_TAG_1} ");
            sql.Append($" ,JB.{TbJob.JOB_TAG_2} ");
            sql.Append($" ,JB.{TbJob.JOB_TAG_3} ");
            sql.Append($" ,JB.{TbJob.JOB_TAG_4} ");
            sql.Append($" ,JB.{TbJob.JOB_TAG_5} ");
            sql.Append($" ,JB.{TbJob.JOB_ERR_1} ");
            sql.Append($" ,JB.{TbJob.JOB_ERR_2} ");
            sql.Append($" ,JB.{TbJob.JOB_ERR_3} ");
            sql.Append($" ,JB.{TbJob.JOB_ERR_4} ");
            sql.Append($" ,JB.{TbJob.JOB_ERR_5} ");
            sql.Append($"FROM {MAIN_TABLE} AS BC ");
            sql.Append($"LEFT JOIN {TbJob.MAIN_TABLE} AS JB ON JB.{TbJob.JOB_BARCODE} = BC.{BARCODE} AND JB.{TbJob.JOB_PROCESS_CODE} = BC.{PROCESS_CODE} ");
            return sql.ToString();
        }

        #endregion

        #region 更新系

        #region ERP側の更新
        /// <summary>
        /// ERP側の更新
        /// </summary>
        /// <returns></returns>
        public static string Update()
        {
            var sql = new StringBuilder();
            sql.Append($"UPDATE {MAIN_TABLE} SET ");
            sql.Append($"  {BRC_TIME_PROCESSED} = @{BRC_TIME_PROCESSED} ");
            sql.Append($" ,{BRC_STATUS}         = @{BRC_STATUS} ");
            sql.Append($" ,{BRC_ERR_1}          = @{BRC_ERR_1} ");
            sql.Append($" ,{BRC_ERR_2}          = @{BRC_ERR_2} ");
            sql.Append($" ,{BRC_ERR_3}          = @{BRC_ERR_3} ");
            sql.Append($"WHERE {BARCODE}      = @{BARCODE} ");
            sql.Append($"  AND {PROCESS_CODE} = @{PROCESS_CODE} ");
            return sql.ToString();
        }
        /// <summary>
        /// ERP側の更新
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static string UpdateFromSimulator(List<string> columns)
        {
            var items = new List<string>();
            foreach (var column in columns)
            {
                items.Add($" {column} = @{column} ");
            }
            var sql = new StringBuilder();
            sql.Append($"UPDATE {MAIN_TABLE} SET {string.Join(",", items)}");
            sql.Append($"WHERE {BARCODE}      = @{BARCODE} ");
            sql.Append($"  AND {PROCESS_CODE} = @{PROCESS_CODE} ");
            return sql.ToString();
        }
        #endregion

        #region マージ
        /// <summary>
        /// Merge
        /// </summary>
        /// <param name="Parameters"></param>
        /// <returns></returns>
        public static string Merge(out List<ParameterItem> parameters)
        {
            return Func.CreateMergeStatement(MAIN_TABLE, Fields, out parameters);
        }
        #endregion

        #endregion
    }
}
