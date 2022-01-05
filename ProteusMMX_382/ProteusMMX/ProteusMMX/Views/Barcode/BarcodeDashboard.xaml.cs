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

		protected  override  void OnAppearing()
		{
			AssetColor.BackgroundColor = Color.Transparent;
			WorkorderByAssetColor.BackgroundColor = Color.Transparent;
			WorkorderByLocationColor.BackgroundColor = Color.Transparent;
			AssetForBillOfMaterialColor.BackgroundColor = Color.Transparent;
			AssetForAttachmentColor.BackgroundColor = Color.Transparent;
		}

		private void AssetBGColor_Change(object sender, EventArgs e)
        {
			AssetColor.BackgroundColor = Color.FromHex("#006de0");
		}

        private void WorkorderByAsset_Change(object sender, EventArgs e)
        {
			WorkorderByAssetColor.BackgroundColor = Color.FromHex("#006de0");
		}

		private void WorkorderByLocationColor_Change(object sender, EventArgs e)
		{
			WorkorderByLocationColor.BackgroundColor = Color.FromHex("#006de0");
		}
		private void AssetForBillOfMaterialColor_Change(object sender, EventArgs e)
		{
			AssetForBillOfMaterialColor.BackgroundColor = Color.FromHex("#006de0");
		}

		private void AssetForAttachmentColor_Change(object sender, EventArgs e)
		{
			AssetForAttachmentColor.BackgroundColor = Color.FromHex("#006de0");
		}
	}
}