using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MicroFinance.Validations
{
    class PanNumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string PanNumber = (string)value;
            if(PanNumber.Length==10)
            {
                if(IsPanNumber(PanNumber)==true)
                {
                    return ValidationResult.ValidResult;
                }
                else
                {
                    return new ValidationResult(false, "Invalid Pan Number");
                }
            }
            else
            {
                return new ValidationResult(false, "Invalid Pan Number");
            }
            

        }
        public bool IsPanNumber(string pannumber)
        {
           
            for (int i = 0; i < pannumber.Length; i++)
            {
                if (i <= 4)
                {
                    if (char.IsLetter(pannumber[i]) == true)
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (i > 4 && i <= 8)
                {
                    if (char.IsDigit(pannumber[i]) == true)
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
                if (i==9)
                {
                    if(char.IsLetter(pannumber[i])==true)
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
