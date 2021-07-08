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
            InitializeComponent(); 
            MessageStatus.DataContext = StatusMsg;
           // mainframe.NavigationService.Navigate(new LoanRecommend());

        }
        public static void StatusMessageofPage(int Type, string Message)
        {
            StatusMsg.MessageType = Type;
            StatusMsg.StatusMessage = Message;
        }
        private void CreEmployee_Click(object sender, RoutedEventArgs e)
        {
            mainframe.NavigationService.Navigate(new AddEmployee());
        }

        private void CrBranch_Click(object sender, RoutedEventArgs e)
        {
            mainframe.NavigationService.Navigate(new CreateBranch());
        }

        private void modifyemployee_Click(object sender, RoutedEventArgs e)
        {
            mainframe.NavigationService.Navigate(new ModifyEmployee());
        }
        private void Addregion_Click(object sender, RoutedEventArgs e)
        {
            AddRegion ad = new AddRegion();
            ad.ShowDialog();

        }

        private void CustomerAdd_Click(object sender, RoutedEventArgs e)
        {
            mainframe.NavigationService.Navigate(new dummypage());
        }

        private void LoanRequestFO_Click(object sender, RoutedEventArgs e)
        {
            mainframe.NavigationService.Navigate(new LoanRequest());

        }

        private void SendtoHimark_Click(object sender, RoutedEventArgs e)
        {
            mainframe.NavigationService.Navigate(new LoanRecommend());
        }

        private void ApprovefromHImark_Click(object sender, RoutedEventArgs e)
        {
            mainframe.NavigationService.Navigate(new LoanAfterHimark());
        }
    }
}
