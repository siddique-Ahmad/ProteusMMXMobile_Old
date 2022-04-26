using ProteusMMX.Helpers;
using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
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
    public class WorkorderToolListingPageViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {

        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly IWorkorderService _workorderService;


        #endregion

        #region Properties

        #region Page Properties
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
                    OnPropertyChanged("WelcomeTextTitle");
                }
            }
        }




        #endregion




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

        string _toolName;
        public string ToolName
        {
            get
            {
                return _toolName;
            }

            set
            {
                if (value != _toolName)
                {
                    _toolName = value;
                    OnPropertyChanged("ToolName");
                }
            }
        }

        string _toolNumber;
        public string ToolNumber
        {
            get
            {
                return _toolNumber;
            }

            set
            {
                if (value != _toolNumber)
                {
                    _toolNumber = value;
                    OnPropertyChanged("ToolNumber");
                }
            }
        }

        string _toolCribName;
        public string ToolCribName
        {
            get
            {
                return _toolCribName;
            }

            set
            {
                if (value != _toolCribName)
                {
                    _toolCribName = value;
                    OnPropertyChanged("ToolCribName");
                }
            }
        }

        string _toolSize;
        public string ToolSize
        {
            get
            {
                return _toolSize;
            }

            set
            {
                if (value != _toolSize)
                {
                    _toolSize = value;
                    OnPropertyChanged("ToolSize");
                }
            }
        }
        string _remove;
        public string Remove
        {
            get
            {
                return _remove;
            }

            set
            {
                if (value != _remove)
                {
                    _remove = value;
                    OnPropertyChanged("Remove");
                }
            }
        }

        bool _removeIsEnable = true;
        public bool RemoveIsEnable
        {
            get
            {
                return _removeIsEnable;
            }

            set
            {
                if (value != _removeIsEnable)
                {
                    _removeIsEnable = value;
                    OnPropertyChanged(nameof(RemoveIsEnable));
                }
            }
        }
        bool _removeIsVisible = true;
        public bool RemoveIsVisible
        {
            get
            {
                return _removeIsVisible;
            }

            set
            {
                if (value != _removeIsVisible)
                {
                    _removeIsVisible = value;
                    OnPropertyChanged(nameof(RemoveIsVisible));
                }
            }
        }
        #endregion

        #region Dialog Actions Titles

        string CreateTool;
        string DeleteTool;

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



        string _addTool = "";
        public string AddTool
        {
            get
            {
                return _addTool;
            }

            set
            {
                if (value != _addTool)
                {
                    _addTool = value;
                    OnPropertyChanged(nameof(AddTool));
                }
            }
        }

        string _toolSizeTitle = "";
        public string ToolSizeTitle
        {
            get
            {
                return _toolSizeTitle;
            }

            set
            {
                if (value != _toolSizeTitle)
                {
                    _toolSizeTitle = value;
                    OnPropertyChanged(nameof(ToolSizeTitle));
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

        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);
        public ICommand NewToolbarCommand => new AsyncCommand(AddNewToolbar);
        public ICommand ScanCommand => new AsyncCommand(ScanTools);




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


        ObservableCollection<WorkOrderTool> _toolsCollection = new ObservableCollection<WorkOrderTool>();

        public ObservableCollection<WorkOrderTool> ToolsCollection
        {
            get
            {
                return _toolsCollection;
            }

        }

        #endregion





        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {



                if (navigationData != null)
                {

                    var navigationParams = navigationData as PageParameters;
                    this.Page = navigationParams.Page;

                    var workorder = navigationParams.Parameter as workOrders;
                    this.WorkorderID = workorder.WorkOrderID;



                }

                OperationInProgress = true;
                await SetTitlesPropertiesForPage();

                if (Application.Current.Properties.ContainsKey("CreateTool"))
                {
                    CreateTool = Application.Current.Properties["CreateTool"].ToString();

                }
                if (Application.Current.Properties.ContainsKey("DeleteTool"))
                {
                    DeleteTool = Application.Current.Properties["DeleteTool"].ToString();

                }

                Application.Current.Properties["DeleteToolKey"] = DeleteTool;


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

        public WorkorderToolListingPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IWorkorderService workorderService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _workorderService = workorderService;
        }

        public async Task SetTitlesPropertiesForPage()
        {

            PageTitle = WebControlTitle.GetTargetNameByTitleName("Tools");
            WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
            LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
            CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
            SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
            ToolName = WebControlTitle.GetTargetNameByTitleName("ToolName");
            ToolNumber = WebControlTitle.GetTargetNameByTitleName("ToolNumber");
            ToolCribName = WebControlTitle.GetTargetNameByTitleName("ToolCribName");
            AddTool = WebControlTitle.GetTargetNameByTitleName("AddTool");
            ToolSize = WebControlTitle.GetTargetNameByTitleName("ToolSize");
            Remove = WebControlTitle.GetTargetNameByTitleName("RemoveTool");
            SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");
            TotalRecordTitle = WebControlTitle.GetTargetNameByTitleName("TotalRecords");
            SearchPlaceholder = WebControlTitle.GetTargetNameByTitleName("Search") + WebControlTitle.GetTargetNameByTitleName("Tools");
            // TotalRecordTitle = WebControlTitle.GetTargetNameByTitleName(titles, "TotalRecords");


        }
        public async Task ScanTools()
        {

            try
            {
                OperationInProgress = true;


                #region Barcode Section and Search Section

                if (String.IsNullOrWhiteSpace(this.SearchText))
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
                    await RefillToolsCollection();

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
                await RefillToolsCollection();


            });

        }
        private async Task RemoveAllToolsFromCollection()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ToolsCollection.Clear();
                OnPropertyChanged(nameof(ToolsCollection));
            });



        }
        public async Task RefillToolsCollection()
        {

            PageNumber = 1;
            await RemoveAllToolsFromCollection();
            await GetWorkorderToolsFromSearchBar();
        }
        public async Task ShowActions()
        {
            try
            {
                if (CreateTool == "E")
                {
                    var response = await DialogService.SelectActionAsync("", SelectTitle, CancelTitle, new ObservableCollection<string>() {  LogoutTitle });

                    if (response == LogoutTitle)
                    {
                        await _authenticationService.LogoutAsync();
                        await NavigationService.NavigateToAsync<LoginPageViewModel>();
                        await NavigationService.RemoveBackStackAsync();
                    }

                    //if (response == AddTool)
                    //{
                    //    TargetNavigationData tnobj = new TargetNavigationData();

                    //    tnobj.WorkOrderId = this.WorkorderID;
                    //    await NavigationService.NavigateToAsync<AddNewToolViewModel>(tnobj);

                    //}
                }
                else if (CreateTool == "V")
                {
                    var response = await DialogService.SelectActionAsync("", SelectTitle, CancelTitle, new ObservableCollection<string>() { AddTool, LogoutTitle });

                    if (response == LogoutTitle)
                    {
                        await _authenticationService.LogoutAsync();
                        await NavigationService.NavigateToAsync<LoginPageViewModel>();
                        await NavigationService.RemoveBackStackAsync();
                    }


                }
                else
                {
                    var response = await DialogService.SelectActionAsync("", SelectTitle, CancelTitle, new ObservableCollection<string>() { LogoutTitle });

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

        public async Task AddNewToolbar()
        {
            try
            {
                if (CreateTool == "E")
                {
                    TargetNavigationData tnobj = new TargetNavigationData();

                    tnobj.WorkOrderId = this.WorkorderID;
                    await NavigationService.NavigateToAsync<AddNewToolViewModel>(tnobj);
                }
                else if (CreateTool == "V")
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

        //private async Task RefillWorkorderToolCollection()
        //{
        //    PageNumber = 1;
        //    await RemoveAllWorkorderToolsFromCollection();
        //    await GetWorkorderTools();
        //}










        public async Task GetWorkorderToolsAuto()
        {
            // PageNumber++;
            await GetWorkorderTools();
        }

        async Task GetWorkorderToolsFromSearchBar()
        {
            try
            {
                OperationInProgress = true;
                var workordersResponse = await _workorderService.GetWorkorderTools(WorkorderID.ToString(), this.SearchText);
                if (workordersResponse != null && workordersResponse.workOrderWrapper != null
                    && workordersResponse.workOrderWrapper.tools != null && workordersResponse.workOrderWrapper.tools.Count > 0)
                {

                    var workordertools = workordersResponse.workOrderWrapper.tools;
                    await AddWorkorderToolsInWorkorderCollection(workordertools);
                    TotalRecordCount = workordersResponse.workOrderWrapper.tools.Count;

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
        async Task GetWorkorderTools()
        {
            try
            {
                OperationInProgress = true;
                var workordersResponse = await _workorderService.GetWorkorderTools(WorkorderID.ToString(), null);
                if (workordersResponse != null && workordersResponse.workOrderWrapper != null
                    && workordersResponse.workOrderWrapper.tools != null && workordersResponse.workOrderWrapper.tools.Count > 0)
                {

                    var workordertools = workordersResponse.workOrderWrapper.tools;
                    await AddWorkorderToolsInWorkorderCollection(workordertools);
                    TotalRecordCount = workordersResponse.workOrderWrapper.tools.Count;

                }
                else
                {
                    TotalRecordCount = 0;
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

        private async Task AddWorkorderToolsInWorkorderCollection(List<WorkOrderTool> tools)
        {
            if (tools != null && tools.Count > 0)
            {
                foreach (var item in tools)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _toolsCollection.Add(item);
                        OnPropertyChanged(nameof(ToolsCollection));
                    });



                }

            }
        }


        private async Task RemoveAllWorkorderToolsFromCollection()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ToolsCollection.Clear();
                OnPropertyChanged(nameof(ToolsCollection));
            });



        }

        public async Task RemoveTool(int? toolID)
        {
            try
            {
                var workordertoolID = toolID;

                OperationInProgress = true;







                var yourobject = new workOrderWrapper
                {
                    tool = new WorkOrderTool
                    {

                        WorkOrderToolID = workordertoolID
                    },



                };


                var response = await _workorderService.RemoveTool(yourobject);

                if (Boolean.Parse(response.servicestatus))
                {
                    RemoveAllWorkorderToolsFromCollection();
                    await GetWorkorderTools();
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

        public async Task OnViewAppearingAsync(VisualElement view)
        {
            if (DeleteTool == "V")
            {
                RemoveIsEnable = false;
            }
            if (DeleteTool == "N")
            {
                RemoveIsVisible = false;
            }
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                await RemoveAllWorkorderToolsFromCollection();
                await GetWorkorderTools();
            }
        }

        public async Task OnViewDisappearingAsync(VisualElement view)
        {
            this.SearchText = "";
        }


        #endregion

    }
}
