using ProteusMMX.Model.AssetModel;
using ProteusMMX.ViewModel.Asset;
using ProteusMMX.ViewModel.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Asset
{
	[XamlCompilation(XamlCompilationOptions.Skip)]
	public partial class AssetListingPage : ContentPage
	{
		public AssetListingPage ()
		{
			InitializeComponent ();
            NavigationPage.SetBackButtonTitle(this, "");
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }
        public AssetListingPageViewModel ViewModel
        {
            get
            {
                return this.BindingContext as AssetListingPageViewModel;
            }
        }

        private async void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {

            var item = e.Item as Assets;
            if (ViewModel.AssetsCollection.Count == 0)
            {
                return;
            }

            //hit bottom!
            if (item == ViewModel.AssetsCollection[ViewModel.AssetsCollection.Count - 1])
            {
                //Add More items to collection

                await ViewModel.GetAssetsAuto();

            }
        }
        int count = 0;
        private async void filterText_TextChanged(object sender, TextChangedEventArgs e)
        {

            SearchBar searchBar = (SearchBar)sender;
            if (string.IsNullOrEmpty(searchBar.Text))
            {
                count = count + 1;
                if (count == 1)
                {                    
                    await ViewModel.OnViewDisappearingAsync(this);
                    await ViewModel.ReloadPageAfterSerchBoxCancle(); 
                }
                else
                {
                    count = 0;
                }

            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is IHandleViewAppearing viewAware)
            {
                await viewAware.OnViewAppearingAsync(this);
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            if (BindingContext is IHandleViewDisappearing viewAware)
            {
                ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
                ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
                await viewAware.OnViewDisappearingAsync(this);
            }
        }
    }
}