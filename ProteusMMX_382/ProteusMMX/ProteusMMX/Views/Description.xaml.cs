using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel;
using ProteusMMX.Model.WorkOrderModel;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Description : ContentPage
	{
		public Description ()
		{
			InitializeComponent ();
            // BindingContext = new DescriptionViewModel();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#85C1E9");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
        }

        public DescriptionViewModel ViewModel
        {
            get
            {
                return this.BindingContext as DescriptionViewModel;
            }
        }
    }
}