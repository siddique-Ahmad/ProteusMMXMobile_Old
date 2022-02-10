using Acr.UserDialogs;
using Newtonsoft.Json;
using ProteusMMX.Constants;
using ProteusMMX.Controls;
using ProteusMMX.Converters;
using ProteusMMX.DependencyInterface;
using ProteusMMX.Helpers;
using ProteusMMX.Helpers.DateTime;
using ProteusMMX.Helpers.Storage;
using ProteusMMX.Helpers.StringFormatter;
using ProteusMMX.Helpers.Validation;
using ProteusMMX.Model;
using ProteusMMX.Model.CommonModels;
using ProteusMMX.Model.WorkOrderModel;
using ProteusMMX.Services.Authentication;
using ProteusMMX.Services.FormLoadInputs;
using ProteusMMX.Services.Workorder;
using ProteusMMX.Services.Workorder.Inspection;
using ProteusMMX.Services.Workorder.TaskAndLabour;
using ProteusMMX.Utils;
using ProteusMMX.ViewModel.Miscellaneous;
using ProteusMMX.ViewModel.SelectionListPagesViewModels;
using Syncfusion.XForms.Border;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;


namespace ProteusMMX.ViewModel.Workorder
{


    public class TaskAndLabourPageViewModel : ViewModelBase, IHandleViewAppearing, IHandleViewDisappearing
    {
        #region Fields

        protected readonly IAuthenticationService _authenticationService;

        protected readonly IFormLoadInputService _formLoadInputService;

        protected readonly IWorkorderService _workorderService;
        public readonly IInspectionService _inspectionService;

        protected readonly ITaskAndLabourService _taskAndLabourService;
        #endregion

        #region Properties


        #region Page Properties

