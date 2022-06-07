namespace NipponPaint.NpCommon.FormControls
{
    partial class LabelStatusRadioButtons
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
            this.LblTitle1 = new System.Windows.Forms.Label();
            this.LblTitle2 = new System.Windows.Forms.Label();
            this.Rbt1 = new System.Windows.Forms.RadioButton();
            this.Rbt2 = new System.Windows.Forms.RadioButton();
            this.RadioButtonPanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // LblTitle1
            // 
            this.LblTitle1.BackColor = System.Drawing.Color.Navy;
            this.LblTitle1.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblTitle1.ForeColor = System.Drawing.Color.White;
            this.LblTitle1.Location = new System.Drawing.Point(0, 0);
            this.LblTitle1.Margin = new System.Windows.Forms.Padding(4);
            this.LblTitle1.Name = "LblTitle1";
            this.LblTitle1.Size = new System.Drawing.Size(154, 30);
            this.LblTitle1.TabIndex = 0;
            this.LblTitle1.Text = "テスト缶(&S)";
            this.LblTitle1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LblTitle2
            // 
            this.LblTitle2.BackColor = System.Drawing.Color.Navy;
            this.LblTitle2.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LblTitle2.ForeColor = System.Drawing.Color.White;
            this.LblTitle2.Location = new System.Drawing.Point(0, 35);
            this.LblTitle2.Name = "LblTitle2";
            this.LblTitle2.Size = new System.Drawing.Size(154, 30);
            this.LblTitle2.TabIndex = 1;
            this.LblTitle2.Text = "信頼できる配合(&B)";
            this.LblTitle2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Rbt1
            // 
            this.Rbt1.AutoSize = true;
            this.Rbt1.Location = new System.Drawing.Point(161, 10);
            this.Rbt1.Name = "Rbt1";
            this.Rbt1.Size = new System.Drawing.Size(14, 13);
            this.Rbt1.TabIndex = 2;
            this.Rbt1.TabStop = true;
            this.Rbt1.UseVisualStyleBackColor = true;
            // 
            // Rbt2
            // 
            this.Rbt2.AutoSize = true;
            this.Rbt2.Location = new System.Drawing.Point(161, 45);
            this.Rbt2.Name = "Rbt2";
            this.Rbt2.Size = new System.Drawing.Size(14, 13);
            this.Rbt2.TabIndex = 3;
            this.Rbt2.TabStop = true;
            this.Rbt2.UseVisualStyleBackColor = true;
            // 
            // RadioButtonPanel
            // 
            this.RadioButtonPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RadioButtonPanel.Location = new System.Drawing.Point(0, 0);
            this.RadioButtonPanel.Name = "RadioButtonPanel";
            this.RadioButtonPanel.Size = new System.Drawing.Size(342, 63);
            this.RadioButtonPanel.TabIndex = 4;
            // 
            // LabelStatusRadioButtons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Rbt2);
            this.Controls.Add(this.Rbt1);
            this.Controls.Add(this.LblTitle2);
            this.Controls.Add(this.LblTitle1);
            this.Controls.Add(this.RadioButtonPanel);
            this.Font = new System.Drawing.Font("メイリオ", 11.25F);
            this.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.Name = "LabelStatusRadioButtons";
            this.Size = new System.Drawing.Size(342, 63);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LblTitle1;
        private System.Windows.Forms.Label LblTitle2;
        private System.Windows.Forms.RadioButton Rbt1;
        private System.Windows.Forms.RadioButton Rbt2;
        private System.Windows.Forms.Panel RadioButtonPanel;
    }
}
