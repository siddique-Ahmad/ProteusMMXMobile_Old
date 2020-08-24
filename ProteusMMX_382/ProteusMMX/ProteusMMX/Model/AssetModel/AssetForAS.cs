using System;
using System.Collections.Generic;
using System.Text;

namespace ProteusMMX.Model.AssetModel
{
    public class AssetForAS
    {
        public int AssetID { get; set; }
        public string AssetNumber { get; set; }
        public string AssetName { get; set; }
        public string Description { get; set; }

        public bool ShowAssetSystem
        {
            get
            {
                return false;
            }
        }
    }
}