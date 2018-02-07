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
using System.IO;

namespace SBFA
{
    public partial class ReceiveOldRepayment : DevExpress.XtraEditors.XtraForm
    {
        float due = 0;
        string cur = "";
        public ReceiveOldRepayment()
        {
            InitializeComponent();
        }

        private void ReceiveOldRepayment_Load(object sender, EventArgs e)
        {
            SBFAApi agent = new SBFAApi();
            using (new OperationContextScope(agent.context))
            {
                Globals.SetPickList(cmbPayMethod, "paymet");
                Globals.SetPickList(cmbBranch, "bra");
                Globals.SetPickList(cmbCurrency, "cur");

                Globals.SetPickList(cmbFeePayMethod, "paymet");
                Globals.SetPickList(cmbFeeBranch, "bra");
                Globals.SetPickList(cmbFeeCurrency, "cur");

                if (SBFAMain.oldAccount != "" && SBFAMain.oldLoan != "")
                {
                    txtAccount.Text = SBFAMain.oldAccount;
                    txtLoan.Text = SBFAMain.oldLoan;

                    sbfa.BusinessRegistration reg = agent.operation.GetOldBusinessRegistrationByRegistration(txtAccount.Text, txtLoan.Text);
                    lblName.Text = reg.FirstNames + " " + reg.LastName;
                    lblNIN.Text = reg.NIN;
                    lblLoc.Text = agent.operation.GetEntityName(reg.FK_ResidenceIslandLocationId, "isl") + "," + agent.operation.GetEntityName(reg.FK_ResidenceDistrictLocationId, "dis");

                    sbfa.BusinessAccountOld bus = agent.operation.GetOldBusinessAccountByAccount(txtAccount.Text, txtLoan.Text);
                    double bal = bus.AccountBalance;// CalculateBalance(bus.AccountBalance.ToString(), bus.IntrestRate.ToString(), bus.LastCalculationDate);
                    lblTotal.Text = bal.ToString();
                    lblDue.Text = bal.ToString();
                }
            }
        }

        public struct DateTimeSpan
        {
            private readonly int years;
            private readonly int months;
            private readonly int days;
            private readonly int hours;
            private readonly int minutes;
            private readonly int seconds;
            private readonly int milliseconds;

            public DateTimeSpan(int years, int months, int days, int hours, int minutes, int seconds, int milliseconds)
            {
                this.years = years;
                this.months = months;
                this.days = days;
                this.hours = hours;
                this.minutes = minutes;
                this.seconds = seconds;
                this.milliseconds = milliseconds;
            }

            public int Years { get { return years; } }
            public int Months { get { return months; } }
            public int Days { get { return days; } }
            public int Hours { get { return hours; } }
            public int Minutes { get { return minutes; } }
            public int Seconds { get { return seconds; } }
            public int Milliseconds { get { return milliseconds; } }

            enum Phase { Years, Months, Days, Done }

            public static DateTimeSpan CompareDates(DateTime date1, DateTime date2)
            {
                if (date2 < date1)
                {
                    var sub = date1;
                    date1 = date2;
                    date2 = sub;
                }

                DateTime current = date1;
                int years = 0;
                int months = 0;
                int days = 0;

                Phase phase = Phase.Years;
                DateTimeSpan span = new DateTimeSpan();
                int officialDay = current.Day;

                while (phase != Phase.Done)
                {
                    switch (phase)
                    {
                        case Phase.Years:
                            if (current.AddYears(years + 1) > date2)
                            {
                                phase = Phase.Months;
                                current = current.AddYears(years);
                            }
                            else
                            {
                                years++;
                            }
                            break;
                        case Phase.Months:
                            if (current.AddMonths(months + 1) > date2)
                            {
                                phase = Phase.Days;
                                current = current.AddMonths(months);
                                if (current.Day < officialDay && officialDay <= DateTime.DaysInMonth(current.Year, current.Month))
                                    current = current.AddDays(officialDay - current.Day);
                            }
                            else
                            {
                                months++;
                            }
                            break;
                        case Phase.Days:
                            if (current.AddDays(days + 1) > date2)
                            {
                                current = current.AddDays(days);
                                var timespan = date2 - current;
                                span = new DateTimeSpan(years, months, days, timespan.Hours, timespan.Minutes, timespan.Seconds, timespan.Milliseconds);
                                phase = Phase.Done;
                            }
                            else
                            {
                                days++;
                            }
                            break;
                    }
                }

                return span;
            }
        }

