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

        ObservableCollection<RecommendView> BindingList = new ObservableCollection<RecommendView>();
        public RecommendNew()
        {
            InitializeComponent();
        }
        public RecommendNew(int statusCode)
        {
            InitializeComponent();
            
           
            CurrentStatus = statusCode;
            if(statusCode==11)
            {
                TileText.Text = "Loan Approval";
                RecommendLoanBtn.Content = "Approve";
                RecommendList = LoanRepository.GetRecommendList(statusCode,true);
            }
            else if(CurrentStatus==12)
            {
                RecommendList = LoanRepository.GetRecommendList(statusCode, true);
                RecommendPanel.Visibility = Visibility.Collapsed;
                ReportPanel.Visibility = Visibility.Visible;
                loanprocess.GetLoanDetailList(12);
                NeftList = loanprocess.LoanProcessList;
            }
            else
            {
                RecommendList = LoanRepository.GetRecommendList(statusCode);
            }
            BindingLoad();
            RecommendGrid.ItemsSource = BindingList;
            
            RecommendGrid.Items.Refresh();

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
                foreach(RecommendView rm in RecommendList)
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
                neft.GenerateNEFT_File(FilterFinalList(FinalList));
                LoanRepository.RecommendLoans(RecommendList, CurrentStatus + 2);
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

        private void RecommendLoanBtn_Click(object sender, RoutedEventArgs e)
        {
            if(CurrentStatus==11)
            {
                int count = LoanRepository.RecommendLoans(BindingList, CurrentStatus + 1);
                LoanRepository.ApproveLoans(BindingList);
                MainWindow.StatusMessageofPage(1, count.ToString() + "Loan(s) Approved Successfully!...");
                this.NavigationService.Navigate(new RecommendNew(CurrentStatus));

            }
            else
            {
                int count = LoanRepository.RecommendLoans(BindingList, CurrentStatus + 1);
                MainWindow.StatusMessageofPage(1, count.ToString() + "Loan(s) Approved Successfully!...");
                this.NavigationService.Navigate(new RecommendNew(CurrentStatus));
            }
            
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

            if(StartDate>EndDate)
            {
                MessageBox.Show("Enter Valid Date!...", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
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
    }
}
