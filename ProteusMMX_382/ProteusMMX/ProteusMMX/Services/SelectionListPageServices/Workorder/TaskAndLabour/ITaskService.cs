using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.Workorder.TaskAndLabour
{
    public interface ITaskService
    {
        Task<ServiceOutput> GetTask(string UserID, string PageNumber, string RowCount, string SearchTaskNumber);
        Task<ServiceOutput> GetEmployee(string UserID, string PageNumber, string RowCount, string SearchEmployeeName);
        Task<ServiceOutput> GetContractor(string UserID, string PageNumber, string RowCount, string SearchContractorName);

    }
}
