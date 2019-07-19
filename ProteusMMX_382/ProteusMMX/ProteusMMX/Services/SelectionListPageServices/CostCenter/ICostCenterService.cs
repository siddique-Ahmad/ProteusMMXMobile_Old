using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.CostCenter
{
    public interface ICostCenterService
    {
        Task<ServiceOutput> GetCostCenters(string UserID, string PageNumber, string RowCount, string SearchCostCenterName);

    }
}
