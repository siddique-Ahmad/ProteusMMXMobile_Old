using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Barcode
{
	 [XamlCompilation(XamlCompilationOptions.Skip)]
	public partial class SearchAssetByAssetNumber : ContentPage
	{
		public SearchAssetByAssetNumber ()
		{
			InitializeComponent ();
			NavigationPage.SetBackButtonTitle(this, "");
			((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
			((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
		}
	}
}