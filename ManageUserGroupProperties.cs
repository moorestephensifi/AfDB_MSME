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
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Net;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;

namespace SBFA
{
    public partial class ManageUserGroupProperties : DevExpress.XtraEditors.XtraForm
    {
       
        public ManageUserGroupProperties()
        {
            InitializeComponent();
        }

        private void ManageUserGroupProperties_Load(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    //get application user roles
                    sbfa.ApplicationRoles[] roles = agent.operation.GetApplicationRoles("default");
                    foreach (sbfa.ApplicationRoles role in roles)
                    {
                        string currentRole = role.Name;
                        treeSystemRoles.Nodes["systemRoles"].Nodes.Add(currentRole, currentRole);
                        treeSystemRoles.Nodes["systemRoles"].Nodes[currentRole].Nodes.Add(role.Description);
                    }

                    //get user group roles
                    //get application user group roles
                    sbfa.ApplicationRoleGroups[] rolesGroup = agent.operation.GetApplicationGroupRoles("default");
                    foreach (sbfa.ApplicationRoleGroups role in rolesGroup)
                    {
                        string currentRole = role.Name;
                        treeUserGroups.Nodes["userGroups"].Nodes.Add(currentRole, currentRole);
                        treeUserGroups.Nodes["userGroups"].Nodes[currentRole].Nodes.Add(role.Description);
                    }

                }
            }
            catch
            {
                ShowErrorMessage("Failed to load configuration");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if(txtGroup.Text=="")
            {
                ShowErrorMessage("Provide the group name");
                return;
            }
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    //get application user roles
                    sbfa.UserRoleActionResponse response = agent.operation.AddUserGroup(txtGroup.Text, txtDescription.Text);
                    if (response.actionStatus)
                    {
                        string currentRole = txtGroup.Text;
                        treeUserGroups.Nodes["userGroups"].Nodes.Add(currentRole, currentRole);
                        treeUserGroups.Nodes["userGroups"].Nodes[currentRole].Nodes.Add(txtDescription.Text);
                    }

                }
            }
            catch
            {
                ShowErrorMessage("Failed to add group");
            }
        }

        private void treeUserGroups_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                treeSystemRoles.Nodes["systemRoles"].Nodes.Clear();
                treeUserRoles.Nodes["userSystemRoles"].Nodes.Clear();
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    //get application user roles
                    sbfa.ApplicationRoles[] roles = agent.operation.GetApplicationRoles("default");
                    foreach (sbfa.ApplicationRoles role in roles)
                    {
                        string currentRole = role.Name;
                        treeSystemRoles.Nodes["systemRoles"].Nodes.Add(currentRole, currentRole);
                        treeSystemRoles.Nodes["systemRoles"].Nodes[currentRole].Nodes.Add(role.Description);
                    }
                    //get selected user roles
                    sbfa.ApplicationRoles[] userRoles = agent.operation.GetApplicationUserGroupRoles(treeUserGroups.SelectedNode.Text);
                    foreach (sbfa.ApplicationRoles role in userRoles)
                    {
                        string currentRole = role.Name;
                        treeUserRoles.Nodes["userSystemRoles"].Nodes.Add(currentRole, currentRole);
                        treeUserRoles.Nodes["userSystemRoles"].Nodes[currentRole].Nodes.Add(role.Description);
                        //remove from system roles
                        treeSystemRoles.Nodes["systemRoles"].Nodes[currentRole].Remove();
                    }

                }
            }
            catch
            {
                ShowErrorMessage("Failed to retrieve configuration");
            }
        }

        private void btnAddRole_Click(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.UserRoleActionResponse response = agent.operation.AddUserGroupRole(treeUserGroups.SelectedNode.Text, treeSystemRoles.SelectedNode.Text);
                    if (response.actionStatus)
                    {
                        TreeNode temp = treeSystemRoles.SelectedNode;
                        treeSystemRoles.SelectedNode.Remove();
                        treeUserRoles.Nodes["userSystemRoles"].Nodes.Add(temp);
                    }
                    else
                    {
                        ;
                    }
                }
            }
            catch
            {
                ShowErrorMessage("Failed to update configuration");
            }
        }

        private void btnRemoveRole_Click(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.UserRoleActionResponse response = agent.operation.RemoveUserGroupRole(treeUserGroups.SelectedNode.Text, treeUserRoles.SelectedNode.Text);
                    if (response.actionStatus)
                    {
                        TreeNode temp = treeUserRoles.SelectedNode;
                        treeUserRoles.SelectedNode.Remove();
                        treeSystemRoles.Nodes["systemRoles"].Nodes.Add(temp);
                    }
                    else
                    {
                        ;
                    }
                }
            }
            catch
            {
                ShowErrorMessage("Failed to update configuration");
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
    }
}