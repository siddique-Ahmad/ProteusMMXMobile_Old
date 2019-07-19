using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.AssetModel
{
    public class AssetAttachments
    {
        public int AssetAttachmentID { get; set; }
        public int? AssetID { get; set; }
        public string Path { get; set; }
        public DateTime ModifiedTimestamp { get; set; }
        public string ModifiedUserName { get; set; }
        public bool? AssociatedWithWorkOrder { get; set; }
        public string Expression { get; set; }
        public string Attachment { get; set; }
        public string AttachmentNameWithExtension { get; set; }
    }
}
