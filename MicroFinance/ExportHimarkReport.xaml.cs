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
        List<HimarkRequestView> RequestList = new List<HimarkRequestView>();
        ObservableCollection<HimarkRequestView> ORequestList = new ObservableCollection<HimarkRequestView>();
        ObservableCollection<HimarkRequestView> BindingRequest = new ObservableCollection<HimarkRequestView>();
        public ExportHimarkReport()
        {
            InitializeComponent();
            BranchList = CollectionReportRepo.GetBranchNames();
            BranchNameCombo.ItemsSource = BranchList;
            RequestList = HimarkRepository.GetHimarkRequestList();
            LoadData();
            RequestListDataGrid.ItemsSource = ORequestList;
        }


        void LoadData()
        {
            foreach(HimarkRequestView hm in RequestList)
            {
                ORequestList.Add(hm);
                BindingRequest.Add(hm);
                OveralltotalCount.Text = ORequestList.Count().ToString();
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
            branchtotalcount.Text = BindingRequest.Count().ToString();
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

            if(BindingRequest.Count==0)
            {
                MessageBox.Show("No Request in Branch");
            }
            branchtotalcount.Text = GetCount(selectedBranch.BranchId).ToString();
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
                RetainCustomer(name);
            }
        }

        void ExportHimarkFile()
        {

            List<HimarkModel> HimarkList = new List<HimarkModel>();
            HimarkList = HimarkRepository.GetDetailsForReport(RequestList);
            HiMark himarkReport = new HiMark();
            try
            {
                himarkReport = new HiMark();
                himarkReport.createHimarkXls(HimarkList);
                MainWindow.StatusMessageofPage(1, "File Exported Successfully!...");
            }
            catch (Exception ex)
            {
                MainWindow.StatusMessageofPage(0, ex.Message);
            }
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
            }
            

        }

        private void CalcelBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
