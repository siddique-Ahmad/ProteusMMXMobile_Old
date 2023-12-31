﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class WorkOrder
    {
        public int WorkOrderID { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public string WorkOrderNumber { get; set; }
        public int? WorkOrderMasterID { get; set; }
        public int? ScheduleID { get; set; }
        public int? AssetID { get; set; }

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
        public string AssetSystemName { get; set; }
        public string LocationName { get; set; }

        public string CostCenterName { get; set; }
        public string EmployeeName { get; set; }
        public string WorkOrderRequesterName { get; set; }
        public string FacilityName { get; set; }

        public bool IsRiskQuestions { get; set; }

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
        public string WorkOrderApproved { get; set; }
        public bool WorkOrderApprovedFlag
        {
            get
            {
                if (this.WorkOrderApproved != null)
                {
                    return true;
                }

                return false;
            }
        }
    }
}
