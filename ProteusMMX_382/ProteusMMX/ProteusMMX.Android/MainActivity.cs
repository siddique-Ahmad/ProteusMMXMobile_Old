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

namespace ProteusMMX.Droid
{
    [Activity(Label = "ProteusMMX", Icon = "@drawable/icon", Theme = "@style/MainTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.User)]
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
            Android.Support.V4.App.NotificationCompat.Builder builder = new Android.Support.V4.App.NotificationCompat.Builder(this);
            if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.Lollipop)
            {
                builder.SetSmallIcon(Resource.Drawable.icon);
            }
            else
            {
                builder.SetSmallIcon(Resource.Drawable.icon);
            }
            LoadApplication(new App());
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

