using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace MicroFinance.Modal
{
    public class Branch:BindableBase
    {
        public List<Branch> BranchList = new List<Branch>();
        public List<string> RegionList = new List<string>();
        public List<string> BranchListDetails = new List<string>();
        private string ConnectionString = Properties.Settings.Default.DBConnection;
        public Branch()
        {
          
        }
        private string _regionName;
        public string RegionName
        {
            get
            {
                return _regionName;
            }
            set
            {
                if (value != _regionName)
                {
                   _regionName = value;
                   RaisedPropertyChanged("RegionName");
                }
                
            }
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
               
             _branchname = value;
              RaisedPropertyChanged("BranchName");
              
            }
        }
        private string _branchaddress;
        public string BranchAddress
        {
            get
            {
                return _branchaddress;
            }
            set
            {
                _branchaddress = value;
                RaisedPropertyChanged("BranchAddress");
            }
        }
        private string _landlinenumber;
        public string LandlineNumber
        {
            get
            {
                return _landlinenumber;
            }
            set
            {
                _landlinenumber = value;
                 RaisedPropertyChanged("LandlineNumber"); 
            }

            
        }
        int _landlinecostpermonth;
        public int CostPerMonth
        {
            get
            {
                return _landlinecostpermonth;
            }
            set
            {
              
                        _landlinecostpermonth = value;
                        RaisedPropertyChanged("CostPerMonth");
              
            } 
        }
        private string _managername;
        public string ManagerName
        {
            get
            {
                return _managername;
            }
            set
            {
                if(value!=_managername)
                {
                    _managername = value;
                    RaisedPropertyChanged("ManagerName");
                }
            }
        }
        private string _accoutantname;
        public string AccountantName
        {
            get
            {
                return _accoutantname;
            }
            set
            {
                _accoutantname = value;
                RaisedPropertyChanged("AccountantName");
            }
        }
        private DateTime _agreementenddate = DateTime.Today;
        public DateTime AgreementEndDate
        {
            get
            {
                return _agreementenddate;
            }
            set
            {
                _agreementenddate = value;
                RaisedPropertyChanged("AgreementStartDate");
            }
        }
        private DateTime _agreementstartdate = DateTime.Today;
        public DateTime AgreementStartDate
        {
            get
            {
                return _agreementstartdate;
            }
            set
            {
                _agreementstartdate = value;
                RaisedPropertyChanged("AgreementStartDate");
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
        private string _ebconnectionname;
        public string EBConnectionName
        {
            get
            {
                return _ebconnectionname;
            }
            set
            {
               
                 _ebconnectionname = value;
                  RaisedPropertyChanged("EbConnectionName");
            }
        }
        private string _ebconnectionnubmer;
        public string EBConnectionNumber
        {
            get
            {
                return _ebconnectionnubmer;
            }
            set
            {
               
                _ebconnectionnubmer = value;
                  RaisedPropertyChanged("EBConnectionNumber");
            }
        }
        public string InternetConnectionName { get; set; }
        private int _internetconnectioncost;
        public int InternetConnectionCost
        {
            get
            {
                return _internetconnectioncost;
            }
            set
            {
                    if (value != _internetconnectioncost)
                    {
                     
                         _internetconnectioncost = value;
                         RaisedPropertyChanged("InternetConnectionCost");
                       
                        
                    }
               
            }
        }
        private string _ownername;
        public string OwnerName
        {
            get
            {
                return _ownername;
            }
            set
            {
                _ownername = value;
                RaisedPropertyChanged("OwnerName");
            }
        }
        private string _ownercontactnumber;
        public string OwnerContactNumber
        {
            get
            {
                return _ownercontactnumber;
            }
            set
            {

               if(value!=OwnerContactNumber)
                {
                   
                     _ownercontactnumber = value;
                      RaisedPropertyChanged("OwnerContactNumber");
                      
                }

            }
        }
        public string OwnerAddress
        {
            get;
            set;
        }
        private int _advancepaid;
        public int AdvancePaid
        {
            get
            {
                return _advancepaid;
            }
            set
            {
                if(value!=_advancepaid)
                {
                   
                    _advancepaid = value;
                    RaisedPropertyChanged("AdvancePaid");
                }
            }
        }
        private int _rentpermonth;
        public int RentPerMonth
        {
            get
            {
                return _rentpermonth;
            }
            set
            {
               
                    _rentpermonth = value;
                    RaisedPropertyChanged("RentPerMonth");
               
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
                if(value!=_accountnumber)
                {
                   _accountnumber = value;
                   RaisedPropertyChanged("AccountNumber");
                }
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
                if(value!=_ifsccode)
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

        public void GetRegionList()
        {
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "Select RegionName from Region";
                    SqlDataReader sqlDataReader = sqlcomm.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        RegionList.Add(sqlDataReader.GetString(0));
                    }
                    sqlDataReader.Close();
                }
                sqlconn.Close();
            }
        }
        public void GetBranchList()
        {
            BranchList = new List<Branch>();
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.Connection = sqlconn;
                sqlcomm.CommandText = "select BranchName,RegionName from BranchDetails";
                SqlDataReader reader = sqlcomm.ExecuteReader();
                while (reader.Read())
                {
                    BranchList.Add(new Branch
                    {
                        _branchname=reader.GetString(0),
                        _regionName = reader.GetString(1)
                    }) ;
                }
                reader.Close();
                sqlconn.Close();
            }

        }
        public void GetBranchList(string RegionName)
        {
            BranchList = new List<Branch>();
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.Connection = sqlconn;
                sqlcomm.CommandText = "select BranchName from BranchDetails where RegionName='"+RegionName+"'";
                SqlDataReader reader = sqlcomm.ExecuteReader();
                while (reader.Read())
                {
                    BranchListDetails.Add(reader.GetString(0).ToString());
                }
                reader.Close();
                sqlconn.Close();
            }

        }


        public int GetBranchCount()
        {
            int number = 1;
            using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlComm = new SqlCommand();
                    sqlComm.Connection = sqlconn;
                    sqlComm.CommandText = "select count(BranchName) from BranchDetails";
                    int n = (int)sqlComm.ExecuteScalar();
                    number += n;
                }
                sqlconn.Close();
            }
            
            return number;
        }
        public string GetRegionID(string RegionName)
        {
            string Result = "";
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "Select RegionID From Region where RegionName='" + RegionName + "'";
                    Result = (string)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
            }
            return Result;
        }

        public string GetBranchName(string ID)
        {
            string Result="";
            using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "Select BranchName From BranchDetails where Bid='" + ID + "'";
                    Result =(string) sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
            }
            return Result;
        }
        public string GetBranchID(string Name)
        {
            string Result = "";
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "Select Bid From BranchDetails where BranchName='" + Name + "'";
                    Result = (string)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
            }
            return Result;
        }

        public List<string> ActiveEmployees(string Branchid)
        {
            List<string> ActiveEmployees = new List<string>();
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select Empid from EmployeeBranch where Bid='"+Branchid+"' and IsActive="+1+"";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    while(reader.Read())
                    {
                        string Result = reader.GetString(0);
                        ActiveEmployees.Add(Result);
                    }
                }
                sqlconn.Close();
            }
            return ActiveEmployees;
        }

        public string GetRegionName(string ID)
        {
            string Result = "";
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "Select RegionName From BranchDetails where Bid='" + ID + "'";
                    Result = (string)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
            }
            return Result;
        }

        public string GenerateBranchID()// IDPattern 01202106001 (01-RegionNumber/2021-CurrentYear/06-CurrentMonth/001-(CountOfBranch+1))
        {
            int count = GetBranchCount();
            int regionnumber = 0;
            string Result = "";
            int year = DateTime.Now.Year;
            int mon = DateTime.Now.Month;
            string month = ((mon) < 10 ? "0" + mon : mon.ToString());
            using (SqlConnection sqlcon = new SqlConnection(ConnectionString))
            {
                sqlcon.Open();
                if (sqlcon.State == ConnectionState.Open)
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = sqlcon;
                    sqlCommand.CommandText = "select RegionCode from Region where RegionName='"+RegionName+"'";
                    regionnumber = (int)sqlCommand.ExecuteScalar();
                }
                sqlcon.Close();
            }
            Result = DigitConvert(regionnumber.ToString(),2)+ year+ month+DigitConvert(count.ToString());
            return Result;
        }
        public string DigitConvert(string digit, int place=3)
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

        public void AddBranch(string regionId)
        {
            int count = GetBranchCount();
           using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
           {
                    sqlconn.Open();
                    if (sqlconn.State == ConnectionState.Open)
                    {
                        SqlCommand sqlcomm = new SqlCommand();
                        sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "insert into BranchDetails(BranchCode,RegionId,Bid,RegionName,BranchName,Address,LandLineNumber,LandLineCost,AgreementStartDate,EBNumber,EBConnectionName,InternetConnectionName,InternetCost,BuildingOwnerName,OwnerContact,OwnerAddress,AdvancePaid,MonthlyRent,OwnerACBankName,OwnerACBranchName,AccountHolderName,AccountNumber,IFSCCode,MICRCode,AgreementEndDate,OwnerPanNumber)values(" + count + ",'" + GetRegionID(RegionName) + "','" + GenerateBranchID() + "','" + _regionName + "','" + _branchname + "','" + _branchaddress + "','" + _landlinenumber + "'," + _landlinecostpermonth + ",'" + _agreementstartdate.ToString("MM-dd-yyyy") + "','" + _ebconnectionnubmer + "','" + _ebconnectionname.ToUpper() + "','" + InternetConnectionName + "'," + _internetconnectioncost + ",'" + OwnerName + "','" + _ownercontactnumber + "','" + OwnerAddress + "'," + _advancepaid + "," + _rentpermonth + ",'" + _bankname + "','" + _bankbranchname + "','" + _accountholdername + "','" + _accountnumber + "','" + _ifsccode + "','" + MICRCode + "','" + _agreementenddate.ToString("MM-dd-yyyy") + "','" + _pannumber + "')";

                    int res=(int) sqlcomm.ExecuteNonQuery();
                    if (res == 1)
                        CreateBranchFiles();
                    }
                    sqlconn.Close();
           }
            
        }


        void CreateBranchFiles()
        {
            string BasePath = MainWindow.DriveBasePath;

            string BranchPath = BasePath + "\\" +RegionName+"\\"+ BranchName;
            Directory.CreateDirectory(BranchPath);

            CreateEmployeeFolders(BranchPath);
            CreateCutomerFolders(BranchPath);
            CreateGuarantorFolders(BranchPath);
            CreateNomineeFolders(BranchPath);
        }
        void CreateEmployeeFolders(string BranchPath)
        {
            string EmployeePath = BranchPath + "\\" + "Employee";
            Directory.CreateDirectory(EmployeePath);
            Directory.CreateDirectory(EmployeePath + "\\" + "Address Proof");
            Directory.CreateDirectory(EmployeePath + "\\" + "Photo Proof");
            Directory.CreateDirectory(EmployeePath + "\\" + "Profile Picture");
        }
        void CreateCutomerFolders(string BranchPath)
        {
            string CustomerPath = BranchPath + "\\" + "Customer";
            Directory.CreateDirectory(CustomerPath);
            Directory.CreateDirectory(CustomerPath + "\\" + "Address Proof");
            Directory.CreateDirectory(CustomerPath + "\\" + "Photo Proof");
            Directory.CreateDirectory(CustomerPath + "\\" + "Profile Picture");
            Directory.CreateDirectory(CustomerPath + "\\" + "Combine Photo");
        }
        void CreateGuarantorFolders(string BranchPath)
        {
            string GuarantorPath = BranchPath + "\\" + "Guarantor";
            Directory.CreateDirectory(GuarantorPath);
            Directory.CreateDirectory(GuarantorPath + "\\" + "Address Proof");
            Directory.CreateDirectory(GuarantorPath + "\\" + "Photo Proof");
            Directory.CreateDirectory(GuarantorPath + "\\" + "Profile Picture");
           
        }
        void CreateNomineeFolders(string BranchPath)
        {
            string GuarantorPath = BranchPath + "\\" + "Nominee";
            Directory.CreateDirectory(GuarantorPath);
            Directory.CreateDirectory(GuarantorPath + "\\" + "Address Proof");
            Directory.CreateDirectory(GuarantorPath + "\\" + "Photo Proof");
            Directory.CreateDirectory(GuarantorPath + "\\" + "Profile Picture");

        }

        public bool IsExists()
        {
            using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select RegionName,BranchName from BranchDetails where RegionName = '"+_regionName+"' and BranchName = '"+_branchname+"'";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            string regionresult = reader.GetString(0);
                            string branchresult = reader.GetString(1);
                            if (regionresult.Equals(_regionName,StringComparison.CurrentCultureIgnoreCase)&&branchresult.Equals(BranchName,StringComparison.CurrentCultureIgnoreCase))
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

        public int IsAmount(string value)
        {
            int a;
            bool result = int.TryParse(value, out a);
            if(result!=true)
            {
                throw new ArgumentException("Invalid Amount");
            }
            return a;
        }
       
       
    }
}
