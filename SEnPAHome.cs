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
using DevExpress.XtraBars.Docking2010.Views;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;

namespace SEnPA
{
    public partial class SEnPAHome : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Form sendNewEmail;
        Form sendNewSMS;
        public SEnPAHome()
        {
            InitializeComponent();
        }

        void OpenForm(XtraForm frm)
        {
            tabbedView.AddDocument(frm);
            tabbedView.ActivateDocument(frm);
        }

        void OpenForm(XtraUserControl frm)
        {
            tabbedView.AddDocument(frm);
            tabbedView.ActivateDocument(frm);
        }

        void OpenNewForm(XtraForm frm)
        {
            frm.Show();
        }


        void barButtonNavigation_ItemClick(object sender, ItemClickEventArgs e)
        {
            switch(e.Item.Caption.ToLower())
            {
                case "send email":
                    sendNewEmail = new SendEmail();
                    tabbedView.AddDocument(sendNewEmail);
                    tabbedView.ActivateDocument(sendNewEmail);
                    break;
                case "send sms":
                    sendNewSMS = new SendSMS();
                    tabbedView.AddDocument(sendNewSMS);
                    tabbedView.ActivateDocument(sendNewSMS);
                    break;
                case "manage users":
                    OpenForm(new ManageUsers());
                    break;
                default:
                    break;
            }
        }
        void tabbedView_DocumentClosed(object sender, DocumentEventArgs e)
        {
            ;
        }
        
        private void SEnPAHome_Load(object sender, EventArgs e)
        {
            
        }

        private void employeesAccordionControlElement_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private void barManageUsers_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(new ManageUsers());
        }

        private void barManageUserGroups_ItemClick(object sender, ItemClickEventArgs e)
        {
            new ManageUserGroupProperties().ShowDialog();
        }
        
        private void barButtonItemMangeSMS_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            OpenForm(new ManageSMS());
        }

        private void barButtonItemManageEmail_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            OpenForm(new ManageEmail());
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            OpenForm(new ManageEmailDesign());
        }

        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            OpenForm(new TestReg());
        }

        private void navBarItemLibrary_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            OpenForm(new Library());
        }

        private void barButtonItemNewRegistration_ItemClick(object sender, ItemClickEventArgs e)
        {
            RegistrationView.currentId = 0;
            OpenNewForm(new Registration());
        }

        private void navBarItemNewRegistration_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            RegistrationView.currentId = 0;
            OpenNewForm(new Registration());
        }

        private void navBarItemViewRegistrations_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            OpenForm(new RegistrationView());
        }
    }
}