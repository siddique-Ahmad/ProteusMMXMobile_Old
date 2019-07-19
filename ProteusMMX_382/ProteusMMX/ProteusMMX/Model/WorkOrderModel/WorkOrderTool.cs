using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class WorkOrderTool
    {
        public int? WorkOrderToolID { get; set; }
        public int? WorkOrderID { get; set; }
        public int? ToolID { get; set; }
        public DateTime? ModifiedTimestamp { get; set; }
        public string ModifiedUserName { get; set; }
        public string ToolNumber { get; set; }
        public string ToolName { get; set; }
        public string ToolCribName { get; set; }
        public string ToolSize { get; set; }
    }
}
