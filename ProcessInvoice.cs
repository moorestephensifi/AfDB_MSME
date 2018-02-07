using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.ServiceModel;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using System.IO;

namespace SBFA
{
    public partial class ProcessInvoice : DevExpress.XtraEditors.XtraForm
    {
        float due = 0;
        string cur = "";
        public ProcessInvoice()
        {
            InitializeComponent();
        }

        private void ProcessInvoice_Load(object sender, EventArgs e)
        {
            SBFAApi agent = new SBFAApi();
            using (new OperationContextScope(agent.context))
            {
                //check documents
                sbfa.Invoice invoice = agent.operation.GetInvoice(SBFAMain.currentInvoiceId);
                lblAmount.Text = invoice.Currency + invoice.Amount.ToString();
                lbSur.Text = invoice.Currency + invoice.AmountSurcharge.ToString();
                lblDisc.Text = invoice.Currency + invoice.AmountDiscount.ToString();
                lblTotal.Text = invoice.Currency + invoice.AmountTotal.ToString();
                lblPaid.Text = invoice.CurrencyPaid + invoice.AmountPaid.ToString();
                lblDue.Text = invoice.Currency + (invoice.AmountTotal - invoice.AmountPaid);
                lblReference.Text = invoice.FK_ReferenceNumber;
                lblFor.Text = invoice.DocumentType.ToUpper();
                lblReceipt.Text = invoice.ReceiptNumber;
                due = invoice.AmountTotal - invoice.AmountPaid;
                cur = invoice.Currency;
                Globals.SetPickList(cmbPayMethod, "paymet");
                Globals.SetPickList(cmbBranch, "bra");
                Globals.SetPickList(cmbCurrency, "cur");
                if (due <= 0)
                {
                    btnOpenPay.Visible = false;
                    pnlPay.Visible = false;
                    btnPrint.Visible = true;
                }
                else
                {
                    btnOpenPay.Visible = true;
                    btnPrint.Visible = false;
                }

                //get applicant details if registration
                if (invoice.DocumentType == "loan")
                {
                    sbfa.LoanRequest reg = agent.operation.GetLoanRequest(invoice.FK_DocumentId);
                    lblName.Text = reg.FirstNames + " " + reg.LastName;
                    lblNIN.Text = reg.NIN;
                    lblLoc.Text = agent.operation.GetEntityName(reg.FK_ResidenceIslandLocationId, "isl") + "," + agent.operation.GetEntityName(reg.FK_ResidenceDistrictLocationId, "dis");
                }
                else if(invoice.DocumentType == "repayment")
                {
                    sbfa.BusinessRegistration reg = agent.operation.GetBusinessRegistrationByRegistration(invoice.FK_ReferenceNumber.Split('_')[0]);
                    lblName.Text = reg.FirstNames + " " + reg.LastName;
                    lblNIN.Text = reg.NIN;
                    lblLoc.Text = agent.operation.GetEntityName(reg.FK_ResidenceIslandLocationId, "isl") + "," + agent.operation.GetEntityName(reg.FK_ResidenceDistrictLocationId, "dis");

                }

                sbfa.InvoiceItem[] items = agent.operation.GetInvoiceItems(SBFAMain.currentInvoiceId);
                foreach (sbfa.InvoiceItem item in items)
                {
                    string[] row = { item.Id.ToString(), item.Description, item.Amount.ToString() };
                    var listViewItem = new ListViewItem(row);
                    lstItems.Items.Add(listViewItem);
                }
            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {

            if (txtPayAmount.Text.Equals(""))
            {
                ShowValidationError("Please enter the amount to be paid.");
                return;
            }
            if (isNumeric(txtPayAmount.Text) == false)
            {
                ShowValidationError("Please enter a valid amount to be paid.");
                txtPayAmount.Focus();
                return;
            }

            SBFAApi agent = new SBFAApi();
            using (new OperationContextScope(agent.context))
            {
                string response = agent.operation.PayInvoice(SBFAMain.currentInvoiceId, cmbCurrency.Text, float.Parse(txtPayAmount.Text), float.Parse(lblChange.Text), Globals.GetComboBoxValue(cmbBranch), Globals.GetComboBoxValue(cmbPayMethod));
                if (response != "0")
                {
                    lblReceipt.Text = response;
                    pnlPay.Visible = false;
                    btnOpenPay.Visible = false;
                    btnPrint.Visible = true;
                }

                if (response != "0")
                {
                    lblReceipt.Text = response;
                    pnlPay.Visible = false;
                    btnOpenPay.Visible = false;
                    btnPrint.Visible = false;

                    ShowSuccessMessage("Payment successful. The receipt number for this payment is " + lblReceipt.Text);

                    if (chkPrint.Checked == true)
                    {
                        try
                        {
                            printReceipt();
                        }
                        catch (Exception ex)
                        {
                            ShowError("An error occured in printing. Please install a PDF Reader and try again.");
                        }
                    }
                }
                else
                {
                    ShowErrorMessage("An error occured making the payment!");
                }
            }
        }

        private void txtPayAmount_TextChanged(object sender, EventArgs e)
        {
            if (txtPayAmount.Text == "")
            {
                return;
            }
            try
            {
                lblCur.Text = cur;
                lblChange.Text = (due - float.Parse(txtPayAmount.Text)).ToString();
            }
            catch (Exception ex)
            {
                //ShowValidationError("Enter a valid amount");
            }

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            pnlPay.Visible = true;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            pnlPay.Visible = false;

        }

        private void printReceipt()
        {
            SBFAApi agent = new SBFAApi();
            using (new OperationContextScope(agent.context))
            {
                Byte[] doc = agent.operation.GetAutoDocument("receipt", long.Parse(lblReceipt.Text));
                string filePath = Application.StartupPath + "\\filer\\" + lblReceipt.Text + ".pdf";
                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Write(doc, 0, doc.Length);
                fs.Flush();
                fs.Close();
                System.Diagnostics.Process newProcess = new System.Diagnostics.Process();
                newProcess.StartInfo = new System.Diagnostics.ProcessStartInfo(filePath);
                newProcess.Start();
                newProcess.WaitForExit();
                try
                {
                    System.IO.File.Delete(filePath);
                }
                catch
                {

                }

                this.Close();
            }
        }

        public void ShowValidationError(string message)
        {
            FlyoutAction action = new FlyoutAction() { Caption = "Validation Error!", Description = message };

            FlyoutCommand command1 = new FlyoutCommand() { Text = "OK", Result = System.Windows.Forms.DialogResult.Yes };
            action.Commands.Add(command1);

            FlyoutProperties properties = new FlyoutProperties();
            properties.ButtonSize = new Size(100, 40);
            properties.Style = FlyoutStyle.MessageBox;
            properties.Appearance.BackColor = Color.Orange;
            properties.Appearance.ForeColor = Color.White;
            DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog.Show(this, action, properties);
        }

        public void ShowSuccessMessage(string message)
        {
            FlyoutAction action = new FlyoutAction() { Caption = "Success!", Description = message };

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

        public void ShowErrorMessage(string message)
        {
            FlyoutAction action = new FlyoutAction() { Caption = "Payment Failed!", Description = message };

            FlyoutCommand command1 = new FlyoutCommand() { Text = "OK", Result = System.Windows.Forms.DialogResult.Yes };
            action.Commands.Add(command1);

            FlyoutProperties properties = new FlyoutProperties();
            properties.ButtonSize = new Size(100, 40);
            properties.Style = FlyoutStyle.MessageBox;
            properties.Appearance.BackColor = Color.Red;
            properties.Appearance.ForeColor = Color.White;
            DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog.Show(this, action, properties);
        }

        public bool isNumeric(string text)
        {
            double myNum = 0;

            if (Double.TryParse(text, out myNum))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                printReceipt();
            }catch(Exception ex)
            {
                ShowError("An error occured in printing. Please install a PDF Reader and try again.");
            }
           
        }

        public void ShowError(string message)
        {
            FlyoutAction action = new FlyoutAction() { Caption = "Printing failed!", Description = message };

            FlyoutCommand command1 = new FlyoutCommand() { Text = "OK", Result = System.Windows.Forms.DialogResult.Yes };
            action.Commands.Add(command1);

            FlyoutProperties properties = new FlyoutProperties();
            properties.ButtonSize = new Size(100, 40);
            properties.Style = FlyoutStyle.MessageBox;
            properties.Appearance.BackColor = Color.Red;
            properties.Appearance.ForeColor = Color.White;
            DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog.Show(this, action, properties);
        }

        private void txtPayAmount_KeyPress(object sender, KeyPressEventArgs e)
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