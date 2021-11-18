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
using MicroFinance.ViewModel;
using MicroFinance.Modal;
using System.Collections.ObjectModel;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for HimarkResultView.xaml
    /// </summary>
    public partial class HimarkResultView : Page
    {
        List<HimarkResultModel> ResultList = new List<HimarkResultModel>();
        List<CustomerHimarkDataModel> CustomerData = new List<CustomerHimarkDataModel>();
        ObservableCollection<HimarkResultModel> HimarkDataList = new ObservableCollection<HimarkResultModel>();
        public HimarkResultView()
        {
            InitializeComponent();
        }
        public HimarkResultView(ObservableCollection<HimarkResultModel> HimarkResultData)
        {
            InitializeComponent();
            HimarkDataList = HimarkResultData;

            LoadCount();
            RequestDataGrid.ItemsSource = HimarkDataList;
        }

        void LoadCount()
        {
            int OverallCount = HimarkDataList.Count;
            OverallCountText.Text = OverallCount.ToString()+".";
            int ErrorCount = HimarkDataList.Where(temp => string.IsNullOrEmpty(temp.RequestID) == true).Count();
            ErrorCountText.Text = ErrorCount.ToString()+".";
            if(ErrorCount==0)
            {
                ShowErrorDataBtn.Visibility = Visibility.Collapsed;
            }
        }
        

        private async void UploadFileBtn_Click(object sender, RoutedEventArgs e)
        {
            string FileName = HimarkDataList.Select(temp => temp.FileName).FirstOrDefault();
            bool Result = HimarkResult.IsAlreadyUpload(FileName);
            if(Result)
            {
                try
                {
                    GifPanel.Visibility = Visibility.Visible;
                    await System.Threading.Tasks.Task.Run(() => LoanRepository.InsertHimarkData(HimarkDataList));
                    GifPanel.Visibility = Visibility.Collapsed;
                    MainWindow.StatusMessageofPage(1, FileName + " Upload SuccessFully.");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                this.NavigationService.Navigate(new DashBoardRegionOfficer());
                
            }
            else
            {
                MessageBox.Show("This File Already Upload!..", "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowErrorDataBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            foreach(HimarkResultModel HM in HimarkDataList)
            {
                if(string.IsNullOrEmpty(HM.RequestID))
                {
                    sb.Append(HM.AadharNumber + ", ");
                }
            }

            string Message = "Error Data or Mis-Match Data in Given File\n\n" + sb.ToString();
            MessageBox.Show(Message, "Error Data's (Aadhar Number)", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
