﻿using ProteusMMX.Helpers;
using ProteusMMX.Model.AssetModel;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Asset;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Asset;
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

namespace ProteusMMX.ViewModel.Barcode
{
    public class SearchAssetByAssetNumberViewModel:ViewModelBase
    {
        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly IAssetModuleService _assetService;

        Page page;


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

        int _assetID;
        public int AssetID
        {
            get
            {
                return _assetID;
            }

            set
            {
                if (value != _assetID)
                {
                    _assetID = value;
                    OnPropertyChanged(nameof(AssetID));
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

        #endregion

        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);

        public ICommand ScanCommand => new AsyncCommand(SearchAsset);

        public ICommand AssetSelectedCommand => new Command<Assets>(OnSelectAssetsync);

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


        ObservableCollection<Assets> _assetsCollection = new ObservableCollection<Assets>();

        public ObservableCollection<Assets> AssetsCollection
        {
            get
            {
                return _assetsCollection;
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

                    var navigationParams = navigationData as PageParameters;
                    this.Page = navigationParams.Page;

                }

                OperationInProgress = true;
                await SetTitlesPropertiesForPage();
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

        public SearchAssetByAssetNumberViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IAssetModuleService assetService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _assetService = assetService;

        }

        public async Task SetTitlesPropertiesForPage()
        {
                PageTitle = WebControlTitle.GetTargetNameByTitleName("Assets");
                WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                AssetName = WebControlTitle.GetTargetNameByTitleName("AssetName");
                AssetNumber = WebControlTitle.GetTargetNameByTitleName("AssetNumber");
                Description = WebControlTitle.GetTargetNameByTitleName("Description");
                CreateNewAsset = WebControlTitle.GetTargetNameByTitleName("CreateAsset");
                SearchPlaceholder = WebControlTitle.GetTargetNameByTitleName("SearchorScanByAssetNumberNameTag");
                GoTitle = WebControlTitle.GetTargetNameByTitleName("Go");
                ScanTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                SearchButtonTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");
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




    


        private async Task RemoveAllAssetsFromCollection()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                AssetsCollection.Clear();
                OnPropertyChanged(nameof(AssetsCollection));
            });



        }

        public async Task SearchAsset()
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
                    await GetAssetCollection();

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
                await GetAssetCollection();


            });

        }
        private async Task GetAssetCollection()
        {
            TargetNavigationData tnobj = new TargetNavigationData();
            tnobj.SearchText = this.SearchText;
           
            // await NavigationService.NavigateToAsync<SearchAssetFromBarcodeViewModel>(tnobj);
            await NavigationService.NavigateToAsync<AssetListingPageViewModel>(tnobj);
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


        async void OnAlertYesNoClicked()
        {
            var answer = await App.Current.MainPage.DisplayAlert(WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("ThisassetdoesnotexistWouldyouliketocreatenewasset"), "Yes", "No");
            if (answer == true)
            {


            }


        }
        private async void OnSelectAssetsync(Assets item)
        {
        }


        #endregion
    }
}
