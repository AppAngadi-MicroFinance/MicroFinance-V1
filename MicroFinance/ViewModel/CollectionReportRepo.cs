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


        public static List<CollectionReportView> GetCollectionDetails(string branchID,string GroupID,DateTime CollectionDate)
        {
            List<CollectionReportView> CollectionDetialsList = new List<CollectionReportView>();
            List<CollectionDetails> Details = new List<CollectionDetails>();
            using(SqlConnection sqlconn=new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustomerGroup.CustId,LoanCollectionEntry.BranchId,LoanCollectionEntry.Total,LoanCollectionEntry.CollectedBy from LoanCollectionEntry,CustomerGroup where CollectedOn='"+CollectionDate+"' and LoanCollectionEntry.CustId=CustomerGroup.CustId and CustomerGroup.PeerGroupId in(select GroupId from PeerGroup where SHGid='" + GroupID + "')and LoanCollectionEntry.BranchId='"+branchID+"'";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            Details.Add(new CollectionDetails { CustID = reader.GetString(0), BranchId = reader.GetString(1), Total = reader.GetInt32(2), EmpId = reader.GetString(3) });
                        }
                    }
                    reader.Close();
                    
                    foreach(CollectionDetails CD in Details)
                    {
                        CollectionReportView CollectionData = new CollectionReportView();
                        sqlcomm.CommandText = "select BranchName from BranchDetails where Bid='" + CD.BranchId + "'";
                        CollectionData.BranchName = (string)sqlcomm.ExecuteScalar();
                        sqlcomm.CommandText = "select Name from Employee where EmpId = '"+CD.EmpId+"'";
                        CollectionData.FoName = (string)sqlcomm.ExecuteScalar();
                        sqlcomm.CommandText = "select Name from CustomerDetails where CustId='" + CD.CustID + "'";
                        CollectionData.CustomerName = (string)sqlcomm.ExecuteScalar();
                        CollectionData.Amount = CD.Total;
                        sqlcomm.CommandText = "select SHGName from SelfHelpGroup where SHGId=(select SHGid from PeerGroup where GroupId=(select peerGroupId from CustomerGroup where CustId='"+CD.CustID+"'))";
                        CollectionData.CenterName = (string)sqlcomm.ExecuteScalar();

                        CollectionDetialsList.Add(CollectionData);
                    }
                    sqlconn.Close();
                }
                return CollectionDetialsList;
            }


        }




    }

    public class CollectionDetails
    {
        public string BranchId { get; set; }
        public string CustID { get; set; }
        public int Total { get; set; }
        public string EmpId { get; set; }
    }
}
