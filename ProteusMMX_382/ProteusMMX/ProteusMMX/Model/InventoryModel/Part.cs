using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.InventoryModel
{
    public class Part
    {
        public int PartID { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public int CategoryID { get; set; }
        public int StockClassID { get; set; }
        public string PartSize { get; set; }
        public string Description { get; set; }
        public string MeasurementUnit { get; set; }
        public string Manufacturer { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public string ShelfLife { get; set; }
        public string AdditionalDetails { get; set; }
        public string ModifiedUserName { get; set; }
        public int LastCostID { get; set; }
        public int StandardCostID { get; set; }
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
        public string CategoryName { get; set; }
        public string StockClassName { get; set; }
        public int? StockroomPartID { get; set; }
        public int? QuantityOnHand { get; set; }
        public decimal? QuantityAllocated { get; set; }
        public string ShelfBin { get; set; }
        public string SerialNumber { get; set; }

       
    }
}
