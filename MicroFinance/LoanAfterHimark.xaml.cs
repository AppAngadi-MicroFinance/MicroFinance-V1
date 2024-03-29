﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for LoanAfterHimark.xaml
    /// </summary>
    public partial class LoanAfterHimark : Page
    {
        LoanProcess loanprocess = new LoanProcess();
        string BranchID = MainWindow.LoginDesignation.BranchId;
        public List<LoanProcess> RecommendList = new List<LoanProcess>();
        public List<LoanProcess> SelectedCustomerList = new List<LoanProcess>();
        public LoanAfterHimark()
        {
            InitializeComponent();
            loanprocess.GetLoanDetailList(BranchID,8);
            RecommendList.Clear();
            RecommendList = loanprocess.LoanProcessList;
            LoadData();
            setCount();
            setAmount();
            BulkRecommendBtn.Visibility = Visibility.Collapsed;
        }

        void LoadData()
        {
            Custlist.Items.Clear();
            foreach(LoanProcess lp in RecommendList)
            {
                Custlist.Items.Add(lp);
            }
        }

        void RemoveItem(string ID)
        {
            Custlist.Items.Clear();
            foreach (LoanProcess lp in RecommendList)
            {
                if(lp.LoanRequestID.Equals(ID)==true)
                {
                    RecommendList.Remove(lp);
                }
                else
                {
                    Custlist.Items.Add(lp);
                }
                
            }
        }
        void setCount()
        {
            int count1 = 0;
            int count2 = 0;
            foreach (LoanProcess c in RecommendList)
            {

                if (c.LoanType == "General Loan"||c.LoanType=="General")
                {
                    count1++;
                }
                else if (c.LoanType == "Special")
                {
                    count2++;
                }
            }
            GeneralLoanCount.Text = count1.ToString();
            SpecialLoanCount.Text = count2.ToString();
        }
        void setAmount()
        {
            int Amount1 = 0;
            int Amount2 = 0;
            foreach (LoanProcess c in RecommendList)
            {
                Amount1 += c.LoanAmount;
            }
            LoanAmountValue.Text = Amount1.ToString();
            BacklogValue.Text = Amount2.ToString();
        }

        private void Custlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoanProcess SelectedCustomer = Custlist.SelectedItem as LoanProcess;
            if (SelectedCustomerList.Contains(SelectedCustomer) == true)
            {
                SelectedCustomerList.Remove(SelectedCustomer);
                SelectedCustomersView.Items.Refresh();
                SelectedCustomersView.ItemsSource = SelectedCustomerList;
                if (SelectedCustomerList.Count > 0)
                    BulkRecommendBtn.Visibility = Visibility.Visible;
                else
                    BulkRecommendBtn.Visibility = Visibility.Collapsed;

            }
            else
            {
                SelectedCustomerList.Add(SelectedCustomer);
                //RecommededcustomerList.Items.Clear();
                SelectedCustomersView.ItemsSource = SelectedCustomerList;
                SelectedCustomersView.Items.Refresh();
                if (SelectedCustomerList.Count > 0)
                    BulkRecommendBtn.Visibility = Visibility.Visible;
                else
                    BulkRecommendBtn.Visibility = Visibility.Collapsed;

            }
        }

        private void ApproveLoanBtn_Click(object sender, RoutedEventArgs e)
        {
            LoanProcess loan = new LoanProcess();
            Button btn = sender as Button;
            string ID = btn.Uid.ToString();
            Custlist.Items.Refresh();
            if(loan.IsAlreadyApproved(ID))
            {
                MainWindow.StatusMessageofPage(1, "Loan Already Approved!...");
            }
            else
            {
                loan = GetRecommendDetails(ID);
                string ApprovedBy = MainWindow.LoginDesignation.EmpId;
                loan.ApprovedBy = ApprovedBy;
                loan.RequestApproval(ID);
                RemoveItem(ID);
                MainWindow.StatusMessageofPage(1, "Loan Approved Successfully...");
            }
            
            
        }

        public LoanProcess GetRecommendDetails(string ID)
        {
            LoanProcess selectedLoan = new LoanProcess();
            int i = 0;
            foreach(LoanProcess process in RecommendList)
            {
                if(process.LoanRequestID==ID)
                {
                    selectedLoan = RecommendList.ElementAt(i);
                }
                else
                {
                    i++;
                }
            }
            return selectedLoan;
        }

        private void RejectResonOKBtn_Click(object sender, RoutedEventArgs e)
        {
            LoanProcess loan = new LoanProcess();
            Button button = sender as Button;
            string ID = button.Uid.ToString();
            loan = GetRecommendDetails(ID);
            string ApprovedBy = MainWindow.LoginDesignation.EmpId;
            loan.ApprovedBy = ApprovedBy;
            loan.RejectLoan(ID);
            Custlist.Items.Refresh();
        }

        private void xBackwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
                this.NavigationService.GoBack();
        }

        private void BulkRecommendBtn_Click(object sender, RoutedEventArgs e)
        {
            int Count = 0;
            foreach(LoanProcess lp in SelectedCustomerList)
            {
                loanprocess.ChangeLoanStatus(lp.LoanRequestID, 9);
                Count++;
            }
            MainWindow.StatusMessageofPage(1, Count.ToString() + " Loan(s) Recommend Successfully!...");
            this.NavigationService.Navigate(new DashBoardHeadOfficer());
        }
    }
}
