using Acr.UserDialogs;

using ProteusMMX.Helpers;
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
using ProteusMMX.ViewModel.Asset;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.Views.Common;
using Rg.Plugins.Popup.Services;
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

namespace ProteusMMX.ViewModel.ServiceRequest
{
    public class ServiceRequestListingPageViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {


        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly IServiceRequestModuleService _serviceRequestService;

        protected readonly IWorkorderService _workorderService;

        #endregion

        #region Properties

        ServiceOutput _formControlsAndRights;
        public ServiceOutput FormControlsAndRights
        {
            get { return _formControlsAndRights; }
            set
            {
                if (value != _formControlsAndRights)
                {
                    _formControlsAndRights = value;

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

        int _serviceRequestID;
        public int ServiceRequestID
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
                    OnPropertyChanged("RequestNumber");
                }
            }
        }

        string _priority;
        public string Priority
        {
            get
            {
                return _priority;
            }

            set
            {
                if (value != _priority)
                {
                    _priority = value;
                    OnPropertyChanged("Priority");
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
                    OnPropertyChanged("Description");
                }
            }
        }






        #endregion

        #region Dialog Actions Titles

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
                    SearchText_TextChanged();
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
        string _createServiceRequest = "";
        public string CreateServiceRequest
        {
            get
            {
                return _createServiceRequest;
            }

            set
            {
                if (value != _createServiceRequest)
                {
                    _createServiceRequest = value;
                    OnPropertyChanged(nameof(CreateServiceRequest));
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
        #endregion

        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);
        public ICommand AddNewServiceCommand => new AsyncCommand(AddNewService);

        public ICommand ServiceRequestSelectedCommand => new Command<ServiceRequests>(OnSelectServiceRequestsync);

        public ICommand ScanCommand => new AsyncCommand(SearchServiceRequest);

        #endregion

        #region RightsProperties

        string Create;
        string Edit;

        string Accept;
        string Decline;

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
        bool _TotalRecordForPhone = false;
        public bool TotalRecordForPhone
        {
            get
            {
                return _TotalRecordForPhone;
            }

            set
            {
                if (value != _TotalRecordForPhone)
                {
                    _TotalRecordForPhone = value;
                    OnPropertyChanged(nameof(TotalRecordForPhone));
                }
            }
        }

        bool _TotalRecordForTab = false;
        public bool TotalRecordForTab
        {
            get
            {
                return _TotalRecordForTab;
            }

            set
            {
                if (value != _TotalRecordForTab)
                {
                    _TotalRecordForTab = value;
                    OnPropertyChanged(nameof(TotalRecordForTab));
                }
            }
        }

        ObservableCollection<ServiceRequests> _serviceRequestCollection = new ObservableCollection<ServiceRequests>();

        public ObservableCollection<ServiceRequests> ServiceRequestCollection
        {
            get
            {
                return _serviceRequestCollection;
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

                }

                OperationInProgress = true;
                await SetTitlesPropertiesForPage();
                await GetServiceRequestControlRights();
                if (Application.Current.Properties.ContainsKey("CreateServiceRequestKey"))
                {
                    var CreateRights = Application.Current.Properties["CreateServiceRequestKey"].ToString();
                    if (CreateRights != null)
                    {
                        Create = CreateRights.ToString();

                    }
                }
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

                if (Device.Idiom == TargetIdiom.Phone)
                {
                    this.TotalRecordForPhone = true;
                }
                else
                {
                    this.TotalRecordForTab = true;
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
        public ServiceRequestListingPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IServiceRequestModuleService serviceReqService, IWorkorderService workorderService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _serviceRequestService = serviceReqService;
            _workorderService = workorderService;
        }

        public async Task GetServiceRequestControlRights()
        {
            try
            {
                ServiceOutput FormControlsAndRightsForDetails = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "ServiceRequest", "Details");
                ServiceOutput FormControlsAndRightsForButton = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "ServiceRequest", "ServiceRequest");
                ServiceOutput FormControlsAndRightsForOperatorTag = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "ServiceRequest", "OperatorTag");
                ServiceOutput FormControlsAndRightsForMaintenanceTag = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "ServiceRequest", "MaintenanceTag");
                ServiceOutput FormControlsAndRightsForSHETag = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "ServiceRequest", "SHETag");

