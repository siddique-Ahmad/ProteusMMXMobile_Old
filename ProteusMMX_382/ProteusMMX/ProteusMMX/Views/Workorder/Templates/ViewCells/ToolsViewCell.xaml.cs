using ProteusMMX.Helpers;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.ViewModel.Workorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Workorder.Templates.ViewCells
{
	 [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ToolsViewCell : ViewCell
    {
        public ToolsViewCell(ref object ParentContext)
        {
            InitializeComponent();
            this.ParentContext = ParentContext;
            this.ToolName.Text = WebControlTitle.GetTargetNameByTitleName("ToolName");
            this.ToolNumber.Text = WebControlTitle.GetTargetNameByTitleName("ToolNumber");
            this.ToolCribName.Text = WebControlTitle.GetTargetNameByTitleName("ToolCribName");
            this.ToolSize.Text= WebControlTitle.GetTargetNameByTitleName("ToolSize");
            string DeleteToolKeyValue = "";

            if (Application.Current.Properties.ContainsKey("DeleteToolKey"))
            {
                DeleteToolKeyValue = Application.Current.Properties["DeleteToolKey"].ToString();
            }
            if (DeleteToolKeyValue=="N")
            {
                RemoveImage.IsVisible = false;
            }
           
        }
      
        public static readonly BindableProperty ParentContextProperty =
         BindableProperty.Create("ParentContext", typeof(object), typeof(ToolsViewCell), null, propertyChanged: OnParentContextPropertyChanged);

        public object ParentContext
        {
            get { return GetValue(ParentContextProperty); }
            set { SetValue(ParentContextProperty, value); }
        }
       
        private static void OnParentContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                (bindable as ToolsViewCell).ParentContext = newValue;
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            string DeleteToolKeyValue = "";

            if (Application.Current.Properties.ContainsKey("DeleteToolKey"))
            {
                DeleteToolKeyValue = Application.Current.Properties["DeleteToolKey"].ToString();
            }
            if (DeleteToolKeyValue == "E")
            {
                var imageSender = (Image)sender;
                var a = imageSender.BindingContext as ProteusMMX.Model.WorkOrderModel.WorkOrderTool;
                (ParentContext as WorkorderToolListingPageViewModel).RemoveTool(a.WorkOrderToolID);
            }
        }
    }
}