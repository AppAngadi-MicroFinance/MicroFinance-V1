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

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for LoanRecommend.xaml
    /// </summary>
    public partial class LoanRecommend : Page
    {
        public string LoginBranchID = MainWindow.LoginDesignation.BranchId;
        public List<LoanProcess> loanDetails = new List<LoanProcess>();
        public static List<Cust> RecommenedList = new List<Cust>();
        LoanProcess loanProcess = new LoanProcess();
        public LoanRecommend()
        {
            InitializeComponent();
            //AddList();
            loanProcess.GetRequestList(LoginBranchID);
            loanDetails=loanProcess.RequestList;
            Custlist.ItemsSource = loanDetails;
            setCount();
            
           // RecommededcustomerList.ItemsSource = RecommenedList;
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
            //foreach (Cust c in customerlist)
            //{
                
            //    if(c.LoanType=="General")
            //    {
            //        count1++;
            //    }
            //    else if(c.LoanType=="Special")
            //    {
            //        count2++;
            //    }
            //}
            GeneralLoanCount.Text = count1.ToString();
            SpecialLoanCount.Text = count2.ToString();
        }

        public void AddList()
        {
            loanDetails.Add(new LoanProcess { LoanAmount = 10000, LoanType = "General Loan", LoanPeriod = 24 ,CustomerName="Ashraf Ali",LocalityTown="Trichy",DoorNumber="42/20a",StreetName="Kuppu sami Naidu Street",AadharNo="852074109630",Religion="Muslim",Occupation="Daily Wages",Community="BCM",City="Trichy",MonthlyIncome=15000});
        }

        private void SendToHiMarkBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LoanAfterHimark());

        }

        private void ApprovetoHiMarkBtn_Click(object sender, RoutedEventArgs e)
        {
            //Cust SelectedCustomer = Custlist.SelectedItem as Cust;
            //RecommenedList.Add(SelectedCustomer);
            //SelectedCustomersView.ItemsSource = RecommenedList;
            //SelectedCustomersView.Items.Refresh();
            Button button = sender as Button;
            string ID = button.Uid.ToString();
            Custlist.Items.Refresh();
            loanProcess.RecommendLoan(ID);
            //MessageBox.Show(ID);
            
        }
    }
}
