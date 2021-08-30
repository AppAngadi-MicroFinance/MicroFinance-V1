using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroFinance.Modal;
using Excel = Microsoft.Office.Interop.Excel;

namespace MicroFinance.Reports
{
     public class SUMAtoHO
    {
        public string ConnectionString = MicroFinance.Properties.Settings.Default.DBConnection;
        public DateTime ApproveDate { get; set; }
        public string LoanAcNo { get; set; }
        public string AadharNumber { get; set; }
        public string Name { get; set; }
        public int Disbursement { get; set; }
        public string FileName { get; set; }
        public List<SUMAtoHO> ImportSamuFile(string Path)
        {
            List<SUMAtoHO> SamuList = new List<SUMAtoHO>();
            var FilePath = Path.Split('\\');
            string FileName = FilePath[FilePath.Length - 1];
            Excel.Application application;
            Excel.Workbook workbook;
            // Excel.Worksheet worksheet;
            StringBuilder sb = new StringBuilder();
            int SC;
            // int RC = 0;
            // int CC = 0;
            string Sheetname = null;

            application = new Excel.Application();
            workbook = application.Workbooks.Open(Path);
            SC = workbook.Worksheets.Count;
            try
            {
                foreach (Excel.Worksheet sheet in workbook.Worksheets)
                {
                    if (sheet.Name.Equals("Loan", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Excel.Range usedrange = sheet.UsedRange;
                        Sheetname = sheet.Name;
                        SamuList = GetDataFromExcel(sheet, FileName);
                        break;
                    }
                }
            }
            catch(Exception ex)
            {
                var s = ex.Message;
            }
            finally
            {
                workbook.Close();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                application.Quit();
                System.Runtime.InteropServices.Marshal.ReleaseComObject(application);
            }
            return SamuList;
        }
        public List<SUMAtoHO> GetDataFromExcel(Excel.Worksheet worksheet, string Filename)
        {
            List<SUMAtoHO> SamuList = new List<SUMAtoHO>();
            Excel.Range userange = worksheet.UsedRange;
            int rowcount = userange.Rows.Count;
            int ColumnCount = userange.Columns.Count;
            int AadharColumn = 0;
            int LoanAcNoColumn = 0;
            int CustomerNameColumn = 0;
            int DisbursementColumn = 0;
            int DateColumn = 0;
            for (int i = 1; i <= ColumnCount; i++)
            {
                int row = 1;
                string value = (worksheet.Cells[row, i] as Excel.Range).Value;
                if(!string.IsNullOrEmpty(value))
                {
                    value = value.Trim();
                    if (value.Equals("Aadhar"))
                    {
                        AadharColumn = i;
                    }
                    else if (value.Equals("Date"))
                    {
                        DateColumn = i;
                    }
                    else if (value.Equals("Loan Acc No"))
                    {
                        LoanAcNoColumn = i;
                    }
                    else if (value.Equals("Customer Name"))
                    {
                        CustomerNameColumn = i;
                    }
                    else if (value.Equals("Disbursement"))
                    {
                        DisbursementColumn = i;
                    }
                }
                
            }
            for (int Rownumber = 2; Rownumber <= rowcount; Rownumber++)
            {
                var IsNull = (worksheet.Cells[Rownumber, DateColumn] as Excel.Range);
                if (IsNull.Text == "")
                {
                    break;
                }
                else
                {
                    DateTime _reportDate = (DateTime)(worksheet.Cells[Rownumber, DateColumn] as Excel.Range).Value;
                    var aadhar= (worksheet.Cells[Rownumber, AadharColumn] as Excel.Range).Value;
                    string _aadharNumber = aadhar.ToString();
                    //string _aadharNumber = (worksheet.Cells[Rownumber, AadharColumn] as Excel.Range).Value;
                    string _loanacno = (worksheet.Cells[Rownumber, LoanAcNoColumn] as Excel.Range).Value;
                    string _customername= (worksheet.Cells[Rownumber, CustomerNameColumn] as Excel.Range).Value;
                    int _disbursement=(int) (worksheet.Cells[Rownumber, DisbursementColumn] as Excel.Range).Value;
                    string _fileName = Filename;

                    SamuList.Add(new SUMAtoHO
                    {
                        ApproveDate = _reportDate,
                        AadharNumber = _aadharNumber,
                        LoanAcNo = _loanacno,
                        Name = _customername,
                        Disbursement = _disbursement,
                        FileName = _fileName
                    }) ;

                }
            }
            return SamuList;
        }

        public bool IsFileExists(string FileName)
        {
            bool Result = false;
            using (SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select FileName from DisbursementFromSAMU where FileName = '"+FileName+"'";
                    string Res = (string)sqlcomm.ExecuteScalar();
                    if(FileName.Equals(Res))
                    {
                        Result = true;
                    }
                }
                sqlconn.Close();
            }
            return Result;
        }

        public void InsertData(SUMAtoHO Value,string LoanID)
        {
            using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "insert into DisbursementFromSAMU(ApprovedDate,LoanAcNo,LoanID,FileName)values('"+Value.ApproveDate.ToString("MM-dd-yyyy")+"','"+Value.LoanAcNo+"','"+LoanID+"','"+Value.FileName+"')";
                    sqlcomm.ExecuteNonQuery();
                }
            }
        }


    }
}
