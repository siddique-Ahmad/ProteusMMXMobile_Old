using System;
using System.Collections.Generic;
using System.Text;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class WorkorderDD
    {
        public int? LocationID { get; set; }
        public int? ShiftID { get; set; }
        public int? PriorityID { get; set; }
        public string LocationName { get; set; }
        public string ShiftName { get; set; }
        public string PriorityName { get; set; }
    }
}
