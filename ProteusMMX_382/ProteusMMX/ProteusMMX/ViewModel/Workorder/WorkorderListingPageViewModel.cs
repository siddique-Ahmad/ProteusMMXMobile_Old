using Acr.UserDialogs;
using Microsoft.AppCenter.Crashes;
using ProteusMMX.Constants;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.DateTime;
using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.Views.Common;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace ProteusMMX.ViewModel.Workorder
{
    public class WorkorderListingPageViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {

        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        public readonly IWorkorderService _workorderService;


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

        DateTime _dueDate;
        public DateTime DueDate
        {
            get
            {
                return _dueDate;
            }
            set
            {
                _dueDate = value;
                OnPropertyChanged("DueDate");
                // RefillDueDateFromPicker();

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

        string _sortByLocationLabelTitle;
        public string SortByLocationLabelTitle
        {
            get
            {
                return _sortByLocationLabelTitle;
            }

            set
            {
                if (value != _sortByLocationLabelTitle)
                {
                    _sortByLocationLabelTitle = value;
                    OnPropertyChanged("SortByLocationLabelTitle");
                }
            }
        }
        string _sortByDateText;
        public string SortByDateText
        {
            get
            {
                return _sortByDateText;
            }

            set
            {
                if (value != _sortByDateText)
                {
                    _sortByDateText = value;
                    OnPropertyChanged("SortByDateText");
                    // RefillDueDateFromPicker();
                }
            }
        }


        string _sortByShiftLabelTitle;
        public string SortByShiftLabelTitle
        {
            get
            {
                return _sortByShiftLabelTitle;
            }

            set
            {
                if (value != _sortByShiftLabelTitle)
                {
                    _sortByShiftLabelTitle = value;
                    OnPropertyChanged("SortByShiftLabelTitle");
                }
            }
        }

        string _sortByDuedateTitle;
        public string SortByDuedateTitle
        {
            get
            {
                return _sortByDuedateTitle;
            }

            set
            {
                if (value != _sortByDuedateTitle)
                {
                    _sortByDuedateTitle = value;
                    OnPropertyChanged("SortByDuedateTitle");
                }
            }
        }

        string _sortByPriorityLabelTitle;
        public string SortByPriorityLabelTitle
        {
            get
            {
                return _sortByPriorityLabelTitle;
            }

            set
            {
                if (value != _sortByPriorityLabelTitle)
                {
                    _sortByPriorityLabelTitle = value;
                    OnPropertyChanged("SortByPriorityLabelTitle");
                }
            }
        }
        string MaxInspectionCompDate = string.Empty;

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
        string KPIDashboardType = string.Empty;
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

        bool _isGetWorkorderCall;
        public bool IsGetWorkorderCall
        {
            get
            {
                return _isGetWorkorderCall;
            }

            set
            {
                if (value != _isGetWorkorderCall)
                {
                    _isGetWorkorderCall = value;
                    OnPropertyChanged("IsGetWorkorderCall");

                }
            }
        }

        bool _isGetWorkorderCallFromRequiredDate;
        public bool IsGetWorkorderCallFromRequiredDate
        {
            get
            {
                return _isGetWorkorderCallFromRequiredDate;
            }

            set
            {
                if (value != _isGetWorkorderCallFromRequiredDate)
                {
                    _isGetWorkorderCallFromRequiredDate = value;
                    OnPropertyChanged("IsGetWorkorderCallFromRequiredDate");

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

        Dictionary<int?, string> sortByLocationpickerTitlesitems = new Dictionary<int?, string>();

        ObservableCollection<string> _sortByLocationpickerTitles = new ObservableCollection<string>();

        // Picker LocationPickerITEM = new Picker();


        public ObservableCollection<string> SortByLocationpickerTitles
        {
            get
            {
                return _sortByLocationpickerTitles;
            }

            set
            {
                if (value != _sortByLocationpickerTitles)
                {
                    _sortByLocationpickerTitles = value;
                    OnPropertyChanged("SortByLocationpickerTitles");

                }
            }

        }

        Dictionary<int?, string> sortByShiftpickerTitlesitems = new Dictionary<int?, string>();

        ObservableCollection<string> _sortByShiftpickerTitles = new ObservableCollection<string>();

        public ObservableCollection<string> SortByShiftpickerTitles
        {
            get
            {
                return _sortByShiftpickerTitles;
            }

            set
            {
                if (value != _sortByShiftpickerTitles)
                {
                    _sortByShiftpickerTitles = value;
                    OnPropertyChanged("SortByShiftpickerTitles");

                }
            }

        }


        Dictionary<int?, string> sortByPrioritypickerTitlesitems = new Dictionary<int?, string>();

        ObservableCollection<string> _sortByPriorityPickerTitles = new ObservableCollection<string>();

        public ObservableCollection<string> SortByPriorityPickerTitles
        {
            get
            {
                return _sortByPriorityPickerTitles;
            }

            set
            {
                if (value != _sortByPriorityPickerTitles)
                {
                    _sortByPriorityPickerTitles = value;
                    OnPropertyChanged("SortByPriorityPickerTitles");

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



        string _locationSelectedPickerText;
        public string LocationSelectedPickerText
        {
            get
            {
                return _locationSelectedPickerText;
            }

            set
            {
                if (value != _locationSelectedPickerText)
                {
                    _locationSelectedPickerText = value;
                    OnPropertyChanged("LocationSelectedPickerText");
                }
            }
        }

        string _shiftSelectedPickerText;
        public string ShiftSelectedPickerText
        {
            get
            {
                return _shiftSelectedPickerText;
            }

            set
            {
                if (value != _shiftSelectedPickerText)
                {
                    _shiftSelectedPickerText = value;
                    OnPropertyChanged("ShiftSelectedPickerText");
                }
            }
        }

        string _prioritySelectedPickerText;
        public string PrioritySelectedPickerText
        {
            get
            {
                return _prioritySelectedPickerText;
            }

            set
            {
                if (value != _prioritySelectedPickerText)
                {
                    _prioritySelectedPickerText = value;
                    OnPropertyChanged("PrioritySelectedPickerText");
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
        string _FailedInspectionTitle;
        public string FailedInspectionTitle
        {
            get
            {
                return _FailedInspectionTitle;
            }

            set
            {
                if (value != _FailedInspectionTitle)
                {
                    _FailedInspectionTitle = value;
                    OnPropertyChanged("FailedInspectionTitle");
                }
            }
        }
        string _locationNameFilterText;
        public string LocationNameFilterText
        {
            get
            {
                return _locationNameFilterText;
            }

            set
            {
                _locationNameFilterText = value;
                OnPropertyChanged(nameof(LocationNameFilterText));
            }
        }
        string _shiftNameFilterText;
        public string ShiftNameFilterText
        {
            get
            {
                return _shiftNameFilterText;
            }

            set
            {
                _shiftNameFilterText = value;
                OnPropertyChanged(nameof(ShiftNameFilterText));
            }
        }

        string _priorityNameFilterText;
        public string PriorityNameFilterText
        {
            get
            {
                return _priorityNameFilterText;
            }

            set
            {
                _priorityNameFilterText = value;
                OnPropertyChanged(nameof(PriorityNameFilterText));
            }
        }

        string _sortByDueDate;
        public string SortByDueDate
        {
            get
            {
                return _sortByDueDate;
            }

            set
            {
                _sortByDueDate = value;
                OnPropertyChanged(nameof(SortByDueDate));
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

        string _inspectionOnlyTitle;
        public string InspectionOnlyTitle
        {
            get
            {
                return _inspectionOnlyTitle;
            }

            set
            {
                if (value != _inspectionOnlyTitle)
                {
                    _inspectionOnlyTitle = value;
                    OnPropertyChanged("InspectionOnlyTitle");
                }
            }
        }

        string _taskOnlyTitle;
        public string TaskOnlyTitle
        {
            get
            {
                return _taskOnlyTitle;
            }

            set
            {
                if (value != _taskOnlyTitle)
                {
                    _taskOnlyTitle = value;
                    OnPropertyChanged("TaskOnlyTitle");
                }
            }
        }

        string _completionTitle;
        public string CompletionTitle
        {
            get
            {
                return _completionTitle;
            }

            set
            {
                if (value != _completionTitle)
                {
                    _completionTitle = value;
                    OnPropertyChanged("CompletionTitle");
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

        int _locationSelectedIndexPicker = -1;
        public int LocationSelectedIndexPicker
        {
            get
            {
                return _locationSelectedIndexPicker;
            }

            set
            {
                _locationSelectedIndexPicker = value;
                OnPropertyChanged("LocationSelectedIndexPicker");

            }
        }

        int _shiftSelectedIndexPicker = -1;
        public int ShiftSelectedIndexPicker
        {
            get
            {
                return _shiftSelectedIndexPicker;
            }

            set
            {
                _shiftSelectedIndexPicker = value;
                OnPropertyChanged("ShiftSelectedIndexPicker");
                //RefillShiftFromPicker();

            }
        }

        int _prioritySelectedIndexPicker = -1;
        public int PrioritySelectedIndexPicker
        {
            get
            {
                return _prioritySelectedIndexPicker;
            }

            set
            {
                _prioritySelectedIndexPicker = value;
                OnPropertyChanged("PrioritySelectedIndexPicker");
                //RefillPriorityFromPicker();

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
        ObservableCollection<workOrders> _workordersCollection = new ObservableCollection<workOrders>();

        public ObservableCollection<workOrders> WorkordersCollection
        {
            get
            {
                return _workordersCollection;
            }

        }

        #endregion

        #region **** Icon Image ****
        string _sortBy = "";
        public string SortBy
        {
            get
            {
                return _sortBy;
            }

            set
            {
                if (value != _sortBy)
                {
                    _sortBy = value;
                    OnPropertyChanged("SortBy");
                }
            }
        }

        string _filterBy = "";
        public string FilterBy
        {
            get
            {
                return _filterBy;
            }

            set
            {
                if (value != _filterBy)
                {
                    _filterBy = value;
                    OnPropertyChanged("FilterBy");
                }
            }
        }

        string _groupBy = "";
        public string GroupBy
        {
            get
            {
                return _groupBy;
            }

            set
            {
                if (value != _groupBy)
                {
                    _groupBy = value;
                    OnPropertyChanged("GroupBy");
                }
            }
        }

        #endregion
        #endregion

        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);
        public ICommand SortByCommand => new AsyncCommand(SortByAction);

        public ICommand SortByFilterCommand => new AsyncCommand(SortByFilterAction);
        public ICommand ScanCommand => new AsyncCommand(SearchWorkorder);

        public ICommand ShowDateCommand => new AsyncCommand(ShowdatePicker);
        public ICommand DateCommand => new AsyncCommand(Cleardate);

        public ICommand NewWorkOrderCommand => new AsyncCommand(NewWorkOrder);
        public ICommand WorkorderSelectedCommand => new Command<workOrders>(OnSelectWorkorderAsync);

        #endregion

        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                if (Device.RuntimePlatform == Device.UWP)
                {
                    SortBy = "Assets/shortingN.png";
                    FilterBy = "Assets/filterN.png";
                    GroupBy = "Assets/searchN.png";
                }
                else
                {
                    SortBy = "shortingN.png";
                    FilterBy = "filterN.png";
                    GroupBy = "searchN.png";
                }

                UserDialogs.Instance.ShowLoading("Please wait..", MaskType.Gradient);
                await Task.Delay(10);
                if (navigationData != null)
                {

                    var navigationParams = navigationData as TargetNavigationData;
                    this.SearchText = navigationParams.SearchText;

                    IsLocationCallFrombarcodePage = navigationParams.IsLocationCallFrombarcodePage;
                    IsAssetCallFrombarcodePage = navigationParams.IsAssetCallFrombarcodePage;

                    //this.KPIDashboardType = navigationParams.Type;
                }
                await SetTitlesPropertiesForPage();

                Application.Current.Properties["gridrowindex"] = 1;
                //await GetWorkorderControlRights();

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

                OperationInProgress = false;



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

        public WorkorderListingPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IWorkorderService workorderService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _workorderService = workorderService;
        }

        public async Task SetTitlesPropertiesForPage()
        {


            FailedInspectionTitle = WebControlTitle.GetTargetNameByTitleName("FailedInspection");
            PageTitle = WebControlTitle.GetTargetNameByTitleName("WorkOrders");
            WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
            LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
            CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
            SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
            CreateWorkorderTitle = WebControlTitle.GetTargetNameByTitleName("CreateWorkOrder");

            PreventiveMaintenenceTitle = WebControlTitle.GetTargetNameByTitleName("PreventiveMaintenance");
            DemandMaintenenceTitle = WebControlTitle.GetTargetNameByTitleName("DemandMaintenance");
            EmergencyMaintenanceTitle = WebControlTitle.GetTargetNameByTitleName("EmergencyMaintenance");

            TaskOnlyTitle = WebControlTitle.GetTargetNameByTitleName("Task") +""+ WebControlTitle.GetTargetNameByTitleName("Only");
            InspectionOnlyTitle = WebControlTitle.GetTargetNameByTitleName("Inspection") + "" + WebControlTitle.GetTargetNameByTitleName("Only");
            CompletionTitle = WebControlTitle.GetTargetNameByTitleName("CompletionDate");

            SortByActivationdateTitle = WebControlTitle.GetTargetNameByTitleName("SortByActivationdate");

            AscendingTitle = WebControlTitle.GetTargetNameByTitleName("Ascending");
            DescendingTitle = WebControlTitle.GetTargetNameByTitleName("Descending");
            SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");
            await AddTitlesToPicker();

            if (AppSettings.User.ULFeatures)
            {

                if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic") || AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Professional"))
                {
                    if (string.IsNullOrWhiteSpace(FailedInspectionTitle))
                    {
                        PickerTitles = new ObservableCollection<string>() { PreventiveMaintenenceTitle, DemandMaintenenceTitle,CompletionTitle,TaskOnlyTitle,InspectionOnlyTitle };
                    }
                    else
                    {
                        PickerTitles = new ObservableCollection<string>() { PreventiveMaintenenceTitle, DemandMaintenenceTitle, FailedInspectionTitle, CompletionTitle, TaskOnlyTitle, InspectionOnlyTitle };
                    }
                }



                else
                {


                    if (string.IsNullOrWhiteSpace(FailedInspectionTitle))
                    {
                        PickerTitles = new ObservableCollection<string>() { PreventiveMaintenenceTitle, DemandMaintenenceTitle, EmergencyMaintenanceTitle, CompletionTitle, TaskOnlyTitle, InspectionOnlyTitle };
                    }
                    else
                    {
                        PickerTitles = new ObservableCollection<string>() { PreventiveMaintenenceTitle, DemandMaintenenceTitle, EmergencyMaintenanceTitle, FailedInspectionTitle, CompletionTitle, TaskOnlyTitle, InspectionOnlyTitle };
                    }

                }
            }


            else
            {
                if (string.IsNullOrWhiteSpace(FailedInspectionTitle))
                {
                    PickerTitles = new ObservableCollection<string>() { PreventiveMaintenenceTitle, DemandMaintenenceTitle, CompletionTitle, TaskOnlyTitle, InspectionOnlyTitle };
                }
                else
                {
                    PickerTitles = new ObservableCollection<string>() { PreventiveMaintenenceTitle, DemandMaintenenceTitle, FailedInspectionTitle, CompletionTitle, TaskOnlyTitle, InspectionOnlyTitle };
                }

            }
            if (PreventiveMaintenenceTitle == null)
            {
                PickerTitles.Remove(PreventiveMaintenenceTitle);
            }
            if (DemandMaintenenceTitle == null)
            {
                PickerTitles.Remove(DemandMaintenenceTitle);
            }
            if (EmergencyMaintenanceTitle == null)
            {
                PickerTitles.Remove(EmergencyMaintenanceTitle);
            }

            WorkorderTypeLabelTitle = WebControlTitle.GetTargetNameByTitleName("SortByWorkOrderType");
            SortByLocationLabelTitle = WebControlTitle.GetTargetNameByTitleName("Sortby") + " " + WebControlTitle.GetTargetNameByTitleName("Location");
            SortByShiftLabelTitle = WebControlTitle.GetTargetNameByTitleName("Sortby") + " " + WebControlTitle.GetTargetNameByTitleName("Shift");
            SortByDuedateTitle = WebControlTitle.GetTargetNameByTitleName("Sortby") + " " + WebControlTitle.GetTargetNameByTitleName("Due") + " " + WebControlTitle.GetTargetNameByTitleName("Date");
            SortByPriorityLabelTitle = WebControlTitle.GetTargetNameByTitleName("Sortby") + " " + WebControlTitle.GetTargetNameByTitleName("Priority");
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
        public async Task ShowActions()
        {
            try
            {
                if (Device.RuntimePlatform != Device.UWP)
                {
                    UserDialogs.Instance.ShowLoading("Please wait..", MaskType.Gradient);
                    await Task.Delay(10);
                }

                if (CreateWorkorderRights == "E")
                {
                    //var response = await DialogService.SelectActionAsync("", "", CancelTitle, new ObservableCollection<string>() { LogoutTitle });
                    var response = await DialogService.SelectActionAsync("", SelectTitle, CancelTitle, new ObservableCollection<string>() { LogoutTitle });

                    if (response == LogoutTitle)
                    {
                        await _authenticationService.LogoutAsync();
                        await NavigationService.NavigateToAsync<LoginPageViewModel>();
                        await NavigationService.RemoveBackStackAsync();
                    }

                    //if (response == CreateWorkorderTitle)
                    //{
                    //    await NavigationService.NavigateToAsync<CreateWorkorderPageViewModel>();
                    //    // NavigationPage.BackBarButtonItem.TintColor = UIColor.Red;
                    //}
                }
                else if (CreateWorkorderRights == "V")
                {
                    var response = await DialogService.SelectActionAsync("", "", CancelTitle, new ObservableCollection<string>() { CreateWorkorderTitle, LogoutTitle });

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
                    var response = await DialogService.SelectActionAsync("", "", CancelTitle, new ObservableCollection<string>() { LogoutTitle });

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
            }

            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }
        
        public async Task SortByFilterAction()
        {

            try
            {
                //if (Device.RuntimePlatform != Device.UWP)
                //{
                //    UserDialogs.Instance.ShowLoading("Please wait..", MaskType.Gradient);
                //    await Task.Delay(10);
                //}
                string response = string.Empty;
                if (Application.Current.Properties.ContainsKey("WorkOFilterTypeKye"))
                {
                    response = Application.Current.Properties["WorkOFilterTypeKye"].ToString();
                }
              //  var response = await DialogService.SelectActionAsync("FilterBy", SortByActivationdateTitle, CancelTitle, PickerTitles);

                if (response == CancelTitle)
                {
                    this.SelectedSortingText = null;

                }

                if (response == PreventiveMaintenenceTitle)
                {
                    if (Device.RuntimePlatform == Device.UWP)
                    {
                        FilterBy = "Assets/filterNs.png";

                    }
                    else
                    {
                        FilterBy = "filterNs.png";
                    }

                    WorkorderTypeFilterText = "PreventiveMaintenance";
                    await RefillWorkorderCollection();
                }

                else if (response == DemandMaintenenceTitle)
                {
                    if (Device.RuntimePlatform == Device.UWP)
                    {
                        FilterBy = "Assets/filterNs.png";

                    }
                    else
                    {
                        FilterBy = "filterNs.png";
                    }
                    WorkorderTypeFilterText = "DemandMaintenance";
                    await RefillWorkorderCollection();

                }

                else if (response == EmergencyMaintenanceTitle)
                {
                    if (Device.RuntimePlatform == Device.UWP)
                    {
                        FilterBy = "Assets/filterNs.png";

                    }
                    else
                    {
                        FilterBy = "filterNs.png";
                    }
                    WorkorderTypeFilterText = "EmergencyMaintenance";
                    await RefillWorkorderCollection();

                }

                else if (response == FailedInspectionTitle)
                {
                    if (Device.RuntimePlatform == Device.UWP)
                    {
                        FilterBy = "Assets/filterNs.png";

                    }
                    else
                    {
                        FilterBy = "filterNs.png";
                    }
                    WorkorderTypeFilterText = "FailedInspection";
                    await RefillWorkorderCollection();
                }

                else if (response == CompletionTitle)
                {
                    if (Device.RuntimePlatform == Device.UWP)
                    {
                        FilterBy = "Assets/filterNs.png";

                    }
                    else
                    {
                        FilterBy = "filterNs.png";
                    }
                    WorkorderTypeFilterText = "completiondate";
                    await RefillWorkorderCollection();
                }

                else if (response == TaskOnlyTitle)
                {
                    if (Device.RuntimePlatform == Device.UWP)
                    {
                        FilterBy = "Assets/filterNs.png";

                    }
                    else
                    {
                        FilterBy = "filterNs.png";
                    }
                    WorkorderTypeFilterText = "taskandlaboronly";
                    await RefillWorkorderCollection();
                }

                else if (response == InspectionOnlyTitle)
                {
                    if (Device.RuntimePlatform == Device.UWP)
                    {
                        FilterBy = "Assets/filterNs.png";

                    }
                    else
                    {
                        FilterBy = "filterNs.png";
                    }
                    WorkorderTypeFilterText = "inspectiononly";
                    await RefillWorkorderCollection();
                }

                else
                {
                    if (Device.RuntimePlatform == Device.UWP)
                    {
                        FilterBy = "Assets/filterN.png";

                    }
                    else
                    {
                        FilterBy = "filterN.png";
                    }
                    WorkorderTypeFilterText = null;
                    await RefillWorkorderCollection();
                }

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
            }

            finally
            {
               // UserDialogs.Instance.HideLoading();
            }
        }
        public async Task SortByAction()
        {
            try
            {
                //if (Device.RuntimePlatform != Device.UWP)
                //{
                //    UserDialogs.Instance.ShowLoading("Please wait..", MaskType.Gradient);
                //    await Task.Delay(10);
                //}

                string response = string.Empty;
                if (Application.Current.Properties.ContainsKey("SortingTypeKye"))
                {
                    response = Application.Current.Properties["SortingTypeKye"].ToString();
                }

                if (response == CancelTitle)
                {
                    this.SelectedSortingText = null;
                }

                else if (response == AscendingTitle)
                {
                    if (Device.RuntimePlatform == Device.UWP)
                    {
                        SortBy = "Assets/shortingNs.png";

                    }
                    else
                    {
                        SortBy = "shortingNs.png";
                    }

                    this.SelectedSortingText = "ASC";
                    //reset pageno. and start search again.
                    await RefillWorkorderCollection();
                }

                else if (response == DescendingTitle)
                {
                    if (Device.RuntimePlatform == Device.UWP)
                    {
                        SortBy = "Assets/shortingNs.png";

                    }
                    else
                    {
                        SortBy = "shortingNs.png";
                    }
                    this.SelectedSortingText = "DESC";
                    //reset pageno. and start search again.
                    await RefillWorkorderCollection();
                }

                else
                {
                    this.SelectedSortingText = null;
                    //reset pageno. and start search again.
                    await RefillWorkorderCollection();
                    if (Device.RuntimePlatform == Device.UWP)
                    {
                        SortBy = "Assets/shortingN.png";

                    }
                    else
                    {
                        SortBy = "shortingN.png";
                    }

                }

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                //OperationInProgress = false;
            }

            finally
            {
              //  UserDialogs.Instance.HideLoading();
                //OperationInProgress = false;
            }
        }

        public async Task RefillWorkorderCollection()
        {

            PageNumber = 1;
            await RemoveAllWorkorderFromCollection();
            if (Application.Current.Properties.ContainsKey("PriorityID"))
            {
                var priorityfilter = Application.Current.Properties["PriorityID"].ToString();
                this.PriorityNameFilterText = priorityfilter;
            }
            if (Application.Current.Properties.ContainsKey("overdue"))
            {
                var overdue = Application.Current.Properties["overdue"].ToString();
                this.KPIDashboardType = overdue;
            }
            if (Application.Current.Properties.ContainsKey("today"))
            {
                var Today = Application.Current.Properties["today"].ToString();
                this.KPIDashboardType = Today;
            }
            if (Application.Current.Properties.ContainsKey("weekly"))
            {
                var Weekly = Application.Current.Properties["weekly"].ToString();
                this.KPIDashboardType = Weekly;
            }
            await GetWorkorders();

        }

        public async Task searchBoxTextCler()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Please wait..", MaskType.Gradient);
                //await Task.Delay(10);
                await OnViewDisappearingAsync(null);
                await RefillWorkorderCollection();
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
        public async Task Cleardate()
        {

            SortByDateText = null;

        }
        public async Task ShowdatePicker()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Please wait..", MaskType.Gradient);
                //await Task.Delay(10);

                UserDialogs.Instance.DatePrompt(new DatePromptConfig { OnAction = (result) => SetCompletionDateResult(result), IsCancellable = true });
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
        public async Task SearchWorkorder()
        {

            try
            {
                UserDialogs.Instance.ShowLoading("Please wait..", MaskType.Gradient);
              //  await Task.Delay(10);


                #region Barcode Section and Search Section

                if (SearchButtonTitle == ScanTitle)
                {
                    try
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
                    catch (Exception ex)
                    {

                        //MailMessage mail = new MailMessage();
                        //SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                        //mail.From = new MailAddress("siddique@inzerotech.com");
                        //mail.To.Add("siddique@inzerotech.com");
                        //mail.Subject = ex.Message;
                        //mail.Body = ex.StackTrace;

                        //SmtpServer.Port = 587;
                        //SmtpServer.Host = "smtp.gmail.com";
                        //SmtpServer.EnableSsl = true;
                        //SmtpServer.UseDefaultCredentials = false;
                        //SmtpServer.Credentials = new System.Net.NetworkCredential("siddique@inzerotech.com", "siddique1@");

                        //SmtpServer.Send(mail);
                    }

                }

                else
                {
                    //reset pageno. and start search again.
                    await GetWorkordersFromSearchBar();
                    // await GetWorkorders();
                }

                #endregion

                //await NavigationService.NavigateToAsync<WorkorderListingPageViewModel>();
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

        public async Task NewWorkOrder()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Please wait..", MaskType.Gradient);
                //await Task.Delay(10);
                if (CreateWorkorderRights == "E")
                {
                    await NavigationService.NavigateToAsync<CreateWorkorderPageViewModel>();
                }
                else if (CreateWorkorderRights == "V")
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
                //await GetWorkorders();

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

                SortByLocationpickerTitles.Add(SelectTitle);
                SortByShiftpickerTitles.Add(SelectTitle);
                SortByPriorityPickerTitles.Add(SelectTitle);


            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();

            }

            finally
            {
                // UserDialogs.Instance.HideLoading();

            }
        }

        public async Task GetWorkordersAuto()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                PageNumber++;
                await GetWorkorders();
            }
        }

        public async Task GetWorkorders()
        {
            try
            {
                if (String.IsNullOrWhiteSpace(this.KPIDashboardType))
                {
                    this.KPIDashboardType = null;
                }
                //UserDialogs.Instance.ShowLoading("Please wait..", MaskType.Gradient);
                //await Task.Delay(10);
                var workordersResponse = await _workorderService.GetWorkorders(UserID, PageNumber.ToString(), RowCount.ToString(), SearchText, WorkorderTypeFilterText, SelectedSortingText, LocationNameFilterText, ShiftNameFilterText, PriorityNameFilterText, SortByDueDate, KPIDashboardType);
                if (workordersResponse != null && workordersResponse.workOrderWrapper != null
                    && workordersResponse.workOrderWrapper.workOrders != null && workordersResponse.workOrderWrapper.workOrders.Count > 0)
                {
                    if (!string.IsNullOrWhiteSpace(workordersResponse.workOrderWrapper.IsCheckedCause))
                    {
                        Application.Current.Properties["IsCheckedCauseKey"] = workordersResponse.workOrderWrapper.IsCheckedCause;
                    }
                    else
                    {
                        Application.Current.Properties["IsCheckedCauseKey"] = false;
                    }

                    var workorders = workordersResponse.workOrderWrapper.workOrders;

                    await AddWorkordersInWorkorderCollection(workorders);

                }
                TotalRecordCount = workordersResponse.workOrderWrapper.WorkOrderCount;

            }
            catch (Exception ex)
            {

                //  UserDialogs.Instance.HideLoading();
            }

            finally
            {
                // UserDialogs.Instance.HideLoading();
            }
        }

        async Task GetWorkordersRequiredDate()
        {
            try
            {
                OperationInProgress = true;
                var workordersResponse = await _workorderService.GetWorkorders(UserID, "0", "0", "null", "null", "null", "null", "null", "null", "null", "null");
                if (workordersResponse != null && workordersResponse.workOrderWrapper != null
                    && workordersResponse.workOrderWrapper.workOrders != null && workordersResponse.workOrderWrapper.workOrders.Count > 0)
                {
                    var workorders = workordersResponse.workOrderWrapper.workOrders.Where(x => Convert.ToDateTime(x.RequiredDate).Date.ToString("dd MMM yyyy") == SortByDueDate);
                    await RemoveAllWorkorderFromCollection();
                    await AddWorkordersInWorkorderCollection(workorders.ToList());
                    TotalRecordCount = workorders.Count();
                    IsGetWorkorderCallFromRequiredDate = true;
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

        public async Task GetWorkordersFromSearchBar()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Please wait..", MaskType.Gradient);
               // await Task.Delay(10);
                string WorkorderType = GetPickerType();
                var workordersResponse = await _workorderService.GetWorkorders(UserID, "0", "0", SearchText, WorkorderTypeFilterText, SelectedSortingText, LocationNameFilterText, ShiftNameFilterText, PriorityNameFilterText, SortByDueDate, KPIDashboardType);
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

                UserDialogs.Instance.HideLoading();
            }

            finally
            {
                
                UserDialogs.Instance.HideLoading();
            }
        }

        public async Task ClearSearchBox()
        {

            await GetWorkorders();
        }

        public async Task ClearIconClick()
        {
            SearchText = null; WorkorderTypeFilterText = null; SelectedSortingText = null; LocationNameFilterText = null; ShiftNameFilterText = null; PriorityNameFilterText = null; SortByDueDate = null; KPIDashboardType = null;
            await WorkorderCler();
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

        public async Task RemoveAllWorkorderFromCollection()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                _workordersCollection.Clear();
                OnPropertyChanged(nameof(WorkordersCollection));
            });
        }

        private async Task RefillLocationFromPicker(string Locationfilter)
        {

            if (string.IsNullOrWhiteSpace(Locationfilter))
            {
                LocationNameFilterText = null;
                await RefillWorkorderCollection();
            }
            else
            {
                LocationNameFilterText = Locationfilter;
                await RefillWorkorderCollection();

            }

        }

        private async Task RefillShiftFromPicker(string Shiftfilter)
        {

            if (string.IsNullOrWhiteSpace(Shiftfilter))
            {
                ShiftNameFilterText = null;
                await RefillWorkorderCollection();
            }

            else
            {

                ShiftNameFilterText = Shiftfilter.ToString();
                await RefillWorkorderCollection();
            }





        }

        private async Task RefillDueDateFromPicker(string SortByDateText)
        {

            if (!string.IsNullOrWhiteSpace(SortByDateText))
            {
                SortByDueDate = Convert.ToDateTime(SortByDateText).Date.ToString("dd MMM yyyy");

            }
            else
            {
                SortByDueDate = "null";
            }
            await RefillWorkorderCollection();

        }

        private async Task RefillPriorityFromPicker(string Priorityfilter)
        {
            if (string.IsNullOrWhiteSpace(Priorityfilter))
            {
                PriorityNameFilterText = null;
                await RefillWorkorderCollection();
            }

            else
            {
                PriorityNameFilterText = Priorityfilter.ToString();
                await RefillWorkorderCollection();
            }





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
            else if (SelectedPickerText == FailedInspectionTitle)
            {
                WorkorderTypeFilterText = "FailedInspection";
                await RefillWorkorderCollection();
            }
            else if (SelectedPickerText == CompletionTitle)
            {
                WorkorderTypeFilterText = "completiondate";
                await RefillWorkorderCollection();
            }
            else if (SelectedPickerText == TaskOnlyTitle)
            {
                WorkorderTypeFilterText = "taskandlaboronly";
                await RefillWorkorderCollection();
            }
            else if (SelectedPickerText == InspectionOnlyTitle)
            {
                WorkorderTypeFilterText = "inspectiononly";
                await RefillWorkorderCollection();
            }
            else
            {
                WorkorderTypeFilterText = null;
                await RefillWorkorderCollection();
            }




        }

        private string GetPickerType()
        {
            if (SelectedIndexPicker == -1)
            {
                return null;
            }

            var SelectedPickerText = PickerTitles[SelectedIndexPicker];

            if (SelectedPickerText == SelectTitle)
            {
                WorkorderTypeFilterText = null;
            }

            else if (SelectedPickerText == PreventiveMaintenenceTitle)
            {
                WorkorderTypeFilterText = "PreventiveMaintenance";
            }

            else if (SelectedPickerText == DemandMaintenenceTitle)
            {
                WorkorderTypeFilterText = "DemandMaintenance";

            }

            else if (SelectedPickerText == EmergencyMaintenanceTitle)
            {
                WorkorderTypeFilterText = "EmergencyMaintenance";

            }
            else if (SelectedPickerText == FailedInspectionTitle)
            {
                WorkorderTypeFilterText = "FailedInspection";
            }
            else
            {
                WorkorderTypeFilterText = null;
            }

            return WorkorderTypeFilterText;
        }

        private async void OnSelectWorkorderAsync(workOrders item)
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                //await Task.Delay(10);
                if (item.WorkOrderApproved == "true" && item.CompletionDate != null)
                {
                    return;
                }
                if (EditRights == "E" || EditRights == "V")
                {
                    if (AppSettings.User.blackhawkLicValidator.RiskAssasment.Equals(true))
                    {
                        if (item != null)
                        {
                            if (item.IsRiskQuestion == false)
                            {
                                /// UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
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
                                    ///  UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                                    // OperationInProgress = true;
                                    Application.Current.Properties["WorkorderID"] = item.WorkOrderID;
                                    await NavigationService.NavigateToAsync<WorkorderTabbedPageViewModel>(item);
                                    //OperationInProgress = false;
                                    UserDialogs.Instance.HideLoading();
                                }
                            }
                        }
                    }
                    else
                    {
                        /// UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                        //OperationInProgress = true;

                        await NavigationService.NavigateToAsync<WorkorderTabbedPageViewModel>(item);
                        // OperationInProgress = false;
                        UserDialogs.Instance.HideLoading();
                    }
                }
            }
            catch (Exception ex)
            {

                // DialogService.ShowToast(ex.InnerException.Message, 10000);
                UserDialogs.Instance.HideLoading();

            }
            finally
            {
                UserDialogs.Instance.HideLoading();

                // OperationInProgress = false;
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
                var Inspection = await _workorderService.GetWorkorderInspection(WorkorderID.ToString(), UserID);


                ///TODO: Get Inspection Time 
                var InspectionTime = await _workorderService.GetWorkorderInspectionTime(UserID, WorkorderID.ToString());
                List<InspectionTOAnswers> listtoAnswer = new List<InspectionTOAnswers>();
                foreach (var item in Inspection.workOrderEmployee)
                {
                    listtoAnswer.Add(new InspectionTOAnswers()
                    {

                        StartDate = item.StartDate,
                        CompletionDate = item.CompletionDate

                    });
                }
                foreach (var item in Inspection.workorderContractor)
                {
                    listtoAnswer.Add(new InspectionTOAnswers()
                    {

                        StartDate = item.StartDate,
                        CompletionDate = item.CompletionDate

                    });
                }
                if (listtoAnswer != null && listtoAnswer.Count > 0)
                {

                    MaxInspectionCompDate = listtoAnswer.Max(i => i.CompletionDate).ToString();


                }
                else
                {

                    MaxInspectionCompDate = string.Empty;

                }

                #region check if all answers of a sections are required

                // check if all answers of a sections are required///////
                if (Convert.ToBoolean(workorderWrapper.workOrderWrapper != null))
                {
                    if (Convert.ToBoolean(workorderWrapper.workOrderWrapper.sections != null && workorderWrapper.workOrderWrapper.sections.Count > 0))
                    {
                        StringBuilder RequiredSection = new StringBuilder();

                        foreach (var item in workorderWrapper.workOrderWrapper.sections)
                        {
                            RequiredSection.Append(item.SectionName);
                            RequiredSection.Append(",");
                        }

                        await DialogService.ShowAlertAsync(RequiredSection.ToString().TrimEnd(','), WebControlTitle.GetTargetNameByTitleName("PleaseProvideFollowingSectionQuestionAnswer"), "OK");
                        return;


                    }
                }

                #endregion

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
                //if (workorderWrapper.workOrderWrapper.workOrder.CompletionDate < InspectionTime.InspectionCompletionDate.GetValueOrDefault().Date)
                //{
                //    UserDialogs.Instance.HideLoading();

                //    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Workordercompletiondatecannotbepriorfromcompletiondateontaskandlaborrecord"), 2000);
                //    return;
                //}
                if (!string.IsNullOrWhiteSpace(MaxInspectionCompDate))
                {
                    if (workorderWrapper.workOrderWrapper.workOrder.CompletionDate < Convert.ToDateTime(MaxInspectionCompDate).Date)
                    {
                        UserDialogs.Instance.HideLoading();

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("WorkOrderCompletionDateMustBeBiggerThanWorkOrderInspectionCompletionDate"), 2000);
                        return;
                    }
                }

                #endregion


                #region Check Task and Labour/Inspection data
                if (Convert.ToBoolean(workorderWrapper.workOrderWrapper.IsCheckedLaborHours))
                {


                    if (Inspection.listInspection != null && Inspection.listInspection.Count > 0)
                    {
                        bool InspectionEmployeeHours = false;
                        bool InspectionContractorHours = false;

                        #region  Check all Employee Whose start date is not null
                        List<WorkOrderEmployee> EmpStartdateNotNull = Inspection.workOrderEmployee.Where(x => x.StartDate != null).ToList();
                        #endregion

                        if (!EmpStartdateNotNull.Any(x => string.IsNullOrEmpty(x.InspectionTime)))
                        {
                            InspectionEmployeeHours = EmpStartdateNotNull.All(a => int.Parse((a.InspectionTime)) > 0);
                        }

                        #region Check all Contractor Whose start date is not null
                        List<WorkorderContractor> ContractStartdateNotNull = Inspection.workorderContractor.Where(x => x.StartDate != null).ToList();
                        #endregion

                        if (!ContractStartdateNotNull.Any(x => string.IsNullOrEmpty(x.InspectionTime)))
                        {
                            InspectionContractorHours = ContractStartdateNotNull.All(a => int.Parse(a.InspectionTime) > 0);
                        }




                        if (InspectionEmployeeHours == false)
                        {
                            UserDialogs.Instance.HideLoading();
                            DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("PleasefillTechnicianHoursForInspection"), 2000);
                            return;
                        }
                        if (InspectionContractorHours == false)
                        {
                            UserDialogs.Instance.HideLoading();
                            DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("PleasefillTechnicianHoursForInspection"), 2000);
                            return;
                        }

                    }
                    else
                    {

                        if (workorderLabourWrapper.workOrderWrapper.workOrderLabors.Count == 0)
                        {
                            UserDialogs.Instance.HideLoading();

                            DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("TasksandLaborOrInspectionIsRequiredForCloseWO"), 2000);
                            return;
                        }
                        else if ((workorderLabourWrapper.workOrderWrapper.workOrderLabors.Count > 0))
                        {


                            foreach (var item in workorderLabourWrapper.workOrderWrapper.workOrderLabors)
                            {
                                bool AllTaskNumber = workorderLabourWrapper.workOrderWrapper.workOrderLabors.Any(a => string.IsNullOrWhiteSpace(a.TaskNumber));
                                if (AllTaskNumber == true)
                                {

                                    UserDialogs.Instance.HideLoading();
                                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("TasksandLaborOrInspectionIsRequiredForCloseWO"), 2000);
                                    return;

                                }
                                #region  Check all Employee Whose start date is not null
                                List<WorkOrderLabor> EmpStartdateNotNull = workorderLabourWrapper.workOrderWrapper.workOrderLabors.Where(x => x.StartDate != null).ToList();
                                #endregion
                                bool AllTaskHours = EmpStartdateNotNull.All(a => !string.IsNullOrWhiteSpace(a.HoursAtRate1.Replace("00.00", "")));

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

                bool fdasignatureKey = AppSettings.User.blackhawkLicValidator.IsFDASignatureValidation;

                if (fdasignatureKey == true)
                {
                    if (AppSettings.User.RequireSignaturesForValidation == "True")
                    {

                        Application.Current.Properties["SignatureFacID"] = workorderItem.FacilityID;
                        Application.Current.Properties["SignatureWOID"] = WorkorderID;
                        Application.Current.Properties["WorkorderNavigation"] = "False";
                        //  await NavigationService.NavigateBackAsync();
                        var page = new CloseWorkorderSignaturePage();
                        await PopupNavigation.PushAsync(page);
                    }
                    else
                    {

                        var workorder = new workOrderWrapper
                        {
                            TimeZone = AppSettings.UserTimeZone,
                            CultureName = AppSettings.UserCultureName,
                            UserId = Convert.ToInt32(UserID),
                            ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                            DistributeCost = workorderWrapper.workOrderWrapper.workOrder.DistributeCost,
                            workOrder = new workOrders
                            {
                                IsSignatureValidated = false,
                                FacilityID = workorderItem.FacilityID,
                                WorkOrderID = WorkorderID,
                                ModifiedUserName = AppSettings.UserName,

                            },

                        };


                        var response = await _workorderService.CloseWorkorder(workorder);

                        if (Boolean.Parse(response.servicestatus))
                        {
                            DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("WorkOrderSuccessfullyClosed"), 2000);
                            await RefillWorkorderCollection();
                        }
                        else
                        {
                            DialogService.ShowToast(response.servicestatusmessge, 2000);
                            UserDialogs.Instance.HideLoading();
                            return;


                        }
                    }
                }
                else
                {


                    var workorder = new workOrderWrapper
                    {
                        TimeZone = AppSettings.UserTimeZone,
                        CultureName = AppSettings.UserCultureName,
                        UserId = Convert.ToInt32(UserID),
                        ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                        DistributeCost = workorderWrapper.workOrderWrapper.workOrder.DistributeCost,
                        workOrder = new workOrders
                        {
                            IsSignatureValidated = false,
                            FacilityID = workorderItem.FacilityID,
                            WorkOrderID = WorkorderID,
                            ModifiedUserName = AppSettings.UserName,

                        },

                    };

                    var response = await _workorderService.CloseWorkorder(workorder);

                    if (Boolean.Parse(response.servicestatus))
                    {
                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("WorkOrderSuccessfullyClosed"), 2000);
                        await RefillWorkorderCollection();
                    }
                    else
                    {
                        DialogService.ShowToast(response.servicestatusmessge, 2000);
                        UserDialogs.Instance.HideLoading();
                        return;


                    }
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
            try
            {

                bool filter = false;

                UserDialogs.Instance.ShowLoading("Please wait..", MaskType.Gradient);
               // await Task.Delay(3000);
                if (string.IsNullOrWhiteSpace(this.SearchText))
                {
                    await RefillWorkorderCollection();

                }
                else
                {
                   await GetWorkordersFromSearchBar();
                }

                if (Application.Current.Properties.ContainsKey("SortingTypeKye"))
                {
                    var SortingType = Application.Current.Properties["SortingTypeKye"];
                    if (SortingType != null)
                    {
                        await SortByAction();

                    }

                }

                if (Application.Current.Properties.ContainsKey("WorkOFilterTypeKye"))
                {
                    var SortingType = Application.Current.Properties["WorkOFilterTypeKye"];
                    if (SortingType != null)
                    {
                        await SortByFilterAction();

                    }

                }


                if (Application.Current.Properties.ContainsKey("LocationFilterkey"))
                {
                    var Locationfilter = Application.Current.Properties["LocationFilterkey"];
                    if (Locationfilter != null)
                    {
                        if (Device.RuntimePlatform == Device.UWP)
                        {
                            GroupBy = "Assets/searchNs.png";

                        }
                        else
                        {
                            GroupBy = "searchNs.png";
                        }

                        filter = true;
                        await RefillLocationFromPicker(Locationfilter.ToString());

                    }
                    else
                    {
                        await RefillLocationFromPicker(null);
                    }
                }
                if (Application.Current.Properties.ContainsKey("ShiftFilterkey"))
                {
                    var Shiftfilter = Application.Current.Properties["ShiftFilterkey"];
                    if (Shiftfilter != null)
                    {
                        if (Device.RuntimePlatform == Device.UWP)
                        {
                            GroupBy = "Assets/searchNs.png";

                        }
                        else
                        {
                            GroupBy = "searchNs.png";
                        }

                        filter = true;
                        await RefillShiftFromPicker(Shiftfilter.ToString());

                    }
                    else
                    {

                        await RefillShiftFromPicker(null);
                    }
                }
                if (Application.Current.Properties.ContainsKey("PriorityFilterkey"))
                {
                    var Priorityfilter = Application.Current.Properties["PriorityFilterkey"];
                    if (Priorityfilter != null)
                    {
                        if (Device.RuntimePlatform == Device.UWP)
                        {
                            GroupBy = "Assets/searchNs.png";

                        }
                        else
                        {
                            GroupBy = "searchNs.png";
                        }

                        filter = true;
                        await RefillPriorityFromPicker(Priorityfilter.ToString());

                    }
                    else
                    {

                        await RefillPriorityFromPicker(null);
                    }
                }
                if (Application.Current.Properties.ContainsKey("DateFilterkey"))
                {
                    var datefilter = Application.Current.Properties["DateFilterkey"];
                    if (datefilter != null)
                    {
                        if (Device.RuntimePlatform == Device.UWP)
                        {
                            GroupBy = "Assets/searchNs.png";

                        }
                        else
                        {
                            GroupBy = "searchNs.png";
                        }

                        filter = true;
                        await RefillDueDateFromPicker(datefilter.ToString());

                    }
                    else
                    {

                        await RefillDueDateFromPicker(null);
                    }
                }
                if (filter == false)
                {
                    if (Device.RuntimePlatform == Device.UWP)
                    {
                        GroupBy = "Assets/searchN.png";

                    }
                    else
                    {
                        GroupBy = "searchN.png";
                    }

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

        public async Task OnViewDisappearingAsync(VisualElement view)
        {

            if (view == null)
            {
                this.SearchText = null;
            }
            //else
            //{
            //    ////Clear priority///
            //    sortByPrioritypickerTitlesitems.Clear();
            //    SortByPriorityPickerTitles.Clear();
            //    PriorityNameFilterText = null;
            //    WorkorderTypeFilterText = null;
            //    ////Clear Shift///
            //    sortByShiftpickerTitlesitems.Clear();
            //    SortByShiftpickerTitles.Clear();
            //    ShiftNameFilterText = null;


            //    ////Clear Location///
            //    sortByLocationpickerTitlesitems.Clear();
            //    SortByLocationpickerTitles.Clear();
            //    LocationNameFilterText = null;

            //    ///Clear RequiredDate////
            //    SortByDateText = null;
            //    SortByDueDate = null;

            //    this.SearchText = null;

            if (Application.Current.Properties.ContainsKey("overdue"))
            {
                Application.Current.Properties.Remove("overdue");
            }
            if (Application.Current.Properties.ContainsKey("weekly"))
            {
                Application.Current.Properties.Remove("weekly");
            }
            if (Application.Current.Properties.ContainsKey("today"))
            {
                Application.Current.Properties.Remove("today");
            }
            if (Application.Current.Properties.ContainsKey("PriorityID"))
            {
                Application.Current.Properties.Remove("PriorityID");
            }


            //    //if (Application.Current.Properties.ContainsKey("LocationFilterkey"))
            //    //{
            //    //    Application.Current.Properties.Remove("LocationFilterkey");
            //    //}
            //    //if (Application.Current.Properties.ContainsKey("ShiftFilterkey"))
            //    //{
            //    //    Application.Current.Properties.Remove("ShiftFilterkey");
            //    //}
            //    //if (Application.Current.Properties.ContainsKey("ShiftFilterkeyText"))
            //    //{
            //    //    Application.Current.Properties.Remove("ShiftFilterkeyText");
            //    //}
            //    //if (Application.Current.Properties.ContainsKey("PriorityFilterkey"))
            //    //{
            //    //    Application.Current.Properties.Remove("PriorityFilterkey");
            //    //}
            //    //if (Application.Current.Properties.ContainsKey("PriorityFilterkeyText"))
            //    //{
            //    //    Application.Current.Properties.Remove("PriorityFilterkeyText");
            //    //}
            //    //if (Application.Current.Properties.ContainsKey("DateFilterkey"))
            //    //{
            //    //    Application.Current.Properties.Remove("DateFilterkey");
            //    //}

            //    //if (Application.Current.Properties.ContainsKey("SortingTypeKye"))
            //    //{
            //    //    Application.Current.Properties.Remove("SortingTypeKye");
            //    //}

            //    //if (Application.Current.Properties.ContainsKey("WorkOFilterTypeKye"))
            //    //{
            //    //    Application.Current.Properties.Remove("WorkOFilterTypeKye");
            //    //}

            //    if (Device.RuntimePlatform == Device.UWP)
            //    {
            //        SortBy = "Assets/shortingN.png";
            //        FilterBy = "Assets/filterN.png";
            //        GroupBy = "Assets/searchN.png";
            //    }
            //    else
            //    {
            //        SortBy = "shortingN.png";
            //        FilterBy = "filterN.png";
            //        GroupBy = "searchN.png";
            //    }

            //}

        }

        private void SetCompletionDateResult(DatePromptResult result)
        {
            if (result.Ok)
            {

                if (result.Value.Date == DateTime.Parse("1/1/0001 12:00:00 AM"))
                {
                    SortByDateText = string.Empty;
                }
                else
                {
                    SortByDateText = result.SelectedDate.Date.ToString("dd MMM yyyy");
                }
            }

        }

        public async Task WorkorderCler()
        {

            ////Clear priority///
            sortByPrioritypickerTitlesitems.Clear();
            SortByPriorityPickerTitles.Clear();
            PriorityNameFilterText = null;
            WorkorderTypeFilterText = null;
            ////Clear Shift///
            sortByShiftpickerTitlesitems.Clear();
            SortByShiftpickerTitles.Clear();
            ShiftNameFilterText = null;


            ////Clear Location///
            sortByLocationpickerTitlesitems.Clear();
            SortByLocationpickerTitles.Clear();
            LocationNameFilterText = null;

            ///Clear RequiredDate////
            SortByDateText = null;
            SortByDueDate = null;

            this.SearchText = null;

            if (Application.Current.Properties.ContainsKey("overdue"))
            {
                Application.Current.Properties.Remove("overdue");
            }
            if (Application.Current.Properties.ContainsKey("weekly"))
            {
                Application.Current.Properties.Remove("weekly");
            }
            if (Application.Current.Properties.ContainsKey("today"))
            {
                Application.Current.Properties.Remove("today");
            }
            if (Application.Current.Properties.ContainsKey("PriorityID"))
            {
                Application.Current.Properties.Remove("PriorityID");
            }
            if (Application.Current.Properties.ContainsKey("LocationFilterkey"))
            {
                Application.Current.Properties.Remove("LocationFilterkey");
            }
            if (Application.Current.Properties.ContainsKey("ShiftFilterkey"))
            {
                Application.Current.Properties.Remove("ShiftFilterkey");
            }
            if (Application.Current.Properties.ContainsKey("ShiftFilterkeyText"))
            {
                Application.Current.Properties.Remove("ShiftFilterkeyText");
            }
            if (Application.Current.Properties.ContainsKey("PriorityFilterkey"))
            {
                Application.Current.Properties.Remove("PriorityFilterkey");
            }
            if (Application.Current.Properties.ContainsKey("PriorityFilterkeyText"))
            {
                Application.Current.Properties.Remove("PriorityFilterkeyText");
            }
            if (Application.Current.Properties.ContainsKey("DateFilterkey"))
            {
                Application.Current.Properties.Remove("DateFilterkey");
            }
            if (Application.Current.Properties.ContainsKey("SortingTypeKye"))
            {
                Application.Current.Properties.Remove("SortingTypeKye");
            }

            if (Application.Current.Properties.ContainsKey("WorkOFilterTypeKye"))
            {
                Application.Current.Properties.Remove("WorkOFilterTypeKye");
            }
            if (Device.RuntimePlatform == Device.UWP)
            {
                SortBy = "Assets/shortingN.png";
                FilterBy = "Assets/filterN.png";
                GroupBy = "Assets/searchN.png";
            }
            else
            {
                SortBy = "shortingN.png";
                FilterBy = "filterN.png";
                GroupBy = "searchN.png";
            }
        }
        #endregion

    }
}
