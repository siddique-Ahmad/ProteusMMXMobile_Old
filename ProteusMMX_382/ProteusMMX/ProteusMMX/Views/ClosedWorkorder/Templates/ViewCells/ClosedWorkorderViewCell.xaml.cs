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
	public partial class ClosedWorkorderViewCell : ViewCell
	{
		public ClosedWorkorderViewCell (ref object ParentContext)
		{
			InitializeComponent ();
            string DescriptionKeyValue = "";
            string WorkOrderTypeKeyValue = "";
            string PriorityKeyValue = "";
            this.WorkOrderNumberTitle.Text = WebControlTitle.GetTargetNameByTitleName("WorkorderNumber"); ;
            this.TargetNameTitle.Text = WebControlTitle.GetTargetNameByTitleName("Target") + " " + WebControlTitle.GetTargetNameByTitleName("Name");

            if (Application.Current.Properties.ContainsKey("PriorityKey"))
            {
                PriorityKeyValue = Application.Current.Properties["PriorityKey"].ToString();
            }
            if (PriorityKeyValue == "E" || PriorityKeyValue == "V")
            {

                this.PriorityColon.IsVisible = true;
            }

            this.WorkOrderTypeTitle.Text = WebControlTitle.GetTargetNameByTitleName("WorkOrderType");
            this.WorkorderTypeColon.IsVisible = true;
            //if (Application.Current.Properties.ContainsKey("WorkOrderTypeKey"))
            //{
            //    WorkOrderTypeKeyValue = Application.Current.Properties["WorkOrderTypeKey"].ToString();
            //}
            //if (WorkOrderTypeKeyValue == "E" || WorkOrderTypeKeyValue == "V")
            //{
            //    this.WorkOrderTypeTitle.Text = WebControlTitle.GetTargetNameByTitleName("WorkOrderType");
            //    this.WorkorderTypeColon.IsVisible = true;
            //}
            if (Application.Current.Properties.ContainsKey("DescriptionKey"))
            {
                DescriptionKeyValue = Application.Current.Properties["DescriptionKey"].ToString();
            }
            if (DescriptionKeyValue == "E" || DescriptionKeyValue == "V")
            {
                this.DescriptionTitle.Text = WebControlTitle.GetTargetNameByTitleName("Description");
                this.DescriptionVisible.IsVisible = true;
            }
        }
        public static readonly BindableProperty ParentContextProperty =
       BindableProperty.Create("ParentContext", typeof(object), typeof(ClosedWorkorderViewCell), null, propertyChanged: OnParentContextPropertyChanged);

        public object ParentContext
        {
            get { return GetValue(ParentContextProperty); }
            set { SetValue(ParentContextProperty, value); }
        }

        private static void OnParentContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                (bindable as ClosedWorkorderViewCell).ParentContext = newValue;
            }
        }
    }
}