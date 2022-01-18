using Acr.UserDialogs;
using ProteusMMX.Helpers;
using ProteusMMX.Model;
using ProteusMMX.Model.AssetModel;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Asset;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Navigation;
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


namespace ProteusMMX.ViewModel.Asset
{
    public class AssetListingPageViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {

        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly IAssetModuleService _assetService;

        protected readonly IWorkorderService _workorderService;

        public readonly INavigationService _navigationService;

        Page page;


        #endregion

        #region Properties


        bool _descriptionIsVisible = true;
        public bool DescriptionIsVisible
        {
            get
            {
                return _descriptionIsVisible;
            }

            set
            {
                if (value != _descriptionIsVisible)
                {
                    _descriptionIsVisible = value;
                    OnPropertyChanged(nameof(DescriptionIsVisible));
                }
            }
        }

        List<FormControl> _assetControlsNew = new List<FormControl>();
        public List<FormControl> AssetControlsNew
        {
            get
            {
                return _assetControlsNew;
            }

            set
            {
                _assetControlsNew = value;
            }
        }

        ServiceOutput _formControlsAndRights;
        public ServiceOutput FormControlsAndRights
        {
            get { return _formControlsAndRights; }
            set
            {
                if (value != _formControlsAndRights)
                {
                    _formControlsAndRights = value;

                }
            }
        }


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


