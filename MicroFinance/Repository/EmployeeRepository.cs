using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using MicroFinance.Modal;

namespace MicroFinance.ViewModel
{
    public class EmployeeRepository
    {
        static string ConnectionString = MicroFinance.Properties.Settings.Default.DBConnection;
        public static bool AddEmployee(Employee employee)
        {
            bool Result = false;


            using (SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                string address = employee.HouseNo + "\t" + employee.TownName;
                int EmpCount = 1;
                string BranchID = string.Empty;
                string EmployeeID = string.Empty;
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand Sqlcomm = new SqlCommand();
                    Sqlcomm.Connection = sqlconn;

                    Sqlcomm.CommandText = "Select Bid From BranchDetails where BranchName='" + employee.BranchName + "'";
                    BranchID = (string)Sqlcomm.ExecuteScalar();
                    Sqlcomm.CommandText = "Select Count(EmpId) from Employee";
                    EmpCount += (int)Sqlcomm.ExecuteScalar();
                    EmployeeID = GenerateEmployeeID(BranchID, EmpCount);
                     Sqlcomm.CommandText = "insert into Employee(EmpId,Name,DOB,age,MobileNo,Religion,EmailId,Education,AadhaarNo,DateOfJoin,BankName,BranchName,AccountNumber,IFSCCode,MICRCode,Address,PinCode,District,IsAddressProof,AddressProofName,IsPhotoProof,PhotoProofName,IsProfilePhoto,IsActive,FatherName,PanNumber,Community,Caste,Password)values(@empId,@Name,@dob,@age,@mobile,@religion,@email,@education,@aadhar,@doj,@bankName,@branchName,@accountNo,@IFSC,@MICR,@address,@pincode,@district,@isAddressproof,@addressProofName,@isPhotoProof,@photoProofName,@isProfilePhoto,@isActive,@fatherName,@panNo,@community,@cast,@Password)";
                    Sqlcomm.Parameters.AddWithValue("@empId", EmployeeID);
                    Sqlcomm.Parameters.AddWithValue("@Name", employee.EmployeeName);
                    Sqlcomm.Parameters.AddWithValue("@dob", employee.DOB.ToString("MM/dd/yyyy"));
                    Sqlcomm.Parameters.AddWithValue("@age", employee.Age);
                    Sqlcomm.Parameters.AddWithValue("@mobile",employee.ContactNumber);
                    Sqlcomm.Parameters.AddWithValue("@religion", employee.Religion);
                    Sqlcomm.Parameters.AddWithValue("@email", employee.Email);
                    Sqlcomm.Parameters.AddWithValue("@education", employee.Education);
                    Sqlcomm.Parameters.AddWithValue("@aadhar", employee.AadharNumber);
                    Sqlcomm.Parameters.AddWithValue("@doj", employee.DateOfJoining.ToString("MM/dd/yyyy"));
                    Sqlcomm.Parameters.AddWithValue("@bankName", employee.BankName);
                    Sqlcomm.Parameters.AddWithValue("@branchName", employee.BankBranchName);
                    Sqlcomm.Parameters.AddWithValue("@accountNo", employee.AccountNumber);

                    Sqlcomm.Parameters.AddWithValue("@IFSC", employee.IFSCCode);
                    Sqlcomm.Parameters.AddWithValue("@MICR", employee.MICRCode);
                    Sqlcomm.Parameters.AddWithValue("@address", address);
                    Sqlcomm.Parameters.AddWithValue("@pincode", employee.Pincode);
                    Sqlcomm.Parameters.AddWithValue("@district", employee.District);

                    Sqlcomm.Parameters.AddWithValue("@isAddressproof", IsTrue(employee.IsAddressProof));
                    Sqlcomm.Parameters.AddWithValue("@addressProofName", employee.AddressProofName);

                    Sqlcomm.Parameters.AddWithValue("@isPhotoProof", IsTrue(employee.IsPhotoProof));
                    Sqlcomm.Parameters.AddWithValue("@photoProofName", employee.PhotoProofName);

                    Sqlcomm.Parameters.AddWithValue("@isProfilePhoto", IsTrue(employee.IsProfilePicture));
                    Sqlcomm.Parameters.AddWithValue("@isActive", IsTrue(employee.IsActive));
                    Sqlcomm.Parameters.AddWithValue("@fatherName", employee.FatherName);
                    Sqlcomm.Parameters.AddWithValue("@panNo", employee.PanNumber);

                    Sqlcomm.Parameters.AddWithValue("@community", employee.Community);
                    Sqlcomm.Parameters.AddWithValue("@cast", employee.Caste);
                    Sqlcomm.Parameters.AddWithValue("@Password", employee.ContactNumber);

                    // Sqlcomm.Parameters.AddWithValue("@addressProof", Convertion(_addressproofimage));
                    // Sqlcomm.Parameters.AddWithValue("@photoproof", Convertion(_photoproofimage));
                    // Sqlcomm.Parameters.AddWithValue("@profilePhoto", Convertion(_profileimage));
                    int res = (int)Sqlcomm.ExecuteNonQuery();
                    if(res==1)
                    {
                        Sqlcomm.CommandText = "insert into EmployeeBranch (Empid, BranchId, Designation, DateOfAppoint, IsActive)values('" + EmployeeID + "','" + BranchID + "','" + employee.Designation + "','" + employee.DateOfJoining.ToString("MM/dd/yyyy") + "'," + IsTrue(true) + ")";
                        Sqlcomm.ExecuteNonQuery();
                        Result = true;
                    }
                    
                    if (employee.IsAddressProof==true)
                    {
                        string Folderpath = MainWindow.DriveBasePath + "\\" + employee.Region + "\\" + employee.BranchName + "\\" + "Employee\\Address Proof";
                        string FileName = EmployeeID;
                        byte[] data = Convertion(employee.AddressProofImage);
                        SaveImageToDrive.SaveImage(Folderpath, FileName, data);
                    }
                    if(employee.IsPhotoProof==true)
                    {
                        string Folderpath = MainWindow.DriveBasePath + "\\" + employee.Region + "\\" + employee.BranchName + "\\" + "Employee\\Photo Proof";
                        string FileName = EmployeeID;
                        byte[] data = Convertion(employee.PhotoProofImage);
                        SaveImageToDrive.SaveImage(Folderpath, FileName, data);
                    }
                    if(employee.IsProfilePicture==true)
                    {
                        string Folderpath = MainWindow.DriveBasePath + "\\" + employee.Region + "\\" + employee.BranchName + "\\" + "Employee\\Profile Picture";
                        string FileName = EmployeeID;
                        byte[] data = Convertion(employee.ProfileImage);
                        SaveImageToDrive.SaveImage(Folderpath, FileName, data);
                    }
                    
                }
            }
            return Result;
        }

