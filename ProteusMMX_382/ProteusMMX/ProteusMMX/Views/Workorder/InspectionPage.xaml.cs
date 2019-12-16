using Acr.UserDialogs;
using Newtonsoft.Json;
using NodaTime;
using ProteusMMX.Constants;
using ProteusMMX.Controls;
using ProteusMMX.DependencyInterface;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.DateTime;
using ProteusMMX.Helpers.Storage;
using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Navigation;
using ProteusMMX.Services.Request;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Services.Workorder.Inspection;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.SelectionListPagesViewModels.Workorder.TaskAndLabour;
using ProteusMMX.ViewModel.Workorder;
using ProteusMMX.Views.Common;
using ProteusMMX.Views.SelectionListPages.Workorder.TaskAndLabour;
using Rg.Plugins.Popup.Extensions;
using SignaturePad.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProteusMMX.Views.Workorder
{
    [XamlCompilation(XamlCompilationOptions.Skip)]
    public partial class InspectionPage : ContentPage
    {
        StackLayout layout1 = new StackLayout();
        private readonly IRequestService _requestService;
        Button btnCreateWorkorder;
        double totalTime;
        //Task<ServiceOutput> flInput;
        public int? WorkorderID { get; set; }
        public string BaseURL { get; set; }

        ServiceOutput CC;

        public StringBuilder AnswerText = new StringBuilder();
        bool _isPickerDataSubscribed;
        public bool IsPickerDataSubscribed
        {
            get { return _isPickerDataSubscribed; }
            set
            {
                if (value != _isPickerDataSubscribed)
                {
                    _isPickerDataSubscribed = value;
                    OnPropertyChanged(nameof(IsPickerDataSubscribed));
                }
            }
        }
        //Contractor
        int? _contractorID;
        public int? ContractorID
        {
            get
            {
                return _contractorID;
            }

            set
            {
                if (value != _contractorID)
                {
                    _contractorID = value;
                    OnPropertyChanged(nameof(ContractorID));
                }
            }
        }

        string _contractorName;
        public string ContractorName
        {
            get
            {
                return _contractorName;
            }

            set
            {
                if (value != _contractorName)
                {
                    _contractorName = value;
                    OnPropertyChanged(nameof(ContractorName));
                }
            }
        }
        //Employee
        int? _employeeID;
        public int? EmployeeID
        {
            get
            {
                return _employeeID;
            }

            set
            {
                if (value != _employeeID)
                {
                    _employeeID = value;
                    OnPropertyChanged(nameof(EmployeeID));
                }
            }
        }

        string _employeeName;
        public string EmployeeName
        {
            get
            {
                return _employeeName;
            }

            set
            {
                if (value != _employeeName)
                {
                    _employeeName = value;
                    OnPropertyChanged(nameof(EmployeeName));
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
        FormListButton CreateWorkorderRights;

        //TimeZone = AppSettings.UserTimeZone,
        //CultureName = AppSettings.UserCultureName,
        //UserId = Convert.ToInt32(UserID),
        //ClientIANATimeZone = AppSettings.ClientIANATimeZone,

        public string UserId = AppSettings.User.UserID.ToString();
        string ServerTimeZone = AppSettings.User.ServerIANATimeZone;
        string UserTimeZone = AppSettings.ClientIANATimeZone;

        public ServiceOutput inspectionTime { get; set; }
        public DateTime? InspectionCompletionDate { get; set; }
        public DateTime? InspectionStartDate { get; set; }



        public InspectionPage()
        {

            InitializeComponent();

            if (Device.Idiom == TargetIdiom.Phone)
            {
                //Phone.IsVisible = true;
                //Tablet.IsVisible = false;
            }
            else
            {
                // Phone.IsVisible = false;
                // Tablet.IsVisible = true;
            }

            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#85C1E9");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
            this.Title = WebControlTitle.GetTargetNameByTitleName("Inspection");
            // TotalInspectionTime.Text = WebControlTitle.GetTargetNameByTitleName("TotalInspectionTime") + "(hh:mm:ss)";
            //InspectionTimer.Text = WebControlTitle.GetTargetNameByTitleName("InspectionTimer") + "(hh:mm:ss)";
            // btnStartTimer.Text = WebControlTitle.GetTargetNameByTitleName("StartTimer");
            // btnCreateWorkorder.Text = WebControlTitle.GetTargetNameByTitleName("CreateWorkOrder");
            if (Device.Idiom == TargetIdiom.Phone)
            {
                // this.InspectionStartDateLabelPhone.Text = WebControlTitle.GetTargetNameByTitleName("InspectionStartDate");
                // this.InspectionCompletionDateLabelPhone.Text = WebControlTitle.GetTargetNameByTitleName("InspectionCompletionDate");

            }
            else
            {
                //  this.InspectionStartDateLabelTablet.Text = WebControlTitle.GetTargetNameByTitleName("InspectionStartDate");
                // this.InspectionCompletionDateLabelTablet.Text = WebControlTitle.GetTargetNameByTitleName("InspectionCompletionDate");
            }


        }

        InspectionPageViewModel ViewModel => this.BindingContext as InspectionPageViewModel;
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!IsPickerDataSubscribed)
            {

                //Retrive Emloyee
                MessagingCenter.Subscribe<object>(this, MessengerKeys.EmployeeRequested_AddTask, OnEmployeeRequested);

                //Retrive Contractor
                MessagingCenter.Subscribe<object>(this, MessengerKeys.ContractorRequested_AddTask, OnContractorRequested);


                IsPickerDataSubscribed = true;
            }


            if (BindingContext is IHandleViewAppearing viewAware)
            {
                await viewAware.OnViewAppearingAsync(this);
            }



            await OnAppearingOld();





        }


        protected async Task OnAppearingOld()
        {
            Button btnAddInspection = new Button();
            Button btnAddEmployee = new Button();
            Button btnAddContractor = new Button();
            btnCreateWorkorder = new Button();
           

            try
            {

                this.WorkorderID = ViewModel.WorkorderID;
                AnswerText.Clear();


                this.btnCreateWorkorder.IsVisible = false;


                #region Inspection Timer Region

                // For Retriving the Inspection Time of Workorder

                //  inspectionTime = await ViewModel._inspectionService.GetWorkorderInspectionTime(UserId, WorkorderID.ToString());

                //  var timeInspection = TimeSpan.FromSeconds(Convert.ToDouble(inspectionTime.InspectionTimeInSeconds));
                // var timeString = (int)timeInspection.TotalHours + ":" + timeInspection.Minutes + ":" + timeInspection.Seconds;
                //  var timeString = string.Format("{0:00}:{1:00}:{2:00}", ((int)timeInspection.TotalHours), timeInspection.Minutes, timeInspection.Seconds);
                //   this.InspectionTimeLabel.Text = timeString; //this.InspectionTimeLabel.Text = timeString; //TimeSpan.FromSeconds(Convert.ToDouble(inspectionTime.Result.InspectionTimeInSeconds)).ToString();
                ///   this.InspectionTimeLabel.BindingContext = new InspectionTimer() { WorkorderID = WorkorderID, TotalRunningTime = TimeSpan.FromSeconds(Convert.ToDouble(inspectionTime.InspectionTimeInSeconds)) };
                //   this.btnStartTimer.BindingContext = new InspectionTimer() { WorkorderID = WorkorderID };



                //if (inspectionTime.InspectionStartDate != null)
                //{
                //    if (Device.Idiom == TargetIdiom.Phone)
                //    {
                //        //this.PickerInspectionStartDatePhone.Text = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(inspectionTime.InspectionStartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("d");
                //    }
                //    else
                //    {
                //        //this.PickerInspectionStartDateTablet.Text = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(inspectionTime.InspectionStartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("d");
                //    }
                //    this.InspectionStartDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(inspectionTime.InspectionStartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).Date;
                //}
                //else
                //{
                //    if (Device.Idiom == TargetIdiom.Phone)
                //    {
                //        //this.PickerInspectionStartDatePhone.Text = " ";
                //    }
                //    else
                //    {
                //       // this.PickerInspectionStartDatePhone.Text = " ";
                //    }
                //    this.InspectionStartDate = null;

                //}


                //if (inspectionTime.InspectionCompletionDate != null)
                //{
                //    if (Device.Idiom == TargetIdiom.Phone)
                //    {
                //        //this.PickerInspectionCompletionDatePhone.Text = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(inspectionTime.InspectionCompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("d");
                //    }
                //    else
                //    {
                //       // this.PickerInspectionCompletionDateTablet.Text = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(inspectionTime.InspectionCompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("d");
                //    }
                //    this.InspectionCompletionDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(inspectionTime.InspectionCompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).Date;
                //}

                //else
                //{
                //    if (Device.Idiom == TargetIdiom.Phone)
                //    {
                //        //this.PickerInspectionCompletionDatePhone.Text = " ";
                //    }
                //    else
                //    {
                //       // this.PickerInspectionCompletionDateTablet.Text = " ";
                //    }
                //    this.InspectionCompletionDate = null;
                //}



                //#region Retrive local Saved Timers

                //InspectionTimer savedWorkOrderInspection = null;
                //try
                //{
                //    string k1 = "WorkOrderInspection:" + WorkorderID;
                //    savedWorkOrderInspection = JsonConvert.DeserializeObject<InspectionTimer>(WorkorderInspectionStorge.Storage.Get(k1));

                //}
                //catch (Exception)
                //{

                //}

                //if (savedWorkOrderInspection != null)
                //{
                //    try
                //    {

                //        // this.btnStartTimer.BindingContext = savedWorkOrderInspection;
                //        // this.TimerText.Text = TimeSpan.FromSeconds(Convert.ToDouble((int)savedWorkOrderInspection.TotalRunningTime.TotalSeconds)).ToString();

                //        if (savedWorkOrderInspection.IsTimerRunning)
                //        {
                //            //  btnStartTimer.Text = WebControlTitle.GetTargetNameByTitleName("StopTimer");
                //            // btnStartTimer.BackgroundColor = Color.Red;
                //        }

                //    }
                //    catch (Exception ex)
                //    {



                //    }

                //}

                //#endregion

                #endregion

                MainLayout.Children.Clear();
                ParentLayout.Children.Clear();

                await RetriveAllWorkorderInspectionsAsync();


                //// UI for Multiple Employeee/////////////////////
                ///
               
                foreach (var item in CC.workorderContractor)
                {

                    string FinalContractorName = string.Empty;

                    Label taskNumberLabel = new Label { TextColor = Color.Black };
                   
                    taskNumberLabel.Text = WebControlTitle.GetTargetNameByTitleName("ContractorName") + ": " + item.ContractorName + "   ";

                    //Label estimatedHourLabel = new Label { TextColor = Color.Black, };
                    //estimatedHourLabel.Text = WebControlTitle.GetTargetNameByTitleName("EstimatedHours") + ": " + string.Format(StringFormat.NumericZero(), item.EstimatedHours);

                    Button startButton = new Button
                    {
                        Text = WebControlTitle.GetTargetNameByTitleName("Start"),
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
                        BorderColor = Color.Black,
                        TextColor = Color.White
                    };

                    DatePicker startDate = new DatePicker
                    {
                        Date = Convert.ToDateTime(item.StartDate),
                        BackgroundColor = Color.FromHex("#87CEFA"),
                    };

                    DatePicker CompletionDate = new DatePicker
                    {
                        Date = Convert.ToDateTime(item.CompletionDate),
                        BackgroundColor = Color.FromHex("#87CEFA"),
                    };

                    btnAddInspection = new Button
                    {
                        Text = "AddInspection",
                        BackgroundColor = Color.FromHex("#87CEFA"),
                        //  CommandParameter = item,
                        BorderColor = Color.Black,
                        TextColor = Color.White
                    };

                    btnAddEmployee = new Button
                    {
                        Text = "AddEmployee",
                        BackgroundColor = Color.FromHex("#87CEFA"),
                        //  CommandParameter = item,
                        BorderColor = Color.Black,
                        TextColor = Color.White
                    };

                    btnAddContractor = new Button
                    {
                        Text = "AddContractor",
                        BackgroundColor = Color.FromHex("#87CEFA"),
                        //  CommandParameter = item,
                        BorderColor = Color.Black,
                        TextColor = Color.White
                    };

                    if (this.btnCreateWorkorder.IsVisible)
                    {
                        btnCreateWorkorder = new Button
                        {
                            Text = WebControlTitle.GetTargetNameByTitleName("CreateWorkOrder"),
                            BackgroundColor = Color.FromHex("#87CEFA"),
                            BorderColor = Color.Black,
                            TextColor = Color.White

                        };
                    }


                    Entry hoursEntry = new Entry { TextColor = Color.Black, Placeholder = "hh" };
                    Entry minuteEntry = new Entry { TextColor = Color.Black, Placeholder = "mm", };

                    #region GlobalTimer Logic
                    WorkorderContractor savedContractorlocal = null;

                    try
                    {

                        string k1 = "WorkOrderContractor:" + item.ContractorLaborCraftID;
                        savedContractorlocal = JsonConvert.DeserializeObject<WorkorderContractor>(WorkorderInspectionStorge.Storage.Get(k1));


                    }
                    catch (Exception)
                    {

                    }

                    if (savedContractorlocal != null)
                    {
                        try
                        {
                            //set in buttons commands

                            startButton.CommandParameter = savedContractorlocal;
                            stopButton.CommandParameter = savedContractorlocal;

                            startButton.BackgroundColor = Color.Green;
                            //startButtonforRate2.BackgroundColor = Color.Green;

                            var timeInspection = TimeSpan.FromSeconds(Convert.ToDouble(savedContractorlocal.InspectionTime));
                            var timeString = (int)timeInspection.Hours + ":" + timeInspection.Minutes + ":" + timeInspection.Seconds;

                            hoursEntry.Text = timeInspection.Hours.ToString();
                            minuteEntry.Text = timeInspection.Minutes.ToString();



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
                        WorkorderContractor workordercontrcator = buttonStart.CommandParameter as WorkorderContractor;

                        workordercontrcator.StartTimeOfTimer = DateTime.Now;
                        startButton.CommandParameter = workordercontrcator; //reassign to commandParameter.


                        var parent = buttonStart.Parent;
                        Grid parentGrid = parent as Grid;
                        //  parentGrid.StyleId = item.HoursAtRate1.ToString();
                        Button btnStopLocal = parentGrid.Children[1] as Button;//Find the stopbutton from parent
                        btnStopLocal.CommandParameter = workordercontrcator; //reassign to commandParameter to stopbutton



                        //Save in Local
                        string key = "WorkorderContracator:" + workordercontrcator.ContractorLaborCraftID;
                        // workorderempcontrcator.Description = "";
                        WorkorderInspectionStorge.Storage.Set(key, JsonConvert.SerializeObject(workordercontrcator));


                        //StartTime = DateTime.Now;

                        startButton.BackgroundColor = Color.Green;
                        stopButton.IsEnabled = true;
                        stopButton.BackgroundColor = Color.FromHex("#87CEFA");




                    };



                    stopButton.Clicked += (sender, e) =>
                    {
                        var StopTime = DateTime.Now;

                        var x1 = sender as Button;
                        WorkorderContractor workOrdercontrcator = x1.CommandParameter as WorkorderContractor;

                        if (workOrdercontrcator.StartTimeOfTimer == DateTime.Parse("1/1/0001 12:00:00 AM"))
                        {
                            return;
                        }

                        TimeSpan elapsed = StopTime.Subtract(workOrdercontrcator.StartTimeOfTimer);

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

                        startDate.Date =Convert.ToDateTime(DateTime.Now.ToString());
                        CompletionDate.Date = Convert.ToDateTime(DateTime.Now.ToString());
                        stopButton.BackgroundColor = BackgroundColor = Color.FromHex("#D3D3D3");
                        stopButton.IsEnabled = false;
                        startButton.BackgroundColor = BackgroundColor = Color.FromHex("#D3D3D3");
                        startButton.IsEnabled = false;
                        this.BackgroundColor = Color.White;


                    };

                    if (!string.IsNullOrWhiteSpace(item.InspectionTime))
                    {
                        var timeInspection = TimeSpan.FromSeconds(Convert.ToDouble(item.InspectionTime));
                        var timeString = (int)timeInspection.Hours + ":" + timeInspection.Minutes + ":" + timeInspection.Seconds;

                        hoursEntry.Text = timeInspection.Hours.ToString();
                        minuteEntry.Text = timeInspection.Minutes.ToString();
                    }

                    //Label startDateLabel = new Label { Text = WebControlTitle.GetTargetNameByTitleName("StartDate") };
                    //RequiredDateCustomDatePicker startDatePicker = new RequiredDateCustomDatePicker() { HorizontalOptions = LayoutOptions.Start, SelectedDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone), MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).Date };
                    //StackLayout startDateStacklayout = new StackLayout() { Orientation = StackOrientation.Vertical, Children = { startDateLabel, startDatePicker } };


                    //Label completionDateLabel = new Label { Text = WebControlTitle.GetTargetNameByTitleName("CompletionDate"), TextColor = Color.Black, };
                    //Button completeDateButton = new Button() { HorizontalOptions = LayoutOptions.FillAndExpand, HeightRequest = 35, WidthRequest = 180, BackgroundColor = Color.FromHex("#87CEFA"), };
                    //// Image image = new Image { Source = getSource() };
                    ////var tapGestureRecognizer = new TapGestureRecognizer();
                    ////tapGestureRecognizer.Tapped += imageClicked;
                    ////image.GestureRecognizers.Add(tapGestureRecognizer);

                    ////CompHours.Clicked += CompletionDate_Clicked;

                    //StackLayout completionDateImageStacklayout = new StackLayout()
                    //{
                    //    Orientation = StackOrientation.Horizontal,
                    //    Children = { completeDateButton }
                    //};

                    //StackLayout completionDateStacklayout = new StackLayout() { Orientation = StackOrientation.Vertical, Children = { completionDateLabel, completionDateImageStacklayout } };
                    //StackLayout datesStacklayout = new StackLayout() { Orientation = StackOrientation.Horizontal, Children = { startDateStacklayout, completionDateStacklayout } };

                    //DateTime startDateTask = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.StartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                    //startDatePicker.SelectedDate = startDateTask;



                    try
                    {
                        //string FinalHours = Convert.ToDecimal(string.Format("{0:F2}", item.HoursAtRate1)).ToString();
                        //var FinalHrs1 = FinalHours.Split('.');
                        //hoursEntry.Text = FinalHrs1[0];
                        //minuteEntry.Text = FinalHrs1[1];

                        //string FinalHours2 = Convert.ToDecimal(string.Format("{0:F2}", item.HoursAtRate2)).ToString();
                        //var FinalHrs2 = FinalHours2.Split('.');
                        //hoursEntryforRate2.Text = FinalHrs2[0];
                        //minuteEntryforRate2.Text = FinalHrs2[1];
                        //completeDateButton.Text = item.CompletionDate != null ? DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString() : "";


                    }
                    catch (Exception ex)
                    {

                    }
                    //hoursEntry.TextChanged += OnTextChanged1;
                    //minuteEntry.TextChanged += HoursTextChanged;

                    //hoursEntryforRate2.TextChanged += OnTextChanged1;
                    //minuteEntryforRate2.TextChanged += HoursTextChanged;


                    var startStopButtonGrid = new Grid();


                    startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });



                    startStopButtonGrid.Children.Add(startButton, 1, 0);
                    startStopButtonGrid.Children.Add(stopButton, 2, 0);
                    if (Device.Idiom == TargetIdiom.Phone)
                    {
                        startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("HoursAtRate1"), VerticalOptions = LayoutOptions.Center }, 0, 1);
                        startStopButtonGrid.Children.Add(hoursEntry, 1, 1);
                        startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("Min"), VerticalOptions = LayoutOptions.Center }, 0, 2);
                        startStopButtonGrid.Children.Add(minuteEntry, 1, 2);


                    }
                    else
                    {

                        startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("hrs"), VerticalOptions = LayoutOptions.Center }, 3, 0);
                        startStopButtonGrid.Children.Add(hoursEntry, 4, 0);
                        startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("Min"), VerticalOptions = LayoutOptions.Center }, 5, 0);
                        startStopButtonGrid.Children.Add(minuteEntry, 6, 0);
                        startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("StartDate"), VerticalOptions = LayoutOptions.Center }, 7, 0);
                        startStopButtonGrid.Children.Add(startDate, 8, 0);
                        startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("CompletionDate"), VerticalOptions = LayoutOptions.Center }, 9, 0);
                        startStopButtonGrid.Children.Add(CompletionDate, 10, 0);
                        startStopButtonGrid.Children.Add(new Label { Text = item.ContractorLaborCraftID.ToString() + ":" + "ContractorLaborCraft", IsVisible = false, BackgroundColor = Color.FromHex("#87CEFA"), }, 11, 0);
                    }





                    //var descriptionGrid = new Grid();
                    //descriptionGrid.Padding = new Thickness(5);
                    //descriptionGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120, GridUnitType.Star) });

                    //if (String.IsNullOrEmpty(item.Description))
                    //{
                    //    descriptionGrid.Children.Add(new Label { TextColor = Color.Black, Text = "Description: " + item.Description, }, 0, 0);
                    //}
                    //else
                    //{
                    //    // String result = Regex.Replace(item.Description, @"<[^>]*><br />&nbsp", String.Empty);
                    //    string result = RemoveHTML.StripHtmlTags(item.Description);
                    //    descriptionGrid.Children.Add(new Label { TextColor = Color.Black, Text = "Description: " + result }, 0, 0);
                    //}

                    //// grid1.Children.Add(new Label { Text ="Description:"+ item.Description, Font = Font.SystemFontOfSize(12, FontAttributes.Bold) }, 0, 0);

                    //var employeeNameGrid = new Grid();
                    //employeeNameGrid.Padding = new Thickness(5);
                    //employeeNameGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120, GridUnitType.Star) });



                    //var contractorNameTitle = WebControlTitle.GetTargetNameByTitleName("ContractorName") + ": ";
                    //var employeeNameTitle = WebControlTitle.GetTargetNameByTitleName("EmployeeName") + ": ";


                    //if (!String.IsNullOrEmpty(item.EmployeeName))
                    //{
                    //    employeeNameGrid.Children.Add(new Label { TextColor = Color.Black, Text = employeeNameTitle + item.EmployeeName + "(" + item.LaborCraftCode + ")" }, 0, 0);
                    //}
                    //else if (!String.IsNullOrEmpty(item.ContractorName))
                    //{
                    //    if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic"))
                    //    {
                    //        employeeNameGrid.Children.Add(new Label { TextColor = Color.Black, IsVisible = false, Text = contractorNameTitle + item.ContractorName + "(" + item.LaborCraftCode + ")" }, 0, 0);

                    //    }
                    //    else
                    //    {
                    //        employeeNameGrid.Children.Add(new Label { TextColor = Color.Black, Text = contractorNameTitle + item.ContractorName + "(" + item.LaborCraftCode + ")" }, 0, 0);
                    //    }
                    //}
                    //Entry descriptionEntry;


                    //if (String.IsNullOrEmpty(item.Description))
                    //{
                    //    descriptionEntry = new Entry { IsEnabled = false, BackgroundColor = Color.FromHex("#D3D3D3"), Text = item.Description, WidthRequest = 300, HeightRequest = 150 };
                    //    //LabelDescription = new Label { IsEnabled = false, BackgroundColor = Color.FromHex("#D3D3D3"), Text = item.Description, WidthRequest = 300, HeightRequest = 150, LineBreakMode = LineBreakMode.WordWrap };
                    //}
                    //else
                    //{
                    //    string result = RemoveHTML.StripHtmlTags(item.Description);
                    //    descriptionEntry = new Entry { IsEnabled = false, BackgroundColor = Color.FromHex("#D3D3D3"), Text = result, WidthRequest = 300, HeightRequest = 150 };
                    //    //LabelDescription = new Label { IsEnabled = false, BackgroundColor = Color.FromHex("#D3D3D3"), Text = result, WidthRequest = 300, HeightRequest = 150, LineBreakMode = LineBreakMode.WordWrap };
                    //}



                    //  WorkOrderLabor savedWorkOrderLabor = null;
                    // WorkOrderLabor savedWorkOrderLabor2 = null;
                    // WorkOrderLabor savedWorkOrderLabor1 = null;
                    try
                    {
                        //string rate1 = Convert.ToDecimal(string.Format("{0:F2}",item.HoursAtRate1)).ToString();
                        //var Finalrate1 = rate1.Split('.');
                        //string finalrate1 = Finalrate1[0];



                        //string k1 = "WorkOrderLabor:" + item.WorkOrderLaborID;
                        //string k2 = "WorkOrderLaborHours2:" + item.HoursAtRate2;

                        //// string k2 = "WorkOrderLabor:" + item.WorkOrderLaborID + finalrate2;
                        //savedWorkOrderLabor = JsonConvert.DeserializeObject<WorkOrderLabor>(WorkOrderLaborStorge.Storage.Get(k1));
                        //savedWorkOrderLabor2 = JsonConvert.DeserializeObject<WorkOrderLabor>(WorkOrderLaborStorge.Storage.Get(k2));

                    }
                    catch (Exception)
                    {

                    }

                    //if (savedWorkOrderLabor != null)
                    //{
                    //    try
                    //    {
                    //        //set in buttons commands

                    //        startButton.CommandParameter = savedWorkOrderLabor;
                    //        stopButton.CommandParameter = savedWorkOrderLabor;

                    //        string k3 = "WorkOrderLaborHours1:" + item.HoursAtRate1;
                    //        savedWorkOrderLabor1 = JsonConvert.DeserializeObject<WorkOrderLabor>(WorkOrderLaborStorge.Storage.Get(k3));
                    //        //startButtonforRate2.CommandParameter = savedWorkOrderLabor;
                    //        //stopButtonforRate2.CommandParameter = savedWorkOrderLabor;


                    //        startButton.BackgroundColor = Color.Green;
                    //        //startButtonforRate2.BackgroundColor = Color.Green;

                    //        string FinalHours = Convert.ToDecimal(string.Format("{0:F2}", savedWorkOrderLabor1.HoursAtRate1)).ToString();
                    //        var FinalHrs1 = FinalHours.Split('.');
                    //        hoursEntry.Text = FinalHrs1[0];
                    //        minuteEntry.Text = FinalHrs1[1];

                    //        //string FinalHours2 = Convert.ToDecimal(string.Format("{0:F2}", savedWorkOrderLabor.HoursAtRate2)).ToString();
                    //        //var FinalHrs2 = FinalHours2.Split('.');
                    //        //hoursEntryforRate2.Text = FinalHrs2[0];
                    //        //minuteEntryforRate2.Text = FinalHrs2[1];

                    //        completeDateButton.Text = item.CompletionDate != null ? DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString() : "";





                    //    }
                    //    catch (Exception ex)
                    //    {

                    //    }

                    //}

                    //if (savedWorkOrderLabor2 != null)
                    //{
                    //    try
                    //    {
                    //        //set in buttons commands


                    //        startButtonforRate2.CommandParameter = savedWorkOrderLabor2;
                    //        stopButtonforRate2.CommandParameter = savedWorkOrderLabor2;



                    //        startButtonforRate2.BackgroundColor = Color.Green;


                    //        string FinalHours2 = Convert.ToDecimal(string.Format("{0:F2}", savedWorkOrderLabor2.HoursAtRate2)).ToString();
                    //        var FinalHrs2 = FinalHours2.Split('.');
                    //        hoursEntryforRate2.Text = FinalHrs2[0];
                    //        minuteEntryforRate2.Text = FinalHrs2[1];

                    //        completeDateButton.Text = item.CompletionDate != null ? DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString() : "";





                    //    }
                    //    catch (Exception ex)
                    //    {

                    //    }

                    //}
                    //#endregion

                    //if (Application.Current.Properties.ContainsKey("CreateTask"))
                    //{
                    //    CreateTask = Application.Current.Properties["CreateTask"].ToString();


                    //}
                    //if (Application.Current.Properties.ContainsKey("EmployeeTab"))
                    //{
                    //    var EmployeeTab = Application.Current.Properties["EmployeeTab"].ToString();
                    //    if (EmployeeTab != null && EmployeeTab == "E")
                    //    {
                    //        employeeNameGrid.IsVisible = true;
                    //    }
                    //    else if (EmployeeTab == "V")
                    //    {
                    //        employeeNameGrid.IsEnabled = false;
                    //    }
                    //    else
                    //    {
                    //        employeeNameGrid.IsVisible = false;
                    //    }


                    //}
                    //if (Application.Current.Properties.ContainsKey("ContractorTab"))
                    //{
                    //    var ContractorTab = Application.Current.Properties["ContractorTab"].ToString();
                    //    if (ContractorTab != null && ContractorTab == "E")
                    //    {
                    //        employeeNameGrid.IsVisible = true;
                    //    }
                    //    else if (ContractorTab == "V")
                    //    {
                    //        employeeNameGrid.IsEnabled = false;
                    //    }
                    //    else
                    //    {
                    //        employeeNameGrid.IsVisible = false;
                    //    }


                    //}
                    //if (Application.Current.Properties.ContainsKey("CompletionDateTab"))
                    //{
                    //    var CompletionDateTab = Application.Current.Properties["CompletionDateTab"].ToString();
                    //    if (CompletionDateTab != null && CompletionDateTab == "E")
                    //    {
                    //        completeDateButton.IsVisible = true;
                    //        completionDateLabel.IsVisible = true;
                    //    }
                    //    else if (CompletionDateTab == "V")
                    //    {
                    //        completeDateButton.IsEnabled = false;
                    //        completionDateLabel.IsEnabled = false;
                    //    }
                    //    else
                    //    {
                    //        completeDateButton.IsVisible = false;
                    //        completionDateLabel.IsVisible = false;
                    //    }


                    //}
                    //if (Application.Current.Properties.ContainsKey("StartdateTab"))
                    //{
                    //    var StartdateTab = Application.Current.Properties["StartdateTab"].ToString();
                    //    if (StartdateTab != null && StartdateTab == "E")
                    //    {
                    //        startDateStacklayout.IsVisible = true;
                    //    }
                    //    else if (StartdateTab == "V")
                    //    {
                    //        startDateStacklayout.IsEnabled = false;

                    //    }
                    //    else
                    //    {
                    //        startDateStacklayout.IsVisible = false;
                    //    }


                    //}




                    //var taskNumberGrid = new Grid();
                    //taskNumberGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.5, GridUnitType.Star) });
                    //taskNumberGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.5, GridUnitType.Star) });
                    //taskNumberGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });


                    layout1 = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        Spacing = 10,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        Children =
                                {
                                 new Label{ Text=taskNumberLabel.Text,FontAttributes=FontAttributes.Bold},startStopButtonGrid
                                }
                    };
                    ParentLayout.Children.Add(layout1);
                }
                foreach (var item in CC.workOrderEmployee)
                {
                    string FinalEmployeeName = string.Empty;
                    Label taskNumberLabel = new Label { TextColor = Color.Black };
                   
                    taskNumberLabel.Text = WebControlTitle.GetTargetNameByTitleName("EmployeeName") + ": " + item.EmployeeName + "   ";

                    //Label estimatedHourLabel = new Label { TextColor = Color.Black, };
                    //estimatedHourLabel.Text = WebControlTitle.GetTargetNameByTitleName("EstimatedHours") + ": " + string.Format(StringFormat.NumericZero(), item.EstimatedHours);

                    Button startButton = new Button
                    {
                        Text = WebControlTitle.GetTargetNameByTitleName("Start"),
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
                        BorderColor = Color.Black,
                        TextColor = Color.White
                    };
                    DatePicker startDate = new DatePicker
                    {
                        Date = Convert.ToDateTime(item.StartDate),
                        BackgroundColor = Color.FromHex("#87CEFA"),
                    };

                    DatePicker CompletionDate = new DatePicker
                    {
                        Date = Convert.ToDateTime(item.CompletionDate),
                        BackgroundColor = Color.FromHex("#87CEFA"),
                    };

                    btnAddInspection = new Button
                    {
                        Text = "Add Inspection",
                        BackgroundColor = Color.FromHex("#87CEFA"),
                        //  CommandParameter = item,
                        BorderColor = Color.Black,
                        TextColor = Color.White
                    };

                    btnAddEmployee = new Button
                    {
                        Text = "Add Employee",
                        BackgroundColor = Color.FromHex("#87CEFA"),
                        //  CommandParameter = item,
                        BorderColor = Color.Black,
                        TextColor = Color.White
                    };

                    btnAddContractor = new Button
                    {
                        Text = "Add Contractor",
                        BackgroundColor = Color.FromHex("#87CEFA"),
                        //  CommandParameter = item,
                        BorderColor = Color.Black,
                        TextColor = Color.White
                    };

                    if (this.btnCreateWorkorder.IsVisible)
                    {
                        btnCreateWorkorder = new Button
                        {
                            Text = WebControlTitle.GetTargetNameByTitleName("CreateWorkOrder"),
                            BackgroundColor = Color.FromHex("#87CEFA"),
                            BorderColor = Color.Black,
                            TextColor = Color.White

                        };
                    }

                    btnAddInspection.Clicked += (sender, e) =>
                    {
                        var page = new AddInspectionData(WorkorderID);
                        Navigation.PushAsync(page);

                    };
                   
                    btnAddEmployee.Clicked += (sender, e) =>
                    {
                        ViewModel._navigationService.NavigateToAsync<EmployeeListSelectionPageViewModel>();

                    };

                    btnCreateWorkorder.Clicked += (sender, e) =>
                    {
                        var page = new CreateWorkorderFromInspectionPageContent(WorkorderID, AnswerText);
                        Navigation.PushAsync(page);


                    };
                    Entry hoursEntry = new Entry { TextColor = Color.Black, Placeholder = "hh" };
                    Entry minuteEntry = new Entry { TextColor = Color.Black, Placeholder = "mm", };

                    #region GlobalTimer Logic
                    WorkOrderEmployee savedemployeelocal = null;
                    
                    try
                    {
                       
                        string k1 = "WorkOrderEmployee:" + item.EmployeeLaborCraftID;
                        savedemployeelocal = JsonConvert.DeserializeObject<WorkOrderEmployee>(WorkorderInspectionStorge.Storage.Get(k1));


                    }
                    catch (Exception)
                    {

                    }

                    if (savedemployeelocal != null)
                    {
                        try
                        {
                            //set in buttons commands

                            startButton.CommandParameter = savedemployeelocal;
                            stopButton.CommandParameter = savedemployeelocal;

                            startButton.BackgroundColor = Color.Green;
                            //startButtonforRate2.BackgroundColor = Color.Green;

                            var timeInspection = TimeSpan.FromSeconds(Convert.ToDouble(savedemployeelocal.InspectionTime));
                            var timeString = (int)timeInspection.Hours + ":" + timeInspection.Minutes + ":" + timeInspection.Seconds;

                            hoursEntry.Text = timeInspection.Hours.ToString();
                            minuteEntry.Text = timeInspection.Minutes.ToString();



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
                        WorkOrderEmployee workorderemployee = buttonStart.CommandParameter as WorkOrderEmployee;

                        workorderemployee.StartTimeOfTimer = DateTime.Now;
                        startButton.CommandParameter = workorderemployee; //reassign to commandParameter.


                        var parent = buttonStart.Parent;
                        Grid parentGrid = parent as Grid;
                        //  parentGrid.StyleId = item.HoursAtRate1.ToString();
                        Button btnStopLocal = parentGrid.Children[1] as Button;//Find the stopbutton from parent
                        btnStopLocal.CommandParameter = workorderemployee; //reassign to commandParameter to stopbutton



                        //Save in Local
                        string key = "WorkorderEmployee:" + workorderemployee.EmployeeLaborCraftID;
                        // workorderempcontrcator.Description = "";
                        WorkorderInspectionStorge.Storage.Set(key, JsonConvert.SerializeObject(workorderemployee));


                        //StartTime = DateTime.Now;

                        startButton.BackgroundColor = Color.Green;
                        stopButton.IsEnabled = true;
                        stopButton.BackgroundColor = Color.FromHex("#87CEFA");




                    };

                

                    stopButton.Clicked += (sender, e) =>
                    {
                        var StopTime = DateTime.Now;

                        var x1 = sender as Button;
                        WorkOrderEmployee workOrderemp = x1.CommandParameter as WorkOrderEmployee;

                        if (workOrderemp.StartTimeOfTimer == DateTime.Parse("1/1/0001 12:00:00 AM"))
                        {
                            return;
                        }

                        TimeSpan elapsed = StopTime.Subtract(workOrderemp.StartTimeOfTimer);

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

                        startDate.Date = Convert.ToDateTime(DateTime.Now.ToString());
                        CompletionDate.Date = Convert.ToDateTime(DateTime.Now.ToString());
                        stopButton.BackgroundColor = BackgroundColor = Color.FromHex("#D3D3D3");
                        stopButton.IsEnabled = false;
                        startButton.BackgroundColor = BackgroundColor = Color.FromHex("#D3D3D3");
                        startButton.IsEnabled = false;
                        this.BackgroundColor = Color.White;


                    };

                    btnAddContractor.Clicked += (sender, e) =>
                    {
                        ViewModel._navigationService.NavigateToAsync<ContractorListSelectionPageViewModel>();

                    };

                    

                    if (!string.IsNullOrWhiteSpace(item.InspectionTime))
                    {
                        var timeInspection = TimeSpan.FromSeconds(Convert.ToDouble(item.InspectionTime));
                        var timeString = (int)timeInspection.Hours + ":" + timeInspection.Minutes + ":" + timeInspection.Seconds;

                        hoursEntry.Text = timeInspection.Hours.ToString();
                        minuteEntry.Text = timeInspection.Minutes.ToString();
                    }





                    Label startDateLabel = new Label { Text = WebControlTitle.GetTargetNameByTitleName("StartDate") };
                    RequiredDateCustomDatePicker startDatePicker = new RequiredDateCustomDatePicker() { HorizontalOptions = LayoutOptions.Start, SelectedDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone), MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).Date };
                    StackLayout startDateStacklayout = new StackLayout() { Orientation = StackOrientation.Vertical, Children = { startDateLabel, startDatePicker } };


                    Label completionDateLabel = new Label { Text = WebControlTitle.GetTargetNameByTitleName("CompletionDate"), TextColor = Color.Black, };
                    Button completeDateButton = new Button() { HorizontalOptions = LayoutOptions.FillAndExpand, HeightRequest = 35, WidthRequest = 180, BackgroundColor = Color.FromHex("#87CEFA"), };
                    // Image image = new Image { Source = getSource() };
                    //var tapGestureRecognizer = new TapGestureRecognizer();
                    //tapGestureRecognizer.Tapped += imageClicked;
                    //image.GestureRecognizers.Add(tapGestureRecognizer);

                    //CompHours.Clicked += CompletionDate_Clicked;

                    StackLayout completionDateImageStacklayout = new StackLayout()
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children = { completeDateButton }
                    };

                    StackLayout completionDateStacklayout = new StackLayout() { Orientation = StackOrientation.Vertical, Children = { completionDateLabel, completionDateImageStacklayout } };
                    StackLayout datesStacklayout = new StackLayout() { Orientation = StackOrientation.Horizontal, Children = { startDateStacklayout, completionDateStacklayout } };

                    //DateTime startDateTask = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.StartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                    //startDatePicker.SelectedDate = startDateTask;



                    try
                    {
                        //string FinalHours = Convert.ToDecimal(string.Format("{0:F2}", item.HoursAtRate1)).ToString();
                        //var FinalHrs1 = FinalHours.Split('.');
                        //hoursEntry.Text = FinalHrs1[0];
                        //minuteEntry.Text = FinalHrs1[1];

                        //string FinalHours2 = Convert.ToDecimal(string.Format("{0:F2}", item.HoursAtRate2)).ToString();
                        //var FinalHrs2 = FinalHours2.Split('.');
                        //hoursEntryforRate2.Text = FinalHrs2[0];
                        //minuteEntryforRate2.Text = FinalHrs2[1];
                        //completeDateButton.Text = item.CompletionDate != null ? DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString() : "";


                    }
                    catch (Exception ex)
                    {

                    }
                    //hoursEntry.TextChanged += OnTextChanged1;
                    //minuteEntry.TextChanged += HoursTextChanged;

                    //hoursEntryforRate2.TextChanged += OnTextChanged1;
                    //minuteEntryforRate2.TextChanged += HoursTextChanged;



                    var startStopButtonGrid = new Grid();

                    startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                    startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });


                    // startStopButtonGrid.Children.Add(taskNumberLabel, 0, 0);
                    startStopButtonGrid.Children.Add(startButton, 1, 0);
                    startStopButtonGrid.Children.Add(stopButton, 2, 0);
                    if (Device.Idiom == TargetIdiom.Phone)
                    {
                        startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("hrs"), VerticalOptions = LayoutOptions.Center }, 0, 1);
                        startStopButtonGrid.Children.Add(hoursEntry, 1, 1);
                        startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("Min"), VerticalOptions = LayoutOptions.Center }, 0, 2);
                        startStopButtonGrid.Children.Add(minuteEntry, 1, 2);


                    }
                    else
                    {

                        startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("hrs"), VerticalOptions = LayoutOptions.Center }, 3, 0);
                        startStopButtonGrid.Children.Add(hoursEntry, 4, 0);
                        startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("Min"), VerticalOptions = LayoutOptions.Center }, 5, 0);
                        startStopButtonGrid.Children.Add(minuteEntry, 6, 0);
                        startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("StartDate"), VerticalOptions = LayoutOptions.Center }, 7, 0);
                        startStopButtonGrid.Children.Add(startDate, 8, 0);
                        startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("CompletionDate"), VerticalOptions = LayoutOptions.Center }, 9, 0);
                        startStopButtonGrid.Children.Add(CompletionDate, 10, 0);
                        startStopButtonGrid.Children.Add(new Label { Text = item.EmployeeLaborCraftID.ToString() + ":" + "EmployeeLaborCraft", IsVisible = false, BackgroundColor = Color.FromHex("#87CEFA"), }, 11, 0);
                    }





                    //var descriptionGrid = new Grid();
                    //descriptionGrid.Padding = new Thickness(5);
                    //descriptionGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120, GridUnitType.Star) });

                    //if (String.IsNullOrEmpty(item.Description))
                    //{
                    //    descriptionGrid.Children.Add(new Label { TextColor = Color.Black, Text = "Description: " + item.Description, }, 0, 0);
                    //}
                    //else
                    //{
                    //    // String result = Regex.Replace(item.Description, @"<[^>]*><br />&nbsp", String.Empty);
                    //    string result = RemoveHTML.StripHtmlTags(item.Description);
                    //    descriptionGrid.Children.Add(new Label { TextColor = Color.Black, Text = "Description: " + result }, 0, 0);
                    //}

                    //// grid1.Children.Add(new Label { Text ="Description:"+ item.Description, Font = Font.SystemFontOfSize(12, FontAttributes.Bold) }, 0, 0);

                    //var employeeNameGrid = new Grid();
                    //employeeNameGrid.Padding = new Thickness(5);
                    //employeeNameGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120, GridUnitType.Star) });



                    //var contractorNameTitle = WebControlTitle.GetTargetNameByTitleName("ContractorName") + ": ";
                    //var employeeNameTitle = WebControlTitle.GetTargetNameByTitleName("EmployeeName") + ": ";


                    //if (!String.IsNullOrEmpty(item.EmployeeName))
                    //{
                    //    employeeNameGrid.Children.Add(new Label { TextColor = Color.Black, Text = employeeNameTitle + item.EmployeeName + "(" + item.LaborCraftCode + ")" }, 0, 0);
                    //}
                    //else if (!String.IsNullOrEmpty(item.ContractorName))
                    //{
                    //    if (AppSettings.User.blackhawkLicValidator.ProductLevel.Equals("Basic"))
                    //    {
                    //        employeeNameGrid.Children.Add(new Label { TextColor = Color.Black, IsVisible = false, Text = contractorNameTitle + item.ContractorName + "(" + item.LaborCraftCode + ")" }, 0, 0);

                    //    }
                    //    else
                    //    {
                    //        employeeNameGrid.Children.Add(new Label { TextColor = Color.Black, Text = contractorNameTitle + item.ContractorName + "(" + item.LaborCraftCode + ")" }, 0, 0);
                    //    }
                    //}
                    //Entry descriptionEntry;


                    //if (String.IsNullOrEmpty(item.Description))
                    //{
                    //    descriptionEntry = new Entry { IsEnabled = false, BackgroundColor = Color.FromHex("#D3D3D3"), Text = item.Description, WidthRequest = 300, HeightRequest = 150 };
                    //    //LabelDescription = new Label { IsEnabled = false, BackgroundColor = Color.FromHex("#D3D3D3"), Text = item.Description, WidthRequest = 300, HeightRequest = 150, LineBreakMode = LineBreakMode.WordWrap };
                    //}
                    //else
                    //{
                    //    string result = RemoveHTML.StripHtmlTags(item.Description);
                    //    descriptionEntry = new Entry { IsEnabled = false, BackgroundColor = Color.FromHex("#D3D3D3"), Text = result, WidthRequest = 300, HeightRequest = 150 };
                    //    //LabelDescription = new Label { IsEnabled = false, BackgroundColor = Color.FromHex("#D3D3D3"), Text = result, WidthRequest = 300, HeightRequest = 150, LineBreakMode = LineBreakMode.WordWrap };
                    //}



                    //  WorkOrderLabor savedWorkOrderLabor = null;
                    // WorkOrderLabor savedWorkOrderLabor2 = null;
                    // WorkOrderLabor savedWorkOrderLabor1 = null;
                    try
                    {
                        //string rate1 = Convert.ToDecimal(string.Format("{0:F2}",item.HoursAtRate1)).ToString();
                        //var Finalrate1 = rate1.Split('.');
                        //string finalrate1 = Finalrate1[0];



                        //string k1 = "WorkOrderLabor:" + item.WorkOrderLaborID;
                        //string k2 = "WorkOrderLaborHours2:" + item.HoursAtRate2;

                        //// string k2 = "WorkOrderLabor:" + item.WorkOrderLaborID + finalrate2;
                        //savedWorkOrderLabor = JsonConvert.DeserializeObject<WorkOrderLabor>(WorkOrderLaborStorge.Storage.Get(k1));
                        //savedWorkOrderLabor2 = JsonConvert.DeserializeObject<WorkOrderLabor>(WorkOrderLaborStorge.Storage.Get(k2));

                    }
                    catch (Exception)
                    {

                    }

                    //if (savedWorkOrderLabor != null)
                    //{
                    //    try
                    //    {
                    //        //set in buttons commands

                    //        startButton.CommandParameter = savedWorkOrderLabor;
                    //        stopButton.CommandParameter = savedWorkOrderLabor;

                    //        string k3 = "WorkOrderLaborHours1:" + item.HoursAtRate1;
                    //        savedWorkOrderLabor1 = JsonConvert.DeserializeObject<WorkOrderLabor>(WorkOrderLaborStorge.Storage.Get(k3));
                    //        //startButtonforRate2.CommandParameter = savedWorkOrderLabor;
                    //        //stopButtonforRate2.CommandParameter = savedWorkOrderLabor;


                    //        startButton.BackgroundColor = Color.Green;
                    //        //startButtonforRate2.BackgroundColor = Color.Green;

                    //        string FinalHours = Convert.ToDecimal(string.Format("{0:F2}", savedWorkOrderLabor1.HoursAtRate1)).ToString();
                    //        var FinalHrs1 = FinalHours.Split('.');
                    //        hoursEntry.Text = FinalHrs1[0];
                    //        minuteEntry.Text = FinalHrs1[1];

                    //        //string FinalHours2 = Convert.ToDecimal(string.Format("{0:F2}", savedWorkOrderLabor.HoursAtRate2)).ToString();
                    //        //var FinalHrs2 = FinalHours2.Split('.');
                    //        //hoursEntryforRate2.Text = FinalHrs2[0];
                    //        //minuteEntryforRate2.Text = FinalHrs2[1];

                    //        completeDateButton.Text = item.CompletionDate != null ? DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString() : "";





                    //    }
                    //    catch (Exception ex)
                    //    {

                    //    }

                    //}

                    //if (savedWorkOrderLabor2 != null)
                    //{
                    //    try
                    //    {
                    //        //set in buttons commands


                    //        startButtonforRate2.CommandParameter = savedWorkOrderLabor2;
                    //        stopButtonforRate2.CommandParameter = savedWorkOrderLabor2;



                    //        startButtonforRate2.BackgroundColor = Color.Green;


                    //        string FinalHours2 = Convert.ToDecimal(string.Format("{0:F2}", savedWorkOrderLabor2.HoursAtRate2)).ToString();
                    //        var FinalHrs2 = FinalHours2.Split('.');
                    //        hoursEntryforRate2.Text = FinalHrs2[0];
                    //        minuteEntryforRate2.Text = FinalHrs2[1];

                    //        completeDateButton.Text = item.CompletionDate != null ? DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString() : "";





                    //    }
                    //    catch (Exception ex)
                    //    {

                    //    }

                    //}
                    //#endregion

                    //if (Application.Current.Properties.ContainsKey("CreateTask"))
                    //{
                    //    CreateTask = Application.Current.Properties["CreateTask"].ToString();


                    //}
                    //if (Application.Current.Properties.ContainsKey("EmployeeTab"))
                    //{
                    //    var EmployeeTab = Application.Current.Properties["EmployeeTab"].ToString();
                    //    if (EmployeeTab != null && EmployeeTab == "E")
                    //    {
                    //        employeeNameGrid.IsVisible = true;
                    //    }
                    //    else if (EmployeeTab == "V")
                    //    {
                    //        employeeNameGrid.IsEnabled = false;
                    //    }
                    //    else
                    //    {
                    //        employeeNameGrid.IsVisible = false;
                    //    }


                    //}
                    //if (Application.Current.Properties.ContainsKey("ContractorTab"))
                    //{
                    //    var ContractorTab = Application.Current.Properties["ContractorTab"].ToString();
                    //    if (ContractorTab != null && ContractorTab == "E")
                    //    {
                    //        employeeNameGrid.IsVisible = true;
                    //    }
                    //    else if (ContractorTab == "V")
                    //    {
                    //        employeeNameGrid.IsEnabled = false;
                    //    }
                    //    else
                    //    {
                    //        employeeNameGrid.IsVisible = false;
                    //    }


                    //}
                    //if (Application.Current.Properties.ContainsKey("CompletionDateTab"))
                    //{
                    //    var CompletionDateTab = Application.Current.Properties["CompletionDateTab"].ToString();
                    //    if (CompletionDateTab != null && CompletionDateTab == "E")
                    //    {
                    //        completeDateButton.IsVisible = true;
                    //        completionDateLabel.IsVisible = true;
                    //    }
                    //    else if (CompletionDateTab == "V")
                    //    {
                    //        completeDateButton.IsEnabled = false;
                    //        completionDateLabel.IsEnabled = false;
                    //    }
                    //    else
                    //    {
                    //        completeDateButton.IsVisible = false;
                    //        completionDateLabel.IsVisible = false;
                    //    }


                    //}
                    //if (Application.Current.Properties.ContainsKey("StartdateTab"))
                    //{
                    //    var StartdateTab = Application.Current.Properties["StartdateTab"].ToString();
                    //    if (StartdateTab != null && StartdateTab == "E")
                    //    {
                    //        startDateStacklayout.IsVisible = true;
                    //    }
                    //    else if (StartdateTab == "V")
                    //    {
                    //        startDateStacklayout.IsEnabled = false;

                    //    }
                    //    else
                    //    {
                    //        startDateStacklayout.IsVisible = false;
                    //    }


                    //}




                    //var taskNumberGrid = new Grid();
                    //taskNumberGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.5, GridUnitType.Star) });
                    //taskNumberGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1.5, GridUnitType.Star) });
                    //taskNumberGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });



                    layout1 = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        Spacing = 10,
                         HorizontalOptions = LayoutOptions.Fill,
                        Children =
                                {
                                 new Label{ Text=taskNumberLabel.Text,FontAttributes=FontAttributes.Bold},startStopButtonGrid
                                }
                    };
                    ParentLayout.Children.Add(layout1);
                }



                var layout12 = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = 5,
                    HorizontalOptions = LayoutOptions.Start,
                    Children =
                                {
                                  btnAddInspection,btnAddEmployee,btnAddContractor,btnCreateWorkorder,
                                }
                };


                var oneBox = new BoxView { BackgroundColor = Color.Black, HeightRequest = 2, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
                var FinalLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Spacing = 10,
                    Children =
                                {
                                   new Label{Text="Inspection Timer " +":"+ "hh:mm:ss" ,TextColor=Color.Black},layout12, oneBox

                                }
                };
                ParentLayout.Children.Add(FinalLayout);



                // }
                if (this.btnCreateWorkorder.IsVisible)
                {
                    CC = await ViewModel._inspectionService.GetFailedWorkorderInspection(this.WorkorderID.ToString(), "1");
                }
                else
                {
                    CC = await ViewModel._inspectionService.GetFailedWorkorderInspection(this.WorkorderID.ToString(), "0");
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine(ex.Message + "<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>" + ex.StackTrace);
            }

        }







        private async Task RetriveAllWorkorderInspectionsAsync()
        {
           
            CC = await ViewModel._inspectionService.GetWorkorderInspection(this.WorkorderID.ToString());
            if (CC.listInspection == null || CC.listInspection.Count == 0)
            {
                //this.InspectionTimerLayout.IsVisible = false;
                //DisabledText.Text = WebControlTitle.GetTargetNameByTitleName("ThisTabisDisabled");
                //DisabledText.IsVisible = true;
                //return;
            }

            BindLayout(CC.listInspection);
        }

        private void Timer_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;

            if (btn.Text == WebControlTitle.GetTargetNameByTitleName("StartTimer"))
            {
                btn.Text = WebControlTitle.GetTargetNameByTitleName("StopTimer");
                var context = btn.BindingContext as InspectionTimer;

                context.InspectionStartTime = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone); //DateTime.Now;

                btn.BackgroundColor = Color.Red;
                context.IsTimerRunning = true;

                #region Global Time Logic

                try
                {
                    //Save in Local
                    string key = "WorkOrderInspection:" + context.WorkorderID;
                    WorkorderInspectionStorge.Storage.Set(key, JsonConvert.SerializeObject(context));
                }
                catch (Exception ex)
                {


                }

                #endregion

            }

            else
            {
                btn.Text = WebControlTitle.GetTargetNameByTitleName("StartTimer");
                var context = btn.BindingContext as InspectionTimer;
                context.InspectionStopTime = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone); //DateTime.Now;

                var difference = context.InspectionStopTime - context.InspectionStartTime;
                context.TotalRunningTime = context.TotalRunningTime + difference.Value;
                // this.TimerText.Text = TimeSpan.FromSeconds(Convert.ToDouble((int)context.TotalRunningTime.TotalSeconds)).ToString();   //String.Format("Hours: {0} Minutes: {1} Seconds: {2}", context.TotalRunningTime.Hours, context.TotalRunningTime.Minutes, context.TotalRunningTime.Seconds);
                btn.BackgroundColor = Color.Green;
                context.IsTimerRunning = false;



                #region Assign Start and completion date property according to timer stopped
                InspectionStartDate = context.InspectionStartTime;
                InspectionCompletionDate = context.InspectionStopTime;
                #endregion


                #region Test region on fill the start date and end date
                // Fill the start date if inspection start date is null and fill the end date
                if (inspectionTime.InspectionStartDate == null)
                {
                    if (Device.Idiom == TargetIdiom.Phone)
                    {
                        //this.PickerInspectionStartDatePhone.Text = context.InspectionStartTime.GetValueOrDefault().Date.ToShortDateString();
                    }
                    else
                    {
                        // this.PickerInspectionStartDateTablet.Text = context.InspectionStartTime.GetValueOrDefault().Date.ToShortDateString();
                    }

                    this.InspectionStartDate = context.InspectionStartTime.GetValueOrDefault().Date;
                    if (Device.Idiom == TargetIdiom.Phone)
                    {
                        //this.PickerInspectionCompletionDatePhone.Text = context.InspectionStopTime.GetValueOrDefault().Date.ToShortDateString();
                    }
                    else
                    {
                        //this.PickerInspectionCompletionDateTablet.Text = context.InspectionStopTime.GetValueOrDefault().Date.ToShortDateString();
                    }


                    this.InspectionCompletionDate = context.InspectionStopTime.GetValueOrDefault().Date;
                }

                else
                {
                    if (Device.Idiom == TargetIdiom.Phone)
                    {
                        //this.PickerInspectionStartDatePhone.Text = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(inspectionTime.InspectionStartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("d");
                    }
                    else
                    {
                        //this.PickerInspectionStartDateTablet.Text = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(inspectionTime.InspectionStartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("d");
                    }
                    this.InspectionStartDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(inspectionTime.InspectionStartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).Date;
                    if (Device.Idiom == TargetIdiom.Phone)
                    {
                        //this.PickerInspectionCompletionDatePhone.Text = context.InspectionStopTime.GetValueOrDefault().Date.ToShortDateString();
                    }
                    else
                    {
                        // this.PickerInspectionCompletionDateTablet.Text = context.InspectionStopTime.GetValueOrDefault().Date.ToShortDateString();
                    }
                    this.InspectionCompletionDate = context.InspectionStopTime.GetValueOrDefault().Date;
                }








                #endregion


                try
                {
                    //Save in Local
                    string key = "WorkOrderInspection:" + context.WorkorderID;
                    WorkorderInspectionStorge.Storage.Set(key, JsonConvert.SerializeObject(context));
                }
                catch
                {

                }
            }
        }

        async void HoursTextChanged(object sender, EventArgs e)
        {
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
                //await DisplayAlert(WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("WronginputinMinutes."), WebControlTitle.GetTargetNameByTitleName("OK"));
                UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("WronginputinMinutes"), TimeSpan.FromSeconds(2));

                e1.Text = "";
                return;
            }
            if (minuteValue > 59)
            {
                //await DisplayAlert(WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("Minutesshouldbelessthen59."), WebControlTitle.GetTargetNameByTitleName("OK"));

                //await DisplayAlert(flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Minutesshouldbelessthen59").TargetName, flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("Minutesshouldbelessthen59"), TimeSpan.FromSeconds(2));

                e1.Text = "";
                return;
            }

        }
        private void BindLayout(List<ExistingInspections> listInspection)
        {
            // List<ExistingInspections> listInspection
            List<ExistingInspections> distinctList = listInspection.OrderByDescending(x => x.SectionID == 0).ToList();
            foreach (var item in distinctList)
            {
                if (item.SectionID == 0 || item.SectionID == null)
                {
                    var layout2 = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        Children = { }

                    };
                    //var oneBox = new BoxView { BackgroundColor = Color.Black, HeightRequest = 3, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };

                    var mainLayoutGroupSingle = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        Children = { layout2 }

                    };
                    //mainLayoutGroup.Children.Add(oneBox);


                    //var frame = new Frame
                    //{
                    //    Padding = new Thickness(10, 10, 10, 10),
                    //    OutlineColor = Color.Accent,
                    //    HorizontalOptions = LayoutOptions.FillAndExpand,
                    //    Content = mainLayoutGroup,
                    //};


                    View Question;
                    View Label;
                    //View LabelHours;
                    View Layout;

                    Grid sta;
                    switch (item.ResponseTypeName)
                    {
                        case "Pass/Fail":


                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, };
                            if (Device.Idiom == TargetIdiom.Phone)
                            {
                                Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(12, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            }
                            else
                            {
                                Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            }

                            //LabelHours = new Label { Text = item.EstimatedHours.ToString(), Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };

                            GenerateAnswerText(item);

                            var gridPF = new Grid() { HorizontalOptions = LayoutOptions.End };
                            gridPF.RowDefinitions.Add(new RowDefinition { Height = new GridLength(80) });
                            gridPF.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            gridPF.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                            if (Device.Idiom == TargetIdiom.Phone)
                            {
                                var btnTruePF = new Button() { HeightRequest = 36, FontSize = 10, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "Pass", BackgroundColor = Color.Gray };
                                var btnFalsePF = new Button() { HeightRequest = 36, FontSize = 10, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "Fail", BackgroundColor = Color.Gray };
                                btnTruePF.Clicked += BtnTrue_Clicked;
                                btnFalsePF.Clicked += BtnFalse_Clicked;

                                gridPF.Children.Add(btnTruePF, 0, 0);
                                gridPF.Children.Add(btnFalsePF, 1, 0);
                                Layout = gridPF;
                                switch (item.AnswerDescription)
                                {
                                    case "":
                                        break;
                                    case "Pass":
                                        BtnTrue_Clicked(btnTruePF, null);
                                        break;
                                    case "Fail":
                                        this.btnCreateWorkorder.IsVisible = true;
                                        BtnFalse_Clicked(btnFalsePF, null);
                                        break;

                                }
                            }
                            else
                            {
                                var btnTruePF = new Button() { VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "Pass", BackgroundColor = Color.Gray };
                                var btnFalsePF = new Button() { VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "Fail", BackgroundColor = Color.Gray };
                                btnTruePF.Clicked += BtnTrue_Clicked;
                                btnFalsePF.Clicked += BtnFalse_Clicked;

                                gridPF.Children.Add(btnTruePF, 0, 0);
                                gridPF.Children.Add(btnFalsePF, 1, 0);
                                Layout = gridPF;
                                switch (item.AnswerDescription)
                                {
                                    case "":
                                        break;
                                    case "Pass":
                                        BtnTrue_Clicked(btnTruePF, null);
                                        break;
                                    case "Fail":
                                        this.btnCreateWorkorder.IsVisible = true;

                                        BtnFalse_Clicked(btnFalsePF, null);
                                        break;

                                }
                            }





                            sta = new Grid() { HorizontalOptions = LayoutOptions.FillAndExpand, BindingContext = item };
                            sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                            sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                            sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            sta.Children.Add(Question, 0, 0);
                            sta.Children.Add(Label, 0, 0);

                            if (Device.Idiom == TargetIdiom.Phone)
                            {
                                sta.Children.Add(Layout, 0, 1);
                            }
                            else
                            {
                                sta.Children.Add(Layout, 1, 0);
                            }
                            layout2.Children.Add(sta);

                            break;

                        case "Standard Range":
                            //LabelHours = new Label { Text = item.EstimatedHours.ToString(), Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };
                            if (Device.Idiom == TargetIdiom.Phone)
                            {
                                Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(12, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            }
                            else
                            {
                                Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            }

                            Layout = new Entry() { Keyboard = Keyboard.Numeric, WidthRequest = 65, HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Start, };

                            GenerateAnswerText(item);

                            (Layout as Entry).BindingContext = new Range() { MaxRange = item.MaxRange, MinRange = item.MinRange };
                            (Layout as Entry).TextChanged += StandardRange_TextChanged;
                            (Layout as Entry).Text = item.AnswerDescription;

                            sta = new Grid() { HorizontalOptions = LayoutOptions.FillAndExpand, BindingContext = item };
                            sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(80) });
                            sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                            sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            sta.Children.Add(Question, 0, 0);
                            sta.Children.Add(Label, 0, 0);
                            sta.Children.Add(Layout, 1, 0);
                            // sta.Children.Add(LabelHours, 2, 0);
                            layout2.Children.Add(sta);


                            break;

                        case "Yes/No/N/A":
                            //LabelHours = new Label { Text = item.EstimatedHours.ToString(), Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };
                            if (Device.Idiom == TargetIdiom.Phone)
                            {
                                Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(12, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            }
                            else
                            {
                                Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            }



                            GenerateAnswerText(item);

                            var grid = new Grid() { HorizontalOptions = LayoutOptions.End };
                            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(80) });
                            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                            if (Device.Idiom == TargetIdiom.Phone)
                            {
                                var btnTrue = new Button() { HeightRequest = 36, FontSize = 10, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "Yes", BackgroundColor = Color.Gray };
                                var btnFalse = new Button() { HeightRequest = 36, FontSize = 10, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "No", BackgroundColor = Color.Gray };
                                var btnNA = new Button() { HeightRequest = 36, FontSize = 10, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "NA", BackgroundColor = Color.Gray };
                                //Bind the ResponseSubType in Buttons
                                btnTrue.BindingContext = item.ResponseSubType;
                                btnFalse.BindingContext = item.ResponseSubType;
                                btnNA.BindingContext = item.ResponseSubType;


                                //TODO: Add the NA button over here
                                btnTrue.Clicked += BtnYes_Clicked; //Create New handler for YES/NO otherwise it will affact the functionality of Pass/Fail
                                btnFalse.Clicked += BtnNo_Clicked;
                                btnNA.Clicked += BtnNA_Clicked;

                                grid.Children.Add(btnTrue, 0, 0);
                                grid.Children.Add(btnFalse, 1, 0);
                                grid.Children.Add(btnNA, 2, 0);
                                //TODO: Add the NA button in grid



                                //TODO: Set the retriving answer accordingly
                                switch (item.AnswerDescription)
                                {
                                    case "":
                                        break;
                                    case "NA":
                                        BtnNA_Clicked(btnNA, null);
                                        break;
                                    case "Yes":
                                        BtnYes_Clicked(btnTrue, null);
                                        break;
                                    case "No":
                                        BtnNo_Clicked(btnFalse, null);
                                        break;

                                }

                                #region Negative response create workorder btnshow
                                if (item.ResponseSubType == null || item.ResponseSubType == false)
                                {
                                    switch (item.AnswerDescription)
                                    {
                                        case "":
                                            break;
                                        case "NA":
                                            break;
                                        case "Yes":
                                            btnCreateWorkorder.IsVisible = true;
                                            break;
                                        case "No":
                                            break;

                                    }
                                }
                                else
                                {
                                    switch (item.AnswerDescription)
                                    {
                                        case "":
                                            break;
                                        case "NA":
                                            break;
                                        case "Yes":
                                            break;
                                        case "No":
                                            btnCreateWorkorder.IsVisible = true;
                                            break;

                                    }
                                }
                                #endregion

                            }
                            else
                            {
                                var btnTrue = new Button() { VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "Yes", BackgroundColor = Color.Gray };
                                var btnFalse = new Button() { VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "No", BackgroundColor = Color.Gray };
                                var btnNA = new Button() { VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "NA", BackgroundColor = Color.Gray };
                                //Bind the ResponseSubType in Buttons
                                btnTrue.BindingContext = item.ResponseSubType;
                                btnFalse.BindingContext = item.ResponseSubType;
                                btnNA.BindingContext = item.ResponseSubType;


                                //TODO: Add the NA button over here
                                btnTrue.Clicked += BtnYes_Clicked; //Create New handler for YES/NO otherwise it will affact the functionality of Pass/Fail
                                btnFalse.Clicked += BtnNo_Clicked;
                                btnNA.Clicked += BtnNA_Clicked;

                                grid.Children.Add(btnTrue, 0, 0);
                                grid.Children.Add(btnFalse, 1, 0);
                                grid.Children.Add(btnNA, 2, 0);
                                //TODO: Add the NA button in grid



                                //TODO: Set the retriving answer accordingly
                                switch (item.AnswerDescription)
                                {
                                    case "":
                                        break;
                                    case "NA":
                                        BtnNA_Clicked(btnNA, null);
                                        break;
                                    case "Yes":
                                        BtnYes_Clicked(btnTrue, null);
                                        break;
                                    case "No":
                                        BtnNo_Clicked(btnFalse, null);
                                        break;

                                }

                                #region Negative response create workorder btnshow
                                if (item.ResponseSubType == null || item.ResponseSubType == false)
                                {
                                    switch (item.AnswerDescription)
                                    {
                                        case "":
                                            break;
                                        case "NA":
                                            break;
                                        case "Yes":
                                             btnCreateWorkorder.IsVisible = true;
                                            break;
                                        case "No":
                                            break;

                                    }
                                }
                                else
                                {
                                    switch (item.AnswerDescription)
                                    {
                                        case "":
                                            break;
                                        case "NA":
                                            break;
                                        case "Yes":
                                            break;
                                        case "No":
                                             btnCreateWorkorder.IsVisible = true;
                                            break;

                                    }
                                }
                                #endregion
                            }
                            Layout = grid;
                            sta = new Grid() { HorizontalOptions = LayoutOptions.FillAndExpand, BindingContext = item };
                            sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                            sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                            sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            sta.Children.Add(Question, 0, 0);
                            sta.Children.Add(Label, 0, 0);
                            if (Device.Idiom == TargetIdiom.Phone)
                            {
                                sta.Children.Add(Layout, 0, 1);
                            }
                            else
                            {
                                sta.Children.Add(Layout, 1, 0);
                            }
                            layout2.Children.Add(sta);

                            break;

                        case "Count":
                            //LabelHours = new Label { Text = item.EstimatedHours.ToString(), Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                            if (Device.Idiom == TargetIdiom.Phone)
                            {
                                Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(12, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            }
                            else
                            {
                                Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            }
                            GenerateAnswerText(item);

                            Layout = new Entry() { Keyboard = Keyboard.Numeric, WidthRequest = 65, HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Start };
                            (Layout as Entry).Text = item.AnswerDescription;
                            (Layout as Entry).TextChanged += OnlyNumeric_TextChanged;

                            sta = new Grid() { HorizontalOptions = LayoutOptions.FillAndExpand, BindingContext = item };
                            sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(80) });
                            sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                            sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            sta.Children.Add(Question, 0, 0);
                            sta.Children.Add(Label, 0, 0);
                            sta.Children.Add(Layout, 1, 0);
                            //sta.Children.Add(LabelHours, 2, 0);
                            layout2.Children.Add(sta);


                            break;
                        case "Text":

                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                            if (Device.Idiom == TargetIdiom.Phone)
                            {
                                Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(12, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            }
                            else
                            {
                                Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            }
                            GenerateAnswerText(item);

                            Layout = new CustomEditor() { HorizontalOptions = LayoutOptions.FillAndExpand, HeightRequest = 60 };
                            (Layout as CustomEditor).Text = item.AnswerDescription;

                            sta = new Grid() { HorizontalOptions = LayoutOptions.FillAndExpand, BindingContext = item };
                            sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(80) });
                            sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                            sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            sta.Children.Add(Question, 0, 0);
                            sta.Children.Add(Label, 0, 0);
                            sta.Children.Add(Layout, 0, 1);
                            //sta.Children.Add(LabelHours, 2, 0);
                            layout2.Children.Add(sta);


                            break;

                        case "Multiple Choice":
                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                            if (Device.Idiom == TargetIdiom.Phone)
                            {
                                Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(12, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            }
                            else
                            {
                                Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            }
                            GenerateAnswerText(item);


                            Layout = new Picker() { WidthRequest = 65, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End };

                            if (!string.IsNullOrWhiteSpace(item.Option1))
                                (Layout as Picker).Items.Add(item.Option1);
                            if (!string.IsNullOrWhiteSpace(item.Option2))
                                (Layout as Picker).Items.Add(item.Option2);
                            if (!string.IsNullOrWhiteSpace(item.Option3))
                                (Layout as Picker).Items.Add(item.Option3);
                            if (!string.IsNullOrWhiteSpace(item.Option4))
                                (Layout as Picker).Items.Add(item.Option4);
                            if (!string.IsNullOrWhiteSpace(item.Option5))
                                (Layout as Picker).Items.Add(item.Option5);
                            if (!string.IsNullOrWhiteSpace(item.Option6))
                                (Layout as Picker).Items.Add(item.Option6);
                            if (!string.IsNullOrWhiteSpace(item.Option7))
                                (Layout as Picker).Items.Add(item.Option7);
                            if (!string.IsNullOrWhiteSpace(item.Option8))
                                (Layout as Picker).Items.Add(item.Option8);
                            if (!string.IsNullOrWhiteSpace(item.Option9))
                                (Layout as Picker).Items.Add(item.Option9);
                            if (!string.IsNullOrWhiteSpace(item.Option10))
                                (Layout as Picker).Items.Add(item.Option10);

                            var index = (Layout as Picker).Items.IndexOf(item.AnswerDescription);
                            (Layout as Picker).SelectedIndex = index;

                            sta = new Grid() { HorizontalOptions = LayoutOptions.FillAndExpand, BindingContext = item };
                            sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(80) });
                            sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                            sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            sta.Children.Add(Question, 0, 0);
                            sta.Children.Add(Label, 0, 0);
                            sta.Children.Add(Layout, 1, 0);
                            //sta.Children.Add(LabelHours, 2, 0);
                            layout2.Children.Add(sta);


                            break;

                        case "None":

                            Question = new Label { Text = "", Font = Font.SystemFontOfSize(16, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                            if (Device.Idiom == TargetIdiom.Phone)
                            {
                                Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(12, FontAttributes.Bold), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            }
                            else
                            {
                                Label = new Label { Text = item.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.Bold), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                            }
                            GenerateAnswerText(item);


                            // Layout = new Entry() { HorizontalOptions = LayoutOptions.FillAndExpand };
                            //(Layout as Entry).Text = item.AnswerDescription;
                            sta = new Grid() { HorizontalOptions = LayoutOptions.FillAndExpand, BindingContext = item };
                            sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(80) });
                            sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                            sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                            sta.Children.Add(Question, 0, 0);
                            sta.Children.Add(Label, 0, 0);
                            //sta.Children.Add(Layout, 1, 0);
                            layout2.Children.Add(sta);


                            break;
                    }

                    var btnsave = new Button() { Text = WebControlTitle.GetTargetNameByTitleName("Save"), HorizontalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Color.FromHex("#87CEFA"), TextColor = Color.White, BorderColor = Color.Black };
                    btnsave.Clicked += Btnsave_Clicked;



                    #region Add Signature image
                    if (item.SignatureRequired.GetValueOrDefault())
                    {
                        //var imageView = new CustomImage() { ImageBase64String = ImageBase64 };
                        var imageView = new CustomImage() { ImageBase64String = item.SignatureString, HeightRequest = 150 };
                        byte[] byteImg = Convert.FromBase64String(item.SignatureString);

                        // byte[] byteImageCompr = DependencyService.Get<IResizeImage>().ResizeImageAndroid(byteImg, 350, 350);
                        imageView.Source = ImageSource.FromStream(() => new MemoryStream(byteImg));
                        layout2.Children.Add(imageView);

                        var addSignatureButton = new Button() { Text = WebControlTitle.GetTargetNameByTitleName("AddSignature"), BackgroundColor = Color.FromHex("#87CEFA"), TextColor = Color.White, BorderColor = Color.Black };
                        addSignatureButton.Clicked += AddSignatureButton_Clicked;
                        layout2.Children.Add(addSignatureButton);

                        var s = item.SignaturePath;
                    }
                    #endregion

                    layout2.Children.Add(btnsave);

                    #region Estimated Hour Region
                    if (Device.Idiom == TargetIdiom.Phone)
                    {
                        var estimatedHourStackLayout = new StackLayout() { Orientation = StackOrientation.Horizontal };
                        var estimatedHourTitleLabel = new Label() { FontSize = 12, TextColor = Color.Black, Text = WebControlTitle.GetTargetNameByTitleName("EstimatedHours") };
                        var estimatedHourLabel = new Label() { FontSize = 12, TextColor = Color.Black };
                        estimatedHourLabel.Text = item.EstimatedHours.ToString();
                        estimatedHourStackLayout.Children.Add(estimatedHourTitleLabel);
                        estimatedHourStackLayout.Children.Add(estimatedHourLabel);
                        mainLayoutGroupSingle.Children.Add(estimatedHourStackLayout);
                    }
                    else
                    {
                        var estimatedHourStackLayout = new StackLayout() { Orientation = StackOrientation.Horizontal };
                        var estimatedHourTitleLabel = new Label() { TextColor = Color.Black, Text = WebControlTitle.GetTargetNameByTitleName("EstimatedHours") };
                        var estimatedHourLabel = new Label() { TextColor = Color.Black };
                        estimatedHourLabel.Text = item.EstimatedHours.ToString();
                        estimatedHourStackLayout.Children.Add(estimatedHourTitleLabel);
                        estimatedHourStackLayout.Children.Add(estimatedHourLabel);
                        mainLayoutGroupSingle.Children.Add(estimatedHourStackLayout);
                    }



                    #endregion
                    var oneBox = new BoxView { BackgroundColor = Color.Black, HeightRequest = 2, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
                    mainLayoutGroupSingle.Children.Add(oneBox);

                    MainLayout.Children.Add(mainLayoutGroupSingle);

                    this.AnswerText.Append(System.Environment.NewLine);


                }

                else
                {
                    List<ExistingInspections> commonSections = new List<ExistingInspections>();

                    foreach (var item1 in listInspection)
                    {
                        if (item1.IsAdded == false)
                        {
                            if (item.SectionID == item1.SectionID)
                            {
                                commonSections.Add(item1);
                                item1.IsAdded = true;
                            }
                        }
                    }


                    if (commonSections.Count == 0)
                    {
                        continue;
                    }

                    var layout1 = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        Children = { }

                    };
                    //var oneBox = new BoxView { BackgroundColor = Color.Black, HeightRequest = 3, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };

                    var mainLayoutGroup = new StackLayout
                    {
                        Orientation = StackOrientation.Vertical,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        Children = { layout1 }

                    };
                    //mainLayoutGroup.Children.Add(oneBox);

                    //var frame = new Frame
                    //{
                    //    Padding = new Thickness(10, 10, 10, 10),
                    //    OutlineColor = Color.Accent,
                    //    HorizontalOptions = LayoutOptions.FillAndExpand,
                    //    Content = mainLayoutGroup,
                    //};


                    string sectionName = "" + commonSections[0].SectionName;
                    layout1.Children.Add(new Label() { Text = sectionName, Font = Font.SystemFontOfSize(20, FontAttributes.Bold), HorizontalTextAlignment = TextAlignment.Center });

                    this.AnswerText.Append("Group Name: ");
                    this.AnswerText.Append(sectionName);
                    this.AnswerText.Append(System.Environment.NewLine);



                    foreach (var item1 in commonSections)
                    {
                        View Question;
                        View Label1;
                        View Layout;

                        //StackLayout sta;
                        Grid sta;

                        switch (item1.ResponseTypeName)
                        {
                            case "Pass/Fail":

                                Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };


                                if (Device.Idiom == TargetIdiom.Phone)
                                {
                                    Label1 = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(12, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                else
                                {
                                    Label1 = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                GenerateAnswerText(item1);

                                var gridPF = new Grid() { HorizontalOptions = LayoutOptions.End };
                                gridPF.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                                gridPF.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                gridPF.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                if (Device.Idiom == TargetIdiom.Phone)
                                {
                                    var btnTruePF = new Button() { HeightRequest = 36, FontSize = 10, HorizontalOptions = LayoutOptions.End, Text = "Pass", BackgroundColor = Color.Gray };
                                    var btnFalsePF = new Button() { HeightRequest = 36, FontSize = 10, HorizontalOptions = LayoutOptions.End, Text = "Fail", BackgroundColor = Color.Gray };
                                    btnTruePF.Clicked += BtnTrue_Clicked;
                                    btnFalsePF.Clicked += BtnFalse_Clicked;

                                    gridPF.Children.Add(btnTruePF, 0, 0);
                                    gridPF.Children.Add(btnFalsePF, 1, 0);


                                    switch (item1.AnswerDescription)
                                    {
                                        case "":
                                            break;
                                        case "Pass":
                                            BtnTrue_Clicked(btnTruePF, null);
                                            break;
                                        case "Fail":
                                            this.btnCreateWorkorder.IsVisible = true;
                                            BtnFalse_Clicked(btnFalsePF, null);
                                            break;

                                    }
                                }
                                else
                                {
                                    var btnTruePF = new Button() { HorizontalOptions = LayoutOptions.End, Text = "Pass", BackgroundColor = Color.Gray };
                                    var btnFalsePF = new Button() { HorizontalOptions = LayoutOptions.End, Text = "Fail", BackgroundColor = Color.Gray };
                                    btnTruePF.Clicked += BtnTrue_Clicked;
                                    btnFalsePF.Clicked += BtnFalse_Clicked;

                                    gridPF.Children.Add(btnTruePF, 0, 0);
                                    gridPF.Children.Add(btnFalsePF, 1, 0);


                                    switch (item1.AnswerDescription)
                                    {
                                        case "":
                                            break;
                                        case "Pass":
                                            BtnTrue_Clicked(btnTruePF, null);
                                            break;
                                        case "Fail":
                                            this.btnCreateWorkorder.IsVisible = true;
                                            BtnFalse_Clicked(btnFalsePF, null);
                                            break;

                                    }
                                }
                                Layout = gridPF;
                                sta = new Grid() { };
                                sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                sta.Children.Add(Question, 0, 0);
                                sta.Children.Add(Label1, 0, 0);
                                if (Device.Idiom == TargetIdiom.Phone)
                                {
                                    sta.Children.Add(Layout, 0, 1);
                                }
                                else
                                {
                                    sta.Children.Add(Layout, 1, 0);
                                }
                                //sta.Children.Add(new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, Children = { new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand, Margin = new Thickness(0, 0, 0, 20) } } }, 0, 0);
                                layout1.Children.Add(sta);

                                break;

                            case "Standard Range":
                                Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                                if (Device.Idiom == TargetIdiom.Phone)
                                {
                                    Label1 = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(12, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                else
                                {
                                    Label1 = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                GenerateAnswerText(item1);

                                Layout = new Entry() { Keyboard = Keyboard.Numeric, WidthRequest = 65, HorizontalOptions = LayoutOptions.End };

                                (Layout as Entry).BindingContext = new Range() { MaxRange = item1.MaxRange, MinRange = item1.MinRange };
                                (Layout as Entry).TextChanged += StandardRange_TextChanged;
                                (Layout as Entry).Text = item1.AnswerDescription;


                                sta = new Grid() { };
                                sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                sta.Children.Add(Question, 0, 0);
                                sta.Children.Add(Label1, 0, 0);
                                sta.Children.Add(Layout, 1, 0);
                                // sta.Children.Add(new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, Children = { new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand, Margin = new Thickness(0, 0, 0, 20) } } }, 0, 0);
                                layout1.Children.Add(sta);

                                break;

                            case "Yes/No/N/A":

                                Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                                if (Device.Idiom == TargetIdiom.Phone)
                                {
                                    Label1 = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(12, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                else
                                {
                                    Label1 = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                GenerateAnswerText(item1);


                                var grid = new Grid() { HorizontalOptions = LayoutOptions.End };
                                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                if (Device.Idiom == TargetIdiom.Phone)
                                {
                                    var btnTrue = new Button() { HeightRequest = 36, FontSize = 10, HorizontalOptions = LayoutOptions.End, Text = "Yes", BackgroundColor = Color.Gray };
                                    var btnFalse = new Button() { HeightRequest = 36, FontSize = 10, HorizontalOptions = LayoutOptions.End, Text = "No", BackgroundColor = Color.Gray };
                                    var btnNA = new Button() { HeightRequest = 36, FontSize = 10, HorizontalOptions = LayoutOptions.End, Text = "NA", BackgroundColor = Color.Gray };
                                    //Bind the ResponseSubType in Buttons
                                    btnTrue.BindingContext = item1.ResponseSubType;
                                    btnFalse.BindingContext = item1.ResponseSubType;
                                    btnNA.BindingContext = item1.ResponseSubType;

                                    //TODO: Add the NA button over here
                                    btnTrue.Clicked += BtnYes_Clicked; //Create New handler for YES/NO otherwise it will affact the functionality of Pass/Fail
                                    btnFalse.Clicked += BtnNo_Clicked;
                                    btnNA.Clicked += BtnNA_Clicked;

                                    grid.Children.Add(btnTrue, 0, 0);
                                    grid.Children.Add(btnFalse, 1, 0);
                                    grid.Children.Add(btnNA, 2, 0);
                                    //TODO: Add the NA button in grid



                                    switch (item1.AnswerDescription)
                                    {
                                        case "":
                                            break;
                                        case "NA":
                                            BtnNA_Clicked(btnNA, null);
                                            break;
                                        case "Yes":
                                            BtnYes_Clicked(btnTrue, null);
                                            break;
                                        case "No":
                                            this.btnCreateWorkorder.IsVisible = true;
                                            BtnNo_Clicked(btnFalse, null);
                                            break;

                                    }


                                    #region Negative response create workorder btnshow
                                    if (item1.ResponseSubType == null || item1.ResponseSubType == false)
                                    {
                                        switch (item1.AnswerDescription)
                                        {
                                            case "":
                                                break;
                                            case "NA":
                                                break;
                                            case "Yes":
                                                btnCreateWorkorder.IsVisible = true;
                                                break;
                                            case "No":
                                                break;

                                        }
                                    }
                                    else
                                    {
                                        switch (item1.AnswerDescription)
                                        {
                                            case "":
                                                break;
                                            case "NA":
                                                break;
                                            case "Yes":
                                                break;
                                            case "No":
                                                 btnCreateWorkorder.IsVisible = true;
                                                break;

                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    var btnTrue = new Button() { HorizontalOptions = LayoutOptions.End, Text = "Yes", BackgroundColor = Color.Gray };
                                    var btnFalse = new Button() { HorizontalOptions = LayoutOptions.End, Text = "No", BackgroundColor = Color.Gray };
                                    var btnNA = new Button() { HorizontalOptions = LayoutOptions.End, Text = "NA", BackgroundColor = Color.Gray };
                                    //Bind the ResponseSubType in Buttons
                                    btnTrue.BindingContext = item1.ResponseSubType;
                                    btnFalse.BindingContext = item1.ResponseSubType;
                                    btnNA.BindingContext = item1.ResponseSubType;

                                    //TODO: Add the NA button over here
                                    btnTrue.Clicked += BtnYes_Clicked; //Create New handler for YES/NO otherwise it will affact the functionality of Pass/Fail
                                    btnFalse.Clicked += BtnNo_Clicked;
                                    btnNA.Clicked += BtnNA_Clicked;

                                    grid.Children.Add(btnTrue, 0, 0);
                                    grid.Children.Add(btnFalse, 1, 0);
                                    grid.Children.Add(btnNA, 2, 0);
                                    //TODO: Add the NA button in grid



                                    switch (item1.AnswerDescription)
                                    {
                                        case "":
                                            break;
                                        case "NA":
                                            BtnNA_Clicked(btnNA, null);
                                            break;
                                        case "Yes":
                                            BtnYes_Clicked(btnTrue, null);
                                            break;
                                        case "No":
                                            this.btnCreateWorkorder.IsVisible = true;
                                            BtnNo_Clicked(btnFalse, null);
                                            break;

                                    }


                                    #region Negative response create workorder btnshow
                                    if (item1.ResponseSubType == null || item1.ResponseSubType == false)
                                    {
                                        switch (item1.AnswerDescription)
                                        {
                                            case "":
                                                break;
                                            case "NA":
                                                break;
                                            case "Yes":
                                                 btnCreateWorkorder.IsVisible = true;
                                                break;
                                            case "No":
                                                break;

                                        }
                                    }
                                    else
                                    {
                                        switch (item1.AnswerDescription)
                                        {
                                            case "":
                                                break;
                                            case "NA":
                                                break;
                                            case "Yes":
                                                break;
                                            case "No":
                                                  btnCreateWorkorder.IsVisible = true;
                                                break;

                                        }
                                    }
                                    #endregion
                                }
                                Layout = grid;
                                sta = new Grid() { };
                                sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                sta.Children.Add(Question, 0, 0);
                                sta.Children.Add(Label1, 0, 0);
                                if (Device.Idiom == TargetIdiom.Phone)
                                {
                                    sta.Children.Add(Layout, 0, 1);
                                }
                                else
                                {
                                    sta.Children.Add(Layout, 1, 0);
                                }
                                layout1.Children.Add(sta);

                                break;

                            case "Count":
                                Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                                if (Device.Idiom == TargetIdiom.Phone)
                                {
                                    Label1 = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(12, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                else
                                {
                                    Label1 = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }

                                GenerateAnswerText(item1);

                                Layout = new Entry() { Keyboard = Keyboard.Numeric, WidthRequest = 65, HorizontalOptions = LayoutOptions.End };
                                (Layout as Entry).Text = item1.AnswerDescription;
                                (Layout as Entry).TextChanged += OnlyNumeric_TextChanged;


                                sta = new Grid() { };
                                sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                sta.Children.Add(Question, 0, 0);
                                sta.Children.Add(Label1, 0, 0);
                                sta.Children.Add(Layout, 1, 0);
                                // sta.Children.Add(new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, Children = { new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand, Margin = new Thickness(0, 0, 0, 20) } } }, 0, 0);
                                layout1.Children.Add(sta);

                                break;
                            case "Text":

                                Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };


                                if (Device.Idiom == TargetIdiom.Phone)
                                {
                                    Label1 = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(12, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                else
                                {
                                    Label1 = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                GenerateAnswerText(item1);

                                Layout = new CustomEditor() { HorizontalOptions = LayoutOptions.FillAndExpand, HeightRequest = 60 };
                                (Layout as CustomEditor).Text = item1.AnswerDescription;



                                sta = new Grid() { };
                                sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                                sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                sta.Children.Add(Question, 0, 0);
                                sta.Children.Add(Label1, 0, 0);
                                sta.Children.Add(Layout, 0, 1);
                                // sta.Children.Add(new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, Children = { new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand, Margin = new Thickness(0, 0, 0, 20) } } }, 0, 0);
                                layout1.Children.Add(sta);

                                break;

                            case "Multiple Choice":
                                Question = new Label { Text = "", Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                                if (Device.Idiom == TargetIdiom.Phone)
                                {
                                    Label1 = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(12, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                else
                                {
                                    Label1 = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                GenerateAnswerText(item1);

                                Layout = new Picker() { WidthRequest = 65, HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Center };

                                if (!string.IsNullOrWhiteSpace(item1.Option1))
                                    (Layout as Picker).Items.Add(item1.Option1);
                                if (!string.IsNullOrWhiteSpace(item1.Option2))
                                    (Layout as Picker).Items.Add(item1.Option2);
                                if (!string.IsNullOrWhiteSpace(item1.Option3))
                                    (Layout as Picker).Items.Add(item1.Option3);
                                if (!string.IsNullOrWhiteSpace(item1.Option4))
                                    (Layout as Picker).Items.Add(item1.Option4);
                                if (!string.IsNullOrWhiteSpace(item1.Option5))
                                    (Layout as Picker).Items.Add(item1.Option5);
                                if (!string.IsNullOrWhiteSpace(item1.Option6))
                                    (Layout as Picker).Items.Add(item1.Option6);
                                if (!string.IsNullOrWhiteSpace(item1.Option7))
                                    (Layout as Picker).Items.Add(item1.Option7);
                                if (!string.IsNullOrWhiteSpace(item1.Option8))
                                    (Layout as Picker).Items.Add(item1.Option8);
                                if (!string.IsNullOrWhiteSpace(item1.Option9))
                                    (Layout as Picker).Items.Add(item1.Option9);
                                if (!string.IsNullOrWhiteSpace(item1.Option10))
                                    (Layout as Picker).Items.Add(item1.Option10);


                                var index = (Layout as Picker).Items.IndexOf(item1.AnswerDescription);
                                (Layout as Picker).SelectedIndex = index;

                                sta = new Grid() { };
                                sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                sta.Children.Add(Question, 0, 0);
                                sta.Children.Add(Label1, 0, 0);
                                sta.Children.Add(Layout, 1, 0);
                                // sta.Children.Add(new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, Children = { new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand, Margin = new Thickness(0, 0, 0, 20) } } }, 0, 0);
                                layout1.Children.Add(sta);

                                break;

                            case "None":

                                Question = new Label { Text = "", Font = Font.SystemFontOfSize(16, FontAttributes.None), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start };

                                if (Device.Idiom == TargetIdiom.Phone)
                                {
                                    Label1 = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(12, FontAttributes.Bold), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                else
                                {
                                    Label1 = new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.Bold), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand };
                                }
                                GenerateAnswerText(item1);

                                sta = new Grid() { };
                                sta.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                                sta.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                                sta.Children.Add(Question, 0, 0);
                                sta.Children.Add(Label1, 0, 0);
                                // sta.Children.Add(new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, Children = { new Label { Text = item1.InspectionDescription, Font = Font.SystemFontOfSize(18, FontAttributes.Bold), TextColor = Color.Black, HorizontalOptions = LayoutOptions.Start, BindingContext = item1, LineBreakMode = LineBreakMode.WordWrap, VerticalOptions = LayoutOptions.FillAndExpand, Margin = new Thickness(0, 0, 0, 20) } } }, 0, 0);
                                //sta.Children.Add(Layout, 1, 0);
                                layout1.Children.Add(sta);

                                break;
                        }

                    }

                    var btnSaveSection = new Button() { Text = WebControlTitle.GetTargetNameByTitleName("SaveInspection"), CommandParameter = commonSections, BackgroundColor = Color.FromHex("#87CEFA"), TextColor = Color.White, BorderColor = Color.Black };
                    btnSaveSection.Clicked += BtnSaveSection_Clicked;





                    #region Add Signature image
                    if (item.SignatureRequired.GetValueOrDefault())
                    {
                        //var imageView = new CustomImage() { ImageBase64String = ImageBase64 };
                        var imageView = new CustomImage() { ImageBase64String = item.SignatureString, HeightRequest = 150 };
                        byte[] byteImg = Convert.FromBase64String(item.SignatureString);

                        // byte[] byteImageCompr = DependencyService.Get<IResizeImage>().ResizeImageAndroid(byteImg, 350, 350);
                        imageView.Source = ImageSource.FromStream(() => new MemoryStream(byteImg));
                        layout1.Children.Add(imageView);

                        var addSignatureButton = new Button() { Text = WebControlTitle.GetTargetNameByTitleName("AddSignature"), BackgroundColor = Color.FromHex("#87CEFA"), TextColor = Color.White, BorderColor = Color.Black };
                        addSignatureButton.Clicked += AddSignatureButton_Clicked;
                        layout1.Children.Add(addSignatureButton);

                    }
                    #endregion

                    layout1.Children.Add(btnSaveSection);

                    #region Estimated Hour Region
                    if (Device.Idiom == TargetIdiom.Phone)
                    {
                        var estimatedHourStackLayout = new StackLayout() { Orientation = StackOrientation.Horizontal };
                        var estimatedHourTitleLabel = new Label() { FontSize = 12, TextColor = Color.Black, Text = WebControlTitle.GetTargetNameByTitleName("EstimatedHours") };
                        var estimatedHourLabel = new Label() { FontSize = 12, TextColor = Color.Black };
                        estimatedHourLabel.Text = item.EstimatedHours.ToString();
                        estimatedHourStackLayout.Children.Add(estimatedHourTitleLabel);
                        estimatedHourStackLayout.Children.Add(estimatedHourLabel);
                        mainLayoutGroup.Children.Add(estimatedHourStackLayout);

                    }
                    else
                    {
                        var estimatedHourStackLayout = new StackLayout() { Orientation = StackOrientation.Horizontal };
                        var estimatedHourTitleLabel = new Label() { TextColor = Color.Black, Text = WebControlTitle.GetTargetNameByTitleName("EstimatedHours") };
                        var estimatedHourLabel = new Label() { TextColor = Color.Black };
                        estimatedHourLabel.Text = item.EstimatedHours.ToString();
                        estimatedHourStackLayout.Children.Add(estimatedHourTitleLabel);
                        estimatedHourStackLayout.Children.Add(estimatedHourLabel);
                        mainLayoutGroup.Children.Add(estimatedHourStackLayout);
                    }
                    #endregion
                    var oneBox = new BoxView { BackgroundColor = Color.Black, HeightRequest = 2, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };

                    mainLayoutGroup.Children.Add(oneBox);

                    MainLayout.Children.Add(mainLayoutGroup);

                    this.AnswerText.Append(System.Environment.NewLine);
                    this.AnswerText.Append(System.Environment.NewLine);

                }

            }




        }

        private async void AddSignatureButton_Clicked(object sender, EventArgs e)
        {
            var parentView = ((sender as Button).Parent as StackLayout);
            var image = parentView.Children[parentView.Children.Count - 3] as CustomImage;
            var signaturePad = new SignaturePage(image);
            signaturePad.OnSignatureDrawn += SignaturePad_OnSignatureDrawn;
            await Navigation.PushPopupAsync(signaturePad);
        }

        private void SignaturePad_OnSignatureDrawn(object sender, SignaturePageModel e)
        {
            if (e.SignatureBase64 == null)
            {
                e.ImageView.Source = ImageSource.FromFile("");
                e.ImageView.ImageBase64String = null;
            }

            else
            {
                byte[] byteImg = Convert.FromBase64String(e.SignatureBase64);
                e.ImageView.Source = ImageSource.FromStream(() => new MemoryStream(byteImg));
                e.ImageView.ImageBase64String = e.SignatureBase64;
            }
        }



        private async void CreateWorkorder_Clicked(object sender, EventArgs e)
        {

            var page = new CreateWorkorderFromInspectionPageContent(WorkorderID, AnswerText);
            //  await Navigation.PushPopupAsync(page);
            await Navigation.PushAsync(page);

        }



        private void GenerateAnswerText(ExistingInspections item)
        {
            this.AnswerText.Append(item.InspectionDescription);
            this.AnswerText.Append(": ");
            this.AnswerText.Append(item.AnswerDescription);
            this.AnswerText.Append(System.Environment.NewLine);
        }

        private void BtnTrue_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;

            //TODO: Get the button color from the btn object.
            btn.BackgroundColor = Color.Green;

            var grid = (sender as Button).Parent as Grid;
            var btnFalse = grid.Children[1] as Button;

            //TODO: Get the button color from the btn object set the inverse color over here.
            btnFalse.BackgroundColor = Color.Gray;

        }

        private void BtnFalse_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;

            //TODO: Get the button color from the btn object.
            btn.BackgroundColor = Color.Red;

            var grid = (sender as Button).Parent as Grid;
            var btnFalse = grid.Children[0] as Button;

            //TODO: Get the button color from the btn object set the inverse color over here.
            btnFalse.BackgroundColor = Color.Gray;

        }


        private void BtnYes_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;

            var grid = (sender as Button).Parent as Grid;
            var responseSubType = Convert.ToBoolean(btn.BindingContext);

            var btnFalse = grid.Children[1] as Button;
            var btnNA = grid.Children[2] as Button;
            //TODO: Get the button color from the btn object set the inverse color over here.
            if (responseSubType)
            {
                btn.BackgroundColor = Color.Green;

            }
            else
            {
                btn.BackgroundColor = Color.Red;

            }

            btnFalse.BackgroundColor = Color.Gray;
            btnNA.BackgroundColor = Color.Gray;

        }

        private void BtnNo_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;

            //TODO: Get the button color from the btn object.
            btn.BackgroundColor = Color.Red;

            var grid = (sender as Button).Parent as Grid;
            var responseSubType = Convert.ToBoolean(btn.BindingContext);

            var btnYes = grid.Children[0] as Button;
            var btnNA = grid.Children[2] as Button;
            //TODO: Get the button color from the btn object set the inverse color over here.
            if (!responseSubType)
            {
                btn.BackgroundColor = Color.Green;

            }
            else
            {
                btn.BackgroundColor = Color.Red;

            }

            btnYes.BackgroundColor = Color.Gray;
            btnNA.BackgroundColor = Color.Gray;

        }

        private void BtnNA_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;

            //TODO: Get the button color from the btn object.
            btn.BackgroundColor = Color.FromHex("#d3d3d3");

            var grid = (sender as Button).Parent as Grid;
            var responseSubType = Convert.ToBoolean(btn.BindingContext);

            var btnTrue = grid.Children[0] as Button;
            var btnFalse = grid.Children[1] as Button;

            //TODO: Get the button color from the btn object set the inverse color over here.
            btnTrue.BackgroundColor = Color.Gray;
            btnFalse.BackgroundColor = Color.Gray;

        }

        private void PassFail_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;

            if (btn.Text == "Pass")
            {
                btn.Text = "Fail";
                btn.BackgroundColor = Color.Red;
            }

            else
            {
                btn.Text = "Pass";
                btn.BackgroundColor = Color.Green;
            }
        }

        private void YesNo_Clicked(object sender, EventArgs e)
        {
            var btn = sender as Button;

            if (btn.Text == "Yes")
            {
                btn.Text = "No";
                btn.BackgroundColor = Color.Red;
            }

            else
            {
                btn.Text = "Yes";
                btn.BackgroundColor = Color.Green;
            }
        }

        private void StandardRange_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry e1 = sender as Entry;
            String val = e1.Text; //Get Current Text

            if (val.Contains(" "))//If it is more than your character restriction
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(val, "^[0-9.-]*$"))
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }

            #region Check Range
            try
            {

                if (string.IsNullOrWhiteSpace(e1.Text))
                {
                    return;
                }

                var range = e1.BindingContext as Range;

                if (range.MinRange != null && range.MaxRange != null)
                {
                    if (decimal.Parse(e1.Text) >= range.MinRange && decimal.Parse(e1.Text) <= range.MaxRange)
                    {
                        e1.BackgroundColor = Color.White;
                    }

                    else
                    {
                         this.btnCreateWorkorder.IsVisible = true;
                        e1.BackgroundColor = Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            #endregion
        }
        private void OnlyNumeric_TextChanged(object sender, TextChangedEventArgs e)
        {
            Entry e1 = sender as Entry;
            String val = e1.Text; //Get Current Text

            if (val.Contains(" "))//If it is more than your character restriction
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(val, "^[0-9.]*$"))
            {
                val = val.Remove(val.Length - 1);// Remove Last character 
                e1.Text = val; //Set the Old value
            }
        }

        private async void BtnSaveSection_Clicked(object sender, EventArgs e)
        {

            try
            {
                List<InspectionTOAnswers> listtoAnswer = new List<InspectionTOAnswers>();

                #region Local Validation
                if (Device.Idiom == TargetIdiom.Phone)
                {
                    //if (!string.IsNullOrWhiteSpace(this.PickerInspectionCompletionDatePhone.Text) && !string.IsNullOrWhiteSpace(this.PickerInspectionStartDatePhone.Text) && Convert.ToDateTime(this.PickerInspectionStartDatePhone.Text) > Convert.ToDateTime(this.PickerInspectionCompletionDatePhone.Text))
                    //{
                    //    //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, "Inspection", formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                    //    //await DisplayAlert("Alert", WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), "OK");

                    //    UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), TimeSpan.FromSeconds(2));

                    //    return;
                    //}

                }
                else
                {
                    //if (!string.IsNullOrWhiteSpace(this.PickerInspectionCompletionDateTablet.Text) && !string.IsNullOrWhiteSpace(this.PickerInspectionStartDateTablet.Text) && Convert.ToDateTime(this.PickerInspectionStartDateTablet.Text) > Convert.ToDateTime(this.PickerInspectionCompletionDateTablet.Text))
                    //{
                    //    //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, "Inspection", formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                    //    //await DisplayAlert("Alert", WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), "OK");

                    //    UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), TimeSpan.FromSeconds(2));

                    //    return;
                    //}
                }



                #endregion


                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                await Task.Delay(1000);

                var stacklayout = (sender as Button).Parent as StackLayout;

                //var context = (sender as Button).CommandParameter as List<ExistingInspections>;

                List<InspectionAnswer> listAnswer = new List<InspectionAnswer>();
                int viewCount = 0;

                try
                {
                    var signatureImage = stacklayout.Children[stacklayout.Children.Count - 3] as CustomImage;
                    if (signatureImage == null)
                    {
                        viewCount = 1;
                    }
                    else
                    {
                        viewCount = 3;
                    }

                }
                catch (Exception) { }

                for (int i = 1; i < stacklayout.Children.Count - viewCount; i++) // run it only for grid
                {
                    var stacklayout1 = stacklayout.Children[i] as Grid;

                    var context = (stacklayout1.Children[1] as Label).BindingContext as ExistingInspections;

                    var value = ExtractValueFormSection(stacklayout1);


                    if (string.IsNullOrWhiteSpace(value) && context.ResponseTypeName != "None")
                    {
                        UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("Pleasemakesureallfieldsarefilled"), TimeSpan.FromSeconds(3));
                    }

                    listAnswer.Add(new InspectionAnswer()
                    {
                        InspectionID = context.InspectionID
                                      ,
                        AnswerDescription = string.IsNullOrWhiteSpace(value) ? "" : value
                                      ,
                        WorkOrderID = WorkorderID
                    });
                }


                #region Save Answer to server

                var yourobject = new InspectionToAnswer
                {
                    InspectionAnswers = listAnswer,
                    WorkorderID = WorkorderID,

                };


                CustomImage signatureImageView = null;
                InspectionToAnswer answer = null;
                try
                {
                    var count = ((sender as Button).Parent as StackLayout).Children.Count;
                    //signView = ((sender as Button).Parent as StackLayout).Children[count - 2] as SignaturePadView;
                    signatureImageView = ((sender as Button).Parent as StackLayout).Children[stacklayout.Children.Count - 3] as CustomImage;
                }
                catch (Exception) { }

                if (signatureImageView != null)
                {
                    answer = await RetriveSignatureFromImage(signatureImageView, yourobject);
                }
                else
                {
                    answer = yourobject;
                }


                //GlobalMethod objglobal = new GlobalMethod();
                //var serviceStatus = await objglobal.ServiceCallWebClient(BaseURL + "/Inspection/service/AnswerInspection", "POST", null, answer);


                var serviceStatus = await ViewModel._inspectionService.AnswerInspection(answer);

                #endregion

                //if (btnStartTimer.Text == WebControlTitle.GetTargetNameByTitleName("StopTimer"))
                //{
                //    Timer_Clicked(btnStartTimer, null);
                //}

                #region Validation from workorder


                ServiceOutput abc = await ViewModel._workorderService.GetWorkorderByWorkorderID(UserId, WorkorderID.ToString());

                string workordercompDate = string.Empty;
                string workorderstartDate = string.Empty;


                if (abc.workOrderWrapper.workOrder.WorkStartedDate != null)
                    workorderstartDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(abc.workOrderWrapper.workOrder.WorkStartedDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("d");
                if (abc.workOrderWrapper.workOrder.CompletionDate != null)
                    workordercompDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(abc.workOrderWrapper.workOrder.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("d");


                #region Start date picker validation

                //replace this.PickerInspectionStartDate.Date with this.InspectionStartDate
                if (this.InspectionStartDate != null)
                {
                    //// if inspection start date is before than wo start date the give alert >>> Inspection start date can not lesser than WO start date
                    if (!string.IsNullOrWhiteSpace(workorderstartDate))
                    {
                        if (workorderstartDate != null && this.InspectionStartDate < DateTime.Parse(workorderstartDate))
                        {
                            // await DisplayAlert(WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("InspectionstartdatecannotlesserthanWOstartdate"), WebControlTitle.GetTargetNameByTitleName("OK"));

                            // await DisplayAlert(flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, WebControlTitle.GetTargetNameByTitleName(flInput, "InspectionstartdatecannotlesserthanWOstartdate"), flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);

                            UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("InspectionstartdatecannotlesserthanWOstartdate"), TimeSpan.FromSeconds(2));

                            UserDialogs.Instance.HideLoading();
                            return;
                        }
                    }

                    //// if inspection start date is after than wo completion date the give alert >>> Inspection start date can not greater than WO completion date
                    if (!string.IsNullOrWhiteSpace(workordercompDate))
                    {
                        if (workordercompDate != null && this.InspectionStartDate > DateTime.Parse(workordercompDate))
                        {
                            //await DisplayAlert(flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, WebControlTitle.GetTargetNameByTitleName(flInput, "InspectionstartdatecannotgreaterthanWOcompletiondate"), flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                            //await DisplayAlert(WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("InspectionstartdatecannotgreaterthanWOcompletiondate"), WebControlTitle.GetTargetNameByTitleName("OK"));

                            UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("InspectionstartdatecannotgreaterthanWOcompletiondate"), TimeSpan.FromSeconds(2));


                            UserDialogs.Instance.HideLoading();
                            return;
                        }
                    }

                }
                #endregion

                #region Completion Date picker validation
                if (!string.IsNullOrWhiteSpace(this.InspectionCompletionDate.ToString()))
                {
                    //// if inspection completion date is before than wo start date the give alert >>> Inspection completion date can not lesser than WO start date
                    if (!string.IsNullOrWhiteSpace(workorderstartDate))
                    {
                        if (workorderstartDate != null && this.InspectionCompletionDate < DateTime.Parse(workorderstartDate))
                        {

                            UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("InspectioncompletiondatecannotlesserthanWOstartdate"), TimeSpan.FromSeconds(2));

                            //await DisplayAlert(flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, WebControlTitle.GetTargetNameByTitleName(flInput, "InspectioncompletiondatecannotlesserthanWOstartdate"), flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                            //await DisplayAlert(WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("InspectioncompletiondatecannotlesserthanWOstartdate"), WebControlTitle.GetTargetNameByTitleName("OK"));

                            UserDialogs.Instance.HideLoading();
                            return;
                        }
                    }

                    //// if inspection completion date is after than wo completion date the give alert >>> Inspection completion date can not greater than WO completion date

                    // Bypass this validation if auto fill completion date is "ON"
                    var IsAutoFillOnCompletionDate = Convert.ToBoolean(abc.workOrderWrapper.IsCheckedAutoFillCompleteOnTaskAndLabor);

                    if (!string.IsNullOrWhiteSpace(workordercompDate) && !IsAutoFillOnCompletionDate)
                    {
                        if (workordercompDate != null && this.InspectionCompletionDate > DateTime.Parse(workordercompDate))
                        {
                            UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("InspectioncompletiondatecannotgreaterthanWOcompletiondate"), TimeSpan.FromSeconds(2));
                            // await DisplayAlert(WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("InspectioncompletiondatecannotgreaterthanWOcompletiondate"), WebControlTitle.GetTargetNameByTitleName("OK"));

                            //await DisplayAlert(flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, WebControlTitle.GetTargetNameByTitleName(flInput, "InspectioncompletiondatecannotgreaterthanWOcompletiondate"), flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                            UserDialogs.Instance.HideLoading();
                            return;
                        }
                    }
                }
                #endregion

                #endregion

                #region Save Inspection Time to server




                // 1st Employee/Contractor Layout//
                try
                {
                    var Stacklayout = ParentLayout.Children[0] as StackLayout;
                    var gridlayout = Stacklayout.Children[1] as Grid;

                    var HoursEntryValue = gridlayout.Children[3] as Entry;
                    var MinuteEntryValue = gridlayout.Children[5] as Entry;
                    var StartdateValue = gridlayout.Children[7] as DatePicker;
                    var CompletionDateValue = gridlayout.Children[9] as DatePicker;
                    var employeecontrcatorids = gridlayout.Children[10] as Label;
                    var employeecontrcatoridssubstring = employeecontrcatorids.Text.Split(':');
                    string EmpCntID = Int32.Parse(employeecontrcatoridssubstring[0]).ToString();
                    string EmpCntValue = employeecontrcatoridssubstring[1];
                    string EmpLaborCraftID = "0";
                    string ContLaborCraftID = "0";
                    if (EmpCntValue == "EmployeeLaborCraft")
                    {
                        EmpLaborCraftID = EmpCntID;
                    }
                    if (EmpCntValue == "ContractorLaborCraft")
                    {
                        ContLaborCraftID = EmpCntID;
                    }


                    var hours = Int32.Parse(HoursEntryValue.Text);
                    var minutes = Int32.Parse(MinuteEntryValue.Text);
                    var result = new TimeSpan(hours, minutes, 0);
                    totalTime = result.TotalSeconds;
                    this.InspectionStartDate = Convert.ToDateTime(StartdateValue.Date.ToString());
                    this.InspectionCompletionDate = Convert.ToDateTime(CompletionDateValue.Date.ToString());

                    listtoAnswer.Add(new InspectionTOAnswers()
                    {
                        WorkOrderID = WorkorderID,
                        InspectionTime = ((int)totalTime).ToString(),
                        StartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                        CompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                        EmployeeLaborCraftid = EmpLaborCraftID,
                        ContractorLaborCraftId = ContLaborCraftID,
                        ModifiedUserName = AppSettings.UserName
                    });

                }
                catch (Exception ex)
                {


                }




                // 2nd Employee/Contractor Layout///
                try
                {
                    var SecondStacklayout = ParentLayout.Children[1] as StackLayout;
                    var Secondgridlayout = SecondStacklayout.Children[1] as Grid;
                    if (Secondgridlayout == null)
                    {
                    }
                    else
                    {
                        var SecondHoursEntryValue = Secondgridlayout.Children[3] as Entry;
                        var SecondMinuteEntryValue = Secondgridlayout.Children[5] as Entry;
                        var SecondStartdateValue = Secondgridlayout.Children[7] as DatePicker;
                        var SecondCompletionDateValue = Secondgridlayout.Children[9] as DatePicker;
                        var employeecontrcatorids2 = Secondgridlayout.Children[10] as Label;
                        var employeecontrcatoridssubstring2 = employeecontrcatorids2.Text.Split(':');
                        string EmpCntID2 = Int32.Parse(employeecontrcatoridssubstring2[0]).ToString();
                        string EmpCntValue2 = employeecontrcatoridssubstring2[1];
                        string EmpLaborCraftID2 = "0";
                        string ContLaborCraftID2 = "0";
                        if (EmpCntValue2 == "EmployeeLaborCraft")
                        {
                            EmpLaborCraftID2 = EmpCntID2;
                        }
                        if (EmpCntValue2 == "ContractorLaborCraft")
                        {
                            ContLaborCraftID2 = EmpCntID2;
                        }

                        var Secondhours = Int32.Parse(SecondHoursEntryValue.Text);
                        var Secondminutes = Int32.Parse(SecondMinuteEntryValue.Text);
                        var Secondresult = new TimeSpan(Secondhours, Secondminutes, 0);
                        totalTime = Secondresult.TotalSeconds;
                        this.InspectionStartDate = Convert.ToDateTime(SecondStartdateValue.Date.ToString());
                        this.InspectionCompletionDate = Convert.ToDateTime(SecondCompletionDateValue.Date.ToString());

                        listtoAnswer.Add(new InspectionTOAnswers()
                        {
                            WorkOrderID = WorkorderID,
                            InspectionTime = ((int)totalTime).ToString(),
                            StartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                            CompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                            EmployeeLaborCraftid = EmpLaborCraftID2,
                            ContractorLaborCraftId = ContLaborCraftID2,
                            ModifiedUserName = AppSettings.UserName
                        });
                    }

                }
                catch (Exception ex)
                {


                }



                // 3rd Employee/Contractor Layout///
                try
                {
                    var ThirdStacklayout = ParentLayout.Children[2] as StackLayout;
                    var Thirdgridlayout = ThirdStacklayout.Children[1] as Grid;
                    if (Thirdgridlayout == null)
                    {
                    }
                    else
                    {
                        var ThirdHoursEntryValue = Thirdgridlayout.Children[3] as Entry;
                        var ThirdMinuteEntryValue = Thirdgridlayout.Children[5] as Entry;
                        var ThirdStartdateValue = Thirdgridlayout.Children[7] as DatePicker;
                        var ThirdCompletionDateValue = Thirdgridlayout.Children[9] as DatePicker;

                        var employeecontrcatorids3 = Thirdgridlayout.Children[10] as Label;
                        var employeecontrcatoridssubstring3 = employeecontrcatorids3.Text.Split(':');
                        string EmpCntID3 = Int32.Parse(employeecontrcatoridssubstring3[0]).ToString();
                        string EmpCntValue3 = employeecontrcatoridssubstring3[1];
                        string EmpLaborCraftID3 = "0";
                        string ContLaborCraftID3 = "0";
                        if (EmpCntValue3 == "EmployeeLaborCraft")
                        {
                            EmpLaborCraftID3 = EmpCntID3;
                        }
                        if (EmpCntValue3 == "ContractorLaborCraft")
                        {
                            ContLaborCraftID3 = EmpCntID3;
                        }

                        var Thirdhours = Int32.Parse(ThirdHoursEntryValue.Text);
                        var Thirdminutes = Int32.Parse(ThirdMinuteEntryValue.Text);
                        var Thirdresult = new TimeSpan(Thirdhours, Thirdminutes, 0);
                        totalTime = Thirdresult.TotalSeconds;
                        this.InspectionStartDate = Convert.ToDateTime(ThirdStartdateValue.Date.ToString());
                        this.InspectionCompletionDate = Convert.ToDateTime(ThirdCompletionDateValue.Date.ToString());

                        listtoAnswer.Add(new InspectionTOAnswers()
                        {
                            WorkOrderID = WorkorderID,
                            InspectionTime = ((int)totalTime).ToString(),
                            StartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                            CompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                            EmployeeLaborCraftid = EmpLaborCraftID3,
                            ContractorLaborCraftId = ContLaborCraftID3,
                            ModifiedUserName = AppSettings.UserName
                        });
                    }

                }
                catch (Exception ex)
                {


                }



                // 4th Employee/Contractor Layout///
                try
                {
                    var FourthStacklayout = ParentLayout.Children[3] as StackLayout;
                    var Fourthgridlayout = FourthStacklayout.Children[1] as Grid;
                    if (Fourthgridlayout == null)
                    {
                    }
                    else
                    {


                        var FourthHoursEntryValue = Fourthgridlayout.Children[3] as Entry;
                        var FourthMinuteEntryValue = Fourthgridlayout.Children[5] as Entry;
                        var FourthStartdateValue = Fourthgridlayout.Children[7] as DatePicker;
                        var FourthCompletionDateValue = Fourthgridlayout.Children[9] as DatePicker;

                        var employeecontrcatorids4 = Fourthgridlayout.Children[10] as Label;
                        var employeecontrcatoridssubstring4 = employeecontrcatorids4.Text.Split(':');
                        string EmpCntID4 = Int32.Parse(employeecontrcatoridssubstring4[0]).ToString();
                        string EmpCntValue4 = employeecontrcatoridssubstring4[1];
                        string EmpLaborCraftID4 = "0";
                        string ContLaborCraftID4 = "0";
                        if (EmpCntValue4 == "EmployeeLaborCraft")
                        {
                            EmpLaborCraftID4 = EmpCntID4;
                        }
                        if (EmpCntValue4 == "ContractorLaborCraft")
                        {
                            ContLaborCraftID4 = EmpCntID4;
                        }


                        var Fourthhours = Int32.Parse(FourthHoursEntryValue.Text);
                        var Fourthminutes = Int32.Parse(FourthMinuteEntryValue.Text);
                        var Fourthresult = new TimeSpan(Fourthhours, Fourthminutes, 0);
                        totalTime = Fourthresult.TotalSeconds;
                        this.InspectionStartDate = Convert.ToDateTime(FourthStartdateValue.Date.ToString());
                        this.InspectionCompletionDate = Convert.ToDateTime(FourthCompletionDateValue.Date.ToString());

                        listtoAnswer.Add(new InspectionTOAnswers()
                        {
                            WorkOrderID = WorkorderID,
                            InspectionTime = ((int)totalTime).ToString(),
                            StartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                            CompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                            EmployeeLaborCraftid = EmpLaborCraftID4,
                            ContractorLaborCraftId = ContLaborCraftID4,
                            ModifiedUserName = AppSettings.UserName
                        });
                    }

                }
                catch (Exception ex)
                {


                }



                // 5th Employee/Contractor Layout///
                try
                {
                    var FifthStacklayout = ParentLayout.Children[4] as StackLayout;
                    var Fifthgridlayout = FifthStacklayout.Children[1] as Grid;
                    if (Fifthgridlayout == null)
                    {
                    }
                    else
                    {
                        var FifthHoursEntryValue = Fifthgridlayout.Children[3] as Entry;
                        var FifthMinuteEntryValue = Fifthgridlayout.Children[5] as Entry;
                        var FifthStartdateValue = Fifthgridlayout.Children[7] as DatePicker;
                        var FifthCompletionDateValue = Fifthgridlayout.Children[9] as DatePicker;

                        var employeecontrcatorids5 = Fifthgridlayout.Children[10] as Label;
                        var employeecontrcatoridssubstring5 = employeecontrcatorids5.Text.Split(':');
                        string EmpCntID5 = Int32.Parse(employeecontrcatoridssubstring5[0]).ToString();
                        string EmpCntValue5 = employeecontrcatoridssubstring5[1];
                        string EmpLaborCraftID5 = "0";
                        string ContLaborCraftID5 = "0";
                        if (EmpCntValue5 == "EmployeeLaborCraft")
                        {
                            EmpLaborCraftID5 = EmpCntID5;
                        }
                        if (EmpCntValue5 == "ContractorLaborCraft")
                        {
                            ContLaborCraftID5 = EmpCntID5;
                        }

                        var Fifthhours = Int32.Parse(FifthHoursEntryValue.Text);
                        var Fifthminutes = Int32.Parse(FifthMinuteEntryValue.Text);
                        var Fifthresult = new TimeSpan(Fifthhours, Fifthminutes, 0);
                        totalTime = Fifthresult.TotalSeconds;
                        this.InspectionStartDate = Convert.ToDateTime(FifthStartdateValue.Date.ToString());
                        this.InspectionCompletionDate = Convert.ToDateTime(FifthCompletionDateValue.Date.ToString());

                        listtoAnswer.Add(new InspectionTOAnswers()
                        {
                            WorkOrderID = WorkorderID,
                            InspectionTime = ((int)totalTime).ToString(),
                            StartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                            CompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                            EmployeeLaborCraftid = EmpLaborCraftID5,
                            ContractorLaborCraftId = ContLaborCraftID5,
                            ModifiedUserName = AppSettings.UserName
                        });
                    }
                }
                catch (Exception ex)
                {


                }



                //var payload = new Dictionary<string, string>
                //{
                //  {"inspectionToAnswers", listtoAnswer.ToList()},
                //  {"ClientIANATimeZone", DateTimeZoneProviders.Serialization.GetSystemDefault().ToString()},
                //   {"UserID",long.Parse(this.UserId).ToString()}
                //};


                var yourobject1 = new InspectionTOAnswers
                {
                    inspectionToAnswers = listtoAnswer,
                    ClientIANATimeZone = DateTimeZoneProviders.Serialization.GetSystemDefault().ToString(),
                    UserID = long.Parse(this.UserId),

                };

                //var yourobject1 = new InspectionToAnswer
                //{
                //    InspectionTimeInSeconds = ((int)totalTime).ToString(),
                //    WorkorderID = WorkorderID,
                //    InspectionStartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                //    InspectionCompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                //    ClientIANATimeZone = DateTimeZoneProviders.Serialization.GetSystemDefault().ToString(),
                //    UserID = long.Parse(this.UserId)

                //};

                //JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
                //{
                //    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                //};
                //string microsoftJson = JsonConvert.SerializeObject(yourobject1, microsoftDateFormatSettings);

                //ServiceOutput serviceStatus1 = await objglobal.ServiceCallWebClient31(BaseURL + "/Inspection/service/SaveWorkOrderInspectionTime", "POST", null, microsoftJson);
                // var workorderemp = (WorkOrderEmployee)((Button)sender).CommandParameter;
                // var workordercnt = (WorkorderContractor)((Button)sender).CommandParameter;
                ServiceOutput serviceStatus1 = await ViewModel._inspectionService.SaveWorkorderInspectionTime(yourobject1);


                #endregion

                if (serviceStatus1.servicestatus == "true")
                {
                    // enthour.Text = "";
                    // entmin.Text = "";
                    UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("AnswerSuccessfullySaved"), TimeSpan.FromSeconds(2));

                    foreach (var item in listtoAnswer)
                    {
                        string Employeekey = "WorkorderEmployee:" + item.EmployeeLaborCraftid;
                        string Contractorkey = "WorkorderContracator:" + item.ContractorLaborCraftId;
                        WorkorderInspectionStorge.Storage.Delete(Employeekey);
                        WorkorderInspectionStorge.Storage.Delete(Contractorkey);

                    }
                    //await DisplayAlert("", flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "AnswerSuccessfullySaved").TargetName, flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                }

                //DeleteSavedTimer();
                UserDialogs.Instance.HideLoading();
                MainLayout.Children.Clear();
                OnAppearing();

            }
            catch (Exception ex)
            {


            }


        }
        private async void Btnsave_Clicked(object sender, EventArgs e)
        {
            List<InspectionTOAnswers> listtoAnswer = new List<InspectionTOAnswers>();

            #region Local Validation
            if (Device.Idiom == TargetIdiom.Phone)
            {
                //if (!string.IsNullOrWhiteSpace(this.PickerInspectionCompletionDatePhone.Text) && !string.IsNullOrWhiteSpace(this.PickerInspectionStartDatePhone.Text) && Convert.ToDateTime(this.PickerInspectionStartDatePhone.Text) > Convert.ToDateTime(this.PickerInspectionCompletionDatePhone.Text))
                //{
                //    //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, "Inspection", formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                //    //await DisplayAlert("Alert", WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), "OK");
                //    UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), TimeSpan.FromSeconds(2));
                //    return;
                //}
            }
            else
            {
                //if (!string.IsNullOrWhiteSpace(this.PickerInspectionCompletionDateTablet.Text) && !string.IsNullOrWhiteSpace(this.PickerInspectionStartDateTablet.Text) && Convert.ToDateTime(this.PickerInspectionStartDateTablet.Text) > Convert.ToDateTime(this.PickerInspectionCompletionDateTablet.Text))
                //{
                //    //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, "Inspection", formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                //    //await DisplayAlert("Alert", WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), "OK");
                //    UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), TimeSpan.FromSeconds(2));
                //    return;
                //}
            }




            #endregion

            UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
            await Task.Delay(1000);
            var ssdv = (sender as Button).Parent as StackLayout;




            var kfm = ssdv.Children[0] as Grid;

            //  var kfm1 = ParentLayout.Children[0] as StackLayout;

            if (kfm == null)
            {
                return;
            }

            var context = kfm.BindingContext as ExistingInspections;
            string value = null;

            switch (context.ResponseTypeName)
            {

                case "Pass/Fail":
                    var gridPF = kfm.Children[2] as Grid;
                    var btn1PF = gridPF.Children[0] as Button;
                    var btn2PF = gridPF.Children[1] as Button;
                    value = "";
                    if (btn1PF.BackgroundColor != Color.Gray)
                    {
                        value = btn1PF.Text;
                    }
                    if (btn2PF.BackgroundColor != Color.Gray)
                    {
                        value = btn2PF.Text;
                    }
                    break;

                case "Standard Range":
                    value = (kfm.Children[2] as Entry).Text;
                    break;

                case "Yes/No/N/A":
                    var grid = kfm.Children[2] as Grid;
                    var btn1 = grid.Children[0] as Button;
                    var btn2 = grid.Children[1] as Button;
                    var btn3 = grid.Children[2] as Button;
                    value = "";
                    if (btn1.BackgroundColor != Color.Gray)
                    {
                        value = btn1.Text;
                    }
                    if (btn2.BackgroundColor != Color.Gray)
                    {
                        value = btn2.Text;
                    }

                    //TODO: add the btn3 which is NA button and add the value.
                    if (btn3.BackgroundColor != Color.Gray)
                    {
                        value = btn3.Text;
                    }

                    break;


                case "Count":
                    value = (kfm.Children[2] as Entry).Text;
                    break;
                case "Text":
                    value = (kfm.Children[2] as Editor).Text;
                    break;

                case "Multiple Choice":
                    var index3 = (kfm.Children[2] as Picker).SelectedIndex;
                    if (index3 == -1)
                    {
                        value = null;
                    }
                    else
                    {
                        value = (kfm.Children[2] as Picker).Items[index3];
                    }

                    break;
            }


            List<InspectionAnswer> listAnswer = new List<InspectionAnswer>();
            listAnswer.Add(new InspectionAnswer()
            {
                InspectionID = context.InspectionID,
                AnswerDescription = string.IsNullOrWhiteSpace(value) ? "" : value,
                WorkOrderID = WorkorderID
            });



            #region Save Answer to server
            var yourobject = new InspectionToAnswer
            {
                InspectionAnswers = listAnswer,
                WorkorderID = WorkorderID,

            };

            CustomImage signatureImageView = null;
            InspectionToAnswer answer = null;
            try
            {
                signatureImageView = ((sender as Button).Parent as StackLayout).Children[1] as CustomImage;
            }
            catch (Exception) { }

            if (signatureImageView != null)
            {
                answer = await RetriveSignatureFromImage(signatureImageView, yourobject);
            }
            else
            {
                answer = yourobject;
            }


            // GlobalMethod objglobal = new GlobalMethod();
            //ServiceOutput serviceStatus = await objglobal.ServiceCallWebClient(BaseURL + "/Inspection/service/AnswerInspection", "POST", null, answer);
            ServiceOutput serviceStatus = await ViewModel._inspectionService.AnswerInspection(answer);
            #endregion

            //if (btnStartTimer.Text == WebControlTitle.GetTargetNameByTitleName("StopTimer"))
            //{
            //    Timer_Clicked(btnStartTimer, null);
            //}

            #region Validation from workorder

            //Dictionary<string, string> urlseg = new Dictionary<string, string>();
            //urlseg.Add("WORKORDERID", WorkorderID.ToString());
            //urlseg.Add("USERID", this.UserId);
            //Task<ServiceOutput> abc = new GlobalMethod().ServiceCallWebClient(BaseURL + "/Inspection/Service/WorkOrder", "GET", urlseg, null);
            ServiceOutput abc = await ViewModel._workorderService.GetWorkorderByWorkorderID(UserId, WorkorderID.ToString());

            string workordercompDate = string.Empty;
            string workorderstartDate = string.Empty;


            if (abc.workOrderWrapper.workOrder.WorkStartedDate != null)
                workorderstartDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(abc.workOrderWrapper.workOrder.WorkStartedDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("d");
            if (abc.workOrderWrapper.workOrder.CompletionDate != null)
                workordercompDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(abc.workOrderWrapper.workOrder.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("d");


            #region Start date picker validation

            //replace this.PickerInspectionStartDate.Date with this.InspectionStartDate
            if (this.InspectionStartDate != null)
            {
                //// if inspection start date is before than wo start date the give alert >>> Inspection start date can not lesser than WO start date
                if (!string.IsNullOrWhiteSpace(workorderstartDate))
                {
                    if (workorderstartDate != null && this.InspectionStartDate < DateTime.Parse(workorderstartDate))
                    {
                        //await DisplayAlert(flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, WebControlTitle.GetTargetNameByTitleName(flInput, "InspectionstartdatecannotlesserthanWOstartdate"), flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);

                        //await DisplayAlert(WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("InspectionstartdatecannotlesserthanWOstartdate"), WebControlTitle.GetTargetNameByTitleName("OK"));


                        UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("InspectionstartdatecannotlesserthanWOstartdate"), TimeSpan.FromSeconds(2));



                        UserDialogs.Instance.HideLoading();
                        return;
                    }
                }

                //// if inspection start date is after than wo completion date the give alert >>> Inspection start date can not greater than WO completion date
                if (!string.IsNullOrWhiteSpace(workordercompDate))
                {
                    if (workordercompDate != null && this.InspectionStartDate > DateTime.Parse(workordercompDate))
                    {
                        //await DisplayAlert(flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, WebControlTitle.GetTargetNameByTitleName(flInput, "InspectionstartdatecannotgreaterthanWOcompletiondate"), flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);

                        //await DisplayAlert(WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("InspectionstartdatecannotgreaterthanWOcompletiondate"), WebControlTitle.GetTargetNameByTitleName("OK"));

                        UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("InspectionstartdatecannotgreaterthanWOcompletiondate"), TimeSpan.FromSeconds(2));


                        UserDialogs.Instance.HideLoading();
                        return;
                    }
                }

            }
            #endregion

            #region Completion Date picker validation
            if (!string.IsNullOrWhiteSpace(this.InspectionCompletionDate.ToString()))
            {
                //// if inspection completion date is before than wo start date the give alert >>> Inspection completion date can not lesser than WO start date
                if (!string.IsNullOrWhiteSpace(workorderstartDate))
                {
                    if (workorderstartDate != null && this.InspectionCompletionDate < DateTime.Parse(workorderstartDate))
                    {
                        //await DisplayAlert(flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, WebControlTitle.GetTargetNameByTitleName(flInput, "InspectioncompletiondatecannotlesserthanWOstartdate"), flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);

                        // await DisplayAlert(WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("InspectioncompletiondatecannotlesserthanWOstartdate"), WebControlTitle.GetTargetNameByTitleName("OK"));

                        UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("InspectioncompletiondatecannotlesserthanWOstartdate"), TimeSpan.FromSeconds(2));


                        UserDialogs.Instance.HideLoading();
                        return;
                    }
                }

                //// if inspection completion date is after than wo completion date the give alert >>> Inspection completion date can not greater than WO completion date

                // Bypass this validation if auto fill completion date is "ON"
                var IsAutoFillOnCompletionDate = Convert.ToBoolean(abc.workOrderWrapper.IsCheckedAutoFillCompleteOnTaskAndLabor);

                if (!string.IsNullOrWhiteSpace(workordercompDate) && !IsAutoFillOnCompletionDate)
                {
                    if (workordercompDate != null && this.InspectionCompletionDate > DateTime.Parse(workordercompDate))
                    {
                        //await DisplayAlert(flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, WebControlTitle.GetTargetNameByTitleName(flInput, "InspectioncompletiondatecannotgreaterthanWOcompletiondate"), flInput.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);

                        //await DisplayAlert(WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("InspectioncompletiondatecannotgreaterthanWOcompletiondate"), WebControlTitle.GetTargetNameByTitleName("OK"));

                        UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("InspectioncompletiondatecannotgreaterthanWOcompletiondate"), TimeSpan.FromSeconds(2));


                        UserDialogs.Instance.HideLoading();
                        return;
                    }
                }
            }
            #endregion

            #endregion

            #region Save Inspection Time to server

            


            // 1st Employee/Contractor Layout//
            try
            {
                var Stacklayout = ParentLayout.Children[0] as StackLayout;
                var gridlayout = Stacklayout.Children[1] as Grid;

                var HoursEntryValue = gridlayout.Children[3] as Entry;
                var MinuteEntryValue = gridlayout.Children[5] as Entry;
                var StartdateValue = gridlayout.Children[7] as DatePicker;
                var CompletionDateValue = gridlayout.Children[9] as DatePicker;
                var employeecontrcatorids = gridlayout.Children[10] as Label;
                var employeecontrcatoridssubstring = employeecontrcatorids.Text.Split(':');
                string EmpCntID = Int32.Parse(employeecontrcatoridssubstring[0]).ToString();
                string EmpCntValue = employeecontrcatoridssubstring[1];
                string EmpLaborCraftID = "0";
                string ContLaborCraftID = "0";
                if (EmpCntValue == "EmployeeLaborCraft")
                {
                    EmpLaborCraftID = EmpCntID;
                }
                if (EmpCntValue == "ContractorLaborCraft")
                {
                    ContLaborCraftID = EmpCntID;
                }


                var hours = Int32.Parse(HoursEntryValue.Text);
                var minutes = Int32.Parse(MinuteEntryValue.Text);
                var result = new TimeSpan(hours, minutes, 0);
                totalTime = result.TotalSeconds;
                this.InspectionStartDate = Convert.ToDateTime(StartdateValue.Date.ToString());
                this.InspectionCompletionDate = Convert.ToDateTime(CompletionDateValue.Date.ToString());

                listtoAnswer.Add(new InspectionTOAnswers()
                {
                    WorkOrderID = WorkorderID,
                    InspectionTime = ((int)totalTime).ToString(),
                    StartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                    CompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                    EmployeeLaborCraftid = EmpLaborCraftID,
                    ContractorLaborCraftId = ContLaborCraftID,
                    ModifiedUserName = AppSettings.UserName
                });

            }
            catch (Exception ex)
            {

               
            }




            // 2nd Employee/Contractor Layout///
            try
            {
                var SecondStacklayout = ParentLayout.Children[1] as StackLayout;
                var Secondgridlayout = SecondStacklayout.Children[1] as Grid;
                if (Secondgridlayout == null)
                {
                }
                else
                {
                    var SecondHoursEntryValue = Secondgridlayout.Children[3] as Entry;
                    var SecondMinuteEntryValue = Secondgridlayout.Children[5] as Entry;
                    var SecondStartdateValue = Secondgridlayout.Children[7] as DatePicker;
                    var SecondCompletionDateValue = Secondgridlayout.Children[9] as DatePicker;
                    var employeecontrcatorids2 = Secondgridlayout.Children[10] as Label;
                    var employeecontrcatoridssubstring2 = employeecontrcatorids2.Text.Split(':');
                    string EmpCntID2 = Int32.Parse(employeecontrcatoridssubstring2[0]).ToString();
                    string EmpCntValue2 = employeecontrcatoridssubstring2[1];
                    string EmpLaborCraftID2 = "0";
                    string ContLaborCraftID2 = "0";
                    if (EmpCntValue2 == "EmployeeLaborCraft")
                    {
                        EmpLaborCraftID2 = EmpCntID2;
                    }
                    if (EmpCntValue2 == "ContractorLaborCraft")
                    {
                        ContLaborCraftID2 = EmpCntID2;
                    }

                    var Secondhours = Int32.Parse(SecondHoursEntryValue.Text);
                    var Secondminutes = Int32.Parse(SecondMinuteEntryValue.Text);
                    var Secondresult = new TimeSpan(Secondhours, Secondminutes, 0);
                    totalTime = Secondresult.TotalSeconds;
                    this.InspectionStartDate = Convert.ToDateTime(SecondStartdateValue.Date.ToString());
                    this.InspectionCompletionDate = Convert.ToDateTime(SecondCompletionDateValue.Date.ToString());

                    listtoAnswer.Add(new InspectionTOAnswers()
                    {
                        WorkOrderID = WorkorderID,
                        InspectionTime = ((int)totalTime).ToString(),
                        StartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                        CompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                        EmployeeLaborCraftid = EmpLaborCraftID2,
                        ContractorLaborCraftId = ContLaborCraftID2,
                        ModifiedUserName = AppSettings.UserName
                    });
                }

            }
            catch (Exception ex)
            {

               
            }



            // 3rd Employee/Contractor Layout///
            try
            {
                var ThirdStacklayout = ParentLayout.Children[2] as StackLayout;
                var Thirdgridlayout = ThirdStacklayout.Children[1] as Grid;
                if (Thirdgridlayout == null)
                {
                }
                else
                {
                    var ThirdHoursEntryValue = Thirdgridlayout.Children[3] as Entry;
                    var ThirdMinuteEntryValue = Thirdgridlayout.Children[5] as Entry;
                    var ThirdStartdateValue = Thirdgridlayout.Children[7] as DatePicker;
                    var ThirdCompletionDateValue = Thirdgridlayout.Children[9] as DatePicker;

                    var employeecontrcatorids3 = Thirdgridlayout.Children[10] as Label;
                    var employeecontrcatoridssubstring3 = employeecontrcatorids3.Text.Split(':');
                    string EmpCntID3 = Int32.Parse(employeecontrcatoridssubstring3[0]).ToString();
                    string EmpCntValue3 = employeecontrcatoridssubstring3[1];
                    string EmpLaborCraftID3 = "0";
                    string ContLaborCraftID3 = "0";
                    if (EmpCntValue3 == "EmployeeLaborCraft")
                    {
                        EmpLaborCraftID3 = EmpCntID3;
                    }
                    if (EmpCntValue3 == "ContractorLaborCraft")
                    {
                        ContLaborCraftID3 = EmpCntID3;
                    }

                    var Thirdhours = Int32.Parse(ThirdHoursEntryValue.Text);
                    var Thirdminutes = Int32.Parse(ThirdMinuteEntryValue.Text);
                    var Thirdresult = new TimeSpan(Thirdhours, Thirdminutes, 0);
                    totalTime = Thirdresult.TotalSeconds;
                    this.InspectionStartDate = Convert.ToDateTime(ThirdStartdateValue.Date.ToString());
                    this.InspectionCompletionDate = Convert.ToDateTime(ThirdCompletionDateValue.Date.ToString());

                    listtoAnswer.Add(new InspectionTOAnswers()
                    {
                        WorkOrderID = WorkorderID,
                        InspectionTime = ((int)totalTime).ToString(),
                        StartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                        CompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                        EmployeeLaborCraftid = EmpLaborCraftID3,
                        ContractorLaborCraftId = ContLaborCraftID3,
                        ModifiedUserName = AppSettings.UserName
                    });
                }

            }
            catch (Exception ex)
            {

               
            }
           


            // 4th Employee/Contractor Layout///
            try
            {
                var FourthStacklayout = ParentLayout.Children[3] as StackLayout;
                var Fourthgridlayout = FourthStacklayout.Children[1] as Grid;
                if (Fourthgridlayout == null)
                {
                }
                else
                {


                    var FourthHoursEntryValue = Fourthgridlayout.Children[3] as Entry;
                    var FourthMinuteEntryValue = Fourthgridlayout.Children[5] as Entry;
                    var FourthStartdateValue = Fourthgridlayout.Children[7] as DatePicker;
                    var FourthCompletionDateValue = Fourthgridlayout.Children[9] as DatePicker;

                    var employeecontrcatorids4 = Fourthgridlayout.Children[10] as Label;
                    var employeecontrcatoridssubstring4 = employeecontrcatorids4.Text.Split(':');
                    string EmpCntID4 = Int32.Parse(employeecontrcatoridssubstring4[0]).ToString();
                    string EmpCntValue4 = employeecontrcatoridssubstring4[1];
                    string EmpLaborCraftID4 = "0";
                    string ContLaborCraftID4 = "0";
                    if (EmpCntValue4 == "EmployeeLaborCraft")
                    {
                        EmpLaborCraftID4 = EmpCntID4;
                    }
                    if (EmpCntValue4 == "ContractorLaborCraft")
                    {
                        ContLaborCraftID4 = EmpCntID4;
                    }


                    var Fourthhours = Int32.Parse(FourthHoursEntryValue.Text);
                    var Fourthminutes = Int32.Parse(FourthMinuteEntryValue.Text);
                    var Fourthresult = new TimeSpan(Fourthhours, Fourthminutes, 0);
                    totalTime = Fourthresult.TotalSeconds;
                    this.InspectionStartDate = Convert.ToDateTime(FourthStartdateValue.Date.ToString());
                    this.InspectionCompletionDate = Convert.ToDateTime(FourthCompletionDateValue.Date.ToString());

                    listtoAnswer.Add(new InspectionTOAnswers()
                    {
                        WorkOrderID = WorkorderID,
                        InspectionTime = ((int)totalTime).ToString(),
                        StartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                        CompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                        EmployeeLaborCraftid = EmpLaborCraftID4,
                        ContractorLaborCraftId = ContLaborCraftID4,
                        ModifiedUserName = AppSettings.UserName
                    });
                }

            }
            catch (Exception ex)
            {


            }



            // 5th Employee/Contractor Layout///
            try
            {
                var FifthStacklayout = ParentLayout.Children[4] as StackLayout;
                var Fifthgridlayout = FifthStacklayout.Children[1] as Grid;
                if (Fifthgridlayout == null)
                {
                }
                else
                {
                    var FifthHoursEntryValue = Fifthgridlayout.Children[3] as Entry;
                    var FifthMinuteEntryValue = Fifthgridlayout.Children[5] as Entry;
                    var FifthStartdateValue = Fifthgridlayout.Children[7] as DatePicker;
                    var FifthCompletionDateValue = Fifthgridlayout.Children[9] as DatePicker;

                    var employeecontrcatorids5 = Fifthgridlayout.Children[10] as Label;
                    var employeecontrcatoridssubstring5 = employeecontrcatorids5.Text.Split(':');
                    string EmpCntID5 = Int32.Parse(employeecontrcatoridssubstring5[0]).ToString();
                    string EmpCntValue5 = employeecontrcatoridssubstring5[1];
                    string EmpLaborCraftID5 = "0";
                    string ContLaborCraftID5 = "0";
                    if (EmpCntValue5 == "EmployeeLaborCraft")
                    {
                        EmpLaborCraftID5 = EmpCntID5;
                    }
                    if (EmpCntValue5 == "ContractorLaborCraft")
                    {
                        ContLaborCraftID5 = EmpCntID5;
                    }

                    var Fifthhours = Int32.Parse(FifthHoursEntryValue.Text);
                    var Fifthminutes = Int32.Parse(FifthMinuteEntryValue.Text);
                    var Fifthresult = new TimeSpan(Fifthhours, Fifthminutes, 0);
                    totalTime = Fifthresult.TotalSeconds;
                    this.InspectionStartDate = Convert.ToDateTime(FifthStartdateValue.Date.ToString());
                    this.InspectionCompletionDate = Convert.ToDateTime(FifthCompletionDateValue.Date.ToString());

                    listtoAnswer.Add(new InspectionTOAnswers()
                    {
                        WorkOrderID = WorkorderID,
                        InspectionTime = ((int)totalTime).ToString(),
                        StartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                        CompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
                        EmployeeLaborCraftid = EmpLaborCraftID5,
                        ContractorLaborCraftId = ContLaborCraftID5,
                        ModifiedUserName = AppSettings.UserName
                    });
                }
            }
            catch (Exception ex)
            {

               
            }
            


            //var payload = new Dictionary<string, string>
            //{
            //  {"inspectionToAnswers", listtoAnswer.ToList()},
            //  {"ClientIANATimeZone", DateTimeZoneProviders.Serialization.GetSystemDefault().ToString()},
            //   {"UserID",long.Parse(this.UserId).ToString()}
            //};


            var yourobject1 = new InspectionTOAnswers
            {
                inspectionToAnswers = listtoAnswer,
                ClientIANATimeZone = DateTimeZoneProviders.Serialization.GetSystemDefault().ToString(),
                UserID = long.Parse(this.UserId),

            };

            //var yourobject1 = new InspectionToAnswer
            //{
            //    InspectionTimeInSeconds = ((int)totalTime).ToString(),
            //    WorkorderID = WorkorderID,
            //    InspectionStartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
            //    InspectionCompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,
            //    ClientIANATimeZone = DateTimeZoneProviders.Serialization.GetSystemDefault().ToString(),
            //    UserID = long.Parse(this.UserId)

            //};

            //JsonSerializerSettings microsoftDateFormatSettings = new JsonSerializerSettings
            //{
            //    DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            //};
            //string microsoftJson = JsonConvert.SerializeObject(yourobject1, microsoftDateFormatSettings);

            //ServiceOutput serviceStatus1 = await objglobal.ServiceCallWebClient31(BaseURL + "/Inspection/service/SaveWorkOrderInspectionTime", "POST", null, microsoftJson);
           // var workorderemp = (WorkOrderEmployee)((Button)sender).CommandParameter;
           // var workordercnt = (WorkorderContractor)((Button)sender).CommandParameter;
            ServiceOutput serviceStatus1 = await ViewModel._inspectionService.SaveWorkorderInspectionTime(yourobject1);


            #endregion


            if (serviceStatus1.servicestatus == "true")
            {
                //enthour.Text = "";
                //entmin.Text = "";

                UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("AnswerSuccessfullySaved"), TimeSpan.FromSeconds(2));

                // Delete Employee and Contrcator Timer/////
                //var buttonSave = sender as Button;
                //var parent = buttonSave.Parent;
                //var parentLayout = parent.Parent as StackLayout;

                //var workorderemp = buttonSave.CommandParameter as WorkOrderEmployee;
                //var workordercnt = buttonSave.CommandParameter as WorkorderContractor;

                foreach(var item in listtoAnswer)
                {
                    string Employeekey = "WorkorderEmployee:" + item.EmployeeLaborCraftid;
                    string Contractorkey = "WorkorderContracator:" + item.ContractorLaborCraftId;
                    WorkorderInspectionStorge.Storage.Delete(Employeekey);
                    WorkorderInspectionStorge.Storage.Delete(Contractorkey);
                   
                }

              
            }

           // DeleteSavedTimer();
            UserDialogs.Instance.HideLoading();

            if (string.IsNullOrWhiteSpace(value))
            {
                UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("Pleasemakesureallfieldsarefilled"), TimeSpan.FromSeconds(2));
                //await DisplayAlert("", WebControlTitle.GetTargetNameByTitleName("Pleasemakesureallfieldsarefilled"), WebControlTitle.GetTargetNameByTitleName("OK"));

            }


            MainLayout.Children.Clear();
            OnAppearing();

        }



        private async Task<InspectionToAnswer> RetriveSignatureFromView(SignaturePadView signView, InspectionToAnswer inspectionAnswer)
        {
            if (inspectionAnswer != null)
            {
                var _stream = await signView.GetImageStreamAsync(SignatureImageFormat.Png, false, false);
                if (_stream != null)
                {
                    inspectionAnswer.InspectionAnswers[0].SignaturePath = Convert.ToBase64String(StreamToByteArrary(_stream));
                    inspectionAnswer.InspectionAnswers[0].SignatureString = JsonConvert.SerializeObject(signView.Points);
                }
                return inspectionAnswer;
            }
            return null;
        }
        private async Task<InspectionToAnswer> RetriveSignatureFromImage(CustomImage signatureView, InspectionToAnswer inspectionAnswer)
        {
            if (inspectionAnswer != null)
            {
                if (signatureView.ImageBase64String != null)
                {
                    inspectionAnswer.InspectionAnswers[0].SignaturePath = signatureView.ImageBase64String;
                    inspectionAnswer.InspectionAnswers[0].SignatureString = signatureView.ImageBase64String;
                }
                return inspectionAnswer;
            }
            return null;


        }
        public static byte[] StreamToByteArrary(Stream input)
        {
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        private void DeleteSavedTimer()
        {
            //this.TimerText.Text = "";
            string key = "WorkOrderInspection:" + WorkorderID;
            WorkorderInspectionStorge.Storage.Delete(key);
        }
        private string ExtractValueFormSection(Grid kfm)
        {
            var context = (kfm.Children[1] as Label).BindingContext as ExistingInspections;

            string value = null;

            switch (context.ResponseTypeName)
            {
                case "Pass/Fail":

                    var gridPF = kfm.Children[2] as Grid;
                    var btn1PF = gridPF.Children[0] as Button;
                    var btn2PF = gridPF.Children[1] as Button;
                    value = "";
                    if (btn1PF.BackgroundColor != Color.Gray)
                    {
                        value = btn1PF.Text;
                    }
                    if (btn2PF.BackgroundColor != Color.Gray)
                    {
                        value = btn2PF.Text;
                    }
                    break;
                //value = (kfm.Children[2] as Button).Text;
                //break;

                case "Standard Range":
                    value = (kfm.Children[2] as Entry).Text;
                    break;

                case "Yes/No/N/A":
                    //value = (kfm.Children[2] as Button).Text;

                    var grid = kfm.Children[2] as Grid;
                    var btn1 = grid.Children[0] as Button;
                    var btn2 = grid.Children[1] as Button;
                    var btn3 = grid.Children[2] as Button;
                    value = "";
                    if (btn1.BackgroundColor != Color.Gray)
                    {
                        value = btn1.Text;
                    }
                    if (btn2.BackgroundColor != Color.Gray)
                    {
                        value = btn2.Text;
                    }

                    //TODO: add the btn3 which is NA button and add the value.
                    if (btn3.BackgroundColor != Color.Gray)
                    {
                        value = btn3.Text;
                    }

                    break;


                case "Count":
                    value = (kfm.Children[2] as Entry).Text;
                    break;
                case "Text":
                    value = (kfm.Children[2] as Editor).Text;
                    break;

                case "Multiple Choice":
                    var index3 = (kfm.Children[2] as Picker).SelectedIndex;
                    if (index3 == -1)
                    {
                        value = null;
                    }
                    else
                    {
                        value = (kfm.Children[2] as Picker).Items[index3];
                    }

                    break;
            }

            return value;
        }
        private void PickerInspectionStartDate_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.DatePrompt(new DatePromptConfig { IsCancellable = true, OnAction = (result) => SetStartDate(result, sender, e), SelectedDate = InspectionStartDate, MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone) });
        }
        private void PickerInspectionCompletionDate_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.DatePrompt(new DatePromptConfig { IsCancellable = true, OnAction = (result) => SetCompletionDate(result, sender, e), SelectedDate = InspectionCompletionDate, MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone) });
        }

        private void SetCompletionDate(DatePromptResult result, object sender, EventArgs e)
        {

            if (result.Ok)
            {
                var s = sender as Button;
                if (result.SelectedDate.Date.Year == 0001)
                {
                    s.Text = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).ToString("d");
                    this.InspectionCompletionDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).Date;
                }
                else
                {
                    s.Text = result.SelectedDate.Date.ToString("d");
                    this.InspectionCompletionDate = result.SelectedDate.Date;
                }

            }

            //else
            //{
            //    var s = sender as Button;
            //    s.Text = "";
            //    this.InspectionCompletionDate = null;
            //}

        }
        //private void ClearStartDate_Clicked(object sender, EventArgs e)
        //{
        //    if(Device.Idiom==TargetIdiom.Phone)
        //    {
        //        this.PickerInspectionStartDatePhone.Text = "";
        //    }
        //    else
        //    {
        //        this.PickerInspectionStartDateTablet.Text = "";
        //    }

        //    this.InspectionStartDate = null;
        //}
        //private void ClearCompletionDate_Clicked(object sender, EventArgs e)
        //{
        //    if (Device.Idiom == TargetIdiom.Phone)
        //    {
        //        this.PickerInspectionCompletionDatePhone.Text = "";
        //    }
        //    else
        //    {
        //        this.PickerInspectionCompletionDateTablet.Text = "";
        //    }

        //    this.InspectionCompletionDate = null;
        //}


        private void SetStartDate(DatePromptResult result, object sender, EventArgs e)
        {

            if (result.Ok)
            {
                var s = sender as Button;
                if (result.SelectedDate.Date.Year == 0001)
                {
                    s.Text = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).ToString("d");
                    this.InspectionStartDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).Date;
                }
                else
                {
                    s.Text = result.SelectedDate.Date.ToString("d");
                    this.InspectionStartDate = result.SelectedDate.Date;
                }

            }

            //else
            //{
            //    var s = sender as Button;
            //    s.Text = "";
            //    this.InspectionStartDate = null;
            //}

        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            if (BindingContext is IHandleViewDisappearing viewAware)
            {
                await viewAware.OnViewDisappearingAsync(this);
            }
        }
        private void OnEmployeeRequested(object obj)
        {

            if (obj != null)
            {

                var employee = obj as EmployeeLookUp;
                this.EmployeeID = employee.EmployeeLaborCraftID;
                this.EmployeeName = ShortString.shorten(employee.EmployeeName) + "(" + employee.LaborCraftCode + ")";

                //Associate Employee to Inspection///
                Uri posturi = new Uri(AppSettings.BaseURL + "/Inspection/service/AssociateEmployeeAndContractorLaborCraftToWorkOrder");

                var payload = new Dictionary<string, string>
            {
              {"EmployeeLaborCraftID", this.EmployeeID.ToString()},
              {"WorkorderID", WorkorderID.ToString()},
               {"ContractorLaborCraftID","0"}
            };

                string strPayload = JsonConvert.SerializeObject(payload);
                HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
                var t = Task.Run(() => SendURI(posturi, c));

                //UserDialogs.Instance.HideLoading();
                //MainLayout.Children.Clear();
                ParentLayout.Children.Clear();
                OnAppearingOld();

            }


        }

        private void OnContractorRequested(object obj)
        {

            if (obj != null)
            {

                var contractor = obj as ContractorLookUp;
                this.ContractorID = contractor.ContractorLaborCraftID;
                this.ContractorName = ShortString.shorten(contractor.ContractorName) + "(" + contractor.LaborCraftCode + ")";  //Associate Employee to Inspection///


                //Associate Contractor to Inspection///
                Uri posturi = new Uri(AppSettings.BaseURL + "/Inspection/service/AssociateEmployeeAndContractorLaborCraftToWorkOrder");

                var payload = new Dictionary<string, string>
            {
              {"EmployeeLaborCraftID", "0"},
              {"WorkorderID", WorkorderID.ToString()},
               {"ContractorLaborCraftID",this.ContractorID.ToString()}
            };

                string strPayload = JsonConvert.SerializeObject(payload);
                HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
                var t = Task.Run(() => SendURI(posturi, c));

                //UserDialogs.Instance.HideLoading();
                //MainLayout.Children.Clear();
                ParentLayout.Children.Clear();
                 OnAppearingOld();

            }
        }
        static async Task SendURI(Uri u, HttpContent c)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = u,
                    Content = c
                };

                HttpResponseMessage result = await client.SendAsync(request);
                if (result.IsSuccessStatusCode)
                {
                    response = result.StatusCode.ToString();
                }
            }

        }
    }
}

        
        
    




       

    public class Range
    {
        public decimal? MaxRange { get; set; }
        public decimal? MinRange { get; set; }
    }
    public class InspectionTimer
    {
        public int? WorkorderID { get; set; }
        public DateTime? InspectionStartTime { get; set; }
        public DateTime? InspectionStopTime { get; set; }
        public TimeSpan TotalRunningTime { get; set; }
        public bool IsTimerRunning { get; set; }

    }

