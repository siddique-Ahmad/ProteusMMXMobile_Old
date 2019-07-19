using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Services.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.Barcode
{
    public class BarcodeService : IBarcodeService
    {
        private readonly IRequestService _requestService;
        public BarcodeService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Task<ServiceOutput> GetFormLoadInputForBarcode(string UserID )
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.Path += AppSettings.GetFormLoadInputBarcode;
            builder.Path += UserID;
            builder.Path += "/WorkOrderDetail";

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> GetAssetByAssetNumber(StockroomPartsSearch Part)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.Path += AppSettings.GetAssetByAssetNumber;
         

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri , Part);
        }

        public Task<ServiceOutput> GetAssetAttachmentsByAssetNumber(StockroomPartsSearch Part)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.Path += AppSettings.GetAssetAttachmentsByAssetNumber;


            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, Part);
        }

        public string GetAttachmentsByFileName(string FileName)
        {
            return AppSettings.BaseURL + AppSettings.GetAttachmentsByFileName + FileName;

            //UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            //builder.Path += AppSettings.GetAttachmentsByFileName;
            //builder.Path += FileName;

            //var uri = builder.ToString();
            //return uri;
            
        }
    }
}
