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
        public VerifyCustomer(Notification notificationDetail,int Status)
        {
            _status = Status;
            InitializeComponent();

            if(_status==2)
            {
                VerifyCustomerBtn.Content = "Approve";
            }

            OverallObj = notificationDetail;
            FillDetails();
            
            BranchDetailsGrid.DataContext = customer;
            CustomerGrid.DataContext = customer;
            AddressGrid.DataContext = customer;
            GurantorGrid.DataContext = guarantor;
            NomineeGrid.DataContext = nominee;
            AddressGrid.DataContext =customer;
            PhotoProofGrid.DataContext = customer;
            ProfilePhoto.Source = customer.ProfilePicture;
        }
        string CustomerId;
        void FillDetails()
        {
            CustomerId = OverallObj.CustomerId;
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
            if (VerifyCustomerBtn.Content.Equals("Approve"))
            {

            }
            else
            {

            }
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
    }
}
