using MicroFinance.Modal;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static StaticProperty StatusMsg = new StaticProperty();
        public static LoginDetails LoginDesignation = new LoginDetails();
        public MainWindow()
        {
            LoginDesignation.LoginDesignation = "Field Officer";
            LoginDesignation.EmpId = "0100220210702";
            LoginDesignation.BranchId = "01202106002";
            LoginDesignation.RegionName = "Trichy";
            InitializeComponent();
            MessageStatus.DataContext = StatusMsg;
        }
        public static void StatusMessageofPage(int Type, string Message)
        {
            StatusMsg.MessageType = Type;
            StatusMsg.StatusMessage = Message;
        }
        
        private void xLogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LogOutState();
        }

        private void xLoginButton_Click(object sender, RoutedEventArgs e)
        {

            string UserName = xUserName.Text;
            string Password = xPassword.Password;
            int power = int.Parse(UserName);
            if (power == 1)
            {
                LoginBorder.Visibility = Visibility.Collapsed;
                mainframe.NavigationService.Navigate(new DashboardFieldOfficer());
                LoggedInState();
            }
            else if(power == 2)
            {
                LoginBorder.Visibility = Visibility.Collapsed;
                mainframe.NavigationService.Navigate(new DashboardAccountant());
                LoggedInState();
            }
            else if (power == 3)
            {
                LoginBorder.Visibility = Visibility.Collapsed;
                mainframe.NavigationService.Navigate(new DashboardBranchManager());
                LoggedInState();
            }
            else if (power == 4)
            {
                LoginBorder.Visibility = Visibility.Collapsed;
                mainframe.NavigationService.Navigate(new DashBoardHeadOfficer());
                LoggedInState();
            }
            
        }

        void LoggedInState()
        {
            xLoginWindow.Visibility = Visibility.Collapsed;
            xUserName.Clear();
            xPassword.Clear();

            xLogoutButton.Visibility = Visibility.Visible;
        }
        void LogOutState()
        {
            xLogoutButton.Visibility = Visibility.Collapsed;
            xLoginWindow.Visibility = Visibility.Visible;
            LoginBorder.Visibility = Visibility.Visible;
            mainframe.Content = null;
        }
    }
}
