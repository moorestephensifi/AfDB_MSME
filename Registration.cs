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
using System.ServiceModel.Channels;
using System.Net;
using System.ServiceModel;

namespace SEnPA
{
    public partial class Registration : DevExpress.XtraEditors.XtraForm
    {
        senpa.SEnPAClient senpaSys = new senpa.SEnPAClient();
        public Registration()
        {
            InitializeComponent();
        }

        //set pick list
        void SetPickList(System.Windows.Forms.ComboBox lst, string type)
        {
            //get document types
            if (type == "BusinessDevelopmentOfficer")
            {
                senpa.PickList[] lstItems = senpaSys.GetUserPickList(type);
                foreach (senpa.PickList typ in lstItems)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = typ.Text;
                    item.Value = typ.Id;
                    lst.Items.Add(item);
                }
            }
            else
            {
                senpa.PickList[] lstItems = senpaSys.GetPickList(type);
                foreach (senpa.PickList typ in lstItems)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = typ.Text;
                    item.Value = typ.Id;
                    lst.Items.Add(item);
                }
            }
        }

        //set pick list value
        void SetPickListValue(System.Windows.Forms.ComboBox lst, long value)
        {
            lst.SelectedValue = value;
        }

        int GetComboBoxValue(System.Windows.Forms.ComboBox lst)
        {
            return int.Parse((lst.SelectedItem as ComboboxItem).Value.ToString());
        }

        private void Registration_Load(object sender, EventArgs e)
        {
            /*
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.accessToken;

            var context = new OperationContext(senpaSys.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                SetPickList(cmbBusDistrict, "district");
                SetPickList(cmbBusIsland, "island");
                SetPickList(cmbBusRegType, "busregtype");
                SetPickList(cmbBusType, "bustype");
                SetPickList(cmbEdu, "edu");
                SetPickList(cmbResDistrict, "district");
                SetPickList(cmbResIsland, "island");
                SetPickList(cmbBDO, "BusinessDevelopmentOfficer");

                if (RegistrationView.currentId != 0)
                {
                    //get registration
                    senpa.RegistrationRequest registration = senpaSys.GetRegistrationRequest(RegistrationView.currentId);
                    lblId.Text = registration.Id.ToString();
                    lblReference.Text = registration.ReferenceNumber;
                    txtBusinessRegNumber.Text = registration.BusinessRegistrationNumber;
                    txtBusinessName.Text = registration.BusinessName;

                    SetPickListValue(cmbBusType, registration.FK_BusinessTypeId);
                    SetPickListValue(cmbBusRegType, registration.FK_BusinessRegistrationTypeId);
                    SetPickListValue(cmbBusIsland, registration.FK_BusinessIslandLocationId);
                    SetPickListValue(cmbBusDistrict, registration.FK_BusinessIslandDistrictId);

                    txtNIN.Text = registration.NIN;
                    txtFirstName.Text = registration.FirstNames;
                    txtLastName.Text = registration.LastName;
                    cmbSalutation.Text = registration.Salutation;
                    txtCitizenship.Text = registration.Citizenship;
                    cmbGender.Text = registration.Gender;
                    dtpDOB.Value = registration.DOB;

                    SetPickListValue(cmbResIsland, registration.FK_ResidenceIslandLocationId);
                    SetPickListValue(cmbResDistrict, registration.FK_ResidenceDistrictLocationId);

                    txtMobile.Text = registration.Mobile;
                    txtHomeTel.Text = registration.HomeTelephone;
                    txtWorkTel.Text = registration.WorkTelephone;
                    txtEmail.Text = registration.Email;

                    SetPickListValue(cmbEdu, registration.FK_EducationLevelId);

                    chkTerms.Checked = registration.TermsAndConditionsAccepted;

                    SetPickListValue(cmbBDO, registration.FK_BusinessDevelopmentOfficerId);
                }
            }
            */
        }

        private void tileItemDocumentation_ItemClick(object sender, TileItemEventArgs e)
        {

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.accessToken;

            var context = new OperationContext(senpaSys.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                senpa.RegistrationRequest newRegistration = new senpa.RegistrationRequest();
                newRegistration.Id = ((long.Parse(lblId.Text) > 0) ? 0 : long.Parse(lblId.Text));
                newRegistration.ReferenceNumber = Utilities.GenerateReferenceNumber();
                newRegistration.BusinessRegistrationNumber = txtBusinessRegNumber.Text;
                newRegistration.BusinessName = txtBusinessName.Text;
                newRegistration.FK_BusinessTypeId = GetComboBoxValue(cmbBusType);
                newRegistration.FK_BusinessRegistrationTypeId = GetComboBoxValue(cmbBusRegType);
                newRegistration.FK_BusinessIslandLocationId = GetComboBoxValue(cmbBusIsland);
                newRegistration.FK_BusinessIslandDistrictId = GetComboBoxValue(cmbBusDistrict);
                newRegistration.NIN = txtNIN.Text;
                newRegistration.FirstNames = txtFirstName.Text;
                newRegistration.LastName = txtLastName.Text;
                newRegistration.Salutation = cmbSalutation.Text;
                newRegistration.Citizenship = txtCitizenship.Text;
                newRegistration.Gender = cmbGender.Text;
                newRegistration.DOB = dtpDOB.Value;
                newRegistration.FK_ResidenceIslandLocationId = GetComboBoxValue(cmbResIsland);
                newRegistration.FK_ResidenceDistrictLocationId = GetComboBoxValue(cmbResDistrict);
                newRegistration.Mobile = txtMobile.Text;
                newRegistration.HomeTelephone = txtHomeTel.Text;
                newRegistration.WorkTelephone = txtWorkTel.Text;
                newRegistration.Email = txtEmail.Text;
                newRegistration.FK_EducationLevelId = GetComboBoxValue(cmbEdu);
                newRegistration.TermsAndConditionsAccepted = chkTerms.Checked;
                newRegistration.FK_BusinessDevelopmentOfficerId = GetComboBoxValue(cmbBDO);
                newRegistration.SubmissionType = "";
                newRegistration.Status = "";
                newRegistration.StatusReason = "";
                newRegistration.Stage = "";
                newRegistration.SubmissionDate = DateTime.Now;
                newRegistration.ReviewCompletionDate = DateTime.Now;
                newRegistration.FeePaymentDate = DateTime.Now;
                newRegistration.BusinessAssessmentStartDate = DateTime.Now;
                newRegistration.SiteVisitDate = DateTime.Now;
                newRegistration.SiteVisitReportCompletionDate = DateTime.Now;
                newRegistration.FollowUpVisitDate = DateTime.Now;
                newRegistration.BusinessAssessmentCompletionDate = DateTime.Now;
                newRegistration.CertificateIssueDate = DateTime.Now;
                newRegistration.DocumentType = "registration";
                newRegistration.RequireWorkFlow = true;
                newRegistration.WorkFlowId = 0;
                newRegistration.WorkFlowStatus = "New";
                newRegistration.Created = DateTime.Now;
                newRegistration.CreatedBy = Globals.userLogged;
                newRegistration.LastModified = DateTime.Now;
                newRegistration.LastModifiedBy = Globals.userLogged;

                long response = senpaSys.SaveRegistrationRequest(newRegistration);
                lblId.Text = response.ToString();

                btnWorkFlow.Text = senpaSys.GetCurrentWorkFlowStage(long.Parse(lblId.Text), "registration");
                //set assign list
                senpa.ApplicationUserSummary[] userList = senpaSys.GetAssigningUserList(long.Parse(lblId.Text), "registration");
                cmbAssign.Items.Clear();
                foreach (senpa.ApplicationUserSummary typ in userList)
                {
                    ComboboxItem item = new ComboboxItem();
                    item.Text = typ.Username;
                    item.Value = typ.Id;
                    cmbAssign.Items.Add(item);
                }
            }
        }

        private void btnWorkFlow_Click(object sender, EventArgs e)
        {
            var httpRequestProperty = new HttpRequestMessageProperty();
            httpRequestProperty.Headers[HttpRequestHeader.Authorization] = Globals.accessToken;

            var context = new OperationContext(senpaSys.InnerChannel);
            using (new OperationContextScope(context))
            {
                context.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = httpRequestProperty;
                string response = senpaSys.CheckCurrentStageDocumentRequirements(long.Parse(lblId.Text), "registration");
                if(response.ToLower()=="none")
                {
                    senpa.DocumentWorkflow wrkFlow = senpaSys.UpdateWorkFlowStage(long.Parse(lblId.Text), "registration");
                    btnWorkFlow.Text = senpaSys.GetCurrentWorkFlowStage(long.Parse(lblId.Text), "registration");
                    //set assign list
                    senpa.ApplicationUserSummary[] userList = senpaSys.GetAssigningUserList(long.Parse(lblId.Text), "registration");
                    cmbAssign.Items.Clear();
                    foreach (senpa.ApplicationUserSummary typ in userList)
                    {
                        ComboboxItem item = new ComboboxItem();
                        item.Text = typ.Username;
                        item.Value = typ.Id;
                        cmbAssign.Items.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show(response);
                }              

            }
        }

        private void tileControlRegistration_Click(object sender, EventArgs e)
        {

        }
    }
}