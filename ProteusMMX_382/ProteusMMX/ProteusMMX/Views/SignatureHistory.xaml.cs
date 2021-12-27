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
	public partial class SignatureHistory : ContentPage
	{
		public SignatureHistory()
		{
			InitializeComponent ();
            // BindingContext = new DescriptionViewModel();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }

        public SignatureHistoryViewModel ViewModel
        {
            get
            {
                return this.BindingContext as SignatureHistoryViewModel;
            }
        }
    }
}