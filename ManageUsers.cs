using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.Net;

namespace SEnPA
{
    public partial class ManageUsers : DevExpress.XtraEditors.XtraForm
    {
        senpaSecurity.SEnPASecurityClient security = new senpaSecurity.SEnPASecurityClient();
        public static string currentUsername = "";

        public ManageUsers()
        {
            InitializeComponent();
        }

        private void ManageUsers_Load(object sender, EventArgs e)
        {
            txtBarEditUser.EditValue = "";
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            lstUsers.Items.Clear();
            //attempt log in           
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.authorizationKey;

            var context = new OperationContext(security.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                senpaSecurity.ApplicationUsers[] response = security.GetUsers(txtBarEditUser.EditValue.ToString());
                foreach (senpaSecurity.ApplicationUsers user in response)
                {
                    string[] row = { user.Username,user.FirstName +" " + user.Surname, user.RoleGroup,user.MobileNumber,user.EmailAddress, ((user.Active) ? "Yes" : "No"), ((user.Locked) ? "Yes" : "No"), ((user.PasswordExpires) ? "Yes" : "No"), ((user.PasswordExpires) ? user.PasswordExpiryDate.ToShortDateString() : "") };
                    var listViewItem = new ListViewItem(row);
                    lstUsers.Items.Add(listViewItem);
                }
            }
        }

        private void barButtonItemFind_ItemClick(object sender, ItemClickEventArgs e)
        {
            lstUsers.Items.Clear();
            //attempt log in           
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.authorizationKey;

            var context = new OperationContext(security.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                senpaSecurity.ApplicationUsers[] response = security.GetUsers(txtBarEditUser.EditValue.ToString());
                foreach (senpaSecurity.ApplicationUsers user in response)
                {
                    string[] row = { user.Username, user.FirstName + " " + user.Surname, user.RoleGroup,user.MobileNumber,user.EmailAddress, ((user.Active) ? "Yes" : "No"), ((user.Locked) ? "Yes" : "No"), ((user.PasswordExpires) ? "Yes" : "No"), ((user.PasswordExpires) ? user.PasswordExpiryDate.ToShortDateString() : "") };
                    var listViewItem = new ListViewItem(row);
                    lstUsers.Items.Add(listViewItem);
                }
            }
        }

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                currentUsername = lstUsers.SelectedItems[0].SubItems[0].Text;
            }
            catch(Exception ex)
            {

            }
        }

        private void barButtonItemEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            new ManagerUserProperties().ShowDialog();
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            new AddUser().ShowDialog();
        }
    }
}