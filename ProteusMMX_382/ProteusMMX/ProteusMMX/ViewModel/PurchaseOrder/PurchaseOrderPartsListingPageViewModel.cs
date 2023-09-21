using Acr.UserDialogs;
using ProteusMMX.Helpers;
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
    public class PurchaseOrderPartsListingPageViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly IPurchaseOrderService _purchaseOrderService;


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
        int? _requisitionID;
        public int? RequisitionID
        {
            get
            {
                return _requisitionID;
            }

            set
            {
                if (value != _requisitionID)
                {
                    _requisitionID = value;
                    OnPropertyChanged(nameof(RequisitionID));
                }
            }
        }




        #endregion

        bool _disabledTextIsEnable = false;
        public bool DisabledTextIsEnable
        {
            get
            {
                return _disabledTextIsEnable;
            }

            set
            {
                if (value != _disabledTextIsEnable)
                {
                    _disabledTextIsEnable = value;
                    OnPropertyChanged(nameof(DisabledTextIsEnable));
                }
            }
        }


        string _disabledText = "";
        public string DisabledText
        {
            get
            {
                return _disabledText;
            }

            set
            {
                if (value != _disabledText)
                {
                    _disabledText = value;
                    OnPropertyChanged("DisabledText");
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

        #endregion

        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);

        public ICommand ScanCommand => new AsyncCommand(SearchPuchaseOrder);

        public ICommand PurchaseOrderPartSelectedCommand => new Command<Model.PurchaseOrderModel.PurchaseOrderParts>(OnSelectPurchaseOrderNonStockroomsync);
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
        string _receivedDate;
        public string ReceivedDate
        {
            get
            {
                return _receivedDate;
            }

            set
            {
                if (value != _receivedDate)
                {
                    _receivedDate = value;
                    OnPropertyChanged("ReceivedDate");
                }
            }
        }
        string _receiverName;
        public string ReceiverName
        {
            get
            {
                return _receiverName;
            }

            set
            {
                if (value != _receiverName)
                {
                    _receiverName = value;
                    OnPropertyChanged("ReceiverName");
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
        string _balanceDue;
        public string BalanceDue
        {
            get
            {
                return _balanceDue;
            }

            set
            {
                if (value != _balanceDue)
                {
                    _balanceDue = value;
                    OnPropertyChanged("BalanceDue");
                }
            }
        }

        string _quantityOrdered;
        public string QuantityOrdered
        {
            get
            {
                return _quantityOrdered;
            }

            set
            {
                if (value != _quantityOrdered)
                {
                    _quantityOrdered = value;
                    OnPropertyChanged("QuantityOrdered");
                }
            }
        }


        string _quantityReceived;
        public string QuantityReceived
        {
            get
            {
                return _quantityReceived;
            }

            set
            {
                if (value != _quantityReceived)
                {
                    _quantityReceived = value;
                    OnPropertyChanged("QuantityReceived");
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


        ObservableCollection<Model.PurchaseOrderModel.PurchaseOrderParts> _purchaseOrderPartsCollection = new ObservableCollection<Model.PurchaseOrderModel.PurchaseOrderParts>();

        public ObservableCollection<Model.PurchaseOrderModel.PurchaseOrderParts> PurchaseOrderPartsCollection
        {
            get
            {
                return _purchaseOrderPartsCollection;
            }

        }

        #endregion




        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {

               
                var navigationParams = navigationData as PageParameters;
                this.Page = navigationParams.Page;

                var purchaseorderParts = navigationParams.Parameter as Model.PurchaseOrderModel.PurchaseOrder;
                this.RequisitionID = purchaseorderParts.RequisitionID;

                OperationInProgress = true;
                await SetTitlesPropertiesForPage();
                //await GetPurchaseOrderParts();

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

        public PurchaseOrderPartsListingPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IPurchaseOrderService purchaseOrderService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _purchaseOrderService = purchaseOrderService;
        }

        public async Task SetTitlesPropertiesForPage()
        {

         
                PageTitle = WebControlTitle.GetTargetNameByTitleName("POParts");
                WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                PurchaseOrderNumber = WebControlTitle.GetTargetNameByTitleName("PurchaseOrderNumber");
                SearchPlaceholder = WebControlTitle.GetTargetNameByTitleName("ScanOrTypePurchaseOrderNo");
                GoTitle = WebControlTitle.GetTargetNameByTitleName("Go");
                ScanTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                SearchButtonTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                PartName = WebControlTitle.GetTargetNameByTitleName("PartName");
                PartNumber = WebControlTitle.GetTargetNameByTitleName("PartNumber");
                ReceivedDate = WebControlTitle.GetTargetNameByTitleName("ReceivedDate");
                ReceiverName = WebControlTitle.GetTargetNameByTitleName("ReceiverName");
                BalanceDue = WebControlTitle.GetTargetNameByTitleName("BalanceDue");
                QuantityOrdered = WebControlTitle.GetTargetNameByTitleName("QuantityOrdered");
                QuantityReceived = WebControlTitle.GetTargetNameByTitleName("QuantityReceived");
            SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");
            ShelfBin = WebControlTitle.GetTargetNameByTitleName("ShelfBin");




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




        public async Task GetPurchaseOrdersAuto()
        {
            PageNumber++;
            await GetPurchaseOrderParts();
        }
        async Task GetPurchaseOrderParts()
        {
            try
            {
                OperationInProgress = true;
                var purchaseOrderPartsResponse = await _purchaseOrderService.GetPurchaseOrderDetailsByRequisitionID(this.RequisitionID.ToString(), UserID);
                if (purchaseOrderPartsResponse != null && purchaseOrderPartsResponse.poWrapper != null
                    && purchaseOrderPartsResponse.poWrapper.purchaseOrderParts != null && purchaseOrderPartsResponse.poWrapper.purchaseOrderParts.Count > 0)
                {

                    var purchaseorderParts = purchaseOrderPartsResponse.poWrapper.purchaseOrderParts;
                    await AddPOPartsInPurchaseOrderPartsCollection(purchaseorderParts);

                }
                else
                {

                    DisabledText = "No record Found";//WebControlTitle.GetTargetNameByTitleName("ThisTabisDisabled");
                    DisabledTextIsEnable = true;
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

        private async Task AddPOPartsInPurchaseOrderPartsCollection(List<Model.PurchaseOrderModel.PurchaseOrderParts> poparts)
        {
            if (poparts != null && poparts.Count > 0)
            {
                while (PurchaseOrderPartsCollection.Count > 0)
                {
                    PurchaseOrderPartsCollection.RemoveAt(0);
                }
                   
                foreach (var item in poparts)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _purchaseOrderPartsCollection.Add(item);
                        OnPropertyChanged(nameof(PurchaseOrderPartsCollection));
                    });



                }

            }
        }

        private async Task RemoveAllPurchaseOrderFromCollection()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                PurchaseOrderPartsCollection.Clear();
                OnPropertyChanged(nameof(PurchaseOrderPartsCollection));
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
            //await GetPuchaseOrderFromSearchBar();
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



        private async void OnSelectPurchaseOrderNonStockroomsync(Model.PurchaseOrderModel.PurchaseOrderParts item)
        {
           // OperationInProgress = true;
            if (item != null)
            {
               

                
                if (item.BalanceDue == 0)
                {
                  
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Partisalreadyreceived"), 2000);
                    UserDialogs.Instance.HideLoading();
                    // await App.Current.MainPage.DisplayAlert(WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("Partisalreadyreceived"), WebControlTitle.GetTargetNameByTitleName("OK"));
                    return;
                }
               
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.PurchaseOrderPartID = item.PurchaseOrderPartID;
                tnobj.BalanceDue = item.BalanceDue;
                tnobj.RequisitionID = this.RequisitionID;
                tnobj.PartNumber = item.PartNumber;
                tnobj.ShelfBin = item.ShelfBin;
                await NavigationService.NavigateToAsync<ReceivePuchaseOrderStockroomPartViewModel>(tnobj);
               

               // OperationInProgress = false;
            }
          //  OperationInProgress = false;
        }

        public async Task OnViewAppearingAsync(VisualElement view)
        {
            await RemoveAllPurchaseOrderFromCollection();
            await GetPurchaseOrderParts();
        }

        public async Task OnViewDisappearingAsync(VisualElement view)
        {

        }

        #endregion
    }
}
