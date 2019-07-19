using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.InventoryModel
{
    public class InventoryTransaction
    {
        public string Description { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public string PartNumber { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public string PartName { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public string StockroomName { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public string TransactionType { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public int AdjustmentQuantity { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public int? QuantityOnHand { get; set; }
        public decimal UnitCost { get; set; }
        public string TransactionReason { get; set; }
        public string CostCenter { get; set; }
        public int CostCenterID { get; set; }
        public string ModifiedUserName { get; set; }
        /// <summary>
        /// Mandatory
        /// </summary>
        public int? StockroomPartID { get; set; }

        public string Transactor { get; set; }

        public int? ShelfBinID { get; set; }
        public string ShelfBinName { get; set; }

        public string CheckOutTo { get; set; }
        public string UserField1 { get; set; }
        public string UserField2 { get; set; }
        public string UserField3 { get; set; }
        public string UserField4 { get; set; }

        public bool UpdateLastPhysicalInventorydate { get; set; }

    }
}
