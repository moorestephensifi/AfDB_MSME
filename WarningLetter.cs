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
using System.IO;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;

namespace SBFA
{
    public partial class WarningLetter : DevExpress.XtraEditors.XtraForm
    {
        public WarningLetter()
        {
            InitializeComponent();
        }

        private void WarningLetter_Load(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.WarningLetter sch = agent.operation.GetWarningStage(SBFAMain.currentRepaymentId);
                    if (sch.WarningStage == "First" || sch.WarningStage == "Second" || sch.WarningStage == "Final")
                    {
                        if (sch.Responded)
                        {
                            ShowSuccessMessage(sch.FirstRemark + "\n\n" + sch.SecondRemark + "\n\n" + sch.FinalRemark);
                            this.Close();
                        }
                        else
                        {
                            lbltext.Text = ((sch.WarningStage == "First") ? "" : ((sch.WarningStage == "Second") ? "First" : "Second"));
                            if (sch.WarningStage == "Final" && sch.FinalRemark != "")
                            {
                                lbltext.Text = "";
                                btnPay.Enabled = false;
                                lblAccount.Text = "None";
                            }
                            else
                            {
                                lblAccount.Text = sch.WarningStage;
                            }
                            lblRemarks.Text = sch.FirstRemark + "\n\n" + sch.SecondRemark + "\n\n" + sch.FinalRemark;
                        }
                    }
                    else
                    {
                        //check last payment period
                        bool save = agent.operation.SetWarningStage(SBFAMain.currentRepaymentId, Globals.Warning[0], "", false);
                        if (!save)
                        {
                            ShowErrorMessage("Not yet due for warning letter");
                            this.Close();
                        }
                        if (save) lblAccount.Text = Globals.Warning[0];
                    }

                }
            }
            catch
            {
                ShowErrorMessage("There has been a problem initializing form");
            }
        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                DownloadForm(lblAccount.Text);
                this.Close();
            }
            catch
            {
                ShowErrorMessage("Error processing letter");
            }
        }

        private void DownloadForm(string docType)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    Byte[] doc = agent.operation.GetAutoDocument(docType, SBFAMain.currentRepaymentId);
                    string filePath = Application.StartupPath + "\\filer\\" + "repayment" + SBFAMain.currentRepaymentId.ToString() + ".pdf";
                    FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                    fs.Write(doc, 0, doc.Length);
                    fs.Flush();
                    fs.Close();
                    try
                    {
                        System.Diagnostics.Process newProcess = new System.Diagnostics.Process();
                        newProcess.StartInfo = new System.Diagnostics.ProcessStartInfo(filePath);
                        newProcess.Start();
                        newProcess.WaitForExit();
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("Please install a PDF Reader on your computer and try again.");
                    }

                    try
                    {
                        System.IO.File.Delete(filePath);
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Error downloading form");
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if(txtRemark.Text=="")
            {
                return;
            }

            if (lbltext.Text != "")
            {
                try
                {
                    SBFAApi agent = new SBFAApi();
                    using (new OperationContextScope(agent.context))
                    {

                        bool save = agent.operation.SetWarningStage(SBFAMain.currentRepaymentId, lbltext.Text, txtRemark.Text, chkRemark.Checked);
                        if (save)
                        {
                            ShowSuccessMessage("Saved!!");
                        }
                        else
                        {
                            ShowErrorMessage("Error updating remarks!!");
                        }
                    }
                }
                catch
                {
                    ShowErrorMessage("Error saving");
                }
            }
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
            FlyoutAction action = new FlyoutAction() { Caption = "Warning Letter!", Description = message };

            FlyoutCommand command1 = new FlyoutCommand() { Text = "OK", Result = System.Windows.Forms.DialogResult.Yes };
            action.Commands.Add(command1);

            FlyoutProperties properties = new FlyoutProperties();
            properties.ButtonSize = new Size(100, 40);
            properties.Style = FlyoutStyle.MessageBox;
            properties.Appearance.BackColor = Color.Red;
            properties.Appearance.ForeColor = Color.White;
            DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog.Show(this, action, properties);
        }

        public void ShowMessageBox(string caption, string message, string boxType)
        {
            if (boxType.Equals("error"))
            {
                XtraMessageBox.Show(message, message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (boxType.Equals("success"))
            {
                XtraMessageBox.Show(message, message, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (boxType.Equals("warning"))
            {
                XtraMessageBox.Show(message, message, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                XtraMessageBox.Show(message, message, MessageBoxButtons.OK);

            }

        }
    }
}