using MicroFinance.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Repository
{
    public class SARepository
    {
        public static List<FOCollectionView> GetCollectionDetails(string BranchId,DateTime Date)
        {
            DateTime nextdate = Date.AddDays(1);
            List<FOCollectionView> CollectionDetails = new List<FOCollectionView>();
            List<string> EmpList = new List<string>();
            int Amount = 0;
            using (SqlConnection Sqlconn=new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                Sqlconn.Open();
                if(ConnectionState.Open==Sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = Sqlconn;
                    sqlcomm.CommandText = "select EmpId from EmployeeBranch where BranchId='"+BranchId+"'and IsActive=1 and Designation='Field Officer'";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            EmpList.Add(reader.GetString(0));
                        }
                        reader.Close();
                    }
                    foreach(string S in EmpList)
                    {
                        sqlcomm.CommandText = "select sum(PaidDue)as Amount from LoanCollectionEntry where CAST(CollectedOn as date)='" + Date.ToString("yyyy-MM-dd")+"' and BranchId='"+BranchId+"'and Collectedby='"+S+"' ";
                        object obj = sqlcomm.ExecuteScalar();
                        if(!DBNull.Value.Equals(obj))
                        {
                            Amount = Convert.ToInt32(obj);
                            string BranchName = MainWindow.BasicDetails.BranchList.Where(temp => temp.BranchId == BranchId).Select(temp => temp.BranchName).FirstOrDefault();
                            string EmpName = MainWindow.BasicDetails.EmployeeList.Where(temp => temp.EmployeeId == S).Select(temp => temp.EmployeeName).FirstOrDefault().ToUpper();
                            FOCollectionView Collection = new FOCollectionView { BranchName = BranchName, EmployeeName = EmpName, CollectedDate = Date, CollectionAmount = Amount };
                            CollectionDetails.Add(Collection);
                        }
                        
                    }
                    
                }
            }
            return CollectionDetails;
        }
        public static SACollectionViewModel GetCollectionDetails(string BranchID,String EmpID,DateTime Date)
        {
            SACollectionViewModel CollectionDetails = new SACollectionViewModel();
            return CollectionDetails;
        }



        public static SALoanStatusView GetApplicationStatusDetails(string BranchId)
        {
            SALoanStatusView StatusDetail = new SALoanStatusView();
            List<StatusModal> Statuslist = new List<StatusModal>();
            using (SqlConnection sqlconn=new SqlConnection(Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    for(int i=1;i<=14;i++)
                    {
                        sqlcomm.CommandText = "select count(*) from LoanApplication where BranchId='"+BranchId+"' and LoanStatus='"+i+"'";
                        int res =(int) sqlcomm.ExecuteScalar();
                        Statuslist.Add(new StatusModal { Code = i, Count = res });
                    }
                    StatusDetail.StatusDetails = Statuslist;
                }
                sqlconn.Close();
            }
            return StatusDetail;
        }
    }
}
