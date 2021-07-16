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
    /// Interaction logic for DashboardBranchManager.xaml
    /// </summary>
    public partial class DashboardBranchManager : Page
    {
        public DashboardBranchManager()
        {
            InitializeComponent();
        }

        private void xAddCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddCustomer());
        }

        private void xLoanRequestListBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LoanRecommend());
        }

        private void xRecommendCustome_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CustomerNotification(1));
        }
    }
}
