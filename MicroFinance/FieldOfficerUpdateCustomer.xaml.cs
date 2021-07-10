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
    public partial class FieldOfficerUpdateCustomer : Page
    {
        Notification notification = new Notification();

        string EmployeeId = MainWindow.LoginDesignation.EmpId;
        string BranchId = MainWindow.LoginDesignation.BranchId;
        List<Notification> PendingCustomer = new List<Notification>();
        public FieldOfficerUpdateCustomer()
        {
            InitializeComponent();
            GetPendingCustomerDetails();
        }
        void GetPendingCustomerDetails()
        {
            string fieldofficer = MainWindow.LoginDesignation.LoginDesignation;
            using (SqlConnection sqlConnection=new SqlConnection(Properties.Settings.Default.db))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select distinct CustomerDetails.CustId,CustomerDetails.Name from CustomerDetails join CustomerGroup on CustomerDetails.CustomerStatus = 0  where CustomerGroup.BranchId = (select BranchId from BranchEmployees where BranchEmployees.Empid = '" + EmployeeId + "' and IsActive = 'True')";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while(sqlDataReader.Read())
                {
                    PendingCustomer.Add(new Notification { CustomerId = sqlDataReader.GetString(0), EmpId = EmployeeId, NotificationFrom = sqlDataReader.GetString(1), NotificationPurpose = "Update Remain Details", NotificationDate = DateTime.Now.ToString("dd-MM-yyyy"), CustomerStatus = "Active",BranchName="KK Nagar1" });
                }
                sqlDataReader.Close();
            }
            MainGrid.DataContext = PendingCustomer;
            NotficationCount.Text = PendingCustomer.Count.ToString();
            NotificationList.ItemsSource = PendingCustomer;
        }

        private void NotificationList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            notification = new Notification();
            notification =(Notification)NotificationList.SelectedItem;
            NavigationService.GetNavigationService(this).Navigate(new AddCustomer(notification.CustomerId));
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
