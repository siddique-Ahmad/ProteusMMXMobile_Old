using ProteusMMX.Helpers;
using ProteusMMX.ViewModel.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Inventory.Templates.ViewCells
{
     [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StockroomViewCell : ViewCell
    {
        public StockroomViewCell(ref object ParentContext)
        {
            InitializeComponent();
            this.StockroomName.Text = WebControlTitle.GetTargetNameByTitleName("StockroomName");
            this.NumberOfParts.Text = WebControlTitle.GetTargetNameByTitleName("NumberOfParts");
            this.QuantityOnHand.Text = WebControlTitle.GetTargetNameByTitleName("QuantityOnHand");
            this.TotalQuantityAvailable.Text = WebControlTitle.GetTargetNameByTitleName("TotalQuantityAvailable");
        }
        public static readonly BindableProperty ParentContextProperty =
       BindableProperty.Create("ParentContext", typeof(object), typeof(StockroomViewCell), null, propertyChanged: OnParentContextPropertyChanged);

        public object ParentContext
        {
            get { return GetValue(ParentContextProperty); }
            set { SetValue(ParentContextProperty, value); }
        }

        private static void OnParentContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                (bindable as StockroomViewCell).ParentContext = newValue;
            }
        }
    }
}