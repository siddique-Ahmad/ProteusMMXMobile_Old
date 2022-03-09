using ProteusMMX.Model.AssetModel;
using ProteusMMX.ViewModel;
using ProteusMMX.ViewModel.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ShowAssetSystem : ContentPage
	{
		public ShowAssetSystem ()
		{
			InitializeComponent ();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }

        public ShowAssetSystemViewModel ViewModel
        {
            get
            {
                return this.BindingContext as ShowAssetSystemViewModel;
            }
        }



        private async void  ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            var item = e.Item as AssetForAS;
            if (ViewModel.AssetForASCollection.Count == 0)
            {
                return;
            }

            //hit bottom!
            if (item == ViewModel.AssetForASCollection[ViewModel.AssetForASCollection.Count - 1])
            {
                //Add More items to collection

               await ViewModel.GetAssetsAuto();

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
                await viewAware.OnViewDisappearingAsync(this);
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
                    await ViewModel.searchBoxTextCler();

                }
                else
                {
                    count = 0;
                }

            }
        }
    }
}

