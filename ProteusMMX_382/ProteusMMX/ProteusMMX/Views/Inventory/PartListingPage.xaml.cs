using ProteusMMX.Model.InventoryModel;
using ProteusMMX.ViewModel.Inventory;
using ProteusMMX.ViewModel.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Inventory
{
	 [XamlCompilation(XamlCompilationOptions.Skip)]
	public partial class PartListingPage : ContentPage
	{
		public PartListingPage ()
		{
			InitializeComponent ();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }
        public PartListingPageViewModel ViewModel
        {
            get
            {
                return this.BindingContext as PartListingPageViewModel;
            }
        }
        private async void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {

            var item = e.Item as StockroomPart;
            if (ViewModel.StockroomPartsCollection.Count == 0)
            {
                return;
            }

            //hit bottom!
            if (item == ViewModel.StockroomPartsCollection[ViewModel.StockroomPartsCollection.Count - 1])
            {
                //Add More items to collection

                await ViewModel.GetStockroomPartsAuto();

            }





        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

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
    }
}