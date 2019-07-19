using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class InspectionAnswer
    {
        public int InspectionAnswerID { get; set; }
        public int InspectionID { get; set; }
        public string AnswerDescription { get; set; }
        public Nullable<System.DateTime> InspectionDate { get; set; }
        public Nullable<System.DateTime> ModifiedTimeStamp { get; set; }
        public string ModifiedUserName { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
        public Nullable<int> SubInspectionID { get; set; }

        public string SignaturePath { get; set; }
        public string SignatureString { get; set; }
    }
}
