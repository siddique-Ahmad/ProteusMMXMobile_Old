using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Request;

namespace ProteusMMX.Services.SelectionListPageServices.Location
{
    public class LocationService : ILocationService
    {
        private readonly IRequestService _requestService;
        public LocationService(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public Task<ServiceOutput> GetLocations(string UserID, string PageNumber, string RowCount, int FacilityID ,string SearchLocationName)
        {

            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetTargetByFacility);
            builder.AppendToPath(FacilityID.ToString());
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);
            builder.AppendToPath("null");
            builder.AppendToPath("null");

            if (string.IsNullOrWhiteSpace(SearchLocationName))
            {
                builder.AppendToPath("null");

            }
            else
            {
                builder.AppendToPath(SearchLocationName);
            }

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);

        }

  
    }
}