        string LabourEstimatedHours = string.Empty;

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
        bool _disabledTextIsEnable = false;
        public bool DisabledTextIsEnable
        {
            get
            {
                return _disabledTextIsEnable;
            }

            set
            {
                if (value != _disabledTextIsEnable)
                {
                    _disabledTextIsEnable = value;
                    OnPropertyChanged(nameof(DisabledTextIsEnable));
                }
            }
        }
        bool _taskLabourSearchBoxIsEnable = true;
        public bool TaskLabourSearchBoxIsEnable
        {
            get
            {
                return _taskLabourSearchBoxIsEnable;
            }

            set
            {
                if (value != _taskLabourSearchBoxIsEnable)
                {
                    _taskLabourSearchBoxIsEnable = value;
                    OnPropertyChanged(nameof(TaskLabourSearchBoxIsEnable));
                }
            }
        }
        bool _taskLabourSearchButtonIsEnable = true;
        public bool TaskLabourSearchButtonIsEnable
        {
            get
            {
                return _taskLabourSearchButtonIsEnable;
            }

            set
            {
                if (value != _taskLabourSearchButtonIsEnable)
                {
                    _taskLabourSearchButtonIsEnable = value;
                    OnPropertyChanged(nameof(TaskLabourSearchButtonIsEnable));
                }
            }
        }
        string _disabledText = "";
        public string DisabledText
        {
            get
            {
                return _disabledText;
            }

            set
            {
                if (value != _disabledText)
                {
                    _disabledText = value;
                    OnPropertyChanged("DisabledText");
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

        int? _workOrderLaborHourID;
        public int? WorkOrderLaborHourID
        {
            get
            {
                return _workOrderLaborHourID;
            }

            set
            {
                if (value != _workOrderLaborHourID)
                {
                    _workOrderLaborHourID = value;
                    OnPropertyChanged(nameof(WorkOrderLaborHourID));
                }
            }
        }

        int? _workOrderLaborHour2ID;
        public int? WorkOrderLaborHour2ID
        {
            get
            {
                return _workOrderLaborHour2ID;
            }

            set
            {
                if (value != _workOrderLaborHour2ID)
                {
                    _workOrderLaborHour2ID = value;
                    OnPropertyChanged(nameof(WorkOrderLaborHour2ID));
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




        #endregion


        string CreateTask;
        string WOLabourTime;


        #region Commands
        public ICommand ToolbarCommand => new AsyncCommand(ShowActions);

        public ICommand ScanCommand => new AsyncCommand(SearchWorkorder);

        #endregion

        #region Methods
        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                OperationInProgress = true;

                if (navigationData != null)
                {
                    var navigationParams = navigationData as PageParameters;
                    this.Page = navigationParams.Page;

                    var workorder = navigationParams.Parameter as workOrders;
                    this.WorkorderID = workorder.WorkOrderID;
                }

                await SetTitlesPropertiesForPage();
            
                ServiceOutput InspectionList = await _inspectionService.GetWorkorderInspection(this.WorkorderID.ToString(), AppSettings.User.UserID.ToString());
                if (InspectionList.listInspection != null && InspectionList.listInspection.Count > 0)
                {
                    DisabledText = WebControlTitle.GetTargetNameByTitleName("ThisTabisDisabled");
                    DisabledTextIsEnable = true;
                    TaskLabourSearchBoxIsEnable = false;
                    TaskLabourSearchButtonIsEnable = false;
                    //var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() { LogoutTitle });
                    //if (response == LogoutTitle)
                    //{
                    //  await _authenticationService.LogoutAsync();
                    //  await NavigationService.NavigateToAsync<LoginPageViewModel>();
                    //  await NavigationService.RemoveBackStackAsync();
                    //}

                    return;
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

        public TaskAndLabourPageViewModel(IAuthenticationService authenticationService, IFormLoadInputService formLoadInputService, IWorkorderService workorderService, ITaskAndLabourService taskAndLabourService, IInspectionService inspectionservice)
        {
            _authenticationService = authenticationService;
            _formLoadInputService = formLoadInputService;
            _workorderService = workorderService;
            _taskAndLabourService = taskAndLabourService;
            _inspectionService = inspectionservice;
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
                    //WelcomeTextTitle = WebControlTitle.GetTargetNameByTitleName("Welcome") + " " + AppSettings.UserName;
                    LogoutTitle = WebControlTitle.GetTargetNameByTitleName("Logout");
                    CancelTitle = WebControlTitle.GetTargetNameByTitleName("Cancel");
                    SelectTitle = WebControlTitle.GetTargetNameByTitleName("Select");
                    //SelectOptionsTitle = WebControlTitle.GetTargetNameByTitleName(titles, "SelectOptionsTitles"); 



                    AddTaskTitle = WebControlTitle.GetTargetNameByTitleName("AddTask");

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
                if (Application.Current.Properties.ContainsKey("LabourEstimatedHours"))
                {
                    LabourEstimatedHours = Application.Current.Properties["LabourEstimatedHours"].ToString();

                }

                StackLayout contentLayout = await GetContentLayout();
                ServiceOutput workorderLabour = null;
                //clear the contentLayout 
                contentLayout.Children.Clear();


                ///TODO: Get Workorder data 
                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    workorderLabour = await _taskAndLabourService.WorkOrderLaborsByWorkOrderIDAndTaskNumber(UserID, WorkorderID.ToString(), SearchText);
                }
                else
                {
                    workorderLabour = await _taskAndLabourService.WorkOrderLaborsByWorkOrderID(UserID, WorkorderID.ToString());
                }


                if (workorderLabour != null && workorderLabour.workOrderWrapper != null && workorderLabour.workOrderWrapper.workOrderLabors != null && workorderLabour.workOrderWrapper.workOrderLabors.Count > 0)
                {

                    foreach (var item in workorderLabour.workOrderWrapper.workOrderLabors)
                    {

                        StackLayout MasterstackLayout = new StackLayout();
                        StackLayout frameStackLayout = new StackLayout
                        {
                            Padding = new Thickness(3, 0, 3, 0)
                        };
                        MasterstackLayout.Children.Add(frameStackLayout);
                        Frame frame = new Frame
                        {
                            BackgroundColor = Color.White,
                            BorderColor = Color.Black,
                            CornerRadius = 10
                        };
                        frameStackLayout.Children.Add(frame);

                        Grid MainGrid = new Grid
                        {
                            Padding = new Thickness(-10, -10, -15, -10)
                        };
                        MainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        MainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        MainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        MainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        MainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        MainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        frame.Content = MainGrid;

                        #region ***** EmployeeName ******

                        var contractorNameTitle = WebControlTitle.GetTargetNameByTitleName("ContractorName") + ": ";
                        var employeeNameTitle = WebControlTitle.GetTargetNameByTitleName("EmployeeName") + ": ";

                        string EmployeeName = string.Empty;

                        if (!String.IsNullOrEmpty(item.EmployeeName))
                        {
                            EmployeeName = employeeNameTitle + item.EmployeeName + "(" + item.LaborCraftCode + ")";
                        }
                        else if (!String.IsNullOrEmpty(item.ContractorName))
                        {
                            EmployeeName = contractorNameTitle + item.ContractorName + "(" + item.LaborCraftCode + ")";
                        }

                        Label EName = new Label
                        {
                            Text = EmployeeName,
                            FontAttributes = FontAttributes.Bold,
                            Margin = new Thickness(0, 0, 0, 2),
                            TextColor = Color.FromHex("#006de0"),
                            FontSize = 14.72,
                            FontFamily = "Ebrima"
                        };
                        MainGrid.Children.Add(EName, 0, 0);
                        #endregion

                        #region **** Task Number and Estimated Hors ****

                        StackLayout TaskStackLayout = new StackLayout();
                        MainGrid.Children.Add(TaskStackLayout, 0, 1);
                        Grid Taskgrid = new Grid();
                        Taskgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        Taskgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Absolute) });
                        Taskgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                        Taskgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        Taskgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(2, GridUnitType.Absolute) });
                        Taskgrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                        Label TaskNumber = new Label
                        {
                            FontSize = 13,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromHex("#333333"),
                            Text = WebControlTitle.GetTargetNameByTitleName("TaskNumber")
                        };
                        Taskgrid.Children.Add(TaskNumber, 0, 0);

                        Label TNDot = new Label
                        {
                            FontSize = 13,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromHex("#333333"),
                            Text = ":"
                        };
                        Taskgrid.Children.Add(TNDot, 1, 0);

                        Label TaskNumberVal = new Label
                        {
                            TextColor = Color.FromHex("#333333"),
                            Text = ShortString.shor17ten(item.TaskNumber)
                        };

                        Taskgrid.Children.Add(TaskNumberVal, 2, 0);
                        Grid.SetColumnSpan(TaskNumberVal, 3);

                        if (LabourEstimatedHours == "N")
                        {

                        }
                        else
                        {
                            Label EstHourse = new Label
                            {
                                FontSize = 13,
                                FontAttributes = FontAttributes.Bold,
                                TextColor = Color.FromHex("#333333"),
                                Text = WebControlTitle.GetTargetNameByTitleName("EstimatedHours")
                            };
                            Taskgrid.Children.Add(EstHourse, 3, 0);

                            Label EstDot = new Label
                            {
                                FontSize = 13,
                                FontAttributes = FontAttributes.Bold,
                                TextColor = Color.FromHex("#333333"),
                                Text = ":"
                            };
                            Taskgrid.Children.Add(EstDot, 4, 0);

                            Label EstHourseVal = new Label
                            {
                                TextColor = Color.FromHex("#333333"),
                                Text = string.Format(StringFormat.NumericZero(), item.EstimatedHours)
                            };

                            Taskgrid.Children.Add(EstHourseVal, 5, 0);
                            TaskStackLayout.Children.Add(Taskgrid);
                        }
                        #endregion

                        #region **** Description ****

                        StackLayout DescriptionStackLayout = new StackLayout();
                        MainGrid.Children.Add(DescriptionStackLayout, 0, 2);

                        Label Description = new Label
                        {
                            FontSize = 13,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromHex("#333333"),
                            Text = WebControlTitle.GetTargetNameByTitleName("Description") + ": "
                        };
                        DescriptionStackLayout.Children.Add(Description);

                        SfBorder DescriptionBorder = new SfBorder
                        {
                            CornerRadius = 5,
                            BorderColor = Color.Black
                        };

                        StackLayout DescStackLayout = new StackLayout();
                        MainGrid.Children.Add(DescStackLayout, 0, 2);
                        bool MoreTextIsEnable = false;
                        if (!string.IsNullOrWhiteSpace(item.Description))
                        {
                            if (item.Description.Length >= 90)
                            {
                                MoreTextIsEnable = true;

                            }
                        }
                        if (String.IsNullOrEmpty(item.Description))
                        {
                            CustomEntry DescEntry = new CustomEntry()
                            {
                                HeightRequest = 40,
                                BackgroundColor = Color.White,
                                TextColor = Color.Black,
                                Text = item.Description,
                                FontSize = 13,
                                Margin = new Thickness(0, -10, 0, 0)
                            };
                            DescStackLayout.Children.Add(DescEntry);
                        }
                        else
                        {
                            string result = RemoveHTML.StripHtmlTags(item.Description);
                            CustomEntry DescEntry = new CustomEntry()
                            {
                                HeightRequest = 40,

                                TextColor = Color.Black,
                                Text = result,
                                FontSize = 13,
                                BackgroundColor = Color.White,
                                Margin = new Thickness(0, 0, 0, 0)
                            };
                            DescStackLayout.Children.Add(DescEntry);

                        }



                        Label more = new Label
                        {
                            IsVisible = MoreTextIsEnable,
                            HorizontalOptions = LayoutOptions.End,
                            TextColor = Color.FromHex("#006de0"),
                            Text = WebControlTitle.GetTargetNameByTitleName("More"),
                            FontAttributes = FontAttributes.Bold
                        };

                        DescStackLayout.Children.Add(more);
                        DescriptionBorder.Content = DescStackLayout;
                        DescriptionStackLayout.Children.Add(DescriptionBorder);
                        TargetNavigationData tnobj = new TargetNavigationData();
                        string result1 = RemoveHTML.StripHtmlTags(item.Description);
                        tnobj.Description = result1;
                        var tapRecognizer = new TapGestureRecognizer { NumberOfTapsRequired = 1, Command = new Command(async () => await NavigationService.NavigateToAsync<DescriptionViewModel>(tnobj)) };

                        tapRecognizer.Tapped += (sender, e) =>
                        {

                        };


                        more.GestureRecognizers.Add(tapRecognizer);
                        #endregion

                        #region ***** Button And Hrs And Min ****
                        StackLayout buttnoStackLayout = new StackLayout();

                        MainGrid.Children.Add(buttnoStackLayout, 0, 3);

                        Grid hrsMin = new Grid();
                        hrsMin.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        hrsMin.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
                        buttnoStackLayout.Children.Add(hrsMin);

                        StackLayout buttnoStackLayoutHrs = new StackLayout
                        {
                            Padding = new Thickness(0, -7, 0, 0)
                        };
                        Grid BMainGrid = new Grid();
                        BMainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                        BMainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                        BMainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                        BMainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        BMainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                        hrsMin.Children.Add(buttnoStackLayoutHrs, 0, 0);
                        Label Hrs1 = new Label
                        {
                            FontSize = 13,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromHex("#333333"),
                            Text = "HourAtRate1"
                        };
                        buttnoStackLayoutHrs.Children.Add(Hrs1);
                        buttnoStackLayoutHrs.Children.Add(BMainGrid);

                        #region ***** Start Button ****

                        StackLayout startButtonStackLayout = new StackLayout
                        {
                            Margin = new Thickness(-6, 0, 0, 0)
                        };
                        BMainGrid.Children.Add(startButtonStackLayout, 0, 0);
                        Grid startButtonGrid = new Grid();
                        startButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                        SfButton startButton;
                        if (item.Timer1 != null && item.Timer1 == 1)
                        {
                            startButton = new SfButton
                            {
                                Text = WebControlTitle.GetTargetNameByTitleName("Start"),
                                FontAttributes = FontAttributes.Bold,
                                ShowIcon = true,
                                BackgroundColor = Color.Transparent,
                                CommandParameter = item,
                                TextColor = Color.Green,
                                ImageSource = "starticon1.png",
                                IsEnabled = false,

                            };
                        }
                        else
                        {
                            startButton = new SfButton
                            {
                                Text = WebControlTitle.GetTargetNameByTitleName("Start"),
                                FontAttributes = FontAttributes.Bold,
                                ShowIcon = true,
                                BackgroundColor = Color.Transparent,
                                CommandParameter = item,
                                TextColor = Color.Black,
                                ImageSource = "starticon.png",
                                IsEnabled = true,
                            };
                        }

                        startButtonGrid.Children.Add(startButton, 0, 0);
                        startButtonStackLayout.Children.Add(startButtonGrid);

                        #endregion

                        #region ***** Stop Button ****

                        StackLayout StopButtonStackLayout = new StackLayout
                        {
                            Margin = new Thickness(-20, 0, 0, 0)
                        };
                        BMainGrid.Children.Add(StopButtonStackLayout, 1, 0);
                        Grid StopButtonGrid = new Grid();
                        StopButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        SfButton stopButton;

                        if (item.Timer1 != null && item.Timer1 == 1)
                        {
                            stopButton = new SfButton
                            {
                                Text = WebControlTitle.GetTargetNameByTitleName("Stop"),
                                FontAttributes = FontAttributes.Bold,
                                ShowIcon = true,
                                BackgroundColor = Color.Transparent,
                                TextColor = Color.FromHex("#87CEFA"),
                                CommandParameter = item,
                                ImageSource = "stoppending.png",
                                IsEnabled = true
                            };
                        }
                        
                        else
                        {
                            stopButton = new SfButton
                            {
                                Text = WebControlTitle.GetTargetNameByTitleName("Stop"),
                                FontAttributes = FontAttributes.Bold,
                                ShowIcon = true,
                                BackgroundColor = Color.Transparent,
                                TextColor = Color.Black,
                                CommandParameter = item,
                                ImageSource = "stopicon.png",
                                IsEnabled = false,
                            };
                        }
                        StopButtonGrid.Children.Add(stopButton, 0, 0);
                        StopButtonStackLayout.Children.Add(StopButtonGrid);

                        #endregion

                        #region ***** complate Button ****

                        StackLayout complateButtonStackLayout = new StackLayout
                        {
                            Margin = new Thickness(-35, 0, 0, 0)
                        };
                        BMainGrid.Children.Add(complateButtonStackLayout, 2, 0);
                        Grid complateButtonGrid = new Grid();
                        complateButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        SfButton completeButton;

                        if (item.Timer1 != null && (item.Timer1 == 2 || item.Timer1 == 1))
                        {
                            completeButton = new SfButton
                            {
                                Text = WebControlTitle.GetTargetNameByTitleName("Complete"),
                                FontAttributes = FontAttributes.Bold,
                                ShowIcon = true,
                                BackgroundColor = Color.Transparent,
                                TextColor = Color.FromHex("#879afa"),
                                CommandParameter = item,
                                ImageSource = "complateicon.png",
                                IsEnabled = true,
                            };
                        }
                        
                        else
                        {
                            completeButton = new SfButton
                            {
                                Text = WebControlTitle.GetTargetNameByTitleName("Complete"),
                                FontAttributes = FontAttributes.Bold,
                                ShowIcon = true,
                                BackgroundColor = Color.Transparent,
                                TextColor = Color.Black,
                                CommandParameter = item,
                                ImageSource = "complateicon.png",
                                IsEnabled = false,
                            };
                        }
                        complateButtonGrid.Children.Add(completeButton, 0, 0);
                        complateButtonStackLayout.Children.Add(complateButtonGrid);

                        #endregion

                        #region **** Hrs *****
                        StackLayout HrsStackLayout = new StackLayout();
                        BMainGrid.Children.Add(HrsStackLayout, 3, 0);
                        Grid HrsGrid = new Grid();
                        HrsGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        HrsGrid.RowDefinitions.Add(new RowDefinition { Height = 40 });
                        SfBorder HrsBorder = new SfBorder
                        {
                            BorderColor = Color.Black,
                            CornerRadius = 10,
                        };
                        HrsStackLayout.Children.Add(HrsGrid);
                        HrsGrid.Children.Add(HrsBorder, 0, 0);
                        CustomEntry hoursEntry;
                        if (item.Timer1 != null && (item.Timer1 == 1))
                        {
                            hoursEntry = new CustomEntry
                            {
                                Placeholder = "Hrs",
                                FontSize = 14,
                                WidthRequest = 45,
                                IsReadOnly = true,
                            };
                        }
                        else
                        {
                            hoursEntry = new CustomEntry
                            {
                                Placeholder = "Hrs",
                                FontSize = 14,
                                WidthRequest = 45,
                            };
                        }

                        HrsBorder.Content = hoursEntry;
                        #endregion

                        #region **** Min *****
                        StackLayout MinStackLayout = new StackLayout();
                        BMainGrid.Children.Add(MinStackLayout, 4, 0);
                        Grid MinGrid = new Grid();
                        MinGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        MinGrid.RowDefinitions.Add(new RowDefinition { Height = 40 });
                        SfBorder MinBorder = new SfBorder
                        {
                            BorderColor = Color.Black,
                            CornerRadius = 10,

                        };
                        MinStackLayout.Children.Add(MinGrid);
                        MinGrid.Children.Add(MinBorder, 0, 0);
                        CustomEntry minuteEntry;
                        if (item.Timer1 != null && item.Timer1 == 1)
                        {
                            minuteEntry = new CustomEntry
                            {
                                Placeholder = "Min",
                                FontSize = 14,
                                WidthRequest = 45,
                                IsReadOnly = true,
                            };
                        }
                        else
                        {
                            minuteEntry = new CustomEntry
                            {
                                Placeholder = "Min",
                                FontSize = 14,
                                WidthRequest = 45
                            };
                        }

                        MinBorder.Content = minuteEntry;
                        #endregion
                        var completeDateButton = new Button();

                        #endregion

                        #region ***** Button And Hrs And MinforRate2 ****
                        StackLayout startStopButtonGridHoursforRate2 = new StackLayout();
                        startStopButtonGridHoursforRate2.Padding = new Thickness(0, -7, 0, 0);
                        hrsMin.Children.Add(startStopButtonGridHoursforRate2, 0, 1);
                        Grid BMainGridforRate2 = new Grid();
                        BMainGridforRate2.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                        BMainGridforRate2.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                        BMainGridforRate2.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                        BMainGridforRate2.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        BMainGridforRate2.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        Label Hrs2 = new Label
                        {
                            FontSize = 13,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromHex("#333333"),
                            Text = "HourAtRate2"
                        };
                        startStopButtonGridHoursforRate2.Children.Add(Hrs2);
                        startStopButtonGridHoursforRate2.Children.Add(BMainGridforRate2);

                        #region ***** Start Button Rate2 ****

                        StackLayout startButtonStackLayoutforRate2 = new StackLayout
                        {
                            Margin = new Thickness(-6, 0, 0, 0)
                        };
                        BMainGridforRate2.Children.Add(startButtonStackLayoutforRate2, 0, 0);
                        Grid startButtonGridforRate2 = new Grid();
                        startButtonGridforRate2.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                        SfButton startButtonforRate2;

                        if (item.Timer2 != null && item.Timer2 == 4)
                        {
                            startButtonforRate2 = new SfButton
                            {
                                Text = WebControlTitle.GetTargetNameByTitleName("Start"),
                                FontAttributes = FontAttributes.Bold,
                                ShowIcon = true,
                                BackgroundColor = Color.Transparent,
                                CommandParameter = item,
                                TextColor = Color.Green,
                                ImageSource = "starticon1.png",
                                IsEnabled = false,
                            };
                        }
                        else
                        {
                            startButtonforRate2 = new SfButton
                            {
                                Text = WebControlTitle.GetTargetNameByTitleName("Start"),
                                FontAttributes = FontAttributes.Bold,
                                ShowIcon = true,
                                BackgroundColor = Color.Transparent,
                                CommandParameter = item,
                                TextColor = Color.Black,
                                ImageSource = "starticon.png",
                                IsEnabled = true,
                            };
                        }
                        startButtonGridforRate2.Children.Add(startButtonforRate2, 0, 0);
                        startButtonStackLayoutforRate2.Children.Add(startButtonGridforRate2);



                        #endregion

                        #region ***** Stop ButtonforRate2 ****

                        StackLayout StopButtonStackLayoutforRate2 = new StackLayout
                        {
                            Margin = new Thickness(-20, 0, 0, 0)
                        };
                        BMainGridforRate2.Children.Add(StopButtonStackLayoutforRate2, 1, 0);
                        Grid StopButtonGridforRate2 = new Grid();
                        StopButtonGridforRate2.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        SfButton stopButtonforRate2;


                        if (item.Timer2 != null && item.Timer2 == 4)
                        {
                            stopButtonforRate2 = new SfButton
                            {
                                Text = WebControlTitle.GetTargetNameByTitleName("Stop"),
                                FontAttributes = FontAttributes.Bold,
                                ShowIcon = true,
                                BackgroundColor = Color.Transparent,
                                TextColor = Color.FromHex("#87CEFA"),
                                CommandParameter = item,
                                ImageSource = "stoppending.png",
                                IsEnabled = true
                            };
                        }
                      
                        else
                        {
                            stopButtonforRate2 = new SfButton
                            {
                                Text = WebControlTitle.GetTargetNameByTitleName("Stop"),
                                FontAttributes = FontAttributes.Bold,
                                ShowIcon = true,
                                BackgroundColor = Color.Transparent,
                                TextColor = Color.Black,
                                CommandParameter = item,
                                ImageSource = "stopicon.png",
                                IsEnabled = false,
                            };
                        }
                        StopButtonGridforRate2.Children.Add(stopButtonforRate2, 0, 0);
                        StopButtonStackLayoutforRate2.Children.Add(StopButtonGridforRate2);

                        #endregion

                        #region ***** complate ButtonforRate2 ****

                        StackLayout complateButtonStackLayoutforRate2 = new StackLayout
                        {
                            Margin = new Thickness(-35, 0, 0, 0)
                        };
                        BMainGridforRate2.Children.Add(complateButtonStackLayoutforRate2, 2, 0);
                        Grid complateButtonGridforRate2 = new Grid();
                        complateButtonGridforRate2.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        SfButton completeButtonforRate2;
                        if (item.Timer1 != null && (item.Timer2 == 5 || item.Timer2 == 4))
                        {
                             completeButtonforRate2 = new SfButton
                            {
                                Text = WebControlTitle.GetTargetNameByTitleName("Complete"),
                                FontAttributes = FontAttributes.Bold,
                                ShowIcon = true,
                                BackgroundColor = Color.Transparent,
                                TextColor = Color.FromHex("#879afa"),
                                CommandParameter = item,
                                ImageSource = "complateicon.png",
                                 IsEnabled = true,
                             };
                        }
                        
                        else
                        {
                            completeButtonforRate2 = new SfButton
                            {
                                Text = WebControlTitle.GetTargetNameByTitleName("Complete"),
                                FontAttributes = FontAttributes.Bold,
                                ShowIcon = true,
                                BackgroundColor = Color.Transparent,
                                TextColor = Color.Black,
                                CommandParameter = item,
                                ImageSource = "complateicon.png",
                                IsEnabled = false,
                            };
                        }
                        complateButtonGridforRate2.Children.Add(completeButtonforRate2, 0, 0);
                        complateButtonStackLayoutforRate2.Children.Add(complateButtonGridforRate2);

                        #endregion

                        #region **** HrsforRate2 *****
                        StackLayout HrsStackLayoutforRate2 = new StackLayout();

                        BMainGridforRate2.Children.Add(HrsStackLayoutforRate2, 3, 0);
                        Grid HrsGridforRate2 = new Grid();
                        HrsGridforRate2.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        HrsGridforRate2.RowDefinitions.Add(new RowDefinition { Height = 40 });
                        SfBorder HrsBorderforRate2 = new SfBorder
                        {
                            BorderColor = Color.Black,
                            CornerRadius = 10,
                        };

                        HrsStackLayoutforRate2.Children.Add(HrsGridforRate2);
                        HrsGridforRate2.Children.Add(HrsBorderforRate2, 0, 0);
                        CustomEntry hoursEntryforRate2;

                        if (item.Timer2 != null && item.Timer2 == 4)
                        {
                            hoursEntryforRate2 = new CustomEntry
                            {
                                Placeholder = "Hrs",
                                FontSize = 14,
                                WidthRequest = 45,
                                IsReadOnly=true,
                            };
                        }
                        else
                        {
                            hoursEntryforRate2 = new CustomEntry
                            {
                                Placeholder = "Hrs",
                                FontSize = 14,
                                WidthRequest = 45,
                            };
                        }
                        HrsBorderforRate2.Content = hoursEntryforRate2;
                        #endregion

                        #region **** Min Rate2*****
                        StackLayout MinStackLayoutforRate2 = new StackLayout();
                        BMainGridforRate2.Children.Add(MinStackLayoutforRate2, 4, 0);
                        Grid MinGridforRate2 = new Grid();
                        MinGridforRate2.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
                        MinGridforRate2.RowDefinitions.Add(new RowDefinition { Height = 40 });
                        SfBorder MinBorderforRate2 = new SfBorder
                        {
                            BorderColor = Color.Black,
                            CornerRadius = 10,

                        };
                        MinStackLayoutforRate2.Children.Add(MinGridforRate2);
                        MinGridforRate2.Children.Add(MinBorderforRate2, 0, 0);
                        CustomEntry minuteEntryforRate2;
                        if (item.Timer2 != null && item.Timer2 == 4)
                        {
                            minuteEntryforRate2 = new CustomEntry
                            {
                                Placeholder = "Min",
                                FontSize = 14,
                                WidthRequest = 45,
                                IsReadOnly = true
                            };
                        }
                        else
                        {
                            minuteEntryforRate2 = new CustomEntry
                            {
                                Placeholder = "Min",
                                FontSize = 14,
                                WidthRequest = 45
                            };
                        }


                        MinBorderforRate2.Content = minuteEntryforRate2;
                        #endregion

                        if (AppSettings.User.EnableHoursAtRate == false)
                        {
                            //MainGrid.Children[4].IsVisible = false;
                            startStopButtonGridHoursforRate2.IsVisible = false;
                        }

                        #endregion

                        #region From Date And Button
                        StackLayout FromStackLayout = new StackLayout();
                        MainGrid.Children.Add(buttnoStackLayout, 0, 5);
                        Grid FromMainGrid = new Grid();
                        FromMainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                        FromMainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Star });
                        FromMainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = 90 });

