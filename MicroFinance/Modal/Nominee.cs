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
    public class Nominee:BindableBase
    {
        public string _customerId { get; set; }

        private string _nomineeName;
        public string NomineeName
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
            }
        }
        private bool _isNomineeNull;
        public bool IsNomineeNull
        {
            get
            {
                return _isNomineeNull;
            }
            set
            {
                _isNomineeNull = value;
                RaisedPropertyChanged("IsNomineeNull");
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

        public void AddNomineeDetails()
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
                sqlCommand.CommandText = "insert into NomineeDetails (CustId,Name,Dob,Age,Mobile,Occupation,RelationShip,Address,Pincode,AddressProofName,PhotoProofName,IsAddressProof,IsPhotoProof,IsProfilePhoto,AddressProof,PhotoProof,ProfilePhoto) values('" + _customerId + "','" + NomineeName + "','" + DateofBirth + "','" + Age + "','" + ContactNumber + "','" + Occupation + "','" + RelationShip + "','" + Address + "','" + Pincode + "','" + NameofAddressProof + "','" + NameofPhotoProof + "','" + _isAddressProof + "','" + _isPhotoProof + "','" + _isProfilePhoto + "',@addressproof,@photoproof,@profileproof)";
                sqlCommand.Parameters.AddWithValue("@addressproof", Convertion(AddressProof));
                sqlCommand.Parameters.AddWithValue("@photoproof", Convertion(PhotoProof));
                sqlCommand.Parameters.AddWithValue("@profileproof", Convertion(ProfilePicture));
                sqlCommand.ExecuteNonQuery();
                sqlCommand.CommandText = "update CustomerDetails set NomineeStatus = 'True' where CustId = '" + _customerId + "'";
                sqlCommand.ExecuteNonQuery();

            }
        }

        public void UpdateNomineeDetails()
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
                sqlCommand.CommandText = "update NomineeDetails set Name='" + NomineeName + "',Dob='" + DateofBirth + "',Age='" + Age + "',Mobile='" + ContactNumber + "',Occupation='" + Occupation + "',RelationShip='" + RelationShip + "',Address='" + Address + "',Pincode='" + Pincode + "',AddressProofName='" + NameofAddressProof + "',PhotoProofName='" + NameofPhotoProof + "',IsAddressProof='" + _isAddressProof + "',IsPhotoProof='" + _isPhotoProof + "',IsProfilePhoto='" + _isProfilePhoto + "',AddressProof=@addressproof,PhotoProof=@photoproof,ProfilePhoto=@profileproof where CustId='"+_customerId+"'";
                sqlCommand.Parameters.AddWithValue("@addressproof", Convertion(AddressProof));
                sqlCommand.Parameters.AddWithValue("@photoproof", Convertion(PhotoProof));
                sqlCommand.Parameters.AddWithValue("@profileproof", Convertion(ProfilePicture));
                sqlCommand.ExecuteNonQuery();
            }
        }

        public void GetNomineeDetails()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "select * from NomineeDetails where CustId='" + _customerId + "'";
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                while (sqlDataReader.Read())
                {
                    _nomineeName = sqlDataReader.GetString(1);
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
                    _pincode = sqlDataReader.GetInt32(8);
                    if (sqlDataReader.GetBoolean(11))
                    {
                        NameofAddressProof = sqlDataReader.GetString(9);
                        AddressProof = ByteToBI((byte[])sqlDataReader.GetValue(14));
                    }
                    if (sqlDataReader.GetBoolean(12))
                    {
                        NameofPhotoProof = sqlDataReader.GetString(10);
                        PhotoProof = ByteToBI((byte[])sqlDataReader.GetValue(15));
                    }
                    if (sqlDataReader.GetBoolean(13))
                        ProfilePicture = ByteToBI((byte[])sqlDataReader.GetValue(16));
                    IsNomineeNull = true;
                }
            }
        }
    }
}