                if (FormControlsAndRightsForButton != null && FormControlsAndRightsForButton.lstModules != null && FormControlsAndRightsForButton.lstModules.Count > 0)
                {
                    var ServiceRequestModule = FormControlsAndRightsForButton.lstModules[0];
                    if (ServiceRequestModule.ModuleName == "ServiceRequests") //ModuleName can't be  changed in service
                    {
                        if (ServiceRequestModule.lstSubModules != null && ServiceRequestModule.lstSubModules.Count > 0)
                        {
                            var ServiceRequestSubModule = ServiceRequestModule.lstSubModules[0];
                            if (ServiceRequestSubModule.listControls != null && ServiceRequestSubModule.listControls.Count > 0)
                            {

                                try
                                {
                                    Application.Current.Properties["AcceptKey"] = ServiceRequestSubModule.listControls.FirstOrDefault(i => i.ControlName == "Accept").Expression;
                                    Application.Current.Properties["DEclineKey"] = ServiceRequestSubModule.listControls.FirstOrDefault(i => i.ControlName == "Decline").Expression;
                                    Application.Current.Properties["CreateServiceRequestKey"] = ServiceRequestSubModule.listControls.FirstOrDefault(i => i.ControlName == "New").Expression;
                                    Application.Current.Properties["EditServiceRequestKey"] = ServiceRequestSubModule.listControls.FirstOrDefault(i => i.ControlName == "Edit").Expression;


                                }
                                catch (Exception ex)
                                {


                                }





                            }



                        }
                    }
                    var ServiceRequestModuleAttachment = FormControlsAndRightsForButton.lstModules[2];
                    if (ServiceRequestModuleAttachment.ModuleName == "Attachments") //ModuleName can't be  changed in service
                    {
                        if (ServiceRequestModuleAttachment.lstSubModules != null && ServiceRequestModuleAttachment.lstSubModules.Count > 0)
                        {
                            var ServiceRequestSubModule = ServiceRequestModuleAttachment.lstSubModules[0];
                            if (ServiceRequestSubModule.listControls != null && ServiceRequestSubModule.listControls.Count > 0)
                            {
                                try
                                {
                                    Application.Current.Properties["CreateSRAttachment"] = ServiceRequestSubModule.listControls.FirstOrDefault(i => i.ControlName == "Add").Expression;
                                    Application.Current.Properties["RemoveSRAttachment"] = ServiceRequestSubModule.listControls.FirstOrDefault(i => i.ControlName == "Remove").Expression;
                                    Application.Current.Properties["AttachmentFiles"] = ServiceRequestSubModule.listControls.FirstOrDefault(i => i.ControlName == "Attach").Expression;
                                    Application.Current.Properties["SRAttachmentTabKey"] = ServiceRequestModuleAttachment.Expression;
                                }
                                catch (Exception ex)
                                {


                                }

                            }
                        }
                    }
                }

                if (FormControlsAndRightsForDetails != null && FormControlsAndRightsForDetails.lstModules != null && FormControlsAndRightsForDetails.lstModules.Count > 0)
                {
                    var ServiceRequestModule = FormControlsAndRightsForDetails.lstModules[0];
                    if (ServiceRequestModule.ModuleName == "Details") //ModuleName can't be  changed in service
                    {
                        if (ServiceRequestModule.lstSubModules != null && ServiceRequestModule.lstSubModules.Count > 0)
                        {
                            var ServiceRequestSubModule = ServiceRequestModule.lstSubModules[0];
                            if (ServiceRequestSubModule.listControls != null && ServiceRequestSubModule.listControls.Count > 0)
                            {


                                try
                                {
                                    Application.Current.Properties["PriorityTabKey"] = ServiceRequestSubModule.listControls.FirstOrDefault(i => i.ControlName == "PriorityID").Expression;
                                    Application.Current.Properties["ServiceRequestAdditionalDetailsKey"] = ServiceRequestSubModule.listControls.FirstOrDefault(i => i.ControlName == "AdditionalDetails").Expression;
                                    Application.Current.Properties["ServiceRequestDetailsControls"] = ServiceRequestSubModule;
                                    Application.Current.Properties["ServiceRequestTarget"] = ServiceRequestSubModule.listControls.FirstOrDefault(i => i.ControlName == "AssetID").Expression; ;
                                }
                                catch (Exception ex)
                                {


                                }

                            }



                        }
                    }
                }

