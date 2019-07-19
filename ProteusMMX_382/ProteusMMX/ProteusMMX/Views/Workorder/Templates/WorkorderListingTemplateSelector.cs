using ProteusMMX.Helpers.DateTime;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Views.Workorder.Templates.ViewCells;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProteusMMX.Views.Workorder.Templates
{
    public class WorkorderListingTemplateSelector : DataTemplateSelector
    {
        public DataTemplate PastCurrentDayTemplate { get; set; }
        public DataTemplate TomorrowDayTemplate { get; set; }
        public DataTemplate DayAfterTomorrowTemplate { get; set; }

      


        public object ParentBindingContext;
        public WorkorderListingTemplateSelector()
        {
            //PastCurrentDayTemplate = new DataTemplate(typeof(PastViewCell));
            //TomorrowDayTemplate = new DataTemplate(typeof(TomorrowViewCell));
            //DayAfterTomorrowTemplate = new DataTemplate(typeof(DayAfterTomorrowViewCell));

            PastCurrentDayTemplate = new DataTemplate(() =>
            {
                return new PastViewCell(ref ParentBindingContext);

            });

            DayAfterTomorrowTemplate = new DataTemplate(() =>
            {
                return new DayAfterTomorrowViewCell(ref ParentBindingContext);

            });

            TomorrowDayTemplate = new DataTemplate(() =>
            {
                return new TomorrowViewCell(ref ParentBindingContext);

            });


        }

         
        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {

            //DateTime date = DateTime.Now; //((ProteusMMX.Views.WorkOrderPage.Workorders.Work)item).Workorder.RequiredDate ?? DateTime.Now;

            //DateTime date = DateTime.Now; // (workOrders)item).Workorder.RequiredDate ?? DateTime.Now;

            //DateTime date = workorder.RequestedDate ?? DateTime.Now;
            try
            {
                ParentBindingContext = container.BindingContext;
                //return ((List<string>)((ListView)container).ItemsSource).IndexOf(item as string) % 2 == 0 ? EvenTemplate : UnevenTemplate;
                var workorder = item as workOrders;
                DateTime date = DateTimeConverter.ConvertDateTimeToDifferentTimeZone((workorder.RequiredDate ?? DateTime.Now).ToUniversalTime(), AppSettings.User.ServerIANATimeZone); //ServerTimeZone);

                if (date.Date < DateTime.Now.Date || date == DateTime.Now.Date)
                {
                    //PastCurrentDayTemplate = new DataTemplate(() =>
                    //{
                    //    return new PastViewCell(container.BindingContext);

                    //});
                    return PastCurrentDayTemplate;

                }

                if (date.Date > DateTime.Now.Date.AddDays(1))
                {
                    //DayAfterTomorrowTemplate = new DataTemplate(() =>
                    //{
                    //    return new DayAfterTomorrowViewCell(container.BindingContext);

                    //});
                    return DayAfterTomorrowTemplate;
                }

                if (date.Date > DateTime.Now.Date)
                {
                    //TomorrowDayTemplate = new DataTemplate(() =>
                    //{
                    //    return new TomorrowViewCell(container.BindingContext);

                    //});
                    return TomorrowDayTemplate;
                }


                //PastCurrentDayTemplate = new DataTemplate(() =>
                //{
                //    return new PastViewCell(container.BindingContext);

                //});
              
            }
            catch (Exception ex)
            {

                
            }


            return PastCurrentDayTemplate;


        }

     
    }
}
