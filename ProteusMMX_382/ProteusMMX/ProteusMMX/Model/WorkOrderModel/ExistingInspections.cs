using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class ExistingInspections
    {
        public int InspectionID { get; set; }
        public Nullable<int> SectionID { get; set; }
        public string SectionName { get; set; }
        public string InspectionDescription { get; set; }
        public string ResponseTypeName { get; set; }
        public int ResponseTypeID { get; set; }
        public Nullable<decimal> MinRange { get; set; }
        public Nullable<decimal> MaxRange { get; set; }
        public string Option1 { get; set; }
        public string Option3 { get; set; }
        public string Option4 { get; set; }
        public string Option2 { get; set; }
        public string Option5 { get; set; }
        public string Option6 { get; set; }
        public string Option7 { get; set; }
        public string Option8 { get; set; }
        public string Option9 { get; set; }
        public string Option10 { get; set; }
        public Nullable<bool> IsGroupInspection { get; set; }
        public Nullable<System.DateTime> ModifiedTimeStamp { get; set; }
        public string ModifiedUserName { get; set; }
        public Nullable<bool> SignatureRequired { get; set; }

        public string AnswerDescription { get; set; }

        public bool IsAdded { get; set; }

        public string SignaturePath { get; set; }
        public string SignatureString { get; set; }

        public string EstimatedHours { get; set; }

        public Nullable<bool> ResponseSubType { get; set; }
    }
}
