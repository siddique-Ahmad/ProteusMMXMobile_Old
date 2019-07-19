using ProteusMMX.Views.ClosedWorkorder.Templates.ViewCells;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProteusMMX.Views.ClosedWorkorder.Templates
{
    public class ClosedWorkorderStockroomPartsTemplateSelector:DataTemplateSelector
    {
        public DataTemplate StockroomTemplate { get; set; }



        public object ParentBindingContext;
        public ClosedWorkorderStockroomPartsTemplateSelector()
        {
            //PastCurrentDayTemplate = new DataTemplate(typeof(PastViewCell));
            //TomorrowDayTemplate = new DataTemplate(typeof(TomorrowViewCell));
            //DayAfterTomorrowTemplate = new DataTemplate(typeof(DayAfterTomorrowViewCell));

            StockroomTemplate = new DataTemplate(() =>
            {
                return new ClosedWorkorderStockroomPartsViewCell(ref ParentBindingContext);

            });



        }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ParentBindingContext = container.BindingContext;
            return StockroomTemplate;
        }

    }
}
