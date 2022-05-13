using MicroFinance.ReportExports.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports
{
    public class LoanRepository
    {
        //string ConnectionString = "Data Source=.;Initial Catalog=db_a5d7b5_microfinance;Integrated Security=True";
        string ConnectionString = string.Empty;
        const string EmptyMasterMessage = "MasterList is Empty. Please load MasterList First";

        SqlConnection GlobalConnection;
        SqlCommand cmd;

        public List<LoanApplicationModel> LoanMetaMaster = new List<LoanApplicationModel>();
        public Dictionary<string, MFOrigin> BranchDetailDICT = new Dictionary<string, MFOrigin>();
        public Dictionary<string, string> CustomerBranchDICT = new Dictionary<string, string>();

        Dictionary<string, LoanRepoModel> LoanReopDICT = new Dictionary<string, LoanRepoModel>();

        public Dictionary<string, string> CustomerNameDICT = new Dictionary<string, string>();
        public Dictionary<string, string> SHGNameDICT = new Dictionary<string, string>();
        public Dictionary<string, string> EmployeeNameDICT = new Dictionary<string, string>();
        public Dictionary<string, string> RequestIdLoanIdDICT = new Dictionary<string, string>();
        public Dictionary<string, string> LoanDetailsDICT = new Dictionary<string, string>();


        public LoanRepository(string globalConnectionString)
        {
            this.ConnectionString = globalConnectionString;
            GlobalConnection = new SqlConnection(this.ConnectionString);
            GlobalConnection.Open();
            cmd = new SqlCommand();
            cmd.Connection = GlobalConnection;

            LoadBranchDetials();
            LoadCustomerDICT();
            LoanCustomer_BranchDICT();
            LoadSHG_DICT();
            LoadEmployee_DICT();
        }


        public LoanRepository(string connectionstring, DateRange range)
        {
            GlobalConnection = new SqlConnection(this.ConnectionString);
            GlobalConnection.Open();
            cmd = new SqlCommand();
            cmd.Connection = GlobalConnection;

            LoadBranchDetials();
            LoadCustomerDICT();
            LoanCustomer_BranchDICT();
            LoadSHG_DICT();
            LoadEmployee_DICT();

            this.LoanMetaMaster = Get_AllApprovedLoans(range);
        }

        void LoanDetail()
        {
            List<string> loanIdList = LoadLoanRequest();
            foreach (string loanId in loanIdList)
            {
                cmd.CommandText = "select LoanID, ApprovedBy, ApproveDate, IsActive from LoanDetails where LoanID = '" + loanId + "'";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    LoanRepoModel obj = new LoanRepoModel();
                    obj.LoanId = dr.GetString(0);
                    obj.ApprovedBy_EmpId = dr.GetString(1);
                    obj.ApprovedBy_EmpName = EmployeeNameDICT[obj.ApprovedBy_EmpId];
                    obj.ApprovedDate = dr.GetDateTime(2);
                    obj.IsActive = dr.GetBoolean(3);
                    try
                    {
                        LoanReopDICT.Add(loanId, obj);
                    }
                    catch (Exception ex) { }
                }
                dr.Close();
            }
        }
        public List<LoanApplicationModel> Get_AllLoanApplications(DateRange range)
        {
            List<LoanApplicationModel> toReturn = new List<LoanApplicationModel>();
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select BranchId, SHGId, EmployeeId, RequestId, CustId, EnrollDate from LoanApplication where EnrollDate between '" + range.FromDate_String + "' and '" + range.ToDate_String + "'";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LoanApplicationModel obj = new LoanApplicationModel();
                obj.OriginDetail.BranchId = dr.GetString(0);
                obj.OriginDetail.SHGId = dr.GetString(1);
                obj.EmployeeId = dr.GetString(2);
                obj.RequestId = dr.GetString(3);
                obj.CustomerId = dr.GetString(4);
                obj.RequestedDate = dr.GetDateTime(5);
                toReturn.Add(obj);
            }
            dr.Close();

            foreach (LoanApplicationModel item in toReturn)
            {
                MFOrigin branchOrigin = BranchDetailDICT[item.OriginDetail.BranchId];
                item.OriginDetail.BranchName = branchOrigin.BranchName;
                item.OriginDetail.RegionId = branchOrigin.RegionId;
                item.OriginDetail.RegionName = branchOrigin.RegionName;

                item.EmployeeName = EmployeeNameDICT[item.EmployeeId];
                item.OriginDetail.SHGName = SHGNameDICT[item.OriginDetail.SHGId];
                item.CustomerName = CustomerNameDICT[item.CustomerId];
            }
            return toReturn;
        }
        public List<LoanApplicationModel> Get_AllApprovedLoans(DateRange range)
        {
            List<LoanApplicationModel> toReturn = new List<LoanApplicationModel>();
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select BranchId, SHGId, EmployeeId, CustId, RequestId, EnrollDate, LoanStatus, LoanType, Purpose, LoanAmount from LoanApplication where LoanStatus = 14 and EnrollDate between '" + range.FromDate_String + "' and '" + range.ToDate_String + "'";
            SqlDataReader dr1 = cmd.ExecuteReader();
            while (dr1.Read())
            {
                LoanApplicationModel obj1 = new LoanApplicationModel();
                obj1.OriginDetail.BranchId = dr1.GetString(0);
                obj1.OriginDetail.SHGId = dr1.GetString(1);
                obj1.EmployeeId = dr1.GetString(2);
                obj1.CustomerId = dr1.GetString(3);

                obj1.RequestId = dr1.GetString(4);

                obj1.RequestedDate = dr1.GetDateTime(5);
                obj1.LoanStatus = dr1.GetInt32(6);

                obj1.LoanType = dr1.GetString(7);
                obj1.LoanPurpose = dr1.GetString(8);
                obj1.LoanAmount = dr1.GetInt32(9);
                toReturn.Add(obj1);
            }
            dr1.Close();

            foreach (LoanApplicationModel item in toReturn)
            {
                MFOrigin branchOrigin = BranchDetailDICT[item.OriginDetail.BranchId];
                item.OriginDetail.BranchName = branchOrigin.BranchName;
                item.OriginDetail.RegionId = branchOrigin.RegionId;
                item.OriginDetail.RegionName = branchOrigin.RegionName;

                item.EmployeeName = EmployeeNameDICT[item.EmployeeId];
                item.OriginDetail.SHGName = SHGNameDICT[item.OriginDetail.SHGId];
                item.CustomerName = CustomerNameDICT[item.CustomerId];

                item.LoanId = Get_LoanId_4RequestId(item.RequestId);
                if (item.LoanId != string.Empty)
                {
                    LoanRepoModel obj = LoanReopDICT[item.LoanId];
                    item.LoanApplicationStatus.ApprovedBy_EmpId = obj.ApprovedBy_EmpId;
                    item.LoanApplicationStatus.ApprovedBy_EmpName = obj.ApprovedBy_EmpName;
                    item.LoanApplicationStatus.ApprovedDate = obj.ApprovedDate;
                    item.LoanApplicationStatus.IsActive = obj.IsActive;
                }
            }
            return toReturn;
        }

        public List<LoanApplicationModel> Get_AllLoanDisClosed(DateRange range)
        {
            List<LoanApplicationModel> toReturn = new List<LoanApplicationModel>();
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select LoanID, CustomerID, LoanPeriod from LoanDetails where IsActive = 0";
            SqlDataReader dr = cmd.ExecuteReader();
            int LoanPeriod = 0;
            while (dr.Read())
            {
                LoanApplicationModel obj = new LoanApplicationModel();
                obj.LoanId = dr.GetString(0);
                obj.CustomerId = dr.GetString(1);
                LoanPeriod = dr.GetInt32(2);
            }
            dr.Close();

            foreach (LoanApplicationModel item in toReturn)
            {
                int collectionCount = Get_LoanCollectionEntryCount(item.LoanId);
                if (collectionCount == LoanPeriod)
                {
                    item.DisClosedDate = Get_LastEntryDate(item.LoanId);
                }
            }

            foreach (LoanApplicationModel item in toReturn)
            {
                item.OriginDetail.BranchId = CustomerBranchDICT[item.CustomerId];
                MFOrigin obj = BranchDetailDICT[item.OriginDetail.BranchId];
                item.OriginDetail.BranchName = obj.BranchName;
                item.OriginDetail.RegionId = obj.RegionId;
                item.OriginDetail.RegionName = obj.RegionName;
            }
            return toReturn;
        }

        public List<CollectionEntryModel> Get_AllCollectionEntries(DateRange range)
        {
            List<CollectionEntryModel> toReturn = new List<CollectionEntryModel>();
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select BranchId, CustId, LoanId, Principal, Interest, ActualDue, PaidDue, ActualPaymentDate, CollectedOn, Attendance, CollectedBy from LoanCollectionEntry where CollectedOn between '" + range.FromDate_String + "' and '" + range.ToDate_String + "'";
            SqlDataReader dr = cmd.ExecuteReader();

            while (dr.Read())
            {
                CollectionEntryModel obj = new CollectionEntryModel();
                obj.OriginDetail.BranchId = dr.GetString(0);
                obj.CustomerId = dr.GetString(1);
                obj.LoanId = dr.GetString(2);
                obj.PrincipleAmount = dr.GetInt32(3);
                obj.InterestAmount = dr.GetInt32(4);
                obj.ActualDue = dr.GetInt32(5);
                obj.PaidDue = dr.GetInt32(6);
                obj.ActualPaymentDate = dr.GetDateTime(7);
                obj.CollectedDate = dr.GetDateTime(8);

                int isPresent = dr.GetInt32(9);
                obj.IsPresent = isPresent > 0 ? true : false;
                obj.CollectedBy_EmpID = dr.GetString(10);

                toReturn.Add(obj);
            }
            dr.Close();
            foreach (CollectionEntryModel item in toReturn)
            {
                MFOrigin branchData = BranchDetailDICT[item.OriginDetail.BranchId];
                item.OriginDetail.BranchName = branchData.BranchName;

                item.OriginDetail.RegionId = branchData.RegionId;
                item.OriginDetail.RegionName = branchData.RegionName;

                item.OriginDetail.SHGId = Get_SHGID_4CustomerId(item.CustomerId);//
                item.OriginDetail.SHGName = SHGNameDICT[item.OriginDetail.SHGId];
                item.CollectedBy_EmpName = EmployeeNameDICT[item.CollectedBy_EmpID];
            }

            return toReturn;
        }
        public List<LoanApplicationModel> Get_AllHighmarkRejected(DateRange range)
        {
            List<LoanApplicationModel> toReturn = new List<LoanApplicationModel>();
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select RequestId, LoanAmount, EnrollDate, CustId, EmployeeId, BranchId, SHGId from LoanApplication where LoanStatus = 3 and EnrollDate between '" + range.FromDate + "' and '" + range.ToDate + "'";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LoanApplicationModel obj = new LoanApplicationModel();
                obj.RequestId = dr.GetString(0);
                obj.LoanAmount = dr.GetInt32(1);
                obj.RequestedDate = dr.GetDateTime(2);
                obj.CustomerId = dr.GetString(3);
                obj.EmployeeId = dr.GetString(4);
                obj.OriginDetail.BranchId = dr.GetString(5);
                obj.OriginDetail.SHGId = dr.GetString(6);
                toReturn.Add(obj);
            }
            dr.Close();

            foreach (LoanApplicationModel item in toReturn)
            {
                item.CustomerName = CustomerNameDICT[item.CustomerId];
                item.EmployeeName = EmployeeNameDICT[item.EmployeeId];
                MFOrigin obj = BranchDetailDICT[item.OriginDetail.BranchId];
                item.OriginDetail.BranchName = obj.BranchName;
                item.OriginDetail.RegionName = obj.RegionName;
            }
            return toReturn;
        }
        public List<LoanApplicationModel> Get_AllHighmarkApproved(DateRange range)
        {
            List<LoanApplicationModel> toReturn = new List<LoanApplicationModel>();
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select RequestId, LoanAmount, EnrollDate, CustId, EmployeeId, BranchId, SHGId from LoanApplication where LoanStatus > 3 and EnrollDate between '" + range.FromDate + "' and '" + range.ToDate + "'";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LoanApplicationModel obj = new LoanApplicationModel();
                obj.RequestId = dr.GetString(0);
                obj.LoanAmount = dr.GetInt32(1);
                obj.RequestedDate = dr.GetDateTime(2);
                obj.CustomerId = dr.GetString(3);
                obj.EmployeeId = dr.GetString(4);
                obj.OriginDetail.BranchId = dr.GetString(5);
                obj.OriginDetail.SHGId = dr.GetString(6);
                toReturn.Add(obj);
            }
            dr.Close();
            return toReturn;
        }

        public List<LoanSummaryModel> Get_LoanSummaryDetails(DateRange range)
        {
            List<LoanSummaryModel> toReturn = new List<LoanSummaryModel>();
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select LoanID, CustomerID, LoanAmount, ApproveDate from LoanDetails where IsActive = 1 and ApproveDate >= '" + range.FromDate_String + "'";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LoanSummaryModel obj = new LoanSummaryModel();
                obj.LoanId = dr.GetString(0);
                obj.CustomerId = dr.GetString(1);
                obj.CustomerName = CustomerNameDICT[obj.CustomerId];
                obj.LoanAmount = dr.GetInt32(2);
                obj.ApprovedDate = dr.GetDateTime(3);
                toReturn.Add(obj);
            }
            dr.Close();

            foreach (LoanSummaryModel item in toReturn)
            {
                try
                {
                    try { item.OriginDetail = BranchDetailDICT[CustomerBranchDICT[item.CustomerId]]; } catch (Exception ex) { }

                    item.OriginDetail.SHGId = Get_SHGID_4CustomerId(item.CustomerId);
                    item.PrincipleAmount = Get_PrincipleAmount(item.LoanId);
                    item.OriginDetail.SHGName = SHGNameDICT[item.OriginDetail.SHGId];
                }
                catch (Exception ex) { }
            }
            return toReturn;
        }
        public List<LoanSummaryModel> Get_InActiveLoan(DateRange range)
        {
            List<LoanSummaryModel> toReturn = new List<LoanSummaryModel>();
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select LoanID, CustomerID, LoanAmount, ApproveDate from LoanDetails where IsActive = 1 and YEAR(ApproveDate) >= " + range.FromDate.Year + "";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LoanSummaryModel obj = new LoanSummaryModel();
                obj.LoanId = dr.GetString(0);
                obj.CustomerId = dr.GetString(1);
                obj.CustomerName = CustomerNameDICT[obj.CustomerId];
                obj.LoanAmount = dr.GetInt32(2);
                obj.ApprovedDate = dr.GetDateTime(3);
                toReturn.Add(obj);
            }
            dr.Close();

            foreach (LoanSummaryModel item in toReturn)
            {
                try
                {
                    try { item.OriginDetail = BranchDetailDICT[CustomerBranchDICT[item.CustomerId]]; } catch (Exception ex) { }

                    item.OriginDetail.SHGId = Get_SHGID_4CustomerId(item.CustomerId);
                    item.PrincipleAmount = Get_PrincipleAmount(item.LoanId);
                    item.OriginDetail.SHGName = SHGNameDICT[item.OriginDetail.SHGId];

                    item.WeeksPaid = GetPaymentCount(item.LoanId);
                    item.AccountCloseOn = GetAccountCloseDate(item.LoanId);
                    item.AccountClosedBY = Get_AccountClosedEmployee(item.LoanId);
                }
                catch (Exception ex) { }
            }
            return toReturn;
        }

        int GetPaymentCount(string loanID)
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select COUNT(*) from LoanCollectionEntry where LoanId = '" + loanID + "'";
            int res = (int)cmd.ExecuteScalar();
            return res;
        }
        DateTime GetAccountCloseDate(string loanID)
        {
            DateTime toReturn = new DateTime();
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select Top 1 CollectedOn from LoanCollectionEntry where LoanId = '" + loanID + "' order by Balance";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                toReturn = dr.GetDateTime(0);
                break;
            }
            dr.Close();
            return toReturn;
        }
        string Get_AccountClosedEmployee(string loanID)
        {
            string toReturn = string.Empty;
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select Top 1 CollectedBy from LoanCollectionEntry where LoanId = '" + loanID + "' order by Balance";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                toReturn = dr.GetString(0);
                break;
            }
            dr.Close();
            return toReturn;
        }

        public List<LoanCollectionEntryModel> Get_LoanCollectionEnties(string loanId)
        {
            List<LoanCollectionEntryModel> toReturn = new List<LoanCollectionEntryModel>();
            cmd.CommandText = "select Principal, CollectedOn, CollectedBy from LoanCollectionEntry where LoanId = '" + loanId + "'";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LoanCollectionEntryModel item = new LoanCollectionEntryModel();
                item.PrincipleAmount = dr.GetInt32(0);
                item.CollectedOn = dr.GetDateTime(1);
                item.CollectedBy_EmpID = dr.GetString(2);
                item.CollectedBy_EmpName = EmployeeNameDICT[item.CollectedBy_EmpID];
                toReturn.Add(item);
            }
            dr.Close();
            return toReturn;
        }
        public List<CollectionEntryData> Get_CollectionEntry(DateRange range)
        {
            List<CollectionEntryData> toReturn = new List<CollectionEntryData>();
            cmd.CommandText = "select BranchId, CustId, LoanId, Principal, CollectedOn, CollectedBy from LoanCollectionEntry where CollectedOn > '" + range.FromDate_String + "' and CollectedOn <= '" + range.ToDate_String + "'";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                CollectionEntryData obj = new CollectionEntryData();
                obj.OriginDetail.BranchId = dr.GetString(0);
                obj.CustomerId = dr.GetString(1);
                obj.LoanId = dr.GetString(2);
                obj.PrincipleAmount = dr.GetInt32(3);
                obj.CollectedOn = dr.GetDateTime(4);
                obj.CollectedBy_EmpID = dr.GetString(5);

                obj.OriginDetail.BranchName = BranchDetailDICT[obj.OriginDetail.BranchId].BranchName;
                obj.CustomerName = CustomerNameDICT[obj.CustomerId];
                obj.CollectedBy_EmpName = EmployeeNameDICT[obj.CollectedBy_EmpID];
                toReturn.Add(obj);
            }
            dr.Close();

            foreach (CollectionEntryData item in toReturn)
            {
                try
                {
                    item.OriginDetail.RegionId = BranchDetailDICT[item.OriginDetail.BranchId].RegionId;
                    item.OriginDetail.RegionName = BranchDetailDICT[item.OriginDetail.BranchId].RegionName;
                    item.OriginDetail.SHGId = Get_SHGID_4CustomerId(item.CustomerId);
                    item.OriginDetail.SHGName = SHGNameDICT[item.OriginDetail.SHGId];
                }
                catch (Exception ex) { }
            }

            foreach (CollectionEntryData item in toReturn)
                item.LoanAmount = Get_LoanAmount(item.LoanId);

            return toReturn;
        }
        public Dictionary<string, List<string>> Get_AllApprovedLoanId(DateRange range)
        {
            Dictionary<string, List<string>> toReturn = new Dictionary<string, List<string>>();
            List<string> approvedLoanID = new List<string>();
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select LoanID from LoanDetails where IsActive = 1 and ApproveDate > '" + range.FromDate + "' and ApproveDate <= '" + range.ToDate + "'";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                approvedLoanID.Add(dr.GetString(0));
            }
            dr.Close();

            // LoanId => EmployeeId
            foreach (string loanId in approvedLoanID)
            {
                List<string> empList = Get_CollectionEmployee(loanId);
                if (empList.Count > 0)
                {
                    toReturn.Add(loanId, empList);
                }
                else
                {
                    string emp = Get_CollectionEmployeeFromOrigin(loanId);
                    toReturn.Add(loanId, new List<string>() { emp });
                }
            }
            return toReturn;
        }
        public string Get_CollectionEmployeeFromOrigin(string loanID)
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select EmpId from TimeTable where SHGId = (select SHGId from CustomerGroup where CustId = (select CustomerID from LoanDetails where LoanID = '" + loanID + "'))";
            return cmd.ExecuteScalar().ToString();
        }
        public List<string> Get_CollectionEmployee(string loanId)
        {
            List<string> toReturn = new List<string>();
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select distinct CollectedBy from LoanCollectionEntry where LoanId = '" + loanId + "'";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                toReturn.Add(dr.GetString(0));
            }
            dr.Close();
            return toReturn;
        }


        public int Get_LoanCollectionEntryCount(string loanId)
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = " select COUNT(*) from LoanCollectionEntry where LoanId = '" + loanId + "'";
            int res = (int)cmd.ExecuteScalar();
            return res;
        }
        public int Get_PrincipleAmount(string loanId)
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select distinct(Principal) from LoanCollectionMaster where LoanId = '" + loanId + "'";
            int res = (int)cmd.ExecuteScalar();
            return res;
        }
        public DateTime Get_LastEntryDate(string loanId)
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select top 1 CollectedOn from LoanCollectionEntry where LoanId = '" + loanId + "' order by Balance";
            SqlDataReader dr = cmd.ExecuteReader();
            DateTime dt = new DateTime();
            while (dr.Read())
            {
                dt = dr.GetDateTime(0);
            }
            return dt;
        }
        public string Get_CollectionEntryEmployee(string loanId)
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select top 1 CollectedBy from LoanCollectionEntry where LoanId = '" + loanId + "' order by Balance";
            SqlDataReader dr = cmd.ExecuteReader();
            string res = string.Empty;
            while (dr.Read())
            {
                res = dr.GetString(0);
            }
            return res;
        }
        public string Get_RegionID(string branchId)
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select RegionId from BranchDetails where Bid = '" + branchId + "'";
            string regionId = (string)cmd.ExecuteScalar();
            return regionId;
        }
        public string Get_RegionName(string regionId)
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select RegionName from BranchDetails where RegionId = '" + regionId + "'";
            string regionName = (string)cmd.ExecuteScalar();
            return regionName;
        }
        public string Get_BranchName(string branchId)
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select BranchName from BranchDetails where Bid = '" + branchId + "'";
            string branchName = (string)cmd.ExecuteScalar();
            return branchName;
        }
        public string Get_SHGName(string shgId)
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select SHGName from SelfHelpGroup where SHGId = '" + shgId + "'";
            string SHGName = (string)cmd.ExecuteScalar();
            return SHGName;
        }
        public string Get_EmployeeName(string employeeId)
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select Name from Employee where EmpId = '" + employeeId + "'";
            string employeeName = (string)cmd.ExecuteScalar();
            return employeeName;
        }
        public string Get_CustomerName(string customerId)
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select Name from CustomerDetails where CustId = '" + customerId + "'";
            string customerName = (string)cmd.ExecuteScalar();
            return customerName;
        }


        public int Get_LoanAmount(string loanId)
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select LoanAmount from LoanDetails where LoanID = '" + loanId + "'";
            int loanAmount = (int)cmd.ExecuteScalar();
            return loanAmount;
        }
        public string Get_LoanId_4RequestId(string requestId)
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select LoanID from DisbursementFromSAMU where RequestID = '" + requestId + "'";
            var res = cmd.ExecuteScalar();
            string loanID = string.Empty;

            if (res != null)
                loanID = res.ToString();
            return loanID;
        }
        List<string> LoadLoanRequest()
        {
            List<string> toReturn = new List<string>();
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select RequestID, LoanID from DisbursementFromSAMU";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                try
                {
                    RequestIdLoanIdDICT.Add(dr.GetString(0), dr.GetString(1));
                    toReturn.Add(dr.GetString(1));
                }
                catch (Exception ex) { }
            }
            dr.Close();
            return toReturn;
        }
        void LoadBranchDetials()
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select Bid, BranchName, RegionId, RegionName from BranchDetails";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                MFOrigin obj = new MFOrigin();
                obj.BranchId = dr.GetString(0);
                obj.BranchName = dr.GetString(1);
                obj.RegionId = dr.GetString(2);
                obj.RegionName = dr.GetString(3);
                BranchDetailDICT.Add(obj.BranchId, obj);
            }
            dr.Close();
        }


        void LoadCustomerDICT()
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select CustId, Name from CustomerDetails where CustId in (select CustId from CustomerGroup)";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                try
                {
                    CustomerNameDICT.Add(dr.GetString(0), dr.GetString(1));
                }
                catch (Exception ex) { }
            }
            dr.Close();
        }
        void LoadSHG_DICT()
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select SHGId, SHGName from SelfHelpGroup";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                SHGNameDICT.Add(dr.GetString(0), dr.GetString(1));
            }
            dr.Close();
        }
        void LoadEmployee_DICT()
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select EmpId, Name from Employee";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                EmployeeNameDICT.Add(dr.GetString(0), dr.GetString(1));
            }
            dr.Close();
        }
        void LoanCustomer_BranchDICT()
        {
            foreach (string custId in CustomerNameDICT.Keys.ToArray())
            {
                string cBranch = Get_CustomerBranch(custId);
                try
                {

                    CustomerBranchDICT.Add(custId, BranchDetailDICT[cBranch].BranchId);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public string Get_CustomerBranch(string customerId)
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select BranchId from SelfHelpGroup where SHGId = (select SHGID from CustomerGroup where CustId = '" + customerId + "')";
            return cmd.ExecuteScalar().ToString();
        }

        
        public string Get_EmployeeBranch(string empId)
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select distinct BranchId from SelfHelpGroup where SHGId in (select SHGId from TimeTable where EmpId = '" + empId + "')";
            return cmd.ExecuteScalar().ToString();
        }

        public string Get_SHGID_4CustomerId(string customerId)
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select SHGID from CustomerGroup where CustId = '" + customerId + "'";
            string SHGName = (string)cmd.ExecuteScalar();
            return SHGName;
        }
        public List<DateTime> Get_MonthPeriods(DateRange range)
        {
            List<DateTime> toReturn = new List<DateTime>();
            DateTime startRange = range.FromDate;

            DateTime toCompare = new DateTime(range.ToDate.Year, range.ToDate.Month, range.ToDate.Day);
            while (startRange < toCompare)
            {
                startRange = startRange.AddMonths(1);
                toReturn.Add(startRange);
            }
            return toReturn;
        }
    }
}
