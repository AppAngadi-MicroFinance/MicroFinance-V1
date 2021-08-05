using System;
using System.Collections.Generic;
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
using MicroFinance.Modal;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for FieldOfficerUpdateCustomer.xaml
    /// </summary>
    public partial class CustomerNotification : Page
    {
        Notification notification = new Notification();

        string EmployeeId = MainWindow.LoginDesignation.EmpId;
        string BranchId = MainWindow.LoginDesignation.BranchId;
        List<Notification> PendingCustomer = new List<Notification>();
        int _status;
        public CustomerNotification(int Status)
        {
            _status = Status;
            InitializeComponent();
            GetDetailsBasedStatus();
            MainGrid.DataContext = PendingCustomer;
            NotficationCount.Text = PendingCustomer.Count.ToString();
            NotificationList.ItemsSource = PendingCustomer;
        }

        void GetDetailsBasedStatus()
        {
            if(_status==0)
            {
                GetPendingCustomerDetails();
            }
            else if(_status==1)
            {
                GetRecomendedCustomer();
            }
            else if(_status==2)
            {
                GetWatingForApprovalCustomer();
            }
        }

        void GetPendingCustomerDetails()
        {
            using (SqlConnection sqlConnection=new SqlConnection(Properties.Settings.Default.db))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select distinct CustomerDetails.CustId,CustomerDetails.Name,CustomerGroup.BranchName from CustomerDetails join CustomerGroup on CustomerDetails.CustId = CustomerGroup.CustId join SelfHelpGroup2 on SelfHelpGroup2.SHGName = CustomerGroup.SelfHelpGroup where CustomerDetails.CustomerStatus = 0 and SelfHelpGroup2.BranchId = '"+BranchId+"' and SelfHelpGroup2.FOid = '"+EmployeeId+"' ";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while(sqlDataReader.Read())
                {
                    PendingCustomer.Add(new Notification { CustomerId = sqlDataReader.GetString(0), EmpId = EmployeeId, NotificationFrom = sqlDataReader.GetString(1), NotificationPurpose = "Update Remain Details", NotificationDate = DateTime.Now.ToString("dd-MM-yyyy"), CustomerStatus = "Pending",BranchName=sqlDataReader.GetString(2) });
                }
                sqlDataReader.Close();
            }
        }

        void GetRecomendedCustomer()
        {

            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.db))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select distinct CustomerDetails.CustId,CustomerDetails.Name,CustomerGroup.BranchName from CustomerDetails join CustomerGroup on CustomerDetails.CustId = CustomerGroup.CustId join SelfHelpGroup2 on SelfHelpGroup2.SHGName = CustomerGroup.SelfHelpGroup where CustomerDetails.CustomerStatus = 1 and SelfHelpGroup2.BranchId = '" + BranchId + "' and SelfHelpGroup2.FOid = '" + EmployeeId + "' ";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    PendingCustomer.Add(new Notification { CustomerId = sqlDataReader.GetString(0), EmpId = EmployeeId, NotificationFrom = sqlDataReader.GetString(1), NotificationPurpose = "Update Remain Details", NotificationDate = DateTime.Now.ToString("dd-MM-yyyy"), CustomerStatus = "Pending", BranchName = sqlDataReader.GetString(2) });
                }
                sqlDataReader.Close();
            }
        }
        void GetWatingForApprovalCustomer()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.db))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select distinct CustomerDetails.CustId,CustomerDetails.Name,CustomerGroup.BranchName from CustomerDetails join CustomerGroup on CustomerDetails.CustId = CustomerGroup.CustId join SelfHelpGroup2 on SelfHelpGroup2.SHGName = CustomerGroup.SelfHelpGroup where CustomerDetails.CustomerStatus = 2 and SelfHelpGroup2.BranchId = '" + BranchId + "' and SelfHelpGroup2.FOid = '" + EmployeeId + "' ";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    PendingCustomer.Add(new Notification { CustomerId = sqlDataReader.GetString(0), EmpId = EmployeeId, NotificationFrom = sqlDataReader.GetString(1), NotificationPurpose = "Update Remain Details", NotificationDate = DateTime.Now.ToString("dd-MM-yyyy"), CustomerStatus = "Pending", BranchName = sqlDataReader.GetString(2) });
                }
                sqlDataReader.Close();
            }
        }

        private void NotificationList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            notification = new Notification();
            notification = (Notification)NotificationList.SelectedItem;
            if (_status==0)
            {
                NavigationService.GetNavigationService(this).Navigate(new AddCustomer(notification.CustomerId));
            }
            else if(_status==1)
            {
                NavigationService.GetNavigationService(this).Navigate(new VerifyCustomer(notification,1));
            }
            else
            {
                NavigationService.GetNavigationService(this).Navigate(new VerifyCustomer(notification,2));
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
