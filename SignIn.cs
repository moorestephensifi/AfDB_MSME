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
    public partial class SignIn : Form
    {
        senpaSecurity.SEnPASecurityClient security = new senpaSecurity.SEnPASecurityClient();

        public SignIn()
        {
            InitializeComponent();
        }
        
        private void SignIn_Load(object sender, EventArgs e)
        {
            byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes("senpa:senpa");
            string base64 = Convert.ToBase64String(bytes, Base64FormattingOptions.None);
            Globals.authorizationKey = "Basic " + base64;
        }

        private void btnSignIn_Click_1(object sender, EventArgs e)
        {
            
          
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmdSignIn_Click(object sender, EventArgs e)
        {
            SEnPAMain openMain = new SEnPAMain();
            openMain.Show();
            this.Hide();

        }

        private void picClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
