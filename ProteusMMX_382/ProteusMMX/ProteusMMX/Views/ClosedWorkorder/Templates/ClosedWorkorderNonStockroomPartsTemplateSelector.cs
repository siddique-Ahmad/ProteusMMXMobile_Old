using ProteusMMX.Views.ClosedWorkorder.Templates.ViewCells;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProteusMMX.Views.ClosedWorkorder.Templates
{
    public class ClosedWorkorderNonStockroomPartsTemplateSelector:DataTemplateSelector
    {

        public DataTemplate NonStockroomTemplate { get; set; }



        public object ParentBindingContext;
        public ClosedWorkorderNonStockroomPartsTemplateSelector()
        {
            //PastCurrentDayTemplate = new DataTemplate(typeof(PastViewCell));
            //TomorrowDayTemplate = new DataTemplate(typeof(TomorrowViewCell));
            //DayAfterTomorrowTemplate = new DataTemplate(typeof(DayAfterTomorrowViewCell));

            NonStockroomTemplate = new DataTemplate(() =>
            {
                return new ClosedWorkorderNonStockroomPartsViewCell(ref ParentBindingContext);

            });



        }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ParentBindingContext = container.BindingContext;
            return NonStockroomTemplate;
        }
    }
}
