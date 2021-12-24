using MicroFinance.Repository;
using MicroFinance.ViewModel;
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
    /// Interaction logic for LoanCloseView.xaml
    /// </summary>
    public partial class LoanCloseView : Page
    {
        List<EnrollDetailsView> ApplicationList = new List<EnrollDetailsView>();
        public LoanCloseView(string AadharNumber)
        {
            InitializeComponent();
            ApplicationList = EnrollDetailsRepository.GetEnrollDetails(AadharNumber);
            EnrollDetailGrid.ItemsSource = ApplicationList;
        }

       

        private void RejectBtn_Click(object sender, RoutedEventArgs e)
        {
            if(EnrollDetailGrid.SelectedItem!=null)
            {
                EnrollDetailsView SelectedApplication = EnrollDetailGrid.SelectedItem as EnrollDetailsView;
                int LoanStatus = (SelectedApplication.LoanStatusCode < 3) ? 4 : 13;
                bool res = LoanRepository.DeactivateApplication(SelectedApplication.RequestID, LoanStatus);
                this.NavigationService.Navigate(new DashboardBranchManager());
            }
        }
    }
}
