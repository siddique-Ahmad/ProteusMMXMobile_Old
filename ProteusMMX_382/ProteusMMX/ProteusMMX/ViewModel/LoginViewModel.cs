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
using Plugin.LocalNotification;
using System.Collections.Generic;
using System.Net.Http.Headers;
using Plugin.LocalNotification.EventArgs;
using System.Text;
using Plugin.LocalNotification.AndroidOption;

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

        #endregion

        #region Commands

        public ICommand OnLoginTapCommand => new AsyncCommand(OnLoginTapAsync);

        int UserId;
        int nid;

        #endregion

        string WorkOrderId = null;
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                #region Local Notification


                if (AppSettings.User != null)
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
               
                #endregion
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
            Dictionary<string, string> urlSegment = new Dictionary<string, string>();
            urlSegment.Add("USERID", UserId.ToString());

            ServiceOutput notifications = await ServiceCallWebClient(AppSettings.BaseURL + "/Inspection/service/GetNotification", "GET", urlSegment, null);
            if (notifications.servicestatus == "true" && notifications.notificationWrapper.Status)
            {
                TestNotification(notifications.notificationWrapper);
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
            nid = Convert.ToInt32(notifications.ID);
            var notification = new NotificationRequest
            {
                BadgeNumber = nid,
                Description = notifications.Message,
                Title = notifications.Title,
                ReturningData = Convert.ToString(notifications.ID),                
                NotificationId = notifications.WorkOrderId,
                
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
        static async Task SendURI(Uri u, HttpContent c)
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

                if (user.mmxUser.UserLicense == "Web")
                {
                    UserDialogs.Instance.HideLoading();

                    DialogService.ShowToast("Current User Doesnot have Mobile License", 2000);

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
                await InitializeTranslations();
                UserDialogs.Instance.HideLoading();
                #region Local Notification




                UserId = user.mmxUser.UserID;
                var minutes = TimeSpan.FromSeconds(10);
                Xamarin.Forms.Device.StartTimer(minutes, () =>
                {

                    //check for notifications
                    GetUserNotification();

                    // Returning true means repeat this timer
                    return true;
                });

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



