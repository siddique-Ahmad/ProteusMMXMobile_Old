using Acr.UserDialogs;
using ProteusMMX.Helpers;
using ProteusMMX.Model;
using ProteusMMX.Model.AssetModel;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.InventoryModel;
using ProteusMMX.Model.PurchaseOrderModel;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Asset;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Inventory;
using ProteusMMX.Services.PurchaseOrder;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
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

namespace ProteusMMX.ViewModel.PurchaseOrder
{
    public class PurchaseorderListingPageViewModel : ViewModelBase
    {
        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly IPurchaseOrderService _purchaseOrderService;

        protected readonly IWorkorderService _workorderService;

        #endregion

        #region Properties

        #region Page Properties

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

        Page _page;
        public Page Page
        {
            get
            {
                return _page;
            }

            set
            {
                _page = value;
                OnPropertyChanged(nameof(Page));
            }
        }

        int _stockroomID;
        public int StockroomID
        {
            get
            {
                return _stockroomID;
            }

            set
            {
                if (value != _stockroomID)
                {
                    _stockroomID = value;
                    OnPropertyChanged(nameof(StockroomID));
                }
            }
        }

        #endregion

        #region Title Properties

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

        bool _TotalRecordForPhone = false;
        public bool TotalRecordForPhone
        {
            get
            {
                return _TotalRecordForPhone;
            }

            set
            {
                if (value != _TotalRecordForPhone)
                {
                    _TotalRecordForPhone = value;
                    OnPropertyChanged(nameof(TotalRecordForPhone));
                }
            }
        }

        bool _TotalRecordForTab = false;
        public bool TotalRecordForTab
        {
            get
            {
                return _TotalRecordForTab;
            }

            set
            {
                if (value != _TotalRecordForTab)
                {
                    _TotalRecordForTab = value;
                    OnPropertyChanged(nameof(TotalRecordForTab));
                }
            }
        }



        int _totalRecordCount;
        public int TotalRecordCount
        {
            get
            {
                return _totalRecordCount;
            }

            set
            {
                if (value != _totalRecordCount)
                {
                    _totalRecordCount = value;
                    OnPropertyChanged("TotalRecordCount");
                }
            }
        }

        string _purchaseOrderNumber;
        public string PurchaseOrderNumber
        {
            get
            {
                return _purchaseOrderNumber;
            }

            set
            {
                if (value != _purchaseOrderNumber)
                {
                    _purchaseOrderNumber = value;
                    OnPropertyChanged("PurchaseOrderNumber");
                }
            }
        }





        #endregion

        #region Dialog Actions Titles

        string _searchPlaceholder;
        public string SearchPlaceholder
        {
            get
            {
                return _searchPlaceholder;
            }

            set
            {
                if (value != _searchPlaceholder)
                {
                    _searchPlaceholder = value;
                    OnPropertyChanged("SearchPlaceholder");
                }
            }
        }

        string _goTitle;
        public string GoTitle
        {
            get
            {
                return _goTitle;
            }

            set
            {
                if (value != _goTitle)
                {
                    _goTitle = value;
                    OnPropertyChanged("GoTitle");
                }
            }
        }
        string _searchText;
        public string SearchText
        {
            get
            {
                return _searchText;
            }

            set
            {
                if (value != _searchText)
                {
                    _searchText = value;
                    OnPropertyChanged("SearchText");
                    SearchText_TextChanged();
                }
            }
        }
        string _scanTitle;
        public string ScanTitle
        {
            get
            {
                return _scanTitle;
            }

            set
            {
                if (value != _scanTitle)
                {
                    _scanTitle = value;
                    OnPropertyChanged("ScanTitle");
                }
            }
        }

        string _searchButtonTitle;
        public string SearchButtonTitle
        {
            get
            {
                return _searchButtonTitle;
            }

            set
            {
                if (value != _searchButtonTitle)
                {
                    _searchButtonTitle = value;
                    OnPropertyChanged("SearchButtonTitle");
                }
            }
        }

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
        string _totalRecordTitle;
        public string TotalRecordTitle
        {
            get
            {
                return _totalRecordTitle;
            }

            set
            {
                if (value != _totalRecordTitle)
                {
                    _totalRecordTitle = value;
                    OnPropertyChanged("TotalRecordTitle");
                }
            }
        }

