using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.Location
{
    public interface ILocationService
    {
        Task<ServiceOutput> GetLocations(string UserID, string PageNumber, string RowCount, int FacilityID ,string SearchLocation);

    }
}
