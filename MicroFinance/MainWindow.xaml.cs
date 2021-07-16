using MicroFinance.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        string _userName;
        public static LoginDetails LoginDesignation;

        public static string ConnectionString = Properties.Settings.Default.db;
        public MainWindow()
        {
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
            _userName = UserName;
            try
            {
                LoginDesignation = new LoginDetails(_userName);
                //int power = int.Parse(UserName);
                string power = LoginDesignation.LoginDesignation;
                power = power.ToUpper();
                if (power.Equals("FIELD OFFICER"))
                {
                    LoginBorder.Visibility = Visibility.Collapsed;
                    mainframe.NavigationService.Navigate(new DashboardFieldOfficer());
                    LoggedInState();
                    MainWindow.StatusMessageofPage(1, "Ready...");
                }
                else if (power.Equals("ACCOUNTANT"))
                {
                    LoginBorder.Visibility = Visibility.Collapsed;
                    mainframe.NavigationService.Navigate(new DashboardAccountant());
                    LoggedInState();
                    MainWindow.StatusMessageofPage(1, "Ready...");
                }
                else if (power.Equals("MANAGER"))
                {
                    LoginBorder.Visibility = Visibility.Collapsed;
                    mainframe.NavigationService.Navigate(new DashboardBranchManager());
                    LoggedInState();
                    MainWindow.StatusMessageofPage(1, "Ready...");
                }
                else if (power.Equals("REGION MANAGER"))
                {
                    LoginBorder.Visibility = Visibility.Collapsed;
                    mainframe.NavigationService.Navigate(new DashBoardHeadOfficer());
                    LoggedInState();
                    MainWindow.StatusMessageofPage(1, "Ready...");
                }
            }
            catch
            {
                StatusMessageofPage(0, "Please Valid User Name.....");
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
