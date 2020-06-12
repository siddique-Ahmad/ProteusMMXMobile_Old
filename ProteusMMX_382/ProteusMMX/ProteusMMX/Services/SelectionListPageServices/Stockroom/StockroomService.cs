using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.Stockroom
{
    public class StockroomService : IStockroomService
    {
        private readonly IRequestService _requestService;
        public StockroomService(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public Task<ServiceOutput> GetStockrooms(string UserID, string PageNumber, string RowCount, string SEARCHSTOCKROOMNAME)
        {

            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetStockrooms);
            builder.AppendToPath(UserID.ToString());
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);
           
            if (string.IsNullOrWhiteSpace(SEARCHSTOCKROOMNAME))
            {
                builder.AppendToPath("null");

            }
            else
            {
                builder.AppendToPath(SEARCHSTOCKROOMNAME);
            }

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);

        }


    }
}
