using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.Priority
{
    public interface IPriorityService
    {
        Task<ServiceOutput> GetPriorities(string PageNumber, string RowCount, string SearchPriorityName);

    }
}
