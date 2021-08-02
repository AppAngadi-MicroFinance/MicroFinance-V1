using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace MicroFinance.Modal
{
    public class LoanProcess:Customer
    {
        public List<LoanProcess> RequestList = new List<LoanProcess>();
        public List<LoanProcess> RecommendList = new List<LoanProcess>();
        private string[] _guaranterDetails = new string[2];
        //string ConnectionString = MicroFinance.Properties.Settings.Default.DBConnection;

       // public string CustomerID { get; set; }

       // public LoanDetails loanDetails;
        
        public string GuaranterName
        {
            get
            {
                string Result = "";
                using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
                {
                    sqlconn.Open();
                    if (sqlconn.State == ConnectionState.Open)
                    {
                        SqlCommand sqlcomm = new SqlCommand();
                        sqlcomm.Connection = sqlconn;
                        // Pending Check if Guarnter is True
                        sqlcomm.CommandText = "select Name from GuarenteeDetails where CustId='" + _customerId + "'";
                        Result = (string)sqlcomm.ExecuteScalar();
                    }
                    sqlconn.Close();
                }
                return Result;
            }
        }

        
        public string GuaranterRelatioShip
        {
            get
            {
                string Result = "";
                using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
                {
                    sqlconn.Open();
                    if (sqlconn.State == ConnectionState.Open)
                    {
                        SqlCommand sqlcomm = new SqlCommand();
                        sqlcomm.Connection = sqlconn;
                        // Pending Check if Guarnter is True
                        sqlcomm.CommandText = "select RelationShip from GuarenteeDetails where CustId='" + _customerId + "'";
                        Result = (string)sqlcomm.ExecuteScalar();
                    }
                    sqlconn.Close();
                }
                return Result;
            }
            
        }

        public string FieldOfficerName
        {
            get
            {
                string Result = "";
                using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
                {
                    sqlconn.Open();
                    if (sqlconn.State == ConnectionState.Open)
                    {
                        SqlCommand sqlcomm = new SqlCommand();
                        sqlcomm.Connection = sqlconn;
                        sqlcomm.CommandText = "select Name from Employee where EmpId='"+EmployeeID+"'";
                        Result = (string)sqlcomm.ExecuteScalar();
                    }
                    sqlconn.Close();
                }
                return Result;

            }
        }

        public string SHGName
        {
            get
            {
                string Result="";
                using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
                {
                    sqlconn.Open();
                    if(sqlconn.State==ConnectionState.Open)
                    {
                        SqlCommand sqlcomm = new SqlCommand();
                        sqlcomm.Connection = sqlconn;
                        sqlcomm.CommandText = "select SelfHelpGroup from CustomerGroup where CustId='"+_customerId+"'";
                        Result = (string)sqlcomm.ExecuteScalar();
                    }
                    sqlconn.Close();
                }
                return Result;
            }
        }

        public string Address
        {
            get
            {
                return DoorNumber + ", " + StreetName + ", " + Pincode;
            }
        }

       

        public void GetRequestList(string Bid)
        {
            using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select * from CustomerDetails join loanApplication on LoanApplication.BranchID='"+Bid+"' and LoanApplication.LoanStatus=1 and LoanApplication.CustId=CustomerDetails.CustId";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            string[] _fullAdress = reader.GetString(18).Split('|', '~');
                            RequestList.Add(
                                new LoanProcess
                                {
                            _customerId = reader.GetString(0),
                            CustomerName = reader.GetString(1),
                            FatherName = reader.GetString(2),
                            MotherName = reader.GetString(3),
                            DateofBirth = reader.GetDateTime(4),
                            Age = reader.GetInt32(5),
                            Gender = reader.GetString(6),
                            ContactNumber = reader.GetString(7),
                            AadharNo = reader.GetString(8),
                            Religion = reader.GetString(9),
                            Caste = reader.GetString(10),
                            Community = reader.GetString(11),
                            Education = reader.GetString(12),
                            FamilyMembers = reader.GetInt32(13),
                            EarningMembers = reader.GetInt32(14),
                            Occupation = reader.GetString(15),
                            MonthlyIncome = reader.GetInt32(16),
                            MothlyExpenses = reader.GetInt32(17),
                            DoorNumber = _fullAdress[0],
                            StreetName = _fullAdress[2],
                            LocalityTown = _fullAdress[4],
                            City = _fullAdress[6],
                            State = _fullAdress[8],
                            Pincode = reader.GetInt32(19),
                            HousingType = reader.GetString(20),
                            //if (reader.GetBoolean(23))
                            //{
                                HavingBankDetails = true,
                                AccountHolder = reader.GetString(28),
                                AccountNumber = reader.GetString(29),
                                BankName = reader.GetString(30),
                                BankBranchName = reader.GetString(31),
                                IFSCCode = reader.GetString(32),
                                MICRCode = reader.GetString(33),
                            //}
                            //if (reader.GetBoolean(24))
                            //{
                                NameofAddressProof = reader.GetString(21),
                                AddressProof = ByteToBI((byte[])reader.GetValue(34)),
                            //}
                            //if (reader.GetBoolean(25))
                            //{
                                NameofPhotoProof = reader.GetString(22),
                                PhotoProof = ByteToBI((byte[])reader.GetValue(35)),
                            //}
                            //if (reader.GetBoolean(26))
                                ProfilePicture = ByteToBI((byte[])reader.GetValue(36)),  
                           LoanRequestID=reader.GetString(42),
                           EmployeeID=reader.GetString(43),
                           LoanType=reader.GetString(44),
                           LoanAmount=reader.GetInt32(45),
                           LoanPeriod=reader.GetInt32(46),
                           LoanPurpose=reader.GetString(47),
                           EnrollDate=reader.GetDateTime(48),  
                        }) ;

                        }
                    }

                }
            }
        }
       
        public void GetRecommendList()
        {
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select * from CustomerDetails join LoanRequest on LoanRequest.StatusCode=2 and LoanRequest.CustomerID=CustomerDetails.CustId";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string[] _fullAdress = reader.GetString(17).Split('|', '~');
                            RecommendList.Add(
                                new LoanProcess
                                {
                                    _customerId = reader.GetString(0),
                                    CustomerName = reader.GetString(1),
                                    FatherName = reader.GetString(2),
                                    MotherName = reader.GetString(3),
                                    DateofBirth = reader.GetDateTime(4),
                                    Age = reader.GetInt32(5),
                                    Gender = reader.GetString(6),
                                    ContactNumber = reader.GetString(7),
                                    Religion = reader.GetString(8),
                                    Caste = reader.GetString(9),
                                    Community = reader.GetString(10),
                                    Education = reader.GetString(11),
                                    FamilyMembers = reader.GetInt32(12),
                                    EarningMembers = reader.GetInt32(13),
                                    Occupation = reader.GetString(14),
                                    MonthlyIncome = reader.GetInt32(15),
                                    MothlyExpenses = reader.GetInt32(16),
                                    DoorNumber = _fullAdress[0],
                                    StreetName = _fullAdress[2],
                                    LocalityTown = _fullAdress[4],
                                    City = _fullAdress[6],
                                    State = _fullAdress[8],
                                    Pincode = reader.GetInt32(18),
                                    HousingType = reader.GetString(19),
                                    HousingIndex = reader.GetInt32(20).ToString(),
                                    //if (reader.GetBoolean(23))
                                    //{
                                    HavingBankDetails = true,
                                    AccountHolder = reader.GetString(27),
                                    AccountNumber = reader.GetString(28),
                                    BankName = reader.GetString(29),
                                    BankBranchName = reader.GetString(30),
                                    IFSCCode = reader.GetString(31),
                                    MICRCode = reader.GetString(32),
                                    
                                    //}
                                    //if (reader.GetBoolean(24))
                                    //{
                                    NameofAddressProof = reader.GetString(21),
                                    AddressProof = ByteToBI((byte[])reader.GetValue(33)),
                                    //}
                                    //if (reader.GetBoolean(25))
                                    //{
                                    NameofPhotoProof = reader.GetString(22),
                                    PhotoProof = ByteToBI((byte[])reader.GetValue(34)),
                                    //}
                                    //if (reader.GetBoolean(26))
                                    ProfilePicture = ByteToBI((byte[])reader.GetValue(35)),
                                    AadharNo = reader.GetString(40),
                                    LoanRequestID = reader.GetString(41),
                                    EmployeeID = reader.GetString(43),
                                    LoanType = reader.GetString(44),
                                    LoanAmount = reader.GetInt32(45),
                                    LoanPeriod = reader.GetInt32(46),
                                    LoanPurpose = reader.GetString(47),
                                    EnrollDate = reader.GetDateTime(48),

                                });

                        }
                    }

                }
            }
        }
        public void ApproveLoan(string ID)
        {
            GetRequestDetails(ID);
            RequestApproval(ID);
            string LoanId = GenerateLoanID();
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "insert into LoanDetails(LoanID,CustomerID,LoanType,LoanPeriod,InterestRate,RequestedBY,ApprovedBy,ApproveDate,LoanAmount)values('" +LoanId  + "','" + _customerId + "','" + LoanType + "'," + LoanPeriod + "," + InterestRate + ",'" + EmployeeID + "','" + ApprovedBy + "','" + DateTime.Now.ToString("MM-dd-yyyy") + "'," + LoanAmount + ")";
                    sqlcomm.ExecuteNonQuery();
                    sqlcomm = new SqlCommand();
                    sqlcomm.Connection=sqlconn;
                    sqlcomm.CommandText = "insert into LoanDisposement(LoanID,CustID,DurationInWeeks,LoanAmount,StartDate,EndDate,LoanType,Active) values ('" + LoanId + "','" + _customerId + "','" + InstallmentWeek(LoanPeriod) + "',"+LoanAmount+",'"+DateTime.Today.ToString("MM-dd-yyyy")+ "','" + DateTime.Today.ToString("MM-dd-yyyy") + "','"+LoanType+"','True')";
                    sqlcomm.ExecuteNonQuery();
                    sqlcomm = new SqlCommand();
                    sqlcomm.Connection=sqlconn;
                    sqlcomm.CommandText = "update CustomerDetails set IsActive='true' where CustId='"+_customerId+"'";
                    sqlcomm.ExecuteNonQuery();
                }
                sqlconn.Close();
            }

        }
        public void RejectLoan(string ID)
        {
            using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "Update LoanRequest Set StatusCode='4',Status='Rejected',Remark='" + Remark + "'where RequestID='" + ID + "' ";
                    sqlcomm.ExecuteNonQuery();
                }
            }
        }

        public int InstallmentWeek(int month)
        {
            int Result = 0;
            switch(month)
            {
                case 12:
                    Result = 50;
                    break;
                case 24:
                    Result = 100;
                    break;
                case 6:
                    Result = 25;
                    break;
            }
            return Result;
        }
       

    }
}
