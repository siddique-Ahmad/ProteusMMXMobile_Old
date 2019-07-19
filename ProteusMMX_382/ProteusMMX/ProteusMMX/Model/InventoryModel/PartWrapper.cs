using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.InventoryModel
{
    public class PartWrapper
    {
        public List<Part> parts { get; set; }


        public int partCount { get; set; }


        public string BillOfMaterialName { get; set; }
        public bool AssetExist { get; set; }
    }
}
