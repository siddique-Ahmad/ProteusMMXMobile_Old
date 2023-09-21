using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.Workorder.TaskAndLabour
{
    public interface ITaskAndLabourService
    {
        Task<ServiceOutput> WorkOrderLaborsByWorkOrderIDAndTaskNumber(string UserID, string WorkorderID , string TaskID);
        Task<ServiceOutput> WorkOrderLaborsByWorkOrderID(string UserID, string WorkorderID);
        
        Task<ServiceOutput> UpdateTaskAndLabour(object workorder);

        Task<ServiceOutput> CreateWorkOrderLaborHours(object workorder);
        Task<ServiceOutput> CreateWorkOrderLabor(object workorder);

        Task<ServiceOutput> ClosedWorkOrdersLaborByClosedWorkorderID(string CLOSEDWORKORDERID, string UserID);
        
    }
}
