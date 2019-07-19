using ProteusMMX.Helpers;
using ProteusMMX.Model.ServiceRequestModel;
using ProteusMMX.ViewModel.ServiceRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Service_Request.Templates.ViewCells
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServiceRequestViewCell : ViewCell
    {
        public ServiceRequestViewCell(ref object ParentContext)
        {
            InitializeComponent();
            string PriorityTabKeyvalue = "";
            this.ParentContext = ParentContext;
            this.RequestNumber.Text = WebControlTitle.GetTargetNameByTitleName("RequestNumber");
            this.Description.Text = WebControlTitle.GetTargetNameByTitleName("Description");
            if (Application.Current.Properties.ContainsKey("PriorityTabKey"))
            {
                PriorityTabKeyvalue = Application.Current.Properties["PriorityTabKey"].ToString();
            }
            if (PriorityTabKeyvalue == "E" || PriorityTabKeyvalue == "V")
            {
                //this.PriorityName.Text = (ParentContext as ServiceRequestListingPageViewModel).Priority;
                this.PriorityName.Text = WebControlTitle.GetTargetNameByTitleName("Priority");
                this.PriorityColon.IsVisible = true;
            }
        }
        public static readonly BindableProperty ParentContextProperty =
     BindableProperty.Create("ParentContext", typeof(object), typeof(ServiceRequestViewCell), null, propertyChanged: OnParentContextPropertyChanged);

        public object ParentContext
        {
            get { return GetValue(ParentContextProperty); }
            set { SetValue(ParentContextProperty, value); }
        }
        public async void ServiceRequestMenuItems(object sender, EventArgs e)
        {
            try
            {
                var menuItem = sender as MenuItem;
                if (menuItem.Text == WebControlTitle.GetTargetNameByTitleName("Accept"))
                {
                    await (ParentContext as ServiceRequestListingPageViewModel).AcceptServiceRequest(menuItem.CommandParameter as ServiceRequests);
                }
                else
                {
                    await (ParentContext as ServiceRequestListingPageViewModel).DeclineServiceRequest(menuItem.CommandParameter as ServiceRequests);
                }

            }
            catch (Exception ex)
            {


            }
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            try
            {
                string acceptkeyvalue = "";
                string declinekeyvalue = "";
                if (Application.Current.Properties.ContainsKey("AcceptKey"))
                {
                    acceptkeyvalue = Application.Current.Properties["AcceptKey"].ToString();
                }

                if (Application.Current.Properties.ContainsKey("DEclineKey"))
                {
                    declinekeyvalue = Application.Current.Properties["DEclineKey"].ToString();
                }

                ViewCell theViewCell = ((ViewCell)sender);

                var item = theViewCell.BindingContext as ServiceRequests;
                theViewCell.ContextActions.Clear();
                if (item != null)
                {

                    MenuItem mn = new MenuItem();
                    MenuItem mp = new MenuItem();

                    if (acceptkeyvalue == "E")
                    {
                        mn.Clicked += ServiceRequestMenuItems;//for accept
                        mn.Text = WebControlTitle.GetTargetNameByTitleName("Accept");
                        mn.CommandParameter = item;
                        theViewCell.ContextActions.Add(mn);
                    }
                    else if (acceptkeyvalue == "V")
                    {
                        //  mn.Clicked += ServiceRequestMenuItems;//for accept
                        mn.Text = WebControlTitle.GetTargetNameByTitleName("Accept");
                        mn.CommandParameter = item;
                        theViewCell.ContextActions.Add(mn);
                    }
                    else
                    {

                    }

                    if (declinekeyvalue == "E")
                    {
                        mp.Clicked += ServiceRequestMenuItems;//for accept
                        mp.Text = WebControlTitle.GetTargetNameByTitleName("Decline");
                        mp.CommandParameter = item;
                        theViewCell.ContextActions.Add(mp);
                    }
                    else if (declinekeyvalue == "V")
                    {
                        //  mp.Clicked += ServiceRequestMenuItems;//for accept
                        mp.Text = WebControlTitle.GetTargetNameByTitleName("Decline");
                        mp.CommandParameter = item;
                        theViewCell.ContextActions.Add(mp);
                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            { }
        }


        private static void OnParentContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                (bindable as ServiceRequestViewCell).ParentContext = newValue;
            }
        }
    }
}