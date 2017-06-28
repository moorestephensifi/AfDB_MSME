namespace SEnPA
{
    partial class AddUser
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.chkAutoUsername = new System.Windows.Forms.CheckBox();
            this.chkDefaultPassword = new System.Windows.Forms.CheckBox();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.labelControl9 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl10 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl4 = new DevExpress.XtraEditors.GroupControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl11 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl12 = new DevExpress.XtraEditors.LabelControl();
            this.txtName = new System.Windows.Forms.TextBox();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.txtEmail = new System.Windows.Forms.TextBox();
            this.txtMobile = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtConfirm = new System.Windows.Forms.TextBox();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.chkLocked = new System.Windows.Forms.CheckBox();
            this.chkPasswordExpires = new System.Windows.Forms.CheckBox();
            this.cmbRoleGroups = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).BeginInit();
            this.groupControl4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(578, 52);
            this.panelControl1.TabIndex = 0;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(12, 110);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 13);
            this.labelControl1.TabIndex = 1;
            this.labelControl1.Text = "Username";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(12, 32);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(51, 13);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "First Name";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(12, 74);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(42, 13);
            this.labelControl3.TabIndex = 3;
            this.labelControl3.Text = "Surname";
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(12, 147);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(67, 13);
            this.labelControl4.TabIndex = 4;
            this.labelControl4.Text = "Default Group";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.cmbRoleGroups);
            this.groupControl1.Controls.Add(this.txtUsername);
            this.groupControl1.Controls.Add(this.txtSurname);
            this.groupControl1.Controls.Add(this.txtName);
            this.groupControl1.Controls.Add(this.labelControl4);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Location = new System.Drawing.Point(0, 75);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(302, 182);
            this.groupControl1.TabIndex = 7;
            this.groupControl1.Text = "User ID";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.chkPasswordExpires);
            this.groupControl2.Controls.Add(this.chkLocked);
            this.groupControl2.Controls.Add(this.chkActive);
            this.groupControl2.Location = new System.Drawing.Point(312, 75);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(254, 128);
            this.groupControl2.TabIndex = 8;
            this.groupControl2.Text = "Properties";
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.chkDefaultPassword);
            this.panelControl2.Controls.Add(this.chkAutoUsername);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl2.Location = new System.Drawing.Point(0, 375);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(578, 30);
            this.panelControl2.TabIndex = 9;
            // 
            // chkAutoUsername
            // 
            this.chkAutoUsername.AutoSize = true;
            this.chkAutoUsername.Checked = true;
            this.chkAutoUsername.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoUsername.Location = new System.Drawing.Point(8, 7);
            this.chkAutoUsername.Name = "chkAutoUsername";
            this.chkAutoUsername.Size = new System.Drawing.Size(100, 17);
            this.chkAutoUsername.TabIndex = 0;
            this.chkAutoUsername.Text = "Auto Username";
            this.chkAutoUsername.UseVisualStyleBackColor = true;
            this.chkAutoUsername.CheckedChanged += new System.EventHandler(this.chkAutoUsername_CheckedChanged);
            // 
            // chkDefaultPassword
            // 
            this.chkDefaultPassword.AutoSize = true;
            this.chkDefaultPassword.Checked = true;
            this.chkDefaultPassword.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDefaultPassword.Location = new System.Drawing.Point(114, 7);
            this.chkDefaultPassword.Name = "chkDefaultPassword";
            this.chkDefaultPassword.Size = new System.Drawing.Size(110, 17);
            this.chkDefaultPassword.TabIndex = 1;
            this.chkDefaultPassword.Text = "Default Password";
            this.chkDefaultPassword.UseVisualStyleBackColor = true;
            this.chkDefaultPassword.CheckedChanged += new System.EventHandler(this.chkDefaultPassword_CheckedChanged);
            // 
            // groupControl3
            // 
            this.groupControl3.Controls.Add(this.txtMobile);
            this.groupControl3.Controls.Add(this.txtEmail);
            this.groupControl3.Controls.Add(this.labelControl10);
            this.groupControl3.Controls.Add(this.labelControl9);
            this.groupControl3.Location = new System.Drawing.Point(0, 269);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(302, 100);
            this.groupControl3.TabIndex = 10;
            this.groupControl3.Text = "Contact Details";
            this.groupControl3.Paint += new System.Windows.Forms.PaintEventHandler(this.groupControl3_Paint);
            // 
            // labelControl9
            // 
            this.labelControl9.Location = new System.Drawing.Point(12, 34);
            this.labelControl9.Name = "labelControl9";
            this.labelControl9.Size = new System.Drawing.Size(66, 13);
            this.labelControl9.TabIndex = 5;
            this.labelControl9.Text = "Email Address";
            // 
            // labelControl10
            // 
            this.labelControl10.Location = new System.Drawing.Point(12, 69);
            this.labelControl10.Name = "labelControl10";
            this.labelControl10.Size = new System.Drawing.Size(70, 13);
            this.labelControl10.TabIndex = 6;
            this.labelControl10.Text = "Mobile Number";
            // 
            // groupControl4
            // 
            this.groupControl4.Controls.Add(this.txtConfirm);
            this.groupControl4.Controls.Add(this.txtPassword);
            this.groupControl4.Controls.Add(this.labelControl12);
            this.groupControl4.Controls.Add(this.labelControl11);
            this.groupControl4.Location = new System.Drawing.Point(312, 222);
            this.groupControl4.Name = "groupControl4";
            this.groupControl4.Size = new System.Drawing.Size(254, 100);
            this.groupControl4.TabIndex = 11;
            this.groupControl4.Text = "Password";
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.Image = global::SEnPA.Properties.Resources.sign_add;
            this.btnSave.Location = new System.Drawing.Point(425, 338);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(61, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.ImageOptions.Image = global::SEnPA.Properties.Resources.sign_deny;
            this.btnCancel.Location = new System.Drawing.Point(504, 338);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(62, 23);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // labelControl11
            // 
            this.labelControl11.Location = new System.Drawing.Point(12, 34);
            this.labelControl11.Name = "labelControl11";
            this.labelControl11.Size = new System.Drawing.Size(46, 13);
            this.labelControl11.TabIndex = 6;
            this.labelControl11.Text = "Password";
            // 
            // labelControl12
            // 
            this.labelControl12.Location = new System.Drawing.Point(12, 74);
            this.labelControl12.Name = "labelControl12";
            this.labelControl12.Size = new System.Drawing.Size(86, 13);
            this.labelControl12.TabIndex = 7;
            this.labelControl12.Text = "Confirm Password";
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(91, 29);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(193, 21);
            this.txtName.TabIndex = 5;
            this.txtName.TextChanged += new System.EventHandler(this.txtName_TextChanged);
            // 
            // txtSurname
            // 
            this.txtSurname.Location = new System.Drawing.Point(91, 71);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(193, 21);
            this.txtSurname.TabIndex = 6;
            this.txtSurname.TextChanged += new System.EventHandler(this.txtSurname_TextChanged);
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(91, 107);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Size = new System.Drawing.Size(193, 21);
            this.txtUsername.TabIndex = 7;
            // 
            // txtEmail
            // 
            this.txtEmail.Location = new System.Drawing.Point(91, 31);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.Size = new System.Drawing.Size(193, 21);
            this.txtEmail.TabIndex = 8;
            // 
            // txtMobile
            // 
            this.txtMobile.Location = new System.Drawing.Point(91, 66);
            this.txtMobile.Name = "txtMobile";
            this.txtMobile.Size = new System.Drawing.Size(193, 21);
            this.txtMobile.TabIndex = 9;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(104, 30);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(146, 21);
            this.txtPassword.TabIndex = 10;
            // 
            // txtConfirm
            // 
            this.txtConfirm.Location = new System.Drawing.Point(104, 66);
            this.txtConfirm.Name = "txtConfirm";
            this.txtConfirm.Size = new System.Drawing.Size(146, 21);
            this.txtConfirm.TabIndex = 11;
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Checked = true;
            this.chkActive.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkActive.Location = new System.Drawing.Point(21, 31);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(56, 17);
            this.chkActive.TabIndex = 9;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // chkLocked
            // 
            this.chkLocked.AutoSize = true;
            this.chkLocked.Location = new System.Drawing.Point(21, 69);
            this.chkLocked.Name = "chkLocked";
            this.chkLocked.Size = new System.Drawing.Size(59, 17);
            this.chkLocked.TabIndex = 10;
            this.chkLocked.Text = "Locked";
            this.chkLocked.UseVisualStyleBackColor = true;
            // 
            // chkPasswordExpires
            // 
            this.chkPasswordExpires.AutoSize = true;
            this.chkPasswordExpires.Location = new System.Drawing.Point(21, 103);
            this.chkPasswordExpires.Name = "chkPasswordExpires";
            this.chkPasswordExpires.Size = new System.Drawing.Size(110, 17);
            this.chkPasswordExpires.TabIndex = 11;
            this.chkPasswordExpires.Text = "Password Expires";
            this.chkPasswordExpires.UseVisualStyleBackColor = true;
            // 
            // cmbRoleGroups
            // 
            this.cmbRoleGroups.FormattingEnabled = true;
            this.cmbRoleGroups.Location = new System.Drawing.Point(91, 144);
            this.cmbRoleGroups.Name = "cmbRoleGroups";
            this.cmbRoleGroups.Size = new System.Drawing.Size(193, 21);
            this.cmbRoleGroups.TabIndex = 8;
            // 
            // AddUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 405);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupControl4);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.panelControl2);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.panelControl1);
            this.Name = "AddUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create New User";
            this.Load += new System.EventHandler(this.AddUser_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl4)).EndInit();
            this.groupControl4.ResumeLayout(false);
            this.groupControl4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.CheckBox chkAutoUsername;
        private System.Windows.Forms.CheckBox chkDefaultPassword;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.LabelControl labelControl10;
        private DevExpress.XtraEditors.LabelControl labelControl9;
        private DevExpress.XtraEditors.GroupControl groupControl4;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.LabelControl labelControl12;
        private DevExpress.XtraEditors.LabelControl labelControl11;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.TextBox txtMobile;
        private System.Windows.Forms.TextBox txtEmail;
        private System.Windows.Forms.TextBox txtConfirm;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.CheckBox chkPasswordExpires;
        private System.Windows.Forms.CheckBox chkLocked;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.ComboBox cmbRoleGroups;
    }
}