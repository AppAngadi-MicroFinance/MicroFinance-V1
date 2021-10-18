using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroFinance.Modal;
using MicroFinance.ViewModel;

namespace MicroFinance.Repository
{
    public class CustomerRepository
    {

       

        public void AddCustomerDetails(string Region, string BranchName, string SelfHelpGroup, string PeerGroup,Customer CustData)
        {
            string CustomerID = string.Empty;
            int CustCount = 1;
            string BranchID=string.Empty;
            string AddressofCustomer = CustData.DoorNumber + "|~" + CustData.StreetName + "|~" + CustData.LocalityTown + "|~" + CustData.Taluk + "|~" + CustData.City + "|~" + CustData.State;
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = "select Count(CustID) from CustomerDetails";
                CustCount += (int)sqlCommand.ExecuteScalar();
                sqlCommand.CommandText = "select Bid from BranchDetails where BranchName='" + BranchName + "' and RegionName='" + Region + "'";
                BranchID = (string)sqlCommand.ExecuteScalar();
                CustomerID = GenerateCustomerID(BranchID, CustCount);
                //                sqlCommand.CommandText = @"insert into CustomerDetails(CustId, Name, Dob, Age, Mobile, Religion, Community, Education, FamilyMembers, EarningMembers, Occupation, MonthlyIncome, Address, Pincode, HousingType, HousingIndex,IsAddressProof, IsPhotoProof, IsProfilePhoto, GuarenteeStatus, NomineeStatus,IsActive,CustomerStatus,FatherName,MotherName,Gender,Caste,MonthlyExpenses,IsBankDetails,AadharNumber) 
                //values ('" + _customerId + "','" + CustomerName + "','" + DateofBirth.ToString("yyyy-MM-dd") + "','" + Age + "','" + ContactNumber + "','" + Religion + "','" + Community +
                //"','" + Education + "','" + FamilyMembers + "','" + EarningMembers + "','" + Occupation + "','" + MonthlyIncome + "','" + AddressofCustomer + "','" + Pincode + "','" +
                //HousingType + "','" + HousingIndex + "','" + false + "','" + false + "','" + false + "','" + false + "','" + false + "','" + false + "','" + 0 + "','" + FatherName + 
                //"','" + MotherName + "','" + Gender + "','" + Caste + "','" + MothlyExpenses + "','" + false + "','" + null + "')";

                sqlCommand.CommandText = "insert into CustomerDetails(CustId, Name, FatherName, MotherName, Dob, Age, Gender, Mobile,AadharNumber,Religion, Caste, Community,Education, FamilyMembers, EarningMembers, Occupation, MonthlyIncome, MonthlyExpenses, Address,Pincode, HousingType, IsBankDetails, IsAddressProof, IsPhotoProof, IsProfilePhoto, BankACHolderName, BankAccountNo, BankName,BankBranchName, IFSCCode, MICRCode, GuarenteeStatus, NomineeStatus, CustomerStatus, IsActive,HusbandName,YearlyIncome,IsCombinePhoto,PhotoProofName,PhotoProofNo,AddressProofName,AddressProofNo)values(@custId, @name, @fatherName, @motherName, @dob, @age, @gender, @mobile, @aadhar, @religion, @caste, @community, @education, @familyMembers,@earningMembers, @occupation, @monthlyIncome, @monthlyExpence, @address, @pincode, @houseType,  @isBankDetails,@isAddressProof, @isPhotoproof, @isProfilePhoto, @bankAccHolder, @bankAcNo, @banckName, @bankBranchName, @ifsc, @micr, @guarenteeStatus, @nomineeStatus, @customerStatus, @isActive,'" + CustData.HusbandName + "','" + CustData.YearlyIncome + "','" + false + "','" + CustData.NameofPhotoProof + "','" + CustData.PhotoProofNo + "','" + CustData.NameofAddressProof + "','" + CustData.AddressProofNo + "')";

                sqlCommand.Parameters.AddWithValue("@custId", CustomerID); sqlCommand.Parameters.AddWithValue("@name", CustData.CustomerName);
                sqlCommand.Parameters.AddWithValue("@fatherName", CustData.FatherName); sqlCommand.Parameters.AddWithValue("@motherName", CustData.MotherName);
                sqlCommand.Parameters.AddWithValue("@dob", CustData.DateofBirth.ToString("MM-dd-yyyy")); sqlCommand.Parameters.AddWithValue("@age", CustData.Age);

                sqlCommand.Parameters.AddWithValue("@gender", CustData.Gender); sqlCommand.Parameters.AddWithValue("@mobile", CustData.ContactNumber);
                sqlCommand.Parameters.AddWithValue("@aadhar", CustData.AadharNo); sqlCommand.Parameters.AddWithValue("@religion", CustData.Religion);
                sqlCommand.Parameters.AddWithValue("@caste", CustData.Caste); sqlCommand.Parameters.AddWithValue("@community", CustData.Community);

                sqlCommand.Parameters.AddWithValue("@education", CustData.Education); sqlCommand.Parameters.AddWithValue("@familyMembers", CustData.FamilyMembers);
                sqlCommand.Parameters.AddWithValue("@earningMembers", CustData.EarningMembers); sqlCommand.Parameters.AddWithValue("@occupation", CustData.Occupation);
                sqlCommand.Parameters.AddWithValue("@monthlyIncome", CustData.MonthlyIncome); sqlCommand.Parameters.AddWithValue("@monthlyExpence", CustData.MonthlyIncome);
                sqlCommand.Parameters.AddWithValue("@address", AddressofCustomer); sqlCommand.Parameters.AddWithValue("@pincode", CustData.Pincode);
                sqlCommand.Parameters.AddWithValue("@houseType", CustData.HousingType);

                //sqlCommand.Parameters.AddWithValue("@addressProofName", NameofAddressProof); sqlCommand.Parameters.AddWithValue("@photoProffName",NameofPhotoProof);
                sqlCommand.Parameters.AddWithValue("@isBankDetails", CustData.HavingBankDetails); sqlCommand.Parameters.AddWithValue("@isAddressProof", false);
                sqlCommand.Parameters.AddWithValue("@isPhotoproof", false); sqlCommand.Parameters.AddWithValue("@isProfilePhoto", false);
                sqlCommand.Parameters.AddWithValue("@bankAccHolder", CustData.AccountHolder); sqlCommand.Parameters.AddWithValue("@bankAcNo", CustData.AccountNumber);

                sqlCommand.Parameters.AddWithValue("@banckName", CustData.BankName); sqlCommand.Parameters.AddWithValue("@bankBranchName", CustData.BankBranchName);

                sqlCommand.Parameters.AddWithValue("@ifsc", CustData.IFSCCode); sqlCommand.Parameters.AddWithValue("@micr", CustData.MICRCode);

                //sqlCommand.Parameters.AddWithValue("@addressProof", Convertion(AddressProof)); sqlCommand.Parameters.AddWithValue("@photoProof", Convertion(PhotoProof));
                //sqlCommand.Parameters.AddWithValue("@profilePhoto", Convertion(ProfilePicture));

                sqlCommand.Parameters.AddWithValue("@guarenteeStatus", false);

                sqlCommand.Parameters.AddWithValue("@nomineeStatus", false); sqlCommand.Parameters.AddWithValue("@customerStatus", 1);
                sqlCommand.Parameters.AddWithValue("@isActive", true);

                if (sqlCommand.ExecuteNonQuery() == 1)
                    InsertIntoCustomerGroup(CustomerID, PeerGroup, CustData.IsLeader, GetMembersCountINPeerGroup(PeerGroup));
                

            }
        }

