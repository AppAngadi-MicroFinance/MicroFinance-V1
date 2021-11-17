using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using MicroFinance.Modal;
using Microsoft.Office.Interop.Excel;
using MicroFinance.Reports;

namespace MicroFinance.Modal
{
    class GTtoSamunnati
    {

        public List<SamuunatiReport> GetDetails()
        {
            List<SamuunatiReport> CustomerDetails = new List<SamuunatiReport>();
            List<string> CustomerIdList = new List<string>();
            using(SqlConnection sql=new SqlConnection(Properties.Settings.Default.db))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                command.CommandText = "select CustId from LoanApplication where LoanStatus = '10'";
                SqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    CustomerIdList.Add(reader.GetString(0));
                }
                reader.Close();
                foreach(string id in CustomerIdList)
                {
                    SamuunatiReport gs = new SamuunatiReport();
                    command.CommandText = "select Name,Dob,Mobile,Address,Pincode,AadharNumber,BankName,BankBranchName,IFSCCode,BankACHolderName,BankAccountNo,Gender from CustomerDetails where CustId='"+id+"'";
                    reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        gs.CustomerName = reader.GetString(0);
                        gs.DOB = reader.GetDateTime(1).ToString("dd-MMMM-yyyy");
                        gs.Phone = reader.GetString(2);
                        string[] _fullAdress = reader.GetString(3).Split('|', '~');
                        gs.Address1 = _fullAdress[0];
                        gs.Address2 = _fullAdress[2];
                        gs.Address3 = _fullAdress[4];
                        gs.Address4 = _fullAdress[6];
                        gs.City = _fullAdress[8];
                        gs.State = _fullAdress[10];
                        gs.PostalCode = reader.GetInt32(4).ToString();
                        gs.AadharNo = reader.GetString(5);
                        gs.BankName = reader.GetString(6);
                        gs.BankBranchName = reader.GetString(7);
                        gs.IFSCcode = reader.GetString(8);
                        gs.BankAccName = reader.GetString(9);
                        gs.BankAccNo = reader.GetString(10);
                        gs.Gender = reader.GetString(11);
                    }
                    reader.Close();
                    command.CommandText = "select Mobile from GuarenteeDetails where CustId = '"+id+"'";
                    gs.PANTANno = command.ExecuteScalar().ToString();
                    command.CommandText = "select LoanApplication.RequestId,LoanApplication.LoanAmount,HimarkResult.ReportID from LoanApplication,HimarkResult where CustId='"+id+"' and LoanApplication.RequestId = HimarkResult.RequestID";
                    reader = command.ExecuteReader();
                    while(reader.Read())
                    {
                        gs.LoanAmount = reader.GetInt32(1);
                        gs.ReportID = reader.GetString(2);
                    }
                    reader.Close();
                    //default values

