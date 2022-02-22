using ProteusMMX.Helpers;
using ProteusMMX.Model.AssetModel;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Workorder;
using ProteusMMX.ViewModel.Workorder;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProteusMMX.Model.CommonModels
{
    public class TargetNavigationData
    {
        public int? FacilityID { get; set; }

        public int? ClosedWorkorderID { get; set; }

        public string PurchaseOrderNumber { get; set; }

        public string Startdate { get; set; }

        public string ShelfBin { get; set; }

        public string EndDate { get; set; }

        public string PartNumber { get; set; }

        public string SearchCriteria { get; set; }
        public int? LocationID { get; set; }
        public decimal? QuantityAllocated { get; set; }
        public int? ToolCribID { get; set; }
        public int? RequisitionID { get; set; }

        public int BalanceDue { get; set; }
        public int WorkOrderId { get; set; }

        public int? WORKORDERID { get; set; }

        public int WorkOrderNonStockroomPartID { get; set; }

        public int PurchaseOrderAssetID { get; set; }

        public int PurchaseOrderNonStockroomPartID { get; set; }

        public int PurchaseOrderPartID { get; set; }

        public int? ServiceRequestID { get; set; }
        public string RequestNumber { get; set; }

        public string Type { get; set; }
        public int WorkOrderStockroomPartID { get; set; }

        public int? StockroomID { get; set; }
        public string StockroomName { get; set; }
        public int? StockroomPartID { get; set; }

        public int AssetID { get; set; }

        public int? AssetSystemID { get; set; }

        public int? TaskID { get; set; }

        public int? WorkOrderLabourId { get; set; }

        public string HrsText { get; set; }

        public string SearchText { get; set; }

        public string FacilityName { get; set; }

        public string LocationName { get; set; }

        public string AssetName { get; set; }

        public string Description { get; set; }
        public string AssetSystemName { get; set; }

        public string AssetSystemNumber { get; set; }

        public string CurrentRuntime { get; set; }
        public bool IsLocationCallFrombarcodePage { get; set; }

        public bool IsAssetCallFrombarcodePage { get; set; }

        public bool IsLoginCallfromRiskPage { get; set; }

        public List<ShelfBin> lstShelfBin { get; set; }

        public WorkorderListingPageViewModel ViewModel { get; set; }

        public  IWorkorderService WorkorderService { get; set; }

        public List<SignatureAuditDetail> SignatureAuditDetails { get; set; }



    }
}
