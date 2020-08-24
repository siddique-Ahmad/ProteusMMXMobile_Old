using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class TAssetSystem
    {
        public int? AssetSystemID { get; set; }
        public string AssetSystemName { get; set; }
        public string AssetSystemNumber { get; set; }

        public string AssetSystemNumberLocal
        {
            get
            {
                if (string.IsNullOrWhiteSpace(AssetSystemName))
                {
                    return AssetSystemNumber;
                }
                return AssetSystemNumber + "(" + AssetSystemName + ")";
            }
        }
    }
}
