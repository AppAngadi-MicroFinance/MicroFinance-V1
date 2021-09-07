using System;
using System.Collections.Generic;
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
    /// Interaction logic for HimarkResultData.xaml
    /// </summary>
    public partial class HimarkResultData : Page
    {
        string BranchID = MainWindow.LoginDesignation.BranchId;
        HimarkResult HMResult = new HimarkResult();
        List<HimarkResultModel> HMResultList = new List<HimarkResultModel>();
        List<string> CategoryList = new List<string>();
        LoanProcess loanProcess = new LoanProcess();
        public HimarkResultData()
        {
            InitializeComponent();
            HMResultList= HMResult.GetBranchRequest(BranchID);
            CategoryList = HMResult.CategoryList;
            if (CategoryList.Count()!=0)
            {
                CategoryCombo.ItemsSource = CategoryList;
                BulkAcceptBtn.Visibility = Visibility.Collapsed;
                BulkRejectBtn.Visibility = Visibility.Collapsed;
                CategoryCombo.SelectedIndex = 0;
                string selectedvalue = CategoryCombo.SelectedValue as string;
                UpdateData(selectedvalue);
                buttonVisibility(selectedvalue);
            }
            else
            {
                HimarkResultList.Visibility = Visibility.Collapsed;
                StatusText.Visibility = Visibility.Collapsed;
                CategoryCombo.Visibility = Visibility.Collapsed;
                CategoryText.Visibility = Visibility.Collapsed;
                BulkAcceptBtn.Visibility = Visibility.Collapsed;
                BulkRejectBtn.Visibility = Visibility.Collapsed;
                ResultViewPanel.Visibility = Visibility.Collapsed;
                NotifyText.Visibility = Visibility.Visible;
            }
           

        }
        void buttonVisibility(string value)
        {
            if(value.Equals("Accept"))
            {
                BulkAcceptBtn.Visibility = Visibility.Visible;
                BulkRejectBtn.Visibility = Visibility.Collapsed;
            }
            else if(value.Equals("Reject"))
            {
                BulkAcceptBtn.Visibility = Visibility.Collapsed;
                BulkRejectBtn.Visibility = Visibility.Visible;
            }
        }

        //void LoanHimarkData(List<HimarkResultModel> himarkResultslist)
        //{
        //    foreach(HimarkResultModel hm in himarkResultslist)
        //    {
        //        HMResult.InsertHimarkDate(hm);
        //    }
        //}

        private void CategoryCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string SelectedValue = CategoryCombo.SelectedItem as string;
            StatusText.Text = SelectedValue.ToUpper() + "ED CUSTOMERS";
            UpdateData(SelectedValue);
            buttonVisibility(SelectedValue);
        }

        void UpdateData(string CategoryType)
        {
            HimarkResultList.Items.Clear();
            foreach (HimarkResultModel hm in HMResultList)
            {
                if(hm.Status.Equals(CategoryType,StringComparison.CurrentCultureIgnoreCase))
                {
                    HimarkResultList.Items.Add(hm);
                }
            }
        }


        void RemoveItem(string UId)
        {
            HimarkResultList.Items.Clear();
            RemoveItemFromLsit(UId);
            foreach(HimarkResultModel hm in HMResultList)
            { 
                    HimarkResultList.Items.Add(hm);  
            }
        }

        void RemoveItemFromLsit(string UId)
        {
            foreach (HimarkResultModel hm in HMResultList)
            {
                if (hm.RequestID.Equals(UId) == true)
                {
                    HMResultList.Remove(hm);
                    break;
                }
            }
        }
        private void RetainBtn_Click(object sender, RoutedEventArgs e)
        {
            Button Btn = sender as Button;
            HimarkResultList.Items.Refresh();
            string ID = Btn.Uid.ToString();
            loanProcess.RetainFromHimark(ID);
            RemoveItem(ID);
        }

        private void AcceptBtn_Click(object sender, RoutedEventArgs e)
        {
            
            Button Btn = sender as Button;
            HimarkResultList.Items.Refresh();
            string ID = Btn.Uid.ToString();
            loanProcess.ApproveLoanFromHimark(ID);
            RemoveItem(ID);
        }

        private void RejectBtn_Click(object sender, RoutedEventArgs e)
        {
            Button Btn = sender as Button;
            HimarkResultList.Items.Refresh();
            string ID = Btn.Uid.ToString();
            loanProcess.RejectFromHimark(ID);
        }

        private void BulkAcceptBtn_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            foreach(HimarkResultModel hm in HMResultList)
            {
                if(hm.Status.Equals("Accept"))
                {
                    loanProcess.ChangeLoanStatus(hm.RequestID, 2);
                    count++;
                }
            }
            string Message = "" + count.ToString() + " Loan Approved Successfully!...";
            MainWindow.StatusMessageofPage(1, Message);
            this.NavigationService.Navigate(new HimarkResultData());

        }

        private void BulkRejectBtn_Click(object sender, RoutedEventArgs e)
        {

            foreach (HimarkResultModel hm in HMResultList)
            {
                if (hm.Status.Equals("Reject"))
                {
                    loanProcess.ChangeLoanStatus(hm.RequestID, 3);
                }
            }
            this.NavigationService.Navigate(new HimarkResultData());
        }
    }
}
