using Acr.UserDialogs;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.StringFormatter;
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
    public class PartListingPageViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly IInventoryService _inventoryService;


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
        bool _StockRoomTransactionDialogVisibility;
        public bool StockRoomTransactionDialogVisibility
        {
            get
            {
                return _StockRoomTransactionDialogVisibility;
            }

            set
            {
                if (value != _StockRoomTransactionDialogVisibility)
                {
                    _StockRoomTransactionDialogVisibility = value;
                    OnPropertyChanged("StockRoomTransactionDialogVisibility");
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

        int? _stockroomID;
        public int? StockroomID
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
                    OnPropertyChanged(nameof(StockroomName));
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
        string _partNameTitle;
        public string PartNameTitle
        {
            get
            {
                return _partNameTitle;
            }

            set
            {
                if (value != _partNameTitle)
                {
                    _partNameTitle = value;
                    OnPropertyChanged("PartNameTitle");
                }
            }
        }

        string _partNumberTitle;
        public string PartNumberTitle
        {
            get
            {
                return _partNumberTitle;
            }

            set
            {
                if (value != _partNumberTitle)
                {
                    _partNumberTitle = value;
                    OnPropertyChanged("PartNumberTitle");
                }
            }
        }

        string _quantityOnHandTitle;
        public string QuantityOnHandTitle
        {
            get
            {
                return _quantityOnHandTitle;
            }

            set
            {
                if (value != _quantityOnHandTitle)
                {
                    _quantityOnHandTitle = value;
                    OnPropertyChanged("QuantityOnHandTitle");
                }
            }
        }

        string _quantityAllocatedTitle;
        public string QuantityAllocatedTitle
        {
            get
            {
                return _quantityAllocatedTitle;
            }

            set
            {
                if (value != _quantityAllocatedTitle)
                {
                    _quantityAllocatedTitle = value;
                    OnPropertyChanged("QuantityAllocatedTitle");
                }
            }
        }
        string _serialNumberTitle;
        public string SerialNumberTitle
        {
            get
            {
                return _serialNumberTitle;
            }

            set
            {
                if (value != _serialNumberTitle)
                {
                    _serialNumberTitle = value;
                    OnPropertyChanged("SerialNumberTitle");
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

        string _partName;
        public string PartName
        {
            get
            {
                return _partName;
            }

            set
            {
                if (value != _partName)
                {
                    _partName = value;
                    OnPropertyChanged("PartName");
                }
            }
        }

        string _partNumber;
        public string PartNumber
        {
            get
            {
                return _partNumber;
            }

            set
            {
                if (value != _partNumber)
                {
                    _partNumber = value;
                    OnPropertyChanged("PartNumber");
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

        string _quantityAllocated;
        public string QuantityAllocated
        {
            get
            {
                return _quantityAllocated;
            }

            set
            {
                if (value != _quantityAllocated)
                {
                    _quantityAllocated = value;
                    OnPropertyChanged("QuantityAllocated");
                }
            }
        }
        string _serialNumber;
        public string SerialNumber
        {
            get
            {
                return _serialNumber;
            }

            set
            {
                if (value != _serialNumber)
                {
                    _serialNumber = value;
                    OnPropertyChanged("SerialNumber");
                }
            }
        }

        string _shelfBin;
        public string ShelfBin
        {
            get
            {
                return _shelfBin;
            }

            set
            {
                if (value != _shelfBin)
                {
                    _shelfBin = value;
                    OnPropertyChanged("ShelfBin");
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

        public ICommand StockroomPartSelectedCommand => new Command<StockroomPart>(OnSelectStockroomPartAsync);


        public ICommand ScanCommand => new AsyncCommand(SearchPart);
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


        ObservableCollection<StockroomPart> _stockroomPartsCollection = new ObservableCollection<StockroomPart>();

        public ObservableCollection<StockroomPart> StockroomPartsCollection
        {
            get
            {
                return _stockroomPartsCollection;
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

                    var navigationParams = navigationData as TargetNavigationData;
                    this.StockroomID = navigationParams.StockroomID;
                    this.StockroomName = navigationParams.StockroomName;

                }

                OperationInProgress = true;
                await SetTitlesPropertiesForPage();
                await CreateControlsForPage();

                await GetStockroomParts();
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
        public async Task CreateControlsForPage()
        {

            if (Application.Current.Properties.ContainsKey("StockroomTransactionDialog"))
            {
                var StockRoomTransactionVisibility = Application.Current.Properties["StockroomTransactionDialog"].ToString();
                if (StockRoomTransactionVisibility == "E" || StockRoomTransactionVisibility == "V")
                {
                    StockRoomTransactionDialogVisibility = true;

                }
                else
                {
                    StockRoomTransactionDialogVisibility = false;
                }
            }
           
        }






        public PartListingPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IInventoryService inventoryService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _inventoryService = inventoryService;
        }

        public async Task SetTitlesPropertiesForPage()
        {


            PageTitle = StockroomName;// + (WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName).PadLeft(104); 
            //WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
            LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
            CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
            SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
            PartName = WebControlTitle.GetTargetNameByTitleName("PartName");
            PartNumber = WebControlTitle.GetTargetNameByTitleName("PartNumber");
            QuantityOnHand = WebControlTitle.GetTargetNameByTitleName("QuantityOnHand");
            QuantityAllocated = WebControlTitle.GetTargetNameByTitleName("QuantityAllocated");
            SerialNumber = WebControlTitle.GetTargetNameByTitleName("SerialNumber");
            ShelfBin = WebControlTitle.GetTargetNameByTitleName("ShelfBin");
            SearchPlaceholder = WebControlTitle.GetTargetNameByTitleName("SearchOrScanPartNumberName");
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




        public async Task GetStockroomPartsAuto()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                PageNumber++;
                await GetStockroomParts();
            }
        }
        async Task GetStockroomParts()
        {
            try
            {
                OperationInProgress = true;
                var stockroomPartResponse = await _inventoryService.GetStockroomParts(StockroomID.ToString(), PageNumber.ToString(), RowCount.ToString());
                if (stockroomPartResponse != null && stockroomPartResponse.inventoryWrapper != null
                    && stockroomPartResponse.inventoryWrapper.stockroomparts != null && stockroomPartResponse.inventoryWrapper.stockroomparts.Count > 0)
                {

                    var stockroomparts = stockroomPartResponse.inventoryWrapper.stockroomparts;
                    //List<StockroomPart> finalstockroompart = stockroomparts.Select(a => new StockroomPart() { PartName = a.PartName, PartNumber = a.PartNumber,QuantityOnHand = a.QuantityOnHand,SerialNumber =a.SerialNumber ,ShelfBin = a.ShelfBin, QuantityAllocated = decimal.Parse(string.Format(StringFormat.NumericZero(), string.IsNullOrWhiteSpace(a.QuantityAllocated.ToString()) ? 0 : decimal.Parse(a.QuantityAllocated.ToString()))) }).ToList();
                    await AddStockroomPartsInStockroomPartsCollection(stockroomparts);
                    TotalRecordCount = stockroomPartResponse.inventoryWrapper.recordCountStockRoomParts;
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
        async Task GetStockroomPartFromSearchBar()
        {
            try
            {
                OperationInProgress = true;
                var stockroompartResponse = await _inventoryService.GetStockroomPartFromSearchBar(this.StockroomID.ToString(), this.SearchText, UserID);
                if (stockroompartResponse.partWrapper.parts == null || stockroompartResponse.partWrapper.parts.Count == 0)
                {
                    TotalRecordCount = 0;
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Thispartdoesnotexist"), 2000);
                }
                if (stockroompartResponse != null && stockroompartResponse.partWrapper != null
                    && stockroompartResponse.partWrapper.parts != null && stockroompartResponse.partWrapper.parts.Count > 0)
                {

                    var stockroomparts = stockroompartResponse.partWrapper.parts;
                    List<StockroomPart> finalstockroompart = stockroomparts.Select(a => new StockroomPart() { PartName = a.PartName, PartNumber = a.PartNumber, QuantityOnHand = a.QuantityOnHand, QuantityAllocated = a.QuantityAllocated, SerialNumber = a.SerialNumber, ShelfBin = a.ShelfBin, StockroomPartID = a.StockroomPartID }).ToList();
                    await AddStockroomPartsInStockroomPartsCollection(finalstockroompart);
                    TotalRecordCount = stockroompartResponse.partWrapper.parts.Count;


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
        private async Task AddStockroomPartsInStockroomPartsCollection(List<StockroomPart> stkroomparts)
        {
            if (stkroomparts != null && stkroomparts.Count > 0)
            {
                foreach (var item in stkroomparts)
                {
                    bool checkDuplicate = StockroomPartsCollection.Any(sp => sp.StockroomPartID == item.StockroomPartID);
                    if (checkDuplicate == true)
                    {

                    }
                    else
                    {
                        Device.BeginInvokeOnMainThread(() =>
                        {

                            _stockroomPartsCollection.Add(item);
                            OnPropertyChanged(nameof(StockroomPartsCollection));

                        });

                    }



                }

            }
        }




        private async Task RemoveAllStockroomsPartsFromCollection()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                StockroomPartsCollection.Clear();
                OnPropertyChanged(nameof(StockroomPartsCollection));
            });



        }


        private async void OnSelectStockroomPartAsync(StockroomPart item)
        {

            if (!AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Professional") && StockRoomTransactionDialogVisibility == true)
            {
                if (item != null)
                {
                    UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                    //  OperationInProgress = true;
                    TargetNavigationData tnobj = new TargetNavigationData();
                    tnobj.StockroomPartID = item.StockroomPartID;
                    tnobj.QuantityAllocated = item.QuantityAllocated;
                    tnobj.ShelfBin = item.ShelfBin;
                    if (item.SerialNumber != null)
                    {
                        UserDialogs.Instance.HideLoading();
                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("TransactionCannotbeperformedonSerializedpart"), 2000);
                        return;
                    }

                    await NavigationService.NavigateToAsync<InventoryTransactionPageViewModel>(tnobj);
                    UserDialogs.Instance.HideLoading();
                    //OperationInProgress = false;
                }
            }
        }

        public async Task SearchPart()
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
                    await RefillPartCollection();

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
                await RefillPartCollection();


            });

        }
        private async Task RefillPartCollection()
        {
            PageNumber = 1;
            await RemoveAllStockroomsPartsFromCollection();
            await GetStockroomPartFromSearchBar();
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
            //if (string.IsNullOrWhiteSpace(this.SearchText))
            //{
            //    PageNumber = 1;
            //    await RemoveAllStockroomsPartsFromCollection();
            //    await GetStockroomParts();
            //}
            if (Application.Current.Properties.ContainsKey("CallfromTransactionPage"))
            {
                var TransactionPageCall = Application.Current.Properties["CallfromTransactionPage"].ToString();
                if (TransactionPageCall == "true")
                {
                    Application.Current.Properties["CallfromTransactionPage"] = "false";
                    PageNumber = 1;
                    await RemoveAllStockroomsPartsFromCollection();
                    await GetStockroomParts();


                }

            }

        }
        public async Task ReloadPageAfterSerchBoxCancle()
        {
            PageNumber = 1;
            await RemoveAllStockroomsPartsFromCollection();
            await GetStockroomParts();
        }
        public async Task OnViewDisappearingAsync(VisualElement view)
        {
            this.SearchText = null;
        }

        #endregion
    }
}
