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

namespace SEnPA
{
    public partial class ManagerUserProperties : DevExpress.XtraEditors.XtraForm
    {
        senpaSecurity.SEnPASecurityClient security = new senpaSecurity.SEnPASecurityClient();
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
            //attempt 
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.authorizationKey;

            var context = new OperationContext(security.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                senpaSecurity.ApplicationUsers response = security.GetUser(ManageUsers.currentUsername);
                lblUsername.Text = ManageUsers.currentUsername;
                chkActive.Checked = response.Active;
                chkExpires.Checked = response.PasswordExpires;
                chkLocked.Checked = response.Locked;
                lblChanged.Text = response.PasswordLastChanged.ToShortDateString();
                lblExpiry.Text = response.PasswordExpiryDate.ToShortDateString();
                txtEmail.Text = response.EmailAddress;
                txtMobile.Text = response.MobileNumber;
                txtName.Text = response.FirstName;
                txtSurname.Text = response.Surname;

                //get application user roles
                senpaSecurity.ApplicationRoles[] roles = security.GetApplicationRoles("default");
                foreach (senpaSecurity.ApplicationRoles role in roles)
                {
                    string currentRole = role.RoleName;
                    treeSystemRoles.Nodes["systemRoles"].Nodes.Add(currentRole, currentRole);
                    treeSystemRoles.Nodes["systemRoles"].Nodes[currentRole].Nodes.Add(role.Description);
                }
                //get selected user roles
                senpaSecurity.ApplicationRoles[] userRoles = security.GetApplicationRoles(ManageUsers.currentUsername);
                foreach (senpaSecurity.ApplicationRoles role in userRoles)
                {
                    string currentRole = role.RoleName;
                    treeUserRoles.Nodes["userSystemRoles"].Nodes.Add(currentRole, currentRole);
                    treeUserRoles.Nodes["userSystemRoles"].Nodes[currentRole].Nodes.Add(role.Description);
                    //remove from system roles
                    treeSystemRoles.Nodes["systemRoles"].Nodes[currentRole].Remove();
                }
                //get user group roles
                //get application user group roles
                senpaSecurity.ApplicationRoleGroups[] rolesGroup = security.GetApplicationGroupRoles("default");
                foreach (senpaSecurity.ApplicationRoleGroups role in rolesGroup)
                {
                    string currentRole = role.RoleGroup;
                    treeSystemRoles.Nodes["systemGroupRoles"].Nodes.Add(currentRole, currentRole);
                    treeSystemRoles.Nodes["systemGroupRoles"].Nodes[currentRole].Nodes.Add(role.Description);
                }
                //get selected user roles
                senpaSecurity.ApplicationRoleGroups[] userRolesGroup = security.GetApplicationGroupRoles(ManageUsers.currentUsername);
                foreach (senpaSecurity.ApplicationRoleGroups role in userRolesGroup)
                {
                    string currentRole = role.RoleGroup;
                    treeUserRoles.Nodes["userSystemGroupRoles"].Nodes.Add(currentRole, currentRole);
                    treeUserRoles.Nodes["userSystemGroupRoles"].Nodes[currentRole].Nodes.Add(role.Description);
                    //remove from system roles
                    treeSystemRoles.Nodes["systemGroupRoles"].Nodes[currentRole].Remove();
                }
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
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.authorizationKey;

            var context = new OperationContext(security.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                TreeNode temp = treeSystemRoles.SelectedNode;
                senpaSecurity.UserRoleActionResponse response;
                if (FindRootNode(temp).Name == "systemRoles")
                {
                    response = security.AddRole(ManageUsers.currentUsername, treeSystemRoles.SelectedNode.Text);
                }
                else
                {
                    response = security.AddGroupRole(ManageUsers.currentUsername, treeSystemRoles.SelectedNode.Text);
                }
                if (response.actionStatus)
                {                   
                    if (FindRootNode(temp).Name== "systemRoles")
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

        private void btnRemoveRole_Click(object sender, EventArgs e)
        {
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.authorizationKey;

            var context = new OperationContext(security.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                TreeNode temp = treeUserRoles.SelectedNode;
                senpaSecurity.UserRoleActionResponse response;
                if (FindRootNode(temp).Name == "userSystemRoles")
                {
                    response = security.RemoveRole(ManageUsers.currentUsername, treeUserRoles.SelectedNode.Text);
                }
                else
                {
                    response = security.RemoveGroupRole(ManageUsers.currentUsername, treeUserRoles.SelectedNode.Text);
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

        private void chkActive_CheckedChanged(object sender, EventArgs e)
        {
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.authorizationKey;

            var context = new OperationContext(security.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                senpaSecurity.UserActionResponse response = security.UpdateUser(ManageUsers.currentUsername, ((chkActive.Checked)? "enable" : "disable"));
                chkActive.Checked = response.actionStatus;
            }
        }

        private void chkLocked_CheckedChanged(object sender, EventArgs e)
        {
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.authorizationKey;

            var context = new OperationContext(security.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                senpaSecurity.UserActionResponse response = security.UpdateUser(ManageUsers.currentUsername, ((chkLocked.Checked) ? "lock" : "unlock"));
                chkLocked.Checked = response.actionStatus;
            }
        }

        private void chkExpires_CheckedChanged(object sender, EventArgs e)
        {
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.authorizationKey;

            var context = new OperationContext(security.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                senpaSecurity.UserActionResponse response = security.UpdateUser(ManageUsers.currentUsername, ((chkExpires.Checked) ? "expire" : "notexpire"));
                chkExpires.Checked = response.actionStatus;
            }
        }
    }
}