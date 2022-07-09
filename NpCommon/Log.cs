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
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.IO;
#endregion

namespace NipponPaint.NpCommon
{
    /// <summary>
    /// ログ出力クラス
    /// </summary>
    public static class Log
    {
        #region 定数
        /// <summary>
        /// メッセージ種別
        /// </summary>
        public enum LogType
        {
            /// <summary>
            /// 情報
            /// </summary>
            [Display(Description = "情報")]
            Info,
            /// <summary>
            /// 警告
            /// </summary>
            [Display(Description = "警告")]
            Warn,
            /// <summary>
            /// エラー
            /// </summary>
            [Display(Description = "エラー")]
            Error,
            /// <summary>
            /// クリティカル
            /// </summary>
            [Display(Description = "クリティカル")]
            Critical,
            /// <summary>
            /// 質問
            /// </summary>
            [Display(Description = "質問")]
            Question,
            /// <summary>
            /// デバッグ
            /// </summary>
            [Display(Description = "デバッグ")]
            Debug,
            /// <summary>
            /// ログ対象外
            /// </summary>
            [Display(Description = "ログ対象外")]
            NotApplicableLog,
        }
        /// <summary>
        /// アプリケーション種別
        /// </summary>
        public enum ApplicationType
        {
            /// <summary>
            /// オーダーマネージャ
            /// </summary>
            [Display(Description = "OrderManager")]
            OrderManager,
            /// <summary>
            /// データベースマネージャ
            /// </summary>
            [Display(Description = "DatabaseManager")]
            Databasemanager,
            /// <summary>
            /// Supervisor I/F
            /// </summary>
            [Display(Description = "SupervisorInterface")]
            SupervisorInterface,
        }
        // ログ作成先フォルダ
        private const string SUB_FOLDER = "Log";
        private const string FILE_DATE_FORMAT = "yyyyMMdd";
        #endregion

        #region public static function

        #region ログ出力処理
        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="messegeId"></param>
        public static string Write(Sentence.Messages messegeId, ApplicationType applicationType, object[] addtionalInfo = null)
        {
            // アプリケーション種別を取得
            var applicationTypeText = Messages.GetApplicationTypeText(applicationType);
            // メッセージ情報取得
            var messageInfo = Messages.GetInfo(messegeId);
            // ログ種類には便宜上、Orderプロパティを使用している
            var logTypeText = Messages.GetLogTypeText((LogType)messageInfo.Order);
            // メッセージの編集
            var messageText = string.Empty;
            if (addtionalInfo == null || !addtionalInfo.Any())
            {
                messageText = messageInfo.Description;
            }
            else
            {
                messageText = string.Format(messageInfo.Description, addtionalInfo);
            }
            if (messageInfo.Order == (int)Log.LogType.NotApplicableLog)
            {
                return messageText;
            }
#if DEBUG
#else
            if (messageInfo.Order == (int)Log.LogType.Debug)
            {
                return messageText;
            }
#endif
            // ファイルパスの生成
            string folderPath = Path.Combine(Application.StartupPath, SUB_FOLDER);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            string filePath = Path.Combine(folderPath, $"{applicationTypeText}{DateTime.Now.ToString(FILE_DATE_FORMAT)}.log");
            // 出力処理
            using (StreamWriter writer = new StreamWriter(filePath, true))
            {
                writer.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff")}{'\t'}{applicationTypeText}{'\t'}{logTypeText}{'\t'}{messageText}");
            }
            return messageText;
        }

        /// <summary>
        /// ログ出力
        /// </summary>
        /// <param name="messegeId"></param>
        public static string Write(Sentence.Messages messegeId, ApplicationType applicationType, string addtionalInfo)
        {
            if (string.IsNullOrEmpty(addtionalInfo))
            {
                return Write(messegeId, applicationType);
            }
            else
            {
                return Write(messegeId, applicationType, new string[] { addtionalInfo });
            }
        }
        #endregion

        #region ログクリーンアップ処理
        /// <summary>
        /// ログクリーンアップ処理
        /// </summary>
        /// <param name="settings"></param>
        /// <param name="applicationType"></param>
        public static void CleanUp(IniFile.Settings settings, ApplicationType applicationType)
        {
            try
            {
                // フォルダ存在確認
                string folderPath = Path.Combine(Application.StartupPath, SUB_FOLDER);
                if (!Directory.Exists(folderPath))
                {
                    return;
                }
                // 保持日付
                var targetDate = int.Parse(DateTime.Now.AddDays(-settings.Log.RetentionPeriodDays).ToString(FILE_DATE_FORMAT));
                // アプリケーション種別を取得
                var applicationTypeText = Messages.GetApplicationTypeText(applicationType);
                var files = Directory.GetFiles(Path.Combine(folderPath));
                foreach (var file in files)
                {
                    var info = new FileInfo(file);
                    var fileDate = info.Name.Replace(applicationTypeText, "").Replace(info.Extension, "");
                    if (int.TryParse(fileDate, out var date))
                    {
                        if (date < targetDate)
                        {
                            File.Delete(file);
                        }
                    }
                }
                Write(Sentence.Messages.LogCleanupComplate, applicationType);
            }
            catch (Exception ex)
            {
                Write(Sentence.Messages.LogCleanupFailure, applicationType, ex.Message);
            }
        }
        #endregion

        #endregion

    }
}
