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
using MicroFinance.Utils;
using MicroFinance.ViewModel;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static string DriveBasePath = string.Empty;
        public static StringBuilder TimeBuilder = new StringBuilder();
        public static StaticProperty StatusMsg = new StaticProperty();
        string _userName;
        string _password;
        public static LoginDetails LoginDesignation;
        public static BasicDetailsStatic BasicDetails;
        public static LanguageSelector language = new LanguageSelector();
        public static string message;

        public static string ConnectionString = Properties.Settings.Default.DBConnection;
        public MainWindow()
        {
            InitializeComponent();
            SystemFunction.OpenDrive();
            BasicDetails = new BasicDetailsStatic();
            MessageStatus.DataContext = StatusMsg;
            xHeaderDate.Text ="Date: "+ DateTime.Now.ToString("dd-MMM-yyyy");
            DriveBasePath = SystemFunction.DriveBasePath;

            MainGrid.Width= (int)System.Windows.SystemParameters.WorkArea.Width;
            MainGrid.Height= (int)System.Windows.SystemParameters.WorkArea.Height-30;


            SystemFunction.IsTamil = false;

            




        }
        public static void StatusMessageofPage(int Type, string Message)
        {
            StatusMsg.MessageType = Type;
            StatusMsg.StatusMessage = Message;
        }
        
        private void xLogoutButton_Click(object sender, RoutedEventArgs e)
        {
            LogOutState();
            UserProfilePanel.Visibility = Visibility.Collapsed;
        }

        private void xLoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string UserName = xUserName.Text;
                string Password = xPassword.Password;
                // LoginDesignation = new LoginDetails(UserName);
                LoginDesignation = new LoginDetails(UserName, Password);
                if (LoginDesignation.IsRegisteredSystem() == true)
                {

                    _userName = UserName;
                    _password = Password;


                    if (_userName.ToLower() == "Admin".ToLower() && _password == "GTrust123")
                    {
                        LoginBorder.Visibility = Visibility.Collapsed;
                        UserProfilePanel.Visibility = Visibility.Visible;
                        HomeBtn.Visibility = Visibility.Visible;
                        xHeaderUsername.Text = "ADMIN";
                        mainframe.NavigationService.Navigate(new DashBoardHeadOfficer());
                        LoggedInState();
                        message = language.translate(SystemFunction.IsTamil, "W1");//Ready…
                        MainWindow.StatusMessageofPage(1, message);
                    }
                    else
                    {
                        GetLogin();
                        UserProfilePanel.Visibility = Visibility.Visible;
                    }

                }
                else
                {
                    message = language.translate(SystemFunction.IsTamil, "W17");//Unauthorized System .....
                    StatusMessageofPage(0, message);
                }
            }
            catch(Exception ex)
            {
                StatusMessageofPage(0, ex.Message);
            }
           



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
                    message = language.translate(SystemFunction.IsTamil, "W1");//Ready…
                    MainWindow.StatusMessageofPage(1, message);
                }
                else if (power.Equals("ACCOUNTANT"))
                {
                    LoginBorder.Visibility = Visibility.Collapsed;
                    // mainframe.NavigationService.Navigate(new DashboardAccountant());
                    mainframe.NavigationService.Navigate(new DashboardBranchManager());
                    LoggedInState();
                    message = language.translate(SystemFunction.IsTamil, "W1");//Ready…
                    MainWindow.StatusMessageofPage(1, message);
                }
                else if (power.Equals("MANAGER"))
                {
                    LoginBorder.Visibility = Visibility.Collapsed;
                    mainframe.NavigationService.Navigate(new DashboardBranchManager());
                    LoggedInState();
                    message = language.translate(SystemFunction.IsTamil, "W1");//Ready…
                    MainWindow.StatusMessageofPage(1, message);
                }
                else if (power.Equals("REGION MANAGER"))
                {
                    LoginBorder.Visibility = Visibility.Collapsed;
                    mainframe.NavigationService.Navigate(new DashBoardRegionOfficer());
                    LoggedInState();
                    message = language.translate(SystemFunction.IsTamil, "W1");//Ready…
                    MainWindow.StatusMessageofPage(1, message);
                }
                HomeBtn.Visibility = Visibility.Visible;
            }
            catch
            {
                message = language.translate(SystemFunction.IsTamil, "L1");
                StatusMessageofPage(0, message);//Please Enter a Valid Username and Password.....
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
            HomeBtn.Visibility = Visibility.Collapsed;
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
                    message = language.translate(SystemFunction.IsTamil, "L2");//Password does not Matching. Please ReEnter..!
                    MessageBox.Show(message);
                }
                message = language.translate(SystemFunction.IsTamil, "L3");//"This is not your mobile number."
                MessageBox.Show(message);
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
                message = language.translate(SystemFunction.IsTamil, "L4");//Password changed Sucessfully.
                MessageBox.Show(message);
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

        private void HomeBtn_Click(object sender, RoutedEventArgs e)
        {
            string Designation = MainWindow.LoginDesignation.LoginDesignation;
            Designation = (Designation == null) ? "" : Designation;
            LoadHomePage(Designation);
        }

        public void LoadHomePage(string Designation)
        {
            if (Designation.Equals("Field Officer"))
                mainframe.NavigationService.Navigate(new DashboardFieldOfficer());
            else if (Designation.Equals("Accountant"))
                mainframe.NavigationService.Navigate(new DashboardAccountant());
            else if (Designation.Equals("Branch Manager") || Designation.Equals("Manager"))
                mainframe.NavigationService.Navigate(new DashboardBranchManager());
            else if (Designation.Equals("Region Manager"))
                mainframe.NavigationService.Navigate(new DashBoardRegionOfficer());
            else
                mainframe.NavigationService.Navigate(new DashBoardHeadOfficer());
        }

        private void TamilCheckbox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox box = sender as CheckBox;
            if(box.IsChecked==true)
            {
                SystemFunction.IsTamil = true;
            }
            else if(box.IsChecked==false)
            {
                SystemFunction.IsTamil = false;
            }
        }
    }
}
