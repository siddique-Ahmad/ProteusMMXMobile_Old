using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.Asset
{
    public class AssetService : IAssetService
    {
        private readonly IRequestService _requestService;
        public AssetService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Task<ServiceOutput> GetAssetsByFacility(string UserID, string PageNumber, string RowCount, int? FacilityID, string SearchAssetName)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetTargetByFacility);
            builder.AppendToPath(FacilityID.ToString());
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);

            if (string.IsNullOrWhiteSpace(SearchAssetName))
            {
                builder.AppendToPath("null");
            }
            else
            {
                builder.AppendToPath(SearchAssetName);
            }

            builder.AppendToPath("null");
            builder.AppendToPath("null");


            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> GetAssetsByFacilityAndLocation(string UserID, string PageNumber, string RowCount, int? FacilityID, int? LocationID, string SearchAssetName)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetTargetByFacilityAndLocation);

            if (FacilityID==null)
                builder.AppendToPath("null");
            else
                builder.AppendToPath(FacilityID.ToString());

            if (LocationID == null)
                builder.AppendToPath("null");
            else
                builder.AppendToPath(LocationID.ToString());

            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);
          
            if (string.IsNullOrWhiteSpace(SearchAssetName))
            {
                builder.AppendToPath("null");

            }
            else
            {
                builder.AppendToPath(SearchAssetName);
            }

            builder.AppendToPath("null");

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

     


    }
}
