using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MicroFinance.Validations
{
    public class AadharValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string Aadhar = (string)value;
            
            if(Aadhar.Length<=12)
            {
                for (int i = 0; i < Aadhar.Length; i++)
                {
                    if (char.IsDigit(Aadhar[i]) == false)
                    {
                        return new ValidationResult(false, "Aadhar Number Must Be Digit");
                    }
                }
                if (Aadhar.Length==12)
                {
                    return ValidationResult.ValidResult;
                }
                else
                {
                    return new ValidationResult(false, "Invalid Aadhar Number");
                }
               
            }
            else
            {
                return new ValidationResult(false, "Aadhar Number Must Be 12 Digit");
            }
        }

       
    }
}
