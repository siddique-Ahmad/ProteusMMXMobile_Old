using ProteusMMX.Helpers;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Workorder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Workorder.Templates.ViewCells
{
	 [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DayAfterTomorrowViewCell : ViewCell
	{
        Color rowcolor = Color.White;
        public DayAfterTomorrowViewCell ()
		{
			InitializeComponent ();
		}


        public DayAfterTomorrowViewCell(ref object ParentContext)
        {
            InitializeComponent();

            string WorkOrderTypeKeyValue = "";
            string WorkOrderStartedDateKeyValue = "";
            string WorkOrderCompletionDateKeyValue = "";
            string WorkOrderRequestedDateKeyValue = "";
            string DescriptionKeyValue = "";
            string PriorityKeyValue = "";

            // this.CloseWorkorderMenuItem.Text = WebControlTitle.GetTargetNameByTitleName("CloseWorkOrder");
            this.ParentContext = ParentContext;
            this.WorkorderNumberLabel.Text = WebControlTitle.GetTargetNameByTitleName("WorkorderNumber");

            this.RequiredDateLabel.Text = WebControlTitle.GetTargetNameByTitleName("RequiredDate");
            this.TargetNameLabel.Text = WebControlTitle.GetTargetNameByTitleName("Target") + " " + WebControlTitle.GetTargetNameByTitleName("Name");
            // this.WorkorderTypeLabel.Text = (ParentContext as WorkorderListingPageViewModel).WorkOrderTypeTitle;
            this.ActivationDateLabel.Text = WebControlTitle.GetTargetNameByTitleName("ActivationDate");

            if (Application.Current.Properties.ContainsKey("PriorityKey"))
            {
                PriorityKeyValue = Application.Current.Properties["PriorityKey"].ToString();
            }
            if (PriorityKeyValue == "E" || PriorityKeyValue == "V")
            {
               
                this.PriorityColon.IsVisible = true;
            }
            this.WorkorderTypeLabel.Text = WebControlTitle.GetTargetNameByTitleName("WorkOrderType");
            this.WorkorderTypeColon.IsVisible = true;
            //if (Application.Current.Properties.ContainsKey("WorkOrderTypeKey"))
            //{
            //    WorkOrderTypeKeyValue = Application.Current.Properties["WorkOrderTypeKey"].ToString();
            //}
            //if (WorkOrderTypeKeyValue == "E" || WorkOrderTypeKeyValue == "V")
            //{
              
            //}
            if (Application.Current.Properties.ContainsKey("WorkOrderStartedDateKey"))
            {
                WorkOrderStartedDateKeyValue = Application.Current.Properties["WorkOrderStartedDateKey"].ToString();
            }
            if (WorkOrderStartedDateKeyValue == "E" || WorkOrderStartedDateKeyValue == "V")
            {
                this.WorkStartedDateLabel.Text = WebControlTitle.GetTargetNameByTitleName("WorkStartedDate");
                this.WorkStartedDateVisible.IsVisible = true;
            }
            if (Application.Current.Properties.ContainsKey("WorkOrderCompletionDateKey"))
            {
                WorkOrderCompletionDateKeyValue = Application.Current.Properties["WorkOrderCompletionDateKey"].ToString();
            }
            if (WorkOrderCompletionDateKeyValue == "E" || WorkOrderCompletionDateKeyValue == "V")
            {
                this.WorkCompletionDateLabel.Text = WebControlTitle.GetTargetNameByTitleName("CompletionDate");
                this.WorkCompletionDateVisible.IsVisible = true;
            }
            if (Application.Current.Properties.ContainsKey("WorkOrderRequestedDateKey"))
            {
                WorkOrderRequestedDateKeyValue = Application.Current.Properties["WorkOrderRequestedDateKey"].ToString();
            }
            if (AppSettings.User.blackhawkLicValidator.ServiceRequestIsEnabled.Equals(true))
            {
                if (WorkOrderRequestedDateKeyValue == "E" || WorkOrderRequestedDateKeyValue == "V")
                {
                    this.WorkRequestedDateLabel.Text = WebControlTitle.GetTargetNameByTitleName("RequestedDate");
                    this.WorkRequestedDateVisible.IsVisible = true;
                }
            }
            if (Application.Current.Properties.ContainsKey("DescriptionKey"))
            {
                DescriptionKeyValue = Application.Current.Properties["DescriptionKey"].ToString();
            }
            if (DescriptionKeyValue == "E" || DescriptionKeyValue == "V")
            {
                this.DescriptionLabel.Text = WebControlTitle.GetTargetNameByTitleName("Description");
                this.DescriptionVisible.IsVisible = true;
            }
        }

        public async void CloseWorkorder(object sender, EventArgs e)
        {
            try
            {
                var menuItem = sender as MenuItem;
                menuItem.IsDestructive = false;
                await (ParentContext as WorkorderListingPageViewModel).CloseWorkorder(menuItem.CommandParameter as workOrders);
            }
            catch (Exception ex)
            {


            }
        }
        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            try
            {
                string CloseWorkOrderKeyValue = "";

                if (Application.Current.Properties.ContainsKey("CloseWorkorderRightsKey"))
                {
                    CloseWorkOrderKeyValue = Application.Current.Properties["CloseWorkorderRightsKey"].ToString();
                }

                ViewCell theViewCell = ((ViewCell)sender);

                var item = theViewCell.BindingContext as workOrders;
                theViewCell.ContextActions.Clear();
                if (item != null)
                {

                    MenuItem mn = new MenuItem();


                    if (CloseWorkOrderKeyValue == "E")
                    {
                        mn.Clicked += CloseWorkorder;
                        mn.Text = WebControlTitle.GetTargetNameByTitleName("CloseWorkOrder");
                        mn.CommandParameter = item;
                        theViewCell.ContextActions.Add(mn);
                    }
                    else if (CloseWorkOrderKeyValue == "V")
                    {
                        //  mn.Clicked += CloseWorkorder;
                        mn.Text = WebControlTitle.GetTargetNameByTitleName("CloseWorkOrder");
                        mn.CommandParameter = item;
                        theViewCell.ContextActions.Add(mn);
                    }
                    else
                    {

                    }

                }
            }
            catch (Exception ex)
            { }
        }


        public static readonly BindableProperty ParentContextProperty =
              BindableProperty.Create("ParentContext", typeof(object), typeof(DayAfterTomorrowViewCell), null, propertyChanged: OnParentContextPropertyChanged);

        public object ParentContext
        {
            get { return GetValue(ParentContextProperty); }
            set { SetValue(ParentContextProperty, value); }
        }

        private static void OnParentContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                (bindable as DayAfterTomorrowViewCell).ParentContext = newValue;
            }
        }
        private void Cell_OnAppearing(object sender, EventArgs e)
        {
            // var viewCell = (ViewCell)sender;


            if (Device.RuntimePlatform == Device.UWP)
            {

                var viewCell = (ViewCell)sender;
                if (viewCell.View != null && viewCell.View.BackgroundColor == default(Color))
                {
                    int rowindex = Convert.ToInt32(Application.Current.Properties["gridrowindex"]);
                    if (rowindex % 2 == 0)
                    {
                        // rowcolor = Color.White;
                        viewCell.View.BackgroundColor = Color.White;


                    }
                    else
                    {

                        viewCell.View.BackgroundColor = Color.FromHex("#D3D3D3");
                    }
                    rowindex = rowindex + 1;
                    Application.Current.Properties["gridrowindex"] = rowindex;
                }
            }
           
           

        }
    }
}