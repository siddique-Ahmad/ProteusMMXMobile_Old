using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.ClosedWorkOrderModel
{
    public class ClosedWorkOrderNonStockroomParts
    {
        public int? ClosedWorkOrderNonStockroomPartID { get; set; }
        public int? ClosedWorkOrderID { get; set; }
        public int? QuantityRequired { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public int? UnitCostID { get; set; }
        public DateTime? ModifiedTimestamp { get; set; }
        public string ModifiedUserName { get; set; }
    }
}
