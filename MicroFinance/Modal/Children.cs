using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
   public class Children : BindableBase
    {
        public string _customerId { get; set; }
        private List<string> _addressProofNames = new List<string> { "Family Card", "Aadhar Card", "Voter Id" ,"Pan Card","Driving Licence","Passport", "Others" };

        public List<string> AddressProofName
        {
            get
            {
                return _addressProofNames;
            }
        }
       
        
        public string CustomerId { get; set; }
        private string _c1name;
        public string C1Name
        {
            get
            {
                return _c1name;
            }
            set
            {
                _c1name = value;
            }
        }
        private DateTime _c1dob = DateTime.Now;
       // private string _c1dob ="";
        public DateTime C1Dob
        {
            get
            {
                return _c1dob; 
            }
            set
            {
                _c1dob = value;
                C1Age = AgeCalculator(Convert.ToDateTime(_c1dob));
                RaisedPropertyChanged("C1Dob");
                //CustDateOfBirth = false;
                //CustDetailsOverAll = false;
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
        private int _c1age;
        public int C1Age
        {
            get
            {
                return _c1age;
            }
            set
            {
                _c1age = value;
            }
        }
        private string _c1gender;
        public string C1Gender
        {
            get
            {
                return _c1gender;
            }
            set
            {
                _c1gender = value;
                RaisedPropertyChanged("C1Gender");
                //CustGender = false;
                //CustDetailsOverAll = false;
            }
        }
        private string _c1contactNumber;
        public string C1ContactNumber
        {
            get
            {
                return _c1contactNumber;
            }
            set
            {
                _c1contactNumber = value;
                RaisedPropertyChanged("C1ContactBox");
                //CustContactNumber = false;
                //CustDetailsOverAll = false;
            }
        }

        private string _c1idprooftype;
        public string C1IdProofType
        {
            get
            {
                return _c1idprooftype;
            }
            set
            {
                _c1idprooftype = value;
            }
        }
        private string _c1idproofno;
        public string C1IdProofNo
        {
            get
            {
                return _c1idproofno;
            }
            set
            {
                _c1idproofno = value;
            }
        }

        private string _c1addressprooftype;
        public string C1AddressProofType
        {
            get
            {
                return _c1addressprooftype;
            }
            set
            {
                _c1addressprooftype = value;
                RaisedPropertyChanged("C1AddressProofType");
            }
        }
        private string _c1addressproofno;
        public string C1AddressProofNo
        {
            get
            {
               return  _c1addressproofno;
            }
            set
            {
                _c1addressproofno = value;
            }
        }
        private int _c1monthlyincome;
        public int C1MonthlyIncome
        {
            get
            {
                return _c1monthlyincome;
            }
            set
            {
                _c1monthlyincome = value;
                RaisedPropertyChanged("C1MonthlyIncome");
            }
        }
        private string _c2name;
        public string C2Name
        {
            get
            {
                return _c2name;
            }
            set
            {
                _c2name = value;
                RaisedPropertyChanged("C2Name");

            }
        }
        private DateTime _c2dob = DateTime.Now;
       // private string _c2dob = "";
        public DateTime C2Dob
        {
            get
            {
                return _c2dob;
            }
            set
            {
                _c2dob = value;
                C2Age = AgeCalculator(Convert.ToDateTime(_c2dob));
                RaisedPropertyChanged("C2Dob");
                
                //CustDateOfBirth = false;
                //CustDetailsOverAll = false;
            }
        }
       
        private int _c2age;
        public int C2Age
        {
            get
            {
                return _c2age;
            }
            set
            {
                _c2age = value;
               
            }
        }
        private string _c2gender;
        public string C2Gender
        {
            get
            {
                return _c2gender;
            }
            set
            {
                _c2gender = value;
                RaisedPropertyChanged("C2Gender");
                //CustGender = false;
                //CustDetailsOverAll = false;
            }
        }
        private string _c2contactNumber;
        public string C2ContactNumber
        {
            get
            {
                return _c2contactNumber;
            }
            set
            {
                _c2contactNumber = value;
                RaisedPropertyChanged("C2ContactBox");
                //CustContactNumber = false;
                //CustDetailsOverAll = false;
            }
        }
       

        private string _c2idprooftype;
        public string C2IdProofType
        {
            get
            {
                return _c2idprooftype;
            }
            set
            {
                _c2idprooftype = value;
                RaisedPropertyChanged("C2IdProofType");

            }
        }
        private string _c2idproofno;
        public string C2IdProofNo
        {
            get
            {
                return _c2idproofno;
            }
            set
            {
                _c2idproofno = value;
            }
        }

        private string _c2addressprooftype;
        public string C2AddressProofType
        {
            get
            {
                return _c2addressprooftype;
            }
            set
            {
                _c2addressprooftype = value;
                RaisedPropertyChanged("C2AddressProofType");
            }
        }
        private string _c2addressproofno;
        public string C2AddressProofNo
        {
            get
            {
                return _c2addressproofno;
            }
            set
            {
                _c2addressproofno = value;
            }
        }
        private int _c2monthlyincome;
        public int C2MonthlyIncome
        {
            get
            {
                return _c2monthlyincome;
            }
            set
            {
                _c2monthlyincome = value;
                RaisedPropertyChanged("C2MonthlyIncome");
            }
        }

        private bool _ischildrennull;
        public bool IsChildrenNull
        {
            get
            {
                return _ischildrennull;
            }
            set
            {
                _ischildrennull = value;
                RaisedPropertyChanged("IsChildrenNull");
            }
        }

        private string _nameofAddressProofc1;
        public string NameofAddressProofC1
        {
            get
            {
                return _nameofAddressProofc1;
            }
            set
            {
                _nameofAddressProofc1 = value;
                RaisedPropertyChanged("AddressProofName");

            }
        }
        private string _nameofPhotoProofc1;
        public string NameofPhotoProofC1
        {
            get
            {
                return _nameofPhotoProofc1;
            }
            set
            {
                _nameofPhotoProofc1 = value;
                RaisedPropertyChanged("AddressProofName");
            }
        }
        private string _nameofAddressProofc2;
        public string NameofAddressProofC2
        {
            get
            {
                return _nameofAddressProofc2;
            }
            set
            {
                _nameofAddressProofc2 = value;
                RaisedPropertyChanged("AddressProofName");

            }
        }
        private string _nameofPhotoProofc2;
        public string NameofPhotoProofC2
        {
            get
            {
                return _nameofPhotoProofc2;
            }
            set
            {
                _nameofPhotoProofc2 = value;
                RaisedPropertyChanged("AddressProofName");
            }
        }

        public bool IsChildDetailExist(string custid)
        {
            bool isexist = false;
            using(SqlConnection con= new SqlConnection(Properties.Settings.Default.db))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select count(*) from ChildDetails where custid='" + custid + "'";
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                isexist = count == 0 ? false : true;
            }
            return isexist;
        }

        public void UpdateChildDetails(Children child)
        {
            using(SqlConnection con= new SqlConnection(Properties.Settings.Default.db))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                if(child._customerId!=null)
                {
                    cmd.CommandText = "update ChildDetails set C1Name=@C1Name,C1Mobile=@C1Mobile,C1DOB=@C1DOB,C1Age=@C1Age,C1IdProofType=@C1IdProofType,C1IdProofNo=@C1IdProofNo,C1AddressProoftype=@C1AddressProoftype,C1AddressProofNo=@C1AddressProofNo,C1MonthlyIncome=@C1MonthlyIncome,C2Name=@C2Name,C2Mobile=@C2Mobile,C2DOB=@C2DOB,C2Age=@C2Age,C2IdProofType=@C2IdProofType,C2IdProofNo=@C2IdProofNo,C2MonthlyIncome=@C2MonthlyIncome,C1Gender=@C1Gender,C2Gender=@C2Gender where custid='" + child._customerId + "'";
                    cmd.Parameters.AddWithValue("@C1Name", child.C1Name==null?"":child.C1Name);
                    cmd.Parameters.AddWithValue("@C1Mobile", child.C1ContactNumber==null?"":child.C1ContactNumber);
                    cmd.Parameters.AddWithValue("@C1DOB", child.C1Dob == null ? "" : child.C1Dob.ToString());
                    cmd.Parameters.AddWithValue("@C1Age", child.C1Age == null ? "" : child.C1Age.ToString());
                    cmd.Parameters.AddWithValue("@C1IdProofType", child.C1IdProofType == null ? "" : child.C1IdProofType);
                    cmd.Parameters.AddWithValue("@C1IdProofNo", child.C1IdProofNo == null ? "" : child.C1IdProofNo);
                    cmd.Parameters.AddWithValue("@C1AddressProoftype", child.C1AddressProofType == null ? "" : child.C1AddressProofType);
                    cmd.Parameters.AddWithValue("@C1AddressProofNo", child.C1AddressProofNo == null ? "" : child.C1AddressProofNo);
                    cmd.Parameters.AddWithValue("@C1MonthlyIncome", child.C1MonthlyIncome == null ? 0 : child.C1MonthlyIncome);
                    cmd.Parameters.AddWithValue("@C2Name", child.C2Name == null ? "" : child.C2Name);
                    cmd.Parameters.AddWithValue("@C2Mobile", child.C2ContactNumber == null ? "" : child.C2ContactNumber);
                    cmd.Parameters.AddWithValue("@C2DOB", child.C2Dob == null ? "" : child.C2Dob.ToString());
                    cmd.Parameters.AddWithValue("@C2Age", child.C2Age == null ? 0 : child.C2Age);
                    cmd.Parameters.AddWithValue("@C2IdProofType", child.C2IdProofType == null ? "" : child.C2IdProofType);
                    cmd.Parameters.AddWithValue("@C2IdProofNo", child.C2IdProofNo == null ? "" : child.C2IdProofNo);
                    cmd.Parameters.AddWithValue("@C2MonthlyIncome", child.C2MonthlyIncome == null ? 0 : child.C2MonthlyIncome);
                    cmd.Parameters.AddWithValue("@C1Gender", child.C1Gender == null ? "" : child.C1Gender);
                    cmd.Parameters.AddWithValue("@C2Gender", child.C2Gender == null ? "" : child.C2Gender);
                    cmd.ExecuteNonQuery();

                }



            }
        }
        public void GetChildrenDetails()
        {
            using(SqlConnection con= new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select c1name,c1mobile,c1dob,c1age,c1idprooftype,c1idproofno,c1addressprooftype,c1addressproofno,c1monthlyincome,c2name,c2mobile,c2dob,c2age,c2idprooftype,c2idproofno,c2addressprooftype,c2addressproofno,c2monthlyincome,C1Gender,C2Gender from ChildDetails  where CustId='" + _customerId + "'";
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while(dr.Read())
                {
                    _c1name = dr.GetString(0);
                    _c1contactNumber = dr.GetString(1);
                    var dt = dr.GetDataTypeName(2);
                    _c1dob = dr.GetDateTime(2);
                    _c1age = dr.GetInt32(3);
                    _c1idprooftype = dr.GetString(4);
                    _c1idproofno = dr.GetString(5);
                    _c1addressprooftype = dr.GetString(6);
                    _c1addressproofno = dr.GetString(7);
                    _c1monthlyincome = dr.GetInt32(8);

                    _c2name = dr.GetString(9);
                    _c2contactNumber = dr.GetString(10);
                    _c2dob = dr.GetDateTime(11);
                    _c2age = dr.GetInt32(12);
                    _c2idprooftype = dr.GetString(13);
                    _c2idproofno = dr.GetString(14);
                    _c2addressprooftype = dr.GetString(15);
                    _c2addressproofno = dr.GetString(16);
                    _c2monthlyincome = dr.GetInt32(17);

                    _c1gender = dr.GetString(18);
                    _c2gender = dr.GetString(19);

                }
                dr.Close();
            }
        }

        public void InsertChildDetails(Children child)
        {
            DateTime? dtime = null;
            DateTime? dtime2 = null;
            if (child.C1Dob != null)
            {
                dtime = child.C1Dob;
            }
            if (child.C2Dob != null)
            {
                dtime2 = child.C2Dob;
            }
            using (SqlConnection con = new SqlConnection(Properties.Settings.Default.db))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.Parameters.Clear();
                cmd.CommandText = "insert into ChildDetails(CustId,C1Name,C1Mobile,C1DOB,C1Age,C1IdProoftype,C1IdProofNo,C1AddressProoftype,C1AddressProofNo,C1MonthlyIncome,C2Name,C2Mobile,C2DOB,C2Age,C2IdProofType,C2IdProofNo,C2AddressProofType,C2AddressProofNo,C2MonthlyIncome,C1Gender,C2Gender) values(@CustId,@C1Name,@C1Mobile,@C1DOB,@C1Age,@C1IdProoftype,@C1IdProofNo,@C1AddressProoftype,@C1AddressProofNo,@C1MonthlyIncome,@C2Name,@C2Mobile,@C2DOB,@C2Age,@C2IdProofType,@C2IdProofNo,@C2AddressProofType,@C2AddressProofNo,@C2MonthlyIncome,@C1Gender,@C2Gender)";
                cmd.Parameters.AddWithValue("@CustId", _customerId);
                cmd.Parameters.AddWithValue("@C1Name", child.C1Name != null ? child.C1Name : "");
                cmd.Parameters.AddWithValue("@C1Mobile", child.C1ContactNumber != null ? child.C1ContactNumber : "");
                cmd.Parameters.AddWithValue("@C1DOB", dtime);
                cmd.Parameters.AddWithValue("@C1Age", child.C1Age != null ? child.C1Age : 0);
                cmd.Parameters.AddWithValue("@C1IdProoftype", child.NameofPhotoProofC1 != null ? child.NameofPhotoProofC1 : "");
                cmd.Parameters.AddWithValue("@C1IdProofNo", child.C1IdProofNo != null ? child.C1IdProofNo : "");
                cmd.Parameters.AddWithValue("@C1AddressProoftype", child.NameofAddressProofC1 != null ? child.NameofAddressProofC1 : "");
                cmd.Parameters.AddWithValue("@C1AddressProofNo", child.C1AddressProofNo != null ? child.C1AddressProofNo : "");
                cmd.Parameters.AddWithValue("@C1MonthlyIncome", child.C1MonthlyIncome != null ? child.C1MonthlyIncome : 0);
                cmd.Parameters.AddWithValue("@C2Name", child.C2Name != null ? child.C2Name : "");
                cmd.Parameters.AddWithValue("@C2Mobile", child.C2ContactNumber != null ? child.C2ContactNumber : "");
                cmd.Parameters.AddWithValue("@C2DOB", dtime2);
                cmd.Parameters.AddWithValue("@C2Age", child.C2Age != null ? child.C2Age : 0);
                cmd.Parameters.AddWithValue("@C2IdProoftype", child.NameofPhotoProofC2 != null ? child.NameofPhotoProofC2 : "");
                cmd.Parameters.AddWithValue("@C2IdProofNo", child.C2IdProofNo != null ? child.C2IdProofNo : "");
                cmd.Parameters.AddWithValue("@C2AddressProoftype", child.NameofAddressProofC2 != null ? child.NameofAddressProofC2 : "");
                cmd.Parameters.AddWithValue("@C2AddressProofNo", child.C2AddressProofNo != null ? child.C2AddressProofNo : "");
                cmd.Parameters.AddWithValue("@C2MonthlyIncome", child.C2MonthlyIncome != null ? child.C2MonthlyIncome : 0);
                cmd.Parameters.AddWithValue("@C1Gender", child.C1Gender != null ? child.C1Gender : "");
                cmd.Parameters.AddWithValue("@C2Gender", child.C2Gender != null ? child.C2Gender : "");
                cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
            }

       
        }

    }
}
