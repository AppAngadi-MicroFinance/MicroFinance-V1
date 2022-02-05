using MicroFinance.Validations;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MicroFinance.Modal
{
    public class Customer : CustomerDetailsForVerification
    {
        public string _customerId { get; set; }

        private string _customerName;
        public string CustomerName
        {
            get
            {
                return _customerName;
            }
            set
            {
                _customerName = value;
                CustName = false;
                CustDetailsOverAll = false;
            }
        }
        private DateTime _dateofBirth = DateTime.Now;
        public DateTime DateofBirth
        {
            get
            {
                return _dateofBirth;
            }
            set
            {
                _dateofBirth = value;
                Age = AgeCalculator(_dateofBirth);
                RaisedPropertyChanged("DateofBirth");
                CustDateOfBirth = false;
                CustDetailsOverAll = false;
            }
        }
        int AgeCalculator(DateTime dob)
        {
            int age = 0;
            if (dob != DateTime.MinValue)
            {
                DateTime now = DateTime.Today;
                age = now.Year - dob.Year;

                if (now.Month < dob.Month || (now.Month == dob.Month && now.Day < dob.Day))
                    age--;
            }
            return age;
        }
        private int _age;
        public int Age
        {
            get
            {
                return _age;
            }
            set
            {
                _age = value;
            }
        }
        private string _contactNumber;
        public string ContactNumber
        {
            get
            {
                return _contactNumber;
            }
            set
            {
                _contactNumber = value;
                RaisedPropertyChanged("ContactBox");
                CustContactNumber = false;
                CustDetailsOverAll = false;
            }
        }

        private string _community;
        public string Community
        {
            get
            {
                return _community;
            }
            set
            {
                _community = value;
                CustCommunity = false;
                CustDetailsOverAll = false;
            }
        }
        private string _education;
        public string Education
        {
            get
            {
                return _education;
            }
            set
           {
                _education = value;
                CustEducation = false;
                CustDetailsOverAll = false;
            }
        }
        private int _familymembers;
        public int FamilyMembers
        {
            get
            {
                return _familymembers;
            }
            set
            {
                _familymembers = value;
                CustFamilyMember = false;
                CustDetailsOverAll = false;
            }
        }
        private int _earningmembers;
        public int EarningMembers
        {
            get
            {
                return _earningmembers;
            }
            set
            {
                _earningmembers = value;
                CustEarningMember = false;
                CustDetailsOverAll = false;
            }
        }
        private string _occupation;
        public string Occupation
        {
            get
            {
                return _occupation;
            }
            set
            {
                _occupation = value;
                CustOccupation = false;
                CustDetailsOverAll = false;
            }
        }
        private int _monthlyIncome;
        public int MonthlyIncome
        {
            get
            {
                return _monthlyIncome;
            }
            set
            {
                _monthlyIncome = value;
                CustMonthlyIncome = false;
                CustDetailsOverAll = false;
            }
        }
        private string _doorNumber;
        public string DoorNumber
        {
            get
            {
                return _doorNumber;
            }
            set
            {
                _doorNumber = value;
                CustomerDoorNumber = false;
                CustAddressOverAll = false;
            }
        }
        private string _streetName;
        public string StreetName
        {
            get
            {
                return _streetName;
            }
            set
            {
                _streetName = value;
                CustStreetName = false;
                CustAddressOverAll = false;
            }
        }
        private string _localityTown;
        public string LocalityTown
        {
            get
            {
                return _localityTown;
            }
            set
            {
                _localityTown = value;
                CustomerLocality = false;
                CustAddressOverAll = false;
            }
        }
        private int _pincode;
        public int Pincode
        {
            get
            {
                return _pincode;
            }
            set
            {
                _pincode = value;
                CustomerPincode = false;
                CustAddressOverAll = false;
            }
        }

        private string _city;
        public string City
        {
            get
            {
                return _city;
            }
            set
            {
                _city = value;
                CustomerCity = false;
                CustAddressOverAll = false;
            }
        }
        private string _state;
        public string State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
                CustomerState = false;
                CustAddressOverAll = false;
            }
        }
        private string _housingType;
        public string HousingType
        {
            get
            {
                return _housingType;
            }
            set
            {
                _housingType = value;
                CustomerHousingType = false;
                CustAddressOverAll = false;
            }
        }
        private string _taluk;
        public string Taluk
        {
            get
            {
                return _taluk;
            }
            set
            {
                _taluk = value;
            }
        }
        private string _housingIndex;
        public string HousingIndex
        {
            get
            {
                return _housingIndex;
            }
            set
            {
                _housingIndex = value;
            }
        }

        private string _residencyType;
        public string ResidencyType
        {
            get
            {
                return _residencyType;
            }
            set
            {
                _residencyType = value;
                RaisedPropertyChanged("ResidencyType");
            }
        }

        private string _landholding;
        public string LandHolding
        {
            get
            {
                return _landholding;
            }
            set
            {
                _landholding = value;
                RaisedPropertyChanged("LandHolding");
            }
        }

        private string _landtype;
        public string LandType
        {
            get
            {
                return _landtype;
            }
            set
            {
                _landtype = value;
                RaisedPropertyChanged("LandType");
            }
        }

        private string _landVolume;
        public string LandVolume
        {
            get
            {
                return _landVolume;
            }
            set
            {
                _landVolume = value;
                RaisedPropertyChanged("LandVolume");
            }
        }

        private string _aadharNumber=null;
        public string AadharNo
        {
            get
            {
                return _aadharNumber;
            }
            set
            {
                _aadharNumber = value;
                CustAadharNumber = false;
                CustDetailsOverAll = false;
            }
        }
        private bool _isLeader;
        public bool IsLeader
        {
            get
            {
                return _isLeader;
            }
            set
            {
                _isLeader = value;
            }

        }
        private List<string> _religionlist = new List<string>();
        public List<string> Religionlist
        {
            get
            {
                return _religionlist;
            }
        }
        private string _religion;
        public string Religion
        {
            get
            {
                return _religion;
            }
            set
            {
                _religion = value;
                CustReligion = false;
                CustDetailsOverAll = false;
            }
        }
        private BitmapImage _addressProof;
        public BitmapImage AddressProof
        {
            get
            {
                return _addressProof;
            }
            set
            {
                _addressProof = value;
                RaisedPropertyChanged("AddressProof");
                CustomerAddressProof = false;
                OverAllPhotoVerification = false;
            }
        }
        private BitmapImage _photoProof;
        public BitmapImage PhotoProof
        {
            get
            {
                return _photoProof;
            }
            set
            {
                _photoProof = value;
                RaisedPropertyChanged("PhotoProof");
                CustomerAddressProof = false;
                OverAllPhotoVerification = false;
            }
        }
        private BitmapImage _profilePicture;
        public BitmapImage ProfilePicture
        {
            get
            {
                return _profilePicture;
            }
            set
            {
                _profilePicture = value;
                RaisedPropertyChanged("ProfilePicture");
                CustomerProfilePicture = false;
                OverAllPhotoVerification = false;
            }
        }

        private BitmapImage _combinePhoto;
        public BitmapImage 
            CombinePhoto
        {
            get
            {
                return _combinePhoto;
            }
            set
            {
                _combinePhoto = value;
                RaisedPropertyChanged("CombinePhoto");
                Combinephoto = false;
                OverAllPhotoVerification = false;
            }
        }

        public byte[] Convertion(BitmapImage image)
        {
            byte[] Data;
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                Data = ms.ToArray();
            }
            return Data;
        }


        public BitmapImage ByteToBI(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }

        private List<string> _addressProofNames = new List<string> { "Family Card", "Aadhar Card", "Voter Id" };

        public List<string> AddressProofName
        {
            get
            {
                return _addressProofNames;
            }
        }
        private string _nameofAddressProof;
        public string NameofAddressProof 
        { 
            get
            {
                return _nameofAddressProof;
            }
            set
            {
                _nameofAddressProof = value;
                OverAllPhotoVerification = false;
            }
        }
        private string _photoProofNo;
        public string PhotoProofNo 
        { 
            get
            {
                return _photoProofNo;
            }
            set
            {
                _photoProofNo = value;
                OverAllPhotoVerification = false;
            }
        }
        private string _AddressProofNo;
        public string AddressProofNo
        {
            get
            {
                return _AddressProofNo;
            }
            set
            {
                _AddressProofNo = value;
                OverAllPhotoVerification = false;
            }
        }
        private string _nameofPhotoProof;
        public string NameofPhotoProof
        {
            get
            {
                return _nameofPhotoProof;
            }
            set
            {
                _nameofPhotoProof = value;
                OverAllPhotoVerification = false;
            }
        }

        private bool _havingBankDetails;
        public bool HavingBankDetails
        {
            get
            {
                return _havingBankDetails;
            }
            set
            {
                _havingBankDetails = value;
                RaisedPropertyChanged("HavingBankDetails");
                
            }
        }
        private string _accountHolderName;
        public string AccountHolder
        {
            get
            {
                return _accountHolderName;
            }
            set
            {
                _accountHolderName = value;
                RaisedPropertyChanged("AccountHolder");
                BankHolderName = false;
                CustBankDetailsOverAll = false;
            }
        }

        private string _accountnumber;
        public string AccountNumber
        {
            get
            {
                return _accountnumber;
            }
            set
            {
                _accountnumber = value;
                RaisedPropertyChanged("AccountNumber");
                BankAccountNo = false;
                CustBankDetailsOverAll = false;
            }
        }
        private string _bankname;
        public string BankName
        {
            get
            {
                return _bankname;
            }
            set
            {
                _bankname = value;
                RaisedPropertyChanged("BankName");
                Bankname = false;
                CustBankDetailsOverAll = false;
            }
        }
        private string _bankbranchname;
        public string BankBranchName
        {
            get
            {
                return _bankbranchname;
            }
            set
            {
                _bankbranchname = value;
                RaisedPropertyChanged("BankBranchName");
                BranchName = false;
                CustBankDetailsOverAll = false;
            }
        }
        private string _ifsccode;
        public string IFSCCode
        {
            get
            {
                return _ifsccode;
            }
            set
            {
                if (value != _ifsccode)
                {
                    _ifsccode = value.ToUpper();
                    RaisedPropertyChanged("IFSCCODE");
                    BIfscCode = false;
                    CustBankDetailsOverAll = false;
                }
            }
        }
        private string _micrcode;
        public string MICRCode
        {
            get
            {
                return _micrcode;
            }
            set
            {
                if (value != _micrcode)
                {
                    _micrcode = value.ToUpper();
                    RaisedPropertyChanged("MICRCode");
                    BMicrCode = false;
                    CustBankDetailsOverAll = false;
                }
            }
        }

        private string _gender;
        public string Gender
        {
            get
            {
                return _gender;
            }
            set
            {
                _gender = value;
                CustGender = false;
                CustDetailsOverAll = false;
            }
        }

        private string _fatherName;
        public string FatherName
        {
            get
            {
                return _fatherName;
            }
            set
            {
                _fatherName = value;
                CustFatherName = false;
                CustDetailsOverAll = false;
            }
        }

        private string _motherName;
        public string MotherName
        {
            get
            {
                return _motherName;
            }
            set
            {
                _motherName = value;
                CustMotherName = false;
                CustDetailsOverAll = false;
            }
        }

        private string _husbandName;
        public string HusbandName
        {
            get
            {
                return _husbandName;
            }
            set
            {
                _husbandName = value;
                CustHusbandName = false;
                CustDetailsOverAll = false;
            }
        }

        private string _caste;
        public string Caste
        {
            get
            {
                return _caste;
            }
            set
            {
                _caste = value;
                CustCaste = false;
                CustDetailsOverAll = false;
            }
        }

        private int _monthlyExpenses;
        public int MothlyExpenses
        {
            get
            {
                return _monthlyExpenses;
            }
            set
            {
                _monthlyExpenses = value;
                CustMonthlyExpenses = false;
                CustDetailsOverAll = false;
            }
        }

        private int _yearIncome;
        public int YearlyIncome
        {
            get
            {
                return _yearIncome;
            }
            set
            {
                _yearIncome = value;
                CustFamilyYearlyIncome = false;
                CustDetailsOverAll = false;
            }
        }


        private string _branchname;

        public Customer()
        {
            AddReligion();
        }
        private void AddReligion()
        {
            _religionlist.Add("Hindu");
            _religionlist.Add("Muslim");
        }

        public void GetAllDetailsofCustomers()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select * from CustomerDetails where CustId='" + _customerId + "'";
                SqlDataReader sqlData = sqlCommand.ExecuteReader();
                while (sqlData.Read())
                {
                    _customerName = sqlData.GetString(1);
                    _fatherName = sqlData.GetString(2);
                    _motherName = sqlData.GetString(3);
                    _dateofBirth = sqlData.GetDateTime(4);
                    _age = sqlData.GetInt32(5);
                    _gender = sqlData.GetString(6);
                    _contactNumber = sqlData.GetString(7);
                    _aadharNumber = sqlData.GetString(8);
                    _religion = sqlData.GetString(9);
                    _caste = sqlData.GetString(10);
                    _community = sqlData.GetString(11);
                    _education = sqlData.GetString(12);
                    _familymembers = sqlData.GetInt32(13);
                    _earningmembers = sqlData.GetInt32(14);
                    _occupation = sqlData.GetString(15);
                    _monthlyIncome = sqlData.GetInt32(16);
                    _monthlyExpenses = sqlData.GetInt32(17);
                    try
                    {
                        string[] _fullAdress = sqlData.GetString(18).Split('|', '~');
                        _doorNumber = _fullAdress[0];
                        _streetName = _fullAdress[2];
                        _localityTown = _fullAdress[4];
                        _taluk = _fullAdress[6];
                        _city = _fullAdress[8];
                        _state = _fullAdress[10];
                    }
                    catch (Exception ex)
                    {

                    }
                   
                    
                    _pincode = sqlData.GetInt32(19);
                    _housingType = sqlData.GetString(20);
                    if (sqlData.GetBoolean(23))
                    {
                        _havingBankDetails = true;
                        _accountHolderName = sqlData.GetString(27);
                        _accountnumber = sqlData.GetString(28);
                        _bankname = sqlData.GetString(29);
                        _bankbranchname = sqlData.GetString(30);
                        _ifsccode = sqlData.GetString(31);
                        _micrcode = sqlData.GetString(32);
                    }
                    NameofAddressProof = sqlData.GetString(21);
                    if (sqlData.GetBoolean(24))
                    {
                        //_addressProof = ByteToBI((byte[])sqlData.GetValue(33));
                        string FolderPath = MainWindow.DriveBasePath + "\\" + MainWindow.LoginDesignation.RegionName + "\\" + MainWindow.LoginDesignation.BranchName + "\\" + "Customer\\Address Proof";
                        _addressProof = SaveImageToDrive.GetImage(FolderPath, _customerId);
                    }
                    NameofPhotoProof = sqlData.GetString(22);
                    if (sqlData.GetBoolean(25))
                    {
                        // _photoProof = ByteToBI((byte[])sqlData.GetValue(34));
                        string FolderPath = MainWindow.DriveBasePath + "\\" + MainWindow.LoginDesignation.RegionName + "\\" + MainWindow.LoginDesignation.BranchName + "\\" + "Customer\\Photo Proof";
                        _photoProof = SaveImageToDrive.GetImage(FolderPath, _customerId);
                    }
                    if (sqlData.GetBoolean(26))
                    {
                        //_profilePicture = ByteToBI((byte[])sqlData.GetValue(35));
                        string FolderPath = MainWindow.DriveBasePath + "\\" + MainWindow.LoginDesignation.RegionName + "\\" + MainWindow.LoginDesignation.BranchName + "\\" + "Customer\\Profile Picture";
                        _profilePicture = SaveImageToDrive.GetImage(FolderPath, _customerId);
                    }
                       
                    if (sqlData.GetBoolean(36))
                        _ishavingGuarantorAlready = true;
                    if (sqlData.GetBoolean(37))
                        _ishavingNomineeAlready = true;
                    _husbandName = sqlData.GetString(40);
                    _yearIncome = sqlData.GetInt32(41);
                    if(sqlData.GetBoolean(42))
                    {
                        //_combinePhoto = ByteToBI((byte[])sqlData.GetValue(43));
                        string Folderpath = MainWindow.DriveBasePath + "\\" + MainWindow.LoginDesignation.RegionName + "\\" + MainWindow.LoginDesignation.BranchName + "\\" + "Customer\\Combine Photo";
                        _combinePhoto = SaveImageToDrive.GetImage(Folderpath, _customerId);
                    }
                       
                    _photoProofNo = sqlData.GetString(44);
                    _AddressProofNo = sqlData.GetString(45);
                    ResidencyType = (DBNull.Value.Equals(sqlData["Residency"])) ? "" : sqlData.GetString(46);
                    LandHolding = (DBNull.Value.Equals(sqlData["LandHolding"])) ? "" : sqlData.GetString(47);

                    LandType = (DBNull.Value.Equals(sqlData["LandType"])) ? "" : sqlData.GetString(48);
                    LandVolume = (DBNull.Value.Equals(sqlData["LandVolume"])) ? "" : sqlData.GetString(49);


                }
                sqlData.Close();
                
                sqlCommand.CommandText = "select IsLeader from CustomerGroup where CustId='" + _customerId + "'";
                sqlData = sqlCommand.ExecuteReader();
                while(sqlData.Read())
                {
                    _isLeader = sqlData.GetBoolean(0);
                }
            }
        }
        public void SaveCustomerDetails(string Region, string BranchName, string SelfHelpGroup, string PeerGroup, Guarantor guarantor, Nominee nominee)
        {
            _branchname = BranchName;
            AddCustomerDetails(Region, BranchName, SelfHelpGroup, PeerGroup);
            guarantor._customerId = _customerId;
            nominee._customerId = _customerId;
            if (guarantor.IsGuarantorNull)
            {
                guarantor.AddGuarantorDetails();
            }
            if (nominee.IsNomineeNull)
            {
                nominee.AddNomineeDetails();
            }
            if (AddressProof != null)
            {
               // AddAddressProof();
                AddAddressProofToDrive(_branchname);
            }
            if (PhotoProof != null)
            {
                //AddPhotoProof();
                AddPhotoProofToDrive();
            }
            if (ProfilePicture != null)
            {
               // AddProfilePhoto();
                AddProfilePhotoToDrive();
            }
            if(HavingBankDetails)
            {
                AddBankDetails();
                
            }
            if  (LandHolding!=null)
            {
                if(LandHolding.Equals("YES"))
                {
                    AddHouseTypeDetails(true);
                }
                else if(LandHolding.Equals("NO"))
                {
                    AddHouseTypeDetails();
                }
                
            }
            if (AadharNo!="")
            {
                AddAadharNumber();
            }
            if(CombinePhoto!=null)
            {
                //AddCombinePhoto();
                AddCombinePhotoToDrive();
            }
            EmployeeID = MainWindow.LoginDesignation.EmpId;
            CustomerID = _customerId;
            BranchID = MainWindow.LoginDesignation.BranchId;
            string CenterID =SelfHelpGroup;
            SendRequest(Region, BranchName,CenterID);
            //CheckAndChangeStatus();

        }

        void AddHouseTypeDetails(bool IsHavingLand=false)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                if(sqlConnection.State==ConnectionState.Open)
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = sqlConnection;
                    if(IsHavingLand)
                    {
                        sqlCommand.CommandText = "update CustomerDetails set Residency='"+ResidencyType+"',LandHolding='"+LandHolding+"',LandType='"+LandType+"',LandVolume='"+LandVolume+"' where CustId = '" + _customerId + "'";
                        sqlCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        sqlCommand.CommandText = "update CustomerDetails set Residency='" + ResidencyType + "',LandHolding='" + LandHolding + "' where CustId = '" + _customerId + "'";
                        sqlCommand.ExecuteNonQuery();
                    }
                    
                }
                
            }
        }


        void AddBankDetails()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "update CustomerDetails set BankACHolderName = '"+AccountHolder+"' , BankAccountNo='" + AccountNumber + "' ,BankName = '"+BankName+"',BankBranchName='"+BankBranchName+"',IFSCCode='"+IFSCCode+"',MICRCode='"+MICRCode+"',IsBankDetails='True' where CustId = '" + _customerId + "'";
                sqlCommand.ExecuteNonQuery();
            }
        }
        void AddAddressProof()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "update CustomerDetails set AddressProof = @addressproof , AddressProofName='" + NameofAddressProof + "' ,IsAddressProof = 'True' where CustId = '" + _customerId + "'";
                sqlCommand.Parameters.AddWithValue("@addressproof", Convertion(AddressProof));
                sqlCommand.ExecuteNonQuery();
            }
        }


        //for Drive Insert
        void AddAddressProofToDrive(string BName)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "update CustomerDetails set AddressProofName='" + NameofAddressProof + "' ,IsAddressProof = 'True' where CustId = '" + _customerId + "'";
                sqlCommand.ExecuteNonQuery();
                string Folderpath = MainWindow.DriveBasePath + "\\" + MainWindow.LoginDesignation.RegionName + "\\" + MainWindow.LoginDesignation.BranchName + "\\" + "Customer\\Address Proof";
                SaveImageToDrive.SaveImage(Folderpath, _customerId, Convertion(AddressProof));
            }
        }
        void AddPhotoProof()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "update CustomerDetails set PhotoProof = @photoProof,IsPhotoProof = 'True',PhotoProofName='" + NameofPhotoProof + "' where CustId = '" + _customerId + "'";
                sqlCommand.Parameters.AddWithValue("@photoproof", Convertion(PhotoProof));
                sqlCommand.ExecuteNonQuery();
            }
        }

        void AddPhotoProofToDrive()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "update CustomerDetails set IsPhotoProof = 'True',PhotoProofName='" + NameofPhotoProof + "' where CustId = '" + _customerId + "'";
                sqlCommand.ExecuteNonQuery();
                byte[] data= Convertion(PhotoProof);
                string Folderpath = MainWindow.DriveBasePath + "\\" + MainWindow.LoginDesignation.RegionName + "\\" + MainWindow.LoginDesignation.BranchName + "\\" + "Customer\\Photo Proof";
                SaveImageToDrive.SaveImage(Folderpath, _customerId, data);
                
            }
        }
        void AddProfilePhoto()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "update CustomerDetails set IsProfilePhote = @combinephoto, IsCombinePhoto = 'True' where CustId = '" + _customerId + "'";
                sqlCommand.Parameters.AddWithValue("@combinephoto", Convertion(CombinePhoto));
                sqlCommand.ExecuteNonQuery();
            }
        }

        void AddProfilePhotoToDrive()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "update CustomerDetails set IsProfilePhoto = 'True' where CustId = '" + _customerId + "'";
                sqlCommand.ExecuteNonQuery();



                byte[] data=(CombinePhoto!=null)?Convertion(CombinePhoto):null;
                sqlCommand.CommandText = "update CustomerDetails set IsProfilePhote = @combinephoto, IsCombinePhoto = 'True' where CustId = '" + _customerId + "'";
                string Folderpath = MainWindow.DriveBasePath + "\\" + MainWindow.LoginDesignation.RegionName + "\\" + MainWindow.LoginDesignation.BranchName + "\\" + "Customer\\Profile Picture";
                SaveImageToDrive.SaveImage(Folderpath, _customerId, data);

            }
        }
        void AddCombinePhoto()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "update CustomerDetails set ProfilePhoto = @addressProof, IsProfilePhoto = 'True' where CustId = '" + _customerId + "'";
                sqlCommand.Parameters.AddWithValue("@addressproof", Convertion(ProfilePicture));
                sqlCommand.ExecuteNonQuery();
            }
        }
        void AddCombinePhotoToDrive()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "update CustomerDetails set  IsCombinePhoto = 'True' where CustId = '" + _customerId + "'";
                sqlCommand.ExecuteNonQuery();
                
                byte[] data= Convertion(ProfilePicture);
                string Folderpath = MainWindow.DriveBasePath + "\\" + MainWindow.LoginDesignation.RegionName + "\\" + MainWindow.LoginDesignation.BranchName + "\\" + "Customer\\Combine Photo";
                SaveImageToDrive.SaveImage(Folderpath, _customerId, data);
               
            }
        }
        void AddAadharNumber()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "update CustomerDetails set AadharNumber='"+AadharNo+"' where CustId = '" + _customerId + "'";
                sqlCommand.ExecuteNonQuery();
            }
        }
        public string GetCustId(string BranchName,string Region)
        {
            GenerateCustomerId GCID = new GenerateCustomerId();
            GCID.BranchName = BranchName;
            GCID.Region = Region;
            return GCID.GenerateCustomerID();
        }
        public void AddCustomerDetails(string Region, string BranchName, string SelfHelpGroup, string PeerGroup)
        {
            
            string AddressofCustomer = DoorNumber + "|~" + StreetName + "|~" + LocalityTown + "|~" + Taluk + "|~" + City + "|~" + State;
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
//                sqlCommand.CommandText = @"insert into CustomerDetails(CustId, Name, Dob, Age, Mobile, Religion, Community, Education, FamilyMembers, EarningMembers, Occupation, MonthlyIncome, Address, Pincode, HousingType, HousingIndex,IsAddressProof, IsPhotoProof, IsProfilePhoto, GuarenteeStatus, NomineeStatus,IsActive,CustomerStatus,FatherName,MotherName,Gender,Caste,MonthlyExpenses,IsBankDetails,AadharNumber) 
//values ('" + _customerId + "','" + CustomerName + "','" + DateofBirth.ToString("yyyy-MM-dd") + "','" + Age + "','" + ContactNumber + "','" + Religion + "','" + Community +
//"','" + Education + "','" + FamilyMembers + "','" + EarningMembers + "','" + Occupation + "','" + MonthlyIncome + "','" + AddressofCustomer + "','" + Pincode + "','" +
//HousingType + "','" + HousingIndex + "','" + false + "','" + false + "','" + false + "','" + false + "','" + false + "','" + false + "','" + 0 + "','" + FatherName + 
//"','" + MotherName + "','" + Gender + "','" + Caste + "','" + MothlyExpenses + "','" + false + "','" + null + "')";

                sqlCommand.CommandText = "insert into CustomerDetails(CustId, Name, FatherName, MotherName, Dob, Age, Gender, Mobile,AadharNumber,Religion, Caste, Community,Education, FamilyMembers, EarningMembers, Occupation, MonthlyIncome, MonthlyExpenses, Address,Pincode, HousingType, IsBankDetails, IsAddressProof, IsPhotoProof, IsProfilePhoto, BankACHolderName, BankAccountNo, BankName,BankBranchName, IFSCCode, MICRCode, GuarenteeStatus, NomineeStatus, CustomerStatus, IsActive,HusbandName,YearlyIncome,IsCombinePhoto,PhotoProofName,PhotoProofNo,AddressProofName,AddressProofNo)values(@custId, @name, @fatherName, @motherName, @dob, @age, @gender, @mobile, @aadhar, @religion, @caste, @community, @education, @familyMembers,@earningMembers, @occupation, @monthlyIncome, @monthlyExpence, @address, @pincode, @houseType,  @isBankDetails,@isAddressProof, @isPhotoproof, @isProfilePhoto, @bankAccHolder, @bankAcNo, @banckName, @bankBranchName, @ifsc, @micr, @guarenteeStatus, @nomineeStatus, @customerStatus, @isActive,'"+HusbandName+"','"+YearlyIncome+"','"+false+ "','" + _nameofPhotoProof + "','" + _photoProofNo+"','"+_nameofAddressProof+"','"+_AddressProofNo+"')";

                sqlCommand.Parameters.AddWithValue("@custId", _customerId);sqlCommand.Parameters.AddWithValue("@name", CustomerName);
                sqlCommand.Parameters.AddWithValue("@fatherName",_fatherName);sqlCommand.Parameters.AddWithValue("@motherName",_motherName);
                sqlCommand.Parameters.AddWithValue("@dob", DateofBirth.ToString("MM-dd-yyyy"));sqlCommand.Parameters.AddWithValue("@age",Age);

                sqlCommand.Parameters.AddWithValue("@gender",Gender);sqlCommand.Parameters.AddWithValue("@mobile",ContactNumber);
                sqlCommand.Parameters.AddWithValue("@aadhar",AadharNo);sqlCommand.Parameters.AddWithValue("@religion",Religion);
                sqlCommand.Parameters.AddWithValue("@caste",Caste);sqlCommand.Parameters.AddWithValue("@community",Community);

                sqlCommand.Parameters.AddWithValue("@education",Education);sqlCommand.Parameters.AddWithValue("@familyMembers",FamilyMembers);
                sqlCommand.Parameters.AddWithValue("@earningMembers",EarningMembers);sqlCommand.Parameters.AddWithValue("@occupation",Occupation);
                sqlCommand.Parameters.AddWithValue("@monthlyIncome",MonthlyIncome);sqlCommand.Parameters.AddWithValue("@monthlyExpence",_monthlyExpenses);
                sqlCommand.Parameters.AddWithValue("@address", AddressofCustomer);sqlCommand.Parameters.AddWithValue("@pincode",Pincode);
                sqlCommand.Parameters.AddWithValue("@houseType",HousingType);

                //sqlCommand.Parameters.AddWithValue("@addressProofName", NameofAddressProof); sqlCommand.Parameters.AddWithValue("@photoProffName",NameofPhotoProof);
                sqlCommand.Parameters.AddWithValue("@isBankDetails",HavingBankDetails); sqlCommand.Parameters.AddWithValue("@isAddressProof",false);
                sqlCommand.Parameters.AddWithValue("@isPhotoproof",false); sqlCommand.Parameters.AddWithValue("@isProfilePhoto",false);
                sqlCommand.Parameters.AddWithValue("@bankAccHolder", AccountHolder); sqlCommand.Parameters.AddWithValue("@bankAcNo",AccountNumber);
                
                sqlCommand.Parameters.AddWithValue("@banckName",BankName); sqlCommand.Parameters.AddWithValue("@bankBranchName",BankBranchName);
                sqlCommand.Parameters.AddWithValue("@ifsc",IFSCCode);sqlCommand.Parameters.AddWithValue("@micr",MICRCode);

                //sqlCommand.Parameters.AddWithValue("@addressProof", Convertion(AddressProof)); sqlCommand.Parameters.AddWithValue("@photoProof", Convertion(PhotoProof));
                //sqlCommand.Parameters.AddWithValue("@profilePhoto", Convertion(ProfilePicture));
                
                sqlCommand.Parameters.AddWithValue("@guarenteeStatus",false);

                sqlCommand.Parameters.AddWithValue("@nomineeStatus",false); sqlCommand.Parameters.AddWithValue("@customerStatus",1);
                sqlCommand.Parameters.AddWithValue("@isActive",true);

                if (sqlCommand.ExecuteNonQuery() == 1)
                {
                    string CenterID = SelfHelpGroup;
                    InsertIntoCustomerGroup(_customerId, PeerGroup, IsLeader, GetMembersCountINPeerGroup(PeerGroup), CenterID);
                }
                    

            }
        }

        void InsertIntoCustomerGroup(string custId, string pgId, bool isLeader, int cpId,string SHGID)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlConnection;
                cmd.CommandText = "insert into CustomerGroup (CustId, PeerGroupId, IsLeader, CPid,SHGID)values(@custId, @pgId, @isLeader, @cPid,@shgid)";
                cmd.Parameters.AddWithValue("@custId", custId);
                cmd.Parameters.AddWithValue("@pgId", pgId);
                cmd.Parameters.AddWithValue("@isLeader", isLeader);
                cmd.Parameters.AddWithValue("@cPid", cpId);
                cmd.Parameters.AddWithValue("@shgid", SHGID);
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        int GetMembersCountINPeerGroup(string peerGroupId)
        {
            int Count = 1;
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlConnection;
                cmd.CommandText = "select COUNT(CustId) from CustomerGroup where PeerGroupId = '"+ peerGroupId + "'";
                Count += (int)cmd.ExecuteScalar();
                sqlConnection.Close();
            }
            return Count;
        }
        public void UpdateExistingDetails(string BranchName, string SelfHelpGroup, string PeerGroup, Guarantor guarantor, Nominee nominee)
        {
            _branchname = BranchName;
            ChangeCustomerDetails(BranchName, SelfHelpGroup, PeerGroup);
            guarantor._customerId = _customerId;
            nominee._customerId = _customerId;
            if (guarantor.IsGuarantorNull)
            {
                if (_ishavingGuarantorAlready)
                    guarantor.UpdateGuarantorDetails();
                else
                    guarantor.AddGuarantorDetails();
            }
            if (nominee.IsNomineeNull)
            {
                if (_ishavingNomineeAlready)
                    nominee.UpdateNomineeDetails();
                else
                    nominee.AddNomineeDetails();
            }
            if (AddressProof != null)
            {
                //AddAddressProof();
                AddAddressProofToDrive(_branchname);
            }
            if (PhotoProof != null)
            {
                //AddPhotoProof();
                AddPhotoProofToDrive();
            }
            if (ProfilePicture != null)
            {
                //AddProfilePhoto();
                AddProfilePhotoToDrive();
            }
            if(HavingBankDetails)
            {
                AddBankDetails();
            }
            if(AadharNo!="")
            {
                AddAadharNumber();
            }
            if(CombinePhoto!=null)
            {
                //AddCombinePhoto();
                AddCombinePhotoToDrive();
            }
            //CheckAndChangeStatus();
        }
        
        public void ChangeCustomerDetails(string BranchName, string SelfHelpGroup, string PeerGroup)
        {
            string AddressofCustomer = DoorNumber + "|~" + StreetName + "|~" + LocalityTown + "|~" + Taluk + "|~" + City + "|~" + State;
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "update CustomerDetails set Name='" + CustomerName + "',Dob='" + DateofBirth.ToString("yyyy-MM-dd") + "',Age='" + Age + "',Mobile='" + ContactNumber + "',Religion='" + Religion + "',Community='" + Community + "',Education='" + Education + "',FamilyMembers='" + FamilyMembers + "',EarningMembers='" + EarningMembers + "',Occupation='" + Occupation + "',MonthlyIncome='" + MonthlyIncome + "',Address='" + AddressofCustomer + "',Pincode='" + Pincode + "',HousingType='" + HousingType + "',FatherName='"+FatherName+"',MotherName='"+MotherName+"',Gender='"+Gender+"',Caste='"+Caste+"',MonthlyExpenses='"+MothlyExpenses+"',HusbandName='"+HusbandName+"',YearlyIncome='"+YearlyIncome+"',AddressProofName='"+_nameofAddressProof+"',AddressProofNo='"+_AddressProofNo+"',PhotoProofName='"+_nameofPhotoProof+"',PhotoProofNo='"+_photoProofNo+"',LandHolding='"+_landholding+"',Residency='"+ResidencyType+"',LandType='"+LandType+"',LandVolume='"+LandVolume+"' where CustId='" + _customerId + "'";
                sqlCommand.ExecuteNonQuery();
                sqlCommand.CommandText = "select Bid from BranchDetails where BranchName='" + BranchName + "'";
                string BranchId = sqlCommand.ExecuteScalar().ToString();
                string CenterID = SelfHelpGroup;
                //sqlCommand.CommandText = "update CustomerGroup set SHGID='" + CenterID + "', PeerGroupId='" + PeerGroup + "',IsLeader='" + IsLeader + "' where CustId='"+_customerId+"' and SHGID='"+CenterID+"'";
                //int res= sqlCommand.ExecuteNonQuery();
                UpdateCustomerGroup(_customerId, CenterID, PeerGroup);
            }
        }

        public void UpdateCustomerGroup(string CustomerID,string CenterID,string PeerGroupID)
        {
            using (SqlConnection sqlconn=new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.Connection = sqlconn;
                sqlcomm.CommandText = "update CustomerGroup set SHGID='" + CenterID + "', PeerGroupId='" + PeerGroupID + "',IsLeader='" + IsLeader + "' where CustId='" + _customerId + "'";
                int res= sqlcomm.ExecuteNonQuery();
                string EmployeeID = MainWindow.BasicDetails.CenterList.Where(temp => temp.SHGId == CenterID).Select(temp => temp.EmpId).FirstOrDefault();
                sqlcomm.CommandText = "Update LoanApplication set EmployeeId='" + EmployeeID + "' , SHGId='" + CenterID + "' where CustId='" + CustomerID + "'";
                int res1= sqlcomm.ExecuteNonQuery();
            }
        }

        public void GetVerfiedDetailsofCustomer()
        {
            using(SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlConnection;
                command.CommandText = "select CustomerName,CustomerGender,CustomerDOB,FatherName,MotherName,Guardian,Contact,CAddress,Religion,Caste,Community,Education,FamilyMembers,EarningMembers,Occupation,MonthlyIncome,MonthlyExpence,FamilyAnnualIncome,CustomerDoorNo,CustomerStreet,CustomerLocality,CustomerCity,CustomerState,CustomerPincode,HouseType,CAddressProof,CPhotoProof,CProfilePic,CustomerCombinePhoto,AccountHolderName,AccountNo,BankName,BranchName,IFSC,MICR from CustomerVerification where CustId='" + _customerId + "'";
                SqlDataReader dataReader = command.ExecuteReader();
                while(dataReader.Read())
                {
                    CustName = dataReader.GetBoolean(0);
                    CustGender= dataReader.GetBoolean(1);
                    CustDateOfBirth= dataReader.GetBoolean(2);
                    CustFatherName= dataReader.GetBoolean(3);
                    CustMotherName= dataReader.GetBoolean(4);
                    CustHusbandName= dataReader.GetBoolean(5);
                    CustContactNumber= dataReader.GetBoolean(6);
                    CustAadharNumber= dataReader.GetBoolean(7);
                    CustReligion= dataReader.GetBoolean(8);
                    CustCaste = dataReader.GetBoolean(9);
                    CustCommunity= dataReader.GetBoolean(10);
                    CustEducation= dataReader.GetBoolean(11);
                    CustFamilyMember= dataReader.GetBoolean(12);
                    CustEarningMember= dataReader.GetBoolean(13);
                    CustOccupation= dataReader.GetBoolean(14);
                    CustMonthlyIncome= dataReader.GetBoolean(15);
                    CustMonthlyExpenses= dataReader.GetBoolean(16);
                    CustFamilyYearlyIncome= dataReader.GetBoolean(17);
                    CustomerDoorNumber= dataReader.GetBoolean(18);
                    CustStreetName= dataReader.GetBoolean(19);
                    CustomerLocality= dataReader.GetBoolean(20);
                    CustomerCity= dataReader.GetBoolean(21);
                    CustomerState= dataReader.GetBoolean(22);
                    CustomerPincode= dataReader.GetBoolean(23);
                    CustomerHousingType= dataReader.GetBoolean(24);
                    CustomerAddressProof= dataReader.GetBoolean(25);
                    CustomerPhotoProof= dataReader.GetBoolean(26);
                    CustomerProfilePicture= dataReader.GetBoolean(27);
                    Combinephoto= dataReader.GetBoolean(28);
                    BankHolderName = dataReader.GetBoolean(29);
                    BankAccountNo = dataReader.GetBoolean(30);
                    Bankname = dataReader.GetBoolean(31);
                    BranchName = dataReader.GetBoolean(32);
                    BIfscCode = dataReader.GetBoolean(33);
                    BMicrCode = dataReader.GetBoolean(34);
                }
            }
        }
        public void CheckAndChangeStatus()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                bool _check = false;
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                //sqlCommand.CommandText = "select IsAddressProof, IsPhotoProof, IsProfilePhoto, GuarenteeStatus, NomineeStatus,IsBankDetails from CustomerDetails where CustId='" + _customerId + "'";
                //SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                //while (sqlDataReader.Read())
                //{
                //    if ((sqlDataReader.GetBoolean(0)==true && sqlDataReader.GetBoolean(1)==true && sqlDataReader.GetBoolean(2)==true && sqlDataReader.GetBoolean(3)==true && sqlDataReader.GetBoolean(4)==true && sqlDataReader.GetBoolean(5)==true ))
                //    {
                //        _check = true;
                //    }

                //}
                //sqlDataReader.Close();
                //bool _checkAadhar = false;
                //sqlCommand.CommandText = "select AadharNumber from CustomerDetails where CustId='" + _customerId + "'";
                //sqlDataReader = sqlCommand.ExecuteReader();
                //while(sqlDataReader.Read())
                //{
                //    if(!sqlDataReader.IsDBNull(0))
                //    {
                //        string _ad = sqlDataReader.GetString(0);
                //        if(!string.IsNullOrEmpty(_ad))
                //        _checkAadhar = true;
                //    }
                //}
                //sqlDataReader.Close();
                //if (_check==true && _checkAadhar==true)
                //{
                    sqlCommand.CommandText = "update CustomerDetails set CustomerStatus='1' where CustId='" + _customerId + "'";
                    sqlCommand.ExecuteNonQuery();
                //}
            }
        }
        private bool _ishavingGuarantorAlready = false;
        private bool _ishavingNomineeAlready = false;
        public bool IsPeerGroupFull(string pgID)
        {
            int count = 0;
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select COUNT(CustId) from CustomerGroup where PeerGroupId = '" + pgID + "'";
                count = (int)sqlCommand.ExecuteScalar();
                sqlConnection.Close();
            }
            if (count >= 5)
                return false;
            else
                return true;

        }
    }
}
