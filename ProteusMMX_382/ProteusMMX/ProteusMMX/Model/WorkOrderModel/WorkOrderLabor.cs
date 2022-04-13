using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class WorkOrderLabor
    {
        /// <summary>
        /// Mandatory
        /// </summary>
        public int? WorkOrderLaborID { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public int? WorkOrderID { get; set; }
        public int? TaskID { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public DateTime? StartDate { get; set; }

       
        public DateTime? CompletionDate { get; set; }
        public int? ContractorLaborCraftID { get; set; }
        public int? EmployeeLaborCraftID { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public string HoursAtRate1 { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public string HoursAtRate2 { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public string HoursAtRate3 { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public string HoursAtRate4 { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public string HoursAtRate5 { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public int? Sequence { get; set; }
        public DateTime? ModifiedTimestamp { get; set; }
        public string ModifiedUserName { get; set; }
        public string TaskNumber { get; set; }
        public string Description { get; set; }
        public decimal? EstimatedHours { get; set; }
        public string EmployeeName { get; set; }
        public string LaborCraftCode { get; set; }
        public string ContractorName { get; set; }
        public List<TaskLookUp> Tasks { get; set; }
        public List<EmployeeLookUp> Employees { get; set; }
        public List<ContractorLookUp> Contractors { get; set; }

        #region New properties for Timer

        public DateTime? EndDate { get; set; }
        public DateTime? HoursAtRate1Stop { get; set; }
        public DateTime? HoursAtRate2Stop { get; set; }
        public DateTime? HoursAtRate1Start { get; set; }
        public DateTime? HoursAtRate2Start { get; set; }
        public int? WorkOrderLaborHourID1 { get; set; }
        public int? WorkOrderLaborHourID2 { get; set; }
        public string TotalHoursAtRate1 { get; set; }
        public string TotalHoursAtRate2 { get; set; }
        public int? WorkOrderLaborHourID { get; set; }
        public bool IsMannual { get; set; }
        public int? TimerID { get; set; }

        public int? Timer1 { get; set; }
        public int? Timer2 { get; set; }

        #endregion

        #region For GlobalTimer
        public DateTime StartTimeOfTimer { get; set; }
      
        #endregion

    }
}
