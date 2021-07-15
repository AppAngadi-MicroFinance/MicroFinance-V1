using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace MicroFinance.Reports
{
    public class Memberexcel
    {
        public static void MemberExcel()
        {
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Columns.AutoFit();
            xlWorkSheet.Columns.HorizontalAlignment = XlHAlign.xlHAlignCenter;
            xlWorkSheet.Columns.VerticalAlignment = XlHAlign.xlHAlignCenter;
            xlWorkSheet.Columns.ColumnWidth = 20;
            //S.No.
            var no = xlWorkSheet.Cells.Range["A1"];
            no.Value = "S.NO.";
            no.Font.Bold = true;
            no.Font.Size = 11;
            no.RowHeight = 46;
            no.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            no.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //FO Name
            var FieldOfficier = xlWorkSheet.Cells.Range["B1"];
            FieldOfficier.Value = "Field Officier Name";
            FieldOfficier.Font.Bold = true;
            FieldOfficier.Font.Size = 11;
            FieldOfficier.RowHeight = 46;
            FieldOfficier.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            FieldOfficier.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Center name
            var Centername = xlWorkSheet.Cells.Range["C1"];
            Centername.Value = "Center Name";
            Centername.Font.Bold = true;
            Centername.Font.Size = 11;
            Centername.RowHeight = 46;
            Centername.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Centername.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Member ID
            var MemberID = xlWorkSheet.Cells.Range["D1"];
            MemberID.Value = "Member ID";
            MemberID.Font.Bold = true;
            MemberID.Font.Size = 11;
            MemberID.RowHeight = 46;
            MemberID.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            MemberID.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Member NAme
            var Membername = xlWorkSheet.Cells.Range["E1"];
            Membername.Value = "Member Name";
            Membername.Font.Bold = true;
            Membername.Font.Size = 11;
            Membername.RowHeight = 46;
            Membername.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Membername.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Regional Name
            var RegionalName = xlWorkSheet.Cells.Range["F1"];
            RegionalName.Value = "Regional Name";
            RegionalName.Font.Bold = true;
            RegionalName.Font.Size = 11;
            RegionalName.RowHeight = 46;
            RegionalName.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            RegionalName.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Enrollment Date
            var enrolldate = xlWorkSheet.Cells.Range["G1"];
            enrolldate.Value = "Enrollment Date";
            enrolldate.Font.Bold = true;
            enrolldate.Font.Size = 11;
            enrolldate.RowHeight = 46;
            enrolldate.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            enrolldate.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Drop Out Date
            var Dropout = xlWorkSheet.Cells.Range["H1"];
            Dropout.Value = "Drop Out Date";
            Dropout.Font.Bold = true;
            Dropout.Font.Size = 11;
            Dropout.RowHeight = 46;
            Dropout.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Dropout.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Age
            var Age = xlWorkSheet.Cells.Range["I1"];
            Age.Value = "AGE";
            Age.Font.Bold = true;
            Age.Font.Size = 11;
            Age.RowHeight = 46;
            Age.ColumnWidth = 10;
            Age.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Age.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //DOB
            var DOB = xlWorkSheet.Cells.Range["J1"];
            DOB.Value = "Date Of Birth";
            DOB.Font.Bold = true;
            DOB.Font.Size = 11;
            DOB.RowHeight = 46;
            DOB.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            DOB.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Address
            var Addredss = xlWorkSheet.Cells.Range["K1"];
            Addredss.Value = "Address";
            Addredss.Font.Bold = true;
            Addredss.Font.Size = 11;
            Addredss.RowHeight = 46;
            Addredss.ColumnWidth = 40;
            Addredss.WrapText = true;
            Addredss.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Addredss.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //District
            var District = xlWorkSheet.Cells.Range["L1"];
            District.Value = "District";
            District.Font.Bold = true;
            District.Font.Size = 11;
            District.RowHeight = 46;
            District.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            District.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //pincode
            var Pincode = xlWorkSheet.Cells.Range["M1"];
            Pincode.Value = "Pincode";
            Pincode.Font.Bold = true;
            Pincode.Font.Size = 11;
            Pincode.RowHeight = 46;
            Pincode.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Pincode.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Martial Status
            var Martialstatus = xlWorkSheet.Cells.Range["N1"];
            Martialstatus.Value = "Martial Status";
            Martialstatus.Font.Bold = true;
            Martialstatus.Font.Size = 11;
            Martialstatus.RowHeight = 46;
            Martialstatus.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Martialstatus.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Housing Type 
            var Housingtype = xlWorkSheet.Cells.Range["O1"];
            Housingtype.Value = "Housing Type";
            Housingtype.Font.Bold = true;
            Housingtype.Font.Size = 11;
            Housingtype.RowHeight = 46;
            Housingtype.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Housingtype.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //religion
            var Religion = xlWorkSheet.Cells.Range["P1"];
            Religion.Value = "Religion";
            Religion.Font.Bold = true;
            Religion.Font.Size = 11;
            Religion.RowHeight = 46;
            Religion.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Religion.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Community
            var Community = xlWorkSheet.Cells.Range["Q1"];
            Community.Value = "Community";
            Community.Font.Bold = true;
            Community.Font.Size = 11;
            Community.RowHeight = 46;
            Community.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Community.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Family occupation
            var familyoccupation = xlWorkSheet.Cells.Range["R1"];
            familyoccupation.Value = "Family Occupation";
            familyoccupation.Font.Bold = true;
            familyoccupation.Font.Size = 11;
            familyoccupation.RowHeight = 46;
            familyoccupation.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            familyoccupation.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //contact Number
            var Contact = xlWorkSheet.Cells.Range["S1"];
            Contact.Value = "Contact Number";
            Contact.Font.Bold = true;
            Contact.Font.Size = 11;
            Contact.RowHeight = 46;
            Contact.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Contact.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Education
            var Education = xlWorkSheet.Cells.Range["T1"];
            Education.Value = "Education ";
            Education.Font.Bold = true;
            Education.Font.Size = 11;
            Education.RowHeight = 46;
            Education.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Education.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            // Address Proof Type
            var Addressprooftype = xlWorkSheet.Cells.Range["U1"];
            Addressprooftype.Value = "Address Proof Type";
            Addressprooftype.Font.Bold = true;
            Addressprooftype.Font.Size = 11;
            Addressprooftype.RowHeight = 46;
            Addressprooftype.WrapText = true;
            Addressprooftype.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Addressprooftype.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            // Address proof Number
            var AppAddressproofNo = xlWorkSheet.Cells.Range["V1"];
            AppAddressproofNo.Value = "Address Proof Number";
            AppAddressproofNo.Font.Bold = true;
            AppAddressproofNo.Font.Size = 11;
            AppAddressproofNo.RowHeight = 46;
            AppAddressproofNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            AppAddressproofNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Address Proof Date
            var Addressproofdate = xlWorkSheet.Cells.Range["W1"];
            Addressproofdate.Value = "Address Prooof Date";
            Addressproofdate.Font.Bold = true;
            Addressproofdate.Font.Size = 11;
            Addressproofdate.RowHeight = 46;
            Addressproofdate.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Addressproofdate.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Photo Proof type 
            var Photoprooftype = xlWorkSheet.Cells.Range["X1"];
            Photoprooftype.Value = "Photo Proof";
            Photoprooftype.Font.Bold = true;
            Photoprooftype.Font.Size = 11;
            Photoprooftype.RowHeight = 46;
            Photoprooftype.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Photoprooftype.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Photo Proof Number
            var PhotoproofNo = xlWorkSheet.Cells.Range["Y1"];
            PhotoproofNo.Value = "Photo Proof Number";
            PhotoproofNo.Font.Bold = true;
            PhotoproofNo.Font.Size = 11;
            PhotoproofNo.RowHeight = 46;
            PhotoproofNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            PhotoproofNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Photo Proof Date
            var Photoproofdate = xlWorkSheet.Cells.Range["Z1"];
            Photoproofdate.Value = "Photo Proof Date";
            Photoproofdate.Font.Bold = true;
            Photoproofdate.Font.Size = 11;
            Photoproofdate.RowHeight = 46;
            Photoproofdate.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Photoproofdate.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Guarantor Name
            var Guarantorname = xlWorkSheet.Cells.Range["AA1"];
            Guarantorname.Value = "Guarantor Name";
            Guarantorname.Font.Bold = true;
            Guarantorname.Font.Size = 11;
            Guarantorname.RowHeight = 46;
            Guarantorname.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Guarantorname.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Guarantor Relationshipp
            var Guarantorrelationship = xlWorkSheet.Cells.Range["AB1"];
            Guarantorrelationship.Value = "Guarantor Relationship";
            Guarantorrelationship.Font.Bold = true;
            Guarantorrelationship.Font.Size = 11;
            Guarantorrelationship.RowHeight = 46;
            Guarantorrelationship.Columns.AutoFit();
            Guarantorrelationship.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            Guarantorrelationship.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Guarantor Address
            var GuarantorAddress = xlWorkSheet.Cells.Range["AC1"];
            GuarantorAddress.Value = "Guarantor Address";
            GuarantorAddress.Font.Bold = true;
            GuarantorAddress.Font.Size = 11;
            GuarantorAddress.RowHeight = 46;
            GuarantorAddress.ColumnWidth = 24;
            GuarantorAddress.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            GuarantorAddress.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Housing Index 
            var HousingIndex = xlWorkSheet.Cells.Range["AC1"];
            HousingIndex.Value = "Housing Index";
            HousingIndex.Font.Bold = true;
            HousingIndex.Font.Size = 11;
            HousingIndex.RowHeight = 46;
            HousingIndex.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            HousingIndex.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Totsl Family number 
            var FamilyNo = xlWorkSheet.Cells.Range["AD1"];
            FamilyNo.Value = "Total Family Members";
            FamilyNo.Font.Bold = true;
            FamilyNo.Font.Size = 11;
            FamilyNo.RowHeight = 46;
            FamilyNo.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            FamilyNo.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            // number of earing members 
            var earingmembers = xlWorkSheet.Cells.Range["AD1"];
            earingmembers.Value = "No. of Earing Members";
            earingmembers.Font.Bold = true;
            earingmembers.Font.Size = 11;
            earingmembers.RowHeight = 46;
            earingmembers.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            earingmembers.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            //Family Monthly income
            var MonthlyIncome = xlWorkSheet.Cells.Range["AD1"];
            MonthlyIncome.Value = "Family Monthly Income";
            MonthlyIncome.Font.Bold = true;
            MonthlyIncome.Font.Size = 11;
            MonthlyIncome.RowHeight = 46;
            MonthlyIncome.VerticalAlignment = Excel.XlHAlign.xlHAlignCenter;
            MonthlyIncome.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

            try
            {
                xlWorkBook.SaveAs(@"D:\Costumer.xlsx");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadLine();
            }
            finally
            {
                xlWorkBook.Close();
                xlApp.Quit();
                xlApp = null;
                xlWorkBook = null;
                File.Delete(@"D:\temp.xlsx");
            }
        }
    }
}
