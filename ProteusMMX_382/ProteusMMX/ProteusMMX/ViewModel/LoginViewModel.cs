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
        bool _RememberMeSwitch = false;
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

        public ICommand OnLoginTapCommand => new AsyncCommand(OnLoginTapAsync);


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

              

                if (AppSettings.User!=null && AppSettings.User.blackhawkLicValidator.FDAEnable)
                {
                    if (AppSettings.User.blackhawkLicValidator.FDAEnable && AppSettings.User.blackhawkLicValidator.Signvalue == "True")
                    {

                        SiteUrl = AppSettings.BaseURL;
                        LoginSwitch = false;
                        LabelSwitch = false;
                        return;
                    }


                }

                if (AppSettings.RememberMeSwitchFlag == true)
                {

                    UserName = AppSettings.UserName;
                    Password = AppSettings.Password;
                    SiteUrl = AppSettings.BaseURL;
                    
                    if (string.IsNullOrWhiteSpace(UserName) && string.IsNullOrWhiteSpace(Password))
                    {
                        AppSettings.RememberMeSwitchFlag = false;
                    }



                    var user = await _authenticationService.UserIsAuthenticatedAndValidAsync(SiteUrl, UserName, Password);
                    if (user != null && user.mmxUser != null)
                    {
                        ////Set Company Profile Picture///////
                        if (!string.IsNullOrWhiteSpace(user.mmxUser.blackhawkLicValidator.CompanyProfileLogo))
                        {
                            string newcompanyprofilebase64 = string.Empty;
                            if (Device.RuntimePlatform == Device.UWP)
                            {
                                string companyprofilebase64 = user.mmxUser.blackhawkLicValidator.CompanyProfileLogo;
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
                                string companyprofilebase64 = user.mmxUser.blackhawkLicValidator.CompanyProfileLogo;
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

                                  
                                    AttachmentImageSource = Xamarin.Forms.ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(Convert.ToBase64String(imgUser))));



                                }
                            }
                        }



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

                    else if (AppSettings.RememberMeSwitchFlag == true)
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


       
        public async Task OnLoginTapAsync()
        {
            try
            {

                ServiceOutput apiVersion = new ServiceOutput();
                UserDialogs.Instance.ShowLoading("Please wait..loading all data");

                if (string.IsNullOrEmpty(SiteUrl))
                {
                    UserDialogs.Instance.HideLoading();
                    await DialogService.ShowAlertAsync("Please enter Site Url", "Alert", "OK");
                    return;
                }
                apiVersion = await _authenticationService.GetAPIVersion(SiteUrl);


                if (apiVersion == null)
                {
                    UserDialogs.Instance.HideLoading();

                    await DialogService.ShowAlertAsync("Please verify the site URL.", "Alert", "OK");
                    return;

                }

                AppSettings.BaseURL = SiteUrl;


                var user = await _authenticationService.LoginAsync(AppSettings.BaseURL, UserName, Password);
                
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
                /// Save the MMXUser Properties the so we can reuse that.
                AppSettings.APIVersion = user.mmxUser.blackhawkLicValidator.APIVersion;
                AppSettings.User = user.mmxUser;
                AppSettings.UserName = UserName;
                AppSettings.Password = Password;
                AppSettings.User.blackhawkLicValidator.FDAEnable = user.mmxUser.blackhawkLicValidator.FDAEnable;
                AppSettings.User.blackhawkLicValidator.Signvalue= user.mmxUser.blackhawkLicValidator.Signvalue;
                AppSettings.User.blackhawkLicValidator.CompanyProfileLogo = user.mmxUser.blackhawkLicValidator.CompanyProfileLogo;

                //var FDALicenseKey = await _authenticationService.GetFDAValidationAsync(SiteUrl, null);
                if (user.mmxUser.blackhawkLicValidator.FDAEnable && user.mmxUser.blackhawkLicValidator.Signvalue == "True")
                {

                }
               

                // Initialize Translations Property
                Application.Current.Properties["UserNameType"] = "TextBox";
                SignatureStorage.Storage.Set("FDASignatureUserValidated", "False");
                Application.Current.Properties["Password"] = Password;
                await InitializeTranslations();
                UserDialogs.Instance.HideLoading();
                await NavigationService.NavigateToAsync<DashboardPageViewModel>();
                await NavigationService.RemoveLastFromBackStackAsync();
              
                if (AppSettings.RememberMeSwitchFlag == false)
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
    }
}
       


   