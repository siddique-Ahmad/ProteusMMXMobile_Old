using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.ServiceRequest;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.ServiceRequest
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ServiceRequestAttachmentPage : ContentPage
	{
		public ServiceRequestAttachmentPage ()
		{
			InitializeComponent ();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#85C1E9");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
        }
        public ServiceRequestAttachmentPageViewModel ViewModel
        {
            get
            {
                return this.BindingContext as ServiceRequestAttachmentPageViewModel;
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

        private void Next_Clicked(object sender, EventArgs e)
        {
            try
            {
               // this.CarouselView.Position += 1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void Previous_Clicked(object sender, EventArgs e)
        {
            try
            {
                //this.CarouselView.Position -= 1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void CarouselView_PositionSelected(object sender, SelectedPositionChangedEventArgs e)
        {
            try
            {

                //this.ImageCountLabel.Text = (Int32.Parse(e.SelectedPosition.ToString()) + 1) + "/" + vm.Zoos.Count;
            }
            catch (Exception)
            {
            }
        }
    }
}