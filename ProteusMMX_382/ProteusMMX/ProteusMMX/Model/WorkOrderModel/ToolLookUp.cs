using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class ToolLookUp
    {
        public int? ToolID { get; set; }
        public string ToolNumber { get; set; }
        public string ToolName { get; set; }
        public string Description { get; set; }
        public string ToolCribName { get; set; }

        public int? ToolCribID { get; set; }

        public string ToolNumberLocal
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ToolName))
                {
                    return ToolNumber;
                }
                return ToolNumber + "(" + ToolName + ")";
            }
        }

    }
}
