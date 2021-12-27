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
	public partial class BarcodeDashboard : ContentPage
	{
		public BarcodeDashboard ()
		{
			InitializeComponent ();
			((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
			((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
		}
	}
}