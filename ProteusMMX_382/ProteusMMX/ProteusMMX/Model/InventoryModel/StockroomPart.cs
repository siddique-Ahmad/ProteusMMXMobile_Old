using ProteusMMX.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.InventoryModel
{
    public class StockroomPart
    {
        public int? StockroomPartID { get; set; }
        public string SerialNumber { get; set; }
        public string ShelfBin { get; set; }
        public int? QuantityOnHand { get; set; }
        public int? UnitCostID { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public string PartSize { get; set; }
        public string Description { get; set; }
        public string MeasurementUnit { get; set; }
        public string Manufacturer { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public string ShelfLife { get; set; }
        public string StockClassName { get; set; }
        public string CategoryName { get; set; }
        public decimal? UnitCostAmount { get; set; }
        public string UnitCostCurrency { get; set; }
        public decimal? QuantityAllocated { get; set; }
        public decimal? TotalQuantityAvailable { get; set; }
        public int? EconomicOrderQuantity { get; set; }
        public int? PhysicalInventoryFrequency { get; set; }
        public DateTime? LastPhysicalInventoryDate { get; set; }
        public int? MaximumQuantity { get; set; }
        public int? ReorderPoint { get; set; }
        public DateTime? NextPhysicalInventoryDate { get; set; }
        public decimal? CarryingCostAmount { get; set; }
        public string CarryingCostCurrency { get; set; }
        public string IsCriticalPart { get; set; }
        public decimal? OriginalAmount { get; set; }
        public bool? IsPartSerialized { get; set; }
        public List<ShelfBin> ShelfBins { get; set; }

        public string PartNumberLocal
        {
            get
            {
                if (string.IsNullOrWhiteSpace(PartName))
                {
                    return PartNumber;
                }
                return PartNumber + "(" + PartName + ")";
            }
        }
    }
}
