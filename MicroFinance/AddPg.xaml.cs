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
    /// Interaction logic for AddPg.xaml
    /// </summary>
    public partial class AddPg : Window
    {
        public AddPg()
        {
            InitializeComponent();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddCustomer addCustomer = new AddCustomer();
            AddPeerGroup APG = new AddPeerGroup();
            APG.Branch= addCustomer.SelectBranch.SelectedItem.ToString();
            APG.FieldOfficer = addCustomer.SelectFO.SelectedItem.ToString();
            APG.SelfHelpGroup = addCustomer.SelectSHG.SelectedItem.ToString();
            APG.PeerGroup = GroupNameBox.Text;

            //adding Process

            //recommend Notification process



        }
    }
}
