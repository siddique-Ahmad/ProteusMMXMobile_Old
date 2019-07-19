﻿using ProteusMMX.ViewModel.ClosedWorkorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.ClosedWorkorder
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClosedWorkorderStockroomParts : ContentPage
	{
		public ClosedWorkorderStockroomParts ()
		{
			InitializeComponent ();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#85C1E9");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
        }
        public ClosedWorkorderStockroomPartsViewModel ViewModel
        {
            get
            {
                return this.BindingContext as ClosedWorkorderStockroomPartsViewModel;
            }
        }
    }
}