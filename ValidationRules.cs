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
    public partial class ValidationRules : DevExpress.XtraEditors.XtraForm
    {
        string currentDoc = "";
        long currentId = 0;
        public ValidationRules()
        {
            InitializeComponent();
        }

        private void ValidationRules_Load(object sender, EventArgs e)
        {
            Globals.SetDataTypePickList(cmbDataType);
        }

        private void treeDocuments_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    if (treeDocuments.SelectedNode.Text.ToLower() != "documents")
                    {
                        currentDoc = treeDocuments.SelectedNode.Name;
                        Globals.SetFieldsPickList(cmbField, currentDoc);

                        lstRules.Items.Clear();
                        sbfa.WorkFlowFieldValidations[] response = agent.operation.GetValidationsList(currentDoc);
                        foreach (sbfa.WorkFlowFieldValidations rule in response)
                        {
                            string[] row = { rule.Id.ToString(), rule.ParameterField, rule.ParameterFieldName, rule.ParameterDataType, rule.ParameterValue, rule.ParameterMaxValue, rule.ParameterEvaluationType, ((rule.Active) ? "Yes" : "No") };
                            var listViewItem = new ListViewItem(row);
                            lstRules.Items.Add(listViewItem);
                        }

                    }
                }
            }
            catch
            {
                ShowErrorMessage("Failed to retrieve details");
            }
        }

        private void cmbDataType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Globals.SetEvaluationPickList(cmbEvaluationType, cmbDataType.SelectedValue.ToString());
        }

        private void lstRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {

                    chkNew.Checked = false;
                    currentId = long.Parse(lstRules.SelectedItems[0].SubItems[0].Text);
                    sbfa.WorkFlowFieldValidations response = agent.operation.GetValidation(currentId);
                    Globals.SetPickListValue(cmbField, response.ParameterField);
                    chkActive.Checked = response.Active;
                    Globals.SetPickListValue(cmbDataType, response.ParameterDataType);
                    txtText.Text = response.ParameterFieldName;
                    txtValue.Text = response.ParameterValue;
                    txtValueMax.Text = response.ParameterMaxValue;
                    Globals.SetPickListValue(cmbEvaluationType, response.ParameterEvaluationType);

                }
            }
            catch
            {
                ShowErrorMessage("Failed to retrieve rule details");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtValue.Text=="" || txtValueMax.Text=="")
            {
                ShowErrorMessage("please set the value and max value");
                return;
            }
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {

                    long save = agent.operation.SaveValidation(((chkNew.Checked) ? 0 : currentId), currentDoc, cmbField.SelectedValue.ToString(), cmbDataType.SelectedValue.ToString(), txtText.Text, txtValue.Text, txtValueMax.Text, cmbEvaluationType.SelectedValue.ToString(), chkActive.Checked);
                    if (save > 0)
                    {
                        currentId = save;
                        chkNew.Checked = false;
                        string[] row = { save.ToString(), cmbField.SelectedText, txtText.Text, cmbDataType.SelectedText, txtValue.Text, txtValueMax.Text, cmbEvaluationType.SelectedText, ((chkActive.Checked) ? "Yes" : "No") };
                        var listViewItem = new ListViewItem(row);
                        lstRules.Items.Add(listViewItem);
                    }

                }
            }
            catch {

                ShowErrorMessage("Error saving rule");
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
            FlyoutAction action = new FlyoutAction() { Caption = "Rules", Description = message };

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