using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.ServiceRequest
{
    public interface IServiceRequestModuleService
    {
        Task<ServiceOutput> GetServiceRequests(string UserID, string PageNumber, string RowCount);

        Task<ServiceOutput> GetServiceRequestDetailByServiceRequestID(string ServiceRequestID,string UserID);

        Task<ServiceOutput> ServiceRequestByServiceRequestNumber(string UserID, string ServiceRequestNumber);

        Task<ServiceOutput> CreateServiceRequest(object ServiceRequest);

        Task<ServiceOutput> AcceptServiceRequest(object ServiceRequest);

        Task<ServiceOutput> DeclineServiceRequest(object ServiceRequest);

        Task<ServiceOutput> EditServiceRequest(object ServiceRequest);

    }
}
