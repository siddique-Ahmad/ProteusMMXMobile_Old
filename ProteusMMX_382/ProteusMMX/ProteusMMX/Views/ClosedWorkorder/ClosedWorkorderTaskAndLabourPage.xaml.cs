using ProteusMMX.ViewModel.ClosedWorkorder;
using ProteusMMX.ViewModel.Miscellaneous;
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
	public partial class ClosedWorkorderTaskAndLabourPage : ContentPage
	{
		public ClosedWorkorderTaskAndLabourPage ()
		{
			InitializeComponent ();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#85C1E9");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
        }
        public ClosedWorkorderTaskAndLabourPageViewModel ViewModel
        {
            get
            {
                return this.BindingContext as ClosedWorkorderTaskAndLabourPageViewModel;
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