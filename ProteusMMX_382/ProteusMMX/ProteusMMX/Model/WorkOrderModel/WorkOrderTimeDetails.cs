using System;
using System.Collections.Generic;
using System.Text;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class WorkOrderTimeDetails
    {
        public string OverdueWorkOrder { get; set; }
        public string TodayOpenWorkOrder { get; set; }
        public string WeeklyWorkOrder { get; set; }
        public string TodayHours { get; set; }
        public string WeeklyHours { get; set; }
        public string MonthlyHours { get; set; }
    }
}
