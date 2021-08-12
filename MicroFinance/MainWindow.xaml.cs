using MicroFinance.Modal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
            xHeaderDate.Text = DateTime.Now.ToShortDateString();
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
            LoginDesignation = new LoginDetails(UserName);
            #region Temp code 1,2,3,4 Login
            //if (UserName == "1")
            //{
            //    LoginBorder.Visibility = Visibility.Collapsed;
            //    mainframe.NavigationService.Navigate(new DashboardFieldOfficer());
            //    LoggedInState();
            //    MainWindow.StatusMessageofPage(1, "Ready...");
            //}
            //else if (UserName == "2")
            //{
            //    LoginBorder.Visibility = Visibility.Collapsed;
            //    mainframe.NavigationService.Navigate(new DashboardAccountant());
            //    LoggedInState();
            //    MainWindow.StatusMessageofPage(1, "Ready...");
            //}
            //else if (UserName == "3")
            //{
            //    LoginBorder.Visibility = Visibility.Collapsed;
            //    mainframe.NavigationService.Navigate(new DashboardBranchManager());
            //    LoggedInState();
            //    MainWindow.StatusMessageofPage(1, "Ready...");
            //}
            //else if (UserName == "4")
            //{
            //    LoginBorder.Visibility = Visibility.Collapsed;
            //    mainframe.NavigationService.Navigate(new DashBoardHeadOfficer());
            //    LoggedInState();
            //    MainWindow.StatusMessageofPage(1, "Ready...");
            //}
            #endregion

            if (_userName.ToLower() == "Admin".ToLower())
            {
                LoginBorder.Visibility = Visibility.Collapsed;
                mainframe.NavigationService.Navigate(new DashBoardHeadOfficer());
                LoggedInState();
                MainWindow.StatusMessageofPage(1, "Ready...");
            }
            else
            {
                GetLogin();
            }
            


            //LoginDesignation = new LoginDetails(GetEmployeeName(_userName));
            //if (_userName.ToLower() == "Admin".ToLower())
            //{
            //    LoginBorder.Visibility = Visibility.Collapsed;
            //    mainframe.NavigationService.Navigate(new DashBoardHeadOfficer());
            //    LoggedInState();
            //    MainWindow.StatusMessageofPage(1, "Ready...");
            //}
            //else
            //{
            //    if (CheckUserExist(_userName))
            //    {
            //        string dbPassword = GetPassword(_userName);
            //        if (Password == "GTrust")
            //        {
            //            if (dbPassword == "GTrust")
            //            {
            //                xUserNameP.Text = _userName;
            //                xSetNewPasswordPopUP.Visibility = Visibility.Visible;
            //            }
            //            else
            //            {
            //                MessageBox.Show("Your password has been already changed.");
            //            }
            //        }
            //        else
            //        {
            //            if (dbPassword == Password)
            //            {
            //                GetLogin();
            //            }
            //            else
            //            {
            //                MessageBox.Show("Incorrect Password");
            //            }
            //        }
            //    }
            //    else
            //    {
            //        MessageBox.Show("Invalid Username..!");
            //    }
            //}
        }


        string GetPassword(string userName)
        {
            string thisPassword = string.Empty;
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select Password from Employee where UserName = '"+ userName +"'";
                thisPassword = (string)cmd.ExecuteScalar();
                con.Close();
            }
            return thisPassword;
        }
        
        void GetLogin()
        {
            try
            {
                xHeaderUsername.Text = GetEmployeeName4ID(LoginDesignation.EmpId);
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
                    mainframe.NavigationService.Navigate(new DashBoardRegionOfficer());
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
            xHeaderUsername.Text = string.Empty;
            xLogoutButton.Visibility = Visibility.Collapsed;
            xLoginWindow.Visibility = Visibility.Visible;
            LoginBorder.Visibility = Visibility.Visible;
            mainframe.Content = null;
        }

        private void xLoginButtonP_Click(object sender, RoutedEventArgs e)
        {
            if(xPhoneNumberP.Text != GetMobileNumber(_userName))
            {
                if (xNewPassword1.Text != xNewPassword2.Text)
                {
                    MessageBox.Show("Password does not Matching. Please ReEnter..!");
                }
                MessageBox.Show("This is not your mobile number.");
            }
            else
            {
                SetPassword(_userName, xNewPassword2.Text);
                xUserNameP.Clear();
                xPhoneNumberP.Clear();
                xNewPassword2.Clear();
                xNewPassword1.Clear();
                xPassword.Clear();
                xSetNewPasswordPopUP.Visibility = Visibility.Collapsed;
                MessageBox.Show("Password changed Sucessfully.");
            }
        }

        void SetPassword(string userName, string pwd)
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "update Employee set Password = '" + userName + "' where Name = '" + pwd + "'";
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        string GetMobileNumber(string userName)
        {
            string mobileNo = string.Empty;
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select MobileNo from Employee where Name = '" + userName + "'";
                mobileNo = (string)cmd.ExecuteScalar();
                con.Close();
            }
            return mobileNo;
        }
        bool CheckUserExist(string userName)
        {
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "SELECT Count(UserName) FROM Employee WHERE UserName = '" + userName + "'";
                var count = cmd.ExecuteScalar();
                con.Close();
                return int.Parse(count.ToString()) == 1;
            }
        }

        string GetEmployeeName(string userName)
        {
            string empName = string.Empty;
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "SELECT Name FROM Employee WHERE UserName = '" + userName + "'";
                empName = (string)cmd.ExecuteScalar();
                con.Close();
            }
            return empName;
        }
        string GetEmployeeName4ID(string empId)
        {
            string empName = string.Empty;
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "SELECT Name FROM Employee WHERE EmpId = '" + empId + "'";
                empName = (string)cmd.ExecuteScalar();
                con.Close();
            }
            return empName;
        }
    }
}
