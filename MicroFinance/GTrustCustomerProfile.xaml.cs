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
using MicroFinance.ViewModel;
using MicroFinance.Reports;
using MicroFinance.Repository;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for GTrustCustomerProfile.xaml
    /// </summary>
    public partial class GTrustCustomerProfile : Page
    {
        public List<LoanApplicationViewModel> Applications = new List<LoanApplicationViewModel>();
        public List<LoanViewModel> Loans = new List<LoanViewModel>();
        CustomerViewModel Customer = new CustomerViewModel();
        public GTrustCustomerProfile()
        {
            InitializeComponent();
        }
        public GTrustCustomerProfile(string CustomerID)
        {
            InitializeComponent();
            //LoadLoans();
            Loans = LoanRepository.LoanDetails(CustomerID);
            Applications = LoanRepository.LoanApplicationDetails(CustomerID);
            SecurityBalance.Text = CustomerRepository.GetSavingsAccountBalance(CustomerID).ToString("N2");
            Customer = CustomerRepository.GetCustomerMetaDetials(CustomerID);
            CustomerDetailsGrid.DataContext = Customer;
            ApplicationDetailsList.ItemsSource = Applications;
            LoanDetailsList.ItemsSource = Loans;
            CheckVisibility();
        }
        void CheckVisibility()
        {
            if (Loans.Count == 0)
                LoandetailsPanel.Visibility = Visibility.Collapsed;
            if (Applications.Count == 0)
                LoanApplicationPanel.Visibility = Visibility.Collapsed;

        }

        void LoadLoans()
        {
            for(int i=0;i<10;i++)
            {
                //Applications.Add(new LoanViewModel());
               // Loans.Add(new LoanApplicationViewModel{LoanType=i.ToString() });
            }
        }
    }

    class CustomerProfile
    {
        int TotalWeeks = 50;

        public int WeekPassed { get; set; }

        public string Purpose{ get; set; }

        public int ActualLoanAmount { get;set; }
        
        public int ActualBalanceLoan { get; set; }

        public string LoanAmount
        {
            get { return GetMoneystring(ActualLoanAmount); }
        }
        public string BalanceAmount
        {
            get { return GetMoneystring(ActualBalanceLoan); }
        }
        public string DateString
        {
            get { return GetDateString(); }
        }




        private string GetMoneystring(int actualLoanAmount)
        {
            return "";
        }
        private string GetDateString()
        {
            return DateTime.Now.ToLongDateString();
        }
    }

}
