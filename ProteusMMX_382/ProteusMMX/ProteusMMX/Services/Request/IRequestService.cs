using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.Request
{
    public interface IRequestService
    {
        Task<ServiceOutput> GetAsync(string url);

        Task<ServiceOutput> PostAsync(string url, object obj);
    }
}
