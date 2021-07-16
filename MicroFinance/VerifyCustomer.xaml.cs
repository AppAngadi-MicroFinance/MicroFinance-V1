using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for VerifyCustomer.xaml
    /// </summary>
    public partial class VerifyCustomer : Page
    {
        Notification OverallObj = new Notification();
        Customer customer = new Customer();
        Guarantor guarantor = new Guarantor();
        Nominee nominee = new Nominee();
        int _status;
        string CustomerId;
        List<BranchDetails> branchdetails = new List<BranchDetails>();
        public VerifyCustomer(Notification notificationDetail,int Status)
        {
            _status = Status;
            OverallObj = notificationDetail;
            CustomerId = OverallObj.CustomerId;
            InitializeComponent();

            if(_status==2)
            {
                VerifyCustomerBtn.Content = "Approve";
                Reject.Visibility = Visibility.Visible;
            }
            GetBranchDetails();
            FillDetails();
            
            BranchDetailsGrid.DataContext = branchdetails;
            CustomerGrid.DataContext = customer;
            AddressGrid.DataContext = customer;
            GurantorGrid.DataContext = guarantor;
            NomineeGrid.DataContext = nominee;
            AddressGrid.DataContext =customer;
            PhotoProofGrid.DataContext = customer;
            ProfilePhoto.Source = customer.ProfilePicture;
            ViewAccountdetailsPanel.DataContext = customer;
        }
        void FillDetails()
        {
            customer._customerId = CustomerId;
            guarantor._customerId = CustomerId;
            nominee._customerId = CustomerId;
            customer.GetAllDetailsofCustomers();
            guarantor.GetGuranteeDetails();
            nominee.GetNomineeDetails();
        }
        private void ViewGaurantor_Click(object sender, RoutedEventArgs e)
        {
            ViewGuarantorPopopup.IsOpen = true;
            ViewGuarantorGrid.DataContext = guarantor;
        }

        private void ViewNominee_Click(object sender, RoutedEventArgs e)
        {
            ViewNomineePopopup.IsOpen = true;
            ViewNomineeDetails.DataContext = nominee;
        }

        private void ViewAddressProof_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Address Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = customer.AddressProof;
        }

        private void ViewPhotoProof_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Photo Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = customer.PhotoProof;
        }

        private void ViewAddressProofOfGuarantor_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Address Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = guarantor.AddressProof;
        }

        private void ViewGuarantorAddPhotoProof_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Photo Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = guarantor.PhotoProof;
        }

        private void ViewGuarantorAddProfilePhoto_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Profile Picture";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = guarantor.ProfilePicture;
        }

        private void NomineeViewAddressProof_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Address Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = nominee.AddressProof;
        }

        private void NomineeViewPhotoProof_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Photo Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = nominee.PhotoProof;
        }

        private void NomineeViewProfilePicture_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Profile Picture";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = nominee.ProfilePicture;
        }

        private void VerifyCustomerBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (VerifyCustomerBtn.Content.Equals("Approve"))
                {
                    using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
                    {
                        sql.Open();
                        SqlCommand command = new SqlCommand();
                        command.Connection = sql;
                        command.CommandText = "update CustomerDetails set CustomerStatus='3' where CustId='" + CustomerId + "'";
                        command.ExecuteNonQuery();
                    }
                    MainWindow.StatusMessageofPage(1, "Successfully Customer Recommended...");
                    NavigationService.GetNavigationService(this).Navigate(new CustomerNotification(1));
                }
                else
                {
                    using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
                    {
                        sql.Open();
                        SqlCommand command = new SqlCommand();
                        command.Connection = sql;
                        command.CommandText = "update CustomerDetails set CustomerStatus='2' where CustId='" + CustomerId + "'";
                        command.ExecuteNonQuery();
                    }
                    MainWindow.StatusMessageofPage(1, "Successfully Customer Approved...");
                    NavigationService.GetNavigationService(this).Navigate(new CustomerNotification(2));
                }
            }
            catch
            {
                MainWindow.StatusMessageofPage(2, "Check Correctly...");
            }
        }
        void GetBranchDetails()
        {
            string _region="";
            string _branch="";
            string _fieldofficer="";
            string _Shg="";
            string _pg="";
            using(SqlConnection connection=new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "select BranchDetails.RegionName,BranchDetails.BranchName,CustomerGroup.SelfHelpGroup,CustomerGroup.PeerGroup from CustomerGroup join BranchDetails on BranchDetails.Bid=CustomerGroup.BranchId where CustId='" + CustomerId + "'";
                SqlDataReader dataReader = command.ExecuteReader();
                while(dataReader.Read())
                {
                    _region = dataReader.GetString(0);
                    _branch = dataReader.GetString(1);
                    _Shg = dataReader.GetString(2);
                    _pg = dataReader.GetString(3);
                }
                dataReader.Close();
                command.CommandText = "select Employee.Name from BranchEmployees join Employee on BranchEmployees.Empid=Employee.EmpId where BranchEmployees.Bid=(select distinct BranchId from CustomerGroup where CustId='" + CustomerId + "') and BranchEmployees.Designation='Field Officer'";
                dataReader = command.ExecuteReader();
                while(dataReader.Read())
                {
                    _fieldofficer = dataReader.GetString(0);
                }
                dataReader.Close();
            }
            branchdetails.Add(new BranchDetails { RegionName = _region, BranchName = _branch, FieldOfficerName = _fieldofficer, SHGName = _Shg, PGName = _pg });
        }

        private void ViewGuarantorOk_Click(object sender, RoutedEventArgs e)
        {
            ViewGuarantorPopopup.IsOpen = false;
        }

        private void ViewNomineeok_Click(object sender, RoutedEventArgs e)
        {
            ViewNomineePopopup.IsOpen = false;
        }

        private void ProfileImageOk_Click(object sender, RoutedEventArgs e)
        {
            ViewImagePopup.IsOpen = false;
        }

        private void ViewBank_Click(object sender, RoutedEventArgs e)
        {
            ViewAccountdetailsPanel.IsOpen = true;
        }

        private void BankOk_Click(object sender, RoutedEventArgs e)
        {
            ViewAccountdetailsPanel.IsOpen = false;
        }

        private void PanelCloseBtn_Click(object sender, RoutedEventArgs e)
        {
            ViewAccountdetailsPanel.IsOpen = false;
        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "update CustomerDetails set CustomerStatus='4' where CustId='" + CustomerId + "'";
                command.ExecuteNonQuery();
            }
            MainWindow.StatusMessageofPage(1, "Successfully Customer Rejected...");
            NavigationService.GetNavigationService(this).Navigate(new CustomerNotification(2));
        }
    }
    class BranchDetails
    {
        public string RegionName { get; set; }
        public string BranchName { get; set; }
        public string FieldOfficerName { get; set; }
        public string SHGName { get; set;}
        public string PGName { get; set; }
    }
}
