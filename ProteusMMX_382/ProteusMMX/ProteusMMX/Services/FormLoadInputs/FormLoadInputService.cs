using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.FormLoadInputs
{
    public class FormLoadInputService : IFormLoadInputService
    {
        private readonly IRequestService _requestService;
        public FormLoadInputService(IRequestService requestService)
        {
            _requestService = requestService;
        }

        public Task<ServiceOutput> GetFormControlsAndRights(string UserID, string ModuleName)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetFormControlRights);
            builder.AppendToPath(UserID);
            builder.AppendToPath(ModuleName);


            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> GetFormLoadInputForBarcode(string UserID , string FormName)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetFormLoadInput);
            builder.AppendToPath(UserID);
            builder.AppendToPath(FormName);
          

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

    }
}
