using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.Stockroom
{
    public interface IStockroomService
    {
        Task<ServiceOutput> GetStockrooms(string UserID, string PageNumber, string RowCount, string SEARCHSTOCKROOMNAME);

    }
}
