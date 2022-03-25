using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Repository
{
    public class NotificationRepository
    {
        public static string ConnectionString = MicroFinance.Properties.Settings.Default.DBConnection;

        public static int GetVerifyDocumentNotifyCount(string EmpID)
        {
            int Count = 0;

            using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select count(*) from LoanApplication where EmployeeId='"+EmpID+"' and LoanStatus=3";
                    Count = (int)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
            }
            return Count;

        }

       

        public static int GetVerifyDocumentNotifyCount(string BranchID,int StatusCode)
        {
            int Count = 0;
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select count(*) from LoanApplication where BranchId = '"+BranchID+"' and LoanStatus = '"+StatusCode+"'";
                    Count = (int)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
            }
            return Count;
        }

        public static int GetHimarkResultCount(string BranchId)
        {
            int Count = 0;
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select count(*) from HimarkResult,LoanApplication where HimarkResult.RequestID in (select RequestID from LoanApplication where LoanStatus=2 and BranchId='"+BranchId+"') and HimarkResult.RequestID=LoanApplication.RequestId";
                    Count = (int)sqlcomm.ExecuteScalar();
                }
                sqlconn.Close();
            }
            return Count;
        }
        public static int GetLoanApplicationCount(int StatusCode)
        {
            int Count = 0;
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    if(StatusCode==11)
                    {
                        sqlcomm.CommandText = "select Count(*) from DisbursementFromSAMU, LoanApplication where DisbursementFromSAMU.RequestID  in (select RequestID from LoanApplication where LoanStatus = '"+StatusCode+"') and DisbursementFromSAMU.RequestID = LoanApplication.RequestId";
                        Count = (int)sqlcomm.ExecuteScalar();
                    }
                    else
                    {
                        sqlcomm.CommandText = "select count(*) from LoanApplication where LoanStatus='" + StatusCode + "'";
                        Count = (int)sqlcomm.ExecuteScalar();
                    }
                }
                sqlconn.Close();
            }
            return Count;
        }
        

    }
}
