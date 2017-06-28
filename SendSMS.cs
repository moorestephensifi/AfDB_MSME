using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SEnPA
{
    public partial class SendSMS : Form
    {
        senpa.SEnPAClient senpaSys = new senpa.SEnPAClient();

        public SendSMS()
        {
            InitializeComponent();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.accessToken;

            var context = new OperationContext(senpaSys.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                bool response = senpaSys.SendSMS(txtMobile.Text, txtMessage.Text);
                if (response)
                {
                    MessageBox.Show("Sent SMS!!");
                }
                else
                {
                    MessageBox.Show("Error sending SMS");
                }
            }
        }
    }
}
