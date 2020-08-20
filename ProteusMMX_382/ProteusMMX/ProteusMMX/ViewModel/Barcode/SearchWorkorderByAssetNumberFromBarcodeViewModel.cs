using Acr.UserDialogs;
using ProteusMMX.Constants;
using ProteusMMX.Helpers;
using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.Workorder;
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
    public class SearchWorkorderByAssetNumberFromBarcodeViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {

        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly IWorkorderService _workorderService;


        #endregion

        #region Properties
        string EditRights;
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


        #endregion

        #region Title Properties
        bool? _inspectionUser = AppSettings.User.IsInspectionUser;
        public bool? InspectionUser
        {
            get { return _inspectionUser; }
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
                    OnPropertyChanged(nameof(FormControlsAndRights));
                }
            }
        }
        string CreateWorkorderRights;
        string CloseWorkOrderRights;


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


        string _workorderTypeLabelTitle;
        public string WorkorderTypeLabelTitle
        {
            get
            {
                return _workorderTypeLabelTitle;
            }

            set
            {
                if (value != _workorderTypeLabelTitle)
                {
                    _workorderTypeLabelTitle = value;
                    OnPropertyChanged("WorkorderTypeLabelTitle");
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

        string _WorkStartedDateTitle;
        public string WorkStartedDateTitle
        {
            get
            {
                return _WorkStartedDateTitle;
            }

            set
            {
                if (value != _WorkStartedDateTitle)
                {
                    _WorkStartedDateTitle = value;
                    OnPropertyChanged("WorkStartedDateTitle");
                }
            }
        }
        string _WorkCompletionDateTitle;
        public string WorkCompletionDateTitle
        {
            get
            {
                return _WorkCompletionDateTitle;
            }

            set
            {
                if (value != _WorkCompletionDateTitle)
                {
                    _WorkCompletionDateTitle = value;
                    OnPropertyChanged("WorkCompletionDateTitle");
                }
            }
        }
        string _WorkRequestedDateTitle;
        public string WorkRequestedDateTitle
        {
            get
            {
                return _WorkRequestedDateTitle;
            }

            set
            {
                if (value != _WorkRequestedDateTitle)
                {
                    _WorkRequestedDateTitle = value;
                    OnPropertyChanged("WorkRequestedDateTitle");
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


        string _requiredDateTitle;
        public string RequiredDateTitle
        {
            get
            {
                return _requiredDateTitle;
            }

            set
            {
                if (value != _requiredDateTitle)
                {
                    _requiredDateTitle = value;
                    OnPropertyChanged("RequiredDateTitle");
                }
            }
        }


        string _activationDateTitle;
        public string ActivationDateTitle
        {
            get
            {
                return _activationDateTitle;
            }

            set
            {
                if (value != _activationDateTitle)
                {
                    _activationDateTitle = value;
                    OnPropertyChanged("ActivationDateTitle");
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

        string _createWorkorderTitle;
        public string CreateWorkorderTitle
        {
            get
            {
                return _createWorkorderTitle;
            }

            set
            {
                if (value != _createWorkorderTitle)
                {
                    _createWorkorderTitle = value;
                    OnPropertyChanged("CreateWorkorderTitle");
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

        bool _isLocationCallFrombarcodePage;
        public bool IsLocationCallFrombarcodePage
        {
            get
            {
                return _isLocationCallFrombarcodePage;
            }

            set
            {
                if (value != _isLocationCallFrombarcodePage)
                {
                    _isLocationCallFrombarcodePage = value;
                    OnPropertyChanged("IsLocationCallFrombarcodePage");

                }
            }
        }
        bool _isAssetCallFrombarcodePage;
        public bool IsAssetCallFrombarcodePage
        {
            get
            {
                return _isAssetCallFrombarcodePage;
            }

            set
            {
                if (value != _isAssetCallFrombarcodePage)
                {
                    _isAssetCallFrombarcodePage = value;
                    OnPropertyChanged("IsAssetCallFrombarcodePage");

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

        #region Picker Properties

        ObservableCollection<string> _pickerTitles;

        public ObservableCollection<string> PickerTitles
        {
            get
            {
                return _pickerTitles;
            }

            set
            {
                if (value != _pickerTitles)
                {
                    _pickerTitles = value;
                    OnPropertyChanged("PickerTitles");

                }
            }

        }

        string _selectedPickerText;
        public string SelectedPickerText
        {
            get
            {
                return _selectedPickerText;
            }

            set
            {
                if (value != _selectedPickerText)
                {
                    _selectedPickerText = value;
                    OnPropertyChanged("SelectedPickerText");
                }
            }
        }

        string _workorderTypeFilterText;
        public string WorkorderTypeFilterText
        {
            get
            {
                return _workorderTypeFilterText;
            }

            set
            {
                _workorderTypeFilterText = value;
                OnPropertyChanged(nameof(WorkorderTypeFilterText));
            }
        }

        string _preventiveMaintenenceTitle;
        public string PreventiveMaintenenceTitle
        {
            get
            {
                return _preventiveMaintenenceTitle;
            }

            set
            {
                if (value != _preventiveMaintenenceTitle)
                {
                    _preventiveMaintenenceTitle = value;
                    OnPropertyChanged("PreventiveMaintenenceTitle");
                }
            }
        }

        string _demandMaintenenceTitle;
        public string DemandMaintenenceTitle
        {
            get
            {
                return _demandMaintenenceTitle;
            }

            set
            {
                if (value != _demandMaintenenceTitle)
                {
                    _demandMaintenenceTitle = value;
                    OnPropertyChanged("DemandMaintenenceTitle");
                }
            }
        }


        string _emergencyMaintenanceTitle;
        public string EmergencyMaintenanceTitle
        {
            get
            {
                return _emergencyMaintenanceTitle;
            }

            set
            {
                if (value != _emergencyMaintenanceTitle)
                {
                    _emergencyMaintenanceTitle = value;
                    OnPropertyChanged("EmergencyMaintenanceTitle");
                }
            }
        }


        int _selectedIndexPicker = -1;
        public int SelectedIndexPicker
        {
            get
            {
                return _selectedIndexPicker;
            }

            set
            {
                _selectedIndexPicker = value;
                OnPropertyChanged("SelectedIndexPicker");
                RefillWororderFromPicker();
            }
        }






        #endregion

        #region Sorting ToolbarItem Properties

        string _sortByActivationdateTitle;
        public string SortByActivationdateTitle
        {
            get
            {
                return _sortByActivationdateTitle;
            }

            set
            {
                if (value != _sortByActivationdateTitle)
                {
                    _sortByActivationdateTitle = value;
                    OnPropertyChanged(nameof(SortByActivationdateTitle));
                }
            }
        }


        string _noneTitle;
        public string NoneTitle
        {
            get
            {
                return _noneTitle;
            }

            set
            {
                if (value != _noneTitle)
                {
                    _noneTitle = value;
                    OnPropertyChanged(nameof(NoneTitle));
                }
            }
        }


        string _ascendingTitle;
        public string AscendingTitle
        {
            get
            {
                return _ascendingTitle;
            }

            set
            {
                if (value != _ascendingTitle)
                {
                    _ascendingTitle = value;
                    OnPropertyChanged(nameof(AscendingTitle));
                }
            }
        }

        string _descendingTitle;
        public string DescendingTitle
        {
            get
            {
                return _descendingTitle;
            }

            set
            {
                if (value != _descendingTitle)
                {
                    _descendingTitle = value;
                    OnPropertyChanged(nameof(DescendingTitle));
                }
            }
        }

        string _selectedSortingText;
        public string SelectedSortingText
        {
            get
            {
                return _selectedSortingText;
            }

            set
            {
                if (value != _selectedSortingText)
                {
                    _selectedSortingText = value;
                    OnPropertyChanged(nameof(SelectedSortingText));
                }
            }
        }


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


        ObservableCollection<workOrders> _workordersCollection = new ObservableCollection<workOrders>();

        public ObservableCollection<workOrders> WorkordersCollection
        {
            get
            {
                return _workordersCollection;
            }

        }

        #endregion
        #endregion

        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);
        public ICommand SortByCommand => new AsyncCommand(SortByAction);
        public ICommand ScanCommand => new AsyncCommand(SearchWorkorder);

        public ICommand WorkorderSelectedCommand => new Command<workOrders>(OnSelectWorkorderAsync);

        #endregion

        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {

                if (navigationData != null)
                {

                    var navigationParams = navigationData as TargetNavigationData;
                    this.SearchText = navigationParams.SearchText;
                    // IsLocationCallFrombarcodePage = navigationParams.IsLocationCallFrombarcodePage;
                    // IsAssetCallFrombarcodePage = navigationParams.IsAssetCallFrombarcodePage;
                }

                OperationInProgress = true;

                Application.Current.Properties["gridrowindex"] = 1;
                await SetTitlesPropertiesForPage();
                if (Application.Current.Properties.ContainsKey("CloseWorkorderRightsKey"))
                {
                    var CloseWorkorderRightsExpression = Application.Current.Properties["CloseWorkorderRightsKey"].ToString();
                    if (CloseWorkorderRightsExpression != null)
                    {
                        CloseWorkOrderRights = CloseWorkorderRightsExpression.ToString();

                    }
                }
                if (Application.Current.Properties.ContainsKey("CreateWorkorderRights"))
                {
                    var CreateWorkorderRightsExpression = Application.Current.Properties["CreateWorkorderRights"].ToString();
                    if (CreateWorkorderRightsExpression != null)
                    {
                        CreateWorkorderRights = CreateWorkorderRightsExpression.ToString();

                    }
                }
                if (Application.Current.Properties.ContainsKey("EditRights"))
                {
                    var EditWorkorderRightsExpression = Application.Current.Properties["EditRights"].ToString();
                    if (EditWorkorderRightsExpression != null)
                    {
                        EditRights = EditWorkorderRightsExpression.ToString();

                    }
                }

                await GetWorkordersFromAsset();


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

        public SearchWorkorderByAssetNumberFromBarcodeViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IWorkorderService workorderService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _workorderService = workorderService;
        }

        public async Task SetTitlesPropertiesForPage()
        {

            //var titles = await _formLoadInputService.GetFormLoadInputForBarcode(UserID, AppSettings.WorkorderDetailFormName);
            //if (titles != null && titles.CFLI.Count > 0 && titles.listWebControlTitles.Count > 0)
            {
                PageTitle = WebControlTitle.GetTargetNameByTitleName("WorkOrders");
                WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                CreateWorkorderTitle = WebControlTitle.GetTargetNameByTitleName("CreateWorkOrder");
                //SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName(titles, "SelectOptionsTitles"); 
                PreventiveMaintenenceTitle = WebControlTitle.GetTargetNameByTitleName("PreventiveMaintenance");
                DemandMaintenenceTitle = WebControlTitle.GetTargetNameByTitleName("DemandMaintenance");
                EmergencyMaintenanceTitle = WebControlTitle.GetTargetNameByTitleName("EmergencyMaintenance");

                SortByActivationdateTitle = WebControlTitle.GetTargetNameByTitleName("SortByActivationdate");
                NoneTitle = WebControlTitle.GetTargetNameByTitleName("None");
                AscendingTitle = WebControlTitle.GetTargetNameByTitleName("Ascending");
                DescendingTitle = WebControlTitle.GetTargetNameByTitleName("Descending");
                SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");






                ///Add the titles in picker titles
                /// 
                await AddTitlesToPicker();


                if (AppSettings.User.ULFeatures)
                {
                    if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic") || AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Professional"))
                    {
                        PickerTitles = new ObservableCollection<string>() { SelectTitle, PreventiveMaintenenceTitle, DemandMaintenenceTitle };

                    }
                    else
                    {
                        PickerTitles = new ObservableCollection<string>() { SelectTitle, PreventiveMaintenenceTitle, DemandMaintenenceTitle, EmergencyMaintenanceTitle };
                    }

                }
                else
                {
                    PickerTitles = new ObservableCollection<string>() { SelectTitle, PreventiveMaintenenceTitle, DemandMaintenenceTitle };
                }
                WorkorderTypeLabelTitle = WebControlTitle.GetTargetNameByTitleName("Sortby") + " " + WebControlTitle.GetTargetNameByTitleName("WorkOrderType");
                WorkOrderNumberTitle = WebControlTitle.GetTargetNameByTitleName("WorkorderNumber");
                DescriptionTitle = WebControlTitle.GetTargetNameByTitleName("Description");
                WorkOrderTypeTitle = WebControlTitle.GetTargetNameByTitleName("WorkOrderType");
                TargetNameTitle = WebControlTitle.GetTargetNameByTitleName("Target") + " " + WebControlTitle.GetTargetNameByTitleName("Name");
                RequiredDateTitle = WebControlTitle.GetTargetNameByTitleName("RequiredDate");
                ActivationDateTitle = WebControlTitle.GetTargetNameByTitleName("ActivationDate");

                TotalRecordTitle = WebControlTitle.GetTargetNameByTitleName("TotalRecords");

                SearchPlaceholder = WebControlTitle.GetTargetNameByTitleName("SearchOrScanWorkOrder");
                GoTitle = WebControlTitle.GetTargetNameByTitleName("Go");
                ScanTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                SearchButtonTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                WorkStartedDateTitle = WebControlTitle.GetTargetNameByTitleName("WorkStartedDate");
                WorkCompletionDateTitle = WebControlTitle.GetTargetNameByTitleName("CompletionDate");
                WorkRequestedDateTitle = WebControlTitle.GetTargetNameByTitleName("RequestedDate");

            }
        }
        public async Task ShowActions()
        {
            try
            {

                if (CreateWorkorderRights == "E")
                {
                    var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() { CreateWorkorderTitle, LogoutTitle });

                    if (response == LogoutTitle)
                    {
                        await _authenticationService.LogoutAsync();
                        await NavigationService.NavigateToAsync<LoginPageViewModel>();
                        await NavigationService.RemoveBackStackAsync();
                    }

                    if (response == CreateWorkorderTitle)
                    {
                        await NavigationService.NavigateToAsync<CreateWorkorderPageViewModel>();
                    }
                }
                else if (CreateWorkorderRights == "V")
                {
                    var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() { CreateWorkorderTitle, LogoutTitle });

                    if (response == LogoutTitle)
                    {
                        await _authenticationService.LogoutAsync();
                        await NavigationService.NavigateToAsync<LoginPageViewModel>();
                        await NavigationService.RemoveBackStackAsync();
                    }

                    if (response == CreateWorkorderTitle)
                    {

                    }
                }
                else
                {
                    var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() { LogoutTitle });

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

        public async Task SortByAction()
        {
            try
            {
                var response = await DialogService.SelectActionAsync(WebControlTitle.GetTargetNameByTitleName("SortByActivationdate"), SortByActivationdateTitle, CancelTitle, new ObservableCollection<string>() { NoneTitle, AscendingTitle, DescendingTitle });
                if (response == CancelTitle)
                {
                    this.SelectedSortingText = null;

                }

                else if (response == AscendingTitle)
                {
                    this.SelectedSortingText = "ASC";
                    //reset pageno. and start search again.
                    await RefillWorkorderCollection();
                }

                else if (response == DescendingTitle)
                {
                    this.SelectedSortingText = "DESC";
                    //reset pageno. and start search again.
                    await RefillWorkorderCollection();
                }

                else
                {
                    this.SelectedSortingText = null;
                    //reset pageno. and start search again.
                    await RefillWorkorderCollection();

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

        private async Task RefillWorkorderCollection()
        {

            PageNumber = 1;
            await RemoveAllWorkorderFromCollection();
            await GetWorkordersFromAsset();

        }

        public async Task SearchWorkorder()
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
                    await GetWorkordersFromSearchBar();

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
                await GetWorkordersFromSearchBar();


            });

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


        public async Task AddTitlesToPicker()
        {

            try
            {
                OperationInProgress = true;


                if (AppSettings.User.ULFeatures)
                {
                    this.PickerTitles = new ObservableCollection<string>() { SelectTitle, PreventiveMaintenenceTitle, DemandMaintenenceTitle, EmergencyMaintenanceTitle };
                    //this.SelectedIndexPicker = 0;


                }

                else
                {
                    this.PickerTitles = new ObservableCollection<string>() { SelectTitle, PreventiveMaintenenceTitle, DemandMaintenenceTitle };
                    // this.SelectedIndexPicker = 0;

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


        public async Task GetWorkordersAuto()
        {
            //if (string.IsNullOrWhiteSpace(SearchText))
            //{
                PageNumber++;
                await GetWorkordersFromAsset();
            //}
        }



            //}
            //async Task GetWorkorders()
            //{
            //    try
            //    {
            //        OperationInProgress = true;
            //        var workordersResponse = await _workorderService.GetWorkorders(UserID, PageNumber.ToString(), RowCount.ToString(), SearchText, WorkorderTypeFilterText, SelectedSortingText);
            //        if (workordersResponse != null && workordersResponse.workOrderWrapper != null
            //            && workordersResponse.workOrderWrapper.workOrders != null && workordersResponse.workOrderWrapper.workOrders.Count > 0)
            //        {

            //            var workorders = workordersResponse.workOrderWrapper.workOrders;

            //            await AddWorkordersInWorkorderCollection(workorders);
            //        }
            //        TotalRecordCount = workordersResponse.workOrderWrapper.WorkOrderCount;


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

            //async Task GetWorkordersFromAsset()
            //{
            //    try
            //    {
            //        OperationInProgress = true;
            //        var workordersResponse = await _workorderService.GetWorkorders(UserID, "0", "0", SearchText, WorkorderTypeFilterText, SelectedSortingText);
            //        // var workordersResponse = await _workorderService.GetWorkorders(UserID,"0","0", SearchText, WorkorderTypeFilterText, SelectedSortingText);
            //        if (workordersResponse != null && workordersResponse.workOrderWrapper != null
            //            && workordersResponse.workOrderWrapper.workOrders != null && workordersResponse.workOrderWrapper.workOrders.Count > 0)
            //        {

            //            var workorders = workordersResponse.workOrderWrapper.workOrders;
            //            await RemoveAllWorkorderFromCollection();
            //            await AddWorkordersInWorkorderCollection(workorders);
            //            TotalRecordCount = workordersResponse.workOrderWrapper.workOrders.Count();


            //        }
            //        else
            //        {
            //            TotalRecordCount = workordersResponse.workOrderWrapper.workOrders.Count();
            //            await RemoveAllWorkorderFromCollection();

            //        }




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

            async Task GetWorkordersFromAsset()
        {
            try
            {
                OperationInProgress = true;
                var workordersResponse = await _workorderService.GetWorkorders(UserID, PageNumber.ToString(), RowCount.ToString(), SearchText, WorkorderTypeFilterText, SelectedSortingText,"null","null","null","null");
                // var workordersResponse = await _workorderService.GetWorkorders(UserID,"0","0", SearchText, WorkorderTypeFilterText, SelectedSortingText);
                if (workordersResponse != null && workordersResponse.workOrderWrapper != null
                    && workordersResponse.workOrderWrapper.workOrders != null && workordersResponse.workOrderWrapper.WorkOrderCount > 0)
                {

                    var workorders = workordersResponse.workOrderWrapper.workOrders;
                   // await RemoveAllWorkorderFromCollection();
                    await AddWorkordersInWorkorderCollection(workorders);
                    TotalRecordCount = workordersResponse.workOrderWrapper.WorkOrderCount;


                }
                else
                {
                    TotalRecordCount = workordersResponse.workOrderWrapper.WorkOrderCount;
                    await RemoveAllWorkorderFromCollection();
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Thisassetisnotassociatedwithanyworkorder"), 2000);
                 //   return;

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
        async Task GetWorkordersFromSearchBar()
        {
            try
            {
                OperationInProgress = true;
                var workordersResponse = await _workorderService.GetWorkorders(UserID, "0", "0", SearchText, "null", "null","null", "null", "null","null");
                if (workordersResponse != null && workordersResponse.workOrderWrapper != null
                    && workordersResponse.workOrderWrapper.workOrders != null && workordersResponse.workOrderWrapper.workOrders.Count > 0)
                {

                    var workorders = workordersResponse.workOrderWrapper.workOrders;
                    await RemoveAllWorkorderFromCollection();
                    await AddWorkordersInWorkorderCollection(workorders);
                    TotalRecordCount = workordersResponse.workOrderWrapper.workOrders.Count;



                }
                else
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("ThisWorkOrderdoesnotexist"));
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

        private async Task AddWorkordersInWorkorderCollection(List<workOrders> workorders)
        {
            if (workorders != null && workorders.Count > 0)
            {
                foreach (var item in workorders)
                {

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _workordersCollection.Add(item);
                        OnPropertyChanged(nameof(WorkordersCollection));
                    });


                }
            }
        }


        private async Task RemoveAllWorkorderFromCollection()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                _workordersCollection.Clear();
                OnPropertyChanged(nameof(WorkordersCollection));
            });



        }

        private async Task RefillWororderFromPicker()
        {
            if (SelectedIndexPicker == -1)
            {
                return;
            }
            var SelectedPickerText = PickerTitles[SelectedIndexPicker];


            if (SelectedPickerText == SelectTitle)
            {
                WorkorderTypeFilterText = null;
                await RefillWorkorderCollection();
            }

            else if (SelectedPickerText == PreventiveMaintenenceTitle)
            {
                WorkorderTypeFilterText = "PreventiveMaintenance";
                await RefillWorkorderCollection();
            }

            else if (SelectedPickerText == DemandMaintenenceTitle)
            {
                WorkorderTypeFilterText = "DemandMaintenance";
                await RefillWorkorderCollection();

            }

            else if (SelectedPickerText == EmergencyMaintenanceTitle)
            {
                WorkorderTypeFilterText = "EmergencyMaintenance";
                await RefillWorkorderCollection();

            }

            else
            {
                WorkorderTypeFilterText = null;
                await RefillWorkorderCollection();
            }

        }

        private async void OnSelectWorkorderAsync(workOrders item)
        {
            if (EditRights == "E" || EditRights == "V")
            {
                if (AppSettings.User.blackhawkLicValidator.RiskAssasment.Equals(true))
                {


                    if (item != null)
                    {
                        if (item.IsRiskQuestion == false)
                        {
                            UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                            //OperationInProgress = true;
                            TargetNavigationData tnobj = new TargetNavigationData();
                            tnobj.WorkOrderId = item.WorkOrderID;
                            Application.Current.Properties["Workorderitem"] = item;
                            Application.Current.Properties["WorkorderID"] = item.WorkOrderID;

                            await NavigationService.NavigateToAsync<RiskQuestionPageViewModel>(tnobj);
                            UserDialogs.Instance.HideLoading();
                            //OperationInProgress = false;
                        }
                        else
                        {

                            if (item != null)
                            {
                                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                                // OperationInProgress = true;

                                await NavigationService.NavigateToAsync<WorkorderTabbedPageViewModel>(item);
                                //OperationInProgress = false;
                                UserDialogs.Instance.HideLoading();
                            }

                        }
                    }
                }
                else
                {
                    UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                    //OperationInProgress = true;

                    await NavigationService.NavigateToAsync<WorkorderTabbedPageViewModel>(item);
                    // OperationInProgress = false;
                    UserDialogs.Instance.HideLoading();
                }
            }
        }
        public async Task CloseWorkorder(workOrders workorderItem)
        {
            try
            {
                var WorkorderID = workorderItem.WorkOrderID;
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                // OperationInProgress = true;










                ///TODO: Get Workorder Labour data 
                var workorderLabourWrapper = await _workorderService.GetWorkorderLabour(UserID, WorkorderID.ToString());


                ///TODO: Get Workorder data 
                var workorderWrapper = await _workorderService.GetWorkorderByWorkorderID(UserID, WorkorderID.ToString());



                ///TODO: Get Inspection 
                var Inspection = await _workorderService.GetWorkorderInspection(WorkorderID.ToString(),AppSettings.User.UserID.ToString());


                ///TODO: Get Inspection Time 
                var InspectionTime = await _workorderService.GetWorkorderInspectionTime(UserID, WorkorderID.ToString());



                #region Check signature required in inspection


                ///TODO: Get Inspection InspectionSignatureResponse
                var InspectionSignatureResponse = await _workorderService.IsSignatureRequiredOnInspection(WorkorderID.ToString());
                if (InspectionSignatureResponse.IsSignatureRequiredAndEmpty)
                {
                    UserDialogs.Instance.HideLoading();
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("PleasefillallsignaturesinWorkordersInspectionstocloseWorkorder"), 2000);
                    return;
                }

                #endregion


                #region save and check the completion date before closing the workorder

                if (workorderWrapper.workOrderWrapper.workOrder.CompletionDate == null)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleasefillthecompletiondatebeforeclosingtheworkorder"), 2000);
                    return;
                }

                #endregion


                #region Comparison of WorkOrder completion date with Task and Labour completion date


                //Comparison of WorkOrder completion date with Task and Labour completion date/////
                if (workorderLabourWrapper.workOrderWrapper.FinalCompletionDate != null && workorderWrapper.workOrderWrapper.workOrder.CompletionDate < workorderLabourWrapper.workOrderWrapper.FinalCompletionDate.GetValueOrDefault().Date)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Workordercompletiondatecannotbepriorfromcompletiondateontaskandlaborrecord"), 2000);
                    return;
                }
                //Comparison of WorkOrder completion date with Inspection completion date//
                if (workorderWrapper.workOrderWrapper.workOrder.CompletionDate < InspectionTime.InspectionCompletionDate.GetValueOrDefault().Date)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Workordercompletiondatecannotbepriorfromcompletiondateontaskandlaborrecord"), 2000);
                    return;
                }

                #endregion


                #region Check Task and Labour data
                if (Convert.ToBoolean(workorderWrapper.workOrderWrapper.IsCheckedLaborHours))
                {


                    if (Inspection.listInspection != null && Inspection.listInspection.Count > 0)
                    {

                    }
                    else
                    {

                        if (workorderLabourWrapper.workOrderWrapper.workOrderLabors.Count == 0)
                        {
                            UserDialogs.Instance.HideLoading();

                            DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Taskandlaborisrequiredforclosedworkorder"), 2000);
                            return;
                        }
                        else if ((workorderLabourWrapper.workOrderWrapper.workOrderLabors.Count > 0))
                        {


                            foreach (var item in workorderLabourWrapper.workOrderWrapper.workOrderLabors)
                            {

                                bool AllTaskHours = workorderLabourWrapper.workOrderWrapper.workOrderLabors.All(a => a.HoursAtRate1 > 0);

                                if (AllTaskHours == false)
                                {
                                    UserDialogs.Instance.HideLoading();

                                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleaseenterhoursorminutesfieldvalueonthetaskandlabortab"), 2000);
                                    return;
                                }
                                else
                                {



                                }
                            }
                        }
                    }
                }
                #endregion


                #region Check Cause if required
                if (workorderWrapper.workOrderWrapper.workOrder.WorkOrderType == "DemandMaintenance")
                {
                    if (Convert.ToBoolean(workorderWrapper.workOrderWrapper.IsCheckedCause))
                    {

                        if (workorderWrapper.workOrderWrapper.Cause == null || workorderWrapper.workOrderWrapper.Cause.Count == 0)
                        {
                            UserDialogs.Instance.HideLoading();

                            DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("PleasefilltheCause"), 2000);
                            return;
                        }
                    }

                }
                #endregion


                #region Check Cost Center and Worktype if required
                if (workorderWrapper.workOrderWrapper.workOrder.WorkOrderType == "PreventiveMaintenance")
                {
                    if (Convert.ToBoolean(workorderWrapper.workOrderWrapper.IsCheckedCostCentre))
                    {
                        if (string.IsNullOrWhiteSpace(workorderWrapper.workOrderWrapper.workOrder.CostCenterName))
                        {
                            UserDialogs.Instance.HideLoading();

                            DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("PleasefilltheCostCenter"), 2000);
                            return;
                        }
                    }
                    if (Convert.ToBoolean(workorderWrapper.workOrderWrapper.IsCheckedWorkType))
                    {
                        if (string.IsNullOrWhiteSpace(workorderWrapper.workOrderWrapper.workOrder.WorkTypeName))
                        {
                            UserDialogs.Instance.HideLoading();

                            DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleasefilltheworktype"), 2000);
                            return;
                        }
                    }
                }
                #endregion



                //var yourobject = new workOrderWrapper
                //{
                //    TimeZone = "UTC",
                //    CultureName = "en-US",
                //    workOrder = new workOrders
                //    {

                //        WorkOrderID = wkid.GetValueOrDefault(),
                //        ModifiedUserName = usrname,
                //        FacilityID = abc.Result.workOrderWrapper.workOrder.FacilityID

                //    },

                //};


                var workorder = new workOrderWrapper
                {
                    TimeZone = AppSettings.UserTimeZone,
                    CultureName = AppSettings.UserCultureName,
                    UserId = Convert.ToInt32(UserID),
                    ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                    workOrder = workorderItem


                };


                var response = await _workorderService.CloseWorkorder(workorder);

                if (Boolean.Parse(response.servicestatus))
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("WorkOrderSuccessfullyClosed"), 2000);
                    await RefillWorkorderCollection();
                }
                UserDialogs.Instance.HideLoading();

                // OperationInProgress = false;

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();

                // OperationInProgress = false;
            }

            finally
            {
                UserDialogs.Instance.HideLoading();

                // OperationInProgress = false;
            }
        }
        public async Task OnViewAppearingAsync(VisualElement view)
        {

            //PageNumber = 1;
            //await RemoveAllWorkorderFromCollection();
            //await GetWorkordersFromSearchBar();
        }

        public async Task OnViewDisappearingAsync(VisualElement view)
        {
           // this.SearchText = null;
        }


        #endregion

    }
}
