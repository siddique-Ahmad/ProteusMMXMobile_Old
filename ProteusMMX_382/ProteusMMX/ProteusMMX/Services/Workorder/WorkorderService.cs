using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Services.Request;

namespace ProteusMMX.Services.Workorder
{
    public class WorkorderService : IWorkorderService
    {
        public StockroomPartsSearch PartToSearch { get; set; }
        private readonly IRequestService _requestService;
        public WorkorderService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Task<ServiceOutput> GetWorkorderByWorkorderID(string UserID, string WorkorderID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkorder);
            builder.AppendToPath(WorkorderID);
            builder.AppendToPath(UserID);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        public Task<ServiceOutput> GetWorkorderDDRecord(string UserID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkorderDDRecord);
            builder.AppendToPath(UserID);


            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> GetWorkOrderKPIDetails(int EmpID, int? PriorityID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetKPIRecord);
            builder.AppendToPath(EmpID.ToString());
            if (PriorityID == null)
                builder.AppendToPath("null");
            else
                builder.AppendToPath(PriorityID.ToString());
           


            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        

     
        public Task<ServiceOutput> GetWorkorders(string UserID, string PageNumber, string RowCount, string WorkorderNumber, string WorkorderType, string ActivationDateSortingType, string LocationSearch, string ShiftSearch, string PrioritySearch, string SortByDueDate, string KPIType)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkorders);
            builder.AppendToPath(UserID);
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);

            if(WorkorderNumber == null)
                builder.AppendToPath("null");
            else
                builder.AppendToPath(WorkorderNumber);

         

            if (WorkorderType == null)
                builder.AppendToPath("null");
            else
                builder.AppendToPath(WorkorderType);

            if (ActivationDateSortingType == null)
                builder.AppendToPath("null");
            else
                builder.AppendToPath(ActivationDateSortingType);

            if (LocationSearch == null)
                builder.AppendToPath("null");
            else
                builder.AppendToPath(LocationSearch);

            if (ShiftSearch == null)
                builder.AppendToPath("null");
            else
                builder.AppendToPath(ShiftSearch);

            if (PrioritySearch == null)
                builder.AppendToPath("null");
            else
                builder.AppendToPath(PrioritySearch);

            if (SortByDueDate == null)
                builder.AppendToPath("null");
            else
                builder.AppendToPath(SortByDueDate);

            if (KPIType == null)
                builder.AppendToPath("null");
            else
                builder.AppendToPath(KPIType);


            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> GetWorkorderLabour(string UserID, string WorkorderID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkorderLabour);
            builder.AppendToPath(WorkorderID);
            builder.AppendToPath(UserID);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> GetWorkorderInspection(string WorkorderID,string userid)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkorderInspection);
            builder.AppendToPath(WorkorderID);
            builder.AppendToPath(userid);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }


        public Task<ServiceOutput> GetWorkorderInspectionTime(string UserID, string WorkorderID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkorderInspectionTime);
            builder.AppendToPath(WorkorderID);
            builder.AppendToPath(UserID);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> UpdateWorkorder(object workorder)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.UpdateWorkOrder);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, workorder);//GetAsync(uri);
        }

        public Task<ServiceOutput> SaveWorkOrderAcknowledgement(object workorder)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.SaveWorkOrderAcknowledgement);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, workorder);//GetAsync(uri);
        }
        

        public Task<ServiceOutput> IsSignatureRequiredOnInspection(string WorkorderID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetIsWorkOrderSignatureRequired);
            builder.AppendToPath(WorkorderID);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> CloseWorkorder(object workorder)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.CloseWorkOrder);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, workorder);//GetAsync(uri);
        }

        public Task<ServiceOutput> CreateWorkorder(object workorder)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.CreateWorkOrder);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, workorder);//GetAsync(uri);
        }

        public Task<ServiceOutput> GetEmployeeAssignTo(string UserID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetEmployeeAssignTo);
            builder.AppendToPath(UserID);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> GetWorkorderStockroomParts(int Workorderid,string SearchValue)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkorderStockroomParts);
            //builder.AppendToPath(Workorderid);
            var uri = builder.Uri.AbsoluteUri;
            PartToSearch = new StockroomPartsSearch();
            PartToSearch.WorkorderID = Workorderid;
            if(string.IsNullOrWhiteSpace(SearchValue))
            {
                PartToSearch.PartNumber = "null";
            }
            else
            {
                PartToSearch.PartNumber = SearchValue;
            }
           
            return _requestService.PostAsync(uri, PartToSearch);
        }



        public Task<ServiceOutput> GetWorkorderTools(string Workorderid,string SearchText)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkorderTools);
            builder.AppendToPath(Workorderid);
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                builder.AppendToPath("null");
            }
            else
            {
                builder.AppendToPath(SearchText);
            }
            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> GetWorkorderNonStockroomParts(int Workorderid,string SearchText)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkorderNonStockroomParts);
            builder.AppendToPath(Workorderid.ToString());
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                builder.AppendToPath("null");
            }
            else
            {
                builder.AppendToPath(SearchText);
            }
            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> CreateWorkorderNonStockroomParts(object workorder)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.CreateWorkorderNonStockroomParts);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, workorder);//GetAsync(uri);
        }
        public Task<ServiceOutput> RemoveTool(object workorder)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.RemoveTool);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, workorder);//GetAsync(uri);
        }
       
        public Task<ServiceOutput> GetWorkorderNonStockroomPartsDetail(string WorkOrderID, string workordernonstockroompartid)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkorderNonStockroomPartDetail);
            builder.AppendToPath(WorkOrderID);
            builder.AppendToPath(workordernonstockroompartid);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> EditWorkorderNonStockroomParts(object workorder)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.EditWorkorderNonStockroomParts);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, workorder);//GetAsync(uri);
        }

        public Task<ServiceOutput> CreateWorkorderStockroomParts(object workorder)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.CreateWorkorderStockroomParts);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, workorder);//GetAsync(uri);
        }

        public Task<ServiceOutput> GetWorkorderStockroomPartsDetail(string WorkOrderID, string WORKORDERSTOCKROOMPARTID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkorderStockroomPartDetail);
            builder.AppendToPath(WorkOrderID);
            builder.AppendToPath(WORKORDERSTOCKROOMPARTID);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> EditWorkorderStockroomParts(object workorder)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.EditWorkorderStockroomParts);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, workorder);//GetAsync(uri);
        }

        public Task<ServiceOutput> CheckDuplicatePart(int Workorderid, string stkroomid, string PartNumber, string StockroomName)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.CheckDuplicateParts);
            //builder.AppendToPath(Workorderid);
            var uri = builder.Uri.AbsoluteUri;
            PartToSearch = new StockroomPartsSearch();
            PartToSearch.WorkorderID = Workorderid;
            PartToSearch.StockroomID = stkroomid;
            PartToSearch.PartNumber = PartNumber;
            PartToSearch.StockRoomName = "null";
            return _requestService.PostAsync(uri, PartToSearch);
        }

        public Task<ServiceOutput> GetStockroomPartDetailFromScan(string stkroomid, string PartNumber)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetStockroomPartDetailFromScan);
            var uri = builder.Uri.AbsoluteUri;
            PartToSearch = new StockroomPartsSearch();
           
            PartToSearch.StockroomID = stkroomid;
            PartToSearch.PartNumber = PartNumber;
           
            return _requestService.PostAsync(uri, PartToSearch);
        }
        public Task<ServiceOutput> GetWorkordersFromAsset(string AssetNumber, string UserID, int PageNumber, int RowCount)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkordersFromAsset);
            var uri = builder.Uri.AbsoluteUri;
            PartToSearch = new StockroomPartsSearch();

            PartToSearch.AssetNumber = AssetNumber;
            PartToSearch.UserID =Convert.ToInt32(UserID);
            PartToSearch.PageNumber = PageNumber;
            PartToSearch.RowspPage = RowCount;

            return _requestService.PostAsync(uri, PartToSearch);
        }

        public Task<ServiceOutput> GetWorkorderControlRights(string UserID,string PARENTCONTROL,string CHILDCONTROL)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkorderControlRights);
            builder.AppendToPath(UserID);
            builder.AppendToPath(PARENTCONTROL);
            builder.AppendToPath(CHILDCONTROL);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        

    }
}
