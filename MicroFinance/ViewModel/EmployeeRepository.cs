using System;
using System.Collections.Generic;
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
                     Sqlcomm.CommandText = "insert into Employee(EmpId,Name,DOB,age,MobileNo,Religion,EmailId,Education,AadhaarNo,DateOfJoin,BankName,BranchName,AccountNumber,IFSCCode,MICRCode,Address,PinCode,District,IsAddressProof,AddressProofName,IsPhotoProof,PhotoProofName,IsProfilePhoto,IsActive,FatherName,PanNumber,Community,Caste)values(@empId,@Name,@dob,@age,@mobile,@religion,@email,@education,@aadhar,@doj,@bankName,@branchName,@accountNo,@IFSC,@MICR,@address,@pincode,@district,@isAddressproof,@addressProofName,@isPhotoProof,@photoProofName,@isProfilePhoto,@isActive,@fatherName,@panNo,@community,@cast)";
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
                        string Folderpath = MainWindow.DriveBasePath + "\\" + "Employee\\" +employee.BranchName+"\\"+ employee.AddressProofName;
                        string FileName = EmployeeID;
                        byte[] data = Convertion(employee.AddressProofImage);
                        SaveImageToDrive.SaveImage(Folderpath, FileName, data);
                    }
                    if(employee.IsPhotoProof==true)
                    {
                        string Folderpath = MainWindow.DriveBasePath + "\\" + "Employee\\" + employee.BranchName + "\\" + employee.PhotoProofName;
                        string FileName = EmployeeID;
                        byte[] data = Convertion(employee.PhotoProofImage);
                        SaveImageToDrive.SaveImage(Folderpath, FileName, data);
                    }
                    if(employee.IsProfilePicture==true)
                    {
                        string Folderpath = MainWindow.DriveBasePath + "\\" + "Employee\\" + employee.BranchName + "\\" + "ProfilePicture";
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
    }
}
