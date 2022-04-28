using Acr.UserDialogs;
using ProteusMMX.Constants;
using ProteusMMX.Helpers;
using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.SelectionListPageServices.Tool;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.SelectionListPagesViewModels.Workorder.Tools;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace ProteusMMX.ViewModel.Workorder
{
    public class AddNewToolViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly IWorkorderService _workorderService;

        protected readonly IToolService _toolService;
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

        string _toolCribTitle;
        public string ToolCribTitle
        {
            get
            {
                return _toolCribTitle;
            }

            set
            {
                if (value != _toolCribTitle)
                {
                    _toolCribTitle = value;
                    OnPropertyChanged("ToolCribTitle");
                }
            }
        }

        string _toolNumbertitle;
        public string ToolNumbertitle
        {
            get
            {
                return _toolNumbertitle;
            }

            set
            {
                if (value != _toolNumbertitle)
                {
                    _toolNumbertitle = value;
                    OnPropertyChanged("ToolNumbertitle");
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



        #region CreateTool Properties





        string _serverTimeZone = AppSettings.User.ServerIANATimeZone;
        public string ServerTimeZone
        {
            get { return _serverTimeZone; }
        }


        #region Normal Field Properties



        string _toolCribText;
        public string ToolCribText
        {
            get
            {
                return _toolCribText;
            }

            set
            {
                if (value != _toolCribText)
                {
                    _toolCribText = value;
                    OnPropertyChanged(nameof(ToolCribText));
                }
            }
        }

        int? _toolCribID;
        public int? ToolCribID
        {
            get
            {
                return _toolCribID;
            }

            set
            {
                if (value != _toolCribID)
                {
                    _toolCribID = value;
                    OnPropertyChanged(nameof(ToolCribID));
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


        string _toolNumberText;
        public string ToolNumberText
        {
            get
            {
                return _toolNumberText;
            }

            set
            {
                if (value != _toolNumberText)
                {
                    _toolNumberText = value;
                    OnPropertyChanged(nameof(ToolNumberText));
                }
            }
        }

        int? _toolID;
        public int? ToolID
        {
            get
            {
                return _toolID;
            }

            set
            {
                if (value != _toolID)
                {
                    _toolID = value;
                    OnPropertyChanged(nameof(ToolID));
                }
            }
        }



        #endregion


        #endregion





        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);

        public ICommand ToolCribCommand => new AsyncCommand(ShowToolCrib);
        public ICommand ToolNumberCommand => new AsyncCommand(ShowToolNumber);

        public ICommand ScanCommand => new AsyncCommand(SearchToolNumber);

        //Save Command
        public ICommand SaveToolCommand => new AsyncCommand(SaveTool);

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
                }



              //  FormLoadInputForWorkorder = await _formLoadInputService.GetFormLoadInputForBarcode(UserID, AppSettings.WorkorderDetailFormName);
                await SetTitlesPropertiesForPage();
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

        public AddNewToolViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IWorkorderService workorderService, IToolService ToolService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _workorderService = workorderService;
            _toolService = ToolService;
        }

        public async Task SetTitlesPropertiesForPage()
        {
            try
            {

               
                    PageTitle = WebControlTitle.GetTargetNameByTitleName("Tools");
                    WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                    LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                    ToolCribTitle = WebControlTitle.GetTargetNameByTitleName("ToolCribName");
                    ToolNumbertitle = WebControlTitle.GetTargetNameByTitleName("ToolNumber");
                    GoTitle = WebControlTitle.GetTargetNameByTitleName("Go");
                    ScanTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                    SearchButtonTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
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


        public async Task ShowToolCrib()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                //OperationInProgress = true;
                IsPickerDataRequested = true;

                await NavigationService.NavigateToAsync<ToolCribListSelectionPageViewModel>(new TargetNavigationData() { }); //Pass the control here
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

        public async Task ShowToolNumber()
        {
            try
            {
                if (this.ToolCribID == null)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleaseselecttoolcrib"), 2000);
                    return;
                }
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

                //OperationInProgress = true;
                IsPickerDataRequested = true;
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.ToolCribID = this.ToolCribID;
                tnobj.WorkOrderId = this.WorkorderID;
                await NavigationService.NavigateToAsync<ToolNumberListSelectionPageViewModel>(tnobj);
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



        public async Task SaveTool()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));

              //  OperationInProgress = true;

                if (ToolCribID == null)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Toolcribismandatory"));
                    return;
                }
                else if (ToolID == null)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Toolnumberismandatory"));
                    return;
                }
                var Tool = new WorkOrderTool();
                Tool.ToolID = ToolID;
                Tool.WorkOrderID = WorkorderID;

                #endregion


                var yourobject = new workOrderWrapper
                {
                    tool = Tool,
                    



                };



                var response = await _toolService.CreateWorkOrderTool(yourobject);
                if (response != null && bool.Parse(response.servicestatus))
                {

                  
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Toolsuccessfullyadded"), 2000);
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
                        MessagingCenter.Subscribe<object>(this, MessengerKeys.ToolCribRequested, OnToolCribRequested);

                        //Retrive ToolNumber
                        MessagingCenter.Subscribe<object>(this, MessengerKeys.ToolNumberRequested, OnToolNumberRequested);

                        

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
        private void OnToolCribRequested(object obj)
        {

            if (obj != null)
            {
                this.ToolNumberText = "";
                var toolcrib = obj as ToolTipCrib;
                this.ToolCribID = toolcrib.ToolCribID;
                this.ToolCribText = ShortString.shorten(toolcrib.ToolCribName);
            }


        }
        public async Task SearchToolNumber()
        {

            try
            {
                if (this.ToolCribID == null)
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Pleaseselecttoolcrib"), 2000);
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

                else
                {
                    //reset pageno. and start search again.
                    await GetToolNumberFromScan();

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
        private void OnToolNumberRequested(object obj)
        {

            if (obj != null)
            {

                var toolNumber = obj as ToolLookUp;
                this.ToolID = toolNumber.ToolID;
                this.ToolNumberText = ShortString.shorten(toolNumber.ToolNumber);
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
                await GetToolNumberFromScan();


            });

        }
        private async Task GetToolNumberFromScan()
        {
            var duplicatetool= await _toolService.CheckDupliacateTool(WorkorderID.ToString(),this.SearchText);
            if (duplicatetool.workOrderWrapper.IsToolExistsInToolCrib)
            {

                DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("ThistoolisalreadyAddedinthisWorkOrder"), 2000);
                return;
            }
            var ToolDetailsFromScan= await _toolService.GetToolNumberDetailFromScan(WorkorderID.ToString(),UserID,ToolCribID.ToString(),"0","0",this.SearchText);
            if (ToolDetailsFromScan.workOrderWrapper.toolLookUp == null)
            {
                DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Thistoolnumberdoesnotexist"), 2000);
                return;
            }
            var FinalToolDetailsFromScan = ToolDetailsFromScan.workOrderWrapper.toolLookUp.Find(i => i.ToolNumber.ToLower() == this.SearchText.ToLower());
            if (FinalToolDetailsFromScan == null)
            {
               return;
            }
            else
            {
              
                this.ToolNumberText = FinalToolDetailsFromScan.ToolNumber;
                ToolID = FinalToolDetailsFromScan.ToolID;

            }
        }
     

       


    }
}
