using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.Workorder.Attachments
{
    public class AttachmentService : IAttachmentService
    {
        public StockroomPartsSearch PartToSearch { get; set; }
        private readonly IRequestService _requestService;
        public AttachmentService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Task<ServiceOutput> GetWorkorderAttachments(string UserID, string WorkorderID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkOrderAttachments);
            builder.AppendToPath(WorkorderID);
            builder.AppendToPath(UserID);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        public Task<ServiceOutput> ClosedWorkorderAttachmentByClosedWorkorderID(string CLOSEDWORKORDERID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.ClosedWorkorderAttachmentByClosedWorkorderID);
            builder.AppendToPath(CLOSEDWORKORDERID);
          
            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        

        public Task<ServiceOutput> DeleteWorkorderAttachment(object workorder)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.DeleteWorkOrderAttachments);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, workorder);
        }

        public Task<ServiceOutput> CreateWorkorderAttachment(string UserID, object workorder)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.CreateWorkOrderAttachments);
            builder.AppendToPath(UserID);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, workorder);
        }
        public Task<ServiceOutput> GetAssetAttachmentsByAssetNumber(string AssetNumber)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetAssetAttachmentsByAssetNumber);
            var uri = builder.Uri.AbsoluteUri;
            PartToSearch = new StockroomPartsSearch();

            PartToSearch.AssetNumber = AssetNumber;
         

            return _requestService.PostAsync(uri, PartToSearch);
        }
    }
}
