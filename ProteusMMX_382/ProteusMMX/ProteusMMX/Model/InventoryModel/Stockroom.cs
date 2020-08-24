using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.InventoryModel
{
    public class Stockroom
    {
        
        public int? StockroomID { get; set; }
        public string StockroomName { get; set; }
        public int? NumberOfParts { get; set; }
        public int? QuantityOnHand { get; set; }
        public decimal? QuantityAllocated { get; set; }
        public decimal? TotalQuantityAvailable { get; set; }
        public decimal? TotalCostAmount { get; set; }
        public string TotalCostCurrency { get; set; }
        public string UserField1 { get; set; }
        public string UserField2 { get; set; }
        public string UserField3 { get; set; }
        public string UserField4 { get; set; }
        public string UserField5 { get; set; }
        public string UserField6 { get; set; }
        public string UserField7 { get; set; }
        public string UserField8 { get; set; }
        public string UserField9 { get; set; }
        public string UserField10 { get; set; }
    }
}
    


