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
using MicroFinance.Modal;
using MicroFinance.ViewModel;
using MicroFinance.Repository;
using System.Collections.ObjectModel;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for CreateSHG.xaml
    /// </summary>
    public partial class CreateSHG : Page
    {
        SHGModal SHG = new SHGModal();
        ObservableCollection<BranchViewModel> Branches = new ObservableCollection<BranchViewModel>();
        public CreateSHG()
        {
            InitializeComponent();
            SHGGrid.DataContext = SHG;
            Branches = MainWindow.BasicDetails.BranchList;
            BranchCombo.ItemsSource = Branches;
            BranchCombo.SelectedIndex = currentBranch();
            DeActiveBtn.Visibility = Visibility.Collapsed;
            UpdateBtn.Visibility = Visibility.Collapsed;

        }
        int currentBranch()
        {
            int Count = 0;
            foreach(BranchViewModel Branch in Branches)
            {
                if(Branch.BranchId.Equals(MainWindow.LoginDesignation.BranchId))
                {
                    return Count;
                }
                Count++;
            }
            return -1;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void BranchCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(BranchCombo.SelectedIndex!=-1)
            {
                BranchViewModel branch = BranchCombo.SelectedItem as BranchViewModel;
                SHG.BranchID = branch.BranchId;
            }
            
        }

        private void SubmitBtn_Click(object sender, RoutedEventArgs e)
        {
            if(ValidCheck())
            {
                string res = SHGRepository.AddSHG(SHG);
                if(res!=null)
                {
                    SHG.SHGId = res;
                    this.NavigationService.Navigate(new AssignSHG(SHG));
                }
                else
                {

                }
            }
            else
            {
                MessageBox.Show("Enter All Details!...", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            

        }


        bool ValidCheck()
        {
            if(BranchCombo.SelectedIndex!=-1 &&!string.IsNullOrEmpty(CenterNameBox.Text)&&!string.IsNullOrEmpty(TalukBox.Text)&&!string.IsNullOrEmpty(DistrictBox.Text))
            {
                return true;
            }
            return false;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new DashboardBranchManager());
        }
    }
}
