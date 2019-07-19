using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.Asset
{
    public interface IAssetService
    {
        Task<ServiceOutput> GetAssetsByFacilityAndLocation(string UserID, string PageNumber, string RowCount, int? FacilityID , int? LocationID, string SearchAssetName);
        Task<ServiceOutput> GetAssetsByFacility(string UserID, string PageNumber, string RowCount, int? FacilityID , string SearchAssetName);

    }
}
