using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.AssignTo
{
    public interface IAssignToService
    {
        Task<ServiceOutput> GetAssignToEmployeeFacilityWise(string UserID, string PageNumber, string RowCount, string SearchAssignToEmployeeName);

    }
}
