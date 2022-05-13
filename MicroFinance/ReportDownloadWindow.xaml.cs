using MicroFinance.Modal;
using MicroFinance.ReportExports;
using MicroFinance.ReportExports.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ReportDownloadWindow.xaml
    /// </summary>
    public partial class ReportDownloadWindow : Page
    {
        DateRange ContextRange = new DateRange();
        List<ReportListViewModel> ReportTypes = new List<ReportListViewModel>();
        GTReport GTReports;
        ReportListViewModel SelectedItem = new ReportListViewModel();
        ObservableCollection<string> FinalPathList = new ObservableCollection<string>();

        string BaseDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "REPORTS\\");
        string ConnectionString = MicroFinance.Properties.Settings.Default.DBConnection;
        string SelectedFileItem = string.Empty;
        int SelectedFileItemIndex = -1;
        public ReportDownloadWindow()
        {
            InitializeComponent();
            LoadReportTypes();
            ResetDateRange();
            xReportTypes.ItemsSource = ReportTypes;
        }
        void LoadReportTypes()
        {
            ReportTypes.Add(new ReportListViewModel(1,"Account Closings"));
            ReportTypes.Add(new ReportListViewModel(2,"Application Logins"));
            ReportTypes.Add(new ReportListViewModel(3,"Highmark Approvals"));
            ReportTypes.Add(new ReportListViewModel(4,"Highmark Rejections"));
            ReportTypes.Add(new ReportListViewModel(5,"Loan Amount"));
            ReportTypes.Add(new ReportListViewModel(6,"Loan Amount recovery"));
            ReportTypes.Add(new ReportListViewModel(7,"Loan Disbursments"));
            ReportTypes.Add(new ReportListViewModel(8,"Loan Outstanding Amount")); 
        }
        void SelectionAction(ReportListViewModel selectedItem)
        {
            ResetDateRange();
            xSelectedReport.Text = selectedItem.ReportType;
            foreach (ReportListViewModel item in ReportTypes)
            {
                if (item.Index == selectedItem.Index)
                    item.IsSelected = true;
                else
                    item.IsSelected = false;
            }
        }

        void ResetDateRange()
        {
            ContextRange = null;
            xFromDateTB.Text = string.Empty;
            xToDateTB.Text = string.Empty;
        }
        private void xReportTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(xReportTypes.SelectedIndex > -1)
            {
                SelectedItem = xReportTypes.SelectedItem as ReportListViewModel;
                SelectionAction(SelectedItem);
            }
        }
        private void xReportFilesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (xReportFilesList.SelectedIndex >= 0)
            {
                SelectedFileItem = xReportFilesList.SelectedItem as string;
                SelectedFileItemIndex = xReportFilesList.SelectedIndex;
                OpenFile(FinalPathList[SelectedFileItemIndex]);
                xReportFilesList.SelectedIndex = -1;
            }
        }
        bool DoDateVerify(DateRange range)
        {
            if (range.FromDate == new DateTime())
                return false;
            else if (range.ToDate == new DateTime())
                return false;
            else
                return true;
        }
        private async void xGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            if(ContextRange != null && DoDateVerify(ContextRange))
            {
                FinalPathList.Clear();
                xLoadingGifPanel.Visibility = Visibility.Visible;
                xFinalReportPathPanel.Visibility = Visibility.Collapsed;

                await Task.Run(() => ReportGenerationProcess());

                FinalPath_Binding(FinalPathList);
                xFinalReportPathPanel.Visibility = Visibility.Visible;
                xLoadingGifPanel.Visibility = Visibility.Collapsed;
            }
            else
                MessageBox.Show("You have to select date range.");
        }
        private void xOpenAll_Click(object sender, RoutedEventArgs e)
        {
            if (FinalPathList.Count() > 0)
            {
                OpenAllFiles();
            }
        }

        void ReportGenerationProcess()
        {
            if (GTReports == null)
                GTReports = new GTReport(ConnectionString);

            if (SelectedItem.Index > 0)
            {
                switch (SelectedItem.Index)
                {
                    case 1:
                        FinalPathList = new ObservableCollection<string>(GTReports.AccountClosingReport(ContextRange));
                        break;

                    case 2:
                        FinalPathList = new ObservableCollection<string>(GTReports.ApplicationLoginReport(ContextRange));
                        break;

                    case 3:
                        FinalPathList = new ObservableCollection<string>(GTReports.ApplicationHighmarkApproved(ContextRange));
                        break;

                    case 4:
                        FinalPathList = new ObservableCollection<string>(GTReports.ApplicationHighmarkRejection(ContextRange));
                        break;

                    case 5:
                        FinalPathList = new ObservableCollection<string>(GTReports.LoanAmount(ContextRange));
                        break;

                    case 6:
                        FinalPathList = new ObservableCollection<string>(GTReports.LoanRecovery(ContextRange));
                        break;

                    case 7:
                        FinalPathList = new ObservableCollection<string>(GTReports.LoanDisbursement(ContextRange));
                        break;

                    case 8:
                        FinalPathList = new ObservableCollection<string>(GTReports.OutstandingReport(ContextRange));
                        break;

                    default:
                        break;
                }
            }
        }
        void FinalPath_Binding(ObservableCollection<string> filePathList)
        {
            var res = filePathList.Select(o => o.Replace(BaseDirectory, ""));
            xReportFilesList.ItemsSource = res.ToList();
        }
        void OpenAllFiles()
        {
            foreach(string fileItem in FinalPathList)
            {
                OpenFile(fileItem);
            }
        }
        void OpenFile(string path)
        {
            try
            {
                if (File.Exists(path))
                    Process.Start(path);
                else
                    MessageBox.Show("File missing or already opened..!");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void xFromDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(xFromDate.SelectedDate != null)
            {
                if (ContextRange == null)
                    ContextRange = new DateRange();
                ContextRange.FromDate = (DateTime)xFromDate.SelectedDate;
                ContextRange.ToDate = new DateTime();
                xFromDateTB.Text = ContextRange.FromDate_String;
                xFromDate.SelectedDate = null;
            }
        }

        private void xToDate_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(xToDate.SelectedDate != null)
            {
                if (ContextRange == null)
                    ContextRange = new DateRange();

                if (ContextRange.FromDate <= xToDate.SelectedDate)
                {
                    ContextRange.ToDate = (DateTime)xToDate.SelectedDate;
                    xToDateTB.Text = ContextRange.ToDate_String;
                }
                else
                    MessageBox.Show("Invalid date selection.");
                xToDate.SelectedDate = null;
            }   
        }
    }

    public class ReportListViewModel : BindableBase
    {
        public int Index { get; set; }
        public string ReportType { get; set; }

        bool _isSelected;
        public bool IsSelected 
        { 
            get { return _isSelected; }
            set 
            {
                _isSelected = value;
                RaisedPropertyChanged("IsSelected");
            }
        }

        public ReportListViewModel(int index, string reportType)
        {
            this.Index = index;
            this.ReportType = reportType;
            this.IsSelected = false;
        }
        public ReportListViewModel()
        {

        }
    }
}
