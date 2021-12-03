using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Workorder
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WorkOrderStockRoomPartsListingForIOS : ContentPage
	{
		public WorkOrderStockRoomPartsListingForIOS ()
		{
			InitializeComponent ();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }
        public ViewModel.Workorder.WorkOrderStockroomPartsListingPageViewModelForIOS ViewModel
        {
            get
            {
                return this.BindingContext as ViewModel.Workorder.WorkOrderStockroomPartsListingPageViewModelForIOS;
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is ViewModel.Miscellaneous.IHandleViewAppearing viewAware)
            {
                ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
                ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
                await viewAware.OnViewAppearingAsync(this);
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            if (BindingContext is ViewModel.Miscellaneous.IHandleViewDisappearing viewAware)
            {
                await viewAware.OnViewDisappearingAsync(this);
            }
        }
    }
}