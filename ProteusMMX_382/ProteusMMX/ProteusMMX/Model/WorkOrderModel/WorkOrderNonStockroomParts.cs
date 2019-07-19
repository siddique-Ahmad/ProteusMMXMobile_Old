using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class WorkOrderNonStockroomParts
    {
        public int WorkOrderNonStockroomPartID { get; set; }
        public int? WorkOrderID { get; set; }
        public int? QuantityRequired { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public int? UnitCostID { get; set; }
        public DateTime? ModifiedTimestamp { get; set; }
        public string ModifiedUserName { get; set; }
        public decimal? UnitCostAmount { get; set; }
        public string UnitCostCurrency { get; set; }
    }
}
