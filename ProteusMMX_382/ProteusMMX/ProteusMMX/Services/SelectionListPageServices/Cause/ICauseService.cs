using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.Cause
{
    public interface ICauseService
    {
        Task<ServiceOutput> GetCause(string SearchCause);

    }
}
