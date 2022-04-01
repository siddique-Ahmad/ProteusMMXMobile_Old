using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.Workorder.Inspection
{
    public class InspectionService : IInspectionService
    {

        private readonly IRequestService _requestService;
        public InspectionService(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public Task<ServiceOutput> GetWorkorderInspectionTime(string UserID, string WorkorderID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkorderInspectionTime);
            builder.AppendToPath(WorkorderID);
            builder.AppendToPath(UserID);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        public Task<ServiceOutput> GetRiskQuestions(string WorkorderID,string UserID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetAllRiskQuestions);
            builder.AppendToPath(WorkorderID);
            builder.AppendToPath(UserID);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        
        public Task<ServiceOutput> GetClosedWorkOrdersInspection(string ClosedWorkorderID,string UserID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetClosedWorkorderInspection);
            builder.AppendToPath(ClosedWorkorderID);
            builder.AppendToPath(UserID);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        
        public Task<ServiceOutput> GetWorkorderInspection(string WorkorderID,string Userid)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkorderInspection);
            builder.AppendToPath(WorkorderID);
            builder.AppendToPath(Userid);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        public Task<ServiceOutput> GetFailedWorkorderInspection(string WorkorderID,string failedinpsection)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetFailedWorkorderInspection);
            builder.AppendToPath(WorkorderID);
            builder.AppendToPath(failedinpsection);
            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        

        public Task<ServiceOutput> AnswerInspection(object inspectionAnswer)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.AnswerInspection);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, inspectionAnswer);//GetAsync(uri);
        }
        public Task<ServiceOutput> RiskAnswers(object RiskAnswer)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.RiskAnswers);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, RiskAnswer);//GetAsync(uri);
        }
        
        public Task<ServiceOutput> SaveWorkorderInspectionTime(object inspectionAnswer)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.SaveWorkorderInspectionTime);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, inspectionAnswer);//GetAsync(uri);
        }

        public Task<ServiceOutput> CreateInspectionTimeDetails(object inspectionAnswer)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.CreateInspectionTimeDetails);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, inspectionAnswer);//GetAsync(uri);
        }

    }
}
