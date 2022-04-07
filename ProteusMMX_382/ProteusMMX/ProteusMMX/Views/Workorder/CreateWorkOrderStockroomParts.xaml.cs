using ProteusMMX.ViewModel.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Workorder
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class CreateWorkOrderStockroomParts : ContentPage
    {
        public CreateWorkOrderStockroomParts()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
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

        private void UnitCost_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {

                Entry e1 = sender as Entry;
                String val = e1.Text; //Get Current Text

                if (val.Contains(" "))//If it is more than your character restriction
                {
                    val = val.Remove(val.Length - 1);// Remove Last character 
                    e1.Text = val; //Set the Old value
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(val, "^[0-9.]*$"))
                {
                    val = val.Remove(val.Length - 1);// Remove Last character 
                    e1.Text = val; //Set the Old value
                }


            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void QuantityRequired_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Entry e1 = sender as Entry;
                String val = e1.Text; //Get Current Text

                if (val.Contains(" "))//If it is more than your character restriction
                {
                    val = val.Remove(val.Length - 1);// Remove Last character 
                    e1.Text = val; //Set the Old value
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(val, "^[0-9.]*$"))
                {
                    val = val.Remove(val.Length - 1);// Remove Last character 
                    e1.Text = val; //Set the Old value
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void QuantityAllocated_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Entry e1 = sender as Entry;
                String val = e1.Text; //Get Current Text

                if (val.Contains(" "))//If it is more than your character restriction
                {
                    val = val.Remove(val.Length - 1);// Remove Last character 
                    e1.Text = val; //Set the Old value
                }
                if (!System.Text.RegularExpressions.Regex.IsMatch(val, "^[0-9.]*$"))
                {
                    val = val.Remove(val.Length - 1);// Remove Last character 
                    e1.Text = val; //Set the Old value
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}