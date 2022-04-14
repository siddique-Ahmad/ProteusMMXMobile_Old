using Acr.UserDialogs;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.Workorder;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Workorder
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class WorkorderListingPage : ContentPage
    {
        public WorkorderListingPage()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }

        public WorkorderListingPageViewModel ViewModel
        {
            get
            {
                return this.BindingContext as WorkorderListingPageViewModel;
            }
        }

        private async void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {

            var item = e.Item as workOrders;
            if (ViewModel.WorkordersCollection.Count == 0)
            {
                return;
            }

            //hit bottom!
            if (item == ViewModel.WorkordersCollection[ViewModel.WorkordersCollection.Count - 1])
            {
                //Add More items to collection

                await ViewModel.GetWorkordersAuto();

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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            TargetNavigationData tnobj = new TargetNavigationData();
            tnobj.ViewModel = ViewModel;
            tnobj.WorkorderService = ViewModel._workorderService;
            var morepage = new ShowMore(tnobj);
            await Navigation.PushPopupAsync(morepage);

        }

        private void Picker_Focused(object sender, FocusEventArgs e)
        {

        }
        int count = 0;
        private async void filterText_TextChanged(object sender, TextChangedEventArgs e)
        {

            SearchBar searchBar = (SearchBar)sender;
            if (string.IsNullOrEmpty(searchBar.Text))
            {
                count = count + 1;
                if (count == 1)
                {
                    await ViewModel.searchBoxTextCler();

                }
                else
                {
                    count = 0;
                }

            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                UserDialogs.Instance.ShowLoading("Please wait..", MaskType.Gradient);
                await Task.Delay(10);
                await ViewModel.ClearIconClick();
                await ViewModel.RefillWorkorderCollection();
            }
            catch (Exception ex)
            {

                UserDialogs.Instance.HideLoading();
            }

            finally
            {
                UserDialogs.Instance.HideLoading();
            }
        }

    }
}