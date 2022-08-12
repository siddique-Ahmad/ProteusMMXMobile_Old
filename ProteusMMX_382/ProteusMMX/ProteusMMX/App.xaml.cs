using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using Plugin.FirebasePushNotification;
using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;
using ProteusMMX.Controls;
using ProteusMMX.DependencyInterface;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.Storage;
using ProteusMMX.Model;
using ProteusMMX.Services.Navigation;
using ProteusMMX.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ProteusMMX
{
    public partial class App : Application
    {
        int UserId;
        public static int ScreenWidth { get; set; }
        public static int ScreenHeight { get; set; }

        public static void Logout()
        {


        }
        string TockenNumber = string.Empty;
        string NotificationMode= string.Empty;
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTc4MDAzQDMxMzkyZTMzMmUzMGVuNXozMzdmdEE5RVd6c3ZMeDZVR2lvWkg0UHB4YkZ2SXpER3JxdjBXUDA9");
            InitializeComponent();
            try
            {
               NotificationMode = JsonConvert.DeserializeObject(NotifactionStorage.Storage.Get("NotificationModedb")).ToString();
                
            }
            catch (Exception)
            {
                NotifactionStorage.Storage.Set("NotificationModedb", JsonConvert.SerializeObject("Internet"));
                NotificationMode = JsonConvert.DeserializeObject(NotifactionStorage.Storage.Get("NotificationModedb")).ToString();
            }
            #region **** FirebasePushNotification ****
            CrossFirebasePushNotification.Current.Subscribe(topic: "all");
            TockenNumber = CrossFirebasePushNotification.Current.Token;
            
            if (NotificationMode == "Internet")
            {
                CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
                {
                    var data = p;
                    var data1 = data.Data;
                    var Titel = data1["title"];
                    var Body = data1["body"];
                    var Key = data1["key1"];
                    NotifactionStorage.Storage.Set("Notificationdb", JsonConvert.SerializeObject(Key));
                    
                };
                if (!string.IsNullOrEmpty(TockenNumber))
                {
                    Application.Current.Properties["TockenNumberKey"] = TockenNumber;
                    CrossFirebasePushNotification.Current.OnTokenRefresh += Current_OnTokenRefresh;                    
                }
            }
            else
            {
                NotificationCenter.Current.NotificationTapped += OnLocalNotificationTapped;
            }
            #endregion

           

            try
            {
                Locator.Instance.Build();

                InitNavigation();
               
            }
            catch (Exception ex)
            {

                throw;
            }
            
        }
        private void Current_OnTokenRefresh(object source, FirebasePushNotificationTokenEventArgs e)
        {
            TockenNumber = e.Token;
            System.Diagnostics.Debug.WriteLine($"Token: {e.Token}");
            //NotifactionStorage.Storage.Set("Notificationdb", JsonConvert.SerializeObject(e.Token));
        }

        private async void OnLocalNotificationTapped(NotificationEventArgs e)
        {
            if (e.Request != null && e.Request.NotificationId > 0)
            {
                NotifactionStorage.Storage.Set("Notificationdb", JsonConvert.SerializeObject(e.Request.NotificationId));
                if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
                {
                    await InitDasebordNavigation();
                }

            }

        }
        private Task InitDasebordNavigation()
        {
            var navigationService = Locator.Instance.Resolve<INavigationService>();
            return navigationService.DasebordAsync();

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
            int nid = Convert.ToInt32(notifications.ID);
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
                NotificationId = nid,
                


        };

            Plugin.LocalNotification.NotificationCenter.Current.Show(notification);
        }
        
        private Task InitNavigation()
        {
            var navigationService = Locator.Instance.Resolve<INavigationService>();
            return navigationService.InitializeAsync();

        }

        protected override void OnStart()
        {
            //AppCenter.Start("ios=51232fc7-be85-4adb-99d3-e1c8f6f3db2f" +
            //       "uwp=7529a611-c100-4257-872f-a9315b2161dc" +
            //        "android=7edb0700-5702-4890-9beb-3561942dd6f4",
            //      // "android= 24b17e2f-b607-4143-abd0-60877acd4676",

                  
            //       typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            
        }

        protected override void OnResume()
        {
           
        }
    }
}
