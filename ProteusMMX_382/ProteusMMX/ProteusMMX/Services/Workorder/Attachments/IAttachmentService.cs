using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.Workorder.Attachments
{
    public interface IAttachmentService
    {
        Task<ServiceOutput> GetWorkorderAttachments(string UserID, string WorkorderID);

        Task<ServiceOutput> GetServiceRequestAttachments(string UserID, string ServiceRequestID);
        Task<ServiceOutput> CreateWorkorderAttachment(string UserID, object workorder);

        Task<ServiceOutput> CreateServiceRequestAttachment(string UserID, object ServiceRequest);
        Task<ServiceOutput> DeleteWorkorderAttachment(object workorder);

        Task<ServiceOutput> DeleteServiceRequestAttachment(object ServiceRequest);
        Task<ServiceOutput> ClosedWorkorderAttachmentByClosedWorkorderID(string CLOSEDWORKORDERID);

        Task<ServiceOutput> GetAssetAttachmentsByAssetNumber(string AssetNumber);

        
    }
}
