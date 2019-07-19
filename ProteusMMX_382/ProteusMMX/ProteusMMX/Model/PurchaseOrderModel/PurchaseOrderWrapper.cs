using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.PurchaseOrderModel
{
    public class PurchaseOrderWrapper
    {
        public List<PurchaseOrder> purchaseOrders { get; set; }

        public int recordCountPO { get; set; }
        public List<PurchaseOrderParts> purchaseOrderParts { get; set; }
        public List<PurchaseOrderNonStockroomParts> purchaseOrderNonStockroomParts { get; set; }
        public List<PurchaseOrderAssets> purchaseOrderAssets { get; set; }
        public List<POReceiver> receivers { get; set; }
        public PurchaseOrderAssetsReceiving assetsReceiving { get; set; }
        public PurchaseOrderPartsReceiving partsReceiving { get; set; }

    }
}
