using ProteusMMX.Helpers;
using ProteusMMX.ViewModel.ClosedWorkorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.ClosedWorkorder.Templates.ViewCells
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ClosedWorkorderStockroomPartsViewCell : ViewCell
	{
		public ClosedWorkorderStockroomPartsViewCell (ref object ParentContext)
		{
			InitializeComponent ();
            this.PartName.Text = WebControlTitle.GetTargetNameByTitleName("PartName");
            this.PartNumber.Text = WebControlTitle.GetTargetNameByTitleName("PartNumber");
            this.QuantityRequired.Text = WebControlTitle.GetTargetNameByTitleName("QuantityRequired");
            this.QuantityAllocated.Text = WebControlTitle.GetTargetNameByTitleName("QuantityAllocated");
        }
        public static readonly BindableProperty ParentContextProperty =
  BindableProperty.Create("ParentContext", typeof(object), typeof(ClosedWorkorderStockroomPartsViewCell), null, propertyChanged: OnParentContextPropertyChanged);

        public object ParentContext
        {
            get { return GetValue(ParentContextProperty); }
            set { SetValue(ParentContextProperty, value); }
        }

        private static void OnParentContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                (bindable as ClosedWorkorderStockroomPartsViewCell).ParentContext = newValue;
            }
        }
    }
}