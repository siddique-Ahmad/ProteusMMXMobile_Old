using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.ServiceRequest
{
    public class ServiceRequestModuleService: IServiceRequestModuleService
    {
        private readonly IRequestService _requestService;
        public ServiceRequestModuleService(IRequestService requestService)
        {
            _requestService = requestService;
        }
        

        public Task<ServiceOutput> GetAdministrators()
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetAdministrator);
           
            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        public Task<ServiceOutput> GetServiceRequests(string UserID, string PageNumber, string RowCount)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetServiceRequest);
            builder.AppendToPath(UserID);
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        public Task<ServiceOutput> GetServiceRequestDetailByServiceRequestID(string ServiceRequestID,string UserID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetServiceRequestBYServiceRequestID);
            builder.AppendToPath(ServiceRequestID);
            builder.AppendToPath(UserID);
            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        
        public Task<ServiceOutput> ServiceRequestByServiceRequestNumber(string UserID, string ServiceRequestNumber)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetServiceRequestByRequestNumber);
            builder.AppendToPath(UserID);
            builder.AppendToPath(ServiceRequestNumber);
           
            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        public Task<ServiceOutput> CreateServiceRequest(object servicerequest)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.CreateServiceRequest);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, servicerequest);//GetAsync(uri);
        }
        public Task<ServiceOutput> AcceptServiceRequest(object servicerequest)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.AcceptServiceRequest);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, servicerequest);//GetAsync(uri);
        }
        public Task<ServiceOutput> DeclineServiceRequest(object servicerequest)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.DeclineServiceRequest);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, servicerequest);//GetAsync(uri);
        }


        public Task<ServiceOutput> EditServiceRequest(object servicerequest)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.EditServiceRequest);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, servicerequest);//GetAsync(uri);
        }
        

    }
}
