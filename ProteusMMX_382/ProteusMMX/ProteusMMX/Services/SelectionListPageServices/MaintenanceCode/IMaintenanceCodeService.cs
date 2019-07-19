using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.MaintenanceCode
{
    public interface IMaintenanceCodeService
    {
        Task<ServiceOutput> GetMaintenanceCode(string PageNumber, string RowCount, string SearchMaintenanceCode);

    }
}
