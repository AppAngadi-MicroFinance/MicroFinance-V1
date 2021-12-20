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
using MicroFinance.ViewModel;
using MicroFinance.APIModal;

namespace MicroFinance
{
    /// <summary>
    /// Interaction logic for CustomerEnrollDataView.xaml
    /// </summary>
    public partial class CustomerEnrollDataView : Page
    {
        public VMCustomerEnrollData _customerdata = new VMCustomerEnrollData();
        private VMCustomerDetails CustomerDetails = new VMCustomerDetails();
        private VMCustomerGroup CustomerGroup = new VMCustomerGroup();
        private VMGuarenteeDetails GuarenteeDetails = new VMGuarenteeDetails();

        public CustomerEnrollDataView()
        {
            InitializeComponent();
           
        }
        public CustomerEnrollDataView(VMCustomerEnrollData CustomerData)
        {
            InitializeComponent();
            _customerdata = CustomerData;

        }


        void FormDataForBinding()
        {
            CustomerDetails.Name = _customerdata.CustomerName;
            CustomerDetails.FatherName = _customerdata.FatherName;
            CustomerDetails.MotherName = _customerdata.MotherName;
            CustomerDetails.Dob = _customerdata.DateOfBirth;
            CustomerDetails.Age = _customerdata.Age;
            CustomerDetails.Gender = _customerdata.Gender;
            CustomerDetails.Mobile = _customerdata.ContactNumber;
            CustomerDetails.AadharNumber = _customerdata.AadharNumber;
            CustomerDetails.Religion = _customerdata.Religion;
            CustomerDetails.Caste = _customerdata.Caste;
            CustomerDetails.Community = _customerdata.Community;
            CustomerDetails.Education = _customerdata.Education;
            CustomerDetails.FamilyMembers = _customerdata.FamilyMembers;
            CustomerDetails.EarningMembers = _customerdata.EarningMembers;
            CustomerDetails.Occupation = _customerdata.Occupation;
            CustomerDetails.MonthlyIncome = _customerdata.MonthlyIncome;
            CustomerDetails.MonthlyExpenses = _customerdata.MonthlyExpence;
            CustomerDetails.DoorNo = _customerdata.DoorNo;
            CustomerDetails.StreetName = _customerdata.StreetName;
            CustomerDetails.Village = _customerdata.Village;
            CustomerDetails.State = _customerdata.GState;
            CustomerDetails.Pincode =Convert.ToInt32( _customerdata.PinCode);
            CustomerDetails.HousingType = _customerdata.HousingType;
            CustomerDetails.AddressProofName = _customerdata.AddressProofName;
            CustomerDetails.PhotoProofName = _customerdata.PhotoProofName;
            CustomerDetails.IsBankDetails = true;
            CustomerDetails.IsAddressProof = (_customerdata.AddressProof != null) ? true : false;
            CustomerDetails.IsPhotoProof = (_customerdata.PhotoProof != null) ? true : false;
            CustomerDetails.IsCombinePhoto = (_customerdata.CombinePhoto != null) ? true : false;
            CustomerDetails.BankACHolderName = _customerdata.AccountHolderName;
            CustomerDetails.BankAccountNo = _customerdata.AccountNumber;
            CustomerDetails.BankName = _customerdata.BankName;
            CustomerDetails.BankBranchName = _customerdata.BranchName;
            CustomerDetails.IFSCCode = _customerdata.IFSCCode;
            CustomerDetails.MICRCode = _customerdata.MICRCode;
            CustomerDetails.AddressProof = _customerdata.AddressProof;
            CustomerDetails.PhotoProof = _customerdata.PhotoProof;
            CustomerDetails.ProfilePhoto = _customerdata.CombinePhoto;
            CustomerDetails.GuarenteeStatus = true;
            CustomerDetails.NomineeStatus = true;
            CustomerDetails.CustomerStatus =0;
            CustomerDetails.IsActive = false;
            CustomerDetails.HusbandName = _customerdata.GuardianName;
            CustomerDetails.YearlyIncome = _customerdata.FamilyYearlyIncome;
            CustomerDetails.IsCombinePhoto = true;
            CustomerDetails.CombinePhoto = _customerdata.CombinePhoto;
            CustomerDetails.PhotoProofNo = _customerdata.PhotoProofNumber;
            CustomerDetails.AddressProofNo = _customerdata.AddressProofNumber;
            CustomerDetails.Residency = "";
            CustomerDetails.LandHolding = _customerdata.LandHolding;
            CustomerDetails.LandVolume = _customerdata.LandVolume;




        }
    }
}
