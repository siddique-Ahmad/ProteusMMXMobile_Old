using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.Parts
{
    public class PartService : IPartService
    {
        private readonly IRequestService _requestService;
        public PartService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Task<ServiceOutput> GetParts(StockroomPartsSearch PartToSearch)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetParts);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, PartToSearch);//GetAsync(uri);
        }


    }
}
