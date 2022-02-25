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
using Windows.UI.ViewManagement;
using Windows.ApplicationModel.Core;
using Windows.UI;
using ProteusMMX.Crypto;

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
        string _pageTitle = "ProteusMMX Login";
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

        string _siteUrlLabel = "ProteusMMX URL";
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

        string _userNameLabel = "User Name";
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

        string _copyrightLabel = "Copyright @ 2021 Eagle Technology Inc.";
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
                    if (!string.IsNullOrWhiteSpace(FDAKey.CompanyProfileLogo))
                    {
                        string newcompanyprofilebase64 = string.Empty;
                        if (Device.RuntimePlatform == Device.UWP)
                        {
                            string companyprofilebase64 = FDAKey.CompanyProfileLogo;
                            newcompanyprofilebase64 = companyprofilebase64.Replace("data:image/png;base64,", "");
                            if (string.IsNullOrWhiteSpace(newcompanyprofilebase64))
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
                        else
                        {
                            string companyprofilebase64 = FDAKey.CompanyProfileLogo;
                            newcompanyprofilebase64 = companyprofilebase64.Replace("data:image/png;base64,", "");
                            if (string.IsNullOrWhiteSpace(newcompanyprofilebase64))
                            {
                                newcompanyprofilebase64 = companyprofilebase64.Replace("data:image/jpeg;base64,", "");
                            }
                            byte[] imgUser = StreamToBase64.StringToByte(newcompanyprofilebase64);
                            MemoryStream stream = new MemoryStream(imgUser);
                            bool isimage = Extension.IsImage(stream);
                            if (isimage == true)
                            {

                                // byte[] byteImage = await Xamarin.Forms.DependencyService.Get<IResizeImage>().ResizeImageAndroid(imgUser, 160, 120);
                                AttachmentImageSource = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(imgUser))));



                            }
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

                ServiceOutput FormControlsAndRightsForDetails = await _workorderService.GetWorkorderControlRights(AppSettings.User.UserID.ToString(), "workorders", "Details");
                if (FormControlsAndRightsForDetails != null && FormControlsAndRightsForDetails.lstModules != null && FormControlsAndRightsForDetails.lstModules.Count > 0)
                {
                    var WorkOrderModule = FormControlsAndRightsForDetails.lstModules[0];
                    if (WorkOrderModule.ModuleName == "Details")  
                    {
                        if (WorkOrderModule.lstSubModules != null && WorkOrderModule.lstSubModules.Count > 0)
                        {
                            var WorkOrderSubModule = WorkOrderModule.lstSubModules[0];
                            if (WorkOrderSubModule.listControls != null && WorkOrderSubModule.listControls.Count > 0)
                            {
                                try
                                {
                                    Application.Current.Properties["CreateWorkorderRights"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "New").Expression;
                                 
                                    Application.Current.Properties["CloseWorkorderRightsKey"] = WorkOrderSubModule.listControls.FirstOrDefault(i => i.ControlName == "CompleteAndClose").Expression;



                                  

                                }
                                catch (Exception ex)
                                {


                                }



                            }



                        }
                    }
                }
                UserDialogs.Instance.HideLoading();
               await NavigationService.NavigateToAsync<DashboardPageViewModel>();
                await NavigationService.RemoveLastFromBackStackAsync();


                if (RememberMeSwitch == false)
                {
                    Settings.Remove(nameof(UserName));
                    Settings.Remove(nameof(Password));
                   
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
