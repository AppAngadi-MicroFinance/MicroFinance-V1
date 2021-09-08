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

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for DownloadCollectionReport.xaml
    /// </summary>
    public partial class DownloadCollectionReport : Page
    {
        public List<BranchNameView> BranchList = new List<BranchNameView>();
        public List<CenterNameView> CenterList = new List<CenterNameView>();
        public List<EmployeeNameView> EmployeeList = new List<EmployeeNameView>();
        public DownloadCollectionReport()
        {
            InitializeComponent();
            LoadData();
            BranchNameCombo.ItemsSource = BranchList;
            //FONameCombo.ItemsSource = EmployeeList;
        }


        public void LoadData()
        {
            BranchList = CollectionReportRepo.GetBranchNames();
            CenterList = CollectionReportRepo.GetCenters();
            EmployeeList = CollectionReportRepo.GetEmployees();

        }

        void LoadFO(string BId)
        {
            FONameCombo.Items.Clear();
            foreach(EmployeeNameView Emp in EmployeeList)
            {
                if(Emp.BranchId==BId)
                {
                    FONameCombo.Items.Add(Emp);
                }
            }

        }

        private void BranchNameCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BranchNameView SelectedBranch = BranchNameCombo.SelectedValue as BranchNameView;
            LoadFO(SelectedBranch.BranchId);
        }
    }
}
