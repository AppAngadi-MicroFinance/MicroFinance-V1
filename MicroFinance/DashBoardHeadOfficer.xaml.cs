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
using MicroFinance.Modal;
using MicroFinance.Reports;
using Microsoft.Win32;
using MicroFinance.ViewModel;
using System.Collections.ObjectModel;
using MicroFinance.Repository;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for DashBoardHeadOfficer.xaml
    /// </summary>
    public partial class DashBoardHeadOfficer : Page
    {
       
        string ConnectionString = MicroFinance.Properties.Settings.Default.DBConnection;
        GTtoSamunnati GTtoSAMU = new GTtoSamunnati();
        Branch branch = new Branch();
        List<string> RegionList = new List<string>();
        List<Branch> BranchList = new List<Branch>();
        string BranchID = "01202107002";
        public DashBoardHeadOfficer()
        {
            InitializeComponent();
            // ManageApprovalNotification();
            // EmployeeFrame.NavigationService.Navigate(new TransferEmployee());
            LoadCount();
        }

        void LoadCount()
        {
            int count = NotificationRepository.GetLoanApplicationCount(11);
            xLoanDisbursmentCount.Text = (count<100)?count.ToString():"99+";
        }


       

        private void xAddNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddEmployee());
        }

        private void xPendingCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }
        private void xLoanRequestListBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LoanAfterHimark());
        }

        private void xAddNewBranch_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CreateBranch());
        }

        

        private void xFindCustomer_Click(object sender, RoutedEventArgs e)
        {
            //xSearchPersonPop.Visibility = Visibility.Visible;
            this.NavigationService.Navigate(new CustomerSearch());
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
            using (SqlConnection sqlconn = new SqlConnection(Properties.Settings.Default.DBConnection))
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

        private  void LoanDesposment_Click(object sender, RoutedEventArgs e)
        {
            //List<SUMAtoHO> ResultList = new List<SUMAtoHO>();
            //OpenFileDialog openFileDlg = new OpenFileDialog();
            //openFileDlg.Filter = "Excel Files |*.xls;*.xlsx;*.xlsm";
            //openFileDlg.Title = "Choose File";
            //openFileDlg.InitialDirectory = @"C:\";
            //Nullable<bool> result = openFileDlg.ShowDialog();
            //if (result == true)
            //{
            //    GifPanel.Visibility = Visibility.Visible;
            //    string FileFrom = openFileDlg.FileName;
            //    await System.Threading.Tasks.Task.Run(() =>ResultList= uploadSamuFile(FileFrom));
            //    GifPanel.Visibility = Visibility.Collapsed;
            //    this.NavigationService.Navigate(new HOLoanApproval(ResultList));

            //}
            this.NavigationService.Navigate(new RecommendNew(11));
            
        }


        List<SUMAtoHO> uploadSamuFile(string FileFrom)
        {
            SUMAtoHO Suma = new SUMAtoHO();
            List<SUMAtoHO> SumaApprovalList = new List<SUMAtoHO>();
            var FilePath = FileFrom.Split('\\');
            string FileName = FilePath[FilePath.Length - 1];
            SUMAtoHO SUMA = new SUMAtoHO();
           
            if (!SUMA.IsFileExists(FileName))
            {
                SumaApprovalList = Suma.ImportSamuFile(FileFrom);

                //this.NavigationService.Navigate(new HOLoanApproval(FileFrom));
            }
            else
            {
                MainWindow.StatusMessageofPage(0, "This File Already Upload Please Check!...");
            }
            return SumaApprovalList;
        }

        private  void SAMUSendRequestBtn_Click(object sender, RoutedEventArgs e)
        {
            //old
            this.NavigationService.Navigate(new SamuExport());



            //GifPanel.Visibility = Visibility.Visible;
            //try
            //{
            //    await System.Threading.Tasks.Task.Run(() => GenerateSamuFile());
            //    GifPanel.Visibility = Visibility.Collapsed;
            //}
            //catch
            //{
            //    MainWindow.StatusMessageofPage(0, "Error...");
            //    GifPanel.Visibility = Visibility.Collapsed;
            //}



            //new
            //ObservableCollection<RecommendView> RecommendList = new ObservableCollection<RecommendView>();
            //GifPanel.Visibility = Visibility.Visible;
            //try
            //{
            //    await System.Threading.Tasks.Task.Run(() =>RecommendList= GetSamuRequest());
            //    GifPanel.Visibility = Visibility.Collapsed;
            //}
            //catch
            //{
            //    MainWindow.StatusMessageofPage(0, "Error...");
            //    GifPanel.Visibility = Visibility.Collapsed;
            //}
        }

        ObservableCollection<RecommendView> GetSamuRequest()
        {
            ObservableCollection<RecommendView> RequestList = new ObservableCollection<RecommendView>();
            try
            {
                RequestList = LoanRepository.GetRecommendListForRM(10);
            }
            catch
            {
                MainWindow.StatusMessageofPage(0, "Error...");
            }
            return RequestList;
        }

        private void CollectionReportDownload_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DownloadCollectionReport());
        }

        private async void SamuImportBtn_Click(object sender, RoutedEventArgs e)
        {
            List<SUMAtoHO> ResultList = new List<SUMAtoHO>();
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Filter = "Excel Files |*.xls;*.xlsx;*.xlsm";
            openFileDlg.Title = "Choose File";
            openFileDlg.InitialDirectory = @"C:\";
            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {
                GifPanel.Visibility = Visibility.Visible;
                string FileFrom = openFileDlg.FileName;
                List<SamuReportView> RequestList = new List<SamuReportView>();
                
                await System.Threading.Tasks.Task.Run(() => RequestList = SamuRepository.GetSamuRequest(FileFrom));
                GifPanel.Visibility = Visibility.Collapsed;

                this.NavigationService.Navigate(new SamuResult(RequestList));
                // this.NavigationService.Navigate(new HOLoanApproval(ResultList));

            }
        }

        private void BtnDownloadNEFT_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RecommendNew(12));
        }

        private void TransferBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new TransferEmployee());
        }
    }
}
