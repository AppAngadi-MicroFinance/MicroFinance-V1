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
using MicroFinance.ViewModel;
using MicroFinance.Repository;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for DashboardFieldOfficer.xaml
    /// </summary>
    public partial class DashboardFieldOfficer : Page
    {
        public static LoginDetails LoginDesignation;
        string DayOFWeek;
        public DashboardFieldOfficer()
        {
            InitializeComponent();
            //xCustomerPendings.Text = LoadPendingCustomers(MainWindow.LoginDesignation.BranchId).ToString();

            EnrollStartDate.SelectedDate = DateTime.Now;
            EnrollEndDate.SelectedDate = DateTime.Now;
            EnrollDatailsPanel.Visibility = Visibility.Collapsed;
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
            // this.NavigationService.Navigate(new CustomerNotification(0));
            //this.NavigationService.Navigate(new CustomerVerified("0100220210814", 2));
            string EmpID = MainWindow.LoginDesignation.EmpId;
            this.NavigationService.Navigate(new NotificationPage(3,EmpID));
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
            xDaySelectionPopup.Visibility = Visibility.Visible;
        }
        static public DataTable ConvertToDataTable<T>(IList<T> data)
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
        static void GenerateScheduleSheet(CollectionSheetModelPDF obj)
        {
            //string filedate = showDate.Text;
            DataTable dt1 = new DataTable();
            dt1 = ConvertToDataTable(obj.GroupWiseTotal);


            DataTable dt2 = new DataTable();
            dt2 = ConvertToDataTable(obj.CollectionList);


            //For table 1
            ReportDataSource reportDataSource1 = new ReportDataSource();
            reportDataSource1.Name = "DataSet1"; // Name of the DataSet we set in .rdlc
            reportDataSource1.Value = dt1;

            //For table 2
            ReportDataSource reportDataSource2 = new ReportDataSource();
            reportDataSource2.Name = "DataSet2"; // Name of the DataSet we set in .rdlc
            reportDataSource2.Value = dt2;

            //Setting Report Viewer
            ReportViewer reportViewer1 = new ReportViewer();
            reportViewer1.LocalReport.ReportEmbeddedResource = "MicroFinance.CollectionSheetFO.rdlc";
            reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
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
                byte[] bytes = reportViewer1.LocalReport.Render("PDF", null, out mimeType1, out encoding1, out extension1,out streamids1, out warnings1);
                dir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "REPORTS\\");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                if (Directory.Exists(dir))
                {
                    //var temp = "" + data + "" + "_WeeklyReport"+showDate+"";
                    //var temp = "" + data + "" + "_" + "" + SelectedCluster + "";
                    FileStream fs = new FileStream(dir + "Collection_Sheet_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".pdf", FileMode.Create);

                    var temps = fs.ToString();
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't generate Collection Sheet for You. Contact admin.");
            }
        }

        private void xCancelDaySelection_Click(object sender, RoutedEventArgs e)
        {
            xDaySelectionPopup.Visibility = Visibility.Collapsed;
            DayOFWeek = string.Empty;
        }

        private async void xDownloadBtn_Click(object sender, RoutedEventArgs e)
        {

            GifPanel.Visibility = Visibility.Visible;
            await System.Threading.Tasks.Task.Run(()=>DownloadPdf());
            GifPanel.Visibility = Visibility.Collapsed;
            xDaySelectionPopup.Visibility = Visibility.Collapsed;
            MainWindow.StatusMessageofPage(1, "Collection Report Generated SuccessFully!...");
        }

        void DownloadPdf()
        {
            CollectionSheetModelPDF collectinoDetails = new CollectionSheetModelPDF(MainWindow.LoginDesignation.EmpId, DayOFWeek);
            GenerateScheduleSheet(collectinoDetails);
            
        }

        private void xDaysList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = xDaysList.SelectedItem as ComboBoxItem;
            DayOFWeek = selectedItem.Content as string;
        }

        private void EnrollDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            //string BranchId = MainWindow.LoginDesignation.BranchId;
            //string EmpID = MainWindow.LoginDesignation.EmpId;
            //this.NavigationService.Navigate(new EnrollDetails(BranchId,EmpID));
            SectionAGrid.IsEnabled = false;
            SectionBGrid.IsEnabled = false;
            SectionCGrid.IsEnabled = false;
            SectionDGrid.IsEnabled = false;
            EnrollDatailsPanel.Visibility = Visibility.Visible;
        }

        private async void EnrollOkBtn_Click(object sender, RoutedEventArgs e)
        {
            List<EnrollDetailsView> EnrollDetails = new List<EnrollDetailsView>();
            DateTime StartDate = EnrollStartDate.SelectedDate.Value;
            DateTime EndDate = EnrollEndDate.SelectedDate.Value;
            if(StartDate!=null && EndDate!=null)
            {
                if(StartDate<=EndDate)
                {
                    DateModel DateData = new DateModel();
                    DateData.FromDate = StartDate;
                    DateData.EndDate = EndDate;
                    string BranchId = MainWindow.LoginDesignation.BranchId;
                    string EmpID = MainWindow.LoginDesignation.EmpId;
                    GifPanel.Visibility = Visibility.Visible;
                    await System.Threading.Tasks.Task.Run(() => EnrollDetails = GetEnrollDetails(DateData, EmpID, BranchId));
                    GifPanel.Visibility = Visibility.Collapsed;


                    if (EnrollDetails.Count == 0)
                    {
                        string Message = string.Format("No Enroll Found Betweeen {0} to {1}.", StartDate.ToString("dd-MMM-yyyy"), EndDate.ToString("dd-MMM-yyyy"));
                        MessageBox.Show(Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        this.NavigationService.Navigate(new EnrollDetails(EnrollDetails, BranchId));
                    }
                }
                else
                {
                    MessageBox.Show("Enter Proper Date!...", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                

                
            }
        }



        public static List<EnrollDetailsView> GetEnrollDetails(DateModel DateData,string EmpID,string BranchID)
        {
            List<EnrollDetailsView> EnrollDetails = new List<EnrollDetailsView>();
            EnrollDetails = EnrollDetailsRepository.GetEnrollDetails(BranchID, EmpID, DateData);
            return EnrollDetails;
        }

        private void EnrollCancelBtn_Click(object sender, RoutedEventArgs e)
        {
            SectionAGrid.IsEnabled = true;
            SectionBGrid.IsEnabled = true;
            SectionCGrid.IsEnabled = true;
            SectionDGrid.IsEnabled = true;
            MainGrid.IsEnabled = true;
        }
    }
}
