﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MicroFinance.Repository;
using MicroFinance.ViewModel;
using Microsoft.Win32;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for DashboardBranchManager.xaml
    /// </summary>
    public partial class DashboardBranchManager : Page
    {
        Branch branch = new Branch();
       // string BranchId = "01202107002";
        public string LoginBranchID = MainWindow.LoginDesignation.BranchId;
        public ObservableCollection<LoanProcess> loanDetails = new ObservableCollection<LoanProcess>();
        public static List<LoanProcess> RecommenedList = new List<LoanProcess>();
        public List<string> dummylist = new List<string> { "Ashraf Ali", "Safdhar", "Sasi", "Thalif", "Santhosh", "Ashraf Ali", "Safdhar", "Sasi", "Thalif", "Santhosh", "Ashraf Ali", "Safdhar", "Sasi", "Thalif", "Santhosh" };
        LoanProcess loanProcess = new LoanProcess();
        //string BranchId = MainWindow.LoginDesignation.BranchId;
        public DashboardBranchManager()
        {
            InitializeComponent();
            //ManageApprovalNotification();
            EnrollStartDate.SelectedDate = DateTime.Now;
            EnrollEndDate.SelectedDate = DateTime.Now;
        }
        //void ManageApprovalNotification()
        //{
        //    int forApprovals = GetCustomersStatus1(BranchId);
        //    if (forApprovals > 0)
        //    {
        //        xCustApprovalsCount.Text = forApprovals.ToString();
        //    }
        //    else
        //    {
        //        xNotificationBatch.Visibility = Visibility.Collapsed;
        //    }
        //}

        private void xAddCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddCustomer());
        }

        private void xLoanRequestListBtn_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new LoanRecommend(8));
            this.NavigationService.Navigate(new RecommendNew(8));
        }

        private void xRecommendCustome_Click(object sender, RoutedEventArgs e)
        {
            //this.NavigationService.Navigate(new CustomerNotification(2));
            this.NavigationService.Navigate(new NotificationPage(7));
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
        //void LoanHimarkData(List<HimarkResultModel> himarkResultslist)
        //{
        //    HimarkResult himark = new HimarkResult();
        //    foreach (HimarkResultModel hm in himarkResultslist)
        //    {
        //        himark.InsertHimarkDate(hm);
        //    }
        //}

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
            GridA.IsEnabled = false;
            GridB.IsEnabled = false;
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
            GridA.IsEnabled = true;
            GridB.IsEnabled = true;
            GridC.IsEnabled = true;
            GridD.IsEnabled = true;
            EnrollDatailsPanel.Visibility = Visibility.Collapsed;
        }
    }
}
