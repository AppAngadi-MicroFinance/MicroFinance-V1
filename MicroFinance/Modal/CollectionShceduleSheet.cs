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

        public string GroupId { get; set; }

        public string SheetId
        {
            get; set;
        }

        string _customerId;
        public string CustomerId
        {
            get
            {
                return _customerId;
            }
            set
            {
                _customerId = value;
                CustomerName = GetCustomerName(_customerId);
            }
        }

        string _customerName;
        public string CustomerName
        {
            get
            {
                return _customerName;
            }
            set
            {
                _customerName = value;
            }
        }

        public string Attendance { get; set; }

        string _loanId;
        public string LoanID
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
        public DateTime LoanDate { get; set; }

        public int CurrentWeekNo { get; set; }
        public int SecurityDeposite { get; set; }
        public int TotalSD { get; set; }

        public int LoanAmount { get; set; }
        public int WeeksPaid { get; set; }
        public int PrincipalPaid { get; set; }
        public int OutStandingAmount { get; set; }

        public int Principal { get; set; }
        public int Interest { get; set; }

        public int TotalAmonut { get; set; }



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
            ActiveLoans = GetActiveLoanCustomer(employeeID, "WEDNESSDAY");
        }
        string SetGroupID(string custID)
        {
            string groupId = string.Empty;
            using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "select PeerGroupId from CustomerGroup where CustId = '" + custID + "'";
                GroupId = (string)command.ExecuteScalar();
                sql.Close();
            }
            return groupId;
        }
        void GetPaidDetails(string loanId)
        {
            using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "select Count(Principal), SUM(Principal), MIN(Balance), SUM(SecurityDeposite) from LoanCollectionEntry where LoanId = '" + loanId + "' and Attendance > 0";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    WeeksPaid = reader.GetInt32(0) + 1;
                    if (WeeksPaid <= 1)
                    {
                        PrincipalPaid = 0;
                        OutStandingAmount = LoanAmount;
                        SecurityDeposite = 0;
                    }
                    else
                    {
                        PrincipalPaid = reader.GetInt32(1);
                        OutStandingAmount = reader.GetInt32(2);
                        SecurityDeposite = reader.GetInt32(3);
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
                    TotalAmonut = reader.GetInt32(2);
                }
                sql.Close();
            }
        }


        string GetCustomerName(string custId)
        {
            
            string name = string.Empty;
            using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "select Name from CustomerDetails where CustId = '" + custId + "'";
                name = (string)command.ExecuteScalar();
                sql.Close();
            }
            return name;
        }

        public static List<CollectionShceduleSheet> GetActiveLoanCustomer(string empID, string day)
        {
            string idd = string.Empty;
            List<string> EmployeeSHG = new List<string>();
            using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "select SHGId from TimeTable where EmpId = '" + empID + "' and CollectionDay ='" + day + "'";
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

            List<CustomerIDinGroup> CustomersINPeerGroup = new List<CustomerIDinGroup>();
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
                        idd = initLeft + "." + initRight;
                        CustomersINPeerGroup.Add(new CustomerIDinGroup(pgID, reader.GetString(0), idd)); 
                    }
                    sql.Close();
                }
            }

            List<CollectionShceduleSheet> ActiveLoanCustomer = new List<CollectionShceduleSheet>();
            List<string> LoanIdForCustomerID = new List<string>();
            foreach(CustomerIDinGroup item in CustomersINPeerGroup)
            {
                using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
                {
                    sql.Open();
                    SqlCommand command = new SqlCommand();
                    command.Connection = sql;
                    command.CommandText = "select CustomerID, LoanID, LoanAmount,ApproveDate from LoanDetails where CustomerID = '"+item.CustomerId+"' and IsActive = 1";
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        CollectionShceduleSheet temp = new CollectionShceduleSheet();
                        temp.SheetId = item.CustomerPGId;
                        temp.GroupId = item.GroupId;
                        temp.CustomerId = reader.GetString(0);
                        temp.LoanAmount = reader.GetInt32(2);
                        temp.LoanDate = reader.GetDateTime(3);
                        temp.Attendance = string.Empty;
                        temp.LoanID = reader.GetString(1);
                        ActiveLoanCustomer.Add(temp);
                    }
                    sql.Close();
                }
            }
            return ActiveLoanCustomer;
        }
    }

    public class CustomerIDinGroup
    {
        public string GroupId { get; set; }
        public string CustomerId { get; set; }
        public string CustomerPGId { get; set; }
        
        public CustomerIDinGroup(string gId, string cId, string cPgId)
        {
            GroupId = gId;
            CustomerId = cId;
            CustomerPGId = cPgId;
        }
    }

    public class GroupWiseDetails
    {
        string _groupName;
        public string GroupName 
        {
            get { return _groupName; }
            set { _groupName = value; }
        }
        string _groupId;
        public string GroupId
        {
            get { return _groupId; }
            set 
            { 
                _groupId= value;
                GetGroupName(_groupId);
            }
        }

        int _amount;
        public int Amount
        {
            get { return _amount; }
            set { _amount = value; }
        }
        public GroupWiseDetails(string gid, int amt)
        {
            GroupId = gid;
            Amount = amt;
        }
        public GroupWiseDetails()
        {

        }
        void GetGroupName(string groupId)
        {
            using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "select GroupName from PeerGroup where GroupId = '" + groupId +"'";
                GroupName = (string)command.ExecuteScalar();
                sql.Close();
            }
        }
    }

}
