using ProteusMMX.Model.CommonModels;
using ProteusMMX.ViewModel;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.Workorder;
using Syncfusion.XForms.Buttons;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Workorder
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class EditWorkorderPage : ContentPage
    {
        public EditWorkorderPage()
        {
            InitializeComponent();

            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            if (AppSettings.User.blackhawkLicValidator.IsFDASignatureValidation)
            {
                if (AppSettings.User.RequireSignaturesForValidation == "True")
                {
                    this.Grid_column2.SetValue(Grid.RowProperty, 4);
                }
                else
                {
                    this.Grid_column2.SetValue(Grid.RowProperty, 3);
                }
            }
            else
            {
                this.Grid_column2.SetValue(Grid.RowProperty, 3);
            }

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is IHandleViewAppearing viewAware)
            {
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

        private void NumericEntry_TextChanged(object sender, TextChangedEventArgs e)
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

        private void NumericEntry_TextChanged_1(object sender, TextChangedEventArgs e)
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

        private void NumericEntry_TextChanged_2(object sender, TextChangedEventArgs e)
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

        private void NumericEntry_TextChanged_3(object sender, TextChangedEventArgs e)
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

        EditWorkorderPageViewModel ViewModel => this.BindingContext as EditWorkorderPageViewModel;

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            TargetNavigationData tnobj = new TargetNavigationData();
            tnobj.AssetSystemID = ViewModel.AssetSystemID;
            tnobj.AssetSystemName = ViewModel.AssetSystemName;
            tnobj.AssetSystemNumber = ViewModel.AssetSystemNumber;
            ViewModel._navigationService.NavigateToAsync<ShowAssetSystemViewModel>(tnobj);
        }
        private void RadioButton_StateChanged(object sender, StateChangedEventArgs e)
        {
            if (RadioButton.IsChecked == false)
            {
                return;
            }
            else
            {
                Button1.IsChecked = false;
            }

        }
        private void Button1_StateChanged(object sender, StateChangedEventArgs e)
        {
            if (Button1.IsChecked == false)
            {
                return;
            }
            else
            {
                RadioButton.IsChecked = false;
            }

        }
    }
}