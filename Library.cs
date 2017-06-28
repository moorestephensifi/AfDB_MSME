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
using System.ServiceModel.Channels;
using System.ServiceModel;
using System.IO;
using DevExpress.XtraEditors;

namespace SEnPA
{
    public partial class Library : DevExpress.XtraEditors.XtraForm
    {
        senpa.SEnPAClient senpaSys = new senpa.SEnPAClient();
        public static long currentFolderId = 1;
        public Library()
        {
            InitializeComponent(); 
        }

        UserControl folderControl(string name, long Id)
        {
            Folder newFolder = new Folder();
            newFolder.Controls["lblText"].Text = name;
            newFolder.Controls["lblId"].Text = Id.ToString();
            newFolder.Controls["lblText"].DoubleClick += new System.EventHandler(this.folderPic_DoubleClick);
            newFolder.Controls["folderPic"].DoubleClick += new System.EventHandler(this.folderPic_DoubleClick);
            return newFolder;
        }

        UserControl fileControl(string name, string fileType, long Id)
        {
            Folder newFile = new Folder();
            newFile.Controls["lblText"].Text = name;
            newFile.Controls["lblId"].Text = Id.ToString();
            newFile.Controls["lblText"].DoubleClick += new System.EventHandler(this.filePic_DoubleClick);
            newFile.Controls["folderPic"].DoubleClick += new System.EventHandler(this.filePic_DoubleClick);
            //set filetype image
            if (fileType.IndexOf("pdf") >= 0)
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SEnPA.Properties.Resources.PDF;
            else if (fileType.IndexOf("word") >= 0)
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SEnPA.Properties.Resources.word;
            else if (fileType.IndexOf("excel") >= 0)
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SEnPA.Properties.Resources.excel;
            else if (fileType.IndexOf("jpg") >= 0)
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SEnPA.Properties.Resources.JPG;
            else if (fileType.IndexOf("png") >= 0)
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SEnPA.Properties.Resources.PNG;
            else if (fileType.IndexOf("gif") >= 0)
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SEnPA.Properties.Resources.GIF;
            else 
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SEnPA.Properties.Resources.uknwn;

            return newFile;
        }

        private void folderPic_DoubleClick(object sender, EventArgs e)
        {
            pnlExplorer.Controls.Clear();
            Control control = (Control)sender;
            Library.currentFolderId = long.Parse(control.Parent.Controls["lblId"].Text);    
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.accessToken;

            var context = new OperationContext(senpaSys.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                lblFolderMap.Text = senpaSys.GetFolderPath(currentFolderId).Replace(","," / ");

                senpa.DocumentFolders[] response = senpaSys.GetFolders(currentFolderId);
                for (int x = 0; x < response.Length; x++)
                {
                    pnlExplorer.Controls.Add(folderControl(response[x].FolderName, response[x].Id));
                }
                //get files
                senpa.DocumentLibrary[] documents = senpaSys.GetFolderDocuments(currentFolderId);
                foreach (senpa.DocumentLibrary doc in documents)
                {
                    pnlExplorer.Controls.Add(fileControl(doc.DocumentName, doc.DocumentContentType, doc.Id));
                }
            }
        }

        private void filePic_DoubleClick(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            long docId = long.Parse(control.Parent.Controls["lblId"].Text);
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.accessToken;

            var context = new OperationContext(senpaSys.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                senpa.DocumentLibrary doc = senpaSys.GetDocument(docId);
                string filePath = Application.StartupPath + "\\filer\\" + control.Parent.Controls["lblText"].Text;
                FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                fs.Write(doc.DocumentData, 0, doc.DocumentData.Length);
                fs.Flush();
                fs.Close();
                System.Diagnostics.Process newProcess = new System.Diagnostics.Process();
                newProcess.StartInfo = new System.Diagnostics.ProcessStartInfo(filePath);
                newProcess.Start();
                newProcess.WaitForExit();
                try
                {
                    System.IO.File.Delete(filePath);
                }
                catch
                {

                }
            }
        }

