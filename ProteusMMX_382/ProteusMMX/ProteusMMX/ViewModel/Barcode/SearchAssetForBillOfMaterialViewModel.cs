using ProteusMMX.Helpers;
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

namespace ProteusMMX.ViewModel.Barcode
{
    public class SearchAssetForBillOfMaterialViewModel:ViewModelBase
    {
        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly IInventoryService _inventoryService;
        protected readonly IAssetModuleService _assetService;


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
        string _billOfMaterialTitle;
        public string BillOfMaterialTitle
        {
            get
            {
                return _billOfMaterialTitle;
            }

            set
            {
                if (value != _billOfMaterialTitle)
                {
                    _billOfMaterialTitle = value;
                    OnPropertyChanged("BillOfMaterialTitle");
                }
            }
        }

        string _billOfMaterialName;
        public string BillOfMaterialName
        {
            get
            {
                return _billOfMaterialName;
            }

            set
            {
                if (value != _billOfMaterialName)
                {
                    _billOfMaterialName = value;
                    OnPropertyChanged("BillOfMaterialName");
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
        string _partSize;
        public string PartSize
        {
            get
            {
                return _partSize;
            }

            set
            {
                if (value != _partSize)
                {
                    _partSize = value;
                    OnPropertyChanged("PartSize");
                }
            }
        }
        string _description;
        public string Description
        {
            get
            {
                return _description;
            }

            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged("Description");
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

      


        public ICommand ScanCommand => new AsyncCommand(SearchAssetForBOM);
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


        ObservableCollection<Part> _partsCollection = new ObservableCollection<Part>();

        public ObservableCollection<Part> PartsCollection
        {
            get
            {
                return _partsCollection;
            }

        }

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

                if (navigationData != null)
                {

                    var navigationParams = navigationData as TargetNavigationData;
                    this.StockroomID = navigationParams.StockroomID;

                }

                OperationInProgress = true;
                await SetTitlesPropertiesForPage();
               

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

        public SearchAssetForBillOfMaterialViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IInventoryService inventoryService, IAssetModuleService assetService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _inventoryService = inventoryService;
            _assetService = assetService;
        }

        public async Task SetTitlesPropertiesForPage()
        {


            PageTitle = WebControlTitle.GetTargetNameByTitleName("SearchScanAssetForBillOfMaterial");
            WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
            LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
            CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
            SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
            PartName = WebControlTitle.GetTargetNameByTitleName("PartName");
            PartNumber = WebControlTitle.GetTargetNameByTitleName("PartNumber");
            PartSize = WebControlTitle.GetTargetNameByTitleName("PartSize");
            Description = WebControlTitle.GetTargetNameByTitleName("Description");
            SerialNumber = WebControlTitle.GetTargetNameByTitleName("SerialNumber");
            SearchPlaceholder = WebControlTitle.GetTargetNameByTitleName("AssetNumber");
            GoTitle = WebControlTitle.GetTargetNameByTitleName("Go");
            ScanTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
            SearchButtonTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
            BillOfMaterialTitle= WebControlTitle.GetTargetNameByTitleName("BillOfMaterialName");
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




        
      
        async Task GetBOMParts()
        {
            try
            {
                OperationInProgress = true;
                var partResponse = await _inventoryService.GetBOMParts(this.SearchText);

                //var AssetResponse = await _assetService.GetAssetsFromSearchBar(this.SearchText, UserID);
                //if (AssetResponse.assetWrapper.assets == null || AssetResponse.assetWrapper.assets.Count == 0)
                //{
                //    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("ThisAssetdoesnotExist"), 2000);
                //    return;
                //}
                if (partResponse.AssetExist == false)
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("ThisAssetdoesnotExist"), 2000);
                    BillOfMaterialName = "";
                    return;
                }
                if (partResponse.partWrapper.parts == null || partResponse.partWrapper.parts.Count == 0)
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("NoBillofMaterialfoundforthisAsset"), 2000);
                    BillOfMaterialName = "";
                }

                if (partResponse != null && partResponse.partWrapper != null
                    && partResponse.partWrapper.parts != null && partResponse.partWrapper.parts.Count > 0)
                {

                    var parts = partResponse.partWrapper.parts;
               
                    BillOfMaterialName = partResponse.partWrapper.BillOfMaterialName;
                    
                    await AddBOMPartsInCollection(parts);

                }
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
        private async Task AddBOMPartsInCollection(List<Part> parts)
        {
            if (parts != null && parts.Count > 0)
            {
                foreach (var item in parts)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _partsCollection.Add(item);
                        OnPropertyChanged(nameof(PartsCollection));
                    });



                }

            }
        }




        private async Task RemoveAllBOMPartsFromCollection()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                PartsCollection.Clear();
                OnPropertyChanged(nameof(PartsCollection));
            });



        }


    
        public async Task SearchAssetForBOM()
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
                    await GetBOMPartsByAssetNumber();

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
                await GetBOMPartsByAssetNumber();


            });

        }
        private async Task GetBOMPartsByAssetNumber()
        {
          
            await RemoveAllBOMPartsFromCollection();
            await GetBOMParts();
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

        #endregion
    }
}
