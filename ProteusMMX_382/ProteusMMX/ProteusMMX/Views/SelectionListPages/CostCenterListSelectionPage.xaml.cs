using ProteusMMX.ViewModel.SelectionListPagesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.SelectionListPages
{
	 [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class CostCenterListSelectionPage : ContentPage
    {
        public CostCenterListSelectionPage()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }

        public CostCenterListSelectionPageViewModel ViewModel
        {
            get
            {
                return this.BindingContext as CostCenterListSelectionPageViewModel;
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