using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Helpers
{
    public class ShortString
    {
        public static string shorten(string yourStr)
        {
            string toView;
            const int maxView = 40;

            if (yourStr.Length > maxView)
                toView = yourStr.Substring(0, maxView) + " ..."; // all you have is to use Substring(int, int) .net method
            else
                toView = yourStr;
            return toView;
        }

        public static string shortenMobile(string yourStr)
        {
            string toView;
            const int maxView = 90;

            if (yourStr.Length > maxView)
                toView = yourStr.Substring(0, maxView) + " ..."; // all you have is to use Substring(int, int) .net method
            else
                toView = yourStr;
            return toView;
        }
        public static string shortenSignature(string yourStr)
        {
            string toView;
            const int maxView = 25;

            if (yourStr.Length > maxView)
                toView = yourStr.Substring(0, maxView) + " ..."; // all you have is to use Substring(int, int) .net method
            else
                toView = yourStr;
            return toView;
        }
    }
}
