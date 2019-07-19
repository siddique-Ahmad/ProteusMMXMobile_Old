using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.PurchaseOrder
{
     [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class PuchaseOrderTabbedPage : TabbedPage
    {
        public PuchaseOrderTabbedPage ()
        {
            InitializeComponent();
            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#85C1E9");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
        }
    }
}