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
using System.ComponentModel.DataAnnotations;
#endregion

namespace NipponPaint.NpCommon
{
    public class Sentence
    {
        public enum Messages
        {
            /// <summary>
            /// 例外
            /// </summary>
            [Display(Order = (int)Log.LogType.Critical, Description = "例外が発生しました（内容：{0}）")]
            Exception,

            /// <summary>
            /// ダイアログを開きました
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "ダイアログを開きました")]
            OpenDialog,

            /// <summary>
            /// 主画面の初期化を完了しました
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "主画面の初期化を完了しました")]
            InitializedMainForm,

            /// <summary>
            /// 一覧を更新しました
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "一覧を更新しました")]
            PreviewData,

            /// <summary>
            /// 主画面を開きました
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "主画面を開きました")]
            OpenMainForm,

            /// <summary>
            /// 主画面を閉じました
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "主画面を閉じました")]
            CloseMainForm,

            /// <summary>
            /// データベースの読み込みを実行しました
            /// </summary>
            [Display(Order = (int)Log.LogType.NotApplicableLog, Description = "データベースの読み込みを実行しました（SQL：{0}）")]
            SelectedDatabase,

            /// <summary>
            /// データベースへの登録を実行しました
            /// </summary>
            [Display(Order = (int)Log.LogType.NotApplicableLog, Description = "データベースへの登録を実行しました（SQL：{0}）")]
            RegistedDatabase,

            /// <summary>
            /// ボタンクリック
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "ボタンをクリックしました（機能：{0}）")]
            ButtonClicked,

            /// <summary>
            /// 保存完了
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "設定内容を保存しました")]
            SaveComplate,

            /// <summary>
            /// 存在チェックエラー
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "指定した処理No（{0}）は存在しません")]
            NotExistsDataNumber,

            /// <summary>
            /// 未入力エラー
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "{0}が未入力です")]
            NotEntryData,

            /// <summary>
            /// 一覧の行を選択しました。
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "一覧の行を選択しました")]
            SelectRow,

            /// <summary>
            /// 「注文を閉じる」ボタンをクリック
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "選択されたオーダーがクローズされます。続けてよいですか？ {0}")]
            BtnOrderCloseClicked,

            /// <summary>
            /// 「ステータスを戻す」ボタンをクリック
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "オーダーのステータスを\"CCM配合待ち\"に変更しますか？ ")]
            BtnStatusResumeClicked,

            [Display(Order = (int)Log.LogType.Error, Description = "原料選択でｴﾗｰ")]
            SelectMaterialError,

            /// <summary>
            /// 「注文を戻す」ボタンをクリック
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "選択された注文を戻しますか？")]
            BtnOrderBackClick,

            /// <summary>
            /// 保存完了
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "設定内容を保存できませんでした（エラー：{0}）")]
            SaveFailure,

            #region Supervisor I/F

            /// <summary>
            /// Supervisor I/F 処理開始
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "Supervisor I/F 処理を開始します")]
            StartSupervisorInterface,
            /// <summary>
            /// Supervisor I/F 処理実行
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "Supervisor I/F 処理を実行します（バーコード：{0}、件数：{1}）")]
            ExecuteSupervisorInterface,
            /// <summary>
            /// Supervisor I/F 処理終了
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "Supervisor I/F 処理を終了します")]
            EndSupervisorInterface,
            #endregion

        }
    }
}
