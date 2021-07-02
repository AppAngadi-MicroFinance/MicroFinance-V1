using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroFinance.Modal
{
    public class Validation
    {
        public bool IsAmount(string value)
        {
            int a;
            bool result = int.TryParse(value, out a);
            if (result != true)
            {
                throw new ArgumentException("Invalid Amount");
            }
            return true;
        }
        public bool IsLandline(string value)
        {
            double v;
            bool result = double.TryParse(value, out v);
            if (result == true)
            {
                return true;
            }
            return false;
        }
        public bool IsString(string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsDigit(value[i]) == true)
                {
                    throw new ArgumentException("Numbers Not Allowed");
                }
            }
            return true;
        }
        public bool IsEBNumber(string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsDigit(value[i]) == false)
                {
                    throw new ArgumentException("The Eb Number is Invalid");
                }
            }
            string temp = value;
            if (temp.Length <= 12)
            {
                if (temp.Length != 12)
                {
                    throw new ArgumentException("The Eb Number is Invalid,Number Must Be 12 Digit");
                }
            }
            else
            {
                throw new ArgumentException("EB Number is Too Long");
            }

            return true;

        }
        public bool IsAadhar(string value)
        {
            bool AadharNumber;
            double a;
            bool res = double.TryParse(value, out a);
            if (res == true)
            {
                string temp = a.ToString();
                if (temp.Length <= 12)
                {
                    if (temp.Length == 12)
                    {
                        AadharNumber = true;
                    }
                    else
                    {
                        throw new ArgumentException("Aadhar Number Must Be 12 Digit");
                    }

                }
                else
                {
                    throw new ArgumentException("Aadhar Number is Too Long");
                }
            }
            else
            {
                throw new ArgumentException("Aadhar Number is Invalid");
            }
            return AadharNumber;

        }
        public bool IsAccountNumber(string value)
        {
            bool result;
            double a;
            bool res = double.TryParse(value, out a);
            if (res == true)
            {
                string temp = a.ToString();
                if (temp.Length <= 15)
                {

                    result = true;
                }
                else
                {
                    throw new ArgumentException("Account Number is Invalid,Account Number length must be Less than or Equal to 15");
                }
            }
            else
            {
                throw new ArgumentException("Account Number is Invalid");
            }
            return result;

        }
        public bool IsPhoneNumber(string value)
        {
            bool Phnumber;
            bool res;
            double number;
            res = double.TryParse(value, out number);

            if (res == true)
            {
                double digit = Math.Floor(Math.Log10(number) + 1);
                if (digit <= 10)
                {
                    if (digit == 10)
                    {
                        Phnumber = true;
                    }
                    else
                    {
                        throw new ArgumentException("Phone Number is Invalid,Phone Number Must be 10 digit");
                    }

                }
                else
                {
                    throw new ArgumentException("The PhoneNumber is Too Long");
                }

            }
            else
            {
                throw new ArgumentException("Phone Number is Invalid");
            }
            return Phnumber;
        }
        public bool IsEmail(string value)
        {
            List<char> symbol = new List<char> { '~', '`', '!', '#', '$', '%', '^', ',','"',':',';','?','?','/','*','+','-'};
            string googlemail = "@gmail.com";
            string yahoomail = "@yahoo.com";
            bool Result;
            if(value.Length>=11)
            {
                for(int i=0;i<value.Length-10;i++)
                {
                    if(char.IsUpper(value[i])==true)
                    {
                        throw new ArgumentException("Email Cannot Be UpperCase");
                    }
                    if(symbol.Contains(value[i])==true)
                    {
                        throw new ArgumentException("Email Cannot Allowned Special Cases");
                    }
                }
                int callength = value.Length-10;
                string ispattern = value.Substring(callength);
                if(ispattern==googlemail||ispattern==yahoomail)
                {
                    Result = true;
                }
                else
                {
                    throw new ArgumentException("Email not in CorrectFormat");
                }

            }
            else
            {
                throw new ArgumentException("Email is Invalid");
            }
            return Result;
        }
    }
}
