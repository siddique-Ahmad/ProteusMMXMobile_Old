using Acr.UserDialogs;
using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Translations;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Utils;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System.Text.RegularExpressions;
using ProteusMMX.Helpers.Storage;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using Microsoft.AppCenter.Crashes;
using ProteusMMX.Helpers.Attachment;
using System.IO;
using ProteusMMX.DependencyInterface;

namespace ProteusMMX.ViewModel
{
    public class LoginPageViewModel : ViewModelBase
    {
        #region Fields

        protected readonly IAuthenticationService _authenticationService;
        protected readonly IWebControlTitlesService _webControlTitlesService;
        protected readonly IFormLoadInputService _formLoadInputService;
        protected readonly IWorkorderService _workorderService;

        #endregion
        private static ISettings Settings => CrossSettings.Current;
        #region Properties
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


        bool _siteUrlLabelVisibility = true;
        public bool SiteUrlLabelVisibility
        {
            get
            {
                return _siteUrlLabelVisibility;
            }

            set
            {
                if (value != _siteUrlLabelVisibility)
                {
                    _siteUrlLabelVisibility = value;
                    OnPropertyChanged("SiteUrlLabelVisibility");

                }
            }
        }
        bool _siteUrlEntryVisibility = true;
        public bool SiteUrlEntryVisibility
        {
            get
            {
                return _siteUrlEntryVisibility;
            }

            set
            {
                if (value != _siteUrlEntryVisibility)
                {
                    _siteUrlEntryVisibility = value;
                    OnPropertyChanged("SiteUrlEntryVisibility");

                }
            }
        }
        bool _rememberMeVisibility = true;
        public bool RememberMeVisibility
        {
            get
            {
                return _rememberMeVisibility;
            }

            set
            {
                if (value != _rememberMeVisibility)
                {
                    _rememberMeVisibility = value;
                    OnPropertyChanged("RememberMeVisibility");

                }
            }
        }

        bool _copyrightLabelVisibility = true;
        public bool CopyrightLabelVisibility
        {
            get
            {
                return _copyrightLabelVisibility;
            }

            set
            {
                if (value != _copyrightLabelVisibility)
                {
                    _copyrightLabelVisibility = value;
                    OnPropertyChanged("CopyrightLabelVisibility");

                }
            }
        }


        bool _supportLabelVisibility = true;
        public bool SupportLabelVisibility
        {
            get
            {
                return _supportLabelVisibility;
            }

            set
            {
                if (value != _supportLabelVisibility)
                {
                    _supportLabelVisibility = value;
                    OnPropertyChanged("SupportLabelVisibility");

                }
            }
        }
        bool _isLoginCallRiskPage;
        public bool IsLoginCallRiskPage
        {
            get
            {
                return _isLoginCallRiskPage;
            }

            set
            {
                if (value != _isLoginCallRiskPage)
                {
                    _isLoginCallRiskPage = value;
                    OnPropertyChanged("IsLoginCallRiskPage");

                }
            }
        }
        string _pageTitle = "MMX Login";
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

        string _siteUrlLabel = "MMX URL";
        public string SiteUrlLabel
        {
            get
            {
                return _siteUrlLabel;
            }

            set
            {
                if (value != _siteUrlLabel)
                {
                    _siteUrlLabel = value;
                    OnPropertyChanged("SiteUrlLabel");
                }
            }
        }

        string _siteUrl = "https://proteusmmx.com/";
        public string SiteUrl
        {
            get
            {
                return _siteUrl;
            }

            set
            {
                if (value != _siteUrl)
                {
                    _siteUrl = value;
                    OnPropertyChanged("SiteUrl");
                }
            }
        }

        string _userNameLabel = "UserName";
        public string UserNameLabel
        {
            get
            {
                return _userNameLabel;
            }

            set
            {
                if (value != _userNameLabel)
                {
                    _userNameLabel = value;
                    OnPropertyChanged("UserNameLabel");
                }
            }
        }

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

        string _passwordLabel = "Password";
        public string PasswordLabel
        {
            get
            {
                return _passwordLabel;
            }

            set
            {
                if (value != _passwordLabel)
                {
                    _passwordLabel = value;
                    OnPropertyChanged("PasswordLabel");
                }
            }
        }

        bool _loginSwitch = true;
        public bool LoginSwitch
        {
            get
            {
                return _loginSwitch;
            }

            set
            {
                if (value != _loginSwitch)
                {
                    _loginSwitch = value;
                    OnPropertyChanged("LoginSwitch");
                }
            }
        }

        bool _labelSwitch = true;
        public bool LabelSwitch
        {
            get
            {
                return _labelSwitch;
            }

            set
            {
                if (value != _labelSwitch)
                {
                    _labelSwitch = value;
                    OnPropertyChanged("LabelSwitch");
                }
            }
        }

        string _password;
        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                if (value != _password)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }

        string _loginButtonLabel = "Login";
        public string LoginButtonLabel
        {
            get
            {
                return _loginButtonLabel;
            }

            set
            {
                if (value != _loginButtonLabel)
                {
                    _loginButtonLabel = value;
                    OnPropertyChanged("LoginButtonLabel");
                }
            }
        }

        string _copyrightLabel = "Copyright @ Eagle Technology Inc.";
        public string CopyrightLabel
        {
            get
            {
                return _copyrightLabel;
            }

            set
            {
                if (value != _copyrightLabel)
                {
                    _copyrightLabel = value;
                    OnPropertyChanged("CopyrightLabel");
                }
            }
        }

