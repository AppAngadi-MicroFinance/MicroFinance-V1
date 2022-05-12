﻿using MicroFinance.Modal;
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
        DateRange CurrentDateRange = new DateRange();
        List<ReportListViewModel> ReportTypes = new List<ReportListViewModel>();
        GTReport GTReports;
        ReportListViewModel SelectedItem = new ReportListViewModel();
        List<string> FinalPathList = new List<string>();

        string BaseDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "REPORTS\\");
        string ConnectionString = MicroFinance.Properties.Settings.Default.DBConnection;
        string SelectedFileItem = string.Empty;
        int SelectedFileItemIndex = -1;
        public ReportDownloadWindow()
        {
            InitializeComponent();
            LoadReportTypes();
            xReportTypes.ItemsSource = ReportTypes;
            CurrentDateRange.FromDate = DateTime.Now.AddMonths(-4);
            CurrentDateRange.ToDate = DateTime.Now;
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
            xSelectedReport.Text = selectedItem.ReportType;
            foreach (ReportListViewModel item in ReportTypes)
            {
                if (item.Index == selectedItem.Index)
                    item.IsSelected = true;
                else
                    item.IsSelected = false;
            }
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
        private async void xGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            xLoadingGifPanel.Visibility = Visibility.Visible;

            await Task.Run(() => ReportGenerationProcess());

            FinalPath_Binding(FinalPathList);
            xFinalReportPathPanel.Visibility = Visibility.Visible;
            xLoadingGifPanel.Visibility = Visibility.Collapsed;
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
                        FinalPathList = GTReports.AccountClosingReport(CurrentDateRange);
                        break;

                    case 2:
                        FinalPathList = GTReports.ApplicationLoginReport(CurrentDateRange);
                        break;

                    case 3:
                        FinalPathList = GTReports.ApplicationHighmarkApproved(CurrentDateRange);
                        break;

                    case 4:
                        FinalPathList = GTReports.ApplicationHighmarkRejection(CurrentDateRange);
                        break;

                    case 5:
                        FinalPathList = GTReports.LoanAmount(CurrentDateRange);
                        break;

                    case 6:
                        FinalPathList = GTReports.LoanRecovery(CurrentDateRange);
                        break;

                    case 7:
                        FinalPathList = GTReports.LoanDisbursement(CurrentDateRange);
                        break;

                    case 8:
                        FinalPathList = GTReports.OutstandingReport(CurrentDateRange);
                        break;

                    default:
                        break;
                }
            }
        }
        void FinalPath_Binding(List<string> filePathList)
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
