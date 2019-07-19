using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.CloseWorkorder
{
    public class CloseWorkorderService : ICloseWorkorderService
    {
        private readonly IRequestService _requestService;
        public CloseWorkorderService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Task<ServiceOutput> GetClosedWorkOrdersByAssetNumber(object obj)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.ClosedWorkOrdersByAssetNumber);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, obj);//GetAsync(uri);
        }

        public Task<ServiceOutput> ClosedWorkOrdersByPartNumber(object obj)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.ClosedWorkOrdersByPartNumber);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, obj);//GetAsync(uri);
        }

        public Task<ServiceOutput> ClosedWorkOrdersByWorkOrderNumber(string workorderNumber, string PageNumber, string RowspPage)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.ClosedWorkOrdersByWorkOrderNumber);
            builder.AppendToPath(workorderNumber);
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowspPage);


            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        public Task<ServiceOutput> ClosedWorkOrdersByLocation(string locationName,string PageNumber, string RowspPage)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.ClosedWorkOrdersByLocationName);
            builder.AppendToPath(locationName);
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowspPage);


            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> ClosedWorkOrdersByClosedWorkOrderDate(string UserID, string startDate, string endDate, string PageNumber, string RowspPage)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.ClosedWorkOrdersByClosedWorkOrderDate);
            builder.AppendToPath(startDate);
            builder.AppendToPath(endDate);
            builder.AppendToPath(UserID);
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowspPage);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        //ClosedWorkOrders
        public Task<ServiceOutput> ClosedWorkOrders(string closedWorkorderNumber, string UserID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.ClosedWorkOrders);
            builder.AppendToPath(closedWorkorderNumber);
            builder.AppendToPath(UserID);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        public Task<ServiceOutput> GetClosedWorkOrdersToolsByClosedWorkorderID(string CLOSEDWORKORDERID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetClosedWorkOrdersToolsByClosedWorkorderID);
            builder.AppendToPath(CLOSEDWORKORDERID);
           
            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        public Task<ServiceOutput> GetClosedWorkOrdersStockroomPartsByClosedWorkorderID(string CLOSEDWORKORDERID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetClosedWorkOrdersStockroomPartsByClosedWorkorderID);
            builder.AppendToPath(CLOSEDWORKORDERID);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        public Task<ServiceOutput> GetClosedWorkOrdersNonStockroomPartsByClosedWorkorderID(string CLOSEDWORKORDERID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetClosedWorkOrdersNonStockroomPartsByClosedWorkorderID);
            builder.AppendToPath(CLOSEDWORKORDERID);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

       
    }
}
