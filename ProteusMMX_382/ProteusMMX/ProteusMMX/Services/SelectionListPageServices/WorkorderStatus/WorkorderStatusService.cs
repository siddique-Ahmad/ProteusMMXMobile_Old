using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Request;

namespace ProteusMMX.Services.SelectionListPageServices.WorkorderStatus
{
    public class WorkorderStatusService : IWorkorderStatusService
    {
        private readonly IRequestService _requestService;
        public WorkorderStatusService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Task<ServiceOutput> GetWorkorderStatus(string UserID, string PageNumber, string RowCount, string SearchWorkorderStatusName)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkOrderStatus);
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);

            if (string.IsNullOrWhiteSpace(SearchWorkorderStatusName))
            {
                builder.AppendToPath("null");

            }
            else
            {
                builder.AppendToPath(SearchWorkorderStatusName);
            }

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
    }
}
