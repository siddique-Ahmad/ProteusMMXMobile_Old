using Acr.UserDialogs;

using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using PCLStorage;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using ProteusMMX.Constants;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.DateTime;
using ProteusMMX.Helpers.Storage;
using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.Barcode;
using ProteusMMX.Services.Dialog;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Asset;
using ProteusMMX.ViewModel.Barcode;
using ProteusMMX.ViewModel.ClosedWorkorder;
using ProteusMMX.ViewModel.Inventory;
using ProteusMMX.ViewModel.KPIDashboard;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.PurchaseOrder;
using ProteusMMX.ViewModel.SelectionListPagesViewModels;
using ProteusMMX.ViewModel.ServiceRequest;
using ProteusMMX.ViewModel.Workorder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace ProteusMMX.ViewModel
{
    public class DashboardPageViewModel : ViewModelBase
    {

        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly IWorkorderService _workorderService;


        #endregion

        #region Properties

        #region Page Properties

        ServiceOutput _formControlsAndRights;
        public ServiceOutput FormControlsAndRights
        {
            get { return _formControlsAndRights; }
            set
            {
                if (value != _formControlsAndRights)
                {
                    _formControlsAndRights = value;
                    OnPropertyChanged(nameof(FormControlsAndRights));
                }
            }
        }
        string _pageTitle = "";
        public string PageTitle
        {
            get
            {
                return _pageTitle;
            }

            set
            {
                if (value != _pageTitle)
                {
                    _pageTitle = value;
                    OnPropertyChanged("PageTitle");
                }
            }
        }

        string _userID = AppSettings.User.UserID.ToString();

        public string UserID
        {
            get
            {
                return _userID;
            }

            set
            {
                if (value != _userID)
                {
                    _userID = value;
                    OnPropertyChanged("UserID");
                }
            }
        }

        static string Year = DateTime.Parse(DateTime.Now.ToString()).Year.ToString();
        string _copyrightLabel = "Copyright ©" + Year + " Eagle Technology Inc.";
        public string CopyrightLabel
        {
            get
            {
                return _copyrightLabel;
            }

            set
            {
                if (value != _copyrightLabel)
                {
                    _copyrightLabel = value;
                    OnPropertyChanged("CopyrightLabel");
                }
            }
        }

        string _supportLabel = "Make sure to have latest MMX mobile service. Questions? Email/Call Tech support.";
        public string SupportLabel
        {
            get
            {
                return _supportLabel;
            }

            set
            {
                if (value != _supportLabel)
                {
                    _supportLabel = value;
                    OnPropertyChanged("SupportLabel");
                }
            }
        }

        string _aPIVersion = "App: v" + AppSettings.APPVersion + " " + "API: v" + AppSettings.APIVersion;
        public string APIVersion
        {
            get
            {
                return _aPIVersion;
            }

            set
            {
                if (value != _aPIVersion)
                {
                    _aPIVersion = value;
                    OnPropertyChanged("APIVersion");
                }
            }
        }

        #endregion

        #region Title Properties

        string _workorderTitle;
        public string WorkorderTitle
        {
            get
            {
                return _workorderTitle;
            }

            set
            {
                if (value != _workorderTitle)
                {
                    _workorderTitle = value;
                    OnPropertyChanged("WorkorderTitle");
                }
            }
        }

        string _closeWorkorderTitle;
        public string CloseWorkorderTitle
        {
            get
            {
                return _closeWorkorderTitle;
            }

            set
            {
                if (value != _closeWorkorderTitle)
                {
                    _closeWorkorderTitle = value;
                    OnPropertyChanged("CloseWorkorderTitle");
                }
            }
        }

        string _inventoryTransactionTitle;
        public string InventoryTransactionTitle
        {
            get
            {
                return _inventoryTransactionTitle;
            }

            set
            {
                if (value != _inventoryTransactionTitle)
                {
                    _inventoryTransactionTitle = value;
                    OnPropertyChanged("InventoryTransactionTitle");
                }
            }
        }

        string _serviceRequestTitle;
        public string ServiceRequestTitle
        {
            get
            {
                return _serviceRequestTitle;
            }

            set
            {
                if (value != _serviceRequestTitle)
                {
                    _serviceRequestTitle = value;
                    OnPropertyChanged("ServiceRequestTitle");
                }
            }
        }

        string _assetsTitle;
        public string AssetsTitle
        {
            get
            {
                return _assetsTitle;
            }

            set
            {
                if (value != _assetsTitle)
                {
                    _assetsTitle = value;
                    OnPropertyChanged("AssetsTitle");
                }
            }
        }

        string _barcodeTitle;
        public string BarcodeTitle
        {
            get
            {
                return _barcodeTitle;
            }

            set
            {
                if (value != _barcodeTitle)
                {
                    _barcodeTitle = value;
                    OnPropertyChanged("BarcodeTitle");
                }
            }
        }

        string _receivingTitle;
        public string ReceivingTitle
        {
            get
            {
                return _receivingTitle;
            }

            set
            {
                if (value != _receivingTitle)
                {
                    _receivingTitle = value;
                    OnPropertyChanged("ReceivingTitle");
                }
            }
        }

        string _welcomeTextTitle;
        public string WelcomeTextTitle
        {
            get
            {
                return _welcomeTextTitle;
            }

            set
            {
                if (value != _welcomeTextTitle)
                {
                    _welcomeTextTitle = value;
                    OnPropertyChanged("WelcomeTextTitle");
                }
            }
        }


        #endregion

        #region Dialog Actions Titles

        string _logoutTitle = "";
        public string LogoutTitle
        {
            get
            {
                return _logoutTitle;
            }

            set
            {
                if (value != _logoutTitle)
                {
                    _logoutTitle = value;
                    OnPropertyChanged("LogoutTitle");
                }
            }
        }

        string _selectTitle = "";
        public string SelectTitle
        {
            get
            {
                return _selectTitle;
            }

            set
            {
                if (value != _selectTitle)
                {
                    _selectTitle = value;
                    OnPropertyChanged("SelectTitle");
                }
            }
        }

        string _cancelTitle = "";
        public string CancelTitle
        {
            get
            {
                return _cancelTitle;
            }

            set
            {
                if (value != _cancelTitle)
                {
                    _cancelTitle = value;
                    OnPropertyChanged("CancelTitle");
                }
            }
        }

        string _selectOptionsTitle;
        public string SelectOptionsTitle
        {
            get
            {
                return _selectOptionsTitle;
            }

            set
            {
                if (value != _selectOptionsTitle)
                {
                    _selectOptionsTitle = value;
                    OnPropertyChanged("SelectOptionsTitle");
                }
            }
        }

        #endregion

        #region Module Visibility Properties


        bool _workorderVisibility;
        public bool WorkorderVisibility
        {
            get
            {
                return _workorderVisibility;
            }

            set
            {
                if (value != _workorderVisibility)
                {
                    _workorderVisibility = value;
                    OnPropertyChanged("WorkorderVisibility");
                }
            }
        }
        bool _workorderBorderVisibility = true;
        public bool WorkorderBorderVisibility
        {
            get
            {
                return _workorderBorderVisibility;
            }

            set
            {
                if (value != _workorderBorderVisibility)
                {
                    _workorderBorderVisibility = value;
                    OnPropertyChanged("WorkorderBorderVisibility");
                }
            }
        }

        bool _closeWorkorderVisibility;
        public bool CloseWorkorderVisibility
        {
            get
            {
                return _closeWorkorderVisibility;
            }

            set
            {
                if (value != _closeWorkorderVisibility)
                {
                    _closeWorkorderVisibility = value;
                    OnPropertyChanged("CloseWorkorderVisibility");
                }
            }
        }
        bool _closedWorkorderBorderVisibility;
        public bool ClosedWorkorderBorderVisibility
        {
            get
            {
                return _closedWorkorderBorderVisibility;
            }

            set
            {
                if (value != _closedWorkorderBorderVisibility)
                {
                    _closedWorkorderBorderVisibility = value;
                    OnPropertyChanged("ClosedWorkorderBorderVisibility");
                }
            }
        }

        bool _inventoryTransactionVisibility;
        public bool InventoryTransactionVisibility
        {
            get
            {
                return _inventoryTransactionVisibility;
            }

            set
            {
                if (value != _inventoryTransactionVisibility)
                {
                    _inventoryTransactionVisibility = value;
                    OnPropertyChanged("InventoryTransactionVisibility");
                }
            }
        }
        bool _inventoryBorderVisibility;
        public bool InventoryBorderVisibility
        {
            get
            {
                return _inventoryBorderVisibility;
            }

            set
            {
                if (value != _inventoryBorderVisibility)
                {
                    _inventoryBorderVisibility = value;
                    OnPropertyChanged("InventoryBorderVisibility");
                }
            }
        }

        bool _serviceRequestVisibility;
        public bool ServiceRequestVisibility
        {
            get
            {
                return _serviceRequestVisibility;
            }

            set
            {
                if (value != _serviceRequestVisibility)
                {
                    _serviceRequestVisibility = value;
                    OnPropertyChanged("ServiceRequestVisibility");
                }
            }
        }
        bool _serviceRequestBorderVisibility;
        public bool ServiceRequestBorderVisibility
        {
            get
            {
                return _serviceRequestBorderVisibility;
            }

            set
            {
                if (value != _serviceRequestBorderVisibility)
                {
                    _serviceRequestBorderVisibility = value;
                    OnPropertyChanged("ServiceRequestBorderVisibility");
                }
            }
        }

        bool _assetVisibility;
        public bool AssetVisibility
        {
            get
            {
                return _assetVisibility;
            }

            set
            {
                if (value != _assetVisibility)
                {
                    _assetVisibility = value;
                    OnPropertyChanged("AssetVisibility");
                }
            }
        }

        bool _assetBorderVisibility;
        public bool AssetBorderVisibility
        {
            get
            {
                return _assetBorderVisibility;
            }

            set
            {
                if (value != _assetBorderVisibility)
                {
                    _assetBorderVisibility = value;
                    OnPropertyChanged("AssetBorderVisibility");
                }
            }
        }

        bool _barcodeVisibility;
        public bool BarcodeVisibility
        {
            get
            {
                return _barcodeVisibility;
            }

            set
            {
                if (value != _barcodeVisibility)
                {
                    _barcodeVisibility = value;
                    OnPropertyChanged("BarcodeVisibility");
                }
            }
        }
        bool _barcodeBorderVisibility;
        public bool BarcodeBorderVisibility
        {
            get
            {
                return _barcodeBorderVisibility;
            }

            set
            {
                if (value != _barcodeBorderVisibility)
                {
                    _barcodeBorderVisibility = value;
                    OnPropertyChanged("BarcodeBorderVisibility");
                }
            }
        }

        bool _receivingVisibility;
        public bool ReceivingVisibility
        {
            get
            {
                return _receivingVisibility;
            }

            set
            {
                if (value != _receivingVisibility)
                {
                    _receivingVisibility = value;
                    OnPropertyChanged("ReceivingVisibility");
                }
            }
        }
        bool _receivingBorderVisibility;
        public bool ReceivingBorderVisibility
        {
            get
            {
                return _receivingBorderVisibility;
            }

            set
            {
                if (value != _receivingBorderVisibility)
                {
                    _receivingBorderVisibility = value;
                    OnPropertyChanged("ReceivingBorderVisibility");
                }
            }
        }

        #endregion


        #endregion

        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);
        public ICommand WorkordersCommand => new AsyncCommand(ShowWorkorders);

        public ICommand KPICommand => new AsyncCommand(ShowKPIDashboard);
        public ICommand CloseWorkordersCommand => new AsyncCommand(ShowCloseWorkorders);
        public ICommand InventoryCommand => new AsyncCommand(ShowInventory);
        public ICommand ServiceRequestCommand => new AsyncCommand(ShowServiceRequest);
        public ICommand AssetCommand => new AsyncCommand(ShowAsset);
        public ICommand BarcodeCommand => new AsyncCommand(ShowBarcode);
        public ICommand ReceivingCommand => new AsyncCommand(ShowReceiving);
        #endregion
        String folderName = "ProteusMMXDasebord";

        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                var IsExitPCLDetails = Helpers.PCLStorage.PCLCreateFIle("Details");
                var IsExitPCLTasks = Helpers.PCLStorage.PCLCreateFIle("Tasks");
                var IsExitPCLInspections = Helpers.PCLStorage.PCLCreateFIle("Inspections");
                var IsExitPCLTools = Helpers.PCLStorage.PCLCreateFIle("Tools");
                var IsExitPCLParts = Helpers.PCLStorage.PCLCreateFIle("Parts");
                var IsExitPCLAttachments = Helpers.PCLStorage.PCLCreateFIle("Attachments");
                var IsExitFormControlsAndRights = Helpers.PCLStorage.PCLCreateFIle("Dasebord");

                OperationInProgress = true;
                await GetWorkorderControlRights();
                await SetTitlesPropertiesForPage();
                await SetDashboardVisibility();
                if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.iOS)
                {
                    await GoToWorkOrderDetils();
                }
                NavigationPage.SetHasBackButton(this.Page, false);
            }
            catch (Exception ex)
            {
                OperationInProgress = false;

            }

            finally
            {
                OperationInProgress = false;
            }
        }

        public DashboardPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IWorkorderService workorderService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _workorderService = workorderService;
        }

        public async Task SetTitlesPropertiesForPage()
        {

            PageTitle = WebControlTitle.GetTargetNameByTitleName("Dashboard");
            WelcomeTextTitle = AppSettings.User.UserName;
            LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
            CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
            SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
            //SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName(titles, "SelectOptionsTitles"); 


            WorkorderTitle = ShortString.short25(WebControlTitle.GetTargetNameByTitleName("WorkOrder"));


            CloseWorkorderTitle = ShortString.short25(WebControlTitle.GetTargetNameByTitleName("ClosedWorkOrder"));

            InventoryTransactionTitle = ShortString.short25(WebControlTitle.GetTargetNameByTitleName("InventoryTransaction"));

            ServiceRequestTitle = ShortString.short25(WebControlTitle.GetTargetNameByTitleName("ServiceRequest"));

            AssetsTitle = ShortString.short25(WebControlTitle.GetTargetNameByTitleName("Assets"));
            BarcodeTitle = ShortString.short25(WebControlTitle.GetTargetNameByTitleName("SearchScanBarcode"));
            ReceivingTitle = ShortString.shorten(WebControlTitle.GetTargetNameByTitleName("Receiving"));

            SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");



        }
        public async Task ShowActions()
        {
            try
            {
                //UserDialogs.Instance.ShowLoading();
                var response = await DialogService.SelectActionAsync("", SelectTitle, CancelTitle, new ObservableCollection<string>() { LogoutTitle });

                if (response == LogoutTitle)
                {
                    await _authenticationService.LogoutAsync();
                    await NavigationService.NavigateToAsync<LoginPageViewModel>();
                    await NavigationService.RemoveBackStackAsync();
                }


            }
            catch (Exception ex)
            {
                // UserDialogs.Instance.HideLoading();
                OperationInProgress = false;
            }

            finally
            {
                // UserDialogs.Instance.HideLoading();
                OperationInProgress = false;
            }
        }

        public async Task SetDashboardVisibility()
        {
            Thread.Sleep(2000);
           
            var DasebordSerializer = await PCLHelper.ReadAllTextAsync("Dasebord", folder);
            if (DasebordSerializer == null || DasebordSerializer == "")
            {
                FormControlsAndRights = await _formLoadInputService.GetFormControlsAndRights(UserID, AppSettings.DashBoardName);

                string serialized = await Helpers.PCLStorage.PostPCLAsync(FormControlsAndRights);
                bool WriteText = await PCLHelper.WriteTextAllAsync("Dasebord", serialized, folder);

            }
            else
            {
                var PClFormControlsAndRightsForDasebord = Helpers.PCLStorage.GetPCLAsync(DasebordSerializer);
                FormControlsAndRights = PClFormControlsAndRightsForDasebord;
            }

            //Set Asset Visibility
            if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
            {
                if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
                {
                    var GlobalModule = FormControlsAndRights.lstModules.FirstOrDefault(i => i.ModuleName == "Assets");

                    if (GlobalModule.Expression == "E" || GlobalModule.Expression == "V")
                    {
                        AssetVisibility = true;
                        AssetBorderVisibility = true;
                    }
                    else
                    {
                        AssetVisibility = false;
                        AssetBorderVisibility = false;
                    }
                }
            }

            //Set Bar Code Visibility
            if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
            {
                if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
                {
                    var GlobalModule = FormControlsAndRights.lstModules.FirstOrDefault(i => i.ModuleName == "BarcodeFunctions");

                    if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic"))
                    {
                        BarcodeVisibility = false;
                        BarcodeBorderVisibility = false;
                    }
                    else if ((GlobalModule.Expression == "E" || GlobalModule.Expression == "V"))
                    {
                        BarcodeVisibility = true;
                        BarcodeBorderVisibility = true;
                    }
                    else
                    {
                        BarcodeVisibility = false;
                        BarcodeBorderVisibility = false;
                    }
                }
            }

            //Set Inventory Visibility
            if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
            {
                if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
                {
                    var GlobalModule = FormControlsAndRights.lstModules.FirstOrDefault(i => i.ModuleName == "Stockrooms");

                    if (GlobalModule.Expression == "E" || GlobalModule.Expression == "V")
                    {
                        InventoryTransactionVisibility = true;
                        InventoryBorderVisibility = true;
                    }
                    else
                    {
                        InventoryTransactionVisibility = false;
                        InventoryBorderVisibility = false;
                    }
                }
            }

            //Set PurchaseOrder Visibility
            if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
            {
                if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
                {
                    var GlobalModule = FormControlsAndRights.lstModules.FirstOrDefault(i => i.ModuleName == "Receiving");

                    if (GlobalModule.Expression == "E" || GlobalModule.Expression == "V")
                    {
                        ReceivingVisibility = true;
                        ReceivingBorderVisibility = true;
                    }
                    else
                    {
                        ReceivingVisibility = false;
                        ReceivingBorderVisibility = false;
                    }
                }
            }

            //Set Service Request Visibility
            if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
            {
                if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
                {
                    var GlobalModule = FormControlsAndRights.lstModules.FirstOrDefault(i => i.ModuleName == "ServiceRequests");

                    if (GlobalModule.Expression == "E" || GlobalModule.Expression == "V")
                    {
                        ServiceRequestVisibility = true;
                        ServiceRequestBorderVisibility = true;
                    }
                    else
                    {
                        ServiceRequestVisibility = false;
                        ServiceRequestBorderVisibility = false;
                    }
                }
            }

            //Set Workorder Visibility
            if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
            {
                if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
                {
                    var GlobalModule = FormControlsAndRights.lstModules.FirstOrDefault(i => i.ModuleName == "WorkOrders");

                    if (GlobalModule.Expression == "E" || GlobalModule.Expression == "V")
                    {
                        WorkorderVisibility = true;
                        WorkorderBorderVisibility = true;
                    }
                    else
                    {
                        WorkorderVisibility = false;
                        WorkorderBorderVisibility = false;
                    }
                }
            }

            //Set ClosedWo Visibility//
            if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
            {
                if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
                {
                    var GlobalModule = FormControlsAndRights.lstModules.FirstOrDefault(i => i.ModuleName == "ClosedWorkOrders");

                    if (GlobalModule.Expression == "E" || GlobalModule.Expression == "V")
                    {
                        CloseWorkorderVisibility = true;
                        ClosedWorkorderBorderVisibility = true;
                    }
                    else
                    {
                        CloseWorkorderVisibility = false;
                        ClosedWorkorderBorderVisibility = false;
                    }
                }
            }

        }

        public async Task GoToWorkOrderDetils()
        {
            try
            {
                var workordersDetailsRight = JsonConvert.DeserializeObject(NotifactionStorage.Storage.Get("Notificationdb"));

               // DialogService.ShowToast(workordersDetailsRight.ToString(), 200);

                if (workordersDetailsRight != null && workordersDetailsRight != "")
                {


                    UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                    workOrders item = new workOrders();
                    item.WorkOrderID = Convert.ToInt32(workordersDetailsRight);
                    //Save Acknowledment date
                    await SaveAcknowledgementDate(item.WorkOrderID);

                    string data = string.Empty;
                    NotifactionStorage.Storage.Set("Notificationdb", JsonConvert.SerializeObject(data));
                    await NavigationService.NavigateToAsync<WorkorderTabbedPageViewModel>(item);
                }

            }
            catch (Exception ex)
            {
                string data = string.Empty;
                NotifactionStorage.Storage.Set("Notificationdb", JsonConvert.SerializeObject(data));
                // OperationInProgress = false;
                UserDialogs.Instance.HideLoading();
            }

            finally
            {
                //OperationInProgress = false;
                UserDialogs.Instance.HideLoading();
            }
        }
        public async Task SaveAcknowledgementDate(int workorderid)
        {
            var workorder = new ServiceInput
            {
                UserID = AppSettings.User.UserID,
                WorkorderID = workorderid,
                ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                AcknowledgedDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone)

            };
            var response = await _workorderService.SaveWorkOrderAcknowledgement(workorder);
            if (response != null && bool.Parse(response.servicestatus))
            {
                DialogService.ShowToast("Successfully Acknowledged", 200);
            }


        }
        public async Task ShowKPIDashboard()
        {

            try
            {

                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                //OperationInProgress = true;
                await NavigationService.NavigateToAsync<KPIDashboardViewModel>();



            }
            catch (Exception ex)
            {
                // OperationInProgress = false;
                UserDialogs.Instance.HideLoading();
            }

            finally
            {
                //OperationInProgress = false;
                UserDialogs.Instance.HideLoading();
            }
        }
        public async Task ShowWorkorders()
        {

            try
            {

                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                //OperationInProgress = true;

                await NavigationService.NavigateToAsync<WorkorderListingPageViewModel>();


            }
            catch (Exception ex)
            {
                // OperationInProgress = false;
                UserDialogs.Instance.HideLoading();
            }

            finally
            {
                //OperationInProgress = false;
                UserDialogs.Instance.HideLoading();
            }
        }

        public async Task ShowCloseWorkorders()
        {

            try
            {
                // OperationInProgress = true;
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                await NavigationService.NavigateToAsync<SearchClosedWorkorderPageViewModel>();




            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                //OperationInProgress = false;

            }

            finally
            {
                //OperationInProgress = false;
                UserDialogs.Instance.HideLoading();

            }
        }

        public async Task ShowInventory()
        {

            try
            {
                //OperationInProgress = true;
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                await NavigationService.NavigateToAsync<StockroomListingPageViewModel>();
            }
            catch (Exception ex)
            {
                // OperationInProgress = false;
                UserDialogs.Instance.HideLoading();
            }

            finally
            {
                UserDialogs.Instance.HideLoading();
                //OperationInProgress = false;

            }
        }

        public async Task ShowServiceRequest()
        {

            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                // OperationInProgress = true;

                await NavigationService.NavigateToAsync<ServiceRequestListingPageViewModel>();

            }
            catch (Exception ex)
            {
                // OperationInProgress = false;
                UserDialogs.Instance.HideLoading();
            }

            finally
            {
                // OperationInProgress = false;
                UserDialogs.Instance.HideLoading();
            }
        }

        public async Task ShowAsset()
        {

            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                // OperationInProgress = true;


                await NavigationService.NavigateToAsync<AssetListingPageViewModel>();

            }
            catch (Exception ex)
            {
                // OperationInProgress = false;
                UserDialogs.Instance.HideLoading();
            }

            finally
            {
                //OperationInProgress = false;
                UserDialogs.Instance.HideLoading();

            }
        }

        public async Task ShowBarcode()
        {

            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                //OperationInProgress = true;

                await NavigationService.NavigateToAsync<BarcodeDashboardViewModel>();
            }
            catch (Exception ex)
            {
                //  OperationInProgress = false;
                UserDialogs.Instance.HideLoading();
            }

            finally
            {
                // OperationInProgress = false;
                UserDialogs.Instance.HideLoading();
            }
        }

        public async Task ShowReceiving()
        {

            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                //OperationInProgress = true;

                await NavigationService.NavigateToAsync<PurchaseorderListingPageViewModel>();
            }
            catch (Exception ex)
            {
                //OperationInProgress = false;
                UserDialogs.Instance.HideLoading();
            }

            finally
            {
                //OperationInProgress = false;
                UserDialogs.Instance.HideLoading();
            }
        }

        IFolder folder = FileSystem.Current.LocalStorage;
        public async Task GetWorkorderControlRights()
        {
            try
            {
                int WOlistCount = 0;
                if (Application.Current.Properties.ContainsKey("WOlistControlsCount"))
                {
                    var WOlistControlsCount = Application.Current.Properties["WOlistControlsCount"].ToString();
                    if (WOlistControlsCount != null)
                    {
                        WOlistCount = Convert.ToInt32(WOlistControlsCount);

                    }
                }

                int WOTasklistCount = 0;
                if (Application.Current.Properties.ContainsKey("WOTasklistControlsCount"))
                {
                    var WOTasklistControlsCount = Application.Current.Properties["WOTasklistControlsCount"].ToString();
                    if (WOTasklistControlsCount != null)
                    {
                        WOTasklistCount = Convert.ToInt32(WOTasklistControlsCount);

                    }
                }

                int WOInspectionlistCount = 0;
                if (Application.Current.Properties.ContainsKey("WOInspectionlistControlsCount"))
                {
                    var WOInspectionlistControlsCount = Application.Current.Properties["WOInspectionlistControlsCount"].ToString();
                    if (WOInspectionlistControlsCount != null)
                    {
                        WOInspectionlistCount = Convert.ToInt32(WOInspectionlistControlsCount);

                    }
                }

                int WOToolslistCount = 0;
                if (Application.Current.Properties.ContainsKey("WOToolslistControlsCount"))
                {
                    var WOToolslistControlsCount = Application.Current.Properties["WOToolslistControlsCount"].ToString();
                    if (WOToolslistControlsCount != null)
                    {
                        WOToolslistCount = Convert.ToInt32(WOToolslistControlsCount);

                    }
                }

                int WOPartslistCount = 0;
                if (Application.Current.Properties.ContainsKey("WOPartslistControlsCount"))
                {
                    var WOPartslistControlsCount = Application.Current.Properties["WOPartslistControlsCount"].ToString();
                    if (WOPartslistControlsCount != null)
                    {
                        WOPartslistCount = Convert.ToInt32(WOPartslistControlsCount);

                    }
                }

                int WOAttalistCount = 0;
                if (Application.Current.Properties.ContainsKey("WOAttalistControlsCount"))
                {
                    var WOAttalistControlsCount = Application.Current.Properties["WOAttalistControlsCount"].ToString();
                    if (WOAttalistControlsCount != null)
                    {
                        WOAttalistCount = Convert.ToInt32(WOAttalistControlsCount);

                    }
                }

                if (WOlistCount == 0 && WOTasklistCount == 0 && WOInspectionlistCount == 0 && WOToolslistCount == 0 && WOPartslistCount == 0 && WOAttalistCount == 0)
                {
                    ServiceOutput FormControlsAndRightsForDetails;
                    //  var DetailsSerializer = await PCLHelper.ReadAllTextAsync("Details", folder);
                    var DetailsSerializer = "";
                    if (DetailsSerializer == null || DetailsSerializer == "")
                    {
                        FormControlsAndRightsForDetails = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "workorders", "Details");

                        string serialized = await Helpers.PCLStorage.PostPCLAsync(FormControlsAndRightsForDetails);
                        bool WriteText = await PCLHelper.WriteTextAllAsync("Details", serialized, folder);

                    }
                    else
                    {
                        var PClFormControlsAndRightsForDetails = Helpers.PCLStorage.GetPCLAsync(DetailsSerializer);
                        FormControlsAndRightsForDetails = PClFormControlsAndRightsForDetails;
                    }

                    ServiceOutput FormControlsAndRightsForTask;
                    //var TasksSerializer = await PCLHelper.ReadAllTextAsync("Tasks", folder);
                    var TasksSerializer = "";
                    if (TasksSerializer == null || TasksSerializer == "")
                    {
                         FormControlsAndRightsForTask = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "workorders", "Tasks");


                        string serialized = await Helpers.PCLStorage.PostPCLAsync(FormControlsAndRightsForTask);
                        bool WriteText = await PCLHelper.WriteTextAllAsync("Tasks", serialized, folder);

                    }
                    else
                    {
                        var PClFormControlsAndRightsForTask = Helpers.PCLStorage.GetPCLAsync(TasksSerializer);
                        FormControlsAndRightsForTask = PClFormControlsAndRightsForTask;
                    }

                    ServiceOutput FormControlsAndRightsForInspection;
                    //var InspectionSerializer = await PCLHelper.ReadAllTextAsync("Inspections", folder);
                    var InspectionSerializer = "";
                    if (InspectionSerializer == null || InspectionSerializer == "")
                    {
                        FormControlsAndRightsForInspection = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "workorders", "Inspections");


                        string serialized = await Helpers.PCLStorage.PostPCLAsync(FormControlsAndRightsForInspection);
                        bool WriteText = await PCLHelper.WriteTextAllAsync("Inspections", serialized, folder);

                    }
                    else
                    {
                        var PClFormControlsAndRightsForInspection = Helpers.PCLStorage.GetPCLAsync(InspectionSerializer);
                        FormControlsAndRightsForInspection = PClFormControlsAndRightsForInspection;
                    }

                    ServiceOutput FormControlsAndRightsForTools;
                    //var ToolsSerializer = await PCLHelper.ReadAllTextAsync("Tools", folder);
                    var ToolsSerializer = "";
                    if (ToolsSerializer == null || ToolsSerializer == "")
                    {
                        FormControlsAndRightsForTools = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "workorders", "Tools");


                        string serialized = await Helpers.PCLStorage.PostPCLAsync(FormControlsAndRightsForTools);
                        bool WriteText = await PCLHelper.WriteTextAllAsync("Tools", serialized, folder);

                    }
                    else
                    {
                        var PClFormControlsAndRightsForTools = Helpers.PCLStorage.GetPCLAsync(ToolsSerializer);
                        FormControlsAndRightsForTools = PClFormControlsAndRightsForTools;
                    }


                    ServiceOutput FormControlsAndRightsForParts;
                    //var PartsSerializer = await PCLHelper.ReadAllTextAsync("Parts", folder);
                    var PartsSerializer = "";
                    if (PartsSerializer == null || PartsSerializer == "")
                    {
                        FormControlsAndRightsForParts = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "workorders", "Parts");

                        string serialized = await Helpers.PCLStorage.PostPCLAsync(FormControlsAndRightsForParts);
                        bool WriteText = await PCLHelper.WriteTextAllAsync("Parts", serialized, folder);

                    }
                    else
                    {
                        var PClFormControlsAndRightsForParts = Helpers.PCLStorage.GetPCLAsync(PartsSerializer);
                        FormControlsAndRightsForParts = PClFormControlsAndRightsForParts;
                    }

                    ServiceOutput FormControlsAndRightsForAttachments;
                    //var AttachmentsSerializer = await PCLHelper.ReadAllTextAsync("Attachments", folder);
                    var AttachmentsSerializer = "";
                    if (AttachmentsSerializer == null || AttachmentsSerializer == "")
                    {
                        FormControlsAndRightsForAttachments = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "workorders", "Attachments");

                        string serialized = await Helpers.PCLStorage.PostPCLAsync(FormControlsAndRightsForAttachments);
                        bool WriteText = await PCLHelper.WriteTextAllAsync("Attachments", serialized, folder);

                    }
                    else
                    {
                        var PClFormControlsAndRightsForAttachments = Helpers.PCLStorage.GetPCLAsync(AttachmentsSerializer);
                        FormControlsAndRightsForAttachments = PClFormControlsAndRightsForAttachments;
                    }

                    if (FormControlsAndRightsForDetails != null && FormControlsAndRightsForDetails.lstModules != null && FormControlsAndRightsForDetails.lstModules.Count > 0)
                    {
                        var WorkOrderModule = FormControlsAndRightsForDetails.lstModules[0];
                        if (WorkOrderModule.ModuleName == "Details") //ModuleName can't be  changed in service
                        {
                            if (WorkOrderModule.lstSubModules != null && WorkOrderModule.lstSubModules.Count > 0)
                            {
                                var WorkOrderSubModule = WorkOrderModule.lstSubModules[0];
                                if (WorkOrderSubModule.listControls != null && WorkOrderSubModule.listControls.Count > 0)
                                {
                                    try
                                    {
                                        Application.Current.Properties["WOlistControlsCount"] = WorkOrderSubModule.listControls.Count;

                                        Application.Current.Properties["CreateWorkorderRights"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "New").Expression;
                                        Application.Current.Properties["EditRights"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "Edit").Expression;
                                        Application.Current.Properties["CloseWorkorderRightsKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "CompleteAndClose").Expression;

                                        ///Set workOrderListing Page Rights
                                        Application.Current.Properties["WorkOrderStartedDateKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "WorkStartedDate").Expression;
                                        Application.Current.Properties["WorkOrderCompletionDateKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "CompletionDate").Expression;
                                        Application.Current.Properties["WorkOrderRequestedDateKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "RequestedDate").Expression;
                                        Application.Current.Properties["WorkOrderTypeKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "WorkTypeID").Expression;
                                        Application.Current.Properties["DescriptionKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "Description").Expression;
                                        Application.Current.Properties["PriorityKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "PriorityID").Expression;

                                        ///Set workOrderEdit Page Rights


                                        Application.Current.Properties["WorkorderAdditionalDetailsKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "AdditionalDetails").Expression;
                                        Application.Current.Properties["WorkOrderInternalNoteKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "InternalNote").Expression;
                                        Application.Current.Properties["WorkorderCauseKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "Causes").Expression;
                                        Application.Current.Properties["WorkorderTargetKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "AssetID").Expression;
                                        Application.Current.Properties["WorkorderDetailsControls"] = WorkOrderSubModule;
                                        Application.Current.Properties["DistributeCost"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "DistributeCost").Expression;

                                    }
                                    catch (Exception ex)
                                    {


                                    }



                                }

                            }
                        }
                    }

                    if (FormControlsAndRightsForTask != null && FormControlsAndRightsForTask.lstModules != null && FormControlsAndRightsForTask.lstModules.Count > 0)
                    {
                        var WorkOrderTaskModule = FormControlsAndRightsForTask.lstModules[0];
                        if (WorkOrderTaskModule.ModuleName == "TasksandLabor") //ModuleName can't be  changed in service 
                        {
                            if (WorkOrderTaskModule.lstSubModules != null && WorkOrderTaskModule.lstSubModules.Count > 0)
                            {
                                var WorkOrderTaskSubModule = WorkOrderTaskModule.lstSubModules[0];
                                if (WorkOrderTaskSubModule.listControls != null && WorkOrderTaskSubModule.listControls.Count > 0)
                                {

                                    try
                                    {
                                        Application.Current.Properties["WOTasklistControlsCount"] = WorkOrderTaskSubModule.listControls.Count;

                                        Application.Current.Properties["TaskandLabourTabKey"] = WorkOrderTaskModule.Expression;
                                        Application.Current.Properties["CreateTask"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "Add").Expression;
                                        Application.Current.Properties["EditTask"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "Edit").Expression;
                                        Application.Current.Properties["LabourEstimatedHours"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "EstimatedHours").Expression;
                                        Application.Current.Properties["WOLabourTime"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "WorkOrderLaborTime").Expression;
                                        Application.Current.Properties["TaskTabDetails"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "TaskID").Expression;
                                        Application.Current.Properties["HourAtRate1"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "HoursAtRate1").Expression;
                                        Application.Current.Properties["HourAtRate2"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "HoursAtRate2").Expression;
                                        Application.Current.Properties["EmployeeTab"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "EmployeeLaborCraftID").Expression;
                                        Application.Current.Properties["ContractorTab"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "ContractorLaborCraftID").Expression;
                                        Application.Current.Properties["StartdateTab"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "StartDate").Expression;
                                        Application.Current.Properties["CompletionDateTab"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "CompletionDate").Expression;

                                    }
                                    catch (Exception ex)
                                    {


                                    }



                                }



                            }
                        }
                    }

                    if (FormControlsAndRightsForInspection != null && FormControlsAndRightsForInspection.lstModules != null && FormControlsAndRightsForInspection.lstModules.Count > 0)
                    {
                        var WorkOrderInspectionModule = FormControlsAndRightsForInspection.lstModules[0];
                        if (WorkOrderInspectionModule.ModuleName == "WorkOrderInspections") //ModuleName can't be  changed in service 
                        {

                            Application.Current.Properties["InspectionTabKey"] = WorkOrderInspectionModule.Expression;

                        }
                        if (WorkOrderInspectionModule.lstSubModules != null && WorkOrderInspectionModule.lstSubModules.Count > 0)
                        {
                            var WorkOrderInspectionSubModule = WorkOrderInspectionModule.lstSubModules[0];
                            if (WorkOrderInspectionSubModule.listControls != null && WorkOrderInspectionSubModule.listControls.Count > 0)
                            {

                                try
                                {
                                    Application.Current.Properties["WOInspectionlistControlsCount"] = WorkOrderInspectionSubModule.listControls.Count;

                                    Application.Current.Properties["AssociateEmployeeContr"] = WorkOrderInspectionSubModule.listControls.FirstOrDefault(i => i.ControlName == "AssociateEmployeeContr").Expression;
                                    Application.Current.Properties["AssociateInspection"] = WorkOrderInspectionSubModule.listControls.FirstOrDefault(i => i.ControlName == "AssociateInspection").Expression;
                                    Application.Current.Properties["RemoveInspection"] = WorkOrderInspectionSubModule.listControls.FirstOrDefault(i => i.ControlName == "RemoveInspection").Expression;
                                    Application.Current.Properties["RemoveEmployeeContrator"] = WorkOrderInspectionSubModule.listControls.FirstOrDefault(i => i.ControlName == "RemoveEmployeeContrator").Expression;
                                }
                                catch (Exception ex)
                                {


                                }
                            }
                        }
                    }

                    if (FormControlsAndRightsForTools != null && FormControlsAndRightsForTools.lstModules != null && FormControlsAndRightsForTools.lstModules.Count > 0)
                    {
                        var WorkOrderToolsModule = FormControlsAndRightsForTools.lstModules[0];
                        if (WorkOrderToolsModule.ModuleName == "Tools") //ModuleName can't be  changed in service 
                        {

                            Application.Current.Properties["ToolsTabKey"] = WorkOrderToolsModule.Expression;
                            var WorkOrderToolsSubModule = WorkOrderToolsModule.lstSubModules[0];
                            if (WorkOrderToolsSubModule.listControls != null && WorkOrderToolsSubModule.listControls.Count > 0)
                            {

                                try
                                {
                                    Application.Current.Properties["WOToolslistControlsCount"] = WorkOrderToolsSubModule.listControls.Count;

                                    Application.Current.Properties["CreateTool"] = WorkOrderToolsSubModule.listControls.FirstOrDefault(i => i.ControlName == "Add").Expression;
                                    Application.Current.Properties["DeleteTool"] = WorkOrderToolsSubModule.listControls.FirstOrDefault(i => i.ControlName == "Remove").Expression;

                                }
                                catch (Exception ex)
                                {


                                }

                            }
                        }
                    }

                    if (FormControlsAndRightsForParts != null && FormControlsAndRightsForParts.lstModules != null && FormControlsAndRightsForParts.lstModules.Count > 0)
                    {
                        var WorkOrderPartsModule = FormControlsAndRightsForParts.lstModules[0];
                        if (WorkOrderPartsModule.ModuleName == "Parts") //ModuleName can't be  changed in service 
                        {

                            Application.Current.Properties["PartsTabKey"] = WorkOrderPartsModule.Expression;
                            var WorkOrderPartsSubModule = WorkOrderPartsModule.lstSubModules[0];
                            if (WorkOrderPartsSubModule.listControls != null && WorkOrderPartsSubModule.listControls.Count > 0)
                            {

                                try
                                {
                                    Application.Current.Properties["WOPartslistControlsCount"] = WorkOrderPartsSubModule.listControls.Count;

                                    Application.Current.Properties["AddParts"] = WorkOrderPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "Add").Expression;
                                    Application.Current.Properties["EditParts"] = WorkOrderPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "Edit").Expression;
                                    Application.Current.Properties["RemoveParts"] = WorkOrderPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "Remove").Expression;

                                }
                                catch (Exception ex)
                                {


                                }

                            }
                        }
                    }

                    if (FormControlsAndRightsForAttachments != null && FormControlsAndRightsForAttachments.lstModules != null && FormControlsAndRightsForAttachments.lstModules.Count > 0)
                    {
                        var WorkOrderAttachmentModule = FormControlsAndRightsForAttachments.lstModules[0];
                        if (WorkOrderAttachmentModule.ModuleName == "Attachments") //ModuleName can't be  changed in service 
                        {

                            Application.Current.Properties["AttachmentTabKey"] = WorkOrderAttachmentModule.Expression;
                            var WorkOrderAttachmentSubModule = WorkOrderAttachmentModule.lstSubModules[0];
                            if (WorkOrderAttachmentSubModule.listControls != null && WorkOrderAttachmentSubModule.listControls.Count > 0)
                            {
                                try
                                {
                                    Application.Current.Properties["WOAttalistControlsCount"] = WorkOrderAttachmentSubModule.listControls.Count;

                                    Application.Current.Properties["CreateAttachment"] = WorkOrderAttachmentSubModule.listControls.FirstOrDefault(i => i.ControlName == "Add").Expression;
                                    Application.Current.Properties["DeleteAttachments"] = WorkOrderAttachmentSubModule.listControls.FirstOrDefault(i => i.ControlName == "Remove").Expression;
                                    Application.Current.Properties["AttachmentFiles"] = WorkOrderAttachmentSubModule.listControls.FirstOrDefault(i => i.ControlName == "Attach").Expression;

                                }
                                catch (Exception ex)
                                {


                                }


                            }
                        }
                    }
                }
            }
            catch (Exception)
            {


            }

        }

        #endregion

    }
}