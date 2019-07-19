using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using ProteusMMX.Services.Navigation;
using ProteusMMX.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ProteusMMX
{
    public partial class App : Application
    {
        public static int ScreenWidth { get; set; }
        public static int ScreenHeight { get; set; }

        public static void Logout()
        {


        }

        public App()
        {
            InitializeComponent();
            Locator.Instance.Build();
            InitNavigation();
          

        }

        private Task InitNavigation()
        {
            var navigationService = Locator.Instance.Resolve<INavigationService>();
            //return navigationService.NavigateToAsync<ExtendedSplashViewModel>();  // this page is used for preprocessing work
            return navigationService.InitializeAsync();
        }

        protected override void OnStart()
        {
            AppCenter.Start("ios=51232fc7-be85-4adb-99d3-e1c8f6f3db2f" +
                   "uwp=7529a611-c100-4257-872f-a9315b2161dc" +
                    "android=7edb0700-5702-4890-9beb-3561942dd6f4",
                  // "android= 24b17e2f-b607-4143-abd0-60877acd4676",

                  
                   typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
