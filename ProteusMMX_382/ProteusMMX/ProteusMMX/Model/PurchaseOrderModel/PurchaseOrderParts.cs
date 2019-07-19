using ProteusMMX.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.PurchaseOrderModel
{
    public class PurchaseOrderParts
    {
        public int PurchaseOrderPartID { get; set; }
        public int QuantityOrdered { get; set; }
        public int BalanceDue { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; }

        public int QuantityReceived { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string ReceiverName { get; set; }

        public string ShelfBin { get; set; }
        public List<ShelfBin> ShelfBins { get; set; }
    }
}
