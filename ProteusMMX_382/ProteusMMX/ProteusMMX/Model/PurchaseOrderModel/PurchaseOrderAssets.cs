using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.PurchaseOrderModel
{
    public class PurchaseOrderAssets
    {
        public int AssetID { get; set; }
        public string AssetNumber { get; set; }
        public string AssetName { get; set; }

        public DateTime? ReceivedDate { get; set; }
        public int PurchaseOrderAssetID { get; set; }

        public string ReceiverName { get; set; }
        public int UserId { get; set; }
        public string ClientIANATimeZone { get; set; }
    }
}
