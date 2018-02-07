namespace SBFA
{
    partial class UploadTemplates
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
            this.components = new System.ComponentModel.Container();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.panel75 = new System.Windows.Forms.Panel();
            this.label20 = new System.Windows.Forms.Label();
            this.label46 = new System.Windows.Forms.Label();
            this.separatorControl11 = new DevExpress.XtraEditors.SeparatorControl();
            this.txtName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnUpload = new DevExpress.XtraEditors.SimpleButton();
            this.gridTemplates = new DevExpress.XtraGrid.GridControl();
            this.gridViewTemplates = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.uploadDocuments = new System.Windows.Forms.OpenFileDialog();
            this.templateBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.colDocumentContentType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDocumentName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDocumentType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panel75.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTemplates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTemplates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.templateBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Manager = null;
            this.barDockControl1.Size = new System.Drawing.Size(589, 0);
            // 
            // barDockControl2
            // 
            this.barDockControl2.CausesValidation = false;
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl2.Location = new System.Drawing.Point(0, 0);
            this.barDockControl2.Manager = null;
            this.barDockControl2.Size = new System.Drawing.Size(589, 0);
            // 
            // panel75
            // 
            this.panel75.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.panel75.Controls.Add(this.label20);
            this.panel75.Location = new System.Drawing.Point(2, 2);
            this.panel75.Name = "panel75";
            this.panel75.Size = new System.Drawing.Size(301, 33);
            this.panel75.TabIndex = 60;
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.White;
            this.label20.Location = new System.Drawing.Point(126, 5);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(172, 21);
            this.label20.TabIndex = 0;
            this.label20.Text = "Manage Templates";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label46.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label46.Location = new System.Drawing.Point(6, 38);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(221, 16);
            this.label46.TabIndex = 59;
            this.label46.Text = "Create, edit and configure Templates";
            // 
            // separatorControl11
            // 
            this.separatorControl11.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.separatorControl11.Location = new System.Drawing.Point(1, 55);
            this.separatorControl11.Name = "separatorControl11";
            this.separatorControl11.Size = new System.Drawing.Size(584, 23);
            this.separatorControl11.TabIndex = 58;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(67, 75);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(510, 21);
            this.txtName.TabIndex = 61;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(31, 13);
            this.label1.TabIndex = 62;
            this.label1.Text = "Type";
            // 
            // btnUpload
            // 
            this.btnUpload.Location = new System.Drawing.Point(502, 102);
            this.btnUpload.Name = "btnUpload";
            this.btnUpload.Size = new System.Drawing.Size(75, 23);
            this.btnUpload.TabIndex = 63;
            this.btnUpload.Text = "Upload";
            this.btnUpload.Click += new System.EventHandler(this.btnUpload_Click);
            // 
            // gridTemplates
            // 
            this.gridTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridTemplates.DataSource = this.templateBindingSource;
            this.gridTemplates.Location = new System.Drawing.Point(2, 131);
            this.gridTemplates.MainView = this.gridViewTemplates;
            this.gridTemplates.Name = "gridTemplates";
            this.gridTemplates.Size = new System.Drawing.Size(583, 252);
            this.gridTemplates.TabIndex = 64;
            this.gridTemplates.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTemplates});
            // 
            // gridViewTemplates
            // 
            this.gridViewTemplates.Appearance.Row.BackColor = System.Drawing.Color.Gainsboro;
            this.gridViewTemplates.Appearance.Row.BackColor2 = System.Drawing.Color.White;
            this.gridViewTemplates.Appearance.Row.Options.UseBackColor = true;
            this.gridViewTemplates.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDocumentType,
            this.colDocumentName,
            this.colDocumentContentType});
            this.gridViewTemplates.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFullFocus;
            this.gridViewTemplates.GridControl = this.gridTemplates;
            this.gridViewTemplates.Name = "gridViewTemplates";
            this.gridViewTemplates.OptionsBehavior.Editable = false;
            // 
            // templateBindingSource
            // 
            this.templateBindingSource.DataSource = typeof(SBFA.sbfa.DocumentTemplateLibrary);
            // 
            // colDocumentContentType
            // 
            this.colDocumentContentType.FieldName = "DocumentContentType";
            this.colDocumentContentType.Name = "colDocumentContentType";
            this.colDocumentContentType.Visible = true;
            this.colDocumentContentType.VisibleIndex = 0;
            // 
            // colDocumentName
            // 
            this.colDocumentName.FieldName = "DocumentName";
            this.colDocumentName.Name = "colDocumentName";
            this.colDocumentName.Visible = true;
            this.colDocumentName.VisibleIndex = 1;
            // 
            // colDocumentType
            // 
            this.colDocumentType.FieldName = "DocumentType";
            this.colDocumentType.Name = "colDocumentType";
            this.colDocumentType.Visible = true;
            this.colDocumentType.VisibleIndex = 2;
            // 
            // UploadTemplates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(589, 384);
            this.Controls.Add(this.gridTemplates);
            this.Controls.Add(this.btnUpload);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.panel75);
            this.Controls.Add(this.label46);
            this.Controls.Add(this.separatorControl11);
            this.Controls.Add(this.barDockControl1);
            this.Controls.Add(this.barDockControl2);
            this.Name = "UploadTemplates";
            this.Text = "Upload Templates";
            this.Load += new System.EventHandler(this.UploadTemplates_Load);
            this.panel75.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.separatorControl11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTemplates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTemplates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.templateBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private System.Windows.Forms.Panel panel75;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label46;
        private DevExpress.XtraEditors.SeparatorControl separatorControl11;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnUpload;
        private DevExpress.XtraGrid.GridControl gridTemplates;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTemplates;
        private System.Windows.Forms.OpenFileDialog uploadDocuments;
        private System.Windows.Forms.BindingSource templateBindingSource;
        private DevExpress.XtraGrid.Columns.GridColumn colDocumentType;
        private DevExpress.XtraGrid.Columns.GridColumn colDocumentName;
        private DevExpress.XtraGrid.Columns.GridColumn colDocumentContentType;
    }
}