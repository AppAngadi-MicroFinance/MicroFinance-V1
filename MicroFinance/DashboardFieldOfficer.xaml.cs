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
    /// Interaction logic for DashboardFieldOfficer.xaml
    /// </summary>
    public partial class DashboardFieldOfficer : Page
    {
        public DashboardFieldOfficer()
        {
            InitializeComponent();
            
        }

        private void xAddCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddCustomer());
        }

        private void xCollectionEntryBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CollectionStartPage());
        }

        private void xLoanRequestBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LoanRequest());
        }

        private void xFindCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            xSearchPersonPop.Visibility = Visibility.Visible;
        }

        private void xPopupCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            xSearchPersonPop.Visibility = Visibility.Collapsed;
            xSearchBoxCustomer.Clear();
        }

        private void xPendingCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CustomerNotification(0));
        }
    }
}
