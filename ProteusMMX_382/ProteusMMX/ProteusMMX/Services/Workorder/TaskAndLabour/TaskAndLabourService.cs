using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Request;

namespace ProteusMMX.Services.Workorder.TaskAndLabour
{
    public class TaskAndLabourService : ITaskAndLabourService
    {
        private readonly IRequestService _requestService;
        public TaskAndLabourService(IRequestService requestService)
        {
            _requestService = requestService;
        }
        

        public Task<ServiceOutput> CreateWorkOrderLaborHours(object workorder)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.CreateWorkOrderLaborHours);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, workorder);//GetAsync(uri);
        }

        public Task<ServiceOutput> UpdateTaskAndLabour(object workorder)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.UpdateWorkOrderLabour);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, workorder);//GetAsync(uri);
        }

        public Task<ServiceOutput> WorkOrderLaborsByWorkOrderID(string UserID, string WorkorderID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkorderLabour);
            builder.AppendToPath(WorkorderID);
            builder.AppendToPath(UserID);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> WorkOrderLaborsByWorkOrderIDAndTaskNumber(string UserID, string WorkorderID, string TaskID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkorderLaborsByWorkOrderIDAndTaskNumber);
            builder.AppendToPath(WorkorderID);
            builder.AppendToPath(TaskID);
            builder.AppendToPath(UserID);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        public Task<ServiceOutput> ClosedWorkOrdersLaborByClosedWorkorderID(string CLOSEDWORKORDERID, string UserID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.ClosedWorkOrdersLaborByClosedWorkorderID);
            builder.AppendToPath(CLOSEDWORKORDERID);
            builder.AppendToPath(UserID);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        
        public Task<ServiceOutput> CreateWorkOrderLabor(object workorder)
        {

            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.CreateWorkOrderLabor);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, workorder);//GetAsync(uri);

        }

    }
}
