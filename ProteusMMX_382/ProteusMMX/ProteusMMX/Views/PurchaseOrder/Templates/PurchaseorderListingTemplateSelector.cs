using ProteusMMX.Views.PurchaseOrder.Templates.ViewCells;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProteusMMX.Views.PurchaseOrder.Templates
{
    public class PurchaseorderListingTemplateSelector:DataTemplateSelector
    {

        public DataTemplate PurchaseOrderTemplate { get; set; }



        public object ParentBindingContext;
        public PurchaseorderListingTemplateSelector()
        {
            //PastCurrentDayTemplate = new DataTemplate(typeof(PastViewCell));
            //TomorrowDayTemplate = new DataTemplate(typeof(TomorrowViewCell));
            //DayAfterTomorrowTemplate = new DataTemplate(typeof(DayAfterTomorrowViewCell));

            PurchaseOrderTemplate = new DataTemplate(() =>
            {
                return new PurchaseorderViewCell(ref ParentBindingContext);

            });



        }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ParentBindingContext = container.BindingContext;
            return PurchaseOrderTemplate;
        }
    }
}
