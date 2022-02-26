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
using MicroFinance.Reports;
using MicroFinance.ViewModel;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for CollectionDetailsView.xaml
    /// </summary>
    public partial class CollectionDetailsView : Page
    {
        public ObservableCollection<CollectionDetailsModel> OverAllList = new ObservableCollection<CollectionDetailsModel>();
        public ObservableCollection<CollectionDetailsModel> BindingList = new ObservableCollection<CollectionDetailsModel>();
        List<CollectionDetailView> CollectionDetails = new List<CollectionDetailView>();


        //Basic Details
        ObservableCollection<BranchViewModel> Branches = new ObservableCollection<BranchViewModel>();
        ObservableCollection<EmployeeViewModel> Employees = new ObservableCollection<EmployeeViewModel>();
        List<TimeTableViewModel> Centers = new List<TimeTableViewModel>();
        public CollectionDetailsView()
        {
            InitializeComponent();
        }
        public CollectionDetailsView(List<CollectionDetailView>  Collections)
        {
            InitializeComponent();
            CollectionDetails=Collections;

            Branches = MainWindow.BasicDetails.BranchList;
            Employees = MainWindow.BasicDetails.EmployeeList;
            Centers = MainWindow.BasicDetails.CenterList;
            LoadData();
            LoadBranch();
            if(MainWindow.LoginDesignation.LoginDesignation=="Field Officer")
            {
                string BranchID = MainWindow.LoginDesignation.BranchId;
                string EmployeeID = MainWindow.LoginDesignation.EmpId;
                BranchCombo.SelectedIndex = SelectedBranch(BranchID);
                BranchCombo.IsEnabled = false;
                EmployeeCombo.SelectedIndex = SelectedEmployee(EmployeeID);
                EmployeeCombo.IsEnabled = false;
            }
            else if (MainWindow.LoginDesignation.LoginDesignation == "Manager")
            {
                string BranchID = MainWindow.LoginDesignation.BranchId;
                BranchCombo.SelectedIndex = SelectedBranch(BranchID);
                BranchCombo.IsEnabled = false;
                CenterCombo.SelectedIndex = 0;
            }

            CollectionDetailsGrid.ItemsSource = BindingList;
        }

        void LoadData()
        {
            foreach(CollectionDetailView C in CollectionDetails)
            {
                CollectionDetailsModel Collection = new CollectionDetailsModel();
                Collection.BranchId = C.BranchId;
                Collection.BranchName = MainWindow.BasicDetails.BranchList.Where(temp => temp.BranchId == Collection.BranchId).Select(temp=>temp.BranchName).FirstOrDefault();
                Collection.CustId = C.CustId;
                Collection.CustomerName = C.CustomerName;
                Collection.LoanId = C.LoanId;
                Collection.Principal = C.Principal;
                Collection.Interest = C.Interest;
                Collection.Total = C.Total;
                Collection.SecurityDeposite = C.SecurityDeposite;
                Collection.ActualDue = C.ActualDue;
                Collection.PaidDue = C.PaidDue;
                Collection.Balance = C.Balance;
                Collection.ActualPaymentDate = C.ActualPaymentDate;
                Collection.CollectedOn = C.CollectedOn;
                Collection.Attendance = C.Attendance;
                Collection.Extras = C.Extras;
                Collection.CollectedBy = C.CollectedBy;
                Collection.EmployeeName = MainWindow.BasicDetails.EmployeeList.Where(temp => temp.EmployeeId == C.CollectedBy).Select(temp => temp.EmployeeName).FirstOrDefault();
                Collection.CenterName = C.CenterName;
                Collection.CenterID = C.CenterID;



                OverAllList.Add(Collection);
                BindingList.Add(Collection);

            }
            CollectedAmountText.Text = BindingList.Select(temp => temp.PaidDue).Sum().ToString();
            TotalAmountTobeCollectedText.Text = BindingList.Select(temp => temp.ActualDue).Sum().ToString();
        }
        void LoadBranch()
        {
            BranchViewModel AllBranch = new BranchViewModel { BranchId = "ALL", BranchName = "ALL", RegionId = "ALL" };
            BranchCombo.Items.Add(AllBranch);
            foreach(BranchViewModel branch in Branches)
            {
                BranchCombo.Items.Add(branch);
            }
        }
        void loadEmployee(string BranchID)
        {
            EmployeeCombo.Items.Clear();
            EmployeeViewModel AllEmployee = new EmployeeViewModel { EmployeeId = "ALL", EmployeeName = "ALL", Designation = "ALL" };
            EmployeeCombo.Items.Add(AllEmployee);
            foreach(EmployeeViewModel emp in Employees)
            {
                if (emp.BranchId == BranchID && emp.Designation == "Field Officer")
                {
                    EmployeeCombo.Items.Add(emp);
                }
            }
        }
        void LoadCenters(string EmployeeID)
        {
            CenterCombo.Items.Clear();
            TimeTableViewModel AllTimeTable = new TimeTableViewModel {SHGId="ALL",SHGName="ALL",EmpId=""};
            CenterCombo.Items.Add(AllTimeTable);
            foreach(TimeTableViewModel center in Centers)
            {
                if(center.EmpId==EmployeeID)
                {
                    CenterCombo.Items.Add(center);
                }
            }

        }
        int SelectedBranch(string BranchID)
        {
            int index = 0;
            foreach(BranchViewModel branch in BranchCombo.Items)
            {
                if(branch.BranchId==BranchID)
                {
                    return index;
                }
                else
                {
                    index++;
                }
            }
            return -1;
        }
        int SelectedEmployee(string EmployeeID)
        {
            int index = 0;
            foreach(EmployeeViewModel emp in EmployeeCombo.Items)
            {
                if(emp.EmployeeId==EmployeeID)
                {
                    return index;
                }
                else
                {
                    index++;
                }
            }
            return -1;
        }
        private void BranchCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(BranchCombo.SelectedItem!=null)
            {
                BranchViewModel SelectedBranch = BranchCombo.SelectedItem as BranchViewModel;
                loadEmployee(SelectedBranch.BranchId);
            }
        }

        private void EmployeeCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(EmployeeCombo.SelectedItem!=null)
            {
                EmployeeViewModel SelectedEmployee = EmployeeCombo.SelectedItem as EmployeeViewModel;
                if(SelectedEmployee.EmployeeId!="ALL")
                {
                    LoadCenters(SelectedEmployee.EmployeeId);
                }
            }
        }

        private void OKBtn_Click(object sender, RoutedEventArgs e)
        {
            if(BranchCombo.SelectedItem!=null)
            {
                BranchViewModel SelectedBranch = BranchCombo.SelectedItem as BranchViewModel;
                if(SelectedBranch.BranchId!="ALL")
                {
                    EmployeeViewModel SelectedEmployee = EmployeeCombo.SelectedItem as EmployeeViewModel;
                    if(SelectedEmployee.EmployeeId!="ALL")
                    {
                        TimeTableViewModel SelectedCenter = CenterCombo.SelectedItem as TimeTableViewModel;
                        if(SelectedCenter.SHGId!="ALL")
                        {
                            LoadCollection(1, SelectedCenter.SHGId);
                            CollectedAmountText.Text = BindingList.Select(temp => temp.PaidDue).Sum().ToString();
                            TotalAmountTobeCollectedText.Text = BindingList.Select(temp => temp.ActualDue).Sum().ToString();
                        }
                        else
                        {
                            LoadCollection(SelectedEmployee.EmployeeId, 1);
                            CollectedAmountText.Text = BindingList.Select(temp => temp.PaidDue).Sum().ToString();
                            TotalAmountTobeCollectedText.Text = BindingList.Select(temp => temp.ActualDue).Sum().ToString();
                        }

                    }
                    else
                    {
                        LoadCollection(SelectedBranch.BranchId);
                        CollectedAmountText.Text = BindingList.Select(temp => temp.PaidDue).Sum().ToString();
                        TotalAmountTobeCollectedText.Text = BindingList.Select(temp => temp.ActualDue).Sum().ToString();
                    }
                }
                else
                {
                    LoadCollection();
                    CollectedAmountText.Text = BindingList.Select(temp => temp.PaidDue).Sum().ToString();
                    TotalAmountTobeCollectedText.Text = BindingList.Select(temp => temp.ActualDue).Sum().ToString();
                }
            }
            else
            {
                MessageBox.Show("Select Branch");
            }
        }

        void LoadCollection()
        {
            BindingList.Clear();
            foreach(CollectionDetailsModel Collection in OverAllList)
            {
                BindingList.Add(Collection);
            }
        }
        void LoadCollection(string BranchID)
        {
            BindingList.Clear();
            foreach(CollectionDetailsModel Collection in OverAllList)
            {
                if(Collection.BranchId==BranchID)
                {
                    BindingList.Add(Collection);
                }
            }
        }
        void LoadCollection(string EmpID,int a)
        {
            BindingList.Clear();
            foreach(CollectionDetailsModel collection in OverAllList)
            {
                if(collection.CollectedBy==EmpID)
                {
                    BindingList.Add(collection);
                }
            }
        }
        void LoadCollection(int a,string CenterID)
        {
            BindingList.Clear();
            foreach(CollectionDetailsModel collection in OverAllList)
            {
                if(collection.CenterID==CenterID)
                {
                    BindingList.Add(collection);
                }
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            string Designation = MainWindow.LoginDesignation.LoginDesignation;
            Designation = (Designation == null) ? "" : Designation;
            LoadHomePage(Designation);
        }
        private void RepostBtn_Click(object sender, RoutedEventArgs e)
        {
            CollectionDetailsReport report = new CollectionDetailsReport(BindingList);
            report.GenerateCollectionRepost();
            string Designation = MainWindow.LoginDesignation.LoginDesignation;
            Designation = (Designation == null) ? "" : Designation;
            LoadHomePage(Designation);
        }
        public void LoadHomePage(string Designation)
        {
            if (Designation.Equals("Field Officer"))
                this.NavigationService.Navigate(new DashboardFieldOfficer());
            else if (Designation.Equals("Accountant"))
                this.NavigationService.Navigate(new DashboardAccountant());
            else if (Designation.Equals("Branch Manager") || Designation.Equals("Manager"))
                this.NavigationService.Navigate(new DashboardBranchManager());
            else if (Designation.Equals("Region Manager"))
                this.NavigationService.Navigate(new DashBoardRegionOfficer());
            else
                this.NavigationService.Navigate(new DashBoardHeadOfficer());
        }
    }


    public class CollectionDetailsModel
    {
        public string BranchId { get; set; }
        public string CustId { get; set; }
        public string CustomerName { get; set; }
        public string LoanId { get; set; }
        public int Principal { get; set; }
        public int Interest { get; set; }
        public int Total { get; set; }
        public int SecurityDeposite { get; set; }
        public int ActualDue { get; set; }
        public int PaidDue { get; set; }
        public int Balance { get; set; }
        public DateTime ActualPaymentDate { get; set; }
        public DateTime CollectedOn { get; set; }
        public int Attendance { get; set; }
        public int Extras { get; set; }
        public string CollectedBy { get; set; }
        public string EmployeeName { get; set; }
        public string BranchName { get; set; }
        public string CenterName { get; set; }
        public string CenterID { get; set; }
        public override string ToString()
        {
            return (this.BranchName + "," + this.EmployeeName + "," + this.CenterName + "," + this.CustomerName+"," + this.Principal +","+this.CollectedOn.ToString("dd-mm-yyyy")+"," +this.Interest + "," + this.SecurityDeposite+"," + this.Total);
        }
    }
}
