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
    public class TbJob
    {
        #region 定数
        public const string MAIN_TABLE = "TB_JOB";

        public const string JOB_BARCODE = "JOB_BARCODE";
        public const string JOB_PROCESS_CODE = "PROCESS_CODE";
        public const string JOB_TIME_INSERTED = "JOB_TIME_INSERTED";
        public const string JOB_STATUS = "JOB_STATUS";
        public const string JOB_TARE_WEIGHT_EXPECTED = "JOB_TARE_WEIGHT_EXPECTED";
        public const string JOB_TARE_WEIGHT_DETECTED = "JOB_TARE_WEIGHT_DETECTED";
        public const string JOB_TARE_WEIGHT_PERC_ERR_ADMITTED = "JOB_TARE_WEIGHT_PERC_ERR_ADMITTED";
        public const string JOB_GROSS_WEIGHT_EXPECTED = "JOB_GROSS_WEIGHT_EXPECTED";
        public const string JOB_GROSS_WEIGHT_DETECTED = "JOB_GROSS_WEIGHT_DETECTED";
        public const string JOB_GROSS_WEIGHT_PERC_ERR_ADMITTED = "JOB_GROSS_WEIGHT_PERC_ERR_ADMITTED";
        public const string JOB_NET_WEIGHT_EXPECTED = "JOB_NET_WEIGHT_EXPECTED";
        public const string JOB_NET_WEIGHT_DETECTED = "JOB_NET_WEIGHT_DETECTED";
        public const string JOB_NET_WEIGHT_PERC_ERR_ADMITTED = "JOB_NET_WEIGHT_PERC_ERR_ADMITTED";
        public const string JOB_TOT_COLORANT_WEIGHT_EXPECTED = "JOB_TOT_COLORANT_WEIGHT_EXPECTED";
        public const string JOB_TOT_COLORANT_WEIGHT_DETECTED = "JOB_TOT_COLORANT_WEIGHT_DETECTED";
        public const string JOB_TOT_COLORANT_WEIGHT_PERC_ERR_ADMITTED = "JOB_TOT_COLORANT_WEIGHT_PERC_ERR_ADMITTED";
        public const string JOB_TOT_GRAVIMETRIC_WEIGHT_EXPECTED = "JOB_TOT_GRAVIMETRIC_WEIGHT_EXPECTED";
        public const string JOB_TOT_GRAVIMETRIC_WEIGHT_DETECTED = "JOB_TOT_GRAVIMETRIC_WEIGHT_DETECTED";
        public const string JOB_TOT_GRAVIMETRIC_WEIGHT_PERC_ERR_ADMITTED = "JOB_TOT_GRAVIMETRIC_WEIGHT_PERC_ERR_ADMITTED";
        public const string JOB_MIXING = "JOB_MIXING";
        public const string JOB_MIXING_TIME = "JOB_MIXING_TIME";
        public const string JOB_MIXING_SPEED = "JOB_MIXING_SPEED";
        public const string JOB_CAPPING = "JOB_CAPPING";
        public const string JOB_LID_PLACING = "JOB_LID_PLACING";
        public const string JOB_LID_CHECK = "JOB_LID_CHECK";
        public const string JOB_PRINTING_1 = "JOB_PRINTING_1";
        public const string JOB_PRINTING_2 = "JOB_PRINTING_2";
        public const string JOB_PRINTING_3 = "JOB_PRINTING_3";
        public const string JOB_EXIT_POSITION = "JOB_EXIT_POSITION";
        public const string JOB_TAG_1 = "JOB_TAG_1";
        public const string JOB_TAG_2 = "JOB_TAG_2";
        public const string JOB_TAG_3 = "JOB_TAG_3";
        public const string JOB_TAG_4 = "JOB_TAG_4";
        public const string JOB_TAG_5 = "JOB_TAG_5";
        public const string JOB_ERR_1 = "JOB_ERR_1";
        public const string JOB_ERR_2 = "JOB_ERR_2";
        public const string JOB_ERR_3 = "JOB_ERR_3";
        public const string JOB_ERR_4 = "JOB_ERR_4";
        public const string JOB_ERR_5 = "JOB_ERR_5";
        #endregion

        public static List<MergeItemDefine> Fields = new List<MergeItemDefine>()
        {
            new MergeItemDefine(){ IsKey = true, IsInsert = true, IsUpdate = false, Field = JOB_BARCODE },
            new MergeItemDefine(){ IsKey = true, IsInsert = true, IsUpdate = false, Field = JOB_PROCESS_CODE },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = false, Field = JOB_TIME_INSERTED },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = false, Field = JOB_STATUS },
            new MergeItemDefine(){ IsKey = false, IsInsert = false, IsUpdate = false, Field = JOB_TARE_WEIGHT_EXPECTED },
            new MergeItemDefine(){ IsKey = false, IsInsert = false, IsUpdate = false, Field = JOB_TARE_WEIGHT_DETECTED },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = JOB_TARE_WEIGHT_PERC_ERR_ADMITTED },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = JOB_GROSS_WEIGHT_EXPECTED },
            new MergeItemDefine(){ IsKey = false, IsInsert = false, IsUpdate = false, Field = JOB_GROSS_WEIGHT_DETECTED },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = JOB_GROSS_WEIGHT_PERC_ERR_ADMITTED },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = JOB_NET_WEIGHT_EXPECTED },
            new MergeItemDefine(){ IsKey = false, IsInsert = false, IsUpdate = false, Field = JOB_NET_WEIGHT_DETECTED },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = JOB_NET_WEIGHT_PERC_ERR_ADMITTED },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = JOB_TOT_COLORANT_WEIGHT_EXPECTED },
            new MergeItemDefine(){ IsKey = false, IsInsert = false, IsUpdate = false, Field = JOB_TOT_COLORANT_WEIGHT_DETECTED },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = JOB_TOT_COLORANT_WEIGHT_PERC_ERR_ADMITTED },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = JOB_TOT_GRAVIMETRIC_WEIGHT_EXPECTED },
            new MergeItemDefine(){ IsKey = false, IsInsert = false, IsUpdate = false, Field = JOB_TOT_GRAVIMETRIC_WEIGHT_DETECTED },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = JOB_TOT_GRAVIMETRIC_WEIGHT_PERC_ERR_ADMITTED },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = JOB_MIXING },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = JOB_MIXING_TIME },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = JOB_MIXING_SPEED },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = JOB_CAPPING },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = JOB_LID_PLACING },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = JOB_LID_CHECK },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = JOB_PRINTING_1 },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = JOB_PRINTING_2 },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = JOB_PRINTING_3 },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = JOB_EXIT_POSITION },
            new MergeItemDefine(){ IsKey = false, IsInsert = false, IsUpdate = false, Field = JOB_TAG_1 },
            new MergeItemDefine(){ IsKey = false, IsInsert = false, IsUpdate = false, Field = JOB_TAG_2 },
            new MergeItemDefine(){ IsKey = false, IsInsert = false, IsUpdate = false, Field = JOB_TAG_3 },
            new MergeItemDefine(){ IsKey = false, IsInsert = false, IsUpdate = false, Field = JOB_TAG_4 },
            new MergeItemDefine(){ IsKey = false, IsInsert = false, IsUpdate = false, Field = JOB_TAG_5 },
            new MergeItemDefine(){ IsKey = false, IsInsert = false, IsUpdate = false, Field = JOB_ERR_1 },
            new MergeItemDefine(){ IsKey = false, IsInsert = false, IsUpdate = false, Field = JOB_ERR_2 },
            new MergeItemDefine(){ IsKey = false, IsInsert = false, IsUpdate = false, Field = JOB_ERR_3 },
            new MergeItemDefine(){ IsKey = false, IsInsert = false, IsUpdate = false, Field = JOB_ERR_4 },
            new MergeItemDefine(){ IsKey = false, IsInsert = false, IsUpdate = false, Field = JOB_ERR_5 },
        };

        #region 更新系

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

        #region インサート
        /// <summary>
        /// InsertFirst
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string InsertFirst(out List<ParameterItem> parameters)
        {
            return Func.CreateInsertStatement(MAIN_TABLE, Fields, out parameters);
        }
        #endregion

        #region ERP側の更新
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
            sql.Append($"WHERE {JOB_BARCODE}      = @{JOB_BARCODE} ");
            sql.Append($"  AND {JOB_PROCESS_CODE} = @{JOB_PROCESS_CODE} ");
            return sql.ToString();
        }
        #endregion

        #endregion


    }
}
