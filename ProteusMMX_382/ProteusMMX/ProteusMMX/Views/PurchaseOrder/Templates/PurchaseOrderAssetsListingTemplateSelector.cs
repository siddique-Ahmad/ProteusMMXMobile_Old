using ProteusMMX.Views.PurchaseOrder.Templates.ViewCells;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProteusMMX.Views.PurchaseOrder.Templates
{
    public class PurchaseOrderAssetsListingTemplateSelector : DataTemplateSelector
    {

        public DataTemplate PurchaseOrderAssetsTemplate { get; set; }



        public object ParentBindingContext;
        public PurchaseOrderAssetsListingTemplateSelector()
        {
            //PastCurrentDayTemplate = new DataTemplate(typeof(PastViewCell));
            //TomorrowDayTemplate = new DataTemplate(typeof(TomorrowViewCell));
            //DayAfterTomorrowTemplate = new DataTemplate(typeof(DayAfterTomorrowViewCell));

            PurchaseOrderAssetsTemplate = new DataTemplate(() =>
            {
                return new PurchaseOrderAssetsViewCell(ref ParentBindingContext);

            });



        }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ParentBindingContext = container.BindingContext;
            return PurchaseOrderAssetsTemplate;
        }
    }
}
