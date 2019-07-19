using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Request;

namespace ProteusMMX.Services.SelectionListPageServices.WorkorderType
{
    public class WorkorderTypeService : IWorkorderTypeService
    {
        private readonly IRequestService _requestService;
        public WorkorderTypeService(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public Task<ServiceOutput> GetWorkorderType(string UserID, string PageNumber, string RowCount, string SearchWorkorderType)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkOrderType);
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);

            if (string.IsNullOrWhiteSpace(SearchWorkorderType))
            {
                builder.AppendToPath("null");

            }
            else
            {
                builder.AppendToPath(SearchWorkorderType);
            }

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
    }
}
