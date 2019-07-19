using ProteusMMX.Views.Workorder.Templates.ViewCells;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProteusMMX.Views.Workorder.Templates
{
    public class WorkOrderStockRoomPartsListingTemplateSelector : DataTemplateSelector
    {
        public DataTemplate WorkOrderstkpartTemplate { get; set; }



        public object ParentBindingContext;
        public WorkOrderStockRoomPartsListingTemplateSelector()
        {
            //PastCurrentDayTemplate = new DataTemplate(typeof(PastViewCell));
            //TomorrowDayTemplate = new DataTemplate(typeof(TomorrowViewCell));
            //DayAfterTomorrowTemplate = new DataTemplate(typeof(DayAfterTomorrowViewCell));

            WorkOrderstkpartTemplate = new DataTemplate(() =>
            {
                return new WorkorderstockroompartsViewCell(ref ParentBindingContext);

            });



        }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ParentBindingContext = container.BindingContext;
            return WorkOrderstkpartTemplate;
        }
    }
}
