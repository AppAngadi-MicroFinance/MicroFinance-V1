using MicroFinance.Repository;
using MicroFinance.ViewModel;
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

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for SACollectionView.xaml
    /// </summary>
    public partial class SACollectionView : Page
    {

        ObservableCollection<BranchViewModel> Branches = new ObservableCollection<BranchViewModel>();
        ObservableCollection<EmployeeViewModel> Employees = new ObservableCollection<EmployeeViewModel>();
        public SACollectionView()
        {
            InitializeComponent();
            Branches = MainWindow.BasicDetails.BranchList;
            // Employees = MainWindow.BasicDetails.EmployeeList;
            CollectionDateSelecter.SelectedDate = DateTime.Today;
            LoadBranch();
            BranchCombo.SelectedIndex = 0;

        }
        void LoadBranch()
        {
            //BranchViewModel AllBranch = new BranchViewModel { BranchId = "ALL", BranchName = "ALL", RegionId = "ALL" };
            //BranchCombo.Items.Add(AllBranch);
            foreach(BranchViewModel branch in Branches)
            {
                BranchCombo.Items.Add(branch);
            }
        }
        void LoadEmployee(string BranchId)
        {
            EmployeeCombo.Items.Clear();
            EmployeeViewModel AllEmployee = new EmployeeViewModel { BranchId="ALL",EmployeeName="ALL",EmployeeId="ALL"};
            if(BranchId=="ALL")
            {
                EmployeeCombo.Items.Add(AllEmployee);
            }
            else
            {
                EmployeeCombo.Items.Add(AllEmployee);
                foreach(EmployeeViewModel employee in Employees)
                {
                    if(employee.BranchId==BranchId && employee.Designation=="Field Officer")
                    {
                        EmployeeCombo.Items.Add(employee);
                    }
                    
                }
            }
            EmployeeCombo.SelectedIndex = 0;
        }

        private async void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            if(BranchCombo.SelectedItem!=null && CollectionDateSelecter.SelectedDate!=null)
            {
                CollectionDetailsList.Items.Clear();
                GifPanel.Visibility = Visibility.Visible;
                List<FOCollectionView> CollectionDetails = new List<FOCollectionView>();
                BranchViewModel SelectedBranch = BranchCombo.SelectedItem as BranchViewModel;
                DateTime SelectedDate = CollectionDateSelecter.SelectedDate.Value;
                await System.Threading.Tasks.Task.Run(() => CollectionDetails = SARepository.GetCollectionDetails(SelectedBranch.BranchId, SelectedDate));
                if(CollectionDetails.Count!=0)
                {
                    GifPanel.Visibility = Visibility.Collapsed;
                    LoadCollectionDetails(CollectionDetails);
                    BranchNameText.Text = SelectedBranch.BranchName;
                    TotalAmountText.Text = CollectionDetails.Select(temp => temp.CollectionAmount).Sum().ToString();
                    BranchCombo.SelectedIndex = -1;
                }
                else
                {
                    GifPanel.Visibility = Visibility.Collapsed;
                    BranchNameText.Text = SelectedBranch.BranchName;
                    TotalAmountText.Text = "0";
                    MessageBox.Show("No Data found", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    BranchCombo.SelectedIndex = -1;
                }
            }
        }



        void LoadCollectionDetails(List<FOCollectionView> Details)
        {
            CollectionDetailsList.Items.Clear();
            foreach(FOCollectionView C in Details)
            {
                CollectionDetailsList.Items.Add(C);
            }
        }

        private void BranchCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if(BranchCombo.SelectedItem!=null)
            //{
            //    BranchViewModel SelectedBranch = BranchCombo.SelectedItem as BranchViewModel;

            //    LoadEmployee(SelectedBranch.BranchId);
            //}
        }
    }
}
