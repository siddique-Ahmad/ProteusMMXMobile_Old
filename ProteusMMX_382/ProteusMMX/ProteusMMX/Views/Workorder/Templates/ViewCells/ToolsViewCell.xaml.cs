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
        }
      
        public static readonly BindableProperty ParentContextProperty =
         BindableProperty.Create("ParentContext", typeof(object), typeof(ToolsViewCell), null, propertyChanged: OnParentContextPropertyChanged);

        public object ParentContext
        {
            get { return GetValue(ParentContextProperty); }
            set { SetValue(ParentContextProperty, value); }
        }
        public async void RemoveTool(object sender, EventArgs e)
        {
            try
            {

                var menuItem = sender as MenuItem;
                await (ParentContext as WorkorderToolListingPageViewModel).RemoveTool(menuItem.CommandParameter as WorkOrderTool);
            }
            catch (Exception ex)
            {


            }
        }
        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            try
            {
                string DeleteToolKeyValue = "";

                if (Application.Current.Properties.ContainsKey("DeleteToolKey"))
                {
                    DeleteToolKeyValue = Application.Current.Properties["DeleteToolKey"].ToString();
                }

                ViewCell theViewCell = ((ViewCell)sender);

                var item = theViewCell.BindingContext as WorkOrderTool;
                theViewCell.ContextActions.Clear();
                if (item != null)
                {

                    MenuItem mn = new MenuItem();


                    if (DeleteToolKeyValue == "E")
                    {
                        mn.Clicked += RemoveTool;
                        mn.Text = WebControlTitle.GetTargetNameByTitleName("RemoveTool");
                        mn.CommandParameter = item;
                        theViewCell.ContextActions.Add(mn);
                    }
                    else if (DeleteToolKeyValue == "V")
                    {
                        //  mn.Clicked += RemoveTool;
                        mn.Text = WebControlTitle.GetTargetNameByTitleName("RemoveTool");
                        mn.CommandParameter = item;
                        theViewCell.ContextActions.Add(mn);
                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            { }
        }

        private static void OnParentContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                (bindable as ToolsViewCell).ParentContext = newValue;
            }
        }
    }
}