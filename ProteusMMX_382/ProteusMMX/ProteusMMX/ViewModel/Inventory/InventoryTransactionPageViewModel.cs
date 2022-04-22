using Acr.UserDialogs;
using ProteusMMX.Constants;
using ProteusMMX.DependencyInterface;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.Attachment;
using ProteusMMX.Helpers.StringFormatter;
using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.InventoryModel;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Inventory;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.SelectionListPagesViewModels;
using ProteusMMX.ViewModel.SelectionListPagesViewModels.Inventory;
using ProteusMMX.ViewModel.SelectionListPagesViewModels.Workorder.Parts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;
namespace ProteusMMX.ViewModel.Inventory
{
    public class InventoryTransactionPageViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        #region Fields
        List<ShelfBin> shelfbin = new List<ShelfBin>();
        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly IInventoryService _inventoryService;

        bool LastPhysicalInventorydate;
        #endregion

        #region Properties
        #region Dialog Actions Titles



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
        #region Page Properties
        bool _Perform_TransactionIsVisible = true;
        public bool Perform_TransactionIsVisible
        {
            get
            {
                return _Perform_TransactionIsVisible;
            }

            set
            {
                if (value != _Perform_TransactionIsVisible)
                {
                    _Perform_TransactionIsVisible = value;
                    OnPropertyChanged(nameof(Perform_TransactionIsVisible));
                }
            }
        }
        bool _Perform_TransactionIsEnabled = true;
        public bool Perform_TransactionIsEnabled
        {
            get
            {
                return _Perform_TransactionIsEnabled;
            }

            set
            {
                if (value != _Perform_TransactionIsEnabled)
                {
                    _Perform_TransactionIsEnabled = value;
                    OnPropertyChanged(nameof(Perform_TransactionIsEnabled));
                }
            }
        }
        bool _physicalDateSwitch = false;
        public bool PhysicalDateSwitch
        {
            get
            {
                return _physicalDateSwitch;
            }

            set
            {
                if (value != _physicalDateSwitch)
                {
                    _physicalDateSwitch = value;
                    OnPropertyChanged("PhysicalDateSwitch");
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

        int? _stockroompartID;
        public int? StockroompartID
        {
            get
            {
                return _stockroompartID;
            }

            set
            {
                if (value != _stockroompartID)
                {
                    _stockroompartID = value;
                    OnPropertyChanged(nameof(StockroompartID));
                }
            }
        }

        int? _stockroomID;
        public int? StockroomID
        {
            get
            {
                return _stockroomID;
            }

            set
            {
                if (value != _stockroomID)
                {
                    _stockroomID = value;
                    OnPropertyChanged(nameof(StockroomID));
                }
            }
        }
        int? _shelfBinID;
        public int? ShelfBinID
        {
            get
            {
                return _shelfBinID;
            }

            set
            {
                if (value != _shelfBinID)
                {
                    _shelfBinID = value;
                    OnPropertyChanged(nameof(ShelfBinID));
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






        #endregion

        #region Title Properties

        string _stockRoomNametitle;
        public string StockRoomNametitle
        {
            get
            {
                return _stockRoomNametitle;
            }

            set
            {
                if (value != _stockRoomNametitle)
                {
                    _stockRoomNametitle = value;
                    OnPropertyChanged("StockRoomNametitle");
                }
            }
        }
        string _shelfBintitle;
        public string ShelfBintitle
        {
            get
            {
                return _shelfBintitle;
            }

            set
            {
                if (value != _shelfBintitle)
                {
                    _shelfBintitle = value;
                    OnPropertyChanged("ShelfBintitle");
                }
            }
        }
        string _performBytitle;
        public string PerformBytitle
        {
            get
            {
                return _performBytitle;
            }

            set
            {
                if (value != _performBytitle)
                {
                    _performBytitle = value;
                    OnPropertyChanged("PerformBytitle");
                }
            }
        }

        string _shelfBinTitle;
        public string ShelfBinTitle
        {
            get
            {
                return _shelfBinTitle;
            }

            set
            {
                if (value != _shelfBinTitle)
                {
                    _shelfBinTitle = value;
                    OnPropertyChanged("ShelfBinTitle");
                }
            }
        }


        string _checkOutTitle;
        public string CheckOutTitle
        {
            get
            {
                return _checkOutTitle;
            }

            set
            {
                if (value != _checkOutTitle)
                {
                    _checkOutTitle = value;
                    OnPropertyChanged("CheckOutTitle");
                }
            }
        }
        string _partNameTitle;
        public string PartNameTitle
        {
            get
            {
                return _partNameTitle;
            }

            set
            {
                if (value != _partNameTitle)
                {
                    _partNameTitle = value;
                    OnPropertyChanged("PartNameTitle");
                }
            }
        }


        string _partNumberTitle;
        public string PartNumberTitle
        {
            get
            {
                return _partNumberTitle;
            }

            set
            {
                if (value != _partNumberTitle)
                {
                    _partNumberTitle = value;
                    OnPropertyChanged("PartNumberTitle");
                }
            }
        }

        string _quantityOnHandTitle;
        public string QuantityOnHandTitle
        {
            get
            {
                return _quantityOnHandTitle;
            }

            set
            {
                if (value != _quantityOnHandTitle)
                {
                    _quantityOnHandTitle = value;
                    OnPropertyChanged("QuantityOnHandTitle");
                }
            }
        }

        string _quantityAllocatedTitle;
        public string QuantityAllocatedTitle
        {
            get
            {
                return _quantityAllocatedTitle;
            }

            set
            {
                if (value != _quantityAllocatedTitle)
                {
                    _quantityAllocatedTitle = value;
                    OnPropertyChanged("QuantityAllocatedTitle");
                }
            }
        }

        string _unitCosttitle;
        public string UnitCosttitle
        {
            get
            {
                return _unitCosttitle;
            }

            set
            {
                if (value != _unitCosttitle)
                {
                    _unitCosttitle = value;
                    OnPropertyChanged("UnitCosttitle");
                }
            }
        }

        string _quantityAllocatedtitle;
        public string QuantityAllocatedtitle
        {
            get
            {
                return _quantityAllocatedtitle;
            }

            set
            {
                if (value != _quantityAllocatedtitle)
                {
                    _quantityAllocatedtitle = value;
                    OnPropertyChanged("QuantityAllocatedtitle");
                }
            }
        }

        string _transactionTypetitle;
        public string TransactionTypetitle
        {
            get
            {
                return _transactionTypetitle;
            }

            set
            {
                if (value != _transactionTypetitle)
                {
                    _transactionTypetitle = value;
                    OnPropertyChanged("TransactionTypetitle");
                }
            }
        }


        string _lastPhysicalInventorytitle;
        public string LastPhysicalInventorytitle
        {
            get
            {
                return _lastPhysicalInventorytitle;
            }

            set
            {
                if (value != _lastPhysicalInventorytitle)
                {
                    _lastPhysicalInventorytitle = value;
                    OnPropertyChanged("LastPhysicalInventorytitle");
                }
            }
        }



        string _adjustmentquantitytitle;
        public string Adjustmentquantitytitle
        {
            get
            {
                return _adjustmentquantitytitle;
            }

            set
            {
                if (value != _adjustmentquantitytitle)
                {
                    _adjustmentquantitytitle = value;
                    OnPropertyChanged("Adjustmentquantitytitle");
                }
            }
        }

        string _transactionReasontitle;
        public string TransactionReasontitle
        {
            get
            {
                return _transactionReasontitle;
            }

            set
            {
                if (value != _transactionReasontitle)
                {
                    _transactionReasontitle = value;
                    OnPropertyChanged("TransactionReasontitle");
                }
            }
        }

        string _costcentertitle;
        public string Costcentertitle
        {
            get
            {
                return _costcentertitle;
            }

            set
            {
                if (value != _costcentertitle)
                {
                    _costcentertitle = value;
                    OnPropertyChanged("Costcentertitle");
                }
            }
        }

        string _descriptiontitle;
        public string Descriptiontitle
        {
            get
            {
                return _descriptiontitle;
            }

            set
            {
                if (value != _descriptiontitle)
                {
                    _descriptiontitle = value;
                    OnPropertyChanged("Descriptiontitle");
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



        #endregion



        #region CreateWorkOrderStockroomParts Properties





        string _serverTimeZone = AppSettings.User.ServerIANATimeZone;
        public string ServerTimeZone
        {
            get { return _serverTimeZone; }
        }

        private Xamarin.Forms.ImageSource _attachmentImageSource;
        public Xamarin.Forms.ImageSource AttachmentImageSource
        {
            get { return _attachmentImageSource; }
            set
            {
                _attachmentImageSource = value;
                OnPropertyChanged("AttachmentImageSource");
            }
        }
        #region Normal Field Properties

        string _partNameText;
        public string PartNameText
        {
            get
            {
                return _partNameText;
            }

            set
            {
                if (value != _partNameText)
                {
                    _partNameText = value;
                    OnPropertyChanged("PartNameText");
                }
            }
        }
        bool _partNameIsVisible = true;
        public bool PartNameIsVisible
        {
            get
            {
                return _partNameIsVisible;
            }

            set
            {
                if (value != _partNameIsVisible)
                {
                    _partNameIsVisible = value;
                    OnPropertyChanged(nameof(PartNameIsVisible));
                }
            }
        }

        string _adjustmentQuantityText;
        public string AdjustmentQuantityText
        {
            get
            {
                return _adjustmentQuantityText;
            }

            set
            {
                if (value != _adjustmentQuantityText)
                {
                    _adjustmentQuantityText = value;
                    OnPropertyChanged("AdjustmentQuantityText");
                }
            }
        }
        bool _adjustmentQuantityIsVisible = true;
        public bool AdjustmentQuantityIsVisible
        {
            get
            {
                return _adjustmentQuantityIsVisible;
            }

            set
            {
                if (value != _adjustmentQuantityIsVisible)
                {
                    _adjustmentQuantityIsVisible = value;
                    OnPropertyChanged(nameof(AdjustmentQuantityIsVisible));
                }
            }
        }
        bool _adjustmentQuantityIsEnabled = true;
        public bool AdjustmentQuantityIsEnabled
        {
            get
            {
                return _adjustmentQuantityIsEnabled;
            }

            set
            {
                if (value != _adjustmentQuantityIsEnabled)
                {
                    _adjustmentQuantityIsEnabled = value;
                    OnPropertyChanged(nameof(AdjustmentQuantityIsEnabled));
                }
            }
        }

        string _stockroomNameText;
        public string StockroomNameText
        {
            get
            {
                return _stockroomNameText;
            }

            set
            {
                if (value != _stockroomNameText)
                {
                    _stockroomNameText = value;
                    OnPropertyChanged(nameof(StockroomNameText));
                }
            }
        }
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
                    OnPropertyChanged("DescriptionText");
                }
            }
        }
        string _shelfBinText;
        public string ShelfBinText
        {
            get
            {
                return _shelfBinText;
            }

            set
            {
                if (value != _shelfBinText)
                {
                    _shelfBinText = value;
                    OnPropertyChanged("ShelfBinText");
                }
            }
        }
        string _performByText;
        public string PerformByText
        {
            get
            {
                return _performByText;
            }

            set
            {
                if (value != _performByText)
                {
                    _performByText = value;
                    OnPropertyChanged("PerformByText");
                }
            }
        }
        string _checkOutText;
        public string CheckOutText
        {
            get
            {
                return _checkOutText;
            }

            set
            {
                if (value != _checkOutText)
                {
                    _checkOutText = value;
                    OnPropertyChanged("CheckOutText");
                }
            }
        }
        bool _checkOutIsVisible = true;
        public bool CheckOutIsVisible
        {
            get
            {
                return _checkOutIsVisible;
            }

            set
            {
                if (value != _checkOutIsVisible)
                {
                    _checkOutIsVisible = value;
                    OnPropertyChanged(nameof(CheckOutIsVisible));
                }
            }
        }
        bool _checkOutIsEnabled = true;
        public bool CheckOutIsEnabled
        {
            get
            {
                return _checkOutIsEnabled;
            }

            set
            {
                if (value != _checkOutIsEnabled)
                {
                    _checkOutIsEnabled = value;
                    OnPropertyChanged(nameof(CheckOutIsEnabled));
                }
            }
        }
        string _partNumberText;
        public string PartNumberText
        {
            get
            {
                return _partNumberText;
            }

            set
            {
                if (value != _partNumberText)
                {
                    _partNumberText = value;
                    OnPropertyChanged(nameof(PartNumberText));
                }
            }
        }
        bool _partNumberIsVisible = true;
        public bool PartNumberIsVisible
        {
            get
            {
                return _partNumberIsVisible;
            }

            set
            {
                if (value != _partNumberIsVisible)
                {
                    _partNumberIsVisible = value;
                    OnPropertyChanged(nameof(PartNumberIsVisible));
                }
            }
        }
        int? _quantityOnHandText;
        public int? QuantityOnHandText
        {
            get
            {
                return _quantityOnHandText;
            }

            set
            {
                if (value != _quantityOnHandText)
                {
                    _quantityOnHandText = value;
                    OnPropertyChanged(nameof(QuantityOnHandText));
                }
            }
        }
        bool _quantityOnHandIsVisible = true;
        public bool QuantityOnHandIsVisible
        {
            get
            {
                return _quantityOnHandIsVisible;
            }

            set
            {
                if (value != _quantityOnHandIsVisible)
                {
                    _quantityOnHandIsVisible = value;
                    OnPropertyChanged(nameof(QuantityOnHandIsVisible));
                }
            }
        }

        string _quantityRequiredText;
        public string QuantityRequiredText
        {
            get
            {
                return _quantityRequiredText;
            }

            set
            {
                if (value != _quantityRequiredText)
                {
                    _quantityRequiredText = value;
                    OnPropertyChanged(nameof(QuantityRequiredText));
                }
            }
        }

        string _quantityAllocatedText;
        public string QuantityAllocatedText
        {
            get
            {
                return _quantityAllocatedText;
            }

            set
            {
                if (value != _quantityAllocatedText)
                {
                    _quantityAllocatedText = value;
                    OnPropertyChanged(nameof(QuantityAllocatedText));
                }
            }

        }

        string _shelfBinName;
        public string ShelfBinName
        {
            get
            {
                return _shelfBinName;
            }

            set
            {
                if (value != _shelfBinName)
                {
                    _shelfBinName = value;
                    OnPropertyChanged(nameof(ShelfBinName));
                }
            }
        }

        string _unitCostText;
        public string UnitCostText
        {
            get
            {
                return _unitCostText;
            }

            set
            {
                if (value != _unitCostText)
                {
                    _unitCostText = value;
                    OnPropertyChanged(nameof(UnitCostText));
                }
            }
        }

        string _transactionTypeText;
        public string TransactionTypeText
        {
            get
            {
                return _transactionTypeText;
            }

            set
            {
                if (value != _transactionTypeText)
                {
                    _transactionTypeText = value;
                    OnPropertyChanged("TransactionTypeText");
                }
            }
        }
        bool _transactionTypeIsVisible = true;
        public bool TransactionTypeIsVisible
        {
            get
            {
                return _transactionTypeIsVisible;
            }

            set
            {
                if (value != _transactionTypeIsVisible)
                {
                    _transactionTypeIsVisible = value;
                    OnPropertyChanged(nameof(TransactionTypeIsVisible));
                }
            }
        }
        bool _transactionTypeIsEnabled = true;
        public bool TransactionTypeIsEnabled
        {
            get
            {
                return _transactionTypeIsEnabled;
            }

            set
            {
                if (value != _transactionTypeIsEnabled)
                {
                    _transactionTypeIsEnabled = value;
                    OnPropertyChanged(nameof(TransactionTypeIsEnabled));
                }
            }
        }
        string _transactionReasonText;
        public string TransactionReasonText
        {
            get
            {
                return _transactionReasonText;
            }

            set
            {
                if (value != _transactionReasonText)
                {
                    _transactionReasonText = value;
                    OnPropertyChanged("TransactionReasonText");
                }
            }
        }
        string _transactionReasonID;
        public string TransactionReasonID
        {
            get
            {
                return _transactionReasonID;
            }

            set
            {
                if (value != _transactionReasonID)
                {
                    _transactionReasonID = value;
                    OnPropertyChanged("TransactionReasonID");
                }
            }
        }

        string _costCenterText;
        public string CostCenterText
        {
            get
            {
                return _costCenterText;
            }

            set
            {
                if (value != _costCenterText)
                {
                    _costCenterText = value;
                    OnPropertyChanged("CostCenterText");
                }
            }
        }
        int _costCenterID;
        public int CostCenterID
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
                    OnPropertyChanged("CostCenterID");
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

                }
            }
        }


        #endregion


        #endregion

        #region RightsProperties


        ButtonControls CostCenter;
        ButtonControls Description;
        ButtonControls TransactionReason;
        ButtonControls TRansactor;
        ButtonControls UnitCost;


        bool _SelfBinIsVisible = true;
        public bool SelfBinIsVisible
        {
            get
            {
                return _SelfBinIsVisible;
            }

            set
            {
                if (value != _SelfBinIsVisible)
                {
                    _SelfBinIsVisible = value;
                    OnPropertyChanged(nameof(SelfBinIsVisible));
                }
            }
        }
        bool _SelfBinIsEnable = true;
        public bool SelfBinIsEnable
        {
            get
            {
                return _SelfBinIsEnable;
            }

            set
            {
                if (value != _SelfBinIsEnable)
                {
                    _SelfBinIsEnable = value;
                    OnPropertyChanged(nameof(SelfBinIsEnable));
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
        bool _descriptionIsEnabled = true;
        public bool DescriptionIsEnabled
        {
            get
            {
                return _descriptionIsEnabled;
            }

            set
            {
                if (value != _descriptionIsEnabled)
                {
                    _descriptionIsEnabled = value;
                    OnPropertyChanged(nameof(DescriptionIsEnabled));
                }
            }
        }
        bool _unitCostIsVisible = true;
        public bool UnitCostIsVisible
        {
            get
            {
                return _unitCostIsVisible;
            }

            set
            {
                if (value != _unitCostIsVisible)
                {
                    _unitCostIsVisible = value;
                    OnPropertyChanged(nameof(UnitCostIsVisible));
                }
            }
        }
        bool _unitCostIsEnabled = true;
        public bool UnitCostIsEnabled
        {
            get
            {
                return _unitCostIsEnabled;
            }

            set
            {
                if (value != _unitCostIsEnabled)
                {
                    _unitCostIsEnabled = value;
                    OnPropertyChanged(nameof(UnitCostIsEnabled));
                }
            }
        }
        bool _trasnactorIsVisible = true;
        public bool TrasnactorIsVisible
        {
            get
            {
                return _trasnactorIsVisible;
            }

            set
            {
                if (value != _trasnactorIsVisible)
                {
                    _trasnactorIsVisible = value;
                    OnPropertyChanged(nameof(TrasnactorIsVisible));
                }
            }
        }
        bool _trasnactorIsEnabled = true;
        public bool TrasnactorIsEnabled
        {
            get
            {
                return _trasnactorIsEnabled;
            }

            set
            {
                if (value != _trasnactorIsEnabled)
                {
                    _trasnactorIsEnabled = value;
                    OnPropertyChanged(nameof(TrasnactorIsEnabled));
                }
            }
        }
        bool _costcenterIsVisibile = true;
        public bool CostcenterIsVisibile
        {
            get
            {
                return _costcenterIsVisibile;
            }

            set
            {
                if (value != _costcenterIsVisibile)
                {
                    _costcenterIsVisibile = value;
                    OnPropertyChanged(nameof(CostcenterIsVisibile));
                }
            }
        }
        bool _costcenterIsEnabled = true;
        public bool CostcenterIsEnabled
        {
            get
            {
                return _costcenterIsEnabled;
            }

            set
            {
                if (value != _costcenterIsEnabled)
                {
                    _costcenterIsEnabled = value;
                    OnPropertyChanged(nameof(CostcenterIsEnabled));
                }
            }
        }
        bool _userfeild1IsVisible = true;
        public bool Userfeild1IsVisible
        {
            get
            {
                return _userfeild1IsVisible;
            }

            set
            {
                if (value != _userfeild1IsVisible)
                {
                    _userfeild1IsVisible = value;
                    OnPropertyChanged(nameof(Userfeild1IsVisible));
                }
            }
        }
        bool _userfeild1IsEnabled = true;
        public bool Userfeild1IsEnabled
        {
            get
            {
                return _userfeild1IsEnabled;
            }

            set
            {
                if (value != _userfeild1IsEnabled)
                {
                    _userfeild1IsEnabled = value;
                    OnPropertyChanged(nameof(Userfeild1IsEnabled));
                }
            }
        }

        bool _updateLastPhysicalDateVisible = true;
        public bool UpdateLastPhysicalDateVisible
        {
            get
            {
                return _updateLastPhysicalDateVisible;
            }

            set
            {
                if (value != _updateLastPhysicalDateVisible)
                {
                    _updateLastPhysicalDateVisible = value;
                    OnPropertyChanged(nameof(UpdateLastPhysicalDateVisible));
                }
            }
        }
        bool _updateLastPhysicalDateEnabled = true;
        public bool UpdateLastPhysicalDateEnabled
        {
            get
            {
                return _updateLastPhysicalDateEnabled;
            }

            set
            {
                if (value != _updateLastPhysicalDateEnabled)
                {
                    _updateLastPhysicalDateEnabled = value;
                    OnPropertyChanged(nameof(UpdateLastPhysicalDateEnabled));
                }
            }
        }

        bool _userfeild2IsVisible = true;
        public bool Userfeild2IsVisible
        {
            get
            {
                return _userfeild2IsVisible;
            }

            set
            {
                if (value != _userfeild2IsVisible)
                {
                    _userfeild2IsVisible = value;
                    OnPropertyChanged(nameof(Userfeild2IsVisible));
                }
            }
        }
        bool _userfeild2IsEnabled = true;
        public bool Userfeild2IsEnabled
        {
            get
            {
                return _userfeild2IsEnabled;
            }

            set
            {
                if (value != _userfeild2IsEnabled)
                {
                    _userfeild2IsEnabled = value;
                    OnPropertyChanged(nameof(Userfeild2IsEnabled));
                }
            }
        }

        bool _userfeild3IsVisible = true;
        public bool Userfeild3IsVisible
        {
            get
            {
                return _userfeild3IsVisible;
            }

            set
            {
                if (value != _userfeild3IsVisible)
                {
                    _userfeild3IsVisible = value;
                    OnPropertyChanged(nameof(Userfeild3IsVisible));
                }
            }
        }
        bool _userfeild3IsEnabled = true;
        public bool Userfeild3IsEnabled
        {
            get
            {
                return _userfeild3IsEnabled;
            }

            set
            {
                if (value != _userfeild3IsEnabled)
                {
                    _userfeild3IsEnabled = value;
                    OnPropertyChanged(nameof(Userfeild3IsEnabled));
                }
            }
        }
        bool _userfeild4IsVisible = true;
        public bool Userfeild4IsVisible
        {
            get
            {
                return _userfeild4IsVisible;
            }

            set
            {
                if (value != _userfeild4IsVisible)
                {
                    _userfeild4IsVisible = value;
                    OnPropertyChanged(nameof(Userfeild4IsVisible));
                }
            }
        }
        bool _userfeild4IsEnabled = true;
        public bool Userfeild4IsEnabled
        {
            get
            {
                return _userfeild4IsEnabled;
            }

            set
            {
                if (value != _userfeild4IsEnabled)
                {
                    _userfeild4IsEnabled = value;
                    OnPropertyChanged(nameof(Userfeild4IsEnabled));
                }
            }
        }

        bool _transactionReasonIsVisible = true;
        public bool TransactionReasonIsVisible
        {
            get
            {
                return _transactionReasonIsVisible;
            }

            set
            {
                if (value != _transactionReasonIsVisible)
                {
                    _transactionReasonIsVisible = value;
                    OnPropertyChanged(nameof(TransactionReasonIsVisible));
                }
            }
        }
        bool _transactionReasonIsEnabled = true;
        public bool TransactionReasonIsEnabled
        {
            get
            {
                return _transactionReasonIsEnabled;
            }

            set
            {
                if (value != _transactionReasonIsEnabled)
                {
                    _transactionReasonIsEnabled = value;
                    OnPropertyChanged(nameof(TransactionReasonIsEnabled));
                }
            }
        }
        #endregion
        #region UserfieldProperties
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


        #endregion


        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);

        public ICommand TransactionTypeCommand => new AsyncCommand(ShowTransactionType);
        public ICommand TransactionReasonCommand => new AsyncCommand(ShowTransactionReason);

        public ICommand CostCenterCommand => new AsyncCommand(ShowCostCenter);

        public ICommand ShelfBinCommand => new AsyncCommand(ShowShelfBin);

        public ICommand PerformByCommand => new AsyncCommand(ShowPerformBY);

        public ICommand CheckOutCommand => new AsyncCommand(ShowCheckoutTO);



        public ICommand ScanCommand => new AsyncCommand(SearchPart);

        //Save Command
        public ICommand PerformInventoryCommand => new AsyncCommand(SaveInventory);

        #endregion

        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                OperationInProgress = true;



                var targetNavigationData = navigationData as TargetNavigationData;
                this.StockroompartID = targetNavigationData.StockroomPartID;
                this.QuantityAllocatedText = targetNavigationData.QuantityAllocated.ToString();
                this.ShelfBinName = targetNavigationData.ShelfBin;
                this.PerformByText = AppSettings.UserName;
                this.CheckOutText = AppSettings.UserName;
                await GetTransactionParameters();
                await SetTitlesPropertiesForPage();
                OperationInProgress = false;
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

        public InventoryTransactionPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IInventoryService inventoryService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _inventoryService = inventoryService;
        }

        public async Task SetTitlesPropertiesForPage()
        {
            try
            {


                PageTitle = WebControlTitle.GetTargetNameByTitleName("PartTransaction");
                WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                StockRoomNametitle = WebControlTitle.GetTargetNameByTitleName("Stockroom");
                TransactionTypetitle = WebControlTitle.GetTargetNameByTitleName("TransactionType");
                LastPhysicalInventorytitle = WebControlTitle.GetTargetNameByTitleName("UpdateLastPhysicalInventorydate");
                Adjustmentquantitytitle = WebControlTitle.GetTargetNameByTitleName("TransactionQuantity");
                TransactionReasontitle = WebControlTitle.GetTargetNameByTitleName("TransactionReasonName");
                Costcentertitle = WebControlTitle.GetTargetNameByTitleName("CostCenter");
                UnitCosttitle = WebControlTitle.GetTargetNameByTitleName("UnitCost");
                Descriptiontitle = WebControlTitle.GetTargetNameByTitleName("Description");
                ShelfBintitle = WebControlTitle.GetTargetNameByTitleName("ShelfBin");
                PartNameTitle = WebControlTitle.GetTargetNameByTitleName("PartName");
                PartNumberTitle = WebControlTitle.GetTargetNameByTitleName("PartNumber");
                QuantityOnHandTitle = WebControlTitle.GetTargetNameByTitleName("QuantityOnHand");
                QuantityAllocatedTitle = WebControlTitle.GetTargetNameByTitleName("QuantityAllocated");
                CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                PerformBytitle = WebControlTitle.GetTargetNameByTitleName("PerformBy");
                ShelfBintitle = WebControlTitle.GetTargetNameByTitleName("ShelfBin");
                CheckOutTitle = WebControlTitle.GetTargetNameByTitleName("CheckOutTo");
                UserField1Title = WebControlTitle.GetTargetNameByTitleName("UserField1");
                UserField2Title = WebControlTitle.GetTargetNameByTitleName("UserField2");
                UserField3Title = WebControlTitle.GetTargetNameByTitleName("UserField3");
                UserField4Title = WebControlTitle.GetTargetNameByTitleName("UserField4");
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


            if (Application.Current.Properties.ContainsKey("InventoryPerformTransaction"))
            {
                var Perform_Transaction = Application.Current.Properties["InventoryPerformTransaction"].ToString();
                if (Perform_Transaction == "E")
                {
                    this.Perform_TransactionIsVisible = true;
                }
                else if (Perform_Transaction == "V")
                {
                    this.Perform_TransactionIsEnabled = false;

                }
                else
                {
                    this.Perform_TransactionIsVisible = false;
                }
            }

            if (Application.Current.Properties.ContainsKey("InventoryPartNumber"))
            {
                var PartNumber = Application.Current.Properties["InventoryPartNumber"].ToString();
                if (PartNumber == "E" || PartNumber == "V")
                {
                    this.PartNumberIsVisible = true;
                }
                else
                {
                    this.PartNumberIsVisible = false;
                }
            }
            if (Application.Current.Properties.ContainsKey("InventoryPartName"))
            {
                var PartName = Application.Current.Properties["InventoryPartName"].ToString();
                if (PartName == "E" || PartName == "V")
                {
                    this.PartNameIsVisible = true;
                }
                else
                {
                    this.PartNameIsVisible = false;
                }
            }
            if (Application.Current.Properties.ContainsKey("InventoryQuantityOnHand"))
            {
                var QuantityOnHand = Application.Current.Properties["InventoryQuantityOnHand"].ToString();
                if (QuantityOnHand == "E" || QuantityOnHand == "V")
                {
                    this.QuantityOnHandIsVisible = true;
                }
                else
                {
                    this.QuantityOnHandIsVisible = false;
                }
            }

            if (Application.Current.Properties.ContainsKey("InventoryCostCenterName"))
            {
                var CostCenter = Application.Current.Properties["InventoryCostCenterName"].ToString();
                if (CostCenter == "E")
                {
                    CostcenterIsVisibile = true;
                }
                else if (CostCenter == "V")
                {
                    CostcenterIsEnabled = false;
                }
                else
                {
                    CostcenterIsVisibile = false;
                }
            }
            if (Application.Current.Properties.ContainsKey("InventoryDescription"))
            {
                var Description = Application.Current.Properties["InventoryDescription"].ToString();
                if (Description == "E")
                {
                    DescriptionIsVisible = true;
                }
                else if (Description == "V")
                {
                    DescriptionIsEnabled = false;
                }
                else
                {
                    DescriptionIsVisible = false;
                }
            }
            if (Application.Current.Properties.ContainsKey("InventoryTransactionReasonName"))
            {
                var TransactionReason = Application.Current.Properties["InventoryTransactionReasonName"].ToString();
                if (TransactionReason == "E")
                {
                    TransactionReasonIsVisible = true;
                }
                else if (TransactionReason == "V")
                {
                    TransactionReasonIsEnabled = false;
                }
                else
                {
                    TransactionReasonIsVisible = false;
                }
            }
            if (Application.Current.Properties.ContainsKey("InventoryTransactor"))
            {
                var TRansactor = Application.Current.Properties["InventoryTransactor"].ToString();
                if (TRansactor == "E")
                {
                    TrasnactorIsVisible = true;
                }
                else if (TRansactor == "V")
                {
                    TrasnactorIsEnabled = false;
                }
                else
                {
                    TrasnactorIsVisible = false;
                }
            }

            //Unit Cost/////////
            if (Application.Current.Properties.ContainsKey("InventoryUnitCostID"))
            {
                var UnitCost = Application.Current.Properties["InventoryUnitCostID"].ToString();
                if (UnitCost == "E")
                {
                    UnitCostIsVisible = true;
                }
                else if (UnitCost == "V")
                {
                    UnitCostIsEnabled = false;
                }
                else
                {
                    UnitCostIsVisible = false;
                }
            }
            if (Application.Current.Properties.ContainsKey("InventoryCheckOutTo"))
            {
                var CheckOutTo = Application.Current.Properties["InventoryCheckOutTo"].ToString();
                if (CheckOutTo == "E")
                {
                    this.CheckOutIsVisible = true;
                }
                else if (CheckOutTo == "V")
                {
                    this.CheckOutIsEnabled = false;
                }
                else
                {
                    this.CheckOutIsVisible = false;
                }
            }
            if (Application.Current.Properties.ContainsKey("InventoryTransactionType"))
            {
                var TransactionType = Application.Current.Properties["InventoryTransactionType"].ToString();
                if (TransactionType == "E")
                {
                    this.TransactionTypeIsVisible = true;
                }
                else if (TransactionType == "V")
                {
                    this.TransactionTypeIsEnabled = false;
                }
                else
                {
                    this.TransactionTypeIsVisible = false;
                }
            }
            if (Application.Current.Properties.ContainsKey("InventoryTransactionQuantity"))
            {
                var Adjustmentquantity = Application.Current.Properties["InventoryTransactionQuantity"].ToString();
                if (Adjustmentquantity == "E")
                {
                    this.AdjustmentQuantityIsVisible = true;
                }
                else if (Adjustmentquantity == "V")
                {
                    this.AdjustmentQuantityIsEnabled = false;
                }
                else
                {
                    this.AdjustmentQuantityIsVisible = false;
                }
            }
            if (Application.Current.Properties.ContainsKey("InventoryUserField1"))
            {
                var UserField1 = Application.Current.Properties["InventoryUserField1"].ToString();
                if (UserField1 == "E")
                {
                    this.Userfeild1IsVisible = true;
                }
                else if (UserField1 == "V")
                {
                    this.Userfeild1IsEnabled = false;
                }
                else
                {
                    this.Userfeild1IsVisible = false;
                }
            }
            if (Application.Current.Properties.ContainsKey("InventoryUserField2"))
            {
                var UserField2 = Application.Current.Properties["InventoryUserField2"].ToString();
                if (UserField2 == "E")
                {
                    this.Userfeild2IsVisible = true;
                }
                else if (UserField2 == "V")
                {
                    this.Userfeild2IsEnabled = false;
                }
                else
                {
                    this.Userfeild2IsVisible = false;
                }
            }
            if (Application.Current.Properties.ContainsKey("InventoryUserField3"))
            {
                var UserField3 = Application.Current.Properties["InventoryUserField3"].ToString();
                if (UserField3 == "E")
                {
                    this.Userfeild3IsVisible = true;
                }
                else if (UserField3 == "V")
                {
                    this.Userfeild3IsEnabled = false;
                }
                else
                {
                    this.Userfeild3IsVisible = false;
                }
            }
            if (Application.Current.Properties.ContainsKey("InventoryUserField4"))
            {
                var UserField4 = Application.Current.Properties["InventoryUserField4"].ToString();
                if (UserField4 == "E")
                {
                    this.Userfeild4IsVisible = true;
                }
                else if (UserField4 == "V")
                {
                    this.Userfeild4IsEnabled = false;
                }
                else
                {
                    this.Userfeild4IsVisible = false;
                }
            }
            if (Application.Current.Properties.ContainsKey("UpdateLastPhysicalInventorydate"))
            {
                var UpdateLastPhysicalInventorydate = Application.Current.Properties["UpdateLastPhysicalInventorydate"].ToString();
                if (UpdateLastPhysicalInventorydate == "E")
                {
                    this.UpdateLastPhysicalDateVisible = true;
                }
                else if (UpdateLastPhysicalInventorydate == "V")
                {
                    this.UpdateLastPhysicalDateEnabled = false;
                }
                else
                {
                    this.UpdateLastPhysicalDateVisible = false;
                }
            }



            if (Application.Current.Properties.ContainsKey("ShelfBinKey"))
            {
                var shelfBin = Application.Current.Properties["ShelfBinKey"].ToString();
                if (shelfBin == "E")
                {
                    SelfBinIsVisible = true;
                }
                else if (shelfBin == "V")
                {
                    SelfBinIsEnable = false;
                }
                else
                {
                    SelfBinIsVisible = false;
                }
            }
        }
                                   
                                

                                    
                               

        public async Task ShowActions()
        {
            try
            {

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
                OperationInProgress = false;
            }

            finally
            {
                OperationInProgress = false;
            }
        }

        public async Task ShowShelfBin()
        {
            try
            {
                OperationInProgress = true;
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.StockroomPartID = this.StockroompartID;
                await NavigationService.NavigateToAsync<InventoryShelfBinListSelectionPageViewModel>(tnobj); //Pass the control here
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
        public async Task ShowPerformBY()
        {
            try
            {
                OperationInProgress = true;

                await NavigationService.NavigateToAsync<InventoryPerformByListSelectionPageViewModel>(); //Pass the control here
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
        public async Task ShowCheckoutTO()
        {
            try
            {
                OperationInProgress = true;

                await NavigationService.NavigateToAsync<InventoryCheckoutTOListSelectionPageViewModel>(); //Pass the control here
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

        public async Task ShowCostCenter()
        {
            try
            {
                OperationInProgress = true;
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.StockroomPartID = this.StockroompartID;
                await NavigationService.NavigateToAsync<InventoryCostCenterListSelectionPageViewModel>(tnobj); //Pass the control here
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

        public async Task ShowTransactionType()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                // OperationInProgress = true;
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.StockroomPartID = this.StockroompartID;
                await NavigationService.NavigateToAsync<TransactionTypeListSelectionPageViewModel>(tnobj); //Pass the control here
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

        public async Task ShowTransactionReason()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                //OperationInProgress = true;
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.StockroomPartID = this.StockroompartID;
                await NavigationService.NavigateToAsync<TransactionReasonListSelectionPageViewModel>(tnobj); //Pass the control here
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
        async Task GetTransactionParameters()
        {
            try
            {

                OperationInProgress = true;
                var TransactionParameters = await _inventoryService.GetTransactionReason(StockroompartID.ToString(), UserID);
                if (TransactionParameters != null && TransactionParameters.inventoryWrapper.trasactionDialog != null)
                {
                    var trasactiontype = TransactionParameters.inventoryWrapper.trasactionDialog;
                    this.PartNameText = trasactiontype.PartName;
                    this.PartNumberText = trasactiontype.PartNumber;
                    this.QuantityOnHandText = trasactiontype.QuantityOnHand;
                    this.StockroomNameText = trasactiontype.StockroomName;
                    this.QuantityAllocatedText = Math.Round(Convert.ToDecimal(this.QuantityAllocatedText), 2).ToString();
                    this.UnitCostText = string.Format(StringFormat.CurrencyZero(), trasactiontype.OriginalAmount == null ? 0 : trasactiontype.OriginalAmount);


                    ////Set Part Profile Picture///////
                    if (!string.IsNullOrWhiteSpace(TransactionParameters.inventoryWrapper.trasactionDialog.Base64Image))
                    {


                        if (Device.RuntimePlatform == Device.UWP)
                        {
                            byte[] imgUser = StreamToBase64.StringToByte(TransactionParameters.inventoryWrapper.trasactionDialog.Base64Image);
                            MemoryStream stream = new MemoryStream(imgUser);
                            bool isimage = Extension.IsImage(stream);
                            if (isimage == true)
                            {

                                byte[] byteImage = await Xamarin.Forms.DependencyService.Get<IResizeImage>().ResizeImageAndroid(imgUser, 160, 100);
                                AttachmentImageSource = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(byteImage))));



                            }
                        }
                        else
                        {
                            byte[] imgUser = StreamToBase64.StringToByte(TransactionParameters.inventoryWrapper.trasactionDialog.Base64Image);
                            MemoryStream stream = new MemoryStream(imgUser);
                            bool isimage = Extension.IsImage(stream);
                            if (isimage == true)
                            {

                                //byte[] byteImage = await Xamarin.Forms.DependencyService.Get<IResizeImage>().ResizeImageAndroid(imgUser, 160, 100);
                                AttachmentImageSource = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(imgUser))));



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

        public async Task SaveInventory()
        {
            try
            {

                if (PhysicalDateSwitch == true)
                {
                    LastPhysicalInventorydate = true;
                }
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                //   OperationInProgress = true;


                if (String.IsNullOrWhiteSpace(TransactionTypeText))
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("TransactionTypeisMandatory"));
                    return;

                }
                if (String.IsNullOrWhiteSpace(AdjustmentQuantityText))
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("QuantityisMandatory"));
                    return;

                }
                if (Convert.ToInt32(AdjustmentQuantityText) <= 0)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleasefillthevalidquantity"));
                    return;

                }

                if (String.IsNullOrWhiteSpace(UnitCostText))
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("UnitCostismandatoryfield"));
                    return;

                }
                try
                {

                    var k = Convert.ToInt32(AdjustmentQuantityText);
                    //var k = decimal.Parse(AdjustmentQuantityText);
                }
                catch (Exception ex)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleaseenterthevalidquantity"));
                    return;
                }
                try
                {

                    var k = decimal.Parse(UnitCostText);
                }
                catch (Exception ex)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleasefillthevalidunitcost"));
                    return;
                }

                if (TransactionTypeText == "Subtract" && int.Parse(AdjustmentQuantityText) > QuantityOnHandText)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("TransactionQuantitycannotbegreaterthanQuantityOnHand"));
                    return;
                }

                if (this.ShelfBinID == null)
                {
                    this.ShelfBinID = 0;
                }

                #endregion


                var inventory = new InventoryTransaction
                {

                    AdjustmentQuantity = Convert.ToInt32(this.AdjustmentQuantityText),
                    CostCenter = this.CostCenterText,
                    CostCenterID = this.CostCenterID,
                    Description = String.IsNullOrEmpty(this.DescriptionText) ? null : this.DescriptionText.Trim(),
                    PartName = this.PartNameText,
                    PartNumber = this.PartNumberText,
                    StockroomName = this.StockroomNameText,
                    QuantityOnHand = this.QuantityOnHandText,
                    StockroomPartID = this.StockroompartID,
                    TransactionReason = this.TransactionReasonText,
                    TransactionType = this.TransactionTypeText,
                    ShelfBinID = this.ShelfBinID,
                    ShelfBinName = this.ShelfBinText,
                    Transactor = this.PerformByText,
                    CheckOutTo = this.CheckOutText,
                    UserField1 = UserField1,
                    UserField2 = UserField2,
                    UserField3 = UserField3,
                    UserField4 = UserField4,
                    ModifiedUserName = AppSettings.User.UserName,
                    UpdateLastPhysicalInventorydate = LastPhysicalInventorydate,
                    UnitCost = decimal.Parse(this.UnitCostText)

                };


                var response = await _inventoryService.PerformInventoryTransaction(inventory);
                if (response != null && bool.Parse(response.servicestatus))
                {
                    Application.Current.Properties["CallfromTransactionPage"] = "true";
                    //DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Transactionissuccessfullysaved"), 2000);
                    DialogService.ShowAlertAsync(WebControlTitle.GetTargetNameByTitleName("Transactionissuccessfullysaved"), response.TransactionNumber, "OK");
                    await NavigationService.NavigateBackAsync();

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







        public Task OnViewDisappearingAsync(VisualElement view)
        {

            return Task.FromResult(true);

        }

        public async Task OnViewAppearingAsync(VisualElement view)
        {
            try
            {

                try
                {


                    OperationInProgress = true;

                    if (!IsPickerDataSubscribed)
                    {


                        MessagingCenter.Subscribe<object>(this, MessengerKeys.ShelfBinRequested, OnShelfBinRequested);

                        MessagingCenter.Subscribe<object>(this, MessengerKeys.TransactionReasonRequested, OnTransactionReasonRequested);

                        MessagingCenter.Subscribe<object>(this, MessengerKeys.TransactionTypeRequested, OnTransactionTypeRequested);

                        MessagingCenter.Subscribe<object>(this, MessengerKeys.CostcenterRequested, OnCostCenterRequested);

                        MessagingCenter.Subscribe<object>(this, MessengerKeys.OnPerformBYRequested, OnPerformBYRequested);

                        MessagingCenter.Subscribe<object>(this, MessengerKeys.OnCheckoutRequested, OnCheckoutToRequested);


                        IsPickerDataSubscribed = true;
                    }

                    else if (IsPickerDataRequested)
                    {

                        IsPickerDataRequested = false;
                        return;
                    }


                    /// here perform tasks




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


        private void OnShelfBinRequested(object obj)
        {

            if (obj != null)
            {

                var ShelfBin = obj as ShelfBin;
                this.ShelfBinID = ShelfBin.ShelfBinID;
                this.ShelfBinText = ShelfBin.ShelfBinName;
            }


        }

        private void OnPerformBYRequested(object obj)
        {

            if (obj != null)
            {

                var PerformBY = obj as ComboDD;
                this.PerformByText = PerformBY.SelectedText;

            }


        }
        private void OnCheckoutToRequested(object obj)
        {

            if (obj != null)
            {

                var Checkout = obj as ComboDD;
                this.CheckOutText = Checkout.SelectedText;

            }


        }
        private void OnCostCenterRequested(object obj)
        {

            if (obj != null)
            {

                var costCenter = obj as CostCenter;
                this.CostCenterID = costCenter.CostCenterID;
                this.CostCenterText = costCenter.CostCenterName;
            }


        }
        private void OnTransactionReasonRequested(object obj)
        {

            if (obj != null)
            {

                var transactionReason = obj as TransactionReason;
                this.TransactionReasonID = transactionReason.TransactionReasonID.ToString();
                this.TransactionReasonText = transactionReason.TransactionReasonName;
            }


        }
        private void OnTransactionTypeRequested(object obj)
        {

            if (obj != null)
            {

                var transactionType = obj as TransactionType;
                this.TransactionTypeText = transactionType.TypeName;

            }


        }
        public async Task SearchPart()
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
                    //  await GetPartFromScan();

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
                //  await GetPartFromScan();


            });

        }
        //private async Task GetPartFromScan()
        //{
        //    var duplicatepart = await _workorderService.CheckDuplicatePart(WorkorderID, StockroomID.ToString(), this.SearchText, "null");
        //    if (duplicatepart.inventoryWrapper.isPartDuplicate)
        //    {
        //        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("ThisPartisalreadyaddedinworkorder."), 2000);
        //        return;
        //    }
        //    var getstockroomPartDetail = await _workorderService.GetStockroomPartDetailFromScan(StockroomID.ToString(), this.SearchText);
        //    if (getstockroomPartDetail.inventoryWrapper == null || getstockroomPartDetail.inventoryWrapper.stockroomparts == null)
        //    {
        //        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Thispartdoesnotexist."), 2000);
        //        return;
        //    }

        //    if (getstockroomPartDetail.inventoryWrapper != null || getstockroomPartDetail.inventoryWrapper.stockroomparts != null)
        //    {
        //        bool? SerializedPart = getstockroomPartDetail.inventoryWrapper.stockroomparts[0].IsPartSerialized;
        //        if (SerializedPart == true)
        //        {
        //            DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("CannotScanSerializedPart."), 2000);
        //            return;
        //        }
        //        else
        //        {
        //            PartNumberText = getstockroomPartDetail.inventoryWrapper.stockroomparts[0].PartNumber;
        //            if (getstockroomPartDetail.inventoryWrapper.stockroomparts[0].OriginalAmount == null)
        //            {
        //                UnitCostText = "0";
        //            }
        //            else
        //            {
        //                UnitCostText = String.Format(StringFormat.CurrencyZero(), getstockroomPartDetail.inventoryWrapper.stockroomparts[0].OriginalAmount);

        //            }
        //            if (getstockroomPartDetail.inventoryWrapper.stockroomparts.SingleOrDefault().ShelfBins == null)
        //            {
        //                ShelfBinText = "";
        //                if (getstockroomPartDetail.inventoryWrapper.stockroomparts[0].ShelfBin == null)
        //                {

        //                }
        //                else
        //                {
        //                    ShelfBinText = getstockroomPartDetail.inventoryWrapper.stockroomparts[0].ShelfBin.ToString();

        //                }
        //            }
        //            else
        //            {
        //                ShelfBinText = "";
        //                shelfbin.Clear();
        //                foreach (var item in getstockroomPartDetail.inventoryWrapper.stockroomparts.SingleOrDefault().ShelfBins)
        //                {
        //                    ShelfBin slfbin1 = new ShelfBin();
        //                    slfbin1.ShelfBinID = item.ShelfBinID;
        //                    slfbin1.ShelfBinName = item.ShelfBinName;
        //                    shelfbin.Add(slfbin1);

        //                }



        //                var firstElement = shelfbin.First();
        //                ShelfBinText = firstElement.ShelfBinName.ToString();
        //                this.ShelfBinID = firstElement.ShelfBinID;
        //            }
        //            this.StockroompartID = getstockroomPartDetail.inventoryWrapper.stockroomparts[0].StockroomPartID;

        //        }
        //    }

        //}





    }
}
