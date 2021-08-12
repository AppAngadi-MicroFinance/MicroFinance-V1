using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Controls;

namespace MicroFinance.Converters
{
    class MoneyConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string s = "₹. ";
            if (string.IsNullOrEmpty(value.ToString()))
            {
                return s + "0";
            }
            int price = (int)value;
            return s + price;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(string.IsNullOrEmpty(value.ToString()))
            {
                return 0;
            }
            string _money = (string)value;
            if (_money[0] == '₹')
            {
                StringBuilder sb = new StringBuilder();
                for (int i = 3; i < _money.Length; i++)
                {
                    sb.Append(_money[i]);
                }
                value = sb.ToString();
            }
            return value;
        }
    }
}