                if (FormControlsAndRightsForSHETag != null && FormControlsAndRightsForSHETag.lstModules != null && FormControlsAndRightsForSHETag.lstModules.Count > 0)
                {
                    var ServiceRequestModuleSHETAg = FormControlsAndRightsForSHETag.lstModules[0];
                    if (ServiceRequestModuleSHETAg.ModuleName == "SHETagDetail") //ModuleName can't be  changed in service
                    {
                        if (ServiceRequestModuleSHETAg.lstSubModules != null && ServiceRequestModuleSHETAg.lstSubModules.Count > 0)
                        {
                            var ServiceRequestSubModuleSheTAg = ServiceRequestModuleSHETAg.lstSubModules[0];
                            if (ServiceRequestSubModuleSheTAg.listControls != null && ServiceRequestSubModuleSheTAg.listControls.Count > 0)
                            {


                                try
                                {
                                    Application.Current.Properties["SHETagTypeKey"] = ServiceRequestSubModuleSheTAg.listControls.FirstOrDefault(i => i.ControlName == "TagType").Expression;

                                }
                                catch (Exception ex)
                                {


                                }


                            }



                        }
                    }
                }

                if (FormControlsAndRightsForMaintenanceTag != null && FormControlsAndRightsForMaintenanceTag.lstModules != null && FormControlsAndRightsForMaintenanceTag.lstModules.Count > 0)
                {
                    var ServiceRequestModuleSHETAg = FormControlsAndRightsForMaintenanceTag.lstModules[0];
                    if (ServiceRequestModuleSHETAg.ModuleName == "MaintenanceTagDetail") //ModuleName can't be  changed in service
                    {
                        if (ServiceRequestModuleSHETAg.lstSubModules != null && ServiceRequestModuleSHETAg.lstSubModules.Count > 0)
                        {
                            var ServiceRequestSubModuleSheTAg = ServiceRequestModuleSHETAg.lstSubModules[0];
                            if (ServiceRequestSubModuleSheTAg.listControls != null && ServiceRequestSubModuleSheTAg.listControls.Count > 0)
                            {


                                try
                                {
                                    Application.Current.Properties["MaintenanceTagTypeKey"] = ServiceRequestSubModuleSheTAg.listControls.FirstOrDefault(i => i.ControlName == "TagType").Expression;

                                }
                                catch (Exception ex)
                                {


                                }

                            }



                        }
                    }
                }

