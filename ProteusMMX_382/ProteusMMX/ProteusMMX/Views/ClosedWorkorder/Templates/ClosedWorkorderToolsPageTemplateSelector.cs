using ProteusMMX.Views.ClosedWorkorder.Templates.ViewCells;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProteusMMX.Views.ClosedWorkorder.Templates
{
    public class ClosedWorkorderToolsPageTemplateSelector:DataTemplateSelector
    {
        public DataTemplate ToolTemplate { get; set; }



        public object ParentBindingContext;
        public ClosedWorkorderToolsPageTemplateSelector()
        {
            //PastCurrentDayTemplate = new DataTemplate(typeof(PastViewCell));
            //TomorrowDayTemplate = new DataTemplate(typeof(TomorrowViewCell));
            //DayAfterTomorrowTemplate = new DataTemplate(typeof(DayAfterTomorrowViewCell));

            ToolTemplate = new DataTemplate(() =>
            {
                return new ClosedWorkorderToolsViewCell(ref ParentBindingContext);

            });



        }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ParentBindingContext = container.BindingContext;
            return ToolTemplate;
        }
    }
}
