using ProteusMMX.Model.ServiceRequestModel;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.ServiceRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.ServiceRequest
{
	 [XamlCompilation(XamlCompilationOptions.Skip)]
	public partial class ServiceRequestListingPage : ContentPage
	{
		public ServiceRequestListingPage ()
		{
			InitializeComponent ();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;

        }
        public ServiceRequestListingPageViewModel ViewModel
        {
            get
            {
                return this.BindingContext as ServiceRequestListingPageViewModel;
            }
        }

        private async void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {

            var item = e.Item as ServiceRequests;
            if (ViewModel.ServiceRequestCollection.Count == 0)
            {
                return;
            }

            //hit bottom!
            if (item == ViewModel.ServiceRequestCollection[ViewModel.ServiceRequestCollection.Count - 1])
            {
                //Add More items to collection

                await ViewModel.GetServiceRequestAuto();

            }

        }
        //int count = 0;
        private async void filterText_TextChanged(object sender, TextChangedEventArgs e)
        {

            SearchBar searchBar = (SearchBar)sender;
            if (string.IsNullOrEmpty(searchBar.Text))
            {
                //count = count + 1;
                //if (count == 1)
                //{
                    await ViewModel.OnViewDisappearingAsync(this);
                    await ViewModel.ReloadPageAfterSerchBoxCancle();
                //}
                //else
                //{
                //    count = 0;
                //}

            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is IHandleViewAppearing viewAware)
            {
                ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
                ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
                //Application.Current.Properties["ServiceRequestListingViewModel"] = ViewModel;
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