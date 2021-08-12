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

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for DashBoardRegionOfficer.xaml
    /// </summary>
    public partial class DashBoardRegionOfficer : Page
    {
        Branch branch = new Branch();
        // string BranchId = "01202107002";
        public string LoginBranchID = MainWindow.LoginDesignation.BranchId;
        public ObservableCollection<LoanProcess> loanDetails = new ObservableCollection<LoanProcess>();
        public static List<LoanProcess> RecommenedList = new List<LoanProcess>();
        public List<string> dummylist = new List<string> { "Ashraf Ali", "Safdhar", "Sasi", "Thalif", "Santhosh", "Ashraf Ali", "Safdhar", "Sasi", "Thalif", "Santhosh", "Ashraf Ali", "Safdhar", "Sasi", "Thalif", "Santhosh" };
        LoanProcess loanProcess = new LoanProcess();
        public DashBoardRegionOfficer()
        {
            InitializeComponent();
        }

        
        private void xAddCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddCustomer());
        }

        private void xLoanRequestListBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new LoanRecommend());
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
                himarkReport.createHimarkXls();
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
                HimarkResult HMResult = new HimarkResult();
                if (HMResult.IsAlreadyUpload(FileName))
                {
                    HMResult.GetFileDetails(FileFrom);
                    LoanHimarkData(HMResult.himarkResultslist);
                    MainWindow.StatusMessageofPage(1, "File Upload Successfully!...");

                }
                else
                {
                    MainWindow.StatusMessageofPage(0, "This File Already Upload Please Check!...");
                }


            }
        }
        void LoanHimarkData(List<HimarkResult> himarkResultslist)
        {
            HimarkResult himark = new HimarkResult();
            foreach (HimarkResult hm in himarkResultslist)
            {
                himark.InsertHimarkDate(hm);
            }
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
    }
}