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

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for RegionManagerNotifyPanel.xaml
    /// </summary>
    public partial class RegionManagerNotifyPanel : Page
    {
        Notification notificationObj = new Notification();
        public RegionManagerNotifyPanel(Notification AllNotificationsList)
        {
            InitializeComponent();
            notificationObj = AllNotificationsList;
            NotificationList.ItemsSource = new List<Notification> { notificationObj };
        }
        private void NotificationList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            VerifyCustomer verifyCustomer = new VerifyCustomer(notificationObj);
            verifyCustomer.VerifyCustomerBtn.Content = "Approve";
            NavigationService.GetNavigationService(this).Navigate(verifyCustomer);
        }
    }
}
