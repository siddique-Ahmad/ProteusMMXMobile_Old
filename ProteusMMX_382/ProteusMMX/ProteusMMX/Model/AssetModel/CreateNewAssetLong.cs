using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.AssetModel
{
    public class CreateNewAssetLong
    {
        public int UserID { get; set; }
        public string ClientIANATimeZone { get; set; }
        public bool IsAttachmentModified { get; set; }
        public asset asset { get; set; }
        public attachments Attachments { get; set; }
    }
}
