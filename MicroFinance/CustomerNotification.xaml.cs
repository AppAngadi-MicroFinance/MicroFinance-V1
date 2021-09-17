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
                GetDocumentPendingCustomerDetails();
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

        void GetDocumentPendingCustomerDetails()
        {
            using (SqlConnection sqlConnection=new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select LoanApplication.CustId,CustomerDetails.Name,LoanApplication.RequestId from LoanApplication join CustomerDetails on CustomerDetails.CustId = LoanApplication.CustId where LoanApplication.EmployeeId='" + EmployeeId+ "' and LoanApplication.BranchId='" + BranchId+ "' and LoanApplication.LoanStatus = 3 ";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while(sqlDataReader.Read())
                {
                    PendingCustomer.Add(new Notification { CustomerId = sqlDataReader.GetString(0), EmpId = EmployeeId, NotificationFrom = sqlDataReader.GetString(1), NotificationPurpose = "Update Document Details", NotificationDate = DateTime.Now.ToString("dd-MM-yyyy"), CustomerStatus = "Pending",LoanRequestId=sqlDataReader.GetString(2) });
                }
                sqlDataReader.Close();
            }
        }

        void GetRecomendedCustomer()
        {

            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select LoanApplication.CustId,CustomerDetails.Name,LoanApplication.RequestId,LoanApplication.EmployeeId from LoanApplication join CustomerDetails on CustomerDetails.CustId = LoanApplication.CustId where  LoanApplication.BranchId='" + BranchId + "' and LoanApplication.LoanStatus = 6 ";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    PendingCustomer.Add(new Notification { CustomerId = sqlDataReader.GetString(0), EmpId = sqlDataReader.GetString(3), NotificationFrom = sqlDataReader.GetString(1), NotificationPurpose = "Update Document Details", NotificationDate = DateTime.Now.ToString("dd-MM-yyyy"), CustomerStatus = "Pending", LoanRequestId = sqlDataReader.GetString(2) });
                }
                sqlDataReader.Close();
            }
        }
        void GetWatingForApprovalCustomer()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select LoanApplication.CustId,CustomerDetails.Name,LoanApplication.RequestId,LoanApplication.EmployeeId  from LoanApplication join CustomerDetails on CustomerDetails.CustId = LoanApplication.CustId where  LoanApplication.BranchId='" + BranchId + "' and LoanApplication.LoanStatus = 7 ";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    PendingCustomer.Add(new Notification { CustomerId = sqlDataReader.GetString(0), EmpId = sqlDataReader.GetString(3), NotificationFrom = sqlDataReader.GetString(1), NotificationPurpose = "Update Document Details", NotificationDate = DateTime.Now.ToString("dd-MM-yyyy"), CustomerStatus = "Pending", LoanRequestId = sqlDataReader.GetString(2) });
                }
                sqlDataReader.Close();
            }
        }

        private void NotificationList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            notification = new Notification();
            notification = (Notification)NotificationList.SelectedItem;
            if(notification!=null)
            {
                if (_status == 0)
                {
                    NavigationService.GetNavigationService(this).Navigate(new CustomerVerified(notification.CustomerId, 3, notification.LoanRequestId, notification.EmpId));
                }
                else if (_status == 1)
                {
                    NavigationService.GetNavigationService(this).Navigate(new CustomerVerified(notification.CustomerId, 6, notification.LoanRequestId, notification.EmpId));
                }
                else
                {
                    NavigationService.GetNavigationService(this).Navigate(new CustomerVerified(notification.CustomerId, 7, notification.LoanRequestId, notification.EmpId));
                }
            }
           
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
