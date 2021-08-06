using MicroFinance.Modal;
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

using Microsoft.Reporting;
using Microsoft.Reporting.WinForms;
using System.ComponentModel;
using System.IO;
using System.Data;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for DashboardFieldOfficer.xaml
    /// </summary>
    public partial class DashboardFieldOfficer : Page
    {
        public static LoginDetails LoginDesignation;
        public DashboardFieldOfficer()
        {
            InitializeComponent();
            //xCustomerPendings.Text = LoadPendingCustomers(MainWindow.LoginDesignation.BranchId).ToString();
        }

        int LoadPendingCustomers(string branchId)
        {
            int Pendings = 0;
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select Count(CustomerDetails.CustId) from CustomerDetails join CustomerGroup on CustomerDetails.CustId = CustomerGroup.CustId join SelfHelpGroup2 on SelfHelpGroup2.SHGName = CustomerGroup.SelfHelpGroup where CustomerDetails.CustomerStatus = 0 and SelfHelpGroup2.BranchId = '"+branchId+"'";
                Pendings = (int)cmd.ExecuteScalar();
                con.Close();
            }
            return Pendings;
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
            //this.NavigationService.Navigate(new CustomerVerified("0100220210814", 2));
        }

        private void xNotificationBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GetNavigationService(this).Navigate(new CollectionSheet());
        }

        private void xAddPeerGroup_Click(object sender, RoutedEventArgs e)
        {
            AddPg APG = new AddPg();
            APG.ShowDialog();
        }

        private void xCollectionSheet_Click(object sender, RoutedEventArgs e)
        {
            string empid = "E0100220210704";
            CollectionShceduleSheet.GenerateShceduleSheet(empid);
        }
    }
}
