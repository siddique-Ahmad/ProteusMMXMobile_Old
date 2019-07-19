using ProteusMMX.Views.ClosedWorkorder.Templates.ViewCells;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProteusMMX.Views.ClosedWorkorder.Templates
{
    public class ClosedWorkorderListingPageTemplateSelector:DataTemplateSelector
    {
        public DataTemplate ClosedWorkorderTemplate { get; set; }



        public object ParentBindingContext;
        public ClosedWorkorderListingPageTemplateSelector()
        {
            //PastCurrentDayTemplate = new DataTemplate(typeof(PastViewCell));
            //TomorrowDayTemplate = new DataTemplate(typeof(TomorrowViewCell));
            //DayAfterTomorrowTemplate = new DataTemplate(typeof(DayAfterTomorrowViewCell));

            ClosedWorkorderTemplate = new DataTemplate(() =>
            {
                return new ClosedWorkorderViewCell(ref ParentBindingContext);

            });



        }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ParentBindingContext = container.BindingContext;
            return ClosedWorkorderTemplate;
        }
    }
}
