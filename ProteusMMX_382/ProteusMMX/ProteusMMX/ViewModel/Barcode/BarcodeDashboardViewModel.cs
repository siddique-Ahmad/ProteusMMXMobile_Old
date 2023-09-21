using ProteusMMX.Helpers;
using ProteusMMX.Model;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.Barcode;
using ProteusMMX.Services.Dialog;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Asset;
using ProteusMMX.ViewModel.Inventory;
using ProteusMMX.ViewModel.PurchaseOrder;
using ProteusMMX.ViewModel.SelectionListPagesViewModels;
using ProteusMMX.ViewModel.ServiceRequest;
using ProteusMMX.ViewModel.Workorder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace ProteusMMX.ViewModel.Barcode
{
    public class BarcodeDashboardViewModel:ViewModelBase
    {
        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;


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


        string _copyrightLabel = "Copyright @ Eagle Technology Inc.";
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
        string _searchAssetTitle;
        public string SearchAssetTitle
        {
            get
            {
                return _searchAssetTitle;
            }

            set
            {
                if (value != _searchAssetTitle)
                {
                    _searchAssetTitle = value;
                    OnPropertyChanged("SearchAssetTitle");
                }
            }
        }
        string _searchWorkorderByAssetTitle;
        public string SearchWorkorderByAssetTitle
        {
            get
            {
                return _searchWorkorderByAssetTitle;
            }

            set
            {
                if (value != _searchWorkorderByAssetTitle)
                {
                    _searchWorkorderByAssetTitle = value;
                    OnPropertyChanged("SearchWorkorderByAssetTitle");
                }
            }
        }
        string _searchWorkorderByLocationTitle;
        public string SearchWorkorderByLocationTitle
        {
            get
            {
                return _searchWorkorderByLocationTitle;
            }

            set
            {
                if (value != _searchWorkorderByLocationTitle)
                {
                    _searchWorkorderByLocationTitle = value;
                    OnPropertyChanged("SearchWorkorderByLocationTitle");
                }
            }
        }
        string _searchAssetForBillOfMaterialTitle;
        public string SearchAssetForBillOfMaterialTitle
        {
            get
            {
                return _searchAssetForBillOfMaterialTitle;
            }

            set
            {
                if (value != _searchAssetForBillOfMaterialTitle)
                {
                    _searchAssetForBillOfMaterialTitle = value;
                    OnPropertyChanged("SearchAssetForBillOfMaterialTitle");
                }
            }
        }
        string _searchAssetForAttachmentTitle;
        public string SearchAssetForAttachmentTitle
        {
            get
            {
                return _searchAssetForAttachmentTitle;
            }

            set
            {
                if (value != _searchAssetForAttachmentTitle)
                {
                    _searchAssetForAttachmentTitle = value;
                    OnPropertyChanged("SearchAssetForAttachmentTitle");
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

        bool _searchAssetVisibility;
        public bool SearchAssetVisibility
        {
            get
            {
                return _searchAssetVisibility;
            }

            set
            {
                if (value != _searchAssetVisibility)
                {
                    _searchAssetVisibility = value;
                    OnPropertyChanged("SearchAssetVisibility");
                }
            }
        }

        bool _searchWoBYAssetVisibility;
        public bool SearchWoBYAssetVisibility
        {
            get
            {
                return _searchWoBYAssetVisibility;
            }

            set
            {
                if (value != _searchWoBYAssetVisibility)
                {
                    _searchWoBYAssetVisibility = value;
                    OnPropertyChanged("SearchWoBYAssetVisibility");
                }
            }
        }

        bool _searchWOByLocationVisibility;
        public bool SearchWOByLocationVisibility
        {
            get
            {
                return _searchWOByLocationVisibility;
            }

            set
            {
                if (value != _searchWOByLocationVisibility)
                {
                    _searchWOByLocationVisibility = value;
                    OnPropertyChanged("SearchWOByLocationVisibility");
                }
            }
        }

        bool _searchBillOfMaterialVisibility;
        public bool SearchBillOfMaterialVisibility
        {
            get
            {
                return _searchBillOfMaterialVisibility;
            }

            set
            {
                if (value != _searchBillOfMaterialVisibility)
                {
                    _searchBillOfMaterialVisibility = value;
                    OnPropertyChanged("SearchBillOfMaterialVisibility");
                }
            }
        }

        bool _searchAssetForAttachmentVisibility;
        public bool SearchAssetForAttachmentVisibility
        {
            get
            {
                return _searchAssetForAttachmentVisibility;
            }

            set
            {
                if (value != _searchAssetForAttachmentVisibility)
                {
                    _searchAssetForAttachmentVisibility = value;
                    OnPropertyChanged("SearchAssetForAttachmentVisibility");
                }
            }
        }

     

        #endregion


        #endregion

        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);
        public ICommand SearchAssetCommand => new AsyncCommand(SearchAsset);
        public ICommand SearchWorkorderByAssetCommand => new AsyncCommand(SearchWorkorderByAsset);
        public ICommand SearchWorkorderByLocationCommand => new AsyncCommand(SearchWorkorderByLocation);
        public ICommand SearchAssetForBillOfMaterialCommand => new AsyncCommand(SearchAssetForBillOfMaterial);
        public ICommand SearchAssetForAttachmentCommand => new AsyncCommand(SearchAssetForAttachment);
       
        #endregion

        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {

                //if (ConnectivityService.IsConnected == false)
                //{
                //    await DialogService.ShowAlertAsync("internet not available", "Alert", "OK");
                //    return;

                //}


                OperationInProgress = true;
                await SetTitlesPropertiesForPage();
                await SetBarcodeModulesVisibility();
                OperationInProgress = false;


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

        public BarcodeDashboardViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
        }

        public async Task SetTitlesPropertiesForPage()
        {
                PageTitle = WebControlTitle.GetTargetNameByTitleName("BarcodeFunctions");
                WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                //SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName(titles, "SelectOptionsTitles"); 


                SearchAssetTitle = ShortString.short25(WebControlTitle.GetTargetNameByTitleName("SearchAsset"));
                SearchWorkorderByAssetTitle = ShortString.short25(WebControlTitle.GetTargetNameByTitleName("SearchWorkOrderbyAsset"));
                SearchWorkorderByLocationTitle = ShortString.short25(WebControlTitle.GetTargetNameByTitleName("SearchWorkOrderbyLocation"));
                SearchAssetForBillOfMaterialTitle = ShortString.short25(WebControlTitle.GetTargetNameByTitleName("SearchScanAssetForBillOfMaterial"));
                SearchAssetForAttachmentTitle = ShortString.short25(WebControlTitle.GetTargetNameByTitleName("SearchscanAssetforAttachments"));
                BarcodeTitle = ShortString.short25(WebControlTitle.GetTargetNameByTitleName("SearchScanBarcode"));
                SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");
        }
        public async Task SetBarcodeModulesVisibility()
        {
           // FormControlsAndRights = await _formLoadInputService.GetFormControlsAndRights(UserID, AppSettings.BarcodeModuleName);

            FormControlsAndRights = await _formLoadInputService.GetFormControlsAndRights(UserID, AppSettings.DashBoardName);

            if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
            {
                var SubModules = FormControlsAndRights.lstModules.FirstOrDefault(i => i.ModuleName == "BarcodeSearchAsset");
              
                if (SubModules.Expression == "E" || SubModules.Expression == "V")
                {
                    SearchAssetVisibility = true;
                }
                else
                {
                    SearchAssetVisibility = false;
                }
            }
            if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
            {
                
                var SubModules = FormControlsAndRights.lstModules.FirstOrDefault(i => i.ModuleName == "BarcodeSearchWorkOrderByAsset");
              
                if (SubModules.Expression == "E" || SubModules.Expression == "V")
                {
                    SearchWoBYAssetVisibility = true;
                }
                else
                {
                    SearchWoBYAssetVisibility = false;
                }
            }
            if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
            {
             
                var SubModules = FormControlsAndRights.lstModules.FirstOrDefault(i => i.ModuleName == "BarcodeSearchWorkOrderByLocation");
              
                if (SubModules.Expression == "E" || SubModules.Expression == "V")
                {
                    SearchWOByLocationVisibility = true;
                }
                else
                {
                    SearchWOByLocationVisibility = false;
                }
            }
            if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
            {
                var SubModules = FormControlsAndRights.lstModules.FirstOrDefault(i => i.ModuleName == "BarcodeAssetForBOM");
             
                if (SubModules.Expression == "E" || SubModules.Expression == "V")
                {
                    SearchBillOfMaterialVisibility = true;
                }
                else
                {
                    SearchBillOfMaterialVisibility = false;
                }
            }
            if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
            {
                var SubModules = FormControlsAndRights.lstModules.FirstOrDefault(i => i.ModuleName == "BarcodeAssetForAttachment");
             
                if (SubModules.Expression == "E" || SubModules.Expression == "V")
                {
                    SearchAssetForAttachmentVisibility = true;
                }
                else
                {
                    SearchAssetForAttachmentVisibility = false;
                }
            }


        }
      
        public async Task ShowActions()
        {
            try
            {
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
                OperationInProgress = false;
            }

            finally
            {
                OperationInProgress = false;
            }
        }

        public async Task SearchAsset()
        {

            try
            {
                OperationInProgress = true;

                await NavigationService.NavigateToAsync<SearchAssetByAssetNumberViewModel>();
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

        public async Task SearchWorkorderByAsset()
        {

            try
            {
                OperationInProgress = true;

                await NavigationService.NavigateToAsync<SearchWorkorderByAssetNumberViewModel>();



                #region Test

                //var options = new MobileBarcodeScanningOptions()
                //{
                //    AutoRotate = false,
                //    TryHarder = true,

                //};

                //ZXingScannerPage _scanner = new ZXingScannerPage(options)
                //{
                //    DefaultOverlayTopText = "Align the barcode within the frame",
                //    DefaultOverlayBottomText = string.Empty,
                //    DefaultOverlayShowFlashButton = true
                //};

                //_scanner.OnScanResult += _scanner_OnScanResult;

                //var navPage = App.Current.MainPage as NavigationPage;
                //await navPage.PushAsync(_scanner);

                #endregion
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

        public async Task SearchWorkorderByLocation()
        {

            try
            {
                OperationInProgress = true;

                await NavigationService.NavigateToAsync<SearchWorkorderByLocationViewModel>();
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

        public async Task SearchAssetForBillOfMaterial()
        {

            try
            {
                OperationInProgress = true;

                await NavigationService.NavigateToAsync<SearchAssetForBillOfMaterialViewModel>();
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

        public async Task SearchAssetForAttachment()
        {

            try
            {
                OperationInProgress = true;

                await NavigationService.NavigateToAsync<SearchAssetForAttachmentViewModel>();
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

        #endregion
    }
}
