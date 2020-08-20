using ProteusMMX.ViewModel;
using ProteusMMX.ViewModel.ClosedWorkorder;
using ProteusMMX.ViewModel.Miscellaneous;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.ClosedWorkorder
{
     [XamlCompilation(XamlCompilationOptions.Skip)]
	public partial class ClosedWorkorderDetailsPage : ContentPage
	{
		public ClosedWorkorderDetailsPage ()
		{
			InitializeComponent ();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#85C1E9");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;

            if (AppSettings.User.blackhawkLicValidator.IsFDASignatureValidation)
            {
                if (AppSettings.User.RequireSignaturesForValidation == "True")
                {
                    this.SignatureLayout.SetValue(Grid.RowProperty, 34);
                }
                else
                {
                   
                }
            }
            else
            {
                this.SignatureLayout.SetValue(Grid.RowProperty, 34);
            }


        }
        public ClosedWorkorderDetailsPageViewModel ViewModel
        {
            get
            {
                return this.BindingContext as ClosedWorkorderDetailsPageViewModel;
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