        void InsertIntoCustomerGroup(string custId, string pgId, bool isLeader, int cpId)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlConnection;
                cmd.CommandText = "insert into CustomerGroup (CustId, PeerGroupId, IsLeader, CPid)values(@custId, @pgId, @isLeader, @cPid)";
                cmd.Parameters.AddWithValue("@custId", custId);
                cmd.Parameters.AddWithValue("@pgId", pgId);
                cmd.Parameters.AddWithValue("@isLeader", isLeader);
                cmd.Parameters.AddWithValue("@cPid", cpId);
                cmd.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        int GetMembersCountINPeerGroup(string peerGroupId)
        {
            int Count = 1;
            using (SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = sqlConnection;
                cmd.CommandText = "select COUNT(CustId) from CustomerGroup where PeerGroupId = '" + peerGroupId + "'";
                Count += (int)cmd.ExecuteScalar();
                sqlConnection.Close();
            }
            return Count;
        }







        private static string GenerateCustomerID(string BranchId,int CustCount)
        {
            int count = CustCount;
            string Result = "";
            int year = DateTime.Now.Year;
            int mon = DateTime.Now.Month;
            string month = ((mon) < 10 ? "0" + mon : mon.ToString());
            string region = BranchId.Substring(0, 2);
            string branch = BranchId.Substring(8);
            Result = region + branch + year + month + ((count < 10) ? "0" + count : count.ToString());
            return Result;
        }


        public static List<CenterViewModel> GetCenters()
        {
            List<CenterViewModel> CenterList = new List<CenterViewModel>();
            using(SqlConnection sqlconn=new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select BranchId,SHGId,SHGName from SelfHelpGroup";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            CenterViewModel center = new CenterViewModel();
                            center.BranchId = reader.GetString(0);
                            center.CenterID = reader.GetString(1);
                            center.CenterName = reader.GetString(2);

                            CenterList.Add(center);
                        }
                    }
                }
                sqlconn.Close();
            }
            return CenterList;
        }

        public static List<CustomerViewModel> Customers(string CenterID)
        {
            List<CustomerViewModel> CustomerList = new List<CustomerViewModel>();
            List<string> CustomerIDList = new List<string>();
            string CenterName = "";
            using (SqlConnection sqlconn=new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustId from CustomerGroup where PeerGroupId in (select GroupId from PeerGroup where SHGid='"+CenterID+"')";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            CustomerIDList.Add(reader.GetString(0));                        
                        }
                    }
                    reader.Close();
                    sqlcomm.CommandText = "select SHGName from SelfHelpGroup where SHGId='"+CenterID+"'";
                    CenterName = (string)sqlcomm.ExecuteScalar();
                    foreach (string s in CustomerIDList)
                    {
                        CustomerViewModel Customer = new CustomerViewModel();
                        sqlcomm.CommandText = "select Name from CustomerDetails where CustId='" + s + "'";
                        Customer.CustomerName = (string)sqlcomm.ExecuteScalar();
                        Customer.CustomerID = s;
                        Customer.CenterId = CenterID;
                        Customer.CenterName = CenterName;
                        sqlcomm.CommandText = "select COUNT(LoanID) from LoanDetails where CustomerID='"+s+"' and IsActive=1";
                        int result = (int)sqlcomm.ExecuteScalar();
                        Customer.ActiveLoans = result;

                        CustomerList.Add(Customer);
                    }
                }
                sqlconn.Close();
            }
            return CustomerList;
        }
    }
}
