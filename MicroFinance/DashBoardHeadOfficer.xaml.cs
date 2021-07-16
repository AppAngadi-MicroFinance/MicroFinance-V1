using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Interaction logic for DashBoardHeadOfficer.xaml
    /// </summary>
    public partial class DashBoardHeadOfficer : Page
    {
        string ConnectionString = "Data Source=.;Initial Catalog=MicroFinance;Integrated Security=True";
        public DashBoardHeadOfficer()
        {
            InitializeComponent();
        }

        private void xAddNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddEmployee());
        }

        private void xPendingCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ModifyEmployee());
        }
        private void xLoanRequestListBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LoanAfterHimark());
        }

        private void xAddNewBranch_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CreateBranch());
        }

        private void xPopupCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            xSearchPersonPop.Visibility = Visibility.Collapsed;
           
        }

        private void xFindCustomer_Click(object sender, RoutedEventArgs e)
        {
            xSearchPersonPop.Visibility = Visibility.Visible;
            xPopUpHeading.Text = "Find Customer";
        }

        private void xFindEmployee_Click(object sender, RoutedEventArgs e)
        {
            xSearchPersonPop.Visibility = Visibility.Visible;
            EmployeeSearchFrame.NavigationService.Navigate(new ModifyEmployee());
            xPopUpHeading.Text = "Find Employee";
        }

        private void xAllowanceReportBtn_Click(object sender, RoutedEventArgs e)
        {
            AddRegion region = new AddRegion();
            region.ShowDialog();
        }

        private void xCustomerApproval_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CustomerNotification(2));
        }

        private void EmployeeSeachPanelCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            xSearchPersonPop.Visibility = Visibility.Collapsed;
        }


        //void GetCustomersForApprovals(string branchId)
        //{
        //    using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
        //    {
        //        sqlconn.Open();
        //        if (sqlconn.State == ConnectionState.Open)
        //        {
        //            SqlCommand sqlcomm = new SqlCommand();
        //            sqlcomm.Connection = sqlconn;
        //            sqlcomm.CommandText = "select SNo from Region where RegionName='" + Region + "'";
        //        }
        //        sqlconn.Close();
        //        return Result;
        //    }
        //}
    }
}
