using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MicroFinance.Validations
{
    public class AccountNumberValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string AccountNumber = (string)value;
            if (AccountNumber.Length <= 15)
            {
                for (int i = 0; i < AccountNumber.Length; i++)
                {
                    if (char.IsDigit(AccountNumber[i]) == false)
                    {
                        return new ValidationResult(false, "Account Number not Allowed Characters");
                    }
                }
            }
            else
            {
                return new ValidationResult(false, "Invalid Account Number");
            }
            return ValidationResult.ValidResult;
        }
    }
}
