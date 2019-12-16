using System;
using System.Collections.Generic;
using System.Text;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class ListEmployeeContractor
    {
        public DateTime? StartDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public string InspectionTime { get; set; }
        public string EmployeeName { get; set; }

        public int? EmployeeLaborCraftID { get; set; }

        public string ContractorName { get; set; }

        public int? ContractorLaborCraftID { get; set; }
    }
}
