using System;
using System.Collections.Generic;
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
        public List<LoanProcess> loanDetails = new List<LoanProcess>();
        public static List<LoanProcess> RecommenedList = new List<LoanProcess>();
        public List<string> dummylist = new List<string> { "Ashraf Ali", "Safdhar", "Sasi", "Thalif", "Santhosh", "Ashraf Ali", "Safdhar", "Sasi", "Thalif", "Santhosh", "Ashraf Ali", "Safdhar", "Sasi", "Thalif", "Santhosh" };
        LoanProcess loanProcess = new LoanProcess();
        public LoanRecommend()
        {
            InitializeComponent();
            //AddList();
            //RequestedListBoxNew.ItemsSource = dummylist;
            loanProcess.GetRequestList(LoginBranchID);
            loanDetails=loanProcess.RequestList;
            Custlist.ItemsSource = loanDetails;
            setCount();
            SelectedCustomersView.ItemsSource = RecommenedList;
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

                if (c.LoanType == "General Loan")
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
                loanProcess.RecommendLoan(ID);
                AddtoRecommendList(ID);
                SelectedCustomersView.Items.Refresh();
                MainWindow.StatusMessageofPage(1, "loan Recommend Successfully...");

            }
            
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
    }
}
