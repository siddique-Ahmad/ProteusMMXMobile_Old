using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.SelectionListPageServices.Parts
{
    public interface IPartService
    {
        Task<ServiceOutput> GetParts(StockroomPartsSearch PartToSearch);

    }
}
