
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MicroFinance.Validations
{
    class MoneyValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                string _money = (string)value;
                int _isNumber = Convert.ToInt32(_money);
                return ValidationResult.ValidResult;
            }
            catch
            {
                return new ValidationResult(false, "Number Only Allowed");
            }
        }
    }
}
