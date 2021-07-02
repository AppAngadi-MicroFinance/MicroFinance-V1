using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MicroFinance.Validations
{
    public class PriceValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                long Price = Convert.ToInt64(value);
                if (Price > 0 && Price <= long.MaxValue)
                {
                    return ValidationResult.ValidResult;
                }
                else
                {
                    return new ValidationResult(false, "Invalid Amount");
                }
            }
            catch
            {
                return new ValidationResult(false, "Invalid Amount");
            }
            
        }
    }
}
