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
    public partial class AddUser : DevExpress.XtraEditors.XtraForm
    {
       
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
            try
            {
                txtPassword.ReadOnly = true;
                txtConfirm.ReadOnly = true;
                txtUsername.ReadOnly = true;
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    //get application user group roles
                    sbfa.ApplicationRoleGroups[] rolesGroup = agent.operation.GetApplicationGroupRoles("default");
                    foreach (sbfa.ApplicationRoleGroups role in rolesGroup)
                    {
                        cmbRoleGroups.Items.Add(role.Name);
                    }
                    Globals.SetPickList(cmbStakeholder, "stahol");
                }
            }
            catch
            {
                ShowErrorMessage("There has been an error initializing user");
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
            if(txtUsername.Text=="" || txtName.Text=="")
            {
                ShowErrorMessage("Please enter all main fields");
                return;
            }

            if(!chkDefaultPassword.Checked && (txtPassword.Text!=txtConfirm.Text))
            {
                ShowErrorMessage("Password mismatch");
                return;
            }
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    string password = "";
                    if (chkDefaultPassword.Checked)
                        password = agent.operation.DefaultPassword();
                    else
                        password = txtPassword.Text;
                    sbfa.UserActionResponse response = agent.operation.AddUser(txtUsername.Text, password, Globals.GetComboBoxValue(cmbStakeholder), cmbRoleGroups.Text, txtName.Text, txtSurname.Text, txtEmail.Text, txtMobile.Text, chkPasswordExpires.Checked, chkActive.Checked, chkLocked.Checked);
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
                        ShowErrorMessage(response.responseMessage);
                    }
                }
            }
            catch
            {
                ShowErrorMessage("There has been an error saving user");
            }
        }

        private void txtName_TextChanged_1(object sender, EventArgs e)
        {
            if (chkAutoUsername.Checked && txtName.Text.Length > 0)
                txtUsername.Text = (txtName.Text.Substring(0, 1) + txtSurname.Text).ToLower();
        }

        private void txtSurname_TextChanged_1(object sender, EventArgs e)
        {
            if (chkAutoUsername.Checked && txtName.Text.Length > 0)
                txtUsername.Text = (txtName.Text.Substring(0, 1) + txtSurname.Text).ToLower();
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