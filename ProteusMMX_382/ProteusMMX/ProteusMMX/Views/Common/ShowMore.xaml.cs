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
    public partial class ShowMore : PopupPage
    {

        public readonly INavigationService _navigationService;
        protected readonly IAuthenticationService _authenticationService;
        public readonly IWorkorderService _workorderService;
        WorkorderListingPageViewModel ViewModel;
        string ShiftSelectedText;
        string PrioritySelectedText;
        public ShowMore(object navigationData)
        {
            InitializeComponent();

            if (navigationData != null)
            {

                var navigationParams = navigationData as TargetNavigationData;
                ViewModel = navigationParams.ViewModel;
                _workorderService = navigationParams.WorkorderService;

            }
           
            LocationPicker.Items.Insert(0,SelectTitle);
            ShiftPicker.Items.Insert(0,SelectTitle);
            PriorityPicker.Items.Insert(0,SelectTitle);
            this.OKButton.Text = WebControlTitle.GetTargetNameByTitleName("OK");
            this.CancelButton.Text = WebControlTitle.GetTargetNameByTitleName("Cancel");
            this.SortByLocationLableTitle.Text = WebControlTitle.GetTargetNameByTitleName("Sortby") + " " + WebControlTitle.GetTargetNameByTitleName("Location").ToString();
            this.SortByShiftLabelTitle.Text = WebControlTitle.GetTargetNameByTitleName("Sortby") + " " + WebControlTitle.GetTargetNameByTitleName("Shift");
            this.SortByDueDateTitle.Text = WebControlTitle.GetTargetNameByTitleName("Sortby") + " " + WebControlTitle.GetTargetNameByTitleName("Due") + " " + WebControlTitle.GetTargetNameByTitleName("Date");
            this.SortByPriorityLabelTitle.Text = WebControlTitle.GetTargetNameByTitleName("Sortby") + " " + WebControlTitle.GetTargetNameByTitleName("Priority");
            this.OnViewAppearingAsync(null);
           


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


        string _sortByLocationLabelTitle;
        public string SortByLocationLabelTitle
        {
            get
            {
                return _sortByLocationLabelTitle;
            }

            set
            {
                if (value != _sortByLocationLabelTitle)
                {
                    _sortByLocationLabelTitle = value;
                    OnPropertyChanged("SortByLocationLabelTitle");
                }
            }
        }

        #region Commands

        // public ICommand ShowDateCommand => new AsyncCommand(ShowdatePicker);
        // public ICommand DateCommand => new AsyncCommand(Cleardate);


        #endregion



        //string _sortByShiftLabelTitle;
        //public string SortByShiftLabelTitle
        //{
        //    get
        //    {
        //        return _sortByShiftLabelTitle;
        //    }

        //    set
        //    {
        //        if (value != _sortByShiftLabelTitle)
        //        {
        //            _sortByShiftLabelTitle = value;
        //            OnPropertyChanged("SortByShiftLabelTitle");
        //        }
        //    }
        //}

        string _sortByDuedateTitle;
        public string SortByDuedateTitle
        {
            get
            {
                return _sortByDuedateTitle;
            }

            set
            {
                if (value != _sortByDuedateTitle)
                {
                    _sortByDuedateTitle = value;
                    OnPropertyChanged("SortByDuedateTitle");
                }
            }
        }


        string _workorderTypeFilterText;
        public string WorkorderTypeFilterText
        {
            get
            {
                return _workorderTypeFilterText;
            }

            set
            {
                _workorderTypeFilterText = value;
                OnPropertyChanged(nameof(WorkorderTypeFilterText));
            }
        }

        string _locationNameFilterText;
        public string LocationNameFilterText
        {
            get
            {
                return _locationNameFilterText;
            }

            set
            {
                _locationNameFilterText = value;
                OnPropertyChanged(nameof(LocationNameFilterText));
            }
        }
        string _shiftNameFilterText;
        public string ShiftNameFilterText
        {
            get
            {
                return _shiftNameFilterText;
            }

            set
            {
                _shiftNameFilterText = value;
                OnPropertyChanged(nameof(ShiftNameFilterText));
            }
        }

        string _priorityNameFilterText;
        public string PriorityNameFilterText
        {
            get
            {
                return _priorityNameFilterText;
            }

            set
            {
                _priorityNameFilterText = value;
                OnPropertyChanged(nameof(PriorityNameFilterText));
            }
        }

        string _sortByDueDate;
        public string SortByDueDate
        {
            get
            {
                return _sortByDueDate;
            }

            set
            {
                _sortByDueDate = value;
                OnPropertyChanged(nameof(SortByDueDate));
            }
        }

        string _userID = AppSettings.User.UserID.ToString();
        public string UserID
        {
            get
            {
                return _userID;
            }

            set
            {
                if (value != _userID)
                {
                    _userID = value;
                    OnPropertyChanged("UserID");
                }
            }
        }

        int _rowCount = 10;
        public int RowCount
        {
            get
            {
                return _rowCount;
            }

            set
            {
                if (value != _rowCount)
                {
                    _rowCount = value;
                    OnPropertyChanged(nameof(RowCount));
                }
            }
        }



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

        async Task GetWorkordersRequiredDate()
        {
            try
            {
                OperationInProgress = true;
                var workordersResponse = await _workorderService.GetWorkorders(UserID, "0", "0", "null", "null", "null", "null", "null", "null", "null");
                if (workordersResponse != null && workordersResponse.workOrderWrapper != null
                    && workordersResponse.workOrderWrapper.workOrders != null && workordersResponse.workOrderWrapper.workOrders.Count > 0)
                {
                    var workorders = workordersResponse.workOrderWrapper.workOrders.Where(x => Convert.ToDateTime(x.RequiredDate).Date.ToString("dd MMM yyyy") == SortByDueDate);
                    await RemoveAllWorkorderFromCollection();
                    await AddWorkordersInWorkorderCollection(workorders.ToList());
                    TotalRecordCount = workorders.Count();
                    IsGetWorkorderCallFromRequiredDate = true;
                }



            }
            catch (Exception ex)
            {

                OperationInProgress = false;
            }

            finally
            {
                OperationInProgress = false;
            }
        }

        bool _isGetWorkorderCallFromRequiredDate;
        public bool IsGetWorkorderCallFromRequiredDate
        {
            get
            {
                return _isGetWorkorderCallFromRequiredDate;
            }

            set
            {
                if (value != _isGetWorkorderCallFromRequiredDate)
                {
                    _isGetWorkorderCallFromRequiredDate = value;
                    OnPropertyChanged("IsGetWorkorderCallFromRequiredDate");

                }
            }
        }

        public bool _operationInProgress;
        public bool OperationInProgress
        {
            get
            {
                return _operationInProgress;
            }

            set
            {

                if (value != _operationInProgress)
                {
                    _operationInProgress = value;
                    OnPropertyChanged("OperationInProgress");

                }
            }
        }






        int _selectedIndexPicker = -1;
        public int SelectedIndexPicker
        {
            get
            {
                return _selectedIndexPicker;
            }

            set
            {
                _selectedIndexPicker = value;
                OnPropertyChanged("SelectedIndexPicker");
                RefillWororderFromPicker();
            }
        }





        string _selectTitle = "";
        public string SelectTitle
        {
            get
            {
                return _selectTitle;
            }

            set
            {
                if (value != _selectTitle)
                {
                    _selectTitle = value;
                    OnPropertyChanged("SelectTitle");
                }
            }
        }


        ObservableCollection<string> _pickerTitles;

        public ObservableCollection<string> PickerTitles
        {
            get
            {
                return _pickerTitles;
            }

            set
            {
                if (value != _pickerTitles)
                {
                    _pickerTitles = value;
                    OnPropertyChanged("PickerTitles");

                }
            }

        }

        Dictionary<int?, string> sortByLocationpickerTitlesitems = new Dictionary<int?, string>();

        ObservableCollection<string> _sortByLocationpickerTitles = new ObservableCollection<string>();

        // Picker LocationPickerITEM = new Picker();


        //public ObservableCollection<string> SortByLocationpickerTitles
        //{
        //    get
        //    {
        //        return _sortByLocationpickerTitles;
        //    }

        //    set
        //    {
        //        if (value != _sortByLocationpickerTitles)
        //        {
        //            _sortByLocationpickerTitles = value;
        //            OnPropertyChanged("SortByLocationpickerTitles");

        //        }
        //    }

        //}

        Dictionary<int?, string> sortByShiftpickerTitlesitems = new Dictionary<int?, string>();

        ObservableCollection<string> _sortByShiftpickerTitles = new ObservableCollection<string>();

        public ObservableCollection<string> SortByShiftpickerTitles
        {
            get
            {
                return _sortByShiftpickerTitles;
            }

            set
            {
                if (value != _sortByShiftpickerTitles)
                {
                    _sortByShiftpickerTitles = value;
                    OnPropertyChanged("SortByShiftpickerTitles");

                }
            }

        }


        Dictionary<int?, string> sortByPrioritypickerTitlesitems = new Dictionary<int?, string>();

        ObservableCollection<string> _sortByPriorityPickerTitles = new ObservableCollection<string>();

        public ObservableCollection<string> SortByPriorityPickerTitles
        {
            get
            {
                return _sortByPriorityPickerTitles;
            }

            set
            {
                if (value != _sortByPriorityPickerTitles)
                {
                    _sortByPriorityPickerTitles = value;
                    OnPropertyChanged("SortByPriorityPickerTitles");

                }
            }

        }

        private async Task RefillLocationFromPicker()
        {
            //if (LocationSelectedIndexPicker == -1)
            //{
            //    return;
            //}
            //var LocationSelectedPickerText = SortByLocationpickerTitles[LocationSelectedIndexPicker];

            //if (LocationSelectedPickerText == SelectTitle)
            //{
            //    LocationNameFilterText = null;
            //    await RefillWorkorderCollection();
            //}
            //else
            //{
            //    LocationNameFilterText = LocationSelectedPickerText;
            //    await RefillWorkorderCollection();

            //}

        }

        private async Task RefillWorkorderCollection()
        {

            PageNumber = 1;
            await RemoveAllWorkorderFromCollection();
            //await GetWorkorders();


        }

        private async Task RemoveAllWorkorderFromCollection()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                _workordersCollection.Clear();
                OnPropertyChanged(nameof(WorkordersCollection));
            });



        }

        ObservableCollection<workOrders> _workordersCollection = new ObservableCollection<workOrders>();

        public ObservableCollection<workOrders> WorkordersCollection
        {
            get
            {
                return _workordersCollection;
            }

        }

        int _pageNumber = 1;
        public int PageNumber
        {
            get
            {
                return _pageNumber;
            }

            set
            {
                if (value != _pageNumber)
                {
                    _pageNumber = value;
                    OnPropertyChanged(nameof(PageNumber));
                }
            }
        }

        private async Task RefillShiftFromPicker()
        {
            //if (ShiftSelectedIndexPicker == -1)
            //{
            //    return;
            //}
            //var ShiftSelectedPickerText = SortByShiftpickerTitles[ShiftSelectedIndexPicker];

            //if (ShiftSelectedPickerText == SelectTitle)
            //{
            //    ShiftNameFilterText = null;
            //    await RefillWorkorderCollection();
            //}

            //else
            //{
            //    var filtered = from kvp in sortByShiftpickerTitlesitems
            //                   where kvp.Value.ToLower() == ShiftSelectedPickerText.ToLower()
            //                   select kvp.Key;
            //    int? firstItem = filtered.ElementAt(0);

            //    ShiftNameFilterText = firstItem.ToString();
            //    await RefillWorkorderCollection();
            //}

        }


       

        private async Task RefillDueDateFromPicker()
        {

            if (!string.IsNullOrWhiteSpace(SortByDateText.Text))
            {
                SortByDueDate = Convert.ToDateTime(SortByDateText).Date.ToString("dd MMM yyyy");

            }
            else
            {
                SortByDueDate = "null";
            }
            await RefillWorkorderCollection();
            // if (SortByDateText == null)
            // {
            //     IsGetWorkorderCallFromRequiredDate = false;
            //     await RefillWorkorderCollection();
            // }
            // else
            // {

            //     GetWorkordersRequiredDate();
            //}



        }

        private async Task RefillPriorityFromPicker()
        {


        }

        public async Task Cleardate()
        {

            SortByDateText.Text = null;
            // IsGetWorkorderCallFromRequiredDate = false;

        }
        public async Task ShowdatePicker()
        {

            UserDialogs.Instance.DatePrompt(new DatePromptConfig { OnAction = (result) => SetCompletionDateResult(result), IsCancellable = true });
        }


        public async Task OnViewAppearingAsync(VisualElement view)
        {
            //IsGetWorkorderCallFromRequiredDate = false;

            if (string.IsNullOrWhiteSpace(this.SearchText))
            {
                await RefillWorkorderCollection();

            }
            ////1st Method////////////////////
            var workordersResponse = await _workorderService.GetWorkorderDDRecord(UserID);



            if (workordersResponse != null && workordersResponse.Location != null && workordersResponse.Location.Count > 0)
            {


                foreach (var item in workordersResponse.Location)
                {
                    if (LocationPicker.SelectedIndex != -1 || ShiftPicker.SelectedIndex != -1 || PriorityPicker.SelectedIndex != -1)
                    {

                    }
                    else
                    {

                        if (!String.IsNullOrWhiteSpace(item.LocationName))
                        {

                            LocationPicker.Items.Add(item.LocationName);
                        }




                    }

                }


            }

            if (workordersResponse != null && workordersResponse.Shifts != null && workordersResponse.Shifts.Count > 0)
            {

                foreach (var item in workordersResponse.Shifts)
                {
                    if (LocationPicker.SelectedIndex != -1 || ShiftPicker.SelectedIndex != -1 || PriorityPicker.SelectedIndex != -1)
                    {

                    }
                    else
                    {

                        if (!String.IsNullOrWhiteSpace(item.ShiftName))
                        {
                            if (!sortByShiftpickerTitlesitems.ContainsKey(item.ShiftID))
                            {
                                sortByShiftpickerTitlesitems.Add(item.ShiftID, item.ShiftName);
                            }
                            ShiftPicker.Items.Add(item.ShiftName);


                        }

                    }
                }

            }

            if (workordersResponse != null && workordersResponse.Priority != null && workordersResponse.Priority.Count > 0)
            {

                foreach (var item in workordersResponse.Priority)
                {


                    if (LocationPicker.SelectedIndex != -1 || ShiftPicker.SelectedIndex != -1 || PriorityPicker.SelectedIndex != -1)
                    {

                    }
                    else
                    {

                        if (!String.IsNullOrWhiteSpace(item.PriorityName))
                        {
                            if (!sortByPrioritypickerTitlesitems.ContainsKey(item.PriorityID))
                            {
                                sortByPrioritypickerTitlesitems.Add(item.PriorityID, item.PriorityName);
                            }
                            PriorityPicker.Items.Add(item.PriorityName);
                            //PriorityPicker.ItemsSource.Add(item.PriorityName);


                        }

                    }

                }


            }

            if (Application.Current.Properties.ContainsKey("LocationFilterkey"))
            {
                var Locationfilter = Application.Current.Properties["LocationFilterkey"];
                if (Locationfilter != null)
                {
                    LocationPicker.SelectedItem = Locationfilter;

                }
            }
            if (Application.Current.Properties.ContainsKey("ShiftFilterkeyText"))
            {
                var Shiftfilter = Application.Current.Properties["ShiftFilterkeyText"];
                if (Shiftfilter != null)
                {
                    ShiftPicker.SelectedItem = Shiftfilter;

                }
            }
            if (Application.Current.Properties.ContainsKey("PriorityFilterkeyText"))
            {
                var Priorityfilter = Application.Current.Properties["PriorityFilterkeyText"];
                if (Priorityfilter != null)
                {
                   
                    PriorityPicker.SelectedItem = Priorityfilter;

                }
            }
            if (Application.Current.Properties.ContainsKey("DateFilterkey"))
            {
                var datefilter = Application.Current.Properties["DateFilterkey"];
                if (datefilter != null)
                {
                    SortByDateText.Text = datefilter.ToString();

                }
            }



        }

        public async Task OnViewDisappearingAsync(VisualElement view)
        {
            //    ////Clear priority///
            //    sortByPrioritypickerTitlesitems.Clear();
            //    SortByPriorityPickerTitles.Clear();
            //    PriorityNameFilterText = null;

            //    ////Clear Shift///
            //    sortByShiftpickerTitlesitems.Clear();
            //    SortByShiftpickerTitles.Clear();
            //    ShiftNameFilterText = null;


            //    ////Clear Location///
            //    sortByLocationpickerTitlesitems.Clear();
            //    SortByLocationpickerTitles.Clear();
            //    LocationNameFilterText = null;

            //    ///Clear RequiredDate////
            //    SortByDateText = null;
        }

        private void SetCompletionDateResult(DatePromptResult result)
        {
            if (result.Ok)
            {

                if (result.Value.Date == DateTime.Parse("1/1/0001 12:00:00 AM"))
                {
                    SortByDateText.Text = string.Empty;
                }
                else
                {
                    SortByDateText.Text = result.SelectedDate.Date.ToString("dd MMM yyyy");
                }
            }
        }





        private async Task RefillWororderFromPicker()
        {
            if (SelectedIndexPicker == -1)
            {
                return;
            }

            var SelectedPickerText = PickerTitles[SelectedIndexPicker];

            //var ShiftSelectedPickerText = SortByShiftpickerTitles[ShiftSelectedIndexPicker];
            //var PrioritySelectedPickerText = SortByPriorityPickerTitles[PrioritySelectedIndexPicker];


            if (SelectedPickerText == SelectTitle)
            {
                WorkorderTypeFilterText = null;
                await RefillWorkorderCollection();
            }

            else if (SelectedPickerText == PreventiveMaintenenceTitle)
            {
                WorkorderTypeFilterText = "PreventiveMaintenance";
                await RefillWorkorderCollection();
            }

            else if (SelectedPickerText == DemandMaintenenceTitle)
            {
                WorkorderTypeFilterText = "DemandMaintenance";
                await RefillWorkorderCollection();

            }

            else if (SelectedPickerText == EmergencyMaintenanceTitle)
            {
                WorkorderTypeFilterText = "EmergencyMaintenance";
                await RefillWorkorderCollection();

            }

            else
            {
                WorkorderTypeFilterText = null;
                await RefillWorkorderCollection();
            }




        }

        private async Task AddWorkordersInWorkorderCollection(List<workOrders> workorders)
        {

            if (workorders != null && workorders.Count > 0)
            {
                foreach (var item in workorders)
                {

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        _workordersCollection.Add(item);
                        OnPropertyChanged(nameof(WorkordersCollection));
                    });

                }


            }
        }

        int _totalRecordCount;
        public int TotalRecordCount
        {
            get
            {
                return _totalRecordCount;
            }

            set
            {
                if (value != _totalRecordCount)
                {
                    _totalRecordCount = value;
                    OnPropertyChanged("TotalRecordCount");
                }
            }
        }

        string _searchText;
        public string SearchText
        {
            get
            {
                return _searchText;
            }

            set
            {
                if (value != _searchText)
                {
                    _searchText = value;
                    OnPropertyChanged("SearchText");
                    SearchText_TextChanged();
                }
            }
        }

        private void SearchText_TextChanged()
        {

            try
            {
                if (SearchText == null || SearchText.Length == 0)
                {
                    SearchButtonTitle = ScanTitle;
                }

                else if (SearchText.Length > 0)
                {
                    SearchButtonTitle = GoTitle;
                }
            }
            catch (Exception ex)
            {


            }
        }

        string _goTitle;
        public string GoTitle
        {
            get
            {
                return _goTitle;
            }

            set
            {
                if (value != _goTitle)
                {
                    _goTitle = value;
                    OnPropertyChanged("GoTitle");
                }
            }
        }

        string _searchButtonTitle;
        public string SearchButtonTitle
        {
            get
            {
                return _searchButtonTitle;
            }

            set
            {
                if (value != _searchButtonTitle)
                {
                    _searchButtonTitle = value;
                    OnPropertyChanged("SearchButtonTitle");
                }
            }
        }

        string _scanTitle;
        public string ScanTitle
        {
            get
            {
                return _scanTitle;
            }

            set
            {
                if (value != _scanTitle)
                {
                    _scanTitle = value;
                    OnPropertyChanged("ScanTitle");
                }
            }
        }

        string _selectedSortingText;
        public string SelectedSortingText
        {
            get
            {
                return _selectedSortingText;
            }

            set
            {
                if (value != _selectedSortingText)
                {
                    _selectedSortingText = value;
                    OnPropertyChanged(nameof(SelectedSortingText));
                }
            }
        }



        private async void Button_OKClicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.ShowLoading();
            Application.Current.Properties["LocationFilterkey"] = LocationNameFilterText;
            Application.Current.Properties["ShiftFilterkey"] = ShiftNameFilterText;
            Application.Current.Properties["PriorityFilterkey"] = PriorityNameFilterText;
            Application.Current.Properties["DateFilterkey"] = SortByDateText.Text;
            await PopupNavigation.PopAsync();
            await ViewModel.OnViewAppearingAsync(null);
            UserDialogs.Instance.HideLoading();
        }
        private async void ShowDateCommand(object sender, EventArgs e)
        {
            UserDialogs.Instance.DatePrompt(new DatePromptConfig { OnAction = (result) => SetCompletionDateResult(result), IsCancellable = true });

        }

        private void Button_CancelClicked(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();

        }
        void OnDateClearTapped(object sender, EventArgs args)
        {
            SortByDateText.Text = null;
        }
        void OnLocationClearTapped(object sender, EventArgs args)
        {
            LocationPicker.SelectedItem = null;
            LocationNameFilterText = null;
        }
        void OnShiftClearTapped(object sender, EventArgs args)
        {
            ShiftPicker.SelectedItem = null;
            ShiftNameFilterText = null;
            Application.Current.Properties["ShiftFilterkeyText"] = "";
        }
        void OnPriorityClearTapped(object sender, EventArgs args)
        {
            PriorityPicker.SelectedItem = null;
            PriorityNameFilterText = null;
            Application.Current.Properties["PriorityFilterkeyText"] = "";
        }

        void LocationSelectedIndexPicker(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            if (picker.SelectedIndex == -1)
            {
                return;
            }

            var LocationSelectedPickerText = LocationPicker.Items[picker.SelectedIndex];

            if (LocationSelectedPickerText == SelectTitle)
            {
                LocationNameFilterText = null;

            }
            else
            {
                LocationNameFilterText = LocationSelectedPickerText;


            }
        }
        void ShiftSelectedIndexPicker(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            if (picker.SelectedIndex == -1)
            {
                return;
            }

            var ShiftSelectedPickerText = ShiftPicker.Items[picker.SelectedIndex];
            Application.Current.Properties["ShiftFilterkeyText"] = ShiftSelectedPickerText;
           
            if (ShiftSelectedPickerText == SelectTitle)
            {
                ShiftNameFilterText = null;

            }
            else
            {
                var filtered = from kvp in sortByShiftpickerTitlesitems
                               where kvp.Value.ToLower() == ShiftSelectedPickerText.ToLower()
                               select kvp.Key;
                int? firstItem = filtered.ElementAt(0);

                ShiftNameFilterText = firstItem.ToString();

            }


        }
        void PrioritySelectedIndexPicker(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            if (picker.SelectedIndex == -1)
            {
                return;
            }

            var PrioritySelectedPickerText = PriorityPicker.Items[picker.SelectedIndex];
            Application.Current.Properties["PriorityFilterkeyText"] = PrioritySelectedPickerText;
            if (PrioritySelectedPickerText == SelectTitle)
            {
                PriorityNameFilterText = null;

            }
          
            else
            {
                var filtered = from kvp in sortByPrioritypickerTitlesitems
                               where kvp.Value.ToLower() == PrioritySelectedPickerText.ToLower()
                               select kvp.Key;
                int? firstItem = filtered.ElementAt(0);

                PriorityNameFilterText = firstItem.ToString();
                
            }


        }
    }
}
        




        
    

