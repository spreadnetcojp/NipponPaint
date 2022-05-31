namespace NipponPaint.NpCommon.FormControls
{
    partial class LabelTextSeparate
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

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.PnlData = new System.Windows.Forms.Panel();
            this.LblData2 = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.LblData1 = new System.Windows.Forms.Label();
            this.PnlTitle = new System.Windows.Forms.Panel();
            this.LblWordCount = new System.Windows.Forms.Label();
            this.LblTitle = new System.Windows.Forms.Label();
            this.panelBorder1 = new NipponPaint.NpCommon.FormControls.PanelBorder();
            this.PnlData.SuspendLayout();
            this.PnlTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // PnlData
            // 
            this.PnlData.BackColor = System.Drawing.Color.White;
            this.PnlData.Controls.Add(this.LblData2);
            this.PnlData.Controls.Add(this.splitter1);
            this.PnlData.Controls.Add(this.LblData1);
            this.PnlData.Font = new System.Drawing.Font("メイリオ", 11.25F);
            this.PnlData.Location = new System.Drawing.Point(156, 2);
            this.PnlData.Name = "PnlData";
            this.PnlData.Size = new System.Drawing.Size(1065, 26);
            this.PnlData.TabIndex = 1;
            // 
            // LblData2
            // 
            this.LblData2.Dock = System.Windows.Forms.DockStyle.Left;
            this.LblData2.Font = new System.Drawing.Font("メイリオ", 10F);
            this.LblData2.Location = new System.Drawing.Point(3, 0);
            this.LblData2.Name = "LblData2";
            this.LblData2.Size = new System.Drawing.Size(1004, 26);
            this.LblData2.TabIndex = 3;
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.Color.Red;
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(3, 26);
            this.splitter1.TabIndex = 2;
            this.splitter1.TabStop = false;
            // 
            // LblData1
            // 
            this.LblData1.AutoSize = true;
            this.LblData1.Dock = System.Windows.Forms.DockStyle.Left;
            this.LblData1.Font = new System.Drawing.Font("メイリオ", 10F);
            this.LblData1.Location = new System.Drawing.Point(0, 0);
            this.LblData1.Name = "LblData1";
            this.LblData1.Size = new System.Drawing.Size(0, 21);
            this.LblData1.TabIndex = 1;
            this.LblData1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // PnlTitle
            // 
            this.PnlTitle.BackColor = System.Drawing.Color.Navy;
            this.PnlTitle.Controls.Add(this.LblWordCount);
            this.PnlTitle.Controls.Add(this.LblTitle);
            this.PnlTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.PnlTitle.Location = new System.Drawing.Point(0, 0);
            this.PnlTitle.Name = "PnlTitle";
            this.PnlTitle.Size = new System.Drawing.Size(154, 30);
            this.PnlTitle.TabIndex = 0;
            // 
            // LblWordCount
            // 
            this.LblWordCount.BackColor = System.Drawing.Color.Navy;
            this.LblWordCount.Dock = System.Windows.Forms.DockStyle.Left;
            this.LblWordCount.Font = new System.Drawing.Font("メイリオ", 11.25F);
            this.LblWordCount.ForeColor = System.Drawing.Color.White;
            this.LblWordCount.Location = new System.Drawing.Point(69, 0);
            this.LblWordCount.Name = "LblWordCount";
            this.LblWordCount.Size = new System.Drawing.Size(79, 30);
            this.LblWordCount.TabIndex = 1;
            this.LblWordCount.Text = "(**/**)";
            this.LblWordCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblTitle
            // 
            this.LblTitle.BackColor = System.Drawing.Color.Navy;
            this.LblTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.LblTitle.Font = new System.Drawing.Font("メイリオ", 11.25F);
            this.LblTitle.ForeColor = System.Drawing.Color.White;
            this.LblTitle.Location = new System.Drawing.Point(0, 0);
            this.LblTitle.Name = "LblTitle";
            this.LblTitle.Size = new System.Drawing.Size(69, 30);
            this.LblTitle.TabIndex = 0;
            this.LblTitle.Text = "色名";
            this.LblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelBorder1
            // 
            this.panelBorder1.BackColor = System.Drawing.SystemColors.Control;
            this.panelBorder1.BorderColor = System.Drawing.Color.White;
            this.panelBorder1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelBorder1.BorderWidth = 5;
            this.panelBorder1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelBorder1.Location = new System.Drawing.Point(154, 0);
            this.panelBorder1.Margin = new System.Windows.Forms.Padding(4);
            this.panelBorder1.Name = "panelBorder1";
            this.panelBorder1.Size = new System.Drawing.Size(1069, 30);
            this.panelBorder1.TabIndex = 2;
            // 
            // LabelTextSeparate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.PnlData);
            this.Controls.Add(this.panelBorder1);
            this.Controls.Add(this.PnlTitle);
            this.Font = new System.Drawing.Font("メイリオ", 9F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "LabelTextSeparate";
            this.Size = new System.Drawing.Size(1223, 30);
            this.PnlData.ResumeLayout(false);
            this.PnlData.PerformLayout();
            this.PnlTitle.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel PnlData;
        private System.Windows.Forms.Label LblData2;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Label LblData1;
        private System.Windows.Forms.Panel PnlTitle;
        private System.Windows.Forms.Label LblWordCount;
        private System.Windows.Forms.Label LblTitle;
        private PanelBorder panelBorder1;
    }
}