        DateTime GetLastCalculationDate(DateTime lastPay)
        {
            DateTime compareTo = lastPay;
            DateTime now = DateTime.Now;
            var dateSpan = DateTimeSpan.CompareDates(compareTo, now);
            //Console.WriteLine("Years: " + dateSpan.Years);
            //Console.WriteLine("Months: " + dateSpan.Months);
            //Console.WriteLine("Days: " + dateSpan.Days);
            // Console.WriteLine("Hours: " + dateSpan.Hours);
            // Console.WriteLine("Minutes: " + dateSpan.Minutes);
            // Console.WriteLine("Seconds: " + dateSpan.Seconds);
            // Console.WriteLine("Milliseconds: " + dateSpan.Milliseconds);
            int months = ((dateSpan.Years * 12) + dateSpan.Months);
            return (lastPay.AddMonths(months));
        }

        double CalculateBalance(string amount, string rate, DateTime lastPay)
        {
            // Make sure we use types that hold decimal places
            double new_balance, ending_balance;
            double interest_paid, annual_rate, principle_paid, payment;
            ending_balance = Convert.ToDouble(amount);
            //(double)yearlyInterestRate / 100 / 12;
            annual_rate = Convert.ToDouble(rate);
            // Setup a counter to count payments
            int count = 1;
            //get months in between
            DateTime compareTo = lastPay;
            DateTime now = DateTime.Now;
            var dateSpan = DateTimeSpan.CompareDates(compareTo, now);
            //Console.WriteLine("Years: " + dateSpan.Years);
            //Console.WriteLine("Months: " + dateSpan.Months);
            //Console.WriteLine("Days: " + dateSpan.Days);
            // Console.WriteLine("Hours: " + dateSpan.Hours);
            // Console.WriteLine("Minutes: " + dateSpan.Minutes);
            // Console.WriteLine("Seconds: " + dateSpan.Seconds);
            // Console.WriteLine("Milliseconds: " + dateSpan.Milliseconds);
            int months = ((dateSpan.Years * 12) + dateSpan.Months);
            // Get our standard payment which is 1/months of loan
            //payment = (ending_balance / months);
            //payment = Convert.ToDouble(monthly);
            while (count <= months)
            {
                new_balance = ending_balance;
                // Calculate interest by multiplying rate against balance
                interest_paid = new_balance * (annual_rate / 100 / 12.0);
                // Subtract interest from your payment
                // principle_paid = payment - interest_paid;
                // Subtract final payment from running balance
                ending_balance = new_balance + interest_paid;
                // If the balance remaining plus its interest is less than payment amount
                // Then print out 0 balance, the interest paid and that balance minus the interest will tell us
                // how much principle you paid to get to zero.

                count++;
            }
            return ending_balance;
        }

