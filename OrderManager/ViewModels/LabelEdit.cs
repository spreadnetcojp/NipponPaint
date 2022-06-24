//*****************************************************************************
//
//  システム名：調色工場用自動計量システム NpCommon
//
//  Copyright 三菱電機エンジニアリング株式会社 2022 All rights reserved.
//
//-----------------------------------------------------------------------------
//  変更履歴:
//  Ver      日付        担当       コメント
//  0.0      2022/04/30  M.Nakai    新規作成
#region 更新履歴
#endregion
//*****************************************************************************

#region using defines
#endregion

namespace NipponPaint.OrderManager.ViewModels
{
    /// <summary>
    /// ビューモデル
    /// ラベルタイプダイアログ
    /// </summary>
    public class LabelEdit
    {
        /// <summary>
        /// ラベルタイプ
        /// </summary>
        public int LabelType { get; set; }

        /// <summary>
        /// 処理切り替えパラメータ
        /// </summary>
        // FrmLabelEditの処理切替用Parameter
        // 新規作成         0 を設定する
        // 複製して新規作成 1 を設定する
        // 修正             2 を設定する
        public int Parameter{ get; set; }


        public LabelEdit()
        {
            
            //FrmLabelSelection frmLabelSelection = new FrmLabelSelection();
            //int.TryParse(frmLabelSelection.TxtLabelType.Value, out int labelType);
            //LabelType = labelType;
            //新規作成
            //複写して新規作成
            //修正

        }
    }
}
