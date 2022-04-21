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
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Asset;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Navigation;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.SelectionListPagesViewModels;
using ProteusMMX.Views.Common;
using Rg.Plugins.Popup.Services;
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

namespace ProteusMMX.ViewModel.Workorder
{
    public class EditWorkorderPageViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {

        string CurrentRuntimeEnablevalue = string.Empty;
        string CurrentRuntimeVisiblevalue = string.Empty;
        #region Fields
        ServiceOutput workorderWrapper;

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly IWorkorderService _workorderService;

        public readonly INavigationService _navigationService;

        protected readonly IAssetModuleService _assetService;
        #endregion

        #region Properties

        #region Page Properties

        string MinInspectionStartDate = string.Empty;
        string MaxInspectionStartDate = string.Empty;
        string MaxInspectionCompDate = string.Empty;
        string MinInspectionCompDate = string.Empty;
        string MaxInspectionCompDateforNull = string.Empty;
        string InspctionStartDateforNull = string.Empty;

        bool _EditWorkIsEnable = true;
        public bool EditWorkIsEnable
        {
            get
            {
                return _EditWorkIsEnable;
            }

            set
            {
                if (value != _EditWorkIsEnable)
                {
                    _EditWorkIsEnable = value;
                    OnPropertyChanged(nameof(EditWorkIsEnable));
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
        string _associatedAssets;
        public string AssociatedAssets
        {
            get
            {
                return _associatedAssets;
            }

            set
            {
                if (value != _associatedAssets)
                {
                    _associatedAssets = value;
                    OnPropertyChanged(nameof(AssociatedAssets));
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
        bool _EditWorkIsVisible = true;
        public bool EditWorkIsVisible
        {
            get
            {
                return _EditWorkIsVisible;
            }

            set
            {
                if (value != _EditWorkIsVisible)
                {
                    _EditWorkIsVisible = value;
                    OnPropertyChanged(nameof(EditWorkIsVisible));
                }
            }
        }
        bool _AssetIsVisible = true;
        public bool AssetIsVisible
        {
            get
            {
                return _AssetIsVisible;
            }

            set
            {
                if (value != _AssetIsVisible)
                {
                    _AssetIsVisible = value;
                    OnPropertyChanged(nameof(AssetIsVisible));
                }
            }
        }

        bool _WorkorderNumberrIsVisible = true;
        public bool WorkorderNumberIsVisible
        {
            get
            {
                return _WorkorderNumberrIsVisible;
            }

            set
            {
                if (value != _WorkorderNumberrIsVisible)
                {
                    _WorkorderNumberrIsVisible = value;
                    OnPropertyChanged(nameof(WorkorderNumberIsVisible));
                }
            }
        }

        bool _jobNumberIsVisible = true;
        public bool JobNumberIsVisible
        {
            get
            {
                return _jobNumberIsVisible;
            }

            set
            {
                if (value != _jobNumberIsVisible)
                {
                    _jobNumberIsVisible = value;
                    OnPropertyChanged(nameof(JobNumberIsVisible));
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




        bool _assignToEmployeeIsVisible = true;
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


        bool _workorderRequesterIsVisible = true;
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


        bool _costCenterIsVisible = true;
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

        bool _priorityIsVisible = true;
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

        bool _currentRuntimeIsEnable = true;
        public bool CurrentRuntimeIsEnable
        {
            get
            {
                return _currentRuntimeIsEnable;
            }

            set
            {
                if (value != _currentRuntimeIsEnable)
                {
                    _currentRuntimeIsEnable = value;
                    OnPropertyChanged(nameof(CurrentRuntimeIsEnable));
                }
            }
        }

        bool _shiftIsVisible = true;
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

        bool _workorderStatusIsVisible = true;
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


        bool _workorderTypeIsVisible = true;
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


        bool _maintenanceCodeIsVisible = true;
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
        bool _isCostLayoutIsVisibleForChild = true;
        public bool IsCostLayoutIsVisibleForChild
        {
            get
            {
                return _isCostLayoutIsVisibleForChild;
            }

            set
            {
                if (value != _isCostLayoutIsVisibleForChild)
                {
                    _isCostLayoutIsVisibleForChild = value;
                    OnPropertyChanged(nameof(IsCostLayoutIsVisibleForChild));
                }
            }
        }

        bool _isCostLayoutIsVisibleForParent = true;
        public bool IsCostLayoutIsVisibleForParent
        {
            get
            {
                return _isCostLayoutIsVisibleForParent;
            }

            set
            {
                if (value != _isCostLayoutIsVisibleForParent)
                {
                    _isCostLayoutIsVisibleForParent = value;
                    OnPropertyChanged(nameof(IsCostLayoutIsVisibleForParent));
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


        bool _estimstedDowntimeIsVisible = true;
        public bool EstimstedDowntimeIsVisible
        {
            get
            {
                return _estimstedDowntimeIsVisible;
            }

            set
            {
                if (value != _estimstedDowntimeIsVisible)
                {
                    _estimstedDowntimeIsVisible = value;
                    OnPropertyChanged(nameof(EstimstedDowntimeIsVisible));
                }
            }
        }

        bool _actualDowntimeIsVisible = true;
        public bool ActualDowntimeIsVisible
        {
            get
            {
                return _actualDowntimeIsVisible;
            }

            set
            {
                if (value != _actualDowntimeIsVisible)
                {
                    _actualDowntimeIsVisible = value;
                    OnPropertyChanged(nameof(ActualDowntimeIsVisible));
                }
            }
        }
        bool _locationIsVisibleForLicensing = true;
        public bool LocationIsVisibleForLicensing
        {
            get
            {
                return _locationIsVisibleForLicensing;
            }

            set
            {
                if (value != _locationIsVisibleForLicensing)
                {
                    _locationIsVisibleForLicensing = value;
                    OnPropertyChanged(nameof(LocationIsVisibleForLicensing));
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


        bool _miscellaneousLabourCostIsVisible = true;
        public bool MiscellaneousLabourCostIsVisible
        {
            get
            {
                return _miscellaneousLabourCostIsVisible;
            }

            set
            {
                if (value != _miscellaneousLabourCostIsVisible)
                {
                    _miscellaneousLabourCostIsVisible = value;
                    OnPropertyChanged(nameof(MiscellaneousLabourCostIsVisible));
                }
            }
        }


        bool _miscellaneousMaterialCostIsVisible = true;
        public bool MiscellaneousMaterialCostIsVisible
        {
            get
            {
                return _miscellaneousMaterialCostIsVisible;
            }

            set
            {
                if (value != _miscellaneousMaterialCostIsVisible)
                {
                    _miscellaneousMaterialCostIsVisible = value;
                    OnPropertyChanged(nameof(MiscellaneousMaterialCostIsVisible));
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
        string CloseWorkorderRights;

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

        #region ***** DistributeCost *****
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

        bool _distributeCostforAssetsystemIsVisible = true;
        public bool DistributeCostforAssetsystemIsVisible
        {
            get
            {
                return _distributeCostforAssetsystemIsVisible;
            }

            set
            {
                if (value != _distributeCostforAssetsystemIsVisible)
                {
                    _distributeCostforAssetsystemIsVisible = value;
                    OnPropertyChanged(nameof(DistributeCostforAssetsystemIsVisible));
                }
            }
        }

        bool _distributeCostforAssetsystemIsEnable = true;
        public bool DistributeCostforAssetsystemIsEnable
        {
            get
            {
                return _distributeCostforAssetsystemIsEnable;
            }

            set
            {
                if (value != _distributeCostforAssetsystemIsEnable)
                {
                    _distributeCostforAssetsystemIsEnable = value;
                    OnPropertyChanged(nameof(DistributeCostforAssetsystemIsEnable));
                }
            }
        }
        #endregion

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



        List<FormControl> _workorderControlsNew = new List<FormControl>();
        public List<FormControl> WorkorderControlsNew
        {
            get
            {
                return _workorderControlsNew;
            }

            set
            {
                _workorderControlsNew = value;
            }
        }



        #endregion

        #region Title Properties
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
        string _workorderTitle;
        public string WorkorderTitle
        {
            get
            {
                return _workorderTitle;
            }

            set
            {
                if (value != _workorderTitle)
                {
                    _workorderTitle = value;
                    OnPropertyChanged("WorkorderTitle");
                }
            }
        }

        string _closeWorkorderTitle;
        public string CloseWorkorderTitle
        {
            get
            {
                return _closeWorkorderTitle;
            }

            set
            {
                if (value != _closeWorkorderTitle)
                {
                    _closeWorkorderTitle = value;
                    OnPropertyChanged("CloseWorkorderTitle");
                }
            }
        }

        string _inventoryTransactionTitle;
        public string InventoryTransactionTitle
        {
            get
            {
                return _inventoryTransactionTitle;
            }

            set
            {
                if (value != _inventoryTransactionTitle)
                {
                    _inventoryTransactionTitle = value;
                    OnPropertyChanged("InventoryTransactionTitle");
                }
            }
        }

        string _serviceRequestTitle;
        public string ServiceRequestTitle
        {
            get
            {
                return _serviceRequestTitle;
            }

            set
            {
                if (value != _serviceRequestTitle)
                {
                    _serviceRequestTitle = value;
                    OnPropertyChanged("ServiceRequestTitle");
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

        string _barcodeTitle;
        public string BarcodeTitle
        {
            get
            {
                return _barcodeTitle;
            }

            set
            {
                if (value != _barcodeTitle)
                {
                    _barcodeTitle = value;
                    OnPropertyChanged("BarcodeTitle");
                }
            }
        }

        string _receivingTitle;
        public string ReceivingTitle
        {
            get
            {
                return _receivingTitle;
            }

            set
            {
                if (value != _receivingTitle)
                {
                    _receivingTitle = value;
                    OnPropertyChanged("ReceivingTitle");
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



        #region EditWorkorder Properties

        //Originator
        string _originatorTitle;
        public string OriginatorTitle
        {
            get
            {
                return _originatorTitle;
            }

            set
            {
                if (value != _originatorTitle)
                {
                    _originatorTitle = value;
                    OnPropertyChanged(nameof(OriginatorTitle));
                }
            }
        }

        string _originatorName;
        public string OriginatorName
        {
            get
            {
                return _originatorName;
            }

            set
            {
                if (value != _originatorName)
                {
                    _originatorName = value;
                    OnPropertyChanged(nameof(OriginatorName));
                }
            }
        }

        bool _originatorIsVisible = true;
        public bool OriginatorIsVisible
        {
            get
            {
                return _originatorIsVisible;
            }

            set
            {
                if (value != _originatorIsVisible)
                {
                    _originatorIsVisible = value;
                    OnPropertyChanged(nameof(OriginatorIsVisible));
                }
            }
        }

        bool _originatorIsEnable = true;
        public bool OriginatorIsEnable
        {
            get
            {
                return _originatorIsEnable;
            }

            set
            {
                if (value != _originatorIsEnable)
                {
                    _originatorIsEnable = value;
                    OnPropertyChanged(nameof(OriginatorIsEnable));
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

        string _totalTimeTitle;
        public string TotalTimeTitle
        {
            get
            {
                return _totalTimeTitle;
            }

            set
            {
                if (value != _totalTimeTitle)
                {
                    _totalTimeTitle = value;
                    OnPropertyChanged(nameof(TotalTimeTitle));
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



        bool _isWorkorderFromSchedule = false;
        public bool IsWorkorderFromSchedule
        {
            get
            {
                return _isWorkorderFromSchedule;
            }

            set
            {
                if (value != _isWorkorderFromSchedule)
                {
                    _isWorkorderFromSchedule = value;
                    OnPropertyChanged(nameof(IsWorkorderFromSchedule));
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

        // DateTime _minimumRequiredDate=DateTime.Now;
        DateTime _minimumRequiredDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);
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
        DateTime? _workStartedDate1;
        public DateTime? WorkStartedDate1
        {
            get
            {
                return _workStartedDate1;
            }

            set
            {
                if (value != _workStartedDate1)
                {
                    _workStartedDate1 = value;
                    OnPropertyChanged(nameof(WorkStartedDate1));
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


        bool _workStartedDateIsVisible = true;
        public bool WorkStartedDateIsVisible
        {
            get
            {
                return _workStartedDateIsVisible;
            }

            set
            {
                if (value != _workStartedDateIsVisible)
                {
                    _workStartedDateIsVisible = value;
                    OnPropertyChanged(nameof(WorkStartedDateIsVisible));
                }
            }
        }
        bool _workorderCompletionDateIsVisible = true;
        public bool WorkorderCompletionDateIsVisible
        {
            get
            {
                return _workorderCompletionDateIsVisible;
            }

            set
            {
                if (value != _workorderCompletionDateIsVisible)
                {
                    _workorderCompletionDateIsVisible = value;
                    OnPropertyChanged(nameof(WorkorderCompletionDateIsVisible));
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

        bool _totalTimeIsEnable = true;
        public bool TotalTimeIsEnable
        {
            get
            {
                return _totalTimeIsEnable;
            }

            set
            {
                if (value != _totalTimeIsEnable)
                {
                    _totalTimeIsEnable = value;
                    OnPropertyChanged(nameof(TotalTimeIsEnable));
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


        string _CurrentRuntimeText;
        public string CurrentRuntimeText
        {
            get
            {
                return _CurrentRuntimeText;
            }

            set
            {
                if (value != _CurrentRuntimeText)
                {
                    _CurrentRuntimeText = value;
                    OnPropertyChanged(nameof(CurrentRuntimeText));
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
        string _LottoUrl;
        public string Lottourl
        {
            get
            {
                return _LottoUrl;
            }

            set
            {
                if (value != _LottoUrl)
                {
                    _LottoUrl = value;
                    OnPropertyChanged(nameof(Lottourl));
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

        //string _additionalDetailsTextforMobile;
        //public string AdditionalDetailsTextForMobile
        //{
        //    get
        //    {
        //        return _additionalDetailsTextforMobile;
        //    }

        //    set
        //    {
        //        if (value != _additionalDetailsTextforMobile)
        //        {
        //            _additionalDetailsTextforMobile = value;
        //            OnPropertyChanged(nameof(AdditionalDetailsTextForMobile));
        //        }
        //    }
        //}

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

        string _internalNotesTextForMobile;
        public string InternalNotesTextForMobile
        {
            get
            {
                return _internalNotesTextForMobile;
            }

            set
            {
                if (value != _internalNotesTextForMobile)
                {
                    _internalNoteText = value;
                    OnPropertyChanged(nameof(InternalNotesTextForMobile));
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

        string _internalNotesTitle;
        public string InternalNotesTitle
        {
            get
            {
                return _internalNotesTitle;
            }

            set
            {
                if (value != _internalNotesTitle)
                {
                    _internalNotesTitle = value;
                    OnPropertyChanged(nameof(InternalNotesTitle));
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
        bool _internalNotesIsVisible = true;
        public bool InternalNotesIsVisible
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
                    OnPropertyChanged(nameof(InternalNotesIsVisible));
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

        bool _internalNotesIsEnable = true;
        public bool InternalNotesIsEnable
        {
            get
            {
                return _internalNotesIsEnable;
            }

            set
            {
                if (value != _internalNotesIsEnable)
                {
                    _internalNotesIsEnable = value;
                    OnPropertyChanged(nameof(InternalNotesIsEnable));
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

        string _requestedDateTitle;
        public string RequestedDateTitle
        {
            get
            {
                return _requestedDateTitle;
            }

            set
            {
                if (value != _requestedDateTitle)
                {
                    _requestedDateTitle = value;
                    OnPropertyChanged(nameof(RequestedDateTitle));
                }
            }
        }
        //string _requestedDateText;
        //public string RequestedDateText
        //{
        //    get
        //    {
        //        return _requestedDateText;
        //    }

        //    set
        //    {
        //        if (value != _requestedDateText)
        //        {
        //            _requestedDateText = value;
        //            OnPropertyChanged(nameof(RequestedDateText));
        //        }
        //    }
        //}
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
        string _closedDateText;
        public string ClosedDateText
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


        //CurrentRuntime
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
        string _riskQuestion;
        public string RiskQuestion
        {
            get
            {
                return _riskQuestion;
            }

            set
            {
                if (value != _riskQuestion)
                {
                    _riskQuestion = value;
                    OnPropertyChanged(nameof(RiskQuestion));
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

        string _totalTimeWorkorder;
        public string TotalTimeWorkorder
        {
            get
            {
                return _totalTimeWorkorder;
            }

            set
            {
                if (value != _totalTimeWorkorder)
                {
                    _totalTimeWorkorder = value;
                    OnPropertyChanged(nameof(TotalTimeWorkorder));
                }
            }
        }

        bool _jobNumberFlag = false;
        public bool JobNumberFlag
        {
            get
            {
                return _jobNumberFlag;
            }

            set
            {
                if (value != _jobNumberFlag)
                {
                    _jobNumberFlag = value;
                    OnPropertyChanged(nameof(JobNumberFlag));
                }
            }
        }

        //Approval Level
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

        //Approval Number
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


        bool _WorkorderCompletionDateWarningTextIsVisible = false;
        public bool WorkorderCompletionDateWarningTextIsVisible
        {
            get
            {
                return _WorkorderCompletionDateWarningTextIsVisible;
            }

            set
            {
                if (value != _WorkorderCompletionDateWarningTextIsVisible)
                {
                    _WorkorderCompletionDateWarningTextIsVisible = value;
                    OnPropertyChanged(nameof(WorkorderCompletionDateWarningTextIsVisible));
                }
            }
        }
        bool _WorkStartedDateWarningTextIsVisible = false;
        public bool WorkStartedDateWarningTextIsVisible
        {
            get
            {
                return _WorkStartedDateWarningTextIsVisible;
            }

            set
            {
                if (value != _WorkStartedDateWarningTextIsVisible)
                {
                    _WorkStartedDateWarningTextIsVisible = value;
                    OnPropertyChanged(nameof(WorkStartedDateWarningTextIsVisible));
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
        #endregion




        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);


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
        public ICommand CauseCommand => new AsyncCommand(ShowCause);
        public ICommand MaintenanceCodeCommand => new AsyncCommand(ShowMaintenanceCode);

        public ICommand DescriptionCommand => new AsyncCommand(ShowMore1Description);

        public ICommand TapCommand1 => new AsyncCommand(ShowMoreDescription);

        public ICommand TapCommand2 => new AsyncCommand(ShowMoreAdditionalDetails);

        public ICommand TapCommand3 => new AsyncCommand(InternalNotesDetails);

        public ICommand TapCommandSignature => new AsyncCommand(ShowSignatures);


        //Save Command
        public ICommand SaveWorkorderCommand => new AsyncCommand(SaveWorkorder);

        public ICommand TapCommand => new AsyncCommand(SpeechtoText);

        public ICommand IssueWorkorderCommand => new AsyncCommand(IssueWorkorder);


        #endregion

        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                OperationInProgress = true;



                if (navigationData != null)
                {

                    var navigationParams = navigationData as PageParameters;
                    this.Page = navigationParams.Page;

                    var workorder = navigationParams.Parameter as workOrders;
                    this.WorkorderID = workorder.WorkOrderID;
                    Application.Current.Properties["WorkorderIDafterCreation"] = this.WorkorderID;


                }

                if (AppSettings.User.blackhawkLicValidator.ServiceRequestIsEnabled.Equals(true))
                {
                    if (Application.Current.Properties.ContainsKey("WorkOrderRequestedDateKey"))
                    {
                        var WorkOrderRequestedDate = Application.Current.Properties["WorkOrderRequestedDateKey"].ToString();
                        if (WorkOrderRequestedDate != null && (WorkOrderRequestedDate == "E" || WorkOrderRequestedDate == "V"))
                        {

                            this.RequestedDateIsVisible = true;
                        }
                        else
                        {
                            this.RequestedDateIsVisible = false;
                        }
                    }
                }
                if (Device.Idiom == TargetIdiom.Phone)
                {
                    this.IsCostLayoutIsVisibleForPhone = true;
                }
                else
                {
                    this.IsCostLayoutIsVisibleForTab = true;
                }

                await SetTitlesPropertiesForPage();

                workorderWrapper = await _workorderService.GetWorkorderByWorkorderID(UserID, WorkorderID.ToString());
                if (workorderWrapper.workOrderWrapper.Cause == null)
                {

                }
                else
                {
                    this.Cause = workorderWrapper.workOrderWrapper.Cause[0];

                }
                if (workorderWrapper.workOrderWrapper.WorkorderCreatedfromSchedule)
                {
                    IsWorkorderFromSchedule = true;
                }
                await SetControlsPropertiesForPage(workorderWrapper);
                await CreateControlsForPage();



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

        public EditWorkorderPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IWorkorderService workorderService, INavigationService navigationService, IAssetModuleService assetService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _workorderService = workorderService;
            _navigationService = navigationService;
            _assetService = assetService;
        }

        public async Task SetTitlesPropertiesForPage()
        {
            try
            {


                {
                    OriginatorTitle = "Originator";
                    CurrentRuntimeTitle = WebControlTitle.GetTargetNameByTitleName("CurrentRuntime");
                    PageTitle = WebControlTitle.GetTargetNameByTitleName("Details");
                    WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                    LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                    CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                    SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                    TotalTimeTilte = WebControlTitle.GetTargetNameByTitleName("TotalTime");

                    WorkorderTitle = WebControlTitle.GetTargetNameByTitleName("WorkOrder");
                    CloseWorkorderTitle = WebControlTitle.GetTargetNameByTitleName("CloseWorkOrder");
                    InventoryTransactionTitle = WebControlTitle.GetTargetNameByTitleName("InventoryTransaction");
                    ServiceRequestTitle = WebControlTitle.GetTargetNameByTitleName("ServiceRequest");
                    AssetsTitle = WebControlTitle.GetTargetNameByTitleName("Assets");
                    BarcodeTitle = WebControlTitle.GetTargetNameByTitleName("SearchScanBarcode");
                    ReceivingTitle = WebControlTitle.GetTargetNameByTitleName("Receiving");
                    TargetTitle = WebControlTitle.GetTargetNameByTitleName("LocationHierarchy");
                    ActivationDateTitle = WebControlTitle.GetTargetNameByTitleName("ActivationDate");
                    RequestedDateTitle = WebControlTitle.GetTargetNameByTitleName("RequestedDate");

                    SectionNameTitle = WebControlTitle.GetTargetNameByTitleName("SectionName");
                    SaveTitle = WebControlTitle.GetTargetNameByTitleName("Save");
                    SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                    FacilityTitle = WebControlTitle.GetTargetNameByTitleName("Facility");
                    LocationTitle = WebControlTitle.GetTargetNameByTitleName("Location");
                    AssetsTitle = WebControlTitle.GetTargetNameByTitleName("Asset");
                    AssetSystemTitle = WebControlTitle.GetTargetNameByTitleName("AssetSystem");
                    InternalNotesTitle = WebControlTitle.GetTargetNameByTitleName("InternalNote");
                    AdditionalDetailsTitle = WebControlTitle.GetTargetNameByTitleName("Notes");
                    MoreText = WebControlTitle.GetTargetNameByTitleName("More");
                    Signatures = WebControlTitle.GetTargetNameByTitleName("Signatures");
                    AssociatedAssets = (WebControlTitle.GetTargetNameByTitleName("AssociatedAssets"));
                    ChargeCostsOnlyToChildAssets = WebControlTitle.GetTargetNameByTitleName("ChargeCostsOnlyToChildAssets");
                    ParentCostsOnly = WebControlTitle.GetTargetNameByTitleName("Chargecosttotheparentsystemandchildassets");
                    DistributeCostforAssetsystem = WebControlTitle.GetTargetNameByTitleName("DistributeCostforAssetsystem");
                    if (DistributeCostforAssetsystem == null)
                    {
                        IsCostLayoutIsVisible = false;
                    }
                    if (ParentCostsOnly == null || ChargeCostsOnlyToChildAssets == null)
                    {

                        IsCostLayoutIsVisibleForChild = false;
                        IsCostLayoutIsVisibleForParent = false;

                    }


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


            ///Seperate the static controls so they don't create twice and we have to keep it
            ///some place so we set its visibility and required field also.
            ///

            #region Extract Details control



            if (Application.Current.Properties.ContainsKey("EditRights"))
            {
                var Edit = Application.Current.Properties["EditRights"].ToString();
                if (Edit == "E")
                {
                    this.EditWorkIsVisible = true;
                }
                else if (Edit == "V")
                {
                    this.EditWorkIsEnable = false;
                }
                else
                {
                    this.EditWorkIsVisible = false;
                }

            }



            if (Application.Current.Properties.ContainsKey("CloseWorkorderRightsKey"))
            {
                var CloseWorkorderRightsExpression = Application.Current.Properties["CloseWorkorderRightsKey"].ToString();
                if (CloseWorkorderRightsExpression != null)
                {
                    CloseWorkorderRights = CloseWorkorderRightsExpression.ToString();

                }
            }
            //if (Application.Current.Properties.ContainsKey("workorderSubModuleListDialogues"))
            //{
            //    SubModule workorderSubModule = Application.Current.Properties["workorderSubModuleListDialogues"] as SubModule;
            //    if (workorderSubModule.listDialoges != null && workorderSubModule.listDialoges.Count > 0)
            //    {
            //        var WorkorderDialog = workorderSubModule.listDialoges.FirstOrDefault(i => i.DialogName == "WorkOrderDialog");
            //        if (WorkorderDialog != null && WorkorderDialog.listTab != null && WorkorderDialog.listTab.Count > 0)
            //        {
            //var TaskLabourTab = WorkorderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "TasksandLabor");
            //var InspectionTab = WorkorderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "WorkOrderInspections");
            //var ToolsTab = WorkorderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "Tools");
            //var PartsTab = WorkorderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "Parts");
            //var AttachmentTab = WorkorderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "Attachments");
            //Application.Current.Properties["TaskandLabourTabKey"] = TaskLabourTab.Expression;
            //Application.Current.Properties["InspectionTabKey"] = InspectionTab.Expression;
            //Application.Current.Properties["ToolsTabKey"] = ToolsTab.Expression;
            //Application.Current.Properties["PartsTabKey"] = PartsTab.Expression;
            //Application.Current.Properties["AttachmentTabKey"] = AttachmentTab.Expression;

            //var DetailsTab = WorkorderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "Details");
            //var WorkStartDateDetails = DetailsTab.listControls.FirstOrDefault(i => i.ControlName == "WorkStartedDate");
            //var WorkCompletionDateDetails = DetailsTab.listControls.FirstOrDefault(i => i.ControlName == "CompletionDate");
            //var AdditionalDetailsTab = WorkorderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "AdditionalDetails");
            //var CauseTab = WorkorderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "Causes");


            if (Application.Current.Properties.ContainsKey("WorkorderTargetKey"))
            {
                var TargetExpression = Application.Current.Properties["WorkorderTargetKey"].ToString();
                if (TargetExpression == "E")
                {
                    this.AssetIsVisible = true;
                }
                else if (TargetExpression == "V")
                {
                    this.AssetIsEnable = false;
                }
                else
                {
                    this.AssetIsVisible = false;
                }
            }

            if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic") || AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Professional"))
            {
                this.AssetSystemIsVisibleForLicensing = false;


            }
            if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic"))
            {
                this.LocationIsVisibleForLicensing = false;


            }

            if (Application.Current.Properties.ContainsKey("WorkorderDetailsControls"))
            {
                SubModule WorkorderDetails = Application.Current.Properties["WorkorderDetailsControls"] as SubModule;
                WorkorderControlsNew = WorkorderDetails.listControls;
            }

            if (Application.Current.Properties.ContainsKey("WorkOrderStartedDateKey"))
            {
                var StartdateExpression = Application.Current.Properties["WorkOrderStartedDateKey"].ToString();
                if (StartdateExpression != null && StartdateExpression == "E")
                {
                    this.WorkStartedDateIsVisible = true;

                }
                else if (StartdateExpression == "V")
                {
                    // this.WorkStartedDateIsVisible =true;
                    this.WorkStartedDateIsEnable = false;

                }
                else
                {
                    this.WorkStartedDateIsVisible = false;


                }
            }


            if (Application.Current.Properties.ContainsKey("WorkOrderCompletionDateKey"))
            {
                var CompletiondateExpression = Application.Current.Properties["WorkOrderCompletionDateKey"].ToString();
                if (CompletiondateExpression != null && CompletiondateExpression == "E")
                {
                    this.WorkorderCompletionDateIsVisible = true;

                }
                else if (CompletiondateExpression == "V")
                {
                    //this.WorkorderCompletionDateIsVisible = true;
                    this.WorkorderCompletionDateIsEnable = false;

                }
                else
                {
                    this.WorkorderCompletionDateIsVisible = false;


                }
            }
            if (Application.Current.Properties.ContainsKey("WorkorderAdditionalDetailsKey"))
            {
                var WorkorderAdditionalDetails = Application.Current.Properties["WorkorderAdditionalDetailsKey"].ToString();
                if (WorkorderAdditionalDetails != null && WorkorderAdditionalDetails == "E")
                {

                    this.AdditionalDetailsIsVisible = true;
                }
                else if (WorkorderAdditionalDetails != null && WorkorderAdditionalDetails == "V")
                {
                    this.AdditionalDetailsIsEnable = false;
                }
                else
                {
                    this.AdditionalDetailsIsVisible = false;
                }
            }
            if (Application.Current.Properties.ContainsKey("WorkOrderInternalNoteKey"))
            {
                var WorkorderInternalNotes = Application.Current.Properties["WorkOrderInternalNoteKey"].ToString();
                if (WorkorderInternalNotes != null && WorkorderInternalNotes == "E")
                {

                    this.InternalNotesIsVisible = true;
                }
                else if (WorkorderInternalNotes != null && WorkorderInternalNotes == "V")
                {
                    this.InternalNotesIsEnable = false;
                }
                else
                {
                    this.InternalNotesIsVisible = false;
                }
            }

            if (Application.Current.Properties.ContainsKey("WorkorderCauseKey"))
            {
                var WorkorderCauses = Application.Current.Properties["WorkorderCauseKey"].ToString();
                if (WorkorderCauses != null && WorkorderCauses == "E")
                {

                    this.CauseIsVisible = true;
                }
                else if (WorkorderCauses == "V")
                {
                    this.CauseIsEnable = false;
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
            //Because showing sometimes on page, That's why initializing in Initialize component
            //if (AppSettings.User.blackhawkLicValidator.ServiceRequestIsEnabled.Equals(true))
            //{
            //    if (Application.Current.Properties.ContainsKey("WorkOrderRequestedDateKey"))
            //    {
            //        var WorkOrderRequestedDate = Application.Current.Properties["WorkOrderRequestedDateKey"].ToString();
            //        if (WorkOrderRequestedDate != null && (WorkOrderRequestedDate == "E" || WorkOrderRequestedDate == "V"))
            //        {

            //            this.RequestedDateIsVisible = true;
            //        }
            //        else
            //        {
            //            this.RequestedDateIsVisible = false;
            //        }
            //    }
            //}







            #endregion




            #region Apply Control Typewise ordering.

            if (WorkorderControlsNew != null && WorkorderControlsNew.Count > 0)
            {

                #region Logic 1

                var sortedList = new List<FormControl>();

                //Add DateTime First
                foreach (var item in WorkorderControlsNew)
                {
                    if (item.DisplayFormat == "DateTime")
                    {
                        sortedList.Add(item);
                    }

                }

                //Add Except DateTime 
                foreach (var item in WorkorderControlsNew)
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

                    }

                }

                foreach (var item in WorkorderControlsNew)
                {
                    if (item.ControlName == "RequesterPhone")
                    {
                        sortedList.Add(item);
                    }
                    if (item.ControlName == "RequesterFullName")
                    {
                        sortedList.Add(item);
                    }

                }



                //ReAssign to WorkorderControlsNew
                WorkorderControlsNew = sortedList;

                #endregion



            }

            #endregion

            #region Remove Overridden controls from titles New

            if (WorkorderControlsNew != null && WorkorderControlsNew.Count > 0)
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




                    var WorkOrderNumber = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "WorkOrderNumber");
                    if (WorkOrderNumber != null)
                    {
                        WorkorderNumbeTitle = WorkOrderNumber.TargetName;
                        OverriddenControlsNew.Add(WorkOrderNumber);
                        WorkorderControlsNew.Remove(WorkOrderNumber);
                    }

                    var CurrentRuntime = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "CurrentRuntime");
                    if (CurrentRuntime != null)
                    {
                        CurrentRuntimeTitle = CurrentRuntime.TargetName;
                        OverriddenControlsNew.Add(CurrentRuntime);
                        CurrentRuntimeVisiblevalue = CurrentRuntime.Expression;
                        CurrentRuntimeEnablevalue = CurrentRuntime.Expression;

                    }

                    var JobNumber = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "JobNumber");
                    if (JobNumber != null)
                    {
                        JobNumberTitle = JobNumber.TargetName;
                        OverriddenControlsNew.Add(JobNumber);
                        WorkorderControlsNew.Remove(JobNumber);
                    }

                    var description = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "Description");
                    if (description != null)
                    {
                        DescriptionTitle = description.TargetName;
                        OverriddenControlsNew.Add(description);
                        WorkorderControlsNew.Remove(description);
                    }

                    var TotalTime = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "TotalTime");
                    if (TotalTime != null)
                    {
                        TotalTimeTilte = TotalTime.TargetName;
                        OverriddenControlsNew.Add(TotalTime);
                        WorkorderControlsNew.Remove(TotalTime);
                    }

                    var Originator = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "Originator");
                    if (Originator != null)
                    {
                        OriginatorTitle = Originator.TargetName;
                        OverriddenControlsNew.Add(Originator);
                        WorkorderControlsNew.Remove(Originator);
                    }

                    var DistributeCost = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "DistributeCost");
                    if (DistributeCost != null)
                    {
                        DistributeCostforAssetsystem = DistributeCost.TargetName;
                        OverriddenControlsNew.Add(DistributeCost);
                        WorkorderControlsNew.Remove(DistributeCost);
                    }

                    var RequiredDate = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "RequiredDate");
                    if (RequiredDate != null)
                    {
                        RequiredDateTitle = RequiredDate.TargetName;
                        OverriddenControlsNew.Add(RequiredDate);
                        WorkorderControlsNew.Remove(RequiredDate);
                    }

                    var WorkStartedDate = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "WorkStartedDate");
                    if (WorkStartedDate != null)
                    {
                        WorkStartedDateTitle = WorkStartedDate.TargetName;
                        OverriddenControlsNew.Add(WorkStartedDate);
                        WorkorderControlsNew.Remove(WorkStartedDate);
                    }

                    var CompletionDate = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "CompletionDate");
                    if (CompletionDate != null)
                    {
                        WorkorderCompletionDateTitle = CompletionDate.TargetName;
                        OverriddenControlsNew.Add(CompletionDate);
                        WorkorderControlsNew.Remove(CompletionDate);
                    }

                    var AssignedToEmployeeID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "AssignedToEmployeeID");
                    if (AssignedToEmployeeID != null)
                    {
                        AssignToEmployeeTitle = AssignedToEmployeeID.TargetName;
                        OverriddenControlsNew.Add(AssignedToEmployeeID);
                        WorkorderControlsNew.Remove(AssignedToEmployeeID);
                    }

                    var WorkOrderRequesterID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "WorkOrderRequesterID");
                    if (WorkOrderRequesterID != null)
                    {
                        WorkorderRequesterTitle = WorkOrderRequesterID.TargetName;
                        OverriddenControlsNew.Add(WorkOrderRequesterID);
                        WorkorderControlsNew.Remove(WorkOrderRequesterID);
                    }

                    var CostCenterID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "CostCenterID");
                    if (CostCenterID != null)
                    {
                        CostCenterTitle = CostCenterID.TargetName;
                        OverriddenControlsNew.Add(CostCenterID);
                        WorkorderControlsNew.Remove(CostCenterID);
                    }

                    var PriorityID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "PriorityID");
                    if (PriorityID != null)
                    {
                        PriorityTitle = PriorityID.TargetName;
                        OverriddenControlsNew.Add(PriorityID);
                        WorkorderControlsNew.Remove(PriorityID);
                    }

                    var ShiftID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "ShiftID");
                    if (ShiftID != null)
                    {
                        ShiftTitle = ShiftID.TargetName;
                        OverriddenControlsNew.Add(ShiftID);
                        WorkorderControlsNew.Remove(ShiftID);
                    }

                    var WorkOrderStatusID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "WorkOrderStatusID");
                    if (WorkOrderStatusID != null)
                    {
                        WorkorderStatusTitle = WorkOrderStatusID.TargetName;
                        OverriddenControlsNew.Add(WorkOrderStatusID);
                        WorkorderControlsNew.Remove(WorkOrderStatusID);
                    }

                    var WorkTypeID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "WorkTypeID");
                    if (WorkTypeID != null)
                    {
                        WorkorderTypeTitle = WorkTypeID.TargetName;
                        OverriddenControlsNew.Add(WorkTypeID);
                        WorkorderControlsNew.Remove(WorkTypeID);
                    }


                    var CauseID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "Causes");
                    if (CauseID != null)
                    {
                        CauseTitle = CauseID.TargetName;
                        OverriddenControlsNew.Add(CauseID);
                        WorkorderControlsNew.Remove(CauseID);
                    }


                    var MaintenanceCodeID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "MaintenanceCodeID");
                    if (MaintenanceCodeID != null)
                    {
                        MaintenanceCodeTitle = MaintenanceCodeID.TargetName;
                        OverriddenControlsNew.Add(MaintenanceCodeID);
                        WorkorderControlsNew.Remove(MaintenanceCodeID);
                    }


                    var EstimatedDowntime = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "EstimatedDowntime");
                    if (EstimatedDowntime != null)
                    {
                        EstimstedDowntimeTitle = EstimatedDowntime.TargetName;
                        OverriddenControlsNew.Add(EstimatedDowntime);
                        WorkorderControlsNew.Remove(EstimatedDowntime);
                    }

                    var ActualDowntime = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "ActualDowntime");
                    if (ActualDowntime != null)
                    {
                        ActualDowntimeTitle = ActualDowntime.TargetName;
                        OverriddenControlsNew.Add(ActualDowntime);
                        WorkorderControlsNew.Remove(ActualDowntime);
                    }

                    var MiscellaneousLaborCostID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "MiscellaneousLaborCostID");
                    if (MiscellaneousLaborCostID != null)
                    {
                        MiscellaneousLabourCostTitle = MiscellaneousLaborCostID.TargetName;
                        OverriddenControlsNew.Add(MiscellaneousLaborCostID);
                        WorkorderControlsNew.Remove(MiscellaneousLaborCostID);
                    }

                    var MiscellaneousMaterialsCostID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "MiscellaneousMaterialsCostID");
                    if (MiscellaneousMaterialsCostID != null)
                    {
                        MiscellaneousMaterialCostTitle = MiscellaneousMaterialsCostID.TargetName;
                        OverriddenControlsNew.Add(MiscellaneousMaterialsCostID);
                        WorkorderControlsNew.Remove(MiscellaneousMaterialsCostID);
                    }
                    var activationdateTitle = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "ActivationDate");
                    ActivationDateTitle = activationdateTitle.TargetName;
                    var requestedDateTitle = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "RequestedDate");
                    RequestedDateTitle = requestedDateTitle.TargetName;
                }
                catch (Exception ex)
                {


                }

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

                            case "WorkOrderNumber":
                                {

                                    WorkorderNumberIsVisible = ApplyIsVisible(item.Expression);
                                    break;

                                }

                            case "JobNumber":
                                {

                                    JobNumberIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }

                            case "Description":
                                {
                                    DescriptionIsEnable = ApplyIsEnable(item.Expression);
                                    DescriptionIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }


                            case "RequiredDate":
                                {
                                    RequiredDateIsEnable = ApplyIsEnable(item.Expression);
                                    RequiredDateIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }



                            case "WorkStartedDate":
                                {
                                    WorkStartedDateIsEnable = ApplyIsEnable(item.Expression);
                                    WorkStartedDateIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }


                            case "CompletionDate":
                                {
                                    WorkorderCompletionDateIsEnable = ApplyIsEnable(item.Expression);
                                    WorkorderCompletionDateIsVisible = ApplyIsVisible(item.Expression);
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
                                    if (AppSettings.User.blackhawkLicValidator.ServiceRequestIsEnabled.Equals(false))
                                    {
                                        this.WorkorderRequesterIsVisible = false;
                                        break;
                                    }
                                    if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic"))
                                    {
                                        this.WorkorderRequesterIsVisible = false;
                                        break;

                                    }
                                    WorkorderRequesterIsEnable = ApplyIsEnable(item.Expression);
                                    WorkorderRequesterIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }


                            case "CostCenterID":
                                {
                                    if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic") || AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Professional"))
                                    {
                                        this.CostCenterIsVisible = false;
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

                            //case "CurrentRuntime":
                            //    {
                            //        CurrentRuntimeIsEnable = ApplyIsEnable(item.Expression);
                            //        CurrentRuntimeIsVisible = ApplyIsVisible(item.Expression);
                            //        break;
                            //    }


                            case "ShiftID":
                                {
                                    if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic") || AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Professional"))
                                    {
                                        this.ShiftIsVisible = false;
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
                                    WorkorderTypeIsEnable = ApplyIsEnable(item.Expression);
                                    WorkorderTypeIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }


                            case "Causes":
                                {
                                    CauseIsEnable = ApplyIsEnable(item.Expression);
                                    CauseIsVisible = ApplyIsVisible(item.Expression);
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
                                    EstimstedDowntimeIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }

                            case "ActualDowntime":
                                {
                                    ActualDowntimeIsEnable = ApplyIsEnable(item.Expression);
                                    ActualDowntimeIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }

                            case "MiscellaneousLaborCostID":
                                {
                                    MiscellaneousLabourCostIsEnable = ApplyIsEnable(item.Expression);
                                    MiscellaneousLabourCostIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }

                            case "MiscellaneousMaterialsCostID":
                                {
                                    if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic"))
                                    {
                                        this.MiscellaneousMaterialCostIsVisible = false;
                                        break;

                                    }
                                    MiscellaneousMaterialCostIsEnable = ApplyIsEnable(item.Expression);
                                    MiscellaneousMaterialCostIsVisible = ApplyIsVisible(item.Expression);
                                    break;
                                }

                            case "DistributeCost":
                                {
                                    DistributeCostforAssetsystemIsVisible = ApplyIsVisible(item.Expression);
                                    DistributeCostforAssetsystemIsEnable = ApplyIsEnable(item.Expression);
                                    break;
                                }

                            case "Originator":
                                {
                                    DistributeCostforAssetsystemIsVisible = ApplyIsVisible(item.Expression);
                                    DistributeCostforAssetsystemIsEnable = ApplyIsEnable(item.Expression);
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

            #region Remove None visibility controls
            if (WorkorderControlsNew != null && WorkorderControlsNew.Count > 0)
            {
                ///Remove operator name and Work Permit Request///
                WorkorderControlsNew.RemoveAll((i => i.ControlName == "OperatorID"));
                WorkorderControlsNew.RemoveAll((i => i.ControlName == "WorkPermitRequestTime"));


                WorkorderControlsNew.RemoveAll(i => i.Expression == "N");
                WorkorderControlsNew.RemoveAll((i => i.ControlName == "ClosedDate"));
                WorkorderControlsNew.RemoveAll((i => i.ControlName == "ActivationDate"));

                WorkorderControlsNew.RemoveAll((i => i.ControlName == "CurrentRuntime"));
                WorkorderControlsNew.RemoveAll((i => i.ControlName == "RequestedDate"));
                if (AppSettings.User.blackhawkLicValidator.RiskAssasment.Equals(false))
                {
                    WorkorderControlsNew.RemoveAll((i => i.ControlName == "RiskQuestion"));
                }

                var workorderWrapper = await _workorderService.GetWorkorderByWorkorderID(UserID, WorkorderID.ToString());

                if (workorderWrapper != null && workorderWrapper.workOrderWrapper != null && workorderWrapper.workOrderWrapper.workOrder != null)
                {

                    var workorder = workorderWrapper.workOrderWrapper.workOrder;

                    if (!string.IsNullOrWhiteSpace(workorder.RiskQuestion))
                    {
                        this.RiskQuestion = workorder.RiskQuestion;
                    }
                }

                if (workorderWrapper != null && workorderWrapper.workOrderWrapper != null && workorderWrapper.workOrderWrapper.workOrder != null)
                {
                    var workorder = workorderWrapper.workOrderWrapper.workOrder;
                    if (AppSettings.User.blackhawkLicValidator.ServiceRequestIsEnabled.Equals(false))
                    {

                        WorkorderControlsNew.RemoveAll((i => i.ControlName == "RequestedDate"));
                        WorkorderControlsNew.RemoveAll((i => i.ControlName == "RequesterEmail"));
                        WorkorderControlsNew.RemoveAll((i => i.ControlName == "RequestNumber"));
                        this.RequestedDateIsVisible = false;

                    }
                    else
                    {
                        if (workorder.RequestNumber == null && workorder.RequestedDate == null)
                        {

                            WorkorderControlsNew.RemoveAll((i => i.ControlName == "RequestedDate"));

                            WorkorderControlsNew.RemoveAll((i => i.ControlName == "RequestNumber"));
                            this.RequestedDateIsVisible = false;
                        }
                        if (workorder.RequesterFullName == null)
                        {                           
                            WorkorderControlsNew.RemoveAll((i => i.ControlName == "RequesterFullName"));
                        }
                        if (workorder.RequesterPhone == null)
                        {
                            WorkorderControlsNew.RemoveAll((i => i.ControlName == "RequesterPhone"));
                        }
                        if (string.IsNullOrWhiteSpace(workorder.RequesterEmail))
                        {
                            WorkorderControlsNew.RemoveAll((i => i.ControlName == "RequesterEmail"));
                        }
                    }


                }
            }

            #endregion

            #region Generate and Bind Dyanmic controls New
            if (WorkorderControlsNew != null && WorkorderControlsNew.Count > 0)
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

                foreach (var item in WorkorderControlsNew)
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

        private bool ApplyIsEnable(ControlRole finalizedRole)
        {
            if (finalizedRole.RoleRight == ControlRight.Edit)
            {
                return true;
            }
            else
            {
                return false;
            }
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
            var control = new CustomPicker();
            control.Image = "unnamed";
            control.HeightRequest = 45;

            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                title.FontAttributes = FontAttributes.Bold;
            }
            title.TextColor = Color.Black;
            title.Text = formControl.TargetName; //+ "<<>>" + formControl.ControlName;

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
            // control.ItemsSource = formControl.listCombo;
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

                case "LOTOUrl":
                    {
                        if (control is Picker)
                        {
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.ClosedDateText));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(Lottourl)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                Lottourl = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.Lottourl));
                            control.IsEnabled = false;
                            control.BackgroundColor = Color.FromHex("#D0D3D4");
                            control.InputTransparent = true;
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            Lottourl = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.Lottourl), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.Lottourl), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }


                case "ClosedDate":
                    {
                        if (control is Picker)
                        {
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.ClosedDateText));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(ClosedDateText)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                ClosedDateText = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.ClosedDateText));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            ClosedDateText = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.ClosedDateText), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.ClosedDateText), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "AbnormalityID":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.AbnormalityText));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(AbnormalityText)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                AbnormalityText = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;
                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.AbnormalityText));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            AbnormalityText = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.AbnormalityText), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.AbnormalityText), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "ActualDowntime":
                    {
                        //if (control is Picker)
                        //{
                        //    var x = control as Picker;
                        //    control.SetBinding(Picker.SelectedItemProperty, nameof(this.ActualDowntimeText));
                        //}

                        //else if (control is Entry)
                        //{
                        //    control.SetBinding(Entry.TextProperty, nameof(this.ActualDowntimeText));
                        //}

                        //else if (control is DatePicker)
                        //{
                        //    // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                        //    ActualDowntimeText = DateTime.Now.ToString();
                        //    control.SetBinding(DatePicker.DateProperty, nameof(this.ActualDowntimeText));
                        //}

                        //else if (control is CustomDatePicker)
                        //{
                        //    control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.ActualDowntimeText));
                        //}
                        break;

                    }

                case "AmStepID":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.AmStepID));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(AmStepID)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                AmStepID = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;
                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.AmStepID));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            AmStepID = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.AmStepID), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.AmStepID), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "AnalysisPerformedDate":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.AnalysisPerformedDate));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(AnalysisPerformedDate)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                AnalysisPerformedDate = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;


                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.AnalysisPerformedDate));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            AnalysisPerformedDate = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.AnalysisPerformedDate), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.AnalysisPerformedDate), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "ConfirmEmail":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.ConfirmEmail));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(ConfirmEmail)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                ConfirmEmail = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;
                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.ConfirmEmail));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            ConfirmEmail = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.ConfirmEmail), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.ConfirmEmail), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "CountermeasuresDefinedDate":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.CountermeasuresDefinedDate));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(CountermeasuresDefinedDate)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                CountermeasuresDefinedDate = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.CountermeasuresDefinedDate));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            CountermeasuresDefinedDate = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.CountermeasuresDefinedDate), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.CountermeasuresDefinedDate), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "CurrentRuntime":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.CurrentRuntime));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(CurrentRuntime)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                CurrentRuntime = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;
                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.CurrentRuntime));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            CurrentRuntime = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.CurrentRuntime), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.CurrentRuntime), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "DiagnosticTime":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.DiagnosticTime));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(DiagnosticTime)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                DiagnosticTime = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;
                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.DiagnosticTime));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            DiagnosticTime = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.DiagnosticTime), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.DiagnosticTime), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "DigitalSignatures":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.DigitalSignatures));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(DigitalSignatures)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                DigitalSignatures = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.DigitalSignatures));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            DigitalSignatures = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.DigitalSignatures), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.DigitalSignatures), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "RiskQuestion":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.DiagnosticTime));

                            Button riskbutton = new Button();
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;
                            x.IsEnabled = false;
                            x.BackgroundColor = Color.FromHex("#D0D3D4");
                            x.InputTransparent = true;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try
                            {
                                item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(RiskQuestion));

                            }
                            catch (Exception)
                            {

                            }

                            if (item != null)
                            {

                                x.SelectedItem = item;
                                RiskQuestion = item.SelectedText.ToString();


                            }


                        }
                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.RiskQuestion));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            RiskQuestion = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.RiskQuestion), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.RiskQuestion), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "ImplementationValidatedDate":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.ImplementationValidatedDate));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(ImplementationValidatedDate)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                ImplementationValidatedDate = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;
                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.ImplementationValidatedDate));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            ImplementationValidatedDate = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.ImplementationValidatedDate), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.ImplementationValidatedDate), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "InitialWaitTime":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.InitialWaitTime));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(InitialWaitTime)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                InitialWaitTime = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.InitialWaitTime));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            InitialWaitTime = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.InitialWaitTime), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.InitialWaitTime), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "JobNumber":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.JobNumber));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(JobNumber)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                JobNumber = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.JobNumber));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            JobNumber = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.JobNumber), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.JobNumber), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }


                case "ApprovalLevel":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.JobNumber));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(ApprovalLevel)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                ApprovalLevel = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.ApprovalLevel));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            ApprovalLevel = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.ApprovalLevel), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.ApprovalLevel), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }
                case "ApprovalNumber":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.JobNumber));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(ApprovalNumber)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                ApprovalNumber = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.ApprovalNumber));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            ApprovalNumber = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.ApprovalNumber), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.ApprovalNumber), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }


                case "NotificationTime":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.NotificationTime));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(NotificationTime)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                NotificationTime = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;
                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.NotificationTime));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            NotificationTime = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.NotificationTime), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.NotificationTime), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "PartWaitingTime":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.PartWaitingTime));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(PartWaitingTime)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                PartWaitingTime = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.PartWaitingTime));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            PartWaitingTime = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.PartWaitingTime), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.PartWaitingTime), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "ProblemID":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.ProblemID));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(ProblemID)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                ProblemID = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.ProblemID));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            ProblemID = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.ProblemID), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.ProblemID), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "project":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.Project));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(Project)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                Project = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;
                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.Project));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            Project = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.Project), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.Project), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "RelatedToID":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.RelatedToID));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(RelatedToID)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                RelatedToID = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.RelatedToID));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            RelatedToID = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.RelatedToID), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.RelatedToID), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "RepairingOrReplacementTime":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.RepairingOrReplacementTime));


                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(RepairingOrReplacementTime)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                RepairingOrReplacementTime = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.RepairingOrReplacementTime));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            RepairingOrReplacementTime = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.RepairingOrReplacementTime), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.RepairingOrReplacementTime), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                //case "RequestedDate":
                //    {
                //        if (control is Picker)
                //        {
                //            //var x = control as Picker;
                //            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.RequestedDate));

                //            var x = control as Picker;
                //            x.ClassId = formControl.ControlName;

                //            var source = x.ItemsSource as List<ComboDD>;
                //            ComboDD item = null;
                //            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(RequestedDate)); }
                //            catch (Exception) { }

                //            if (item != null)
                //            {
                //                x.SelectedItem = item;
                //                RequestedDate = item.SelectedValue.ToString();
                //            }

                //            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                //        }

                //        else if (control is Entry)
                //        {
                //            control.SetBinding(Entry.TextProperty, nameof(this.RequestedDate));
                //        }

                //        else if (control is DatePicker)
                //        {
                //            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                //            RequestedDate = DateTime.Now;
                //            control.SetBinding(DatePicker.DateProperty, nameof(this.RequestedDate), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                //            control.IsEnabled = false;
                //            control.BackgroundColor = Color.Gray;
                //        }

                //        else if (control is CustomDatePicker)
                //        {
                //            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.RequestedDate), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                //            control.IsEnabled = false;
                //            control.BackgroundColor = Color.Gray;
                //        }
                //        break;

                //    }

                case "RequesterEmail":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.RequesterEmail));

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
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.RequesterFullName));

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
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.RequesterPhone));


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
                            var x = control as Entry;
                            control.SetBinding(Entry.TextProperty, nameof(this.RequesterPhone));
                            //x.TextChanged += RequestPhone_TextChanged;


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
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.RequestNumber));


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
                            control.IsEnabled = false;
                            control.BackgroundColor = Color.FromHex("#D0D3D4");
                            control.InputTransparent = true;

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

                case "ServiceRequestModeID":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.ServiceRequestModeID));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(ServiceRequestModeID)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                ServiceRequestModeID = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.ServiceRequestModeID));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            ServiceRequestModeID = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.ServiceRequestModeID), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.ServiceRequestModeID), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "StartupTime":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.StartupTime));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(StartupTime)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                StartupTime = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.StartupTime));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            StartupTime = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.StartupTime), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.StartupTime), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "TotalTime":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.TotalTime));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(TotalTime)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                TotalTime = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.TotalTime));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            TotalTime = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.TotalTime), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.TotalTime), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "UnsafeConditionID":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.UnsafeConditionID));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(UnsafeConditionID)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                UnsafeConditionID = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;


                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.UnsafeConditionID));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            UnsafeConditionID = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.UnsafeConditionID), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UnsafeConditionID), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
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

                //case "UserField21":
                //    {
                //        if (control is Picker)
                //        {
                //            var x = control as Picker;
                //            x.ClassId = formControl.ControlName;

                //            var source = x.ItemsSource as List<ComboDD>;
                //            ComboDD item = null;
                //            try { item = source.FirstOrDefault(s => s.SelectedText.Trim() == UserField21.Trim()); }
                //            catch (Exception) { }

                //            if (item != null)
                //            {
                //                x.SelectedItem = item;

                //                UserField21 = item.SelectedText.ToString();
                //            }

                //            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                //        }

                //        else if (control is Entry)
                //        {
                //            control.SetBinding(Entry.TextProperty, nameof(this.UserField21));
                //        }

                //        else if (control is DatePicker)
                //        {
                //            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                //            UserField21 = DateTime.Now.ToString();
                //            control.SetBinding(DatePicker.DateProperty, nameof(this.UserField21), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                //        }

                //        else if (control is CustomDatePicker)
                //        {
                //            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField21), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                //        }
                //        break;

                //    }


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




                case "WorkOrderNumber":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.WorkOrderNumber));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(WorkOrderNumber)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                WorkOrderNumber = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;


                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.WorkOrderNumber));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            WorkOrderNumber = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.WorkOrderNumber));
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.WorkOrderNumber));
                        }
                        break;

                    }


                default:
                    break;
            }
        }


        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {

            var picker = sender as Picker;


            switch (picker.ClassId)
            {

                //case "ActivationDate":
                //    {
                //        this.ActivationDateText = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                //        break;
                //    }

                case "ClosedDate":
                    {
                        this.ClosedDateText = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;
                    }

                case "AbnormalityID":
                    {
                        this.AbnormalityText = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;
                    }

                case "ActualDowntime":
                    {
                        //this.ActualDowntimeText = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;
                    }

                case "AmStepID":
                    {
                        this.AmStepID = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;
                    }

                case "AnalysisPerformedDate":
                    {
                        this.AnalysisPerformedDate = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;
                    }

                case "ConfirmEmail":
                    {
                        this.ConfirmEmail = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;
                    }

                case "CountermeasuresDefinedDate":
                    {
                        this.CountermeasuresDefinedDate = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;
                    }

                case "CurrentRuntime":
                    {
                        this.CurrentRuntime = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;
                    }

                case "DiagnosticTime":
                    {
                        this.DiagnosticTime = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;
                    }

                case "DigitalSignatures":
                    {
                        this.DigitalSignatures = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;
                    }

                case "EstimatedDowntime":
                    {
                        //this.EstimatedDowntime = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;
                    }

                case "ImplementationValidatedDate":
                    {
                        this.ImplementationValidatedDate = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;
                    }

                case "InitialWaitTime":
                    {
                        this.InitialWaitTime = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;
                    }

                case "JobNumber":
                    {
                        this.JobNumber = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;

                    }

                case "NotificationTime":
                    {
                        this.NotificationTime = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;

                    }

                case "PartWaitingTime":
                    {
                        this.PartWaitingTime = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;

                    }

                case "ProblemID":
                    {
                        this.ProblemID = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;

                    }

                case "project":
                    {
                        this.Project = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;

                    }

                case "RelatedToID":
                    {
                        this.RelatedToID = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;

                    }

                case "RepairingOrReplacementTime":
                    {
                        this.RepairingOrReplacementTime = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;

                    }

                //case "RequestedDate":
                //    {
                //        this.RequestedDate = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                //        break;

                //    }

                case "RequesterEmail":
                    {
                        this.RequesterEmail = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;

                    }

                case "RequesterFullName":
                    {
                        this.RequesterFullName = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;

                    }

                case "RequesterPhone":
                    {
                        this.RequesterPhone = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;

                    }

                case "RequestNumber":
                    {
                        this.RequestNumber = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;

                    }

                case "ServiceRequestModeID":
                    {
                        this.ServiceRequestModeID = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;

                    }

                case "StartupTime":
                    {
                        this.StartupTime = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;

                    }

                case "TotalTime":
                    {
                        this.TotalTime = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;

                    }

                case "UnsafeConditionID":
                    {
                        this.UnsafeConditionID = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;

                    }

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






                case "WorkOrderNumber":
                    {
                        this.WorkOrderNumber = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;

                    }


                default:
                    break;
            }


        }

        [Obsolete]
        public async Task<ControlRole> ParseControlRoleExpressionWithFormsRoles(string controlExpression, List<Roles> formRoles)
        {

            try
            {
                List<ControlRole> ParsedResult = new List<ControlRole>();

                var controlRoles = controlExpression.Split('|');
                foreach (var formRole in formRoles)
                {
                    foreach (var controlRole in controlRoles)
                    {
                        int position = controlRole.IndexOf("-");
                        if (position < 0)
                            continue;

                        var s1 = controlRole.Substring(0, position);
                        var controlRoleName = s1.Split(':')[1];
                        var controlRoleRight = controlRole.Substring(position + 1);


                        if (string.Equals(controlRoleName, formRole.RoleName, StringComparison.OrdinalIgnoreCase))
                        {

                            var role = new ControlRole();
                            role.RoleName = controlRoleName;
                            switch (controlRoleRight)
                            {
                                case "E":
                                    {
                                        role.RoleRight = ControlRight.Edit;
                                    }
                                    break;
                                case "V":
                                    {
                                        role.RoleRight = ControlRight.View;
                                    }
                                    break;
                                case "N":
                                    {
                                        role.RoleRight = ControlRight.None;
                                    }
                                    break;
                            }
                            ParsedResult.Add(role);


                        }
                    }
                }

                // Apply role Precedence
                var sortedList = ParsedResult.OrderBy(x => (int)(x.RoleRight)).ToList();
                if (sortedList != null && sortedList.Count > 0)
                {
                    return sortedList[0];
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                return null;
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
                //var grid = page.Content as Grid;
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
                if (CloseWorkorderRights == "E")
                {
                    var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() { CloseWorkorderTitle, LogoutTitle });

                    if (response == LogoutTitle)
                    {
                        await _authenticationService.LogoutAsync();
                        await NavigationService.NavigateToAsync<LoginPageViewModel>();
                        await NavigationService.RemoveBackStackAsync();
                    }

                    if (response == CloseWorkorderTitle)
                    {
                        // TODO: Create funtionality for CloseWorkorder.

                        await CloseWorkorder();

                    }
                }
                else if (CloseWorkorderRights == "V")
                {
                    var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() { CloseWorkorderTitle, LogoutTitle });

                    if (response == LogoutTitle)
                    {
                        await _authenticationService.LogoutAsync();
                        await NavigationService.NavigateToAsync<LoginPageViewModel>();
                        await NavigationService.RemoveBackStackAsync();
                    }

                    if (response == CloseWorkorderTitle)
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


        public async Task OnViewAppearingAsync(VisualElement view)
        {
            try
            {


                ///TODO: Need to check on Appearing editWorkorder page whether picker data is requested or wokorder data need to refresh 
                /// In Every ShowFacility or other picker page we have to true this flag and on its callback we have to false it.


                OperationInProgress = true;

                if (!IsPickerDataSubscribed)
                {
                    //Retrive Facility
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.FacilityRequested, OnFacilityRequested);

                    //Retrive Location
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.LocationRequested, OnLocationRequested);


                    //Retrive Asset
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.AssetRequested, OnAssetRequested);


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
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.CauseRequested, OnCauseRequested);

                    //Retrive WorkorderType
                    MessagingCenter.Subscribe<object>(this, MessengerKeys.MaintenanceCodeRequested, OnMaintenanceCodeRequested);

                    IsPickerDataSubscribed = true;
                }

                else if (IsPickerDataRequested)
                {
                    ////Retrive Facility
                    //MessagingCenter.Subscribe<object>(this, MessengerKeys.FacilityRequested, OnFacilityRequested);

                    ////Retrive Location
                    //MessagingCenter.Subscribe<object>(this, MessengerKeys.LocationRequested, OnLocationRequested);


                    ////Retrive Asset
                    //MessagingCenter.Subscribe<object>(this, MessengerKeys.AssetRequested, OnAssetRequested);


                    ////Retrive Asset System
                    //MessagingCenter.Subscribe<object>(this, MessengerKeys.AssetSyastemRequested, OnAssetSystemRequested);


                    ////Retrive AssignTo
                    //MessagingCenter.Subscribe<object>(this, MessengerKeys.AssignToRequested, OnAssignToRequested);

                    ////Retrive WorkorderRequester
                    //MessagingCenter.Subscribe<object>(this, MessengerKeys.WorkorderRequesterRequested, OnWorkorderRequesterRequested);


                    ////Retrive Cost Center
                    //MessagingCenter.Subscribe<object>(this, MessengerKeys.CostCenterRequested, OnCostCenterRequested);


                    ////Retrive Priority
                    //MessagingCenter.Subscribe<object>(this, MessengerKeys.PriorityRequested, OnPriorityRequested);

                    ////Retrive Shifts
                    //MessagingCenter.Subscribe<object>(this, MessengerKeys.ShiftRequested, OnShiftRequested);

                    ////Retrive WorkorderStatus
                    //MessagingCenter.Subscribe<object>(this, MessengerKeys.WorkorderStatusRequested, OnWorkorderStatusRequested);

                    ////Retrive WorkorderType
                    //MessagingCenter.Subscribe<object>(this, MessengerKeys.WorkorderTypeRequested, OnWorkorderTypeRequested);

                    ////Retrive WorkorderType
                    //MessagingCenter.Subscribe<object>(this, MessengerKeys.CauseRequested, OnCauseRequested);

                    ////Retrive WorkorderType
                    //MessagingCenter.Subscribe<object>(this, MessengerKeys.MaintenanceCodeRequested, OnMaintenanceCodeRequested);

                    IsPickerDataRequested = false;
                    return;
                }


                /// Retrive Workorder Data and Set Control properties
                /// 
             //   await SetControlsPropertiesForPage(workOrderWra);

                ///TODO: Get Workorder Labour data 
                var workorderLabourWrapper = await _workorderService.GetWorkorderLabour(UserID, WorkorderID.ToString());


                ///TODO: Get Workorder data 
                var workorderWrapper = await _workorderService.GetWorkorderByWorkorderID(UserID, WorkorderID.ToString());



                ///TODO: Get Inspection 
                //var Inspection = await _workorderService.GetWorkorderInspection(WorkorderID.ToString());

                var Inspection = await _workorderService.GetWorkorderInspection(this.WorkorderID.ToString(), UserID);
                List<InspectionTOAnswers> listtoAnswer = new List<InspectionTOAnswers>();
                foreach (var item in Inspection.workOrderEmployee)
                {

                    listtoAnswer.Add(new InspectionTOAnswers()
                    {

                        StartDate = item.StartDate,
                        CompletionDate = item.CompletionDate,

                    });
                }
                foreach (var item in Inspection.workorderContractor)
                {
                    listtoAnswer.Add(new InspectionTOAnswers()
                    {

                        StartDate = item.StartDate,
                        CompletionDate = item.CompletionDate,

                    });
                }

                if (listtoAnswer != null && listtoAnswer.Count > 0)
                {
                    MinInspectionStartDate = listtoAnswer.Min(i => i.StartDate).ToString();
                    MaxInspectionStartDate = listtoAnswer.Max(i => i.StartDate).ToString();
                    MaxInspectionCompDate = listtoAnswer.Max(i => i.CompletionDate).ToString();
                    MinInspectionCompDate = listtoAnswer.Min(i => i.CompletionDate).ToString();
                    MaxInspectionCompDateforNull = listtoAnswer.Any(i => i.CompletionDate == null).ToString();
                    InspctionStartDateforNull = listtoAnswer.Any(i => i.StartDate == null).ToString();

                }
                else
                {
                    MinInspectionStartDate = string.Empty;
                    MaxInspectionStartDate = string.Empty;
                    MaxInspectionCompDate = string.Empty;
                    MinInspectionCompDate = string.Empty;
                    MaxInspectionCompDateforNull = string.Empty;
                    InspctionStartDateforNull = string.Empty;
                }
                /////TODO: Get Inspection Start and Completiondate
                //// var InspectionTime = await _workorderService.GetWorkorderInspectionTime(UserID, WorkorderID.ToString());

                ////if (Application.Current.Properties.ContainsKey("MinimumInspectionStartDate"))
                ////{
                ////    try
                ////    {
                ////        var minInspectionStartDate = Application.Current.Properties["MinimumInspectionStartDate"].ToString();
                ////        if (minInspectionStartDate != null)
                ////        {
                ////            MinInspectionStartDate = minInspectionStartDate.ToString();

                ////        }
                ////    }
                ////    catch (Exception ex)
                ////    {

                ////        MinInspectionStartDate = null;
                ////    }

                ////}
                ////if (Application.Current.Properties.ContainsKey("MaximumInspectionCompletionDate"))
                ////{
                ////    try
                ////    {
                ////        var maxInspectionCompDate = Application.Current.Properties["MaximumInspectionCompletionDate"].ToString();
                ////        if (maxInspectionCompDate != null)
                ////        {
                ////            MaxInspectionCompDate = maxInspectionCompDate.ToString();

                ////        }
                ////    }
                ////    catch (Exception ex)
                ////    {

                ////        MaxInspectionCompDate = null;
                ////    }

                ////}
                //if (Application.Current.Properties.ContainsKey("MaximumInspectionCompletionDateforNull"))
                //{
                //    try
                //    {
                //        var maxInspectionCompDate = Application.Current.Properties["MaximumInspectionCompletionDateforNull"].ToString();
                //        if (maxInspectionCompDate != null)
                //        {
                //            MaxInspectionCompDateforNull = maxInspectionCompDate.ToString();

                //        }
                //    }
                //    catch (Exception ex)
                //    {

                //        MaxInspectionCompDateforNull = null;
                //    }

                //}


                //TODO:
                #region Auto Fill Start Date and completion date from inspection


                //if (InspectionUser == true) //checks whether User have inspection role or not
                //{
                if (Inspection.listInspection != null && Inspection.listInspection.Count > 0) //checks whether inspection present in workorder or not
                {


                    #region Auto Fill Start Date from inspection

                    //TODO: if the wo start date is null and inspection start date is not null than mask the wo start date with inspection start date.
                    if (Convert.ToBoolean(workorderWrapper.workOrderWrapper.IsCheckedAutoFillStartdateOnTaskAndLabor) && workorderWrapper.workOrderWrapper.workOrder.WorkStartedDate == null)
                    {
                        if (InspctionStartDateforNull == "True")
                        {
                            this.WorkStartedDateWarningTextIsVisible = false;
                            if (workorderWrapper.workOrderWrapper.workOrder.WorkStartedDate == null)
                            {
                                WorkStartedDate1 = null;
                            }


                        }
                        else
                        {

                            if (!string.IsNullOrWhiteSpace(MinInspectionStartDate))
                            {
                                WorkStartedDateWarningText = WebControlTitle.GetTargetNameByTitleName("OriginalWorkStartedDateisnotfilled");
                                WorkStartedDate1 = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(MinInspectionStartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                                this.WorkStartedDateWarningTextIsVisible = true;
                            }
                            else
                            {
                                WorkStartedDate1 = null;
                                this.WorkStartedDateWarningTextIsVisible = false;
                            }
                        }
                    }

                    #endregion


                    #region Auto Fill Completion date from inspection

                    var inspectionCompletionDate = MaxInspectionCompDate;
                    var workorderCompletiontDate = workorderWrapper.workOrderWrapper.workOrder.CompletionDate;
                    var IsAutoFillOnCompletionDate = workorderWrapper.workOrderWrapper.IsCheckedAutoFillCompleteOnTaskAndLabor;


                    if (Convert.ToBoolean(IsAutoFillOnCompletionDate))
                    {
                        if (MaxInspectionCompDateforNull == "True")
                        {
                            this.WorkorderCompletionDateWarningTextIsVisible = false;
                            if (workorderWrapper.workOrderWrapper.workOrder.CompletionDate == null)
                            {
                                WorkorderCompletionDate = null;
                            }


                        }
                        else
                        {
                            if (workorderWrapper.workOrderWrapper.workOrder.CompletionDate != null && workorderWrapper.workOrderWrapper.workOrder.CompletionDate.GetValueOrDefault().Date < Convert.ToDateTime(MaxInspectionCompDate).Date)
                            {
                                WorkorderCompletionDateWarningText = WebControlTitle.GetTargetNameByTitleName("OriginalCompletionDateis") +
                                                        "  " +
                                                        DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderWrapper.workOrderWrapper.workOrder.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("d");
                                this.WorkorderCompletionDateWarningTextIsVisible = true;
                            }
                            else if (workorderWrapper.workOrderWrapper.workOrder.CompletionDate == null && !string.IsNullOrWhiteSpace(MaxInspectionCompDate))
                            {
                                WorkorderCompletionDateWarningText = WebControlTitle.GetTargetNameByTitleName("OriginalCompletionDateisnotfilled");
                                this.WorkorderCompletionDateWarningTextIsVisible = true;
                            }


                            if (Convert.ToBoolean(IsAutoFillOnCompletionDate) && workorderCompletiontDate != null)
                            {
                                if (inspectionCompletionDate != null)
                                {
                                    //// If inspection completion and wo completion date are not null than mask the wo completion date when inspection completion
                                    //// date is greater than wo completion date.      
                                    if (Convert.ToDateTime(MaxInspectionCompDate).Date > workorderCompletiontDate.GetValueOrDefault().Date.Date)
                                    {
                                        WorkorderCompletionDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(inspectionCompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                                        //CompletionDate1.IsVisible = true;
                                        //ShowCompletionDate.IsVisible = false;
                                        //lblAutoText.IsVisible = true;
                                    }
                                }


                            }
                            if (Convert.ToBoolean(IsAutoFillOnCompletionDate) && workorderCompletiontDate == null)
                            {
                                //// If wo completion date is null than fill the inspection completion date.
                                if (!string.IsNullOrWhiteSpace(inspectionCompletionDate))
                                {
                                    WorkorderCompletionDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(inspectionCompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                                    //CompletionDate1.IsVisible = true;
                                    //ShowCompletionDate.IsVisible = false;
                                    //lblAutoText.IsVisible = true;
                                }
                                else ////If wo completion date and inspection completion date are null than null the wo completion date.
                                {
                                    //CompletionDate1.IsVisible = false;
                                    //ShowCompletionDate.IsVisible = true;
                                    //lblAutoText.IsVisible = false;
                                    WorkorderCompletionDate = null;
                                    WorkorderCompletionDateWarningText = null;
                                }



                            }
                        }

                    }

                    #endregion



                }


                // }


                #endregion





                if (Convert.ToBoolean(workorderWrapper.workOrderWrapper.IsCheckedAutoFillStartdateOnTaskAndLabor) && workorderWrapper.workOrderWrapper.workOrder.WorkStartedDate == null)
                {

                    if (workorderLabourWrapper.workOrderWrapper.InitialStartDate != null)
                    {

                        WorkStartedDateWarningText = WebControlTitle.GetTargetNameByTitleName("OriginalWorkStartedDateisnotfilled");
                        this.WorkStartedDateWarningTextIsVisible = true;


                    }
                    else
                    {
                        //WorkStartedDateWarningText = WebControlTitle.GetTargetNameByTitleName("OriginalWorkStartedDateisnotfilled");
                    }

                    if (workorderLabourWrapper.workOrderWrapper.InitialStartDate != null)
                    {

                        WorkStartedDate1 = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderLabourWrapper.workOrderWrapper.InitialStartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                        // WorkStartedDate1.Date = wklabour.Result.workOrderWrapper.InitialStartDate.GetValueOrDefault().Date;
                        //WorkStartedDate1.IsVisible = true;
                        //ShowCompletionDate1.IsVisible = false;
                        //lblAutoText1.IsVisible = true;
                    }
                }


                if (Convert.ToBoolean(workorderWrapper.workOrderWrapper.IsCheckedAutoFillCompleteOnTaskAndLabor))
                {

                    //if (workorderWrapper.workOrderWrapper.workOrder.CompletionDate != null)
                    //{

                    //}
                    //else
                    //{

                    //}


                    if (Convert.ToBoolean(workorderWrapper.workOrderWrapper.IsCheckedAutoFillCompleteOnTaskAndLabor) && workorderWrapper.workOrderWrapper.workOrder.CompletionDate != null)
                    {


                        if ((workorderLabourWrapper.workOrderWrapper.workOrderLabors.Count > 0))
                        {


                            foreach (var item in workorderLabourWrapper.workOrderWrapper.workOrderLabors)
                            {
                                bool taskDates = workorderLabourWrapper.workOrderWrapper.workOrderLabors.Any(a => a.CompletionDate == null);

                                if (taskDates == true)
                                {

                                    WorkorderCompletionDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderWrapper.workOrderWrapper.workOrder.CompletionDate.GetValueOrDefault()).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                                    WorkorderCompletionDateWarningText = null;
                                    break;


                                }

                                else
                                {
                                    DateTime FinalCompdate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderLabourWrapper.workOrderWrapper.FinalCompletionDate.GetValueOrDefault()).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                                    DateTime wkcompdt = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderWrapper.workOrderWrapper.workOrder.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                                    if (workorderLabourWrapper.workOrderWrapper.FinalCompletionDate != null && FinalCompdate.Date > wkcompdt.Date)
                                    {

                                        WorkorderCompletionDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderLabourWrapper.workOrderWrapper.FinalCompletionDate.GetValueOrDefault()).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                                        WorkorderCompletionDateWarningText = WebControlTitle.GetTargetNameByTitleName("OriginalCompletionDateis") + "  " + DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderWrapper.workOrderWrapper.workOrder.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("d");
                                        this.WorkorderCompletionDateWarningTextIsVisible = true;
                                        break;



                                    }
                                    else
                                    {
                                        WorkorderCompletionDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderWrapper.workOrderWrapper.workOrder.CompletionDate.GetValueOrDefault()).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);

                                        WorkorderCompletionDateWarningText = null;
                                        break;

                                    }
                                }
                            }
                        }
                    }
                    else if (Convert.ToBoolean(workorderWrapper.workOrderWrapper.IsCheckedAutoFillCompleteOnTaskAndLabor) && workorderWrapper.workOrderWrapper.workOrder.CompletionDate == null)
                    {


                        if ((workorderLabourWrapper.workOrderWrapper.workOrderLabors.Count > 0))
                        {


                            foreach (var item in workorderLabourWrapper.workOrderWrapper.workOrderLabors)
                            {
                                bool taskDates = workorderLabourWrapper.workOrderWrapper.workOrderLabors.Any(a => a.CompletionDate == null);

                                if (taskDates == true)
                                {
                                    //CompletionDate1.IsVisible = false;
                                    //ShowCompletionDate.IsVisible = true;
                                    //lblAutoText.IsVisible = false;
                                    WorkorderCompletionDate = null;
                                    WorkorderCompletionDateWarningText = null;
                                    break;
                                }

                                else
                                {

                                    if (workorderLabourWrapper.workOrderWrapper.FinalCompletionDate != null)
                                    {
                                        WorkorderCompletionDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderLabourWrapper.workOrderWrapper.FinalCompletionDate.GetValueOrDefault()).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                                        WorkorderCompletionDateWarningText = WebControlTitle.GetTargetNameByTitleName("OriginalCompletionDateisnotfilled");
                                        this.WorkorderCompletionDateWarningTextIsVisible = true;
                                        break;

                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (workorderLabourWrapper.workOrderWrapper.FinalCompletionDate != null && workorderWrapper.workOrderWrapper.workOrder.CompletionDate != null)
                        {
                            var workorderComletionDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderWrapper.workOrderWrapper.workOrder.CompletionDate.GetValueOrDefault().Date).ToUniversalTime(), ServerTimeZone);
                            var FinalTaskCompletionDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderLabourWrapper.workOrderWrapper.FinalCompletionDate.GetValueOrDefault().Date).ToUniversalTime(), ServerTimeZone);

                            if (FinalTaskCompletionDate.Date > workorderComletionDate.Date)
                            {
                                WorkorderCompletionDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderLabourWrapper.workOrderWrapper.FinalCompletionDate.Value).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                                //CompletionDate1.IsVisible = true;
                                //ShowCompletionDate.IsVisible = false;

                                //lblAutoText.IsVisible = true;

                            }


                        }
                        if (workorderLabourWrapper.workOrderWrapper.FinalCompletionDate != null && workorderWrapper.workOrderWrapper.workOrder.CompletionDate != null) //ShowCompletionDate.IsVisible == false)
                        {

                            var workorderComletionDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderWrapper.workOrderWrapper.workOrder.CompletionDate.GetValueOrDefault().Date).ToUniversalTime(), ServerTimeZone);
                            var FinalTaskCompletionDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderLabourWrapper.workOrderWrapper.FinalCompletionDate.GetValueOrDefault().Date).ToUniversalTime(), ServerTimeZone);

                            if (FinalTaskCompletionDate.Date > workorderComletionDate.Date)
                            {
                                WorkorderCompletionDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderLabourWrapper.workOrderWrapper.FinalCompletionDate.Value).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);

                                //lblAutoText.IsVisible = true;

                            }

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


        public async Task SetControlsPropertiesForPage(ServiceOutput workorderWrapper)
        {

            var grid = new Grid();

            //grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(40, GridUnitType.Star) });

            var topLeft = new Label { Text = "Top Left" };
            var topMiddle = new Label { Text = "Top Right" };
            var topRight = new Label { Text = "Top Right" };


            grid.Children.Add(topLeft, 0, 0);
            grid.Children.Add(topMiddle, 1, 0);
            grid.Children.Add(topRight, 0, 1);


            if (workorderWrapper != null && workorderWrapper.workOrderWrapper != null && workorderWrapper.workOrderWrapper.workOrder != null)
            {
                bool fdasignatureKey = AppSettings.User.blackhawkLicValidator.IsFDASignatureValidation;

                if (fdasignatureKey == true)
                {

                    if (AppSettings.User.RequireSignaturesForValidation == "True")
                    {
                        SignaturesIsVisible = true;
                        if (workorderWrapper.workOrderWrapper.SignatureAuditDetails != null)
                        {
                            foreach (var item in workorderWrapper.workOrderWrapper.SignatureAuditDetails)
                            {


                                SignatureText += item.Signature + "                     " + item.SignatureTimestamp + "                             " + item.SignatureIntent + Environment.NewLine;

                            }
                        }


                    }
                }

                var workorder = workorderWrapper.workOrderWrapper.workOrder;

                Application.Current.Properties["TaskOrInspection"] = workorderWrapper.workOrderWrapper._IsWorkOrderHasTaskORInspection;


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

                this.WorkorderNumberText = workorder.WorkOrderNumber;

                this.JobNumberText = workorder.JobNumber;
                if (!string.IsNullOrWhiteSpace(this.JobNumberText))
                {
                    this.JobNumberFlag = true;
                }

                if (workorder.TotalTime != null)
                {
                    this.TotalTimeText = workorder.TotalTime;
                }
                else
                {
                    this.TotalTimeText = null;
                }

                this.DescriptionText = workorder.Description;

                if (!string.IsNullOrWhiteSpace(workorder.Description) && workorder.Description.Length >= 150)
                {
                    MoreDescriptionTextIsEnable = true;
                }

                OriginatorName = workorder.Originator;
                this.DescriptionMoreText = workorder.Description;
                this.DescriptionText = workorder.Description;


                if (!string.IsNullOrWhiteSpace(workorder.AdditionalDetails))
                {
                    //if (Device.Idiom == TargetIdiom.Phone)
                    //{
                    //    //this.AdditionalDetailsTextForMobile = RemoveHTML.StripHTML(workorder.AdditionalDetails);
                    //    this.AdditionalDetailsText = RemoveHTML.StripHTML(workorder.AdditionalDetails);
                    //}
                    //else
                    //{
                    this.AdditionalDetailsText = RemoveHTML.StripHTML(workorder.AdditionalDetails);
                    if (workorder.AdditionalDetails.Length >= 150)
                    {
                        MoreAdditionalDetailsTextIsEnable = true;
                    }
                    this.AdditionalDetailsMoreText = this.AdditionalDetailsText;
                    this.AdditionalDetailsText = this.AdditionalDetailsText;
                    // }

                }


                this.ApprovalLevel = workorder.ApprovalLevel;
                this.ApprovalNumber = workorder.ApprovalNumber;

                //this.RequiredDate = workorder.RequiredDate;

                // DateTime date = DateTimeConverter.ConvertDateTimeToDifferentTimeZone((workorder.RequestedDate ?? DateTime.Now).ToUniversalTime(), AppSettings.User.ServerIANATimeZone); //ServerTimeZone);

                //this.WorkStartedDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(abc.Result.workOrderWrapper.workOrder.WorkStartedDate).ToUniversalTime(), ServerTimeZone);  //workorder.WorkStartedDate;

                this.RequiredDate1 = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorder.RequiredDate ?? DateTime.Now).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                //this.MinimumRequiredDate= DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorder.RequiredDate).ToUniversalTime(), AppSettings.User.TimeZone);
                /// Workorder Start Date Property Set
                if (workorder.WorkStartedDate == null)
                {
                    this.WorkStartedDate1 = null;
                    this.MaximumWorkStartedDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);

                }
                else
                {
                    this.WorkStartedDate1 = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorder.WorkStartedDate ?? DateTime.Now).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
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


                if (!string.IsNullOrEmpty(workorder.Description) && workorder.Description.Length >= 150)
                {
                    MoreInternalNoteTextIsEnable = true;
                }
                InternalNoteMoreText = workorder.InternalNote;
                this.InternalNoteText = workorder.InternalNote;

                /// Set Targets and Other
                /// 
                if (!string.IsNullOrEmpty(workorder.FacilityName))
                {
                    FacilityName = ShortString.shorten(workorder.FacilityName);
                }
                else
                {
                    FacilityName = workorder.FacilityName;
                }
                FacilityID = workorder.FacilityID;

                if (!string.IsNullOrEmpty(workorder.LocationName))
                {
                    LocationName = ShortString.shorten(workorder.LocationName);

                }
                else
                {
                    LocationName = workorder.LocationName;

                }
                LocationID = workorder.LocationID;

                if (!string.IsNullOrEmpty(workorder.AssetName))
                {
                    AssetName = ShortString.shorten(workorder.AssetName);

                }
                else
                {
                    AssetName = workorder.AssetName;

                }
                AssetID = workorder.AssetID;
                if (AssetID != null)
                {
                    var AssetWrapper = await _assetService.GetAssetsBYAssetID(this.AssetID.ToString(), AppSettings.User.UserID);


                    if (string.IsNullOrWhiteSpace(AssetWrapper.assetWrapper.asset.CurrentRuntime.ToString()))
                    {
                        this.CurrentRuntimeText = "0.0";


                    }
                    else
                    {
                        this.CurrentRuntimeText = string.Format(StringFormat.NumericZero(), string.IsNullOrWhiteSpace(AssetWrapper.assetWrapper.asset.CurrentRuntime.ToString()) ? 0 : decimal.Parse(AssetWrapper.assetWrapper.asset.CurrentRuntime.ToString()));
                       // this.CurrentRuntimeText = string.Format(StringFormat.NumericZero(), AssetWrapper.assetWrapper.asset.CurrentRuntime.ToString());

                    }
                }
                else
                {
                    CurrentRuntimeIsVisible = false;
                }

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
                AssetSystemID = workorder.AssetSystemID;

                if (!string.IsNullOrEmpty(workorder.EmployeeName))
                {
                    AssignToEmployeeName = ShortString.shorten(workorder.EmployeeName);

                }
                else
                {
                    AssignToEmployeeName = workorder.EmployeeName;

                }
                AssignToEmployeeID = workorder.AssignedToEmployeeID;
                string targetname = string.Empty;
                if (workorder.AssetID != null)
                {
                    targetname = "Assets";
                }
                else if (workorder.AssetSystemID != null)
                {
                    targetname = "Asset System";
                }
                else
                {
                    targetname = "Location";
                }

                //Set Target String/////////
                TargetName = "(" + targetname + ")" + " " + AppSettings.User.CompanyName + " " + ">>" + " " + workorder.FacilityName + ">>" + workorder.LocationName;
                ActivationDateText = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorder.ActivationDate ?? DateTime.Now).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                //if (!string.IsNullOrEmpty(ActivationDateText.ToString()))
                //{
                //   ActivationDateText = Convert.ToDateTime(ActivationDateText.ToString("0:MMM d, yyyy"));
                //}

                if (!string.IsNullOrEmpty(workorder.CostCenterName))
                {
                    CostCenterName = ShortString.shorten(workorder.CostCenterName);

                }
                else
                {
                    CostCenterName = workorder.CostCenterName;

                }
                // ActivationDateText = workorder.ActivationDate.ToString();
                CostCenterID = workorder.CostCenterID;

                if (workorderWrapper.workOrderWrapper.Cause != null && workorderWrapper.workOrderWrapper.Cause.Count > 0)
                {
                    if (!string.IsNullOrEmpty(workorderWrapper.workOrderWrapper.Cause[0].CauseNumber))
                    {
                        CauseName = ShortString.shorten(workorderWrapper.workOrderWrapper.Cause[0].CauseNumber);

                    }
                    else
                    {
                        CauseName = workorderWrapper.workOrderWrapper.Cause[0].CauseNumber;

                    }
                    CauseID = workorderWrapper.workOrderWrapper.Cause[0].CauseID;
                }

                if (!string.IsNullOrEmpty(workorder.WorkOrderRequesterName))
                {
                    WorkorderRequesterName = ShortString.shorten(workorder.WorkOrderRequesterName);

                }
                else
                {
                    WorkorderRequesterName = workorder.WorkOrderRequesterName;

                }
                WorkorderRequesterID = workorder.WorkOrderRequesterID;

                if (!string.IsNullOrEmpty(workorder.ShiftName))
                {
                    ShiftName = ShortString.shorten(workorder.ShiftName);

                }
                else
                {
                    ShiftName = workorder.ShiftName;

                }
                ShiftID = workorder.ShiftID;

                if (!string.IsNullOrEmpty(workorder.WorkOrderStatusName))
                {
                    WorkorderStatusName = ShortString.shorten(workorder.WorkOrderStatusName);

                }
                else
                {
                    WorkorderStatusName = workorder.WorkOrderStatusName;

                }
                WorkorderStatusID = workorder.WorkOrderStatusID;

                if (!string.IsNullOrEmpty(workorder.WorkTypeName))
                {
                    WorkorderTypeName = ShortString.shorten(workorder.WorkTypeName);

                }
                else
                {
                    WorkorderTypeName = workorder.WorkTypeName;

                }
                WorkorderTypeID = workorder.WorkTypeID;

                if (!string.IsNullOrEmpty(workorder.MaintenanceCodeName))
                {
                    MaintenanceCodeName = ShortString.shorten(workorder.MaintenanceCodeName);

                }
                else
                {
                    MaintenanceCodeName = workorder.MaintenanceCodeName;

                }
                MaintenanceCodeID = workorder.MaintenanceCodeID;

                if (!string.IsNullOrEmpty(workorder.PriorityName))
                {
                    PriorityName = ShortString.shorten(workorder.PriorityName);

                }
                else
                {
                    PriorityName = workorder.PriorityName;

                }
                PriorityID = workorder.PriorityID;


                //EstimstedDowntimeText = string.Format(StringFormat.NumericZero(),workorder.EstimatedDowntime);
                //ActualDowntimeText = string.Format(StringFormat.NumericZero(), string.IsNullOrWhiteSpace(workorder.ActualDowntime) ? 0 : decimal.Parse(workorder.ActualDowntime));

                EstimstedDowntimeText = workorder.EstimatedDowntime == null ? "0" : workorder.EstimatedDowntime;
                ActualDowntimeText = workorder.ActualDowntime == null ? "0" : workorder.ActualDowntime;
                MiscellaneousLabourCostText = string.Format(StringFormat.CurrencyZero(), workorder.MiscellaneousLaborCost == null ? 0 : workorder.MiscellaneousLaborCost);
                MiscellaneousMaterialCostText = string.Format(StringFormat.CurrencyZero(), workorder.MiscellaneousMaterialsCost == null ? 0 : workorder.MiscellaneousMaterialsCost);
                Lottourl = workorder.LOTOUrl;

                ///Set Dyanmic Field Propertiesf
                ///
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
                #endregion


                #region New Properties 31.5.2018


                ///TODO: Set all properties here for those custom control is made.

                ConfirmEmail = workorder.ConfirmEmail;
                DigitalSignatures = workorder.DigitalSignatures;
                JobNumber = workorder.JobNumber;
                Project = workorder.project;

                ///Service Request Fields
                RequesterFullName = workorder.RequesterFullName;
                RequesterEmail = workorder.RequesterEmail;
                RequesterPhone = workorder.RequesterPhone;
                if (workorder.RequestNumber != null && workorder.RequestedDate != null)
                {
                    RequestNumber = workorder.RequestNumber;
                    //  RequestedDate = workorder.RequestedDate.ToString();
                    RequestedDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorder.RequestedDate ?? DateTime.Now).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);

                }







                #endregion







                #endregion



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

                //  OperationInProgress = false;

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
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleaseselectthefacilityfirst"), 2000);
                    return;

                }

                await NavigationService.NavigateToAsync<LocationListSelectionPageViewModel>(FacilityID);
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();

                //  OperationInProgress = false;

            }

            finally
            {
                UserDialogs.Instance.HideLoading();

                //OperationInProgress = false;

            }
        }

        public async Task ShowAssets()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                // OperationInProgress = true;
                IsPickerDataRequested = true;
                if (FacilityID == null)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleaseselectthefacilityfirst"), 2000);
                    return;

                }
                await NavigationService.NavigateToAsync<AssetListSelectionPageViewModel>(new TargetNavigationData() { FacilityID = this.FacilityID, LocationID = this.LocationID }); //Pass the control here
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();

                //  OperationInProgress = false;

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
                // OperationInProgress = true;
                IsPickerDataRequested = true;
                if (FacilityID == null)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleaseselectthefacilityfirst"), 2000);
                    return;

                }
                await NavigationService.NavigateToAsync<AssetSystemListSelectionPageViewModel>(new TargetNavigationData() { FacilityID = this.FacilityID, LocationID = this.LocationID }); //Pass the control here
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

                //  OperationInProgress = false;

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

                //  OperationInProgress = false;

            }

            finally
            {
                UserDialogs.Instance.HideLoading();

                // OperationInProgress = false;

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

                // OperationInProgress = false;

            }
        }

        public async Task ShowPriority()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                // OperationInProgress = true;
                IsPickerDataRequested = true;
                await NavigationService.NavigateToAsync<PriorityListSelectionPageViewModel>(new TargetNavigationData() { }); //Pass the control here
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

        public async Task ShowShift()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                // OperationInProgress = true;
                IsPickerDataRequested = true;
                await NavigationService.NavigateToAsync<ShiftListSelectionPageViewModel>(new TargetNavigationData() { }); //Pass the control here
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

        public async Task ShowWorkorderStatus()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                // OperationInProgress = true;
                IsPickerDataRequested = true;
                await NavigationService.NavigateToAsync<WorkorderStatusListSelectionPageViewModel>(new TargetNavigationData() { }); //Pass the control here
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();

                //   OperationInProgress = false;

            }

            finally
            {
                UserDialogs.Instance.HideLoading();

                // OperationInProgress = false;

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

        public async Task ShowCause()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                //OperationInProgress = true;
                IsPickerDataRequested = true;
                await NavigationService.NavigateToAsync<CauseListSelectionPageViewModel>(new TargetNavigationData() { }); //Pass the control here
            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();

                //OperationInProgress = false;

            }

            finally
            {
                UserDialogs.Instance.HideLoading();

                //  OperationInProgress = false;

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

                //  OperationInProgress = false;

            }

            finally
            {
                UserDialogs.Instance.HideLoading();

                //  OperationInProgress = false;

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
                this.LocationName = ShortString.shorten(location.LocationName);


                // if Location is selected reset the  Asset and Asset System
                ResetAsset();
                ResetAssetSystem();

            }


        }

        private async void OnAssetRequested(object obj)
        {

            if (obj != null)
            {

                var asset = obj as TAsset;
                this.AssetID = asset.AssetID;
                this.AssetName = ShortString.shorten(asset.AssetName);

                if (this.AssetID == null || this.AssetID == 0)
                {
                    CurrentRuntimeIsVisible = false;

                }

                else
                {
                    var AssetWrapper = await _assetService.GetAssetsBYAssetID(this.AssetID.ToString(), AppSettings.User.UserID);

                    if (string.IsNullOrWhiteSpace(AssetWrapper.assetWrapper.asset.CurrentRuntime.ToString()))
                    {
                        this.CurrentRuntimeText = "0.0";


                    }

                    else
                    {
                        this.CurrentRuntimeText = AssetWrapper.assetWrapper.asset.CurrentRuntime.ToString();

                    }

                    if (CurrentRuntimeVisiblevalue == "E")
                    {
                        CurrentRuntimeIsVisible = true;
                    }
                    else if (CurrentRuntimeVisiblevalue == "V")
                    {
                        CurrentRuntimeIsEnable = false;
                    }
                    else
                    {
                        CurrentRuntimeIsVisible = false;
                    }
                }


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

        private void OnAssignToRequested(object obj)
        {

            if (obj != null)
            {

                var assignTo = obj as ComboDD;
                this.AssignToEmployeeID = assignTo.SelectedValue;
                this.AssignToEmployeeName = ShortString.shorten(assignTo.SelectedText);
            }


        }

        private void OnWorkorderRequesterRequested(object obj)
        {

            if (obj != null)
            {

                var requester = obj as ComboDD;
                this.WorkorderRequesterID = requester.SelectedValue;
                this.WorkorderRequesterName = ShortString.shorten(requester.SelectedText);
            }


        }


        private void OnCostCenterRequested(object obj)
        {

            if (obj != null)
            {

                var costCenter = obj as ComboDD;
                this.CostCenterID = costCenter.SelectedValue;
                this.CostCenterName = ShortString.shorten(costCenter.SelectedText);
            }


        }


        private void OnPriorityRequested(object obj)
        {

            if (obj != null)
            {

                var priority = obj as ComboDD;
                this.PriorityID = priority.SelectedValue;
                this.PriorityName = ShortString.shorten(priority.SelectedText);
            }


        }

        private void OnShiftRequested(object obj)
        {

            if (obj != null)
            {

                var shift = obj as ComboDD;
                this.ShiftID = shift.SelectedValue;
                this.ShiftName = ShortString.shorten(shift.SelectedText);
            }


        }

        private void OnWorkorderStatusRequested(object obj)
        {

            if (obj != null)
            {

                var workorderStatus = obj as ComboDD;
                this.WorkorderStatusID = workorderStatus.SelectedValue;
                this.WorkorderStatusName = ShortString.shorten(workorderStatus.SelectedText);
            }


        }

        private void OnWorkorderTypeRequested(object obj)
        {

            if (obj != null)
            {

                var workorderType = obj as ComboDD;
                this.WorkorderTypeID = workorderType.SelectedValue;
                this.WorkorderTypeName = ShortString.shorten(workorderType.SelectedText);
            }


        }

        private void OnCauseRequested(object obj)
        {

            if (obj != null)
            {

                var cause = obj as Cause;
                this.CauseID = cause.CauseID;
                this.CauseName = ShortString.shorten(cause.CauseNumber);

                this.Cause = cause;
            }


        }

        private void OnMaintenanceCodeRequested(object obj)
        {

            if (obj != null)
            {

                var maintenanceCode = obj as ComboDD;
                this.MaintenanceCodeID = maintenanceCode.SelectedValue;
                this.MaintenanceCodeName = ShortString.shorten(maintenanceCode.SelectedText);
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
            var nullLocation = new TAsset() { AssetID = 0, AssetName = string.Empty };
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


        public async Task SaveWorkorder()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                //OperationInProgress = true;


                ///TODO: Apply Requester Email Validation. 
                ///Its a dynamic field Validations can't be applied or Need type information to apply validation.

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
                var validationResult = await ValidateControlsIsRequired(WorkorderControlsNew);
                if (validationResult.FailedItem != null)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(validationResult.ErrorMessage);
                    return;
                }

                if (Application.Current.Properties.ContainsKey("IsCheckedCauseKey"))
                {
                    string IsCheckedCause = Application.Current.Properties["IsCheckedCauseKey"].ToString();
                    if (!string.IsNullOrWhiteSpace(IsCheckedCause))
                    {
                        if (IsCheckedCause == "True" && CauseID == null)
                        {
                            UserDialogs.Instance.HideLoading();
                            DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("PleasefilltheCause"));
                            return;
                        }
                    }
                }


                ///TODO: Get Workorder Labour data 
                var workorderLabourWrapper = await _workorderService.GetWorkorderLabour(UserID, WorkorderID.ToString());


                ///TODO: Get Workorder data 
                var workorderWrapper = await _workorderService.GetWorkorderByWorkorderID(UserID, WorkorderID.ToString());


                //#region check if all answers of a sections are required

                //// check if all answers of a sections are required///////
                //if (Convert.ToBoolean(workorderWrapper.workOrderWrapper != null && WorkorderCompletionDate != null))
                //{
                //    if (Convert.ToBoolean(workorderWrapper.workOrderWrapper.sections != null && workorderWrapper.workOrderWrapper.sections.Count > 0))
                //    {
                //        StringBuilder RequiredSection = new StringBuilder();

                //        foreach (var item in workorderWrapper.workOrderWrapper.sections)
                //        {
                //            RequiredSection.Append(item.SectionName);
                //            RequiredSection.Append(",");
                //        }

                //        await DialogService.ShowAlertAsync(RequiredSection.ToString().TrimEnd(','), WebControlTitle.GetTargetNameByTitleName("PleaseProvideFollowingSectionQuestionAnswer"), "OK");
                //        return;


                //    }
                //}

                //#endregion


                #region Check WorkOrder Identified through Breakdown
                if (Convert.ToBoolean(workorderWrapper.workOrderWrapper.WorkOrderIdentifiedThroughBreakdownFlag) && MaintenanceCodeName == "IDENTIFIED THROUGH BREAKDOWN")
                {
                    string expression = @"^(\()?[0-9]+(?>,[0-9]{3})*(?>\.[0-9]{3})?(?<!^[0\.]+)$";

                    string actualDowntimeText = string.Empty;
                    if (ActualDowntimeText != null)
                    {
                        actualDowntimeText = Convert.ToString(ActualDowntimeText);
                    }

                    string estimstedDowntimeText = string.Empty;
                    if (EstimstedDowntimeText != null)
                    {
                        estimstedDowntimeText = Convert.ToString(EstimstedDowntimeText);
                    }
                    if (EstimstedDowntimeText==null || !(Regex.IsMatch(estimstedDowntimeText, expression)))
                    {

                        DialogService.ShowToast("EstimatedDowntime Is Required");
                        return;
                    }
                    if (ActualDowntimeText==null || !(Regex.IsMatch(actualDowntimeText, expression)))
                    {
                        DialogService.ShowToast("ActualDowntime Is Required");
                        return;
                    }
                    if (String.IsNullOrWhiteSpace(UserField21))
                    {
                        DialogService.ShowToast("UserField21 Is Required");
                        return;
                    }
                    if (String.IsNullOrWhiteSpace(PriorityName))
                    {
                        DialogService.ShowToast("PriorityName Is Required");
                        return;
                    }
                }
                #endregion

                ///TODO: Get Workorder data and check IsCheckedAutoFillStartdateOnTaskAndLabor
                ///If True and Workorder startdate is  null and the initial start date of Task and labour is not null
                ///then display message PleaseFirstClearTaskstartdate from task and Labour then you can clear the 
                ///Workorder start date.
                if (Convert.ToBoolean(workorderWrapper.workOrderWrapper.IsCheckedAutoFillStartdateOnTaskAndLabor) && WorkStartedDate1 == null)
                {
                    if (workorderLabourWrapper.workOrderWrapper.InitialStartDate != null)
                    {
                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("PleaseFirstClearTaskstartdate"), 2000);
                        //await DisplayAlert("", formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "PleaseFirstClearTaskstartdate").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);

                        //TODO: set StartDAte as previous
                        if (workorderWrapper.workOrderWrapper.workOrder.WorkStartedDate != null)
                        {
                            WorkStartedDate1 = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderWrapper.workOrderWrapper.workOrder.WorkStartedDate.GetValueOrDefault()).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                        }

                        UserDialogs.Instance.HideLoading();

                        return;
                    }

                }



                ///Need To check
                ///TODO: Get Workorder data and check IsCheckedAutoFillCompleteOnTaskAndLabor
                ///
                if (Convert.ToBoolean(workorderWrapper.workOrderWrapper.IsCheckedAutoFillCompleteOnTaskAndLabor) && !(WorkorderCompletionDate == null))
                {
                    if ((workorderLabourWrapper.workOrderWrapper.workOrderLabors.Count > 0))
                    {
                        foreach (var item in workorderLabourWrapper.workOrderWrapper.workOrderLabors)
                        {
                            var dt = workorderLabourWrapper.workOrderWrapper.workOrderLabors.Where(a => a.CompletionDate == null);
                            int cnt = dt.Count();
                            if (cnt == 0)
                            {
                                break;
                            }

                            else if (WorkorderCompletionDate == null && cnt > 0)
                            {
                                UserDialogs.Instance.HideLoading();

                                //await DisplayAlert("", formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "CompletiondateCannotbepriorthanworkorderlabourcompletiondate").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                                DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("CompletiondateCannotbepriorthanworkorderlabourcompletiondate"), 2000);
                                return;
                            }
                            else
                            {

                                //if (workorderLabourWrapper.workOrderWrapper.FinalCompletionDate != null)
                                //{
                                //    CompletionDate1.IsVisible = true;
                                //    break;
                                //}
                            }
                        }
                    }
                }



                string FinalStartDate = string.Empty;
                string FinalCompDate = string.Empty;
                string InitialStDate = string.Empty;
                if (workorderLabourWrapper.workOrderWrapper.InitialStartDate != null)
                {
                    InitialStDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderLabourWrapper.workOrderWrapper.InitialStartDate).ToUniversalTime(), ServerTimeZone).ToString("d");
                }
                if (workorderLabourWrapper.workOrderWrapper.FinalCompletionDate != null)
                {
                    FinalCompDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderLabourWrapper.workOrderWrapper.FinalCompletionDate).ToUniversalTime(), ServerTimeZone).ToString("d");
                }
                if (workorderLabourWrapper.workOrderWrapper.FinalStartDate != null)
                {
                    FinalStartDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderLabourWrapper.workOrderWrapper.FinalStartDate).ToUniversalTime(), ServerTimeZone).ToString("d");
                }


                //TODO:
                #region Dates Validation from inspection.


                ///  abc            >> workorder data >>> workorderWrapper
                ///  InspectionTime >> data of inspectionTime 
                ///  Inspection     >> Inspection Date
                ///  


                ///TODO: Get Inspection 
                var Inspection = await _workorderService.GetWorkorderInspection(WorkorderID.ToString(), AppSettings.User.UserID.ToString());

                List<InspectionTOAnswers> liststartAnswer = new List<InspectionTOAnswers>();
                List<InspectionTOAnswers> listcompletionAnswer = new List<InspectionTOAnswers>();
                foreach (var item in Inspection.workOrderEmployee)
                {
                    if (item.StartDate != null)
                    {
                        liststartAnswer.Add(new InspectionTOAnswers()
                        {

                            StartDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.StartDate).ToUniversalTime(), ServerTimeZone),

                        });
                    }
                    else
                    {
                        liststartAnswer.Add(new InspectionTOAnswers()
                        {

                            StartDate = item.StartDate,

                        });
                    }

                    if (item.CompletionDate != null)
                    {
                        listcompletionAnswer.Add(new InspectionTOAnswers()
                        {


                            CompletionDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.CompletionDate).ToUniversalTime(), ServerTimeZone),

                        });
                    }
                    else
                    {
                        listcompletionAnswer.Add(new InspectionTOAnswers()
                        {


                            CompletionDate = item.CompletionDate,

                        });
                    }

                }
                foreach (var item in Inspection.workorderContractor)
                {
                    if (item.StartDate != null)
                    {
                        liststartAnswer.Add(new InspectionTOAnswers()
                        {

                            StartDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.StartDate).ToUniversalTime(), ServerTimeZone),

                        });
                    }
                    else
                    {
                        liststartAnswer.Add(new InspectionTOAnswers()
                        {

                            StartDate = item.StartDate,

                        });
                    }
                    if (item.CompletionDate != null)
                    {
                        listcompletionAnswer.Add(new InspectionTOAnswers()
                        {


                            CompletionDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.CompletionDate).ToUniversalTime(), ServerTimeZone),

                        });
                    }
                    else
                    {
                        listcompletionAnswer.Add(new InspectionTOAnswers()
                        {


                            CompletionDate = item.CompletionDate,

                        });
                    }
                }
                if (liststartAnswer != null && liststartAnswer.Count > 0)
                {
                    liststartAnswer.RemoveAll(x => x.StartDate == null);


                }
                if (listcompletionAnswer != null && listcompletionAnswer.Count > 0)
                {

                    listcompletionAnswer.RemoveAll(x => x.CompletionDate == null);


                }
                ///TODO: Get Inspection Time 
                //  var InspectionTime = await _workorderService.GetWorkorderInspectionTime(UserID, WorkorderID.ToString());



                //TODO: apply the time zone. (Done)
                //if (InspectionUser ?? false) //checks whether User have inspection role or not
                //{
                string inspectionStartDate = null;
                string inspectionCompletionDate = null;
                if (Inspection.listInspection != null && Inspection.listInspection.Count > 0) //checks whether inspection present in workorder or not
                {


                    //if (!string.IsNullOrWhiteSpace(MaxInspectionStartDate))
                    //{
                    //    inspectionStartDate = Convert.ToDateTime(MinInspectionStartDate).ToString();
                    //}

                    //if (!string.IsNullOrWhiteSpace(MinInspectionCompDate))
                    //{
                    //    inspectionCompletionDate = Convert.ToDateTime(Max).ToString();
                    //}



                    var IsAutoFillOnCompletionDate = workorderWrapper.workOrderWrapper.IsCheckedAutoFillCompleteOnTaskAndLabor;

                    DateTime? workorderCompletiontDate = null;
                    DateTime? workorderStartDate = null;

                    if (WorkorderCompletionDate != null)
                    {
                        workorderCompletiontDate = Convert.ToDateTime(WorkorderCompletionDate);
                    }
                    if (WorkStartedDate1 != null)
                    {
                        workorderStartDate = Convert.ToDateTime(WorkStartedDate1);
                    }


                    #region General validation with inspection

                    // for start date picker 
                    if (WorkStartedDate1 != null)  //(!string.IsNullOrWhiteSpace(WorkStartedDate1.Text))//if (ShowCompletionDate1.IsVisible == false) //if start date picker is visible
                    {
                        if (workorderStartDate != null && liststartAnswer != null && liststartAnswer.Count > 0)
                        {
                            //inspectionStartDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(inspectionStartDate).ToUniversalTime(), ServerTimeZone).ToString();
                            if (liststartAnswer.Any(x => x.StartDate.Value.Date < workorderStartDate.Value.Date))
                            {

                                //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, WebControlTitle.GetTargetNameByTitleName(formLoadInputs, "WOstartdatecannotgreaterthanInpectionstartdate"), formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                                DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("WOstartdatecannotgreaterthanInpectionstartdate"), 2000);
                                UserDialogs.Instance.HideLoading();

                                return;
                            }
                        }


                        if (workorderStartDate != null && listcompletionAnswer != null && listcompletionAnswer.Count > 0)
                        {
                            //inspectionCompletionDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(inspectionCompletionDate).ToUniversalTime(), ServerTimeZone).ToString();
                            if (listcompletionAnswer.Any(x => x.CompletionDate.Value.Date < workorderStartDate.Value.Date))
                            {

                                //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, WebControlTitle.GetTargetNameByTitleName(formLoadInputs, "WOstartdatecannotgreaterthanInpectioncompletiondate"), formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                                DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("WOstartdatecannotgreaterthanInpectioncompletiondate"), 2000);
                                UserDialogs.Instance.HideLoading();

                                return;
                            }
                        }



                    }


                    //for end date picker
                    if (workorderCompletiontDate != null) //(!string.IsNullOrWhiteSpace(CompletionDate1.Text))  //if (ShowCompletionDate.IsVisible == false)
                    {
                        if (workorderCompletiontDate != null && liststartAnswer != null && liststartAnswer.Count > 0)
                        {
                            //inspectionStartDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(inspectionStartDate).ToUniversalTime(), ServerTimeZone).ToString();
                            if (liststartAnswer.Any(x => x.StartDate.Value.Date > workorderCompletiontDate.Value.Date))
                            {

                                //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, WebControlTitle.GetTargetNameByTitleName(formLoadInputs, "WOcompletiondatecannotlessthanInpectionstartdate"), formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                                DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("WOcompletiondatecannotlessthanInpectionstartdate"), 2000);
                                UserDialogs.Instance.HideLoading();

                                return;
                            }
                        }

                        if (listcompletionAnswer != null && listcompletionAnswer.Count > 0 && workorderCompletiontDate != null)
                        {
                            //inspectionCompletionDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(inspectionCompletionDate).ToUniversalTime(), ServerTimeZone).ToString();
                            if (listcompletionAnswer.Any(x => x.CompletionDate.Value.Date > workorderCompletiontDate.Value.Date))
                            {

                                //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, WebControlTitle.GetTargetNameByTitleName(formLoadInputs, "WOcompletiondatecannotlessthanInpectioncompletiondate"), formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                                DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("WOcompletiondatecannotlessthanInpectioncompletiondate"), 2000);
                                UserDialogs.Instance.HideLoading();

                                return;
                            }
                        }
                    }

                    #endregion



                    #region Start date clear validation on autofilltask&Labour
                    if (bool.Parse(workorderWrapper.workOrderWrapper.IsCheckedAutoFillStartdateOnTaskAndLabor))
                    {
                        var workorder = workorderWrapper.workOrderWrapper.workOrder;

                        if (workorderWrapper.workOrderWrapper.workOrder.WorkStartedDate != null)
                        {
                            // If inspection start date is filled than prevent user to remove wo start date
                            if (liststartAnswer != null && liststartAnswer.Any(x => x.StartDate != null) && WorkStartedDate1 == null)//ShowCompletionDate1.IsVisible == true)
                            {
                                DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("PleasefirstclearInspectionstartdatetoclearWorkorderstartdate"), 2000);

                                //await DisplayAlert("", WebControlTitle.GetTargetNameByTitleName(formLoadInputs, "PleasefirstclearInspectionstartdatetoclearWorkorderstartdate"), formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                                UserDialogs.Instance.HideLoading();

                                return;
                            }
                        }
                    }

                    #endregion

                    #region Completion date clear validation on autofilltask&Labour                       
                    if (bool.Parse(workorderWrapper.workOrderWrapper.IsCheckedAutoFillCompleteOnTaskAndLabor))
                    {

                    }

                    #endregion


                }


                // }

                #endregion

                #region Start date and End Date validation


                if (WorkStartedDate1 != null && WorkorderCompletionDate != null)
                {
                    if (WorkorderCompletionDate.Value.Date < WorkStartedDate1.Value.Date)
                    {
                        UserDialogs.Instance.HideLoading();

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("WorkOrderCompletiondatecannotbepriorfromWorkOrderStartDate"), 2000);
                        return;
                    }
                }

                if (WorkStartedDate1 != null)
                {
                    if (workorderLabourWrapper.workOrderWrapper.InitialStartDate != null && WorkStartedDate1.Value.Date > DateTime.Parse(InitialStDate)) //wklabour.Result.workOrderWrapper.InitialStartDate.GetValueOrDefault().Date)
                    {
                        UserDialogs.Instance.HideLoading();

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Workstartdatecannotbeexceedfromstartdateontaskandlaborrecord"), 2000);
                        return;
                    }
                }
                if (WorkorderCompletionDate != null)
                {
                    if (workorderLabourWrapper.workOrderWrapper.FinalCompletionDate != null && WorkorderCompletionDate.Value.Date < DateTime.Parse(FinalCompDate)) //wklabour.Result.workOrderWrapper.FinalCompletionDate.GetValueOrDefault().Date)
                    {
                        UserDialogs.Instance.HideLoading();

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Workordercompletiondatecannotbepriorfromcompletiondateontaskandlaborrecord"), 2000);
                        return;
                    }
                }

                if (WorkorderCompletionDate != null)
                {
                    if (workorderLabourWrapper.workOrderWrapper.FinalStartDate != null && WorkorderCompletionDate.Value.Date < DateTime.Parse(FinalStartDate)) //wklabour.Result.workOrderWrapper.FinalStartDate.GetValueOrDefault().Date)
                    {
                        UserDialogs.Instance.HideLoading();

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("WorkOrderCompletiondatecannotbepriorfromStartDateonTaskandLabortab"), 2000);
                        return;
                    }

                }


                #endregion
                if (AssetID == 0)
                {
                    AssetID = null;
                }
                bool IsValidPhoneNumber = IsPhoneNumber(RequesterPhone);
                if (!IsValidPhoneNumber)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("EnterValidPhoneNumber"), 2000);
                    return;
                }
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

                else if (LocationID == null && AssetID == null && AssetSystemID == null)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Selectthelocationassetsystemassetfield"), 2000);
                    return;
                }

                else if (AssetID != null && AssetSystemID != null)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("PleaseselecteitherassetOrassetsystem"), 2000);
                    return;
                }
                else if (!string.IsNullOrWhiteSpace(ApprovalLevel) && string.IsNullOrWhiteSpace(ApprovalNumber))
                {
                    UserDialogs.Instance.HideLoading();
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("ApprovalNumberIsRequired"), 2000);
                    return;
                }

                else if (!string.IsNullOrWhiteSpace(ApprovalNumber) && string.IsNullOrWhiteSpace(ApprovalLevel))
                {
                    UserDialogs.Instance.HideLoading();
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("ApprovalLevelIsRequired"), 2000);
                    return;
                }
                bool approvalLevel = false;
                bool approvalNumber = false;
                if (!string.IsNullOrWhiteSpace(ApprovalLevel))
                {
                    approvalLevel = IsNumber(ApprovalLevel);
                    if (approvalLevel == false)
                    {

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("ApprovalLevelMustbeNumberic"), 2000);
                        return;
                    }

                }
                if (!string.IsNullOrWhiteSpace(ApprovalNumber))
                {
                    approvalNumber = IsNumber(ApprovalNumber);
                    if (approvalNumber == false)
                    {

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("ApprovalNumberMustbeNumberic"), 2000);
                        return;
                    }
                }


                if (!string.IsNullOrWhiteSpace(ApprovalLevel))
                {
                    if (int.Parse(ApprovalLevel) > 0 && int.Parse(ApprovalLevel) < 6)
                    {

                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("ApprovalLevelMustBeBetween1To6"), 2000);
                        return;
                    }
                }
                if (!string.IsNullOrWhiteSpace(ApprovalNumber))
                {
                    if (int.Parse(ApprovalNumber) > 0 && int.Parse(ApprovalNumber) < 6)
                    {

                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();
                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("ApprovalNumberMustBeBetween1To6"), 2000);
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


                #endregion


                ///TODO:ActualDowntime1 , MLC1 , MMC1 , EstimatedDowntime1 parse into decimal value.

                /// Create WO wrapper and data into it and update the workorder
                /// 

                var workOrder = new workOrders();
                #region workOrder properties initialzation
                if (string.IsNullOrWhiteSpace(CurrentRuntimeText))
                {
                    this.CurrentRuntimeText = "0.00";
                }
                else
                {
                    this.CurrentRuntimeText = CurrentRuntimeText.Replace(',', '.');
                }
                workOrder.ModifiedUserName = AppSettings.User.UserName;
                workOrder.Description = String.IsNullOrEmpty(DescriptionText.Trim()) ? null : DescriptionText.Trim();
                workOrder.RequiredDate = RequiredDate1.Date.Add(DateTime.Now.TimeOfDay);
                workOrder.WorkStartedDate = WorkStartedDate1.HasValue ? WorkStartedDate1.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null;
                workOrder.CompletionDate = WorkorderCompletionDate.HasValue ? WorkorderCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null;
                workOrder.FacilityID = FacilityID;
                workOrder.LocationID = LocationID;
                workOrder.AssetID = AssetID;
                workOrder.AssetSystemID = AssetSystemID;
                workOrder.AssignedToEmployeeID = AssignToEmployeeID;
                workOrder.CostCenterID = CostCenterID;
                workOrder.WorkOrderRequesterID = WorkorderRequesterID;
                workOrder.PriorityID = PriorityID;
                workOrder.ShiftID = ShiftID;
                workOrder.WorkOrderStatusID = WorkorderStatusID;
                workOrder.WorkTypeID = WorkorderTypeID;
                workOrder.MaintenanceCodeID = MaintenanceCodeID;
                workOrder.WorkOrderID = this.WorkorderID;
                // workOrder.TotalTime = this.TotalTimeText;
                //MiscellaneousLaborCostID = workorderWrapper.workOrderWrapper.workOrder.MiscellaneousLaborCostID,
                //MiscellaneousMaterialsCostID = workorderWrapper.workOrderWrapper.workOrder.MiscellaneousMaterialsCostID,
                workOrder.AdditionalDetails = AdditionalDetailsText; //String.IsNullOrEmpty(AdditionalDetails1.Text) ? null : AdditionalDetails1.Text;
                workOrder.MiscellaneousLaborCostID = workorderWrapper.workOrderWrapper.workOrder.MiscellaneousLaborCostID;
                workOrder.MiscellaneousMaterialsCostID = workorderWrapper.workOrderWrapper.workOrder.MiscellaneousMaterialsCostID;
                workOrder.InternalNote = InternalNoteText;

                if (string.IsNullOrWhiteSpace(ActualDowntimeText))
                {
                    ActualDowntimeText = "0";
                }
                else
                {
                    this.ActualDowntimeText = ActualDowntimeText;
                }
                if (string.IsNullOrWhiteSpace(EstimstedDowntimeText))
                {
                    EstimstedDowntimeText = "0";
                }

                if (string.IsNullOrWhiteSpace(MiscellaneousLabourCostText))
                {
                    MiscellaneousLabourCostText = "0.0";
                }
                if (string.IsNullOrWhiteSpace(MiscellaneousMaterialCostText))
                {
                    MiscellaneousMaterialCostText = "0.0";
                }


                //workOrder.ApprovalLevel=
                workOrder.DistributeCost = IsCostDistributed;
                workOrder.ParentandChildCost = ParentCostDistributed;
                workOrder.ChildCost = ChildCostDistributed;
                workOrder.ActualDowntime = ActualDowntimeText;
                workOrder.EstimatedDowntime = EstimstedDowntimeText;
                workOrder.MiscellaneousLaborCost = decimal.Parse(MiscellaneousLabourCostText, CultureInfo.InvariantCulture);
                workOrder.MiscellaneousMaterialsCost = decimal.Parse(MiscellaneousMaterialCostText, CultureInfo.InvariantCulture);


                #region Dynamic Field need to add in model so it can save on server.


                /// >>> Marked property in already present in wrapper


                //ActivationDate = ActivationDateText, //Type need to change to string in model

                workOrder.ApprovalLevel = ApprovalLevel;
                workOrder.ApprovalNumber = ApprovalNumber;

                workOrder.ConfirmEmail = ConfirmEmail;
                workOrder.DigitalSignatures = DigitalSignatures;
                workOrder.JobNumber = JobNumber;
                workOrder.project = Project;

                //RequestedDate = RequestedDate >>> Type need to change to string in model
                workOrder.RequesterEmail = RequesterEmail;
                workOrder.RequesterFullName = RequesterFullName;
                workOrder.RequesterPhone = RequesterPhone;


                //UserField21 = UserField21, //UserField21b.Text,
                //WorkOrderNumber



                #endregion


                #region User Fields

                workOrder.UserField1 = String.IsNullOrEmpty(UserField1) ? null : UserField1.Trim();
                workOrder.UserField2 = String.IsNullOrEmpty(UserField2) ? null : UserField2.Trim();
                workOrder.UserField3 = String.IsNullOrEmpty(UserField3) ? null : UserField3.Trim();
                workOrder.UserField4 = String.IsNullOrEmpty(UserField4) ? null : UserField4.Trim();
                workOrder.UserField5 = String.IsNullOrEmpty(UserField5) ? null : UserField5.Trim();
                workOrder.UserField6 = String.IsNullOrEmpty(UserField6) ? null : UserField6.Trim();
                workOrder.UserField7 = String.IsNullOrEmpty(UserField7) ? null : UserField7.Trim();
                workOrder.UserField8 = String.IsNullOrEmpty(UserField8) ? null : UserField8.Trim();
                workOrder.UserField9 = String.IsNullOrEmpty(UserField9) ? null : UserField9.Trim();
                workOrder.UserField10 = String.IsNullOrEmpty(UserField10) ? null : UserField10.Trim();
                workOrder.UserField11 = String.IsNullOrEmpty(UserField11) ? null : UserField11.Trim();
                workOrder.UserField12 = String.IsNullOrEmpty(UserField12) ? null : UserField12.Trim();
                workOrder.UserField13 = String.IsNullOrEmpty(UserField13) ? null : UserField13.Trim();
                workOrder.UserField14 = String.IsNullOrEmpty(UserField14) ? null : UserField14.Trim();
                workOrder.UserField15 = String.IsNullOrEmpty(UserField15) ? null : UserField15.Trim();
                workOrder.UserField16 = String.IsNullOrEmpty(UserField16) ? null : UserField16.Trim();
                workOrder.UserField17 = String.IsNullOrEmpty(UserField17) ? null : UserField17.Trim();
                workOrder.UserField18 = String.IsNullOrEmpty(UserField18) ? null : UserField18.Trim();
                workOrder.UserField19 = String.IsNullOrEmpty(UserField19) ? null : UserField19.Trim();
                workOrder.UserField20 = String.IsNullOrEmpty(UserField20) ? null : UserField20.Trim();
                workOrder.UserField21 = String.IsNullOrEmpty(UserField21) ? null : UserField21.Trim();
                workOrder.UserField22 = String.IsNullOrEmpty(UserField22) ? null : UserField22.Trim();
                workOrder.UserField23 = String.IsNullOrEmpty(UserField23) ? null : UserField23.Trim();
                workOrder.UserField24 = String.IsNullOrEmpty(UserField24) ? null : UserField24.Trim();
                #endregion


                #region New Properties 31.5.2018







                #endregion

                bool fdasignatureKey = AppSettings.User.blackhawkLicValidator.IsFDASignatureValidation;

                if (fdasignatureKey == true)
                {
                    if (AppSettings.User.RequireSignaturesForValidation == "True")
                    {
                        workOrder.IsSignatureValidated = true;
                        Application.Current.Properties["CauseID"] = this.CauseID;
                        Application.Current.Properties["CauseJson"] = this.Cause;
                        Application.Current.Properties["WorkorderWrapper"] = workOrder;

                        WorkorderCompletionDateWarningTextIsVisible = false;
                        WorkStartedDateWarningTextIsVisible = false;
                        var page = new EditWorkorderSignaturePage();
                        await PopupNavigation.PushAsync(page);



                    }
                    else
                    {
                        if (this.CauseID == null)
                        {
                            var workorder = new workOrderWrapper
                            {
                                TimeZone = AppSettings.UserTimeZone,
                                CultureName = AppSettings.UserCultureName,
                                UserId = Convert.ToInt32(UserID),
                                ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                                cause = null,
                                workOrder = new workOrders
                                {
                                    ModifiedUserName = AppSettings.User.UserName,
                                    Description = String.IsNullOrEmpty(DescriptionText.Trim()) ? null : DescriptionText.Trim(),
                                    RequiredDate = RequiredDate1.Date.Add(DateTime.Now.TimeOfDay),
                                    WorkStartedDate = WorkStartedDate1.HasValue ? WorkStartedDate1.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                                    CompletionDate = WorkorderCompletionDate.HasValue ? WorkorderCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                                    FacilityID = FacilityID,
                                    LocationID = LocationID,
                                    AssetID = AssetID,
                                    CurrentRuntime = CurrentRuntimeText,
                                    AssetSystemID = AssetSystemID,
                                    AssignedToEmployeeID = AssignToEmployeeID,
                                    CostCenterID = CostCenterID,
                                    WorkOrderRequesterID = WorkorderRequesterID,
                                    PriorityID = PriorityID,
                                    ShiftID = ShiftID,
                                    WorkOrderStatusID = WorkorderStatusID,
                                    WorkTypeID = WorkorderTypeID,
                                    MaintenanceCodeID = MaintenanceCodeID,
                                    WorkOrderID = WorkorderID,
                                    MiscellaneousLaborCostID = workorderWrapper.workOrderWrapper.workOrder.MiscellaneousLaborCostID,
                                    MiscellaneousMaterialsCostID = workorderWrapper.workOrderWrapper.workOrder.MiscellaneousMaterialsCostID,
                                    AdditionalDetails = AdditionalDetailsText, //String.IsNullOrEmpty(AdditionalDetails1.Text) ? null : AdditionalDetails1.Text,
                                    InternalNote = InternalNoteText,
                                    ActualDowntime = ActualDowntimeText,
                                    EstimatedDowntime = EstimstedDowntimeText,
                                    MiscellaneousLaborCost = decimal.Parse(MiscellaneousLabourCostText, CultureInfo.InvariantCulture),
                                    MiscellaneousMaterialsCost = decimal.Parse(MiscellaneousMaterialCostText, CultureInfo.InvariantCulture),
                                    ApprovalLevel = this.ApprovalLevel,
                                    ApprovalNumber = this.ApprovalNumber,
                                    IsSignatureValidated = false,
                                    DistributeCost = IsCostDistributed,
                                    ParentandChildCost = ParentCostDistributed,
                                    ChildCost = ChildCostDistributed,

                                    #region Dynamic Field need to add in model so it can save on server.


                                    /// >>> Marked property in already present in wrapper


                                    //ActivationDate = ActivationDateText, //Type need to change to string in model

                                    ConfirmEmail = ConfirmEmail,
                                    DigitalSignatures = DigitalSignatures,
                                    JobNumber = JobNumber,
                                    project = Project,

                                    //RequestedDate = RequestedDate >>> Type need to change to string in model
                                    RequesterEmail = RequesterEmail,
                                    RequesterFullName = RequesterFullName,
                                    RequesterPhone = RequesterPhone,


                                    //UserField21 = UserField21, //UserField21b.Text,
                                    //WorkOrderNumber



                                    #endregion


                                    #region User Fields

                                    UserField1 = String.IsNullOrEmpty(UserField1) ? null : UserField1.Trim(),
                                    UserField2 = String.IsNullOrEmpty(UserField2) ? null : UserField2.Trim(),
                                    UserField3 = String.IsNullOrEmpty(UserField3) ? null : UserField3.Trim(),
                                    UserField4 = String.IsNullOrEmpty(UserField4) ? null : UserField4.Trim(),
                                    UserField5 = String.IsNullOrEmpty(UserField5) ? null : UserField5.Trim(),
                                    UserField6 = String.IsNullOrEmpty(UserField6) ? null : UserField6.Trim(),
                                    UserField7 = String.IsNullOrEmpty(UserField7) ? null : UserField7.Trim(),
                                    UserField8 = String.IsNullOrEmpty(UserField8) ? null : UserField8.Trim(),
                                    UserField9 = String.IsNullOrEmpty(UserField9) ? null : UserField9.Trim(),
                                    UserField10 = String.IsNullOrEmpty(UserField10) ? null : UserField10.Trim(),
                                    UserField11 = String.IsNullOrEmpty(UserField11) ? null : UserField11.Trim(),
                                    UserField12 = String.IsNullOrEmpty(UserField12) ? null : UserField12.Trim(),
                                    UserField13 = String.IsNullOrEmpty(UserField13) ? null : UserField13.Trim(),
                                    UserField14 = String.IsNullOrEmpty(UserField14) ? null : UserField14.Trim(),
                                    UserField15 = String.IsNullOrEmpty(UserField15) ? null : UserField15.Trim(),
                                    UserField16 = String.IsNullOrEmpty(UserField16) ? null : UserField16.Trim(),
                                    UserField17 = String.IsNullOrEmpty(UserField17) ? null : UserField17.Trim(),
                                    UserField18 = String.IsNullOrEmpty(UserField18) ? null : UserField18.Trim(),
                                    UserField19 = String.IsNullOrEmpty(UserField19) ? null : UserField19.Trim(),
                                    UserField20 = String.IsNullOrEmpty(UserField20) ? null : UserField20.Trim(),
                                    UserField21 = String.IsNullOrEmpty(UserField21) ? null : UserField21.Trim(),
                                    UserField22 = String.IsNullOrEmpty(UserField22) ? null : UserField22.Trim(),
                                    UserField23 = String.IsNullOrEmpty(UserField23) ? null : UserField23.Trim(),
                                    UserField24 = String.IsNullOrEmpty(UserField24) ? null : UserField24.Trim(),
                                    #endregion






                                },

                            };


                            var response = await _workorderService.UpdateWorkorder(workorder);
                            if (response != null && bool.Parse(response.servicestatus))
                            {
                                WorkorderCompletionDateWarningTextIsVisible = false;
                                WorkStartedDateWarningTextIsVisible = false;
                                //DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Workordersuccessfullyupdated", 2000);
                                DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Workordersuccessfullyupdated."), 2000);
                                //var workorderWrapper1 = await _workorderService.GetWorkorderByWorkorderID(UserID, WorkorderID.ToString());
                                //await SetControlsPropertiesForPage(workorderWrapper1);
                                await NavigationService.NavigateBackAsync();
                            }
                        }
                        else
                        {

                            #endregion
                            var workorder = new workOrderWrapper
                            {
                                TimeZone = AppSettings.UserTimeZone,
                                CultureName = AppSettings.UserCultureName,
                                UserId = Convert.ToInt32(UserID),
                                ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                                cause = Cause,
                                workOrder = new workOrders
                                {

                                    ModifiedUserName = AppSettings.User.UserName,
                                    Description = String.IsNullOrEmpty(DescriptionText.Trim()) ? null : DescriptionText.Trim(),
                                    RequiredDate = RequiredDate1.Date.Add(DateTime.Now.TimeOfDay),
                                    WorkStartedDate = WorkStartedDate1.HasValue ? WorkStartedDate1.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                                    CompletionDate = WorkorderCompletionDate.HasValue ? WorkorderCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                                    FacilityID = FacilityID,
                                    LocationID = LocationID,
                                    AssetID = AssetID,
                                    AssetSystemID = AssetSystemID,
                                    AssignedToEmployeeID = AssignToEmployeeID,
                                    CostCenterID = CostCenterID,
                                    WorkOrderRequesterID = WorkorderRequesterID,
                                    PriorityID = PriorityID,
                                    ShiftID = ShiftID,
                                    CurrentRuntime = CurrentRuntimeText,
                                    WorkOrderStatusID = WorkorderStatusID,
                                    WorkTypeID = WorkorderTypeID,
                                    MaintenanceCodeID = MaintenanceCodeID,
                                    WorkOrderID = WorkorderID,
                                    MiscellaneousLaborCostID = workorderWrapper.workOrderWrapper.workOrder.MiscellaneousLaborCostID,
                                    MiscellaneousMaterialsCostID = workorderWrapper.workOrderWrapper.workOrder.MiscellaneousMaterialsCostID,
                                    AdditionalDetails = AdditionalDetailsText, //String.IsNullOrEmpty(AdditionalDetails1.Text) ? null : AdditionalDetails1.Text,
                                    InternalNote = InternalNoteText,
                                    ActualDowntime = ActualDowntimeText,
                                    EstimatedDowntime = EstimstedDowntimeText,
                                    MiscellaneousLaborCost = decimal.Parse(MiscellaneousLabourCostText, CultureInfo.InvariantCulture),
                                    MiscellaneousMaterialsCost = decimal.Parse(MiscellaneousMaterialCostText, CultureInfo.InvariantCulture),
                                    ApprovalLevel = this.ApprovalLevel,
                                    ApprovalNumber = this.ApprovalNumber,
                                    IsSignatureValidated = false,
                                    DistributeCost = IsCostDistributed,
                                    ParentandChildCost = ParentCostDistributed,
                                    ChildCost = ChildCostDistributed,
                                    #region Dynamic Field need to add in model so it can save on server.


                                    /// >>> Marked property in already present in wrapper


                                    //ActivationDate = ActivationDateText, //Type need to change to string in model

                                    ConfirmEmail = ConfirmEmail,
                                    DigitalSignatures = DigitalSignatures,
                                    JobNumber = JobNumber,
                                    project = Project,

                                    //RequestedDate = RequestedDate >>> Type need to change to string in model
                                    RequesterEmail = RequesterEmail,
                                    RequesterFullName = RequesterFullName,
                                    RequesterPhone = RequesterPhone,


                                    //UserField21 = UserField21, //UserField21b.Text,
                                    //WorkOrderNumber



                                    #endregion


                                    #region User Fields

                                    UserField1 = String.IsNullOrEmpty(UserField1) ? null : UserField1.Trim(),
                                    UserField2 = String.IsNullOrEmpty(UserField2) ? null : UserField2.Trim(),
                                    UserField3 = String.IsNullOrEmpty(UserField3) ? null : UserField3.Trim(),
                                    UserField4 = String.IsNullOrEmpty(UserField4) ? null : UserField4.Trim(),
                                    UserField5 = String.IsNullOrEmpty(UserField5) ? null : UserField5.Trim(),
                                    UserField6 = String.IsNullOrEmpty(UserField6) ? null : UserField6.Trim(),
                                    UserField7 = String.IsNullOrEmpty(UserField7) ? null : UserField7.Trim(),
                                    UserField8 = String.IsNullOrEmpty(UserField8) ? null : UserField8.Trim(),
                                    UserField9 = String.IsNullOrEmpty(UserField9) ? null : UserField9.Trim(),
                                    UserField10 = String.IsNullOrEmpty(UserField10) ? null : UserField10.Trim(),
                                    UserField11 = String.IsNullOrEmpty(UserField11) ? null : UserField11.Trim(),
                                    UserField12 = String.IsNullOrEmpty(UserField12) ? null : UserField12.Trim(),
                                    UserField13 = String.IsNullOrEmpty(UserField13) ? null : UserField13.Trim(),
                                    UserField14 = String.IsNullOrEmpty(UserField14) ? null : UserField14.Trim(),
                                    UserField15 = String.IsNullOrEmpty(UserField15) ? null : UserField15.Trim(),
                                    UserField16 = String.IsNullOrEmpty(UserField16) ? null : UserField16.Trim(),
                                    UserField17 = String.IsNullOrEmpty(UserField17) ? null : UserField17.Trim(),
                                    UserField18 = String.IsNullOrEmpty(UserField18) ? null : UserField18.Trim(),
                                    UserField19 = String.IsNullOrEmpty(UserField19) ? null : UserField19.Trim(),
                                    UserField20 = String.IsNullOrEmpty(UserField20) ? null : UserField20.Trim(),
                                    UserField21 = String.IsNullOrEmpty(UserField21) ? null : UserField21.Trim(),
                                    UserField22 = String.IsNullOrEmpty(UserField22) ? null : UserField22.Trim(),
                                    UserField23 = String.IsNullOrEmpty(UserField23) ? null : UserField23.Trim(),
                                    UserField24 = String.IsNullOrEmpty(UserField24) ? null : UserField24.Trim(),
                                    #endregion






                                },

                            };


                            var response = await _workorderService.UpdateWorkorder(workorder);
                            if (response != null && bool.Parse(response.servicestatus))
                            {
                                WorkorderCompletionDateWarningTextIsVisible = false;
                                WorkStartedDateWarningTextIsVisible = false;
                                DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Workordersuccessfullyupdated."), 2000);
                                //var workorderWrapper1 = await _workorderService.GetWorkorderByWorkorderID(UserID, WorkorderID.ToString());
                                //await SetControlsPropertiesForPage(workorderWrapper1);
                                await NavigationService.NavigateBackAsync();
                            }
                        }
                    }
                }
                else
                {
                    if (this.CauseID == null)
                    {
                        var workorder = new workOrderWrapper
                        {
                            TimeZone = AppSettings.UserTimeZone,
                            CultureName = AppSettings.UserCultureName,
                            UserId = Convert.ToInt32(UserID),
                            ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                            cause = null,
                            workOrder = new workOrders
                            {
                                ModifiedUserName = AppSettings.User.UserName,
                                Description = String.IsNullOrEmpty(DescriptionText.Trim()) ? null : DescriptionText.Trim(),
                                RequiredDate = RequiredDate1.Date.Add(DateTime.Now.TimeOfDay),
                                WorkStartedDate = WorkStartedDate1.HasValue ? WorkStartedDate1.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                                CompletionDate = WorkorderCompletionDate.HasValue ? WorkorderCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                                FacilityID = FacilityID,
                                LocationID = LocationID,
                                AssetID = AssetID,
                                CurrentRuntime = CurrentRuntimeText,
                                AssetSystemID = AssetSystemID,
                                AssignedToEmployeeID = AssignToEmployeeID,
                                CostCenterID = CostCenterID,
                                WorkOrderRequesterID = WorkorderRequesterID,
                                PriorityID = PriorityID,
                                ShiftID = ShiftID,
                                WorkOrderStatusID = WorkorderStatusID,
                                WorkTypeID = WorkorderTypeID,
                                MaintenanceCodeID = MaintenanceCodeID,
                                WorkOrderID = WorkorderID,
                                MiscellaneousLaborCostID = workorderWrapper.workOrderWrapper.workOrder.MiscellaneousLaborCostID,
                                MiscellaneousMaterialsCostID = workorderWrapper.workOrderWrapper.workOrder.MiscellaneousMaterialsCostID,
                                AdditionalDetails = AdditionalDetailsText, //String.IsNullOrEmpty(AdditionalDetails1.Text) ? null : AdditionalDetails1.Text,
                                InternalNote = InternalNoteText,
                                ActualDowntime = ActualDowntimeText,
                                EstimatedDowntime = EstimstedDowntimeText,
                                MiscellaneousLaborCost = decimal.Parse(MiscellaneousLabourCostText, CultureInfo.InvariantCulture),
                                MiscellaneousMaterialsCost = decimal.Parse(MiscellaneousMaterialCostText, CultureInfo.InvariantCulture),
                                ApprovalLevel = this.ApprovalLevel,
                                ApprovalNumber = this.ApprovalNumber,
                                IsSignatureValidated = false,
                                DistributeCost = IsCostDistributed,
                                ParentandChildCost = ParentCostDistributed,
                                ChildCost = ChildCostDistributed,

                                #region Dynamic Field need to add in model so it can save on server.


                                /// >>> Marked property in already present in wrapper


                                //ActivationDate = ActivationDateText, //Type need to change to string in model

                                ConfirmEmail = ConfirmEmail,
                                DigitalSignatures = DigitalSignatures,
                                JobNumber = JobNumber,
                                project = Project,

                                //RequestedDate = RequestedDate >>> Type need to change to string in model
                                RequesterEmail = RequesterEmail,
                                RequesterFullName = RequesterFullName,
                                RequesterPhone = RequesterPhone,


                                //UserField21 = UserField21, //UserField21b.Text,
                                //WorkOrderNumber



                                #endregion


                                #region User Fields

                                UserField1 = String.IsNullOrEmpty(UserField1) ? null : UserField1.Trim(),
                                UserField2 = String.IsNullOrEmpty(UserField2) ? null : UserField2.Trim(),
                                UserField3 = String.IsNullOrEmpty(UserField3) ? null : UserField3.Trim(),
                                UserField4 = String.IsNullOrEmpty(UserField4) ? null : UserField4.Trim(),
                                UserField5 = String.IsNullOrEmpty(UserField5) ? null : UserField5.Trim(),
                                UserField6 = String.IsNullOrEmpty(UserField6) ? null : UserField6.Trim(),
                                UserField7 = String.IsNullOrEmpty(UserField7) ? null : UserField7.Trim(),
                                UserField8 = String.IsNullOrEmpty(UserField8) ? null : UserField8.Trim(),
                                UserField9 = String.IsNullOrEmpty(UserField9) ? null : UserField9.Trim(),
                                UserField10 = String.IsNullOrEmpty(UserField10) ? null : UserField10.Trim(),
                                UserField11 = String.IsNullOrEmpty(UserField11) ? null : UserField11.Trim(),
                                UserField12 = String.IsNullOrEmpty(UserField12) ? null : UserField12.Trim(),
                                UserField13 = String.IsNullOrEmpty(UserField13) ? null : UserField13.Trim(),
                                UserField14 = String.IsNullOrEmpty(UserField14) ? null : UserField14.Trim(),
                                UserField15 = String.IsNullOrEmpty(UserField15) ? null : UserField15.Trim(),
                                UserField16 = String.IsNullOrEmpty(UserField16) ? null : UserField16.Trim(),
                                UserField17 = String.IsNullOrEmpty(UserField17) ? null : UserField17.Trim(),
                                UserField18 = String.IsNullOrEmpty(UserField18) ? null : UserField18.Trim(),
                                UserField19 = String.IsNullOrEmpty(UserField19) ? null : UserField19.Trim(),
                                UserField20 = String.IsNullOrEmpty(UserField20) ? null : UserField20.Trim(),
                                UserField21 = String.IsNullOrEmpty(UserField21) ? null : UserField21.Trim(),
                                UserField22 = String.IsNullOrEmpty(UserField22) ? null : UserField22.Trim(),
                                UserField23 = String.IsNullOrEmpty(UserField23) ? null : UserField23.Trim(),
                                UserField24 = String.IsNullOrEmpty(UserField24) ? null : UserField24.Trim(),
                                #endregion






                            },

                        };


                        var response = await _workorderService.UpdateWorkorder(workorder);
                        if (response != null && bool.Parse(response.servicestatus))
                        {
                            WorkorderCompletionDateWarningTextIsVisible = false;
                            WorkStartedDateWarningTextIsVisible = false;
                            //DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Workordersuccessfullyupdated", 2000);
                            DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Workordersuccessfullyupdated."), 2000);
                            //var workorderWrapper1 = await _workorderService.GetWorkorderByWorkorderID(UserID, WorkorderID.ToString());
                            //await SetControlsPropertiesForPage(workorderWrapper1);
                            await NavigationService.NavigateBackAsync();
                        }
                    }
                    else
                    {

                        #endregion
                        var workorder = new workOrderWrapper
                        {
                            TimeZone = AppSettings.UserTimeZone,
                            CultureName = AppSettings.UserCultureName,
                            UserId = Convert.ToInt32(UserID),
                            ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                            cause = Cause,
                            workOrder = new workOrders
                            {

                                ModifiedUserName = AppSettings.User.UserName,
                                Description = String.IsNullOrEmpty(DescriptionText.Trim()) ? null : DescriptionText.Trim(),
                                RequiredDate = RequiredDate1.Date.Add(DateTime.Now.TimeOfDay),
                                WorkStartedDate = WorkStartedDate1.HasValue ? WorkStartedDate1.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                                CompletionDate = WorkorderCompletionDate.HasValue ? WorkorderCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                                FacilityID = FacilityID,
                                LocationID = LocationID,
                                AssetID = AssetID,
                                AssetSystemID = AssetSystemID,
                                AssignedToEmployeeID = AssignToEmployeeID,
                                CostCenterID = CostCenterID,
                                CurrentRuntime = CurrentRuntimeText,
                                WorkOrderRequesterID = WorkorderRequesterID,
                                PriorityID = PriorityID,
                                ShiftID = ShiftID,
                                WorkOrderStatusID = WorkorderStatusID,
                                WorkTypeID = WorkorderTypeID,
                                MaintenanceCodeID = MaintenanceCodeID,
                                WorkOrderID = WorkorderID,
                                MiscellaneousLaborCostID = workorderWrapper.workOrderWrapper.workOrder.MiscellaneousLaborCostID,
                                MiscellaneousMaterialsCostID = workorderWrapper.workOrderWrapper.workOrder.MiscellaneousMaterialsCostID,
                                AdditionalDetails = AdditionalDetailsText, //String.IsNullOrEmpty(AdditionalDetails1.Text) ? null : AdditionalDetails1.Text,
                                InternalNote = InternalNoteText,
                                ActualDowntime = ActualDowntimeText,
                                EstimatedDowntime = EstimstedDowntimeText,
                                MiscellaneousLaborCost = decimal.Parse(MiscellaneousLabourCostText, CultureInfo.InvariantCulture),
                                MiscellaneousMaterialsCost = decimal.Parse(MiscellaneousMaterialCostText, CultureInfo.InvariantCulture),
                                ApprovalLevel = this.ApprovalLevel,
                                ApprovalNumber = this.ApprovalNumber,
                                IsSignatureValidated = false,
                                DistributeCost = IsCostDistributed,
                                ParentandChildCost = ParentCostDistributed,
                                ChildCost = ChildCostDistributed,
                                #region Dynamic Field need to add in model so it can save on server.


                                /// >>> Marked property in already present in wrapper


                                //ActivationDate = ActivationDateText, //Type need to change to string in model

                                ConfirmEmail = ConfirmEmail,
                                DigitalSignatures = DigitalSignatures,
                                JobNumber = JobNumber,
                                project = Project,

                                //RequestedDate = RequestedDate >>> Type need to change to string in model
                                RequesterEmail = RequesterEmail,
                                RequesterFullName = RequesterFullName,
                                RequesterPhone = RequesterPhone,


                                //UserField21 = UserField21, //UserField21b.Text,
                                //WorkOrderNumber



                                #endregion


                                #region User Fields

                                UserField1 = String.IsNullOrEmpty(UserField1) ? null : UserField1.Trim(),
                                UserField2 = String.IsNullOrEmpty(UserField2) ? null : UserField2.Trim(),
                                UserField3 = String.IsNullOrEmpty(UserField3) ? null : UserField3.Trim(),
                                UserField4 = String.IsNullOrEmpty(UserField4) ? null : UserField4.Trim(),
                                UserField5 = String.IsNullOrEmpty(UserField5) ? null : UserField5.Trim(),
                                UserField6 = String.IsNullOrEmpty(UserField6) ? null : UserField6.Trim(),
                                UserField7 = String.IsNullOrEmpty(UserField7) ? null : UserField7.Trim(),
                                UserField8 = String.IsNullOrEmpty(UserField8) ? null : UserField8.Trim(),
                                UserField9 = String.IsNullOrEmpty(UserField9) ? null : UserField9.Trim(),
                                UserField10 = String.IsNullOrEmpty(UserField10) ? null : UserField10.Trim(),
                                UserField11 = String.IsNullOrEmpty(UserField11) ? null : UserField11.Trim(),
                                UserField12 = String.IsNullOrEmpty(UserField12) ? null : UserField12.Trim(),
                                UserField13 = String.IsNullOrEmpty(UserField13) ? null : UserField13.Trim(),
                                UserField14 = String.IsNullOrEmpty(UserField14) ? null : UserField14.Trim(),
                                UserField15 = String.IsNullOrEmpty(UserField15) ? null : UserField15.Trim(),
                                UserField16 = String.IsNullOrEmpty(UserField16) ? null : UserField16.Trim(),
                                UserField17 = String.IsNullOrEmpty(UserField17) ? null : UserField17.Trim(),
                                UserField18 = String.IsNullOrEmpty(UserField18) ? null : UserField18.Trim(),
                                UserField19 = String.IsNullOrEmpty(UserField19) ? null : UserField19.Trim(),
                                UserField20 = String.IsNullOrEmpty(UserField20) ? null : UserField20.Trim(),
                                UserField21 = String.IsNullOrEmpty(UserField21) ? null : UserField21.Trim(),
                                UserField22 = String.IsNullOrEmpty(UserField22) ? null : UserField22.Trim(),
                                UserField23 = String.IsNullOrEmpty(UserField23) ? null : UserField23.Trim(),
                                UserField24 = String.IsNullOrEmpty(UserField24) ? null : UserField24.Trim(),
                                #endregion






                            },

                        };


                        var response = await _workorderService.UpdateWorkorder(workorder);
                        if (response != null && bool.Parse(response.servicestatus))
                        {
                            WorkorderCompletionDateWarningTextIsVisible = false;
                            WorkStartedDateWarningTextIsVisible = false;
                            DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Workordersuccessfullyupdated."), 2000);
                            //var workorderWrapper1 = await _workorderService.GetWorkorderByWorkorderID(UserID, WorkorderID.ToString());
                            //await SetControlsPropertiesForPage(workorderWrapper1);
                            await NavigationService.NavigateBackAsync();
                        }
                    }
                }

                UserDialogs.Instance.HideLoading();
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
                        //case "ActivationDate":
                        //    {
                        //        validationResult = ValidateValidations(formLoadItem, ActivationDateText);
                        //        if (validationResult.FailedItem != null)
                        //        {
                        //            return validationResult;
                        //        }
                        //        break;

                        //    }

                        case "ClosedDate":
                            {
                                validationResult = ValidateValidations(formLoadItem, ClosedDateText);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "AbnormalityID":
                            {
                                validationResult = ValidateValidations(formLoadItem, AbnormalityText);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "ActualDowntime":
                            {
                                //validationResult = ValidateValidations(formLoadItem, ActualDowntimeText);
                                //if (validationResult.FailedItem != null)
                                //{
                                //    return validationResult;
                                //}
                                break;

                            }

                        case "AmStepID":
                            {
                                validationResult = ValidateValidations(formLoadItem, AmStepID);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "AnalysisPerformedDate":
                            {
                                validationResult = ValidateValidations(formLoadItem, AnalysisPerformedDate);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "ConfirmEmail":
                            {
                                validationResult = ValidateValidations(formLoadItem, ConfirmEmail);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "CountermeasuresDefinedDate":
                            {

                                validationResult = ValidateValidations(formLoadItem, CountermeasuresDefinedDate);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "CurrentRuntime":
                            {
                                validationResult = ValidateValidations(formLoadItem, CurrentRuntime);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "DiagnosticTime":
                            {
                                validationResult = ValidateValidations(formLoadItem, DiagnosticTime);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "DigitalSignatures":
                            {
                                validationResult = ValidateValidations(formLoadItem, DigitalSignatures);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "EstimatedDowntime":
                            {
                                //validationResult = ValidateValidations(formLoadItem, EstimatedDowntime);
                                //if (validationResult.FailedItem != null)
                                //{
                                //    return validationResult;
                                //}
                                break;
                            }

                        case "ImplementationValidatedDate":
                            {
                                validationResult = ValidateValidations(formLoadItem, ImplementationValidatedDate);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "InitialWaitTime":
                            {
                                validationResult = ValidateValidations(formLoadItem, InitialWaitTime);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "JobNumber":
                            {
                                validationResult = ValidateValidations(formLoadItem, JobNumber);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "NotificationTime":
                            {
                                validationResult = ValidateValidations(formLoadItem, NotificationTime);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "PartWaitingTime":
                            {
                                validationResult = ValidateValidations(formLoadItem, PartWaitingTime);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "ProblemID":
                            {
                                validationResult = ValidateValidations(formLoadItem, ProblemID);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "project":
                            {
                                validationResult = ValidateValidations(formLoadItem, Project);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }
                        case "ApprovalLevel":
                            {
                                validationResult = ValidateValidations(formLoadItem, ApprovalLevel);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }
                        case "ApprovalNumber":
                            {
                                validationResult = ValidateValidations(formLoadItem, ApprovalNumber);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "RelatedToID":
                            {
                                validationResult = ValidateValidations(formLoadItem, RelatedToID);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "RepairingOrReplacementTime":
                            {
                                validationResult = ValidateValidations(formLoadItem, RepairingOrReplacementTime);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        //case "RequestedDate":
                        //    {
                        //        validationResult = ValidateValidations(formLoadItem, RequestedDate);
                        //        if (validationResult.FailedItem != null)
                        //        {
                        //            return validationResult;
                        //        }
                        //        break;
                        //    }

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

                        case "ServiceRequestModeID":
                            {
                                validationResult = ValidateValidations(formLoadItem, ServiceRequestModeID);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "StartupTime":
                            {
                                validationResult = ValidateValidations(formLoadItem, StartupTime);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "TotalTime":
                            {
                                validationResult = ValidateValidations(formLoadItem, TotalTime);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

                        case "UnsafeConditionID":
                            {
                                validationResult = ValidateValidations(formLoadItem, UnsafeConditionID);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }
                        case "LOTOUrl":
                            {
                                validationResult = ValidateValidations(formLoadItem, UnsafeConditionID);
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



                        case "WorkOrderNumber":
                            {
                                validationResult = ValidateValidations(formLoadItem, WorkOrderNumber);
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

                        case "WorkOrderNumber":
                            {
                                break;
                            }



                        case "JobNumber":
                            {
                                break;
                            }



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



                        case "WorkStartedDate":
                            {
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, WorkStartedDate1.ToString());
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }


                        case "CompletionDate":
                            {
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, WorkorderCompletionDate.ToString());
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


                        case "Causes":
                            {
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, CauseID.ToString());
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
                                string estimateDowntimeText = string.Empty;
                                if (EstimstedDowntimeText != null)
                                {
                                    estimateDowntimeText = Convert.ToString(EstimstedDowntimeText);
                                }
                                validationResult = ValidateValidationsForOverriddennControls1(formLoadItem, estimateDowntimeText);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "ActualDowntime":
                            {
                                string actualDowntimeText = string.Empty;
                                if (ActualDowntimeText != null)
                                {
                                    actualDowntimeText = Convert.ToString(ActualDowntimeText);
                                }
                                validationResult = ValidateValidationsForOverriddennControls1(formLoadItem, actualDowntimeText);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "MiscellaneousLaborCostID":
                            {
                                validationResult = ValidateValidationsForOverriddennControls1(formLoadItem, MiscellaneousLabourCostText);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "MiscellaneousMaterialsCostID":
                            {
                                validationResult = ValidateValidationsForOverriddennControls1(formLoadItem, MiscellaneousMaterialCostText);
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

        private ValidationResult ValidateValidationsForOverriddennControls1(FormControl formControl, string PropertyValue)
        {
            var validationResult = new ValidationResult();
            if ((formControl.IsRequired ?? false) && string.IsNullOrWhiteSpace(PropertyValue))
            {

                validationResult.FailedItem = formControl;
                validationResult.ErrorMessage = formControl.TargetName + " " + ConstantStrings.IsRequiredField;
                return validationResult;
            }
            else
            {
                if ((formControl.IsRequired ?? false) && !string.IsNullOrWhiteSpace(PropertyValue))
                {
                    string Val = PropertyValue.Trim('0', '.', ',');
                    if ( string.IsNullOrWhiteSpace(Val))
                    {
                        validationResult.FailedItem = formControl;
                        validationResult.ErrorMessage = formControl.TargetName + " " + ConstantStrings.IsRequiredField;
                        return validationResult;
                    }
                }
                
            }

            return validationResult;

        }



        public async Task CloseWorkorder()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                //OperationInProgress = true;


                if (WorkorderCompletionDate == null)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleasefillthecompletiondatebeforeclosingtheworkorder"), 2000);
                    return;
                }



                ///TODO: Get Workorder Labour data 
                var workorderLabourWrapper = await _workorderService.GetWorkorderLabour(UserID, WorkorderID.ToString());


                ///TODO: Get Workorder data 
                var workorderWrapper = await _workorderService.GetWorkorderByWorkorderID(UserID, WorkorderID.ToString());



                ///TODO: Get Inspection 
                var Inspection = await _workorderService.GetWorkorderInspection(WorkorderID.ToString(), UserID);


                ///TODO: Get Inspection Time 
                var InspectionTime = await _workorderService.GetWorkorderInspectionTime(UserID, WorkorderID.ToString());


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

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleasesavethecompletiondatebeforeclosingtheworkorder"), 2000);
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
                        if (!Inspection.workOrderEmployee.Any(x => string.IsNullOrEmpty(x.InspectionTime)))
                        {
                            InspectionEmployeeHours = Inspection.workOrderEmployee.All(a => int.Parse((a.InspectionTime)) > 0);
                        }

                        if (!Inspection.workorderContractor.Any(x => string.IsNullOrEmpty(x.InspectionTime)))
                        {
                            InspectionContractorHours = Inspection.workorderContractor.All(a => int.Parse(a.InspectionTime) > 0);
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

                                bool AllTaskHours = workorderLabourWrapper.workOrderWrapper.workOrderLabors.All(a => !string.IsNullOrWhiteSpace(a.HoursAtRate1));

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

                        Application.Current.Properties["SignatureFacID"] = FacilityID;
                        Application.Current.Properties["SignatureWOID"] = WorkorderID;
                        Application.Current.Properties["WorkorderNavigation"] = "True";
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
                            DistributeCost = IsCostDistributed,
                            workOrder = new workOrders
                            {
                                IsSignatureValidated = false,
                                FacilityID = FacilityID,
                                WorkOrderID = WorkorderID,
                                ModifiedUserName = AppSettings.UserName,

                            },

                        };


                        var response = await _workorderService.CloseWorkorder(workorder);

                        if (Boolean.Parse(response.servicestatus))
                        {
                            DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("WorkOrderSuccessfullyClosed"), 2000);
                            await NavigationService.NavigateBackAsync();
                            UserDialogs.Instance.HideLoading();
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
                        DistributeCost = IsCostDistributed,

                        ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                        workOrder = new workOrders
                        {
                            IsSignatureValidated = false,
                            FacilityID = FacilityID,
                            WorkOrderID = WorkorderID,
                            ModifiedUserName = AppSettings.UserName,

                        },

                    };


                    var response = await _workorderService.CloseWorkorder(workorder);

                    if (Boolean.Parse(response.servicestatus))
                    {
                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("WorkOrderSuccessfullyClosed"), 2000);
                        await NavigationService.NavigateBackAsync();
                        UserDialogs.Instance.HideLoading();
                    }
                    else
                    {
                        DialogService.ShowToast(response.servicestatusmessge, 2000);
                        UserDialogs.Instance.HideLoading();
                        return;


                    }
                }

                UserDialogs.Instance.HideLoading();
                //                OperationInProgress = false;

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

        public Task OnViewDisappearingAsync(VisualElement view)
        {

            return Task.FromResult(true);

        }


        public Task IssueWorkorder()
        {
            try
            {
                UserDialogs.Instance.ShowLoading("", MaskType.Gradient);
                Task.Delay(100);
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.WorkOrderId = WorkorderID;
                NavigationService.NavigateToAsync<CreateWorkorderPageViewModel>(tnobj);

            }
            catch (Exception ex)
            {

                UserDialogs.Instance.HideLoading();
            }

            finally
            {
                UserDialogs.Instance.HideLoading();

            }

            return Task.CompletedTask;
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
        public static bool IsPhoneNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return true;
            }
            return number.All(char.IsDigit);
        }

        public async Task ShowSignatures()
        {

            try
            {
                UserDialogs.Instance.HideLoading();

                TargetNavigationData tnobj = new TargetNavigationData();
                if (workorderWrapper.workOrderWrapper != null && workorderWrapper.workOrderWrapper.SignatureAuditDetails != null)
                {

                    tnobj.SignatureAuditDetails = workorderWrapper.workOrderWrapper.SignatureAuditDetails;
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
        public async Task ShowMoreDescription()
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

        public async Task ShowMore1Description()
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
        public async Task ShowMoreAdditionalDetails()
        {

            try
            {
                UserDialogs.Instance.HideLoading();

                TargetNavigationData tnobj = new TargetNavigationData();
                //if (Device.Idiom == TargetIdiom.Phone)
                //{
                //    tnobj.Description = AdditionalDetailsTextForMobile;
                //}
                //else
                //{
                tnobj.Description = AdditionalDetailsMoreText;
                // }

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

        public async Task InternalNotesDetails()
        {

            try
            {
                UserDialogs.Instance.HideLoading();

                TargetNavigationData tnobj = new TargetNavigationData();
                //if (Device.Idiom == TargetIdiom.Phone)
                //{
                //    tnobj.Description = InternalNotesTextForMobile;
                //}

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
        public static bool IsNumber(string s)
        {
            return s.All(char.IsNumber);
        }



    }
}
