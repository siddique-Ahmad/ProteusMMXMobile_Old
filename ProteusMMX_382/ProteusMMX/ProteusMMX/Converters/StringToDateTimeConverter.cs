using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace ProteusMMX.Converters
{
    public class StringToDateTimeConverter : IValueConverter
    {
        DateTime dt;
        string s = string.Empty;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if (value == null)
                {
                    return null;
                }
                s = value.ToString();
                var formats = new string[]
                {
             "dd/MM/yyyy",
             "d/M/yyyy",
             "dd/mm/yy",
             "d/m/yy",

             "dd-MM-yyyy",
             "d-M-yyyy",
             "dd-MM-yy",
             "d-M-yy",

             "dd.MM.yyyy",
             "d.M.yyyy",
             "dd.MM.yy",
             "d.M.yy",

             "dd MM yyyy",
             "d M yyyy",
             "dd MM yy",
             "d M yy",
                };
               
                DateTime.TryParseExact(s, formats, CultureInfo.InvariantCulture,DateTimeStyles.None, out dt);
               // object converteddate = dt;
                if (value.ToString() == dt.ToString())
                {
                    return null;
                }


                //if (value.ToString() == "01-Jan-01 12:00:00")
                //{
                //    return null;
                //}
                if (value != null)
                {
                    DateTime date;
                    DateTime.TryParse(value.ToString(), out date);
                    return date;

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
          
            if (value == null)
            {
                return null;
            }
            else if (value.ToString() == dt.ToString())
            {
                value = null;
                return value;
            }
            
            else
            {
                return value.ToString();

            }
        }
    }
}
