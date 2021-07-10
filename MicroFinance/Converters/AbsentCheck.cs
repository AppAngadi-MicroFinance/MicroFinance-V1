using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MicroFinance.Converters
{
    class AbsentCheck : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int _getNo = (int)value;
            if (_getNo == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool _get = (bool)value;
            if (true)
            {
                return 0;
            }
            else
            {
                return 2;
            }
        }
    }
}
