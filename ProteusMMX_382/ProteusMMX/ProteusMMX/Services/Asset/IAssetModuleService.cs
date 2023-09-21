using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.Asset
{
    public interface IAssetModuleService
    {
        Task<ServiceOutput> GetAssets(string UserID, string PageNumber, string RowCount);

        Task<ServiceOutput> AssetsByAssetSystemID(string AssetSystemID, string PageNumber, string RowsPage, string AssetSystemNumber,string Search);

        Task<ServiceOutput> GetAssetsBYAssetID(string AssetID,int Userid);

        Task<ServiceOutput> GetCategory(string UserID, string SearchCategory);

        Task<ServiceOutput> GetVendor(string UserID, string PageNumber, string RowCount, string Vendor);

        Task<ServiceOutput> GetRuntimeUnit(string UserID, string RuntimeUnit);

        Task<ServiceOutput> GetAssetsFromSearchBar(string AssetNumber,string UserID);
        Task<ServiceOutput> GetAssetsFromSearchBarFromBarcode(object AssetToSearch);


        Task<ServiceOutput> CreateAsset(object Asset);

        Task<ServiceOutput> EditAsset(object Asset);

    }
}
