using Autofac;
using ProteusMMX.Services.Asset;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.Barcode;
using ProteusMMX.Services.CloseWorkorder;
using ProteusMMX.Services.Connectivity;
using ProteusMMX.Services.Dialog;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Inventory;
using ProteusMMX.Services.Navigation;
using ProteusMMX.Services.PurchaseOrder;
using ProteusMMX.Services.Request;
using ProteusMMX.Services.SelectionListPageServices;
using ProteusMMX.Services.SelectionListPageServices.Asset;
using ProteusMMX.Services.SelectionListPageServices.AssetSystem;
using ProteusMMX.Services.SelectionListPageServices.AssignTo;
using ProteusMMX.Services.SelectionListPageServices.Cause;
using ProteusMMX.Services.SelectionListPageServices.CostCenter;
using ProteusMMX.Services.SelectionListPageServices.Location;
using ProteusMMX.Services.SelectionListPageServices.MaintenanceCode;
using ProteusMMX.Services.SelectionListPageServices.Parts;
using ProteusMMX.Services.SelectionListPageServices.Priority;
using ProteusMMX.Services.SelectionListPageServices.Shift;
using ProteusMMX.Services.SelectionListPageServices.Stockroom;
using ProteusMMX.Services.SelectionListPageServices.Tool;
using ProteusMMX.Services.SelectionListPageServices.Workorder.TaskAndLabour;
using ProteusMMX.Services.SelectionListPageServices.WorkorderRequester;
using ProteusMMX.Services.SelectionListPageServices.WorkorderStatus;
using ProteusMMX.Services.SelectionListPageServices.WorkorderType;
using ProteusMMX.Services.ServiceRequest;
using ProteusMMX.Services.Translations;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Services.Workorder.Attachments;
using ProteusMMX.Services.Workorder.Inspection;
using ProteusMMX.Services.Workorder.TaskAndLabour;
using ProteusMMX.ViewModel.Asset;
using ProteusMMX.ViewModel.Barcode;
using ProteusMMX.ViewModel.ClosedWorkorder;
using ProteusMMX.ViewModel.Inventory;
using ProteusMMX.ViewModel.PurchaseOrder;
using ProteusMMX.ViewModel.SelectionListPagesViewModels;
using ProteusMMX.ViewModel.SelectionListPagesViewModels.Asset;
using ProteusMMX.ViewModel.SelectionListPagesViewModels.Inventory;
using ProteusMMX.ViewModel.SelectionListPagesViewModels.PurchaseOrder;
using ProteusMMX.ViewModel.SelectionListPagesViewModels.Workorder.Parts;
using ProteusMMX.ViewModel.SelectionListPagesViewModels.Workorder.TaskAndLabour;
using ProteusMMX.ViewModel.SelectionListPagesViewModels.Workorder.Tools;
using ProteusMMX.ViewModel.ServiceRequest;
using ProteusMMX.ViewModel.Workorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProteusMMX.ViewModel
{
    public class Locator
    {

        private IContainer _container;
        private ContainerBuilder _containerBuilder;

        private static readonly Locator _instance = new Locator();

        public static Locator Instance
        {
            get
            {
                return _instance;
            }
        }

        public Locator()
        {
            //Register Singltons

            _containerBuilder = new ContainerBuilder();
            _containerBuilder.RegisterType<ConnectivityService>().As<IConnectivityService>();
            _containerBuilder.RegisterType<RequestService>().As<IRequestService>();
            _containerBuilder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            _containerBuilder.RegisterType<NavigationService>().As<INavigationService>();
            _containerBuilder.RegisterType<DialogService>().As<IDialogService>();
            _containerBuilder.RegisterType<FormLoadInputService>().As<IFormLoadInputService>();
            _containerBuilder.RegisterType<WebControlTitlesService>().As<IWebControlTitlesService>();
            _containerBuilder.RegisterType<BarcodeService>().As<IBarcodeService>();
            _containerBuilder.RegisterType<WorkorderService>().As<IWorkorderService>();
            _containerBuilder.RegisterType<TaskAndLabourService>().As<ITaskAndLabourService>();
            _containerBuilder.RegisterType<CloseWorkorderService>().As<ICloseWorkorderService>();



            _containerBuilder.RegisterType<ViewModelBase>();
            _containerBuilder.RegisterType<ExtendedSplashViewModel>();
            _containerBuilder.RegisterType<LoginPageViewModel>();
            _containerBuilder.RegisterType<DashboardPageViewModel>();
            _containerBuilder.RegisterType<WorkorderListingPageViewModel>();
            _containerBuilder.RegisterType<CreateWorkorderPageViewModel>();
            _containerBuilder.RegisterType<WorkorderTabbedPageViewModel>();
            _containerBuilder.RegisterType<EditWorkorderPageViewModel>();
            _containerBuilder.RegisterType<TaskAndLabourPageViewModel>();
            _containerBuilder.RegisterType<CreateTaskPageViewModel>();
            _containerBuilder.RegisterType<WorkOrderStockroomPartsListingPageViewModel>();
            _containerBuilder.RegisterType<WorkorderToolListingPageViewModel>();
            _containerBuilder.RegisterType<AddNewToolViewModel>();
            _containerBuilder.RegisterType<WorkOrderNonStockroomPartsListingPageViewModel>();
            _containerBuilder.RegisterType<CreateNonStockroomPartsPageViewModel>();
            _containerBuilder.RegisterType<EditNonStockroomPartsPageViewModel>();
            _containerBuilder.RegisterType<CreateWorkOrderStockroomPartsViewModel>();
            _containerBuilder.RegisterType<EditWorkOrderStockroomPartsViewModel>();
            _containerBuilder.RegisterType<AttachmentsPageViewModel>();
            _containerBuilder.RegisterType<AssetListingPageViewModel>();
            _containerBuilder.RegisterType<ServiceRequestListingPageViewModel>();
            _containerBuilder.RegisterType<StockroomListingPageViewModel>();
            _containerBuilder.RegisterType<PurchaseorderListingPageViewModel>();
            _containerBuilder.RegisterType<InspectionPageViewModel>();
            _containerBuilder.RegisterType<PartListingPageViewModel>();
            _containerBuilder.RegisterType<InventoryTransactionPageViewModel>();
            _containerBuilder.RegisterType<CreateNewAssetPageViewModel>();
            _containerBuilder.RegisterType<EditAssetPageViewModel>();
            _containerBuilder.RegisterType<ServiceRequestAttachmentPageViewModel>();
            _containerBuilder.RegisterType<ServiceRequestTabbedPageViewModel>();
            _containerBuilder.RegisterType<CreateServiceRequestViewModel>();
            _containerBuilder.RegisterType<EditServiceRequestViewModel>();
            _containerBuilder.RegisterType<PuchaseOrderTabbedPageViewModel>();
            _containerBuilder.RegisterType<PurchaseOrderAssetsListingPageViewModel>();
            _containerBuilder.RegisterType<PurchaseOrderNonStockroomPartsListingPageViewModel>();
            _containerBuilder.RegisterType<PurchaseOrderPartsListingPageViewModel>();

            _containerBuilder.RegisterType<ReceivePuchaseOrderAssetViewModel>();
            _containerBuilder.RegisterType<ReceivePuchaseOrderNonStockroomPartViewModel>();
            _containerBuilder.RegisterType<ReceivePuchaseOrderStockroomPartViewModel>();
            _containerBuilder.RegisterType<BarcodeDashboardViewModel>();
            _containerBuilder.RegisterType<SearchClosedWorkorderPageViewModel>();
            _containerBuilder.RegisterType<ClosedWorkorderListingPageViewModel>();
            _containerBuilder.RegisterType<ClosedWorkorderTabbedPageViewModel>();
            _containerBuilder.RegisterType<ClosedWorkorderDetailsPageViewModel>();
            _containerBuilder.RegisterType<ClosedWorkorderTaskAndLabourPageViewModel>();
            _containerBuilder.RegisterType<ClosedWorkorderToolsPageViewModel>();
            _containerBuilder.RegisterType<ClosedWorkorderStockroomPartsViewModel>();
            _containerBuilder.RegisterType<ClosedWorkorderNonStockroomPartsViewModel>();
            _containerBuilder.RegisterType<ClosedWorkorderAttachmentsViewModel>();
            _containerBuilder.RegisterType<ClosedWorkorderInspectionViewModel>();

            //Service Request///
            _containerBuilder.RegisterType<ServiceRequestTabbedPageViewModel>();
            _containerBuilder.RegisterType<EditServiceRequestViewModel>();
            _containerBuilder.RegisterType<ServiceRequestAttachmentPageViewModel>();

            _containerBuilder.RegisterType<SearchAssetByAssetNumberViewModel>();
            _containerBuilder.RegisterType<SearchWorkorderByAssetNumberViewModel>();
            _containerBuilder.RegisterType<SearchWorkorderByLocationViewModel>();
            _containerBuilder.RegisterType<SearchAssetForBillOfMaterialViewModel>();
            _containerBuilder.RegisterType<SearchAssetForAttachmentViewModel>();
            _containerBuilder.RegisterType<PurchaseOrderShelfBinListSelectionPageViewModel>();

            _containerBuilder.RegisterType<InventoryPerformByListSelectionPageViewModel>();
            _containerBuilder.RegisterType<InventoryCheckoutTOListSelectionPageViewModel>();
            _containerBuilder.RegisterType<RiskQuestionPageViewModel>();
            _containerBuilder.RegisterType<DescriptionViewModel>();
            _containerBuilder.RegisterType<SignatureHistoryViewModel>();
            _containerBuilder.RegisterType<ClosedWorkorderStockroomPartsViewModelForIOS>();
            _containerBuilder.RegisterType<WorkOrderStockroomPartsListingPageViewModelForIOS>();
            _containerBuilder.RegisterType<SearchWorkorderByLocationFromBarcodeViewModel>();
            _containerBuilder.RegisterType<SearchWorkorderByAssetNumberFromBarcodeViewModel>();
            _containerBuilder.RegisterType<SearchAssetFromBarcodeViewModel>();
            
            
            #region Register service for list Pages

            _containerBuilder.RegisterType<FacilityService>().As<IFacilityService>();
            _containerBuilder.RegisterType<LocationService>().As<ILocationService>();
            _containerBuilder.RegisterType<AssetService>().As<IAssetService>();
            _containerBuilder.RegisterType<AssetSystemService>().As<IAssetSystemService>();
            _containerBuilder.RegisterType<AssignToService>().As<IAssignToService>();
            _containerBuilder.RegisterType<WorkorderRequesterService>().As<IWorkorderRequesterService>();
            _containerBuilder.RegisterType<CostCenterService>().As<ICostCenterService>();
            _containerBuilder.RegisterType<PriorityService>().As<IPriorityService>();
            _containerBuilder.RegisterType<ShiftService>().As<IShiftService>();
            _containerBuilder.RegisterType<WorkorderStatusService>().As<IWorkorderStatusService>();
            _containerBuilder.RegisterType<WorkorderTypeService>().As<IWorkorderTypeService>();
            _containerBuilder.RegisterType<CauseService>().As<ICauseService>();
            _containerBuilder.RegisterType<MaintenanceCodeService>().As<IMaintenanceCodeService>();

            #region Workorder
            _containerBuilder.RegisterType<TaskService>().As<ITaskService>();
            _containerBuilder.RegisterType<ToolService>().As<IToolService>();
            _containerBuilder.RegisterType<PartService>().As<IPartService>();
            _containerBuilder.RegisterType<StockroomService>().As<IStockroomService>();
            _containerBuilder.RegisterType<AttachmentService>().As<IAttachmentService>();
            _containerBuilder.RegisterType<AssetModuleService>().As<IAssetModuleService>();
            _containerBuilder.RegisterType<ServiceRequestModuleService>().As<IServiceRequestModuleService>();
            _containerBuilder.RegisterType<InventoryService>().As<IInventoryService>();
            _containerBuilder.RegisterType<PurchaseOrderService>().As<IPurchaseOrderService>();
            _containerBuilder.RegisterType<InspectionService>().As<IInspectionService>();
            #region Task And Labour


            #endregion

            #endregion

            #endregion



            #region Register Viewmodels for list Pages

            _containerBuilder.RegisterType<FacilityListSelectionPageViewModel>();
            _containerBuilder.RegisterType<LocationListSelectionPageViewModel>();
            _containerBuilder.RegisterType<AssetListSelectionPageViewModel>();
            _containerBuilder.RegisterType<AssetSystemListSelectionPageViewModel>();
            _containerBuilder.RegisterType<AssignToListSelectionPageViewModel>();
            _containerBuilder.RegisterType<WorkorderRequesterListSelectionPageViewModel>();
            _containerBuilder.RegisterType<CostCenterListSelectionPageViewModel>();
            _containerBuilder.RegisterType<PriorityListSelectionPageViewModel>();
            _containerBuilder.RegisterType<ShiftListSelectionPageViewModel>();
            _containerBuilder.RegisterType<WorkorderStatusListSelectionPageViewModel>();
            _containerBuilder.RegisterType<WorkorderTypeListSelectionPageViewModel>();
            _containerBuilder.RegisterType<CauseListSelectionPageViewModel>();
            _containerBuilder.RegisterType<MaintenanceCodeListSelectionPageViewModel>();

            #region Workorder

            #region Task And Labour


            _containerBuilder.RegisterType<TaskListSelectionPageViewModel>();
            _containerBuilder.RegisterType<EmployeeListSelectionPageViewModel>();
            _containerBuilder.RegisterType<ContractorListSelectionPageViewModel>();


            #endregion

            #region Tools


            _containerBuilder.RegisterType<ToolCribListSelectionPageViewModel>();
            _containerBuilder.RegisterType<ToolNumberListSelectionPageViewModel>();


            #endregion

            #region Parts


            _containerBuilder.RegisterType<PartListSelectionPageViewModel>();
            _containerBuilder.RegisterType<StockroomListSelectionPageViewModel>();
            _containerBuilder.RegisterType<ShelfBinListSelectionPageViewModel>();


            #endregion
            #endregion

            #endregion
            #region Inventory

            _containerBuilder.RegisterType<InventoryCostCenterListSelectionPageViewModel>();
            _containerBuilder.RegisterType<InventoryShelfBinListSelectionPageViewModel>();
            _containerBuilder.RegisterType<TransactionReasonListSelectionPageViewModel>();
            _containerBuilder.RegisterType<TransactionTypeListSelectionPageViewModel>();



            #endregion

            #region Asset

            _containerBuilder.RegisterType<CategoryListSelectionPageViewModel>();
            _containerBuilder.RegisterType<VendorListSelectionPageViewModel>();
            _containerBuilder.RegisterType<RuntimeUnitListSelectionPageViewModel>();



            #endregion

            #region Receiving

            _containerBuilder.RegisterType<ReceiverListSelectionPageViewModel>();

            #endregion
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface
        {
            _containerBuilder.RegisterType<TImplementation>().As<TInterface>();
        }

        public void Register<T>() where T : class
        {
            _containerBuilder.RegisterType<T>();
        }

        public void Build()
        {
            _container = _containerBuilder.Build();
        }
    }

}
