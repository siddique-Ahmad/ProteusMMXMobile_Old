using Acr.UserDialogs;
using ProteusMMX.Helpers;
using ProteusMMX.Model;
using ProteusMMX.Model.AssetModel;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.InventoryModel;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Asset;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Inventory;
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

namespace ProteusMMX.ViewModel.Inventory
{
    public class StockroomListingPageViewModel:ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly IInventoryService _inventoryService;

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

        string _stockroomName;
        public string StockroomName
        {
            get
            {
                return _stockroomName;
            }

            set
            {
                if (value != _stockroomName)
                {
                    _stockroomName = value;
                    OnPropertyChanged("StockroomName");
                }
            }
        }

        string _numberOfParts;
        public string NumberOfParts
        {
            get
            {
                return _numberOfParts;
            }

            set
            {
                if (value != _numberOfParts)
                {
                    _numberOfParts = value;
                    OnPropertyChanged("NumberOfParts");
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
        string _quantityOnHand;
        public string QuantityOnHand
        {
            get
            {
                return _quantityOnHand;
            }

            set
            {
                if (value != _quantityOnHand)
                {
                    _quantityOnHand = value;
                    OnPropertyChanged("QuantityOnHand");
                }
            }
        }

        string _totalQuantityAvailable;
        public string TotalQuantityAvailable
        {
            get
            {
                return _totalQuantityAvailable;
            }

            set
            {
                if (value != _totalQuantityAvailable)
                {
                    _totalQuantityAvailable = value;
                    OnPropertyChanged("TotalQuantityAvailable");
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

        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);
        public ICommand StockroomSelectedCommand => new Command<Stockroom>(OnSelectStockroomAsync);

        public ICommand ScanCommand => new AsyncCommand(SearchStockroom);

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


        ObservableCollection<Stockroom> _stockroomsCollection = new ObservableCollection<Stockroom>();

        public ObservableCollection<Stockroom> StockroomsCollection
        {
            get
            {
                return _stockroomsCollection;
            }

        }

        #endregion




        #region Methods

        public async Task GetInventoryControlRights()
        {

            ServiceOutput FormControlsAndRightsForInventory = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "StockroomParts", "Dialog");


            if (FormControlsAndRightsForInventory != null && FormControlsAndRightsForInventory.lstModules != null && FormControlsAndRightsForInventory.lstModules.Count > 0)
            {
                var StockroomParts = FormControlsAndRightsForInventory.lstModules[0];
                if (StockroomParts.ModuleName == "StockroomParts") //ModuleName can't be  changed in service
                {
                    if (StockroomParts.lstSubModules != null && StockroomParts.lstSubModules.Count > 0)
                    {
                        var StockroomPartsSubModule = StockroomParts.lstSubModules[0];
                        if (StockroomPartsSubModule.listControls != null && StockroomPartsSubModule.listControls.Count > 0)
                        {
                            try
                            {
                                Application.Current.Properties["StockroomPartsVisibilityKey"] = StockroomParts.Expression;
                                Application.Current.Properties["StockroomTransactionDialog"] = StockroomPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "StockroomTransactionDialog").Expression;
                                Application.Current.Properties["ShelfBinKey"] = StockroomPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "ShelfBin").Expression;

                                Application.Current.Properties["InventoryPerformTransaction"] = StockroomPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "PerformTransaction").Expression;
                                Application.Current.Properties["UpdateLastPhysicalInventorydate"] = StockroomPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "UpdateLastPhysicalInventorydate").Expression;
                                Application.Current.Properties["InventoryUserField1"] = StockroomPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "UserField1").Expression;
                                Application.Current.Properties["InventoryUserField2"] = StockroomPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "UserField2").Expression;
                                Application.Current.Properties["InventoryUserField3"] = StockroomPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "UserField3").Expression;
                                Application.Current.Properties["InventoryUserField4"] = StockroomPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "UserField4").Expression;
                                Application.Current.Properties["InventoryPartNumber"] = StockroomPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "PartNumber").Expression;
                                Application.Current.Properties["InventoryPartName"] = StockroomPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "PartName").Expression;
                                Application.Current.Properties["InventoryQuantityOnHand"] = StockroomPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "QuantityOnHand").Expression;
                                Application.Current.Properties["InventoryCostCenterName"] = StockroomPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "CostCenterName").Expression;
                                Application.Current.Properties["InventoryDescription"] = StockroomPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "Description").Expression;
                                Application.Current.Properties["InventoryTransactionReasonName"] = StockroomPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "TransactionReasonName").Expression;
                                Application.Current.Properties["InventoryTransactor"] = StockroomPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "Transactor").Expression;
                                Application.Current.Properties["InventoryUnitCostID"] = StockroomPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "UnitCostID").Expression;
                                Application.Current.Properties["InventoryCheckOutTo"] = StockroomPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "CheckOutTo").Expression;
                                Application.Current.Properties["InventoryTransactionType"] = StockroomPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "TransactionType").Expression;
                                Application.Current.Properties["InventoryTransactionQuantity"] = StockroomPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "TransactionQuantity").Expression;

                            }
                            catch (Exception ex)
                            {


                            }


                        }



                    }
                }
            }

        }


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
                await GetInventoryControlRights();
                
                if (Application.Current.Properties.ContainsKey("StockroomPartsVisibilityKey"))
                {
                    var StockroomPartsVisibilityKey = Application.Current.Properties["StockroomPartsVisibilityKey"].ToString();
                    if (StockroomPartsVisibilityKey == "E" || StockroomPartsVisibilityKey == "V")
                    {
                        InventoryTransactionVisibility = true;
                    }
                    else
                    {
                        InventoryTransactionVisibility = false;
                    }

                }
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

        public StockroomListingPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IInventoryService inventoryService, IWorkorderService workorderService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _inventoryService = inventoryService;
            _workorderService = workorderService;
        }

        public async Task SetTitlesPropertiesForPage()
        {

          //  var titles = await _formLoadInputService.GetFormLoadInputForBarcode(UserID, AppSettings.WorkorderDetailFormName);
            //if (titles != null && titles.CFLI.Count > 0 && titles.listWebControlTitles.Count > 0)
            //{
                PageTitle = WebControlTitle.GetTargetNameByTitleName("StockroomName");
                WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                StockroomName = WebControlTitle.GetTargetNameByTitleName("StockroomName");
                NumberOfParts = WebControlTitle.GetTargetNameByTitleName("NumberOfParts");
                QuantityOnHand = WebControlTitle.GetTargetNameByTitleName("QuantityOnHand");
                TotalQuantityAvailable = WebControlTitle.GetTargetNameByTitleName("TotalQuantityAvailable");
                SearchPlaceholder = WebControlTitle.GetTargetNameByTitleName("TypeOrScanStockRoomName");
                GoTitle = WebControlTitle.GetTargetNameByTitleName("Go");
                ScanTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                SearchButtonTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                TotalRecordTitle= WebControlTitle.GetTargetNameByTitleName("TotalRecords");
            SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");


            //}
        }
        public async Task ShowActions()
        {
            try
            {
                var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() {LogoutTitle });

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


        public async Task CreateControlsForPage()
        {
         
            //if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
            //{
            //    var GlobalModule = FormControlsAndRights.lstModules[0];
            //    if (GlobalModule.ModuleName == "Inventory")
            //    {
            //        if (GlobalModule.lstSubModules != null && GlobalModule.lstSubModules.Count > 0)
            //        {
            //            var GlobalSubModule = GlobalModule.lstSubModules.FirstOrDefault(i => i.SubModuleName == "Stockrooms");
            //            if (GlobalSubModule.listDialoges != null && GlobalSubModule.listDialoges.Count > 0)
            //            {
            //                var StockRoomParts = GlobalSubModule.listDialoges.FirstOrDefault(i => i.DialogName == "StockroomParts");
            //                var StockRoomTransactionDialog = StockRoomParts.listTab.FirstOrDefault(i => i.DialogTabName == "StockroomTransactionDialog");
            //                Application.Current.Properties["StockRoomTransactionDialog"] = StockRoomTransactionDialog.Expression;
                           
            //                if (StockRoomParts.Expression == "E" || StockRoomParts.Expression == "V")
            //                {
            //                    InventoryTransactionVisibility = true;
            //                }
            //                else
            //                {
            //                    InventoryTransactionVisibility = false;
            //                }
            //                //}
            //            }
            //        }
            //    }
            //}
        }

        public async Task GetStockroomsAuto()
        {
            if (string.IsNullOrWhiteSpace(this.SearchText))
            {
                PageNumber++;
                await GetStockrooms();
            }
        }
        async Task GetStockrooms()
        {
            try
            {
                OperationInProgress = true;
                var stockroomResponse = await _inventoryService.GetStockrooms(UserID, PageNumber.ToString(), RowCount.ToString(),"null");
                if (stockroomResponse != null && stockroomResponse.inventoryWrapper != null
                    && stockroomResponse.inventoryWrapper.stockrooms != null && stockroomResponse.inventoryWrapper.stockrooms.Count > 0)
                {

                    var stockrooms = stockroomResponse.inventoryWrapper.stockrooms;
                    await AddStockroomInStockroomCollection(stockrooms);
                    TotalRecordCount = stockroomResponse.inventoryWrapper.recordCountStockRooms;

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

        async Task GetStockroomsFromSearchBar()
        {
            try
            {
                OperationInProgress = true;
                var stockroomResponse = await _inventoryService.GetStockroomsFromSearchBar(this.SearchText,UserID);
                if (stockroomResponse.stockroomWrapper.stockRooms == null || stockroomResponse.stockroomWrapper.stockRooms.Count == 0)
                {
                    TotalRecordCount = 0;
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Thisstockroomdoesnotexist"), 2000);
                }
                if (stockroomResponse != null && stockroomResponse.stockroomWrapper != null
                    && stockroomResponse.stockroomWrapper.stockRooms != null && stockroomResponse.stockroomWrapper.stockRooms.Count > 0)
                {
                    var stockrooms = stockroomResponse.stockroomWrapper.stockRooms;
                    await AddStockroomInStockroomCollection(stockrooms);
                    TotalRecordCount = stockroomResponse.stockroomWrapper.stockRooms.Count;

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
        private async Task AddStockroomInStockroomCollection(List<Stockroom> stkrooms)
        {
            if (stkrooms != null && stkrooms.Count > 0)
            {
                foreach (var item in stkrooms)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _stockroomsCollection.Add(item);
                        OnPropertyChanged(nameof(StockroomsCollection));
                    });



                }

            }
        }


        private async Task RemoveAllStockroomsFromCollection()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                StockroomsCollection.Clear();
                OnPropertyChanged(nameof(StockroomsCollection));
            });



        }

        private async void OnSelectStockroomAsync(Stockroom item)
        {
            if (InventoryTransactionVisibility == true)
            {
                if (item != null)
                {
                    UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                   // OperationInProgress = true;
                    TargetNavigationData tnobj = new TargetNavigationData();
                    tnobj.StockroomID = item.StockroomID;
                    tnobj.StockroomName = item.StockroomName;
                    await NavigationService.NavigateToAsync<PartListingPageViewModel>(tnobj);
                    //OperationInProgress = false;
                    UserDialogs.Instance.HideLoading();
                }
            }
        }


        public async Task SearchStockroom()
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

                    ZXingScannerPage _scanner = new ZXingScannerPage(options)
                    {
                        DefaultOverlayTopText = "Align the barcode within the frame",
                        DefaultOverlayBottomText = string.Empty,
                        DefaultOverlayShowFlashButton = true
                    };

                    _scanner.OnScanResult += _scanner_OnScanResult;
                    var navPage = App.Current.MainPage as NavigationPage;
                    await navPage.PushAsync(_scanner);
                }

                else
                {
                    //reset pageno. and start search again.
                    await RefillStockroomCollection();

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
                await RefillStockroomCollection();


            });

        }
        private async Task RefillStockroomCollection()
        {
            PageNumber = 1;
            await RemoveAllStockroomsFromCollection();
            await GetStockroomsFromSearchBar();
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

        public async Task OnViewAppearingAsync(VisualElement view)
        {

            if (string.IsNullOrWhiteSpace(this.SearchText))
            {
                PageNumber = 1;
                await RemoveAllStockroomsFromCollection();
                await GetStockrooms();

            }
           
        }

        public async Task OnViewDisappearingAsync(VisualElement view)
        {
            this.SearchText = null;
        }
        #endregion
    }
}
