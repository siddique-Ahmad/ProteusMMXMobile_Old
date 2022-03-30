using Acr.UserDialogs;

using Microsoft.AppCenter.Crashes;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using ProteusMMX.Constants;
using ProteusMMX.Helpers;
using ProteusMMX.Model;
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
        string _copyrightLabel = "Copyright ©"+ Year +" Eagle Technology Inc.";
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
        bool _workorderBorderVisibility=true;
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

        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {

               
                OperationInProgress = true;
                await SetTitlesPropertiesForPage();
                await SetDashboardVisibility();
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

            if (Device.RuntimePlatform == Device.UWP)
            {
                PageTitle = WebControlTitle.GetTargetNameByTitleName("Dashboard")+ (WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.User.UserName).PadLeft(90);
            }
           
            else
            {
                PageTitle = WebControlTitle.GetTargetNameByTitleName("Dashboard");
            }
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
                var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() { LogoutTitle });

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
           FormControlsAndRights = await _formLoadInputService.GetFormControlsAndRights(UserID, AppSettings.DashBoardName);


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

        #endregion
       
    }
}