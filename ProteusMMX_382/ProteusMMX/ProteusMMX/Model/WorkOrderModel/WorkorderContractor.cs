using System;
using System.Collections.Generic;
using System.Text;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class WorkorderContractor
    {
        public int? WorkOrderInspectionTimeID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string InspectionTime { get; set; }
        public string ContractorName { get; set; }

        public string LaborCraftCode { get; set; }

        public int? ContractorLaborCraftID { get; set; }
        public int? WorkOrderInspectionDetailsID { get; set; }
        public string TimerStatus { get; set; }
        public DateTime? InspectionCompletionDate { get; set; }
        public bool StartBtn { get; set; }
        public bool StopBtn { get; set; }

        public string Hours { get; set; }
        public string Minutes { get; set; }

        #region For GlobalTimer
        public DateTime StartTimeOfTimer { get; set; }

        #endregion

    }
}
