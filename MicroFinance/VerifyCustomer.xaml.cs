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
        public VerifyCustomer(Notification notificationDetail)
        {
            InitializeComponent();
       //     OverallObj = notificationDetail;
       //     BranchDetailsGrid.DataContext = notificationDetail.CustomerObj;
       //     CustomerGrid.DataContext = notificationDetail.CustomerObj;
       //     AddressGrid.DataContext = notificationDetail.CustomerObj;
       //     GurantorGrid.DataContext = notificationDetail.GuarantorObj;
       //     NomineeGrid.DataContext = notificationDetail.NomineeObj;
       //     AddressGrid.DataContext = notificationDetail.CustomerObj;
       //     PhotoProofGrid.DataContext= notificationDetail.CustomerObj;
       ////     PhotoProfileGrid.DataContext= notificationDetail.CustomerObj;
       //     ProfilePhoto.Source = notificationDetail.CustomerObj.ProfilePicture;
        }

        private void ViewGaurantor_Click(object sender, RoutedEventArgs e)
        {
            ViewGuarantorPopopup.IsOpen = true;
            ViewGuarantorGrid.DataContext = OverallObj.GuarantorObj;
        }

        private void ViewNominee_Click(object sender, RoutedEventArgs e)
        {
            ViewNomineePopopup.IsOpen = true;
            ViewNomineeDetails.DataContext = OverallObj.NomineeObj;
        }

        private void ViewAddressProof_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Address Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = OverallObj.CustomerObj.AddressProof;
        }

        private void ViewPhotoProof_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Photo Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = OverallObj.CustomerObj.PhotoProof;
        }

        private void ViewAddressProofOfGuarantor_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Address Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = OverallObj.GuarantorObj.AddressProof;
        }

        private void ViewGuarantorAddPhotoProof_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Photo Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = OverallObj.GuarantorObj.PhotoProof;
        }

        private void ViewGuarantorAddProfilePhoto_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Profile Picture";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = OverallObj.GuarantorObj.ProfilePicture;
        }

        private void NomineeViewAddressProof_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Address Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = OverallObj.NomineeObj.AddressProof;
        }

        private void NomineeViewPhotoProof_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Photo Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = OverallObj.NomineeObj.PhotoProof;
        }

        private void NomineeViewProfilePicture_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Profile Picture";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = OverallObj.NomineeObj.ProfilePicture;
        }

        private void VerifyCustomerBtn_Click(object sender, RoutedEventArgs e)
        {

            RegionManagerWindow RMW = new RegionManagerWindow();
            if (VerifyCustomerBtn.Content.Equals("Approve"))
            {
                MessageBox.Show("Successfully Customer Added");
            }
            else
            {
                RMW.ShowDialog();
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
    }
}
