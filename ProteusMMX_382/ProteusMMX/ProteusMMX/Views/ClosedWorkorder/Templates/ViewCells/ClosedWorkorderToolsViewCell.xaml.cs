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
	public partial class ClosedWorkorderToolsViewCell : ViewCell
	{
		public ClosedWorkorderToolsViewCell (ref object ParentContext)
		{
			InitializeComponent ();
            this.ToolName.Text = WebControlTitle.GetTargetNameByTitleName("ToolName");
            this.ToolNumber.Text = WebControlTitle.GetTargetNameByTitleName("ToolNumber");
            this.ToolCribName.Text = WebControlTitle.GetTargetNameByTitleName("ToolCribName");
            this.ToolSize.Text = WebControlTitle.GetTargetNameByTitleName("ToolSize");
        }
        public static readonly BindableProperty ParentContextProperty =
       BindableProperty.Create("ParentContext", typeof(object), typeof(ClosedWorkorderToolsViewCell), null, propertyChanged: OnParentContextPropertyChanged);

        public object ParentContext
        {
            get { return GetValue(ParentContextProperty); }
            set { SetValue(ParentContextProperty, value); }
        }
      
        private static void OnParentContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                (bindable as ClosedWorkorderToolsViewCell).ParentContext = newValue;
            }
        }
    }
}