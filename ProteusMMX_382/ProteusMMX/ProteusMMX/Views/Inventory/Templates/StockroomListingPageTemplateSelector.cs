using ProteusMMX.Views.Inventory.Templates.ViewCells;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProteusMMX.Views.Inventory.Templates
{
    public class StockroomListingPageTemplateSelector: DataTemplateSelector
    {
        public DataTemplate StockroomTemplate { get; set; }



        public object ParentBindingContext;
        public StockroomListingPageTemplateSelector()
        {
            //PastCurrentDayTemplate = new DataTemplate(typeof(PastViewCell));
            //TomorrowDayTemplate = new DataTemplate(typeof(TomorrowViewCell));
            //DayAfterTomorrowTemplate = new DataTemplate(typeof(DayAfterTomorrowViewCell));

            StockroomTemplate = new DataTemplate(() =>
            {
                return new StockroomViewCell(ref ParentBindingContext);

            });



        }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ParentBindingContext = container.BindingContext;
            return StockroomTemplate;
        }
    }
}
