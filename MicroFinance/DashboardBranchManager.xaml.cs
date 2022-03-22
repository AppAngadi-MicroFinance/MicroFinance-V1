using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
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
using MicroFinance.Repository;
using MicroFinance.ViewModel;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for DashboardBranchManager.xaml
    /// </summary>
    public partial class DashboardBranchManager : Page
    {
        Branch branch = new Branch();
        public string LoginBranchID = MainWindow.LoginDesignation.BranchId;
        public ObservableCollection<LoanProcess> loanDetails = new ObservableCollection<LoanProcess>();
        public static List<LoanProcess> RecommenedList = new List<LoanProcess>();
        LoanProcess loanProcess = new LoanProcess();
        public DashboardBranchManager()
        {
            InitializeComponent();
            EnrollStartDate.SelectedDate = DateTime.Now;
            EnrollEndDate.SelectedDate = DateTime.Now;
            LoadCount();
        }


        void LoadCount()
        {
            string BranchID = MainWindow.LoginDesignation.BranchId;
            xCustomerPendingsAC.Text = NotificationRepository.GetVerifyDocumentNotifyCount(BranchID, 6).ToString();
            xHimarkResultCount.Text = NotificationRepository.GetHimarkResultCount(BranchID).ToString();
            xCustomerPendingsBM.Text = NotificationRepository.GetVerifyDocumentNotifyCount(BranchID, 7).ToString();
            xRecommendLoanCount.Text = NotificationRepository.GetVerifyDocumentNotifyCount(BranchID, 8).ToString();

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
                await System.Threading.Tasks.Task.Run(() => ResultData = GetRecommDetails(8));
                if(ResultData.Count!=0)
                {
                    GifPanel.Visibility = Visibility.Collapsed;
                    this.NavigationService.Navigate(new RecommendNew(8,ResultData));
                }
                else
                {
                    GifPanel.Visibility = Visibility.Collapsed;
                    MessageBox.Show("No Records Found!", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            catch(Exception ex)
            {
                GifPanel.Visibility = Visibility.Collapsed;
                MessageBox.Show(ex.Message);
            }
            
        }

        public ObservableCollection<RecommendView> GetRecommDetails(int Code)
        {
            return LoanRepository.GetRecommendList(Code);
        }

        private void xRecommendCustome_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new CustomerNotification(2));
            this.NavigationService.Navigate(new NotificationPage(7));
        }

        private void xAddSHGBtn_Click(object sender, RoutedEventArgs e)
        {
            // this.NavigationService.Navigate(new AddNewSelfHelpGroup());
            this.NavigationService.Navigate(new CreateSHG());
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


        private void xDailyCollection_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CollectionVerify());
        }

        private void xNotificationBtn_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new CustomerNotification(1));
            this.NavigationService.Navigate(new NotificationPage(6));
        }

        private void HimarkPanelCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            HimarkExportPanel.Visibility = Visibility.Collapsed;
        }

        private void HimarkBtn_Click(object sender, RoutedEventArgs e)
        {
            HimarkExportPanel.Visibility = Visibility.Visible;
            loanProcess = new LoanProcess();
            loanProcess.GetRequestList(LoginBranchID);
            loanDetails = loanProcess.RequestList;
            RequestedListBoxNew.ItemsSource = loanDetails;
        }

        private void ExportHimarkBtn_Click(object sender, RoutedEventArgs e)
        {
            List<HiMark> Himarklist = new List<HiMark>();
            HiMark himarkReport = new HiMark();
            foreach (LoanProcess lp in loanDetails)
            {
                himarkReport = new HiMark(lp);
                Himarklist.Add(himarkReport);
            }
            try
            {
                himarkReport = new HiMark();
                himarkReport.hiMarksList = Himarklist;
               // himarkReport.createHimarkXls();
                MainWindow.StatusMessageofPage(1, "Excel Export Successfully... Location: Doucuments\\Reports\\Hi-Mark Report");
                HimarkExportPanel.Visibility = Visibility.Collapsed;
            }
            catch
            {
                MainWindow.StatusMessageofPage(0, "Error Occured");
            }
        }

        private void ImportHimarkBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDlg = new OpenFileDialog();
            openFileDlg.Filter = "Excel Files |*.xls;*.xlsx;*.xlsm";
            openFileDlg.Title = "Choose File";
            openFileDlg.InitialDirectory = @"C:\";
            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {
                string FileFrom = openFileDlg.FileName;
                var FilePath = FileFrom.Split('\\');
                string FileName = FilePath[FilePath.Length - 1];
                HimarkResultModel HMResult = new HimarkResultModel();
                HimarkResult HmResultList = new HimarkResult();
                if (HimarkResult.IsAlreadyUpload(FileName))
                {
                    List<HimarkResultModel> resultList = new List<HimarkResultModel>();
                    resultList = HmResultList.GetFileDetails(FileFrom);
                   
                   // LoanHimarkData(resultList);
                   
                    MainWindow.StatusMessageofPage(1, "File Upload Successfully!...");

                }
                else
                {
                    MainWindow.StatusMessageofPage(0, "This File Already Upload Please Check!...");
                }
                

            }
        }
        

        private void RequestedListBoxNew_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void HimarkResultBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new HimarkResultData());
        }

        private void xAddNewEmployee_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddEmployee());
        }

        private void xFindCustomer_Click(object sender, RoutedEventArgs e)
        {
            string BranchID = MainWindow.LoginDesignation.BranchId;
            this.NavigationService.Navigate(new CustomerSearch(BranchID));
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
                    await System.Threading.Tasks.Task.Run(() => EnrollDetails = GetEnrollDetails(DateData,BranchId));
                    GifPanel.Visibility = Visibility.Collapsed;


                    if (EnrollDetails.Count == 0)
                    {
                        string Message = string.Format("No Enroll Found Betweeen {0} to {1}.", StartDate.ToString("dd-MMM-yyyy"), EndDate.ToString("dd-MMM-yyyy"));
                        MessageBox.Show(Message, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        this.NavigationService.Navigate(new EnrollDetails(EnrollDetails));
                    }
                }
                else
                {
                    MessageBox.Show("Enter Proper Date!...", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }

            }
        }
        public static List<EnrollDetailsView> GetEnrollDetails(DateModel DateData,string BranchID)
        {
            List<EnrollDetailsView> EnrollDetails = new List<EnrollDetailsView>();
            EnrollDetails = EnrollDetailsRepository.GetEnrollDetails(BranchID,DateData);
            return EnrollDetails;
        }

        private void EnrollCancelBtn_Click(object sender, RoutedEventArgs e)
        {
            MenuPanel.IsEnabled = true;
            EnrollDatailsPanel.Visibility = Visibility.Collapsed;
        }

        private void AssignShedule_Click(object sender, RoutedEventArgs e)
        {
            List<SHGModal> Centers = new List<SHGModal>();
            string BranchId = MainWindow.LoginDesignation.BranchId;
            Centers= SHGRepository.GetCenters(true, BranchId);
            if(Centers.Count!=0)
            {
                this.NavigationService.Navigate(new CenterView(Centers));
            }
            else
            {
                MessageBox.Show("No Un Alloated Center Available", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void EditShedule_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<TimeTable> SheduleList = new ObservableCollection<TimeTable>();
            string BranchID = MainWindow.LoginDesignation.BranchId;
            SheduleList = TimeTableRepository.GetShedules(BranchID);
            if(SheduleList.Count!=0)
            {
                this.NavigationService.Navigate(new EditShedule(SheduleList));
            }
            

            
        }

        private void ViewCenter_Click(object sender, RoutedEventArgs e)
        {
            List<SHGModal> Centers = new List<SHGModal>();
            string BranchId = MainWindow.LoginDesignation.BranchId;
            Centers = SHGRepository.GetCenters(BranchId);
            if (Centers.Count != 0)
            {
                this.NavigationService.Navigate(new CenterView(Centers,1));
            }
            else
            {
                MessageBox.Show("No Center Available", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeactiveLoanApplication_Click(object sender, RoutedEventArgs e)
        {
            FindCustomerPanel.Visibility = Visibility.Visible;
            MenuPanel.IsEnabled = false;
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            if(!string.IsNullOrEmpty(AadharNumberBox.Text))
            {
                string AadharNumber = AadharNumberBox.Text;
                this.NavigationService.Navigate(new LoanCloseView(AadharNumber));
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            FindCustomerPanel.Visibility = Visibility.Collapsed;
            MenuPanel.IsEnabled = true;
        }

        private void CollectionDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DownloadCollectionReport(1));
        }

        private async void SDRecommendBtn_Click(object sender, RoutedEventArgs e)
        {
            BranchRequestView RequestDetails = new BranchRequestView { BranchID = MainWindow.LoginDesignation.BranchId, StatusCode = 1 };
            GifPanel.Visibility = Visibility.Visible;
            MenuPanel.IsEnabled = false;
            await GetRequestDetails(RequestDetails);
            MenuPanel.IsEnabled = true;
            GifPanel.Visibility = Visibility.Collapsed;
            if(RequestDetailsList.Count!=0)
            {
                this.NavigationService.Navigate(new SDRecommendView(RequestDetailsList));
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

            if(Response.IsSuccessStatusCode)
            {
                var result = await Response.Content.ReadAsStringAsync();
                var status = JsonConvert.DeserializeObject<List<SavingsAccountRequestView>>(result);
                if(status!=null)
                {
                    RequestDetailsList = status;
                }
                else
                {
                    MessageBox.Show("No Data Found");
                }
            }
            else
            {
                MessageBox.Show(Response.StatusCode.ToString());
            }
        }


        public class BranchRequestView
        {
            public string BranchID { get; set; }
            public int StatusCode { get; set; }
        }

        private void AddEcpence_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new ExpenseEntry());
        }
    }
}
