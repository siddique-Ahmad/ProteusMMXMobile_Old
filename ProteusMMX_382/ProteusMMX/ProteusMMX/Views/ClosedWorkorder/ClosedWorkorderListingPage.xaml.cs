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
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#85C1E9");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
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
    }
}