        string _supportLabel = "Make sure to have latest MMX mobile service. Questions? Email/Call Tech support.";
        public string SupportLabel
        {
            get
            {
                return _supportLabel;
            }

            set
            {
                if (value != _supportLabel)
                {
                    _supportLabel = value;
                    OnPropertyChanged("SupportLabel");
                }
            }
        }
        string RememberMeSwitchValue = "";
        bool _rememberMe;
        public bool RememberMe
        {
            get
            {
                return _rememberMe;
            }

            set
            {
                if (value != _rememberMe)
                {
                    _rememberMe = value;
                    OnPropertyChanged("RememberMe");
                }
            }
        }
        bool _RememberMeSwitch = true;
        public bool RememberMeSwitch
        {
            get
            {
                return _RememberMeSwitch;
            }

            set
            {
                if (value != _RememberMeSwitch)
                {
                    _RememberMeSwitch = value;
                    OnPropertyChanged("RememberMeSwitch");
                }
            }
        }

        #endregion

        #region Commands
        public ICommand LoginCommand => new AsyncCommand(LoginAsync);
        #endregion
        public override async Task InitializeAsync(object navigationData)
        {

            try
            {


              

                if (navigationData != null)
                {


                    var navigationParams = navigationData as TargetNavigationData;
                    WorkorderID = navigationParams.WorkOrderId;





                }


                if (Device.RuntimePlatform == Device.iOS)
                {
                    OperationInProgress = true;
                }
                else
                {
                   
                    UserDialogs.Instance.ShowLoading("Please wait..loading all data");
                }



                if (Application.Current.Properties.ContainsKey("RememberMeSwitchKey"))
                {
                    RememberMeSwitchValue = Application.Current.Properties["RememberMeSwitchKey"].ToString();
                }

                var FDAKey = await _authenticationService.GetFDAValidationAsync(AppSettings.BaseURL, null);
                if (FDAKey != null)
                {

                    ////Set Company Profile Picture///////
                 
                    string newcompanyprofilebase64 = string.Empty;
                    if (Device.RuntimePlatform == Device.UWP)
                    {
                        string companyprofilebase64 = FDAKey.CompanyProfileLogo;
                        newcompanyprofilebase64 = companyprofilebase64.Replace("data:image/png;base64,", "");
                        if(string.IsNullOrWhiteSpace(newcompanyprofilebase64))
                        {
                            newcompanyprofilebase64 = companyprofilebase64.Replace("data:image/jpeg;base64,", "");
                        }
                        byte[] imgUser = StreamToBase64.StringToByte(newcompanyprofilebase64);
                        MemoryStream stream = new MemoryStream(imgUser);
                        bool isimage = Extension.IsImage(stream);
                        if (isimage == true)
                        {

                            byte[] byteImage = await Xamarin.Forms.DependencyService.Get<IResizeImage>().ResizeImageAndroid(imgUser, 160, 100);
                            AttachmentImageSource = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(byteImage))));



                        }
                    }

