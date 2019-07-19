using ProteusMMX.Views.PurchaseOrder.Templates.ViewCells;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProteusMMX.Views.PurchaseOrder.Templates
{
    public class PurchaseOrderNonStockroomPartsListingTemplateSelector : DataTemplateSelector
    {

        public DataTemplate PurchaseOrderNonStockroomPartsTemplate { get; set; }



        public object ParentBindingContext;
        public PurchaseOrderNonStockroomPartsListingTemplateSelector()
        {
            //PastCurrentDayTemplate = new DataTemplate(typeof(PastViewCell));
            //TomorrowDayTemplate = new DataTemplate(typeof(TomorrowViewCell));
            //DayAfterTomorrowTemplate = new DataTemplate(typeof(DayAfterTomorrowViewCell));

            PurchaseOrderNonStockroomPartsTemplate = new DataTemplate(() =>
            {
                return new PurchaseOrderNonStockroomPartsViewCell(ref ParentBindingContext);

            });



        }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ParentBindingContext = container.BindingContext;
            return PurchaseOrderNonStockroomPartsTemplate;
        }
    }
}
