using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.Translations
{
    public interface IWebControlTitlesService
    {
        Task<ServiceOutput> GetWebControlTitles(string UserID);
    }
}
