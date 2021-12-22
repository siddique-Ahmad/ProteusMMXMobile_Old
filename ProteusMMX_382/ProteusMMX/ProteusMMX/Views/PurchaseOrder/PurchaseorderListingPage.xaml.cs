using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.PurchaseOrder
{
     [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class PurchaseorderListingPage : ContentPage
    {
        public PurchaseorderListingPage()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }
        public PurchaseorderListingPageViewModel ViewModel
        {
            get
            {
                return this.BindingContext as PurchaseorderListingPageViewModel;
            }
        }

        private async void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {

            var item = e.Item as Model.PurchaseOrderModel.PurchaseOrder;
            if (ViewModel.PurchaseOrderCollection.Count == 0)
            {
                return;
            }

            //hit bottom!
            if (item == ViewModel.PurchaseOrderCollection[ViewModel.PurchaseOrderCollection.Count - 1])
            {
                //Add More items to collection

                await ViewModel.GetPurchaseOrdersAuto();

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
    }
}