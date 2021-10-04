using ProteusMMX.ViewModel.Barcode;
using ProteusMMX.ViewModel.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Barcode
{
	 [XamlCompilation(XamlCompilationOptions.Skip)]
	public partial class SearchAssetForAttachment : ContentPage
	{
		public SearchAssetForAttachment ()
		{
			InitializeComponent ();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#85C1E9");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
        }
        public SearchAssetForAttachmentViewModel ViewModel
        {
            get
            {
                return this.BindingContext as SearchAssetForAttachmentViewModel;
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
                //this.CarouselView.Position += 1;
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
               // this.CarouselView.Position -= 1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        
    }
}