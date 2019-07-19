using ProteusMMX.ViewModel.Miscellaneous;
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
	public partial class EditServiceRequest : ContentPage
	{
		public EditServiceRequest ()
		{
			InitializeComponent ();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#85C1E9");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
            if (AppSettings.User.blackhawkLicValidator.IsFDASignatureValidation)
            {
                if (AppSettings.User.RequireSignaturesForValidation == "True")
                {

                }
                else
                {
                    this.SaveButton.SetValue(Grid.RowProperty, 3);
                }
            }
            else
            {
                this.SaveButton.SetValue(Grid.RowProperty, 3);
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
    }
}