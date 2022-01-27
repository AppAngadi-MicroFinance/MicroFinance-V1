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
    /// Interaction logic for SuperAdminDashboard.xaml
    /// </summary>
    public partial class SuperAdminDashboard : Page
    {
        public SuperAdminDashboard()
        {
            InitializeComponent();
        }

        private void CollectionBtn_Click(object sender, RoutedEventArgs e)
        {
            SuperAdminMainPanel.NavigationService.Navigate(new SACollectionView());
        }

        private void LoanStatusBtn_Click(object sender, RoutedEventArgs e)
        {
            SuperAdminMainPanel.NavigationService.Navigate(new SALoanApplcationView());
        }

        private void ExpenseReportBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ExpenceReportView());
        }
    }
}
