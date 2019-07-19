using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.PurchaseOrderModel
{
    public class PurchaseOrderNonStockroomPartsReceiving
    {
        public int UserId { get; set; }
        public int? PurchaseOrderNonStockroomPartsReceivingID { get; set; }
        public int? PurchaseOrderNonStockroomPartID { get; set; }
        public int? ReceiverID { get; set; }
        public int? QuantityReceived { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string PackingSlipNumber { get; set; }
        public DateTime? ModifiedTimestamp { get; set; }
        public string ModifiedUserName { get; set; }

        public string ClientIANATimeZone { get; set; }
    }
}
