
using Acr.UserDialogs;
using ProteusMMX.Constants;
using ProteusMMX.Helpers;
using ProteusMMX.Model;
using ProteusMMX.Model.ClosedWorkOrderModel;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.CloseWorkorder;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.SelectionListPageServices;
using ProteusMMX.Services.SelectionListPageServices.Asset;
using ProteusMMX.Utils;
using ProteusMMX.Views.ClosedWorkorder;
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

namespace ProteusMMX.ViewModel.ClosedWorkorder
{
    public class ClosedWorkorderListingPageViewModel : ViewModelBase
    {


        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly ICloseWorkorderService _closeWorkorderService;

        Page page;


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
        string _workOrderNumberTitle;
        public string WorkOrderNumberTitle
        {
            get
            {
                return _workOrderNumberTitle;
            }

            set
            {
                if (value != _workOrderNumberTitle)
                {
                    _workOrderNumberTitle = value;
                    OnPropertyChanged("WorkOrderNumberTitle");
                }
            }
        }


        string _descriptionTitle;
        public string DescriptionTitle
        {
            get
            {
                return _descriptionTitle;
            }

            set
            {
                if (value != _descriptionTitle)
                {
                    _descriptionTitle = value;
                    OnPropertyChanged("DescriptionTitle");
                }
            }
        }


        string _workOrderTypeTitle;
        public string WorkOrderTypeTitle
        {
            get
            {
                return _workOrderTypeTitle;
            }

            set
            {
                if (value != _workOrderTypeTitle)
                {
                    _workOrderTypeTitle = value;
                    OnPropertyChanged("WorkOrderTypeTitle");
                }
            }
        }


        string _targetNameTitle;
        public string TargetNameTitle
        {
            get
            {
                return _targetNameTitle;
            }

            set
            {
                if (value != _targetNameTitle)
                {
                    _targetNameTitle = value;
                    OnPropertyChanged("TargetNameTitle");
                }
            }
        }
        string _workOrderNumber;
        public string WorkOrderNumber
        {
            get
            {
                return _workOrderNumber;
            }

            set
            {
                if (value != _workOrderNumber)
                {
                    _workOrderNumber = value;
                    OnPropertyChanged("WorkOrderNumber");
                }
            }
        }



