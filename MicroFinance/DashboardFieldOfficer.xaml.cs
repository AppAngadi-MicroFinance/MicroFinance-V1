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

        }
        void FillData3(List<CollectionDetails> collectionList)
        {
            //string filedate = showDate.Text;
            DataTable dt1 = new DataTable();
            dt1 = ConvertToDataTable(collectionList.ToList());
            ReportDataSource reportDataSource1 = new ReportDataSource();
            reportDataSource1.Name = "DataSet1"; // Name of the DataSet we set in .rdlc
            reportDataSource1.Value = dt1;
            ReportViewer reportViewer1 = new ReportViewer();
            reportViewer1.LocalReport.ReportEmbeddedResource = "CollectionPDF.LoanCollection.rdlc";
            reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            reportViewer1.RefreshReport();
            reportViewer1.ProcessingMode = ProcessingMode.Local;

            Warning[] warnings1;
            string[] streamids1;
            string mimeType1;
            string encoding1;
            string extension1;
            try
            {
                string dir = string.Empty;
                //string showdatess = Changeformat(showDate.Text);
                byte[] bytes = reportViewer1.LocalReport.Render(
                  "PDF", null, out mimeType1, out encoding1, out extension1,
                  out streamids1, out warnings1);
                dir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "REPORTS\\");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                if (Directory.Exists(dir))
                {
                    //var temp = "" + data + "" + "_WeeklyReport"+showDate+"";

                    //var temp = "" + data + "" + "_" + "" + SelectedCluster + "";

                    FileStream fs = new FileStream(dir + "1" + ".pdf", FileMode.Create);

                    var temps = fs.ToString();
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        //void GetActiveLoanCustomer(string empId)
        //{
        //    using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
        //    {
        //        sql.Open();
        //        SqlCommand command = new SqlCommand();
        //        command.Connection = sql;
        //        command.CommandText = "select LoanDetails.CustomerID, LoanDetails.LoanID, LoanDetails.LoanType, LoanDetails.LoanAmount,LoanDetails.ApproveDate from LoanDetails  join CustomerGroup on CustomerGroup.CustId = LoanDetails.CustomerID join PeerGroup on PeerGroup.GroupId = CustomerGroup.PeerGroupId join TimeTable on TimeTable.SHGId = PeerGroup.SHGid and TimeTable.EmpId = '" + empId + "' where LoanDetails.IsActive = 1";
        //        SqlDataReader reader = command.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            CollectionDetails temp = new CollectionDetails();
        //            temp.CustID = reader.GetString(0);
        //            temp.LoanId = reader.GetString(1);
        //            temp.LoanType = reader.GetString(2);
        //            temp.LoanAmount = reader.GetInt32(3);
        //            temp.SanctionDate = reader.GetDateTime(4);
        //            temp.Attendance = string.Empty;

        //            ActiveLoanCustomer.Add(temp);
        //        }
        //        sql.Close();
        //    }
        //}
    }
}
