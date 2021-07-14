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
            InitializeComponent();
            MessageStatus.DataContext = StatusMsg;
            
        }
        public static void StatusMessageofPage(int Type, string Message)
        {
            StatusMsg.MessageType = Type;
            StatusMsg.StatusMessage = Message;
        }


        //Login
        private void xLoginButton_Click(object sender, RoutedEventArgs e)
        {
            Navigate();
        }

        
        public void Navigate()
        {
            string UserName = xUserName.Text;
            string Password = xPassword.Text;
            if (Validation(UserName, Password))
            {
                //int power = GetDesignation();
                int power = int.Parse(UserName);
                if (power == 1)
                {
                    mainframe.NavigationService.Navigate(new DashboardFieldOfficer());
                }
                else if (power == 2)
                {
                    mainframe.NavigationService.Navigate(new DashboardAccountant());
                }
                else if(power == 3)
                {
                    mainframe.NavigationService.Navigate(new DashboardBranchManager());
                }
                else if(power == 4)
                {
                    mainframe.NavigationService.Navigate(new DashBoardHeadOfficer());
                }
                LogInState();
            }
        }

        void LogInState()
        {
            xLogoutButton.Visibility = Visibility.Visible;
            xLoginWindow.Visibility = Visibility.Collapsed;

            xUserName.Text = string.Empty;
            xPassword.Text = string.Empty;
        }

        void LogOutState()
        {
            xLogoutButton.Visibility = Visibility.Collapsed;
            xLoginWindow.Visibility = Visibility.Visible;
        }


        public bool Validation(string userName, string passWord)
        {
            return true;
        }


        public int GetDesignation()
        {
            return 1;
        }

        private void xLogoutButton_Click(object sender, RoutedEventArgs e)
        {
            mainframe.Content = null;
            LogOutState();
        }
    }
}
