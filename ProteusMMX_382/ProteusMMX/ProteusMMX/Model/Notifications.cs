using System;
using System.Collections.Generic;
using System.Text;

namespace ProteusMMX.Model
{
    public class Notifications
    {
        public int? ID { get; set; }
        public int? UserId { get; set; }
        public int? WorkOrderId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public bool Status { get; set; }
    }
}
