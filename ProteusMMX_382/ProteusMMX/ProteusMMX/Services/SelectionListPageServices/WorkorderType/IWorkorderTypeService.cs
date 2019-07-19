using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.WorkorderType
{
    public interface IWorkorderTypeService
    {
        Task<ServiceOutput> GetWorkorderType(string UserID, string PageNumber, string RowCount, string SearchWorkorderType);

    }
}
