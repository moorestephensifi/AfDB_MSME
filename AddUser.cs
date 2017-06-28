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
    public partial class AddUser : DevExpress.XtraEditors.XtraForm
    {
        senpaSecurity.SEnPASecurityClient security = new senpaSecurity.SEnPASecurityClient();
        public AddUser()
        {
            InitializeComponent();
        }

        private void groupControl3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void AddUser_Load(object sender, EventArgs e)
        {
            txtPassword.ReadOnly = true;
            txtConfirm.ReadOnly = true;
            txtUsername.ReadOnly = true;
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.authorizationKey;

            var context = new OperationContext(security.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                //get application user group roles
                senpaSecurity.ApplicationRoleGroups[] rolesGroup = security.GetApplicationGroupRoles("default");
                foreach (senpaSecurity.ApplicationRoleGroups role in rolesGroup)
                {
                    cmbRoleGroups.Items.Add(role.RoleGroup);
                }
            }
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            if (chkAutoUsername.Checked && txtName.Text.Length>0)
                txtUsername.Text = (txtName.Text.Substring(0, 1) + txtSurname.Text).ToLower();
        }

        private void txtSurname_TextChanged(object sender, EventArgs e)
        {
            if (chkAutoUsername.Checked && txtName.Text.Length > 0)
                txtUsername.Text = (txtName.Text.Substring(0, 1) + txtSurname.Text).ToLower();
        }

        private void chkAutoUsername_CheckedChanged(object sender, EventArgs e)
        {
            txtUsername.ReadOnly = chkAutoUsername.Checked;
        }

        private void chkDefaultPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.ReadOnly = chkDefaultPassword.Checked;
            txtConfirm.ReadOnly = chkDefaultPassword.Checked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.authorizationKey;

            var context = new OperationContext(security.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                string password = "";
                if (chkDefaultPassword.Checked)
                    password = security.DefaultPassword();
                else
                    password = txtPassword.Text;
                senpaSecurity.UserActionResponse response = security.AddUser(txtUsername.Text, password, cmbRoleGroups.Text, txtName.Text, txtSurname.Text, txtEmail.Text, txtMobile.Text, chkPasswordExpires.Checked, chkActive.Checked, chkLocked.Checked);
                if (response.actionStatus)
                {
                    txtName.Text = "";
                    txtSurname.Text = "";
                    txtUsername.Text = "";
                    txtEmail.Text = "";
                    txtMobile.Text = "";
                    txtPassword.Text = "";
                }
                else
                {
                    MessageBox.Show(response.responseMessage);
                }
            }
        }
    }
}