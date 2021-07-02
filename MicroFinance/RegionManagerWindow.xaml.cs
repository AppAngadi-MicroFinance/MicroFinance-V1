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
using System.Windows.Shapes;
using MicroFinance.Modal;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for RegionManagerWindow.xaml
    /// </summary>
    public partial class RegionManagerWindow : Window
    {
        public RegionManagerWindow()
        {
            InitializeComponent();
            Notification NTF = new Notification();
            NTF.NotificationFrom = MainWindow.LoginDesignation.LoginDesignation;
            NTF.NotificationPurpose = "Approve Customer To Add";
            NTF.NotificationDate = DateTime.Today.ToShortDateString();
            NTF.CustomerObj = AddCustomer.customer;
            NTF.GuarantorObj = AddCustomer.guarantor;
            NTF.NomineeObj = AddCustomer.nominee;
            RegionManagerNotifyPanel BMNF = new RegionManagerNotifyPanel(NTF);
            frame.Navigate(BMNF, UriKind.Relative);
        }
    }
}
