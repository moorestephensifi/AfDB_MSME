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
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.IO;

namespace SEnPA
{
    public partial class TestReg : DevExpress.XtraEditors.XtraForm
    {
        senpa.SEnPAClient senpaSys = new senpa.SEnPAClient();
        public TestReg()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.accessToken;

            var context = new OperationContext(senpaSys.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
               // senpa.Register response = senpaSys.AddRegister(txtName.Text, txtSurname.Text);
               // MessageBox.Show(response.WorkFlowStatus);
               // txtId.Text = response.Id.ToString();

                btnAct.Text = senpaSys.GetCurrentWorkFlowStage(long.Parse(txtId.Text), "Reg");
            }
        }

        private void btnAct_Click(object sender, EventArgs e)
        {
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.accessToken;

            var context = new OperationContext(senpaSys.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                senpa.DocumentWorkflow response = senpaSys.UpdateWorkFlowStage(long.Parse(txtId.Text),"Reg");
                MessageBox.Show(response.WorkFlowStatus);

                btnAct.Text = senpaSys.GetCurrentWorkFlowStage(long.Parse(txtId.Text), "Reg");
            }
        }

        private void TestReg_Load(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.accessToken;

            var context = new OperationContext(senpaSys.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                senpa.ApplicationUserSummary[] response = senpaSys.GetAssigningUserList(long.Parse(txtId.Text), "Reg");
                cmbUsers.Items.Clear();
                for(int x=0;x<response.Length;x++)
                {
                    cmbUsers.Items.Add(response[x].Username);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.accessToken;

            var context = new OperationContext(senpaSys.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                string response = senpaSys.CheckCurrentStageDocumentRequirements(long.Parse(txtId.Text), "Reg");
                MessageBox.Show(response);

                senpa.WorkFlowStageDocuments[] rez = senpaSys.GetDocumentsRequiredList(long.Parse(txtId.Text), "Reg");
                cmbdocs.Items.Clear();
                for (int x = 0; x < rez.Length; x++)
                {
                    cmbdocs.Items.Add(rez[x].DocumentTypeId);
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            op.ShowDialog();
            string fileName = op.SafeFileName;
            MessageBox.Show(fileName);
            byte[] buffer = File.ReadAllBytes(op.FileName);
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.accessToken;

            var context = new OperationContext(senpaSys.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                bool done = senpaSys.UploadWorkFlowDocument(long.Parse(txtId.Text), "Reg", fileName, buffer, int.Parse(cmbdocs.Text),1);
                if(done)
                {
                    MessageBox.Show("Zvaita");
                }
                else
                {
                    MessageBox.Show("Not done Hahahaha!!!");
                }

            }
        }
    }
}