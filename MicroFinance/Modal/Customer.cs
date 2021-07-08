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
    public class Customer : BindableBase
    {
        public string ConnectionString = MicroFinance.Properties.Settings.Default.DBConnection;
        public string _customerId { get; set; }
        private string _region;
        public string Region
        {
            get
            {
                return _region;
            }
            set
            {
                _region = value;
                RaisedPropertyChanged("Region");
            }
        }
        private string _branch;
        public string Branch
        {
            get
            {
                return _branch;
            }
            set
            {
                _branch = value;
                RaisedPropertyChanged("Branch");
            }
        }
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
        private DateTime _dateofBirth=DateTime.Now;
        public DateTime DateofBirth
        {
            get
            {
                return  _dateofBirth;
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
        private List<string> _religionlist=new List<string>();
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
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select * from CustomerDetails where CustId='" + _customerId + "'";
                SqlDataReader sqlData = sqlCommand.ExecuteReader();
                while(sqlData.Read())
                {
                    _customerName = sqlData.GetString(1);
                    _dateofBirth = sqlData.GetDateTime(2);
                    _age = sqlData.GetInt32(3);
                    _contactNumber = sqlData.GetString(4);
                    _religion = sqlData.GetString(5);
                    _community = sqlData.GetString(6);
                    _education = sqlData.GetString(7);
                    _familymembers = sqlData.GetInt32(8);
                    _earningmembers = sqlData.GetInt32(9);
                    _occupation = sqlData.GetString(10);
                    _monthlyIncome = sqlData.GetInt32(11);
                    string[] _fullAdress = sqlData.GetString(12).Split('|','~');
                    _doorNumber = _fullAdress[0];
                    _streetName = _fullAdress[2];
                    _localityTown = _fullAdress[4];
                    _city = _fullAdress[6];
                    _state = _fullAdress[8];
                    _pincode = sqlData.GetInt32(13);
                    _housingType = sqlData.GetString(14);
                    _housingIndex = sqlData.GetInt32(15).ToString();
                    if (sqlData.GetBoolean(18))
                    {
                        NameofAddressProof = sqlData.GetString(16);
                        _addressProof = ByteToBI((byte[])sqlData.GetValue(21));
                    }
                    if(sqlData.GetBoolean(19))
                    {
                        NameofPhotoProof = sqlData.GetString(17);
                        _photoProof = ByteToBI((byte[])sqlData.GetValue(22));
                    }
                    if (sqlData.GetBoolean(20))
                        _profilePicture = ByteToBI((byte[])sqlData.GetValue(23));
                    if (sqlData.GetBoolean(24))
                        _ishavingGuarantorAlready = true;
                    if (sqlData.GetBoolean(25))
                        _ishavingNomineeAlready = true;

                }
                sqlData.Close();
                sqlCommand.CommandText = "select IsLeader from CustomerGroup where CustId='" + _customerId + "'";
                _isLeader = (bool)sqlCommand.ExecuteScalar();
            }
        }



        public string GetLastCustomerId(string BranchName,string SelfHelpGroup,string PeerGroup)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select TOP 1  CustId from CustomerGroup where BranchName = '" + BranchName + "' and SelfHelpGroup = '" + SelfHelpGroup + "' and PeerGroup = '" + PeerGroup + "' order by CustId desc";
                //getting last customer id in that group 
                String LastCustomerId = null;
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    LastCustomerId = sqlDataReader.GetString(0);
                }
                return LastCustomerId;
            }
        }
        string NextCustomerId(string LastCustomerId)
        {
            int temp = 0;
            int tense = 0;
            int i;
            for(i=LastCustomerId.Length-1;i>=0;i--)
            {
                if(char.IsDigit( LastCustomerId[i]))
                {
                    temp +=(int)Char.GetNumericValue( LastCustomerId[i]) * (int)Math.Pow(10, tense);
                    tense++;
                }
                else
                {
                    temp++;
                    break;
                }
            }
            string NextId = LastCustomerId.Substring(0, i+1) + temp.ToString();
            return NextId;
        }
        public void SaveCustomerDetails(string BranchName,string SelfHelpGroup,string PeerGroup,Guarantor guarantor,Nominee nominee)
        {

            AddCustomerDetails(BranchName, SelfHelpGroup, PeerGroup);
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
            if(AddressProof!=null)
            {
                AddAddressProof();
            }
            if(PhotoProof!=null)
            {
                AddPhotoProof();
            }
            if(ProfilePicture!=null)
            {
                AddProfilePhoto();
            }
            CheckAndChangeStatus();
            
        }
        void AddAddressProof()
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "update CustomerDetails set AddressProof = @addressproof , AddressProofName='"+NameofAddressProof+ "' ,IsAddressProof = 'True' where CustId = '" + _customerId + "'";
                sqlCommand.Parameters.AddWithValue("@addressproof", Convertion(AddressProof));
                sqlCommand.ExecuteNonQuery();
            }
        }
        void AddPhotoProof()
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "update CustomerDetails set PhotoProof = @photoProof,IsPhotoProof = 'True',PhotoProofName='"+NameofPhotoProof+"' where CustId = '" + _customerId + "'";
                sqlCommand.Parameters.AddWithValue("@photoproof", Convertion(PhotoProof));
                sqlCommand.ExecuteNonQuery();
            }
        }
        void AddProfilePhoto()
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "update CustomerDetails set ProfilePhoto = @addressProof, IsProfilePhoto = 'True' where CustId = '" + _customerId + "'";
                sqlCommand.Parameters.AddWithValue("@addressproof", Convertion(ProfilePicture));
                sqlCommand.ExecuteNonQuery();
            }
        }
        public void AddCustomerDetails(string BranchName,string SelfHelpGroup,string PeerGroup)
        {

            string _lastCustId = GetLastCustomerId(BranchName, SelfHelpGroup, PeerGroup);
            if(_lastCustId==null)
            {
                _lastCustId = "cc2";
            }
            _customerId= NextCustomerId(_lastCustId);

            string AddressofCustomer = DoorNumber + "|~" + StreetName + "|~" + LocalityTown + "|~" + City + "|~" + State;
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.db))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "insert into CustomerDetails(CustId, Name, Dob, Age, Mobile, Religion, Community, Education, FamilyMembers, EarningMembers, Occupation, MonthlyIncome, Address, Pincode, HousingType, HousingIndex,IsAddressProof, IsPhotoProof, IsProfilePhoto, GuarenteeStatus, NomineeStatus,IsActive,CustomerStatus) values ('" + _customerId + "','" + CustomerName + "','" + DateofBirth.ToString("yyyy-MM-dd") + "','" + Age + "','" + ContactNumber + "','" + Religion + "','" + Community + "','" + Education + "','" + FamilyMembers + "','" + EarningMembers + "','" + Occupation + "','" + MonthlyIncome + "','" + AddressofCustomer + "','" + Pincode + "','" + HousingType + "','" + HousingIndex + "','"+false+ "','" + false + "','" + false + "','" + false + "','" + false + "','" + true + "','"+0+"')";
                sqlCommand.ExecuteNonQuery();
                sqlCommand.CommandText = "select Bid from BranchDetails where BranchName='" + BranchName + "'";
                string BranchId = sqlCommand.ExecuteScalar().ToString();
                sqlCommand.CommandText = "insert into CustomerGroup(BranchId, BranchName, SelfHelpGroup, PeerGroup, CustId, IsLeader) values('" + BranchId + "','" + BranchName + "','" + SelfHelpGroup + "','" + PeerGroup + "','" + _customerId + "','" + IsLeader + "')";
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void UpdateExistingDetails(string BranchName,string SelfHelpGroup,string PeerGroup,Guarantor guarantor,Nominee nominee)
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
            CheckAndChangeStatus();
        }
        public void ChangeCustomerDetails(string BranchName, string SelfHelpGroup, string PeerGroup)
        {
            string AddressofCustomer = DoorNumber + "|~" + StreetName + "|~" + LocalityTown + "|~" + City + "|~" + State;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "update CustomerDetails set Name='" + CustomerName + "',Dob='" + DateofBirth.ToString("yyyy-MM-dd") + "',Age='" + Age + "',Mobile='" + ContactNumber + "',Religion='" + Religion + "',Community='" + Community + "',Education='" + Education + "',FamilyMembers='" + FamilyMembers + "',EarningMembers='" + EarningMembers + "',Occupation='" + Occupation + "',MonthlyIncome='" + MonthlyIncome + "',Address='" + AddressofCustomer + "',Pincode='" + Pincode + "',HousingType='" + HousingType + "',HousingIndex='" + HousingIndex + "' where CustId='"+_customerId+"'";
                sqlCommand.ExecuteNonQuery();
                sqlCommand.CommandText = "select Bid from BranchDetails where BranchName='" + BranchName + "'";
                string BranchId = sqlCommand.ExecuteScalar().ToString();
                sqlCommand.CommandText = "update CustomerGroup set BranchId='" + BranchId + "', SelfHelpGroup='" + SelfHelpGroup + "',BranchName='" + BranchName + "',PeerGroup='" + PeerGroup + "',IsLeader='" + IsLeader + "'";
            }
        }
        public void CheckAndChangeStatus()
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                bool _check = false;
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select IsAddressProof, IsPhotoProof, IsProfilePhoto, GuarenteeStatus, NomineeStatus from CustomerDetails where CustId='" + _customerId + "'";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while(sqlDataReader.Read())
                {
                    if( (sqlDataReader.GetBoolean(0) && sqlDataReader.GetBoolean(0) && sqlDataReader.GetBoolean(0) && sqlDataReader.GetBoolean(0) && sqlDataReader.GetBoolean(0)))
                    {
                        _check=true;
                    }
                    
                }
                sqlDataReader.Close();
                if(_check)
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
