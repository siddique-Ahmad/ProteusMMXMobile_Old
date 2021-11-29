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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WorkorderStockroomPartsTabbedPage : TabbedPage
    {
       
        public WorkorderStockroomPartsTabbedPage()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
            
            this.CurrentPageChanged += (object sender, EventArgs e) => {
               //ViewModel._navigationservice.InitializeAsync<WorkOrderNonStockroomPartsListingPageViewModel>(WorkorderID);
               // ViewModel.


            };
        }
        public WorkorderStockroomPartsTabbedPageViewModel ViewModel
        {
            get
            {
                return this.BindingContext as WorkorderStockroomPartsTabbedPageViewModel;
            }
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            Application.Current.Properties["WorkorderService"] = ViewModel._workorderService;

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