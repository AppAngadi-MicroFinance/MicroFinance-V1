using MicroFinance.ReportExports.Models;
using MicroFinance.ReportExports.ReportTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.ReportExports
{
    class GTReport
    {
        string ConnectionString = string.Empty;
        LoanRepository LoanRepos;
        string BaseDirectory = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "REPORTS\\");
        
        public GTReport(string globalConnectionString)
        {
            this.ConnectionString = globalConnectionString;
            this.LoanRepos = new LoanRepository(this.ConnectionString);
        }
        //
        public List<string> ApplicationLoginReport(DateRange Range)
        {
            string Center_FilePath = BaseDirectory + "Application Logins Centerwise " + Range.RangeString + ".xlsx";
            string Employee_FilePath = BaseDirectory + "Application Logins Employeewise " + Range.RangeString + ".xlsx";
            string Region_FilePath = BaseDirectory + "Application Logins Regionwise " + Range.RangeString + ".xlsx";
            List<string> FilePaths = new List<string>() { Center_FilePath, Employee_FilePath, Region_FilePath};
            CleanFilePaths(FilePaths);
            // Customer Application
            ApplicationLoginReport logins = new ApplicationLoginReport(LoanRepos, Range);
            ReportTool.ReportFormat1(Center_FilePath, BranchId_Heading, BranchName_Heading, CenterId_Heading, CenterName_Heading, logins.MonthPeriods, logins.CenterWise_Applications);
            ReportTool.ReportFormat1(Employee_FilePath, BranchId_Heading, BranchName_Heading, EmployeeId_Heading, EmployeeName_Heading, logins.MonthPeriods, logins.EmployeeWise_Applications);
            ReportTool.ReportFormat1(Region_FilePath, RegionId_Heading, RegionName_Heading, BranchId_Heading, BranchName_Heading, logins.MonthPeriods, logins.RegionWise_Applications);

            return FilePaths;
        }
        //
        public List<string> AccountClosingReport(DateRange Range)
        {
            string Center_FilePath = BaseDirectory + "Account closings Centerwise " + Range.RangeString + ".xlsx";
            string Employee_FilePath = BaseDirectory + "Account closings Employeewise " + Range.RangeString + ".xlsx";
            string Region_FilePath = BaseDirectory + "Account closings Regionwise " + Range.RangeString + ".xlsx";
            List<string> FilePaths = new List<string>() { Center_FilePath, Employee_FilePath, Region_FilePath };
            CleanFilePaths(FilePaths);
            // Account closing 
            AccountClosingReport closings = new AccountClosingReport(LoanRepos, Range);
            ReportTool.ReportFormat1(Center_FilePath, BranchId_Heading, BranchName_Heading, CenterId_Heading, CenterName_Heading, closings.MonthPeriods, closings.CenterWise_Data);
            ReportTool.ReportFormat1(Employee_FilePath, BranchId_Heading, BranchName_Heading, EmployeeId_Heading, EmployeeName_Heading, closings.MonthPeriods, closings.EmployeeWise_Data);
            ReportTool.ReportFormat1(Region_FilePath, RegionId_Heading, RegionName_Heading, BranchId_Heading, BranchName_Heading, closings.MonthPeriods, closings.BranchWise_Data);

            return FilePaths;
        }
        //
        public List<string> OutstandingReport(DateRange Range)
        {
            string Center_FilePath = BaseDirectory + "Outstanding Amount Centerwise " + Range.RangeString + ".xlsx";
            string Employee_FilePath = BaseDirectory + "Outstanding Amount Employeewise " + Range.RangeString + ".xlsx";
            string Region_FilePath = BaseDirectory + "Outstanding Amount Regionwise " + Range.RangeString + ".xlsx";
            List<string> FilePaths = new List<string>() { Center_FilePath, Employee_FilePath, Region_FilePath };
            CleanFilePaths(FilePaths);
            //Outstanding amount report
            OutStandingReport outstanding = new OutStandingReport(LoanRepos, Range);
            ReportTool.ReportFormat1(Center_FilePath, BranchId_Heading, BranchName_Heading, CenterId_Heading, CenterName_Heading, outstanding.MonthPeriods, outstanding.CenterWise_OData);
            ReportTool.ReportFormat1(Employee_FilePath, BranchId_Heading, BranchName_Heading, EmployeeId_Heading, EmployeeName_Heading, outstanding.MonthPeriods, outstanding.EmployeeWise_OData);
            ReportTool.ReportFormat1(Region_FilePath, RegionId_Heading, RegionName_Heading, BranchId_Heading, BranchName_Heading, outstanding.MonthPeriods, outstanding.BranchWise_OData);

            return FilePaths;
        }
        //
        public List<string> ApplicationHighmarkRejection(DateRange Range)
        {
            string Center_FilePath = BaseDirectory + "Highmark rejection Centerwise " + Range.RangeString + ".xlsx";
            string Employee_FilePath = BaseDirectory + "Highmark rejection Employeewise " + Range.RangeString + ".xlsx";
            string Region_FilePath = BaseDirectory + "Highmark rejection Regionwise " + Range.RangeString + ".xlsx";
            List<string> FilePaths = new List<string>() { Center_FilePath, Employee_FilePath, Region_FilePath };
            CleanFilePaths(FilePaths);
            // Application Highmark rejection
            HighmarkReport himarkRejection = new HighmarkReport(LoanRepos, Range,false);
            ReportTool.ReportFormat1(Center_FilePath, BranchId_Heading, BranchName_Heading, CenterId_Heading, CenterName_Heading, himarkRejection.MonthPeriods, himarkRejection.CenterWise_RejectionData);
            ReportTool.ReportFormat1(Employee_FilePath, BranchId_Heading, BranchName_Heading, EmployeeId_Heading, EmployeeName_Heading, himarkRejection.MonthPeriods, himarkRejection.EmployeeWise_RejectionData);
            ReportTool.ReportFormat1(Region_FilePath, RegionId_Heading, RegionName_Heading, BranchId_Heading, BranchName_Heading, himarkRejection.MonthPeriods, himarkRejection.RegionWise_RejectionData);

            return FilePaths;
        }
        //
        public List<string> ApplicationHighmarkApproved(DateRange Range)
        {
            string Center_FilePath = BaseDirectory + "Highmark approved Centerwise " + Range.RangeString + ".xlsx";
            string Employee_FilePath = BaseDirectory + "Highmark approved Employeewise " + Range.RangeString + ".xlsx";
            string Region_FilePath = BaseDirectory + "Highmark approved Regionwise " + Range.RangeString + ".xlsx";
            List<string> FilePaths = new List<string>() { Center_FilePath, Employee_FilePath, Region_FilePath };
            CleanFilePaths(FilePaths);
            //
            HighmarkReport himarkRejection = new HighmarkReport(LoanRepos, Range,true);
            ReportTool.ReportFormat1(Center_FilePath, BranchId_Heading, BranchName_Heading, CenterId_Heading, CenterName_Heading, himarkRejection.MonthPeriods, himarkRejection.CenterWise_ApprovedData);
            ReportTool.ReportFormat1(Employee_FilePath, BranchId_Heading, BranchName_Heading, EmployeeId_Heading, EmployeeName_Heading, himarkRejection.MonthPeriods, himarkRejection.EmployeeWise_ApprovedData);
            ReportTool.ReportFormat1(Region_FilePath, RegionId_Heading, RegionName_Heading, BranchId_Heading, BranchName_Heading, himarkRejection.MonthPeriods, himarkRejection.RegionWise_ApprovedData);

            return FilePaths;
        }
        //
        public List<string> LoanAmount(DateRange Range)
        {
            string Center_FilePath = BaseDirectory + "Loan Amount Centerwise " + Range.RangeString + ".xlsx";
            string Employee_FilePath = BaseDirectory + "Loan Amount Employeewise " + Range.RangeString + ".xlsx";
            string Region_FilePath = BaseDirectory + "Loan Amount Logins Regionwise " + Range.RangeString + ".xlsx";
            List<string> FilePaths = new List<string>() { Center_FilePath, Employee_FilePath, Region_FilePath };
            CleanFilePaths(FilePaths);
            // Loan Amount details
            LoanAmountReport TotalLoanAmountReport = new LoanAmountReport(LoanRepos, Range);
            ReportTool.ReportFormat1(Center_FilePath, BranchId_Heading, BranchName_Heading, CenterId_Heading, CenterName_Heading, TotalLoanAmountReport.MonthPeriods, TotalLoanAmountReport.CenterWise_AmountData);
            ReportTool.ReportFormat1(Employee_FilePath, BranchId_Heading, BranchName_Heading, EmployeeId_Heading, EmployeeName_Heading, TotalLoanAmountReport.MonthPeriods, TotalLoanAmountReport.EmployeeWise_AmountData);
            ReportTool.ReportFormat1(Region_FilePath, RegionId_Heading, RegionName_Heading, BranchId_Heading, BranchName_Heading, TotalLoanAmountReport.MonthPeriods, TotalLoanAmountReport.RegionWise_AmountData);

            return FilePaths;
        }
        //
        public List<string> LoanRecovery(DateRange Range)
        {
            string Center_FilePath = BaseDirectory + "Loan Recovery Centerwise " + Range.RangeString + ".xlsx";
            string Employee_FilePath = BaseDirectory + "Loan Recovery Employeewise " + Range.RangeString + ".xlsx";
            string Region_FilePath = BaseDirectory + "Loan Recovery Regionwise " + Range.RangeString + ".xlsx";
            List<string> FilePaths = new List<string>() { Center_FilePath, Employee_FilePath, Region_FilePath };
            CleanFilePaths(FilePaths);
            //Loan Recovery details
            CollectionReport CollectionReportData = new CollectionReport(LoanRepos, Range);
            ReportTool.ReportFormat1(Center_FilePath, BranchId_Heading, BranchName_Heading, CenterId_Heading, CenterName_Heading, CollectionReportData.MonthPeriods, CollectionReportData.CenterWise_CollectedData);
            ReportTool.ReportFormat1(Employee_FilePath, BranchId_Heading, BranchName_Heading, EmployeeId_Heading, EmployeeName_Heading, CollectionReportData.MonthPeriods, CollectionReportData.EmployeeWise_CollectedData);
            ReportTool.ReportFormat1(Region_FilePath, RegionId_Heading, RegionName_Heading, BranchId_Heading, BranchName_Heading, CollectionReportData.MonthPeriods, CollectionReportData.DistrictWise_CollectedData);

            return FilePaths;
        }
        public List<string> LoanDisbursement(DateRange Range)
        {
            string Center_FilePath = BaseDirectory + "Loan Disbursement Centerwise " + Range.RangeString + ".xlsx";
            string Employee_FilePath = BaseDirectory + "Loan Disbursement Employeewise " + Range.RangeString + ".xlsx";
            string Region_FilePath = BaseDirectory + "Loan Disbursement Regionwise " + Range.RangeString + ".xlsx";
            List<string> FilePaths = new List<string>() { Center_FilePath, Employee_FilePath, Region_FilePath };
            CleanFilePaths(FilePaths);
            // Loan Disbursement details
            DisbursementReport disbursementReport = new DisbursementReport(LoanRepos, Range);
            ReportTool.ReportFormat1(Center_FilePath, BranchId_Heading, BranchName_Heading, CenterId_Heading, CenterName_Heading, disbursementReport.MonthPeriods, disbursementReport.CenterWise_DisbursmentData);
            ReportTool.ReportFormat1(Employee_FilePath, BranchId_Heading, BranchName_Heading, EmployeeId_Heading, EmployeeName_Heading, disbursementReport.MonthPeriods, disbursementReport.EmployeeWise_DisbursmentData);
            ReportTool.ReportFormat1(Region_FilePath, RegionId_Heading, RegionName_Heading, BranchId_Heading, BranchName_Heading, disbursementReport.MonthPeriods, disbursementReport.RegionWise_DisbursmentData);

            return FilePaths;
        }

        public List<string> POSReport(DateRange Range)
        {
            string POS_reportPath = BaseDirectory + "POS Report " + Range.RangeString + ".xlsx";
            List<string> FilePaths = new List<string>() { POS_reportPath};
            CleanFilePaths(FilePaths);

            POSReportTool POS_Report = new POSReportTool();
            return new List<string>();
        }
        void CleanFilePaths(List<string> filePaths)
        {
            foreach(string path in filePaths)
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
        }
        string BranchId_Heading = "Branch ID";
        string BranchName_Heading = "Branch Name";
        string CenterId_Heading = "Center ID";
        string CenterName_Heading = "Center Name";
        string RegionId_Heading = "Region ID";
        string RegionName_Heading = "Region Name";
        string EmployeeId_Heading = "Employee ID";
        string EmployeeName_Heading = "Employee Name";
    }
}
