﻿using System;
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
        }

        
        private void xAddCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddCustomer());
        }

        private void xLoanRequestListBtn_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new LoanRecommend(9));
            this.NavigationService.Navigate(new RecommendNew(9));
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

        }

        private void xFindCustomer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void xFindEmployee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void xAllowanceReportBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BranchCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void xAddNewBranch_Click(object sender, RoutedEventArgs e)
        {

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
            GridC.IsEnabled = false;
            GridD.IsEnabled = false;
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
                    DateData.EndDate = EndDate;
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
            GridC.IsEnabled = true;
            GridD.IsEnabled = true;
            EnrollDatailsPanel.Visibility = Visibility.Collapsed;
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
               // _bname = MainWindow.BasicDetails.BranchList.Where(temp => temp.BranchName == id).Select(temp => temp.BranchId).FirstOrDefault();
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
