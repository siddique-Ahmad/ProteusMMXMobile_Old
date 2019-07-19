using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.Cause
{
    public class CauseService : ICauseService
    {
        private readonly IRequestService _requestService;
        public CauseService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Task<ServiceOutput> GetCause(string SearchCause)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetCause);
           
            if (string.IsNullOrWhiteSpace(SearchCause))
            {
                builder.AppendToPath("null");

            }
            else
            {
                builder.AppendToPath(SearchCause);
            }

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
    }
}
