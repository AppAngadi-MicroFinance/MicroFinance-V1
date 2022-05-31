using MicroFinance.Modal;
using MicroFinance.ReportExports.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports.ReportTools
{
    public class POSReportTool
    {
        static string Connectionstring = Properties.Settings.Default.DBConnection;
        static Dictionary<string, Customer> customerdetails = new Dictionary<string, Customer>();
        public static Dictionary<string, string> branches;
        public static Dictionary<string, string> regions;

        public List<POSReport> POS_Report = new List<POSReport>();
        static LoanRepository LoanRepos;
        public POSReportTool()
        {
            LoanRepos = new LoanRepository(Connectionstring);
            branches = GetAllBranchNames();
            regions = GetAllRegion();
            customerdetails = GetALLCustomerDetail();
            POS_Report = GetPOSReport();
        }
        public List<ReportModel> Get_POSReport()
        {
            List<ReportModel> FinalData = new List<ReportModel>();
            foreach (POSReport item in POS_Report)
            {
                ReportModel model1 = new ReportModel();
                model1.Column_1 = item.BranchName;
                model1.Column_2 = item.Center;
                model1.Column_3 = item.CustId;
                model1.Column_4 = item.CustName;
                model1.Column_5 = item.AadhaarNumber;
                model1.Column_6 = item.AccountNo;
                model1.Column_7 = item.SamAccountNo;
                model1.Column_8 = item.DisbursementAmount.ToString();
                model1.Column_9 = item.DisbursementDate.ToShortDateString();
                model1.Column_10 = item.PrincipalRepaid.ToString();
                model1.Column_11 = item.InterestRepaid.ToString();
                model1.Column_12 = item.TotalRepaid.ToString();
                model1.Column_13 = item.LedgerBalance.ToString();
                
                FinalData.Add(model1);
            }
            return FinalData;
        }
        static List<POSReport> GetPOSReport()
        {
            List<POSReport> posreportlist = new List<POSReport>();
            List<LoanMetaDetail> Loandetails = GetAllLoanDetails();
            
            var samreport = GetDisbursementFromSamu();
            var collection = GetallLoanCollection();

            foreach (LoanMetaDetail loan in Loandetails)
            {
                POSReport posreport = new POSReport();
                posreport.CustId = loan.CustomerId;
                posreport.Center = LoanRepos.SHGNameDICT[LoanRepos.CustomerSHG_DICT[posreport.CustId]];
                posreport.CustName = customerdetails[loan.CustomerId].CustomerName;
                posreport.AadhaarNumber = customerdetails[loan.CustomerId].AadharNo;
                posreport.AccountNo = samreport.Where(temp => temp.GTLoanId == loan.LoanId).Select(temp => temp.GTLoanId).FirstOrDefault();
                posreport.SamAccountNo = samreport.Where(temp => temp.GTLoanId == loan.LoanId).Select(temp => temp.SamLoanAcNumber).FirstOrDefault();
                posreport.DisbursementAmount = loan.LoanAmount;
                posreport.DisbursementDate = loan.approveddate;
                posreport.PrincipalRepaid = collection.Where(temp => temp.LoanId == loan.LoanId).Sum(temp => temp.PrincipleAmount);
                posreport.InterestRepaid = collection.Where(temp => temp.LoanId == loan.LoanId).Sum(temp => temp.InterestAmount);
                posreport.TotalRepaid = posreport.PrincipalRepaid + posreport.InterestRepaid;
                posreport.LedgerBalance = loan.LoanAmount - posreport.PrincipalRepaid;

                string regionid = loan.CustomerId.Substring(0, 2);
                string branchid = loan.CustomerId.Substring(2, 3);

                if (regions.Keys.Contains(regionid))
                {
                    posreport.Region = regions[regionid];
                }
                if (branches.Keys.Contains(branchid))
                {
                    posreport.BranchName = branches[branchid];
                }
                posreportlist.Add(posreport);
            }
            return posreportlist;
        }
        
        public static List<LoanMetaDetail> GetAllLoanDetails()
        {
            List<LoanMetaDetail> loandetaillist = new List<LoanMetaDetail>();
            using (SqlConnection con = new SqlConnection(Connectionstring))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select loanid,customerid,loanamount,approvedate,isactive from loandetails ";
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    loandetaillist.Add(new LoanMetaDetail
                    {
                        LoanId = dr.GetString(0),
                        CustomerId = dr.GetString(1),
                        LoanAmount = dr.GetInt32(2),
                        approveddate = dr.GetDateTime(3),
                        IsActive = dr.GetBoolean(4)
                    });
                }
                dr.Close();
            }
            return loandetaillist;
        }
        public static List<DisbursementFromSamu> GetDisbursementFromSamu()
        {
            List<DisbursementFromSamu> disbursementfromsamulist = new List<DisbursementFromSamu>();
            using (SqlConnection con = new SqlConnection(Connectionstring))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select approveddate,loanacno,loanid,requestid from DisbursementFromSAMU where LoanID is not null";
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    disbursementfromsamulist.Add(new DisbursementFromSamu
                    {
                        ApproveDate = dr.GetDateTime(0),
                        SamLoanAcNumber = dr.GetString(1),
                        GTLoanId = dr.GetString(2),
                        RequestId = dr.GetString(3)
                    });
                }
                dr.Close();
            }
            return disbursementfromsamulist;
        }
        public static List<CollectionEntryModel> GetallLoanCollection()
        {
            List<CollectionEntryModel> collectionentrylist = new List<CollectionEntryModel>();
            using (SqlConnection con = new SqlConnection(Connectionstring))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select branchid,CustId,LoanId,Principal,Interest,Total,SecurityDeposite from LoanCollectionEntry";
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CollectionEntryModel obj = new CollectionEntryModel();
                    obj.OriginDetail.BranchId = dr.GetString(1);
                    obj.LoanId = dr.GetString(2);
                    obj.PrincipleAmount = dr.GetInt32(3);
                    obj.InterestAmount = dr.GetInt32(4);
                    obj.TotalAmount = dr.GetInt32(5);
                    obj.SecurityDeposite = dr.GetInt32(6);
                    collectionentrylist.Add(obj);
                }
                dr.Close();
            }
            return collectionentrylist;
        }

        public static Dictionary<string, Customer> GetALLCustomerDetail()
        {
            Dictionary<string, Customer> allcustomeridnames = new Dictionary<string, Customer>();
            using (SqlConnection con = new SqlConnection(Connectionstring))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select custid,name,aadharnumber from customerdetails";
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Customer custObj = new Customer();
                    custObj.CustomerId = dr.GetString(0);
                    custObj.CustomerName = dr.GetString(1);
                    custObj.AadharNo = dr.GetString(2);
                    if(!allcustomeridnames.Keys.Contains(custObj.CustomerId))
                        allcustomeridnames.Add(dr.GetString(0), custObj);
                }
                dr.Close();
            }
            return allcustomeridnames;
        }
        public static Dictionary<string, string> GetAllRegion()
        {
            Dictionary<string, string> regions = new Dictionary<string, string>();
            using (SqlConnection con = new SqlConnection(Connectionstring))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = " select SUBSTRING(regionid,2,len(regionid)),regionname from Region";
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    regions.Add(dr.GetString(0), dr.GetString(1));
                }
                dr.Close();
            }
            return regions;
        }
        public static Dictionary<string, string> GetAllBranchNames()
        {
            Dictionary<string, string> branches = new Dictionary<string, string>();
            using (SqlConnection con = new SqlConnection(Connectionstring))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                con.Open();
                cmd.CommandText = "select SUBSTRING(bid,9,len(bid)),branchname from BranchDetails";
                SqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    branches.Add(dr.GetString(0), dr.GetString(1));
                }
                dr.Close();
            }
            return branches;
        }
    }
}
