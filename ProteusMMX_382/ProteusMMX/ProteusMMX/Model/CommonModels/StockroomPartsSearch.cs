using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.CommonModels
{
    public class StockroomPartsSearch : ServiceInput
    {
        public string AssetNumber { get; set; }
        public string StockroomID { get; set; }
        public string PartNumber { get; set; }
        public int PageNumber { get; set; }
        public int RowspPage { get; set; }
        public int UserID { get; set; }
        public string StockRoomName { get; set; }
    }



}
