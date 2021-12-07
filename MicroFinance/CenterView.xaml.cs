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
using MicroFinance.Repository;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for CenterView.xaml
    /// </summary>
    public partial class CenterView : Page
    {
        public CenterView(List<SHGModal> CenterList)
        {
            InitializeComponent();
            CenterGridView.ItemsSource = CenterList;
        }
        public CenterView(List<SHGModal> CenterList,int value)
        {
            InitializeComponent();
            CenterGridView.ItemsSource = CenterList;
            CreateSheduleH.Visibility = Visibility.Collapsed;
        }

        private void CreateSheduleBtn_Click(object sender, RoutedEventArgs e)
        {
            SHGModal SelectedShg = CenterGridView.SelectedItem as SHGModal;
            this.NavigationService.Navigate(new AssignSHG(SelectedShg));
        }

        private void DeActiveCenterBtn_Click(object sender, RoutedEventArgs e)
        {
            SHGModal SelectedShg = CenterGridView.SelectedItem as SHGModal;
            string message = "Are you Sure you Want to Deactive " + SelectedShg.SHGName;
            MessageBoxResult result = MessageBox.Show(message,"Confirm",MessageBoxButton.YesNo,MessageBoxImage.Question);
            if(MessageBoxResult.Yes==result)
            {
                SHGRepository.DeActiveSHG(SelectedShg.SHGId);
                this.NavigationService.Navigate(new DashboardBranchManager());
            }
        }

        private void UpdateCenterBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
