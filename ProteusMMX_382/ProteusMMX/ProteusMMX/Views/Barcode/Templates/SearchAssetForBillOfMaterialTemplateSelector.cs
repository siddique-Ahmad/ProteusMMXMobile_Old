using ProteusMMX.Views.Barcode.Templates.ViewCells;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProteusMMX.Views.Barcode.Templates
{
    public class SearchAssetForBillOfMaterialTemplateSelector:DataTemplateSelector
    {

        public DataTemplate BOMTemplate { get; set; }



        public object ParentBindingContext;
        public SearchAssetForBillOfMaterialTemplateSelector()
        {
            //PastCurrentDayTemplate = new DataTemplate(typeof(PastViewCell));
            //TomorrowDayTemplate = new DataTemplate(typeof(TomorrowViewCell));
            //DayAfterTomorrowTemplate = new DataTemplate(typeof(DayAfterTomorrowViewCell));

            BOMTemplate = new DataTemplate(() =>
            {
                return new BillOfMaterialViewCell(ref ParentBindingContext);

            });



        }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ParentBindingContext = container.BindingContext;
            return BOMTemplate;
        }
    }
}
