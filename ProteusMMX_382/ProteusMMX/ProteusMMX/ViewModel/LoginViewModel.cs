using Acr.UserDialogs;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using PCLStorage;
//using Windows.UI.ViewManagement;
//using Windows.ApplicationModel.Core;
//using Windows.UI;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification.EventArgs;
using Plugin.Settings;
using Plugin.Settings.Abstractions;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.DateTime;
using ProteusMMX.Helpers.Storage;
using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Translations;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

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

        string _notiWorkorderID;
        public string NotiWorkorderID
        {
            get
            {
                return _notiWorkorderID;
            }

            set
            {
                if (value != _notiWorkorderID)
                {
                    _notiWorkorderID = value;
                    OnPropertyChanged(nameof(NotiWorkorderID));
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
        static string Year = DateTime.Parse(DateTime.Now.ToString()).Year.ToString();
        string _copyrightLabel = "Copyright ©" + Year + " Eagle Technology Inc.";

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

        string _aPIVersion = "App: v " + AppSettings.APPVersion;
        public string APIVersion
        {
            get
            {
                return _aPIVersion;
            }

            set
            {
                if (value != _aPIVersion)
                {
                    _aPIVersion = value;
                    OnPropertyChanged("APIVersion");
                }
            }
        }

        #endregion

        #region Commands

        public ICommand OnLoginTapCommand => new AsyncCommand(OnLoginTapAsync);

        int UserId;
        int nid;
        string TockenNumber = string.Empty;
        #endregion

        string WorkOrderId = null;

        IFolder folder = FileSystem.Current.LocalStorage;
        String filename = "TitelAll.txt";

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {

                //if (navigationData != null)
                //{
                //    var navigationParams = navigationData as TargetNavigationData;
                //    WorkorderID = navigationParams.WorkOrderId;
                //}

                if (Device.RuntimePlatform == Device.iOS)
                {
                    OperationInProgress = true;
                }
                else
                {

                    UserDialogs.Instance.ShowLoading("Please wait..loading all data");
                }

               

                if (AppSettings.User != null && AppSettings.User.blackhawkLicValidator.FDAEnable)
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
                        #region Local Notification


                        if (user.mmxUser != null && user.mmxUser.NotificationMode == "Intranet")
                        {

                            UserId = AppSettings.User.UserID;
                            var minutes = TimeSpan.FromSeconds(30);
                            Xamarin.Forms.Device.StartTimer(minutes, () =>
                            {

                                //check for notifications
                                GetUserNotification();

                                // Returning true means repeat this timer
                                return true;
                            });
                        }
                        if (!string.IsNullOrEmpty(NotiWorkorderID))
                        {
                            Application.Current.Properties["NotificationId"] = NotiWorkorderID;
                        }


                        NotifactionStorage.Storage.Set("NotificationModedb", JsonConvert.SerializeObject(user.mmxUser.NotificationMode));
                        if (user.mmxUser.NotificationMode == "Internet")
                        {
                            if (Application.Current.Properties.ContainsKey("TockenNumberKey"))
                            {
                                string TockenNumbers = Application.Current.Properties["TockenNumberKey"].ToString();

                                if (string.IsNullOrWhiteSpace(TockenNumber))
                                {
                                    TockenNumber = TockenNumbers;
                                }
                            }
                            if (string.IsNullOrWhiteSpace(user.mmxUser.FCMToken) && !string.IsNullOrEmpty(TockenNumber))
                            {
                                await PostToken(user.mmxUser.UserID, TockenNumber);
                            }
                            else
                            {
                                if (user.mmxUser.FCMToken != TockenNumber)
                                {
                                    await PostToken(user.mmxUser.UserID, TockenNumber);
                                }
                            }

                        }

                        #endregion

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
                        //await NavigationService.RemoveLastFromBackStackAsync();

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

        public async void GetUserNotification()
        {
            try
            {
                Dictionary<string, string> urlSegment = new Dictionary<string, string>();
                urlSegment.Add("USERID", UserId.ToString());

                ServiceOutput notifications = await ServiceCallWebClient(AppSettings.BaseURL + "/Inspection/service/GetNotification", "GET", urlSegment, null);
                if (notifications.servicestatus == "true" && notifications.notificationWrapper.Status)
                {
                    TestNotification(notifications.notificationWrapper);
                }
            }
            catch (Exception)
            {
                DialogService.ShowToast("internet not working properly", 2000);

            }


        }
        public async Task<ServiceOutput> ServiceCallWebClient(string url, string mtype, IDictionary<string, string> urlSegment, object jsonString)
        {
            ServiceOutput responseContent = new ServiceOutput();
            try
            {

                if (!string.IsNullOrEmpty(url))
                {
                    string segurl = string.Empty;
                    if (urlSegment != null)
                    {
                        foreach (KeyValuePair<string, string> entry in urlSegment)
                        {
                            segurl = segurl + "/" + entry.Value;
                        }
                    }

                    if (!string.IsNullOrEmpty(segurl))
                    {
                        url = url + segurl;
                    }



                    if (!string.IsNullOrEmpty(mtype))
                    {
                        if (mtype.ToLower().Equals("get"))
                        {


                            HttpClient client = new HttpClient();
                            client.BaseAddress = new Uri(url);

                            // Add an Accept header for JSON format.
                            client.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json"));

                            HttpResponseMessage response = client.GetAsync(url).Result;

                            if (response.IsSuccessStatusCode)
                            {
                                var content = JsonConvert.DeserializeObject(
                                        response.Content.ReadAsStringAsync()
                                        .Result);


                                responseContent = JsonConvert.DeserializeObject<ServiceOutput>(content.ToString());

                            }


                        }
                        else if (mtype.ToLower().Equals("post"))
                        {
                            try
                            {
                                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                                httpWebRequest.ContentType = "application/json";
                                httpWebRequest.Method = mtype;
                                var stream = await httpWebRequest.GetRequestStreamAsync();
                                string Json = JsonConvert.SerializeObject(jsonString);
                                using (var writer = new StreamWriter(stream))
                                {
                                    writer.Write(Json);
                                    writer.Flush();
                                    writer.Dispose();
                                }

                                using (HttpWebResponse response = await httpWebRequest.GetResponseAsync() as HttpWebResponse)
                                {
                                    if (response.StatusCode == HttpStatusCode.OK)
                                    {
                                        //  Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                                        {
                                            var content = reader.ReadToEnd();

                                            responseContent = JsonConvert.DeserializeObject<ServiceOutput>(content.ToString());
                                        }
                                    }


                                }

                            }
                            catch (WebException ex)
                            {

                            }
                            catch (Exception ex)
                            {

                            }

                        }
                    }
                }
            }



            catch (Exception ex)
            {

                throw ex;
            }

            return responseContent;
        }
        public void TestNotification(Notifications notifications)
        {
            String FinalDescription = string.Empty;
            nid = Convert.ToInt32(notifications.ID);
            if (!String.IsNullOrWhiteSpace(notifications.Message))
            {
                FinalDescription = RemoveHTML.StripHtmlTags(notifications.Message);
            }
            else
            {
                FinalDescription = notifications.Message;
            }
            var notification = new NotificationRequest
            {
                BadgeNumber = nid,
                Description = FinalDescription,
                Title = notifications.Title,
                ReturningData = Convert.ToString(notifications.ID),
                NotificationId = notifications.WorkOrderId,
                iOS = new Plugin.LocalNotification.iOSOption.iOSOptions
                {
                    PlayForegroundSound = true,
                },

            };

            NotificationCenter.Current.NotificationReceived += Current_NotificationReceived;
            //NotificationCenter.Current.NotificationTapped += Current_NotificationTapped; ;
            notification.Android.IconSmallName = new AndroidIcon("icon.png");
            Plugin.LocalNotification.NotificationCenter.Current.Show(notification);
        }

        //private async void Current_NotificationTapped(NotificationEventArgs e)
        //{
        //   // await DialogService.ShowAlertAsync("Tab notifaction login page .", "Alert", "OK");
        //    if (e.Request != null && e.Request.NotificationId > 0)
        //    {
        //        NotifactionStorage.Storage.Set("Notificationdb", JsonConvert.SerializeObject(e.Request.NotificationId));
        //    }
        //}

        //private void Current_NotificationTapped(NotificationTapped e)
        //{
        //    if (e.Request != null && e.Request.NotificationId > 0)
        //    {

        //    }
        //}

        private void Current_NotificationReceived(NotificationEventArgs e)
        {

            try
            {
                Device.BeginInvokeOnMainThread(() =>
                    {

                        Uri posturi = new Uri(AppSettings.BaseURL + "/Inspection/Service/CreateNotification");

                        var notification = new ServiceInput()
                        {
                            UserID = nid

                        };

                        string strPayload = JsonConvert.SerializeObject(notification);
                        HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
                        var t = Task.Run(() => SendURI(posturi, c));
                    });
            }
            catch (Exception)
            {
            }

        }
        static async Task SendURI(Uri u, HttpContent c)
        {
            try
            {
                var response = string.Empty;
                using (var client = new HttpClient())
                {
                    HttpRequestMessage request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Post,
                        RequestUri = u,
                        Content = c
                    };

                    HttpResponseMessage result = await client.SendAsync(request);
                    if (result.IsSuccessStatusCode)
                    {
                        response = result.StatusCode.ToString();
                    }
                }
            }
            catch (Exception)
            {

                throw;
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
                // Crashes.GenerateTestCrash();
                ServiceOutput apiVersion = new ServiceOutput();
                UserDialogs.Instance.ShowLoading("Please wait..loading all data", MaskType.Gradient);
                await Task.Delay(100);
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
                if (Device.RuntimePlatform == Device.UWP)
                {
                    user.mmxUser.blackhawkLicValidator.CompanyProfileLogo = null;
                }

                UserId = user.mmxUser.UserID;

                NotifactionStorage.Storage.Set("NotificationModedb", JsonConvert.SerializeObject(user.mmxUser.NotificationMode));

                if (!String.IsNullOrEmpty(user.mmxUser.UserLicense))
                {

                    if (user.mmxUser.UserLicense.ToLower() == "web")
                    {
                        UserDialogs.Instance.HideLoading();

                        DialogService.ShowToast("Current User Doesnot have Mobile License", 2000);

                        return;
                    }
                }
                else if(String.IsNullOrEmpty(user.mmxUser.UserLicense))
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast("Current User Doesnot have Mobile License", 2000);

                    return;
                }
                //DateTime expirationDate = DateTime.Parse(user.mmxUser.blackhawkLicValidator.ProductKeyExpirationDate);

                DateTime expirationDate = DateTime.ParseExact(user.mmxUser.blackhawkLicValidator.ProductKeyExpirationDate, "MM/dd/yyyy", null);

                DateTime userCurrentDate = DateTimeConverter.ClientCurrentDateTimeByZone(user.mmxUser.TimeZone).Date;
                if (userCurrentDate > expirationDate.Date)
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast("Product Key is Expire", 2000);

                    return;
                }
                /// Save the MMXUser Properties the so we can reuse that.
                AppSettings.APIVersion = apiVersion.APIVersion;
                AppSettings.User = user.mmxUser;
                AppSettings.UserName = UserName;
                AppSettings.Password = Password;
                AppSettings.User.blackhawkLicValidator.FDAEnable = user.mmxUser.blackhawkLicValidator.FDAEnable;
                AppSettings.User.blackhawkLicValidator.Signvalue = user.mmxUser.blackhawkLicValidator.Signvalue;
                AppSettings.User.blackhawkLicValidator.CompanyProfileLogo = user.mmxUser.blackhawkLicValidator.CompanyProfileLogo;


                //var FDALicenseKey = await _authenticationService.GetFDAValidationAsync(SiteUrl, null);
                if (user.mmxUser.blackhawkLicValidator.FDAEnable && user.mmxUser.blackhawkLicValidator.Signvalue == "True")
                {

                }


                // Initialize Translations Property
                Application.Current.Properties["UserNameType"] = "TextBox";
                SignatureStorage.Storage.Set("FDASignatureUserValidated", "False");
                Application.Current.Properties["Password"] = Password;
                
                if (user.mmxUser.NotificationMode == "Internet")
                {

                    if (Application.Current.Properties.ContainsKey("TockenNumberKey"))
                    {
                        string TockenNumbers = Application.Current.Properties["TockenNumberKey"].ToString();

                        if (string.IsNullOrWhiteSpace(TockenNumber))
                        {
                            TockenNumber = TockenNumbers;
                        }
                    }
                    if (!string.IsNullOrWhiteSpace(TockenNumber))
                    {
                        if (string.IsNullOrWhiteSpace(user.mmxUser.FCMToken) && !string.IsNullOrEmpty(TockenNumber))
                        {
                            await PostToken(user.mmxUser.UserID, TockenNumber);
                            //var Tokens = await _authenticationService.PostTokenAsync(AppSettings.BaseURL + "/Inspection/Service/FCMTokenCreate", user.mmxUser.UserID, TockenNumber);

                            //if (Convert.ToBoolean(Tokens.servicestatus) == false)
                            //{

                            //}
                        }
                        else
                        {
                            if (user.mmxUser.FCMToken != TockenNumber)
                            {
                                await PostToken(user.mmxUser.UserID, TockenNumber);
                                //var Tokens = await _authenticationService.PostTokenAsync(AppSettings.BaseURL + "/Inspection/Service/FCMTokenCreate", user.mmxUser.UserID, TockenNumber);
                                //if (Convert.ToBoolean(Tokens.servicestatus) == false)
                                //{

                                //}
                            }
                        }
                    }
                    else
                    {

                    }
                }
                await InitializeTranslations();
                UserDialogs.Instance.HideLoading();
                #region Local Notification


                if (user.mmxUser.NotificationMode == "Intranet")
                {
                    var minutes = TimeSpan.FromSeconds(10);
                    Xamarin.Forms.Device.StartTimer(minutes, () =>
                    {
                        //check for notifications
                        GetUserNotification();

                        // Returning true means repeat this timer
                        return true;
                    });
                }


                #endregion
                await NavigationService.NavigateToAsync<DashboardPageViewModel>();
                // await NavigationService.RemoveLastFromBackStackAsync();

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

        private async Task PostToken(long UserId, string Token)
        {
            try
            {
                Device.BeginInvokeOnMainThread(async () =>
                {

                    Uri posturi =  new  Uri(AppSettings.BaseURL + "/Inspection/Service/FCMTokenCreate");

                    var notificationToken = new ServiceInput()
                    {
                        UserID = UserId,
                        FCMToken = Token,

                    };

                    string strPayload = JsonConvert.SerializeObject(notificationToken);
                    HttpContent c =  new StringContent(strPayload, Encoding.UTF8, "application/json");
                    var t = Task.Run(() => SendURI(posturi, c));
                });
            }
            catch (Exception)
            {
            }
        }

        public async Task InitializeTranslations()
        {

            #region Set Badge to 0 in IOS
            
            Uri posturi = new Uri(AppSettings.BaseURL + "/inspection/service/BadgeUpdate");

            var payload = new Dictionary<string, string>
            {

              {"UserID ", AppSettings.User.UserID.ToString()}

            };

            string strPayload = JsonConvert.SerializeObject(payload);
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            var t = Task.Run(() => SendURI(posturi, c));

            #endregion

            //var AllTitelserializer = await PCLHelper.ReadAllTextAsync(filename, folder);
            var AllTitelserializer = "";
            if (AllTitelserializer == null || AllTitelserializer == "")
            {
                var translations = await _webControlTitlesService.GetWebControlTitles(AppSettings.User.UserID.ToString());

                string serialized = await Helpers.PCLStorage.PostPCLAsync(translations);

                bool WriteText = await PCLHelper.WriteTextAllAsync(filename, serialized, folder);
               

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
            else
            {
                var translations = Helpers.PCLStorage.GetPCLAsync(AllTitelserializer);

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
}



