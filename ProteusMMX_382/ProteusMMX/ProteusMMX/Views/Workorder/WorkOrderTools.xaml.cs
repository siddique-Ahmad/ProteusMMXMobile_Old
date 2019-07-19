﻿using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.Workorder;
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
    public partial class WorkOrderTools : ContentPage
    {
        public WorkOrderTools()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#85C1E9");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
        }
        public WorkorderToolListingPageViewModel ViewModel
        {
            get
            {
                return this.BindingContext as WorkorderToolListingPageViewModel;
            }
        }

        private async void ListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {

            var item = e.Item as WorkOrderTool;
            if (ViewModel.ToolsCollection.Count == 0)
            {
                return;
            }

            //hit bottom!
            if (item == ViewModel.ToolsCollection[ViewModel.ToolsCollection.Count - 1])
            {
                //Add More items to collection

                await ViewModel.GetWorkorderToolsAuto();

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