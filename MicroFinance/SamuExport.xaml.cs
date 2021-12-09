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
    /// Interaction logic for SamuExport.xaml
    /// </summary>
    public partial class SamuExport : Page
    {
        ObservableCollection<BranchViewModel> Branches = new ObservableCollection<BranchViewModel>();
        List<HimarkRequestView> RequestList = new List<HimarkRequestView>();
        GTtoSamunnati GTtoSAMU = new GTtoSamunnati();
        ObservableCollection<RecommendView> SamuRequestList = new ObservableCollection<RecommendView>();
        ObservableCollection<RecommendView> BindingData = new ObservableCollection<RecommendView>();
        public SamuExport()
        {
            InitializeComponent();

            Branches = MainWindow.BasicDetails.BranchList;
            LoadBranch();
            
            EnrollStartDate.SelectedDate = DateTime.Now;
            EnrollEndDate.SelectedDate = DateTime.Now;
            SelectAllCheck.IsChecked = true;



            SamuRequestList = LoanRepository.GetRecommendListForRM(10);
            LoadBinding();
            RecommendGrid.ItemsSource = BindingData;
            SetSelectedCount();
            
        }
        void LoadBranch()
        {
            BranchViewModel AllBranch = new BranchViewModel { BranchId = "ALL", BranchName = "ALL", RegionId = "ALL" };
            BranchCombo.Items.Add(AllBranch);
            foreach(BranchViewModel branch in Branches)
            {
                BranchCombo.Items.Add(branch);
            }
        }
        

        ObservableCollection<RecommendView> GetDetails()
        {
            ObservableCollection<RecommendView> RequestDetails = new ObservableCollection<RecommendView>();
            RequestDetails = LoanRepository.GetRecommendListForRM(10);
            return RequestDetails;
        }

        void LoadBinding()
        {
            foreach (RecommendView recommend in SamuRequestList)
            {
                BindingData.Add(recommend);
            }
        }

        void CheckAll()
        {
            foreach (RecommendView r in BindingData)
            {
                r.IsRecommend = true;
            }
            SetSelectedCount();
        }
        void UncheckAll()
        {
            foreach (RecommendView r in BindingData)
            {
                r.IsRecommend = false;
            }
            SetSelectedCount();
        }

        bool IsAllcheck()
        {
            foreach (RecommendView r in BindingData)
            {
                if (r.IsRecommend == false)
                {
                    return false;
                }
            }
            return true;
        }



        void ChangeCount(string BranchId)
        {
            int count = 0;
            foreach(HimarkRequestView hm in RequestList)
            {
                if(hm.BranchID==BranchId)
                {
                    count++;
                }
            }
            
        }

        private void SamuCancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DashBoardRegionOfficer());
        }

        private async void SamuExportBtn_Click(object sender, RoutedEventArgs e)
        {
            List<string> RecommendList = BindingData.Where(temp => temp.IsRecommend == true).Select(temp => temp.CustomerID).ToList();
            if(RecommendList.Count!=0)
            {
                GifPanel.Visibility = Visibility.Visible;
                try
                {
                    await System.Threading.Tasks.Task.Run(() => GenerateSamuFile());
                    GifPanel.Visibility = Visibility.Collapsed;
                    this.NavigationService.Navigate(new DashBoardRegionOfficer());
                }
                catch
                {
                    MainWindow.StatusMessageofPage(0, "Error...");
                    GifPanel.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                MessageBox.Show("No Data Selected!", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            

        }
        void GenerateSamuFile()
        {
            try
            {
                List<string> RecommendList = BindingData.Where(temp => temp.IsRecommend == true).Select(temp => temp.CustomerID).ToList();
                List<string> RequestIds = BindingData.Where(temp => temp.IsRecommend == true).Select(temp => temp.RequestID).ToList();
                
                GTtoSAMU.GenerateSamunnati_File(FilterData(BindingData));
                HimarkRepository.UpdateStatusToExportExcel(RequestIds, 11);
                string EmpID =string.IsNullOrEmpty(MainWindow.LoginDesignation.EmpId)?"ADMIN":MainWindow.LoginDesignation.EmpId;
                LoanRepository.InsertTransaction(RequestIds, EmpID, 11);
                MainWindow.StatusMessageofPage(1, "Excel Generated Successfully!...");
            }
            catch
            {
                MainWindow.StatusMessageofPage(0, "Error...");
                
            }
        }


        ObservableCollection<RecommendView> FilterData(ObservableCollection<RecommendView> recommends)
        {
            ObservableCollection<RecommendView> ResultList = new ObservableCollection<RecommendView>();
            foreach(RecommendView R in recommends)
            {
                if(R.IsRecommend==true)
                {
                    ResultList.Add(R);
                }
            }
            return ResultList;
        }

        private void BranchCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BindingData.Clear();
            BranchViewModel SelectedBranch = BranchCombo.SelectedItem as BranchViewModel;
            if(SelectedBranch!=null)
            {
                if(SelectedBranch.BranchId=="ALL")
                {
                    foreach(RecommendView recommend in SamuRequestList)
                    {
                        BindingData.Add(recommend);
                    }
                }
                else
                {
                    foreach(RecommendView recommend in SamuRequestList)
                    {
                        if(recommend.BranchID==SelectedBranch.BranchId)
                        {
                            BindingData.Add(recommend);
                        }
                    }
                }
            }
            SetSelectedCount();
        }

        private void FilterSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            BindingData.Clear();
            DateTime StartDate = EnrollStartDate.SelectedDate.Value;
            DateTime EndDate = EnrollEndDate.SelectedDate.Value;
            BranchViewModel SelectedBranch = BranchCombo.SelectedItem as BranchViewModel;

            if (StartDate > EndDate || StartDate == null || EndDate == null)
            {
                MessageBox.Show("Enter Valid Date!...", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (BranchCombo.SelectedIndex != 0 && BranchCombo.SelectedIndex!=-1)
                {
                    foreach (RecommendView R in SamuRequestList)
                    {
                        DateTime Date = R.RequestDate.Date;
                        if (R.RequestDate >= StartDate.Date && R.RequestDate <= EndDate.Date && SelectedBranch.BranchId == R.BranchID)
                        {
                            BindingData.Add(R);
                        }
                    }
                }
                else
                {
                    foreach (RecommendView R in SamuRequestList)
                    {
                        DateTime Date = R.RequestDate.Date;
                        if (R.RequestDate >= StartDate.Date && R.RequestDate <= EndDate.Date)
                        {
                            BindingData.Add(R);
                        }
                    }
                }

            }
            SetSelectedCount();
        }

        void SetSelectedCount()
        {
            int Count = 0;
            foreach (RecommendView R in BindingData)
            {
                if (R.IsRecommend == true)
                {
                    Count++;
                }
            }

            SelectedCountText.Text = (Count < 10) ? "0" + Count.ToString() : Count.ToString();
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
            SetSelectedCount();
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
            SetSelectedCount();
        }

        private void RejectBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string RequestID = btn.DataContext.ToString();
            //string Req = btn.Uid.ToString();

            string CustomerName = GetCustomerName(RequestID);
            string Message = "Are You Sure You Want To Reject " + CustomerName + " ";
            MessageBoxResult result = MessageBox.Show(Message, "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (MessageBoxResult.Yes == result)
            {
                LoanRepository.RejectLoan(RequestID);
                string EmpID = string.IsNullOrEmpty(MainWindow.LoginDesignation.EmpId) ? "ADMIN" : MainWindow.LoginDesignation.EmpId;
                LoanRepository.InsertTransaction(RequestID, EmpID, 13);
                this.NavigationService.Navigate(new SamuExport());
            }
        }
        string GetCustomerName(string ReqId)
        {
            string Result = "";
            foreach (RecommendView rm in SamuRequestList)
            {
                if (rm.RequestID == ReqId)
                {
                    Result = rm.CustomerName;
                    break;
                }
            }
            return Result;
        }
    }
}
