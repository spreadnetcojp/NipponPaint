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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Attr = NipponPaint.NpCommon.Attributes.AttributeUtility;
#endregion

namespace NipponPaint.NpCommon.Database.Sql.SupervisorPc
{
    public class TbFormula
    {
        #region 定数
        public const string MAIN_TABLE = "TB_FORMULA";

        public const string PRD_BARCODE = "PRD_BARCODE";
        public const string PRD_PROCESS_CODE = "PROCESS_CODE";
        public const string PRD_TIME_INSERTED = "PRD_TIME_INSERTED";
        public const string PRD_STATUS = "PRD_STATUS";
        public const string PRD_CODE = "PRD_CODE";
        public const string PRD_DESC = "PRD_DESC";
        public const string PRD_UM = "UM";
        public const string PRD_SPECIFIC_GRAVITY = "PRD_SPECIFIC_GRAVITY";
        public const string PRD_QTY_REQ = "PRD_QTY_REQ";
        public const string PRD_QTY_DISP = "PRD_QTY_DISP";
        public const string PRD_START_DISP = "PRD_START_DISP";
        public const string PRD_END_DISP = "PRD_END_DISP";
        public const string PRD_PRIORITY = "PRD_PRIORITY";
        public const string PRD_NUM = "PRD_NUM";
        public const string PRD_ISPREFILLED = "PRD_ISPREFILLED";
        public const string PRD_PREFILLED_QTY = "PRD_PREFILLED_QTY";

        public static List<MergeItemDefine> Fields = new List<MergeItemDefine>()
        {
            new MergeItemDefine(){ IsKey = true, IsInsert = true, IsUpdate = false, Field = PRD_BARCODE },
            new MergeItemDefine(){ IsKey = true, IsInsert = true, IsUpdate = false, Field = PRD_PROCESS_CODE },
            new MergeItemDefine(){ IsKey = true, IsInsert = true, IsUpdate = false, Field = PRD_CODE },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = false, Field = PRD_TIME_INSERTED },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = false, Field = PRD_STATUS },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = PRD_DESC },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = PRD_UM },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = PRD_SPECIFIC_GRAVITY },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = PRD_QTY_REQ },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = false, Field = PRD_QTY_DISP },
            new MergeItemDefine(){ IsKey = false, IsInsert = false, IsUpdate = false, Field = PRD_START_DISP },
            new MergeItemDefine(){ IsKey = false, IsInsert = false, IsUpdate = false, Field = PRD_END_DISP },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = PRD_PRIORITY },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = PRD_NUM },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = PRD_ISPREFILLED },
            new MergeItemDefine(){ IsKey = false, IsInsert = true, IsUpdate = true, Field = PRD_PREFILLED_QTY },
        };
        #endregion

        #region 参照系

        /// <summary>
        /// 一覧取得
        /// </summary>
        /// <returns></returns>
        public static string GetPreview(string barCode, string processCode)
        {
            var sql = new StringBuilder();
            sql.Append($"SELECT ");
            sql.Append($"  {PRD_CODE} ");
            sql.Append($" ,{PRD_TIME_INSERTED} ");
            sql.Append($" ,{PRD_STATUS} ");
            sql.Append($" ,{PRD_DESC} ");
            sql.Append($" ,{PRD_UM} ");
            sql.Append($" ,{PRD_SPECIFIC_GRAVITY} ");
            sql.Append($" ,{PRD_QTY_REQ} ");
            sql.Append($" ,{PRD_QTY_DISP} ");
            sql.Append($" ,{PRD_START_DISP} ");
            sql.Append($" ,{PRD_END_DISP} ");
            sql.Append($" ,{PRD_PRIORITY} ");
            sql.Append($" ,{PRD_NUM} ");
            sql.Append($" ,{PRD_ISPREFILLED} ");
            sql.Append($" ,{PRD_PREFILLED_QTY} ");
            sql.Append($"FROM {MAIN_TABLE} ");
            sql.Append($"WHERE {PRD_BARCODE} = '{barCode}' ");
            sql.Append($"  AND {PRD_PROCESS_CODE} = '{processCode}' ");
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
            sql.Append($"  BC.{TbBarcode.BARCODE} ");
            sql.Append($" ,BC.{TbBarcode.PROCESS_CODE} ");
            sql.Append($" ,BC.{TbBarcode.BRC_TIME_INSERTED} ");
            sql.Append($" ,BC.{TbBarcode.BRC_TIME_PROCESSED} ");
            sql.Append($" ,BC.{TbBarcode.BRC_STATUS} ");
            sql.Append($" ,BC.{TbBarcode.BRC_ERR_1} ");
            sql.Append($" ,BC.{TbBarcode.BRC_ERR_2} ");
            sql.Append($" ,BC.{TbBarcode.BRC_ERR_3} ");
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
            sql.Append($" ,FM.{PRD_BARCODE} ");
            sql.Append($" ,FM.{PRD_PROCESS_CODE} ");
            sql.Append($" ,FM.{PRD_TIME_INSERTED} ");
            sql.Append($" ,FM.{PRD_STATUS} ");
            sql.Append($" ,FM.{PRD_CODE} ");
            sql.Append($" ,FM.{PRD_DESC} ");
            sql.Append($" ,FM.{PRD_SPECIFIC_GRAVITY} ");
            sql.Append($" ,FM.{PRD_QTY_REQ} ");
            sql.Append($" ,FM.{PRD_QTY_DISP} ");
            sql.Append($" ,FM.{PRD_START_DISP} ");
            sql.Append($" ,FM.{PRD_END_DISP} ");
            sql.Append($" ,FM.{PRD_PRIORITY} ");
            sql.Append($" ,FM.{PRD_NUM} ");
            sql.Append($" ,FM.{PRD_ISPREFILLED} ");
            sql.Append($" ,FM.{PRD_PREFILLED_QTY} ");
            sql.Append($"FROM {MAIN_TABLE} AS FM ");
            sql.Append($"INNER JOIN {TbBarcode.MAIN_TABLE} AS BC ON BC.{TbBarcode.BARCODE} = FM.{PRD_BARCODE} AND BC.{TbBarcode.PROCESS_CODE} = FM.{PRD_PROCESS_CODE} ");
            sql.Append($"INNER JOIN {TbJob.MAIN_TABLE}     AS JB ON JB.{TbJob.JOB_BARCODE} = FM.{PRD_BARCODE} AND JB.{TbJob.JOB_PROCESS_CODE} = FM.{PRD_PROCESS_CODE} ");
            sql.Append($"WHERE FM.{PRD_STATUS} <> 1 ");
            sql.Append($"  AND FM.{PRD_BARCODE} = @barcode ");
            return sql.ToString();
        }

        #endregion

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
            sql.Append($"WHERE {PRD_BARCODE}      = @{PRD_BARCODE} ");
            sql.Append($"  AND {PRD_PROCESS_CODE} = @{PRD_PROCESS_CODE} ");
            sql.Append($"  AND {PRD_CODE}         = @{PRD_CODE} ");
            return sql.ToString();
        }
        #endregion


        #endregion
    }
}
