using MicroFinance.Reports;
using MicroFinance.ViewModel;
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

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for GenerateHimarkReport.xaml
    /// </summary>
    public partial class GenerateHimarkReport : Page
    {
        public GenerateHimarkReport()
        {
            InitializeComponent();
        }
        List<HimarkRequestView> RequestData = new List<HimarkRequestView>();
        public GenerateHimarkReport(List<HimarkRequestView> ReqList)
        {
            InitializeComponent();
            RequestData = ReqList;
            RequestListDataGrid.ItemsSource = ReqList;
            CountText.Text = ReqList.Count.ToString();
        }

        void ExportHimarkFile()
        {
            List<HimarkModel> HimarkList = new List<HimarkModel>();
            HimarkList = HimarkRepository.GetDetailsForReport(RequestData);
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

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DashBoardRegionOfficer());
        }

        private async void HmExportBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GifPanel.Visibility = Visibility.Visible;
                await System.Threading.Tasks.Task.Run(() => ExportHimarkFile());
                GifPanel.Visibility = Visibility.Collapsed;
                this.NavigationService.Navigate(new DashBoardRegionOfficer());
            }
            catch(Exception ex)
            {
                GifPanel.Visibility = Visibility.Collapsed;
                MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }
    }
}
