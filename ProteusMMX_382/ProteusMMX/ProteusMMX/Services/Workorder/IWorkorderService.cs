using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.Workorder
{
    public interface IWorkorderService
    {
        Task<ServiceOutput> GetWorkorders(string UserID, string PageNumber , string RowCount , string WorkorderNumber , string WorkorderType , string ActivationDateSortingType, string LocationSearch, string ShiftSearch, string PrioritySearch, string SortByDueDate, string KPIType);

       

        

        Task<ServiceOutput> GetWorkordersFromAsset(string AssetNumber, string UserID, int PageNumber, int RowCount);

        Task<ServiceOutput> GetWorkorderControlRights(string UserID, string PARENTCONTROL, string CHILDCONTROL);
        Task<ServiceOutput> GetWorkorderDDRecord(string UserID);

        Task<ServiceOutput> GetWorkOrderKPIDetails(int EmpId,int? PriorityID);

        Task<ServiceOutput> GetWorkorderByWorkorderID(string UserID, string WorkorderID);
        Task<ServiceOutput> GetWorkorderLabour(string UserID, string WorkorderID);
        Task<ServiceOutput> GetWorkorderInspection(string WorkorderID,string Userid);
        Task<ServiceOutput> GetWorkorderInspectionTime(string UserID, string WorkorderID);
        Task<ServiceOutput> CreateWorkorder(object workorder);
        Task<ServiceOutput> UpdateWorkorder(object workorder);

        Task<ServiceOutput> SaveWorkOrderAcknowledgement(object workorder);
        Task<ServiceOutput> IsSignatureRequiredOnInspection(string WorkorderID);
        Task<ServiceOutput> CloseWorkorder(object workorder);

        Task<ServiceOutput> GetEmployeeAssignTo(string UserID);

        Task<ServiceOutput> GetWorkorderStockroomParts(int Workorderid,string SearchValue);

        Task<ServiceOutput> RemoveTool(object workorder);

        Task<ServiceOutput> GetWorkorderTools(string Workorderid,string SearchText);

        Task<ServiceOutput> GetWorkorderNonStockroomParts(int Workorderid, string SearchText);

        Task<ServiceOutput> CreateWorkorderNonStockroomParts(object workorder);

        Task<ServiceOutput> GetWorkorderNonStockroomPartsDetail(string WORKORDERID, string WORKORDERNONSTOCKROOMPARTID);

        Task<ServiceOutput> EditWorkorderNonStockroomParts(object workorder);

        Task<ServiceOutput> CreateWorkorderStockroomParts(object workorder);

        Task<ServiceOutput> GetWorkorderStockroomPartsDetail(string WORKORDERID, string WORKORDERSTOCKROOMPARTID);

        Task<ServiceOutput> EditWorkorderStockroomParts(object workorder);

        Task<ServiceOutput> CheckDuplicatePart(int Workorderid,string stkroomid,string PartNumber,string StockroomName);

        Task<ServiceOutput> GetStockroomPartDetailFromScan(string stkroomid, string PartNumber);

        
    }

}
