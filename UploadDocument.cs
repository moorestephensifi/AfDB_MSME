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
using System.IO;

namespace SEnPA
{
    public partial class UploadDocument : DevExpress.XtraEditors.XtraForm
    {
        senpa.SEnPAClient senpaSys = new senpa.SEnPAClient();
        public UploadDocument()
        {
            InitializeComponent();
        }

        private void UploadDocument_Load(object sender, EventArgs e)
        {
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.accessToken;

            var context = new OperationContext(senpaSys.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                //get document types
                senpa.DocumentTypes[] docTypes = senpaSys.GetDocumentTypes();
                foreach(senpa.DocumentTypes typ in docTypes)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = typ.DocumentType;
                    item.Value = typ.Id;
                    cmbDocumentType.Items.Add(item);                    
                }

              
            }
        }

        private void btncancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            openFile.ShowDialog();
            txtFile.Text = openFile.FileName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtFile.Text == "")
                return;
            byte[] buffer = File.ReadAllBytes(txtFile.Text);
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.accessToken;

            var context = new OperationContext(senpaSys.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                bool done = senpaSys.UploadDocument(0,Path.GetFileName(txtFile.Text), buffer,int.Parse((cmbDocumentType.SelectedItem as ComboboxItem).Value.ToString()), Library.currentFolderId);
                if (done)
                {
                    txtFile.Text = "";
                    MessageBox.Show("Document saved");
                }
                else
                {
                    MessageBox.Show("Error uploading your document");
                }

            }
        }
    }
}