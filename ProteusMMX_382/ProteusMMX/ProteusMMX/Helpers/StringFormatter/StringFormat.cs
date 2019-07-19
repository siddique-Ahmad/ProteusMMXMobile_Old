using System;
using System.Collections.Generic;
using System.Text;

namespace ProteusMMX.Helpers.StringFormatter
{
    public class StringFormat
    {
        public static string CurrencyZero()
        {
            string CurrencyZero = AppSettings.User.CurrencyZero; //Device2.Storage.Get("CurrencyZero");
            string numberOfDecimalPlaces = CurrencyZero;
            string formatString = String.Concat("{0:F", numberOfDecimalPlaces, "}");
            return formatString;
        }

        public static string NumericZero()
        {
            string NumericZero = AppSettings.User.NumericZero; //Device2.Storage.Get("NumericZero");
            string numberOfDecimalPlaces = NumericZero;
            string formatString = String.Concat("{0:F", numberOfDecimalPlaces, "}");
            return formatString;
        }
    }
}
