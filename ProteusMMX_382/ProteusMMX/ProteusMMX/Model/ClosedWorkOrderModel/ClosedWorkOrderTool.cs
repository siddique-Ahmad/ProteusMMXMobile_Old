using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.ClosedWorkOrderModel
{
    public class ClosedWorkOrderTool
    {
        public int? ClosedWorkOrderToolID { get; set; }
        public int? ClosedWorkOrderID { get; set; }
        public string ToolNumber { get; set; }
        public string ToolName { get; set; }
        public string ToolCribName { get; set; }
        public DateTime? ModifiedTimestamp { get; set; }
        public string ModifiedUserName { get; set; }

        public string ToolSize { get; set; }
    }
}
