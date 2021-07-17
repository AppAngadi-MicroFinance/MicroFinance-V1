using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MicroFinance.Modal
{

    public class CollectionSheet
    {
        public string RegionName { get; set; } = "Trichy";
        public string BranchName { get; set; } = "Central";
        public string CenterName { get; set; } = "Bharathiyar St-B";
        public string Date { get; set; } = "2021-07-16";
        public string Day { get; set; } = "Friday";
        public List<CollectionFormatDetails> CollectionFormat=new List<CollectionFormatDetails>();
        public Dictionary<string, int> GroupTotals;
        List<string> Groups;

		public CollectionSheet()
        {
            GetColletion();
        }

		void GetColletion()
        {
            List<GroupWithCustomer> ActiveCustomers = new List<GroupWithCustomer>();
			using(SqlConnection connection=new SqlConnection(Properties.Settings.Default.db))
            {
                connection.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "select CustomerGroup.CustId,CustomerDetails.Name,CustomerGroup.PeerGroup from CustomerGroup join CustomerDetails on CustomerGroup.CustId = CustomerDetails.CustId where CustomerGroup.BranchId = (select Bid from BranchDetails where RegionName = '" + RegionName + "' and BranchName = '" + BranchName + "') and CustomerGroup.SelfHelpGroup = '" + CenterName + "' and CustomerDetails.IsActive = 1";
                SqlDataReader dataReader = command.ExecuteReader();
				while(dataReader.Read())
                {
                    ActiveCustomers.Add(new GroupWithCustomer {  CustId=dataReader.GetString(0),Name=dataReader.GetString(1),GroupName=dataReader.GetString(2)});
                }
                dataReader.Close();
				foreach(var customer in ActiveCustomers)
                {
                    command.CommandText = "select LoanDisposement.LoanType,LoanDisposement.LoanAmount,LoanCollection.RemainingDueAmount,LoanCollection.Principal,LoanCollection.Interest,LoanCollection.LoanID from LoanCollection join LoanDisposement on LoanCollection.LoanID=LoanDisposement.LoanID where LoanCollection.CustId='" + customer.CustId + "' and LoanCollection.Date='" + Date + "' and LoanCollection.Day='" + Day + "' and LoanDisposement.Active=1";
                    dataReader = command.ExecuteReader();
                    string _loadId = "";
                    CollectionFormatDetails collectionFormats = new CollectionFormatDetails();
                    while (dataReader.Read())
                    {
                        collectionFormats.GroupName = customer.GroupName;
                        collectionFormats.MemberName = customer.Name;
                        collectionFormats.Date = Date;
                        collectionFormats.LoanType = dataReader.GetString(0);
                        int Total = dataReader.GetInt32(1);
                        int _remaining = dataReader.GetInt32(2);
                        collectionFormats.Amount = Total;
                        collectionFormats.PaidPrinciple = Total - _remaining;
                        collectionFormats.Loanos = _remaining;
                        int _principal = dataReader.GetInt32(3);
                        int _interest = dataReader.GetInt32(4);
                        _loadId = dataReader.GetString(5);
                        collectionFormats.Principle = _principal;
                        collectionFormats.Interest = _interest;
                        collectionFormats.Total = _principal + _interest;
                    }
                    dataReader.Close();
                    command.CommandText = "select Sum(NoOfPaymentDone) from LoanCollection where LoanID='" + _loadId + "'";
                    collectionFormats.NumberofPayment = (int)command.ExecuteScalar();
					if(!String.IsNullOrEmpty(collectionFormats.MemberName))
                    {
                        CollectionFormat.Add(collectionFormats);
                    }
                }
            }
        }

		void CalculateGroupTotal()
        {
			foreach(CollectionFormatDetails collections in CollectionFormat)
            {
				if(GroupTotals.ContainsKey(collections.GroupName))
                {
                    int _temp = 0;
					if(GroupTotals.TryGetValue(collections.GroupName,out _temp))
                    {
                        _temp += collections.Total;
                        GroupTotals[collections.GroupName] = _temp;
                    }
                }
				else
                {
                    GroupTotals.Add(collections.GroupName, collections.Total);
                }
            }
        }
    }
	class GroupWithCustomer
    {
		public string CustId { get; set; }
		public string Name { get; set; }
		public string GroupName { get; set; }
    }
     public class CollectionFormatDetails
    {
		public string GroupName { get; set; }
        public string MemberName { get; set; }
        public string Date { get; set; }
        public string LoanType { get; set; }
        public int Amount { get; set; }
        public int NumberofPayment { get; set; }
        public int PaidPrinciple { get; set; }
        public int Loanos { get; set; }
        public int Principle { get; set; }
        public int Interest { get; set; }
        public int Total { get; set; }
    }
}
