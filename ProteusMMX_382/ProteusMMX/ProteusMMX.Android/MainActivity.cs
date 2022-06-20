using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Acr.UserDialogs;
using Plugin.Permissions;
using Plugin.CurrentActivity;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;

using Android.Content.Res;
using Microsoft.AppCenter.Crashes;
using Plugin.LocalNotification;
using Android.Content;
using Xamarin.Forms;
using ProteusMMX.Services.Navigation;
using System.Threading.Tasks;
using ProteusMMX.ViewModel;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.ViewModel.Workorder;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;

namespace ProteusMMX.Droid
{
    [Activity(Label = "ProteusMMX", Icon = "@drawable/icon", MainLauncher =true, Theme = "@style/MainTheme", LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.User)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
           

            //  AppCenter.Start("24b17e2f-b607-4143-abd0-60877acd4676", typeof(Analytics), typeof(Crashes));

            AppCenter.Start("7edb0700-5702-4890-9beb-3561942dd6f4", typeof(Analytics), typeof(Crashes));
            CrossCurrentActivity.Current.Init(this, bundle);
            global::ZXing.Net.Mobile.Forms.Android.Platform.Init();
            UserDialogs.Init(this);
            Rg.Plugins.Popup.Popup.Init(this);
            NotificationCenter.CreateNotificationChannel();
            NotificationCenter.NotifyNotificationTapped(Intent);
            //var data= NotificationCenter.NotifyNotificationTapped(Intent);
            //if (Android.OS.Build.VERSION.SdkInt >= Build.VERSION_CODES.Lollipop)
            //{
            //    //notification.setSmallIcon(R.drawable.icon_transperent);
            //    //notification.setColor(getResources().getColor(R.color.notification_color));
            //}
            //else
            //{
            //    notification.SmallIcon(R.Drawable.icon);
            //}

            LoadApplication(new App());
        }

       
        protected override void OnNewIntent(Intent intent)
        {
            NotificationCenter.NotifyNotificationTapped(intent);
            base.OnNewIntent(intent);
            NavigateTo(intent);
        }

        

        void NavigateTo(Intent intent)
        {
           
            if (intent.Action == "android.intent.action.MAIN")
            {
                InitNavigation();
               // Xamarin.Forms.Application.Current.MainPage.Navigation.PushModalAsync(NavigationService.NavigateToAsync<WorkorderTabbedPageViewModel>(item));
            }
        }
     

        private Task InitNavigation()
        {
            var navigationService = Locator.Instance.Resolve<INavigationService>();
            //return navigationService.NavigateToAsync<ExtendedSplashViewModel>();  // this page is used for preprocessing work
            return navigationService.DasebordAsync();
        }

        public override void OnBackPressed()
        {
            Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        {
           
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            for (int i = 0; i < permissions.Length; i++)
            {
                if (permissions[i].Equals("android.permission.CAMERA") && grantResults[i] == Permission.Granted)
                    global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
            //base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
          //  global::ZXing.Net.Mobile.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

           // DeviceOrientationImplementation.NotifyOrientationChange(newConfig.Orientation);
        }
    }
}

