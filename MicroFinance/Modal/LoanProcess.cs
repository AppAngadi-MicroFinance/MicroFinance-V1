using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace MicroFinance.Modal
{
    public class LoanProcess : Customer
    {
        public ObservableCollection<LoanProcess> RequestList = new ObservableCollection<LoanProcess>();
        public ObservableCollection<LoanProcess> RecommendList = new ObservableCollection<LoanProcess>();
        public List<LoanProcess> LoanProcessList = new List<LoanProcess>();
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
                        sqlcomm.CommandText = "select Name from Employee where EmpId='" + EmployeeID + "'";
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
                string Result = "";
                using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
                {
                    sqlconn.Open();
                    if (sqlconn.State == ConnectionState.Open)
                    {
                        SqlCommand sqlcomm = new SqlCommand();
                        sqlcomm.Connection = sqlconn;
                        sqlcomm.CommandText = "select SHGName from SelfHelpGroup where SHGId=(select SHGid  from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='" + _customerId + "'))";
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
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustomerDetails.CustId,Name,FatherName,MotherName,Dob,age,Gender,Mobile,AadharNumber,Religion,Caste,Community,Education,FamilyMembers,EarningMembers,Occupation,MonthlyIncome,MonthlyExpenses,Address,Pincode,HousingType,AddressProofName,PhotoProofName,IsAddressProof,IsPhotoProof,IsProfilePhoto,BankACHolderName,BankAccountNo,BankBranchName,IFSCCode,MICRCode,AddressProof,PhotoProof,ProfilePhoto,LoanApplication.RequestId,LoanApplication.LoanAmount,LoanApplication.LoanType,LoanApplication.LoanPeriod,LoanApplication.Purpose,LoanApplication.EnrollDate,LoanApplication.LoanStatus,LoanApplication.EmployeeId,LoanApplication.BranchId, CustomerDetails.BankName from CustomerDetails,LoanApplication where CustomerDetails.CustId=LoanApplication.CustId and LoanApplication.BranchId='" + Bid + "' and LoanApplication.LoanStatus='1'";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
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
                                    NameofAddressProof = (DBNull.Value.Equals(reader.GetString(21)) ? "" : reader.GetString(21)),
                                    NameofPhotoProof = (DBNull.Value.Equals(reader.GetString(22)) ? "" : reader.GetString(22)),
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
                                    LoanStatus = reader.GetInt32(40),
                                    EmployeeID = reader.GetString(41),
                                    BranchID = reader.GetString(42),
                                    BankName = reader.GetString(43)
                                });

                        }
                    }

                }
            }
        }
        public void GetRecommendList(string Bid)
        {
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustomerDetails.CustId,Name,FatherName,MotherName,Dob,age,Gender,Mobile,AadharNumber,Religion,Caste,Community,Education,FamilyMembers,EarningMembers,Occupation,MonthlyIncome,MonthlyExpenses,Address,Pincode,HousingType,AddressProofName,PhotoProofName,IsAddressProof,IsPhotoProof,IsProfilePhoto,BankACHolderName,BankAccountNo,BankBranchName,IFSCCode,MICRCode,AddressProof,PhotoProof,ProfilePhoto,LoanApplication.RequestId,LoanApplication.LoanAmount,LoanApplication.LoanType,LoanApplication.LoanPeriod,LoanApplication.Purpose,LoanApplication.EnrollDate,LoanApplication.LoanStatus,LoanApplication.EmployeeId,LoanApplication.BranchId, CustomerDetails.BankName from CustomerDetails,LoanApplication where CustomerDetails.CustId=LoanApplication.CustId and LoanApplication.BranchId='" + Bid + "' and LoanApplication.LoanStatus='7'";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string[] _fullAdress = reader.GetString(18).Split('|', '~');
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
                                    NameofAddressProof = (DBNull.Value.Equals(reader.GetString(21)) ? "" : reader.GetString(21)),
                                    NameofPhotoProof = (DBNull.Value.Equals(reader.GetString(22)) ? "" : reader.GetString(22)),
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
                                    BankName = reader.GetString(43)


                                });

                        }
                    }

                }
            }
        }
        public void GetLoanDetailList(string Bid, int Code)
        {
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustomerDetails.CustId,Name,FatherName,MotherName,Dob,age,Gender,Mobile,AadharNumber,Religion,Caste,Community,Education,FamilyMembers,EarningMembers,Occupation,MonthlyIncome,MonthlyExpenses,Address,Pincode,HousingType,AddressProofName,PhotoProofName,IsAddressProof,IsPhotoProof,IsProfilePhoto,BankACHolderName,BankAccountNo,BankBranchName,IFSCCode,MICRCode,AddressProof,PhotoProof,ProfilePhoto,LoanApplication.RequestId,LoanApplication.LoanAmount,LoanApplication.LoanType,LoanApplication.LoanPeriod,LoanApplication.Purpose,LoanApplication.EnrollDate,LoanApplication.LoanStatus,LoanApplication.EmployeeId,LoanApplication.BranchId, CustomerDetails.BankName from CustomerDetails,LoanApplication where CustomerDetails.CustId=LoanApplication.CustId and LoanApplication.BranchId='" + Bid + "' and LoanApplication.LoanStatus='" + Code + "'";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string[] _fullAdress = reader.GetString(18).Split('|', '~');
                            LoanProcessList.Add(
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
                                    BankName = reader.GetString(43)


                                });

                        }
                    }

                }
            }
        }
        public void GetLoanDetailList(string Bid, int Code, string EmpID)
        {
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustomerDetails.CustId,Name,FatherName,MotherName,Dob,age,Gender,Mobile,AadharNumber,Religion,Caste,Community,Education,FamilyMembers,EarningMembers,Occupation,MonthlyIncome,MonthlyExpenses,Address,Pincode,HousingType,AddressProofName,PhotoProofName,IsAddressProof,IsPhotoProof,IsProfilePhoto,BankACHolderName,BankAccountNo,BankBranchName,IFSCCode,MICRCode,AddressProof,PhotoProof,ProfilePhoto,LoanApplication.RequestId,LoanApplication.LoanAmount,LoanApplication.LoanType,LoanApplication.LoanPeriod,LoanApplication.Purpose,LoanApplication.EnrollDate,LoanApplication.LoanStatus,LoanApplication.EmployeeId,LoanApplication.BranchId, CustomerDetails.BankName from CustomerDetails,LoanApplication where CustomerDetails.CustId=LoanApplication.CustId and LoanApplication.BranchId='" + Bid + "' and LoanApplication.LoanStatus='" + Code + "' and LoanApplication.EmployeeId='" + EmpID + "'";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string[] _fullAdress = reader.GetString(18).Split('|', '~');
                            LoanProcessList.Add(
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
                                    BankName = reader.GetString(43)


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
                    sqlcomm.CommandText = "Update LoanApplication Set LoanStatus='2' where Requestid='" + ReqID + "' ";
                    sqlcomm.ExecuteNonQuery();
                }
            }
        }
        public void RetainFromHimark(string ReqID)
        {
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "Update LoanApplication Set LoanStatus='3' where Requestid='" + ReqID + "' ";
                    sqlcomm.ExecuteNonQuery();
                }
            }
        }
        public void RejectFromHimark(string ReqID)
        {
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "Update LoanApplication Set LoanStatus='4' where Requestid='" + ReqID + "' ";
                    sqlcomm.ExecuteNonQuery();
                }
            }
        }
        public void ChangeLoanStatus(string ReqID, int StatusCode)
        {
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "Update LoanApplication Set LoanStatus='" + StatusCode + "' where Requestid='" + ReqID + "' ";
                    sqlcomm.ExecuteNonQuery();
                }
            }
        }
        public void ApproveLoan(string ID)
        {
            GetRequestDetails(ID);
            ChangeLoanStatus(ID, 11);
            string LoanId = GenerateLoanID();
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "insert into LoanDetails(LoanID,CustomerID,LoanType,LoanPeriod,InterestRate,RequestedBY,ApprovedBy,ApproveDate,LoanAmount,IsActive)values('" + LoanId + "','" + _customerId + "','" + LoanType + "'," + LoanPeriod + "," + InterestRate + ",'" + EmployeeID + "','" + ApprovedBy + "','" + DateTime.Now.ToString("MM-dd-yyyy") + "'," + LoanAmount + ",'true')";
                    sqlcomm.ExecuteNonQuery();
                    sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "update CustomerDetails set IsActive='true' where CustId='" + _customerId + "'";
                    int Result=(int)sqlcomm.ExecuteNonQuery();
                    if(Result==1)
                    {
                        LoadData1(LoanId);
                    }
                }
                sqlconn.Close();
            }

        }
        public void RejectLoan(string ID)
        {
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "Update LoanApplication Set LoanStatus='10',Remark='" + Remark + "'where RequestId='" + ID + "' ";
                    sqlcomm.ExecuteNonQuery();
                }
            }
        }
        public int InstallmentWeek(int month)
        {
            int Result = 0;
            switch (month)
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


        //--Loan Master Table Entry Section--
        public DayOfWeek GetSHGCollectionDay(string CustomerID)
        {
            DayOfWeek Result = default;
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CollectionDay from TimeTable where SHGId=(select SHGid  from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='" + CustomerID + "'))";
                    string Value = (string)sqlcomm.ExecuteScalar();
                    Result = WeekDay(Value);
                }
                sqlconn.Close();
            }
            return Result;
        }


        void LoadData1(string LoanID)
        {
            // GetLoanDetails(CustId);
            DateTime ApproveDate = DateTime.Now;
            DayOfWeek CollectionDay = GetSHGCollectionDay(_customerId);
            DateTime NextCollectionDate = CollectionDate(CollectionDay);
            List<Loan> LoanCollectionList = Interestcc(LoanAmount, LoanPeriod, ApproveDate, NextCollectionDate);
            foreach (Loan item in LoanCollectionList)
            {
                InsertIntoLoanMaster(BranchID, _customerId, LoanID, item.WeekNo, item.DueDate, item.Amount, item.Interest, item.Total);
            }
        }
       
        //void GetLoanDetails(string CustomerID)
        //{
        //    using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.DBConnection))
        //    {
        //        sql.Open();
        //        SqlCommand command = new SqlCommand();
        //        command.Connection = sql;
        //        command.CommandText = "select LoanAmount, LoanPeriod, InterestRate,ApproveDate, LoanId from LoanDetails where IsActive = 1 and CustomerID = '" + CustomerID + "'";
        //        SqlDataReader reader = command.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            LoanAmount = reader.GetInt32(0);
        //            LoanPeriod = reader.GetInt32(1);
        //            InterestRate = reader.GetInt32(2);
        //            ApprovedDate = reader.GetDateTime(3);
        //            LoanId = reader.GetString(4);
        //        }
        //        reader.Close();
        //    }
        //}
        public static List<Loan> Interestcc(int amount, int weeksCount, DateTime loanIssuedDate, DateTime nextDueDate)
        {
            //int days = (nextDueDate - loanIssuedDate).Days;
            //if (days >= excuseDays)
            //    nextDueDate = nextDueDate.AddDays(7);

            int[] InterestSeq = new int[] { 5, 4, 2, 1, 0 };
            int interval = weeksCount / 5;
            List<Loan> Collection = new List<Loan>();

            int SinglePayment = amount / weeksCount;

            int PaymentCount = 0;
            int periodd = 0;
            for (int i = 0; i < weeksCount; i++)
            {
                PaymentCount++;
                Collection.Add(new Loan((i + 1), nextDueDate, SinglePayment, AmountForPercent(amount, InterestSeq[periodd]) / interval));
                nextDueDate = nextDueDate.AddDays(7);
                if (PaymentCount == interval)
                {
                    PaymentCount = 0;
                    periodd++;
                }
            }
            return Collection;
        }
        static void InsertIntoLoanMaster(string branchId, string custId, string LoanId, int weekNo, DateTime dueDate, int principal, int interest, int total)
        {
            using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "insert into LoanCollectionMaster(BranchId, CustId, LoanId, WeekNo, DueDate, Principal, Interest, Total) values('" + branchId + "','" + custId + "','" + LoanId + "'," + weekNo + ",'" + dueDate.ToString("yyyy-MM-dd") + "'," + principal + "," + interest + "," + total + ")";
                command.ExecuteNonQuery();
                sql.Close();
            }
        }
        public static int AmountForPercent(int amount, int percent)
        {
            decimal cal = amount / 100;
            decimal cal2 = cal * percent;
            return Convert.ToInt32(cal2);
        }
        public DateTime CollectionDate(DayOfWeek day)
        {
            DateTime ResultDate = default;
            DayOfWeek Today = DateTime.Now.DayOfWeek;
            int calValue = ((int)day - (int)Today);
            if (calValue == 0)
            {
                ResultDate = DateTime.Now.AddDays(7);
            }
            else if (calValue > 3)
            {
                ResultDate = DateTime.Now.AddDays(calValue);
            }
            else if (calValue <= 3 && calValue > 0)
            {
                ResultDate = DateTime.Now.AddDays(calValue + 7);
            }
            else if (calValue < 0)
            {
                int a = 7 - Math.Abs(calValue);
                if (Math.Abs(calValue) > 3)
                {
                    ResultDate = DateTime.Now.AddDays(7 + a);
                }
                else
                {
                    ResultDate = DateTime.Now.AddDays(a);
                }


            }
            return ResultDate;
        }
        public DayOfWeek WeekDay(string Value)
        {
            DayOfWeek result = default;
            Value = Value.ToLower();
            switch (Value)
            {
                case "monday":
                    result = DayOfWeek.Monday;
                    break;
                case "tuesday":
                    result = DayOfWeek.Tuesday;
                    break;
                case "wednesday":
                    result = DayOfWeek.Wednesday;
                    break;
                case "thursday":
                    result = DayOfWeek.Thursday;
                    break;
                case "friday":
                    result = DayOfWeek.Friday;
                    break;
                case "saturday":
                    result = DayOfWeek.Saturday;
                    break;
                case "sunday":
                    result = DayOfWeek.Sunday;
                    break;
            }
            return result;
        }


    }
    public class Loan
    {
        public int WeekNo { get; set; }
        public DateTime DueDate { get; set; }
        public int Amount { get; set; }
        public int Interest { get; set; }
        public int Total { get; set; }
        public Loan(int weekNo, DateTime date, int amount, int interest)
        {
            WeekNo = weekNo;
            DueDate = date;
            Amount = amount;
            Interest = interest;
            Total = amount + interest;
        }
    }
}
