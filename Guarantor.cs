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
    public partial class Guarantor : DevExpress.XtraEditors.XtraForm
    {
        public Guarantor()
        {
            InitializeComponent();
        }

        private void Guarantor_Load(object sender, EventArgs e)
        {
            txtDonorNoOfDependents.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtDonorYearsOfEmployment.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtDonorMonthlyIncome.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtDonorMonthlyExpenditure.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.Guarantor[] gua = agent.operation.GetGuarantors(SBFAMain.currentId);
                    gridGuarantors.DataSource = gua;
                    gridGuarantors.RefreshDataSource();

                    sbfa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(SBFAMain.currentId, "loan");
                    if (currentStage.StageName == "Complete")
                    {
                        btnAddGuarantor.Visible = false;
                    }
                    else
                    {
                        btnAddGuarantor.Visible = true;
                    }
                }
            }
            catch
            {
                ShowErrorMessage("Error initializing guarantors");
            }
        }

        private void btnAddGuarantor_Click(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.Guarantor gua = new sbfa.Guarantor();
                    gua.FK_BusinessRegistrationId = SBFAMain.currentId;
                    gua.GuarantorNIN = txtDonorNIN.Text;
                    gua.GuarantorName = txtDonorName.Text;
                    gua.GuarantorSurname = txtDonorSurname.Text;
                    gua.GuarantorDOB = dtDonorDOB.DateTime;
                    gua.GuarantorAddress = txtDonorAddress.Text;
                    gua.GuarantorContactNo = txtDonorContactNo.Text;
                    gua.GuarantorMaritalStatus = cmbDonorMaritalStatus.Text;
                    gua.GuarantorNoOfDependents = txtDonorNoOfDependents.Text;
                    gua.GuarantorEmploymentStatus = cmbDonorEmploymentStatus.Text;
                    gua.GuarantorEmployersAddress = txtDonorEmployerAddress.Text;
                    gua.GuarantorEmployersName = txtDonorEmployerName.Text;
                    gua.GuarantorCurrentPosition = txtDonorCurrentPosition.Text;
                    gua.GuarantorNoOfYears = Globals.GetIntValue(txtDonorYearsOfEmployment);
                    gua.GuarantorTotalMonthlyIncome = Globals.GetFloatValue(txtDonorMonthlyIncome);
                    gua.GuarantorTotalMonthlyExpenditure = Globals.GetFloatValue(txtDonorMonthlyExpenditure);

                    long save = agent.operation.SaveLoanRequestGuarantor(gua);
                    if (save > 0)
                    {
                        MessageBox.Show("Saved!!");

                        sbfa.Guarantor[] guas = agent.operation.GetGuarantors(SBFAMain.currentId);
                        gridGuarantors.DataSource = guas;
                        gridGuarantors.RefreshDataSource();
                    }
                }
            }
            catch
            {
                ShowErrorMessage("Error saving guarantor");
            }
        }

        private void txtDonorNIN_Leave(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    //check from local db first for details
                    sbfa.Guarantor guarantor = agent.operation.GetGuarantor(txtDonorNIN.Text);

                    if (guarantor == null || guarantor.GuarantorName == null)
                    {
                        sbfa.Resident response = agent.operation.GetResident(txtDonorNIN.Text);

                        txtDonorName.Text = response.FirstName;
                        txtDonorSurname.Text = response.Surname;

                        dtDonorDOB.DateTime = response.DateOfBirth;

                        if (response.FirstName == "")
                            txtDonorNIN.Text = "";
                    }
                    else
                    {
                        //  guarantor.GuarantorNIN = txtDonorNIN.Text;
                        txtDonorName.Text = guarantor.GuarantorName;
                        txtDonorSurname.Text = guarantor.GuarantorSurname;
                        dtDonorDOB.DateTime = guarantor.GuarantorDOB;
                        txtDonorAddress.Text = guarantor.GuarantorAddress;
                        txtDonorContactNo.Text = guarantor.GuarantorContactNo;
                        cmbDonorMaritalStatus.Text = guarantor.GuarantorMaritalStatus;
                        txtDonorNoOfDependents.Text = guarantor.GuarantorNoOfDependents;
                        cmbDonorEmploymentStatus.Text = guarantor.GuarantorEmploymentStatus;
                        txtDonorEmployerAddress.Text = guarantor.GuarantorEmployersAddress;
                        txtDonorEmployerName.Text = guarantor.GuarantorEmployersName;
                        txtDonorCurrentPosition.Text = guarantor.GuarantorCurrentPosition;
                        txtDonorYearsOfEmployment.Text = guarantor.GuarantorNoOfYears.ToString();
                        txtDonorMonthlyIncome.Text = guarantor.GuarantorTotalMonthlyIncome.ToString();
                        txtDonorMonthlyExpenditure.Text = guarantor.GuarantorTotalMonthlyExpenditure.ToString();
                    }
                }
            }
            catch
            {
                ShowErrorMessage("Failed to validate with national database");
            }
        }

        private void btnRefreshSuppliers_Click(object sender, EventArgs e)
        {

        }

        private void gridViewGuarantors_Click(object sender, EventArgs e)
        {
            try
            {
                string currentNIN = (gridViewGuarantors.GetRowCellValue(gridViewGuarantors.FocusedRowHandle, gridViewGuarantors.Columns[0]).ToString());

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    //check from local db first for details
                    sbfa.Guarantor guarantor = agent.operation.GetLoanRequestGuarantor(currentNIN, SBFAMain.currentId);

                    //  guarantor.GuarantorNIN = txtDonorNIN.Text;
                    txtDonorName.Text = guarantor.GuarantorName;
                    txtDonorSurname.Text = guarantor.GuarantorSurname;
                    dtDonorDOB.DateTime = guarantor.GuarantorDOB;
                    txtDonorAddress.Text = guarantor.GuarantorAddress;
                    txtDonorContactNo.Text = guarantor.GuarantorContactNo;
                    cmbDonorMaritalStatus.Text = guarantor.GuarantorMaritalStatus;
                    txtDonorNoOfDependents.Text = guarantor.GuarantorNoOfDependents;
                    cmbDonorEmploymentStatus.Text = guarantor.GuarantorEmploymentStatus;
                    txtDonorEmployerAddress.Text = guarantor.GuarantorEmployersAddress;
                    txtDonorEmployerName.Text = guarantor.GuarantorEmployersName;
                    txtDonorCurrentPosition.Text = guarantor.GuarantorCurrentPosition;
                    txtDonorYearsOfEmployment.Text = guarantor.GuarantorNoOfYears.ToString();
                    txtDonorMonthlyIncome.Text = guarantor.GuarantorTotalMonthlyIncome.ToString();
                    txtDonorMonthlyExpenditure.Text = guarantor.GuarantorTotalMonthlyExpenditure.ToString();

                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error loading guarantor");
            }
            
        }

        public void ShowErrorMessage(string message)
        {
            FlyoutAction action = new FlyoutAction() { Caption = "Loan Guarantor!", Description = message };

            FlyoutCommand command1 = new FlyoutCommand() { Text = "Error Processing?", Result = System.Windows.Forms.DialogResult.Yes };
            action.Commands.Add(command1);

            FlyoutProperties properties = new FlyoutProperties();
            properties.ButtonSize = new Size(100, 40);
            properties.Style = FlyoutStyle.MessageBox;
            properties.Appearance.BackColor = Color.Red;
            properties.Appearance.ForeColor = Color.White;
            DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog.Show(this, action, properties);
        }
    }
}