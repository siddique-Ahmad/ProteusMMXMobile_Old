using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.PurchaseOrderModel
{
    public class PurchaseOrderPartsReceiving
    {
        public int UserId { get; set; }
        public int? PurchaseOrderPartsReceivingID { get; set; }
        public int? PurchaseOrderPartID { get; set; }
        public int? QuantityReceived { get; set; }
        public int? ReceiverID { get; set; }
        public DateTime? ReceivedDate { get; set; }
        public string InvoiceNumber { get; set; }
        public string PackingSlipNumber { get; set; }
        public DateTime? ModifiedTimestamp { get; set; }
        public string ModifiedUserName { get; set; }
        public string VendorNotes { get; set; }
        public int? ReturnedtoVendor { get; set; }
        public string ReasonforReturn { get; set; }
        public int? ShelfBinID { get; set; }
        public string ClientIANATimeZone { get; set; }
    }
}
