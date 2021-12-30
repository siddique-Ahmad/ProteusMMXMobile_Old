using ProteusMMX.Model.CommonModels;
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
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;

            if (AppSettings.User.blackhawkLicValidator.IsFDASignatureValidation)
            {
                if (AppSettings.User.RequireSignaturesForValidation == "True")
                {
                    this.SignatureLayout.SetValue(Grid.RowProperty, 37);
                }
                else
                {
                   
                }
            }
            else
            {
                this.SignatureLayout.SetValue(Grid.RowProperty, 37);
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

        private void TapGestureRecognizer_Tapped(object sender, System.EventArgs e)
        {
            TargetNavigationData tnobj = new TargetNavigationData();
            tnobj.AssetSystemID = ViewModel.AssetSystemID;
            tnobj.AssetSystemName = ViewModel.AssetSystemName;
            tnobj.AssetSystemNumber = ViewModel.AssetSystemNumber;
            ViewModel._navigationService.NavigateToAsync<ShowAssetSystemViewModel>(tnobj);

        }
        private void RadioButton_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            if (RadioButton.IsChecked == false)
            {
                return;
            }
            else
            {
                Button1.IsChecked = false;
            }
        }

        private void Button1_StateChanged(object sender, Syncfusion.XForms.Buttons.StateChangedEventArgs e)
        {
            if (Button1.IsChecked == false)
            {
                return;
            }
            else
            {
                RadioButton.IsChecked = false;
            }

        }
    }
}