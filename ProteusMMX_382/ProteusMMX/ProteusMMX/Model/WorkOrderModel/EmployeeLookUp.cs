using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class EmployeeLookUp
    {
        public int? EmployeeLaborCraftID { get; set; }
        public string EmployeeNumber { get; set; }
        public string EmployeeName { get; set; }
        public string LaborCraftCode { get; set; }

        public string EmployeeNameLocal
        {
            get
            {
                if (EmployeeName == "Select")
                {
                    return EmployeeName;
                }
                return EmployeeName + "(" + LaborCraftCode + ")";
            }
        }

    }
}
