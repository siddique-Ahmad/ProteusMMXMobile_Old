using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class workOrders
    {
        public int WorkOrderID { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public string WorkOrderNumber { get; set; }
        public int? WorkOrderMasterID { get; set; }
        public int? ScheduleID { get; set; }
        public int? AssetID { get; set; }

        public string Originator { get; set; }
        public string JobNumber { get; set; }
        public int? CustomerLocationID { get; set; }
        public int? LocationID { get; set; }
        public int? StockroomPartID { get; set; }
        public int? PriorityID { get; set; }
        public int? WorkOrderStatusID { get; set; }
        public int? MaintenanceCodeID { get; set; }
        public int? WorkTypeID { get; set; }
        public int? CostCenterID { get; set; }
        public int? ShiftID { get; set; }
        public int? WorkOrderRequesterID { get; set; }
        public int? AssignedToEmployeeID { get; set; }
        public string RequestNumber { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public DateTime? ActivationDate { get; set; }
        public DateTime? WorkStartedDate { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public DateTime? RequiredDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string AdditionalDetails { get; set; }

        public string InternalNote { get; set; }
        public DateTime? LastPrintDate { get; set; }
        public string EmailTo { get; set; }
        public string EmailCC { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public bool? AutoPrint { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public bool? AutoEmail { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public bool? AutoSendToMobile { get; set; }
        public int? KitsRequired { get; set; }
        public int? KitsCompleted { get; set; }
        public string EstimatedDowntime { get; set; }
        public string ActualDowntime { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public string MobileStatus { get; set; }
        public int? MobileEmployeeID { get; set; }
        public string WorkOrderType { get; set; }
        public DateTime ModifiedTimestamp { get; set; }
        public string ModifiedUserName { get; set; }
        public int? AssetSystemID { get; set; }
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
        public string PriorityName { get; set; }

        public string WorkOrderStatusName { get; set; }
        public string WorkTypeName { get; set; }
        public string ShiftName { get; set; }
        public int? FacilityID { get; set; }

        public string MaintenanceCodeName { get; set; }
        public string AssetName { get; set; }

        public string AssetNumber { get; set; }
        public string AssetSystemName { get; set; }

        public string AssetSystemNumber { get; set; }
        public string LocationName { get; set; }

        public string CostCenterName { get; set; }
        public string EmployeeName { get; set; }
        public string WorkOrderRequesterName { get; set; }
        public string FacilityName { get; set; }

        public bool IsRiskQuestion { get; set; }

        public string RiskQuestion { get; set; }

        public bool WorkOrderCompleted
        {
            get
            {
                if (this.CompletionDate != null)
                {
                    return true;
                }

                return false;
            }
        }
        //public string WorkOrderHasAttachmentwithCount { get; set; }
        //public bool WorkOrderHasAttachment
        //{
        //    get
        //    {
        //        if (this.WorkOrderHasAttachmentwithCount != null)
        //        {
        //            return true;
        //        }

        //        return false;
        //    }
        //}

        //public string IsWorkOrderHasFailedInspection { get; set; }
        //public bool WorkOrderHasFailedInspection
        //{
        //    get
        //    {
        //        if (this.IsWorkOrderHasFailedInspection != null)
        //        {
        //            return true;
        //        }

        //        return false;
        //    }
        //}
        public string WorkOrderHasAttachment { get; set; }
        public bool WorkOrderAttachmentFlag
        {
            get
            {
                if (this.WorkOrderHasAttachment == "True")
                {
                    return true;
                }

                return false;
            }
        }
        public string IsWorkOrderHasFailedInspection { get; set; }
        public bool WorkOrderHasFailedInspection
        {
            get
            {
                if (this.IsWorkOrderHasFailedInspection == "True")
                {
                    return true;
                }

                return false;
            }
        }


        public int? WorkOrderAttachmentCount { get; set; }

       
        public string WorkOrderApproved { get; set; }
        public bool WorkOrderApprovedFlag
        {
            get
            {
                if (this.WorkOrderApproved == "true")
                {
                    return true;
                }

                return false;
            }
        }

        public bool AssetNameFlag
        {
            get
            {
                if (this.AssetName != null)
                {
                    return true;
                }

                return false;
            }
        }

        public bool AssetSystemNameFlag
        {
            get
            {
                if (this.AssetSystemName != null)
                {
                    return true;
                }

                return false;
            }
        }
        public bool AssetSystemFlag
        {
            get
            {
                if (this.AssetSystemName != null)
                {
                    return true;
                }
                else if (this.AssetName != null)
                {
                    return true;
                }

                return false;
            }
        }

        public bool PriorityNameIsVisible
        {
            get
            {
                if (this.PriorityName != null)
                {
                    return true;
                }

                return false;
            }
        }
        public string ApprovalLevel { get; set; }
        public string ApprovalNumber { get; set; }
        public string SignatureIntent { get; set; }
        public string Signature { get; set; }
        public DateTime? SignatureTimestamp { get; set; }
        public string LOTOUrl { get; set; }

        public bool IsSignatureValidated { get; set; }

        public bool? DistributeCost { get; set; }

        public bool? ParentandChildCost { get; set; }
        public bool? ChildCost { get; set; }

        #region New Properties 31.5.2018

        public string AbnormalityID { get; set; }
        public string AmStepID { get; set; }
        public string AnalysisPerformedDate { get; set; }
        public string ClosedDate { get; set; }
        public string CountermeasuresDefinedDate { get; set; }
        public string CurrentRuntime { get; set; }
        public string DiagnosticTime { get; set; }
        public string ImplementationValidatedDate { get; set; }
        public string InitialWaitTime { get; set; }
        public string NotificationTime { get; set; }
        public string PartWaitingTime { get; set; }
        public string ProblemID { get; set; }
        public string RelatedToID { get; set; }
        public string RepairingOrReplacementTime { get; set; }
        public string ServiceRequestModeID { get; set; }
        public string StartupTime { get; set; }
        public string TotalTime { get; set; }
        public string UnsafeConditionID { get; set; }


        #endregion
    }
}
