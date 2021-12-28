using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
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
using Microsoft.Reporting.WinForms;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for DownloadCollectionReport.xaml
    /// </summary>
    public partial class DownloadCollectionReport : Page
    {
        public ObservableCollection<BranchViewModel> Branches = new ObservableCollection<BranchViewModel>();
        public ObservableCollection<EmployeeViewModel> Employees = new ObservableCollection<EmployeeViewModel>();
        public List<TimeTableViewModel> Centers = new List<TimeTableViewModel>();
        public DownloadCollectionReport()
        {
            InitializeComponent();
            LoadData();
            //BranchNameCombo.ItemsSource = BranchList;
            //FONameCombo.ItemsSource = EmployeeList;
        }
        public DownloadCollectionReport(int Code)
        {
            InitializeComponent();
            LoadData();
           if(Code==1)
            {
                //BranchNameCombo.IsEnabled = false;
                BranchNameCombo.ItemsSource = Branches;
                string BranchID = MainWindow.LoginDesignation.BranchId;
                BranchNameCombo.SelectedIndex = SelectedBranch(BranchID);
            }
        }


        int SelectedBranch(string BranchID)
        {
            int Index = 0;
            foreach(BranchViewModel branch in Branches)
            {
                if(branch.BranchId==BranchID)
                {
                    return Index;
                }
                else
                {
                    Index++;
                }
            }

            return -1;
        }
        int SelectedEmployee(string EmpID)
        {
            int Index = 0;
            foreach (EmployeeViewModel employee in Employees)
            {
                if (employee.EmployeeId == EmpID)
                {
                    return Index;
                }
                else
                {
                    Index++;
                }
            }
            return -1;
        }


        public void LoadData()
        {
            //BranchList = CollectionReportRepo.GetBranchNames();
            //CenterList = CollectionReportRepo.GetCenters();
            //EmployeeList = CollectionReportRepo.GetEmployees();
            Branches = MainWindow.BasicDetails.BranchList;
            Employees = MainWindow.BasicDetails.EmployeeList;
            Centers = MainWindow.BasicDetails.CenterList;
            

        }

        void LoadFO(string BId)
        {
            FONameCombo.Items.Clear();
            foreach(EmployeeViewModel Emp in Employees)
            {
                if(Emp.BranchId==BId)
                {
                    FONameCombo.Items.Add(Emp);
                }
            }

        }
        void LoadCenter(string FOID)
        {
            CenterNameCombo.Items.Clear();
            CenterNameView Allcenter = new CenterNameView();
            Allcenter.CenterId = "All";
            Allcenter.CenterName = "All";
            Allcenter.BranchId = "All";
            CenterNameCombo.Items.Add(Allcenter);
            foreach(TimeTableViewModel C in Centers)
            {
                if(C.EmpId==FOID)
                {
                    CenterNameCombo.Items.Add(C);
                }
            }
        }

        private void BranchNameCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(BranchNameCombo.SelectedItem!=null)
            {
                FONameCombo.IsEnabled = true;
                BranchViewModel SelectedBranch = BranchNameCombo.SelectedValue as BranchViewModel;
                LoadFO(SelectedBranch.BranchId);
            }
           
        }

        private void FONameCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CenterNameCombo.IsEnabled = true;
            EmployeeNameView SelectedEmployee = FONameCombo.SelectedValue as EmployeeNameView;
            LoadCenter(SelectedEmployee.EmpId);
        }

        private async void DownloadSheetBtn_Click(object sender, RoutedEventArgs e)
        {
            if(BranchNameCombo.SelectedValue!=null && FONameCombo.SelectedValue!=null && CenterNameCombo.SelectedValue!=null)
            {
                BranchNameView SelectedBranch = BranchNameCombo.SelectedValue as BranchNameView;
                EmployeeNameView SelectedEmployee = FONameCombo.SelectedValue as EmployeeNameView;
                CenterNameView selectedCenter = CenterNameCombo.SelectedValue as CenterNameView;
                GifPanel.Visibility = Visibility.Visible;
                string branchId = SelectedBranch.BranchId;
                string CenterID = selectedCenter.CenterId;
                string EmpId = SelectedEmployee.EmpId;
                DateTime selectedDate = CollectionDate.SelectedDate.Value;

                await System.Threading.Tasks.Task.Run(()=> GenerateReport(branchId,CenterID,EmpId,selectedDate));
                GifPanel.Visibility = Visibility.Collapsed;
                MainWindow.StatusMessageofPage(1, "Report generated Successfully!....");
            }
        }
        void GenerateReport(string BranchID,string CenterId,String EmpID,DateTime date)
        {
            
            //DateTime selecteddate = CollectionDate.SelectedDate.Value;
            List<CollectionReportView> CollectionList = CollectionReportRepo.GetCollectionDetails(BranchID,EmpID,CenterId,date);
            GenerateCollectionReport(CollectionList);
        }

        static public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }


        static void GenerateCollectionReport(List<CollectionReportView> obj)
        {
            //string filedate = showDate.Text;
            DataTable dt1 = new DataTable();
            dt1 = ConvertToDataTable(obj);

            
            //DataTable dt2 = new DataTable();
            //dt2 = ConvertToDataTable(obj);


            //For table 1
            ReportDataSource reportDataSource1 = new ReportDataSource();
            reportDataSource1.Name = "DataSet1"; // Name of the DataSet we set in .rdlc
            reportDataSource1.Value = dt1;

            //For table 2
            //ReportDataSource reportDataSource2 = new ReportDataSource();
            //reportDataSource2.Name = "DataSet2"; // Name of the DataSet we set in .rdlc
            //reportDataSource2.Value = dt2;

            //Setting Report Viewer
            ReportViewer reportViewer1 = new ReportViewer();
            reportViewer1.LocalReport.ReportEmbeddedResource = "MicroFinance.CollectionReport.rdlc";
            reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            //reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            reportViewer1.RefreshReport();
            reportViewer1.ProcessingMode = ProcessingMode.Local;

            Warning[] warnings1;
            string[] streamids1;
            string mimeType1;
            string encoding1;
            string extension1;
            try
            {
                string dir = string.Empty;
                //string showdatess = Changeformat(showDate.Text);
                byte[] bytes = reportViewer1.LocalReport.Render("PDF", null, out mimeType1, out encoding1, out extension1, out streamids1, out warnings1);
                dir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "REPORTS\\");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                if (Directory.Exists(dir))
                {
                    //var temp = "" + data + "" + "_WeeklyReport"+showDate+"";
                    //var temp = "" + data + "" + "_" + "" + SelectedCluster + "";
                    FileStream fs = new FileStream(dir + "Collection_Report_Sheet_" + DateTime.Now.ToString("dd-MMM-yyyy") + ".pdf", FileMode.Create);

                    var temps = fs.ToString();
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can't generate Collection Sheet for You. Contact admin.");
            }
        }

        private void CenterNameCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionDate.IsEnabled = true;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ContineBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
