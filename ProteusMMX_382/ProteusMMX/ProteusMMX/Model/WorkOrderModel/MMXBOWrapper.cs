using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class MMXBOWrapper
    {
        public List<Company> companies { get; set; }
        public List<Facility> facilities { get; set; }
        public List<TLocation> locations { get; set; }
        public List<TAsset> assets { get; set; }
        public List<TAssetSystem> assetSystems { get; set; }
    }
}
