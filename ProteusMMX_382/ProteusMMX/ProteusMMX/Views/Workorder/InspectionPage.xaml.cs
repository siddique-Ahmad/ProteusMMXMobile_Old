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
using ProteusMMX.Services.Dialog;
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
        string EmployeecontrcatorRights;
        string InspectionRights;
        StackLayout layoutdelete = new StackLayout();
        StackLayout layout1 = new StackLayout();
        StackLayout FinalLayout = new StackLayout();
        private readonly IRequestService _requestService;
        Label TotalInspectionTimeHours;
        Label TotalInspectionTimeMinutes;
        TimeSpan total;
        Label TotalInspectionTime;
        Button btnCreateWorkorder;
        double totalTime;
        Button btnAddInspection;
        Button btnAddEmployee;
        Button btnAddContractor;
        //Task<ServiceOutput> flInput;
        public int WorkorderID { get; set; }
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
        bool _viewTextIsEnable = false;
        public bool ViewTextIsEnable
        {
            get
            {
                return _viewTextIsEnable;
            }

            set
            {
                if (value != _viewTextIsEnable)
                {
                    _viewTextIsEnable = value;
                    OnPropertyChanged(nameof(ViewTextIsEnable));
                }
            }
        }

        bool _employeeContractorIsDeleted = false;
        public bool EmployeeContractorIsDeleted
        {
            get
            {
                return _employeeContractorIsDeleted;
            }

            set
            {
                if (value != _employeeContractorIsDeleted)
                {
                    _employeeContractorIsDeleted = value;
                    OnPropertyChanged(nameof(EmployeeContractorIsDeleted));
                }
            }
        }

        bool _isOnAppearingCalled = false;
        public bool IsOnAppearingCalled
        {
            get
            {
                return _isOnAppearingCalled;
            }

            set
            {
                if (value != _isOnAppearingCalled)
                {
                    _isOnAppearingCalled = value;
                    OnPropertyChanged(nameof(IsOnAppearingCalled));
                }
            }
        }
        FormListButton CreateWorkorderRights;
        public bool CreateWorkorderButtonColor;

        //TimeZone = AppSettings.UserTimeZone,
        //CultureName = AppSettings.UserCultureName,
        //UserId = Convert.ToInt32(UserID),
        //ClientIANATimeZone = AppSettings.ClientIANATimeZone,

        public string UserId = AppSettings.User.UserID.ToString();
        string ServerTimeZone = AppSettings.User.ServerIANATimeZone;
        string UserTimeZone = AppSettings.ClientIANATimeZone;
        List<int?> workOrderInspectionTimeID = new List<int?>();
        public ServiceOutput inspectionTime { get; set; }
        public DateTime? InspectionCompletionDate { get; set; }
        public DateTime? InspectionStartDate { get; set; }

        public DateTime? MinimumInspectionStartDate { get; set; }

        public DateTime? MaximumInspectionStartDate { get; set; }

        public DateTime? MaximumInspectionCompletionDate { get; set; }

        public DateTime? MinimumInspectionCompletionDate { get; set; }

        public string MaximumInspectionCompletionDateforNull { get; set; }

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

        public InspectionPage()
        {

            InitializeComponent();

            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.FromHex("#85C1E9");
            ((NavigationPage)Application.Current.MainPage).BarTextColor = Color.Black;
            this.Title = WebControlTitle.GetTargetNameByTitleName("Inspection");
            if (Application.Current.Properties.ContainsKey("AssociateInspection"))
            {
                var AssociateInspectionRightsExpression = Application.Current.Properties["AssociateInspection"].ToString();
                if (AssociateInspectionRightsExpression != null)
                {
                    InspectionRights = AssociateInspectionRightsExpression.ToString();

                }
            }
            if (Application.Current.Properties.ContainsKey("AssociateEmployeeContr"))
            {
                var AssociateEmployeeContrRightsExpression = Application.Current.Properties["AssociateEmployeeContr"].ToString();
                if (AssociateEmployeeContrRightsExpression != null)
                {
                    EmployeecontrcatorRights = AssociateEmployeeContrRightsExpression.ToString();

                }
            }

        }

        InspectionPageViewModel ViewModel => this.BindingContext as InspectionPageViewModel;
        protected override async void OnAppearing()
        {
            UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
            await Task.Delay(3000);
            base.OnAppearing();

            this.WorkorderID = ViewModel.WorkorderID;
            ServiceOutput taskandlabourList = await ViewModel._taskAndLabourService.WorkOrderLaborsByWorkOrderID(UserID, WorkorderID.ToString());
            if (taskandlabourList != null && taskandlabourList.workOrderWrapper != null && taskandlabourList.workOrderWrapper.workOrderLabors != null && taskandlabourList.workOrderWrapper.workOrderLabors.Count > 0)
            {
                ViewModel.DisabledText = WebControlTitle.GetTargetNameByTitleName("ThisTabisDisabled");
                ViewModel.DisabledTextIsEnable = true;
                await ViewModel.SetTitlesPropertiesForPage();
                ParentLayout.IsVisible = false;
                MainLayout.IsVisible = false;
                UserDialogs.Instance.HideLoading();
                return;
            }
            if (!IsPickerDataSubscribed)
            {

                //Retrive Emloyee
                MessagingCenter.Subscribe<object>(this, MessengerKeys.EmployeeRequested_AddInspection, OnEmployeeRequested);

                //Retrive Contractor
                MessagingCenter.Subscribe<object>(this, MessengerKeys.ContractorRequested_AddInspection, OnContractorRequested);


                IsPickerDataSubscribed = true;
            }


            if (BindingContext is IHandleViewAppearing viewAware)
            {
                await viewAware.OnViewAppearingAsync(this);
            }


            //await OnAppearingOld();

            ///Rights for Add Inspection
            ///


            if (InspectionRights == "E")
            {

                btnAddInspection = new Button
                {
                    Text = WebControlTitle.GetTargetNameByTitleName("AddInspection"),
                    BackgroundColor = Color.FromHex("#87CEFA"),
                    BorderColor = Color.Black,
                    IsVisible = true,
                    CornerRadius = 5,
                    TextColor = Color.White

                };
            }
            else if (InspectionRights == "V")
            {
                btnAddInspection = new Button
                {
                    Text = WebControlTitle.GetTargetNameByTitleName("AddInspection"),
                    BackgroundColor = Color.FromHex("#87CEFA"),
                    BorderColor = Color.Black,
                    IsEnabled = false,
                    CornerRadius = 5,
                    TextColor = Color.White

                };
            }
            else
            {
                btnAddInspection = new Button
                {
                    Text = WebControlTitle.GetTargetNameByTitleName("AddInspection"),
                    BackgroundColor = Color.FromHex("#87CEFA"),
                    BorderColor = Color.Black,
                    IsVisible = false,
                    CornerRadius = 5,
                    TextColor = Color.White

                };
            }

            ///Rights for Employee and Contractor
            if (EmployeecontrcatorRights == "E")
            {
                btnAddEmployee = new Button
                {
                    Text = WebControlTitle.GetTargetNameByTitleName("AddEmployee"),
                    BackgroundColor = Color.FromHex("#87CEFA"),
                    IsEnabled = false,
                    BorderColor = Color.Black,
                    CornerRadius = 5,
                    TextColor = Color.White
                };

                btnAddContractor = new Button
                {
                    Text = WebControlTitle.GetTargetNameByTitleName("AddContractor"),
                    BackgroundColor = Color.FromHex("#87CEFA"),
                    IsEnabled = false,
                    CornerRadius = 5,
                    BorderColor = Color.Black,
                    TextColor = Color.White
                };
            }
            else if (EmployeecontrcatorRights == "V")
            {
                btnAddEmployee = new Button
                {
                    Text = WebControlTitle.GetTargetNameByTitleName("AddEmployee"),
                    BackgroundColor = Color.FromHex("#87CEFA"),
                    IsEnabled = false,
                    CornerRadius = 5,
                    BorderColor = Color.Black,
                    TextColor = Color.White
                };

                btnAddContractor = new Button
                {
                    Text = WebControlTitle.GetTargetNameByTitleName("AddContractor"),
                    BackgroundColor = Color.FromHex("#87CEFA"),
                    IsEnabled = false,
                    CornerRadius = 5,
                    BorderColor = Color.Black,
                    TextColor = Color.White
                };
            }
            else
            {
                btnAddEmployee = new Button
                {
                    Text = WebControlTitle.GetTargetNameByTitleName("AddEmployee"),
                    BackgroundColor = Color.FromHex("#87CEFA"),
                    IsVisible = false,
                    CornerRadius = 5,
                    BorderColor = Color.Black,
                    TextColor = Color.White
                };

                btnAddContractor = new Button
                {
                    Text = WebControlTitle.GetTargetNameByTitleName("AddContractor"),
                    BackgroundColor = Color.FromHex("#87CEFA"),
                    IsVisible = false,
                    CornerRadius = 5,
                    BorderColor = Color.Black,
                    TextColor = Color.White
                };
            }
            if (Device.Idiom == TargetIdiom.Phone)
            {
                btnAddInspection.WidthRequest = 90;
                btnAddInspection.HeightRequest = 50;
                btnAddInspection.FontSize = 9;

                btnAddEmployee.WidthRequest = 90;
                btnAddEmployee.HeightRequest = 50;
                btnAddEmployee.FontSize = 9;

                btnAddContractor.WidthRequest = 90;
                btnAddContractor.HeightRequest = 50;
                btnAddContractor.FontSize = 9;

            }


            btnAddInspection.Clicked += (sender, e) =>
            {
                var page = new AddInspectionData(WorkorderID);
                Navigation.PushAsync(page);

            };
            TotalInspectionTime = new Label();
            TotalInspectionTimeHours = new Label();
            TotalInspectionTimeMinutes = new Label();
            btnAddEmployee.Clicked += (sender, e) =>
            {
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.WORKORDERID = this.WorkorderID;
                tnobj.Type = "Inspection";
                ViewModel._navigationService.NavigateToAsync<EmployeeListSelectionPageViewModel>(tnobj);

            };
            btnAddContractor.Clicked += (sender, e) =>
            {
                TargetNavigationData tnobj = new TargetNavigationData();
                tnobj.WORKORDERID = this.WorkorderID;
                tnobj.Type = "Inspection";
                ViewModel._navigationService.NavigateToAsync<ContractorListSelectionPageViewModel>(tnobj);

            };
            btnCreateWorkorder = new Button();

            if (this.btnCreateWorkorder.IsVisible)
            {
                if (Device.Idiom == TargetIdiom.Phone)
                {

                    btnCreateWorkorder = new Button
                    {
                        Text = WebControlTitle.GetTargetNameByTitleName("CreateWorkOrder"),
                        BackgroundColor = Color.FromHex("#87CEFA"),
                        BorderColor = Color.Black,
                        WidthRequest = 90,
                        HeightRequest = 50,
                        CornerRadius = 5,
                        FontSize = 9,
                        TextColor = Color.White

                    };


                }
                else
                {
                    btnCreateWorkorder = new Button
                    {
                        Text = WebControlTitle.GetTargetNameByTitleName("CreateWorkOrder"),
                        BackgroundColor = Color.FromHex("#87CEFA"),
                        BorderColor = Color.Black,
                        CornerRadius = 5,
                        TextColor = Color.White

                    };
                }

            }


            btnCreateWorkorder.Clicked += (sender, e) =>
            {
                var page = new CreateWorkorderFromInspectionPageContent(WorkorderID, AnswerText);
                Navigation.PushAsync(page);


            };

            try
            {


                AnswerText.Clear();


                this.btnCreateWorkorder.IsVisible = false;
                layout1.Children.Clear();
                FinalLayout.Children.Clear();
                MainLayout.Children.Clear();
                ParentLayout.Children.Clear();
                ParentLayout.Children.Remove(FinalLayout);

                await RetriveAllWorkorderInspectionsAsync();


                //// UI for Multiple Employeee/////////////////////
                ///
                foreach (var item in CC.workOrderEmployee)
                {
                    string FinalEmployeeName = string.Empty;
                    Label taskNumberLabel = new Label { TextColor = Color.Black };

                    taskNumberLabel.Text = WebControlTitle.GetTargetNameByTitleName("EmployeeName") + ": " + item.EmployeeName + "   ";
                    Button startButton = new Button();
                    Button stopButton = new Button();
                    if (Device.Idiom == TargetIdiom.Phone || Device.Idiom == TargetIdiom.Tablet)
                    {
                        startButton = new Button
                        {
                            Text = WebControlTitle.GetTargetNameByTitleName("Start"),
                            BackgroundColor = Color.FromHex("#87CEFA"),
                            CommandParameter = item,
                            WidthRequest = 60,
                            HeightRequest = 20,
                            CornerRadius = 5,
                            FontSize = 9,
                            BorderColor = Color.Black,
                            TextColor = Color.White
                        };
                    }
                    else
                    {
                        startButton = new Button
                        {
                            Text = WebControlTitle.GetTargetNameByTitleName("Start"),
                            BackgroundColor = Color.FromHex("#87CEFA"),
                            CommandParameter = item,
                            CornerRadius = 5,
                            BorderColor = Color.Black,
                            TextColor = Color.White

                        };
                    }

                    if (Device.Idiom == TargetIdiom.Phone || Device.Idiom == TargetIdiom.Tablet)
                    {

                        stopButton = new Button
                        {
                            Text = WebControlTitle.GetTargetNameByTitleName("Stop"),
                            BackgroundColor = Color.FromHex("#87CEFA"),
                            CommandParameter = item,
                            WidthRequest = 60,
                            HeightRequest = 20,
                            FontSize = 9,
                            CornerRadius = 5,
                            BorderColor = Color.Black,
                            TextColor = Color.White
                        };
                    }
                    else
                    {
                        stopButton = new Button
                        {
                            Text = WebControlTitle.GetTargetNameByTitleName("Stop"),
                            BackgroundColor = Color.FromHex("#87CEFA"),
                            CommandParameter = item,
                            CornerRadius = 5,
                            BorderColor = Color.Black,
                            TextColor = Color.White
                        };
                    }
                    CustomDatePicker startDate;
                    if (item.StartDate != null)
                    {
                        startDate = new CustomDatePicker
                        {
                            SelectedDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.StartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone),
                            MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                            HeightRequest = 2,

                            HorizontalOptions = LayoutOptions.Start

                        };
                    }
                    else
                    {
                        startDate = new CustomDatePicker
                        {
                            // SelectedDate = Convert.ToDateTime(item.StartDate),
                            //  BackgroundColor = Color.FromHex("#87CEFA"),
                            MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                            HeightRequest = 2,
                           // CornerRadius = 5,
                            HorizontalOptions = LayoutOptions.Start

                        };
                    }
                    CustomDatePicker CompletionDate;
                    if (item.CompletionDate != null)
                    {
                        CompletionDate = new CustomDatePicker
                        {
                            SelectedDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone),
                            MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                            HeightRequest = 2,
                            //CornerRadius = 5,
                            HorizontalOptions = LayoutOptions.Start

                        };
                    }
                    else
                    {
                        CompletionDate = new CustomDatePicker
                        {
                            //  SelectedDate = Convert.ToDateTime(item.CompletionDate),
                            MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                            HeightRequest = 2,
                          //  CornerRadius = 5,
                            HorizontalOptions = LayoutOptions.Start

                        };

                    }


                    //if (this.btnCreateWorkorder.IsVisible)
                    //{
                    //    if (Device.Idiom == TargetIdiom.Phone)
                    //    {

                    //        btnCreateWorkorder = new Button
                    //        {
                    //            Text = WebControlTitle.GetTargetNameByTitleName("CreateWorkOrder"),
                    //            BackgroundColor = Color.FromHex("#87CEFA"),
                    //            BorderColor = Color.Black,
                    //            WidthRequest = 90,
                    //            HeightRequest = 50,
                    //            FontSize = 9,
                    //            TextColor = Color.White

                    //        };


                    //    }
                    //    else
                    //    {
                    //        btnCreateWorkorder = new Button
                    //        {
                    //            Text = WebControlTitle.GetTargetNameByTitleName("CreateWorkOrder"),
                    //            BackgroundColor = Color.FromHex("#87CEFA"),
                    //            BorderColor = Color.Black,
                    //            TextColor = Color.White

                    //        };
                    //    }

                    //}


                    //btnCreateWorkorder.Clicked += (sender, e) =>
                    //{
                    //    var page = new CreateWorkorderFromInspectionPageContent(WorkorderID, AnswerText);
                    //    Navigation.PushAsync(page);


                    //};


                    Entry hoursEntry = new MyEntry { TextColor = Color.Black, Placeholder = "hh" };
                    Entry minuteEntry = new MyEntry { TextColor = Color.Black, Placeholder = "mm", };
                    hoursEntry.TextChanged += OnTextChanged1;
                    minuteEntry.TextChanged += HoursTextChanged1;

                    #region GlobalTimer Logic
                    WorkOrderEmployee savedemployeelocal = null;

                    try
                    {


                        string k1 = "WorkOrderEmployee:" + item.WorkOrderInspectionTimeID;
                        savedemployeelocal = JsonConvert.DeserializeObject<WorkOrderEmployee>(WorkorderInspectionStorge.Storage.Get(k1));



                    }
                    catch (Exception ex)
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
                        string key = "WorkOrderEmployee:" + workorderemployee.WorkOrderInspectionTimeID;
                        // workorderempcontrcator.Description = "";
                        //if (Device.RuntimePlatform == Device.Android)
                        //{
                        //    WorkOrderLaborStorge.Storage.Set(key, JsonConvert.SerializeObject(workorderemployee));

                        //}
                        //else
                        //{
                        WorkorderInspectionStorge.Storage.Set(key, JsonConvert.SerializeObject(workorderemployee));
                        // }


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
                        workOrderInspectionTimeID.Add(workOrderemp.WorkOrderInspectionTimeID);


                        if (workOrderemp.StartTimeOfTimer == DateTime.Parse("1/1/0001 12:00:00 AM"))
                        {
                            return;
                        }

                        TimeSpan elapsed = StopTime.Subtract(workOrderemp.StartTimeOfTimer);

                        int mn = elapsed.Minutes;
                        if (String.IsNullOrWhiteSpace(minuteEntry.Text))
                        {
                            minuteEntry.Text = "0";
                        }
                        if (String.IsNullOrWhiteSpace(hoursEntry.Text))
                        {
                            hoursEntry.Text = "0";
                        }
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
                        //startDate.SelectedDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(DateTime.Now.TimeOfDay).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                        //CompletionDate.SelectedDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(DateTime.Now.TimeOfDay).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                        // startDate.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString());
                        //CompletionDate.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString());
                        startDate.SelectedDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);
                        CompletionDate.SelectedDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);

                        stopButton.BackgroundColor = BackgroundColor = Color.FromHex("#708090");
                        stopButton.IsEnabled = false;
                        startButton.BackgroundColor = BackgroundColor = Color.FromHex("#708090");
                        startButton.IsEnabled = false;
                        this.BackgroundColor = Color.White;


                    };


                    if (ParentLayout.Children.Count > 4)
                    {
                        break;
                    }


                    if (!string.IsNullOrWhiteSpace(item.InspectionTime))
                    {
                        var timeInspection = TimeSpan.FromSeconds(Convert.ToDouble(item.InspectionTime));
                        var timeString = (int)timeInspection.TotalHours + ":" + timeInspection.Minutes + ":" + timeInspection.Seconds;


                        string FinalHours1 = Convert.ToDecimal(string.Format("{0:F2}", timeInspection.TotalHours)).ToString();
                        var FinalHrs2 = FinalHours1.Split('.');
                        hoursEntry.Text = FinalHrs2[0];
                        //hoursEntry.Text = timeInspection.Hours.ToString();
                        minuteEntry.Text = timeInspection.Minutes.ToString();


                        total = total.Add(new TimeSpan(int.Parse(hoursEntry.Text), int.Parse(minuteEntry.Text), 0));
                        string FinalHours = Convert.ToDecimal(string.Format("{0:F2}", total.TotalHours)).ToString();
                        var FinalHrs1 = FinalHours.Split('.');
                        string DisplayHours = FinalHrs1[0];

                        TotalInspectionTime.Text = DisplayHours + ":" + total.Minutes;


                    }


                    if (Device.Idiom == TargetIdiom.Phone)
                    {
                        var startStopButtonGridforPhone1 = new Grid();
                        startStopButtonGridforPhone1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridforPhone1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridforPhone1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridforPhone1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridforPhone1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridforPhone1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

                        startStopButtonGridforPhone1.Children.Add(startButton, 1, 0);
                        startStopButtonGridforPhone1.Children.Add(stopButton, 2, 0);
                        startStopButtonGridforPhone1.Children.Add(new Label { FontSize = 10, Text = WebControlTitle.GetTargetNameByTitleName("hrs"), VerticalOptions = LayoutOptions.Center }, 3, 0);
                        startStopButtonGridforPhone1.Children.Add(hoursEntry, 4, 0);
                        startStopButtonGridforPhone1.Children.Add(new Label { FontSize = 10, Text = WebControlTitle.GetTargetNameByTitleName("Min"), VerticalOptions = LayoutOptions.Center }, 5, 0);
                        startStopButtonGridforPhone1.Children.Add(minuteEntry, 6, 0);
                        var btnDelete = new Button() { CornerRadius = 5, WidthRequest = 30, HeightRequest = 20, FontSize = 10, StyleId = item.EmployeeLaborCraftID.ToString(), Text = WebControlTitle.GetTargetNameByTitleName("Delete"), BackgroundColor = Color.FromHex("#87CEFA"), TextColor = Color.White, BorderColor = Color.Black, };
                        btnDelete.Clicked += BtnEmployeeDelete_Clicked;
                        startStopButtonGridforPhone1.Children.Add(btnDelete, 7, 0);


                        var startStopButtonGridforPhone2 = new Grid();
                        startStopButtonGridforPhone2.HeightRequest = 120;
                        startDate.HeightRequest = 3;
                        CompletionDate.HeightRequest = 3;
                        startStopButtonGridforPhone2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridforPhone2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridforPhone2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridforPhone2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridforPhone2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

                        startStopButtonGridforPhone2.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("StartDate"), VerticalOptions = LayoutOptions.Center }, 0, 1);
                        startStopButtonGridforPhone2.Children.Add(startDate, 1, 1);
                        startStopButtonGridforPhone2.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("CompletionDate"), VerticalOptions = LayoutOptions.Center }, 0, 2);
                        startStopButtonGridforPhone2.Children.Add(CompletionDate, 1, 2);
                        startStopButtonGridforPhone2.Children.Add(new Label { Text = item.EmployeeLaborCraftID.ToString() + ":" + "EmployeeLaborCraft", IsVisible = false, BackgroundColor = Color.FromHex("#87CEFA"), }, 0, 3);


                        layout1 = new StackLayout
                        {
                            Orientation = StackOrientation.Vertical,
                            Spacing = 10,
                            HorizontalOptions = LayoutOptions.StartAndExpand,
                            Children =
                                {
                                 new Label{ Text=taskNumberLabel.Text,FontAttributes=FontAttributes.Bold},startStopButtonGridforPhone1,startStopButtonGridforPhone2
                                }
                        };

                    }
                    else
                    {
                        var startStopButtonGrid = new Grid();
                        startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        if (Device.Idiom == TargetIdiom.Tablet)
                        {
                            startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                            startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                            startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                            startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

                        }
                        else
                        {
                            startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
                            startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                            startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
                            startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        }


                        if (Device.Idiom == TargetIdiom.Tablet)
                        {
                            //var btnDelete = new Button() { FontSize=9, HeightRequest = 20, WidthRequest = 60, StyleId = item.EmployeeLaborCraftID.ToString(), Text = WebControlTitle.GetTargetNameByTitleName("Delete"), BackgroundColor = Color.FromHex("#87CEFA"), TextColor = Color.White, BorderColor = Color.Black, };
                            //btnDelete.Clicked += BtnEmployeeDelete_Clicked;

                            //startStopButtonGrid.Children.Add(startButton, 1, 0);
                            //startStopButtonGrid.Children.Add(stopButton, 2, 0);

                            //startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("hrs"), VerticalOptions = LayoutOptions.Center }, 3, 0);
                            //startStopButtonGrid.Children.Add(hoursEntry, 4, 0);
                            //startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("Min"), VerticalOptions = LayoutOptions.Center }, 5, 0);
                            //startStopButtonGrid.Children.Add(minuteEntry, 6, 0);
                            //startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("StartDate") }, 7, 0);
                            //startStopButtonGrid.Children.Add(startDate, 8, 0);
                            //startStopButtonGrid.Children.Add(new Label {Text = WebControlTitle.GetTargetNameByTitleName("CompletionDate")}, 9, 0);
                            //startStopButtonGrid.Children.Add(CompletionDate, 10, 0);
                            //startStopButtonGrid.Children.Add(btnDelete, 11, 0);
                            var btnDelete = new Button() { CornerRadius = 5, FontSize = 9, HeightRequest = 10, WidthRequest = 70, StyleId = item.EmployeeLaborCraftID.ToString(), Text = WebControlTitle.GetTargetNameByTitleName("Delete"), BackgroundColor = Color.FromHex("#87CEFA"), TextColor = Color.White, BorderColor = Color.Black, };
                            btnDelete.Clicked += BtnEmployeeDelete_Clicked;


                            startStopButtonGrid.Children.Add(startButton, 0, 1);
                            startStopButtonGrid.Children.Add(stopButton, 1, 1);
                            startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("hrs"), VerticalOptions = LayoutOptions.Center }, 3, 0);
                            startStopButtonGrid.Children.Add(hoursEntry, 3, 1);
                            startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("Min"), VerticalOptions = LayoutOptions.Center }, 5, 0);
                            startStopButtonGrid.Children.Add(minuteEntry, 5, 1);
                            startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("StartDate"), VerticalOptions = LayoutOptions.Center }, 6, 0);
                            startStopButtonGrid.Children.Add(startDate, 6, 1);
                            startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("CompletionDate"), VerticalOptions = LayoutOptions.Center }, 7, 0);
                            startStopButtonGrid.Children.Add(CompletionDate, 7, 1);
                            startStopButtonGrid.Children.Add(btnDelete, 9, 1);

                        }
                        else
                        {
                            var btnDelete = new Button() { CornerRadius = 5, StyleId = item.EmployeeLaborCraftID.ToString(), Text = WebControlTitle.GetTargetNameByTitleName("Delete"), HorizontalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Color.FromHex("#87CEFA"), TextColor = Color.White, BorderColor = Color.Black, };
                            btnDelete.Clicked += BtnEmployeeDelete_Clicked;

                            startStopButtonGrid.Children.Add(startButton, 1, 0);
                            startStopButtonGrid.Children.Add(stopButton, 2, 0);
                            startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("hrs"), VerticalOptions = LayoutOptions.Center }, 3, 0);
                            startStopButtonGrid.Children.Add(hoursEntry, 4, 0);
                            startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("Min"), VerticalOptions = LayoutOptions.Center }, 5, 0);
                            startStopButtonGrid.Children.Add(minuteEntry, 6, 0);
                            startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("StartDate"), VerticalOptions = LayoutOptions.Center }, 7, 0);
                            startStopButtonGrid.Children.Add(startDate, 8, 0);
                            startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("CompletionDate"), VerticalOptions = LayoutOptions.Center }, 9, 0);
                            startStopButtonGrid.Children.Add(CompletionDate, 10, 0);
                            startStopButtonGrid.Children.Add(new Label { Text = item.EmployeeLaborCraftID.ToString() + ":" + "EmployeeLaborCraft", IsVisible = false, BackgroundColor = Color.FromHex("#87CEFA"), }, 11, 0);
                            startStopButtonGrid.Children.Add(btnDelete, 12, 0);



                        }


                        layout1 = new StackLayout
                        {
                            Orientation = StackOrientation.Vertical,
                            Spacing = 10,

                            HorizontalOptions = LayoutOptions.StartAndExpand,
                            Children =
                                {
                                 new Label{StyleId=item.EmployeeLaborCraftID.ToString() + ":" + "EmployeeLaborCraft", Text=taskNumberLabel.Text,FontAttributes=FontAttributes.Bold},startStopButtonGrid
                                }
                        };

                    }

                    ParentLayout.Children.Add(layout1);

                }
                foreach (var item in CC.workorderContractor)
                {

                    string FinalContractorName = string.Empty;

                    Label taskNumberLabel = new Label { TextColor = Color.Black };

                    taskNumberLabel.Text = WebControlTitle.GetTargetNameByTitleName("ContractorName") + ": " + item.ContractorName + "   ";
                    Button startButton = new Button();
                    Button stopButton = new Button();
                    if (Device.Idiom == TargetIdiom.Phone || Device.Idiom == TargetIdiom.Tablet)
                    {
                        startButton = new Button
                        {
                            Text = WebControlTitle.GetTargetNameByTitleName("Start"),
                            BackgroundColor = Color.FromHex("#87CEFA"),
                            CommandParameter = item,
                            WidthRequest = 60,
                            HeightRequest = 20,
                            FontSize = 9,
                            CornerRadius = 5,
                            BorderColor = Color.Black,
                            TextColor = Color.White
                        };
                    }
                    else
                    {
                        startButton = new Button
                        {
                            Text = WebControlTitle.GetTargetNameByTitleName("Start"),
                            BackgroundColor = Color.FromHex("#87CEFA"),
                            CommandParameter = item,
                            CornerRadius = 5,
                            BorderColor = Color.Black,
                            TextColor = Color.White

                        };
                    }

                    if (Device.Idiom == TargetIdiom.Phone || Device.Idiom == TargetIdiom.Tablet)
                    {

                        stopButton = new Button
                        {
                            Text = WebControlTitle.GetTargetNameByTitleName("Stop"),
                            BackgroundColor = Color.FromHex("#87CEFA"),
                            CommandParameter = item,
                            WidthRequest = 60,
                            CornerRadius = 5,
                            HeightRequest = 20,
                            FontSize = 9,
                            BorderColor = Color.Black,
                            TextColor = Color.White
                        };
                    }
                    else
                    {
                        stopButton = new Button
                        {
                            Text = WebControlTitle.GetTargetNameByTitleName("Stop"),
                            BackgroundColor = Color.FromHex("#87CEFA"),
                            CommandParameter = item,
                            CornerRadius = 5,
                            BorderColor = Color.Black,
                            TextColor = Color.White
                        };
                    }

                    CustomDatePicker startDate;
                    if (item.StartDate != null)
                    {
                        startDate = new CustomDatePicker
                        {

                            SelectedDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.StartDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone),
                            MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                            HeightRequest = 2,
                            //CornerRadius = 5,
                            HorizontalOptions = LayoutOptions.Start

                        };
                    }
                    else
                    {
                        startDate = new CustomDatePicker
                        {
                            MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                            HeightRequest = 2,
                           // CornerRadius = 5,
                            HorizontalOptions = LayoutOptions.Start

                        };
                    }
                    CustomDatePicker CompletionDate;
                    if (item.CompletionDate != null)
                    {
                        CompletionDate = new CustomDatePicker
                        {
                            SelectedDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone),
                            MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                            HeightRequest = 2,
                           // CornerRadius = 5,
                            HorizontalOptions = LayoutOptions.Start

                        };
                    }
                    else
                    {
                        CompletionDate = new CustomDatePicker
                        {
                            //SelectedDate = Convert.ToDateTime(item.CompletionDate),
                            MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                            HeightRequest = 2,
                           // CornerRadius = 5,
                            HorizontalOptions = LayoutOptions.Start

                        };
                    }



                    //if (this.btnCreateWorkorder.IsVisible)
                    //{
                    //    if (Device.Idiom == TargetIdiom.Phone)
                    //    {

                    //        btnCreateWorkorder = new Button
                    //        {
                    //            Text = WebControlTitle.GetTargetNameByTitleName("CreateWorkOrder"),
                    //            BackgroundColor = Color.FromHex("#87CEFA"),
                    //            BorderColor = Color.Black,
                    //            WidthRequest = 90,
                    //            HeightRequest = 50,
                    //            FontSize = 9,
                    //            TextColor = Color.White

                    //        };


                    //    }
                    //    else
                    //    {
                    //        btnCreateWorkorder = new Button
                    //        {
                    //            Text = WebControlTitle.GetTargetNameByTitleName("CreateWorkOrder"),
                    //            BackgroundColor = Color.FromHex("#87CEFA"),
                    //            BorderColor = Color.Black,
                    //            TextColor = Color.White

                    //        };
                    //    }
                    //}

                    //btnCreateWorkorder.Clicked += (sender, e) =>
                    //{
                    //    var page = new CreateWorkorderFromInspectionPageContent(WorkorderID, AnswerText);
                    //    Navigation.PushAsync(page);


                    //};
                    Entry hoursEntry = new MyEntry { TextColor = Color.Black, Placeholder = "hh" };
                    Entry minuteEntry = new MyEntry { TextColor = Color.Black, Placeholder = "mm", };
                    hoursEntry.TextChanged += OnTextChanged1;
                    minuteEntry.TextChanged += HoursTextChanged1;

                    #region GlobalTimer Logic
                    WorkorderContractor savedContractorlocal = null;

                    try
                    {

                        string k1 = "WorkorderContracator:" + item.WorkOrderInspectionTimeID;
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
                        string key = "WorkorderContracator:" + workordercontrcator.WorkOrderInspectionTimeID;
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
                        workOrderInspectionTimeID.Add(workOrdercontrcator.WorkOrderInspectionTimeID);

                        if (workOrdercontrcator.StartTimeOfTimer == DateTime.Parse("1/1/0001 12:00:00 AM"))
                        {
                            return;
                        }

                        TimeSpan elapsed = StopTime.Subtract(workOrdercontrcator.StartTimeOfTimer);

                        int mn = elapsed.Minutes;
                        if (String.IsNullOrWhiteSpace(minuteEntry.Text))
                        {
                            minuteEntry.Text = "0";
                        }
                        if (String.IsNullOrWhiteSpace(hoursEntry.Text))
                        {
                            hoursEntry.Text = "0";
                        }
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

                        //startDate.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString());
                        //CompletionDate.SelectedDate = Convert.ToDateTime(DateTime.Now.ToString());


                        startDate.SelectedDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);
                        CompletionDate.SelectedDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone);


                        //  startDate.SelectedDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(DateTime.Now.TimeOfDay).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                        // CompletionDate.SelectedDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(DateTime.Now.TimeOfDay).ToUniversalTime(), AppSettings.User.ServerIANATimeZone);
                        stopButton.BackgroundColor = BackgroundColor = Color.FromHex("#708090");
                        stopButton.IsEnabled = false;
                        startButton.BackgroundColor = BackgroundColor = Color.FromHex("#708090");
                        startButton.IsEnabled = false;
                        this.BackgroundColor = Color.White;


                    };
                    if (ParentLayout.Children.Count > 4)
                    {
                        break;
                    }
                    if (!string.IsNullOrWhiteSpace(item.InspectionTime))
                    {
                        var timeInspection = TimeSpan.FromSeconds(Convert.ToDouble(item.InspectionTime));
                        var timeString = (int)timeInspection.Hours + ":" + timeInspection.Minutes + ":" + timeInspection.Seconds;

                        string FinalHours1 = Convert.ToDecimal(string.Format("{0:F2}", timeInspection.TotalHours)).ToString();
                        var FinalHrs2 = FinalHours1.Split('.');
                        hoursEntry.Text = FinalHrs2[0];
                        minuteEntry.Text = timeInspection.Minutes.ToString();
                        total = total.Add(new TimeSpan(int.Parse(hoursEntry.Text), int.Parse(minuteEntry.Text), 0));

                        string FinalHours = Convert.ToDecimal(string.Format("{0:F2}", total.TotalHours)).ToString();
                        var FinalHrs1 = FinalHours.Split('.');
                        string DisplayHours = FinalHrs1[0];

                        TotalInspectionTime.Text = DisplayHours + ":" + total.Minutes;
                    }


                    if (Device.Idiom == TargetIdiom.Phone)
                    {
                        var startStopButtonGridforPhone1 = new Grid();
                        startStopButtonGridforPhone1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridforPhone1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridforPhone1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridforPhone1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridforPhone1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridforPhone1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

                        startStopButtonGridforPhone1.Children.Add(startButton, 1, 0);
                        startStopButtonGridforPhone1.Children.Add(stopButton, 2, 0);
                        startStopButtonGridforPhone1.Children.Add(new Label { FontSize = 10, Text = WebControlTitle.GetTargetNameByTitleName("hrs"), VerticalOptions = LayoutOptions.Center }, 3, 0);
                        startStopButtonGridforPhone1.Children.Add(hoursEntry, 4, 0);
                        startStopButtonGridforPhone1.Children.Add(new Label { FontSize = 10, Text = WebControlTitle.GetTargetNameByTitleName("Min"), VerticalOptions = LayoutOptions.Center }, 5, 0);
                        startStopButtonGridforPhone1.Children.Add(minuteEntry, 6, 0);
                        var btnDelete = new Button() { CornerRadius = 5, WidthRequest = 30, HeightRequest = 20, FontSize = 10, StyleId = item.ContractorLaborCraftID.ToString(), Text = WebControlTitle.GetTargetNameByTitleName("Delete"), BackgroundColor = Color.FromHex("#87CEFA"), TextColor = Color.White, BorderColor = Color.Black, };
                        btnDelete.Clicked += BtnContractorDelete_Clicked;
                        startStopButtonGridforPhone1.Children.Add(btnDelete, 7, 0);


                        var startStopButtonGridforPhone2 = new Grid();
                        startStopButtonGridforPhone2.HeightRequest = 120;
                        startDate.HeightRequest = 3;
                        CompletionDate.HeightRequest = 3;
                        startStopButtonGridforPhone2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridforPhone2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridforPhone2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridforPhone2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGridforPhone2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });


                        startStopButtonGridforPhone2.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("StartDate"), VerticalOptions = LayoutOptions.Center }, 0, 1);
                        startStopButtonGridforPhone2.Children.Add(startDate, 1, 1);
                        startStopButtonGridforPhone2.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("CompletionDate"), VerticalOptions = LayoutOptions.Center }, 0, 2);
                        startStopButtonGridforPhone2.Children.Add(CompletionDate, 1, 2);
                        startStopButtonGridforPhone2.Children.Add(new Label { Text = item.ContractorLaborCraftID.ToString() + ":" + "ContractorLaborCraft", IsVisible = false, BackgroundColor = Color.FromHex("#87CEFA"), }, 0, 3);


                        layout1 = new StackLayout
                        {
                            Orientation = StackOrientation.Vertical,
                            Spacing = 10,
                            HorizontalOptions = LayoutOptions.StartAndExpand,
                            Children =
                                {
                                 new Label{ Text=taskNumberLabel.Text,FontAttributes=FontAttributes.Bold},startStopButtonGridforPhone1,startStopButtonGridforPhone2
                                }
                        };

                    }
                    else
                    {
                        var startStopButtonGrid = new Grid();
                        startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        if (Device.Idiom == TargetIdiom.Tablet)
                        {
                            startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                            startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                            startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                            startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

                        }
                        else
                        {
                            startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
                            startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                            startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
                            startStopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
                        }



                        if (Device.Idiom == TargetIdiom.Tablet)
                        {


                            //var btnDelete = new Button() { FontSize = 9, HeightRequest = 20, WidthRequest = 60, StyleId = item.ContractorLaborCraftID.ToString(), Text = WebControlTitle.GetTargetNameByTitleName("Delete"), BackgroundColor = Color.FromHex("#87CEFA"), TextColor = Color.White, BorderColor = Color.Black, };
                            //btnDelete.Clicked += BtnContractorDelete_Clicked;

                            //startStopButtonGrid.Children.Add(startButton, 1, 0);
                            //startStopButtonGrid.Children.Add(stopButton, 2, 0);

                            //startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("hrs"), VerticalOptions = LayoutOptions.Center }, 3, 0);
                            //startStopButtonGrid.Children.Add(hoursEntry, 4, 0);
                            //startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("Min"), VerticalOptions = LayoutOptions.Center }, 5, 0);
                            //startStopButtonGrid.Children.Add(minuteEntry, 6, 0);
                            //startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("StartDate")}, 7, 0);
                            //startStopButtonGrid.Children.Add(startDate, 8, 0);
                            //startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("CompletionDate")}, 9, 0);
                            //startStopButtonGrid.Children.Add(CompletionDate, 10, 0);
                            //startStopButtonGrid.Children.Add(btnDelete, 11, 0);
                            var btnDelete = new Button() { CornerRadius = 5, FontSize = 9, HeightRequest = 10, WidthRequest = 70, StyleId = item.ContractorLaborCraftID.ToString(), Text = WebControlTitle.GetTargetNameByTitleName("Delete"), BackgroundColor = Color.FromHex("#87CEFA"), TextColor = Color.White, BorderColor = Color.Black, };
                            btnDelete.Clicked += BtnContractorDelete_Clicked;


                            startStopButtonGrid.Children.Add(startButton, 0, 1);
                            startStopButtonGrid.Children.Add(stopButton, 1, 1);
                            startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("hrs"), VerticalOptions = LayoutOptions.Center }, 3, 0);
                            startStopButtonGrid.Children.Add(hoursEntry, 3, 1);
                            startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("Min"), VerticalOptions = LayoutOptions.Center }, 5, 0);
                            startStopButtonGrid.Children.Add(minuteEntry, 5, 1);
                            startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("StartDate"), VerticalOptions = LayoutOptions.Center }, 6, 0);
                            startStopButtonGrid.Children.Add(startDate, 6, 1);
                            startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("CompletionDate"), VerticalOptions = LayoutOptions.Center }, 7, 0);
                            startStopButtonGrid.Children.Add(CompletionDate, 7, 1);
                            startStopButtonGrid.Children.Add(btnDelete, 9, 1);


                        }
                        else
                        {
                            var btnDelete = new Button() { CornerRadius = 5, StyleId = item.ContractorLaborCraftID.ToString(), Text = WebControlTitle.GetTargetNameByTitleName("Delete"), HorizontalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Color.FromHex("#87CEFA"), TextColor = Color.White, BorderColor = Color.Black, };
                            btnDelete.Clicked += BtnContractorDelete_Clicked;

                            startStopButtonGrid.Children.Add(startButton, 1, 0);
                            startStopButtonGrid.Children.Add(stopButton, 2, 0);
                            startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("hrs"), VerticalOptions = LayoutOptions.Center }, 3, 0);
                            startStopButtonGrid.Children.Add(hoursEntry, 4, 0);
                            startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("Min"), VerticalOptions = LayoutOptions.Center }, 5, 0);
                            startStopButtonGrid.Children.Add(minuteEntry, 6, 0);
                            startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("StartDate"), VerticalOptions = LayoutOptions.Center }, 7, 0);
                            startStopButtonGrid.Children.Add(startDate, 8, 0);
                            startStopButtonGrid.Children.Add(new Label { Text = WebControlTitle.GetTargetNameByTitleName("CompletionDate"), VerticalOptions = LayoutOptions.Center }, 9, 0);
                            startStopButtonGrid.Children.Add(CompletionDate, 10, 0);
                            startStopButtonGrid.Children.Add(new Label { Text = item.ContractorLaborCraftID.ToString() + ":" + "ContractorLaborCraft", IsVisible = false, BackgroundColor = Color.FromHex("#87CEFA"), }, 11, 0);
                            startStopButtonGrid.Children.Add(btnDelete, 12, 0);



                        }


                        layout1 = new StackLayout
                        {
                            Orientation = StackOrientation.Vertical,
                            Spacing = 10,
                            HorizontalOptions = LayoutOptions.StartAndExpand,
                            Children =
                                {
                                 new Label{StyleId=item.ContractorLaborCraftID.ToString() + ":" + "ContractorLaborCraft", Text=taskNumberLabel.Text,FontAttributes=FontAttributes.Bold},startStopButtonGrid
                                }
                        };

                    }


                    ParentLayout.Children.Add(layout1);

                }




                var layout12 = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,

                    Spacing = 2,
                    HorizontalOptions = LayoutOptions.Start,
                    Children =
                                {
                                  btnAddInspection,btnAddEmployee,btnAddContractor,btnCreateWorkorder,
                                }
                };


                var oneBox = new BoxView { BackgroundColor = Color.Black, HeightRequest = 2, VerticalOptions = LayoutOptions.FillAndExpand, HorizontalOptions = LayoutOptions.FillAndExpand };
                FinalLayout = new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Spacing = 10,
                    Children =
                                {
                                   new Label{FontAttributes=FontAttributes.Bold, Text=WebControlTitle.GetTargetNameByTitleName("TotalInspectionTime")+"(HH:MM) " +":  "+TotalInspectionTime.Text ,TextColor=Color.Black},layout12, oneBox

                                }
                };
                ParentLayout.Children.Add(FinalLayout);
                total = total.Subtract(total);

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
            UserDialogs.Instance.HideLoading();

        }


        protected async Task OnAppearingOld()
        {

            //this.WorkorderID = ViewModel.WorkorderID;
            //ServiceOutput taskandlabourList = await ViewModel._taskAndLabourService.WorkOrderLaborsByWorkOrderID(UserID, WorkorderID.ToString());
            //if (taskandlabourList != null && taskandlabourList.workOrderWrapper != null && taskandlabourList.workOrderWrapper.workOrderLabors != null && taskandlabourList.workOrderWrapper.workOrderLabors.Count > 0)
            //{
            //    ViewModel.DisabledText = WebControlTitle.GetTargetNameByTitleName("ThisTabisDisabled");
            //    ViewModel.DisabledTextIsEnable = true;
            //   // ViewModel.ViewTextIsEnable = false;
            //    ParentLayout.IsVisible = false;
            //    MainLayout.IsVisible = false;
            //    return;
            //}


            // UserDialogs.Instance.HideLoading();

        }







        private async Task RetriveAllWorkorderInspectionsAsync()
        {

           

            CC = await ViewModel._inspectionService.GetWorkorderInspection(this.WorkorderID.ToString(), AppSettings.User.UserID.ToString());
            if (CC.listInspection == null || CC.listInspection.Count > 0)
            {
                if (EmployeecontrcatorRights == "E")
                {
                    this.btnAddEmployee.IsEnabled = true;
                    this.btnAddContractor.IsEnabled = true;
                }

            }

            if (CC.listInspection.Count == 0 && (CC.workOrderEmployee.Count > 0 || CC.workorderContractor.Count > 0))
            {

                foreach (var item in CC.workOrderEmployee)
                {
                    Uri posturi = new Uri(AppSettings.BaseURL + "/Inspection/service/DeleteEmployeeAndContrator");

                    var payload = new Dictionary<string, string>
             {
              {"EmployeeLaborCraftID", item.EmployeeLaborCraftID.ToString()},
              {"WorkorderID", WorkorderID.ToString()},
               {"ContractorLaborCraftID", "0"},

            };

                    string strPayload = JsonConvert.SerializeObject(payload);
                    HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
                    var t = Task.Run(() => SendURI(posturi, c));


                }
                foreach (var item in CC.workorderContractor)
                {
                    Uri posturi = new Uri(AppSettings.BaseURL + "/Inspection/service/DeleteEmployeeAndContrator");

                    var payload = new Dictionary<string, string>
            {
              {"EmployeeLaborCraftID", "0"},
              {"WorkorderID", WorkorderID.ToString()},
               {"ContractorLaborCraftID", item.ContractorLaborCraftID.ToString()},

            };

                    string strPayload = JsonConvert.SerializeObject(payload);
                    HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
                    var t = Task.Run(() => SendURI(posturi, c));

                }

                OnAppearing();



            }
            if (CC.listInspection.Count > 0 && CC.workOrderEmployee.Count == 0)
            {
                ServiceOutput taskResponse = null;

                taskResponse = await ViewModel._taskService.GetEmployee(UserID, "0", "0", AppSettings.User.EmployeeName, "Inspection", this.WorkorderID);

                if (taskResponse != null && taskResponse.workOrderWrapper != null && taskResponse.workOrderWrapper.workOrderLabor != null && taskResponse.workOrderWrapper.workOrderLabor.Employees != null && taskResponse.workOrderWrapper.workOrderLabor.Employees.Count > 0)
                {
                    var assingedToEmployees = taskResponse.workOrderWrapper.workOrderLabor.Employees;
                    if (assingedToEmployees != null)
                    {
                        this.EmployeeID = assingedToEmployees.First().EmployeeLaborCraftID;
                        this.EmployeeName = ShortString.shorten(assingedToEmployees.First().EmployeeName) + "(" + assingedToEmployees.First().LaborCraftCode + ")";
                    }

                }
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

                OnAppearing();
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
            Entry e1 = sender as MyEntry;
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
                        Spacing = 20,
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
                                var btnTruePF = new Button() { CornerRadius = 5, HeightRequest = 36, FontSize = 10, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "Pass", BackgroundColor = Color.Gray };
                                var btnFalsePF = new Button() { CornerRadius = 5, HeightRequest = 36, FontSize = 10, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "Fail", BackgroundColor = Color.Gray };
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
                                var btnTruePF = new Button() { CornerRadius = 5, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "Pass", BackgroundColor = Color.Gray };
                                var btnFalsePF = new Button() { CornerRadius = 5, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "Fail", BackgroundColor = Color.Gray };
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

                            Layout = new MyEntry() { Keyboard = Keyboard.Numeric, WidthRequest = 65, HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Start, };

                            GenerateAnswerText(item);

                            (Layout as MyEntry).BindingContext = new Range() { MaxRange = item.MaxRange, MinRange = item.MinRange };
                            (Layout as MyEntry).TextChanged += StandardRange_TextChanged;
                            (Layout as MyEntry).Text = item.AnswerDescription;

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
                                var btnTrue = new Button() { CornerRadius = 5, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "Yes", BackgroundColor = Color.Gray };
                                var btnFalse = new Button() { CornerRadius = 5, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "No", BackgroundColor = Color.Gray };
                                var btnNA = new Button() { CornerRadius = 5, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End, Text = "NA", BackgroundColor = Color.Gray };
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

                            Layout = new MyEntry() { Keyboard = Keyboard.Numeric, WidthRequest = 65, HorizontalOptions = LayoutOptions.End, VerticalOptions = LayoutOptions.Start };
                            (Layout as MyEntry).Text = item.AnswerDescription;
                            (Layout as MyEntry).TextChanged += OnlyNumeric_TextChanged;

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


                            if (Device.RuntimePlatform == Device.UWP)
                            {
                                Layout = new MyPicker() { VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End };
                            }
                            else
                            {
                                Layout = new MyPicker() { WidthRequest = 300, VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End };
                            }

                            if (!string.IsNullOrWhiteSpace(item.Option1))
                                (Layout as MyPicker).Items.Add(item.Option1);
                            if (!string.IsNullOrWhiteSpace(item.Option2))
                                (Layout as MyPicker).Items.Add(item.Option2);
                            if (!string.IsNullOrWhiteSpace(item.Option3))
                                (Layout as MyPicker).Items.Add(item.Option3);
                            if (!string.IsNullOrWhiteSpace(item.Option4))
                                (Layout as MyPicker).Items.Add(item.Option4);
                            if (!string.IsNullOrWhiteSpace(item.Option5))
                                (Layout as MyPicker).Items.Add(item.Option5);
                            if (!string.IsNullOrWhiteSpace(item.Option6))
                                (Layout as MyPicker).Items.Add(item.Option6);
                            if (!string.IsNullOrWhiteSpace(item.Option7))
                                (Layout as MyPicker).Items.Add(item.Option7);
                            if (!string.IsNullOrWhiteSpace(item.Option8))
                                (Layout as MyPicker).Items.Add(item.Option8);
                            if (!string.IsNullOrWhiteSpace(item.Option9))
                                (Layout as MyPicker).Items.Add(item.Option9);
                            if (!string.IsNullOrWhiteSpace(item.Option10))
                                (Layout as MyPicker).Items.Add(item.Option10);

                            var index = (Layout as MyPicker).Items.IndexOf(item.AnswerDescription);
                            (Layout as MyPicker).SelectedIndex = index;

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

                    var btnsave = new Button() { CornerRadius = 5, WidthRequest = 300, HeightRequest = 40, Text = WebControlTitle.GetTargetNameByTitleName("Save"), HorizontalOptions = LayoutOptions.Center, BackgroundColor = Color.FromHex("#87CEFA"), TextColor = Color.White, BorderColor = Color.Black };
                    btnsave.Clicked += Btnsave_Clicked;

                    var btnDelete = new Button() { CornerRadius = 5, WidthRequest = 300, HeightRequest = 40, StyleId = item.InspectionID.ToString(), Text = WebControlTitle.GetTargetNameByTitleName("Delete"), HorizontalOptions = LayoutOptions.Center, BackgroundColor = Color.FromHex("#87CEFA"), TextColor = Color.White, BorderColor = Color.Black };
                    btnDelete.Clicked += BtnDelete_Clicked;

                    #region Add Signature image
                    if (item.SignatureRequired.GetValueOrDefault())
                    {
                        //var imageView = new CustomImage() { ImageBase64String = ImageBase64 };
                        var imageView = new CustomImage() { ImageBase64String = item.SignatureString, HeightRequest = 150 };
                        byte[] byteImg = Convert.FromBase64String(item.SignatureString);

                        // byte[] byteImageCompr = DependencyService.Get<IResizeImage>().ResizeImageAndroid(byteImg, 350, 350);
                        imageView.Source = ImageSource.FromStream(() => new MemoryStream(byteImg));
                        layout2.Children.Add(imageView);

                        var addSignatureButton = new Button() {CornerRadius=5, Text = WebControlTitle.GetTargetNameByTitleName("AddSignature"), BackgroundColor = Color.FromHex("#87CEFA"), TextColor = Color.White, BorderColor = Color.Black };
                        addSignatureButton.Clicked += AddSignatureButton_Clicked;
                        layout2.Children.Add(addSignatureButton);

                        var s = item.SignaturePath;
                    }
                    #endregion

                    layout2.Children.Add(btnsave);
                    layout2.Children.Add(btnDelete);

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
                        Spacing = 20,
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
                                    var btnTruePF = new Button() { CornerRadius = 5, HeightRequest = 36, FontSize = 10, HorizontalOptions = LayoutOptions.End, Text = "Pass", BackgroundColor = Color.Gray };
                                    var btnFalsePF = new Button() { CornerRadius = 5, HeightRequest = 36, FontSize = 10, HorizontalOptions = LayoutOptions.End, Text = "Fail", BackgroundColor = Color.Gray };
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
                                    var btnTruePF = new Button() { CornerRadius = 5, HorizontalOptions = LayoutOptions.End, Text = "Pass", BackgroundColor = Color.Gray };
                                    var btnFalsePF = new Button() { CornerRadius = 5, HorizontalOptions = LayoutOptions.End, Text = "Fail", BackgroundColor = Color.Gray };
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

                                Layout = new MyEntry() { Keyboard = Keyboard.Numeric, WidthRequest = 65, HorizontalOptions = LayoutOptions.End };

                                (Layout as MyEntry).BindingContext = new Range() { MaxRange = item1.MaxRange, MinRange = item1.MinRange };
                                (Layout as MyEntry).TextChanged += StandardRange_TextChanged;
                                (Layout as MyEntry).Text = item1.AnswerDescription;


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
                                    var btnTrue = new Button() { CornerRadius = 5, HeightRequest = 36, FontSize = 10, HorizontalOptions = LayoutOptions.End, Text = "Yes", BackgroundColor = Color.Gray };
                                    var btnFalse = new Button() { CornerRadius = 5, HeightRequest = 36, FontSize = 10, HorizontalOptions = LayoutOptions.End, Text = "No", BackgroundColor = Color.Gray };
                                    var btnNA = new Button() { CornerRadius = 5, HeightRequest = 36, FontSize = 10, HorizontalOptions = LayoutOptions.End, Text = "NA", BackgroundColor = Color.Gray };
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
                                            //this.btnCreateWorkorder.IsVisible = true;
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
                                    var btnTrue = new Button() { CornerRadius = 5, HorizontalOptions = LayoutOptions.End, Text = "Yes", BackgroundColor = Color.Gray };
                                    var btnFalse = new Button() { CornerRadius = 5, HorizontalOptions = LayoutOptions.End, Text = "No", BackgroundColor = Color.Gray };
                                    var btnNA = new Button() { CornerRadius = 5, HorizontalOptions = LayoutOptions.End, Text = "NA", BackgroundColor = Color.Gray };
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
                                            //this.btnCreateWorkorder.IsVisible = true;
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

                                Layout = new MyEntry() { Keyboard = Keyboard.Numeric, WidthRequest = 65, HorizontalOptions = LayoutOptions.End };
                                (Layout as MyEntry).Text = item1.AnswerDescription;
                                (Layout as MyEntry).TextChanged += OnlyNumeric_TextChanged;


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

                                if (Device.RuntimePlatform == Device.UWP)
                                {
                                    Layout = new MyPicker() { VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End };
                                }
                                else
                                {
                                    Layout = new MyPicker() {WidthRequest=300,VerticalOptions = LayoutOptions.Start, HorizontalOptions = LayoutOptions.End };
                                }

                                if (!string.IsNullOrWhiteSpace(item1.Option1))
                                    (Layout as MyPicker).Items.Add(item1.Option1);
                                if (!string.IsNullOrWhiteSpace(item1.Option2))
                                    (Layout as MyPicker).Items.Add(item1.Option2);
                                if (!string.IsNullOrWhiteSpace(item1.Option3))
                                    (Layout as MyPicker).Items.Add(item1.Option3);
                                if (!string.IsNullOrWhiteSpace(item1.Option4))
                                    (Layout as MyPicker).Items.Add(item1.Option4);
                                if (!string.IsNullOrWhiteSpace(item1.Option5))
                                    (Layout as MyPicker).Items.Add(item1.Option5);
                                if (!string.IsNullOrWhiteSpace(item1.Option6))
                                    (Layout as MyPicker).Items.Add(item1.Option6);
                                if (!string.IsNullOrWhiteSpace(item1.Option7))
                                    (Layout as MyPicker).Items.Add(item1.Option7);
                                if (!string.IsNullOrWhiteSpace(item1.Option8))
                                    (Layout as MyPicker).Items.Add(item1.Option8);
                                if (!string.IsNullOrWhiteSpace(item1.Option9))
                                    (Layout as MyPicker).Items.Add(item1.Option9);
                                if (!string.IsNullOrWhiteSpace(item1.Option10))
                                    (Layout as MyPicker).Items.Add(item1.Option10);


                                var index = (Layout as MyPicker).Items.IndexOf(item1.AnswerDescription);
                                (Layout as MyPicker).SelectedIndex = index;

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

                    var btnSaveSection = new Button() { CornerRadius = 5, WidthRequest = 300, Text = WebControlTitle.GetTargetNameByTitleName("Save"), CommandParameter = commonSections, HeightRequest = 40, BackgroundColor = Color.FromHex("#87CEFA"), HorizontalOptions = LayoutOptions.Center, TextColor = Color.White, BorderColor = Color.Black };
                    btnSaveSection.Clicked += BtnSaveSection_Clicked;

                    var btnDelete = new Button() { CornerRadius = 5, WidthRequest = 300, StyleId = item.SectionID.ToString(), Text = WebControlTitle.GetTargetNameByTitleName("Delete"), HeightRequest = 40, HorizontalOptions = LayoutOptions.Center, BackgroundColor = Color.FromHex("#87CEFA"), TextColor = Color.White, BorderColor = Color.Black };
                    btnDelete.Clicked += BtnSectionDelete_Clicked;



                    #region Add Signature image
                    if (item.SignatureRequired.GetValueOrDefault())
                    {
                        //var imageView = new CustomImage() { ImageBase64String = ImageBase64 };
                        var imageView = new CustomImage() { ImageBase64String = item.SignatureString, HeightRequest = 150 };
                        byte[] byteImg = Convert.FromBase64String(item.SignatureString);

                        // byte[] byteImageCompr = DependencyService.Get<IResizeImage>().ResizeImageAndroid(byteImg, 350, 350);
                        imageView.Source = ImageSource.FromStream(() => new MemoryStream(byteImg));
                        layout1.Children.Add(imageView);

                        var addSignatureButton = new Button() { CornerRadius = 5, WidthRequest = 300, Text = WebControlTitle.GetTargetNameByTitleName("AddSignature"), HeightRequest = 40, BackgroundColor = Color.FromHex("#87CEFA"), HorizontalOptions = LayoutOptions.Center, TextColor = Color.White, BorderColor = Color.Black };
                        addSignatureButton.Clicked += AddSignatureButton_Clicked;
                        layout1.Children.Add(addSignatureButton);

                    }
                    #endregion

                    layout1.Children.Add(btnSaveSection);
                    layout1.Children.Add(btnDelete);

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
            var image = parentView.Children[parentView.Children.Count - 4] as CustomImage;
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
                        // CreateWorkorderButtonColor = false;
                    }

                    else
                    {
                        this.btnCreateWorkorder.IsVisible = true;
                        //this.btnCreateWorkorder.BackgroundColor = Color.White;
                        e1.BackgroundColor = Color.Red;
                        // CreateWorkorderButtonColor = true;
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
                CC = await ViewModel._inspectionService.GetWorkorderInspection(this.WorkorderID.ToString(), AppSettings.User.UserID.ToString());
                if (CC.workOrderEmployee.Count == 0 && CC.workorderContractor.Count == 0)
                {

                    UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("PleaseFirstAddEmployeeORContractor"), TimeSpan.FromSeconds(2));
                    return;

                }
                List<InspectionTOAnswers> listtoAnswer = new List<InspectionTOAnswers>();
                List<InspectionTOAnswers> liststartAnswer = new List<InspectionTOAnswers>();
                List<InspectionTOAnswers> listcompletionAnswer = new List<InspectionTOAnswers>();


                UserDialogs.Instance.ShowLoading(WebControlTitle.GetTargetNameByTitleName("Loading"));
                await Task.Delay(1000);

                var stacklayout = (sender as Button).Parent as StackLayout;

                //var context = (sender as Button).CommandParameter as List<ExistingInspections>;

                List<InspectionAnswer> listAnswer = new List<InspectionAnswer>();
                int viewCount = 0;

                try
                {
                    // var signatureImage = stacklayout.Children[stacklayout.Children.Count - 3] as CustomImage;
                    foreach (View i in (stacklayout).Children.Where(x => x.GetType() == typeof(CustomImage)))
                    {

                        var signatureImage = i;
                        //CustomImage signatureImage = stacklayout.Children.Where(x => x.GetType() == typeof(CustomImage)) as CustomImage;
                        if (signatureImage == null)
                        {
                            viewCount = 1;
                        }
                        else
                        {
                            viewCount = 3;
                        }

                    }

                }
                catch (Exception) { }

                for (int i = 1; i < stacklayout.Children.Count - viewCount; i++) // run it only for grid
                {
                    try
                    {
                        var stacklayout1 = stacklayout.Children[i] as Grid;
                        if (stacklayout1 == null)
                        {

                        }
                        else
                        {
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
                    }
                    catch (Exception ex)
                    {


                    }

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

                    foreach (View i in (stacklayout).Children.Where(x => x.GetType() == typeof(CustomImage)))
                    {

                        signatureImageView = i as CustomImage;
                    }

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



                #region Save Inspection Time to server




                // 1st Employee/Contractor Layout//

                Label employeecontrcatorids = new Label();
                string EmpLaborCraftID = "0";
                string ContLaborCraftID = "0";
                try
                {
                    var Stacklayout = ParentLayout.Children[0] as StackLayout;
                    var gridlayout = Stacklayout.Children[1] as Grid;


                    var HoursEntryValue = gridlayout.Children[3] as Entry;
                    var MinuteEntryValue = gridlayout.Children[5] as Entry;
                    if (Device.Idiom == TargetIdiom.Phone)
                    {
                        var gridlayout2 = Stacklayout.Children[2] as Grid;
                        var StartdateValue = gridlayout2.Children[1] as CustomDatePicker;
                        var CompletionDateValue = gridlayout2.Children[3] as CustomDatePicker;
                        this.InspectionStartDate = StartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(StartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                        this.InspectionCompletionDate = CompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(CompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;
                        employeecontrcatorids = gridlayout2.Children[4] as Label;


                    }
                    else
                    {
                        employeecontrcatorids = gridlayout.Children[10] as Label;
                        var StartdateValue = gridlayout.Children[7] as CustomDatePicker;
                        var CompletionDateValue = gridlayout.Children[9] as CustomDatePicker;

                        this.InspectionStartDate = StartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(StartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                        this.InspectionCompletionDate = CompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(CompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;




                    }

                    if (Device.Idiom == TargetIdiom.Tablet)
                    {
                        var employeecontrcatoridsforTablet = Stacklayout.Children[0] as Label;
                        var employeecontrcatoridsforTabletsubstring = employeecontrcatoridsforTablet.StyleId.Split(':');
                        string EmpCntID = Int32.Parse(employeecontrcatoridsforTabletsubstring[0]).ToString();
                        string EmpCntValue = employeecontrcatoridsforTabletsubstring[1];

                        if (EmpCntValue == "EmployeeLaborCraft")
                        {
                            EmpLaborCraftID = EmpCntID;
                        }
                        if (EmpCntValue == "ContractorLaborCraft")
                        {
                            ContLaborCraftID = EmpCntID;
                        }

                    }
                    else
                    {
                        var employeecontrcatoridssubstring = employeecontrcatorids.Text.Split(':');
                        string EmpCntID = Int32.Parse(employeecontrcatoridssubstring[0]).ToString();
                        string EmpCntValue = employeecontrcatoridssubstring[1];

                        if (EmpCntValue == "EmployeeLaborCraft")
                        {
                            EmpLaborCraftID = EmpCntID;
                        }
                        if (EmpCntValue == "ContractorLaborCraft")
                        {
                            ContLaborCraftID = EmpCntID;
                        }
                    }

                    int hours = 0;
                    int minutes = 0;


                    if (string.IsNullOrWhiteSpace(HoursEntryValue.Text))
                    {

                    }
                    else
                    {
                        hours = Int32.Parse(HoursEntryValue.Text);
                    }

                    if (string.IsNullOrWhiteSpace(MinuteEntryValue.Text))
                    {

                    }
                    else
                    {
                        minutes = Int32.Parse(MinuteEntryValue.Text);
                    }

                    var result = new TimeSpan(hours, minutes, 0);
                    totalTime = result.TotalSeconds;



                    #region Local Validation

                    {
                        if (!String.IsNullOrWhiteSpace(this.InspectionStartDate.ToString()) && !String.IsNullOrWhiteSpace(this.InspectionCompletionDate.ToString()))
                        {
                            if (this.InspectionStartDate.GetValueOrDefault().Date > this.InspectionCompletionDate.GetValueOrDefault().Date)
                            {
                                //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, "Inspection", formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                                //await DisplayAlert("Alert", WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), "OK");
                                UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), TimeSpan.FromSeconds(2));
                                UserDialogs.Instance.HideLoading();
                                return;
                            }
                        }
                    }




                    #endregion
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
                    liststartAnswer.Add(new InspectionTOAnswers()
                    {

                        StartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,

                    });
                    listcompletionAnswer.Add(new InspectionTOAnswers()
                    {

                        CompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,

                    });

                }
                catch (Exception ex)
                {


                }




                // 2nd Employee/Contractor Layout///
                Label employeecontrcatorids2 = new Label();
                string EmpLaborCraftID2 = "0";
                string ContLaborCraftID2 = "0";
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
                        if (Device.Idiom == TargetIdiom.Phone)
                        {
                            var Secondgridlayout2 = SecondStacklayout.Children[2] as Grid;

                            var SecondStartdateValue = Secondgridlayout2.Children[1] as CustomDatePicker;
                            var SecondCompletionDateValue = Secondgridlayout2.Children[3] as CustomDatePicker;
                            this.InspectionStartDate = SecondStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(SecondStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                            this.InspectionCompletionDate = SecondCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(SecondCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;
                            employeecontrcatorids2 = Secondgridlayout2.Children[4] as Label;

                        }
                        else
                        {
                            var SecondStartdateValue = Secondgridlayout.Children[7] as CustomDatePicker;
                            var SecondCompletionDateValue = Secondgridlayout.Children[9] as CustomDatePicker;
                            employeecontrcatorids2 = Secondgridlayout.Children[10] as Label;
                            this.InspectionStartDate = SecondStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(SecondStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                            this.InspectionCompletionDate = SecondCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(SecondCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;
                        }


                        if (Device.Idiom == TargetIdiom.Tablet)
                        {
                            var employeecontrcatoridsforTablet = SecondStacklayout.Children[0] as Label;
                            var employeecontrcatoridsforTabletsubstring = employeecontrcatoridsforTablet.StyleId.Split(':');
                            string EmpCntID2 = Int32.Parse(employeecontrcatoridsforTabletsubstring[0]).ToString();
                            string EmpCntValue2 = employeecontrcatoridsforTabletsubstring[1];

                            if (EmpCntValue2 == "EmployeeLaborCraft")
                            {
                                EmpLaborCraftID2 = EmpCntID2;
                            }
                            if (EmpCntValue2 == "ContractorLaborCraft")
                            {
                                ContLaborCraftID2 = EmpCntID2;
                            }

                        }
                        else
                        {
                            var employeecontrcatoridssubstring2 = employeecontrcatorids2.Text.Split(':');
                            string EmpCntID2 = Int32.Parse(employeecontrcatoridssubstring2[0]).ToString();
                            string EmpCntValue2 = employeecontrcatoridssubstring2[1];

                            if (EmpCntValue2 == "EmployeeLaborCraft")
                            {
                                EmpLaborCraftID2 = EmpCntID2;
                            }
                            if (EmpCntValue2 == "ContractorLaborCraft")
                            {

                                ContLaborCraftID2 = EmpCntID2;
                            }

                        }

                        int Secondhours = 0;
                        int Secondminutes = 0;

                        if (string.IsNullOrWhiteSpace(SecondHoursEntryValue.Text))
                        {

                        }
                        else
                        {
                            Secondhours = Int32.Parse(SecondHoursEntryValue.Text);
                        }

                        if (string.IsNullOrWhiteSpace(SecondMinuteEntryValue.Text))
                        {

                        }
                        else
                        {
                            Secondminutes = Int32.Parse(SecondMinuteEntryValue.Text);
                        }

                        var Secondresult = new TimeSpan(Secondhours, Secondminutes, 0);
                        totalTime = Secondresult.TotalSeconds;
                        //this.InspectionStartDate = SecondStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(SecondStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                        //this.InspectionCompletionDate = SecondCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(SecondCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;

                        // this.InspectionStartDate = Convert.ToDateTime(SecondStartdateValue.SelectedDate.ToString());
                        // this.InspectionCompletionDate = Convert.ToDateTime(SecondCompletionDateValue.SelectedDate.ToString());
                        #region Local Validation

                        {
                            if (!String.IsNullOrWhiteSpace(this.InspectionStartDate.ToString()) && !String.IsNullOrWhiteSpace(this.InspectionCompletionDate.ToString()))
                            {
                                if (this.InspectionStartDate.GetValueOrDefault().Date > this.InspectionCompletionDate.GetValueOrDefault().Date)
                                {
                                    //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, "Inspection", formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                                    //await DisplayAlert("Alert", WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), "OK");
                                    UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), TimeSpan.FromSeconds(2));
                                    UserDialogs.Instance.HideLoading();
                                    return;
                                }
                            }
                        }




                        #endregion
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
                        liststartAnswer.Add(new InspectionTOAnswers()
                        {

                            StartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,

                        });
                        listcompletionAnswer.Add(new InspectionTOAnswers()
                        {

                            CompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,

                        });
                    }

                }
                catch (Exception ex)
                {


                }



                // 3rd Employee/Contractor Layout///
                Label employeecontrcatorids3 = new Label();
                string EmpLaborCraftID3 = "0";
                string ContLaborCraftID3 = "0";
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
                        if (Device.Idiom == TargetIdiom.Phone)
                        {
                            var Thirdgridlayout2 = ThirdStacklayout.Children[2] as Grid;

                            var ThirdStartdateValue = Thirdgridlayout2.Children[1] as CustomDatePicker;
                            var ThirdCompletionDateValue = Thirdgridlayout2.Children[3] as CustomDatePicker;
                            this.InspectionStartDate = ThirdStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(ThirdStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                            this.InspectionCompletionDate = ThirdCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(ThirdCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;
                            employeecontrcatorids3 = Thirdgridlayout2.Children[4] as Label;

                        }
                        else
                        {
                            var ThirdStartdateValue = Thirdgridlayout.Children[7] as CustomDatePicker;
                            var ThirdCompletionDateValue = Thirdgridlayout.Children[9] as CustomDatePicker;
                            employeecontrcatorids3 = Thirdgridlayout.Children[10] as Label;

                            this.InspectionStartDate = ThirdStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(ThirdStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                            this.InspectionCompletionDate = ThirdCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(ThirdCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;
                        }


                        if (Device.Idiom == TargetIdiom.Tablet)
                        {
                            var employeecontrcatoridsforTablet3 = ThirdStacklayout.Children[0] as Label;
                            var employeecontrcatoridsforTabletsubstring3 = employeecontrcatoridsforTablet3.StyleId.Split(':');
                            string EmpCntID3 = Int32.Parse(employeecontrcatoridsforTabletsubstring3[0]).ToString();
                            string EmpCntValue3 = employeecontrcatoridsforTabletsubstring3[1];

                            if (EmpCntValue3 == "EmployeeLaborCraft")
                            {
                                EmpLaborCraftID3 = EmpCntID3;
                            }
                            if (EmpCntValue3 == "ContractorLaborCraft")
                            {
                                ContLaborCraftID3 = EmpCntID3;
                            }

                        }
                        else
                        {
                            var employeecontrcatoridssubstring3 = employeecontrcatorids3.Text.Split(':');
                            string EmpCntID3 = Int32.Parse(employeecontrcatoridssubstring3[0]).ToString();
                            string EmpCntValue3 = employeecontrcatoridssubstring3[1];

                            if (EmpCntValue3 == "EmployeeLaborCraft")
                            {
                                EmpLaborCraftID3 = EmpCntID3;
                            }
                            if (EmpCntValue3 == "ContractorLaborCraft")
                            {
                                ContLaborCraftID3 = EmpCntID3;
                            }


                        }

                        int Thirdminutes = 0;
                        int Thirdhours = 0;
                        if (string.IsNullOrWhiteSpace(ThirdHoursEntryValue.Text))
                        {

                        }
                        else
                        {
                            Thirdhours = Int32.Parse(ThirdHoursEntryValue.Text);
                        }

                        if (string.IsNullOrWhiteSpace(ThirdMinuteEntryValue.Text))
                        {

                        }
                        else
                        {
                            Thirdminutes = Int32.Parse(ThirdMinuteEntryValue.Text);
                        }


                        var Thirdresult = new TimeSpan(Thirdhours, Thirdminutes, 0);
                        totalTime = Thirdresult.TotalSeconds;
                        //this.InspectionStartDate = ThirdStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(ThirdStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                        //this.InspectionCompletionDate = ThirdCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(ThirdCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;

                        // this.InspectionStartDate = Convert.ToDateTime(ThirdStartdateValue.Date.ToString());
                        // this.InspectionCompletionDate = Convert.ToDateTime(ThirdCompletionDateValue.Date.ToString());
                        #region Local Validation

                        {
                            if (!String.IsNullOrWhiteSpace(this.InspectionStartDate.ToString()) && !String.IsNullOrWhiteSpace(this.InspectionCompletionDate.ToString()))
                            {
                                if (this.InspectionStartDate.GetValueOrDefault().Date > this.InspectionCompletionDate.GetValueOrDefault().Date)
                                {
                                    //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, "Inspection", formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                                    //await DisplayAlert("Alert", WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), "OK");
                                    UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), TimeSpan.FromSeconds(2));
                                    UserDialogs.Instance.HideLoading();
                                    return;
                                }
                            }
                        }




                        #endregion
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
                        liststartAnswer.Add(new InspectionTOAnswers()
                        {

                            StartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,

                        });
                        listcompletionAnswer.Add(new InspectionTOAnswers()
                        {

                            CompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,

                        });
                    }

                }
                catch (Exception ex)
                {


                }



                // 4th Employee/Contractor Layout///
                Label employeecontrcatorids4 = new Label();
                string EmpLaborCraftID4 = "0";
                string ContLaborCraftID4 = "0";
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
                        if (Device.Idiom == TargetIdiom.Phone)
                        {
                            var Fourthgridlayout2 = FourthStacklayout.Children[2] as Grid;

                            var FourthStartdateValue = Fourthgridlayout2.Children[1] as CustomDatePicker;
                            var FourthCompletionDateValue = Fourthgridlayout2.Children[3] as CustomDatePicker;
                            this.InspectionStartDate = FourthStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(FourthStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                            this.InspectionCompletionDate = FourthCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(FourthCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;
                            employeecontrcatorids4 = Fourthgridlayout2.Children[4] as Label;

                        }
                        else
                        {
                            var FourthStartdateValue = Fourthgridlayout.Children[7] as CustomDatePicker;
                            var FourthCompletionDateValue = Fourthgridlayout.Children[9] as CustomDatePicker;
                            employeecontrcatorids4 = Fourthgridlayout.Children[10] as Label;


                            this.InspectionStartDate = FourthStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(FourthStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                            this.InspectionCompletionDate = FourthCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(FourthCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;
                        }



                        if (Device.Idiom == TargetIdiom.Tablet)
                        {
                            var employeecontrcatoridsforTablet = FourthStacklayout.Children[0] as Label;
                            var employeecontrcatoridsforTabletsubstring = employeecontrcatoridsforTablet.StyleId.Split(':');
                            string EmpCntID4 = Int32.Parse(employeecontrcatoridsforTabletsubstring[0]).ToString();
                            string EmpCntValue4 = employeecontrcatoridsforTabletsubstring[1];

                            if (EmpCntValue4 == "EmployeeLaborCraft")
                            {
                                EmpLaborCraftID4 = EmpCntID4;
                            }
                            if (EmpCntValue4 == "ContractorLaborCraft")
                            {
                                ContLaborCraftID4 = EmpCntID4;
                            }

                        }
                        else
                        {
                            var employeecontrcatoridssubstring4 = employeecontrcatorids4.Text.Split(':');
                            string EmpCntID4 = Int32.Parse(employeecontrcatoridssubstring4[0]).ToString();
                            string EmpCntValue4 = employeecontrcatoridssubstring4[1];

                            if (EmpCntValue4 == "EmployeeLaborCraft")
                            {
                                EmpLaborCraftID4 = EmpCntID4;
                            }
                            if (EmpCntValue4 == "ContractorLaborCraft")
                            {
                                ContLaborCraftID4 = EmpCntID4;
                            }


                        }


                        int Fourthhours = 0;
                        int Fourthminutes = 0;

                        if (string.IsNullOrWhiteSpace(FourthHoursEntryValue.Text))
                        {

                        }
                        else
                        {
                            Fourthhours = Int32.Parse(FourthHoursEntryValue.Text);
                        }

                        if (string.IsNullOrWhiteSpace(FourthMinuteEntryValue.Text))
                        {

                        }
                        else
                        {
                            Fourthminutes = Int32.Parse(FourthMinuteEntryValue.Text);
                        }



                        var Fourthresult = new TimeSpan(Fourthhours, Fourthminutes, 0);
                        totalTime = Fourthresult.TotalSeconds;
                        //this.InspectionStartDate = FourthStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(FourthStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                        //this.InspectionCompletionDate = FourthCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(FourthCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;
                        //this.InspectionStartDate = Convert.ToDateTime(FourthStartdateValue.Date.ToString());
                        //this.InspectionCompletionDate = Convert.ToDateTime(FourthCompletionDateValue.Date.ToString());
                        #region Local Validation

                        {
                            if (!String.IsNullOrWhiteSpace(this.InspectionStartDate.ToString()) && !String.IsNullOrWhiteSpace(this.InspectionCompletionDate.ToString()))
                            {
                                if (this.InspectionStartDate.GetValueOrDefault().Date > this.InspectionCompletionDate.GetValueOrDefault().Date)
                                {
                                    //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, "Inspection", formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                                    //await DisplayAlert("Alert", WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), "OK");
                                    UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), TimeSpan.FromSeconds(2));
                                    UserDialogs.Instance.HideLoading();
                                    return;
                                }
                            }
                        }




                        #endregion
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
                        liststartAnswer.Add(new InspectionTOAnswers()
                        {

                            StartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,

                        });
                        listcompletionAnswer.Add(new InspectionTOAnswers()
                        {

                            CompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,

                        });
                    }

                }
                catch (Exception ex)
                {


                }



                // 5th Employee/Contractor Layout///
                Label employeecontrcatorids5 = new Label();
                string EmpLaborCraftID5 = "0";
                string ContLaborCraftID5 = "0";
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
                        if (Device.Idiom == TargetIdiom.Phone)
                        {
                            var Fifthgridlayout2 = FifthStacklayout.Children[2] as Grid;

                            var FifthStartdateValue = Fifthgridlayout2.Children[1] as CustomDatePicker;
                            var FifthCompletionDateValue = Fifthgridlayout2.Children[3] as CustomDatePicker;
                            this.InspectionStartDate = FifthStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(FifthStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                            this.InspectionCompletionDate = FifthCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(FifthCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;
                            employeecontrcatorids5 = Fifthgridlayout2.Children[4] as Label;

                        }
                        else
                        {
                            var FifthStartdateValue = Fifthgridlayout.Children[7] as CustomDatePicker;
                            var FifthCompletionDateValue = Fifthgridlayout.Children[9] as CustomDatePicker;
                            employeecontrcatorids5 = Fifthgridlayout.Children[10] as Label;



                            this.InspectionStartDate = FifthStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(FifthStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                            this.InspectionCompletionDate = FifthCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(FifthCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;
                        }



                        if (Device.Idiom == TargetIdiom.Tablet)
                        {
                            var employeecontrcatoridsforTablet = FifthStacklayout.Children[0] as Label;
                            var employeecontrcatoridsforTabletsubstring = employeecontrcatoridsforTablet.StyleId.Split(':');
                            string EmpCntID5 = Int32.Parse(employeecontrcatoridsforTabletsubstring[0]).ToString();
                            string EmpCntValue5 = employeecontrcatoridsforTabletsubstring[1];

                            if (EmpCntValue5 == "EmployeeLaborCraft")
                            {
                                EmpLaborCraftID5 = EmpCntID5;
                            }
                            if (EmpCntValue5 == "ContractorLaborCraft")
                            {
                                ContLaborCraftID5 = EmpCntID5;
                            }

                        }
                        else
                        {
                            var employeecontrcatoridssubstring5 = employeecontrcatorids5.Text.Split(':');
                            string EmpCntID5 = Int32.Parse(employeecontrcatoridssubstring5[0]).ToString();
                            string EmpCntValue5 = employeecontrcatoridssubstring5[1];

                            if (EmpCntValue5 == "EmployeeLaborCraft")
                            {
                                EmpLaborCraftID5 = EmpCntID5;
                            }
                            if (EmpCntValue5 == "ContractorLaborCraft")
                            {
                                ContLaborCraftID5 = EmpCntID5;
                            }


                        }


                        int Fifthhours = 0;
                        int Fifthminutes = 0;

                        if (string.IsNullOrWhiteSpace(FifthHoursEntryValue.Text))
                        {

                        }
                        else
                        {
                            Fifthhours = Int32.Parse(FifthHoursEntryValue.Text);
                        }

                        if (string.IsNullOrWhiteSpace(FifthMinuteEntryValue.Text))
                        {

                        }
                        else
                        {
                            Fifthminutes = Int32.Parse(FifthMinuteEntryValue.Text);
                        }



                        var Fifthresult = new TimeSpan(Fifthhours, Fifthminutes, 0);
                        totalTime = Fifthresult.TotalSeconds;
                        //this.InspectionStartDate = FifthStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(FifthStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                        //this.InspectionCompletionDate = FifthCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(FifthCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;

                        //this.InspectionStartDate = Convert.ToDateTime(FifthStartdateValue.Date.ToString());
                        //this.InspectionCompletionDate = Convert.ToDateTime(FifthCompletionDateValue.Date.ToString());
                        #region Local Validation

                        {
                            if (!String.IsNullOrWhiteSpace(this.InspectionStartDate.ToString()) && !String.IsNullOrWhiteSpace(this.InspectionCompletionDate.ToString()))
                            {
                                if (this.InspectionStartDate.GetValueOrDefault().Date > this.InspectionCompletionDate.GetValueOrDefault().Date)
                                {
                                    //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, "Inspection", formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                                    //await DisplayAlert("Alert", WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), "OK");
                                    UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), TimeSpan.FromSeconds(2));
                                    UserDialogs.Instance.HideLoading();
                                    return;
                                }
                            }
                        }




                        #endregion
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
                        liststartAnswer.Add(new InspectionTOAnswers()
                        {

                            StartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,

                        });
                        listcompletionAnswer.Add(new InspectionTOAnswers()
                        {

                            CompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,

                        });
                    }
                }
                catch (Exception ex)
                {


                }






                #endregion


                #region Validation from workorder


                ServiceOutput abc = await ViewModel._workorderService.GetWorkorderByWorkorderID(UserId, WorkorderID.ToString());

                string workordercompDate = string.Empty;
                string workorderstartDate = string.Empty;


                if (abc.workOrderWrapper.workOrder.WorkStartedDate != null)
                    workorderstartDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(abc.workOrderWrapper.workOrder.WorkStartedDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("d");
                if (abc.workOrderWrapper.workOrder.CompletionDate != null)
                    workordercompDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(abc.workOrderWrapper.workOrder.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("d");

                if (listtoAnswer != null && listtoAnswer.Count > 0)
                {
                    MinimumInspectionStartDate = listtoAnswer.Min(i => i.StartDate);
                    MaximumInspectionCompletionDate = listtoAnswer.Max(i => i.CompletionDate);

                }
                else
                {
                    MinimumInspectionStartDate = null;
                    MaximumInspectionCompletionDate = null;
                }

                liststartAnswer.RemoveAll(x => x.StartDate == null);
                listcompletionAnswer.RemoveAll(x => x.CompletionDate == null);
                #region Start date picker validation

                //replace this.PickerInspectionStartDate.Date with this.InspectionStartDate
                if (!string.IsNullOrWhiteSpace(this.MinimumInspectionStartDate.ToString()))
                {
                    //// if inspection start date is before than wo start date the give alert >>> Inspection start date can not lesser than WO start date
                    if (!string.IsNullOrWhiteSpace(workorderstartDate))
                    {
                        if (workorderstartDate != null && liststartAnswer.Any(x => x.StartDate.Value.Date < DateTime.Parse(workorderstartDate)))
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
                        if (workordercompDate != null && liststartAnswer.Any(x => x.StartDate.Value.Date > DateTime.Parse(workordercompDate)))
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
                if (!string.IsNullOrWhiteSpace(this.MaximumInspectionCompletionDate.ToString()))
                {
                    //// if inspection completion date is before than wo start date the give alert >>> Inspection completion date can not lesser than WO start date
                    if (!string.IsNullOrWhiteSpace(workorderstartDate))
                    {
                        if (workorderstartDate != null && listcompletionAnswer.Any(x => x.CompletionDate.Value.Date < DateTime.Parse(workorderstartDate)))
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
                        if (workordercompDate != null && listcompletionAnswer.Any(x => x.CompletionDate.Value.Date > DateTime.Parse(workordercompDate)))
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


                var yourobject1 = new InspectionTOAnswers
                {
                    inspectionToAnswers = listtoAnswer,
                    ClientIANATimeZone = DateTimeZoneProviders.Serialization.GetSystemDefault().ToString(),
                    UserID = long.Parse(this.UserId),

                };


                ServiceOutput serviceStatus1 = await ViewModel._inspectionService.SaveWorkorderInspectionTime(yourobject1);

                if (serviceStatus1.servicestatus == "true")
                {
                    // enthour.Text = "";
                    // entmin.Text = "";
                    UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("AnswerSuccessfullySaved"), TimeSpan.FromSeconds(2));

                    foreach (var item in workOrderInspectionTimeID)
                    {
                        string Employeekey = "WorkOrderEmployee:" + item.Value;
                        string Contractorkey = "WorkorderContracator:" + item.Value;
                        WorkorderInspectionStorge.Storage.Delete(Employeekey);
                        WorkorderInspectionStorge.Storage.Delete(Contractorkey);

                    }
                    workOrderInspectionTimeID.Clear();


                }


                UserDialogs.Instance.HideLoading();
                total = total.Subtract(total);
                MainLayout.Children.Clear();
                OnAppearing();

            }
            catch (Exception ex)
            {


            }


        }
        private async void Btnsave_Clicked(object sender, EventArgs e)
        {
            CC = await ViewModel._inspectionService.GetWorkorderInspection(this.WorkorderID.ToString(), AppSettings.User.UserID.ToString());
            if (CC.workOrderEmployee.Count == 0 && CC.workorderContractor.Count == 0)
            {

                UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("PleaseFirstAddEmployeeORContractor"), TimeSpan.FromSeconds(2));
                return;

            }
            List<InspectionTOAnswers> listtoAnswer = new List<InspectionTOAnswers>();
            List<InspectionTOAnswers> liststartAnswer = new List<InspectionTOAnswers>();
            List<InspectionTOAnswers> listcompletionAnswer = new List<InspectionTOAnswers>();



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



            #region Save Inspection Time to server




            // 1st Employee/Contractor Layout//

            Label employeecontrcatorids = new Label();
            string EmpLaborCraftID = "0";
            string ContLaborCraftID = "0";
            try
            {
                var Stacklayout = ParentLayout.Children[0] as StackLayout;
                var gridlayout = Stacklayout.Children[1] as Grid;


                var HoursEntryValue = gridlayout.Children[3] as Entry;
                var MinuteEntryValue = gridlayout.Children[5] as Entry;
                if (Device.Idiom == TargetIdiom.Phone)
                {
                    var gridlayout2 = Stacklayout.Children[2] as Grid;
                    var StartdateValue = gridlayout2.Children[1] as CustomDatePicker;
                    var CompletionDateValue = gridlayout2.Children[3] as CustomDatePicker;
                    this.InspectionStartDate = StartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(StartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                    this.InspectionCompletionDate = CompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(CompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;
                    employeecontrcatorids = gridlayout2.Children[4] as Label;


                }
                else
                {
                    employeecontrcatorids = gridlayout.Children[10] as Label;
                    var StartdateValue = gridlayout.Children[7] as CustomDatePicker;
                    var CompletionDateValue = gridlayout.Children[9] as CustomDatePicker;

                    this.InspectionStartDate = StartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(StartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                    this.InspectionCompletionDate = CompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(CompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;




                }

                if (Device.Idiom == TargetIdiom.Tablet)
                {
                    var employeecontrcatoridsforTablet = Stacklayout.Children[0] as Label;
                    var employeecontrcatoridsforTabletsubstring = employeecontrcatoridsforTablet.StyleId.Split(':');
                    string EmpCntID = Int32.Parse(employeecontrcatoridsforTabletsubstring[0]).ToString();
                    string EmpCntValue = employeecontrcatoridsforTabletsubstring[1];

                    if (EmpCntValue == "EmployeeLaborCraft")
                    {
                        EmpLaborCraftID = EmpCntID;
                    }
                    if (EmpCntValue == "ContractorLaborCraft")
                    {
                        ContLaborCraftID = EmpCntID;
                    }

                }
                else
                {
                    var employeecontrcatoridssubstring = employeecontrcatorids.Text.Split(':');
                    string EmpCntID = Int32.Parse(employeecontrcatoridssubstring[0]).ToString();
                    string EmpCntValue = employeecontrcatoridssubstring[1];

                    if (EmpCntValue == "EmployeeLaborCraft")
                    {
                        EmpLaborCraftID = EmpCntID;
                    }
                    if (EmpCntValue == "ContractorLaborCraft")
                    {
                        ContLaborCraftID = EmpCntID;
                    }
                }

                int hours = 0;
                int minutes = 0;


                if (string.IsNullOrWhiteSpace(HoursEntryValue.Text))
                {

                }
                else
                {
                    hours = Int32.Parse(HoursEntryValue.Text);
                }

                if (string.IsNullOrWhiteSpace(MinuteEntryValue.Text))
                {

                }
                else
                {
                    minutes = Int32.Parse(MinuteEntryValue.Text);
                }

                var result = new TimeSpan(hours, minutes, 0);
                totalTime = result.TotalSeconds;



                #region Local Validation

                {
                    if (!String.IsNullOrWhiteSpace(this.InspectionStartDate.ToString()) && !String.IsNullOrWhiteSpace(this.InspectionCompletionDate.ToString()))
                    {
                        if (this.InspectionStartDate.GetValueOrDefault().Date > this.InspectionCompletionDate.GetValueOrDefault().Date)
                        {
                            //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, "Inspection", formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                            //await DisplayAlert("Alert", WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), "OK");
                            UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), TimeSpan.FromSeconds(2));
                            UserDialogs.Instance.HideLoading();
                            return;
                        }
                    }
                }




                #endregion
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
                liststartAnswer.Add(new InspectionTOAnswers()
                {

                    StartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,

                });
                listcompletionAnswer.Add(new InspectionTOAnswers()
                {

                    CompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,

                });

            }
            catch (Exception ex)
            {


            }




            // 2nd Employee/Contractor Layout///
            Label employeecontrcatorids2 = new Label();
            string EmpLaborCraftID2 = "0";
            string ContLaborCraftID2 = "0";
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
                    if (Device.Idiom == TargetIdiom.Phone)
                    {
                        var Secondgridlayout2 = SecondStacklayout.Children[2] as Grid;

                        var SecondStartdateValue = Secondgridlayout2.Children[1] as CustomDatePicker;
                        var SecondCompletionDateValue = Secondgridlayout2.Children[3] as CustomDatePicker;
                        this.InspectionStartDate = SecondStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(SecondStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                        this.InspectionCompletionDate = SecondCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(SecondCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;
                        employeecontrcatorids2 = Secondgridlayout2.Children[4] as Label;

                    }
                    else
                    {
                        var SecondStartdateValue = Secondgridlayout.Children[7] as CustomDatePicker;
                        var SecondCompletionDateValue = Secondgridlayout.Children[9] as CustomDatePicker;
                        employeecontrcatorids2 = Secondgridlayout.Children[10] as Label;
                        this.InspectionStartDate = SecondStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(SecondStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                        this.InspectionCompletionDate = SecondCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(SecondCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;
                    }


                    if (Device.Idiom == TargetIdiom.Tablet)
                    {
                        var employeecontrcatoridsforTablet = SecondStacklayout.Children[0] as Label;
                        var employeecontrcatoridsforTabletsubstring = employeecontrcatoridsforTablet.StyleId.Split(':');
                        string EmpCntID2 = Int32.Parse(employeecontrcatoridsforTabletsubstring[0]).ToString();
                        string EmpCntValue2 = employeecontrcatoridsforTabletsubstring[1];

                        if (EmpCntValue2 == "EmployeeLaborCraft")
                        {
                            EmpLaborCraftID2 = EmpCntID2;
                        }
                        if (EmpCntValue2 == "ContractorLaborCraft")
                        {
                            ContLaborCraftID2 = EmpCntID2;
                        }

                    }
                    else
                    {
                        var employeecontrcatoridssubstring2 = employeecontrcatorids2.Text.Split(':');
                        string EmpCntID2 = Int32.Parse(employeecontrcatoridssubstring2[0]).ToString();
                        string EmpCntValue2 = employeecontrcatoridssubstring2[1];

                        if (EmpCntValue2 == "EmployeeLaborCraft")
                        {
                            EmpLaborCraftID2 = EmpCntID2;
                        }
                        if (EmpCntValue2 == "ContractorLaborCraft")
                        {

                            ContLaborCraftID2 = EmpCntID2;
                        }

                    }

                    int Secondhours = 0;
                    int Secondminutes = 0;

                    if (string.IsNullOrWhiteSpace(SecondHoursEntryValue.Text))
                    {

                    }
                    else
                    {
                        Secondhours = Int32.Parse(SecondHoursEntryValue.Text);
                    }

                    if (string.IsNullOrWhiteSpace(SecondMinuteEntryValue.Text))
                    {

                    }
                    else
                    {
                        Secondminutes = Int32.Parse(SecondMinuteEntryValue.Text);
                    }

                    var Secondresult = new TimeSpan(Secondhours, Secondminutes, 0);
                    totalTime = Secondresult.TotalSeconds;
                    //this.InspectionStartDate = SecondStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(SecondStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                    //this.InspectionCompletionDate = SecondCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(SecondCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;

                    // this.InspectionStartDate = Convert.ToDateTime(SecondStartdateValue.SelectedDate.ToString());
                    // this.InspectionCompletionDate = Convert.ToDateTime(SecondCompletionDateValue.SelectedDate.ToString());
                    #region Local Validation

                    {
                        if (!String.IsNullOrWhiteSpace(this.InspectionStartDate.ToString()) && !String.IsNullOrWhiteSpace(this.InspectionCompletionDate.ToString()))
                        {
                            if (this.InspectionStartDate.GetValueOrDefault().Date > this.InspectionCompletionDate.GetValueOrDefault().Date)
                            {
                                //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, "Inspection", formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                                //await DisplayAlert("Alert", WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), "OK");
                                UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), TimeSpan.FromSeconds(2));
                                UserDialogs.Instance.HideLoading();
                                return;
                            }
                        }
                    }




                    #endregion
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
                    liststartAnswer.Add(new InspectionTOAnswers()
                    {

                        StartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,

                    });
                    listcompletionAnswer.Add(new InspectionTOAnswers()
                    {

                        CompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,

                    });
                }

            }
            catch (Exception ex)
            {


            }



            // 3rd Employee/Contractor Layout///
            Label employeecontrcatorids3 = new Label();
            string EmpLaborCraftID3 = "0";
            string ContLaborCraftID3 = "0";
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
                    if (Device.Idiom == TargetIdiom.Phone)
                    {
                        var Thirdgridlayout2 = ThirdStacklayout.Children[2] as Grid;

                        var ThirdStartdateValue = Thirdgridlayout2.Children[1] as CustomDatePicker;
                        var ThirdCompletionDateValue = Thirdgridlayout2.Children[3] as CustomDatePicker;
                        this.InspectionStartDate = ThirdStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(ThirdStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                        this.InspectionCompletionDate = ThirdCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(ThirdCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;
                        employeecontrcatorids3 = Thirdgridlayout2.Children[4] as Label;

                    }
                    else
                    {
                        var ThirdStartdateValue = Thirdgridlayout.Children[7] as CustomDatePicker;
                        var ThirdCompletionDateValue = Thirdgridlayout.Children[9] as CustomDatePicker;
                        employeecontrcatorids3 = Thirdgridlayout.Children[10] as Label;

                        this.InspectionStartDate = ThirdStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(ThirdStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                        this.InspectionCompletionDate = ThirdCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(ThirdCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;
                    }


                    if (Device.Idiom == TargetIdiom.Tablet)
                    {
                        var employeecontrcatoridsforTablet3 = ThirdStacklayout.Children[0] as Label;
                        var employeecontrcatoridsforTabletsubstring3 = employeecontrcatoridsforTablet3.StyleId.Split(':');
                        string EmpCntID3 = Int32.Parse(employeecontrcatoridsforTabletsubstring3[0]).ToString();
                        string EmpCntValue3 = employeecontrcatoridsforTabletsubstring3[1];

                        if (EmpCntValue3 == "EmployeeLaborCraft")
                        {
                            EmpLaborCraftID3 = EmpCntID3;
                        }
                        if (EmpCntValue3 == "ContractorLaborCraft")
                        {
                            ContLaborCraftID3 = EmpCntID3;
                        }

                    }
                    else
                    {
                        var employeecontrcatoridssubstring3 = employeecontrcatorids3.Text.Split(':');
                        string EmpCntID3 = Int32.Parse(employeecontrcatoridssubstring3[0]).ToString();
                        string EmpCntValue3 = employeecontrcatoridssubstring3[1];

                        if (EmpCntValue3 == "EmployeeLaborCraft")
                        {
                            EmpLaborCraftID3 = EmpCntID3;
                        }
                        if (EmpCntValue3 == "ContractorLaborCraft")
                        {
                            ContLaborCraftID3 = EmpCntID3;
                        }


                    }

                    int Thirdminutes = 0;
                    int Thirdhours = 0;
                    if (string.IsNullOrWhiteSpace(ThirdHoursEntryValue.Text))
                    {

                    }
                    else
                    {
                        Thirdhours = Int32.Parse(ThirdHoursEntryValue.Text);
                    }

                    if (string.IsNullOrWhiteSpace(ThirdMinuteEntryValue.Text))
                    {

                    }
                    else
                    {
                        Thirdminutes = Int32.Parse(ThirdMinuteEntryValue.Text);
                    }


                    var Thirdresult = new TimeSpan(Thirdhours, Thirdminutes, 0);
                    totalTime = Thirdresult.TotalSeconds;
                    //this.InspectionStartDate = ThirdStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(ThirdStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                    //this.InspectionCompletionDate = ThirdCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(ThirdCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;

                    // this.InspectionStartDate = Convert.ToDateTime(ThirdStartdateValue.Date.ToString());
                    // this.InspectionCompletionDate = Convert.ToDateTime(ThirdCompletionDateValue.Date.ToString());
                    #region Local Validation

                    {
                        if (!String.IsNullOrWhiteSpace(this.InspectionStartDate.ToString()) && !String.IsNullOrWhiteSpace(this.InspectionCompletionDate.ToString()))
                        {
                            if (this.InspectionStartDate.GetValueOrDefault().Date > this.InspectionCompletionDate.GetValueOrDefault().Date)
                            {
                                //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, "Inspection", formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                                //await DisplayAlert("Alert", WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), "OK");
                                UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), TimeSpan.FromSeconds(2));
                                UserDialogs.Instance.HideLoading();
                                return;
                            }
                        }
                    }




                    #endregion
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
                    liststartAnswer.Add(new InspectionTOAnswers()
                    {

                        StartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,

                    });
                    listcompletionAnswer.Add(new InspectionTOAnswers()
                    {

                        CompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,

                    });
                }

            }
            catch (Exception ex)
            {


            }



            // 4th Employee/Contractor Layout///
            Label employeecontrcatorids4 = new Label();
            string EmpLaborCraftID4 = "0";
            string ContLaborCraftID4 = "0";
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
                    if (Device.Idiom == TargetIdiom.Phone)
                    {
                        var Fourthgridlayout2 = FourthStacklayout.Children[2] as Grid;

                        var FourthStartdateValue = Fourthgridlayout2.Children[1] as CustomDatePicker;
                        var FourthCompletionDateValue = Fourthgridlayout2.Children[3] as CustomDatePicker;
                        this.InspectionStartDate = FourthStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(FourthStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                        this.InspectionCompletionDate = FourthCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(FourthCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;
                        employeecontrcatorids4 = Fourthgridlayout2.Children[4] as Label;

                    }
                    else
                    {
                        var FourthStartdateValue = Fourthgridlayout.Children[7] as CustomDatePicker;
                        var FourthCompletionDateValue = Fourthgridlayout.Children[9] as CustomDatePicker;
                        employeecontrcatorids4 = Fourthgridlayout.Children[10] as Label;


                        this.InspectionStartDate = FourthStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(FourthStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                        this.InspectionCompletionDate = FourthCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(FourthCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;
                    }



                    if (Device.Idiom == TargetIdiom.Tablet)
                    {
                        var employeecontrcatoridsforTablet = FourthStacklayout.Children[0] as Label;
                        var employeecontrcatoridsforTabletsubstring = employeecontrcatoridsforTablet.StyleId.Split(':');
                        string EmpCntID4 = Int32.Parse(employeecontrcatoridsforTabletsubstring[0]).ToString();
                        string EmpCntValue4 = employeecontrcatoridsforTabletsubstring[1];

                        if (EmpCntValue4 == "EmployeeLaborCraft")
                        {
                            EmpLaborCraftID4 = EmpCntID4;
                        }
                        if (EmpCntValue4 == "ContractorLaborCraft")
                        {
                            ContLaborCraftID4 = EmpCntID4;
                        }

                    }
                    else
                    {
                        var employeecontrcatoridssubstring4 = employeecontrcatorids4.Text.Split(':');
                        string EmpCntID4 = Int32.Parse(employeecontrcatoridssubstring4[0]).ToString();
                        string EmpCntValue4 = employeecontrcatoridssubstring4[1];

                        if (EmpCntValue4 == "EmployeeLaborCraft")
                        {
                            EmpLaborCraftID4 = EmpCntID4;
                        }
                        if (EmpCntValue4 == "ContractorLaborCraft")
                        {
                            ContLaborCraftID4 = EmpCntID4;
                        }


                    }


                    int Fourthhours = 0;
                    int Fourthminutes = 0;

                    if (string.IsNullOrWhiteSpace(FourthHoursEntryValue.Text))
                    {

                    }
                    else
                    {
                        Fourthhours = Int32.Parse(FourthHoursEntryValue.Text);
                    }

                    if (string.IsNullOrWhiteSpace(FourthMinuteEntryValue.Text))
                    {

                    }
                    else
                    {
                        Fourthminutes = Int32.Parse(FourthMinuteEntryValue.Text);
                    }



                    var Fourthresult = new TimeSpan(Fourthhours, Fourthminutes, 0);
                    totalTime = Fourthresult.TotalSeconds;
                    //this.InspectionStartDate = FourthStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(FourthStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                    //this.InspectionCompletionDate = FourthCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(FourthCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;
                    //this.InspectionStartDate = Convert.ToDateTime(FourthStartdateValue.Date.ToString());
                    //this.InspectionCompletionDate = Convert.ToDateTime(FourthCompletionDateValue.Date.ToString());
                    #region Local Validation

                    {
                        if (!String.IsNullOrWhiteSpace(this.InspectionStartDate.ToString()) && !String.IsNullOrWhiteSpace(this.InspectionCompletionDate.ToString()))
                        {
                            if (this.InspectionStartDate.GetValueOrDefault().Date > this.InspectionCompletionDate.GetValueOrDefault().Date)
                            {
                                //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, "Inspection", formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                                //await DisplayAlert("Alert", WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), "OK");
                                UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), TimeSpan.FromSeconds(2));
                                UserDialogs.Instance.HideLoading();
                                return;
                            }
                        }
                    }




                    #endregion
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
                    liststartAnswer.Add(new InspectionTOAnswers()
                    {

                        StartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,

                    });
                    listcompletionAnswer.Add(new InspectionTOAnswers()
                    {

                        CompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,

                    });
                }

            }
            catch (Exception ex)
            {


            }



            // 5th Employee/Contractor Layout///
            Label employeecontrcatorids5 = new Label();
            string EmpLaborCraftID5 = "0";
            string ContLaborCraftID5 = "0";
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
                    if (Device.Idiom == TargetIdiom.Phone)
                    {
                        var Fifthgridlayout2 = FifthStacklayout.Children[2] as Grid;

                        var FifthStartdateValue = Fifthgridlayout2.Children[1] as CustomDatePicker;
                        var FifthCompletionDateValue = Fifthgridlayout2.Children[3] as CustomDatePicker;
                        this.InspectionStartDate = FifthStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(FifthStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                        this.InspectionCompletionDate = FifthCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(FifthCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;
                        employeecontrcatorids5 = Fifthgridlayout2.Children[4] as Label;

                    }
                    else
                    {
                        var FifthStartdateValue = Fifthgridlayout.Children[7] as CustomDatePicker;
                        var FifthCompletionDateValue = Fifthgridlayout.Children[9] as CustomDatePicker;
                        employeecontrcatorids5 = Fifthgridlayout.Children[10] as Label;



                        this.InspectionStartDate = FifthStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(FifthStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                        this.InspectionCompletionDate = FifthCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(FifthCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;
                    }



                    if (Device.Idiom == TargetIdiom.Tablet)
                    {
                        var employeecontrcatoridsforTablet = FifthStacklayout.Children[0] as Label;
                        var employeecontrcatoridsforTabletsubstring = employeecontrcatoridsforTablet.StyleId.Split(':');
                        string EmpCntID5 = Int32.Parse(employeecontrcatoridsforTabletsubstring[0]).ToString();
                        string EmpCntValue5 = employeecontrcatoridsforTabletsubstring[1];

                        if (EmpCntValue5 == "EmployeeLaborCraft")
                        {
                            EmpLaborCraftID5 = EmpCntID5;
                        }
                        if (EmpCntValue5 == "ContractorLaborCraft")
                        {
                            ContLaborCraftID5 = EmpCntID5;
                        }

                    }
                    else
                    {
                        var employeecontrcatoridssubstring5 = employeecontrcatorids5.Text.Split(':');
                        string EmpCntID5 = Int32.Parse(employeecontrcatoridssubstring5[0]).ToString();
                        string EmpCntValue5 = employeecontrcatoridssubstring5[1];

                        if (EmpCntValue5 == "EmployeeLaborCraft")
                        {
                            EmpLaborCraftID5 = EmpCntID5;
                        }
                        if (EmpCntValue5 == "ContractorLaborCraft")
                        {
                            ContLaborCraftID5 = EmpCntID5;
                        }


                    }


                    int Fifthhours = 0;
                    int Fifthminutes = 0;

                    if (string.IsNullOrWhiteSpace(FifthHoursEntryValue.Text))
                    {

                    }
                    else
                    {
                        Fifthhours = Int32.Parse(FifthHoursEntryValue.Text);
                    }

                    if (string.IsNullOrWhiteSpace(FifthMinuteEntryValue.Text))
                    {

                    }
                    else
                    {
                        Fifthminutes = Int32.Parse(FifthMinuteEntryValue.Text);
                    }



                    var Fifthresult = new TimeSpan(Fifthhours, Fifthminutes, 0);
                    totalTime = Fifthresult.TotalSeconds;
                    //this.InspectionStartDate = FifthStartdateValue.SelectedDate.HasValue ? Convert.ToDateTime(FifthStartdateValue.SelectedDate.ToString()) : (DateTime?)null;
                    //this.InspectionCompletionDate = FifthCompletionDateValue.SelectedDate.HasValue ? Convert.ToDateTime(FifthCompletionDateValue.SelectedDate.ToString()) : (DateTime?)null;

                    //this.InspectionStartDate = Convert.ToDateTime(FifthStartdateValue.Date.ToString());
                    //this.InspectionCompletionDate = Convert.ToDateTime(FifthCompletionDateValue.Date.ToString());
                    #region Local Validation

                    {
                        if (!String.IsNullOrWhiteSpace(this.InspectionStartDate.ToString()) && !String.IsNullOrWhiteSpace(this.InspectionCompletionDate.ToString()))
                        {
                            if (this.InspectionStartDate.GetValueOrDefault().Date > this.InspectionCompletionDate.GetValueOrDefault().Date)
                            {
                                //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, "Inspection", formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                                //await DisplayAlert("Alert", WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), "OK");
                                UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("InspectionStartDatecannotgreaterthanInspectionEnddate"), TimeSpan.FromSeconds(2));
                                UserDialogs.Instance.HideLoading();
                                return;
                            }
                        }
                    }




                    #endregion
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
                    liststartAnswer.Add(new InspectionTOAnswers()
                    {

                        StartDate = this.InspectionStartDate.HasValue ? this.InspectionStartDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,

                    });
                    listcompletionAnswer.Add(new InspectionTOAnswers()
                    {

                        CompletionDate = this.InspectionCompletionDate.HasValue ? this.InspectionCompletionDate.Value.Date.Add(DateTime.Now.TimeOfDay) : (DateTime?)null,

                    });
                }
            }
            catch (Exception ex)
            {


            }






            #endregion


            #region Validation from workorder


            ServiceOutput abc = await ViewModel._workorderService.GetWorkorderByWorkorderID(UserId, WorkorderID.ToString());

            string workordercompDate = string.Empty;
            string workorderstartDate = string.Empty;


            if (abc.workOrderWrapper.workOrder.WorkStartedDate != null)
                workorderstartDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(abc.workOrderWrapper.workOrder.WorkStartedDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("d");
            if (abc.workOrderWrapper.workOrder.CompletionDate != null)
                workordercompDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(abc.workOrderWrapper.workOrder.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("d");

            if (listtoAnswer != null && listtoAnswer.Count > 0)
            {
                MinimumInspectionStartDate = listtoAnswer.Min(i => i.StartDate);
                MaximumInspectionCompletionDate = listtoAnswer.Max(i => i.CompletionDate);

            }
            else
            {
                MinimumInspectionStartDate = null;
                MaximumInspectionCompletionDate = null;
            }

            liststartAnswer.RemoveAll(x => x.StartDate == null);
            listcompletionAnswer.RemoveAll(x => x.CompletionDate == null);
            #region Start date picker validation

            //replace this.PickerInspectionStartDate.Date with this.InspectionStartDate
            if (!string.IsNullOrWhiteSpace(this.MinimumInspectionStartDate.ToString()))
            {
                //// if inspection start date is before than wo start date the give alert >>> Inspection start date can not lesser than WO start date
                if (!string.IsNullOrWhiteSpace(workorderstartDate))
                {
                    if (workorderstartDate != null && liststartAnswer.Any(x => x.StartDate.Value.Date < DateTime.Parse(workorderstartDate)))
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
                    if (workordercompDate != null && liststartAnswer.Any(x => x.StartDate.Value.Date > DateTime.Parse(workordercompDate)))
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
            if (!string.IsNullOrWhiteSpace(this.MaximumInspectionCompletionDate.ToString()))
            {
                //// if inspection completion date is before than wo start date the give alert >>> Inspection completion date can not lesser than WO start date
                if (!string.IsNullOrWhiteSpace(workorderstartDate))
                {
                    if (workorderstartDate != null && listcompletionAnswer.Any(x => x.CompletionDate.Value.Date < DateTime.Parse(workorderstartDate)))
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
                    if (workordercompDate != null && listcompletionAnswer.Any(x => x.CompletionDate.Value.Date > DateTime.Parse(workordercompDate)))
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

            var yourobject1 = new InspectionTOAnswers
            {
                inspectionToAnswers = listtoAnswer,
                ClientIANATimeZone = DateTimeZoneProviders.Serialization.GetSystemDefault().ToString(),
                UserID = long.Parse(this.UserId),

            };


            ServiceOutput serviceStatus1 = await ViewModel._inspectionService.SaveWorkorderInspectionTime(yourobject1);

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

                foreach (var item in workOrderInspectionTimeID)
                {
                    string Employeekey = "WorkOrderEmployee:" + item.Value;
                    string Contractorkey = "WorkorderContracator:" + item.Value;
                    WorkorderInspectionStorge.Storage.Delete(Employeekey);
                    WorkorderInspectionStorge.Storage.Delete(Contractorkey);

                }
                workOrderInspectionTimeID.Clear();


            }

            // DeleteSavedTimer();
            UserDialogs.Instance.HideLoading();

            if (string.IsNullOrWhiteSpace(value))
            {
                UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("Pleasemakesureallfieldsarefilled"), TimeSpan.FromSeconds(2));
                //await DisplayAlert("", WebControlTitle.GetTargetNameByTitleName("Pleasemakesureallfieldsarefilled"), WebControlTitle.GetTargetNameByTitleName("OK"));

            }

            total = total.Subtract(total);
            MainLayout.Children.Clear();
            OnAppearing();


        }

        private async void BtnDelete_Clicked(object sender, EventArgs e)
        {
            var ssdv = (sender as Button).Parent as StackLayout;




            var kfm = ssdv.Children.Last() as Button;

            //  var kfm1 = ParentLayout.Children[0] as StackLayout;

            if (kfm == null)
            {
                return;
            }

            string MiscallanesousID = kfm.StyleId;

            //Delete SingleInspection From Workorder///
            Uri posturi = new Uri(AppSettings.BaseURL + "/Inspection/service/deleteWorkorderinspectionData");

            var payload = new Dictionary<string, string>
            {
              {"InspectionID", MiscallanesousID},
              {"WorkorderID", WorkorderID.ToString()},

            };

            string strPayload = JsonConvert.SerializeObject(payload);
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            var t = Task.Run(() => SendURI(posturi, c));

            OnAppearing();


        }
        private async void BtnSectionDelete_Clicked(object sender, EventArgs e)
        {

            var ssdv = (sender as Button).Parent as StackLayout;




            var kfm = ssdv.Children.Last() as Button;

            //  var kfm1 = ParentLayout.Children[0] as StackLayout;

            if (kfm == null)
            {
                return;
            }

            string FinalSectionID = kfm.StyleId;

            //Delete GroupInspection From Workorder///
            Uri posturi = new Uri(AppSettings.BaseURL + "/Inspection/service/deleteWorkordersectionData");

            var payload = new Dictionary<string, string>
            {
              {"SectionID", FinalSectionID},
              {"WorkorderID", WorkorderID.ToString()},

            };

            string strPayload = JsonConvert.SerializeObject(payload);
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            var t = Task.Run(() => SendURI(posturi, c));

            OnAppearing();

        }
        private async void BtnEmployeeDelete_Clicked(object sender, EventArgs e)
        {

            var EmployeeID = (sender as Button);


            string FinalEmployeeID = EmployeeID.StyleId;

            //Delete GroupInspection From Workorder///
            Uri posturi = new Uri(AppSettings.BaseURL + "/Inspection/service/DeleteEmployeeAndContrator");

            var payload = new Dictionary<string, string>
            {
              {"EmployeeLaborCraftID", FinalEmployeeID},
              {"WorkorderID", WorkorderID.ToString()},
               {"ContractorLaborCraftID", "0"},

            };

            string strPayload = JsonConvert.SerializeObject(payload);
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            var t = Task.Run(() => SendURI(posturi, c));

            //UserDialogs.Instance.HideLoading();
            // MainLayout.Children.Clear();

            OnAppearing();

        }
        private async void BtnContractorDelete_Clicked(object sender, EventArgs e)
        {

            var ContrcatorID = (sender as Button);


            string FinalContID = ContrcatorID.StyleId;

            //Delete GroupInspection From Workorder///
            Uri posturi = new Uri(AppSettings.BaseURL + "/Inspection/service/DeleteEmployeeAndContrator");

            var payload = new Dictionary<string, string>
            {
              {"EmployeeLaborCraftID", "0"},
              {"WorkorderID", WorkorderID.ToString()},
               {"ContractorLaborCraftID",FinalContID},

            };

            string strPayload = JsonConvert.SerializeObject(payload);
            HttpContent c = new StringContent(strPayload, Encoding.UTF8, "application/json");
            var t = Task.Run(() => SendURI(posturi, c));



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
            if (ParentLayout.Children.Count > 5)
            {
                UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("CannotAddMorethan5EmployeeORContractor"), TimeSpan.FromSeconds(2));
                return;
            }
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



                // OnAppearing();

            }


        }

        private void OnContractorRequested(object obj)
        {
            if (ParentLayout.Children.Count > 5)
            {
                UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("CannotAddMorethan5EmployeeORContractor"), TimeSpan.FromSeconds(2));
                return;
            }
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



                //OnAppearing();


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
        async void HoursTextChanged1(object sender, EventArgs e)
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
                UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("WronginputinMinutes."));
                e1.Text = "";
                return;
            }
            if (minuteValue > 59)
            {
                //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Minutesshouldbelessthen59").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                UserDialogs.Instance.Toast(WebControlTitle.GetTargetNameByTitleName("Minutesshouldbelessthen59"));
                e1.Text = "";
                return;
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

