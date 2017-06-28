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
using System.Net;
using System.ServiceModel;

namespace SEnPA
{
    public partial class ManageEmail : DevExpress.XtraEditors.XtraForm
    {
        senpa.SEnPAClient senpaSys = new senpa.SEnPAClient();
        public ManageEmail()
        {
            InitializeComponent();
        }

        private void ManageEmail_Load(object sender, EventArgs e)
        {
            barEditItemFind.EditValue = "";
        }

        private void barButtonItemFind_ItemClick(object sender, ItemClickEventArgs e)
        {
            lstEmail.Items.Clear();
            //attempt log in           
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.accessToken;

            var context = new OperationContext(senpaSys.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                senpa.Email[] response = senpaSys.GetEmails(barEditItemFind.EditValue.ToString());
                foreach (senpa.Email email in response)
                {
                    string[] row = { email.Id.ToString(), email.Category, email.Destination,email.Subject, ((email.DeliveryCode == 1701) ? "Sent" : "") + " " + email.DeliveryMessage , email.SentOn.ToShortDateString() };
                    var listViewItem = new ListViewItem(row);
                    lstEmail.Items.Add(listViewItem);
                }
            }
        }
    }
}