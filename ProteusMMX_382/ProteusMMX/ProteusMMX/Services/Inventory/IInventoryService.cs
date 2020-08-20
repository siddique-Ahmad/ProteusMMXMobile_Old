using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.Inventory
{
    public interface IInventoryService
    {
        Task<ServiceOutput> GetStockrooms(string UserID, string PageNumber, string RowCount);

        Task<ServiceOutput> GetPerformBY(string UserID);

        Task<ServiceOutput> GetBOMParts(string AssetNumber);

        Task<ServiceOutput> GetStockroomsFromSearchBar(string Name,string UserID);

        Task<ServiceOutput> GetStockroomPartFromSearchBar(string StockroomID, string PartNumber, string UserID);

        Task<ServiceOutput> GetStockroomParts(string StockroomID, string PageNumber, string RowCount);

        Task<ServiceOutput> GetCostCenter(string STOCKROOMPARTID, string UserID);

        Task<ServiceOutput> GetShelfBin(string STOCKROOMPARTID, string UserID);

        Task<ServiceOutput> GetTransactionReason(string STOCKROOMPARTID, string UserID);

        Task<ServiceOutput> PerformInventoryTransaction(object inventory);


    }
}
