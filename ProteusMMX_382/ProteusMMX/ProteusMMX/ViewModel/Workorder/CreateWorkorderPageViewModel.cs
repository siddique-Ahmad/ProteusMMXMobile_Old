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
using ProteusMMX.Services.SelectionListPageServices;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Services.Workorder.TaskAndLabour;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.SelectionListPagesViewModels;
using ProteusMMX.Views.Common;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
using Syncfusion.XForms.Border;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProteusMMX.ViewModel.Workorder
{
    public class CreateWorkorderPageViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        #region Fields

        string CurrentRuntimeEnablevalue = string.Empty;
        string CurrentRuntimeVisiblevalue = string.Empty;

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IAssetModuleService _assetService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly IWorkorderService _workorderService;

        protected readonly ITaskAndLabourService _taskAndLabourService;

        protected readonly IFacilityService _facilityService;


        #endregion

        #region Properties
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
        #region Page Properties
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

        string _selectOptionsTitle = "Select Options";
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
        string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }

            set
            {
                if (value != _userName)
                {
                    _userName = value;
                    OnPropertyChanged("UserName");
                }
            }
        }

        #region **** Validation ****
        //Description
        string _descriptionColor;
        public string DescriptionColor
        {
            get
            {
                return _descriptionColor;
            }

            set
            {
                if (value != _descriptionColor)
                {
                    _descriptionColor = value;
                    OnPropertyChanged(nameof(DescriptionColor));
                }
            }
        }

        string _descriptionIsRequred;
        public string DescriptionIsRequred
        {
            get
            {
                return _descriptionIsRequred;
            }

            set
            {
                if (value != _descriptionIsRequred)
                {
                    _descriptionIsRequred = value;
                    OnPropertyChanged(nameof(DescriptionIsRequred));
                }
            }
        }

        //WorkOrderStatusID
        string _workOrderStatusColor;
        public string WorkOrderStatusColor
        {
            get
            {
                return _workOrderStatusColor;
            }

            set
            {
                if (value != _workOrderStatusColor)
                {
                    _workOrderStatusColor = value;
                    OnPropertyChanged(nameof(WorkOrderStatusColor));
                }
            }
        }

        string _workOrderStatusIsRequred;
        public string WorkOrderStatusIsRequred
        {
            get
            {
                return _workOrderStatusIsRequred;
            }

            set
            {
                if (value != _workOrderStatusIsRequred)
                {
                    _workOrderStatusIsRequred = value;
                    OnPropertyChanged(nameof(WorkOrderStatusIsRequred));
                }
            }
        }

        //WorkTypeID
        string _currentRuntimeColor;
        public string CurrentRuntimeColor
        {
            get
            {
                return _currentRuntimeColor;
            }

            set
            {
                if (value != _currentRuntimeColor)
                {
                    _currentRuntimeColor = value;
                    OnPropertyChanged(nameof(CurrentRuntimeColor));
                }
            }
        }

        string _currentRuntimeIsRequred;
        public string CurrentRuntimeIsRequred
        {
            get
            {
                return _currentRuntimeIsRequred;
            }

            set
            {
                if (value != _currentRuntimeIsRequred)
                {
                    _currentRuntimeIsRequred = value;
                    OnPropertyChanged(nameof(CurrentRuntimeIsRequred));
                }
            }
        }

        //WorkTypeID
        string _workTypeIDColor;
        public string WorkTypeIDColor
        {
            get
            {
                return _workTypeIDColor;
            }

            set
            {
                if (value != _workTypeIDColor)
                {
                    _workTypeIDColor = value;
                    OnPropertyChanged(nameof(WorkTypeIDColor));
                }
            }
        }

        string _workTypeIDIsRequred;
        public string WorkTypeIDIsRequred
        {
            get
            {
                return _workTypeIDIsRequred;
            }

            set
            {
                if (value != _workTypeIDIsRequred)
                {
                    _workTypeIDIsRequred = value;
                    OnPropertyChanged(nameof(WorkTypeIDIsRequred));
                }
            }
        }


        //cause
        string _causeColor;
        public string CauseColor
        {
            get
            {
                return _causeColor;
            }

            set
            {
                if (value != _causeColor)
                {
                    _causeColor = value;
                    OnPropertyChanged(nameof(CauseColor));
                }
            }
        }

        string _causeIsRequred;
        public string CauseIsRequred
        {
            get
            {
                return _causeIsRequred;
            }

            set
            {
                if (value != _causeIsRequred)
                {
                    _causeIsRequred = value;
                    OnPropertyChanged(nameof(CauseIsRequred));
                }
            }
        }

        //MaintenanceCode
        string _maintenanceCodeColor;
        public string MaintenanceCodeColor
        {
            get
            {
                return _maintenanceCodeColor;
            }

            set
            {
                if (value != _maintenanceCodeColor)
                {
                    _maintenanceCodeColor = value;
                    OnPropertyChanged(nameof(MaintenanceCodeColor));
                }
            }
        }

        string _maintenanceCodeIsRequred;
        public string MaintenanceCodeIsRequred
        {
            get
            {
                return _maintenanceCodeIsRequred;
            }

            set
            {
                if (value != _maintenanceCodeIsRequred)
                {
                    _maintenanceCodeIsRequred = value;
                    OnPropertyChanged(nameof(MaintenanceCodeIsRequred));
                }
            }
        }

        //EstimstedDowntime
        string _estimstedDowntimeColor;
        public string EstimstedDowntimeColor
        {
            get
            {
                return _estimstedDowntimeColor;
            }

            set
            {
                if (value != _estimstedDowntimeColor)
                {
                    _estimstedDowntimeColor = value;
                    OnPropertyChanged(nameof(EstimstedDowntimeColor));
                }
            }
        }

        string _estimstedDowntimeIsRequred;
        public string EstimstedDowntimeIsRequred
        {
            get
            {
                return _estimstedDowntimeIsRequred;
            }

            set
            {
                if (value != _estimstedDowntimeIsRequred)
                {
                    _estimstedDowntimeIsRequred = value;
                    OnPropertyChanged(nameof(EstimstedDowntimeIsRequred));
                }
            }
        }


        //ActualDowntime
        string _actualDowntimeColor;
        public string ActualDowntimeColor
        {
            get
            {
                return _actualDowntimeColor;
            }

            set
            {
                if (value != _actualDowntimeColor)
                {
                    _actualDowntimeColor = value;
                    OnPropertyChanged(nameof(ActualDowntimeColor));
                }
            }
        }

        string _actualDowntimeIsRequred;
        public string ActualDowntimeIsRequred
        {
            get
            {
                return _actualDowntimeIsRequred;
            }

            set
            {
                if (value != _actualDowntimeIsRequred)
                {
                    _actualDowntimeIsRequred = value;
                    OnPropertyChanged(nameof(ActualDowntimeIsRequred));
                }
            }
        }


        //MiscellaneousLaborCostID
        string _miscellaneousLaborCostIDColor;
        public string MiscellaneousLaborCostIDColor
        {
            get
            {
                return _miscellaneousLaborCostIDColor;
            }

            set
            {
                if (value != _miscellaneousLaborCostIDColor)
                {
                    _miscellaneousLaborCostIDColor = value;
                    OnPropertyChanged(nameof(MiscellaneousLaborCostIDColor));
                }
            }
        }

        string _miscellaneousLaborCostIDIsRequred;
        public string MiscellaneousLaborCostIDIsRequred
        {
            get
            {
                return _miscellaneousLaborCostIDIsRequred;
            }

            set
            {
                if (value != _miscellaneousLaborCostIDIsRequred)
                {
                    _miscellaneousLaborCostIDIsRequred = value;
                    OnPropertyChanged(nameof(MiscellaneousLaborCostIDIsRequred));
                }
            }
        }

        //MiscellaneousMaterialsCostID
        string _miscellaneousMaterialsCostIDColor;
        public string MiscellaneousMaterialsCostIDColor
        {
            get
            {
                return _miscellaneousMaterialsCostIDColor;
            }

            set
            {
                if (value != _miscellaneousMaterialsCostIDColor)
                {
                    _miscellaneousMaterialsCostIDColor = value;
                    OnPropertyChanged(nameof(MiscellaneousMaterialsCostIDColor));
                }
            }
        }

        string _miscellaneousMaterialsCostIDIsRequred;
        public string MiscellaneousMaterialsCostIDIsRequred
        {
            get
            {
                return _miscellaneousMaterialsCostIDIsRequred;
            }

            set
            {
                if (value != _miscellaneousMaterialsCostIDIsRequred)
                {
                    _miscellaneousMaterialsCostIDIsRequred = value;
                    OnPropertyChanged(nameof(MiscellaneousMaterialsCostIDIsRequred));
                }
            }
        }

        //Description
        string _facilityColor;
        public string FacilityColor
        {
            get
            {
                return _facilityColor;
            }

            set
            {
                if (value != _facilityColor)
                {
                    _facilityColor = value;
                    OnPropertyChanged(nameof(FacilityColor));
                }
            }
        }

        string _facilityIsRequred;
        public string FacilityIsRequred
        {
            get
            {
                return _facilityIsRequred;
            }

            set
            {
                if (value != _facilityIsRequred)
                {
                    _facilityIsRequred = value;
                    OnPropertyChanged(nameof(FacilityIsRequred));
                }
            }
        }
       
        // ShiftID       
        string _shiftIDColor;
        public string ShiftIDColor
        {
            get
            {
                return _shiftIDColor;
            }

            set
            {
                if (value != _shiftIDColor)
                {
                    _shiftIDColor = value;
                    OnPropertyChanged(nameof(ShiftIDColor));
                }
            }
        }

        string _shiftIDIsRequred;
        public string ShiftIDIsRequred
        {
            get
            {
                return _shiftIDIsRequred;
            }

            set
            {
                if (value != _shiftIDIsRequred)
                {
                    _shiftIDIsRequred = value;
                    OnPropertyChanged(nameof(ShiftIDIsRequred));
                }
            }
        }

        // PriorityID       
        string _priorityIDColor;
        public string PriorityIDColor
        {
            get
            {
                return _priorityIDColor;
            }

            set
            {
                if (value != _priorityIDColor)
                {
                    _priorityIDColor = value;
                    OnPropertyChanged(nameof(PriorityIDColor));
                }
            }
        }

        string _priorityIDIsRequred;
        public string PriorityIDIsRequred
        {
            get
            {
                return _priorityIDIsRequred;
            }

            set
            {
                if (value != _priorityIDIsRequred)
                {
                    _priorityIDIsRequred = value;
                    OnPropertyChanged(nameof(PriorityIDIsRequred));
                }
            }
        }

        // AssignedToEmployeeID       
        string _assignedToEmployeeIDColor;
        public string AssignedToEmployeeIDColor
        {
            get
            {
                return _assignedToEmployeeIDColor;
            }

            set
            {
                if (value != _assignedToEmployeeIDColor)
                {
                    _assignedToEmployeeIDColor = value;
                    OnPropertyChanged(nameof(AssignedToEmployeeIDColor));
                }
            }
        }

        string _assignedToEmployeeIDIsRequred;
        public string AssignedToEmployeeIDIsRequred
        {
            get
            {
                return _assignedToEmployeeIDIsRequred;
            }

            set
            {
                if (value != _assignedToEmployeeIDIsRequred)
                {
                    _assignedToEmployeeIDIsRequred = value;
                    OnPropertyChanged(nameof(AssignedToEmployeeIDIsRequred));
                }
            }
        }

        // EstimatedDowntime       
        string _estimatedDowntimeColor;
        public string EstimatedDowntimeColor
        {
            get
            {
                return _estimatedDowntimeColor;
            }

            set
            {
                if (value != _estimatedDowntimeColor)
                {
                    _estimatedDowntimeColor = value;
                    OnPropertyChanged(nameof(EstimatedDowntimeColor));
                }
            }
        }

        string _estimatedDowntimeIsRequred;
        public string EstimatedDowntimeIsRequred
        {
            get
            {
                return _estimatedDowntimeIsRequred;
            }

            set
            {
                if (value != _estimatedDowntimeIsRequred)
                {
                    _estimatedDowntimeIsRequred = value;
                    OnPropertyChanged(nameof(EstimatedDowntimeIsRequred));
                }
            }
        }

        // Originator       
        string _originatorColor;
        public string OriginatorColor
        {
            get
            {
                return _originatorColor;
            }

            set
            {
                if (value != _originatorColor)
                {
                    _originatorColor = value;
                    OnPropertyChanged(nameof(OriginatorColor));
                }
            }
        }

        string _originatorIsRequred;
        public string OriginatorIsRequred
        {
            get
            {
                return _originatorIsRequred;
            }

            set
            {
                if (value != _originatorIsRequred)
                {
                    _originatorIsRequred = value;
                    OnPropertyChanged(nameof(OriginatorIsRequred));
                }
            }
        }

        // workOrderRequesterID       
        string _workOrderRequesterIDColor;
        public string WorkOrderRequesterIDColor
        {
            get
            {
                return _workOrderRequesterIDColor;
            }

            set
            {
                if (value != _workOrderRequesterIDColor)
                {
                    _workOrderRequesterIDColor = value;
                    OnPropertyChanged(nameof(WorkOrderRequesterIDColor));
                }
            }
        }

        string _workOrderRequesterIDIsRequred;
        public string WorkOrderRequesterIDIsRequred
        {
            get
            {
                return _workOrderRequesterIDIsRequred;
            }

            set
            {
                if (value != _workOrderRequesterIDIsRequred)
                {
                    _workOrderRequesterIDIsRequred = value;
                    OnPropertyChanged(nameof(WorkOrderRequesterIDIsRequred));
                }
            }
        }

        // CostCenterID       
        string _costCenterIDColor;
        public string CostCenterIDColor
        {
            get
            {
                return _costCenterIDColor;
            }

            set
            {
                if (value != _costCenterIDColor)
                {
                    _costCenterIDColor = value;
                    OnPropertyChanged(nameof(CostCenterIDColor));
                }
            }
        }

        string _costCenterIDIsRequred;
        public string CostCenterIDIsRequred
        {
            get
            {
                return _costCenterIDIsRequred;
            }

            set
            {
                if (value != _costCenterIDIsRequred)
                {
                    _costCenterIDIsRequred = value;
                    OnPropertyChanged(nameof(CostCenterIDIsRequred));
                }
            }
        }

        #endregion

        #region CreateWorkorder Properties





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

        bool _childCostDistributed = true;
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
        //DateTime _requiredDate = DateTime.Now;
        DateTime _requiredDate1 = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);
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
        DateTime? _workStartedDate1 = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);
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

        DateTime? _maximumWorkStartedDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);
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

        DateTime? _maximumWorkorderCompletionDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);
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

        bool _currentRuntimeIsVisible = false;
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

        string _CurrentRuntimeTitle;
        public string CurrentRuntimeTitle
        {
            get
            {
                return _CurrentRuntimeTitle;
            }

            set
            {
                if (value != _CurrentRuntimeTitle)
                {
                    _CurrentRuntimeTitle = value;
                    OnPropertyChanged(nameof(CurrentRuntimeTitle));
                }
            }
        }

        bool _CurrentRuntimeIsEnable = true;
        public bool CurrentRuntimeIsEnable
        {
            get
            {
                return _CurrentRuntimeIsEnable;
            }

            set
            {
                if (value != _CurrentRuntimeIsEnable)
                {
                    _CurrentRuntimeIsEnable = value;
                    OnPropertyChanged(nameof(CurrentRuntimeIsEnable));
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
        string _activationDateText;
        public string ActivationDateText
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
        string _lottoUrl;
        public string LottoUrl
        {
            get
            {
                return _lottoUrl;
            }

            set
            {
                if (value != _lottoUrl)
                {
                    _lottoUrl = value;
                    OnPropertyChanged(nameof(LottoUrl));
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
        string _requestedDate;
        public string RequestedDate
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


        //Save Command
        public ICommand SaveWorkorderCommand => new AsyncCommand(CreateWorkorder);

        public ICommand TapCommand => new AsyncCommand(SpeechtoText);

        #endregion

        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                OperationInProgress = true;
                if (navigationData != null)
                {
                    var navigationParams = navigationData as TargetNavigationData;
                    if (navigationParams.WorkOrderId > 0)
                    {
                        this.WorkorderID = navigationParams.WorkOrderId;

                    }
                    //Set Facility
                    if (navigationParams.FacilityID != null)
                    {
                        this.FacilityID = navigationParams.FacilityID;
                        this.FacilityName = navigationParams.FacilityName;
                    }
                    //Set Location
                    if (navigationParams.LocationID != null)
                    {
                        this.LocationID = navigationParams.LocationID;
                        this.LocationName = navigationParams.LocationName;
                    }
                    //Set Asset
                    if (navigationParams.AssetID != 0)
                    {
                        this.AssetID = navigationParams.AssetID;
                        this.AssetName = navigationParams.AssetName;
                    }
                    //Set AssetSystem
                    if (navigationParams.AssetID != AssetSystemID)
                    {
                        this.AssetSystemID = navigationParams.AssetSystemID;
                        this.AssetSystemName = navigationParams.AssetSystemName;
                    }


                    //Set CurrentRuntime
                    if (navigationParams.CurrentRuntime != null)
                    {
                        this.CurrentRuntimeText = navigationParams.CurrentRuntime.ToString();

                    }
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
                var employeeAssignTo = await _workorderService.GetEmployeeAssignTo(UserID);
                if (employeeAssignTo != null && employeeAssignTo.employeesAssignedTo != null && employeeAssignTo.employeesAssignedTo.Count > 0)
                {
                    this.AssignToEmployeeID = employeeAssignTo.employeesAssignedTo.FirstOrDefault().SelectedValue;
                    this.AssignToEmployeeName = employeeAssignTo.employeesAssignedTo.FirstOrDefault().SelectedText;
                }
                if (Device.Idiom == TargetIdiom.Phone)
                {
                    this.IsCostLayoutIsVisibleForPhone = true;
                }
                else
                {
                    this.IsCostLayoutIsVisibleForTab = true;
                }
                await GetWorkorderControlRights();
                OriginatorName = AppSettings.User.UserName;
                await SetTitlesPropertiesForPage();
                await CreateControlsForPage();
                if (this.WorkorderID > 0)
                {
                    await SetControlsPropertiesForPage();
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
        public CreateWorkorderPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IWorkorderService workorderService, IFacilityService facilityService, IAssetModuleService assetService, ITaskAndLabourService taskAndLabourService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _workorderService = workorderService;
            _facilityService = facilityService;
            _assetService = assetService;
            _taskAndLabourService = taskAndLabourService;
        }

        public async Task SetTitlesPropertiesForPage()
        {
            try
            {
                OriginatorTitle = "Originator";
                CurrentRuntimeTitle = WebControlTitle.GetTargetNameByTitleName("CurrentRuntime");
                PageTitle = WebControlTitle.GetTargetNameByTitleName("CreateWorkOrder");
                WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                WorkorderTitle = WebControlTitle.GetTargetNameByTitleName("WorkOrder");
                CloseWorkorderTitle = WebControlTitle.GetTargetNameByTitleName("ClosedWorkOrder");
                InventoryTransactionTitle = WebControlTitle.GetTargetNameByTitleName("InventoryTransaction");
                ServiceRequestTitle = WebControlTitle.GetTargetNameByTitleName("ServiceRequest");
                AssetsTitle = WebControlTitle.GetTargetNameByTitleName("Assets");
                BarcodeTitle = WebControlTitle.GetTargetNameByTitleName("SearchScanBarcode");
                ReceivingTitle = WebControlTitle.GetTargetNameByTitleName("Receiving");
                SaveTitle = WebControlTitle.GetTargetNameByTitleName("Save");
                SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                FacilityTitle = WebControlTitle.GetTargetNameByTitleName("Facility");
                LocationTitle = WebControlTitle.GetTargetNameByTitleName("Location");
                AssetsTitle = WebControlTitle.GetTargetNameByTitleName("Asset");
                AssetSystemTitle = WebControlTitle.GetTargetNameByTitleName("AssetSystem");
                InternalNotesTitle = WebControlTitle.GetTargetNameByTitleName("InternalNote");
                ChargeCostsOnlyToChildAssets = WebControlTitle.GetTargetNameByTitleName("ChargeCostsOnlyToChildAssets");
                ParentCostsOnly = WebControlTitle.GetTargetNameByTitleName("Chargecosttotheparentsystemandchildassets");
                AdditionalDetailsTitle = WebControlTitle.GetTargetNameByTitleName("Notes");
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
            if (Application.Current.Properties.ContainsKey("WorkorderDetailsControls"))
            {
                SubModule WorkorderDetails = Application.Current.Properties["WorkorderDetailsControls"] as SubModule;
                WorkorderControlsNew = WorkorderDetails.listControls;
            }

            if (Application.Current.Properties.ContainsKey("CreateWorkorderRights"))
            {
                var Edit = Application.Current.Properties["CreateWorkorderRights"].ToString();
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
                    ////WorkTypeID
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
                        if (CurrentRuntime.IsRequired ?? true)
                        {
                            CurrentRuntimeIsRequred = "true";
                            CurrentRuntimeColor = "Red";
                        }
                        else
                        {
                            CurrentRuntimeColor = "false";
                        }

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
                        if (description.IsRequired ?? true)
                        {
                            DescriptionIsRequred = "true";
                            DescriptionColor = "Red";
                        }
                        else
                        {
                            DescriptionIsRequred = "false";
                        }
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
                        if (AssignedToEmployeeID.IsRequired ?? true)
                        {
                            AssignedToEmployeeIDIsRequred = "true";
                            AssignedToEmployeeIDColor = "Red";
                        }
                        else
                        {
                            AssignedToEmployeeIDIsRequred = "false";
                        }
                    }

                    var WorkOrderRequesterID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "WorkOrderRequesterID");
                    if (WorkOrderRequesterID != null)
                    {
                        WorkorderRequesterTitle = WorkOrderRequesterID.TargetName;
                        OverriddenControlsNew.Add(WorkOrderRequesterID);
                        WorkorderControlsNew.Remove(WorkOrderRequesterID);

                        if (WorkOrderRequesterID.IsRequired ?? true)
                        {
                            WorkOrderRequesterIDIsRequred = "true";
                            WorkOrderRequesterIDColor = "Red";
                        }
                        else
                        {
                            WorkOrderRequesterIDIsRequred = "false";
                        }
                    }

                    var CostCenterID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "CostCenterID");
                    if (CostCenterID != null)
                    {
                        CostCenterTitle = CostCenterID.TargetName;
                        OverriddenControlsNew.Add(CostCenterID);
                        WorkorderControlsNew.Remove(CostCenterID);
                        WorkorderControlsNew.Remove(description);
                        if (CostCenterID.IsRequired ?? true)
                        {
                            CostCenterIDIsRequred = "true";
                            CostCenterIDColor = "Red";
                        }
                        else
                        {
                            CostCenterIDIsRequred = "false";
                        }
                    }

                    var PriorityID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "PriorityID");
                    if (PriorityID != null)
                    {
                        PriorityTitle = PriorityID.TargetName;
                        OverriddenControlsNew.Add(PriorityID);
                        WorkorderControlsNew.Remove(PriorityID);
                        if (PriorityID.IsRequired ?? true)
                        {
                            PriorityIDIsRequred = "true";
                            PriorityIDColor = "Red";
                        }
                        else
                        {
                            PriorityIDIsRequred = "false";
                        }
                    }

                    var ShiftID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "ShiftID");
                    if (ShiftID != null)
                    {
                        ShiftTitle = ShiftID.TargetName;
                        OverriddenControlsNew.Add(ShiftID);
                        WorkorderControlsNew.Remove(ShiftID);
                        if (ShiftID.IsRequired ?? true)
                        {
                            ShiftIDIsRequred = "true";
                            ShiftIDColor = "Red";
                        }
                        else
                        {
                            ShiftIDIsRequred = "false";
                        }
                    }

                    var WorkOrderStatusID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "WorkOrderStatusID");
                    if (WorkOrderStatusID != null)
                    {
                        WorkorderStatusTitle = WorkOrderStatusID.TargetName;
                        OverriddenControlsNew.Add(WorkOrderStatusID);
                        WorkorderControlsNew.Remove(WorkOrderStatusID);
                        if (WorkOrderStatusID.IsRequired ?? true)
                        {
                            WorkOrderStatusIsRequred = "true";
                            WorkOrderStatusColor = "Red";
                        }
                        else
                        {
                            WorkOrderStatusIsRequred = "false";
                        }
                    }

                    var WorkTypeID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "WorkTypeID");
                    if (WorkTypeID != null)
                    {
                        WorkorderTypeTitle = WorkTypeID.TargetName;
                        OverriddenControlsNew.Add(WorkTypeID);
                        WorkorderControlsNew.Remove(WorkTypeID);
                        if (WorkTypeID.IsRequired ?? true)
                        {
                            WorkTypeIDIsRequred = "true";
                            WorkTypeIDColor = "Red";
                        }
                        else
                        {
                            WorkTypeIDIsRequred = "false";
                        }
                    }


                    var CauseID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "Causes");
                    if (CauseID != null)
                    {
                        CauseTitle = CauseID.TargetName;
                        OverriddenControlsNew.Add(CauseID);
                        WorkorderControlsNew.Remove(CauseID);
                        if (CauseID.IsRequired ?? true)
                        {
                            CauseIsRequred = "true";
                            CauseColor = "Red";
                        }
                        else
                        {
                            CauseIsRequred = "false";
                        }
                    }


                    var MaintenanceCodeID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "MaintenanceCodeID");
                    if (MaintenanceCodeID != null)
                    {
                        MaintenanceCodeTitle = MaintenanceCodeID.TargetName;
                        OverriddenControlsNew.Add(MaintenanceCodeID);
                        WorkorderControlsNew.Remove(MaintenanceCodeID);
                        if (MaintenanceCodeID.IsRequired ?? true)
                        {
                            MaintenanceCodeIsRequred = "true";
                            MaintenanceCodeColor = "Red";
                        }
                        else
                        {
                            MaintenanceCodeColor = "false";
                        }
                    }


                    var EstimatedDowntime = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "EstimatedDowntime");
                    if (EstimatedDowntime != null)
                    {
                        EstimstedDowntimeTitle = EstimatedDowntime.TargetName;
                        OverriddenControlsNew.Add(EstimatedDowntime);
                        WorkorderControlsNew.Remove(EstimatedDowntime);
                        if (EstimatedDowntime.IsRequired ?? true)
                        {
                            EstimstedDowntimeIsRequred = "true";
                            EstimstedDowntimeColor = "Red";
                        }
                        else
                        {
                            EstimstedDowntimeColor = "false";
                        }
                    }

                    var ActualDowntime = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "ActualDowntime");
                    if (ActualDowntime != null)
                    {
                        ActualDowntimeTitle = ActualDowntime.TargetName;
                        OverriddenControlsNew.Add(ActualDowntime);
                        WorkorderControlsNew.Remove(ActualDowntime);
                        if (ActualDowntime.IsRequired ?? true)
                        {
                            ActualDowntimeIsRequred = "true";
                            ActualDowntimeColor = "Red";
                        }
                        else
                        {
                            ActualDowntimeColor = "false";
                        }
                    }

                    var MiscellaneousLaborCostID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "MiscellaneousLaborCostID");
                    if (MiscellaneousLaborCostID != null)
                    {
                        MiscellaneousLabourCostTitle = MiscellaneousLaborCostID.TargetName;
                        OverriddenControlsNew.Add(MiscellaneousLaborCostID);
                        WorkorderControlsNew.Remove(MiscellaneousLaborCostID);
                        if (MiscellaneousLaborCostID.IsRequired ?? true)
                        {
                            MiscellaneousLaborCostIDIsRequred = "true";
                            MiscellaneousLaborCostIDColor = "Red";
                        }
                        else
                        {
                            MiscellaneousLaborCostIDColor = "false";
                        }
                    }

                    var MiscellaneousMaterialsCostID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "MiscellaneousMaterialsCostID");
                    if (MiscellaneousMaterialsCostID != null)
                    {
                        MiscellaneousMaterialCostTitle = MiscellaneousMaterialsCostID.TargetName;
                        OverriddenControlsNew.Add(MiscellaneousMaterialsCostID);
                        WorkorderControlsNew.Remove(MiscellaneousMaterialsCostID);
                        if (MiscellaneousMaterialsCostID.IsRequired ?? true)
                        {
                            MiscellaneousMaterialsCostIDIsRequred = "true";
                            MiscellaneousMaterialsCostIDColor = "Red";
                        }
                        else
                        {
                            MiscellaneousMaterialsCostIDColor = "false";
                        }
                    }

                    var WorkOrderRequester = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "WorkOrderRequesterID");
                    if (WorkOrderRequester != null)
                    {
                        if (WorkOrderRequester.IsRequired ?? true)
                        {
                            WorkOrderRequesterIDIsRequred = "true";
                            WorkOrderRequesterIDColor = "Red";
                        }
                        else
                        {
                            MiscellaneousMaterialsCostIDColor = "false";
                        }
                    }

                    var Originator = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "Originator");
                    if (Originator != null)
                    {
                        if (Originator.IsRequired ?? true)
                        {
                            OriginatorIsRequred = "true";
                            OriginatorColor = "Red";
                        }
                        else
                        {
                            OriginatorColor = "false";
                        }
                    }

                    //var ShiftID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "ShiftID");
                    //var PriorityID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "PriorityID");
                    //var AssignedToEmployeeID = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "AssignedToEmployeeID");

                    //var EstimatedDowntime = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "EstimatedDowntime");
                    //var ActualDowntime = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "ActualDowntime");

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

                                    WorkorderStatusIsVisible = ApplyIsVisible(item.Expression);
                                    WorkorderStatusIsEnable = ApplyIsEnable(item.Expression);
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
                WorkorderControlsNew.RemoveAll((i => i.ControlName == "CurrentRuntime"));
                WorkorderControlsNew.RemoveAll((i => i.ControlName == "ActivationDate"));
                WorkorderControlsNew.RemoveAll((i => i.ControlName == "RequestedDate"));
                WorkorderControlsNew.RemoveAll((i => i.ControlName == "RequesterEmail"));
                WorkorderControlsNew.RemoveAll((i => i.ControlName == "RequestNumber"));

                WorkorderControlsNew.RemoveAll((i => i.ControlName == "RiskQuestion"));
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

            #region Old Code


            ///// Remove static controls titles so they won't render twice.
            ///// Apply visibility according to expression on Overridden controls 
            //#region Remove Overridden controls from titles

            //if (titles != null && titles.CFLI != null && titles.CFLI.Count > 0)
            //{

            //    try
            //    {
            //        ///WorkOrderNumber
            //        ///JobNumber
            //        ///Description
            //        ///RequiredDate
            //        ///WorkStartedDate
            //        ///CompletionDate
            //        ///AssignedToEmployeeID
            //        ///WorkOrderRequesterID
            //        ///CostCenterID
            //        ///PriorityID
            //        ///ShiftID
            //        ///WorkOrderStatusID
            //        ///WorkTypeID
            //        ///UserField22
            //        ///MaintenanceCodeID




            //        var WorkOrderNumber = titles.CFLI.FirstOrDefault(x => x.FieldName == "WorkOrderNumber");
            //        if (WorkOrderNumber != null)
            //        {
            //            OverriddenControls.Add(WorkOrderNumber);
            //            titles.CFLI.Remove(WorkOrderNumber);
            //        }

            //        var JobNumber = titles.CFLI.FirstOrDefault(x => x.FieldName == "JobNumber");
            //        if (JobNumber != null)
            //        {
            //            OverriddenControls.Add(JobNumber);
            //            titles.CFLI.Remove(JobNumber);
            //        }

            //        var description = titles.CFLI.FirstOrDefault(x => x.FieldName == "Description");
            //        if (description != null)
            //        {
            //            OverriddenControls.Add(description);
            //            titles.CFLI.Remove(description);
            //        }

            //        var RequiredDate = titles.CFLI.FirstOrDefault(x => x.FieldName == "RequiredDate");
            //        if (RequiredDate != null)
            //        {
            //            OverriddenControls.Add(RequiredDate);
            //            titles.CFLI.Remove(RequiredDate);
            //        }

            //        var WorkStartedDate = titles.CFLI.FirstOrDefault(x => x.FieldName == "WorkStartedDate");
            //        if (WorkStartedDate != null)
            //        {
            //            OverriddenControls.Add(WorkStartedDate);
            //            titles.CFLI.Remove(WorkStartedDate);
            //        }

            //        var CompletionDate = titles.CFLI.FirstOrDefault(x => x.FieldName == "CompletionDate");
            //        if (CompletionDate != null)
            //        {
            //            OverriddenControls.Add(CompletionDate);
            //            titles.CFLI.Remove(CompletionDate);
            //        }

            //        var AssignedToEmployeeID = titles.CFLI.FirstOrDefault(x => x.FieldName == "AssignedToEmployeeID");
            //        if (AssignedToEmployeeID != null)
            //        {
            //            OverriddenControls.Add(AssignedToEmployeeID);
            //            titles.CFLI.Remove(AssignedToEmployeeID);
            //        }

            //        var WorkOrderRequesterID = titles.CFLI.FirstOrDefault(x => x.FieldName == "WorkOrderRequesterID");
            //        if (WorkOrderRequesterID != null)
            //        {
            //            OverriddenControls.Add(WorkOrderRequesterID);
            //            titles.CFLI.Remove(WorkOrderRequesterID);
            //        }

            //        var CostCenterID = titles.CFLI.FirstOrDefault(x => x.FieldName == "CostCenterID");
            //        if (CostCenterID != null)
            //        {
            //            OverriddenControls.Add(CostCenterID);
            //            titles.CFLI.Remove(CostCenterID);
            //        }

            //        var PriorityID = titles.CFLI.FirstOrDefault(x => x.FieldName == "PriorityID");
            //        if (PriorityID != null)
            //        {
            //            OverriddenControls.Add(PriorityID);
            //            titles.CFLI.Remove(PriorityID);
            //        }

            //        var ShiftID = titles.CFLI.FirstOrDefault(x => x.FieldName == "ShiftID");
            //        if (ShiftID != null)
            //        {
            //            OverriddenControls.Add(ShiftID);
            //            titles.CFLI.Remove(ShiftID);
            //        }

            //        var WorkOrderStatusID = titles.CFLI.FirstOrDefault(x => x.FieldName == "WorkOrderStatusID");
            //        if (WorkOrderStatusID != null)
            //        {
            //            OverriddenControls.Add(WorkOrderStatusID);
            //            titles.CFLI.Remove(WorkOrderStatusID);
            //        }

            //        var WorkTypeID = titles.CFLI.FirstOrDefault(x => x.FieldName == "WorkTypeID");
            //        if (WorkTypeID != null)
            //        {
            //            OverriddenControls.Add(WorkTypeID);
            //            titles.CFLI.Remove(WorkTypeID);
            //        }

            //        var MaintenanceCodeID = titles.CFLI.FirstOrDefault(x => x.FieldName == "MaintenanceCodeID");
            //        if (MaintenanceCodeID != null)
            //        {
            //            OverriddenControls.Add(MaintenanceCodeID);
            //            titles.CFLI.Remove(MaintenanceCodeID);
            //        }


            //        var EstimatedDowntime = titles.CFLI.FirstOrDefault(x => x.FieldName == "EstimatedDowntime");
            //        if (EstimatedDowntime != null)
            //        {
            //            OverriddenControls.Add(EstimatedDowntime);
            //            titles.CFLI.Remove(EstimatedDowntime);
            //        }

            //        var ActualDowntime = titles.CFLI.FirstOrDefault(x => x.FieldName == "ActualDowntime");
            //        if (ActualDowntime != null)
            //        {
            //            OverriddenControls.Add(ActualDowntime);
            //            titles.CFLI.Remove(ActualDowntime);
            //        }

            //        var MiscellaneousLaborCostID = titles.CFLI.FirstOrDefault(x => x.FieldName == "MiscellaneousLaborCostID");
            //        if (MiscellaneousLaborCostID != null)
            //        {
            //            OverriddenControls.Add(MiscellaneousLaborCostID);
            //            titles.CFLI.Remove(MiscellaneousLaborCostID);
            //        }

            //        var MiscellaneousMaterialsCostID = titles.CFLI.FirstOrDefault(x => x.FieldName == "MiscellaneousMaterialsCostID");
            //        if (MiscellaneousMaterialsCostID != null)
            //        {
            //            OverriddenControls.Add(MiscellaneousMaterialsCostID);
            //            titles.CFLI.Remove(MiscellaneousMaterialsCostID);
            //        }






            //    }
            //    catch (Exception ex)
            //    {


            //    }

            //}

            //#endregion

            //#region Apply visibility according to expression on Overridden controls

            //if (OverriddenControls != null && OverriddenControls.Count > 0)
            //{

            //    try
            //    {
            //        ///WorkOrderNumber
            //        ///JobNumber
            //        ///Description
            //        ///RequiredDate
            //        ///WorkStartedDate
            //        ///CompletionDate
            //        ///AssignedToEmployeeID
            //        ///WorkOrderRequesterID
            //        ///CostCenterID
            //        ///PriorityID
            //        ///ShiftID
            //        ///WorkOrderStatusID
            //        ///WorkTypeID
            //        ///UserField22
            //        ///MaintenanceCodeID

            //        var formRoles = titles.lstRoles;

            //        foreach (var item in OverriddenControls)
            //        {
            //            var finalizedRole = await ParseControlRoleExpressionWithFormsRoles(item.Expression, formRoles);

            //            switch (item.FieldName)
            //            {

            //                case "WorkOrderNumber":
            //                    {
            //                        break;
            //                    }



            //                case "JobNumber":
            //                    {
            //                        break;
            //                    }



            //                case "Description":
            //                    {
            //                        DescriptionIsEnable = ApplyIsEnable(finalizedRole);
            //                        break;
            //                    }


            //                case "RequiredDate":
            //                    {
            //                        RequiredDateIsEnable = ApplyIsEnable(finalizedRole);
            //                        break;
            //                    }



            //                case "WorkStartedDate":
            //                    {
            //                        WorkStartedDateIsEnable = ApplyIsEnable(finalizedRole);
            //                        break;
            //                    }


            //                case "CompletionDate":
            //                    {
            //                        WorkorderCompletionDateIsEnable = ApplyIsEnable(finalizedRole);
            //                        break;
            //                    }

            //                case "AssignedToEmployeeID":
            //                    {
            //                        AssignToEmployeeIsEnable = ApplyIsEnable(finalizedRole);
            //                        break;
            //                    }


            //                case "WorkOrderRequesterID":
            //                    {
            //                        WorkorderRequesterIsEnable = ApplyIsEnable(finalizedRole);
            //                        break;
            //                    }


            //                case "CostCenterID":
            //                    {
            //                        CostCenterIsEnable = ApplyIsEnable(finalizedRole);
            //                        break;
            //                    }


            //                case "PriorityID":
            //                    {
            //                        PriorityIsEnable = ApplyIsEnable(finalizedRole);
            //                        break;
            //                    }


            //                case "ShiftID":
            //                    {
            //                        ShiftIsEnable = ApplyIsEnable(finalizedRole);
            //                        break;
            //                    }


            //                case "WorkOrderStatusID":
            //                    {
            //                        WorkorderStatusIsEnable = ApplyIsEnable(finalizedRole);
            //                        break;
            //                    }


            //                case "WorkTypeID":
            //                    {
            //                        WorkorderTypeIsEnable = ApplyIsEnable(finalizedRole);
            //                        break;
            //                    }


            //                case "UserField22":
            //                    {
            //                        CauseIsEnable = ApplyIsEnable(finalizedRole);
            //                        break;
            //                    }


            //                case "MaintenanceCodeID":
            //                    {
            //                        MaintenanceCodeIsEnable = ApplyIsEnable(finalizedRole);
            //                        break;
            //                    }


            //                case "EstimatedDowntime":
            //                    {
            //                        EstimstedDowntimeIsEnable = ApplyIsEnable(finalizedRole);
            //                        break;
            //                    }

            //                case "ActualDowntime":
            //                    {
            //                        ActualDowntimeIsEnable = ApplyIsEnable(finalizedRole);
            //                        break;
            //                    }

            //                case "MiscellaneousLaborCostID":
            //                    {
            //                        MiscellaneousLabourCostIsEnable = ApplyIsEnable(finalizedRole);
            //                        break;
            //                    }

            //                case "MiscellaneousMaterialsCostID":
            //                    {
            //                        MiscellaneousMaterialCostIsEnable = ApplyIsEnable(finalizedRole);
            //                        break;
            //                    }

            //            }

            //        }

            //    }
            //    catch (Exception ex)
            //    {


            //    }

            //}

            //#endregion

            //#region Generate and Bind Dyanmic controls
            //if (titles != null && titles.CFLI != null && titles.CFLI.Count > 0)
            //{

            //    #region Test 3



            //    Grid contentGrid = await GetContentGrid();
            //    contentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            //    //contentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            //    //contentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });



            //    int rowCount = 0;
            //    int columnCount = 0;
            //    bool isItemAddedInFirstColumn = false;
            //    bool isItemAddedInSecondColumn = false;
            //    var formRoles = titles.lstRoles;
            //    foreach (var item in titles.CFLI)
            //    {
            //        var finalizedRole = await ParseControlRoleExpressionWithFormsRoles(item.Expression, formRoles);

            //        switch (item.DisplayFormat)
            //        {

            //            case "ComboBox":
            //                if (!isItemAddedInFirstColumn)
            //                {
            //                    if (!isItemAddedInSecondColumn)
            //                    {
            //                        GenerateComboBoxLayout(finalizedRole, item, contentGrid, rowCount, columnCount);

            //                        //increment column
            //                        isItemAddedInFirstColumn = true;
            //                        isItemAddedInSecondColumn = false;
            //                        columnCount = 1;
            //                    }
            //                    else
            //                    {
            //                        //generate new row
            //                        contentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            //                        rowCount++;
            //                        GenerateComboBoxLayout(finalizedRole, item, contentGrid, rowCount, columnCount);

            //                        columnCount = 1;
            //                        isItemAddedInFirstColumn = true;
            //                        isItemAddedInSecondColumn = false;

            //                    }
            //                }

            //                else
            //                {
            //                    GenerateComboBoxLayout(finalizedRole, item, contentGrid, rowCount, columnCount);

            //                    //increment column
            //                    isItemAddedInFirstColumn = false;
            //                    isItemAddedInSecondColumn = true;
            //                    columnCount = 0;
            //                }


            //                break;

            //            case "DateTime":
            //                if (!isItemAddedInFirstColumn)
            //                {
            //                    if (!isItemAddedInSecondColumn)
            //                    {
            //                        GenerateDateTimeLayout(finalizedRole, item, contentGrid, rowCount, columnCount);

            //                        //increment column
            //                        isItemAddedInFirstColumn = true;
            //                        isItemAddedInSecondColumn = false;
            //                        columnCount = 1;
            //                    }
            //                    else
            //                    {
            //                        //generate new row
            //                        contentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            //                        rowCount++;
            //                        GenerateDateTimeLayout(finalizedRole, item, contentGrid, rowCount, columnCount);

            //                        columnCount = 1;
            //                        isItemAddedInFirstColumn = true;
            //                        isItemAddedInSecondColumn = false;

            //                    }
            //                }

            //                else
            //                {
            //                    GenerateDateTimeLayout(finalizedRole, item, contentGrid, rowCount, columnCount);

            //                    //increment column
            //                    isItemAddedInFirstColumn = false;
            //                    isItemAddedInSecondColumn = true;
            //                    columnCount = 0;
            //                }




            //                break;

            //            case "TextBox":
            //                if (!isItemAddedInFirstColumn)
            //                {
            //                    if (!isItemAddedInSecondColumn)
            //                    {
            //                        GenerateTextBoxLayout(finalizedRole, item, contentGrid, rowCount, columnCount);

            //                        //increment column
            //                        isItemAddedInFirstColumn = true;
            //                        isItemAddedInSecondColumn = false;
            //                        columnCount = 1;
            //                    }
            //                    else
            //                    {
            //                        //generate new row
            //                        contentGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            //                        rowCount++;
            //                        GenerateTextBoxLayout(finalizedRole, item, contentGrid, rowCount, columnCount);

            //                        columnCount = 1;
            //                        isItemAddedInFirstColumn = true;
            //                        isItemAddedInSecondColumn = false;

            //                    }
            //                }

            //                else
            //                {
            //                    GenerateTextBoxLayout(finalizedRole, item, contentGrid, rowCount, columnCount);

            //                    //increment column
            //                    isItemAddedInFirstColumn = false;
            //                    isItemAddedInSecondColumn = true;
            //                    columnCount = 0;
            //                }


            //                break;


            //        }


            //    }



            //    #endregion







            //}
            //#endregion



            #endregion
        }

        public async Task SetControlsPropertiesForPage()
        {


            var workorderWrapper = await _workorderService.GetWorkorderByWorkorderID(UserID, WorkorderID.ToString());
            if (workorderWrapper != null && workorderWrapper.workOrderWrapper != null && workorderWrapper.workOrderWrapper.workOrder != null)
            {
                bool fdasignatureKey = AppSettings.User.blackhawkLicValidator.IsFDASignatureValidation;



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



                this.DescriptionText = workorder.Description;


                this.DescriptionText = workorder.Description;


                if (!string.IsNullOrWhiteSpace(workorder.AdditionalDetails))
                {

                    this.AdditionalDetailsText = RemoveHTML.StripHTML(workorder.AdditionalDetails);


                }


                this.ApprovalLevel = workorder.ApprovalLevel;
                this.ApprovalNumber = workorder.ApprovalNumber;


                this.RequiredDate1 = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorder.RequiredDate ?? DateTime.Now).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);


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
                //if (AssetID != null)
                //{
                //    var AssetWrapper = await _assetService.GetAssetsBYAssetID(this.AssetID.ToString());


                //    if (string.IsNullOrWhiteSpace(AssetWrapper.assetWrapper.asset.CurrentRuntime.ToString()))
                //    {
                //        this.CurrentRuntimeText = "0.0";


                //    }
                //    else
                //    {
                //        this.CurrentRuntimeText = AssetWrapper.assetWrapper.asset.CurrentRuntime.ToString();

                //    }
                //}
                //else
                //{
                //    CurrentRuntimeIsVisible = false;
                //}

                if (!string.IsNullOrEmpty(workorder.AssetSystemName))
                {
                    AssetSystemName = ShortString.shorten(workorder.AssetSystemName);
                    //ShowAssociatedAssets = true;
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


                EstimstedDowntimeText = string.Format(StringFormat.NumericZero(), string.IsNullOrWhiteSpace(workorder.EstimatedDowntime) ? 0 : decimal.Parse(workorder.EstimatedDowntime));
                ActualDowntimeText = string.Format(StringFormat.NumericZero(), string.IsNullOrWhiteSpace(workorder.ActualDowntime) ? 0 : decimal.Parse(workorder.ActualDowntime));
                MiscellaneousLabourCostText = string.Format(StringFormat.CurrencyZero(), workorder.MiscellaneousLaborCost == null ? 0 : workorder.MiscellaneousLaborCost);
                MiscellaneousMaterialCostText = string.Format(StringFormat.CurrencyZero(), workorder.MiscellaneousMaterialsCost == null ? 0 : workorder.MiscellaneousMaterialsCost);


                ///Set Dyanmic Field Properties
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


                }







                #endregion







                #endregion



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
            if (formControl.IsRequired ?? true)
            {
                var title = new Label();
                var control = new BorderedEntry();
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
            else
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
        }

        private void GenerateComboBoxLayout(FormControl formControl, Grid contentGrid, int row, int column)
        {
            if (formControl.IsRequired ?? true)
            {
                var title = new Label();
                var control = new CustomValidationPicker();

                control.Image = "unnamed";
                control.HeightRequest = 45;

                Frame frame = new Frame { BorderColor = Color.Orange, CornerRadius = 10, HasShadow = true, Content = control };

                title.Text = formControl.TargetName;// + "<<>>" + formControl.ControlName;
                if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
                {
                    title.FontAttributes = FontAttributes.Bold;
                }
                title.TextColor = Color.Black;


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
            else
            {
                var title = new Label();
                var control = new CustomPicker();

                control.Image = "unnamed";
                control.HeightRequest = 45;

                Frame frame = new Frame { BorderColor = Color.Orange, CornerRadius = 10, HasShadow = true, Content = control };

                title.Text = formControl.TargetName;// + "<<>>" + formControl.ControlName;
                if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
                {
                    title.FontAttributes = FontAttributes.Bold;
                }
                title.TextColor = Color.Black;


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


        }

        private void GenerateDateTimeLayout(FormControl formControl, Grid contentGrid, int row, int column)
        {
            var title = new Label();

            View control;
            if (formControl.IsRequired ?? false)
            {
                control = new DatePicker();
            }
            else
            {
                control = new CustomDatePicker();
            }
            if (Device.RuntimePlatform == Device.iOS || Device.RuntimePlatform == Device.Android)
            {
                title.FontAttributes = FontAttributes.Bold;
            }
            title.TextColor = Color.Black;
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
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.ActivationDateText));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;
                            x.IsEnabled = false;
                            x.InputTransparent = true;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(LottoUrl)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                ActivationDateText = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.LottoUrl));

                            control.IsEnabled = false;
                            control.BackgroundColor = Color.FromHex("#D0D3D4");
                            control.InputTransparent = true;
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            LottoUrl = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.LottoUrl), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.LottoUrl), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

                case "ActivationDate":
                    {
                        if (control is Picker)
                        {
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.ActivationDateText));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;
                            x.IsEnabled = false;
                            x.InputTransparent = true;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(ActivationDateText)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                ActivationDateText = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.ActivationDateText));
                            control.IsEnabled = false;
                            control.InputTransparent = true;
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            ActivationDateText = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.ActivationDateText), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.ActivationDateText), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
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

                case "EstimatedDowntime":
                    {
                        //if (control is Picker)
                        //{
                        //    var x = control as Picker;
                        //    control.SetBinding(Picker.SelectedItemProperty, nameof(this.EstimatedDowntime));
                        //}

                        //else if (control is Entry)
                        //{
                        //    control.SetBinding(Entry.TextProperty, nameof(this.EstimatedDowntime));
                        //}

                        //else if (control is DatePicker)
                        //{
                        //    // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                        //    EstimatedDowntime = DateTime.Now.ToString();
                        //    control.SetBinding(DatePicker.DateProperty, nameof(this.EstimatedDowntime));
                        //}

                        //else if (control is CustomDatePicker)
                        //{
                        //    control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.EstimatedDowntime));
                        //}
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

                case "RequestedDate":
                    {
                        if (control is Picker)
                        {
                            //var x = control as Picker;
                            //control.SetBinding(Picker.SelectedItemProperty, nameof(this.RequestedDate));

                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;

                            var source = x.ItemsSource as List<ComboDD>;
                            ComboDD item = null;
                            try { item = source.FirstOrDefault(s => s.SelectedValue == Int32.Parse(RequestedDate)); }
                            catch (Exception) { }

                            if (item != null)
                            {
                                x.SelectedItem = item;
                                RequestedDate = item.SelectedValue.ToString();
                            }

                            x.SelectedIndexChanged += Picker_SelectedIndexChanged;

                        }

                        else if (control is Entry)
                        {
                            control.SetBinding(Entry.TextProperty, nameof(this.RequestedDate));
                        }

                        else if (control is DatePicker)
                        {
                            // because DatePicker Doesn't bind with blank or null.then initialize it with current date.
                            RequestedDate = DateTime.Now.ToString();
                            control.SetBinding(DatePicker.DateProperty, nameof(this.RequestedDate), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.RequestedDate), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
                        }
                        break;

                    }

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
                            control.SetBinding(Entry.TextProperty, nameof(this.RequesterPhone));
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
                            x.IsEnabled = false;
                            x.InputTransparent = true;
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
                            x.SelectedIndex = -1;

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
                            x.SelectedIndex = -1;
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
                            x.SelectedIndex = -1;
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
                            x.SelectedIndex = -1;
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
                            x.SelectedIndex = -1;
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
                            x.SelectedIndex = -1;
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
                            x.ClassId = formControl.ControlName;
                            x.SelectedIndex = -1;
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

                        else if (control is CustomDatePicker)
                        {
                            control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField21), mode: BindingMode.TwoWay, converter: new StringToDateTimeConverter());
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

                        //else if (control is CustomDatePicker)
                        //{
                        //    control.SetBinding(CustomDatePicker.SelectedDateProperty, nameof(this.UserField22));
                        //}
                        //break;

                        #endregion




                        if (control is Picker)
                        {
                            var x = control as Picker;
                            x.ClassId = formControl.ControlName;
                            x.SelectedIndex = -1;
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
                            x.SelectedIndex = -1;
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

                case "ActivationDate":
                    {
                        this.ActivationDateText = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;
                    }

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

                case "RequestedDate":
                    {
                        this.RequestedDate = (picker.SelectedItem as ComboDD).SelectedValue.ToString();
                        break;

                    }

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

                // OperationInProgress = false;

            }

            finally
            {
                UserDialogs.Instance.HideLoading();

                //OperationInProgress = false;

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
                // OperationInProgress = false;

            }
        }

        public async Task ShowAssetSystem()
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

                // OperationInProgress = true;
                IsPickerDataRequested = true;
                await NavigationService.NavigateToAsync<AssignToListSelectionPageViewModel>(new TargetNavigationData() { }); //Pass the control here
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

                //  OperationInProgress = true;
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

                //  OperationInProgress = true;
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
                //  OperationInProgress = false;

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

                // OperationInProgress = true;
                IsPickerDataRequested = true;
                await NavigationService.NavigateToAsync<WorkorderStatusListSelectionPageViewModel>(new TargetNavigationData() { }); //Pass the control here
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

                OperationInProgress = false;

            }

            finally
            {
                UserDialogs.Instance.HideLoading();

                OperationInProgress = false;

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
                    var AssetWrapper = await _assetService.GetAssetsBYAssetID(this.AssetID.ToString());

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

                // if Asset is selected reset the   Asset System

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


        public async Task CreateWorkorder()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                var validationResultForOverriddenControls = await ValidateOverriddenControlsIsRequired(OverriddenControlsNew);
                if (validationResultForOverriddenControls.FailedItem != null)
                {
                    UserDialogs.Instance.HideLoading();
                    DialogService.ShowToast(validationResultForOverriddenControls.ErrorMessage);
                    return;
                }
                var validationResult = await ValidateControlsIsRequired(WorkorderControlsNew);
                if (validationResult.FailedItem != null)
                {
                    UserDialogs.Instance.HideLoading();
                    DialogService.ShowToast(validationResult.ErrorMessage);
                    return;
                }
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




                #endregion


                ///TODO:ActualDowntime1 , MLC1 , MMC1 , EstimatedDowntime1 parse into decimal value.

                /// Create WO wrapper and data into it and update the workorder


                var workOrder = new workOrders();

                #region workOrder properties initialzation

                workOrder.ModifiedUserName = AppSettings.User.UserName;
                if (string.IsNullOrWhiteSpace(CurrentRuntimeText))
                {
                    this.CurrentRuntimeText = "0.00";


                }
                workOrder.CurrentRuntime = CurrentRuntimeText;
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
                workOrder.Originator = OriginatorName;
                workOrder.AdditionalDetails = AdditionalDetailsText;
                workOrder.InternalNote = InternalNoteText;


                if (string.IsNullOrWhiteSpace(ActualDowntimeText))
                {
                    ActualDowntimeText = "0";
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


                workOrder.DistributeCost = IsCostDistributed;
                workOrder.ParentandChildCost = ParentCostDistributed;
                workOrder.ChildCost = ChildCostDistributed;
                workOrder.ActualDowntime = ActualDowntimeText;
                workOrder.EstimatedDowntime = EstimstedDowntimeText;
                workOrder.MiscellaneousLaborCost = decimal.Parse(MiscellaneousLabourCostText);
                workOrder.MiscellaneousMaterialsCost = decimal.Parse(MiscellaneousMaterialCostText);


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




                bool fdasignatureKey = AppSettings.User.blackhawkLicValidator.IsFDASignatureValidation;

                if (fdasignatureKey == true)
                {
                    if (AppSettings.User.RequireSignaturesForValidation == "True")
                    {
                        workOrder.IsSignatureValidated = true;
                        Application.Current.Properties["CauseID"] = this.CauseID;
                        Application.Current.Properties["CauseJson"] = this.Cause;
                        Application.Current.Properties["WorkorderWrapper"] = workOrder;
                        UserName = AppSettings.UserName;
                        var page = new CreateWorkorderSignaturePage();
                        await PopupNavigation.PushAsync(page);

                    }
                    else
                    {
                        workOrder.IsSignatureValidated = false;
                        if (CauseID == null)
                        {
                            var workorder = new workOrderWrapper
                            {
                                TimeZone = AppSettings.UserTimeZone,
                                CultureName = AppSettings.UserCultureName,
                                UserId = Convert.ToInt32(AppSettings.User.UserID),
                                ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                                DistributeCost = IsCostDistributed,
                                workOrder = workOrder,
                                cause = null,


                            };
                            var response = await _workorderService.CreateWorkorder(workorder);
                            if (response != null && bool.Parse(response.servicestatus.ToString()))
                            {
                                if (this.WorkorderID > 0)
                                {
                                    #region Create Default Task

                                    ServiceOutput assignto = await _workorderService.GetEmployeeAssignTo(UserID);
                                    if (assignto.EmployeeLaborCraftID == 0 || string.IsNullOrWhiteSpace(assignto.EmployeeLaborCraftID.ToString()))
                                    {


                                    }

                                    var yourobject1 = new workOrderWrapper
                                    {
                                        TimeZone = "UTC",
                                        CultureName = "en-US",
                                        ClientIANATimeZone = NodaTime.DateTimeZoneProviders.Serialization.GetSystemDefault().ToString(),
                                        workOrderLabor = new WorkOrderLabor
                                        {

                                            WorkOrderID = this.WorkorderID,
                                            TaskID = null,
                                            StartDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).Date,
                                            EmployeeLaborCraftID = assignto.EmployeeLaborCraftID


                                        },

                                    };

                                    var status = await _taskAndLabourService.CreateWorkOrderLabor(yourobject1);
                                    if (Boolean.Parse(status.servicestatus))
                                    {

                                        UserDialogs.Instance.HideLoading();

                                    }
                                    #endregion
                                }

                                UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("Workordersuccessfullycreated"));
                                await NavigationService.NavigateToAsync<WorkorderTabbedPageViewModel>(response.workOrderWrapper.workOrder);
                                await NavigationService.RemoveLastFromBackStackAsync();


                            }
                        }
                        else
                        {

                            var workorder = new workOrderWrapper
                            {
                                TimeZone = AppSettings.UserTimeZone,
                                CultureName = AppSettings.UserCultureName,
                                UserId = Convert.ToInt32(AppSettings.User.UserID),
                                ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                                DistributeCost = IsCostDistributed,
                                workOrder = workOrder,
                                cause = this.Cause,


                            };
                            var response = await _workorderService.CreateWorkorder(workorder);
                            if (response != null && bool.Parse(response.servicestatus.ToString()))
                            {
                                if (this.WorkorderID > 0)
                                {
                                    #region Create Default Task

                                    ServiceOutput assignto = await _workorderService.GetEmployeeAssignTo(UserID);
                                    if (assignto.EmployeeLaborCraftID == 0 || string.IsNullOrWhiteSpace(assignto.EmployeeLaborCraftID.ToString()))
                                    {


                                    }

                                    var yourobject1 = new workOrderWrapper
                                    {
                                        TimeZone = "UTC",
                                        CultureName = "en-US",
                                        ClientIANATimeZone = NodaTime.DateTimeZoneProviders.Serialization.GetSystemDefault().ToString(),
                                        workOrderLabor = new WorkOrderLabor
                                        {

                                            WorkOrderID = this.WorkorderID,
                                            TaskID = null,
                                            StartDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).Date,
                                            EmployeeLaborCraftID = assignto.EmployeeLaborCraftID


                                        },

                                    };

                                    var status = await _taskAndLabourService.CreateWorkOrderLabor(yourobject1);
                                    if (Boolean.Parse(status.servicestatus))
                                    {

                                        UserDialogs.Instance.HideLoading();

                                    }
                                    #endregion
                                }
                                UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("Workordersuccessfullycreated"));
                                await NavigationService.NavigateToAsync<WorkorderTabbedPageViewModel>(response.workOrderWrapper.workOrder);
                                await NavigationService.RemoveLastFromBackStackAsync();

                            }
                        }
                    }
                }

                else
                {
                    workOrder.IsSignatureValidated = false;
                    if (CauseID == null)
                    {
                        var workorder = new workOrderWrapper
                        {
                            TimeZone = AppSettings.UserTimeZone,
                            CultureName = AppSettings.UserCultureName,
                            UserId = Convert.ToInt32(AppSettings.User.UserID),
                            ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                            DistributeCost = IsCostDistributed,
                            workOrder = workOrder,
                            cause = null,


                        };
                        var response = await _workorderService.CreateWorkorder(workorder);
                        if (response != null && bool.Parse(response.servicestatus.ToString()))
                        {
                            if (this.WorkorderID > 0)
                            {
                                #region Create Default Task

                                ServiceOutput assignto = await _workorderService.GetEmployeeAssignTo(UserID);
                                if (assignto.EmployeeLaborCraftID == 0 || string.IsNullOrWhiteSpace(assignto.EmployeeLaborCraftID.ToString()))
                                {


                                }

                                var yourobject1 = new workOrderWrapper
                                {
                                    TimeZone = "UTC",
                                    CultureName = "en-US",
                                    ClientIANATimeZone = NodaTime.DateTimeZoneProviders.Serialization.GetSystemDefault().ToString(),
                                    workOrderLabor = new WorkOrderLabor
                                    {

                                        WorkOrderID = this.WorkorderID,
                                        TaskID = null,
                                        StartDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).Date,
                                        EmployeeLaborCraftID = assignto.EmployeeLaborCraftID


                                    },

                                };

                                var status = await _taskAndLabourService.CreateWorkOrderLabor(yourobject1);
                                if (Boolean.Parse(status.servicestatus))
                                {

                                    UserDialogs.Instance.HideLoading();

                                }
                                #endregion
                            }
                            UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("Workordersuccessfullycreated"));
                            await NavigationService.NavigateToAsync<WorkorderTabbedPageViewModel>(response.workOrderWrapper.workOrder);
                            await NavigationService.RemoveLastFromBackStackAsync();


                        }
                    }
                    else
                    {

                        var workorder = new workOrderWrapper
                        {
                            TimeZone = AppSettings.UserTimeZone,
                            CultureName = AppSettings.UserCultureName,
                            UserId = Convert.ToInt32(AppSettings.User.UserID),
                            ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                            DistributeCost = IsCostDistributed,
                            workOrder = workOrder,
                            cause = this.Cause,


                        };
                        var response = await _workorderService.CreateWorkorder(workorder);
                        if (response != null && bool.Parse(response.servicestatus.ToString()))
                        {
                            if (this.WorkorderID > 0)
                            {
                                #region Create Default Task

                                ServiceOutput assignto = await _workorderService.GetEmployeeAssignTo(UserID);
                                if (assignto.EmployeeLaborCraftID == 0 || string.IsNullOrWhiteSpace(assignto.EmployeeLaborCraftID.ToString()))
                                {


                                }

                                var yourobject1 = new workOrderWrapper
                                {
                                    TimeZone = "UTC",
                                    CultureName = "en-US",
                                    ClientIANATimeZone = NodaTime.DateTimeZoneProviders.Serialization.GetSystemDefault().ToString(),
                                    workOrderLabor = new WorkOrderLabor
                                    {

                                        WorkOrderID = this.WorkorderID,
                                        TaskID = null,
                                        StartDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).Date,
                                        EmployeeLaborCraftID = assignto.EmployeeLaborCraftID


                                    },

                                };

                                var status = await _taskAndLabourService.CreateWorkOrderLabor(yourobject1);
                                if (Boolean.Parse(status.servicestatus))
                                {

                                    UserDialogs.Instance.HideLoading();

                                }
                                #endregion
                            }
                            UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("Workordersuccessfullycreated"));
                            await NavigationService.NavigateToAsync<WorkorderTabbedPageViewModel>(response.workOrderWrapper.workOrder);
                            await NavigationService.RemoveLastFromBackStackAsync();

                        }
                    }
                }
                #endregion
                UserDialogs.Instance.HideLoading();

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

        public async Task GetWorkorderControlRights()
        {
            try
            {
                ServiceOutput FormControlsAndRightsForDetails = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "workorders", "Details");
                ServiceOutput FormControlsAndRightsForTask = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "workorders", "Tasks");
                ServiceOutput FormControlsAndRightsForInspection = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "workorders", "Inspections");
                ServiceOutput FormControlsAndRightsForTools = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "workorders", "Tools");
                ServiceOutput FormControlsAndRightsForParts = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "workorders", "Parts");
                ServiceOutput FormControlsAndRightsForAttachments = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "workorders", "Attachments");
                if (FormControlsAndRightsForDetails != null && FormControlsAndRightsForDetails.lstModules != null && FormControlsAndRightsForDetails.lstModules.Count > 0)
                {
                    var WorkOrderModule = FormControlsAndRightsForDetails.lstModules[0];
                    if (WorkOrderModule.ModuleName == "Details") //ModuleName can't be  changed in service 
                    {
                        if (WorkOrderModule.lstSubModules != null && WorkOrderModule.lstSubModules.Count > 0)
                        {
                            var WorkOrderSubModule = WorkOrderModule.lstSubModules[0];
                            if (WorkOrderSubModule.listControls != null && WorkOrderSubModule.listControls.Count > 0)
                            {
                                try
                                {
                                    Application.Current.Properties["CreateWorkorderRights"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "New").Expression;
                                    Application.Current.Properties["EditRights"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "Edit").Expression;
                                    Application.Current.Properties["CloseWorkorderRightsKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "CompleteAndClose").Expression;

                                    ///Set workOrderListing Page Rights
                                    Application.Current.Properties["WorkOrderStartedDateKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "WorkStartedDate").Expression;
                                    Application.Current.Properties["WorkOrderCompletionDateKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "CompletionDate").Expression;
                                    Application.Current.Properties["WorkOrderRequestedDateKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "RequestedDate").Expression;
                                    Application.Current.Properties["WorkOrderTypeKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "WorkTypeID").Expression;
                                    Application.Current.Properties["DescriptionKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "Description").Expression;
                                    Application.Current.Properties["PriorityKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "PriorityID").Expression;

                                    ///Set workOrderEdit Page Rights


                                    Application.Current.Properties["WorkorderAdditionalDetailsKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "AdditionalDetails").Expression;
                                    Application.Current.Properties["WorkOrderInternalNoteKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "InternalNote").Expression;
                                    Application.Current.Properties["WorkorderCauseKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "Causes").Expression;
                                    Application.Current.Properties["WorkorderTargetKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "AssetID").Expression;
                                    Application.Current.Properties["WorkorderDetailsControls"] = WorkOrderSubModule;
                                    Application.Current.Properties["DistributeCost"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "DistributeCost").Expression;

                                }
                                catch (Exception ex)
                                {


                                }



                            }



                        }
                    }
                }
                if (FormControlsAndRightsForTask != null && FormControlsAndRightsForTask.lstModules != null && FormControlsAndRightsForTask.lstModules.Count > 0)
                {
                    var WorkOrderTaskModule = FormControlsAndRightsForTask.lstModules[0];
                    if (WorkOrderTaskModule.ModuleName == "TasksandLabor") //ModuleName can't be  changed in service 
                    {
                        if (WorkOrderTaskModule.lstSubModules != null && WorkOrderTaskModule.lstSubModules.Count > 0)
                        {
                            var WorkOrderTaskSubModule = WorkOrderTaskModule.lstSubModules[0];
                            if (WorkOrderTaskSubModule.listControls != null && WorkOrderTaskSubModule.listControls.Count > 0)
                            {

                                try
                                {
                                    Application.Current.Properties["TaskandLabourTabKey"] = WorkOrderTaskModule.Expression;
                                    Application.Current.Properties["CreateTask"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "Add").Expression;
                                    Application.Current.Properties["LabourEstimatedHours"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "EstimatedHours").Expression;
                                    Application.Current.Properties["WOLabourTime"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "WorkOrderLaborTime").Expression;
                                    Application.Current.Properties["TaskTabDetails"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "TaskID").Expression;
                                    Application.Current.Properties["HourAtRate1"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "HoursAtRate1").Expression;
                                    Application.Current.Properties["EmployeeTab"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "EmployeeLaborCraftID").Expression;
                                    Application.Current.Properties["ContractorTab"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "ContractorLaborCraftID").Expression;
                                    Application.Current.Properties["StartdateTab"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "StartDate").Expression;
                                    Application.Current.Properties["CompletionDateTab"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "CompletionDate").Expression;

                                }
                                catch (Exception ex)
                                {


                                }



                            }



                        }
                    }
                }
                if (FormControlsAndRightsForInspection != null && FormControlsAndRightsForInspection.lstModules != null && FormControlsAndRightsForInspection.lstModules.Count > 0)
                {
                    var WorkOrderInspectionModule = FormControlsAndRightsForInspection.lstModules[0];
                    if (WorkOrderInspectionModule.ModuleName == "WorkOrderInspections") //ModuleName can't be  changed in service 
                    {

                        Application.Current.Properties["InspectionTabKey"] = WorkOrderInspectionModule.Expression;

                    }
                    if (WorkOrderInspectionModule.lstSubModules != null && WorkOrderInspectionModule.lstSubModules.Count > 0)
                    {
                        var WorkOrderInspectionSubModule = WorkOrderInspectionModule.lstSubModules[0];
                        if (WorkOrderInspectionSubModule.listControls != null && WorkOrderInspectionSubModule.listControls.Count > 0)
                        {

                            try
                            {
                                Application.Current.Properties["AssociateEmployeeContr"] = WorkOrderInspectionSubModule.listControls.FirstOrDefault(i => i.ControlName == "AssociateEmployeeContr").Expression;
                                Application.Current.Properties["AssociateInspection"] = WorkOrderInspectionSubModule.listControls.FirstOrDefault(i => i.ControlName == "AssociateInspection").Expression;
                            }
                            catch (Exception ex)
                            {


                            }
                        }
                    }
                }
                if (FormControlsAndRightsForTools != null && FormControlsAndRightsForTools.lstModules != null && FormControlsAndRightsForTools.lstModules.Count > 0)
                {
                    var WorkOrderToolsModule = FormControlsAndRightsForTools.lstModules[0];
                    if (WorkOrderToolsModule.ModuleName == "Tools") //ModuleName can't be  changed in service 
                    {

                        Application.Current.Properties["ToolsTabKey"] = WorkOrderToolsModule.Expression;
                        var WorkOrderToolsSubModule = WorkOrderToolsModule.lstSubModules[0];
                        if (WorkOrderToolsSubModule.listControls != null && WorkOrderToolsSubModule.listControls.Count > 0)
                        {

                            try
                            {
                                Application.Current.Properties["CreateTool"] = WorkOrderToolsSubModule.listControls.FirstOrDefault(i => i.ControlName == "Add").Expression;
                                Application.Current.Properties["DeleteTool"] = WorkOrderToolsSubModule.listControls.FirstOrDefault(i => i.ControlName == "Remove").Expression;

                            }
                            catch (Exception ex)
                            {


                            }

                        }
                    }
                }
                if (FormControlsAndRightsForParts != null && FormControlsAndRightsForParts.lstModules != null && FormControlsAndRightsForParts.lstModules.Count > 0)
                {
                    var WorkOrderPartsModule = FormControlsAndRightsForParts.lstModules[0];
                    if (WorkOrderPartsModule.ModuleName == "Parts") //ModuleName can't be  changed in service 
                    {

                        Application.Current.Properties["PartsTabKey"] = WorkOrderPartsModule.Expression;
                        var WorkOrderPartsSubModule = WorkOrderPartsModule.lstSubModules[0];
                        if (WorkOrderPartsSubModule.listControls != null && WorkOrderPartsSubModule.listControls.Count > 0)
                        {

                            try
                            {
                                Application.Current.Properties["AddParts"] = WorkOrderPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "Add").Expression;
                                Application.Current.Properties["EditParts"] = WorkOrderPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "Edit").Expression;
                                Application.Current.Properties["RemoveParts"] = WorkOrderPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "Remove").Expression;

                            }
                            catch (Exception ex)
                            {


                            }

                        }
                    }
                }
                if (FormControlsAndRightsForAttachments != null && FormControlsAndRightsForAttachments.lstModules != null && FormControlsAndRightsForAttachments.lstModules.Count > 0)
                {
                    var WorkOrderAttachmentModule = FormControlsAndRightsForAttachments.lstModules[0];
                    if (WorkOrderAttachmentModule.ModuleName == "Attachments") //ModuleName can't be  changed in service 
                    {

                        Application.Current.Properties["AttachmentTabKey"] = WorkOrderAttachmentModule.Expression;
                        var WorkOrderAttachmentSubModule = WorkOrderAttachmentModule.lstSubModules[0];
                        if (WorkOrderAttachmentSubModule.listControls != null && WorkOrderAttachmentSubModule.listControls.Count > 0)
                        {
                            try
                            {
                                Application.Current.Properties["CreateAttachment"] = WorkOrderAttachmentSubModule.listControls.FirstOrDefault(i => i.ControlName == "Add").Expression;
                                Application.Current.Properties["DeleteAttachments"] = WorkOrderAttachmentSubModule.listControls.FirstOrDefault(i => i.ControlName == "Remove").Expression;

                            }
                            catch (Exception ex)
                            {


                            }


                        }
                    }
                }
            }
            catch (Exception)
            {


            }

        }

        #region ***** Old Code ****
        //public async Task GetWorkorderControlRights()
        //{
        //    try
        //    {
        //        ServiceOutput FormControlsAndRightsForDetails = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "workorders", "Details");
        //        ServiceOutput FormControlsAndRightsForTask = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "workorders", "Tasks");
        //        ServiceOutput FormControlsAndRightsForInspection = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "workorders", "Inspections");
        //        ServiceOutput FormControlsAndRightsForTools = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "workorders", "Tools");
        //        ServiceOutput FormControlsAndRightsForParts = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "workorders", "Parts");
        //        ServiceOutput FormControlsAndRightsForAttachments = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "workorders", "Attachments");
        //        if (FormControlsAndRightsForDetails != null && FormControlsAndRightsForDetails.lstModules != null && FormControlsAndRightsForDetails.lstModules.Count > 0)
        //        {
        //            var WorkOrderModule = FormControlsAndRightsForDetails.lstModules[0];
        //            if (WorkOrderModule.ModuleName == "Details") //ModuleName can't be  changed in service 
        //            {
        //                if (WorkOrderModule.lstSubModules != null && WorkOrderModule.lstSubModules.Count > 0)
        //                {
        //                    var WorkOrderSubModule = WorkOrderModule.lstSubModules[0];
        //                    if (WorkOrderSubModule.listControls != null && WorkOrderSubModule.listControls.Count > 0)
        //                    {
        //                        try
        //                        {
        //                            Application.Current.Properties["CreateWorkorderRights"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "New").Expression;
        //                            Application.Current.Properties["EditRights"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "Edit").Expression;
        //                            Application.Current.Properties["CloseWorkorderRightsKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "CompleteAndClose").Expression;

        //                            ///Set workOrderListing Page Rights
        //                            Application.Current.Properties["WorkOrderStartedDateKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "WorkStartedDate").Expression;
        //                            Application.Current.Properties["WorkOrderCompletionDateKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "CompletionDate").Expression;
        //                            Application.Current.Properties["WorkOrderRequestedDateKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "RequestedDate").Expression;
        //                            Application.Current.Properties["WorkOrderTypeKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "WorkTypeID").Expression;
        //                            Application.Current.Properties["DescriptionKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "Description").Expression;
        //                            Application.Current.Properties["PriorityKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "PriorityID").Expression;

        //                            ///Set workOrderEdit Page Rights


        //                            Application.Current.Properties["WorkorderAdditionalDetailsKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "AdditionalDetails").Expression;
        //                            Application.Current.Properties["WorkOrderInternalNoteKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "InternalNote").Expression;
        //                            Application.Current.Properties["WorkorderCauseKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "Causes").Expression;
        //                            Application.Current.Properties["WorkorderTargetKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "AssetID").Expression;
        //                            Application.Current.Properties["WorkorderDetailsControls"] = WorkOrderSubModule;
        //                            Application.Current.Properties["DistributeCost"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "DistributeCost").Expression;

        //                        }
        //                        catch (Exception ex)
        //                        {


        //                        }



        //                    }



        //                }
        //            }
        //        }




        //    }
        //    catch (Exception)
        //    {


        //    }

        //} 
        #endregion
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
                        case "ActivationDate":
                            {
                                validationResult = ValidateValidations(formLoadItem, ActivationDateText);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;

                            }

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

                        case "RequestedDate":
                            {
                                validationResult = ValidateValidations(formLoadItem, RequestedDate);
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
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, EstimstedDowntimeText);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "ActualDowntime":
                            {
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, ActualDowntimeText);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "MiscellaneousLaborCostID":
                            {
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, MiscellaneousLabourCostText);
                                if (validationResult.FailedItem != null)
                                {
                                    return validationResult;
                                }
                                break;
                            }

                        case "MiscellaneousMaterialsCostID":
                            {
                                validationResult = ValidateValidationsForOverriddennControls(formLoadItem, MiscellaneousMaterialCostText);
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
        public static bool IsPhoneNumber(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                return true;
            }
            return number.All(char.IsDigit);
        }
        public static bool IsNumber(string s)
        {
            return s.All(char.IsNumber);
        }
        #endregion
    }
}
