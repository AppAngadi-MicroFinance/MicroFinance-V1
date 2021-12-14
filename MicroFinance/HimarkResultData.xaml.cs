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
using MicroFinance.ViewModel;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for HimarkResultData.xaml
    /// </summary>
    public partial class HimarkResultData : Page
    {
        string BranchID = MainWindow.LoginDesignation.BranchId;
        HimarkResult HMResult = new HimarkResult();
        List<HimarkResultModel> HMResultList = new List<HimarkResultModel>();
        ObservableCollection<HimarkResultModel> BindingData = new ObservableCollection<HimarkResultModel>();
        List<string> CategoryList = new List<string>();
        LoanProcess loanProcess = new LoanProcess();
        public HimarkResultData()
        {
            InitializeComponent();
            HMResultList = HMResult.GetBranchRequest(BranchID);
            CategoryList = HMResult.CategoryList;
            if (HMResultList.Count != 0)
            {
                CategoryCombo.ItemsSource = CategoryList;
                CategoryCombo.SelectedIndex = 0;
                string selectedvalue = CategoryCombo.SelectedValue as string;
                LoadBinding(selectedvalue);
                RequestDataGrid.ItemsSource = BindingData.OrderByDescending(temp=>temp.ReportDate);
                SelectAllCheck.IsChecked = true;
            }
        }

        void LoadBinding()
        {
            BindingData.Clear();
            foreach (HimarkResultModel Hm in HMResultList)
            {
                BindingData.Add(Hm);
            }
        }
        void LoadBinding(string Status)
        {
            BindingData.Clear();
            foreach (HimarkResultModel Hm in HMResultList)
            {
                if (Hm.Status.Equals(Status))
                {
                    BindingData.Add(Hm);
                }

            }

            RequestDataGrid.ItemsSource = BindingData.OrderByDescending(temp => temp.ReportDate);
            SelectAllCheck.IsChecked = true;
        }


        private void CategoryCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string SelectedValue = CategoryCombo.SelectedItem as string;
            if (SelectedValue != null)
            {
                LoadBinding(SelectedValue);

                

            }


        }


        void SetButtonContent(string Value)
        {
            if (Value.Equals("Accept", StringComparison.CurrentCultureIgnoreCase))
            {
                BulkAcceptBtn.Content = "Accept";
                SolidColorBrush PrimaryColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F77604"));
                BulkAcceptBtn.Background = PrimaryColor;
            }
            else if (Value.Equals("Reject", StringComparison.CurrentCultureIgnoreCase))
            {
                BulkAcceptBtn.Content = "Reject";
                SolidColorBrush SecondaryColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#0EABF2"));
                BulkAcceptBtn.Background = SecondaryColor;
            }
            else if (Value.Equals("Re-verify", StringComparison.CurrentCultureIgnoreCase))
            {
                BulkAcceptBtn.Content = "Re-Verify";
                SolidColorBrush PrimaryColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F77604"));
                BulkAcceptBtn.Background = PrimaryColor;
            }
            else if (Value.Equals("Accept/SecuredDPD", StringComparison.CurrentCultureIgnoreCase))
            {
                BulkAcceptBtn.Content = "Accept/SecureDPD";
                SolidColorBrush PrimaryColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F77604"));
                BulkAcceptBtn.Background = PrimaryColor;
                BulkAcceptBtn.Width = 160;
            }
            else if (Value.Equals("Accept - Partial", StringComparison.CurrentCultureIgnoreCase))
            {
                BulkAcceptBtn.Content = "Reject-Partial";
                SolidColorBrush PrimaryColor = (SolidColorBrush)(new BrushConverter().ConvertFrom("#F77604"));
                BulkAcceptBtn.Background = PrimaryColor;
                BulkAcceptBtn.Width = 160;
            }

        }


        






        private void RetainBtn_Click(object sender, RoutedEventArgs e)
        {
            Button Btn = sender as Button;
            string ID = Btn.Uid.ToString();
            loanProcess.RetainFromHimark(ID);

        }

        private void AcceptBtn_Click(object sender, RoutedEventArgs e)
        {

            Button Btn = sender as Button;
            // HimarkResultList.Items.Refresh();
            string ID = Btn.Uid.ToString();
            loanProcess.ApproveLoanFromHimark(ID);

        }

        private void RejectBtn_Click(object sender, RoutedEventArgs e)
        {
            Button Btn = sender as Button;
            //HimarkResultList.Items.Refresh();
            string ID = Btn.Uid.ToString();
            loanProcess.RejectFromHimark(ID);
        }

        private void BulkAcceptBtn_Click(object sender, RoutedEventArgs e)
        {
            int SelectedCount = BindingData.Where(temp => temp.IsRecommend == true).Select(temp => temp.RequestID).Count();
            if(SelectedCount!=0)
            {
                
                    List<string> RequestList = BindingData.Where(temp => temp.IsRecommend == true).Select(temp => temp.RequestID).ToList();
                    LoanRepository.ChangeLoanStatus(RequestList, 3);
                LoanRepository.InsertTransaction(RequestList, MainWindow.LoginDesignation.EmpId, 3);
                    string Message = "" + RequestList.Count.ToString() + " Loan(s) Accepted with SecureDPD Successfully!...";
                    MainWindow.StatusMessageofPage(1, Message);
                    this.NavigationService.Navigate(new HimarkResultData());
                
            }
            else
            {
                MessageBox.Show("No Record Selected", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            


        }

        private void SelectAllCheck_Click(object sender, RoutedEventArgs e)
        {
            if (SelectAllCheck.IsChecked == true)
            {
                CheckAll();
            }
            else if (SelectAllCheck.IsChecked == false)
            {
                UncheckAll();
            }
        }
        void CheckAll()
        {
            foreach (HimarkResultModel r in BindingData)
            {
                r.IsRecommend = true;
            }

        }
        void UncheckAll()
        {
            foreach (HimarkResultModel r in BindingData)
            {
                r.IsRecommend = false;
            }

        }

        bool IsAllcheck()
        {
            foreach (HimarkResultModel r in BindingData)
            {
                if (r.IsRecommend == false)
                {
                    return false;
                }
            }
            return true;
        }

        private void IndividualCheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox check = sender as CheckBox;
            if (check.IsChecked == true)
            {
                if (IsAllcheck())
                {
                    SelectAllCheck.IsChecked = true;
                }
            }
            else if (check.IsChecked == false)
            {
                SelectAllCheck.IsChecked = false;
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DashboardBranchManager());
        }

        private void IndividualRejectBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string RequestID = btn.DataContext.ToString();
            //string Req = btn.Uid.ToString();

            string CustomerName = GetCustomerName(RequestID);
            string Message = "Are You Sure You Want To Reject " + CustomerName + " ";
            MessageBoxResult result = MessageBox.Show(Message, "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (MessageBoxResult.Yes == result)
            {
                LoanRepository.ChangeLoanStatus(RequestID,4);
                this.NavigationService.Navigate(new HimarkResultData());
            }
        }
        string GetCustomerName(string ReqId)
        {
            string Result = "";
            foreach (HimarkResultModel rm in HMResultList)
            {
                if (rm.RequestID == ReqId)
                {
                    Result = rm.CustomerName;
                    break;
                }
            }
            return Result;
        }

        private void BulkRejectBtn_Click(object sender, RoutedEventArgs e)
        {
            int SelectedCount = BindingData.Where(temp => temp.IsRecommend == true).Select(temp => temp.RequestID).Count();
            if (SelectedCount != 0)
            {

                List<string> RequestList = BindingData.Where(temp => temp.IsRecommend == true).Select(temp => temp.RequestID).ToList();
                LoanRepository.ChangeLoanStatus(RequestList, 4);
                LoanRepository.InsertTransaction(RequestList, MainWindow.LoginDesignation.EmpId, 4);
                string Message = "" + RequestList.Count.ToString() + " Loan(s) Accepted with SecureDPD Successfully!...";
                MainWindow.StatusMessageofPage(1, Message);
                this.NavigationService.Navigate(new HimarkResultData());

            }
            else
            {
                MessageBox.Show("No Record Selected", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
