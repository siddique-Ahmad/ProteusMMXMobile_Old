using ProteusMMX.ViewModel.Barcode;
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
	public partial class SearchAssetForBillOfMaterial : ContentPage
	{
		public SearchAssetForBillOfMaterial ()
		{
			InitializeComponent ();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }
        public SearchAssetForBillOfMaterialViewModel ViewModel
        {
            get
            {
                return this.BindingContext as SearchAssetForBillOfMaterialViewModel;
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
                await ViewModel.SearchBoxClerText();
                //}
                //else
                //{
                //    count = 0;
                //}

            }
        }
    }
}