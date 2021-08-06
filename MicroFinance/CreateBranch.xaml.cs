using MicroFinance.Modal;
using MicroFinance.Validations;
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
    /// Interaction logic for CreateBranch.xaml
    /// </summary>
    public partial class CreateBranch : Page
    {
        Branch CB;
        public List<string> Regionlist;
        StringBuilder Emptyfields = new StringBuilder();
        StringBuilder Requiredfields = new StringBuilder();
        public CreateBranch()
        {
            InitializeComponent();
            CB = new Branch();
            CB.GetRegionList();
            MainGrid.DataContext = CB;
            Regionlist = CB.RegionList;
            RegionBox.ItemsSource = Regionlist; 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BranchAccountdetailsPanel.IsOpen = true;
            MainGrid.IsEnabled = false;
            MainGrid.Opacity = 0.4;
        }

        private void close_Click(object sender, RoutedEventArgs e)
        {
            BranchAccountdetailsPanel.IsOpen = false;
            MainGrid.Opacity = 1.0;
        }

        private void Savebtn_Click(object sender, RoutedEventArgs e)
        {
            IsemptyCheck();
            if(Requiredfields.Length==0)
            {
                if(Emptyfields.Length==0)
                {
                    ConfirmPanel.IsOpen = true;
                    MainGrid.Opacity = 0.4;
                    MainGrid.IsEnabled = false;
                }
                else
                {
                   if(MessageBox.Show("Check These fields are Empty\n"+Emptyfields.ToString()+"Are you sure You want to create Branch Without These Information","Warning",MessageBoxButton.YesNo,MessageBoxImage.Exclamation)==MessageBoxResult.Yes)
                   {
                        ConfirmPanel.IsOpen = true;
                        MainGrid.Opacity = 0.4;
                        MainGrid.IsEnabled = false;
                   }
                }
            }
            else
            {
                MessageBox.Show("These Fields are Mandatory Please Fill All these Fields\n" + Requiredfields.ToString(),"Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        private void Cancelbtn_Click(object sender, RoutedEventArgs e)
        {
            ConfirmPanel.IsOpen = false;
            MainGrid.IsEnabled = true;
            MainGrid.Opacity = 1.0;
            
        }

        private void PanelCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            BranchAccountdetailsPanel.IsOpen = false;
            MainGrid.IsEnabled = true;
            MainGrid.Opacity = 1.0;
        }

        private void CreateBr_Click(object sender, RoutedEventArgs e)
        {
            ConfirmPanel.IsOpen = false;
            
            try
            {
                if(!CB.IsExists())
                {
                    CB.AddBranch(RegionBox.Text);
                    MainGrid.IsEnabled = true;
                    MainGrid.Opacity = 1.0;
                    MainWindow.StatusMessageofPage(1, "Branch Created Successfully");
                }
                else
                {
                    MainWindow.StatusMessageofPage(0, "Branch AlredyExits...");
                }

                this.NavigationService.Navigate(new CreateBranch());
            }
            catch(Exception ex)
            {
                ConfirmPanel.IsOpen = false;
                MainGrid.IsEnabled = true;
                MainWindow.StatusMessageofPage(1,ex.Message);
               // MessageBox.Show(ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        public void IsemptyCheck()
        {
            Requiredfields = new StringBuilder();
            Emptyfields = new StringBuilder();
            if(RegionBox.Text=="")
            {
                Requiredfields.Append("Region Name*\n");
            }
            if (BranchnameBox.Text == "")
            {
                Requiredfields.Append("Branch Name*\n");
            }
            if (AddressBox.Text == "")
            {
                Requiredfields.Append("Branch Address*\n");
            }
            if (LandlineBox.Text == "")
            {
                Emptyfields.Append("Landline\n");
            }
            if (CostpermonthBox.Text == "")
            {
                Emptyfields.Append("Landline Cost\n");
            }
            if (openingdate.Text == "")
            {
                Requiredfields.Append("Opening Date*\n");
            }
            if (Ebnumberbox.Text == "")
            {
                Requiredfields.Append("EB Number*\n");
            }
            if (ebconnectionnamebox.Text == "")
            {
                Requiredfields.Append("EB Connection Name*\n");
            }
            if (internetnamebox.Text == "")
            {
                Emptyfields.Append("Internet Name\n");
            }
            if (internetcostbox.Text == "")
            {
                Emptyfields.Append("Internet Cost\n");
            }
            if (ownernamebox.Text == "")
            {
                Requiredfields.Append("Owner Name*\n");
            }
            if (ownercontactnumberbox.Text == "")
            {
                Requiredfields.Append("Owner Contact Number*\n");
            }
            if (owneraddressbox.Text == "")
            {
                Requiredfields.Append("Owner Address*\n");
            }
            if (Advancepaidbox.Text == ""||Advancepaidbox.Text=="0")
            {
                Requiredfields.Append("Advance*\n");
            }
            if (monthrentbox.Text == "")
            {
                Requiredfields.Append("Rent Per Month*\n");
            }
            if (accountholdernamebox.Text == "")
            {
                Emptyfields.Append("Account Holder Name\n");
            }
            if (acccountnumberbox.Text == "")
            {
                Emptyfields.Append("Account Number\n");
            }
            if (banknamebox.Text == "")
            {
                Emptyfields.Append("Bank Name\n");
            }
            if (bankbranchnamebox.Text == "")
            {
                Emptyfields.Append("Bank Branch Name\n");
            }
            if (ifscbox.Text == "")
            {
                Emptyfields.Append("IFSC Code\n");
            }
            if (micrbox.Text == "")
            {
                Emptyfields.Append("MICR Code\n");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            BranchAccountdetailsPanel.IsOpen = false;
            MainGrid.IsEnabled = true;
        }

        private void BranchnameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            bool result= CB.IsExists();
            if(result==true)
            {
                MessageBox.Show("Branch Already Exists in this Region...", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                this.NavigationService.Navigate(new CreateBranch());
            }
        }

        private void xBackwardButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
                this.NavigationService.GoBack();
        }
    }
}
