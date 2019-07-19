using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.Tool
{
    public interface IToolService
    {
        Task<ServiceOutput> GetToolCrib(string UserID, string PageNumber, string RowCount, string SEARCHTOOLCRIB);

        Task<ServiceOutput> GetToolNumber(string workorderid, string UserID, string TOOLCRIBID, string PageNumber, string RowCount, string SEARCHTOOLS);

        Task<ServiceOutput> CreateWorkOrderTool(object tool);

        Task<ServiceOutput> CheckDupliacateTool(string workorderid, string TOOLNUMBER);

        Task<ServiceOutput> GetToolNumberDetailFromScan(string workorderid, string UserID, string TOOLCRIBID, string PageNumber, string RowCount, string SEARCHTOOLS);
        

    }
}
