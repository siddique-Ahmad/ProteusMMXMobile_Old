using ProteusMMX.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class TaskLookUp
    {
        public int? TaskID { get; set; }
        public string TaskNumber { get; set; }
        public string LaborCraftCode { get; set; }
        public string Description { get; set; }

        public string TaskNumberLocal
        {
            get
            {
                if (TaskNumber == "Select")
                {
                    return TaskNumber;
                }
                string DescText = RemoveHTML.StripHtmlTags(Description);
                return TaskNumber + "(" + ShortString.shorten(DescText) + ")";
            }
        }

    }
}
