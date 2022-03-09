using Acr.UserDialogs;
using Microsoft.AppCenter;
using ProteusMMX.Helpers;
using ProteusMMX.Model.AssetModel;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Services.Asset;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Navigation;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace ProteusMMX.ViewModel
{
    public class ShowAssetSystemViewModel : ViewModelBase
    {
        protected readonly IAssetModuleService _assetService;



        string _AssetSystemName;
        public string AssetSystemNameText
        {
            get
            {
                return _AssetSystemName;
            }

            set
            {
                if (value != _AssetSystemName)
                {
                    _AssetSystemName = value;
                    OnPropertyChanged(nameof(AssetSystemNameText));
                }
            }
        }

        string _assetSystemName;
        public string AssetSystemName
        {
            get
            {
                return _assetSystemName;
            }

            set
            {
                if (value != _assetSystemName)
                {
                    _assetSystemName = value;
                    OnPropertyChanged(nameof(AssetSystemName));
                }
            }
        }

        string _assetSystemNumber;
        public string AssetSystemNumber
        {
            get
            {
                return _assetSystemNumber;
            }

            set
            {
                if (value != _assetSystemNumber)
                {
                    _assetSystemNumber = value;
                    OnPropertyChanged(nameof(AssetSystemNumber));
                }
            }
        }

        string _AssetSystemNumber;
        public string AssetSystemNumberText
        {
            get
            {
                return _AssetSystemNumber;
            }

            set
            {
                if (value != _AssetSystemNumber)
                {
                    _AssetSystemNumber = value;
                    OnPropertyChanged(nameof(AssetSystemNumberText));
                }
            }
        }

        string _assetsystemID;
        public string AssetSystemID
        {
            get
            {
                return _assetsystemID;
            }

            set
            {
                if (value != _assetsystemID)
                {
                    _assetsystemID = value;
                    OnPropertyChanged("AssetSystemID");
                }
            }
        }

        //string _assetsystemNumber;
        //public string AssetSystemNumber
        //{
        //    get
        //    {
        //        return _assetsystemNumber;
        //    }

        //    set
        //    {
        //        if (value != _assetsystemNumber)
        //        {
        //            _assetsystemNumber = value;
        //            OnPropertyChanged("AssetSystemNumber");
        //        }
        //    }
        //}
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
        string _createNewAsset = "";
        public string CreateNewAsset
        {
            get
            {
                return _createNewAsset;
            }

            set
            {
                if (value != _createNewAsset)
                {
                    _createNewAsset = value;
                    OnPropertyChanged(nameof(CreateNewAsset));
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

        int _rowsPage = 5;
        public int RowsPage
        {
            get
            {
                return _rowsPage;
            }

            set
            {
                if (value != _rowsPage)
                {
                    _rowsPage = value;
                    OnPropertyChanged(nameof(RowsPage));
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

        public ICommand ScanCommand => new AsyncCommand(SearchAsset);

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                if (navigationData != null)
                {

                    var navigationParams = navigationData as TargetNavigationData;

                    //Set Facility
                    if (navigationParams.AssetSystemName != null)
                    {
                        this.AssetSystemNameText = navigationParams.AssetSystemName;
                        this.AssetSystemNumberText = navigationParams.AssetSystemNumber;
                        this.AssetSystemID = navigationParams.AssetSystemID.ToString();

                        if (string.IsNullOrWhiteSpace(this.AssetSystemID))
                        {
                            this.AssetSystemID = "null";
                        }
                        AssetSystemName = WebControlTitle.GetTargetNameByTitleName("AssetSystemName");
                        AssetSystemNumber = WebControlTitle.GetTargetNameByTitleName("AssetSystemNumber");
                        SearchPlaceholder = WebControlTitle.GetTargetNameByTitleName("SearchorScanByAssetNumberNameTag");
                        GoTitle = WebControlTitle.GetTargetNameByTitleName("Go");
                        ScanTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                        SearchButtonTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                        TotalRecordTitle = WebControlTitle.GetTargetNameByTitleName("TotalRecords");
                        SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                        AssetsByAssetSystem();
                    }

                }
            }
            catch
            {

            }
        }

        public async Task GetAssetsAuto()
        {
            if(string.IsNullOrWhiteSpace(this.SearchText))
            {
                PageNumber++;
                await AssetsByAssetSystem();
            }
           

        }

        public async Task searchBoxTextCler()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Please wait..", MaskType.Gradient);
                await Task.Delay(10);
                PageNumber = 1;
                await RemoveAllAssetsFromCollection();
                await GetAssetsFromSearchBar();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                //OperationInProgress = false;

            }

            finally
            {
                UserDialogs.Instance.HideLoading();
                //OperationInProgress = false;

            }
        }

        async Task GetAssetsFromSearchBar()
        {
            try
            {
                // UserDialogs.Instance.ShowLoading();
                OperationInProgress = true;
                var assetsResponse = await _assetService.AssetsByAssetSystemID(AssetSystemID,"0","0", AssetSystemNumberText, this.SearchText);

                if (assetsResponse != null && assetsResponse.assetforassWrapper != null
                   && assetsResponse.assetforassWrapper.assetForAS != null && assetsResponse.assetforassWrapper.assetForAS.Count > 0)
                {
                    var assetForAS = assetsResponse.assetforassWrapper.assetForAS;
                    await AddAssetsInAssetForASCollection(assetForAS);
                    TotalRecordCount = assetsResponse.assetforassWrapper.assetForAS.Count;
                }
                else
                {
                    TotalRecordCount = 0;
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("ThisAssetdoesnotExist"), 2000);
                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                OperationInProgress = false;
            }

            finally
            {
                UserDialogs.Instance.HideLoading();
                OperationInProgress = false;
            }
        }
        async Task AssetsByAssetSystem()
        {
            try
            {
                // UserDialogs.Instance.ShowLoading();
                // OperationInProgress = true;
                var assetsResponse = await _assetService.AssetsByAssetSystemID(AssetSystemID, PageNumber.ToString(), RowsPage.ToString(), AssetSystemNumberText,"null");

                if (assetsResponse != null && assetsResponse.assetforassWrapper != null
                  && assetsResponse.assetforassWrapper.assetForAS != null && assetsResponse.assetforassWrapper.assetForAS.Count > 0)
                {

                    var assetForAS = assetsResponse.assetforassWrapper.assetForAS;
                    await AddAssetsInAssetForASCollection(assetForAS);
                    TotalRecordCount = assetsResponse.assetforassWrapper.assetCount;

                }
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                OperationInProgress = false;
            }

            finally
            {
                UserDialogs.Instance.HideLoading();
                OperationInProgress = false;
            }
        }

        private async Task AddAssetsInAssetForASCollection(List<AssetForAS> assetForAS)
        {
            if (assetForAS != null && assetForAS.Count > 0)
            {
                foreach (var item in assetForAS)
                {
                    Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
                    {
                        _assetForAsCollection.Add(item);
                        OnPropertyChanged(nameof(AssetForASCollection));
                    });
                }
            }
        }

        ObservableCollection<AssetForAS> _assetForAsCollection = new ObservableCollection<AssetForAS>();

        public ObservableCollection<AssetForAS> AssetForASCollection
        {
            get
            {
                return _assetForAsCollection;
            }
        }

        public ShowAssetSystemViewModel(IAssetModuleService assetService)
        {

            _assetService = assetService;

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
        public async Task SearchAsset()
        {

            try
            {
                // UserDialogs.Instance.ShowLoading();
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
                    await RefillAssetCollection();

                }

                #endregion

                //await NavigationService.NavigateToAsync<WorkorderListingPageViewModel>();
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                OperationInProgress = false;

            }

            finally
            {
                UserDialogs.Instance.HideLoading();
                OperationInProgress = false;

            }
        }
        private async void _scanner_OnScanResult(ZXing.Result result)
        {
            //Set the text property
            this.SearchText = result.Text;

            ///Pop the scanner page
            Xamarin.Forms.Device.BeginInvokeOnMainThread(async () =>
            {
                var navPage = App.Current.MainPage as NavigationPage;
                await navPage.PopAsync();


                //reset pageno. and start search again.
                await RefillAssetCollection();


            });

        }
        private async Task RemoveAllAssetsFromCollection()
        {
            Xamarin.Forms.Device.BeginInvokeOnMainThread(() =>
            {
                AssetForASCollection.Clear();
                OnPropertyChanged(nameof(AssetForASCollection));
            });



        }
        private async Task RefillAssetCollection()
        {

            PageNumber = 1;
            await RemoveAllAssetsFromCollection();
            await GetAssetsFromSearchBar();
        }

    }

}

