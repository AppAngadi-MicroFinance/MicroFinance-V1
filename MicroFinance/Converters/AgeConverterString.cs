using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace MicroFinance.Converters
{
    public class AgeConverterString : IValueConverter
    {
        DateTime dob = System.DateTime.Now;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            string dob1= (string)value;
            try
            {
                dob = System.Convert.ToDateTime(dob1);
            }
            catch(Exception ex)
            {

            }
           
            int age = 0;
            if (dob != DateTime.MinValue)
            {
                DateTime now = DateTime.Today;
                age = now.Year - dob.Year;

                if (now.Month < dob.Month || (now.Month == dob.Month && now.Day < dob.Day))
                    age--;
            }

            return age;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
