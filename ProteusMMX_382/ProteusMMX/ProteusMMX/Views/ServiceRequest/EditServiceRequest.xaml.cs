﻿using ProteusMMX.ViewModel.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.ServiceRequest
{
	 [XamlCompilation(XamlCompilationOptions.Skip)]
	public partial class EditServiceRequest : ContentPage
	{
		public EditServiceRequest ()
		{
			InitializeComponent ();
            NavigationPage.SetBackButtonTitle(this, "");
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            if (AppSettings.User.blackhawkLicValidator.IsFDASignatureValidation)
            {
                if (AppSettings.User.RequireSignaturesForValidation == "True")
                {
                    this.Grid_column2.SetValue(Grid.RowProperty, 3);
                }
                else
                {
                    this.Grid_column2.SetValue(Grid.RowProperty, 2);
                }
            }
            else
            {
                this.Grid_column2.SetValue(Grid.RowProperty, 2);
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
    }
}