using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class StockroomPartsLookUp
    {
        public int? StockroomPartID { get; set; }
        public string StockroomName { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public string Manufacturer { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public int? QuantityOnHand { get; set; }
        public string SerialNumber { get; set; }
        public decimal? UnitCostAmount { get; set; }
        public string UnitCostCurrency { get; set; }
    }
}