        DateTime _startDate = DateTime.Now;
        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }

            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }
        }



        DateTime _endDate = DateTime.Now;
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }

            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }


        string _workOrderType;
        public string WorkOrderType
        {
            get
            {
                return _workOrderType;
            }

            set
            {
                if (value != _workOrderType)
                {
                    _workOrderType = value;
                    OnPropertyChanged("WorkOrderType");
                }
            }
        }


        string _targetName;
        public string TargetName
        {
            get
            {
                return _targetName;
            }

            set
            {
                if (value != _targetName)
                {
                    _targetName = value;
                    OnPropertyChanged("TargetName");
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
        
            string _listingSearchText;
        public string ListingSearchText
        {
            get
            {
                return _listingSearchText;
            }

            set
            {
                if (value != _listingSearchText)
                {
                    _listingSearchText = value;
                    OnPropertyChanged("ListingSearchText");
                    ListingSearchText_TextChanged();
                }
            }
        }
        string _searchCriteria;
        public string SearchCriteria
        {
            get
            {
                return _searchCriteria;
            }

            set
            {
                if (value != _searchCriteria)
                {
                    _searchCriteria = value;
                    OnPropertyChanged("SearchCriteria");
                    SearchText_TextChanged();
                }
            }
        }
        
        #endregion

        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);

        public ICommand ScanCommand => new AsyncCommand(SearchClosedWorkorder);
        public ICommand SearchListClosedWorkorder => new AsyncCommand(SearchListClosedWorkorders);
        public ICommand ClosedWorkorderSelectedCommand => new Command<ClosedWorkOrder>(OnSelectClosedWokrorderAssetsync);

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

        ObservableCollection<ClosedWorkOrder> _pickerItemCollection = new ObservableCollection<ClosedWorkOrder>();

        public ObservableCollection<ClosedWorkOrder> PickerItemCollection
        {
            get
            {
                return _pickerItemCollection;
            }

        }


        List<ClosedWorkOrder> _closedWorkorderCollection = new List<ClosedWorkOrder>();

        public List<ClosedWorkOrder> ClosedWorkorderCollection
        {
            get
            {
                return _closedWorkorderCollection;
            }

            set
            {
                _closedWorkorderCollection = value;
                OnPropertyChanged(nameof(ClosedWorkorderCollection));
            }
        }

        TAsset _nullItem = new TAsset() { AssetID = 0, AssetName = string.Empty };
        public TAsset NullItem
        {
            get
            {
                return _nullItem;
            }
        }

        #endregion
        List<ClosedWorkOrder> closedWorkorder;



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
                if (navigationData != null)
                {

                    var navigationParams = navigationData as TargetNavigationData;
                    this.SearchText = navigationParams.SearchText; 
                    this.SearchCriteria= navigationParams.SearchCriteria;
                    if(!string.IsNullOrWhiteSpace(navigationParams.Startdate) && !string.IsNullOrWhiteSpace(navigationParams.EndDate))
                    {
                        this.StartDate = Convert.ToDateTime(navigationParams.Startdate);
                        this.EndDate = Convert.ToDateTime(navigationParams.EndDate);
                    }
                   
                }
                await SetTitlesPropertiesForPage();
                if (Device.Idiom == TargetIdiom.Phone)
                {
                    this.TotalRecordForPhone = true;
                }
                else
                {
                    this.TotalRecordForTab = true;
                }
          
                if (this.SearchCriteria=="AssetNumber")
                {
                   await GetCloseWorkorderByAssetNumber();
                }
                else if (this.SearchCriteria == "PartNumber")
                {
                    await GetCloseWorkorderByPartNumber();
                }
                else if (this.SearchCriteria == "LocationName")
                {
                    await GetCloseWorkorderByLocation();
                }
                else if (this.SearchCriteria == "WorkorderNumber")
                {
                    await GetCloseWorkorderByWorkorderNumber();
                }
                else if (this.SearchCriteria == "ClosedDate")
                {
                    await GetCloseWorkorderByDate();
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

        public ClosedWorkorderListingPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, ICloseWorkorderService closedworkorderservice)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _closeWorkorderService = closedworkorderservice;
          

        }

        public async Task SetTitlesPropertiesForPage()
        {

           
                PageTitle = WebControlTitle.GetTargetNameByTitleName("ClosedWorkOrder");// + (WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName).PadLeft(86);
               // WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
              
                SearchPlaceholder = WebControlTitle.GetTargetNameByTitleName("SearchClosedWorkOrder");
                GoTitle = WebControlTitle.GetTargetNameByTitleName("Go");
                ScanTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                SearchButtonTitle = WebControlTitle.GetTargetNameByTitleName("Scan");


                WorkOrderNumberTitle = WebControlTitle.GetTargetNameByTitleName("WorkorderNumber");
                DescriptionTitle = WebControlTitle.GetTargetNameByTitleName("Description");
                WorkOrderTypeTitle = WebControlTitle.GetTargetNameByTitleName("WorkOrderType");
                TargetNameTitle = WebControlTitle.GetTargetNameByTitleName("Target") + " " + WebControlTitle.GetTargetNameByTitleName("Name");
                LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                TotalRecordTitle = WebControlTitle.GetTargetNameByTitleName("TotalRecords");
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




        public async Task GetClosedWorkordersAuto()
        {
           
                if (string.IsNullOrWhiteSpace(ListingSearchText))
                {

                    PageNumber++;
                    if (this.SearchCriteria == "AssetNumber")
                    {
                        await GetCloseWorkorderByAssetNumber();
                    }
                    else if (this.SearchCriteria == "PartNumber")
                    {
                        await GetCloseWorkorderByPartNumber();
                    }
                    else if (this.SearchCriteria == "LocationName")
                    {
                        await GetCloseWorkorderByLocation();
                    }
                    else if (this.SearchCriteria == "WorkorderNumber")
                    {
                        await GetCloseWorkorderByWorkorderNumber();
                    }
                    
                    else if (this.SearchCriteria == "ClosedDate")
                    {
                       
                            await GetCloseWorkorderByDate();
                       
                    }
                   
                   
                }
           

        }

       
                    
        //async Task GetClosedWorkorders()
        //{
        //    try
        //    {
        //        await AddClosedWorkorderinCollection(ClosedWorkorderCollection);

        //    }
        //    catch (Exception ex)
        //    {

        //        OperationInProgress = false;
        //    }

        //    finally
        //    {
        //        OperationInProgress = false;
        //    }
        //}
        async Task GetAssetsFromSearchBar()
        {
            try
            {
                //OperationInProgress = true;
                //var AssetResponse = await _assetService.GetAssetsFromSearchBar(this.SearchText, UserID);
                //if (AssetResponse != null && AssetResponse.assetWrapper != null
                //    && AssetResponse.assetWrapper.assets != null && AssetResponse.assetWrapper.assets.Count > 0)
                //{

                //    var assets = AssetResponse.assetWrapper.assets;
                //    await AddAssetsInAssetCollection(assets);

                //}
             
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
         public async Task ListingClosedWorkorderCollection()
        {
            try
            {

                var closedWorkorders = await _closeWorkorderService.ClosedWorkOrdersByWorkOrderNumber(this.ListingSearchText, "0", "0");
                closedWorkorder = closedWorkorders.clWorkOrderWrapper.clworkOrders;
                //List<ClosedWorkOrder> lstCWO1 = closedWorkorder.Where(i => i.WorkOrderNumber.Contains(ListingSearchText)).ToList();

                //  await RemoveAllClosedWorkorderFromCollection();
                PickerItemCollection.Clear();
                await AddClosedWorkorderinCollection(closedWorkorder);
                if (ListingSearchText != "")
                {
                    TotalRecordCount = closedWorkorder.Count;
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

        
        private async Task AddClosedWorkorderinCollection(List<ClosedWorkOrder> closedwk)
        {
            if (closedwk != null && closedwk.Count > 0)
            {
                foreach (var item in closedwk)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _pickerItemCollection.Add(item);
                        OnPropertyChanged(nameof(PickerItemCollection));
                    });



                }

            }
        }


        public async Task RemoveAllClosedWorkorderFromCollection()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                PickerItemCollection.Clear();
                OnPropertyChanged(nameof(PickerItemCollection));
            });



        }
        public async Task SearchListClosedWorkorders()
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
                    await ListingClosedWorkorderCollection();

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
        public async Task SearchClosedWorkorder()
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
                    await RefillClosedWorkorderCollection();

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
            //this.SearchText = result.Text;
            this.ListingSearchText = result.Text;

            ///Pop the scanner page
            Device.BeginInvokeOnMainThread(async () =>
            {
                var navPage = App.Current.MainPage as NavigationPage;
                await navPage.PopAsync();


                //reset pageno. and start search again.
                await ListingClosedWorkorderCollection();


            });

        }
        public async Task RefillClosedWorkorderCollection()
        {
            PageNumber = 1;
            await RemoveAllClosedWorkorderFromCollection();
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

        private void ListingSearchText_TextChanged()
        {

            try
            {
                if (ListingSearchText == null || ListingSearchText.Length == 0)
                {
                    SearchButtonTitle = ScanTitle;
                }

                else if (ListingSearchText.Length > 0)
                {
                    SearchButtonTitle = GoTitle;
                }
            }
            catch (Exception ex)
            {


            }
        }



        private async void OnSelectClosedWokrorderAssetsync(ClosedWorkOrder item)
        {
            if (item != null)
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                //OperationInProgress = true;
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.ClosedWorkorderID = item.ClosedWorkOrderID;
                Application.Current.Properties["ClosedWorkorderID"] = tnobj.ClosedWorkorderID;
                await NavigationService.NavigateToAsync<ClosedWorkorderTabbedPageViewModel>(item);
                //await NavigationService.NavigateToAsync<ClosedWorkorderTaskAndLabourPageViewModel>(tnobj);
                UserDialogs.Instance.HideLoading();

                // OperationInProgress = false;
            }
        }
        
        
        public async Task GetCloseWorkorderByAssetNumber()
        {

            try
            {

                var PartToSearch = new StockroomPartsSearch();
                PartToSearch.AssetNumber = this.SearchText;
                PartToSearch.PageNumber = PageNumber;
                PartToSearch.RowspPage = RowCount;

                var closedWorkorders = await _closeWorkorderService.GetClosedWorkOrdersByAssetNumber(PartToSearch);

              //  closedWorkorder = closedWorkorders.clWorkOrderWrapper.clworkOrders;
               
                //await AddClosedWorkorderinCollection(closedWorkorder);
                if (closedWorkorders.clWorkOrderWrapper.CLosedOrderCount != 0)
                {
                    var closedWork = closedWorkorders.clWorkOrderWrapper.clworkOrders;
                    await AddClosedWorkorderinCollection(closedWork);
                    TotalRecordCount = closedWorkorders.clWorkOrderWrapper.CLosedOrderCount;
                }
                if (closedWorkorders.clWorkOrderWrapper.clworkOrders == null || closedWorkorders.clWorkOrderWrapper.clworkOrders.Count == 0 && PageNumber == 1)
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("ThisAssetDoesNotAssociatedwithAnyClosedWorkOrder"), 2000);
                    return;
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
       
        public async Task GetCloseWorkorderByWorkorderNumber()
        {

            try
            {
      


                var closedWorkorders = await _closeWorkorderService.ClosedWorkOrdersByWorkOrderNumber(this.SearchText, PageNumber.ToString(), RowCount.ToString());

                closedWorkorder = closedWorkorders.clWorkOrderWrapper.clworkOrders;

                if (closedWorkorders.clWorkOrderWrapper.clworkOrders == null || closedWorkorders.clWorkOrderWrapper.clworkOrders.Count == 0 && PageNumber == 1)
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("ThisNumberDoestNotAssociatedwithAnyClosedWorkOrder"), 2000);
                }
              
                if (closedWorkorders.clWorkOrderWrapper.CLosedOrderCount != 0)
                {
                    await AddClosedWorkorderinCollection(closedWorkorder);
                    TotalRecordCount = closedWorkorders.clWorkOrderWrapper.CLosedOrderCount;
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

        public async Task GetCloseWorkorderByLocation()
        {

            try
            {         
                var closedWorkorders = await _closeWorkorderService.ClosedWorkOrdersByLocation(this.SearchText, PageNumber.ToString(), RowCount.ToString());

                closedWorkorder = closedWorkorders.clWorkOrderWrapper.clworkOrders;

                if (closedWorkorders.clWorkOrderWrapper.clworkOrders == null || closedWorkorders.clWorkOrderWrapper.clworkOrders.Count == 0 && PageNumber == 1)
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("ThisLocationDoestNotAssociatedwithAnyClosedWorkOrder"), 2000);
                }
              
                if (closedWorkorders.clWorkOrderWrapper.CLosedOrderCount != 0)
                {
                    await AddClosedWorkorderinCollection(closedWorkorder);
                    TotalRecordCount = closedWorkorders.clWorkOrderWrapper.CLosedOrderCount;
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

        public async Task GetCloseWorkorderByPartNumber()
        {

            try
            {
                //Crashes.GenerateTestCrash();
                var PartToSearch = new StockroomPartsSearch();
                PartToSearch.PartNumber = this.SearchText;
                PartToSearch.PageNumber = PageNumber;
                PartToSearch.RowspPage = RowCount;

                var closedWorkorders = await _closeWorkorderService.ClosedWorkOrdersByPartNumber(PartToSearch);

                closedWorkorder = closedWorkorders.clWorkOrderWrapper.clworkOrders;
                if (closedWorkorders.clWorkOrderWrapper.clworkOrders == null || closedWorkorders.clWorkOrderWrapper.clworkOrders.Count == 0 && PageNumber == 1)
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("ThisPartDoestNotAssociatedwithAnyClosedWorkOrder"), 2000);
                }
         
                if (closedWorkorders.clWorkOrderWrapper.CLosedOrderCount != 0)
                {
                    await AddClosedWorkorderinCollection(closedWorkorder);
                    TotalRecordCount = closedWorkorders.clWorkOrderWrapper.CLosedOrderCount;
                }
            }
            catch (Exception ex)
            {

                OperationInProgress = false;
               // Crashes.TrackError(ex);
            }

            finally
            {
                OperationInProgress = false;
            }

        }


        public async Task GetCloseWorkorderByDate()
        {

            try
            {

                //  Crashes.GenerateTestCrash();
                //var closedWorkorders = await _closeWorkorderService.ClosedWorkOrdersByClosedWorkOrderDate(this.UserID, StartDate.ToString("yyyy-MM-dd"), EndDate.ToString("yyyy-MM-dd"), PageNumber.ToString(), RowCount.ToString());

                //List<ClosedWorkOrder> closedWorkorder = closedWorkorders.clWorkOrderWrapper.clworkOrders;

                //await AddClosedWorkorderinCollection(closedWorkorder);

                string parsedFinalStartDate = string.Empty;
                DateTime dt;
                if (DateTime.TryParse(StartDate.ToString(), out dt))
                {
                    parsedFinalStartDate = dt.ToString("dd-MMM-yyyy hh:mmtt");
                }
                string parsedFinalEndDate = string.Empty;
                DateTime dt1;
                if (DateTime.TryParse(EndDate.ToString(), out dt1))
                {
                    parsedFinalEndDate = dt1.ToString("dd-MMM-yyyy hh:mmtt");
                }

              
                var closedWorkorders = await _closeWorkorderService.ClosedWorkOrdersByClosedWorkOrderDate(this.UserID, parsedFinalStartDate, parsedFinalEndDate, PageNumber.ToString(), RowCount.ToString());
               

                closedWorkorder = closedWorkorders.clWorkOrderWrapper.clworkOrders;

                if (closedWorkorders.clWorkOrderWrapper.clworkOrders == null || closedWorkorders.clWorkOrderWrapper.clworkOrders.Count == 0 && PageNumber==1)
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("BetweenthisdateThereisnoworkorderclosed"), 2000);
                }
                await AddClosedWorkorderinCollection(closedWorkorder);
                if (closedWorkorders.clWorkOrderWrapper.CLosedOrderCount != 0)
                {
                    TotalRecordCount = closedWorkorders.clWorkOrderWrapper.CLosedOrderCount;
                }
            }
            catch (Exception ex)
            {
               // Crashes.TrackError(ex);
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
