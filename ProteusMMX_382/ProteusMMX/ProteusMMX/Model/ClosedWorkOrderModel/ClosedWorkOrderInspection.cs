using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.ClosedWorkOrderModel
{
    public class ClosedWorkOrderInspection
    {
        public int? ClosedWorkOrderInspectionID { get; set; }
        public int? ClosedWorkOrderID { get; set; }
        public string SectionName { get; set; }
        public string InspectionDescription { get; set; }
        public string AnswerDescription { get; set; }
        public string ResponseType { get; set; }
        public decimal? MinRange { get; set; }
        public decimal? MaxRange { get; set; }
        public bool? SignatureRequired { get; set; }
        public DateTime? ModifiedTimeStamp { get; set; }
        public string ModifiedUserName { get; set; }

        public bool IsAdded { get; set; }

        public string InspectionTimeInSeconds { get; set; }

        public string ContractorName { get; set; }
        public string EmployeeName { get; set; }

        public string SignaturePath { get; set; }
        public string SignatureString { get; set; }

        public DateTime? InspectionStartDate { get; set; }
        public DateTime? InspectionCompletionDate { get; set; }

        public string EstimatedHours { get; set; }
        public string LaborCraftCode { get; set; }

        public Nullable<bool> ResponseSubType { get; set; }
    }
}
