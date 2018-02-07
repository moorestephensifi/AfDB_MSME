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
using System.ServiceModel;
using System.IO;
using DevExpress.XtraBars.Docking2010.Views.WindowsUI;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraGrid.Views.Grid;
using System.ComponentModel.DataAnnotations;

namespace SBFA
{
    public partial class SBFAMain : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public int uploadButtonsTopPosition = 82, uploadSiteButtonsTopPosition = 82, existingDocumentsPosition = 82;
        public static long currentId = 0, currentBusinessType = 0, currentInvoiceId = 0, currentBusinessId = 0, currentRenewalId = 0, currentBusinessRegistrationId = 0;
        public static long currentFolderId = 1;
        public static long currentWorkFlow = 0;
        public static string currentUsername = "";
        int currentBusiness = 0;
        long currentLoanId = 0, currentLoanDisId = 0, currentSiteId = 0;
        string currentAccount = "", currentRecoveryAccount = "";
        string currentDocumentDesign = "";
        string category = "";
        bool preQualNINValid = false;
        bool svNINValid = false, svBINValid = false;
        bool svApproved = false, svOfficerApprove = false;
        string currentApprovalStage = "";
        public static long currentRepaymentId = 0;
        public static string oldAccount = "", oldLoan = "", currentOldLoanNumber = "";

        //for messaging
        public static bool forAccount = false;
        public static string name = "";
        public static string mobile = "";
        public static string email = "";

        #region Navigation

        NavigationPage activePage, previousPage;
        List<NavigationPage> navStack;
        bool backPressed;

