using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.ServiceRequestModel
{
    public class ServiceRequestAttachment
    {
        public int? ServiceRequestAttachmentID { get; set; }
        public int? ServiceRequestID { get; set; }
        public string Path { get; set; }
        public DateTime? ModifiedTimestamp { get; set; }
        public string ModifiedUserName { get; set; }
        public string Expression { get; set; }
        public string AttachmentBase64String { get; set; }
        public string AttachmentNameWithExtension { get; set; }

        public string attachmentFile { get; set; }
        public string attachmentFileExtension { get; set; }
    }
}
