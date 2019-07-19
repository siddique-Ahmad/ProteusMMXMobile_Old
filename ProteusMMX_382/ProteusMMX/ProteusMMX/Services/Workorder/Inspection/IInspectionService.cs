using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.Services.Workorder.Inspection
{
    public interface IInspectionService
    {
        Task<ServiceOutput> GetWorkorderInspectionTime(string UserID, string WorkorderID);
        Task<ServiceOutput> GetWorkorderInspection(string WorkorderID);
        Task<ServiceOutput> AnswerInspection(object workorder);

        Task<ServiceOutput> GetClosedWorkOrdersInspection(string CLOSEDWORKORDERID,string UserID);

        Task<ServiceOutput> GetRiskQuestions(string WorkorderID,string UserID);

        Task<ServiceOutput> RiskAnswers(object workorder);

        Task<ServiceOutput> SaveWorkorderInspectionTime(object inspectionAnswer);
    }
}
