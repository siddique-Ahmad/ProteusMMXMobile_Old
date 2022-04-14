using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.ClosedWorkOrderModel
{
    public class ClosedWorkOrder
    {
        public int? ClosedWorkOrderID { get; set; }
        public string WorkOrderNumber { get; set; }
        public string LocationName { get; set; }
        public string InternalNote { get; set; }
        public string JobNumber { get; set; }
        public string ScheduleType { get; set; }
        public int? AssetID { get; set; }
        public string AssetNumber { get; set; }
        public string AssetName { get; set; }
        public string PartNumber { get; set; }
        public string PartName { get; set; }
        public string StockroomName { get; set; }
        public string SerialNumber { get; set; }
        public decimal? InitialRuntime { get; set; }
        public string RuntimeUnits { get; set; }
        public string PriorityName { get; set; }
        public string WorkOrderStatusName { get; set; }
        public string MaintenanceCodeName { get; set; }
        public string WorkTypeName { get; set; }
        public string CostCenterName { get; set; }

        public string FacilityName { get; set; }
        public string ShiftName { get; set; }
        public string WorkOrderRequesterName { get; set; }
        public string AssignToEmployee { get; set; }
        public string RequestNumber { get; set; }
        public string Description { get; set; }

        public string Originator { get; set; }
        public DateTime? ActivationDate { get; set; }
        public DateTime? WorkStartedDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public DateTime? ClosedDate { get; set; }
        public string AdditionalDetails { get; set; }
        public DateTime? LastPrintDate { get; set; }
        public string EmailTo { get; set; }
        public string EmailCC { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public bool? AutoPrint { get; set; }
        public bool? AutoEmail { get; set; }
        public bool? AutoSendToMobile { get; set; }
        public int? KitsRequired { get; set; }
        public int? KitsCompleted { get; set; }
        public string WorkOrderType { get; set; }
        public string CustomerNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLocationName { get; set; }
      //  public string LocationName { get; set; }
        public DateTime? ModifiedTimestamp { get; set; }
        public string ModifiedUserName { get; set; }
        public string AssetSystemNumber { get; set; }
        public string AssetSystemName { get; set; }
        public string EstimatedDowntime { get; set; }
        public string ActualDowntime { get; set; }
        public int? MiscellaneousLaborCostID { get; set; }
        public int? MiscellaneousMaterialsCostID { get; set; }
        public decimal? MiscellaneousLaborCost { get; set; }
        public decimal? MiscellaneousMaterialsCost { get; set; }
        public string RequesterFullName { get; set; }
        public string UserField1 { get; set; }
        public string UserField2 { get; set; }
        public string UserField3 { get; set; }
        public string UserField4 { get; set; }
        public string UserField5 { get; set; }
        public string UserField6 { get; set; }
        public string UserField7 { get; set; }
        public string UserField8 { get; set; }
        public string UserField9 { get; set; }
        public string UserField10 { get; set; }
        public string UserField11 { get; set; }
        public string UserField12 { get; set; }
        public string UserField13 { get; set; }
        public string UserField14 { get; set; }
        public string UserField15 { get; set; }
        public string UserField16 { get; set; }
        public string UserField17 { get; set; }
        public string UserField18 { get; set; }
        public string UserField19 { get; set; }
        public string UserField20 { get; set; }
        public string UserField21 { get; set; }
        public string UserField22 { get; set; }
        public string UserField23 { get; set; }
        public string UserField24 { get; set; }
        public string RequesterEmail { get; set; }
        public string ConfirmEmail { get; set; }
        public DateTime? RequestedDate { get; set; }
        public string DigitalSignatures { get; set; }
        public string project { get; set; }
        public string RequesterPhone { get; set; }
        public string TargetName { get; set; }

        public string CurrentRuntime { get; set; }
        public string SectionName { get; set; }

        public string ApprovalLevel { get; set; }
        public string ApprovalNumber { get; set; }
        public string SignatureIntent { get; set; }
        public string Signature { get; set; }

        // public string TotalTime { get; set; }
        public bool? DistributeCost { get; set; }
        public string TotalTime { get; set; }
        public DateTime? SignatureTimestamp { get; set; }

        
        public bool? ParentandChildCost { get; set; }
        public bool? ChildCost { get; set; }
    }
}