        private void DoBackNavigation()
        {
            try
            {
                NavigationPage chosenBackToPage = navStack[navStack.Count - 1];
                navStack.Remove(chosenBackToPage);

                backPressed = true;
                navigationFrame.SelectedPage = chosenBackToPage;

                if (navStack.Count == 0)
                {
                    GoHome();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                GoHome();
            }
        }

        private void GoHome()
        {
            navigationFrame.SelectedPage = navPageHome;
        }

        private void navigationFrame_SelectedPageChanging(object sender, SelectedPageChangingEventArgs e)
        {
            try
            {
                previousPage = (NavigationPage)navigationFrame.SelectedPage;
                if (backPressed == false)
                {
                    navStack.Add(previousPage);
                }
                else
                {
                    backPressed = false;
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {

            }

        }

        private void navigationFrame_SelectedPageChanged(object sender, SelectedPageChangedEventArgs e)
        {
            activePage = (NavigationPage)navigationFrame.SelectedPage;
            //hide menus
            loanRibbon.Visible = false;
            accountsRibbon.Visible = false;
            disburseRibbon.Visible = false;
            siteRibbon.Visible = false;
            ribbonBtnsUsers.Visible = false;
            ribbonBtnsReferences.Visible = false;
            ribbonBtnsWorkflow.Visible = false;
            ribbonBtnsTemplates.Visible = false;
            recRibbonPage.Visible = false;
            siteRibbonPage.Visible = false;
            payQuickActions.Visible = false;
            docQuickActions.Visible = false;
            lonQuickActions.Visible = false;
            ribPrequalification.Visible = false;
            ribLoanProcessing.Visible = false;
            disQuickOptions.Visible = false;
            sigQuickActions.Visible = false;
            ribbonPageManageRepayment.Visible = false;
            ribbonPagePendingPayments.Visible = false;
            ribbonRecoverySiteVisit.Visible = true;

            //disbursement
            ribbonPageManageDisburse.Visible = false;
            ribbonPageApproveDisburse.Visible = false;
            ribbonDisReqActions.Visible = false;
            ribbonPageRepayment.Visible = false;
            vouNotifyRibbonPage.Visible = false;
            vouDisRibbonPage.Visible = false;
            //enable loan wrkflow tools
            wrkFlowToolsRibbon.Visible = true;
            loanAssRibbon.Visible = true;
            loanSiteRibbon.Visible = true;

            oldRibbon.Visible = false;
            cmdRecSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            cmdRecSiteReport.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            cmdNewSite.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            cmdOpenVisits.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

            if (navigationFrame.SelectedPage == navPagePrequalification)
            {
                loanRibbon.Visible = true;
                ribPrequalification.Visible = true;
                ribLoanProcessing.Visible = false;
                mainRibbon.SelectedPage = ribPrequalification;

                try
                {
                    SBFAApi agent = new SBFAApi();
                    using (new OperationContextScope(agent.context))
                    {
                        sbfa.ReferenceTable[] busType = agent.operation.GetReferenceTableItems("bustyp");
                        foreach (sbfa.ReferenceTable bus in busType)
                        {
                            string currentBus = "_" + bus.Id.ToString();
                            cmbPreBusinessType.Items.Add(bus.Name);

                        }

                        sbfa.ReferenceTable[] secType = agent.operation.GetReferenceTableItems("sec");
                        foreach (sbfa.ReferenceTable sec in secType)
                        {
                            string currentSec = "_" + sec.Id.ToString();
                            chkSecurity.Items.Add(sec.Name);

                        }
                    }
                }
                catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
                catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
                catch (Exception ex)
                {
                    ShowErrorMessage(ex.Message);
                }

            }
            else if (navigationFrame.SelectedPage == navPageViewLoans)
            {
                lonQuickActions.Visible = true;
                mainRibbon.SelectedPage = homeRibbonPage;
            }
            else if (navigationFrame.SelectedPage == navPageLoans)
            {
                loanRibbon.Visible = true;
                ribLoanProcessing.Visible = true;
                ribPrequalification.Visible = false;
                cmdApproveLoan.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                cmdDeclineLoan.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                mainRibbon.SelectedPage = ribLoanProcessing;
            }
            else if (navigationFrame.SelectedPage == navPageApproveLoan)
            {
                loanRibbon.Visible = true;
                ribLoanProcessing.Visible = true;
                ribPrequalification.Visible = false;
                cmdApproveLoan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                cmdDeclineLoan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                mainRibbon.SelectedPage = ribLoanProcessing;
            }
            else if (navigationFrame.SelectedPage == navPageDisbursements)
            {
                disQuickOptions.Visible = true;
                mainRibbon.SelectedPage = homeRibbonPage;
            }
            else if (navigationFrame.SelectedPage == navPageLoanDisbursement)
            {
                disburseRibbon.Visible = true;
                ribbonPageManageDisburse.Visible = true;
                mainRibbon.SelectedPage = disRibbonPage;
            }
            else if (navigationFrame.SelectedPage == navPageDisbursementRequest)
            {
                //refer to initialize method               
            }
            else if (navigationFrame.SelectedPage == navPageSignedDisbursements)
            {
                sigQuickActions.Visible = true;
                mainRibbon.SelectedPage = homeRibbonPage;
            }
            else if (navigationFrame.SelectedPage == navPageLoanDisbursed)
            {
                accountsRibbon.Visible = true;
                mainRibbon.SelectedPage = accDisRibbonPage;
            }
            else if (navigationFrame.SelectedPage == navPageRecoveryVisit)
            {
                accountsRibbon.Visible = true;
                accPayRibbonPage.Visible = true;
                accDisRibbonPage.Visible = false;
                ribbonPageRepayment.Visible = true;
                ribbonPagePayments.Visible = true;
                mainRibbon.SelectedPage = accPayRibbonPage;

                oldRibbon.Visible = true;
                ribbonOldRec.Visible = false;
                ribbonPageRepayment.Visible = false;
                if (Globals.hasAccess("viewPayments"))
                {
                    cmdOldPayments.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                else
                {
                    cmdOldPayments.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }

                if (Globals.hasAccess("viewLoans"))
                    cmdOldRequests.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    cmdOldRequests.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                if (Globals.hasAccess("viewAccounts"))
                    cmdOldAccounts.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    cmdOldAccounts.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;


                if (Globals.hasAccess("processPayment"))
                    barButtonItem20.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    barButtonItem20.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                ribbonPageRepayment.Visible = ((Globals.hasAccess("manageRepayments")) ? true : false);


            }
            else if (navigationFrame.SelectedPage == navPageRecovery)
            {
                accountsRibbon.Visible = true;
                accPayRibbonPage.Visible = true;
                accDisRibbonPage.Visible = false;
                ribbonPageRepayment.Visible = true;
                ribbonPagePayments.Visible = true;
                mainRibbon.SelectedPage = accPayRibbonPage;
                cmdOpenVisits.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                ribbonPageRepayment.Visible = true;
                oldRibbon.Visible = true;
                ribbonOldRec.Visible = false;
                if (Globals.hasAccess("viewPayments"))
                {
                    cmdOldPayments.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                }
                else
                {
                    cmdOldPayments.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }

                if (Globals.hasAccess("viewLoans"))
                    cmdOldRequests.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    cmdOldRequests.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                if (Globals.hasAccess("viewAccounts"))
                    cmdOldAccounts.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    cmdOldAccounts.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;


                if (Globals.hasAccess("processPayment"))
                    barButtonItem20.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    barButtonItem20.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                ribbonPageRepayment.Visible = ((Globals.hasAccess("manageRepayments")) ? true : false);


            }
            else if (navigationFrame.SelectedPage == navPageOldRecovery)
            {
                oldRibbon.Visible = true;
                // ribbonOldRec.Visible = true;
                ribbonOldPay.Visible = false;
                mainRibbon.SelectedPage = ribbonOldDb;
                if (Globals.hasAccess("viewLoans"))
                    cmdOldRequests.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    cmdOldRequests.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                if (Globals.hasAccess("viewAccounts"))
                    cmdOldAccounts.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    cmdOldAccounts.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else if (navigationFrame.SelectedPage == navPageOldRepayments)
            {
                oldRibbon.Visible = true;
                ribbonOldRec.Visible = false;
                ribbonOldPay.Visible = false;
                mainRibbon.SelectedPage = ribbonOldDb;
                if (Globals.hasAccess("viewLoans"))
                    cmdOldRequests.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    cmdOldRequests.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                if (Globals.hasAccess("viewAccounts"))
                    cmdOldAccounts.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    cmdOldAccounts.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else if (navigationFrame.SelectedPage == navPageOldRequests)
            {
                oldRibbon.Visible = true;
                ribbonOldRec.Visible = false;
                ribbonOldPay.Visible = false;
                mainRibbon.SelectedPage = ribbonOldDb;
                if (Globals.hasAccess("viewLoans"))
                    cmdOldRequests.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    cmdOldRequests.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                if (Globals.hasAccess("viewAccounts"))
                    cmdOldAccounts.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    cmdOldAccounts.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            }
            else if (navigationFrame.SelectedPage == navPageOldMigrationAccounts)
            {
                oldRibbon.Visible = true;
                ribbonOldRec.Visible = false;
                ribbonOldPay.Visible = true;
                mainRibbon.SelectedPage = ribbonOldDb;
                if (Globals.hasAccess("viewLoans"))
                    cmdOldRequests.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    cmdOldRequests.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                if (Globals.hasAccess("viewAccounts"))
                    cmdOldAccounts.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    cmdOldAccounts.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                if (Globals.hasAccess("processPayment")) ribbonOldPay.Visible = true;
                else ribbonOldPay.Visible = false;
            }
            else if (navigationFrame.SelectedPage == navPageRepaymentSchedule)
            {
                accountsRibbon.Visible = true;
                accPayRibbonPage.Visible = true;
                accDisRibbonPage.Visible = false;
                mainRibbon.SelectedPage = accPayRibbonPage;

                if (Globals.hasAccess("processPayment"))
                    barButtonItem20.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    barButtonItem20.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                ribbonPageRepayment.Visible = ((Globals.hasAccess("manageRepayments")) ? true : false);
                ribbonPageManageRepayment.Visible = ((Globals.hasAccess("manageRepayments")) ? true : false);
            }
            else if (navigationFrame.SelectedPage == navPagePendingRepayments)
            {
                accountsRibbon.Visible = true;
                accPayRibbonPage.Visible = true;
                accDisRibbonPage.Visible = false;
                ribbonPagePendingPayments.Visible = true;
                mainRibbon.SelectedPage = accPayRibbonPage;
                ribbonRecoverySiteVisit.Visible = false;


                if (Globals.hasAccess("processPayment"))
                    barButtonItem20.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                else
                    barButtonItem20.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                //ribbonPageRepayment.Visible = ((Globals.hasAccess("manageRepayments")) ? true : false);
                //ribbonPageManageRepayment.Visible = ((Globals.hasAccess("manageRepayments")) ? true : false);
            }
            else if (navigationFrame.SelectedPage == navPageDocuments)
            {
                docQuickActions.Visible = true;
                mainRibbon.SelectedPage = homeRibbonPage;
            }
            else if (navigationFrame.SelectedPage == navPagePayments)
            {
                payQuickActions.Visible = true;
                mainRibbon.SelectedPage = homeRibbonPage;
            }
            else if (navigationFrame.SelectedPage == navPageRecomendations)
            {
                siteRibbon.Visible = true;
                recRibbonPage.Visible = true;
                siteRibbonPage.Visible = false;
                mainRibbon.SelectedPage = recRibbonPage;
            }
            else if (navigationFrame.SelectedPage == navPageWorkFlows)
            {
                ribbonBtnsWorkflow.Visible = true;
            }
            else if (navigationFrame.SelectedPage == navPageBusinessType) { }
            else if (navigationFrame.SelectedPage == navPageUsers)
            {
                ribbonBtnsUsers.Visible = true;
            }
            else if (navigationFrame.SelectedPage == navPageReferences)
            {
                ribbonBtnsReferences.Visible = true;
            }
            else if (navigationFrame.SelectedPage == navPageEmail) { }
            else if (navigationFrame.SelectedPage == navPageSMS) { }
            else if (navigationFrame.SelectedPage == navPageDesignDocuments)
            {
                ribbonBtnsTemplates.Visible = true;
                cmdSaveDocDesign.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
            }
            else if (navigationFrame.SelectedPage == navPageSiteVisitReport)
            {
                siteRibbon.Visible = true;
                siteRibbonPage.Visible = true;
                recRibbonPage.Visible = false;
                mainRibbon.SelectedPage = siteRibbonPage;
            }
            else if (navigationFrame.SelectedPage == navPageScheduleSiteVisit)
            {
                siteRibbon.Visible = true;
                siteRibbonPage.Visible = true;
                recRibbonPage.Visible = false;
                mainRibbon.SelectedPage = siteRibbonPage;
            }

        }

        List<string> lstSteps; //for registration progress indicator
        int activeIndex; //for registration progress indicator

        #endregion

        #region WorkflowProgressIndicators

        private void RefreshLoanIndicator()
        {
            try
            {
                lstSteps = new List<string>();

                SBFAApi agent = new SBFAApi();
                //get the details of the stages from the server
                using (new OperationContextScope(agent.context))
                {
                    int lastPosition = 0;

                    if (long.Parse(lblId.Text) == 0)
                    {
                        sbfa.WorkFlowStages[] stages = agent.operation.GetWorkFlowStages(1);
                        foreach (sbfa.WorkFlowStages stg in stages)
                        {
                            lstSteps.Add(stg.StagePosition + ";" + stg.StageName + ";" + (stg.StageName.ToString().Equals("Saved") ? "true" : "false"));
                            lastPosition = stg.StagePosition;
                        }
                    }
                    else
                    {
                        sbfa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(long.Parse(lblId.Text), "loan");
                        //lblStage.Text = currentStage.StageName;

                        sbfa.WorkFlowStages[] stages = agent.operation.GetWorkFlowStages(1);
                        foreach (sbfa.WorkFlowStages stg in stages)
                        {
                            lstSteps.Add(stg.StagePosition + ";" + stg.StageName + ";" + (stg.StageName.ToString().Equals(currentStage.StageName) ? "true" : "false"));
                            lastPosition = stg.StagePosition;
                        }
                        lstSteps.Add(lastPosition + 1 + ";Complete;" + ("Complete".ToString().Equals(currentStage.StageName) ? "true" : "false"));
                    }



                }

                int stepNo = lstSteps.Count;
                int controlXValue = 0;

                foreach (string item in lstSteps)
                {
                    string[] itemBundle = item.Split(';');
                    int itemIndex = int.Parse(itemBundle[0]);
                    string itemTitle = itemBundle[1];
                    bool itemActive = Boolean.Parse(itemBundle[2]);

                    if (itemActive == true)
                    {
                        activeIndex = itemIndex;
                    }
                }

                panLoanWorkFlowIndicator.Controls.Clear();

                foreach (string item in lstSteps)
                {
                    string[] itemBundle = item.Split(';');
                    int itemIndex = int.Parse(itemBundle[0]);
                    string itemTitle = itemBundle[1];
                    bool itemActive = Boolean.Parse(itemBundle[2]);


                    Panel panel = new Panel();
                    panel.BackgroundImage = Properties.Resources.current_item_fw;
                    panel.BackgroundImageLayout = ImageLayout.Stretch;
                    panel.Height = 50;
                    panel.Width = panLoanWorkFlowIndicator.Width / stepNo;
                    panel.Location = new Point(controlXValue, 0);
                    panel.AutoSize = true;
                    panel.Tag = itemIndex;

                    //determine the background image based on the status
                    if (itemIndex == 0)
                    {
                        if (itemActive == true)
                        {
                            panel.BackgroundImage = Properties.Resources.current_start_item_fw;
                        }
                        else
                        {
                            panel.BackgroundImage = Properties.Resources.completed_start_item_fw;
                        }
                    }
                    else
                    {
                        if (itemActive == true)
                        {
                            panel.BackgroundImage = Properties.Resources.current_item_fw;
                        }
                        else
                        {
                            if (itemIndex == (activeIndex + 1))
                            {
                                panel.BackgroundImage = Properties.Resources.next_undone_item_fw;
                            }
                            else if (itemIndex < activeIndex)
                            {
                                panel.BackgroundImage = Properties.Resources.completed_start_item_fw;
                            }
                            else
                            {
                                panel.BackgroundImage = Properties.Resources.full_undone_item_fw;
                            }

                        }


                    }


                    Label lbl = new Label();
                    lbl.ForeColor = Color.White;
                    lbl.Text = itemTitle;
                    lbl.Anchor = (AnchorStyles.Right);
                    lbl.TextAlign = ContentAlignment.MiddleRight;
                    lbl.BackColor = Color.Transparent;
                    lbl.Width = panel.Width;
                    lbl.Top = 12;
                    lbl.AutoSize = false;


                    if (itemActive == true)
                    {
                        lbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    }
                    else
                    {
                        lbl.Font = new Font("Segoe UI", 8, FontStyle.Regular);
                    }

                    panel.Controls.Add(lbl);

                    //tbProgress.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                    //tbProgress.SetRowSpan(panel, 1);
                    panLoanWorkFlowIndicator.Controls.Add(panel);
                    controlXValue += (panLoanWorkFlowIndicator.Width / stepNo);


                }

                panLoanWorkFlowIndicator.Refresh();
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void panLoanWorkFlowIndicator_Paint(object sender, PaintEventArgs e)
        {
            //panel2 is the parent panel
            foreach (Panel panel in panLoanWorkFlowIndicator.Controls)
            {
                int stepNo = lstSteps.Count;
                panel.Width = panLoanWorkFlowIndicator.Width / stepNo;
                int controlXValue = 0;

                controlXValue += (panLoanWorkFlowIndicator.Width / stepNo);
                panel.Location = new Point(controlXValue * (int)panel.Tag, 0);
                panel.Update();
            }

        }

        private void RefreshRecommendationIndicator()
        {
            try
            {
                lstSteps = new List<string>();

                SBFAApi agent = new SBFAApi();
                //get the details of the stages from the server
                using (new OperationContextScope(agent.context))
                {
                    int lastPosition = 0;

                    if (currentId == 0)
                    {
                        long wrkId = agent.operation.GetWorkFlowId(long.Parse(lblId.Text), "loaapp");
                        sbfa.WorkFlowStages[] stages = agent.operation.GetWorkFlowStages(wrkId);
                        foreach (sbfa.WorkFlowStages stg in stages)
                        {
                            lstSteps.Add(stg.StagePosition + ";" + stg.StageName + ";" + (stg.StageName.ToString().Equals("Saved") ? "true" : "false"));
                            lastPosition = stg.StagePosition;
                        }
                    }
                    else
                    {
                        sbfa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(currentId, "loaapp");
                        // lblStage.Text = currentStage.StageName;
                        long wrkId = agent.operation.GetWorkFlowId(long.Parse(lblId.Text), "loaapp");
                        sbfa.WorkFlowStages[] stages = agent.operation.GetWorkFlowStages(wrkId);
                        foreach (sbfa.WorkFlowStages stg in stages)
                        {
                            lstSteps.Add(stg.StagePosition + ";" + stg.StageName + ";" + (stg.StageName.ToString().Equals(currentStage.StageName) ? "true" : "false"));
                            lastPosition = stg.StagePosition;
                        }
                        lstSteps.Add(lastPosition + 1 + ";Complete;" + ("Complete".ToString().Equals(currentStage.StageName) ? "true" : "false"));
                    }



                }

                int stepNo = lstSteps.Count;
                int controlXValue = 0;

                foreach (string item in lstSteps)
                {
                    string[] itemBundle = item.Split(';');
                    int itemIndex = int.Parse(itemBundle[0]);
                    string itemTitle = itemBundle[1];
                    bool itemActive = Boolean.Parse(itemBundle[2]);

                    if (itemActive == true)
                    {
                        activeIndex = itemIndex;
                    }
                }

                panRecWorkflowIndicator.Controls.Clear();

                foreach (string item in lstSteps)
                {
                    string[] itemBundle = item.Split(';');
                    int itemIndex = int.Parse(itemBundle[0]);
                    string itemTitle = itemBundle[1];
                    bool itemActive = Boolean.Parse(itemBundle[2]);


                    Panel panel = new Panel();
                    panel.BackgroundImage = Properties.Resources.current_item_fw;
                    panel.BackgroundImageLayout = ImageLayout.Stretch;
                    panel.Height = 50;
                    panel.Width = panRecWorkflowIndicator.Width / stepNo;
                    panel.Location = new Point(controlXValue, 0);
                    panel.AutoSize = true;
                    panel.Tag = itemIndex;

                    //determine the background image based on the status
                    if (itemIndex == 0)
                    {
                        if (itemActive == true)
                        {
                            panel.BackgroundImage = Properties.Resources.current_start_item_fw;
                        }
                        else
                        {
                            panel.BackgroundImage = Properties.Resources.completed_start_item_fw;
                        }
                    }
                    else
                    {
                        if (itemActive == true)
                        {
                            panel.BackgroundImage = Properties.Resources.current_item_fw;
                        }
                        else
                        {
                            if (itemIndex == (activeIndex + 1))
                            {
                                panel.BackgroundImage = Properties.Resources.next_undone_item_fw;
                            }
                            else if (itemIndex < activeIndex)
                            {
                                panel.BackgroundImage = Properties.Resources.completed_start_item_fw;
                            }
                            else
                            {
                                panel.BackgroundImage = Properties.Resources.full_undone_item_fw;
                            }

                        }


                    }


                    Label lbl = new Label();
                    lbl.ForeColor = Color.White;
                    lbl.Text = itemTitle;
                    lbl.Anchor = (AnchorStyles.Right);
                    lbl.TextAlign = ContentAlignment.MiddleRight;
                    lbl.BackColor = Color.Transparent;
                    lbl.Width = panel.Width;
                    lbl.Top = 12;
                    lbl.AutoSize = false;


                    if (itemActive == true)
                    {
                        lbl.Font = new Font("Segoe UI", 9, FontStyle.Bold);
                    }
                    else
                    {
                        lbl.Font = new Font("Segoe UI", 8, FontStyle.Regular);
                    }

                    panel.Controls.Add(lbl);

                    //tbProgress.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                    //tbProgress.SetRowSpan(panel, 1);
                    panRecWorkflowIndicator.Controls.Add(panel);
                    controlXValue += (panRecWorkflowIndicator.Width / stepNo);


                }

                panRecWorkflowIndicator.Refresh();
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void panRecWorkflowIndicator_Paint(object sender, PaintEventArgs e)
        {
            foreach (Panel panel in panRecWorkflowIndicator.Controls)
            {
                int stepNo = lstSteps.Count;
                panel.Width = panRecWorkflowIndicator.Width / stepNo;
                int controlXValue = 0;

                controlXValue += (panRecWorkflowIndicator.Width / stepNo);
                panel.Location = new Point(controlXValue * (int)panel.Tag, 0);
                panel.Update();
            }
        }

        #endregion

        #region custom

        private void SideBarRights()
        {
            preQualification.Visible = ((Globals.hasAccess("prequalification")) ? true : false);
            loanApplication.Visible = ((Globals.hasAccess("captureApplication")) ? true : false);
            navBarFindLoanApplications.Visible = ((Globals.hasAccess("viewLoans")) ? true : false);
            navBarItem1.Visible = ((Globals.hasAccess("viewLoans")) ? true : false);
            navBarFindDisbursements.Visible = ((Globals.hasAccess("viewDisbursements")) ? true : false);
            navBarPaymentVouchers.Visible = ((Globals.hasAccess("viewVouchers")) ? true : false);
            navBarAccounts.Visible = ((Globals.hasAccess("viewAccounts")) ? true : false);
            navBarItem3.Visible = ((Globals.hasAccess("viewAccounts")) ? true : false);
            navBarRepayments.Visible = ((Globals.hasAccess("viewPayments")) ? true : false);
            navBarItem2.Visible = ((Globals.hasAccess("viewPayments")) ? true : false);

            if (Globals.hasAccess("prequalification") || Globals.hasAccess("captureApplication"))
            {
                loanApplicationNavBarGroup.Visible = true;
            }
            else
            {
                loanApplicationNavBarGroup.Visible = false;
            }

            if (Globals.hasAccess("viewLoans") || Globals.hasAccess("processLoan"))
            {
                loansNavBarGroup.Visible = true;
            }
            else
            {
                loansNavBarGroup.Visible = false;
            }

            if (Globals.hasAccess("processPayment") || Globals.hasAccess("manageRepayments") || Globals.hasAccess("viewLoans") || Globals.hasAccess("viewPayments"))
            {
                recoveyNavBarGroup.Visible = true;
            }
            else
            {
                recoveyNavBarGroup.Visible = false;
            }

            if (Globals.hasAccess("processPayment") || Globals.hasAccess("viewPayments"))
            {
                paymentsNavBarGroup.Visible = true;
            }
            else
            {
                paymentsNavBarGroup.Visible = false;
            }

            if (Globals.hasAccess("raiseDisbursement") || Globals.hasAccess("raiseVoucher") || Globals.hasAccess("viewDisbursements") || Globals.hasAccess("viewVouchers"))
            {
                disbursementsNavBarGroup.Visible = true;
            }
            else
            {
                disbursementsNavBarGroup.Visible = false;
            }
        }

        private void TopBarRights()
        {
            ribbonPageCategory1.Visible = ((Globals.hasAccess("administration")) ? true : false);
            ribbonPageCategory3.Visible = ((Globals.hasAccess("messaging")) ? true : false);
        }

        public void DocumentButton(sbfa.WorkFlowStageDocumentStatus doc, string type = "loan")
        {
            SimpleButton addDocument = new SimpleButton();
            addDocument.Appearance.BackColor = System.Drawing.Color.Lavender;
            addDocument.Appearance.Options.UseBackColor = true;
            addDocument.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            addDocument.ImageOptions.Image = global::SBFA.Properties.Resources.sign_add;
            addDocument.Location = new System.Drawing.Point(40, this.uploadButtonsTopPosition);
            addDocument.LookAndFeel.SkinName = "Office 2016 Black";
            addDocument.Name = "_" + doc.Id.ToString() + "_" + doc.FK_DocumentTypeId.ToString() + "_" + doc.FK_StageId.ToString();
            addDocument.Size = new System.Drawing.Size(157, 26);
            addDocument.TabIndex = 11;
            addDocument.Text = doc.DocumentType;
            if (type == "loan")
                addDocument.Click += new System.EventHandler(this.UplaodDocument_Click);

            PictureBox pic = new PictureBox();
            if (doc.Uploaded)
                pic.Image = picDone.Image;
            else
                pic.Image = picBlur.Image;

            // pic.Image = ((System.Drawing.Image)(sbfa.Properties.Resources.sign_tick));
            pic.Location = new System.Drawing.Point(8, this.uploadButtonsTopPosition);
            pic.Name = "pic" + "_" + doc.Id.ToString();
            pic.Size = new System.Drawing.Size(26, 26);
            pic.TabIndex = 12;
            pic.TabStop = false;
            pic.Visible = true;
            if (type == "loan")
            {
                grpDocuments.Controls.Add(addDocument);
                grpDocuments.Controls.Add(pic);
            }
            else
            {

            }
            uploadButtonsTopPosition += 38;
        }

        private void UplaodDocument_Click(object sender, EventArgs e)
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
                    bool done = agent.operation.UploadWorkFlowDocument(long.Parse(lblId.Text), "loan", fileName, buffer, int.Parse(control.Name.Split('_')[2]), 3, false);
                    if (done)
                    {
                        (grpDocuments.Controls["pic" + "_" + control.Name.Split('_')[1]] as PictureBox).Image = picDone.Image;
                    }
                    else
                    {
                        ShowErrorMessage("Please check that you are uploading a valid document format.");

                    }

                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("Please check that you are uploading a valid document format.");
            }
        }

        public void DocumentButton(sbfa.SiteVisitReport doc)
        {
            try
            {
                SimpleButton addDocument = new SimpleButton();
                addDocument.Appearance.BackColor = System.Drawing.Color.Lavender;
                addDocument.Appearance.Options.UseBackColor = true;
                addDocument.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
                addDocument.ImageOptions.Image = global::SBFA.Properties.Resources.sign_add;
                addDocument.Location = new System.Drawing.Point(40, this.uploadSiteButtonsTopPosition);
                addDocument.LookAndFeel.SkinName = "Office 2016 Black";
                addDocument.Name = "_" + doc.Id.ToString() + "_" + doc.FK_SiteVisitId.ToString() + "_" + doc.FK_StakeholderId.ToString();
                addDocument.Size = new System.Drawing.Size(157, 26);
                addDocument.TabIndex = 11;
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    addDocument.Text = agent.operation.GetEntityName(doc.FK_StakeholderId, "stahol");
                }
                addDocument.Click += new System.EventHandler(this.UplaodSiteReport_Click);
                grpSiteDocuments.Controls.Add(addDocument);

                PictureBox pic = new PictureBox();
                if (doc.UploadStatus)
                    pic.Image = picDone.Image;
                else
                    pic.Image = picBlur.Image;

                // pic.Image = ((System.Drawing.Image)(sbfa.Properties.Resources.sign_tick));
                pic.Location = new System.Drawing.Point(8, this.uploadSiteButtonsTopPosition);
                pic.Name = "picSite" + "_" + doc.Id.ToString();
                pic.Size = new System.Drawing.Size(26, 26);
                pic.TabIndex = 12;
                pic.TabStop = false;
                pic.Visible = true;
                grpSiteDocuments.Controls.Add(pic);
                uploadSiteButtonsTopPosition += 38;
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void UplaodSiteReport_Click(object sender, EventArgs e)
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
                    bool done = agent.operation.UploadSiteVisitWorkFlowDocument(long.Parse(control.Name.Split('_')[2]), int.Parse(control.Name.Split('_')[3]), "", currentId, "loan", fileName, buffer, 0, 3);
                    if (done)
                    {
                        (grpSiteDocuments.Controls["picSite" + "_" + control.Name.Split('_')[1]] as PictureBox).Image = picDone.Image;
                    }
                    else
                    {
                        ShowErrorMessage("Please check that you are uploading a valid document format.");
                    }

                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("Please check that you are uploading a valid document format.");
            }
        }

        public void DocumentLink(sbfa.WorkFlowStageDocumentStatus doc, string view, string type = "loan")
        {
            LinkLabel addDocument = new LinkLabel();
            addDocument.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            addDocument.Location = new System.Drawing.Point(43, this.existingDocumentsPosition);
            addDocument.Name = "_" + doc.Id.ToString() + "_" + doc.FK_DocumentTypeId.ToString() + "_" + doc.FK_StageId.ToString() + "_ex";
            addDocument.Size = new System.Drawing.Size(169, 13);
            addDocument.TabIndex = 27;
            addDocument.TabStop = true;
            addDocument.Text = doc.DocumentType;
            addDocument.Click += new System.EventHandler(this.OpenDocument_Click);
            PictureBox pic = new PictureBox();
            if (doc.Uploaded)
                pic.Image = picDoc.Image;
            else
                pic.Image = picBlur.Image;

            // pic.Image = ((System.Drawing.Image)(sbfa.Properties.Resources.sign_tick));
            pic.Location = new System.Drawing.Point(8, this.existingDocumentsPosition);
            pic.Name = "picView" + "_" + doc.Id.ToString();
            pic.Size = new System.Drawing.Size(26, 26);
            pic.TabIndex = 12;
            pic.TabStop = false;
            pic.Visible = true;
            if (view == "business")
            {
                if (type == "loan")
                {
                    grpExistingWorkflow.Controls.Add(addDocument);
                    grpExistingWorkflow.Controls.Add(pic);
                }
                else
                {

                }
            }


            existingDocumentsPosition += 38;
        }

        private void OpenDocument_Click(object sender, EventArgs e)
        {
            try
            {
                Control control = (Control)sender;
                long docId = long.Parse(control.Name.Split('_')[1]);
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.DocumentLibrary doc = agent.operation.GetDocument(docId);
                    string filePath = Application.StartupPath + "\\filer\\" + control.Text;
                    FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                    fs.Write(doc.DocumentData, 0, doc.DocumentData.Length);
                    fs.Flush();
                    fs.Close();
                    try
                    {
                        System.Diagnostics.Process newProcess = new System.Diagnostics.Process();
                        newProcess.StartInfo = new System.Diagnostics.ProcessStartInfo(filePath);
                        newProcess.Start();
                        newProcess.WaitForExit();
                    }
                    catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
                    catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("Please install a PDF Reader on your computer and try again.");
                    }
                    try
                    {
                        System.IO.File.Delete(filePath);
                    }
                    catch
                    {

                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There was an error opening your document");

            }
        }

        public void DocumentLink(sbfa.AutoDocument doc)
        {
            LinkLabel addDocument = new LinkLabel();
            addDocument.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            addDocument.Location = new System.Drawing.Point(43, this.existingDocumentsPosition);
            addDocument.Name = "_" + doc.Id.ToString() + "_" + doc.DocumentType.ToString() + "_ex";
            addDocument.Size = new System.Drawing.Size(169, 13);
            addDocument.TabIndex = 27;
            addDocument.TabStop = true;
            addDocument.Text = doc.DocumentTypeName;
            addDocument.Click += new System.EventHandler(this.OpenAutoDocument_Click);
            PictureBox pic = new PictureBox();
            pic.Image = picDoc.Image;
            // pic.Image = ((System.Drawing.Image)(sbfa.Properties.Resources.sign_tick));
            pic.Location = new System.Drawing.Point(8, this.existingDocumentsPosition);
            pic.Name = "picExView" + "_" + doc.Id.ToString();
            pic.Size = new System.Drawing.Size(26, 26);
            pic.TabIndex = 12;
            pic.TabStop = false;
            pic.Visible = true;
            grpExistingWorkflow.Controls.Add(addDocument);
            grpExistingWorkflow.Controls.Add(pic);

            existingDocumentsPosition += 38;
        }

        private void OpenAutoDocument_Click(object sender, EventArgs e)
        {
            try
            {
                Control control = (Control)sender;
                long docId = long.Parse(control.Name.Split('_')[1]);
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {

                    Byte[] doc = agent.operation.GetAutoDocument(control.Name.Split('_')[2], docId);
                    string filePath = Application.StartupPath + "\\filer\\" + control.Text + ".pdf";
                    FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                    fs.Write(doc, 0, doc.Length);
                    fs.Flush();
                    fs.Close();
                    try
                    {
                        System.Diagnostics.Process newProcess = new System.Diagnostics.Process();
                        newProcess.StartInfo = new System.Diagnostics.ProcessStartInfo(filePath);
                        newProcess.Start();
                        newProcess.WaitForExit();
                    }
                    catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
                    catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("Please install a PDF Reader on your computer and try again.");
                    }
                    try
                    {
                        System.IO.File.Delete(filePath);
                    }
                    catch
                    {

                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There has been an error openning your document");
            }
        }

        public void StakeholderSelect(sbfa.Stakeholder stake)
        {
            CheckBox addDocument = new CheckBox();
            addDocument.AutoSize = true;
            addDocument.Location = new System.Drawing.Point(15, this.uploadSiteButtonsTopPosition);
            addDocument.Name = "stake_" + stake.Id.ToString();
            addDocument.Size = new System.Drawing.Size(80, 17);
            addDocument.TabIndex = 0;
            addDocument.Text = stake.Name;
            addDocument.UseVisualStyleBackColor = true;

            addDocument.Click += new System.EventHandler(this.AddStakeholder_Click);
            grpSelectStakeholders.Controls.Add(addDocument);

            uploadSiteButtonsTopPosition += 28;
        }

        public void AddStakeholder_Click(object sender, EventArgs e)
        {
            try
            {
                Control control = (Control)sender;
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    int currentStake = int.Parse(control.Name.Split('_')[1]);
                    if ((control as CheckBox).Checked)
                    {
                        long done = agent.operation.SaveSiteVisitReport(long.Parse(lblSiteId.Text), currentStake, "");
                        if (done > 0)
                        {

                        }
                        else
                        {
                            ;
                        }
                    }
                    else
                    {
                        ShowSuccessMessage("Pending");
                        (control as CheckBox).Checked = true;
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("Failed to add stakeholder");
            }
        }

        private void InitializeLoanForm(long Id)
        {
            try
            {
                navigationFrame.SelectedPage = navPageLoans;
                ribbonOpenAssesment.Visible = true;
                uploadButtonsTopPosition = 82;
                existingDocumentsPosition = 82;
                grpDocuments.Controls.OfType<SimpleButton>().ToList().ForEach(btn => btn.Dispose());
                grpDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
                grpExistingWorkflow.Controls.OfType<LinkLabel>().ToList().ForEach(btn => btn.Dispose());
                grpExistingWorkflow.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {

                    Globals.SetPickList(cmbBusIsland, "isl");
                    Globals.SetPickList(cmbBusRegType, "busregtyp");
                    Globals.SetPickList(cmbBusType, "bustyp");
                    //Globals.SetPickList(cmbResDistrict, "dis");
                    Globals.SetPickList(cmbResIsland, "isl");
                    Globals.SetPickList(cmbLO, "LoansOfficer");
                    Globals.SetGenderPickList(cmbGender);
                    Globals.SetSalutationPickList(cmbSalutation);
                    Globals.SetPickList(cmbEdu, "edu");

                    if (Id != 0)
                    {
                        //get registration
                        sbfa.LoanRequest registration = agent.operation.GetLoanRequest(Id);
                        lblId.Text = registration.Id.ToString();
                        lblReference.Text = registration.ReferenceNumber;
                        txtBusinessName.Text = registration.BusinessRegistrationNumber;
                        txtBusinessRegNumber.Text = registration.BusinessName;

                        Globals.SetPickListValue(cmbBusType, registration.FK_BusinessTypeId);
                        currentBusinessType = registration.FK_BusinessTypeId;
                        Globals.SetPickListValue(cmbBusRegType, registration.FK_BusinessRegistrationTypeId);
                        Globals.SetPickListValue(cmbBusIsland, registration.FK_BusinessIslandLocationId);

                        Globals.SetPickList(cmbBusDistrict, "dis", registration.FK_BusinessIslandLocationId);

                        Globals.SetPickListValue(cmbBusDistrict, registration.FK_BusinessIslandDistrictId);

                        Globals.SetPickListValue(cmbGender, registration.Gender);
                        Globals.SetPickListValue(cmbSalutation, registration.Salutation);

                        txtNIN.Text = registration.NIN;
                        txtFirstName.Text = registration.FirstNames;
                        txtLastName.Text = registration.LastName;
                        try
                        {
                            sbfa.Resident response = agent.operation.GetResident(txtNIN.Text);
                            txtCitizenship.Text = response.Nationality;
                        }
                        catch { }
                        //cmbSalutation.Text = registration.Salutation;
                        //cmbGender.Text = registration.Gender;
                        dtpDOB.EditValue = registration.DOB;
                        txtAge.Text = registration.Age.ToString();

                        txtSEnPAReg.Text = registration.SEnPARegistrationNo;
                        txtNoOfEmployees.Text = registration.NoOfEmployees.ToString();

                        Globals.SetPickListValue(cmbResIsland, registration.FK_ResidenceIslandLocationId);

                        Globals.SetPickList(cmbResDistrict, "dis", registration.FK_ResidenceIslandLocationId);

                        Globals.SetPickListValue(cmbResDistrict, registration.FK_ResidenceDistrictLocationId);

                        txtMobile.Text = registration.Mobile;
                        txtHomeTel.Text = registration.HomeTelephone;
                        txtWorkTel.Text = registration.WorkTelephone;
                        txtEmail.Text = registration.Email;

                        //Employment details
                        cmbEmployed.SelectedIndex = ((registration.Employed) ? 0 : 1);
                        //cmbEmployed.SelectedText = ((cmbEmployed.Employed) ? "Yes" : "No"); //
                        cmbEmploymentDetails.SelectedIndex = ((registration.EmploymentDetails == "Self-Employed") ? 0 : ((registration.EmploymentDetails == "Employed") ? 1 : 2));
                        //cmbEmploymentDetails.SelectedText = registration.EmploymentDetails;
                        txtNameOfEmployer.Text = registration.NameOfEmployer;
                        txtNoOfYearAtCurrent.Text = registration.CurrentNoOfYears.ToString();
                        txtCurrentPosition.Text = registration.CurrentPosition;
                        txtPreviousEmployer.Text = registration.PreviousEmployer;
                        txtPreviousPosition.Text = registration.PreviousPosition;
                        txtBackGroundExperience.Text = registration.BackgroundExperience;
                        txtNoOfYearsAtPrevious.Text = registration.PreviousNoOfYears.ToString();

                        //Financial details
                        txtTotalCostOfProject.Text = registration.CostOfProject.ToString();
                        txtApplicantsMonthlyIncome.Text = registration.MonthlyIncome.ToString();
                        txtApplicantsOtherIncome.Text = registration.OtherIncome.ToString();
                        txtBusinessProjectsMonthly.Text = registration.BusinessMonthlyIncome.ToString();
                        txtPersonalExpenitureTotal.Text = registration.PersonalExpenditure.ToString();
                        txtBusinessExpenditureLoan.Text = registration.BusinessExpenditureLoan.ToString();
                        txtBusinessExpenditureUtilityBills.Text = registration.BusinessExpenditureUtilityBills.ToString();
                        txtBusinessExpenditureRent.Text = registration.BusinessExpenditureRent.ToString();
                        txtBusinessExpenditureSalaries.Text = registration.BusinessExpenditureStaffSalaries.ToString();
                        txtBusinessExpenditureOther.Text = registration.BusinessExpenditureOther.ToString();
                        txtPersonalIncomeTotal.Text = registration.PersonalIncomeTotal.ToString();
                        txtPersonalExpenitureTotal.Text = registration.PersonalExpenditureTotal.ToString();
                        txtBusinessIncomeTotal.Text = registration.BusinessIncomeTotal.ToString();
                        txtBusinessExpenditureTotal.Text = registration.BusinessExpenditureTotal.ToString();
                        txtNameOfBank.Text = registration.NameOfBank.ToString();
                        txtAccountNumber.Text = registration.AccountNo.ToString();
                        cmbTypeOfAccount.SelectedIndex = ((registration.TypeOfAccount == "None") ? 0 : ((registration.TypeOfAccount == "Current") ? 1 : 2));
                        // cmbTypeOfAccount.SelectedText = registration.TypeOfAccount.ToString();
                        dtLastPaymentMade.DateTime = registration.DateOfLastPayment;
                        txtLoanBalance.Text = registration.LoanBalance.ToString();

                        //Loan Details
                        txtLoanAmountRequested.Text = registration.LoanAmountRequested.ToString();
                        if (registration.AnnualTurnoverRange == "0") cmbAnnualTurnoverRange.SelectedIndex = 0;
                        else if (registration.AnnualTurnoverRange == "1 - 250,000") cmbAnnualTurnoverRange.SelectedIndex = 1;
                        else if (registration.AnnualTurnoverRange == "250,001 - 500,000") cmbAnnualTurnoverRange.SelectedIndex = 2;
                        else if (registration.AnnualTurnoverRange == "500,001 - 750,000") cmbAnnualTurnoverRange.SelectedIndex = 3;
                        else if (registration.AnnualTurnoverRange == "750,001 - 1,000,000") cmbAnnualTurnoverRange.SelectedIndex = 4;
                        else if (registration.AnnualTurnoverRange == "1,000,000 +") cmbAnnualTurnoverRange.SelectedIndex = 5;
                        //cmbAnnualTurnoverRange.SelectedIndex = 0;
                        //cmbAnnualTurnoverRange.SelectedText = registration.AnnualTurnoverRange.ToString();
                        cmbSecurity.Text = registration.HasSecurity.ToString().Replace("True", "Yes");
                        txtPurposeOfLoan.Text = registration.PurposeOfLoan.ToString();

                        //add listview items for the type of securit added
                        displayCheckedSecurityItems(registration.TypeOfSecurity);

                        //Donor Details
                        txtDonorNIN.Text = registration.GuarantorNIN.ToString();
                        txtDonorName.Text = registration.GuarantorName.ToString();
                        txtDonorSurname.Text = registration.GuarantorSurname.ToString();
                        dtDonorDOB.DateTime = registration.GuarantorDOB;
                        txtDonorAddress.Text = registration.GuarantorAddress.ToString();
                        txtDonorContactNo.Text = registration.GuarantorContactNo.ToString();
                        cmbDonorMaritalStatus.Text = registration.GuarantorMaritalStatus.ToString();
                        txtDonorNoOfDependents.Text = registration.GuarantorNoOfDependents.ToString();
                        cmbDonorEmploymentStatus.Text = registration.GuarantorEmploymentStatus.ToString();
                        txtDonorEmployerAddress.Text = registration.GuarantorEmployersAddress.ToString();
                        txtDonorEmployerName.Text = registration.GuarantorEmployersAddress.ToString();
                        txtDonorCurrentPosition.Text = registration.GuarantorCurrentPosition.ToString();
                        txtDonorYearsOfEmployment.Text = registration.GuarantorNoOfYears.ToString();
                        txtDonorMonthlyIncome.Text = registration.GuarantorTotalMonthlyIncome.ToString();
                        txtDonorMonthlyExpenditure.Text = registration.GuarantorTotalMonthlyExpenditure.ToString();

                        Globals.SetPickListValue(cmbLO, registration.FK_LoansOfficerId);

                        //btnWorkFlow.Text = agent.operation.GetCurrentWorkFlowStage(long.Parse(lblId.Text), "loan");
                        //get current stage
                        sbfa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(long.Parse(lblId.Text), "loan");
                        //lblStage.Text = currentStage.StageName;
                        if (currentStage.RequireDocuments)
                        {
                            grpDocuments.Visible = true;
                            sbfa.WorkFlowStageDocumentStatus[] rdocuments = agent.operation.GetDocumentsRequiredStatus(long.Parse(lblId.Text), "loan");
                            foreach (sbfa.WorkFlowStageDocumentStatus doc in rdocuments)
                            {
                                DocumentButton(doc);
                            }
                        }
                        else
                        {
                            grpDocuments.Visible = false;
                        }

                        if (currentStage.RequirePayment)
                        {
                            cmbWorkFlowSkip.Links[0].Visible = false;
                            cmdWorkFlow.Links[0].Visible = false;
                            cmdWorkFlowRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        }
                        else
                        {
                            cmdWorkFlowRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        }

                        if (currentStage.StageOptional)
                        {
                            cmbWorkFlowSkip.Links[0].Visible = true;
                        }
                        else
                        {
                            cmbWorkFlowSkip.Links[0].Visible = false;
                        }

                        if (currentStage.RequireSiteVisit || currentStage.RequireRecommendations)
                        {
                            loanSiteRibbon.Visible = true;
                            loanSiteRibbon.Visible = ((Globals.hasAccess("siteVisit")) ? true : false);
                        }
                        else
                        {
                            loanSiteRibbon.Visible = false;
                        }

                        if (currentStage.RequireSiteVisit)
                        {
                            cmdNavNewVisit.Links[0].Visible = true;
                            cmdNavVisitReport.Links[0].Visible = true;
                        }
                        else
                        {
                            cmdNavNewVisit.Links[0].Visible = false;
                            cmdNavVisitReport.Links[0].Visible = false;
                        }

                        if (currentStage.RequireRecommendations)
                        {
                            cmdNavRecommendations.Links[0].Visible = true;
                        }
                        else
                        {
                            cmdNavRecommendations.Links[0].Visible = false;
                        }


                        if (!agent.operation.CheckAccessToStage(long.Parse(lblId.Text), "loan") || currentStage.StageName == "Complete")
                        {
                            cmdWorkFlow.Links[0].Visible = false;
                        }
                        else
                        {
                            cmdWorkFlow.Links[0].Visible = true;
                        }

                        if (currentStage.StageName == "Complete")
                        {
                            cmdAssesLoan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            wrkFlowToolsRibbon.Visible = false;
                            ribbonSaveLoan.Visible = true;
                            Save.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        }
                        else
                        {
                            loanAssRibbon.Visible = false;
                            wrkFlowToolsRibbon.Visible = true;
                            ribbonSaveLoan.Visible = true;
                            Save.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        }
                        //get existing docs
                        sbfa.AutoDocument[] autoDocs = agent.operation.CheckAutoDocument(currentId, "loan");
                        foreach (sbfa.AutoDocument auto in autoDocs)
                        {
                            DocumentLink(auto);
                        }

                        sbfa.WorkFlowStageDocumentStatus[] documents = agent.operation.GetAllRequiredDocuments(currentId, "loan");
                        foreach (sbfa.WorkFlowStageDocumentStatus doc in documents)
                        {
                            DocumentLink(doc, "business");
                        }

                    }
                }

                RefreshLoanIndicator();

                wrkFlowToolsRibbon.Visible = ((Globals.hasAccess("processLoan")) ? true : false);

                ribbonOpenAssesment.Visible = ((Globals.hasAccess("assessLoan")) ? true : false);
                loanAssRibbon.Visible = ((Globals.hasAccess("approveLoan")) ? true : false);
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("The system has failed to initialize your form properly");
            }
        }

        private void InitializeNewQualifiedLoan()
        {
            try
            {
                navigationFrame.SelectedPage = navPageLoans;
                ribbonOpenAssesment.Visible = false;
                wrkFlowToolsRibbon.Visible = false;
                loanAssRibbon.Visible = false;
                loanSiteRibbon.Visible = false;
                grpDocuments.Controls.OfType<SimpleButton>().ToList().ForEach(btn => btn.Dispose());
                grpDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
                Globals.SetPickList(cmbBusIsland, "isl");
                Globals.SetPickList(cmbBusRegType, "busregtyp");
                Globals.SetPickList(cmbBusType, "bustyp");
                //Globals.SetPickList(cmbResDistrict, "dis");
                Globals.SetPickList(cmbResIsland, "isl");
                Globals.SetPickList(cmbLO, "LoansOfficer");
                Globals.SetGenderPickList(cmbGender);
                Globals.SetSalutationPickList(cmbSalutation);

                lblId.Text = "0";
                lblReference.Text = "0";
                txtBusinessName.Text = txtPreBusinessName.Text;
                txtBusinessRegNumber.Text = txtPreBIN.Text; ;

                Globals.SetPickListValue(cmbBusType, cmbPreBusinessType.SelectedValue);

                txtNIN.Text = txtPreNIN.Text;
                txtFirstName.Text = txtPreNames.Text;
                txtLastName.Text = txtPreSurname.Text; ;
                //cmbSalutation.Text = registration.Salutation;
                //cmbGender.Text = registration.Gender;
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    //check from local db first for details
                    sbfa.BusinessRegistration registration = agent.operation.GetBusinessRegistration(txtNIN.Text);

                    if (registration == null || registration.FirstNames == null)
                    {
                        sbfa.Resident response = agent.operation.GetResident(txtNIN.Text);

                        // Type { get => type; set => type = value; }
                        // Status { get => status; set => status = value; }
                        dtpDOB.DateTime = response.DateOfBirth;
                        DateTime now = DateTime.Today;
                        int age = now.Year - response.DateOfBirth.Year;
                        if (response.DateOfBirth > now.AddYears(-age)) age--;

                        txtAge.Text = age.ToString();
                    }

                }
                txtMobile.Text = "";
                txtHomeTel.Text = "";
                txtWorkTel.Text = "";
                txtEmail.Text = txtPreEmail.Text;

                txtSEnPAReg.Text = txtPreSEnPA.Text;
                txtNoOfEmployees.Text = txtPreNoOfEmployees.Text;

                //Employment details
                cmbEmployed.Text = "";
                cmbEmploymentDetails.Text = "";
                txtNameOfEmployer.Text = "";
                txtNoOfYearAtCurrent.Text = "0";
                txtCurrentPosition.Text = "";
                txtPreviousEmployer.Text = "";
                txtPreviousPosition.Text = "";
                txtBackGroundExperience.Text = "";

                //Financial details
                txtTotalCostOfProject.Text = "0";
                txtApplicantsMonthlyIncome.Text = "0";
                txtApplicantsOtherIncome.Text = "0";
                txtBusinessProjectsMonthly.Text = "0";
                txtPersonalExpenitureTotal.Text = "0";
                txtBusinessExpenditureLoan.Text = "0";
                txtBusinessExpenditureUtilityBills.Text = "0";
                txtBusinessExpenditureRent.Text = "0";
                txtBusinessExpenditureSalaries.Text = "0";
                txtBusinessExpenditureOther.Text = "0";
                txtPersonalIncomeTotal.Text = "0";
                txtPersonalExpenitureTotal.Text = "0";
                txtBusinessIncomeTotal.Text = "0";
                txtBusinessExpenditureTotal.Text = "0";
                txtNameOfBank.Text = "";
                txtAccountNumber.Text = "";
                cmbTypeOfAccount.SelectedIndex = 0;
                dtLastPaymentMade.DateTime = DateTime.Now.AddYears(200);
                txtLoanBalance.Text = "0";

                //Loan Details
                txtLoanAmountRequested.Text = txtPreLoanAmount.Text;
                cmbAnnualTurnoverRange.SelectedValue = cmbPreAnnualTurnover.SelectedValue;
                cmbSecurity.SelectedValue = "Yes";
                txtPurposeOfLoan.Text = "";
                chkTypeOfSecurity.Items.Clear();

                using (new OperationContextScope(agent.context))
                {
                    sbfa.ReferenceTable[] secType = agent.operation.GetReferenceTableItems("sec");
                    foreach (sbfa.ReferenceTable sec in secType)
                    {
                        string currentSec = "_" + sec.Id.ToString();
                        chkTypeOfSecurity.Items.Add(sec.Name);

                    }
                }

                chkTypeOfSecurity.SelectedValue = chkSecurity.SelectedValue;
                currentId = 0;

                //Donor Details
                txtDonorNIN.Text = "";
                txtDonorName.Text = "";
                txtDonorSurname.Text = "";
                dtDonorDOB.DateTime = DateTime.Now.AddYears(200);
                txtDonorAddress.Text = "";
                txtDonorContactNo.Text = "";
                cmbDonorMaritalStatus.SelectedIndex = 0;
                txtDonorNoOfDependents.Text = "";
                cmbDonorEmploymentStatus.SelectedIndex = 0;
                txtDonorEmployerAddress.Text = "";
                txtDonorEmployerName.Text = "";
                txtDonorCurrentPosition.Text = "";
                txtDonorYearsOfEmployment.Text = "";
                txtDonorMonthlyIncome.Text = "";
                txtDonorMonthlyExpenditure.Text = "";

                Globals.SetPickListValue(cmbLO, 0);

                RefreshLoanIndicator();
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There was an issue initializing your pre-qualification form");
            }
        }

        private void displayCheckedSecurityItems(string dbItems)
        {
            try
            {
                chkTypeOfSecurity.Items.Clear();
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.ReferenceTable[] secType = agent.operation.GetReferenceTableItems("sec");
                    int x = 0;
                    foreach (sbfa.ReferenceTable sec in secType)
                    {
                        chkTypeOfSecurity.Items.Add(sec.Name);
                        if (dbItems.Contains(sec.Name.Trim()))
                        {
                            chkTypeOfSecurity.SetItemChecked(x, true);
                        }
                        x++;
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("Display of Security list has encountered an error");
            }
        }

        private void InitializeLoanApprovalForm(long Id)
        {
            try
            {
                wrkFlowToolsRibbon.Visible = false;
                ribbonSaveLoan.Visible = false;
                loanSiteRibbon.Visible = false;
                // btnWorkFlow.Visible = true;
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {

                    if (Id != 0)
                    {
                        cmdApproveLoan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        cmdDeclineLoan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        cmdAssesLoan.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        sbfa.LoanRequest loan = agent.operation.GetLoanRequest(long.Parse(lblId.Text));
                        alblAmount.Text = "SCR" + loan.LoanAmountRequested.ToString();
                        alblBusuness.Text = loan.BusinessName;
                        alblName.Text = loan.FirstNames + " " + loan.LastName;
                        //get current stage
                        sbfa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(long.Parse(lblId.Text), "loaapp");
                        lblApproveStage.Text = currentStage.StageName;
                        currentApprovalStage = currentStage.StageName;

                        if (!agent.operation.CheckAccessToStage(long.Parse(lblId.Text), "loaapp") || currentStage.StageName == "Complete")
                        {
                            loanAssRibbon.Visible = false;
                        }
                        else
                        {
                            loanAssRibbon.Visible = true;
                        }

                        lblPreviousComments.Text = "";

                        sbfa.LoanApprovalRecommendations[] recs = agent.operation.GetLoanApprovalRecommendations(currentId);
                        foreach (sbfa.LoanApprovalRecommendations rec in recs)
                        {
                            lblPreviousComments.Text = lblPreviousComments.Text + "\n\n" + rec.FK_Stage + "\n" + rec.Recommendation;
                        }
                    }
                }

                RefreshRecommendationIndicator();
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("Failed to properly initiaize approval form");
            }
        }

        private void InitializeNewLoanForm()
        {
            try
            {
                navigationFrame.SelectedPage = navPageLoans;
                ribbonOpenAssesment.Visible = false;
                //Globals.SetPickList(cmbBusDistrict, "dis");
                Globals.SetPickList(cmbBusIsland, "isl");
                Globals.SetPickList(cmbBusRegType, "busregtyp");
                Globals.SetPickList(cmbBusType, "bustyp");
                Globals.SetPickList(cmbEdu, "edu");
                // Globals.SetPickList(cmbResDistrict, "dis");
                Globals.SetPickList(cmbResIsland, "isl");
                Globals.SetGenderPickList(cmbGender);
                Globals.SetSalutationPickList(cmbSalutation);
                Globals.SetPickList(cmbLO, "LoansOfficer");
                chkTypeOfSecurity.Items.Clear();
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.ReferenceTable[] secType = agent.operation.GetReferenceTableItems("sec");
                    foreach (sbfa.ReferenceTable sec in secType)
                    {
                        string currentSec = "_" + sec.Id.ToString();
                        chkTypeOfSecurity.Items.Add(sec.Name);

                    }
                }
                currentId = 0;
                uploadButtonsTopPosition = 82;
                grpDocuments.Controls.OfType<SimpleButton>().ToList().ForEach(btn => btn.Dispose());
                grpDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
                lblId.Text = "0";
                lblReference.Text = "0";
                txtBusinessName.Text = "";
                txtBusinessRegNumber.Text = "";
                currentBusinessType = 0;
                txtNIN.Text = "";
                txtFirstName.Text = "";
                txtLastName.Text = "";
                txtCitizenship.Text = "";
                txtMobile.Text = "";
                txtHomeTel.Text = "";
                txtWorkTel.Text = "";
                txtEmail.Text = "";
                wrkFlowToolsRibbon.Visible = false;
                loanAssRibbon.Visible = false;
                loanSiteRibbon.Visible = false;

                txtMobile.Text = "";
                txtHomeTel.Text = "";
                txtWorkTel.Text = "";
                txtEmail.Text = "";

                txtSEnPAReg.Text = "";
                txtNoOfEmployees.Text = "0";

                //Employment details
                cmbEmployed.Text = "";
                cmbEmploymentDetails.Text = "";
                txtNameOfEmployer.Text = "";
                txtNoOfYearAtCurrent.Text = "0";
                txtCurrentPosition.Text = "";
                txtPreviousEmployer.Text = "";
                txtPreviousPosition.Text = "";
                txtBackGroundExperience.Text = "";

                //Financial details
                txtTotalCostOfProject.Text = "0";
                txtApplicantsMonthlyIncome.Text = "0";
                txtApplicantsOtherIncome.Text = "0";
                txtBusinessProjectsMonthly.Text = "0";
                txtPersonalExpenitureTotal.Text = "0";
                txtBusinessExpenditureLoan.Text = "0";
                txtBusinessExpenditureUtilityBills.Text = "0";
                txtBusinessExpenditureRent.Text = "0";
                txtBusinessExpenditureSalaries.Text = "0";
                txtBusinessExpenditureOther.Text = "0";
                txtPersonalIncomeTotal.Text = "0";
                txtPersonalExpenitureTotal.Text = "0";
                txtBusinessIncomeTotal.Text = "0";
                txtBusinessExpenditureTotal.Text = "0";
                txtNameOfBank.Text = "";
                txtAccountNumber.Text = "";
                cmbTypeOfAccount.SelectedIndex = 0;
                dtLastPaymentMade.DateTime = DateTime.Now.AddYears(200);
                txtLoanBalance.Text = "0";

                //Loan Details
                txtLoanAmountRequested.Text = "0";
                cmbAnnualTurnoverRange.SelectedIndex = 0;
                cmbSecurity.SelectedValue = "Yes";
                txtPurposeOfLoan.Text = "";

                currentId = 0;

                //Donor Details
                txtDonorNIN.Text = "";
                txtDonorName.Text = "";
                txtDonorSurname.Text = "";
                dtDonorDOB.DateTime = DateTime.Now.AddYears(200);
                txtDonorAddress.Text = "";
                txtDonorContactNo.Text = "";
                cmbDonorMaritalStatus.SelectedIndex = 0;
                txtDonorNoOfDependents.Text = "0";
                cmbDonorEmploymentStatus.SelectedIndex = 0;
                txtDonorEmployerAddress.Text = "";
                txtDonorEmployerName.Text = "";
                txtDonorCurrentPosition.Text = "";
                txtDonorYearsOfEmployment.Text = "0";
                txtDonorMonthlyIncome.Text = "0";
                txtDonorMonthlyExpenditure.Text = "0";

                RefreshLoanIndicator();
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("An error has occured while initializing loan form");
            }
        }

        private void InitializeSiteVisit()
        {
            try
            {
                cmdSiteSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                cmdSiteScheduleSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                Globals.SetPickList(cmbSVLO, "LoansOfficer");
                Globals.SetPickList(cmbSVLM, "LoansManager");
                this.uploadSiteButtonsTopPosition = 8;
                grpSelectStakeholders.Controls.OfType<CheckBox>().ToList().ForEach(btn => btn.Dispose());

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.Stakeholder[] current = agent.operation.GetBusinessTypeStakeholders(int.Parse(currentBusinessType.ToString()));
                    foreach (sbfa.Stakeholder stake in current)
                    {
                        StakeholderSelect(stake);
                    }
                    //check for site visit id
                    sbfa.SiteVisit site = agent.operation.GetCurrentWorkflowSiteVisit(currentId, "loan");
                    if (site == null || site.CreatedBy == null)
                    {
                        //add site
                        long siteId = agent.operation.CreateWorkflowSiteVisit(currentId, "loan");
                        lblSiteId.Text = siteId.ToString();
                        site = agent.operation.GetCurrentWorkflowSiteVisit(currentId, "loan");
                    }
                    else
                    {
                        ;
                    }
                    lblSiteId.Text = site.Id.ToString();
                    //get applicant details if registration                    
                    sbfa.LoanRequest reg = agent.operation.GetLoanRequest(site.FK_DocumentId);

                    txtSVPhone.Text = reg.Mobile;
                    rlblSiteName.Text = reg.FirstNames + " " + reg.LastName + " (" + reg.NIN + ")";
                    rlblSiteBusiness.Text = reg.BusinessName;
                    rlblSiteTime.Text = DateTime.Now.ToShortDateString();

                    txtSVAddress.Text = site.VisitAddress;
                    dtpSVDate.DateTime = site.VisitDate;
                    chkSVConfirmed.Checked = site.Confirmed;
                    chkSVPhone.Checked = site.Phone;
                    chkSVSMS.Checked = site.SMS;
                    chkSVEmail.Checked = site.Email;
                    Globals.SetPickListValue(cmbSVLO, site.FK_LO);
                    Globals.SetPickListValue(cmbSVLM, site.FK_LM);

                    if (site.OfficerComment == "" || site.ManagerComment == "")
                    {
                        approveSiteRibbon.Visible = true;
                        confirmSiteRibbon.Visible = false;

                        if (site.OfficerComment != "")
                        {
                            svOfficerApprove = true;
                            lblOfficerComment.Text = site.OfficerComment;
                            chkOfficerApprove.Checked = site.OfficerApproved;
                            //check manager access
                            bool mgr = false;
                            foreach (string v in Globals.userGroupRoles)
                            {
                                if (v == "LoansManager")
                                    mgr = true;
                            }
                            if (!mgr)
                            {
                                approveSiteRibbon.Visible = false;
                            }
                        }
                        else
                        {
                            svOfficerApprove = false;
                        }
                    }
                    else
                    {
                        approveSiteRibbon.Visible = false;
                        confirmSiteRibbon.Visible = true;
                    }

                    if (site.Confirmed)
                    {
                        cmdConfirmSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        ribbonPageNotifySiteVisit.Visible = true;
                        cmdNotifySiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        cmdRescheduleSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmdConfirmSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        ribbonPageNotifySiteVisit.Visible = false;
                        cmdNotifySiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdRescheduleSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    //get reports
                    sbfa.SiteVisitReport[] reports = agent.operation.GetSiteVisitReports(long.Parse(lblSiteId.Text));
                    foreach (sbfa.SiteVisitReport rep in reports)
                    {
                        (grpSelectStakeholders.Controls["stake" + "_" + rep.FK_StakeholderId.ToString()] as CheckBox).Checked = true;
                    }

                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There has been an error initializing your site visit");
            }
        }

        private void InitializeSiteVisitReport()
        {
            try
            {
                cmdSiteSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                cmdSiteScheduleSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                approveSiteRibbon.Visible = false;
                confirmSiteRibbon.Visible = false;
                ribbonPageNotifySiteVisit.Visible = false;
                Globals.SetPickList(cmbSVRLO, "LoansOfficer");
                Globals.SetPickList(cmbSVRLM, "LoansManager");

                uploadSiteButtonsTopPosition = 82;
                grpSiteDocuments.Controls.OfType<SimpleButton>().ToList().ForEach(btn => btn.Dispose());
                grpSiteDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    //check for site visit id
                    sbfa.SiteVisit site = agent.operation.GetCurrentWorkflowSiteVisit(currentId, "loan");
                    if (site == null || site.CreatedBy == null)
                    {
                        //add site
                        long siteId = agent.operation.CreateWorkflowSiteVisit(currentId, "loan");
                        lblSiteId.Text = siteId.ToString();
                        site = agent.operation.GetCurrentWorkflowSiteVisit(currentId, "loan");
                    }
                    else
                    {
                        ;
                    }
                    lblSiteId.Text = site.Id.ToString();

                    Globals.SetPickListValue(cmbSVRLO, site.FK_LO);
                    Globals.SetPickListValue(cmbSVRLM, site.FK_LM);
                    txtVisitBackground.Text = site.Background;
                    txtVisitConclusion.Text = site.Conclusion;
                    txtVisitDescription.Text = site.Description;
                    txtVisitPurpose.Text = site.Purpose;
                    txtVisitRecommendation.Text = site.Recommendation;

                    //get applicant details if registration                    
                    sbfa.LoanRequest reg = agent.operation.GetLoanRequest(site.FK_DocumentId);
                    lblSiteName.Text = reg.FirstNames + " " + reg.LastName;
                    lblSiteBusiness.Text = reg.BusinessName;
                    lblSiteTime.Text = DateTime.Now.ToShortDateString();

                    sbfa.SiteVisitReport[] documents = agent.operation.GetSiteVisitReports(long.Parse(lblSiteId.Text));
                    foreach (sbfa.SiteVisitReport doc in documents)
                    {
                        DocumentButton(doc);
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There has been an error initializing the site visit report");
            }
        }

        private void InitializeRecommendations()
        {
            try
            {
                lstRecoStake.Items.Clear();
                treeRecoStake.Nodes["existingStakeholders"].Nodes.Clear();
                Globals.SetPickList(cmbAction, "act");
                //Globals.SetPickList(cmbBDOReco, "BusinessDevelopmentOfficer");
                Globals.SetRecommendationStatusPickList(cmbStatusReco);

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.Stakeholder[] current = agent.operation.GetBusinessTypeStakeholders(int.Parse(currentBusinessType.ToString()));
                    foreach (sbfa.Stakeholder stake in current)
                    {
                        string currentStake = "_" + stake.Id.ToString();
                        treeRecoStake.Nodes["existingStakeholders"].Nodes.Add(currentStake, stake.Name);
                    }
                    //check for last site visit id
                    sbfa.Recommendations reco = agent.operation.GetCurrentWorkflowRecommendations(currentId, "loan");
                    if (reco == null || reco.CreatedBy == null)
                    {
                        //add site
                        long recoId = agent.operation.CreateWorkflowRecommendations(currentId, "loan");
                        rlblRecoId.Text = recoId.ToString();
                        reco = agent.operation.GetCurrentWorkflowRecommendations(currentId, "loan");
                    }
                    else
                    {
                        ;
                    }

                    rlblRecoId.Text = reco.Id.ToString();
                    //get applicant details if registration                    
                    sbfa.LoanRequest reg = agent.operation.GetLoanRequest(reco.FK_DocumentId);
                    rlblName.Text = reg.FirstNames + " " + reg.LastName;
                    rlblBusiness.Text = reg.BusinessName;
                    rlblLoc.Text = "";
                    //get reports
                    sbfa.RecommendedAction[] reports = agent.operation.GetRecommendedActions(long.Parse(rlblRecoId.Text));
                    foreach (sbfa.RecommendedAction rep in reports)
                    {
                        string[] row = { rep.FK_StakeholderId.ToString(), agent.operation.GetEntityName(rep.FK_StakeholderId, "stahol"), agent.operation.GetEntityName(rep.FK_ActionId, "act"), "", rep.Status };
                        var listViewItem = new ListViewItem(row);
                        lstRecoStake.Items.Add(listViewItem);
                        treeRecoStake.Nodes["existingStakeholders"].Nodes["_" + rep.FK_StakeholderId.ToString()].Remove();
                    }

                }

                navigationFrame.SelectedPage = navPageRecomendations;
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There has been an error initializing recommendations form.");
            }
        }

        private void InitializeLibrary()
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.DocumentFolders[] response = agent.operation.GetFolders(currentFolderId);
                    for (int x = 0; x < response.Length; x++)
                    {
                        treeFolders.Nodes["documentLibrary_1"].Nodes.Add("folder_" + response[x].Id.ToString(), response[x].FolderName);
                        pnlExplorer.Controls.Add(folderControl(response[x].FolderName, response[x].Id));
                    }

                    Globals.SetPickList(cmbDocumentType, "doctyp");

                    sbfa.ReferenceTable[] doctypes = agent.operation.GetReferenceTableItems("doctyp");
                    foreach (sbfa.ReferenceTable typ in doctypes)
                    {
                        treeFolders.Nodes["documentLibrary_3"].Nodes.Add("docType_" + typ.Id.ToString(), typ.Name);
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("Library initialization has encountered an error.");
            }
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
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SBFA.Properties.Resources.PDF;
            else if (fileType.IndexOf("word") >= 0)
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SBFA.Properties.Resources.word;
            else if (fileType.IndexOf("excel") >= 0)
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SBFA.Properties.Resources.excel;
            else if (fileType.IndexOf("jpg") >= 0)
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SBFA.Properties.Resources.JPG;
            else if (fileType.IndexOf("png") >= 0)
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SBFA.Properties.Resources.PNG;
            else if (fileType.IndexOf("gif") >= 0)
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SBFA.Properties.Resources.GIF;
            else
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SBFA.Properties.Resources.uknwn;

            return newFile;
        }

        UserControl fileTemplateControl(string name, string fileType, string Id)
        {
            Folder newFile = new Folder();
            newFile.Controls["lblText"].Text = name;
            newFile.Controls["lblId"].Text = Id;
            newFile.Controls["lblText"].DoubleClick += new System.EventHandler(this.fileTemplatePic_DoubleClick);
            newFile.Controls["folderPic"].DoubleClick += new System.EventHandler(this.fileTemplatePic_DoubleClick);
            //set filetype image
            if (fileType.IndexOf("pdf") >= 0)
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SBFA.Properties.Resources.PDF;
            else if (fileType.IndexOf("word") >= 0)
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SBFA.Properties.Resources.word;
            else if (fileType.IndexOf("excel") >= 0)
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SBFA.Properties.Resources.excel;
            else if (fileType.IndexOf("jpg") >= 0)
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SBFA.Properties.Resources.JPG;
            else if (fileType.IndexOf("png") >= 0)
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SBFA.Properties.Resources.PNG;
            else if (fileType.IndexOf("gif") >= 0)
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SBFA.Properties.Resources.GIF;
            else
                (newFile.Controls["folderPic"] as System.Windows.Forms.PictureBox).Image = global::SBFA.Properties.Resources.uknwn;

            return newFile;
        }

        private void folderPic_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                pnlExplorer.Controls.Clear();
                Control control = (Control)sender;

                currentFolderId = long.Parse(control.Parent.Controls["lblId"].Text);
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    lblFolderMap.Text = agent.operation.GetFolderPath(currentFolderId).Replace(",", " / ");

                    sbfa.DocumentFolders[] response = agent.operation.GetFolders(currentFolderId);
                    for (int x = 0; x < response.Length; x++)
                    {
                        pnlExplorer.Controls.Add(folderControl(response[x].FolderName, response[x].Id));
                    }
                    //get files
                    sbfa.DocumentLibrary[] documents = agent.operation.GetFolderDocuments(currentFolderId);
                    foreach (sbfa.DocumentLibrary doc in documents)
                    {
                        pnlExplorer.Controls.Add(fileControl(doc.DocumentName, doc.DocumentContentType, doc.Id));
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There has been an error while openning folder");
            }
        }

        private void filePic_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Control control = (Control)sender;
                long docId = long.Parse(control.Parent.Controls["lblId"].Text);
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.DocumentLibrary doc = agent.operation.GetDocument(docId);
                    string filePath = Application.StartupPath + "\\filer\\" + control.Parent.Controls["lblText"].Text;
                    FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                    fs.Write(doc.DocumentData, 0, doc.DocumentData.Length);
                    fs.Flush();
                    fs.Close();
                    try
                    {
                        System.Diagnostics.Process newProcess = new System.Diagnostics.Process();
                        newProcess.StartInfo = new System.Diagnostics.ProcessStartInfo(filePath);
                        newProcess.Start();
                        newProcess.WaitForExit();
                    }
                    catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
                    catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("Please install a PDF Reader on your computer and try again.");
                    }
                    try
                    {
                        System.IO.File.Delete(filePath);
                    }
                    catch
                    {

                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("Failed to open required document");
            }
        }

        private void fileTemplatePic_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                Control control = (Control)sender;
                string docId = (control.Parent.Controls["lblId"].Text);
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.DocumentTemplateLibrary doc = agent.operation.GetDocumentTemplate(docId);
                    string filePath = Application.StartupPath + "\\filer\\" + control.Parent.Controls["lblText"].Text;
                    FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                    fs.Write(doc.DocumentData, 0, doc.DocumentData.Length);
                    fs.Flush();
                    fs.Close();
                    try
                    {
                        System.Diagnostics.Process newProcess = new System.Diagnostics.Process();
                        newProcess.StartInfo = new System.Diagnostics.ProcessStartInfo(filePath);
                        newProcess.Start();
                        newProcess.WaitForExit();
                    }
                    catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
                    catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("Please install a PDF Reader on your computer and try again.");
                    }
                    try
                    {
                        System.IO.File.Delete(filePath);
                    }
                    catch
                    {

                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("Error accessing file");
            }
        }

        private TreeNode FindRootNode(TreeNode treeNode)
        {
            while (treeNode.Parent != null)
            {
                treeNode = treeNode.Parent;
            }
            return treeNode;
        }

        private void InitializeWorkflowAdmin()
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.WorkFlows[] response = agent.operation.GetWorkFlows();
                    foreach (sbfa.WorkFlows wrkFlow in response)
                    {
                        string currentFlow = "_" + wrkFlow.Id.ToString();
                        treeWorkFlow.Nodes["workFlows"].Nodes.Add(currentFlow, wrkFlow.WorkFlowName);
                    }

                    Globals.SetPickList(cmbEnd, "rolgro");
                    Globals.SetPickList(cmbStart, "rolgro");
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("Error initializing workflow administration form");
            }
        }

        private void InitializeBusinessTypes()
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.ReferenceTable[] busType = agent.operation.GetReferenceTableItems("bustyp");
                    foreach (sbfa.ReferenceTable bus in busType)
                    {
                        string currentBus = "_" + bus.Id.ToString();
                        treeBusinessType.Nodes["businessType"].Nodes.Add(currentBus, bus.Name);
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("Error initializing administration form");
            }
        }

