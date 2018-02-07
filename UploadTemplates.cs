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
using System.IO;
using System.ServiceModel;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;

namespace SBFA
{
    public partial class UploadTemplates : DevExpress.XtraEditors.XtraForm
    {
        public UploadTemplates()
        {
            InitializeComponent();
        }

        private void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                Control control = (Control)sender;
                uploadDocuments.ShowDialog();
                string fileName = uploadDocuments.SafeFileName;
                //MessageBox.Show(fileName);
                byte[] buffer = File.ReadAllBytes(uploadDocuments.FileName);
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    bool done = agent.operation.UploadTemplateDocument(txtName.Text, fileName, buffer);
                    if (done)
                    {
                        sbfa.DocumentTemplateLibrary[] response = agent.operation.GetDocumentTemplates();

                        gridTemplates.DataSource = response;
                        gridTemplates.RefreshDataSource();
                    }
                    else
                    {
                        ShowErrorMessage("Not done !!!");
                    }

                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage("Failed to upload document");
            }
        }

        private void UploadTemplates_Load(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.DocumentTemplateLibrary[] response = agent.operation.GetDocumentTemplates();

                    gridTemplates.DataSource = response;
                    gridTemplates.RefreshDataSource();
                }
            }
            catch
            {
                ShowErrorMessage("Initializing form error");
            }
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
            FlyoutAction action = new FlyoutAction() { Caption = "Templates", Description = message };

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