using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<ServiceOutput> LoginAsync(string url, string userName, string password);
        Task<bool> LogoutAsync();
        void SaveAuthenticatedUser(MMXUser user);

        Task<ServiceOutput> UserIsAuthenticatedAndValidAsync(string url, string userName, string password);

        Task<ServiceOutput> GetFDAValidationAsync(string url, object abc);

        
        Task<ServiceOutput> GetAPIVersion(string url);

    }
}
