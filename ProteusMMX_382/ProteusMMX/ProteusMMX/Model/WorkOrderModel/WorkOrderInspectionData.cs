using System;
using System.Collections.Generic;
using System.Text;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class WorkOrderInspectionData
    {
        public string EmployeeName { get; set; }
        public string ContractorName { get; set; }
        public string InspectionTime { get; set; }
        public string SectionName { get; set; }
        public string InspectionType { get; set; }
        public string EstimatedHours { get; set; }

        public int SectionID { get; set; }
        public List<WorkOrderSectionData> sectiondata { get; set; }
    }
}
