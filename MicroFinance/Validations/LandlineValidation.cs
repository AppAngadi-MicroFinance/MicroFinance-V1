using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MicroFinance.Validations
{
    public class LandlineValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string Landlinenumber = (string)value;
            if(Landlinenumber.Length==11||Landlinenumber.Length==6)
            {
                for(int i=0;i<Landlinenumber.Length;i++)
                {
                    if(char.IsDigit(Landlinenumber[i])==false)
                    {
                        return new ValidationResult(false, "Invalid LandLine Number,");
                    }
                }
                return ValidationResult.ValidResult;

            }
            else
            {
                return new ValidationResult(false, "Invalid LandlineNumber");
            }
           
        }
    }
}
