using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.CommonModels
{
    public class ServiceInput
    {


        public long InspectionID { get; set; }
        public int WorkorderID { get; set; }
        public string AssetNumber { get; set; }
        public string StockroomID { get; set; }
        public string PartNumber { get; set; }
        public int PageNumber { get; set; }
        public int RowspPage { get; set; }
        public string TimeZone { get; set; }
        public string ClientIANATimeZone { get; set; }

    }
}
