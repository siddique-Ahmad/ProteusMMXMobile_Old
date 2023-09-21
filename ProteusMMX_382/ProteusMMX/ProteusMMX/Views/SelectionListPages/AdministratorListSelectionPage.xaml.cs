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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdministratorListSelectionPage : ContentPage
    {
        public AdministratorListSelectionPage()
        {
            InitializeComponent();
        }
        public AdministratorListSelectionPageViewModel ViewModel
        {
            get
            {
                return this.BindingContext as AdministratorListSelectionPageViewModel;
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