using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.ServiceRequestModel
{
    public class ServiceRequests
    {
        public int? ServiceRequestID { get; set; }

        public DateTime? ReportedDate { get; set; }
        public int? FacilityID { get; set; }
        public int? AssetID { get; set; }
        public int? LocationID { get; set; }
        public int? CustomerLocationID { get; set; }
        public int? PriorityID { get; set; }
        public int? WorkOrderStatusID { get; set; }
        public int? MaintenanceCodeID { get; set; }
        public string MaintenanceCodeName { get; set; }
        public int? WorkTypeID { get; set; }
        public int? CostCenterID { get; set; }
        public int? ShiftID { get; set; }
        public int? WorkOrderRequesterID { get; set; }
        public int? AssignedToEmployeeID { get; set; }
        public int? AdministratorID { get; set; }
        public string AdministratorName { get; set; }
        public DateTime? RequestedDate { get; set; }
        public string Description { get; set; }
        public string RequestNumber { get; set; }
        public DateTime? RequiredDate { get; set; }
        public string AdditionalDetails { get; set; }
        public string EmailTo { get; set; }
        public string EmailCC { get; set; }
        public string EmailSubject { get; set; }
        public string EmailBody { get; set; }
        public string EstimatedDowntime { get; set; }
        public bool? AutoPrint { get; set; }
        public bool? AutoEmail { get; set; }
        public bool? AutoSendToMobile { get; set; }
        public DateTime? ModifiedTimestamp { get; set; }
        public string ModifiedUserName { get; set; }
        public int? AssetSystemID { get; set; }
        public int? MobileEmployeeID { get; set; }
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
        public string RequesterPhone { get; set; }
        public string TargetName { get; set; }
        public string PriorityName { get; set; }
        public string WorkOrderStatusName { get; set; }
        public string WorkTypeName { get; set; }
        public string ShiftName { get; set; }
        public List<ServiceRequestAttachment> attachments { get; set; }

        public string AssetName { get; set; }
        public string AssetSystemName { get; set; }
        public string LocationName { get; set; }

        public string CostCenterName { get; set; }
        public string EmployeeName { get; set; }
        public string WorkOrderRequesterName { get; set; }
        public string FacilityName { get; set; }

        public int UserId { get; set; }
        public string TagType { get; set; }
        public bool IsSignatureValidated { get; set; }
    }
}
