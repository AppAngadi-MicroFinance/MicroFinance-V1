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
    /// Interaction logic for NotificationPage.xaml
    /// </summary>
    public partial class NotificationPage : Page
    {
        ObservableCollection<RecommendView> NotificationList = new ObservableCollection<RecommendView>();
        int CurrentStatus = 0;
        public NotificationPage(int StatusCode)
        {
            InitializeComponent();
            CurrentStatus = StatusCode;
            NotificationList = LoanRepository.GetRecommendList(StatusCode);
            NotificationGrid.ItemsSource = NotificationList;
        }
        public NotificationPage(int StatusCode,string EmpID)
        {
            InitializeComponent();
            CurrentStatus = StatusCode;
            NotificationList = LoanRepository.GetRecommendList(StatusCode,EmpID);
            NotificationGrid.ItemsSource = NotificationList;
        }

        private void NotifyClickBtn_Click(object sender, RoutedEventArgs e)
        {
            RecommendView r = new RecommendView();
            if(NotificationGrid.SelectedItem!=null)
            {
                r = NotificationGrid.SelectedItem as RecommendView;
                this.NavigationService.Navigate(new CustomerVerified(r.CustomerID, CurrentStatus, r.RequestID, r.EmpId));
            }
           
        }
    }
}
