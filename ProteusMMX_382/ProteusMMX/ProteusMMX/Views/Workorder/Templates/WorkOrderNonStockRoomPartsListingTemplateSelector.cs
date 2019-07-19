using ProteusMMX.Views.Workorder.Templates.ViewCells;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProteusMMX.Views.Workorder.Templates
{
    public class WorkOrderNonStockRoomPartsListingTemplateSelector : DataTemplateSelector
    {
        public DataTemplate WorkOrdernonstkpartTemplate { get; set; }



        public object ParentBindingContext;
        public WorkOrderNonStockRoomPartsListingTemplateSelector()
        {
            //PastCurrentDayTemplate = new DataTemplate(typeof(PastViewCell));
            //TomorrowDayTemplate = new DataTemplate(typeof(TomorrowViewCell));
            //DayAfterTomorrowTemplate = new DataTemplate(typeof(DayAfterTomorrowViewCell));

            WorkOrdernonstkpartTemplate = new DataTemplate(() =>
            {
                return new WorkorderNonStockroomPartsViewCell(ref ParentBindingContext);

            });



        }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ParentBindingContext = container.BindingContext;
            return WorkOrdernonstkpartTemplate;
        }
    }
}
