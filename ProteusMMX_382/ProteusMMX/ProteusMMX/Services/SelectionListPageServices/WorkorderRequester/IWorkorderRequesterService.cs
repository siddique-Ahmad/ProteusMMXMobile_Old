using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.WorkorderRequester
{
    public interface IWorkorderRequesterService
    {
        Task<ServiceOutput> GetWorkorderRequester(string UserID, string PageNumber, string RowCount, string SearchWorkorderRequester);
    }
}
