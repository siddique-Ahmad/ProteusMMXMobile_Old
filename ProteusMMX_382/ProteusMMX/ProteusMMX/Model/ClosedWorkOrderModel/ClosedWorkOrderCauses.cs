using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.ClosedWorkOrderModel
{
    public class ClosedWorkOrderCauses
    {
        public int ClosedWorkOrderCauseID { get; set; }
        public int ClosedWorkOrderID { get; set; }
        public string CauseNumber { get; set; }
        public string CauseDescription { get; set; }
        public decimal Downtime { get; set; }
        public DateTime ModifiedTimestamp { get; set; }
        public string ModifiedUserName { get; set; }
    }
}
