using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Request;

namespace ProteusMMX.Services.SelectionListPageServices.Priority
{
    public class PriorityService : IPriorityService
    {
        private readonly IRequestService _requestService;
        public PriorityService(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public Task<ServiceOutput> GetPriorities(string PageNumber, string RowCount, string SearchPriorityName)
        {

            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetPriorities);
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);

            if (string.IsNullOrWhiteSpace(SearchPriorityName))
            {
                builder.AppendToPath("null");

            }
            else
            {
                builder.AppendToPath(SearchPriorityName);
            }

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);

        }
    }
}