        #endregion

        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);

        public ICommand ScanCommand => new AsyncCommand(SearchPuchaseOrder);

        public ICommand PurchaseOrderSelectedCommand => new Command<Model.PurchaseOrderModel.PurchaseOrder>(OnSelectPurchaseOrdersync);
        #endregion



        #region ListView Properties

        int _pageNumber = 1;
        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }

            set
            {
                if (value != _pageNumber)
                {
                    _pageNumber = value;
                    OnPropertyChanged(nameof(PageNumber));
                }
            }
        }

        int _rowCount = 10;
        public int RowCount
        {
            get
            {
                return _rowCount;
            }

            set
            {
                if (value != _rowCount)
                {
                    _rowCount = value;
                    OnPropertyChanged(nameof(RowCount));
                }
            }
        }


        ObservableCollection<Model.PurchaseOrderModel.PurchaseOrder> _purchaseOrderCollection = new ObservableCollection<Model.PurchaseOrderModel.PurchaseOrder>();

        public ObservableCollection<Model.PurchaseOrderModel.PurchaseOrder> PurchaseOrderCollection
        {
            get
            {
                return _purchaseOrderCollection;
            }

        }

        #endregion




        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {


                if (navigationData != null)
                {

                    var navigationParams = navigationData as PageParameters;
                    this.Page = navigationParams.Page;

                }

                OperationInProgress = true;
                await SetTitlesPropertiesForPage();
                await GetPurchaseOrderControlRights();
                await GetPurchaseOrders();
                if (Device.Idiom == TargetIdiom.Phone)
                {
                    this.TotalRecordForPhone = true;
                }
                else
                {
                    this.TotalRecordForTab = true;
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

        public PurchaseorderListingPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IPurchaseOrderService purchaseOrderService, IWorkorderService workorderService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _purchaseOrderService = purchaseOrderService;
            _workorderService = workorderService;
        }
        public async Task GetPurchaseOrderControlRights()
        {

            ServiceOutput FormControlsAndRightsForDetails = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "Purchaseorder", "Details");


            if (FormControlsAndRightsForDetails != null && FormControlsAndRightsForDetails.lstModules != null && FormControlsAndRightsForDetails.lstModules.Count > 0)
            {
                var POModule = FormControlsAndRightsForDetails.lstModules[0];
                if (POModule.ModuleName == "PurchaseOrders") //ModuleName can't be  changed in service
                {

                    if (POModule.lstSubModules != null && POModule.lstSubModules.Count > 0)
                    {
                        var POSubModule = POModule.lstSubModules[0];
                        if (POSubModule.listControls != null && POSubModule.listControls.Count > 0)
                        {
                            try
                            {
                                Application.Current.Properties["NonStockroomParts"] = POSubModule.listControls.FirstOrDefault(i => i.ControlName == "NonStockroomParts").Expression;
                                Application.Current.Properties["Parts"] = POSubModule.listControls.FirstOrDefault(i => i.ControlName == "Parts").Expression;
                                Application.Current.Properties["Assets"] = POSubModule.listControls.FirstOrDefault(i => i.ControlName == "Assets").Expression;

                            }
                            catch (Exception ex)
                            {


                            }



                        }



                    }
                }
            }
            FormControlsAndRights = await _formLoadInputService.GetFormControlsAndRights(AppSettings.User.UserID.ToString(), AppSettings.ReceivingModuleName);
            if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
            {
                var PurchaseOrderModule = FormControlsAndRights.lstModules[0];
                if (PurchaseOrderModule.ModuleName == "Purchasing") //ModuleName can't be  changed in service
                {
                    if (PurchaseOrderModule.lstSubModules != null && PurchaseOrderModule.lstSubModules.Count > 0)
                    {

                        var PurchaseOrderSubModule = PurchaseOrderModule.lstSubModules.FirstOrDefault(i => i.SubModuleName == "PurchaseOrders");

                        if (PurchaseOrderSubModule != null)
                        {
                            if (PurchaseOrderSubModule.Button != null && PurchaseOrderSubModule.Button.Count > 0)
                            {
                                //  CloseWorkorderRights = workorderSubModule.Button.FirstOrDefault(i => i.Name == "Close");

                            }

                            if (PurchaseOrderSubModule.listDialoges != null && PurchaseOrderSubModule.listDialoges.Count > 0)
                            {
                                var PurchaseOrderDialog = PurchaseOrderSubModule.listDialoges.FirstOrDefault(i => i.DialogName == "Receiving");
                                if (PurchaseOrderDialog != null && PurchaseOrderDialog.listTab != null && PurchaseOrderDialog.listTab.Count > 0)
                                {
                                    try
                                    {
                                        var PONonStockPartsTab = PurchaseOrderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "NonStockroomParts");
                                        var POAssetsTab = PurchaseOrderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "Assets");
                                        var POPartsTab = PurchaseOrderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "Parts");
                                        var POPartsRecieveButton = POPartsTab.ButtonControls.FirstOrDefault(i => i.Name == "ReceiveParts");
                                        var PONonStockPartsRecieveButton = PONonStockPartsTab.ButtonControls.FirstOrDefault(i => i.Name == "ReceiveNonStockroomParts");
                                        var POAssetsRecieveButton = POAssetsTab.ButtonControls.FirstOrDefault(i => i.Name == "ReceiveAssets");

                                        Application.Current.Properties["ReceiveParts"] = POPartsRecieveButton.Expression;
                                        Application.Current.Properties["ReceiveNonStockroomParts"] = PONonStockPartsRecieveButton.Expression;
                                        Application.Current.Properties["ReceiveAssets"] = POAssetsRecieveButton.Expression;
                                    }
                                    catch (Exception ex)
                                    {


                                    }

                                }
                            }
                        }
                    }
                }

            }

        }
        public async Task SetTitlesPropertiesForPage()
        {
                PageTitle = WebControlTitle.GetTargetNameByTitleName("PurchaseOrders");
                WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                PurchaseOrderNumber = WebControlTitle.GetTargetNameByTitleName("PurchaseOrderNumber");
                SearchPlaceholder = WebControlTitle.GetTargetNameByTitleName("ScanOrTypePurchaseOrderNo");
                GoTitle = WebControlTitle.GetTargetNameByTitleName("Go");
                ScanTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                SearchButtonTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                TotalRecordTitle = WebControlTitle.GetTargetNameByTitleName("TotalRecords");
                SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");
        }
        public async Task ShowActions()
        {
            try
            {
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
                OperationInProgress = false;
            }

            finally
            {
                OperationInProgress = false;
            }
        }




        public async Task GetPurchaseOrdersAuto()
        {
            if(string.IsNullOrWhiteSpace(SearchText))
            {
                PageNumber++;
                await GetPurchaseOrders();
            }
        }
        async Task GetPurchaseOrders()
        {
            try
            {
                OperationInProgress = true;
                var purchaseResponse = await _purchaseOrderService.GetPurchaseOrders(UserID, PageNumber.ToString(), RowCount.ToString());
                if (purchaseResponse != null && purchaseResponse.poWrapper != null
                    && purchaseResponse.poWrapper.purchaseOrders != null && purchaseResponse.poWrapper.purchaseOrders.Count > 0)
                {

                    var purchaseorders = purchaseResponse.poWrapper.purchaseOrders;

                    await AddPOInPurchaseOrderCollection(purchaseorders);
                    TotalRecordCount = purchaseResponse.poWrapper.recordCountPO;

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

        async Task GetPuchaseOrderFromSearchBar()
        {
            try
            {
                OperationInProgress = true;
                var purchaseResponse = await _purchaseOrderService.GetPurchaseOrderByPuchaseOrderNumber(this.SearchText, UserID);
                if (purchaseResponse.poWrapper.purchaseOrders == null || purchaseResponse.poWrapper.purchaseOrders.Count == 0)
                {
                    TotalRecordCount = 0;
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("ThisPOdoesnotexist"), 2000);
                }
                if (purchaseResponse != null && purchaseResponse.poWrapper != null
                    && purchaseResponse.poWrapper.purchaseOrders != null && purchaseResponse.poWrapper.purchaseOrders.Count > 0)
                {

                    var purchaseorders = purchaseResponse.poWrapper.purchaseOrders;

                    await AddPOInPurchaseOrderCollection(purchaseorders);
                    TotalRecordCount = purchaseResponse.poWrapper.purchaseOrders.Count;

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

        private async Task AddPOInPurchaseOrderCollection(List<Model.PurchaseOrderModel.PurchaseOrder> po)
        {
            if (po != null && po.Count > 0)
            {
                foreach (var item in po)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _purchaseOrderCollection.Add(item);
                        OnPropertyChanged(nameof(PurchaseOrderCollection));
                    });



                }

            }
        }


        private async Task RemoveAllPurchaseOrderFromCollection()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                PurchaseOrderCollection.Clear();
                OnPropertyChanged(nameof(PurchaseOrderCollection));
            });



        }

        public async Task SearchPuchaseOrder()
        {

            try
            {
                OperationInProgress = true;


                #region Barcode Section and Search Section

                if (SearchButtonTitle == ScanTitle)
                {
                    var options = new MobileBarcodeScanningOptions()
                    {
                        AutoRotate = false,
                        TryHarder = true,

                    };

                    options.PossibleFormats = new List<ZXing.BarcodeFormat>() { ZXing.BarcodeFormat.CODE_39, ZXing.BarcodeFormat.CODE_93, ZXing.BarcodeFormat.CODE_128, ZXing.BarcodeFormat.EAN_13, ZXing.BarcodeFormat.QR_CODE };
                    options.TryHarder = false; options.BuildBarcodeReader().Options.AllowedLengths = new[] { 44 };
                    ZXingScannerPage _scanner = new ZXingScannerPage(options)
                    {
                        DefaultOverlayTopText = "Align the barcode within the frame",
                        DefaultOverlayBottomText = string.Empty,
                        DefaultOverlayShowFlashButton = true
                    };
                    _scanner.AutoFocus();

                    _scanner.OnScanResult += _scanner_OnScanResult;
                    var navPage = App.Current.MainPage as NavigationPage;
                    await navPage.PushAsync(_scanner);
                }

                else
                {
                    //reset pageno. and start search again.
                    await RefillPuchaseOrderCollection();

                }

                #endregion

                //await NavigationService.NavigateToAsync<WorkorderListingPageViewModel>();
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


        private async void _scanner_OnScanResult(ZXing.Result result)
        {
            //Set the text property
            this.SearchText = result.Text;

            ///Pop the scanner page
            Device.BeginInvokeOnMainThread(async () =>
            {
                var navPage = App.Current.MainPage as NavigationPage;
                await navPage.PopAsync();


                //reset pageno. and start search again.
                await RefillPuchaseOrderCollection();


            });

        }
        private async Task RefillPuchaseOrderCollection()
        {
            PageNumber = 1;
            await RemoveAllPurchaseOrderFromCollection();
            await GetPuchaseOrderFromSearchBar();
        }

        private void SearchText_TextChanged()
        {

            try
            {
                if (SearchText == null || SearchText.Length == 0)
                {
                    SearchButtonTitle = ScanTitle;
                }

                else if (SearchText.Length > 0)
                {
                    SearchButtonTitle = GoTitle;
                }
            }
            catch (Exception ex)
            {


            }
        }



        private async void OnSelectPurchaseOrdersync(Model.PurchaseOrderModel.PurchaseOrder item)
        {
            if (item != null)
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                //OperationInProgress = true;
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.RequisitionID = item.RequisitionID;
                tnobj.PurchaseOrderNumber = item.PurchaseOrderNumber;
                await NavigationService.NavigateToAsync<PuchaseOrderTabbedPageViewModel>(item);
                // OperationInProgress = false;
                UserDialogs.Instance.HideLoading();

            }
        }
       
        #endregion


    }
}
