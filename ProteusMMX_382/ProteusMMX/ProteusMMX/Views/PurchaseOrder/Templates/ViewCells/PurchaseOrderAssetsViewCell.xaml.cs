using ProteusMMX.Helpers;
using ProteusMMX.ViewModel.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.PurchaseOrder.Templates.ViewCells
{
	 [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PurchaseOrderAssetsViewCell : ViewCell
	{
		public PurchaseOrderAssetsViewCell (ref object ParentContext)
		{
			InitializeComponent ();
            this.AssetName.Text = WebControlTitle.GetTargetNameByTitleName("AssetName");
            this.AssetNumber.Text = WebControlTitle.GetTargetNameByTitleName("AssetNumber");
            this.ReceivedDate.Text = WebControlTitle.GetTargetNameByTitleName("ReceivedDate");
            this.ReceiverName.Text = WebControlTitle.GetTargetNameByTitleName("ReceiverName");
        }
        public static readonly BindableProperty ParentContextProperty =
    BindableProperty.Create("ParentContext", typeof(object), typeof(PurchaseOrderAssetsViewCell), null, propertyChanged: OnParentContextPropertyChanged);

        public object ParentContext
        {
            get { return GetValue(ParentContextProperty); }
            set { SetValue(ParentContextProperty, value); }
        }

        private static void OnParentContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                (bindable as PurchaseOrderAssetsViewCell).ParentContext = newValue;
            }
        }
    }
}