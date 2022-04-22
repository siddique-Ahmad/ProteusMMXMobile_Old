using Acr.UserDialogs;
using ProteusMMX.Helpers;
using ProteusMMX.Model;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace ProteusMMX.ViewModel.Workorder
{
    public class WorkOrderStockroomPartsListingPageViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {

        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected IWorkorderService _workorderService;


        #endregion

        #region Properties
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

        string _addStockroompartTitle = "";
        public string AddStockroompartTitle
        {
            get
            {
                return _addStockroompartTitle;
            }

            set
            {
                if (value != _addStockroompartTitle)
                {
                    _addStockroompartTitle = value;
                    OnPropertyChanged("AddStockroompartTitle");
                }
            }
        }
        string _nonStockPartsTitle = "";
        public string NonStockPartsTitle
        {
            get
            {
                return _nonStockPartsTitle;
            }

            set
            {
                if (value != _nonStockPartsTitle)
                {
                    _nonStockPartsTitle = value;
                    OnPropertyChanged("NonStockPartsTitle");
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
        string Remove;



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


        ObservableCollection<WorkOrderStockroomParts> _stockroomPartsCollection = new ObservableCollection<WorkOrderStockroomParts>();

        public ObservableCollection<WorkOrderStockroomParts> StockroomPartsCollection
        {
            get
            {
                return _stockroomPartsCollection;
            }

        }

        #endregion
        #endregion

        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);

        public ICommand ToolAddNewSPartCommand => new AsyncCommand(AddNewSPart);
        public ICommand ScanCommand => new AsyncCommand(ScanParts);

        public ICommand AddPartCommand => new AsyncCommand(AddPart);

        public ICommand GetNonStockroomPartCommand => new AsyncCommand(GetNonStockroomParts);

        public ICommand WorkorderStockroomPartSelectedCommand => new Command<WorkOrderStockroomParts>(OnSelectWorkorderStockroomPartAsync);
        #endregion

        #region Rights Properties
        bool _stockroomPartIsEnabled = true;
        public bool StockroomPartIsEnabled
        {
            get
            {
                return _stockroomPartIsEnabled;
            }

            set
            {
                if (value != _stockroomPartIsEnabled)
                {
                    _stockroomPartIsEnabled = value;
                    OnPropertyChanged(nameof(StockroomPartIsEnabled));
                }
            }
        }
        bool _stockroomPartIsVisible = true;
        public bool StockroomPartIsVisible
        {
            get
            {
                return _stockroomPartIsVisible;
            }

            set
            {
                if (value != _stockroomPartIsVisible)
                {
                    _stockroomPartIsVisible = value;
                    OnPropertyChanged(nameof(StockroomPartIsVisible));
                }
            }
        }

        bool _nonStockroomPartIsVisible = true;
        public bool NonStockroomPartIsVisible
        {
            get
            {
                return _nonStockroomPartIsVisible;
            }

            set
            {
                if (value != _nonStockroomPartIsVisible)
                {
                    _nonStockroomPartIsVisible = value;
                    OnPropertyChanged(nameof(NonStockroomPartIsVisible));
                }
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

                    var navigationParams = navigationData as PageParameters;
                    this.Page = navigationParams.Page;

                    var workorder = navigationParams.Parameter as workOrders;
                    this.WorkorderID = workorder.WorkOrderID;



                }

                OperationInProgress = true;
                await SetTitlesPropertiesForPage();
                if (Application.Current.Properties.ContainsKey("AddParts"))
                {
                    AddParts = Application.Current.Properties["AddParts"].ToString();
                    if (AddParts == "E")
                    {
                        this.StockroomPartIsVisible = true;
                    }
                    else if (AddParts == "V")
                    {
                        this.StockroomPartIsEnabled = false;
                    }
                    else
                    {
                        this.StockroomPartIsVisible = false;
                    }
                }
                if (Application.Current.Properties.ContainsKey("EditParts"))
                {
                    EditParts = Application.Current.Properties["EditParts"].ToString();
                }
                if (Application.Current.Properties.ContainsKey("RemoveParts"))
                {
                    Remove = Application.Current.Properties["RemoveParts"].ToString();
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

        public WorkOrderStockroomPartsListingPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IWorkorderService workorderService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _workorderService = workorderService;


        }

        public WorkOrderStockroomPartsListingPageViewModel()
        {



        }


        public async Task SetTitlesPropertiesForPage()
        {


            PageTitle = WebControlTitle.GetTargetNameByTitleName("StockroomParts");
            WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
            LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
            CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
            SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
            PartName = WebControlTitle.GetTargetNameByTitleName("PartName");
            PartNumber = WebControlTitle.GetTargetNameByTitleName("PartNumber");
            QuantityRequired = WebControlTitle.GetTargetNameByTitleName("QuantityRequired");
            QuantityAllocated = WebControlTitle.GetTargetNameByTitleName("QuantityAllocated");
            AddStockroompartTitle = WebControlTitle.GetTargetNameByTitleName("Addstockroompart");
            SearchPlaceholder = WebControlTitle.GetTargetNameByTitleName("SearchOrScanpartsNo");
            NonStockPartsTitle = WebControlTitle.GetTargetNameByTitleName("NonStockParts");
            GoTitle = WebControlTitle.GetTargetNameByTitleName("Go");
            ScanTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
            SearchButtonTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
            SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");


        }
        public async Task ShowActions()
        {
            try
            {
                if (AddParts == "E")
                {
                    var response = await DialogService.SelectActionAsync("", SelectTitle, CancelTitle, new ObservableCollection<string>() { LogoutTitle });

                    if (response == AddStockroompartTitle)
                    {
                        await NavigationService.NavigateToAsync<CreateWorkOrderStockroomPartsViewModel>(this.WorkorderID);
                    }

                    if (response == LogoutTitle)
                    {
                        await _authenticationService.LogoutAsync();
                        await NavigationService.NavigateToAsync<LoginPageViewModel>();
                        await NavigationService.RemoveBackStackAsync();
                    }
                }
               else if (AddParts == "V")
                {
                    var response = await DialogService.SelectActionAsync("", SelectTitle, CancelTitle, new ObservableCollection<string>() { AddStockroompartTitle, LogoutTitle });

                    if (response == AddStockroompartTitle)
                    {
                        
                    }

                    if (response == LogoutTitle)
                    {
                        await _authenticationService.LogoutAsync();
                        await NavigationService.NavigateToAsync<LoginPageViewModel>();
                        await NavigationService.RemoveBackStackAsync();
                    }
                }
                else
                {
                    var response = await DialogService.SelectActionAsync("", SelectTitle, CancelTitle, new ObservableCollection<string>() {  LogoutTitle });

                   
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
                OperationInProgress = false;
            }

            finally
            {
                OperationInProgress = false;
            }
        }

        public async Task AddNewSPart()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                await Task.Delay(1000);
                if (AddParts == "E")
                {
                    
                        await NavigationService.NavigateToAsync<CreateWorkOrderStockroomPartsViewModel>(this.WorkorderID);
                   
                }
                else if (AddParts == "V")
                {
                }
                
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

        public async Task AddPart()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                // OperationInProgress = true;

                await NavigationService.NavigateToAsync<CreateWorkOrderStockroomPartsViewModel>(WorkorderID); //Pass the control here
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();

                //OperationInProgress = false;
            }

            finally
            {
                UserDialogs.Instance.HideLoading();

                // OperationInProgress = false;
            }
        }
        public async Task GetNonStockroomParts()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                //OperationInProgress = true;

                await NavigationService.NavigateToAsync<WorkOrderNonStockroomPartsListingPageViewModel>(WorkorderID); //Pass the control here
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();

                // OperationInProgress = false;
            }

            finally
            {
                UserDialogs.Instance.HideLoading();

                //OperationInProgress = false;
            }
        }


        //public async Task GetWorkorderToolsAuto()
        //{
        //    // PageNumber++;
        //    await GetWorkorderTools();
        //}
        async Task GetWorkorderStockRoomParts()
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
                var workordersResponse = await _workorderService.GetWorkorderStockroomParts(WorkorderID, "null");
                if (workordersResponse != null && workordersResponse.workOrderWrapper != null
                    && workordersResponse.workOrderWrapper.workOrderStockroomParts != null && workordersResponse.workOrderWrapper.workOrderStockroomParts.Count > 0)
                {

                    var workorderstkparts = workordersResponse.workOrderWrapper.workOrderStockroomParts;
                    await AddWorkorderStockroomPartsInWorkorderCollection(workorderstkparts);

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
        async Task GetWorkorderStockRoomPartsFromSearchBar()
        {
            try
            {
                OperationInProgress = true;
                var workordersResponse = await _workorderService.GetWorkorderStockroomParts(WorkorderID, this.SearchText);
                if (workordersResponse != null && workordersResponse.workOrderWrapper != null
                    && workordersResponse.workOrderWrapper.workOrderStockroomParts != null && workordersResponse.workOrderWrapper.workOrderStockroomParts.Count > 0)
                {

                    var workorderstkparts = workordersResponse.workOrderWrapper.workOrderStockroomParts;
                    await AddWorkorderStockroomPartsInWorkorderCollection(workorderstkparts);

                }
                else
                {
                    if (!String.IsNullOrWhiteSpace(this.SearchText))
                    {


                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Thispartdoesnotexist"), 2000);
                        return;
                    }
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

        private async Task AddWorkorderStockroomPartsInWorkorderCollection(List<WorkOrderStockroomParts> stkparts)
        {
            if (stkparts != null && stkparts.Count > 0)
            {

                while (StockroomPartsCollection.Count > 0)
                {
                    StockroomPartsCollection.RemoveAt(0);
                }

                foreach (var item in stkparts)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        StockroomPartsCollection.Add(item);
                        OnPropertyChanged(nameof(StockroomPartsCollection));
                    });



                }




            }
        }

        private async void OnSelectWorkorderStockroomPartAsync(WorkOrderStockroomParts item)
        {
            if (EditParts == "E" || EditParts == "V")
            {


                if (item != null)
                {
                    UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                    //  OperationInProgress = true;
                    TargetNavigationData tnobj = new TargetNavigationData();
                    tnobj.WorkOrderStockroomPartID = item.WorkOrderStockroomPartID;
                    tnobj.WorkOrderId = this.WorkorderID;
                    await NavigationService.NavigateToAsync<EditWorkOrderStockroomPartsViewModel>(tnobj);
                    //OperationInProgress = false;
                    UserDialogs.Instance.HideLoading();

                }
            }
        }

        public async Task ScanParts()
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
                    await RefillPartsCollection();

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
                await RefillPartsCollection();


            });

        }
        public async Task RefillPartsCollection()
        {

            PageNumber = 1;
            await RemoveAllPartsFromCollection();
            await GetWorkorderStockRoomPartsFromSearchBar();
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

        private async Task RemoveAllPartsFromCollection()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                StockroomPartsCollection.Clear();
                OnPropertyChanged(nameof(StockroomPartsCollection));
            });



        }

        public async Task OnViewAppearingAsync(VisualElement view)
        {
            await SetTitlesPropertiesForPage();
            if (Application.Current.Properties.ContainsKey("AddParts"))
            {
                AddParts = Application.Current.Properties["AddParts"].ToString();
                if (AddParts == "E")
                {
                    this.StockroomPartIsVisible = true;
                }
                else if (AddParts == "V")
                {
                    this.StockroomPartIsEnabled = false;
                }
                else
                {
                    this.StockroomPartIsVisible = false;
                }
            }
            if (Application.Current.Properties.ContainsKey("EditParts"))
            {
                EditParts = Application.Current.Properties["EditParts"].ToString();
            }
            if (Application.Current.Properties.ContainsKey("RemoveParts"))
            {
                Remove = Application.Current.Properties["RemoveParts"].ToString();
            }

            if (string.IsNullOrWhiteSpace(SearchText))
            {
                await RemoveAllPartsFromCollection();
                await GetWorkorderStockRoomParts();
            }

        }

        public async Task OnViewDisappearingAsync(VisualElement view)
        {
            this.SearchText = "";
        }

        #endregion

    }
}
