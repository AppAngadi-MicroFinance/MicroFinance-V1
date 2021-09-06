using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MicroFinance.Modal;
using MicroFinance.Reports;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for HOLoanApproval.xaml
    /// </summary>
    public partial class HOLoanApproval : Page
    {
        LoanProcess loanprocess = new LoanProcess();
        SUMAtoHO Suma = new SUMAtoHO();
        List<SUMAtoHO> SumaApprovalList = new List<SUMAtoHO>();
        string BranchID = MainWindow.LoginDesignation.BranchId;
        public List<LoanProcess> RecommendList = new List<LoanProcess>();
        List<LoanProcess> ApprovedCustomerList = new List<LoanProcess>();
        NEFT neft = new NEFT();
        public HOLoanApproval(List<SUMAtoHO> ResList)
        {
            InitializeComponent();
            loanprocess.GetLoanDetailList(9);
            RecommendList.Clear();
            RecommendList = loanprocess.LoanProcessList;
            SumaApprovalList=ResList;
            LoadData();
            setCount();
        }
        public HOLoanApproval()
        {
            InitializeComponent();
            loanprocess.GetLoanDetailList(9);
            RecommendList.Clear();
            RecommendList = loanprocess.LoanProcessList;
            // Custlist.ItemsSource = RecommendList;
            LoadData();
            setCount();
        }
        void LoadData()
        {
            Custlist.Items.Clear();
            foreach(SUMAtoHO SM in SumaApprovalList)
            {
                string AadharNumber = SM.AadharNumber;
                string name = SM.Name;
                foreach(LoanProcess lp in RecommendList)
                {
                    if(lp.AadharNo.Equals(AadharNumber, StringComparison.CurrentCultureIgnoreCase) &&lp.CustomerName.Equals(name,StringComparison.CurrentCultureIgnoreCase))
                    {
                        ApprovedCustomerList.Add(lp);
                        Custlist.Items.Add(lp);
                    }
                }
            }
        }
        void RemoveItemFromList(string ID)
        {
            Custlist.Items.Clear();
            foreach (LoanProcess lp in ApprovedCustomerList)
            {
                if(lp.LoanRequestID.Equals(ID)==true)
                {
                    RecommendList.Remove(lp);
                    break;
                }
               
            
            }
        }
        void setCount()
        {
            //int count1 = 0;
            //int count2 = 0;
            //foreach (LoanProcess c in RecommendList)
            //{

            //    if (c.LoanType == "General Loan" || c.LoanType == "General")
            //    {
            //        count1++;
            //    }
            //    else if (c.LoanType == "Special")
            //    {
            //        count2++;
            //    }
            //}
            GeneralLoanCount.Text = RecommendList.Count().ToString();
            SpecialLoanCount.Text = SumaApprovalList.Count().ToString();
        }
        private void xBackwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
                this.NavigationService.GoBack();
        }

        private void Custlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void RejectResonOKBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ApproveLoanBtn_Click(object sender, RoutedEventArgs e)
        {
            loanprocess= new LoanProcess();
            Button btn = sender as Button;
            Custlist.Items.Refresh();
            int index = GetIndexofItem(btn.Uid.ToString());
            //Custlist.Items.RemoveAt(index);
            string ID = btn.Uid.ToString();
            loanprocess = GetRecommendDetails(ID);
            loanprocess.ApprovedBy = MainWindow.LoginDesignation.EmpId;
            string AadharNumber = loanprocess.AadharNo;
            string Name = loanprocess.CustomerName;
            SUMAtoHO Data = GetSumuDate(AadharNumber, Name);
            loanprocess.ApproveLoan(ID, Data);
            //AddtoApprovalList(ID);
            RemoveItemFromList(ID);
            MainWindow.StatusMessageofPage(1, "Loan Approved SuccessFully!..");
        }


        public LoanProcess GetRecommendDetails(string ID)
        {
            LoanProcess selectedLoan = new LoanProcess();
            int i = 0;
            foreach (LoanProcess process in RecommendList)
            {
                if (process.LoanRequestID == ID)
                {
                    selectedLoan = RecommendList.ElementAt(i);
                }
                else
                {
                    i++;
                }
            }
            return selectedLoan;
        }

        public int GetIndexofItem(string ID)
        {
            LoanProcess selectedLoan = new LoanProcess();
            int i = 0;
            int index = 0;
            foreach (LoanProcess process in RecommendList)
            {
                if (process.LoanRequestID == ID)
                {
                    selectedLoan = RecommendList.ElementAt(i);
                    index = i;
                }
                else
                {
                    i++;
                }
            }
            return index;
        }
        //public void AddtoApprovalList(string ID)
        //{
            
        //    foreach (LoanProcess l in RecommendList)
        //    {
        //        if (l.LoanRequestID.Equals(ID))
        //        {
        //            if(!SelectedCustomersView.Items.Contains(l))
        //            {
        //                SelectedCustomersView.Items.Add(l);
        //                ApprovedCustomerList.Add(l);
        //            }
                    
        //        }
        //    }
        //}

        private async void Generate_NEFTBtn_Click(object sender, RoutedEventArgs e)
        {
            GifPanel.Visibility = Visibility.Visible;
            await System.Threading.Tasks.Task.Run(() => GenerateNEFTFile());
            GifPanel.Visibility = Visibility.Collapsed;
            this.NavigationService.Navigate(new DashBoardHeadOfficer());
            
        }


        void GenerateNEFTFile()
        {
            try
            {
                neft.GenerateNEFT_File(ApprovedCustomerList);
                MainWindow.StatusMessageofPage(1, "Excel Generated Successfully!...");
            }
            catch (Exception ex)
            {
                MainWindow.StatusMessageofPage(0, "Error!");

            }
        }

        private void BulkApprovalBtn_Click(object sender, RoutedEventArgs e)
        {
            int Count = 0;
            foreach(LoanProcess lp in ApprovedCustomerList)
            { 
                loanprocess = new LoanProcess();
                string ID = lp.LoanRequestID;
                loanprocess = GetRecommendDetails(ID);
                loanprocess.ApprovedBy = MainWindow.LoginDesignation.EmpId;
                
                string AadharNumber = lp.AadharNo;
                string Name = lp.CustomerName;
                SUMAtoHO Data = GetSumuDate(AadharNumber, Name);
                loanprocess.ApproveLoan(ID,Data);
                Count++;
            }
            MainWindow.StatusMessageofPage(1, Count.ToString() + " Loan(s) Approved Successfully!...");
            BulkApprovalBtn.Visibility = Visibility.Collapsed;
        
        }

        private SUMAtoHO GetSumuDate(string AadharNo,string Name)
        {
            SUMAtoHO Result = new SUMAtoHO();
            foreach (SUMAtoHO sm in SumaApprovalList)
            {
                if (sm.AadharNumber.Equals(AadharNo) && sm.Name.Equals(Name))
                {
                    Result = sm;
                    break;
                }
            }
            return Result;
        }
    }
}
