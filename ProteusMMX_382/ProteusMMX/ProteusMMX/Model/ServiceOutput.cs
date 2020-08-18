using System;
using System.Collections.Generic;
using ProteusMMX.Model;
using ProteusMMX.Model.AssetModel;
using ProteusMMX.Model.ClosedWorkOrderModel;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Model.InventoryModel;
using ProteusMMX.Model.PurchaseOrderModel;
using ProteusMMX.Model.ServiceRequestModel;
using System.Text;
using ProteusMMX.Model.CommonModels;

namespace ProteusMMX.Model
{
    public class ServiceOutput
    {
        public string CompanyProfileLogo { get; set; }
        public workOrderWrapper workOrderWrapper { get; set; }

        public List<AssignTo> assignToEmployees { get; set; }


        public StockroomWrapper stockroomWrapper { get; set; }

        public PartWrapper partWrapper { get; set; }
        
        public MMXUser mmxUser { get; set; }

        public AssetWrapper assetWrapper { get; set; }

        public AssetForAssWrapper assetforassWrapper { get; set; }

        public MMXBOWrapper targetWrapper { get; set; }

        public ClosedWorkOrderWrapper clWorkOrderWrapper { get; set; }

        public PurchaseOrderWrapper poWrapper { get; set; }

        public ServiceRequestWrapper serviceRequestWrapper { get; set; }

        public InventoryWrapper inventoryWrapper { get; set; }


        public List<ComboDD> costCenters { get; set; }

        public List<ComboDD> requesters { get; set; }

        public List<ComboDD> Cause { get; set; }

        public List<ComboDD> employeesAssignedTo { get; set; }

        public List<ComboDD> assetsSystems { get; set; }

        public List<ComboDD> PrioritiesDD { get; set; }
        public List<ComboDD> WorkOrderStatusDD { get; set; }
        public List<ComboDD> WorkTypesDD { get; set; }
        public List<ComboDD> ShiftsDD { get; set; }


        public List<ComboDD> MaintenanceCodesDD { get; set; }

        public string servicestatusmessge { get; set; }



        public string APIVersion { get; set; }

        public bool FDAEnable { get; set; }

        public string AcknowledgementURLProtocol { get; set; }

        public string Signvalue { get; set; }

        public List<WorkOrderEmployee> workOrderEmployee { get; set; }
        public List<WorkorderContractor> workorderContractor { get; set; }
        public List<WorkOrderFormLoad> CFLI { get; set; }

        public List<Model.WorkOrderModel.WebControlTitleLabel> listWebControlTitles { get; set; }

        public List<Roles> lstRoles { get; set; }
        public List<ExistingInspections> listInspection { get; set; }

        public List<WorkOrderInspectionData> WorkOrderInspectionDataWrapper { get; set; }

        public string servicestatus { get; set; }

        public string InspectionTimeInSeconds { get; set; }

        public string ContractorName { get; set; }
        public string EmployeeName { get; set; }

        public int EmployeeLaborCraftID { get; set; }

        public bool IsSignatureRequiredAndEmpty { get; set; }

        public Nullable<DateTime> InspectionStartDate { get; set; }
        public Nullable<DateTime> InspectionCompletionDate { get; set; }

        public string LaborCraftCode { get; set; }
        public string PartNumber { get; set; }
        public string StockroomID { get; set; }
        public int PageNumber { get; set; }
        public int RowspPage { get; set; }
        public List<ComboDD> UserComboDD { get; set; }

        public List<WorkorderDD> Location { get; set; }
        public List<WorkorderDD> Priority { get; set; }
        public List<WorkorderDD> Shifts { get; set; }

        public List<Module> lstModules { get; set; }
        public bool AssetExist { get; set; }
        public int? RecordCount { get; set; }
    }
}
