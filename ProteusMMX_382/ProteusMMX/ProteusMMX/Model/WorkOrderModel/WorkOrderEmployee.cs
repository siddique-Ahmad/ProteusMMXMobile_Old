using System;
using System.Collections.Generic;
using System.Text;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class WorkOrderEmployee
    {
        public int? WorkOrderInspectionTimeID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string InspectionTime { get; set; }
        public string EmployeeName { get; set; }
        public string LaborCraftCode { get; set; }
        public int? EmployeeLaborCraftID { get; set; }


        #region For GlobalTimer
        public DateTime StartTimeOfTimer { get; set; }

        #endregion
    }
}
