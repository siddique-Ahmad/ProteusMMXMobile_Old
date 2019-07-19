
using ProteusMMX.Views.Asset.Templates.ViewCells;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProteusMMX.Views.Asset.Templates
{
    public class AssetListingTemplateSelector:DataTemplateSelector
    {
        public DataTemplate AssetTemplate { get; set; }



        public object ParentBindingContext;
        public AssetListingTemplateSelector()
        {
            //PastCurrentDayTemplate = new DataTemplate(typeof(PastViewCell));
            //TomorrowDayTemplate = new DataTemplate(typeof(TomorrowViewCell));
            //DayAfterTomorrowTemplate = new DataTemplate(typeof(DayAfterTomorrowViewCell));

            AssetTemplate = new DataTemplate(() =>
            {
                return new AssetsViewCell(ref ParentBindingContext);

            });



        }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ParentBindingContext = container.BindingContext;
            return AssetTemplate;
        }
    }
}
