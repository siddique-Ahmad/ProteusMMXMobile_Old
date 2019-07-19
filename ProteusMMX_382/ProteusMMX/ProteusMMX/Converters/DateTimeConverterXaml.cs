using ProteusMMX.Helpers.DateTime;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ProteusMMX.Converters
{
    public class DateTimeConverterXaml : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
               
                if (value != null)
                {
                    DateTime date;
                    DateTime.TryParse(value.ToString(), out date);
                    return DateTimeConverter.ConvertDateTimeToDifferentTimeZone(date.ToUniversalTime(), AppSettings.User.ServerIANATimeZone); //ServerTimeZone);

                }

                else
                {
                    return value;
                }

            }
            catch (Exception)
            {

                return value;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
