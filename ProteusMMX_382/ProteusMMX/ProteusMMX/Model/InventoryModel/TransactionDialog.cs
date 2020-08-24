using ProteusMMX.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.InventoryModel
{
    public class TransationDialog
    {
        public int? StockroomPartID { get; set; }
        public int? QuantityOnHand { get; set; }
        public int? UnitCostID { get; set; }
        public string StockroomName { get; set; }
        public int? StockroomID { get; set; }
        public decimal? OriginalAmount { get; set; }
        public int? PartID { get; set; }
        public string PartNumber { get; set; }

        public string Description { get; set; }
        public string PartName { get; set; }
        public List<TransactionType> transactionTypes { get; set; }
        public List<TransactionReason> transactionReasons { get; set; }
        public List<CostCenter> costCenters { get; set; }
        public int? ProfileID { get; set; }
        public string Base64Image { get; set; }
        public List<ShelfBin> shelfBins { get; set; }
    }
}
