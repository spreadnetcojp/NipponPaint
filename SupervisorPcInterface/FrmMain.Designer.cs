
namespace SupervisorPcInterface
{
    partial class FrmMain
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.TickTimer = new System.Windows.Forms.Timer(this.components);
            this.LblStatus = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // TickTimer
            // 
            this.TickTimer.Tick += new System.EventHandler(this.TickTimer_Tick);
            // 
            // LblStatus
            // 
            this.LblStatus.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblStatus.Location = new System.Drawing.Point(2, 19);
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(281, 23);
            this.LblStatus.TabIndex = 0;
            this.LblStatus.Text = "起動時初回処理中";
            this.LblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 61);
            this.Controls.Add(this.LblStatus);
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "SupervisorPcInterface";
            this.Shown += new System.EventHandler(this.FrmMain_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer TickTimer;
        private System.Windows.Forms.Label LblStatus;
    }
}

