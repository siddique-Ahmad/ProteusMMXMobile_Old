using ProteusMMX.Model.WorkOrderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Model.ServiceRequestModel
{
    public class ServiceRequestWrapper
    {
        public bool IsAttachmentModified { get; set; }
        public ServiceRequests serviceRequest { get; set; }
        public List<ServiceRequests> serviceRequests { get; set; }
        public string TimeZone { get; set; }
        public string ClientIANATimeZone { get; set; }
        public int SRCount { get; set; }
        public string CultureName { get; set; }
        public List<ServiceRequestFormLoad> formInputs { get; set; }
        public List<WebControlTitleLabel> listWebControlTitles { get; set; }

        public int UserId { get; set; }

        public List<SignatureAuditDetail> SignatureAuditDetails { get; set; }

        public ServiceRequestAttachment attachment { get; set; }
        public List<ServiceRequestAttachment> attachments { get; set; }
    }
}
