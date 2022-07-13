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
            /// 「注文を閉じる」ボタンをクリック
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "選択されたオーダーがクローズされます。続けてよいですか？")]
            BtnOrderCloseMultipleClicked,

            /// <summary>
            /// 「ステータスを戻す」ボタンをクリック
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "オーダーのステータスを\"CCM配合待ち\"に変更しますか？")]
            BtnStatusResumeClicked,

            /// <summary>
            /// 「ステータスを変更する」ボタンをクリック
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "オーダーのステータスを\"缶製造実施中\"に変更しますか？")]
            BtnChangeStatusClicked,

            /// <summary>
            /// 原料選択でｴﾗｰ
            /// </summary>
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

            /// <summary>
            /// 「緊急印刷」ボタンをクリック
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "緊急印刷がクリックされました")]
            BtnPrintEmergencyClick,

            #region DatabaseManager
            /// <summary>
            /// DatabaseManager 「閉じる」ボタンをクリック(設定を保存していない場合)
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "クローズ前に設定を保存してください")]
            SaveIncompleteInformation,

            /// <summary>
            /// DatabaseManager 「設定を保存」ボタンをクリック(路線便送り状Noの入力に不備がある場合)
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "路線便送り状Noのエラー")]
            TrkInputError,
            #endregion

            #region Supervisor I/F

            /// <summary>
            /// Supervisor I/F 処理開始
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "Supervisor I/F 処理を開始します")]
            StartSupervisorInterface,
            /// <summary>
            /// Supervisor I/F 処理実行
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "Supervisor I/F 処理を実行します（{0}）")]
            ExecuteSupervisorInterface,
            /// <summary>
            /// Supervisor I/F 処理終了
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "Supervisor I/F 処理を終了します")]
            EndSupervisorInterface,
            #endregion

            #region 仕様文章一覧（Query-4)
            /// <summary>
            /// 担当者を決定
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "担当者を決定しました")]
            AssignOperator,

            /// <summary>
            /// オペレーターにより缶が破棄された
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "オペレーターにより缶が破棄されました")]
            CanDiscardedByOperator,

            /// <summary>
            /// 充填重量が最大缶容量を超過
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "充填重量が最大缶容量を超過しました")]
            CanfillingWeightOverMaximumLimit,

            /// <summary>
            /// 缶に完全に吐出されなかった
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "缶に完全に吐出されませんでした")]
            CanNotCompletelyDispensed,

            /// <summary>
            /// 見つからない
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "見つかりませんでした")]
            CanNotFind,

            /// <summary>
            /// 缶種チェックが失敗
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "缶種チェックが失敗しました")]
            CanTypeCheckFailed,

            /// <summary>
            /// 製品オーダーをクローズできません(製造中の缶あり)
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "製品オーダーをクローズできません(製造中の缶あり)")]
            CannotCloseorderOneOrMoreCansAreInTheProductionLine,

            /// <summary>
            /// 缶タイプを削除できない
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "缶タイプを削除できません")]
            CannotDeleteCanType,

            /// <summary>
            /// キャップタイプを削除できない
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "キャップタイプを削除できません")]
            CannotDeleteCapType,

            /// <summary>
            /// ラベルタイプを削除できない
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "ラベルタイプを削除できません")]
            CannotDeleteLabelType,

            /// <summary>
            /// キャップエラー
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "キャップエラー")]
            CapError,

            /// <summary>
            /// CCMデータ受け取り拒絶：配合処方データの前に修正配合を受け取った
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "CCMデータ受け取り拒絶：配合処方データの前に修正配合を受け取りました")]
            RejectedCorrectionForAnOrderWithNoFormula,

            /// <summary>
            /// CCMデータ受け取り拒絶：配合処方の番号が重複している
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "CCMデータ受け取り拒絶：配合処方の番号が重複しています")]
            DuplicateRevisionNumber,

            /// <summary>
            /// CCMデータ受け取り拒絶：製品コードと一致するオーダーがない
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "CCMデータ受け取り拒絶：製品コードと一致するオーダーがありません")]
            NoOrderWithMatchingProductCode,

            /// <summary>
            /// CCMデータ受け取り拒絶：このオーダーは修正配合のみ受け取り可能
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "CCMデータ受け取り拒絶：このオーダーは修正配合のみ受け取り可能です")]
            OrderAllowsCorrectionFormulaOnly,

            /// <summary>
            /// CCMデータ受け取り拒絶：配合処方に合致しないデータが入っている
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "CCMデータ受け取り拒絶：配合処方に合致しないデータが入っています")]
            UnkwownItemsInFormula,

            /// <summary>
            /// CCMデータ受け取り拒絶：データフォーマットが違う
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "CCMデータ受け取り拒絶：データフォーマットが違います")]
            WrongDataFormat,

            /// <summary>
            /// Clear
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "Clearします")]
            Clear,

            /// <summary>
            /// ログファイル情報をクリヤーしますか？
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "ログファイル情報をクリヤーしますか？")]
            ClearLogFileInformation,

            /// <summary>
            /// 閉じる
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "閉じます")]
            Close,

            /// <summary>
            /// 注文を閉じる
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "注文を閉じます")]
            CloseOrder,

            /// <summary>
            /// 自動完了通知
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "自動完了通知")]
            CloseOrdersAutomatically,

            /// <summary>
            /// この位置でのコミュニケーションが時間通貨
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "この位置でのコミュニケーションが時間通貨しました")]
            CommunicationTimeoutPosition,

            /// <summary>
            /// 注文完了
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "注文完了しました")]
            CompletedOrders,

            /// <summary>
            /// テスト缶仕上がり
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "テスト缶仕上がり")]
            ConfirmTestCan,

            /// <summary>
            /// 修正缶
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "缶修正しました")]
            Correction,
            /// <summary>
            /// オーダー番号を削除できませんでした
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "オーダー番号を削除できませんでした")]
            CouldNotRejectOrderNumber,

            /// <summary>
            /// CSVファイル作成
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "CSVファイルを作成しました")]
            CreateCSVFile,

            /// <summary>
            /// データベースバックアップ作成
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "データベースバックアップを作成しました")]
            CreateDatabaseBackup,

            /// <summary>
            /// データベースバックアップ終了
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "データベースバックアップが終了しました")]
            DatabaseBackupDone,

            /// <summary>
            /// データベースバックアップエラー
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "データベースバックアップエラー")]
            DatabaseBackupError,

            /// <summary>
            /// データベースロールバック終了
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "データベースロールバックを終了しました")]
            DatabaseRollbackDone,

            /// <summary>
            /// データベースロールバックエラー
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "データベースロールバックエラー")]
            DatabaseRollbackError,

            /// <summary>
            /// 削除
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "削除します")]
            Delete,

            /// <summary>
            /// 送り状No.エラー
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "送り状No.エラー")]
            DeliveryOrderNumberIsIncorrect,

            /// <summary>
            /// 吐出された配合リリース
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "吐出された配合リリースです")]
            DispensedFormulaRelease,

            /// <summary>
            /// このサーキットで吐出エラー
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "このサーキットで吐出エラー")]
            DispensingErrorCircuit,

            /// <summary>
            /// レポートを印刷しますか？
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "レポートを印刷しますか？")]
            DoYouWantToPaintReport,

            /// <summary>
            /// CCM品名重複
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "CCM品名が重複しています")]
            DuplicateCCMPaintName,

            /// <summary>
            /// 白コードを複写
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "白コードを複写しています")]
            DuplicateWhiteCode,

            /// <summary>
            /// エラー
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "エラー")]
            Error,

            /// <summary>
            /// 原料選択でエラー
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "原料選択でエラー")]
            ErrorInRawMaterialsSelection,

            /// <summary>
            /// エラーを無視して続行
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "エラーを無視して続行します")]
            ErrorMask,

            /// <summary>
            /// 色名ラベルプリンターの通信(COM)ポート開放中にエラー
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "色名ラベルプリンターの通信(COM)ポート開放中にエラー")]
            ErrorWhileOpeningAddOnLabelPrinterCOMPort,

            /// <summary>
            /// CCMシステムの通信(COM)ポート開放中にエラー
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "CCMシステムの通信(COM)ポート開放中にエラー")]
            ErrorWhileOpeningCCMSystemCOMPort,

            /// <summary>
            /// 製品ラベルプリンターの通信(COM)ポートオープン中にエラー
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "製品ラベルプリンターの通信(COM)ポートオープン中にエラー")]
            ErrorWhileOpeningProductLabelPrinter,

            /// <summary>
            /// 荷札ラベルプリンターの通信(COM)ポート中にエラー
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "荷札ラベルプリンターの通信(COM)ポート中にエラー")]
            ErrorWhileOpeningShippingLanbelPrinter,

            /// <summary>
            /// エラー
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "エラー")]
            Errors,

            /// <summary>
            /// FTP経由ファイル交換失敗
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "FTP経由ファイル交換失敗しました")]
            FTPFileProcessingFiled,

            /// <summary>
            /// FTP経由ファイル交換成功
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "FTP経由ファイル交換成功しました")]
            FTPFileProcessingSuccess,

            /// <summary>
            /// HG Systemとの接続設定
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "HG Systemとの接続設定")]
            HGSysyemConnectionParameters,

            /// <summary>
            /// HG Systemデータサーバ非接続
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "HG Systemデータサーバ非接続")]
            HGSystemDataServerNotConnected,

            /// <summary>
            /// HG Systemデータサーバスタート
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "HG Systemデータサーバスタート")]
            HGSystemDataServerStarted,

            /// <summary>
            /// HG Systemファイルが重複
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "HG Systemファイルが重複しています")]
            HGSystemFileDuplicated,

            /// <summary>
            /// HG Systemファイルが破棄
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "HG Systemファイルが破棄されました")]
            HGSystemFileRejected,

            /// <summary>
            /// 不完全データ
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "不完全データ")]
            IncompleteData,

            /// <summary>
            /// 初期化中
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "初期化中です")]
            Initializing,

            /// <summary>
            /// 缶消失
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "缶が消失しました")]
            LostCan,

            /// <summary>
            /// マスクされた
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "マスクされました")]
            Masked,

            /// <summary>
            /// ミキシングができない
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "ミキシングができませんでした")]
            MixingImpossible,

            /// <summary>
            /// 複写して新規作成
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "複写して新規作成します")]
            NewFromSelected,

            /// <summary>
            /// 選択全削除
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "選択全削除します")]
            None,

            /// <summary>
            /// チェックされていない
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "チェックされていません")]
            NotChecked,

            /// <summary>
            /// パックされていない
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "パックされていません")]
            NotPacked,

            /// <summary>
            /// 注文が完成したと通知しますか？
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "注文が完成したと通知しますか？")]
            NotifyOrderAsCompletelyProduced,

            /// <summary>
            /// OK
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "OK")]
            OK,

            /// <summary>
            /// 注文が見つからない
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "注文が見つかりません")]
            OrderNotFound,

            /// <summary>
            /// 吐出許容量を超過
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "吐出許容量を超過しています")]
            OutOfRangeQuantity,

            /// <summary>
            /// テーブルをパック
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "デーブルをパックします")]
            PackTables,

            /// <summary>
            /// パックされた
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "パックされました")]
            Packed,

            /// <summary>
            /// ペイントネームが見つからないため、デフォルト値が取込めない
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "ペイントネームが見つからないため、デフォルト値が取込めません")]
            PaintNameNotFoundDefaultValuesNotLoaded,

            /// <summary>
            /// CCM配合待ち
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "CCM配合待ちです")]
            PendingAtCCM,

            /// <summary>
            /// スタート待ち(PM)
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "スタート待ち(PM)です")]
            PendingAtPM,

            /// <summary>
            /// 品名、色名を入れて下さい
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "品名、色名を入れてください")]
            PleaseInsertProductNameAndPaintName,

            /// <summary>
            /// クローズの前に設定を保存してください
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "クローズの前に設定を保存してください")]
            PleaseSaveOrRestoreSettingsBeforeClosing,

            /// <summary>
            /// 存在する
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "存在します")]
            Present,

            /// <summary>
            /// プリント
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "プリントします")]
            Print,

            /// <summary>
            /// 色名ラベルのプリント
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "色名ラベルをプリントします")]
            PrintAddOnLabel,

            /// <summary>
            /// 色名ラベルのプリント
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "色名ラベルをプリントします")]
            PrintAddOnLabels,

            /// <summary>
            /// バーコード印刷
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "バーコードを印刷します")]
            PrintBarcode,

            /// <summary>
            /// 送り状No.一覧印刷
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "送り状No.の一覧を印刷します")]
            PrintFromCSVFile,

            /// <summary>
            /// 作業指示書の印刷
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "作業指示書を印刷します")]
            PrintInstructionalSheet,

            /// <summary>
            /// ラベル印刷
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "ラベルを印刷します")]
            PrintLabels,

            /// <summary>
            /// 製品オーダーレポートをプリントしますか？
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "製品オーダーレポートをプリントしますか？")]
            PrintOrderProductionReport,

            /// <summary>
            /// 製品ラベルのプリント
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "製品ラベルをプリントします")]
            PrintProductLabel,

            /// <summary>
            /// 製品ラベルのプリント
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "製品ラベルをプリントします")]
            PrintProductLabels,

            /// <summary>
            /// 控え板ラベル印刷
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "控え板ラベルを印刷します")]
            PrintSamplePlateLabels,

            /// <summary>
            /// 荷札印刷
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "荷札を印刷します")]
            PrintShippingLabel,

            /// <summary>
            /// 荷札ラベル印刷
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "荷札ラベルを印刷します")]
            PrintShippingLabels,

            /// <summary>
            /// 最終配合で製造された
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "最終配合で製造されました")]
            ProducedWithLastReleaseFormula,

            /// <summary>
            /// 古い配合で製造された
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "古い配合で製造されました")]
            ProducedWithOldReleaseFormula,

            /// <summary>
            /// 製造完了
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "製造完了しました")]
            ProductionCompleted,

            /// <summary>
            /// 検索結果を表示
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "検索結果を表示します")]
            QueryResultsDisplay,

            /// <summary>
            /// 検索内容設定
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "検索内容を設定します")]
            QuerySettings,

            /// <summary>
            /// 準備完
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "準備完了しました")]
            ReadyForProduction,

            /// <summary>
            /// 注文破棄
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "注文を破棄します")]
            RejectOrders,

            /// <summary>
            /// オーダーを削除しますか？
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "オーダーを削除しますか？")]
            RejectOrder,

            /// <summary>
            /// Order Numberを削除しました
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "Order Numberを削除しました")]
            RejectOrderNumber,

            /// <summary>
            /// 設定を元に戻す
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "設定を元に戻します")]
            RestoreSettings,

            /// <summary>
            /// 缶の再製造
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "缶を再製造します")]
            RevertCan,

            /// <summary>
            /// 注文を戻す
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "注文を戻します")]
            RevertOver,

            /// <summary>
            /// 選択された缶を再製造しますか？
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "選択された缶を再製造しますか？")]
            RevertSelectedCan,

            /// <summary>
            /// 選択された注文を戻しますか？
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "選択された注文を戻しますか？")]
            RevertSelectedOrder,

            /// <summary>
            /// バックアップ地点まで戻る
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "バックアップ地点まで戻ります")]
            RollbackToBackupPoint,

            /// <summary>
            /// HGサーバー作動
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "HGサーバーを作動します")]
            RunHGServer,

            /// <summary>
            /// 検索実行
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "検索を実行します")]
            RunQuery,

            /// <summary>
            /// 実行中
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "実行中です")]
            Runnning,

            /// <summary>
            /// 製造缶実施中
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "製造缶実施中です")]
            RunningProductionCans,

            /// <summary>
            /// テスト缶実施中
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "テスト缶実施中です")]
            RunnningProductionCans,

            /// <summary>
            /// 保存
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "保存します")]
            Save,

            /// <summary>
            /// 設定を保存
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "設定を保存します")]
            SaveSettings,

            /// <summary>
            /// 選択された缶の穴とキャップが一致しない
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "選択された缶の穴とキャップが一致しません")]
            SelectedCanTypeBungholeAndSizeDoNotMuch,

            /// <summary>
            /// 選択された缶タイプは削除されます。続けますか？
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "選択された缶タイプは削除されます。続けますか？")]
            SelectedCanTypeWillBeDeletedContinue,

            /// <summary>
            /// 選択されたキャップタイプは削除されます。続けますか？
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "選択されたキャップタイプは削除されます。続けますか？")]
            SelectedCapTypeWillBeDeletedContinue,

            /// <summary>
            /// CCMの品名を削除
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "選択されたCCMの品名は削除されます。続けますか？")]
            SelectedCCMPaintNameWillBeDeletedContinue,

            /// <summary>
            /// 選択したラベルタイプを削除します。処理を続けますか？
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "選択したラベルタイプを削除します。処理を続けますか？")]
            SelectedLabelTypeWillBeDeletedContinue,

            /// <summary>
            /// 選択されたオーダーがクローズされます。続けて良いですか？
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "選択されたオーダーがクローズされます。続けて良いですか？")]
            SelectedOrderWillBeClosedContinue,

            /// <summary>
            /// 白コードを削除します。処理を続けますか？
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "白コードを削除します。処理を続けますか？")]
            SelectedWhiteCodeWillBeDeletedContinue,

            /// <summary>
            /// CCMデータ送信
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "CCMデータを送信します")]
            SendCCMData,

            /// <summary>
            /// HG Systemデータを送る
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "HG Systemデータを送ります")]
            SendHGSystemData,

            /// <summary>
            /// 最終配合が吐出されていない缶があります！　それでもオーダーをクローズしますか？
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "最終配合が吐出されていない缶があります！　それでもオーダーをクローズしますか？")]
            SomeCansHaventBeenDispensedWithLastReleaseCloseOrder,

            /// <summary>
            /// 注文開始
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "注文を開始します")]
            StartOrder,

            /// <summary>
            /// HGサーバー停止
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "HGサーバーを停止します")]
            StopHGServer,

            /// <summary>
            /// 停止中
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "停止中です")]
            Stopped,

            /// <summary>
            /// 正常処理
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "正常処理されました")]
            SuccessfullyProduced,

            /// <summary>
            /// 電話番号は1バイト数値コードのみ入力できます
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "電話番号は1バイト数値のみ入力できます")]
            TelephoneNumberOnlyAllowsSingleByteNumericalCharacters,

            /// <summary>
            /// 'Kanji','漢字'と書かれたフィールドは2バイトコード文字を入力しなければなりません
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "'Kanji','漢字'と書かれたフィールドは2バイトコード文字を入力しなければなりません")]
            TheFieldsMarkedAsKanjiMustBeFilledWithDualByteCoded,

            /// <summary>
            /// 本日および翌日の路線便配送決定
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "本日および翌日の路線配送便を決定します")]
            TruckCompanyDeliveriesScheduledForTodayTomorrow,

            /// <summary>
            /// 路線便決定
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "路線便を決定します")]
            TruckCompanySettings,

            /// <summary>
            /// 不明缶
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "不明缶")]
            UnknownCan,

            /// <summary>
            /// 処理異常
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "処理異常")]
            UnsuccsessfullyProduced,

            /// <summary>
            /// 調色担当待ち
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "調色担当待ちです")]
            WaitingOperatorAssignment,

            /// <summary>
            /// 出庫
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "出庫します")]
            Warehouse,

            /// <summary>
            /// 警告！！　送り状の番号残が100以下：
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "警告！！　送り状の番号残が100以下です：")]
            RemainingDeliveryOrderNumbers,

            /// <summary>
            /// 白ベースコードが見つからないため、デフォルト値が取込めない
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "白ベースコードが見つからないため、デフォルト値が取込めません")]
            WhiteCodeNotFoundDefaultValueNotLoaded,

            /// <summary>
            /// 充填缶の重量はゼロではないが、それをゼロとセット
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "充填缶の重量はゼロではありませんが、それをゼロとセットします")]
            WhiteWeightNotZeroWillBeSetToZero,

            /// <summary>
            /// 空缶への白投入量が0です。続行しますか？
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "空缶への白投入量が0です。続行しますか？")]
            WhiteWeightZeroEmptyCanProceedAnyway,

            /// <summary>
            /// 誤った缶の種類
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "誤った缶の種類")]
            WrongCanType,

            /// <summary>
            /// 誤った入力時重量
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "誤った入力時重量")]
            WrongInputWeight,

            /// <summary>
            /// 誤った出力重量
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "誤った出力重量")]
            WrongOutputWeight,

            /// <summary>
            /// オペレーター削除
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "オペレーターを削除します")]
            DeleteOperator,

            /// <summary>
            /// オーダーは削除されました！
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "オーダーは削除されました！")]
            OrdersDeleted,

            /// <summary>
            /// オーダーのステタスを"CCM配合待ち"に変更しますか？
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "オーダーのステタスを'CCM配合待ち'に変更しますか？")]
            ChangeOrderStatusToWaitingCCMFormula,

            /// <summary>
            /// オーダー削除
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "オーダーを削除します")]
            DeleteOrder,

            /// <summary>
            /// 統計報告パスワード変更
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "統計報告パスワードを変更します")]
            ChangeStatisticReportPassword,

            /// <summary>
            /// 
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "選択した製品を削除します。処理を続けますか？")]
            SelectedProductDeletedContinue,

            /// <summary>
            /// データがありません
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "データがありません")]
            MissingData,

            /// <summary>
            /// 1オーダーのオーダークローズ
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "1オーダーのオーダーをクローズします")]
            CloseCurrentOrder,

            /// <summary>
            /// CCM配合待ち(ST1)オーダークローズ
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "CCM配合待ち(ST1)のオーダーをクローズします")]
            CloseWaitingForCCMMormula,

            /// <summary>
            /// テスト缶実施中(ST3)オーダークローズ
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "テスト缶実施中(ST3)のオーダーをクローズします")]
            CloseProductionOfTestCan,

            /// <summary>
            /// 製造缶実施中(ST4)オーダークローズ
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "製造缶実施中(ST4)のオーダーをクローズします")]
            CloseProductionOfProduct,

            /// <summary>
            /// 全て選択
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "全てを選択します")]
            SelectAll,

            /// <summary>
            /// 選択したオーダーを削除しますか？
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "選択したオーダーを削除しますか？")]
            DeleteSelectedOrders,

            /// <summary>
            /// 対象ファイル選択
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "対象ファイルを選択します")]
            SelectDestinationFile,

            /// <summary>
            /// CSVファイル作成成功！
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "CSVファイル作成成功！")]
            FileCSVCreateSuccessfull,

            /// <summary>
            /// FTPファイル取り込み工程成功(
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "FTPファイル取り込み工程成功(")]
            FTPFileProcessingIsSuccess,

            /// <summary>
            /// 今バックアップ開始
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "今からバックアップを開始します")]
            ExecBackupNow,

            /// <summary>
            /// データ復帰
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "データの復帰をします")]
            RestoreTables,

            /// <summary>
            /// 履歴データ削除
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "履歴データを削除します")]
            PackTable,

            /// <summary>
            /// トラブル発生時のデータベース保存
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "トラブル発生時のデータベースを保存します")]
            TroubleBackup,

            /// <summary>
            /// 履歴をデータベースから削除
            /// </summary>
            [Display(Order = (int)Log.LogType.Info, Description = "履歴をデータベースから削除します")]
            DeleteOrderFromArchive,

            //↓↓追加で作成　重複する内容が見つかれば削除します。
            /// <summary>
            /// データ復帰
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "選択したデータを復元しますか？")]
            RestoreSelectedTables,

            /// <summary>
            /// 履歴データ削除
            /// </summary>
            [Display(Order = (int)Log.LogType.Question, Description = "アーカイブから履歴データを削除しますか？")]
            ErasePackTable,
            //↑↑追加で作成　重複する内容が見つかれば削除します。

            #endregion

            #region ログクリーンアップ
            /// <summary>
            /// ログ削除失敗
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "ログのクリーンアップに失敗しました（エラー：{0}）")]
            LogCleanupFailure,
            /// <summary>
            /// ログ削除失敗
            /// </summary>
            [Display(Order = (int)Log.LogType.Error, Description = "ログをクリーンアップしました")]
            LogCleanupComplate,
            #endregion

        }
    }
}
