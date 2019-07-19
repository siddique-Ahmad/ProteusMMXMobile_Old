using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.AssetSystem
{
    public interface IAssetSystemService
    {
        Task<ServiceOutput> GetAssetSystemByFacilityAndLocation(string UserID, string PageNumber, string RowCount, int? FacilityID, int? LocationID, string SearchAssetName);
        Task<ServiceOutput> GetAssetSystemByFacility(string UserID, string PageNumber, string RowCount, int? FacilityID, string SearchAssetName);

        Task<ServiceOutput> GetAssetSystemforAssetPage(string UserID, string PageNumber, string RowCount, string SearchAssetName);
    }
}
