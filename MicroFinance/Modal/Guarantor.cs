using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using MicroFinance.Validations;

namespace MicroFinance.Modal
{
    public class Guarantor:GuarantorDetailsForVerification
    {
        public string _customerId { get; set; }
        Customer customer;
        public Guarantor()
        {
            customer= new Customer();
        }
        public Guarantor(Customer cust)
        {
            customer = cust;
        }
        private string _guarantorName;
        public string GuarantorName
        {
            get
            {
                return _guarantorName;
            }
            set
            {
                _guarantorName = value;
                RaisedPropertyChanged("GuarantorName");
                GName = false;
                OverAllBasicDetailsofGuarantor = false;
            }
        }
        private DateTime _dateofBirth=DateTime.Today;
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
                GuarantorDOB = false;
                OverAllBasicDetailsofGuarantor = false;
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
                GuarantorContact = false;
                OverAllBasicDetailsofGuarantor = false;

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
                GuarantorGender = false;
                OverAllBasicDetailsofGuarantor = false;
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
                GuarantorOccupation = false;
                OverAllBasicDetailsofGuarantor = false;
            }
        }
        private string _relationship;
        public string RelationShip
        {
            get
            {
                return _relationship;
            }
            set
            {
                _relationship = value;
                GuarantorRelationship = false;
                OverAllBasicDetailsofGuarantor = false;
            }
        }
        private bool _isNominee;
        public bool IsNominee
        {
            get
            {
                return _isNominee;
            }
            set
            {
                _isNominee = value;
            }
        }
        private bool _isGuarantorNull;
        public bool IsGuarantorNull
        {
            get
            {
                return _isGuarantorNull;
            }
            set
            {
                _isGuarantorNull = value;
                RaisedPropertyChanged("IsGuarantorNull");

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
                GuarantorAddressProof = false;
                OverAllGuarantorPhotoVerification = false;
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
                GuarantorPhotoProof = false;
                OverAllGuarantorPhotoVerification = false;
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
                GuarantorProfilePicture = false;
                OverAllGuarantorPhotoVerification = false;
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
                GuarantorDoorNumber = false;
                OverAllBasicDetailsofGuarantor = false;
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
                GuarantorStreet = false;
                OverAllBasicDetailsofGuarantor = false;
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
                GuarantorLocality = false;
                OverAllBasicDetailsofGuarantor = false;
            }
        }
        private string _pincode;
        public string Pincode
        {
            get
            {
                return _pincode;
            }
            set
            {
                _pincode = value;
                GuarantorPincode = false;
                OverAllBasicDetailsofGuarantor = false;
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
                GuarantorCity = false;
                OverAllBasicDetailsofGuarantor = false;
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
                GuarantorState = false;
                OverAllBasicDetailsofGuarantor = false;
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
                OverAllBasicDetailsofGuarantor = false;
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
                OverAllBasicDetailsofGuarantor = false;
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
        public string NameofAddressProof { 
            get
            {
                return _nameofAddressProof;
            }
            set
            {
                _nameofAddressProof = value;
                RaisedPropertyChanged("NameofAddressProof");
                OverAllGuarantorPhotoVerification = false;
            }
        }
        private string _nameofPhotoProof;
        public string NameofPhotoProof {
            get
            {
                return _nameofPhotoProof;
            }
            set
            {
                _nameofPhotoProof = value;
                RaisedPropertyChanged("NameofPhotoProof");
                OverAllGuarantorPhotoVerification = false;
            }
        }

        public List<String> CheckNulls()
        {
            List<string> NullFields = new List<string>();
            if(GuarantorName.Equals(null))
            {
                NullFields.Add("Guarantor Name");
            }
            if(DateofBirth.Equals(DateTime.Today))
            {
                NullFields.Add("Date of Birth");
            }
            if(Gender.Equals(null))
            {
                NullFields.Add("Gender");
            }
            if(ContactNumber.Equals(null))
            {
                NullFields.Add("Contact Number");
            }
            if(Occupation.Equals(null))
            {
                NullFields.Add("Occupation");
            }
            if(RelationShip.Equals(null))
            {
                NullFields.Add("Relationship");
            }
            if(DoorNumber.Equals(null))
            {
                NullFields.Add("DoorNumber");
            }
            if(StreetName.Equals(null))
            {
                NullFields.Add("Street Name");
            }
            if(LocalityTown.Equals(null))
            {
                NullFields.Add("Locality");
            }
            if(Pincode.Equals(null))
            {
                NullFields.Add("Pincode");

            }
            if (City.Equals(null))
            {
                NullFields.Add("City");
            }
            if(State.Equals(null))
            {
                NullFields.Add("State");
            }
            if(AddressProof.Equals(null))
            {
                NullFields.Add("Address Proof");
            }
            if(PhotoProof.Equals(null))
            {
                NullFields.Add("Photo Proof");
            }
            if(ProfilePicture.Equals(null))
            {
                NullFields.Add("Profile Picture");
            }
            return NullFields;

        }
        public void AddGuarantorDetails()
        {
            InsertGuarantorDetails();
            if(AddressProof!=null)
            {
                AddGuarantorAddressProof();
            }
            if(PhotoProof!=null)
            {
                AddGuarantorPhotoProof();
            }
            if(ProfilePicture!=null)
            {
                AddGuarantorProfilePhoto();
            }
        }

        void InsertGuarantorDetails()
        {
            string Address = DoorNumber + "|~" + StreetName + "|~" + LocalityTown + "|~" + City + "|~" + State;
            bool _isAddressProof = false;
            bool _isPhotoProof = false;
            bool _isProfilePhoto = false;

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "insert into GuarenteeDetails (CustId,Name,Dob,Age,Mobile,Occupation,RelationShip,Address,Pincode,IsAddressProof,IsPhotoProof,IsProfilePhoto,Gender,AddressProofName,AddressProofNo,PhotoProofName,PhotoProofNo) values('" + _customerId + "','" + GuarantorName + "','" + DateofBirth.ToString("yyyy-MM-dd") + "','" + Age + "','" + ContactNumber + "','" + Occupation + "','" + RelationShip + "','" + Address + "','" + Pincode + "','" + _isAddressProof + "','" + _isPhotoProof + "','" + _isProfilePhoto + "','" + Gender + "','"+NameofAddressProof+"','"+AddressProofNo+"','"+NameofPhotoProof+"','"+PhotoProofNo+"')";

                sqlCommand.ExecuteNonQuery();
                sqlCommand.CommandText = "update CustomerDetails set GuarenteeStatus = 'True' where CustId = '" + _customerId + "'";
                sqlCommand.ExecuteNonQuery();

            }
        }
        void AddGuarantorAddressProof()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "update GuarenteeDetails set AddressProof = @addressProof, IsAddressProof = 'True',AddressProofName='"+NameofAddressProof+"' where CustId = '" + _customerId + "'";
                sqlCommand.Parameters.AddWithValue("@addressproof", Convertion(AddressProof));
                sqlCommand.ExecuteNonQuery();

            }
        }

        void AddGuarantorPhotoProof()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "update GuarenteeDetails set PhotoProof = @photoproof, IsPhotoProof = 'True',PhotoProofName='" + NameofPhotoProof + "' where CustId = '" + _customerId + "'";
                sqlCommand.Parameters.AddWithValue("@photoproof", Convertion(PhotoProof));
                sqlCommand.ExecuteNonQuery();

            }
        }

        void AddGuarantorProfilePhoto()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "update GuarenteeDetails set ProfilePhoto = @photoproof, IsProfilePhoto = 'True' where CustId = '" + _customerId + "'";
                sqlCommand.Parameters.AddWithValue("@profileproof", Convertion(ProfilePicture));
                sqlCommand.ExecuteNonQuery();

            }
        }
        public void UpdateGuarantorDetails()
        {
            string Address = DoorNumber + "|~" + StreetName + "|~" + LocalityTown + "|~" + City + "|~" + State;
            bool _isAddressProof = false;
            bool _isPhotoProof = false;
            bool _isProfilePhoto = false;
            if (AddressProof != null)
            {
                _isAddressProof = true;
            }
            if (PhotoProof != null)
            {
                _isPhotoProof = true;
            }
            if (ProfilePicture != null)
            {
                _isProfilePhoto = true;
            }
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "update GuarenteeDetails Set Name='" + GuarantorName + "',Dob='" + DateofBirth.ToString("yyyy-MM-dd") + "',Age='" + Age + "',Mobile='" + ContactNumber + "',Occupation='" + Occupation + "',RelationShip='" + RelationShip + "',Address='" + Address + "',Pincode='" + Pincode + "',IsAddressProof='" + _isAddressProof + "',IsPhotoProof='" + _isPhotoProof + "',IsProfilePhoto='" + _isProfilePhoto + "',AddressProof=@addressproof,PhotoProof=@photoproof,ProfilePhoto=@profileproof,Gender='" + Gender + "',AddressProofName='"+NameofAddressProof+"',AddressProofNo='"+AddressProofNo+"',PhotoProofName='"+NameofPhotoProof+"',PhotoProofNo='"+PhotoProofNo+"' where CustId='" + _customerId + "'";
                sqlCommand.Parameters.AddWithValue("@addressproof", Convertion(AddressProof));
                sqlCommand.Parameters.AddWithValue("@photoproof", Convertion(PhotoProof));
                sqlCommand.Parameters.AddWithValue("@profileproof", Convertion(ProfilePicture));
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void GetGuranteeDetails()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "select * from GuarenteeDetails where CustId='" + _customerId + "'";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while(sqlDataReader.Read())
                {
                    _guarantorName = sqlDataReader.GetString(1);
                    _dateofBirth = sqlDataReader.GetDateTime(2);
                    _age = sqlDataReader.GetInt32(3);
                    _contactNumber = sqlDataReader.GetString(4);
                    _occupation = sqlDataReader.GetString(5);
                    _relationship = sqlDataReader.GetString(6);
                    String[] Address = sqlDataReader.GetString(7).Split('|','~');
                    _doorNumber = Address[0];
                    _streetName = Address[2];
                    _localityTown = Address[4];
                    _city = Address[6];
                    _state = Address[8];
                    _pincode = sqlDataReader.GetInt32(8).ToString();
                    NameofAddressProof = sqlDataReader.GetString(9);
                    if (sqlDataReader.GetBoolean(11))
                    {
                        AddressProof = ByteToBI((byte[])sqlDataReader.GetValue(14));
                    }
                    NameofPhotoProof = sqlDataReader.GetString(10);
                    if (sqlDataReader.GetBoolean(12))
                    {
                        PhotoProof = ByteToBI((byte[])sqlDataReader.GetValue(15));
                    }
                    if (sqlDataReader.GetBoolean(13))
                        ProfilePicture = ByteToBI((byte[])sqlDataReader.GetValue(16));
                    IsGuarantorNull = true;
                    Gender = sqlDataReader.GetString(17);
                    AddressProofNo = sqlDataReader.GetString(18);
                    PhotoProofNo = sqlDataReader.GetString(19);
                }
            }
        }

        public void GetGuarantorVerifedDetails()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.db))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlConnection;
                command.CommandText = "select GName,GGender,GDOB,GContact,GOccupation,GRelationship,GDoorNo,GStreet,GLocality,GCity,GState,GPincode,GAddressProof,GPhotoProof,GProfilePic from CustomerVerification where CustId='" + _customerId + "'";
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    GName = dataReader.GetBoolean(0);
                    GuarantorGender = dataReader.GetBoolean(1);
                    GuarantorDOB = dataReader.GetBoolean(2);
                    GuarantorContact = dataReader.GetBoolean(3);
                    GuarantorOccupation = dataReader.GetBoolean(4);
                    GuarantorRelationship = dataReader.GetBoolean(5);
                    GuarantorDoorNumber = dataReader.GetBoolean(6);
                    GuarantorStreet = dataReader.GetBoolean(7);
                    GuarantorLocality = dataReader.GetBoolean(8);
                    GuarantorCity = dataReader.GetBoolean(9);
                    GuarantorState = dataReader.GetBoolean(10);
                    GuarantorPincode = dataReader.GetBoolean(11);
                    GuarantorAddressProof = dataReader.GetBoolean(12);
                    GuarantorPhotoProof = dataReader.GetBoolean(13);
                    GuarantorProfilePicture = dataReader.GetBoolean(14);
                }
            }
        }
    }
}
