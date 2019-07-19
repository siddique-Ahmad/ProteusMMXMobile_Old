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
	public partial class PurchaseOrderPartsViewCell : ViewCell
	{
		public PurchaseOrderPartsViewCell (ref object ParentContext)
		{
			InitializeComponent ();
            this.PartName.Text = WebControlTitle.GetTargetNameByTitleName("PartName");
            this.PartNumber.Text = WebControlTitle.GetTargetNameByTitleName("PartNumber");
            this.BalanceDue.Text = WebControlTitle.GetTargetNameByTitleName("BalanceDue");
            this.QuantityOrdered.Text = WebControlTitle.GetTargetNameByTitleName("QuantityOrdered");
            this.QuantityReceived.Text = WebControlTitle.GetTargetNameByTitleName("QuantityReceived");
            this.ReceiverName.Text = WebControlTitle.GetTargetNameByTitleName("ReceiverName");
            this.ReceivedDate.Text = WebControlTitle.GetTargetNameByTitleName("ReceivedDate");
            this.ShelfBin.Text = WebControlTitle.GetTargetNameByTitleName("ShelfBin");
        }
        public static readonly BindableProperty ParentContextProperty =
    BindableProperty.Create("ParentContext", typeof(object), typeof(PurchaseOrderPartsViewCell), null, propertyChanged: OnParentContextPropertyChanged);

        public object ParentContext
        {
            get { return GetValue(ParentContextProperty); }
            set { SetValue(ParentContextProperty, value); }
        }

        private static void OnParentContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                (bindable as PurchaseOrderPartsViewCell).ParentContext = newValue;
            }
        }
    }
}