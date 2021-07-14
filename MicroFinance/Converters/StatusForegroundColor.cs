using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;
using System.Windows.Media;

namespace MicroFinance.Converters
{
    class StatusForegroundColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int _message = (int)value;

            BrushConverter bc = new BrushConverter();

            if (_message==1)
            {
                return (Brush)bc.ConvertFrom("Blue");
            }
            else if(_message==0)
            {
                return (Brush)bc.ConvertFrom("Red");
            }
            else
            {
                return (Brush)bc.ConvertFrom("#26d512");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
