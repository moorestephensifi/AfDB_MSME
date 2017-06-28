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
    public partial class ManageSMS : DevExpress.XtraEditors.XtraForm
    {
        senpa.SEnPAClient senpaSys = new senpa.SEnPAClient();
        public ManageSMS()
        {
            InitializeComponent();
        }

        private void ManageSMS_Load(object sender, EventArgs e)
        {
            barEditItemFind.EditValue = "";
        }

        private void barButtonItemFind_ItemClick(object sender, ItemClickEventArgs e)
        {
            lstSMS.Items.Clear();
            //attempt log in           
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.accessToken;

            var context = new OperationContext(senpaSys.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                senpa.SMS[] response = senpaSys.GetSMSs(barEditItemFind.EditValue.ToString());
                foreach (senpa.SMS sms in response)
                {
                    string[] row = { sms.Id.ToString(), sms.Category, sms.Destination, sms.Message, ((sms.DeliveryCode==1701)?"Sent":"") + " " + sms.DeliveryMessage, sms.SentOn.ToShortDateString() };
                    var listViewItem = new ListViewItem(row);
                    lstSMS.Items.Add(listViewItem);
                }
            }
        }
    }
}