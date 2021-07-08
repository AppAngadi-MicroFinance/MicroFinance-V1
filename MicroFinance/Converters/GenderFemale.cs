using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MicroFinance.Converters
{
    class GenderFemale : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                String _genderType = value.ToString().ToUpper();
                if (_genderType.Equals("FEMALE"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if((bool)value)
            {
                return "Female";
            }
            else
            {
                return "";
            }
        }
    }
}
