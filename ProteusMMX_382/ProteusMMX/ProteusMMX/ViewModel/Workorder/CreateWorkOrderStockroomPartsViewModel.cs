using Acr.UserDialogs;
using ProteusMMX.Constants;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.StringFormatter;
using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.InventoryModel;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.SelectionListPagesViewModels;
using ProteusMMX.ViewModel.SelectionListPagesViewModels.Workorder.Parts;
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
    public class CreateWorkOrderStockroomPartsViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        #region Fields
        List<ShelfBin> shelfbin = new List<ShelfBin>();
        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

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



        #region CreateWorkOrderStockroomParts Properties





        string _serverTimeZone = AppSettings.User.ServerIANATimeZone;
        public string ServerTimeZone
        {
            get { return _serverTimeZone; }
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





        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);

        public ICommand StockroomNameCommand => new AsyncCommand(ShowStockroom);
        public ICommand PartNumberCommand => new AsyncCommand(ShowPart);

        public ICommand ShelfBinCommand => new AsyncCommand(ShowShelfBin);

        public ICommand ScanCommand => new AsyncCommand(SearchPart);

        //Save Command
        public ICommand SaveStockroomPartsCommand => new AsyncCommand(SaveStockroomParts);

        #endregion

        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                OperationInProgress = true;


                if (navigationData != null)
                {


                    this.WorkorderID = Convert.ToInt32(navigationData);



                }



                //    FormLoadInputForWorkorder = await _formLoadInputService.GetFormLoadInputForBarcode(UserID, AppSettings.WorkorderDetailFormName);
                await SetTitlesPropertiesForPage();


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

        public CreateWorkOrderStockroomPartsViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IWorkorderService workorderService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _workorderService = workorderService;
        }

        public async Task SetTitlesPropertiesForPage()
        {
            try
            {


                PageTitle = WebControlTitle.GetTargetNameByTitleName("CreateStockroomParts");
                WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                StockRoomNametitle = WebControlTitle.GetTargetNameByTitleName("Stockroom");
                PartNumberTitle = WebControlTitle.GetTargetNameByTitleName("Parts");
                QuantityRequiredtitle = WebControlTitle.GetTargetNameByTitleName("QuantityRequired");
                QuantityAllocatedtitle = WebControlTitle.GetTargetNameByTitleName("QuantityAllocated");
                UnitCosttitle = WebControlTitle.GetTargetNameByTitleName("UnitCost");
                GoTitle = WebControlTitle.GetTargetNameByTitleName("Go");
                ScanTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                SearchButtonTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                ShelfBintitle = WebControlTitle.GetTargetNameByTitleName("ShelfBin");
                CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
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
        public async Task ShowStockroom()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                // OperationInProgress = true;

                await NavigationService.NavigateToAsync<StockroomListSelectionPageViewModel>(new TargetNavigationData() { }); //Pass the control here
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
                // OperationInProgress = false;

            }
        }

        public async Task ShowPart()
        {
            try
            {
                if (this.StockroomID == null)
                {
                    UserDialogs.Instance.HideLoading();
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleaseselectstockroomfirst"), 2000);
                    return;
                }
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                // OperationInProgress = true;
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.StockroomID = this.StockroomID;
                tnobj.WorkOrderId = this.WorkorderID;
                await NavigationService.NavigateToAsync<PartListSelectionPageViewModel>(tnobj); //Pass the control here
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
        public async Task ShowShelfBin()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                //  OperationInProgress = true;
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.lstShelfBin = shelfbin;

                await NavigationService.NavigateToAsync<ShelfBinListSelectionPageViewModel>(tnobj); //Pass the control here
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




        public async Task SaveStockroomParts()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                // OperationInProgress = true;

                //Check Mandatory fields and other Validations////

                if (StockroomID == null)
                {
                    UserDialogs.Instance.HideLoading();
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Stockroomismandatoryfield"));
                    return;
                }

                else if (StockroompartID == null)
                {
                    UserDialogs.Instance.HideLoading();
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Partismandatoryfield"));
                    return;
                }


                //else if (string.IsNullOrWhiteSpace(QuantityAllocatedText))
                //{
                //    UserDialogs.Instance.HideLoading();
                //    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Quantityallocatedismandatoryfield"));
                //    return;
                //}
                //else if (string.IsNullOrWhiteSpace(QuantityRequiredText))
                //{
                //    UserDialogs.Instance.HideLoading();
                //    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("QunatityRequiredismandatoryfield"));
                //    return;
                //}

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
                workOrderStockroompart.WorkOrderID = WorkorderID;
                workOrderStockroompart.ShelfBinID = this.ShelfBinID;
                workOrderStockroompart.UnitCostAmount = decimal.Parse(UnitCostText);


                #endregion






                #endregion


                var workorder = new workOrderWrapper
                {

                    workOrderStockroomPart = workOrderStockroompart

                };


                var response = await _workorderService.CreateWorkorderStockroomParts(workorder);
                if (response != null && bool.Parse(response.servicestatus))
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Partsaddedsuccessfully"), 2000);
                    await NavigationService.NavigateBackAsync();

                }
                UserDialogs.Instance.HideLoading();
                // OperationInProgress = false;



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
                        //Retrive ToolCrib
                        MessagingCenter.Subscribe<object>(this, MessengerKeys.PartRequested, OnPartRequested);

                        //Retrive ToolNumber
                        MessagingCenter.Subscribe<object>(this, MessengerKeys.StockoomRequested, OnStockroomRequested);

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

        private void OnPartRequested(object obj)
        {

            if (obj != null)
            {

                var part = obj as StockroomPart;

                this.StockroompartID = part.StockroomPartID;
                if (string.IsNullOrWhiteSpace(part.PartNumber))
                {
                    this.PartNumberText = (part.PartNumber);
                }
                else
                {
                    this.PartNumberText = ShortString.shorten(part.PartNumber);
                }

                ShelfBinText = "";
                QuantityRequiredText = "";
                QuantityAllocatedText = "";
                UnitCostText = "";


                this.UnitCostText = string.Format(StringFormat.CurrencyZero(), part.UnitCostAmount == null ? 0 : part.UnitCostAmount);

                //if (part.ShelfBins != null)
                //{
                //    shelfbin.Clear();

                //    foreach (var item in part.ShelfBins)
                //    {
                //        ShelfBin slfbin1 = new ShelfBin();
                //        slfbin1.ShelfBinID = item.ShelfBinID;
                //        slfbin1.ShelfBinName = item.ShelfBinName;
                //        shelfbin.Add(slfbin1);

                //    }
                //    var firstElement = shelfbin.First();
                //    ShelfBinText = firstElement.ShelfBinName.ToString();
                //    this.ShelfBinID = firstElement.ShelfBinID;

                //}
                //else
                //{
                if (!String.IsNullOrWhiteSpace(part.ShelfBin))
                {
                    ShelfBinText = part.ShelfBin;
                }
                else
                {

                }

                //   }


            }


        }

        private void OnStockroomRequested(object obj)
        {

            if (obj != null)
            {

                var stkroom = obj as Stockroom;
                this.StockroomID = stkroom.StockroomID;
                this.StockroomNameText = ShortString.shorten(stkroom.StockroomName);

                PartNumberText = "";
                ShelfBinText = "";
                QuantityRequiredText = "";
                QuantityAllocatedText = "";
                UnitCostText = "";


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
        public async Task SearchPart()
        {

            try
            {
                if (this.StockroomID == null)
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleaseselectstockroomfirst"), 2000);
                    return;
                }
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
                    await GetPartFromScan();

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
                await GetPartFromScan();


            });

        }
        private async Task GetPartFromScan()
        {

            var duplicatepart = await _workorderService.CheckDuplicatePart(WorkorderID, StockroomID.ToString(), this.SearchText, "null");
            if (duplicatepart.inventoryWrapper.isPartDuplicate)
            {
                DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("ThisPartisalreadyaddedinworkorder"), 2000);
                return;
            }
            var getstockroomPartDetail = await _workorderService.GetStockroomPartDetailFromScan(StockroomID.ToString(), this.SearchText);
            if (getstockroomPartDetail.inventoryWrapper == null || getstockroomPartDetail.inventoryWrapper.stockroomparts == null)
            {
                DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Thispartdoesnotexist"), 2000);
                return;
            }

            if (getstockroomPartDetail.inventoryWrapper != null || getstockroomPartDetail.inventoryWrapper.stockroomparts != null)
            {
                bool? SerializedPart = getstockroomPartDetail.inventoryWrapper.stockroomparts[0].IsPartSerialized;
                if (SerializedPart == true)
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("CannotScanSerializedPart"), 2000);
                    return;
                }
                else
                {
                    PartNumberText = ShortString.shorten(getstockroomPartDetail.inventoryWrapper.stockroomparts[0].PartNumber);
                    if (getstockroomPartDetail.inventoryWrapper.stockroomparts[0].OriginalAmount == null)
                    {
                        UnitCostText = "0";
                    }
                    else
                    {
                        UnitCostText = String.Format(StringFormat.CurrencyZero(), getstockroomPartDetail.inventoryWrapper.stockroomparts[0].OriginalAmount);

                    }
                    if (getstockroomPartDetail.inventoryWrapper.stockroomparts.SingleOrDefault().ShelfBins == null)
                    {
                        ShelfBinText = "";
                        if (getstockroomPartDetail.inventoryWrapper.stockroomparts[0].ShelfBin == null)
                        {

                        }
                        else
                        {
                            ShelfBinText = getstockroomPartDetail.inventoryWrapper.stockroomparts[0].ShelfBin.ToString();

                        }
                    }
                    else
                    {
                        ShelfBinText = "";
                        shelfbin.Clear();
                        foreach (var item in getstockroomPartDetail.inventoryWrapper.stockroomparts.SingleOrDefault().ShelfBins)
                        {
                            ShelfBin slfbin1 = new ShelfBin();
                            slfbin1.ShelfBinID = item.ShelfBinID;
                            slfbin1.ShelfBinName = item.ShelfBinName;
                            shelfbin.Add(slfbin1);

                        }



                        var firstElement = shelfbin.First();
                        ShelfBinText = firstElement.ShelfBinName.ToString();
                        this.ShelfBinID = firstElement.ShelfBinID;
                    }
                    this.StockroompartID = getstockroomPartDetail.inventoryWrapper.stockroomparts[0].StockroomPartID;

                }
            }

        }





    }
}
