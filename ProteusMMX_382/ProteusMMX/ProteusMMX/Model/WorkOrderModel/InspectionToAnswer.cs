using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class InspectionToAnswer
    {
        public List<InspectionAnswer> InspectionAnswers { get; set; }
        public string SectionName { get; set; }
        public int SectionID { get; set; }
        public bool SignatureRequired { get; set; }
        public bool IsGroupInspection { get; set; }
        public int? WorkorderID { get; set; }
        public string ClientIANATimeZone { get; set; }
        public long UserID { get; set; }
        public string InspectionTimeInSeconds { get; set; }

        public Nullable<DateTime> InspectionStartDate { get; set; }
        public Nullable<DateTime> InspectionCompletionDate { get; set; }





    }
}
