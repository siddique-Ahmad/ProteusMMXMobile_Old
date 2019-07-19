using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.WorkOrderModel
{
    public class WorkOrderAttachment
    {
        public int? WorkOrderAttachmentID { get; set; }
        public int? WorkOrderID { get; set; }
        public string Path { get; set; }
        public DateTime? ModifiedTimestamp { get; set; }
        public string ModifiedUserName { get; set; }
        public string Module { get; set; }
        public int? AssetID { get; set; }
        public string Expression { get; set; }
        public string attachmentFile { get; set; }
        public string attachmentFileExtension { get; set; }

        public int? attachmentcount { get; set; }
    }
}
