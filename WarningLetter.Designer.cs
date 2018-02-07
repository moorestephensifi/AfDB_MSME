namespace SBFA
{
    partial class WarningLetter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WarningLetter));
            this.label1 = new System.Windows.Forms.Label();
            this.label98 = new System.Windows.Forms.Label();
            this.lblAccount = new System.Windows.Forms.Label();
            this.panel81 = new System.Windows.Forms.Panel();
            this.label67 = new System.Windows.Forms.Label();
            this.btnPay = new DevExpress.XtraEditors.SimpleButton();
            this.lblRemarks = new System.Windows.Forms.Label();
            this.chkRemark = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.separatorControl1 = new DevExpress.XtraEditors.SeparatorControl();
            this.lbltext = new System.Windows.Forms.Label();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.panel81.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 407);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(401, 35);
            this.label1.TabIndex = 74;
            this.label1.Text = "You have chosen to send a warning letter to the client";
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label98.Location = new System.Drawing.Point(316, 326);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(90, 17);
            this.label98.TabIndex = 72;
            this.label98.Text = "Next Warning:";
            // 
            // lblAccount
            // 
            this.lblAccount.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAccount.Location = new System.Drawing.Point(347, 326);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(135, 17);
            this.lblAccount.TabIndex = 73;
            this.lblAccount.Text = ".....";
            this.lblAccount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel81
            // 
            this.panel81.BackColor = System.Drawing.Color.LightSeaGreen;
            this.panel81.Controls.Add(this.label67);
            this.panel81.Location = new System.Drawing.Point(0, 0);
            this.panel81.Name = "panel81";
            this.panel81.Size = new System.Drawing.Size(313, 40);
            this.panel81.TabIndex = 71;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.ForeColor = System.Drawing.Color.White;
            this.label67.Location = new System.Drawing.Point(156, 7);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(154, 21);
            this.label67.TabIndex = 31;
            this.label67.Text = "Warning Notification";
            // 
            // btnPay
            // 
            this.btnPay.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPay.Appearance.ForeColor = System.Drawing.Color.Red;
            this.btnPay.Appearance.Options.UseFont = true;
            this.btnPay.Appearance.Options.UseForeColor = true;
            this.btnPay.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPay.ImageOptions.Image")));
            this.btnPay.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.btnPay.Location = new System.Drawing.Point(350, 403);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(147, 38);
            this.btnPay.TabIndex = 75;
            this.btnPay.Text = "Send Warning";
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // lblRemarks
            // 
            this.lblRemarks.AutoSize = true;
            this.lblRemarks.Location = new System.Drawing.Point(5, 215);
            this.lblRemarks.Name = "lblRemarks";
            this.lblRemarks.Size = new System.Drawing.Size(27, 13);
            this.lblRemarks.TabIndex = 76;
            this.lblRemarks.Text = ".....";
            // 
            // chkRemark
            // 
            this.chkRemark.AutoSize = true;
            this.chkRemark.Location = new System.Drawing.Point(117, 73);
            this.chkRemark.Name = "chkRemark";
            this.chkRemark.Size = new System.Drawing.Size(80, 17);
            this.chkRemark.TabIndex = 77;
            this.chkRemark.Text = "Responded";
            this.chkRemark.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 125);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 78;
            this.label2.Text = "Remark";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(117, 97);
            this.txtRemark.Multiline = true;
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(380, 75);
            this.txtRemark.TabIndex = 79;
            // 
            // separatorControl1
            // 
            this.separatorControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.separatorControl1.Location = new System.Drawing.Point(0, 300);
            this.separatorControl1.Name = "separatorControl1";
            this.separatorControl1.Size = new System.Drawing.Size(493, 23);
            this.separatorControl1.TabIndex = 80;
            // 
            // lbltext
            // 
            this.lbltext.AutoSize = true;
            this.lbltext.Location = new System.Drawing.Point(117, 47);
            this.lbltext.Name = "lbltext";
            this.lbltext.Size = new System.Drawing.Size(31, 13);
            this.lbltext.TabIndex = 81;
            this.lbltext.Text = "......";
            // 
            // simpleButton1
            // 
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.Location = new System.Drawing.Point(415, 178);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(78, 35);
            this.simpleButton1.TabIndex = 82;
            this.simpleButton1.Text = "Save";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // WarningLetter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(505, 453);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.lbltext);
            this.Controls.Add(this.separatorControl1);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkRemark);
            this.Controls.Add(this.lblRemarks);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label98);
            this.Controls.Add(this.lblAccount);
            this.Controls.Add(this.panel81);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WarningLetter";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.WarningLetter_Load);
            this.panel81.ResumeLayout(false);
            this.panel81.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.Panel panel81;
        private System.Windows.Forms.Label label67;
        private DevExpress.XtraEditors.SimpleButton btnPay;
        private System.Windows.Forms.Label lblRemarks;
        private System.Windows.Forms.CheckBox chkRemark;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtRemark;
        private DevExpress.XtraEditors.SeparatorControl separatorControl1;
        private System.Windows.Forms.Label lbltext;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}