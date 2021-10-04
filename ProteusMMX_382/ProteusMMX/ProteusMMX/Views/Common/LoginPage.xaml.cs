using Microsoft.AppCenter.Crashes;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Translations;
using ProteusMMX.Services.Workorder;
using ProteusMMX.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Common
{
     [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class LoginPage : ContentPage
    {

        IAuthenticationService AuthService = Locator.Instance.Resolve<IAuthenticationService>();
        public LoginPage()
        {
            
            InitializeComponent();

            if (Device.Idiom == TargetIdiom.Phone)
            {
                Phone.IsVisible = true;
                Tablet.IsVisible = false;
            }
            else
            {
                Phone.IsVisible = false;
                Tablet.IsVisible = true;
            }
            //((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#85C1E9");
            //((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
            NavigationPage.SetHasBackButton(this, false);

          

            SiteURLEvent.Unfocused += async (sender, e) =>
            {
                try
                {
                    Entry siteurl = sender as Entry;

                    var FDALicensekey = await AuthService.GetFDAValidationAsync(siteurl.Text, null);
                    if (FDALicensekey != null)
                    {
                        if (FDALicensekey.FDAEnable && FDALicensekey.Signvalue == "True")
                        {
                            //LoginSwitch1.IsVisible = false;
                            LabelSwitch1.IsVisible = false;


                        }
                    }
                }
                catch (Exception ex)
                {

                    Crashes.TrackError(ex);
                }
                
                
            };
        }
        public LoginPageViewModel ViewModel
        {
            get
            {
                return this.BindingContext as LoginPageViewModel;
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            NavigationPage.SetHasBackButton(this, false); // I wanted just the button to be hidden

        }

        protected override bool OnBackButtonPressed()
        {
            
            Device.BeginInvokeOnMainThread(async () =>
            {
                System.Diagnostics.Process.GetCurrentProcess().Kill();
            });
            return true;
        }


    }
}