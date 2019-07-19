using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Request;

namespace ProteusMMX.Services.Translations
{
    public class WebControlTitlesService : IWebControlTitlesService
    {
        private readonly IRequestService _requestService;
        public WebControlTitlesService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Task<ServiceOutput> GetWebControlTitles(string UserID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWebControlTitles);
            builder.AppendToPath(UserID);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
    }
}
