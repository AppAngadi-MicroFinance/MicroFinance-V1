using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace MicroFinance.Modal
{
    public class Employee : BindableBase
    {
        public List<Employee> EmployeeList = new List<Employee>();
        string Connectionstring = MicroFinance.Properties.Settings.Default.DBConnection;
        public Employee()
        {
           
        }
        private string _branchname;
        public string BranchName
        {
            get
            {
                return _branchname;
            }
            set
            {
                if (value != _branchname)
                {
                    _branchname = value;
                    RaisedPropertyChanged("BranchName");
                }
            }
        }
        private string _employeeID;
        public string EmployeeID
        {
            get
            {
                return _employeeID;
            }
            set
            {
                _employeeID = value;
                RaisedPropertyChanged("EmployeeID");
            }
        }
        private string _branchID;
        public string BranchID
        {
            get
            {
                return _branchID;
            }
            set
            {
                _branchID = value;
                RaisedPropertyChanged("BranchID");
            }
        }


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
        private string _designation;
        public string Designation
        {
            get
            {
                return _designation;
            }
            set
            {
                   _designation = value;
                   RaisedPropertyChanged("Designation");
            }
        }
        private string _employeename;
        public string EmployeeName
        {
            get
            {
                return _employeename;
            }
            set
            {
               
               _employeename = value;
               RaisedPropertyChanged("EmployeeName");
               
            }
           
        }
        private DateTime _dob=DateTime.Today;
        public DateTime DOB
        {
            get
            {
                return _dob;
            }
            set
            {
                    _dob = value;
                    Age = CalculateAge(value);
                    RaisedPropertyChanged("DOB");
            }
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
                RaisedPropertyChanged("Age");
            }
        }
        private string _contactnumber;
        public string ContactNumber
        {
            get
            {
                return _contactnumber;
            }
            set
            {
                if(value!=_contactnumber)
                {
                   
                   _contactnumber = value;
                   RaisedPropertyChanged("ContactNumber");
                }
            }
        }
        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                if(value!=_email)
                {
                  _email = value;
                   RaisedPropertyChanged("Email");
                }
            }
        }
        private string _houseno;
        public string HouseNo
        {
            get
            {
                return _houseno;
            }
            set
            {
                _houseno = value;
                RaisedPropertyChanged("HouseNo");
            }
        }
        private string _townname;
        public string TownName
        {
            get
            {
                return _townname;
            }
            set
            {
                _townname = value;
                RaisedPropertyChanged("TownName");
            }
        }
        private string _district;
        public string District
        {
            get
            {
                return _district;
            }
            set
            {
                _district = value;
                RaisedPropertyChanged("District");
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
                RaisedPropertyChanged("Pincode");
            }
        }
        //
        private string _fathername;
        public string FatherName
        {
            get
            {
                return _fathername;
            }
            set
            {
                _fathername = value;
                RaisedPropertyChanged("FatherName");
            }
        }
        private string _aadharnumber;
        public string AadharNumber
        {
            get
            {
                return _aadharnumber;
            }
            set
            {
                if(value!=_aadharnumber)
                {
                    _aadharnumber = value;
                    RaisedPropertyChanged("Aadharnumber");
                }
            }
        }
        private string _pannumber;
        public string PanNumber
        {
            get
            {
                return _pannumber;
            }
            set
            {
                _pannumber = value.ToUpper();
                RaisedPropertyChanged("PanNumber");
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
                RaisedPropertyChanged("Religion");
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
                RaisedPropertyChanged("Community");
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
                RaisedPropertyChanged("Caste");
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
                RaisedPropertyChanged("Education");
            }
        }
        private DateTime _dateofjoining=DateTime.Now;
        public DateTime DateOfJoining
        {
            get
            {
                return _dateofjoining.Date;
            }
            set
            {
                _dateofjoining = value;
                RaisedPropertyChanged("DateOfJoining");
            }
        }
        private string _addressproofName;
        public string AddressProofName
        {
            get
            {
                return _addressproofName;
            }
            set
            {
                _addressproofName = value;
                RaisedPropertyChanged("AddressProofName");
            }
        }
        private BitmapImage _addressproofimage;
        public BitmapImage AddressProofImage
        {
            get
            {
                return _addressproofimage;
            }
            set
            {
                _addressproofimage = value;
                RaisedPropertyChanged("AddressProofImage");
            }
        }

        private string _photoproofname;
        public string PhotoProofName
        {
            get
            {
                return _photoproofname;
            }
            set
            {
                _photoproofname = value;
                RaisedPropertyChanged("PhotoProofName");
            }
        }
        private BitmapImage _photoproofimage;
        public BitmapImage PhotoProofImage
        {
            get
            {
                return _photoproofimage;
            }
            set
            {
                _photoproofimage = value;
                RaisedPropertyChanged("PhotoProofImage");
            }
        }
        private BitmapImage _profileimage;
        public BitmapImage ProfileImage
        {
            get
            {
                return _profileimage;
            }
            set
            {
                _profileimage = value;
                RaisedPropertyChanged("ProfileImage");
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
        private string _accountholdername;
        public string AccountHolderName
        {
            get
            {
                return _accountholdername;
            }
            set
            {
                
               _accountholdername = value;
              RaisedPropertyChanged("AccountHolderName");
                 
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
              RaisedPropertyChanged("Account Number");
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
        private bool _isphotoproof;
        public bool IsPhotoProof
        {
            get
            {
                return _isphotoproof;
            }
            set
            {
                _isphotoproof = value;
                RaisedPropertyChanged("IsPhotoProof");
            }
        }
        private bool _isaddressproof=false;
        public bool IsAddressProof
        {
            get
            {
                return _isaddressproof;
            }
            set
            {
                _isaddressproof = value;
                RaisedPropertyChanged("IsAddressProof");
            }
        }
        private bool _isprofilepicture;
        public bool IsProfilePicture
        {
            get
            {
                return _isprofilepicture;
            }
            set
            {
                _isprofilepicture = value;
                RaisedPropertyChanged("IsProfilePicture");
            }
        }
        private bool _isactive=true;
        public bool IsActive
        {
            get
            {
                return _isactive;
            }
            set
            {
                _isactive = value;
            }
        }

        public void EmployeeAdd()
        {
            if(!IsExists())
            {
                using (SqlConnection sqlConn = new SqlConnection(Connectionstring))
                {
                    string address = _houseno+"\t" + _townname;
                    string ID = GenerateEmployeeID();
                    sqlConn.Open();
                    SqlCommand Sqlcomm = new SqlCommand();
                    Sqlcomm.Connection = sqlConn;
                    Sqlcomm.CommandText = "insert into Employee(EmpId,Name,DOB,age,MobileNo,Region,EmailId,Education,AadhaarNo,DateOfJoin,BankName,BranchName,AccountNumber,IFSCCode,MICRCode,Address,PinCode,District,IsAddressProof,AddressProofName,AddressProof,IsPhotoProof,PhotoProofName,PhotoProof,IsProfilePhoto,ProfilePhoto,IsActive,Designation,Bid,FatherName,PanNumber,KYCNumber,Community,Caste)values('" +ID + "','" + EmployeeName + "','" + _dob.ToString("MM/dd/yyyy") + "','" + Age + "','" + _contactnumber + "','" + _religion + "','" + _email + "','" + _education + "','" + _aadharnumber + "','" + DateOfJoining.ToString("MM/dd/yyyy") + "','" + _bankname +"','" + BankBranchName + "','" + _accountnumber + "','" + _ifsccode + "','" + _micrcode + "','" + address + "','" + _pincode + "','" + _district + "',"+ IsTrue(_isaddressproof) +",'" + _addressproofName + "',@addressproof,"+IsTrue(_isphotoproof)+",'" + _photoproofname + "',@photoproof,"+IsTrue(_isprofilepicture)+",@profilepicture,"+IsTrue(_isactive)+",'" + _designation + "','" + GetBranchID() + "','"+_fathername+"','"+_pannumber+"','"+ID+"','"+_community+"','"+_caste+"')";
                    Sqlcomm.Parameters.AddWithValue("@addressproof", Convertion(_addressproofimage));
                    Sqlcomm.Parameters.AddWithValue("@photoproof", Convertion(_photoproofimage));
                    Sqlcomm.Parameters.AddWithValue("@profilepicture", Convertion(_profileimage));
                    Sqlcomm.ExecuteNonQuery();
                    sqlConn.Close();
                }
            }
            else
            {
                using (SqlConnection sqlConn = new SqlConnection(Connectionstring))
                {
                    string address = _houseno + "\t" + _townname;
                    sqlConn.Open();
                    SqlCommand Sqlcomm = new SqlCommand();
                    Sqlcomm.Connection = sqlConn;
                    Sqlcomm.CommandText = "update Employee set Name = '"+EmployeeName+"',DOB = '"+_dob.ToString("MM/dd/yyyy")+"',age = '"+_age+"',MobileNo = '"+_contactnumber+"',Region = '"+_religion+"',EmailId = '"+_email+"',Education = '"+_education+"',AadhaarNo = '"+_aadharnumber+"',DateOfJoin = '"+_dateofjoining.ToString("MM/dd/yyyy")+"',BankName = '"+_bankname+"',BranchName = '"+_bankbranchname+"',AccountNumber = '"+_accountnumber+"',IFSCCode = '"+_ifsccode+"',MICRCode = '"+_micrcode+"',Address = '"+address+"',PinCode = '"+_pincode+"',District = '"+District+"',IsAddressProof = '"+IsTrue(_isaddressproof)+"',AddressProofName = '"+_addressproofName+ "',AddressProof = @addressproof,IsPhotoProof = '"+IsTrue(_isphotoproof)+"',PhotoProofName = '"+_photoproofname+ "',PhotoProof = photoproof,IsProfilePhoto = '"+IsTrue(_isprofilepicture)+ "',ProfilePhoto =@profilepicture,IsActive ="+IsTrue(_isactive)+",Designation = '"+_designation+"',Bid = '"+BranchID+ "', FatherName='" + FatherName + "',PanNumber='" + _pannumber + "',Community='"+_community+"',Caste='"+_caste+"' where Name = '" + EmployeeName + "' and AadhaarNo = '" + _aadharnumber + "'";
                    Sqlcomm.Parameters.AddWithValue("@addressproof", Convertion(_addressproofimage));
                    Sqlcomm.Parameters.AddWithValue("@photoproof", Convertion(_photoproofimage));
                    Sqlcomm.Parameters.AddWithValue("@profilepicture", Convertion(_profileimage));
                    Sqlcomm.ExecuteNonQuery();
                    sqlConn.Close();
                }
            }
        }
        public string GetBranchID()
        {
            string ID = "";
            using(SqlConnection sqlconn=new SqlConnection(Connectionstring))
            {
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "(select Bid from BranchDetails where BranchName='"+_branchname+"')";
                    ID = (string)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
            }
            return ID;
        }
        public int IsTrue(bool value)
        {
            if(value==true)
            {
                return 1;
            }
            return 0;
        }

        public bool IsByte(int value)
        {
            if(value==0)
            {
                return false;
            }
            return true;
        }

        public string GetRegionNumber()
        {
            string Result = "";
            using (SqlConnection sqlconn = new SqlConnection(Connectionstring))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select SNo from Region where RegionName='" + Region + "'";
                    Result = (string)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
                return Result;
            }
        }
        public string GetBranchNumber()
        {
            string Result = "";
            using (SqlConnection sqlconn = new SqlConnection(Connectionstring))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select SNo from BranchDetails where BranchName='" + BranchName + "'";
                    Result = (string)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
                return Result;
            }
        }
        public string GenerateEmployeeID() // IDPattern 0100220210605 (01-Region+002-BranchName/2021-CurrentYear/06-CurrentMonth/05-(CountOfCustomers+1))
        {
            int count = 1;
            string Result = "";
            int year = DateTime.Now.Year;
            int mon = DateTime.Now.Month;
            string month = ((mon) < 10 ? "0" + mon : mon.ToString());
            using (SqlConnection sqlcon = new SqlConnection(Connectionstring))
            {
                sqlcon.Open();
                if (sqlcon.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlcon;
                    sqlcomm.CommandText = "Select Count(EmpId) from Employee";
                    count += (int)sqlcomm.ExecuteScalar();
                }
                sqlcon.Close();
            }
            string region = DigitConvert(GetRegionNumber(), 2);
            string branch = DigitConvert(GetBranchNumber());
            Result = region + branch + year + month + ((count < 10) ? "0" + count : count.ToString());
            return Result;
        }
        public string DigitConvert(string digit, int place = 3)
        {
            StringBuilder sb = new StringBuilder();
            string number = digit;
            string Result = "";
            if (number.Length < place)
            {
                for (int i = 0; i < (place - (number.Length)); i++)
                {
                    sb.Append(0);
                }
                Result = sb.ToString() + number;
            }
            else
            {
                Result = number;
            }

            return Result;
        }
        public bool IsExists()
        {
           using(SqlConnection sqlconn=new SqlConnection(Connectionstring))
            {
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select Name,AadhaarNo from Employee where Name='" + _employeename+"' and AadhaarNo='"+_aadharnumber+"'";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            string Nameresult = reader.GetString(0);
                            string Aadharresult = reader.GetString(1);
                            if (Nameresult.Equals(_employeename,StringComparison.CurrentCultureIgnoreCase) &&Aadharresult.Equals(_aadharnumber,StringComparison.CurrentCultureIgnoreCase))
                            {
                                return true;
                            }
                        }
                    }
                    reader.Close();
                }
                sqlconn.Close();
            }
            return false;
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
       

        public void GetEmployeeDetails(string EmpID)
        {
            using (SqlConnection sqlconn=new SqlConnection(Connectionstring))
            {
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select EmpId,Name,DOB,Age,MobileNo,Region,EmailId,Education,AadhaarNo,DateOfJoin,BankName,BranchName,AccountNumber,IFSCCode,MICRCode,Address,PinCode,District,IsAddressProof,AddressProofName,AddressProof,IsPhotoProof,PhotoProofName,PhotoProof,IsProfilePhoto,ProfilePhoto,IsActive,Designation,Bid,FatherName,PanNumber from Employee where EmpId='"+EmpID+"'";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            string addressarr = reader.GetString(15);
                            string[] address = addressarr.Split('\t').ToArray();

                            _employeeID = reader.GetString(0);
                            _employeename = reader.GetString(1);
                            _dob = reader.GetDateTime(2);
                            _age = reader.GetInt32(3);
                            _contactnumber = reader.GetString(4);
                            _religion = reader.GetString(5);
                            _email = reader.GetString(6);
                            _education = reader.GetString(7);
                            _aadharnumber = reader.GetString(8);
                            _dateofjoining = reader.GetDateTime(9);
                            _bankname = reader.GetString(10);
                            _bankbranchname = reader.GetString(11);
                            _accountnumber = reader.GetString(12);
                            _ifsccode = reader.GetString(13);
                            _micrcode = reader.GetString(14);
                            _houseno = address[0].ToString();
                            _townname = address[1].ToString();
                            _pincode = reader.GetString(16);
                            _district = reader.GetString(17);
                            _isaddressproof = reader.GetBoolean(18);
                            _addressproofName = (reader.GetBoolean(18) ? reader.GetString(19) : "");
                            _addressproofimage = (reader.GetBoolean(18) ? ByteToBI((byte[])reader.GetValue(20)) : null);
                            _isphotoproof = reader.GetBoolean(21);
                            _photoproofname = (reader.GetBoolean(21) ? reader.GetString(22) : "");
                            _photoproofimage = (reader.GetBoolean(21) ? ByteToBI((byte[])reader.GetValue(23)) : null);
                            _isprofilepicture = reader.GetBoolean(24);
                            _profileimage = (reader.GetBoolean(24) ? ByteToBI((byte[])reader.GetValue(25)) : null);
                            _isactive = reader.GetBoolean(26);
                            _designation = reader.GetString(27);
                            _branchID = reader.GetString(28);
                            _fathername = reader.GetString(29);
                            _pannumber = reader.GetString(30);
                            
                        }
                        reader.Close();
                    }
                    sqlconn.Close();
                }
            }
        }
        public int CalculateAge(DateTime date)
        {
            int age = 0;
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            age = year - date.Year;
            if(date.Month>month)
            {
                age -= 1;
            }
            return age;
        }
    }
}
