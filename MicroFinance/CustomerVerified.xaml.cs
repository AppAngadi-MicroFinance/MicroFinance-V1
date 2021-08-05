﻿using MicroFinance.Modal;
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
    /// Interaction logic for CustomerVerified.xaml
    /// </summary>
    public partial class CustomerVerified : Page
    {
        string _branchName;
        string _regionName;
        string _shgName;
        string _pgName;

        public CustomerVerified(string CustId)
        {
            InitializeComponent();

            customerNamegrid.DataContext = customer;
            Guarantorgrid.DataContext = guarantor;
        }
        int CustomerStatus;
        public CustomerVerified(Customer Cs,Guarantor Gu,Nominee No,int status,string RegionName,string BranchName,string SHGName,string PG)
        {
            InitializeComponent();

            _regionName = RegionName;
            _branchName = BranchName;
            _shgName = SHGName;
            _pgName = PG;

            Cs.GetAllDetailsofCustomers();
            Gu.GetGuranteeDetails();
            No.GetNomineeDetails();
            customer = Cs;
            guarantor = Gu;
            nominee = No;
            CustomerStatus = status;

            ContextAssigning();
            VisiblityOfPhotoPanel();
        }
        Customer customer = new Customer();
        Guarantor guarantor = new Guarantor();
        Nominee nominee = new Nominee();

        void ContextAssigning()
        {
            customerNamegrid.DataContext = customer;
            Guarantorgrid.DataContext = guarantor;
            customerAddressGrid.DataContext = customer;
            BankDetailsGrid.DataContext = customer;
            NomineeGrid.DataContext = nominee;
        }

        void VisiblityOfPhotoPanel()
        {
            if(CustomerStatus==0)
            {
                DocumentPanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                DocumentPanel.Visibility = Visibility.Visible;
            }
        }

        void DataContextForPhotos()
        {

            customerAddressProofgrid.DataContext = customer;

            //customerPhotoProofGrid.DataContext = verification;
            customerPhotoProofGrid.DataContext = customer;

            //customerProfileGrid.DataContext = verification;
            customerProfileGrid.DataContext = customer;

           // guarantorAddressProofGrid.DataContext = verification;
            guarantorAddressProofGrid.DataContext = guarantor;

           // guarantorPhotoProofGrid.DataContext = verification;
            guarantorPhotoProofGrid.DataContext = guarantor;

           // guarantorprofilegrid.DataContext = verification;
            guarantorprofilegrid.DataContext = guarantor;

            //nomieeAddressProofGrid.DataContext = verification;
            nomieeAddressProofGrid.DataContext = nominee;

           // nomineePhotoproofGrid.DataContext = verification;
            nomineePhotoproofGrid.DataContext = nominee;

           // nomineeprofileGrid.DataContext = verification;
            nomineeprofileGrid.DataContext = nominee;

           // combineGrid.DataContext = verification;
            combineGrid.DataContext = customer;
        }
     

        private void custNameVeriedBtn_Click(object sender, RoutedEventArgs e)
        {
            customer.CustName = true;
        }

        private void GenderVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustGender = true;
        }

        private void CustDobVeification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustDateOfBirth = true;
        }

        private void CustFatherVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustFatherName = true;
        }

        private void CustMotherVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustMotherName = true;
        }

        private void CustHusbandVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustHusbandName = true;
        }

        private void CustContactVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustContactNumber = true;
        }

        private void CustAadharVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustAadharNumber = true;
        }

        private void CustReligionVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustReligion = true;
        }

        private void CustCommunityVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustCommunity = true;
        }

        private void CustCasteVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustCaste = true;
        }

        private void CustEducationVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustEducation = true;
        }

        private void CustFamilyMemberVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustEarningMember = true;
            customer.CustFamilyMember = true;
        }

        private void CustOccupationVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustOccupation = true;
        }

        private void CustMonthlyVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustMonthlyExpenses = true;
            customer.CustMonthlyIncome = true;
        }

        private void CustYearlyVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustFamilyYearlyIncome = true;
        }

        private void CustDoorNoVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustomerDoorNumber = true;
        }

        private void CustStreetVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustStreetName = true;
        }

        private void CustLocalityVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustomerLocality = true;
        }

        private void CustCityVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustomerCity = true;
        }

        private void CustStateVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustomerState = true;
        }

        private void CustPincodeVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustomerPincode = true;
        }

        private void CustHousingTypeVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustomerHousingType = true;
        }

        private void GNameVerification_Click(object sender, RoutedEventArgs e)
        {
            guarantor.GName = true;
        }

        private void GGenderVerification_Click(object sender, RoutedEventArgs e)
        {
            guarantor.GuarantorGender = true;
        }

        private void GDobVerification_Click(object sender, RoutedEventArgs e)
        {
            guarantor.GuarantorDOB = true;
        }

        private void GContactVerification_Click(object sender, RoutedEventArgs e)
        {
            guarantor.GuarantorContact = true;
        }

        private void GOccupationVerification_Click(object sender, RoutedEventArgs e)
        {
            guarantor.GuarantorOccupation = true;
        }

        private void GRelationshipVerification_Click(object sender, RoutedEventArgs e)
        {
            guarantor.GuarantorRelationship = true;
        }

        private void GDoorNoVerification_Click(object sender, RoutedEventArgs e)
        {
            guarantor.GuarantorDoorNumber = true;
        }

        private void GStreetVerification_Click(object sender, RoutedEventArgs e)
        {
            guarantor.GuarantorStreet = true;
        }

        private void GLocalityVerification_Click(object sender, RoutedEventArgs e)
        {
            guarantor.GuarantorLocality = true;
        }

        private void GCityVerification_Click(object sender, RoutedEventArgs e)
        {
            guarantor.GuarantorCity = true;
        }

        private void GStateVerification_Click(object sender, RoutedEventArgs e)
        {
            guarantor.GuarantorState = true;
        }

        private void GPincodeVerification_Click(object sender, RoutedEventArgs e)
        {
            guarantor.GuarantorPincode = true;
        }

        private void NNameVerification_Click(object sender, RoutedEventArgs e)
        {
            nominee.NName = true;
        }

        private void NGenderVerirication_Click(object sender, RoutedEventArgs e)
        {
            nominee.NomineeGender = true;
        }

        private void NDobVerification_Click(object sender, RoutedEventArgs e)
        {
            nominee.NomineeDOB = true;
        }

        private void NcontactVerification_Click(object sender, RoutedEventArgs e)
        {
            nominee.NomineeContact = true;
        }

        private void NOccupationVerification_Click(object sender, RoutedEventArgs e)
        {
            nominee.NomineeOccupation = true;
        }

        private void NRelationVerification_Click(object sender, RoutedEventArgs e)
        {
            nominee.NomineeRelationship = true;
        }

        private void NDoorVerification_Click(object sender, RoutedEventArgs e)
        {
            nominee.NomineeDoorNo = true;
        }

        private void NStreetVerification_Click(object sender, RoutedEventArgs e)
        {
            nominee.NomineeStreet = true;
        }

        private void NLocalityVerification_Click(object sender, RoutedEventArgs e)
        {
            nominee.NomineeLocality = true;
        }

        private void NCityVErification_Click(object sender, RoutedEventArgs e)
        {
            nominee.NomineeCity = true;
        }

        private void NStateVerification_Click(object sender, RoutedEventArgs e)
        {
            nominee.NomineeState = true;
        }

        private void NPincodeVerification_Click(object sender, RoutedEventArgs e)
        {
            nominee.NomineePincode = true;
        }

        private void HolderVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.BankHolderName = true;
        }

        private void AccountNoVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.BankAccountNo = true;
        }

        private void BankNameVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.Bankname = true;
        }

        private void BranchNameVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.BranchName = true;
        }

        private void IFscVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.BIfscCode = true;
        }

        private void MicrVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.BMicrCode = true;
        }

        private void CustAddressProofVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustomerAddressProof = true;
        }

        private void CustPhotoProofVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustomerPhotoProof = true;
        }

        private void CustProfileVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.CustomerProfilePicture = true;
        }

        private void GAddressProofVerification_Click(object sender, RoutedEventArgs e)
        {
            guarantor.GuarantorAddressProof = true;
        }

        private void GPhotoProofVerification_Click(object sender, RoutedEventArgs e)
        {
            guarantor.GuarantorPhotoProof = true;
        }

        private void GProfileVerification_Click(object sender, RoutedEventArgs e)
        {
            guarantor.GuarantorProfilePicture = true;
        }

        private void NAddressProofVerification_Click(object sender, RoutedEventArgs e)
        {
            nominee.NomineeAddressProof = true;
        }

        private void NPhotoProofVerification_Click(object sender, RoutedEventArgs e)
        {
            nominee.NomineePhotoProof = true;
        }

        private void NProfileVerification_Click(object sender, RoutedEventArgs e)
        {
            nominee.NomineeProfilePicture = true;
        }

        private void CombinePhotoVerification_Click(object sender, RoutedEventArgs e)
        {
            customer.Combinephoto = true;
        }
        public static StaticProperty CaptureImageMessage = new StaticProperty(); 
        Capture CapturePhoto = new Capture();
        string WhichClassButtonClick;

        

        private void ImageSavebtn_Click(object sender, RoutedEventArgs e)
        {
            BitmapImage image = Capture.SavedImage;
            if (image != null)
            {
                string txt = PhotoProofNametxt.Text;
                SetImage(image, txt);
                CaptureImage.Visibility = Visibility.Collapsed;
                Capture.SavedImage = null;
                //    MessageBox.Show("Photo Added Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
               // StatusMessageWhileCapturingImage(0, "Please Capture or Select Photo To Save....");
                //CaptureImageStatus.Text = "Please Capture or Select Photo";
            }
        }
        void SetImage(BitmapImage image, string imagename)
        {

            if (WhichClassButtonClick.Equals("Customer"))
            {
                switch (imagename)
                {
                    case "Address Proof":
                        customer.AddressProof = image;
                        CustAddressProofError.Visibility = Visibility.Collapsed;
                        customer.CustomerAddressProof = false;
                        MainWindow.StatusMessageofPage(1, "Successfully Customer Address Proof Added...");
                        break;
                    case "Photo Proof":
                        customer.PhotoProof = image;
                        CustPhotoProofError.Visibility = Visibility.Collapsed;
                        MainWindow.StatusMessageofPage(1, "Successfully Customer Photo Proof Added...");
                        break;
                    case "Profile Picture":
                        {
                            customer.ProfilePicture = image;
                            CustProfilePictrueError.Visibility = Visibility.Collapsed;
                            MainWindow.StatusMessageofPage(1, "Successfully Customer Profile Picture Added...");
                        }
                        break;
                    case "Combine Photo":
                        {
                            customer.CombinePhoto = image;
                            CustProfilePictrueError.Visibility = Visibility.Collapsed;
                            MainWindow.StatusMessageofPage(1, "Successfully Customer Profile Picture Added...");
                        }
                        break;
                }
            }
            else if (WhichClassButtonClick.Equals("Guarantor"))
            {
                switch (imagename)
                {
                    case "Address Proof":
                        guarantor.AddressProof = image;
                        GAddressProofError.Visibility = Visibility.Collapsed;
                        //MainWindow.StatusMsg.MessageType = 1;
                        //MainWindow.StatusMsg.StatusMessage = "Successfully Guarantor Address Proof Added";
                        break;
                    case "Photo Proof":
                        guarantor.PhotoProof = image;
                        //MainWindow.StatusMsg.MessageType = 1;
                        GPhotoProofError.Visibility = Visibility.Collapsed;
                        //MainWindow.StatusMsg.StatusMessage = "Successfully Guarantor Photo Proof Added";
                        break;
                    case "Profile Picture":
                        guarantor.ProfilePicture = image;
                        GProfilePictureError.Visibility = Visibility.Collapsed;
                        //MainWindow.StatusMsg.MessageType = 1;
                        //MainWindow.StatusMsg.StatusMessage = "Successfully Guarantor Profile Picture Added";
                        break;
                }
            }
            else if (WhichClassButtonClick.Equals("Nominee"))
            {
                switch (imagename)
                {
                    case "Address Proof":
                        nominee.AddressProof = image;
                        NAddressProofError.Visibility = Visibility.Collapsed;
                        //MainWindow.StatusMsg.MessageType = 1;
                        //MainWindow.StatusMsg.StatusMessage = "Successfully Nominee Address Proof Added";
                        break;
                    case "Photo Proof":
                        nominee.PhotoProof = image;
                        NPhotoProofError.Visibility = Visibility.Collapsed;
                        //MainWindow.StatusMsg.MessageType = 1;
                        //MainWindow.StatusMsg.StatusMessage = "Successfully Nominee Photo Proof Added";
                        break;
                    case "Profile Picture":
                        nominee.ProfilePicture = image;
                        NProfileError.Visibility = Visibility.Collapsed;
                        //MainWindow.StatusMsg.MessageType = 1;
                        //MainWindow.StatusMsg.StatusMessage = "Successfully Nominee Profile Picture Added";
                        break;
                }
            }
        }
        private void ImgCancelbtn_Click(object sender, RoutedEventArgs e)
        {
            CaptureImage.Visibility = Visibility.Collapsed;
        }

        private void ProfileImageOk_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddCustAddressProof_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            CapturePhoto = new Capture();
            ImageSavebtn.Content = "Save";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            PhotoProofNametxt.Text = "Address Proof";
            WhichClassButtonClick = "Customer";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void ShowCustAddressProof_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Address Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = customer.AddressProof;
        }

        private void EditCustAddressProof_Click(object sender, RoutedEventArgs e)
        {
            ImageSavebtn.Content = "Update";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            CapturePhoto.CapImg.Source = customer.AddressProof;
            PhotoProofNametxt.Text = "Address Proof";
            WhichClassButtonClick = "Customer";
            CaptureImage.Visibility = Visibility.Visible;
        }

        private void CustPhotoProofAdd_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            CapturePhoto = new Capture();
            ImageSavebtn.Content = "Save";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            PhotoProofNametxt.Text = "Photo Proof";
            WhichClassButtonClick = "Customer";
            CaptureImage.Visibility = Visibility.Visible;
            CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void CustPhotoProofView_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Photo Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = customer.PhotoProof;
        }

        private void CustPhotoProofEdit_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            ImageSavebtn.Content = "Update";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            CapturePhoto.CapImg.Source = customer.PhotoProof;
            PhotoProofNametxt.Text = "Photo Proof";
            WhichClassButtonClick = "Customer";
            CaptureImage.Visibility = Visibility.Visible;
            CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void CustProfileEdit_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            CapturePhoto = new Capture();
            ImageSavebtn.Content = "Save";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            PhotoProofNametxt.Text = "Profile Picture";
            WhichClassButtonClick = "Customer";
            CaptureImage.Visibility = Visibility.Visible;
            CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void CustProfileView_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Profile Picture";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = customer.ProfilePicture;
        }

        private void CustProfileAdd_Click(object sender, RoutedEventArgs e)
        {
            ImageSavebtn.Content = "Update";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            CapturePhoto.CapImg.Source = customer.ProfilePicture;
            PhotoProofNametxt.Text = "Profile Picture";
            WhichClassButtonClick = "Customer";
            CaptureImage.Visibility = Visibility.Visible;
        }

        private void GAddressProofAdd_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            CapturePhoto = new Capture();
            ImageSavebtn.Content = "Save";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            PhotoProofNametxt.Text = "Address Proof";
            WhichClassButtonClick = "Guarantor";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void GAddressProofShow_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Address Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = guarantor.AddressProof;
        }

        private void GAddressProofEdit_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            ImageSavebtn.Content = "Update";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            CapturePhoto.CapImg.Source = guarantor.AddressProof;
            PhotoProofNametxt.Text = "Address Proof";
            WhichClassButtonClick = "Guarantor";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void GPhotoProofAdd_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            CapturePhoto = new Capture();
            ImageSavebtn.Content = "Save";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            PhotoProofNametxt.Text = "Photo Proof";
            WhichClassButtonClick = "Guarantor";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void GPhotoProofShow_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Photo Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = guarantor.PhotoProof;
        }
        private void GphotoProof_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            ImageSavebtn.Content = "Update";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            CapturePhoto.CapImg.Source = guarantor.PhotoProof;
            PhotoProofNametxt.Text = "Photo Proof";
            WhichClassButtonClick = "Guarantor";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void GprofileAdd_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            CapturePhoto = new Capture();
            ImageSavebtn.Content = "Save";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            PhotoProofNametxt.Text = "Profile Picture";
            WhichClassButtonClick = "Guarantor";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void GProfileShow_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Profile Picture";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = guarantor.ProfilePicture;
        }

        private void GprofileEdit_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            ImageSavebtn.Content = "Update";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            CapturePhoto.CapImg.Source = guarantor.ProfilePicture;
            PhotoProofNametxt.Text = "Profile Picture";
            WhichClassButtonClick = "Guarantor";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void NAddressProofAdd_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            CapturePhoto = new Capture();
            ImageSavebtn.Content = "Save";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            PhotoProofNametxt.Text = "Address Proof";
            WhichClassButtonClick = "Nominee";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void NAddressProofShow_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Address Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = nominee.AddressProof;
        }

        private void NAddressProofEdit_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            ImageSavebtn.Content = "Update";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            CapturePhoto.CapImg.Source = nominee.AddressProof;
            PhotoProofNametxt.Text = "Address Proof";
            WhichClassButtonClick = "Nominee";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void NPhotoProofAdd_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            CapturePhoto = new Capture();
            ImageSavebtn.Content = "Save";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            PhotoProofNametxt.Text = "Photo Proof";
            WhichClassButtonClick = "Nominee";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void NPhotoProofShow_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Photo Proof";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = nominee.PhotoProof;
        }

        private void NPhotoProofEdit_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            ImageSavebtn.Content = "Update";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            CapturePhoto.CapImg.Source = nominee.PhotoProof;
            PhotoProofNametxt.Text = "Photo Proof";
            WhichClassButtonClick = "Nominee";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void NProfileAdd_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            CapturePhoto = new Capture();
            ImageSavebtn.Content = "Save";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            PhotoProofNametxt.Text = "Profile Picture";
            WhichClassButtonClick = "Nominee";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void NProfileEdit_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Profile Picture";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = nominee.ProfilePicture;
        }

        private void NProfileView_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            ImageSavebtn.Content = "Update";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            CapturePhoto.CapImg.Source = nominee.ProfilePicture;
            PhotoProofNametxt.Text = "Profile Picture";
            WhichClassButtonClick = "Nominee";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void CombinePhotoAdd_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            CapturePhoto = new Capture();
            ImageSavebtn.Content = "Save";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            PhotoProofNametxt.Text = "Combine Photo";
            WhichClassButtonClick = "Customer";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }

        private void CombinePhotoShow_Click(object sender, RoutedEventArgs e)
        {
            ImageTitle.Text = "Combine Photo";
            ViewImagePopup.IsOpen = true;
            viewImage.Source = customer.CombinePhoto;
        }

        private void CombinePhotoEdit_Click(object sender, RoutedEventArgs e)
        {
            CaptureImageMessage = new StaticProperty();
            ImageSavebtn.Content = "Update";
            Captureframe.NavigationService.Navigate(CapturePhoto);
            CapturePhoto.CapImg.Source = customer.CombinePhoto;
            PhotoProofNametxt.Text = "Combine Photo";
            WhichClassButtonClick = "Customer";
            CaptureImage.Visibility = Visibility.Visible; CaptureImageStatus.DataContext = CaptureImageMessage;
        }
        bool GreenCheck = true;
        private void VerifyNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            if(CustomerStatus==0)
            {
                CustomerCheck();
                GuarantorCheck();
                NomineeCheck();
                BankCheck();
            }
            if(CustomerStatus==2)
            {
                CustomerCheck();
                GuarantorCheck();
                NomineeCheck();
                BankCheck();
                DocumentCheck();
            }
            if(GreenCheck)
            {
                if(CustomerStatus==0)
                {
                    InsertVerificationDetails();
                    customer.SaveCustomerDetails(_regionName, _branchName, _shgName, _pgName, guarantor, nominee);
                    MainWindow.StatusMessageofPage(1, "Successfully Customer Details Added");
                    NavigationService.GetNavigationService(this).Navigate(new DashboardFieldOfficer());
                    Thread.Sleep(2000);
                    MainWindow.StatusMessageofPage(1, "Ready...");
                }
                else if(CustomerStatus==2)
                {

                }
            }
            else
            {
                MainWindow.StatusMessageofPage(0, "Please Verifiy Mandotory Fields.....");
            }
            
        }
        void CustomerCheck()
        {
            if (!customer.CustName)
            {
                CustNameError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustNameError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustGender)
            {
                CustGenderError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustGenderError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustDateOfBirth)
            {
                CustDobError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustDobError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustFatherName)
            {
                CustFatherError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustFatherError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustMotherName)
            {
                CustMotherError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustMotherError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustHusbandName)
            {
                CustHusbandError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustHusbandError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustContactNumber)
            {
                CustContactError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustContactError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustAadharNumber)
            {
                CustAadharError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustAadharError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustReligion)
            {
                CustReligionError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustReligionError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustCommunity)
            {
                CustCommunityError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustCommunityError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustCaste)
            {
                CustCasteError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustCasteError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustEducation)
            {
                CustEducationError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustEducationError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustFamilyMember)
            {
                CustFamilyMembersError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustFamilyMembersError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustEarningMember)
            {
                CustFamilyMembersError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustFamilyMembersError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustOccupation)
            {
                CustOccupationError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustOccupationError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustMonthlyIncome)
            {
                CustmontlyIncomeError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustmontlyIncomeError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustMonthlyExpenses)
            {
                CustmontlyIncomeError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustmontlyIncomeError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustFamilyYearlyIncome)
            {
                CustYearlyIncomeError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustYearlyIncomeError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustomerDoorNumber)
            {
                CustDoorNoError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustDoorNoError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustStreetName)
            {
                CustStreetNameError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustStreetNameError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustomerLocality)
            {
                CustLocalityError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustLocalityError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustomerCity)
            {
                CustCityError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustCityError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustomerState)
            {
                CustStateError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustStateError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustomerPincode)
            {
                CustPincodeError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustPincodeError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustomerHousingType)
            {
                CustHousingTypeError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustHousingTypeError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
        }
        void GuarantorCheck()
        {
            if(!guarantor.GName)
            {
                GNameError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                GNameError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            
            if(!guarantor.GuarantorGender)
            {
                GGenderError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                GGenderError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!guarantor.GuarantorDOB)
            {
                GDObError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                GDObError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!guarantor.GuarantorContact)
            {
                GContactError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                GContactError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!guarantor.GuarantorOccupation)
            {
                GOccupationError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                GOccupationError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!guarantor.GuarantorRelationship)
            {
                GRelationshipError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                GRelationshipError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!guarantor.GuarantorDoorNumber)
            {
                GDoorNoError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                GDoorNoError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!guarantor.GuarantorStreet)
            {
                GStreetNameError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                GStreetNameError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!guarantor.GuarantorLocality)
            {
                GLocalityError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                GLocalityError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!guarantor.GuarantorCity)
            {
                GCityError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                GCityError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!guarantor.GuarantorState)
            {
                GStateError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                GStateError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!guarantor.GuarantorPincode)
            {
                GPincodeError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                GPincodeError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
        }
        void NomineeCheck()
        {
            if(!nominee.NName)
            {
                NNameError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                NNameError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if(!nominee.NomineeGender)
            {
                NGenderError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                NGenderError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!nominee.NomineeDOB)
            {
                NDObError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                NDObError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!nominee.NomineeContact)
            {
                NContactError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                NContactError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!nominee.NomineeOccupation)
            {
                NOccupationError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                NOccupationError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!nominee.NomineeRelationship)
            {
                NRelationshipError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                NRelationshipError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!nominee.NomineeDoorNo)
            {
                NDoorNoError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                NDoorNoError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!nominee.NomineeStreet)
            {
                NStreetNameError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                NStreetNameError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!nominee.NomineeLocality)
            {
                NLocalityError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                NLocalityError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!nominee.NomineeCity)
            {
                NCityError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                NCityError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!nominee.NomineeState)
            {
                NStateError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                NStateError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!nominee.NomineePincode)
            {
                NPincodeError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                NPincodeError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
        }
        void BankCheck()
        {
            if(!customer.BankHolderName)
            {
               AccountHolderError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                AccountHolderError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if(!customer.BankAccountNo)
            {
                AccountNoError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                AccountNoError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.Bankname)
            {
                BAnkNameError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                BAnkNameError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.BIfscCode)
            {
                IFSCError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                IFSCError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.BMicrCode)
            {
                MICRError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                MICRError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.BranchName)
            {
                BAnkBranchError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                BAnkBranchError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
        }
        void DocumentCheck()
        {
            if(!customer.CustomerAddressProof)
            {
                CustAddressProofError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustAddressProofError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if(!customer.CustomerPhotoProof)
            {
                CustPhotoProofError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustPhotoProofError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.CustomerProfilePicture)
            {
                CustProfilePictrueError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CustProfilePictrueError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!guarantor.GuarantorAddressProof)
            {
                GAddressProofError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                GAddressProofError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!guarantor.GuarantorPhotoProof)
            {
                GPhotoProofError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                GPhotoProofError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!guarantor.GuarantorProfilePicture)
            {
                GProfilePictureError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                GProfilePictureError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!nominee.NomineeAddressProof)
            {
                NAddressProofError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                NAddressProofError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!nominee.NomineePhotoProof)
            {
                NPhotoProofError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                NPhotoProofError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!nominee.NomineeProfilePicture)
            {
                NProfileError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                NProfileError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
            if (!customer.Combinephoto)
            {
                CombinePhotoError.Visibility = Visibility.Visible;
                GreenCheck = false;
            }
            else
            {
                CombinePhotoError.Visibility = Visibility.Collapsed;
                GreenCheck = true;
            }
        }

        void InsertVerificationDetails()
        {
            using(SqlConnection sql=new SqlConnection(Properties.Settings.Default.db))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "insert into CustomerVerification(CustId,CustomerName,CustomerGender,CustomerDOB,FatherName,MotherName,Guardian,Contact,CAddress,Religion,Caste,Community,Education,FamilyMembers,EarningMembers,Occupation,MonthlyIncome,MonthlyExpence,FamilyAnnualIncome,CustomerDoorNo,CustomerStreet,CustomerLocality,CustomerCity,CustomerState,CustomerPincode,HouseType,GName,GGender,GDOB,GContact,GOccupation,GRelationship,GDoorNo,GStreet,GLocality,GCity,GState,GPincode,NName,NGender,NDOB,NContact,NOccupation,NRelationship,NDoorNo,NStreet,NLocality,NCity,NState,NPincode,AccountHolderName,AccountNo,BankName,BranchName,IFSC,MICR,CAddressProof,CPhotoProof,CProfilePic,GAddressProof,GPhotoProof,GProfilePic,NAddressProof,NPhotoProof,NProfilePic,CustomerCombinePhoto) values ('" + customer._customerId + "','" + customer.CustName + "','" + customer.CustGender + "','" + customer.CustDateOfBirth + "','" + customer.CustFatherName + "','" + customer.CustMotherName + "','" + customer.CustHusbandName + "','" + customer.CustContactNumber + "','" + customer.CustAadharNumber + "','" + customer.CustReligion + "','" + customer.CustCaste + "','" + customer.CustCommunity + "','" + customer.CustEducation + "','" + customer.CustFamilyMember + "','" + customer.CustEarningMember + "','" + customer.CustOccupation + "','" + customer.CustMonthlyIncome + "','" + customer.CustMonthlyExpenses + "','" + customer.CustFamilyYearlyIncome + "','" + customer.CustomerDoorNumber + "','" + customer.CustStreetName + "','" + customer.CustomerLocality + "','" + customer.CustomerCity + "','" + customer.CustomerState + "','" + customer.CustomerPincode + "','" + customer.CustomerHousingType + "','" + guarantor.GName + "','" + guarantor.GuarantorGender + "','" + guarantor.GuarantorDOB + "','" + guarantor.GuarantorContact + "','" + guarantor.GuarantorOccupation + "','" + guarantor.GuarantorRelationship + "','" + guarantor.GuarantorDoorNumber + "','" + guarantor.GuarantorStreet + "','" + guarantor.GuarantorLocality + "','" + guarantor.GuarantorCity + "','" + guarantor.GuarantorState + "','" + guarantor.GuarantorPincode + "','" + nominee.NName + "','" + nominee.NomineeGender + "','" + nominee.NomineeDOB + "','" + nominee.NomineeContact + "','" + nominee.NomineeOccupation + "','" + nominee.NomineeRelationship + "','" + nominee.NomineeDoorNo + "','" + nominee.NomineeStreet + "','" + nominee.NomineeLocality + "','" + nominee.NomineeCity + "','" + nominee.NomineeState + "','" + nominee.NomineePincode + "','" + customer.BankHolderName + "','" + customer.BankAccountNo + "','" + customer.Bankname + "','" + customer.BranchName + "','" + customer.BIfscCode + "','" + customer.BMicrCode + "','" + customer.CustomerAddressProof + "','" + customer.CustomerPhotoProof + "','" + customer.CustomerProfilePicture + "','" + guarantor.GuarantorAddressProof + "','" + guarantor.GuarantorPhotoProof + "','" + guarantor.GuarantorProfilePicture + "','" + nominee.NomineeAddressProof + "','" + nominee.NomineePhotoProof + "','" + nominee.NomineeProfilePicture + "','" + customer.Combinephoto + "')";
                command.ExecuteNonQuery();
            }
        }

        private void CancelVerify_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
    
}