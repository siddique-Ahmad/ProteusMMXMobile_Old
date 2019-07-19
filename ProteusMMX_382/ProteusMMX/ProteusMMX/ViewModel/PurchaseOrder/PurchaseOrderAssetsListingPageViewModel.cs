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
    public class PurchaseOrderAssetsListingPageViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
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

        string _assetName;
        public string AssetName
        {
            get
            {
                return _assetName;
            }

            set
            {
                if (value != _assetName)
                {
                    _assetName = value;
                    OnPropertyChanged("AssetName");
                }
            }
        }
        string _assetNumber;
        public string AssetNumber
        {
            get
            {
                return _assetNumber;
            }

            set
            {
                if (value != _assetNumber)
                {
                    _assetNumber = value;
                    OnPropertyChanged("AssetNumber");
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

        public ICommand PurchaseOrderAssetSelectedCommand => new Command<Model.PurchaseOrderModel.PurchaseOrderAssets>(OnSelectPurchaseOrderAssetsync);
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


        // ObservableCollection<Model.PurchaseOrderModel.PurchaseOrderAssets> _purchaseOrderAssetsCollection = new ObservableCollection<Model.PurchaseOrderModel.PurchaseOrderAssets>();

        ObservableCollection<Model.PurchaseOrderModel.PurchaseOrderAssets> _purchaseOrderAssetsCollection = new ObservableCollection<Model.PurchaseOrderModel.PurchaseOrderAssets>();
        public ObservableCollection<Model.PurchaseOrderModel.PurchaseOrderAssets> PurchaseOrderAssetsCollection
        {
            get
            {
                return _purchaseOrderAssetsCollection;
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
               // await GetPurchaseOrdersAssets();

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

        public PurchaseOrderAssetsListingPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IPurchaseOrderService purchaseOrderService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _purchaseOrderService = purchaseOrderService;
        }

        public async Task SetTitlesPropertiesForPage()
        {

           
                PageTitle = WebControlTitle.GetTargetNameByTitleName("POAsset");
                WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                PurchaseOrderNumber = WebControlTitle.GetTargetNameByTitleName("PurchaseOrderNumber");
                SearchPlaceholder = WebControlTitle.GetTargetNameByTitleName("ScanOrTypePurchaseOrderNo");
                GoTitle = WebControlTitle.GetTargetNameByTitleName("Go");
                ScanTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                SearchButtonTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                AssetName = WebControlTitle.GetTargetNameByTitleName("AssetName");
                AssetNumber = WebControlTitle.GetTargetNameByTitleName("AssetNumber");
                ReceivedDate = WebControlTitle.GetTargetNameByTitleName("ReceivedDate");
                ReceiverName = WebControlTitle.GetTargetNameByTitleName("ReceiverName");
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




        public async Task GetPurchaseOrdersAssetsAuto()
        {
            PageNumber++;
            await GetPurchaseOrdersAssets();
        }
        async Task GetPurchaseOrdersAssets()
        {
            try
            {
                OperationInProgress = true;
                var purchaseorderAssetsResponse = await _purchaseOrderService.GetPurchaseOrderDetailsByRequisitionID(this.RequisitionID.ToString(),UserID);
                if (purchaseorderAssetsResponse != null && purchaseorderAssetsResponse.poWrapper != null
                    && purchaseorderAssetsResponse.poWrapper.purchaseOrderAssets != null && purchaseorderAssetsResponse.poWrapper.purchaseOrderAssets.Count > 0)
                {

                    var purchaseorders = purchaseorderAssetsResponse.poWrapper.purchaseOrderAssets;
                    
                    await AddPOAssetsInPurchaseOrderAssetsCollection(purchaseorders);

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

       

        private async Task AddPOAssetsInPurchaseOrderAssetsCollection(List<Model.PurchaseOrderModel.PurchaseOrderAssets> poAsset)
        {
            if (poAsset != null && poAsset.Count > 0)
            {
                foreach (var item in poAsset)
                {
                   
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _purchaseOrderAssetsCollection.Add(item);
                        OnPropertyChanged(nameof(PurchaseOrderAssetsCollection));
                    });



                }

            }
        }


        private async Task RemoveAllPurchaseOrderFromCollection()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                PurchaseOrderAssetsCollection.Clear();
                OnPropertyChanged(nameof(PurchaseOrderAssetsCollection));
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
           // await GetPuchaseOrderFromSearchBar();
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



        private async void OnSelectPurchaseOrderAssetsync(Model.PurchaseOrderModel.PurchaseOrderAssets item)
        {
            if (item != null)
            {
               

               // OperationInProgress = true;
                if (item.ReceivedDate !=null)
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Assetisalreadyreceived"), 2000);
                    UserDialogs.Instance.HideLoading();

                    //    await App.Current.MainPage.DisplayAlert(WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("Assetisalreadyreceived"), WebControlTitle.GetTargetNameByTitleName("OK"));
                    return;
                }

                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.PurchaseOrderAssetID = item.PurchaseOrderAssetID;
                await NavigationService.NavigateToAsync<ReceivePuchaseOrderAssetViewModel>(tnobj);
                UserDialogs.Instance.HideLoading();

               // OperationInProgress = false;
            }
        }

        public async Task OnViewAppearingAsync(VisualElement view)
        {
            await RemoveAllPurchaseOrderFromCollection();
            await GetPurchaseOrdersAssets();
        }

        public async Task OnViewDisappearingAsync(VisualElement view)
        {

        }

        #endregion
    }
}
