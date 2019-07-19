using ProteusMMX.ViewModel.SelectionListPagesViewModels.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.SelectionListPages.Inventory
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class InventoryPerformByListSelectionPage : ContentPage
	{
		public InventoryPerformByListSelectionPage ()
		{
			InitializeComponent ();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#85C1E9");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
        }
        public InventoryPerformByListSelectionPageViewModel ViewModel
        {
            get
            {
                return this.BindingContext as InventoryPerformByListSelectionPageViewModel;
            }
        }
    }
}