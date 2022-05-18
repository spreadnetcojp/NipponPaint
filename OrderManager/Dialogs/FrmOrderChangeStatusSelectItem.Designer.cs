namespace NipponPaint.OrderManager.Dialogs
{
    partial class FrmOrderChangeStatusSelectItem
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.PnlBtnChk = new System.Windows.Forms.Panel();
            this.ChkSelectAll = new System.Windows.Forms.CheckBox();
            this.BtnClose = new System.Windows.Forms.Button();
            this.BtnChangeStatus = new System.Windows.Forms.Button();
            this.PnlList = new System.Windows.Forms.Panel();
            this.GvChangeOrders = new System.Windows.Forms.DataGridView();
            this.PnlBtnChk.SuspendLayout();
            this.PnlList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvChangeOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // PnlBtnChk
            // 
            this.PnlBtnChk.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlBtnChk.Controls.Add(this.ChkSelectAll);
            this.PnlBtnChk.Controls.Add(this.BtnClose);
            this.PnlBtnChk.Controls.Add(this.BtnChangeStatus);
            this.PnlBtnChk.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PnlBtnChk.Location = new System.Drawing.Point(0, 693);
            this.PnlBtnChk.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.PnlBtnChk.Name = "PnlBtnChk";
            this.PnlBtnChk.Size = new System.Drawing.Size(1184, 68);
            this.PnlBtnChk.TabIndex = 99;
            // 
            // ChkSelectAll
            // 
            this.ChkSelectAll.AutoSize = true;
            this.ChkSelectAll.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ChkSelectAll.Location = new System.Drawing.Point(634, 21);
            this.ChkSelectAll.Name = "ChkSelectAll";
            this.ChkSelectAll.Size = new System.Drawing.Size(113, 27);
            this.ChkSelectAll.TabIndex = 5;
            this.ChkSelectAll.Text = "全て選択(&A)";
            this.ChkSelectAll.UseVisualStyleBackColor = true;
            // 
            // BtnClose
            // 
            this.BtnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BtnClose.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnClose.Location = new System.Drawing.Point(233, 6);
            this.BtnClose.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.BtnClose.Name = "BtnClose";
            this.BtnClose.Size = new System.Drawing.Size(220, 54);
            this.BtnClose.TabIndex = 4;
            this.BtnClose.Text = "閉じる";
            this.BtnClose.UseVisualStyleBackColor = true;
            // 
            // BtnChangeStatus
            // 
            this.BtnChangeStatus.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.BtnChangeStatus.Location = new System.Drawing.Point(6, 6);
            this.BtnChangeStatus.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.BtnChangeStatus.Name = "BtnChangeStatus";
            this.BtnChangeStatus.Size = new System.Drawing.Size(220, 54);
            this.BtnChangeStatus.TabIndex = 3;
            this.BtnChangeStatus.Text = "ステータスを変更する";
            this.BtnChangeStatus.UseVisualStyleBackColor = true;
            // 
            // PnlList
            // 
            this.PnlList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PnlList.Controls.Add(this.GvChangeOrders);
            this.PnlList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PnlList.Font = new System.Drawing.Font("メイリオ", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.PnlList.Location = new System.Drawing.Point(0, 0);
            this.PnlList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.PnlList.Name = "PnlList";
            this.PnlList.Size = new System.Drawing.Size(1184, 761);
            this.PnlList.TabIndex = 98;
            // 
            // GvChangeOrders
            // 
            this.GvChangeOrders.AllowUserToAddRows = false;
            this.GvChangeOrders.AllowUserToDeleteRows = false;
            this.GvChangeOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvChangeOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GvChangeOrders.Location = new System.Drawing.Point(0, 0);
            this.GvChangeOrders.Name = "GvChangeOrders";
            this.GvChangeOrders.ReadOnly = true;
            this.GvChangeOrders.RowTemplate.Height = 21;
            this.GvChangeOrders.Size = new System.Drawing.Size(1182, 759);
            this.GvChangeOrders.TabIndex = 0;
            // 
            // FrmOrderChangeStatusSelectItem
            // 
            this.AcceptButton = this.BtnClose;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.ControlBox = false;
            this.Controls.Add(this.PnlBtnChk);
            this.Controls.Add(this.PnlList);
            this.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.Name = "FrmOrderChangeStatusSelectItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "ステータス一括変更";
            this.PnlBtnChk.ResumeLayout(false);
            this.PnlBtnChk.PerformLayout();
            this.PnlList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GvChangeOrders)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel PnlBtnChk;
        private System.Windows.Forms.CheckBox ChkSelectAll;
        private System.Windows.Forms.Button BtnClose;
        private System.Windows.Forms.Button BtnChangeStatus;
        private System.Windows.Forms.Panel PnlList;
        private System.Windows.Forms.DataGridView GvChangeOrders;
    }
}