using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    public class DetailsForCustomerVerification:BindableBase
    {
        public string CustomerId { get; set; }
        private bool _customerName;
        public bool CustName
        {
            get
            {
                return _customerName;
            }
            set
            {
                _customerName = value;
                RaisedPropertyChanged("CustName");

            }
        }


        private bool _gender;
        public bool CustGender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
                RaisedPropertyChanged("CustGender");
            }
        }

        private bool _dateOfBirth;
        public bool CustDateOfBirth
        {
            get
            {
                return _dateOfBirth;
            }
            set
            {
                _dateOfBirth = value;
                RaisedPropertyChanged("CustDateOfBirth");
            }
        }

        private bool _age;
        public bool CustAge
        {
            get
            {
                return _age;
            }
            set
            {
                _age = value;
                RaisedPropertyChanged("CustAge");
            }
        }

        private bool _fatherName;
        public bool CustFatherName
        {
            get
            {
                return _fatherName;
            }
            set
            {
                _fatherName = value;
                RaisedPropertyChanged("CustFatherName");
            }
        }

        private bool _motherName;
        public bool CustMotherName
        {
            get
            {
                return _motherName;
            }
            set
            {
                _motherName = value;
                RaisedPropertyChanged("CustMotherName");
            }
        }

        private bool _husbandName;
        public bool CustHusbandName
        {
            get
            {
                return _husbandName;
            }
            set
            {
                _husbandName = value;
                RaisedPropertyChanged("CustHusbandName");
            }
        }

        private bool _contactNumber;
        public bool CustContactNumber
        {
            get
            {
                return _contactNumber;
            }
            set
            {
                _contactNumber = value;
                RaisedPropertyChanged("CustContactNumber");
            }
        }

        private bool _aadharNumber;
        public bool CustAadharNumber
        {
            get
            {
                return _aadharNumber;
            }
            set
            {
                _aadharNumber = value;
                RaisedPropertyChanged("CustAadharNumber");
            }
        }

        private bool _religion;
        public bool CustReligion
        {
            get
            {
                return _religion;
            }
            set
            {
                _religion = value;
                RaisedPropertyChanged("CustReligion");
            }
        }

        private bool _community;
        public bool CustCommunity
        {
            get
            {
                return _community;
            }
            set
            {
                _community = value;
                RaisedPropertyChanged("CustCommunity");
            }
        }

        private bool _caste;
        public bool CustCaste
        {
            get
            {
                return _caste;
            }
            set
            {
                _caste = value;
                RaisedPropertyChanged("CustCaste");
            }
        }

        private bool _education;
        public bool CustEducation
        {
            get
            {
                return _education;
            }
            set
            {
                _education = value;
                RaisedPropertyChanged("CustEducation");
            }
        }

        private bool _familymember;
        public bool CustFamilyMember
        {
            get
            {
                return _familymember;
            }
            set
            {
                _familymember = value;
                RaisedPropertyChanged("CustFamilyMember");
            }
        }

        private bool _earningMember;
        public bool CustEarningMember
        {
            get
            {
                return _earningMember;
            }
            set
            {
                _earningMember = value;
                RaisedPropertyChanged("CustEarningMember");
            }
        }
        private bool _custOccupation;
        public bool CustOccupation
        {
            get
            {
                return _custOccupation;
            }
            set
            {
                _custOccupation = value;
                RaisedPropertyChanged("CustOccupation");
            }
        }

        private bool _custMonthlyIncome;
        public bool CustMonthlyIncome
        {
            get
            {
                return _custMonthlyIncome;
            }
            set
            {
               _custMonthlyIncome = value;
                RaisedPropertyChanged("CustMonthlyIncome");
            }
        }

        private bool _custMonthlyExpenses;
        public bool CustMonthlyExpenses
        {
            get
            {
                return _custMonthlyExpenses;
            }
            set
            {
                _custMonthlyExpenses = value;
                RaisedPropertyChanged("CustMonthlyExpenses");
            }
        }

        private bool _familyYearlyIncome;
        public bool CustFamilyYearlyIncome
        {
            get
            {
                return _familyYearlyIncome;
            }
            set
            {
                _familyYearlyIncome = value;
                RaisedPropertyChanged("CustFamilyYearlyIncome");
            }
        }

        private bool _doorNumber;
        public bool CustomerDoorNumber
        {
            get
            {
                return _doorNumber;
            }
            set
            {
                _doorNumber = value;
                RaisedPropertyChanged("CustomerDoorNumber");
            }
        }

        private bool _streetName;
        public bool CustStreetName
        {
            get
            {
                return _streetName;
            }
            set
            {
                _streetName = value;
                RaisedPropertyChanged("CustStreetName");
            }
        }

        private bool _customerlocalityTown;
        public bool CustomerLocality
        {
            get
            {
                return _customerlocalityTown;
            }
            set
            {
                _customerlocalityTown = value;
                RaisedPropertyChanged("CustomerLocality");
            }
        }

        private bool _customerCity;
        public bool CustomerCity
        {
            get
            {
                return _customerCity;
            }
            set
            {
                _customerCity = value;
                RaisedPropertyChanged("CustomerCity");
            }
        }

        private bool _customerState;
        public bool CustomerState
        {
            get
            {
                return _customerState;
            }
            set
            {
                _customerState = value;
                RaisedPropertyChanged("CustomerState");
            }
        }

        private bool _customerPincode;
        public bool CustomerPincode
        {
            get
            {
                return _customerPincode;
            }
            set
            {
                _customerPincode = value;
                RaisedPropertyChanged("CustomerPincode");
            }
        }

        private bool _customerHousingType;
        public bool CustomerHousingType
        {
            get
            {
                return _customerHousingType;
            }
            set
            {
                _customerHousingType = value;
                RaisedPropertyChanged("CustomerHousingType");
            }
        }

        private bool _guarantorName;
        public bool GName
        {
            get
            {
                return _guarantorName;
            }
            set
            {
                _guarantorName = value;
                RaisedPropertyChanged("GName");
            }
        }

        private bool _guarantorGender;
        public bool GuarantorGender
        {
            get
            {
                return _guarantorGender;
            }
            set
            {
                _guarantorGender = value;
                RaisedPropertyChanged("GuarantorGender");
            }
        }

        private bool _guarantorDOB;
        public bool GuarantorDOB
        {
            get
            {
                return _guarantorDOB;
            }
            set
            {
                _guarantorDOB = value;
                RaisedPropertyChanged("GuarantorDOB");
            }
        }

        private bool _guarantorContact;
        public bool GuarantorContact
        {
            get
            {
                return _guarantorContact;
            }
            set
            {
                _guarantorContact = value;
                RaisedPropertyChanged("GuarantorContact");
            }
        }

        private bool _guarantorOccupation;
        public bool GuarantorOccupation
        {
            get
            {
                return _guarantorOccupation;
            }
            set
            {
                _guarantorOccupation = value;
                RaisedPropertyChanged("GuarantorOccupation");
            }
        }

        private bool _guarantorRelationship;
        public bool GuarantorRelationship
        {
            get
            {
                return _guarantorRelationship;
            }
            set
            {
                _guarantorRelationship = value;
                RaisedPropertyChanged("GuarantorRelationship");
            }
        }

        private bool _guarantorDoorNumber;
        public bool GuarantorDoorNumber
        {
            get
            {
                return _guarantorDoorNumber;
            }
            set
            {
                _guarantorDoorNumber = value;
                RaisedPropertyChanged("GuarantorDoorNumber");
            }
        }

        private bool _guarantorStreet;
        public bool GuarantorStreet
        {
            get
            {
                return _guarantorStreet;
            }
            set
            {
                _guarantorStreet = value;
                RaisedPropertyChanged("GuarantorStreet");
            }
        }

        private bool _guarantorLocality;
        public bool GuarantorLocality
        {
            get
            {
                return _guarantorLocality;
            }
            set
            {
                _guarantorLocality = value;
                RaisedPropertyChanged("GuarantorLocality");
            }
        }

        private bool _guarantorCity;
        public bool GuarantorCity
        {
            get
            {
                return _guarantorCity;
            }
            set
            {
                _guarantorCity = value;
                RaisedPropertyChanged("GuarantorCity");
            }
        }

        private bool _guarantorState;
        public bool GuarantorState
        {
            get
            {
                return _guarantorState;
            }
            set
            {
                _guarantorState = value;
                RaisedPropertyChanged("GuarantorState");
            }
        }

        private bool _guarantorPincode;
        public bool GuarantorPincode
        {
            get
            {
                return _guarantorPincode ;
            }
            set
            {
                _guarantorPincode = value;
                RaisedPropertyChanged("GuarantorPincode");
            }
        }

        private bool _nomineeName;
        public bool NName
        {
            get
            {
                return _nomineeName;
            }
            set
            {
                _nomineeName = value;
                RaisedPropertyChanged("NomineeName");
            }
        }

        private bool _nomineeGender;
        public bool NomineeGender
        {
            get
            {
                return _nomineeGender;
            }
            set
            {
                _nomineeGender = value;
                RaisedPropertyChanged("NomineeGender");
            }
        }

        private bool _nomineeDOB;
        public bool NomineeDOB
        {
            get
            {
                return _nomineeDOB;
            }
            set
            {
                _nomineeDOB = value;
                RaisedPropertyChanged("NomineeDOB");
            }
        }

        private bool _nomineeContact;
        public bool NomineeContact
        {
            get
            {
                return _nomineeContact;
            }
            set
            {
                _nomineeContact= value;
                RaisedPropertyChanged("NomineeContact");
            }
        }

        private bool _nomineeOccupation;
        public bool NomineeOccupation
        {
            get
            {
                return _nomineeOccupation;
            }
            set
            {
                _nomineeOccupation = value;
                RaisedPropertyChanged("NomineeOccupation");
            }
        }

        private bool _nomineeRelationship;
        public bool NomineeRelationship
        {
            get
            {
                return _nomineeRelationship;
            }
            set
            {
                _nomineeRelationship = value;
                RaisedPropertyChanged("NomineeRelationship");
            }
        }

        private bool _nomineeDoorNo;
        public bool NomineeDoorNo
        {
            get
            {
                return _nomineeDoorNo;
            }
            set
            {
                _nomineeDoorNo = value;
                RaisedPropertyChanged("NomineeDoorNo");
            }
        }

        private bool _nomineeStreet;
        public bool NomineeStreet
        {
            get
            {
                return _nomineeStreet;
            }
            set
            {
                _nomineeStreet = value;
                RaisedPropertyChanged("NomineeStreet");
            }
        }

        private bool _nomineeLocality;
        public bool NomineeLocality
        {
            get
            {
                return _nomineeLocality;
            }
            set
            {
                _nomineeLocality = value;
                RaisedPropertyChanged("NomineeLocality");
            }
        }

        private bool _nomineeCity;
        public bool NomineeCity
        {
            get
            {
                return _nomineeCity;
            }
            set
            {
                _nomineeCity = value;
                RaisedPropertyChanged("NomineeCity");
            }
        }

        private bool _nomineeState;
        public bool NomineeState
        {
            get
            {
                return _nomineeState;
            }
            set
            {
                _nomineeState = value;
                RaisedPropertyChanged("NomineeState");
            }
        }

        private bool _nomineePincode;
        public bool NomineePincode
        {
            get
            {
                return _nomineePincode;
            }
            set
            {
                _nomineePincode= value;
                RaisedPropertyChanged("NomineePincode");
            }
        }

        private bool _bankHolderName;
        public bool BankHolderName
        {
            get
            {
                return _bankHolderName;
            }
            set
            {
                _bankHolderName = value;
                RaisedPropertyChanged("BankHolderName");
            }
        }

        private bool _bankAccountNo;
        public bool BankAccountNo
        {
            get
            {
                return _bankAccountNo;
            }
            set
            {
                _bankAccountNo = value;
                RaisedPropertyChanged("BankAccountNo");
            }
        }

        private bool _bankName;
        public bool Bankname
        {
            get
            {
                return _bankName;
            }
            set
            {
                _bankName = value;
                RaisedPropertyChanged("Bankname");
            }
        }

        private bool _ifscCode;
        public bool BIfscCode
        {
            get
            {
                return _ifscCode;
            }
            set
            {
                _ifscCode = value;
                RaisedPropertyChanged("BIfscCode");
            }
        }

        private bool _micrCode;
        public bool BMicrCode
        {
            get
            {
                return _micrCode;
            }
            set
            {
                _micrCode = value;
                RaisedPropertyChanged("BMicrCode");
            }
        }

        private bool _branchName;
        public bool BranchName
        {
            get
            {
                return _branchName;
            }
            set
            {
                _branchName = value;
                RaisedPropertyChanged("BranchName");
            }
        }

        private bool _customerAddressProof;
        public bool CustomerAddressProof
        {
            get
            {
                return _customerAddressProof;
            }
            set
            {
                _customerAddressProof = value;
                RaisedPropertyChanged("CustomerAddressProof");
            }
        }

        private bool _customerPhotoProof;
        public bool CustomerPhotoProof
        {
            get
            {
                return _customerPhotoProof;
            }
            set
            {
                _customerPhotoProof = value;
                RaisedPropertyChanged("CustomerPhotoProof");
            }
        }

        private bool _customerProfilePicture;
        public bool CustomerProfilePicture
        {
            get
            {
                return _customerProfilePicture;
            }
            set
            {
                _customerProfilePicture = value;
                RaisedPropertyChanged("CustomerProfilePicture");
            }
        }

        private bool _guarantorAddressProof;
        public bool GuarantorAddressProof
        {
            get
            {
                return _guarantorAddressProof;
            }
            set
            {
                _guarantorAddressProof = value;
                RaisedPropertyChanged("GuarantorAddressProof");
            }
        }

        private bool _guarantorPhtoProof;
        public bool GuarantorPhotoProof
        {
            get
            {
                return _guarantorPhtoProof;
            }
            set
            {
                _guarantorPhtoProof = value;
                RaisedPropertyChanged("GuarantorPhotoProof");
            }
        }

        private bool _guarantorProfilePicture;
        public bool GuarantorProfilePicture
        {
            get
            {
                return _guarantorProfilePicture;
            }
            set
            {
                _guarantorProfilePicture = value;
                RaisedPropertyChanged("GuarantorProfilePicture");
            }
        }

        private bool _nomineeAddressProof;
        public bool NomineeAddressProof
        {
            get
            {
                return _nomineeAddressProof;
            }
            set
            {
                _nomineeAddressProof = value;
                RaisedPropertyChanged("NomineeAddressProof");
            }
        }

        private bool _nomineePhotoProof;
        public bool NomineePhotoProof
        {
            get
            {
                return _nomineePhotoProof;
            }
            set
            {
                _nomineePhotoProof = value;
                RaisedPropertyChanged("NomineePhotoProof");
            }
        }

        private bool _nomineeprofilePicture;
        public bool NomineeProfilePicture
        {
            get
            {
                return _nomineeprofilePicture;
            }
            set
            {
                _nomineeprofilePicture = value;
                RaisedPropertyChanged("NomineeProfilePicture");
            }
        }

        private bool _combinePhoto;
        public bool Combinephoto
        {
            get
            {
                return _combinePhoto;
            }
            set
            {
                _combinePhoto = value;
                RaisedPropertyChanged("Combinephoto");
            }
        }
    }
}
