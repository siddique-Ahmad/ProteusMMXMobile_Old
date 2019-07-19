using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.WorkorderStatus
{
    public interface IWorkorderStatusService
    {
        Task<ServiceOutput> GetWorkorderStatus(string UserID, string PageNumber, string RowCount, string SearchWorkorderStatusName);

    }
}