        private static byte[] Convertion(BitmapImage image)
        {
            byte[] Data;
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                Data = ms.ToArray();
            }
            return Data;
        }

        public static int IsTrue(bool value)
        {
            if (value == true)
            {
                return 1;
            }
            return 0;
        }

        public static bool IsByte(int value)
        {
            if (value == 0)
            {
                return false;
            }
            return true;
        }


        private static string GenerateEmployeeID(string BranchId,int EmpCount) // IDPattern 0100220210605 (01-Region+002-BranchName/2021-CurrentYear/06-CurrentMonth/05-(CountOfCustomers+1))
        {
            int count = EmpCount;
            string Result = "";
            int year = DateTime.Now.Year;
            int mon = DateTime.Now.Month;
            string month = ((mon) < 10 ? "0" + mon : mon.ToString());
            string region = BranchId.Substring(0, 2);
            string branch = BranchId.Substring(8);
            Result = "E" + region + branch + year + month + ((count < 10) ? "0" + count : count.ToString());
            return Result;
        }

        public string DigitConvert(string digit, int place = 3)
        {
            StringBuilder sb = new StringBuilder();
            string number = digit;
            string Result = "";
            if (number.Length < place)
            {
                for (int i = 0; i < (place - (number.Length)); i++)
                {
                    sb.Append(0);
                }
                Result = sb.ToString() + number;
            }
            else
            {
                Result = number;
            }

            return Result;
        }


       public static List<RegionViewModel> GetRegions()
       {
            List<RegionViewModel> RegionList = new List<RegionViewModel>();

            using (SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select RegionID,RegionName from Region";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            RegionViewModel Region = new RegionViewModel();

                            Region.RegionId = reader.GetString(0);
                            Region.RegionName = reader.GetString(1);

                            RegionList.Add(Region);

                        }
                    }
                    reader.Close();

                }
                sqlconn.Close();
            }
            return RegionList;
        }

        public static ObservableCollection<BranchViewModel> GetBranches()
        {
            ObservableCollection<BranchViewModel> BranchList = new ObservableCollection<BranchViewModel>();

            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select RegionId,Bid,BranchName from BranchDetails";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            BranchViewModel branch = new BranchViewModel();
                            branch.RegionId = reader.GetString(0);
                            branch.BranchId = reader.GetString(1);
                            branch.BranchName = reader.GetString(2);
                            BranchList.Add(branch);
                        }
                    }
                    reader.Close();

                }
                return BranchList;
            }
        }
        public static ObservableCollection<EmployeeViewModel> GetEmployees()
        {
            ObservableCollection<EmployeeViewModel> EmployeeList = new ObservableCollection<EmployeeViewModel>();
            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select EmployeeBranch.EmpId,EmployeeBranch.BranchId,EmployeeBranch.Designation,Employee.Name from EmployeeBranch,Employee where Employee.EmpId=EmployeeBranch.EmpId and EmployeeBranch.IsActive='1'";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            EmployeeViewModel employee = new EmployeeViewModel();
                            employee.EmployeeId = reader.GetString(0);
                            employee.BranchId = reader.GetString(1);
                            employee.Designation = reader.GetString(2);
                            employee.EmployeeName = reader.GetString(3);
                            EmployeeList.Add(employee);
                        }
                    }
                    reader.Close();
                }

            }
            return EmployeeList;

        }


        public static List<TimeTableViewModel> GetTimeTable(string EmpId)
        {
            List<TimeTableViewModel> TimeTableList = new List<TimeTableViewModel>();

            using (SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select SelfHelpGroup.SHGName,SelfHelpGroup.SHGId,CollectionTime,CollectionDay,EmpId from TimeTable,SelfHelpGroup where TimeTable.SHGId=SelfHelpGroup.SHGId and EmpId='" + EmpId + "'";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            TimeTableViewModel TimeTable = new TimeTableViewModel();
                            TimeTable.SHGName = reader.GetString(0);
                            TimeTable.SHGId = reader.GetString(1);
                            TimeTable.CollectionTime = reader.GetTimeSpan(2);
                            TimeTable.CollectionDay = reader.GetString(3);
                            TimeTable.EmpId = reader.GetString(4);


                            TimeTableList.Add(TimeTable);
                        }
                    }
                    reader.Close();
                }
                sqlconn.Close();
            }
            return TimeTableList;

        }
        public static List<TimeTableViewModel> GetTimeTable()
        {
            List<TimeTableViewModel> TimeTableList = new List<TimeTableViewModel>();

            using (SqlConnection sqlconn = new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if (ConnectionState.Open == sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select SelfHelpGroup.SHGName,SelfHelpGroup.SHGId,CollectionTime,CollectionDay,EmpId from TimeTable,SelfHelpGroup where TimeTable.SHGId=SelfHelpGroup.SHGId";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            TimeTableViewModel TimeTable = new TimeTableViewModel();
                            TimeTable.SHGName = reader.GetString(0);
                            TimeTable.SHGId = reader.GetString(1);
                            TimeTable.CollectionTime = reader.GetTimeSpan(2);
                            TimeTable.CollectionDay = reader.GetString(3);
                            TimeTable.EmpId = reader.GetString(4);


                            TimeTableList.Add(TimeTable);
                        }
                    }
                    reader.Close();
                }
                sqlconn.Close();
            }
            return TimeTableList;

        }




        public static bool IsAlreadyHaveShedule(SHGView shg)
        {
            bool Result = false;
            using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select SHGID from TimeTable where CollectionDay='" + shg.CollectionDay + "' and CollectionTime='" + shg.CollectionTime + "' and EmpId='" + shg.NewEmployee + "'";
                    string Res = (string)sqlcomm.ExecuteScalar();
                    if(Res!=null)
                    {
                        return true;
                    }
                }
            }
            return Result;
        }

        public static string GetEmployeeName(string employeeId)
        {
            string EmployeeName = string.Empty;
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select Name from Employee where EmpId = '" + employeeId + "'";
                EmployeeName = (string)cmd.ExecuteScalar();
            }
            return EmployeeName;
        }


        public static bool AssignNewEmployeeToCenter(SHGView SHG)
        {
            bool Result = true;

            using (SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "update TimeTable set CollectionTime='" + SHG.CollectionTime + "' , Empid='" + SHG.NewEmployee + "'  where SHGId='"+SHG.SHGID+"'";
                    int res =(int) sqlcomm.ExecuteNonQuery();
                    if(res==0)
                    {
                        return false;
                    }
                }
            }
            return Result;
        }

        public static void AssignNewEmployeeToCenter(string EmpId,List<TimeTableViewModel> TimeTableList)
        {
            using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(sqlconn.State==ConnectionState.Open)
                {
                    foreach(TimeTableViewModel TT in TimeTableList)
                    {
                        SqlCommand sqlcomm = new SqlCommand();
                        sqlcomm.Connection = sqlconn;
                        sqlcomm.CommandText = "update TimeTable set EmpId='"+EmpId+"' where SHGId='"+TT.SHGId+"'";
                        sqlcomm.ExecuteNonQuery();
                    }
                    
                }
            }
        }

        public static List<EmployeeViewModel> GetEmployees(string branchId)
        {
            List<EmployeeViewModel> toReturn = new List<EmployeeViewModel>();
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = "select EmpId, Name from Employee where EmpId in (select EmpId from EmployeeBranch where IsActive = 1 and Designation = 'Field Officer' and BranchId = '" + branchId + "')";
                SqlDataReader dr = cmd.ExecuteReader();

                while(dr.Read())
                {
                    EmployeeViewModel obj = new EmployeeViewModel();
                    obj.BranchId = branchId;
                    obj.Designation = "Field Officer";
                    obj.EmployeeId = dr.GetString(0);
                    obj.EmployeeName = dr.GetString(1);
                    toReturn.Add(obj);
                }
            }
            return toReturn;
        }


        public static List<EmployeeViewModel> GetNewEmployees(string BranchId)
        {
            List<EmployeeViewModel> NewEmployees = new List<EmployeeViewModel>();
            using (SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "select Employee.EmpId,Employee.Name from Employee,EmployeeBranch where Employee.EmpId  not in(select distinct EmpId from TimeTable) and EmployeeBranch.EmpId=Employee.EmpId and EmployeeBranch.BranchId='"+BranchId+"' and EmployeeBranch.Designation='Field Officer'";
                    SqlDataReader reader = sqlcomm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while(reader.Read())
                        {
                            EmployeeViewModel employee = new EmployeeViewModel();
                            employee.BranchId = BranchId;
                            employee.EmployeeId = reader.GetString(0);
                            employee.EmployeeName = reader.GetString(1);
                            NewEmployees.Add(employee);
                        }
                    }
                    reader.Close();
                }
                sqlconn.Close();
            }
            return NewEmployees;

        }

        public static bool TransferEmployee(TransferEmployeeView EmployeeDetails)
        {
            bool Result = false;
            using(SqlConnection sqlconn=new SqlConnection(ConnectionString))
            {
                sqlconn.Open();
                if(ConnectionState.Open==sqlconn.State)
                {
                    SqlCommand sqlcomm = new SqlCommand();
                    sqlcomm.Connection = sqlconn;
                    sqlcomm.CommandText = "update EmployeeBranch set IsActive='0' where EmpId='"+EmployeeDetails.EmployeeId+"'and BranchId='"+EmployeeDetails.BranchId+"'and Designation='"+EmployeeDetails.OldDesignation+"'";
                     int res=(int)sqlcomm.ExecuteNonQuery();

                    if(res==1)
                    {
                        sqlcomm.CommandText = "insert into EmployeeBranch (Empid, BranchId, Designation, DateOfAppoint, IsActive)values('" + EmployeeDetails.EmployeeId + "','" + EmployeeDetails.NewBranchId + "','" + EmployeeDetails.NewDesignation + "','" + DateTime.Now.ToString("MM/dd/yyyy") + "'," + IsTrue(true) + ")";
                        sqlcomm.ExecuteNonQuery();
                        return true;
                    }
                    
                }
            }
            return Result;
        }
    }
}
