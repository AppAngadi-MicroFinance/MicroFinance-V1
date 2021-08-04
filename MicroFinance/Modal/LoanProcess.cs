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
                        sqlcomm.CommandText = "select SHGName from SelfHelpGroup where SHGId=(select SHGid  from PeerGroup where GroupId=(select PeerGroupId from CutomerGroup where CustId='"+_customerId+"'))";
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
                    sqlcomm.CommandText = "select CustomerDetails.CustId,Name,FatherName,MotherName,Dob,age,Gender,Mobile,AadharNumber,Religion,Caste,Community,Education,FamilyMembers,EarningMembers,Occupation,MonthlyIncome,MonthlyExpenses,Address,Pincode,HousingType,AddressProofName,PhotoProofName,IsAddressProof,IsPhotoProof,IsProfilePhoto,BankACHolderName,BankAccountNo,BankBranchName,IFSCCode,MICRCode,AddressProof,PhotoProof,ProfilePhoto,LoanApplication.RequestId,LoanApplication.LoanAmount,LoanApplication.LoanType,LoanApplication.LoanPeriod,LoanApplication.Purpose,LoanApplication.EnrollDate,LoanApplication.LoanStatus,LoanApplication.EmployeeId,LoanApplication.BranchId, CustomerDetails.BankName from CustomerDetails,LoanApplication where CustomerDetails.CustId=LoanApplication.CustId and LoanApplication.BranchId='" + Bid+"' and LoanApplication.LoanStatus='1'";
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
                                    StreetName = _fullAdress[1],
                                    LocalityTown = _fullAdress[2],
                                    City = _fullAdress[3],
                                    State = _fullAdress[4],
                                    Pincode = reader.GetInt32(19),
                                    HousingType = reader.GetString(20),
                                    NameofAddressProof = reader.GetString(21),
                                    NameofPhotoProof = reader.GetString(22),
                                    AccountHolder = reader.GetString(26),
                                    AccountNumber = reader.GetString(27),
                                    BankBranchName = reader.GetString(28),
                                    IFSCCode = reader.GetString(29),
                                    MICRCode = reader.GetString(30),
                                    AddressProof = (reader.GetBoolean(23)) ? ByteToBI((byte[])reader.GetValue(31)) : null,
                                    PhotoProof = (reader.GetBoolean(24)) ? ByteToBI((byte[])reader.GetValue(32)) : null,
                                    ProfilePicture = (reader.GetBoolean(25)) ? ByteToBI((byte[])reader.GetValue(33)) : null,
                                    LoanRequestID = reader.GetString(34),
                                    LoanAmount = reader.GetInt32(35),
                                    LoanType = reader.GetString(36),
                                    LoanPeriod = reader.GetInt32(37),
                                    LoanPurpose = reader.GetString(38),
                                    EnrollDate = reader.GetDateTime(39),
                                    EmployeeID = reader.GetString(41),
                                    BranchID = reader.GetString(42),
                                    BankName=reader.GetString(43)


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
                            string[] _fullAdress = reader.GetString(18).Split('|', '~');
                            RecommendList.Add(
                                new LoanProcess
                    {
                      _customerId=reader.GetString(0),
                      CustomerName=reader.GetString(1),
                      FatherName=reader.GetString(2),
                      MotherName=reader.GetString(3),
                      DateofBirth=reader.GetDateTime(4),
                      Age=reader.GetInt32(5),
                      Gender=reader.GetString(6),
                      ContactNumber=reader.GetString(7),
                      AadharNo=reader.GetString(8),
                      Religion=reader.GetString(9),
                      Caste=reader.GetString(10),
                      Community=reader.GetString(11),
                      Education=reader.GetString(12),
                      FamilyMembers=reader.GetInt32(13),
                      EarningMembers=reader.GetInt32(14),
                      Occupation=reader.GetString(15),
                      MonthlyIncome=reader.GetInt32(16),
                      MothlyExpenses=reader.GetInt32(17),
                      DoorNumber=_fullAdress[0],
                      StreetName=_fullAdress[1],
                      LocalityTown=_fullAdress[2],
                      City=_fullAdress[3],
                      State=_fullAdress[4],
                      Pincode=reader.GetInt32(19),
                      HousingType=reader.GetString(20),
                      NameofAddressProof=reader.GetString(21),
                      NameofPhotoProof=reader.GetString(22),
                     AccountHolder=reader.GetString(26),
                     AccountNumber=reader.GetString(27),
                     BankBranchName=reader.GetString(28),
                     IFSCCode=reader.GetString(29),
                     MICRCode=reader.GetString(30),
                     AddressProof=(reader.GetBoolean(23))?ByteToBI((byte[]) reader.GetValue(31)):null,
                     PhotoProof=(reader.GetBoolean(24))?ByteToBI((byte[]) reader.GetValue(32)):null,
                     ProfilePicture=(reader.GetBoolean(25))?ByteToBI((byte[]) reader.GetValue(33)):null,
                     LoanRequestID=reader.GetString(34),
                     LoanAmount=reader.GetInt32(35),
                     LoanType=reader.GetString(36),
                     LoanPeriod=reader.GetInt32(37),
                     LoanPurpose=reader.GetString(38),
                     EnrollDate=reader.GetDateTime(39),
                     EmployeeID=reader.GetString(41),
                     BranchID=reader.GetString(42),






                    });

                        }
                    }

                }
            }
        }

        public void ApproveLoanFromHimark(string ReqID)
        {
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "Update LoanApplication Set LoanStatus='2' where Requestid='" + ReqID+ "' ";
                    sqlcomm.ExecuteNonQuery();
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
