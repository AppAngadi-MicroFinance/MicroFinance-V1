﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroFinance.Modal;
using MicroFinance.Reports;

namespace MicroFinance.ViewModel
{
    public class HimarkRepository
    {

        string ConnectionString = MicroFinance.Properties.Settings.Default.DBConnection;

        public static List<HimarkRequestView> GetHimarkRequestList()
        {
            List<HimarkRequestView> ResultView = new List<HimarkRequestView>();
            using(SqlConnection sqlconn=new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustomerDetails.Name,LoanApplication.RequestId,LoanApplication.CustId,LoanApplication.LoanAmount,LoanApplication.LoanPeriod,LoanApplication.EmployeeId,LoanApplication.BranchId,BranchDetails.BranchName,Employee.Name,LoanApplication.EnrollDate from CustomerDetails,LoanApplication,BranchDetails,Employee where RequestId in(select RequestId from LoanApplication where LoanStatus='1') and LoanApplication.CustId=CustomerDetails.CustId and BranchDetails.Bid=LoanApplication.BranchId and Employee.EmpId=LoanApplication.EmployeeId";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            HimarkRequestView HMRequestCustomer = new HimarkRequestView();
                            HMRequestCustomer.CustomerName = reader.GetString(0);
                            HMRequestCustomer.RequestID = reader.GetString(1);
                            HMRequestCustomer.CustomerID = reader.GetString(2);
                            HMRequestCustomer.LoanAmount = reader.GetInt32(3);
                            HMRequestCustomer.LoanPeriod = reader.GetInt32(4);
                            HMRequestCustomer.EmpId = reader.GetString(5);
                            HMRequestCustomer.BranchID = reader.GetString(6);
                            HMRequestCustomer.BranchName = reader.GetString(7);
                            HMRequestCustomer.EmpName = reader.GetString(8);
                            HMRequestCustomer.RequestDate = reader.GetDateTime(9);
                            // SqlCommand sqlcomm = new SqlCommand();
                            // sqlcomm.Connection = sqlconn;
                            // sqlcomm.CommandText = "select Name from Employee where EmpId='" + HMRequestCustomer.EmpId + "'";
                            //HMRequestCustomer.EmpName = GetEmpName(HMRequestCustomer.EmpId);
                            ResultView.Add(HMRequestCustomer);
                        }
                    }
                    reader.Close();
                    foreach(HimarkRequestView Hm in ResultView)
                    {
                        sqlcomm.CommandText = "select CollectionDay from TimeTable where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='"+Hm.CustomerID+"'))";
                        Hm.Collectionday = (string)sqlcomm.ExecuteScalar();
                        sqlcomm.CommandText = "select SHGName from SelfHelpGroup where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='" + Hm.CustomerID + "'))";
                        Hm.CenterName = (string)sqlcomm.ExecuteScalar();
                    }
                }
            }
            return ResultView;
        }


        public static List<HimarkRequestView> GetHimarkRejectToApproveList(DateModel DateData)
        {
            List<HimarkRequestView> ResultView = new List<HimarkRequestView>();
            using (SqlConnection sqlconn = new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustomerDetails.Name,LoanApplication.RequestId,LoanApplication.CustId,LoanApplication.LoanAmount,LoanApplication.LoanPeriod,LoanApplication.EmployeeId,LoanApplication.BranchId,BranchDetails.BranchName,Employee.Name,LoanApplication.EnrollDate,Loanapplication.SHGId from CustomerDetails,LoanApplication,BranchDetails,Employee where RequestId in(select RequestId from HimarkResult where status='Reject') and LoanApplication.CustId=CustomerDetails.CustId and BranchDetails.Bid=LoanApplication.BranchId and Employee.EmpId=LoanApplication.EmployeeId and EnrollDate between '"+DateData.FromDate.ToString("yyyy-MM-dd")+"' and '"+DateData.ToDate.ToString("yyyy-MM-dd")+"' and  LoanApplication.LoanStatus not in (4,5,13,14)";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            HimarkRequestView HMRequestCustomer = new HimarkRequestView();
                            HMRequestCustomer.CustomerName = reader.GetString(0);
                            HMRequestCustomer.RequestID = reader.GetString(1);
                            HMRequestCustomer.CustomerID = reader.GetString(2);
                            HMRequestCustomer.LoanAmount = reader.GetInt32(3);
                            HMRequestCustomer.LoanPeriod = reader.GetInt32(4);
                            HMRequestCustomer.EmpId = reader.GetString(5);
                            HMRequestCustomer.BranchID = reader.GetString(6);
                            HMRequestCustomer.BranchName = reader.GetString(7);
                            HMRequestCustomer.EmpName = reader.GetString(8);
                            HMRequestCustomer.RequestDate = reader.GetDateTime(9);
                            HMRequestCustomer.CenterID = reader.GetString(10);
                            ResultView.Add(HMRequestCustomer);
                        }
                    }
                    reader.Close();
                    foreach (HimarkRequestView Hm in ResultView)
                    {
                        TimeTableViewModel Center = MainWindow.BasicDetails.CenterList.Where(temp => temp.SHGId == Hm.CenterID).Select(temp=>new TimeTableViewModel {SHGName=temp.SHGName,SHGId=temp.SHGId,CollectionDay=temp.CollectionDay }).FirstOrDefault();
                        if(Center!=null)
                        {
                            Hm.Collectionday = Center.CollectionDay;
                        Hm.CenterName = Center.SHGName;
                        }
                        
                    }
                }
            }
            return ResultView;
        }

        public static List<HimarkRequestView> GetHimarkApprovetoRejectList(DateModel DateData)
        {
            List<HimarkRequestView> ResultView = new List<HimarkRequestView>();
            using (SqlConnection sqlconn = new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select CustomerDetails.Name,LoanApplication.RequestId,LoanApplication.CustId,LoanApplication.LoanAmount,LoanApplication.LoanPeriod,LoanApplication.EmployeeId,LoanApplication.BranchId,BranchDetails.BranchName,Employee.Name,LoanApplication.EnrollDate,Loanapplication.SHGId from CustomerDetails,LoanApplication,BranchDetails,Employee where RequestId in(select RequestId from HimarkResult where status not in ('Reject')) and LoanApplication.CustId=CustomerDetails.CustId and BranchDetails.Bid=LoanApplication.BranchId and Employee.EmpId=LoanApplication.EmployeeId and EnrollDate between '" + DateData.FromDate.ToString("yyyy-MM-dd") + "' and '" + DateData.ToDate.ToString("yyyy-MM-dd") + "' and  LoanApplication.LoanStatus in (5,13)";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            HimarkRequestView HMRequestCustomer = new HimarkRequestView();
                            HMRequestCustomer.CustomerName = reader.GetString(0);
                            HMRequestCustomer.RequestID = reader.GetString(1);
                            HMRequestCustomer.CustomerID = reader.GetString(2);
                            HMRequestCustomer.LoanAmount = reader.GetInt32(3);
                            HMRequestCustomer.LoanPeriod = reader.GetInt32(4);
                            HMRequestCustomer.EmpId = reader.GetString(5);
                            HMRequestCustomer.BranchID = reader.GetString(6);
                            HMRequestCustomer.BranchName = reader.GetString(7);
                            HMRequestCustomer.EmpName = reader.GetString(8);
                            HMRequestCustomer.RequestDate = reader.GetDateTime(9);
                            HMRequestCustomer.CenterID = reader.GetString(10);
                            ResultView.Add(HMRequestCustomer);
                        }
                    }
                    reader.Close();
                    foreach (HimarkRequestView Hm in ResultView)
                    {
                        TimeTableViewModel Center = MainWindow.BasicDetails.CenterList.Where(temp => temp.SHGId == Hm.CenterID).Select(temp => new TimeTableViewModel { SHGName = temp.SHGName, SHGId = temp.SHGId, CollectionDay = temp.CollectionDay }).FirstOrDefault();
                        if (Center != null)
                        {
                            Hm.Collectionday = Center.CollectionDay;
                            Hm.CenterName = Center.SHGName;
                        }

                    }
                }
            }
            return ResultView;
        }

        public static string GetEmpName(string ID)
        {
            string Result = "";
            using (SqlConnection sqlconn=new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                SqlCommand sqlcomm = new SqlCommand();
                sqlcomm.Connection = sqlconn;
                sqlcomm.CommandText = "select Name from Employee where EmpId='" + ID + "'";
                Result = (string)sqlcomm.ExecuteScalar();
                
            }
            return Result;
        }


        public static List<HimarkModel> GetDetailsForReport(List<HimarkRequestView> RequestList)
        {
            List<HimarkModel> ReportList = new List<HimarkModel>();

            using(SqlConnection sqlconn=new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    foreach(HimarkRequestView HM in RequestList)
                    {
                        HimarkModel HMData = new HimarkModel();
                        sqlcomm.CommandText = "select BranchName,AgreementStartDate from BranchDetails where Bid='"+HM.BranchID+"'";
                        SqlDataReader reader = sqlcomm.ExecuteReader();
                        if(reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                string Name = reader.GetString(0);
                                HMData.CBOName = "GRAMEEN TRUST";
                                // HMData.CBOName = Name.ToUpper();
                                HMData.FIGName = Name.ToUpper();
                                HMData.SamFinBranch = Name.ToUpper();
                                HMData.FIGFormationDate = reader.GetDateTime(1).ToString("MM-dd-yyyy");
                            }
                        }
                        reader.Close();
                        //Read Shg Details
                        sqlcomm.CommandText = "select SHGName,Taluk,District from SelfHelpGroup where SHGId=(select SHGid from PeerGroup where GroupId=(select PeerGroupId from CustomerGroup where CustId='"+HM.CustomerID+"'))";
                        SqlDataReader reader3 = sqlcomm.ExecuteReader();
                        if(reader3.HasRows)
                        {
                            while(reader3.Read())
                            {
                                HMData.FIGVillage = reader3.GetString(0);
                                HMData.FIGTaluk = reader3.GetString(1);
                                HMData.FIGDistrict = reader3.GetString(2);
                                HMData.State = "TamilNadu";
                                HMData.FIGState = "TAMIL NADU";
                            }
                        }
                        reader3.Close();
                        HMData.EligibleLoanAmount = HM.LoanAmount.ToString();
                        HMData.RMName = HM.EmpName;
                       // HMData.DateOfApplication = DateTime.Now.ToString("dd-MMM-yyyy");
                        sqlcomm.CommandText = "select Name,FatherName,MotherName,Dob,Age,Gender,Mobile,Religion,Caste,Education,Occupation,MonthlyIncome,MonthlyExpenses,address,Pincode,HousingType,PhotoProofName,PhotoProofNo,AddressProofName,AddressProofNo,BankAccountNo,BankName,BankBranchName,IFSCCode,MICRCode,Residency,LandHolding,LandHoldingProof from CustomerDetails where CustId='"+HM.CustomerID+"'";
                        SqlDataReader reader1 = sqlcomm.ExecuteReader();
                        if(reader1.HasRows)
                        {
                            while(reader1.Read())
                            {
                                HMData.ApplicantName = reader1.GetString(0).ToUpper();
                                HMData.ApplicantFatherName = reader1.GetString(1).ToUpper();
                                HMData.ApplicantMotherName = reader1.GetString(2).ToString();
                                HMData.ApplicantDOB = reader1.GetDateTime(3).ToString("dd-MMM-yyyy");
                                HMData.ApplicantAge = reader1.GetInt32(4);
                                HMData.ApplicantGender = reader1.GetString(5);
                                HMData.ApplicantMobile = reader1.GetString(6);
                                HMData.Religion = reader1.GetString(7);
                                HMData.Caste = reader1.GetString(8);
                                HMData.Education = reader1.GetString(9);
                                HMData.Occupation = reader1.GetString(10);
                                HMData.MonthlyIncome = reader1.GetInt32(11).ToString();
                                HMData.MonthlyExpenses = reader1.GetInt32(12).ToString();
                                string[] fulladdress = reader1.GetString(13).Split('|', '~').ToArray();
                                HMData.DoorNO = (fulladdress[0].ToString()== "0")?" ": fulladdress[0].ToString();
                                HMData.StreetName = fulladdress[2].ToString().ToUpper();
                                HMData.Village = fulladdress[4].ToString().ToUpper();
                                HMData.Taluk = fulladdress[6].ToString().ToUpper();
                                HMData.District = fulladdress[8].ToString().ToUpper();
                                HMData.Pincode = reader1.GetInt32(14).ToString();
                                HMData.Place = HMData.Taluk;
                                HMData.Residence = (DBNull.Value.Equals(reader1["Residency"])) ? "" : reader1.GetString(25).ToUpper();
                                HMData.TypeOFResidence = (DBNull.Value.Equals(reader1["HousingType"])) ? "" : reader1.GetString(15).ToUpper();
                                HMData.LandHolding = (DBNull.Value.Equals(reader1["LandHolding"])) ? "" : reader1.GetString(26).ToUpper();
                                // HMData.Residence = reader1.GetString(25).ToUpper();
                                // HMData.TypeOFResidence = reader1.GetString(15).ToUpper();
                               // HMData.LandHolding = reader1.GetString(26).ToUpper();
                                HMData.ApplicantIDProofType = reader1.GetString(16).ToUpper();
                                HMData.ApplicantIDProofNo = reader1.GetString(17).ToUpper();
                                HMData.ApplicantAddressProofType = reader1.GetString(18).ToUpper();
                                HMData.ApplicantAddressProofNo = reader1.GetString(19);
                                HMData.ApplicantAccountNO = reader1.GetString(20);
                                HMData.ApplicantBankName = reader1.GetString(21);
                                HMData.ApplicantBranchName = reader1.GetString(22).ToUpper();
                                HMData.ApplicantIFSCcode = reader1.GetString(23).ToUpper();
                                HMData.LoanPurpose = "AGRI";
                                HMData.LoanTenure = (HM.LoanPeriod == 50) ? "12": "0";
                                HMData.LandHoldingProof = (DBNull.Value.Equals(reader1["LandHoldingProof"])) ? "" : reader1.GetString(27).ToUpper();
                            }
                        }
                        reader1.Close();
                        sqlcomm.CommandText = "select isLeader from CustomerGroup where CustId='" + HM.CustomerID + "'";
                        HMData.Member = ((bool)sqlcomm.ExecuteScalar()) ? "LEADER" : "MEMBER";
                        sqlcomm.CommandText = "select Name,dob,age,RelationShip,AddressProofName,AddressProofNo,PhotoProofName,PhotoProofNo,Gender,Mobile,MonthlyHHIncome from GuarenteeDetails where CustId='" + HM.CustomerID + "'";
                        SqlDataReader reader2 = sqlcomm.ExecuteReader();
                        if(reader2.HasRows)
                        {
                            while(reader2.Read())
                            {
                                HMData.COapplicantName = reader2.GetString(0);
                                HMData.COapplicantDOB = reader2.GetDateTime(1).ToString("dd-MMM-yyyy");
                                HMData.COapplicantAge = reader2.GetInt32(2);
                                string RelationShip = reader2.GetString(3);
                                HMData.RelationshipWithApplicant = RelationShip;
                                HMData.RelationshipWithCOapplicant = RelationShipCal(RelationShip);
                                HMData.COapplicantAddressProofType = reader2.GetString(4);
                                HMData.COapplicantAddressProofNo = reader2.GetString(5);
                                HMData.COapplicantIDProofType = reader2.GetString(6);
                                HMData.COapplicantIDProofNo = reader2.GetString(7);
                                HMData.COapplicantGender = reader2.GetString(8);
                                HMData.CoApplicantMobileNo = reader2.GetString(9);
                                HMData.GMonthlyIncome = (DBNull.Value.Equals(reader2["MonthlyHHIncome"])) ? 0 : reader2.GetInt32(10); 

                            }
                        }
                        reader2.Close();
                        HMData.DateOfApplication = HM.RequestDate;

                        sqlcomm.CommandText = "select C1Name,C1Mobile,C1DOB,C1Age,C1IdProofType,C1IdProofNo,C1AddressProofType,C1AddressProofNo,C1MonthlyIncome,C2Name,C2Mobile,C2DOB,C2Age,C2IdProofType,C2IdProofNo,C2AddressProofType,C2AddressProofNo,C2MonthlyIncome,C1Gender,C2Gender from ChildDetails where CustId='" + HM.CustomerID+"'";
                        SqlDataReader dr3 = sqlcomm.ExecuteReader();
                        if(dr3.HasRows)
                        {
                            while(dr3.Read())
                            {
                                
                                HMData.C1Name      =        (DBNull.Value.Equals(dr3["C1Name"])) ? "" : dr3.GetString(0).ToUpper();
                                HMData.C1Mobile =           (DBNull.Value.Equals(dr3["C1Mobile"])) ? "" : dr3.GetString(1).ToUpper();
                                HMData.C1DOB =              (DBNull.Value.Equals(dr3["C1DOB"])) ? "" : dr3.GetDateTime(2).ToString().ToUpper();
                                HMData.C1Age =              (DBNull.Value.Equals(dr3["C1Age"])) ? "" : dr3.GetInt32(3).ToString().ToUpper();
                                HMData.C1IdProofType=       (DBNull.Value.Equals(dr3["C1IdProofType"])) ? "" : dr3.GetString(4).ToUpper();
                                HMData.C1IdProofNo =        (DBNull.Value.Equals(dr3["C1IdProofNo"])) ? "" : dr3.GetString(5).ToUpper();
                                HMData.C1AddressProofType = (DBNull.Value.Equals(dr3["C1AddressProofType"])) ? "" : dr3.GetString(6).ToUpper();
                                HMData.C1AddressProofNo =   (DBNull.Value.Equals(dr3["C1AddressProofNo"])) ? "" : dr3.GetString(7).ToUpper();
                                HMData.C1MonthlyIncome =    (DBNull.Value.Equals(dr3["C1MonthlyIncome"])) ? "" : dr3.GetInt32(8).ToString().ToUpper();
                                HMData.C2Name =             (DBNull.Value.Equals(dr3["C2Name"])) ? "" : dr3.GetString(9).ToUpper();
                                HMData.C2Mobile =           (DBNull.Value.Equals(dr3["C2Mobile"])) ? "" : dr3.GetString(10).ToUpper();
                                HMData.C2DOB =              (DBNull.Value.Equals(dr3["C2DOB"])) ? "" : dr3.GetDateTime(11).ToString().ToUpper();
                                HMData.C2Age =              (DBNull.Value.Equals(dr3["C2Age"])) ? "" : dr3.GetInt32(12).ToString().ToUpper();
                                HMData.C2IdProofType =      (DBNull.Value.Equals(dr3["C2IdProofType"])) ? "" : dr3.GetString(13).ToUpper();
                                HMData.C2IdProofNo =        (DBNull.Value.Equals(dr3["C2IdProofNo"])) ? "" : dr3.GetString(14).ToUpper();
                                HMData.C2AddressProofType = (DBNull.Value.Equals(dr3["C2AddressProofType"])) ? "" : dr3.GetString(15).ToUpper();
                                HMData.C2AddressProofNo =   (DBNull.Value.Equals(dr3["C2AddressProofNo"])) ? "" : dr3.GetString(16).ToUpper();
                                HMData.C2MonthlyIncome =      (DBNull.Value.Equals(dr3["C2MonthlyIncome"])) ? "" : dr3.GetInt32(17).ToString().ToUpper();
                                HMData.C1Gender= (DBNull.Value.Equals(dr3["C1Gender"])) ? "" : dr3.GetString(18).ToString().ToUpper();
                                HMData.C2Gender= (DBNull.Value.Equals(dr3["C2Gender"])) ? "" : dr3.GetString(19).ToString().ToUpper();
                            }
                        }
                        dr3.Close();

                        
                        ReportList.Add(HMData);
                    }    
                }
            }
            return ReportList;
        }

        private static string RelationShipCal(string Relation)
        {
            string Result="";

            switch(Relation)
            {
                case "MOTHER":
                    Result = "DAUGHTER";
                    break;
                case "FATHER":
                    Result = "DAUGHTER";
                    break;
                case "HUSBAND":
                    Result = "WIFE";
                    break;
                case "SON":
                    Result = "MOTHER";
                    break;
                case "DAUGHTER":
                    Result = "MOTHER";
                    break;
                case "BROTHER":
                    Result = "SISTER";
                    break;
                case "SISTER":
                    Result = "SISTER";
                    break;
            }
            return Result;
        }

        public static void ChangeLoanStatus(string ReqID, int StatusCode)
        {
            using (SqlConnection sqlconn = new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "Update LoanApplication Set LoanStatus='" + StatusCode + "' where Requestid='" + ReqID + "' ";
                    sqlcomm.ExecuteNonQuery();
                }
            }
        }


        public static void UpdateStatusToExportExcel(List<HimarkRequestView> RequestList,int Code)
        {
            using (SqlConnection sqlconn=new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    foreach(HimarkRequestView hm in RequestList)
                    {
                        sqlcomm.CommandText = "update LoanApplication set LoanStatus='"+Code+"' where RequestId='" + hm.RequestID + "'";
                        sqlcomm.ExecuteNonQuery();
                    }
                }
                sqlconn.Close();
            }
        }
        public static void UpdateStatusToExportExcel(List<string> RequestList, int Code)
        {
            using (SqlConnection sqlconn = new SqlConnection(MicroFinance.Properties.Settings.Default.DBConnection))
            {
                sqlconn.Open();
                if (sqlconn.State == ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    foreach (string hm in RequestList)
                    {
                        sqlcomm.CommandText = "update LoanApplication set LoanStatus='" + Code + "' where RequestId='" + hm + "'";
                        sqlcomm.ExecuteNonQuery();
                    }
                }
                sqlconn.Close();
            }
        }

    }
}
