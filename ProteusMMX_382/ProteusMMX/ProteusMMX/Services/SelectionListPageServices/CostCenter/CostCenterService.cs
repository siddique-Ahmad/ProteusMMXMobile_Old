using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.CostCenter
{
    public class CostCenterService : ICostCenterService
    {
        private readonly IRequestService _requestService;
        public CostCenterService(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public Task<ServiceOutput> GetCostCenters(string UserID, string PageNumber, string RowCount, string SearchCostCenterName)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetCostCenters);
            builder.AppendToPath(UserID);
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);

            if (string.IsNullOrWhiteSpace(SearchCostCenterName))
            {
                builder.AppendToPath("null");

            }
            else
            {
                builder.AppendToPath(SearchCostCenterName);
            }

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
    }
}
