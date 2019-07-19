using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.FormLoadInputs
{
    public interface IFormLoadInputService
    {
        Task<ServiceOutput> GetFormLoadInputForBarcode(string UserID , string FormName);
        Task<ServiceOutput> GetFormControlsAndRights(string UserID , string ModuleName);
    }
}
