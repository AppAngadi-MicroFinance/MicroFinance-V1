using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MicroFinance.Validations
{
    public class EbNumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string Ebnumber = (string)value;
            if(Ebnumber.Length==12)
            {
                for(int i=0;i<Ebnumber.Length;i++)
                {
                    if(char.IsDigit(Ebnumber[i])==false)
                    {
                        return new ValidationResult(false, "Eb Number only Allows 0-9");
                    }
                }
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "Eb Number is Invalid");
            }
            
        }
    }
}
