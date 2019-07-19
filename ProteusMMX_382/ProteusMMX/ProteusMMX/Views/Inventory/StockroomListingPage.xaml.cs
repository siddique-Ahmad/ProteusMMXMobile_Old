﻿using ProteusMMX.Model.InventoryModel;
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
	public partial class StockroomListingPage : ContentPage
	{
		public StockroomListingPage ()
		{
			InitializeComponent ();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#85C1E9");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
        }
        public StockroomListingPageViewModel ViewModel
        {
            get
            {
                return this.BindingContext as StockroomListingPageViewModel;
            }
        }
        private async void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {

            var item = e.Item as Stockroom;
            if (ViewModel.StockroomsCollection.Count == 0)
            {
                return;
            }

            //hit bottom!
            if (item == ViewModel.StockroomsCollection[ViewModel.StockroomsCollection.Count - 1])
            {
                //Add More items to collection

                await ViewModel.GetStockroomsAuto();

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