        private void Library_Load(object sender, EventArgs e)
        {
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.accessToken;

            var context = new OperationContext(senpaSys.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                senpa.DocumentFolders[] response = senpaSys.GetFolders(currentFolderId);
                for (int x = 0; x < response.Length; x++)
                {
                    treeFolders.Nodes["documentLibrary_1"].Nodes.Add("folder_" + response[x].Id.ToString(), response[x].FolderName);
                    pnlExplorer.Controls.Add(folderControl(response[x].FolderName, response[x].Id));
                }

            }
        }

        private void barButtonItemAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            barEditItemAdd.Visibility = BarItemVisibility.Always;
            barButtonItemAddOk.Visibility= BarItemVisibility.Always;
        }

        private void barButtonItemAddOk_ItemClick(object sender, ItemClickEventArgs e)
        {

            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.accessToken;

            var context = new OperationContext(senpaSys.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                long response = senpaSys.CreateFolder(barEditItemAdd.EditValue.ToString(), currentFolderId);
                if (response>0)
                {
                    if (currentFolderId == 1)
                    {
                        treeFolders.Nodes["documentLibrary_1"].Nodes.Add("folder_" + response, barEditItemAdd.EditValue.ToString());
                    }
                    pnlExplorer.Controls.Add(folderControl(barEditItemAdd.EditValue.ToString(), response));
                }
                else
                {
                    MessageBox.Show("Error creating folder");
                }
                barEditItemAdd.Visibility = BarItemVisibility.Never;
                barButtonItemAddOk.Visibility = BarItemVisibility.Never;
            }
        }

        private void treeFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            pnlExplorer.Controls.Clear();
            currentFolderId = long.Parse(treeFolders.SelectedNode.Name.Split('_')[1]);
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.accessToken;

            var context = new OperationContext(senpaSys.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                lblFolderMap.Text = senpaSys.GetFolderPath(currentFolderId).Replace(",", " / ");
                //get folders
                senpa.DocumentFolders[] response = senpaSys.GetFolders(currentFolderId);
                for (int x = 0; x < response.Length; x++)
                {
                    pnlExplorer.Controls.Add(folderControl(response[x].FolderName, response[x].Id));
                }
                //get files
                senpa.DocumentLibrary[] documents = senpaSys.GetFolderDocuments(currentFolderId);
                foreach(senpa.DocumentLibrary doc in documents)
                {
                    pnlExplorer.Controls.Add(fileControl(doc.DocumentName,doc.DocumentContentType, doc.Id));
                }
            }
        }

        private void barButtonItemUpload_ItemClick(object sender, ItemClickEventArgs e)
        {
            new UploadDocument().ShowDialog();
        }

        private void btnSearchFiles_Click(object sender, EventArgs e)
        {
            pnlExplorer.Controls.Clear();
            Control control = (Control)sender;
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.accessToken;

            var context = new OperationContext(senpaSys.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                                //get files
                senpa.DocumentLibrary[] documents = senpaSys.SearchDocuments(currentFolderId,txtSearchFiles.Text);
                foreach (senpa.DocumentLibrary doc in documents)
                {
                    pnlExplorer.Controls.Add(fileControl(doc.DocumentName, doc.DocumentContentType, doc.Id));
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            pnlExplorer.Controls.Clear();
            Control control = (Control)sender;
            
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.accessToken;

            var context = new OperationContext(senpaSys.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                Library.currentFolderId = senpaSys.GetParentFolderId(currentFolderId);
                lblFolderMap.Text = senpaSys.GetFolderPath(currentFolderId).Replace(",", " / ");

                senpa.DocumentFolders[] response = senpaSys.GetFolders(currentFolderId);
                for (int x = 0; x < response.Length; x++)
                {
                    pnlExplorer.Controls.Add(folderControl(response[x].FolderName, response[x].Id));
                }
                //get files
                senpa.DocumentLibrary[] documents = senpaSys.GetFolderDocuments(currentFolderId);
                foreach (senpa.DocumentLibrary doc in documents)
                {
                    pnlExplorer.Controls.Add(fileControl(doc.DocumentName, doc.DocumentContentType, doc.Id));
                }
            }
        }
    }
}