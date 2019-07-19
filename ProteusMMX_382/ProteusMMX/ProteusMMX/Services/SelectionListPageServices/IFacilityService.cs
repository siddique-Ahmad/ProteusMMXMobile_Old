using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices
{
    public interface IFacilityService
    {
        Task<ServiceOutput> GetFacilities(string UserID, string PageNumber, string RowCount, string SearchFacilityName);

    }
}