                        buttnoStackLayout.Children.Add(FromMainGrid);

                        #region **** Start Date ****
                        StackLayout FromDateStackLayout = new StackLayout();
                        FromMainGrid.Children.Add(FromDateStackLayout, 0, 0);
                        Label FDate = new Label
                        {
                            Text = WebControlTitle.GetTargetNameByTitleName("StartDate"),
                            FontSize = 13,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromHex("#333333")
                        };
                        FromDateStackLayout.Children.Add(FDate);
                        SfBorder FsfBorder = new SfBorder
                        {
                            HeightRequest = 35,
                            BorderColor = Color.Black,
                            CornerRadius = 10
                        };
                        FromDateStackLayout.Children.Add(FsfBorder);
                        RequiredDateCustomDatePicker fromEntry = new RequiredDateCustomDatePicker
                        {
                            SelectedDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone),
                            MaximumDate = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).Date,

                            //FontSize = 11
                        };
                        fromEntry.Margin = new Thickness(0, 5, 0, 0);
                        FsfBorder.Content = fromEntry;
                        #endregion

                        #region **** CmpDate ****
                        StackLayout CmpDateStackLayout = new StackLayout();
                        FromMainGrid.Children.Add(CmpDateStackLayout, 1, 0);
                        Label CDate = new Label
                        {
                            Text = WebControlTitle.GetTargetNameByTitleName("CompletionDate"),
                            FontSize = 13,
                            FontAttributes = FontAttributes.Bold,
                            TextColor = Color.FromHex("#333333")
                        };
                        CmpDateStackLayout.Children.Add(CDate);
                        //SfBorder CsfBorder = new SfBorder
                        //{
                        //    HeightRequest = 35,
                        //    BorderColor = Color.Black,
                        //    CornerRadius = 10
                        //};

                        completeDateButton.BackgroundColor = Color.White;
                        completeDateButton.BorderColor = Color.Black;
                        completeDateButton.BorderWidth = 1;
                        completeDateButton.CornerRadius = 10;
                        completeDateButton.TextColor = Color.Black;
                        completeDateButton.FontSize = 13;
                        completeDateButton.HeightRequest = 35;
                        completeDateButton.Margin = new Thickness(0, 0, 0, 0);

                        CmpDateStackLayout.Children.Add(completeDateButton);
                        // CsfBorder.Content = completeDateButton;
                        #endregion

                        #region ****** Save Button *****
                        StackLayout SbtnStackLayout = new StackLayout();
                        FromMainGrid.Children.Add(SbtnStackLayout, 2, 0);
                        SfBorder SbtnsfBorder = new SfBorder
                        {
                            HeightRequest = 35,
                            Margin = new Thickness(0, 24, 0, 0),
                            BorderColor = Color.Black,
                            CornerRadius = 10
                        };
                        SbtnStackLayout.Children.Add(SbtnsfBorder);

                        SfButton saveButton = new SfButton
                        {
                            Text = "Save",
                            FontAttributes = FontAttributes.Bold,
                            ShowIcon = true,
                            BackgroundColor = Color.White,
                            TextColor = Color.Black,
                            WidthRequest = 20,
                            CommandParameter = item,
                            ImageSource = "saveicon.png"
                        };
                        SbtnsfBorder.Content = saveButton;
                        #endregion

                        #endregion

                        #region **** Hrs and min ****
                        try
                        {
                            string FinalHours = Convert.ToDecimal(string.Format("{0:F2}", item.HoursAtRate1)).ToString();
                            var FinalHrs1 = FinalHours.Split('.');
                            hoursEntry.Text = FinalHrs1[0];
                            minuteEntry.Text = FinalHrs1[1];

                            string FinalHours2 = Convert.ToDecimal(string.Format("{0:F2}", item.HoursAtRate2)).ToString();
                            var FinalHrs2 = FinalHours2.Split('.');
                            hoursEntryforRate2.Text = FinalHrs2[0];
                            minuteEntryforRate2.Text = FinalHrs2[1];

                            if (item.CompletionDate != null)
                            {
                                completeDateButton.Text = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString();
                            }


                        }
                        catch (Exception ex)
                        {

                        }
                        hoursEntry.TextChanged += OnTextChanged1;
                        minuteEntry.TextChanged += HoursTextChanged;
                        hoursEntryforRate2.TextChanged += OnTextChanged1;
                        minuteEntryforRate2.TextChanged += HoursTextChanged;
                        #endregion

                        #region GlobalTimer Logic
                        //WorkOrderLabor savedWorkOrderLabor = null;
                        //WorkOrderLabor savedWorkOrderLabor2 = null;
                        //WorkOrderLabor savedWorkOrderLabor1 = null;
                        //try
                        //{

                        //    string k1 = "WorkOrderLabor:" + item.WorkOrderLaborID;
                        //    savedWorkOrderLabor = JsonConvert.DeserializeObject<WorkOrderLabor>(WorkOrderLaborStorge.Storage.Get(k1));
                        //}
                        //catch (Exception)
                        //{

                        //}

                        //try
                        //{

                        //    string k2 = "WorkOrderLaborHours2:" + item.WorkOrderLaborID;
                        //    savedWorkOrderLabor2 = JsonConvert.DeserializeObject<WorkOrderLabor>(WorkOrderLaborStorge.Storage.Get(k2));
                        //}
                        //catch (Exception)
                        //{

                        //}

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


                        //        startButton.TextColor = Color.Green;
                        //        startButton.ImageSource = "starticon1.png";
                        //        //startButtonforRate2.BackgroundColor = Color.Green;

                        //        string FinalHours = Convert.ToDecimal(string.Format("{0:F2}", savedWorkOrderLabor1.HoursAtRate1)).ToString();
                        //        var FinalHrs1 = FinalHours.Split('.');
                        //        hoursEntry.Text = FinalHrs1[0];
                        //        minuteEntry.Text = FinalHrs1[1];

                        //        //string FinalHours2 = Convert.ToDecimal(string.Format("{0:F2}", savedWorkOrderLabor.HoursAtRate2)).ToString();
                        //        //var FinalHrs2 = FinalHours2.Split('.');
                        //        //hoursEntryforRate2.Text = FinalHrs2[0];
                        //        //minuteEntryforRate2.Text = FinalHrs2[1];
                        //        if (item.CompletionDate != null)
                        //        {
                        //            completeDateButton.Text = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString();
                        //        }
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


                        //        startButtonforRate2.TextColor = Color.Green;
                        //        startButtonforRate2.ImageSource = "starticon1.png";

                        //        string FinalHours2 = Convert.ToDecimal(string.Format("{0:F2}", item.HoursAtRate2)).ToString();
                        //        var FinalHrs2 = FinalHours2.Split('.');
                        //        hoursEntryforRate2.Text = FinalHrs2[0];
                        //        minuteEntryforRate2.Text = FinalHrs2[1];

                        //        completeDateButton.Text = item.CompletionDate != null ? DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(item.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString() : "";

                        //    }
                        //    catch (Exception ex)
                        //    {

                        //    }

                        //}
                        #endregion

                        #region ***** button Click Event *****
                        /// Start button Click 
                        startButton.Clicked += async (sender, e) =>
                    {
                        UserDialogs.Instance.ShowLoading("", MaskType.Gradient);
                        var buttonStart = sender as SfButton;
                        WorkOrderLabor workOrderLabor = buttonStart.CommandParameter as WorkOrderLabor;
                        var workOrderWrapper = new workOrderWrapper
                        {
                            TimeZone = AppSettings.UserTimeZone,
                            CultureName = AppSettings.UserCultureName,
                            UserId = Convert.ToInt32(UserID),
                            ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                            workOrderLabor = new WorkOrderLabor
                            {
                                StartDate = DateTime.Now.Add(DateTime.Now.TimeOfDay),
                                EndDate = null,
                                CompletionDate = null,
                                HoursAtRate1Start = DateTime.Now.Add(DateTime.Now.TimeOfDay),
                                ModifiedUserName = AppSettings.User.UserName,
                                WorkOrderLaborID = workOrderLabor.WorkOrderLaborID,
                                WorkOrderID = this.WorkorderID,
                                WorkOrderLaborHourID = null,
                                TaskID = workOrderLabor.TaskID,
                                TimerID = 1,

                            },

                        };
                        var response = await _taskAndLabourService.CreateWorkOrderLaborHours(workOrderWrapper);
                        if (response != null && bool.Parse(response.servicestatus))
                        {

                            //if (response.workOrderWrapper != null && response.workOrderWrapper.workOrderLabor != null)
                            //{
                            //    this.WorkOrderLaborHourID = response.workOrderWrapper.workOrderLabor.WorkOrderLaborHourID;
                            //    Application.Current.Properties["WorkOrderLaborHourID"] = WorkOrderLaborHourID;
                            //}
                            var data = buttonStart.Parent.Parent.Parent as Grid;
                            var Min1 = data.Children[4] as StackLayout;
                            var Min2 = Min1.Children[0] as Grid;
                            var Min3 = Min2.Children[0] as SfBorder;
                            var Min4 = Min3.Content as Entry;
                            Min4.IsReadOnly = true;
                            var Hras1 = data.Children[3] as StackLayout;
                            var Hras2 = Hras1.Children[0] as Grid;
                            var Hras3 = Hras2.Children[0] as SfBorder;
                            var Hras4 = Hras3.Content as Entry;
                            Hras4.IsReadOnly = true;

                            startButton.TextColor = Color.Green;
                            startButton.ImageSource = "starticon1.png";

                            stopButton.TextColor = Color.FromHex("#87CEFA");
                            stopButton.ImageSource = "stoppending.png";

                            completeButton.TextColor = Color.FromHex("#879afa");

                            startButton.IsEnabled = false;
                            stopButton.IsEnabled = true;
                            completeButton.IsEnabled = true;

                            await OnViewAppearingAsync(null);
                            UserDialogs.Instance.HideLoading();
                            DialogService.ShowToast("Timer Successfully Started");
                        }
                        //    try
                        //{
                        //    //save its workOrderLabor in local storage so we can start timer when we come on this page then we can retrive it.
                        //    var buttonStart = sender as SfButton;
                        //    WorkOrderLabor workOrderLabor = buttonStart.CommandParameter as WorkOrderLabor;

                        //    workOrderLabor.StartTimeOfTimer = DateTime.Now;
                        //    startButton.CommandParameter = workOrderLabor; //reassign to commandParameter.


                        //    var parent = buttonStart.Parent.Parent.Parent;
                        //    Grid parentGrid = parent as Grid;
                        //    //  parentGrid.StyleId = item.HoursAtRate1.ToString();
                        //    StackLayout btnStopLocal = parentGrid.Children[1] as StackLayout;//Find the stopbutton from parent
                        //    Grid btnStopLocalGrid = btnStopLocal.Children[0] as Grid;//Find the stopbutton from parent
                        //    SfButton btnStop = btnStopLocalGrid.Children[0] as SfButton;//Find the stopbutton from parent
                        //    btnStop.CommandParameter = workOrderLabor; //reassign to commandParameter to stopbutton


                        //    //Save in Local
                        //    string key = "WorkOrderLabor:" + workOrderLabor.WorkOrderLaborID;
                        //    workOrderLabor.Description = "";
                        //    WorkOrderLaborStorge.Storage.Set(key, JsonConvert.SerializeObject(workOrderLabor));

                        //    string keyhours1 = "WorkOrderLaborHours1:" + workOrderLabor.HoursAtRate1;
                        //    workOrderLabor.Description = "";
                        //    WorkOrderLaborStorge.Storage.Set(keyhours1, JsonConvert.SerializeObject(workOrderLabor));

                        //    //StartTime = DateTime.Now;

                        //    startButton.TextColor = Color.Green;
                        //    startButton.ImageSource = "starticon1.png";
                        //    stopButton.IsEnabled = true;
                        //    stopButton.TextColor = Color.FromHex("#87CEFA");
                        //    stopButton.ImageSource = "stoppending.png";

                        //}
                        //catch (Exception ex)
                        //{

                        //    throw;
                        //}

                    };

                        startButtonforRate2.Clicked += async (sender, e) =>
                        {
                            var buttonStartforRate2 = sender as SfButton;
                            WorkOrderLabor workOrderLabor = buttonStartforRate2.CommandParameter as WorkOrderLabor;
                            var workOrderWrapper = new workOrderWrapper
                            {
                                TimeZone = AppSettings.UserTimeZone,
                                CultureName = AppSettings.UserCultureName,
                                UserId = Convert.ToInt32(UserID),
                                ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                                workOrderLabor = new WorkOrderLabor
                                {
                                    StartDate = DateTime.Now.Add(DateTime.Now.TimeOfDay),
                                    EndDate = null,
                                    CompletionDate = null,
                                    IsMannual = false,
                                    HoursAtRate2Start = DateTime.Now.Add(DateTime.Now.TimeOfDay),
                                    ModifiedUserName = AppSettings.User.UserName,
                                    WorkOrderLaborID = workOrderLabor.WorkOrderLaborID,
                                    WorkOrderID = this.WorkorderID,
                                    WorkOrderLaborHourID = null,
                                    TaskID = workOrderLabor.TaskID,
                                    TimerID = 4,
                                },

                            };
                            var response = await _taskAndLabourService.CreateWorkOrderLaborHours(workOrderWrapper);
                            if (response != null && bool.Parse(response.servicestatus))
                            {

                                //if (response.workOrderWrapper != null && response.workOrderWrapper.workOrderLabor != null)
                                //{
                                //    this.WorkOrderLaborHour2ID = response.workOrderWrapper.workOrderLabor.WorkOrderLaborHourID;
                                //    Application.Current.Properties["WorkOrderLaborHour2ID"] = WorkOrderLaborHour2ID;
                                //}
                                var data = buttonStartforRate2.Parent.Parent.Parent as Grid;
                                var Min1 = data.Children[4] as StackLayout;
                                var Min2 = Min1.Children[0] as Grid;
                                var Min3 = Min2.Children[0] as SfBorder;
                                var Min4 = Min3.Content as Entry;
                                Min4.IsReadOnly = true;
                                var Hras1 = data.Children[3] as StackLayout;
                                var Hras2 = Hras1.Children[0] as Grid;
                                var Hras3 = Hras2.Children[0] as SfBorder;
                                var Hras4 = Hras3.Content as Entry;
                                Hras4.IsReadOnly = true;

                                startButtonforRate2.TextColor = Color.Green;
                                startButtonforRate2.ImageSource = "starticon1.png";
                                startButtonforRate2.IsEnabled = false;
                                stopButtonforRate2.IsEnabled = true;
                                stopButtonforRate2.TextColor = Color.FromHex("#87CEFA");
                                stopButtonforRate2.ImageSource = "stoppending.png";

                                completeButtonforRate2.TextColor = Color.FromHex("#879afa");

                                UserDialogs.Instance.HideLoading();
                                await OnViewAppearingAsync(null);
                                DialogService.ShowToast("Timer Successfully Started");
                            }
                            //try
                            //{
                            //    //save its workOrderLabor in local storage so we can start timer when we come on this page then we can retrive it.

                            //    var buttonStartforRate2 = sender as SfButton;
                            //    WorkOrderLabor workOrderLabor = buttonStartforRate2.CommandParameter as WorkOrderLabor;

                            //    workOrderLabor.StartTimeOfTimer = DateTime.Now;
                            //    startButtonforRate2.CommandParameter = workOrderLabor; //reassign to commandParameter.


                            //    var parent = buttonStartforRate2.Parent.Parent.Parent;
                            //    Grid parentGrid = parent as Grid;
                            //    //  parentGrid.StyleId = item.HoursAtRate1.ToString();
                            //    StackLayout btnStopLocal = parentGrid.Children[1] as StackLayout;//Find the stopbutton from parent
                            //    Grid btnStopLocalGrid = btnStopLocal.Children[0] as Grid;//Find the stopbutton from parent
                            //    SfButton btnStop = btnStopLocalGrid.Children[0] as SfButton;//Find the stopbutton from parent
                            //    btnStop.CommandParameter = workOrderLabor; //reassign to commandParameter to stopbutton

                            //    //var parent = buttonStartforRate2.Parent;
                            //    //Grid parentGrid = parent as Grid;
                            //    //// parentGrid.StyleId = item.HoursAtRate2.ToString();
                            //    //Button btnStopLocal = parentGrid.Children[1] as Button;//Find the stopbutton from parent
                            //    //btnStopLocal.CommandParameter = workOrderLabor; //reassign to commandParameter to stopbutton



                            //    //Save in Local
                            //    string key = "WorkOrderLaborHours2:" + workOrderLabor.WorkOrderLaborID;
                            //    workOrderLabor.Description = "";
                            //    WorkOrderLaborStorge.Storage.Set(key, JsonConvert.SerializeObject(workOrderLabor));


                            //    //StartTime = DateTime.Now;

                            //    startButtonforRate2.TextColor = Color.Green;
                            //    startButtonforRate2.ImageSource = "starticon1.png";
                            //    stopButtonforRate2.IsEnabled = true;
                            //    stopButtonforRate2.TextColor = Color.FromHex("#87CEFA");
                            //    stopButtonforRate2.ImageSource = "stoppending.png";
                            //}
                            //catch (Exception)
                            //{

                            //    throw;
                            //}


                        };

                        /// Stop button Click 
                        stopButton.Clicked += async (sender, e) =>
                           {
                               UserDialogs.Instance.ShowLoading("", MaskType.Gradient);
                               var buttonStop = sender as SfButton;
                               WorkOrderLabor workOrderLabor = buttonStop.CommandParameter as WorkOrderLabor;
                               var workOrderWrapper = new workOrderWrapper
                               {
                                   TimeZone = AppSettings.UserTimeZone,
                                   CultureName = AppSettings.UserCultureName,
                                   UserId = Convert.ToInt32(UserID),
                                   ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                                   workOrderLabor = new WorkOrderLabor
                                   {
                                       StartDate = null,
                                       EndDate = DateTime.Now.Add(DateTime.Now.TimeOfDay),
                                       CompletionDate = null,
                                       HoursAtRate1Start = null,
                                       IsMannual = false,
                                       HoursAtRate1Stop = DateTime.Now.Add(DateTime.Now.TimeOfDay),
                                       ModifiedUserName = AppSettings.User.UserName,
                                       WorkOrderLaborID = workOrderLabor.WorkOrderLaborID,
                                       WorkOrderID = this.WorkorderID,
                                       WorkOrderLaborHourID = item.WorkOrderLaborHourID1,
                                       TaskID = workOrderLabor.TaskID,
                                       TimerID = 2,
                                   },

                               };
                               var response = await _taskAndLabourService.CreateWorkOrderLaborHours(workOrderWrapper);
                               if (response != null && bool.Parse(response.servicestatus))
                               {
                                   UserDialogs.Instance.HideLoading();
                                   await OnViewAppearingAsync(null);
                                   DialogService.ShowToast("Timer Successfully Stopped");
                               }
                               //try
                               //{

                               //    var StopTime = DateTime.Now;

                               //    var x1 = sender as SfButton;
                               //    WorkOrderLabor workOrderLabor = x1.CommandParameter as WorkOrderLabor;

                               //    if (workOrderLabor.StartTimeOfTimer == DateTime.Parse("1/1/0001 12:00:00 AM"))
                               //    {
                               //        return;
                               //    }

                               //    TimeSpan elapsed = StopTime.Subtract(workOrderLabor.StartTimeOfTimer);

                               //    int mn = elapsed.Minutes;
                               //    if (String.IsNullOrWhiteSpace(minuteEntry.Text))
                               //    {
                               //        minuteEntry.Text = "0";
                               //    }
                               //    if (String.IsNullOrWhiteSpace(hoursEntry.Text))
                               //    {
                               //        hoursEntry.Text = "0";
                               //    }
                               //    int mn1 = Convert.ToInt32(minuteEntry.Text);
                               //    if (mn + mn1 > 59)
                               //    {
                               //        TimeSpan span = TimeSpan.FromMinutes(mn + mn1);
                               //        string elapsedTime1 = String.Format("{0:00}:{1:00}",
                               //                                      span.Hours, span.Minutes);
                               //        int hrs = span.Hours;
                               //        int hrs1 = Convert.ToInt32(hoursEntry.Text);
                               //        hoursEntry.Text = (hrs + hrs1).ToString();

                               //        int hrs2 = span.Minutes;
                               //        minuteEntry.Text = hrs2.ToString();
                               //    }
                               //    else
                               //    {

                               //        int hrs = elapsed.Hours;
                               //        int hrs1 = Convert.ToInt32(hoursEntry.Text);
                               //        hoursEntry.Text = (hrs + hrs1).ToString();

                               //        int hrs2 = elapsed.Minutes;
                               //        int hrs21 = Convert.ToInt32(minuteEntry.Text);
                               //        minuteEntry.Text = (hrs2 + hrs21).ToString();
                               //    }


                               //    completeDateButton.Text = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).ToString();

                               //    stopButton.TextColor = Color.Red;
                               //    stopButton.ImageSource = "startred.png";
                               //    startButton.TextColor = Color.Red;
                               //    startButton.ImageSource = "stopicon1.png";
                               //    var parent = stopButton.Parent.Parent.Parent.Parent.Parent;
                               //    Grid parentGrid = parent as Grid;
                               //    StackLayout Hrs1StackLayout = parentGrid.Children[0] as StackLayout;
                               //    Hrs1StackLayout.StyleId = item.HoursAtRate1.ToString();

                               //    ////////Save HoursAtRate1 to database//////
                               //    ///

                               //    var workorderLabourHour1 = (WorkOrderLabor)((SfButton)sender).CommandParameter;
                               //    var taskID = workorderLabourHour1.TaskID;
                               //    var workOrderLaborID = workorderLabourHour1.WorkOrderLaborID;


                               //    if (String.IsNullOrEmpty(hoursEntry.Text))
                               //    {
                               //        hoursEntry.Text = "0";
                               //    }
                               //    if (String.IsNullOrEmpty(minuteEntry.Text))
                               //    {
                               //        minuteEntry.Text = "0";
                               //    }


                               //    if (minuteEntry.Text.Length == 1)
                               //    {
                               //        int FormattedHours = Convert.ToInt32(minuteEntry.Text);
                               //        minuteEntry.Text = string.Format("{0:00}", FormattedHours);

                               //    }



                               //    decimal FinalHour1 = decimal.Parse(hoursEntry.Text + "." + minuteEntry.Text);



                               //    var workOrderWrapper = new workOrderWrapper
                               //    {
                               //        TimeZone = AppSettings.UserTimeZone,
                               //        CultureName = AppSettings.UserCultureName,
                               //        UserId = Convert.ToInt32(UserID),
                               //        ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                               //        workOrderLabor = new WorkOrderLabor
                               //        {
                               //            ModifiedUserName = AppSettings.User.UserName,
                               //            CompletionDate =DateTime.Parse(completeDateButton.Text),
                               //            HoursAtRate1 = FinalHour1,
                               //            TaskID = taskID,
                               //            StartDate = fromEntry.SelectedDate.Value.Add(DateTime.Now.TimeOfDay),

                               //            WorkOrderLaborID = workOrderLaborID
                               //        },

                               //    };




                               //    var response = await _taskAndLabourService.UpdateTaskAndLabour(workOrderWrapper);
                               //    if (response != null && bool.Parse(response.servicestatus))
                               //    {
                               //        DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("TaskLabourSuccessfullyUpdated"), 2000);
                               //    }
                               //    }
                               //catch (Exception ex)
                               //{
                               //    throw;
                               //}
                           };

                        stopButtonforRate2.Clicked += async (sender, e) =>
                        {
                            var buttonStop2 = sender as SfButton;
                            WorkOrderLabor workOrderLabor = buttonStop2.CommandParameter as WorkOrderLabor;
                            var workOrderWrapper = new workOrderWrapper
                            {
                                TimeZone = AppSettings.UserTimeZone,
                                CultureName = AppSettings.UserCultureName,
                                UserId = Convert.ToInt32(UserID),
                                ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                                workOrderLabor = new WorkOrderLabor
                                {
                                    StartDate = null,
                                    EndDate = DateTime.Now.Add(DateTime.Now.TimeOfDay),
                                    CompletionDate = null,
                                    HoursAtRate1Start = null,
                                    IsMannual = false,
                                    HoursAtRate2Stop = DateTime.Now.Add(DateTime.Now.TimeOfDay),
                                    ModifiedUserName = AppSettings.User.UserName,
                                    WorkOrderLaborID = workOrderLabor.WorkOrderLaborID,
                                    WorkOrderID = this.WorkorderID,
                                    WorkOrderLaborHourID = item.WorkOrderLaborHourID2,
                                    TaskID = workOrderLabor.TaskID,
                                    TimerID = 5,
                                },

                            };
                            var response = await _taskAndLabourService.CreateWorkOrderLaborHours(workOrderWrapper);
                            if (response != null && bool.Parse(response.servicestatus))
                            {
                                await this.OnViewAppearingAsync(null);
                                DialogService.ShowToast("Timer Successfully Stopped");
                            }
                            //try
                            //{
                            //    var StopTime = DateTime.Now;

                            //    var x1 = sender as SfButton;
                            //    WorkOrderLabor workOrderLabor = x1.CommandParameter as WorkOrderLabor;

                            //    if (workOrderLabor.StartTimeOfTimer == DateTime.Parse("1/1/0001 12:00:00 AM"))
                            //    {
                            //        return;
                            //    }

                            //    TimeSpan elapsed = StopTime.Subtract(workOrderLabor.StartTimeOfTimer);

                            //    int mn = elapsed.Minutes;
                            //    if (String.IsNullOrWhiteSpace(minuteEntryforRate2.Text))
                            //    {
                            //        minuteEntryforRate2.Text = "0";
                            //    }
                            //    if (String.IsNullOrWhiteSpace(hoursEntryforRate2.Text))
                            //    {
                            //        hoursEntryforRate2.Text = "0";
                            //    }
                            //    int mn1 = Convert.ToInt32(minuteEntryforRate2.Text);
                            //    if (mn + mn1 > 59)
                            //    {


                            //        TimeSpan span = TimeSpan.FromMinutes(mn + mn1);
                            //        string elapsedTime1 = String.Format("{0:00}:{1:00}",
                            //                                      span.Hours, span.Minutes);
                            //        int hrs = span.Hours;
                            //        int hrs1 = Convert.ToInt32(hoursEntryforRate2.Text);
                            //        hoursEntryforRate2.Text = (hrs + hrs1).ToString();

                            //        int hrs2 = span.Minutes;
                            //        minuteEntryforRate2.Text = hrs2.ToString();
                            //    }
                            //    else
                            //    {

                            //        int hrs = elapsed.Hours;
                            //        int hrs1 = Convert.ToInt32(hoursEntryforRate2.Text);
                            //        hoursEntryforRate2.Text = (hrs + hrs1).ToString();

                            //        int hrs2 = elapsed.Minutes;
                            //        int hrs21 = Convert.ToInt32(minuteEntryforRate2.Text);
                            //        minuteEntryforRate2.Text = (hrs2 + hrs21).ToString();
                            //    }


                            //    completeDateButton.Text = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).ToString();

                            //    stopButtonforRate2.TextColor = Color.Red;
                            //    stopButtonforRate2.ImageSource = "startred.png";
                            //    startButtonforRate2.TextColor = Color.Red;
                            //    startButtonforRate2.ImageSource = "stopicon1.png";

                            //    var parent = stopButton.Parent.Parent.Parent.Parent.Parent;
                            //    Grid parentGrid = parent as Grid;
                            //    StackLayout Hrs2StackLayout = parentGrid.Children[1] as StackLayout;
                            //    Hrs2StackLayout.StyleId = item.HoursAtRate2.ToString();
                            //}
                            //catch (Exception)
                            //{

                            //    throw;
                            //}

                        };

                        /// complete Button Click 
                        completeButton.Clicked += async (sender, e) =>
                        {
                            completeDateButton.Text = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).ToString();
                            UserDialogs.Instance.ShowLoading("", MaskType.Gradient);
                            var buttonComplete = sender as SfButton;
                            WorkOrderLabor workOrderLabor = buttonComplete.CommandParameter as WorkOrderLabor;
                            var workOrderWrapper = new workOrderWrapper
                            {
                                TimeZone = AppSettings.UserTimeZone,
                                CultureName = AppSettings.UserCultureName,
                                UserId = Convert.ToInt32(UserID),
                                ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                                workOrderLabor = new WorkOrderLabor
                                {
                                    StartDate = null,
                                    EndDate = null,
                                    CompletionDate = DateTime.Now.Add(DateTime.Now.TimeOfDay),
                                    HoursAtRate1Start = null,
                                    HoursAtRate1Stop = null,
                                    IsMannual = false,
                                    ModifiedUserName = AppSettings.User.UserName,
                                    WorkOrderLaborID = workOrderLabor.WorkOrderLaborID,
                                    WorkOrderID = this.WorkorderID,
                                    WorkOrderLaborHourID = item.WorkOrderLaborHourID1,
                                    TaskID = workOrderLabor.TaskID,
                                    TimerID = 3,
                                },

                            };
                            var response = await _taskAndLabourService.CreateWorkOrderLaborHours(workOrderWrapper);
                            if (response != null && bool.Parse(response.servicestatus))
                            {
                                UserDialogs.Instance.HideLoading();
                                await this.OnViewAppearingAsync(null);
                                DialogService.ShowToast("Timer Successfully Completed");
                            }
                            //try
                            //{
                            //    stopButton.IsEnabled = false;
                            //    stopButton.TextColor = Color.FromHex("#708090");
                            //    stopButton.ImageSource = "stopcomplate.png";

                            //    completeButton.IsEnabled = false;
                            //    completeButton.TextColor = Color.FromHex("#708090");
                            //    completeButton.ImageSource = "complet1.png";

                            //    startButton.IsEnabled = false;
                            //    startButton.TextColor = Color.FromHex("#708090");
                            //    startButton.ImageSource = "startcomplte.png";

                            //    completeDateButton.Text = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).ToString();
                            //    var parent = stopButton.Parent.Parent.Parent.Parent.Parent;
                            //    Grid parentGrid = parent as Grid;
                            //    StackLayout Hrs1StackLayout = parentGrid.Children[0] as StackLayout;
                            //    Hrs1StackLayout.StyleId = item.HoursAtRate1.ToString();
                            //}
                            //catch (Exception ex)
                            //{

                            //    throw;
                            //}
                        };

                        completeButtonforRate2.Clicked += async (sender, e) =>
                        {
                            completeDateButton.Text = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).ToString();
                            var buttonComplete2 = sender as SfButton;
                            WorkOrderLabor workOrderLabor = buttonComplete2.CommandParameter as WorkOrderLabor;
                            var workOrderWrapper = new workOrderWrapper
                            {
                                TimeZone = AppSettings.UserTimeZone,
                                CultureName = AppSettings.UserCultureName,
                                UserId = Convert.ToInt32(UserID),
                                ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                                workOrderLabor = new WorkOrderLabor
                                {
                                    StartDate = null,
                                    EndDate = null,
                                    IsMannual = false,
                                    CompletionDate = DateTime.Now.Add(DateTime.Now.TimeOfDay),
                                    HoursAtRate1Start = null,
                                    HoursAtRate1Stop = null,
                                    ModifiedUserName = AppSettings.User.UserName,
                                    WorkOrderLaborID = workOrderLabor.WorkOrderLaborID,
                                    WorkOrderID = this.WorkorderID,
                                    WorkOrderLaborHourID = item.WorkOrderLaborHourID2,
                                    TaskID = workOrderLabor.TaskID,
                                    TimerID = 6,
                                },

                            };
                            var response = await _taskAndLabourService.CreateWorkOrderLaborHours(workOrderWrapper);
                            if (response != null && bool.Parse(response.servicestatus))
                            {
                                await this.OnViewAppearingAsync(null);
                                DialogService.ShowToast("Timer Successfully Completed");
                            }
                            //stopButtonforRate2.IsEnabled = false;
                            //stopButtonforRate2.TextColor = Color.FromHex("#708090");
                            //stopButtonforRate2.ImageSource = "stopcomplate.png";

                            //completeButtonforRate2.IsEnabled = false;
                            //completeButtonforRate2.TextColor = Color.FromHex("#708090");
                            //completeButtonforRate2.ImageSource = "complet1.png";

                            //startButtonforRate2.IsEnabled = false;
                            //startButtonforRate2.TextColor = Color.FromHex("#708090");
                            //startButtonforRate2.ImageSource = "startcomplte.png";

                            //completeDateButton.Text = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).ToString();
                            //var parent = stopButton.Parent.Parent.Parent.Parent.Parent;
                            //Grid parentGrid = parent as Grid;
                            //StackLayout Hrs2StackLayout = parentGrid.Children[1] as StackLayout;
                            //Hrs2StackLayout.StyleId = item.HoursAtRate2.ToString();
                        };

                        // save Button 
                        saveButton.Clicked += async (sender, e) =>
                        {
                            try
                            {
                                OperationInProgress = true;
                                await SaveTaskAndLabour(sender, e);
                            }
                            catch (Exception ex)
                            {
                                OperationInProgress = false;

                            }

                            finally
                            {
                                OperationInProgress = false;
                            }
                        };

                        completeDateButton.Clicked += CompletionDate_Clicked;
                        #endregion
                        contentLayout.Children.Add(MasterstackLayout);
                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private ImageSource getSource()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return ImageSource.FromFile("Remove.png");
                case Device.Android:
                    return ImageSource.FromFile("Remove.png");
                case Device.UWP:
                    return ImageSource.FromFile("Assets/Remove.png");
                default:
                    return ImageSource.FromFile("Remove.png");
            }
        }

        private async void imageClicked(object sender, EventArgs args)
        {
            var ImageCicked = sender as Image;
            var parent = ImageCicked.Parent;
            var parentLayout = parent.Parent as StackLayout;
            var datesLayout = parentLayout.Children[1] as StackLayout;

            //var startDateLayout = datesLayout.Children[0] as StackLayout;
            //var startDatePicker = startDateLayout.Children[1] as RequiredDateCustomDatePicker;
            //startDate = DateTime.Parse(startDatePicker.SelectedDate.ToString());

            // .. var completeDateLayout = datesLayout.Children[1] as StackLayout;
            var completeDateButton = datesLayout.Children[0] as Button;
            // var completeDateButton = parentLayout.Children[0] as Button;
            completeDateButton.Text = "";
        }

        private async void CompletionDate_Clicked(object sender, EventArgs e)
        {
            UserDialogs.Instance.DatePrompt(new DatePromptConfig { OnAction = (result) => SetCompletionDateResult(result, sender, e), IsCancellable = true, MaximumDate = DateTime.Now });
        }

        private void SetCompletionDateResult(DatePromptResult result, object sender, EventArgs e)
        {
            if (result.Ok)
            {
                var s = sender as Button;

                if (result.Value.Date == DateTime.Parse("1/1/0001 12:00:00 AM"))
                {
                    s.Text = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).Date.ToString();
                }

                else
                {
                    s.Text = result.SelectedDate.ToString();
                }

                if (result.Value.Date.Date > DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).Date)
                {
                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("TaskCompletionDatecannotbefuturedate"));
                    s.Text = DateTimeConverter.ClientCurrentDateTimeByZone(AppSettings.User.TimeZone).Date.ToString();
                }


            }
            else { }
            //else
            //{
            //    var s = sender as Button;
            //    s.Text = "";
            //}

        }

        public async Task SaveTaskAndLabour(object sender, EventArgs e)
        {
            try
            {
                ///TODO: Get Workorder data 
                var workorderWrapper = await _workorderService.GetWorkorderByWorkorderID(UserID, WorkorderID.ToString());

                var workLabourHour = await _taskAndLabourService.WorkOrderLaborsByWorkOrderID(UserID, WorkorderID.ToString());

                if (workorderWrapper != null && workorderWrapper.workOrderWrapper != null && workorderWrapper.workOrderWrapper.workOrder != null)
                {
                    DateTime startDate = new DateTime();
                    string completeDate = "";
                    string hours = "";
                    string minutes = "";

                    string hoursforRate1 = "";
                    string minutesforRate1 = "";

                    DateTime? dateComp = null;
                    DateTime? FinaldateComp = null;
                    //TODO: find out the dates of pickers date

                    try
                    {
                        var buttonSave = sender as SfButton;
                        var parent = buttonSave.Parent.Parent;

                        // var parent = buttonStartforRate2.Parent.Parent.Parent;
                        Grid parentGrid = parent.Parent as Grid;
                        StackLayout startDateLayout = parentGrid.Children[0] as StackLayout;//Find the stopbutton from parent
                        var sfBorder = startDateLayout.Children[1] as SfBorder;
                        var FromDates = sfBorder.Children[0] as RequiredDateCustomDatePicker;
                        startDate = DateTime.Parse(FromDates.SelectedDate.ToString());

                        StackLayout completeDateLayout = parentGrid.Children[1] as StackLayout;//Find the stopbutton from parent
                        var completeDateButton = completeDateLayout.Children[1] as Button;
                        completeDate = completeDateButton.Text;

                        //4,6
                        var parentLayout = parent.Parent.Parent as StackLayout;

                        var GridHrs1 = parentLayout.Children[0] as Grid;
                        var stackallocHrs1 = GridHrs1.Children[0] as StackLayout;
                        var GridHrs2 = stackallocHrs1.Children[1] as Grid;
                        var stackallocHrs2 = GridHrs2.Children[3] as StackLayout;
                        var GridHrs3 = stackallocHrs2.Children[0] as Grid;
                        var sfBorderHrs = GridHrs3.Children[0] as SfBorder;
                        Entry hoursEntry = sfBorderHrs.Children[0] as CustomEntry; // Hours

                        var stackallocMin1 = GridHrs2.Children[4] as StackLayout;
                        var GridMin3 = stackallocMin1.Children[0] as Grid;
                        var sfBorderMin = GridMin3.Children[0] as SfBorder;
                        Entry minutesEntry = sfBorderMin.Children[0] as CustomEntry; // Minutes

                        #region **** Old Hrs And Min ****

                        //var stackallocHrs10 = GridHrs.Children[3] as StackLayout;
                        //var GridHrs1 = stackallocHrs1.Children[0] as Grid;
                        //var sfBorderHrs = GridHrs1.Children[0] as SfBorder;
                        //Entry hoursEntry = sfBorderHrs.Children[0] as CustomEntry; // Hours


                        //var stackallocMin = parentLayout.Children[3] as StackLayout;
                        //var GridMin = GridHrs.Children[0] as Grid;
                        //var sfBorderMin = GridMin.Children[0] as SfBorder;
                        //Entry minutesEntry = sfBorderMin.Children[0] as CustomEntry; // Minutes
                        //var startAndStopButtonGrid = parentLayout.Children[4] as Grid;
                        //var hrf = startAndStopButtonGrid.Children[0] as CustomEntry;
                        //Entry hoursEntry = startAndStopButtonGrid.Children[4] as CustomEntry; // Hours
                        //Entry minutesEntry = startAndStopButtonGrid.Children[6] as CustomEntry; // Minutes 
                        #endregion


                        if (minutesEntry.IsReadOnly)
                        {
                            hours = "0";
                            minutes = "00";
                        }
                        else
                        {
                            hours = hoursEntry.Text;
                            minutes = minutesEntry.Text;
                        }



                        //5,7

                        var GridHrs21 = GridHrs1.Children[1] as StackLayout;
                        var GridHrs22 = GridHrs21.Children[1] as Grid;
                        var stackallocHrs22 = GridHrs22.Children[3] as StackLayout;
                        var GridHrs23 = stackallocHrs22.Children[0] as Grid;
                        var sfBorderHrs2 = GridHrs23.Children[0] as SfBorder;
                        Entry hoursEntryforRate1 = sfBorderHrs2.Children[0] as CustomEntry; // Hours

                        var stackallocMin21 = GridHrs22.Children[4] as StackLayout;
                        var GridMin23 = stackallocMin21.Children[0] as Grid;
                        var sfBorderMin2 = GridMin23.Children[0] as SfBorder;
                        Entry minutesEntryforRate1 = sfBorderMin2.Children[0] as CustomEntry; // Minutes

                        //var startAndStopButtonGridforRate1 = parentLayout.Children[5] as Grid;
                        //Entry hoursEntryforRate1 = startAndStopButtonGridforRate1.Children[4] as CustomEntry; // Hours
                        //Entry minutesEntryforRate1 = startAndStopButtonGridforRate1.Children[6] as CustomEntry; // Minutes

                        if (minutesEntryforRate1.IsReadOnly)
                        {
                            hoursforRate1 = "0";
                            minutesforRate1 = "00";
                        }
                        else
                        {
                            hoursforRate1 = hoursEntryforRate1.Text;
                            minutesforRate1 = minutesEntryforRate1.Text;
                        }
                        //hoursforRate1 = hoursEntryforRate1.Text;
                        //minutesforRate1 = minutesEntryforRate1.Text;

                        var FindMainGrid1 = buttonSave.Parent.Parent.Parent.Parent;
                    }
                    catch (Exception ex)
                    {


                    }

                    try
                    {

                        if (!Convert.ToBoolean(workorderWrapper.workOrderWrapper.IsCheckedAutoFillCompleteOnTaskAndLabor))
                        {
                            if (!string.IsNullOrWhiteSpace(completeDate))
                            {
                                if (workorderWrapper.workOrderWrapper.workOrder.CompletionDate != null && Convert.ToDateTime(completeDate).Date > DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderWrapper.workOrderWrapper.workOrder.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone))
                                {
                                    //DialogService.ShowToast(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "CompletionDatecannotbegreaterthanWorkorderCompletionDate").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                                    DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("CompletionDatecannotbegreaterthanWorkorderCompletionDate"));
                                    return;
                                }
                            }
                        }


                        string workordercompDate = string.Empty;
                        string workorderstartDate = string.Empty;
                        if (workorderWrapper.workOrderWrapper.workOrder.CompletionDate != null)
                        {
                            workordercompDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderWrapper.workOrderWrapper.workOrder.CompletionDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString();
                        }
                        if (workorderWrapper.workOrderWrapper.workOrder.CompletionDate.HasValue && startDate.Date > DateTime.Parse(workordercompDate))
                        {
                            //DialogService.ShowToast(validationResult.ErrorMessage);
                            DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("TaskStartdatecannotbegreaterthanWorkOrderCompletionDate"));
                            return;
                        }
                        if (workorderWrapper.workOrderWrapper.workOrder.WorkStartedDate != null)
                        {
                            workorderstartDate = DateTimeConverter.ConvertDateTimeToDifferentTimeZone(Convert.ToDateTime(workorderWrapper.workOrderWrapper.workOrder.WorkStartedDate).ToUniversalTime(), AppSettings.User.ServerIANATimeZone).ToString("d");
                        }
                        if (workorderWrapper.workOrderWrapper.workOrder.WorkStartedDate.HasValue && startDate.Date < DateTime.Parse(workorderstartDate))
                        {
                            //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "TaskStartdatecannotbelessthanWorkOrderStartDate").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                            DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("TaskStartdatecannotbelessthanWorkOrderStartDate"));
                            return;
                        }
                        if (!string.IsNullOrWhiteSpace(completeDate))
                        {
                            dateComp = DateTime.Parse(completeDate);
                            FinaldateComp = dateComp.GetValueOrDefault().Date.Add(DateTime.Now.TimeOfDay);

                            if (startDate.Date > DateTime.Parse(completeDate).Date)
                            {
                                //await DisplayAlert(formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "Alert").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "TaskCompletionDatedatecannotbelessthanTaskStartDate").TargetName, formLoadInputs.Result.listWebControlTitles.FirstOrDefault(i => i.TitleName == "OK").TargetName);
                                DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("TaskCompletionDatedatecannotbelessthanTaskStartDate"));
                                return;
                            }
                        }


                        var workorderLabour = (WorkOrderLabor)((SfButton)sender).CommandParameter;
                        var taskID = workorderLabour.TaskID;
                        var workOrderLaborID = workorderLabour.WorkOrderLaborID;
                        var workOrderLaborHours2 = workorderLabour.HoursAtRate2;

                        if (String.IsNullOrEmpty(hours))
                        {
                            hours = "0";
                        }
                        if (String.IsNullOrEmpty(minutes))
                        {
                            minutes = "0";
                        }

                        if (String.IsNullOrEmpty(hoursforRate1))
                        {
                            hoursforRate1 = "0";
                        }
                        if (String.IsNullOrEmpty(minutesforRate1))
                        {
                            minutesforRate1 = "0";
                        }
                        if (minutes.Length == 1)
                        {
                            int FormattedHours = Convert.ToInt32(minutes);
                            minutes = string.Format("{0:00}", FormattedHours);

                        }

                        if (minutesforRate1.Length == 1)
                        {
                            int FormattedHours = Convert.ToInt32(minutesforRate1);
                            minutesforRate1 = string.Format("{0:00}", FormattedHours);

                        }

                        decimal decHour1 = decimal.Parse(hours + "." + minutes);

                        decimal decHour2 = decimal.Parse(hoursforRate1 + "." + minutesforRate1);


                        #region Check Employee Work Hour Flag
                        if (workLabourHour.workOrderWrapper.EmployeeWorkHourFlag)
                        {
                            if (decHour1 > workLabourHour.workOrderWrapper.EmployeeWorkHourValue)
                            {

                                var result = await DialogService.ShowConfirmAsync("Are you sure you want to add " + decHour1 + " hours to this workorder?", WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("Yes"), WebControlTitle.GetTargetNameByTitleName("No"));
                                if (result == true)
                                {

                                }
                                else
                                {
                                    return;
                                }
                            }
                            if (decHour2 > workLabourHour.workOrderWrapper.EmployeeWorkHourValue)
                            {
                                var result = await DialogService.ShowConfirmAsync("Are you sure you want to add " + decHour2 + " hours to this workorder?", WebControlTitle.GetTargetNameByTitleName("Alert"), WebControlTitle.GetTargetNameByTitleName("Yes"), WebControlTitle.GetTargetNameByTitleName("No"));
                                if (result == true)
                                {

                                }
                                else
                                {
                                    return;
                                }

                            }

                        }


                        #endregion


                        var workOrderWrapper = new workOrderWrapper
                        {
                            TimeZone = AppSettings.UserTimeZone,
                            CultureName = AppSettings.UserCultureName,
                            UserId = Convert.ToInt32(UserID),
                            ClientIANATimeZone = AppSettings.ClientIANATimeZone,
                            workOrderLabor = new WorkOrderLabor
                            {
                                IsMannual=true,
                                ModifiedUserName = AppSettings.User.UserName,
                                CompletionDate = FinaldateComp,
                                HoursAtRate1 = decHour1,
                                HoursAtRate2 = decHour2,
                                TaskID = taskID,
                                StartDate = startDate.Date.Add(DateTime.Now.TimeOfDay),
                                WorkOrderLaborID = workOrderLaborID
                            },

                        };




                        var response = await _taskAndLabourService.UpdateTaskAndLabour(workOrderWrapper);
                        if (response != null && bool.Parse(response.servicestatus))
                        {

                            DialogService.ShowToast(WebControlTitle.GetTargetNameByTitleName("TaskLabourSuccessfullyUpdated"), 2000);
                            try
                            {
                                #region **** Find Button ***
                                var buttonSave = sender as SfButton;
                                var FindMainGrid = buttonSave.Parent.Parent.Parent.Parent;
                                var satackLayout1 = FindMainGrid as StackLayout;
                                var parentGrids = satackLayout1.Children[0] as Grid;
                                var Hrs1StackLayout = parentGrids.Children[0] as StackLayout;
                                var Hrs2StackLayout = parentGrids.Children[1] as StackLayout;
                                var parentLayout = buttonSave.Parent.Parent.Parent.Parent as StackLayout;
                                #region **** Find Button 1 *****

                                var parent = parentLayout.Children[0] as Grid;
                                var buttons1 = parent.Children[0] as StackLayout;
                                var button2 = buttons1.Children[1] as Grid;

                                var stratbutton = button2.Children[0] as StackLayout;
                                var stratbutton1 = stratbutton.Children[0] as Grid;
                                var stratsfBodder = stratbutton1.Children[0] as SfBorder;
                                SfButton btnStartLocal = stratsfBodder as SfButton; //Find the stopbutton from parent

                                var stopbutton = button2.Children[1] as StackLayout;
                                var stopbutton1 = stopbutton.Children[0] as Grid;
                                var stopBtnsfBodder = stopbutton1.Children[0] as SfBorder;
                                SfButton btnStopLocal = stopBtnsfBodder as SfButton; //Find the stopbutton from parent

                                var Completebutton = button2.Children[2] as StackLayout;
                                var Completebutton1 = Completebutton.Children[0] as Grid;
                                var CompletesfBodder = Completebutton1.Children[0] as SfBorder;
                                SfButton btnCompleteLocal = CompletesfBodder as SfButton;  //Find the stopbutton from parent
                                #endregion

                                #region **** Find Button 2 *****

                                var buttons21 = parent.Children[1] as StackLayout;
                                var button22 = buttons21.Children[1] as Grid;

                                var stratbutton2 = button22.Children[0] as StackLayout;
                                var stratbutton21 = stratbutton2.Children[0] as Grid;
                                var stratsfBodder2 = stratbutton21.Children[0] as SfBorder;
                                SfButton btnStartLocal2 = stratsfBodder as SfButton; //Find the stopbutton from parent

                                var stopbutton2 = button22.Children[1] as StackLayout;
                                var stopbutton21 = stopbutton2.Children[0] as Grid;
                                var stopBtnsfBodder2 = stopbutton21.Children[0] as SfBorder;
                                SfButton btnStopLocal2 = stopBtnsfBodder as SfButton; //Find the stopbutton from parent

                                var Completebutton2 = button2.Children[2] as StackLayout;
                                var Completebutton21 = Completebutton2.Children[0] as Grid;
                                var CompletesfBodder2 = Completebutton21.Children[0] as SfBorder;
                                SfButton btnCompleteLocal2 = CompletesfBodder as SfButton; //Find the stopbutton from parent
                                #endregion
                                #endregion


                                #region ***** Old Code *****
                                //Button btnStartLocal = parentGrid.Children[0] as Button;//Find the stopbutton from parent
                                //Button btnStopLocal = parentGrid.Children[1] as Button;//Find the stopbutton from parent
                                //Button btnCompleteLocal = parentGrid.Children[2] as Button;//Find the stopbutton from parent

                                //var parentGrid2 = parentLayout.Children[5] as Grid;
                                //Button btnStartLocal2 = parentGrid2.Children[0] as Button;//Find the stopbutton from parent
                                //Button btnStopLocal2 = parentGrid2.Children[1] as Button;//Find the stopbutton from parent
                                //Button btnCompleteLocal2 = parentGrid2.Children[2] as Button;//Find the stopbutton from parent 
                                #endregion

                                var upadatedLabor = buttonSave.CommandParameter as WorkOrderLabor;  //reassign to commandParameter to stopbutton
                                upadatedLabor.HoursAtRate1 = decHour1;
                                upadatedLabor.HoursAtRate2 = decHour2;

                                btnStartLocal.CommandParameter = upadatedLabor.HoursAtRate1;
                                btnStopLocal.CommandParameter = upadatedLabor.HoursAtRate1;

                                btnStartLocal2.CommandParameter = upadatedLabor.HoursAtRate2;
                                btnStopLocal2.CommandParameter = upadatedLabor.HoursAtRate2;


                                if (Hrs1StackLayout.StyleId == null)
                                {
                                    //btnStartLocal.IsEnabled = true;
                                    //btnStartLocal.BackgroundColor = Color.FromHex("#87CEFA");

                                    //btnCompleteLocal.IsEnabled = true;
                                    //btnCompleteLocal.BackgroundColor = Color.FromHex("#87CEFA");

                                }
                                else
                                {
                                    string key = "WorkOrderLabor:" + workorderLabour.WorkOrderLaborID;
                                    string keyhours1 = "WorkOrderLabor:" + workorderLabour.HoursAtRate1;
                                    WorkOrderLaborStorge.Storage.Delete(key);
                                    WorkOrderLaborStorge.Storage.Delete(keyhours1);
                                }
                                if (Hrs2StackLayout.StyleId == null)
                                {
                                    //btnStartLocal2.IsEnabled = true;
                                    //btnStartLocal2.BackgroundColor = Color.FromHex("#87CEFA");

                                    //btnCompleteLocal2.IsEnabled = true;
                                    //btnCompleteLocal2.BackgroundColor = Color.FromHex("#87CEFA");
                                }
                                else
                                {
                                    string key2 = "WorkOrderLaborHours2:" + workorderLabour.WorkOrderLaborID;
                                    WorkOrderLaborStorge.Storage.Delete(key2);
                                }


                            }
                            catch (Exception)
                            {


                            }
                            await this.OnViewAppearingAsync(null);
                        }
                    }
                    catch (Exception ex)
                    {


                    }

                }
            }
            catch (Exception)
            {


            }
        }

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
                if (Application.Current.Properties.ContainsKey("CreateTask"))
                {
                    CreateTask = Application.Current.Properties["CreateTask"].ToString();
                }
                if (DisabledTextIsEnable == false)
                {
                    if (CreateTask == "E")
                    {
                        var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() { AddTaskTitle, LogoutTitle });

                        if (response == LogoutTitle)
                        {
                            await _authenticationService.LogoutAsync();
                            await NavigationService.NavigateToAsync<LoginPageViewModel>();
                            await NavigationService.RemoveBackStackAsync();
                        }

                        if (response == AddTaskTitle)
                        {
                            await NavigationService.NavigateToAsync<CreateTaskPageViewModel>(new PageParameters() { Parameter = WorkorderID });

                        }
                    }
                    else if (CreateTask == "V")
                    {
                        var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() { AddTaskTitle, LogoutTitle });

                        if (response == LogoutTitle)
                        {
                            await _authenticationService.LogoutAsync();
                            await NavigationService.NavigateToAsync<LoginPageViewModel>();
                            await NavigationService.RemoveBackStackAsync();
                        }

                    }
                    else
                    {
                        var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() { LogoutTitle });

                        if (response == LogoutTitle)
                        {
                            await _authenticationService.LogoutAsync();
                            await NavigationService.NavigateToAsync<LoginPageViewModel>();
                            await NavigationService.RemoveBackStackAsync();
                        }

                    }

                }
                else
                {
                    var response = await DialogService.SelectActionAsync(SelectOptionsTitle, SelectTitle, CancelTitle, new ObservableCollection<string>() { LogoutTitle });

                    if (response == LogoutTitle)
                    {
                        await _authenticationService.LogoutAsync();
                        await NavigationService.NavigateToAsync<LoginPageViewModel>();
                        await NavigationService.RemoveBackStackAsync();
                    }
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


                try
                {
                    OperationInProgress = true;
                  
                    if (string.IsNullOrWhiteSpace(SearchText))
                    {
                        await GenerateTaskAndLabourLayout();
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
            CustomEntry e1 = sender as CustomEntry;
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
            CustomEntry e1 = sender as CustomEntry;
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
