using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.Tool
{
    public class ToolService : IToolService
    {
        private readonly IRequestService _requestService;
        public ToolService(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public Task<ServiceOutput> GetToolCrib(string UserID, string PageNumber, string RowCount, string SEARCHTOOLCRIB)
        {

            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkOrderToolCrib);
            builder.AppendToPath(UserID.ToString());
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);
           
            if (string.IsNullOrWhiteSpace(SEARCHTOOLCRIB))
            {
                builder.AppendToPath("null");

            }
            else
            {
                builder.AppendToPath(SEARCHTOOLCRIB);
            }

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);

        }


        public Task<ServiceOutput> GetToolNumber(string workorderid, string UserID, string TOOLCRIBID, string PageNumber, string RowCount, string SEARCHTOOLS)
        {

            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkOrderToolNumber);
            builder.AppendToPath(workorderid.ToString());
            builder.AppendToPath(UserID.ToString());
            builder.AppendToPath(TOOLCRIBID.ToString());
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);

            if (string.IsNullOrWhiteSpace(SEARCHTOOLS))
            {
                builder.AppendToPath("null");

            }
            else
            {
                builder.AppendToPath(SEARCHTOOLS);
            }

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);

        }

        public Task<ServiceOutput> CreateWorkOrderTool(object tool)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.CreateWorkorderTool);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, tool);//GetAsync(uri);
        }

        public Task<ServiceOutput> CheckDupliacateTool(string workorderid, string TOOLNUMBER)
        {
          
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.CheckDuplicateTool);
            builder.AppendToPath(workorderid.ToString());
            builder.AppendToPath(TOOLNUMBER.ToString());
            var uri = builder.Uri.AbsoluteUri;
             return  _requestService.GetAsync(uri);
           
        }
        public Task<ServiceOutput> GetToolNumberDetailFromScan(string workorderid, string UserID, string TOOLCRIBID, string PageNumber, string RowCount, string SEARCHTOOLS)
        {
           
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetToolNumberDetailFromScan);
            builder.AppendToPath(workorderid.ToString());
            builder.AppendToPath(UserID.ToString());
            builder.AppendToPath(TOOLCRIBID.ToString());
            builder.AppendToPath("0");
            builder.AppendToPath("0");
            if (string.IsNullOrWhiteSpace(SEARCHTOOLS))
            {
                builder.AppendToPath("null");

            }
            else
            {
                builder.AppendToPath(SEARCHTOOLS);
            }
            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);

        }

        

    }
}
