using ProteusMMX.Helpers;
using ProteusMMX.ViewModel.Miscellaneous;
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
    public partial class WorkOrderPartTab : TabbedPage
    {
        public WorkOrderPartTab()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
            this.CurrentPageChanged += (object sender, EventArgs e) => {
                var index = this.Children.IndexOf(this.CurrentPage);
                var CurrentPage = this.Children.ElementAt(index);
                string Selectedpage = CurrentPage.ToString();
                if (Selectedpage.Contains("WorkOrderStockRoomPartsListing"))
                {
                    //this.Title = WebControlTitle.GetTargetNameByTitleName("Details");
                }
                else if (Selectedpage.Contains("WorkOrderNonStockRoomPartsListing"))
                {
                    //this.Title = WebControlTitle.GetTargetNameByTitleName("TasksandLabor");
                }
                
                else
                {

                }

            };
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