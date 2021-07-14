
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
                if (_money[0] == '₹')
                {
                StringBuilder sb = new StringBuilder();
                    for(int i=3;i<_money.Length;i++)
                    {
                    sb.Append(_money[i]);
                    }
                value = sb.ToString();
                }
                int _isNumber = Convert.ToInt32(value);
                return ValidationResult.ValidResult;
        }
            catch(Exception E)
            {
                return new ValidationResult(false, "Number Only Allowed");
            }

}
    }
}
