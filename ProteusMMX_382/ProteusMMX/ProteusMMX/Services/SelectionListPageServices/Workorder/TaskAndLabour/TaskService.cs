﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Request;

namespace ProteusMMX.Services.SelectionListPageServices.Workorder.TaskAndLabour
{
    public class TaskService : ITaskService
    {
        private readonly IRequestService _requestService;
        public TaskService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Task<ServiceOutput> GetContractor(string UserID, string PageNumber, string RowCount, string SearchContractorName, string Type, int workorderid)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkOrderLaborLookUp);
            builder.AppendToPath(UserID);
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);


            SearchContractorName = string.IsNullOrWhiteSpace(SearchContractorName) ? "null" : SearchContractorName;

            builder.AppendToPath("null");
            builder.AppendToPath("null");
            builder.AppendToPath(SearchContractorName);
            builder.AppendToPath(Type);
            builder.AppendToPath(workorderid.ToString());


            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        
        public Task<ServiceOutput> GetDefaultEmployee(string AssetID, string LocationID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetDefaultEmployee);
            if(String.IsNullOrEmpty(AssetID))
            {
                builder.AppendToPath("null");
            }
            else
            {
                builder.AppendToPath(AssetID);
            }
            if (String.IsNullOrEmpty(LocationID))
            {
                builder.AppendToPath("null");
            }
            else
            {
                builder.AppendToPath(LocationID);
            }
            
          
            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
            public Task<ServiceOutput> GetEmployee(string UserID, string PageNumber, string RowCount, string SearchEmployeeName,string Type, int workorderid)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkOrderLaborLookUp);
            builder.AppendToPath(UserID);
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);


            SearchEmployeeName = string.IsNullOrWhiteSpace(SearchEmployeeName) ? "null" : SearchEmployeeName;

            builder.AppendToPath("null");
            builder.AppendToPath(SearchEmployeeName);
            builder.AppendToPath("null");
            builder.AppendToPath(Type);
            builder.AppendToPath(workorderid.ToString());

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> GetTask(string UserID, string PageNumber, string RowCount, string SearchTaskNumber, string Type, int workorderid)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetWorkOrderLaborLookUp);
            builder.AppendToPath(UserID);
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);


            SearchTaskNumber = string.IsNullOrWhiteSpace(SearchTaskNumber) ? "null" : SearchTaskNumber;
           
            builder.AppendToPath(SearchTaskNumber);
            builder.AppendToPath("null");
            builder.AppendToPath("null");
            builder.AppendToPath(Type);
            builder.AppendToPath(workorderid.ToString());


            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
    }
}
