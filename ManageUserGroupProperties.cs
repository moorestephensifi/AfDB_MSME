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

namespace SEnPA
{
    public partial class ManageUserGroupProperties : DevExpress.XtraEditors.XtraForm
    {
        senpaSecurity.SEnPASecurityClient security = new senpaSecurity.SEnPASecurityClient();
        public ManageUserGroupProperties()
        {
            InitializeComponent();
        }

        private void ManageUserGroupProperties_Load(object sender, EventArgs e)
        {
            //attempt 
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.authorizationKey;

            var context = new OperationContext(security.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
               
                //get application user roles
                senpaSecurity.ApplicationRoles[] roles = security.GetApplicationRoles("default");
                foreach (senpaSecurity.ApplicationRoles role in roles)
                {
                    string currentRole = role.RoleName;
                    treeSystemRoles.Nodes["systemRoles"].Nodes.Add(currentRole, currentRole);
                    treeSystemRoles.Nodes["systemRoles"].Nodes[currentRole].Nodes.Add(role.Description);
                }
               
                //get user group roles
                //get application user group roles
                senpaSecurity.ApplicationRoleGroups[] rolesGroup = security.GetApplicationGroupRoles("default");
                foreach (senpaSecurity.ApplicationRoleGroups role in rolesGroup)
                {
                    string currentRole = role.RoleGroup;
                    treeUserGroups.Nodes["userGroups"].Nodes.Add(currentRole, currentRole);
                    treeUserGroups.Nodes["userGroups"].Nodes[currentRole].Nodes.Add(role.Description);
                }
               
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //attempt 
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.authorizationKey;

            var context = new OperationContext(security.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;

                //get application user roles
                senpaSecurity.UserRoleActionResponse response = security.AddUserGroup(txtGroup.Text, txtDescription.Text);
                if(response.actionStatus)
                {
                    string currentRole = txtGroup.Text;
                    treeUserGroups.Nodes["userGroups"].Nodes.Add(currentRole, currentRole);
                    treeUserGroups.Nodes["userGroups"].Nodes[currentRole].Nodes.Add(txtDescription.Text);
                }

            }
        }

        private void treeUserGroups_AfterSelect(object sender, TreeViewEventArgs e)
        {
            treeSystemRoles.Nodes["systemRoles"].Nodes.Clear();
            treeUserRoles.Nodes["userSystemRoles"].Nodes.Clear();

            //attempt 
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.authorizationKey;

            var context = new OperationContext(security.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                //get application user roles
                senpaSecurity.ApplicationRoles[] roles = security.GetApplicationRoles("default");
                foreach (senpaSecurity.ApplicationRoles role in roles)
                {
                    string currentRole = role.RoleName;
                    treeSystemRoles.Nodes["systemRoles"].Nodes.Add(currentRole, currentRole);
                    treeSystemRoles.Nodes["systemRoles"].Nodes[currentRole].Nodes.Add(role.Description);
                }
                //get selected user roles
                senpaSecurity.ApplicationRoles[] userRoles = security.GetApplicationUserGroupRoles(treeUserGroups.SelectedNode.Text);
                foreach (senpaSecurity.ApplicationRoles role in userRoles)
                {
                    string currentRole = role.RoleName;
                    treeUserRoles.Nodes["userSystemRoles"].Nodes.Add(currentRole, currentRole);
                    treeUserRoles.Nodes["userSystemRoles"].Nodes[currentRole].Nodes.Add(role.Description);
                    //remove from system roles
                    treeSystemRoles.Nodes["systemRoles"].Nodes[currentRole].Remove();
                }

            }
        }

        private void btnAddRole_Click(object sender, EventArgs e)
        {
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.authorizationKey;

            var context = new OperationContext(security.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                senpaSecurity.UserRoleActionResponse response = security.AddUserGroupRole(treeUserGroups.SelectedNode.Text, treeSystemRoles.SelectedNode.Text);
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

        private void btnRemoveRole_Click(object sender, EventArgs e)
        {
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.authorizationKey;

            var context = new OperationContext(security.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                senpaSecurity.UserRoleActionResponse response = security.RemoveUserGroupRole(treeUserGroups.SelectedNode.Text, treeUserRoles.SelectedNode.Text);
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
    }
}