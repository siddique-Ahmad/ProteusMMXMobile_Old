using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.AssignTo
{
    public class AssignToService : IAssignToService
    {
        private readonly IRequestService _requestService;
        public AssignToService(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public Task<ServiceOutput> GetAssignToEmployeeFacilityWise(string UserID, string PageNumber, string RowCount, string SearchAssignToEmployeeName)
        {

            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetAssignedToEmployee);
            builder.AppendToPath(UserID);
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);

            if (string.IsNullOrWhiteSpace(SearchAssignToEmployeeName))
            {
                builder.AppendToPath("null");

            }
            else
            {
                builder.AppendToPath(SearchAssignToEmployeeName);
            }

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);

        }
    }
}
