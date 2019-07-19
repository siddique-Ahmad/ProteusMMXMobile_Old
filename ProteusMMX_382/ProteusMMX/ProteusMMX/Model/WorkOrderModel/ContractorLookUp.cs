using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class ContractorLookUp
    {
        public int? ContractorLaborCraftID { get; set; }
        public string ContractorCode { get; set; }
        public string ContractorName { get; set; }
        public string LaborCraftCode { get; set; }

        public string ContractorNameLocal
        {
            get
            {

                if (string.IsNullOrWhiteSpace(LaborCraftCode))
                {
                    return ContractorName;
                }
                return ContractorName + "(" + LaborCraftCode + ")";
            }
        }

    }
}
