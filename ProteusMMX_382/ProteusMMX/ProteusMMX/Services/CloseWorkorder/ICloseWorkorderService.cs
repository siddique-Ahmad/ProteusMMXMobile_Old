using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.CloseWorkorder
{
    public interface ICloseWorkorderService
    {
        Task<ServiceOutput> GetClosedWorkOrdersByAssetNumber(object obj);

        Task<ServiceOutput> ClosedWorkOrdersByClosedWorkOrderDate(string UserID, string startDate, string endDate, string PageNumber, string RowspPage);

        Task<ServiceOutput> ClosedWorkOrdersByPartNumber(object obj);
        Task<ServiceOutput> ClosedWorkOrdersByWorkOrderNumber(string workorderNumber, string PageNumber, string RowspPage);
        Task<ServiceOutput> ClosedWorkOrdersByLocation(string locationName, string PageNumber, string RowspPage);
        Task<ServiceOutput> ClosedWorkOrders(string closedWorkorderNumber, string UserID);

        Task<ServiceOutput> GetClosedWorkOrdersToolsByClosedWorkorderID(string CLOSEDWORKORDERID);

        Task<ServiceOutput> GetClosedWorkOrdersStockroomPartsByClosedWorkorderID(string CLOSEDWORKORDERID);

        Task<ServiceOutput> GetClosedWorkOrdersNonStockroomPartsByClosedWorkorderID(string CLOSEDWORKORDERID);

    }
}
