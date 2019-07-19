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
        Task<ServiceOutput> CreateWorkorderAttachment(string UserID, object workorder);
        Task<ServiceOutput> DeleteWorkorderAttachment(object workorder);
        Task<ServiceOutput> ClosedWorkorderAttachmentByClosedWorkorderID(string CLOSEDWORKORDERID);

        Task<ServiceOutput> GetAssetAttachmentsByAssetNumber(string AssetNumber);

        
    }
}
