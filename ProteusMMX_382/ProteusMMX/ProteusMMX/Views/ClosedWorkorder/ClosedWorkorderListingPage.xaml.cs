using ProteusMMX.Model.ClosedWorkOrderModel;
using ProteusMMX.ViewModel.ClosedWorkorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.ClosedWorkorder
{
	 [XamlCompilation(XamlCompilationOptions.Skip)]
	public partial class ClosedWorkorderListingPage : ContentPage
	{
		public ClosedWorkorderListingPage ()
		{
			InitializeComponent ();
            NavigationPage.SetBackButtonTitle(this, "");
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }
        public ClosedWorkorderListingPageViewModel ViewModel
        {
            get
            {
                return this.BindingContext as ClosedWorkorderListingPageViewModel;
            }
        }
        private async void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {

            var item = e.Item as ClosedWorkOrder;
            if (ViewModel.PickerItemCollection.Count == 0)
            {
                return;
            }

            //hit bottom!
            if (item == ViewModel.PickerItemCollection[ViewModel.PickerItemCollection.Count - 1])
            {
                //Add More items to collection
                await ViewModel.GetClosedWorkordersAuto();

            }

        }

        private async void filterText_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            if (string.IsNullOrEmpty(searchBar.Text))
            {
                await ViewModel.RemoveAllClosedWorkorderFromCollection();
                await ViewModel.ListingClosedWorkorderCollection();
                //await ViewModel.RefillClosedWorkorderCollection();
            }
        }
    }
}