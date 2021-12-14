using ProteusMMX.DependencyInterface;
using ProteusMMX.ViewModel.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.ClosedWorkorder
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClosedWorkorderAttachments : ContentPage
	{
		public ClosedWorkorderAttachments ()
		{
			InitializeComponent ();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is IHandleViewAppearing viewAware)
            {
                ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
                ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
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

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            var a = button.BindingContext as ProteusMMX.ViewModel.ClosedWorkorder.WorkorderAttachment;
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
    }
}