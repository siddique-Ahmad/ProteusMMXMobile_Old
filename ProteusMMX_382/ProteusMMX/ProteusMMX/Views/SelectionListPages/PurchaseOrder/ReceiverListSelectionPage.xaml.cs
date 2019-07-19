using ProteusMMX.ViewModel.SelectionListPagesViewModels.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.SelectionListPages.PurchaseOrder
{
	 [XamlCompilation(XamlCompilationOptions.Skip)]
	public partial class ReceiverListSelectionPage : ContentPage
	{
		public ReceiverListSelectionPage ()
		{
			InitializeComponent ();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#85C1E9");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
        }
        public ReceiverListSelectionPageViewModel ViewModel
        {
            get
            {
                return this.BindingContext as ReceiverListSelectionPageViewModel;
            }
        }
        private async void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {

            var item = e.Item;
            if (ViewModel.PickerItemCollection.Count == 0)
            {
                return;
            }

            //hit bottom!
            if (item == ViewModel.PickerItemCollection[ViewModel.PickerItemCollection.Count - 1])
            {
                //Add More items to collection
                //await ViewModel.GetPickerItemsAuto();

            }

        }
    }
}