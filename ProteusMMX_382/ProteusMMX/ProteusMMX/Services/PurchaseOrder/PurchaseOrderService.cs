using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.PurchaseOrder
{
    public class PurchaseOrderService: IPurchaseOrderService
    {
        private readonly IRequestService _requestService;
        public PurchaseOrderService(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public Task<ServiceOutput> GetPurchaseOrders(string UserID, string PageNumber, string RowCount)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetPurchaseOrders);
            builder.AppendToPath(UserID);
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        public Task<ServiceOutput> GetReceivers()
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetReceivers);
           
            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        
        public Task<ServiceOutput> GetPurchaseOrderDetailsByRequisitionID(string RequisitionID, string UserID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetPurchaseOrderDetailByRequisitionID);
            builder.AppendToPath(RequisitionID);
            builder.AppendToPath(UserID);
         
            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        
        public Task<ServiceOutput> GetPurchaseOrderByPuchaseOrderNumber(string PurchaseOrderNumber, string UserID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetPurchaseOrdersbyPONumber);
            builder.AppendToPath(PurchaseOrderNumber);
            builder.AppendToPath(UserID);
           
          
            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> ReceivePurchaseOrderAssets(object POAsset)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.ReceivePurchaseOrderAsset);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, POAsset);//GetAsync(uri);
        }
        public Task<ServiceOutput> ReceivePurchaseOrderNonStockroomPart(object POAsset)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.ReceivePurchaseOrderNonStockroomPart);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, POAsset);//GetAsync(uri);
        }
        public Task<ServiceOutput> ReceivePurchaseOrderStockroomPart(object POAsset)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.ReceivePurchaseOrderStockroomPart);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, POAsset);//GetAsync(uri);
        }

    }
}
