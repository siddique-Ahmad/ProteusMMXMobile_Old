using System;
using System.Collections.Generic;
using System.Text;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class InspectionTOAnswers
    {
        public List<InspectionTOAnswers> inspectionToAnswers { get; set; }
        public string ClientIANATimeZone { get; set; }
        public long UserID { get; set; }

        public int? WorkOrderID { get; set; }
        public string InspectionTime { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string EmployeeLaborCraftid { get; set; }
        public string ContractorLaborCraftId { get; set; }
        public string ModifiedUserName { get; set; }
        public DateTime? ModifiedTimestamp { get; set; }

        public int? WorkOrderInspectionTimeID { get; set; }
        public int? WorkOrderInspectionDetailsID { get; set; }
        public string TimerStatus { get; set; }
    }
}
