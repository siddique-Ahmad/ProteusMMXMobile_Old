using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.ClosedWorkOrderModel
{
    public class ClosedWorkOrderLabor
    {
        public int? ClosedWorkOrderLaborID { get; set; }
        public int? ClosedWorkOrderID { get; set; }
        public string TaskNumber { get; set; }
        public string Description { get; set; }
        public decimal? EstimatedHours { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string ContractorCode { get; set; }
        public string ContractorName { get; set; }
        public string LaborCraftCode { get; set; }
        public decimal? HoursAtRate1 { get; set; }
        public decimal? HoursAtRate2 { get; set; }
        public decimal? HoursAtRate3 { get; set; }
        public decimal? HoursAtRate4 { get; set; }
        public decimal? HoursAtRate5 { get; set; }
        public int? Sequence { get; set; }
        public DateTime? ModifiedTimestamp { get; set; }
        public string ModifiedUserName { get; set; }
        public int? LaborCraftRate1ID { get; set; }
        public int? LaborCraftRate2ID { get; set; }
        public int? LaborCraftRate3ID { get; set; }
        public int? LaborCraftRate4ID { get; set; }
        public int? LaborCraftRate5ID { get; set; }
    }
}
