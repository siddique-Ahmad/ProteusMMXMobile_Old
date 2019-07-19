using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Request;

namespace ProteusMMX.Services.SelectionListPageServices.WorkorderRequester
{
    public class WorkorderRequesterService : IWorkorderRequesterService
    {
        private readonly IRequestService _requestService;
        public WorkorderRequesterService(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public Task<ServiceOutput> GetWorkorderRequester(string UserID, string PageNumber, string RowCount, string SearchWorkorderRequester)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkorderRequester);
            builder.AppendToPath(UserID);
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);

            if (string.IsNullOrWhiteSpace(SearchWorkorderRequester))
            {
                builder.AppendToPath("null");

            }
            else
            {
                builder.AppendToPath(SearchWorkorderRequester);
            }

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
    }
}
