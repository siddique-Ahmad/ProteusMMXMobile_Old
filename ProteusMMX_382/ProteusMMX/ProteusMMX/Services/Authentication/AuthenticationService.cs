using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Services.Connectivity;
using ProteusMMX.Services.Dialog;
using ProteusMMX.Services.Navigation;
using ProteusMMX.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRequestService _requestService;

        public AuthenticationService( IRequestService requestService)
        {
            _requestService = requestService;
        }

      

        public Task<ServiceOutput> LoginAsync(string url, string userName, string password)
        {

            try
            {

                UriBuilder builder = new UriBuilder(url);
                builder.Path += AppSettings.MMXLogin;
                var uri = builder.Uri.AbsoluteUri;

                var user = new MMXUser()
                {
                    UserName = userName,
                    Password = password

                };

                return _requestService.PostAsync(uri, user);




            }
            catch (Exception ex)
            {

                return null;

            }

        }


        public Task<bool> LogoutAsync()
        {
            try
            {
                AppSettings.RemoveUserData();
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);
            }
        }

        public void SaveAuthenticatedUser(MMXUser user)
        {
            try
            {
                AppSettings.User = user;
            }
            catch (Exception ex)
            {

                
            }
        }


        public Task<ServiceOutput> GetAPIVersion(string url)
        {
            UriBuilder builder = new UriBuilder(url);
            builder.Path += AppSettings.GetAPIVersion;
            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> UserIsAuthenticatedAndValidAsync(string url, string userName, string password)
        {
             return LoginAsync(url, userName, password);
        }

        public Task<ServiceOutput> GetFDAValidationAsync(string url,object abc)
        {
            UriBuilder builder = new UriBuilder(url);
            builder.AppendToPath(AppSettings.FDAValidation);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, abc);//GetAsync(uri);
        }
    }
}
