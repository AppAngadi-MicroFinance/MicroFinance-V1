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
using MicroFinance.Utils;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for HimarkResultView.xaml
    /// </summary>
    public partial class HimarkResultView : Page
    {
        public static LanguageSelector language = new LanguageSelector();
        public static string message;
        List<HimarkResultModel> ResultList = new List<HimarkResultModel>();
        List<CustomerHimarkDataModel> CustomerData = new List<CustomerHimarkDataModel>();
        ObservableCollection<HimarkResultExcelModel> HimarkDataList = new ObservableCollection<HimarkResultExcelModel>();
        public HimarkResultView()
        {
            InitializeComponent();
        }
        public HimarkResultView(ObservableCollection<HimarkResultExcelModel> HimarkResultData)
        {
            InitializeComponent();
            HimarkDataList = HimarkResultData;

            LoadCount();
            RequestDataGrid.ItemsSource = null;
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
                    message = language.translate(SystemFunction.IsTamil, "SA28");//Upload SuccessFully.
                    MainWindow.StatusMessageofPage(1, FileName + message);
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                this.NavigationService.Navigate(new DashBoardRegionOfficer());
                
            }
            else
            {
                message = language.translate(SystemFunction.IsTamil, "AE8");//This File Already Upload!..
                MessageBox.Show(message, "Warning", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DashBoardRegionOfficer());
        }

        private void ShowErrorDataBtn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            foreach(HimarkResultExcelModel HM in HimarkDataList)
            {
                if(string.IsNullOrEmpty(HM.RequestID))
                {
                    sb.Append(HM.AadharNumber + ", ");
                }
            }
            message = language.translate(SystemFunction.IsTamil, "W20");//Error Data or Mis-Match Data in Given File
            string Message = message + sb.ToString();
            MessageBox.Show(Message, "Error Data's (Aadhar Number)", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
