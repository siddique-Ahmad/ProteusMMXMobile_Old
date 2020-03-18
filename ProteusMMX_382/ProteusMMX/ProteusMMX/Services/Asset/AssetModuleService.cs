using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.Asset
{
    public class AssetModuleService: IAssetModuleService
    {
        public StockroomPartsSearch AssetToSearch { get; set; }
        private readonly IRequestService _requestService;
        public AssetModuleService(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public Task<ServiceOutput> GetAssets(string UserID, string PageNumber, string RowCount)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetAssets);
            builder.AppendToPath(UserID);
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        public Task<ServiceOutput> GetAssetsBYAssetID(string AssetID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetAssetsBYAssetID);
            builder.AppendToPath(AssetID);
           
            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        
        public Task<ServiceOutput> GetRuntimeUnit(string UserID, string RuntimeUnit)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetRuntimeUnit);
            builder.AppendToPath(UserID);
            if (String.IsNullOrWhiteSpace(RuntimeUnit))
            {
                builder.AppendToPath("null");
            }
            else
            {
                builder.AppendToPath(RuntimeUnit);
            }


            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> GetCategory(string UserID, string SearchCategory)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetCategory);
            builder.AppendToPath(UserID);

            if (String.IsNullOrWhiteSpace(SearchCategory))
            {
                builder.AppendToPath("null");
            }
            else
            {
                builder.AppendToPath(SearchCategory);
            }

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> GetVendor(string UserID, string PageNumber, string RowCount, string Vendor)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetVendor);
            builder.AppendToPath(UserID);
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);
            if (String.IsNullOrWhiteSpace(Vendor))
            {
                builder.AppendToPath("null");
            }
            else
            {
                builder.AppendToPath(Vendor);
            }


            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> GetAssetsFromSearchBar(string AssetNumber, string UserID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetAssetsFromSearchBar);

            var uri = builder.Uri.AbsoluteUri;
            AssetToSearch = new StockroomPartsSearch();
            AssetToSearch.AssetNumber = AssetNumber;
            AssetToSearch.UserID = Convert.ToInt32(UserID);
            return _requestService.PostAsync(uri, AssetToSearch);
        }
        public Task<ServiceOutput> GetAssetsFromSearchBarFromBarcode(object obj)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetAssetsFromSearchBar);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, obj);
        }



        public Task<ServiceOutput> CreateAsset(object Asset)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.CreateAsset);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, Asset);//GetAsync(uri);
        }

        public Task<ServiceOutput> EditAsset(object Asset)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.EditAsset);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, Asset);//GetAsync(uri);
        }

        public Task<ServiceOutput> AssetsByAssetSystemID(string AssetSystemID, string PageNumber, string RowsPage, string AssetSystemNumber,string Search)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.AssetsByAssetSystem);
            builder.AppendToPath(AssetSystemID);
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowsPage);
            if (String.IsNullOrWhiteSpace(AssetSystemNumber))
            {
                builder.AppendToPath("null");
            }
            else
            {
                builder.AppendToPath(AssetSystemNumber);
            }
            if (String.IsNullOrWhiteSpace(Search))
            {
                builder.AppendToPath("null");
            }
            else
            {
                builder.AppendToPath(Search);
            }

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

    }
}
