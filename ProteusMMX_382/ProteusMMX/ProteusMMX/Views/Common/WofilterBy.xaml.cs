using Acr.UserDialogs;
using ProteusMMX.Helpers;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.Navigation;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.Workorder;
using ProteusMMX.Views.Workorder;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WofilterBy : PopupPage
    {

        public readonly INavigationService _navigationService;
        protected readonly IAuthenticationService _authenticationService;
        public readonly IWorkorderService _workorderService;
        WorkorderListingPageViewModel ViewModel;
        string ShiftSelectedText;
        string PrioritySelectedText;
        public WofilterBy(object navigationData)
        {
            InitializeComponent();
            var navigationParams = navigationData as TargetNavigationData;
            ViewModel = navigationParams.ViewModel;
            //_workorderService = navigationParams.WorkorderService;

            PreventiveMaintenenceTitle = WebControlTitle.GetTargetNameByTitleName("PreventiveMaintenance");
            DemandMaintenenceTitle = WebControlTitle.GetTargetNameByTitleName("DemandMaintenance");
            EmergencyMaintenanceTitle = WebControlTitle.GetTargetNameByTitleName("EmergencyMaintenance");
            TaskOnlyTitle = WebControlTitle.GetTargetNameByTitleName("Task") + "" + WebControlTitle.GetTargetNameByTitleName("Only");
            InspectionOnlyTitle = WebControlTitle.GetTargetNameByTitleName("Inspection") + "" + WebControlTitle.GetTargetNameByTitleName("Only");
            CompletionTitle = WebControlTitle.GetTargetNameByTitleName("CompletionDate");
            FailedInspectionTitle = WebControlTitle.GetTargetNameByTitleName("FailedInspection");

            lst.ItemsSource = new List<string>() { PreventiveMaintenenceTitle, DemandMaintenenceTitle, EmergencyMaintenanceTitle, TaskOnlyTitle, InspectionOnlyTitle , CompletionTitle , FailedInspectionTitle };
            string response = string.Empty;


            List<FilterOrder> list = new List<FilterOrder>();

            if (Application.Current.Properties.ContainsKey("WorkOFilterTypeKye"))
            {
                response = Application.Current.Properties["WorkOFilterTypeKye"].ToString();
                string SelectIcon = string.Empty;
                if (Device.RuntimePlatform == Device.UWP)
                {
                    SelectIcon = "Assets/check.png";

                }
                else
                {
                    SelectIcon = "check.png";
                }
                if (response== PreventiveMaintenenceTitle)
                {
                    list.Add(new FilterOrder { Filters = PreventiveMaintenenceTitle, Images = SelectIcon });
                    list.Add(new FilterOrder { Filters = DemandMaintenenceTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = EmergencyMaintenanceTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = TaskOnlyTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = InspectionOnlyTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = CompletionTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = FailedInspectionTitle, Images = "" });
                    lst.ItemsSource = list;
                }
                else if (response == DemandMaintenenceTitle)
                {
                    list.Add(new FilterOrder { Filters = PreventiveMaintenenceTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = DemandMaintenenceTitle, Images = SelectIcon });
                    list.Add(new FilterOrder { Filters = EmergencyMaintenanceTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = TaskOnlyTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = InspectionOnlyTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = CompletionTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = FailedInspectionTitle, Images = "" });
                    lst.ItemsSource = list;
                }
                else if (response == EmergencyMaintenanceTitle)
                {
                    list.Add(new FilterOrder { Filters = PreventiveMaintenenceTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = DemandMaintenenceTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = EmergencyMaintenanceTitle, Images = SelectIcon });
                    list.Add(new FilterOrder { Filters = TaskOnlyTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = InspectionOnlyTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = CompletionTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = FailedInspectionTitle, Images = "" });
                    lst.ItemsSource = list;
                }
                else if (response == TaskOnlyTitle)
                {
                    list.Add(new FilterOrder { Filters = PreventiveMaintenenceTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = DemandMaintenenceTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = EmergencyMaintenanceTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = TaskOnlyTitle, Images = SelectIcon });
                    list.Add(new FilterOrder { Filters = InspectionOnlyTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = CompletionTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = FailedInspectionTitle, Images = "" });
                    lst.ItemsSource = list;
                }
                else if (response == InspectionOnlyTitle)
                {
                    list.Add(new FilterOrder { Filters = PreventiveMaintenenceTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = DemandMaintenenceTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = EmergencyMaintenanceTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = TaskOnlyTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = InspectionOnlyTitle, Images = SelectIcon });
                    list.Add(new FilterOrder { Filters = CompletionTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = FailedInspectionTitle, Images = "" });
                    lst.ItemsSource = list;
                }
                else if (response == CompletionTitle)
                {
                    list.Add(new FilterOrder { Filters = PreventiveMaintenenceTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = DemandMaintenenceTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = EmergencyMaintenanceTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = TaskOnlyTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = InspectionOnlyTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = CompletionTitle, Images = SelectIcon });
                    list.Add(new FilterOrder { Filters = FailedInspectionTitle, Images = "" });
                    lst.ItemsSource = list;
                }
                else if (response == FailedInspectionTitle)
                {

                    list.Add(new FilterOrder { Filters = PreventiveMaintenenceTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = DemandMaintenenceTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = EmergencyMaintenanceTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = TaskOnlyTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = InspectionOnlyTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = CompletionTitle, Images = "" });
                    list.Add(new FilterOrder { Filters = FailedInspectionTitle, Images = SelectIcon });
                    lst.ItemsSource = list;
                }
                //lst.SelectedItem = response;
            }
            else
            {
                list.Add(new FilterOrder { Filters = PreventiveMaintenenceTitle, Images = "" });
                list.Add(new FilterOrder { Filters = DemandMaintenenceTitle, Images = "" });
                list.Add(new FilterOrder { Filters = EmergencyMaintenanceTitle, Images = "" });
                list.Add(new FilterOrder { Filters = TaskOnlyTitle, Images = "" });
                list.Add(new FilterOrder { Filters = InspectionOnlyTitle, Images = "" });
                list.Add(new FilterOrder { Filters = CompletionTitle, Images = "" });
                list.Add(new FilterOrder { Filters = FailedInspectionTitle, Images = "" });
                lst.ItemsSource = list;
            }

        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is IHandleViewAppearing viewAware)
            {
                await viewAware.OnViewAppearingAsync(this);
            }
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            if (BindingContext is IHandleViewDisappearing viewAware)
            {
                await viewAware.OnViewDisappearingAsync(this);
            }
        }


        #region ***** Titel Property ****


        string _preventiveMaintenenceTitle;
        public string PreventiveMaintenenceTitle
        {
            get
            {
                return _preventiveMaintenenceTitle;
            }

            set
            {
                if (value != _preventiveMaintenenceTitle)
                {
                    _preventiveMaintenenceTitle = value;
                    OnPropertyChanged("PreventiveMaintenenceTitle");
                }
            }
        }

        string _demandMaintenenceTitle;
        public string DemandMaintenenceTitle
        {
            get
            {
                return _demandMaintenenceTitle;
            }

            set
            {
                if (value != _demandMaintenenceTitle)
                {
                    _demandMaintenenceTitle = value;
                    OnPropertyChanged("DemandMaintenenceTitle");
                }
            }
        }

        string _inspectionOnlyTitle;
        public string InspectionOnlyTitle
        {
            get
            {
                return _inspectionOnlyTitle;
            }

            set
            {
                if (value != _inspectionOnlyTitle)
                {
                    _inspectionOnlyTitle = value;
                    OnPropertyChanged("InspectionOnlyTitle");
                }
            }
        }

        string _taskOnlyTitle;
        public string TaskOnlyTitle
        {
            get
            {
                return _taskOnlyTitle;
            }

            set
            {
                if (value != _taskOnlyTitle)
                {
                    _taskOnlyTitle = value;
                    OnPropertyChanged("TaskOnlyTitle");
                }
            }
        }

        string _completionTitle;
        public string CompletionTitle
        {
            get
            {
                return _completionTitle;
            }

            set
            {
                if (value != _completionTitle)
                {
                    _completionTitle = value;
                    OnPropertyChanged("CompletionTitle");
                }
            }
        }

        string _emergencyMaintenanceTitle;
        public string EmergencyMaintenanceTitle
        {
            get
            {
                return _emergencyMaintenanceTitle;
            }

            set
            {
                if (value != _emergencyMaintenanceTitle)
                {
                    _emergencyMaintenanceTitle = value;
                    OnPropertyChanged("EmergencyMaintenanceTitle");
                }
            }
        }

        string _FailedInspectionTitle;
        public string FailedInspectionTitle
        {
            get
            {
                return _FailedInspectionTitle;
            }

            set
            {
                if (value != _FailedInspectionTitle)
                {
                    _FailedInspectionTitle = value;
                    OnPropertyChanged("FailedInspectionTitle");
                }
            }
        }

        #endregion
       

        private async void lst_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var data = e.Item as FilterOrder;
            if (data != null && data.Filters != null)
            {                
                string item = data.Filters;
                Application.Current.Properties["WorkOFilterTypeKye"] = item;
                await PopupNavigation.PopAllAsync();
                await ViewModel.OnViewAppearingAsync(null);
            }
            else
            {
                await PopupNavigation.PopAllAsync();
            }
        }
    }

    public class FilterOrder
    {
        public string Filters { get; set; }
        public string Images { get; set; }
    }
}








