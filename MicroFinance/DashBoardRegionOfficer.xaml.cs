using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MicroFinance.Modal;
using MicroFinance.Reports;
using Microsoft.Win32;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using MicroFinance.ViewModel;
using MicroFinance.Repository;
using Newtonsoft.Json;
using System.Net.Http;
using MessageBox = System.Windows.MessageBox;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for DashBoardRegionOfficer.xaml
    /// </summary>
    public partial class DashBoardRegionOfficer : Page
    {
        Branch branch = new Branch();
        public string LoginBranchID = MainWindow.LoginDesignation.BranchId;
        public ObservableCollection<LoanProcess> loanDetails = new ObservableCollection<LoanProcess>();
        public static List<LoanProcess> RecommenedList = new List<LoanProcess>();

        public static List<HimarkRequestView> RequestList = new List<HimarkRequestView>();
        LoanProcess loanProcess = new LoanProcess();
        public DashBoardRegionOfficer()
        {
            InitializeComponent();

            EnrollStartDate.SelectedDate = DateTime.Now;
            EnrollEndDate.SelectedDate = DateTime.Now;
            LoadCount();
        }


        void LoadCount()
        {
            int c = NotificationRepository.GetLoanApplicationCount(1);
            xExportHimarkCount.Text = c.ToString();
            int c1 = NotificationRepository.GetLoanApplicationCount(9);
            xLoanRecommendCount.Text = c1.ToString();
            int c2 = NotificationRepository.GetLoanApplicationCount(10);
            xSendToSamuCount.Text = c2.ToString();
        }

        
        private void xAddCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddCustomer());
        }

        private async void xLoanRequestListBtn_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<RecommendView> ResultData = new ObservableCollection<RecommendView>();
            //this.NavigationService.Navigate(new LoanRecommend(8));
            try
            {
                GifPanel.Visibility = Visibility.Visible;
                await System.Threading.Tasks.Task.Run(() => ResultData = GetRecommDetails(9));
                if (ResultData.Count != 0)
                {
                    GifPanel.Visibility = Visibility.Collapsed;
                    this.NavigationService.Navigate(new RecommendNew(9, ResultData));
                }
                else
                {
                    GifPanel.Visibility = Visibility.Collapsed;
                    MessageBox.Show("No Records Found!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch (Exception ex)
            {
                GifPanel.Visibility = Visibility.Collapsed;
                MessageBox.Show(ex.Message);
            }

        }

        public ObservableCollection<RecommendView> GetRecommDetails(int Code)
        {
            return LoanRepository.GetRecommendListForRM(Code);
        }

        private void xRecommendCustome_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CustomerNotification(2));
        }

        private void xAddSHGBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddNewSelfHelpGroup());
        }
        int GetCustomersStatus1(string branchId)
        {
            int value = 0;
            using (SqlConnection sqlconn = new SqlConnection(MainWindow.ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = sqlconn;
                    cmd.CommandText = "select count(distinct CustomerDetails.CustId) from CustomerDetails join CustomerGroup on CustomerGroup.BranchId = '" + branchId + "' where CustomerDetails.CustomerStatus = 1";
                    value = (int)cmd.ExecuteScalar();
                }
                sqlconn.Close();
            }
            return value;
        }


        private void HimarkPanelCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            HimarkExportPanel.Visibility = Visibility.Collapsed;
        }

        private void HimarkBtn_Click(object sender, RoutedEventArgs e)
        {
            #region OldModule
            //loanProcess = new LoanProcess();
            //loanProcess.GetRequestList();
            //loanDetails = loanProcess.RequestList;


            //RequestList = HimarkRepository.GetHimarkRequestList();
            //RequestedListBoxNew.ItemsSource = RequestList;
            //HimarkExportPanel.Visibility = Visibility.Visible;
            #endregion
            this.NavigationService.Navigate(new ExportHimarkReport());
        }

        private async void ExportHimarkBtn_Click(object sender, RoutedEventArgs e)
        {
            #region OldModule
            //Stopwatch stopwatch2 = new Stopwatch();

            //List<HimarkModel> HimarkList = new List<HimarkModel>();
            //HimarkList = HimarkRepository.GetDetailsForReport(RequestList);
            ////stopwatch2.Start();
            ////MainWindow.TimeBuilder.Append("\nStarting Time for CreateHimarkFile : " + stopwatch2.Elapsed.Milliseconds.ToString());
            ////List<HiMark> Himarklist = new List<HiMark>();
            //HiMark himarkReport = new HiMark();
            ////foreach (LoanProcess lp in loanDetails)
            ////{
            ////    himarkReport = new HiMark(lp);
            ////    Himarklist.Add(himarkReport);
            ////}
            //try
            //{

            //    himarkReport = new HiMark();
            //    himarkReport.createHimarkXls(HimarkList);
            //    MainWindow.StatusMessageofPage(1, "Excel Export Successfully... Location: Doucuments\\Reports\\Hi-Mark Report");
            //    
            //    MainWindow.TimeBuilder.Append("\nStarting Time for CreateHimarkFile : " + stopwatch2.Elapsed.Milliseconds.ToString());
            //    stopwatch2.Stop();
            //   // System.Windows.MessageBox.Show(MainWindow.TimeBuilder.ToString());
            //}
            //catch (Exception ex)
            //{
            //    MainWindow.StatusMessageofPage(0, ex.Message);
            //}
            #endregion
            HimarkExportPanel.Visibility = Visibility.Collapsed;
            GifPanel.Visibility = Visibility.Visible;
            await System.Threading.Tasks.Task.Run(() => ExportHimarkFile());
            GifPanel.Visibility = Visibility.Collapsed;
        }
        void ExportHimarkFile()
        {
            
            List<HimarkModel> HimarkList = new List<HimarkModel>();
            HimarkList =HimarkRepository.GetDetailsForReport(RequestList);
            HiMark himarkReport = new HiMark();
            try
            {
                himarkReport = new HiMark();
                himarkReport.createHimarkXls(HimarkList);
                MainWindow.StatusMessageofPage(1,"File Exported Successfully!...");
            }
            catch (Exception ex)
            {
                MainWindow.StatusMessageofPage(0, ex.Message);
            }
        }

        private async void ImportHimarkBtn_Click(object sender, RoutedEventArgs e)
        {
            //BrowseInputFile();
            List<HimarkResultModel> himarkResults = new List<HimarkResultModel>();
            List<CustomerHimarkDataModel> CustomerDetailsList = new List<CustomerHimarkDataModel>();

            ObservableCollection<HimarkResultExcelModel> HimarkResultData = new ObservableCollection<HimarkResultExcelModel>();
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Filter = "Excel Files |*.xls;*.xlsx;*.xlsm";
            openFileDlg.Title = "Choose File";
            openFileDlg.InitialDirectory = @"C:\";
            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {
                string FileFrom = openFileDlg.FileName;
                if(!IsFileUsed(FileFrom))
                {
                    GifPanel.Visibility = Visibility.Visible;
                   // await System.Threading.Tasks.Task.Run(() =>himarkResults= ImportHimarkFile(FileFrom));
                   // await System.Threading.Tasks.Task.Run(() => CustomerDetailsList = LoanRepository.GetDetailsForHimarkResult());
                    await System.Threading.Tasks.Task.Run(() => HimarkResultData = CombineData(ImportHimarkFile(FileFrom),LoanRepository.GetDetailsForHimarkResult()));
                    GifPanel.Visibility = Visibility.Collapsed;
                    this.NavigationService.Navigate(new HimarkResultView(HimarkResultData));
                }
                else
                {
                    MainWindow.StatusMessageofPage(0, "The process Cant't Access "+FileFrom+" It is used by another process");
                }
            }
        }


        ObservableCollection<HimarkResultExcelModel> CombineData(List<HimarkResultExcelModel> HimarkData,List<CustomerHimarkDataModel> CustomerData)
        {
            ObservableCollection<HimarkResultExcelModel> ResultList = new ObservableCollection<HimarkResultExcelModel>();
            int sno = 0;
            foreach (HimarkResultExcelModel HM in HimarkData)
            {
                sno += 1;
                CustomerHimarkDataModel Customer = CustomerData.Where(temp => temp.AadharNumber == HM.AadharNumber).FirstOrDefault();
                if(Customer!=null)
                {
                    HM.RequestID = Customer.RequestID;
                    HM.Name = Customer.CustomerName;
                }
                HM.SNo = sno;
                ResultList.Add(HM);
            }

            return ResultList;
        }

        List<HimarkResultExcelModel> ImportHimarkFile(string FileFrom)
        {
            List<HimarkResultExcelModel> ResultList = new List<HimarkResultExcelModel>();
            var FilePath = FileFrom.Split('\\');
            string FileName = FilePath[FilePath.Length - 1];
            HimarkResultModel HMResult = new HimarkResultModel();
            HimarkResult HmResultList = new HimarkResult();
            
                try
                {
                    List<HimarkResultModel> resultList = new List<HimarkResultModel>();
                   ResultList= HmResultList.BulkInsertData(FileFrom, 0);
                    
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            
            return ResultList;
        }

        private bool IsFileUsed(string FilePath)
        {
            try
            {
                using(FileStream stream=new FileStream(FilePath,FileMode.Open))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }
            return false;
        }
        

        private void RequestedListBoxNew_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void HimarkResultBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HimarkResultData());
        }
        private void xCustomerApproval_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CustomerNotification(2));
        }
        
        private void LoanDesposment_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HOLoanApproval());
        }

        private void xAddNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddEmployee());
        }

        private void xFindCustomer_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CustomerSearch());
        }

        private void xFindEmployee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void xAllowanceReportBtn_Click(object sender, RoutedEventArgs e)
        {
            AddRegion region = new AddRegion();
            region.ShowDialog();
        }

        private void BranchCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void xAddNewBranch_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CreateBranch());
        }

        private void RegionCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void xSearchPersonPopcloseBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EmployeeList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AssignBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AssignCenter("E0100120210904"));
        }

        private void EnrollDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            MenuPanel.IsEnabled = false;
            EnrollDatailsPanel.Visibility = Visibility.Visible;
        }

        private async void EnrollOkBtn_Click(object sender, RoutedEventArgs e)
        {
            List<EnrollDetailsView> EnrollDetails = new List<EnrollDetailsView>();
            DateTime StartDate = EnrollStartDate.SelectedDate.Value;
            DateTime EndDate = EnrollEndDate.SelectedDate.Value;
            if (StartDate != null && EndDate != null)
            {
                if (StartDate <= EndDate)
                {
                    DateModel DateData = new DateModel();
                    DateData.FromDate = StartDate;
                    DateData.ToDate = EndDate;
                    string BranchId = MainWindow.LoginDesignation.BranchId;
                    GifPanel.Visibility = Visibility.Visible;
                    await System.Threading.Tasks.Task.Run(() => EnrollDetails = GetEnrollDetails(DateData));
                    GifPanel.Visibility = Visibility.Collapsed;


                    if (EnrollDetails.Count == 0)
                    {
                        string Message = string.Format("No Enroll Found Betweeen {0} to {1}.", StartDate.ToString("dd-MMM-yyyy"), EndDate.ToString("dd-MMM-yyyy"));
                        System.Windows.MessageBox.Show(Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        this.NavigationService.Navigate(new EnrollDetails(EnrollDetails));
                    }
                }
                else
                {
                    System.Windows.MessageBox.Show("Enter Proper Date!...", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
        }

        public static List<EnrollDetailsView> GetEnrollDetails(DateModel DateData)
        {
            List<EnrollDetailsView> EnrollDetails = new List<EnrollDetailsView>();
            EnrollDetails = EnrollDetailsRepository.GetEnrollDetails(DateData);
            return EnrollDetails;
        }

        private void EnrollCancelBtn_Click(object sender, RoutedEventArgs e)
        {
            MenuPanel.IsEnabled = true;
            EnrollDatailsPanel.Visibility = Visibility.Collapsed;
        }

        private void TransferBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new TransferEmployee());
        }

        private void SAMUSendRequestBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new SamuExport());
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

        private void CollectionDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DownloadCollectionReport());
        }

        private async void SDRecommendBtn_Click(object sender, RoutedEventArgs e)
        {
            BranchRequestView RequestDetails = new BranchRequestView { BranchID = MainWindow.LoginDesignation.BranchId, StatusCode = 2 };
            GifPanel.Visibility = Visibility.Visible;
            MenuPanel.IsEnabled = false;
            await GetRequestDetails(RequestDetails);
            MenuPanel.IsEnabled = true;
            GifPanel.Visibility = Visibility.Collapsed;
            if (RequestDetailsList.Count != 0)
            {
                this.NavigationService.Navigate(new SDRecommendView(RequestDetailsList,2));
            }
        }
        List<SavingsAccountRequestView> RequestDetailsList = new List<SavingsAccountRequestView>();

        async Task GetRequestDetails(BranchRequestView RequestDetails)
        {
            string url = "http://examsign-001-site4.itempurl.com/api/GetRequests/Branch";
            var json = JsonConvert.SerializeObject(RequestDetails);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            HttpClient Client = new HttpClient();
            HttpResponseMessage Response = new HttpResponseMessage();
            Response = await Client.PostAsync(url, stringContent);

            if (Response.IsSuccessStatusCode)
            {
                var result = await Response.Content.ReadAsStringAsync();
                var status = JsonConvert.DeserializeObject<List<SavingsAccountRequestView>>(result);
                if (status != null)
                {
                    RequestDetailsList = status;
                }
                else
                {
                    System.Windows.MessageBox.Show("No Data Found");
                }
            }
            else
            {
                System.Windows.MessageBox.Show(Response.StatusCode.ToString());
            }
        }
        public class BranchRequestView
        {
            public string BranchID { get; set; }
            public int StatusCode { get; set; }
        }
    }

    public class HimarkResultExcelModel
    {
        public int SNo { get; set; }
       
        public bool IsRecommend
        {
            get;set;
        }
       
        public string RequestID
        {
            get;set;
        }
       
        public string AadharNumber
        {
            get;set;
        }
        public string Name { get; set; }
        public int EligibleLoanAmount { get; set; }
        public string Status { get; set; }
        public string HiMarkRemark { get; set; }
        public int ActiveUnsecureLoan { get; set; }
        public int ActiveUnsecureLoanin6Months { get; set; }
        public int OutstandingAmount { get; set; }
        public string DPDSummary { get; set; }
        public string HIMarkScore { get; set; }
        public string ScoreCommend { get; set; }
        private string _bname;
        public string BranchName { get; set; }
        public string BName
        {
            get
            {
                return _bname;
            }
            set
            {
                string id = value;
                _bname = MainWindow.BasicDetails.BranchList.Where(temp => temp.BranchName == id).Select(temp => temp.BranchId).FirstOrDefault();
                BranchName = id;
            }
        }
        public string FOName { get; set; }
        public string CustomerName { get; set; }
        public string GroupName { get; set; }
        public DateTime ReportDate { get; set; }
        public int DPDAmount { get; set; }
        public string FileName { get; set; }
        public string ReportID { get; set; }
        public string CustomerID { get; set; }
    }
}
