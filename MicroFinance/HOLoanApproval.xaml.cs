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
        string BranchID = MainWindow.LoginDesignation.BranchId;
        public List<LoanProcess> RecommendList = new List<LoanProcess>();
        List<LoanProcess> ApprovedCustomerList = new List<LoanProcess>();
        NEFT neft = new NEFT();
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
            foreach(LoanProcess lp in RecommendList)
            {
                Custlist.Items.Add(lp);
            }
        }
        void RemoveItemFromList(string ID)
        {
            Custlist.Items.Clear();
            foreach (LoanProcess lp in RecommendList)
            {
                if(lp.LoanRequestID.Equals(ID)==true)
                {
                    RecommendList.Remove(lp);
                }
                else
                {
                    Custlist.Items.Add(lp);
                }
            
            }
        }
        void setCount()
        {
            int count1 = 0;
            int count2 = 0;
            foreach (LoanProcess c in RecommendList)
            {

                if (c.LoanType == "General Loan" || c.LoanType == "General")
                {
                    count1++;
                }
                else if (c.LoanType == "Special")
                {
                    count2++;
                }
            }
            GeneralLoanCount.Text = count1.ToString();
            SpecialLoanCount.Text = count2.ToString();
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
            loanprocess.ApproveLoan(ID);
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

        private void Generate_NEFTBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                neft.GenerateNEFT_File(ApprovedCustomerList);
                MainWindow.StatusMessageofPage(1, "Excel Generate Successfully!...");
            }
            catch
            {

            }
            
        }

        private void BulkApprovalBtn_Click(object sender, RoutedEventArgs e)
        {
            int Count = 0;
            foreach(LoanProcess lp in RecommendList)
            {
                loanprocess = new LoanProcess();
                string ID = lp.LoanRequestID;
                loanprocess = GetRecommendDetails(ID);
                loanprocess.ApprovedBy = MainWindow.LoginDesignation.EmpId;
                loanprocess.ApproveLoan(ID);
                Count++;
            }
            MainWindow.StatusMessageofPage(1, Count.ToString() + " Loans Approved Successfully!...");
        
        }
    }
}
