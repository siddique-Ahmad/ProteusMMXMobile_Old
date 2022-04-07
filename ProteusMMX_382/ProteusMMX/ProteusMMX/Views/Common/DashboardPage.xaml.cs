using ProteusMMX.ViewModel;
using ProteusMMX.ViewModel.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Common
{
	 [XamlCompilation(XamlCompilationOptions.Skip)]
	public partial class DashboardPage : ContentPage
	{
		public DashboardPage ()
		{
			InitializeComponent ();
            NavigationPage.SetBackButtonTitle(this, "");
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }
        public DashboardPageViewModel ViewModel
        {
            get
            {
                return this.BindingContext as DashboardPageViewModel;
            }
        }

        protected override bool OnBackButtonPressed()
        {

            Device.BeginInvokeOnMainThread(async () =>
            {
                var result = await DisplayAlert("Alert!", "Please Logout from app if u really want to exit","OK","Cancel");
              //  if (result) await Navigation.PopToRootAsync(); // or anything else
            });

            return true;


        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            BarcodeColor.BackgroundColor = Color.Transparent;
            AssetsColor.BackgroundColor = Color.Transparent;
            ServiceRequestColor.BackgroundColor = Color.Transparent;
            InventoryColor.BackgroundColor = Color.Transparent;
            CloseWorkordersColor.BackgroundColor = Color.Transparent;
            WorkordersColor.BackgroundColor = Color.Transparent;
            KPIColor.BackgroundColor = Color.Transparent;
            ReceivingColor.BackgroundColor = Color.Transparent;
            if (BindingContext is IHandleViewAppearing viewAware)
            {
                ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
                ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
                await viewAware.OnViewAppearingAsync(this);
            }
        }

        protected override async void OnDisappearing()
        {
            
            base.OnDisappearing();
            
            if (BindingContext is IHandleViewDisappearing viewAware)
            {
                await viewAware.OnViewDisappearingAsync(this);
            }
        }

        private void ReceivingBGColor_Change(object sender, EventArgs e)
        {
            ReceivingColor.BackgroundColor = Color.FromHex("#006de0");
        }

        private void KPIBGColor_Change(object sender, EventArgs e)
        {
            KPIColor.BackgroundColor = Color.FromHex("#006de0");
        }

        private void WorkordersBGColor_Change(object sender, EventArgs e)
        {
            WorkordersColor.BackgroundColor = Color.FromHex("#006de0");
        }

        private void CloseWorkordersBGColor_Change(object sender, EventArgs e)
        {
            CloseWorkordersColor.BackgroundColor = Color.FromHex("#006de0");
        }

        private void InventoryBGColor_Change(object sender, EventArgs e)
        {
            InventoryColor.BackgroundColor = Color.FromHex("#006de0");
        }

        private void ServiceRequestBGColor_Change(object sender, EventArgs e)
        {
            ServiceRequestColor.BackgroundColor = Color.FromHex("#006de0");
        }

        private void AssetBGColor_Change(object sender, EventArgs e)
        {
            AssetsColor.BackgroundColor = Color.FromHex("#006de0");
        }

        private void BarcodeBGColor_Change(object sender, EventArgs e)
        {
            BarcodeColor.BackgroundColor = Color.FromHex("#006de0");
        }

    }
}