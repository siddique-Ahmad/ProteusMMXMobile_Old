using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.PurchaseOrderModel
{
    public class PurchaseOrder
    {
        public int PurchaseOrderID { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public int RequisitionID { get; set; }

        public string VendorName { get; set; }
    }
}
