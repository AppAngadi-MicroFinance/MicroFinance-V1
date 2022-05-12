using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
namespace MicroFinance.Converters
{
    public class ListItemSelectionColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Color Selected = (Color)System.Windows.Media.ColorConverter.ConvertFromString("#cf8e44");
            Color UnSelected = (Color)System.Windows.Media.ColorConverter.ConvertFromString("#e8c9a5");

            return ((bool)value) ? new SolidColorBrush(Selected) : new SolidColorBrush(UnSelected);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
