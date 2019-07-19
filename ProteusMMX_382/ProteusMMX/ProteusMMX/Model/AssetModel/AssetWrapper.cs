using ProteusMMX.Model.WorkOrderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.AssetModel
{
    public class AssetWrapper
    {
        public Assets asset { get; set; }

        public int assetCount { get; set; }
        public List<AssetAttachments> attachments { get; set; }
        public bool IsAttachmentModified { get; set; }
        public List<Assets> assets { get; set; }
        public List<ComboDD> listCategory { get; set; }
        public List<ComboDD> listVendor { get; set; }
        public List<ComboDD> listAssetSystem { get; set; }
        public List<ComboDD> listRuntimeUnits { get; set; }
        public List<ComboDD> requesters { get; set; }
        //  public List<AssetFormLoad> formInputs { get; set; }
        public List<WebControlTitleLabel> listWebControlTitles { get; set; }
        public string AttachmentPathDirectory { get; set; }

        public List<ComboDD> costCenters { get; set; }

    }
}
