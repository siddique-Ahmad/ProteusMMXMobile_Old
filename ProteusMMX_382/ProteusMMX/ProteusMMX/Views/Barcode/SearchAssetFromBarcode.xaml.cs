using ProteusMMX.Model.AssetModel;
using ProteusMMX.ViewModel.Barcode;
using ProteusMMX.ViewModel.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Barcode
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchAssetFromBarcode : ContentPage
	{
		public SearchAssetFromBarcode ()
		{
			InitializeComponent ();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#85C1E9");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
        }
        public SearchAssetFromBarcodeViewModel ViewModel
        {
            get
            {
                return this.BindingContext as SearchAssetFromBarcodeViewModel;
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

    }
}