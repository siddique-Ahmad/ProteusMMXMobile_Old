using ProteusMMX.Model.WorkOrderModel;
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
	public partial class SearchWorkorderByAssetNumberFromBarcode : ContentPage
	{
		public SearchWorkorderByAssetNumberFromBarcode ()
		{
			InitializeComponent ();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }
        public SearchWorkorderByAssetNumberFromBarcodeViewModel ViewModel
        {
            get
            {
                return this.BindingContext as SearchWorkorderByAssetNumberFromBarcodeViewModel;
            }
        }

        private async void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {

            var item = e.Item as workOrders;
            if (ViewModel.WorkordersCollection.Count == 0)
            {
                return;
            }

            //hit bottom!
            if (item == ViewModel.WorkordersCollection[ViewModel.WorkordersCollection.Count - 1])
            {
                //Add More items to collection

                await ViewModel.GetWorkordersAuto();

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
    }
}