using NodaTime;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using ProteusMMX.Extensions;
using ProteusMMX.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ProteusMMX
{
    public static class AppSettings
    {
        private static ISettings Settings => CrossSettings.Current;

        static string RememberMeSwitchValue = "";

        public static string APIVersion
        {
            get => Settings.GetValueOrDefault(nameof(APIVersion), string.Empty);

            set => Settings.AddOrUpdateValue(nameof(APIVersion), value);
        }

        public static string APPVersion
        {
            get => Settings.GetValueOrDefault(nameof(APPVersion), "3.15.2");
        }
        public static string UserName
        {
            get => Settings.GetValueOrDefault(nameof(UserName), string.Empty);

            set => Settings.AddOrUpdateValue(nameof(UserName), value);
        }

        public static string Password
        {
            get => Settings.GetValueOrDefault(nameof(Password), string.Empty);

            set => Settings.AddOrUpdateValue(nameof(Password), value);
        }

        public static bool RememberMeSwitchFlag
        {
            get => Settings.GetValueOrDefault(nameof(RememberMeSwitchFlag), false);

            set => Settings.AddOrUpdateValue(nameof(RememberMeSwitchFlag), value);
        }

        public static string UserCultureName
        {
            get => Settings.GetValueOrDefault(nameof(UserCultureName), "en-US");
        }
        public static string UserTimeZone
        {
            get => Settings.GetValueOrDefault(nameof(UserTimeZone), "UTC");
        }
        public static string ClientIANATimeZone
        {
            get => Settings.GetValueOrDefault(nameof(ClientIANATimeZone), DateTimeZoneProviders.Serialization.GetSystemDefault().ToString());
        }


        public static MMXUser User
        {
            get => Settings.GetValueOrDefault(nameof(User), default(MMXUser));

            set => Settings.AddOrUpdateValue(nameof(User), value);
        }

        public static Dictionary<string, string> Translations = new Dictionary<string, string>();

       
        public static void RemoveUserData()
        {
          
            Settings.Remove(nameof(User));
            if (Application.Current.Properties.ContainsKey("RememberMeSwitchKey"))
            {
                RememberMeSwitchValue = Application.Current.Properties["RememberMeSwitchKey"].ToString();
            }
            //Application.Current.Properties["RememberMeSwitchKey"] = false;

            if (RememberMeSwitchValue == "false")
            {
                Settings.Remove(nameof(UserName));
                Settings.Remove(nameof(Password));
                //Settings.Remove(nameof(BaseURL));
                //Settings.Remove(nameof(RememberMeSwitch));
            }
        
            
        }



        ////// Endpoints Data
       // private static string _baseURL ="https://proteusmmx.com/";  //DefaultURL
        public static string BaseURL
        {
            get => Settings.GetValueOrDefault(nameof(BaseURL), "https://proteusmmx.com/");

            set => Settings.AddOrUpdateValue(nameof(BaseURL), value);

        }



        #region Login Module

        private static string _getAPIVersion = "/Inspection/service/GetAPIVersion";
        public static string GetAPIVersion
        {
            get
            {
                return _getAPIVersion;
            }

            set
            {
                if (value != _getAPIVersion)
                {
                    _getAPIVersion = value;
                }
            }

        }


        private static string _mmxLogin = "/Inspection/service/MMXLogin";
        public static string MMXLogin
        {
            get
            {
                return _mmxLogin;
            }

            set
            {
                if (value != _mmxLogin)
                {
                    _mmxLogin = value;
                }
            }

        }

        private static string _getWebControlTitles = "Inspection/service/GetWebControlTitles";
        public static string GetWebControlTitles
        {
            get
            {
                return _getWebControlTitles;
            }

            set
            {
                if (value != _getWebControlTitles)
                {
                    _getWebControlTitles = value;
                }
            }

        }

        #endregion

        #region Barcode Module
        private static string _getFormLoadInputBarcode = "/Inspection/service/GetFormLoadInputByUserIDAndFormName/";
        public static string GetFormLoadInputBarcode
        {
            get
            {
                return _getFormLoadInputBarcode;
            }

            set
            {
                if (value != _getFormLoadInputBarcode)
                {
                    _getFormLoadInputBarcode = value;
                }
            }

        }

        private static string _getFormLoadInput = "Inspection/service/GetFormLoadInputByUserIDAndFormName";
        public static string GetFormLoadInput
        {
            get
            {
                return _getFormLoadInput;
            }

            set
            {
                if (value != _getFormLoadInput)
                {
                    _getFormLoadInput = value;
                }
            }

        }

        private static string _getFormControlRights = "Inspection/service/GetFormControlRights";
        public static string GetFormControlRights
        {
            get
            {
                return _getFormControlRights;
            }

            set
            {
                if (value != _getFormControlRights)
                {
                    _getFormControlRights = value;
                }
            }

        }

        private static string _getAssetByAssetNumber = "/Inspection/service/AssetByAssetNumber";
        public static string GetAssetByAssetNumber
        {
            get
            {
                return _getAssetByAssetNumber;
            }

            set
            {
                if (value != _getAssetByAssetNumber)
                {
                    _getAssetByAssetNumber = value;
                }
            }

        }


       

        private static string _getAttachmentsByFileName = "/Inspection/Service/attachmentdisplay.ashx?filename=";
        public static string GetAttachmentsByFileName
        {
            get
            {
                return _getAttachmentsByFileName;
            }

            set
            {
                if (value != _getAttachmentsByFileName)
                {
                    _getAttachmentsByFileName = value;
                }
            }

        }

        #endregion

        #region Workorder Module

        

       private static string _getWorkordersKPIType = "Inspection/service/GetWorkOrderDetails";
        public static string GetWorkordersKPIType
        {
            get
            {
                return _getWorkordersKPIType;
            }

            set
            {
                if (value != _getWorkordersKPIType)
                {
                    _getWorkordersKPIType = value;
                }
            }

        }

        #region Workorder
        private static string _getWorkorders = "Inspection/service/WorkOrders";
        public static string GetWorkorders
        {
            get
            {
                return _getWorkorders;
            }

            set
            {
                if (value != _getWorkorders)
                {
                    _getWorkorders = value;
                }
            }

        }

        private static string _getWorkorder = "Inspection/service/WorkOrder";
        public static string GetWorkorder
        {
            get
            {
                return _getWorkorder;
            }

            set
            {
                if (value != _getWorkorder)
                {
                    _getWorkorder = value;
                }
            }

        }

        private static string _getWorkorderDDRecord = "Inspection/service/GetWorkorderDDRecord";
        public static string GetWorkorderDDRecord
        {
            get
            {
                return _getWorkorderDDRecord;
            }

            set
            {
                if (value != _getWorkorderDDRecord)
                {
                    _getWorkorderDDRecord = value;
                }
            }

        }

        private static string _getKPIRecord = "Inspection/service/GetWorkOrderTime";
        public static string GetKPIRecord
        {
            get
            {
                return _getKPIRecord;
            }

            set
            {
                if (value != _getKPIRecord)
                {
                    _getKPIRecord = value;
                }
            }

        }

        

        private static string _getWorkorderLabour = "Inspection/service/WorkOrderLabors";
        public static string GetWorkorderLabour
        {
            get
            {
                return _getWorkorderLabour;
            }

            set
            {
                if (value != _getWorkorderLabour)
                {
                    _getWorkorderLabour = value;
                }
            }

        }

        private static string _getWorkorderInspection = "Inspection/service/GetInspectionsByWorkOrderID";
        public static string GetWorkorderInspection
        {
            get
            {
                return _getWorkorderInspection;
            }

            set
            {
                if (value != _getWorkorderInspection)
                {
                    _getWorkorderInspection = value;
                }
            }

        }
        private static string _getfailedWorkorderInspection = "Inspection/service/GetFailedInspection";
        public static string GetFailedWorkorderInspection
        {
            get
            {
                return _getfailedWorkorderInspection;
            }

            set
            {
                if (value != _getfailedWorkorderInspection)
                {
                    _getfailedWorkorderInspection = value;
                }
            }

        }

        private static string _getWorkorderInspectionTime = "Inspection/service/GetWorkOrderInspectionTimeByWorkOrderID";
        public static string GetWorkorderInspectionTime
        {
            get
            {
                return _getWorkorderInspectionTime;
            }

            set
            {
                if (value != _getWorkorderInspectionTime)
                {
                    _getWorkorderInspectionTime = value;
                }
            }

        }
        private static string _getAllRiskQuestions = "Inspection/service/Riskquestions";
        public static string GetAllRiskQuestions
        {
            get
            {
                return _getAllRiskQuestions;
            }

            set
            {
                if (value != _getAllRiskQuestions)
                {
                    _getAllRiskQuestions = value;
                }
            }

        }
        
        private static string _getClosedWorkorderInspection = "Inspection/service/ClosedWorkOrdersInspection";
        public static string GetClosedWorkorderInspection
        {
            get
            {
                return _getClosedWorkorderInspection;
            }

            set
            {
                if (value != _getClosedWorkorderInspection)
                {
                    _getClosedWorkorderInspection = value;
                }
            }

        }

        private static string _createWorkOrder = "Inspection/service/CreateWorkOrder";
        public static string CreateWorkOrder
        {
            get
            {
                return _createWorkOrder;
            }

            set
            {
                if (value != _createWorkOrder)
                {
                    _createWorkOrder = value;
                }
            }

        }

        private static string _fDAValidation = "Inspection/service/FDAValidation";
        public static string FDAValidation
        {
            get
            {
                return _fDAValidation;
            }

            set
            {
                if (value != _fDAValidation)
                {
                    _fDAValidation = value;
                }
            }

        }

        private static string _updateWorkOrder = "Inspection/service/UpdateWorkOrder";
        public static string UpdateWorkOrder
        {
            get
            {
                return _updateWorkOrder;
            }

            set
            {
                if (value != _updateWorkOrder)
                {
                    _updateWorkOrder = value;
                }
            }

        }

        private static string _saveWorkOrderAcknowledgement = "Inspection/service/SaveWorkOrderAcknowledgement";
        public static string SaveWorkOrderAcknowledgement
        {
            get
            {
                return _saveWorkOrderAcknowledgement;
            }

            set
            {
                if (value != _saveWorkOrderAcknowledgement)
                {
                    _saveWorkOrderAcknowledgement = value;
                }
            }

        }

        private static string _getIsWorkOrderSignatureRequired = "Inspection/service/IsWorkOrderSignatureRequiredAndEmpty";
        public static string GetIsWorkOrderSignatureRequired
        {
            get
            {
                return _getIsWorkOrderSignatureRequired;
            }

            set
            {
                if (value != _getIsWorkOrderSignatureRequired)
                {
                    _getIsWorkOrderSignatureRequired = value;
                }
            }

        }

        private static string _closeWorkOrder = "Inspection/service/CloseWorkOrder";
        public static string CloseWorkOrder
        {
            get
            {
                return _closeWorkOrder;
            }

            set
            {
                if (value != _closeWorkOrder)
                {
                    _closeWorkOrder = value;
                }
            }

        }

        private static string _getEmployeeAssignTo = "Inspection/service/EmployeeAssignedToUser";
        public static string GetEmployeeAssignTo
        {
            get
            {
                return _getEmployeeAssignTo;
            }

            set
            {
                if (value != _getEmployeeAssignTo)
                {
                    _getEmployeeAssignTo = value;
                }
            }

        }
        #endregion

        #region TaskAndLabour

        //WorkOrderLaborsByWorkOrderIDAndTaskNumber
        private static string _getWorkorderLaborsByWorkOrderIDAndTaskNumber = "Inspection/service/WorkOrderLaborsByWorkOrderIDAndTaskNumber";
        public static string GetWorkorderLaborsByWorkOrderIDAndTaskNumber
        {
            get
            {
                return _getWorkorderLaborsByWorkOrderIDAndTaskNumber;
            }

            set
            {
                if (value != _getWorkorderLaborsByWorkOrderIDAndTaskNumber)
                {
                    _getWorkorderLaborsByWorkOrderIDAndTaskNumber = value;
                }
            }

        }
        private static string _closedWorkOrdersLaborByClosedWorkorderID = "Inspection/service/ClosedWorkOrdersLabor";
        public static string ClosedWorkOrdersLaborByClosedWorkorderID
        {
            get
            {
                return _closedWorkOrdersLaborByClosedWorkorderID;
            }

            set
            {
                if (value != _closedWorkOrdersLaborByClosedWorkorderID)
                {
                    _closedWorkOrdersLaborByClosedWorkorderID = value;
                }
            }

        }
        

        //UpdateWorkOrderLabor
        private static string _updateWorkOrderLabour = "Inspection/service/UpdateWorkOrderLabor";
        public static string UpdateWorkOrderLabour
        {
            get
            {
                return _updateWorkOrderLabour;
            }

            set
            {
                if (value != _updateWorkOrderLabour)
                {
                    _updateWorkOrderLabour = value;
                }
            }

        }

        //CreateWorkOrderLaborHours
        private static string _createWorkOrderLaborHours = "Inspection/service/CreateWorkOrderLaborHours";
        public static string CreateWorkOrderLaborHours
        {
            get
            {
                return _createWorkOrderLaborHours;
            }

            set
            {
                if (value != _createWorkOrderLaborHours)
                {
                    _createWorkOrderLaborHours = value;
                }
            }

        }
        
        //CreateWorkOrderLabor
        private static string _createWorkOrderLabor = "Inspection/service/CreateWorkOrderLabor";
        public static string CreateWorkOrderLabor
        {
            get
            {
                return _createWorkOrderLabor;
            }

            set
            {
                if (value != _createWorkOrderLabor)
                {
                    _createWorkOrderLabor = value;
                }
            }

        }


        #endregion
        #region Inspection
        private static string _answerInspection = "Inspection/service/AnswerInspection";
        public static string AnswerInspection
        {
            get
            {
                return _answerInspection;
            }

            set
            {
                if (value != _answerInspection)
                {
                    _answerInspection = value;
                }
            }

        }
        private static string _riskAnswers = "Inspection/service/RiskAnswers";
        public static string RiskAnswers
        {
            get
            {
                return _riskAnswers;
            }

            set
            {
                if (value != _riskAnswers)
                {
                    _riskAnswers = value;
                }
            }

        }

        private static string _saveWorkorderInspectionTime = "Inspection/service/SaveWorkOrderInspectionTime";
        public static string SaveWorkorderInspectionTime
        {
            get
            {
                return _saveWorkorderInspectionTime;
            }

            set
            {
                if (value != _saveWorkorderInspectionTime)
                {
                    _saveWorkorderInspectionTime = value;
                }
            }

        }

        private static string _createInspectionTimeDetails = "Inspection/service/CreateInspectionTimeDetails";
        public static string CreateInspectionTimeDetails
        {
            get
            {
                return _createInspectionTimeDetails;
            }

            set
            {
                if (value != _createInspectionTimeDetails)
                {
                    _createInspectionTimeDetails = value;
                }
            }

        }
        #endregion
        #region WorkOrderStockroomParts

        //GetWorkOrderStockroomParts
        private static string _getWorkorderStockroomParts = "Inspection/service/WorkOrderStockroomParts";
        public static string GetWorkorderStockroomParts
        {
            get
            {
                return _getWorkorderStockroomParts;
            }

            set
            {
                if (value != _getWorkorderStockroomParts)
                {
                    _getWorkorderStockroomParts = value;
                }
            }

        }

        private static string _checkDuplicateParts = "Inspection/service/FilterParts";
        public static string CheckDuplicateParts
        {
            get
            {
                return _checkDuplicateParts;
            }

            set
            {
                if (value != _checkDuplicateParts)
                {
                    _checkDuplicateParts = value;
                }
            }

        }

        private static string _editWorkorderStockroomParts = "Inspection/service/UpdateWorkOrderStockroomParts";
        public static string EditWorkorderStockroomParts
        {
            get
            {
                return _editWorkorderStockroomParts;
            }

            set
            {
                if (value != _editWorkorderStockroomParts)
                {
                    _editWorkorderStockroomParts = value;
                }
            }

        }
        private static string _getParts = "Inspection/Service/FilterStockroomPartByStockroomIDWorkOrderID";
        public static string GetParts
        {
            get
            {
                return _getParts;
            }

            set
            {
                if (value != _getParts)
                {
                    _getParts = value;
                }
            }

        }

        private static string _getStockrooms = "Inspection/Service/Stockrooms";
        public static string GetStockrooms
        {
            get
            {
                return _getStockrooms;
            }

            set
            {
                if (value != _getStockrooms)
                {
                    _getStockrooms = value;
                }
            }

        }
        private static string _getPerformBY = "Inspection/Service/GetPerformBy";
        public static string GetPerformBY
        {
            get
            {
                return _getPerformBY;
            }

            set
            {
                if (value != _getPerformBY)
                {
                    _getPerformBY = value;
                }
            }

        }

        private static string _getStockroomsFromSearch = "Inspection/Service/StockroomByStockroomName";
        public static string GetStockroomsFromSearch
        {
            get
            {
                return _getStockroomsFromSearch;
            }

            set
            {
                if (value != _getStockroomsFromSearch)
                {
                    _getStockroomsFromSearch = value;
                }
            }

        }
        private static string _getStockroomPartFromSearch = "Inspection/service/StockRoomPartByPartNumber";
        public static string GetStockroomPartFromSearch
        {
            get
            {
                return _getStockroomPartFromSearch;
            }

            set
            {
                if (value != _getStockroomPartFromSearch)
                {
                    _getStockroomPartFromSearch = value;
                }
            }

        }

        private static string _getBOMPart = "Inspection/service/GetBOMPartsByAssetNumber";
        public static string GetBOMPart
        {
            get
            {
                return _getBOMPart;
            }

            set
            {
                if (value != _getBOMPart)
                {
                    _getBOMPart = value;
                }
            }

        }

        private static string _getStockroomParts = "Inspection/Service/StockroomPartByStockroomID";
        public static string GetStockroomParts
        {
            get
            {
                return _getStockroomParts;
            }

            set
            {
                if (value != _getStockroomParts)
                {
                    _getStockroomParts = value;
                }
            }

        }

        private static string _getCostCenter = "Inspection/Service/InventoryTransition";
        public static string GetCostCenter
        {
            get
            {
                return _getCostCenter;
            }

            set
            {
                if (value != _getCostCenter)
                {
                    _getCostCenter = value;
                }
            }

        }

        private static string _getshelfbin = "Inspection/Service/InventoryTransition";
        public static string Getshelfbin
        {
            get
            {
                return _getshelfbin;
            }

            set
            {
                if (value != _getshelfbin)
                {
                    _getshelfbin = value;
                }
            }

        }

        private static string _gettransactionReason = "Inspection/Service/InventoryTransition";
        public static string GettransactionReason
        {
            get
            {
                return _gettransactionReason;
            }

            set
            {
                if (value != _gettransactionReason)
                {
                    _gettransactionReason = value;
                }
            }

        }

        private static string _performTransaction = "Inspection/Service/InventoryPerformTransaction";
        public static string PerformTransaction
        {
            get
            {
                return _performTransaction;
            }

            set
            {
                if (value != _performTransaction)
                {
                    _performTransaction = value;
                }
            }

        }
        private static string _getWorkorderStockroomPartDetail = "Inspection/service/WorkOrderStockroomPartsByWorkOrderStockroomPartID";
        public static string GetWorkorderStockroomPartDetail
        {
            get
            {
                return _getWorkorderStockroomPartDetail;
            }

            set
            {
                if (value != _getWorkorderStockroomPartDetail)
                {
                    _getWorkorderStockroomPartDetail = value;
                }
            }

        }

        private static string _getStockroomPartDetailFromScan = "Inspection/service/GetStockroomPartsByStockroomIDPartNumber";
        public static string GetStockroomPartDetailFromScan
        {
            get
            {
                return _getStockroomPartDetailFromScan;
            }

            set
            {
                if (value != _getStockroomPartDetailFromScan)
                {
                    _getStockroomPartDetailFromScan = value;
                }
            }

        }
        private static string _getAssetAttachmentsByAssetNumber = "Inspection/service/AssetAttachmentsByAssetNumber";
        public static string GetAssetAttachmentsByAssetNumber
        {
            get
            {
                return _getAssetAttachmentsByAssetNumber;
            }

            set
            {
                if (value != _getAssetAttachmentsByAssetNumber)
                {
                    _getAssetAttachmentsByAssetNumber = value;
                }
            }

        }

        
        private static string _getWorkordersFromAsset = "Inspection/service/WorkOrderListingByAssetNumber";
        public static string GetWorkordersFromAsset
        {
            get
            {
                return _getWorkordersFromAsset;
            }

            set
            {
                if (value != _getWorkordersFromAsset)
                {
                    _getWorkordersFromAsset = value;
                }
            }

        }
        private static string _getWorkorderControlRights = "Inspection/service/GetWorkorderControlRights";
        public static string GetWorkorderControlRights
        {
            get
            {
                return _getWorkorderControlRights;
            }

            set
            {
                if (value != _getWorkorderControlRights)
                {
                    _getWorkorderControlRights = value;
                }
            }

        }
        


        #endregion

        #region WorkOrderTools

        //GetWorkOrderTools
        private static string _getWorkorderTools = "Inspection/service/WorkOrderTools";
        public static string GetWorkorderTools
        {
            get
            {
                return _getWorkorderTools;
            }

            set
            {
                if (value != _getWorkorderTools)
                {
                    _getWorkorderTools = value;
                }
            }

        }

        private static string _getWorkOrderToolNumber = "Inspection/service/GetWorkOrderToolsByToolCribID";
        public static string GetWorkOrderToolNumber
        {
            get
            {
                return _getWorkOrderToolNumber;
            }

            set
            {
                if (value != _getWorkOrderToolNumber)
                {
                    _getWorkOrderToolNumber = value;
                }
            }

        }
        private static string _createWorkorderTool = "Inspection/service/CreateWorkOrderTool";
        public static string CreateWorkorderTool
        {
            get
            {
                return _createWorkorderTool;
            }

            set
            {
                if (value != _createWorkorderTool)
                {
                    _createWorkorderTool = value;
                }
            }

        }
        private static string _checkDuplicateTool = "Inspection/service/IsExistsToolInWorkOrderTools";
        public static string CheckDuplicateTool
        {
            get
            {
                return _checkDuplicateTool;
            }

            set
            {
                if (value != _checkDuplicateTool)
                {
                    _checkDuplicateTool = value;
                }
            }

        }



        private static string _getToolNumberDetailFromScan = "Inspection/service/GetWorkOrderToolsByToolCribID";
        public static string GetToolNumberDetailFromScan
        {
            get
            {
                return _getToolNumberDetailFromScan;
            }

            set
            {
                if (value != _getToolNumberDetailFromScan)
                {
                    _getToolNumberDetailFromScan = value;
                }
            }

        }

        private static string _getWorkOrderToolCrib = "Inspection/service/WorkOrderToolCribs";
        public static string GetWorkOrderToolCrib
        {
            get
            {
                return _getWorkOrderToolCrib;
            }

            set
            {
                if (value != _getWorkOrderToolCrib)
                {
                    _getWorkOrderToolCrib = value;
                }
            }

        }





        #endregion

        #region WorkOrderNonStockroomParts

        //GetWorkOrderNonStockroomParts
        private static string _getWorkorderNonStockroomParts = "Inspection/service/WorkOrderNonStockroomParts";
        public static string GetWorkorderNonStockroomParts
        {
            get
            {
                return _getWorkorderNonStockroomParts;
            }

            set
            {
                if (value != _getWorkorderNonStockroomParts)
                {
                    _getWorkorderNonStockroomParts = value;
                }
            }

        }

        private static string _createWorkorderNonStockroomParts = "Inspection/service/CreateWorkOrderNonStockroomParts";
        public static string CreateWorkorderNonStockroomParts
        {
            get
            {
                return _createWorkorderNonStockroomParts;
            }

            set
            {
                if (value != _createWorkorderNonStockroomParts)
                {
                    _createWorkorderNonStockroomParts = value;
                }
            }

        }
        private static string _removeTool = "Inspection/service/RemoveWorkOrderTool";
        public static string RemoveTool
        {
            get
            {
                return _removeTool;
            }

            set
            {
                if (value != _removeTool)
                {
                    _removeTool = value;
                }
            }

        }

        private static string _getWorkorderNonStockroomPartDetail = "Inspection/service/WorkOrderNonStockroomPartsByWorkOrderNonStockroomPartID";
        public static string GetWorkorderNonStockroomPartDetail
        {
            get
            {
                return _getWorkorderNonStockroomPartDetail;
            }

            set
            {
                if (value != _getWorkorderNonStockroomPartDetail)
                {
                    _getWorkorderNonStockroomPartDetail = value;
                }
            }

        }
        private static string _editWorkorderNonStockroomParts = "Inspection/service/UpdateWorkOrderNonStockroomParts";
        public static string EditWorkorderNonStockroomParts
        {
            get
            {
                return _editWorkorderNonStockroomParts;
            }

            set
            {
                if (value != _editWorkorderNonStockroomParts)
                {
                    _editWorkorderNonStockroomParts = value;
                }
            }

        }


        private static string _createWorkorderStockroomParts = "Inspection/service/CreateWorkOrderStockroomParts";
        public static string CreateWorkorderStockroomParts
        {
            get
            {
                return _createWorkorderStockroomParts;
            }

            set
            {
                if (value != _createWorkorderStockroomParts)
                {
                    _createWorkorderStockroomParts = value;
                }
            }

        }



        #endregion

        #region Attachments


        private static string _getWorkOrderAttachments = "Inspection/service/WorkOrderAttachments";
        public static string GetWorkOrderAttachments
        {
            get
            {
                return _getWorkOrderAttachments;
            }

            set
            {
                if (value != _getWorkOrderAttachments)
                {
                    _getWorkOrderAttachments = value;
                }
            }

        }

        private static string _getServiceRequestAttachments = "Inspection/service/ServiceRequestAttachments";
        public static string GetServiceRequestAttachments
        {
            get
            {
                return _getServiceRequestAttachments;
            }

            set
            {
                if (value != _getServiceRequestAttachments)
                {
                    _getServiceRequestAttachments = value;
                }
            }

        }
        private static string _closedWorkorderAttachmentByClosedWorkorderID = "Inspection/service/ClosedWorkOrdersAttachmentsByClosedWorkOrderID";
        public static string ClosedWorkorderAttachmentByClosedWorkorderID
        {
            get
            {
                return _closedWorkorderAttachmentByClosedWorkorderID;
            }

            set
            {
                if (value != _closedWorkorderAttachmentByClosedWorkorderID)
                {
                    _closedWorkorderAttachmentByClosedWorkorderID = value;
                }
            }

        }
        
        private static string _deleteWorkOrderAttachments = "Inspection/service/RemoveWorkOrderAttachment";
        public static string DeleteWorkOrderAttachments
        {
            get
            {
                return _deleteWorkOrderAttachments;
            }

            set
            {
                if (value != _deleteWorkOrderAttachments)
                {
                    _deleteWorkOrderAttachments = value;
                }
            }

        }
        private static string _deleteServiceRequestAttachments = "Inspection/service/RemoveServiceRequestAttachment";
        public static string DeleteServiceRequestAttachments
        {
            get
            {
                return _deleteServiceRequestAttachments;
            }

            set
            {
                if (value != _deleteServiceRequestAttachments)
                {
                    _deleteServiceRequestAttachments = value;
                }
            }

        }
        
        private static string _createWorkOrderAttachments = "Inspection/service/CreateWorkOrderAttachment";
        public static string CreateWorkOrderAttachments
        {
            get
            {
                return _createWorkOrderAttachments;
            }

            set
            {
                if (value != _createWorkOrderAttachments)
                {
                    _createWorkOrderAttachments = value;
                }
            }

        }
        private static string _createServiceRequestAttachments = "Inspection/service/CreateServiceRequestAttachment";
        public static string CreateServiceRequestAttachments
        {
            get
            {
                return _createServiceRequestAttachments;
            }

            set
            {
                if (value != _createServiceRequestAttachments)
                {
                    _createServiceRequestAttachments = value;
                }
            }

        }
        

        #endregion
        #endregion
        #region Assets




        private static string _getAssets = "Inspection/service/Assets";
        public static string GetAssets
        {
            get
            {
                return _getAssets;
            }

            set
            {
                if (value != _getAssets)
                {
                    _getAssets = value;
                }
            }

        }

        private static string _assetsbyassestsystem = "Inspection/service/AssetsByAssetSystemID";
        public static string AssetsByAssetSystem
        {
            get
            {
                return _assetsbyassestsystem;
            }

            set
            {
                if (value != _assetsbyassestsystem)
                {
                    _assetsbyassestsystem = value;
                }
            }

        }

        private static string _getAssetsBYAssetID = "Inspection/service/AssetByAssetID";
        public static string GetAssetsBYAssetID
        {
            get
            {
                return _getAssetsBYAssetID;
            }

            set
            {
                if (value != _getAssetsBYAssetID)
                {
                    _getAssetsBYAssetID = value;
                }
            }

        }
        private static string _getCategory = "Inspection/service/ComboGetCategory";
        public static string GetCategory
        {
            get
            {
                return _getCategory;
            }

            set
            {
                if (value != _getCategory)
                {
                    _getCategory = value;
                }
            }

        }
        private static string _getVendor = "Inspection/service/ComboGetVendors";
        public static string GetVendor
        {
            get
            {
                return _getVendor;
            }

            set
            {
                if (value != _getVendor)
                {
                    _getVendor = value;
                }
            }

        }
        private static string _getRuntimeUnit = "Inspection/service/ComboGetRuntimeUnits";
        public static string GetRuntimeUnit
        {
            get
            {
                return _getRuntimeUnit;
            }

            set
            {
                if (value != _getRuntimeUnit)
                {
                    _getRuntimeUnit = value;
                }
            }

        }

        private static string _getAssetsFromSearchBar = "Inspection/service/AssetByAssetNumber";
        public static string GetAssetsFromSearchBar
        {
            get
            {
                return _getAssetsFromSearchBar;
            }

            set
            {
                if (value != _getAssetsFromSearchBar)
                {
                    _getAssetsFromSearchBar = value;
                }
            }

        }

        private static string _createAsset = "Inspection/service/CreateAsset";
        public static string CreateAsset
        {
            get
            {
                return _createAsset;
            }

            set
            {
                if (value != _createAsset)
                {
                    _createAsset = value;
                }
            }

        }

        private static string _editAsset = "Inspection/service/UpdateAsset";
        public static string EditAsset
        {
            get
            {
                return _editAsset;
            }

            set
            {
                if (value != _editAsset)
                {
                    _editAsset = value;
                }
            }

        }

        #endregion

        #region PurchaseOrder




        private static string _getPurchaseOrders = "Inspection/Service/PurchaseOrdersAll";
        public static string GetPurchaseOrders
        {
            get
            {
                return _getPurchaseOrders;
            }

            set
            {
                if (value != _getPurchaseOrders)
                {
                    _getPurchaseOrders = value;
                }
            }

        }
        private static string _getReceivers = "Inspection/Service/Receivers";
        public static string GetReceivers
        {
            get
            {
                return _getReceivers;
            }

            set
            {
                if (value != _getReceivers)
                {
                    _getReceivers = value;
                }
            }

        }

        private static string _getPurchaseOrderDetailByRequisitionID = "Inspection/Service/PurchaseOrders";
        public static string GetPurchaseOrderDetailByRequisitionID
        {
            get
            {
                return _getPurchaseOrderDetailByRequisitionID;
            }

            set
            {
                if (value != _getPurchaseOrderDetailByRequisitionID)
                {
                    _getPurchaseOrderDetailByRequisitionID = value;
                }
            }

        }

        private static string _getPurchaseOrdersbyPONumber = "Inspection/Service/PurchaseOrdersByPONumber";
        public static string GetPurchaseOrdersbyPONumber
        {
            get
            {
                return _getPurchaseOrdersbyPONumber;
            }

            set
            {
                if (value != _getPurchaseOrdersbyPONumber)
                {
                    _getPurchaseOrdersbyPONumber = value;
                }
            }

        }

        private static string _receivePurchaseOrderAsset = "Inspection/Service/PurchaseOrderAssetsReceiving";
        public static string ReceivePurchaseOrderAsset
        {
            get
            {
                return _receivePurchaseOrderAsset;
            }

            set
            {
                if (value != _receivePurchaseOrderAsset)
                {
                    _receivePurchaseOrderAsset = value;
                }
            }

        }
        private static string _receivePurchaseOrderNonStockroomPart = "Inspection/Service/PurchaseOrderNonStockroomPartsReceiving";
        public static string ReceivePurchaseOrderNonStockroomPart
        {
            get
            {
                return _receivePurchaseOrderNonStockroomPart;
            }

            set
            {
                if (value != _receivePurchaseOrderNonStockroomPart)
                {
                    _receivePurchaseOrderNonStockroomPart = value;
                }
            }

        }
        private static string _receivePurchaseOrderStockroomPart = "Inspection/Service/PurchaseOrderPartsReceiving";
        public static string ReceivePurchaseOrderStockroomPart
        {
            get
            {
                return _receivePurchaseOrderStockroomPart;
            }

            set
            {
                if (value != _receivePurchaseOrderStockroomPart)
                {
                    _receivePurchaseOrderStockroomPart = value;
                }
            }

        }

        #endregion

        #region ServiceRequest


        

        private static string _getAdministrator = "Inspection/service/GetSRAEmloyees";
        public static string GetAdministrator
        {
            get
            {
                return _getAdministrator;
            }

            set
            {
                if (value != _getAdministrator)
                {
                    _getAdministrator = value;
                }
            }

        }
        private static string _getServiceRequest = "Inspection/service/ServiceRequests";
        public static string GetServiceRequest
        {
            get
            {
                return _getServiceRequest;
            }

            set
            {
                if (value != _getServiceRequest)
                {
                    _getServiceRequest = value;
                }
            }

        }

        private static string _getServiceRequestBYServiceRequestID = "Inspection/service/ServiceRequest";
        public static string GetServiceRequestBYServiceRequestID
        {
            get
            {
                return _getServiceRequestBYServiceRequestID;
            }

            set
            {
                if (value != _getServiceRequestBYServiceRequestID)
                {
                    _getServiceRequestBYServiceRequestID = value;
                }
            }

        }
        private static string _getServiceRequestByRequestNumber = "Inspection/service/ServiceRequestByServiceRequestNumber";
        public static string GetServiceRequestByRequestNumber
        {
            get
            {
                return _getServiceRequestByRequestNumber;
            }

            set
            {
                if (value != _getServiceRequestByRequestNumber)
                {
                    _getServiceRequestByRequestNumber = value;
                }
            }

        }
        private static string _createServiceRequest = "Inspection/service/CreateServiceRequest";
        public static string CreateServiceRequest
        {
            get
            {
                return _createServiceRequest;
            }

            set
            {
                if (value != _createServiceRequest)
                {
                    _createServiceRequest = value;
                }
            }

        }
        private static string _declineServiceRequest = "Inspection/service/DeclineServiceRequest";
        public static string DeclineServiceRequest
        {
            get
            {
                return _declineServiceRequest;
            }

            set
            {
                if (value != _declineServiceRequest)
                {
                    _declineServiceRequest = value;
                }
            }

        }

        private static string _acceptServiceRequest = "Inspection/service/AcceptServiceRequest";
        public static string AcceptServiceRequest
        {
            get
            {
                return _acceptServiceRequest;
            }

            set
            {
                if (value != _acceptServiceRequest)
                {
                    _acceptServiceRequest = value;
                }
            }

        }
        private static string _editServiceRequest = "Inspection/service/UpdateServiceRequest";
        public static string EditServiceRequest
        {
            get
            {
                return _editServiceRequest;
            }

            set
            {
                if (value != _editServiceRequest)
                {
                    _editServiceRequest = value;
                }
            }

        }

        #endregion
        #region Closed Workorder Module

        //ClosedWorkOrdersByAssetNumber
        private static string _closedWorkOrdersByAssetNumber = "Inspection/service/ClosedWorkOrdersByAssetNumber";
        public static string ClosedWorkOrdersByAssetNumber
        {
            get
            {
                return _closedWorkOrdersByAssetNumber;
            }

            set
            {
                if (value != _closedWorkOrdersByAssetNumber)
                {
                    _closedWorkOrdersByAssetNumber = value;
                }
            }

        }

        //ClosedWorkOrdersByWorkOrderNumber
        private static string _closedWorkOrdersByWorkOrderNumber = "Inspection/service/ClosedWorkOrdersByWorkOrderNumber";
        public static string ClosedWorkOrdersByWorkOrderNumber
        {
            get
            {
                return _closedWorkOrdersByWorkOrderNumber;
            }

            set
            {
                if (value != _closedWorkOrdersByWorkOrderNumber)
                {
                    _closedWorkOrdersByWorkOrderNumber = value;
                }
            }

        }

        //ClosedWorkOrdersByLocationName
        private static string _closedWorkOrdersByLocationName = "Inspection/service/ClosedWorkOrdersByLocationName";
        public static string ClosedWorkOrdersByLocationName
        {
            get
            {
                return _closedWorkOrdersByLocationName;
            }

            set
            {
                if (value != _closedWorkOrdersByLocationName)
                {
                    _closedWorkOrdersByLocationName = value;
                }
            }

        }


        //ClosedWorkOrdersByPartNumber
        private static string _closedWorkOrdersByPartNumber = "Inspection/service/ClosedWorkOrdersByPartNumber";
        public static string ClosedWorkOrdersByPartNumber
        {
            get
            {
                return _closedWorkOrdersByPartNumber;
            }

            set
            {
                if (value != _closedWorkOrdersByPartNumber)
                {
                    _closedWorkOrdersByPartNumber = value;
                }
            }

        }


        //ClosedWorkOrdersByClosedWorkOrderDate
        private static string _closedWorkOrdersByClosedWorkOrderDate = "Inspection/service/ClosedWorkOrdersByClosedWorkOrderDate";
        public static string ClosedWorkOrdersByClosedWorkOrderDate
        {
            get
            {
                return _closedWorkOrdersByClosedWorkOrderDate;
            }

            set
            {
                if (value != _closedWorkOrdersByClosedWorkOrderDate)
                {
                    _closedWorkOrdersByClosedWorkOrderDate = value;
                }
            }

        }

        //ClosedWorkOrdersByClosedWorkOrderDate
        private static string _closedWorkOrders = "Inspection/service/ClosedWorkOrders";
        public static string ClosedWorkOrders
        {
            get
            {
                return _closedWorkOrders;
            }

            set
            {
                if (value != _closedWorkOrders)
                {
                    _closedWorkOrders = value;
                }
            }

        }
        
       private static string _getClosedWorkOrdersToolsByClosedWorkorderID = "Inspection/service/ClosedWorkOrdersToolsByClosedWorkOrderID";
        public static string GetClosedWorkOrdersToolsByClosedWorkorderID
        {
            get
            {
                return _getClosedWorkOrdersToolsByClosedWorkorderID;
            }

            set
            {
                if (value != _getClosedWorkOrdersToolsByClosedWorkorderID)
                {
                    _getClosedWorkOrdersToolsByClosedWorkorderID = value;
                }
            }

        }
        private static string _getClosedWorkOrdersStockroomPartsByClosedWorkorderID = "Inspection/service/ClosedWorkOrdersStockroomParts";
        public static string GetClosedWorkOrdersStockroomPartsByClosedWorkorderID
        {
            get
            {
                return _getClosedWorkOrdersStockroomPartsByClosedWorkorderID;
            }

            set
            {
                if (value != _getClosedWorkOrdersStockroomPartsByClosedWorkorderID)
                {
                    _getClosedWorkOrdersStockroomPartsByClosedWorkorderID = value;
                }
            }

        }
        private static string _getClosedWorkOrdersNonStockroomPartsByClosedWorkorderID = "Inspection/service/ClosedWorkOrdersNonStockroomParts";
        public static string GetClosedWorkOrdersNonStockroomPartsByClosedWorkorderID
        {
            get
            {
                return _getClosedWorkOrdersNonStockroomPartsByClosedWorkorderID;
            }

            set
            {
                if (value != _getClosedWorkOrdersNonStockroomPartsByClosedWorkorderID)
                {
                    _getClosedWorkOrdersNonStockroomPartsByClosedWorkorderID = value;
                }
            }

        }
        
        #endregion

        //////////////////////////////////////////////////////////////////////////////////////////
        #region FormNames

        public static string WorkorderDetailFormName
        {
            get => Settings.GetValueOrDefault(nameof(WorkorderDetailFormName), "WorkOrderDetail");

            set => Settings.AddOrUpdateValue(nameof(WorkorderDetailFormName), value);
        }

        #endregion


        //////////////////////////////////////////////////////////////////////////////////////////
        #region Module Names

        public static string WorkorderModuleName
        {
            get => Settings.GetValueOrDefault(nameof(WorkorderModuleName), "Workorders");

            set => Settings.AddOrUpdateValue(nameof(WorkorderModuleName), value);
        }

        public static string DashBoardName
        {
            get => Settings.GetValueOrDefault(nameof(DashBoardName), "dashboard");

            set => Settings.AddOrUpdateValue(nameof(DashBoardName), value);
        }

        #endregion

        public static string AssetModuleName
        {
            get => Settings.GetValueOrDefault(nameof(AssetModuleName), "Assets");

            set => Settings.AddOrUpdateValue(nameof(AssetModuleName), value);
        }

        public static string ServiceRequestModuleName
        {
            get => Settings.GetValueOrDefault(nameof(ServiceRequestModuleName), "ServiceRequests");

            set => Settings.AddOrUpdateValue(nameof(ServiceRequestModuleName), value);
        }

        public static string BarcodeModuleName
        {
            get => Settings.GetValueOrDefault(nameof(BarcodeModuleName), "Barcodefunctions");

            set => Settings.AddOrUpdateValue(nameof(BarcodeModuleName), value);
        }

        public static string ReceivingModuleName
        {
            get => Settings.GetValueOrDefault(nameof(ReceivingModuleName), "Purchasing");

            set => Settings.AddOrUpdateValue(nameof(ReceivingModuleName), value);
        }
        public static string InventoryModuleName
        {
            get => Settings.GetValueOrDefault(nameof(InventoryModuleName), "Inventory");

            set => Settings.AddOrUpdateValue(nameof(InventoryModuleName), value);
        }
        /////////////////////////////////////////////////////////////////////////////////////////

        #region Selection List Page Endpoints

        private static string _getComboListFacilitiesFacilityWise = "Inspection/service/ComboListFacilitiesFacilityWise";
        public static string GetComboListFacilitiesFacilityWise
        {
            get
            {
                return _getComboListFacilitiesFacilityWise;
            }

            set
            {
                if (value != _getComboListFacilitiesFacilityWise)
                {
                    _getComboListFacilitiesFacilityWise = value;
                }
            }

        }

        private static string _getTargetByFacility = "Inspection/service/TargetByFacility";
        public static string GetTargetByFacility
        {
            get
            {
                return _getTargetByFacility;
            }

            set
            {
                if (value != _getTargetByFacility)
                {
                    _getTargetByFacility = value;
                }
            }

        }
        private static string _getAssetSystemForAsset = "Inspection/service/ComboGetAssetSystemByUser";
        public static string GetAssetSystemForAsset
        {
            get
            {
                return _getAssetSystemForAsset;
            }

            set
            {
                if (value != _getAssetSystemForAsset)
                {
                    _getAssetSystemForAsset = value;
                }
            }

        }


        private static string _getTargetByFacilityAndLocation = "Inspection/service/TargetByFacilityAndLocation";
        public static string GetTargetByFacilityAndLocation
        {
            get
            {
                return _getTargetByFacilityAndLocation;
            }

            set
            {
                if (value != _getTargetByFacilityAndLocation)
                {
                    _getTargetByFacilityAndLocation = value;
                }
            }

        }

        private static string _getAssignedToEmployee = "Inspection/service/ComboListemployeesAssignedToFacilityWise";
        public static string GetAssignedToEmployee
        {
            get
            {
                return _getAssignedToEmployee;
            }

            set
            {
                if (value != _getAssignedToEmployee)
                {
                    _getAssignedToEmployee = value;
                }
            }

        }

        private static string _getWorkorderRequester = "Inspection/service/ComboListRequesterFacilityWise";
        public static string GetWorkorderRequester
        {
            get
            {
                return _getWorkorderRequester;
            }

            set
            {
                if (value != _getWorkorderRequester)
                {
                    _getWorkorderRequester = value;
                }
            }

        }

        private static string _getCostCenters = "Inspection/service/ComboGetCostCenter";
        public static string GetCostCenters
        {
            get
            {
                return _getCostCenters;
            }

            set
            {
                if (value != _getCostCenters)
                {
                    _getCostCenters = value;
                }
            }

        }

        private static string _getPriorities = "Inspection/service/ComboListPriorities";
        public static string GetPriorities
        {
            get
            {
                return _getPriorities;
            }

            set
            {
                if (value != _getPriorities)
                {
                    _getPriorities = value;
                }
            }

        }


        private static string _getShifts = "Inspection/service/ComboListShifts";
        public static string GetShifts
        {
            get
            {
                return _getShifts;
            }

            set
            {
                if (value != _getShifts)
                {
                    _getShifts = value;
                }
            }

        }

        private static string _getWorkOrderStatus = "Inspection/service/ComboListWorkOrderStatus";
        public static string GetWorkOrderStatus
        {
            get
            {
                return _getWorkOrderStatus;
            }

            set
            {
                if (value != _getWorkOrderStatus)
                {
                    _getWorkOrderStatus = value;
                }
            }

        }

        private static string _getWorkOrderType = "Inspection/service/ComboListWorkType";
        public static string GetWorkOrderType
        {
            get
            {
                return _getWorkOrderType;
            }

            set
            {
                if (value != _getWorkOrderType)
                {
                    _getWorkOrderType = value;
                }
            }

        }


        private static string _getCause = "Inspection/service/GetAllCauses";
        public static string GetCause
        {
            get
            {
                return _getCause;
            }

            set
            {
                if (value != _getCause)
                {
                    _getCause = value;
                }
            }

        }
        private static string _getDefaultEmployee = "Inspection/service/GetDefaultEmployee";
        public static string GetDefaultEmployee
        {
            get
            {
                return _getDefaultEmployee;
            }

            set
            {
                if (value != _getDefaultEmployee)
                {
                    _getDefaultEmployee = value;
                }
            }

        }

        private static string _getMaintenanceCodes = "Inspection/service/ComboListMaintenanceCodes";
        public static string GetMaintenanceCodes
        {
            get
            {
                return _getMaintenanceCodes;
            }

            set
            {
                if (value != _getMaintenanceCodes)
                {
                    _getMaintenanceCodes = value;
                }
            }

        }

        #region Workorder

        #region TaskAndLabour


        private static string _getWorkOrderLaborLookUp = "Inspection/service/WorkOrderLaborLookUpsData";
        public static string GetWorkOrderLaborLookUp
        {
            get
            {
                return _getWorkOrderLaborLookUp;
            }

            set
            {
                if (value != _getWorkOrderLaborLookUp)
                {
                    _getWorkOrderLaborLookUp = value;
                }
            }

        }

        #endregion

        #endregion

        #endregion
    }
}
