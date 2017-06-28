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
    public partial class ManageEmailDesign : DevExpress.XtraEditors.XtraForm
    {
        senpa.SEnPAClient senpaSys = new senpa.SEnPAClient();
        public ManageEmailDesign()
        {
            InitializeComponent();
        }

        private TreeNode FindRootNode(TreeNode treeNode)
        {
            while (treeNode.Parent != null)
            {
                treeNode = treeNode.Parent;
            }
            return treeNode;
        }

        private void ManageEmailDesign_Load(object sender, EventArgs e)
        {
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.accessToken;

            var context = new OperationContext(senpaSys.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                senpa.EmailMessageDesign[]  response = senpaSys.GetEmailMessageDesigns();
                foreach(senpa.EmailMessageDesign design in response)
                {
                    string currentRole = "_"+design.Id.ToString();
                    treeDesigns.Nodes["emailDesigns"].Nodes.Add(currentRole, design.MessageName);
                    treeDesigns.Nodes["emailDesigns"].Nodes[currentRole].Nodes.Add(design.Subject);
                }
            }
        }

        private void treeDesigns_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.accessToken;

            var context = new OperationContext(senpaSys.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                if (treeDesigns.SelectedNode.Text.ToLower()!="designs")
                {
                    senpa.EmailMessageDesign response = senpaSys.GetEmailMessageDesign(treeDesigns.SelectedNode.Text);
                    System.IO.File.WriteAllText(Application.StartupPath + "\\html\\emaildesign.html", response.Message.Replace("<subject>", response.Subject).Replace("<message>", "This is just a preview of message design"));
                    webView.Url = new Uri(Application.StartupPath + "\\html\\emaildesign.html");
                    webView.Refresh();
                }
            }
        }
    }
}