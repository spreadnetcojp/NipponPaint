namespace NipponPaint.SupervisorIfSim.FormControls
{
    partial class ThreeRadioButtons
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
            this.ThirdButton = new System.Windows.Forms.RadioButton();
            this.SecondButton = new System.Windows.Forms.RadioButton();
            this.FirstButton = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // ThirdButton
            // 
            this.ThirdButton.AutoSize = true;
            this.ThirdButton.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ThirdButton.Location = new System.Drawing.Point(152, 0);
            this.ThirdButton.Name = "ThirdButton";
            this.ThirdButton.Size = new System.Drawing.Size(62, 22);
            this.ThirdButton.TabIndex = 5;
            this.ThirdButton.TabStop = true;
            this.ThirdButton.Tag = "2";
            this.ThirdButton.Text = "エラー";
            this.ThirdButton.UseVisualStyleBackColor = true;
            // 
            // SecondButton
            // 
            this.SecondButton.AutoSize = true;
            this.SecondButton.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SecondButton.Location = new System.Drawing.Point(70, 0);
            this.SecondButton.Name = "SecondButton";
            this.SecondButton.Size = new System.Drawing.Size(74, 22);
            this.SecondButton.TabIndex = 4;
            this.SecondButton.TabStop = true;
            this.SecondButton.Tag = "1";
            this.SecondButton.Text = "吐出完了";
            this.SecondButton.UseVisualStyleBackColor = true;
            // 
            // FirstButton
            // 
            this.FirstButton.AutoSize = true;
            this.FirstButton.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FirstButton.Location = new System.Drawing.Point(3, 0);
            this.FirstButton.Name = "FirstButton";
            this.FirstButton.Size = new System.Drawing.Size(62, 22);
            this.FirstButton.TabIndex = 3;
            this.FirstButton.TabStop = true;
            this.FirstButton.Tag = "0";
            this.FirstButton.Text = "未吐出";
            this.FirstButton.UseVisualStyleBackColor = true;
            // 
            // ThreeRadioButtons
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ThirdButton);
            this.Controls.Add(this.SecondButton);
            this.Controls.Add(this.FirstButton);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ThreeRadioButtons";
            this.Size = new System.Drawing.Size(220, 22);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton ThirdButton;
        private System.Windows.Forms.RadioButton SecondButton;
        private System.Windows.Forms.RadioButton FirstButton;
    }
}
