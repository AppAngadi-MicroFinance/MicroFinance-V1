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

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for LoanRecommend.xaml
    /// </summary>
    public partial class LoanRecommend : Page
    {
        List<Cust> customerlist = new List<Cust>();
        public static List<Cust> RecommenedList = new List<Cust>();
        public LoanRecommend()
        {
            InitializeComponent();
            AddList();
            setCount();
            Custlist.ItemsSource = customerlist;
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
            foreach (Cust c in customerlist)
            {
                
                if(c.LoanType=="General")
                {
                    count1++;
                }
                else if(c.LoanType=="Special")
                {
                    count2++;
                }
            }
            GeneralLoanCount.Text = count1.ToString();
            SpecialLoanCount.Text = count2.ToString();
        }

        public void AddList()
        {
            customerlist.Add(new Cust { CustName = "Ashraf ALi", FieldOfficerName = "Safdhar", LoanType = "General", LoanAmount = 10000, LoanPeriod = "12", MonthlyIncome = 5000 });
            customerlist.Add(new Cust { CustName = "Thalif", FieldOfficerName = "Safdhar", LoanType = "General", LoanAmount = 10000, LoanPeriod = "12", MonthlyIncome = 5000 });
            customerlist.Add(new Cust { CustName = "Santhosh", FieldOfficerName = "Safdhar", LoanType = "General", LoanAmount = 10000, LoanPeriod = "12", MonthlyIncome = 5000 });
            customerlist.Add(new Cust { CustName = "Sameer", FieldOfficerName = "Safdhar", LoanType = "General", LoanAmount = 10000, LoanPeriod = "12", MonthlyIncome = 5000 });
            customerlist.Add(new Cust { CustName = "Rashik", FieldOfficerName = "Safdhar", LoanType = "General", LoanAmount = 10000, LoanPeriod = "12", MonthlyIncome = 5000 });
            customerlist.Add(new Cust { CustName = "Mani", FieldOfficerName = "Safdhar", LoanType = "General", LoanAmount = 10000, LoanPeriod = "12", MonthlyIncome = 5000 });

        }

        private void SendToHiMarkBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LoanAfterHimark());

        }

        private void ApprovetoHiMarkBtn_Click(object sender, RoutedEventArgs e)
        {
            Cust SelectedCustomer = Custlist.SelectedItem as Cust;
            RecommenedList.Add(SelectedCustomer);
            //RecommededcustomerList.Items.Clear();
            SelectedCustomersView.ItemsSource = RecommenedList;
            SelectedCustomersView.Items.Refresh();

        }
    }
}