        string _showAssetSystem;
        public string ShowAssetSystem
        {
            get
            {
                return _showAssetSystem;
            }

            set
            {
                if (value != _showAssetSystem)
                {
                    _showAssetSystem = value;
                    OnPropertyChanged("ShowAssetSystem");
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

        ObservableCollection<Assets> _assetsCollection = new ObservableCollection<Assets>();

        public ObservableCollection<Assets> AssetsCollection
        {
            get
            {
                return _assetsCollection;
            }

        }

        #endregion

        #region RightsProperties

        string Create;
        string Edit;


        #endregion


       

        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {

                //if (ConnectivityService.IsConnected == false)
                //{
                //    UserDialogs.Instance.HideLoading();
                //    await DialogService.ShowAlertAsync("internet not available", "Alert", "OK");
                //    return;

                //}

                if (navigationData != null)
                {


                    var navigationParams = navigationData as TargetNavigationData;
                    this.SearchText = navigationParams.SearchText;

                }
                UserDialogs.Instance.ShowLoading();
                //OperationInProgress = true;
                await SetTitlesPropertiesForPage();
                await GetAssetControlRights();
                if (Application.Current.Properties.ContainsKey("CreateAssetKey"))
                {
                    var CreateRights = Application.Current.Properties["CreateAssetKey"].ToString();
                    if (CreateRights != null)
                    {
                        Create = CreateRights.ToString();

                    }
                }
                if (Application.Current.Properties.ContainsKey("EditAssetKey"))
                {
                    var EditRights = Application.Current.Properties["EditAssetKey"].ToString();
                    if (EditRights != null)
                    {
                        Edit = EditRights.ToString();

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
                if (string.IsNullOrWhiteSpace(this.SearchText))
                {
                   
                }
                else
                {
                    await RemoveAllAssetsFromCollection();
                    await GetAssetsFromSearchBar();
                    return;
                }

            }
            catch (Exception ex)
            {
                //OperationInProgress = false;
                UserDialogs.Instance.HideLoading();

            }

            finally
            {
                UserDialogs.Instance.HideLoading();
                //OperationInProgress = false;
            }
        }


        public async Task GetAssetControlRights()
        {

            ServiceOutput FormControlsAndRightsForDetails = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "Assets", "Details");
            ServiceOutput FormControlsAndRightsForButton = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "Assets", "Assets");

            if (FormControlsAndRightsForButton != null && FormControlsAndRightsForButton.lstModules != null && FormControlsAndRightsForButton.lstModules.Count > 0)
            {
                var AssetModule = FormControlsAndRightsForButton.lstModules[0];
                if (AssetModule.ModuleName == "Assets") //ModuleName can't be  changed in service
                {
                    if (AssetModule.lstSubModules != null && AssetModule.lstSubModules.Count > 0)
                    {
                        var AssetSubModule = AssetModule.lstSubModules[0];
                        if (AssetSubModule.listControls != null && AssetSubModule.listControls.Count > 0)
                        {
                            try
                            {
                                Application.Current.Properties["CreateAssetKey"] = AssetSubModule.listControls.FirstOrDefault(i => i.ControlName == "New").Expression;
                                Application.Current.Properties["EditAssetKey"] = AssetSubModule.listControls.FirstOrDefault(i => i.ControlName == "Edit").Expression;
                                Application.Current.Properties["IssueWorkorderKey"] = AssetSubModule.listControls.FirstOrDefault(i => i.ControlName == "IssueWorkOrder").Expression;

                            }
                            catch (Exception ex)
                            {


                            }




                        }



                    }
                }
            }
            if (FormControlsAndRightsForDetails != null && FormControlsAndRightsForDetails.lstModules != null && FormControlsAndRightsForDetails.lstModules.Count > 0)
            {
                var AssetModule = FormControlsAndRightsForDetails.lstModules[0];
                if (AssetModule.ModuleName == "Details") //ModuleName can't be  changed in service
                {

                    if (AssetModule.lstSubModules != null && AssetModule.lstSubModules.Count > 0)
                    {
                        var AssetSubModule = AssetModule.lstSubModules[0];
                        if (AssetSubModule.listControls != null && AssetSubModule.listControls.Count > 0)
                        {
                            try
                            {
                                Application.Current.Properties["DescriptionTabKey"] = AssetSubModule.listControls.FirstOrDefault(i => i.ControlName == "Description").Expression;
                                Application.Current.Properties["AssetAdditionalDetailsTabKey"] = AssetSubModule.listControls.FirstOrDefault(i => i.ControlName == "AdditionalDetails").Expression;
                                Application.Current.Properties["AssetLocationDetailsTabKey"] = AssetSubModule.listControls.FirstOrDefault(i => i.ControlName == "Location").Expression;
                                // Application.Current.Properties["AssetDetailsTabKey"] = AssetSubModule;    
                                Application.Current.Properties["AssetsDetailsControls"] = AssetSubModule;
                            }
                            catch (Exception ex)
                            {


                            }



                        }



                    }
                }
            }

        }

        //public AssetListingPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IAssetModuleService assetService)
        //{
        //    _authenticationService = authenticationService;
        //    _formLoadInputService = formLoadInputService;
        //    _assetService = assetService;

        //}

        public AssetListingPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IAssetModuleService assetService, INavigationService navigationService, IWorkorderService workorderService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _assetService = assetService;
            _navigationService = navigationService;
            _workorderService = workorderService;
        }
        //public async Task NavigationFromAssetListing(string AssetSytemName,string AssetSystemNumber)
        //{
        //    TargetNavigationData tnobj = new TargetNavigationData();
        //    tnobj.Description = AssetSytemName +"     " + AssetSystemNumber;
        //    await NavigationService.NavigateToAsync<DescriptionViewModel>(tnobj);
        //}

        public async Task NavigationFromAssetListing(string AssetSystemID, string AssetSytemName, string AssetSystemNumber)
        {
            TargetNavigationData tnobj = new TargetNavigationData();
            tnobj.AssetSystemID = Convert.ToInt32(AssetSystemID);
            tnobj.AssetSystemName = AssetSytemName;
            tnobj.AssetSystemNumber = AssetSystemNumber;
            await NavigationService.NavigateToAsync<ShowAssetSystemViewModel>(tnobj);
        }
        public async Task SetTitlesPropertiesForPage()
        {

           
                PageTitle = WebControlTitle.GetTargetNameByTitleName("Assets");
                WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                AssetName = WebControlTitle.GetTargetNameByTitleName("AssetName");
                ShowAssetSystem = WebControlTitle.GetTargetNameByTitleName("show") + " " + WebControlTitle.GetTargetNameByTitleName("AssetSystem");
                AssetNumber = WebControlTitle.GetTargetNameByTitleName("AssetNumber");
                Description = WebControlTitle.GetTargetNameByTitleName("Description");
                CreateNewAsset = WebControlTitle.GetTargetNameByTitleName("CreateAsset");
                SearchPlaceholder = WebControlTitle.GetTargetNameByTitleName("SearchorScanByAssetNumberNameTag");
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
               // UserDialogs.Instance.ShowLoading();

                if (Create=="E")
                {
                    var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() { CreateNewAsset, LogoutTitle });

                    if (response == LogoutTitle)
                    {
                        await _authenticationService.LogoutAsync();
                        await NavigationService.NavigateToAsync<LoginPageViewModel>();
                        await NavigationService.RemoveBackStackAsync();
                    }

                    if (response == CreateNewAsset)
                    {
                        //TargetNavigationData tnobj = new TargetNavigationData();

                        //tnobj.WorkOrderId = this.WorkorderID;
                       // UserDialogs.Instance.ShowLoading();
                        await NavigationService.NavigateToAsync<CreateNewAssetPageViewModel>();

                    }
                }
                else if (Create == "V")
                {
                    var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() { CreateNewAsset, LogoutTitle });

                    if (response == LogoutTitle)
                    {
                        await _authenticationService.LogoutAsync();
                        await NavigationService.NavigateToAsync<LoginPageViewModel>();
                        await NavigationService.RemoveBackStackAsync();
                    }

                    if (response == CreateNewAsset)
                    {
                       

                    }
                }
                else
                {
                    var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() {LogoutTitle });