        private void txtPayAmount_TextChanged(object sender, EventArgs e)
        {
            if(txtPayAmount.Text=="")
            {
                return;
            }
            try
            {
                lblDue.Text = (double.Parse(lblTotal.Text) - double.Parse(txtPayAmount.Text)).ToString();
                lblChange.Text = (double.Parse(lblTotal.Text) - double.Parse(txtPayAmount.Text)).ToString();
                lblPaid.Text = txtPayAmount.Text;
            }
            catch
            {
                ShowValidationError("Enter valid amount");
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPayPenalty_Click(object sender, EventArgs e)
        {
            pnlFees.Visible = true;
            SBFAApi agent = new SBFAApi();
            using (new OperationContextScope(agent.context))
            {
                if (SBFAMain.oldAccount != "" && SBFAMain.oldLoan != "")
                {
                    txtFeeAccount.Text = SBFAMain.oldAccount;
                    txtFeeLoan.Text = SBFAMain.oldLoan;

                    sbfa.BusinessRegistration reg = agent.operation.GetOldBusinessRegistrationByRegistration(txtFeeAccount.Text, txtFeeLoan.Text);
                    lblName.Text = reg.FirstNames + " " + reg.LastName;
                    lblNIN.Text = reg.NIN;
                    lblLoc.Text = agent.operation.GetEntityName(reg.FK_ResidenceIslandLocationId, "isl") + "," + agent.operation.GetEntityName(reg.FK_ResidenceDistrictLocationId, "dis");

                    sbfa.BusinessAccountOld bus = agent.operation.GetOldBusinessAccountByAccount(txtFeeAccount.Text, txtFeeLoan.Text);
                    double bal = bus.Penalty + bus.ProcessingFee + bus.CancellationFee;
                    lblTotal.Text = bal.ToString();
                    lblDue.Text = bal.ToString();
                }
            }
        }

        private void txtLoan_Leave(object sender, EventArgs e)
        {
            if (txtAccount.Text == "") return;
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.BusinessRegistration reg = agent.operation.GetOldBusinessRegistrationByRegistration(txtAccount.Text,txtLoan.Text);
                    lblName.Text = reg.FirstNames + " " + reg.LastName;
                    lblNIN.Text = reg.NIN;
                    lblLoc.Text = agent.operation.GetEntityName(reg.FK_ResidenceIslandLocationId, "isl") + "," + agent.operation.GetEntityName(reg.FK_ResidenceDistrictLocationId, "dis");

                    sbfa.BusinessAccountOld bus = agent.operation.GetOldBusinessAccountByAccount(txtAccount.Text,txtLoan.Text);
                    double bal = bus.AccountBalance;// CalculateBalance(bus.AccountBalance.ToString(), bus.IntrestRate.ToString(), bus.LastCalculationDate);
                    lblTotal.Text = bal.ToString();
                    lblDue.Text = bal.ToString();
                }
            }
            catch
            {
                //
            }
        }

        private void txtAccount_Leave(object sender, EventArgs e)
        {
            if (txtLoan.Text == "") return;
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.BusinessRegistration reg = agent.operation.GetOldBusinessRegistrationByRegistration(txtAccount.Text, txtLoan.Text);
                    lblName.Text = reg.FirstNames + " " + reg.LastName;
                    lblNIN.Text = reg.NIN;
                    lblLoc.Text = agent.operation.GetEntityName(reg.FK_ResidenceIslandLocationId, "isl") + "," + agent.operation.GetEntityName(reg.FK_ResidenceDistrictLocationId, "dis");

                    sbfa.BusinessAccountOld bus = agent.operation.GetOldBusinessAccountByAccount(txtAccount.Text, txtLoan.Text);
                    double bal = bus.AccountBalance;// CalculateBalance(bus.AccountBalance.ToString(), bus.IntrestRate.ToString(), bus.LastCalculationDate);
                    lblTotal.Text = bal.ToString();
                    lblDue.Text = bal.ToString();
                }
            }
            catch
            {
                //
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
                txtFeePayAmount.Focus();
                return;
            }

