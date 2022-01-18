using ProteusMMX.Helpers;
using ProteusMMX.Helpers.DateTime;
using ProteusMMX.Helpers.StringFormatter;
using ProteusMMX.Model.ClosedWorkOrderModel;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.CloseWorkorder;
using ProteusMMX.Services.FormLoadInputs;
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
using ProteusMMX.Model;
using System.Linq;
using Acr.UserDialogs;
using ProteusMMX.Services.Navigation;

namespace ProteusMMX.ViewModel.ClosedWorkorder
{
    public class ClosedWorkorderDetailsPageViewModel : ViewModelBase
    {
        #region Fields

        ServiceOutput response;
        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly ICloseWorkorderService _closeWorkorderService;

        public readonly INavigationService _navigationService;

        #endregion


        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);

        public ICommand TapCommand => new AsyncCommand(ShowMoreDescription);
        public ICommand TapCommand1 => new AsyncCommand(ShowLongDescription);

        public ICommand TapCommandSignature => new AsyncCommand(ShowSignatures);

        public ICommand InternalNotesTapCommand => new AsyncCommand(ShowInternalNotesDescription);



        #endregion

        #region Properties

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

        bool _miscellaneousLabourCostLayoutIsVisible = true;
        public bool MiscellaneousLabourCostLayoutIsVisible
        {
            get
            {
                return _miscellaneousLabourCostLayoutIsVisible;
            }

            set
            {
                if (value != _miscellaneousLabourCostLayoutIsVisible)
                {
                    _miscellaneousLabourCostLayoutIsVisible = value;
                    OnPropertyChanged(nameof(MiscellaneousLabourCostLayoutIsVisible));
                }
            }

        }
        string _targetTitle;
        public string TargetTitle
        {
            get
            {
                return _targetTitle;
            }

            set
            {
                if (value != _targetTitle)
                {
                    _targetTitle = value;
                    OnPropertyChanged(nameof(TargetTitle));
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
                    OnPropertyChanged(nameof(TargetName));
                }
            }
        }