                    if (FDAKey.FDAEnable && FDAKey.Signvalue=="True")
                    {

                        SiteUrl = AppSettings.BaseURL;
                        LoginSwitch = false;
                        LabelSwitch = false;
                        return;
                    }
                }

                if (RememberMeSwitchValue == "true" || RememberMeSwitch == true)
                {

                    UserName = AppSettings.UserName;
                    Password = AppSettings.Password;
                    // RememberMeSwitch = AppSettings.RememberMeSwitch;
                    SiteUrl = AppSettings.BaseURL;
                    RememberMeSwitch = true;
                    if (string.IsNullOrWhiteSpace(UserName) && string.IsNullOrWhiteSpace(Password))
                    {
                        RememberMeSwitch = false;
                    }



                    var user = await _authenticationService.UserIsAuthenticatedAndValidAsync(SiteUrl, UserName,Password);
                    if (user != null && user.mmxUser != null)
                    {
                        // user.mmxUser.P


                        string signaturevalidated = SignatureStorage.Storage.Get("FDASignatureUserValidated");
                        if (!string.IsNullOrWhiteSpace(signaturevalidated))
                        {
                            if (signaturevalidated == "True")
                            {
                                Application.Current.Properties["UserNameType"] = "Label";
                            }
                            else
                            {
                                Application.Current.Properties["UserNameType"] = "TextBox";
                            }
                        }

                        // Initialize Translations Property


                        Application.Current.Properties["Password"] = Password;
                        await InitializeTranslations();
                        await GetWorkorderControlRights();
                        await GetServiceRequestControlRights();
                        await GetAssetControlRights();
                        await GetClosedWorkorderandInventoryControlRights();
                        await GetPurchaseOrderControlRights();
                        UserDialogs.Instance.HideLoading();
                        OperationInProgress = false;
                        await NavigationService.NavigateToAsync<DashboardPageViewModel>();
                        await NavigationService.RemoveLastFromBackStackAsync();
                    }

                    else if (RememberMeSwitch == true && RememberMeSwitchValue == "false")
                    {
                        UserDialogs.Instance.HideLoading();
                        OperationInProgress = false;
                        await DialogService.ShowAlertAsync("Please Enter Valid Credential.", "Alert", "OK");

                        return;
                    }

                }
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

        public LoginPageViewModel(IAuthenticationService authenticationService, IWebControlTitlesService webControlTitlesService, IFormLoadInputService formloadservice, IWorkorderService workorderService)
        {
            _authenticationService = authenticationService;
            _webControlTitlesService = webControlTitlesService;
            _formLoadInputService = formloadservice;
            _workorderService = workorderService;
        }
        public async Task LoginAsync()
        {
            try
            {

                ServiceOutput apiVersion = new ServiceOutput();
                UserDialogs.Instance.ShowLoading("Please wait..loading all data");

                if (string.IsNullOrEmpty(SiteUrl))
                {
                    UserDialogs.Instance.HideLoading();

                    // DialogService.ShowToast(, 2000);
                    await DialogService.ShowAlertAsync("Please enter Site Url", "Alert", "OK");
                    return;
                }

                else
                {
                    ServiceOutput httpsscheme = new ServiceOutput();
                   
                    SiteUrl = Regex.Replace(SiteUrl, "[^a-zA-Z0-9-_./:]+", "", RegexOptions.Compiled);
                    var builder = new UriBuilder(SiteUrl);
                    SiteUrl = RemovePortIfDefault(builder.Uri).ToString();

                    bool IsValidEmail = Regex.IsMatch(SiteUrl,
                @"^(http|https)\://|[a-zA-Z0-9\-\.]+\.[a-zA-Z](:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;%\$#\=~])*[^\.\,\)\(\s]$",
                RegexOptions.IgnoreCase);

                    if(IsValidEmail)
                    {
                        apiVersion = await _authenticationService.GetAPIVersion(SiteUrl);
                        
                        if(apiVersion!=null)
                        {
                            //httpsscheme = await _authenticationService.GetFDAValidationAsync(SiteUrl, null);
                            //if (!string.IsNullOrWhiteSpace(httpsscheme.AcknowledgementURLProtocol) && httpsscheme.AcknowledgementURLProtocol.ToLower() == "true")
                            //{
                            //    builder.Scheme = "https";
                            //}
                            //else
                            //{
                            //    builder.Scheme = "http";
                            //}
                            //var result = builder.Uri;
                            //SiteUrl = RemovePortIfDefault(result).ToString();
                            
                        }
                        else
                        {
                            UserDialogs.Instance.HideLoading();

                            await DialogService.ShowAlertAsync("Please verify the site URL.", "Alert", "OK");
                            return;
                        }

                    }
                    else
                    {
                        UserDialogs.Instance.HideLoading();

                        await DialogService.ShowAlertAsync("Please verify the site URL.", "Alert", "OK");
                        return;
                    }

                   
                   
                }


                apiVersion = await _authenticationService.GetAPIVersion(SiteUrl);


                if (apiVersion == null)
                {
                    UserDialogs.Instance.HideLoading();

                    await DialogService.ShowAlertAsync("Please verify the site URL.", "Alert", "OK");
                    return;

                }
               

                AppSettings.BaseURL = SiteUrl;
                AppSettings.APIVersion = apiVersion.APIVersion;

                var user = await _authenticationService.LoginAsync(AppSettings.BaseURL, UserName,Password);
                if (user == null || user.mmxUser == null || Convert.ToBoolean(user.servicestatus) == false)
                {
                    UserDialogs.Instance.HideLoading();

                    await DialogService.ShowAlertAsync("Please Enter Valid Credential.", "Alert", "OK");

                    return;
                }
                if (user.mmxUser.UserLicense == "Web")
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast("Current User Doesnot have Mobile License", 2000);

                    return;
                }
                /// Save the MMXUser the so we can reuse that.
                /// 

                AppSettings.User = user.mmxUser;
                AppSettings.UserName = UserName;
                AppSettings.Password = Password;

                var FDALicenseKey = await _authenticationService.GetFDAValidationAsync(SiteUrl, null);
                if (FDALicenseKey.FDAEnable && FDALicenseKey.Signvalue == "True")
                {

                }
                else
                {
                    if (Application.Current.Properties.ContainsKey("RememberMeSwitchKey"))
                    {
                        RememberMeSwitchValue = Application.Current.Properties["RememberMeSwitchKey"].ToString();
                    }
                    if (RememberMeSwitch == true)
                    {

                        RememberMeSwitchValue = "true";
                        Application.Current.Properties["RememberMeSwitchKey"] = "true";
                    }
                    else
                    {
                        Application.Current.Properties["RememberMeSwitchKey"] = "false";
                        RememberMeSwitchValue = "false";
                    }

                 
                }


                // Initialize Translations Property
                Application.Current.Properties["UserNameType"] = "TextBox";
                SignatureStorage.Storage.Set("FDASignatureUserValidated", "False");
                Application.Current.Properties["Password"] = Password;
                await InitializeTranslations();
                await GetWorkorderControlRights();
                await GetServiceRequestControlRights();
                await GetAssetControlRights();
                await GetPurchaseOrderControlRights();
                await GetClosedWorkorderandInventoryControlRights();
                // await GetWorkorderModuleRights();
                // await GetServiceRequestModuleRights();
                // await GetAssetModuleRights();
                // First push page than remove last.
                UserDialogs.Instance.HideLoading();

                await NavigationService.NavigateToAsync<DashboardPageViewModel>();
                await NavigationService.RemoveLastFromBackStackAsync();
                if (RememberMeSwitch == false)
                {
                    Settings.Remove(nameof(UserName));
                    Settings.Remove(nameof(Password));
                    //Settings.Remove(nameof(SiteUrl));

                    // Settings.Remove(nameof(user));
                }


            }
            catch (Exception ex)
            {
                UserDialogs.Instance.HideLoading();
                OperationInProgress = false;
                Crashes.TrackError(ex);

            }

            finally
            {
                UserDialogs.Instance.HideLoading();
                OperationInProgress = false;
                //   await DialogService.ShowAlertAsync("Please verify the site URL.", "Alert", "OK");

            }
        }
        public static Uri RemovePortIfDefault(Uri uri)
        {
            
                UriBuilder builder = new UriBuilder(uri);
                builder.Port = -1;
                return builder.Uri;
            
        }

        public async Task InitializeTranslations()
        {
            var translations = await _webControlTitlesService.GetWebControlTitles(AppSettings.User.UserID.ToString());

            if (translations != null && translations.listWebControlTitles != null)
            {
                var translationDictionary = AppSettings.Translations;
                translationDictionary.Clear();
                foreach (var item in translations.listWebControlTitles)
                {
                    try
                    {
                        translationDictionary.Add(item.TitleName, item.TargetName);
                    }
                    catch (Exception ex)
                    {
                    }

                }

            }

        }
      
    public async Task GetWorkorderModuleRights()
        {
            FormControlsAndRights = await _formLoadInputService.GetFormControlsAndRights(AppSettings.User.UserID.ToString(), AppSettings.WorkorderModuleName);
            if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
            {
                var workordersModule = FormControlsAndRights.lstModules[0];
                if (workordersModule.ModuleName == "WorkOrders") //ModuleName can't be  changed in service 
                {
                    if (workordersModule.lstSubModules != null && workordersModule.lstSubModules.Count > 0)
                    {

                        var workorderSubModule = workordersModule.lstSubModules.FirstOrDefault(i => i.SubModuleName == "WorkOrders");
                        var Edit = workorderSubModule.Button.FirstOrDefault(i => i.Name == "Edit");

                        Application.Current.Properties["Edit"] = Edit.Expression;
                        Application.Current.Properties["workorderSubModuleListDialogues"] = workorderSubModule;

                        if (workorderSubModule != null)
                        {
                            if (workorderSubModule.Button != null && workorderSubModule.Button.Count > 0)
                            {

                                Application.Current.Properties["CloseWorkorderRightsKey"] = workorderSubModule.Button.FirstOrDefault(i => i.Name == "CompleteAndClose").Expression;
                            }

                            if (workorderSubModule.listDialoges != null && workorderSubModule.listDialoges.Count > 0)
                            {
                                var WorkorderDialog = workorderSubModule.listDialoges.FirstOrDefault(i => i.DialogName == "WorkOrderDialog");
                                if (WorkorderDialog != null && WorkorderDialog.listTab != null && WorkorderDialog.listTab.Count > 0)
                                {
                                    var TaskLabourTab = WorkorderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "TasksandLabor");
                                    var InspectionTab = WorkorderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "WorkOrderInspections");
                                    var ToolsTab = WorkorderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "Tools");
                                    var PartsTab = WorkorderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "Parts");
                                    var AttachmentTab = WorkorderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "Attachments");
                                    Application.Current.Properties["TaskandLabourTabKey"] = TaskLabourTab.Expression;
                                    Application.Current.Properties["InspectionTabKey"] = InspectionTab.Expression;
                                    Application.Current.Properties["ToolsTabKey"] = ToolsTab.Expression;
                                    Application.Current.Properties["PartsTabKey"] = PartsTab.Expression;
                                    Application.Current.Properties["AttachmentTabKey"] = AttachmentTab.Expression;

                                    var DetailsTab = WorkorderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "Details");
                                    var WorkStartDateDetails = DetailsTab.listControls.FirstOrDefault(i => i.ControlName == "WorkStartedDate");
                                    var WorkCompletionDateDetails = DetailsTab.listControls.FirstOrDefault(i => i.ControlName == "CompletionDate");
                                    var AdditionalDetailsTab = WorkorderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "AdditionalDetails");
                                    var CauseTab = WorkorderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "Causes");
                                    var Asset = DetailsTab.listControls.FirstOrDefault(i => i.ControlName == "AssetID");

                                    Application.Current.Properties["DetailsTabKey"] = DetailsTab.Expression;
                                    Application.Current.Properties["WorkStartDateDetails"] = WorkStartDateDetails.Expression;
                                    Application.Current.Properties["WorkCompletionDateDetails"] = WorkCompletionDateDetails.Expression;
                                    Application.Current.Properties["AdditionalDetailsTab"] = AdditionalDetailsTab.Expression;
                                    Application.Current.Properties["CauseTab"] = CauseTab.Expression;
                                    Application.Current.Properties["Asset"] = Asset.Expression;


                                    //Get StockroomParts Rights/////
                                    var StockroomPartsDialog = PartsTab.listTabDialog.FirstOrDefault(i => i.TabDialogName == "StockroomParts");
                                    if (StockroomPartsDialog != null && StockroomPartsDialog.ButtonControls != null && StockroomPartsDialog.ButtonControls.Count > 0)
                                    {
                                        Application.Current.Properties["AddParts"] = StockroomPartsDialog.ButtonControls.FirstOrDefault(i => i.Name == "Add").Expression;
                                        Application.Current.Properties["EditParts"] = StockroomPartsDialog.ButtonControls.FirstOrDefault(i => i.Name == "Edit").Expression;
                                        Application.Current.Properties["RemoveParts"] = StockroomPartsDialog.ButtonControls.FirstOrDefault(i => i.Name == "Remove").Expression;


                                    }

                                    //Get Tools Rights/////
                                    if (ToolsTab != null && ToolsTab.ButtonControls != null && ToolsTab.ButtonControls.Count > 0)
                                    {
                                        Application.Current.Properties["CreateTool"] = ToolsTab.ButtonControls.FirstOrDefault(i => i.Name == "Add").Expression;
                                        Application.Current.Properties["DeleteTool"] = ToolsTab.ButtonControls.FirstOrDefault(i => i.Name == "Remove").Expression;

                                    }

                                    //Get WorkOrderAttachment Rights/////
                                    var AttachmentsTab = WorkorderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "Attachments");
                                    if (AttachmentsTab.ButtonControls != null && AttachmentsTab.ButtonControls.Count > 0)
                                    {

                                        Application.Current.Properties["CreateAttachment"] = AttachmentsTab.ButtonControls.FirstOrDefault(i => i.Name == "Add").Expression;
                                        Application.Current.Properties["DeleteAttachments"] = AttachmentsTab.ButtonControls.FirstOrDefault(i => i.Name == "Remove").Expression;
                                    }

                                    //Get Inspection Rights/////

                                    if (workorderSubModule.Button != null && workorderSubModule.Button.Count > 0)
                                    {
                                        Application.Current.Properties["CreateworkorderRights"] = workorderSubModule.Button.FirstOrDefault(i => i.Name == "New").Expression;

                                    }

                                    ///Set workOrderListing Page Rights
                                    var WorkOrderStartedDate = DetailsTab.listControls.FirstOrDefault(i => i.ControlName == "WorkStartedDate");
                                    var WorkOrderCompletionDate = DetailsTab.listControls.FirstOrDefault(i => i.ControlName == "CompletionDate");
                                    var WorkOrderRequestedDate = DetailsTab.listControls.FirstOrDefault(i => i.ControlName == "RequestedDate");
                                    var Description = DetailsTab.listControls.FirstOrDefault(i => i.ControlName == "Description");
                                    Application.Current.Properties["WorkOrderStartedDateKey"] = WorkOrderStartedDate.Expression;
                                    Application.Current.Properties["WorkOrderCompletionDateKey"] = WorkOrderCompletionDate.Expression;
                                    Application.Current.Properties["WorkOrderRequestedDateKey"] = WorkOrderRequestedDate.Expression;
                                    Application.Current.Properties["DescriptionKey"] = Description.Expression;
                                    var WorkOrderType = DetailsTab.listControls.FirstOrDefault(i => i.ControlName == "WorkTypeID");
                                    var Priority = DetailsTab.listControls.FirstOrDefault(i => i.ControlName == "Priority");
                                    Application.Current.Properties["WorkOrderTypeKey"] = WorkOrderType.Expression;
                                    Application.Current.Properties["PriorityKey"] = Priority.Expression;
                                    Application.Current.Properties["CreateWorkorderRights"] = workorderSubModule.Button.FirstOrDefault(i => i.Name == "New").Expression;
                                    Application.Current.Properties["EditRights"] = workorderSubModule.Button.FirstOrDefault(i => i.Name == "Edit").Expression;

                                }
                            }
                        }
                    }
                }
            }

        }
        public async Task GetServiceRequestModuleRights()
        {
            FormControlsAndRights = await _formLoadInputService.GetFormControlsAndRights(AppSettings.User.UserID.ToString(), AppSettings.ServiceRequestModuleName);
            if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
            {
                var ServiceRequestModule = FormControlsAndRights.lstModules[0];
                if (ServiceRequestModule.ModuleName == "ServiceRequests") //ModuleName can't be  changed in service 
                {
                    if (ServiceRequestModule.lstSubModules != null && ServiceRequestModule.lstSubModules.Count > 0)
                    {
                        var ServiceRequestSubModule = ServiceRequestModule.lstSubModules.FirstOrDefault(i => i.SubModuleName == "ServiceRequests");

                        if (ServiceRequestSubModule != null)
                        {
                            if (ServiceRequestSubModule.Button != null && ServiceRequestSubModule.Button.Count > 0)
                            {

                                Application.Current.Properties["AcceptKey"] = ServiceRequestSubModule.Button.FirstOrDefault(i => i.Name == "Accept").Expression;
                                Application.Current.Properties["DEclineKey"] = ServiceRequestSubModule.Button.FirstOrDefault(i => i.Name == "Decline").Expression;
                                Application.Current.Properties["CreateServiceRequestKey"] = ServiceRequestSubModule.Button.FirstOrDefault(i => i.Name == "New").Expression;
                                Application.Current.Properties["EditServiceRequestKey"] = ServiceRequestSubModule.Button.FirstOrDefault(i => i.Name == "Edit").Expression;

                            }
                            if (ServiceRequestSubModule.listDialoges != null && ServiceRequestSubModule.listDialoges.Count > 0)
                            {
                                var ServiceRequestDialog = ServiceRequestSubModule.listDialoges.FirstOrDefault(i => i.DialogName == "ServiceRequestDialog");
                                if (ServiceRequestDialog != null && ServiceRequestDialog.listTab != null && ServiceRequestDialog.listTab.Count > 0)
                                {
                                    var DetailsTab = ServiceRequestDialog.listTab.FirstOrDefault(i => i.DialogTabName == "Details");
                                    var PriorityTab = DetailsTab.listControls.FirstOrDefault(i => i.ControlName == "PriorityID");
                                    Application.Current.Properties["PriorityTabKey"] = PriorityTab.Expression;
                                }
                            }
                        }
                    }
                }
            }

        }
        public async Task GetAssetModuleRights()
        {
            FormControlsAndRights = await _formLoadInputService.GetFormControlsAndRights(AppSettings.User.UserID.ToString(), AppSettings.AssetModuleName);
            if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
            {
                var AssetModule = FormControlsAndRights.lstModules[0];
                if (AssetModule.ModuleName == "Assets") //ModuleName can't be  changed in service 
                {
                    if (AssetModule.lstSubModules != null && AssetModule.lstSubModules.Count > 0)
                    {
                        var AssetSubModule = AssetModule.lstSubModules.FirstOrDefault(i => i.SubModuleName == "Assets");

                        if (AssetSubModule != null)
                        {
                            if (AssetSubModule.Button != null && AssetSubModule.Button.Count > 0)
                            {
                                Application.Current.Properties["CreateAssetKey"] = AssetSubModule.Button.FirstOrDefault(i => i.Name == "New").Expression;
                                Application.Current.Properties["EditAssetKey"] = AssetSubModule.Button.FirstOrDefault(i => i.Name == "Edit").Expression;
                            }
                        }
                        if (AssetSubModule.listDialoges != null && AssetSubModule.listDialoges.Count > 0)
                        {
                            var AssetDialog = AssetSubModule.listDialoges.FirstOrDefault(i => i.DialogName == "AssetDialog");
                            if (AssetDialog != null && AssetDialog.listTab != null && AssetDialog.listTab.Count > 0)
                            {
                                var DetailsTab = AssetDialog.listTab.FirstOrDefault(i => i.DialogTabName == "Details");

                                if (DetailsTab != null && DetailsTab.listControls != null && DetailsTab.listControls.Count > 0)
                                {
                                    var DescriptionTab = DetailsTab.ButtonControls.FirstOrDefault(i => i.Name == "Description");
                                    Application.Current.Properties["DescriptionTabKey"] = DescriptionTab.Expression;

                                }
                            }
                        }
                    }
                }
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
                                catch (Exception)
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


                                Application.Current.Properties["TaskandLabourTabKey"] = WorkOrderTaskModule.Expression;
                                Application.Current.Properties["CreateTask"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "Add").Expression;
                                Application.Current.Properties["WOLabourTime"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "WorkOrderLaborTime").Expression;


                                Application.Current.Properties["TaskTabDetails"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "TaskID").Expression;
                                Application.Current.Properties["HourAtRate1"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "HoursAtRate1").Expression;
                                Application.Current.Properties["EmployeeTab"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "EmployeeLaborCraftID").Expression;
                                Application.Current.Properties["ContractorTab"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "ContractorLaborCraftID").Expression;
                                Application.Current.Properties["StartdateTab"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "StartDate").Expression;
                                Application.Current.Properties["CompletionDateTab"] = WorkOrderTaskSubModule.listControls.FirstOrDefault(i => i.ControlName == "CompletionDate").Expression;



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


                            Application.Current.Properties["CreateTool"] = WorkOrderToolsSubModule.listControls.FirstOrDefault(i => i.ControlName == "Add").Expression;
                            Application.Current.Properties["DeleteTool"] = WorkOrderToolsSubModule.listControls.FirstOrDefault(i => i.ControlName == "Remove").Expression;

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


                            Application.Current.Properties["AddParts"] = WorkOrderPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "Add").Expression;
                            Application.Current.Properties["EditParts"] = WorkOrderPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "Edit").Expression;
                            Application.Current.Properties["RemoveParts"] = WorkOrderPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "Remove").Expression;

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


                            Application.Current.Properties["CreateAttachment"] = WorkOrderAttachmentSubModule.listControls.FirstOrDefault(i => i.ControlName == "Add").Expression;
                            Application.Current.Properties["DeleteAttachments"] = WorkOrderAttachmentSubModule.listControls.FirstOrDefault(i => i.ControlName == "Remove").Expression;

                        }
                    }
                }
            }
            catch (Exception)
            {

               
            }
           
        }

        public async Task GetServiceRequestControlRights()
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



                            Application.Current.Properties["AcceptKey"] = ServiceRequestSubModule.listControls.FirstOrDefault(i => i.ControlName == "Accept").Expression;
                            Application.Current.Properties["DEclineKey"] = ServiceRequestSubModule.listControls.FirstOrDefault(i => i.ControlName == "Decline").Expression;
                            Application.Current.Properties["CreateServiceRequestKey"] = ServiceRequestSubModule.listControls.FirstOrDefault(i => i.ControlName == "New").Expression;
                            Application.Current.Properties["EditServiceRequestKey"] = ServiceRequestSubModule.listControls.FirstOrDefault(i => i.ControlName == "Edit").Expression;





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
                            Application.Current.Properties["CreateSRAttachment"] = ServiceRequestSubModule.listControls.FirstOrDefault(i => i.ControlName == "Add").Expression;
                            Application.Current.Properties["RemoveSRAttachment"] = ServiceRequestSubModule.listControls.FirstOrDefault(i => i.ControlName == "Remove").Expression;
                            Application.Current.Properties["SRAttachmentTabKey"] = ServiceRequestModuleAttachment.Expression;
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



                            Application.Current.Properties["PriorityTabKey"] = ServiceRequestSubModule.listControls.FirstOrDefault(i => i.ControlName == "PriorityID").Expression;
                            Application.Current.Properties["ServiceRequestAdditionalDetailsKey"] = ServiceRequestSubModule.listControls.FirstOrDefault(i => i.ControlName == "AdditionalDetails").Expression;
                            Application.Current.Properties["ServiceRequestDetailsControls"] = ServiceRequestSubModule;
                            Application.Current.Properties["ServiceRequestTarget"] = ServiceRequestSubModule.listControls.FirstOrDefault(i => i.ControlName == "AssetID").Expression; ;
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



                            Application.Current.Properties["SHETagTypeKey"] = ServiceRequestSubModuleSheTAg.listControls.FirstOrDefault(i => i.ControlName == "TagType").Expression;

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



                            Application.Current.Properties["MaintenanceTagTypeKey"] = ServiceRequestSubModuleSheTAg.listControls.FirstOrDefault(i => i.ControlName == "TagType").Expression;

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



                            Application.Current.Properties["OperatorTagTypeKey"] = ServiceRequestSubModuleSheTAg.listControls.FirstOrDefault(i => i.ControlName == "TagType").Expression;

                        }



                    }
                }
            }
        }










        public async Task GetAssetControlRights()
        {

            ServiceOutput FormControlsAndRightsForDetails = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "Assets", "Details");
            ServiceOutput FormControlsAndRightsForButton = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "Assets", "Assets");
            
            if (FormControlsAndRightsForButton != null && FormControlsAndRightsForButton.lstModules != null && FormControlsAndRightsForButton.lstModules.Count > 0)
            {
                var AssetModule = FormControlsAndRightsForButton.lstModules[0];
                if (AssetModule.ModuleName == "Assets") //ModuleName can't be  changed in service 
                {
                    if (AssetModule.lstSubModules != null && AssetModule.lstSubModules.Count > 0)
                    {
                        var AssetSubModule = AssetModule.lstSubModules[0];
                        if (AssetSubModule.listControls != null && AssetSubModule.listControls.Count > 0)
                        {
                            Application.Current.Properties["CreateAssetKey"] = AssetSubModule.listControls.FirstOrDefault(i => i.ControlName == "New").Expression;
                            Application.Current.Properties["EditAssetKey"] = AssetSubModule.listControls.FirstOrDefault(i => i.ControlName == "Edit").Expression;
                            Application.Current.Properties["IssueWorkorderKey"] = AssetSubModule.listControls.FirstOrDefault(i => i.ControlName == "IssueWorkOrder").Expression;




                        }



                    }
                }
            }
            if (FormControlsAndRightsForDetails != null && FormControlsAndRightsForDetails.lstModules != null && FormControlsAndRightsForDetails.lstModules.Count > 0)
            {
                var AssetModule = FormControlsAndRightsForDetails.lstModules[0];
                if (AssetModule.ModuleName == "Details") //ModuleName can't be  changed in service 
                {

                    if (AssetModule.lstSubModules != null && AssetModule.lstSubModules.Count > 0)
                    {
                        var AssetSubModule = AssetModule.lstSubModules[0];
                        if (AssetSubModule.listControls != null && AssetSubModule.listControls.Count > 0)
                        {
                            Application.Current.Properties["DescriptionTabKey"] = AssetSubModule.listControls.FirstOrDefault(i => i.ControlName == "Description").Expression;
                            Application.Current.Properties["AssetAdditionalDetailsTabKey"] = AssetSubModule.listControls.FirstOrDefault(i => i.ControlName == "AdditionalDetails").Expression;
                            Application.Current.Properties["AssetLocationDetailsTabKey"] = AssetSubModule.listControls.FirstOrDefault(i => i.ControlName == "Location").Expression;
                           // Application.Current.Properties["AssetDetailsTabKey"] = AssetSubModule;    
                            Application.Current.Properties["AssetsDetailsControls"] = AssetSubModule;


                        }



                    }
                }
            }

        }
        public async Task GetPurchaseOrderControlRights()
        {

            ServiceOutput FormControlsAndRightsForDetails = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "Purchaseorder", "Details");


            if (FormControlsAndRightsForDetails != null && FormControlsAndRightsForDetails.lstModules != null && FormControlsAndRightsForDetails.lstModules.Count > 0)
            {
                var POModule = FormControlsAndRightsForDetails.lstModules[0];
                if (POModule.ModuleName == "PurchaseOrders") //ModuleName can't be  changed in service 
                {

                    if (POModule.lstSubModules != null && POModule.lstSubModules.Count > 0)
                    {
                        var POSubModule = POModule.lstSubModules[0];
                        if (POSubModule.listControls != null && POSubModule.listControls.Count > 0)
                        {
                            Application.Current.Properties["NonStockroomParts"] = POSubModule.listControls.FirstOrDefault(i => i.ControlName == "NonStockroomParts").Expression;
                            Application.Current.Properties["Parts"] = POSubModule.listControls.FirstOrDefault(i => i.ControlName == "Parts").Expression;
                            Application.Current.Properties["Assets"] = POSubModule.listControls.FirstOrDefault(i => i.ControlName == "Assets").Expression;

                            

                        }



                    }
                }
            }
            FormControlsAndRights = await _formLoadInputService.GetFormControlsAndRights(AppSettings.User.UserID.ToString(), AppSettings.ReceivingModuleName);
            if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
            {
                var PurchaseOrderModule = FormControlsAndRights.lstModules[0];
                if (PurchaseOrderModule.ModuleName == "Purchasing") //ModuleName can't be  changed in service 
                {
                    if (PurchaseOrderModule.lstSubModules != null && PurchaseOrderModule.lstSubModules.Count > 0)
                    {

                        var PurchaseOrderSubModule = PurchaseOrderModule.lstSubModules.FirstOrDefault(i => i.SubModuleName == "PurchaseOrders");

                        if (PurchaseOrderSubModule != null)
                        {
                            if (PurchaseOrderSubModule.Button != null && PurchaseOrderSubModule.Button.Count > 0)
                            {
                                //  CloseWorkorderRights = workorderSubModule.Button.FirstOrDefault(i => i.Name == "Close");

                            }

                            if (PurchaseOrderSubModule.listDialoges != null && PurchaseOrderSubModule.listDialoges.Count > 0)
                            {
                                var PurchaseOrderDialog = PurchaseOrderSubModule.listDialoges.FirstOrDefault(i => i.DialogName == "Receiving");
                                if (PurchaseOrderDialog != null && PurchaseOrderDialog.listTab != null && PurchaseOrderDialog.listTab.Count > 0)
                                {
                                    var PONonStockPartsTab = PurchaseOrderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "NonStockroomParts");
                                    var POAssetsTab = PurchaseOrderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "Assets");
                                    var POPartsTab = PurchaseOrderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "Parts");
                                    var POPartsRecieveButton = POPartsTab.ButtonControls.FirstOrDefault(i => i.Name == "ReceiveParts");
                                    var PONonStockPartsRecieveButton = PONonStockPartsTab.ButtonControls.FirstOrDefault(i => i.Name == "ReceiveNonStockroomParts");
                                    var POAssetsRecieveButton = POAssetsTab.ButtonControls.FirstOrDefault(i => i.Name == "ReceiveAssets");

                                    Application.Current.Properties["ReceiveParts"] = POPartsRecieveButton.Expression;
                                    Application.Current.Properties["ReceiveNonStockroomParts"] = PONonStockPartsRecieveButton.Expression;
                                    Application.Current.Properties["ReceiveAssets"] = POAssetsRecieveButton.Expression;

                                }
                            }
                        }
                    }
                }
                
            }

        }

        public async Task GetClosedWorkorderandInventoryControlRights()
        {

            ServiceOutput FormControlsAndRightsForInventory = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "StockroomParts", "Dialog");
            ServiceOutput FormControlsAndRightsForClosedworkorder = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "Closedworkorder", "Dialog");

            if (FormControlsAndRightsForInventory != null && FormControlsAndRightsForInventory.lstModules != null && FormControlsAndRightsForInventory.lstModules.Count > 0)
            {
                var StockroomParts = FormControlsAndRightsForInventory.lstModules[0];
                if (StockroomParts.ModuleName == "StockroomParts") //ModuleName can't be  changed in service 
                {
                    if (StockroomParts.lstSubModules != null && StockroomParts.lstSubModules.Count > 0)
                    {
                        var StockroomPartsSubModule = StockroomParts.lstSubModules[0];
                        if (StockroomPartsSubModule.listControls != null && StockroomPartsSubModule.listControls.Count > 0)
                        {
                            Application.Current.Properties["StockroomPartsVisibilityKey"] = StockroomParts.Expression;
                            Application.Current.Properties["StockroomTransactionDialog"] = StockroomPartsSubModule.listControls.FirstOrDefault(i => i.ControlName == "StockroomTransactionDialog").Expression;
                           



                        }



                    }
                }
            }
            if (FormControlsAndRightsForClosedworkorder != null && FormControlsAndRightsForClosedworkorder.lstModules != null && FormControlsAndRightsForClosedworkorder.lstModules.Count > 0)
            {
                var ClosedWorkorderModule = FormControlsAndRightsForClosedworkorder.lstModules[0];
                if (ClosedWorkorderModule.ModuleName == "ClosedWorkOrderDialog") //ModuleName can't be  changed in service 
                {
                    if (ClosedWorkorderModule.lstSubModules != null && ClosedWorkorderModule.lstSubModules.Count > 0)
                    {
                        var ClosedWorkorderSubModule = ClosedWorkorderModule.lstSubModules[0];
                        if (ClosedWorkorderSubModule.listControls != null && ClosedWorkorderSubModule.listControls.Count > 0)
                        {
                            Application.Current.Properties["ClosedWorkorderCauseTabKey"] = ClosedWorkorderSubModule.listControls.FirstOrDefault(i => i.ControlName == "Causes").Expression;
                            Application.Current.Properties["ClosedWorkorderAdditionalDetailsTabKey"] = ClosedWorkorderSubModule.listControls.FirstOrDefault(i => i.ControlName == "AdditionalDetails").Expression;

                            Application.Current.Properties["ClosedWorkorderInternalNotesKey"] = ClosedWorkorderSubModule.listControls.FirstOrDefault(i => i.ControlName == "InternalNote").Expression;

                        }



                    }
                }
            }

        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden || response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    //throw new Exception(content);
                    //Debug.WriteLine()
                }

                //throw new HttpRequestException(content);
            }
        }
        private async Task<ServiceOutput> DeserializeObject(string content, JsonSerializerSettings jsonSerializerSettings)
        {
            try
            {
                return JsonConvert.DeserializeObject<ServiceOutput>(content, jsonSerializerSettings);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    

        #region Test Region 

        public DateTime? _presentDate = DateTime.Now;
        public DateTime? _pastDate = new DateTime(2018, 4, 28, 11, 20, 20);
        public DateTime? _futureDate = DateTime.Now.AddDays(2);


        public DateTime? PresentDate
        {
            get
            {
                return _presentDate;
            }
        }
        public DateTime? PastDate
        {
            get { return _pastDate; }
        }
        public DateTime? FutureDate
        {
            get
            {
                return _futureDate;
            }
        }



        #endregion

      
    }
}
