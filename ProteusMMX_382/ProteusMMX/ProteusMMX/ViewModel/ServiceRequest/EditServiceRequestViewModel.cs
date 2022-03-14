using Acr.UserDialogs;
using NodaTime;
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
using ProteusMMX.Model.ServiceRequestModel;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Asset;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.ServiceRequest;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.SelectionListPagesViewModels;
using ProteusMMX.ViewModel.SelectionListPagesViewModels.Asset;
using ProteusMMX.Views.Common;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProteusMMX.ViewModel.ServiceRequest
{
    public class EditServiceRequestViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        #region Fields
        ServiceOutput ServiceRequestWrapper;

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly IServiceRequestModuleService _serviceRequestService;

        protected readonly IWorkorderService _workorderService;
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



        List<FormControl> _serviceRequestControlsNew = new List<FormControl>();
        public List<FormControl> ServiceRequestControlsNew
        {
            get
            {
                return _serviceRequestControlsNew;
            }

            set
            {
                _serviceRequestControlsNew = value;
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

        string _tagTypeSelectedPickerText;
        public string TagTypeSelectedPickerText
        {
            get
            {
                return _tagTypeSelectedPickerText;
            }

            set
            {
                if (value != _tagTypeSelectedPickerText)
                {
                    _tagTypeSelectedPickerText = value;
                    OnPropertyChanged("TagTypeSelectedPickerText");
                }
            }
        }
        string _tagTypeLabelTitle;
        public string TagTypeLabelTitle
        {
            get
            {
                return _tagTypeLabelTitle;
            }

            set
            {
                if (value != _tagTypeLabelTitle)
                {
                    _tagTypeLabelTitle = value;
                    OnPropertyChanged("TagTypeLabelTitle");
                }
            }
        }

        ObservableCollection<string> _tagTypePickerTitles;

        public ObservableCollection<string> TagTypePickerTitles
        {
            get
            {
                return _tagTypePickerTitles;
            }

            set
            {
                if (value != _tagTypePickerTitles)
                {
                    _tagTypePickerTitles = value;
                    OnPropertyChanged("TagTypePickerTitles");

                }
            }

        }

        int _tagTypeSelectedIndexPicker = -1;
        public int TagTypeSelectedIndexPicker
        {
            get
            {
                return _tagTypeSelectedIndexPicker;
            }

            set
            {
                _tagTypeSelectedIndexPicker = value;
                OnPropertyChanged("TagTypeSelectedIndexPicker");
                RefillTagTypeFromPicker();


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
        string _acceptTitle = "";
        public string AcceptTitle
        {
            get
            {
                return _acceptTitle;
            }

            set
            {
                if (value != _acceptTitle)
                {
                    _acceptTitle = value;
                    OnPropertyChanged("AcceptTitle");
                }
            }
        }

        string _declineTitle = "";
        public string DeclineTitle
        {
            get
            {
                return _declineTitle;
            }

            set
            {
                if (value != _declineTitle)
                {
                    _declineTitle = value;
                    OnPropertyChanged("DeclineTitle");
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



        #region EditServiceRequest Properties





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

        // Required Date
        DateTime _requiredDate1;
        public DateTime RequiredDate1
        {
            get
            {
                return _requiredDate1;
            }

            set
            {
                if (value != _requiredDate1)
                {
                    _requiredDate1 = value;
                    OnPropertyChanged(nameof(RequiredDate1));
                }
            }
        }

        DateTime _minimumRequiredDate=DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);
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
        string _administrator;
        public string Administrator
        {
            get
            {
                return _administrator;
            }

            set
            {
                if (value != _administrator)
                {
                    _administrator = value;
                    OnPropertyChanged(nameof(Administrator));
                }
            }
        }
        string _autoSendToMobile;
        public string AutoSendToMobile
        {
            get
            {
                return _autoSendToMobile;
            }

            set
            {
                if (value != _autoSendToMobile)
                {
                    _autoSendToMobile = value;
                    OnPropertyChanged(nameof(AutoSendToMobile));
                }
            }
        }

        string _requesterConfirmEmail;
        public string RequesterConfirmEmail
        {
            get
            {
                return _requesterConfirmEmail;
            }

            set
            {
                if (value != _requesterConfirmEmail)
                {
                    _requesterConfirmEmail = value;
                    OnPropertyChanged(nameof(RequesterConfirmEmail));
                }
            }
        }

        string _estimatedDowntime;
        public string EstimatedDowntime
        {
            get
            {
                return _estimatedDowntime;
            }

            set
            {
                if (value != _estimatedDowntime)
                {
                    _estimatedDowntime = value;
                    OnPropertyChanged(nameof(EstimatedDowntime));
                }
            }
        }
        string _sendSMSToEmployee;
        public string SendSMSToEmployee
        {
            get
            {
                return _sendSMSToEmployee;
            }

            set
            {
                if (value != _sendSMSToEmployee)
                {
                    _sendSMSToEmployee = value;
                    OnPropertyChanged(nameof(SendSMSToEmployee));
                }
            }
        }
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
                    OnPropertyChanged("AssetsTitle");
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
        int? _serviceRequestID;
        public int? ServiceRequestID
        {
            get
            {
                return _serviceRequestID;
            }

            set
            {
                if (value != _serviceRequestID)
                {
                    _serviceRequestID = value;
                    OnPropertyChanged(nameof(ServiceRequestID));
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

        string _administratorTitle;
        public string AdministratorTitle
        {
            get
            {
                return _administratorTitle;
            }

            set
            {
                if (value != _administratorTitle)
                {
                    _administratorTitle = value;
                    OnPropertyChanged(nameof(AdministratorTitle));
                }
            }
        }

        string _administratorName;
        public string AdministratorName
        {
            get
            {
                return _administratorName;
            }

            set
            {
                if (value != _administratorName)
                {
                    _administratorName = value;
                    OnPropertyChanged(nameof(AdministratorName));
                }
            }
        }
        // AdministratorID
        int? _administratorID;
        public int? AdministratorID
        {
            get
            {
                return _administratorID;
            }

            set
            {
                if (value != _administratorID)
                {
                    _administratorID = value;
                    OnPropertyChanged(nameof(AdministratorID));
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

        bool _tagTypeIsVisible = true;
        public bool TagTypeIsVisible
        {
            get
            {
                return _tagTypeIsVisible;
            }

            set
            {
                if (value != _tagTypeIsVisible)
                {
                    _tagTypeIsVisible = value;
                    OnPropertyChanged(nameof(TagTypeIsVisible));
                }
            }
        }

        
        bool _AssetIdIsVisible = true;
        public bool AssetIdIsVisible
        {
            get
            {
                return _AssetIdIsVisible;
            }

            set
            {
                if (value != _AssetIdIsVisible)
                {
                    _AssetIdIsVisible = value;
                    OnPropertyChanged(nameof(AssetIdIsVisible));
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
        bool _tagTypeIsEnabled = true;
        public bool TagTypeIsEnabled
        {
            get
            {
                return _tagTypeIsEnabled;
            }

            set
            {
                if (value != _tagTypeIsEnabled)
                {
                    _tagTypeIsEnabled = value;
                    OnPropertyChanged(nameof(TagTypeIsEnabled));
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

        Color _colorBackground;
        public Color ColorBackground
        {
            get
            {
                return _colorBackground;
            }

            set
            {
                if (value != _colorBackground)
                {
                    _colorBackground = value;
                    OnPropertyChanged(nameof(ColorBackground));
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


        DateTime? _installationDate;
        public DateTime? InstallationDate
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


        int? _originalCost;
        public int? OriginalCost
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


        DateTime? _warrantyDate;
        public DateTime? WarrantyDate
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


        #region RightsProperties

        
        string Accept;
        string Decline;
        string Edit;

        bool _assignToEmployeeIsVisible = false;
        public bool AssignToEmployeeIsVisible
        {
            get
            {
                return _assignToEmployeeIsVisible;
            }

            set
            {
                if (value != _assignToEmployeeIsVisible)
                {
                    _assignToEmployeeIsVisible = value;
                    OnPropertyChanged(nameof(AssignToEmployeeIsVisible));
                }
            }
        }
        bool _workorderRequesterIsVisible = false;
        public bool WorkorderRequesterIsVisible
        {
            get
            {
                return _workorderRequesterIsVisible;
            }

            set
            {
                if (value != _workorderRequesterIsVisible)
                {
                    _workorderRequesterIsVisible = value;
                    OnPropertyChanged(nameof(WorkorderRequesterIsVisible));
                }
            }
        }
        bool _costCenterIsVisible = false;
        public bool CostCenterIsVisible
        {
            get
            {
                return _costCenterIsVisible;
            }

            set
            {
                if (value != _costCenterIsVisible)
                {
                    _costCenterIsVisible = value;
                    OnPropertyChanged(nameof(CostCenterIsVisible));
                }
            }
        }
        bool _priorityIsVisible = false;
        public bool PriorityIsVisible
        {
            get
            {
                return _priorityIsVisible;
            }

            set
            {
                if (value != _priorityIsVisible)
                {
                    _priorityIsVisible = value;
                    OnPropertyChanged(nameof(PriorityIsVisible));
                }
            }
        }
        bool _shiftIsVisible = false;
        public bool ShiftIsVisible
        {
            get
            {
                return _shiftIsVisible;
            }

            set
            {
                if (value != _shiftIsVisible)
                {
                    _shiftIsVisible = value;
                    OnPropertyChanged(nameof(ShiftIsVisible));
                }
            }
        }
        bool _workorderStatusIsVisible = false;
        public bool WorkorderStatusIsVisible
        {
            get
            {
                return _workorderStatusIsVisible;
            }

            set
            {
                if (value != _workorderStatusIsVisible)
                {
                    _workorderStatusIsVisible = value;
                    OnPropertyChanged(nameof(WorkorderStatusIsVisible));
                }
            }
        }
        bool _workorderTypeIsVisible = false;
        public bool WorkorderTypeIsVisible
        {
            get
            {
                return _workorderTypeIsVisible;
            }

            set
            {
                if (value != _workorderTypeIsVisible)
                {
                    _workorderTypeIsVisible = value;
                    OnPropertyChanged(nameof(WorkorderTypeIsVisible));
                }
            }
        }

        bool _assetSystemIsVisibleForLicensing = true;
        public bool AssetSystemIsVisibleForLicensing
        {
            get
            {
                return _assetSystemIsVisibleForLicensing;
            }

            set
            {
                if (value != _assetSystemIsVisibleForLicensing)
                {
                    _assetSystemIsVisibleForLicensing = value;
                    OnPropertyChanged(nameof(AssetSystemIsVisibleForLicensing));
                }
            }
        }
        bool _maintenanceCodeIsVisible = false;
        public bool MaintenanceCodeIsVisible
        {
            get
            {
                return _maintenanceCodeIsVisible;
            }

            set
            {
                if (value != _maintenanceCodeIsVisible)
                {
                    _maintenanceCodeIsVisible = value;
                    OnPropertyChanged(nameof(MaintenanceCodeIsVisible));
                }
            }
        }

        bool _serviceRequestIsEnabled = true;
        public bool ServiceRequestIsEnabled
        {
            get
            {
                return _serviceRequestIsEnabled;
            }

            set
            {
                if (value != _serviceRequestIsEnabled)
                {
                    _serviceRequestIsEnabled = value;
                    OnPropertyChanged(nameof(ServiceRequestIsEnabled));
                }
            }
        }
        bool _serviceRequestIsVisible = true;
        public bool ServiceRequestIsVisible
        {
            get
            {
                return _serviceRequestIsVisible;
            }

            set
            {
                if (value != _serviceRequestIsVisible)
                {
                    _serviceRequestIsVisible = value;
                    OnPropertyChanged(nameof(ServiceRequestIsVisible));
                }
            }
        }

        bool _administratorIsEnable = true;
        public bool AdministratorIsEnable
        {
            get
            {
                return _administratorIsEnable;
            }

            set
            {
                if (value != _administratorIsEnable)
                {
                    _administratorIsEnable = value;
                    OnPropertyChanged(nameof(AdministratorIsEnable));
                }
            }
        }

        bool _administratoreIsVisible = true;
        public bool AdministratorIsVisible
        {
            get
            {
                return _administratoreIsVisible;
            }

            set
            {
                if (value != _administratoreIsVisible)
                {
                    _administratoreIsVisible = value;
                    OnPropertyChanged(nameof(AdministratorIsVisible));
                }
            }
        }

        bool _requiredDateIsVisible = true;
        public bool RequiredDateIsVisible
        {
            get
            {
                return _requiredDateIsVisible;
            }

            set
            {
                if (value != _requiredDateIsVisible)
                {
                    _requiredDateIsVisible = value;
                    OnPropertyChanged(nameof(RequiredDateIsVisible));
                }
            }
        }
        #endregion

        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);

        public ICommand AdministratorCommand => new AsyncCommand(ShowAdministrator);
        public ICommand FacilityCommand => new AsyncCommand(ShowFacilities);
        public ICommand LocationCommand => new AsyncCommand(ShowLocations);
        public ICommand AssetCommand => new AsyncCommand(ShowAssets);
        public ICommand AssetSystemCommand => new AsyncCommand(ShowAssetSystem);
        public ICommand AssignToCommand => new AsyncCommand(ShowAssignTo);
        public ICommand WorkorderRequesterCommand => new AsyncCommand(ShowWorkorderRequester);
        public ICommand CostCenterCommand => new AsyncCommand(ShowCostCenter);
        public ICommand PriorityCommand => new AsyncCommand(ShowPriority);
        public ICommand ShiftCommand => new AsyncCommand(ShowShift);
        public ICommand WorkorderStatusCommand => new AsyncCommand(ShowWorkorderStatus);
        public ICommand WorkorderTypeCommand => new AsyncCommand(ShowWorkorderType);

        public ICommand MaintenanceCodeCommand => new AsyncCommand(ShowMaintenanceCode);


        //Save Command
        public ICommand SaveServiceRequestCommand => new AsyncCommand(EditServiceRequest);

        public ICommand TapCommand => new AsyncCommand(SpeechtoText);
        public ICommand TapCommand2 => new AsyncCommand(ShowMoreAdditionalDetails);

        public ICommand TapCommandSignature => new AsyncCommand(ShowSignatures);

        #endregion

        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                OperationInProgress = true;

             
                if (navigationData != null)
                {

                    //var navigationParams = navigationData as TargetNavigationData;
                    //this.ServiceRequestID = navigationParams.ServiceRequestID;
                    //this.RequestNumber= navigationParams.RequestNumber;

                    var navigationParams = navigationData as PageParameters;
                    this.Page = navigationParams.Page;

                    var servicerequest = navigationParams.Parameter as ServiceRequests;
                    this.ServiceRequestID = servicerequest.ServiceRequestID;
                    this.RequestNumber = servicerequest.RequestNumber;
                }



                await SetTitlesPropertiesForPage();
                if (Application.Current.Properties.ContainsKey("EditServiceRequestKey"))
                {
                    var EditRights = Application.Current.Properties["EditServiceRequestKey"].ToString();
                    if (EditRights != null)
                    {
                        Edit = EditRights.ToString();

                    }
                }
                if (Application.Current.Properties.ContainsKey("AcceptKey"))
                {
                    var AcceptRights = Application.Current.Properties["AcceptKey"].ToString();
                    if (AcceptRights != null)
                    {
                        Accept = AcceptRights.ToString();

                    }
                }
                if (Application.Current.Properties.ContainsKey("DEclineKey"))
                {
                    var DeclineRights = Application.Current.Properties["DEclineKey"].ToString();
                    if (DeclineRights != null)
                    {
                        Decline = DeclineRights.ToString();

                    }
                }
                if (Application.Current.Properties.ContainsKey("ServiceRequestAdditionalDetailsKey"))
                {
                    var AdditionalRights = Application.Current.Properties["ServiceRequestAdditionalDetailsKey"].ToString();
                    if (AdditionalRights != null)
                    {
                        if (AdditionalRights == "E")
                        {
                            this.AdditionalDetailsIsEnable = true;
                        }
                        else if (AdditionalRights == "V")
                        {
                            this.AdditionalDetailsIsEnable = false;
                        }
                        else
                        {
                            this.AdditionalDetailsIsVisible = false;
                        }

                    }
                }
          
              
                if (Application.Current.Properties.ContainsKey("ServiceRequestDetailsControls"))
                {
                    SubModule ServiceRequestSubModule = Application.Current.Properties["ServiceRequestDetailsControls"] as SubModule;
                    ServiceRequestControlsNew = ServiceRequestSubModule.listControls;
                }



                if (Edit == "E")
                {
                    this.ServiceRequestIsVisible = true;
                }
                else if (Edit == "V")
                {
                    this.ServiceRequestIsEnabled = false;
                }
                else
                {
                    this.ServiceRequestIsVisible = false;
                }

                if (Application.Current.Properties.ContainsKey("ServiceRequestTarget"))
                {
                    var Target = Application.Current.Properties["ServiceRequestTarget"].ToString();
                    if (Target != null && Target == "E")
                    {
                        this.AssetIdIsVisible = true;

                    }
                    else if (Target == "V")
                    {
                        this.LocationIsEnable = false;
                        this.FacilityIsEnable = false;
                        this.AssetIsEnable = false;
                        this.AssetSystemIsEnable = false;

                    }
                    else
                    {
                        AssetIdIsVisible = false;
                        // FacilityIsVisible = false;
                    }
                }



                if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic") || AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Professional"))
                {
                    AssetSystemIsVisibleForLicensing = false;
                }
                
                await SetControlsPropertiesForPage();
                await CreateControlsForPage();
                //await this.OnViewAppearingAsync(null);




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

        public EditServiceRequestViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IServiceRequestModuleService serviceRequestService, IWorkorderService workorderService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _serviceRequestService = serviceRequestService;
            _workorderService = workorderService;
        }

        public async Task SetTitlesPropertiesForPage()
        {
            try
            {
                AdministratorTitle = "Administrator";
                PageTitle = WebControlTitle.GetTargetNameByTitleName("Details");
                TagTypeLabelTitle = WebControlTitle.GetTargetNameByTitleName("TagType");
               
                //PageTitle = WebControlTitle.GetTargetNameByTitleName("ServiceRequest")+" - "+ RequestNumber;
                WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                TagTypePickerTitles = new ObservableCollection<string>() {"SHE Tag", "Maintenance Tag", "Operator Tag" };
                //DescriptionTitle = WebControlTitle.GetTargetNameByTitleName("Description");
                AdditionalDetailsTitle = WebControlTitle.GetTargetNameByTitleName("Notes");
                FacilityTitle = WebControlTitle.GetTargetNameByTitleName("Facility");
                LocationTitle = WebControlTitle.GetTargetNameByTitleName("Location");
                AssetsTitle = WebControlTitle.GetTargetNameByTitleName("Asset");
                AssetSystemTitle = WebControlTitle.GetTargetNameByTitleName("AssetSystem");
                //AssignToEmployeeTitle = WebControlTitle.GetTargetNameByTitleName("Coordinator");
                //WorkorderRequesterTitle = WebControlTitle.GetTargetNameByTitleName("WorkorderRequester");
                //CostCenterTitle = WebControlTitle.GetTargetNameByTitleName("CostCenter");
                //PriorityTitle = WebControlTitle.GetTargetNameByTitleName("Priority");
                //ShiftTitle = WebControlTitle.GetTargetNameByTitleName("Shift");
                //WorkorderStatusTitle = WebControlTitle.GetTargetNameByTitleName("WorkorderStatus");
                //WorkorderTypeTitle = WebControlTitle.GetTargetNameByTitleName("WorkorderType");
                //CauseTitle = WebControlTitle.GetTargetNameByTitleName("Cause");
                //MaintenanceCodeTitle = WebControlTitle.GetTargetNameByTitleName("MaintenanceCode");
                AcceptTitle = WebControlTitle.GetTargetNameByTitleName("Accept");
                DeclineTitle = WebControlTitle.GetTargetNameByTitleName("Decline");
                SaveTitle = WebControlTitle.GetTargetNameByTitleName("Save");
                //RequiredDateTitle = WebControlTitle.GetTargetNameByTitleName("RequiredDate");
                SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                MoreText = WebControlTitle.GetTargetNameByTitleName("More");
                Signatures = WebControlTitle.GetTargetNameByTitleName("Signatures");


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





            #endregion

            #region Apply Control Typewise ordering.

            if (ServiceRequestControlsNew != null && ServiceRequestControlsNew.Count > 0)
            {

                //    #region Logic 1

                var sortedList = new List<FormControl>();

                foreach (var item in ServiceRequestControlsNew)
                {
                    if (item.DisplayFormat == "DateTime")
                    {
                        sortedList.Add(item);
                    }

                }
                //Add Except DateTime
                foreach (var item in ServiceRequestControlsNew)
                {
                    if (item.DisplayFormat != "DateTime")
                    {
                        sortedList.Add(item);
                        if (item.ControlName == "RequesterPhone")
                        {
                            sortedList.Remove(item);
                        }
                        if (item.ControlName == "RequesterFullName")
                        {

                            sortedList.Remove(item);
                        }
                        if (item.ControlName == "RequesterEmail")
                        {
                            sortedList.Remove(item);
                        }
                        if (item.ControlName == "ConfirmEmail")
                        {
                            sortedList.Remove(item);
                        }
                    }

                }

                foreach (var item in ServiceRequestControlsNew)
                {
                    if (item.ControlName == "RequesterPhone")
                    {
                        sortedList.Add(item);
                    }
                    if (item.ControlName == "RequesterFullName")
                    {
                        sortedList.Add(item);
                    }
                    if (item.ControlName == "RequesterEmail")
                    {
                        sortedList.Add(item);
                    }
                    if (item.ControlName == "ConfirmEmail")
                    {
                        sortedList.Add(item);
                    }

                }



                //    //ReAssign to WorkorderControlsNew
                ServiceRequestControlsNew = sortedList;
                //    #endregion
               



            }
            #endregion

            #region Remove Overridden controls from titles New
            #region Remove Overridden controls from titles New
            if (ServiceRequestControlsNew != null && ServiceRequestControlsNew.Count > 0)
            {

                try
                {

                    ///Description
                    ///AssignedToEmployeeID
                    ///WorkOrderRequesterID
                    ///CostCenterID
                    ///PriorityID
                    ///ShiftID
                    ///WorkOrderStatusID
                    ///WorkTypeID
                    ///MaintenanceCodeID




                    //var Description = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "Description");
                    //if (Description != null)
                    //{
                    //    OverriddenControlsNew.Add(Description);
                    //    ServiceRequestControlsNew.Remove(Description);
                    //}

                    //var AssignedToEmployeeID = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "AssignedToEmployeeID");
                    //if (AssignedToEmployeeID != null)
                    //{
                    //    OverriddenControlsNew.Add(AssignedToEmployeeID);
                    //    ServiceRequestControlsNew.Remove(AssignedToEmployeeID);
                    //}

                    //var WorkOrderRequesterID = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "WorkOrderRequesterID");
                    //if (WorkOrderRequesterID != null)
                    //{
                    //    OverriddenControlsNew.Add(WorkOrderRequesterID);
                    //    ServiceRequestControlsNew.Remove(WorkOrderRequesterID);
                    //}

                    //var CostCenterID = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "CostCenterID");
                    //if (CostCenterID != null)
                    //{
                    //    OverriddenControlsNew.Add(CostCenterID);
                    //    ServiceRequestControlsNew.Remove(CostCenterID);
                    //}

                    //var PriorityID = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "PriorityID");
                    //if (PriorityID != null)
                    //{
                    //    OverriddenControlsNew.Add(PriorityID);
                    //    ServiceRequestControlsNew.Remove(PriorityID);
                    //}

                    //var ShiftID = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "ShiftID");
                    //if (ShiftID != null)
                    //{
                    //    OverriddenControlsNew.Add(ShiftID);
                    //    ServiceRequestControlsNew.Remove(ShiftID);
                    //}

                    //var WorkOrderStatusID = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "WorkOrderStatusID");
                    //if (WorkOrderStatusID != null)
                    //{
                    //    OverriddenControlsNew.Add(WorkOrderStatusID);
                    //    ServiceRequestControlsNew.Remove(WorkOrderStatusID);
                    //}

                    //var WorkTypeID = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "WorkTypeID");
                    //if (WorkTypeID != null)
                    //{
                    //    OverriddenControlsNew.Add(WorkTypeID);
                    //    ServiceRequestControlsNew.Remove(WorkTypeID);
                    //}




                    //var MaintenanceCodeID = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "MaintenanceCodeID");
                    //if (MaintenanceCodeID != null)
                    //{
                    //    OverriddenControlsNew.Add(MaintenanceCodeID);
                    //    ServiceRequestControlsNew.Remove(MaintenanceCodeID);
                    //}


                    var description = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "Description");
                    if (description != null)
                    {
                        DescriptionTitle = description.TargetName;
                        OverriddenControlsNew.Add(description);
                        ServiceRequestControlsNew.Remove(description);
                    }



                    var AssignedToEmployeeID = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "AssignedToEmployeeID");
                    if (AssignedToEmployeeID != null)
                    {
                        AssignToEmployeeTitle = AssignedToEmployeeID.TargetName;
                        OverriddenControlsNew.Add(AssignedToEmployeeID);
                        ServiceRequestControlsNew.Remove(AssignedToEmployeeID);
                    }

                    var WorkOrderRequesterID = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "WorkOrderRequesterID");
                    if (WorkOrderRequesterID != null)
                    {
                        WorkorderRequesterTitle = WorkOrderRequesterID.TargetName;
                        OverriddenControlsNew.Add(WorkOrderRequesterID);
                        ServiceRequestControlsNew.Remove(WorkOrderRequesterID);
                    }

                    var CostCenterID = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "CostCenterID");
                    if (CostCenterID != null)
                    {
                        CostCenterTitle = CostCenterID.TargetName;
                        OverriddenControlsNew.Add(CostCenterID);
                        ServiceRequestControlsNew.Remove(CostCenterID);
                    }

                    var PriorityID = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "PriorityID");
                    if (PriorityID != null)
                    {
                        PriorityTitle = PriorityID.TargetName;
                        OverriddenControlsNew.Add(PriorityID);
                        ServiceRequestControlsNew.Remove(PriorityID);
                    }

                    var ShiftID = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "ShiftID");
                    if (ShiftID != null)
                    {
                        ShiftTitle = ShiftID.TargetName;
                        OverriddenControlsNew.Add(ShiftID);
                        ServiceRequestControlsNew.Remove(ShiftID);
                    }

                    var WorkOrderStatusID = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "WorkOrderStatusID");
                    if (WorkOrderStatusID != null)
                    {
                        WorkorderStatusTitle = WorkOrderStatusID.TargetName;
                        OverriddenControlsNew.Add(WorkOrderStatusID);
                        ServiceRequestControlsNew.Remove(WorkOrderStatusID);
                    }

                    var WorkTypeID = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "WorkTypeID");
                    if (WorkTypeID != null)
                    {
                        WorkorderTypeTitle = WorkTypeID.TargetName;
                        OverriddenControlsNew.Add(WorkTypeID);
                        ServiceRequestControlsNew.Remove(WorkTypeID);
                    }


                    var CauseID = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "Causes");
                    if (CauseID != null)
                    {
                        CauseTitle = CauseID.TargetName;
                        OverriddenControlsNew.Add(CauseID);
                        ServiceRequestControlsNew.Remove(CauseID);
                    }


                    var MaintenanceCodeID = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "MaintenanceCodeID");
                    if (MaintenanceCodeID != null)
                    {
                        MaintenanceCodeTitle = MaintenanceCodeID.TargetName;
                        OverriddenControlsNew.Add(MaintenanceCodeID);
                        ServiceRequestControlsNew.Remove(MaintenanceCodeID);
                    }



                    var EstimatedDowntime = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "EstimatedDowntime");
                    if (EstimatedDowntime != null)
                    {
                        EstimstedDowntimeTitle = EstimatedDowntime.TargetName;
                        // OverriddenControlsNew.Add(EstimatedDowntime);
                        // ServiceRequestControlsNew.Remove(EstimatedDowntime);
                    }

                    var ActualDowntime = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "ActualDowntime");
                    if (ActualDowntime != null)
                    {
                        ActualDowntimeTitle = ActualDowntime.TargetName;
                        OverriddenControlsNew.Add(ActualDowntime);
                        ServiceRequestControlsNew.Remove(ActualDowntime);
                    }

                    var MiscellaneousLaborCostID = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "MiscellaneousLaborCostID");
                    if (MiscellaneousLaborCostID != null)
                    {
                        MiscellaneousLabourCostTitle = MiscellaneousLaborCostID.TargetName;
                        OverriddenControlsNew.Add(MiscellaneousLaborCostID);
                        ServiceRequestControlsNew.Remove(MiscellaneousLaborCostID);
                    }

                    var MiscellaneousMaterialsCostID = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "MiscellaneousMaterialsCostID");
                    if (MiscellaneousMaterialsCostID != null)
                    {
                        MiscellaneousMaterialCostTitle = MiscellaneousMaterialsCostID.TargetName;
                        OverriddenControlsNew.Add(MiscellaneousMaterialsCostID);
                        ServiceRequestControlsNew.Remove(MiscellaneousMaterialsCostID);
                    }

                    var requiredDate = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "RequiredDate");
                    if (requiredDate != null)
                    {
                        RequiredDateTitle = requiredDate.TargetName;
                        OverriddenControlsNew.Add(requiredDate);
                        ServiceRequestControlsNew.Remove(requiredDate);
                    }

                    var administrator = ServiceRequestControlsNew.FirstOrDefault(x => x.ControlName == "AdministratorID");
                    if (administrator != null)
                    {

                        AdministratorTitle = administrator.TargetName;
                        OverriddenControlsNew.Add(administrator);
                        ServiceRequestControlsNew.Remove(administrator);
                    }

                }
                catch (Exception ex)
                {


                }

            }

            #endregion

            #endregion
            #region Remove None visibility controls
            if (ServiceRequestControlsNew != null && ServiceRequestControlsNew.Count > 0)
            {
                ServiceRequestControlsNew.RemoveAll(i => i.Expression == "N");
                ServiceRequestControlsNew.RemoveAll(i => i.ControlName == "AdministratorID");
                ServiceRequestControlsNew.RemoveAll(i => i.ControlName == "AutoSendToMobile");
                ServiceRequestControlsNew.RemoveAll(i => i.ControlName == "MobileEmployeeID");
                ServiceRequestControlsNew.RemoveAll(i => i.ControlName == "Description");
                ServiceRequestControlsNew.RemoveAll(i => i.ControlName == "AssignedToEmployeeID");
                ServiceRequestControlsNew.RemoveAll(i => i.ControlName == "WorkOrderRequesterID");
                ServiceRequestControlsNew.RemoveAll(i => i.ControlName == "CostCenterID");
                ServiceRequestControlsNew.RemoveAll(i => i.ControlName == "PriorityID");
                ServiceRequestControlsNew.RemoveAll(i => i.ControlName == "ShiftID");
                ServiceRequestControlsNew.RemoveAll(i => i.ControlName == "WorkOrderStatusID");
                ServiceRequestControlsNew.RemoveAll(i => i.ControlName == "WorkTypeID");
                ServiceRequestControlsNew.RemoveAll(i => i.ControlName == "MaintenanceCodeID");
                ServiceRequestControlsNew.RemoveAll(i => i.ControlName == "RequiredDate");
                ServiceRequestControlsNew.RemoveAll(i => i.ControlName == "AdministratorID");
            }

            #endregion
            #region Apply visibility according to expression on Overridden controls New

            if (OverriddenControlsNew != null && OverriddenControlsNew.Count > 0)
            {

                try
                {
                    ///WorkOrderNumber
                    ///JobNumber
                    ///Description
                    ///RequiredDate
                    ///WorkStartedDate
                    ///CompletionDate
                    ///AssignedToEmployeeID
                    ///WorkOrderRequesterID
                    ///CostCenterID
                    ///PriorityID
                    ///ShiftID
                    ///WorkOrderStatusID
                    ///WorkTypeID
                    ///UserField22
                    ///MaintenanceCodeID

                    //var formRoles = titles.lstRoles;

                    foreach (var item in OverriddenControlsNew)
                    {
                        //var finalizedRole = await ParseControlRoleExpressionWithFormsRoles(item.Expression, formRoles);

                        switch (item.ControlName)
                        {



                            case "Description":
                                {
                                    DescriptionIsEnable = ApplyIsEnable(item.Expression);
                                    DescriptionIsVisible = ApplyIsEnable(item.Expression);
                                    break;
                                }

                            case "RequiredDate":
                                {
                                    RequiredDateIsEnable = ApplyIsEnable(item.Expression);
                                    RequiredDateIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }

                            case "AdministratorID":
                                {
                                    AdministratorIsEnable = ApplyIsEnable(item.Expression);
                                    AdministratorIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }

                            case "AssignedToEmployeeID":
                                {
                                    AssignToEmployeeIsEnable = ApplyIsEnable(item.Expression);
                                    AssignToEmployeeIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }


                            case "WorkOrderRequesterID":
                                {
                                    WorkorderRequesterIsEnable = ApplyIsEnable(item.Expression);
                                    WorkorderRequesterIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }


                            case "CostCenterID":
                                {
                                    if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic") || AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Professional"))
                                    {
                                        CostCenterIsVisible = false;
                                        break;
                                    }
                                    CostCenterIsEnable = ApplyIsEnable(item.Expression);
                                    CostCenterIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }


                            case "PriorityID":
                                {
                                    PriorityIsEnable = ApplyIsEnable(item.Expression);
                                    PriorityIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }


                            case "ShiftID":
                                {
                                    if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic") || AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Professional"))
                                    {
                                        ShiftIsVisible = false;
                                        break;
                                    }
                                    ShiftIsEnable = ApplyIsEnable(item.Expression);
                                    ShiftIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }


                            case "WorkOrderStatusID":
                                {
                                    WorkorderStatusIsEnable = ApplyIsEnable(item.Expression);
                                    WorkorderStatusIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }



                            case "WorkTypeID":
                                {
                                    if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic"))
                                    {
                                        WorkorderTypeIsVisible = false;
                                        break;

                                    }
                                    WorkorderTypeIsEnable = ApplyIsEnable(item.Expression);
                                    WorkorderTypeIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }


                            case "Causes":
                                {
                                    CauseIsEnable = ApplyIsEnable(item.Expression);
                                    
                                    break;
                                }


                            case "MaintenanceCodeID":
                                {
                                    MaintenanceCodeIsEnable = ApplyIsEnable(item.Expression);
                                    MaintenanceCodeIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }


                            case "EstimatedDowntime":
                                {
                                    EstimstedDowntimeIsEnable = ApplyIsEnable(item.Expression);
                                    break;
                                }

                            case "ActualDowntime":
                                {
                                    ActualDowntimeIsEnable = ApplyIsEnable(item.Expression);
                                    break;
                                }

                            case "MiscellaneousLaborCostID":
                                {
                                    MiscellaneousLabourCostIsEnable = ApplyIsEnable(item.Expression);
                                    break;
                                }

                            case "MiscellaneousMaterialsCostID":
                                {
                                    MiscellaneousMaterialCostIsEnable = ApplyIsEnable(item.Expression);
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
            if (ServiceRequestControlsNew != null && ServiceRequestControlsNew.Count > 0)
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

                foreach (var item in ServiceRequestControlsNew)
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

            title.Text = formControl.TargetName;
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
            var control = new CustomPicker();
            control.Image = "unnamed";
            control.HeightRequest = 45;
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                title.FontAttributes = FontAttributes.Bold;
            }
            title.TextColor = Color.Black;

            title.Text = formControl.TargetName;

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
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                title.FontAttributes = FontAttributes.Bold;
            }
            title.TextColor = Color.Black;

            View control;
            if (formControl.IsRequired ?? false)
            {
                control = new DatePicker();
            }
            else
            {
                control = new CustomDatePicker();
            }

            SetControlBindingAccordingToControlType(control, formControl);
            //new CustomDatePicker(); //new DatePicker();

            title.Text = formControl.TargetName;

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




                case "Administrator":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.AmStepID));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;
                          
                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(Administrator)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                Administrator = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;
                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.Administrator));
                        }

                        else if (control is Picker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            Administrator = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.Administrator), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.Administrator), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "ConfirmEmail":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.AnalysisPerformedDate));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(RequesterConfirmEmail)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                RequesterConfirmEmail = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;


                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.RequesterConfirmEmail));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            RequesterConfirmEmail = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.RequesterConfirmEmail), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.RequesterConfirmEmail), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "EstimatedDowntime":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.ConfirmEmail));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(EstimatedDowntime)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                EstimatedDowntime = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;
                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.EstimatedDowntime));
                            var x = control as Entry;
                            x.TextChanged += Entry_TextChangedEstimatedDowntime;
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            EstimatedDowntime = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.EstimatedDowntime), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.EstimatedDowntime), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "SendSMSToEmployee":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.CountermeasuresDefinedDate));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(SendSMSToEmployee)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                SendSMSToEmployee = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.SendSMSToEmployee));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            SendSMSToEmployee = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.SendSMSToEmployee), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.SendSMSToEmployee), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "RequesterEmail":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.CurrentRuntime));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(RequesterEmail)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                RequesterEmail = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;
                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.RequesterEmail));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            RequesterEmail = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.RequesterEmail), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.RequesterEmail), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "RequesterFullName":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.DiagnosticTime));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(RequesterFullName)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                RequesterFullName = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;
                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.RequesterFullName));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            RequesterFullName = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.RequesterFullName), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.RequesterFullName), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "RequesterPhone":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.DigitalSignatures));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(RequesterPhone)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                RequesterPhone = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.RequesterPhone));
                            var x = control as Entry;
                            x.TextChanged += Entry_RequesterPhone;

                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            RequesterPhone = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.RequesterPhone), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.RequesterPhone), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }


                case "RequestNumber":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.ImplementationValidatedDate));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(RequestNumber)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                RequestNumber = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;
                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.RequestNumber));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            RequestNumber = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.RequestNumber), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.RequestNumber), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "RequiredDate":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.InitialWaitTime));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(RequiredDate1.ToString())); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                RequiredDate1 = Convert.ToDateTime(item.SelectedValue.ToString());
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.RequiredDate1));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            RequiredDate1 = Convert.ToDateTime(DateTime.Now.ToString());
                            control.SetBinding(DatePicker.DateProperty, nameof(this.RequiredDate1), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                            //control.SetBinding(RequiredDateCustomDatePicker.SelectedDateProperty, nameof(this.RequiredDate), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());

                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.RequiredDate1), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }


                #region User Field Section

                case "UserField1":
                    {
                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField1.Trim()); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                UserField1 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField1), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField2":
                    {
                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField2.Trim()); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                UserField2 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField2), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField3":
                    {
                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField3.Trim()); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField3 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField3), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField4":
                    {
                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField4.Trim()); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField4 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField4), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField5":
                    {
                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField5.Trim()); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField5 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField5), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField6":
                    {
                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField6.Trim()); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField6 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField6), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField7":
                    {
                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField7.Trim()); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField7 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField7), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField8":
                    {
                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField8.Trim()); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField8 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField8), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField9":
                    {
                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField9.Trim()); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField9 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField9), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField10":
                    {
                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField10.Trim()); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField10 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField10), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField11":
                    {
                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField11.Trim()); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField11 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField11), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField12":
                    {
                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField12.Trim()); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField12 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField12), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField13":
                    {
                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField13.Trim()); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField13 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField13), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField14":
                    {
                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField14.Trim()); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField14 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField14), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField15":
                    {
                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField15.Trim()); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField15 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField15), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField16":
                    {
                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField16.Trim()); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField16 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField16), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField17":
                    {
                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField17.Trim()); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField17 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField17), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField18":
                    {
                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField18.Trim()); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField18 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField18), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField19":
                    {
                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField19.Trim()); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField19 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField19), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField20":
                    {
                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField20.Trim()); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                UserField20 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField20), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField21":
                    {
                        if (control is Picker)
                        {                            
                            var x = control as Picker;
                            //x.Image = "unnamed";
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField21.Trim()); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField21 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField21), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }


                case "UserField22":
                    {
                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;

                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField22.Trim()); }
                            catch (Exception) { }


                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField22 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField22), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UserField23":
                    {
                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;

                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField23.Trim()); }
                            catch (Exception) { }


                            if (item != null)
                            {
                                x.SelectedItem = item;

                                UserField23 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField23), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }


                case "UserField24":
                    {
                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            //var item = source.FirstOrDefault(s => s.SelectedText == UserField24);

                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == (UserField24.Trim())); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                UserField24 = item.SelectedText.ToString();
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField24), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }



                #endregion






                default:
                    break;
            }
        }
        private void Entry_RequesterPhone(object sender, EventArgs e)
        {
            Entry e1 = sender as Entry;
            String val = e1.Text; //Get Current Text

            if (val.Contains(" "))//If it is more than your character restriction
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(val, "^[0-9]*$"))
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }
        }
        private void Entry_TextChangedEstimatedDowntime(object sender, EventArgs e)
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

                //var grid = page.Content as Grid;
                var scrollView = grid.Children[0] as ScrollView;
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
                var response = "";

                if ((Decline == "E" || Decline == "V" || Accept == "E" || Accept == "V" || Accept == "N" || Decline == "N"))
                {
                    if (Decline == "N" && Accept == "N")
                    {
                        response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() { LogoutTitle });
                    }
                    else if (Decline == "N")
                    {
                        response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() { AcceptTitle, LogoutTitle });
                    }
                    else if (Accept == "N")
                    {
                        response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() { DeclineTitle, LogoutTitle });
                    }
                    else
                    {
                        response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() { AcceptTitle, DeclineTitle, LogoutTitle });
                    }

                    if (response == LogoutTitle)
                    {
                        await _authenticationService.LogoutAsync();
                        await NavigationService.NavigateToAsync<LoginPageViewModel>();
                        await NavigationService.RemoveBackStackAsync();
                    }
                    if (Accept == "E")
                    {
                        if (response == AcceptTitle)
                        {
                            await AcceptServiceRequest();
                        }
                    }
                    if (Decline == "E")
                    {
                        if (response == DeclineTitle)
                        {
                            await DeclineServiceRequest();
                        }
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


        public async Task OnViewAppearingAsync(VisualElement view)
        {
            try
            {

                OperationInProgress = true;

                if (!IsPickerDataSubscribed)
                {
                    //Retrive Facility
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.FacilityRequested, OnFacilityRequested);

                    //Retrive Location
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.LocationRequested, OnLocationRequested);


                    //Retrive Asset
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.AssetRequested, OnAssetRequested);

                    //Retrive Administrator
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.AdministerRequested, OnAdministratorRequested);

                    //Retrive Asset System
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.AssetSyastemRequested, OnAssetSystemRequested);


                    //Retrive AssignTo
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.AssignToRequested, OnAssignToRequested);

                    //Retrive WorkorderRequester
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.WorkorderRequesterRequested, OnWorkorderRequesterRequested);


                    //Retrive Cost Center
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.CostCenterRequested, OnCostCenterRequested);


                    //Retrive Priority
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.PriorityRequested, OnPriorityRequested);

                    //Retrive Shifts
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.ShiftRequested, OnShiftRequested);

                    //Retrive WorkorderStatus
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.WorkorderStatusRequested, OnWorkorderStatusRequested);

                    //Retrive WorkorderType
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.WorkorderTypeRequested, OnWorkorderTypeRequested);



                    //Retrive WorkorderType
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.MaintenanceCodeRequested, OnMaintenanceCodeRequested);




                    IsPickerDataSubscribed = true;
                }

                else if (IsPickerDataRequested)
                {

                    IsPickerDataRequested = false;
                    return;
                }
              //  await SetControlsPropertiesForPage();


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




        #region Show Selection List Pages Methods
        public async Task ShowFacilities()
        {

            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                //OperationInProgress = true;
                IsPickerDataRequested = true;
                await NavigationService.NavigateToAsync<FacilityListSelectionPageViewModel>(); //Pass the control here
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
        public async Task ShowAdministrator()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                // OperationInProgress = true;
                IsPickerDataRequested = true;
                await NavigationService.NavigateToAsync<AdministratorListSelectionPageViewModel>(new TargetNavigationData() { }); //Pass the control here
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();

                // OperationInProgress = false;

            }

            finally
            {
                UserDialogs.Instance.HideLoading();

                //  OperationInProgress = false;

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
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleaseselectthefacilityfirst"),2000);
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
                // OperationInProgress = false;

            }
        }

        public async Task ShowAssets()
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
                await NavigationService.NavigateToAsync<AssetListSelectionPageViewModel>(new TargetNavigationData() { FacilityID = this.FacilityID, LocationID = this.LocationID }); //Pass the control here
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

        public async Task ShowAssetSystem()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                // OperationInProgress = true;
                IsPickerDataRequested = true;
                if (FacilityID == null)
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleaseselectthefacilityfirst"), 2000);
                    return;

                }
                await NavigationService.NavigateToAsync<AssetSystemListSelectionPageViewModel>(new TargetNavigationData() { FacilityID = this.FacilityID, LocationID = this.LocationID }); //Pass the control here
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

        public async Task ShowAssignTo()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                //OperationInProgress = true;
                IsPickerDataRequested = true;
                await NavigationService.NavigateToAsync<AssignToListSelectionPageViewModel>(new TargetNavigationData() { }); //Pass the control here
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

        public async Task ShowWorkorderRequester()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                // OperationInProgress = true;
                IsPickerDataRequested = true;
                await NavigationService.NavigateToAsync<WorkorderRequesterListSelectionPageViewModel>(new TargetNavigationData() { }); //Pass the control here
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();

                // OperationInProgress = false;

            }

            finally
            {
                UserDialogs.Instance.HideLoading();

                //  OperationInProgress = false;

            }
        }

        public async Task ShowCostCenter()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                //OperationInProgress = true;
                IsPickerDataRequested = true;
                await NavigationService.NavigateToAsync<CostCenterListSelectionPageViewModel>(new TargetNavigationData() { }); //Pass the control here
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

        public async Task ShowPriority()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

              //  OperationInProgress = true;
                IsPickerDataRequested = true;
                await NavigationService.NavigateToAsync<PriorityListSelectionPageViewModel>(new TargetNavigationData() { }); //Pass the control here
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

        public async Task ShowShift()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                //OperationInProgress = true;
                IsPickerDataRequested = true;
                await NavigationService.NavigateToAsync<ShiftListSelectionPageViewModel>(new TargetNavigationData() { }); //Pass the control here
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();

              //  OperationInProgress = false;

            }

            finally
            {
                UserDialogs.Instance.HideLoading();

               // OperationInProgress = false;

            }
        }

        public async Task ShowWorkorderStatus()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                //OperationInProgress = true;
                IsPickerDataRequested = true;
                await NavigationService.NavigateToAsync<WorkorderStatusListSelectionPageViewModel>(new TargetNavigationData() { }); //Pass the control here
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

        public async Task ShowWorkorderType()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

              //  OperationInProgress = true;
                IsPickerDataRequested = true;
                await NavigationService.NavigateToAsync<WorkorderTypeListSelectionPageViewModel>(new TargetNavigationData() { }); //Pass the control here
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



        public async Task ShowMaintenanceCode()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

               // OperationInProgress = true;
                IsPickerDataRequested = true;
                await NavigationService.NavigateToAsync<MaintenanceCodeListSelectionPageViewModel>(new TargetNavigationData() { }); //Pass the control here
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





        #endregion


        #region MessagingService Callback Methods
        private void OnFacilityRequested(object obj)
        {

            if (obj != null)
            {
                var facility = obj as Facility;
                this.FacilityID = facility.FacilityID;
                this.FacilityName = facility.FacilityName;


                // if Facility is selected reset the location, Asset and Asset System
                ResetLocation();
                ResetAsset();
                ResetAssetSystem();

            }


        }

        private void OnLocationRequested(object obj)
        {

            if (obj != null)
            {
                var location = obj as TLocation;
                this.LocationID = location.LocationID;
                this.LocationName = location.LocationName;


                // if Location is selected reset the  Asset and Asset System
                ResetAsset();
                ResetAssetSystem();

            }


        }
        private void OnAdministratorRequested(object obj)
        {

            if (obj != null)
            {

                var administrator = obj as ComboDD;
                this.AdministratorID = administrator.SelectedValue;
                this.AdministratorName = administrator.SelectedText;

            }


        }
        private void OnAssetRequested(object obj)
        {

            if (obj != null)
            {

                var asset = obj as TAsset;
                this.AssetID = asset.AssetID;
                this.AssetName =asset.AssetName;

                // if Asset is selected reset the   Asset System
               
            }


        }

        private void OnAssetSystemRequested(object obj)
        {

            if (obj != null)
            {

                var assetSystem = obj as TAssetSystem;
                this.AssetSystemID = assetSystem.AssetSystemID;
                this.AssetSystemName =assetSystem.AssetSystemName;

                // if AssetSystem is selected reset the   Asset
               
            }


        }

        private void OnAssignToRequested(object obj)
        {

            if (obj != null)
            {

                var assignTo = obj as ComboDD;
                this.AssignToEmployeeID = assignTo.SelectedValue;
                this.AssignToEmployeeName = assignTo.SelectedText;
            }


        }

        private void OnWorkorderRequesterRequested(object obj)
        {

            if (obj != null)
            {

                var requester = obj as ComboDD;
                this.WorkorderRequesterID = requester.SelectedValue;
                this.WorkorderRequesterName = requester.SelectedText;
            }


        }


        private void OnCostCenterRequested(object obj)
        {

            if (obj != null)
            {

                var costCenter = obj as ComboDD;
                this.CostCenterID = costCenter.SelectedValue;
                this.CostCenterName = costCenter.SelectedText;
            }


        }


        private void OnPriorityRequested(object obj)
        {

            if (obj != null)
            {

                var priority = obj as ComboDD;
                this.PriorityID = priority.SelectedValue;
                this.PriorityName = priority.SelectedText;
            }


        }

        private void OnShiftRequested(object obj)
        {

            if (obj != null)
            {

                var shift = obj as ComboDD;
                this.ShiftID = shift.SelectedValue;
                this.ShiftName =shift.SelectedText;
            }


        }

        private void OnWorkorderStatusRequested(object obj)
        {

            if (obj != null)
            {

                var workorderStatus = obj as ComboDD;
                this.WorkorderStatusID = workorderStatus.SelectedValue;
                this.WorkorderStatusName = workorderStatus.SelectedText;
            }


        }

        private void OnWorkorderTypeRequested(object obj)
        {

            if (obj != null)
            {

                var workorderType = obj as ComboDD;
                this.WorkorderTypeID = workorderType.SelectedValue;
                this.WorkorderTypeName = workorderType.SelectedText;
            }


        }

        private void OnCauseRequested(object obj)
        {

            if (obj != null)
            {

                var cause = obj as Cause;
                this.CauseID = cause.CauseID;
                this.CauseName = cause.CauseNumber;
            }


        }

        private void OnMaintenanceCodeRequested(object obj)
        {

            if (obj != null)
            {

                var maintenanceCode = obj as ComboDD;
                this.MaintenanceCodeID = maintenanceCode.SelectedValue;
                this.MaintenanceCodeName =maintenanceCode.SelectedText;
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

        public async Task SetControlsPropertiesForPage()
        {
            ServiceRequestWrapper = await _serviceRequestService.GetServiceRequestDetailByServiceRequestID(this.ServiceRequestID.ToString(),AppSettings.User.UserID.ToString());
            bool fdasignatureKey = AppSettings.User.blackhawkLicValidator.IsFDASignatureValidation;

            if (fdasignatureKey == true)
            {
                if (AppSettings.User.RequireSignaturesForValidation == "True")
                {
                    SignaturesIsVisible = true;
                    if (ServiceRequestWrapper.serviceRequestWrapper.SignatureAuditDetails != null)
                    {
                        foreach (var item in ServiceRequestWrapper.serviceRequestWrapper.SignatureAuditDetails)
                        {
                            SignatureText += item.Signature + "                     " + item.SignatureTimestamp + "                             " + item.SignatureIntent + Environment.NewLine;

                        }
                    }


                }
            }

            if (ServiceRequestWrapper != null && ServiceRequestWrapper.serviceRequestWrapper != null && ServiceRequestWrapper.serviceRequestWrapper.serviceRequest != null)
            {

                var serviceRequest = ServiceRequestWrapper.serviceRequestWrapper.serviceRequest;

                DescriptionText = serviceRequest.Description;
                TagTypeSelectedPickerText = serviceRequest.TagType;
                if(string.IsNullOrWhiteSpace(TagTypeSelectedPickerText))
                {

                    TagTypeIsVisible = false;
                }
                else
                {
                    /////Added Rights for SHE Tag,Maintenanace tag and Operator Tag/////////////
                    if(serviceRequest.TagType=="SHE Tag")
                    {
                        if (Application.Current.Properties.ContainsKey("SHETagTypeKey"))
                        {
                            var SHETagTypeKey = Application.Current.Properties["SHETagTypeKey"].ToString();
                            if (SHETagTypeKey != null)
                            {
                                if (SHETagTypeKey == "E")
                                {
                                    this.TagTypeIsEnabled = true;
                                }
                                else if (SHETagTypeKey == "V")
                                {
                                    this.TagTypeIsEnabled = false;
                                    ColorBackground = Color.FromHex("#D3D3D3");
                                }
                                else
                                {
                                    this.TagTypeIsVisible = false;
                                }

                            }
                        }
                    }

                    if (serviceRequest.TagType == "Maintenance Tag")
                    {
                        if (Application.Current.Properties.ContainsKey("MaintenanceTagTypeKey"))
                        {
                            var MaintenanceTagTypeKey = Application.Current.Properties["MaintenanceTagTypeKey"].ToString();
                            if (MaintenanceTagTypeKey != null)
                            {
                                if (MaintenanceTagTypeKey == "E")
                                {
                                    this.TagTypeIsEnabled = true;
                                }
                                else if (MaintenanceTagTypeKey == "V")
                                {
                                    this.TagTypeIsEnabled = false;
                                    ColorBackground = Color.FromHex("#D3D3D3");
                                }
                                else
                                {
                                    this.TagTypeIsVisible = false;
                                }

                            }
                        }
                    }
                    if (serviceRequest.TagType == "Operator Tag")
                    {
                        if (Application.Current.Properties.ContainsKey("OperatorTagTypeKey"))
                        {
                            var OperatorTagTypeKey = Application.Current.Properties["OperatorTagTypeKey"].ToString();
                            if (OperatorTagTypeKey != null)
                            {
                                if (OperatorTagTypeKey == "E")
                                {
                                    this.TagTypeIsEnabled = true;
                                }
                                else if (OperatorTagTypeKey == "V")
                                {
                                    this.TagTypeIsEnabled = false;
                                    ColorBackground = Color.FromHex("#D3D3D3");
                                }
                                else
                                {
                                    this.TagTypeIsVisible = false;
                                }

                            }
                        }
                    }
                }
                if(!string.IsNullOrWhiteSpace(serviceRequest.AdditionalDetails))
                {
                    AdditionalDetailsText = RemoveHTML.StripHTML(serviceRequest.AdditionalDetails);
                }
                else
                {
                    AdditionalDetailsText = serviceRequest.AdditionalDetails;
                }
               
                /// Set Targets and Other
                /// 
                if (!string.IsNullOrEmpty(serviceRequest.FacilityName))
                {
                    FacilityName = ShortString.shorten(serviceRequest.FacilityName);

                }
                else
                {
                    FacilityName = serviceRequest.FacilityName;

                }
                FacilityID = serviceRequest.FacilityID;

                if (!string.IsNullOrEmpty(serviceRequest.LocationName))
                {
                    LocationName = ShortString.shorten(serviceRequest.LocationName);

                }
                else
                {
                    LocationName = serviceRequest.LocationName;

                }
                LocationID = serviceRequest.LocationID;

                if (!string.IsNullOrEmpty(serviceRequest.AssetName))
                {
                    AssetName = ShortString.shorten(serviceRequest.AssetName);

                }
                else
                {
                    AssetName = serviceRequest.AssetName;

                }
                AssetID = serviceRequest.AssetID;

                if (!string.IsNullOrEmpty(serviceRequest.AssetSystemName))
                {
                    AssetSystemName = ShortString.shorten(serviceRequest.AssetSystemName);

                }
                else
                {
                    AssetSystemName = serviceRequest.AssetSystemName;

                }
                AssetSystemID = serviceRequest.AssetSystemID;

                if (!string.IsNullOrEmpty(serviceRequest.EmployeeName))
                {
                    AssignToEmployeeName = ShortString.shorten(serviceRequest.EmployeeName);

                }
                else
                {
                    AssignToEmployeeName = serviceRequest.EmployeeName;

                }
                AssignToEmployeeID = serviceRequest.AssignedToEmployeeID;

                //
                if (!string.IsNullOrEmpty(serviceRequest.CostCenterName))
                {
                    CostCenterName = ShortString.shorten(serviceRequest.CostCenterName);

                }
                else
                {
                    CostCenterName = serviceRequest.CostCenterName;

                }
                CostCenterID = serviceRequest.CostCenterID;



                if (!string.IsNullOrEmpty(serviceRequest.WorkOrderRequesterName))
                {
                    WorkorderRequesterName = ShortString.shorten(serviceRequest.WorkOrderRequesterName);

                }
                else
                {
                    WorkorderRequesterName = serviceRequest.WorkOrderRequesterName;

                }
                WorkorderRequesterID = serviceRequest.WorkOrderRequesterID;

                if (!string.IsNullOrEmpty(serviceRequest.ShiftName))
                {
                    ShiftName = ShortString.shorten(serviceRequest.ShiftName);

                }
                else
                {
                    ShiftName = serviceRequest.ShiftName;

                }
                ShiftID = serviceRequest.ShiftID;

                if (!string.IsNullOrEmpty(serviceRequest.WorkOrderStatusName))
                {
                    WorkorderStatusName = ShortString.shorten(serviceRequest.WorkOrderStatusName);

                }
                else
                {
                    WorkorderStatusName = serviceRequest.WorkOrderStatusName;

                }
                WorkorderStatusID = serviceRequest.WorkOrderStatusID;

                if (!string.IsNullOrEmpty(serviceRequest.WorkTypeName))
                {
                    WorkorderTypeName = ShortString.shorten(serviceRequest.WorkTypeName);

                }
                else
                {
                    WorkorderTypeName = serviceRequest.WorkTypeName;

                }
                WorkorderTypeID = serviceRequest.WorkTypeID;


                if (!string.IsNullOrEmpty(serviceRequest.AdministratorName))
                {
                    AdministratorName = ShortString.shorten(serviceRequest.AdministratorName);

                }
                else
                {
                    AdministratorName = serviceRequest.AdministratorName;

                }
                AdministratorID = serviceRequest.AdministratorID;


                if (!string.IsNullOrEmpty(serviceRequest.MaintenanceCodeName))
                {
                    MaintenanceCodeName = ShortString.shorten(serviceRequest.MaintenanceCodeName);

                }
                else
                {
                    MaintenanceCodeName = serviceRequest.MaintenanceCodeName;

                }
                MaintenanceCodeID = serviceRequest.MaintenanceCodeID;

                if (!string.IsNullOrEmpty(serviceRequest.PriorityName))
                {
                    PriorityName = ShortString.shorten(serviceRequest.PriorityName);

                }
                else
                {
                    PriorityName = serviceRequest.PriorityName;

                }
                PriorityID = serviceRequest.PriorityID;


                RequesterEmail = serviceRequest.RequesterEmail;
                RequesterConfirmEmail = serviceRequest.RequesterEmail;
                //EstimatedDowntime = serviceRequest.EstimatedDowntime.ToString();
                EstimatedDowntime = string.Format(StringFormat.NumericZero(), string.IsNullOrWhiteSpace(serviceRequest.EstimatedDowntime.ToString()) ? 0 : decimal.Parse(serviceRequest.EstimatedDowntime.ToString()));
                RequesterFullName = serviceRequest.RequesterFullName;
                RequesterPhone = serviceRequest.RequesterPhone;
                // ActivationDateText = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorder.ActivationDate ?? DateTime.Now).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);

                // RequiredDate = Convert.ToDateTime(serviceRequest.RequiredDate);
                RequiredDate1 = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(serviceRequest.RequiredDate ?? DateTime.Now).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                //this.MinimumRequiredDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(serviceRequest.RequiredDate).ToUniversalTime(), AppSettings.User.TimeZone);
                ///Set Dyanmic Field Properties
                ///
                #region Set Dyanmic Field Properties



                #region User Fields

                UserField1 = serviceRequest.UserField1;
                UserField2 = serviceRequest.UserField2;
                UserField3 = serviceRequest.UserField3;
                UserField4 = serviceRequest.UserField4;
                UserField5 = serviceRequest.UserField5;
                UserField6 = serviceRequest.UserField6;
                UserField7 = serviceRequest.UserField7;
                UserField8 = serviceRequest.UserField8;
                UserField9 = serviceRequest.UserField9;
                UserField10 = serviceRequest.UserField10;
                UserField11 = serviceRequest.UserField11;
                UserField12 = serviceRequest.UserField12;
                UserField13 = serviceRequest.UserField13;
                UserField14 = serviceRequest.UserField14;
                UserField15 = serviceRequest.UserField15;
                UserField16 = serviceRequest.UserField16;
                UserField17 = serviceRequest.UserField17;
                UserField18 = serviceRequest.UserField18;
                UserField19 = serviceRequest.UserField19;
                UserField20 = serviceRequest.UserField20;
                UserField21 = serviceRequest.UserField21;
                UserField22 = serviceRequest.UserField22;
                UserField23 = serviceRequest.UserField23;
                UserField24 = serviceRequest.UserField24;
                #endregion



                #endregion



            }

        }
        public async Task EditServiceRequest()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                //OperationInProgress = true;

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
                var validationResult = await ValidateControlsIsRequired(ServiceRequestControlsNew);
                if (validationResult.FailedItem != null)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(validationResult.ErrorMessage);
                    return;
                }






                if (AssetID == 0)
                {
                    AssetID = null;
                }
                
                if (!string.IsNullOrWhiteSpace(RequesterEmail) || !string.IsNullOrWhiteSpace(RequesterConfirmEmail))
                {
                    if (RequesterEmail != RequesterConfirmEmail)
                    {
                        UserDialogs.Instance.HideLoading();
                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("EmailandConfirmEmailmustbesame"), 2000);
                        return;

                    }
                }
                if (!String.IsNullOrWhiteSpace(RequesterEmail))
                {
                    bool IsValidEmail = Regex.IsMatch(RequesterEmail,
              @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
              @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
              RegexOptions.IgnoreCase);

                    if (!IsValidEmail)
                    {
                        UserDialogs.Instance.HideLoading();

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("EnterValidEmail"), 2000);
                        return;
                    }
                }

                //bool IsValidPhoneNumber = IsPhoneNumber(RequesterPhone);
                //if (!IsValidPhoneNumber)
                //{
                //    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("EnterValidPhoneNumber"), 2000);
                //    return;
                //}
                #region Description And target validation
                if (String.IsNullOrWhiteSpace(DescriptionText))
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Fillthedescriptionfield"), 2000);
                    return;
                }

                if (FacilityID == null)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Selectthefacilityfield"), 2000);
                    return;
                }

                else if (LocationID == null && AssetSystemID == null && AssetID==null)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Selectthelocationassetsystemassetfield"), 2000);
                    return;
                }
               
                else if (AssetSystemID != null && AssetID != null)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("PleaseselecteitherassetOrassetsystem"), 2000);
                    return;
                }





                #endregion

                /// Create Asset wrapper


                var ServiceRequest = new ServiceRequests();
                #region ServiceRequest properties initialzation

                ServiceRequest.ModifiedUserName = AppSettings.User.UserName;
                ServiceRequest.ServiceRequestID = ServiceRequestID;
                ServiceRequest.Description = String.IsNullOrEmpty(DescriptionText) ? null : DescriptionText.Trim();
                ServiceRequest.AdditionalDetails = AdditionalDetailsText;
                ServiceRequest.FacilityID = FacilityID;
               
                if (this.AssetID == null && AssetSystemID == null)
                {
                    ServiceRequest.LocationID = LocationID;
                }
                else
                {
                    ServiceRequest.LocationID = null;
                }
                ServiceRequest.AssetSystemID = AssetSystemID;
                ServiceRequest.AssetID = this.AssetID;
                ServiceRequest.AssignedToEmployeeID = AssignToEmployeeID;
                ServiceRequest.CostCenterID = CostCenterID;
                ServiceRequest.WorkOrderRequesterID = WorkorderRequesterID;
                ServiceRequest.PriorityID = PriorityID;
                ServiceRequest.ShiftID = ShiftID;
                ServiceRequest.WorkOrderStatusID = WorkorderStatusID;
                ServiceRequest.WorkTypeID = WorkorderTypeID;
                ServiceRequest.TagType = TagTypeSelectedPickerText;
                ServiceRequest.MaintenanceCodeID = MaintenanceCodeID;

                ServiceRequest.AdministratorID = this.AdministratorID;
                ServiceRequest.EstimatedDowntime = !string.IsNullOrEmpty(EstimatedDowntime) ? (decimal?)decimal.Parse(EstimatedDowntime.Replace(",", "")) : null;
                ServiceRequest.RequesterEmail = RequesterEmail;
                ServiceRequest.ConfirmEmail = RequesterEmail;
                ServiceRequest.RequesterFullName = RequesterFullName;
                ServiceRequest.RequesterPhone = RequesterPhone;
                ServiceRequest.RequiredDate = RequiredDate1.Date.Add(DateTime.Now.TimeOfDay);
                ServiceRequest.MobileEmployeeID = null;


                #region User Fields

                ServiceRequest.UserField1 = String.IsNullOrEmpty(UserField1) ? null : UserField1.Trim();
                ServiceRequest.UserField2 = String.IsNullOrEmpty(UserField2) ? null : UserField2.Trim();
                ServiceRequest.UserField3 = String.IsNullOrEmpty(UserField3) ? null : UserField3.Trim();
                ServiceRequest.UserField4 = String.IsNullOrEmpty(UserField4) ? null : UserField4.Trim();
                ServiceRequest.UserField5 = String.IsNullOrEmpty(UserField5) ? null : UserField5.Trim();
                ServiceRequest.UserField6 = String.IsNullOrEmpty(UserField6) ? null : UserField6.Trim();
                ServiceRequest.UserField7 = String.IsNullOrEmpty(UserField7) ? null : UserField7.Trim();
                ServiceRequest.UserField8 = String.IsNullOrEmpty(UserField8) ? null : UserField8.Trim();
                ServiceRequest.UserField9 = String.IsNullOrEmpty(UserField9) ? null : UserField9.Trim();
                ServiceRequest.UserField10 = String.IsNullOrEmpty(UserField10) ? null : UserField10.Trim();
                ServiceRequest.UserField11 = String.IsNullOrEmpty(UserField11) ? null : UserField11.Trim();
                ServiceRequest.UserField12 = String.IsNullOrEmpty(UserField12) ? null : UserField12.Trim();
                ServiceRequest.UserField13 = String.IsNullOrEmpty(UserField13) ? null : UserField13.Trim();
                ServiceRequest.UserField14 = String.IsNullOrEmpty(UserField14) ? null : UserField14.Trim();
                ServiceRequest.UserField15 = String.IsNullOrEmpty(UserField15) ? null : UserField15.Trim();
                ServiceRequest.UserField16 = String.IsNullOrEmpty(UserField16) ? null : UserField16.Trim();
                ServiceRequest.UserField17 = String.IsNullOrEmpty(UserField17) ? null : UserField17.Trim();
                ServiceRequest.UserField18 = String.IsNullOrEmpty(UserField18) ? null : UserField18.Trim();
                ServiceRequest.UserField19 = String.IsNullOrEmpty(UserField19) ? null : UserField19.Trim();
                ServiceRequest.UserField20 = String.IsNullOrEmpty(UserField20) ? null : UserField20.Trim();
                ServiceRequest.UserField21 = String.IsNullOrEmpty(UserField21) ? null : UserField21.Trim();
                ServiceRequest.UserField22 = String.IsNullOrEmpty(UserField22) ? null : UserField22.Trim();
                ServiceRequest.UserField23 = String.IsNullOrEmpty(UserField23) ? null : UserField23.Trim();
                ServiceRequest.UserField24 = String.IsNullOrEmpty(UserField24) ? null : UserField24.Trim();
                #endregion

                bool fdasignatureKey = AppSettings.User.blackhawkLicValidator.IsFDASignatureValidation;

                if (fdasignatureKey == true)
                {
                    if (AppSettings.User.RequireSignaturesForValidation == "True")
                    {
                        ServiceRequest.IsSignatureValidated = true;

                        //  Application.Current.Properties["CauseID"] = this.CauseID;
                        Application.Current.Properties["ServiceRequestWrapper"] = ServiceRequest;

                        var page = new EditServiceRequestSignaturePage();
                        await PopupNavigation.PushAsync(page);

                    }
                    else
                    {
                        ServiceRequest.IsSignatureValidated = false;
                        #endregion
                        var serviceRequest = new ServiceRequestWrapper
                        {
                            TimeZone = "UTC",
                            UserId = Convert.ToInt32(this.UserID),
                            ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                            CultureName = "en-US",
                            serviceRequest = ServiceRequest,

                        };


                        var response = await _serviceRequestService.EditServiceRequest(serviceRequest);
                        if (response != null && bool.Parse(response.servicestatus))
                        {
                            DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Servicerequestsuccessfullyupdated"), 2000);


                        }
                    }
                }
                else
                {
                    ServiceRequest.IsSignatureValidated = false;
                    #endregion
                    var serviceRequest = new ServiceRequestWrapper
                    {
                        TimeZone = "UTC",
                        UserId = Convert.ToInt32(this.UserID),
                        ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                        CultureName = "en-US",
                        serviceRequest = ServiceRequest,

                    };


                    var response = await _serviceRequestService.EditServiceRequest(serviceRequest);
                    if (response != null && bool.Parse(response.servicestatus))
                    {
                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Servicerequestsuccessfullyupdated"), 2000);


                    }
                }
                UserDialogs.Instance.HideLoading();

                //OperationInProgress = false;



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
                        case "Administrator":
                            {
                                validationResult = ValidateValidations(formLoadItem, Administrator);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "ConfirmEmail":
                            {
                                validationResult = ValidateValidations(formLoadItem, RequesterConfirmEmail);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "EstimatedDowntime":
                            {
                                validationResult = ValidateValidations(formLoadItem, EstimatedDowntime);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "SendSMSToEmployee":
                            {
                                validationResult = ValidateValidations(formLoadItem, SendSMSToEmployee);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "RequesterEmail":
                            {
                                validationResult = ValidateValidations(formLoadItem, RequesterEmail);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "RequesterFullName":
                            {
                                validationResult = ValidateValidations(formLoadItem, RequesterFullName);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "RequesterPhone":
                            {
                                validationResult = ValidateValidations(formLoadItem, RequesterPhone);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "RequestNumber":
                            {

                                validationResult = ValidateValidations(formLoadItem, RequestNumber);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }



                        case "RequiredDate":
                            {
                                validationResult = ValidateValidations(formLoadItem, RequiredDate1.ToString());
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


                        case "Description":
                            {
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, DescriptionText);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }


                        case "RequiredDate":
                            {
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, RequiredDate1.ToString());
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }








                        case "AssignedToEmployeeID":
                            {
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, AssignToEmployeeID.ToString());
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }


                        case "WorkOrderRequesterID":
                            {
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, WorkorderRequesterID.ToString());
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }


                        case "CostCenterID":
                            {
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, CostCenterID.ToString());
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }


                        case "PriorityID":
                            {
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, PriorityID.ToString());
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }


                        case "ShiftID":
                            {
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, ShiftID.ToString());
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }


                        case "WorkOrderStatusID":
                            {
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, WorkorderStatusID.ToString());
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }


                        case "WorkTypeID":
                            {
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, WorkorderTypeID.ToString());
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }





                        case "MaintenanceCodeID":
                            {
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, MaintenanceCodeID.ToString());
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }


                        case "EstimatedDowntime":
                            {
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, EstimstedDowntimeText);
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

        public async Task AcceptServiceRequest()
        {
            try
            {

                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));


                //Check if Service Request Has Target///////////////
                var ServiceRequestResponse = await _serviceRequestService.GetServiceRequestDetailByServiceRequestID(this.ServiceRequestID.ToString(), AppSettings.User.UserID.ToString());
                if (ServiceRequestResponse.serviceRequestWrapper != null && ServiceRequestResponse.serviceRequestWrapper.serviceRequest != null)
                {

                    if (ServiceRequestResponse.serviceRequestWrapper.serviceRequest.LocationID == null && ServiceRequestResponse.serviceRequestWrapper.serviceRequest.AssetID == null && ServiceRequestResponse.serviceRequestWrapper.serviceRequest.AssetSystemID == null)
                    {
                        UserDialogs.Instance.HideLoading();


                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Selectthelocationassetsystemassetfield"), 2000);
                        return;
                    }
                }

                bool fdasignatureKey = AppSettings.User.blackhawkLicValidator.IsFDASignatureValidation;

                if (fdasignatureKey == true)
                {

                    if (AppSettings.User.RequireSignaturesForValidation == "True")
                    {
                        // ServiceRequest.IsSignatureValidated = true;

                        Application.Current.Properties["SRID"] = this.ServiceRequestID;
                        Application.Current.Properties["ServiceRequestNavigation"] = "True";

                        var page = new AcceptServiceRequestSignaturePage();
                        await PopupNavigation.PushAsync(page);

                    }
                    else
                    {

                        var yourobject = new ServiceRequestWrapper
                        {
                            TimeZone = "UTC",
                            CultureName = "en-US",
                            UserId = Convert.ToInt32(this.UserID),
                            serviceRequest = new ServiceRequests
                            {
                                IsSignatureValidated = false,
                                ServiceRequestID = this.ServiceRequestID,

                            },

                        };


                        var response = await _serviceRequestService.AcceptServiceRequest(yourobject);

                        if (Boolean.Parse(response.servicestatus))
                        {
                            await NavigationService.NavigateBackAsync();
                        }
                    }
                }
                else
                {

                    var yourobject = new ServiceRequestWrapper
                    {
                        TimeZone = "UTC",
                        CultureName = "en-US",
                        UserId = Convert.ToInt32(this.UserID),
                        serviceRequest = new ServiceRequests
                        {
                            IsSignatureValidated = false,
                            ServiceRequestID = this.ServiceRequestID,

                        },

                    };


                    var response = await _serviceRequestService.AcceptServiceRequest(yourobject);

                    if (Boolean.Parse(response.servicestatus))
                    {
                        await NavigationService.NavigateBackAsync();
                    }
                }




                UserDialogs.Instance.HideLoading();

                // OperationInProgress = false;

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
        public async Task DeclineServiceRequest()
        {
            try
            {

                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                ////Check if User is Admin////
                //bool IFUserIsAdmin = Convert.ToBoolean(AppSettings.User.UserIsAdmin);
                //if (IFUserIsAdmin == false)
                //{
                //    //Check if User is Service Request Admin////
                //    bool IFUserIsSRAdmin = Convert.ToBoolean(AppSettings.User.UserIsSRAdmin);
                //    if (IFUserIsSRAdmin == false)
                //    {
                //        UserDialogs.Instance.HideLoading();

                //        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("CurrentuserisnotauthorizedtodeclineselectedServiceRequest"), 2000);
                //        return;
                //    }
                //}


                

                var page = new SRDecline(UserID, ServiceRequestID, _serviceRequestService);
                await PopupNavigation.PushAsync(page);


               

            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();

                // OperationInProgress = false;
            }

            finally
            {
                UserDialogs.Instance.HideLoading();

                //  OperationInProgress = false;
            }
        }
        public async Task ShowSignatures()
        {

            try
            {
                UserDialogs.Instance.HideLoading();

                TargetNavigationData tnobj = new TargetNavigationData();
                if (ServiceRequestWrapper.serviceRequestWrapper != null && ServiceRequestWrapper.serviceRequestWrapper.SignatureAuditDetails != null)
                {

                    tnobj.SignatureAuditDetails = ServiceRequestWrapper.serviceRequestWrapper.SignatureAuditDetails;
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
        public async Task ShowMoreAdditionalDetails()
        {

            try
            {
                UserDialogs.Instance.HideLoading();

                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.Description = AdditionalDetailsText;
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
        public async Task SpeechtoText()
        {

            try
            {
                string Value = await DependencyService.Get<ISpeechToText>().SpeechToText();

                this.AdditionalDetailsText += " " + Value;
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
        private async Task RefillTagTypeFromPicker()
        {
            if (TagTypeSelectedIndexPicker == -1)
            {
                return;
            }

            var SelectedPickerText = TagTypePickerTitles[TagTypeSelectedIndexPicker];
            TagTypeSelectedPickerText = SelectedPickerText;
            //var ShiftSelectedPickerText = SortByShiftpickerTitles[ShiftSelectedIndexPicker];
            //var PrioritySelectedPickerText = SortByPriorityPickerTitles[PrioritySelectedIndexPicker];


            //if (SelectedPickerText == SelectTitle)
            //{
            //    WorkorderTypeFilterText = null;
            //    await RefillWorkorderCollection();
            //}


            //else
            //{
            //    WorkorderTypeFilterText = null;
            //    await RefillWorkorderCollection();
            //}




        }
       

    }
}
