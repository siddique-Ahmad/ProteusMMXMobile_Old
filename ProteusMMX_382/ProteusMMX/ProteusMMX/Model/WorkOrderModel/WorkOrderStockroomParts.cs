using ProteusMMX.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class WorkOrderStockroomParts
    {
        public int WorkOrderStockroomPartID { get; set; }
        public int? WorkOrderID { get; set; }
        public int? StockroomPartID { get; set; }
        public int? QuantityRequired { get; set; }
        public int? QuantityAllocated { get; set; }
        public DateTime? ModifiedTimestamp { get; set; }
        public string ModifiedUserName { get; set; }
        public int? UnitCostID { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public string SerialNumber { get; set; }
        public string StockroomName { get; set; }
        public decimal? UnitCostAmount { get; set; }
        public string UnitCostCurrency { get; set; }

        public string ShelfBin { get; set; }

        public List<ShelfBin> ShelfBins { get; set; }

        public int? ShelfBinID { get; set; }
    }
}
