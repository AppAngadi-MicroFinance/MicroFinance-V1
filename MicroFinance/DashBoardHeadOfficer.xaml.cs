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

        string BranchID = MainWindow.LoginDesignation.BranchId;
        public DashBoardHeadOfficer()
        {
            InitializeComponent();
            ManageApprovalNotification();
        }


        void ManageApprovalNotification()
        {
            int forApprovals = GetCustomersStatus2(BranchID);
            if (forApprovals > 0)
            {
                xCustApprovalsCount.Text = forApprovals.ToString();
            }
            else
            {
                xNotificationBatch.Visibility = Visibility.Collapsed;
            }
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


        int GetCustomersStatus2(string branchId)
        {
            int value = 0;
            using (SqlConnection sqlconn = new SqlConnection(MainWindow.ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = sqlconn;
                    cmd.CommandText = "select count(distinct CustomerDetails.CustId) from CustomerDetails join CustomerGroup on CustomerGroup.BranchId = '"+branchId+"' where CustomerDetails.CustomerStatus = 2";
                    value = (int)cmd.ExecuteScalar();
                }
                sqlconn.Close();
            }
            return value;
        }

        private void EmployeeSeachPanelCloseBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
