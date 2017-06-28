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
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace SEnPA
{
    public partial class RegistrationView : DevExpress.XtraEditors.XtraForm
    {
        senpa.SEnPAClient senpaSys = new senpa.SEnPAClient();
        public static long currentId = 0;
        public RegistrationView()
        {
            InitializeComponent();
        }

        private void RegistrationView_Load(object sender, EventArgs e)
        {
            barEditItemSearch.EditValue = "";
        }

        private void barButtonItemSearch_ItemClick(object sender, ItemClickEventArgs e)
        {
            lstRegistrations.Items.Clear();
            //attempt log in           
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.accessToken;

            var context = new OperationContext(senpaSys.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                senpa.RegistrationRequest[] response = senpaSys.GetRegistrationRequests(barEditItemSearch.EditValue.ToString());
                foreach (senpa.RegistrationRequest registration in response)
                {
                    string[] row = { registration.Id.ToString(), registration.ReferenceNumber, registration.BusinessRegistrationNumber, registration.BusinessName, registration.NIN, registration.FirstNames, registration.LastName, registration.Citizenship, registration.Status };
                    var listViewItem = new ListViewItem(row);
                    lstRegistrations.Items.Add(listViewItem);
                }
            }
        }

        private void lstRegistrations_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                currentId = long.Parse(lstRegistrations.SelectedItems[0].SubItems[0].Text);
            }
            catch (Exception ex)
            {
                
            }
        }

        private void barButtonItemEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            new Registration().ShowDialog();
        }
    }
}