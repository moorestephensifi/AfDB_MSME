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
using System.Net;
using System.ServiceModel;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;

namespace SBFA
{
    public partial class ManagerUserProperties : DevExpress.XtraEditors.XtraForm
    {
       
        public ManagerUserProperties()
        {
            InitializeComponent();
        }

        private TreeNode FindRootNode(TreeNode treeNode)
        {
            while (treeNode.Parent != null)
            {
                treeNode = treeNode.Parent;
            }
            return treeNode;
        }

        private void ManagerUserProperties_Load(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.ApplicationUsers response = agent.operation.GetUser(SBFAMain.currentUsername);
                    lblUsername.Text = SBFAMain.currentUsername;
                    chkActive.Checked = response.Active;
                    chkExpires.Checked = response.PasswordExpires;
                    chkLocked.Checked = response.Locked;
                    lblChanged.Text = response.PasswordLastChanged.ToShortDateString();
                    lblExpiry.Text = response.PasswordExpiryDate.ToShortDateString();
                    txtEmail.Text = response.EmailAddress;
                    txtMobile.Text = response.MobileNumber;
                    txtName.Text = response.FirstName;
                    txtSurname.Text = response.Surname;
                    Globals.SetPickList(cmbStakeholder, "stahol");
                    Globals.SetPickListValue(cmbStakeholder, response.FK_StakeholderId);

                    //get application user roles
                    sbfa.ApplicationRoles[] roles = agent.operation.GetApplicationRoles("default");
                    foreach (sbfa.ApplicationRoles role in roles)
                    {
                        string currentRole = role.Name;
                        treeSystemRoles.Nodes["systemRoles"].Nodes.Add(currentRole, currentRole);
                        treeSystemRoles.Nodes["systemRoles"].Nodes[currentRole].Nodes.Add(role.Description);
                    }
                    //get selected user roles
                    sbfa.ApplicationRoles[] userRoles = agent.operation.GetApplicationRoles(SBFAMain.currentUsername);
                    foreach (sbfa.ApplicationRoles role in userRoles)
                    {
                        string currentRole = role.Name;
                        treeUserRoles.Nodes["userSystemRoles"].Nodes.Add(currentRole, currentRole);
                        treeUserRoles.Nodes["userSystemRoles"].Nodes[currentRole].Nodes.Add(role.Description);
                        //remove from system roles
                        treeSystemRoles.Nodes["systemRoles"].Nodes[currentRole].Remove();
                    }
                    //get user group roles
                    //get application user group roles
                    sbfa.ApplicationRoleGroups[] rolesGroup = agent.operation.GetApplicationGroupRoles("default");
                    foreach (sbfa.ApplicationRoleGroups role in rolesGroup)
                    {
                        string currentRole = role.Name;
                        treeSystemRoles.Nodes["systemGroupRoles"].Nodes.Add(currentRole, currentRole);
                        treeSystemRoles.Nodes["systemGroupRoles"].Nodes[currentRole].Nodes.Add(role.Description);
                    }
                    //get selected user roles
                    sbfa.ApplicationRoleGroups[] userRolesGroup = agent.operation.GetApplicationGroupRoles(SBFAMain.currentUsername);
                    foreach (sbfa.ApplicationRoleGroups role in userRolesGroup)
                    {
                        string currentRole = role.Name;
                        treeUserRoles.Nodes["userSystemGroupRoles"].Nodes.Add(currentRole, currentRole);
                        treeUserRoles.Nodes["userSystemGroupRoles"].Nodes[currentRole].Nodes.Add(role.Description);
                        //remove from system roles
                        treeSystemRoles.Nodes["systemGroupRoles"].Nodes[currentRole].Remove();
                    }
                }
            }
            catch
            {
                ShowErrorMessage("Failed to initialize status");
            }
        }

        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void treeSystemRoles_AfterSelect(object sender, TreeViewEventArgs e)
        {            
            try
            {
                lblDescription.Text = treeSystemRoles.SelectedNode.Nodes[treeSystemRoles.SelectedNode.Text].Text;
            }
            catch { }
        }

        private void treeUserRoles_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                lblDescription.Text = treeUserRoles.SelectedNode.Nodes[treeUserRoles.SelectedNode.Text].Text;
            }
            catch { }
        }

        private void btnAddRole_Click(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    TreeNode temp = treeSystemRoles.SelectedNode;
                    sbfa.UserRoleActionResponse response;
                    if (FindRootNode(temp).Name == "systemRoles")
                    {
                        response = agent.operation.AddRole(SBFAMain.currentUsername, treeSystemRoles.SelectedNode.Text);
                    }
                    else
                    {
                        response = agent.operation.AddGroupRole(SBFAMain.currentUsername, treeSystemRoles.SelectedNode.Text);
                    }
                    if (response.actionStatus)
                    {
                        if (FindRootNode(temp).Name == "systemRoles")
                        {
                            treeSystemRoles.SelectedNode.Remove();
                            treeUserRoles.Nodes["userSystemRoles"].Nodes.Add(temp);
                        }
                        else
                        {
                            treeSystemRoles.SelectedNode.Remove();
                            treeUserRoles.Nodes["userSystemGroupRoles"].Nodes.Add(temp);
                        }
                    }
                    else
                    {
                        ;
                    }
                }
            }
            catch
            {
                ShowErrorMessage("Failed to update status");
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
                    sbfa.UserRoleActionResponse response;
                    if (FindRootNode(temp).Name == "userSystemRoles")
                    {
                        response = agent.operation.RemoveRole(SBFAMain.currentUsername, treeUserRoles.SelectedNode.Text);
                    }
                    else
                    {
                        response = agent.operation.RemoveGroupRole(SBFAMain.currentUsername, treeUserRoles.SelectedNode.Text);
                    }
                    if (response.actionStatus)
                    {

                        if (FindRootNode(temp).Name == "userSystemRoles")
                        {
                            treeUserRoles.SelectedNode.Remove();
                            treeSystemRoles.Nodes["systemRoles"].Nodes.Add(temp);
                        }
                        else
                        {
                            treeUserRoles.SelectedNode.Remove();
                            treeSystemRoles.Nodes["systemGroupRoles"].Nodes.Add(temp);
                        }

                    }
                    else
                    {
                        ;
                    }
                }
            }
            catch
            {
                ShowErrorMessage("Failed to update status");
            }
        }

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            try { 
            SBFAApi agent = new SBFAApi();
            using (new OperationContextScope(agent.context))
            {
                sbfa.UserActionResponse response = agent.operation.UpdateUser(SBFAMain.currentUsername, ((chkActive.Checked)? "enable" : "disable"));
                chkActive.Checked = response.actionStatus;
            }
            }
            catch
            {
                ShowErrorMessage("Failed to update status");
            }
        }

        private void chkLocked_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.UserActionResponse response = agent.operation.UpdateUser(SBFAMain.currentUsername, ((chkLocked.Checked) ? "lock" : "unlock"));
                    chkLocked.Checked = response.actionStatus;
                }
            }
            catch
            {
                ShowErrorMessage("Failed to update status");
            }
        }

        private void chkExpires_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.UserActionResponse response = agent.operation.UpdateUser(SBFAMain.currentUsername, ((chkExpires.Checked) ? "expire" : "notexpire"));
                    chkExpires.Checked = response.actionStatus;
                }
            }
            catch
            {
                ShowErrorMessage("Failed to update status");
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
            FlyoutAction action = new FlyoutAction() { Caption = "User!", Description = message };

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