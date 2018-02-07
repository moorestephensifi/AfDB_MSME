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
    public partial class ManageStageAutoDocuments : DevExpress.XtraEditors.XtraForm
    {
        long currentId = 0;
        public ManageStageAutoDocuments()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    long wrk = agent.operation.CreateWorkFlowStagesAutoDocument(ManageStages.currentWorkFlowStage, cmbTemplate.SelectedValue.ToString(), Globals.GetComboBoxValue(cmbEmail), Globals.GetComboBoxValue(cmbSMS), chkActive.Checked);
                    if (wrk > -1)
                    {
                        lstDocuments.Items.Clear();
                        sbfa.WorkFlowStagesAutoDocuments[] response = agent.operation.GetWorkFlowStagesAutoDocuments(ManageStages.currentWorkFlowStage);
                        foreach (sbfa.WorkFlowStagesAutoDocuments wrkFlow in response)
                        {
                            string[] row = { wrkFlow.Id.ToString(), wrkFlow.FK_AutoDocumentName, ((wrkFlow.SendEmail) == -1 ? "No" : ((wrkFlow.SendEmail) == 0 ? "On Enter" : "On Leave")), ((wrkFlow.SendSMS) == -1 ? "No" : ((wrkFlow.SendSMS) == 0 ? "On Enter" : "On Leave")) };
                            var listViewItem = new ListViewItem(row);
                            lstDocuments.Items.Add(listViewItem);
                        }
                    }
                }
            }
            catch
            {
                ShowErrorMessage("Error saving your stage document config");
            }
        }

        private void ManageStageAutoDocuments_Load(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    lstDocuments.Items.Clear();
                    sbfa.WorkFlowStagesAutoDocuments[] response = agent.operation.GetWorkFlowStagesAutoDocuments(ManageStages.currentWorkFlowStage);
                    foreach (sbfa.WorkFlowStagesAutoDocuments wrkFlow in response)
                    {
                        string[] row = { wrkFlow.Id.ToString(), wrkFlow.FK_AutoDocumentName, ((wrkFlow.SendEmail) == -1 ? "No" : ((wrkFlow.SendEmail) == 0 ? "On Enter" : "On Leave")), ((wrkFlow.SendSMS) == -1 ? "No" : ((wrkFlow.SendSMS) == 0 ? "On Enter" : "On Leave")) };
                        var listViewItem = new ListViewItem(row);
                        lstDocuments.Items.Add(listViewItem);
                    }
                }

                Globals.SetStageMessagingPickList(cmbEmail);
                Globals.SetStageMessagingPickList(cmbSMS);
                Globals.SetAutoDocumentPickList(cmbTemplate);
            }
            catch
            {
                ShowErrorMessage("Failed to properly initialize documents config");
            }
        }

        private void lstDocuments_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {

                    sbfa.WorkFlowStagesAutoDocuments response = agent.operation.GetWorkFlowStagesAutoDocument(long.Parse(lstDocuments.SelectedItems[0].SubItems[0].Text));
                    currentId = response.Id;
                    Globals.SetPickListValue(cmbEmail, response.SendEmail);
                    Globals.SetPickListValue(cmbSMS, response.SendSMS);
                    Globals.SetPickListValue(cmbTemplate, response.FK_AutoDocumentName);
                    chkActive.Checked = response.Active;

                }
            }
            catch
            {
                ShowErrorMessage("Error fetching record details");
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
            FlyoutAction action = new FlyoutAction() { Caption = "Auto Documents!", Description = message };

            FlyoutCommand command1 = new FlyoutCommand() { Text = "OK", Result = System.Windows.Forms.DialogResult.Yes };
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