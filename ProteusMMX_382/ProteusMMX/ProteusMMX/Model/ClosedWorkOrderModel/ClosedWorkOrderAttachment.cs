using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.ClosedWorkOrderModel
{
    public class ClosedWorkOrderAttachment
    {
        public int? ClosedWorkOrderAttachmentID { get; set; }
        public int? ClosedWorkOrderID { get; set; }
        public string Path { get; set; }
        public DateTime? ModifiedTimestamp { get; set; }
        public string ModifiedUserName { get; set; }
        public string Expression { get; set; }

        public string attachmentFile { get; set; }
        public string attachmentFileExtension { get; set; }
    }
}
