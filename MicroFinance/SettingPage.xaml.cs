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
    /// Interaction logic for SettingPage.xaml
    /// </summary>
    public partial class SettingPage : Page
    {
        public SettingPage()
        {
            InitializeComponent();
        }

        private void PurposeBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new LoanPurposeView(2));
        }

        private void BankNameBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new LoanPurposeView(1));
        }

        private void ExpenseBtn_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.NavigationService.Navigate(new LoanPurposeView(0));
        }
    }
}
