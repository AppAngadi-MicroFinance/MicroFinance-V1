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
            //xSearchPersonPop.Visibility = Visibility.Visible;
            this.NavigationService.Navigate(new CustomerSearch());
        }

        private void xFindEmployee_Click(object sender, RoutedEventArgs e)
        {
            branch = new Branch();
            xSearchPersonPop.Visibility = Visibility.Visible;
            RegionCombo.ItemsSource = null;
            branch.GetRegionList();
            branch.GetBranchList();
            RegionList = branch.RegionList;
            BranchList = branch.BranchList;
            RegionCombo.ItemsSource = RegionList;
            BranchCombo.Items.Clear();
            EmployeeList.Items.Clear();  
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

        private void RegionCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BranchCombo.Items.Clear();
            string SelectedRegion = RegionCombo.SelectedValue as string;
            FetchBranch(SelectedRegion);
        }
        public void FetchBranch(string regionname)
        {
            foreach (Branch b in BranchList)
            {
                if (b.RegionName == regionname)
                {
                    BranchCombo.Items.Add(b.BranchName);
                }
            }
        }

        private void BranchCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EmployeeList.Items.Clear();
            List<string> ActiveEmployees = new List<string>();
            string selectedBrach = BranchCombo.SelectedValue as string;
            ActiveEmployees= branch.ActiveEmployees(branch.GetBranchID(selectedBrach));
            foreach(string s in ActiveEmployees)
            {
                Employee emp = new Employee();
                emp.GetEmployeeDetails(s);
                EmployeeList.Items.Add(emp);
            }
            
            

        }
        private void xSearchPersonPopcloseBtn_Click(object sender, RoutedEventArgs e)
        {
            xSearchPersonPop.Visibility = Visibility.Collapsed;
        }

        private void EmployeeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Employee selectedEmployee = EmployeeList.SelectedValue as Employee;
            this.NavigationService.Navigate(new AddEmployee(selectedEmployee));
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

        private void SAMUSendRequestBtn_Click(object sender, RoutedEventArgs e)
        {
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
        }

        void GenerateSamuFile()
        {
            try
            {
                GTtoSAMU.GenerateSamunnati_File();
                MainWindow.StatusMessageofPage(1, "Excel Generated Successfully!...");
            }
            catch
            {
                MainWindow.StatusMessageofPage(0, "Error...");
            }
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
