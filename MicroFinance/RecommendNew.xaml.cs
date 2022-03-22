using System;
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
using MicroFinance.Reports;
using MicroFinance.ViewModel;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for RecommendNew.xaml
    /// </summary>
    public partial class RecommendNew : Page
    {
        public static int CurrentStatus = 0;
        LoanProcess loanprocess = new LoanProcess();
        List<LoanProcess> NeftList = new List<LoanProcess>();
        NEFT neft = new NEFT();
        ObservableCollection<RecommendView> RecommendList = new ObservableCollection<RecommendView>();
        ObservableCollection<BranchViewModel> BranchList = new ObservableCollection<BranchViewModel>();

        ObservableCollection<RecommendView> BindingList = new ObservableCollection<RecommendView>();
        public RecommendNew()
        {
            InitializeComponent();
        }
        public RecommendNew(int statusCode)
        {
            InitializeComponent();
            EnrollStartDate.SelectedDate = DateTime.Today;
            EnrollEndDate.SelectedDate = DateTime.Today;
            SelectAllCheck.IsChecked = true;
           
            CurrentStatus = statusCode;
            if(statusCode==11)
            {
                TileText.Text = "Loan Approval";
                RecommendLoanBtn.Content = "Approve";
                RecommendList = LoanRepository.GetRecommendList(statusCode,true);
                BranchList = EmployeeRepository.GetBranches();
                BranchCombo.ItemsSource = BranchList;
            }
            else if(CurrentStatus==12)
            {
                RecommendList = LoanRepository.GetApproveList(statusCode, true);
                RecommendPanel.Visibility = Visibility.Collapsed;
                ReportPanel.Visibility = Visibility.Visible;
                loanprocess.GetLoanDetailList(12);
                NeftList = loanprocess.LoanProcessList;
                BranchList = EmployeeRepository.GetBranches();
                BranchCombo.ItemsSource = BranchList;
            }
            else if(CurrentStatus==9)
            {
                RecommendList = LoanRepository.GetRecommendListForRM(statusCode);
                HMRefNumber.Visibility = Visibility.Visible;
                BranchList = EmployeeRepository.GetBranches();
                BranchCombo.ItemsSource = BranchList;
            }
            else
            {
                RecommendList = LoanRepository.GetRecommendList(statusCode);
                BranchList = EmployeeRepository.GetBranches();
                BranchCombo.ItemsSource = BranchList;
            }
            BindingLoad();
            RecommendGrid.ItemsSource = BindingList;
            SelectedCountText.Text = BindingList.Count.ToString();
            
            RecommendGrid.Items.Refresh();

            if(MainWindow.LoginDesignation.LoginDesignation=="Manager")
            {
                BranchNamePanel.Visibility = Visibility.Collapsed;
            }

        }
        public RecommendNew(int statusCode,ObservableCollection<RecommendView> RecDataList)
        {
            InitializeComponent();
            EnrollStartDate.SelectedDate = DateTime.Today;
            EnrollEndDate.SelectedDate = DateTime.Today;
            SelectAllCheck.IsChecked = true;

            CurrentStatus = statusCode;
            if (statusCode == 11)
            {
                TileText.Text = "Loan Approval";
                RecommendLoanBtn.Content = "Approve";
                RecommendList = LoanRepository.GetRecommendList(statusCode, true);
                BranchList = EmployeeRepository.GetBranches();
                BranchCombo.ItemsSource = BranchList;
            }
            else if (CurrentStatus == 12)
            {
                RecommendList = LoanRepository.GetApproveList(statusCode, true);
                RecommendPanel.Visibility = Visibility.Collapsed;
                ReportPanel.Visibility = Visibility.Visible;
                loanprocess.GetLoanDetailList(12);
                NeftList = loanprocess.LoanProcessList;
                BranchList = EmployeeRepository.GetBranches();
                BranchCombo.ItemsSource = BranchList;
            }
            else if (CurrentStatus == 9)
            {
                RecommendList = RecDataList;
                HMRefNumber.Visibility = Visibility.Visible;
                BranchList = MainWindow.BasicDetails.BranchList;
                BranchCombo.ItemsSource = BranchList;
            }
            else
            {
                RecommendList = RecDataList;
                BranchList = MainWindow.BasicDetails.BranchList;
                BranchCombo.ItemsSource = BranchList;
            }
            BindingLoad();
            RecommendGrid.ItemsSource = BindingList;
            SelectedCountText.Text = BindingList.Count.ToString();

            RecommendGrid.Items.Refresh();

            if (MainWindow.LoginDesignation.LoginDesignation == "Manager")
            {
                BranchNamePanel.Visibility = Visibility.Collapsed;
            }

        }


        void SetSelectedCount()
        {
            int Count = 0;
            foreach(RecommendView R in BindingList)
            {
                if(R.IsRecommend==true)
                {
                    Count++;
                }
            }

            SelectedCountText.Text = (Count < 10) ? "0" + Count.ToString() : Count.ToString();
        }

        void BindingLoad()
        {
            foreach(RecommendView RV in RecommendList)
            {
                BindingList.Add(RV);
            }
        }


        List<LoanProcess> FilterFinalList(List<LoanProcess> loans)
        {
            List<LoanProcess> FilterList = new List<LoanProcess>();

            foreach(LoanProcess lp in loans)
            {
                foreach(RecommendView rm in BindingList)
                {
                    if(rm.RequestID==lp.LoanRequestID)
                    {
                        if(rm.IsRecommend==true)
                        {
                            FilterList.Add(lp);
                            break;
                        }
                        
                    }
                }
            }
            return FilterList; 
        }

        void GenerateNEFTFile(List<LoanProcess> FinalList)
        {
            try
            {
                List<LoanProcess> FilterData = FilterFinalList(FinalList);
                List<string> RequestIDList = FilterData.Select(temp => temp.LoanRequestID).ToList();
                string EmpID = string.IsNullOrEmpty(MainWindow.LoginDesignation.EmpId) ? "ADMIN" : MainWindow.LoginDesignation.EmpId;
                neft.GenerateNEFT_File(FilterData);
                LoanRepository.RecommendLoans(RecommendList, CurrentStatus + 2);
                LoanRepository.InsertTransaction(RequestIDList, EmpID, CurrentStatus + 2);
                MainWindow.StatusMessageofPage(1, "Excel Generated Successfully!...");
            }
            catch (Exception ex)
            {
                MainWindow.StatusMessageofPage(0, "Error!");

            }
        }

        private void SelectAll_CheckBox_Click(object sender, RoutedEventArgs e)
        {

        }

        void ApproveLoans(List<string> ReqList)
        {
            LoanRepository.ChangeLoanStatus(ReqList, CurrentStatus + 1);
            LoanRepository.InsertTransaction(ReqList, MainWindow.LoginDesignation.EmpId, CurrentStatus + 1);
            LoanRepository.ApproveLoans(BindingList);
        }

        private async void RecommendLoanBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurrentStatus == 11)
                {
                    List<string> RequestIDList = BindingList.Where(temp => temp.IsRecommend == true).Select(temp => temp.RequestID).ToList();
                    if (RequestIDList.Count != 0)
                    {
                        GifPanel.Visibility = Visibility.Visible;
                        RecommendPanel.IsEnabled = false;
                        await System.Threading.Tasks.Task.Run(() => ApproveLoans(RequestIDList));
                        GifPanel.Visibility = Visibility.Collapsed;
                        RecommendPanel.IsEnabled = true;
                        MainWindow.StatusMessageofPage(1, RequestIDList.Count.ToString() + "Loan(s) Approved Successfully!...");
                        this.NavigationService.Navigate(new RecommendNew(CurrentStatus));
                    }
                    else
                    {
                        MessageBox.Show("No Record Selected", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }
                else
                {
                    List<string> RequestIDList = BindingList.Where(temp => temp.IsRecommend == true).Select(temp => temp.RequestID).ToList();
                    if (RequestIDList.Count != 0)
                    {
                        GifPanel.Visibility = Visibility.Visible;
                        RecommendPanel.IsEnabled = false;
                        await System.Threading.Tasks.Task.Run(() => ChangeLoanStatusAndAddTransaction(RequestIDList));
                        GifPanel.Visibility = Visibility.Collapsed;
                        RecommendPanel.IsEnabled = true;
                        MainWindow.StatusMessageofPage(1, RequestIDList.Count.ToString() + "Loan(s) Approved Successfully!...");
                        this.NavigationService.Navigate(new RecommendNew(CurrentStatus));
                    }
                    else
                    {
                        MessageBox.Show("No Record Selected", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                }
            }
            catch (Exception ex)
            {
                RecommendPanel.IsEnabled = true;
                GifPanel.Visibility = Visibility.Collapsed;
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
            
        }

        void ChangeLoanStatusAndAddTransaction(List<string> ReqList)
        {
            LoanRepository.ChangeLoanStatus(ReqList, CurrentStatus + 1);
            LoanRepository.InsertTransaction(ReqList, MainWindow.LoginDesignation.EmpId, CurrentStatus + 1);
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            if(CurrentStatus==8)
            {
                this.NavigationService.Navigate(new DashboardBranchManager());
            }
            else if (CurrentStatus == 9)
            {
                this.NavigationService.Navigate(new DashBoardRegionOfficer());
            }
            else if (CurrentStatus == 11 && CurrentStatus==12)
            {
                this.NavigationService.Navigate(new DashBoardHeadOfficer());
            }
            else
            {
                this.NavigationService.Navigate(new DashBoardHeadOfficer());
            }
        }

        private void RejectBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string RequestID = btn.DataContext.ToString();
            //string Req = btn.Uid.ToString();

            string CustomerName = GetCustomerName(RequestID);
            string Message = "Are You Sure You Want To Reject " + CustomerName + " ";
           MessageBoxResult result=  MessageBox.Show(Message, "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if(MessageBoxResult.Yes==result)
            {
                LoanRepository.RejectLoan(RequestID);
                this.NavigationService.Navigate(new RecommendNew(CurrentStatus));
            }
        }


        string GetCustomerName(string ReqId)
        {
            string Result = "";
            foreach(RecommendView rm in RecommendList)
            {
                if(rm.RequestID==ReqId)
                {
                    Result = rm.CustomerName;
                    break;
                }
            }
            return Result;
        }

        private void reportCancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DashBoardHeadOfficer());
        }

        private async void GenerateNEFTBtn_Click(object sender, RoutedEventArgs e)
        {
            GifPanel.Visibility = Visibility.Visible;
            await System.Threading.Tasks.Task.Run(() => GenerateNEFTFile(NeftList));
            GifPanel.Visibility = Visibility.Collapsed;
            this.NavigationService.Navigate(new DashBoardHeadOfficer());
        }

        private void FilterSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            BindingList.Clear();
            DateTime StartDate = EnrollStartDate.SelectedDate.Value;
            DateTime EndDate = EnrollEndDate.SelectedDate.Value;
            BranchViewModel SelectedBranch = BranchCombo.SelectedItem as BranchViewModel;

            if(StartDate>EndDate || StartDate==null || EndDate==null)
            {
                MessageBox.Show("Enter Valid Date!...", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            { 
                if(BranchCombo.SelectedIndex!=-1)
                {
                    foreach (RecommendView R in RecommendList)
                    {
                        DateTime Date = R.RequestDate.Date;
                        if (R.RequestDate >= StartDate.Date && R.RequestDate <= EndDate.Date && SelectedBranch.BranchId == R.BranchID)
                        {
                            BindingList.Add(R);
                        }
                    }
                }
                else
                {
                    foreach (RecommendView R in RecommendList)
                    {
                        DateTime Date = R.RequestDate.Date;
                        if (R.RequestDate >= StartDate.Date && R.RequestDate <= EndDate.Date)
                        {
                            BindingList.Add(R);
                        }
                    }
                }
                
            }
            SetSelectedCount();

        }

        private void BranchCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //BranchViewModel SelectedBranch = BranchCombo.SelectedItem as BranchViewModel;
            //ObservableCollection<RecommendView> CurrentList = LoadCurrent(BindingList);


            //BindingList.Clear();
            //foreach(RecommendView R in CurrentList)
            //{
            //    if(SelectedBranch.BranchId==R.BranchID)
            //    {
            //        BindingList.Add(R);
            //    }
            //}

        }

        public static ObservableCollection<RecommendView> LoadCurrent(ObservableCollection<RecommendView> BindingList)
        {
            ObservableCollection<RecommendView> ResultList = new ObservableCollection<RecommendView>();

            foreach(RecommendView r in BindingList)
            {
                ResultList.Add(r);
            }
            return ResultList;
        }

        private void SelectAllCheck_Click(object sender, RoutedEventArgs e)
        {
            if(SelectAllCheck.IsChecked==true)
            {
                CheckAll();
            }
            else if(SelectAllCheck.IsChecked==false)
            {
                UncheckAll();
            }
            SetSelectedCount();
        }


        void CheckAll()
        {
            foreach(RecommendView r in BindingList)
            {
                r.IsRecommend = true;
            }
            SetSelectedCount();
        }
        void UncheckAll()
        {
            foreach (RecommendView r in BindingList)
            {
                r.IsRecommend = false;
            }
            SetSelectedCount();
        }

        bool IsAllcheck()
        {
            foreach(RecommendView r in BindingList)
            {
                if(r.IsRecommend==false)
                {
                    return false;
                }
            }
            return true;
        }

        private void IndividualCheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox check = sender as CheckBox;
            if(check.IsChecked==true)
            {
                if(IsAllcheck())
                {
                    SelectAllCheck.IsChecked = true;
                }
            }
            else if(check.IsChecked==false)
            {
                SelectAllCheck.IsChecked = false;
            }
            SetSelectedCount();
        }

        private void BulkRejectBtn_Click(object sender, RoutedEventArgs e)
        {
            List<string> RequestIDList = BindingList.Where(temp => temp.IsRecommend == true).Select(temp => temp.RequestID).ToList();
            if(RequestIDList.Count!=0)
            {
                LoanRepository.ChangeLoanStatus(RequestIDList, 13);
                LoanRepository.InsertTransaction(RequestIDList, MainWindow.LoginDesignation.EmpId,13);
                MainWindow.StatusMessageofPage(1, RequestIDList.Count.ToString() + "Loan(s) Rejected Successfully!...");
                this.NavigationService.Navigate(new RecommendNew(CurrentStatus));
            }
            else
            {
                MessageBox.Show("No Record Selected", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            
        }
    }
}
