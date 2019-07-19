using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.PurchaseOrder
{
    public interface IPurchaseOrderService
    {
        Task<ServiceOutput> GetPurchaseOrders(string UserID, string PageNumber, string RowCount);

        Task<ServiceOutput> GetReceivers();

        Task<ServiceOutput> ReceivePurchaseOrderAssets(object POAsset);

        Task<ServiceOutput> ReceivePurchaseOrderNonStockroomPart(object POAsset);


        Task<ServiceOutput> ReceivePurchaseOrderStockroomPart(object POAsset);

        Task<ServiceOutput> GetPurchaseOrderDetailsByRequisitionID(string RequisitionID,string UserID);

        Task<ServiceOutput> GetPurchaseOrderByPuchaseOrderNumber(string PurchaseOrderNumber,string UserID);
    }
}
