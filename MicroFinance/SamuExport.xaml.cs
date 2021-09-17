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
using MicroFinance.ViewModel;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for SamuExport.xaml
    /// </summary>
    public partial class SamuExport : Page
    {
        List<BranchNameView> BranchList = new List<BranchNameView>();
        List<HimarkRequestView> RequestList = new List<HimarkRequestView>();
        GTtoSamunnati GTtoSAMU = new GTtoSamunnati();
        public SamuExport()
        {
            InitializeComponent();
            
            BranchList = CollectionReportRepo.GetBranchNames();
            BranchNameCombo.ItemsSource = BranchList;
            
            LoadData();
            OverallCount.Text = RequestList.Count().ToString();
            Samu_Report_View_Grid.ItemsSource = RequestList;
        }

        void LoadData()
        {
            RequestList= SamuRepository.GetSamuRequestList();
        }

        private void BranchNameCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BranchNameView selectedBranch = BranchNameCombo.SelectedValue as BranchNameView;
            ChangeCount(selectedBranch.BranchId);


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
            BranchCount.Text = count.ToString();
        }

        private void SamuCancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DashBoardHeadOfficer());
        }

        private async void SamuExportBtn_Click(object sender, RoutedEventArgs e)
        {
            GifPanel.Visibility = Visibility.Visible;
            try
            {
                await System.Threading.Tasks.Task.Run(() => GenerateSamuFile());
                GifPanel.Visibility = Visibility.Collapsed;
                this.NavigationService.Navigate(new DashBoardHeadOfficer());
            }
            catch
            {
                MainWindow.StatusMessageofPage(0, "Error...");
                GifPanel.Visibility = Visibility.Collapsed;
            }

        }
        void GenerateSamuFile()
        {
            try
            {
                GTtoSAMU.GenerateSamunnati_File();
                HimarkRepository.UpdateStatusToExportExcel(RequestList, 11);
                MainWindow.StatusMessageofPage(1, "Excel Generated Successfully!...");
            }
            catch
            {
                MainWindow.StatusMessageofPage(0, "Error...");
            }
        }
    }
}
