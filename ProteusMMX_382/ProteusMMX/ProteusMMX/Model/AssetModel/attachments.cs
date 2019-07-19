using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.AssetModel
{
    public class attachments
    {
        public int AssetAttachmentID { get; set; }

        public int AssetID { get; set; }

        public bool AssociatedWithWorkOrder { get; set; }

        public string Attachment { get; set; }

        public string AttachmentNameWithExtension { get; set; }

        public string ModifiedUserName { get; set; }
    }
}
