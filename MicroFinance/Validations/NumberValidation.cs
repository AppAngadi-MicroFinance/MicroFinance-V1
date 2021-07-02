using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MicroFinance.Validations
{
    class NumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
           {
                int _isNumber = Convert.ToInt32(value);
                return ValidationResult.ValidResult;
            }
            catch
            {
                return new ValidationResult(false, "Number Only Allowed");
            }
        }
    }
}
