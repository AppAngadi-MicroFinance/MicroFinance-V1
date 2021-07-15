using MicroFinance.Validations;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MicroFinance.Modal
{
    public class Customer : LoanDetails
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
        public string NameofAddressProof { get; set; }
        public string NameofPhotoProof { get; set; }

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
            }
        }




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
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.db))
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
                    _religion = sqlData.GetString(8);
                    _caste = sqlData.GetString(9);
                    _community = sqlData.GetString(10);
                    _education = sqlData.GetString(11);
                    _familymembers = sqlData.GetInt32(12);
                    _earningmembers = sqlData.GetInt32(13);
                    _occupation = sqlData.GetString(14);
                    _monthlyIncome = sqlData.GetInt32(15);
                    _monthlyExpenses = sqlData.GetInt32(16);
                    string[] _fullAdress = sqlData.GetString(17).Split('|', '~');
                    _doorNumber = _fullAdress[0];
                    _streetName = _fullAdress[2];
                    _localityTown = _fullAdress[4];
                    _city = _fullAdress[6];
                    _state = _fullAdress[8];
                    _pincode = sqlData.GetInt32(18);
                    _housingType = sqlData.GetString(19);
                    _housingIndex = sqlData.GetInt32(20).ToString(); 
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
                    if (sqlData.GetBoolean(24))
                    {
                        NameofAddressProof = sqlData.GetString(21);
                        _addressProof = ByteToBI((byte[])sqlData.GetValue(33));
                    }
                    if (sqlData.GetBoolean(25))
                    {
                        NameofPhotoProof = sqlData.GetString(22);
                        _photoProof = ByteToBI((byte[])sqlData.GetValue(34));
                    }
                    if (sqlData.GetBoolean(26))
                        _profilePicture = ByteToBI((byte[])sqlData.GetValue(35));
                    if (sqlData.GetBoolean(36))
                        _ishavingGuarantorAlready = true;
                    if (sqlData.GetBoolean(37))
                        _ishavingNomineeAlready = true;
                    _aadharNumber = sqlData.GetString(40);

                }
                sqlData.Close();
                sqlCommand.CommandText = "select IsLeader from CustomerGroup where CustId='" + _customerId + "'";
                _isLeader = (bool)sqlCommand.ExecuteScalar();
            }
        }
        public void SaveCustomerDetails(string Region, string BranchName, string SelfHelpGroup, string PeerGroup, Guarantor guarantor, Nominee nominee)
        {

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
                AddAddressProof();
            }
            if (PhotoProof != null)
            {
                AddPhotoProof();
            }
            if (ProfilePicture != null)
            {
                AddProfilePhoto();
            }
            if(HavingBankDetails)
            {
                AddBankDetails();
            }
            if(AadharNo!="")
            {
                AddAadharNumber();
            }
            CheckAndChangeStatus();

        }
        void AddBankDetails()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.db))
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
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.db))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "update CustomerDetails set AddressProof = @addressproof , AddressProofName='" + NameofAddressProof + "' ,IsAddressProof = 'True' where CustId = '" + _customerId + "'";
                sqlCommand.Parameters.AddWithValue("@addressproof", Convertion(AddressProof));
                sqlCommand.ExecuteNonQuery();
            }
        }
        void AddPhotoProof()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.db))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "update CustomerDetails set PhotoProof = @photoProof,IsPhotoProof = 'True',PhotoProofName='" + NameofPhotoProof + "' where CustId = '" + _customerId + "'";
                sqlCommand.Parameters.AddWithValue("@photoproof", Convertion(PhotoProof));
                sqlCommand.ExecuteNonQuery();
            }
        }
        void AddProfilePhoto()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.db))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "update CustomerDetails set ProfilePhoto = @addressProof, IsProfilePhoto = 'True' where CustId = '" + _customerId + "'";
                sqlCommand.Parameters.AddWithValue("@addressproof", Convertion(ProfilePicture));
                sqlCommand.ExecuteNonQuery();
            }
        }
        void AddAadharNumber()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.db))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "update CustomerDetails set AadharNumber='"+AadharNo+"' where CustId = '" + _customerId + "'";
                sqlCommand.ExecuteNonQuery();
            }
        }
        public void AddCustomerDetails(string Region, string BranchName, string SelfHelpGroup, string PeerGroup)
        {
            GenerateCustomerId GCID = new GenerateCustomerId();
            GCID.BranchName = BranchName;
            GCID.Region = Region;
            _customerId = GCID.GenerateCustomerID();
            string AddressofCustomer = DoorNumber + "|~" + StreetName + "|~" + LocalityTown + "|~" + City + "|~" + State;
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.db))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "insert into CustomerDetails(CustId, Name, Dob, Age, Mobile, Religion, Community, Education, FamilyMembers, EarningMembers, Occupation, MonthlyIncome, Address, Pincode, HousingType, HousingIndex,IsAddressProof, IsPhotoProof, IsProfilePhoto, GuarenteeStatus, NomineeStatus,IsActive,CustomerStatus,FatherName,MotherName,Gender,Caste,MonthlyExpenses,IsBankDetails,AadharNumber) values ('" + _customerId + "','" + CustomerName + "','" + DateofBirth.ToString("yyyy-MM-dd") + "','" + Age + "','" + ContactNumber + "','" + Religion + "','" + Community + "','" + Education + "','" + FamilyMembers + "','" + EarningMembers + "','" + Occupation + "','" + MonthlyIncome + "','" + AddressofCustomer + "','" + Pincode + "','" + HousingType + "','" + HousingIndex + "','" + false + "','" + false + "','" + false + "','" + false + "','" + false + "','" + false + "','" + 0 + "','"+FatherName+"','"+MotherName+"','"+Gender+"','"+Caste+"','"+MothlyExpenses+"','"+false+"','"+null+"')";
                sqlCommand.ExecuteNonQuery();
                sqlCommand.CommandText = "select Bid from BranchDetails where BranchName='" + BranchName + "'";
                string BranchId = sqlCommand.ExecuteScalar().ToString();
                sqlCommand.CommandText = "insert into CustomerGroup(BranchId, BranchName, SelfHelpGroup, PeerGroup, CustId, IsLeader) values('" + BranchId + "','" + BranchName + "','" + SelfHelpGroup + "','" + PeerGroup + "','" + _customerId + "','" + IsLeader + "')";
                sqlCommand.ExecuteNonQuery();

            }
        }
        public void UpdateExistingDetails(string BranchName, string SelfHelpGroup, string PeerGroup, Guarantor guarantor, Nominee nominee)
        {
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
                AddAddressProof();
            }
            if (PhotoProof != null)
            {
                AddPhotoProof();
            }
            if (ProfilePicture != null)
            {
                AddProfilePhoto();
            }
            if(HavingBankDetails)
            {
                AddBankDetails();
            }
            if(AadharNo!="")
            {
                AddAadharNumber();
            }
            CheckAndChangeStatus();
        }
        public void ChangeCustomerDetails(string BranchName, string SelfHelpGroup, string PeerGroup)
        {
            string AddressofCustomer = DoorNumber + "|~" + StreetName + "|~" + LocalityTown + "|~" + City + "|~" + State;
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.db))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "update CustomerDetails set Name='" + CustomerName + "',Dob='" + DateofBirth.ToString("yyyy-MM-dd") + "',Age='" + Age + "',Mobile='" + ContactNumber + "',Religion='" + Religion + "',Community='" + Community + "',Education='" + Education + "',FamilyMembers='" + FamilyMembers + "',EarningMembers='" + EarningMembers + "',Occupation='" + Occupation + "',MonthlyIncome='" + MonthlyIncome + "',Address='" + AddressofCustomer + "',Pincode='" + Pincode + "',HousingType='" + HousingType + "',HousingIndex='" + HousingIndex + "',FatherName='"+FatherName+"',MotherName='"+MotherName+"',Gender='"+Gender+"',Caste='"+Caste+"',MonthlyExpenses='"+MothlyExpenses+"' where CustId='" + _customerId + "'";
                sqlCommand.ExecuteNonQuery();
                sqlCommand.CommandText = "select Bid from BranchDetails where BranchName='" + BranchName + "'";
                string BranchId = sqlCommand.ExecuteScalar().ToString();
                sqlCommand.CommandText = "update CustomerGroup set BranchId='" + BranchId + "', SelfHelpGroup='" + SelfHelpGroup + "',BranchName='" + BranchName + "',PeerGroup='" + PeerGroup + "',IsLeader='" + IsLeader + "' where CustId='"+_customerId+"'";
                sqlCommand.ExecuteNonQuery();
            }
        }
        public void CheckAndChangeStatus()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.db))
            {
                bool _check = false;
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select IsAddressProof, IsPhotoProof, IsProfilePhoto, GuarenteeStatus, NomineeStatus,IsBankDetails from CustomerDetails where CustId='" + _customerId + "'";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    if ((sqlDataReader.GetBoolean(0)==true && sqlDataReader.GetBoolean(1)==true && sqlDataReader.GetBoolean(2)==true && sqlDataReader.GetBoolean(3)==true && sqlDataReader.GetBoolean(4)==true && sqlDataReader.GetBoolean(5)==true ))
                    {
                        _check = true;
                    }

                }
                sqlDataReader.Close();
                bool _checkAadhar = false;
                sqlCommand.CommandText = "select AadharNumber from CustomerDetails where CustId='" + _customerId + "'";
                sqlDataReader = sqlCommand.ExecuteReader();
                while(sqlDataReader.Read())
                {
                    if(!sqlDataReader.IsDBNull(0))
                    {
                        string _ad = sqlDataReader.GetString(0);
                        if(!string.IsNullOrEmpty(_ad))
                        _checkAadhar = true;
                    }
                }
                sqlDataReader.Close();
                if (_check==true && _checkAadhar==true)
                {
                    sqlCommand.CommandText = "update CustomerDetails set CustomerStatus='1' where CustId='" + _customerId + "'";
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }
        private bool _ishavingGuarantorAlready = false;
        private bool _ishavingNomineeAlready = false;

    }
}
