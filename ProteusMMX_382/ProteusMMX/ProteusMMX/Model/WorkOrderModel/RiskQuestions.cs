using System;
using System.Collections.Generic;
using System.Text;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class RiskQuestions
    {
        public int? InspectionID { get; set; }
        public string AnswerDescription { get; set; }
        public string ModifiedUserName { get; set; }
        public int? WorkOrderID { get; set; }
        public string SignatureString { get; set; }
        public string QuestionDescription { get; set; }
    }
}
