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

        #endregion
    }
}
