using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Request;

namespace ProteusMMX.Services.SelectionListPageServices
{
    public class FacilityService : IFacilityService
    {
        private readonly IRequestService _requestService;
        public FacilityService(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public Task<ServiceOutput> GetFacilities(string UserID, string PageNumber, string RowCount, string SearchFacilityName)
        {

            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetComboListFacilitiesFacilityWise);
            builder.AppendToPath(UserID);
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);

            if (string.IsNullOrWhiteSpace(SearchFacilityName))
            {
                builder.AppendToPath("null");

            }
            else
            {
                builder.AppendToPath(SearchFacilityName);
            }

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);

        }
    }
}
