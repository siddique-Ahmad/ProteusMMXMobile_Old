using ProteusMMX.Views.Inventory.Templates.ViewCells;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProteusMMX.Views.Inventory.Templates
{
    public class PartListingPageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PartTemplate { get; set; }



        public object ParentBindingContext;
        public PartListingPageTemplateSelector()
        {
            //PastCurrentDayTemplate = new DataTemplate(typeof(PastViewCell));
            //TomorrowDayTemplate = new DataTemplate(typeof(TomorrowViewCell));
            //DayAfterTomorrowTemplate = new DataTemplate(typeof(DayAfterTomorrowViewCell));

            PartTemplate = new DataTemplate(() =>
            {
                return new PartViewcell(ref ParentBindingContext);

            });



        }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ParentBindingContext = container.BindingContext;
            return PartTemplate;
        }
    }
}
