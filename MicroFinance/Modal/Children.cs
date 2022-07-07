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


        public void GetChildrenDetails()
        {
            using(SqlConnection con= new SqlConnection())
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select c1name,c1mobile,c1dob,c1age,c1idprooftype,c1idproofno,c1addressprooftype,c1addressproofno,c1monthlyincome,c2name,c2mobile,c2dob,c2age,c2idprooftype,c2idproofno,c2addressprooftype,c2addressproofno,c2monthlyincome from ChildDetails  where CustId='" + _customerId + "'";
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while(dr.Read())
                {

                }
                dr.Close();
            }
        }


    }
}
