using System;
using System.Collections.Generic;
using System.Text;

namespace ProteusMMX.Model
{
    public class BlackhawkLicValidator
    {
        public string ProductLevel { get; set; }
        public bool InspectionModuleIsEnabled { get; set; }
        public bool KPIModuleIsEnabled { get; set; }
        public bool StandardKPIIsEnabled { get; set; }
        public bool CustomKPIISEnabled { get; set; }
        public bool ActiveDirectoryModuleIsEnabled { get; set; }
        public bool MachineLedgerIsEnabled { get; set; }
        public bool APICallIsAllowed { get; set; }
        public bool SRQPortalsModuleIsEnabled { get; set; }
        public int UserLicenseCount { get; set; }
        public int FacilityLicenseCount { get; set; }
        public int MobileUserLicenseCount { get; set; }
        public int ProteusAlarmCount { get; set; }
        public int ServiceRequestAdminCount { get; set; }
        public int InspectionUserLicenseCount { get; set; }
        public int InspectionAdminLicenseCount { get; set; }
        public int ActiveDirectoryUserLicenseCount { get; set; }
        public string ProductKeyExpirationDate { get; set; }

        public bool ServiceRequestIsEnabled { get; set; }

        public bool RiskAssasment { get; set; }

        public bool IsFDASignatureValidation { get; set; }

    }
}
