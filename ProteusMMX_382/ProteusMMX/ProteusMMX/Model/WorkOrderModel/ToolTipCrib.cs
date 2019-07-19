using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class ToolTipCrib
    {
        public string ToolCribName { get; set; }
        public int? ToolCribID { get; set; }
        public List<ToolLookUp> ToolLookUps { get; set; }
    }
}
