using ProteusMMX.Views.PurchaseOrder.Templates.ViewCells;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProteusMMX.Views.PurchaseOrder.Templates
{
    public class PurchaseOrderPartsListingTemplateSelector : DataTemplateSelector
    {

        public DataTemplate PurchaseOrderPartsTemplate { get; set; }



        public object ParentBindingContext;
        public PurchaseOrderPartsListingTemplateSelector()
        {
            //PastCurrentDayTemplate = new DataTemplate(typeof(PastViewCell));
            //TomorrowDayTemplate = new DataTemplate(typeof(TomorrowViewCell));
            //DayAfterTomorrowTemplate = new DataTemplate(typeof(DayAfterTomorrowViewCell));

            PurchaseOrderPartsTemplate = new DataTemplate(() =>
            {
                return new PurchaseOrderPartsViewCell(ref ParentBindingContext);

            });



        }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ParentBindingContext = container.BindingContext;
            return PurchaseOrderPartsTemplate;
        }
    }
}
