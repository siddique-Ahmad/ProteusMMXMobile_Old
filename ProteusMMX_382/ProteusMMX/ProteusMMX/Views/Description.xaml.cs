using ProteusMMX.ViewModel;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Description : ContentPage
    {
        public Description()
        {
            InitializeComponent();
            NavigationPage.SetBackButtonTitle(this, "");
            // BindingContext = new DescriptionViewModel();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;

        }

        public DescriptionViewModel ViewModel
        {
            get
            {
                return this.BindingContext as DescriptionViewModel;
            }
        }
    }
}