using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
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
using Newtonsoft.Json;

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
        public List<CollectionDetailView> CollectionDetails = new List<CollectionDetailView>();
        public DownloadCollectionReport()
        {
            InitializeComponent();
            LoadData();
            CollectionStartDate.SelectedDate = DateTime.Now;
            CollectionEndDate.SelectedDate = DateTime.Now;
            LoadBranch();
            
        }
        public DownloadCollectionReport(int Code)
        {
            InitializeComponent();
            LoadData();
            CollectionStartDate.SelectedDate = DateTime.Now;
            CollectionEndDate.SelectedDate = DateTime.Now;
            if (Code==1)
            {
                
                LoadBranch();
                string BranchID = MainWindow.LoginDesignation.BranchId;
                BranchNameCombo.SelectedIndex = SelectedBranch(BranchID);
                BranchNameCombo.IsEnabled = false;
            }
            else if(Code==2)
            {
                LoadBranch();
                string BranchID = MainWindow.LoginDesignation.BranchId;
                BranchNameCombo.SelectedIndex = SelectedBranch(BranchID);
                BranchNameCombo.IsEnabled = false;
                string EmployeeID = MainWindow.LoginDesignation.EmpId;
                FONameCombo.SelectedIndex = SelectedEmployee(EmployeeID);
                FONameCombo.IsEnabled = false;
            }
        }


        void LoadBranch()
        {
            BranchViewModel AllBranch = new BranchViewModel { BranchId = "ALL", BranchName = "ALL", RegionId = "ALL" };
            BranchNameCombo.Items.Add(AllBranch);
            foreach(BranchViewModel branch in Branches)
            {
                BranchNameCombo.Items.Add(branch);
            }
        }


        int SelectedBranch(string BranchID)
        {
            int Index = 0;
            foreach(BranchViewModel branch in Branches)
            {
                if(branch.BranchId==BranchID)
                {
                    return Index+1;
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
            foreach (EmployeeViewModel employee in FONameCombo.Items)
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
            EmployeeViewModel AllEmployee = new EmployeeViewModel { BranchId = "ALL", EmployeeId = "ALL", EmployeeName = "ALL" };
            FONameCombo.Items.Add(AllEmployee);
            foreach(EmployeeViewModel Emp in Employees)
            {
                if(Emp.BranchId==BId && Emp.Designation=="Field Officer")
                {
                    FONameCombo.Items.Add(Emp);
                }
            }

        }
        void LoadCenter(string FOID)
        {

            CenterNameCombo.Items.Clear();
            TimeTableViewModel Allcenter = new TimeTableViewModel();
            Allcenter.SHGId = "ALL";
            Allcenter.SHGName = "ALL";
            Allcenter.EmpId = "ALL";
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
                
                BranchViewModel SelectedBranch = BranchNameCombo.SelectedValue as BranchViewModel;
                if(SelectedBranch.BranchId!="ALL")
                {
                    FONameCombo.IsEnabled = true;
                    LoadFO(SelectedBranch.BranchId);
                }
                else
                {
                    FONameCombo.IsEnabled = false;
                    FONameCombo.SelectedIndex = -1;
                    CenterNameCombo.IsEnabled = false;
                    CenterNameCombo.SelectedIndex = -1;
                }
                
            }
           
        }

        private void FONameCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(FONameCombo.SelectedItem!=null)
            {
                EmployeeViewModel SelectedEmployee = FONameCombo.SelectedValue as EmployeeViewModel;
                if (SelectedEmployee.EmployeeId != "ALL")
                {
                    CenterNameCombo.IsEnabled = true;
                    LoadCenter(SelectedEmployee.EmployeeId);
                }
                else
                {
                    CenterNameCombo.IsEnabled = false;
                    CenterNameCombo.SelectedIndex = -1;
                }
            } 
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
               // DateTime selectedDate = CollectionDate.SelectedDate.Value;

                await System.Threading.Tasks.Task.Run(()=> GenerateReport(branchId,CenterID,EmpId,DateTime.Now));
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
            
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void ContineBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BranchViewModel SelectedBranch = BranchNameCombo.SelectedItem as BranchViewModel;
                if (SelectedBranch.BranchId != "ALL")
                {
                    if (BranchNameCombo.SelectedItem != null)
                    {
                        EmployeeViewModel SelectedEmployee = FONameCombo.SelectedItem as EmployeeViewModel;
                        if (SelectedEmployee.EmployeeId != "ALL")
                        {
                            if (FONameCombo.SelectedItem != null)
                            {
                                TimeTableViewModel SelectedCenter = CenterNameCombo.SelectedItem as TimeTableViewModel;
                                if (SelectedCenter.SHGId != "ALL")
                                {
                                    string CenterID = SelectedCenter.SHGId;
                                    DateTime StartDate = CollectionStartDate.SelectedDate.Value.Date.ToLocalTime();
                                    DateTime EndDate = CollectionEndDate.SelectedDate.Value.Date.ToLocalTime();
                                    //StartDate = StartDate.AddMilliseconds(DateTime.Now.Millisecond);
                                    //EndDate = EndDate.AddMilliseconds(DateTime.Now.Millisecond);

                                    DateModel data = new DateModel { FromDate = StartDate, ToDate = EndDate };
                                    GifPanel.Visibility = Visibility.Visible;
                                    await GetDetails(data, CenterID);
                                    if (CollectionDetails.Count != 0)
                                    {
                                        GifPanel.Visibility = Visibility.Collapsed;
                                        this.NavigationService.Navigate(new CollectionDetailsView(CollectionDetails));
                                    }
                                    else
                                    {
                                        GifPanel.Visibility = Visibility.Collapsed;
                                        MessageBox.Show("No Data Found", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                                    }
                                }
                                else
                                {
                                    string EmpID = SelectedEmployee.EmployeeId;
                                    DateTime StartDate = CollectionStartDate.SelectedDate.Value.Date.ToLocalTime();
                                    DateTime EndDate = CollectionEndDate.SelectedDate.Value.Date.ToLocalTime();
                                    DateModel data = new DateModel { FromDate = StartDate, ToDate = EndDate };


                                    CollectionEmployeeView CollectionDetail = new CollectionEmployeeView();
                                    CollectionDetail.EmployeeID = EmpID;
                                    CollectionDetail.DateData = data;
                                    GifPanel.Visibility = Visibility.Visible;
                                    await GetDetails(CollectionDetail);
                                    if (CollectionDetails.Count != 0)
                                    {
                                        GifPanel.Visibility = Visibility.Collapsed;
                                        this.NavigationService.Navigate(new CollectionDetailsView(CollectionDetails));
                                    }
                                    else
                                    {
                                        GifPanel.Visibility = Visibility.Collapsed;
                                        MessageBox.Show("No Data Found", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Select Employee", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                            }


                        }
                        else
                        {
                            string BranchId = SelectedBranch.BranchId;
                            DateTime StartDate = CollectionStartDate.SelectedDate.Value.ToLocalTime();
                            DateTime EndDate = CollectionEndDate.SelectedDate.Value.ToLocalTime();
                            DateModel DateData = new DateModel { FromDate = StartDate, ToDate = EndDate };
                            GifPanel.Visibility = Visibility.Visible;
                            await GetDetails(BranchId, DateData);
                            if (CollectionDetails.Count != 0)
                            {
                                GifPanel.Visibility = Visibility.Collapsed;
                                this.NavigationService.Navigate(new CollectionDetailsView(CollectionDetails));
                            }
                            else
                            {
                                GifPanel.Visibility = Visibility.Collapsed;
                                MessageBox.Show("No Data Found", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select Branch", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }


                }
                else
                {
                    if (BranchNameCombo.SelectedItem != null)
                    {
                        DateTime StartDate = CollectionStartDate.SelectedDate.Value.ToLocalTime();
                        DateTime EndDate = CollectionEndDate.SelectedDate.Value.ToLocalTime();
                        DateModel DateData = new DateModel { FromDate = StartDate, ToDate = EndDate };
                        GifPanel.Visibility = Visibility.Visible;
                        await GetDetails(DateData);
                        if (CollectionDetails.Count != 0)
                        {
                            GifPanel.Visibility = Visibility.Collapsed;
                            this.NavigationService.Navigate(new CollectionDetailsView(CollectionDetails));
                        }
                        else
                        {
                            GifPanel.Visibility = Visibility.Collapsed;
                            MessageBox.Show("No Data Found", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Select Branch", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }



                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
           


            
        }

        async Task GetDetails(string BranchID, DateModel Data)
        {
            CollectionBranchView CollectionData = new CollectionBranchView();
            CollectionData.BranchID = BranchID;
            CollectionData.DateData = Data;
            var json = JsonConvert.SerializeObject(CollectionData);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            string url1 = "http://examsign-001-site4.itempurl.com/api/GetCollectionDetails/Branch";


            HttpClient client1 = new HttpClient();
            HttpResponseMessage response1 = new HttpResponseMessage();
            response1 = await client1.PostAsync(url1, stringContent);

            if(response1.IsSuccessStatusCode)
            {
                var result = await response1.Content.ReadAsStringAsync();
                var status = JsonConvert.DeserializeObject<List<CollectionDetailView>>(result);

                if(status !=null)
                {
                    CollectionDetails = status;
                }
            }
        }
        async Task GetDetails(DateModel Data)
        {
            DateModel DateData = Data;
            var json = JsonConvert.SerializeObject(DateData);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            string url1 = "http://examsign-001-site4.itempurl.com/api/GetCollectionDetails";


            HttpClient client1 = new HttpClient();
            HttpResponseMessage response1 = new HttpResponseMessage();
            response1 = await client1.PostAsync(url1, stringContent);

            if (response1.IsSuccessStatusCode)
            {
                var result = await response1.Content.ReadAsStringAsync();
                var status = JsonConvert.DeserializeObject<List<CollectionDetailView>>(result);
                if(status !=null)
                {
                    CollectionDetails = status;
                }
            }
        }
        async Task GetDetails(DateModel Data,string CenterId)
        {
            CollectionCenterView CollectionData = new CollectionCenterView();
            CollectionData.CenterId = CenterId;
            CollectionData.DateData = new DateModel { FromDate=Data.FromDate.ToUniversalTime(),ToDate=Data.ToDate.ToUniversalTime()};
            var json = JsonConvert.SerializeObject(CollectionData);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            string url1 = "http://examsign-001-site4.itempurl.com/api/GetCollectionDetails/Center";


            HttpClient client1 = new HttpClient();
            HttpResponseMessage response1 = new HttpResponseMessage();
            response1 = await client1.PostAsync(url1, stringContent);

            if (response1.IsSuccessStatusCode)
            {
                var result = await response1.Content.ReadAsStringAsync();
                var status = JsonConvert.DeserializeObject<List<CollectionDetailView>>(result);

                CollectionDetails = status;
                if(CollectionDetails.Count!=0)
                {
                   // this.NavigationService.Navigate(new CollectionDetailsView(CollectionDetails));
                }
            }
        }
        async Task GetDetails(CollectionEmployeeView Details)
        {
            CollectionEmployeeView CollectionData = new CollectionEmployeeView();
            CollectionData.EmployeeID = Details.EmployeeID;
            CollectionData.DateData = new DateModel { FromDate = Details.DateData.FromDate.ToUniversalTime(), ToDate = Details.DateData.ToDate.ToUniversalTime() };
            var json = JsonConvert.SerializeObject(CollectionData);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");
            string url1 = "http://examsign-001-site4.itempurl.com/api/GetCollectionDetails/Employee";


            HttpClient client1 = new HttpClient();
            HttpResponseMessage response1 = new HttpResponseMessage();
            response1 = await client1.PostAsync(url1, stringContent);

            if (response1.IsSuccessStatusCode)
            {
                var result = await response1.Content.ReadAsStringAsync();
                var status = JsonConvert.DeserializeObject<List<CollectionDetailView>>(result);

                CollectionDetails = status;
                if (CollectionDetails.Count != 0)
                {
                   // this.NavigationService.Navigate(new CollectionDetailsView(CollectionDetails));
                }
            }
        }
        public class CollectionBranchView
        {
            public string BranchID { get; set; }
            public DateModel DateData { get; set; }
        }
        public class CollectionCenterView
        {
            public string CenterId { get; set; }
            public DateModel DateData { get; set; }
        }
        public class CollectionEmployeeView
        {
            public string EmployeeID { get; set; }
            public DateModel DateData { get; set; }
        }
    }
}
