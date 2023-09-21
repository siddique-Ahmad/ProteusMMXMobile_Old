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
	public partial class SearchClosedWorkorderPage : ContentPage
	{
		public SearchClosedWorkorderPage ()
		{
			InitializeComponent ();
			NavigationPage.SetBackButtonTitle(this, "");
			((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
			((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
			((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
		}
	}
}