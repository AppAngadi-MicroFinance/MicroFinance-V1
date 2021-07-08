using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MicroFinance.Converters
{
    class MonthConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string _months = value.ToString();
            int i = 0;
            int _no = 0;
            while(_months[i]!=' ')
            {
                _no = _no * 10;
                _no = _no + (_months[i] - 48);
                i++;
            }
            return _no;
        }
    }
}
