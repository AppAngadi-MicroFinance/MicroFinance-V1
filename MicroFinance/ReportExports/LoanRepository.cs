using MicroFinance.ReportExports.Models;
using MicroFinance.ViewModel;
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


        public Dictionary<string, MFOrigin> BranchDetailDICT = new Dictionary<string, MFOrigin>();
        Dictionary<string, LoanMetaModel> LoanMetaDict = new Dictionary<string, LoanMetaModel>(); //

        public Dictionary<string, string> RequestId_LoanID_DICT = new Dictionary<string, string>();

        public Dictionary<string, string> CustomerBranchDICT = new Dictionary<string, string>();
        public Dictionary<string, string> CustomerSHG_DICT = new Dictionary<string, string>();
        public Dictionary<string, string> CustomerNameDICT = new Dictionary<string, string>();
        public Dictionary<string, string> SHGNameDICT = new Dictionary<string, string>();
        public Dictionary<string, string> EmployeeNameDICT = new Dictionary<string, string>();
        public Dictionary<string, string> EmployeeBranchDICT = new Dictionary<string, string>();

        public Dictionary<string, int> LoanAmountDICT = new Dictionary<string, int>();
        public Dictionary<string, int> LoanPrincipleDICT = new Dictionary<string, int>();


        public LoanRepository(string globalConnectionString)
        {
            this.ConnectionString = globalConnectionString;
            GlobalConnection = new SqlConnection(this.ConnectionString);
            GlobalConnection.Open();
            cmd = new SqlCommand();
            cmd.Connection = GlobalConnection;

            LoadBranchDetials();
            LoadSHG_DICT();

            LoadCustomerDICT();
            LoanCustomer_BranchDICT();
            LoadCustomerSHG();

            LoadEmployee_DICT();
            LoanEmployeeBranch();

            LoadLoanID4RequestID();
            LoadLoanMetaData();
            LoadLoanAmount();
            LoadLoanPrincipleAmount();
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
                if(EmployeeNameDICT.ContainsKey(item.EmployeeId))
                {
                    item.EmployeeName = EmployeeNameDICT[item.EmployeeId];
                }
                
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

                item.LoanId = RequestId_LoanID_DICT[item.RequestId];
                if (item.LoanId != string.Empty)
                {
                    LoanMetaModel obj = LoanMetaDict[item.LoanId];
                    item.LoanApplicationStatus.ApprovedBy_EmpId = obj.ApprovedBy_EmpId;
                    item.LoanApplicationStatus.ApprovedBy_EmpName = obj.ApprovedBy_EmpName;
                    item.LoanApplicationStatus.ApprovedDate = obj.ApprovedDate;
                    item.LoanApplicationStatus.IsActive = obj.IsActive;
                }
            }
            return toReturn;
        }
        //
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

                item.OriginDetail.SHGId = CustomerSHG_DICT[item.CustomerId];
                item.OriginDetail.SHGName = SHGNameDICT[item.OriginDetail.SHGId];
                item.CollectedBy_EmpName = EmployeeNameDICT[item.CollectedBy_EmpID];
            }
            return toReturn;
        }
        public List<LoanApplicationModel> Get_AllHighmarkRejected(DateRange range)
        {
            List<LoanApplicationModel> toReturn = new List<LoanApplicationModel>();
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select LoanApplication.RequestId, LoanApplication.LoanAmount, LoanApplicationLog.TransactionDate, LoanApplication.CustId, LoanApplication.EmployeeId, LoanApplication.BranchId, LoanApplication.SHGId from LoanApplication,LoanApplicationLog where LoanApplicationLog.TransactionDate between '" + range.FromDate_String + "' and '" + range.ToDate_String + "' and  LoanApplicationLog.StatusCode = 4 and LoanApplicationLog.ApplicationID=LoanApplication.RequestId";
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
                if (CustomerNameDICT.ContainsKey(item.CustomerId) && EmployeeNameDICT.ContainsKey(item.EmployeeId) && BranchDetailDICT.ContainsKey(item.OriginDetail.BranchId))
                {
                    item.CustomerName = CustomerNameDICT[item.CustomerId];
                    item.EmployeeName = EmployeeNameDICT[item.EmployeeId];
                    string shgId = item.OriginDetail.SHGId;
                    item.OriginDetail = BranchDetailDICT[item.OriginDetail.BranchId];
                    item.OriginDetail.SHGId = shgId;
                    item.OriginDetail.SHGName = SHGNameDICT[item.OriginDetail.SHGId];
                }
            }
            return toReturn;
        }
        public List<LoanApplicationModel> Get_AllHighmarkApproved(DateRange range)
        {
            List<LoanApplicationModel> toReturn = new List<LoanApplicationModel>();
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select LoanApplication.RequestId, LoanApplication.LoanAmount, LoanApplicationLog.TransactionDate, LoanApplication.CustId, LoanApplication.EmployeeId, LoanApplication.BranchId, LoanApplication.SHGId from LoanApplication,LoanApplicationLog where LoanApplicationLog.TransactionDate between '" + range.FromDate_String + "' and '" + range.ToDate_String + "' and  LoanApplicationLog.StatusCode = 3 and LoanApplicationLog.ApplicationID=LoanApplication.RequestId";
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
            //
            foreach (LoanApplicationModel item in toReturn)
            {
                if(CustomerNameDICT.ContainsKey(item.CustomerId) && EmployeeNameDICT.ContainsKey(item.EmployeeId) && BranchDetailDICT.ContainsKey(item.OriginDetail.BranchId))
                {
                    item.CustomerName = CustomerNameDICT[item.CustomerId];
                    item.EmployeeName = EmployeeNameDICT[item.EmployeeId];
                    string shgId = item.OriginDetail.SHGId;
                    item.OriginDetail = BranchDetailDICT[item.OriginDetail.BranchId];
                    item.OriginDetail.SHGId = shgId;
                    item.OriginDetail.SHGName = SHGNameDICT[item.OriginDetail.SHGId];
                }
            }
            return toReturn;
        }
        //
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
                    item.OriginDetail.SHGId = CustomerSHG_DICT[item.CustomerId];
                    item.OriginDetail.SHGName = SHGNameDICT[item.OriginDetail.SHGId];

                    item.PrincipleAmount = LoanPrincipleDICT[item.LoanId];
                }
                catch (Exception ex) { }
            }
            return toReturn;
        }
        public List<LoanSummaryModel> Get_InActiveLoan(DateRange range)
        {
            List<LoanSummaryModel> toReturn = new List<LoanSummaryModel>();
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select LoanID, CustomerID, LoanAmount, ApproveDate from LoanDetails where IsActive = 0 and YEAR(ApproveDate) >= " + range.FromDate.Year + "";
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

                    item.OriginDetail.SHGId = CustomerSHG_DICT[item.CustomerId];
                    item.OriginDetail.SHGName = SHGNameDICT[item.OriginDetail.SHGId];

                    //item.PrincipleAmount = Get_PrincipleAmount(item.LoanId);
                    //item.WeeksPaid = GetPaymentCount(item.LoanId);

                    MultiStructure temp = Get_AccountClosedEmployeeAndDate(item.LoanId);
                    item.AccountClosedBY = temp.String1;
                    item.AccountClosedOn = temp.Date1;
                }
                catch (Exception ex) { }
            }
            return toReturn;
        }
        //
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

                obj.OriginDetail = BranchDetailDICT[obj.OriginDetail.BranchId];
                obj.CustomerName = CustomerNameDICT[obj.CustomerId];
                obj.CollectedBy_EmpName = EmployeeNameDICT[obj.CollectedBy_EmpID];
                toReturn.Add(obj);
            }
            dr.Close();

            foreach (CollectionEntryData item in toReturn)
            {
                try
                {
                    item.OriginDetail.SHGId = CustomerSHG_DICT[item.CustomerId];
                    item.OriginDetail.SHGName = SHGNameDICT[item.OriginDetail.SHGId];
                    item.LoanAmount = LoanAmountDICT[item.LoanId];
                }
                catch (Exception ex) { }
            }

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
        //
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
        public string Get_CollectionEmployeeFromOrigin(string loanID)
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select EmpId from TimeTable where SHGId = (select SHGId from CustomerGroup where CustId = (select CustomerID from LoanDetails where LoanID = '" + loanID + "'))";
            return cmd.ExecuteScalar().ToString();
        }
        
        MultiStructure Get_AccountClosedEmployeeAndDate(string loanID)
        {
            MultiStructure toReturn = new MultiStructure();
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select Top 1 CollectedBy, CollectedOn from LoanCollectionEntry where LoanId = '" + loanID + "' order by Balance";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                toReturn.String1 = dr.GetString(0);
                toReturn.Date1 = dr.GetDateTime(1);
                break;
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
        DateTime Get_LastEntryDate(string loanId)
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

        void LoadLoanID4RequestID()
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select distinct RequestID, LoanID from DisbursementFromSAMU";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                try
                {
                    RequestId_LoanID_DICT.Add(dr.GetString(0), dr.GetString(1));
                }
                catch (Exception ex) { }
            }
            dr.Close();
        }
        void LoadLoanPrincipleAmount()
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select Distinct LoanId, Principal from LoanCollectionMaster where LoanId in (select LoanId from LoanDetails where IsActive = 1)";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LoanPrincipleDICT.Add(dr.GetString(0), dr.GetInt32(1));
            }
            dr.Close();
        }
        void LoadLoanAmount()
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select distinct LoanID, LoanAmount from LoanDetails where IsActive = 1";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LoanAmountDICT.Add(dr.GetString(0), dr.GetInt32(1));
            }
            dr.Close();
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
        void LoanEmployeeBranch()
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select distinct TimeTable.EmpId, SelfHelpGroup.BranchId from TimeTable, SelfHelpGroup where TimeTable.SHGId = SelfHelpGroup.SHGId";
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                EmployeeBranchDICT.Add(dr.GetString(0), dr.GetString(1));
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
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select CustomerGroup.CustId, SelfHelpGroup.Branchid from CustomerGroup, SelfHelpGroup where CustomerGroup.SHGID = SelfHelpGroup.SHGID";
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                CustomerBranchDICT.Add(dr.GetString(0), dr.GetString(1));
            }
            dr.Close();
        }
        void LoadCustomerSHG()
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select distinct CustomerGroup.CustId,CustomerGroup.SHGID from CustomerGroup, SelfHelpGroup where CustomerGroup.SHGID = SelfHelpGroup.SHGID";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                CustomerSHG_DICT.Add(dr.GetString(0), dr.GetString(1));
            }
            dr.Close();
        }

        void LoadLoanMetaData()
        {
            cmd.CommandText = string.Empty;
            cmd.CommandText = "select distinct LoanID, LoanAmount, ApprovedBy, ApproveDate, IsActive from LoanDetails where IsActive = 1";
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                LoanMetaModel obj = new LoanMetaModel();
                obj.LoanId = dr.GetString(0);
                obj.LoanAmount = dr.GetInt32(1);
                obj.ApprovedBy_EmpId = dr.GetString(2);
                obj.ApprovedBy_EmpName = EmployeeNameDICT[obj.ApprovedBy_EmpId];
                obj.ApprovedDate = dr.GetDateTime(3);
                obj.IsActive = dr.GetBoolean(4);
                try
                {
                    LoanMetaDict.Add(obj.LoanId, obj);
                }
                catch (Exception ex) { }
            }
            dr.Close();
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

    public class MultiStructure
    {
        public string String1 { get; set; }
        public string String2 { get; set; }
        public DateTime Date1 { get; set; }
    }
}
