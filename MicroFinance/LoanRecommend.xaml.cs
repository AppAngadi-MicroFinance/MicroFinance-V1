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
    /// Interaction logic for LoanRecommend.xaml
    /// </summary>
    public partial class LoanRecommend : Page
    {
        public string LoginBranchID = MainWindow.LoginDesignation.BranchId;
        public ObservableCollection<LoanProcess> loanDetails = new ObservableCollection<LoanProcess>();
        public static List<LoanProcess> RecommenedList = new List<LoanProcess>();
        public List<string> dummylist = new List<string> { "Ashraf Ali", "Safdhar", "Sasi", "Thalif", "Santhosh", "Ashraf Ali", "Safdhar", "Sasi", "Thalif", "Santhosh", "Ashraf Ali", "Safdhar", "Sasi", "Thalif", "Santhosh" };
        LoanProcess loanProcess = new LoanProcess();
        public LoanRecommend()
        {
            InitializeComponent();
            //AddList();
            //RequestedListBoxNew.ItemsSource = dummylist;
            loanProcess.GetRecommendList(LoginBranchID);
            loanDetails = loanProcess.RecommendList;
            LoadCustData();
            setCount();
        }
        public void LoadCustData()
        {
            Custlist.Items.Clear();
            foreach(LoanProcess lp in loanDetails)
            {
                Custlist.Items.Add(lp);
            }
        }
        //private void Custlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    Cust SelectedCustomer = Custlist.SelectedItem as Cust;
        //   if(RecommenedList.Contains(SelectedCustomer)==true)
        //    {
        //        RecommenedList.Remove(SelectedCustomer);
        //        SelectedCustomersView.Items.Refresh();
        //        SelectedCustomersView.ItemsSource = RecommenedList;
                
               
        //    }
        //    else
        //    {
        //        RecommenedList.Add(SelectedCustomer);
        //        //RecommededcustomerList.Items.Clear();
        //        SelectedCustomersView.ItemsSource = RecommenedList;
        //        SelectedCustomersView.Items.Refresh();

        //    }

        //}
        void setCount()
        {
            int count1 = 0;
            int count2 = 0;
            foreach (LoanProcess c in loanDetails)
            {

                if (c.LoanType == "General Loan"||c.LoanType== "General")
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

        private void ApprovetoHiMarkBtn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string ID = button.Uid.ToString();
            Custlist.Items.Refresh();
            if(loanProcess.IsAlreadyRecommend(ID))
            {
                MainWindow.StatusMessageofPage(0, "This loan is Already Recommend");
            }
            else
            {
                Custlist.Items.Refresh();
                loanProcess.RecommendLoan(ID);
                //AddtoRecommendList(ID);
                RemoveItemFromList(ID);
                SelectedCustomersView.Items.Add(GetRecommendDetails(ID));
                //LoadCustData();
                MainWindow.StatusMessageofPage(1, "loan Recommend Successfully...");

            }
            
        }

        void RemoveItemFromList(string ID)
        {
            Custlist.Items.Clear();
            foreach (LoanProcess lp in loanDetails)
            {
                if(lp.LoanRequestID.Equals(ID)!=true)
                {
                    loanDetails.Remove(lp);
                }
                else
                {
                    Custlist.Items.Add(lp);
                }
                

            }
        }
        public LoanProcess GetRecommendDetails(string ID)
        {
            LoanProcess selectedLoan = new LoanProcess();
            int i = 0;
            foreach (LoanProcess process in loanDetails)
            {
                if (process.LoanRequestID == ID)
                {
                    selectedLoan = loanDetails.ElementAt(i);
                }
                else
                {
                    i++;
                }
            }
            return selectedLoan;
        }

        public void AddtoRecommendList(string ID)
        {
            LoanProcess ln = new LoanProcess();
            foreach(LoanProcess l in loanDetails)
            {
                if(l.LoanRequestID.Equals(ID))
                {
                    RecommenedList.Add(l);
                }
            }
        }

        private void DownloadExcelBtn_Click(object sender, RoutedEventArgs e)
        {
            List<HiMark> Himarklist = new List<HiMark>();
            HiMark himarkReport = new HiMark();
            foreach (LoanProcess lp in loanDetails)
            {
                himarkReport = new HiMark(lp);
                Himarklist.Add(himarkReport);
            }
            try
            {
                himarkReport = new HiMark();
                himarkReport.hiMarksList = Himarklist;
                himarkReport.createHimarkXls();
                MainWindow.StatusMessageofPage(1, "Excel Export Successfully... Location: D:\\");
            }
            catch 
            {

            }
            
        }

        private void xBackwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
                this.NavigationService.Navigate(new DashboardBranchManager());
        }
    }
}
