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
using MicroFinance.Modal;
using MicroFinance.Reports;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for ExportHimarkReport.xaml
    /// </summary>
    public partial class ExportHimarkReport : Page
    {
        List<BranchNameView> BranchList = new List<BranchNameView>();
        public static List<HimarkRequestView> RequestList = new List<HimarkRequestView>();
        ObservableCollection<HimarkRequestView> ORequestList = new ObservableCollection<HimarkRequestView>();
        ObservableCollection<HimarkRequestView> BindingRequest = new ObservableCollection<HimarkRequestView>();
        public ExportHimarkReport()
        {
            InitializeComponent();
            BranchList = CollectionReportRepo.GetBranchNames();
            //BranchNameCombo.ItemsSource = BranchList;
            LoadBranch();
            RequestList = HimarkRepository.GetHimarkRequestList();
            LoadData();
            RequestListDataGrid.ItemsSource = BindingRequest;
            EnrollStartDate.SelectedDate = DateTime.Now;
            EnrollEndDate.SelectedDate = DateTime.Now;
        }

        void LoadBranch()
        {
            BranchNameCombo.Items.Clear();
            BranchNameCombo.Items.Add(new BranchNameView { BranchId = "ALL", BranchName = "ALL" });
            foreach (BranchNameView branch in BranchList)
            {
                BranchNameCombo.Items.Add(branch);
            }
            
        }
        void LoadData()
        {
            foreach(HimarkRequestView hm in RequestList)
            {
                ORequestList.Add(hm);
                BindingRequest.Add(hm);
                OverAllRequestCount.Text = ORequestList.Count().ToString();
            }
        }
        void LoadData(string Bid)
        {
            BindingRequest.Clear();
            foreach (HimarkRequestView hm in RequestList)
            {
                if(hm.BranchID==Bid)
                {
                    BindingRequest.Add(hm);
                }
              
            }
            //branchtotalcount.Text = BindingRequest.Count().ToString();
        }
        int GetCount(string Bid)
        {
            int Count = 0;
            BindingRequest.Clear();
            foreach (HimarkRequestView hm in RequestList)
            {
                if (hm.BranchID == Bid)
                {
                    Count++;
                }

            }
            return Count;
        }

        private void BranchNameCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BranchNameView selectedBranch = BranchNameCombo.SelectedValue as BranchNameView;
            // LoadData(selectedBranch.BranchId);
           string branchtotal= GetCount(selectedBranch.BranchId).ToString();
            if (BindingRequest.Count==0)
            {
                //MessageBox.Show("No Request in Branch");
            }
            //branchtotalcount.Text = branchtotal;
        }


        void RetainCustomer(string requestid)
        {
            foreach(HimarkRequestView hm in RequestList)
            {
                if(hm.RequestID==requestid)
                {
                    ORequestList.Remove(hm);
                    RequestList.Remove(hm);
                    break;
                }
            }
        }

        private void RetainCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string ID = btn.Uid.ToString();
            string name = btn.DataContext.ToString();
            MessageBoxResult result =(MessageBox.Show("Are You Sure You Want To Remove the Customer", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Information));
            if(result==MessageBoxResult.Yes)
            {
                HimarkRepository.ChangeLoanStatus(name, 4);
                LoanRepository.InsertTransaction(name, MainWindow.LoginDesignation.EmpId,4);
                RetainCustomer(name);
            }
        }

        void ExportHimarkFile()
        {

            List<HimarkModel> HimarkList = new List<HimarkModel>();
            List<HimarkRequestView> FilterData = FilterList(RequestList);
            HimarkList = HimarkRepository.GetDetailsForReport(FilterData);
            List<string> RequestIDList = FilterData.Select(temp => temp.RequestID).ToList();
            HiMark himarkReport = new HiMark();
            try
            {
                himarkReport = new HiMark();
                himarkReport.createHimarkXls1(HimarkList);
                HimarkRepository.UpdateStatusToExportExcel(FilterData,2);
                LoanRepository.InsertTransaction(RequestIDList, MainWindow.LoginDesignation.EmpId, 2);
                MainWindow.StatusMessageofPage(1, "File Exported Successfully!...");
            }
            catch (Exception ex)
            {
                MainWindow.StatusMessageofPage(0, ex.Message);
            }
        }


        List<HimarkRequestView> FilterList(List<HimarkRequestView> RequestList)
        {
            List<HimarkRequestView> FilterData = new List<HimarkRequestView>();

            foreach(HimarkRequestView hm in RequestList)
            {
                foreach(HimarkRequestView R in BindingRequest)
                {
                    if(hm.CustomerID==R.CustomerID)
                    {
                        FilterData.Add(hm);
                    }
                }
            }

            return FilterData;
         }

        private async void ExportHimarkResult_Click(object sender, RoutedEventArgs e)
        {
            if(RequestList.Count==0)
            {
                MessageBox.Show("No Request In List","warning",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else
            {
                GifPanel.Visibility = Visibility.Visible;
                await System.Threading.Tasks.Task.Run(() => ExportHimarkFile());
                GifPanel.Visibility = Visibility.Collapsed;
                this.NavigationService.Navigate(new DashBoardRegionOfficer());
            }
            

        }

        private void CalcelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DashBoardRegionOfficer());
        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            DateTime StartDate = EnrollStartDate.SelectedDate.Value;
            DateTime EndDate = EnrollEndDate.SelectedDate.Value;
            BranchNameView SelectedBranch = BranchNameCombo.SelectedItem as BranchNameView;
            if(BranchNameCombo.SelectedIndex!=-1)
            {
                if (StartDate > EndDate)
                {
                    MessageBox.Show("Please Select Proper Date!..", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    if(SelectedBranch.BranchId.Equals("ALL"))
                    {
                        LoadData(StartDate, EndDate);
                    }
                    else
                    {
                        LoadData(SelectedBranch.BranchId, StartDate, EndDate);
                    }

                }
            }
            else
            {
                MessageBox.Show("Select The Branch!..", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            
        }


        void LoadData(string BId,DateTime StartDate,DateTime EndDate)
        {
            BindingRequest.Clear();
            foreach (HimarkRequestView hm in RequestList)
            {
                if (hm.BranchID == BId && hm.RequestDate>=StartDate && hm.RequestDate<=EndDate)
                {
                    BindingRequest.Add(hm);
                }

            }
            CurrentCount.Text = BindingRequest.Count.ToString();
        }
        void LoadData(DateTime StartDate,DateTime EndDate)
        {
            BindingRequest.Clear();
            foreach (HimarkRequestView hm in RequestList)
            {
                if (hm.RequestDate >= StartDate && hm.RequestDate <= EndDate)
                {
                    BindingRequest.Add(hm);
                }

            }
            CurrentCount.Text = BindingRequest.Count.ToString();
        }
    }
}