            SBFAApi agent = new SBFAApi();
            using (new OperationContextScope(agent.context))
            {
                sbfa.BusinessAccountOld bus = agent.operation.GetOldBusinessAccountByAccount(txtAccount.Text,txtLoan.Text);
                // DateTime lDt = GetLastCalculationDate(bus.LastCalculationDate);
                DateTime lDt = DateTime.Parse(DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString().PadLeft(2, '0') + "-01 06:00:00.000");
                string rec = agent.operation.OldReceiptRepayment(bus.AccountNumber,bus.LoanNumber, cmbCurrency.Text, float.Parse(lblDue.Text), float.Parse(txtPayAmount.Text), Globals.GetComboBoxValue(cmbBranch), Globals.GetComboBoxValue(cmbPayMethod), lDt);
                if (rec != "0")
                {
                    lblReceipt.Text = rec;
                    btnPrint.Visible = true;
                }

                if (rec != "0")
                {
                    lblReceipt.Text = rec;
                    btnPrint.Visible = false;

                    ShowSuccessMessage("Payment successful. The receipt number for this payment is " + lblReceipt.Text);

                    if (chkPrint.Checked == true)
                    {
                        try
                        {
                            printReceipt(rec);
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

        private void txtFeeAccount_Leave(object sender, EventArgs e)
        {
            if (txtFeeLoan.Text == "") return;
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.BusinessRegistration reg = agent.operation.GetOldBusinessRegistrationByRegistration(txtFeeAccount.Text,txtFeeLoan.Text);
                    lblName.Text = reg.FirstNames + " " + reg.LastName;
                    lblNIN.Text = reg.NIN;
                    lblLoc.Text = agent.operation.GetEntityName(reg.FK_ResidenceIslandLocationId, "isl") + "," + agent.operation.GetEntityName(reg.FK_ResidenceDistrictLocationId, "dis");

                    sbfa.BusinessAccountOld bus = agent.operation.GetOldBusinessAccountByAccount(txtFeeAccount.Text,txtFeeLoan.Text);
                    double bal = bus.Penalty + bus.ProcessingFee + bus.CancellationFee;
                    lblTotal.Text = bal.ToString();
                    lblDue.Text = bal.ToString();
                }
            }
            catch
            {
                //
            }
        }

        private void txtFeeLoan_Leave(object sender, EventArgs e)
        {
            if (txtFeeAccount.Text == "") return;
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.BusinessRegistration reg = agent.operation.GetOldBusinessRegistrationByRegistration(txtFeeAccount.Text, txtFeeLoan.Text);
                    lblName.Text = reg.FirstNames + " " + reg.LastName;
                    lblNIN.Text = reg.NIN;
                    lblLoc.Text = agent.operation.GetEntityName(reg.FK_ResidenceIslandLocationId, "isl") + "," + agent.operation.GetEntityName(reg.FK_ResidenceDistrictLocationId, "dis");

                    sbfa.BusinessAccountOld bus = agent.operation.GetOldBusinessAccountByAccount(txtFeeAccount.Text, txtFeeLoan.Text);
                    double bal = bus.Penalty + bus.ProcessingFee + bus.CancellationFee;
                    lblTotal.Text = bal.ToString();
                    lblDue.Text = bal.ToString();
                }
            }
            catch
            {
                //
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            pnlFees.Visible = false;
        }

        private void txtFeePayAmount_TextChanged(object sender, EventArgs e)
        {
            lblDue.Text = (double.Parse(lblTotal.Text) - double.Parse(txtPayAmount.Text)).ToString();
            lblFeeChange.Text = (double.Parse(lblTotal.Text) - double.Parse(txtPayAmount.Text)).ToString();
            lblPaid.Text = txtPayAmount.Text;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (txtFeePayAmount.Text.Equals(""))
            {
                ShowValidationError("Please enter the amount to be paid.");
                return;
            }
            if (isNumeric(txtFeePayAmount.Text) == false)
            {
                ShowValidationError("Please enter a valid amount to be paid.");
                txtFeePayAmount.Focus();
                return;
            }

            SBFAApi agent = new SBFAApi();
            using (new OperationContextScope(agent.context))
            {
                string rec = agent.operation.OldReceiptFeeRepayment(txtFeeAccount.Text,txtFeeLoan.Text, cmbFeeCurrency.Text, float.Parse(txtFeePayAmount.Text), Globals.GetComboBoxValue(cmbFeeBranch), Globals.GetComboBoxValue(cmbFeePayMethod));
                if (rec != "0")
                {
                    lblReceipt.Text = rec;
                    btnPrint.Visible = true;
                }

                if (rec != "0")
                {
                    lblReceipt.Text = rec;
                    btnPrint.Visible = false;

                    ShowSuccessMessage("Payment successful. The receipt number for this payment is " + lblReceipt.Text);

                    if (chkPrint.Checked == true)
                    {
                        try
                        {
                            printReceipt(rec);
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

        private void printReceipt(string rec)
        {
            SBFAApi agent = new SBFAApi();
            using (new OperationContextScope(agent.context))
            {
                Byte[] doc = agent.operation.GetAutoDocument("receipt", long.Parse(rec));
                string filePath = Application.StartupPath + "\\filer\\" + rec + ".pdf";
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
                printReceipt(lblReceipt.Text);
            }
            catch (Exception ex)
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