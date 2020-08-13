using Newtonsoft.Json;
using ProteusMMX.DependencyInterface;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.DateTime;
using ProteusMMX.Helpers.Storage;
using ProteusMMX.Helpers.StringFormatter;
using ProteusMMX.Model;
using ProteusMMX.Model.ClosedWorkOrderModel;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.CloseWorkorder;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Services.Workorder.TaskAndLabour;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace ProteusMMX.ViewModel.ClosedWorkorder
{
    public class ClosedWorkorderTaskAndLabourPageViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly IWorkorderService _workorderService;

        protected readonly ITaskAndLabourService _taskAndLabourService;
        #endregion

        #region Properties

        #region Page Properties
        int? _closedWorkorderID;
        public int? ClosedWorkorderID
        {
            get
            {
                return _closedWorkorderID;
            }

            set
            {
                if (value != _closedWorkorderID)
                {
                    _closedWorkorderID = value;
                    OnPropertyChanged(nameof(ClosedWorkorderID));
                }
            }
        }

        string _pageTitle = "";
        public string PageTitle
        {
            get
            {
                return _pageTitle;
            }

            set
            {
                if (value != _pageTitle)
                {
                    _pageTitle = value;
                    OnPropertyChanged("PageTitle");
                }
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

        ServiceOutput _formLoadInputForWorkorder;
        public ServiceOutput FormLoadInputForWorkorder //Use For Only translation purposes
        {
            get { return _formLoadInputForWorkorder; }
            set
            {
                if (value != _formLoadInputForWorkorder)
                {
                    _formLoadInputForWorkorder = value;
                    OnPropertyChanged(nameof(FormLoadInputForWorkorder));
                }
            }
        }

        ServiceOutput _formControlsAndRights;
        public ServiceOutput FormControlsAndRights
        {
            get { return _formControlsAndRights; }
            set
            {
                if (value != _formControlsAndRights)
                {
                    _formControlsAndRights = value;
                    OnPropertyChanged(nameof(FormControlsAndRights));
                }
            }
        }



        #endregion

        #region Title Properties

        string _welcomeTextTitle;
        public string WelcomeTextTitle
        {
            get
            {
                return _welcomeTextTitle;
            }

            set
            {
                if (value != _welcomeTextTitle)
                {
                    _welcomeTextTitle = value;
                    OnPropertyChanged("WelcomeTextTitle");
                }
            }
        }


        string _searchPlaceholder;
        public string SearchPlaceholder
        {
            get
            {
                return _searchPlaceholder;
            }

            set
            {
                if (value != _searchPlaceholder)
                {
                    _searchPlaceholder = value;
                    OnPropertyChanged(nameof(SearchPlaceholder));
                }
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
                    OnPropertyChanged(nameof(GoTitle));
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
                    OnPropertyChanged(nameof(ScanTitle));
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
                    OnPropertyChanged(nameof(SearchButtonTitle));
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
                    OnPropertyChanged(nameof(SearchText));
                    SearchText_TextChanged();
                }
            }
        }

        #endregion

        #region Dialog Actions Titles

        string _logoutTitle = "";
        public string LogoutTitle
        {
            get
            {
                return _logoutTitle;
            }

            set
            {
                if (value != _logoutTitle)
                {
                    _logoutTitle = value;
                    OnPropertyChanged("LogoutTitle");
                }
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

        string _cancelTitle = "";
        public string CancelTitle
        {
            get
            {
                return _cancelTitle;
            }

            set
            {
                if (value != _cancelTitle)
                {
                    _cancelTitle = value;
                    OnPropertyChanged("CancelTitle");
                }
            }
        }

        string _selectOptionsTitle;
        public string SelectOptionsTitle
        {
            get
            {
                return _selectOptionsTitle;
            }

            set
            {
                if (value != _selectOptionsTitle)
                {
                    _selectOptionsTitle = value;
                    OnPropertyChanged("SelectOptionsTitle");
                }
            }
        }


        #endregion



        #endregion



        #region TaskAndLabourPage Properties


        Page _page;
        public Page Page
        {
            get
            {
                return _page;
            }

            set
            {
                _page = value;
                OnPropertyChanged(nameof(Page));
            }
        }

        int _workorderID;
        public int WorkorderID
        {
            get
            {
                return _workorderID;
            }

            set
            {
                if (value != _workorderID)
                {
                    _workorderID = value;
                    OnPropertyChanged(nameof(WorkorderID));
                }
            }
        }



        string _serverTimeZone = AppSettings.User.ServerIANATimeZone;
        public string ServerTimeZone
        {
            get { return _serverTimeZone; }
        }


        bool? _inspectionUser = AppSettings.User.IsInspectionUser;
        public bool? InspectionUser
        {
            get { return _inspectionUser; }
        }



        string _addTaskTitle;
        public string AddTaskTitle
        {
            get
            {
                return _addTaskTitle;
            }

            set
            {
                if (value != _addTaskTitle)
                {
                    _addTaskTitle = value;
                    OnPropertyChanged(nameof(AddTaskTitle));
                }
            }
        }

        string _entryHours;
        public string EntryHours
        {
            get
            {
                return _entryHours;
            }

            set
            {
                if (value != _entryHours)
                {
                    _addTaskTitle = value;
                    OnPropertyChanged(nameof(EntryHours));
                }
            }
        }




        #endregion





        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);

        public ICommand ScanCommand => new AsyncCommand(SearchWorkorder);

        public string CornerRadius { get; private set; }

        #endregion

        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                OperationInProgress = true;

                //if (ConnectivityService.IsConnected == false)
                //{
                //    await DialogService.ShowAlertAsync("internet not available", "Alert", "OK");
                //    return;

                //}

                if (navigationData != null)
                {

                    var navigationParams = navigationData as PageParameters;
                    this.Page = navigationParams.Page;

                    var workorder = navigationParams.Parameter as ClosedWorkOrder;
                    this.ClosedWorkorderID = workorder.ClosedWorkOrderID;



                }

                await SetTitlesPropertiesForPage();
                FormControlsAndRights = await _formLoadInputService.GetFormControlsAndRights(UserID, AppSettings.WorkorderModuleName);
                await CreateTaskAndLabourLayout(FormControlsAndRights);


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

        public ClosedWorkorderTaskAndLabourPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IWorkorderService workorderService, ITaskAndLabourService taskAndLabourService)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _workorderService = workorderService;
            _taskAndLabourService = taskAndLabourService;
        }

        public async Task SearchWorkorder()
        {

            try
            {
                OperationInProgress = true;


                #region Barcode Section and Search Section

                if (SearchButtonTitle == ScanTitle)
                {
                    var options = new MobileBarcodeScanningOptions()
                    {
                        AutoRotate = false,
                        TryHarder = true,

                    };

                    ZXingScannerPage _scanner = new ZXingScannerPage(options)
                    {
                        DefaultOverlayTopText = "Align the barcode within the frame",
                        DefaultOverlayBottomText = string.Empty,
                        DefaultOverlayShowFlashButton = true
                    };

                    _scanner.OnScanResult += _scanner_OnScanResult;
                    var navPage = App.Current.MainPage as NavigationPage;
                    await navPage.PushAsync(_scanner);
                }

                else
                {

                    await GenerateTaskAndLabourLayout();

                }

                #endregion

                //await NavigationService.NavigateToAsync<WorkorderListingPageViewModel>();
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

        private async void _scanner_OnScanResult(ZXing.Result result)
        {
            //Set the text property
            this.SearchText = result.Text;

            ///Pop the scanner page
            Device.BeginInvokeOnMainThread(async () =>
            {
                var navPage = App.Current.MainPage as NavigationPage;
                await navPage.PopAsync();
                await GenerateTaskAndLabourLayout();


            });

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
        public async Task SetTitlesPropertiesForPage()
        {
            try
            {

                //var titles = await _formLoadInputService.GetFormLoadInputForBarcode(UserID, AppSettings.WorkorderDetailFormName);
                //if (titles != null && titles.listWebControlTitles != null && titles.listWebControlTitles.Count > 0)
                {
                    PageTitle = WebControlTitle.GetTargetNameByTitleName("TasksandLabor");
                    WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                    LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                    CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                    SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                    //SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName(titles, "SelectOptionsTitles"); 



                    AddTaskTitle = WebControlTitle.GetTargetNameByTitleName("AddTask");
                    EntryHours = WebControlTitle.GetTargetNameByTitleName("hh");

                    SearchPlaceholder = WebControlTitle.GetTargetNameByTitleName("SearchByTaskNumberOrDescription");
                    GoTitle = WebControlTitle.GetTargetNameByTitleName("Go");
                    ScanTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                    SearchButtonTitle = WebControlTitle.GetTargetNameByTitleName("Scan");
                    SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName("Select");


                }
            }
            catch (Exception ex)
            {


            }

            finally
            {

            }
        }



        public async Task GenerateTaskAndLabourLayout()
        {

            try
            {

                StackLayout contentLayout = await GetContentLayout();

                var control = new MyEntry();
                //clear the contentLayout 
                contentLayout.Children.Clear();

                ServiceOutput workorderLabour = null;
                ///TODO: Get Workorder data 
                workorderLabour = await _taskAndLabourService.ClosedWorkOrdersLaborByClosedWorkorderID(this.ClosedWorkorderID.ToString(), UserID);



                if (workorderLabour != null && workorderLabour.clWorkOrderWrapper != null && workorderLabour.clWorkOrderWrapper.clworkOrderLabors != null && workorderLabour.clWorkOrderWrapper.clworkOrderLabors.Count > 0)
                {


                    foreach (var item in workorderLabour.clWorkOrderWrapper.clworkOrderLabors)
                    {




                        Label taskNumberLabel = new Label { TextColor = Color.Black };
                        taskNumberLabel.Text = WebControlTitle.GetTargetNameByTitleName("TaskNumber") + ": " + item.TaskNumber;

                        Label estimatedHourLabel = new Label { TextColor = Color.Black, };
                        estimatedHourLabel.Text = WebControlTitle.GetTargetNameByTitleName("EstimatedHours") + ": " + string.Format(StringFormat.NumericZero(), item.EstimatedHours);

                        Button startButton = new Button
                        {
                            Text = WebControlTitle.GetTargetNameByTitleName("Start"),
                            IsVisible = false,
                            CornerRadius = 5,
                            BackgroundColor = Color.FromHex("#87CEFA"),
                            CommandParameter = item,
                            BorderColor = Color.Black,
                            TextColor = Color.White
                        };

                        Button stopButton = new Button
                        {
                            Text = WebControlTitle.GetTargetNameByTitleName("Stop"),
                            BackgroundColor = Color.FromHex("#87CEFA"),
                            CommandParameter = item,
                            CornerRadius = 5,
                            IsVisible = false,
                            BorderColor = Color.Black,
                            TextColor = Color.White
                        };

                        Button saveButton = new Button
                        {
                            Text = WebControlTitle.GetTargetNameByTitleName("Save"),
                            BackgroundColor = Color.FromHex("#87CEFA"),
                            CommandParameter = item,
                            CornerRadius = 5,
                            IsVisible = false,
                            BorderColor = Color.Black,
                            TextColor = Color.White
                        };

                        Button completeButton = new Button
                        {
                            Text = WebControlTitle.GetTargetNameByTitleName("Complete"),
                            BackgroundColor = Color.FromHex("#87CEFA"),
                            CommandParameter = item,
                            CornerRadius = 5,
                            IsVisible = false,
                            BorderColor = Color.Black,
                            TextColor = Color.White
                        };



                        Entry hoursEntry = new MyEntry

                        { TextColor = Color.Black, Placeholder = "hh" };

                        Entry minuteEntry = new MyEntry { TextColor = Color.Black, Placeholder = "mm", };

                        Entry hoursEntryforRate2 = new MyEntry { TextColor = Color.Black, Placeholder = "hh" };
                        Entry minuteEntryforRate2 = new MyEntry { TextColor = Color.Black, Placeholder = "mm", };

                        Label startDateLabel = new Label { Text = WebControlTitle.GetTargetNameByTitleName("StartDate") };
                        DatePicker startDatePicker = new DatePicker() { HorizontalOptions = LayoutOptions.Start, Date = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone), MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).Date };
                        StackLayout startDateStacklayout = new StackLayout() { Orientation = StackOrientation.Vertical, Children = { startDateLabel, startDatePicker } };


                        Label completionDateLabel = new Label { Text = WebControlTitle.GetTargetNameByTitleName("CompletionDate"), TextColor = Color.Black, };
                        Button completeDateButton = new Button() { HorizontalOptions = LayoutOptions.FillAndExpand, HeightRequest = 35, WidthRequest = 180, CornerRadius = 5, BackgroundColor = Color.FromHex("#87CEFA"), };

                        //CompHours.Clicked += CompletionDate_Clicked;



                        StackLayout completionDateStacklayout = new StackLayout() { Orientation = StackOrientation.Vertical, Children = { completionDateLabel, completeDateButton } };
                        StackLayout datesStacklayout = new StackLayout() { Orientation = StackOrientation.Horizontal, Children = { startDateStacklayout, completionDateStacklayout } };

                        DateTime startDateTask = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.StartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                        startDatePicker.Date = startDateTask;




                        try
                        {
                            string FinalHours = Convert.ToDecimal(string.Format("{0:F2}", item.HoursAtRate1)).ToString();
                            var FinalHrs1 = FinalHours.Split('.');
                            hoursEntry.Text = FinalHrs1[0];
                            minuteEntry.Text = FinalHrs1[1];

                            string FinalHoursrate2 = Convert.ToDecimal(string.Format("{0:F2}", item.HoursAtRate2)).ToString();
                            var FinalHrs2 = FinalHoursrate2.Split('.');
                            hoursEntryforRate2.Text = FinalHrs2[0];
                            minuteEntryforRate2.Text = FinalHrs2[1];
                            // CompHours.Text = item.CompletionDate.ToString();

                            //CompHours.Text = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.CompletionDate).ToUniversalTime(), ServerTimeZone).ToString(); 

                            completeDateButton.Text = item.CompletionDate != null ? DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString() : "";

                            //DateTime.Parse(CompHours.Text);
                        }
                        catch (Exception ex)
                        {

                        }
                        hoursEntry.TextChanged += OnTextChanged1;
                        minuteEntry.TextChanged += HoursTextChanged;

                        //hoursEntry.TextChanged += OnTextChanged1;
                        //minuteEntry.TextChanged += HoursTextChanged;

                        var startStopButtonGrid = new Grid();
                        startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });


                        startStopButtonGrid.Children.Add(startButton, 0, 0);
                        startStopButtonGrid.Children.Add(stopButton, 1, 0);
                        startStopButtonGrid.Children.Add(completeButton, 2, 0);
                        startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("hrs"), VerticalOptions = LayoutOptions.Center }, 3, 0);
                        startStopButtonGrid.Children.Add(hoursEntry, 4, 0);
                        startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("Min"), VerticalOptions = LayoutOptions.Center }, 5, 0);
                        startStopButtonGrid.Children.Add(minuteEntry, 6, 0);


                        /// Hours at Rate 2 Layout///////////////////////

                        var startStopButtonGridHours2 = new Grid();
                        startStopButtonGridHours2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridHours2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridHours2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridHours2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridHours2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridHours2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridHours2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridHours2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridHours2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });


                        startStopButtonGridHours2.Children.Add(startButton, 0, 0);
                        startStopButtonGridHours2.Children.Add(stopButton, 1, 0);
                        startStopButtonGridHours2.Children.Add(completeButton, 2, 0);
                        startStopButtonGridHours2.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("hrs"), VerticalOptions = LayoutOptions.Center }, 3, 0);
                        startStopButtonGridHours2.Children.Add(hoursEntryforRate2, 4, 0);
                        startStopButtonGridHours2.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("Min"), VerticalOptions = LayoutOptions.Center }, 5, 0);
                        startStopButtonGridHours2.Children.Add(minuteEntryforRate2, 6, 0);

                        if (AppSettings.User.EnableHoursAtRate == false)
                        {
                            startStopButtonGridHours2.IsVisible = false;
                        }


                        var descriptionGrid = new Grid();
                        descriptionGrid.Padding = new Thickness(5);
                        descriptionGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120, GridUnitType.Star) });

                        if (String.IsNullOrEmpty(item.Description))
                        {
                            descriptionGrid.Children.Add(new Label { TextColor = Color.Black, Text = "Description: " + item.Description, }, 0, 0);
                        }
                        else
                        {
                            // String result = Regex.Replace(item.Description, @"<[^>]*><br />&nbsp", String.Empty);
                            string result = RemoveHTML.StripHtmlTags(item.Description);
                            descriptionGrid.Children.Add(new Label { TextColor = Color.Black, Text = "Description: " + result }, 0, 0);
                        }

                        // grid1.Children.Add(new Label { Text ="Description:"+ item.Description, Font = Font.SystemFontOfSize(12, FontAttributes.Bold) }, 0, 0);

                        var employeeNameGrid = new Grid();
                        employeeNameGrid.Padding = new Thickness(5);
                        employeeNameGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120, GridUnitType.Star) });



                        var contractorNameTitle = WebControlTitle.GetTargetNameByTitleName("ContractorName") + ": ";
                        var employeeNameTitle = WebControlTitle.GetTargetNameByTitleName("EmployeeName") + ": ";


                        if (!String.IsNullOrEmpty(item.EmployeeName))
                        {
                            employeeNameGrid.Children.Add(new Label { TextColor = Color.Black, Text = employeeNameTitle + item.EmployeeName + "(" + item.LaborCraftCode + ")" }, 0, 0);
                        }
                        else if (!String.IsNullOrEmpty(item.ContractorName))
                        {
                            employeeNameGrid.Children.Add(new Label { TextColor = Color.Black, Text = contractorNameTitle + item.ContractorName + "(" + item.LaborCraftCode + ")" }, 0, 0);
                        }
                        Entry descriptionEntry;
                        if (String.IsNullOrEmpty(item.Description))
                        {
                            descriptionEntry = new MyEntry { IsEnabled = false, BackgroundColor = Color.FromHex("#D3D3D3"), Text = item.Description, WidthRequest = 300, HeightRequest = 150 };
                        }
                        else
                        {
                            string result = RemoveHTML.StripHtmlTags(item.Description);
                            descriptionEntry = new MyEntry { IsEnabled = false, BackgroundColor = Color.FromHex("#D3D3D3"), Text = result, WidthRequest = 300, HeightRequest = 150 };
                        }


                        #region GlobalTimer Logic
                        WorkOrderLabor savedWorkOrderLabor = null;
                        try
                        {


                        }
                        catch (Exception)
                        {

                        }

                        if (savedWorkOrderLabor != null)
                        {
                            try
                            {
                                //set in buttons commands

                                startButton.CommandParameter = savedWorkOrderLabor;
                                stopButton.CommandParameter = savedWorkOrderLabor;


                                startButton.BackgroundColor = Color.Green;
                                string FinalHours = Convert.ToDecimal(string.Format("{0:F2}", savedWorkOrderLabor.HoursAtRate1)).ToString();
                                var FinalHrs1 = FinalHours.Split('.');
                                hoursEntry.Text = FinalHrs1[0];
                                minuteEntry.Text = FinalHrs1[1];
                                //  CompHours.Text = item.CompletionDate.ToString();
                                completeDateButton.Text = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString();

                            }
                            catch (Exception ex)
                            {

                            }

                        }
                        #endregion





                        startButton.Clicked += (sender, e) =>
                        {
                            //save its workOrderLabor in local storage so we can start timer when we come on this page then we can retrive it.
                            var buttonStart = sender as Button;
                            WorkOrderLabor workOrderLabor = buttonStart.CommandParameter as WorkOrderLabor;

                            workOrderLabor.StartTimeOfTimer = DateTime.Now;
                            startButton.CommandParameter = workOrderLabor; //reassign to commandParameter.


                            var parent = buttonStart.Parent;
                            Grid parentGrid = parent as Grid;
                            Button btnStopLocal = parentGrid.Children[1] as Button;//Find the stopbutton from parent
                            btnStopLocal.CommandParameter = workOrderLabor; //reassign to commandParameter to stopbutton



                            //Save in Local
                            string key = "WorkOrderLabor:" + workOrderLabor.WorkOrderLaborID;
                            workOrderLabor.Description = "";
                            WorkOrderLaborStorge.Storage.Set(key, JsonConvert.SerializeObject(workOrderLabor));

                            //StartTime = DateTime.Now;

                            startButton.BackgroundColor = Color.Green;

                            stopButton.IsEnabled = true;
                            stopButton.BackgroundColor = Color.FromHex("#87CEFA");



                        };

                        stopButton.Clicked += (sender, e) =>
                        {
                            var StopTime = DateTime.Now;

                            var x1 = sender as Button;
                            WorkOrderLabor workOrderLabor = x1.CommandParameter as WorkOrderLabor;

                            if (workOrderLabor.StartTimeOfTimer == DateTime.Parse("1/1/0001 12:00:00 AM"))
                            {
                                return;
                            }

                            TimeSpan elapsed = StopTime.Subtract(workOrderLabor.StartTimeOfTimer);

                            int mn = elapsed.Minutes;
                            int mn1 = Convert.ToInt32(minuteEntry.Text);
                            if (mn + mn1 > 59)
                            {


                                TimeSpan span = TimeSpan.FromMinutes(mn + mn1);
                                string elapsedTime1 = String.Format("{0:00}:{1:00}",
                                                              span.Hours, span.Minutes);
                                int hrs = span.Hours;
                                int hrs1 = Convert.ToInt32(hoursEntry.Text);
                                hoursEntry.Text = (hrs + hrs1).ToString();

                                int hrs2 = span.Minutes;
                                minuteEntry.Text = hrs2.ToString();
                            }
                            else
                            {

                                int hrs = elapsed.Hours;
                                int hrs1 = Convert.ToInt32(hoursEntry.Text);
                                hoursEntry.Text = (hrs + hrs1).ToString();

                                int hrs2 = elapsed.Minutes;
                                int hrs21 = Convert.ToInt32(minuteEntry.Text);
                                minuteEntry.Text = (hrs2 + hrs21).ToString();
                            }


                            completeDateButton.Text = DateTime.Now.ToString();

                            stopButton.BackgroundColor = Color.Red;
                            startButton.BackgroundColor = Color.Red;
                        };

                        completeButton.Clicked += (sender, e) =>
                        {
                            stopButton.IsEnabled = false;
                            stopButton.BackgroundColor = Color.FromHex("#D3D3D3");

                            completeButton.IsEnabled = false;
                            completeButton.BackgroundColor = Color.FromHex("#D3D3D3");

                            startButton.IsEnabled = false;
                            startButton.BackgroundColor = Color.FromHex("#D3D3D3");

                            completeDateButton.Text = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).ToString();
                        };

                        //saveButton.Clicked += async (sender, e) =>
                        //{
                        //    try
                        //    {
                        //        OperationInProgress = true;
                        //        await SaveTaskAndLabour(sender, e);
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        OperationInProgress = false;

                        //    }

                        //    finally
                        //    {
                        //        OperationInProgress = false;
                        //    }
                        //};

                        //  completeDateButton.Clicked += CompletionDate_Clicked;

                        var taskNumberGrid = new Grid();
                        taskNumberGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.5, GridUnitType.Star) });
                        taskNumberGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.5, GridUnitType.Star) });
                        taskNumberGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });



                        taskNumberGrid.Children.Add(taskNumberLabel, 0, 0);
                        taskNumberGrid.Children.Add(estimatedHourLabel, 1, 0);
                        taskNumberGrid.Children.Add(saveButton, 2, 0);

                        string lbldesc = RemoveHTML.StripHtmlTags(descriptionEntry.Text);


                        Label lbld = new Label { TextColor = Color.Accent };
                        lbld.Text = WebControlTitle.GetTargetNameByTitleName("More");


                        var descriptionLayout = new StackLayout();
                        TargetNavigationData tnobj = new TargetNavigationData();
                        tnobj.Description = lbldesc;
                        var tapRecognizer = new TapGestureRecognizer { NumberOfTapsRequired = 1, Command = new Command(async () => await NavigationService.NavigateToAsync<DescriptionViewModel>(tnobj)) };
                        descriptionLayout.Children.Add(descriptionEntry);
                        tapRecognizer.Tapped += (sender, e) =>
                        {

                        };


                        lbld.GestureRecognizers.Add(tapRecognizer);

                        var layout1 = new StackLayout
                        {
                            Orientation = StackOrientation.Vertical,
                            Spacing = 12,
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            Children =
                                {
                                  taskNumberGrid,descriptionLayout,lbld, datesStacklayout , startStopButtonGrid,startStopButtonGridHours2,employeeNameGrid
                                }
                        };
                        var oneBox = new BoxView { BackgroundColor = Color.Black, HeightRequest = 2, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };

                        var FinalLayout = new StackLayout
                        {
                            Orientation = StackOrientation.Vertical,
                            HorizontalOptions = LayoutOptions.CenterAndExpand,
                            Children =
                                {
                                    layout1

                                }
                        };
                        FinalLayout.Children.Add(oneBox);
                        contentLayout.Children.Add(FinalLayout);
                    }




                }


            }
            catch (Exception ex)
            {


            }

            finally
            {

            }
        }

        //private async void CompletionDate_Clicked(object sender, EventArgs e)
        //{
        //    UserDialogs.Instance.DatePrompt(new DatePromptConfig { OnAction = (result) => SetCompletionDateResult(result, sender, e), IsCancellable = true });

        //}

        //private void SetCompletionDateResult(DatePromptResult result, object sender, EventArgs e)
        //{
        //    if (result.Ok)
        //    {
        //        var s = sender as Button;

        //        if (result.Value.Date == DateTime.Parse("1/1/0001 12:00:00 AM"))
        //        {
        //            s.Text = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.UserTimeZone).Date.ToString();
        //        }

        //        else
        //        {
        //            s.Text = result.SelectedDate.ToString();
        //        }

        //        if (result.Value.Date.Date > DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.UserTimeZone).Date)
        //        {
        //            DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("TaskCompletionDatecannotbefuturedate"));
        //            s.Text = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.UserTimeZone).Date.ToString();
        //        }


        //    }

        //    else
        //    {
        //        var s = sender as Button;
        //        s.Text = "";
        //    }

        //}

        //public async Task SaveTaskAndLabour(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        ///TODO: Get Workorder data 
        //        var workorderWrapper = await _workorderService.GetWorkorderByWorkorderID(UserID, WorkorderID.ToString());

        //        if (workorderWrapper != null && workorderWrapper.workOrderWrapper != null && workorderWrapper.workOrderWrapper.workOrder != null)
        //        {
        //            DateTime startDate = new DateTime();
        //            string completeDate = "";
        //            string hours = "";
        //            string minutes = "";


        //            DateTime? dateComp = null;
        //            DateTime? FinaldateComp = null;
        //            //TODO: find out the dates of pickers date
        //            try
        //            {
        //                var buttonSave = sender as Button;
        //                var parent = buttonSave.Parent;
        //                var parentLayout = parent.Parent as StackLayout;


        //                var datesLayout = parentLayout.Children[3] as StackLayout;
        //                var startDateLayout = datesLayout.Children[0] as StackLayout;
        //                var startDatePicker = startDateLayout.Children[1] as DatePicker;
        //                startDate = startDatePicker.Date;

        //                var completeDateLayout = datesLayout.Children[1] as StackLayout;
        //                var completeDateButton = completeDateLayout.Children[1] as Button;
        //                completeDate = completeDateButton.Text;



        //                //4,6

        //                var startAndStopButtonGrid = parentLayout.Children[4] as Grid;
        //                Entry hoursEntry = startAndStopButtonGrid.Children[4] as Entry; // Hours
        //                Entry minutesEntry = startAndStopButtonGrid.Children[6] as Entry; // Minutes

        //                hours = hoursEntry.Text;
        //                minutes = minutesEntry.Text;



        //            }
        //            catch (Exception ex)
        //            {


        //            }

        //            try
        //            {

        //                if (!Convert.ToBoolean(workorderWrapper.workOrderWrapper.IsCheckedAutoFillCompleteOnTaskAndLabor))
        //                {
        //                    if (!string.IsNullOrWhiteSpace(completeDate))
        //                    {
        //                        if (workorderWrapper.workOrderWrapper.workOrder.CompletionDate != null && Convert.ToDateTime(completeDate).Date > DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderWrapper.workOrderWrapper.workOrder.CompletionDate).ToUniversalTime(), ServerTimeZone))
        //                        {
        //                            //DialogService.ShowToast(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "CompletionDatecannotbegreaterthanWorkorderCompletionDate").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
        //                            DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("CompletionDatecannotbegreaterthanWorkorderCompletionDate"));
        //                            return;
        //                        }
        //                    }
        //                }


        //                string workordercompDate = string.Empty;
        //                string workorderstartDate = string.Empty;
        //                if (workorderWrapper.workOrderWrapper.workOrder.CompletionDate != null)
        //                {
        //                    workordercompDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderWrapper.workOrderWrapper.workOrder.CompletionDate).ToUniversalTime(), ServerTimeZone).ToString();
        //                }
        //                if (workorderWrapper.workOrderWrapper.workOrder.CompletionDate.HasValue && startDate > DateTime.Parse(workordercompDate))
        //                {
        //                    //DialogService.ShowToast(validationResult.ErrorMessage);
        //                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("TaskStartdatecannotbegreaterthanWorkOrderCompletionDate"));
        //                    return;
        //                }
        //                if (workorderWrapper.workOrderWrapper.workOrder.WorkStartedDate != null)
        //                {
        //                    workorderstartDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderWrapper.workOrderWrapper.workOrder.WorkStartedDate).ToUniversalTime(), ServerTimeZone).ToString("d");
        //                }
        //                if (workorderWrapper.workOrderWrapper.workOrder.WorkStartedDate.HasValue && startDate < DateTime.Parse(workorderstartDate))
        //                {
        //                    //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "TaskStartdatecannotbelessthanWorkOrderStartDate").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
        //                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("TaskStartdatecannotbelessthanWorkOrderStartDate"));
        //                    return;
        //                }



        //                if (ConnectivityService.IsConnected == false)
        //                {
        //                    DialogService.ShowToast("internet not available", 2000);
        //                    return;

        //                }

        //                if (!string.IsNullOrWhiteSpace(completeDate))
        //                {
        //                    dateComp = DateTime.Parse(completeDate);
        //                    FinaldateComp = dateComp.GetValueOrDefault().Date.Add(DateTime.Now.TimeOfDay);

        //                    if (startDate > DateTime.Parse(completeDate).Date)
        //                    {
        //                        //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "TaskCompletionDatedatecannotbelessthanTaskStartDate").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
        //                        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("TaskCompletionDatedatecannotbelessthanTaskStartDate"));
        //                        return;
        //                    }
        //                }


        //                var workorderLabour = (WorkOrderLabor)((Button)sender).CommandParameter;
        //                var taskID = workorderLabour.TaskID;
        //                var workOrderLaborID = workorderLabour.WorkOrderLaborID;

        //                if (String.IsNullOrEmpty(hours))
        //                {
        //                    hours = "0";
        //                }
        //                if (String.IsNullOrEmpty(minutes))
        //                {
        //                    minutes = "0";
        //                }
        //                if (minutes.Length == 1)
        //                {
        //                    int FormattedHours = Convert.ToInt32(minutes);
        //                    minutes = string.Format("{0:00}", FormattedHours);

        //                }

        //                decimal dec = decimal.Parse(hours + "." + minutes);

        //                var workOrderWrapper = new workOrderWrapper
        //                {
        //                    TimeZone = AppSettings.UserTimeZone,
        //                    CultureName = AppSettings.UserCultureName,
        //                    UserId = Convert.ToInt32(UserID),
        //                    ClientIANATimeZone = AppSettings.ClientIANATimeZone,
        //                    workOrderLabor = new WorkOrderLabor
        //                    {
        //                        CompletionDate = FinaldateComp,
        //                        HoursAtRate1 = dec,
        //                        TaskID = taskID,
        //                        StartDate = startDate.Date.Add(DateTime.Now.TimeOfDay),
        //                        // EmployeeLaborCraftID=item.EmployeeLaborCraftID,
        //                        WorkOrderLaborID = workOrderLaborID
        //                    },

        //                };




        //                var response = await _taskAndLabourService.UpdateTaskAndLabour(workOrderWrapper);
        //                if (response != null && bool.Parse(response.servicestatus))
        //                {

        //                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("TaskLabourSuccessfullyUpdated"), 2000);
        //                    try
        //                    {
        //                        var buttonSave = sender as Button;
        //                        var parent = buttonSave.Parent;
        //                        var parentLayout = parent.Parent as StackLayout;

        //                        var parentGrid = parentLayout.Children[4] as Grid;
        //                        Button btnStartLocal = parentGrid.Children[0] as Button;//Find the stopbutton from parent
        //                        Button btnStopLocal = parentGrid.Children[1] as Button;//Find the stopbutton from parent
        //                        Button btnCompleteLocal = parentGrid.Children[2] as Button;//Find the stopbutton from parent

        //                        var upadatedLabor = buttonSave.CommandParameter as WorkOrderLabor;  //reassign to commandParameter to stopbutton
        //                        upadatedLabor.HoursAtRate1 = dec;
        //                        btnStartLocal.CommandParameter = upadatedLabor;
        //                        btnStopLocal.CommandParameter = upadatedLabor;


        //                        btnStartLocal.IsEnabled = true;
        //                        btnStartLocal.BackgroundColor = Color.FromHex("#87CEFA");

        //                        btnCompleteLocal.IsEnabled = true;
        //                        btnCompleteLocal.BackgroundColor = Color.FromHex("#87CEFA");

        //                        string key = "WorkOrderLabor:" + workorderLabour.WorkOrderLaborID;
        //                        WorkOrderLaborStorge.Storage.Delete(key);
        //                    }
        //                    catch (Exception)
        //                    {


        //                    }
        //                    await this.OnViewAppearingAsync(null);
        //                }



        //            }
        //            catch (Exception ex)
        //            {


        //            }



        //        }
        //    }
        //    catch (Exception)
        //    {


        //    }
        //}

        private async Task<StackLayout> GetContentLayout()
        {
            var page = this.Page as ContentPage;
            if (page != null)
            {

                var parentGrid = page.Content as Grid;
                var grid = parentGrid.Children[0] as Grid;

                //var grid = page.Content as Grid;
                var scrollView = grid.Children[1] as ScrollView;
                var childStackLayout = scrollView.Content as StackLayout;
                return childStackLayout;
            }

            return null;
        }

        public async Task CreateTaskAndLabourLayout(ServiceOutput FormControlsAndRights)
        {


            ///Seperate the static controls so they don't create twice and we have to keep it
            ///some place so we set its visibility and required field also.
            ///

            #region Extract Details control


            //if (FormControlsAndRights != null && FormControlsAndRights.lstModules != null && FormControlsAndRights.lstModules.Count > 0)
            //{
            //    var workordersModule = FormControlsAndRights.lstModules[0];
            //    if (workordersModule.ModuleName == "Work Orders") //ModuleName can't be  changed in service 
            //    {
            //        if (workordersModule.lstSubModules != null && workordersModule.lstSubModules.Count > 0)
            //        {
            //            //var inspectionSubModule = workordersModule.lstSubModules.FirstOrDefault(i => i.SubModuleName == "INSP/Cal"); //SubModuleName can't be  changed in service
            //            var TasksSubModule = workordersModule.lstSubModules.FirstOrDefault(i => i.SubModuleName == "Tasks");

            //            if (TasksSubModule != null)
            //            {
            //                if (TasksSubModule.listControls != null && TasksSubModule.listControls.Count > 0)
            //                {
            //                    var EditTasks = TasksSubModule.listControls.FirstOrDefault(i => i.ControlName == "Edit");
            //                    var NewTasks = TasksSubModule.listControls.FirstOrDefault(i => i.ControlName == "New");
            //                    var ViewTasks = TasksSubModule.listControls.FirstOrDefault(i => i.ControlName == "View");
            //                }

            //                if (TasksSubModule.listDialoges != null && TasksSubModule.listDialoges.Count > 0)
            //                {
            //                    //var WorkorderDialog = TasksSubModule.listDialoges.FirstOrDefault(i => i.DialogName == "Work Order Dialog");
            //                    //if (WorkorderDialog != null && WorkorderDialog.listTab != null && WorkorderDialog.listTab.Count > 0)
            //                    //{
            //                    //    var DetailsTab = WorkorderDialog.listTab.FirstOrDefault(i => i.DialogTabName == "Details");

            //                    //    if (DetailsTab != null && DetailsTab.listControls != null && DetailsTab.listControls.Count > 0)
            //                    //    {
            //                    //        //WorkorderControlsNew = DetailsTab.listControls;
            //                    //    }
            //                    //}
            //                }
            //            }
            //        }
            //    }
            //}

            #endregion

        }

        public async Task ShowActions()
        {
            try
            {
                var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() { LogoutTitle });

                if (response == LogoutTitle)
                {
                    await _authenticationService.LogoutAsync();
                    await NavigationService.NavigateToAsync<LoginPageViewModel>();
                    await NavigationService.RemoveBackStackAsync();
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

        public async Task OnViewAppearingAsync(VisualElement view)
        {
            try
            {

                /////TODO: Get Workorder Labour data 
                //var workorderLabourWrapper = await _workorderService.GetWorkorderLabour(UserID, WorkorderID.ToString());


                /////TODO: Get Workorder data 
                //var workorderWrapper = await _workorderService.GetWorkorderByWorkorderID(UserID, WorkorderID.ToString());



                /////TODO: Get Inspection 
                //var Inspection = await _workorderService.GetWorkorderInspection(WorkorderID.ToString());


                /////TODO: Get Inspection Time 
                //var InspectionTime = await _workorderService.GetWorkorderInspectionTime(UserID, WorkorderID.ToString());

                try
                {
                    OperationInProgress = true;
                    await GenerateTaskAndLabourLayout();
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
            catch (Exception ex)
            {
                OperationInProgress = false;
            }
            finally
            {
                OperationInProgress = false;
            }
        }

        void OnTextChanged1(object sender, EventArgs e)
        {
            //btnComp.IsEnabled = true;
            //btnComp.BackgroundColor = Color.FromHex("#87CEFA");

            //btnStart.IsEnabled = false;
            //btnStart.BackgroundColor = Color.FromHex("#D3D3D3");
            Entry e1 = sender as Entry;
            String val = e1.Text; //Get Current Text

            if (val.Contains(" "))//If it is more than your character restriction
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(val, "^[0-9]*$"))
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }

        }

        async void HoursTextChanged(object sender, EventArgs e)
        {
            //btnComp.IsEnabled = true;
            //btnComp.BackgroundColor = Color.FromHex("#87CEFA");

            //btnStart.IsEnabled = false;
            //btnStart.BackgroundColor = Color.FromHex("#D3D3D3");
            Entry e1 = sender as Entry;
            String val = e1.Text; //Get Current Text

            if (val.Contains(" "))//If it is more than your character restriction
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(val, "^[0-9]*$"))
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }


            decimal minuteValue;
            if (string.IsNullOrWhiteSpace(val))
            {
                return;
            }
            var x = decimal.TryParse(val, out minuteValue);
            if (!x)
            {
                //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "WronginputinMinutes.").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("WronginputinMinutes."), 2000);
                e1.Text = "";
                return;
            }
            if (minuteValue > 59)
            {
                //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Minutesshouldbelessthen59").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("Minutesshouldbelessthen59"), 2000);
                e1.Text = "";
                return;
            }

        }

        public Task OnViewDisappearingAsync(VisualElement view)
        {
            return Task.FromResult(true);
        }


        #endregion
    }
}
