using System;
using System.Collections.Generic;
using System.Text;

namespace ProteusMMX.Model.PurchaseOrderModel
{
    public class PurchaseOrderListModel
    {
        public int PurchaseOrderID { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public int RequisitionID { get; set; }

        public string VendorName { get; set; }
    }
}
