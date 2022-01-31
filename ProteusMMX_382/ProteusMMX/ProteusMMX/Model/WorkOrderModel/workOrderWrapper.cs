using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class workOrderWrapper
    {
        public string _IsWorkOrderHasTaskORInspection { get; set; }

        public bool WorkorderCreatedfromSchedule { get; set; }

        public bool EmployeeWorkHourFlag { get; set; }

        public decimal EmployeeWorkHourValue { get; set; }

        public bool WorkOrderIdentifiedThroughBreakdownFlag { get; set; }
        public int UserId { get; set; }
        public workOrders workOrder { get; set; }

        //public workOrder
        public List<RuntimeUnit> RuntimeUnit { get; set; }
        public List<workOrders> workOrders { get; set; }
        public string TimeZone { get; set; }
        public string CultureName { get; set; }

        public int WorkOrderCount { get; set; }
        public WorkOrderLabor workOrderLabor { get; set; }

        public List<WorkOrderLabor> workOrderLabors { get; set; }
        public List<Cause> Cause { get; set; }

        public Cause cause { get; set; }
        public List<StockroomPartsLookUp> stockroomPartsLookUp { get; set; }
        public WorkOrderStockroomParts workOrderStockroomPart { get; set; }
        public List<WorkOrderStockroomParts> workOrderStockroomParts { get; set; }
        public WorkOrderNonStockroomParts workOrderNonStockroomPart { get; set; }
        public List<WorkOrderNonStockroomParts> workOrderNonStockroomParts { get; set; }
        public WorkOrderAttachment attachment { get; set; }
        public List<WorkOrderAttachment> attachments { get; set; }
        public WorkOrderTool tool { get; set; }
        public List<WorkOrderTool> tools { get; set; }
        public List<ToolLookUp> toolLookUp { get; set; }

        public List<ToolTipCrib> ToolTipCribs { get; set; }

        public List<WorkOrderFormLoad> formInputs { get; set; }
        public List<WebControlTitleLabel> listWebControlTitles { get; set; }

        public string IsCheckedLaborHours { get; set; }
        public string IsCheckedWorkType { get; set; }
        public string IsCheckedCostCentre { get; set; }
        public string IsCheckedCause { get; set; }
        public string IsCheckedAutoFillCompleteOnTaskAndLabor { get; set; }

        public List<Sections> sections { get; set; }

        public DateTime? InitialStartDate { get; set; }
        public DateTime? FinalCompletionDate { get; set; }

        public bool IsToolExistsInToolCrib { get; set; }

        public DateTime? FinalStartDate { get; set; }

        public string IsCheckedAutoFillStartdateOnTaskAndLabor { get; set; }

        public string ClientIANATimeZone { get; set; }

        public List<RiskInspectionAnswers> RiskQuestions { get; set; }
        public string LOTOUrl { get; set; }

        public bool? DistributeCost { get; set; }

        public List<SignatureAuditDetail> SignatureAuditDetails { get; set; }


    }
}
