using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.Barcode
{
    public interface IBarcodeService
    {
        Task<ServiceOutput> GetFormLoadInputForBarcode(string UserID);
        Task<ServiceOutput> GetAssetByAssetNumber(StockroomPartsSearch Part);
        Task<ServiceOutput> GetAssetAttachmentsByAssetNumber(StockroomPartsSearch Part);
        String GetAttachmentsByFileName(string FileName);
    }
}
