using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MicroFinance.Validations
{
    public class EmailValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string Email = (string)value;
            List<char> symbol = new List<char> { '~', '`', '!', '#', '$', '%', '^', ',', '"', ':', ';', '?', '?', '/', '*', '+', '-' };
            string googlemail = "@gmail.com";
            string yahoomail = "@yahoo.com";
            if (Email.Length >= 11)
            {
                for (int i = 0; i < Email.Length - 10; i++)
                {
                    if (char.IsUpper(Email[i]) == true)
                    {
                       return new ValidationResult(false,"Email Cannot Be UpperCase");
                    }
                    if (symbol.Contains(Email[i]) == true)
                    {
                        return new ValidationResult(false,"Email Cannot Allowned Special Cases");
                    }
                }
                int callength = Email.Length - 10;
                string ispattern = Email.Substring(callength);
                if (ispattern == googlemail || ispattern == yahoomail)
                {
                    return ValidationResult.ValidResult;
                }
                else
                {
                    return new ValidationResult(false,"Email not in CorrectFormat");
                }

            }
            else
            {
                return new ValidationResult(false,"Email is Invalid");
            }
        }
    }
}
