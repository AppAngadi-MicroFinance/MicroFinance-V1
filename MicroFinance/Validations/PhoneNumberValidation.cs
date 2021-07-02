using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MicroFinance.Validations
{
    public class PhoneNumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string Phonenumber = (string)value;
            if(Phonenumber.Length<=10)
            {
                for (int i = 0; i < Phonenumber.Length; i++)
                {
                    if(char.IsDigit(Phonenumber[i])==false)
                    {
                        return new ValidationResult(false, "Phone Number not Allowed Characters");
                    }
                }
                if(Phonenumber.Length==10)
                {
                    return ValidationResult.ValidResult;
                }
                else
                {
                    return new ValidationResult(false, "Invalid PhoneNumber");
                }

            }
            else
            {
                return new ValidationResult(false, "PhoneNumber Must be 10 Digits");
            }
            
            
        }
    }
}
