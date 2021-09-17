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
using MicroFinance.ViewModel;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for RecommendNew.xaml
    /// </summary>
    public partial class RecommendNew : Page
    {
        ObservableCollection<RecommendView> RecommendList = new ObservableCollection<RecommendView>();
        public RecommendNew()
        {
            InitializeComponent();
        }
        public RecommendNew(int statusCode)
        {
            InitializeComponent();
            RecommendList = LoanRepository.GetRecommendList(statusCode);
            RecommendGrid.ItemsSource = RecommendList;

        }

        private void SelectAll_CheckBox_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
