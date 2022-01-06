using ProteusMMX.DependencyInterface;
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
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
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

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            var a = button.BindingContext as ProteusMMX.ViewModel.Barcode.WorkorderAttachment;
            var fileextension = a.attachmentFileExtension;

            if (fileextension != null &&
                                   (fileextension.ToLower().Contains(".pdf") ||
                                   fileextension.ToLower().Contains(".doc") ||
                                   fileextension.ToLower().Contains(".docx") ||
                                   fileextension.ToLower().Contains(".xls") ||
                                   fileextension.ToLower().Contains(".xlsx") ||
                                   fileextension.ToLower().Contains(".txt")))
            {
                switch (Device.RuntimePlatform)
                {
                    case Device.iOS:
                        DependencyService.Get<IPDFViewer>().OpenPDF(AppSettings.BaseURL + "/Inspection/Service/attachmentdisplay.ashx?filename=" + fileextension);
                        break;
                    case Device.Android:
                        DependencyService.Get<IPDFViewer>().OpenPDF(AppSettings.BaseURL + "/Inspection/Service/attachmentdisplay.ashx?filename=" + fileextension);

                        break;
                    case Device.UWP:
                        DependencyService.Get<IPDFViewer>().OpenPDF(AppSettings.BaseURL + "/Inspection/Service/attachmentdisplay.ashx?filename=" + fileextension);
                        break;
                    default:
                        break;
                }
            }
        }

        private async void filterText_TextChanged(object sender, TextChangedEventArgs e)
        {

            SearchBar searchBar = (SearchBar)sender;
            if (string.IsNullOrEmpty(searchBar.Text))
            {
                //count = count + 1;
                //if (count == 1)
                //{
                    await ViewModel.OnViewDisappearingAsync(null);
                    await ViewModel.RefillWorkorderCollection();
                //}
                //else
                //{
                //    count = 0;
                //}

            }
        }
    }
}