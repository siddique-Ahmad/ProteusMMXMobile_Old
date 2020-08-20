using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.AssetModel
{
    public class Assets
    {
        public int AssetID { get; set; }
        public string AssetNumber { get; set; }
        public string AssetName { get; set; }
        public decimal CurrentRuntime { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string SerialNumber { get; set; }
        public string AssetTag { get; set; }
        public string Description { get; set; }
        public DateTime? InstallationDate { get; set; }
        public DateTime? WarrantyDate { get; set; }
        public string Weight { get; set; }
        public string Rating { get; set; }
        public string Capacity { get; set; }
        public string Model { get; set; }
        public string Manufacturer { get; set; }
        public decimal? DailyRuntime { get; set; }
        public int? RuntimeUnits { get; set; }
        public string AdditionalDetails { get; set; }
        public int? BillOfMaterialID { get; set; }
        public int? CategoryID { get; set; }
        public int? LocationID { get; set; }
        public int? VendorID { get; set; }
        public int? CustomerLocationID { get; set; }
        public DateTime ModifiedTimestamp { get; set; }
        public string ModifiedUserName { get; set; }
        public int? OriginalCostID { get; set; }
        public int? FacilityID { get; set; }
        public int? BuildingAutomationBindingID { get; set; }
        public int? AssetSystemID { get; set; }
        public string M3AssetID { get; set; }
        public string Make { get; set; }
        public string DetailedLocation { get; set; }
        public decimal? InstantaneousDemand { get; set; }
        public decimal? IntervalDemand { get; set; }
        public decimal? InstantaneousConsumption { get; set; }
        public decimal? IntervalConsumption { get; set; }
        public string UserField1 { get; set; }
        public string UserField2 { get; set; }
        public string UserField3 { get; set; }
        public string UserField4 { get; set; }
        public string UserField5 { get; set; }
        public string UserField6 { get; set; }
        public string UserField7 { get; set; }
        public string UserField8 { get; set; }
        public string UserField9 { get; set; }
        public string UserField10 { get; set; }
        public string UserField11 { get; set; }
        public string UserField12 { get; set; }
        public string UserField13 { get; set; }
        public string UserField14 { get; set; }
        public string UserField15 { get; set; }
        public string UserField16 { get; set; }
        public string UserField17 { get; set; }
        public string UserField18 { get; set; }
        public string UserField19 { get; set; }
        public string UserField20 { get; set; }
        public string UserField21 { get; set; }
        public string UserField22 { get; set; }
        public string UserField23 { get; set; }
        public string UserField24 { get; set; }
        public List<AssetAttachments> attachments { get; set; }
        public string CustomerLocationName { get; set; }
        public string CustomerName { get; set; }
        public string CategoryName { get; set; }
        public string VendorCode { get; set; }
        public string AssetSystemNumber { get; set; }
        public string FacilityName { get; set; }
        public string LocationName { get; set; }
        public string LocationType { get; set; }

        public string AssetSystemName { get; set; }
        public string VendorName { get; set; }

        public string RuntimeUnitName { get; set; }

        public decimal? OriginalCost { get; set; }
        public string LOTOUrl { get; set; }
    }
}
