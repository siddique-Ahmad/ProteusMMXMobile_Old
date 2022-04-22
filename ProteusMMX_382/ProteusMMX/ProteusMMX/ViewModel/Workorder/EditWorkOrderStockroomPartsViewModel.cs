using Acr.UserDialogs;
using ProteusMMX.Constants;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.StringFormatter;
using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.SelectionListPagesViewModels.Workorder.Parts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
namespace ProteusMMX.ViewModel.Workorder
{
    public class EditWorkOrderStockroomPartsViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        #region Fields
        List<ShelfBin> shelfbin = new List<ShelfBin>();
        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly IWorkorderService _workorderService;
        #endregion

        #region Properties
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
        int? _workorderID;
        public int? WorkorderID
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

        int _workorderstockroompartID;
        public int WorkorderstockroompartID
        {
            get
            {
                return _workorderstockroompartID;
            }

            set
            {
                if (value != _workorderstockroompartID)
                {
                    _workorderstockroompartID = value;
                    OnPropertyChanged(nameof(StockroompartID));
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
        string _partNumbertitle;
        public string PartNumberTitle
        {
            get
            {
                return _partNumbertitle;
            }

            set
            {
                if (value != _partNumbertitle)
                {
                    _partNumbertitle = value;
                    OnPropertyChanged("PartNumberTitle");
                }
            }
        }

        string _quantityRequiredtitle;
        public string QuantityRequiredtitle
        {
            get
            {
                return _quantityRequiredtitle;
            }

            set
            {
                if (value != _quantityRequiredtitle)
                {
                    _quantityRequiredtitle = value;
                    OnPropertyChanged("QuantityRequiredtitle");
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




        #endregion

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

        #endregion



        #region EditWorkOrderStockroomParts Properties





        string _serverTimeZone = AppSettings.User.ServerIANATimeZone;
        public string ServerTimeZone
        {
            get { return _serverTimeZone; }
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


        #endregion


        #endregion


        #region More IsVisible 
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

        bool _moreTextIsEnable = false;
        public bool MoreTextIsEnable
        {
            get
            {
                return _moreTextIsEnable;
            }

            set
            {
                if (value != _moreTextIsEnable)
                {
                    _moreTextIsEnable = value;
                    OnPropertyChanged(nameof(MoreTextIsEnable));
                }
            }
        }

        #endregion


        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);

        public ICommand ShelfBinCommand => new AsyncCommand(ShowShelfBin);

        //Save Command
        public ICommand SaveEditStockroomPartsCommand => new AsyncCommand(EditStockroomParts);

        #endregion

        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                OperationInProgress = true;


                if (navigationData != null)
                {
                    var targetNavigationData = navigationData as TargetNavigationData;

                    this.WorkorderID = targetNavigationData.WorkOrderId;
                    this.WorkorderstockroompartID = targetNavigationData.WorkOrderStockroomPartID;
                }

                if (Application.Current.Properties.ContainsKey("EditParts"))
                {
                    var EditParts = Application.Current.Properties["EditParts"].ToString();
                    if (EditParts == "E")
                    {
                        this.StockroomPartIsVisible = true;
                    }
                    else if (EditParts == "V")
                    {
                        this.StockroomPartIsEnabled = false;

                    }
                    else
                    {
                        this.StockroomPartIsVisible = false;
                    }


                }

                await SetTitlesPropertiesForPage();
                await GetWorkorderStockRoomPartdetails();


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

        public EditWorkOrderStockroomPartsViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IWorkorderService workorderService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _workorderService = workorderService;
        }

        public async Task SetTitlesPropertiesForPage()
        {
            try
            {


                PageTitle = WebControlTitle.GetTargetNameByTitleName("EditStockroomPartDetails");
                WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                StockRoomNametitle = WebControlTitle.GetTargetNameByTitleName("Stockroom");
                PartNumberTitle = WebControlTitle.GetTargetNameByTitleName("Parts");
                QuantityRequiredtitle = WebControlTitle.GetTargetNameByTitleName("QuantityRequired");
                UnitCosttitle = WebControlTitle.GetTargetNameByTitleName("UnitCost");
                ShelfBintitle = WebControlTitle.GetTargetNameByTitleName("ShelfBin");
                CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                SaveTitle = WebControlTitle.GetTargetNameByTitleName("Save");
                QuantityAllocatedtitle = WebControlTitle.GetTargetNameByTitleName("QuantityAllocated");
                SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");


            }
            catch (Exception ex)
            {


            }

            finally
            {

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
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                // OperationInProgress = true;
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.lstShelfBin = shelfbin;

                await NavigationService.NavigateToAsync<ShelfBinListSelectionPageViewModel>(tnobj); //Pass the control here
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


        async Task GetWorkorderStockRoomPartdetails()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                //  OperationInProgress = true;
                var workordersResponse = await _workorderService.GetWorkorderStockroomPartsDetail(WorkorderID.ToString(), WorkorderstockroompartID.ToString());
                if (workordersResponse != null && workordersResponse.workOrderWrapper != null
                    && workordersResponse.workOrderWrapper.workOrderStockroomPart != null)
                {

                    var workorderstkpart = workordersResponse.workOrderWrapper.workOrderStockroomPart;
                    StockroompartID = workorderstkpart.StockroomPartID;
                    WorkorderstockroompartID = workorderstkpart.WorkOrderStockroomPartID;
                    QuantityAllocatedText = string.Format(StringFormat.NumericZero(), workorderstkpart.QuantityAllocated == null ? 0 : workorderstkpart.QuantityAllocated);
                    QuantityRequiredText = string.Format(StringFormat.NumericZero(), workorderstkpart.QuantityRequired == null ? 0 : workorderstkpart.QuantityRequired);
                    StockroomNameText = workorderstkpart.StockroomName;
                    PartNumberText = workorderstkpart.PartNumber;
                    UnitCostText = string.Format(StringFormat.CurrencyZero(), workorderstkpart.UnitCostAmount == null ? 0 : workorderstkpart.UnitCostAmount);
                    ShelfBinText = ShortString.shorten(workorderstkpart.ShelfBin.ToString());
                    this.ShelfBinID = workorderstkpart.ShelfBinID;
                    this.ShelfBinText = workorderstkpart.ShelfBin;
                    //foreach (var item in workorderstkpart.ShelfBins)
                    //{
                    //    ShelfBin slfbin1 = new ShelfBin();
                    //    slfbin1.ShelfBinID = item.ShelfBinID;
                    //    slfbin1.ShelfBinName = item.ShelfBinName;
                    //    shelfbin.Add(slfbin1);

                    //}


                }
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



        public async Task EditStockroomParts()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                // OperationInProgress = true;


                //Check Mandatory fields and other Validations////


                if (string.IsNullOrWhiteSpace(QuantityAllocatedText))
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Quantityallocatedismandatoryfield"));
                    return;
                }
                else if (string.IsNullOrWhiteSpace(QuantityRequiredText))
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("QunatityRequiredismandatoryfield"));
                    return;
                }

                else
                {
                    decimal _QuantityRequired1;
                    if (!decimal.TryParse(QuantityRequiredText, out _QuantityRequired1))
                    {
                        UserDialogs.Instance.HideLoading();

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("PleasefillthevalidQuantityRequired"));
                        return;
                    }

                    decimal _QuantityAllocated1;
                    if (!decimal.TryParse(QuantityAllocatedText, out _QuantityAllocated1))
                    {
                        UserDialogs.Instance.HideLoading();

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleasefillthevalidquantityallocated"));
                        return;
                    }

                    decimal _UnitCost1;
                    if (!decimal.TryParse(UnitCostText, out _UnitCost1))
                    {
                        UserDialogs.Instance.HideLoading();

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleasefillthevalidunitcost"));
                        return;
                    }
                    try
                    {
                        var s = decimal.Parse(QuantityAllocatedText);
                        var k = decimal.Parse(QuantityRequiredText);
                    }
                    catch (Exception ex)
                    {
                        UserDialogs.Instance.HideLoading();

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleaseenterthevalidquantity"));
                        return;
                    }
                    try
                    {
                        var s3 = decimal.Parse(UnitCostText);


                    }
                    catch (Exception ex)
                    {
                        UserDialogs.Instance.HideLoading();

                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("PleaseenterthevalidCost"));
                        return;
                    }
                }

                var workOrderStockroompart = new WorkOrderStockroomParts();
                #region workOrderStockroompart properties initialzation


                workOrderStockroompart.QuantityAllocated = decimal.Parse(QuantityAllocatedText);
                workOrderStockroompart.QuantityRequired = decimal.Parse(QuantityRequiredText);
                workOrderStockroompart.StockroomPartID = StockroompartID;
                workOrderStockroompart.WorkOrderID = this.WorkorderID;
                workOrderStockroompart.WorkOrderStockroomPartID = WorkorderstockroompartID;
                workOrderStockroompart.UnitCostAmount = decimal.Parse(UnitCostText);




                #endregion






                #endregion


                var workorder = new workOrderWrapper
                {

                    workOrderStockroomPart = workOrderStockroompart

                };


                var response = await _workorderService.EditWorkorderStockroomParts(workorder);
                if (response != null && bool.Parse(response.servicestatus))
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Partupdatedsuccessfully"), 2000);
                    await NavigationService.NavigateBackAsync();

                    //await NavigationService.NavigateToAsync<WorkorderTabbedPageViewModel>(response.workOrderWrapper.workOrder);
                    //await NavigationService.RemoveLastFromBackStackAsync();



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

                //  OperationInProgress = false;

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
                this.ShelfBinText = ShortString.shorten(ShelfBin.ShelfBinName);
            }


        }

    }
}
