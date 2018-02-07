namespace SBFA
{
    partial class DeclineDisbursement
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeclineDisbursement));
            this.panel81 = new System.Windows.Forms.Panel();
            this.label67 = new System.Windows.Forms.Label();
            this.separatorControl5 = new DevExpress.XtraEditors.SeparatorControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label98 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.btnPay = new DevExpress.XtraEditors.SimpleButton();
            this.panel81.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl5)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel81
            // 
            this.panel81.BackColor = System.Drawing.Color.LightSeaGreen;
            this.panel81.Controls.Add(this.label67);
            this.panel81.Location = new System.Drawing.Point(1, 1);
            this.panel81.Name = "panel81";
            this.panel81.Size = new System.Drawing.Size(313, 40);
            this.panel81.TabIndex = 65;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.ForeColor = System.Drawing.Color.White;
            this.label67.Location = new System.Drawing.Point(134, 9);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(176, 21);
            this.label67.TabIndex = 31;
            this.label67.Text = "Declining Disbursement";
            // 
            // separatorControl5
            // 
            this.separatorControl5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.separatorControl5.Location = new System.Drawing.Point(1, 47);
            this.separatorControl5.Name = "separatorControl5";
            this.separatorControl5.Size = new System.Drawing.Size(493, 23);
            this.separatorControl5.TabIndex = 64;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label98);
            this.panel1.Controls.Add(this.label91);
            this.panel1.Controls.Add(this.panel81);
            this.panel1.Controls.Add(this.separatorControl5);
            this.panel1.Location = new System.Drawing.Point(-1, -1);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(502, 263);
            this.panel1.TabIndex = 66;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(9, 141);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(480, 111);
            this.textBox1.TabIndex = 69;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(401, 35);
            this.label1.TabIndex = 68;
            this.label1.Text = "You have chosen to decline this disbursement. Please give a reason for this below" +
    ".";
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label98.Location = new System.Drawing.Point(263, 62);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(91, 17);
            this.label98.TabIndex = 66;
            this.label98.Text = "Reference No:";
            // 
            // label91
            // 
            this.label91.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label91.Location = new System.Drawing.Point(359, 62);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(135, 17);
            this.label91.TabIndex = 67;
            this.label91.Text = "Process Status";
            this.label91.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnPay
            // 
            this.btnPay.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPay.Appearance.ForeColor = System.Drawing.Color.Red;
            this.btnPay.Appearance.Options.UseFont = true;
            this.btnPay.Appearance.Options.UseForeColor = true;
            this.btnPay.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.btnPay.Location = new System.Drawing.Point(384, 271);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(104, 38);
            this.btnPay.TabIndex = 67;
            this.btnPay.Text = "Decline";
            // 
            // DeclineDisbursement
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 318);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DeclineDisbursement";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.DeclineDisbursement_Load);
            this.panel81.ResumeLayout(false);
            this.panel81.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl5)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel81;
        private System.Windows.Forms.Label label67;
        private DevExpress.XtraEditors.SeparatorControl separatorControl5;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.Label label91;
        private DevExpress.XtraEditors.SimpleButton btnPay;
    }
}