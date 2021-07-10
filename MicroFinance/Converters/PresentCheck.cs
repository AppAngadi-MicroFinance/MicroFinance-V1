using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MicroFinance.Converters
{
    class PresentCheck : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int _getNo = (int)value;
            if(_getNo==1)
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
            if(true)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
