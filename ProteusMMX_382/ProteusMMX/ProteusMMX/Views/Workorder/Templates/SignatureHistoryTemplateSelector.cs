using ProteusMMX.Views.Workorder.Templates.ViewCells;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProteusMMX.Views.Workorder.Templates
{
    public class SignatureHistoryTemplateSelector:DataTemplateSelector
    {
        public DataTemplate SignatureTemplate { get; set; }



        public object ParentBindingContext;
        public SignatureHistoryTemplateSelector()
        {
            //PastCurrentDayTemplate = new DataTemplate(typeof(PastViewCell));
            //TomorrowDayTemplate = new DataTemplate(typeof(TomorrowViewCell));
            //DayAfterTomorrowTemplate = new DataTemplate(typeof(DayAfterTomorrowViewCell));

            SignatureTemplate = new DataTemplate(() =>
            {
                return new SignatureHistoryViewCell(ref ParentBindingContext);

            });



        }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ParentBindingContext = container.BindingContext;
            return SignatureTemplate;
        }

    }
}
