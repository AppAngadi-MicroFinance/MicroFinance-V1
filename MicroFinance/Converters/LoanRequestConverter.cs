using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Media;
using System.Windows.Media;

namespace MicroFinance.Converters
{
    class LoanRequestConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool _check = (bool)value;
            BrushConverter bc = new BrushConverter();

            if (_check)
            {

                return (Brush)bc.ConvertFrom("Green");
            }
            else
            {
                return (Brush)bc.ConvertFrom("Blue");
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
