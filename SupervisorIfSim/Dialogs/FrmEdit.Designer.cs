
namespace SupervisorIfSim.Dialogs
{
    partial class FrmEdit
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
            this.pnlPreviewFormula = new System.Windows.Forms.Panel();
            this.GvFormula = new System.Windows.Forms.DataGridView();
            this.pnlDetailFormula = new System.Windows.Forms.Panel();
            this.BtnRegist = new System.Windows.Forms.Button();
            this.PnlPrdIsprefilled = new System.Windows.Forms.Panel();
            this.RdbPrdIsprefilledTrue = new System.Windows.Forms.RadioButton();
            this.RdbPrdIsprefilledFalse = new System.Windows.Forms.RadioButton();
            this.PnlPrdUm = new System.Windows.Forms.Panel();
            this.RdbPrdUmCc = new System.Windows.Forms.RadioButton();
            this.RdbPrdUmGram = new System.Windows.Forms.RadioButton();
            this.PnlPrdStatus = new System.Windows.Forms.Panel();
            this.RdbPrdStatusError = new System.Windows.Forms.RadioButton();
            this.RdbPrdStatusCompleted = new System.Windows.Forms.RadioButton();
            this.RdbPrdStatusUndischarged = new System.Windows.Forms.RadioButton();
            this.label39 = new System.Windows.Forms.Label();
            this.TxtPrdPrefilledQty = new System.Windows.Forms.TextBox();
            this.TxtPrdNum = new System.Windows.Forms.TextBox();
            this.TxtPrdPriority = new System.Windows.Forms.TextBox();
            this.TxtPrdEndDisp = new System.Windows.Forms.TextBox();
            this.TxtPrdStartDisp = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.TxtPrdQtyDisp = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.TxtPrdQtyReq = new System.Windows.Forms.TextBox();
            this.TxtPrdSpecificGravity = new System.Windows.Forms.TextBox();
            this.TxtPrdDesc = new System.Windows.Forms.TextBox();
            this.TxtPrdCode = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.TxtPrdTimeInserted = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.pnlPreviewFormula.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GvFormula)).BeginInit();
            this.pnlDetailFormula.SuspendLayout();
            this.PnlPrdIsprefilled.SuspendLayout();
            this.PnlPrdUm.SuspendLayout();
            this.PnlPrdStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPreviewFormula
            // 
            this.pnlPreviewFormula.Controls.Add(this.GvFormula);
            this.pnlPreviewFormula.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlPreviewFormula.Location = new System.Drawing.Point(0, 0);
            this.pnlPreviewFormula.Name = "pnlPreviewFormula";
            this.pnlPreviewFormula.Size = new System.Drawing.Size(300, 423);
            this.pnlPreviewFormula.TabIndex = 49;
            // 
            // GvFormula
            // 
            this.GvFormula.AllowUserToAddRows = false;
            this.GvFormula.AllowUserToDeleteRows = false;
            this.GvFormula.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GvFormula.Location = new System.Drawing.Point(0, 0);
            this.GvFormula.Name = "GvFormula";
            this.GvFormula.ReadOnly = true;
            this.GvFormula.RowTemplate.Height = 21;
            this.GvFormula.Size = new System.Drawing.Size(242, 302);
            this.GvFormula.TabIndex = 0;
            // 
            // pnlDetailFormula
            // 
            this.pnlDetailFormula.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlDetailFormula.Controls.Add(this.BtnRegist);
            this.pnlDetailFormula.Controls.Add(this.PnlPrdIsprefilled);
            this.pnlDetailFormula.Controls.Add(this.PnlPrdUm);
            this.pnlDetailFormula.Controls.Add(this.PnlPrdStatus);
            this.pnlDetailFormula.Controls.Add(this.label39);
            this.pnlDetailFormula.Controls.Add(this.TxtPrdPrefilledQty);
            this.pnlDetailFormula.Controls.Add(this.TxtPrdNum);
            this.pnlDetailFormula.Controls.Add(this.TxtPrdPriority);
            this.pnlDetailFormula.Controls.Add(this.TxtPrdEndDisp);
            this.pnlDetailFormula.Controls.Add(this.TxtPrdStartDisp);
            this.pnlDetailFormula.Controls.Add(this.label17);
            this.pnlDetailFormula.Controls.Add(this.label18);
            this.pnlDetailFormula.Controls.Add(this.label19);
            this.pnlDetailFormula.Controls.Add(this.label20);
            this.pnlDetailFormula.Controls.Add(this.label21);
            this.pnlDetailFormula.Controls.Add(this.label22);
            this.pnlDetailFormula.Controls.Add(this.TxtPrdQtyDisp);
            this.pnlDetailFormula.Controls.Add(this.label23);
            this.pnlDetailFormula.Controls.Add(this.TxtPrdQtyReq);
            this.pnlDetailFormula.Controls.Add(this.TxtPrdSpecificGravity);
            this.pnlDetailFormula.Controls.Add(this.TxtPrdDesc);
            this.pnlDetailFormula.Controls.Add(this.TxtPrdCode);
            this.pnlDetailFormula.Controls.Add(this.label11);
            this.pnlDetailFormula.Controls.Add(this.label12);
            this.pnlDetailFormula.Controls.Add(this.label13);
            this.pnlDetailFormula.Controls.Add(this.label14);
            this.pnlDetailFormula.Controls.Add(this.label15);
            this.pnlDetailFormula.Controls.Add(this.label16);
            this.pnlDetailFormula.Controls.Add(this.TxtPrdTimeInserted);
            this.pnlDetailFormula.Controls.Add(this.label9);
            this.pnlDetailFormula.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetailFormula.Location = new System.Drawing.Point(300, 0);
            this.pnlDetailFormula.Name = "pnlDetailFormula";
            this.pnlDetailFormula.Size = new System.Drawing.Size(382, 423);
            this.pnlDetailFormula.TabIndex = 50;
            // 
            // BtnRegist
            // 
            this.BtnRegist.Location = new System.Drawing.Point(275, 10);
            this.BtnRegist.Name = "BtnRegist";
            this.BtnRegist.Size = new System.Drawing.Size(98, 35);
            this.BtnRegist.TabIndex = 75;
            this.BtnRegist.Text = "登録";
            this.BtnRegist.UseVisualStyleBackColor = true;
            // 
            // PnlPrdIsprefilled
            // 
            this.PnlPrdIsprefilled.Controls.Add(this.RdbPrdIsprefilledTrue);
            this.PnlPrdIsprefilled.Controls.Add(this.RdbPrdIsprefilledFalse);
            this.PnlPrdIsprefilled.Location = new System.Drawing.Point(159, 368);
            this.PnlPrdIsprefilled.Name = "PnlPrdIsprefilled";
            this.PnlPrdIsprefilled.Size = new System.Drawing.Size(220, 25);
            this.PnlPrdIsprefilled.TabIndex = 72;
            // 
            // RdbPrdIsprefilledTrue
            // 
            this.RdbPrdIsprefilledTrue.AutoSize = true;
            this.RdbPrdIsprefilledTrue.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RdbPrdIsprefilledTrue.Location = new System.Drawing.Point(76, 1);
            this.RdbPrdIsprefilledTrue.Name = "RdbPrdIsprefilledTrue";
            this.RdbPrdIsprefilledTrue.Size = new System.Drawing.Size(50, 22);
            this.RdbPrdIsprefilledTrue.TabIndex = 1;
            this.RdbPrdIsprefilledTrue.TabStop = true;
            this.RdbPrdIsprefilledTrue.Text = "true";
            this.RdbPrdIsprefilledTrue.UseVisualStyleBackColor = true;
            // 
            // RdbPrdIsprefilledFalse
            // 
            this.RdbPrdIsprefilledFalse.AutoSize = true;
            this.RdbPrdIsprefilledFalse.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RdbPrdIsprefilledFalse.Location = new System.Drawing.Point(3, 1);
            this.RdbPrdIsprefilledFalse.Name = "RdbPrdIsprefilledFalse";
            this.RdbPrdIsprefilledFalse.Size = new System.Drawing.Size(53, 22);
            this.RdbPrdIsprefilledFalse.TabIndex = 0;
            this.RdbPrdIsprefilledFalse.TabStop = true;
            this.RdbPrdIsprefilledFalse.Text = "false";
            this.RdbPrdIsprefilledFalse.UseVisualStyleBackColor = true;
            // 
            // PnlPrdUm
            // 
            this.PnlPrdUm.Controls.Add(this.RdbPrdUmCc);
            this.PnlPrdUm.Controls.Add(this.RdbPrdUmGram);
            this.PnlPrdUm.Location = new System.Drawing.Point(159, 160);
            this.PnlPrdUm.Name = "PnlPrdUm";
            this.PnlPrdUm.Size = new System.Drawing.Size(220, 25);
            this.PnlPrdUm.TabIndex = 56;
            // 
            // RdbPrdUmCc
            // 
            this.RdbPrdUmCc.AutoSize = true;
            this.RdbPrdUmCc.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RdbPrdUmCc.Location = new System.Drawing.Point(76, 1);
            this.RdbPrdUmCc.Name = "RdbPrdUmCc";
            this.RdbPrdUmCc.Size = new System.Drawing.Size(50, 22);
            this.RdbPrdUmCc.TabIndex = 1;
            this.RdbPrdUmCc.TabStop = true;
            this.RdbPrdUmCc.Text = "ｃｃ";
            this.RdbPrdUmCc.UseVisualStyleBackColor = true;
            // 
            // RdbPrdUmGram
            // 
            this.RdbPrdUmGram.AutoSize = true;
            this.RdbPrdUmGram.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RdbPrdUmGram.Location = new System.Drawing.Point(3, 1);
            this.RdbPrdUmGram.Name = "RdbPrdUmGram";
            this.RdbPrdUmGram.Size = new System.Drawing.Size(62, 22);
            this.RdbPrdUmGram.TabIndex = 0;
            this.RdbPrdUmGram.TabStop = true;
            this.RdbPrdUmGram.Text = "グラム";
            this.RdbPrdUmGram.UseVisualStyleBackColor = true;
            // 
            // PnlPrdStatus
            // 
            this.PnlPrdStatus.Controls.Add(this.RdbPrdStatusError);
            this.PnlPrdStatus.Controls.Add(this.RdbPrdStatusCompleted);
            this.PnlPrdStatus.Controls.Add(this.RdbPrdStatusUndischarged);
            this.PnlPrdStatus.Location = new System.Drawing.Point(159, 82);
            this.PnlPrdStatus.Name = "PnlPrdStatus";
            this.PnlPrdStatus.Size = new System.Drawing.Size(220, 25);
            this.PnlPrdStatus.TabIndex = 50;
            // 
            // RdbPrdStatusError
            // 
            this.RdbPrdStatusError.AutoSize = true;
            this.RdbPrdStatusError.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RdbPrdStatusError.Location = new System.Drawing.Point(155, 1);
            this.RdbPrdStatusError.Name = "RdbPrdStatusError";
            this.RdbPrdStatusError.Size = new System.Drawing.Size(62, 22);
            this.RdbPrdStatusError.TabIndex = 2;
            this.RdbPrdStatusError.TabStop = true;
            this.RdbPrdStatusError.Text = "エラー";
            this.RdbPrdStatusError.UseVisualStyleBackColor = true;
            // 
            // RdbPrdStatusCompleted
            // 
            this.RdbPrdStatusCompleted.AutoSize = true;
            this.RdbPrdStatusCompleted.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RdbPrdStatusCompleted.Location = new System.Drawing.Point(71, 1);
            this.RdbPrdStatusCompleted.Name = "RdbPrdStatusCompleted";
            this.RdbPrdStatusCompleted.Size = new System.Drawing.Size(74, 22);
            this.RdbPrdStatusCompleted.TabIndex = 1;
            this.RdbPrdStatusCompleted.TabStop = true;
            this.RdbPrdStatusCompleted.Text = "吐出完了";
            this.RdbPrdStatusCompleted.UseVisualStyleBackColor = true;
            // 
            // RdbPrdStatusUndischarged
            // 
            this.RdbPrdStatusUndischarged.AutoSize = true;
            this.RdbPrdStatusUndischarged.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.RdbPrdStatusUndischarged.Location = new System.Drawing.Point(3, 1);
            this.RdbPrdStatusUndischarged.Name = "RdbPrdStatusUndischarged";
            this.RdbPrdStatusUndischarged.Size = new System.Drawing.Size(62, 22);
            this.RdbPrdStatusUndischarged.TabIndex = 0;
            this.RdbPrdStatusUndischarged.TabStop = true;
            this.RdbPrdStatusUndischarged.Text = "未吐出";
            this.RdbPrdStatusUndischarged.UseVisualStyleBackColor = true;
            // 
            // label39
            // 
            this.label39.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.label39.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label39.ForeColor = System.Drawing.Color.Black;
            this.label39.Location = new System.Drawing.Point(2, 3);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(367, 50);
            this.label39.TabIndex = 46;
            this.label39.Text = "■TB_FORMURA";
            this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtPrdPrefilledQty
            // 
            this.TxtPrdPrefilledQty.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtPrdPrefilledQty.Location = new System.Drawing.Point(159, 394);
            this.TxtPrdPrefilledQty.Name = "TxtPrdPrefilledQty";
            this.TxtPrdPrefilledQty.Size = new System.Drawing.Size(220, 25);
            this.TxtPrdPrefilledQty.TabIndex = 74;
            this.TxtPrdPrefilledQty.Text = "1234567890123456789012345";
            // 
            // TxtPrdNum
            // 
            this.TxtPrdNum.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtPrdNum.Location = new System.Drawing.Point(159, 342);
            this.TxtPrdNum.Name = "TxtPrdNum";
            this.TxtPrdNum.Size = new System.Drawing.Size(220, 25);
            this.TxtPrdNum.TabIndex = 70;
            this.TxtPrdNum.Text = "1234567890123456789012345";
            // 
            // TxtPrdPriority
            // 
            this.TxtPrdPriority.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtPrdPriority.Location = new System.Drawing.Point(159, 316);
            this.TxtPrdPriority.Name = "TxtPrdPriority";
            this.TxtPrdPriority.Size = new System.Drawing.Size(220, 25);
            this.TxtPrdPriority.TabIndex = 68;
            this.TxtPrdPriority.Text = "1234567890123456789012345";
            // 
            // TxtPrdEndDisp
            // 
            this.TxtPrdEndDisp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtPrdEndDisp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TxtPrdEndDisp.Location = new System.Drawing.Point(159, 290);
            this.TxtPrdEndDisp.Name = "TxtPrdEndDisp";
            this.TxtPrdEndDisp.Size = new System.Drawing.Size(220, 25);
            this.TxtPrdEndDisp.TabIndex = 66;
            this.TxtPrdEndDisp.Text = "1234567890123456789012345";
            // 
            // TxtPrdStartDisp
            // 
            this.TxtPrdStartDisp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtPrdStartDisp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TxtPrdStartDisp.Location = new System.Drawing.Point(159, 264);
            this.TxtPrdStartDisp.Name = "TxtPrdStartDisp";
            this.TxtPrdStartDisp.Size = new System.Drawing.Size(220, 25);
            this.TxtPrdStartDisp.TabIndex = 64;
            this.TxtPrdStartDisp.Text = "1234567890123456789012345";
            // 
            // label17
            // 
            this.label17.BackColor = System.Drawing.Color.Navy;
            this.label17.ForeColor = System.Drawing.Color.White;
            this.label17.Location = new System.Drawing.Point(2, 394);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(155, 25);
            this.label17.TabIndex = 73;
            this.label17.Text = "PRD_PREFILLED_QTY";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.BackColor = System.Drawing.Color.Navy;
            this.label18.ForeColor = System.Drawing.Color.White;
            this.label18.Location = new System.Drawing.Point(2, 368);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(155, 25);
            this.label18.TabIndex = 71;
            this.label18.Text = "PRD_ISPREFILLED";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.BackColor = System.Drawing.Color.Navy;
            this.label19.ForeColor = System.Drawing.Color.White;
            this.label19.Location = new System.Drawing.Point(2, 342);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(155, 25);
            this.label19.TabIndex = 69;
            this.label19.Text = "PRD_NUM";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.Navy;
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(2, 316);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(155, 25);
            this.label20.TabIndex = 67;
            this.label20.Text = "PRD_PRIORITY";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label21.ForeColor = System.Drawing.Color.White;
            this.label21.Location = new System.Drawing.Point(2, 290);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(155, 25);
            this.label21.TabIndex = 65;
            this.label21.Text = "PRD_END_DISP";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            this.label22.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label22.ForeColor = System.Drawing.Color.White;
            this.label22.Location = new System.Drawing.Point(2, 264);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(155, 25);
            this.label22.TabIndex = 63;
            this.label22.Text = "PRD_START_DISP";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtPrdQtyDisp
            // 
            this.TxtPrdQtyDisp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtPrdQtyDisp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.TxtPrdQtyDisp.Location = new System.Drawing.Point(159, 238);
            this.TxtPrdQtyDisp.Name = "TxtPrdQtyDisp";
            this.TxtPrdQtyDisp.Size = new System.Drawing.Size(220, 25);
            this.TxtPrdQtyDisp.TabIndex = 62;
            this.TxtPrdQtyDisp.Text = "1234567890123456789012345";
            // 
            // label23
            // 
            this.label23.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label23.ForeColor = System.Drawing.Color.White;
            this.label23.Location = new System.Drawing.Point(2, 238);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(155, 25);
            this.label23.TabIndex = 61;
            this.label23.Text = "PRD_QTY_DISP";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtPrdQtyReq
            // 
            this.TxtPrdQtyReq.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtPrdQtyReq.Location = new System.Drawing.Point(159, 212);
            this.TxtPrdQtyReq.Name = "TxtPrdQtyReq";
            this.TxtPrdQtyReq.Size = new System.Drawing.Size(220, 25);
            this.TxtPrdQtyReq.TabIndex = 60;
            this.TxtPrdQtyReq.Text = "1234567890123456789012345";
            // 
            // TxtPrdSpecificGravity
            // 
            this.TxtPrdSpecificGravity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtPrdSpecificGravity.Location = new System.Drawing.Point(159, 186);
            this.TxtPrdSpecificGravity.Name = "TxtPrdSpecificGravity";
            this.TxtPrdSpecificGravity.Size = new System.Drawing.Size(220, 25);
            this.TxtPrdSpecificGravity.TabIndex = 58;
            this.TxtPrdSpecificGravity.Text = "1234567890123456789012345";
            // 
            // TxtPrdDesc
            // 
            this.TxtPrdDesc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtPrdDesc.Enabled = false;
            this.TxtPrdDesc.Location = new System.Drawing.Point(159, 134);
            this.TxtPrdDesc.Name = "TxtPrdDesc";
            this.TxtPrdDesc.ReadOnly = true;
            this.TxtPrdDesc.Size = new System.Drawing.Size(220, 25);
            this.TxtPrdDesc.TabIndex = 54;
            this.TxtPrdDesc.Text = "1234567890123456789012345";
            // 
            // TxtPrdCode
            // 
            this.TxtPrdCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtPrdCode.Location = new System.Drawing.Point(159, 108);
            this.TxtPrdCode.Name = "TxtPrdCode";
            this.TxtPrdCode.Size = new System.Drawing.Size(220, 25);
            this.TxtPrdCode.TabIndex = 52;
            this.TxtPrdCode.Text = "1234567890123456789012345";
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.Color.Navy;
            this.label11.ForeColor = System.Drawing.Color.White;
            this.label11.Location = new System.Drawing.Point(2, 212);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(155, 25);
            this.label11.TabIndex = 59;
            this.label11.Text = "PRD_QTY_REQ";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.BackColor = System.Drawing.Color.Navy;
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(2, 186);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(155, 25);
            this.label12.TabIndex = 57;
            this.label12.Text = "PRD_SPECIFIC_GRAVITY";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.BackColor = System.Drawing.Color.Navy;
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(2, 160);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(155, 25);
            this.label13.TabIndex = 55;
            this.label13.Text = "UM";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.BackColor = System.Drawing.Color.Navy;
            this.label14.ForeColor = System.Drawing.Color.White;
            this.label14.Location = new System.Drawing.Point(2, 134);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(155, 25);
            this.label14.TabIndex = 53;
            this.label14.Text = "PRD_DESC";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.BackColor = System.Drawing.Color.Navy;
            this.label15.ForeColor = System.Drawing.Color.White;
            this.label15.Location = new System.Drawing.Point(2, 108);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(155, 25);
            this.label15.TabIndex = 51;
            this.label15.Text = "PRD_CODE";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label16
            // 
            this.label16.BackColor = System.Drawing.Color.Navy;
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(2, 82);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(155, 25);
            this.label16.TabIndex = 49;
            this.label16.Text = "PRD_STATUS";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // TxtPrdTimeInserted
            // 
            this.TxtPrdTimeInserted.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TxtPrdTimeInserted.Location = new System.Drawing.Point(159, 56);
            this.TxtPrdTimeInserted.Name = "TxtPrdTimeInserted";
            this.TxtPrdTimeInserted.Size = new System.Drawing.Size(220, 25);
            this.TxtPrdTimeInserted.TabIndex = 48;
            this.TxtPrdTimeInserted.Text = "1234567890123456789012345";
            // 
            // label9
            // 
            this.label9.BackColor = System.Drawing.Color.Navy;
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(2, 56);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(155, 25);
            this.label9.TabIndex = 47;
            this.label9.Text = "TIME_INSERTED";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FrmEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(682, 423);
            this.Controls.Add(this.pnlDetailFormula);
            this.Controls.Add(this.pnlPreviewFormula);
            this.Font = new System.Drawing.Font("メイリオ", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "FrmEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Supervisor I/F シミュレータ";
            this.pnlPreviewFormula.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GvFormula)).EndInit();
            this.pnlDetailFormula.ResumeLayout(false);
            this.pnlDetailFormula.PerformLayout();
            this.PnlPrdIsprefilled.ResumeLayout(false);
            this.PnlPrdIsprefilled.PerformLayout();
            this.PnlPrdUm.ResumeLayout(false);
            this.PnlPrdUm.PerformLayout();
            this.PnlPrdStatus.ResumeLayout(false);
            this.PnlPrdStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlPreviewFormula;
        private System.Windows.Forms.DataGridView GvFormula;
        private System.Windows.Forms.Panel pnlDetailFormula;
        private System.Windows.Forms.Panel PnlPrdIsprefilled;
        private System.Windows.Forms.RadioButton RdbPrdIsprefilledTrue;
        private System.Windows.Forms.RadioButton RdbPrdIsprefilledFalse;
        private System.Windows.Forms.Panel PnlPrdUm;
        private System.Windows.Forms.RadioButton RdbPrdUmCc;
        private System.Windows.Forms.RadioButton RdbPrdUmGram;
        private System.Windows.Forms.Panel PnlPrdStatus;
        private System.Windows.Forms.RadioButton RdbPrdStatusError;
        private System.Windows.Forms.RadioButton RdbPrdStatusCompleted;
        private System.Windows.Forms.RadioButton RdbPrdStatusUndischarged;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.TextBox TxtPrdPrefilledQty;
        private System.Windows.Forms.TextBox TxtPrdNum;
        private System.Windows.Forms.TextBox TxtPrdPriority;
        private System.Windows.Forms.TextBox TxtPrdEndDisp;
        private System.Windows.Forms.TextBox TxtPrdStartDisp;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox TxtPrdQtyDisp;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox TxtPrdQtyReq;
        private System.Windows.Forms.TextBox TxtPrdSpecificGravity;
        private System.Windows.Forms.TextBox TxtPrdDesc;
        private System.Windows.Forms.TextBox TxtPrdCode;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox TxtPrdTimeInserted;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button BtnRegist;
    }
}