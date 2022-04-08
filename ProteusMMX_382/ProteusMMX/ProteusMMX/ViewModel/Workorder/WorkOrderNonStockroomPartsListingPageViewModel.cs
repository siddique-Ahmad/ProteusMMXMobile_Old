using Acr.UserDialogs;
using ProteusMMX.Helpers;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Workorder;
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

namespace ProteusMMX.ViewModel.Workorder
{
    public class WorkOrderNonStockroomPartsListingPageViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {

        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected IWorkorderService _workorderService;


        #endregion

        #region Properties

        #region Page Properties

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

        int _workorderID;
        public int WorkorderID
        {
            get
            {
                return _workorderID;
            }

            set
            {
                if (value != _workorderID)
                {
                    _workorderID = value;
                    OnPropertyChanged(nameof(WorkorderID));
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

        string _quantityRequired;
        public string QuantityRequired
        {
            get
            {
                return _quantityRequired;
            }

            set
            {
                if (value != _quantityRequired)
                {
                    _quantityRequired = value;
                    OnPropertyChanged("QuantityRequired");
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
        string _createNonStockroomParts = "";
        public string CreateNonStockroomParts
        {
            get
            {
                return _createNonStockroomParts;
            }

            set
            {
                if (value != _createNonStockroomParts)
                {
                    _createNonStockroomParts = value;
                    OnPropertyChanged(nameof(_createNonStockroomParts));
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

        string AddParts;
        string EditParts;

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


        ObservableCollection<WorkOrderNonStockroomParts> _nonstockroomPartsCollection = new ObservableCollection<WorkOrderNonStockroomParts>();

        public ObservableCollection<WorkOrderNonStockroomParts> NonStockroomPartsCollection
        {
            get
            {
                return _nonstockroomPartsCollection;
            }

        }

        #endregion
        #endregion

        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);
        public ICommand ToolAddNewNSPartCommand => new AsyncCommand(AddNewNSPart);
        public ICommand ScanCommand => new AsyncCommand(ScanNonStockParts);
        public ICommand WorkorderNonStockroomPartSelectedCommand => new Command<WorkOrderNonStockroomParts>(OnSelectWorkorderNonStockroomPartAsync);

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

                    //var workorder = navigationParams.Parameter as workOrders;
                    this.WorkorderID = Convert.ToInt32(navigationData);



                }

                OperationInProgress = true;
                await SetTitlesPropertiesForPage();

                if (Device.RuntimePlatform == Device.UWP)
                {

                }
                else
                {
                    await GetWorkorderNonStockRoomParts();
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

        public WorkOrderNonStockroomPartsListingPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IWorkorderService workorderService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _workorderService = workorderService;
        }
        public WorkOrderNonStockroomPartsListingPageViewModel()
        {



        }
        public async Task SetTitlesPropertiesForPage()
        {


            PageTitle = WebControlTitle.GetTargetNameByTitleName("NonStockRoomParts");
            WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
            LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
            CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
            SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
            PartName = WebControlTitle.GetTargetNameByTitleName("PartName");
            PartNumber = WebControlTitle.GetTargetNameByTitleName("PartNumber");
            QuantityRequired = WebControlTitle.GetTargetNameByTitleName("QuantityRequired");
            CreateNonStockroomParts = WebControlTitle.GetTargetNameByTitleName("CreateNonStockroomParts");
            SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");
            ScanTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
            SearchButtonTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
            SearchPlaceholder = WebControlTitle.GetTargetNameByTitleName("NonStockParts");

        }
        public async Task ShowActions()
        {
            try
            {
                var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() {  LogoutTitle });

                if (response == LogoutTitle)
                {
                    await _authenticationService.LogoutAsync();
                    await NavigationService.NavigateToAsync<LoginPageViewModel>();
                    await NavigationService.RemoveBackStackAsync();
                }

                if (response == CreateNonStockroomParts)
                {
                    await NavigationService.NavigateToAsync<CreateNonStockroomPartsPageViewModel>(this.WorkorderID);
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

        public async Task AddNewNSPart()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                await Task.Delay(1000);
                await NavigationService.NavigateToAsync<CreateNonStockroomPartsPageViewModel>(this.WorkorderID);
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
            }

            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

        public async Task ScanNonStockParts()
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
                    await RefillNonStockPartsCollection();

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
        public async Task RefillNonStockPartsCollection()
        {

            PageNumber = 1;
            await RemoveAllNonStockPartsFromCollection();
            await GetWorkorderNonStockRoomPartsFromSearchBar();
        }

        async Task GetWorkorderNonStockRoomPartsFromSearchBar()
        {
            try
            {
                OperationInProgress = true;
                var workordersResponse = await _workorderService.GetWorkorderNonStockroomParts(WorkorderID, this.SearchText);
                if (workordersResponse != null && workordersResponse.workOrderWrapper != null
                   && workordersResponse.workOrderWrapper.workOrderNonStockroomParts != null && workordersResponse.workOrderWrapper.workOrderNonStockroomParts.Count > 0)
                {

                    var workordernonstkparts = workordersResponse.workOrderWrapper.workOrderNonStockroomParts;
                    await AddWorkorderNonStockroomPartsInWorkorderCollection(workordernonstkparts);

                }
                else
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Thispartdoesnotexist"), 2000);
                    return;
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
                await RefillNonStockPartsCollection();


            });

        }
        public async Task GetWorkorderNonStockRoomParts()
        {
            if (Application.Current.Properties.ContainsKey("WorkorderIDafterCreation"))
            {
                var workorderid = Application.Current.Properties["WorkorderIDafterCreation"].ToString();
                if (workorderid != null)
                {
                    WorkorderID = Convert.ToInt32(workorderid);

                }
            }
            if (Application.Current.Properties.ContainsKey("WorkorderService"))
            {
                IWorkorderService WorkorderService = Application.Current.Properties["WorkorderService"] as IWorkorderService;
                if (WorkorderService != null)
                {
                    _workorderService = WorkorderService;

                }
            }
            try
            {
                OperationInProgress = true;
                var workordersResponse = await _workorderService.GetWorkorderNonStockroomParts(this.WorkorderID, this.SearchText);
                if (workordersResponse != null && workordersResponse.workOrderWrapper != null
                    && workordersResponse.workOrderWrapper.workOrderNonStockroomParts != null && workordersResponse.workOrderWrapper.workOrderNonStockroomParts.Count > 0)
                {

                    var workordernonstkparts = workordersResponse.workOrderWrapper.workOrderNonStockroomParts;
                    await AddWorkorderNonStockroomPartsInWorkorderCollection(workordernonstkparts);

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

        public async Task ReloadPageAfterSerchBoxCancle()
        {
            PageNumber = 1;
            await RemoveAllNonStockPartsFromCollection();
            await GetWorkorderNonStockRoomParts();
        }
        private async Task RemoveAllNonStockPartsFromCollection()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                NonStockroomPartsCollection.Clear();
                OnPropertyChanged(nameof(NonStockroomPartsCollection));
            });



        }
        private async Task AddWorkorderNonStockroomPartsInWorkorderCollection(List<WorkOrderNonStockroomParts> nonstkparts)
        {
            if (nonstkparts != null && nonstkparts.Count > 0)
            {
                foreach (var item in nonstkparts)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _nonstockroomPartsCollection.Add(item);
                        OnPropertyChanged(nameof(NonStockroomPartsCollection));
                    });



                }

            }
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
        private async void OnSelectWorkorderNonStockroomPartAsync(WorkOrderNonStockroomParts item)
        {
            if (item != null)
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                // OperationInProgress = true;
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.WorkOrderNonStockroomPartID = item.WorkOrderNonStockroomPartID;
                tnobj.WorkOrderId = this.WorkorderID;
                await NavigationService.NavigateToAsync<EditNonStockroomPartsPageViewModel>(tnobj);
                UserDialogs.Instance.HideLoading();

                // OperationInProgress = false;
            }
        }

        public async Task OnViewAppearingAsync(VisualElement view)
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                await SetTitlesPropertiesForPage();
                NonStockroomPartsCollection.Clear();
                await GetWorkorderNonStockRoomParts();


            }

        }

        public async Task OnViewDisappearingAsync(VisualElement view)
        {
            this.SearchText = "";
        }





        #endregion
    }
}
