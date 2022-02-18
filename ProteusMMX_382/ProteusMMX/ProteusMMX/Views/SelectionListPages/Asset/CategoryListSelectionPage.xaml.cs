using ProteusMMX.ViewModel.SelectionListPagesViewModels.Asset;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.SelectionListPages.Asset
{
	 [XamlCompilation(XamlCompilationOptions.Skip)]
	public partial class CategoryListSelectionPage : ContentPage
	{
		public CategoryListSelectionPage ()
		{
			InitializeComponent ();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }
        public CategoryListSelectionPageViewModel ViewModel
        {
            get
            {
                return this.BindingContext as CategoryListSelectionPageViewModel;
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