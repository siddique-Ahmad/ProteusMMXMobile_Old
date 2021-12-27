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
    public partial class ServiceRequestTabbedPage : TabbedPage
    {
        public ServiceRequestTabbedPage ()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#006de0");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.White;
        }
    }
}