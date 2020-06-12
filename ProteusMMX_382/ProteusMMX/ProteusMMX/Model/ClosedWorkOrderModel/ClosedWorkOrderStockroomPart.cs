using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.ClosedWorkOrderModel
{
    public class ClosedWorkOrderStockroomPart
    {
       
            public int? ClosedWorkOrderStockroomPartID { get; set; }
            public int? ClosedWorkOrderID { get; set; }
            public int? PartID { get; set; }
            public string PartNumber { get; set; }
            public string PartName { get; set; }
            public string StockroomName { get; set; }
            public decimal? QuantityRequired { get; set; }
            public decimal? QuantityAllocated { get; set; }
            public DateTime? ModifiedTimestamp { get; set; }
            public string ModifiedUserName { get; set; }
            public int? UnitCostID { get; set; }
        
    }
}
