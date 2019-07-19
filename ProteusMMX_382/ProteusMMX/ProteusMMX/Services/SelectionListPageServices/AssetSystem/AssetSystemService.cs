using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Request;

namespace ProteusMMX.Services.SelectionListPageServices.AssetSystem
{
    public class AssetSystemService : IAssetSystemService
    {
        private readonly IRequestService _requestService;
        public AssetSystemService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Task<ServiceOutput> GetAssetSystemByFacility(string UserID, string PageNumber, string RowCount, int? FacilityID, string SearchAssetSystemName)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetTargetByFacility);
            builder.AppendToPath(FacilityID.ToString());
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);
            builder.AppendToPath("null");
            if (string.IsNullOrWhiteSpace(SearchAssetSystemName))
            {
                builder.AppendToPath("null");
            }
            else
            {
                builder.AppendToPath(SearchAssetSystemName);
            }
            builder.AppendToPath("null");


            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> GetAssetSystemforAssetPage(string UserID, string PageNumber, string RowCount, string SearchAssetSystemName)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetAssetSystemForAsset);
            builder.AppendToPath(UserID.ToString());
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);
            if (string.IsNullOrWhiteSpace(SearchAssetSystemName))
            {
                builder.AppendToPath("null");
            }
            else
            {
                builder.AppendToPath(SearchAssetSystemName);
            }
           
            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> GetAssetSystemByFacilityAndLocation(string UserID, string PageNumber, string RowCount, int? FacilityID, int? LocationID, string SearchAssetSystemName)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetTargetByFacilityAndLocation);

            if (FacilityID == null)
                builder.AppendToPath("null");
            else
                builder.AppendToPath(FacilityID.ToString());

            if (LocationID == null)
                builder.AppendToPath("null");
            else
                builder.AppendToPath(LocationID.ToString());

            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);
            builder.AppendToPath("null");

            if (string.IsNullOrWhiteSpace(SearchAssetSystemName))
            {
                builder.AppendToPath("null");

            }
            else
            {
                builder.AppendToPath(SearchAssetSystemName);
            }

          

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
    }
}
