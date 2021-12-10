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
using MicroFinance.Utils;
using MicroFinance.ViewModel;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for SamuResult.xaml
    /// </summary>
    public partial class SamuResult : Page
    {
        public static LanguageSelector language = new LanguageSelector();
        public static string message;

        ObservableCollection<SamuReportView> ResultList = new ObservableCollection<SamuReportView>();
        public SamuResult()
        {
            InitializeComponent();
        }
        public SamuResult(string FileName)
        {
            InitializeComponent();
            List<SamuReportView> RequestList= SamuRepository.GetSamuRequest(FileName);
            LoadDatatoResultList(RequestList);
            ReportViewGrid.ItemsSource = ResultList;
        }
        public SamuResult(List<SamuReportView> reportViews)
        {
            InitializeComponent();
            List<SamuReportView> RequestList = reportViews;
            LoadDatatoResultList(RequestList);
            ReportViewGrid.ItemsSource = ResultList;
        }


        void LoadDatatoResultList(List<SamuReportView> samuReportViews)
        {

            foreach(SamuReportView sm in samuReportViews)
            {
                ResultList.Add(sm);
                FilenameText.Text = sm.FileName;
            }
        }

        private void SelectAllCheckBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox Box = sender as CheckBox;
            if(Box.IsChecked==true)
            {
                foreach (SamuReportView sm in ResultList)
                {
                    sm.IsRecommend = true;
                    ReportViewGrid.Items.Refresh();
                }
                
            }
            
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DashBoardHeadOfficer());
        }

        private void UploadSamuDataBtn_Click(object sender, RoutedEventArgs e)
        {
            List<SamuReportView> Finallist = new List<SamuReportView>();
            if(SamuRepository.IsFileAlreadyExists(FilenameText.Text))
            {
                message = language.translate(SystemFunction.IsTamil, "AE13");//
                MainWindow.StatusMessageofPage(0, message);
            }
            else
            {
                SamuRepository.InsertSamuData(ResultList);
                this.NavigationService.Navigate(new DashBoardHeadOfficer());
            }
            
        }
    }
}