                if (FormControlsAndRightsForOperatorTag != null && FormControlsAndRightsForOperatorTag.lstModules != null && FormControlsAndRightsForOperatorTag.lstModules.Count > 0)
                {
                    var ServiceRequestModuleSHETAg = FormControlsAndRightsForOperatorTag.lstModules[0];
                    if (ServiceRequestModuleSHETAg.ModuleName == "OperatorTagDetail") //ModuleName can't be  changed in service
                    {
                        if (ServiceRequestModuleSHETAg.lstSubModules != null && ServiceRequestModuleSHETAg.lstSubModules.Count > 0)
                        {
                            var ServiceRequestSubModuleSheTAg = ServiceRequestModuleSHETAg.lstSubModules[0];
                            if (ServiceRequestSubModuleSheTAg.listControls != null && ServiceRequestSubModuleSheTAg.listControls.Count > 0)
                            {

                                try
                                {
                                    Application.Current.Properties["OperatorTagTypeKey"] = ServiceRequestSubModuleSheTAg.listControls.FirstOrDefault(i => i.ControlName == "TagType").Expression;
                                }
                                catch (Exception ex)
                                {


                                }



                            }



                        }
                    }
                }
            }
            catch (Exception ex)
            {


            }

        }

        public async Task SetTitlesPropertiesForPage()
        {


            PageTitle = WebControlTitle.GetTargetNameByTitleName("ServiceRequest");
            WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
            LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
            CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
            SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
            RequestNumber = WebControlTitle.GetTargetNameByTitleName("RequestNumber");
            Priority = WebControlTitle.GetTargetNameByTitleName("Priority");
            Description = WebControlTitle.GetTargetNameByTitleName("Description");
            CreateServiceRequest = WebControlTitle.GetTargetNameByTitleName("CreateServiceRequest");
            SearchPlaceholder = WebControlTitle.GetTargetNameByTitleName("SearchOrScanServiceRequest");
            GoTitle = WebControlTitle.GetTargetNameByTitleName("Go");
            ScanTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
            SearchButtonTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
            TotalRecordTitle = WebControlTitle.GetTargetNameByTitleName("TotalRecords");
            SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");





        }
        public async Task ShowActions()
        {
            try
            {
                if (Create == "E")
                {
                    var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() { CreateServiceRequest, LogoutTitle });

                    if (response == LogoutTitle)
                    {
                        await _authenticationService.LogoutAsync();
                        await NavigationService.NavigateToAsync<LoginPageViewModel>();
                        await NavigationService.RemoveBackStackAsync();
                    }

                    if (response == CreateServiceRequest)
                    {
                        //TargetNavigationData tnobj = new TargetNavigationData();

                        //tnobj.WorkOrderId = this.WorkorderID;
                        await NavigationService.NavigateToAsync<CreateServiceRequestViewModel>();

                    }
                }

                else if (Create == "V")
                {
                    var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() { CreateServiceRequest, LogoutTitle });

                    if (response == LogoutTitle)
                    {
                        await _authenticationService.LogoutAsync();
                        await NavigationService.NavigateToAsync<LoginPageViewModel>();
                        await NavigationService.RemoveBackStackAsync();
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

        public async Task AddNewService()
        {
            try
            {
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                await Task.Delay(1000);
                if (Create == "E")
                {

                    await NavigationService.NavigateToAsync<CreateServiceRequestViewModel>();
                }

                else if (Create == "V")
                {
                    

                }
            }
            catch (Exception )
            {
                UserDialogs.Instance.HideLoading();
            }

            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }


        public async Task GetServiceRequestAuto()
        {
            PageNumber++;
            await GetServiceRequest();
        }
        async Task GetServiceRequest()
        {
            try
            {
                OperationInProgress = true;
                var ServiceRequestResponse = await _serviceRequestService.GetServiceRequests(UserID, "0", "0");
                if (ServiceRequestResponse != null && ServiceRequestResponse.serviceRequestWrapper != null
                    && ServiceRequestResponse.serviceRequestWrapper.serviceRequests != null && ServiceRequestResponse.serviceRequestWrapper.serviceRequests.Count > 0)
                {

                    var servicerequest = ServiceRequestResponse.serviceRequestWrapper.serviceRequests;
                    await AddSRinServiceRequestCollection(servicerequest);
                    TotalRecordCount = ServiceRequestResponse.serviceRequestWrapper.serviceRequests.Count;

                }
                else
                {
                    TotalRecordCount = ServiceRequestResponse.serviceRequestWrapper.serviceRequests.Count; ;
                }


            }
            catch (Exception ex)
            {
                TotalRecordCount = 0;

                OperationInProgress = false;
            }

            finally
            {
                OperationInProgress = false;
            }
        }

        async Task GetServiceRequestFromSearchBar()
        {
            try
            {
                OperationInProgress = true;
                var ServiceRequestResponse = await _serviceRequestService.ServiceRequestByServiceRequestNumber(UserID, this.SearchText);
                if (ServiceRequestResponse.serviceRequestWrapper.serviceRequests == null || ServiceRequestResponse.serviceRequestWrapper.serviceRequests.Count == 0)
                {
                    TotalRecordCount = 0;
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("ThisServiceRequestdoesnotexist"), 2000);
                }
                if (ServiceRequestResponse != null && ServiceRequestResponse.serviceRequestWrapper != null
                    && ServiceRequestResponse.serviceRequestWrapper.serviceRequests != null && ServiceRequestResponse.serviceRequestWrapper.serviceRequests.Count > 0)
                {

                    var servicerequest = ServiceRequestResponse.serviceRequestWrapper.serviceRequests;
                    await AddSRinServiceRequestCollection(servicerequest);
                    TotalRecordCount = ServiceRequestResponse.serviceRequestWrapper.serviceRequests.Count;
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
        private async Task AddSRinServiceRequestCollection(List<ServiceRequests> servicereq)
        {
            if (servicereq != null && servicereq.Count > 0)
            {
                foreach (var item in servicereq)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _serviceRequestCollection.Add(item);
                        OnPropertyChanged(nameof(ServiceRequestCollection));
                    });



                }

            }
        }


        private async Task RemoveAllServiceRequestFromCollection()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ServiceRequestCollection.Clear();
                OnPropertyChanged(nameof(ServiceRequestCollection));
            });



        }

        public async Task SearchServiceRequest()
        {

            try
            {
                OperationInProgress = true;


                //   CrossBleAdapter.Current.SetAdapterState(true);
                //var state = ble.State;
                //ble.StateChanged += (s, e) =>
                //{

                //};
                //var adapter = CrossBluetoothLE.Current.Adapter;
                //adapter.ena
                //var systemDevices = adapter.GetSystemConnectedOrPairedDevices();
                //foreach (var device in systemDevices)
                //{

                //    await adapter.ConnectToDeviceAsync(device);
                //    var services = await device.GetServicesAsync();
                //    // var characteristics = await services.GetCharacteristicsAsync();
                //    foreach (var item in services)
                //    {
                //        var characteristics = item.GetCharacteristicsAsync();
                //        foreach (var item1 in characteristics.Result)
                //        {
                //            var data = item1.Value;
                //        }
                //    }
                //    //var data = await characteristic.ReadAsync();
                //    //data[0] = 0x13;
                //    //await characteristic.WriteAsync(data);

                //    //characteristic.ValueUpdated += (s, e) =>
                //    //{
                //    //    Debug.WriteLine("New value: {0}", e.Characteristic.Value);
                //    //};
                //    //characteristic.StartUpdates();
                //}

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
                    await RefillServiceRequestCollection();

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
                await RefillServiceRequestCollection();


            });

        }
        private async Task RefillServiceRequestCollection()
        {
            PageNumber = 1;
            await RemoveAllServiceRequestFromCollection();
            await GetServiceRequestFromSearchBar();
        }

        private void SearchText_TextChanged()
        {

            try
            {
                if (SearchText == null || SearchText.Length == 0)
                {
                    SearchButtonTitle = ScanTitle;
                }

                else if (SearchText.Length > 0)
                {
                    SearchButtonTitle = GoTitle;
                }
            }
            catch (Exception ex)
            {


            }
        }

        private async void OnSelectServiceRequestsync(ServiceRequests item)
        {
            if (Edit == "E" || Edit == "V")
            {
                if (item != null)
                {
                    UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                    //OperationInProgress = true;
                    TargetNavigationData tnobj = new TargetNavigationData();
                    tnobj.ServiceRequestID = item.ServiceRequestID;
                    tnobj.RequestNumber = item.RequestNumber;
                    await NavigationService.NavigateToAsync<ServiceRequestTabbedPageViewModel>(item);
                    //OperationInProgress = false;
                    UserDialogs.Instance.HideLoading();
                }
            }
        }
        public async Task AcceptServiceRequest(ServiceRequests serviceRequestItem)
        {
            try
            {
                var ServiceRequestID = serviceRequestItem.ServiceRequestID;
                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                //OperationInProgress = true;





                //Check if Service Request Has Target///////////////
                var ServiceRequestResponse = await _serviceRequestService.GetServiceRequestDetailByServiceRequestID(ServiceRequestID.ToString(), AppSettings.User.UserID.ToString());
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

                        Application.Current.Properties["SRID"] = ServiceRequestID;
                        Application.Current.Properties["ServiceRequestNavigation"] = "False";


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
                                ServiceRequestID = ServiceRequestID,

                            },

                        };


                        var response = await _serviceRequestService.AcceptServiceRequest(yourobject);

                        if (Boolean.Parse(response.servicestatus))
                        {
                            await RemoveAllServiceRequestFromCollection();
                            await GetServiceRequest();
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
                            ServiceRequestID = ServiceRequestID,

                        },

                    };


                    var response = await _serviceRequestService.AcceptServiceRequest(yourobject);

                    if (Boolean.Parse(response.servicestatus))
                    {
                        await RemoveAllServiceRequestFromCollection();
                        await GetServiceRequest();
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
                // OperationInProgress = false;
            }
        }
        public async Task DeclineServiceRequest(ServiceRequests serviceRequestItem)
        {
            try
            {
                var ServiceRequestID = serviceRequestItem.ServiceRequestID;
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
                // OperationInProgress = false;
            }
        }
        public async Task OnViewAppearingAsync(VisualElement view)
        {


            if (string.IsNullOrWhiteSpace(SearchText))
            {
                await RemoveAllServiceRequestFromCollection();
                await GetServiceRequest();
            }
        }

        public async Task OnViewDisappearingAsync(VisualElement view)
        {
            this.SearchText = "";
        }

        public async Task ReloadPageAfterSerchBoxCancle()
        {
            await RemoveAllServiceRequestFromCollection();
            await GetServiceRequest();
        }
        #endregion
    }
}
