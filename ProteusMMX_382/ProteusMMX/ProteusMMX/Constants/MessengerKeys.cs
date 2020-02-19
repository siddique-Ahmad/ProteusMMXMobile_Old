using System;
using System.Collections.Generic;
using System.Text;

namespace ProteusMMX.Constants
{
    public class MessengerKeys
    {
        // Target keys
        public const string FacilityRequested = "FacilityRequested";
        public const string ReceiverRequested = "ReceiverRequested";
        public const string LocationRequested = "LocationRequested";
        public const string AssetRequested = "AssetRequested";
        public const string AssetSyastemRequested = "AssetSyastemRequested";
        public const string AssignToRequested = "AssignToRequested";
        public const string WorkorderRequesterRequested = "WorkorderRequesterRequested";
        public const string CostCenterRequested = "CostCenterRequested";
        public const string PriorityRequested = "PriorityRequested";
        public const string ShiftRequested = "ShiftRequested";
        public const string WorkorderStatusRequested = "WorkorderStatusRequested";
        public const string WorkorderTypeRequested = "WorkorderTypeRequested";
        public const string CauseRequested = "CauseRequested";
        public const string MaintenanceCodeRequested = "MaintenanceCodeRequested";
        public const string OnCategoryRequested = "OnCategoryRequested";
        public const string OnVendorRequested = "OnVendorRequested";
        public const string OnRuntimeUnitRequested = "OnRuntimeUnitRequested";


        // Workorder >>> Task and Labour
        public const string TaskRequested = "TaskRequested";
        public const string EmployeeRequested_AddTask = "EmployeeRequested_AddTask";
        public const string ContractorRequested_AddTask = "ContractorRequested_AddTask";

        // Workorder >>> Tools
        public const string ToolCribRequested = "ToolCribRequested";
        public const string ToolNumberRequested = "ToolNumberRequested";

        // Workorder >>> Parts
        public const string PartRequested = "PartRequested";
        public const string StockoomRequested = "StockoomRequested";
        public const string ShelfBinRequested = "ShelfBinRequested";
        public const string CostcenterRequested = "CostcenterRequested";
        public const string TransactionReasonRequested = "TransactionReasonRequested";
        public const string TransactionTypeRequested = "TransactionTypeRequested";
        public const string OnPerformBYRequested = "OnPerformBYRequested";
        public const string OnCheckoutRequested = "OnCheckoutRequested";


        // Workorder >>> Inspection
        
        public const string EmployeeRequested_AddInspection = "EmployeeRequested_AddInspection";
        public const string ContractorRequested_AddInspection = "ContractorRequested_AddInspection";


    }


}
