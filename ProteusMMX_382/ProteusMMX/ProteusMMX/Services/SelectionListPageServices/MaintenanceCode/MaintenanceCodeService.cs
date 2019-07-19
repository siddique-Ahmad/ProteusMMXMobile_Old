using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Request;

namespace ProteusMMX.Services.SelectionListPageServices.MaintenanceCode
{
    public class MaintenanceCodeService : IMaintenanceCodeService
    {

        private readonly IRequestService _requestService;
        public MaintenanceCodeService(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public Task<ServiceOutput> GetMaintenanceCode(string PageNumber, string RowCount, string SearchMaintenanceCode)
        {

            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetMaintenanceCodes);
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);

            if (string.IsNullOrWhiteSpace(SearchMaintenanceCode))
            {
                builder.AppendToPath("null");

            }
            else
            {
                builder.AppendToPath(SearchMaintenanceCode);
            }

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);

        }
    }
}