        private void InitializeDocumentDesignAdmin()
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.AutoDocumentsDesign[] response = agent.operation.GetAutoDocumentsDesigns();
                    foreach (sbfa.AutoDocumentsDesign desg in response)
                    {
                        string currentDesign = "_" + desg.DocumentName;
                        treeDesignDocs.Nodes["designs"].Nodes.Add(currentDesign, desg.DocumentName.ToUpper());
                    }

                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("Error initializing administration form");
            }
        }

        public void GoToLoansApplication()
        {

        }

        private bool CheckForValue(Control parent)
        {
            bool seT = true;
            foreach (Control c in parent.Controls)
            {
                if (c.GetType() == typeof(LookUpEdit))
                {
                    if ((c as LookUpEdit).EditValue == null)
                    {
                        seT = false;
                        (c as LookUpEdit).Focus();
                        break;
                    }
                }
                else if (c.GetType() == typeof(TextEdit))
                {
                    if ((c as TextEdit).Text == "")
                    {
                        if ((c as TextEdit).Name == "txtNIN")
                        {
                            seT = false;
                            (c as TextEdit).Focus();
                            break;
                        }
                    }
                }
                else
                {
                    CheckForValue(c);
                }
            }

            return seT;
        }

        private void InitializeQuickStats()
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.QuickStats response = agent.operation.GetQuickStats();
                    lblregCount.Text = response.RegisteredBusinessCount.ToString();
                    lblloanCount.Text = response.LoanCount.ToString();
                    lblpenloanCount.Text = response.PendingLoanCount.ToString();
                    lblsiteCount.Text = response.PendingSiteVisitsCount.ToString();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("Quick Statistics have not been loaded properly");
            }
        }

        public void DocumentLink(sbfa.Notifications doc)
        {
            LinkLabel addDocument = new LinkLabel();
            addDocument.LinkColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            addDocument.Location = new System.Drawing.Point(43, (this.existingDocumentsPosition));
            addDocument.Name = "_" + doc.Id.ToString() + "_" + doc.DocumentType.ToString() + "_" + doc.FK_DocumentId;
            //addDocument.Size = new System.Drawing.Size(169, 13);
            addDocument.AutoSize = true;
            addDocument.TabIndex = 27;
            addDocument.Font = new Font("Segoe UI", 10);
            addDocument.TabStop = true;
            addDocument.Text = doc.Title;
            addDocument.Click += new System.EventHandler(this.OpenNotificationDocument_Click);
            PictureBox pic = new PictureBox();
            pic.Image = picNot.Image;
            // pic.Image = ((System.Drawing.Image)(sbfa.Properties.Resources.sign_tick));
            pic.Location = new System.Drawing.Point(8, this.existingDocumentsPosition);
            pic.Name = "picNotView" + "_" + doc.Id.ToString();
            pic.Size = new System.Drawing.Size(26, 26);
            pic.TabIndex = 12;
            pic.TabStop = false;
            pic.Visible = true;

            pnlNotifications.Controls.Add(addDocument);
            pnlNotifications.Controls.Add(pic);

            existingDocumentsPosition += 38;
        }

        private void OpenNotificationDocument_Click(object sender, EventArgs e)
        {
            try
            {
                Control control = (Control)sender;
                long docId = long.Parse(control.Name.Split('_')[3]);
                string docType = control.Name.Split('_')[2];
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    if (docType == "invoice")
                    {
                        try
                        {
                            currentInvoiceId = docId;

                            new ProcessInvoice().ShowDialog();
                        }
                        catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
                        catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
                        catch (Exception ex)
                        {

                        }
                    }
                    else if (docType == "loan" || docType == "loanSite")
                    {
                        try
                        {
                            currentId = docId;

                            InitializeLoanForm(currentId);
                        }
                        catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
                        catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
                        catch (Exception ex)
                        {

                        }
                    }
                    else if (docType == "paymentVoucher")
                    {
                        try
                        {
                            currentLoanDisId = docId;

                            InitializeSignedDisbursementRequestForm(currentLoanDisId);
                        }
                        catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
                        catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
                        catch (Exception ex)
                        {
                            ShowErrorMessage(ex.Message);
                        }
                    }

                    agent.operation.UpdateNotifications(long.Parse(control.Name.Split('_')[1]), true);
                    (pnlNotifications.Controls["picNotView" + "_" + control.Name.Split('_')[1]] as PictureBox).Image = picNotRead.Image;
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void InitializeNotifications()
        {
            try
            {
                pnlNotifications.Controls.OfType<LinkLabel>().ToList().ForEach(btn => btn.Dispose());
                pnlNotifications.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    this.existingDocumentsPosition = 8;
                    sbfa.Notifications[] response = agent.operation.CheckNotifications("", true);
                    foreach (sbfa.Notifications not in response)
                    {
                        DocumentLink(not);
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        public void DisburseButton(sbfa.LoanRequest doc, bool saved, string type)
        {
            SimpleButton addDocument = new SimpleButton();
            addDocument.Appearance.BackColor = System.Drawing.Color.Lavender;
            addDocument.Appearance.Options.UseBackColor = true;
            addDocument.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            addDocument.ImageOptions.Image = global::SBFA.Properties.Resources.sign_add;
            addDocument.Location = new System.Drawing.Point(40, this.uploadButtonsTopPosition);
            addDocument.LookAndFeel.SkinName = "Office 2016 Black";
            addDocument.Name = "_" + doc.Id.ToString() + "_" + type;
            addDocument.Size = new System.Drawing.Size(157, 26);
            addDocument.TabIndex = 11;
            addDocument.Text = type;

            addDocument.Click += new System.EventHandler(this.DisburseUplaodDocument_Click);

            PictureBox pic = new PictureBox();
            if (saved)
                pic.Image = picDone.Image;
            else
                pic.Image = picBlur.Image;

            // pic.Image = ((System.Drawing.Image)(sbfa.Properties.Resources.sign_tick));
            pic.Location = new System.Drawing.Point(8, this.uploadButtonsTopPosition);
            pic.Name = "picD" + "_" + doc.Id.ToString() + "_" + type;
            pic.Size = new System.Drawing.Size(26, 26);
            pic.TabIndex = 12;
            pic.TabStop = false;
            pic.Visible = true;

            grpDisbursementDocuments.Controls.Add(addDocument);
            grpDisbursementDocuments.Controls.Add(pic);

            uploadButtonsTopPosition += 38;
        }

        private void DisburseUplaodDocument_Click(object sender, EventArgs e)
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
                    bool done = agent.operation.UploadWorkFlowDocument(long.Parse(control.Name.Split('_')[1]), control.Name.Split('_')[2], fileName, buffer, 0, 3, false);
                    if (done)
                    {
                        (grpDisbursementDocuments.Controls["picD" + "_" + control.Name.Split('_')[1] + "_" + control.Name.Split('_')[2]] as PictureBox).Image = picDone.Image;
                    }
                    else
                    {
                        ShowErrorMessage("Not done !!!");
                    }

                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("error uploading document, make sure the document meets the valid criteria");
            }
        }

        private void InitializeLoanDisburseForm()
        {
            try
            {
                navigationFrame.SelectedPage = navPageLoanDisbursement;
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.Loan loan = agent.operation.GetLoan(currentLoanId);
                    lblLoanAmount.Text = "SCR" + loan.Amount;
                    lblLoanBusiness.Text = "";
                    lblLoanName.Text = "";
                    lblLoanNumber.Text = loan.LoanNumber;
                    currentId = loan.FK_LoanRequestId;

                    if (loan.AmountDisbursed == 0)
                    {
                        ribbonPageManageDisburse.Visible = true;
                        cmaEditDisbursement.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        if (Globals.hasAccess("raiseDisbursement"))
                            cmdNewDisbursement.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        ribbonPageApproveDisburse.Visible = false;
                        ribbonDisReqActions.Visible = false;
                    }
                    else if (loan.Amount > loan.AmountDisbursed)
                    {
                        ribbonPageManageDisburse.Visible = true;
                        if (Globals.hasAccess("raiseDisbursement"))
                            cmaEditDisbursement.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        if (Globals.hasAccess("raiseDisbursement"))
                            cmdNewDisbursement.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        ribbonPageApproveDisburse.Visible = false;
                        ribbonDisReqActions.Visible = false;
                    }
                    else
                    {
                        cmdNewDisbursement.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        if (Globals.hasAccess("raiseDisbursement"))
                            cmaEditDisbursement.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        ribbonPageManageDisburse.Visible = true;
                        ribbonPageApproveDisburse.Visible = false;
                        ribbonDisReqActions.Visible = false;
                    }

                    sbfa.LoanRequest loanRequest = agent.operation.GetLoanRequest(loan.FK_LoanRequestId);
                    lblLoanBusiness.Text = loanRequest.BusinessName;
                    lblLoanName.Text = loanRequest.FirstNames + " " + loanRequest.LastName;

                    dwnOrder.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    dwnPledge.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    dwnSalary.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    dwnSLA.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                    uploadButtonsTopPosition = 82;
                    grpDisbursementDocuments.Controls.OfType<SimpleButton>().ToList().ForEach(btn => btn.Dispose());
                    grpDisbursementDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());

                    string[] security = loanRequest.TypeOfSecurity.Split(';');
                    foreach (string x in security)
                    {
                        if (x.ToLower() == "pledge")
                        {
                            dwnPledge.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            dwnSLA.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            DisburseButton(loanRequest, agent.operation.CheckDisbursementLetter("Pledge-Change Document", loanRequest.Id), "Pledge-Change Document");
                            DisburseButton(loanRequest, agent.operation.CheckDisbursementLetter("SLA Letter", loanRequest.Id), "SLA Letter");
                        }

                    }


                    dwnOrder.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    DisburseButton(loanRequest, agent.operation.CheckDisbursementLetter("Standing Order Instructions", loanRequest.Id), "Standing Order Instructions");

                    dwnSalary.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    DisburseButton(loanRequest, agent.operation.CheckDisbursementLetter("Salary Assigment Form", loanRequest.Id), "Salary Assigment Form");


                    DisburseButton(loanRequest, agent.operation.CheckDisbursementLetter("Loan Agreement Form", loanRequest.Id), "Loan Agreement Form");
                    // DisburseButton(loanRequest, agent.operation.CheckDisbursementLetter("Disrbursement Form", loanRequest.Id), "Disrbursement Form");

                    sbfa.DisbursementRequest[] disb = agent.operation.GetLoanDisbursementRequests(loan.FK_LoanRequestId);
                    gridLoanDisbursement.DataSource = disb;
                    gridLoanDisbursement.RefreshDataSource();

                    if (loan.AmountDisbursed == 0 && disb != null && disb.Length > 0)
                    {
                        if (Globals.hasAccess("raiseDisbursement"))
                            cmaEditDisbursement.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("Failed to properly initialize disbursement form");
            }

        }

        private void DownloadLoanForm(string docType)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {

                    sbfa.DocumentTemplateLibrary doc = agent.operation.GetDocumentTemplate(docType);
                    string filePath = Application.StartupPath + "\\filer\\" + doc.DocumentName;
                    FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                    fs.Write(doc.DocumentData, 0, doc.DocumentData.Length);
                    fs.Flush();
                    fs.Close();
                    try
                    {
                        System.Diagnostics.Process newProcess = new System.Diagnostics.Process();
                        newProcess.StartInfo = new System.Diagnostics.ProcessStartInfo(filePath);
                        newProcess.Start();
                        newProcess.WaitForExit();
                    }
                    catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
                    catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("Please install a PDF Reader on your computer and try again.");
                    }
                    try
                    {
                        System.IO.File.Delete(filePath);
                    }
                    catch
                    {

                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There has been an error downloading disbursement form");
            }
        }

        private void xDownloadLoanForm(string docType)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {

                    Byte[] doc = agent.operation.GetAutoDocument(docType, currentLoanId);
                    string filePath = Application.StartupPath + "\\filer\\" + "loan" + currentLoanId.ToString() + ".pdf";
                    FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                    fs.Write(doc, 0, doc.Length);
                    fs.Flush();
                    fs.Close();
                    try
                    {
                        System.Diagnostics.Process newProcess = new System.Diagnostics.Process();
                        newProcess.StartInfo = new System.Diagnostics.ProcessStartInfo(filePath);
                        newProcess.Start();
                        newProcess.WaitForExit();
                    }
                    catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
                    catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("Please install a PDF Reader on your computer and try again.");
                    }
                    try
                    {
                        System.IO.File.Delete(filePath);
                    }
                    catch
                    {

                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void InitializeDisbursementRequestForm()
        {
            try
            {
                navigationFrame.SelectedPage = navPageDisbursementRequest;
                disburseRibbon.Visible = true;
                mainRibbon.SelectedPage = disRibbonPage;
                ribbonDisReqActions.Visible = true;

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.Loan loan = agent.operation.GetLoan(currentLoanId);
                    dlblLoanAmount.Text = "SCR" + loan.Amount;
                    dlblLoanBusiness.Text = "";
                    dlblLoanName.Text = "";
                    dlblLoanNumber.Text = loan.LoanNumber;

                    sbfa.LoanRequest loanRequest = agent.operation.GetLoanRequest(loan.FK_LoanRequestId);
                    dlblLoanBusiness.Text = loanRequest.BusinessName;
                    dlblLoanName.Text = loanRequest.FirstNames + " " + loanRequest.LastName;

                    lblDisNumber.Text = "0";

                    sbfa.LoanAssesment ass = agent.operation.GetDisbursementLoanAssesment(loan.FK_LoanRequestId);

                    gridSuppliers.DataSource = ass.Supplier;
                    gridSuppliers.RefreshDataSource();

                    float ttl = 0;
                    for (int a = 0; a < gridViewSuppliers.RowCount; a++)
                    {
                        ttl += float.Parse(gridViewSuppliers.GetRowCellValue(a, gridViewSuppliers.Columns[3]).ToString());
                    }
                    txtDisTotal.Text = ttl.ToString();
                    lblDisStage.Text = "New Request";
                    txtChequeNo.Text = "";
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There has been an error initializing disbursement request");
            }
        }

        private void InitializeDisbursementRequestForm(long Id)
        {
            try
            {
                navigationFrame.SelectedPage = navPageDisbursementRequest;
                disburseRibbon.Visible = true;
                mainRibbon.SelectedPage = disRibbonPage;
                disburseRibbon.Visible = true;
                ribbonPageApproveDisburse.Visible = true;
                ribbonDisReqActions.Visible = true;
                mainRibbon.SelectedPage = disRibbonPage;
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.Loan loan = agent.operation.GetLoan(currentLoanId);
                    dlblLoanAmount.Text = "SCR" + loan.Amount;
                    dlblLoanBusiness.Text = "";
                    dlblLoanName.Text = "";
                    dlblLoanNumber.Text = loan.LoanNumber;

                    sbfa.LoanRequest loanRequest = agent.operation.GetLoanRequest(loan.FK_LoanRequestId);
                    dlblLoanBusiness.Text = loanRequest.BusinessName;
                    dlblLoanName.Text = loanRequest.FirstNames + " " + loanRequest.LastName;

                    lblDisNumber.Text = Id.ToString();

                    sbfa.DisbursementRequest ass = agent.operation.GetDisbursementRequest(Id);
                    sbfa.LoanAssesmentSupplier[] sups = new sbfa.LoanAssesmentSupplier[ass.Supplier.Length];
                    int cnt = 0;
                    foreach (sbfa.DisbursementRequestSupplier dis in ass.Supplier)
                    {
                        sbfa.LoanAssesmentSupplier temp = new sbfa.LoanAssesmentSupplier();
                        temp.Id = dis.Id;
                        temp.Supplier = dis.Supplier;
                        temp.Price = dis.Price;
                        temp.Currency = dis.Currency;
                        sups[cnt] = temp;
                        cnt++;
                    }
                    gridSuppliers.DataSource = sups;
                    gridSuppliers.RefreshDataSource();

                    txtDisTotal.Text = ass.DisbursementAmount.ToString();
                    txtChequeNo.Text = ass.ChequeNo;

                    if (ass.ApprovalStatus == "Saved")
                    {
                        ribbonPageManageDisburse.Visible = false;
                        ribbonPageApproveDisburse.Visible = true;
                        ribbonDisReqActions.Visible = true;
                    }
                    else
                    {
                        ribbonPageManageDisburse.Visible = false;
                        ribbonPageApproveDisburse.Visible = false;
                        ribbonDisReqActions.Visible = false;
                    }

                    lblDisStage.Text = ass.ApprovalStatus;
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There has been an error initializing disbursement request");
            }
        }

        private void InitializeSignedDisbursementRequestForm(long Id)
        {
            try
            {
                navigationFrame.SelectedPage = navPageDisbursementRequest;
                accountsRibbon.Visible = true;
                vouRibbonPage.Visible = false;
                accPayRibbonPage.Visible = false;
                accDisRibbonPage.Visible = true;
                vouEditRibbonPage.Visible = true;
                mainRibbon.SelectedPage = accDisRibbonPage;

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.DisbursementRequest ass = agent.operation.GetDisbursementRequest(Id);
                    //check voucher
                    if (agent.operation.CheckVoucher(ass.Id))
                    {
                        if (Globals.hasAccess("raiseVoucher"))
                            cmdOpenVoucher.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        cmdRaiseVoucher.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }
                    else
                    {
                        cmdOpenVoucher.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        if (Globals.hasAccess("raiseVoucher"))
                            cmdRaiseVoucher.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    sbfa.Loan loan = agent.operation.GetLoanByRequestId(ass.FK_LoanRequestId);
                    currentLoanId = loan.Id;
                    dlblLoanAmount.Text = "SCR" + loan.Amount;
                    dlblLoanBusiness.Text = "";
                    dlblLoanName.Text = "";
                    dlblLoanNumber.Text = loan.LoanNumber;

                    sbfa.LoanRequest loanRequest = agent.operation.GetLoanRequest(loan.FK_LoanRequestId);
                    dlblLoanBusiness.Text = loanRequest.BusinessName;
                    dlblLoanName.Text = loanRequest.FirstNames + " " + loanRequest.LastName;

                    lblDisNumber.Text = Id.ToString();

                    sbfa.LoanAssesmentSupplier[] sups = new sbfa.LoanAssesmentSupplier[ass.Supplier.Length];
                    int cnt = 0;
                    foreach (sbfa.DisbursementRequestSupplier dis in ass.Supplier)
                    {
                        sbfa.LoanAssesmentSupplier temp = new sbfa.LoanAssesmentSupplier();
                        temp.Id = dis.Id;
                        temp.Supplier = dis.Supplier;
                        temp.Price = dis.Price;
                        temp.Currency = dis.Currency;
                        sups[cnt] = temp;
                        cnt++;
                    }
                    gridSuppliers.DataSource = sups;
                    gridSuppliers.RefreshDataSource();

                    txtDisTotal.Text = ass.DisbursementAmount.ToString();
                    txtChequeNo.Text = ass.ChequeNo;
                    lblDisStage.Text = ass.ApprovalStatus;
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There has been an error initializing disbursement form");
            }
        }

        private void InitializeNewPaymentVoucher()
        {
            try
            {
                navigationFrame.SelectedPage = navPagePaymentVoucher;

                accountsRibbon.Visible = true;
                accPayRibbonPage.Visible = false;
                accDisRibbonPage.Visible = true;
                mainRibbon.SelectedPage = accDisRibbonPage;
                vouNotifyRibbonPage.Visible = false;
                vouEditRibbonPage.Visible = false;
                vouRibbonPage.Visible = true;
                vouDisRibbonPage.Visible = false;

                lblVoucherNo.Text = "0";
                lblVoucherDate.Text = DateTime.Now.ToShortDateString();

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.DisbursementRequest ass = agent.operation.GetDisbursementRequest(currentLoanDisId);

                    sbfa.Loan loan = agent.operation.GetLoanByRequestId(ass.FK_LoanRequestId);
                    currentLoanId = loan.Id;
                    vlblLoanAmount.Text = "SCR" + loan.Amount;
                    vlblLoanBusiness.Text = "";
                    vlblLoanName.Text = "";
                    vlblLoanNumber.Text = loan.LoanNumber;

                    sbfa.LoanRequest loanRequest = agent.operation.GetLoanRequest(loan.FK_LoanRequestId);
                    vlblLoanBusiness.Text = loanRequest.BusinessName;
                    vlblLoanName.Text = loanRequest.FirstNames + " " + loanRequest.LastName;

                    vlblDisNumber.Text = currentLoanDisId.ToString();
                    vlblAmount.Text = "SCR" + ass.DisbursementAmount.ToString();

                    vouDocRibbonPage.Visible = false;
                    cmdPrintVoucher.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    cmdSaveVoucher.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There has been an error initializing voucher request");
            }
        }

        private void InitializePaymentVoucher()
        {
            try
            {
                navigationFrame.SelectedPage = navPagePaymentVoucher;
                accountsRibbon.Visible = true;
                accPayRibbonPage.Visible = false;
                accDisRibbonPage.Visible = true;
                mainRibbon.SelectedPage = accDisRibbonPage;

                vouEditRibbonPage.Visible = false;
                vouRibbonPage.Visible = true;
                vouNotifyRibbonPage.Visible = true;
                vouDisRibbonPage.Visible = true;
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.DisbursementRequest ass = agent.operation.GetDisbursementRequest(currentLoanDisId);

                    sbfa.Loan loan = agent.operation.GetLoanByRequestId(ass.FK_LoanRequestId);
                    currentLoanId = loan.Id;
                    vlblLoanAmount.Text = "SCR" + loan.Amount;
                    vlblLoanBusiness.Text = "";
                    vlblLoanName.Text = "";
                    vlblLoanNumber.Text = loan.LoanNumber;

                    sbfa.LoanRequest loanRequest = agent.operation.GetLoanRequest(loan.FK_LoanRequestId);
                    vlblLoanBusiness.Text = loanRequest.BusinessName;
                    vlblLoanName.Text = loanRequest.FirstNames + " " + loanRequest.LastName;

                    vlblDisNumber.Text = currentLoanDisId.ToString();
                    vlblAmount.Text = "SCR" + ass.DisbursementAmount.ToString();

                    vouDocRibbonPage.Visible = false;
                    cmdPrintVoucher.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    cmdSaveVoucher.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                    //get voucher
                    sbfa.PaymentVoucher voucher = agent.operation.GetPaymentVoucher(ass.Id);
                    cmbPayMethod.SelectedText = voucher.PaymentMethod;
                    txtCancellationFee.Text = voucher.CancellationFee.ToString();
                    txtRefDescription.Text = voucher.Refund.ToString();
                    lblVoucherDate.Text = voucher.VoucherDate.ToShortDateString();
                    lblVoucherNo.Text = voucher.Id.ToString();

                    if (ass.ApprovalStatus == "Disbursed")
                    {
                        vouRibbonPage.Visible = false;
                        vouDisRibbonPage.Visible = false;
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There has been an error initializing voucher request");
            }
        }

        private void InitializeRepaymentsForm(string Id)
        {
            try
            {
                navigationFrame.SelectedPage = navPageRepaymentSchedule;
                ribbonPagePayments.Visible = false;
                ribbonPageRepayment.Visible = false;

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.BusinessAccount acc = agent.operation.GetBusinessAccountByAccount(currentAccount);
                    plblLoanAmount.Text = "SCR" + acc.AccountBalance;
                    plblLoanBusiness.Text = "";
                    plblLoanName.Text = "";
                    plblLoanNumber.Text = acc.AccountNumber;

                    if (!agent.operation.CheckFullDisbursement(acc.AccountNumber))
                    {

                        ribbonPageManageRepayment.Visible = false;
                    }
                    else
                    {
                        ribbonPageManageRepayment.Visible = true;
                    }

                    sbfa.BusinessRegistration reg = agent.operation.GetBusinessRegistrationByRegistration(acc.AccountNumber);
                    plblLoanBusiness.Text = reg.BusinessName;
                    plblLoanName.Text = reg.FirstNames + " " + reg.LastName;

                    sbfa.RepaymentSchedule[] disb = agent.operation.GetRepaymentSchedules(currentAccount);
                    gridRepayments.DataSource = disb;
                    gridRepayments.RefreshDataSource();

                    txtAmount.Text = acc.AccountBalance.ToString();
                    txtFee.Text = acc.Penalty.ToString();
                    txtProc.Text = acc.ProcessingFee.ToString();
                    txtCancel.Text = acc.CancellationFee.ToString();
                    txtTotal.Text = (float.Parse(txtAmount.Text) + float.Parse(txtFee.Text) + float.Parse(txtCancel.Text) + float.Parse(txtProc.Text)).ToString();


                    float val = agent.operation.CalculateMonthlyRepaymentByBalance(int.Parse(txtPayback.Text), currentAccount, float.Parse(txtAmount.Text));
                    txtMonthly.Text = val.ToString();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There has been an error initializing repayments list");
            }
        }

        private void InitializeRecoverySiteVisits()
        {
            try
            {
                cmdRecSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                cmdRecSiteReport.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                cmdNewSite.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.BusinessRegistration reg = agent.operation.GetBusinessRegistrationByRegistration(currentRecoveryAccount);

                    rlblSiteNamer.Text = reg.FirstNames + " " + reg.LastName + " (" + reg.NIN + ")";
                    rlblSiteBusinessr.Text = reg.BusinessName;
                    rlblSiteTimer.Text = DateTime.Now.ToShortDateString();

                    sbfa.SiteVisit[] response = agent.operation.GetSiteVisits(reg.Id);

                    gridRecoveryVisit.DataSource = response;
                    gridRecoveryVisit.RefreshDataSource();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There has been an error initializing recovery site visit");
            }
        }

        private void InitializeNewRecoverySiteVisit()
        {
            try
            {
                cmdSiteSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                cmdSiteScheduleSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                Globals.SetPickList(cmbSVLO, "LoansOfficer");
                Globals.SetPickList(cmbSVLM, "LoansManager");
                this.uploadSiteButtonsTopPosition = 8;
                grpSelectStakeholders.Controls.OfType<CheckBox>().ToList().ForEach(btn => btn.Dispose());

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.BusinessRegistration reg = agent.operation.GetBusinessRegistrationByRegistration(currentRecoveryAccount);
                    sbfa.Stakeholder[] current = agent.operation.GetBusinessTypeStakeholders(reg.FK_BusinessRegistrationTypeId);
                    foreach (sbfa.Stakeholder stake in current)
                    {
                        StakeholderSelect(stake);
                    }
                    //check for site visit id
                    //add site
                    long siteId = agent.operation.CreateSiteVisit(reg.Id);
                    lblSiteId.Text = siteId.ToString();
                    sbfa.SiteVisit site = agent.operation.GetSiteVisit(siteId);

                    lblSiteId.Text = site.Id.ToString();

                    txtSVPhone.Text = reg.Mobile;
                    rlblSiteName.Text = reg.FirstNames + " " + reg.LastName + " (" + reg.NIN + ")";
                    rlblSiteBusiness.Text = reg.BusinessName;
                    rlblSiteTime.Text = DateTime.Now.ToShortDateString();

                    txtSVAddress.Text = site.VisitAddress;
                    dtpSVDate.DateTime = site.VisitDate;
                    chkSVConfirmed.Checked = site.Confirmed;
                    chkSVPhone.Checked = site.Phone;
                    chkSVSMS.Checked = site.SMS;
                    chkSVEmail.Checked = site.Email;
                    Globals.SetPickListValue(cmbSVLO, site.FK_LO);
                    Globals.SetPickListValue(cmbSVLM, site.FK_LM);

                    dtpSVDate.DateTime = DateTime.Now.AddDays(30);

                    if (site.OfficerComment == "" || site.ManagerComment == "")
                    {
                        approveSiteRibbon.Visible = true;
                        confirmSiteRibbon.Visible = false;

                        if (site.OfficerComment != "")
                        {
                            svOfficerApprove = true;
                            lblOfficerComment.Text = site.OfficerComment;
                            chkOfficerApprove.Checked = site.OfficerApproved;
                            //check manager access
                            bool mgr = false;
                            foreach (string v in Globals.userGroupRoles)
                            {
                                if (v == "LoansManager")
                                    mgr = true;
                            }
                            if (!mgr)
                            {
                                approveSiteRibbon.Visible = false;
                            }
                        }
                        else
                        {
                            svOfficerApprove = false;
                        }
                    }
                    else
                    {
                        approveSiteRibbon.Visible = false;
                        confirmSiteRibbon.Visible = true;
                    }

                    if (site.Confirmed)
                    {
                        cmdConfirmSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        ribbonPageNotifySiteVisit.Visible = true;
                        cmdNotifySiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        cmdRescheduleSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmdConfirmSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        ribbonPageNotifySiteVisit.Visible = false;
                        cmdNotifySiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdRescheduleSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    //get reports
                    sbfa.SiteVisitReport[] reports = agent.operation.GetSiteVisitReports(long.Parse(lblSiteId.Text));
                    foreach (sbfa.SiteVisitReport rep in reports)
                    {
                        (grpSelectStakeholders.Controls["stake" + "_" + rep.FK_StakeholderId.ToString()] as CheckBox).Checked = true;
                    }

                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There has been an error initializing recovery site visit");
            }
        }

        private void InitializeRecoverySiteVisit()
        {
            try
            {
                cmdSiteSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                cmdSiteScheduleSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                Globals.SetPickList(cmbSVLO, "LoansOfficer");
                Globals.SetPickList(cmbSVLM, "LoansManager");
                this.uploadSiteButtonsTopPosition = 8;
                grpSelectStakeholders.Controls.OfType<CheckBox>().ToList().ForEach(btn => btn.Dispose());

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.BusinessRegistration reg = agent.operation.GetBusinessRegistrationByRegistration(currentRecoveryAccount);
                    sbfa.Stakeholder[] current = agent.operation.GetBusinessTypeStakeholders(reg.FK_BusinessRegistrationTypeId);
                    foreach (sbfa.Stakeholder stake in current)
                    {
                        StakeholderSelect(stake);
                    }
                    //check for site visit id
                    sbfa.SiteVisit site = agent.operation.GetSiteVisit(currentSiteId);

                    lblSiteId.Text = site.Id.ToString();

                    txtSVPhone.Text = reg.Mobile;
                    rlblSiteName.Text = reg.FirstNames + " " + reg.LastName + " (" + reg.NIN + ")";
                    rlblSiteBusiness.Text = reg.BusinessName;
                    rlblSiteTime.Text = DateTime.Now.ToShortDateString();

                    txtSVAddress.Text = site.VisitAddress;
                    dtpSVDate.DateTime = site.VisitDate;
                    chkSVConfirmed.Checked = site.Confirmed;
                    chkSVPhone.Checked = site.Phone;
                    chkSVSMS.Checked = site.SMS;
                    chkSVEmail.Checked = site.Email;
                    Globals.SetPickListValue(cmbSVLO, site.FK_LO);
                    Globals.SetPickListValue(cmbSVLM, site.FK_LM);

                    if (site.OfficerComment == "" || site.ManagerComment == "")
                    {
                        approveSiteRibbon.Visible = true;
                        confirmSiteRibbon.Visible = false;

                        if (site.OfficerComment != "")
                        {
                            svOfficerApprove = true;
                            lblOfficerComment.Text = site.OfficerComment;
                            chkOfficerApprove.Checked = site.OfficerApproved;
                            //check manager access
                            bool mgr = false;
                            foreach (string v in Globals.userGroupRoles)
                            {
                                if (v == "LoansManager")
                                    mgr = true;
                            }
                            if (!mgr)
                            {
                                approveSiteRibbon.Visible = false;
                            }
                        }
                        else
                        {
                            svOfficerApprove = false;
                        }
                    }
                    else
                    {
                        approveSiteRibbon.Visible = false;
                        confirmSiteRibbon.Visible = true;
                    }

                    if (site.Confirmed)
                    {
                        cmdConfirmSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        ribbonPageNotifySiteVisit.Visible = true;
                        cmdNotifySiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        cmdRescheduleSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmdConfirmSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        ribbonPageNotifySiteVisit.Visible = false;
                        cmdNotifySiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdRescheduleSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    //get reports
                    sbfa.SiteVisitReport[] reports = agent.operation.GetSiteVisitReports(long.Parse(lblSiteId.Text));
                    foreach (sbfa.SiteVisitReport rep in reports)
                    {
                        (grpSelectStakeholders.Controls["stake" + "_" + rep.FK_StakeholderId.ToString()] as CheckBox).Checked = true;
                    }

                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There has been an error initializing recovery site visit");
            }
        }

        private void InitializeRecoverySiteVisitReport()
        {
            try
            {
                cmdSiteSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                cmdSiteScheduleSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                approveSiteRibbon.Visible = false;
                confirmSiteRibbon.Visible = false;
                ribbonPageNotifySiteVisit.Visible = false;
                Globals.SetPickList(cmbSVRLO, "LoansOfficer");
                Globals.SetPickList(cmbSVRLM, "LoansManager");

                uploadSiteButtonsTopPosition = 82;
                grpSiteDocuments.Controls.OfType<SimpleButton>().ToList().ForEach(btn => btn.Dispose());
                grpSiteDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.BusinessRegistration reg = agent.operation.GetBusinessRegistrationByRegistration(currentRecoveryAccount);
                    //check for site visit id
                    sbfa.SiteVisit site = agent.operation.GetSiteVisit(currentSiteId);

                    lblSiteId.Text = site.Id.ToString();

                    Globals.SetPickListValue(cmbSVRLO, site.FK_LO);
                    Globals.SetPickListValue(cmbSVRLM, site.FK_LM);
                    txtVisitBackground.Text = site.Background;
                    txtVisitConclusion.Text = site.Conclusion;
                    txtVisitDescription.Text = site.Description;
                    txtVisitPurpose.Text = site.Purpose;
                    txtVisitRecommendation.Text = site.Recommendation;

                    lblSiteName.Text = reg.FirstNames + " " + reg.LastName;
                    lblSiteBusiness.Text = reg.BusinessName;
                    lblSiteTime.Text = DateTime.Now.ToShortDateString();

                    sbfa.SiteVisitReport[] documents = agent.operation.GetSiteVisitReports(long.Parse(lblSiteId.Text));
                    foreach (sbfa.SiteVisitReport doc in documents)
                    {
                        DocumentButton(doc);
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There has been an error initializing recovery site visit report");
            }
        }

        private void InitializeOldRecoverySiteVisits()
        {
            try
            {
                cmdRecSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                cmdRecSiteReport.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                cmdNewSite.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.BusinessRegistration reg = agent.operation.GetOldBusinessRegistrationByRegistration(currentRecoveryAccount, currentOldLoanNumber);

                    rlblSiteNamer.Text = reg.FirstNames + " " + reg.LastName + " (" + reg.NIN + ")";
                    rlblSiteBusinessr.Text = reg.BusinessName;
                    rlblSiteTimer.Text = DateTime.Now.ToShortDateString();

                    sbfa.SiteVisit[] response = agent.operation.GetSiteVisits(reg.Id);

                    gridRecoveryVisit.DataSource = response;
                    gridRecoveryVisit.RefreshDataSource();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There has been an error initializing recovery site visit");
            }
        }

        private void InitializeOldNewRecoverySiteVisit()
        {
            try
            {
                cmdSiteSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                cmdSiteScheduleSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                Globals.SetPickList(cmbSVLO, "LoansOfficer");
                Globals.SetPickList(cmbSVLM, "LoansManager");
                this.uploadSiteButtonsTopPosition = 8;
                grpSelectStakeholders.Controls.OfType<CheckBox>().ToList().ForEach(btn => btn.Dispose());

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.BusinessRegistration reg = agent.operation.GetOldBusinessRegistrationByRegistration(currentRecoveryAccount, currentOldLoanNumber);
                    sbfa.Stakeholder[] current = agent.operation.GetBusinessTypeStakeholders(reg.FK_BusinessRegistrationTypeId);
                    foreach (sbfa.Stakeholder stake in current)
                    {
                        StakeholderSelect(stake);
                    }
                    //check for site visit id
                    //add site
                    long siteId = agent.operation.CreateSiteVisit(reg.Id);
                    lblSiteId.Text = siteId.ToString();
                    sbfa.SiteVisit site = agent.operation.GetSiteVisit(currentSiteId);

                    lblSiteId.Text = site.Id.ToString();

                    txtSVPhone.Text = reg.Mobile;
                    rlblSiteName.Text = reg.FirstNames + " " + reg.LastName + " (" + reg.NIN + ")";
                    rlblSiteBusiness.Text = reg.BusinessName;
                    rlblSiteTime.Text = DateTime.Now.ToShortDateString();

                    txtSVAddress.Text = site.VisitAddress;
                    dtpSVDate.DateTime = site.VisitDate;
                    chkSVConfirmed.Checked = site.Confirmed;
                    chkSVPhone.Checked = site.Phone;
                    chkSVSMS.Checked = site.SMS;
                    chkSVEmail.Checked = site.Email;
                    Globals.SetPickListValue(cmbSVLO, site.FK_LO);
                    Globals.SetPickListValue(cmbSVLM, site.FK_LM);

                    dtpSVDate.DateTime = DateTime.Now.AddDays(30);

                    if (site.OfficerComment == "" || site.ManagerComment == "")
                    {
                        approveSiteRibbon.Visible = true;
                        confirmSiteRibbon.Visible = false;

                        if (site.OfficerComment != "")
                        {
                            svOfficerApprove = true;
                            lblOfficerComment.Text = site.OfficerComment;
                            chkOfficerApprove.Checked = site.OfficerApproved;
                            //check manager access
                            bool mgr = false;
                            foreach (string v in Globals.userGroupRoles)
                            {
                                if (v == "LoansManager")
                                    mgr = true;
                            }
                            if (!mgr)
                            {
                                approveSiteRibbon.Visible = false;
                            }
                        }
                        else
                        {
                            svOfficerApprove = false;
                        }
                    }
                    else
                    {
                        approveSiteRibbon.Visible = false;
                        confirmSiteRibbon.Visible = true;
                    }

                    if (site.Confirmed)
                    {
                        cmdConfirmSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        ribbonPageNotifySiteVisit.Visible = true;
                        cmdNotifySiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        cmdRescheduleSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmdConfirmSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        ribbonPageNotifySiteVisit.Visible = false;
                        cmdNotifySiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdRescheduleSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    //get reports
                    sbfa.SiteVisitReport[] reports = agent.operation.GetSiteVisitReports(long.Parse(lblSiteId.Text));
                    foreach (sbfa.SiteVisitReport rep in reports)
                    {
                        (grpSelectStakeholders.Controls["stake" + "_" + rep.FK_StakeholderId.ToString()] as CheckBox).Checked = true;
                    }

                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There has been an error initializing recovery site visit");
            }
        }

        private void InitializeOldRecoverySiteVisit()
        {
            try
            {
                cmdSiteSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                cmdSiteScheduleSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                Globals.SetPickList(cmbSVLO, "LoansOfficer");
                Globals.SetPickList(cmbSVLM, "LoansManager");
                this.uploadSiteButtonsTopPosition = 8;
                grpSelectStakeholders.Controls.OfType<CheckBox>().ToList().ForEach(btn => btn.Dispose());

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.BusinessRegistration reg = agent.operation.GetOldBusinessRegistrationByRegistration(currentRecoveryAccount, currentOldLoanNumber);
                    sbfa.Stakeholder[] current = agent.operation.GetBusinessTypeStakeholders(reg.FK_BusinessRegistrationTypeId);
                    foreach (sbfa.Stakeholder stake in current)
                    {
                        StakeholderSelect(stake);
                    }
                    //check for site visit id
                    sbfa.SiteVisit site = agent.operation.GetSiteVisit(currentSiteId);

                    lblSiteId.Text = site.Id.ToString();

                    txtSVPhone.Text = reg.Mobile;
                    rlblSiteName.Text = reg.FirstNames + " " + reg.LastName + " (" + reg.NIN + ")";
                    rlblSiteBusiness.Text = reg.BusinessName;
                    rlblSiteTime.Text = DateTime.Now.ToShortDateString();

                    txtSVAddress.Text = site.VisitAddress;
                    dtpSVDate.DateTime = site.VisitDate;
                    chkSVConfirmed.Checked = site.Confirmed;
                    chkSVPhone.Checked = site.Phone;
                    chkSVSMS.Checked = site.SMS;
                    chkSVEmail.Checked = site.Email;
                    Globals.SetPickListValue(cmbSVLO, site.FK_LO);
                    Globals.SetPickListValue(cmbSVLM, site.FK_LM);

                    if (site.OfficerComment == "" || site.ManagerComment == "")
                    {
                        approveSiteRibbon.Visible = true;
                        confirmSiteRibbon.Visible = false;

                        if (site.OfficerComment != "")
                        {
                            svOfficerApprove = true;
                            lblOfficerComment.Text = site.OfficerComment;
                            chkOfficerApprove.Checked = site.OfficerApproved;
                            //check manager access
                            bool mgr = false;
                            foreach (string v in Globals.userGroupRoles)
                            {
                                if (v == "LoansManager")
                                    mgr = true;
                            }
                            if (!mgr)
                            {
                                approveSiteRibbon.Visible = false;
                            }
                        }
                        else
                        {
                            svOfficerApprove = false;
                        }
                    }
                    else
                    {
                        approveSiteRibbon.Visible = false;
                        confirmSiteRibbon.Visible = true;
                    }

                    if (site.Confirmed)
                    {
                        cmdConfirmSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        ribbonPageNotifySiteVisit.Visible = true;
                        cmdNotifySiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        cmdRescheduleSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                    else
                    {
                        cmdConfirmSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        ribbonPageNotifySiteVisit.Visible = false;
                        cmdNotifySiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdRescheduleSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }

                    //get reports
                    sbfa.SiteVisitReport[] reports = agent.operation.GetSiteVisitReports(long.Parse(lblSiteId.Text));
                    foreach (sbfa.SiteVisitReport rep in reports)
                    {
                        (grpSelectStakeholders.Controls["stake" + "_" + rep.FK_StakeholderId.ToString()] as CheckBox).Checked = true;
                    }

                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There has been an error initializing recovery site visit");
            }
        }

        private void InitializeOldRecoverySiteVisitReport()
        {
            try
            {
                cmdSiteSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                cmdSiteScheduleSave.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                approveSiteRibbon.Visible = false;
                confirmSiteRibbon.Visible = false;
                ribbonPageNotifySiteVisit.Visible = false;
                Globals.SetPickList(cmbSVRLO, "LoansOfficer");
                Globals.SetPickList(cmbSVRLM, "LoansManager");

                uploadSiteButtonsTopPosition = 82;
                grpSiteDocuments.Controls.OfType<SimpleButton>().ToList().ForEach(btn => btn.Dispose());
                grpSiteDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.BusinessRegistration reg = agent.operation.GetOldBusinessRegistrationByRegistration(currentRecoveryAccount, currentOldLoanNumber);
                    //check for site visit id
                    sbfa.SiteVisit site = agent.operation.GetSiteVisit(currentSiteId);

                    lblSiteId.Text = site.Id.ToString();

                    Globals.SetPickListValue(cmbSVRLO, site.FK_LO);
                    Globals.SetPickListValue(cmbSVRLM, site.FK_LM);
                    txtVisitBackground.Text = site.Background;
                    txtVisitConclusion.Text = site.Conclusion;
                    txtVisitDescription.Text = site.Description;
                    txtVisitPurpose.Text = site.Purpose;
                    txtVisitRecommendation.Text = site.Recommendation;

                    lblSiteName.Text = reg.FirstNames + " " + reg.LastName;
                    lblSiteBusiness.Text = reg.BusinessName;
                    lblSiteTime.Text = DateTime.Now.ToShortDateString();

                    sbfa.SiteVisitReport[] documents = agent.operation.GetSiteVisitReports(long.Parse(lblSiteId.Text));
                    foreach (sbfa.SiteVisitReport doc in documents)
                    {
                        DocumentButton(doc);
                    }
                }

            }
            catch (TimeoutException tx)
            {
                ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator");
            }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There has been an error initializing recovery site visit report");
            }
        }
        #endregion

        public SBFAMain()
        {
            InitializeComponent();
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        void navBarControl_ActiveGroupChanged(object sender, DevExpress.XtraNavBar.NavBarGroupEventArgs e)
        {
            navigationFrame.SelectedPageIndex = navBarControl.Groups.IndexOf(e.Group);
        }

        void barButtonNavigation_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                InitializeNewLoanForm();
            }
            catch
            {
                ShowErrorMessage("There has been an issue initializing loan form");
            }
        }

        private void ribbonControl_Click(object sender, EventArgs e)
        {

        }

        private void navigationPage1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void comboBoxEdit6_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void labelControl9_Click(object sender, EventArgs e)
        {

        }

        private void labelControl12_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {

        }

        private void labelControl13_Click(object sender, EventArgs e)
        {


        }

        private void SEnPAMain_Load(object sender, EventArgs e)
        {
            txtNoOfEmployees.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtNoOfYearAtCurrent.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtNoOfYearsAtPrevious.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtTotalCostOfProject.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtApplicantsMonthlyIncome.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtApplicantsOtherIncome.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtBusinessProjectsMonthly.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtTotalPersonalExpenditure.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtBusinessExpenditureRent.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtBusinessExpenditureLoan.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtBusinessExpenditureSalaries.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtBusinessExpenditureUtilityBills.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtBusinessExpenditureOther.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtPersonalExpenitureTotal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtPersonalIncomeTotal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtBusinessExpenditureTotal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtBusinessIncomeTotal.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtLoanBalance.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtLoanAmountRequested.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtDonorNoOfDependents.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtDonorYearsOfEmployment.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtDonorMonthlyIncome.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            txtDonorMonthlyExpenditure.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;

            siteRibbon.Visible = false;
            loanRibbon.Visible = false;
            accountsRibbon.Visible = false;
            disburseRibbon.Visible = false;
            docQuickActions.Visible = false;
            lonQuickActions.Visible = false;
            payQuickActions.Visible = false;
            disQuickOptions.Visible = false;
            sigQuickActions.Visible = false;
            oldRibbon.Visible = false;

            btnSearchRegistrations.EditValue = "";
            txtFindInvoices.EditValue = "";
            txtRegBusFind.EditValue = "";
            txtBarEditUser.EditValue = "";
            txtFindEmail.EditValue = "";
            txtFindSMS.EditValue = "";
            txtFindLoanDisburse.EditValue = "";
            txtDisbursementRequestSearch.EditValue = "";
            txtFindLoanAccount.EditValue = "";
            txtRepaymentFind.EditValue = "";
            txtOldRecovery.EditValue = "";
            txtOldRepayments.EditValue = "";
            txtOldRequests.EditValue = "";
            txtOldLoanAccounts.EditValue = "";

            navStack = new List<NavigationPage>();

            SBFAApi agent = new SBFAApi();
            using (new OperationContextScope(agent.context))
            {
                //get user profile
                sbfa.ApplicationUsers tempUser = agent.operation.GetUser(Globals.userLogged);
                Globals.organisation = tempUser.FK_StakeholderId;

                barUserDetail.Caption = tempUser.FirstName + " " + tempUser.Surname;
                lblDashboardUsername.Text = tempUser.FirstName + " " + tempUser.Surname;
                barUserAccess.Caption = "(" + tempUser.FK_RoleGroup + ")";
                barUserOrganisation.Caption = ((tempUser.FK_StakeholderId == 0) ? "SBFA" : agent.operation.GetEntityName(tempUser.FK_StakeholderId, "stahol"));

                //user group
                sbfa.ReferenceTable usrGrp = agent.operation.GetReferenceTableItemByName(tempUser.FK_RoleGroup, "rolgro");
                lblUserGr.Text = usrGrp.Description;

                Globals.SetPickList(cmbPayMethod, "paymet");
            }
            InitializeLibrary();
            InitializeWorkflowAdmin();
            InitializeBusinessTypes();
            InitializeDocumentDesignAdmin();
            InitializeQuickStats();
            InitializeNotifications();

            cmbTypeOfAccount.SelectedIndex = 0;
            cmbSecurity.SelectedIndex = 0;

            SideBarRights();
            TopBarRights();
        }

        private void cmbBusType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnWorkFlow_Click(object sender, EventArgs e)
        {


        }

        private void btnManageSiteVisit_Click(object sender, EventArgs e)
        {

        }

        private void btnSearchRegistrations_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.LoanRequest[] response = agent.operation.GetLoanRequests(btnSearchRegistrations.EditValue.ToString());

                    gridViewLoans.DataSource = response;
                    gridViewLoans.RefreshDataSource();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void lstRegistrations_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                // currentId = long.Parse(lstRegistrations.SelectedItems[0].SubItems[0].Text);
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {

            }
        }

        private void btnSearchRegistrations_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void gridViewRegistrations_DoubleClick(object sender, EventArgs e)
        {

        }

        private void gridRegistrations_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                currentId = long.Parse(gridLoans.GetRowCellValue(gridLoans.FocusedRowHandle, gridLoans.Columns[0]).ToString());

                InitializeLoanForm(currentId);
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void navBarItemViewRegistrations_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            navigationFrame.SelectedPage = navPageViewLoans;
        }

        private void newRegistration_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                InitializePrequalifications();
            }
            catch
            {
                ShowErrorMessage("There seems to be a prblem initializing the PreQualification form");
            }
        }

        private void gridInvoices_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                currentInvoiceId = long.Parse(gridInvoices.GetRowCellValue(gridInvoices.FocusedRowHandle, gridInvoices.Columns[0]).ToString());

                new ProcessInvoice().ShowDialog();
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void btnAddStake_Click(object sender, EventArgs e)
        {

        }

        private void navBarItemRegisteredBusiness_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            navigationFrame.SelectedPage = navPageLoanDisbursed;
        }

        private void txtRegBusFind_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SBFAApi agent = new SBFAApi();
            using (new OperationContextScope(agent.context))
            {

            }
        }

        private void gridRegisteredBusness_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                currentBusinessId = long.Parse(gridRegisteredBusness.GetRowCellValue(gridRegisteredBusness.FocusedRowHandle, gridRegisteredBusness.Columns[0]).ToString());

                // InitializeBusinessForm(currentBusinessId);                
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void btnRecoAddStake_Click(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    if (treeRecoStake.SelectedNode.Text.ToLower() != "stakeholders")
                    {
                        int currentStake = int.Parse(treeRecoStake.SelectedNode.Name.Split('_')[1]);
                        long done = agent.operation.SaveRecommendedAction(long.Parse(rlblRecoId.Text), currentStake, 0, "", false, "", "", true);
                        if (done > 0)
                        {
                            string[] row = { currentStake.ToString(), agent.operation.GetEntityName(currentStake, "stahol"), agent.operation.GetEntityName(1, "act"), "", "" };
                            var listViewItem = new ListViewItem(row);
                            lstRecoStake.Items.Add(listViewItem);
                            treeRecoStake.SelectedNode.Remove();

                        }
                        else
                        {
                            ;
                        }
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void lstRecoStake_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.RecommendedAction act = agent.operation.GetRecommendedAction(long.Parse(rlblRecoId.Text), int.Parse(lstRecoStake.SelectedItems[0].SubItems[0].Text));
                    Globals.SetPickListValue(cmbStatusReco, act.Status);
                    // Globals.SetPickListValue(cmbBDOReco, act.FK_BusinessDevelopmentOfficer);
                    Globals.SetPickListValue(cmbAction, act.FK_ActionId);

                    txtDetails.Text = act.Details;
                    // txtComments.Text = act.Comments;
                    //dtpDueDateReco.EditValue = act.DeadlineDate;
                    txtReasonReco.Text = act.StatusReason;
                    //chkActionedReco.Checked = act.Actioned;
                    chkActiveReco.Checked = act.Active;
                    chkReminderReco.Checked = act.Reminder;
                    lblsthId.Text = act.FK_StakeholderId.ToString();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void btnSaveReco_Click(object sender, EventArgs e)
        {

        }

        private void btnRecommendations_Click(object sender, EventArgs e)
        {

        }

        private void txtNIN_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtNIN.Text == "")
                    return;

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    //check from local db first for details
                    sbfa.BusinessRegistration registration = agent.operation.GetBusinessRegistration(txtNIN.Text);

                    if (registration == null || registration.FirstNames == null)
                    {
                        sbfa.Resident response = agent.operation.GetResident(txtNIN.Text);

                        txtFirstName.Text = response.FirstName;
                        txtLastName.Text = response.Surname;

                        txtCitizenship.Text = response.Nationality;
                        // Type { get => type; set => type = value; }
                        // Status { get => status; set => status = value; }
                        dtpDOB.DateTime = response.DateOfBirth;
                        DateTime now = DateTime.Today;
                        int age = now.Year - response.DateOfBirth.Year;
                        if (response.DateOfBirth > now.AddYears(-age)) age--;

                        txtAge.Text = age.ToString();
                        if (response.FirstName == "")
                            txtNIN.Text = "";
                    }
                    else
                    {
                        txtBusinessName.Text = registration.BusinessRegistrationNumber;
                        txtBusinessRegNumber.Text = registration.BusinessName;
                        Globals.SetPickListValue(cmbBusType, registration.FK_BusinessTypeId);

                        Globals.SetPickListValue(cmbBusRegType, registration.FK_BusinessRegistrationTypeId);
                        Globals.SetPickListValue(cmbBusIsland, registration.FK_BusinessIslandLocationId);
                        Globals.SetPickList(cmbBusDistrict, "dis", registration.FK_BusinessIslandLocationId);
                        Globals.SetPickListValue(cmbBusDistrict, registration.FK_BusinessIslandDistrictId);
                        //registration.NIN = txtNIN.Text;
                        txtFirstName.Text = registration.FirstNames;
                        txtLastName.Text = registration.LastName;
                        cmbSalutation.SelectedValue = registration.Salutation;
                        cmbGender.SelectedValue = registration.Gender;
                        dtpDOB.DateTime = registration.DOB;
                        Globals.SetPickListValue(cmbResIsland, registration.FK_ResidenceIslandLocationId);
                        Globals.SetPickList(cmbResDistrict, "dis", registration.FK_ResidenceIslandLocationId);
                        Globals.SetPickListValue(cmbResDistrict, registration.FK_ResidenceDistrictLocationId);
                        txtMobile.Text = registration.Mobile;
                        txtHomeTel.Text = registration.HomeTelephone;
                        txtWorkTel.Text = registration.WorkTelephone;
                        txtEmail.Text = registration.Email;

                        txtSEnPAReg.Text = registration.SEnPARegistrationNo;
                        DateTime now = DateTime.Today;
                        int age = now.Year - registration.DOB.Year;
                        if (registration.DOB > now.AddYears(-age)) age--;

                        txtAge.Text = age.ToString();
                        registration.NoOfEmployees = int.Parse(txtNoOfEmployees.Text);
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void txtBusinessRegNumber_Leave(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.Business response = agent.operation.GetBusiness(txtBusinessRegNumber.Text);

                    txtBusinessName.Text = response.Name;
                    if (response.Name == "")
                        txtBusinessRegNumber.Text = "";
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void btnBusinessView_Click(object sender, EventArgs e)
        {
            try
            {
                currentBusinessId = long.Parse(gridRegisteredBusness.GetRowCellValue(gridRegisteredBusness.FocusedRowHandle, gridRegisteredBusness.Columns[0]).ToString());

                //InitializeBusinessForm(currentBusinessId);
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void txtSearchFiles_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                pnlExplorer.Controls.Clear();
                Control control = (Control)sender;
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    TreeNode temp = treeFolders.SelectedNode;
                    if (FindRootNode(temp).Name == "documentLibrary_1")
                    {
                        sbfa.DocumentLibrary[] documents = agent.operation.SearchDocuments(currentFolderId, txtSearchFiles.Text);
                        foreach (sbfa.DocumentLibrary doc in documents)
                        {
                            pnlExplorer.Controls.Add(fileControl(doc.DocumentName, doc.DocumentContentType, doc.Id));
                        }
                    }
                    else if (FindRootNode(temp).Name == "documentLibrary_3")
                    {
                        long currentTypeId = long.Parse(treeFolders.SelectedNode.Name.Split('_')[1]);
                        sbfa.DocumentLibrary[] documents = agent.operation.SearchRegistrationDocuments(currentTypeId, txtSearchFiles.Text);
                        foreach (sbfa.DocumentLibrary doc in documents)
                        {
                            pnlExplorer.Controls.Add(fileControl(doc.DocumentName, doc.DocumentContentType, doc.Id));
                        }
                    }

                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentFolderId < 4)
                    return;
                pnlExplorer.Controls.Clear();
                Control control = (Control)sender;
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    currentFolderId = agent.operation.GetParentFolderId(currentFolderId);
                    lblFolderMap.Text = agent.operation.GetFolderPath(currentFolderId).Replace(",", " > ");

                    sbfa.DocumentFolders[] response = agent.operation.GetFolders(currentFolderId);
                    for (int x = 0; x < response.Length; x++)
                    {
                        pnlExplorer.Controls.Add(folderControl(response[x].FolderName, response[x].Id));
                    }
                    //get files
                    sbfa.DocumentLibrary[] documents = agent.operation.GetFolderDocuments(currentFolderId);
                    foreach (sbfa.DocumentLibrary doc in documents)
                    {
                        pnlExplorer.Controls.Add(fileControl(doc.DocumentName, doc.DocumentContentType, doc.Id));
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void btnAddFolder_Click(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    long response = agent.operation.CreateFolder(txtAddFolder.EditValue.ToString(), currentFolderId);
                    if (response > 0)
                    {
                        if (currentFolderId == 1)
                        {
                            treeFolders.Nodes["documentLibrary_1"].Nodes.Add("folder_" + response, txtAddFolder.EditValue.ToString());
                        }
                        pnlExplorer.Controls.Add(folderControl(txtAddFolder.EditValue.ToString(), response));
                    }
                    else
                    {
                        ShowErrorMessage("Error creating folder");
                    }

                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void txtFile_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            uploadDocuments.ShowDialog();
            txtFile.Text = uploadDocuments.FileName;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFile.Text == "")
                    return;
                byte[] buffer = File.ReadAllBytes(txtFile.Text);
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    bool done = agent.operation.UploadDocument(0, Path.GetFileName(txtFile.Text), buffer, Globals.GetComboBoxValue(cmbDocumentType), currentFolderId);
                    if (done)
                    {
                        txtFile.Text = "";
                        ShowSuccessMessage("Document saved");
                    }
                    else
                    {
                        ShowErrorMessage("Error uploading your document");
                    }

                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void treeFolders_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                pnlExplorer.Controls.Clear();

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    TreeNode temp = treeFolders.SelectedNode;
                    if (FindRootNode(temp).Name == "documentLibrary_3")
                    {
                        long currentTypeId = long.Parse(treeFolders.SelectedNode.Name.Split('_')[1]);
                        lblFolderMap.Text = "Loans/" + treeFolders.SelectedNode.Text;
                        //get files
                        sbfa.DocumentLibrary[] documents = agent.operation.GetRegistrationsFolderDocuments(currentTypeId);
                        foreach (sbfa.DocumentLibrary doc in documents)
                        {
                            pnlExplorer.Controls.Add(fileControl(doc.DocumentName, doc.DocumentContentType, doc.Id));
                        }
                    }
                    else if (FindRootNode(temp).Name == "documentLibrary_1")
                    {
                        currentFolderId = long.Parse(treeFolders.SelectedNode.Name.Split('_')[1]);
                        lblFolderMap.Text = agent.operation.GetFolderPath(currentFolderId).Replace(",", " / ");
                        //get folders
                        sbfa.DocumentFolders[] response = agent.operation.GetFolders(currentFolderId);
                        for (int x = 0; x < response.Length; x++)
                        {
                            pnlExplorer.Controls.Add(folderControl(response[x].FolderName, response[x].Id));
                        }
                        //get files
                        sbfa.DocumentLibrary[] documents = agent.operation.GetFolderDocuments(currentFolderId);
                        foreach (sbfa.DocumentLibrary doc in documents)
                        {
                            pnlExplorer.Controls.Add(fileControl(doc.DocumentName, doc.DocumentContentType, doc.Id));
                        }
                    }
                    else if (FindRootNode(temp).Name == "documentLibrary_4")
                    {
                        lblFolderMap.Text = "Templates/" + treeFolders.SelectedNode.Text;
                        sbfa.DocumentTemplateLibrary[] response = agent.operation.GetDocumentTemplates();
                        foreach (sbfa.DocumentTemplateLibrary doc in response)
                        {
                            pnlExplorer.Controls.Add(fileTemplateControl(doc.DocumentName, doc.DocumentContentType, doc.DocumentType));
                        }
                    }
                    else
                    {
                        lblFolderMap.Text = "Help/" + treeFolders.SelectedNode.Text;
                    }

                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void barButtonManageWorkflow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = navPageWorkFlows;
        }

        private void btnWrkUp_Click(object sender, EventArgs e)
        {
            try
            {
                Globals.MoveItems(lstStages, Globals.MoveDirection.Up);

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    if (long.Parse(lstStages.SelectedItems[0].SubItems[1].Text) > 0)
                    {
                        int x = lstStages.SelectedIndices[0];

                        bool done = agent.operation.SwitchStages(long.Parse(lstStages.SelectedItems[0].SubItems[0].Text), long.Parse(lstStages.Items[x + 1].SubItems[0].Text));

                        lstStages.Items.Clear();
                        sbfa.WorkFlowStages[] response = agent.operation.GetWorkFlowStages(long.Parse(treeWorkFlow.SelectedNode.Name.Split('_')[1]));
                        foreach (sbfa.WorkFlowStages wrkFlow in response)
                        {
                            string[] row = { wrkFlow.Id.ToString(), wrkFlow.StagePosition.ToString(), wrkFlow.StageName, wrkFlow.StageDescription, agent.operation.GetEntityName(wrkFlow.FK_RoleGroupId, "rolgro"), ((wrkFlow.StageAssignMode == 1) ? "Yes" : "No"), ((wrkFlow.StageOptional) ? "Yes" : "No"), ((wrkFlow.RequireDocuments) ? "Yes" : "No"), ((wrkFlow.RequirePayment) ? "Yes" : "No"), ((wrkFlow.RequireSiteVisit) ? "Yes" : "No"), ((wrkFlow.RequireRecommendations) ? "Yes" : "No") };
                            var listViewItem = new ListViewItem(row);
                            lstStages.Items.Add(listViewItem);
                        }

                        lstStages.Items[x].Selected = true;
                        lstStages.HideSelection = false;
                        lstStages.Focus();
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void btnWrkDwn_Click(object sender, EventArgs e)
        {
            try
            {
                Globals.MoveItems(lstStages, Globals.MoveDirection.Down);
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {

                    int x = lstStages.SelectedIndices[0];

                    bool done = agent.operation.SwitchStages(long.Parse(lstStages.Items[x - 1].SubItems[0].Text), long.Parse(lstStages.SelectedItems[0].SubItems[0].Text));

                    lstStages.Items.Clear();
                    sbfa.WorkFlowStages[] response = agent.operation.GetWorkFlowStages(long.Parse(treeWorkFlow.SelectedNode.Name.Split('_')[1]));
                    foreach (sbfa.WorkFlowStages wrkFlow in response)
                    {
                        string[] row = { wrkFlow.Id.ToString(), wrkFlow.StagePosition.ToString(), wrkFlow.StageName, wrkFlow.StageDescription, agent.operation.GetEntityName(wrkFlow.FK_RoleGroupId, "rolgro"), ((wrkFlow.StageAssignMode == 1) ? "Yes" : "No"), ((wrkFlow.StageOptional) ? "Yes" : "No"), ((wrkFlow.RequireDocuments) ? "Yes" : "No"), ((wrkFlow.RequirePayment) ? "Yes" : "No"), ((wrkFlow.RequireSiteVisit) ? "Yes" : "No"), ((wrkFlow.RequireRecommendations) ? "Yes" : "No") };
                        var listViewItem = new ListViewItem(row);
                        lstStages.Items.Add(listViewItem);
                    }

                    lstStages.Items[x].Selected = true;
                    lstStages.HideSelection = false;
                    lstStages.Focus();

                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void btnSaveWrk_Click(object sender, EventArgs e)
        {

        }

        private void treeWorkFlow_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    if (treeWorkFlow.SelectedNode.Text.ToLower() != "work flows")
                    {
                        currentWorkFlow = long.Parse(treeWorkFlow.SelectedNode.Name.Split('_')[1]);
                        sbfa.WorkFlows wrk = agent.operation.GetWorkFlow(long.Parse(treeWorkFlow.SelectedNode.Name.Split('_')[1]));
                        lblWrkId.Text = wrk.Id.ToString();
                        txtName.Text = wrk.WorkFlowName;
                        txtDescription.Text = wrk.WorkFlowDescription;
                        Globals.SetPickListValue(cmbStart, wrk.FK_StartRoleGroupId);
                        Globals.SetPickListValue(cmbEnd, wrk.FK_EndRoleGroupId);

                        lstStages.Items.Clear();
                        sbfa.WorkFlowStages[] response = agent.operation.GetWorkFlowStages(long.Parse(treeWorkFlow.SelectedNode.Name.Split('_')[1]));
                        foreach (sbfa.WorkFlowStages wrkFlow in response)
                        {
                            string[] row = { wrkFlow.Id.ToString(), wrkFlow.StagePosition.ToString(), wrkFlow.StageName, wrkFlow.StageDescription, agent.operation.GetEntityName(wrkFlow.FK_RoleGroupId, "rolgro"), ((wrkFlow.StageAssignMode == 1) ? "Yes" : "No"), ((wrkFlow.StageOptional) ? "Yes" : "No"), ((wrkFlow.RequireDocuments) ? "Yes" : "No"), ((wrkFlow.RequirePayment) ? "Yes" : "No"), ((wrkFlow.RequireSiteVisit) ? "Yes" : "No"), ((wrkFlow.RequireRecommendations) ? "Yes" : "No") };
                            var listViewItem = new ListViewItem(row);
                            lstStages.Items.Add(listViewItem);
                        }

                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void btnStages_Click(object sender, EventArgs e)
        {

        }

        private void treeBusinessType_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                treeStakeHolder.Nodes["stakeHolder"].Nodes.Clear();
                treeSavedToBusiness.Nodes["existingStakeholders"].Nodes.Clear();
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    if (treeBusinessType.SelectedNode.Text.ToLower() != "business type")
                    {
                        currentBusiness = int.Parse(treeBusinessType.SelectedNode.Name.Split('_')[1]);
                        sbfa.Stakeholder[] response = agent.operation.GetStakeholders();
                        foreach (sbfa.Stakeholder stake in response)
                        {
                            string currentStake = "_" + stake.Id.ToString();
                            treeStakeHolder.Nodes["stakeHolder"].Nodes.Add(currentStake, stake.Name);
                        }

                        sbfa.Stakeholder[] current = agent.operation.GetBusinessTypeStakeholders(currentBusiness);
                        foreach (sbfa.Stakeholder stake in current)
                        {
                            string currentStake = "_" + stake.Id.ToString();
                            treeSavedToBusiness.Nodes["existingStakeholders"].Nodes.Add(currentStake, stake.Name);
                            //remove from system roles
                            treeStakeHolder.Nodes["stakeHolder"].Nodes[currentStake].Remove();
                        }
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void btnAddStakeToBus_Click(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    if (treeStakeHolder.SelectedNode.Text.ToLower() != "stakeholders")
                    {
                        TreeNode temp = treeStakeHolder.SelectedNode;
                        int currentStake = int.Parse(treeStakeHolder.SelectedNode.Name.Split('_')[1]);
                        bool done = agent.operation.SaveBusinessTypeStakeholder(currentBusiness, currentStake, true);
                        if (done)
                        {
                            treeStakeHolder.SelectedNode.Remove();
                            treeSavedToBusiness.Nodes["existingStakeholders"].Nodes.Add(temp);
                        }
                        else
                        {
                            ;
                        }
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void btnRemoveStake_Click(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    if (treeSavedToBusiness.SelectedNode.Text.ToLower() != "stakeholders")
                    {
                        TreeNode temp = treeSavedToBusiness.SelectedNode;
                        int currentStake = int.Parse(treeSavedToBusiness.SelectedNode.Name.Split('_')[1]);
                        bool done = agent.operation.RemoveBusinessTypeStakeholder(currentBusiness, currentStake);
                        if (done)
                        {
                            treeSavedToBusiness.SelectedNode.Remove();
                            treeStakeHolder.Nodes["stakeholder"].Nodes.Add(temp);
                        }
                        else
                        {
                            ;
                        }
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void barButtonItemBusinessTypes_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = navPageBusinessType;
        }

        private void barButtonItemManageStaeholders_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new ManageStakeholder().ShowDialog();
        }

        private void txtBarEditUser_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.ApplicationUsers[] response = agent.operation.GetUsers(txtBarEditUser.EditValue.ToString());
                    gridUsers.DataSource = response;
                    gridUsers.RefreshDataSource();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void barButtonItemEdit_Click(object sender, EventArgs e)
        {
            try
            {
                currentUsername = (gridViewUsers.GetRowCellValue(gridViewUsers.FocusedRowHandle, gridViewUsers.Columns[0]).ToString());
                new ManagerUserProperties().ShowDialog();
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }

        }

        private void btnAddNewUser_Click(object sender, EventArgs e)
        {
            new AddUser().ShowDialog();
        }

        private void barManageUsers_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = navPageUsers;
        }

        private void barAddUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new AddUser().ShowDialog();
        }

        private void barManageUserGroups_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new ManageUserGroupProperties().ShowDialog();
        }

        private void radioCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                object value = radioCategories.EditValue;
                category = value.ToString();
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    if (category == "dis")
                    {
                        cmbParent.DataSource = null;
                        Globals.SetPickList(cmbParent, "isl");
                        lblParent.Text = "Island";
                    }
                    else
                    {
                        cmbParent.DataSource = null;
                        Globals.SetPickList(cmbParent, "refParent");
                        lblParent.Text = "Parent";
                    }

                    sbfa.ReferenceTable[] response = agent.operation.GetReferenceTableItems(category);
                    gridReferences.DataSource = response;
                    gridReferences.RefreshDataSource();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void gridViewReferences_Click(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {

                    sbfa.ReferenceTable response = agent.operation.GetReferenceTableItem(long.Parse(gridViewReferences.GetRowCellValue(gridViewReferences.FocusedRowHandle, gridViewReferences.Columns[0]).ToString()), category);

                    chkActive.Checked = response.Active;
                    txtRefName.Text = response.Name;
                    txtRefDescription.Text = response.Description;
                    Globals.SetPickListValue(cmbParent, response.FK_ParentId);

                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void btnRefSave_Click(object sender, EventArgs e)
        {
            if (txtRefName.Text == "")
            {
                return;
            }

            if (txtRefDescription.Text == "")
            {
                txtRefDescription.Text = txtRefName.Text;
            }

            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    bool wrk = agent.operation.SaveReferenceTable(txtRefName.Text, txtRefDescription.Text, chkActive.Checked, category, Globals.GetComboBoxValue(cmbParent));
                    if (wrk)
                    {
                        sbfa.ReferenceTable[] response = agent.operation.GetReferenceTableItems(category);
                        gridReferences.DataSource = response;
                        gridReferences.RefreshDataSource();

                        txtRefName.Text = "";
                        txtRefDescription.Text = "";

                        txtRefName.Focus();
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void barManageReferences_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = navPageReferences;
        }

        private void txtFindEmail_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.Email[] response = agent.operation.GetEmails(txtFindEmail.EditValue.ToString());
                    gridEmails.DataSource = response;
                    gridEmails.RefreshDataSource();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void barViewEmails_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            navigationFrame.SelectedPage = navPageEmail;
        }

        private void barViewSMSs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = navPageSMS;
        }

        private void loanApplication_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            try
            {
                InitializeNewLoanForm();
            }
            catch
            {
                ShowErrorMessage("There was an issue initializing new loan form");
            }
        }

        private void cmbBusIsland_EditValueChanged_1(object sender, EventArgs e)
        {
            try
            {
                Globals.SetPickList(cmbBusDistrict, "dis", Globals.GetComboBoxValue(cmbBusIsland));
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmbResIsland_EditValueChanged_1(object sender, EventArgs e)
        {
            try
            {
                Globals.SetPickList(cmbResDistrict, "dis", Globals.GetComboBoxValue(cmbResIsland));
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private string SecurityFromCheckList(CheckedListBox chk)
        {
            string security = string.Empty;

            for (int i = 0; i < chk.Items.Count; i++)
            {
                if (chk.GetItemChecked(i))
                {
                    string str = (string)chk.Items[i];
                    if (security.Equals(string.Empty))
                    {
                        security = str;
                    }
                    else
                    {
                        security = security + ";" + str;
                    }
                }
            }

            return security;
        }

        private void btnOpenLoan_Click(object sender, EventArgs e)
        {
            try
            {
                currentId = long.Parse(gridLoans.GetRowCellValue(gridLoans.FocusedRowHandle, gridLoans.Columns[0]).ToString());
                InitializeLoanForm(currentId);
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }

        }

        private void btnUpdateSiteVisit_Click(object sender, EventArgs e)
        {

        }

        private void btnApproveLoan_Click(object sender, EventArgs e)
        {

        }

        private void btnWrkFlowApprove_Click(object sender, EventArgs e)
        {

        }

        private void labelControl50_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Login login = new Login();
            login.Show();

            this.Hide();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoBackNavigation();
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (txtRecoComment.Text == "")
                {
                    ShowErrorMessage("Please put your comments to proceed");
                    txtRecoComment.Focus();
                    return;
                }
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    #region access
                    sbfa.DocumentWorkflow wrkFlow = agent.operation.UpdateWorkFlowStage(long.Parse(lblId.Text), "loaapp");

                    agent.operation.SaveRecommendation(currentId, currentApprovalStage, true, txtRecoComment.Text);

                    sbfa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(long.Parse(lblId.Text), "loaapp");
                    lblApproveStage.Text = currentStage.StageName;
                    currentApprovalStage = currentStage.StageName;

                    if (!agent.operation.CheckAccessToStage(long.Parse(lblId.Text), "loaapp") || currentStage.StageName == "Complete")
                    {
                        loanAssRibbon.Visible = false;
                    }
                    else
                    {
                        loanAssRibbon.Visible = true;
                        cmdApproveLoan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        cmdDeclineLoan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }

                    txtRecoComment.Text = "";

                    if (currentStage.StageName == "Complete")
                    {
                        //create loan disbursement;
                        bool done = agent.operation.RegisterLoan(long.Parse(lblId.Text), "loan");
                        sbfa.Loan loan = agent.operation.GetLoanByRequestId(long.Parse(lblId.Text));
                        currentLoanId = loan.Id;
                        InitializeLoanDisburseForm();
                    }
                    else
                    {
                        InitializeLoanForm(currentId);
                    }
                    #endregion
                    //RefreshRecommendationIndicator();

                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void txtPreNIN_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbStart_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtFindSMS_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.SMS[] response = agent.operation.GetSMSs(txtFindSMS.EditValue.ToString());
                    gridSMS.DataSource = response;
                    gridSMS.RefreshDataSource();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdAssess_Click(object sender, EventArgs e)
        {
            //ShowDisqualification();
        }

        private void btnRenew_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            navigationFrame.SelectedPage = navPageViewLoans;
        }

        private void txtFindInvoices_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.Invoice[] response = agent.operation.GetInvoices(txtFindInvoices.EditValue.ToString());
                    gridViewInvoices.DataSource = response;
                    gridViewInvoices.RefreshDataSource();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void SEnPAMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void officeNavigationBar_Click(object sender, EventArgs e)
        {

        }

        #region Prequalifications

        private void InitializePrequalifications()
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.ReferenceTable[] busType = agent.operation.GetReferenceTableItems("bustyp");
                    foreach (sbfa.ReferenceTable bus in busType)
                    {
                        string currentBus = "_" + bus.Id.ToString();
                        cmbPreBusinessType.Items.Add(bus.Name);

                    }

                    sbfa.ReferenceTable[] secType = agent.operation.GetReferenceTableItems("sec");
                    foreach (sbfa.ReferenceTable sec in secType)
                    {
                        string currentSec = "_" + sec.Id.ToString();
                        chkSecurity.Items.Add(sec.Name);

                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
            navigationFrame.SelectedPage = navPagePrequalification;
        }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ReceiveRepayment processInvoice = new ReceiveRepayment();
            processInvoice.Show();
        }

        private void barButtonItem21_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = navPagePayments;
        }

        private void ribbonStatusBar_Click(object sender, EventArgs e)
        {

        }

        private void txtSVBIN_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void txtPreNIN_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (txtPreNIN.Text == "")
                    return;

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.Resident response = agent.operation.GetResident(txtPreNIN.Text);

                    txtPreNames.Text = response.FirstName;
                    txtPreSurname.Text = response.Surname;

                    preQualNINValid = true;

                    //txtCitizenship.Text = response.Nationality;
                    // Type { get => type; set => type = value; }
                    // Status { get => status; set => status = value; }
                    ///dtpDOB.DateTime = response.DateOfBirth;
                    if (response.FirstName == "")
                    {
                        txtPreNIN.Text = "";
                        ShowNotFound("person");
                        preQualNINValid = false;
                    }

                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (svNINValid == false && svBINValid == false)
            {
                ShowValidationError("Please either a valid NIN or a BIN");
                return;
            }

            if (txtSVPhone.Text.Equals(""))
            {
                ShowValidationError("Please enter a phone number for the person to be visited.");
                return;
            }

            if (txtSVAddress.Text.Equals(""))
            {
                ShowValidationError("Please enter the location of the visit.");
                return;
            }
        }

        private void barButtonItem31_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem32_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem33_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                InitializeNewPaymentVoucher();
            }
            catch
            {
                ShowErrorMessage("There was an issue initializing new voucher");
            }
        }

        private void txtPreBIN_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.Business response = agent.operation.GetBusiness(txtPreBIN.Text);

                    txtPreBusinessName.Text = response.Name;

                    if (response.Name == "")
                    {
                        txtPreBIN.Text = "";
                        ShowNotFound("business");
                    }

                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdSaveWorkflow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    long wrk = agent.operation.CreateWorkFlow(txtName.Text, txtDescription.Text, Globals.GetComboBoxValue(cmbStart), Globals.GetComboBoxValue(cmbEnd));
                    if (wrk > 0)
                    {
                        string currentFlow = "_" + wrk.ToString();
                        treeWorkFlow.Nodes["workFlows"].Nodes.Add(currentFlow, txtName.Text);
                        lstStages.Items.Clear();
                        currentWorkFlow = wrk;
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdManageStages_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new ManageStages().ShowDialog();
        }

        private void cmdSaveReferences_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    bool wrk = agent.operation.SaveReferenceTable(txtRefName.Text, txtRefDescription.Text, chkActive.Checked, category, Globals.GetComboBoxValue(cmbParent));
                    if (wrk)
                    {
                        sbfa.ReferenceTable[] response = agent.operation.GetReferenceTableItems(category);
                        gridReferences.DataSource = response;
                        gridReferences.RefreshDataSource();
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdEditUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentUsername = (gridViewUsers.GetRowCellValue(gridViewUsers.FocusedRowHandle, gridViewUsers.Columns[0]).ToString());
                new ManagerUserProperties().ShowDialog();
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }

        }

        private void cmdDocumentDesign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = navPageDesignDocuments;
        }

        private void cmdRegistrationRules_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new ValidationRules().Show(this);
        }

        private void cmdChargeRules_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new ChargeRules().Show(this);
        }

        private void cmdSaveDocDesign_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    bool save = agent.operation.SaveAutoDocumentsDesign(currentDocumentDesign, txtDocDesignSMS.Text, txtDocDesignBody.Text, txtDocDesignSubject.Text, chkDocSMS.Checked, chkDocEmail.Checked);
                    if (save)
                    {
                        System.IO.File.WriteAllText(Application.StartupPath + "\\html\\design.html", txtDocDesignBody.Text);
                        webViewer.Url = new Uri(Application.StartupPath + "\\html\\design.html");
                        webViewer.Refresh();
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmbNavBackSite_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoBackNavigation();
        }

        private void cmdSiteSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.SiteVisit site = new sbfa.SiteVisit();
                    site.Id = long.Parse(lblSiteId.Text);

                    site.FK_LO = Globals.GetComboBoxValue(cmbSVRLO);
                    site.FK_LM = Globals.GetComboBoxValue(cmbSVRLM);

                    site.Background = txtVisitBackground.Text;
                    site.Conclusion = txtVisitConclusion.Text;
                    site.Description = txtVisitDescription.Text;
                    site.Recommendation = txtVisitRecommendation.Text;
                    site.Purpose = txtVisitPurpose.Text;

                    bool response = agent.operation.SaveSiteVisit(site);
                    if (response)
                    {
                        ShowSuccessMessage("Record updated");
                    }
                    else
                    {
                        //MessageBox.Show("Error Saving");
                        ShowErrorMessage("An unknown error occured while saving");

                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdSiteScheduleSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.SiteVisit site = new sbfa.SiteVisit();
                    site.Id = long.Parse(lblSiteId.Text);
                    site.FK_LO = Globals.GetComboBoxValue(cmbSVLO);
                    site.FK_LM = Globals.GetComboBoxValue(cmbSVLM);
                    site.VisitDate = dtpSVDate.DateTime;
                    site.VisitAddress = txtSVAddress.Text;
                    site.Confirmed = chkSVConfirmed.Checked;
                    site.Phone = chkSVPhone.Checked;
                    site.SMS = chkSVSMS.Checked;
                    site.Email = chkSVEmail.Checked;


                    bool response = agent.operation.ScheduleSiteVisit(site);
                    if (response)
                    {
                        ShowSuccessMessage("Record updated");
                        if (site.Confirmed)
                        {
                            ribbonPageNotifySiteVisit.Visible = true;
                            cmdNotifySiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        }
                        else
                        {
                            ribbonPageNotifySiteVisit.Visible = false;
                            cmdNotifySiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        }
                    }
                    else
                    {
                        //MessageBox.Show("Error Saving");
                        ShowErrorMessage("An unknown error occured while saving");

                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdNotifySiteVisit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {

                    bool response = agent.operation.NotifySiteVisit(long.Parse(lblSiteId.Text));
                    if (response)
                    {
                        ShowSuccessMessage("Notification sent successfully to all Stakeholders");
                    }
                    else
                    {
                        //MessageBox.Show("Error Saving");
                        ShowErrorMessage("An unknown error occured while saving");

                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmbNavBackReco_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoBackNavigation();
        }

        private void cmbRecoSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    //if status changed to complete check for stakeholder report first
                    string status = cmbStatusReco.SelectedValue.ToString();
                    if (status == "Complete")
                    {
                        string response = agent.operation.CheckCurrentStageRecommandationSiteVisitRequirements(long.Parse(lblId.Text), "loan", long.Parse(rlblRecoId.Text), int.Parse(lblsthId.Text));
                        if (response.ToLower() == "none")
                        {
                            ;//continue
                        }
                        else
                        {
                            ShowErrorMessage(response);
                            return;
                        }
                    }

                    if (int.Parse(lblsthId.Text) == 0)
                    {
                        ShowErrorMessage("Please select a StakeHolder first");
                        return;
                    }

                    long done = agent.operation.SaveRecommendedAction(long.Parse(rlblRecoId.Text), int.Parse(lblsthId.Text), Globals.GetComboBoxValue(cmbAction), txtDetails.Text, chkReminderReco.Checked, cmbStatusReco.SelectedValue.ToString(), txtReasonReco.Text, chkActiveReco.Checked);
                    if (done > 0)
                    {
                        lstRecoStake.Items.Clear();
                        sbfa.RecommendedAction[] reports = agent.operation.GetRecommendedActions(long.Parse(rlblRecoId.Text));
                        foreach (sbfa.RecommendedAction rep in reports)
                        {
                            string[] row = { rep.FK_StakeholderId.ToString(), agent.operation.GetEntityName(rep.FK_StakeholderId, "stahol"), agent.operation.GetEntityName(rep.FK_ActionId, "act"), "", rep.Status };
                            var listViewItem = new ListViewItem(row);
                            lstRecoStake.Items.Add(listViewItem);
                        }
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void btnWrkDel_Click(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    bool delete = agent.operation.DeleteStage(currentWorkFlow, long.Parse(lstStages.SelectedItems[0].SubItems[0].Text));
                    if (delete)
                    {
                        lstStages.Items.Clear();
                        sbfa.WorkFlowStages[] response = agent.operation.GetWorkFlowStages(long.Parse(treeWorkFlow.SelectedNode.Name.Split('_')[1]));
                        foreach (sbfa.WorkFlowStages wrkFlow in response)
                        {
                            string[] row = { wrkFlow.Id.ToString(), wrkFlow.StagePosition.ToString(), wrkFlow.StageName, wrkFlow.StageDescription, agent.operation.GetEntityName(wrkFlow.FK_RoleGroupId, "rolgro"), ((wrkFlow.StageAssignMode == 1) ? "Yes" : "No"), ((wrkFlow.StageOptional) ? "Yes" : "No"), ((wrkFlow.RequireDocuments) ? "Yes" : "No"), ((wrkFlow.RequirePayment) ? "Yes" : "No"), ((wrkFlow.RequireSiteVisit) ? "Yes" : "No"), ((wrkFlow.RequireRecommendations) ? "Yes" : "No") };
                            var listViewItem = new ListViewItem(row);
                            lstStages.Items.Add(listViewItem);
                        }
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdFolderBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (currentFolderId < 4)//reserved folder ids
                    return;
                pnlExplorer.Controls.Clear();

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    currentFolderId = agent.operation.GetParentFolderId(currentFolderId);
                    lblFolderMap.Text = agent.operation.GetFolderPath(currentFolderId).Replace(",", " > ");

                    sbfa.DocumentFolders[] response = agent.operation.GetFolders(currentFolderId);
                    for (int x = 0; x < response.Length; x++)
                    {
                        pnlExplorer.Controls.Add(folderControl(response[x].FolderName, response[x].Id));
                    }
                    //get files
                    sbfa.DocumentLibrary[] documents = agent.operation.GetFolderDocuments(currentFolderId);
                    foreach (sbfa.DocumentLibrary doc in documents)
                    {
                        pnlExplorer.Controls.Add(fileControl(doc.DocumentName, doc.DocumentContentType, doc.Id));
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdOpenLoan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentId = long.Parse(gridLoans.GetRowCellValue(gridLoans.FocusedRowHandle, gridLoans.Columns[0]).ToString());

                InitializeLoanForm(currentId);
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void barButtonItem37_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentInvoiceId = long.Parse(gridInvoices.GetRowCellValue(gridInvoices.FocusedRowHandle, gridInvoices.Columns[0]).ToString());

                new ProcessInvoice().ShowDialog();
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdWorkFlow_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    //check documents
                    string response = agent.operation.CheckCurrentStageDocumentRequirements(long.Parse(lblId.Text), "loan");
                    if (response.ToLower() == "none")
                    {
                        //check recommendations
                        response = agent.operation.CheckCurrentStageRecommendationsRequirements(long.Parse(lblId.Text), "loan");
                        if (response.ToLower() == "none")
                        {
                            //check site
                            response = agent.operation.CheckCurrentStageSiteVisitRequirements(long.Parse(lblId.Text), "loan");
                            if (response.ToLower() == "none")
                            {
                                #region access
                                sbfa.DocumentWorkflow wrkFlow = agent.operation.UpdateWorkFlowStage(long.Parse(lblId.Text), "loan");
                                uploadButtonsTopPosition = 82;
                                existingDocumentsPosition = 82;
                                grpDocuments.Controls.OfType<SimpleButton>().ToList().ForEach(btn => btn.Dispose());
                                grpDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
                                grpExistingWorkflow.Controls.OfType<LinkLabel>().ToList().ForEach(btn => btn.Dispose());
                                grpExistingWorkflow.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
                                sbfa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(long.Parse(lblId.Text), "loan");
                                //lblStage.Text = currentStage.StageName;
                                if (currentStage.RequireDocuments)
                                {
                                    grpDocuments.Visible = true;
                                    sbfa.WorkFlowStageDocumentStatus[] rdocuments = agent.operation.GetDocumentsRequiredStatus(long.Parse(lblId.Text), "loan");
                                    foreach (sbfa.WorkFlowStageDocumentStatus doc in rdocuments)
                                    {
                                        DocumentButton(doc);
                                    }
                                }
                                else
                                {
                                    grpDocuments.Visible = false;
                                }

                                RefreshLoanIndicator();

                                if (currentStage.RequirePayment)
                                {
                                    cmbWorkFlowSkip.Links[0].Visible = false;
                                    cmdWorkFlow.Links[0].Visible = false;
                                    cmdWorkFlowRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                                    cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                                }
                                else
                                {
                                    cmdWorkFlowRefresh.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                                    cmdWorkFlow.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                                }

                                if (currentStage.StageOptional)
                                {
                                    cmbWorkFlowSkip.Links[0].Visible = true;
                                }
                                else
                                {
                                    cmbWorkFlowSkip.Links[0].Visible = false;
                                }

                                if (currentStage.RequireSiteVisit || currentStage.RequireRecommendations)
                                {
                                    loanSiteRibbon.Visible = true;
                                    loanSiteRibbon.Visible = ((Globals.hasAccess("siteVisit")) ? true : false);
                                }
                                else
                                {
                                    loanSiteRibbon.Visible = false;
                                }

                                if (currentStage.RequireSiteVisit)
                                {
                                    cmdNavNewVisit.Links[0].Visible = true;
                                    cmdNavVisitReport.Links[0].Visible = true;
                                }
                                else
                                {
                                    cmdNavNewVisit.Links[0].Visible = false;
                                    cmdNavVisitReport.Links[0].Visible = false;
                                }

                                if (currentStage.RequireRecommendations)
                                {
                                    cmdNavRecommendations.Links[0].Visible = true;
                                }
                                else
                                {
                                    cmdNavRecommendations.Links[0].Visible = false;
                                }


                                if (!agent.operation.CheckAccessToStage(long.Parse(lblId.Text), "loan") || currentStage.StageName == "Complete")
                                {
                                    cmdWorkFlow.Links[0].Visible = false;
                                }
                                else
                                {
                                    cmdWorkFlow.Links[0].Visible = true;
                                }
                                if (currentStage.StageName == "Complete")
                                {
                                    //create loan disbursement
                                    bool approve = agent.operation.CreateLoanApproval(currentId);
                                    if (approve)
                                    {
                                        cmdApproveLoan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                                        loanAssRibbon.Visible = true;
                                        ribbonOpenAssesment.Visible = true;
                                        cmdAssesLoan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                                        wrkFlowToolsRibbon.Visible = false;
                                        ribbonSaveLoan.Visible = true;
                                        Save.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                                        navigationFrame.SelectedPage = navPageApproveLoan;
                                        InitializeLoanApprovalForm(currentId);
                                    }

                                }
                                else
                                {
                                    loanAssRibbon.Visible = false;
                                }
                                //get existing docs
                                sbfa.AutoDocument[] autoDocs = agent.operation.CheckAutoDocument(currentId, "loan");
                                foreach (sbfa.AutoDocument auto in autoDocs)
                                {
                                    DocumentLink(auto);
                                }

                                sbfa.WorkFlowStageDocumentStatus[] documents = agent.operation.GetAllRequiredDocuments(currentId, "loan");
                                foreach (sbfa.WorkFlowStageDocumentStatus doc in documents)
                                {
                                    DocumentLink(doc, "business");
                                }


                                #endregion

                            }
                            else
                            {
                                ShowErrorMessage(response);
                            }
                        }
                        else
                        {
                            ShowErrorMessage(response);
                        }
                    }
                    else
                    {
                        ShowErrorMessage(response);
                    }

                }

                wrkFlowToolsRibbon.Visible = ((Globals.hasAccess("processLoan")) ? true : false);

                ribbonOpenAssesment.Visible = ((Globals.hasAccess("assessLoan")) ? true : false);
                loanAssRibbon.Visible = ((Globals.hasAccess("approveLoan")) ? true : false);
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void Save_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (!CheckForValue(pnlRegistration))
                    return;
                if (cmbLO.SelectedValue == null)
                {
                    //MessageBox.Show("Please assign Business Development Officer");
                    ShowValidationError("Please assign Loans Officer");
                    cmbLO.Focus();
                    return;
                }

                if (cmbSalutation.Text == null || cmbSalutation.Text == "")
                {
                    ShowValidationError("Please select the salutation/title of the applicant.");
                    cmbSalutation.Focus();
                    return;
                }

                if (cmbGender.Text == null || cmbGender.Text == "")
                {
                    ShowValidationError("Please select the gender of the applicant");
                    cmbGender.Focus();
                    return;
                }

                //rule validations
                if (SecurityFromCheckList(chkSecurity).Equals("Insurance"))
                {
                    ShowValidationError("Please select another type of security apart from Insurance.");
                    return;
                }

                //rule validations
                if ((SecurityFromCheckList(chkSecurity).IndexOf("Guarantor") > -1) && (txtDonorNIN.Text == ""))
                {
                    ShowValidationError("Please provide valid guarantor details.");
                    return;
                }

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.LoanRequest newRegistration = new sbfa.LoanRequest();
                    newRegistration.Id = long.Parse(lblId.Text);
                    if (lblReference.Text == "0")
                        newRegistration.ReferenceNumber = Utilities.GenerateReferenceNumber();
                    else
                        newRegistration.ReferenceNumber = lblReference.Text;
                    newRegistration.BusinessRegistrationNumber = txtBusinessName.Text;
                    newRegistration.BusinessName = txtBusinessRegNumber.Text;
                    newRegistration.FK_BusinessTypeId = Globals.GetComboBoxValue(cmbBusType);
                    newRegistration.FK_BusinessRegistrationTypeId = Globals.GetComboBoxValue(cmbBusRegType);
                    newRegistration.FK_BusinessIslandLocationId = Globals.GetComboBoxValue(cmbBusIsland);
                    newRegistration.FK_BusinessIslandDistrictId = Globals.GetComboBoxValue(cmbBusDistrict);
                    newRegistration.NIN = txtNIN.Text;
                    newRegistration.FirstNames = txtFirstName.Text;
                    newRegistration.LastName = txtLastName.Text;
                    newRegistration.Salutation = cmbSalutation.SelectedValue.ToString();
                    newRegistration.Gender = cmbGender.SelectedValue.ToString();
                    newRegistration.DOB = dtpDOB.DateTime;
                    newRegistration.FK_ResidenceIslandLocationId = Globals.GetComboBoxValue(cmbResIsland);
                    newRegistration.FK_ResidenceDistrictLocationId = Globals.GetComboBoxValue(cmbResDistrict);
                    newRegistration.Mobile = txtMobile.Text;
                    newRegistration.HomeTelephone = txtHomeTel.Text;
                    newRegistration.WorkTelephone = txtWorkTel.Text;
                    newRegistration.Email = txtEmail.Text;
                    newRegistration.FK_LoansOfficerId = Globals.GetComboBoxValue(cmbLO);
                    newRegistration.Status = "";
                    newRegistration.StatusReason = "";
                    newRegistration.DocumentType = "loan";
                    newRegistration.RequireWorkFlow = true;
                    newRegistration.WorkFlowId = 0;
                    newRegistration.WorkFlowStatus = "New";
                    newRegistration.Created = DateTime.Now;
                    newRegistration.CreatedBy = Globals.userLogged;
                    newRegistration.LastModified = DateTime.Now;
                    newRegistration.LastModifiedBy = Globals.userLogged;
                    newRegistration.SEnPARegistrationNo = txtSEnPAReg.Text;
                    newRegistration.Age = int.Parse(txtAge.Text);
                    newRegistration.LoanAmountRequested = long.Parse(txtLoanAmountRequested.Text);
                    newRegistration.NoOfEmployees = int.Parse(txtNoOfEmployees.Text);

                    newRegistration.Employed = Globals.GetBoolValue(cmbEmployed);
                    newRegistration.EmploymentDetails = cmbEmploymentDetails.SelectedText;
                    newRegistration.NameOfEmployer = txtNameOfEmployer.Text;
                    newRegistration.CurrentNoOfYears = Globals.GetIntValue(txtNoOfYearAtCurrent);
                    newRegistration.CurrentPosition = txtCurrentPosition.Text;
                    newRegistration.PreviousEmployer = txtPreviousEmployer.Text;
                    newRegistration.PreviousNoOfYears = Globals.GetIntValue(txtNoOfYearsAtPrevious);
                    newRegistration.PreviousPosition = txtPreviousPosition.Text;
                    newRegistration.BackgroundExperience = txtBackGroundExperience.Text;

                    newRegistration.CostOfProject = Globals.GetFloatValue(txtTotalCostOfProject);
                    newRegistration.MonthlyIncome = Globals.GetFloatValue(txtApplicantsMonthlyIncome);
                    newRegistration.OtherIncome = Globals.GetFloatValue(txtApplicantsOtherIncome);
                    newRegistration.BusinessMonthlyIncome = Globals.GetFloatValue(txtBusinessProjectsMonthly);
                    newRegistration.PersonalExpenditure = Globals.GetFloatValue(txtPersonalExpenitureTotal);
                    newRegistration.BusinessExpenditureLoan = Globals.GetFloatValue(txtBusinessExpenditureLoan);
                    newRegistration.BusinessExpenditureRent = Globals.GetFloatValue(txtBusinessExpenditureRent);
                    newRegistration.BusinessExpenditureUtilityBills = Globals.GetFloatValue(txtBusinessExpenditureUtilityBills);
                    newRegistration.BusinessExpenditureStaffSalaries = Globals.GetFloatValue(txtBusinessExpenditureSalaries);
                    newRegistration.BusinessExpenditureOther = Globals.GetFloatValue(txtBusinessExpenditureOther);
                    newRegistration.PersonalIncomeTotal = Globals.GetFloatValue(txtPersonalIncomeTotal);
                    newRegistration.PersonalExpenditureTotal = Globals.GetFloatValue(txtPersonalExpenitureTotal);
                    newRegistration.BusinessIncomeTotal = Globals.GetFloatValue(txtBusinessIncomeTotal);
                    newRegistration.BusinessExpenditureTotal = Globals.GetFloatValue(txtBusinessExpenditureTotal);
                    newRegistration.NameOfBank = txtNameOfBank.Text;
                    newRegistration.AccountNo = txtAccountNumber.Text;
                    newRegistration.TypeOfAccount = cmbTypeOfAccount.SelectedText;
                    newRegistration.DateOfLastPayment = dtLastPaymentMade.DateTime;
                    newRegistration.LoanBalance = Globals.GetFloatValue(txtLoanBalance);

                    newRegistration.LoanAmountRequested = Globals.GetFloatValue(txtLoanAmountRequested);
                    newRegistration.AnnualTurnoverRange = cmbAnnualTurnoverRange.SelectedText;
                    newRegistration.HasSecurity = true;
                    newRegistration.TypeOfSecurity = SecurityFromCheckList(chkTypeOfSecurity);
                    newRegistration.PurposeOfLoan = txtPurposeOfLoan.Text;

                    newRegistration.GuarantorNIN = txtDonorNIN.Text;
                    newRegistration.GuarantorName = txtDonorName.Text;
                    newRegistration.GuarantorSurname = txtDonorSurname.Text;
                    newRegistration.GuarantorDOB = dtDonorDOB.DateTime;
                    newRegistration.GuarantorAddress = txtDonorAddress.Text;
                    newRegistration.GuarantorContactNo = txtDonorContactNo.Text;
                    newRegistration.GuarantorMaritalStatus = cmbDonorMaritalStatus.Text;
                    newRegistration.GuarantorNoOfDependents = txtDonorNoOfDependents.Text;
                    newRegistration.GuarantorEmploymentStatus = cmbDonorEmploymentStatus.Text;
                    newRegistration.GuarantorEmployersAddress = txtDonorEmployerAddress.Text;
                    newRegistration.GuarantorEmployersName = txtDonorEmployerName.Text;
                    newRegistration.GuarantorCurrentPosition = txtDonorCurrentPosition.Text;
                    newRegistration.GuarantorNoOfYears = Globals.GetIntValue(txtDonorYearsOfEmployment);
                    newRegistration.GuarantorTotalMonthlyIncome = Globals.GetFloatValue(txtDonorMonthlyIncome);
                    newRegistration.GuarantorTotalMonthlyExpenditure = Globals.GetFloatValue(txtDonorMonthlyExpenditure);

                    //validate parameters
                    sbfa.WorkFlowFieldValidations[] vals = agent.operation.GetValidationsList("loan");
                    bool allValidated = true;
                    foreach (sbfa.WorkFlowFieldValidations aVal in vals)
                    {
                        if (!Globals.ValidateLoanField(aVal, newRegistration))
                        {
                            ShowErrorMessage(aVal.ParameterFieldName + " field is invalid");
                            allValidated = false;
                            break;
                        }
                    }

                    if (allValidated)
                    {
                        long response = agent.operation.SaveLoanRequest(newRegistration);

                        if (response > 0)
                        {
                            currentId = response;
                            lblId.Text = response.ToString();
                            lblReference.Text = newRegistration.ReferenceNumber;
                            uploadButtonsTopPosition = 82;
                            #region access
                            grpDocuments.Controls.OfType<SimpleButton>().ToList().ForEach(btn => btn.Dispose());
                            grpDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
                            sbfa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(long.Parse(lblId.Text), "loan");
                            //lblStage.Text = currentStage.StageName;
                            if (currentStage.RequireDocuments)
                            {
                                sbfa.WorkFlowStageDocumentStatus[] documents = agent.operation.GetDocumentsRequiredStatus(long.Parse(lblId.Text), "loan");
                                foreach (sbfa.WorkFlowStageDocumentStatus doc in documents)
                                {
                                    DocumentButton(doc);
                                }
                            }
                            else
                            {
                                //  grpDocuments.Visible = false;
                            }

                            RefreshLoanIndicator();

                            if (currentStage.RequirePayment)
                            {
                                cmbWorkFlowSkip.Links[0].Visible = false;
                                cmdWorkFlow.Links[0].Visible = false;
                            }

                            if (currentStage.StageOptional)
                            {
                                cmbWorkFlowSkip.Links[0].Visible = true;
                            }
                            else
                            {
                                cmbWorkFlowSkip.Links[0].Visible = false;
                            }

                            if (currentStage.RequireSiteVisit)
                            {
                                cmdNavNewVisit.Links[0].Visible = true;
                                cmdNavVisitReport.Links[0].Visible = true;
                            }
                            else
                            {
                                cmdNavNewVisit.Links[0].Visible = false;
                                cmdNavVisitReport.Links[0].Visible = false;
                            }

                            if (currentStage.RequireRecommendations)
                            {
                                cmdNavRecommendations.Links[0].Visible = true;
                            }
                            else
                            {
                                cmdNavRecommendations.Links[0].Visible = false;
                            }

                            if (!agent.operation.CheckAccessToStage(long.Parse(lblId.Text), "loan") || currentStage.StageName == "Complete")
                            {
                                cmdWorkFlow.Links[0].Visible = false;
                            }
                            else
                            {
                                cmdWorkFlow.Links[0].Visible = true;
                            }

                            if (currentStage.StageName == "Complete")
                            {
                                cmdAssesLoan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                                wrkFlowToolsRibbon.Visible = false;
                                ribbonSaveLoan.Visible = true;
                                Save.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                            }
                            else
                            {
                                loanAssRibbon.Visible = false;
                                wrkFlowToolsRibbon.Visible = true;
                                ribbonSaveLoan.Visible = true;
                                Save.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            }
                            #endregion

                        }
                        else
                        {
                            //MessageBox.Show("Error Saving");
                            ShowErrorMessage("An error occured while saving the loan application.");
                        }
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void barButtonItem44_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                navigationFrame.SelectedPage = navPageScheduleSiteVisit;
                InitializeSiteVisit();
            }
            catch
            {
                ShowErrorMessage("There was a problem initializing site visit");
            }
        }

        private void barButtonItem46_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                InitializeRecommendations();
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There was an issue initializing recommendations");
            }
        }

        private void cmbWorkFlowSkip_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {

                    #region access
                    bool skip = agent.operation.SkipOptionalStage(long.Parse(lblId.Text), "loan");
                    if (skip)
                    {
                        uploadButtonsTopPosition = 82;
                        grpDocuments.Controls.OfType<SimpleButton>().ToList().ForEach(btn => btn.Dispose());
                        grpDocuments.Controls.OfType<PictureBox>().ToList().ForEach(pic => pic.Dispose());
                        sbfa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(long.Parse(lblId.Text), "loan");
                        //lblStage.Text = currentStage.StageName;
                        RefreshLoanIndicator();
                        if (currentStage.RequireDocuments)
                        {
                            grpDocuments.Visible = true;
                            sbfa.WorkFlowStageDocumentStatus[] rdocuments = agent.operation.GetDocumentsRequiredStatus(long.Parse(lblId.Text), "loan");
                            foreach (sbfa.WorkFlowStageDocumentStatus doc in rdocuments)
                            {
                                DocumentButton(doc);
                            }
                        }
                        else
                        {
                            grpDocuments.Visible = false;
                        }

                        if (currentStage.RequirePayment)
                        {
                            cmbWorkFlowSkip.Links[0].Visible = false;
                            cmdWorkFlow.Links[0].Visible = false;
                        }

                        if (currentStage.StageOptional)
                        {
                            cmbWorkFlowSkip.Links[0].Visible = true;
                        }
                        else
                        {
                            cmbWorkFlowSkip.Links[0].Visible = false;
                        }

                        if (currentStage.RequireSiteVisit)
                        {
                            cmdNavNewVisit.Links[0].Visible = true;
                            cmdNavVisitReport.Links[0].Visible = true;
                        }
                        else
                        {
                            cmdNavNewVisit.Links[0].Visible = false;
                            cmdNavVisitReport.Links[0].Visible = false;
                        }

                        if (currentStage.RequireRecommendations)
                        {
                            cmdNavRecommendations.Links[0].Visible = true;
                        }
                        else
                        {
                            cmdNavRecommendations.Links[0].Visible = false;
                        }


                        if (!agent.operation.CheckAccessToStage(long.Parse(lblId.Text), "loan") || currentStage.StageName == "Complete")
                        {
                            cmdWorkFlow.Links[0].Visible = false;
                        }
                        else
                        {
                            cmdWorkFlow.Links[0].Visible = true;
                        }

                        if (currentStage.StageName == "Complete")
                        {
                            //create loan disbursement
                            bool approve = agent.operation.CreateLoanApproval(currentId);
                            if (approve)
                            {
                                cmdApproveLoan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;

                                loanAssRibbon.Visible = true;
                                ribbonOpenAssesment.Visible = true;
                                cmdAssesLoan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                                wrkFlowToolsRibbon.Visible = false;
                                ribbonSaveLoan.Visible = true;
                                Save.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;

                                navigationFrame.SelectedPage = navPageApproveLoan;
                                InitializeLoanApprovalForm(currentId);
                            }

                        }
                        else
                        {

                        }

                        //get existing docs
                        sbfa.AutoDocument[] autoDocs = agent.operation.CheckAutoDocument(currentId, "loan");
                        foreach (sbfa.AutoDocument auto in autoDocs)
                        {
                            DocumentLink(auto);
                        }

                        sbfa.WorkFlowStageDocumentStatus[] documents = agent.operation.GetAllRequiredDocuments(currentId, "loan");
                        foreach (sbfa.WorkFlowStageDocumentStatus doc in documents)
                        {
                            DocumentLink(doc, "business");
                        }


                        #endregion
                        //set assign list
                        //senpa.ApplicationUserSummary[] userList = agent.operation.GetAssigningUserList(long.Parse(lblId.Text), "registration");
                        //cmbAssign.DataSource = null;
                        //Globals.SetUserPickList(cmbAssign, userList);

                    }
                    else
                    {
                        ShowErrorMessage("Failed to skip optional stage");
                    }
                }

                wrkFlowToolsRibbon.Visible = ((Globals.hasAccess("processLoan")) ? true : false);
                loanSiteRibbon.Visible = ((Globals.hasAccess("siteVisit")) ? true : false);
                ribbonOpenAssesment.Visible = ((Globals.hasAccess("assessLoan")) ? true : false);
                loanAssRibbon.Visible = ((Globals.hasAccess("approveLoan")) ? true : false);
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmbBusIsland_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Globals.SetPickList(cmbBusDistrict, "dis", Globals.GetComboBoxValue(cmbBusIsland));
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmbResIsland_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Globals.SetPickList(cmbResDistrict, "dis", Globals.GetComboBoxValue(cmbResIsland));
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void btnApproveSiteCancel_Click(object sender, EventArgs e)
        {
            pnlApproveSite.Visible = false;
        }

        private void barButtonItem38_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            svApproved = true;
            pnlApproveSite.Visible = true;
            lblSiteAction.Text = "Approve & Recommend Site Visit";
        }

        private void cmdDeclineSelect_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            svApproved = false;
            pnlApproveSite.Visible = true;
            lblSiteAction.Text = "Decline & Recommend Site Visit";
        }

        private void cmdHomeDashboard_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                navigationFrame.SelectedPage = navPageHome;
                InitializeQuickStats();
                InitializeNotifications();
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There has been some problem loading quick stats");
            }
        }

        private void cmdAssesLoan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                navigationFrame.SelectedPage = navPageApproveLoan;
                InitializeLoanApprovalForm(currentId);
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There has been some problem loading quick stats");
            }
        }

        private void cmdOpenAssesment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            new LoanAssesment().ShowDialog();
        }

        private void cmdDeclineLoan_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (txtRecoComment.Text == "")
                {
                    ShowErrorMessage("Please put your comments to proceed");
                    txtRecoComment.Focus();
                    return;
                }
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    //check documents
                    string response = agent.operation.CheckCurrentStageDocumentRequirements(long.Parse(lblId.Text), "loaapp");
                    if (response.ToLower() == "none")
                    {
                        #region access
                        sbfa.DocumentWorkflow wrkFlow = agent.operation.TerminateWorkFlowStage(long.Parse(lblId.Text), "loaapp");

                        agent.operation.SaveRecommendation(currentId, currentApprovalStage, false, txtRecoComment.Text);

                        sbfa.WorkFlowStages currentStage = agent.operation.GetDocumentWorkFlowStage(long.Parse(lblId.Text), "loaapp");
                        lblApproveStage.Text = currentStage.StageName;
                        currentApprovalStage = currentStage.StageName;

                        if (currentStage.StageName == "Complete")
                        {
                            loanAssRibbon.Visible = false;
                        }
                        else
                        {
                            loanAssRibbon.Visible = true;
                            cmdApproveLoan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                            cmdDeclineLoan.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        }

                        if (currentStage.StageName == "Complete")
                        {
                            //create loan disbursement;
                            agent.operation.RegisterDeclineLoan(long.Parse(lblId.Text), "loan");
                        }
                        else
                        {

                        }
                        #endregion
                    }
                    else
                    {
                        ShowErrorMessage(response);
                    }
                    RefreshRecommendationIndicator();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void txtDonorNIN_Leave(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    //check from local db first for details
                    sbfa.Guarantor guarantor = agent.operation.GetGuarantor(txtDonorNIN.Text);

                    if (guarantor == null || guarantor.GuarantorName == null)
                    {
                        sbfa.Resident response = agent.operation.GetResident(txtDonorNIN.Text);

                        txtDonorName.Text = response.FirstName;
                        txtDonorSurname.Text = response.Surname;

                        dtDonorDOB.DateTime = response.DateOfBirth;

                        if (response.FirstName == "")
                            txtDonorNIN.Text = "";
                    }
                    else
                    {
                        //  guarantor.GuarantorNIN = txtDonorNIN.Text;
                        txtDonorName.Text = guarantor.GuarantorName;
                        txtDonorSurname.Text = guarantor.GuarantorSurname;
                        dtDonorDOB.DateTime = guarantor.GuarantorDOB;
                        txtDonorAddress.Text = guarantor.GuarantorAddress;
                        txtDonorContactNo.Text = guarantor.GuarantorContactNo;
                        cmbDonorMaritalStatus.Text = guarantor.GuarantorMaritalStatus;
                        txtDonorNoOfDependents.Text = guarantor.GuarantorNoOfDependents;
                        cmbDonorEmploymentStatus.Text = guarantor.GuarantorEmploymentStatus;
                        txtDonorEmployerAddress.Text = guarantor.GuarantorEmployersAddress;
                        txtDonorEmployerName.Text = guarantor.GuarantorEmployersName;
                        txtDonorCurrentPosition.Text = guarantor.GuarantorCurrentPosition;
                        txtDonorYearsOfEmployment.Text = guarantor.GuarantorNoOfYears.ToString();
                        txtDonorMonthlyIncome.Text = guarantor.GuarantorTotalMonthlyIncome.ToString();
                        txtDonorMonthlyExpenditure.Text = guarantor.GuarantorTotalMonthlyExpenditure.ToString();
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void navBarFindLoanApplications_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            navigationFrame.SelectedPage = navPageViewLoans;
        }

        private void txtNIN_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (txtNIN.Text == "")
                    return;

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    //check from local db first for details
                    sbfa.BusinessRegistration registration = agent.operation.GetBusinessRegistration(txtNIN.Text);

                    if (registration == null || registration.FirstNames == null)
                    {
                        sbfa.Resident response = agent.operation.GetResident(txtNIN.Text);

                        txtFirstName.Text = response.FirstName;
                        txtLastName.Text = response.Surname;

                        txtCitizenship.Text = response.Nationality;
                        // Type { get => type; set => type = value; }
                        // Status { get => status; set => status = value; }
                        dtpDOB.DateTime = response.DateOfBirth;
                        DateTime now = DateTime.Today;
                        int age = now.Year - response.DateOfBirth.Year;
                        if (response.DateOfBirth > now.AddYears(-age)) age--;

                        txtAge.Text = age.ToString();
                        if (response.FirstName == "")
                            txtNIN.Text = "";
                    }
                    else
                    {
                        txtBusinessName.Text = registration.BusinessRegistrationNumber;
                        txtBusinessRegNumber.Text = registration.BusinessName;
                        Globals.SetPickListValue(cmbBusType, registration.FK_BusinessTypeId);

                        Globals.SetPickListValue(cmbBusRegType, registration.FK_BusinessRegistrationTypeId);
                        Globals.SetPickListValue(cmbBusIsland, registration.FK_BusinessIslandLocationId);
                        Globals.SetPickList(cmbBusDistrict, "dis", registration.FK_BusinessIslandLocationId);
                        Globals.SetPickListValue(cmbBusDistrict, registration.FK_BusinessIslandDistrictId);
                        //registration.NIN = txtNIN.Text;
                        txtFirstName.Text = registration.FirstNames;
                        txtLastName.Text = registration.LastName;
                        cmbSalutation.SelectedValue = registration.Salutation;
                        cmbGender.SelectedValue = registration.Gender;
                        dtpDOB.DateTime = registration.DOB;
                        Globals.SetPickListValue(cmbResIsland, registration.FK_ResidenceIslandLocationId);
                        Globals.SetPickList(cmbResDistrict, "dis", registration.FK_ResidenceIslandLocationId);
                        Globals.SetPickListValue(cmbResDistrict, registration.FK_ResidenceDistrictLocationId);
                        txtMobile.Text = registration.Mobile;
                        txtHomeTel.Text = registration.HomeTelephone;
                        txtWorkTel.Text = registration.WorkTelephone;
                        txtEmail.Text = registration.Email;

                        txtSEnPAReg.Text = registration.SEnPARegistrationNo;
                        DateTime now = DateTime.Today;
                        int age = now.Year - registration.DOB.Year;
                        if (registration.DOB > now.AddYears(-age)) age--;

                        txtAge.Text = age.ToString();
                        registration.NoOfEmployees = int.Parse(txtNoOfEmployees.Text);
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void txtDonorNIN_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    //check from local db first for details
                    sbfa.Guarantor guarantor = agent.operation.GetGuarantor(txtDonorNIN.Text);

                    if (guarantor == null || guarantor.GuarantorName == null)
                    {
                        sbfa.Resident response = agent.operation.GetResident(txtDonorNIN.Text);

                        txtDonorName.Text = response.FirstName;
                        txtDonorSurname.Text = response.Surname;

                        dtDonorDOB.DateTime = response.DateOfBirth;

                        if (response.FirstName == "")
                            txtDonorNIN.Text = "";
                    }
                    else
                    {
                        //  guarantor.GuarantorNIN = txtDonorNIN.Text;
                        txtDonorName.Text = guarantor.GuarantorName;
                        txtDonorSurname.Text = guarantor.GuarantorSurname;
                        dtDonorDOB.DateTime = guarantor.GuarantorDOB;
                        txtDonorAddress.Text = guarantor.GuarantorAddress;
                        txtDonorContactNo.Text = guarantor.GuarantorContactNo;
                        cmbDonorMaritalStatus.Text = guarantor.GuarantorMaritalStatus;
                        txtDonorNoOfDependents.Text = guarantor.GuarantorNoOfDependents;
                        cmbDonorEmploymentStatus.Text = guarantor.GuarantorEmploymentStatus;
                        txtDonorEmployerAddress.Text = guarantor.GuarantorEmployersAddress;
                        txtDonorEmployerName.Text = guarantor.GuarantorEmployersName;
                        txtDonorCurrentPosition.Text = guarantor.GuarantorCurrentPosition;
                        txtDonorYearsOfEmployment.Text = guarantor.GuarantorNoOfYears.ToString();
                        txtDonorMonthlyIncome.Text = guarantor.GuarantorTotalMonthlyIncome.ToString();
                        txtDonorMonthlyExpenditure.Text = guarantor.GuarantorTotalMonthlyExpenditure.ToString();
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void txtFindLoanDisburse_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.Loan[] response = agent.operation.GetLoans(txtFindLoanDisburse.EditValue.ToString());

                    gridApprovedLoans.DataSource = response;
                    gridApprovedLoans.RefreshDataSource();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdOpenDisbursement_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentLoanId = long.Parse(gridViewApprovedLoans.GetRowCellValue(gridViewApprovedLoans.FocusedRowHandle, gridViewApprovedLoans.Columns[0]).ToString());

                InitializeLoanDisburseForm();
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void dwnLoanAgreement_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DownloadLoanForm("loanAgreement");
        }

        private void dwnPledge_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DownloadLoanForm("pledge");
        }

        private void dwnSLA_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DownloadLoanForm("slaVehicle");
        }

        private void dwnOrder_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DownloadLoanForm("standingOrder");
        }

        private void dwnSalary_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DownloadLoanForm("salaryAssignment");
        }

        private void dwnDisbursement_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //  DownloadLoanForm("disbursementForm");
        }

        private void cmdNewDisbursement_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                InitializeDisbursementRequestForm();
            }
            catch
            {
                ShowErrorMessage("There has been an issue initializing disbursement form");
            }
        }

        private void cmdDisReqSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.DisbursementRequest dispRequest = new sbfa.DisbursementRequest();
                    dispRequest.ApprovalStatus = "Saved";
                    dispRequest.DeclinedApprovedBy = "";
                    dispRequest.DeclineReason = "";
                    dispRequest.DisbursementAmount = float.Parse(txtDisTotal.Text);
                    dispRequest.ChequeNo = txtChequeNo.Text;
                    dispRequest.DisbursementCurrency = "SCR";
                    dispRequest.FK_LoanRequestId = currentId;
                    dispRequest.Id = long.Parse(lblDisNumber.Text);
                    dispRequest.ProcessEndDate = DateTime.Now;
                    dispRequest.ProcessStartDate = DateTime.Now;
                    List<sbfa.DisbursementRequestSupplier> supplier = new List<sbfa.DisbursementRequestSupplier>();

                    for (int a = 0; a < gridViewSuppliers.RowCount; a++)
                    {
                        sbfa.DisbursementRequestSupplier temp = new sbfa.DisbursementRequestSupplier();
                        temp.Price = float.Parse(gridViewSuppliers.GetRowCellValue(a, gridViewSuppliers.Columns[3]).ToString());
                        temp.FK_DisbursementRequestId = dispRequest.Id;
                        temp.Currency = "SCR";
                        temp.Supplier = (gridViewSuppliers.GetRowCellValue(a, gridViewSuppliers.Columns[1]).ToString());
                        temp.Id = long.Parse(gridViewSuppliers.GetRowCellValue(a, gridViewSuppliers.Columns[0]).ToString());
                        supplier.Add(temp);
                    }

                    sbfa.DisbursementRequestSupplier[] supps = supplier.ToArray();
                    dispRequest.Supplier = supps;
                    long save = agent.operation.SaveDisbursementRequest(dispRequest);
                    if (save < 1)
                    {
                        ShowErrorMessage("Failed to save disbursement");
                    }
                    else
                    {
                        lblDisNumber.Text = save.ToString();
                        ribbonPageApproveDisburse.Visible = true;
                        //ribbonPageManageDisburse.Visible = true;
                        InitializeLoanDisburseForm();
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmaEditDisbursement_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentLoanDisId = long.Parse(gridViewLoanDisbursement.GetRowCellValue(gridViewLoanDisbursement.FocusedRowHandle, gridViewLoanDisbursement.Columns[0]).ToString());

                InitializeDisbursementRequestForm(currentLoanDisId);
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdUploadSigned_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {

                uploadDocuments.ShowDialog();
                string fileName = uploadDocuments.SafeFileName;
                //MessageBox.Show(fileName);
                byte[] buffer = File.ReadAllBytes(uploadDocuments.FileName);
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    bool done = agent.operation.UploadDocument(currentLoanDisId, fileName, buffer, 9000, 3);
                    if (done)
                    {
                        agent.operation.UpdateDisbursementStatus(currentLoanDisId);
                        ribbonPageApproveDisburse.Visible = false;
                        ribbonDisReqActions.Visible = false;
                    }
                    else
                    {
                        ShowErrorMessage("Not done !!!");
                    }

                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdCanceDisbursement_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeclineDisbursement declineForm = new DeclineDisbursement();
            declineForm.ShowDialog();
        }

        private void cmdOpenSignedDis_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = navPageSignedDisbursements;
        }

        private void txtDisbursementRequestSearch_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {

                    sbfa.DisbursementRequest[] ass = agent.operation.GetDisbursementRequests("");

                    gridSignedDisbursements.DataSource = ass;
                    gridSignedDisbursements.RefreshDataSource();

                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdSaveVoucher_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.PaymentVoucher voucher = new sbfa.PaymentVoucher();
                    voucher.Id = long.Parse(lblVoucherNo.Text);
                    voucher.FK_DisbursementRequestId = currentLoanDisId;
                    voucher.CancellationFee = float.Parse(txtCancellationFee.Text);
                    voucher.Refund = float.Parse(txtRefund.Text);
                    voucher.VoucherDate = DateTime.Now;
                    voucher.PaymentMethod = cmbPayMethod.Text;

                    long save = agent.operation.SaveVoucher(voucher);
                    if (save < 1)
                    {
                        ShowErrorMessage("Failed to save voucher");
                    }
                    else
                    {
                        lblVoucherNo.Text = save.ToString();
                        vouNotifyRibbonPage.Visible = true;
                        cmdPrintVoucher.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        cmdNotifyVoucherSMS.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        vouDisRibbonPage.Visible = true;
                        vouNotifyRibbonPage.Visible = true;
                        if (cmbPayMethod.Text.ToLower().IndexOf("transfer") > -1)
                        {
                            vouDocRibbonPage.Visible = true;
                        }
                        else
                        {
                            vouDocRibbonPage.Visible = false;
                        }
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdOpenVoucher_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                InitializePaymentVoucher();
            }
            catch
            {
                ShowErrorMessage("There was an issue initializing voucher");
            }
        }

        private void cmdVoucherAuthorisationLetter_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DownloadLoanForm("transferLetter");
        }

        private void txtFindLoanAccount_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.BusinessAccount[] acc = agent.operation.GetBusinessAccounts(txtFindLoanAccount.EditValue.ToString());
                    gridLoanAccount.DataSource = acc;
                    gridLoanAccount.RefreshDataSource();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void navBarPaymentVouchers_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            navigationFrame.SelectedPage = navPageSignedDisbursements;
        }

        private void navBarFindDisbursements_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            navigationFrame.SelectedPage = navPageDisbursements;
        }

        private void cmbEmploymentDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEmploymentDetails.Text == "Unemployed")
                cmbEmployed.SelectedIndex = 1;
            else
                cmbEmployed.SelectedIndex = 0;
        }

        private void txtAge_Leave(object sender, EventArgs e)
        {

        }

        private void txtLoanAmountRequested_Leave(object sender, EventArgs e)
        {
            if (txtLoanAmountRequested.Text == "")
            {
                txtLoanAmountRequested.Text = "0";
            }
        }

        private void gridViewSuppliers_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {

        }

        private void gridViewSuppliers_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                float ttl = 0;
                for (int a = 0; a < gridViewSuppliers.RowCount; a++)
                {
                    ttl += float.Parse(gridViewSuppliers.GetRowCellValue(a, gridViewSuppliers.Columns[3]).ToString());
                }
                txtDisTotal.Text = ttl.ToString();
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdDisburseVoucher_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    bool done = agent.operation.DisburseDisbursement(currentLoanDisId);
                    if (done)
                    {
                        vouRibbonPage.Visible = false;
                        vouDisRibbonPage.Visible = false;
                        vouNotifyRibbonPage.Visible = true;
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            UserAccountSettings accSettings = new UserAccountSettings();
            accSettings.Show();

        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentAccount = (gridViewLoanAccount.GetRowCellValue(gridViewLoanAccount.FocusedRowHandle, gridViewLoanAccount.Columns[0]).ToString());

                InitializeRepaymentsForm(currentAccount);
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdGenerateSchedule_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            pnlRepayments.Visible = true;
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            pnlRepayments.Visible = false;
        }

        private void txtFee_TextChanged(object sender, EventArgs e)
        {
            txtTotal.Text = (float.Parse(txtAmount.Text) + float.Parse(txtFee.Text)).ToString();
        }

        private void txtPayback_Leave(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    float val = agent.operation.CalculateMonthlyRepaymentByBalance(int.Parse(txtPayback.Text), currentAccount, float.Parse(txtAmount.Text));
                    txtMonthly.Text = val.ToString();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }

        }

        private void btnPay_Click(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    bool done = agent.operation.GenerateMonthlyRepayments(int.Parse(txtPayback.Text), currentAccount, float.Parse(txtMonthly.Text), dtpStartRepayment.Value);
                    if (done)
                    {
                        pnlRepayments.Visible = false;
                        sbfa.RepaymentSchedule[] disb = agent.operation.GetRepaymentSchedules(currentAccount);
                        gridRepayments.DataSource = disb;
                        gridRepayments.RefreshDataSource();
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void navBarAccounts_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            navigationFrame.SelectedPage = navPageRecovery;
        }

        private void navBarRepayments_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            navigationFrame.SelectedPage = navPagePendingRepayments;
        }

        private void txtRepaymentFind_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.RepaymentSchedule[] disb = agent.operation.FindPendingRepaymentSchedules(txtRepaymentFind.EditValue.ToString());
                    gridPendingRepayments.DataSource = disb;
                    gridPendingRepayments.RefreshDataSource();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdNotifyRepayments_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentRepaymentId = long.Parse(gridViewPendingRepayments.GetRowCellValue(gridViewPendingRepayments.FocusedRowHandle, gridViewPendingRepayments.Columns[0]).ToString());

                new WarningLetter().ShowDialog();
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }

        }

        private void txtBusinessRegNumber_Leave_1(object sender, EventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.Business response = agent.operation.GetBusiness(txtBusinessRegNumber.Text);

                    txtBusinessName.Text = response.Name;
                    if (response.Name == "")
                        txtBusinessRegNumber.Text = "";
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void gridLoans_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["Status"]);
                if (category == "Complete")
                {
                    e.Appearance.BackColor = Color.White;
                    e.Appearance.BackColor2 = Color.SpringGreen;
                }
                else if (category == "Saved" || category == "Submitted")
                {
                    e.Appearance.BackColor = Color.White;
                    e.Appearance.BackColor2 = Color.SlateGray;
                }
                else if (category == "Payment")
                {
                    e.Appearance.BackColor = Color.White;
                    e.Appearance.BackColor2 = Color.LightSteelBlue;
                }
                else if (category == "Site Visit")
                {
                    e.Appearance.BackColor = Color.White;
                    e.Appearance.BackColor2 = Color.PaleGoldenrod;
                }
                else
                {
                    e.Appearance.BackColor = Color.White;
                    e.Appearance.BackColor2 = Color.Lavender;
                }
            }
        }

        private void cmdConfirmSiteVisit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    bool response = agent.operation.ConfirmSiteVisit(long.Parse(lblSiteId.Text));
                    if (response)
                    {
                        cmdConfirmSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        ribbonPageNotifySiteVisit.Visible = true;
                        cmdNotifySiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        cmdRescheduleSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdNavVisitReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                navigationFrame.SelectedPage = navPageSiteVisitReport;
                InitializeSiteVisitReport();
            }
            catch
            {
                ShowErrorMessage("There was an issue initializing site visit report");
            }
        }

        private void cmdPeriodEnd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    try
                    {
                        bool response = agent.operation.RunEndOfPeriodLoanRecalculations();
                        if (response)
                        {
                            string status = agent.operation.CountEndOfPeriodLoanRecalculations();
                            ShowSuccessMessage("Completed!! \n Current Pending Run:-> " + status);
                        }
                        else
                        {
                            string status = agent.operation.CountEndOfPeriodLoanRecalculations();
                            ShowSuccessMessage("Completed!! \n Current Pending Run:-> " + status);
                        }
                    }
                    catch (TimeoutException ex)
                    {
                        string status = agent.operation.CountEndOfPeriodLoanRecalculations();
                        ShowSuccessMessage("Completed!! \n Current Pending Run:-> " + status);
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdDownloadDisbursement_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    Byte[] doc = agent.operation.GetDisbursementDocument(currentLoanDisId);
                    string filePath = Application.StartupPath + "\\filer\\" + "disburse" + currentLoanId.ToString() + ".pdf";
                    FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                    fs.Write(doc, 0, doc.Length);
                    fs.Flush();
                    fs.Close();
                    try
                    {
                        System.Diagnostics.Process newProcess = new System.Diagnostics.Process();
                        newProcess.StartInfo = new System.Diagnostics.ProcessStartInfo(filePath);
                        newProcess.Start();
                        newProcess.WaitForExit();
                    }
                    catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
                    catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("Please install a PDF Reader on your computer and try again.");
                    }
                    try
                    {
                        System.IO.File.Delete(filePath);
                    }
                    catch
                    {

                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void barButtonItem24_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoBackNavigation();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserAccountSettings userAccountSettings = new UserAccountSettings();
            userAccountSettings.Show();
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                InitializePrequalifications();
            }
            catch
            {
                ShowErrorMessage("There was an issue initializing PreQualification form");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                InitializeNewLoanForm();
            }
            catch
            {
                ShowErrorMessage("There was an issue initializing new loan form");
            }

        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            navigationFrame.SelectedPage = navPageViewLoans;
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            navigationFrame.SelectedPage = navPageDisbursements;
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            navigationFrame.SelectedPage = navPageRecovery;
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoBackNavigation();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoBackNavigation();
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoBackNavigation();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoBackNavigation();
        }

        private void barButtonItem13_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoBackNavigation();
        }

        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoBackNavigation();
        }

        private void barButtonItem30_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DoBackNavigation();
        }

        private void barButtonItem25_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GoHome();
        }

        private void barButtonItem26_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GoHome();
        }

        private void barButtonItem27_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GoHome();
        }

        private void barButtonItem28_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GoHome();
        }

        private void barButtonItem29_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GoHome();
        }

        private void barButtonItem33_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GoHome();
        }

        private void barButtonItem34_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GoHome();
        }

        private void barButtonItem35_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GoHome();
        }

        private void barButtonItem38_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GoHome();
        }

        private void barButtonItem39_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GoHome();
        }

        private void barButtonItem40_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GoHome();
        }

        private void barButtonItem41_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Application.Exit();
        }

        private void cmdWorkFlowRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void cmdWorkFlowBack_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void cmdRescheduleSiteVisit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    bool response = agent.operation.UnConfirmSiteVisit(long.Parse(lblSiteId.Text));
                    if (response)
                    {
                        cmdConfirmSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                        ribbonPageNotifySiteVisit.Visible = false;
                        cmdNotifySiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                        cmdRescheduleSiteVisit.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void barButtonItem42_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    bool done = agent.operation.ClearEmails();

                    sbfa.Email[] response = agent.operation.GetEmails(txtFindEmail.EditValue.ToString());
                    gridEmails.DataSource = response;
                    gridEmails.RefreshDataSource();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void barButtonItem43_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    bool done = agent.operation.ClearSMSs();

                    sbfa.SMS[] response = agent.operation.GetSMSs(txtFindSMS.EditValue.ToString());
                    gridSMS.DataSource = response;
                    gridSMS.RefreshDataSource();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdGuarantor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SBFAMain.currentId > 0)
                new Guarantor().ShowDialog();
            else
            {
                ShowErrorMessage("You need a registered loan application, to view or record Guarantors.");
            }
        }

        private void cmbEmployed_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmdOldRequests_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = navPageOldRequests;
        }

        private void cmdOldPayments_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = navPageOldRepayments;
        }

        private void cmdOldAccounts_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            navigationFrame.SelectedPage = navPageOldRecovery;
        }

        private void txtOldRequests_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.oldLoanRequest[] response = agent.operation.GetoldLoanRequests(txtOldRequests.EditValue.ToString());
                    gridOldRequests.DataSource = response;
                    gridOldRequests.RefreshDataSource();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void txtOldRepayments_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.oldRepayments[] response = agent.operation.GetoldRepayments(txtOldRepayments.EditValue.ToString());
                    gridOldRepayments.DataSource = response;
                    gridOldRepayments.RefreshDataSource();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void txtOldRecovery_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SBFAApi agent = new SBFAApi();
            using (new OperationContextScope(agent.context))
            {
                sbfa.oldRecovery[] response = agent.operation.GetoldRecoverys(txtOldRecovery.EditValue.ToString());
                gridOldRecovery.DataSource = response;
                gridOldRecovery.RefreshDataSource();
            }
        }

        private void navBarItemOldReq_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            navigationFrame.SelectedPage = navPageOldRecovery;
        }

        private void navBarItem1_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            navigationFrame.SelectedPage = navPageOldRequests;
        }

        private void navBarItem2_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            navigationFrame.SelectedPage = navPageOldRepayments;
        }

        private void txtOldLoanAccounts_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.BusinessAccountOld[] acc = agent.operation.GetOldBusinessAccounts(txtOldLoanAccounts.EditValue.ToString());
                    gridOldLoanAccounts.DataSource = acc;
                    gridOldLoanAccounts.RefreshDataSource();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void navBarItem3_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            navigationFrame.SelectedPage = navPageOldMigrationAccounts;
        }

        private void cmdOldMakePayment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            oldAccount = "";
            oldLoan = "";
            ReceiveOldRepayment processInvoice = new ReceiveOldRepayment();
            processInvoice.Show();
        }

        private void navBarItem4_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            navigationFrame.SelectedPage = navPageOldRecovery;
        }

        private void navBarItem5_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            navigationFrame.SelectedPage = navPageOldMigrationAccounts;
        }

        private void navBarItem7_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            navigationFrame.SelectedPage = navPageOldRepayments;
        }

        private void navBarItem6_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            navigationFrame.SelectedPage = navPageOldRequests;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            navigationFrame.SelectedPageIndex = 7;
        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmdUploadTemplates_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UploadTemplates temps = new UploadTemplates();
            temps.ShowDialog();
        }

        private void cmdRecSiteVisit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentSiteId = long.Parse(gridViewRecoveryVisit.GetRowCellValue(gridViewRecoveryVisit.FocusedRowHandle, gridViewRecoveryVisit.Columns[0]).ToString());
                currentId = 0;
                navigationFrame.SelectedPage = navPageScheduleSiteVisit;
                InitializeRecoverySiteVisit();
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdNewSite_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                navigationFrame.SelectedPage = navPageScheduleSiteVisit;
                InitializeNewRecoverySiteVisit();
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdSendEmail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            forAccount = false;
            new SendEmail().ShowDialog();
        }

        private void cmdSendSMS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            forAccount = false;
            new SendSMS().ShowDialog();
        }

        private void cmdMessage_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    //get registration
                    sbfa.LoanRequest registration = agent.operation.GetLoanRequest(currentId);
                    name = registration.FirstNames + " " + registration.LastName + " (" + registration.NIN + ")";
                    mobile = registration.Mobile;
                }
                if (mobile != "")
                {
                    forAccount = true;
                    new SendSMS().ShowDialog();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdEmail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    //get registration
                    sbfa.LoanRequest registration = agent.operation.GetLoanRequest(currentId);
                    name = registration.FirstNames + " " + registration.LastName + " (" + registration.NIN + ")";
                    email = registration.Email;
                }
                if (email != "")
                {
                    forAccount = true;
                    new SendEmail().ShowDialog();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdRecEmail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentAccount = (gridViewLoanAccount.GetRowCellValue(gridViewLoanAccount.FocusedRowHandle, gridViewLoanAccount.Columns[0]).ToString());
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    //get registration
                    sbfa.BusinessRegistration reg = agent.operation.GetBusinessRegistrationByRegistration(currentAccount);
                    name = reg.FirstNames + " " + reg.LastName + " (" + reg.NIN + ")";
                    email = reg.Email;
                }
                if (mobile != "")
                {
                    forAccount = true;
                    new SendEmail().ShowDialog();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdRecSMS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentAccount = (gridViewLoanAccount.GetRowCellValue(gridViewLoanAccount.FocusedRowHandle, gridViewLoanAccount.Columns[0]).ToString());
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    //get registration
                    sbfa.BusinessRegistration reg = agent.operation.GetBusinessRegistrationByRegistration(currentAccount);
                    name = reg.FirstNames + " " + reg.LastName + " (" + reg.NIN + ")";
                    mobile = reg.Mobile;
                }
                if (mobile != "")
                {
                    forAccount = true;
                    new SendSMS().ShowDialog();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdPrintVoucher_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    Byte[] doc = agent.operation.GetAutoDocument("voucher", currentLoanDisId);
                    //server must have a folder on drive c: called docs
                    //also add logo file called logo.png iin an sbfa folder in docs
                    string filePath = Application.StartupPath + "\\filer\\" + currentLoanDisId + ".pdf";
                    FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                    fs.Write(doc, 0, doc.Length);
                    fs.Flush();
                    fs.Close();


                    try
                    {
                        System.Diagnostics.Process newProcess = new System.Diagnostics.Process();
                        newProcess.StartInfo = new System.Diagnostics.ProcessStartInfo(filePath);
                        newProcess.Start();
                        newProcess.WaitForExit();
                    }
                    catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
                    catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("Please install a PDF Reader on your computer and try again.");
                    }
                    try
                    {
                        System.IO.File.Delete(filePath);
                    }
                    catch
                    {

                    }

                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdNotifyVoucherSMS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {

                    sbfa.DisbursementRequest disb = agent.operation.GetDisbursementRequest(currentLoanDisId);

                    //get registration
                    sbfa.LoanRequest registration = agent.operation.GetLoanRequest(disb.FK_LoanRequestId);
                    name = registration.FirstNames + " " + registration.LastName + " (" + registration.NIN + ")";
                    mobile = registration.Mobile;
                }
                if (mobile != "")
                {
                    forAccount = true;
                    new SendSMS().ShowDialog();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdNotifyVoucherEmail_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {

                    sbfa.DisbursementRequest disb = agent.operation.GetDisbursementRequest(currentLoanDisId);

                    //get registration
                    sbfa.LoanRequest registration = agent.operation.GetLoanRequest(disb.FK_LoanRequestId);
                    name = registration.FirstNames + " " + registration.LastName + " (" + registration.NIN + ")";
                    email = registration.Email;
                }
                if (email != "")
                {
                    forAccount = true;
                    new SendSMS().ShowDialog();
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void txtPreNIN_Leave(object sender, EventArgs e)
        {

            try
            {
                if (txtPreNIN.Text == "")
                    return;

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.Resident response = agent.operation.GetResident(txtPreNIN.Text);

                    txtPreNames.Text = response.FirstName;
                    txtPreSurname.Text = response.Surname;

                    preQualNINValid = true;

                    //txtCitizenship.Text = response.Nationality;
                    // Type { get => type; set => type = value; }
                    // Status { get => status; set => status = value; }
                    ///dtpDOB.DateTime = response.DateOfBirth;
                    if (response.FirstName == "")
                    {
                        txtPreNIN.Text = "";
                        ShowNotFound("person");
                        preQualNINValid = false;
                    }

                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void barButtonItem44_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ReceiveRepayment processInvoice = new ReceiveRepayment();
            processInvoice.Show();
        }

        private void barButtonItem45_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("http://seygovsbfa/sbfaportal/");
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void barButtonItem46_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.StartupPath + @"\Updater.exe");
        }

        private void cmdOpenLoanRequest_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentId = 0;
                currentRecoveryAccount = (gridViewLoanAccount.GetRowCellValue(gridViewLoanAccount.FocusedRowHandle, gridViewLoanAccount.Columns[0]).ToString());

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    sbfa.BusinessRegistration request = agent.operation.GetBusinessRegistrationByRegistration(currentRecoveryAccount);
                    currentId = request.FK_LoanRequestId;
                }

                InitializeLoanForm(currentId);
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdOpenOldReq_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void cmdOpenOldRep_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void cmdOldNewSiteVisit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                navigationFrame.SelectedPage = navPageScheduleSiteVisit;
                InitializeOldNewRecoverySiteVisit();
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage("There was an issue initializing the recovery site visit");
            }
        }

        private void cmdOldOpenSiteVisit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentSiteId = long.Parse(gridViewRecoveryVisit.GetRowCellValue(gridViewRecoveryVisit.FocusedRowHandle, gridViewRecoveryVisit.Columns[0]).ToString());
                currentId = 0;
                navigationFrame.SelectedPage = navPageScheduleSiteVisit;
                InitializeOldRecoverySiteVisit();
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdOldSiteVisitReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentSiteId = long.Parse(gridViewRecoveryVisit.GetRowCellValue(gridViewRecoveryVisit.FocusedRowHandle, gridViewRecoveryVisit.Columns[0]).ToString());

                navigationFrame.SelectedPage = navPageSiteVisitReport;
                InitializeOldRecoverySiteVisitReport();
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void cmdOpenOldAcc_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentId = 0;
                currentRecoveryAccount = (gridViewOldRequests.GetRowCellValue(gridViewOldRequests.FocusedRowHandle, gridViewOldRequests.Columns[1]).ToString());
                currentOldLoanNumber = (gridViewOldRequests.GetRowCellValue(gridViewOldRequests.FocusedRowHandle, gridViewOldRequests.Columns[0]).ToString());
                navigationFrame.SelectedPage = navPageRecoveryVisit;
                InitializeOldRecoverySiteVisits();
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            bool valid = new EmailAddressAttribute().IsValid(txtEmail.Text);
            if (txtEmail.Text == "" || valid)
            {
                ;
            }
            else
            {
                txtEmail.Focus();
                ShowErrorMessage("Please put a valid email address");
            }
        }

        private void cmdOpenVisits_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentId = 0;
                currentRecoveryAccount = (gridViewLoanAccount.GetRowCellValue(gridViewLoanAccount.FocusedRowHandle, gridViewLoanAccount.Columns[0]).ToString());
                navigationFrame.SelectedPage = navPageRecoveryVisit;
                InitializeRecoverySiteVisits();
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdRecSiteReport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentSiteId = long.Parse(gridViewRecoveryVisit.GetRowCellValue(gridViewRecoveryVisit.FocusedRowHandle, gridViewRecoveryVisit.Columns[0]).ToString());

                navigationFrame.SelectedPage = navPageSiteVisitReport;
                InitializeRecoverySiteVisitReport();
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void gridViewOldLoanAccounts_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                oldAccount = (gridViewOldLoanAccounts.GetRowCellValue(gridViewOldLoanAccounts.FocusedRowHandle, gridViewOldLoanAccounts.Columns[0]).ToString());

                oldLoan = (gridViewOldLoanAccounts.GetRowCellValue(gridViewOldLoanAccounts.FocusedRowHandle, gridViewOldLoanAccounts.Columns[1]).ToString());
                ReceiveOldRepayment pay = new ReceiveOldRepayment();
                pay.Show();
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void cmdOpenSigned_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                currentLoanDisId = long.Parse(gridViewSignedDisbursements.GetRowCellValue(gridViewSignedDisbursements.FocusedRowHandle, gridViewSignedDisbursements.Columns[0]).ToString());

                InitializeSignedDisbursementRequestForm(currentLoanDisId);
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void gridViewLoanDisbursement_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                currentLoanDisId = long.Parse(gridViewLoanDisbursement.GetRowCellValue(gridViewLoanDisbursement.FocusedRowHandle, gridViewLoanDisbursement.Columns[0]).ToString());

                InitializeDisbursementRequestForm(currentLoanDisId);
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void btnApproveSiteContinue_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSiteComment.Text.Equals(""))
                {
                    txtSiteComment.Focus();

                    return;
                }

                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    if (svOfficerApprove)
                    {
                        //check manager access
                        bool mgr = false;
                        foreach (string v in Globals.userGroupRoles)
                        {
                            if (v == "LoansManager")
                                mgr = true;
                        }

                        if (mgr)
                        {
                            bool response = agent.operation.UpdateSiteVisitRequest(long.Parse(lblSiteId.Text), svApproved, txtSiteComment.Text, false, long.Parse(lblId.Text), "loan");
                            if (response)
                            {
                                pnlApproveSite.Visible = false;
                                lblOfficerComment.Text = txtSiteComment.Text;
                                txtSiteComment.Text = "";
                            }
                            else
                            {
                                txtSiteComment.Text = "";
                                pnlApproveSite.Visible = false;
                            }

                            // DoBackNavigation();
                            if (currentId != 0)
                                InitializeLoanForm(long.Parse(lblId.Text));
                            else
                            {
                                navigationFrame.SelectedPage = navPageRecoveryVisit;
                                InitializeRecoverySiteVisits();
                            }
                        }
                        else
                        {
                            ShowErrorMessage("You do not have rights");
                        }
                    }
                    else
                    {
                        bool response = agent.operation.UpdateSiteVisitRequest(long.Parse(lblSiteId.Text), svApproved, txtSiteComment.Text, true, long.Parse(lblId.Text), "loan");
                        if (response)
                        {
                            lblOfficerComment.Text = txtSiteComment.Text;
                            txtSiteComment.Text = "";
                            pnlApproveSite.Visible = false;
                            approveSiteRibbon.Visible = false;

                            if (currentId != 0)
                                InitializeLoanForm(long.Parse(lblId.Text));
                            else
                            {
                                navigationFrame.SelectedPage = navPageRecoveryVisit;
                                InitializeRecoverySiteVisits();
                            }

                        }
                        else
                        {
                            pnlApproveSite.Visible = false;
                            txtSiteComment.Text = "";
                            ShowErrorMessage("Failed to update Recommendation");
                        }
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //do validations first
            if (txtPreNIN.Text.Equals(""))
            {
                ShowValidationError("Please enter the NIN.");
                return;
            }

            if (txtPreNames.Text.Equals(""))
            {
                ShowValidationError("Please lookup the NIN to get the name of the person.");
                return;
            }

            if (txtPreSurname.Text.Equals(""))
            {
                ShowValidationError("Please lookup the NIN to get the surname of the person.");
                return;
            }

            if (cmbPreBusinessType.Text.Equals("Select option..."))
            {
                ShowValidationError("Please select the business type.");
                return;
            }

            if (txtPreNoOfEmployees.Text.Equals("") || isNumeric(txtPreNoOfEmployees.Text) == false)
            {
                ShowValidationError("Please enter a valid number of employees.");
                return;
            }

            if (txtPreLoanAmount.Text.Equals("") || isNumeric(txtPreLoanAmount.Text) == false)
            {
                ShowValidationError("Please enter a valid loan amount.");
                return;
            }

            if (chkSecurity.CheckedItems.Count == 0)
            {
                ShowValidationError("Please select some form of security.");
                return;
            }


            string failReason = String.Empty;

            //assess the application

            //1. Valid NIN
            if (preQualNINValid == false)
            {
                failReason = failReason + "\n" + "They do not have a valid NIN.";
            }

            //2. Loan amount must be over SCR300,000
            double loanAmount = double.Parse(txtPreLoanAmount.Text);
            if (loanAmount > 300000)
            {
                failReason = failReason + "\n" + "The requested loan amount is above SCR300,000.";
            }

            //3. Less than 10 employees
            int employeeCount = int.Parse(txtPreNoOfEmployees.Text);
            if (employeeCount > 10)
            {
                failReason = failReason + "\n" + "The number of employees exceeds the limit of 10.";
            }

            //4. Annual Turnover less than $1,000,000
            if (cmbPreAnnualTurnover.Text.Equals("1,000,000+"))
            {
                failReason = failReason + "\n" + "The annual turnover is above SCR100,000,000";
            }

            //5. Must have security (that is not just insurance)
            if (chkSecurity.CheckedItems.Count == 1)
            {
                if (chkSecurity.CheckedItems[0].ToString().Equals("Insurance"))
                    failReason = failReason + "\n" + "Another form of security is required apart from Insurance.";

            }


            if (!failReason.Equals(string.Empty))
            {
                ShowDisqualification(failReason);
            }
            else
            {
                ShowQualification();
                if ((Globals.hasAccess("captureApplication")) ? true : false)
                    InitializeNewQualifiedLoan();
            }

        }

        private void treeDesignDocs_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                SBFAApi agent = new SBFAApi();
                using (new OperationContextScope(agent.context))
                {
                    if (treeDesignDocs.SelectedNode.Text.ToLower() != "designs")
                    {
                        currentDocumentDesign = (treeDesignDocs.SelectedNode.Name.Split('_')[1]);
                        sbfa.AutoDocumentsDesign desg = agent.operation.GetAutoDocumentsDesign(currentDocumentDesign);

                        txtDocDesignBody.Text = desg.DocumentDesign;
                        txtDocDesignSMS.Text = desg.DocumentDesignSMS;
                        txtDocDesignSubject.Text = desg.EmailSubject;
                        chkDocEmail.Checked = desg.Email;
                        chkDocSMS.Checked = desg.SMS;
                    }
                }
            }
            catch (TimeoutException tx) { ShowErrorMessage("The system has encountered connectivity issues. Contact your administrator"); }
            catch (NullReferenceException nx) { ShowErrorMessage("Please select a valid parent document or record to manage or view"); }
            catch (Exception ex)
            {
                ShowErrorMessage(ex.Message);
            }
        }

        public void ShowValidationError(string message)
        {
            FlyoutAction action = new FlyoutAction() { Caption = "Validation Error!", Description = message };

            FlyoutCommand command1 = new FlyoutCommand() { Text = "OK", Result = System.Windows.Forms.DialogResult.Yes };
            action.Commands.Add(command1);

            FlyoutProperties properties = new FlyoutProperties();
            properties.ButtonSize = new Size(100, 40);
            properties.Style = FlyoutStyle.MessageBox;
            properties.Appearance.BackColor = Color.Orange;
            properties.Appearance.ForeColor = Color.White;
            DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog.Show(this, action, properties);
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

        }

        public void ShowErrorMessage(string message)
        {
            if (message.ToLower().IndexOf("object reference") > -1)
                message = "Please select a valid record to manage";
            else if (message.ToLower().IndexOf("input string") > -1)
                message = "Please make sure you have supplied all required details to process your request";

                FlyoutAction action = new FlyoutAction() { Caption = "There has been a problem!", Description = message };

            FlyoutCommand command1 = new FlyoutCommand() { Text = "OK", Result = System.Windows.Forms.DialogResult.Yes };
            action.Commands.Add(command1);

            FlyoutProperties properties = new FlyoutProperties();
            properties.ButtonSize = new Size(100, 40);
            properties.Style = FlyoutStyle.MessageBox;
            properties.Appearance.BackColor = Color.Red;
            properties.Appearance.ForeColor = Color.White;
            DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog.Show(this, action, properties);
        }

        public void ShowNotFound(string item)
        {
            FlyoutAction action = new FlyoutAction() { Caption = "Not Found!", Description = "The " + item + " with this number could not be found. Please ensure you entered it correctly and try again." };

            FlyoutCommand command1 = new FlyoutCommand() { Text = "OK", Result = System.Windows.Forms.DialogResult.Yes };
            action.Commands.Add(command1);

            FlyoutProperties properties = new FlyoutProperties();
            properties.ButtonSize = new Size(100, 40);
            properties.Style = FlyoutStyle.MessageBox;
            properties.Appearance.BackColor = Color.Orange;
            properties.Appearance.ForeColor = Color.White;
            DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog.Show(this, action, properties);
        }

        public void ShowQualification()
        {
            FlyoutAction action = new FlyoutAction() { Caption = "Qualifies!", Description = "This business qualifies for the loan application. Would you like to proceed to the load application now?" };

            FlyoutCommand command1 = new FlyoutCommand() { Text = "Yes, proceed", Result = System.Windows.Forms.DialogResult.Yes };
            FlyoutCommand command2 = new FlyoutCommand() { Text = "Not at this time", Result = System.Windows.Forms.DialogResult.No };
            action.Commands.Add(command1);
            action.Commands.Add(command2);

            FlyoutProperties properties = new FlyoutProperties();
            properties.ButtonSize = new Size(100, 40);
            properties.Style = FlyoutStyle.MessageBox;
            properties.Appearance.BackColor = Color.ForestGreen;
            properties.Appearance.ForeColor = Color.White;
            if (DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog.Show(this, action, properties) == DialogResult.Yes)
            {
                GoToLoansApplication();
            }
        }

        public void ShowDisqualification(string reason)
        {
            FlyoutAction action = new FlyoutAction() { Caption = "Disqualified!", Description = "This business does not qualify for the loan application" + Environment.NewLine + Environment.NewLine + "Reasons for the disqualification are:" + Environment.NewLine + Environment.NewLine + reason };

            FlyoutCommand command1 = new FlyoutCommand() { Text = "OK", Result = System.Windows.Forms.DialogResult.Yes };
            action.Commands.Add(command1);

            FlyoutProperties properties = new FlyoutProperties();
            properties.ButtonSize = new Size(100, 40);
            properties.Style = FlyoutStyle.MessageBox;
            properties.Appearance.BackColor = Color.Red;
            properties.Appearance.ForeColor = Color.White;
            DevExpress.XtraBars.Docking2010.Customization.FlyoutDialog.Show(this, action, properties);

        }

        public bool isNumeric(string text)
        {
            double myNum = 0;

            if (Double.TryParse(text, out myNum))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

    }




}