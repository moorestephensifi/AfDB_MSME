using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.ServiceModel;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;

namespace SBFA
{
    public partial class LoanAssesment : DevExpress.XtraEditors.XtraForm
    {
        public LoanAssesment()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void LoanAssesment_Load(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.LoanAssesment ass = agent.operation.GetLoanAssesment(SBFAMain.currentId);
                    txtAmount.Text = ass.LoanAmount.ToString();
                    txtAnanlysis.Text = ass.Analysis;
                    txtCondition.Text = ass.Condition;
                    txtFee.Text = ass.ProcessingFee.ToString();
                    txtFinancial.Text = ass.FinancialCommitment;
                    txtGrace.Text = ass.GracePeriod.ToString();
                    txtMonthly.Text = ass.MonthlyRepayment.ToString();
                    txtPayback.Text = ass.PaybackPeriod.ToString();
                    txtTotal.Text = (ass.LoanAmount - ass.ProcessingFee).ToString();
                    txtProposedLoanAmount.Text = ass.ProposedLoanAmount.ToString();
                    gridSuppliers.DataSource = ass.Supplier;
                    gridSuppliers.RefreshDataSource();
                }
            }
            catch
            {
                ShowErrorMessage("Error initializing assesment");
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.LoanAssesment ass = new sbfa.LoanAssesment();
                    ass.FK_LoanRequestId = SBFAMain.currentId;
                    ass.LoanAmount = float.Parse(txtAmount.Text);
                    ass.Analysis = txtAnanlysis.Text;
                    ass.Condition = txtCondition.Text;
                    ass.ProcessingFee = float.Parse(txtFee.Text);
                    ass.FinancialCommitment = txtFinancial.Text;
                    ass.GracePeriod = int.Parse(txtGrace.Text);
                    ass.MonthlyRepayment = float.Parse(txtMonthly.Text);
                    ass.PaybackPeriod = int.Parse(txtPayback.Text);
                    ass.Total = float.Parse(txtTotal.Text);
                    ass.ProposedLoanAmount = float.Parse(txtProposedLoanAmount.Text);

                    List<sbfa.LoanAssesmentSupplier> supplier = new List<sbfa.LoanAssesmentSupplier>();

                    for (int a = 0; a < gridViewSuppliers.RowCount; a++)
                    {
                        sbfa.LoanAssesmentSupplier temp = new sbfa.LoanAssesmentSupplier();
                        temp.Price = float.Parse(gridViewSuppliers.GetRowCellValue(a, gridViewSuppliers.Columns[5]).ToString());
                        temp.Quantity = float.Parse(gridViewSuppliers.GetRowCellValue(a, gridViewSuppliers.Columns[3]).ToString());
                        temp.Description = (gridViewSuppliers.GetRowCellValue(a, gridViewSuppliers.Columns[2]).ToString());
                        temp.FK_LoanRequestId = SBFAMain.currentId;
                        temp.PriceDisbursed = 0;
                        temp.Id = ass.Id;
                        temp.Currency = "SCR";
                        temp.Supplier = (gridViewSuppliers.GetRowCellValue(a, gridViewSuppliers.Columns[1]).ToString());
                    }

                    sbfa.LoanAssesmentSupplier[] supps = supplier.ToArray();
                    ass.Supplier = supps;

                    bool save = agent.operation.SaveLoanAssesment(ass);
                    if (save)
                        ShowSuccessMessage("Saved Successfully!!");
                }
            }
            catch
            {
                ShowErrorMessage("Error saving assesment");
            }
        }

        private void btnAddSupplier_Click(object sender, EventArgs e)
        {
            gridViewSuppliers.AddNewRow();
        }

        private void btnRefreshSuppliers_Click(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.LoanAssesment ass = agent.operation.GetLoanAssesment(SBFAMain.currentId);
                    gridSuppliers.DataSource = ass.Supplier;
                    gridSuppliers.RefreshDataSource();
                }
            }
            catch
            {
                ShowErrorMessage("Error processing");
            }
        }

        private void txtFee_TextChanged(object sender, EventArgs e)
        {
           // txtTotal.Text = (float.Parse(txtFee.Text) + float.Parse(txtAmount.Text)).ToString();
        }

        private void btnAddSupplier_Click_1(object sender, EventArgs e)
        {
            if (txtPrice.Text == "" || txtQuantity.Text== "")
            {
                ShowErrorMessage("Enter valid supplier details");
                return;
            }

            if (txtSupplier.Text=="" || float.Parse(txtPrice.Text)<=0 || float.Parse(txtQuantity.Text) <= 0)
            {
                ShowErrorMessage("Enter valid supplier details");
                return;
            }
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.LoanAssesment ass = new sbfa.LoanAssesment();
                    ass.FK_LoanRequestId = SBFAMain.currentId;
                    ass.LoanAmount = float.Parse(txtAmount.Text);
                    ass.Analysis = txtAnanlysis.Text;
                    ass.Condition = txtCondition.Text;
                    ass.ProcessingFee = float.Parse(txtFee.Text);
                    ass.FinancialCommitment = txtFinancial.Text;
                    ass.GracePeriod = int.Parse(txtGrace.Text);
                    ass.MonthlyRepayment = float.Parse(txtMonthly.Text);
                    ass.PaybackPeriod = int.Parse(txtPayback.Text);

                    ass.Total = float.Parse(txtTotal.Text);
                    ass.ProposedLoanAmount = float.Parse(txtProposedLoanAmount.Text);

                    sbfa.LoanAssesmentSupplier temp = new sbfa.LoanAssesmentSupplier();
                    temp.Price = float.Parse(txtPrice.Text);
                    temp.Quantity = float.Parse(txtQuantity.Text);
                    temp.Description = (txtDesc.Text);
                    temp.FK_LoanRequestId = SBFAMain.currentId;
                    temp.PriceDisbursed = 0;
                    temp.Id = ass.Id;
                    temp.Currency = "SCR";
                    temp.Supplier = (txtSupplier.Text);

                    sbfa.LoanAssesmentSupplier[] supps = new sbfa.LoanAssesmentSupplier[] { temp };
                    ass.Supplier = supps;

                    bool save = agent.operation.SaveLoanAssesment(ass);
                    if (save)
                    {
                        ShowSuccessMessage("Saved Succesfully!!");

                        sbfa.LoanAssesment assL = agent.operation.GetLoanAssesment(SBFAMain.currentId);
                        gridSuppliers.DataSource = assL.Supplier;
                        gridSuppliers.RefreshDataSource();
                    }
                    else
                    {
                        ShowErrorMessage("Please ensure your amounts are valid");
                    }
                }
            }
            catch
            {
                ShowErrorMessage("Error adding supplier");
            }
        }

        private void txtPayback_Leave(object sender, EventArgs e)
        {
            if(txtPayback.Text=="")
            {
                ShowErrorMessage("Enter a valid period");
                txtPayback.Focus();
                return;
            }
            if (float.Parse(txtPayback.Text) <=0)
            {
                ShowErrorMessage("Enter a valid period");
                txtPayback.Focus();
                return;
            }
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    float val = agent.operation.CalculateMonthlyRepayment(int.Parse(txtPayback.Text), SBFAMain.currentId);
                    txtMonthly.Text = val.ToString();
                }
            }
            catch
            {
                ShowErrorMessage("Error processing");
            }
        }

        private void txtPayback_TextChanged(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.LoanAssesment ass = new sbfa.LoanAssesment();
                    ass.FK_LoanRequestId = SBFAMain.currentId;
                    ass.LoanAmount = float.Parse(txtAmount.Text);
                    ass.Analysis = txtAnanlysis.Text;
                    ass.Condition = txtCondition.Text;
                    ass.ProcessingFee = float.Parse(txtFee.Text);
                    ass.FinancialCommitment = txtFinancial.Text;
                    ass.GracePeriod = int.Parse(txtGrace.Text);
                    ass.MonthlyRepayment = float.Parse(txtMonthly.Text);
                    ass.PaybackPeriod = int.Parse(txtPayback.Text);

                    sbfa.LoanAssesmentSupplier temp = new sbfa.LoanAssesmentSupplier();
                    temp.Price = 0;
                    temp.Quantity = 0;
                    temp.Description = "";
                    temp.FK_LoanRequestId = SBFAMain.currentId;
                    temp.PriceDisbursed = 0;
                    temp.Id = ass.Id;
                    temp.Currency = "SCR";
                    temp.Supplier = gridViewSuppliers.GetRowCellValue(gridViewSuppliers.FocusedRowHandle, gridViewSuppliers.Columns[1]).ToString();

                    sbfa.LoanAssesmentSupplier[] supps = new sbfa.LoanAssesmentSupplier[] { temp };
                    ass.Supplier = supps;

                    bool save = agent.operation.DeleteLoanAssesment(ass);
                    if (save)
                    {
                      
                        sbfa.LoanAssesment assL = agent.operation.GetLoanAssesment(SBFAMain.currentId);
                        gridSuppliers.DataSource = assL.Supplier;
                        gridSuppliers.RefreshDataSource();
                    }
                }
            }
            catch
            {
                ShowErrorMessage("Error deleting supplier");
            }
        }

        public void ShowErrorMessage(string message)
        {
            FlyoutAction action = new FlyoutAction() { Caption = "Loan Assesment!", Description = message };

            FlyoutCommand command1 = new FlyoutCommand() { Text = "Close?", Result = System.Windows.Forms.DialogResult.Yes };
            action.Commands.Add(command1);

            FlyoutProperties properties = new FlyoutProperties();
            properties.ButtonSize = new Size(100, 40);
            properties.Style = FlyoutStyle.MessageBox;
            properties.Appearance.BackColor = Color.Red;
            properties.Appearance.ForeColor = Color.White;
            DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog.Show(this, action, properties);
        }

        public void ShowSuccessMessage(string message)
        {
            FlyoutAction action = new FlyoutAction() { Caption = "Loan Assesment!", Description = message };

            FlyoutCommand command1 = new FlyoutCommand() { Text = "OK", Result = System.Windows.Forms.DialogResult.Yes };
            action.Commands.Add(command1);

            FlyoutProperties properties = new FlyoutProperties();
            properties.ButtonSize = new Size(100, 40);
            properties.Style = FlyoutStyle.MessageBox;
            properties.Appearance.BackColor = Color.Green;
            properties.Appearance.ForeColor = Color.White;
            DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog.Show(this, action, properties);
            if (DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog.Show(this, action, properties) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void txtProposedLoanAmount_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtPayback_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            //// If you want, you can allow decimal (float) numbers
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }

        private void txtGrace_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            //// If you want, you can allow decimal (float) numbers
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Verify that the pressed key isn't CTRL or any non-numeric digit
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // If you want, you can allow decimal (float) numbers
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
    }
}