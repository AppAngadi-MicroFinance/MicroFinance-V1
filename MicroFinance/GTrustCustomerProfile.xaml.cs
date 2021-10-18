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
    /// Interaction logic for GTrustCustomerProfile.xaml
    /// </summary>
    public partial class GTrustCustomerProfile : Page
    {
        public GTrustCustomerProfile()
        {
            InitializeComponent();
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
