namespace SBFA
{
    partial class ManageStages
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManageStages));
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Stages");
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.btnAutoDocuments = new DevExpress.XtraEditors.SimpleButton();
            this.panel75 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.separatorControl11 = new DevExpress.XtraEditors.SeparatorControl();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.treeStages = new System.Windows.Forms.TreeView();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.cmbDocType = new System.Windows.Forms.ComboBox();
            this.btnRemove = new DevExpress.XtraEditors.SimpleButton();
            this.chkRequired = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnDocs = new DevExpress.XtraEditors.SimpleButton();
            this.lstDocuments = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.cmbTemplate = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbEmail = new System.Windows.Forms.ComboBox();
            this.cmbSMS = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.chkReco = new System.Windows.Forms.CheckBox();
            this.chkSite = new System.Windows.Forms.CheckBox();
            this.chkPay = new System.Windows.Forms.CheckBox();
            this.chkDoc = new System.Windows.Forms.CheckBox();
            this.chkOptional = new System.Windows.Forms.CheckBox();
            this.cmbAssign = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.panel75.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.Controls.Add(this.btnAutoDocuments);
            this.panelControl1.Controls.Add(this.panel75);
            this.panelControl1.Controls.Add(this.label46);
            this.panelControl1.Controls.Add(this.btnSave);
            this.panelControl1.Controls.Add(this.separatorControl11);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(990, 79);
            this.panelControl1.TabIndex = 0;
            // 
            // btnAutoDocuments
            // 
            this.btnAutoDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAutoDocuments.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAutoDocuments.ImageOptions.Image")));
            this.btnAutoDocuments.Location = new System.Drawing.Point(842, 12);
            this.btnAutoDocuments.Name = "btnAutoDocuments";
            this.btnAutoDocuments.Size = new System.Drawing.Size(127, 44);
            this.btnAutoDocuments.TabIndex = 57;
            this.btnAutoDocuments.Text = "More Documents";
            this.btnAutoDocuments.Click += new System.EventHandler(this.btnAutoDocuments_Click);
            // 
            // panel75
            // 
            this.panel75.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel75.Controls.Add(this.label20);
            this.panel75.Location = new System.Drawing.Point(2, 0);
            this.panel75.Name = "panel75";
            this.panel75.Size = new System.Drawing.Size(301, 33);
            this.panel75.TabIndex = 56;
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(170, 5);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(128, 21);
            this.label20.TabIndex = 0;
            this.label20.Text = "Manage Stages";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label46.Location = new System.Drawing.Point(7, 36);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(296, 16);
            this.label46.TabIndex = 55;
            this.label46.Text = "Create, edit and configure workflow stage settings";
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.Location = new System.Drawing.Point(752, 12);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(84, 44);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // separatorControl11
            // 
            this.separatorControl11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.separatorControl11.Location = new System.Drawing.Point(2, 57);
            this.separatorControl11.Name = "separatorControl11";
            this.separatorControl11.Size = new System.Drawing.Size(985, 23);
            this.separatorControl11.TabIndex = 51;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 79);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.treeStages);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel2.Controls.Add(this.lstDocuments);
            this.splitContainerControl1.Panel2.Controls.Add(this.panelControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(990, 540);
            this.splitContainerControl1.SplitterPosition = 206;
            this.splitContainerControl1.TabIndex = 1;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // treeStages
            // 
            this.treeStages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeStages.Location = new System.Drawing.Point(0, 0);
            this.treeStages.Name = "treeStages";
            treeNode1.Name = "workStages";
            treeNode1.Text = "Stages";
            this.treeStages.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1});
            this.treeStages.Size = new System.Drawing.Size(206, 540);
            this.treeStages.TabIndex = 0;
            this.treeStages.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeStages_AfterSelect);
            // 
            // groupControl1
            // 
            this.groupControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupControl1.Controls.Add(this.cmbDocType);
            this.groupControl1.Controls.Add(this.btnRemove);
            this.groupControl1.Controls.Add(this.chkRequired);
            this.groupControl1.Controls.Add(this.label5);
            this.groupControl1.Controls.Add(this.btnDocs);
            this.groupControl1.Location = new System.Drawing.Point(470, 277);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(306, 265);
            this.groupControl1.TabIndex = 21;
            this.groupControl1.Text = "Manage Document";
            // 
            // cmbDocType
            // 
            this.cmbDocType.FormattingEnabled = true;
            this.cmbDocType.Location = new System.Drawing.Point(71, 34);
            this.cmbDocType.Name = "cmbDocType";
            this.cmbDocType.Size = new System.Drawing.Size(230, 21);
            this.cmbDocType.TabIndex = 26;
            // 
            // btnRemove
            // 
            this.btnRemove.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRemove.ImageOptions.Image")));
            this.btnRemove.Location = new System.Drawing.Point(161, 130);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(82, 38);
            this.btnRemove.TabIndex = 24;
            this.btnRemove.Text = "Remove";
            // 
            // chkRequired
            // 
            this.chkRequired.AutoSize = true;
            this.chkRequired.Location = new System.Drawing.Point(71, 89);
            this.chkRequired.Name = "chkRequired";
            this.chkRequired.Size = new System.Drawing.Size(69, 17);
            this.chkRequired.TabIndex = 22;
            this.chkRequired.Text = "Required";
            this.chkRequired.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 37);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(31, 13);
            this.label5.TabIndex = 21;
            this.label5.Text = "Type";
            // 
            // btnDocs
            // 
            this.btnDocs.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDocs.ImageOptions.Image")));
            this.btnDocs.Location = new System.Drawing.Point(71, 130);
            this.btnDocs.Name = "btnDocs";
            this.btnDocs.Size = new System.Drawing.Size(76, 38);
            this.btnDocs.TabIndex = 20;
            this.btnDocs.Text = "Save";
            this.btnDocs.Click += new System.EventHandler(this.btnDocs_Click);
            // 
            // lstDocuments
            // 
            this.lstDocuments.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstDocuments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lstDocuments.FullRowSelect = true;
            this.lstDocuments.GridLines = true;
            this.lstDocuments.Location = new System.Drawing.Point(0, 277);
            this.lstDocuments.Name = "lstDocuments";
            this.lstDocuments.Size = new System.Drawing.Size(470, 258);
            this.lstDocuments.TabIndex = 1;
            this.lstDocuments.UseCompatibleStateImageBehavior = false;
            this.lstDocuments.View = System.Windows.Forms.View.Details;
            this.lstDocuments.SelectedIndexChanged += new System.EventHandler(this.lstDocuments_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Id";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Document";
            this.columnHeader2.Width = 191;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Required";
            this.columnHeader3.Width = 91;
            // 
            // panelControl2
            // 
            this.panelControl2.Controls.Add(this.cmbTemplate);
            this.panelControl2.Controls.Add(this.label8);
            this.panelControl2.Controls.Add(this.cmbEmail);
            this.panelControl2.Controls.Add(this.cmbSMS);
            this.panelControl2.Controls.Add(this.label6);
            this.panelControl2.Controls.Add(this.label7);
            this.panelControl2.Controls.Add(this.cmbGroup);
            this.panelControl2.Controls.Add(this.chkReco);
            this.panelControl2.Controls.Add(this.chkSite);
            this.panelControl2.Controls.Add(this.chkPay);
            this.panelControl2.Controls.Add(this.chkDoc);
            this.panelControl2.Controls.Add(this.chkOptional);
            this.panelControl2.Controls.Add(this.cmbAssign);
            this.panelControl2.Controls.Add(this.label1);
            this.panelControl2.Controls.Add(this.label4);
            this.panelControl2.Controls.Add(this.txtDescription);
            this.panelControl2.Controls.Add(this.txtName);
            this.panelControl2.Controls.Add(this.label3);
            this.panelControl2.Controls.Add(this.label2);
            this.panelControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl2.Location = new System.Drawing.Point(0, 0);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(772, 276);
            this.panelControl2.TabIndex = 0;
            // 
            // cmbTemplate
            // 
            this.cmbTemplate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbTemplate.FormattingEnabled = true;
            this.cmbTemplate.Location = new System.Drawing.Point(95, 201);
            this.cmbTemplate.Name = "cmbTemplate";
            this.cmbTemplate.Size = new System.Drawing.Size(242, 21);
            this.cmbTemplate.TabIndex = 50;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 204);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(51, 13);
            this.label8.TabIndex = 49;
            this.label8.Text = "Template";
            // 
            // cmbEmail
            // 
            this.cmbEmail.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbEmail.FormattingEnabled = true;
            this.cmbEmail.Location = new System.Drawing.Point(95, 155);
            this.cmbEmail.Name = "cmbEmail";
            this.cmbEmail.Size = new System.Drawing.Size(242, 21);
            this.cmbEmail.TabIndex = 48;
            // 
            // cmbSMS
            // 
            this.cmbSMS.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbSMS.FormattingEnabled = true;
            this.cmbSMS.Items.AddRange(new object[] {
            "Manual",
            "Auto"});
            this.cmbSMS.Location = new System.Drawing.Point(471, 155);
            this.cmbSMS.Name = "cmbSMS";
            this.cmbSMS.Size = new System.Drawing.Size(193, 21);
            this.cmbSMS.TabIndex = 47;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(399, 158);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(54, 13);
            this.label6.TabIndex = 46;
            this.label6.Text = "Send SMS";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 158);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 45;
            this.label7.Text = "Send Email";
            // 
            // cmbGroup
            // 
            this.cmbGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Location = new System.Drawing.Point(94, 114);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(242, 21);
            this.cmbGroup.TabIndex = 44;
            // 
            // chkReco
            // 
            this.chkReco.AutoSize = true;
            this.chkReco.Location = new System.Drawing.Point(566, 240);
            this.chkReco.Name = "chkReco";
            this.chkReco.Size = new System.Drawing.Size(113, 17);
            this.chkReco.TabIndex = 43;
            this.chkReco.Text = "Recommendations";
            this.chkReco.UseVisualStyleBackColor = true;
            // 
            // chkSite
            // 
            this.chkSite.AutoSize = true;
            this.chkSite.Location = new System.Drawing.Point(467, 240);
            this.chkSite.Name = "chkSite";
            this.chkSite.Size = new System.Drawing.Size(66, 17);
            this.chkSite.TabIndex = 42;
            this.chkSite.Text = "Site Visit";
            this.chkSite.UseVisualStyleBackColor = true;
            // 
            // chkPay
            // 
            this.chkPay.AutoSize = true;
            this.chkPay.Location = new System.Drawing.Point(333, 240);
            this.chkPay.Name = "chkPay";
            this.chkPay.Size = new System.Drawing.Size(108, 17);
            this.chkPay.TabIndex = 41;
            this.chkPay.Text = "Require Payment";
            this.chkPay.UseVisualStyleBackColor = true;
            // 
            // chkDoc
            // 
            this.chkDoc.AutoSize = true;
            this.chkDoc.Location = new System.Drawing.Point(196, 240);
            this.chkDoc.Name = "chkDoc";
            this.chkDoc.Size = new System.Drawing.Size(119, 17);
            this.chkDoc.TabIndex = 40;
            this.chkDoc.Text = "Require Documents";
            this.chkDoc.UseVisualStyleBackColor = true;
            // 
            // chkOptional
            // 
            this.chkOptional.AutoSize = true;
            this.chkOptional.Location = new System.Drawing.Point(94, 240);
            this.chkOptional.Name = "chkOptional";
            this.chkOptional.Size = new System.Drawing.Size(66, 17);
            this.chkOptional.TabIndex = 39;
            this.chkOptional.Text = "Optional";
            this.chkOptional.UseVisualStyleBackColor = true;
            // 
            // cmbAssign
            // 
            this.cmbAssign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbAssign.FormattingEnabled = true;
            this.cmbAssign.Items.AddRange(new object[] {
            "Manual",
            "Auto"});
            this.cmbAssign.Location = new System.Drawing.Point(471, 114);
            this.cmbAssign.Name = "cmbAssign";
            this.cmbAssign.Size = new System.Drawing.Size(193, 21);
            this.cmbAssign.TabIndex = 38;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(398, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "Assign mode";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 117);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(36, 13);
            this.label4.TabIndex = 36;
            this.label4.Text = "Group";
            // 
            // txtDescription
            // 
            this.txtDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDescription.Location = new System.Drawing.Point(94, 45);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(570, 50);
            this.txtDescription.TabIndex = 35;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(94, 7);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(570, 21);
            this.txtName.TabIndex = 34;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 33;
            this.label3.Text = "Description";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 32;
            this.label2.Text = "Name";
            // 
            // ManageStages
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(990, 619);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.panelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ManageStages";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.ManageStages_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.panel75.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            this.panelControl2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private System.Windows.Forms.TreeView treeStages;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private System.Windows.Forms.ListView lstDocuments;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnDocs;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private System.Windows.Forms.CheckBox chkRequired;
        private System.Windows.Forms.Label label5;
        private DevExpress.XtraEditors.SimpleButton btnRemove;
        private DevExpress.XtraEditors.SeparatorControl separatorControl11;
        private System.Windows.Forms.ComboBox cmbDocType;
        private DevExpress.XtraEditors.SimpleButton btnAutoDocuments;
        private System.Windows.Forms.Panel panel75;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.ComboBox cmbTemplate;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbEmail;
        private System.Windows.Forms.ComboBox cmbSMS;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.CheckBox chkReco;
        private System.Windows.Forms.CheckBox chkSite;
        private System.Windows.Forms.CheckBox chkPay;
        private System.Windows.Forms.CheckBox chkDoc;
        private System.Windows.Forms.CheckBox chkOptional;
        private System.Windows.Forms.ComboBox cmbAssign;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}