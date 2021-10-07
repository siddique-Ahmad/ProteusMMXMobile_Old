using ProteusMMX.Helpers;
using ProteusMMX.Model.CommonModels;
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
    public partial class PastViewCell : ViewCell
    {
        List<FormControl> _workorderControlsNew = new List<FormControl>();
        public List<FormControl> WorkorderControlsNew
        {
            get
            {
                return _workorderControlsNew;
            }

            set
            {
                _workorderControlsNew = value;
            }
        }

        Color rowcolor = Color.White;
        public PastViewCell()
        {
            
            InitializeComponent();

        }

        bool isExpanded = false;
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (isExpanded)
            {
                await ShowMore.FadeTo(0);
                ShowMore.IsVisible = !isExpanded;
                this.Tapped.Text = "Show More";

            }
            else
            {
                ShowMore.IsVisible = !isExpanded;
                await ShowMore.FadeTo(1);
                this.Tapped.Text = "See less";
            }

            isExpanded = !isExpanded;

        }
        public PastViewCell(ref object ParentContext)
        {
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("WorkorderDetailsControls"))
            {
                SubModule WorkorderDetails = Application.Current.Properties["WorkorderDetailsControls"] as SubModule;
                WorkorderControlsNew = WorkorderDetails.listControls;
            }
            string WorkOrderTypeKeyValue = "";
            string WorkOrderStartedDateKeyValue = "";
            string WorkOrderCompletionDateKeyValue = "";
            string WorkOrderRequestedDateKeyValue = "";
            string DescriptionKeyValue = "";
            string PriorityKeyValue = "";


            this.ParentContext = ParentContext;

            /// For workordernumber////
            var workorderNumber = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "WorkOrderNumber");
            //if (workorderNumber != null)
            //{
            //    this.WorkorderNumberLabel.Text = workorderNumber.TargetName;

            //}
            //else
            //{
            //    this.WorkorderNumberLabel.Text = WebControlTitle.GetTargetNameByTitleName("WorkorderNumber");
            //}

            /// For Requireddate////
            var requiredDate = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "RequiredDate");
            if (requiredDate != null)
            {
                this.RequiredDateLabel.Text = requiredDate.TargetName;

            }
            else
            {
                this.RequiredDateLabel.Text = WebControlTitle.GetTargetNameByTitleName("RequiredDate");
            }

            /// For TargetName///
            var target = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "AssetID");
            if (target != null)
            {
                this.TargetNameLabel.Text = target.TargetName;

            }
            else
            {
                this.TargetNameLabel.Text = WebControlTitle.GetTargetNameByTitleName("Target");
            }




            ///For ActivationDate///
            var activationDate = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "ActivationDate");
            if (activationDate != null)
            {
                this.ActivationDateLabel.Text = activationDate.TargetName;

            }
            else
            {
                this.ActivationDateLabel.Text = WebControlTitle.GetTargetNameByTitleName("ActivationDate");
            }


            if (Application.Current.Properties.ContainsKey("PriorityKey"))
            {
                PriorityKeyValue = Application.Current.Properties["PriorityKey"].ToString();
            }
            if (PriorityKeyValue == "E" || PriorityKeyValue == "V")
            {

                this.PriorityColon.IsVisible = true;
            }

            ////For Workordertype////
            var workOrderType = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "WorkTypeID");
            if (workOrderType != null)
            {
                this.WorkorderTypeLabel.Text = workOrderType.TargetName;

            }
            else
            {
                this.WorkorderTypeLabel.Text = WebControlTitle.GetTargetNameByTitleName("WorkOrderType");
            }

            this.WorkorderTypeColon.IsVisible = true;


            ////For Workorderstartdate/////
            if (Application.Current.Properties.ContainsKey("WorkOrderStartedDateKey"))
            {
                WorkOrderStartedDateKeyValue = Application.Current.Properties["WorkOrderStartedDateKey"].ToString();
            }
            if (WorkOrderStartedDateKeyValue == "E" || WorkOrderStartedDateKeyValue == "V")
            {
                var workStartedDate = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "WorkStartedDate");
                if (workStartedDate != null)
                {
                    this.WorkStartedDateLabel.Text = workStartedDate.TargetName;

                }
                else
                {
                    this.WorkStartedDateLabel.Text = WebControlTitle.GetTargetNameByTitleName("WorkStartedDate");
                }


                this.WorkStartedDateVisible.IsVisible = true;
            }


            ////For WorkorderCompletiondate//////
            if (Application.Current.Properties.ContainsKey("WorkOrderCompletionDateKey"))
            {
                WorkOrderCompletionDateKeyValue = Application.Current.Properties["WorkOrderCompletionDateKey"].ToString();
            }
            if (WorkOrderCompletionDateKeyValue == "E" || WorkOrderCompletionDateKeyValue == "V")
            {
                var completionDate = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "CompletionDate");
                if (completionDate != null)
                {
                    this.WorkCompletionDateLabel.Text = completionDate.TargetName;

                }
                else
                {
                    this.WorkCompletionDateLabel.Text = WebControlTitle.GetTargetNameByTitleName("CompletionDate");
                }


                this.WorkCompletionDateVisible.IsVisible = true;
            }


            /// For WorkorderRequestedDate//////////
            if (Application.Current.Properties.ContainsKey("WorkOrderRequestedDateKey"))
            {
                WorkOrderRequestedDateKeyValue = Application.Current.Properties["WorkOrderRequestedDateKey"].ToString();
            }
            if (AppSettings.User.blackhawkLicValidator.ServiceRequestIsEnabled.Equals(true))
            {
                if (WorkOrderRequestedDateKeyValue == "E" || WorkOrderRequestedDateKeyValue == "V")
                {
                    var requestedate = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "RequestedDate");
                    if (requestedate != null)
                    {
                        this.WorkRequestedDateLabel.Text = requestedate.TargetName;

                    }
                    else
                    {
                        this.WorkRequestedDateLabel.Text = WebControlTitle.GetTargetNameByTitleName("RequestedDate");
                    }

                    this.WorkRequestedDateVisible.IsVisible = true;
                }
            }

            /// For Description//////////
            if (Application.Current.Properties.ContainsKey("DescriptionKey"))
            {
                DescriptionKeyValue = Application.Current.Properties["DescriptionKey"].ToString();
            }
            if (DescriptionKeyValue == "E" || DescriptionKeyValue == "V")
            {

                var description = WorkorderControlsNew.FirstOrDefault(x => x.ControlName == "Description");
                if (description != null)
                {
                    this.DescriptionLabel.Text = description.TargetName;

                }
                else
                {
                    this.DescriptionLabel.Text = WebControlTitle.GetTargetNameByTitleName("Description");
                }

                this.DescriptionVisible.IsVisible = true;
            }
        }

        public static readonly BindableProperty ParentContextProperty =
            BindableProperty.Create("ParentContext", typeof(object), typeof(PastViewCell), null, propertyChanged: OnParentContextPropertyChanged);

        public object ParentContext
        {
            get { return GetValue(ParentContextProperty); }
            set { SetValue(ParentContextProperty, value); }
        }

        public async void CloseWorkorder(object sender, EventArgs e)
        {
            try
            {
                var menuItem = sender as MenuItem;
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

        private static void OnParentContextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != oldValue && newValue != null)
            {
                (bindable as PastViewCell).ParentContext = newValue;
            }
        }


        private void Cell_OnAppearing(object sender, EventArgs e)
        {
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


        
