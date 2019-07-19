using ProteusMMX.ViewModel.SelectionListPagesViewModels.Workorder.Parts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.SelectionListPages.Workorder.Parts
{
	 [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class StockroomListSelectionPage : ContentPage
    {
        public StockroomListSelectionPage()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#85C1E9");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
        }

        public StockroomListSelectionPageViewModel ViewModel
        {
            get
            {
                return this.BindingContext as StockroomListSelectionPageViewModel;
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
                await ViewModel.GetPickerItemsAuto();

            }

        }
    }
}