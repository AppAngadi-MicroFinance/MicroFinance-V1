using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ViewModel
{
    public class CollectionReportRepo
    {


        //public static List<CollectionReportView> GetCollectionDetails(string branchID,string GroupID,DateTime CollectionDate)
        //{
        //    List<CollectionReportView> CollectionDetialsList = new List<CollectionReportView>();
        //    List<CollectionDetails> Details = new List<CollectionDetails>();
        //    using(SqlConnection sqlconn=new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
        //    {
        //        sqlconn.Open();
        //        if(ConnectionState.Open==sqlconn.State)
        //        {
        //            SqlCommand sqlcomm = new SqlCommand();
        //            sqlcomm.Connection = sqlconn;
        //            sqlcomm.CommandText = "select CustomerGroup.CustId,LoanCollectionEntry.BranchId,LoanCollectionEntry.Total,LoanCollectionEntry.CollectedBy from LoanCollectionEntry,CustomerGroup where CollectedOn='"+CollectionDate+"' and LoanCollectionEntry.CustId=CustomerGroup.CustId and CustomerGroup.PeerGroupId in(select GroupId from PeerGroup where SHGid='" + GroupID + "')and LoanCollectionEntry.BranchId='"+branchID+"'";
        //            SqlDataReader reader = sqlcomm.ExecuteReader();
        //            if(reader.HasRows)
        //            {
        //                while(reader.Read())
        //                {
        //                    Details.Add(new CollectionDetails { CustID = reader.GetString(0), BranchId = reader.GetString(1), Total = reader.GetInt32(2), EmpId = reader.GetString(3) });
        //                }
        //            }
        //            reader.Close();
                    
        //            foreach(CollectionDetails CD in Details)
        //            {
        //                CollectionReportView CollectionData = new CollectionReportView();
        //                sqlcomm.CommandText = "select BranchName from BranchDetails where Bid='" + CD.BranchId + "'";
        //                CollectionData.BranchName = (string)sqlcomm.ExecuteScalar();
        //                sqlcomm.CommandText = "select Name from Employee where EmpId = '"+CD.EmpId+"'";
        //                CollectionData.FoName = (string)sqlcomm.ExecuteScalar();
        //                sqlcomm.CommandText = "select Name from CustomerDetails where CustId='" + CD.CustID + "'";
        //                CollectionData.CustomerName = (string)sqlcomm.ExecuteScalar();
        //                CollectionData.Amount = CD.Total;
        //                sqlcomm.CommandText = "select SHGName from SelfHelpGroup where SHGId=(select SHGid from PeerGroup where GroupId=(select peerGroupId from CustomerGroup where CustId='"+CD.CustID+"'))";
        //                CollectionData.CenterName = (string)sqlcomm.ExecuteScalar();

        //                CollectionDetialsList.Add(CollectionData);
        //            }
        //            sqlconn.Close();
        //        }
        //        return CollectionDetialsList;
        //    }


        //}

        public static List<BranchNameView> GetBranchNames()
        {
            List<BranchNameView> BranchList = new List<BranchNameView>();
            using (SqlConnection sqlconn=new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select BranchName,Bid from BranchDetails";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            BranchList.Add(new BranchNameView { BranchName = reader.GetString(0), BranchId = reader.GetString(1) });
                        }
                    }
                    reader.Close();
                    sqlconn.Close();
                }
            }
            return BranchList;
        }


        public static List<CenterNameView> GetCenters()
        {
            List<CenterNameView> CenterList = new List<CenterNameView>();
            using (SqlConnection sqlconn=new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select SelfHelpGroup.BranchId,SelfHelpGroup.SHGId,selfHelpGroup.SHGName,TimeTable.EmpId from SelfHelpGroup,TimeTable where SelfHelpGroup.SHGId=TimeTable.SHGId";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            CenterList.Add(new CenterNameView { BranchId = reader.GetString(0), CenterId = reader.GetString(1), CenterName = reader.GetString(2),EmpId=reader.GetString(3) });
                        }
                        reader.Close();
                        sqlconn.Close();
                    }
                }

            }
            return CenterList;
        }

        public static List<EmployeeNameView> GetEmployees()
        {
            List<EmployeeNameView> EmployeeList = new List<EmployeeNameView>();
            using   (SqlConnection sqlconn=new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select Employee.Name,Employee.EmpId,EmployeeBranch.BranchId from Employee,EmployeeBranch where EmployeeBranch.EmpId = Employee.EmpId and EmployeeBranch.Designation = 'Field Officer'";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            EmployeeList.Add(new EmployeeNameView { EmpName = reader.GetString(0), EmpId = reader.GetString(1), BranchId = reader.GetString(2) });
                        }
                        reader.Close();
                        sqlconn.Close();
                    }
                }
            }
            return EmployeeList;
        }


        public static List<CollectionReportView> GetCollectionDetails(string BranchId,string EmpId,string CenterID,DateTime CollectionDate)
        {
            List<CollectionReportView> CollectionList = new List<CollectionReportView>();
            using(SqlConnection sqlconn=new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select BranchDetails.BranchName,SelfHelpGroup.SHGName,Employee.Name,CustomerDetails.Name,LoanCollectionEntry.Total from CustomerDetails,Employee,BranchDetails,SelfHelpGroup,LoanCollectionEntry where BranchDetails.Bid='"+BranchId+"' and SelfHelpGroup.SHGId='"+CenterID+"' and Employee.EmpId='"+EmpId+"' and CustomerDetails.CustId=LoanCollectionEntry.CustId and LoanCollectionEntry.CollectedBy='"+EmpId+"' and CollectedOn='"+CollectionDate+"'";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            CollectionList.Add(
                                new CollectionReportView
                                {
                                    BranchName=reader.GetString(0),
                                    CenterName=reader.GetString(1),
                                    FoName=reader.GetString(2),
                                    CustomerName=reader.GetString(3),
                                    Amount=reader.GetInt32(4),
                                }
                                );
                        }
                        reader.Close();
                    }
                    sqlconn.Close();
                }
            }
            return CollectionList;
        }



        public static List<CollectionReportView> GetCollectionDetails(string BranchId, string EmpId,DateTime CollectionDate)
        {
            DayOfWeek day = CollectionDate.DayOfWeek;
            List<CollectionReportView> CollectionList = new List<CollectionReportView>();
            using (SqlConnection sqlconn = new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select BranchDetails.BranchName,SelfHelpGroup.SHGName,Employee.Name,CustomerDetails.Name,LoanCollectionEntry.Total from CustomerDetails,Employee,BranchDetails,SelfHelpGroup,LoanCollectionEntry where BranchDetails.Bid='01202109001' and SelfHelpGroup.SHGId='01001202109SHG-18' and Employee.EmpId='"+EmpId+"' and CustomerDetails.CustId=LoanCollectionEntry.CustId and LoanCollectionEntry.CollectedBy='E0100120210903' and CollectedOn='"+CollectionDate+"' and SelfHelpGroup.SHGId in (select SHGId from TimeTable where EmpId='"+EmpId+"' and CollectionDay='"+day+"')";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CollectionList.Add(
                                new CollectionReportView
                                {
                                    BranchName = reader.GetString(0),
                                    CenterName = reader.GetString(1),
                                    FoName = reader.GetString(2),
                                    CustomerName = reader.GetString(3),
                                    Amount = reader.GetInt32(4),
                                }
                                );
                        }
                        reader.Close();
                    }
                    sqlconn.Close();
                }
            }
            return CollectionList;
        }

    }
    public class BranchNameView
    {
        public string BranchName { get; set; }
        public string BranchId { get; set; }
    }
    public class CenterNameView
    {
        public string CenterName { get; set; }
        public string CenterId { get; set; }
        public string BranchId { get; set; }
        public string EmpId { get; set; }
    }
    public class EmployeeNameView
    {
        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public string BranchId { get; set; }
    }

    public class CollectionDetails
    {
        public string BranchId { get; set; }
        public string CustID { get; set; }
        public int Total { get; set; }
        public string EmpId { get; set; }
    }
}
