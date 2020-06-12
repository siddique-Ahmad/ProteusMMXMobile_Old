using ProteusMMX.Extensions;
using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Services.Request;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.Inventory
{
    public class InventoryService:IInventoryService
    {
        public StockroomPartsSearch PartToSearch { get; set; }
        private readonly IRequestService _requestService;
        public InventoryService(IRequestService requestService)
        {
            _requestService = requestService;
        }
        public Task<ServiceOutput> GetStockrooms(string UserID, string PageNumber, string RowCount,string searchstockrromName)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetStockrooms);
            builder.AppendToPath(UserID);
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);
            if (String.IsNullOrWhiteSpace(searchstockrromName))
            {
                builder.AppendToPath("null");
            }
            else
            {
                builder.AppendToPath(searchstockrromName);
            }

          
            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        public Task<ServiceOutput> GetPerformBY(string UserID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetPerformBY);
            builder.AppendToPath(UserID);
            

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        
        public Task<ServiceOutput> GetStockroomsFromSearchBar(string Name, string UserID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetStockroomsFromSearch);
            builder.AppendToPath(Name);
            builder.AppendToPath(UserID);
           
            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        public Task<ServiceOutput> GetStockroomPartFromSearchBar(string StockroomID,string PartNumber,string UserID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetStockroomPartFromSearch);
           
            var uri = builder.Uri.AbsoluteUri;
            PartToSearch = new StockroomPartsSearch();
            PartToSearch.StockroomID = StockroomID;
            PartToSearch.PartNumber = PartNumber;
            PartToSearch.UserID = Convert.ToInt32(UserID);
            return _requestService.PostAsync(uri, PartToSearch);
        }

        public Task<ServiceOutput> GetBOMParts(string AssetNumber)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetBOMPart);

            var uri = builder.Uri.AbsoluteUri;
            PartToSearch = new StockroomPartsSearch();
            PartToSearch.AssetNumber = AssetNumber;
           
            return _requestService.PostAsync(uri, PartToSearch);
        }



        public Task<ServiceOutput> GetStockroomParts(string StockroomId, string PageNumber, string RowCount)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetStockroomParts);
            builder.AppendToPath(StockroomId);
            builder.AppendToPath(PageNumber);
            builder.AppendToPath(RowCount);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> GetCostCenter(string StockroompartId,string UserID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GetCostCenter);
            builder.AppendToPath(StockroompartId);
            builder.AppendToPath(UserID);
           

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> GetShelfBin(string StockroompartId, string UserID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.Getshelfbin);
            builder.AppendToPath(StockroompartId);
            builder.AppendToPath(UserID);


            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }

        public Task<ServiceOutput> GetTransactionReason(string StockroompartId, string UserID)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.GettransactionReason);
            builder.AppendToPath(StockroompartId);
            builder.AppendToPath(UserID);


            var uri = builder.Uri.AbsoluteUri;
            return _requestService.GetAsync(uri);
        }
        public Task<ServiceOutput> PerformInventoryTransaction(object inventory)
        {
            UriBuilder builder = new UriBuilder(AppSettings.BaseURL);
            builder.AppendToPath(AppSettings.PerformTransaction);

            var uri = builder.Uri.AbsoluteUri;
            return _requestService.PostAsync(uri, inventory);//GetAsync(uri);
        }




    }
}
