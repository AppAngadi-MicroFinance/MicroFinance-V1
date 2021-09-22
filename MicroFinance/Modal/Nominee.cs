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
    public class Nominee:NomineeDetailsForVerification
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
                NName = false;
                OverAllBasicDetailsofNominee = false;
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
                NomineeDOB = false;
                OverAllBasicDetailsofNominee = false;
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
                NomineeContact = false;
                OverAllBasicDetailsofNominee = false;
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
                NomineeOccupation = false;
                OverAllBasicDetailsofNominee = false;
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
                NomineeGender = false;
                OverAllBasicDetailsofNominee = false;
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
                NomineeRelationship = false;
                OverAllBasicDetailsofNominee = false;
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
                NomineeAddressProof = false;
                OverAllNomineePhotoVerification = false;
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
                NomineePhotoProof = false;
                OverAllNomineePhotoVerification = false;
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
                NomineeProfilePicture = false;
                OverAllNomineePhotoVerification = false;
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
                NomineeDoorNo = false;
                OverAllBasicDetailsofNominee = false;
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
                NomineeStreet = false;
                OverAllBasicDetailsofNominee = false;
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
                NomineeLocality = false;
                OverAllBasicDetailsofNominee = false;
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
                NomineePincode = false;
                OverAllBasicDetailsofNominee = false;
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
                NomineeCity = false;
                OverAllBasicDetailsofNominee = false;
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
                NomineeState = false;
                OverAllBasicDetailsofNominee = false;
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
                RaisedPropertyChanged("NameofAddressProof");
                OverAllNomineePhotoVerification = false;
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
                RaisedPropertyChanged("NameofPhotoProof");
                OverAllNomineePhotoVerification = false;
            }
        }

        public void AddNomineeDetails()
        {
            InsertGuarantorDetails();
            if (AddressProof != null)
            {
                AddGuarantorAddressProof();
            }
            if (PhotoProof != null)
            {
                AddGuarantorPhotoProof();
            }
            if (ProfilePicture != null)
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

            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "insert into NomineeDetails (CustId,Name,Dob,Age,Mobile,Occupation,RelationShip,Address,Pincode,IsAddressProof,IsPhotoProof,IsProfilePhoto,Gender) values('" + _customerId + "','" + NomineeName + "','" + DateofBirth.ToString("yyyy-MM-dd") + "','" + Age + "','" + ContactNumber + "','" + Occupation + "','" + RelationShip + "','" + Address + "','" + Pincode + "','" + _isAddressProof + "','" + _isPhotoProof + "','" + _isProfilePhoto + "','" + Gender + "')";

                sqlCommand.ExecuteNonQuery();
                sqlCommand.CommandText = "update CustomerDetails set GuarenteeStatus = 'True' where CustId = '" + _customerId + "'";
                sqlCommand.ExecuteNonQuery();

            }
        }
        void AddGuarantorAddressProof()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "update NomineeDetails set AddressProof = @addressProof, IsAddressProof = 'True',AddressProofName='" + NameofAddressProof + "' where CustId = '" + _customerId + "'";
                sqlCommand.Parameters.AddWithValue("@addressproof", Convertion(AddressProof));
                sqlCommand.ExecuteNonQuery();

            }
        }



        void AddGuarantorAddressProofToDrive()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "update NomineeDetails set IsAddressProof = 'True',AddressProofName='" + NameofAddressProof + "' where CustId = '" + _customerId + "'";
                sqlCommand.ExecuteNonQuery();
                byte[] data= Convertion(AddressProof);
                string FolderPath = MainWindow.DriveBasePath + "\\" + "Nominee\\" + MainWindow.LoginDesignation.BranchName + "\\" + NameofAddressProof;
                SaveImageToDrive.SaveImage(FolderPath, _customerId, data);
               

            }
        }

        void AddGuarantorPhotoProof()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "update NomineeDetails set PhotoProof = @photoproof, IsPhotoProof = 'True',PhotoProofName='" + NameofPhotoProof + "' where CustId = '" + _customerId + "'";
                sqlCommand.Parameters.AddWithValue("@photoproof", Convertion(PhotoProof));
                sqlCommand.ExecuteNonQuery();

            }
        }

        void AddGuarantorPhotoProofToDrive()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "update NomineeDetails set IsPhotoProof = 'True',PhotoProofName='" + NameofPhotoProof + "' where CustId = '" + _customerId + "'";
                sqlCommand.ExecuteNonQuery();
                byte[] data= Convertion(PhotoProof);
                string FolderPath = MainWindow.DriveBasePath + "\\" + "Nominee\\" + MainWindow.LoginDesignation.BranchName + "\\" + NameofPhotoProof;
                SaveImageToDrive.SaveImage(FolderPath, _customerId, data);
            }
        }

        void AddGuarantorProfilePhoto()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "update NomineeDetails set ProfilePhoto = @profileproof, IsProfilePhoto = 'True' where CustId = '" + _customerId + "'";
                sqlCommand.Parameters.AddWithValue("@profileproof", Convertion(ProfilePicture));
                sqlCommand.ExecuteNonQuery();

            }
        }

        void AddGuarantorProfilePhotoToDrive()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "update NomineeDetails set IsProfilePhoto = 'True' where CustId = '" + _customerId + "'";
                sqlCommand.ExecuteNonQuery();
                byte[] data= Convertion(ProfilePicture);
                string FolderPath = MainWindow.DriveBasePath + "\\" + "Nominee\\" + MainWindow.LoginDesignation.BranchName + "\\" + "Profile Photo";
                SaveImageToDrive.SaveImage(FolderPath, _customerId, data);


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
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                connection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = connection;
                sqlCommand.CommandText = "update NomineeDetails set Name='" + NomineeName + "',Dob='" + DateofBirth.ToString("yyyy-MM-dd") + "',Age='" + Age + "',Mobile='" + ContactNumber + "',Occupation='" + Occupation + "',RelationShip='" + RelationShip + "',Address='" + Address + "',Pincode='" + Pincode + "',Gender='"+Gender+"' where CustId='"+_customerId+"'";
                sqlCommand.ExecuteNonQuery();
            }
            if (AddressProof != null)
            {
                //AddGuarantorAddressProof();
                AddGuarantorAddressProofToDrive();
            }
            if (PhotoProof != null)
            {
                //AddGuarantorPhotoProof();
                AddGuarantorPhotoProofToDrive();
            }
            if (ProfilePicture != null)
            {
                //AddGuarantorProfilePhoto();
                AddGuarantorProfilePhotoToDrive();
            }

        }

        public void GetNomineeDetails()
        {
            using (SqlConnection connection = new SqlConnection(Properties.Settings.Default.DBConnection))
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
                    _pincode = sqlDataReader.GetInt32(8).ToString();
                    if (sqlDataReader.GetBoolean(11))
                    {
                        NameofAddressProof = sqlDataReader.GetString(9);
                        //AddressProof = ByteToBI((byte[])sqlDataReader.GetValue(14));
                        string FolderPath = MainWindow.DriveBasePath + "\\" + "Nominee\\" + MainWindow.LoginDesignation.BranchName + "\\" + NameofAddressProof;
                        SaveImageToDrive.GetImage(FolderPath, _customerId);
                    }
                    if (sqlDataReader.GetBoolean(12))
                    {
                        NameofPhotoProof = sqlDataReader.GetString(10);
                        //PhotoProof = ByteToBI((byte[])sqlDataReader.GetValue(15));
                        string FolderPath = MainWindow.DriveBasePath + "\\" + "Nominee\\" + MainWindow.LoginDesignation.BranchName + "\\" + NameofPhotoProof;
                        SaveImageToDrive.GetImage(FolderPath, _customerId);
                    }
                    if (sqlDataReader.GetBoolean(13))
                    {
                        //ProfilePicture = ByteToBI((byte[])sqlDataReader.GetValue(16));
                        string FolderPath = MainWindow.DriveBasePath + "\\" + "Nominee\\" + MainWindow.LoginDesignation.BranchName + "\\" + "Profile Photo";
                        SaveImageToDrive.GetImage(FolderPath, _customerId);
                    }
                       
                    IsNomineeNull = true;
                    Gender = sqlDataReader.GetString(17);
                }
            }
        }

        public void GetNomineeVerifiedDetails()
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sqlConnection;
                command.CommandText = "select NName,NGender,NDOB,NContact,NOccupation,NRelationship,NDoorNo,NStreet,NLocality,NCity,NState,NPincode,NAddressProof,NPhotoProof,NProfilePic from CustomerVerification where CustId='" + _customerId + "'";
                SqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    NName = dataReader.GetBoolean(0);
                    NomineeGender = dataReader.GetBoolean(1);
                    NomineeDOB = dataReader.GetBoolean(2);
                    NomineeContact = dataReader.GetBoolean(3);
                    NomineeOccupation = dataReader.GetBoolean(4);
                    NomineeRelationship = dataReader.GetBoolean(5);
                    NomineeDoorNo = dataReader.GetBoolean(6);
                    NomineeStreet = dataReader.GetBoolean(7);
                    NomineeLocality = dataReader.GetBoolean(8);
                    NomineeCity = dataReader.GetBoolean(9);
                    NomineeState = dataReader.GetBoolean(10);
                    NomineePincode = dataReader.GetBoolean(11);
                    NomineeAddressProof = dataReader.GetBoolean(12);
                    NomineePhotoProof = dataReader.GetBoolean(13);
                    NomineeProfilePicture = dataReader.GetBoolean(14);
                }
            }
        }
    }
}
