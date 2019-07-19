using System;
using System.Collections.Generic;
using System.Text;

namespace ProteusMMX.Helpers.Validation
{
    public class ValidationRule
    {
        public static bool CheckInt(string input)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(input))
                {

                }
                else
                {
                    Convert.ToInt32(input);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
                
            }

        }

        public static bool CheckDecimal(string input)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(input))
                {

                }
                else
                {
                    Convert.ToDecimal(input);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }

        }
    }
}