        bool _workorderRequesterLayout = true;
        public bool WorkorderRequesterLayout
        {
            get
            {
                return _workorderRequesterLayout;
            }

            set
            {
                if (value != _workorderRequesterLayout)
                {
                    _workorderRequesterLayout = value;
                    OnPropertyChanged(nameof(WorkorderRequesterLayout));
                }
            }
        }
        bool _miscellaneousMaterialCostLayout = true;
        public bool MiscellaneousMaterialCostLayout
        {
            get
            {
                return _miscellaneousMaterialCostLayout;
            }

            set
            {
                if (value != _miscellaneousMaterialCostLayout)
                {
                    _miscellaneousMaterialCostLayout = value;
                    OnPropertyChanged(nameof(MiscellaneousMaterialCostLayout));
                }
            }
        }
        bool _shiftLayoutIsVisible = true;
        public bool ShiftLayoutIsVisible
        {
            get
            {
                return _shiftLayoutIsVisible;
            }

            set
            {
                if (value != _shiftLayoutIsVisible)
                {
                    _shiftLayoutIsVisible = value;
                    OnPropertyChanged(nameof(ShiftLayoutIsVisible));
                }
            }
        }
        bool _costCenterLayoutIsVisible = true;
        public bool CostCenterLayoutIsVisible
        {
            get
            {
                return _costCenterLayoutIsVisible;
            }

            set
            {
                if (value != _costCenterLayoutIsVisible)
                {
                    _costCenterLayoutIsVisible = value;
                    OnPropertyChanged(nameof(CostCenterLayoutIsVisible));
                }
            }
        }
        bool _assetSystemLayoutIsVisible = true;
        public bool AssetSystemLayoutIsVisible
        {
            get
            {
                return _assetSystemLayoutIsVisible;
            }

            set
            {
                if (value != _assetSystemLayoutIsVisible)
                {
                    _assetSystemLayoutIsVisible = value;
                    OnPropertyChanged(nameof(AssetSystemLayoutIsVisible));
                }
            }
        }
        bool _locationLayoutIsVisible = true;
        public bool LocationLayoutIsVisible
        {
            get
            {
                return _locationLayoutIsVisible;
            }

            set
            {
                if (value != _locationLayoutIsVisible)
                {
                    _locationLayoutIsVisible = value;
                    OnPropertyChanged(nameof(LocationLayoutIsVisible));
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
                    OnPropertyChanged(nameof(WelcomeTextTitle));
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
        bool _currentRuntimeIsVisible = true;
        public bool CurrentRuntimeIsVisible
        {
            get
            {
                return _currentRuntimeIsVisible;
            }

            set
            {
                if (value != _currentRuntimeIsVisible)
                {
                    _currentRuntimeIsVisible = value;
                    OnPropertyChanged(nameof(CurrentRuntimeIsVisible));
                }
            }

        }

 string _currentRuntimeTitle;
       public string CurrentRuntimeTitle
       {
           get
           {
               return _currentRuntimeTitle;
           }

           set
           {
               if (value != _currentRuntimeTitle)
               {
                   _currentRuntimeTitle = value;
                   OnPropertyChanged(nameof(CurrentRuntimeTitle));
               }
           }
       }

string _currentRuntime;
       public string CurrentRuntime
       {
           get
           {
               return _currentRuntime;
           }

           set
           {
               if (value != _currentRuntime)
               {
                   _currentRuntime = value;
                   OnPropertyChanged(nameof(CurrentRuntime));
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
        #region CreateWorkorder Properties
        string _sectionNameTitle;
        public string SectionNameTitle
        {
            get
            {
                return _sectionNameTitle;
            }

            set
            {
                if (value != _sectionNameTitle)
                {
                    _sectionNameTitle = value;
                    OnPropertyChanged(nameof(SectionNameTitle));
                }
            }
        }

        string _totalTimeTilte;
        public string TotalTimeTilte
        {
            get
            {
                return _totalTimeTilte;
            }

            set
            {
                if (value != _totalTimeTilte)
                {
                    _totalTimeTilte = value;
                    OnPropertyChanged(nameof(TotalTimeTilte));
                }
            }
        }
        string _sectionNameText;
        public string SectionNameText
        {
            get
            {
                return _sectionNameText;
            }

            set
            {
                if (value != _sectionNameText)
                {
                    _sectionNameText = value;
                    OnPropertyChanged(nameof(SectionNameText));
                }
            }
        }
        string _moreText;
        public string MoreText
        {
            get
            {
                return _moreText;
            }

            set
            {
                if (value != _moreText)
                {
                    _moreText = value;
                    OnPropertyChanged(nameof(MoreText));
                }
            }
        }


        int? _closedWorkorderID;
        public int? ClosedWorkorderID
        {
            get
            {
                return _closedWorkorderID;
            }

            set
            {
                if (value != _closedWorkorderID)
                {
                    _closedWorkorderID = value;
                    OnPropertyChanged(nameof(ClosedWorkorderID));
                }
            }
        }





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


        // Workorder Number
        string _workorderNumberText;
        public string WorkorderNumberText
        {
            get
            {
                return _workorderNumberText;
            }

            set
            {
                if (value != _workorderNumberText)
                {
                    _workorderNumberText = value;
                    OnPropertyChanged(nameof(WorkorderNumberText));
                }
            }
        }

        string _workorderNumbeTitle;
        public string WorkorderNumbeTitle
        {
            get
            {
                return _workorderNumbeTitle;
            }

            set
            {
                if (value != _workorderNumbeTitle)
                {
                    _workorderNumbeTitle = value;
                    OnPropertyChanged(nameof(WorkorderNumbeTitle));
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
        bool _signaturesIsVisible = false;
        public bool SignaturesIsVisible
        {
            get
            {
                return _signaturesIsVisible;
            }

            set
            {
                if (value != _signaturesIsVisible)
                {
                    _signaturesIsVisible = value;
                    OnPropertyChanged(nameof(SignaturesIsVisible));
                }
            }
        }
        string _signatureText;
        public string SignatureText
        {
            get
            {
                return _signatureText;
            }

            set
            {
                if (value != _signatureText)
                {
                    _signatureText = value;
                    OnPropertyChanged(nameof(SignatureText));
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
        DateTime _requiredDate;
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

        string _assetsTitle;
        public string AssetsTitle
        {
            get
            {
                return _assetsTitle;
            }

            set
            {
                if (value != _assetsTitle)
                {
                    _assetsTitle = value;
                    OnPropertyChanged(nameof(AssetsTitle));
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
        bool _IsCostLayoutIsVisibleForPhone = false;
        public bool IsCostLayoutIsVisibleForPhone
        {
            get
            {
                return _IsCostLayoutIsVisibleForPhone;
            }

            set
            {
                if (value != _IsCostLayoutIsVisibleForPhone)
                {
                    _IsCostLayoutIsVisibleForPhone = value;
                    OnPropertyChanged(nameof(IsCostLayoutIsVisibleForPhone));
                }
            }
        }

        bool _IsCostLayoutIsVisibleForTab = false;
        public bool IsCostLayoutIsVisibleForTab
        {
            get
            {
                return _IsCostLayoutIsVisibleForTab;
            }

            set
            {
                if (value != _IsCostLayoutIsVisibleForTab)
                {
                    _IsCostLayoutIsVisibleForTab = value;
                    OnPropertyChanged(nameof(IsCostLayoutIsVisibleForTab));
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

        bool _causeIsVisible = true;
        public bool CauseIsVisible
        {
            get
            {
                return _causeIsVisible;
            }

            set
            {
                if (value != _causeIsVisible)
                {
                    _causeIsVisible = value;
                    OnPropertyChanged(nameof(CauseIsVisible));
                }
            }
        }

        Cause _cause;
        public Cause Cause
        {
            get
            {
                return _cause;
            }

            set
            {
                if (value != _cause)
                {
                    _cause = value;
                    OnPropertyChanged(nameof(Cause));
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
        string _internalNoteText;
        public string InternalNoteText
        {
            get
            {
                return _internalNoteText;
            }

            set
            {
                if (value != _internalNoteText)
                {
                    _internalNoteText = value;
                    OnPropertyChanged(nameof(InternalNoteText));
                }
            }
        }

        string _internalNoteTitle;
        public string InternalNoteTitle
        {
            get
            {
                return _internalNoteTitle;
            }

            set
            {
                if (value != _internalNoteTitle)
                {
                    _internalNoteTitle = value;
                    OnPropertyChanged(nameof(InternalNoteTitle));
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

        bool _internalNotesIsVisible = true;
        public bool InternalNoteIsVisible
        {
            get
            {
                return _internalNotesIsVisible;
            }

            set
            {
                if (value != _internalNotesIsVisible)
                {
                    _internalNotesIsVisible = value;
                    OnPropertyChanged(nameof(InternalNoteIsVisible));
                }
            }
        }

        // EstimstedDowntime

        decimal? _estimstedDowntimeText;
        public decimal? EstimstedDowntimeText
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

        decimal? _actualDowntimeText;
        public decimal? ActualDowntimeText
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


        //Abnormality
        string _abnormalityText;
        public string AbnormalityText
        {
            get
            {
                return _abnormalityText;
            }

            set
            {
                if (value != _abnormalityText)
                {
                    _abnormalityText = value;
                    OnPropertyChanged(nameof(AbnormalityText));
                }
            }
        }


        // Activation Date
        DateTime _activationDateText;
        public DateTime ActivationDateText
        {
            get
            {
                return _activationDateText;
            }

            set
            {
                if (value != _activationDateText)
                {
                    _activationDateText = value;
                    OnPropertyChanged(nameof(ActivationDateText));
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
                    OnPropertyChanged(nameof(ActivationDateTitle));
                }
            }
        }


        // Activation Date
        //string _actualDowntimeText;
        //public string ActualDowntimeText
        //{
        //    get
        //    {
        //        return _actualDowntimeText;
        //    }

        //    set
        //    {
        //        if (value != _actualDowntimeText)
        //        {
        //            _actualDowntimeText = value;
        //            OnPropertyChanged(nameof(ActualDowntimeText));
        //        }
        //    }
        //}


        // Closed Date
        DateTime _closedDateText;
        public DateTime ClosedDateText
        {
            get
            {
                return _closedDateText;
            }

            set
            {
                if (value != _closedDateText)
                {
                    _closedDateText = value;
                    OnPropertyChanged(nameof(ClosedDateText));
                }
            }
        }
        string _closedDateTitle;
        public string ClosedDateTitle
        {
            get
            {
                return _closedDateTitle;
            }

            set
            {
                if (value != _closedDateTitle)
                {
                    _closedDateTitle = value;
                    OnPropertyChanged(nameof(ClosedDateTitle));
                }
            }
        }



        //AmStepID
        string _amStepID;
        public string AmStepID
        {
            get
            {
                return _amStepID;
            }

            set
            {
                if (value != _amStepID)
                {
                    _amStepID = value;
                    OnPropertyChanged(nameof(AmStepID));
                }
            }
        }



        //AnalysisPerformedDate
        string _analysisPerformedDate;
        public string AnalysisPerformedDate
        {
            get
            {
                return _analysisPerformedDate;
            }

            set
            {
                if (value != _analysisPerformedDate)
                {
                    _analysisPerformedDate = value;
                    OnPropertyChanged(nameof(AnalysisPerformedDate));
                }
            }
        }

        //ConfirmEmail
        string _confirmEmail;
        public string ConfirmEmail
        {
            get
            {
                return _confirmEmail;
            }

            set
            {
                if (value != _confirmEmail)
                {
                    _confirmEmail = value;
                    OnPropertyChanged(nameof(ConfirmEmail));
                }
            }
        }
        string _confirmEmailTitle;
        public string ConfirmEmailTitle
        {
            get
            {
                return _confirmEmailTitle;
            }

            set
            {
                if (value != _confirmEmailTitle)
                {
                    _confirmEmailTitle = value;
                    OnPropertyChanged(nameof(ConfirmEmailTitle));
                }
            }
        }

        //CountermeasuresDefinedDate
        string _countermeasuresDefinedDate;
        public string CountermeasuresDefinedDate
        {
            get
            {
                return _countermeasuresDefinedDate;
            }

            set
            {
                if (value != _countermeasuresDefinedDate)
                {
                    _countermeasuresDefinedDate = value;
                    OnPropertyChanged(nameof(CountermeasuresDefinedDate));
                }
            }
        }


        ////CurrentRuntime
        //string _currentRuntime;
        //public string CurrentRuntime
        //{
        //    get
        //    {
        //        return _currentRuntime;
        //    }

        //    set
        //    {
        //        if (value != _currentRuntime)
        //        {
        //            _currentRuntime = value;
        //            OnPropertyChanged(nameof(CurrentRuntime));
        //        }
        //    }
        //}


        //DiagnosticTime
        string _diagnosticTime;
        public string DiagnosticTime
        {
            get
            {
                return _diagnosticTime;
            }

            set
            {
                if (value != _diagnosticTime)
                {
                    _diagnosticTime = value;
                    OnPropertyChanged(nameof(DiagnosticTime));
                }
            }
        }


        //DigitalSignatures
        string _digitalSignatures;
        public string DigitalSignatures
        {
            get
            {
                return _digitalSignatures;
            }

            set
            {
                if (value != _digitalSignatures)
                {
                    _digitalSignatures = value;
                    OnPropertyChanged(nameof(DigitalSignatures));
                }
            }
        }
        string _digitalSignaturesTitle;
        public string DigitalSignaturesTitle
        {
            get
            {
                return _digitalSignaturesTitle;
            }

            set
            {
                if (value != _digitalSignaturesTitle)
                {
                    _digitalSignaturesTitle = value;
                    OnPropertyChanged(nameof(DigitalSignaturesTitle));
                }
            }
        }
        string _signatures;
        public string Signatures
        {
            get
            {
                return _signatures;
            }

            set
            {
                if (value != _signatures)
                {
                    _signatures = value;
                    OnPropertyChanged(nameof(Signatures));
                }
            }
        }

        //EstimatedDowntime
        //string _estimatedDowntime;
        //public string EstimatedDowntime
        //{
        //    get
        //    {
        //        return _estimatedDowntime;
        //    }

        //    set
        //    {
        //        if (value != _estimatedDowntime)
        //        {
        //            _estimatedDowntime = value;
        //            OnPropertyChanged(nameof(EstimatedDowntime));
        //        }
        //    }
        //}

        //ImplementationValidatedDate
        string _implementationValidatedDate;
        public string ImplementationValidatedDate
        {
            get
            {
                return _implementationValidatedDate;
            }

            set
            {
                if (value != _implementationValidatedDate)
                {
                    _implementationValidatedDate = value;
                    OnPropertyChanged(nameof(ImplementationValidatedDate));
                }
            }
        }

        //InitialWaitTime
        string _initialWaitTime;
        public string InitialWaitTime
        {
            get
            {
                return _initialWaitTime;
            }

            set
            {
                if (value != _initialWaitTime)
                {
                    _initialWaitTime = value;
                    OnPropertyChanged(nameof(InitialWaitTime));
                }
            }
        }


        //JobNumber
        string _jobNumber;
        public string JobNumber
        {
            get
            {
                return _jobNumber;
            }

            set
            {
                if (value != _jobNumber)
                {
                    _jobNumber = value;
                    OnPropertyChanged(nameof(JobNumber));
                }
            }
        }

        //MiscellaneousLaborCostID
        string _miscellaneousLaborCostID;
        public string MiscellaneousLaborCostID
        {
            get
            {
                return _miscellaneousLaborCostID;
            }

            set
            {
                if (value != _miscellaneousLaborCostID)
                {
                    _miscellaneousLaborCostID = value;
                    OnPropertyChanged(nameof(MiscellaneousLaborCostID));
                }
            }
        }

        //MiscellaneousMaterialsCostID
        string _miscellaneousMaterialsCostID;
        public string MiscellaneousMaterialsCostID
        {
            get
            {
                return _miscellaneousMaterialsCostID;
            }

            set
            {
                if (value != _miscellaneousMaterialsCostID)
                {
                    _miscellaneousMaterialsCostID = value;
                    OnPropertyChanged(nameof(MiscellaneousMaterialsCostID));
                }
            }
        }

        string _chargeCostsOnlyToChildAssets;
        public string ChargeCostsOnlyToChildAssets
        {
            get
            {
                return _chargeCostsOnlyToChildAssets;
            }

            set
            {
                if (value != _chargeCostsOnlyToChildAssets)
                {
                    _chargeCostsOnlyToChildAssets = value;
                    OnPropertyChanged(nameof(ChargeCostsOnlyToChildAssets));
                }
            }
        }

        string _parentCostsOnly;
        public string ParentCostsOnly
        {
            get
            {
                return _parentCostsOnly;
            }

            set
            {
                if (value != _parentCostsOnly)
                {
                    _parentCostsOnly = value;
                    OnPropertyChanged(nameof(ParentCostsOnly));
                }
            }
        }

        //NotificationTime
        string _notificationTime;
        public string NotificationTime
        {
            get
            {
                return _notificationTime;
            }

            set
            {
                if (value != _notificationTime)
                {
                    _notificationTime = value;
                    OnPropertyChanged(nameof(NotificationTime));
                }
            }
        }


        //PartWaitingTime
        string _partWaitingTime;
        public string PartWaitingTime
        {
            get
            {
                return _partWaitingTime;
            }

            set
            {
                if (value != _partWaitingTime)
                {
                    _partWaitingTime = value;
                    OnPropertyChanged(nameof(PartWaitingTime));
                }
            }
        }

        //ProblemID
        string _problemID;
        public string ProblemID
        {
            get
            {
                return _problemID;
            }

            set
            {
                if (value != _problemID)
                {
                    _problemID = value;
                    OnPropertyChanged(nameof(ProblemID));
                }
            }
        }


        //project
        string _project;
        public string Project
        {
            get
            {
                return _project;
            }

            set
            {
                if (value != _project)
                {
                    _project = value;
                    OnPropertyChanged(nameof(Project));
                }
            }
        }
        string _projectTitle;
        public string ProjectTitle
        {
            get
            {
                return _projectTitle;
            }

            set
            {
                if (value != _projectTitle)
                {
                    _projectTitle = value;
                    OnPropertyChanged(nameof(ProjectTitle));
                }
            }
        }


        //RelatedToID
        string _relatedToID;
        public string RelatedToID
        {
            get
            {
                return _relatedToID;
            }

            set
            {
                if (value != _relatedToID)
                {
                    _relatedToID = value;
                    OnPropertyChanged(nameof(RelatedToID));
                }
            }
        }

        //RepairingOrReplacementTime
        string _repairingOrReplacementTime;
        public string RepairingOrReplacementTime
        {
            get
            {
                return _repairingOrReplacementTime;
            }

            set
            {
                if (value != _repairingOrReplacementTime)
                {
                    _repairingOrReplacementTime = value;
                    OnPropertyChanged(nameof(RepairingOrReplacementTime));
                }
            }
        }

        //RequestedDate
        DateTime _requestedDate;
        public DateTime RequestedDate
        {
            get
            {
                return _requestedDate;
            }

            set
            {
                if (value != _requestedDate)
                {
                    _requestedDate = value;
                    OnPropertyChanged(nameof(RequestedDate));
                }
            }
        }

        //RequesterEmail
        string _requesterEmail;
        public string RequesterEmail
        {
            get
            {
                return _requesterEmail;
            }

            set
            {
                if (value != _requesterEmail)
                {
                    _requesterEmail = value;
                    OnPropertyChanged(nameof(RequesterEmail));
                }
            }
        }

        string _requesterEmailTitle;
        public string RequesterEmailTitle
        {
            get
            {
                return _requesterEmailTitle;
            }

            set
            {
                if (value != _requesterEmailTitle)
                {
                    _requesterEmailTitle = value;
                    OnPropertyChanged(nameof(RequesterEmailTitle));
                }
            }
        }

        //RequesterFullName
        string _requesterFullName;
        public string RequesterFullName
        {
            get
            {
                return _requesterFullName;
            }

            set
            {
                if (value != _requesterFullName)
                {
                    _requesterFullName = value;
                    OnPropertyChanged(nameof(RequesterFullName));
                }
            }
        }
        string _requesterFullNameTitle;
        public string RequesterFullNameTitle
        {
            get
            {
                return _requesterFullNameTitle;
            }

            set
            {
                if (value != _requesterFullNameTitle)
                {
                    _requesterFullNameTitle = value;
                    OnPropertyChanged(nameof(RequesterFullNameTitle));
                }
            }
        }

        //RequesterPhone
        string _requesterPhone;
        public string RequesterPhone
        {
            get
            {
                return _requesterPhone;
            }

            set
            {
                if (value != _requesterPhone)
                {
                    _requesterPhone = value;
                    OnPropertyChanged(nameof(RequesterPhone));
                }
            }
        }
        string _requesterPhoneTitle;
        public string RequesterPhoneTitle
        {
            get
            {
                return _requesterPhoneTitle;
            }

            set
            {
                if (value != _requesterPhoneTitle)
                {
                    _requesterPhoneTitle = value;
                    OnPropertyChanged(nameof(RequesterPhoneTitle));
                }
            }
        }

        //RequestNumber
        string _requestNumber;
        public string RequestNumber
        {
            get
            {
                return _requestNumber;
            }

            set
            {
                if (value != _requestNumber)
                {
                    _requestNumber = value;
                    OnPropertyChanged(nameof(RequestNumber));
                }
            }
        }
        string _requestNumberTitle;
        public string RequestNumberTitle
        {
            get
            {
                return _requestNumberTitle;
            }

            set
            {
                if (value != _requestNumberTitle)
                {
                    _requestNumberTitle = value;
                    OnPropertyChanged(nameof(RequestNumberTitle));
                }
            }
        }


        //ServiceRequestModeID
        string _serviceRequestModeID;
        public string ServiceRequestModeID
        {
            get
            {
                return _serviceRequestModeID;
            }

            set
            {
                if (value != _serviceRequestModeID)
                {
                    _serviceRequestModeID = value;
                    OnPropertyChanged(nameof(ServiceRequestModeID));
                }
            }
        }

        //StartupTime
        string _startupTime;
        public string StartupTime
        {
            get
            {
                return _startupTime;
            }

            set
            {
                if (value != _startupTime)
                {
                    _startupTime = value;
                    OnPropertyChanged(nameof(StartupTime));
                }
            }
        }

        //TotalTime
        string _totalTime;
        public string TotalTime
        {
            get
            {
                return _totalTime;
            }

            set
            {
                if (value != _totalTime)
                {
                    _totalTime = value;
                    OnPropertyChanged(nameof(TotalTime));
                }
            }
        }

        //UnsafeConditionID
        string _unsafeConditionID;
        public string UnsafeConditionID
        {
            get
            {
                return _unsafeConditionID;
            }

            set
            {
                if (value != _unsafeConditionID)
                {
                    _unsafeConditionID = value;
                    OnPropertyChanged(nameof(UnsafeConditionID));
                }
            }
        }

        //WorkOrderNumber
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
                    OnPropertyChanged(nameof(WorkOrderNumber));
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
        string _userField1Title;
        public string UserField1Title
        {
            get
            {
                return _userField1Title;
            }

            set
            {
                if (value != _userField1Title)
                {
                    _userField1Title = value;
                    OnPropertyChanged(nameof(UserField1Title));
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
        string _userField2Title;
        public string UserField2Title
        {
            get
            {
                return _userField2Title;
            }

            set
            {
                if (value != _userField2Title)
                {
                    _userField2Title = value;
                    OnPropertyChanged(nameof(UserField2Title));
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
        string _userField3Title;
        public string UserField3Title
        {
            get
            {
                return _userField3Title;
            }

            set
            {
                if (value != _userField3Title)
                {
                    _userField3Title = value;
                    OnPropertyChanged(nameof(UserField3Title));
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
        string _userField4Title;
        public string UserField4Title
        {
            get
            {
                return _userField4Title;
            }

            set
            {
                if (value != _userField4Title)
                {
                    _userField4Title = value;
                    OnPropertyChanged(nameof(UserField4Title));
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
        string _userField5Title;
        public string UserField5Title
        {
            get
            {
                return _userField5Title;
            }

            set
            {
                if (value != _userField5Title)
                {
                    _userField5Title = value;
                    OnPropertyChanged(nameof(UserField5Title));
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
        string _userField6Title;
        public string UserField6Title
        {
            get
            {
                return _userField6Title;
            }

            set
            {
                if (value != _userField6Title)
                {
                    _userField6Title = value;
                    OnPropertyChanged(nameof(UserField6Title));
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
        string _userField7Title;
        public string UserField7Title
        {
            get
            {
                return _userField7Title;
            }

            set
            {
                if (value != _userField7Title)
                {
                    _userField7Title = value;
                    OnPropertyChanged(nameof(UserField7Title));
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
        string _userField8Title;
        public string UserField8Title
        {
            get
            {
                return _userField8Title;
            }

            set
            {
                if (value != _userField8Title)
                {
                    _userField8Title = value;
                    OnPropertyChanged(nameof(UserField8Title));
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

        string _userField9Title;
        public string UserField9Title
        {
            get
            {
                return _userField9Title;
            }

            set
            {
                if (value != _userField9Title)
                {
                    _userField9Title = value;
                    OnPropertyChanged(nameof(UserField9Title));
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
        string _userField10Title;
        public string UserField10Title
        {
            get
            {
                return _userField10Title;
            }

            set
            {
                if (value != _userField10Title)
                {
                    _userField10Title = value;
                    OnPropertyChanged(nameof(UserField10Title));
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
        string _userField11Title;
        public string UserField11Title
        {
            get
            {
                return _userField11Title;
            }

            set
            {
                if (value != _userField11Title)
                {
                    _userField11Title = value;
                    OnPropertyChanged(nameof(UserField11Title));
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
        string _userField12Title;
        public string UserField12Title
        {
            get
            {
                return _userField12Title;
            }

            set
            {
                if (value != _userField12Title)
                {
                    _userField12Title = value;
                    OnPropertyChanged(nameof(UserField12Title));
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
        string _userField13Title;
        public string UserField13Title
        {
            get
            {
                return _userField13Title;
            }

            set
            {
                if (value != _userField13Title)
                {
                    _userField13Title = value;
                    OnPropertyChanged(nameof(UserField13Title));
                }
            }
        }


        //UserField14
        string _userField14Title;
        public string UserField14Title
        {
            get
            {
                return _userField14Title;
            }

            set
            {
                if (value != _userField14Title)
                {
                    _userField14Title = value;
                    OnPropertyChanged(nameof(UserField14Title));
                }
            }
        }
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
        string _userField15Title;
        public string UserField15Title
        {
            get
            {
                return _userField15Title;
            }

            set
            {
                if (value != _userField15Title)
                {
                    _userField15Title = value;
                    OnPropertyChanged(nameof(UserField15Title));
                }
            }
        }

        //UserField16
        string _userField16Title;
        public string UserField16Title
        {
            get
            {
                return _userField16Title;
            }

            set
            {
                if (value != _userField16Title)
                {
                    _userField16Title = value;
                    OnPropertyChanged(nameof(UserField16Title));
                }
            }
        }
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
        string _userField17Title;
        public string UserField17Title
        {
            get
            {
                return _userField17Title;
            }

            set
            {
                if (value != _userField17Title)
                {
                    _userField17Title = value;
                    OnPropertyChanged(nameof(UserField17Title));
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
        string _userField18Title;
        public string UserField18Title
        {
            get
            {
                return _userField18Title;
            }

            set
            {
                if (value != _userField18Title)
                {
                    _userField18Title = value;
                    OnPropertyChanged(nameof(UserField18Title));
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
        string _userField19Title;
        public string UserField19Title
        {
            get
            {
                return _userField19Title;
            }

            set
            {
                if (value != _userField19Title)
                {
                    _userField19Title = value;
                    OnPropertyChanged(nameof(UserField19Title));
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
        string _userField20Title;
        public string UserField20Title
        {
            get
            {
                return _userField20Title;
            }

            set
            {
                if (value != _userField20Title)
                {
                    _userField20Title = value;
                    OnPropertyChanged(nameof(UserField20Title));
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
        string _userField21Title;
        public string UserField21Title
        {
            get
            {
                return _userField21Title;
            }

            set
            {
                if (value != _userField21Title)
                {
                    _userField21Title = value;
                    OnPropertyChanged(nameof(UserField21Title));
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
        string _userField22Title;
        public string UserField22Title
        {
            get
            {
                return _userField22Title;
            }

            set
            {
                if (value != _userField22Title)
                {
                    _userField22Title = value;
                    OnPropertyChanged(nameof(UserField22Title));
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
        string _userField23Title;
        public string UserField23Title
        {
            get
            {
                return _userField23Title;
            }

            set
            {
                if (value != _userField23Title)
                {
                    _userField23Title = value;
                    OnPropertyChanged(nameof(UserField23Title));
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

        string _approvalLevel;
        public string ApprovalLevel
        {
            get
            {
                return _approvalLevel;
            }

            set
            {
                if (value != _approvalLevel)
                {
                    _approvalLevel = value;
                    OnPropertyChanged(nameof(ApprovalLevel));
                }
            }
        }
        string _approvalNumber;
        public string ApprovalNumber
        {
            get
            {
                return _approvalNumber;
            }

            set
            {
                if (value != _approvalNumber)
                {
                    _approvalNumber = value;
                    OnPropertyChanged(nameof(ApprovalNumber));
                }
            }
        }
        string _userField24Title;
        public string UserField24Title
        {
            get
            {
                return _userField24Title;
            }

            set
            {
                if (value != _userField24Title)
                {
                    _userField24Title = value;
                    OnPropertyChanged(nameof(UserField24Title));
                }
            }
        }

        string _approvalLevelTitle;
        public string ApprovalLevelTitle
        {
            get
            {
                return _approvalLevelTitle;
            }

            set
            {
                if (value != _approvalLevelTitle)
                {
                    _approvalLevelTitle = value;
                    OnPropertyChanged(nameof(ApprovalLevelTitle));
                }
            }
        }

        string _approvalNumberTitle;
        public string ApprovalNumberTitle
        {
            get
            {
                return _approvalNumberTitle;
            }

            set
            {
                if (value != _approvalNumberTitle)
                {
                    _approvalNumberTitle = value;
                    OnPropertyChanged(nameof(ApprovalNumberTitle));
                }
            }
        }

        #endregion


        #endregion

        #endregion

        // Total Time
        string _totalTimeText;
        public string TotalTimeText
        {
            get
            {
                return _totalTimeText;
            }

            set
            {
                if (value != _totalTimeText)
                {
                    _totalTimeText = value;
                    OnPropertyChanged(nameof(TotalTimeText));
                }
            }
        }
        bool _requestedDateIsVisible = true;
        public bool RequestedDateIsVisible
        {
            get
            {
                return _requestedDateIsVisible;
            }

            set
            {
                if (value != _requestedDateIsVisible)
                {
                    _requestedDateIsVisible = value;
                    OnPropertyChanged(nameof(RequestedDateIsVisible));
                }
            }
        }

        bool _requestedNumberIsVisible = true;
        public bool RequestedNumberIsVisible
        {
            get
            {
                return _requestedNumberIsVisible;
            }

            set
            {
                if (value != _requestedNumberIsVisible)
                {
                    _requestedNumberIsVisible = value;
                    OnPropertyChanged(nameof(RequestedNumberIsVisible));
                }
            }
        }
        bool _requesterEmailLayout = true;
        public bool RequesterEmailLayout
        {
            get
            {
                return _requesterEmailLayout;
            }

            set
            {
                if (value != _requesterEmailLayout)
                {
                    _requesterEmailLayout = value;
                    OnPropertyChanged(nameof(RequesterEmailLayout));
                }
            }
        }


        bool _sectionNameIsVisible = true;
        public bool SectionNameIsVisible
        {
            get
            {
                return _sectionNameIsVisible;
            }

            set
            {
                if (value != _sectionNameIsVisible)
                {
                    _sectionNameIsVisible = value;
                    OnPropertyChanged(nameof(SectionNameIsVisible));
                }
            }
        }
        bool _ShowAssociatedAssets = false;
        public bool ShowAssociatedAssets
        {
            get
            {
                return _ShowAssociatedAssets;
            }

            set
            {
                if (value != _ShowAssociatedAssets)
                {
                    _ShowAssociatedAssets = value;
                    OnPropertyChanged(nameof(ShowAssociatedAssets));
                }
            }
        }
        bool _isCostDistributed = false;
        public bool IsCostDistributed
        {
            get
            {
                return _isCostDistributed;
            }

            set
            {
                if (value != _isCostDistributed)
                {
                    _isCostDistributed = value;
                    OnPropertyChanged("IsCostDistributed");
                }
            }
        }

        bool _childCostDistributed = false;
        public bool ChildCostDistributed
        {
            get
            {
                return _childCostDistributed;
            }

            set
            {
                if (value != _childCostDistributed)
                {
                    _childCostDistributed = value;
                    OnPropertyChanged("ChildCostDistributed");
                }
            }
        }

        bool _parentCostDistributed = false;
        public bool ParentCostDistributed
        {
            get
            {
                return _parentCostDistributed;
            }

            set
            {
                if (value != _parentCostDistributed)
                {
                    _parentCostDistributed = value;
                    OnPropertyChanged("ParentCostDistributed");
                }
            }
        }

        string _distributeCostforAssetsystem;
        public string DistributeCostforAssetsystem
        {
            get
            {
                return _distributeCostforAssetsystem;
            }

            set
            {
                if (value != _distributeCostforAssetsystem)
                {
                    _distributeCostforAssetsystem = value;
                    OnPropertyChanged(nameof(DistributeCostforAssetsystem));
                }
            }
        }

        bool _isCostLayoutIsVisible = true;
        public bool IsCostLayoutIsVisible
        {
            get
            {
                return _isCostLayoutIsVisible;
            }

            set
            {
                if (value != _isCostLayoutIsVisible)
                {
                    _isCostLayoutIsVisible = value;
                    OnPropertyChanged(nameof(IsCostLayoutIsVisible));
                }
            }
        }

        bool _isCostLayoutIsEnable = true;
        public bool IsCostLayoutIsEnable
        {
            get
            {
                return _isCostLayoutIsEnable;
            }

            set
            {
                if (value != _isCostLayoutIsEnable)
                {
                    _isCostLayoutIsEnable = value;
                    OnPropertyChanged(nameof(IsCostLayoutIsEnable));
                }
            }
        }

        #region Show More Text

        string _descriptionMoreText;
        public string DescriptionMoreText
        {
            get
            {
                return _descriptionMoreText;
            }

            set
            {
                if (value != _descriptionMoreText)
                {
                    _descriptionMoreText = value;
                    OnPropertyChanged(nameof(DescriptionMoreText));
                }
            }
        }

        bool _moreDescriptionTextIsEnable = false;
        public bool MoreDescriptionTextIsEnable
        {
            get
            {
                return _moreDescriptionTextIsEnable;
            }

            set
            {
                if (value != _moreDescriptionTextIsEnable)
                {
                    _moreDescriptionTextIsEnable = value;
                    OnPropertyChanged(nameof(MoreDescriptionTextIsEnable));
                }
            }
        }


        string _internalNoteMoreText;
        public string InternalNoteMoreText
        {
            get
            {
                return _internalNoteMoreText;
            }

            set
            {
                if (value != _internalNoteMoreText)
                {
                    _internalNoteMoreText = value;
                    OnPropertyChanged(nameof(InternalNoteMoreText));
                }
            }
        }

        bool _moreInternalNoteTextIsEnable = false;
        public bool MoreInternalNoteTextIsEnable
        {
            get
            {
                return _moreInternalNoteTextIsEnable;
            }

            set
            {
                if (value != _moreInternalNoteTextIsEnable)
                {
                    _moreInternalNoteTextIsEnable = value;
                    OnPropertyChanged(nameof(MoreInternalNoteTextIsEnable));
                }
            }
        }



        string _additionalDetailsMoreText;
        public string AdditionalDetailsMoreText
        {
            get
            {
                return _additionalDetailsMoreText;
            }

            set
            {
                if (value != _additionalDetailsMoreText)
                {
                    _additionalDetailsMoreText = value;
                    OnPropertyChanged(nameof(AdditionalDetailsMoreText));
                }
            }
        }

        bool _moreAdditionalDetailsTextIsEnable = false;
        public bool MoreAdditionalDetailsTextIsEnable
        {
            get
            {
                return _moreAdditionalDetailsTextIsEnable;
            }

            set
            {
                if (value != _moreAdditionalDetailsTextIsEnable)
                {
                    _moreAdditionalDetailsTextIsEnable = value;
                    OnPropertyChanged(nameof(MoreAdditionalDetailsTextIsEnable));
                }
            }
        }
        #endregion

        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                OperationInProgress = true;

                //if (ConnectivityService.IsConnected == false)
                //{
                //    await DialogService.ShowAlertAsync("internet not available", "Alert", "OK");
                //    return;

                //}

                var navigationParams = navigationData as PageParameters;
                this.Page = navigationParams.Page;

                var closedworkorder = navigationParams.Parameter as ClosedWorkOrder;
                this.ClosedWorkorderID = closedworkorder.ClosedWorkOrderID;


                //FormLoadInputForWorkorder = await _formLoadInputService.GetFormLoadInputForBarcode(UserID, AppSettings.WorkorderDetailFormName);
                await SetTitlesPropertiesForPage();
                response = await _closeWorkorderService.ClosedWorkOrders(this.ClosedWorkorderID.ToString(), this.UserID);

                if (response != null && response.clWorkOrderWrapper != null && response.clWorkOrderWrapper.clworkOrder != null)
                {
                    //Check for task or Inspection//////////
                    Application.Current.Properties["TaskOrInspection"] = response.clWorkOrderWrapper._IsWorkOrderHasTaskORInspection;



                    bool fdasignatureKey = AppSettings.User.blackhawkLicValidator.IsFDASignatureValidation;

                    if (fdasignatureKey == true)
                    {
                        if (AppSettings.User.RequireSignaturesForValidation == "True")
                        {
                            SignaturesIsVisible = true;
                            if (response.clWorkOrderWrapper.SignatureAuditDetails != null)
                            {
                                foreach (var item in response.clWorkOrderWrapper.SignatureAuditDetails)
                                {
                                    SignatureText += item.Signature + "                     " + item.SignatureTimestamp + "                             " + item.SignatureIntent + Environment.NewLine;

                                }
                            }


                        }
                    }
                    await SetControlsPropertiesForPage(response.clWorkOrderWrapper.clworkOrder);
                    if (response.clWorkOrderWrapper.Causes != null && response.clWorkOrderWrapper.Causes.Count > 0)
                    {

                        CauseName = response.clWorkOrderWrapper.Causes[0].CauseNumber;
                    }
                }
                //  FormControlsAndRights = await _formLoadInputService.GetFormControlsAndRights(UserID, AppSettings.WorkorderModuleName);
                await CreateControlsForPage();
                if (Device.Idiom == TargetIdiom.Phone)
                {
                    this.IsCostLayoutIsVisibleForPhone = true;
                }
                else
                {
                    this.IsCostLayoutIsVisibleForTab = true;
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

        public ClosedWorkorderDetailsPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, ICloseWorkorderService closeWorkorderService, INavigationService navigationService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _closeWorkorderService = closeWorkorderService;
            _navigationService = navigationService;
        }

        public async Task SetTitlesPropertiesForPage()
        {
            try
            {
                CurrentRuntimeTitle = WebControlTitle.GetTargetNameByTitleName("CurrentRuntime");
                PageTitle = WebControlTitle.GetTargetNameByTitleName("Details");
                WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                //SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName(titles, "SelectOptionsTitles"); 

                SectionNameTitle = WebControlTitle.GetTargetNameByTitleName("RiskQuestion");
                TotalTimeTilte = WebControlTitle.GetTargetNameByTitleName("TotalTime");
                DescriptionTitle = WebControlTitle.GetTargetNameByTitleName("Description");
                AdditionalDetailsTitle = WebControlTitle.GetTargetNameByTitleName("Notes");
                InternalNoteTitle = WebControlTitle.GetTargetNameByTitleName("InternalNote");
                RequiredDateTitle = WebControlTitle.GetTargetNameByTitleName("RequiredDate");
                WorkStartedDateTitle = WebControlTitle.GetTargetNameByTitleName("WorkStartedDate");
                WorkorderCompletionDateTitle = WebControlTitle.GetTargetNameByTitleName("CompletionDate");

                FacilityTitle = WebControlTitle.GetTargetNameByTitleName("Facility");
                LocationTitle = WebControlTitle.GetTargetNameByTitleName("Location");
                AssetsTitle = WebControlTitle.GetTargetNameByTitleName("Asset");
                AssetSystemTitle = WebControlTitle.GetTargetNameByTitleName("AssetSystem");
                AssignToEmployeeTitle = WebControlTitle.GetTargetNameByTitleName("Coordinator");
                WorkorderRequesterTitle = WebControlTitle.GetTargetNameByTitleName("WorkorderRequester");
                CostCenterTitle = WebControlTitle.GetTargetNameByTitleName("CostCenter");
                PriorityTitle = WebControlTitle.GetTargetNameByTitleName("Priority");
                ShiftTitle = WebControlTitle.GetTargetNameByTitleName("Shift");
                WorkorderStatusTitle = WebControlTitle.GetTargetNameByTitleName("WorkorderStatus");
                WorkorderTypeTitle = WebControlTitle.GetTargetNameByTitleName("WorkorderType");
                CauseTitle = WebControlTitle.GetTargetNameByTitleName("Cause");
                MaintenanceCodeTitle = WebControlTitle.GetTargetNameByTitleName("MaintenanceCode");

                EstimstedDowntimeTitle = WebControlTitle.GetTargetNameByTitleName("EstimatedDowntime");
                ActualDowntimeTitle = WebControlTitle.GetTargetNameByTitleName("ActualDowntime");
                MiscellaneousLabourCostTitle = WebControlTitle.GetTargetNameByTitleName("MiscellaneousLaborCost");
                MiscellaneousMaterialCostTitle = WebControlTitle.GetTargetNameByTitleName("MiscellaneousMaterialsCost");
                WorkorderNumbeTitle = WebControlTitle.GetTargetNameByTitleName("WorkorderNumber");
                JobNumberTitle = WebControlTitle.GetTargetNameByTitleName("JobNumber");
                InternalNoteTitle = WebControlTitle.GetTargetNameByTitleName("InternalNote");

                ActivationDateTitle = WebControlTitle.GetTargetNameByTitleName("ActivationDate");
                ClosedDateTitle = WebControlTitle.GetTargetNameByTitleName("ClosedDate");
                ConfirmEmailTitle = WebControlTitle.GetTargetNameByTitleName("ConfirmEmail");

                DigitalSignaturesTitle = WebControlTitle.GetTargetNameByTitleName("DigitalSignatures");
                JobNumberTitle = WebControlTitle.GetTargetNameByTitleName("JobNumber");
                ProjectTitle = WebControlTitle.GetTargetNameByTitleName("project");
                TargetTitle = WebControlTitle.GetTargetNameByTitleName("LocationHierarchy");
                RequesterFullNameTitle = WebControlTitle.GetTargetNameByTitleName("RequesterFullName");
                RequesterEmailTitle = WebControlTitle.GetTargetNameByTitleName("RequesterEmail");
                RequesterPhoneTitle = WebControlTitle.GetTargetNameByTitleName("RequesterPhone");
                RequestNumberTitle = WebControlTitle.GetTargetNameByTitleName("RequestNumber");
                RequiredDateTitle = WebControlTitle.GetTargetNameByTitleName("RequiredDate");
                WorkRequestedDateTitle = WebControlTitle.GetTargetNameByTitleName("RequestedDate");

                UserField1Title = WebControlTitle.GetTargetNameByTitleName("UserField1");
                UserField2Title = WebControlTitle.GetTargetNameByTitleName("UserField2");
                UserField3Title = WebControlTitle.GetTargetNameByTitleName("UserField3");
                UserField4Title = WebControlTitle.GetTargetNameByTitleName("UserField4");
                UserField5Title = WebControlTitle.GetTargetNameByTitleName("UserField5");
                UserField6Title = WebControlTitle.GetTargetNameByTitleName("UserField6");
                UserField7Title = WebControlTitle.GetTargetNameByTitleName("UserField7");
                UserField8Title = WebControlTitle.GetTargetNameByTitleName("UserField8");
                UserField9Title = WebControlTitle.GetTargetNameByTitleName("UserField9");
                UserField10Title = WebControlTitle.GetTargetNameByTitleName("UserField10");
                UserField11Title = WebControlTitle.GetTargetNameByTitleName("UserField11");
                UserField12Title = WebControlTitle.GetTargetNameByTitleName("UserField12");
                UserField13Title = WebControlTitle.GetTargetNameByTitleName("UserField13");
                UserField14Title = WebControlTitle.GetTargetNameByTitleName("UserField14");
                UserField15Title = WebControlTitle.GetTargetNameByTitleName("UserField15");
                UserField16Title = WebControlTitle.GetTargetNameByTitleName("UserField16");
                UserField17Title = WebControlTitle.GetTargetNameByTitleName("UserField17");
                UserField18Title = WebControlTitle.GetTargetNameByTitleName("UserField18");
                UserField19Title = WebControlTitle.GetTargetNameByTitleName("UserField19");
                UserField20Title = WebControlTitle.GetTargetNameByTitleName("UserField20");
                UserField21Title = WebControlTitle.GetTargetNameByTitleName("UserField21");
                UserField22Title = WebControlTitle.GetTargetNameByTitleName("UserField22");
                UserField23Title = WebControlTitle.GetTargetNameByTitleName("UserField23");
                UserField24Title = WebControlTitle.GetTargetNameByTitleName("UserField24");
                ApprovalLevelTitle = WebControlTitle.GetTargetNameByTitleName("ApprovalLevel");
                ApprovalNumberTitle = WebControlTitle.GetTargetNameByTitleName("ApprovalNumber");
                MoreText = WebControlTitle.GetTargetNameByTitleName("More");
                SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                Signatures = WebControlTitle.GetTargetNameByTitleName("Signatures");
                ChargeCostsOnlyToChildAssets = WebControlTitle.GetTargetNameByTitleName("ChargeCostsOnlyToChildAssets");
                ParentCostsOnly = WebControlTitle.GetTargetNameByTitleName("Chargecosttotheparentsystemandchildassets");
                DistributeCostforAssetsystem = WebControlTitle.GetTargetNameByTitleName("DistributeCostforAssetsystem");
                if (DistributeCostforAssetsystem == null)
                {
                    IsCostLayoutIsVisible = false;
                }

                


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
            #region Extract Details control
            if (Application.Current.Properties.ContainsKey("ClosedWorkorderInternalNotesKey"))
            {
                var InternalNotes = Application.Current.Properties["ClosedWorkorderInternalNotesKey"].ToString();
                if (InternalNotes == "E" || InternalNotes == "V")
                {
                    this.InternalNoteIsVisible = true;
                }
                else
                {
                    this.InternalNoteIsVisible = false;
                }
            }
            if (Application.Current.Properties.ContainsKey("ClosedWorkorderAdditionalDetailsTabKey"))
            {
                var Addtionaldetails = Application.Current.Properties["ClosedWorkorderAdditionalDetailsTabKey"].ToString();
                if (Addtionaldetails == "E" || Addtionaldetails == "V")
                {
                    this.AdditionalDetailsIsVisible = true;
                }
                else
                {
                    this.AdditionalDetailsIsVisible = false;
                }
            }
            if (Application.Current.Properties.ContainsKey("ClosedWorkorderCauseTabKey"))
            {
                var CauseTab = Application.Current.Properties["ClosedWorkorderCauseTabKey"].ToString();
                if (CauseTab == "E" || CauseTab == "V")
                {
                    this.CauseIsVisible = true;
                }
                else
                {
                    this.CauseIsVisible = false;
                }
            }

            if (Application.Current.Properties.ContainsKey("DistributeCost"))
            {
                if (DistributeCostforAssetsystem == null)
                {
                    IsCostLayoutIsVisible = false;
                    return;
                }
                var WorkorderDIstributeCost = Application.Current.Properties["DistributeCost"].ToString();
                if (WorkorderDIstributeCost != null && WorkorderDIstributeCost == "E")
                {

                    this.IsCostLayoutIsVisible = true;
                }
                else if (WorkorderDIstributeCost != null && WorkorderDIstributeCost == "V")
                {
                    this.IsCostLayoutIsEnable = false;
                }
                else
                {
                    this.IsCostLayoutIsVisible = false;
                }
            }


            if (AppSettings.User.blackhawkLicValidator.ServiceRequestIsEnabled.Equals(false))
            {

                this.WorkorderRequesterLayout = false;
            }
            if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Professional"))
            {
                this.AssetSystemLayoutIsVisible = false;
                this.CostCenterLayoutIsVisible = false;
                this.ShiftLayoutIsVisible = false;
            }
            if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic"))
            {
                this.LocationLayoutIsVisible = false;
                this.AssetSystemLayoutIsVisible = false;
                this.CostCenterLayoutIsVisible = false;
                this.ShiftLayoutIsVisible = false;
                // this.MiscellaneousLabourCostLayoutIsVisible = false;
                this.MiscellaneousMaterialCostLayout = false;
                this.WorkorderRequesterLayout = false;
                this.RequestedDateIsVisible = false;
                this.RequestedNumberIsVisible = false;
            }








            #endregion
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

        public async Task OnViewAppearingAsync(VisualElement view)
        {
            try
            {

                try
                {


                    OperationInProgress = true;

                    /// here perform tasks
                    /// 

                    //response.clWorkOrderWrapper.clworkOrder

                    var response = await _closeWorkorderService.ClosedWorkOrders(this.ClosedWorkorderID.ToString(), this.UserID);

                    if (response != null && response.clWorkOrderWrapper != null && response.clWorkOrderWrapper.clworkOrder != null)
                    {

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
            catch (Exception ex)
            {
                OperationInProgress = false;
            }
            finally
            {
                OperationInProgress = false;
            }
        }

        public async Task SetControlsPropertiesForPage(ClosedWorkOrder closeWorkorder)
        {

            var workorder = closeWorkorder;
            if (workorder.AssetID == null || workorder.AssetID == 0)
            {
                CurrentRuntimeIsVisible = false;

            }
            this.CurrentRuntimeText = workorder.CurrentRuntime;
            this.WorkorderNumberText = workorder.WorkOrderNumber;
            this.JobNumberText = workorder.JobNumber;
            this.DescriptionText = workorder.Description;

            if (workorder.Description.Length >= 150)
            {
                MoreDescriptionTextIsEnable = true;
            }
            this.DescriptionMoreText = workorder.Description;
            this.DescriptionText = workorder.Description;


            this.TotalTimeText = workorder.TotalTime;
           // this.AdditionalDetailsText = workorder.AdditionalDetails;
            if (workorder.AdditionalDetails.Length >= 150)
            {
                MoreAdditionalDetailsTextIsEnable = true;
            }
            this.AdditionalDetailsMoreText = workorder.AdditionalDetails;
            this.AdditionalDetailsText = workorder.AdditionalDetails;

            

            this.RequiredDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorder.RequiredDate ?? DateTime.Now).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);

            this.InternalNoteText = workorder.InternalNote;
            if (workorder.InternalNote.Length >= 150)
            {
                MoreInternalNoteTextIsEnable = true;
            }
            this.InternalNoteMoreText = workorder.InternalNote;
            this.InternalNoteText = workorder.InternalNote;

            /// Workorder Start Date Property Set
            /// 

            if (workorder.DistributeCost == true)
            {
                IsCostDistributed = true;
            }

            if (workorder.ParentandChildCost == true)
            {
                ParentCostDistributed = true;
            }

            if (workorder.ChildCost == true)
            {
                ChildCostDistributed = true;
            }

            if (workorder.WorkStartedDate == null)
            {
                this.WorkStartedDate = null;
                this.MaximumWorkStartedDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);

            }
            else
            {
                this.WorkStartedDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorder.WorkStartedDate ?? DateTime.Now).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                this.MaximumWorkStartedDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);

            }

            /// Workorder Completion Date Property Set
            if (workorder.CompletionDate == null)
            {
                this.WorkorderCompletionDate = null;
                this.MaximumWorkorderCompletionDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);

            }
            else
            {
                this.WorkorderCompletionDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorder.CompletionDate ?? DateTime.Now).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                this.MaximumWorkorderCompletionDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);

            }


            /// Set Targets and Other
            /// 
            string targetname = string.Empty;
            if (!string.IsNullOrWhiteSpace(workorder.AssetName))
            {
                targetname = "Assets";
            }
            else if (!string.IsNullOrWhiteSpace(workorder.AssetSystemName))
            {
                targetname = "Asset System";
            }
            else
            {
                targetname = "Location";
            }

            //Set Target String/////////
            TargetName = "(" + targetname + ")" + " " + AppSettings.User.CompanyName + " " + ">>" + " " + workorder.FacilityName + ">>" + workorder.LocationName;


            if (!string.IsNullOrEmpty(workorder.FacilityName))
            {
                FacilityName = ShortString.shorten(workorder.FacilityName);
            }
            else
            {
                FacilityName = workorder.FacilityName;
            }
            if (!string.IsNullOrEmpty(workorder.LocationName))
            {
                LocationName = ShortString.shorten(workorder.LocationName);
            }
            else
            {
                LocationName = workorder.LocationName;
            }
            if (!string.IsNullOrEmpty(workorder.AssetName))
            {
                AssetName = ShortString.shorten(workorder.AssetName);
            }
            else
            {
                AssetName = workorder.AssetName;
            }
            AssetID = workorder.AssetID;

            if (!string.IsNullOrEmpty(workorder.AssetSystemName))
            {
                AssetSystemName = ShortString.shorten(workorder.AssetSystemName);
                ShowAssociatedAssets = true;
            }
            else
            {
                AssetSystemName = workorder.AssetSystemName;

            }

            if (!string.IsNullOrEmpty(workorder.AssetSystemName))
            {
                AssetSystemName = ShortString.shorten(workorder.AssetSystemName);
            }
            else
            {
                AssetSystemName = workorder.AssetSystemName;
            }
            if (!string.IsNullOrEmpty(workorder.CostCenterName))
            {
                CostCenterName = ShortString.shorten(workorder.CostCenterName);
            }
            else
            {
                CostCenterName = workorder.CostCenterName;
            }



            if (!string.IsNullOrEmpty(workorder.WorkOrderRequesterName))
            {
                WorkorderRequesterName = ShortString.shorten(workorder.WorkOrderRequesterName);
            }
            else
            {
                WorkorderRequesterName = workorder.WorkOrderRequesterName;
            }

            if (!string.IsNullOrEmpty(workorder.ShiftName))
            {
                ShiftName = ShortString.shorten(workorder.ShiftName);
            }
            else
            {
                ShiftName = workorder.ShiftName;
            }

            if (!string.IsNullOrEmpty(workorder.WorkOrderStatusName))
            {
                WorkorderStatusName = ShortString.shorten(workorder.WorkOrderStatusName);
            }
            else
            {
                WorkorderStatusName = workorder.WorkOrderStatusName;
            }

            if (!string.IsNullOrEmpty(workorder.WorkTypeName))
            {
                WorkorderTypeName = ShortString.shorten(workorder.WorkTypeName);
            }
            else
            {
                WorkorderTypeName = workorder.WorkTypeName;
            }

            if (!string.IsNullOrEmpty(workorder.MaintenanceCodeName))
            {
                MaintenanceCodeName = ShortString.shorten(workorder.MaintenanceCodeName);
            }
            else
            {
                MaintenanceCodeName = workorder.MaintenanceCodeName;
            }

            if (!string.IsNullOrEmpty(workorder.PriorityName))
            {
                PriorityName = ShortString.shorten(workorder.PriorityName);
            }
            else
            {
                PriorityName = workorder.PriorityName;
            }

            if (!string.IsNullOrWhiteSpace(workorder.SectionName))
            {
                this.SectionNameText = workorder.SectionName;
            }
            AssignToEmployeeName = workorder.AssignToEmployee;


            EstimstedDowntimeText = decimal.Parse(string.Format(StringFormat.NumericZero(), workorder.EstimatedDowntime == null ? 0 : workorder.EstimatedDowntime));


            ActualDowntimeText = decimal.Parse(string.Format(StringFormat.NumericZero(), workorder.ActualDowntime == null ? 0 : workorder.ActualDowntime));

            MiscellaneousLabourCostText = string.Format(StringFormat.CurrencyZero(), workorder.MiscellaneousLaborCost == null ? 0 : workorder.MiscellaneousLaborCost);
            MiscellaneousMaterialCostText = string.Format(StringFormat.CurrencyZero(), workorder.MiscellaneousMaterialsCost == null ? 0 : workorder.MiscellaneousMaterialsCost);

            #region Set Dyanmic Field Properties



            #region User Fields

            UserField1 = workorder.UserField1;
            UserField2 = workorder.UserField2;
            UserField3 = workorder.UserField3;
            UserField4 = workorder.UserField4;
            UserField5 = workorder.UserField5;
            UserField6 = workorder.UserField6;
            UserField7 = workorder.UserField7;
            UserField8 = workorder.UserField8;
            UserField9 = workorder.UserField9;
            UserField10 = workorder.UserField10;
            UserField11 = workorder.UserField11;
            UserField12 = workorder.UserField12;
            UserField13 = workorder.UserField13;
            UserField14 = workorder.UserField14;
            UserField15 = workorder.UserField15;
            UserField16 = workorder.UserField16;
            UserField17 = workorder.UserField17;
            UserField18 = workorder.UserField18;
            UserField19 = workorder.UserField19;
            UserField20 = workorder.UserField20;
            UserField21 = workorder.UserField21;
            UserField22 = workorder.UserField22;
            UserField23 = workorder.UserField23;
            UserField24 = workorder.UserField24;

            workorder.DistributeCost = IsCostDistributed;
            workorder.ParentandChildCost = ParentCostDistributed;
            workorder.ChildCost = ChildCostDistributed;
            ApprovalLevel = workorder.ApprovalLevel;
            ApprovalNumber = workorder.ApprovalNumber;

            #endregion


            #region Dynamic properties
            // this.WorkorderCompletionDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorder.CompletionDate ?? DateTime.Now).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
            ActivationDateText = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorder.ActivationDate ?? DateTime.Now).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
            //ActivationDateText = workorder.ActivationDate.ToString();
            ClosedDateText = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorder.ClosedDate ?? DateTime.Now).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);

            //  ClosedDateText = workorder.ClosedDate.ToString();
            ConfirmEmail = workorder.ConfirmEmail;

            DigitalSignatures = workorder.DigitalSignatures;
            JobNumber = workorder.JobNumber;
            Project = workorder.project;

            RequesterFullName = workorder.RequesterFullName;
            RequesterEmail = workorder.RequesterEmail;
            RequesterPhone = workorder.RequesterPhone;
            RequestNumber = workorder.RequestNumber;
            // RequestedDate = workorder.RequestedDate.ToString();
            RequestedDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorder.RequestedDate ?? DateTime.Now).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);

            if (AppSettings.User.blackhawkLicValidator.ServiceRequestIsEnabled.Equals(false))
            {
                this.RequestedDateIsVisible = false;
                this.RequestedNumberIsVisible = false;
                this.RequesterEmailLayout = false;
            }
            else
            {
                if (workorder.RequestNumber == null && workorder.RequestedDate == null)
                {
                    this.RequestedDateIsVisible = false;
                    this.RequestedNumberIsVisible = false;
                    this.RequesterEmailLayout = false;
                }
            }
            if (AppSettings.User.blackhawkLicValidator.RiskAssasment.Equals(false))
            {
                this.SectionNameIsVisible = false;
            }


            #endregion







            #endregion

        }

        public Task OnViewDisappearingAsync(VisualElement view)
        {
            return Task.FromResult(true);
        }

        public async Task ShowMoreDescription()
        {

            try
            {
                UserDialogs.Instance.HideLoading();

                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.Description = AdditionalDetailsMoreText;
                await NavigationService.NavigateToAsync<DescriptionViewModel>(tnobj);
                // await Page.DisplayActionSheet(" ", WebControlTitle.GetTargetNameByTitleName("Cancel"), null, AdditionalDetailsText); 
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
        public async Task ShowSignatures()
        {

            try
            {
                UserDialogs.Instance.HideLoading();

                TargetNavigationData tnobj = new TargetNavigationData();
                if (response.clWorkOrderWrapper != null && response.clWorkOrderWrapper.SignatureAuditDetails != null)
                {

                    tnobj.SignatureAuditDetails = response.clWorkOrderWrapper.SignatureAuditDetails;
                }

                await NavigationService.NavigateToAsync<SignatureHistoryViewModel>(tnobj.SignatureAuditDetails);
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
        public async Task ShowLongDescription()
        {

            try
            {
                UserDialogs.Instance.HideLoading();

                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.Description = DescriptionMoreText;
                await NavigationService.NavigateToAsync<DescriptionViewModel>(tnobj);
                // await Page.DisplayActionSheet(" ", WebControlTitle.GetTargetNameByTitleName("Cancel"), null, AdditionalDetailsText); 
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

        public async Task ShowInternalNotesDescription()
        {

            try
            {
                UserDialogs.Instance.HideLoading();

                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.Description = InternalNoteMoreText;
                await NavigationService.NavigateToAsync<DescriptionViewModel>(tnobj);
                // await Page.DisplayActionSheet(" ", WebControlTitle.GetTargetNameByTitleName("Cancel"), null, AdditionalDetailsText); 
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
    }
}
