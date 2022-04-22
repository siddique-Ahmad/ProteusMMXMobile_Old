using Acr.UserDialogs;
using ProteusMMX.Constants;
using ProteusMMX.Controls;
using ProteusMMX.Converters;
using ProteusMMX.DependencyInterface;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.DateTime;
using ProteusMMX.Helpers.StringFormatter;
using ProteusMMX.Helpers.Validation;
using ProteusMMX.Model;
using ProteusMMX.Model.AssetModel;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Asset;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.SelectionListPagesViewModels;
using ProteusMMX.ViewModel.SelectionListPagesViewModels.Asset;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ProteusMMX.Services.SelectionListPageServices;
using Syncfusion.XForms.Border;

namespace ProteusMMX.ViewModel.Asset
{
    public class CreateNewAssetPageViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly IAssetModuleService _assetService;

        protected readonly IWorkorderService _workorderService;
        protected readonly IFacilityService _facilityService;
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

        ServiceOutput _formLoadInputForWorkorder;
        public ServiceOutput FormLoadInputForWorkorder //Use For Only translation purposes
        {
            get { return _formLoadInputForWorkorder; }
            set
            {
                if (value != _formLoadInputForWorkorder)
                {
                    _formLoadInputForWorkorder = value;
                    OnPropertyChanged(nameof(FormLoadInputForWorkorder));
                }
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
                    OnPropertyChanged(nameof(FormControlsAndRights));
                }
            }
        }



        List<FormControl> _overriddenControlsNew = new List<FormControl>();
        public List<FormControl> OverriddenControlsNew
        {
            get
            {
                return _overriddenControlsNew;
            }

            set
            {
                _overriddenControlsNew = value;
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



        #endregion

        #region Title Properties

        string _assetNameTitle;
        public string AssetNameTitle
        {
            get
            {
                return _assetNameTitle;
            }

            set
            {
                if (value != _assetNameTitle)
                {
                    _assetNameTitle = value;
                    OnPropertyChanged("AssetNameTitle");
                }
            }
        }

        string _assetNumberTitle;
        public string AssetNumberTitle
        {
            get
            {
                return _assetNumberTitle;
            }

            set
            {
                if (value != _assetNumberTitle)
                {
                    _assetNumberTitle = value;
                    OnPropertyChanged("AssetNumberTitle");
                }
            }
        }



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



        #endregion



        #region CreateAsset Properties





        string _serverTimeZone = AppSettings.User.ServerIANATimeZone;
        public string ServerTimeZone
        {
            get { return _serverTimeZone; }
        }


        bool? _inspectionUser = AppSettings.User.IsInspectionUser;
        public bool? InspectionUser
        {
            get { return _inspectionUser; }
        }



        bool _isPickerDataRequested;
        public bool IsPickerDataRequested
        {
            get { return _isPickerDataRequested; }
            set
            {
                if (value != _isPickerDataRequested)
                {
                    _isPickerDataRequested = value;
                    OnPropertyChanged(nameof(IsPickerDataRequested));
                }
            }
        }

        bool _isPickerDataSubscribed;
        public bool IsPickerDataSubscribed
        {
            get { return _isPickerDataSubscribed; }
            set
            {
                if (value != _isPickerDataSubscribed)
                {
                    _isPickerDataSubscribed = value;
                    OnPropertyChanged(nameof(IsPickerDataSubscribed));
                }
            }
        }


        #region Normal Field Properties


        // Asset Number
        string _assetNumberText;
        public string AssetNumberText
        {
            get
            {
                return _assetNumberText;
            }

            set
            {
                if (value != _assetNumberText)
                {
                    _assetNumberText = value;
                    OnPropertyChanged(nameof(AssetNumberText));
                }
            }
        }

        string _assetNameText;
        public string AssetNameText
        {
            get
            {
                return _assetNameText;
            }

            set
            {
                if (value != _assetNameText)
                {
                    _assetNameText = value;
                    OnPropertyChanged(nameof(AssetNameText));
                }
            }
        }


        // Job Number
        string _jobNumberText;
        public string JobNumberText
        {
            get
            {
                return _jobNumberText;
            }

            set
            {
                if (value != _jobNumberText)
                {
                    _jobNumberText = value;
                    OnPropertyChanged(nameof(JobNumberText));
                }
            }
        }

        string _jobNumberTitle;
        public string JobNumberTitle
        {
            get
            {
                return _jobNumberTitle;
            }

            set
            {
                if (value != _jobNumberTitle)
                {
                    _jobNumberTitle = value;
                    OnPropertyChanged(nameof(JobNumberTitle));
                }
            }
        }

        //Description
        string _descriptionText;
        public string DescriptionText
        {
            get
            {
                return _descriptionText;
            }

            set
            {
                if (value != _descriptionText)
                {
                    _descriptionText = value;
                    OnPropertyChanged(nameof(DescriptionText));
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
                    OnPropertyChanged(nameof(DescriptionTitle));
                }
            }
        }
        string _saveTitle;
        public string SaveTitle
        {
            get
            {
                return _saveTitle;
            }

            set
            {
                if (value != _saveTitle)
                {
                    _saveTitle = value;
                    OnPropertyChanged(nameof(SaveTitle));
                }
            }
        }
        
        bool _descriptionIsEnable = true;
        public bool DescriptionIsEnable
        {
            get
            {
                return _descriptionIsEnable;
            }

            set
            {
                if (value != _descriptionIsEnable)
                {
                    _descriptionIsEnable = value;
                    OnPropertyChanged(nameof(DescriptionIsEnable));
                }
            }
        }


        // Required Date
        DateTime _requiredDate = DateTime.Now;
        public DateTime RequiredDate
        {
            get
            {
                return _requiredDate;
            }

            set
            {
                if (value != _requiredDate)
                {
                    _requiredDate = value;
                    OnPropertyChanged(nameof(RequiredDate));
                }
            }
        }

        DateTime _minimumRequiredDate;
        public DateTime MinimumRequiredDate
        {
            get
            {
                return _minimumRequiredDate;
            }

            set
            {
                if (value != _minimumRequiredDate)
                {
                    _minimumRequiredDate = value;
                    OnPropertyChanged(nameof(MinimumRequiredDate));
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
                    OnPropertyChanged(nameof(RequiredDateTitle));
                }
            }
        }

        bool _requiredDateIsEnable = true;
        public bool RequiredDateIsEnable
        {
            get
            {
                return _requiredDateIsEnable;
            }

            set
            {
                if (value != _requiredDateIsEnable)
                {
                    _requiredDateIsEnable = value;
                    OnPropertyChanged(nameof(RequiredDateIsEnable));
                }
            }
        }


        // workorder started Date
        DateTime? _workStartedDate;
        public DateTime? WorkStartedDate
        {
            get
            {
                return _workStartedDate;
            }

            set
            {
                if (value != _workStartedDate)
                {
                    _workStartedDate = value;
                    OnPropertyChanged(nameof(WorkStartedDate));
                }
            }
        }

        DateTime? _minimumWorkStartedDate;
        public DateTime? MinimumWorkStartedDate
        {
            get
            {
                return _minimumWorkStartedDate;
            }

            set
            {
                if (value != _minimumWorkStartedDate)
                {
                    _minimumWorkStartedDate = value;
                    OnPropertyChanged(nameof(MinimumWorkStartedDate));
                }
            }
        }

        DateTime? _maximumWorkStartedDate;
        public DateTime? MaximumWorkStartedDate
        {
            get
            {
                return _maximumWorkStartedDate;
            }

            set
            {
                if (value != _maximumWorkStartedDate)
                {
                    _maximumWorkStartedDate = value;
                    OnPropertyChanged(nameof(MaximumWorkStartedDate));
                }
            }
        }

        string _workStartedDateTitle;
        public string WorkStartedDateTitle
        {
            get
            {
                return _workStartedDateTitle;
            }

            set
            {
                if (value != _workStartedDateTitle)
                {
                    _workStartedDateTitle = value;
                    OnPropertyChanged(nameof(WorkStartedDateTitle));
                }
            }
        }


        string _workStartedDateWarningText;
        public string WorkStartedDateWarningText
        {
            get
            {
                return _workStartedDateWarningText;
            }

            set
            {
                if (value != _workStartedDateWarningText)
                {
                    _workStartedDateWarningText = value;
                    OnPropertyChanged(nameof(WorkStartedDateWarningText));
                }
            }
        }


        bool _workStartedDateIsEnable = true;
        public bool WorkStartedDateIsEnable
        {
            get
            {
                return _workStartedDateIsEnable;
            }

            set
            {
                if (value != _workStartedDateIsEnable)
                {
                    _workStartedDateIsEnable = value;
                    OnPropertyChanged(nameof(WorkStartedDateIsEnable));
                }
            }
        }



        // workorder Completion Date
        DateTime? _workorderCompletionDate;
        public DateTime? WorkorderCompletionDate
        {
            get
            {
                return _workorderCompletionDate;
            }

            set
            {
                if (value != _workorderCompletionDate)
                {
                    _workorderCompletionDate = value;
                    OnPropertyChanged(nameof(WorkorderCompletionDate));
                }
            }
        }

        DateTime? _minimumWorkorderCompletionDate;
        public DateTime? MinimumWorkorderCompletionDate
        {
            get
            {
                return _minimumWorkorderCompletionDate;
            }

            set
            {
                if (value != _minimumWorkorderCompletionDate)
                {
                    _minimumWorkorderCompletionDate = value;
                    OnPropertyChanged(nameof(MinimumWorkorderCompletionDate));
                }
            }
        }

        DateTime? _maximumWorkorderCompletionDate;
        public DateTime? MaximumWorkorderCompletionDate
        {
            get
            {
                return _maximumWorkorderCompletionDate;
            }

            set
            {
                if (value != _maximumWorkorderCompletionDate)
                {
                    _maximumWorkorderCompletionDate = value;
                    OnPropertyChanged(nameof(MaximumWorkorderCompletionDate));
                }
            }
        }

        string _workorderCompletionDateTitle;
        public string WorkorderCompletionDateTitle
        {
            get
            {
                return _workorderCompletionDateTitle;
            }

            set
            {
                if (value != _workorderCompletionDateTitle)
                {
                    _workorderCompletionDateTitle = value;
                    OnPropertyChanged(nameof(WorkorderCompletionDateTitle));
                }
            }
        }


        string _workorderCompletionDateWarningText;
        public string WorkorderCompletionDateWarningText
        {
            get
            {
                return _workorderCompletionDateWarningText;
            }

            set
            {
                if (value != _workorderCompletionDateWarningText)
                {
                    _workorderCompletionDateWarningText = value;
                    OnPropertyChanged(nameof(WorkorderCompletionDateWarningText));
                }
            }
        }

        bool _workorderCompletionDateIsEnable = true;
        public bool WorkorderCompletionDateIsEnable
        {
            get
            {
                return _workorderCompletionDateIsEnable;
            }

            set
            {
                if (value != _workorderCompletionDateIsEnable)
                {
                    _workorderCompletionDateIsEnable = value;
                    OnPropertyChanged(nameof(WorkorderCompletionDateIsEnable));
                }
            }
        }

        // Facility
        int? _facilityID;
        public int? FacilityID
        {
            get
            {
                return _facilityID;
            }

            set
            {
                if (value != _facilityID)
                {
                    _facilityID = value;
                    OnPropertyChanged(nameof(FacilityID));
                }
            }
        }

        string _facilityName;
        public string FacilityName
        {
            get
            {
                return _facilityName;
            }

            set
            {
                if (value != _facilityName)
                {
                    _facilityName = value;
                    OnPropertyChanged(nameof(FacilityName));
                }
            }
        }

        string _facilityTitle;
        public string FacilityTitle
        {
            get
            {
                return _facilityTitle;
            }

            set
            {
                if (value != _facilityTitle)
                {
                    _facilityTitle = value;
                    OnPropertyChanged(nameof(FacilityTitle));
                }
            }
        }

        bool _facilityIsEnable = true;
        public bool FacilityIsEnable
        {
            get
            {
                return _facilityIsEnable;
            }

            set
            {
                if (value != _facilityIsEnable)
                {
                    _facilityIsEnable = value;
                    OnPropertyChanged(nameof(FacilityIsEnable));
                }
            }
        }

        // Location
        int? _locationID;
        public int? LocationID
        {
            get
            {
                return _locationID;
            }

            set
            {
                if (value != _locationID)
                {
                    _locationID = value;
                    OnPropertyChanged(nameof(LocationID));
                }
            }
        }

        string _locationName;
        public string LocationName
        {
            get
            {
                return _locationName;
            }

            set
            {
                if (value != _locationName)
                {
                    _locationName = value;
                    OnPropertyChanged(nameof(LocationName));
                }
            }
        }

        string _locationTitle;
        public string LocationTitle
        {
            get
            {
                return _locationTitle;
            }

            set
            {
                if (value != _locationTitle)
                {
                    _locationTitle = value;
                    OnPropertyChanged(nameof(LocationTitle));
                }
            }
        }

        bool _locationIsEnable = true;
        public bool LocationIsEnable
        {
            get
            {
                return _locationIsEnable;
            }

            set
            {
                if (value != _locationIsEnable)
                {
                    _locationIsEnable = value;
                    OnPropertyChanged(nameof(LocationIsEnable));
                }
            }
        }


        // Asset
        int? _assetID;
        public int? AssetID
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
                    OnPropertyChanged(nameof(AssetName));
                }
            }
        }

        string _assetTitle;
        public string AssetTitle
        {
            get
            {
                return _assetTitle;
            }

            set
            {
                if (value != _assetTitle)
                {
                    _assetTitle = value;
                    OnPropertyChanged(nameof(AssetTitle));
                }
            }
        }


        bool _assetIsEnable = true;
        public bool AssetIsEnable
        {
            get
            {
                return _assetIsEnable;
            }

            set
            {
                if (value != _assetIsEnable)
                {
                    _assetIsEnable = value;
                    OnPropertyChanged(nameof(AssetIsEnable));
                }
            }
        }
        bool _assetSystemIsEnable = true;
        public bool AssetSystemIsEnable
        {
            get
            {
                return _assetSystemIsEnable;
            }

            set
            {
                if (value != _assetSystemIsEnable)
                {
                    _assetSystemIsEnable = value;
                    OnPropertyChanged(nameof(AssetSystemIsEnable));
                }
            }
        }
        bool _assetSystemIsVisible = false;
        public bool AssetSystemIsVisible
        {
            get
            {
                return _assetSystemIsVisible;
            }

            set
            {
                if (value != _assetSystemIsVisible)
                {
                    _assetSystemIsVisible = value;
                    OnPropertyChanged(nameof(AssetSystemIsVisible));
                }
            }
        }

        bool _locationForLicensingIsVisible = true;
        public bool LocationForLicensingIsVisible
        {
            get
            {
                return _locationForLicensingIsVisible;
            }

            set
            {
                if (value != _locationForLicensingIsVisible)
                {
                    _locationForLicensingIsVisible = value;
                    OnPropertyChanged(nameof(LocationForLicensingIsVisible));
                }
            }
        }
        bool _locationIsVisible = true;
        public bool LocationIsVisible
        {
            get
            {
                return _locationIsVisible;
            }

            set
            {
                if (value != _locationIsVisible)
                {
                    _locationIsVisible = value;
                    OnPropertyChanged(nameof(LocationIsVisible));
                }
            }
        }


        

        bool _categoryIsVisible = false;
        public bool CategoryIsVisible
        {
            get
            {
                return _categoryIsVisible;
            }

            set
            {
                if (value != _categoryIsVisible)
                {
                    _categoryIsVisible = value;
                    OnPropertyChanged(nameof(CategoryIsVisible));
                }
            }
        }
        bool _runtimeUnitIsVisible = false;
        public bool RuntimeUnitIsVisible
        {
            get
            {
                return _runtimeUnitIsVisible;
            }

            set
            {
                if (value != _runtimeUnitIsVisible)
                {
                    _runtimeUnitIsVisible = value;
                    OnPropertyChanged(nameof(RuntimeUnitIsVisible));
                }
            }
        }
        bool _vendorIsVisible = false;
        public bool VendorIsVisible
        {
            get
            {
                return _vendorIsVisible;
            }

            set
            {
                if (value != _vendorIsVisible)
                {
                    _vendorIsVisible = value;
                    OnPropertyChanged(nameof(VendorIsVisible));
                }
            }
        }
        bool _assetNameIsEnable = true;
        public bool AssetNameIsEnable
        {
            get
            {
                return _assetNameIsEnable;
            }

            set
            {
                if (value != _assetNameIsEnable)
                {
                    _assetNameIsEnable = value;
                    OnPropertyChanged(nameof(AssetNameIsEnable));
                }
            }
        }
        bool _assetNumberIsEnable = true;
        public bool AssetNumberIsEnable
        {
            get
            {
                return _assetNumberIsEnable;
            }

            set
            {
                if (value != _assetNumberIsEnable)
                {
                    _assetNumberIsEnable = value;
                    OnPropertyChanged(nameof(AssetNumberIsEnable));
                }
            }
        }
        bool _categoryIsEnable = true;
        public bool CategoryIsEnable
        {
            get
            {
                return _categoryIsEnable;
            }

            set
            {
                if (value != _categoryIsEnable)
                {
                    _categoryIsEnable = value;
                    OnPropertyChanged(nameof(CategoryIsEnable));
                }
            }
        }

        bool _vendorIsEnable = true;
        public bool VendorIsEnable
        {
            get
            {
                return _vendorIsEnable;
            }

            set
            {
                if (value != _vendorIsEnable)
                {
                    _vendorIsEnable = value;
                    OnPropertyChanged(nameof(VendorIsEnable));
                }
            }
        }
        bool _runtimeUnitCommandIsEnable = true;
        public bool RuntimeUnitCommandIsEnable
        {
            get
            {
                return _runtimeUnitCommandIsEnable;
            }

            set
            {
                if (value != _runtimeUnitCommandIsEnable)
                {
                    _runtimeUnitCommandIsEnable = value;
                    OnPropertyChanged(nameof(RuntimeUnitCommandIsEnable));
                }
            }
        }
        


        // Asset System
        int? _assetSystemID;
        public int? AssetSystemID
        {
            get
            {
                return _assetSystemID;
            }

            set
            {
                if (value != _assetSystemID)
                {
                    _assetSystemID = value;
                    OnPropertyChanged(nameof(AssetSystemID));
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

        string _assetSystemTitle;
        public string AssetSystemTitle
        {
            get
            {
                return _assetSystemTitle;
            }

            set
            {
                if (value != _assetSystemTitle)
                {
                    _assetSystemTitle = value;
                    OnPropertyChanged(nameof(AssetSystemTitle));
                }
            }
        }
        string _categoryTitle;
        public string CategoryTitle
        {
            get
            {
                return _categoryTitle;
            }

            set
            {
                if (value != _categoryTitle)
                {
                    _categoryTitle = value;
                    OnPropertyChanged(nameof(CategoryTitle));
                }
            }
        }
        string _vendorTitle;
        public string VendorTitle
        {
            get
            {
                return _vendorTitle;
            }

            set
            {
                if (value != _vendorTitle)
                {
                    _vendorTitle = value;
                    OnPropertyChanged(nameof(VendorTitle));
                }
            }
        }
        string _runtimeUnitTitle;
        public string RuntimeUnitTitle
        {
            get
            {
                return _runtimeUnitTitle;
            }

            set
            {
                if (value != _runtimeUnitTitle)
                {
                    _runtimeUnitTitle = value;
                    OnPropertyChanged(nameof(RuntimeUnitTitle));
                }
            }
        }

       

        // AssignToEmployee
        int? _assignToEmployeeID;
        public int? AssignToEmployeeID
        {
            get
            {
                return _assignToEmployeeID;
            }

            set
            {
                if (value != _assignToEmployeeID)
                {
                    _assignToEmployeeID = value;
                    OnPropertyChanged(nameof(AssignToEmployeeID));
                }
            }
        }

        string _assignToEmployeeName;
        public string AssignToEmployeeName
        {
            get
            {
                return _assignToEmployeeName;
            }

            set
            {
                if (value != _assignToEmployeeName)
                {
                    _assignToEmployeeName = value;
                    OnPropertyChanged(nameof(AssignToEmployeeName));
                }
            }
        }

        string _assignToEmployeeTitle;
        public string AssignToEmployeeTitle
        {
            get
            {
                return _assignToEmployeeTitle;
            }

            set
            {
                if (value != _assignToEmployeeTitle)
                {
                    _assignToEmployeeTitle = value;
                    OnPropertyChanged(nameof(AssignToEmployeeTitle));
                }
            }
        }

        bool _assignToEmployeeIsEnable = true;
        public bool AssignToEmployeeIsEnable
        {
            get
            {
                return _assignToEmployeeIsEnable;
            }

            set
            {
                if (value != _assignToEmployeeIsEnable)
                {
                    _assignToEmployeeIsEnable = value;
                    OnPropertyChanged(nameof(AssignToEmployeeIsEnable));
                }
            }
        }

        // Workorder Requester
        int? _workorderRequesterID;
        public int? WorkorderRequesterID
        {
            get
            {
                return _workorderRequesterID;
            }

            set
            {
                if (value != _workorderRequesterID)
                {
                    _workorderRequesterID = value;
                    OnPropertyChanged(nameof(WorkorderRequesterID));
                }
            }
        }

        string _workorderRequesterName;
        public string WorkorderRequesterName
        {
            get
            {
                return _workorderRequesterName;
            }

            set
            {
                if (value != _workorderRequesterName)
                {
                    _workorderRequesterName = value;
                    OnPropertyChanged(nameof(WorkorderRequesterName));
                }
            }
        }

        string _workorderRequesterTitle;
        public string WorkorderRequesterTitle
        {
            get
            {
                return _workorderRequesterTitle;
            }

            set
            {
                if (value != _workorderRequesterTitle)
                {
                    _workorderRequesterTitle = value;
                    OnPropertyChanged(nameof(WorkorderRequesterTitle));
                }
            }
        }

        bool _workorderRequesterIsEnable = true;
        public bool WorkorderRequesterIsEnable
        {
            get
            {
                return _workorderRequesterIsEnable;
            }

            set
            {
                if (value != _workorderRequesterIsEnable)
                {
                    _workorderRequesterIsEnable = value;
                    OnPropertyChanged(nameof(WorkorderRequesterIsEnable));
                }
            }
        }


        // Cost Center
        int? _costCenterID;
        public int? CostCenterID
        {
            get
            {
                return _costCenterID;
            }

            set
            {
                if (value != _costCenterID)
                {
                    _costCenterID = value;
                    OnPropertyChanged(nameof(CostCenterID));
                }
            }
        }

        string _costCenterName;
        public string CostCenterName
        {
            get
            {
                return _costCenterName;
            }

            set
            {
                if (value != _costCenterName)
                {
                    _costCenterName = value;
                    OnPropertyChanged(nameof(CostCenterName));
                }
            }
        }

        string _costCenterTitle;
        public string CostCenterTitle
        {
            get
            {
                return _costCenterTitle;
            }

            set
            {
                if (value != _costCenterTitle)
                {
                    _costCenterTitle = value;
                    OnPropertyChanged(nameof(CostCenterTitle));
                }
            }
        }

        bool _costCenterIsEnable = true;
        public bool CostCenterIsEnable
        {
            get
            {
                return _costCenterIsEnable;
            }

            set
            {
                if (value != _costCenterIsEnable)
                {
                    _costCenterIsEnable = value;
                    OnPropertyChanged(nameof(CostCenterIsEnable));
                }
            }
        }


        // Priority
        int? _priorityID;
        public int? PriorityID
        {
            get
            {
                return _priorityID;
            }

            set
            {
                if (value != _priorityID)
                {
                    _priorityID = value;
                    OnPropertyChanged(nameof(PriorityID));
                }
            }
        }

        string _priorityName;
        public string PriorityName
        {
            get
            {
                return _priorityName;
            }

            set
            {
                if (value != _priorityName)
                {
                    _priorityName = value;
                    OnPropertyChanged(nameof(PriorityName));
                }
            }
        }

        string _priorityTitle;
        public string PriorityTitle
        {
            get
            {
                return _priorityTitle;
            }

            set
            {
                if (value != _priorityTitle)
                {
                    _priorityTitle = value;
                    OnPropertyChanged(nameof(PriorityTitle));
                }
            }
        }

        bool _priorityIsEnable = true;
        public bool PriorityIsEnable
        {
            get
            {
                return _priorityIsEnable;
            }

            set
            {
                if (value != _priorityIsEnable)
                {
                    _priorityIsEnable = value;
                    OnPropertyChanged(nameof(PriorityIsEnable));
                }
            }
        }

        // Shift
        int? _shiftID;
        public int? ShiftID
        {
            get
            {
                return _shiftID;
            }

            set
            {
                if (value != _shiftID)
                {
                    _shiftID = value;
                    OnPropertyChanged(nameof(ShiftID));
                }
            }
        }

        string _shiftName;
        public string ShiftName
        {
            get
            {
                return _shiftName;
            }

            set
            {
                if (value != _shiftName)
                {
                    _shiftName = value;
                    OnPropertyChanged(nameof(ShiftName));
                }
            }
        }

        string _shiftTitle;
        public string ShiftTitle
        {
            get
            {
                return _shiftTitle;
            }

            set
            {
                if (value != _shiftTitle)
                {
                    _shiftTitle = value;
                    OnPropertyChanged(nameof(ShiftTitle));
                }
            }
        }

        bool _shiftIsEnable = true;
        public bool ShiftIsEnable
        {
            get
            {
                return _shiftIsEnable;
            }

            set
            {
                if (value != _shiftIsEnable)
                {
                    _shiftIsEnable = value;
                    OnPropertyChanged(nameof(ShiftIsEnable));
                }
            }
        }


        // WorkorderStatus
        int? _workorderStatusID;
        public int? WorkorderStatusID
        {
            get
            {
                return _workorderStatusID;
            }

            set
            {
                if (value != _workorderStatusID)
                {
                    _workorderStatusID = value;
                    OnPropertyChanged(nameof(WorkorderStatusID));
                }
            }
        }

        string _workorderStatusName;
        public string WorkorderStatusName
        {
            get
            {
                return _workorderStatusName;
            }

            set
            {
                if (value != _workorderStatusName)
                {
                    _workorderStatusName = value;
                    OnPropertyChanged(nameof(WorkorderStatusName));
                }
            }
        }

        string _workorderStatusTitle;
        public string WorkorderStatusTitle
        {
            get
            {
                return _workorderStatusTitle;
            }

            set
            {
                if (value != _workorderStatusTitle)
                {
                    _workorderStatusTitle = value;
                    OnPropertyChanged(nameof(WorkorderStatusTitle));
                }
            }
        }

        bool _workorderStatusIsEnable = true;
        public bool WorkorderStatusIsEnable
        {
            get
            {
                return _workorderStatusIsEnable;
            }

            set
            {
                if (value != _workorderStatusIsEnable)
                {
                    _workorderStatusIsEnable = value;
                    OnPropertyChanged(nameof(WorkorderStatusIsEnable));
                }
            }
        }


        // WorkorderType
        int? _workorderTypeID;
        public int? WorkorderTypeID
        {
            get
            {
                return _workorderTypeID;
            }

            set
            {
                if (value != _workorderTypeID)
                {
                    _workorderTypeID = value;
                    OnPropertyChanged(nameof(WorkorderTypeID));
                }
            }
        }

        string _workorderTypeName;
        public string WorkorderTypeName
        {
            get
            {
                return _workorderTypeName;
            }

            set
            {
                if (value != _workorderTypeName)
                {
                    _workorderTypeName = value;
                    OnPropertyChanged(nameof(WorkorderTypeName));
                }
            }
        }

        string _workorderTypeTitle;
        public string WorkorderTypeTitle
        {
            get
            {
                return _workorderTypeTitle;
            }

            set
            {
                if (value != _workorderTypeTitle)
                {
                    _workorderTypeTitle = value;
                    OnPropertyChanged(nameof(WorkorderTypeTitle));
                }
            }
        }

        bool _workorderTypeIsEnable = true;
        public bool WorkorderTypeIsEnable
        {
            get
            {
                return _workorderTypeIsEnable;
            }

            set
            {
                if (value != _workorderTypeIsEnable)
                {
                    _workorderTypeIsEnable = value;
                    OnPropertyChanged(nameof(WorkorderTypeIsEnable));
                }
            }
        }



        // Cause
        int? _causeID;
        public int? CauseID
        {
            get
            {
                return _causeID;
            }

            set
            {
                if (value != _causeID)
                {
                    _causeID = value;
                    OnPropertyChanged(nameof(CauseID));
                }
            }
        }

        string _causeName;
        public string CauseName
        {
            get
            {
                return _causeName;
            }

            set
            {
                if (value != _causeName)
                {
                    _causeName = value;
                    OnPropertyChanged(nameof(CauseName));
                }
            }
        }

        string _causeTitle;
        public string CauseTitle
        {
            get
            {
                return _causeTitle;
            }

            set
            {
                if (value != _causeTitle)
                {
                    _causeTitle = value;
                    OnPropertyChanged(nameof(CauseTitle));
                }
            }
        }

        bool _causeIsEnable = true;
        public bool CauseIsEnable
        {
            get
            {
                return _causeIsEnable;
            }

            set
            {
                if (value != _causeIsEnable)
                {
                    _causeIsEnable = value;
                    OnPropertyChanged(nameof(CauseIsEnable));
                }
            }
        }


        // Maintenance Code
        int? _maintenanceCodeID;
        public int? MaintenanceCodeID
        {
            get
            {
                return _maintenanceCodeID;
            }

            set
            {
                if (value != _maintenanceCodeID)
                {
                    _maintenanceCodeID = value;
                    OnPropertyChanged(nameof(MaintenanceCodeID));
                }
            }
        }

        string _maintenanceCodeName;
        public string MaintenanceCodeName
        {
            get
            {
                return _maintenanceCodeName;
            }

            set
            {
                if (value != _maintenanceCodeName)
                {
                    _maintenanceCodeName = value;
                    OnPropertyChanged(nameof(MaintenanceCodeName));
                }
            }
        }

        string _maintenanceCodeTitle;
        public string MaintenanceCodeTitle
        {
            get
            {
                return _maintenanceCodeTitle;
            }

            set
            {
                if (value != _maintenanceCodeTitle)
                {
                    _maintenanceCodeTitle = value;
                    OnPropertyChanged(nameof(MaintenanceCodeTitle));
                }
            }
        }


        bool _maintenanceCodeIsEnable = true;
        public bool MaintenanceCodeIsEnable
        {
            get
            {
                return _maintenanceCodeIsEnable;
            }

            set
            {
                if (value != _maintenanceCodeIsEnable)
                {
                    _maintenanceCodeIsEnable = value;
                    OnPropertyChanged(nameof(MaintenanceCodeIsEnable));
                }
            }
        }



        // AdditionalDetails

        string _additionalDetailsText;
        public string AdditionalDetailsText
        {
            get
            {
                return _additionalDetailsText;
            }

            set
            {
                if (value != _additionalDetailsText)
                {
                    _additionalDetailsText = value;
                    OnPropertyChanged(nameof(AdditionalDetailsText));
                }
            }
        }

        string _additionalDetailsTitle;
        public string AdditionalDetailsTitle
        {
            get
            {
                return _additionalDetailsTitle;
            }

            set
            {
                if (value != _additionalDetailsTitle)
                {
                    _additionalDetailsTitle = value;
                    OnPropertyChanged(nameof(AdditionalDetailsTitle));
                }
            }
        }


        bool _additionalDetailsIsEnable = true;
        public bool AdditionalDetailsIsEnable
        {
            get
            {
                return _additionalDetailsIsEnable;
            }

            set
            {
                if (value != _additionalDetailsIsEnable)
                {
                    _additionalDetailsIsEnable = value;
                    OnPropertyChanged(nameof(AdditionalDetailsIsEnable));
                }
            }
        }

        bool _additionalDetailsIsVisible = true;
        public bool AdditionalDetailsIsVisible
        {
            get
            {
                return _additionalDetailsIsVisible;
            }

            set
            {
                if (value != _additionalDetailsIsVisible)
                {
                    _additionalDetailsIsVisible = value;
                    OnPropertyChanged(nameof(AdditionalDetailsIsVisible));
                }
            }
        }
        // EstimstedDowntime

        string _estimstedDowntimeText;
        public string EstimstedDowntimeText
        {
            get
            {
                return _estimstedDowntimeText;
            }

            set
            {
                if (value != _estimstedDowntimeText)
                {
                    _estimstedDowntimeText = value;
                    OnPropertyChanged(nameof(EstimstedDowntimeText));
                }
            }
        }

        string _estimstedDowntimeTitle;
        public string EstimstedDowntimeTitle
        {
            get
            {
                return _estimstedDowntimeTitle;
            }

            set
            {
                if (value != _estimstedDowntimeTitle)
                {
                    _estimstedDowntimeTitle = value;
                    OnPropertyChanged(nameof(EstimstedDowntimeTitle));
                }
            }
        }

        bool _estimstedDowntimeIsEnable = true;
        public bool EstimstedDowntimeIsEnable
        {
            get
            {
                return _estimstedDowntimeIsEnable;
            }

            set
            {
                if (value != _estimstedDowntimeIsEnable)
                {
                    _estimstedDowntimeIsEnable = value;
                    OnPropertyChanged(nameof(EstimstedDowntimeIsEnable));
                }
            }
        }

        // ActualDowntime

        string _actualDowntimeText;
        public string ActualDowntimeText
        {
            get
            {
                return _actualDowntimeText;
            }

            set
            {
                if (value != _actualDowntimeText)
                {
                    _actualDowntimeText = value;
                    OnPropertyChanged(nameof(ActualDowntimeText));
                }
            }
        }

        string _actualDowntimeTitle;
        public string ActualDowntimeTitle
        {
            get
            {
                return _actualDowntimeTitle;
            }

            set
            {
                if (value != _actualDowntimeTitle)
                {
                    _actualDowntimeTitle = value;
                    OnPropertyChanged(nameof(ActualDowntimeTitle));
                }
            }
        }

        bool _actualDowntimeIsEnable = true;
        public bool ActualDowntimeIsEnable
        {
            get
            {
                return _actualDowntimeIsEnable;
            }

            set
            {
                if (value != _actualDowntimeIsEnable)
                {
                    _actualDowntimeIsEnable = value;
                    OnPropertyChanged(nameof(ActualDowntimeIsEnable));
                }
            }
        }


        // MicellaneousLabourCost

        string _miscellaneousLabourCostText;
        public string MiscellaneousLabourCostText
        {
            get
            {
                return _miscellaneousLabourCostText;
            }

            set
            {
                if (value != _miscellaneousLabourCostText)
                {
                    _miscellaneousLabourCostText = value;
                    OnPropertyChanged(nameof(MiscellaneousLabourCostText));
                }
            }
        }

        string _miscellaneousLabourCostTitle;
        public string MiscellaneousLabourCostTitle
        {
            get
            {
                return _miscellaneousLabourCostTitle;
            }

            set
            {
                if (value != _miscellaneousLabourCostTitle)
                {
                    _miscellaneousLabourCostTitle = value;
                    OnPropertyChanged(nameof(MiscellaneousLabourCostTitle));
                }
            }
        }

        bool _miscellaneousLabourCostIsEnable = true;
        public bool MiscellaneousLabourCostIsEnable
        {
            get
            {
                return _miscellaneousLabourCostIsEnable;
            }

            set
            {
                if (value != _miscellaneousLabourCostIsEnable)
                {
                    _miscellaneousLabourCostIsEnable = value;
                    OnPropertyChanged(nameof(MiscellaneousLabourCostIsEnable));
                }
            }
        }

        // MicellaneousMaterialCost

        string _miscellaneousMaterialCostText;
        public string MiscellaneousMaterialCostText
        {
            get
            {
                return _miscellaneousMaterialCostText;
            }

            set
            {
                if (value != _miscellaneousMaterialCostText)
                {
                    _miscellaneousMaterialCostText = value;
                    OnPropertyChanged(nameof(MiscellaneousMaterialCostText));
                }
            }
        }

        string _miscellaneousMaterialCostTitle;
        public string MiscellaneousMaterialCostTitle
        {
            get
            {
                return _miscellaneousMaterialCostTitle;
            }

            set
            {
                if (value != _miscellaneousMaterialCostTitle)
                {
                    _miscellaneousMaterialCostTitle = value;
                    OnPropertyChanged(nameof(MiscellaneousMaterialCostTitle));
                }
            }
        }

        bool _miscellaneousMaterialCostIsEnable = true;
        public bool MiscellaneousMaterialCostIsEnable
        {
            get
            {
                return _miscellaneousMaterialCostIsEnable;
            }

            set
            {
                if (value != _miscellaneousMaterialCostIsEnable)
                {
                    _miscellaneousMaterialCostIsEnable = value;
                    OnPropertyChanged(nameof(MiscellaneousMaterialCostIsEnable));
                }
            }
        }


        #endregion


        #region Dynamic Field Properties






        // Asset System
        string _assetSystemText;
        public string AssetSystemText
        {
            get
            {
                return _assetSystemText;
            }

            set
            {
                if (value != _assetSystemText)
                {
                    _assetSystemText = value;
                    OnPropertyChanged(nameof(AssetSystemText));
                }
            }
        }

        int? _vendorID;
        public int? VendorID
        {
            get
            {
                return _vendorID;
            }

            set
            {
                if (value != _vendorID)
                {
                    _vendorID = value;
                    OnPropertyChanged(nameof(VendorID));
                }
            }
        }
        string _vendorText;
        public string VendorText
        {
            get
            {
                return _vendorText;
            }

            set
            {
                if (value != _vendorText)
                {
                    _vendorText = value;
                    OnPropertyChanged(nameof(VendorText));
                }
            }
        }
        int? _runtimeUnitID;
        public int? RuntimeUnitID
        {
            get
            {
                return _runtimeUnitID;
            }

            set
            {
                if (value != _runtimeUnitID)
                {
                    _runtimeUnitID = value;
                    OnPropertyChanged(nameof(RuntimeUnitID));
                }
            }
        }
        string _runtimeUnitText;
        public string RuntimeUnitText
        {
            get
            {
                return _runtimeUnitText;
            }

            set
            {
                if (value != _runtimeUnitText)
                {
                    _runtimeUnitText = value;
                    OnPropertyChanged(nameof(RuntimeUnitText));
                }
            }
        }
        string _assetTagText;
        public string AssetTagText
        {
            get
            {
                return _assetTagText;
            }

            set
            {
                if (value != _assetTagText)
                {
                    _assetTagText = value;
                    OnPropertyChanged(nameof(AssetTagText));
                }
            }
        }




        string _capacityText;
        public string CapacityText
        {
            get
            {
                return _capacityText;
            }

            set
            {
                if (value != _capacityText)
                {
                    _capacityText = value;
                    OnPropertyChanged(nameof(CapacityText));
                }
            }
        }




        string _categoryText;
        public string CategoryText
        {
            get
            {
                return _categoryText;
            }

            set
            {
                if (value != _categoryText)
                {
                    _categoryText = value;
                    OnPropertyChanged(nameof(CategoryText));
                }
            }
        }
        int? _categoryID;
        public int? CategoryID
        {
            get
            {
                return _categoryID;
            }

            set
            {
                if (value != _categoryID)
                {
                    _categoryID = value;
                    OnPropertyChanged(nameof(CategoryID));
                }
            }
        }


        string _currentRuntimeText;
        public string CurrentRuntimeText
        {
            get
            {
                return _currentRuntimeText;
            }

            set
            {
                if (value != _currentRuntimeText)
                {
                    _currentRuntimeText = value;
                    OnPropertyChanged(nameof(CurrentRuntimeText));
                }
            }
        }


        string _dailyRuntime;
        public string DailyRuntime
        {
            get
            {
                return _dailyRuntime;
            }

            set
            {
                if (value != _dailyRuntime)
                {
                    _dailyRuntime = value;
                    OnPropertyChanged(nameof(DailyRuntime));
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
                    OnPropertyChanged(nameof(Description));
                }
            }
        }



        string _detailedLocation;
        public string DetailedLocation
        {
            get
            {
                return _detailedLocation;
            }

            set
            {
                if (value != _detailedLocation)
                {
                    _detailedLocation = value;
                    OnPropertyChanged(nameof(DetailedLocation));
                }
            }
        }



        string _installationDate;
        public string InstallationDate
        {
            get
            {
                return _installationDate;
            }

            set
            {
                if (value != _installationDate)
                {
                    _installationDate = value;
                    OnPropertyChanged(nameof(InstallationDate));
                }
            }
        }


        string _criticalAsset;
        public string CriticalAsset
        {
            get
            {
                return _criticalAsset;
            }

            set
            {
                if (value != _criticalAsset)
                {
                    _criticalAsset = value;
                    OnPropertyChanged(nameof(CriticalAsset));
                }
            }
        }



        string _machineClassification;
        public string MachineClassification
        {
            get
            {
                return _machineClassification;
            }

            set
            {
                if (value != _machineClassification)
                {
                    _machineClassification = value;
                    OnPropertyChanged(nameof(MachineClassification));
                }
            }
        }


        string _manufacturer;
        public string Manufacturer
        {
            get
            {
                return _manufacturer;
            }

            set
            {
                if (value != _manufacturer)
                {
                    _manufacturer = value;
                    OnPropertyChanged(nameof(Manufacturer));
                }
            }
        }


        string _model;
        public string Model
        {
            get
            {
                return _model;
            }

            set
            {
                if (value != _model)
                {
                    _model = value;
                    OnPropertyChanged(nameof(Model));
                }
            }
        }


        string _originalCost;
        public string OriginalCost
        {
            get
            {
                return _originalCost;
            }

            set
            {
                if (value != _originalCost)
                {
                    _originalCost = value;
                    OnPropertyChanged(nameof(OriginalCost));
                }
            }
        }



        string _profileImage;
        public string ProfileImage
        {
            get
            {
                return _profileImage;
            }

            set
            {
                if (value != _profileImage)
                {
                    _profileImage = value;
                    OnPropertyChanged(nameof(ProfileImage));
                }
            }
        }


        string _rating;
        public string Rating
        {
            get
            {
                return _rating;
            }

            set
            {
                if (value != _rating)
                {
                    _rating = value;
                    OnPropertyChanged(nameof(Rating));
                }
            }
        }



        string _runtimeUnits;
        public string RuntimeUnits
        {
            get
            {
                return _runtimeUnits;
            }

            set
            {
                if (value != _runtimeUnits)
                {
                    _runtimeUnits = value;
                    OnPropertyChanged(nameof(RuntimeUnits));
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
                    OnPropertyChanged(nameof(SerialNumber));
                }
            }
        }


        string _vendor;
        public string Vendor
        {
            get
            {
                return _vendor;
            }

            set
            {
                if (value != _vendor)
                {
                    _vendor = value;
                    OnPropertyChanged(nameof(Vendor));
                }
            }
        }


     
        string _warrantyDate;
        public string WarrantyDate
        {
            get
            {
                return _warrantyDate;
            }

            set
            {
                if (value != _warrantyDate)
                {
                    _warrantyDate = value;
                    OnPropertyChanged(nameof(WarrantyDate));
                }
            }
        }


        string _weight;
        public string Weight
        {
            get
            {
                return _weight;
            }

            set
            {
                if (value != _weight)
                {
                    _weight = value;
                    OnPropertyChanged(nameof(Weight));
                }
            }
        }
        string _lotoUrl;
        public string LotoUrl
        {
            get
            {
                return _lotoUrl;
            }

            set
            {
                if (value != _lotoUrl)
                {
                    _lotoUrl = value;
                    OnPropertyChanged(nameof(LotoUrl));
                }
            }
        }








        #region User Fields Section


        //UserField1
        string _userField1;
        public string UserField1
        {
            get
            {
                return _userField1;
            }

            set
            {
                if (value != _userField1)
                {
                    _userField1 = value;
                    OnPropertyChanged(nameof(UserField1));
                }
            }
        }

        //UserField2
        string _userField2;
        public string UserField2
        {
            get
            {
                return _userField2;
            }

            set
            {
                if (value != _userField2)
                {
                    _userField2 = value;
                    OnPropertyChanged(nameof(UserField2));
                }
            }
        }


        //UserField3
        string _userField3;
        public string UserField3
        {
            get
            {
                return _userField3;
            }

            set
            {
                if (value != _userField3)
                {
                    _userField3 = value;
                    OnPropertyChanged(nameof(UserField3));
                }
            }
        }


        //UserField4
        string _userField4;
        public string UserField4
        {
            get
            {
                return _userField4;
            }

            set
            {
                if (value != _userField4)
                {
                    _userField4 = value;
                    OnPropertyChanged(nameof(UserField4));
                }
            }
        }


        //UserField5
        string _userField5;
        public string UserField5
        {
            get
            {
                return _userField5;
            }

            set
            {
                if (value != _userField5)
                {
                    _userField5 = value;
                    OnPropertyChanged(nameof(UserField5));
                }
            }
        }


        //UserField6
        string _userField6;
        public string UserField6
        {
            get
            {
                return _userField6;
            }

            set
            {
                if (value != _userField6)
                {
                    _userField6 = value;
                    OnPropertyChanged(nameof(UserField6));
                }
            }
        }


        //UserField7
        string _userField7;
        public string UserField7
        {
            get
            {
                return _userField7;
            }

            set
            {
                if (value != _userField7)
                {
                    _userField7 = value;
                    OnPropertyChanged(nameof(UserField7));
                }
            }
        }


        //UserField8
        string _userField8;
        public string UserField8
        {
            get
            {
                return _userField8;
            }

            set
            {
                if (value != _userField8)
                {
                    _userField8 = value;
                    OnPropertyChanged(nameof(UserField8));
                }
            }
        }


        //UserField9
        string _userField9;
        public string UserField9
        {
            get
            {
                return _userField9;
            }

            set
            {
                if (value != _userField9)
                {
                    _userField9 = value;
                    OnPropertyChanged(nameof(UserField9));
                }
            }
        }


        //UserField10
        string _userField10;
        public string UserField10
        {
            get
            {
                return _userField10;
            }

            set
            {
                if (value != _userField10)
                {
                    _userField10 = value;
                    OnPropertyChanged(nameof(UserField10));
                }
            }
        }


        //UserField11
        string _userField11;
        public string UserField11
        {
            get
            {
                return _userField11;
            }

            set
            {
                if (value != _userField11)
                {
                    _userField11 = value;
                    OnPropertyChanged(nameof(UserField11));
                }
            }
        }


        //UserField12
        string _userField12;
        public string UserField12
        {
            get
            {
                return _userField12;
            }

            set
            {
                if (value != _userField12)
                {
                    _userField12 = value;
                    OnPropertyChanged(nameof(UserField12));
                }
            }
        }


        //UserField13
        string _userField13;
        public string UserField13
        {
            get
            {
                return _userField13;
            }

            set
            {
                if (value != _userField13)
                {
                    _userField13 = value;
                    OnPropertyChanged(nameof(UserField13));
                }
            }
        }


        //UserField14
        string _userField14;
        public string UserField14
        {
            get
            {
                return _userField14;
            }

            set
            {
                if (value != _userField14)
                {
                    _userField14 = value;
                    OnPropertyChanged(nameof(UserField14));
                }
            }
        }

        //UserField15
        string _userField15;
        public string UserField15
        {
            get
            {
                return _userField15;
            }

            set
            {
                if (value != _userField15)
                {
                    _userField15 = value;
                    OnPropertyChanged(nameof(UserField15));
                }
            }
        }

        //UserField16
        string _userField16;
        public string UserField16
        {
            get
            {
                return _userField16;
            }

            set
            {
                if (value != _userField16)
                {
                    _userField16 = value;
                    OnPropertyChanged(nameof(UserField16));
                }
            }
        }


        //UserField17
        string _userField17;
        public string UserField17
        {
            get
            {
                return _userField17;
            }

            set
            {
                if (value != _userField17)
                {
                    _userField17 = value;
                    OnPropertyChanged(nameof(UserField17));
                }
            }
        }

        //UserField18
        string _userField18;
        public string UserField18
        {
            get
            {
                return _userField18;
            }

            set
            {
                if (value != _userField18)
                {
                    _userField18 = value;
                    OnPropertyChanged(nameof(UserField18));
                }
            }
        }


        //UserField19
        string _userField19;
        public string UserField19
        {
            get
            {
                return _userField19;
            }

            set
            {
                if (value != _userField19)
                {
                    _userField19 = value;
                    OnPropertyChanged(nameof(UserField19));
                }
            }
        }


        //UserField20
        string _userField20;
        public string UserField20
        {
            get
            {
                return _userField20;
            }

            set
            {
                if (value != _userField20)
                {
                    _userField20 = value;
                    OnPropertyChanged(nameof(UserField20));
                }
            }
        }


        //UserField21
        string _userField21;
        public string UserField21
        {
            get
            {
                return _userField21;
            }

            set
            {
                if (value != _userField21)
                {
                    _userField21 = value;
                    OnPropertyChanged(nameof(UserField21));
                }
            }
        }


        //UserField22
        string _userField22;
        public string UserField22
        {
            get
            {
                return _userField22;
            }

            set
            {
                if (value != _userField22)
                {
                    _userField22 = value;
                    OnPropertyChanged(nameof(UserField22));
                }
            }
        }


        //UserField23
        string _userField23;
        public string UserField23
        {
            get
            {
                return _userField23;
            }

            set
            {
                if (value != _userField23)
                {
                    _userField23 = value;
                    OnPropertyChanged(nameof(UserField23));
                }
            }
        }


        //UserField24
        string _userField24;
        public string UserField24
        {
            get
            {
                return _userField24;
            }

            set
            {
                if (value != _userField24)
                {
                    _userField24 = value;
                    OnPropertyChanged(nameof(UserField24));
                }
            }
        }

        #endregion


        #endregion

        #endregion



        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);


        public ICommand FacilityCommand => new AsyncCommand(ShowFacilities);
        public ICommand LocationCommand => new AsyncCommand(ShowLocations);

        public ICommand CategoryCommand => new AsyncCommand(ShowCategory);
        public ICommand AssetSystemCommand => new AsyncCommand(ShowAssetSystem);
        public ICommand VendorCommand => new AsyncCommand(ShowVendor);
        public ICommand RuntimeUnitCommand => new AsyncCommand(ShowRuntimeUnits);



        //Save Command
        public ICommand SaveAssetCommand => new AsyncCommand(CreateAsset);

        public ICommand TapCommand => new AsyncCommand(SpeechtoText);

        #endregion

        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                //OperationInProgress = true;

                //if (ConnectivityService.IsConnected == false)
                //{
                //    UserDialogs.Instance.HideLoading();
                //    await DialogService.ShowAlertAsync("internet not available", "Alert", "OK");
                //    return;

                //}

                if (navigationData != null)
                {

                    var navigationParams = navigationData as TargetNavigationData;
                    this.AssetNumberText = navigationParams.SearchText;
                }

                var facilityResponse = await _facilityService.GetFacilities(UserID, "1", "10", "null");
                if (facilityResponse != null && facilityResponse.targetWrapper != null && facilityResponse.targetWrapper.facilities != null)
                {
                    int facilities = facilityResponse.targetWrapper.facilities.Count();
                    if (facilities == 1)
                    {
                        foreach (var item in facilityResponse.targetWrapper.facilities)
                        {
                            this.FacilityID = item.FacilityID;
                            this.FacilityName = item.FacilityName;
                        }

                    }

                }

                await SetTitlesPropertiesForPage();
                //FormControlsAndRights = await _formLoadInputService.GetFormControlsAndRights(UserID, AppSettings.AssetModuleName);



             
                if (Application.Current.Properties.ContainsKey("AssetAdditionalDetailsTabKey"))
                {
                    var Addtionaldetails = Application.Current.Properties["AssetAdditionalDetailsTabKey"].ToString();
                    if (Addtionaldetails == "E")
                    {
                        this.AdditionalDetailsIsEnable = true;
                    }
                    else if (Addtionaldetails == "V")
                    {
                        this.AdditionalDetailsIsEnable = false;
                    }
                    else
                    {
                        this.AdditionalDetailsIsVisible = false;
                    }
                }
                if (Application.Current.Properties.ContainsKey("AssetLocationDetailsTabKey"))
                {
                    var Locationdetails = Application.Current.Properties["AssetLocationDetailsTabKey"].ToString();

                    if (Locationdetails != null && Locationdetails == "E")
                    {
                        this.LocationIsVisible = true;

                    }
                    else if (Locationdetails == "V")
                    {
                        this.LocationIsEnable = false;
                        this.FacilityIsEnable = false;
                    }
                    else
                    {
                        LocationIsVisible = false;
                        // FacilityIsVisible = false;
                    }
                }


                ServiceOutput FormControlsAndRightsForDetails = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "Assets", "Details");
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


                                AssetControlsNew = AssetSubModule.listControls;


                            }



                        }
                    }
                }
                if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic"))
                {
                    this.LocationForLicensingIsVisible = false;


                }


                
                await CreateControlsForPage();
            }


            catch (Exception ex)
            {
                // OperationInProgress = false;
                UserDialogs.Instance.HideLoading();

            }

            finally
            {
                // OperationInProgress = false;
                UserDialogs.Instance.HideLoading();
            }
        }

        public CreateNewAssetPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IAssetModuleService assetService, IWorkorderService workorderService, IFacilityService facilityService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _assetService = assetService;
            _workorderService = workorderService;
            _facilityService = facilityService;
        }

        public async Task SetTitlesPropertiesForPage()
        {
            try
            {


                PageTitle = WebControlTitle.GetTargetNameByTitleName("CreateNewAsset");
                WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                //AssetNameTitle = WebControlTitle.GetTargetNameByTitleName("AssetName");
                //AssetNumberTitle = WebControlTitle.GetTargetNameByTitleName("AssetNumber");
                FacilityTitle = WebControlTitle.GetTargetNameByTitleName("Facility");
                LocationTitle = WebControlTitle.GetTargetNameByTitleName("Location");
                //AssetSystemTitle = WebControlTitle.GetTargetNameByTitleName("AssetSystem");
                //CategoryTitle = WebControlTitle.GetTargetNameByTitleName("Category");
                //RuntimeUnitTitle = WebControlTitle.GetTargetNameByTitleName("RuntimeUnits");
                //VendorTitle = WebControlTitle.GetTargetNameByTitleName("Vendor");
                AdditionalDetailsTitle = WebControlTitle.GetTargetNameByTitleName("Notes");
                SaveTitle = WebControlTitle.GetTargetNameByTitleName("Save");
                SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");


            }
            catch (Exception ex)
            {


            }

            finally
            {

            }
        }

        public async Task CreateControlsForPage()
        {


            ///Seperate the static controls so they don't create twice and we have to keep it
            ///some place so we set its visibility and required field also.
            ///

            #region Extract Details control


            //if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
            //{
            //    var AssetsModule = FormControlsAndRights.lstModules[0];
            //    if (AssetsModule.ModuleName == "Assets") //ModuleName can't be  changed in service 
            //    {
            //        if (AssetsModule.lstSubModules != null && AssetsModule.lstSubModules.Count > 0)
            //        {

            //            var AssetSubModule = AssetsModule.lstSubModules.FirstOrDefault(i => i.SubModuleName == "Assets");

            //            if (AssetSubModule != null)
            //            {
            //                if (AssetSubModule.listControls != null && AssetSubModule.listControls.Count > 0)
            //                {
            //                    //var CloseWorkorder = workorderSubModule.listControls.FirstOrDefault(i => i.ControlName == "Close");
            //                    //var EditWorkorder = workorderSubModule.listControls.FirstOrDefault(i => i.ControlName == "Edit");
            //                    //var NewWorkorder = workorderSubModule.listControls.FirstOrDefault(i => i.ControlName == "New");
            //                    //var ViewWorkorder = workorderSubModule.listControls.FirstOrDefault(i => i.ControlName == "View");
            //                }

            //                if (AssetSubModule.listDialoges != null && AssetSubModule.listDialoges.Count > 0)
            //                {
            //                    var AssetDialog = AssetSubModule.listDialoges.FirstOrDefault(i => i.DialogName == "AssetDialog");
            //                    if (AssetDialog != null && AssetDialog.listTab != null && AssetDialog.listTab.Count > 0)
            //                    {
            //                        var DetailsTab = AssetDialog.listTab.FirstOrDefault(i => i.DialogTabName == "Details");

            //                        if (DetailsTab != null && DetailsTab.listControls != null && DetailsTab.listControls.Count > 0)
            //                        {
            //                            AssetControlsNew = DetailsTab.listControls;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            #endregion
            #region Apply Control Typewise ordering.

            if (AssetControlsNew != null && AssetControlsNew.Count > 0)
            {

                //    #region Logic 1

                var sortedList = new List<FormControl>();

                foreach (var item in AssetControlsNew)
                {
                    if (item.DisplayFormat == "DateTime")
                    {
                        sortedList.Add(item);
                    }

                }
                //Add Except DateTime
                foreach (var item in AssetControlsNew)
                {
                    if (item.DisplayFormat != "DateTime")
                    {
                        sortedList.Add(item);
                        
                    }

                }

                //    //ReAssign to WorkorderControlsNew
                AssetControlsNew = sortedList;
                //    #endregion




            }
            #endregion

            #region Remove Overridden controls from titles New

            if (AssetControlsNew != null && AssetControlsNew.Count > 0)
            {

                try
                {
                    ///AssetName
                    ///AssetNumber
                    ///AssetSystem
                    ///Category
                    ///Vendor
                    ///RuntimeUnit





                    var assetName = AssetControlsNew.FirstOrDefault(x => x.ControlName == "AssetName");
                    if (assetName != null)
                    {
                        AssetNameTitle = assetName.TargetName;
                        OverriddenControlsNew.Add(assetName);
                        AssetControlsNew.Remove(assetName);
                    }

                    var assetNumber = AssetControlsNew.FirstOrDefault(x => x.ControlName == "AssetNumber");
                    if (assetNumber != null)
                    {
                        AssetNumberTitle = assetNumber.TargetName;
                        OverriddenControlsNew.Add(assetNumber);
                        AssetControlsNew.Remove(assetNumber);
                    }

                    var assetSystem = AssetControlsNew.FirstOrDefault(x => x.ControlName == "AssetSystemID");
                    if (assetSystem != null)
                    {
                        AssetSystemTitle= assetSystem.TargetName;
                        OverriddenControlsNew.Add(assetSystem);
                        AssetControlsNew.Remove(assetSystem);
                    }

                    var category = AssetControlsNew.FirstOrDefault(x => x.ControlName == "CategoryID");
                    if (category != null)
                    {
                        CategoryTitle = category.TargetName;
                        OverriddenControlsNew.Add(category);
                        AssetControlsNew.Remove(category);
                    }

                    var vendor = AssetControlsNew.FirstOrDefault(x => x.ControlName == "VendorID");
                    if (vendor != null)
                    {
                        VendorTitle = vendor.TargetName;
                        OverriddenControlsNew.Add(vendor);
                        AssetControlsNew.Remove(vendor);
                    }

                    var runtimeUnit = AssetControlsNew.FirstOrDefault(x => x.ControlName == "RuntimeUnits");
                    if (runtimeUnit != null)
                    {
                        RuntimeUnitTitle = runtimeUnit.TargetName;
                        OverriddenControlsNew.Add(runtimeUnit);
                        AssetControlsNew.Remove(runtimeUnit);
                    }


                }
                catch (Exception ex)
                {


                }

            }

            #endregion
            #region Remove None visibility controls
            if (AssetControlsNew != null && AssetControlsNew.Count > 0)
            {
                AssetControlsNew.RemoveAll(i => i.Expression == "N");
                AssetControlsNew.RemoveAll(i => i.ControlName == "MachineClassification");
                AssetControlsNew.RemoveAll(i => i.ControlName == "AssetName");
                AssetControlsNew.RemoveAll(i => i.ControlName == "AssetNumber");
                AssetControlsNew.RemoveAll(i => i.ControlName == "AssetSystemID");
                AssetControlsNew.RemoveAll(i => i.ControlName == "CategoryID");
                AssetControlsNew.RemoveAll(i => i.ControlName == "VendorID");
                AssetControlsNew.RemoveAll(i => i.ControlName == "RuntimeUnits");
            }

            #endregion


            #region Apply visibility according to expression on Overridden controls New

            if (OverriddenControlsNew != null && OverriddenControlsNew.Count > 0)
            {

                try
                {
                    ///AssetName
                    ///AssetNumber
                    ///AssetSystem
                    ///Category
                    ///Vendor
                    ///RuntimeUnit

                    //var formRoles = titles.lstRoles;

                    foreach (var item in OverriddenControlsNew)
                    {
                        //var finalizedRole = await ParseControlRoleExpressionWithFormsRoles(item.Expression, formRoles);

                        switch (item.ControlName)
                        {

                          


                            case "AssetName":
                                {
                                    AssetIsEnable = ApplyIsEnable(item.Expression);
                                    break;
                                }


                            case "AssetNumber":
                                {
                                    AssetNumberIsEnable = ApplyIsEnable(item.Expression);
                                    break;
                                }



                            case "AssetSystemID":
                                {
                                    if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic") || AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Professional"))
                                    {
                                        AssetSystemIsVisible = false;
                                        break;
                                    }
                                    AssetSystemIsEnable = ApplyIsEnable(item.Expression);
                                    AssetSystemIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }


                            case "CategoryID":
                                {
                                    CategoryIsEnable = ApplyIsEnable(item.Expression);
                                    CategoryIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }

                            case "VendorID":
                                {
                                    if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic"))
                                    {
                                        this.VendorIsVisible = false;
                                        break;

                                    }
                                    VendorIsEnable = ApplyIsEnable(item.Expression);
                                    VendorIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }

                            case "RuntimeUnits":
                                {
                                    RuntimeUnitCommandIsEnable = ApplyIsEnable(item.Expression);
                                    RuntimeUnitIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }



                        }

                    }

                }
                catch (Exception ex)
                {


                }

            }

            #endregion

            #region Generate and Bind Dyanmic controls New
            if (AssetControlsNew != null && AssetControlsNew.Count > 0)
            {

                #region Test 3



                Grid contentGrid = await GetContentGrid();
                contentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                //contentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                //contentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });



                int rowCount = 0;
                int columnCount = 0;
                bool isItemAddedInFirstColumn = false;
                bool isItemAddedInSecondColumn = false;

                foreach (var item in AssetControlsNew)
                {

                    switch (item.DisplayFormat)
                    {

                        case "ComboBox":
                            if (!isItemAddedInFirstColumn)
                            {
                                if (!isItemAddedInSecondColumn)
                                {
                                    GenerateComboBoxLayout(item, contentGrid, rowCount, columnCount);

                                    //increment column
                                    isItemAddedInFirstColumn = true;
                                    isItemAddedInSecondColumn = false;
                                    columnCount = 1;
                                }
                                else
                                {
                                    //generate new row
                                    contentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                    rowCount++;
                                    GenerateComboBoxLayout(item, contentGrid, rowCount, columnCount);

                                    columnCount = 1;
                                    isItemAddedInFirstColumn = true;
                                    isItemAddedInSecondColumn = false;

                                }
                            }

                            else
                            {
                                GenerateComboBoxLayout(item, contentGrid, rowCount, columnCount);

                                //increment column
                                isItemAddedInFirstColumn = false;
                                isItemAddedInSecondColumn = true;
                                columnCount = 0;
                            }


                            break;

                        case "DateTime":
                            if (!isItemAddedInFirstColumn)
                            {
                                if (!isItemAddedInSecondColumn)
                                {
                                    GenerateDateTimeLayout(item, contentGrid, rowCount, columnCount);

                                    //increment column
                                    isItemAddedInFirstColumn = true;
                                    isItemAddedInSecondColumn = false;
                                    columnCount = 1;
                                }
                                else
                                {
                                    //generate new row
                                    contentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                    rowCount++;
                                    GenerateDateTimeLayout(item, contentGrid, rowCount, columnCount);

                                    columnCount = 1;
                                    isItemAddedInFirstColumn = true;
                                    isItemAddedInSecondColumn = false;

                                }
                            }

                            else
                            {
                                GenerateDateTimeLayout(item, contentGrid, rowCount, columnCount);

                                //increment column
                                isItemAddedInFirstColumn = false;
                                isItemAddedInSecondColumn = true;
                                columnCount = 0;
                            }




                            break;

                        case "TextBox":
                            if (!isItemAddedInFirstColumn)
                            {
                                if (!isItemAddedInSecondColumn)
                                {
                                    GenerateTextBoxLayout(item, contentGrid, rowCount, columnCount);

                                    //increment column
                                    isItemAddedInFirstColumn = true;
                                    isItemAddedInSecondColumn = false;
                                    columnCount = 1;
                                }
                                else
                                {
                                    //generate new row
                                    contentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                    rowCount++;
                                    GenerateTextBoxLayout(item, contentGrid, rowCount, columnCount);

                                    columnCount = 1;
                                    isItemAddedInFirstColumn = true;
                                    isItemAddedInSecondColumn = false;

                                }
                            }

                            else
                            {
                                GenerateTextBoxLayout(item, contentGrid, rowCount, columnCount);

                                //increment column
                                isItemAddedInFirstColumn = false;
                                isItemAddedInSecondColumn = true;
                                columnCount = 0;
                            }


                            break;


                    }


                }



                #endregion







            }
            #endregion


        }


        private bool ApplyIsEnable(String Expression)
        {
            if (Expression == "E")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        private bool ApplyIsVisible(String Expression)
        {
            if (Expression == "N")
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        private void GenerateTextBoxLayout(FormControl formControl, Grid contentGrid, int row, int column)
        {
            var title = new Label();
            var control = new MyEntry();
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                title.FontAttributes = FontAttributes.Bold;
            }
            title.TextColor = Color.Black;

            title.Text = formControl.TargetName;// + "<<>>" + formControl.ControlName;
            SetControlBindingAccordingToControlType(control, formControl);

            if (formControl.Expression == "E")
            {
                control.IsEnabled = true;
            }
            else
            {
                control.IsEnabled = false;

            }

            var wrapperLayout = new StackLayout() { Orientation = StackOrientation.Vertical };
            wrapperLayout.Children.Add(title);
            wrapperLayout.Children.Add(control);

            contentGrid.Children.Add(wrapperLayout, column, row);
        }

        private void GenerateComboBoxLayout(FormControl formControl, Grid contentGrid, int row, int column)
        {
            var title = new Label();
            //var control = new Picker();
            var control = new CustomPicker();
            control.Image = "unnamed";
            control.HeightRequest = 45;
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                title.FontAttributes = FontAttributes.Bold;
            }
            title.TextColor = Color.Black;

            title.Text = formControl.TargetName;// + "<<>>" + formControl.ControlName;

            List<ComboDD> NEWCOMBO = new List<ComboDD>();
            ComboDD abc = new ComboDD();
            abc.SelectedText = WebControlTitle.GetTargetNameByTitleName("Select");
            abc.SelectedValue = 0;
            NEWCOMBO.Insert(0, abc);
            foreach (var item in formControl.listCombo)
            {
                NEWCOMBO.Add(item);

            }
            control.ItemsSource = NEWCOMBO;
            control.ItemDisplayBinding = new Binding(nameof(ComboDD.SelectedText));


            SetControlBindingAccordingToControlType(control, formControl);
            /// bind with its value associative property.
            /// 

            //control.SetBinding(Picker.SelectedItemProperty);

            if (formControl.Expression == "E")
            {
                control.IsEnabled = true;
            }
            else
            {
                control.IsEnabled = false;

            }

            var wrapperLayout = new StackLayout() { Orientation = StackOrientation.Vertical };
            wrapperLayout.Children.Add(title);
            wrapperLayout.Children.Add(control);

            contentGrid.Children.Add(wrapperLayout, column, row);

        }

        private void GenerateDateTimeLayout(FormControl formControl, Grid contentGrid, int row, int column)
        {
            var title = new Label();
            if(Device.RuntimePlatform==Device.iOS|| Device.RuntimePlatform == Device.Android)
            {
                title.FontAttributes = FontAttributes.Bold;
            }
            title.TextColor = Color.Black;
            View control;
            var Boder = new SfBorder { CornerRadius = 5, BorderColor = Color.Black };
            Boder.Content = new CustomDatePicker1 { Padding = new Thickness(0, 3, 0, 0) };
            control = Boder;
            
            //if (formControl.IsRequired ?? false)
            //{
            //    control = new DatePicker();
            //}
            //else
            //{
            //    control = new CustomDatePicker1();
            //}

            SetControlBindingAccordingToControlType(control, formControl);
            //new CustomDatePicker1(); //new DatePicker();

            title.Text = formControl.TargetName;// + "<<>>" + formControl.ControlName;

            if (formControl.Expression == "E")
            {
                control.IsEnabled = true;
            }
            else
            {
                control.IsEnabled = false;

            }

            var wrapperLayout = new StackLayout() { Orientation = StackOrientation.Vertical };
            wrapperLayout.Children.Add(title);
            wrapperLayout.Children.Add(control);

            contentGrid.Children.Add(wrapperLayout, column, row);
        }



        private void SetControlBindingAccordingToControlType(View control, FormControl formControl)
        {
            switch (formControl.ControlName)
            {




                case "AssetTag":
                    {
                        if (control is Picker)
                        {
                            if (control is SfBorder)
                            {
                                var data = control as SfBorder;
                                control = data.Content as CustomDatePicker1;
                            }

                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.AmStepID));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(AssetTagText)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                AssetTagText = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;
                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.AssetTagText));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            AssetTagText = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.AssetTagText), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.AssetTagText), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "Capacity":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.AnalysisPerformedDate));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(CapacityText)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                CapacityText = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;


                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.CapacityText));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            CapacityText = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.CapacityText), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.CapacityText), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "Category":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.ConfirmEmail));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(CategoryText)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                CategoryText = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;
                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.CategoryText));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            CategoryText = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.CategoryText), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.CategoryText), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "CurrentRuntime":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.CountermeasuresDefinedDate));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(CurrentRuntimeText)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                CurrentRuntimeText = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.CurrentRuntimeText));
                            var x = control as Entry;
                            x.TextChanged += Entry_TextChangedCurrentRuntime;
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            CurrentRuntimeText = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.CurrentRuntimeText), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.CurrentRuntimeText), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "DailyRuntime":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.CurrentRuntime));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(DailyRuntime)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                DailyRuntime = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;
                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.DailyRuntime));
                            var x = control as Entry;
                            x.TextChanged += Entry_TextChangedDailyRuntime;
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            DailyRuntime = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.DailyRuntime), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.DailyRuntime), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "Description":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.DiagnosticTime));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(Description)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                Description = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;
                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.Description));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            Description = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.Description), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.Description), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "DetailedLocation":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.DigitalSignatures));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(DetailedLocation)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                DetailedLocation = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.DetailedLocation));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            DetailedLocation = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.DetailedLocation), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.DetailedLocation), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }


                case "InstallationDate":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.ImplementationValidatedDate));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(InstallationDate.ToString())); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                             //   InstallationDate = item.SelectedValue;
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;
                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.InstallationDate));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                           
                            control.SetBinding(DatePicker.DateProperty, nameof(this.InstallationDate), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            if (this.InstallationDate == null || this.InstallationDate == ("1/1/0001 12:00:00 AM"))
                            {

                            }
                            else
                            {
                                InstallationDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).ToString();
                            }
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.InstallationDate), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "CriticalAsset":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.InitialWaitTime));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(CriticalAsset)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                CriticalAsset = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.CriticalAsset));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            CriticalAsset = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.CriticalAsset), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.CriticalAsset), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "MachineClassification%":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.JobNumber));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(MachineClassification)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                MachineClassification = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.MachineClassification));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            MachineClassification = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.MachineClassification), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.MachineClassification), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "Manufacturer":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.NotificationTime));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(Manufacturer)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                Manufacturer = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;
                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.Manufacturer));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            Manufacturer = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.Manufacturer), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.Manufacturer), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "Model":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.PartWaitingTime));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(Model)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                Model = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.Model));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            Model = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.Model), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.Model), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "OriginalCostID":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.ProblemID));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(OriginalCost.ToString())); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                OriginalCost = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.OriginalCost));
                            var x = control as Entry;
                            x.TextChanged += Entry_TextChangedOriginalCost;
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            OriginalCost = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.OriginalCost), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.OriginalCost), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "ProfileImage":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.Project));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(ProfileImage)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                ProfileImage = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;
                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.ProfileImage));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            ProfileImage = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.ProfileImage), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.ProfileImage), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "Rating":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.RelatedToID));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(Rating)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                Rating = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.Rating));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            Rating = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.Rating), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.Rating), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "RuntimeUnits":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.RepairingOrReplacementTime));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(RuntimeUnits)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                RuntimeUnits = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.RuntimeUnits));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            RuntimeUnits = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.RuntimeUnits), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.RuntimeUnits), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "SerialNumber":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.RequestedDate));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(SerialNumber)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                SerialNumber = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.SerialNumber));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            SerialNumber = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.SerialNumber), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.SerialNumber), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "Vendor":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.RequesterEmail));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(Vendor)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                Vendor = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.Vendor));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            Vendor = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.Vendor), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.Vendor), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "WarrantyDate":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.RequesterFullName));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(WarrantyDate.ToString())); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                              //  WarrantyDate =item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.WarrantyDate));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                           
                            control.SetBinding(DatePicker.DateProperty, nameof(this.WarrantyDate), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            if (this.WarrantyDate == null || this.WarrantyDate == ("1/1/0001 12:00:00 AM"))
                            {

                            }
                            else
                            {
                                WarrantyDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).ToString();
                            }
                          
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.WarrantyDate), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "Weight":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.RequesterPhone));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(Weight)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                Weight = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.Weight));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            Weight = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.Weight), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.Weight), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }


                case "LOTOUrl":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.RequesterPhone));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(LotoUrl)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                LotoUrl = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.LotoUrl));
                            
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            LotoUrl = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.LotoUrl), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.LotoUrl), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }


                #region User Field Section

                case "UserField1":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField1)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                UserField1 = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField1));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField1 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField1), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField1), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField2":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField2)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                UserField2 = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField2));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField2 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField2), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField2), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField3":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField3)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField3 = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField3));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField3 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField3), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField3), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField4":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField4)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField4 = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField4));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField4 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField4), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField4), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField5":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField5)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField5 = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField5));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField5 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField5), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField5), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField6":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField6)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField6 = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField6));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField6 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField6), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField6), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField7":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField7)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField7 = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField7));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField7 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField7), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField7), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField8":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField8)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField8 = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField8));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField8 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField8), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField8), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField9":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField9)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField9 = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField9));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField9 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField9), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField9), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField10":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField10)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField10 = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField10));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField10 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField10), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField10), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField11":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField11)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField11 = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField11));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField11 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField11), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField11), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField12":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField12)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField12 = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField12));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField12 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField12), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField12), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField13":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField13)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField13 = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField13));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField13 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField13), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField13), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField14":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField14)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField14 = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField14));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField14 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField14), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField14), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField15":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField15)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField15 = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField15));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField15 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField15), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField15), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField16":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField16)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField16 = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField16));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField16 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField16), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField16), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField17":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField17)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField17 = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField17));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField17 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField17), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField17), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField18":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField18)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField18 = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField18));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField18 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField18), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField18), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField19":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField19)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField19 = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField19));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField19 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField19), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField19), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField20":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField20)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                UserField20 = item.SelectedValue.ToString();
                            }


                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField20));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField20 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField20), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField20), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField21":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField21)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField21 = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField21));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField21 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField21), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField21), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }


                case "UserField22":
                    {
                        #region Old Code
                        //if (control is Picker)
                        //{
                        //    var x = control as Picker;
                        //    x.ClassId = formControl.ControlName;

                        //    var source = x.ItemsSource as List<ComboDD>;
                        //    var item = source.FirstOrDefault(s => s.SelectedText == UserField22);

                        //    if (item != null)
                        //        x.SelectedItem = item;

                        //    UserField22 = item.SelectedValue.ToString();
                        //    x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        //}

                        //else if (control is Entry)
                        //{
                        //    control.SetBinding(Entry.TextProperty, nameof(this.UserField22));
                        //}

                        //else if (control is DatePicker)
                        //{
                        //    // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                        //    UserField22 = DateTime.Now.ToString();
                        //    control.SetBinding(DatePicker.DateProperty, nameof(this.UserField22));
                        //}

                        //else if (control is CustomDatePicker1)
                        //{
                        //    control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField22));
                        //}
                        //break;

                        #endregion



                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }


                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField22)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                UserField22 = item.SelectedValue.ToString();
                            }


                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField22));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField22 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField22), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField22), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;






                    }

                case "UserField23":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;

                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField23)); }
                            catch (Exception) { }


                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField23 = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField23));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField23 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField23), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField23), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }


                case "UserField24":
                    {
                        if (control is SfBorder)
                        {
                            var data = control as SfBorder;
                            control = data.Content as CustomDatePicker1;
                        }

                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            //var item = source.FirstOrDefault(s => s.SelectedText == UserField24);

                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UserField24)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                UserField24 = item.SelectedValue.ToString();
                            }



                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UserField24));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UserField24 = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField24), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker1)
                        {
                            control.SetBinding(CustomDatePicker1.SelectedDateProperty, nameof(this.UserField24), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }



                #endregion

                default:
                    break;
            }
        }
        private void Entry_TextChangedDailyRuntime(object sender, EventArgs e)
        {
            Entry e1 = sender as Entry;
            String val = e1.Text; //Get Current Text

            if (val.Contains(" "))//If it is more than your character restriction
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(val, "^[0-9.]*$"))
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }
        }
        private void Entry_TextChangedCurrentRuntime(object sender, EventArgs e)
        {
            Entry e1 = sender as Entry;
            String val = e1.Text; //Get Current Text

            if (val.Contains(" "))//If it is more than your character restriction
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(val, "^[0-9.]*$"))
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }
        }
        private void Entry_TextChangedOriginalCost(object sender, EventArgs e)
        {
            Entry e1 = sender as Entry;
            String val = e1.Text; //Get Current Text

            if (val.Contains(" "))//If it is more than your character restriction
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(val, "^[0-9.]*$"))
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }
        }


        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {

            var picker = sender as Picker;


            switch (picker.ClassId)
            {



                #region User Field Section




                case "UserField1":
                    {

                        this.UserField1 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;

                    }

                case "UserField2":
                    {

                        this.UserField2 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;

                    }

                case "UserField3":
                    {

                        this.UserField3 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;

                    }

                case "UserField4":
                    {

                        this.UserField4 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;

                    }

                case "UserField5":
                    {

                        this.UserField5 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;

                    }

                case "UserField6":
                    {

                        this.UserField6 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;

                    }

                case "UserField7":
                    {

                        this.UserField7 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;

                    }

                case "UserField8":
                    {

                        this.UserField8 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;

                    }

                case "UserField9":
                    {

                        this.UserField9 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;

                    }

                case "UserField10":
                    {

                        this.UserField10 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;

                    }

                case "UserField11":
                    {

                        this.UserField11 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;

                    }

                case "UserField12":
                    {

                        this.UserField12 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;

                    }

                case "UserField13":
                    {

                        this.UserField13 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;

                    }

                case "UserField14":
                    {

                        this.UserField14 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;

                    }

                case "UserField15":
                    {

                        this.UserField15 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;

                    }

                case "UserField16":
                    {

                        this.UserField16 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;

                    }

                case "UserField17":
                    {

                        this.UserField17 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;

                    }

                case "UserField18":
                    {

                        this.UserField18 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;

                    }

                case "UserField19":
                    {

                        this.UserField19 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;

                    }

                case "UserField20":
                    {

                        this.UserField20 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;

                    }

                case "UserField21":
                    {

                        this.UserField21 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;

                    }


                case "UserField22":
                    {

                        this.UserField22 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;

                    }

                case "UserField23":
                    {

                        this.UserField23 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;

                    }


                case "UserField24":
                    {
                        this.UserField24 = (picker.SelectedItem as ComboDD).SelectedText.ToString();
                        break;
                    }



                #endregion






               
                default:
                    break;
            }


        }


        public async Task<Grid> GetContentGrid()
        {

            var page = this.Page as ContentPage;
            if (page != null)
            {

                var parentGrid = page.Content as Grid;
                var grid = parentGrid.Children[0] as Grid;
                var Abslut = grid.Children[0] as AbsoluteLayout;
                var scrollView = Abslut.Children[0] as ScrollView;
                var scrollViewGrid = scrollView.Content as Grid;
                var contentGrid = scrollViewGrid.Children[1] as Grid;
                return contentGrid;
            }

            return null;

        }
        public async Task ShowActions()
        {
            try
            {
               // UserDialogs.Instance.ShowLoading();
                var response = await DialogService.SelectActionAsync("", SelectTitle, CancelTitle, new ObservableCollection<string>() { LogoutTitle });

                if (response == LogoutTitle)
                {
                    await _authenticationService.LogoutAsync();
                    await NavigationService.NavigateToAsync<LoginPageViewModel>();
                    await NavigationService.RemoveBackStackAsync();
                }




            }
            catch (Exception ex)
            {
               // UserDialogs.Instance.HideLoading();
                OperationInProgress = false;
            }

            finally
            {
               // UserDialogs.Instance.HideLoading();
                OperationInProgress = false;
            }
        }


        public async Task OnViewAppearingAsync(VisualElement view)
        {
            try
            {
                //UserDialogs.Instance.ShowLoading();
               OperationInProgress = true;

                if (!IsPickerDataSubscribed)
                {
                    //Retrive Facility
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.FacilityRequested, OnFacilityRequested);

                    //Retrive Location
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.LocationRequested, OnLocationRequested);


                    //Retrive Asset System
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.AssetSyastemRequested, OnAssetSystemRequested);


                    //Retrive Category
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.OnCategoryRequested, OnCategoryRequested);

                    //Retrive Vendor
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.OnVendorRequested, OnVendorRequested);


                    //Retrive RuntimeUnit
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.OnRuntimeUnitRequested, OnRuntimeUnitRequested);




                    IsPickerDataSubscribed = true;
                }

                else if (IsPickerDataRequested)
                {

                    IsPickerDataRequested = false;
                    UserDialogs.Instance.HideLoading();
                    return;
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




        #region Show Selection List Pages Methods
        public async Task ShowFacilities()
        {

            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

               // OperationInProgress = true;
                IsPickerDataRequested = true;
                await NavigationService.NavigateToAsync<FacilityListSelectionPageViewModel>(); //Pass the control here
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

        public async Task ShowLocations()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                //OperationInProgress = true;
                IsPickerDataRequested = true;
                if (FacilityID == null)
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleaseselectthefacilityfirst"), 2000);
                    return;

                }

                await NavigationService.NavigateToAsync<LocationListSelectionPageViewModel>(FacilityID);
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

        public async Task ShowAssetSystem()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                //OperationInProgress = true;
                IsPickerDataRequested = true;
                await NavigationService.NavigateToAsync<AssetSystemListSelectionPageViewModel>(new TargetNavigationData() { FacilityID = 0, LocationID = 0 }); //Pass the control here
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


        public async Task ShowCategory()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                //OperationInProgress = true;
                IsPickerDataRequested = true;
                await NavigationService.NavigateToAsync<CategoryListSelectionPageViewModel>(new TargetNavigationData() { FacilityID = this.FacilityID, LocationID = this.LocationID }); //Pass the control here
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

        public async Task ShowVendor()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                //OperationInProgress = true;
                IsPickerDataRequested = true;
                await NavigationService.NavigateToAsync<VendorListSelectionPageViewModel>(new TargetNavigationData() { FacilityID = this.FacilityID, LocationID = this.LocationID }); //Pass the control here
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


        public async Task ShowRuntimeUnits()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                // OperationInProgress = true;
                IsPickerDataRequested = true;
                await NavigationService.NavigateToAsync<RuntimeUnitListSelectionPageViewModel>(new TargetNavigationData() { FacilityID = this.FacilityID, LocationID = this.LocationID }); //Pass the control here
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
        #endregion


        #region MessagingService Callback Methods
        private void OnFacilityRequested(object obj)
        {

            if (obj != null)
            {
                var facility = obj as Facility;
                this.FacilityID = facility.FacilityID;
                this.FacilityName = ShortString.shorten(facility.FacilityName);


                // if Facility is selected reset the location, Asset and Asset System
                ResetLocation();
              
                //ResetAssetSystem();

            }


        }

        private void OnLocationRequested(object obj)
        {

            if (obj != null)
            {
                var location = obj as TLocation;
                this.LocationID = location.LocationID;
                this.LocationName = ShortString.shorten(location.LocationName);


                // if Location is selected reset the  Asset and Asset System
               
                //ResetAssetSystem();

            }


        }



        private void OnAssetSystemRequested(object obj)
        {

            if (obj != null)
            {

                var assetSystem = obj as TAssetSystem;
                this.AssetSystemID = assetSystem.AssetSystemID;
                this.AssetSystemName = ShortString.shorten(assetSystem.AssetSystemName);

                // if AssetSystem is selected reset the   Asset
               
            }


        }

        private void OnCategoryRequested(object obj)
        {

            if (obj != null)
            {

                var assignTo = obj as ComboDD;
                this.CategoryID = assignTo.SelectedValue;
                this.CategoryText = ShortString.shorten(assignTo.SelectedText);
            }


        }

        private void OnVendorRequested(object obj)
        {

            if (obj != null)
            {

                var requester = obj as ComboDD;
                this.VendorID = requester.SelectedValue;
                this.VendorText = ShortString.shorten(requester.SelectedText);
            }


        }


        private void OnRuntimeUnitRequested(object obj)
        {

            if (obj != null)
            {

                var costCenter = obj as ComboDD;
                this.RuntimeUnitID = costCenter.SelectedValue;
                this.RuntimeUnitText = ShortString.shorten(costCenter.SelectedText);
            }


        }



        #endregion


        #region Reset Picker Methods

        private void ResetLocation()
        {
            var nullLocation = new TLocation() { LocationID = null, LocationName = string.Empty };
            this.LocationID = nullLocation.LocationID;
            this.LocationName = nullLocation.LocationName;
        }

        private void ResetAsset()
        {
            var nullLocation = new TAsset() { AssetID = null, AssetName = string.Empty };
            this.AssetID = nullLocation.AssetID;
            this.AssetName = nullLocation.AssetName;
        }
        private void ResetAssetSystem()
        {
            var nullLocation = new TAssetSystem() { AssetSystemID = null, AssetSystemName = string.Empty };
            this.AssetSystemID = nullLocation.AssetSystemID;
            this.AssetSystemName = nullLocation.AssetSystemName;
        }

        #endregion


        public async Task CreateAsset()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                //OperationInProgress = true;

                //if (ConnectivityService.IsConnected == false)
                //{
                //    DialogService.ShowToast("internet not available", 2000);
                //    UserDialogs.Instance.HideLoading();

                //    return;

                //}
                if (RuntimeUnitID == 0)
                {
                    RuntimeUnitID = null;
                }
                if (!String.IsNullOrWhiteSpace(LotoUrl))
                {
                    bool IsValidEmail = Regex.IsMatch(LotoUrl,
              @"^(http|https|)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$",
              RegexOptions.IgnoreCase);

                    if (!IsValidEmail || LotoUrl.Contains(" "))
                    {
                        UserDialogs.Instance.HideLoading();

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("InvalidLOTOUrl"), 2000);
                        return;
                    }
                }
                ///TODO: Check the IsRequired Fields for OverridenControls
                ///
                var validationResultForOverriddenControls = await ValidateOverriddenControlsIsRequired(OverriddenControlsNew);
                if (validationResultForOverriddenControls.FailedItem != null)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(validationResultForOverriddenControls.ErrorMessage);
                    return;
                }

                ///TODO: Check the IsRequired Fields According to field rights
                ///on button save we will call formload and check all WorkOrderFormLoad with Its associative Property
                ///in ViewModel if any WorkOrderFormLoad is required than its Associative property can't be null.
                ///
                var validationResult = await ValidateControlsIsRequired(AssetControlsNew);
                if (validationResult.FailedItem != null)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(validationResult.ErrorMessage);
                    return;
                }

                #region Description And target validation
                //if (String.IsNullOrWhiteSpace(Description))
                //{
                //    UserDialogs.Instance.HideLoading();

                //    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Fillthedescriptionfield"), 2000);
                //    return;
                //}

                if (Application.Current.Properties.ContainsKey("AssetLocationDetailsTabKey"))
                {
                    var Locationdetails = Application.Current.Properties["AssetLocationDetailsTabKey"].ToString();

                    if (Locationdetails != null && Locationdetails == "N")
                    {
                        FacilityID = null;

                    }                 
                }

                if (FacilityID == null)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Selectthefacilityfield"), 2000);
                    return;
                }
                else if (LocationID == null)
                {
                    UserDialogs.Instance.HideLoading();
                    string LocationTitle = WebControlTitle.GetTargetNameByTitleName("Select")+" " + WebControlTitle.GetTargetNameByTitleName("Location");
                    DialogService.ShowToast(LocationTitle, 2000);
                    return;
                }

                
                #endregion
                //if (WarrantyDate < InstallationDate)
                //{
                //    UserDialogs.Instance.HideLoading();
                //    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("WarrantyDatecannotbelessthanInstallationDate"), 2000);
                //    return;

                //}
                /// Create Asset wrapper


                var asset = new asset();
                #region Asset properties initialzation
               
                
                asset.ModifiedUserName = AppSettings.User.UserName;
                asset.Description = String.IsNullOrEmpty(Description) ? null : Description.Trim();
                asset.AssetName = String.IsNullOrEmpty(AssetNameText) ? null : AssetNameText.Trim();
                asset.AdditionalDetails = AdditionalDetailsText;
                asset.AssetNumber = String.IsNullOrEmpty(AssetNumberText) ? null : AssetNumberText.Trim();
                asset.FacilityID = FacilityID;
                asset.LocationID = LocationID;
                asset.AssetSystemID = AssetSystemID;
                asset.CategoryID = CategoryID;
                asset.VendorID = VendorID;
                if (RuntimeUnitID == 0)
                {
                    RuntimeUnitID = null;
                }
                asset.RuntimeUnits = RuntimeUnitID;
                asset.AssetTag = String.IsNullOrEmpty(AssetTagText) ? null : AssetTagText.Trim();
                asset.Capacity = String.IsNullOrEmpty(CapacityText) ? null : CapacityText.Trim();
                asset.DetailedLocation = String.IsNullOrEmpty(DetailedLocation) ? null : DetailedLocation.Trim();
               
                if (InstallationDate == null)
                {
                    asset.InstallationDate = null;
                }
                else
                {
                    asset.InstallationDate = Convert.ToDateTime(InstallationDate);

                }
                asset.Manufacturer = String.IsNullOrEmpty(Manufacturer) ? null : Manufacturer.Trim();
                asset.Model = String.IsNullOrEmpty(Model) ? null : Model.Trim();
                asset.OriginalCost =Convert.ToDecimal(OriginalCost);
                asset.Rating = String.IsNullOrEmpty(Rating) ? null : Rating.Trim();
                asset.SerialNumber = String.IsNullOrEmpty(SerialNumber) ? null : SerialNumber.Trim();
                asset.Weight = String.IsNullOrEmpty(Weight) ? null : Weight.Trim();

               
                if (WarrantyDate == null)
                {
                    asset.WarrantyDate = null;
                }
                else
                {
                    asset.WarrantyDate = Convert.ToDateTime(WarrantyDate);

                }
                if (asset.WarrantyDate != null && asset.InstallationDate != null)
                {
                    if (asset.WarrantyDate.GetValueOrDefault().Date < asset.InstallationDate.GetValueOrDefault().Date)
                    {
                        UserDialogs.Instance.HideLoading();
                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("WarrantyDatecannotbelessthanInstallationDate"), 2000);
                        return;

                    }
                }
                if (string.IsNullOrWhiteSpace(CurrentRuntimeText))
                {
                    CurrentRuntimeText = "0";
                }
                if (string.IsNullOrWhiteSpace(DailyRuntime))
                {
                    DailyRuntime = "0";
                }


                asset.CurrentRuntime = decimal.Parse(CurrentRuntimeText);
                asset.DailyRuntime = decimal.Parse(DailyRuntime);
                asset.LOTOUrl = LotoUrl;

                #region User Fields

                asset.UserField1 = UserField1;
                asset.UserField2 = UserField2;
                asset.UserField3 = UserField3;
                asset.UserField4 = UserField4;
                asset.UserField5 = UserField5;
                asset.UserField6 = UserField6;
                asset.UserField7 = UserField7;
                asset.UserField8 = UserField8;
                asset.UserField9 = UserField9;
                asset.UserField10 = UserField10;
                asset.UserField11 = UserField11;
                asset.UserField12 = UserField12;
                asset.UserField13 = UserField13;
                asset.UserField14 = UserField14;
                asset.UserField15 = UserField15;
                asset.UserField16 = UserField16;
                asset.UserField17 = UserField17;
                asset.UserField18 = UserField18;
                asset.UserField19 = UserField19;
                asset.UserField20 = UserField20;
                asset.UserField21 = UserField21;
                asset.UserField22 = UserField22;
                asset.UserField23 = UserField23;
                asset.UserField24 = UserField24;
                #endregion




                #endregion
                var assetwrapper = new CreateNewAssetLong
                {
                    UserID = Convert.ToInt32(AppSettings.User.UserID),
                    ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                    asset = asset,

                };


                var response = await _assetService.CreateAsset(assetwrapper);
                if (response != null && bool.Parse(response.servicestatus))
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Assetsuccessfullycreated"), 2000);
                    await NavigationService.NavigateBackAsync();
                    


                }
                else
                {
                    if (response.servicestatusmessge == "AssetNumber  already exists")
                    {
                        UserDialogs.Instance.HideLoading();

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("AssetNumberalreadyexists"), 2000);
                        return;
                    }
                    else if (response.servicestatusmessge == "AssetTag  already exists")
                    {

                        UserDialogs.Instance.HideLoading();

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("AssetTagalreadyexists"), 2000);
                        return;
                    }
                    else if (response.servicestatusmessge == "SerialNumber  already exists")
                    {
                        UserDialogs.Instance.HideLoading();

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("SerialNumberalreadyexists"), 2000);
                        return;
                    }
                    else if (response.servicestatusmessge == "AssetTag AssetNumber SerialNumber  already exists")
                    {
                        UserDialogs.Instance.HideLoading();

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("AssetTagAssetNumberSerialNumberalreadyexists"), 2000);
                        return;
                    }
                    else if (response.servicestatusmessge == "AssetTag SerialNumber  already exists")
                    {
                        UserDialogs.Instance.HideLoading();

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("AssetTagSerialNumberalreadyexists"), 2000);
                        return;
                    }
                    else if (response.servicestatusmessge == "AssetTag AssetNumber  already exists")
                    {
                        UserDialogs.Instance.HideLoading();

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("AssetTagAssetNumberalreadyexists"), 2000);
                        return;
                    }
                    else if (response.servicestatusmessge == "AssetNumber SerialNumber  already exists")
                    {
                        UserDialogs.Instance.HideLoading();

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("AssetNumberSerialNumberalreadyexists"), 2000);
                        return;
                    }

                   
                }


                UserDialogs.Instance.HideLoading();

                //  OperationInProgress = false;



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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="FormLoadResponse"></param>
        /// <returns>If all validation get pass than return true</returns>
        private async Task<ValidationResult> ValidateControlsIsRequired(List<FormControl> CustomControls)
        {
            ValidationResult validationResult = new ValidationResult();
            if (CustomControls != null && CustomControls.Count > 0)
            {
                foreach (var formLoadItem in CustomControls)
                {
                    switch (formLoadItem.ControlName)
                    {
                        case "AssetTag":
                            {
                                validationResult = ValidateValidations(formLoadItem, AssetTagText);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "Capacity":
                            {
                                validationResult = ValidateValidations(formLoadItem, CapacityText);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "Category":
                            {
                                validationResult = ValidateValidations(formLoadItem, CategoryText);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "CurrentRuntime":
                            {
                                validationResult = ValidateValidations(formLoadItem, CurrentRuntimeText);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "DailyRuntime":
                            {
                                validationResult = ValidateValidations(formLoadItem, DailyRuntime);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "Description":
                            {
                                validationResult = ValidateValidations(formLoadItem, Description);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "DetailedLocation":
                            {
                                validationResult = ValidateValidations(formLoadItem, DetailedLocation);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "InstallationDate":
                            {
                                //if(InstallationDate==null)
                                //{
                                //    return validationResult;
                                //}
                                validationResult = ValidateValidations(formLoadItem, InstallationDate);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }



                        case "CriticalAsset":
                            {
                                validationResult = ValidateValidations(formLoadItem, CriticalAsset);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "MachineClassification":
                            {
                                validationResult = ValidateValidations(formLoadItem, MachineClassification);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }



                        case "Manufacturer":
                            {
                                validationResult = ValidateValidations(formLoadItem, Manufacturer);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "Model":
                            {
                                validationResult = ValidateValidations(formLoadItem, Model);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "OriginalCostID":
                            {
                                validationResult = ValidateValidations(formLoadItem, OriginalCost);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "ProfileImage":
                            {
                                validationResult = ValidateValidations(formLoadItem, ProfileImage);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "Rating":
                            {
                                validationResult = ValidateValidations(formLoadItem, Rating);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "SerialNumber":
                            {
                                validationResult = ValidateValidations(formLoadItem, SerialNumber);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "WarrantyDate":
                            {
                                //if(this.WarrantyDate==null)
                                //{
                                //    return validationResult;
                                //}
                                validationResult = ValidateValidations(formLoadItem, WarrantyDate);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "Weight":
                            {
                                validationResult = ValidateValidations(formLoadItem, Weight);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }
                        case "LOTOUrl":
                            {
                                validationResult = ValidateValidations(formLoadItem, LotoUrl);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }


                        #region User Fields

                        case "UserField1":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField1);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "UserField2":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField2);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "UserField3":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField3);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "UserField4":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField4);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "UserField5":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField5);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "UserField6":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField6);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "UserField7":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField7);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "UserField8":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField8);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "UserField9":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField9);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "UserField10":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField10);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "UserField11":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField11);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "UserField12":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField12);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "UserField13":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField13);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "UserField14":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField14);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "UserField15":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField15);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "UserField16":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField16);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "UserField17":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField17);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "UserField18":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField18);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "UserField19":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField19);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "UserField20":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField20);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "UserField21":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField21);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }


                        case "UserField22":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField22);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "UserField23":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField23);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }


                        case "UserField24":
                            {

                                validationResult = ValidateValidations(formLoadItem, UserField24);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }



                            #endregion





                    }
                }

                return validationResult;
            }

            return validationResult;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="FormLoadResponse"></param>
        /// <returns>If all validation get pass than return true</returns>
        private async Task<ValidationResult> ValidateOverriddenControlsIsRequired(List<FormControl> OverriddenControlsList)
        {
            ValidationResult validationResult = new ValidationResult();
            if (OverriddenControlsList != null && OverriddenControlsList.Count > 0)
            {
                foreach (var formLoadItem in OverriddenControlsList)
                {

                    switch (formLoadItem.ControlName)
                    {



                        case "AssetName":
                            {
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, AssetNameText);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }


                        case "AssetNumber":
                            {
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, AssetNumberText);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }



                        case "CategoryID":
                            {
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, CategoryID.ToString());
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }


                        case "VendorID":
                            {
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, VendorID.ToString());
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "RuntimeUnits":
                            {
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, RuntimeUnitID.ToString());
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }
                        case "AssetSystemID":
                            {
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, AssetSystemID.ToString());
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }



                    }

                }

                return validationResult;
            }

            return validationResult;
        }

        private ValidationResult ValidateValidations(FormControl formControl, string PropertyValue)
        {
            var validationResult = new ValidationResult();
            if ((formControl.IsRequired ?? false) && string.IsNullOrWhiteSpace(PropertyValue))
            {
                validationResult.FailedItem = formControl;
                validationResult.ErrorMessage = formControl.TargetName + " " + ConstantStrings.IsRequiredField;
                return validationResult;
            }


            //TODO: Apply type validations
            if (formControl.DataType == "int")
            {
                if (!ValidationRule.CheckInt(PropertyValue))
                {
                    validationResult.FailedItem = formControl;
                    validationResult.ErrorMessage = formControl.TargetName + " " + ConstantStrings.ShouldBeInteger;
                    return validationResult;
                }
            }


            else if (formControl.DataType == "decimal")
            {
                if (!ValidationRule.CheckDecimal(PropertyValue))
                {
                    validationResult.FailedItem = formControl;
                    validationResult.ErrorMessage = formControl.TargetName + " " + ConstantStrings.ShouldBeDecimal;
                    return validationResult;
                }
            }

            else if (formControl.DataType == "currency")
            {
                if (!ValidationRule.CheckDecimal(PropertyValue))
                {
                    validationResult.FailedItem = formControl;
                    validationResult.ErrorMessage = formControl.TargetName + " " + ConstantStrings.ShouldBeValidCurrency;
                    return validationResult;
                }
            }

            // If All validations pass return empty result
            return validationResult;


        }

        private ValidationResult ValidateValidationsForOverriddennControls(FormControl formControl, string PropertyValue)
        {
            var validationResult = new ValidationResult();
            if ((formControl.IsRequired ?? false) && string.IsNullOrWhiteSpace(PropertyValue))
            {
                validationResult.FailedItem = formControl;
                validationResult.ErrorMessage = formControl.TargetName + " " + ConstantStrings.IsRequiredField;
                return validationResult;
            }

            return validationResult;

        }



        public Task OnViewDisappearingAsync(VisualElement view)
        {

            return Task.FromResult(true);

        }

        public async Task SpeechtoText()
        {

            try
            {
                string Value = await DependencyService.Get<ISpeechToText>().SpeechToText();

                this.AdditionalDetailsText += " " + Value;
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
        #endregion
    }
}
