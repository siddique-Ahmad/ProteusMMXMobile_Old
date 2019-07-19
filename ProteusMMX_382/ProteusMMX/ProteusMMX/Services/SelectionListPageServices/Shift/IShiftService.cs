using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.Shift
{
    public interface IShiftService
    {
        Task<ServiceOutput> GetShifts(string UserID, string SearchShiftName);
    }
}
