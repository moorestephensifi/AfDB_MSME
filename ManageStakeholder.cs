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
    public partial class ManageStakeholder : DevExpress.XtraEditors.XtraForm
    {
        int currentStakeholder = 0;
        public ManageStakeholder()
        {
            InitializeComponent();
        }

        private void treeStakeholder_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    if (treeStakeholder.SelectedNode.Text.ToLower() != "stakeholders")
                    {
                        currentStakeholder = int.Parse(treeStakeholder.SelectedNode.Name.Split('_')[1]);
                        sbfa.Stakeholder stake = agent.operation.GetStakeholder(currentStakeholder);

                        txtName.Text = stake.Name;
                        txtDescription.Text = stake.Description;
                        txtMobile.Text = stake.Mobile;
                        txtEmail.Text = stake.Email;
                        chkActive.Checked = stake.Active;

                        lstBusiness.Items.Clear();
                        sbfa.ReferenceTable[] response = agent.operation.GetStakeholderBusinessTypes(currentStakeholder);
                        foreach (sbfa.ReferenceTable busType in response)
                        {
                            string[] row = { busType.Id.ToString(), busType.Name, busType.Description };
                            var listViewItem = new ListViewItem(row);
                            lstBusiness.Items.Add(listViewItem);
                        }

                    }
                }
            }
            catch
            {
                ShowErrorMessage("Error retrieving stakeholder record");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtName.Text=="")
            {
                ShowErrorMessage("Please specify the name");
                return;
            }
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    long stake = agent.operation.SaveStakeholder(txtName.Text, txtDescription.Text, txtMobile.Text, txtEmail.Text, chkActive.Checked);
                    if (stake > 0)
                    {
                        lstBusiness.Items.Clear();
                        string currentStake = "_" + stake.ToString();
                        treeStakeholder.Nodes["stakeHolder"].Nodes.Add(currentStake, txtName.Text);
                    }
                }
            }
            catch
            {
                ShowErrorMessage("Error saving stakeholder");
            }
        }

        private void ManageStakeholder_Load(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.Stakeholder[] response = agent.operation.GetStakeholders();
                    foreach (sbfa.Stakeholder stake in response)
                    {
                        string currentStake = "_" + stake.Id.ToString();
                        treeStakeholder.Nodes["stakeHolder"].Nodes.Add(currentStake, stake.Name);
                    }

                }
            }
            catch
            {
                ShowErrorMessage("Error in initializing form");
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
            FlyoutAction action = new FlyoutAction() { Caption = "Stakeholders!", Description = message };

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