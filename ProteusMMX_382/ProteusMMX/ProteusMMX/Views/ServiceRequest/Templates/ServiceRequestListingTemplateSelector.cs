using ProteusMMX.Views.Service_Request.Templates.ViewCells;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProteusMMX.Views.ServiceRequest.Templates
{
    public class ServiceRequestListingTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ServiceRequestTemplate { get; set; }



        public object ParentBindingContext;
        public ServiceRequestListingTemplateSelector()
        {
            //PastCurrentDayTemplate = new DataTemplate(typeof(PastViewCell));
            //TomorrowDayTemplate = new DataTemplate(typeof(TomorrowViewCell));
            //DayAfterTomorrowTemplate = new DataTemplate(typeof(DayAfterTomorrowViewCell));

            ServiceRequestTemplate = new DataTemplate(() =>
            {
                return new ServiceRequestViewCell(ref ParentBindingContext);

            });



        }


        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            ParentBindingContext = container.BindingContext;
            return ServiceRequestTemplate;
        }
    }
}
