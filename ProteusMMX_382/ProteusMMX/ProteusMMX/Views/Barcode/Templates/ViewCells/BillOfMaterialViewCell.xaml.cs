using ProteusMMX.Helpers;
using ProteusMMX.ViewModel.Barcode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Barcode.Templates.ViewCells
{
	 [XamlCompilation(XamlCompilationOptions.Skip)]
	public partial class BillOfMaterialViewCell : ViewCell
	{
		public BillOfMaterialViewCell (ref object ParentContext)
		{
			InitializeComponent ();
            this.PartName.Text = WebControlTitle.GetTargetNameByTitleName("PartName");
            this.PartNumber.Text = WebControlTitle.GetTargetNameByTitleName("PartNumber");
            this.PartSize.Text = WebControlTitle.GetTargetNameByTitleName("PartSize");
            this.Description.Text = WebControlTitle.GetTargetNameByTitleName("Description");
        }
        public static readonly BindableProperty ParentContextProperty =
    BindableProperty.Create("ParentContext", typeof(object), typeof(BillOfMaterialViewCell), null, propertyChanged: OnParentContextPropertyChanged);

        public object ParentContext
        {
            get { return GetValue(ParentContextProperty); }
            set { SetValue(ParentContextProperty, value); }
        }

        private static void OnParentContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                (bindable as BillOfMaterialViewCell).ParentContext = newValue;
            }
        }
    }
}