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
using MicroFinance.ViewModel;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for RecommendNew.xaml
    /// </summary>
    public partial class RecommendNew : Page
    {
        public static int CurrentStatus = 0;
        ObservableCollection<RecommendView> RecommendList = new ObservableCollection<RecommendView>();
        public RecommendNew()
        {
            InitializeComponent();
        }
        public RecommendNew(int statusCode)
        {
            InitializeComponent();
            RecommendList = LoanRepository.GetRecommendList(statusCode);
            RecommendGrid.ItemsSource = RecommendList;
            CurrentStatus = statusCode;

        }

        private void SelectAll_CheckBox_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RecommendLoanBtn_Click(object sender, RoutedEventArgs e)
        {
            int count = LoanRepository.RecommendLoans(RecommendList, CurrentStatus+1);
            MainWindow.StatusMessageofPage(1, count.ToString() + "Loan(s) Approved Successfully!...");
            this.NavigationService.Navigate(new RecommendNew(CurrentStatus));
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            if(CurrentStatus==8)
            {
                this.NavigationService.Navigate(new DashboardBranchManager());
            }
            if (CurrentStatus == 9)
            {
                this.NavigationService.Navigate(new DashBoardRegionOfficer());
            }
            if (CurrentStatus == 10)
            {
                this.NavigationService.Navigate(new DashBoardRegionOfficer());
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
    }
}
