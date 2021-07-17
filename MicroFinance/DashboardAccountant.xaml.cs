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
    /// Interaction logic for DashboardAccountant.xaml
    /// </summary>
    public partial class DashboardAccountant : Page
    {
        public DashboardAccountant()
        {
            InitializeComponent();
        }

        private void xCreateSchedule_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void xDailyCollection_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CollectionVerify());
        }
    }
}
