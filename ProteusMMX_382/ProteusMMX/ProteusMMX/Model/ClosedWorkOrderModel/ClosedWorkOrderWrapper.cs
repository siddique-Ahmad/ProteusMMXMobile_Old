using ProteusMMX.Model.WorkOrderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.ClosedWorkOrderModel
{
    public class ClosedWorkOrderWrapper
    {
        public string _IsWorkOrderHasTaskORInspection { get; set; }
        public ClosedWorkOrder clworkOrder { get; set; }
        public List<ClosedWorkOrder> clworkOrders { get; set; }
        public List<SignatureAuditDetail> SignatureAuditDetails { get; set; }
        public List<Cause> Causes { get; set; }
        public ClosedWorkOrderLabor clworkOrderLabor { get; set; }
        public List<ClosedWorkOrderLabor> clworkOrderLabors { get; set; }
        public ClosedWorkOrderStockroomPart clworkOrderStockroomPart { get; set; }
        public List<ClosedWorkOrderStockroomPart> clworkOrderStockroomParts { get; set; }
        public ClosedWorkOrderNonStockroomParts clworkOrderNonStockroomPart { get; set; }
        public List<ClosedWorkOrderNonStockroomParts> clworkOrderNonStockroomParts { get; set; }
        public ClosedWorkOrderAttachment clattachment { get; set; }
        public List<ClosedWorkOrderAttachment> clattachments { get; set; }
        public ClosedWorkOrderTool cltool { get; set; }
        public List<ClosedWorkOrderTool> cltools { get; set; }

        public List<ClosedWorkOrderInspection> ClosedWorkOrderInspection { get; set; }
        
        public bool? IsInspectionUser { get; set; }
        public int CLosedOrderCount { get; set; }

       
    }
}
