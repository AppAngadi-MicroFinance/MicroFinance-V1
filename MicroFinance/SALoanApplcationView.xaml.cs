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
            BranchCombo.ItemsSource = MainWindow.BasicDetails.BranchList;
        }

        private async void OkBtn_Click(object sender, RoutedEventArgs e)
        {
            if(BranchCombo.SelectedItem!=null)
            {
                StatusDetailsList.Items.Clear();
                SALoanStatusView StatusDetails = new SALoanStatusView();
                
                BranchViewModel SelectedBranch = BranchCombo.SelectedItem as BranchViewModel;
                BranchNameText.Text = SelectedBranch.BranchName;
                GifPanel.Visibility = Visibility.Visible;
                await System.Threading.Tasks.Task.Run(()=>StatusDetails = SARepository.GetApplicationStatusDetails(SelectedBranch.BranchId));
                GifPanel.Visibility = Visibility.Collapsed;
                StatusDetailsList.IsEnabled = true;
                LoadStatusList(StatusDetails);
                BranchCombo.SelectedIndex = -1;
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
