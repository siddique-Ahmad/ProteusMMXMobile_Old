using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model
{
    public class MobileRightExpression
    {

        public List<string> assetsRights { get; set; }
        public List<string> workOrderRights { get; set; }
        public List<string> closedWorkOrderRights { get; set; }
        public List<string> serviceRequestRights { get; set; }
        public List<string> receivingRights { get; set; }
        public List<string> stockroomsRights { get; set; }
    }
}
