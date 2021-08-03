﻿using System;
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

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for DashboardBranchManager.xaml
    /// </summary>
    public partial class DashboardBranchManager : Page
    {
        Branch branch = new Branch();
        string BranchId = "01202107002";
        public string LoginBranchID = MainWindow.LoginDesignation.BranchId;
        public List<LoanProcess> loanDetails = new List<LoanProcess>();
        public static List<LoanProcess> RecommenedList = new List<LoanProcess>();
        public List<string> dummylist = new List<string> { "Ashraf Ali", "Safdhar", "Sasi", "Thalif", "Santhosh", "Ashraf Ali", "Safdhar", "Sasi", "Thalif", "Santhosh", "Ashraf Ali", "Safdhar", "Sasi", "Thalif", "Santhosh" };
        LoanProcess loanProcess = new LoanProcess();
        //string BranchId = MainWindow.LoginDesignation.BranchId;
        public DashboardBranchManager()
        {
            InitializeComponent();
            //ManageApprovalNotification();
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
            this.NavigationService.Navigate(new LoanRecommend());
        }

        private void xRecommendCustome_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new CustomerNotification(1));
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

        private void xDailyReportsBtn_Click(object sender, RoutedEventArgs e)
        {
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
                MainWindow.StatusMessageofPage(1, "Excel Export Successfully... Location: D:\\");
            }
            catch
            {

            }
        }
    }
}
