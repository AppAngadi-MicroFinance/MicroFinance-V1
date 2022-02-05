using MicroFinance.Repository;
using MicroFinance.ViewModel;
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

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for SALoanApplcationView.xaml
    /// </summary>
    public partial class SALoanApplcationView : Page
    {
        
        public SALoanApplcationView()
        {
            InitializeComponent();
            LoadBranch();
            FromDate.SelectedDate = DateTime.Today;
            ToDate.SelectedDate = DateTime.Today;
        }

        void LoadBranch()
        {
            BranchViewModel AllBranch = new BranchViewModel { BranchId = "ALL", BranchName = "ALL", RegionId = "ALL" };
            BranchCombo.Items.Add(AllBranch);
            foreach(BranchViewModel branch in MainWindow.BasicDetails.BranchList)
            {
                BranchCombo.Items.Add(branch);
            }
        }

        private async void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            if(BranchCombo.SelectedItem!=null && FromDate.SelectedDate!=null&&ToDate.SelectedDate!=null)
            {
                StatusDetailsList.Items.Clear();
                SALoanStatusView StatusDetails = new SALoanStatusView();
                
                BranchViewModel SelectedBranch = BranchCombo.SelectedItem as BranchViewModel;
                DateTime From = FromDate.SelectedDate.Value;
                DateTime To = ToDate.SelectedDate.Value;
                BranchNameText.Text = SelectedBranch.BranchName;
                GifPanel.Visibility = Visibility.Visible;

                string BranchId = SelectedBranch.BranchId;
                if(BranchId!="ALL")
                {
                    await System.Threading.Tasks.Task.Run(() => StatusDetails = SARepository.GetApplicationStatusDetails(SelectedBranch.BranchId, From, To));
                }
                else
                {
                    await System.Threading.Tasks.Task.Run(() => StatusDetails = SARepository.GetApplicationStatusDetails(From, To));
                }
                
                GifPanel.Visibility = Visibility.Collapsed;
                StatusDetailsList.IsEnabled = true;
                LoadStatusList(StatusDetails);
            }
            else
            {
                MessageBox.Show("Check Date Range and Branch");
            }
            
            
        }


        void LoadStatusList(SALoanStatusView Details)
        {
            StatusDetailsList.Items.Clear();
            List<StatusModal> det = Details.StatusDetails;
            foreach(StatusModal d in det)
            {
                StatusDetailsList.Items.Add(d);
            }
        }
    }
}
