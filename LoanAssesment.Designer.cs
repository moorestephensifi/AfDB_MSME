namespace SBFA
{
    partial class LoanAssesment
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoanAssesment));
            this.panel81 = new System.Windows.Forms.Panel();
            this.label67 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDeleteSupplier = new DevExpress.XtraEditors.SimpleButton();
            this.txtProposedLoanAmount = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.btnAddSupplier = new DevExpress.XtraEditors.SimpleButton();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.txtDesc = new System.Windows.Forms.TextBox();
            this.txtSupplier = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRefreshSuppliers = new DevExpress.XtraEditors.SimpleButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gridSuppliers = new DevExpress.XtraGrid.GridControl();
            this.gridViewSuppliers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSupplier = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrency2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPrice = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPriceDisbursed = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtAnanlysis = new System.Windows.Forms.TextBox();
            this.txtCondition = new System.Windows.Forms.TextBox();
            this.txtFinancial = new System.Windows.Forms.TextBox();
            this.txtGrace = new System.Windows.Forms.TextBox();
            this.txtMonthly = new System.Windows.Forms.TextBox();
            this.txtPayback = new System.Windows.Forms.TextBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.txtFee = new System.Windows.Forms.TextBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.panel4 = new System.Windows.Forms.Panel();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.btnPay = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.supplierBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.submenuContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.panel81.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSuppliers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSuppliers)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.supplierBindingSource)).BeginInit();
            this.submenuContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel81
            // 
            this.panel81.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.panel81.Controls.Add(this.label67);
            this.panel81.Location = new System.Drawing.Point(1, 0);
            this.panel81.Name = "panel81";
            this.panel81.Size = new System.Drawing.Size(313, 40);
            this.panel81.TabIndex = 66;
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label67.ForeColor = System.Drawing.Color.White;
            this.label67.Location = new System.Drawing.Point(134, 9);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(174, 21);
            this.label67.TabIndex = 31;
            this.label67.Text = "Loan Assesment Report";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.btnDeleteSupplier);
            this.panel2.Controls.Add(this.txtProposedLoanAmount);
            this.panel2.Controls.Add(this.label17);
            this.panel2.Controls.Add(this.btnAddSupplier);
            this.panel2.Controls.Add(this.txtPrice);
            this.panel2.Controls.Add(this.textBox4);
            this.panel2.Controls.Add(this.txtQuantity);
            this.panel2.Controls.Add(this.txtDesc);
            this.panel2.Controls.Add(this.txtSupplier);
            this.panel2.Controls.Add(this.label16);
            this.panel2.Controls.Add(this.label15);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.panel6);
            this.panel2.Controls.Add(this.panel5);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.txtAnanlysis);
            this.panel2.Controls.Add(this.txtCondition);
            this.panel2.Controls.Add(this.txtFinancial);
            this.panel2.Controls.Add(this.txtGrace);
            this.panel2.Controls.Add(this.txtMonthly);
            this.panel2.Controls.Add(this.txtPayback);
            this.panel2.Controls.Add(this.txtTotal);
            this.panel2.Controls.Add(this.txtFee);
            this.panel2.Controls.Add(this.txtAmount);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Location = new System.Drawing.Point(1, 56);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(916, 565);
            this.panel2.TabIndex = 67;
            // 
            // btnDeleteSupplier
            // 
            this.btnDeleteSupplier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDeleteSupplier.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDeleteSupplier.Appearance.Options.UseFont = true;
            this.btnDeleteSupplier.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteSupplier.ImageOptions.Image")));
            this.btnDeleteSupplier.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.btnDeleteSupplier.Location = new System.Drawing.Point(745, 873);
            this.btnDeleteSupplier.Name = "btnDeleteSupplier";
            this.btnDeleteSupplier.Size = new System.Drawing.Size(132, 33);
            this.btnDeleteSupplier.TabIndex = 81;
            this.btnDeleteSupplier.Text = "Delete Selected";
            this.btnDeleteSupplier.Click += new System.EventHandler(this.btnDeleteSupplier_Click);
            // 
            // txtProposedLoanAmount
            // 
            this.txtProposedLoanAmount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtProposedLoanAmount.Location = new System.Drawing.Point(618, 49);
            this.txtProposedLoanAmount.Name = "txtProposedLoanAmount";
            this.txtProposedLoanAmount.Size = new System.Drawing.Size(259, 21);
            this.txtProposedLoanAmount.TabIndex = 80;
            this.txtProposedLoanAmount.Text = "0";
            this.txtProposedLoanAmount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProposedLoanAmount_KeyPress);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(487, 52);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(118, 13);
            this.label17.TabIndex = 79;
            this.label17.Text = "Proposed Loan Amount";
            // 
            // btnAddSupplier
            // 
            this.btnAddSupplier.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddSupplier.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddSupplier.Appearance.Options.UseFont = true;
            this.btnAddSupplier.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnAddSupplier.ImageOptions.Image")));
            this.btnAddSupplier.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.btnAddSupplier.Location = new System.Drawing.Point(651, 873);
            this.btnAddSupplier.Name = "btnAddSupplier";
            this.btnAddSupplier.Size = new System.Drawing.Size(86, 33);
            this.btnAddSupplier.TabIndex = 70;
            this.btnAddSupplier.Text = "Add";
            this.btnAddSupplier.Click += new System.EventHandler(this.btnAddSupplier_Click_1);
            // 
            // txtPrice
            // 
            this.txtPrice.Location = new System.Drawing.Point(518, 846);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(359, 21);
            this.txtPrice.TabIndex = 78;
            this.txtPrice.Text = "0";
            this.txtPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPrice_KeyPress);
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(518, 803);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(359, 21);
            this.textBox4.TabIndex = 77;
            this.textBox4.Text = "SCR";
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(518, 754);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(359, 21);
            this.txtQuantity.TabIndex = 76;
            this.txtQuantity.Text = "0";
            this.txtQuantity.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantity_KeyPress);
            // 
            // txtDesc
            // 
            this.txtDesc.Location = new System.Drawing.Point(92, 800);
            this.txtDesc.Multiline = true;
            this.txtDesc.Name = "txtDesc";
            this.txtDesc.Size = new System.Drawing.Size(359, 69);
            this.txtDesc.TabIndex = 75;
            // 
            // txtSupplier
            // 
            this.txtSupplier.Location = new System.Drawing.Point(92, 754);
            this.txtSupplier.Name = "txtSupplier";
            this.txtSupplier.Size = new System.Drawing.Size(359, 21);
            this.txtSupplier.TabIndex = 74;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(460, 851);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(30, 13);
            this.label16.TabIndex = 73;
            this.label16.Text = "Price";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(457, 808);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(51, 13);
            this.label15.TabIndex = 72;
            this.label15.Text = "Currency";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(457, 757);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(49, 13);
            this.label14.TabIndex = 71;
            this.label14.Text = "Quantity";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(10, 832);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(60, 13);
            this.label13.TabIndex = 70;
            this.label13.Text = "Description";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 757);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(45, 13);
            this.label3.TabIndex = 69;
            this.label3.Text = "Supplier";
            // 
            // panel6
            // 
            this.panel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel6.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.panel6.Controls.Add(this.label2);
            this.panel6.Controls.Add(this.btnRefreshSuppliers);
            this.panel6.Location = new System.Drawing.Point(0, 914);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(894, 29);
            this.panel6.TabIndex = 68;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 15);
            this.label2.TabIndex = 31;
            this.label2.Text = "Supplier Details";
            // 
            // btnRefreshSuppliers
            // 
            this.btnRefreshSuppliers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshSuppliers.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnRefreshSuppliers.ImageOptions.Image")));
            this.btnRefreshSuppliers.Location = new System.Drawing.Point(863, 3);
            this.btnRefreshSuppliers.Name = "btnRefreshSuppliers";
            this.btnRefreshSuppliers.Size = new System.Drawing.Size(22, 22);
            this.btnRefreshSuppliers.TabIndex = 2;
            this.btnRefreshSuppliers.Click += new System.EventHandler(this.btnRefreshSuppliers_Click);
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.panel5.Controls.Add(this.label1);
            this.panel5.Location = new System.Drawing.Point(-1, 710);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(898, 29);
            this.panel5.TabIndex = 67;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 15);
            this.label1.TabIndex = 31;
            this.label1.Text = "Manage Supplier Details";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.gridSuppliers);
            this.panel1.Location = new System.Drawing.Point(0, 944);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(895, 7032);
            this.panel1.TabIndex = 41;
            // 
            // gridSuppliers
            // 
            this.gridSuppliers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSuppliers.Location = new System.Drawing.Point(0, 0);
            this.gridSuppliers.MainView = this.gridViewSuppliers;
            this.gridSuppliers.Name = "gridSuppliers";
            this.gridSuppliers.Size = new System.Drawing.Size(895, 7032);
            this.gridSuppliers.TabIndex = 40;
            this.gridSuppliers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSuppliers});
            // 
            // gridViewSuppliers
            // 
            this.gridViewSuppliers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId7,
            this.colSupplier,
            this.colDescription1,
            this.colQuantity,
            this.colCurrency2,
            this.colPrice,
            this.colPriceDisbursed});
            this.gridViewSuppliers.GridControl = this.gridSuppliers;
            this.gridViewSuppliers.Name = "gridViewSuppliers";
            // 
            // colId7
            // 
            this.colId7.FieldName = "Id";
            this.colId7.Name = "colId7";
            this.colId7.Visible = true;
            this.colId7.VisibleIndex = 0;
            // 
            // colSupplier
            // 
            this.colSupplier.FieldName = "Supplier";
            this.colSupplier.Name = "colSupplier";
            this.colSupplier.Visible = true;
            this.colSupplier.VisibleIndex = 1;
            // 
            // colDescription1
            // 
            this.colDescription1.FieldName = "Description";
            this.colDescription1.Name = "colDescription1";
            this.colDescription1.Visible = true;
            this.colDescription1.VisibleIndex = 2;
            // 
            // colQuantity
            // 
            this.colQuantity.FieldName = "Quantity";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.Visible = true;
            this.colQuantity.VisibleIndex = 3;
            // 
            // colCurrency2
            // 
            this.colCurrency2.FieldName = "Currency";
            this.colCurrency2.Name = "colCurrency2";
            this.colCurrency2.Visible = true;
            this.colCurrency2.VisibleIndex = 4;
            // 
            // colPrice
            // 
            this.colPrice.FieldName = "Price";
            this.colPrice.Name = "colPrice";
            this.colPrice.Visible = true;
            this.colPrice.VisibleIndex = 5;
            // 
            // colPriceDisbursed
            // 
            this.colPriceDisbursed.FieldName = "PriceDisbursed";
            this.colPriceDisbursed.Name = "colPriceDisbursed";
            this.colPriceDisbursed.Visible = true;
            this.colPriceDisbursed.VisibleIndex = 6;
            // 
            // txtAnanlysis
            // 
            this.txtAnanlysis.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAnanlysis.Location = new System.Drawing.Point(167, 369);
            this.txtAnanlysis.Multiline = true;
            this.txtAnanlysis.Name = "txtAnanlysis";
            this.txtAnanlysis.Size = new System.Drawing.Size(710, 320);
            this.txtAnanlysis.TabIndex = 39;
            // 
            // txtCondition
            // 
            this.txtCondition.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCondition.Location = new System.Drawing.Point(167, 330);
            this.txtCondition.Name = "txtCondition";
            this.txtCondition.Size = new System.Drawing.Size(710, 21);
            this.txtCondition.TabIndex = 38;
            // 
            // txtFinancial
            // 
            this.txtFinancial.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFinancial.Location = new System.Drawing.Point(167, 290);
            this.txtFinancial.Name = "txtFinancial";
            this.txtFinancial.Size = new System.Drawing.Size(710, 21);
            this.txtFinancial.TabIndex = 37;
            // 
            // txtGrace
            // 
            this.txtGrace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtGrace.Location = new System.Drawing.Point(167, 251);
            this.txtGrace.Name = "txtGrace";
            this.txtGrace.Size = new System.Drawing.Size(710, 21);
            this.txtGrace.TabIndex = 36;
            this.txtGrace.Text = "0";
            this.txtGrace.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtGrace_KeyPress);
            // 
            // txtMonthly
            // 
            this.txtMonthly.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMonthly.Location = new System.Drawing.Point(167, 208);
            this.txtMonthly.Name = "txtMonthly";
            this.txtMonthly.ReadOnly = true;
            this.txtMonthly.Size = new System.Drawing.Size(710, 21);
            this.txtMonthly.TabIndex = 35;
            this.txtMonthly.Text = "0";
            // 
            // txtPayback
            // 
            this.txtPayback.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPayback.Location = new System.Drawing.Point(167, 171);
            this.txtPayback.Name = "txtPayback";
            this.txtPayback.Size = new System.Drawing.Size(710, 21);
            this.txtPayback.TabIndex = 34;
            this.txtPayback.Text = "12";
            this.txtPayback.TextChanged += new System.EventHandler(this.txtPayback_TextChanged);
            this.txtPayback.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPayback_KeyPress);
            this.txtPayback.Leave += new System.EventHandler(this.txtPayback_Leave);
            // 
            // txtTotal
            // 
            this.txtTotal.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTotal.Location = new System.Drawing.Point(167, 131);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(710, 21);
            this.txtTotal.TabIndex = 33;
            // 
            // txtFee
            // 
            this.txtFee.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFee.Location = new System.Drawing.Point(167, 90);
            this.txtFee.Name = "txtFee";
            this.txtFee.ReadOnly = true;
            this.txtFee.Size = new System.Drawing.Size(710, 21);
            this.txtFee.TabIndex = 32;
            this.txtFee.Text = "0";
            this.txtFee.TextChanged += new System.EventHandler(this.txtFee_TextChanged);
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(167, 49);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.ReadOnly = true;
            this.txtAmount.Size = new System.Drawing.Size(304, 21);
            this.txtAmount.TabIndex = 31;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(10, 333);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(52, 13);
            this.label12.TabIndex = 30;
            this.label12.Text = "Condition";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(10, 294);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(102, 13);
            this.label11.TabIndex = 29;
            this.label11.Text = "Financial Comitment";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 521);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 13);
            this.label10.TabIndex = 28;
            this.label10.Text = "Analysis";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 254);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(114, 13);
            this.label9.TabIndex = 27;
            this.label9.Text = "Grace Period (Months)";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 212);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(134, 13);
            this.label8.TabIndex = 26;
            this.label8.Text = "Monthly Repayment (SCR)";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 174);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(126, 13);
            this.label7.TabIndex = 25;
            this.label7.Text = "Payback Period (Months)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(64, 13);
            this.label6.TabIndex = 24;
            this.label6.Text = "Net Amount";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 93);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(79, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Processing Fee";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(125, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Requested Loan Amount";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.SteelBlue;
            this.panel3.Controls.Add(this.labelControl3);
            this.panel3.Location = new System.Drawing.Point(-1, 1144);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(895, 36);
            this.panel3.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl3.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl3.Appearance.Options.UseFont = true;
            this.labelControl3.Appearance.Options.UseForeColor = true;
            this.labelControl3.Location = new System.Drawing.Point(5, 8);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(91, 17);
            this.labelControl3.TabIndex = 1;
            this.labelControl3.Text = "Supplier Details";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.panel4.Controls.Add(this.labelControl2);
            this.panel4.Location = new System.Drawing.Point(-1, -1);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(895, 36);
            this.panel4.TabIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl2.Appearance.ForeColor = System.Drawing.Color.White;
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Appearance.Options.UseForeColor = true;
            this.labelControl2.Location = new System.Drawing.Point(5, 8);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(71, 17);
            this.labelControl2.TabIndex = 1;
            this.labelControl2.Text = "Loan Details";
            // 
            // btnPay
            // 
            this.btnPay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPay.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPay.Appearance.Options.UseFont = true;
            this.btnPay.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnPay.ImageOptions.Image")));
            this.btnPay.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.btnPay.Location = new System.Drawing.Point(728, 10);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(86, 38);
            this.btnPay.TabIndex = 68;
            this.btnPay.Text = "Save";
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleButton1.Appearance.Options.UseFont = true;
            this.simpleButton1.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.simpleButton1.ImageOptions.SvgImageSize = new System.Drawing.Size(32, 32);
            this.simpleButton1.Location = new System.Drawing.Point(822, 10);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(87, 38);
            this.simpleButton1.TabIndex = 69;
            this.simpleButton1.Text = "Close";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // supplierBindingSource
            // 
            this.supplierBindingSource.DataSource = typeof(SBFA.sbfa.LoanAssesmentSupplier);
            // 
            // submenuContext
            // 
            this.submenuContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.submenuContext.Name = "submenuContext";
            this.submenuContext.Size = new System.Drawing.Size(108, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(107, 22);
            this.toolStripMenuItem1.Text = "Delete";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // LoanAssesment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(917, 626);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel81);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LoanAssesment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Load += new System.EventHandler(this.LoanAssesment_Load);
            this.panel81.ResumeLayout(false);
            this.panel81.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSuppliers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSuppliers)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.supplierBindingSource)).EndInit();
            this.submenuContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel81;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel4;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.Windows.Forms.Panel panel3;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private System.Windows.Forms.TextBox txtGrace;
        private System.Windows.Forms.TextBox txtMonthly;
        private System.Windows.Forms.TextBox txtPayback;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.TextBox txtFee;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtAnanlysis;
        private System.Windows.Forms.TextBox txtCondition;
        private System.Windows.Forms.TextBox txtFinancial;
        private DevExpress.XtraEditors.SimpleButton btnPay;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.GridControl gridSuppliers;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSuppliers;
        private DevExpress.XtraGrid.Columns.GridColumn colId7;
        private DevExpress.XtraGrid.Columns.GridColumn colSupplier;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription1;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrency2;
        private DevExpress.XtraGrid.Columns.GridColumn colPrice;
        private DevExpress.XtraGrid.Columns.GridColumn colPriceDisbursed;
        private System.Windows.Forms.BindingSource supplierBindingSource;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.SimpleButton btnRefreshSuppliers;
        private DevExpress.XtraEditors.SimpleButton btnAddSupplier;
        private System.Windows.Forms.TextBox txtPrice;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.TextBox txtDesc;
        private System.Windows.Forms.TextBox txtSupplier;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProposedLoanAmount;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ContextMenuStrip submenuContext;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private DevExpress.XtraEditors.SimpleButton btnDeleteSupplier;
    }
}