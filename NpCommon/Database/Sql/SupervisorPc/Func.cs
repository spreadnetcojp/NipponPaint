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
#endregion

namespace NipponPaint.NpCommon.Database.Sql.SupervisorPc
{
    public class Func
    {

        public static string CreateMergeStatement(string tableName, List<MergeItemDefine> fields, out List<ParameterItem> parameters)
        {
            var selectItems = new List<string>();
            var keyItems = new List<string>();
            var updateItems = new List<string>();
            var insertFields = new List<string>();
            var insertValues = new List<string>();
            parameters = new List<ParameterItem>();
            foreach (var field in fields)
            {
                selectItems.Add($" @{field.Field} AS {field.Field} ");
                parameters.Add(new ParameterItem($"{field.Field}", null));
                if (field.IsKey)
                {
                    keyItems.Add($"{tableName}.{field.Field}=ITEMS.{field.Field}");
                }
                if (field.IsUpdate)
                {
                    updateItems.Add($"{field.Field}=ITEMS.{field.Field}");
                }
                if (field.IsInsert)
                {
                    insertFields.Add($"{field.Field}");
                    insertValues.Add($"@{field.Field}");
                }
            }
            var sql = new StringBuilder();
            sql.Append($"MERGE INTO {tableName} USING (SELECT {string.Join(",", selectItems)}) AS ITEMS ");
            sql.Append($"ON ({string.Join(" AND ", keyItems)}) ");
            sql.Append($"WHEN MATCHED THEN UPDATE SET {string.Join(",", updateItems)} ");
            sql.Append($"WHEN NOT MATCHED THEN INSERT ({string.Join(",", insertFields)}) VALUES ({string.Join(",", insertValues)}); ");
            return sql.ToString();
        }
        public static string CreateInsertStatement(string tableName, List<MergeItemDefine> fields, out List<ParameterItem> parameters)
        {
            var KeyItems = new List<string>();
            var InsertFields = new List<string>();
            var InsertValues = new List<string>();
            parameters = new List<ParameterItem>();
            foreach (var field in fields)
            {
                parameters.Add(new ParameterItem($"{field.Field}", null));
                if (field.IsKey)
                {
                    KeyItems.Add($"{tableName}.{field.Field} = @{field.Field}");
                }
                if (field.IsInsert)
                {
                    InsertFields.Add($"{field.Field}");
                    InsertValues.Add($"@{field.Field}");
                }
            }
            var sql = new StringBuilder();
            sql.Append($"INSERT INTO {tableName} ");
            sql.Append($" ({string.Join(",", InsertFields)}) VALUES ({string.Join(",", InsertValues)}) ");
            sql.Append($"WHERE NOT EXISTS (SELECT * FROM {tableName} WHERE {string.Join(" AND ", KeyItems)} ");
            return sql.ToString();
        }


    }
}
