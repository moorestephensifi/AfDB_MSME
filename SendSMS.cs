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
    public partial class SendSMS : DevExpress.XtraEditors.XtraForm
    {
        public SendSMS()
        {
            InitializeComponent();
        }

        private void SendSMS_Load(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {

                    //get user group roles
                    //get application user group roles
                    sbfa.ApplicationRoleGroups[] rolesGroup = agent.operation.GetApplicationGroupRoles("default");
                    foreach (sbfa.ApplicationRoleGroups role in rolesGroup)
                    {
                        string currentRole = role.Name;
                        treeSystemRoles.Nodes["systemGroupRoles"].Nodes.Add(currentRole, currentRole);
                        treeSystemRoles.Nodes["systemGroupRoles"].Nodes[currentRole].Nodes.Add(role.Description);
                    }

                    if (SBFAMain.forAccount)
                    {
                        lblName.Text = SBFAMain.name;
                        txtDestination.Enabled = false;
                        txtDestination.Text = SBFAMain.mobile;
                    }
                    else
                    {
                        lblName.Text = "Send messages";
                    }
                }
            }
            catch
            {
                ShowErrorMessage("Error initializing sms form");
            }
        }

        private void btnAddRole_Click(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    TreeNode temp = treeSystemRoles.SelectedNode;
                    treeSystemRoles.SelectedNode.Remove();
                    treeUserRoles.Nodes["userSystemGroupRoles"].Nodes.Add(temp);
                }
            }
            catch
            {
                ShowErrorMessage("Error adding group");
            }
        }

        private void btnRemoveRole_Click(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    TreeNode temp = treeUserRoles.SelectedNode;
                    treeUserRoles.SelectedNode.Remove();
                    treeSystemRoles.Nodes["systemGroupRoles"].Nodes.Add(temp);
                }
            }
            catch
            {
                ShowErrorMessage("Error removing group");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    long all = 0;
                    if (txtDestination.Text.Trim() != "")
                    {
                        string[] temp = txtDestination.Text.Trim().Split(new Char[] { ',', ';' });
                        for (int a = 0; a < temp.Length; a++)
                        {
                            bool sent = agent.operation.SendSMS(temp[a], txtMsg.Text);
                            if (!sent) all++;
                        }
                    }

                    //send to groups selected
                    TreeNodeCollection tempGroup = treeUserRoles.Nodes["userSystemGroupRoles"].Nodes;
                    for (int a = 0; a < tempGroup.Count; a++)
                    {
                        long sent = agent.operation.SendGroupSMS(tempGroup[a].Text, txtMsg.Text);
                        all += sent;
                    }

                    ShowSuccessMessage(all.ToString() + " Failed to send!");
                }
            }
            catch
            {
                ShowErrorMessage("Error sending sms");
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
            FlyoutAction action = new FlyoutAction() { Caption = "SMS", Description = message };

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