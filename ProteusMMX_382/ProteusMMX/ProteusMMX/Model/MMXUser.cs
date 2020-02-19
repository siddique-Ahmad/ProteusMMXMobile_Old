using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model
{
    public class MMXUser
    {
        public string EmployeeName { get; set; }
        public string EmailAddress { get; set; }
        public string UserName { get; set; }

        public string Password { get; set; }
        public int UserID { get; set; }
        public bool IsMobileUser { get; set; }
        public string DisplayLanguage { get; set; }
        public string TimeZone { get; set; }
        public bool UserIsAdmin { get; set; }
        public bool UserIsSRAdmin { get; set; }
        public MobileRightExpression mobileRight { get; set; }

        public bool? IsInspectionUser { get; set; }

        public bool? IsCheckedAutoFillStartdateOnTaskAndLabor { get; set; }

        public string ServerIANATimeZone { get; set; }

        public string CurrencyZero { get; set; }

        public string NumericZero { get; set; }

        public bool ULFeatures { get; set; }

        public string UserLicense { get; set; }

        public BlackhawkLicValidator blackhawkLicValidator { get; set; }


        public string CompanyName { get; set; }

        public string RequireSignaturesForValidation { get; set; }

        public bool EnableHoursAtRate { get; set; }
    }

}