                    gs.OperatingUnit = "Samunnati_OU";
                    gs.HMRefernceNo = DateTime.Today.ToString("yyyy") + "_" + DateTime.Today.ToString("MM") + "_" + DateTime.Today.ToString("dd");
                    gs.SamunnatiBranchMapping = "Thiruvanmiyur - BAID";
                    gs.CustomerType = "CBO";
                    gs.CustomerSubtype = "CBO MEMBER";
                    gs.BusinessCategory = "Micro";
                    gs.BusinessIndustryType = "Agriculture";
                    gs.Sector = "AGRI INPUT";
                    gs.PrimaryValueChain = "SEEDS";
                    gs.SecondaryValueChain = "SEEDS";
                    gs.EmailID = "gtrust2007@yahoo.in";
                    gs.Constitution = "Individual";
                    gs.ExistingRelationshipValue = "S0628";
                    gs.NewCustomerAcquisition = "S0628";
                    gs.BankAccLevel = "Member Bank";
                    gs.SalesPesonName = "S0628";
                    gs.LoanProduct = "STL-DL";
                    gs.LoanTenure = "12";
                    gs.LoanTermPeriod = "MONTHS";
                    gs.LoanType = "DIRECT";
                    gs.LoanStartDate = DateTime.Today.ToString("dd-MMMM-yyyy");
                    gs.AmortizationMethod = "EQUAL_PAYMENT";
                    gs.PaymentStartDate = DateTime.Today.AddDays(7).ToString("dd-MMMM-yyyy");
                    gs.PaymentFrequency = "WEEKLY";
                    gs.IntrestStartDate = DateTime.Today.AddDays(7).ToString("dd-MMMM-yyyy");
                    gs.IntrestFrequency ="WEEKLY";
                    gs.LoanIntrestRate = "24";
                    gs.ProcessingFeesPercentage = "1";
                    gs.InsuranceFees1 = "PAI";
                    gs.InsuranceFees2 = "GPL";
                    gs.PenalIntrestRate = "2";
                    CustomerDetails.Add(gs);
                }

            }
            return CustomerDetails;
        }
        public List<SamuunatiReport> GetDetails(List<string> CustomerIds)
        {
            List<SamuunatiReport> CustomerDetails = new List<SamuunatiReport>();
            List<string> CustomerIdList = CustomerIds;
            using (SqlConnection sql = new SqlConnection(Properties.Settings.Default.db))
            {
                sql.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = sql;
                
                foreach (string id in CustomerIdList)
                {
                    SamuunatiReport gs = new SamuunatiReport();
                    command.CommandText = "select Name,Dob,Mobile,Address,Pincode,AadharNumber,BankName,BankBranchName,IFSCCode,BankACHolderName,BankAccountNo,Gender from CustomerDetails where CustId='" + id + "'";
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        gs.CustomerName = reader.GetString(0);
                        gs.DOB = reader.GetDateTime(1).ToString("dd-MMMM-yyyy");
                        gs.Phone = reader.GetString(2);
                        string[] _fullAdress = reader.GetString(3).Split('|', '~');
                        gs.Address1 = _fullAdress[0];
                        gs.Address2 = _fullAdress[2];
                        gs.Address3 = _fullAdress[4];
                        gs.Address4 = _fullAdress[6];
                        gs.City = _fullAdress[8];
                        gs.State = _fullAdress[10];
                        gs.PostalCode = reader.GetInt32(4).ToString();
                        gs.AadharNo = reader.GetString(5);
                        gs.BankName = reader.GetString(6);
                        gs.BankBranchName = reader.GetString(7);
                        gs.IFSCcode = reader.GetString(8);
                        gs.BankAccName = reader.GetString(9);
                        gs.BankAccNo = reader.GetString(10);
                        gs.Gender = reader.GetString(11);
                    }
                    reader.Close();
                    command.CommandText = "select Mobile from GuarenteeDetails where CustId = '" + id + "'";
                    gs.PANTANno = command.ExecuteScalar().ToString();
                    command.CommandText = "select LoanApplication.RequestId,LoanApplication.LoanAmount,HimarkResult.ReportID from LoanApplication,HimarkResult where CustId='" + id + "' and LoanApplication.RequestId = HimarkResult.RequestID";
                    reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        gs.LoanAmount = reader.GetInt32(1);
                        gs.ReportID = reader.GetString(2);
                    }
                    reader.Close();
                    //default values

                    gs.OperatingUnit = "Samunnati_OU";
                    gs.HMRefernceNo = DateTime.Today.ToString("yyyy") + "_" + DateTime.Today.ToString("MM") + "_" + DateTime.Today.ToString("dd");
                    gs.SamunnatiBranchMapping = "Thiruvanmiyur - BAID";
                    gs.CustomerType = "CBO";
                    gs.CustomerSubtype = "CBO MEMBER";
                    gs.BusinessCategory = "Micro";
                    gs.BusinessIndustryType = "Agriculture";
                    gs.Sector = "AGRI INPUT";
                    gs.PrimaryValueChain = "SEEDS";
                    gs.SecondaryValueChain = "SEEDS";
                    gs.EmailID = "gtrust2007@yahoo.in";
                    gs.Constitution = "Individual";
                    gs.ExistingRelationshipValue = "S0628";
                    gs.NewCustomerAcquisition = "S0628";
                    gs.BankAccLevel = "Member Bank";
                    gs.SalesPesonName = "S0628";
                    gs.LoanProduct = "STL-DL";
                    gs.LoanTenure = "12";
                    gs.LoanTermPeriod = "MONTHS";
                    gs.LoanType = "DIRECT";
                    gs.LoanStartDate = DateTime.Today.ToString("dd-MMMM-yyyy");
                    gs.AmortizationMethod = "EQUAL_PAYMENT";
                    gs.PaymentStartDate = DateTime.Today.AddDays(7).ToString("dd-MMMM-yyyy");
                    gs.PaymentFrequency = "WEEKLY";
                    gs.IntrestStartDate = DateTime.Today.AddDays(7).ToString("dd-MMMM-yyyy");
                    gs.IntrestFrequency = "WEEKLY";
                    gs.LoanIntrestRate = "24";
                    gs.ProcessingFeesPercentage = "1";
                    gs.InsuranceFees1 = "PAI";
                    gs.InsuranceFees2 = "GPL";
                    gs.PenalIntrestRate = "2";
                    CustomerDetails.Add(gs);
                }

            }
            return CustomerDetails;
        }


        //public List<SamunnatiResult> samunnatiList = new List<SamunnatiResult>();
        //public List<String> dates = new List<String>();
        //public List<int> Amt = new List<int>();

        //public void GetDetails(string Path)
        //{
        //    Excel.Application application;
        //    Excel.Workbook workbook;
        //    StringBuilder sb = new StringBuilder();
        //    int SC;
        //    string Sheetname = null;

        //    application = new Excel.Application();
        //    workbook = application.Workbooks.Open(Path);

        //    SC = workbook.Worksheets.Count;
        //    foreach (Excel.Worksheet sheet in workbook.Worksheets)
        //    {
        //        if (sheet.Name.Equals("Sheet2", StringComparison.CurrentCultureIgnoreCase))
        //        {
        //            Excel.Range usedrange = sheet.UsedRange;
        //            Sheetname = sheet.Name;
        //            //GetDataFromExcel(sheet);
        //            break;
        //        }

        //    }
        //    workbook.Close();
        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
        //    application.Quit();
        //    System.Runtime.InteropServices.Marshal.ReleaseComObject(application);

        //}

        //public void GetDataFromExcel(Excel.Worksheet worksheet)
        //{
        //    Excel.Range userange = worksheet.UsedRange;
        //    int rowcount = userange.Rows.Count;
        //    int loandatecolumn = 0;
        //    foreach (DataRow Row in worksheet.Rows)
        //    {
        //        loandatecolumn++;
        //        if (Row["Loan Start Date"].ToString() == "Loan Start Date")
        //        {
        //            break;
        //        }
        //    }

        //    for (int Rownumber = 2; Rownumber <= rowcount; Rownumber++)
        //    {
        //        string _date = (worksheet.Cells[Rownumber, loandatecolumn] as Excel.Range).Value;
        //        int _amount = (int)(worksheet.Cells[Rownumber, 39] as Excel.Range).Value;
        //        samunnatiList.Add(
        //            new SamunnatiResult
        //            {
        //                OperatingUnit = (worksheet.Cells[Rownumber, 1] as Excel.Range).Value,
        //                CustomerNumber = (worksheet.Cells[Rownumber, 2] as Excel.Range).Value,
        //                ReportID = (worksheet.Cells[Rownumber, 3] as Excel.Range).Value,
        //                HMRefernceNo = (worksheet.Cells[Rownumber, 4] as Excel.Range).Value,
        //                CustomerName = (worksheet.Cells[Rownumber, 5] as Excel.Range).Value,
        //                SamunnatiBranchMapping = (worksheet.Cells[Rownumber, 6] as Excel.Range).Value,
        //                CustomerType = (worksheet.Cells[Rownumber, 7] as Excel.Range).Value,
        //                CustomerSubtype = (worksheet.Cells[Rownumber, 8] as Excel.Range).Value,
        //                Gender = (worksheet.Cells[Rownumber, 9] as Excel.Range).Value,
        //                DOB = (worksheet.Cells[Rownumber, 10] as Excel.Range).Value,
        //                BusinessCategory = (worksheet.Cells[Rownumber, 11] as Excel.Range).Value,
        //                BusinessIndustryType = (worksheet.Cells[Rownumber, 12] as Excel.Range).Value,
        //                Sector = (worksheet.Cells[Rownumber, 13] as Excel.Range).Value,
        //                PrimaryValueChain = (worksheet.Cells[Rownumber, 14] as Excel.Range).Value,
        //                SecondaryValueChain = (worksheet.Cells[Rownumber, 15] as Excel.Range).Value,
        //                Phone = (worksheet.Cells[Rownumber, 16] as Excel.Range).Value,
        //                Address1 = (worksheet.Cells[Rownumber, 17] as Excel.Range).Value,
        //                Address2 = (worksheet.Cells[Rownumber, 18] as Excel.Range).Value,
        //                Address3 = (worksheet.Cells[Rownumber, 19] as Excel.Range).Value,
        //                Address4 = (worksheet.Cells[Rownumber, 20] as Excel.Range).Value,
        //                City = (worksheet.Cells[Rownumber, 21] as Excel.Range).Value,
        //                PostalCode = (worksheet.Cells[Rownumber, 22] as Excel.Range).Value,
        //                State = (worksheet.Cells[Rownumber, 23] as Excel.Range).Value,
        //                EmailID = (worksheet.Cells[Rownumber, 24] as Excel.Range).Value,
        //                Constitution = (worksheet.Cells[Rownumber, 25] as Excel.Range).Value,
        //                GSTINno = (worksheet.Cells[Rownumber, 26] as Excel.Range).Value,
        //                PANTANno = (worksheet.Cells[Rownumber, 27] as Excel.Range).Value,
        //                AadharNo = (worksheet.Cells[Rownumber, 28] as Excel.Range).Value,
        //                ExistingRelationshipValue = (worksheet.Cells[Rownumber, 29] as Excel.Range).Value,
        //                NewCustomerAcquisition = (worksheet.Cells[Rownumber, 30] as Excel.Range).Value,
        //                BankName = (worksheet.Cells[Rownumber, 31] as Excel.Range).Value,
        //                BankBranchName = (worksheet.Cells[Rownumber, 32] as Excel.Range).Value,
        //                IFSCcode = (worksheet.Cells[Rownumber, 33] as Excel.Range).Value,
        //                BankAccName = (worksheet.Cells[Rownumber, 34] as Excel.Range).Value,
        //                BankAccLevel = (worksheet.Cells[Rownumber, 35] as Excel.Range).Value,
        //                BankAccNo = (worksheet.Cells[Rownumber, 36] as Excel.Range).Value,
        //                SalesPesonName = (worksheet.Cells[Rownumber, 37] as Excel.Range).Value,
        //                LoanProduct = (worksheet.Cells[Rownumber, 38] as Excel.Range).Value,
        //                LoanAmount = (int)(worksheet.Cells[Rownumber, 39] as Excel.Range).Value,
        //                LoanTenure = (worksheet.Cells[Rownumber, 40] as Excel.Range).Value,
        //                LoanTermPeriod = (worksheet.Cells[Rownumber, 41] as Excel.Range).Value,
        //                LoanType = (worksheet.Cells[Rownumber, 42] as Excel.Range).Value,
        //                LoanStartDate = (worksheet.Cells[Rownumber, 43] as Excel.Range).Value,
        //                AmortizationMethod = (worksheet.Cells[Rownumber, 44] as Excel.Range).Value,
        //                PaymentStartDate = (worksheet.Cells[Rownumber, 45] as Excel.Range).Value,
        //                PaymentFrequency = (worksheet.Cells[Rownumber, 46] as Excel.Range).Value,
        //                IntrestStartDate = (worksheet.Cells[Rownumber, 47] as Excel.Range).Value,
        //                IntrestFrequency = (worksheet.Cells[Rownumber, 48] as Excel.Range).Value,
        //                LoanIntrestRate = (worksheet.Cells[Rownumber, 49] as Excel.Range).Value,
        //                ProcessingFeesPercentage = (worksheet.Cells[Rownumber, 50] as Excel.Range).Value,
        //                InsuranceFees1 = (worksheet.Cells[Rownumber, 51] as Excel.Range).Value,
        //                InsuranceFees1Amt = (worksheet.Cells[Rownumber, 52] as Excel.Range).Value,
        //                InsuranceFees2 = (worksheet.Cells[Rownumber, 53] as Excel.Range).Value,
        //                InsuranceFees2Amt = (worksheet.Cells[Rownumber, 54] as Excel.Range).Value,
        //                PenalIntrestRate = (worksheet.Cells[Rownumber, 55] as Excel.Range).Value,
        //            }); 
        //        if (!dates.Contains(_date))
        //        {
        //            dates.Add(_date);
        //        }
        //        if (!Amt.Contains(_amount))
        //        {
        //            Amt.Add(Convert.ToInt32(_amount));
        //        }
        //    }
        //}



        public void GenerateSamunnati_File(List<string> CustomerList)
        {
            #region FileWriteSection
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Columns.AutoFit();
            xlWorkSheet.Cells.WrapText = true;
            xlWorkSheet.Columns.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            xlWorkSheet.Columns.VerticalAlignment = XlHAlign.xlHAlignCenter;
            //Operating Unit
            var operatingunit = xlWorkSheet.Cells.Range["A1"];
            operatingunit.Value = "Operating Unit";
            operatingunit.Font.Bold = true;
            operatingunit.Font.Size = 11;
            operatingunit.RowHeight = 46;
            operatingunit.Columns.ColumnWidth = 14;
            operatingunit.Borders.ColorIndex = 15;
            operatingunit.Cells.Interior.ColorIndex = 6;
            operatingunit.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            operatingunit.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Customer Number
            var CusNo = xlWorkSheet.Cells.Range["B1"];
            CusNo.Value = "Customer Number";
            CusNo.Font.Bold = true;
            CusNo.Font.Size = 11;
            CusNo.RowHeight = 15;
            CusNo.Columns.ColumnWidth = 17;
            CusNo.Borders.ColorIndex = 15;
            CusNo.Cells.Interior.ColorIndex = 6;
            CusNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            CusNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Report ID
            var reportid = xlWorkSheet.Cells.Range["C1"];
            reportid.Value = "Report ID";
            reportid.Font.Bold = true;
            reportid.Font.Size = 11;
            reportid.RowHeight = 15;
            reportid.Columns.ColumnWidth = 24;
            reportid.Borders.ColorIndex = 15;
            reportid.Cells.Interior.ColorIndex = 6;
            reportid.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            reportid.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //H.M Reference no
            var hmrefernce = xlWorkSheet.Cells.Range["D1"];
            hmrefernce.Value = "H.M Reference No";
            hmrefernce.Font.Bold = true;
            hmrefernce.Font.Size = 11;
            hmrefernce.RowHeight = 15;
            hmrefernce.Columns.ColumnWidth = 16;
            reportid.Borders.ColorIndex = 15;
            hmrefernce.Cells.Interior.ColorIndex = 6;
            hmrefernce.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            hmrefernce.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Customer Name
            var cusname = xlWorkSheet.Cells.Range["E1"];
            cusname.Value = "Customer Name";
            cusname.Font.Bold = true;
            cusname.Font.Size = 11;
            cusname.RowHeight = 15;
            cusname.Columns.ColumnWidth = 35;
            cusname.Borders.ColorIndex = 15;
            cusname.Cells.Interior.ColorIndex = 6;
            cusname.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            cusname.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Samunnati Branch Mapping
            var Samunnatibranchmapping = xlWorkSheet.Cells.Range["F1"];
            Samunnatibranchmapping.Value = "Samunnati Branch Mapping";
            Samunnatibranchmapping.Font.Bold = true;
            Samunnatibranchmapping.Font.Size = 11;
            Samunnatibranchmapping.RowHeight = 15;
            Samunnatibranchmapping.Columns.ColumnWidth = 30;
            Samunnatibranchmapping.Borders.ColorIndex = 15;
            Samunnatibranchmapping.Cells.Interior.ColorIndex = 6;
            Samunnatibranchmapping.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Samunnatibranchmapping.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Customer Type
            var custype = xlWorkSheet.Cells.Range["G1"];
            custype.Value = "Customer Type";
            custype.Font.Bold = true;
            custype.Font.Size = 11;
            custype.RowHeight = 15;
            custype.Columns.ColumnWidth = 30;
            custype.Borders.ColorIndex = 15;
            custype.Cells.Interior.ColorIndex = 6;
            custype.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            custype.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Customer Subtype
            var cussubtype = xlWorkSheet.Cells.Range["H1"];
            cussubtype.Value = "Customer SubType";
            cussubtype.Font.Bold = true;
            cussubtype.Font.Size = 11;
            cussubtype.RowHeight = 15;
            cussubtype.Columns.ColumnWidth = 25;
            cussubtype.Borders.ColorIndex = 15;
            cussubtype.Cells.Interior.ColorIndex = 6;
            cussubtype.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            cussubtype.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Gender
            var Gender = xlWorkSheet.Cells.Range["I1"];
            Gender.Value = "Gender";
            Gender.Font.Bold = true;
            Gender.Font.Size = 11;
            Gender.RowHeight = 15;
            Gender.Columns.ColumnWidth = 24;
            Gender.Borders.ColorIndex = 15;
            Gender.Cells.Interior.ColorIndex = 6;
            Gender.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Gender.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //DOB
            var dob = xlWorkSheet.Cells.Range["J1"];
            dob.Value = "Date OF Birth";
            dob.Font.Bold = true;
            dob.Font.Size = 11;
            dob.RowHeight = 15;
            dob.Columns.ColumnWidth = 24;
            dob.Borders.ColorIndex = 15;
            dob.Cells.Interior.ColorIndex = 6;
            dob.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            dob.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Business Category
            var businesscategory = xlWorkSheet.Cells.Range["K1"];
            businesscategory.Value = "Business Category";
            businesscategory.Font.Bold = true;
            businesscategory.Font.Size = 11;
            businesscategory.RowHeight = 15;
            businesscategory.Columns.ColumnWidth = 24;
            businesscategory.Borders.ColorIndex = 15;
            businesscategory.Cells.Interior.ColorIndex = 6;
            businesscategory.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            businesscategory.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Business / industry type
            var businessindtype = xlWorkSheet.Cells.Range["L1"];
            businessindtype.Value = "Business/Industry Type";
            businessindtype.Font.Bold = true;
            businessindtype.Font.Size = 11;
            businessindtype.RowHeight = 15;
            businessindtype.Columns.ColumnWidth = 23;
            reportid.Borders.ColorIndex = 15;
            businessindtype.Cells.Interior.ColorIndex = 6;
            businessindtype.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            businessindtype.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //sector
            var sector = xlWorkSheet.Cells.Range["M1"];
            sector.Value = "Sector";
            sector.Font.Bold = true;
            sector.Font.Size = 11;
            sector.RowHeight = 15;
            sector.Columns.ColumnWidth = 21;
            sector.Borders.ColorIndex = 15;
            sector.Cells.Interior.ColorIndex = 6;
            sector.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            sector.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Primary Value Chain
            var primaryvaluechain = xlWorkSheet.Cells.Range["N1"];
            primaryvaluechain.Value = "Primary Value Chain";
            primaryvaluechain.Font.Bold = true;
            primaryvaluechain.Font.Size = 11;
            primaryvaluechain.RowHeight = 15;
            primaryvaluechain.Columns.ColumnWidth = 23;
            primaryvaluechain.Borders.ColorIndex = 15;
            primaryvaluechain.Cells.Interior.ColorIndex = 6;
            primaryvaluechain.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            primaryvaluechain.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Secondary Value Chain
            var secondaryvaluechain = xlWorkSheet.Cells.Range["O1"];
            secondaryvaluechain.Value = "Secondary Value Chain";
            secondaryvaluechain.Font.Bold = true;
            secondaryvaluechain.Font.Size = 11;
            secondaryvaluechain.RowHeight = 15;
            secondaryvaluechain.Columns.ColumnWidth = 23;
            secondaryvaluechain.Borders.ColorIndex = 15;
            secondaryvaluechain.Cells.Interior.ColorIndex = 6;
            secondaryvaluechain.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            secondaryvaluechain.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Phone NO
            var phoneno = xlWorkSheet.Cells.Range["P1"];
            phoneno.Value = "Phone No";
            phoneno.Font.Bold = true;
            phoneno.Font.Size = 11;
            phoneno.RowHeight = 15;
            phoneno.Columns.ColumnWidth = 21;
            phoneno.Borders.ColorIndex = 15;
            phoneno.Cells.Interior.ColorIndex = 6;
            phoneno.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            phoneno.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Address1
            var address1 = xlWorkSheet.Cells.Range["Q1"];
            address1.Value = "Address 1";
            address1.Font.Bold = true;
            address1.Font.Size = 11;
            address1.RowHeight = 15;
            address1.Columns.ColumnWidth = 21;
            address1.Borders.ColorIndex = 15;
            address1.Cells.Interior.ColorIndex = 6;
            address1.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            address1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Address2
            var address2 = xlWorkSheet.Cells.Range["R1"];
            address2.Value = "Address 2";
            address2.Font.Bold = true;
            address2.Font.Size = 11;
            address2.RowHeight = 15;
            address2.Columns.ColumnWidth = 24;
            address2.Borders.ColorIndex = 15;
            address2.Cells.Interior.ColorIndex = 6;
            address2.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            address2.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Address3
            var address3 = xlWorkSheet.Cells.Range["S1"];
            address3.Value = "Address 3";
            address3.Font.Bold = true;
            address3.Font.Size = 11;
            address3.RowHeight = 15;
            address3.Columns.ColumnWidth = 24;
            address3.Borders.ColorIndex = 15;
            address3.Cells.Interior.ColorIndex = 6;
            address3.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            address3.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Address4
            var address4 = xlWorkSheet.Cells.Range["T1"];
            address4.Value = "Address 4";
            address4.Font.Bold = true;
            address4.Font.Size = 11;
            address4.RowHeight = 15;
            address4.Columns.ColumnWidth = 24;
            address4.Borders.ColorIndex = 15;
            address4.Cells.Interior.ColorIndex = 6;
            address4.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            address4.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //City
            var city = xlWorkSheet.Cells.Range["U1"];
            city.Value = "City";
            city.Font.Bold = true;
            city.Font.Size = 11;
            city.RowHeight = 15;
            city.Columns.ColumnWidth = 24;
            city.Borders.ColorIndex = 15;
            city.Cells.Interior.ColorIndex = 6;
            city.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            city.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Postal Code
            var postal = xlWorkSheet.Cells.Range["V1"];
            postal.Value = "Postal Code";
            postal.Font.Bold = true;
            postal.Font.Size = 11;
            postal.RowHeight = 15;
            postal.Columns.ColumnWidth = 24;
            postal.Borders.ColorIndex = 15;
            postal.Cells.Interior.ColorIndex = 6;
            postal.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            postal.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //State
            var State = xlWorkSheet.Cells.Range["W1"];
            State.Value = "State";
            State.Font.Bold = true;
            State.Font.Size = 11;
            State.RowHeight = 15;
            State.Columns.ColumnWidth = 24;
            State.Borders.ColorIndex = 15;
            State.Cells.Interior.ColorIndex = 6;
            State.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            State.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Email Id
            var emailid = xlWorkSheet.Cells.Range["X1"];
            emailid.Value = "Email ID";
            emailid.Font.Bold = true;
            emailid.Font.Size = 11;
            emailid.RowHeight = 15;
            emailid.Columns.ColumnWidth = 24;
            emailid.Borders.ColorIndex = 15;
            emailid.Cells.Interior.ColorIndex = 6;
            emailid.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            emailid.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Constitution
            var constitution = xlWorkSheet.Cells.Range["Y1"];
            constitution.Value = "Constitution";
            constitution.Font.Bold = true;
            constitution.Font.Size = 11;
            constitution.RowHeight = 15;
            constitution.Columns.ColumnWidth = 24;
            constitution.Borders.ColorIndex = 15;
            constitution.Cells.Interior.ColorIndex = 6;
            constitution.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            constitution.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //GSTIN Number
            var gstin = xlWorkSheet.Cells.Range["Z1"];
            gstin.Value = "GSTIN Number";
            gstin.Font.Bold = true;
            gstin.Font.Size = 11;
            gstin.RowHeight = 15;
            gstin.Columns.ColumnWidth = 24;
            gstin.Borders.ColorIndex = 15;
            gstin.Cells.Interior.ColorIndex = 6;
            gstin.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            gstin.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Pan/tan Number
            var PANno = xlWorkSheet.Cells.Range["AA1"];
            PANno.Value = "PAN / TAN Number";
            PANno.Font.Bold = true;
            PANno.Font.Size = 11;
            PANno.RowHeight = 15;
            PANno.Columns.ColumnWidth = 24;
            PANno.Borders.ColorIndex = 15;
            PANno.Cells.Interior.ColorIndex = 6;
            PANno.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            PANno.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Aadhar Number
            var aadharNo = xlWorkSheet.Cells.Range["AB1"];
            aadharNo.Value = "Aadhar Number";
            aadharNo.Font.Bold = true;
            aadharNo.Font.Size = 11;
            aadharNo.RowHeight = 15;
            aadharNo.Columns.ColumnWidth = 24;
            aadharNo.Borders.ColorIndex = 15;
            aadharNo.Cells.Interior.ColorIndex = 6;
            aadharNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            aadharNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Existing Relationship Value
            var existingrelation = xlWorkSheet.Cells.Range["AC1"];
            existingrelation.Value = "Existing Relationship Value";
            existingrelation.Font.Bold = true;
            existingrelation.Font.Size = 11;
            existingrelation.RowHeight = 15;
            existingrelation.Columns.ColumnWidth = 25;
            existingrelation.Borders.ColorIndex = 15;
            existingrelation.Cells.Interior.ColorIndex = 6;
            existingrelation.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            existingrelation.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //New Customer Acquisition
            var CusAcquisition = xlWorkSheet.Cells.Range["AD1"];
            CusAcquisition.Value = "New Customer Acquisition";
            CusAcquisition.Font.Bold = true;
            CusAcquisition.Font.Size = 11;
            CusAcquisition.RowHeight = 15;
            CusAcquisition.Columns.ColumnWidth = 24;
            CusAcquisition.Borders.ColorIndex = 15;
            CusAcquisition.Cells.Interior.ColorIndex = 6;
            CusAcquisition.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            CusAcquisition.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Bank Name
            var bankname = xlWorkSheet.Cells.Range["AE1"];
            bankname.Value = "Bank Name";
            bankname.Font.Bold = true;
            bankname.Font.Size = 11;
            bankname.RowHeight = 15;
            bankname.Columns.ColumnWidth = 26;
            bankname.Borders.ColorIndex = 15;
            bankname.Cells.Interior.ColorIndex = 6;
            bankname.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            bankname.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Bank Branch Name
            var branchname = xlWorkSheet.Cells.Range["AF1"];
            branchname.Value = "Bank Branch Name";
            branchname.Font.Bold = true;
            branchname.Font.Size = 11;
            branchname.RowHeight = 15;
            branchname.Columns.ColumnWidth = 26;
            branchname.Borders.ColorIndex = 15;
            branchname.Cells.Interior.ColorIndex = 6;
            branchname.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            branchname.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //IFSC code
            var ifsccode = xlWorkSheet.Cells.Range["AG1"];
            ifsccode.Value = "IFSC Code";
            ifsccode.Font.Bold = true;
            ifsccode.Font.Size = 11;
            ifsccode.RowHeight = 15;
            ifsccode.Columns.ColumnWidth = 26;
            ifsccode.Borders.ColorIndex = 15;
            ifsccode.Cells.Interior.ColorIndex = 6;
            ifsccode.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            ifsccode.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Bank Account Name
            var BankAccName = xlWorkSheet.Cells.Range["AH1"];
            BankAccName.Value = "Bank Account Name";
            BankAccName.Font.Bold = true;
            BankAccName.Font.Size = 11;
            BankAccName.RowHeight = 15;
            BankAccName.Columns.ColumnWidth = 24;
            BankAccName.Borders.ColorIndex = 15;
            BankAccName.Cells.Interior.ColorIndex = 6;
            BankAccName.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            BankAccName.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Bank Account Level
            var BankAccLevel = xlWorkSheet.Cells.Range["AI1"];
            BankAccLevel.Value = "Bank Account Level";
            BankAccLevel.Font.Bold = true;
            BankAccLevel.Font.Size = 11;
            BankAccLevel.RowHeight = 15;
            BankAccLevel.Columns.ColumnWidth = 24;
            BankAccLevel.Borders.ColorIndex = 15;
            BankAccLevel.Cells.Interior.ColorIndex = 6;
            BankAccLevel.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            BankAccLevel.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Bank Account Number
            var BankAccNo = xlWorkSheet.Cells.Range["AJ1"];
            BankAccNo.Value = "Bank Account Number";
            BankAccNo.Font.Bold = true;
            BankAccNo.Font.Size = 11;
            BankAccNo.RowHeight = 15;
            BankAccNo.Columns.ColumnWidth = 24;
            BankAccNo.Borders.ColorIndex = 15;
            BankAccNo.Cells.Interior.ColorIndex = 6;
            BankAccNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            BankAccNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Sales Person Name
            var salespersonname = xlWorkSheet.Cells.Range["AK1"];
            salespersonname.Value = "SalesPerson Name";
            salespersonname.Font.Bold = true;
            salespersonname.Font.Size = 11;
            salespersonname.RowHeight = 15;
            salespersonname.Columns.ColumnWidth = 24;
            salespersonname.Borders.ColorIndex = 15;
            salespersonname.Cells.Interior.ColorIndex = 6;
            salespersonname.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            salespersonname.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Loan Product
            var loanproduct = xlWorkSheet.Cells.Range["AL1"];
            loanproduct.Value = "Loan Product";
            loanproduct.Font.Bold = true;
            loanproduct.Font.Size = 11;
            loanproduct.RowHeight = 15;
            loanproduct.Columns.ColumnWidth = 22;
            loanproduct.Borders.ColorIndex = 15;
            loanproduct.Cells.Interior.ColorIndex = 6;
            loanproduct.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            loanproduct.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Loan Amount
            var loanamt = xlWorkSheet.Cells.Range["AM1"];
            loanamt.Value = "Loan Amount";
            loanamt.Font.Bold = true;
            loanamt.Font.Size = 11;
            loanamt.RowHeight = 15;
            loanamt.Columns.ColumnWidth = 24;
            loanamt.Borders.ColorIndex = 15;
            loanamt.Cells.Interior.ColorIndex = 6;
            loanamt.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            loanamt.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Loan Tenure
            var loantenure = xlWorkSheet.Cells.Range["AN1"];
            loantenure.Value = "Loan Tenure";
            loantenure.Font.Bold = true;
            loantenure.Font.Size = 11;
            loantenure.RowHeight = 15;
            loantenure.Columns.ColumnWidth = 24;
            loantenure.Borders.ColorIndex = 15;
            loantenure.Cells.Interior.ColorIndex = 6;
            loantenure.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            loantenure.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Loan Term Period
            var loanperiod = xlWorkSheet.Cells.Range["AO1"];
            loanperiod.Value = "Loan Term Period";
            loanperiod.Font.Bold = true;
            loanperiod.Font.Size = 11;
            loanperiod.RowHeight = 15;
            loanperiod.Columns.ColumnWidth = 24;
            loanperiod.Borders.ColorIndex = 15;
            loanperiod.Cells.Interior.ColorIndex = 6;
            loanperiod.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            loanperiod.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Loan Type
            var loanType = xlWorkSheet.Cells.Range["AP1"];
            loanType.Value = "Loan Type";
            loanType.Font.Bold = true;
            loanType.Font.Size = 11;
            loanType.RowHeight = 15;
            loanType.Columns.ColumnWidth = 24;
            loanType.Borders.ColorIndex = 15;
            loanType.Cells.Interior.ColorIndex = 6;
            loanType.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            loanType.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Loan Start Date
            var loanstartdate = xlWorkSheet.Cells.Range["AQ1"];
            loanstartdate.Value = "Loan Start Date";
            loanstartdate.Font.Bold = true;
            loanstartdate.Font.Size = 11;
            loanstartdate.RowHeight = 15;
            loanstartdate.Columns.ColumnWidth = 24;
            loanstartdate.Borders.ColorIndex = 15;
            loanstartdate.Cells.Interior.ColorIndex = 6;
            loanstartdate.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            loanstartdate.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Amortization Method
            var amortization = xlWorkSheet.Cells.Range["AR1"];
            amortization.Value = "Amortization Method";
            amortization.Font.Bold = true;
            amortization.Font.Size = 11;
            amortization.RowHeight = 15;
            amortization.Columns.ColumnWidth = 24;
            amortization.Borders.ColorIndex = 15;
            amortization.Cells.Interior.ColorIndex = 6;
            amortization.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            amortization.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Payment | Principal Start Date
            var paymentstartdate = xlWorkSheet.Cells.Range["AS1"];
            paymentstartdate.Value = "Payment | Principal State Date";
            paymentstartdate.Font.Bold = true;
            paymentstartdate.Font.Size = 11;
            paymentstartdate.RowHeight = 15;
            paymentstartdate.Columns.ColumnWidth = 34;
            paymentstartdate.Borders.ColorIndex = 15;
            paymentstartdate.Cells.Interior.ColorIndex = 6;
            paymentstartdate.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            paymentstartdate.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Payment | Principal Frequency
            var paymentfrequency = xlWorkSheet.Cells.Range["AT1"];
            paymentfrequency.Value = "Payment | Principal Frequency";
            paymentfrequency.Font.Bold = true;
            paymentfrequency.Font.Size = 11;
            paymentfrequency.RowHeight = 15;
            paymentfrequency.Columns.ColumnWidth = 36;
            paymentfrequency.Borders.ColorIndex = 15;
            paymentfrequency.Cells.Interior.ColorIndex = 6;
            paymentfrequency.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            paymentfrequency.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Intrest Start Date
            var intreststartdate = xlWorkSheet.Cells.Range["AU1"];
            intreststartdate.Value = "Intrest Start Date";
            intreststartdate.Font.Bold = true;
            intreststartdate.Font.Size = 11;
            intreststartdate.RowHeight = 15;
            intreststartdate.Columns.ColumnWidth = 24;
            intreststartdate.Borders.ColorIndex = 15;
            intreststartdate.Cells.Interior.ColorIndex = 6;
            intreststartdate.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            intreststartdate.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Intrest Frequency
            var intrestfrequency = xlWorkSheet.Cells.Range["AV1"];
            intrestfrequency.Value = "Intrest Frequency";
            intrestfrequency.Font.Bold = true;
            intrestfrequency.Font.Size = 11;
            intrestfrequency.RowHeight = 15;
            intrestfrequency.Columns.ColumnWidth = 24;
            intrestfrequency.Borders.ColorIndex = 15;
            intrestfrequency.Cells.Interior.ColorIndex = 6;
            intrestfrequency.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            intrestfrequency.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Loan Intrest Rate
            var loanintrestrate = xlWorkSheet.Cells.Range["AW1"];
            loanintrestrate.Value = "Loan Intrest Rate";
            loanintrestrate.Font.Bold = true;
            loanintrestrate.Font.Size = 11;
            loanintrestrate.RowHeight = 15;
            loanintrestrate.Columns.ColumnWidth = 24;
            loanintrestrate.Borders.ColorIndex = 15;
            loanintrestrate.Cells.Interior.ColorIndex = 6;
            loanintrestrate.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            loanintrestrate.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Processing Fees Percentage
            var processingfee = xlWorkSheet.Cells.Range["AX1"];
            processingfee.Value = "Processing Fees Percentage";
            processingfee.Font.Bold = true;
            processingfee.Font.Size = 11;
            processingfee.RowHeight = 15;
            processingfee.Columns.ColumnWidth = 26;
            processingfee.Borders.ColorIndex = 15;
            processingfee.Cells.Interior.ColorIndex = 6;
            processingfee.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            processingfee.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Insurance Fees 1
            var insurancefee1 = xlWorkSheet.Cells.Range["AY1"];
            insurancefee1.Value = "Insurance Fees 1";
            insurancefee1.Font.Bold = true;
            insurancefee1.Font.Size = 11;
            insurancefee1.RowHeight = 15;
            insurancefee1.Columns.ColumnWidth = 24;
            insurancefee1.Borders.ColorIndex = 15;
            insurancefee1.Cells.Interior.ColorIndex = 6;
            insurancefee1.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            insurancefee1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Insurance Fees 1  Amount
            var insurancefee1amt = xlWorkSheet.Cells.Range["AZ1"];
            insurancefee1amt.Value = "Insurance Fees 1 Amount";
            insurancefee1amt.Font.Bold = true;
            insurancefee1amt.Font.Size = 11;
            insurancefee1amt.RowHeight = 15;
            insurancefee1amt.Columns.ColumnWidth = 24;
            insurancefee1amt.Borders.ColorIndex = 15;
            insurancefee1amt.Cells.Interior.ColorIndex = 6;
            insurancefee1amt.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            insurancefee1amt.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Insurance Fees 2
            var insurancefee2 = xlWorkSheet.Cells.Range["BA1"];
            insurancefee2.Value = "Insurance Fees 2";
            insurancefee2.Font.Bold = true;
            insurancefee2.Font.Size = 11;
            insurancefee2.RowHeight = 15;
            insurancefee2.Columns.ColumnWidth = 24;
            insurancefee2.Borders.ColorIndex = 15;
            insurancefee2.Cells.Interior.ColorIndex = 6;
            insurancefee2.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            insurancefee2.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Insurance Fees 2 amount
            var insurancefee2amt = xlWorkSheet.Cells.Range["BB1"];
            insurancefee2amt.Value = "Insurance Fees 2 Amount";
            insurancefee2amt.Font.Bold = true;
            insurancefee2amt.Font.Size = 11;
            insurancefee2amt.RowHeight = 15;
            insurancefee2amt.Columns.ColumnWidth = 24;
            insurancefee2amt.Borders.ColorIndex = 15;
            insurancefee2amt.Cells.Interior.ColorIndex = 6;
            insurancefee2amt.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            insurancefee2amt.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Penal Intrest Rate
            var penalintrestrate = xlWorkSheet.Cells.Range["BC1"];
            penalintrestrate.Value = "Penal Intrest Rate";
            penalintrestrate.Font.Bold = true;
            penalintrestrate.Font.Size = 11;
            penalintrestrate.RowHeight = 15;
            penalintrestrate.Columns.ColumnWidth = 24;
            penalintrestrate.Borders.ColorIndex = 15;
            penalintrestrate.Cells.Interior.ColorIndex = 6;
            penalintrestrate.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            penalintrestrate.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            #endregion

            FillGTXL(xlWorkSheet, GetDetails(CustomerList));
            string dir = "";
            try
            {
                dir = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Report\\LOAN DIS FORM");
                if (Directory.Exists(dir))
                {
                    string FileName = dir + "\\LOAN DIS FORM" + DateTime.Now.ToString("dd-MMM-yyyy (hh-mm)") + ".xlsx";
                    xlWorkBook.SaveAs(FileName);
                }
                else
                {
                    Directory.CreateDirectory(dir);
                    string FileName = dir + "\\LOAN DIS FORM" + DateTime.Now.ToString("dd-MMM-yyyy (hh-mm)") + ".xlsx";
                    xlWorkBook.SaveAs(FileName);

                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                xlWorkBook.Close();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlWorkBook);
                xlApp.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);
                xlApp = null;
                xlWorkBook = null;
                File.Delete(dir+"\\temp.xlsx");
            }

        }



        public void FillGTXL(Worksheet xlWorkSheet, List<SamuunatiReport> loans)
        {
            int RowStart = 2;
            foreach (SamuunatiReport x in loans)
            {
                xlWorkSheet.Cells[RowStart, 1] = x.OperatingUnit;
                xlWorkSheet.Cells[RowStart, 2] = x.CustomerNumber;
                xlWorkSheet.Cells[RowStart, 3] = x.ReportID;
                xlWorkSheet.Cells[RowStart, 4] = x.HMRefernceNo;
                xlWorkSheet.Cells[RowStart, 5] = x.CustomerName;
                xlWorkSheet.Cells[RowStart, 6] = x.SamunnatiBranchMapping;
                xlWorkSheet.Cells[RowStart, 7] = x.CustomerType;
                xlWorkSheet.Cells[RowStart, 8] = x.CustomerSubtype;
                xlWorkSheet.Cells[RowStart, 9] = x.Gender;
                xlWorkSheet.Cells[RowStart, 10] = x.DOB;
                xlWorkSheet.Cells[RowStart, 11] = x.BusinessCategory;
                xlWorkSheet.Cells[RowStart, 12] = x.BusinessIndustryType;
                xlWorkSheet.Cells[RowStart, 13] = x.Sector;
                xlWorkSheet.Cells[RowStart, 14] = x.PrimaryValueChain;
                xlWorkSheet.Cells[RowStart, 15] = x.SecondaryValueChain;
                xlWorkSheet.Cells[RowStart, 16] = x.Phone;
                xlWorkSheet.Cells[RowStart, 17] = x.Address1;
                xlWorkSheet.Cells[RowStart, 18] = x.Address2;
                xlWorkSheet.Cells[RowStart, 19] = x.Address3;
                xlWorkSheet.Cells[RowStart, 20] = x.Address4;
                xlWorkSheet.Cells[RowStart, 21] = x.City;
                xlWorkSheet.Cells[RowStart, 22] = x.PostalCode;
                xlWorkSheet.Cells[RowStart, 23] = x.State;
                xlWorkSheet.Cells[RowStart, 24] = x.EmailID;
                xlWorkSheet.Cells[RowStart, 25] = x.Constitution;
                xlWorkSheet.Cells[RowStart, 26] = x.GSTINno;
                xlWorkSheet.Cells[RowStart, 27] = x.PANTANno;
                xlWorkSheet.Cells[RowStart, 28] = x.AadharNo;
                xlWorkSheet.Cells[RowStart, 29] = x.ExistingRelationshipValue;
                xlWorkSheet.Cells[RowStart, 30] = x.NewCustomerAcquisition;
                xlWorkSheet.Cells[RowStart, 31] = x.BankName;
                xlWorkSheet.Cells[RowStart, 32] = x.BankBranchName;
                xlWorkSheet.Cells[RowStart, 33] = x.IFSCcode;
                xlWorkSheet.Cells[RowStart, 34] = x.BankAccName;
                xlWorkSheet.Cells[RowStart, 35] = x.BankAccLevel;
                xlWorkSheet.Cells[RowStart, 36] = x.BankAccNo;
                xlWorkSheet.Cells[RowStart, 37] = x.SalesPesonName;
                xlWorkSheet.Cells[RowStart, 38] = x.LoanProduct;
                xlWorkSheet.Cells[RowStart, 39] = x.LoanAmount;
                xlWorkSheet.Cells[RowStart, 40] = x.LoanTenure;
                xlWorkSheet.Cells[RowStart, 41] = x.LoanTermPeriod;
                xlWorkSheet.Cells[RowStart, 42] = x.LoanType;
                xlWorkSheet.Cells[RowStart, 43] = x.LoanStartDate;
                xlWorkSheet.Cells[RowStart, 44] = x.AmortizationMethod;
                xlWorkSheet.Cells[RowStart, 45] = x.PaymentStartDate;
                xlWorkSheet.Cells[RowStart, 46] = x.PaymentFrequency;
                xlWorkSheet.Cells[RowStart, 47] = x.IntrestStartDate;
                xlWorkSheet.Cells[RowStart, 48] = x.IntrestFrequency;
                xlWorkSheet.Cells[RowStart, 49] = x.LoanIntrestRate;
                xlWorkSheet.Cells[RowStart, 50] = x.ProcessingFeesPercentage;
                xlWorkSheet.Cells[RowStart, 51] = x.InsuranceFees1;
                xlWorkSheet.Cells[RowStart, 52] = x.InsuranceFees1Amt;
                xlWorkSheet.Cells[RowStart, 53] = x.InsuranceFees2;
                xlWorkSheet.Cells[RowStart, 54] = x.InsuranceFees2Amt;
                xlWorkSheet.Cells[RowStart, 55] = x.PenalIntrestRate;
                RowStart++;
            }
        }
    }
}
