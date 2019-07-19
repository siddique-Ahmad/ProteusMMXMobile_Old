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
	public partial class PartViewcell : ViewCell
	{
		public PartViewcell (ref object ParentContext)
		{
			InitializeComponent ();
            string ShelfBinKey = "";
            this.PartName.Text = WebControlTitle.GetTargetNameByTitleName("PartName");
            this.PartNumber.Text = WebControlTitle.GetTargetNameByTitleName("PartNumber");
            this.QuantityOnHand.Text = WebControlTitle.GetTargetNameByTitleName("QuantityOnHand");
            this.QuantityAllocated.Text = WebControlTitle.GetTargetNameByTitleName("QuantityAllocated");
            this.SerialNumber.Text = WebControlTitle.GetTargetNameByTitleName("SerialNumber");
            
            if (Application.Current.Properties.ContainsKey("ShelfBinKey"))
            {
                ShelfBinKey = Application.Current.Properties["ShelfBinKey"].ToString();
            }
            if (ShelfBinKey == "E" || ShelfBinKey == "V")
            {
                this.ShelfBin.Text = WebControlTitle.GetTargetNameByTitleName("ShelfBin");
                this.ShelfBinColon.IsVisible = true;
            }

        }
        public static readonly BindableProperty ParentContextProperty =
     BindableProperty.Create("ParentContext", typeof(object), typeof(PartViewcell), null, propertyChanged: OnParentContextPropertyChanged);

        public object ParentContext
        {
            get { return GetValue(ParentContextProperty); }
            set { SetValue(ParentContextProperty, value); }
        }

        private static void OnParentContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                (bindable as PartViewcell).ParentContext = newValue;
            }
        }
    }
}