                    if (response == LogoutTitle)
                    {
                        await _authenticationService.LogoutAsync();
                        await NavigationService.NavigateToAsync<LoginPageViewModel>();
                        await NavigationService.RemoveBackStackAsync();
                    }

                  
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

       


        public async Task GetAssetsAuto()
        {
            if (string.IsNullOrWhiteSpace(this.SearchText))
            {
                PageNumber++;
                await GetAssets();
            }
        }
        async Task GetAssets()
        {
            try
            {
               // UserDialogs.Instance.ShowLoading();
               // OperationInProgress = true;
                var assetsResponse = await _assetService.GetAssets(UserID,PageNumber.ToString(),RowCount.ToString());
                if (assetsResponse != null && assetsResponse.assetWrapper != null
                    && assetsResponse.assetWrapper.assets != null && assetsResponse.assetWrapper.assets.Count > 0)
                {

                    var assets = assetsResponse.assetWrapper.assets;
                    await AddAssetsInAssetCollection(assets);
                    TotalRecordCount = assetsResponse.assetWrapper.assetCount;

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
        async Task GetAssetsFromSearchBar()
        {
            try
            {
               // UserDialogs.Instance.ShowLoading();
                OperationInProgress = true;
                var AssetResponse = await _assetService.GetAssetsFromSearchBar(this.SearchText, UserID);
                if (AssetResponse.assetWrapper.assets == null || AssetResponse.assetWrapper.assets.Count == 0)
                {
                    TotalRecordCount = 0;
                }
                if (AssetResponse != null && AssetResponse.assetWrapper != null
                    && AssetResponse.assetWrapper.assets != null && AssetResponse.assetWrapper.assets.Count > 0)
                {
                    var assets = AssetResponse.assetWrapper.assets;
                    await RemoveAllAssetsFromCollection();
                    await AddAssetsInAssetCollection(assets);
                    TotalRecordCount = AssetResponse.assetWrapper.assets.Count;
                    OperationInProgress = false;
                }
                else
                {
                    OnAlertYesNoClicked();
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
        private async Task AddAssetsInAssetCollection(List<Assets> assets)
        {
            if (assets != null && assets.Count > 0)
            {
                foreach (var item in assets)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _assetsCollection.Add(item);
                        OnPropertyChanged(nameof(AssetsCollection));
                    });



                }

            }
        }

        public async Task ReloadPageAfterSerchBoxCancle()
        {
            PageNumber = 1;
            await RemoveAllAssetsFromCollection();
            await GetAssets();
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
            Device.BeginInvokeOnMainThread(async () =>
            {
                var navPage = App.Current.MainPage as NavigationPage;
                await navPage.PopAsync();


                //reset pageno. and start search again.
                await RefillAssetCollection();


            });

        }
        public async Task RefillAssetCollection()
        {

            PageNumber = 1;
            await RemoveAllAssetsFromCollection();
            await GetAssetsFromSearchBar();
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
            var result = await DialogService.ShowConfirmAsync(WebControlTitle.GetTargetNameByTitleName("ThisassetdoesnotexistWouldyouliketocreatenewasset"), WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("Yes"), WebControlTitle.GetTargetNameByTitleName("No"));

          //  var action = await App.Current.MainPage.DisplayActionSheet(WebControlTitle.GetTargetNameByTitleName("Alert"), "No","Yes", "This asset doesn't exist,","Would you like to create new asset.");
            //  var answer = await App.Current.MainPage.DisplayAlert(WebControlTitle.GetTargetNameByTitleName("Alert"),WebControlTitle.GetTargetNameByTitleName("ThisassetdoesnotexistWouldyouliketocreatenewasset"),"Yes","No");
            if (result == true)
            {
                if (Create == "E")
                {
                    TargetNavigationData tnobj = new TargetNavigationData();
                    tnobj.SearchText = this.SearchText;
                    await NavigationService.NavigateToAsync<CreateNewAssetPageViewModel>(tnobj);
                }
            }


        }
        private async void OnSelectAssetsync(Assets item)
        {
            if (Edit == "E" || Edit == "V")
            {
                if (item != null)
                {
                    UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                    // OperationInProgress = true;
                    TargetNavigationData tnobj = new TargetNavigationData();
                    tnobj.AssetID = item.AssetID;

                    await NavigationService.NavigateToAsync<EditAssetPageViewModel>(tnobj);
                    OperationInProgress = false;
                    UserDialogs.Instance.HideLoading();
                }
            }
        }

        public async Task OnViewAppearingAsync(VisualElement view)
        {
       
            if (string.IsNullOrWhiteSpace(this.SearchText))
            {
                PageNumber = 1;
                await RemoveAllAssetsFromCollection();
                await GetAssets();
             
            }
        }

        public async Task OnViewDisappearingAsync(VisualElement view)
        {
            this.SearchText = null;
        }
        #endregion
    }
}
