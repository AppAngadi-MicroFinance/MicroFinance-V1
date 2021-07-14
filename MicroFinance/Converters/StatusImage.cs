using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows;

namespace MicroFinance.Converters 
{
    class StatusImage : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int _type = (int)value;

            if(_type==1)
            {
                return "Asserts/Icons/Done-Shield-WF.png";
            }
            else if(_type==2)
            {
                return "Asserts/Icons/Warning Shield-WF.png";
            }
            else
            {
                return "Asserts/Icons/Warning Shield-WF.png";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
