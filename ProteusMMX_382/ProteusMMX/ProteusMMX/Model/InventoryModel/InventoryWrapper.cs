using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.InventoryModel
{
    public class InventoryWrapper
    {
        public List<Stockroom> stockrooms { get; set; }
        public List<StockroomPart> stockroomparts { get; set; }
        public TransationDialog trasactionDialog { get; set; }

        public int recordCountStockRooms { get; set; }
        public int recordCountStockRoomParts { get; set; }

        public bool isPartDuplicate { get; set; }

        public string IsMultiplePriceForPartsInWorkOrder { get; set; }
    }
}
