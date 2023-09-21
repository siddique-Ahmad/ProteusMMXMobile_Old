using System;
using System.Collections.Generic;
using System.Text;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class WorkOrderSectionData
    {
        public string TypeName { get; set; }
        public string AnswerType { get; set; }
        public string ResponseType { get; set; }
        public string InspectionDescription { get; set; }
        public string AnswerDescription { get; set; }
        public int InspectionID { get; set; }
        public int SectionID { get; set; }
        public string ContractorName { get; set; }
        public string EmployeeName { get; set; }
        public decimal? MinRange { get; set; }
        public decimal? MaxRange { get; set; }
        public string EstimatedHours { get; set; }
    }
}
