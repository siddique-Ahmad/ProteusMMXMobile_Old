using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.Shift
{
    public class ShiftService : IShiftService
    {
        private readonly IRequestService _requestService;
        public ShiftService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Task<ServiceOutput> GetShifts(string UserID, string SearchShiftName)
        {

            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetShifts);
            builder.AppendToPath(UserID);
            if (string.IsNullOrWhiteSpace(SearchShiftName))
            {
                builder.AppendToPath("null");

            }
            else
            {
                builder.AppendToPath(SearchShiftName);
            }

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);

        }
    }
}
