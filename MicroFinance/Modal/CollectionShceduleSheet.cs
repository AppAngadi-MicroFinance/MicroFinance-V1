using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    public class CollectionShceduleSheet
    {
        string EmpId;
        public string CustGroupId { get; set; }

        public string Attendance { get; set; }
        string _custID;
        public string CustID
        {
            get
            {
                return _custID;
            }
            set
            {
                _custID = value;
                GetCustomerName(_custID);
            }
        }
        public string CustomerName { get; set; }

        string _loanId;
        public string LoanId
        {
            get
            {
                return _loanId;
            }
            set
            {
                _loanId = value;
                GetPaidDetails(_loanId);
                GetNextDueDetails(_loanId);
            }
        }
        public DateTime SanctionDate { get; set; }
        public string LoanType { get; set; }
        public int LoanAmount { get; set; }

        public int NoOfPayments { get; set; }
        public int PaidPrincipal { get; set; }
        public int RemaingLoanAmount { get; set; }

        public int Principal { get; set; }
        public int Interest { get; set; }
        public int Total { get; set; }

        public CollectionShceduleSheet()
        {

        }
        public CollectionShceduleSheet(string empId)
        {
            EmpId = empId;
        }
        public static void  GenerateShceduleSheet(string employeeID)
        {
            List<CollectionShceduleSheet> ActiveLoans = new List<CollectionShceduleSheet>();
            ActiveLoans = GetActiveLoanCustomer(employeeID);
            FillData3(ActiveLoans);
        }

        void GetPaidDetails(string loanId)
        {
            using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "select Count(Principal), SUM(Principal), MIN(Balance) from LoanCollectionEntry where LoanId = '" + loanId + "' and Attendance > 0";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    NoOfPayments = reader.GetInt32(0) + 1;
                    if (NoOfPayments <= 1)
                    {
                        PaidPrincipal = 0;
                        RemaingLoanAmount = LoanAmount;
                    }
                    else
                    {
                        PaidPrincipal = reader.GetInt32(1);
                        RemaingLoanAmount = reader.GetInt32(2);
                    }

                }
                sql.Close();
            }
        }
        void GetNextDueDetails(string loanID)
        {
            using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "select Principal, Interest,Total from LoanCollectionMaster where WeekNo = ((select count(CustId) from LoanCollectionEntry where LoanId = '" + loanID + "' and Attendance > 0) + 1) and LoanId = '" + loanID + "'";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Principal = reader.GetInt32(0);
                    Interest = reader.GetInt32(1);
                    Total = reader.GetInt32(2);
                    
                }
                sql.Close();
            }
        }


        void GetCustomerName(string custId)
        {
            using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "select Name from CustomerDetails where CustId = '" + custId + "'";
                CustomerName = (string)command.ExecuteScalar();
                sql.Close();
            }
        }
        static public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }
        static void FillData3(List<CollectionShceduleSheet> collectionList)
        {
            //string filedate = showDate.Text;
            DataTable dt1 = new DataTable();
            dt1 = ConvertToDataTable(collectionList.ToList());
            ReportDataSource reportDataSource1 = new ReportDataSource();
            reportDataSource1.Name = "DataSet1"; // Name of the DataSet we set in .rdlc
            reportDataSource1.Value = dt1;
            ReportViewer reportViewer1 = new ReportViewer();
            reportViewer1.LocalReport.ReportEmbeddedResource = "MicroFinance.CollectionSheet.rdlc";
            reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            reportViewer1.RefreshReport();
            reportViewer1.ProcessingMode = ProcessingMode.Local;

            Warning[] warnings1;
            string[] streamids1;
            string mimeType1;
            string encoding1;
            string extension1;
            try
            {
                string dir = string.Empty;
                //string showdatess = Changeformat(showDate.Text);
                byte[] bytes = reportViewer1.LocalReport.Render(
                  "PDF", null, out mimeType1, out encoding1, out extension1,
                  out streamids1, out warnings1);
                dir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "REPORTS\\");
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                if (Directory.Exists(dir))
                {
                    //var temp = "" + data + "" + "_WeeklyReport"+showDate+"";

                    //var temp = "" + data + "" + "_" + "" + SelectedCluster + "";

                    FileStream fs = new FileStream(dir + "Collection_Sheet_"+DateTime.Now.ToShortDateString() + ".pdf", FileMode.Create);

                    var temps = fs.ToString();
                    fs.Write(bytes, 0, bytes.Length);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        //static List<CollectionShceduleSheet> GetActiveLoanCustomer(string empId)
        //{
        //    List<CollectionShceduleSheet> ActiveLoanCustomer = new List<CollectionShceduleSheet>();
        //    using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
        //    {
        //        sql.Open();
        //        SqlCommand command = new SqlCommand();
        //        command.Connection = sql;
        //        command.CommandText = "select LoanDetails.CustomerID, LoanDetails.LoanID, LoanDetails.LoanType, LoanDetails.LoanAmount,LoanDetails.ApproveDate from LoanDetails  join CustomerGroup on CustomerGroup.CustId = LoanDetails.CustomerID join PeerGroup on PeerGroup.GroupId = CustomerGroup.PeerGroupId join TimeTable on TimeTable.SHGId = PeerGroup.SHGid and TimeTable.EmpId = '" + empId + "' where LoanDetails.IsActive = 1";
        //        SqlDataReader reader = command.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            CollectionShceduleSheet temp = new CollectionShceduleSheet();
        //            temp.CustID = reader.GetString(0);
        //            temp.LoanType = reader.GetString(2);
        //            temp.LoanAmount = reader.GetInt32(3);
        //            temp.SanctionDate = reader.GetDateTime(4);
        //            temp.Attendance = string.Empty;
        //            temp.LoanId = reader.GetString(1);
        //            ActiveLoanCustomer.Add(temp);
        //        }
        //        sql.Close();
        //    }
        //    return ActiveLoanCustomer;
        //}


        static List<CollectionShceduleSheet> GetActiveLoanCustomer(string empID)
        {
            List<string> EmployeeSHG = new List<string>();
            using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "select SHGId from TimeTable where EmpId = '"+empID+"'";
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    EmployeeSHG.Add(reader.GetString(0));
                }
                sql.Close();
            }

            List<string> PeerGroupForSHG = new List<string>();
            foreach(string shgID in EmployeeSHG)
            {
                using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
                {
                    sql.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sql;
                    command.CommandText = "select GroupId from PeerGroup where SHGid = '"+ shgID + "'";
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        PeerGroupForSHG.Add(reader.GetString(0));
                    }
                    sql.Close();
                }
            }

            List<CustomersInPG> CustomersINPeerGroup = new List<CustomersInPG>();

            int initLeft = 0;
            int initRight = 0;
            foreach(string pgID in PeerGroupForSHG)
            {
                initLeft++;
                using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
                {
                    sql.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sql;
                    command.CommandText = "select CustId from CustomerGroup where PeerGroupId = '"+pgID+"'";
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        initRight++;
                        string idd = initLeft + "." + initRight;
                        CustomersINPeerGroup.Add(new CustomersInPG(reader.GetString(0), idd));
                    }
                    sql.Close();
                }
            }

            List<CollectionShceduleSheet> ActiveLoanCustomer = new List<CollectionShceduleSheet>();
            List<string> LoanIdForCustomerID = new List<string>();
            foreach(CustomersInPG item in CustomersINPeerGroup)
            {
                using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
                {
                    sql.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sql;
                    command.CommandText = "select CustomerID, LoanID, LoanType, LoanAmount,ApproveDate from LoanDetails where CustomerID = '"+item.CustomerId+"' and IsActive = 1";
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        CollectionShceduleSheet temp = new CollectionShceduleSheet();
                        temp.CustGroupId = item.PgId;
                        temp.CustID = reader.GetString(0);
                        temp.LoanType = reader.GetString(2);
                        temp.LoanAmount = reader.GetInt32(3);
                        temp.SanctionDate = reader.GetDateTime(4);
                        temp.Attendance = string.Empty;
                        temp.LoanId = reader.GetString(1);
                        ActiveLoanCustomer.Add(temp);
                    }
                    sql.Close();
                }
            }
            return ActiveLoanCustomer;
        }
    }
    public class CustomersInPG
    {
        public string CustomerId { get; set; }
        public string PgId { get; set; }
        public CustomersInPG(string cid, string pid)
        {
            CustomerId = cid;
            PgId = pid;
        }
    }

}
