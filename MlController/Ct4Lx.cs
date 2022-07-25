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
using System.IO;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using SATO.MLComponent;
#endregion

namespace MlController
{
    public class Ct4Lx : IDisposable
    {

        #region メンバ変数
        private MLComponent _mlComponent;
        private bool _disposed { get; set; } = false;
        private string _errorMessage { get; set; } = string.Empty;
        #endregion

        #region コンストラクタ
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="layoutFileName"></param>
        /// <exception cref="Exception"></exception>
        public Ct4Lx(string layoutFileName)
        {
            var filePath = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "Ct4Lx.ini");
            var setting = new Inifile.Setting(filePath);
            _mlComponent = new MLComponent();
            try
            {
                // 通信設定プロパティの設定
                _mlComponent.Setting = setting.Properties.Setting;
                // タイムアウト値を設定（ここでは3秒）
                _mlComponent.Timeout = setting.Properties.TimeOutSec;
                // 通信プロトコルを設定
                switch (setting.Properties.ProtocolType)
                {
                    case 0:
                        _mlComponent.Protocol = Protocols.Status3;
                        break;
                    case 1:
                        _mlComponent.Protocol = Protocols.Status4;
                        break;
                }
                // レイアウトファイルの設定
                _mlComponent.LayoutFile = layoutFileName;
                // 1枚ごとカット
                _mlComponent.MultiCut = 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            var ret = _mlComponent.OpenPort(1);
            if (ret != 0)
            {
                throw new Exception($"OpenPortエラー。ret={ret}");
            }
        }
        #endregion

        #region ディスポーザ
        /// <summary>
        /// ディスポーザ
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// ディスポーザ
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    PrinterReadyWait();
                    _mlComponent.ClosePort();
                }
                _disposed = true;
            }
        }
        #endregion

        #region デストラクタ
        /// <summary>
        /// デストラクタ
        /// </summary>
        ~Ct4Lx()
        {
            Dispose(false);
        }
        #endregion

        #region public functions

        #region ラベル発行

        /// <summary>
        /// ラベル発行
        /// </summary>
        /// <param name="item"></param>
        public void Print(string item)
        {
            Print(new List<string>() { item });
        }

        /// <summary>
        /// ラベル発行
        /// </summary>
        /// <param name="items"></param>
        public void Print(List<string> items)
        {
            Print(items.ToArray());
        }

        /// <summary>
        /// ラベル発行
        /// </summary>
        public void Print(string[] items)
        {
            foreach (var item in items)
            {
                // プリンタが受信可能になるまで待つ。
                // 本処理を省略すると、紙切れやエラー時にデータを送信し、ラベル抜けが発生する。
                // データ送信前の状態確認は必ず行い、運用に応じて送信後の状態確認も行うこと。
                if (PrinterReadyWait() == false)
                {
                    throw new Exception(_errorMessage);
                }
                // 発行します。
                _mlComponent.PrnData = item;
                var ret = _mlComponent.Output();
                if (ret != 0)
                {
                    throw new Exception($"ラベル発行エラー。ret={ret}");
                }
                Application.DoEvents();
            }
        }

        #endregion

        #endregion

        #region private functions

        #region 印字が完了するまでWait
        /// <summary>
        /// 印字が完了するまでWait
        /// </summary>
        /// <returns></returns>
        private Boolean PrinterReadyWait()
        {
            string myStatus = null;
            int ret = 0;
            //関数の実行結果
            string strSetting = _mlComponent.Setting.Substring(0, 4);
            //出力先がドライバの場合ステータスチェックを行わない
            if ((strSetting != "DRV:") & (strSetting != "ODV:") & (strSetting != "FILE"))
            {
                myStatus = "";
                while ((myStatus != "A000000") & (myStatus != "B000000"))
                {
                    ret = _mlComponent.GetStatus(ref myStatus);
                    if (ret != 0)
                    {
                        _errorMessage = $"ステータス応答がありません。ret={ret}";
                        return false;
                    }
                    //受信ステータスからステータスと残り枚数の部分を取り出す
                    myStatus = myStatus.Substring(2, 7);
                    Application.DoEvents();
                }
            }
            return true;
        }
        #endregion

        #endregion

    }
}
